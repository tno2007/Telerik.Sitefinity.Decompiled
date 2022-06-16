// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Field control that is used to display the wofkflow status for pages.
  /// </summary>
  [RequiresDataItem]
  public class PageWorkflowStatusInfoField : ContentWorkflowStatusInfoField
  {
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.PageWorkflowStatusInfoField.ascx");
    internal new const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.PageWorkflowStatusInfoField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.PageWorkflowStatusInfoField" /> class.
    /// </summary>
    public PageWorkflowStatusInfoField() => this.LayoutTemplatePath = PageWorkflowStatusInfoField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used.
    /// </summary>
    protected override string ScriptDescriptorType => typeof (PageWorkflowStatusInfoField).FullName;

    /// <summary>
    /// Gets the reference to the control that represents the workflow status date.
    /// </summary>
    public new Label DateLabel => this.Container.GetControl<Label>("dateLabel", true);

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.ConfigureControl((IContentWorkflowStatusInfoFieldDefinition) definition);
    }

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="statusFieldDefinition">The status field definition.</param>
    internal new virtual void ConfigureControl(
      IContentWorkflowStatusInfoFieldDefinition statusFieldDefinition)
    {
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.Style.Add("display", "none");

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (PageWorkflowStatusInfoField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.PageWorkflowStatusInfoField.js", fullName)
      };
    }

    internal new virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => this.GetScriptDescriptors();

    internal new virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);
  }
}
