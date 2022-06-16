// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.FieldControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;
using Telerik.Sitefinity.Web.UI.Validation.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// A base abstract class containing needed properties for constructing a field control.
  /// The class implements <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl" /> interface.
  /// </summary>
  [ValidationProperty("Value")]
  public abstract class FieldControl : 
    SimpleScriptView,
    Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl,
    IField,
    IValidatable,
    IHasFieldDisplayMode
  {
    private ValidatorDefinition validatorDefinition;
    private FieldDisplayMode displayMode;
    private string title;
    private bool showMessageOnError = true;
    private Validator validator;
    private WebControl errorMessageControl;
    internal const string FieldControlScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldControl.js";
    internal const string IFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IField.js";
    internal const string ValidatorComponentScript = "Telerik.Sitefinity.Web.UI.Validation.Scripts.Validator.js";
    internal const string XRegExp = "Telerik.Sitefinity.Resources.Scripts.xregexp-min.js";
    internal const string XRegexpUnicodeBase = "Telerik.Sitefinity.Resources.Scripts.xregexp-unicode-base.js";
    private const string fieldDisplayModeScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js";

    /// <summary>Gets or sets the title of the field control.</summary>
    /// <value>The title of the field control.</value>
    [MultilingualProperty]
    public virtual string Title
    {
      get => ControlUtilities.Sanitize(this.title);
      set
      {
        if (!(this.title != value))
          return;
        this.title = ControlUtilities.Sanitize(value);
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the description of the field control.</summary>
    /// <value>The description of the field control.</value>
    [MultilingualProperty]
    public virtual string Description { get; set; }

    /// <summary>
    /// Gets or sets the example text associated with the field control.
    /// </summary>
    /// <value>The example text associated with the field control.</value>
    [MultilingualProperty]
    public virtual string Example { get; set; }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the field.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as Title, Description and Example will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    public virtual string ResourceClassId { get; set; }

    /// <summary>
    /// Gets or sets the name of the data item property the control will be bound to.
    /// </summary>
    /// <value>The name of the data field.</value>
    public virtual string DataFieldName { get; set; }

    /// <summary>
    /// Gets or sets the type of the data item to which the control will be bound to.
    /// </summary>
    /// <value>The type of the data item.</value>
    public virtual string DataItemType { get; set; }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    public virtual object Value { get; set; }

    /// <summary>Gets or sets the template of the field control.</summary>
    /// <value>The template.</value>
    public virtual ConditionalTemplateContainer ConditionalTemplates { get; set; }

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    public virtual FieldDisplayMode DisplayMode
    {
      get => this.displayMode;
      set
      {
        if (this.displayMode == value)
          return;
        this.displayMode = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the format of the data if needed. Used to specify how the value of the field control is
    /// displayed in Read mode. This should be validated by the specific implementation of the field control
    /// </summary>
    /// <value>The data format.</value>
    public virtual string DataFormat { get; set; }

    /// <summary>Gets or sets a validation mechanism for the control.</summary>
    /// <value>The validation.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    [TypeConverter(typeof (ExpandableObjectConverter))]
    public virtual ValidatorDefinition ValidatorDefinition
    {
      get
      {
        if (this.validatorDefinition == null)
          this.validatorDefinition = new ValidatorDefinition();
        return this.validatorDefinition;
      }
      set => this.validatorDefinition = value;
    }

    /// <summary>Gets or sets a validation group for the control</summary>
    /// <value></value>
    public virtual string ValidationGroup { get; set; }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    public virtual HtmlTextWriterTag WrapperTag { get; set; }

    /// <summary>
    /// CSS Class that is added to the whole control upon error in validation
    /// </summary>
    public virtual string ControlCssClassOnError { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show message on error.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show message on error; otherwise, <c>false</c>.
    /// </value>
    public bool ShowMessageOnError
    {
      get => this.showMessageOnError;
      set => this.showMessageOnError = value;
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Web.UI.Fields.FieldControl.Validator" /> configured with the field controls' <see cref="P:Telerik.Sitefinity.Web.UI.Fields.FieldControl.ValidatorDefinition" />.
    /// </summary>
    /// <value></value>
    public Validator Validator
    {
      get
      {
        if (this.validator == null)
        {
          this.validator = new Validator((IValidatorDefinition) this.ValidatorDefinition, this.NamingContainer);
          this.validator.Validated += new EventHandler<ValidatedEventArgs>(this.Validator_Validated);
        }
        else
          this.validator.Configure((IValidatorDefinition) this.ValidatorDefinition);
        return this.validator;
      }
    }

    /// <summary>Gets or sets the default value of the property.</summary>
    /// <remarks>
    /// Used to check for changes in the values
    /// Implement in the iherited control if need some specific behavior
    /// </remarks>
    /// <value>The value.</value>
    public virtual object DefaultValue { get; set; }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    public virtual string FieldName { get; set; }

    /// <summary>
    /// Gets the type (full name) of the script descriptor to be used. By default
    /// it returns current type.
    /// </summary>
    protected virtual string ScriptDescriptorType => this.GetType().FullName;

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal virtual WebControl TitleControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal virtual WebControl DescriptionControl => (WebControl) null;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal virtual WebControl ExampleControl => (WebControl) null;

    /// <summary>Gets the error message control.</summary>
    /// <value>The error message control.</value>
    public virtual WebControl ErrorMessageControl
    {
      get => this.errorMessageControl == null ? this.Container.GetControl<WebControl>("errorMessageControl", false) : this.errorMessageControl;
      set => this.errorMessageControl = value;
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public virtual void Configure(IFieldDefinition definition)
    {
      if (definition is IFieldControlDefinition controlDefinition)
      {
        this.ID = controlDefinition.ID;
        if (controlDefinition.DisplayMode.HasValue)
          this.DisplayMode = controlDefinition.DisplayMode.Value;
        this.DataFieldName = controlDefinition.DataFieldName;
        this.Value = controlDefinition.Value;
        if (controlDefinition.Validation != null)
        {
          this.ValidatorDefinition = controlDefinition.Validation.GetDefinition<ValidatorDefinition>();
          Validator.ResolveLocalizedValues((IValidatorDefinitionBase) this.ValidatorDefinition, this.ValidatorDefinition.ResourceClassId);
        }
        this.ValidatorDefinition.ControlDefinitionName = controlDefinition.ControlDefinitionName;
        this.ValidatorDefinition.ViewName = controlDefinition.ViewName;
        this.ValidatorDefinition.SectionName = controlDefinition.SectionName;
        this.ValidatorDefinition.FieldName = controlDefinition.FieldName;
        this.Description = Telerik.Sitefinity.Localization.Res.ResolveLocalizedValue(controlDefinition.ResourceClassId, controlDefinition.Description);
        this.Example = Telerik.Sitefinity.Localization.Res.ResolveLocalizedValue(controlDefinition.ResourceClassId, controlDefinition.Example);
        this.Title = Telerik.Sitefinity.Localization.Res.ResolveLocalizedValue(controlDefinition.ResourceClassId, controlDefinition.Title);
        ControlUtilities.AddCssClass((WebControl) this, controlDefinition.CssClass);
        this.ResourceClassId = controlDefinition.ResourceClassId;
        this.WrapperTag = controlDefinition.WrapperTag;
        bool? hidden = controlDefinition.Hidden;
        if (hidden.HasValue)
        {
          hidden = controlDefinition.Hidden;
          this.Visible = !hidden.Value;
        }
        this.FieldName = controlDefinition.FieldName;
      }
      this.ChildControlsCreated = false;
    }

    /// <summary>Creates the container.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      this.CheckConditionalTemplates(container);
      return container;
    }

    /// <summary>
    /// Checks if the container contains any conditional templates.
    /// </summary>
    /// <param name="container">The container.</param>
    protected internal virtual void CheckConditionalTemplates(GenericContainer container) => container.GetControl<ConditionalTemplateContainer>()?.Evaluate((object) this);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => this.WrapperTag == HtmlTextWriterTag.Unknown ? HtmlTextWriterTag.Div : this.WrapperTag;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.EnsureNotSelfValidating();
      FormManager current = FormManager.GetCurrent(this.Page);
      if (current == null)
      {
        if (this.DisplayMode == FieldDisplayMode.Write && !this.IsDesignMode())
          throw new InvalidOperationException(string.Format("When in Write mode the control with ID '{0}' requires a FormManager on the page.", (object) this.ID));
      }
      else
        current.RegisterID(this);
      this.SetInnerControlsVisibility();
    }

    private void SetInnerControlsVisibility()
    {
      if (string.IsNullOrEmpty(this.Title) && this.TitleControl != null)
        this.TitleControl.Visible = false;
      if (string.IsNullOrEmpty(this.Example) && this.ExampleControl != null)
        this.ExampleControl.Visible = false;
      if (!string.IsNullOrEmpty(this.Description) || this.DescriptionControl == null)
        return;
      this.DescriptionControl.Visible = false;
    }

    /// <summary>Determines whether the value oft the is valid.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public virtual bool IsValid() => !this.Visible || this.DisplayMode == FieldDisplayMode.Read || this.Validator.IsValid(this.Value);

    /// <summary>
    /// Handles the Validated event of the field controls' Validator control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs" /> instance containing the event data.</param>
    protected virtual void Validator_Validated(object sender, ValidatedEventArgs e)
    {
      if (!this.ShowMessageOnError)
        return;
      this.EnsureErrorMessageControl();
      this.DetermineErrorMessageControlState(e);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorType, this.ClientID);
      controlDescriptor.AddProperty("dataFieldName", (object) this.DataFieldName);
      controlDescriptor.AddProperty("dataFormatString", (object) this.DataFormat);
      controlDescriptor.AddProperty("description", (object) this.Description);
      controlDescriptor.AddProperty("displayMode", (object) this.DisplayMode);
      controlDescriptor.AddProperty("example", (object) this.Example);
      controlDescriptor.AddProperty("title", (object) this.Title);
      controlDescriptor.AddProperty("value", this.Value);
      controlDescriptor.AddProperty("controlErrorCssClass", (object) this.ControlCssClassOnError);
      if (this.DefaultValue != null)
        controlDescriptor.AddProperty("defaultValue", this.DefaultValue);
      if (this.DescriptionControl != null)
        controlDescriptor.AddElementProperty("descriptionElement", this.DescriptionControl.ClientID);
      if (this.ExampleControl != null)
        controlDescriptor.AddElementProperty("exampleElement", this.ExampleControl.ClientID);
      if (this.TitleControl != null)
        controlDescriptor.AddElementProperty("titleElement", this.TitleControl.ClientID);
      string str = this.SerializeValidatorDefinition();
      controlDescriptor.AddProperty("validatorDefinition", (object) str);
      controlDescriptor.AddProperty("fieldName", (object) this.FieldName);
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
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>();
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IField.js", typeof (IField).Assembly.FullName));
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Validation.Scripts.Validator.js", typeof (Validator).Assembly.FullName));
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldControl.js", typeof (FieldControl).Assembly.FullName));
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.FieldDisplayMode.js", typeof (FieldControl).Assembly.FullName));
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.xregexp-min.js", name));
      scriptReferenceList.Add(new ScriptReference("Telerik.Sitefinity.Resources.Scripts.xregexp-unicode-base.js", name));
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }

    internal virtual string SerializeValidatorDefinition()
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (XmlDictionaryWriter jsonWriter = JsonReaderWriterFactory.CreateJsonWriter((Stream) memoryStream, Encoding.Unicode))
          new DataContractJsonSerializer(typeof (ValidatorDefinition)).WriteObject(jsonWriter, (object) this.ValidatorDefinition);
        return Encoding.Unicode.GetString(memoryStream.ToArray());
      }
    }

    private void EnsureNotSelfValidating()
    {
      foreach (IComparingValidatorDefinition validatorDefinition in this.ValidatorDefinition.ComparingValidatorDefinitions)
      {
        if (validatorDefinition.ControlToCompare == this.ID)
          throw new InvalidOperationException(string.Format("You cannot validate a control with id: {0} against it's own value. Check ControlToCompare propertie(s)", (object) this.ID));
      }
    }

    private void EnsureErrorMessageControl()
    {
      if (this.ErrorMessageControl != null)
        return;
      this.ErrorMessageControl = (WebControl) new SitefinityLabel()
      {
        HideIfNoText = true,
        HideIfNoTextMode = HideIfNoTextMode.Server,
        WrapperTagName = HtmlTextWriterTag.Div
      };
      this.SetErrorMessageControlCss();
      this.Container.Controls.Add((Control) this.ErrorMessageControl);
    }

    private void SetErrorMessageControlCss()
    {
      if (string.IsNullOrEmpty(this.ValidatorDefinition.MessageCssClass))
        return;
      if (string.IsNullOrEmpty(this.ErrorMessageControl.CssClass))
        this.ErrorMessageControl.CssClass = this.ValidatorDefinition.MessageCssClass;
      else
        this.ErrorMessageControl.CssClass = string.Format("{0} {1}", (object) this.ErrorMessageControl.CssClass, (object) this.ValidatorDefinition.MessageCssClass);
    }

    private void DetermineErrorMessageControlState(ValidatedEventArgs e)
    {
      if (!e.IsValid)
      {
        if (this.ErrorMessageControl is ITextControl)
          ((ITextControl) this.ErrorMessageControl).Text = ControlUtilities.Sanitize(e.ErrorMessage);
        this.ErrorMessageControl.Visible = true;
      }
      else
        this.ErrorMessageControl.Visible = false;
    }
  }
}
