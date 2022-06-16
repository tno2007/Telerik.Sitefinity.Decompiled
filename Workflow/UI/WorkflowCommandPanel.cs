// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowCommandPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Workflow.Configuration;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>Command panel control for the workflow module.</summary>
  public class WorkflowCommandPanel : ViewModeControl<WorkflowControlPanel>, ICommandPanel
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowSidebar.ascx");
    private const string permissionsDialogUrl = "~/Sitefinity/Dialog/ModulePermissionsDialog";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Workflow.UI.WorkflowCommandPanel" /> class.
    /// </summary>
    /// <param name="controlPanel">The control panel.</param>
    public WorkflowCommandPanel(IControlPanel controlPanel)
    {
      this.ControlPanel = controlPanel;
      this.LayoutTemplatePath = WorkflowCommandPanel.layoutTemplatePath;
    }

    /// <summary>
    /// Reference to the control panel tied to the command panel instance.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// This property is used for communication between the command panel and its control
    /// panel.
    /// </remarks>
    /// <example>
    /// You can refer to <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface for more
    /// complicated example implementing the whole
    /// <see cref="T:Telerik.Sitefinity.Web.UI.Backend.ICommandPanel">ICommandPanel</see> interface.
    /// </example>
    public IControlPanel ControlPanel { get; set; }

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the field containing the permissions dialog url.</summary>
    protected virtual HiddenField PermissionsDialogUrlField => this.Container.GetControl<HiddenField>("hPermissionsDialogUrl", true);

    /// <summary>
    /// Gets the full assembly name of the TaxonomyManager class
    /// </summary>
    protected virtual HiddenField PermissionManagerClassName => this.Container.GetControl<HiddenField>("hPermissionManagerClassName", true);

    /// <summary>Gets the field containing the current user id.</summary>
    protected virtual HiddenField CurrentUserIdField => this.Container.GetControl<HiddenField>("hCurrentUserId", true);

    /// <summary>Gets the repeater displaying all content types.</summary>
    protected internal virtual Repeater ContentTypesRepeater => this.Container.GetControl<Repeater>("contentTypes", true);

    /// <summary>Gets the repeater displaying all sites.</summary>
    protected internal virtual Repeater SitesRepeater => this.Container.GetControl<Repeater>("sites", true);

    /// <summary>Gets the repeater displaying all languages.</summary>
    protected internal virtual Repeater LanguagesRepeater => this.Container.GetControl<Repeater>("languages", true);

    protected internal virtual LinkButton LanguageFilterLink => this.Container.GetControl<LinkButton>("languageFilterLink", true);

    protected internal virtual LinkButton SiteFilterLink => this.Container.GetControl<LinkButton>("siteFilterLink", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      LinkButton control = this.Container.GetControl<LinkButton>("permissionsButton", false);
      if (control != null && SystemManager.IsDBPMode)
        control.Visible = false;
      this.PermissionsDialogUrlField.Value = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ModulePermissionsDialog", UrlResolveOptions.Rooted);
      this.PermissionManagerClassName.Value = typeof (WorkflowManager).AssemblyQualifiedName;
      this.CurrentUserIdField.Value = SecurityManager.GetCurrentUserId().ToString();
      this.ContentTypesRepeater.DataSource = (object) Config.Get<WorkflowConfig>().Workflows.Values;
      this.ContentTypesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ContentTypesRepeater_ItemDataBound);
      this.ContentTypesRepeater.DataBind();
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
      {
        this.SiteFilterLink.Visible = true;
        this.SitesRepeater.DataSource = (object) multisiteContext.GetSites();
        this.SitesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.SitesRepeater_ItemDataBound);
        this.SitesRepeater.DataBind();
      }
      this.LanguageFilterLink.Visible = true;
      this.LanguagesRepeater.DataSource = (object) AppSettings.CurrentSettings.DefinedFrontendLanguages;
      this.LanguagesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.LanguagesRepeater_ItemDataBound);
      this.LanguagesRepeater.DataBind();
    }

    private void SitesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
        return;
      ISite dataItem = (ISite) e.Item.DataItem;
      if (!(e.Item.FindControl("site") is LinkButton control))
        return;
      control.Text = HttpUtility.HtmlEncode(dataItem.Name);
      control.CssClass = string.Format("sf{0}", (object) dataItem.Id);
      control.OnClientClick = string.Format("filter('SiteScope={0}'); return false;", (object) dataItem.Id);
    }

    private void LanguagesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
        return;
      CultureInfo dataItem = (CultureInfo) e.Item.DataItem;
      if (!(e.Item.FindControl("language") is LinkButton control))
        return;
      string displayName = dataItem.DisplayName;
      control.Text = displayName;
      control.CssClass = string.Format("sf{0}", (object) dataItem.Name);
      control.OnClientClick = string.Format("filter('CultureScope={0}'); return false;", (object) dataItem.Name);
    }

    private void ContentTypesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.AlternatingItem && e.Item.ItemType != ListItemType.Item)
        return;
      WorkflowElement dataItem = (WorkflowElement) e.Item.DataItem;
      if (!(e.Item.FindControl("contentType") is LinkButton control))
        return;
      string title = dataItem.Title;
      if (!string.IsNullOrEmpty(dataItem.ResourceClassId))
        title = Res.Get(dataItem.ResourceClassId, title);
      string[] separator = new string[1]{ "." };
      string[] strArray = dataItem.ContentType.Split(separator, StringSplitOptions.RemoveEmptyEntries);
      control.Text = title;
      if (strArray.Length != 0)
        control.CssClass = string.Format("sf{0}", (object) strArray[strArray.Length - 1]);
      control.OnClientClick = string.Format("filter('ContentScope={0}'); return false;", (object) dataItem.ContentType);
    }
  }
}
