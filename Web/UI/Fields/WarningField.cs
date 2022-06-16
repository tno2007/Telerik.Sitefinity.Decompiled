// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.WarningField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Control that provides functionality for displaying warnings.
  /// </summary>
  [RequiresDataItem]
  public class WarningField : FieldControl, ICommandField
  {
    private static readonly string LayoutTemplatePathConst = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.WarningField.ascx");
    internal const string WarningFieldScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.WarningField.js";
    private const string RequiresDataItemContextInterfaceScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js";
    private const string CommandFieldInterfaceScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js";
    private const string ILocalizableFieldControlScriptName = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js";
    private const string IRequiresProviderScriptName = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.WarningField" /> class.
    /// </summary>
    public WarningField() => this.LayoutTemplatePath = WarningField.LayoutTemplatePathConst;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used.
    /// </summary>
    protected override string ScriptDescriptorType => typeof (WarningField).FullName;

    /// <summary>Gets the wrapper for the field control.</summary>
    protected HtmlControl FieldWrapper => this.Container.GetControl<HtmlControl>("fieldWrapper", true);

    /// <summary>Gets the client binder control.</summary>
    protected virtual ClientBinder ItemsBinder => this.Container.GetControl<ClientBinder>("itemsBinder", false);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("fieldWrapper", this.FieldWrapper.ClientID);
      controlDescriptor.AddComponentProperty("itemsBinder", this.ItemsBinder.ClientID);
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
      string fullName = typeof (WarningField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRequiresDataItemContext.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ICommandField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ILocalizableFieldControl.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.WarningField.js", fullName)
      };
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      this.ConfigureControl((IWarningFieldDefinition) definition);
    }

    /// <summary>Configures the fields with the definition.</summary>
    /// <param name="statusFieldDefinition">The status field definition.</param>
    internal virtual void ConfigureControl(IWarningFieldDefinition statusFieldDefinition)
    {
    }

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
