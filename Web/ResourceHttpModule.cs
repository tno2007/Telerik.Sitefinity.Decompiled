// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceHttpModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  internal class ResourceHttpModule : IHttpModule
  {
    internal const string ModuleName = "ResourceModule";
    private MethodInfo decryptionMethod;
    private static readonly ConcurrentDictionary<string, string> resourceCache = new ConcurrentDictionary<string, string>();
    private readonly Regex _webResourceRegex = new Regex("<%\\s*=\\s*WebResource\\(\"(?<resourceName>[^\"]*)\"\\)\\s*%>", RegexOptions.Multiline | RegexOptions.Compiled | RegexOptions.Singleline);

    public void Dispose()
    {
    }

    public void Init(HttpApplication context) => context.BeginRequest += new EventHandler(this.OnBeginRequest);

    private void OnBeginRequest(object sender, EventArgs e)
    {
      try
      {
        string str = SystemManager.CurrentHttpContext.Request.QueryStringGet("d");
        if (str == null)
          return;
        string path = (string) null;
        Stream stream = (Stream) null;
        this.EnsureScriptCombiningDisabled();
        if (!ResourceHttpModule.resourceCache.TryGetValue(str, out path))
        {
          string decryptedResourceKey = this.DecryptScriptResource(str);
          if (decryptedResourceKey != null)
          {
            string resourcePath = (string) null;
            stream = this.GetScriptResourceStream(decryptedResourceKey, out resourcePath);
            if (stream != null && !ResourceHttpModule.resourceCache.ContainsKey(str))
              ResourceHttpModule.resourceCache.TryAdd(str, resourcePath);
          }
        }
        else
          stream = (Stream) new FileStream(path, FileMode.Open, FileAccess.Read);
        if (stream == null)
          return;
        using (stream)
        {
          if (path == null)
            return;
          if (path.EndsWith("js"))
          {
            string contentType = "application/x-javascript";
            this.WriteResponse(stream, contentType);
          }
          else
          {
            if (!path.EndsWith("css"))
              return;
            using (StreamReader streamReader = new StreamReader(stream))
            {
              string s = this._webResourceRegex.Replace(streamReader.ReadToEnd(), (MatchEvaluator) (match => new Page().ClientScript.GetWebResourceUrl(((IEnumerable<Type>) ((IEnumerable<Assembly>) AppDomain.CurrentDomain.GetAssemblies()).FirstOrDefault<Assembly>((Func<Assembly, bool>) (x => x.GetName().Name == "Telerik.Sitefinity.Resources")).GetTypes()).First<Type>(), match.Groups["resourceName"].Value)));
              string contentType = "text/css";
              this.WriteResponse((Stream) new MemoryStream(Encoding.UTF8.GetBytes(s)), contentType);
            }
          }
        }
      }
      catch (Exception ex)
      {
      }
    }

    private string DecryptScriptResource(string urlEncodedData)
    {
      try
      {
        byte[] numArray = HttpServerUtility.UrlTokenDecode(urlEncodedData);
        return Encoding.UTF8.GetString((byte[]) this.DecryptionMethod.Invoke((object) null, new object[5]
        {
          (object) false,
          (object) numArray,
          null,
          (object) 0,
          (object) numArray.Length
        }));
      }
      catch (TargetInvocationException ex)
      {
        return (string) null;
      }
    }

    private void IgnoringClosedConnection(Action action)
    {
      try
      {
        action();
      }
      catch (HttpException ex)
      {
        switch (ex.ErrorCode)
        {
          case -2147024832:
            break;
          case -2147023901:
            break;
          case -2147023667:
            break;
          default:
            throw;
        }
      }
    }

    private Stream GetScriptResourceStream(
      string decryptedResourceKey,
      out string resourcePath)
    {
      resourcePath = (string) null;
      string pattern = "^.*?(?<project>Telerik.Sitefinity.*)\\|(?<embeddedScript>.*\\.(js|css))|$";
      Match match = Regex.Match(decryptedResourceKey, pattern);
      return match.Success ? ResourceHttpModule.GetWebResourceStream(match.Groups["project"].Value, match.Groups["embeddedScript"].Value, out resourcePath) : (Stream) null;
    }

    /// <summary>Gets the web resource stream.</summary>
    /// <param name="projectName">Name of the project.</param>
    /// <param name="embeddedResource">The embedded resource. (E.g Telerik.Sitefinity.Web.UI.Backend.SiteSelector.js)</param>
    /// <param name="resourcePath">The resource path.</param>
    /// <returns></returns>
    internal static Stream GetWebResourceStream(
      string projectName,
      string embeddedResource,
      out string resourcePath)
    {
      resourcePath = (string) null;
      if (!projectName.IsNullOrEmpty() && !embeddedResource.IsNullOrEmpty())
      {
        resourcePath = ResourceHttpModule.GetScriptFromResource(projectName, embeddedResource);
        if (!string.IsNullOrEmpty(resourcePath) && File.Exists(resourcePath))
          return (Stream) new FileStream(resourcePath, FileMode.Open, FileAccess.Read);
      }
      return (Stream) null;
    }

    private static string GetScriptFromResource(string projectName, string embeddedResource)
    {
      string resourcePath = (string) null;
      return ResourceHttpModule.TryGetScriptFromResource(embeddedResource, projectName, out resourcePath) || ResourceHttpModule.TryGetModuleSpecificScript(embeddedResource, out resourcePath) || ResourceHttpModule.TryGetRecycleBinSpecificScript(embeddedResource, out resourcePath) || ResourceHttpModule.TryGetSiteSyncSpecificScript(embeddedResource, out resourcePath) ? resourcePath : (string) null;
    }

    private static bool TryGetScriptFromResource(
      string embeddedResource,
      string projectName,
      out string resourcePath)
    {
      string str1 = string.Format("{0}\\{1}", (object) ResourceHttpModule.GetSolutionDir(), (object) projectName);
      string extension = VirtualPathUtility.GetExtension(embeddedResource);
      string str2 = embeddedResource.Substring(projectName.Length + 1, embeddedResource.Length - projectName.Length - 1 - extension.Length).Replace(".", "\\");
      resourcePath = string.Format("{0}\\{1}{2}", (object) str1, (object) str2, (object) extension);
      if (File.Exists(resourcePath))
        return true;
      resourcePath = (string) null;
      return false;
    }

    private static bool TryGetRecycleBinSpecificScript(
      string embeddedResource,
      out string resourcePath)
    {
      string pattern = "^Telerik\\.Sitefinity\\.RecycleBin\\.(?<embeddedScript>.*)\\.js";
      Match match = Regex.Match(embeddedResource, pattern);
      if (match.Success)
      {
        string solutionDir = ResourceHttpModule.GetSolutionDir();
        string extension = VirtualPathUtility.GetExtension(embeddedResource);
        string str1 = match.Groups["embeddedScript"].Value;
        string str2 = "Telerik.Sitefinity.RecycleBin";
        string str3 = str1.Replace(".", "\\");
        resourcePath = string.Format("{0}\\{1}\\{2}{3}", (object) solutionDir, (object) str2, (object) str3, (object) extension);
        return true;
      }
      resourcePath = (string) null;
      return false;
    }

    private static bool TryGetSiteSyncSpecificScript(
      string embeddedResource,
      out string resourcePath)
    {
      string pattern = "^Telerik\\.Sitefinity\\.SiteSync\\.(?<embeddedScript>.*)\\.js";
      Match match = Regex.Match(embeddedResource, pattern);
      if (match.Success)
      {
        string solutionDir = ResourceHttpModule.GetSolutionDir();
        string extension = VirtualPathUtility.GetExtension(embeddedResource);
        string str1 = match.Groups["embeddedScript"].Value;
        string str2 = "Telerik.Sitefinity.SiteSync.Impl";
        string str3 = str1.Replace(".", "\\");
        resourcePath = string.Format("{0}\\{1}\\{2}{3}", (object) solutionDir, (object) str2, (object) str3, (object) extension);
        return true;
      }
      resourcePath = (string) null;
      return false;
    }

    private static string GetSolutionDir()
    {
      string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
      return baseDirectory.Substring(0, baseDirectory.LastIndexOf("\\SitefinityWebApp"));
    }

    private static bool TryGetModuleSpecificScript(string embeddedResource, out string resourcePath)
    {
      string pattern = "^Telerik\\.Sitefinity\\.Modules\\.(?<moduleName>.*?)\\.(?<embeddedScript>.*)\\.js";
      Match match = Regex.Match(embeddedResource, pattern);
      if (match.Success)
      {
        string str1 = match.Groups["moduleName"].Value;
        string str2 = match.Groups["embeddedScript"].Value;
        string solutionDir = ResourceHttpModule.GetSolutionDir();
        string extension = VirtualPathUtility.GetExtension(embeddedResource);
        if (!(str1 == "Events") && !(str1 == "News") && !(str1 == "Blogs") && !(str1 == "Lists"))
        {
          if (str1 == "Ecommerce")
          {
            string str3 = "Telerik.Sitefinity.Ecommerce";
            string str4 = string.Format("{0}\\{1}", (object) solutionDir, (object) str3);
            string str5 = str2.Replace(".", "\\");
            resourcePath = string.Format("{0}\\{1}{2}", (object) str4, (object) str5, (object) extension);
            return true;
          }
        }
        else
        {
          string str6 = "Telerik.Sitefinity.ContentModules";
          string str7 = string.Format("{0}\\{1}", (object) solutionDir, (object) str6);
          string str8 = (str1 + "." + str2).Replace(".", "\\");
          resourcePath = string.Format("{0}\\{1}{2}", (object) str7, (object) str8, (object) extension);
          return true;
        }
      }
      resourcePath = (string) null;
      return false;
    }

    private void WriteResponse(Stream inputStream, string contentType)
    {
      HttpResponseBase response = SystemManager.CurrentHttpContext.Response;
      response.Clear();
      response.StatusCode = 200;
      response.StatusDescription = "OK";
      response.Buffer = false;
      response.ContentType = contentType;
      response.AppendHeader("Content-length", inputStream.Length.ToString());
      byte[] buffer = new byte[10240];
      int bytesRead = buffer.Length;
      if (bytesRead > 0)
        this.IgnoringClosedConnection((Action) (() =>
        {
          while (bytesRead == buffer.Length)
          {
            bytesRead = inputStream.Read(buffer, 0, buffer.Length);
            if (bytesRead > 0)
              response.OutputStream.Write(buffer, 0, bytesRead);
          }
        }));
      SystemManager.CurrentHttpContext.ApplicationInstance.CompleteRequest();
    }

    private void EnsureScriptCombiningDisabled()
    {
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      bool? combineScriptsBackEnd1 = pagesConfig.CombineScriptsBackEnd;
      bool flag1 = true;
      if (!(combineScriptsBackEnd1.GetValueOrDefault() == flag1 & combineScriptsBackEnd1.HasValue))
      {
        bool? combineScriptsFrontEnd = pagesConfig.CombineScriptsFrontEnd;
        bool flag2 = true;
        if (!(combineScriptsFrontEnd.GetValueOrDefault() == flag2 & combineScriptsFrontEnd.HasValue))
          return;
      }
      bool? combineScriptsBackEnd2 = pagesConfig.CombineScriptsBackEnd;
      bool flag3 = true;
      if (!(combineScriptsBackEnd2.GetValueOrDefault() == flag3 & combineScriptsBackEnd2.HasValue))
      {
        bool? combineScriptsFrontEnd = pagesConfig.CombineScriptsFrontEnd;
        bool flag4 = true;
        if (!(combineScriptsFrontEnd.GetValueOrDefault() == flag4 & combineScriptsFrontEnd.HasValue))
          return;
      }
      ConfigManager manager = ConfigManager.GetManager();
      using (new ElevatedModeRegion((IManager) manager))
      {
        PagesConfig section = manager.GetSection<PagesConfig>();
        section.CombineScriptsBackEnd = new bool?(false);
        section.CombineScriptsFrontEnd = new bool?(false);
        manager.SaveSection((ConfigSection) section);
      }
    }

    private MethodInfo DecryptionMethod
    {
      get
      {
        if (this.decryptionMethod == (MethodInfo) null)
          this.decryptionMethod = typeof (MachineKeySection).GetMethod("EncryptOrDecryptData", BindingFlags.Static | BindingFlags.NonPublic, (Binder) null, new Type[5]
          {
            typeof (bool),
            typeof (byte[]),
            typeof (byte[]),
            typeof (int),
            typeof (int)
          }, (ParameterModifier[]) null);
        return this.decryptionMethod;
      }
    }
  }
}
