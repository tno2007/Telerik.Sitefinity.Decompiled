// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Fluent API facade that defines a definition for section element.
  /// </summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class SectionDefinitionFacade<TParentFacade> : 
    IHasFieldControls<SectionDefinitionFacade<TParentFacade>>
    where TParentFacade : class
  {
    private string moduleName;
    private string definitionName;
    private Type contentType;
    private ConfigElementDictionary<string, ContentViewSectionElement> parentElement;
    private string viewName;
    private TParentFacade parentFacade;
    private string sectionName;
    private string resourceClassId;
    private FieldControlsCollection<SectionDefinitionFacade<TParentFacade>> fields;
    private DefinitionFacadeInfo facadeInfo;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    /// <param name="mode">The field display mode.</param>
    public SectionDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, ContentViewSectionElement> parentElement,
      string viewName,
      TParentFacade parentFacade,
      string sectionName,
      string resourceClassId,
      FieldDisplayMode mode)
    {
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      if (string.IsNullOrEmpty(viewName))
        throw new ArgumentNullException(nameof (viewName));
      if (string.IsNullOrEmpty(sectionName))
        throw new ArgumentNullException(nameof (sectionName));
      this.moduleName = moduleName;
      this.definitionName = definitionName;
      this.contentType = contentType;
      this.parentElement = parentElement;
      this.viewName = viewName;
      this.parentFacade = parentFacade;
      this.sectionName = sectionName;
      this.resourceClassId = resourceClassId;
      this.Section = new ContentViewSectionElement((ConfigElement) parentElement)
      {
        Name = sectionName,
        ResourceClassId = resourceClassId,
        DisplayMode = new FieldDisplayMode?(mode)
      };
      parentElement.Add(this.Section);
      this.facadeInfo = new DefinitionFacadeInfo((Func<string>) (() => this.Section.ResourceClassId), (Func<FieldDisplayMode?>) (() => this.Section.DisplayMode));
      this.facadeInfo.ContentType = this.contentType;
      this.facadeInfo.DefinitionName = this.definitionName;
      this.facadeInfo.ModuleName = this.moduleName;
      this.facadeInfo.SectionName = this.sectionName;
      this.facadeInfo.ViewName = this.viewName;
      this.fields = new FieldControlsCollection<SectionDefinitionFacade<TParentFacade>>(this, this.Section.Fields, (IDefinitionFacadeInfo) this.facadeInfo);
    }

    /// <summary>Gets or sets the current section.</summary>
    /// <value>The section.</value>
    protected ContentViewSectionElement Section { get; set; }

    /// <inheritdoc />
    ConfigElementDictionary<string, FieldDefinitionElement> IHasFieldControls<SectionDefinitionFacade<TParentFacade>>.FieldsConfig => this.Section.Fields;

    /// <inheritdoc />
    IDefinitionFacadeInfo IHasFieldControls<SectionDefinitionFacade<TParentFacade>>.FacadeInfo => (IDefinitionFacadeInfo) this.facadeInfo;

    /// <summary>
    /// Gets this <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewSectionElement" /> instance.
    /// </summary>
    /// <returns></returns>
    public ContentViewSectionElement Get() => this.Section;

    /// <summary>
    /// Sets the object that defines the expandable behavior of the section control.
    /// </summary>
    /// <returns>An instance of the current <see cref="!:ExpandableControlDefinitionFacade&lt;SectionDefinitionFacade&lt;TParentFacade&gt;, ContentViewSectionElement&gt;" />.</returns>
    public ExpandableControlDefinitionFacade<SectionDefinitionFacade<TParentFacade>, ContentViewSectionElement> AddExpandableBehavior()
    {
      ExpandableControlDefinitionFacade<SectionDefinitionFacade<TParentFacade>, ContentViewSectionElement> definitionFacade = new ExpandableControlDefinitionFacade<SectionDefinitionFacade<TParentFacade>, ContentViewSectionElement>(this.moduleName, this.definitionName, this.contentType, this.Section, this.viewName, this, this.resourceClassId);
      this.Section.ExpandableDefinitionConfig = definitionFacade.Get();
      return definitionFacade;
    }

    /// <summary>Sets the section's CSS class.</summary>
    /// <param name="cssClass">The css class</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" />.</returns>
    public SectionDefinitionFacade<TParentFacade> SetCssClass(string cssClass)
    {
      this.Section.CssClass = !string.IsNullOrEmpty(cssClass) ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this;
    }

    /// <summary>
    /// Sets the <see cref="!:DisplayMode" />.
    /// </summary>
    /// <param name="cssClass">The mode</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" />.</returns>
    public SectionDefinitionFacade<TParentFacade> SetDisplayMode(
      FieldDisplayMode mode)
    {
      this.Section.DisplayMode = new FieldDisplayMode?(mode);
      return this;
    }

    /// <summary>Sets the ordinal position of the section.</summary>
    /// <param name="ordinal">The ordinal position.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" />.
    /// </returns>
    /// <value>The ordinal.</value>
    public SectionDefinitionFacade<TParentFacade> SetOrdinal(int ordinal)
    {
      this.Section.Ordinal = new int?(ordinal);
      return this;
    }

    /// <summary>
    /// Sets the localization class for the section that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// The type of the class which should be used to localize the string properties.
    /// </typeparam>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" />.</returns>
    public SectionDefinitionFacade<TParentFacade> LocalizeUsing<TResourceClass>() where TResourceClass : Resource
    {
      this.Section.ResourceClassId = typeof (TResourceClass).Name;
      return this;
    }

    /// <summary>
    /// Sets the localization class for the section that should be used
    /// for localizing the properties. When this method is called, all properties
    /// will start to behave as resource keys.
    /// </summary>
    /// <param name="resourceClassId">The resource class name.</param>
    /// <returns>
    /// An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" />.
    /// </returns>
    public SectionDefinitionFacade<TParentFacade> LocalizeUsing(
      string resourceClassId)
    {
      this.Section.ResourceClassId = !string.IsNullOrEmpty(resourceClassId) ? resourceClassId : throw new ArgumentNullException(nameof (resourceClassId));
      return this;
    }

    /// <summary>Sets the title of the section.</summary>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" />.</returns>
    public SectionDefinitionFacade<TParentFacade> SetTitle(string title)
    {
      this.Section.Title = !string.IsNullOrEmpty(title) ? title : throw new ArgumentNullException(nameof (title));
      return this;
    }

    /// <summary>
    /// Sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed.
    /// </summary>
    /// <param name="id">The control id.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" />.</returns>
    public SectionDefinitionFacade<TParentFacade> SetControlId(string id)
    {
      this.Section.ControlId = !string.IsNullOrEmpty(id) ? id : throw new ArgumentNullException(nameof (id));
      return this;
    }

    /// <summary>Sets the tag that will be rendered as a wrapper.</summary>
    /// <param name="tag">The wrapper tag.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.Definitions.SectionDefinitionFacade`1" />.</returns>
    public SectionDefinitionFacade<TParentFacade> SetWrapperTag(
      HtmlTextWriterTag tag)
    {
      this.Section.WrapperTag = tag;
      return this;
    }

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public TParentFacade Done() => this.parentFacade;

    /// <summary>
    /// Adds the text field (it is preferred to use AddLocalizedTextField for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    public TextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddTextField(
      string fieldName)
    {
      return this.fields.AddTextField(fieldName);
    }

    /// <summary>
    /// Adds text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    public TextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddTextField(
      string fieldName,
      string dataFieldName)
    {
      return this.fields.AddTextField(fieldName, dataFieldName);
    }

    /// <summary>
    /// Adds text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    public TextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddLocalizedTextField(
      string fieldName)
    {
      return this.fields.AddLocalizedTextField(fieldName);
    }

    /// <summary>
    /// Adds text field (it is preferred to use AddLocalizedTextFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddTextFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddTextFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    public SectionDefinitionFacade<TParentFacade> AddTextFieldAndContinue(
      string fieldName,
      string dataFieldName)
    {
      return this.fields.AddTextFieldAndContinue(fieldName, dataFieldName);
    }

    /// <summary>
    /// Adds text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddLocalizedTextFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddLocalizedTextFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds HTML field - use when binding to non-Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Child facade for current customization of the HTML field.</returns>
    public HtmlFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddHtmlField(
      string fieldName)
    {
      return this.fields.AddHtmlField(fieldName);
    }

    /// <summary>
    /// Adds HTML field - use when binding to Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    public HtmlFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddLocalizedHtmlField(
      string fieldName)
    {
      return this.fields.AddLocalizedHtmlField(fieldName);
    }

    /// <summary>
    /// Adds HTML field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    public HtmlFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddHtmlField(
      string fieldName,
      string dataFieldName)
    {
      return this.fields.AddHtmlField(fieldName, dataFieldName);
    }

    /// <summary>
    /// Adds HTML field - use when binding to non-Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddHtmlFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddHtmlFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds HTML field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    public SectionDefinitionFacade<TParentFacade> AddHtmlFieldAndContinue(
      string fieldName,
      string dataFieldName)
    {
      return this.fields.AddHtmlFieldAndContinue(fieldName, dataFieldName);
    }

    /// <summary>
    /// Adds HTML field - use when binding to Lstring properties
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    public SectionDefinitionFacade<TParentFacade> AddLocalizedHtmlFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddLocalizedHtmlFieldAndContinue(fieldName);
    }

    /// <summary>Add a field control for selecting language</summary>
    /// <param name="fieldName">Name of the field. Usually 'AvailableLanguages'</param>
    /// <returns>Child facade for customizing the language field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public LanguageChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddLanguageChoiceField(
      string fieldName)
    {
      return this.fields.AddLanguageChoiceField(fieldName);
    }

    /// <summary>Add a field control for selecting language</summary>
    /// <param name="fieldName">Name of the field. Usually 'AvailableLanguages'</param>
    /// <returns>Current facade (i.e. no further customization of the language field is required)</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public SectionDefinitionFacade<TParentFacade> AddLanguageChoiceFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddLanguageChoiceFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Add a choice field (e.g. drop-down, list of radio buttons, etc.)
    /// </summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddChoiceField(
      string fieldName)
    {
      return this.fields.AddChoiceField(fieldName);
    }

    /// <summary>
    /// Add a choice field and specify how it is going to be rendered
    /// </summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="renderAs">Specify how the control will be rendered</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddChoiceField(
      string fieldName,
      RenderChoicesAs renderAs)
    {
      return this.fields.AddChoiceField(fieldName, renderAs);
    }

    /// <summary>
    /// Add a choice field and specify the type of the control that will use this settings to render it
    /// </summary>
    /// <typeparam name="TFieldControl">Field control inheriting <c>ChoiceField</c> that will use this settings to render the field</typeparam>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddChoiceField<TFieldControl>(
      string fieldName)
      where TFieldControl : ChoiceField
    {
      return this.fields.AddChoiceField<TFieldControl>(fieldName);
    }

    /// <summary>
    /// Add a choice field and specify the type of the control that will use this settings to render it
    /// </summary>
    /// <typeparam name="TFieldControl">Field control inheriting <c>ChoiceField</c> that will use this settings to render the field</typeparam>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="renderAs">Specify how the control will be rendered</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddChoiceField<TFieldControl>(
      string fieldName,
      RenderChoicesAs renderAs)
      where TFieldControl : ChoiceField
    {
      return this.fields.AddChoiceField<TFieldControl>(fieldName, renderAs);
    }

    /// <summary>Add a single checkbox</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddSingleCheckboxField(
      string fieldName,
      string text)
    {
      return this.fields.AddSingleCheckboxField(fieldName, text);
    }

    /// <summary>Add a single checkbox</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <param name="value">Initial value</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddSingleCheckboxField(
      string fieldName,
      string text,
      bool value)
    {
      return this.fields.AddSingleCheckboxField(fieldName, text, value);
    }

    /// <summary>Add a single checkbox that is visually in a group</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddGroupSingleCheckobxField(
      string fieldName,
      string text)
    {
      return this.fields.AddGroupSingleCheckobxField(fieldName, text);
    }

    /// <summary>Add a single checkbox that is visually in a group</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <param name="value">Initial value</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddGroupSingleCheckobxField(
      string fieldName,
      string text,
      bool value)
    {
      return this.fields.AddGroupSingleCheckobxField(fieldName, text, value);
    }

    /// <summary>
    /// Adds the mirror text field (it is preferred to use AddLocalizedMirrorTextField for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    public MirrorTextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddMirrorTextField(
      string fieldName)
    {
      return this.fields.AddMirrorTextField(fieldName);
    }

    /// <summary>
    /// Adds mirror text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    public MirrorTextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddMirrorTextField(
      string fieldName,
      string dataFieldName)
    {
      return this.fields.AddMirrorTextField(fieldName, dataFieldName);
    }

    /// <summary>
    /// Adds the mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    public MirrorTextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddLocalizedMirrorTextField(
      string fieldName)
    {
      return this.fields.AddLocalizedMirrorTextField(fieldName);
    }

    /// <summary>
    /// Adds mirror text field (it is preferred to use AddLocalizedMirrorTextFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddMirrorTextFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddMirrorTextFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds mirror text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    public SectionDefinitionFacade<TParentFacade> AddMirrorTextFieldAndContinue(
      string fieldName,
      string dataFieldName)
    {
      return this.fields.AddMirrorTextFieldAndContinue(fieldName, dataFieldName);
    }

    /// <summary>
    /// Adds mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddLocalizedMirrorTextFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddLocalizedMirrorTextFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds url name mirror text field (it is preferred to use AddLocalizedUrlNameField for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddUrlNameField(
      string mirroredControlId,
      string title)
    {
      return this.fields.AddUrlNameField(mirroredControlId, title);
    }

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName (it is preferred to use AddLocalizedUrlNameField for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddUrlNameField(
      string mirroredControlId)
    {
      return this.fields.AddUrlNameField(mirroredControlId);
    }

    /// <summary>
    /// Adds the url name mirror field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddLocalizedUrlNameField(
      string mirroredControlId,
      string title)
    {
      return this.fields.AddLocalizedUrlNameField(mirroredControlId, title);
    }

    /// <summary>
    /// Adds the url name mirror field and sets title to UrlName - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddLocalizedUrlNameField(
      string mirroredControlId)
    {
      return this.fields.AddLocalizedUrlNameField(mirroredControlId);
    }

    /// <summary>
    /// Adds url name mirror text field (it is preferred to use AddLocalizedUrlNameFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddUrlNameFieldAndContinue(
      string mirroredControlId,
      string title)
    {
      return this.fields.AddUrlNameFieldAndContinue(mirroredControlId, title);
    }

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName
    /// (it is preferred to use AddLocalizedUrlNameFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddUrlNameFieldAndContinue(
      string mirroredControlId)
    {
      return this.fields.AddUrlNameFieldAndContinue(mirroredControlId);
    }

    /// <summary>
    /// Adds url name mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddLocalizedUrlNameFieldAndContinue(
      string mirroredControlId,
      string title)
    {
      return this.fields.AddLocalizedUrlNameFieldAndContinue(mirroredControlId, title);
    }

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddLocalizedUrlNameFieldAndContinue(
      string mirroredControlId)
    {
      return this.fields.AddLocalizedUrlNameFieldAndContinue(mirroredControlId);
    }

    /// <summary>
    /// Adds a field control that can contain other field controls when clicked (expanded)
    /// </summary>
    /// <param name="fieldName">Name of the field the control is bound to</param>
    /// <returns>Child facade for further customization</returns>
    public ExpandableFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddExpandableField(
      string fieldName)
    {
      return this.fields.AddExpandableField(fieldName);
    }

    /// <summary>
    /// Add a field control that adds an expandable field that allows the user to allow/disallow commenting and determine
    /// who can comment (e.g. all/registered users)
    /// </summary>
    /// <param name="allowCommentsFieldName">Boolean to bind to that allows/disallows posting comments</param>
    /// <param name="postRightsFieldName">
    /// Field of type <see cref="T:Telerik.Sitefinity.GenericContent.Model.PostRights" /> the field control will be bound to
    /// </param>
    /// <param name="approveCommentsFieldName">Boolean field to bind to that toggles comment approval</param>
    /// <returns>Child facade for further customization</returns>
    public ExpandableFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddAllowCommentsExpandableField(
      string allowCommentsFieldName,
      string postRightsFieldName,
      string approveCommentsFieldName)
    {
      return this.fields.AddAllowCommentsExpandableField(allowCommentsFieldName, postRightsFieldName, approveCommentsFieldName);
    }

    /// <summary>
    /// Add a field control that adds an expandable field that allows the user to allow/disallow commenting and determine
    /// who can comment (e.g. all/registered users)
    /// </summary>
    /// <returns>Child facade for further customization</returns>
    public ExpandableFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddAllowCommentsExpandableField() => this.fields.AddAllowCommentsExpandableField();

    /// <summary>
    /// Add a field control that adds an expandable field that allows the user to allow/disallow commenting and determine
    /// who can comment (e.g. all/registered users)
    /// </summary>
    /// <returns>Current facade</returns>
    public SectionDefinitionFacade<TParentFacade> AddAllowCommentsExpandableFieldAndContinue() => this.fields.AddAllowCommentsExpandableFieldAndContinue();

    /// <summary>
    /// Add a field control that adds an expandable field that allows the user to allow/disallow commenting and determine
    /// who can comment (e.g. all/registered users)
    /// </summary>
    /// <param name="allowCommentsFieldName">Boolean to bind to that allows/disallows posting comments</param>
    /// <param name="postRightsFieldName">
    /// Field of type <see cref="T:Telerik.Sitefinity.GenericContent.Model.PostRights" /> the field will be bound to
    /// </param>
    /// <param name="approveCommentsFieldName">Boolean field to bind to that toggles comment approval</param>
    /// <returns>Current facade</returns>
    public SectionDefinitionFacade<TParentFacade> AddAllowCommentsExpandableFieldAndContinue(
      string allowCommentsFieldName,
      string postRightsFieldName,
      string approveCommentsFieldName)
    {
      return this.fields.AddAllowCommentsExpandableFieldAndContinue(allowCommentsFieldName, postRightsFieldName, approveCommentsFieldName);
    }

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <param name="allowMultipleUrlsFieldName">Name of a boolean field to bind to that allows/disallows having multiple URLs</param>
    /// <param name="additiionalUrlNamesFieldName">Name of a string array field to bind to that contains the additional url names</param>
    /// <param name="additionalUrlsRedirectToDefaultFieldName">Name of a boolean field to bind to that defines whether the additional URLs for this item redirect to it or do a rewrite</param>
    /// <param name="defaultUrlFieldName">Name of a string field to bind to that stores the default url</param>
    /// <returns>Child facade for furhter customization</returns>
    /// <exception cref="T:System.ArgumentNullException">When either of the arguments is <c>null</c></exception>
    public ExpandableFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddMultipleUrlsExpandableField(
      string allowMultipleUrlsFieldName,
      string additiionalUrlNamesFieldName,
      string additionalUrlsRedirectToDefaultFieldName,
      string defaultUrlFieldName)
    {
      return this.fields.AddMultipleUrlsExpandableField(allowMultipleUrlsFieldName, additiionalUrlNamesFieldName, additionalUrlsRedirectToDefaultFieldName, defaultUrlFieldName);
    }

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <returns>Child facade for furhter customization</returns>
    public ExpandableFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddMultipleUrlsExpandableField() => this.fields.AddMultipleUrlsExpandableField();

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <returns>Current facade</returns>
    public SectionDefinitionFacade<TParentFacade> AddMultipleUrlsExpandableFieldAndContinue() => this.fields.AddMultipleUrlsExpandableFieldAndContinue();

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <param name="allowMultipleUrlsFieldName">Name of a boolean field to bind to that allows/disallows having multiple URLs</param>
    /// <param name="additiionalUrlNamesFieldName">Name of a string array field to bind to that contains the additional url names</param>
    /// <param name="additionalUrlsRedirectToDefaultFieldName">Name of a boolean field to bind to that defines whether the additional URLs for this item redirect to it or do a rewrite</param>
    /// <param name="defaultUrlFieldName">Name of a string field to bind to that stores the default url</param>
    /// <returns>Child facade for furhter customization</returns>
    /// <exception cref="T:System.ArgumentNullException">When either of the arguments is <c>null</c></exception>
    public SectionDefinitionFacade<TParentFacade> AddMultipleUrlsExpandableFieldAndContinue(
      string allowMultipleUrlsFieldName,
      string additiionalUrlNamesFieldName,
      string additionalUrlsRedirectToDefaultFieldName,
      string defaultUrlFieldName)
    {
      return this.fields.AddMultipleUrlsExpandableFieldAndContinue(allowMultipleUrlsFieldName, additiionalUrlNamesFieldName, additionalUrlsRedirectToDefaultFieldName, defaultUrlFieldName);
    }

    /// <summary>Adds the version note control.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the version note control.</returns>
    public VersionNoteDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddVersionNoteControl(
      string fieldName)
    {
      return this.fields.AddVersionNoteControl(fieldName);
    }

    /// <summary>
    /// Adds the version note control and sets FieldName to "Comment".
    /// </summary>
    /// <returns>Child facade for further customization of the version note control.</returns>
    public VersionNoteDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddVersionNoteControl() => this.fields.AddVersionNoteControl();

    /// <summary>Adds the version note control.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddVersionNoteControlAndContinue(
      string fieldName)
    {
      return this.fields.AddVersionNoteControlAndContinue(fieldName);
    }

    /// <summary>
    /// Adds the version note control and sets FieldName to "Comment".
    /// </summary>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddVersionNoteControlAndContinue() => this.fields.AddVersionNoteControlAndContinue();

    /// <summary>Adds the workflow status info field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    public WorkflowStatusInfoDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddWorkflowStatusInfoField(
      string fieldName)
    {
      return this.fields.AddWorkflowStatusInfoField(fieldName);
    }

    /// <summary>
    /// Adds the workflow status info field and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    public WorkflowStatusInfoDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddWorkflowStatusInfoField() => this.fields.AddWorkflowStatusInfoField();

    /// <summary>Adds the workflow status info field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddWorkflowStatusInfoFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddWorkflowStatusInfoFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds the workflow status info field and and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddWorkflowStatusInfoFieldAndContinue() => this.fields.AddWorkflowStatusInfoFieldAndContinue();

    /// <summary>Adds the workflow status info field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    public StatisticsDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddContentStatsField(
      string fieldName)
    {
      return this.fields.AddStatisticsField(fieldName);
    }

    /// <summary>
    /// Adds the workflow status info field and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    public StatisticsDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddContentStatsField() => this.fields.AddStatisticsField();

    /// <summary>Adds the workflow status info field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddContentStatsFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddStatisticsFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds the workflow status info field and and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddContentStatsFieldAndContinue() => this.fields.AddStatisticsFieldAndContinue();

    /// <summary>Adds the workflow status info field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    public ContentLocationInfoDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddContentLocationInfoField(
      string fieldName)
    {
      return this.fields.AddContentLocationInfoField(fieldName);
    }

    /// <summary>
    /// Adds the workflow status info field and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    public ContentLocationInfoDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddContentLocationInfoField() => this.fields.AddContentLocationInfoField();

    /// <summary>Adds the workflow status info field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddContentLocationInfoFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddContentLocationInfoFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds the workflow status info field and and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddContentLocationInfoFieldAndContinue() => this.fields.AddContentLocationInfoFieldAndContinue();

    /// <summary>Adds the relating data field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the relating data field.</returns>
    public RelatingDataFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddRelatingDataField(
      string fieldName)
    {
      return this.fields.AddRelatingDataField(fieldName);
    }

    /// <summary>
    /// Adds the relating data field and sets FieldName to "RelatingDataField".
    /// </summary>
    /// <returns>Child facade for further customization of the relating data field.</returns>
    public RelatingDataFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddRelatingDataField() => this.fields.AddRelatingDataField();

    /// <summary>Adds the relating data field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddRelatingDataFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddRelatingDataFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds the relating data field and and sets FieldName to "RelatingDataField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddRelatingDataFieldAndContinue() => this.fields.AddRelatingDataFieldAndContinue();

    /// <summary>
    /// Adds the language list field with all defaults set. Field name is set as default to "AvailableLanguages"
    /// </summary>
    /// <returns>The facade for the LanguageListField</returns>
    public LanguageListFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddLanguageListField() => this.fields.AddLanguageListField();

    /// <summary>Adds the language list field with all defaults set.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>The facade for the LanguageListField</returns>
    public LanguageListFieldDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddLanguageListField(
      string fieldName)
    {
      return this.fields.AddLanguageListField(fieldName);
    }

    /// <summary>Adds the warning field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the warning field.</returns>
    public WarningDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddWarningField(
      string fieldName)
    {
      return this.fields.AddWarningField(fieldName);
    }

    /// <summary>
    /// Adds the warning field and sets FieldName to "WarningField".
    /// </summary>
    /// <returns>Child facade for further customization of the warning field.</returns>
    public WarningDefinitionFacade<SectionDefinitionFacade<TParentFacade>> AddWarningField() => this.fields.AddWarningField();

    /// <summary>Adds the warning field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddWarningFieldAndContinue(
      string fieldName)
    {
      return this.fields.AddWarningFieldAndContinue(fieldName);
    }

    /// <summary>
    /// Adds the warning field and and sets FieldName to "WarningField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public SectionDefinitionFacade<TParentFacade> AddWarningFieldAndContinue() => this.fields.AddWarningFieldAndContinue();

    /// <summary>
    /// Raises ArgumentNulLException if <paramref name="val" /> is null
    /// </summary>
    /// <typeparam name="TReference">Reference type</typeparam>
    /// <param name="paramName">Name of the parameter (exception info)</param>
    /// <param name="val">Value to test</param>
    /// <exception cref="!:ArgumentNulLException">When <paramref name="val" /> is null</exception>
    protected void AssertNotNull<TReference>(string paramName, TReference val) where TReference : class
    {
      if ((object) val == null)
        throw new ArgumentNullException(paramName);
    }

    /// <summary>
    /// Raises ArgumentNulLException if <paramref name="val" /> is null or empty
    /// </summary>
    /// <param name="paramName">Name of the parameter (exception info)</param>
    /// <param name="val">Value to test</param>
    /// <exception cref="!:ArgumentNulLException">When <paramref name="val" /> is null or empty</exception>
    protected void AssertNotNullOrEmpty(string paramName, string val)
    {
      if (string.IsNullOrEmpty(val))
        throw new ArgumentNullException(paramName);
    }
  }
}
