// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.TextField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Extenders;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// This control is used for inserting/editing/viewing simple text.
  /// </summary>
  [FieldDefinitionElement(typeof (TextFieldDefinitionElement))]
  public class TextField : FieldControl, IExpandableControl
  {
    private int autocompleteSuggestionsCount = 5;
    private string clientTemplateSuffix;
    private bool? expanded = new bool?(true);
    private ExpandableControlDefinition expandableControlDefinition;
    private object value;
    private Dictionary<string, object> conditionDictionary;
    private const string textBoxId = "textBox_write";
    private const string textLabelId = "textLabel_read";
    private const string textFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.TextField.js";
    private const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";
    private const string jqueryUiScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.TextField" /> class.
    /// </summary>
    public TextField() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.TextField.ascx");

    /// <summary>
    /// Gets or sets the number of rows displayed in a multiline text box.
    ///  </summary>
    /// <value>The rows.</value>
    public int Rows { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the text field is used as password field.
    /// </summary>
    public bool IsPasswordMode { get; set; }

    /// <summary>
    /// Gets or sets the value which compared with the actual value of the Text Field, if equal hides the text.
    /// </summary>
    public string HideIfValue { get; set; }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    [TypeConverter(typeof (ObjectStringConverter))]
    public override object Value
    {
      get
      {
        string str = string.Empty;
        switch (this.DisplayMode)
        {
          case FieldDisplayMode.Read:
            str = this.LabelControl.Text;
            break;
          case FieldDisplayMode.Write:
            str = this.TextBoxControl.Text;
            break;
        }
        return (object) str;
      }
      set
      {
        if (this.ChildControlsCreated)
        {
          string empty = string.Empty;
          if (value != null)
            empty = value.ToString();
          switch (this.DisplayMode)
          {
            case FieldDisplayMode.Read:
              this.LabelControl.Text = empty;
              break;
            case FieldDisplayMode.Write:
              this.TextBoxControl.Text = empty;
              break;
          }
          this.value = (object) null;
        }
        else if (value != null)
          this.value = (object) value.ToString();
        else
          this.value = value;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the expandable control definition.</summary>
    /// <value>The expandable control definition.</value>
    public ExpandableControlDefinition ExpandableControlDefinition
    {
      get
      {
        if (this.expandableControlDefinition == null)
          this.expandableControlDefinition = new ExpandableControlDefinition()
          {
            Expanded = this.Expanded,
            ExpandText = this.ExpandText
          };
        return this.expandableControlDefinition;
      }
      set
      {
        this.expandableControlDefinition = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show character counter.
    /// </summary>
    public bool ShowCharacterCounter { set; get; }

    /// <summary>Gets or sets the recommended characters count.</summary>
    public int RecommendedCharactersCount { set; get; }

    /// <summary>Gets or sets the character counter description.</summary>
    public string CharacterCounterDescription { set; get; }

    /// <summary>
    /// Gets or sets a value indicating whether to trim spaces before and after the text.
    /// </summary>
    public bool TrimSpaces { set; get; }

    /// <summary>
    /// Gets or sets a value indicating if the browser should be allowed to autofill the field.
    /// </summary>
    public bool DisableBrowserAutocomplete { set; get; }

    /// <summary>
    /// Gets or sets the autocomplete service url, used to retreive suggestions
    /// </summary>
    public string AutocompleteServiceUrl { set; get; }

    /// <summary>Gets or sets the autocomplete suggestions count</summary>
    public int AutocompleteSuggestionsCount
    {
      get => this.autocompleteSuggestionsCount;
      set => this.autocompleteSuggestionsCount = value;
    }

    /// <summary>
    /// Gets or sets the text that will be displayed on the control that can expand the hidden part.
    /// </summary>
    /// <value></value>
    public string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control is to
    /// be expanded by default true; otherwise false.
    /// </summary>
    /// <value></value>
    public bool? Expanded
    {
      get => this.expanded;
      set => this.expanded = value;
    }

    /// <summary>
    /// Gets or sets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value></value>
    public WebControl ExpandControl => (WebControl) this.ExpandLink;

    /// <summary>
    /// Gets or sets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    /// <value></value>
    public WebControl ExpandTarget => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<WebControl>("expandableTarget_write", true) : this.Container.GetControl<WebControl>("expandableTarget_read", false);

    /// <summary>Gets or sets the default value.</summary>
    /// <value>The default value.</value>
    public override object DefaultValue
    {
      get => string.IsNullOrEmpty(base.DefaultValue as string) ? this.Value : base.DefaultValue;
      set => base.DefaultValue = value;
    }

    /// <summary>Gets or sets the read only replacement.</summary>
    /// <value>The replacement value.</value>
    public string ReadOnlyReplacement { get; set; }

    /// <summary>
    /// Gets or sets the value of the suffix that will be used to make unique fields instantiated in client templates.
    /// </summary>
    public string ClientTemplateSuffix
    {
      get => this.clientTemplateSuffix.IsNullOrEmpty() ? "_" + this.ID : this.clientTemplateSuffix;
      set => this.clientTemplateSuffix = value;
    }

    /// <summary>
    /// Gets the dictionary, holding the IDs of the containers for each state.
    /// </summary>
    public Dictionary<string, object> ConditionDictionary
    {
      get
      {
        if (this.conditionDictionary == null)
          this.conditionDictionary = new Dictionary<string, object>();
        return this.conditionDictionary;
      }
    }

    /// <summary>Gets or sets the current condition.</summary>
    /// <value>Condition to be used.</value>
    public string CurrentCondition { get; set; }

    /// <summary>Gets or sets the unit of the value.</summary>
    /// <remarks>
    /// The unit will be displayed, if present, after the value.
    /// </remarks>
    public string Unit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the text field is nullable.
    /// Empty text field will return null value instead of empty string.
    /// </summary>
    public bool AllowNulls { get; set; }

    /// <summary>Gets or sets the max character count.</summary>
    public int MaxCharactersCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show a tooltip.
    /// </summary>
    public bool ToolTipVisible { get; set; }

    /// <summary>Get or sets the text of the tooltip target.</summary>
    public string ToolTipText { get; set; }

    /// <summary>Gets or sets the title of the tooltip.</summary>
    public string ToolTipTitle { get; set; }

    /// <summary>Gets or sets the content of the tooltip.</summary>
    public string ToolTipContent { get; set; }

    /// <summary>Gets or sets the css class of the tooltip.</summary>
    public string ToolTipCssClass { get; set; }

    /// <summary>Gets or sets the css class of the tooltip target.</summary>
    public string ToolTipTargetCssClass { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the client side component should be initialized in Read mode.
    /// </summary>
    public bool ServerSideOnly { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the field is localizable.
    /// </summary>
    /// <value>A value indicating whether the field is localizable.</value>
    public bool IsLocalizable { get; set; }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control works in any mode.</remarks>
    protected internal virtual Label TitleLabel => this.DisplayMode == FieldDisplayMode.Read ? this.Container.GetControl<Label>("titleLabel_read", true) : this.Container.GetControl<Label>("titleLabel_write", true);

    /// <summary>
    /// Gets the reference to the label control that represents the description of the field control.
    /// </summary>
    /// <remarks>This control works in any mode.</remarks>
    protected internal virtual Label DescriptionLabel => this.DisplayMode == FieldDisplayMode.Read ? this.Container.GetControl<Label>("descriptionLabel_read", true) : this.Container.GetControl<Label>("descriptionLabel_write", true);

    /// <summary>
    /// Gets the reference to the label control that displays the example for this
    /// field control.
    /// </summary>
    /// <remarks>This control is mandatory only in the write mode.</remarks>
    protected internal virtual Label ExampleLabel => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<Label>("exampleLabel_write", true) : this.Container.GetControl<Label>("exampleLabel_read", false);

    /// <summary>
    /// Gets the reference to the link button that is used to expand the control.
    /// </summary>
    /// <value>The expand link.</value>
    protected internal virtual LinkButton ExpandLink => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<LinkButton>("expandButton_write", true) : this.Container.GetControl<LinkButton>("expandButton_read", false);

    /// <summary>Gets the character counter label.</summary>
    /// <value>The character counter label.</value>
    protected internal virtual Label CharacterCounterLabel => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<Label>("characterCounter_write", false) : this.Container.GetControl<Label>("characterCounter_read", false);

    /// <summary>Gets the character counter description label.</summary>
    /// <value>The character counter description label.</value>
    protected internal virtual Label CharacterCounterDescriptionLabel => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<Label>("characterCounterDescription_write", false) : (Label) null;

    /// <summary>
    /// Gets the text box control which should be displayed at Write Mode
    /// </summary>
    /// <value>The text box control.</value>
    protected internal virtual TextBox TextBoxControl => this.DisplayMode == FieldDisplayMode.Write ? this.Container.GetControl<TextBox>("textBox_write", true) : this.Container.GetControl<TextBox>("textBox_read", false);

    /// <summary>
    /// Gets the label control which should be displayed at Read Mode
    /// </summary>
    /// <value>The text label control.</value>
    protected internal virtual Label LabelControl => this.DisplayMode == FieldDisplayMode.Read ? this.Container.GetControl<Label>("textLabel_read", true) : this.Container.GetControl<Label>("textLabel_write", false);

    /// <summary>Gets or sets the client side templates container.</summary>
    /// <value>The client side templates container.</value>
    protected internal virtual Panel ClientSideTemplatesContainer { get; set; }

    /// <summary>Gets the tooltip control.</summary>
    protected internal virtual SitefinityToolTip ToolTip => this.DisplayMode == FieldDisplayMode.Read ? this.Container.GetControl<SitefinityToolTip>("tooltip_read", false) : this.Container.GetControl<SitefinityToolTip>("tooltip_write", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ConstructControl();
      this.ConstructClientSideTemplates(container);
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      if (this.DisplayMode == FieldDisplayMode.Read && this.ServerSideOnly)
        return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[0];
      Dictionary<string, string> idDictionary = this.GetIDDictionary();
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      if (this.DisplayMode == FieldDisplayMode.Write)
        scriptDescriptorList.Add((ScriptDescriptor) this.GetExpandableExtenderDescriptor(this.ClientID));
      ScriptControlDescriptor controlDescriptor = this.GetBaseScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.LabelControl != null)
        controlDescriptor.AddElementProperty("labelElement", this.LabelControl.ClientID);
      if (this.TextBoxControl != null)
        controlDescriptor.AddElementProperty("textBoxElement", this.TextBoxControl.ClientID);
      if (this.ShowCharacterCounter && this.CharacterCounterLabel != null)
      {
        controlDescriptor.AddElementProperty("characterCounterElement", this.CharacterCounterLabel.ClientID);
        controlDescriptor.AddProperty("recommendedCharactersCount", (object) this.RecommendedCharactersCount);
      }
      if (!string.IsNullOrEmpty(this.HideIfValue))
        controlDescriptor.AddProperty("_hideIfValue", (object) this.HideIfValue);
      if (this.ClientSideTemplatesContainer != null)
        controlDescriptor.AddProperty("_conditionalTemplatesContainerId", (object) this.ClientSideTemplatesContainer.ClientID);
      if (!string.IsNullOrEmpty(this.ClientTemplateSuffix))
        controlDescriptor.AddProperty("suffix", (object) this.ClientTemplateSuffix);
      if (idDictionary != null)
        controlDescriptor.AddProperty("conditionDictionary", (object) idDictionary);
      if (!string.IsNullOrEmpty(this.CurrentCondition))
        controlDescriptor.AddProperty("_currentCondition", (object) this.CurrentCondition);
      controlDescriptor.AddProperty("_allowNulls", (object) this.AllowNulls);
      controlDescriptor.AddProperty("_unit", (object) this.Unit);
      controlDescriptor.AddProperty("_textBoxId", (object) "textBox_write");
      controlDescriptor.AddProperty("_textLabelId", (object) "textLabel_read");
      controlDescriptor.AddProperty("_trimSpaces", (object) this.TrimSpaces);
      controlDescriptor.AddProperty("_maxChars", (object) this.MaxCharactersCount);
      controlDescriptor.AddProperty("_readOnlyReplacement", (object) this.ReadOnlyReplacement);
      controlDescriptor.AddProperty("_isLocalizable", (object) this.IsLocalizable);
      if (!string.IsNullOrEmpty(this.AutocompleteServiceUrl))
        controlDescriptor.AddProperty("_autocompleteServiceUrl", (object) this.AutocompleteServiceUrl.Replace("{take}", this.AutocompleteSuggestionsCount.ToString()));
      scriptDescriptorList.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      if (this.DisplayMode == FieldDisplayMode.Read && this.ServerSideOnly)
        return (IEnumerable<ScriptReference>) new ScriptReference[0];
      string fullName = typeof (TextField).Assembly.FullName;
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      if (this.DisplayMode == FieldDisplayMode.Write)
        scriptReferences.Add(this.GetExpandableExtenderScript());
      ScriptReference scriptReference1 = new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.TextField.js", fullName);
      scriptReferences.Add(scriptReference1);
      ScriptReference scriptReference2 = new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName);
      scriptReferences.Add(scriptReference2);
      if (!string.IsNullOrEmpty(this.AutocompleteServiceUrl))
      {
        ScriptReference scriptReference3 = new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources");
        scriptReferences.Add(scriptReference3);
      }
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      this.ConfigureBaseDefinition(definition);
      if (!(definition is ITextFieldDefinition textFieldDefinition))
        return;
      this.ExpandableControlDefinition = new ExpandableControlDefinition()
      {
        ControlDefinitionName = textFieldDefinition.ControlDefinitionName,
        ViewName = textFieldDefinition.ViewName,
        SectionName = textFieldDefinition.SectionName,
        FieldName = textFieldDefinition.FieldName,
        Expanded = textFieldDefinition.ExpandableDefinition.Expanded
      };
      this.Expanded = textFieldDefinition.ExpandableDefinition.Expanded;
      this.Rows = textFieldDefinition.Rows;
      this.Value = textFieldDefinition.Value;
      this.HideIfValue = textFieldDefinition.HideIfValue;
      bool? isPasswordMode = textFieldDefinition.IsPasswordMode;
      int num;
      if (isPasswordMode.HasValue)
      {
        isPasswordMode = textFieldDefinition.IsPasswordMode;
        num = isPasswordMode.Value ? 1 : 0;
      }
      else
        num = 0;
      this.IsPasswordMode = num != 0;
      this.Unit = textFieldDefinition.Unit;
      this.RecommendedCharactersCount = textFieldDefinition.Validation.RecommendedCharactersCount ?? (textFieldDefinition.RecommendedCharactersCount != 0 ? textFieldDefinition.RecommendedCharactersCount : 0);
      this.ShowCharacterCounter = textFieldDefinition.ShowCharacterCounter || (uint) this.RecommendedCharactersCount > 0U;
      this.MaxCharactersCount = textFieldDefinition.MaxCharactersCount;
      this.CharacterCounterDescription = textFieldDefinition.CharacterCounterDescription;
      this.TrimSpaces = textFieldDefinition.TrimSpaces;
      this.AllowNulls = textFieldDefinition.AllowNulls;
      this.ToolTipContent = Res.ResolveLocalizedValue(textFieldDefinition.ResourceClassId, textFieldDefinition.ToolTipContent);
      this.ToolTipCssClass = textFieldDefinition.ToolTipCssClass;
      this.ToolTipTargetCssClass = textFieldDefinition.ToolTipTargetCssClass;
      this.ToolTipText = Res.ResolveLocalizedValue(textFieldDefinition.ResourceClassId, textFieldDefinition.ToolTipText);
      this.ToolTipTitle = Res.ResolveLocalizedValue(textFieldDefinition.ResourceClassId, textFieldDefinition.ToolTipTitle);
      this.ToolTipVisible = textFieldDefinition.ToolTipVisible;
      this.ReadOnlyReplacement = textFieldDefinition.ReadOnlyReplacement;
      this.AutocompleteServiceUrl = textFieldDefinition.AutocompleteServiceUrl;
      if (textFieldDefinition.AutocompleteSuggestionsCount > 0)
        this.AutocompleteSuggestionsCount = textFieldDefinition.AutocompleteSuggestionsCount;
      this.ServerSideOnly = textFieldDefinition.ServerSideOnly;
      this.IsLocalizable = textFieldDefinition.IsLocalizable;
    }

    /// <summary>
    /// The method that is used to set the properties of the contained controls.
    /// </summary>
    protected internal virtual void ConstructControl()
    {
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      switch (this.DisplayMode)
      {
        case FieldDisplayMode.Read:
          if (this.value != null)
            this.LabelControl.Text = this.value as string;
          this.LabelControl.TabIndex = this.TabIndex;
          if (this.ToolTip == null)
            break;
          this.ToolTip.Visible = false;
          break;
        case FieldDisplayMode.Write:
          this.ExampleLabel.Text = this.Example;
          this.TitleLabel.AssociatedControlID = this.TextBoxControl.ID;
          if (this.value != null)
            this.TextBoxControl.Text = this.value as string;
          this.TextBoxControl.Rows = this.Rows;
          if (this.IsPasswordMode)
            this.TextBoxControl.TextMode = TextBoxMode.Password;
          else if (this.Rows > 1)
            this.TextBoxControl.TextMode = TextBoxMode.MultiLine;
          this.ConfigureExpandableControl((IExpandableControlDefinition) this.ExpandableControlDefinition);
          this.TextBoxControl.TabIndex = this.TabIndex;
          if (!this.Expanded.GetValueOrDefault())
            this.ExpandControl.TabIndex = this.TabIndex;
          this.TabIndex = (short) 0;
          if (this.CharacterCounterLabel != null)
          {
            if (this.ShowCharacterCounter)
              this.CharacterCounterLabel.Visible = true;
            else
              this.CharacterCounterLabel.Visible = false;
            if (this.RecommendedCharactersCount != 0 && this.CharacterCounterDescription != null)
              this.CharacterCounterDescriptionLabel.Text = string.Format(this.CharacterCounterDescription, (object) this.RecommendedCharactersCount);
          }
          if (this.ToolTip != null)
          {
            this.ToolTip.Visible = this.ToolTipVisible;
            this.ToolTip.Text = this.ToolTipText;
            this.ToolTip.Title = this.ToolTipTitle;
            this.ToolTip.Content = this.ToolTipContent;
            this.ToolTip.CssClass = this.ToolTipCssClass;
            this.ToolTip.TargetCssClass = this.ToolTipTargetCssClass;
          }
          if (!this.DisableBrowserAutocomplete)
            break;
          this.TextBoxControl.Attributes.Add("autocomplete", "off");
          break;
      }
    }

    /// <summary>Constructs the client side templates.</summary>
    /// <param name="container">The container.</param>
    protected internal virtual void ConstructClientSideTemplates(GenericContainer container)
    {
      Panel panel1 = new Panel();
      panel1.ID = "ConditionalTemplates";
      this.ClientSideTemplatesContainer = panel1;
      this.ClientSideTemplatesContainer.Style.Add("display", "none");
      container.Controls.Add((Control) this.ClientSideTemplatesContainer);
      ConditionalTemplateContainer control = container.GetControl<ConditionalTemplateContainer>();
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) this);
      foreach (ConditionalTemplate template in control.Templates)
      {
        int num = control.MatchTemplate(template, (object) this, properties) ? 1 : 0;
        string lower = string.Format("{0}-{1}-{2}", (object) template.Left, (object) template.Operator, (object) template.Right).ToLower();
        if (num != 0)
        {
          this.CurrentCondition = lower;
          this.ConditionDictionary[lower] = (object) control;
        }
        else
        {
          Panel panel2 = new Panel();
          panel2.ID = "ClientTemplatePanel";
          Panel panel3 = panel2;
          this.EnsureUniqueIds(template);
          panel3.ID += this.ClientTemplateSuffix;
          this.ConditionDictionary[lower] = (object) panel3;
          template.InstantiateIn((Control) panel3);
          string outputForControl = TextField.GetHtmlOutputForControl((Control) panel3);
          this.ClientSideTemplatesContainer.Controls.Add((Control) new Literal()
          {
            Mode = LiteralMode.PassThrough,
            Text = outputForControl
          });
        }
      }
    }

    private void EnsureUniqueIds(ConditionalTemplate conditionalTemplate) => this.TraverseChildrenAndApplyAction((Control) conditionalTemplate, new Action<Control, string>(this.EnsureControlUniqueId), this.ClientID);

    private void TraverseChildrenAndApplyAction(
      Control control,
      Action<Control, string> action,
      string parentId)
    {
      foreach (Control control1 in control.Controls)
      {
        if (control1 != null)
        {
          action(control1, parentId);
          this.TraverseChildrenAndApplyAction(control1, action, control1.ClientID);
        }
      }
    }

    private void EnsureControlUniqueId(Control control, string parentId)
    {
      if (control.ID.IsNullOrEmpty())
        return;
      control.ID = string.Format("{0}_{1}{2}", (object) parentId, (object) control.ID, (object) this.ClientTemplateSuffix);
    }

    private static string GetHtmlOutputForControl(Control ctrl)
    {
      StringBuilder sb = new StringBuilder();
      HtmlTextWriter writer = new HtmlTextWriter((TextWriter) new StringWriter(sb));
      ctrl.RenderControl(writer);
      writer.Flush();
      return sb.ToString();
    }

    /// <summary>
    /// Used to get dictionary with client ID-s for conditions, using the ConditionDictionary containing the components.
    /// </summary>
    private Dictionary<string, string> GetIDDictionary()
    {
      Dictionary<string, string> idDictionary = new Dictionary<string, string>();
      foreach (KeyValuePair<string, object> condition in this.ConditionDictionary)
        idDictionary.Add(condition.Key, (condition.Value as WebControl).ClientID);
      return idDictionary;
    }

    /// <summary>Configures the base definition.</summary>
    /// <param name="definition">The definition.</param>
    public virtual void ConfigureBaseDefinition(IFieldDefinition definition) => base.Configure(definition);

    /// <summary>Gets the base script descriptors.</summary>
    /// <returns></returns>
    public virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();
  }
}
