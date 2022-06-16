// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>
  /// Localization class which holds localizable lables for the module builder module.
  /// </summary>
  internal class ModuleBuilderResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ModuleBuilderResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public ModuleBuilderResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Title of the module for creating custom modules</summary>
    [ResourceEntry("ContentTypesModuleTitle", Description = "Title of the module for creating custom modules", LastModified = "2013/04/01", Value = "Module builder")]
    public string ContentTypesModuleTitle => this[nameof (ContentTypesModuleTitle)];

    /// <summary>Description of the module for creating custom modules</summary>
    [ResourceEntry("ContentTypesModuleDescription", Description = "Description of the module for creating custom modules", LastModified = "2011/09/22", Value = "The module that allows users to create custom modules")]
    public string ContentTypesModuleDescription => this[nameof (ContentTypesModuleDescription)];

    /// <summary>Url name of the module for creating custom modules</summary>
    [ResourceEntry("ContentTypesModuleUrlName", Description = "Url name of the module for creating custom modules", LastModified = "2011/09/22", Value = "Module-builder")]
    public string ContentTypesModuleUrlName => this[nameof (ContentTypesModuleUrlName)];

    /// <summary>Url name of the dashboard page of the custom modules</summary>
    [ResourceEntry("ContentTypesDashboardUrlName", Description = "Url name of the dashboard page of the custom modules", LastModified = "2011/09/22", Value = "dashboard")]
    public string ContentTypesDashboardUrlName => this[nameof (ContentTypesDashboardUrlName)];

    /// <summary>Message shown to users when no dynamic modules exist</summary>
    [ResourceEntry("NoModulesHaveBeenCreatedYet", Description = "Message shown to users when no dynamic modules exist", LastModified = "2011/12/06", Value = "No modules have been created yet")]
    public string NoModulesHaveBeenCreatedYet => this[nameof (NoModulesHaveBeenCreatedYet)];

    /// <summary>phrase: Create a module</summary>
    [ResourceEntry("CreateAModule", Description = "phrase: Create a module", LastModified = "2011/12/06", Value = "Create a module")]
    public string CreateAModule => this[nameof (CreateAModule)];

    /// <summary>Actions menu: Deactivate</summary>
    [ResourceEntry("DeactivateAction", Description = "Actions menu: Deactivate", LastModified = "2013/11/15", Value = "Deactivate")]
    public string DeactivateAction => this[nameof (DeactivateAction)];

    /// <summary>Actions menu: Activate</summary>
    [ResourceEntry("ActivateAction", Description = "Actions menu: Activate", LastModified = "2013/11/15", Value = "Activate")]
    public string ActivateAction => this[nameof (ActivateAction)];

    /// <summary>Actions menu: Export structure</summary>
    [ResourceEntry("ExportStructureAction", Description = "Actions menu: Export structure", LastModified = "2013/11/15", Value = "Export structure")]
    public string ExportStructureAction => this[nameof (ExportStructureAction)];

    /// <summary>Actions menu: Import new structure and settings</summary>
    [ResourceEntry("ImportStructureAction", Description = "Actions menu: Import new structure and settings", LastModified = "2013/11/15", Value = "Import new structure and settings")]
    public string ImportStructureAction => this[nameof (ImportStructureAction)];

    /// <summary>Actions menu: Export content</summary>
    [ResourceEntry("ExportContentAction", Description = "Actions menu: Export content", LastModified = "2013/11/15", Value = "Export content")]
    public string ExportContentAction => this[nameof (ExportContentAction)];

    /// <summary>Actions menu: Delete</summary>
    [ResourceEntry("DeleteAction", Description = "Actions menu: Delete", LastModified = "2013/11/15", Value = "Delete")]
    public string DeleteAction => this[nameof (DeleteAction)];

    /// <summary>phrase: Modules</summary>
    [ResourceEntry("ContentTypes", Description = "phrase: Modules", LastModified = "2011/12/08", Value = "Modules")]
    public string ContentTypes => this[nameof (ContentTypes)];

    /// <summary>phrase: Create a module</summary>
    [ResourceEntry("CreateAContentType", Description = "phrase: Create a module", LastModified = "2011/09/23", Value = "Create a module")]
    public string CreateAContentType => this[nameof (CreateAContentType)];

    /// <summary>
    /// Title of the grid column header which displays module name
    /// </summary>
    [ResourceEntry("ContentTypeNameHeader", Description = "Title of the grid column header which displays module name", LastModified = "2011/09/23", Value = "Name")]
    public string ContentTypeNameHeader => this[nameof (ContentTypeNameHeader)];

    /// <summary>
    /// Title of the grid column header which displays module description
    /// </summary>
    [ResourceEntry("ContentTypeDescriptionHeader", Description = "Title of the grid column header which displays module description", LastModified = "2011/09/23", Value = "Description")]
    public string ContentTypeDescriptionHeader => this[nameof (ContentTypeDescriptionHeader)];

    /// <summary>
    /// Title of the grid column header which displays module actions
    /// </summary>
    [ResourceEntry("ContentTypeActionsHeader", Description = "Title of the grid column header which displays module actions", LastModified = "2011/09/23", Value = "Actions")]
    public string ContentTypeActionsHeader => this[nameof (ContentTypeActionsHeader)];

    /// <summary>
    /// Title of the grid column header which displays module last modified and owner info
    /// </summary>
    [ResourceEntry("ContentTypeLastModifiedOwnerHeader", Description = "Title of the grid column header which displays module last modified and owner info", LastModified = "2011/09/23", Value = "Last modified / owner")]
    public string ContentTypeLastModifiedOwnerHeader => this[nameof (ContentTypeLastModifiedOwnerHeader)];

    /// <summary>phrase: Back to modules</summary>
    [ResourceEntry("BackToContentTypes", Description = "phrase: Back to modules", LastModified = "2011/09/23", Value = "Back to Modules")]
    public string BackToContentTypes => this[nameof (BackToContentTypes)];

    /// <summary>phrase: Back to</summary>
    [ResourceEntry("BackTo", Description = "word: Back to", LastModified = "2011/01/12", Value = "Back to")]
    public string BackTo => this[nameof (BackTo)];

    /// <summary>Title of the module name field.</summary>
    [ResourceEntry("ContentTypeNameTitle", Description = "Title of the module name field", LastModified = "2011/09/23", Value = "Name")]
    public string ContentTypeNameTitle => this[nameof (ContentTypeNameTitle)];

    /// <summary>Example of the module name field</summary>
    [ResourceEntry("ContentTypeNameExample", Description = "Example of the module name field", LastModified = "2011/09/23", Value = "<strong>Example:</strong> <em>Press releases</em>")]
    public string ContentTypeNameExample => this[nameof (ContentTypeNameExample)];

    /// <summary>Title of the module description field</summary>
    [ResourceEntry("ContentTypeDescriptionTitle", Description = "Title of the module description field", LastModified = "2011/09/23", Value = "Description")]
    public string ContentTypeDescriptionTitle => this[nameof (ContentTypeDescriptionTitle)];

    /// <summary>word: Continue</summary>
    [ResourceEntry("Continue", Description = "word: Continue", LastModified = "2011/09/23", Value = "Continue")]
    public string Continue => this[nameof (Continue)];

    /// <summary>word: Cancel</summary>
    [ResourceEntry("Cancel", Description = "word: Cancel", LastModified = "2011/09/23", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>phrase: Content type (singular)</summary>
    [ResourceEntry("ItemTitleTitle", Description = "phrase: Content type of this module (singular)", LastModified = "2011/09/23", Value = "Content type (singular)")]
    public string ItemTitleTitle => this[nameof (ItemTitleTitle)];

    /// <summary>phrase: Content type</summary>
    [ResourceEntry("ContentType", Description = "phrase: Content type of this module", LastModified = "2012/03/19", Value = "Content type")]
    public string ContentType => this[nameof (ContentType)];

    /// <summary>phrase: Example: Press release</summary>
    [ResourceEntry("ItemTitleExample", Description = "phrase: Example: Press release", LastModified = "2011/09/23", Value = "<strong>Example:</strong> <em>Press release</em>")]
    public string ItemTitleExample => this[nameof (ItemTitleExample)];

    /// <summary>phrase: Developer name of Item</summary>
    [ResourceEntry("ItemNameTitle", Description = "phrase: Developer name of the type", LastModified = "2011/09/23", Value = "Developer name of this content type")]
    public string ItemNameTitle => this[nameof (ItemNameTitle)];

    /// <summary>phrase: The name used in code. Spaces are not allowed</summary>
    [ResourceEntry("ItemNameDescription", Description = "phrase: The name used in code. Spaces are not allowed", LastModified = "2011/09/23", Value = "The name used in code. Spaces are not allowed")]
    public string ItemNameDescription => this[nameof (ItemNameDescription)];

    /// <summary>phrase: Example: PressRelease</summary>
    [ResourceEntry("ItemNameExample", Description = "phrase: Example : PressRelease", LastModified = "2011/09/23", Value = "<strong>Example</strong>: PressRelease")]
    public string ItemNameExample => this[nameof (ItemNameExample)];

    /// <summary>word: Finish</summary>
    [ResourceEntry("Finish", Description = "word: Finish", LastModified = "2011/09/23", Value = "Finish")]
    public string Finish => this[nameof (Finish)];

    /// <summary>phrase: Update widget templates and finish</summary>
    [ResourceEntry("UpdateWTemplatesFinish", Description = "phrase: Update widget templates and finish", LastModified = "2011/09/23", Value = "Update widget templates and finish")]
    public string UpdateWTemplatesFinish => this[nameof (UpdateWTemplatesFinish)];

    /// <summary>
    /// phrase: Are you sure you want templates for public widgets to be regenerated?
    /// </summary>
    [ResourceEntry("ConfirmWidgetTemplateUpdate", Description = "phrase: Are you sure you want templates for public widgets to be regenerated?", LastModified = "2012/02/15", Value = "Are you sure you want templates for public widgets to be regenerated?")]
    public string ConfirmWidgetTemplateUpdate => this[nameof (ConfirmWidgetTemplateUpdate)];

    /// <summary>
    /// phrase: If you have made changes in the widget templates, they will be discarded. There is no undo.
    /// </summary>
    [ResourceEntry("NoUndoWidgetTemplateUpdate", Description = "phrase: If you have made changes in the widget templates, they will be discarded. There is no undo.", LastModified = "2012/02/15", Value = "If you have made changes in the widget templates, they will be discarded. There is no undo.")]
    public string NoUndoWidgetTemplateUpdate => this[nameof (NoUndoWidgetTemplateUpdate)];

    /// <summary>
    /// phrase: Are you sure you want to save the changes to this content type?
    /// </summary>
    [ResourceEntry("ConfirmDynamicTypeUpdate", Description = "phrase: Are you sure you want to save the changes to this content type?", LastModified = "2012/07/04", Value = "Are you sure you want to save the changes to this content type?")]
    public string ConfirmDynamicTypeUpdate => this[nameof (ConfirmDynamicTypeUpdate)];

    /// <summary>phrase: Update widget templates as well</summary>
    [ResourceEntry("UpdateWidgetTemplatesCheckboxLabel", Description = "phrase: Update widget templates as well", LastModified = "2012/07/04", Value = "Update widget templates as well")]
    public string UpdateWidgetTemplatesCheckboxLabel => this[nameof (UpdateWidgetTemplatesCheckboxLabel)];

    /// <summary>
    /// phrase: If you have made modifications in widget templates they will be lost
    /// </summary>
    /// <value>If you have made modifications in widget templates they will be lost</value>
    [ResourceEntry("UpdateWidgetTemplatesWarning", Description = "phrase: If you have made modifications in widget templates they will be lost", LastModified = "2012/11/13", Value = "If you have made modifications in widget templates they will be lost")]
    public string UpdateWidgetTemplatesWarning => this[nameof (UpdateWidgetTemplatesWarning)];

    /// <summary>phrase: Yes, save changes</summary>
    [ResourceEntry("YesSaveChanges", Description = "phrase: Yes, save changes", LastModified = "2012/07/04", Value = "Yes, save changes")]
    public string YesSaveChanges => this[nameof (YesSaveChanges)];

    /// <summary>
    /// phrase: Changing parent of this content type will affect:
    /// </summary>
    [ResourceEntry("ChangesWillAffect", Description = "phrase: Changing parent of this content type will affect:", LastModified = "2012/07/04", Value = "Changing parent of this content type will affect:")]
    public string ChangesWillAffect => this[nameof (ChangesWillAffect)];

    /// <summary>phrase: Widgets in Page toolbox</summary>
    [ResourceEntry("AffectedItemsLine1", Description = "phrase: Widgets in Page toolbox", LastModified = "2012/07/04", Value = "Widgets in Page toolbox")]
    public string AffectedItemsLine1 => this[nameof (AffectedItemsLine1)];

    /// <summary>
    /// phrase: Holds all dynamic content widgets for the {0} module.
    /// </summary>
    [ResourceEntry("ModuleSectionDescription", Description = "phrase: Holds all dynamic content widgets for the {0} module.", LastModified = "2014/06/18", Value = "Holds all dynamic content widgets for the {0} module.")]
    public string ModuleSectionDescription => this[nameof (ModuleSectionDescription)];

    /// <summary>phrase: URLs of existing content items</summary>
    [ResourceEntry("AffectedItemsLine2", Description = "phrase: URLs of existing content items", LastModified = "2012/07/04", Value = "URLs of existing content items")]
    public string AffectedItemsLine2 => this[nameof (AffectedItemsLine2)];

    /// <summary>phrase: Backend navigation</summary>
    [ResourceEntry("AffectedItemsLine3", Description = "phrase: Backend navigation", LastModified = "2012/07/04", Value = "Backend navigation")]
    public string AffectedItemsLine3 => this[nameof (AffectedItemsLine3)];

    /// <summary>
    /// phrase: Keep #= ContentTypeItemTitle # widget in the toolbox
    /// </summary>
    [ResourceEntry("KeepWidgetsCheckboxLabel", Description = "phrase: Keep #= ContentTypeItemTitle # widget in the toolbox", LastModified = "2012/07/04", Value = "Keep #= ContentTypeItemTitle # widget in the toolbox")]
    public string KeepWidgetsCheckboxLabel => this[nameof (KeepWidgetsCheckboxLabel)];

    /// <summary>
    /// phrase: All settings for Images will be moved in the Albums widget.
    /// </summary>
    [ResourceEntry("KeepWidgetsWarningLine1", Description = "phrase: All settings for Images will be moved in the Albums widget.", LastModified = "2012/07/04", Value = "All settings for Images will be moved in the Albums widget.")]
    public string KeepWidgetsWarningLine1 => this[nameof (KeepWidgetsWarningLine1)];

    /// <summary>
    /// phrase: You can keep the old Images widget in the toolbox, as well.
    /// </summary>
    [ResourceEntry("KeepWidgetsWarningLine2", Description = "phrase: You can keep the old Images widget in the toolbox, as well.", LastModified = "2012/07/04", Value = "You can keep the old Images widget in the toolbox, as well.")]
    public string KeepWidgetsWarningLine2 => this[nameof (KeepWidgetsWarningLine2)];

    /// <summary>phrase: Yes, Save and update widget templates</summary>
    [ResourceEntry("YesUpdateWidgetTemplates", Description = "phrase: Yes, Save and update widget templates", LastModified = "2012/02/15", Value = "Yes, Save and update widget templates")]
    public string YesUpdateWidgetTemplates => this[nameof (YesUpdateWidgetTemplates)];

    /// <summary>phease: Fields of this item</summary>
    [ResourceEntry("FieldsOfThisItem", Description = "phrase: Fields of this item", LastModified = "2011/09/23", Value = "Fields of this item")]
    public string FieldsOfThisItem => this[nameof (FieldsOfThisItem)];

    /// <summary>phrase: Add a field...</summary>
    [ResourceEntry("AddAField", Description = "phrase: Add a field...", LastModified = "2011/09/23", Value = "Add a field...")]
    public string AddAField => this[nameof (AddAField)];

    /// <summary>Title of the field name field</summary>
    [ResourceEntry("FieldNameTitle", Description = "Title of the field name field", LastModified = "2011/09/25", Value = "Name")]
    public string FieldNameTitle => this[nameof (FieldNameTitle)];

    /// <summary>word: Or</summary>
    [ResourceEntry("Or", Description = "word: Or", LastModified = "2011/09/25", Value = "Or")]
    public string Or => this[nameof (Or)];

    /// <summary>phrase: Which field is the identifier of the content?</summary>
    [ResourceEntry("MainShortFieldSectionTitle", Description = "phrase: Which field is the identifier of the content?", LastModified = "2011/12/08", Value = "Which field is the identifier of the content?")]
    public string MainShortFieldSectionTitle => this[nameof (MainShortFieldSectionTitle)];

    /// <summary>
    /// phrase: It will be displayed in lists of content items and will be used for URL-generating
    /// </summary>
    [ResourceEntry("MainShortFieldSectionDescription", Description = "phrase: It will be displayed in lists of content items and will be used for URL-generating", LastModified = "2013/06/25", Value = "It will be displayed in lists of content items and will be used for URL-generating")]
    public string MainShortFieldSectionDescription => this[nameof (MainShortFieldSectionDescription)];

    /// <summary>
    /// phrase: The identificator must be a Required Short text field
    /// </summary>
    [ResourceEntry("MainShortFieldSectionExamples", Description = "phrase: The identificator must be a Required Short text field", LastModified = "2011/12/08", Value = "The identificator must be a Required Short text field")]
    public string MainShortFieldSectionExamples => this[nameof (MainShortFieldSectionExamples)];

    /// <summary>phrase: Title for main short field section</summary>
    [ResourceEntry("MainShortFieldSectionExamplesTitle", Description = "phrase: Title for main short field section", LastModified = "2011/11/21", Value = "Example")]
    public string MainShortFieldSectionExamplesTitle => this[nameof (MainShortFieldSectionExamplesTitle)];

    /// <summary>phrase: Define a content type of</summary>
    [ResourceEntry("DefineAContentTypeOf", Description = "phrase: Define a content type of", LastModified = "2011/11/28", Value = "Define a content type of")]
    public string DefineAContentTypeOf => this[nameof (DefineAContentTypeOf)];

    /// <summary>Text of the back to module link.</summary>
    [ResourceEntry("BackToModule", Description = "Text of the back to module link.", LastModified = "2011/12/07", Value = "Back to Module")]
    public string BackToModule => this[nameof (BackToModule)];

    /// <summary>Text of the publication date header</summary>
    [ResourceEntry("PublicationDate", Description = "Text of the publication date header", LastModified = "2011/12/08", Value = "Publication date")]
    public string PublicationDate => this[nameof (PublicationDate)];

    /// <summary>Text of the created on date header</summary>
    [ResourceEntry("DateCreated", Description = "Text of the created on date header", LastModified = "2017/05/09", Value = "Created on")]
    public string DateCreated => this[nameof (DateCreated)];

    /// <summary>Text of the last modified date header</summary>
    [ResourceEntry("LastModified", Description = "Text of the last modified date header", LastModified = "2017/05/09", Value = "Last modified")]
    public string LastModified => this[nameof (LastModified)];

    /// <summary>
    /// Title of the type field editor when adding a new field.
    /// </summary>
    [ResourceEntry("AddAFieldTitle", Description = "Title of the type field editor when adding a new field.", LastModified = "2011/11/08", Value = "Add a field")]
    public string AddAFieldTitle => this[nameof (AddAFieldTitle)];

    /// <summary>
    /// Header title of the column that displays the type of the item field.
    /// </summary>
    [ResourceEntry("Type", Description = "Header title of the column that displays the type of the item field.", LastModified = "2011/10/02", Value = "Type")]
    public string Type => this[nameof (Type)];

    /// <summary>Example url</summary>
    [ResourceEntry("UrlNameExample", Description = "Example url", LastModified = "2011/10/02", Value = "Url example: my-first-blog-post")]
    public string UrlNameExample => this[nameof (UrlNameExample)];

    /// <summary>phrase: Short text</summary>
    [ResourceEntry("ShortText", Description = "phrase: Short text", LastModified = "2011/09/25", Value = "Short text")]
    public string ShortText => this[nameof (ShortText)];

    /// <summary>phrase: Long text</summary>
    [ResourceEntry("LongText", Description = "phrase: Long text", LastModified = "2011/09/25", Value = "Long text")]
    public string LongText => this[nameof (LongText)];

    /// <summary>phrase: Choices</summary>
    [ResourceEntry("MultipleChoice", Description = "phrase: Choices", LastModified = "2011/11/25", Value = "Choices")]
    public string MultipleChoice => this[nameof (MultipleChoice)];

    /// <summary>phrase: Yes / No</summary>
    [ResourceEntry("YesNo", Description = "phrase: Yes / No", LastModified = "2011/11/08", Value = "Yes / No")]
    public string YesNo => this[nameof (YesNo)];

    /// <summary>word: Currency</summary>
    [ResourceEntry("Currency", Description = "word: Currency", LastModified = "2011/11/08", Value = "Currency")]
    public string Currency => this[nameof (Currency)];

    /// <summary>phrase: Date and time</summary>
    [ResourceEntry("DateAndTime", Description = "phrase: Date and time", LastModified = "2011/11/08", Value = "Date and Time")]
    public string DateAndTime => this[nameof (DateAndTime)];

    /// <summary>word: Number</summary>
    [ResourceEntry("Number", Description = "word: Number", LastModified = "2011/11/08", Value = "Number")]
    public string Number => this[nameof (Number)];

    /// <summary>word: Classification</summary>
    [ResourceEntry("Classification", Description = "word: Classification", LastModified = "2011/11/08", Value = "Classification")]
    public string Classification => this[nameof (Classification)];

    /// <summary>word: Unknown</summary>
    [ResourceEntry("Unknown", Description = "word: Unknown", LastModified = "2011/11/08", Value = "Unknown")]
    public string Unknown => this[nameof (Unknown)];

    /// <summary>word: Guid</summary>
    [ResourceEntry("Guid", Description = "word: GUID", LastModified = "2012/02/10", Value = "GUID (Globally unique identifier)")]
    public string Guid => this[nameof (Guid)];

    /// <summary>phrase: Array of GUIDs</summary>
    [ResourceEntry("GuidArray", Description = "phrase: Array of GUIDs", LastModified = "2012/02/10", Value = "Array of GUIDs")]
    public string GuidArray => this[nameof (GuidArray)];

    /// <summary>
    /// phrase: This field has no user interface and is accessible only throuh API.
    /// </summary>
    [ResourceEntry("FieldAccessibleThroughAPI", Description = "phrase: This field has no user interface and is accessible only throuh API.", LastModified = "2012/02/10", Value = "This field has no user interface and is accessible only throuh API.")]
    public string FieldAccessibleThroughAPI => this[nameof (FieldAccessibleThroughAPI)];

    /// <summary>
    /// phrase: You don't need to add a GUID as identifier of this content, as this is done automatically.
    /// </summary>
    [ResourceEntry("NoNeedOfContentGuid", Description = "phrase: You don't need to add a GUID as identifier of this content, as this is done automatically.", LastModified = "2012/02/10", Value = "You don't need to add a GUID as identifier of this content, as this is done automatically.")]
    public string NoNeedOfContentGuid => this[nameof (NoNeedOfContentGuid)];

    /// <summary>
    /// phrase: Use this field to store information related to this content type, e.g. to an item from another type using API.
    /// </summary>
    [ResourceEntry("UseFieldForOneToOneRelation", Description = "phrase: Use this field to store information related to this content type, e.g. to an item from another type using API.", LastModified = "2012/02/10", Value = "Use this field to store information related to this content type, e.g. to relate an item from another type using API.")]
    public string UseFieldForOneToOneRelation => this[nameof (UseFieldForOneToOneRelation)];

    /// <summary>
    /// phrase: Use this field to store information related to this content type, e.g. to relate multiple items from another type using API.
    /// </summary>
    [ResourceEntry("UseFieldForOneToManyRelation", Description = "phrase: Use this field to store information related to this content type, e.g. to relate multiple items from another type using API.", LastModified = "2012/02/10", Value = "Use this field to store information related to this content type, e.g. to relate multiple items from another type using API.")]
    public string UseFieldForOneToManyRelation => this[nameof (UseFieldForOneToManyRelation)];

    /// <summary>word: Media field</summary>
    [ResourceEntry("Media", Description = "word: Media fields", LastModified = "2011/11/23", Value = "Media (images, videos, files)")]
    public string Media => this[nameof (Media)];

    /// <summary>Media field types text</summary>
    [ResourceEntry("MediaTypes", Description = "word: MediaTypes", LastModified = "2011/11/23", Value = "What kind of media this field will manage?")]
    public string MediaTypes => this[nameof (MediaTypes)];

    /// <summary>Image media field type</summary>
    [ResourceEntry("ImageMediaField", Description = "Image media field type radio button", LastModified = "2011/11/23", Value = "Images")]
    public string ImageMediaField => this[nameof (ImageMediaField)];

    /// <summary>Video media field type</summary>
    [ResourceEntry("VideoMediaField", Description = "Video media field type radio button", LastModified = "2011/11/23", Value = "Video")]
    public string VideoMediaField => this[nameof (VideoMediaField)];

    /// <summary>File media field type</summary>
    [ResourceEntry("FileMediaField", Description = "File media field type radio button", LastModified = "2011/12/05", Value = "Documents or other files")]
    public string FileMediaField => this[nameof (FileMediaField)];

    /// <summary>
    /// Header title of the column that displays the name of the item field.
    /// </summary>
    [ResourceEntry("Name", Description = "Header title of the column that displays the name of the item field.", LastModified = "2011/10/02", Value = "Name")]
    public string Name => this[nameof (Name)];

    /// <summary>
    /// Title of the tab for general settings of the dynamic field.
    /// </summary>
    [ResourceEntry("General", Description = "Title of the tab for general settings of the dynamic field", LastModified = "2011/11/08", Value = "General")]
    public string General => this[nameof (General)];

    /// <summary>
    /// Title of the tab for limitation settings of the dynamic field
    /// </summary>
    [ResourceEntry("Limitations", Description = "Title of the tab for limitation settings of the dynamic field", LastModified = "2011/11/08", Value = "Limitations")]
    public string Limitations => this[nameof (Limitations)];

    /// <summary>Title of the label for the dynamic field.</summary>
    [ResourceEntry("Label", Description = "Title of the label for the dynamic field.", LastModified = "2011/11/08", Value = "Label")]
    public string Label => this[nameof (Label)];

    /// <summary>
    /// Title of the label for the dynamic field when type of field is multiple choice.
    /// </summary>
    [ResourceEntry("LabelQuestion", Description = "Title of the label for the dynamic field when type of field is multiple choice.", LastModified = "2011/11/12", Value = "Label (Question)")]
    public string LabelQuestion => this[nameof (LabelQuestion)];

    /// <summary>Title of the range label for dynamic field.</summary>
    [ResourceEntry("Range", Description = "Title of the range label for dynamic field.", LastModified = "2011/11/08", Value = "Range")]
    public string Range => this[nameof (Range)];

    /// <summary>word: Done</summary>
    [ResourceEntry("Done", Description = "word: Done", LastModified = "2011/09/25", Value = "Done")]
    public string Done => this[nameof (Done)];

    /// <summary>word: Back</summary>
    [ResourceEntry("Back", Description = "word: Back", LastModified = "2011/11/08", Value = "Back")]
    public string Back => this[nameof (Back)];

    /// <summary>
    /// Label for the checkbox that determines if the field is hidden.
    /// </summary>
    [ResourceEntry("ThisIsAHiddenField", Description = "Label for the checkbox that determines if the field is hidden.", LastModified = "2011/11/08", Value = "This is a hidden field")]
    public string ThisIsAHiddenField => this[nameof (ThisIsAHiddenField)];

    /// <summary>phrase: Interface widget for entering data</summary>
    [ResourceEntry("InterfaceWidgetForEnteringData", Description = "phrase: Interface widget for entering data", LastModified = "2011/11/09", Value = "Interface widget for entering data")]
    public string InterfaceWidgetForEnteringData => this[nameof (InterfaceWidgetForEnteringData)];

    /// <summary>Name of the textbox widget for short text</summary>
    [ResourceEntry("Textbox", Description = "Name of the textbox widget for short text", LastModified = "2011/12/06", Value = "Textbox")]
    public string Textbox => this[nameof (Textbox)];

    /// <summary>Name of the textarea widget for long text</summary>
    [ResourceEntry("Textarea", Description = "Name of the textarea widget for long text", LastModified = "2011/12/06", Value = "Text area")]
    public string Textarea => this[nameof (Textarea)];

    /// <summary>
    /// The separator of widgets in the box for selecting widgets.
    /// </summary>
    [ResourceEntry("WidgetBoxSeparator", Description = "The separator of widgets in the box for selecting widgets.", LastModified = "2011/11/09", Value = "------------------------")]
    public string WidgetBoxSeparator => this[nameof (WidgetBoxSeparator)];

    /// <summary>Title of the option for selecting custom widget.</summary>
    [ResourceEntry("CustomWidget", Description = "Title of the option for selecting custom widget.", LastModified = "2011/11/09", Value = "Custom...")]
    public string CustomWidget => this[nameof (CustomWidget)];

    /// <summary>The name of the rich text editor widget.</summary>
    [ResourceEntry("RichTextEditor", Description = "The name of the rich text editor widget.", LastModified = "2011/11/09", Value = "Rich text editor")]
    public string RichTextEditor => this[nameof (RichTextEditor)];

    /// <summary>
    /// Widget for YesNo field
    /// phrase: Checkbox
    /// </summary>
    [ResourceEntry("YesNoCheckboxes", Description = "phrase: Checkbox", LastModified = "2011/12/02", Value = "Checkbox")]
    public string YesNoCheckboxes => this[nameof (YesNoCheckboxes)];

    /// <summary>The name of the radio button</summary>
    [ResourceEntry("RadioButtons", Description = "Using radio buttons for selection", LastModified = "2011/11/18", Value = "Radio buttons")]
    public string RadioButtons => this[nameof (RadioButtons)];

    /// <summary>The name of the drop down option</summary>
    [ResourceEntry("DropDownList", Description = "Using dropdownlist for selection", LastModified = "2011/11/18", Value = "Dropdown list")]
    public string DropDownList => this[nameof (DropDownList)];

    /// <summary>The name of the checkbox option</summary>
    [ResourceEntry("Checkboxes", Description = "Using dropdownlist for selection", LastModified = "2011/11/18", Value = "Checkboxes")]
    public string Checkboxes => this[nameof (Checkboxes)];

    /// <summary>
    /// Check box for dropdownlist multiple choice
    /// for "make required"
    /// </summary>
    [ResourceEntry("ddlMakeRequired", Description = "Make required", LastModified = "2011/11/18", Value = "Make required")]
    public string ddlMakeRequired => this[nameof (ddlMakeRequired)];

    /// <summary>
    /// phrase: Instructional choice (its value is not submitted)
    /// </summary>
    [ResourceEntry("InstructionalChoice", Description = "phrase: Instructional choice (its value is not submitted)", LastModified = "2011/12/01", Value = "Instructional choice (its value is not submitted)")]
    public string InstructionalChoice => this[nameof (InstructionalChoice)];

    /// <summary>
    /// Checkbox for multiple choice checkboxex
    /// indicating if it is required to select at least one
    /// </summary>
    [ResourceEntry("RequiredCheckBox", Description = "It's required at least one checkbox to be checked", LastModified = "2011/11/18", Value = "It's required at least one checkbox to be checked")]
    public string RequiredCheckBox => this[nameof (RequiredCheckBox)];

    /// <summary>
    /// Text of the radio button in the classification type field editor
    /// limitation, for selecting multiple items
    /// </summary>
    [ResourceEntry("MultipleClassificationItems", Description = "Multiple classification items can be selected", LastModified = "2011/11/18", Value = "Multiple classification items can be selected")]
    public string MultipleClassificationItems => this[nameof (MultipleClassificationItems)];

    /// <summary>
    /// Text of the question for the number of images
    /// which can be selected/uploaded
    /// </summary>
    [ResourceEntry("NumberOfImagesText", Description = "How many images can be uploaded or selected?", LastModified = "2011/11/23", Value = "How many images can be uploaded or selected?")]
    public string NumberOfImagesText => this[nameof (NumberOfImagesText)];

    /// <summary>
    /// Text of the question for the number of videos
    /// which can be selected/uploaded
    /// </summary>
    [ResourceEntry("NumberOfVideosText", Description = "How many videos can be uploaded or selected?", LastModified = "2011/11/23", Value = "How many videos can be uploaded or selected?")]
    public string NumberOfVideosText => this[nameof (NumberOfVideosText)];

    /// <summary>
    /// Text of the question for the number of files
    /// which can be selected/uploaded
    /// </summary>
    [ResourceEntry("NumberOfFilesText", Description = "How many files can be uploaded or selected?", LastModified = "2011/11/23", Value = "How many files can be uploaded or selected?")]
    public string NumberOfFilesText => this[nameof (NumberOfFilesText)];

    /// <summary>
    /// Text of the radio button for single image selection
    /// in the image field type
    /// </summary>
    [ResourceEntry("SingleImageSelectionText", Description = "Only 1 image can be uploaded or selected", LastModified = "2011/11/23", Value = "Only 1 image can be uploaded or selected")]
    public string SingleImageSelectionText => this[nameof (SingleImageSelectionText)];

    /// <summary>
    /// Text of the radio button for the multiple image selection
    /// in the image field type
    /// </summary>
    [ResourceEntry("MultipleImageSelectionText", Description = "Multiple images can be uploaded or selected", LastModified = "2011/11/23", Value = "Multiple images can be uploaded or selected")]
    public string MultipleImageSelectionText => this[nameof (MultipleImageSelectionText)];

    /// <summary>
    /// Text of the radio button for single video selection
    /// in the video field type
    /// </summary>
    [ResourceEntry("SingleVideoSelectionText", Description = "Only 1 video can be uploaded or selected", LastModified = "2011/11/23", Value = "Only 1 video can be uploaded or selected")]
    public string SingleVideoSelectionText => this[nameof (SingleVideoSelectionText)];

    /// <summary>
    /// Text of the radio button for the multiple video selection
    /// in the video field type
    /// </summary>
    [ResourceEntry("MultipleVideoSelectionText", Description = "Multiple videos can be uploaded or selected", LastModified = "2011/11/23", Value = "Multiple videos can be uploaded or selected")]
    public string MultipleVideoSelectionText => this[nameof (MultipleVideoSelectionText)];

    /// <summary>
    /// Text of the radio button for single file selection
    /// in the image file type
    /// </summary>
    [ResourceEntry("SingleFileSelectionText", Description = "Only 1 file can be uploaded or selected", LastModified = "2011/11/23", Value = "Only 1 file can be uploaded or selected")]
    public string SingleFileSelectionText => this[nameof (SingleFileSelectionText)];

    /// <summary>
    /// Text of the radio button for the multiple file selection
    /// in the file field type
    /// </summary>
    [ResourceEntry("MultipleFileSelectionText", Description = "Multiple files can be uploaded or selected", LastModified = "2011/11/23", Value = "Multiple files can be uploaded or selected")]
    public string MultipleFileSelectionText => this[nameof (MultipleFileSelectionText)];

    /// <summary>
    /// Text of the checkbox indicating whether an image library
    /// can be attached of not
    /// </summary>
    [ResourceEntry("AttachLibraryText", Description = "A library can be attached", LastModified = "2011/11/23", Value = "A library can be attached")]
    public string AttachLibraryText => this[nameof (AttachLibraryText)];

    /// <summary>Max upload file size</summary>
    [ResourceEntry("MaxFileSize", Description = "Max file size can be uploaded", LastModified = "2011/11/23", Value = "Max file size can be uploaded")]
    public string MaxFileSize => this[nameof (MaxFileSize)];

    /// <summary>word: MB</summary>
    [ResourceEntry("MB", Description = "word: MB for the file size", LastModified = "2011/11/23", Value = "MB")]
    public string MB => this[nameof (MB)];

    /// <summary>Allowed extensions for media types</summary>
    [ResourceEntry("AllowedExtensions", Description = "File extension allowed (comma separated)", LastModified = "2011/11/23", Value = "File extension allowed (comma separated)")]
    public string AllowedExtensions => this[nameof (AllowedExtensions)];

    /// <summary>
    /// Text of the radio button in the classification type field editor
    /// limitation, for selecting singe item
    /// </summary>
    [ResourceEntry("SingleClassificationItems", Description = "Only one classification item can be selected", LastModified = "2011/11/18", Value = "Only one classification item can be selected")]
    public string SingleClassificationItems => this[nameof (SingleClassificationItems)];

    /// <summary>
    /// Text of the checkbox in the limitations tab for classification
    /// ///
    /// </summary>
    [ResourceEntry("CreateItemsWhileSelecting", Description = "Allow creating classification items while selecting", LastModified = "2011/11/18", Value = "Allow creating classification items while selecting")]
    public string CreateItemsWhileSelecting => this[nameof (CreateItemsWhileSelecting)];

    /// <summary>The name of the textbox selector widget.</summary>
    [ResourceEntry("TextboxSelector", Description = "The name of the textbox selector widget.", LastModified = "2011/11/09", Value = "Textbox selector (select as you type)")]
    public string TextboxSelector => this[nameof (TextboxSelector)];

    /// <summary>The name of the image selector widget.</summary>
    [ResourceEntry("ImageSelector", Description = "The name of the image selector.", LastModified = "2011/11/23", Value = "Image selector")]
    public string ImageSelector => this[nameof (ImageSelector)];

    /// <summary>The name of the Video selector widget.</summary>
    [ResourceEntry("VideoSelector", Description = "The name of the video selector.", LastModified = "2011/11/23", Value = "Video selector")]
    public string VideoSelector => this[nameof (VideoSelector)];

    /// <summary>The name of the image selector widget.</summary>
    [ResourceEntry("FileSelector", Description = "The name of the file selector.", LastModified = "2011/11/23", Value = "File selector")]
    public string FileSelector => this[nameof (FileSelector)];

    /// <summary>The name of the tree-like selector widget.</summary>
    [ResourceEntry("TreelikeSelector", Description = "The name of the tree-like selector widget.", LastModified = "2011/11/09", Value = "Tree-like selector (select from a tree)")]
    public string TreelikeSelector => this[nameof (TreelikeSelector)];

    /// <summary>
    /// Title of the field for entering the type of custom widget.
    /// </summary>
    [ResourceEntry("CustomWidgetTitle", Description = "Title of the field for entering the type of custom widget.", LastModified = "2011/11/09", Value = "Type or Virtual path of the custom widget")]
    public string CustomWidgetTitle => this[nameof (CustomWidgetTitle)];

    /// <summary>
    /// Example for the possible values of custom widget type.
    /// </summary>
    [ResourceEntry("CustomWidgetExample", Description = "Example for the possible values of custom widget type.", LastModified = "2011/11/09", Value = "Type example: CustomControls.MyCustomControlVirtual path<br /> <strong>Example:</strong> ~/UserControls/MyUserControl.ascx")]
    public string CustomWidgetExample => this[nameof (CustomWidgetExample)];

    /// <summary>phrase: Decimal places</summary>
    [ResourceEntry("DecimalPlaces", Description = "phrase: Decimal places", LastModified = "2011/11/09", Value = "Decimal places")]
    public string DecimalPlaces => this[nameof (DecimalPlaces)];

    /// <summary>phrase: None (whole number)</summary>
    [ResourceEntry("NoneWholeNumber", Description = "phrase: None (whole number)", LastModified = "2011/11/09", Value = "None (whole number)")]
    public string NoneWholeNumber => this[nameof (NoneWholeNumber)];

    /// <summary>Title of the DBType field property</summary>
    [ResourceEntry("DBType", Description = "Title of the DBType field property", LastModified = "2011/11/10", Value = "DB Type")]
    public string DBType => this[nameof (DBType)];

    /// <summary>Title of the DBLenght field property</summary>
    [ResourceEntry("DBLength", Description = "Title of the DBLength field property", LastModified = "2011/11/10", Value = "DB Length")]
    public string DBLength => this[nameof (DBLength)];

    /// <summary>
    /// Title of the field property which determines weather null values can be inserted.
    /// </summary>
    [ResourceEntry("AllowEmptyValues", Description = "Title of the field property which determines weather null values can be inserted.", LastModified = "2011/11/10", Value = "Allow empty values")]
    public string AllowEmptyValues => this[nameof (AllowEmptyValues)];

    /// <summary>
    /// Title of the field property which determines weather field will be indexed.
    /// </summary>
    [ResourceEntry("IncludeInIndexes", Description = "Title of the field property which determines weather field will be indexed.", LastModified = "2011/11/10", Value = "Include in indexes")]
    public string IncludeInIndexes => this[nameof (IncludeInIndexes)];

    /// <summary>
    /// Title of the field property that determines the name of the column for the field.
    /// </summary>
    [ResourceEntry("ColumnName", Description = "Title of the field property that determines the name of the column for the field.", LastModified = "2011/11/10", Value = "Column name")]
    public string ColumnName => this[nameof (ColumnName)];

    /// <summary>Title of the field property for database precision</summary>
    [ResourceEntry("DBPrecision", Description = "Title of the field property for database precision", LastModified = "2011/11/10", Value = "DB Precision")]
    public string DBPrecision => this[nameof (DBPrecision)];

    /// <summary>Title of the field property for database scale</summary>
    [ResourceEntry("DBScale", Description = "Title of the field property for database scale", LastModified = "2011/11/10", Value = "DB Scale")]
    public string DBScale => this[nameof (DBScale)];

    /// <summary>
    /// Title of the expand advanced section of type editor link.
    /// </summary>
    [ResourceEntry("AdvancedDBType", Description = "Title of the expand advanced section of type editor link.", LastModified = "2011/11/10", Value = "Advanced (DB Type)")]
    public string AdvancedDBType => this[nameof (AdvancedDBType)];

    /// <summary>
    /// Title of the collapse advanced section of type editor link.
    /// </summary>
    [ResourceEntry("Advanced", Description = "Title of the collapse advanced section of type editor link.", LastModified = "2011/11/10", Value = "Advanced")]
    public string Advanced => this[nameof (Advanced)];

    /// <summary>Example advanced section of type editor link.</summary>
    [ResourceEntry("AdvancedDBTypeExample", Description = "Example advanced section of type editor link.", LastModified = "2011/11/22", Value = "(DB Type, SQL DB Type)")]
    public string AdvancedDBTypeExample => this[nameof (AdvancedDBTypeExample)];

    /// <summary>Description of the field title textbox.</summary>
    [ResourceEntry("FieldTitleDescription", Description = "Description of the field title textbox", LastModified = "2011/12/09", Value = "This is the label of the interface widget for entering data in the field")]
    public string FieldTitleDescription => this[nameof (FieldTitleDescription)];

    /// <summary>Description of the label textbox for YesNo field</summary>
    [ResourceEntry("YesNoFieldTitleDescription", Description = "Description of the label textbox for YesNo field", LastModified = "2011/12/09", Value = "This is the label of the checkbox")]
    public string YesNoFieldTitleDescription => this[nameof (YesNoFieldTitleDescription)];

    /// <summary>Description of the label textbox for datetime field</summary>
    [ResourceEntry("DateFieldTitleDescription", Description = "Description of the label textbox for datetime field", LastModified = "2011/12/09", Value = "This is the label of the interface widget for selecting date")]
    public string DateFieldTitleDescription => this[nameof (DateFieldTitleDescription)];

    /// <summary>
    /// Description of the label textbox for classification field
    /// </summary>
    [ResourceEntry("ClassificationTitleDescription", Description = "Description of the label textbox for classification field", LastModified = "2012/01/05", Value = "This is the label of the interface widget for selecting classification items")]
    public string ClassificationTitleDescription => this[nameof (ClassificationTitleDescription)];

    /// <summary>Description of the label textbox for image field</summary>
    [ResourceEntry("ImageTitleDescription", Description = "Description of the label textbox for image field", LastModified = "2011/12/09", Value = "This is the label of the interface widget for selecting images")]
    public string ImageTitleDescription => this[nameof (ImageTitleDescription)];

    /// <summary>Description of the label textbox for video field</summary>
    [ResourceEntry("VideoTitleDescription", Description = "Description of the label textbox for video field", LastModified = "2011/12/09", Value = "This is the label of the interface widget for selecting videos")]
    public string VideoTitleDescription => this[nameof (VideoTitleDescription)];

    /// <summary>Description of the label textbox for file field</summary>
    [ResourceEntry("FileTitleDescription", Description = "Description of the label textbox for file field", LastModified = "2011/12/09", Value = "This is the label of the interface widget for selecting files")]
    public string FileTitleDescription => this[nameof (FileTitleDescription)];

    /// <summary>phrase: Instructional text</summary>
    [ResourceEntry("InstructionalText", Description = "phrase: Instructional text", LastModified = "2011/11/12", Value = "Instructional text")]
    public string InstructionalText => this[nameof (InstructionalText)];

    /// <summary>
    /// Description of the input for instructional text of the field
    /// </summary>
    [ResourceEntry("InstructionalTextDescription", Description = "Description of the input for instructional text of the field", LastModified = "2011/12/09", Value = "Displayed bellow the widget. Suitable for giving examples")]
    public string InstructionalTextDescription => this[nameof (InstructionalTextDescription)];

    /// <summary>
    /// Description of the input for instructional text of the YesNo field
    /// </summary>
    [ResourceEntry("YesNoInstructionalTextDescription", Description = "Description of the input for instructional text of the YesNo field", LastModified = "2011/12/09", Value = "Displayed bellow the checkbox")]
    public string YesNoInstructionalTextDescription => this[nameof (YesNoInstructionalTextDescription)];

    /// <summary>
    /// Description of the input for instructional text of the DateTime field
    /// </summary>
    [ResourceEntry("DateTimeInstructionalTextDescription", Description = "Description of the input for instructional text of the DateTime field", LastModified = "2011/12/09", Value = "Displayed bellow the date picker")]
    public string DateTimeInstructionalTextDescription => this[nameof (DateTimeInstructionalTextDescription)];

    /// <summary>
    /// Description of the input for instructional text of the media fields
    /// </summary>
    [ResourceEntry("MediaInstructionalTextDescription", Description = "Description of the input for instructional text of the media fields", LastModified = "2011/12/09", Value = "Displayed bellow the label")]
    public string MediaInstructionalTextDescription => this[nameof (MediaInstructionalTextDescription)];

    /// <summary>phrase: Predefined value</summary>
    [ResourceEntry("PredefinedValue", Description = "phrase: Predefined value", LastModified = "2011/11/12", Value = "Predefined value")]
    public string PredefinedValue => this[nameof (PredefinedValue)];

    /// <summary>phrase: Predefined date value</summary>
    [ResourceEntry("PredefinedDateValue", Description = "phrase: Predefined date value", LastModified = "2011/12/09", Value = "Predefined date")]
    public string PredefinedDateValue => this[nameof (PredefinedDateValue)];

    /// <summary>word: Unit</summary>
    [ResourceEntry("NumberUnit", Description = "word: Unit", LastModified = "2011/12/01", Value = "Unit")]
    public string NumberUnit => this[nameof (NumberUnit)];

    /// <summary>phrase: Make required</summary>
    [ResourceEntry("MakeRequired", Description = "phrase: Make required", LastModified = "2011/11/12", Value = "This is  a required field")]
    public string MakeRequired => this[nameof (MakeRequired)];

    /// <summary>phrase: Time range allowed</summary>
    [ResourceEntry("TimeRange", Description = "phrase: Time range allowed", LastModified = "2011/12/09", Value = "Time range allowed")]
    public string TimeRange => this[nameof (TimeRange)];

    /// <summary>word: Length</summary>
    [ResourceEntry("Length", Description = "Number of characters allowed", LastModified = "2011/11/17", Value = "Number of characters allowed")]
    public string Length => this[nameof (Length)];

    /// <summary>word: Range</summary>
    [ResourceEntry("NumberRange", Description = "Range of a number field", LastModified = "2011/11/18", Value = "Range of the entered values")]
    public string NumberRange => this["Range"];

    /// <summary>phrase: Checked by default</summary>
    [ResourceEntry("CheckedByDefault", Description = "phrase: Checked by default", LastModified = "2011/12/02", Value = "Checked by default")]
    public string CheckedByDefault => this[nameof (CheckedByDefault)];

    /// <summary>Can multiple choices be selected</summary>
    [ResourceEntry("CanSelectMultiple", Description = "Multiple choices can be selected", LastModified = "2011/11/18", Value = "Multiple choices can be selected")]
    public string CanSelectMultiple => this[nameof (CanSelectMultiple)];

    /// <summary>phrase: Start date</summary>
    [ResourceEntry("StartDate", Description = "phrase: Start date", LastModified = "2011/12/09", Value = "Start date")]
    public string StartDate => this[nameof (StartDate)];

    /// <summary>phrase: End date</summary>
    [ResourceEntry("EndDate", Description = "phrase: End date", LastModified = "2011/12/09", Value = "End date")]
    public string EndDate => this[nameof (EndDate)];

    /// <summary>word: Min</summary>
    [ResourceEntry("Min", Description = "word: Min", LastModified = "2011/11/13", Value = "Min")]
    public string Min => this[nameof (Min)];

    /// <summary>word: Max</summary>
    [ResourceEntry("Max", Description = "word: Max", LastModified = "2011/11/13", Value = "Max")]
    public string Max => this[nameof (Max)];

    /// <summary>word: Characters</summary>
    [ResourceEntry("Characters", Description = "word: Characters", LastModified = "2011/11/13", Value = "characters")]
    public string Characters => this[nameof (Characters)];

    /// <summary>
    /// phrase: Error message displayed when the entry is outside of specified length.
    /// </summary>
    [ResourceEntry("LengthValidationMessage", Description = "Regular expression for validation.", LastModified = "2011/11/13", Value = "Advanced: Regular expression for validation.")]
    public string LengthValidationMessage => this[nameof (LengthValidationMessage)];

    /// <summary>word: Choices</summary>
    [ResourceEntry("Choices", Description = "word: Choices", LastModified = "2011/11/13", Value = "Choices")]
    public string Choices => this[nameof (Choices)];

    /// <summary>word: Choice 1</summary>
    [ResourceEntry("Choice1", Description = "word: Choice 1", LastModified = "2011/11/13", Value = "Choice 1")]
    public string Choice1 => this[nameof (Choice1)];

    /// <summary>word: Choice 2</summary>
    [ResourceEntry("Choice2", Description = "word: Choice 2", LastModified = "2011/11/13", Value = "Choice 2")]
    public string Choice2 => this[nameof (Choice2)];

    /// <summary>word: Choice 3</summary>
    [ResourceEntry("Choice3", Description = "word: Choice 3", LastModified = "2011/11/13", Value = "Choice 3")]
    public string Choice3 => this[nameof (Choice3)];

    /// <summary>phrase: Date field</summary>
    [ResourceEntry("DateField", Description = "phrase: Date field", LastModified = "2011/11/14", Value = "Date picker")]
    public string DateField => this[nameof (DateField)];

    /// <summary>word: Choices</summary>
    /// <value>Choices</value>
    [ResourceEntry("Choice", Description = "word: Choices", LastModified = "2012/11/21", Value = "Choices")]
    public string Choice => this[nameof (Choice)];

    /// <summary>word: Value</summary>
    /// <value>Value</value>
    [ResourceEntry("Value", Description = "word: Value", LastModified = "2012/11/22", Value = "Value")]
    public string Value => this[nameof (Value)];

    /// <summary>
    /// Description of the label textbox for multiple choice option
    /// </summary>
    /// <value>Label is the text users see in the interface</value>
    [ResourceEntry("ChoiceLabelDetails", Description = "Description of the label textbox for multiple choice option", LastModified = "2012/11/22", Value = "Label is the text users see in the interface")]
    public string ChoiceLabelDetails => this[nameof (ChoiceLabelDetails)];

    /// <summary>
    /// phrase: Value is the unique name of each choice. It is not visible by users in the interface.
    /// </summary>
    /// <value>Value is the unique name of each choice. It is not visible by users in the interface.</value>
    [ResourceEntry("ChoiceValueDetailsLine1", Description = "phrase: Value is the unique name of each choice. It is not visible by users in the interface.", LastModified = "2012/11/23", Value = "Value is the unique name of each choice. It is not visible by users in the interface.")]
    public string ChoiceValueDetailsLine1 => this[nameof (ChoiceValueDetailsLine1)];

    /// <summary>
    /// phrase: While labels can be changed or translated, values should stay the same.
    /// </summary>
    /// <value>While labels can be changed or translated, values should stay the same.</value>
    [ResourceEntry("ChoiceValueDetailsLine2", Description = "phrase: While labels can be changed or translated, values should stay the same.", LastModified = "2012/11/23", Value = "While labels can be changed or translated, values should stay the same. ")]
    public string ChoiceValueDetailsLine2 => this[nameof (ChoiceValueDetailsLine2)];

    /// <summary>phrase: Keep values as short as possible.</summary>
    /// <value>Keep values as short as possible.</value>
    [ResourceEntry("ChoiceValueDetailsLine3", Description = "phrase: Keep values as short as possible.", LastModified = "2012/11/23", Value = "Keep values as short as possible.")]
    public string ChoiceValueDetailsLine3 => this[nameof (ChoiceValueDetailsLine3)];

    /// <summary>
    /// phrase: Allowed characters are letters, numbers and \"_\"
    /// </summary>
    /// <value>Allowed characters are letters, numbers and \"_\"</value>
    [ResourceEntry("ChoiceValueDetailsLine4", Description = "phrase: Allowed characters are letters, numbers and \"_\"", LastModified = "2012/12/06", Value = "Allowed characters are letters, numbers and \"_\"")]
    public string ChoiceValueDetailsLine4 => this[nameof (ChoiceValueDetailsLine4)];

    /// <summary>phrase: How to translate?</summary>
    /// <value>How to translate?</value>
    [ResourceEntry("HowToTranslateLinkText", Description = "phrase: How to translate?", LastModified = "2012/11/23", Value = "How to translate?")]
    public string HowToTranslateLinkText => this[nameof (HowToTranslateLinkText)];

    /// <summary>
    /// phrase: If you want to have labels on several languages, you need web resoruces.
    /// </summary>
    /// <value>If you want to have labels on several languages, you need web resoruces.</value>
    [ResourceEntry("HowToTranslateDetailsLine1", Description = "phrase: If you want to have labels on several languages, you need web resoruces.", LastModified = "2012/12/06", Value = "If you want to have labels on several languages, you need web resoruces.")]
    public string HowToTranslateDetailsLine1 => this[nameof (HowToTranslateDetailsLine1)];

    /// <summary>Resource formula description</summary>
    /// <value>where &lt;ClassKey&gt; is the name of the resource class and &lt;ResourceKey&gt; identifies which key/value pair to be retrieved from the resource file.</value>
    [ResourceEntry("HowToTranslateResourceFormulaDescription", Description = "Resource formula description", LastModified = "2012/12/06", Value = "where &lt;ClassKey&gt; is the name of the resource class and &lt;ResourceKey&gt; identifies which key/value pair to be retrieved from the resource file.")]
    public string HowToTranslateResourceFormulaDescription => this[nameof (HowToTranslateResourceFormulaDescription)];

    /// <summary>
    /// phrase: Instead of typing the actual content, paste this
    /// </summary>
    /// <value>Instead of typing the actual content, paste this</value>
    [ResourceEntry("HowToTranslateDetailsLine2", Description = "phrase: Instead of typing the actual content, paste this", LastModified = "2012/12/06", Value = "Instead of typing the actual content, paste this")]
    public string HowToTranslateDetailsLine2 => this[nameof (HowToTranslateDetailsLine2)];

    /// <summary>
    /// phrase: Are you sure you want to delete {0} field and all its data?
    /// </summary>
    /// <value>Are you sure you want to delete {0} field and all its data?</value>
    [ResourceEntry("ConfirmFieldDeleteMessage", Description = "phrase: Are you sure you want to delete {0} field and all its data?", LastModified = "2013/10/29", Value = "Are you sure you want to delete {0} field and all its data?")]
    public string ConfirmFieldDeleteMessage => this[nameof (ConfirmFieldDeleteMessage)];

    /// <summary>phrase: Delete this field and all its data</summary>
    [ResourceEntry("DeleteFieldLabel", Description = "phrase: Delete this field and all its data", LastModified = "2013/10/29", Value = "Delete this field and all its data")]
    public string DeleteFieldLabel => this[nameof (DeleteFieldLabel)];

    /// <summary>
    /// phrase: Are you sure you want to delete {0} content type and all its data?
    /// </summary>
    /// <value>Are you sure you want to delete {0} content type and all its data?</value>
    [ResourceEntry("ConfirmTypeDeleteMessage", Description = "phrase: Are you sure you want to delete {0} content type and all its data?", LastModified = "2014/01/07", Value = "Are you sure you want to delete {0} content type and all its data?")]
    public string ConfirmTypeDeleteMessage => this[nameof (ConfirmTypeDeleteMessage)];

    /// <summary>phrase: Delete this content type and all its data</summary>
    [ResourceEntry("DeleteContentTypeLabel", Description = "phrase: Delete this content type and all its data", LastModified = "2014/01/07", Value = "Delete this content type and all its data")]
    public string DeleteContentTypeLabel => this[nameof (DeleteContentTypeLabel)];

    /// <summary>phrase: Data type</summary>
    /// <value>Data type</value>
    [ResourceEntry("DataType", Description = "phrase: Data type", LastModified = "2014/02/19", Value = "Data type")]
    public string DataType => this[nameof (DataType)];

    /// <summary>
    /// phrase: No spaces allowed. This is the name used in code and database
    /// </summary>
    /// <value>No spaces allowed. This is the name used in code and database</value>
    [ResourceEntry("NoSpacesAllowed", Description = "phrase: No spaces allowed. This is the name used in code and database", LastModified = "2014/03/18", Value = "No spaces allowed. This is the name used in code and database")]
    public string NoSpacesAllowed => this[nameof (NoSpacesAllowed)];

    /// <summary>phrase: Backend screens tweaks</summary>
    [ResourceEntry("BackendScreensTweaks", Description = "phrase: Backend screens tweaks", LastModified = "2011/11/26", Value = "Backend screens tweaks")]
    public string BackendScreensTweaks => this["BackendScreenTweaks"];

    /// <summary>
    /// Description of the purpose of the backend screens tweaks
    /// </summary>
    [ResourceEntry("BackendScreensTweaksDescription", Description = "Description of the purpose of the backend screens tweaks", LastModified = "2011/11/26", Value = "Add or remove columns to the table (grid) where items are listed; reorder or regroup fields in the form for creating/editing content items")]
    public string BackendScreensTweaksDescription => this[nameof (BackendScreensTweaksDescription)];

    /// <summary>phrase: {0} item fields</summary>
    [ResourceEntry("TypeItemFields", Description = "phrase: {0} item fields", LastModified = "2011/12/06", Value = "{0} item fields")]
    public string TypeItemFields => this[nameof (TypeItemFields)];

    /// <summary>
    /// The description of the functionality for editing fields of the dynamic module type.
    /// </summary>
    [ResourceEntry("TypeItemFieldsDescription", Description = "The description of the functionality for editing fields of the dynamic module type.", LastModified = "2011/12/06", Value = "Title, Content, Tags, Categories...")]
    public string TypeItemFieldsDescription => this[nameof (TypeItemFieldsDescription)];

    /// <summary>phrase: Add a content type.</summary>
    [ResourceEntry("AddContentType", Description = "phrase: Add a content type.", LastModified = "2012/03/12", Value = "Add a content type")]
    public string AddContentType => this[nameof (AddContentType)];

    /// <summary>
    /// phrase: Like Pages can have child-Pages and Categories can have child-Categories
    /// </summary>
    [ResourceEntry("PagesHaveChildPages", Description = "phrase: Like Pages can have child-Pages and Categories can have child-Categories", LastModified = "2012/04/19", Value = "Like <em>Pages</em> can have <em>child-Pages</em> and <em>Categories</em> can have <em>child-Categories</em>")]
    public string PagesHaveChildPages => this[nameof (PagesHaveChildPages)];

    /// <summary>phrase: Items can have child items from the same type</summary>
    [ResourceEntry("TypeHaveSameChildType", Description = "phrase: Items can have child items from the same type", LastModified = "2012/04/19", Value = "Items can have child items from the same type")]
    public string TypeHaveSameChildType => this[nameof (TypeHaveSameChildType)];

    /// <summary>phrase: Create a child item</summary>
    [ResourceEntry("CreateChildItem", Description = "phrase: Create a child item", LastModified = "2012/05/10", Value = "Create a child item")]
    public string CreateChildItem => this[nameof (CreateChildItem)];

    /// <summary>Title of the page for tweaking the backend screens</summary>
    [ResourceEntry("BackendScreensTweaksTitle", Description = "Title of the page for tweaking the backend screens", LastModified = "2011/11/26", Value = "Backend screens tweaks")]
    public string BackendScreensTweaksTitle => this[nameof (BackendScreensTweaksTitle)];

    /// <summary>
    /// Description of the page for tweaking the backend screens
    /// </summary>
    [ResourceEntry("BackendScreensTweaksPageDescription", Description = "Description of the page for tweaking the backend screens", LastModified = "2011/11/26", Value = "The page for tweaking the backend screens of the dynamic modules")]
    public string BackendScreensTweaksPageDescription => this[nameof (BackendScreensTweaksPageDescription)];

    /// <summary>
    /// Url name of the page for tweaking the backend screens of the dynamic modules
    /// </summary>
    [ResourceEntry("BackendScreensTweaksUrlName", Description = "Url name of the page for tweaking the backend screens of the dynamic modules", LastModified = "2011/11/26", Value = "backend-screens-tweaks")]
    public string BackendScreensTweaksUrlName => this[nameof (BackendScreensTweaksUrlName)];

    /// <summary>Title of the backend screens tweaks page</summary>
    [ResourceEntry("BackendScreensTweaksPageTitle", Description = "Title of the backend screens tweaks page", LastModified = "2011/11/26", Value = "Tweak backend screens used for...")]
    public string BackendScreensTweaksPageTitle => this[nameof (BackendScreensTweaksPageTitle)];

    /// <summary>phrase: Listing items</summary>
    [ResourceEntry("ListingItems", Description = "phrase: Listing {0}", LastModified = "2012/03/23", Value = "Listing {0}")]
    public string ListingItems => this[nameof (ListingItems)];

    /// <summary>
    /// Text of the button for tweaking the backend grid screen
    /// </summary>
    [ResourceEntry("GridTweakingButtonText", Description = "Text of the button for tweaking the backend grid screen", LastModified = "2011/11/26", Value = "Add / remove columns in the table (grid)")]
    public string GridTweakingButtonText => this[nameof (GridTweakingButtonText)];

    /// <summary>phrase: Creating / Editing items</summary>
    [ResourceEntry("CreatingEditingItems", Description = "phrase: Creating / Editing {0}", LastModified = "2012/03/23", Value = "Creating / Editing {0}")]
    public string CreatingEditingItems => this[nameof (CreatingEditingItems)];

    /// <summary>
    /// Text of the button for tweaking the backend form screens
    /// </summary>
    [ResourceEntry("FormsTweakingButtonText", Description = "Text of the button for tweaking the backend form screens", LastModified = "2012/03/23", Value = "Reorder fields")]
    public string FormsTweakingButtonText => this[nameof (FormsTweakingButtonText)];

    /// <summary>Text of the link to go back to backend screens tweaks</summary>
    [ResourceEntry("BackToBackendScreensTweaks", Description = "Text of the link to go back to backend screens tweaks", LastModified = "2011/11/26", Value = "Back to Backend screens tweaks")]
    public string BackToBackendScreensTweaks => this[nameof (BackToBackendScreensTweaks)];

    /// <summary>
    /// Title of the grid editor on the backend screens tweaks
    /// </summary>
    [ResourceEntry("GridEditorTitle", Description = "Title of the grid editor on the backend screens tweaks", LastModified = "2019/10/15", Value = "Edit the columns for listing {0}")]
    public string GridEditorTitle => this[nameof (GridEditorTitle)];

    /// <summary>
    /// Title of the form editor on the backend screens tweaks
    /// </summary>
    [ResourceEntry("FormEditorTitle", Description = "Title of the form editor on the backend screens tweaks", LastModified = "2011/11/26", Value = "Reorder fields for creating / editing {0}")]
    public string FormEditorTitle => this[nameof (FormEditorTitle)];

    /// <summary>phrase: Save changes</summary>
    [ResourceEntry("SaveChanges", Description = "phrase: Save changes", LastModified = "2011/11/26", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>word: Preview</summary>
    [ResourceEntry("Preview", Description = "word: Preview", LastModified = "2011/11/26", Value = "Preview")]
    public string Preview => this[nameof (Preview)];

    /// <summary>phrase: Existing columns</summary>
    [ResourceEntry("ExistingColumns", Description = "phrase: Existing columns", LastModified = "2011/11/26", Value = "Existing columns")]
    public string ExistingColumns => this[nameof (ExistingColumns)];

    /// <summary>phrase: Drag columns</summary>
    [ResourceEntry("DragColumns", Description = "phrase: Drag columns", LastModified = "2011/11/26", Value = "Drag columns")]
    public string DragColumns => this[nameof (DragColumns)];

    /// <summary>phrase: Drag between fields...</summary>
    [ResourceEntry("DragBetweenFields", Description = "phrase: Drag between fields...", LastModified = "2011/11/26", Value = "Drag between fields...")]
    public string DragBetweenFields => this[nameof (DragBetweenFields)];

    /// <summary>phrase: Section title</summary>
    [ResourceEntry("SectionTitle", Description = "phrase: Section title", LastModified = "2011/11/26", Value = "Section title")]
    public string SectionTitle => this[nameof (SectionTitle)];

    /// <summary>word: Added</summary>
    [ResourceEntry("Added", Description = "word: Added", LastModified = "2011/11/26", Value = "Added")]
    public string Added => this[nameof (Added)];

    /// <summary>phrase: Collapsed by default</summary>
    [ResourceEntry("CollapsedByDefault", Description = "phrase: Collapsed by default", LastModified = "2011/11/27", Value = "Collapsed by default")]
    public string CollapsedByDefault => this[nameof (CollapsedByDefault)];

    /// <summary>
    /// phrase: Display title and options for expanding / collapsing
    /// </summary>
    [ResourceEntry("DisplayTitle", Description = "phrase: Display title and options for expanding / collapsing", LastModified = "2011/12/01", Value = "Display title and options for expanding / collapsing ")]
    public string DisplayTitle => this[nameof (DisplayTitle)];

    /// <summary>word: Save</summary>
    [ResourceEntry("Save", Description = "word: Save", LastModified = "2011/11/27", Value = "Save")]
    public string Save => this[nameof (Save)];

    /// <summary>word: Title</summary>
    [ResourceEntry("Title", Description = "word: Title", LastModified = "2011/11/27", Value = "Title")]
    public string Title => this[nameof (Title)];

    /// <summary>phrase: All about this module</summary>
    [ResourceEntry("AllAboutThisModule", Description = "phrase: All about this module", LastModified = "2011/12/09", Value = "All about this module")]
    public string AllAboutThisModule => this[nameof (AllAboutThisModule)];

    /// <summary>Title of the module builder module.</summary>
    [ResourceEntry("ModuleTitle", Description = "Title of the module builder module.", LastModified = "2011/08/13", Value = "Module Builder")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Description of the module builder module.</summary>
    [ResourceEntry("ModuleDescription", Description = "Description of the module builder module.", LastModified = "2011/08/13", Value = "Module which allows creation of new dynamic modules through user interface.")]
    public string ModuleDescription => this[nameof (ModuleDescription)];

    /// <summary>Url name of the module builder module.</summary>
    [ResourceEntry("ModuleUrlName", Description = "Url name of the module builder module.", LastModified = "2011/08/13", Value = "module-builder")]
    public string ModuleUrlName => this[nameof (ModuleUrlName)];

    /// <summary>Url name of the module builder dashboard page.</summary>
    [ResourceEntry("ModuleBuilderDashboardUrlName", Description = "Url name of the module builder dashboard page.", LastModified = "2011/08/13", Value = "dashboard")]
    public string ModuleBuilderDashboardUrlName => this[nameof (ModuleBuilderDashboardUrlName)];

    /// <summary>The title of the module page group</summary>
    [ResourceEntry("ContentTypePageGroupTitle", Description = "The title of the module page group", LastModified = "2011/11/07", Value = "Module")]
    public string ContentTypePageGroupTitle => this[nameof (ContentTypePageGroupTitle)];

    /// <summary>The description of the module page group</summary>
    [ResourceEntry("ContentTypePageGroupDescription", Description = "The description of the module page group", LastModified = "2011/11/07", Value = "The page group which holds all the pages for working with single module")]
    public string ContentTypePageGroupDescription => this[nameof (ContentTypePageGroupDescription)];

    /// <summary>The url name of the module page group</summary>
    [ResourceEntry("ContentTypePageGroupUrlName", Description = "The url name of the module page group", LastModified = "2011/11/07", Value = "module")]
    public string ContentTypePageGroupUrlName => this[nameof (ContentTypePageGroupUrlName)];

    /// <summary>Title of the page that hosts module dashboard.</summary>
    [ResourceEntry("ContentTypeDashboardTitle", Description = "Title of the page that hosts module dashboard.", LastModified = "2011/08/17", Value = "Module dashboard")]
    public string ContentTypeDashboardTitle => this[nameof (ContentTypeDashboardTitle)];

    /// <summary>Description of the page that hosts module dashboard.</summary>
    [ResourceEntry("ContentTypeDashboardDescription", Description = "Description of the page that hosts module dashboard.", LastModified = "2011/08/17", Value = "Page that allows you to work with a single module.")]
    public string ContentTypeDashboardDescription => this[nameof (ContentTypeDashboardDescription)];

    /// <summary>Url name of the page that hosts module dashboard.</summary>
    [ResourceEntry("ContentTypeDashboardUrlName", Description = "Url name of the page that hosts module dashboard.", LastModified = "2011/08/17", Value = "type")]
    public string ContentTypeDashboardUrlName => this[nameof (ContentTypeDashboardUrlName)];

    /// <summary>The title of the code reference page</summary>
    [ResourceEntry("CodeReferenceTitle", Description = "The title of the code reference page", LastModified = "2011/11/07", Value = "Code reference")]
    public string CodeReferenceTitle => this[nameof (CodeReferenceTitle)];

    /// <summary>The description of the code reference page</summary>
    [ResourceEntry("CodeReferenceDescription", Description = "The description of the code reference page", LastModified = "2011/11/07", Value = "The page that displays code reference for the modules.")]
    public string CodeReferenceDescription => this[nameof (CodeReferenceDescription)];

    /// <summary>The url name of the code reference page</summary>
    [ResourceEntry("CodeReferenceUrlName", Description = "The url name of the code reference page", LastModified = "2011/11/07", Value = "code-reference")]
    public string CodeReferenceUrlName => this[nameof (CodeReferenceUrlName)];

    /// <summary>word: Delete</summary>
    [ResourceEntry("Delete", Description = "word: Delete", LastModified = "2011/10/25", Value = "Delete")]
    public string Delete => this[nameof (Delete)];

    /// <summary>
    /// Message shown when user tries to create a custom type without any custom fields.
    /// </summary>
    [ResourceEntry("NoFieldsDefined", Description = "Message shown when user tries to create a custom type without any custom fields.", LastModified = "2011/10/27", Value = "No fields have been defined. In order to create a custom type you need to define at least one required short text field.")]
    public string NoFieldsDefined => this[nameof (NoFieldsDefined)];

    /// <summary>
    /// Message shown when user tries to create a module with a name that already exists.
    /// </summary>
    [ResourceEntry("ModuleWithThisNameExistsError", Description = "Message shown when user tries to create a module with name already exists.", LastModified = "2011/11/24", Value = "Module with this name already exists.")]
    public string ModuleWithThisNameExistsError => this[nameof (ModuleWithThisNameExistsError)];

    /// <summary>
    /// Message shown if user tries to continue to step 2 without entering valid module name.
    /// </summary>
    [ResourceEntry("EnterValidModuleName", Description = "Message shown if user tries to continue to step 2 without entering valid module name.", LastModified = "2011/11/25", Value = "Please enter valid module name. It should not be empty and can contain spaces and any word character: a-z, A-Z, _, 0-9")]
    public string EnterValidModuleName => this[nameof (EnterValidModuleName)];

    /// <summary>
    /// Message shown when user tries to create a custom type without any short text fields.
    /// </summary>
    [ResourceEntry("NoShortTextFieldsDefined", Description = "Message shown when user tries to create a custom type without any short text fields.", LastModified = "2011/11/22", Value = "No required short text fields have been defined. In order to create a custom type you need to define at least one required short text field.")]
    public string NoShortTextFieldsDefined => this[nameof (NoShortTextFieldsDefined)];

    /// <summary>
    /// Message shown when user tries to create field with a name that already exists.
    /// </summary>
    [ResourceEntry("TwoFieldsWithTheSameNameErrorMessage", Description = "Message shown when user tries to create field with a name that already exists.", LastModified = "2011/11/24", Value = "Cannot add two fields with the same name. Please enter different names for fields.")]
    public string TwoFieldsWithTheSameNameErrorMessage => this[nameof (TwoFieldsWithTheSameNameErrorMessage)];

    /// <summary>Text of the link to go back to all modules.</summary>
    [ResourceEntry("AllContentTypes", Description = "Text of the link to go back to all modules", LastModified = "2011/11/03", Value = "All modules")]
    public string AllContentTypes => this[nameof (AllContentTypes)];

    /// <summary>Status of the active module.</summary>
    [ResourceEntry("Active", Description = "Status of the active module.", LastModified = "2011/11/03", Value = "Active")]
    public string Active => this[nameof (Active)];

    /// <summary>Status of the inactive module.</summary>
    [ResourceEntry("Inactive", Description = "Status of the installed, but inactive module.", LastModified = "2011/11/03", Value = "Inactive")]
    public string Inactive => this[nameof (Inactive)];

    /// <summary>Status of the not installed module.</summary>
    [ResourceEntry("NotInstalled", Description = "Status of the not installed module.", LastModified = "2011/11/03", Value = "Not installed")]
    public string NotInstalled => this[nameof (NotInstalled)];

    /// <summary>Text of the button that installs a module.</summary>
    [ResourceEntry("InstallThisContentType", Description = "Text of the button that installs a module.", LastModified = "2011/11/21", Value = "<span class=\"sfLinkBtnIn\">Install this module</span>")]
    public string InstallThisContentType => this[nameof (InstallThisContentType)];

    /// <summary>Text of the button that activates a module.</summary>
    [ResourceEntry("ActivateThisContentType", Description = "Text of the button that activates a module.", LastModified = "2011/11/03", Value = "<span class=\"sfLinkBtnIn\">Activate this module</span>")]
    public string ActivateThisContentType => this[nameof (ActivateThisContentType)];

    /// <summary>Text of the button that inactivates a module.</summary>
    [ResourceEntry("InactivateThisContentType", Description = "Text of the button that deactivaes a module.", LastModified = "2011/11/03", Value = "Deactivate this module")]
    public string InactivateThisContentType => this[nameof (InactivateThisContentType)];

    /// <summary>
    /// The text displayed during the installation of the dynamic module.
    /// </summary>
    [ResourceEntry("InstallationInProgressPleaseWait", Description = "The text displayed during the installation of the dynamic module.", LastModified = "2011/11/23", Value = "Installation in progress... Please wait.")]
    public string InstallationInProgressPleaseWait => this[nameof (InstallationInProgressPleaseWait)];

    /// <summary>
    /// The text displayed during the activation of the dynamic module.
    /// </summary>
    [ResourceEntry("ActivationInProgressPleaseWait", Description = "The text displayed during the activation of the dynamic module.", LastModified = "2011/11/03", Value = "Activation in progress... Please wait.")]
    public string ActivationInProgressPleaseWait => this[nameof (ActivationInProgressPleaseWait)];

    /// <summary>
    /// The text displayed during the deactivation of the dynamic module.
    /// </summary>
    [ResourceEntry("DeactivationInProgressPleaseWait", Description = "The text displayed during the deactivation of the dynamic module.", LastModified = "2011/11/22", Value = "Deactivation in progress... Please wait.")]
    public string DeactivationInProgressPleaseWait => this[nameof (DeactivationInProgressPleaseWait)];

    /// <summary>Title of the URL name field.</summary>
    [ResourceEntry("UrlNameTitle", Description = "Title of the URL name field.", LastModified = "2011/11/04", Value = "URL")]
    public string UrlNameTitle => this[nameof (UrlNameTitle)];

    /// <summary>
    /// Error message displayed to the user if the url name property is too long.
    /// </summary>
    [ResourceEntry("UrlNameTooLong", Description = "Error message displayed to the user if the url name property is too long.", LastModified = "2011/11/04", Value = "Url name value is too long. Maximum allowed is 255 characters.")]
    public string UrlNameTooLong => this[nameof (UrlNameTooLong)];

    /// <summary>
    /// Error message displayed to the user if the url name property is empty string.
    /// </summary>
    [ResourceEntry("UrlNameCannotBeEmpty", Description = "Error message displayed to the user if the url name is empty string.", LastModified = "2011/11/04", Value = "Url name cannot be empty.")]
    public string UrlNameCannotBeEmpty => this[nameof (UrlNameCannotBeEmpty)];

    /// <summary>
    /// Error message displayed to the user if the url contains invalid symbols.
    /// </summary>
    [ResourceEntry("UrlNameInvalidSymbols", Description = "Error message displayed to the user if the url contains invalid symbols.", LastModified = "2012/04/10", Value = "The URL contains invalid symbols.")]
    public string UrlNameInvalidSymbols => this[nameof (UrlNameInvalidSymbols)];

    /// <summary>
    /// Title of the link that leads to code reference for the custom modules.
    /// </summary>
    [ResourceEntry("CodeReferenceForContentType", Description = "Title of the link that leads to code reference for the custom modules.", LastModified = "2011/11/07", Value = "Code reference for {0}")]
    public string CodeReferenceForContentType => this[nameof (CodeReferenceForContentType)];

    /// <summary>Description of the 'Code reference' link</summary>
    [ResourceEntry("CodeReferenceForContentTypeDescription", Description = "Description of the 'Code reference' link", LastModified = "2011/11/07", Value = "Ready to use code-snippets for this module.")]
    public string CodeReferenceForContentTypeDescription => this[nameof (CodeReferenceForContentTypeDescription)];

    /// <summary>
    /// The message shown when no items have yet been created.
    /// </summary>
    [ResourceEntry("NoDynamicModuleItemsHaveBeenCreatedYet", Description = "The message shown when no items have yet been created.", LastModified = "2011/11/15", Value = "No {0} have been created yet")]
    public string NoDynamicModuleItemsHaveBeenCreatedYet => this[nameof (NoDynamicModuleItemsHaveBeenCreatedYet)];

    /// <summary>Messsage: Creates new item</summary>
    /// <value>Label of the dialog that creates a new item.</value>
    [ResourceEntry("CreateItem", Description = "Label of the dialog that creates a news item.", LastModified = "2011/11/15", Value = "Create {0} {1}")]
    public string CreateItem => this[nameof (CreateItem)];

    /// <summary>Message: Back to {0}</summary>
    /// <value>Text of the link that will show the view that lists all parent items of content type.</value>
    [ResourceEntry("BackToParent", Description = "Text of the link that will show the view that lists all parent items of content type.", LastModified = "2012/07/04", Value = "Back to {0}")]
    public string BackToParent => this[nameof (BackToParent)];

    /// <summary>phrase: Manage also.</summary>
    [ResourceEntry("ManageAlso", Description = "phrase: Manage also", LastModified = "2011/11/15", Value = "Manage also")]
    public string ManageAlso => this[nameof (ManageAlso)];

    /// <summary>phrase: Comments for news.</summary>
    [ResourceEntry("NavigatetoContentTypes", Description = "phrase: Navigates to content types", LastModified = "2011/11/15", Value = "Content types")]
    public string NavigatetoContentTypes => this[nameof (NavigatetoContentTypes)];

    /// <summary>word: Actions</summary>
    [ResourceEntry("Actions", Description = "word: Actions", LastModified = "2011/11/17", Value = "Actions")]
    public string Actions => this[nameof (Actions)];

    /// <summary>word: Settings</summary>
    [ResourceEntry("Settings", Description = "word: Settings", LastModified = "2011/11/23", Value = "Settings")]
    public string Settings => this[nameof (Settings)];

    /// <summary>Pages where items like this are published</summary>
    [ResourceEntry("PagesWhereTheseItemsArePublished", Description = "Pages where items like this are published", LastModified = "2013/03/25", Value = "Pages where items like this are published")]
    public string PagesWhereTheseItemsArePublished => this[nameof (PagesWhereTheseItemsArePublished)];

    /// <summary>word: Permissions</summary>
    [ResourceEntry("Permissions", Description = "word: Permissions", LastModified = "2011/11/23", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>
    /// Permissions description showed in the content type dashboard
    /// </summary>
    [ResourceEntry("PermissionsDescription", Description = "Permissions description showed in the content type dashboard", LastModified = "2011/12/05", Value = "Who can create, edit, delete the items of this module")]
    public string PermissionsDescription => this[nameof (PermissionsDescription)];

    /// <summary>Used to format the title of the preview detail view</summary>
    [ResourceEntry("PreviewDetailViewTitle", Description = "Used to format the title of the preview detail view", LastModified = "2011/11/23", Value = "Preview")]
    public string PreviewDetailViewTitle => this[nameof (PreviewDetailViewTitle)];

    /// <summary>Used to format the title of the insert detail view</summary>
    [ResourceEntry("InsertDetailViewTitle", Description = "Used to format the title of the insert detail view", LastModified = "2011/11/23", Value = "Create")]
    public string InsertDetailViewTitle => this[nameof (InsertDetailViewTitle)];

    /// <summary>Used to format the title of the edit detail view</summary>
    [ResourceEntry("EditDetailViewTitle", Description = "Used to format the title of the edit detail view", LastModified = "2011/11/23", Value = "Edit")]
    public string EditDetailViewTitle => this[nameof (EditDetailViewTitle)];

    /// <summary>
    /// Used to format the title of the sidebar of specific dynamic module
    /// </summary>
    [ResourceEntry("ManageDynamicModuleSideBarTitle", Description = "Used to format the title of the sidebar of specific dynamic module", LastModified = "2011/11/28", Value = "Manage")]
    public string ManageDynamicModuleSideBarTitle => this[nameof (ManageDynamicModuleSideBarTitle)];

    /// <summary>
    /// Displays the title when trying to change module name and description
    /// </summary>
    [ResourceEntry("NameAndDescriptionOfModule", Description = "Displays the title when trying to change module name and description", LastModified = "2011/12/01", Value = "Name and description")]
    public string NameAndDescriptionOfModule => this[nameof (NameAndDescriptionOfModule)];

    /// <summary>
    /// Description of the edit module name and description link
    /// </summary>
    [ResourceEntry("NameAndDescriptionOfModuleDescription", Description = "Description of the edit module name and description link", LastModified = "2011/12/01", Value = "Edit the name and description of this module")]
    public string NameAndDescriptionOfModuleDescription => this[nameof (NameAndDescriptionOfModuleDescription)];

    /// <summary>phrase: All items</summary>
    [ResourceEntry("AllItems", Description = "phrase: All items", LastModified = "2011/12/03", Value = "All items")]
    public string AllItems => this[nameof (AllItems)];

    /// <summary>phrase: All {0}</summary>
    [ResourceEntry("AllItemsFormatted", Description = "phrase: All {0}", LastModified = "2011/12/12", Value = "All {0}")]
    public string AllItemsFormated => this["AllItemsFormatted"];

    /// <summary>phrase: My {0}</summary>
    [ResourceEntry("MyItemsFormatted", Description = "phrase: My {0}", LastModified = "2011/12/12", Value = "My {0}")]
    public string MyItemsFormatted => this[nameof (MyItemsFormatted)];

    /// <summary>phrase: Loading</summary>
    [ResourceEntry("Loading", Description = "phrase: Loading", LastModified = "2011/12/08", Value = "Loading...")]
    public string Loading => this[nameof (Loading)];

    /// <summary>phrase: Filter {0}</summary>
    [ResourceEntry("FilterSectionTitle", Description = "phrase: Filter {0}", LastModified = "2011/12/12", Value = "Filter {0}")]
    public string FilterSectionTitle => this[nameof (FilterSectionTitle)];

    /// <summary>
    /// Dysplayed when there is an error sending request to server to check module name
    /// </summary>
    [ResourceEntry("ErrorCheckingModuleNameRequest", Description = "Dysplayed when there is an error sending request to server to check module name", LastModified = "2011/12/14", Value = "There has been an error checking module name. Please try again.")]
    public string ErrorCheckingModuleNameRequest => this[nameof (ErrorCheckingModuleNameRequest)];

    /// <summary>phrase: Name cannot be empty</summary>
    [ResourceEntry("CannotBeEmpty", Description = "phrase: {0} cannot be empty", LastModified = "2011/12/14", Value = "cannot be empty")]
    public string CannotBeEmpty => this[nameof (CannotBeEmpty)];

    /// <summary>
    /// phrase: is invalid. Only these characters are allowed: letters, numbers, spaces and _. Don't start with a number.
    /// </summary>
    [ResourceEntry("RegularExpressionErrorMessage", Description = "phrase:  is invalid. Only these characters are allowed: letters, numbers, spaces and _ . Don't start with a number.", LastModified = "2011/12/14", Value = "is invalid. Only these characters are allowed: letters, numbers, spaces and _. Don't start with a number.")]
    public string RegularExpressionErrorMessage => this[nameof (RegularExpressionErrorMessage)];

    /// <summary>
    /// phrase:  is invalid. Only these characters are allowed: letters, numbers and _. Don't start with a number."
    /// </summary>
    [ResourceEntry("RegularExpressionDevNameErrorMessage", Description = "phrase:  is invalid. Only these characters are allowed: letters, numbers and _. Don't start with a number.", LastModified = "2011/12/14", Value = "is invalid. Only these characters are allowed: letters, numbers and _. Don't start with a number.")]
    public string RegularExpressionDevNameErrorMessage => this[nameof (RegularExpressionDevNameErrorMessage)];

    /// <summary>
    /// phrase: An error has occurred while trying to create a new module.
    /// </summary>
    [ResourceEntry("ErrorCreatingNewModule", Description = "phrase: An error has occurred while trying to create a new module.", LastModified = "2012/01/05", Value = "An error has occurred while trying to create a new module.")]
    public string ErrorCreatingNewModule => this[nameof (ErrorCreatingNewModule)];

    /// <summary>
    /// phrase: An error has occurred while trying to delete the module.
    /// </summary>
    [ResourceEntry("ErrorDeletingModule", Description = "phrase: An error has occurred while trying to delete the module.", LastModified = "2012/01/17", Value = "An error has occurred while trying to delete the module.")]
    public string ErrorDeletingModule => this[nameof (ErrorDeletingModule)];

    /// <summary>
    /// phrase: Are you sure you want to delete the {0} module and all its data?
    /// </summary>
    [ResourceEntry("ConfirmModuleDeleteMessage", Description = "phrase: Are you sure you want to delete the {0} module and all its data?", LastModified = "2012/03/15", Value = "Are you sure you want to delete the {0} module and all its data?")]
    public string ConfirmModuleDeleteMessage => this[nameof (ConfirmModuleDeleteMessage)];

    /// <summary>
    /// phrase: Are you really sure you want to delete the {0} module and all its data?
    /// </summary>
    [ResourceEntry("SecondConfirmDeleteModuleMessage", Description = "phrase: Are you really sure you want to delete the {0} module and all its data?", LastModified = "2012/03/15", Value = "Are you really sure you want to delete the {0} module and all its data?")]
    public string SecondConfirmModuleDeleteMessage => this["SecondConfirmDeleteModuleMessage"];

    /// <summary>
    /// phrase: It will be gone forever, and cannot be recovered.
    /// </summary>
    [ResourceEntry("DataWillNotRecover", Description = "phrase: It will be gone forever, and cannot be recovered.", LastModified = "2012/01/23", Value = "It will be gone forever, and cannot be recovered.")]
    public string DataWillNotRecover => this[nameof (DataWillNotRecover)];

    /// <summary>phrase: Yes, Delete it</summary>
    [ResourceEntry("YesDeleteIt", Description = "phrase: Yes, Delete it", LastModified = "2012/01/23", Value = "Yes, Delete it")]
    public string YesDeleteIt => this[nameof (YesDeleteIt)];

    /// <summary>phrase: You cannot delete data tables from database.</summary>
    [ResourceEntry("ErrorDeletingModuleData", Description = "phrase: You cannot delete data tables from database.", LastModified = "2012/01/19", Value = "You cannot delete data tables from database.")]
    public string ErrorDeletingModuleData => this[nameof (ErrorDeletingModuleData)];

    /// <summary>phrase: Delete Module Confirmation.</summary>
    [ResourceEntry("DeleteModuleNotification", Description = "phrase: Delete Module Notification.", LastModified = "2012/01/31", Value = "Delete Module Notification.")]
    public string DeleteModuleNotification => this[nameof (DeleteModuleNotification)];

    /// <summary>
    /// phrase: So, you can delete the module but its data will stay in the database.
    /// </summary>
    [ResourceEntry("YouCanDeleteModuleMessage", Description = "phrase: So, you can delete the module but its data will stay in the database.", LastModified = "2012/01/19", Value = "So, you can delete the module but its data will stay in the database.")]
    public string YouCanDeleteModuleMessage => this[nameof (YouCanDeleteModuleMessage)];

    /// <summary>
    /// phrase: If you want to delete data tables from the database, contact your administrator.
    /// </summary>
    [ResourceEntry("ContactAdminMessage", Description = "phrase: If you want to delete data tables from the database, contact your administrator.", LastModified = "2012/01/19", Value = "If you want to delete data tables from the database, contact your administrator.")]
    public string ContactAdminMessage => this[nameof (ContactAdminMessage)];

    /// <summary>phrase: Anyway, Delete the module but not its data</summary>
    [ResourceEntry("DeleteModuleWithoutData", Description = "phrase: Anyway, Delete the module but not its data", LastModified = "2012/01/17", Value = "Anyway, Delete the module but not its data")]
    public string DeleteModuleWithoutData => this[nameof (DeleteModuleWithoutData)];

    /// <summary>phrase: Delete this module and all its data</summary>
    [ResourceEntry("DeleteModule", Description = "phrase: Delete this module and all its data", LastModified = "2012/01/17", Value = "Delete this module and all its data")]
    public string DeleteModule => this[nameof (DeleteModule)];

    /// <summary>
    /// phrase: There has been an error processing the request. Dynamic module with the requested id doesn't exist!
    /// </summary>
    [ResourceEntry("ErrorProcessingRequest", Description = "phrase: There has been an error processing the request. Dynamic module with the requested id doesn't exist!", LastModified = "2012/01/05", Value = "There has been an error processing the request. Dynamic module with the requested id doesn't exist!")]
    public string ErrorProcessingRequest => this[nameof (ErrorProcessingRequest)];

    /// <summary>phrase: Error updating module fields message</summary>
    [ResourceEntry("ErrorUpdatingModuleFields", Description = "phrase: Error updating module fields message", LastModified = "2011/12/15", Value = "There has been an error updating module fields!")]
    public string ErrorUpdatingModuleFields => this[nameof (ErrorUpdatingModuleFields)];

    /// <summary>phrase: Error activating module</summary>
    [ResourceEntry("ErrorActivatingModule", Description = "phrase: Error activating module", LastModified = "2011/12/15", Value = "There has been an error activating module!")]
    public string ErrorActivatingModule => this[nameof (ErrorActivatingModule)];

    /// <summary>phrase: Error deactivating module</summary>
    [ResourceEntry("ErrorDeactivatingModule", Description = "phrase: Error deactivating module", LastModified = "2011/12/15", Value = "There has been an error activating module!")]
    public string ErrorDeactivatingModule => this[nameof (ErrorDeactivatingModule)];

    /// <summary>phrase: Error updating module name and description</summary>
    [ResourceEntry("ErrorUpdatingModuleNameAndDescription", Description = "phrase: Error updating module name and description", LastModified = "2011/12/15", Value = "There has been an error updating module name and description!")]
    public string ErrorUpdatingModuleNameAndDescription => this[nameof (ErrorUpdatingModuleNameAndDescription)];

    /// <summary>phrase: Error deleting content type</summary>
    [ResourceEntry("ErrorDeletingContentType", Description = "phrase: Error deleting content type", LastModified = "2014/01/07", Value = "There has been an error deleting content type!")]
    public string ErrorDeletingContentType => this[nameof (ErrorDeletingContentType)];

    /// <summary>
    /// phrase: Create, modify, delete content items in content type dashboard
    /// </summary>
    [ResourceEntry("GotoManageContentTypeContentDescription", Description = "phrase: Create, modify, delete content items in content type dashboard", LastModified = "2011/12/16", Value = "Create, modify, delete content items")]
    public string GotoManageContentTypeContentDescription => this[nameof (GotoManageContentTypeContentDescription)];

    /// <summary>phrase: Parent content type</summary>
    [ResourceEntry("ParentContentType", Description = "phrase: Parent content type", LastModified = "2012/03/20", Value = "Parent content type")]
    public string ParentContentType => this[nameof (ParentContentType)];

    /// <summary>
    /// phrase: Blogs are parent content type of Blog posts; Forums are parent content type of Threads.
    /// </summary>
    [ResourceEntry("ParentContentTypeExample", Description = "phrase: Blogs are parent content type of Blog posts; Forums are parent content type of Threads. ", LastModified = "2012/03/20", Value = "Blogs are parent content type of Blog posts; Forums are parent content type of Threads.")]
    public string ParentContentTypeExample => this[nameof (ParentContentTypeExample)];

    /// <summary>word: Example</summary>
    [ResourceEntry("Example", Description = "word: Example", LastModified = "2012/03/20", Value = "Example")]
    public string Example => this[nameof (Example)];

    /// <summary>
    /// phrase: Used to format the go to manage content page link text
    /// </summary>
    [ResourceEntry("GotoManageContentTypeContentLinkText", Description = "phrase: Used to format the go to manage content page link text", LastModified = "2011/12/16", Value = "Go to manage <em>{0}</em> content")]
    public string GotoManageContentTypeContentLinkText => this[nameof (GotoManageContentTypeContentLinkText)];

    /// <summary>phrase: This module contains...</summary>
    [ResourceEntry("ThisModuleContains", Description = "phrase: This module contains...", LastModified = "2012/03/10", Value = "This module contains...")]
    public string ThisModuleContains => this[nameof (ThisModuleContains)];

    /// <summary>phrase: fields</summary>
    [ResourceEntry("TypeEditorDefaultTitlePrefix", Description = "phrase: Content type:", LastModified = "2012/05/16", Value = "Content type:")]
    public string TypeEditorDefaultTitlePrefix => this[nameof (TypeEditorDefaultTitlePrefix)];

    /// <summary>
    /// Error Message: An item with the URL '{0}' already exists. Please change the Url or delete the existing URL first.
    /// </summary>
    [ResourceEntry("DuplicateUrlException", Description = "Error message.", LastModified = "2012/12/27", Value = "An item with the URL '{0}' already exists.")]
    public string DuplicateUrlException => this[nameof (DuplicateUrlException)];

    /// <summary>Error Message: Empty Url is not allowed.</summary>
    [ResourceEntry("EmptyUrlException", Description = "Empty Url is not allowed.", LastModified = "2012/12/27", Value = "Empty Url is not allowed.")]
    public string EmptyUrlException => this[nameof (EmptyUrlException)];

    /// <summary>Error Message: The URL: '{0}' is not valid.</summary>
    [ResourceEntry("NotValidUrlException", Description = "The URL: '{0}' is not valid.", LastModified = "2012/12/27", Value = "The URL: '{0}' is not valid.")]
    public string NotValidUrlException => this[nameof (NotValidUrlException)];

    /// <summary>Update module confirmation message.</summary>
    [ResourceEntry("ConfirmModuleUpdateMessage", Description = "Update module confirmation message.", LastModified = "2013/11/15", Value = "<p class='sfMBottom10'>Module with the same name already exists. Its structure and settings will be updated.</p><p class='sfMBottom10'>The initial module fields which do not exist in the new module structure will be deleted and their data will be lost.</p><p class='sfMBottom10'>Before import, we recommend that you back up your initial module and database</p><p>What do you want to do?</p>")]
    public string ConfirmModuleUpdateMessage => this[nameof (ConfirmModuleUpdateMessage)];

    /// <summary>phrase: Update</summary>
    [ResourceEntry("ConfirmModuleUpdateButtonText", Description = "phrase: Update", LastModified = "2013/11/25", Value = "Update")]
    public string ConfirmModuleUpdateButtonText => this[nameof (ConfirmModuleUpdateButtonText)];

    /// <summary>Update module error title.</summary>
    [ResourceEntry("ErrorUpdatingTitle", Description = "Update module error title.", LastModified = "2013/11/15", Value = "You cannot update this module")]
    public string ErrorUpdatingTitle => this[nameof (ErrorUpdatingTitle)];

    /// <summary>
    /// phrase: The name of this module is not the same as the name of the initial module
    /// </summary>
    [ResourceEntry("ErrorUpdatingNamesDontMatch", Description = "phrase: The name of this module is not the same as the name of the initial module", LastModified = "2013/12/09", Value = "The name of this module is not the same as the name of the initial module")]
    public string ErrorUpdatingNamesDontMatch => this[nameof (ErrorUpdatingNamesDontMatch)];

    /// <summary>
    /// phrase: The Id of this module is not the same as the Id of the initial module
    /// </summary>
    [ResourceEntry("ErrorUpdatingIdsDontMatch", Description = "phrase: The Id of this module is not the same as the Id of the initial module", LastModified = "2013/12/09", Value = "The Id of this module is not the same as the Id of the initial module")]
    public string ErrorUpdatingIdsDontMatch => this[nameof (ErrorUpdatingIdsDontMatch)];

    /// <summary>phrase: OK</summary>
    [ResourceEntry("CloseUpdateErrorButton", Description = "phrase: OK", LastModified = "2013/11/15", Value = "OK")]
    public string CloseUpdateErrorButton => this[nameof (CloseUpdateErrorButton)];

    /// <summary>word: Drafts</summary>
    [ResourceEntry("Draft", Description = "The text of draft dynamic module sidebar button.", LastModified = "2014/02/04", Value = "Drafts")]
    public string Draft => this[nameof (Draft)];

    /// <summary>phrase: Published</summary>
    [ResourceEntry("Published", Description = "The text of published dynamic module sidebar button.", LastModified = "2014/02/04", Value = "Published")]
    public string Published => this[nameof (Published)];

    /// <summary>word: Scheduled</summary>
    [ResourceEntry("Scheduled", Description = "The text of scheduled dynamic module sidebar button.", LastModified = "2014/02/04", Value = "Scheduled")]
    public string Scheduled => this[nameof (Scheduled)];

    /// <summary>Word: Duplicate</summary>
    /// <value>Duplicate</value>
    [ResourceEntry("DuplicateDetailsViewTitle", Description = "Word: Duplicate", LastModified = "2014/04/25", Value = "Duplicate")]
    public string DuplicateDetailsViewTitle => this[nameof (DuplicateDetailsViewTitle)];

    /// <summary>abbreviation: FAQ</summary>
    [ResourceEntry("Faq", Description = "abbreviation : FAQ", LastModified = "2017/09/06", Value = "FAQ")]
    public string Faq => this[nameof (Faq)];

    /// <summary>phrase: Recommended characters</summary>
    [ResourceEntry("RecommendedCharacters", Description = "Recommended characters", LastModified = "2018/09/10", Value = "Recommended characters")]
    public string RecommendedCharacters => this[nameof (RecommendedCharacters)];

    /// <summary>phrase: Displayed in a counter below the field.</summary>
    [ResourceEntry("RecommendedCharactersExample", Description = "Displayed in a counter below the field.", LastModified = "2011/12/09", Value = "Displayed in a counter below the field.")]
    public string RecommendedCharactersExample => this[nameof (RecommendedCharactersExample)];

    /// <summary>phrase: Export module</summary>
    [ResourceEntry("ExportModule", Description = "phrase: Export module", LastModified = "2011/12/05", Value = "Export module")]
    public string ExportModule => this[nameof (ExportModule)];

    /// <summary>
    /// Description of the functionality of the export module functionality.
    /// </summary>
    [ResourceEntry("ExportFunctionalityDescription", Description = "Description of the export module functionality.", LastModified = "2012/01/05", Value = "A .zip file containing the whole structure and settings of this module will be downloaded to your computer.")]
    public string ExportFunctionalityDescription => this[nameof (ExportFunctionalityDescription)];

    /// <summary>The note about how export module functionality works.</summary>
    [ResourceEntry("ExportFunctionalityNote", Description = "The note about how export module functionality works", LastModified = "2011/12/05", Value = "<strong>Note</strong>: No data will be exported (e.g. news items, articles, blog posts), only structure and settings.")]
    public string ExportFunctionalityNote => this[nameof (ExportFunctionalityNote)];

    /// <summary>
    /// The example of what could be the name of the exported module.
    /// </summary>
    [ResourceEntry("ExportModuleNameExample", Description = "The example of what could be the name of the exported module.", LastModified = "2011/12/05", Value = "<strong>Example</strong>: <em>News</em>")]
    public string ExportModuleNameExample => this[nameof (ExportModuleNameExample)];

    /// <summary>Label for the description of the exported module.</summary>
    [ResourceEntry("ExportModuleDescription", Description = "Label for the description of the exported module.", LastModified = "2011/12/05", Value = "Description <span class=\"sfNote\">(optional)</span>")]
    public string ExportModuleDescription => this[nameof (ExportModuleDescription)];

    /// <summary>Label for the version of the exported module.</summary>
    [ResourceEntry("Version", Description = "Label for the version of the exported module.", LastModified = "2011/12/05", Value = "Version <span class=\"sfNote\">(optional)</span>")]
    public string Version => this[nameof (Version)];

    /// <summary>Label for the author of the exported module.</summary>
    [ResourceEntry("Author", Description = "Label for the author of the exported module.", LastModified = "2011/12/05", Value = "Author <span class=\"sfNote\">(optional)</span>")]
    public string Author => this[nameof (Author)];

    /// <summary>Text of the export button</summary>
    [ResourceEntry("Export", Description = "Text of the export button.", LastModified = "2011/12/05", Value = "Export")]
    public string Export => this[nameof (Export)];

    /// <summary>Text of the import module button.</summary>
    [ResourceEntry("ImportAModule", Description = "Text of the import module button.", LastModified = "2011/12/15", Value = "Import a module")]
    public string ImportAModule => this[nameof (ImportAModule)];

    /// <summary>Text of the upload button for importing modules.</summary>
    [ResourceEntry("Upload", Description = "Text of the upload button for importing modules.", LastModified = "2011/12/05", Value = "Upload")]
    public string Upload => this[nameof (Upload)];

    /// <summary>
    /// Title of the field for selecting the file of dynamic module
    /// </summary>
    [ResourceEntry("ImportModuleFileTitle", Description = "Title of the field for selecting the file of dynamic module", LastModified = "2011/12/05", Value = "Upload a .zip file containing an exported module")]
    public string ImportModuleFileTitle => this[nameof (ImportModuleFileTitle)];

    /// <summary>Title of the export module content dialog.</summary>
    [ResourceEntry("ExportModuleContent", Description = "Title of the export module content dialog.", LastModified = "2012/10/23", Value = "Export module content")]
    public string ExportModuleContent => this[nameof (ExportModuleContent)];

    /// <summary>
    /// Phrase: To export module content you should select a source
    /// </summary>
    [ResourceEntry("ExportModuleSelectSource", Description = "Phrase: To export module content you should select a source", LastModified = "2012/10/23", Value = "To export module content you should select a source")]
    public string ExportModuleSelectSource => this[nameof (ExportModuleSelectSource)];

    /// <summary>Title for the update module dialog</summary>
    [ResourceEntry("UpdateAModule", Description = "Title for the update module dialog", LastModified = "2013/11/15", Value = "Import new structure and settings to ")]
    public string UpdateAModule => this[nameof (UpdateAModule)];

    /// <summary>Title for the information section in update module</summary>
    [ResourceEntry("UpdateAModuleInformationTitle", Description = "Title for the information section in update module", LastModified = "2013/11/15", Value = "Import new structure and settings")]
    public string UpdateAModuleInformationTitle => this[nameof (UpdateAModuleInformationTitle)];

    /// <summary>Information section text in update module</summary>
    [ResourceEntry("UpdateAModuleInformationText", Description = "Information section text in update module", LastModified = "2013/11/15", Value = "The initial module fields which do not exist in the new module structure will be deleted and their data will be lost.")]
    public string UpdateAModuleInformationText => this[nameof (UpdateAModuleInformationText)];

    /// <summary>
    /// Information section recommendation text in update module
    /// </summary>
    [ResourceEntry("UpdateAModuleInformationTextRecommendation", Description = "Information section recommendation text in update module", LastModified = "2013/11/15", Value = "Before import, we recommend that you back up your initial module and database.")]
    public string UpdateAModuleInformationTextRecommendation => this[nameof (UpdateAModuleInformationTextRecommendation)];

    /// <summary>phrase: Which {0} to display?</summary>
    [ResourceEntry("WhichItemsToDisplay", Description = "phrase: Which {0} to display?", LastModified = "2011/11/06", Value = "Which {0} to display?")]
    public string WhichItemsToDisplay => this[nameof (WhichItemsToDisplay)];

    /// <summary>phrase: All published {0}</summary>
    [ResourceEntry("AllPublishedItems", Description = "phrase: All published {0}", LastModified = "2011/11/06", Value = "All published {0}")]
    public string AllPublishedItems => this[nameof (AllPublishedItems)];

    /// <summary>phrase: One particular {0} item only...</summary>
    [ResourceEntry("OneParticularItemOnly", Description = "phrase: One particular {0} item only...", LastModified = "2011/11/06", Value = "One particular {0} only...")]
    public string OneParticularItemOnly => this[nameof (OneParticularItemOnly)];

    /// <summary>phrase: Selection of {0}:</summary>
    [ResourceEntry("SelectionOfItems", Description = "phrase: Selection of {0}:", LastModified = "2011/11/06", Value = "Selection of {0}:")]
    public string SelectionOfItems => this[nameof (SelectionOfItems)];

    /// <summary>phrase: No {0} have been created yet</summary>
    [ResourceEntry("NoItemsHaveBeenCreatedYet", Description = "phrase: No {0} have been created yet", LastModified = "2011/11/06", Value = "No {0} have been created yet")]
    public string NoItemsHaveBeenCreatedYet => this[nameof (NoItemsHaveBeenCreatedYet)];

    /// <summary>phrase: No {0} have been selected yet</summary>
    [ResourceEntry("NoItemsHaveBeenSelectedYet", Description = "phrase: No {0} have been selected yet", LastModified = "2012/09/13", Value = "No {0} have been selected yet")]
    public string NoItemsHaveBeenSelectedYet => this[nameof (NoItemsHaveBeenSelectedYet)];

    /// <summary>phrase: Select {0}</summary>
    [ResourceEntry("SelectItems", Description = "phrase: Select {0}", LastModified = "2011/11/06", Value = "Select {0}")]
    public string SelectItems => this[nameof (SelectItems)];

    /// <summary>phrase: Sort {0}</summary>
    [ResourceEntry("SortItems", Description = "Label text.", LastModified = "2011/11/06", Value = "Sort {0}")]
    public string SortItems => this[nameof (SortItems)];

    /// <summary>phrase: Choose {0}</summary>
    [ResourceEntry("ChooseItems", Description = "phrase: Choose {0}", LastModified = "2011/11/06", Value = "Choose {0}")]
    public string ChooseItems => this[nameof (ChooseItems)];

    /// <summary>phrase: By {0} (A-Z)</summary>
    [ResourceEntry("ByMainShortTextFieldAsc", Description = "phrase: By {0} (A-Z)", LastModified = "2012/12/28", Value = "By {0} (A-Z)")]
    public string ByMainShortTextFieldAsc => this[nameof (ByMainShortTextFieldAsc)];

    /// <summary>phrase: By {0} (Z-A)</summary>
    [ResourceEntry("ByMainShortTextFieldDesc", Description = "phrase: By {0} (Z-A)", LastModified = "2012/12/28", Value = "By {0} (Z-A)")]
    public string ByMainShortTextFieldDesc => this[nameof (ByMainShortTextFieldDesc)];

    /// <summary>Type Permissions</summary>
    [ResourceEntry("TypePermissions", Description = "Permission title.", LastModified = "2012/05/11", Value = "Dynamic field permissions")]
    public string TypePermissions => this[nameof (TypePermissions)];

    /// <summary>
    /// Represents the most common application security permissions.
    /// </summary>
    [ResourceEntry("TypePermissionsDescription", Description = "Permission description.", LastModified = "2012/05/11", Value = "Represents the permissions for dynamic fields.")]
    public string TypePermissionsDescription => this[nameof (TypePermissionsDescription)];

    /// <summary>The title of ViewItem security action.</summary>
    [ResourceEntry("ViewItemAction", Description = "The title of ViewItem security action.", LastModified = "2012/05/11", Value = "View {0}")]
    public string ViewItemAction => this[nameof (ViewItemAction)];

    /// <summary>The title of CreateItem security action.</summary>
    [ResourceEntry("CreateItemAction", Description = "The title of CreateItem security action.", LastModified = "2012/05/11", Value = "Create {0}")]
    public string CreateItemAction => this[nameof (CreateItemAction)];

    /// <summary>The title of ModifyItem security action.</summary>
    [ResourceEntry("ModifyItemAction", Description = "The title of ModifyItem security action.", LastModified = "2012/05/11", Value = "Modify {0}")]
    public string ModifyItemAction => this[nameof (ModifyItemAction)];

    /// <summary>The title of DeleteItem security action.</summary>
    [ResourceEntry("DeleteItemAction", Description = "The title of DeleteItem security action.", LastModified = "2012/05/11", Value = "Delete {0}")]
    public string DeleteItemAction => this[nameof (DeleteItemAction)];

    /// <summary>Allows or denies the creation of new data items.</summary>
    [ResourceEntry("CreateItemDescription", Description = "Security action description.", LastModified = "2012/05/11", Value = "Allows or denies the creation of new {0}.")]
    public string CreateItemDescription => this[nameof (CreateItemDescription)];

    /// <summary>Allows or denies the modification of new data items.</summary>
    [ResourceEntry("ModifyItemDescription", Description = "Security action description.", LastModified = "2012/05/11", Value = "Allows or denies changes to existing dynamic field.")]
    public string ModifyItemDescription => this[nameof (ModifyItemDescription)];

    /// <summary>Allows or denies viewing a particular data item.</summary>
    [ResourceEntry("ViewItemDescription", Description = "Security action description.", LastModified = "2012/05/11", Value = "Allows or denies viewing a dynamic field.")]
    public string ViewItemDescription => this[nameof (ViewItemDescription)];

    /// <summary>Allows or denies deleting a particular data item.</summary>
    [ResourceEntry("DeleteItemDescription", Description = "Security action description.", LastModified = "2012/05/11", Value = "Allows or denies deleting a dynamic field.")]
    public string DeleteItemDescription => this[nameof (DeleteItemDescription)];

    /// <summary>Change Permissions</summary>
    [ResourceEntry("ChangeItemPermissions", Description = "The title of Change Permissions security action.", LastModified = "2010/07/20", Value = "Change a {0} permissions")]
    public string ChangeItemPermissions => this[nameof (ChangeItemPermissions)];

    /// <summary>
    /// Allows or denies changing the permissions of a data item.
    /// </summary>
    [ResourceEntry("ChangeItemPermissionsDescription", Description = "Security action description.", LastModified = "2010/07/20", Value = "Allows or denies changing the permissions of a dynamic field.")]
    public string ChangeItemPermissionsDescription => this[nameof (ChangeItemPermissionsDescription)];

    /// <summary>label: Allow permissions per field</summary>
    [ResourceEntry("AllowPermissionsPerField", Description = "label: Allow permissions per field", LastModified = "2012/06/20", Value = "Allow permissions per field")]
    public string AllowPermissionsPerField => this[nameof (AllowPermissionsPerField)];

    /// <summary>
    /// label: Apart from permissions like "Who can create/delete/modify items" you will have permissions like "Who can change item title" or "Who can change item tags".
    /// </summary>
    [ResourceEntry("AllowPermissionsPerFieldAdditionalNote", Description = "label: Allow permissions per field note", LastModified = "2012/06/20", Value = "Apart from permissions like \"Who can create/delete/modify items\" you will have permissions like \"Who can change item title\" or \"Who can change item tags\".")]
    public string AllowPermissionsPerFieldAdditionalNote => this[nameof (AllowPermissionsPerFieldAdditionalNote)];

    /// <summary>
    /// The validation error when the module/meta type structure is different from the one on the target site
    /// </summary>
    [ResourceEntry("SiteSyncValidation_IncompatibleTypeStructure", Description = "The validation error when the module/meta type structure is different from the one on the target site", LastModified = "2013/11/20", Value = "Incompatible module/meta type structure for type {0} ({1}). Data-only sync will not be performed")]
    public string SiteSyncValidation_IncompatibleTypeStructure => this[nameof (SiteSyncValidation_IncompatibleTypeStructure)];

    [ResourceEntry("NewModifiedFirst", Description = "", LastModified = "2014/01/31", Value = "Last modified on top")]
    public string NewModifiedFirst => this[nameof (NewModifiedFirst)];

    [ResourceEntry("NewCreatedFirst", Description = "", LastModified = "2014/01/31", Value = "Last created on top")]
    public string NewCreatedFirst => this[nameof (NewCreatedFirst)];

    [ResourceEntry("ByTitleAsc", Description = "", LastModified = "2014/01/31", Value = "By Title (A-Z)")]
    public string ByTitleAsc => this[nameof (ByTitleAsc)];

    [ResourceEntry("ByTitleDesc", Description = "", LastModified = "2014/01/31", Value = "By Title (Z-A)")]
    public string ByTitleDesc => this[nameof (ByTitleDesc)];

    [ResourceEntry("CustomSorting", Description = "", LastModified = "2014/01/31", Value = "Custom sorting...")]
    public string CustomSorting => this[nameof (CustomSorting)];

    /// <summary>phrase: Sort</summary>
    [ResourceEntry("Sort", Description = "Label text.", LastModified = "2014/01/31", Value = "Sort")]
    public string Sort => this[nameof (Sort)];

    /// <summary>
    /// phrase: Module with name \"{0}\" and different Id already exists!
    /// </summary>
    [ResourceEntry("ModuleWithNameAlreadyExists", Description = "phrase: Module with name \"{0}\" and different Id already exists!", LastModified = "2013/12/03", Value = "Module with name \"{0}\" and different Id already exists!")]
    public string ModuleWithNameAlreadyExists => this[nameof (ModuleWithNameAlreadyExists)];

    /// <summary>
    /// phrase: Module with Id \"{0}\" and different name already exists!
    /// </summary>
    [ResourceEntry("ModuleWithIdAlreadyExists", Description = "phrase: Module with Id \"{0}\" and different name already exists!", LastModified = "2013/12/03", Value = "Module with Id \"{0}\" and different name already exists!")]
    public string ModuleWithIdAlreadyExists => this[nameof (ModuleWithIdAlreadyExists)];

    /// <summary>phrase: Taxonomy with Id \"{0}\" doesn't exists!</summary>
    [ResourceEntry("TaxonomyWithIdDoesntExists", Description = "phrase: Taxonomy with Id \"{0}\" doesn't exists!", LastModified = "2013/12/03", Value = "Taxonomy with Id \"{0}\" doesn't exists!")]
    public string TaxonomyWithIdDoesntExists => this[nameof (TaxonomyWithIdDoesntExists)];

    /// <summary>phrase: Module already exists!</summary>
    /// <value>Module already exists!</value>
    [ResourceEntry("ModuleAlreadyExists", Description = "phrase: Module already exists!", LastModified = "2013/12/04", Value = "Module already exists!")]
    public string ModuleAlreadyExists => this[nameof (ModuleAlreadyExists)];

    /// <summary>
    /// phrase: You do not have permissions to change the database
    /// </summary>
    /// <value>You do not have permissions to change the database</value>
    [ResourceEntry("NoDatabasePermissions", Description = "phrase: You do not have permissions to change the database", LastModified = "2014/01/10", Value = "You do not have permissions to change the database")]
    public string NoDatabasePermissions => this[nameof (NoDatabasePermissions)];

    [ResourceEntry("MoreOptions", Description = "", LastModified = "2013/01/22", Value = "More Options")]
    public string MoreOptions => this[nameof (MoreOptions)];

    [ResourceEntry("MoreOptionsDescription", Description = "", LastModified = "2013/01/22", Value = "(Localization settings)")]
    public string MoreOptionsDescription => this[nameof (MoreOptionsDescription)];

    [ResourceEntry("DisableLinkParser", Description = "", LastModified = "2013/01/22", Value = "Disable Link Parser")]
    public string DisableLinkParser => this[nameof (DisableLinkParser)];

    [ResourceEntry("Address", Description = "", LastModified = "2013/02/26", Value = "Address")]
    public string Address => this[nameof (Address)];

    [ResourceEntry("AddressField", Description = "", LastModified = "2013/02/26", Value = "Address and map")]
    public string AddressField => this[nameof (AddressField)];

    /// <summary>label: Address form only</summary>
    /// <value>Address form only</value>
    [ResourceEntry("AddressFormOnly", Description = "label: Address form only", LastModified = "2013/03/05", Value = "Address form only")]
    public string AddressFormOnly => this[nameof (AddressFormOnly)];

    /// <summary>
    /// label: Map only (point manually or auto-locate on mobile apps)
    /// </summary>
    /// <value>Map only (point manually or auto-locate on mobile apps)</value>
    [ResourceEntry("MapOnly", Description = "label: Map only (point manually or auto-locate on mobile apps)", LastModified = "2013/03/05", Value = "Map only (point manually or auto-locate on mobile apps)")]
    public string MapOnly => this[nameof (MapOnly)];

    /// <summary>label: Address form and map</summary>
    /// <value>Address form and map</value>
    [ResourceEntry("FormMap", Description = "label: Address form and map", LastModified = "2013/03/05", Value = "Address form and map")]
    public string FormMap => this[nameof (FormMap)];

    /// <summary>For entering address, use...</summary>
    /// <value>For entering address, use...</value>
    [ResourceEntry("AddressModeTitle", Description = "For entering address, use...", LastModified = "2013/03/05", Value = "For entering address, use...")]
    public string AddressModeTitle => this[nameof (AddressModeTitle)];

    /// <summary>label: Social media (OpenGraph)</summary>
    [ResourceEntry("OpenGraph", Description = "label: Social media (OpenGraph)", LastModified = "2018/08/22", Value = "Social media (OpenGraph)")]
    public string OpenGraph => this[nameof (OpenGraph)];

    /// <summary>label: Search engine optimization</summary>
    [ResourceEntry("Seo", Description = "label: Search engine optimization", LastModified = "2018/08/22", Value = "Search engine optimization")]
    public string Seo => this[nameof (Seo)];

    /// <summary>label: Meta field</summary>
    [ResourceEntry("MetaField", Description = "label: Meta field", LastModified = "2018/08/22", Value = "Meta field")]
    public string MetaField => this[nameof (MetaField)];

    /// <summary>Meta title field type</summary>
    [ResourceEntry("MetaTitle", Description = "Meta title field type", LastModified = "2018/08/22", Value = "Meta title")]
    public string MetaTitle => this[nameof (MetaTitle)];

    /// <summary>Meta description field type</summary>
    [ResourceEntry("MetaDescription", Description = "Meta description field type", LastModified = "2018/08/22", Value = "Meta description")]
    public string MetaDescription => this[nameof (MetaDescription)];

    /// <summary>label: OpenGraph field</summary>
    [ResourceEntry("OpenGraphField", Description = "label: OpenGraph field", LastModified = "2018/08/22", Value = "OpenGraph field")]
    public string OpenGraphField => this[nameof (OpenGraphField)];

    /// <summary>OpenGraph title field type</summary>
    [ResourceEntry("OpenGraphTitle", Description = "OpenGraph title field type", LastModified = "2018/08/22", Value = "OpenGraph title")]
    public string OpenGraphTitle => this[nameof (OpenGraphTitle)];

    /// <summary>OpenGraph description field type</summary>
    [ResourceEntry("OpenGraphDescription", Description = "OpenGraph description field type", LastModified = "2018/08/22", Value = "OpenGraph description")]
    public string OpenGraphDescription => this[nameof (OpenGraphDescription)];

    /// <summary>OpenGraph image field type</summary>
    [ResourceEntry("OpenGraphImage", Description = "OpenGraph image field type", LastModified = "2018/08/22", Value = "OpenGraph image")]
    public string OpenGraphImage => this[nameof (OpenGraphImage)];

    /// <summary>OpenGraph video field type</summary>
    [ResourceEntry("OpenGraphVideo", Description = "OpenGraph video field type", LastModified = "2018/08/22", Value = "OpenGraph video")]
    public string OpenGraphVideo => this[nameof (OpenGraphVideo)];

    [ResourceEntry("HowToSyncContentWithCloudServices", Description = "", LastModified = "2014/03/21", Value = "How to sync content with Backend Services")]
    public string HowToSyncContentWithCloudServices => this[nameof (HowToSyncContentWithCloudServices)];

    [ResourceEntry("GoToTheCloud", Description = "", LastModified = "2013/04/18", Value = "Go to the cloud")]
    public string GoToTheCloud => this[nameof (GoToTheCloud)];

    [ResourceEntry("CloudServicesDescription", Description = "", LastModified = "2013/04/15", Value = "This module syncs its content with")]
    public string CloudServicesDescription => this[nameof (CloudServicesDescription)];

    [ResourceEntry("CloudServicesTitle", Description = "", LastModified = "2014/03/21", Value = "Connector for Backend Services")]
    public string CloudServicesTitle => this[nameof (CloudServicesTitle)];

    /// <summary>Gets External Link: Create a new module</summary>
    [ResourceEntry("ExternalLinkCreateNewModule", Description = "External Link: Create a new module", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/create-a-dynamic-module")]
    public string ExternalLinkCreateNewModule => this[nameof (ExternalLinkCreateNewModule)];

    /// <summary>
    /// Gets External Link: Edit the module and the module screens
    /// </summary>
    [ResourceEntry("ExternalLinkEditModule", Description = "External Link: Edit the module and the module screens", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/modify-the-backend-screens-of-a-dynamic-module")]
    public string ExternalLinkEditModule => this[nameof (ExternalLinkEditModule)];

    /// <summary>
    /// Gets External Link: Edit content types and their hierarchy
    /// </summary>
    [ResourceEntry("ExternalLinkEditContentType", Description = "External Link: Edit content types and their hierarchy", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/edit-dynamic-content-types-and-their-hierarchy")]
    public string ExternalLinkEditContentType => this[nameof (ExternalLinkEditContentType)];

    /// <summary>Gets External Link: Learn more about Modules</summary>
    [ResourceEntry("ExternalLinkLearnAboutModules", Description = "External Link: Learn more about Modules", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-dynamic-modules-and-the-module-builder")]
    public string ExternalLinkLearnAboutModules => this[nameof (ExternalLinkLearnAboutModules)];

    /// <summary>SEO section name</summary>
    [ResourceEntry("SEOSectionName", Description = "SEO section name.", LastModified = "2018/09/11", Value = "SeoSection")]
    public string SEOSectionName => this[nameof (SEOSectionName)];

    /// <summary>Seo section title</summary>
    [ResourceEntry("SEOSectionTitle", Description = "Title for SEO section.", LastModified = "2018/09/11", Value = "Search engine optimization")]
    public string SEOSectionTitle => this[nameof (SEOSectionTitle)];

    /// <summary>Social media section title</summary>
    [ResourceEntry("SocialMediaSectionTitle", Description = "Title for Social media section.", LastModified = "2018/09/11", Value = "Social media")]
    public string SocialMediaSectionTitle => this[nameof (SocialMediaSectionTitle)];

    /// <summary>Social media section name</summary>
    [ResourceEntry("SocialMediaSectionName", Description = "Socia medial section name.", LastModified = "2018/09/11", Value = "SocialMedia")]
    public string SocialMediaSectionName => this[nameof (SocialMediaSectionName)];
  }
}
