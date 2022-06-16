// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.ModuleBuilderDashboard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  /// <summary>Dashboard of the module builder module.</summary>
  public class ModuleBuilderDashboard : KendoView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.ModuleBuilderDashboard.ascx");
    private static readonly string webServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/DynamicModules/ContentTypeService.svc/");
    private static readonly string dataSourceServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/DataSourceService/providers/?sortExpression=Title&siteId=00000000-0000-0000-0000-000000000000&dataSourceName=");
    private static readonly string exportDataUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/export-contenttype-items/");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = ModuleBuilderDashboard.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the hidden field which holds the content types web service url.
    /// </summary>
    protected virtual HiddenField WebServiceUrlHidden => this.Container.GetControl<HiddenField>("webServiceUrlHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the data source web service url.
    /// </summary>
    protected virtual HiddenField DataSourceServiceUrlHidden => this.Container.GetControl<HiddenField>("dataSourceServiceUrlHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the base title of the type editor
    /// </summary>
    protected virtual HiddenField TypeEditorTitleLabel => this.Container.GetControl<HiddenField>("typeEditorTitleLabel", true);

    /// <summary>
    ///  Gets the reference to the hidden field which holds the export of content type items url.
    /// </summary>
    protected virtual HiddenField ExportDataUrlHidden => this.Container.GetControl<HiddenField>("exportDataUrlHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the base title of the delete module window.
    /// </summary>
    protected virtual HiddenField DeleteModuleTitle => this.Container.GetControl<HiddenField>("deleteModuleTitleHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the base URL of the ModuleBuilder module in the backend.
    /// </summary>
    /// <value>The module base URL hidden.</value>
    protected virtual HiddenField ModuleBaseUrlHidden => this.Container.GetControl<HiddenField>("moduleBaseUrlHidden", true);

    /// <summary>Gets the multisite FAQ placeholder.</summary>
    /// <value>The multisite FAQ placeholder.</value>
    protected virtual HtmlControl MultisiteFaq => this.Container.GetControl<HtmlControl>("multisiteFaq", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the value indicating whether source selector dialog will be shown.
    /// </summary>
    /// <value>The show source selector dialog hidden.</value>
    protected virtual HiddenField ShowSourceSelectorDialogHidden => this.Container.GetControl<HiddenField>("showSourceSelectorDialogHidden", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.WebServiceUrlHidden.Value = ModuleBuilderDashboard.webServiceUrl;
      this.DataSourceServiceUrlHidden.Value = ModuleBuilderDashboard.dataSourceServiceUrl;
      this.ExportDataUrlHidden.Value = ModuleBuilderDashboard.exportDataUrl;
      this.ModuleBaseUrlHidden.Value = this.GetModuleBaseUrl();
      ModuleBuilderResources builderResources = Res.Get<ModuleBuilderResources>();
      this.TypeEditorTitleLabel.Value = builderResources.DefineAContentTypeOf;
      this.DeleteModuleTitle.Value = builderResources.DeleteModuleNotification;
      this.MultisiteFaq.Visible = true;
      this.ShowSourceSelectorDialogHidden.Value = true.ToString();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// This method is tested. If you make changes to the url, please update the tests as well
    /// </summary>
    internal string GetModuleBaseUrl() => RouteHelper.ResolveUrl(BackendSiteMap.FindSiteMapNode(ModuleBuilderModule.moduleBuilderNodeId, false).Url, UrlResolveOptions.Rooted);
  }
}
