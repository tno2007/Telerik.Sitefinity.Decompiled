// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Config;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields
{
  /// <summary>
  /// A field control for selecting a parent page or create a root library/folder
  /// </summary>
  [FieldDefinitionElement(typeof (ParentLibraryFieldDefinitionElement))]
  public class ParentLibraryField : FieldControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.Fields.ParentLibraryField.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Scripts.ParentLibraryField.js";
    private const string iRequiresProviderScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js";
    private bool bindOnLoad = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.ParentLibraryField" /> class.
    /// </summary>
    public ParentLibraryField() => this.LayoutTemplatePath = ParentLibraryField.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider from which the folders to be selected.
    /// </summary>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets or sets the NoParentLib title.</summary>
    public string NoParentLibTitle { get; set; }

    /// <summary>Gets or sets the SelectedParentLib title.</summary>
    public string SelectedParentLibTitle { get; set; }

    /// <summary>
    /// Gets the localizable string that represents the name of the item in singular.
    /// </summary>
    protected virtual string ItemName { get; set; }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", true);

    /// <summary>
    /// Gets the reference to the label control that displays the example for this
    /// field control.
    /// </summary>
    /// <remarks>This control is mandatory only in the write mode.</remarks>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", true);

    /// <summary>
    /// Gets the reference to the radio button control
    /// property is set to <see cref="!:RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual RadioButton NoParent => this.Container.GetControl<RadioButton>("noParent", true);

    /// <summary>
    /// Gets the reference to the radio button control
    /// property is set to <see cref="!:RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual RadioButton SelectParent => this.Container.GetControl<RadioButton>("selectParent", true);

    /// <summary>
    /// Gets the reference to the Folder field control
    /// property is set to <see cref="!:RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual FolderField ParentLibrarySelector => this.Container.GetControl<FolderField>("parentLibrarySelector", true);

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SetRadioButtons();
      this.Title = Res.Get<LibrariesResources>().ParentLibrary;
      this.TitleLabel.Text = this.Title;
      this.ExampleLabel.Text = this.Example;
      this.DescriptionLabel.Text = this.Description;
      this.ParentLibrarySelector.WebServiceUrl = this.WebServiceUrl;
      this.ParentLibrarySelector.BindOnLoad = this.BindOnLoad;
      this.ParentLibrarySelector.ProviderName = this.ProviderName;
      this.ParentLibrarySelector.ItemName = this.ItemName;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("parentLibrarySelector", this.ParentLibrarySelector.ClientID);
      controlDescriptor.AddElementProperty("noParent", this.NoParent.ClientID);
      controlDescriptor.AddElementProperty("selectParent", this.SelectParent.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Scripts.ParentLibraryField.js", typeof (ParentLibraryField).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js", typeof (ParentLibraryField).Assembly.FullName)
    };

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IParentLibraryFieldDefinition libraryFieldDefinition))
        return;
      this.WebServiceUrl = libraryFieldDefinition.WebServiceUrl;
      this.ProviderName = libraryFieldDefinition.ProviderName;
      bool? bindOnLoad = libraryFieldDefinition.BindOnLoad;
      if (bindOnLoad.HasValue)
      {
        bindOnLoad = libraryFieldDefinition.BindOnLoad;
        this.BindOnLoad = bindOnLoad.Value;
      }
      this.NoParentLibTitle = Res.Get(libraryFieldDefinition.ResourceClassId, libraryFieldDefinition.NoParentLibTitle);
      this.SelectedParentLibTitle = Res.Get(libraryFieldDefinition.ResourceClassId, libraryFieldDefinition.SelectedParentLibTitle);
      this.ItemName = Res.Get(libraryFieldDefinition.ResourceClassId, libraryFieldDefinition.LibraryItemName);
    }

    private void SetRadioButtons()
    {
      this.NoParent.Text = this.NoParentLibTitle;
      this.SelectParent.Text = this.SelectedParentLibTitle;
    }
  }
}
