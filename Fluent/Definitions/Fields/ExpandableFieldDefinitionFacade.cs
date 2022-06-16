// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.ExpandableFieldDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>
  /// Fluent API wrapper for <c>ExpandableFieldElement</c>
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the parent facade</typeparam>
  public class ExpandableFieldDefinitionFacade<TParentFacade> : 
    FieldControlDefinitionFacade<ExpandableFieldElement, ExpandableFieldDefinitionFacade<TParentFacade>, TParentFacade>,
    IHasFieldControls<ExpandableFieldDefinitionFacade<TParentFacade>>
    where TParentFacade : class
  {
    private DefinitionFacadeInfo facadeInfo;
    private IHasFieldControls<ExpandableFieldDefinitionFacade<TParentFacade>> fields;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.ExpandableFieldDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    /// <param name="mode">The mode.</param>
    public ExpandableFieldDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement,
      string viewName,
      string sectionName,
      TParentFacade parentFacade,
      string fieldName,
      string resourceClassId,
      FieldDisplayMode mode)
      : base(moduleName, definitionName, contentType, parentElement, viewName, sectionName, parentFacade, fieldName, resourceClassId, mode)
    {
      this.Field.WrapperTag = HtmlTextWriterTag.Li;
      this.facadeInfo = new DefinitionFacadeInfo((Func<string>) (() => this.Field.ResourceClassId), (Func<FieldDisplayMode?>) (() => this.Field.DisplayMode));
      this.facadeInfo.ModuleName = moduleName;
      this.facadeInfo.DefinitionName = definitionName;
      this.facadeInfo.ContentType = contentType;
      this.facadeInfo.ViewName = viewName;
      this.facadeInfo.SectionName = sectionName;
      this.fields = (IHasFieldControls<ExpandableFieldDefinitionFacade<TParentFacade>>) new FieldControlsCollection<ExpandableFieldDefinitionFacade<TParentFacade>>(this, this.FieldsConfig, (IDefinitionFacadeInfo) this.facadeInfo);
    }

    /// <inheritdoc />
    protected override ExpandableFieldElement CreateConfig(
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement)
    {
      return new ExpandableFieldElement((ConfigElement) parentElement);
    }

    /// <inheritdoc />
    public ConfigElementDictionary<string, FieldDefinitionElement> FieldsConfig => this.Field.ExpandableFields;

    /// <inheritdoc />
    public IDefinitionFacadeInfo FacadeInfo => (IDefinitionFacadeInfo) this.facadeInfo;

    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> DefineToggleControl(
      string fieldName,
      string text,
      bool value)
    {
      ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> choiceFieldFacade = this.CreateChoiceFieldFacade(this.ModuleName, this.DefinitionName, this.ContentType, this.Field, this.ViewName, this.SectionName, this, fieldName, this.Field.ResourceClassId, new FieldDisplayMode?(this.Field.DisplayMode.Value));
      choiceFieldFacade.SetDataFieldName(fieldName).SetRenderChoiceAs(RenderChoicesAs.SingleCheckBox).SetCssClass("sfCheckBox").SetWrapperTag(HtmlTextWriterTag.Div).AddChoiceAndContinue(text, value);
      this.Field.ExpandFieldDefinition = choiceFieldFacade.Get();
      return choiceFieldFacade;
    }

    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> DefineToggleControl(
      string fieldName,
      string text)
    {
      ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> choiceFieldFacade = this.CreateChoiceFieldFacade(this.ModuleName, this.DefinitionName, this.ContentType, this.Field, this.ViewName, this.SectionName, this, fieldName, this.Field.ResourceClassId, new FieldDisplayMode?(this.Field.DisplayMode.Value));
      choiceFieldFacade.SetDataFieldName(fieldName).SetRenderChoiceAs(RenderChoicesAs.SingleCheckBox).SetCssClass("sfCheckBox").SetWrapperTag(HtmlTextWriterTag.Div).AddChoiceAndContinue(text);
      this.Field.ExpandFieldDefinition = choiceFieldFacade.Get();
      return choiceFieldFacade;
    }

    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> DefineToggleControl() => this.DefineToggleControl(this.Field.FieldName, "AllowComments");

    /// <inheritdoc />
    public TextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddTextField(
      string fieldName)
    {
      return this.FieldsCollection.AddTextField(fieldName);
    }

    /// <inheritdoc />
    public TextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddTextField(
      string fieldName,
      string dataFieldName)
    {
      return this.FieldsCollection.AddTextField(fieldName, dataFieldName);
    }

    /// <inheritdoc />
    public TextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddLocalizedTextField(
      string fieldName)
    {
      return this.FieldsCollection.AddLocalizedTextField(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddTextFieldAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddTextFieldAndContinue(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddTextFieldAndContinue(
      string fieldName,
      string dataFieldName)
    {
      return this.FieldsCollection.AddTextFieldAndContinue(fieldName, dataFieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddLocalizedTextFieldAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddLocalizedTextFieldAndContinue(fieldName);
    }

    /// <inheritdoc />
    public HtmlFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddHtmlField(
      string fieldName)
    {
      return this.FieldsCollection.AddHtmlField(fieldName);
    }

    /// <inheritdoc />
    public HtmlFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddLocalizedHtmlField(
      string fieldName)
    {
      return this.FieldsCollection.AddLocalizedHtmlField(fieldName);
    }

    /// <inheritdoc />
    public HtmlFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddHtmlField(
      string fieldName,
      string dataFieldName)
    {
      return this.FieldsCollection.AddHtmlField(fieldName, dataFieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddHtmlFieldAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddHtmlFieldAndContinue(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddHtmlFieldAndContinue(
      string fieldName,
      string dataFieldName)
    {
      return this.FieldsCollection.AddHtmlFieldAndContinue(fieldName, dataFieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddLocalizedHtmlFieldAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddLocalizedHtmlFieldAndContinue(fieldName);
    }

    /// <inheritdoc />
    public LanguageChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddLanguageChoiceField(
      string fieldName)
    {
      return this.FieldsCollection.AddLanguageChoiceField(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddLanguageChoiceFieldAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddLanguageChoiceFieldAndContinue(fieldName);
    }

    /// <inheritdoc />
    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddChoiceField(
      string fieldName)
    {
      return this.FieldsCollection.AddChoiceField(fieldName);
    }

    /// <inheritdoc />
    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddChoiceField(
      string fieldName,
      RenderChoicesAs renderAs)
    {
      return this.FieldsCollection.AddChoiceField(fieldName, renderAs);
    }

    /// <inheritdoc />
    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddChoiceField<TFieldControl>(
      string fieldName)
      where TFieldControl : ChoiceField
    {
      return this.FieldsCollection.AddChoiceField<TFieldControl>(fieldName);
    }

    /// <inheritdoc />
    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddChoiceField<TFieldControl>(
      string fieldName,
      RenderChoicesAs renderAs)
      where TFieldControl : ChoiceField
    {
      return this.FieldsCollection.AddChoiceField<TFieldControl>(fieldName, renderAs);
    }

    /// <inheritdoc />
    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddSingleCheckboxField(
      string fieldName,
      string text)
    {
      return this.FieldsCollection.AddSingleCheckboxField(fieldName, text);
    }

    /// <inheritdoc />
    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddSingleCheckboxField(
      string fieldName,
      string text,
      bool value)
    {
      return this.FieldsCollection.AddSingleCheckboxField(fieldName, text, value);
    }

    /// <inheritdoc />
    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddGroupSingleCheckobxField(
      string fieldName,
      string text)
    {
      return this.FieldsCollection.AddGroupSingleCheckobxField(fieldName, text);
    }

    /// <inheritdoc />
    public ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddGroupSingleCheckobxField(
      string fieldName,
      string text,
      bool value)
    {
      return this.FieldsCollection.AddGroupSingleCheckobxField(fieldName, text, value);
    }

    /// <inheritdoc />
    public MirrorTextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddMirrorTextField(
      string fieldName)
    {
      return this.FieldsCollection.AddMirrorTextField(fieldName);
    }

    /// <inheritdoc />
    public MirrorTextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddMirrorTextField(
      string fieldName,
      string dataFieldName)
    {
      return this.FieldsCollection.AddMirrorTextField(fieldName, dataFieldName);
    }

    /// <inheritdoc />
    public MirrorTextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddLocalizedMirrorTextField(
      string fieldName)
    {
      return this.FieldsCollection.AddLocalizedMirrorTextField(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddMirrorTextFieldAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddMirrorTextFieldAndContinue(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddMirrorTextFieldAndContinue(
      string fieldName,
      string dataFieldName)
    {
      return this.FieldsCollection.AddMirrorTextFieldAndContinue(fieldName, dataFieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddLocalizedMirrorTextFieldAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddLocalizedMirrorTextFieldAndContinue(fieldName);
    }

    /// <inheritdoc />
    public MirrorTextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddUrlNameField(
      string mirroredControlId,
      string title)
    {
      return this.FieldsCollection.AddUrlNameField(mirroredControlId, title);
    }

    /// <inheritdoc />
    public MirrorTextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddUrlNameField(
      string mirroredControlId)
    {
      return this.FieldsCollection.AddUrlNameField(mirroredControlId);
    }

    /// <inheritdoc />
    public MirrorTextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddLocalizedUrlNameField(
      string mirroredControlId,
      string title)
    {
      return this.FieldsCollection.AddLocalizedUrlNameField(mirroredControlId, title);
    }

    /// <inheritdoc />
    public MirrorTextFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddLocalizedUrlNameField(
      string mirroredControlId)
    {
      return this.FieldsCollection.AddLocalizedUrlNameField(mirroredControlId);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddUrlNameFieldAndContinue(
      string mirroredControlId,
      string title)
    {
      return this.FieldsCollection.AddUrlNameFieldAndContinue(mirroredControlId, title);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddUrlNameFieldAndContinue(
      string mirroredControlId)
    {
      return this.FieldsCollection.AddUrlNameFieldAndContinue(mirroredControlId);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddLocalizedUrlNameFieldAndContinue(
      string mirroredControlId,
      string title)
    {
      return this.FieldsCollection.AddLocalizedUrlNameFieldAndContinue(mirroredControlId, title);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddLocalizedUrlNameFieldAndContinue(
      string mirroredControlId)
    {
      return this.FieldsCollection.AddLocalizedUrlNameFieldAndContinue(mirroredControlId);
    }

    /// <inheritdoc />
    public VersionNoteDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddVersionNoteControl(
      string fieldName)
    {
      return this.FieldsCollection.AddVersionNoteControl(fieldName);
    }

    /// <inheritdoc />
    public VersionNoteDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddVersionNoteControl() => this.FieldsCollection.AddVersionNoteControl();

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddVersionNoteControlAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddVersionNoteControlAndContinue(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddVersionNoteControlAndContinue() => this.FieldsCollection.AddVersionNoteControlAndContinue();

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddExpandableField(
      string fieldName)
    {
      return this.FieldsCollection.AddExpandableField(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddAllowCommentsExpandableField(
      string allowCommentsFieldName,
      string postRightsFieldName,
      string approveCommentsFieldName)
    {
      return this.FieldsCollection.AddAllowCommentsExpandableField(allowCommentsFieldName, postRightsFieldName, approveCommentsFieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddAllowCommentsExpandableField() => this.FieldsCollection.AddAllowCommentsExpandableField();

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddAllowCommentsExpandableFieldAndContinue() => this.FieldsCollection.AddAllowCommentsExpandableFieldAndContinue();

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddAllowCommentsExpandableFieldAndContinue(
      string allowCommentsFieldName,
      string postRightsFieldName,
      string approveCommentsFieldName)
    {
      return this.FieldsCollection.AddAllowCommentsExpandableFieldAndContinue(allowCommentsFieldName, postRightsFieldName, approveCommentsFieldName);
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
    public ExpandableFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddMultipleUrlsExpandableField(
      string allowMultipleUrlsFieldName,
      string additiionalUrlNamesFieldName,
      string additionalUrlsRedirectToDefaultFieldName,
      string defaultUrlFieldName)
    {
      return this.FieldsCollection.AddMultipleUrlsExpandableField(allowMultipleUrlsFieldName, additiionalUrlNamesFieldName, additionalUrlsRedirectToDefaultFieldName, defaultUrlFieldName);
    }

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <returns>Child facade for furhter customization</returns>
    public ExpandableFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddMultipleUrlsExpandableField() => this.FieldsCollection.AddMultipleUrlsExpandableField();

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <returns>Current facade</returns>
    public ExpandableFieldDefinitionFacade<TParentFacade> AddMultipleUrlsExpandableFieldAndContinue() => this.FieldsCollection.AddMultipleUrlsExpandableFieldAndContinue();

    /// <summary>
    /// Add expandable field that displays UI for managing multiple URLs for the current item
    /// </summary>
    /// <param name="allowMultipleUrlsFieldName">Name of a boolean field to bind to that allows/disallows having multiple URLs</param>
    /// <param name="additiionalUrlNamesFieldName">Name of a string array field to bind to that contains the additional url names</param>
    /// <param name="additionalUrlsRedirectToDefaultFieldName">Name of a boolean field to bind to that defines whether the additional URLs for this item redirect to it or do a rewrite</param>
    /// <param name="defaultUrlFieldName">Name of a string field to bind to that stores the default url</param>
    /// <returns>Child facade for furhter customization</returns>
    /// <exception cref="T:System.ArgumentNullException">When either of the arguments is <c>null</c></exception>
    public ExpandableFieldDefinitionFacade<TParentFacade> AddMultipleUrlsExpandableFieldAndContinue(
      string allowMultipleUrlsFieldName,
      string additiionalUrlNamesFieldName,
      string additionalUrlsRedirectToDefaultFieldName,
      string defaultUrlFieldName)
    {
      return this.FieldsCollection.AddMultipleUrlsExpandableFieldAndContinue(allowMultipleUrlsFieldName, additiionalUrlNamesFieldName, additionalUrlsRedirectToDefaultFieldName, defaultUrlFieldName);
    }

    /// <inheritdoc />
    public WorkflowStatusInfoDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddWorkflowStatusInfoField(
      string fieldName)
    {
      return this.FieldsCollection.AddWorkflowStatusInfoField(fieldName);
    }

    /// <inheritdoc />
    public WorkflowStatusInfoDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddWorkflowStatusInfoField() => this.FieldsCollection.AddWorkflowStatusInfoField();

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddWorkflowStatusInfoFieldAndContinue(
      string fieldName)
    {
      return this.FieldsCollection.AddWorkflowStatusInfoFieldAndContinue(fieldName);
    }

    /// <inheritdoc />
    public ExpandableFieldDefinitionFacade<TParentFacade> AddWorkflowStatusInfoFieldAndContinue() => this.FieldsCollection.AddWorkflowStatusInfoFieldAndContinue();

    /// <summary>
    /// Adds the language list field with all defaults set. Field name is set as default to "AvailableLanguages"
    /// </summary>
    /// <returns>The facade for the LanguageListField</returns>
    public LanguageListFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddLanguageListField() => this.FieldsCollection.AddLanguageListField();

    /// <summary>Adds the language list field with all defaults set.</summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>The facade for the LanguageListField</returns>
    public LanguageListFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> AddLanguageListField(
      string fieldName)
    {
      return this.FieldsCollection.AddLanguageListField(fieldName);
    }

    internal virtual string ModuleName => this.moduleName;

    internal virtual Type ContentType => this.contentType;

    internal virtual string ViewName => this.viewName;

    internal virtual string SectionName => this.sectionName;

    internal virtual string DefinitionName => this.definitionName;

    internal virtual IHasFieldControls<ExpandableFieldDefinitionFacade<TParentFacade>> FieldsCollection => this.fields;

    internal virtual ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>> CreateChoiceFieldFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ExpandableFieldElement field,
      string viewName,
      string sectionName,
      ExpandableFieldDefinitionFacade<TParentFacade> thisInstance,
      string fieldName,
      string resourceClassId,
      FieldDisplayMode? displayMode)
    {
      return new ChoiceFieldDefinitionFacade<ExpandableFieldDefinitionFacade<TParentFacade>>(moduleName, definitionName, contentType, (ConfigElement) field, viewName, sectionName, thisInstance, fieldName, resourceClassId, displayMode.Value);
    }
  }
}
