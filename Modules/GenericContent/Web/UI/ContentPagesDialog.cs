// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>
  /// A dialog listing all pages containing a given content block.
  /// </summary>
  public class ContentPagesDialog : PagesAndTemplatesDialog
  {
    /// <summary>
    /// Gets the name of resource file representing the dialog.
    /// </summary>
    public static readonly string DialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.GenericContent.ContentPagesDialog.ascx");
    private ContentManager contentManager;
    private PageManager pageManager;
    private string pageProviderName;
    private string contentProviderName;

    /// <summary>Gets or sets the name of the content provider.</summary>
    /// <value>The name of the content provider.</value>
    public string ContentProviderName
    {
      get
      {
        if (this.contentProviderName == null)
          this.contentProviderName = SystemManager.CurrentHttpContext.Request.QueryString["contentProviderName"];
        return this.contentProviderName;
      }
      set => this.contentProviderName = value;
    }

    public Guid ContentId { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      string g = this.Page.Request.QueryString["contentId"];
      if (!string.IsNullOrEmpty(g) && Utility.IsGuid(g))
      {
        this.ContentId = new Guid(g);
        this.contentManager = ContentManager.GetManager(this.ContentProviderName);
        this.pageManager = PageManager.GetManager();
      }
      base.InitializeControls(dialogContainer);
      if (this.IsSuccessfullyUpdatedDialog)
        this.CloseLink.OnClientClick = "dialogBase.close('saveEditorChanges');_closeContentPagesDlg();return false;";
      else
        this.CloseLink.OnClientClick = "dialogBase.close();_closeContentPagesDlg();return false;";
    }

    [Obsolete("Use the GetPageDataList method instead")]
    protected override IEnumerable<PageNode> GetPages() => this.ContentId != Guid.Empty ? this.contentManager.GetPagesByContent(this.ContentId, this.pageManager) : (IEnumerable<PageNode>) new PageNode[0];

    protected override IEnumerable<PageData> GetPageDataList() => this.ContentId != Guid.Empty ? this.contentManager.GetPageDataByContent(this.ContentId, this.pageManager) : (IEnumerable<PageData>) new PageData[0];

    protected override IEnumerable<PageTemplate> GetTemplates() => this.ContentId != Guid.Empty ? this.contentManager.GetPageTemplatesByContent(this.ContentId, this.pageManager) : (IEnumerable<PageTemplate>) new PageTemplate[0];

    protected override string GetTemplatePath() => ContentPagesDialog.DialogTemplatePath;
  }
}
