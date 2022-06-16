// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.ControlPresentationResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web.Caching;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  internal class ControlPresentationResolver : IVirtualFileResolver
  {
    internal static readonly string RootPath = "~/SfCtrlPresentation/*";
    internal const int ApproximateControlTemplateCount = 500;
    private static ConcurrentDictionary<string, string> hashToControlPathCache;
    private static ConcurrentDictionary<string, string> controlPathToHashCache;

    static ControlPresentationResolver()
    {
      int concurrencyLevel = 4 * Environment.ProcessorCount;
      ControlPresentationResolver.hashToControlPathCache = new ConcurrentDictionary<string, string>(concurrencyLevel, 500);
      ControlPresentationResolver.controlPathToHashCache = new ConcurrentDictionary<string, string>(concurrencyLevel, 500);
    }

    public bool Exists(PathDefinition definition, string virtualPath) => ControlPresentationResolver.TryExtractControlInfo(virtualPath, out ControlPresentationResolver.ControlPresentationInfo _);

    public Stream Open(PathDefinition definition, string virtualPath)
    {
      ControlPresentationResolver.ControlPresentationInfo controlPresentationInfo;
      if (ControlPresentationResolver.TryExtractControlInfo(virtualPath, out controlPresentationInfo))
      {
        if (controlPresentationInfo.HasData)
        {
          ControlPresentation controlPresentation = controlPresentationInfo.GetControlPresentation();
          string data = controlPresentation.Data;
          if (!string.IsNullOrEmpty(data))
            return (Stream) new MemoryStream(RouteHelper.GetContentWithPreamble(data));
          string str = !string.IsNullOrEmpty(controlPresentation.EmbeddedTemplateName) ? controlPresentation.EmbeddedTemplateName : throw new ApplicationException("Invalid ControlPresentation template name " + controlPresentation.EmbeddedTemplateName);
          if (str.StartsWith("~/SFRes/"))
            str = str.Substring("~/SFRes/".Length);
          return Assembly.Load(str).GetManifestResourceStream(str);
        }
        if (!string.IsNullOrEmpty(controlPresentationInfo.FallbackVirtualPath))
          return VirtualPathManager.OpenFile(controlPresentationInfo.FallbackVirtualPath);
      }
      throw new FileNotFoundException("No ControlPresentation could be identified by " + virtualPath);
    }

    public CacheDependency GetCacheDependency(
      PathDefinition definition,
      string virtualPath,
      IEnumerable virtualPathDependencies,
      DateTime utcStart)
    {
      ControlPresentationResolver.ControlPresentationInfo controlPresentationInfo;
      return ControlPresentationResolver.TryExtractControlInfo(virtualPath, out controlPresentationInfo) ? controlPresentationInfo.GetCacheDependency() : (CacheDependency) null;
    }

    internal static string GenerateUrl(Guid dataId, string pageProviderName = null)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("~/SfCtrlPresentation/");
      if (string.IsNullOrWhiteSpace(pageProviderName))
        pageProviderName = Config.Get<PagesConfig>().DefaultProvider;
      stringBuilder.Append(pageProviderName);
      stringBuilder.Append('_');
      stringBuilder.Append(dataId.ToString("N", (IFormatProvider) CultureInfo.InvariantCulture));
      stringBuilder.Append(".ascx");
      return stringBuilder.ToString();
    }

    internal static string GenerateUrl(
      Type controlType,
      string fallbackVirtualPath,
      string pageProviderName = null)
    {
      string controlPath = controlType.FullName + "/" + fallbackVirtualPath;
      string hash = ControlPresentationResolver.ComputeHash(controlPath);
      ControlPresentationResolver.hashToControlPathCache[hash] = controlPath;
      return "~/SfCtrlPresentation/" + "_SFCT_/" + hash + "/" + controlType.Name + ".ascx";
    }

    private static string ResolveHashedControlPath(string controlPath)
    {
      int length = controlPath.LastIndexOf('/');
      if (length != -1)
      {
        string key = controlPath.Substring(0, length);
        string str;
        if (ControlPresentationResolver.hashToControlPathCache.TryGetValue(key, out str))
          controlPath = str;
      }
      return controlPath;
    }

    private static string ComputeHash(string controlPath)
    {
      string hash1;
      if (ControlPresentationResolver.controlPathToHashCache.TryGetValue(controlPath, out hash1))
        return hash1;
      byte[] hash2;
      using (SHA1CryptoServiceProvider cryptoServiceProvider = new SHA1CryptoServiceProvider())
        hash2 = cryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(controlPath));
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < hash2.Length; ++index)
        stringBuilder.AppendFormat("{0:x2}", (object) hash2[index]);
      string hash3 = stringBuilder.ToString();
      ControlPresentationResolver.controlPathToHashCache[controlPath] = hash3;
      return hash3;
    }

    private static bool TryExtractControlInfo(
      string url,
      out ControlPresentationResolver.ControlPresentationInfo controlPresentationInfo)
    {
      controlPresentationInfo = (ControlPresentationResolver.ControlPresentationInfo) null;
      IDictionary httpContextItems = SystemManager.HttpContextItems;
      if (httpContextItems != null)
        controlPresentationInfo = httpContextItems[(object) url] as ControlPresentationResolver.ControlPresentationInfo;
      if (controlPresentationInfo != null)
        return true;
      string providerName = (string) null;
      string str1 = UrlPath.ResolveUrl("~/SfCtrlPresentation/");
      if (string.IsNullOrWhiteSpace(url) || !url.StartsWith(str1, StringComparison.OrdinalIgnoreCase) || !url.EndsWith(".ascx", StringComparison.OrdinalIgnoreCase))
        return false;
      string str2 = url.Substring(str1.Length);
      if (str2.StartsWith("_SFCT_/"))
      {
        string[] strArray = ControlPresentationResolver.ResolveHashedControlPath(str2.Substring("_SFCT_/".Length)).Split(new char[1]
        {
          '/'
        }, 2, StringSplitOptions.RemoveEmptyEntries);
        if (strArray.Length == 2)
          controlPresentationInfo = (ControlPresentationResolver.ControlPresentationInfo) new ControlPresentationResolver.ControlPresentationInfoByControlType(strArray[0], strArray[1]);
      }
      else if (str2.StartsWith("ByName/"))
      {
        string key;
        if (!ControlPresentationResolver.GetKeyAndProviderName(!str2.EndsWith(".ascx") ? str2.Substring("ByName/".Length, str2.Length - "ByName/".Length) : str2.Substring("ByName/".Length, str2.Length - "ByName/".Length - ".ascx".Length), out key, out providerName))
          return false;
        controlPresentationInfo = (ControlPresentationResolver.ControlPresentationInfo) new ControlPresentationResolver.ControlPresentationInfoByName(providerName, key);
      }
      else
      {
        string key;
        if (!ControlPresentationResolver.GetKeyAndProviderName(str2.Substring(0, str2.Length - ".ascx".Length), out key, out providerName))
          return false;
        Guid result;
        if (Guid.TryParseExact(key, "N", out result))
          controlPresentationInfo = (ControlPresentationResolver.ControlPresentationInfo) new ControlPresentationResolver.ControlPresentationInfoById(providerName, result);
      }
      if (controlPresentationInfo == null)
        return false;
      if (httpContextItems != null)
        httpContextItems[(object) url] = (object) controlPresentationInfo;
      return true;
    }

    private static bool GetKeyAndProviderName(
      string input,
      out string key,
      out string providerName)
    {
      int length = input.IndexOf('_');
      if (length != -1)
      {
        providerName = input.Substring(0, length);
        providerName = providerName.Trim();
        if (!Config.Get<PagesConfig>().Providers.ContainsKey(providerName))
        {
          providerName = (string) null;
          key = (string) null;
          return false;
        }
        key = input.Substring(length + 1).Trim();
      }
      else
      {
        providerName = Config.Get<PagesConfig>().DefaultProvider;
        key = input.Trim();
      }
      return true;
    }

    internal static ConcurrentDictionary<string, string> HashToControlPathCache => ControlPresentationResolver.hashToControlPathCache;

    internal abstract class ControlPresentationInfo
    {
      private string providerName;
      private ControlPresentation controlPresentation;
      private bool? hasData;

      public ControlPresentationInfo(string providerName) => this.providerName = providerName;

      public string ProviderName
      {
        get
        {
          if (this.providerName == null)
            this.providerName = Config.Get<PagesConfig>().DefaultProvider;
          return this.providerName;
        }
      }

      public bool HasData
      {
        get
        {
          if (!this.hasData.HasValue)
            this.GetControlPresentation();
          return this.hasData.Value;
        }
      }

      public string FallbackVirtualPath { get; set; }

      public ControlPresentation GetControlPresentation()
      {
        if (this.controlPresentation == null && !this.hasData.HasValue)
        {
          this.controlPresentation = this.GetControlPresentationInternal(PageManager.GetManager(this.ProviderName));
          this.hasData = new bool?(this.controlPresentation != null);
        }
        return this.controlPresentation;
      }

      public abstract CacheDependency GetCacheDependency();

      protected abstract ControlPresentation GetControlPresentationInternal(
        PageManager manager);
    }

    internal class ControlPresentationInfoById : ControlPresentationResolver.ControlPresentationInfo
    {
      private Guid id;

      public ControlPresentationInfoById(string providerName, Guid id)
        : base(providerName)
      {
        this.id = id;
      }

      public Guid Id => this.id;

      protected override ControlPresentation GetControlPresentationInternal(
        PageManager manager)
      {
        return manager.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (item => item.Id == this.Id)).SingleOrDefault<ControlPresentation>();
      }

      public override CacheDependency GetCacheDependency() => (CacheDependency) new ControlPresentationCacheDependency(this.Id.ToString(), this.ProviderName);
    }

    internal class ControlPresentationInfoByName : 
      ControlPresentationResolver.ControlPresentationInfo
    {
      private string templateName;

      public ControlPresentationInfoByName(string providerName, string templateName)
        : base(providerName)
      {
        this.templateName = templateName;
      }

      public string TemplateName => this.templateName;

      protected override ControlPresentation GetControlPresentationInternal(
        PageManager manager)
      {
        return manager.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (item => item.NameForDevelopers == this.TemplateName)).FirstOrDefault<ControlPresentation>();
      }

      public override CacheDependency GetCacheDependency() => (CacheDependency) new ControlPresentationCacheDependency(PageManager.GetManager(this.ProviderName).GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (item => item.NameForDevelopers == this.TemplateName)).Select<ControlPresentation, Guid>((Expression<Func<ControlPresentation, Guid>>) (item => item.Id)).FirstOrDefault<Guid>().ToString(), this.ProviderName);
    }

    internal class ControlPresentationInfoByControlType : 
      ControlPresentationResolver.ControlPresentationInfo
    {
      private string controlType;
      private string templateName;

      public ControlPresentationInfoByControlType(string controlType, string templateName)
        : base((string) null)
      {
        this.controlType = controlType;
        int num = templateName.LastIndexOf('/');
        if (num != 0 && num < templateName.Length)
        {
          this.FallbackVirtualPath = templateName;
          templateName = templateName.Substring(num + 1);
        }
        this.templateName = templateName;
      }

      public string ControlType => this.controlType;

      public string TemplateName => this.templateName;

      protected override ControlPresentation GetControlPresentationInternal(
        PageManager manager)
      {
        return manager.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (item => item.ControlType == this.ControlType && item.EmbeddedTemplateName == this.templateName)).SingleOrDefault<ControlPresentation>();
      }

      public override CacheDependency GetCacheDependency() => (CacheDependency) new ControlPresentationCacheDependency((this.ControlType + "_" + this.TemplateName).ToUpperInvariant(), this.ProviderName);
    }
  }
}
