// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// A field control used for displaying a selector for specified item type, allows changing of the selected item
  /// and implements IParentSelectorField JavaScript interface.
  /// </summary>
  [FieldDefinitionElement(typeof (ParentSelectorFieldElement))]
  public class ParentSelectorField : FieldControl
  {
    private FlatSelector itemSelector;
    private string listModeClientTemplate;
    private bool allowMultipleSelection;
    private readonly string defaultListModeClientTemplate = "<strong class='sfItemTitle'>{{{{{0}}}}}</strong><span class='sfDate'>{{{{PublicationDate ? PublicationDate.sitefinityLocaleFormat('dd MMM yyyy') : &quot;&quot;}}}} by {{{{Author}}}}</span>";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ParentSelectorField.ascx");
    internal const string parentSelectorFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ParentSelectorField.js";
    private const string iParentSelectorFieldScript = "Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js";
    private const string iRequiresProviderScript = "Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ParentSelectorField" /> class.
    /// </summary>
    public ParentSelectorField() => this.LayoutTemplatePath = ParentSelectorField.layoutTemplatePath;

    /// <summary>Gets or sets the original, selected value.</summary>
    /// <value>The original, selected value.</value>
    public string InitialValue { get; set; }

    /// <summary>Gets or sets the original text.</summary>
    /// <value>The original text.</value>
    public string InitialText { get; set; }

    /// <summary>Gets or sets the no content selected text.</summary>
    /// <value>The no content selected text.</value>
    public string NoContentSelectedText { get; set; }

    /// <summary>Gets or sets the select content button text</summary>
    public string SelectContentButtonText { get; set; }

    /// <summary>
    /// Gets or sets the web service URL for the content selector control.
    /// </summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the type of the items that the selector displays.
    /// </summary>
    /// <value>The type of the items.</value>
    public string ItemsType { get; set; }

    /// <summary>
    /// Gets or sets the name of the field displayed in the selector.
    /// </summary>
    /// <value>The name of the main field.</value>
    public string MainFieldName { get; set; }

    /// <summary>Gets or sets the title of the dialog.</summary>
    /// <value>The title of the dialog.</value>
    public string DialogTitleLabelText { get; set; }

    /// <summary>
    /// If work mode is set to List this template is used to render it
    /// </summary>
    /// <value>The client template for list mode.</value>
    public string ListModeClientTemplate
    {
      get => this.listModeClientTemplate.IsNullOrEmpty() ? this.defaultListModeClientTemplate : this.listModeClientTemplate;
      set => this.listModeClientTemplate = value;
    }

    /// <summary>
    /// Gets or sets the data key names used by the selector data source
    /// </summary>
    public string DataKeyNames { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be searched
    /// </summary>
    public virtual bool AllowSearching { get; set; }

    /// <inheritdoc />
    protected override string ScriptDescriptorType => typeof (ParentSelectorField).FullName;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets whether multiple selection is available</summary>
    public bool AllowMultipleSelection
    {
      get => this.allowMultipleSelection;
      set => this.allowMultipleSelection = value;
    }

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", false);

    /// <inheritdoc />
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", false);

    /// <inheritdoc />
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the label control that displays the example for this
    /// field control.
    /// </summary>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", true);

    /// <inheritdoc />
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>Gets the select single item button.</summary>
    /// <value>The select content button.</value>
    protected LinkButton SelectContentButton => this.Container.GetControl<LinkButton>("btnSelectSingleItem", true);

    protected Literal SelectContentButtonTextLiteral => this.Container.GetControl<Literal>("btnSelectSingleItemLiteral", true);

    /// <summary>
    /// Gets the jquery UI dialog which shows "Choose item" dialog
    /// </summary>
    public HtmlGenericControl SelectorTag => this.Container.GetControl<HtmlGenericControl>("selectorTag", true);

    /// <summary>Gets the selected content label.</summary>
    /// <value>The selected content label.</value>
    protected Label SelectedContentLabel => this.Container.GetControl<Label>("selectedContentTitle", true);

    /// <summary>Gets the title label of the dialog.</summary>
    protected Label DialogTitleLabel => (Label) this.Container.GetControl<SitefinityLabel>("dialogTitleLabel", true);

    /// <summary>Gets the done selecting button.</summary>
    protected LinkButton DoneSelectingButton => this.Container.GetControl<LinkButton>("doneSelectingButton", true);

    /// <summary>Gets the cancel selecting button.</summary>
    protected LinkButton CancelSelectingButton => this.Container.GetControl<LinkButton>("cancelSelectingButton", true);

    /// <summary>Gets the content selector placeholder.</summary>
    protected PlaceHolder ContentSelectorPlaceholder => this.Container.GetControl<PlaceHolder>("contentSelectorPlaceholder", true);

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.DisplayMode == FieldDisplayMode.Read)
      {
        this.SelectContentButton.Visible = false;
      }
      else
      {
        this.itemSelector = new FlatSelector();
        this.itemSelector.ServiceUrl = this.WebServiceUrl;
        this.itemSelector.ItemType = this.ItemsType;
        this.itemSelector.DataKeyNames = this.DataKeyNames;
        this.itemSelector.AllowSearching = this.AllowSearching;
        this.itemSelector.ShowSelectedFilter = false;
        this.itemSelector.InclueAllProvidersOption = false;
        this.itemSelector.AllowMultipleSelection = this.AllowMultipleSelection;
        this.itemSelector.ProviderName = this.ProviderName;
        this.itemSelector.InclueAllProvidersOption = false;
        string mainFieldName = this.MainFieldName;
        if (DynamicTypesHelper.IsFieldLocalizable(this.ItemsType, this.MainFieldName))
          mainFieldName += ".PersistedValue";
        this.itemSelector.DataMembers.Add(new DataMemberInfo()
        {
          Name = this.MainFieldName,
          HeaderText = this.MainFieldName,
          IsExtendedSearchField = true,
          ColumnTemplate = string.Format(this.ListModeClientTemplate, (object) mainFieldName)
        });
        this.ContentSelectorPlaceholder.Controls.Add((Control) this.itemSelector);
      }
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      this.ExampleLabel.Text = this.Example;
      this.DialogTitleLabel.Text = this.DialogTitleLabelText != null ? this.DialogTitleLabelText : string.Format(Res.Get<ModuleBuilderResources>().SelectItems, (object) this.Title);
    }

    /// <inheritdoc />
    public override void Configure(IFieldDefinition definition)
    {
      if (definition is IParentSelectorFieldDefinition selectorFieldDefinition)
      {
        this.WebServiceUrl = selectorFieldDefinition.WebServiceUrl;
        this.ItemsType = selectorFieldDefinition.ItemsType;
        this.MainFieldName = selectorFieldDefinition.MainFieldName;
        this.DataKeyNames = selectorFieldDefinition.DataKeyNames;
        this.AllowSearching = selectorFieldDefinition.AllowSearching;
      }
      base.Configure(definition);
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
      controlDescriptor.AddProperty("initialValue", (object) this.InitialValue);
      controlDescriptor.AddProperty("initialText", (object) this.InitialText);
      controlDescriptor.AddProperty("noContentSelectedText", (object) this.NoContentSelectedText);
      controlDescriptor.AddProperty("buttonChangeText", (object) Res.Get<Labels>().Change);
      controlDescriptor.AddProperty("buttonSelectText", (object) (this.SelectContentButtonText ?? Res.Get<Labels>().Select));
      controlDescriptor.AddProperty("itemType", (object) this.ItemsType);
      controlDescriptor.AddProperty("providerName", (object) this.ProviderName);
      controlDescriptor.AddProperty("allowMultipleSelection", (object) this.AllowMultipleSelection);
      if (this.DisplayMode != FieldDisplayMode.Read)
      {
        controlDescriptor.AddComponentProperty("itemSelector", this.itemSelector.ClientID);
        controlDescriptor.AddElementProperty("selectContentButton", this.SelectContentButton.ClientID);
      }
      controlDescriptor.AddElementProperty("selectorTag", this.SelectorTag.ClientID);
      controlDescriptor.AddElementProperty("selectedContentTitle", this.SelectedContentLabel.ClientID);
      controlDescriptor.AddElementProperty("doneSelectingButton", this.DoneSelectingButton.ClientID);
      controlDescriptor.AddElementProperty("cancelSelectingButton", this.CancelSelectingButton.ClientID);
      if (!this.MainFieldName.IsNullOrWhitespace())
        controlDescriptor.AddProperty("mainFieldName", (object) this.MainFieldName);
      return (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>()
      {
        (ScriptDescriptor) controlDescriptor
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (ParentSelectorField).Assembly.FullName;
      scriptReferences.AddRange((IEnumerable<ScriptReference>) PageManager.GetScriptReferences(ScriptRef.JQueryUI));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.IParentSelectorField.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ParentSelectorField.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <inheritdoc />
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.JQueryUI;
  }
}
