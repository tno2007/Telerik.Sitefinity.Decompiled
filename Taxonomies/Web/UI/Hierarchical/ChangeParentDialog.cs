// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.ChangeParentDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical
{
  /// <summary>
  /// Dialog which provides user interface for changing the parent of the
  /// hierarchical taxons
  /// </summary>
  public class ChangeParentDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.Hierarchical.ChangeParentDialog.ascx");
    private const string changeParentServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/changeparent/";
    private const string batchChangeParentServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/batchchangeparent/";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ChangeParentDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets a reference to changeParentServiceUrl control.</summary>
    protected virtual HiddenField ChangeParentServiceUrl => this.Container.GetControl<HiddenField>("changeParentServiceUrl", true);

    /// <summary>
    /// Gets a reference to batchChangeParentServiceUrl control.
    /// </summary>
    protected virtual HiddenField BatchChangeParentServiceUrl => this.Container.GetControl<HiddenField>("batchChangeParentServiceUrl", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ChangeParentServiceUrl.Value = VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/changeparent/"));
      this.BatchChangeParentServiceUrl.Value = VirtualPathUtility.ToAbsolute(VirtualPathUtility.AppendTrailingSlash("~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/batchchangeparent/"));
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
