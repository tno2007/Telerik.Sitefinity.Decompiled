// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ChoiceField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security.Sanitizers;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// A field control used for displaying and editing choices from a list of values. The control can be rendered
  /// as a Radio button list, Check box list, Drop down list or a List box, depending on the value
  /// of the RenderChoiceAs property.
  /// </summary>
  [FieldDefinitionElement(typeof (ChoiceFieldElement))]
  public class ChoiceField : FieldControl
  {
    private Collection<ChoiceItem> choices;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.ChoiceField.ascx");
    internal const string choiceScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.Choice.js";
    internal const string renderChoicesAsEnumerationScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.RenderChoicesAs.js";
    internal const string choiceFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ChoiceField.js";
    internal const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";
    internal object fieldValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ChoiceField" /> class.
    /// </summary>
    public ChoiceField() => this.LayoutTemplatePath = ChoiceField.layoutTemplatePath;

    /// <summary>Gets or sets the value of the control.</summary>
    public override object Value
    {
      get
      {
        if (!this.ChildControlsCreated)
          return this.fieldValue;
        if (this.DisplayMode != FieldDisplayMode.Write)
          return (object) this.ReadModeLabel.Text;
        switch (this.RenderChoicesAs)
        {
          case RenderChoicesAs.CheckBoxes:
          case RenderChoicesAs.DropDown:
          case RenderChoicesAs.ListBox:
          case RenderChoicesAs.RadioButtons:
          case RenderChoicesAs.HorizontalRadioButtons:
            return (object) this.ChoiceControl.SelectedValue;
          case RenderChoicesAs.SingleCheckBox:
            return (object) this.SingleCheckBox.Checked;
          default:
            throw new NotSupportedException();
        }
      }
      set
      {
        if (this.ChildControlsCreated)
        {
          if (this.DisplayMode == FieldDisplayMode.Write)
          {
            switch (this.RenderChoicesAs)
            {
              case RenderChoicesAs.CheckBoxes:
              case RenderChoicesAs.DropDown:
              case RenderChoicesAs.ListBox:
              case RenderChoicesAs.RadioButtons:
              case RenderChoicesAs.HorizontalRadioButtons:
                this.ChoiceControl.SelectedValue = value as string;
                break;
              case RenderChoicesAs.SingleCheckBox:
                switch (value)
                {
                  case string str:
                    this.SingleCheckBox.Checked = bool.Parse(str);
                    break;
                  case bool flag1:
                    this.SingleCheckBox.Checked = flag1;
                    break;
                }
                break;
              default:
                throw new NotSupportedException();
            }
          }
          else
            this.ReadModeLabel.Text = ObjectFactory.Resolve<IHtmlSanitizer>().Sanitize(value as string);
        }
        else
        {
          string stringValue = value as string;
          if (this.Choices.Count == 1)
          {
            if (value is bool flag2)
            {
              this.Choices.First<ChoiceItem>().Selected = flag2;
            }
            else
            {
              bool result;
              if (stringValue != null && bool.TryParse(stringValue, out result))
                this.Choices.First<ChoiceItem>().Selected = result;
            }
          }
          else if (stringValue != null)
          {
            ChoiceItem choiceItem = this.Choices.FirstOrDefault<ChoiceItem>((Func<ChoiceItem, bool>) (c => c.Value != null && c.Value.Equals(stringValue)));
            if (choiceItem != null)
            {
              foreach (ChoiceItem choice in this.Choices)
                choice.Selected = false;
              choiceItem.Selected = true;
            }
          }
        }
        this.fieldValue = value;
      }
    }

    /// <summary>
    /// Gets the choices that are available to be chosen from.
    /// </summary>
    /// <value>The collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ChoiceItem" /> objects.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual Collection<ChoiceItem> Choices
    {
      get
      {
        if (this.choices == null)
          this.choices = new Collection<ChoiceItem>();
        return this.choices;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether choices are mutually exclusive.
    /// </summary>
    /// <value><c>true</c> if [mutually exclusive]; otherwise, <c>false</c>.</value>
    public virtual bool MutuallyExclusive { get; set; }

    /// <summary>
    /// Gets or sets the way in which choices should be rendered.
    /// </summary>
    /// <value>One of the values of the <see cref="P:Telerik.Sitefinity.Web.UI.Fields.ChoiceField.RenderChoicesAs" /> enumeration.</value>
    public virtual RenderChoicesAs RenderChoicesAs { get; set; }

    /// <summary>A value inidcating whether the title is shown.</summary>
    public virtual bool HideTitle { get; set; }

    /// <summary>Gets or sets the index of currently selected index.</summary>
    /// <remarks>
    /// Use negative number (e.g. -1) to indicate that no choices have been selected.
    /// </remarks>
    public virtual ICollection<int> SelectedChoicesIndex { get; set; }

    /// <summary>
    ///  Represents the control that renders the choice field in WriteMode
    ///  Depending on the RenderChoiceAs - CheckBoxes, DropDown, ListBox, RadioButtons
    /// </summary>
    public ListControl ChoiceControl
    {
      get
      {
        if (this.DisplayMode == FieldDisplayMode.Write)
        {
          switch (this.RenderChoicesAs)
          {
            case RenderChoicesAs.CheckBoxes:
              return (ListControl) this.CheckBoxes;
            case RenderChoicesAs.DropDown:
              return (ListControl) this.DropDown;
            case RenderChoicesAs.ListBox:
              return (ListControl) this.ListBox;
            case RenderChoicesAs.RadioButtons:
            case RenderChoicesAs.HorizontalRadioButtons:
              return (ListControl) this.RadioButtons;
          }
        }
        return (ListControl) null;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the default value of the property.</summary>
    /// <value>The value.</value>
    /// <remarks>Used to check for changes</remarks>
    public override object DefaultValue
    {
      get
      {
        if (base.DefaultValue == null && this.DisplayMode == FieldDisplayMode.Write)
        {
          if (this.RenderChoicesAs == RenderChoicesAs.SingleCheckBox)
          {
            this.DefaultValue = (object) this.SingleCheckBox.Checked.ToString().ToLowerInvariant();
          }
          else
          {
            IEnumerable<ListItem> source1 = this.ChoiceControl.Items.OfType<ListItem>();
            IEnumerable<ListItem> source2 = source1.Where<ListItem>((Func<ListItem, bool>) (i => i.Selected));
            if (source1.Count<ListItem>() > 0)
            {
              if (source2.Count<ListItem>() > 0)
                this.DefaultValue = (object) source2.Select<ListItem, string>((Func<ListItem, string>) (s => s.Value.ToString()));
              else
                this.DefaultValue = (object) source1.ElementAt<ListItem>(0).Value.ToString();
            }
          }
        }
        return base.DefaultValue;
      }
    }

    protected bool ReturnValuesAlwaysInArray { get; set; }

    /// <summary>
    /// When set to true; control works only in server side mode.
    /// </summary>
    public bool DisableClientScripts { get; set; }

    /// <summary>
    /// Invokes GetConditionalControlName and calls this.Container.GetControl with the resulting name
    /// </summary>
    /// <typeparam name="T">Type of the control to load</typeparam>
    /// <param name="originalName">Name of the control without suffixes</param>
    /// <param name="required">True if an exception should be thrown if the control is not found</param>
    /// <returns>Loaded control or null when not found and required is false</returns>
    protected T GetConditionalControl<T>(string originalName, bool required) => this.Container.GetControl<T>(this.GetConditionalControlName(originalName), required);

    /// <summary>
    /// Adds a suffix depending on this.DisplayMode and this.RenderChoiceAs, this making a unique System.Web.Control.ID
    /// </summary>
    /// <param name="originalName">The common part of the name (e.g. titleLabel)</param>
    /// <returns>Unique ID for the control within a conditional template depending on this.DisplayMode and this.RenderChoiceAs</returns>
    protected virtual string GetConditionalControlName(string originalName)
    {
      string str = this.DisplayMode != FieldDisplayMode.Read ? this.RenderChoicesAs.ToString().ToLower() : "read";
      return originalName + "_" + str;
    }

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value></value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the label that displays the title of the choice field.
    /// </summary>
    protected internal virtual Label TitleLabel => this.GetConditionalControl<Label>("titleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displays the description of the choice field.
    /// </summary>
    protected internal virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label that displays the example of the choice field.
    /// </summary>
    protected internal virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the check box list control that represents choices when RenderChoicesAs
    /// property is set to <see cref="F:Telerik.Sitefinity.Web.UI.Fields.Enums.RenderChoicesAs.CheckBoxes" />.
    /// </summary>
    protected internal virtual CheckBoxList CheckBoxes => this.Container.GetControl<CheckBoxList>("checkBoxes", true);

    /// <summary>
    /// Gets the reference to the drop down list control that represents choices when RenderChoicesAs
    /// property is set to <see cref="F:Telerik.Sitefinity.Web.UI.Fields.Enums.RenderChoicesAs.DropDown" />.
    /// </summary>
    protected internal virtual DropDownList DropDown => this.Container.GetControl<DropDownList>("dropDown", true);

    /// <summary>
    /// Gets the reference to the list box control that represents choices when RenderChoicesAs
    /// property is set to <see cref="T:System.Web.UI.WebControls.ListBox" />.
    /// </summary>
    protected internal virtual ListBox ListBox => this.Container.GetControl<ListBox>("listBox", true);

    /// <summary>
    /// Gets the reference to the radio button list control that represents choices when RenderChoicesAs
    /// property is set to <see cref="F:Telerik.Sitefinity.Web.UI.Fields.Enums.RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual RadioButtonList RadioButtons => this.GetConditionalControl<RadioButtonList>("radioButtons", true);

    /// <summary>
    /// Gets a reference to the Label control used to display the value of the choice field in read mode
    /// </summary>
    /// <value>The read mode label.</value>
    protected internal virtual Label ReadModeLabel => this.Container.GetControl<Label>("read", true);

    /// <summary>
    /// Gets a reference to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> control in SingleCheckBox mode.
    /// </summary>
    /// <value>The single check box.</value>
    protected internal virtual CheckBox SingleCheckBox => this.Container.GetControl<CheckBox>("singleCheckBox", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.HideTitle)
        this.TitleLabel.SetTextOrHide(this.Title);
      this.DescriptionLabel.SetTextOrHide(this.Description);
      this.ExampleLabel.SetTextOrHide(this.Example);
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        if (this.RenderChoicesAs == RenderChoicesAs.SingleCheckBox)
        {
          this.SingleCheckBox.Text = this.Choices.Count <= 1 ? this.Choices[0].Text : throw new ArgumentException("When RenderChoiceAs is set to SingleCheckBox, there should be only one ChoiceItem in the collection");
          this.SingleCheckBox.Checked = this.Choices[0].Selected;
        }
        else
        {
          foreach (ChoiceItem choice in this.Choices)
            this.ChoiceControl.Items.Add(new ListItem(choice.Text, choice.Value)
            {
              Selected = choice.Selected
            });
        }
      }
      else
      {
        if (this.fieldValue == null)
          return;
        if (this.fieldValue is string fieldValue1)
          this.ReadModeLabel.Text = fieldValue1;
        if (this.fieldValue is IEnumerable fieldValue2)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (object obj in fieldValue2)
          {
            stringBuilder.Append(obj.ToString());
            stringBuilder.Append(", ");
          }
          if (stringBuilder.Length > 1)
            stringBuilder.Remove(stringBuilder.Length - 2, 2);
          this.ReadModeLabel.Text = stringBuilder.ToString();
        }
        bool? fieldValue3 = this.fieldValue as bool?;
        if (!fieldValue3.HasValue)
          return;
        this.ReadModeLabel.Text = fieldValue3.Value ? Res.Get<Labels>().Yes : Res.Get<Labels>().No;
      }
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      if (definition is IChoiceFieldDefinition choiceFieldDefinition)
      {
        this.Choices.Clear();
        foreach (IChoiceDefinition choice in choiceFieldDefinition.Choices)
          this.Choices.Add(new ChoiceItem(choice));
        this.MutuallyExclusive = choiceFieldDefinition.MutuallyExclusive;
        this.RenderChoicesAs = choiceFieldDefinition.RenderChoiceAs;
        this.SelectedChoicesIndex = choiceFieldDefinition.SelectedChoicesIndex;
        this.HideTitle = choiceFieldDefinition.HideTitle;
        this.ReturnValuesAlwaysInArray = choiceFieldDefinition.ReturnValuesAlwaysInArray;
      }
      this.ConfigureBase(definition);
    }

    internal virtual void ConfigureBase(IFieldDefinition definition) => base.Configure(definition);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      if (this.DisableClientScripts)
        return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[0];
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_mutuallyExclusive", (object) this.MutuallyExclusive);
      controlDescriptor.AddProperty("_renderChoicesAs", (object) this.RenderChoicesAs);
      controlDescriptor.AddProperty("_selectedChoicesIndex", (object) this.SelectedChoicesIndex);
      controlDescriptor.AddProperty("_returnValuesAlwaysInArray", (object) this.ReturnValuesAlwaysInArray);
      controlDescriptor.AddProperty("choices", (object) this.Choices);
      if (this.DisplayMode == FieldDisplayMode.Write)
      {
        if (this.RenderChoicesAs == RenderChoicesAs.SingleCheckBox)
          controlDescriptor.AddElementProperty("choiceElement", this.SingleCheckBox.ClientID);
        else
          controlDescriptor.AddElementProperty("choiceElement", this.ChoiceControl.ClientID);
        if (this.RenderChoicesAs == RenderChoicesAs.CheckBoxes)
        {
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          for (int index = 0; index < this.ChoiceControl.Items.Count; ++index)
          {
            ListItem listItem = this.ChoiceControl.Items[index];
            string key = this.ChoiceControl.ClientID + "_" + (object) index;
            dictionary.Add(key, listItem.Value);
          }
          controlDescriptor.AddProperty("listItemValueMap", (object) dictionary);
        }
      }
      else
        controlDescriptor.AddElementProperty("readModeLabel", this.ReadModeLabel.ClientID);
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
      if (this.DisableClientScripts)
        return (IEnumerable<ScriptReference>) new ScriptReference[0];
      string fullName = typeof (ChoiceField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.Choice.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.RenderChoicesAs.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ChoiceField.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", fullName)
      };
    }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;
  }
}
