// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.TypeEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Hosting;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  /// <summary>
  /// This is the control which provides the functionality for defining the
  /// dynamic type of dynamic modules.
  /// </summary>
  public class TypeEditor : KendoView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.TypeEditor.ascx");

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
          base.LayoutTemplatePath = TypeEditor.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the hidden field which will hold the url
    /// of the taxonomies web service.
    /// </summary>
    protected virtual HiddenField TaxonomiesWebServiceUrl => this.Container.GetControl<HiddenField>("taxonomiesWebServiceUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field which will hold the base url
    /// </summary>
    protected virtual HiddenField SiteBaseUrl => this.Container.GetControl<HiddenField>("siteBaseUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field which will hold the title for the type editor
    /// when a new content type is created
    /// </summary>
    protected virtual HiddenField TypeEditorNewTypeTitle => this.Container.GetControl<HiddenField>("typeEditorNewTypeTitle", true);

    /// <summary>
    /// Gets the reference to the hidden field which will hold the default title suffix
    /// </summary>
    protected virtual HiddenField TypeEditorDefaultTitlePrefix => this.Container.GetControl<HiddenField>("typeEditorDefaultTitlePrefix", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.TaxonomiesWebServiceUrl.Value = VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash("~/Sitefinity/Services/Taxonomies/Taxonomy.svc"));
      this.SiteBaseUrl.Value = HostingEnvironment.ApplicationVirtualPath.TrimEnd('/') + "/";
      this.TypeEditorNewTypeTitle.Value = Res.Get<ModuleBuilderResources>().DefineAContentTypeOf;
      this.TypeEditorDefaultTitlePrefix.Value = Res.Get<ModuleBuilderResources>().TypeEditorDefaultTitlePrefix;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
