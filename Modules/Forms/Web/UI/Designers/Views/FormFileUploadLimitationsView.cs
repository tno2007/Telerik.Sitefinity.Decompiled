// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadLimitationsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views
{
  internal class FormFileUploadLimitationsView : ContentViewDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Forms.FormFileUploadLimitationsView.ascx");
    internal const string designerViewJs = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormFileUploadLimitationsView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => this.GetType().Name;

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<FormsResources>().Limitations;

    /// <summary>Gets the name of the embedded layout template.</summary>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormFileUploadLimitationsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>Gets the min range field.</summary>
    protected virtual TextField MinRangeField => this.Container.GetControl<TextField>("minRangeField", true);

    /// <summary>Gets the max range field.</summary>
    protected virtual TextField MaxRangeField => this.Container.GetControl<TextField>("maxRangeField", true);

    /// <summary>Gets the allowed file types choice field.</summary>
    protected virtual ChoiceField AllowedFileTypesChoiceField => this.Container.GetControl<ChoiceField>("allowedFileTypesChoiceField", true);

    /// <summary>Gets the selected file types choice field.</summary>
    protected virtual ChoiceField SelectedFileTypesChoiceField => this.Container.GetControl<ChoiceField>("selectedFileTypesChoiceField", true);

    /// <summary>Gets the other extensions text field.</summary>
    protected virtual TextField OtherExtensionsTextField => this.Container.GetControl<TextField>("otherExtensionsTextField", true);

    /// <summary>Gets the range violation message field.</summary>
    protected virtual TextField RangeViolationMessageField => this.Container.GetControl<TextField>("rangeViolationMessageField", true);

    /// <summary>Gets the multiple attachments choice field.</summary>
    protected virtual ChoiceField MultipleAttachmentsChoiceField => this.Container.GetControl<ChoiceField>("multipleAttachmentsChoiceField", true);

    /// <summary>Gets the selected file types div.</summary>
    protected virtual HtmlGenericControl SelectedFileTypesDiv => this.Container.GetControl<HtmlGenericControl>("selectedFileTypesDiv", true);

    /// <summary>Gets the other extensions div.</summary>
    protected virtual HtmlGenericControl OtherExtensionsDiv => this.Container.GetControl<HtmlGenericControl>("otherExtensionsDiv", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (FormFileUploadLimitationsView).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("minRangeField", this.MinRangeField.ClientID);
      controlDescriptor.AddComponentProperty("maxRangeField", this.MaxRangeField.ClientID);
      controlDescriptor.AddComponentProperty("allowedFileTypesChoiceField", this.AllowedFileTypesChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("selectedFileTypesChoiceField", this.SelectedFileTypesChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("otherExtensionsTextField", this.OtherExtensionsTextField.ClientID);
      controlDescriptor.AddComponentProperty("rangeViolationMessageField", this.RangeViolationMessageField.ClientID);
      controlDescriptor.AddComponentProperty("multipleAttachmentsChoiceField", this.MultipleAttachmentsChoiceField.ClientID);
      controlDescriptor.AddElementProperty("selectedFileTypesDiv", this.SelectedFileTypesDiv.ClientID);
      controlDescriptor.AddElementProperty("otherExtensionsDiv", this.OtherExtensionsDiv.ClientID);
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
      string assembly = typeof (FormFileUploadLimitationsView).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormFileUploadLimitationsView.js", assembly)
      };
    }
  }
}
