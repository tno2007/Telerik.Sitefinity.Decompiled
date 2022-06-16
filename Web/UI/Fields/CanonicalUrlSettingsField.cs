// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// Represents a canonical url settings field control. Used for editing canonical url settings.
  /// </summary>
  public class CanonicalUrlSettingsField : CompositeFieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.CanonicalUrlSettingsField.ascx");
    internal const string fieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.CanonicalUrlSettingsField.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.CanonicalUrlSettingsField" /> class.
    /// </summary>
    public CanonicalUrlSettingsField() => this.LayoutTemplatePath = CanonicalUrlSettingsField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    protected internal override WebControl DescriptionControl => (WebControl) null;

    protected internal override WebControl ExampleControl => (WebControl) null;

    /// <summary>
    /// Gets or sets the canonical url settings choice field definition.
    /// </summary>
    /// <value>The canonical url settings choice field definition.</value>
    public virtual IChoiceFieldDefinition CanonicalUrlSettingsChoiceFieldDefinition { get; set; }

    /// <summary>
    /// Gets a reference to the choice field control that lists the available canonical url settings.
    /// </summary>
    protected internal virtual ChoiceField CanonicalUrlSettingsSelect => this.Container.GetControl<ChoiceField>("canonicalUrlSettingsSelect", true);

    /// <summary>
    /// Gets a reference to the button that toggle the area with details.
    /// </summary>
    protected internal virtual Control DetailsButton => this.Container.GetControl<Control>("detailsBtn", true);

    /// <summary>
    /// Gets a reference to the control that contains the canonical url settings details.
    /// </summary>
    protected internal virtual Control DetailsContainer => this.Container.GetControl<Control>("detailsCnt", true);

    /// <summary>
    /// Gets a reference to the control that displays the cache settings location.
    /// </summary>
    protected internal virtual ITextControl SettingsLocationControl => this.Container.GetControl<ITextControl>("settingsLocation", true);

    protected internal override WebControl TitleControl => (WebControl) this.Container.GetControl<Label>("titleLabel", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.CanonicalUrlSettingsChoiceFieldDefinition.Choices.Add((IChoiceDefinition) new ChoiceElement()
      {
        Text = Res.Get<PageResources>("AsForWholeSite"),
        Value = (byte) 0.ToString()
      });
      this.CanonicalUrlSettingsChoiceFieldDefinition.Choices.Add((IChoiceDefinition) new ChoiceElement()
      {
        Text = Res.Get<PageResources>("CanonicalUrlDisabled"),
        Value = (byte) 1.ToString()
      });
      this.CanonicalUrlSettingsChoiceFieldDefinition.Choices.Add((IChoiceDefinition) new ChoiceElement()
      {
        Text = Res.Get<PageResources>("CanonicalUrlEnabled"),
        Value = (byte) 2.ToString()
      });
      this.CanonicalUrlSettingsSelect.Configure((IFieldDefinition) this.CanonicalUrlSettingsChoiceFieldDefinition);
      ((ITextControl) this.TitleControl).SetTextOrHide(this.Title);
      this.SettingsLocationControl.Text = Res.Get<Labels>().CanonicalUrlsSettingsLocation;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is ICanonicalUrlSettingsFieldDefinition settingsFieldDefinition))
        return;
      this.CanonicalUrlSettingsChoiceFieldDefinition = (IChoiceFieldDefinition) settingsFieldDefinition.CanonicalUrlSettingsChoiceFieldDefinition.GetDefinition();
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        controlDescriptor.AddComponentProperty("canonicalUrlSettingsSelect", this.CanonicalUrlSettingsSelect.ClientID);
        controlDescriptor.AddElementProperty("detailsButton", this.DetailsButton.ClientID);
        controlDescriptor.AddElementProperty("detailsContainer", this.DetailsContainer.ClientID);
      }
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
      string fullName = typeof (CanonicalUrlSettingsField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.CanonicalUrlSettingsField.js", fullName)
      };
    }

    public override string JavaScriptComponentName => typeof (CanonicalUrlSettingsField).FullName;
  }
}
