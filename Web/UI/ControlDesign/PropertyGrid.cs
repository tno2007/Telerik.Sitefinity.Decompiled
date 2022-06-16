// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PropertyGrid
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control which renders the property grid which lets users change the properties of a control
  /// through the user interface.
  /// </summary>
  public class PropertyGrid : SimpleView, IScriptControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.PropertyGrid.ascx");
    private const string propertyGridScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.PropertyGrid.js";
    private const string iControlDesignerScript = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IControlDesigner.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PropertyGrid" /> class.
    /// </summary>
    public PropertyGrid() => this.LayoutTemplatePath = PropertyGrid.layoutTemplatePath;

    /// <summary>
    /// Gets or sets the reference to the parent property editor contro
    /// </summary>
    public PropertyEditor PropertyEditor { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the reference to the breadcrumb control</summary>
    protected virtual RadBreadCrumb Breadcrumb => this.Container.GetControl<RadBreadCrumb>("breadcrumb", true);

    /// <summary>Gets the reference to the property grid binder</summary>
    protected virtual ClientBinder PropertyGridBinder => this.Container.GetControl<ClientBinder>("propertyGridBinder", true);

    /// <summary>
    /// Gets the reference to the toolbar that switches the views of the property grid
    /// </summary>
    protected virtual RadToolBar PropertyViewsToolbar => this.Container.GetControl<RadToolBar>("propertyViewsToolbar", true);

    /// <summary>
    /// Gets the reference to the button that expands all categories of properties
    /// </summary>
    protected virtual LinkButton ExpandAllButton => this.Container.GetControl<LinkButton>("expandAllButton", true);

    /// <summary>
    /// Gets the reference to the button that collapses all categories of properties
    /// </summary>
    protected virtual LinkButton CollapseAllButton => this.Container.GetControl<LinkButton>("collapseAllButton", true);

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the
    /// specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents
    /// the output stream to render HTML content on the client.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxTemplates | ScriptRef.MicrosoftAjaxWebForms | ScriptRef.JQuery | ScriptRef.JQueryValidate).RegisterScriptControl<PropertyGrid>(this);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (PropertyGrid).FullName, this.ClientID);
      controlDescriptor.AddProperty("_breadcrumbId", (object) this.Breadcrumb.ClientID);
      controlDescriptor.AddProperty("_expandAllButtonId", (object) this.ExpandAllButton.ClientID);
      controlDescriptor.AddProperty("_collapseAllButtonId", (object) this.CollapseAllButton.ClientID);
      controlDescriptor.AddComponentProperty("propertyBinder", this.PropertyGridBinder.ClientID);
      controlDescriptor.AddComponentProperty("propertyEditor", this.PropertyEditor.ClientID);
      controlDescriptor.AddComponentProperty("propertyViewsToolbar", this.PropertyViewsToolbar.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[2]
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IControlDesigner.js", typeof (PropertyGrid).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.PropertyGrid.js", typeof (PropertyGrid).Assembly.FullName)
    };
  }
}
