// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ContentTypeDashboard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  /// <summary>
  /// This control represents the dashboard of the single content type.
  /// </summary>
  public class ContentTypeDashboard : KendoView
  {
    private ModuleBuilderManager moduleBuilderMng;
    private Guid moduleId;
    private DynamicModule module;
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.ContentTypeDashboard.ascx");
    private static readonly string webServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/DynamicModules/ContentTypeService.svc/");
    private static readonly Regex isGuid = new Regex("^(\\{){0,1}[0-9a-fA-F]{8}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{4}\\-[0-9a-fA-F]{12}(\\}){0,1}$", RegexOptions.Compiled);

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = ContentTypeDashboard.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderManager" />.
    /// </summary>
    protected ModuleBuilderManager ModuleBuilderMngr
    {
      get
      {
        if (this.moduleBuilderMng == null)
          this.moduleBuilderMng = ModuleBuilderManager.GetManager();
        return this.moduleBuilderMng;
      }
    }

    /// <summary>
    /// Gets the id of the module for which the dashboard is being displayed.
    /// </summary>
    protected Guid ModuleId
    {
      get
      {
        if (this.moduleId == Guid.Empty)
          this.moduleId = new Guid(ControlExtensions.GetUrlParameters(this.Page)[0]);
        return this.moduleId;
      }
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> which represents the
    /// content type.
    /// </summary>
    protected DynamicModule Module
    {
      get => this.module;
      private set
      {
        if (value == null)
          return;
        this.module = value;
      }
    }

    /// <summary>Gets the module type of this module.</summary>
    protected DynamicModuleType ModuleType => this.ModuleBuilderMngr.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == this.ModuleId)).First<DynamicModuleType>();

    /// <summary>Gets the reference to the title of the page</summary>
    protected virtual ITextControl PageTitle => this.Container.GetControl<ITextControl>("pageTitle", true);

    /// <summary>Gets the reference to the module name literal.</summary>
    protected virtual ITextControl ModuleNameLiteral => this.Container.GetControl<ITextControl>("moduleNameLiteral", true);

    /// <summary>Gets the reference to the module status literal.</summary>
    protected virtual Label ModuleStatusLabel => this.Container.GetControl<Label>("moduleStatus", true);

    /// <summary>Gets the reference to the module description literal.</summary>
    protected virtual ITextControl ModuleDescriptionLiteral => this.Container.GetControl<ITextControl>("moduleDescriptionLiteral", true);

    /// <summary>
    /// Gets the reference to the button that activates the module.
    /// </summary>
    protected virtual IButtonControl ActivateModuleButton => this.Container.GetControl<IButtonControl>("activateModuleButton", true);

    /// <summary>
    /// Gets the reference to the button that inactivates the module.
    /// </summary>
    protected virtual IButtonControl InactivateModuleButton => this.Container.GetControl<IButtonControl>("inactivateModuleButton", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.HiddenField" /> that holds the id of the current module.
    /// </summary>
    protected virtual HiddenField ModuleIdHidden => this.Container.GetControl<HiddenField>("moduleIdHidden", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.HiddenField" /> that holds the statusType of the current module.
    /// </summary>
    protected virtual HiddenField ModuleStatusType => this.Container.GetControl<HiddenField>("moduleStatusType", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.HyperLink" /> that takes the user to the code reference for
    /// the content type.
    /// </summary>
    protected virtual HyperLink CodeReferenceLink => this.Container.GetControl<HyperLink>("codeReferenceLink", true);

    /// <summary>Gets the cloud services link.</summary>
    protected virtual HyperLink CloudServicesLink => this.Container.GetControl<HyperLink>("cloudServicesLink", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the content types web service url.
    /// </summary>
    protected virtual HiddenField WebServiceUrlHidden => this.Container.GetControl<HiddenField>("webServiceUrlHidden", true);

    /// <summary>
    /// Gets the reference to the panel displayed when there is an error with url id
    /// </summary>
    protected virtual Panel NoModuleMessagePanel => this.Container.GetControl<Panel>("noModuleMessage", true);

    /// <summary>Gets the reference to the module settings panel</summary>
    protected virtual Panel ModuleSettingsPanel => this.Container.GetControl<Panel>("moduleSettings", true);

    /// <summary>
    /// Gets the reference to the panel displaying content-type information
    /// </summary>
    protected virtual Panel ModuleDashboardPanel => this.Container.GetControl<Panel>("moduleDashboard", true);

    /// <summary>
    /// Gets the reference to the control that displays the title of the module settings
    /// section.
    /// </summary>
    protected virtual ITextControl ModuleSettingsLiteral => this.Container.GetControl<ITextControl>("moduleSettingsLiteral", true);

    /// <summary>
    /// Hyperlink control which leads user to the screen for tweaking the backend screens of the
    /// dynamic modules.
    /// </summary>
    protected virtual HyperLink BackendScreenTweaksLink => this.Container.GetControl<HyperLink>("backendScreenTweaksLink", true);

    /// <summary>
    /// Hyperlink control which leads user back to current module.
    /// </summary>
    protected virtual Literal BackToCurrentModuleLiteral => this.Container.GetControl<Literal>("backToCurrentModuleLiteral", true);

    /// <summary>
    /// Input text control where the user enters the name of the content type.
    /// </summary>
    protected virtual HtmlInputText ContentTypeName => this.Container.GetControl<HtmlInputText>("contentTypeName", true);

    /// <summary>
    /// Text area control where the user enters the descrition of the content type.
    ///  </summary>
    protected virtual HtmlTextArea ContentTypeDescription => this.Container.GetControl<HtmlTextArea>("contentTypeDescription", true);

    /// <summary>
    /// Master view control which displays the content types of the current module.
    /// </summary>
    protected virtual ContentTypesMasterView ContentTypesMasterView => this.Container.GetControl<ContentTypesMasterView>("contentTypesMasterView", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (this.InitializeURLDependantVariables())
      {
        this.PageTitle.Text = "Module: <em>" + HttpUtility.HtmlEncode(this.Module.Title) + "</em>";
        this.ModuleNameLiteral.Text = HttpUtility.HtmlEncode(this.Module.Title);
        this.ModuleDescriptionLiteral.Text = HttpUtility.HtmlEncode(this.Module.Description);
        this.ContentTypeDescription.Value = this.Module.Description;
        this.ContentTypeName.Value = this.Module.Title;
        string str = Res.Get<ModuleBuilderResources>().NotInstalled;
        switch (this.Module.Status)
        {
          case DynamicModuleStatus.NotInstalled:
            str = Res.Get<ModuleBuilderResources>().Inactive;
            this.ActivateModuleButton.Text = Res.Get<ModuleBuilderResources>().ActivateThisContentType;
            this.ModuleStatusType.Value = DynamicModuleStatus.NotInstalled.ToString();
            break;
          case DynamicModuleStatus.Active:
            str = Res.Get<ModuleBuilderResources>().Active;
            this.ActivateModuleButton.Text = Res.Get<ModuleBuilderResources>().ActivateThisContentType;
            this.ModuleStatusType.Value = DynamicModuleStatus.Active.ToString();
            break;
          case DynamicModuleStatus.Inactive:
            str = Res.Get<ModuleBuilderResources>().Inactive;
            this.ActivateModuleButton.Text = Res.Get<ModuleBuilderResources>().ActivateThisContentType;
            this.ModuleStatusType.Value = DynamicModuleStatus.Inactive.ToString();
            break;
        }
        this.ModuleStatusLabel.Text = str;
        switch (this.Module.Status)
        {
          case DynamicModuleStatus.NotInstalled:
            this.ModuleStatusLabel.CssClass = "sfNotInstalled";
            this.ModuleSettingsPanel.Visible = false;
            break;
          case DynamicModuleStatus.Active:
            this.ModuleStatusLabel.CssClass = "sfActive";
            this.ModuleSettingsPanel.Visible = true;
            break;
          case DynamicModuleStatus.Inactive:
            this.ModuleStatusLabel.CssClass = "sfInactive";
            this.ModuleSettingsPanel.Visible = false;
            break;
        }
        this.BackToCurrentModuleLiteral.Text = Res.Get<ModuleBuilderResources>().BackTo + " " + HttpUtility.HtmlEncode(this.Module.Title);
        ((Control) this.ActivateModuleButton).Visible = this.Module.Status != DynamicModuleStatus.Active;
        ((Control) this.InactivateModuleButton).Visible = this.Module.Status == DynamicModuleStatus.Active;
        this.ModuleSettingsLiteral.Text = HttpUtility.HtmlEncode(this.Module.Title) + " " + Res.Get<ModuleBuilderResources>().Settings.ToLower();
        this.BackendScreenTweaksLink.NavigateUrl = this.GetBackendScreensTweaksLink();
        this.ModuleIdHidden.Value = this.ModuleId.ToString();
        this.CodeReferenceLink.Text = string.Format(Res.Get<ModuleBuilderResources>().CodeReferenceForContentType, (object) HttpUtility.HtmlEncode(this.Module.Title));
        this.CodeReferenceLink.NavigateUrl = this.GetCodeReferenceLink();
        this.CodeReferenceLink.Attributes.Add("title", HttpUtility.HtmlEncode(this.Module.Title));
        this.WebServiceUrlHidden.Value = ContentTypeDashboard.webServiceUrl;
        this.ContentTypesMasterView.ModuleId = this.ModuleId;
      }
      else
      {
        this.NoModuleMessagePanel.Visible = true;
        this.ModuleDashboardPanel.Visible = false;
      }
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private string GetCodeReferenceLink()
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(ModuleBuilderModule.contentTypeCodeReferencePageId, false);
      if (siteMapNode == null)
        return string.Empty;
      return RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.Module.Id + "/" + (object) this.ModuleType.Id;
    }

    private string GetBackendScreensTweaksLink()
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(ModuleBuilderModule.backendScreensTweaksPageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.Module.Id;
    }

    private static bool IsGuid(string candidate, out Guid output)
    {
      bool flag = false;
      output = Guid.Empty;
      if (candidate != null && ContentTypeDashboard.isGuid.IsMatch(candidate))
      {
        output = new Guid(candidate);
        flag = true;
      }
      return flag;
    }

    private bool InitializeURLDependantVariables()
    {
      if (ControlExtensions.GetUrlParameters(this.Page) == null || !ContentTypeDashboard.IsGuid(ControlExtensions.GetUrlParameters(this.Page)[0], out this.moduleId))
        return false;
      if (this.module == null)
      {
        this.Module = this.ModuleBuilderMngr.GetDynamicModules().Where<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Id == this.ModuleId)).FirstOrDefault<DynamicModule>();
        if (this.Module == null)
          return false;
      }
      return true;
    }
  }
}
