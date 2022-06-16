// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class SelectorFieldBase : FieldControl
  {
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.SelectorFieldBase.js";
    private const string LayoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Fields.SelectorFieldBase.ascx";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase" /> class.
    /// </summary>
    public SelectorFieldBase() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.SelectorFieldBase.ascx");

    /// <summary>
    /// Gets the reference to the radio button control
    /// property is set to <see cref="!:RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual RadioButton AllValues => this.Container.GetControl<RadioButton>("allValues", true);

    /// <summary>
    /// Gets the reference to the radio button control
    /// property is set to <see cref="!:RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual RadioButton SelectedValues => this.Container.GetControl<RadioButton>("selectedValues", true);

    /// <summary>
    /// Gets a references to the <see cref="T:System.Web.UI.WebControls.Label" /> with ID selectedValuesLabel.
    /// </summary>
    protected internal virtual Label SelectedValuesLabel => this.Container.GetControl<Label>("selectedValuesLabel", true);

    /// <summary>
    /// Gets a references to the <see cref="T:System.Web.UI.WebControls.Label" /> with ID allValuesLabel.
    /// </summary>
    protected internal virtual Label AllValuesLabel => this.Container.GetControl<Label>("allValuesLabel", true);

    /// <summary>
    /// Gets a references to the <see cref="T:System.Web.UI.WebControls.Label" /> with ID titleLabel.
    /// </summary>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets a references to the <see cref="T:System.Web.UI.WebControls.Label" /> with ID languageNames.
    /// </summary>
    protected internal virtual Label SelectedValuesContainer => this.Container.GetControl<Label>("selectedValuesContainer", true);

    /// <summary>
    /// Gets a references to the <see cref="T:System.Web.UI.WebControls.LinkButton" /> with ID openSelectorButton.
    /// </summary>
    protected internal virtual LinkButton OpenSelectorButton => this.Container.GetControl<LinkButton>("openSelectorButton", true);

    /// <summary>
    /// Gets a references to the <see cref="T:System.Web.UI.WebControls.Label" /> with ID selectorButtonText.
    /// </summary>
    protected internal virtual Label SelectorButtonText => this.Container.GetControl<Label>("selectorButtonText", true);

    /// <summary>
    /// Gets a references to the <see cref="T:Telerik.Web.UI.RadWindow" /> with ID selectorDialog.
    /// </summary>
    protected internal virtual RadWindow SelectorDialog => this.Container.GetControl<RadWindow>("selectorDialog", true);

    /// <summary>
    /// Gets a references to the <see cref="P:Telerik.Sitefinity.Web.UI.Fields.SelectorFieldBase.ClientLabelManager" /> with ID clientLabelManager.
    /// </summary>
    protected internal virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    protected override void InitializeControls(GenericContainer container)
    {
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("selectedValues", this.SelectedValues.ClientID);
      controlDescriptor.AddElementProperty("allValues", this.AllValues.ClientID);
      controlDescriptor.AddElementProperty("selectedValuesContainer", this.SelectedValuesContainer.ClientID);
      controlDescriptor.AddElementProperty("openSelectorButton", this.OpenSelectorButton.ClientID);
      controlDescriptor.AddElementProperty("selectorButtonText", this.SelectorButtonText.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("selectorDialog", this.SelectorDialog.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.SelectorFieldBase.js", typeof (SelectorFieldBase).Assembly.FullName)
    };
  }
}
