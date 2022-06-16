// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.IHasFieldControls`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent.Definitions.Fields;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Intefface for facades that are capable of holding a collection of field controls
  /// </summary>
  /// <typeparam name="TActualFacade">Type of the facade implementing this interface</typeparam>
  public interface IHasFieldControls<TActualFacade> where TActualFacade : class
  {
    /// <summary>
    /// Configuration elemnent that holds the collection of field control configuration elements
    /// </summary>
    ConfigElementDictionary<string, FieldDefinitionElement> FieldsConfig { get; }

    /// <summary>
    /// General info about the facade implementing this iterface. This information is passed down to field controls
    /// </summary>
    IDefinitionFacadeInfo FacadeInfo { get; }

    /// <summary>
    /// Adds the text field (it is preferred to use AddLocalizedTextField for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    TextFieldDefinitionFacade<TActualFacade> AddTextField(string fieldName);

    /// <summary>
    /// Adds text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    TextFieldDefinitionFacade<TActualFacade> AddTextField(
      string fieldName,
      string dataFieldName);

    /// <summary>
    /// Adds text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the text field.</returns>
    TextFieldDefinitionFacade<TActualFacade> AddLocalizedTextField(
      string fieldName);

    /// <summary>
    /// Adds text field (it is preferred to use AddLocalizedTextFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddTextFieldAndContinue(string fieldName);

    /// <summary>
    /// Adds text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    TActualFacade AddTextFieldAndContinue(string fieldName, string dataFieldName);

    /// <summary>
    /// Adds text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddLocalizedTextFieldAndContinue(string fieldName);

    /// <summary>
    /// Adds HTML field - use when binding to non-Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Child facade for current customization of the HTML field.</returns>
    HtmlFieldDefinitionFacade<TActualFacade> AddHtmlField(string fieldName);

    /// <summary>
    /// Adds HTML field - use when binding to Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    HtmlFieldDefinitionFacade<TActualFacade> AddLocalizedHtmlField(
      string fieldName);

    /// <summary>
    /// Adds HTML field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    HtmlFieldDefinitionFacade<TActualFacade> AddHtmlField(
      string fieldName,
      string dataFieldName);

    /// <summary>
    /// Adds HTML field - use when binding to non-Lstring properties.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddHtmlFieldAndContinue(string fieldName);

    /// <summary>
    /// Adds HTML field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    TActualFacade AddHtmlFieldAndContinue(string fieldName, string dataFieldName);

    /// <summary>
    /// Adds HTML field - use when binding to Lstring properties
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the HTML field.</returns>
    TActualFacade AddLocalizedHtmlFieldAndContinue(string fieldName);

    /// <summary>Add a field control for selecting language</summary>
    /// <param name="fieldName">Name of the field. Usually 'AvailableLanguages'</param>
    /// <returns>Child facade for customizing the language field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    LanguageChoiceFieldDefinitionFacade<TActualFacade> AddLanguageChoiceField(
      string fieldName);

    /// <summary>Add a field control for selecting language</summary>
    /// <param name="fieldName">Name of the field. Usually 'AvailableLanguages'</param>
    /// <returns>Current facade (i.e. no further customization of the language field is required)</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    TActualFacade AddLanguageChoiceFieldAndContinue(string fieldName);

    /// <summary>
    /// Add a choice field (e.g. drop-down, list of radio buttons, etc.)
    /// </summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    ChoiceFieldDefinitionFacade<TActualFacade> AddChoiceField(
      string fieldName);

    /// <summary>
    /// Add a choice field and specify how it is going to be rendered
    /// </summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="renderAs">Specify how the control will be rendered</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    ChoiceFieldDefinitionFacade<TActualFacade> AddChoiceField(
      string fieldName,
      RenderChoicesAs renderAs);

    /// <summary>
    /// Add a choice field and specify the type of the control that will use this settings to render it
    /// </summary>
    /// <typeparam name="TFieldControl">Field control inheriting <c>ChoiceField</c> that will use this settings to render the field</typeparam>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    ChoiceFieldDefinitionFacade<TActualFacade> AddChoiceField<TFieldControl>(
      string fieldName)
      where TFieldControl : ChoiceField;

    /// <summary>
    /// Add a choice field and specify the type of the control that will use this settings to render it
    /// </summary>
    /// <typeparam name="TFieldControl">Field control inheriting <c>ChoiceField</c> that will use this settings to render the field</typeparam>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="renderAs">Specify how the control will be rendered</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    ChoiceFieldDefinitionFacade<TActualFacade> AddChoiceField<TFieldControl>(
      string fieldName,
      RenderChoicesAs renderAs)
      where TFieldControl : ChoiceField;

    /// <summary>Add a single checkbox</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    ChoiceFieldDefinitionFacade<TActualFacade> AddSingleCheckboxField(
      string fieldName,
      string text);

    /// <summary>Add a single checkbox</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <param name="value">Initial value</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    ChoiceFieldDefinitionFacade<TActualFacade> AddSingleCheckboxField(
      string fieldName,
      string text,
      bool value);

    /// <summary>Add a single checkbox that is visually in a group</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    ChoiceFieldDefinitionFacade<TActualFacade> AddGroupSingleCheckobxField(
      string fieldName,
      string text);

    /// <summary>Add a single checkbox that is visually in a group</summary>
    /// <param name="fieldName">Name of the field to bind to</param>
    /// <param name="text">Text of the checkbox</param>
    /// <param name="value">Initial value</param>
    /// <returns>Child facade for further customization of the choice field</returns>
    /// <remarks>Sets both FieldName and DataFieldName to the value of <paramref name="fieldName" /></remarks>
    /// <exception cref="!:ArgumentNullException">When <paramref name="fieldName" /> is null or empty</exception>
    ChoiceFieldDefinitionFacade<TActualFacade> AddGroupSingleCheckobxField(
      string fieldName,
      string text,
      bool value);

    /// <summary>
    /// Adds the mirror text field (it is preferred to use AddLocalizedMirrorTextField for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    MirrorTextFieldDefinitionFacade<TActualFacade> AddMirrorTextField(
      string fieldName);

    /// <summary>
    /// Adds mirror text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    MirrorTextFieldDefinitionFacade<TActualFacade> AddMirrorTextField(
      string fieldName,
      string dataFieldName);

    /// <summary>
    /// Adds the mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the Lstring property the control is bound to.</param>
    /// <returns>Child facade for further customization of the mirror text field.</returns>
    MirrorTextFieldDefinitionFacade<TActualFacade> AddLocalizedMirrorTextField(
      string fieldName);

    /// <summary>
    /// Adds mirror text field (it is preferred to use AddLocalizedMirrorTextFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddMirrorTextFieldAndContinue(string fieldName);

    /// <summary>
    /// Adds mirror text field - use when you need custom property binding syntax.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <param name="dataFieldName">Extended binding expression for the property the control is bount to.</param>
    /// <returns>Current facade</returns>
    TActualFacade AddMirrorTextFieldAndContinue(string fieldName, string dataFieldName);

    /// <summary>
    /// Adds mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="fieldName">Name of the property the control is bound to.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddLocalizedMirrorTextFieldAndContinue(string fieldName);

    /// <summary>
    /// Adds url name mirror text field (it is preferred to use AddLocalizedUrlNameField for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    MirrorTextFieldDefinitionFacade<TActualFacade> AddUrlNameField(
      string mirroredControlId,
      string title);

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName (it is preferred to use AddLocalizedUrlNameField for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    MirrorTextFieldDefinitionFacade<TActualFacade> AddUrlNameField(
      string mirroredControlId);

    /// <summary>
    /// Adds the url name mirror field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    MirrorTextFieldDefinitionFacade<TActualFacade> AddLocalizedUrlNameField(
      string mirroredControlId,
      string title);

    /// <summary>
    /// Adds the url name mirror field and sets title to UrlName - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>
    /// Child facade for further customization of the mirror text field.
    /// </returns>
    MirrorTextFieldDefinitionFacade<TActualFacade> AddLocalizedUrlNameField(
      string mirroredControlId);

    /// <summary>
    /// Adds url name mirror text field (it is preferred to use AddLocalizedUrlNameFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddUrlNameFieldAndContinue(string mirroredControlId, string title);

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName
    /// (it is preferred to use AddLocalizedUrlNameFieldAndContinue for binding to Lstring properties).
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddUrlNameFieldAndContinue(string mirroredControlId);

    /// <summary>
    /// Adds url name mirror text field - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <param name="title">The field title.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddLocalizedUrlNameFieldAndContinue(string mirroredControlId, string title);

    /// <summary>
    /// Adds url name mirror text field and sets title to UrlName - the preferred way to bind to an Lstring property.
    /// </summary>
    /// <param name="mirroredControlId">The mirrored control id.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddLocalizedUrlNameFieldAndContinue(string mirroredControlId);

    /// <summary>
    /// Adds a field control that can contain other field controls when clicked (expanded)
    /// </summary>
    /// <param name="fieldName">Name of the field the control is bound to</param>
    /// <returns>Child facade for further customization</returns>
    ExpandableFieldDefinitionFacade<TActualFacade> AddExpandableField(
      string fieldName);

    /// <summary>
    /// Add a field control that adds an expandable field that allows the user to allow/disallow commenting and determine
    /// who can comment (e.g. all/registered users)
    /// </summary>
    /// <returns>Child facade for further customization</returns>
    ExpandableFieldDefinitionFacade<TActualFacade> AddAllowCommentsExpandableField();

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
    ExpandableFieldDefinitionFacade<TActualFacade> AddAllowCommentsExpandableField(
      string allowCommentsFieldName,
      string postRightsFieldName,
      string approveCommentsFieldName);

    /// <summary>
    /// Add a field control that adds an expandable field that allows the user to allow/disallow commenting and determine
    /// who can comment (e.g. all/registered users)
    /// </summary>
    /// <returns>Current facade</returns>
    TActualFacade AddAllowCommentsExpandableFieldAndContinue();

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
    TActualFacade AddAllowCommentsExpandableFieldAndContinue(
      string allowCommentsFieldName,
      string postRightsFieldName,
      string approveCommentsFieldName);

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <param name="allowMultipleUrlsFieldName">Name of a boolean field to bind to that allows/disallows having multiple URLs</param>
    /// <param name="additiionalUrlNamesFieldName">Name of a string array field to bind to that contains the additional url names</param>
    /// <param name="additionalUrlsRedirectToDefaultFieldName">Name of a boolean field to bind to that defines whether the additional URLs for this item redirect to it or do a rewrite</param>
    /// <param name="defaultUrlFieldName">Name of a string field to bind to that stores the default url</param>
    /// <returns>Child facade for furhter customization</returns>
    /// <exception cref="!:ArgumentNullException">When either of the arguments is <c>null</c></exception>
    ExpandableFieldDefinitionFacade<TActualFacade> AddMultipleUrlsExpandableField(
      string allowMultipleUrlsFieldName,
      string additiionalUrlNamesFieldName,
      string additionalUrlsRedirectToDefaultFieldName,
      string defaultUrlFieldName);

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <returns>Child facade for furhter customization</returns>
    ExpandableFieldDefinitionFacade<TActualFacade> AddMultipleUrlsExpandableField();

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <returns>Current facade</returns>
    TActualFacade AddMultipleUrlsExpandableFieldAndContinue();

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <param name="allowMultipleUrlsFieldName">Name of a boolean field to bind to that allows/disallows having multiple URLs</param>
    /// <param name="additiionalUrlNamesFieldName">Name of a string array field to bind to that contains the additional url names</param>
    /// <param name="additionalUrlsRedirectToDefaultFieldName">Name of a boolean field to bind to that defines whether the additional URLs for this item redirect to it or do a rewrite</param>
    /// <param name="defaultUrlFieldName">Name of a string field to bind to that stores the default url</param>
    /// <returns>Child facade for furhter customization</returns>
    /// <exception cref="!:ArgumentNullException">When either of the arguments is <c>null</c></exception>
    TActualFacade AddMultipleUrlsExpandableFieldAndContinue(
      string allowMultipleUrlsFieldName,
      string additiionalUrlNamesFieldName,
      string additionalUrlsRedirectToDefaultFieldName,
      string defaultUrlFieldName);

    /// <summary>Adds the version note control.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the version note control.</returns>
    VersionNoteDefinitionFacade<TActualFacade> AddVersionNoteControl(
      string fieldName);

    /// <summary>
    /// Adds the version note control and sets FieldName to "Comment".
    /// </summary>
    /// <returns>Child facade for further customization of the version note control.</returns>
    VersionNoteDefinitionFacade<TActualFacade> AddVersionNoteControl();

    /// <summary>Adds the version note control.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddVersionNoteControlAndContinue(string fieldName);

    /// <summary>
    /// Adds the version note control and sets FieldName to "Comment".
    /// </summary>
    /// <returns>Current facade.</returns>
    TActualFacade AddVersionNoteControlAndContinue();

    /// <summary>Adds the workflow status info field.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    WorkflowStatusInfoDefinitionFacade<TActualFacade> AddWorkflowStatusInfoField(
      string fieldName);

    /// <summary>
    /// Adds the workflow status info field and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Child facade for further customization of the workflow status info field.</returns>
    WorkflowStatusInfoDefinitionFacade<TActualFacade> AddWorkflowStatusInfoField();

    /// <summary>Adds the workflow status info field and continue.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>Current facade.</returns>
    TActualFacade AddWorkflowStatusInfoFieldAndContinue(string fieldName);

    /// <summary>
    /// Adds the workflow status info field and sets FieldName to "ContentWorkflowStatusInfoField".
    /// </summary>
    /// <returns>Current facade.</returns>
    TActualFacade AddWorkflowStatusInfoFieldAndContinue();

    /// <summary>
    /// Adds the language list field with all defaults set. Field name is set as default to "AvailableLanguages"
    /// </summary>
    /// <returns>The facade for the LanguageListField</returns>
    LanguageListFieldDefinitionFacade<TActualFacade> AddLanguageListField();

    /// <summary>Adds the language list field with all defaults set.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>The facade for the LanguageListField</returns>
    LanguageListFieldDefinitionFacade<TActualFacade> AddLanguageListField(
      string fieldName);
  }
}
