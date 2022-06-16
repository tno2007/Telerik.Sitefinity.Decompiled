// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// This field control is used for specifying field mapping between Sitefinity and external connectors.
  /// </summary>
  [FieldDefinitionElement(typeof (GenericFieldDefinitionElement))]
  public class ConnectorDataMappingField : FieldControl
  {
    private const string ControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ConnectorDataMappingField.js";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    private static readonly string LayoutTemplate = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ConnectorDataMappingField.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ConnectorDataMappingField" /> class.
    /// </summary>
    public ConnectorDataMappingField() => this.LayoutTemplatePath = ConnectorDataMappingField.LayoutTemplate;

    /// <summary>
    /// Gets the reference to the data mapping selector control.
    /// </summary>
    internal ConnectorDataMappingSelector Selector => this.Container.GetControl<ConnectorDataMappingSelector>("selector", true);

    /// <summary>Gets the command bar.</summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true, TraverseMethod.DepthFirst);

    /// <summary>Gets the button that opens the selector.</summary>
    /// <value>The button.</value>
    protected internal virtual LinkButton OpenSelectorLink => this.Container.GetControl<LinkButton>("openSelector", true);

    /// <summary>Gets the selector window container.</summary>
    /// <value>The selector window.</value>
    protected HtmlGenericControl SelectorWindow => this.Container.GetControl<HtmlGenericControl>("selectorWindow", true);

    /// <summary>
    /// Gets the html element that will display information about the number of mapped fields.
    /// </summary>
    /// <value>The selector window.</value>
    protected HtmlGenericControl MappedFieldsInfoElement => this.Container.GetControl<HtmlGenericControl>("mappedFieldsInfo", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().LastOrDefault<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("openSelectorLink", this.OpenSelectorLink.ClientID);
      controlDescriptor.AddElementProperty("mappedFieldsInfo", this.MappedFieldsInfoElement.ClientID);
      controlDescriptor.AddElementProperty("selectorWindow", this.SelectorWindow.ClientID);
      controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      controlDescriptor.AddComponentProperty("selector", this.Selector.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>()
      {
        {
          "FieldMapped",
          Res.Get<FormsResources>().FieldMapped
        },
        {
          "FieldsMapped",
          Res.Get<FormsResources>().FieldsMapped
        },
        {
          "CreateMapping",
          Res.Get<FormsResources>().CreateDataMapping
        },
        {
          "EditMapping",
          Res.Get<FormsResources>().EditDataMapping
        }
      };
      controlDescriptor.AddProperty("resources", (object) dictionary);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference()
      {
        Assembly = this.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ConnectorDataMappingField.js"
      },
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
    }.ToArray();
  }
}
