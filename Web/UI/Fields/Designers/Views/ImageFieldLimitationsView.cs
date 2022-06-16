// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Web.UI.Fields.Designers.Views
{
  public class ImageFieldLimitationsView : ContentViewDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Fields.ImageFieldLimitationsView.ascx");
    internal const string designerViewJs = "Telerik.Sitefinity.Web.UI.Fields.Designers.Scripts.ImageFieldLimitationsView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView" /> class.
    /// </summary>
    public ImageFieldLimitationsView() => this.LayoutTemplatePath = ImageFieldLimitationsView.layoutTemplatePath;

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => typeof (ImageFieldLimitationsView).Name;

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<FormsResources>().Limitations;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the reference to the min range field control.</summary>
    /// <value>The min range field.</value>
    public Control WidthTextField => this.Container.GetControl<Control>("widthTextField", true);

    /// <summary>Gets the reference to the max range field control.</summary>
    /// <value>The min range field.</value>
    public Control HeightTextField => this.Container.GetControl<Control>("heightTextField", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ImageFieldLimitationsView).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("widthTextField", this.WidthTextField.ClientID);
      controlDescriptor.AddComponentProperty("heightTextField", this.HeightTextField.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Designers.Scripts.ImageFieldLimitationsView.js", assembly)
      };
    }
  }
}
