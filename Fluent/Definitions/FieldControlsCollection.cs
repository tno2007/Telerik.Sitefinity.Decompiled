// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.FieldControlsCollection`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>Default implementation of IHasFieldControls.</summary>
  /// <typeparam name="TActualFacade">Type of the facade implementing <c>IHasFieldControls</c></typeparam>
  public class FieldControlsCollection<TActualFacade> : IHasFieldControls<TActualFacade>
    where TActualFacade : class
  {
    private TActualFacade actualFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.FieldControlsCollection`1" /> class.
    /// </summary>
    /// <param name="actualFacade">Actual facade.</param>
    /// <param name="fieldsConfig">The fields config.</param>
    /// <param name="facadeInfo">The facade info.</param>
    public FieldControlsCollection(
      TActualFacade actualFacade,
      ConfigElementDictionary<string, FieldDefinitionElement> fieldsConfig,
      IDefinitionFacadeInfo facadeInfo)
    {
      FieldControlsCollection<TActualFacade>.AssertNotNull<TActualFacade>(nameof (actualFacade), actualFacade);
      FieldControlsCollection<TActualFacade>.AssertNotNull<ConfigElementDictionary<string, FieldDefinitionElement>>(nameof (fieldsConfig), fieldsConfig);
      FieldControlsCollection<TActualFacade>.AssertNotNull<IDefinitionFacadeInfo>(nameof (facadeInfo), facadeInfo);
      this.FieldsConfig = fieldsConfig;
      this.FacadeInfo = facadeInfo;
      this.actualFacade = actualFacade;
    }

    /// <inheritdoc />
    public ConfigElementDictionary<string, FieldDefinitionElement> FieldsConfig { get; private set; }

    /// <inheritdoc />
    public IDefinitionFacadeInfo FacadeInfo { get; private set; }

    /// <summary>
    /// Raises ArgumentNulLException if <paramref name="val" /> is null
    /// </summary>
    /// <typeparam name="TReference">Reference type</typeparam>
    /// <param name="paramName">Name of the parameter (exception info)</param>
    /// <param name="val">Value to test</param>
    /// <exception cref="!:ArgumentNulLException">When <paramref name="val" /> is null</exception>
    private static void AssertNotNull<TReference>(string paramName, TReference val) where TReference : class
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
    private static void AssertNotNullOrEmpty(string paramName, string val)
    {
      if (string.IsNullOrEmpty(val))
        throw new ArgumentNullException(paramName);
    }

    /// <summary>
    /// Adds the text field (it is preferred to use AddLocalizedTextField for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    public TextFieldDefinitionFacade<TActualFacade> AddTextField(
      string fieldName)
    {
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (fieldName), fieldName);
      return new TextFieldDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value).SetDataFieldName(fieldName);
    }

    /// <summary>
    /// Adds text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    public TextFieldDefinitionFacade<TActualFacade> AddTextField(
      string fieldName,
      string dataFieldName)
    {
      return this.AddTextField(fieldName).SetDataFieldName(dataFieldName);
    }

    /// <summary>
    /// Adds text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    public TextFieldDefinitionFacade<TActualFacade> AddLocalizedTextField(
      string fieldName)
    {
      return this.AddTextField(fieldName).SetLocalizableDataFieldName(fieldName);
    }

    /// <summary>
    /// Adds text field (it is preferred to use AddLocalizedTextFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddTextFieldAndContinue(string fieldName) => this.AddTextField(fieldName).Done();

    /// <summary>
    /// Adds text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddTextFieldAndContinue(string fieldName, string dataFieldName) => this.AddTextField(fieldName, dataFieldName).Done();

    /// <summary>
    /// Adds text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddLocalizedTextFieldAndContinue(string fieldName) => this.AddLocalizedTextField(fieldName).Done();

    /// <summary>
    /// Adds HTML field - use when binding to non-Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Child facade for current customization of the HTML field.</returns>
    public HtmlFieldDefinitionFacade<TActualFacade> AddHtmlField(
      string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      return new HtmlFieldDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value).SetDataFieldName(fieldName);
    }

    /// <summary>
    /// Adds HTML field - use when binding to Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    public HtmlFieldDefinitionFacade<TActualFacade> AddLocalizedHtmlField(
      string fieldName)
    {
      return this.AddHtmlField(fieldName).SetLocalizableDataFieldName(fieldName);
    }

    /// <summary>
    /// Adds HTML field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    public HtmlFieldDefinitionFacade<TActualFacade> AddHtmlField(
      string fieldName,
      string dataFieldName)
    {
      return this.AddHtmlField(fieldName).SetDataFieldName(dataFieldName);
    }

    /// <summary>
    /// Adds HTML field - use when binding to non-Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddHtmlFieldAndContinue(string fieldName) => this.AddHtmlField(fieldName).Done();

    /// <summary>
    /// Adds HTML field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddHtmlFieldAndContinue(string fieldName, string dataFieldName) => this.AddHtmlField(fieldName, dataFieldName).Done();

    /// <summary>
    /// Adds HTML field - use when binding to Lstring properties
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    public TActualFacade AddLocalizedHtmlFieldAndContinue(string fieldName) => this.AddLocalizedHtmlField(fieldName).Done();

    /// <summary>Add a field control for selecting language</summary>
    /// <param name="fieldName">Name of the field. Usually 'AvailableLanguages'</param>
    /// <returns>Child facade for customizing the language field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public LanguageChoiceFieldDefinitionFacade<TActualFacade> AddLanguageChoiceField(
      string fieldName)
    {
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (fieldName), fieldName);
      LanguageChoiceFieldDefinitionFacade<TActualFacade> definitionFacade = new LanguageChoiceFieldDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, "languageField", this.FacadeInfo.ResourceClassId, FieldDisplayMode.Write);
      definitionFacade.SetDataFieldName(fieldName);
      return definitionFacade;
    }

    /// <summary>Add a field control for selecting language</summary>
    /// <param name="fieldName">Name of the field. Usually 'AvailableLanguages'</param>
    /// <returns>Current facade (i.e. no further customization of the language field is required)</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public TActualFacade AddLanguageChoiceFieldAndContinue(string fieldName) => this.AddLanguageChoiceField(fieldName).Done();

    /// <summary>
    /// Add a choice field (e.g. drop-down, list of radio buttons, etc.)
    /// </summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<TActualFacade> AddChoiceField(
      string fieldName)
    {
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (fieldName), fieldName);
      ChoiceFieldDefinitionFacade<TActualFacade> definitionFacade = new ChoiceFieldDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value);
      definitionFacade.SetDataFieldName(fieldName);
      return definitionFacade;
    }

    /// <summary>
    /// Add a choice field and specify how it is going to be rendered
    /// </summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="renderAs">Specify how the control will be rendered</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<TActualFacade> AddChoiceField(
      string fieldName,
      RenderChoicesAs renderAs)
    {
      return this.AddChoiceField(fieldName).SetRenderChoiceAs(renderAs);
    }

    /// <summary>
    /// Add a choice field and specify the type of the control that will use this settings to render it
    /// </summary>
    /// <typeparam name="TFieldControl">Field control inheriting <c>ChoiceField</c> that will use this settings to render the field</typeparam>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<TActualFacade> AddChoiceField<TFieldControl>(
      string fieldName)
      where TFieldControl : ChoiceField
    {
      return this.AddChoiceField(fieldName).SetFieldType<TFieldControl>();
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
    public ChoiceFieldDefinitionFacade<TActualFacade> AddChoiceField<TFieldControl>(
      string fieldName,
      RenderChoicesAs renderAs)
      where TFieldControl : ChoiceField
    {
      return this.AddChoiceField(fieldName, renderAs).SetFieldType<TFieldControl>();
    }

    /// <summary>Add a single checkbox</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<TActualFacade> AddSingleCheckboxField(
      string fieldName,
      string text)
    {
      return this.AddChoiceField(fieldName, RenderChoicesAs.SingleCheckBox).SetWrapperTag(HtmlTextWriterTag.Li).SetCssClass("sfCheckBox sfFormSeparator").AddChoiceAndContinue(text);
    }

    /// <summary>Add a single checkbox</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <param name="value">Initial value</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<TActualFacade> AddSingleCheckboxField(
      string fieldName,
      string text,
      bool value)
    {
      return this.AddChoiceField(fieldName, RenderChoicesAs.SingleCheckBox).SetWrapperTag(HtmlTextWriterTag.Li).SetCssClass("sfCheckBox sfFormSeparator").AddChoiceAndContinue(text, value.ToString());
    }

    /// <summary>Add a single checkbox that is visually in a group</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<TActualFacade> AddGroupSingleCheckobxField(
      string fieldName,
      string text)
    {
      return this.AddSingleCheckboxField(fieldName, text).SetCssClass("sfCheckBox sfInGroup").MakeMutuallyExclusive();
    }

    /// <summary>Add a single checkbox that is visually in a group</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <param name="value">Initial value</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    public ChoiceFieldDefinitionFacade<TActualFacade> AddGroupSingleCheckobxField(
      string fieldName,
      string text,
      bool value)
    {
      return this.AddSingleCheckboxField(fieldName, text, value).SetCssClass("sfCheckBox sfInGroup").MakeMutuallyExclusive();
    }

    /// <summary>
    /// Adds the mirror text field (it is preferred to use AddLocalizedMirrorTextField for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    public MirrorTextFieldDefinitionFacade<TActualFacade> AddMirrorTextField(
      string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      return new MirrorTextFieldDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value).SetDataFieldName(fieldName);
    }

    /// <summary>
    /// Adds mirror text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    public MirrorTextFieldDefinitionFacade<TActualFacade> AddMirrorTextField(
      string fieldName,
      string dataFieldName)
    {
      return this.AddMirrorTextField(fieldName).SetDataFieldName(dataFieldName);
    }

    /// <summary>
    /// Adds the mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    public MirrorTextFieldDefinitionFacade<TActualFacade> AddLocalizedMirrorTextField(
      string fieldName)
    {
      return this.AddMirrorTextField(fieldName).SetLocalizableDataFieldName(fieldName);
    }

    /// <summary>
    /// Adds mirror text field (it is preferred to use AddLocalizedMirrorTextFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddMirrorTextFieldAndContinue(string fieldName) => this.AddMirrorTextField(fieldName).Done();

    /// <summary>
    /// Adds mirror text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    public TActualFacade AddMirrorTextFieldAndContinue(string fieldName, string dataFieldName) => this.AddMirrorTextField(fieldName, dataFieldName).Done();

    /// <summary>
    /// Adds mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddLocalizedMirrorTextFieldAndContinue(string fieldName) => this.AddLocalizedMirrorTextField(fieldName).Done();

    /// <summary>
    /// Adds url name mirror text field (it is preferred to use AddLocalizedUrlNameField for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TActualFacade> AddUrlNameField(
      string mirroredControlId,
      string title)
    {
      if (string.IsNullOrEmpty(mirroredControlId))
        throw new ArgumentNullException(nameof (mirroredControlId));
      return this.AddMirrorTextField("UrlName").SetTitle(title).SetId("urlName").SetMirroredControlId(mirroredControlId).AddValidation().MakeRequired().SetRequiredViolationMessage("UrlNameCannotBeEmpty").SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForContentValidator).SetRegularExpressionViolationMessage("UrlNameInvalidSymbols").Done();
    }

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName (it is preferred to use AddLocalizedUrlNameField for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TActualFacade> AddUrlNameField(
      string mirroredControlId)
    {
      return this.AddUrlNameField(mirroredControlId, "UrlName");
    }

    /// <summary>
    /// Adds the url name mirror field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TActualFacade> AddLocalizedUrlNameField(
      string mirroredControlId,
      string title)
    {
      return this.AddUrlNameField(mirroredControlId, title).SetLocalizableDataFieldName("UrlName");
    }

    /// <summary>
    /// Adds the url name mirror field and sets title to UrlName - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    public MirrorTextFieldDefinitionFacade<TActualFacade> AddLocalizedUrlNameField(
      string mirroredControlId)
    {
      return this.AddLocalizedUrlNameField(mirroredControlId, "UrlName");
    }

    /// <summary>
    /// Adds url name mirror text field (it is preferred to use AddLocalizedUrlNameFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddUrlNameFieldAndContinue(string mirroredControlId, string title) => this.AddUrlNameField(mirroredControlId, title).Done();

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName
    /// (it is preferred to use AddLocalizedUrlNameFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddUrlNameFieldAndContinue(string mirroredControlId) => this.AddUrlNameField(mirroredControlId).Done();

    /// <summary>
    /// Adds url name mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddLocalizedUrlNameFieldAndContinue(string mirroredControlId, string title) => this.AddLocalizedUrlNameField(mirroredControlId, title).Done();

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddLocalizedUrlNameFieldAndContinue(string mirroredControlId) => this.AddLocalizedUrlNameField(mirroredControlId).Done();

    /// <summary>
    /// Adds a field control that can contain other field controls when clicked (expanded)
    /// </summary>
    /// <param name="fieldName">Name of the field the control is bound to</param>
    /// <returns>Child facade for further customization</returns>
    public ExpandableFieldDefinitionFacade<TActualFacade> AddExpandableField(
      string fieldName)
    {
      ExpandableFieldDefinitionFacade<TActualFacade> definitionFacade = new ExpandableFieldDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value);
      definitionFacade.SetDataFieldName(fieldName);
      return definitionFacade;
    }

    /// <summary>
    /// Add a field control that adds an expandable field that allows the user to allow/disallow commenting and determine
    /// who can comment (e.g. all/registered users)
    /// </summary>
    /// <returns>Child facade for further customization</returns>
    public ExpandableFieldDefinitionFacade<TActualFacade> AddAllowCommentsExpandableField() => this.AddAllowCommentsExpandableField("AllowComments", "PostRights", "ApproveComments");

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
    public ExpandableFieldDefinitionFacade<TActualFacade> AddAllowCommentsExpandableField(
      string allowCommentsFieldName,
      string postRightsFieldName,
      string approveCommentsFieldName)
    {
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (allowCommentsFieldName), allowCommentsFieldName);
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (postRightsFieldName), postRightsFieldName);
      return this.AddExpandableField(allowCommentsFieldName).SetId("commentsSettingsExpandableField").DefineToggleControl(allowCommentsFieldName, "AllowComments").SetId("allowCommentsField").Done().AddChoiceField(postRightsFieldName, RenderChoicesAs.RadioButtons).SetId("whoCanPostCommentsField").SetTitle("WhoCanPostComments").MakeMutuallyExclusive().SetCssClass("sfRadioList sfDependentGroup").AddChoiceAndContinue("Anyone", 1.ToString()).AddChoiceAndContinue("OnlyRegisteredUsers", 2.ToString()).Done().AddSingleCheckboxField(approveCommentsFieldName, "CommentsApproved").SetId("commentsApprovedField").SetCssClass("sfCheckBox sfInGroup sfDependentGroup").Done();
    }

    /// <summary>
    /// Add a field control that adds an expandable field that allows the user to allow/disallow commenting and determine
    /// who can comment (e.g. all/registered users)
    /// </summary>
    /// <returns>Current facade</returns>
    public TActualFacade AddAllowCommentsExpandableFieldAndContinue() => this.AddAllowCommentsExpandableField().Done();

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
    public TActualFacade AddAllowCommentsExpandableFieldAndContinue(
      string allowCommentsFieldName,
      string postRightsFieldName,
      string approveCommentsFieldName)
    {
      return this.AddAllowCommentsExpandableField(allowCommentsFieldName, postRightsFieldName, approveCommentsFieldName).Done();
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
    public ExpandableFieldDefinitionFacade<TActualFacade> AddMultipleUrlsExpandableField(
      string allowMultipleUrlsFieldName,
      string additiionalUrlNamesFieldName,
      string additionalUrlsRedirectToDefaultFieldName,
      string defaultUrlFieldName)
    {
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (allowMultipleUrlsFieldName), allowMultipleUrlsFieldName);
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (additiionalUrlNamesFieldName), additiionalUrlNamesFieldName);
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (additionalUrlsRedirectToDefaultFieldName), additionalUrlsRedirectToDefaultFieldName);
      FieldControlsCollection<TActualFacade>.AssertNotNullOrEmpty(nameof (defaultUrlFieldName), defaultUrlFieldName);
      return this.AddExpandableField("multipleUrlsExpandableField").SetId("multipleUrlsExpandableField").SetDataFieldName(allowMultipleUrlsFieldName).SetDisplayMode(FieldDisplayMode.Write).LocalizeUsing<ContentResources>().DefineToggleControl(allowMultipleUrlsFieldName, "AllowMultipleURLsForThisItem", false).SetId("allowMultipleUrlsFieldElement").Done().AddTextField("multipleUrlsField").SetId("multipleUrlsField").SetDataFieldName(additiionalUrlNamesFieldName).SetFieldType<MultilineTextField>().SetTitle("AdditionalUrlsOnePerLine").SetRows(5).SetCssClass("sfDependentGroup sfInGroup sfFirstInDependentGroup").AddExpandableBehaviorAndContinue().AddValidation().LocalizeUsingDefaultErrorMessages().SetRegularExpression(DefinitionsHelper.UrlRegularExpressionFilterForAdditionalContentUrlsValidator).SetMessageCssClass("sfError").Done().Done().AddSingleCheckboxField(additionalUrlsRedirectToDefaultFieldName, "AllAditionalUrlsRedirectoToTheDefaultOne", true).SetId("redirectToDefaultUrlChoiceFieldDefinition").MakeMutuallyExclusive().SetCssClass("sfDependentGroup sfInGroup").Done().AddTextField("defaultUrlNameTextField").SetDataFieldName(defaultUrlFieldName).SetId("defaultUrlNameTextField").SetCssClass("sfDependentGroup sfInGroup").SetDisplayMode(FieldDisplayMode.Read).Done();
    }

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <returns>Child facade for furhter customization</returns>
    public ExpandableFieldDefinitionFacade<TActualFacade> AddMultipleUrlsExpandableField() => this.AddMultipleUrlsExpandableField("$Context.AllowMultipleUrls", "$Context.AdditionalUrlNames", "$Context.AdditionalUrlsRedirectToDefault", "$Context.DefaultUrl");

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <returns>Current facade</returns>
    public TActualFacade AddMultipleUrlsExpandableFieldAndContinue() => this.AddMultipleUrlsExpandableField().Done();

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <param name="allowMultipleUrlsFieldName">Name of a boolean field to bind to that allows/disallows having multiple URLs</param>
    /// <param name="additiionalUrlNamesFieldName">Name of a string array field to bind to that contains the additional url names</param>
    /// <param name="additionalUrlsRedirectToDefaultFieldName">Name of a boolean field to bind to that defines whether the additional URLs for this item redirect to it or do a rewrite</param>
    /// <param name="defaultUrlFieldName">Name of a string field to bind to that stores the default url</param>
    /// <returns>Child facade for furhter customization</returns>
    /// <exception cref="T:System.ArgumentNullException">When either of the arguments is <c>null</c></exception>
    public TActualFacade AddMultipleUrlsExpandableFieldAndContinue(
      string allowMultipleUrlsFieldName,
      string additiionalUrlNamesFieldName,
      string additionalUrlsRedirectToDefaultFieldName,
      string defaultUrlFieldName)
    {
      return this.AddMultipleUrlsExpandableField(allowMultipleUrlsFieldName, additiionalUrlNamesFieldName, additionalUrlsRedirectToDefaultFieldName, defaultUrlFieldName).Done();
    }

    /// <summary>Adds the version note control.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the version note control.</returns>
    public VersionNoteDefinitionFacade<TActualFacade> AddVersionNoteControl(
      string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      return new VersionNoteDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value);
    }

    /// <summary>
    /// Adds the version note control and sets FieldName to "Comment".
    /// </summary>
    /// <returns>Child facade for further customization of the version note control.</returns>
    public VersionNoteDefinitionFacade<TActualFacade> AddVersionNoteControl() => this.AddVersionNoteControl("Comment");

    /// <summary>Adds the version note control.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    public TActualFacade AddVersionNoteControlAndContinue(string fieldName) => this.AddVersionNoteControl(fieldName).Done();

    /// <summary>
    /// Adds the version note control and sets FieldName to "Comment".
    /// </summary>
    /// <returns>Current facade.</returns>
    public TActualFacade AddVersionNoteControlAndContinue() => this.AddVersionNoteControl().Done();

    /// <summary>Adds the workflow status info field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the version note control.</returns>
    public WorkflowStatusInfoDefinitionFacade<TActualFacade> AddWorkflowStatusInfoField(
      string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      return new WorkflowStatusInfoDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value);
    }

    /// <summary>
    /// Adds the Adds the workflow status info field and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    public WorkflowStatusInfoDefinitionFacade<TActualFacade> AddWorkflowStatusInfoField() => this.AddWorkflowStatusInfoField("ContentWorkflowStatusInfoField");

    /// <summary>Adds the workflow status info field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public TActualFacade AddWorkflowStatusInfoFieldAndContinue(string fieldName) => this.AddWorkflowStatusInfoField(fieldName).Done();

    /// <summary>
    /// Adds the workflow status info field and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public TActualFacade AddWorkflowStatusInfoFieldAndContinue() => this.AddWorkflowStatusInfoField().Done();

    /// <summary>Adds the content location info field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the version note control.</returns>
    public ContentLocationInfoDefinitionFacade<TActualFacade> AddContentLocationInfoField(
      string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      return new ContentLocationInfoDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value);
    }

    /// <summary>
    /// Adds the Adds the content location info field and sets FieldName to "ContentLocationInfoField".
    /// </summary>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    public ContentLocationInfoDefinitionFacade<TActualFacade> AddContentLocationInfoField() => this.AddContentLocationInfoField("ContentLocationInfoField");

    /// <summary>Adds the content location info field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public TActualFacade AddContentLocationInfoFieldAndContinue(string fieldName) => this.AddContentLocationInfoField(fieldName).Done();

    /// <summary>
    /// Adds the content location info field and sets FieldName to "ContentLocationInfoField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public TActualFacade AddContentLocationInfoFieldAndContinue() => this.AddContentLocationInfoField().Done();

    /// <summary>Adds the relating data field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the relating data field.</returns>
    public RelatingDataFieldDefinitionFacade<TActualFacade> AddRelatingDataField(
      string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      return new RelatingDataFieldDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value);
    }

    /// <summary>
    /// Adds the relating data field and sets FieldName to "RelatingDataField".
    /// </summary>
    /// <returns>Child facade for further customization of the relating data field.</returns>
    public RelatingDataFieldDefinitionFacade<TActualFacade> AddRelatingDataField() => this.AddRelatingDataField("RelatingDataField");

    /// <summary>Adds the relating data field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public TActualFacade AddRelatingDataFieldAndContinue(string fieldName) => this.AddRelatingDataField(fieldName).Done();

    /// <summary>
    /// Adds the relating data field and sets FieldName to "RelatingDataField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public TActualFacade AddRelatingDataFieldAndContinue() => this.AddRelatingDataField().Done();

    /// <summary>Adds the warning field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the version note control.</returns>
    public WarningDefinitionFacade<TActualFacade> AddWarningField(
      string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      return new WarningDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value);
    }

    /// <summary>
    /// Adds the warning field and sets FieldName to "WarningField".
    /// </summary>
    /// <returns>Child facade for further customization of the warning field.</returns>
    public WarningDefinitionFacade<TActualFacade> AddWarningField() => this.AddWarningField("WarningField");

    /// <summary>Adds the warning field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public TActualFacade AddWarningFieldAndContinue(string fieldName) => this.AddWarningField(fieldName).Done();

    /// <summary>
    /// Adds the warning field and sets FieldName to "WarningField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public TActualFacade AddWarningFieldAndContinue() => this.AddWarningField().Done();

    public LanguageListFieldDefinitionFacade<TActualFacade> AddLanguageListField() => this.AddLanguageListField("languageListField");

    public LanguageListFieldDefinitionFacade<TActualFacade> AddLanguageListField(
      string fieldName)
    {
      return new LanguageListFieldDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, typeof (LocalizationResources).Name, this.FacadeInfo.FieldDisplayMode.Value).SetTitle("OtherTranslationsColon").SetFieldType(typeof (LanguageListField)).SetDataFieldName("AvailableLanguages").SetId("languageChoiceField");
    }

    /// <summary>Adds the statistics field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the statistics control.</returns>
    public StatisticsDefinitionFacade<TActualFacade> AddStatisticsField(
      string fieldName)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      return new StatisticsDefinitionFacade<TActualFacade>(this.FacadeInfo.ModuleName, this.FacadeInfo.DefinitionName, this.FacadeInfo.ContentType, this.FieldsConfig, this.FacadeInfo.ViewName, this.FacadeInfo.SectionName, this.actualFacade, fieldName, this.FacadeInfo.ResourceClassId, this.FacadeInfo.FieldDisplayMode.Value);
    }

    /// <summary>
    /// Adds the statistics control and sets FieldName to "ContentStatisticsField".
    /// </summary>
    /// <returns>Child facade for further customization of the statistics control.</returns>
    public StatisticsDefinitionFacade<TActualFacade> AddStatisticsField() => this.AddStatisticsField("ContentStatisticsField");

    /// <summary>Adds the statistics field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public TActualFacade AddStatisticsFieldAndContinue(string fieldName) => this.AddStatisticsField(fieldName).Done();

    /// <summary>
    /// Adds the statistics field and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Current facade.</returns>
    public TActualFacade AddStatisticsFieldAndContinue() => this.AddStatisticsField().Done();
  }
}
