// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.Services.AsyncToolsetXmlUploadHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Web;
using System.Xml;
using Telerik.Sitefinity.Configuration.Basic;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Configuration.Web.Services
{
  public class AsyncToolsetXmlUploadHandler : IHttpHandler
  {
    private GeneralErrorModel ToolsetUploadResult;

    public bool IsReusable => false;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      RouteHelper.ApplyThreadCulturesForCurrentUser();
      this.ToolsetUploadResult = (GeneralErrorModel) null;
      if (context.Request.Files.Count > 0)
        this.StoreToolsetXmlFile(context.Request.Files[0]);
      context.Response.Write(WcfHelper.SerializeToJson((object) this.ToolsetUploadResult, typeof (GeneralErrorModel)));
      context.Response.End();
    }

    internal void StoreToolsetXmlFile(HttpPostedFile file)
    {
      using (StreamReader streamReader = new StreamReader(file.InputStream))
        this.SaveTextEditorToolSetConfiguration(Path.GetFileNameWithoutExtension(file.FileName), streamReader.ReadToEnd());
    }

    private void SaveTextEditorToolSetConfiguration(string toolSetName, string toolSetXml)
    {
      if (!this.ValidateTextEditorToolSet(toolSetXml))
        return;
      using (ConfigManager manager = ConfigManager.GetManager())
      {
        AppearanceConfig section = manager.GetSection<AppearanceConfig>();
        if (this.isStandardEditorConfiguration(toolSetName))
          section.StandardEditorConfiguration = toolSetXml;
        else if (this.isMinimalEditorConfiguration(toolSetName))
        {
          section.MinimalEditorConfiguration = toolSetXml;
        }
        else
        {
          ConfigValueDictionary editorConfigurations = section.EditorConfigurations;
          if (editorConfigurations.ContainsKey(toolSetName))
            editorConfigurations[toolSetName] = toolSetXml;
          else
            editorConfigurations.Add(toolSetName, toolSetXml);
        }
        manager.SaveSection((ConfigSection) section);
      }
    }

    private bool ValidateTextEditorToolSet(string toolSetXml)
    {
      try
      {
        new XmlDocument().LoadXml(toolSetXml);
        this.ToolsetUploadResult = new GeneralErrorModel()
        {
          IsError = false
        };
        return true;
      }
      catch (Exception ex)
      {
        this.ToolsetUploadResult = new GeneralErrorModel()
        {
          Message = ex.Message
        };
        return false;
      }
    }

    private bool isStandardEditorConfiguration(string configurationName) => configurationName == "Default tool set";

    private bool isMinimalEditorConfiguration(string configurationName) => configurationName == "Tool set for comments";
  }
}
