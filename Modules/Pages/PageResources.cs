// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Represents the string resources for the backend navigation
  /// </summary>
  [ObjectInfo("PageResources", ResourceClassId = "PageResources")]
  public class PageResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.PageResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public PageResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.PageResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public PageResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Page Resources</summary>
    [ResourceEntry("PageResourcesTitle", Description = "The title of this class.", LastModified = "2009/04/30", Value = "Page Resources")]
    public string PageResourcesTitle => this[nameof (PageResourcesTitle)];

    /// <summary>Page Resources</summary>
    [ResourceEntry("PageResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/04/30", Value = "Page Resources")]
    public string PageResourcesTitlePlural => this[nameof (PageResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Sitefinity backend pages.
    /// </summary>
    [ResourceEntry("PageResourcesDescription", Description = "The description of this class.", LastModified = "2009/04/30", Value = "Contains localizable resources for Sitefinity backend pages.")]
    public string PageResourcesDescription => this[nameof (PageResourcesDescription)];

    /// <summary>Generic Content</summary>
    [ResourceEntry("GenericContentPermissionsTitle", Description = "The title of the Generic Content permissions page.", LastModified = "2009/12/18", Value = "Generic Content Permissions")]
    public string GenericContentPermissionsTitle => this[nameof (GenericContentPermissionsTitle)];

    /// <summary>Generic Content</summary>
    [ResourceEntry("GenericContentPermissionsHtmlTitle", Description = "The html title of the Generic Content permissions page.", LastModified = "2010/02/15", Value = "Generic Content Permissions")]
    public string GenericContentPermissionsHtmlTitle => this[nameof (GenericContentPermissionsHtmlTitle)];

    /// <summary>Generic</summary>
    [ResourceEntry("GenericContentPermissionsUrlName", Description = "The name that appears in the URL when navigating to GenericContent permissions panel.", LastModified = "2009/12/18", Value = "Permissions")]
    public string GenericContentPermissionsUrlName => this[nameof (GenericContentPermissionsUrlName)];

    /// <summary>
    /// phrase: Represents a permissions page for Sitefinity Generic Content module.
    /// </summary>
    [ResourceEntry("GenericContentPermissionsDescription", Description = "The description of the Generic Content module permissions page.", LastModified = "2012/01/05", Value = "Represents a permissions page for Sitefinity Generic Content module.")]
    public string GenericContentPermissionsDescription => this[nameof (GenericContentPermissionsDescription)];

    /// <summary>Messsage: Back to Generic Content</summary>
    /// <value>Back to Generic Content, from the permissions settings screen.</value>
    [ResourceEntry("BackToContent", Description = "Back to Generic content, from the permissions settings screen.", LastModified = "2009/12/19", Value = "Back to Generic content")]
    public string BackToContent => this[nameof (BackToContent)];

    /// <summary>phrase: You are not allowed to modify this page: {0}</summary>
    /// <value>You are not allowed to modify this page: {0}</value>
    [ResourceEntry("PageNoModifyPermission", Description = "phrase: You are not allowed to modify this page: {0}", LastModified = "2017/10/17", Value = "You are not allowed to modify this page: {0}")]
    public string PageNoModifyPermission => this[nameof (PageNoModifyPermission)];

    /// <summary>
    /// Translated message, similar to "Are you sure you want to delete the control?"
    /// </summary>
    /// <value>Confirmation message used by the Page Editor before deleting a widget.</value>
    [ResourceEntry("ZoneEditorEditableInPagesLabel", Description = "Confirmation message used by the Page Editor before deleting a widget.", LastModified = "2016/08/22", Value = "<span class='sfShared'>Editable on pages</span><span> <a href='#' dockId='{0}' overriddenControlsCount='{1}' class='displayOverridenControls sfMoreDetails'>View where widget was edited</a></span>")]
    public string ZoneEditorEditableInPagesLabel => this[nameof (ZoneEditorEditableInPagesLabel)];

    /// <summary>
    /// Translated message, similar to "Are you sure you want to delete the control?"
    /// </summary>
    /// <value>Confirmation message used by the Page Editor before deleting a widget.</value>
    [ResourceEntry("ZoneEditorEditableInPagesWithoutALinkLabel", Description = "Confirmation message used by the Page Editor before deleting a widget.", LastModified = "2010/05/11", Value = "<span class='sfShared'>Editable on pages</span>")]
    public string ZoneEditorEditableInPagesWithoutALinkLabel => this[nameof (ZoneEditorEditableInPagesWithoutALinkLabel)];

    /// <summary>
    /// Translated message, similar to "Are you sure you want to delete the control?"
    /// </summary>
    /// <value>Confirmation message used by the Page Editor before deleting a widget.</value>
    [ResourceEntry("ZoneEditorConfirmDeleteControl", Description = "Confirmation message used by the Page Editor before deleting a widget.", LastModified = "2010/05/11", Value = "Are you sure you want to delete the control?")]
    public string ZoneEditorConfirmDeleteControl => this[nameof (ZoneEditorConfirmDeleteControl)];

    /// <summary>Translated message, similar to "Delete widget"</summary>
    /// <value>Delete confirmation dialog title.</value>
    [ResourceEntry("ZoneEditorDeleteConfirmationDialogTitle", Description = "Delete confirmation dialog title.", LastModified = "2019/01/28", Value = "Delete widget")]
    public string ZoneEditorDeleteConfirmationDialogTitle => this[nameof (ZoneEditorDeleteConfirmationDialogTitle)];

    /// <summary>
    /// Translated message, similar to "Are you sure you want to delete the layout element?"
    /// </summary>
    /// <value>Confirmation message used by the Page Editor before deleting a layout element.</value>
    [ResourceEntry("ZoneEditorConfirmDeleteLayout", Description = "Confirmation message used by the Page Editor before deleting a layout element.", LastModified = "2010/05/11", Value = "Are you sure you want to delete the layout element?")]
    public string ZoneEditorConfirmDeleteLayout => this[nameof (ZoneEditorConfirmDeleteLayout)];

    /// <summary>Translated message, similar to "Edit"</summary>
    /// <value>Page editor 'edit' command title.</value>
    [ResourceEntry("ZoneEditorEditCommand", Description = "Page editor 'edit' command title.", LastModified = "2010/05/11", Value = "Edit")]
    public string ZoneEditorEditCommand => this[nameof (ZoneEditorEditCommand)];

    /// <summary>Translated message, similar to "Edit"</summary>
    /// <value>Page editor 'edit' command title.</value>
    [ResourceEntry("ZoneEditorControlOverridenInPages", Description = "Page editor 'edit' command title.", LastModified = "2010/05/11", Value = "{0} is edited in {1} pages")]
    public string ZoneEditorControlOverridenInPages => this[nameof (ZoneEditorControlOverridenInPages)];

    /// <summary>Translated message, similar to "Edit"</summary>
    /// <value>Page editor 'edit' command title.</value>
    [ResourceEntry("ZoneEditorControlOverridenInPage", Description = "Page editor 'edit' command title.", LastModified = "2010/05/11", Value = "{0} is edited in 1 page ")]
    public string ZoneEditorControlOverridenInPage => this[nameof (ZoneEditorControlOverridenInPage)];

    /// <summary>
    /// Translated message, similar to "Enable page overrides"
    /// </summary>
    /// <value>Page editor 'Enable page overrides' command title.</value>
    [ResourceEntry("ZoneEditorEnablePageOverride", Description = "Page editor 'edit' command title.", LastModified = "2010/05/11", Value = "Make editable on pages")]
    public string ZoneEditorEnablePageOverride => this[nameof (ZoneEditorEnablePageOverride)];

    /// <summary>
    /// Translated message, similar to "Enable page overrides"
    /// </summary>
    /// <value>Page editor 'Enable page overrides' command title.</value>
    [ResourceEntry("ZoneEditorEnablePageOverrideDisplayContenxtMenuInfo", Description = "Page editor 'edit' command title.", LastModified = "2016/08/29", Value = "You cannot delete or move this widget because it is inherited from page template")]
    public string ZoneEditorEnablePageOverrideDisplayContenxtMenuInfo => this[nameof (ZoneEditorEnablePageOverrideDisplayContenxtMenuInfo)];

    /// <summary>
    /// Title for the prompt dialog when a page cannot be edited.
    /// </summary>
    /// <value>Page cannot be edited</value>
    [ResourceEntry("PageCannotBeEdited", Description = "Title for the prompt dialog when a page cannot be edited.", LastModified = "2020/03/24", Value = "Page cannot be edited")]
    public string PageCannotBeEdited => this[nameof (PageCannotBeEdited)];

    /// <summary>
    /// Explaination of prompt message when opening a page that can't be edited from the old UI.
    /// </summary>
    /// <value>To be able to edit this page enable the new user interface and .NET Core renderer, or contact your administrator.</value>
    [ResourceEntry("PromptMessageSwitchToNewUI", Description = "Explaination of prompt message when opening a page that can't be edited from the old UI.", LastModified = "2020/03/24", Value = "To be able to edit this page enable the new user interface and .NET Core renderer, or contact your administrator.")]
    public string PromptMessageSwitchToNewUI => this[nameof (PromptMessageSwitchToNewUI)];

    /// <summary>
    /// Translated message, similar to "Enable page overrides"
    /// </summary>
    /// <value>Page editor 'Enable page overrides' command title.</value>
    [ResourceEntry("ZoneEditorDisablePageOverride", Description = "Page editor 'edit' command title.", LastModified = "2010/05/11", Value = "Make non-editable on pages")]
    public string ZoneEditorDisablePageOverride => this[nameof (ZoneEditorDisablePageOverride)];

    /// <summary>
    /// Translated message, similar to "Disable page overrides"
    /// </summary>
    /// <value>Page editor 'Disable page overrides' command title.</value>
    [ResourceEntry("ZoneEditorRollback", Description = "Page editor 'edit' command title.", LastModified = "2010/05/11", Value = "Restore to widget from base template")]
    public string ZoneEditorRollback => this[nameof (ZoneEditorRollback)];

    /// <summary>Translated message, similar to "More"</summary>
    /// <value>Page editor 'more' command title.</value>
    [ResourceEntry("ConfirumRollbackButtonLabel", Description = "Confirm dialog buttin label", LastModified = "2014/03/24", Value = "<b>Yes, Restore</b>")]
    public string ConfirumRollbackButtonLabel => this[nameof (ConfirumRollbackButtonLabel)];

    /// <summary>Translated message, similar to "More"</summary>
    /// <value>Page editor 'more' command title.</value>
    [ResourceEntry("ConfirmRollbackToVersionTitle", Description = " ", LastModified = "2016/08/25", Value = "Restore to widget settings from page template")]
    public string ConfirmRollbackToVersionTitle => this[nameof (ConfirmRollbackToVersionTitle)];

    /// <summary>Translated message, similar to "More"</summary>
    /// <value>Page editor 'more' command title.</value>
    [ResourceEntry("ConfirmRollbackToVersion", Description = " ", LastModified = "2016/08/25", Value = "<p>Currently this widget’s content and settings are not the same in the original widget on the page template.</p><p>If you restore to the widget from page template, all changes you have made will be lost.</p><p>Are you sure you want to continue?</p>")]
    public string ConfirmRollbackToVersion => this[nameof (ConfirmRollbackToVersion)];

    /// <summary>
    /// Translated message, similar to "This widget's content and settings have been edited on 3 page(s). Once deleted, all changes will be lost and cannot be restored. Are you sure you want to continue?"
    /// </summary>
    /// <value>Page editor warning text for deleteEditedWidgetConfirmationDialog shown for delete operation on editable widgets.</value>
    [ResourceEntry("DeleteEditedWidgetConfirmation", Description = "Holds the warning text for deleteEditedWidgetConfirmationDialog shown for delete operation on editable widgets", LastModified = "2016/08/26", Value = "<p>This widget's content and settings have been edited on {0} page(s).<br/>Once deleted, all changes will be lost and cannot be restored.</p><p>Are you sure you want to continue?</p>")]
    public string DeleteEditedWidgetConfirmation => this[nameof (DeleteEditedWidgetConfirmation)];

    /// <summary>
    /// Translated message, similar to "Yes, delete this widget"
    /// </summary>
    /// <value>Page editor 'more' command title.</value>
    [ResourceEntry("DeleteEditedWidgetButtonLabel", Description = "Confirm deleteEditedWidgetConfirmationDialog button label", LastModified = "2016/08/19", Value = "<b>Yes, delete this widget</b>")]
    public string DeleteEditedWidgetButtonLabel => this[nameof (DeleteEditedWidgetButtonLabel)];

    /// <summary>Translated message, similar to "More"</summary>
    /// <value>Page editor 'more' command title.</value>
    [ResourceEntry("ZoneEditorMoreCommand", Description = "Page editor 'more' command title.", LastModified = "2010/05/11", Value = "More")]
    public string ZoneEditorMoreCommand => this[nameof (ZoneEditorMoreCommand)];

    /// <summary>Translated message, similar to "Permissions"</summary>
    /// <value>Page editor 'permissions' command title.</value>
    [ResourceEntry("ZoneEditorPermissionsCommand", Description = "Page editor 'permissions' command title.", LastModified = "2010/05/11", Value = "Permissions")]
    public string ZoneEditorPermissionsCommand => this[nameof (ZoneEditorPermissionsCommand)];

    /// <summary>Translated message, similar to "Delete"</summary>
    /// <value>Page editor 'delete' command title.</value>
    [ResourceEntry("ZoneEditorDeleteCommand", Description = "Page editor 'delete' command title.", LastModified = "2010/05/11", Value = "Delete")]
    public string ZoneEditorDeleteCommand => this[nameof (ZoneEditorDeleteCommand)];

    /// <summary>Translated message, similar to "Duplicate"</summary>
    /// <value>Page editor 'duplicate' command title.</value>
    [ResourceEntry("ZoneEditorDuplicationCommand", Description = "Page editor 'duplicate' command title.", LastModified = "2010/05/11", Value = "Duplicate")]
    public string ZoneEditorDuplicationCommand => this[nameof (ZoneEditorDuplicationCommand)];

    /// <summary>Translated message, similar to "Layouts"</summary>
    /// <value>Page editor 'layouts' tab title.</value>
    [ResourceEntry("ZoneEditorLayouts", Description = "Page editor 'layouts' tab title.", LastModified = "2010/05/11", Value = "Layouts")]
    public string ZoneEditorLayouts => this[nameof (ZoneEditorLayouts)];

    /// <summary>Page editor 'themes' tab title.</summary>
    [ResourceEntry("ZoneEditorThemes", Description = "Page editor 'themes' tab title.", LastModified = "2010/07/12", Value = "Themes")]
    public string ZoneEditorThemes => this[nameof (ZoneEditorThemes)];

    /// <summary>Translated message, similar to "Widgets"</summary>
    /// <value>Page editor 'widgets' tab title.</value>
    [ResourceEntry("ZoneEditorControls", Description = "Page editor 'widgets' tab title.", LastModified = "2010/05/11", Value = "Duplicate")]
    public string ZoneEditorControls => this[nameof (ZoneEditorControls)];

    /// <summary>Add personalized version</summary>
    /// <value>Add personalized version</value>
    [ResourceEntry("ZoneEditorAddPersonalizedVersion", Description = "Add personalized version", LastModified = "2015/08/18", Value = "Add personalized version")]
    public string ZoneEditorAddPersonalizedVersion => this[nameof (ZoneEditorAddPersonalizedVersion)];

    /// <summary>Remove this version</summary>
    /// <value>Remove this version</value>
    [ResourceEntry("ZoneEditorRemovePersonalizedVersion", Description = "Remove this version", LastModified = "2015/08/18", Value = "Remove this version")]
    public string ZoneEditorRemovePersonalizedVersion => this[nameof (ZoneEditorRemovePersonalizedVersion)];

    /// <summary>Not specified</summary>
    /// <value>Not specified</value>
    [ResourceEntry("NotSpecified", Description = "Not specified", LastModified = "2015/08/20", Value = "Not specified")]
    public string NotSpecified => this[nameof (NotSpecified)];

    /// <summary>Default</summary>
    /// <value>Default</value>
    [ResourceEntry("Default", Description = "Default", LastModified = "2015/09/08", Value = "Default")]
    public string Default => this[nameof (Default)];

    /// <summary>For</summary>
    /// <value>For</value>
    [ResourceEntry("For", Description = "For", LastModified = "2015/09/09", Value = "For")]
    public string For => this[nameof (For)];

    /// <summary>Personalized for...</summary>
    /// <value>Personalized for...</value>
    [ResourceEntry("PersonalizedFor", Description = "Personalized for...", LastModified = "2015/09/09", Value = "Personalized for...")]
    public string PersonalizedFor => this[nameof (PersonalizedFor)];

    /// <summary>Label: Translations</summary>
    [ResourceEntry("TranslationsSectionTitle", Description = "Label", LastModified = "2011/05/25", Value = "Copy widget settings")]
    public string TranslationsSectionTitle => this[nameof (TranslationsSectionTitle)];

    /// <summary>
    /// Description of the Copy widget settings section in add page template language dialog.
    /// </summary>
    [ResourceEntry("CopyWidgetSettingsDescription", Description = "Description of the Copy widget settings section in add page template language dialog.", LastModified = "2011/05/25", Value = "Widget settings can differ between translations. You can choose to copy widget settings from another translation")]
    public string CopyWidgetSettingsDescription => this[nameof (CopyWidgetSettingsDescription)];

    /// <summary>Label: Basic Templates</summary>
    [ResourceEntry("BasicTemplates", Description = "Label", LastModified = "2019/03/05", Value = "Hybrid Templates")]
    public string BasicTemplates => this[nameof (BasicTemplates)];

    [ResourceEntry("BasicWebformsTemplates", Description = "label: Webforms templates", LastModified = "2019/03/13", Value = "Webforms Templates")]
    public string BasicWebformsTemplates => this[nameof (BasicWebformsTemplates)];

    /// <summary>Label: Custom Templates</summary>
    [ResourceEntry("CustomTemplates", Description = "Label", LastModified = "2009/11/06", Value = "Custom Templates")]
    public string CustomTemplates => this[nameof (CustomTemplates)];

    /// <summary>Label: Templates fro MVC widgets</summary>
    [ResourceEntry("ForMvcWidgetsLabel", Description = "For MVC widgets", LastModified = "2019/03/05", Value = "For MVC widgets")]
    public string ForMvcWidgetsLabel => this[nameof (ForMvcWidgetsLabel)];

    /// <summary>Label: Templates for Webforms widgets</summary>
    [ResourceEntry("ForWebformsWidgetsLabel", Description = "For Webforms widgets", LastModified = "2019/03/05", Value = "For Webforms widgets")]
    public string ForWebformsWidgetsLabel => this[nameof (ForWebformsWidgetsLabel)];

    /// <summary>Label: Templates for MVC and Webforms widgets</summary>
    [ResourceEntry("ForAllWidgetsLabel", Description = "For MVC and Webforms widgets", LastModified = "2019/03/05", Value = "For MVC and Webforms widgets")]
    public string ForAllWidgetsLabel => this[nameof (ForAllWidgetsLabel)];

    [ResourceEntry("BasicWebformsTemplate", Description = "Basic template for Webforms", LastModified = "2019/03/14", Value = "default")]
    public string BasicWebformsTemplate => this[nameof (BasicWebformsTemplate)];

    [ResourceEntry("BasicHybridTemplate", Description = "Basic template for Hybrid", LastModified = "2019/03/14", Value = "default")]
    public string BasicHybridTemplate => this[nameof (BasicHybridTemplate)];

    /// <summary>Label: Learn more with video tutorials</summary>
    [ResourceEntry("LearnMoreWithVideoTutorials", Description = "Label for the external links used in Pages", LastModified = "2011/01/17", Value = "Learn more with video tutorials")]
    public string LearnMoreWithVideoTutorials => this[nameof (LearnMoreWithVideoTutorials)];

    /// <summary>Label: Backend Templates</summary>
    [ResourceEntry("BackendTemplates", Description = "Label", LastModified = "2009/11/06", Value = "Backend Templates")]
    public string BackendTemplates => this[nameof (BackendTemplates)];

    /// <summary>Label: BaseTemplate</summary>
    [ResourceEntry("BaseTemplate", Description = "Label", LastModified = "2014/05/07", Value = "Change base template")]
    public string BaseTemplate => this[nameof (BaseTemplate)];

    /// <summary>Label: 1 Column, Header, Footer</summary>
    [ResourceEntry("OneColumnHeaderFooter", Description = "Label", LastModified = "2009/11/06", Value = "1 Column, Header, Footer")]
    public string OneColumnHeaderFooter => this[nameof (OneColumnHeaderFooter)];

    /// <summary>Label: Left Sidebar, Header, Footer</summary>
    [ResourceEntry("LeftSidebarHeaderFooter", Description = "Label", LastModified = "2009/11/06", Value = "Left Sidebar, Header, Footer")]
    public string LeftSidebarHeaderFooter => this[nameof (LeftSidebarHeaderFooter)];

    /// <summary>Label: Open in new window</summary>
    [ResourceEntry("OpenInNewWindow", Description = "Open in new window", LastModified = "2010/10/04", Value = "Open in new window")]
    public string OpenInNewWindow => this[nameof (OpenInNewWindow)];

    /// <summary>Label: What are themes?</summary>
    [ResourceEntry("WhatAreThemes", Description = "What are themes?", LastModified = "2010/10/04", Value = "What are themes?")]
    public string WhatAreThemes => this[nameof (WhatAreThemes)];

    /// <summary>Label: How to upload my own theme?</summary>
    [ResourceEntry("HowToUploadMyOwnTheme", Description = "How to upload my own theme?", LastModified = "2010/10/04", Value = "How to upload my own theme?")]
    public string HowToUploadMyOwnTheme => this[nameof (HowToUploadMyOwnTheme)];

    /// <summary>Label: Select another template...</summary>
    [ResourceEntry("SelectAnotherTemplate", Description = "Select another template...", LastModified = "2010/10/04", Value = "Select another template...")]
    public string SelectAnotherTemplate => this[nameof (SelectAnotherTemplate)];

    /// <summary>Label: Right Sidebar, Header, Footer</summary>
    [ResourceEntry("RightSidebarHeaderFooter", Description = "Label", LastModified = "2009/11/06", Value = "Right Sidebar, Header, Footer")]
    public string RightSidebarHeaderFooter => this[nameof (RightSidebarHeaderFooter)];

    /// <summary>Label: Left Sidebar, Content</summary>
    [ResourceEntry("LeftSidebar", Description = "Label", LastModified = "2009/11/06", Value = "Left Sidebar, Content")]
    public string LeftSidebar => this[nameof (LeftSidebar)];

    /// <summary>Label: Right Sidebar, Content</summary>
    [ResourceEntry("RightSidebar", Description = "Label", LastModified = "2009/11/06", Value = "Right Sidebar, Content")]
    public string RightSidebar => this[nameof (RightSidebar)];

    /// <summary>Label: 3 Equal Columns, Header, Footer</summary>
    [ResourceEntry("TwoEqualHeaderFooter", Description = "Label", LastModified = "2009/11/06", Value = "2 Equal Columns, Header, Footer")]
    public string TwoEqualHeaderFooter => this[nameof (TwoEqualHeaderFooter)];

    /// <summary>Label: 3 Equal Columns, Header, Footer</summary>
    [ResourceEntry("ThreeEqualHeaderFooter", Description = "Label", LastModified = "2009/11/06", Value = "3 Equal Columns, Header, Footer")]
    public string ThreeEqualHeaderFooter => this[nameof (ThreeEqualHeaderFooter)];

    /// <summary>Label: 2 Sidebars, Header, Footer</summary>
    [ResourceEntry("TwoSidebarsHeaderFooter", Description = "Label", LastModified = "2009/11/06", Value = "2 Sidebars, Header, Footer")]
    public string TwoSidebarsHeaderFooter => this[nameof (TwoSidebarsHeaderFooter)];

    /// <summary>Label: Promo, 3 Columns, Header, Footer</summary>
    [ResourceEntry("Promo3ColumnsHeaderFooter", Description = "Label", LastModified = "2009/11/06", Value = "Promo, 3 Columns, Header, Footer")]
    public string Promo3ColumnsHeaderFooter => this[nameof (Promo3ColumnsHeaderFooter)];

    /// <summary>Label: Layout</summary>
    [ResourceEntry("ShowToolbox", Description = "label", LastModified = "2009/10/23", Value = "Show Toolbox")]
    public string ShowToolbox => this[nameof (ShowToolbox)];

    /// <summary>Label: Hide Toolbox</summary>
    [ResourceEntry("HideToolbox", Description = "label", LastModified = "2009/10/23", Value = "Hide Toolbox")]
    public string HideToolbox => this[nameof (HideToolbox)];

    /// <summary>Label: Toggle toolbox</summary>
    [ResourceEntry("ToggleToolbox", Description = "label", LastModified = "2010/10/18", Value = "Toggle Toolbox")]
    public string ToggleToolbox => this[nameof (ToggleToolbox)];

    /// <summary>Label: Layout</summary>
    [ResourceEntry("LayoutToolbox", Description = "label", LastModified = "2009/10/23", Value = "Layout")]
    public string LayoutToolbox => this[nameof (LayoutToolbox)];

    /// <summary>Label: Content</summary>
    [ResourceEntry("ContentToolbox", Description = "label", LastModified = "2009/10/23", Value = "Content")]
    public string ContentToolbox => this[nameof (ContentToolbox)];

    /// <summary>Label: Theme</summary>
    [ResourceEntry("ThemeToolbox", Description = "word: Theme", LastModified = "2010/07/09", Value = "Theme")]
    public string ThemeToolbox => this[nameof (ThemeToolbox)];

    /// <summary>Label: Settings</summary>
    [ResourceEntry("SettingsToolbox", Description = "word: Settings", LastModified = "2010/07/09", Value = "Settings")]
    public string SettingsToolbox => this[nameof (SettingsToolbox)];

    /// <summary>Themes</summary>
    [ResourceEntry("PageThemesToolboxTitle", Description = "The title of the toolbox for page themes.", LastModified = "2009/09/23", Value = "Set theme <em class='sfInfo'>to have consistent graphic through the site</em>")]
    public string PageThemesToolboxTitle => this[nameof (PageThemesToolboxTitle)];

    /// <summary>Settings</summary>
    [ResourceEntry("PageSettingsToolboxTitle", Description = "The title of the toolbox for page settings.", LastModified = "2009/09/23", Value = "Settings <em class='sfInfo'>to control form behavior</em>")]
    public string PageSettingsToolboxTitle => this[nameof (PageSettingsToolboxTitle)];

    /// <summary>Label: Drag form widgets to the left</summary>
    [ResourceEntry("DragFormWidgetsToTheLeft", Description = "word: Drag form widgets to the left", LastModified = "2017/05/17", Value = "Drag form widgets <em class='sfInfo'>to add fields to the form</em>")]
    public string DragFormWidgetsToTheLeft => this[nameof (DragFormWidgetsToTheLeft)];

    /// <summary>phrase: Provides a way to work with page themes.</summary>
    [ResourceEntry("ThemeToolboxDescription", Description = "phrase: Provides a way to work with page themes.", LastModified = "2010/07/09", Value = "Provides a way to work with page themes.")]
    public string ThemeToolboxDescription => this[nameof (ThemeToolboxDescription)];

    /// <summary>word: Properties</summary>
    [ResourceEntry("ViewProperties", Description = "word: Properties", LastModified = "2014/05/07", Value = "Properties")]
    public string ViewProperties => this[nameof (ViewProperties)];

    [ResourceEntry("TitleAndProperties", Description = "label", LastModified = "2010/07/15", Value = "Title & Properties ")]
    public string TitleAndProperties => this[nameof (TitleAndProperties)];

    /// <summary>Label: Title</summary>
    [ResourceEntry("Title", Description = "label", LastModified = "2010/07/15", Value = "Title")]
    public string Title => this[nameof (Title)];

    /// <summary>phrase: Settings for pages</summary>
    [ResourceEntry("SettingsForPages", Description = "phrase: Settings for pages", LastModified = "2010/04/27", Value = "Settings for pages")]
    public string SettingsForPages => this[nameof (SettingsForPages)];

    /// <summary>phrase: Settings for templates</summary>
    [ResourceEntry("SettingsForTemplates", Description = "phrase: Settings for templates", LastModified = "2010/04/27", Value = "Settings for templates")]
    public string SettingsForTemplates => this[nameof (SettingsForTemplates)];

    /// <summary>phrase: Manage Pages</summary>
    [ResourceEntry("ManagePages", Description = "phrase: Manage Pages", LastModified = "2010/09/30", Value = "Manage Pages")]
    public string ManagePages => this[nameof (ManagePages)];

    /// <summary>Single and Two Columns</summary>
    [ResourceEntry("TwoColumnsSectionTitle", Description = "The title of layout toolbox section.", LastModified = "2009/09/23", Value = "Single and Two Columns")]
    public string TwoColumnsSectionTitle => this[nameof (TwoColumnsSectionTitle)];

    /// <summary>The description of layout toolbox section.</summary>
    [ResourceEntry("TwoColumnsSectionDescription", Description = "The description of layout toolbox section.", LastModified = "2009/09/23", Value = "Contains zone definitions for single and two column layouts")]
    public string TwoColumnsSectionDescription => this[nameof (TwoColumnsSectionDescription)];

    /// <summary>XML Data Source</summary>
    [ResourceEntry("XmlDataSourceTitle", Description = "The title of the XmlDataSource control.", LastModified = "2010/07/26", Value = "XML data source")]
    public string XmlDataSourceTitle => this[nameof (XmlDataSourceTitle)];

    /// <summary>Represents an XML data source to data-bound controls.</summary>
    [ResourceEntry("XmlDataSourceDescription", Description = "The description of the XmlDataSource control.", LastModified = "2009/11/06", Value = "Provides XML data source for other widgets (data bound controls)")]
    public string XmlDataSourceDescription => this[nameof (XmlDataSourceDescription)];

    /// <summary>Site Map Data Source</summary>
    [ResourceEntry("SiteMapDataSourceTitle", Description = "The title of the SiteMapDataSource control.", LastModified = "2010/07/26", Value = "Site map data source")]
    public string SiteMapDataSourceTitle => this[nameof (SiteMapDataSourceTitle)];

    /// <summary>
    /// Provides a data source control that Web server controls and other controls can use to bind to hierarchical site map data.
    /// </summary>
    [ResourceEntry("SiteMapDataSourceDescription", Description = "The description of the SiteMapDataSource control.", LastModified = "2009/11/06", Value = "Provides hierarchical site map data for other widgets")]
    public string SiteMapDataSourceDescription => this[nameof (SiteMapDataSourceDescription)];

    /// <summary>OpenAccess Data Source</summary>
    [ResourceEntry("OpenAccessDataSourceTitle", Description = "The title of the OpenAccessDataSource control.", LastModified = "2009/11/06", Value = "OpenAccess Data Source")]
    public string OpenAccessDataSourceTitle => this[nameof (OpenAccessDataSourceTitle)];

    /// <summary>
    /// The Data Source implementation for Telerik.OpenAccess.
    /// </summary>
    [ResourceEntry("OpenAccessDataSourceDescription", Description = "The description of the OpenAccessDataSource control.", LastModified = "2009/11/06", Value = "Provides Telerik.OpenAccess data source for other widgets")]
    public string OpenAccessDataSourceDescription => this[nameof (OpenAccessDataSourceDescription)];

    /// <summary>100%</summary>
    [ResourceEntry("Col1Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "100%")]
    public string Col1Title => this[nameof (Col1Title)];

    /// <summary>Single column layout.</summary>
    [ResourceEntry("Col1Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Single column layout.")]
    public string Col1Description => this[nameof (Col1Description)];

    /// <summary>25% + 75%</summary>
    [ResourceEntry("Col2T1Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "25% + 75%")]
    public string Col2T1Title => this[nameof (Col2T1Title)];

    /// <summary>Two columns with respectively 25% and 75% width.</summary>
    [ResourceEntry("Col2T1Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Two columns with respectively 25% and 75% width.")]
    public string Col2T1Description => this[nameof (Col2T1Description)];

    /// <summary>33% + 67%</summary>
    [ResourceEntry("Col2T2Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "33% + 67%")]
    public string Col2T2Title => this[nameof (Col2T2Title)];

    /// <summary>Two columns with respectively 33% and 67% width.</summary>
    [ResourceEntry("Col2T2Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Two columns with respectively 33% and 67% width.")]
    public string Col2T2Description => this[nameof (Col2T2Description)];

    /// <summary>50% + 50%</summary>
    [ResourceEntry("Col2T3Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "50% + 50%")]
    public string Col2T3Title => this[nameof (Col2T3Title)];

    /// <summary>Two columns with 50% width each.</summary>
    [ResourceEntry("Col2T3Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Two columns with 50% width each.")]
    public string Col2T3Description => this[nameof (Col2T3Description)];

    /// <summary>67% + 33%</summary>
    [ResourceEntry("Col2T4Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "67% + 33%")]
    public string Col2T4Title => this[nameof (Col2T4Title)];

    /// <summary>Two columns with respectively 67% and 33% width.</summary>
    [ResourceEntry("Col2T4Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Two columns with respectively 67% and 33% width.")]
    public string Col2T4Description => this[nameof (Col2T4Description)];

    /// <summary>75% + 25%</summary>
    [ResourceEntry("Col2T5Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "75% + 25%")]
    public string Col2T5Title => this[nameof (Col2T5Title)];

    /// <summary>Two columns with respectively 67% and 33% width.</summary>
    [ResourceEntry("Col2T5Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Two columns with respectively 75% and 25% width.")]
    public string Col2T5Description => this[nameof (Col2T5Description)];

    /// <summary>33% + 34% + 33%</summary>
    [ResourceEntry("Col3T1Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "33% + 34% + 33%")]
    public string Col3T1Title => this[nameof (Col3T1Title)];

    /// <summary>
    /// Three columns with respectively 33%, 34% and 33% width.
    /// </summary>
    [ResourceEntry("Col3T1Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Three columns with respectively 33%, 34% and 33% width.")]
    public string Col3T1Description => this[nameof (Col3T1Description)];

    /// <summary>25% + 50% + 25%</summary>
    [ResourceEntry("Col3T2Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "25% + 50% + 25%")]
    public string Col3T2Title => this[nameof (Col3T2Title)];

    /// <summary>
    /// Three columns with respectively 25%, 50% and 25% width.
    /// </summary>
    [ResourceEntry("Col3T2Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Three columns with respectively 25%, 50% and 25% width.")]
    public string Col3T2Description => this[nameof (Col3T2Description)];

    /// <summary>4 x 25%</summary>
    [ResourceEntry("Col4T1Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "4 x 25%")]
    public string Col4T1Title => this[nameof (Col4T1Title)];

    /// <summary>Four columns with 25% width each.</summary>
    [ResourceEntry("Col4T1Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Four columns with 25% width each.")]
    public string Col4T1Description => this[nameof (Col4T1Description)];

    /// <summary>5 x 20%</summary>
    [ResourceEntry("Col5T1Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "5 x 20%")]
    public string Col5T1Title => this[nameof (Col5T1Title)];

    /// <summary>Five columns with 20% width each.</summary>
    [ResourceEntry("Col5T1Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Five columns with 20% width each.")]
    public string Col5T1Description => this[nameof (Col5T1Description)];

    /// <summary>fixed + auto</summary>
    [ResourceEntry("Col2T6Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "fixed + auto")]
    public string Col2T6Title => this[nameof (Col2T6Title)];

    /// <summary>
    /// Two columns with respectively fixed width and auto width.
    /// </summary>
    [ResourceEntry("Col2T6Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Two columns with respectively fixed width and auto width.")]
    public string Col2T6Description => this[nameof (Col2T6Description)];

    /// <summary>auto + fixed</summary>
    [ResourceEntry("Col2T7Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "auto + fixed")]
    public string Col2T7Title => this[nameof (Col2T7Title)];

    /// <summary>
    /// Two columns with respectively auto width and fixed width.
    /// </summary>
    [ResourceEntry("Col2T7Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Two columns with respectively auto width and fixed width.")]
    public string Col2T7Description => this[nameof (Col2T7Description)];

    /// <summary>fixed + auto + fixed</summary>
    [ResourceEntry("Col3T3Title", Description = "The title of layout control.", LastModified = "2009/09/23", Value = "fixed + auto + fixed")]
    public string Col3T3Title => this[nameof (Col3T3Title)];

    /// <summary>
    /// Three columns with respectively fixed width, auto width and fixed width.
    /// </summary>
    [ResourceEntry("Col3T3Description", Description = "The description of layout control.", LastModified = "2009/09/23", Value = "Two columns with respectively auto width and fixed width.")]
    public string Col3T3Description => this[nameof (Col3T3Description)];

    /// <summary>Layouts</summary>
    [ResourceEntry("PageLayoutToolboxTitle", Description = "The title of the toolbox for designing pages.", LastModified = "2010/07/26", Value = "Drag layout elements <em class='sfInfo'>to divide the page in columns</em>")]
    public string PageLayoutToolboxTitle => this[nameof (PageLayoutToolboxTitle)];

    /// <summary>Phrase: Or, change the entire template</summary>
    [ResourceEntry("PageLayoutChangeTemplate", Description = "Phrase: Or, change the entire template", LastModified = "2010/09/14", Value = "Or, change the entire template")]
    public string PageLayoutChangeTemplate => this[nameof (PageLayoutChangeTemplate)];

    /// <summary>Phrase: Responsive design Layout</summary>
    [ResourceEntry("ChangeResponsiveDesignLayout", Description = "Phrase: Responsive design Layout", LastModified = "2013/02/01", Value = "Responsive Layout")]
    public string ChangeResponsiveDesignLayout => this[nameof (ChangeResponsiveDesignLayout)];

    /// <summary>
    /// A collection of entities (zone definitions) that compose the layout of a page or a gadget.
    /// </summary>
    [ResourceEntry("PageLayoutToolboxDescription", Description = "The description of the toolbox for designing pages.", LastModified = "2009/09/23", Value = "A collection of entities (zone definitions) that compose the layout of a page or a gadget.")]
    public string PageLayoutToolboxDescription => this[nameof (PageLayoutToolboxDescription)];

    /// <summary>Widgets</summary>
    [ResourceEntry("PageControlsToolboxTitle", Description = "The title of the toolbox for designing pages.", LastModified = "2009/09/23", Value = "Drag widgets <em class='sfInfo'>to add content to the page</em>")]
    public string PageControlsToolboxTitle => this[nameof (PageControlsToolboxTitle)];

    /// <summary>A collection of reusable single-service applications.</summary>
    [ResourceEntry("PageControlsToolboxDescription", Description = "The description of the toolbox for designing pages.", LastModified = "2009/09/23", Value = "A collection of reusable single-service applications.")]
    public string PageControlsToolboxDescription => this[nameof (PageControlsToolboxDescription)];

    /// <summary>Data</summary>
    [ResourceEntry("DataToolboxSectionTitle", Description = "The title of the Data toolbox section in pages toolbox.", LastModified = "2009/09/23", Value = "Data")]
    public string DataToolboxSectionTitle => this[nameof (DataToolboxSectionTitle)];

    /// <summary>Data Toolbox Section</summary>
    [ResourceEntry("DataToolboxSectionDescription", Description = "The description of the Data toolbox section in pages toolbox.", LastModified = "2009/09/23", Value = "Toolbox section containing data controls and data sources.")]
    public string DataToolboxSectionDescription => this[nameof (DataToolboxSectionDescription)];

    /// <summary>RadControls</summary>
    [ResourceEntry("RadControlsTitle", Description = "The title of the RadControls toolbox section in pages toolbox.", LastModified = "2009/09/23", Value = "RadControls")]
    public string RadControlsTitle => this[nameof (RadControlsTitle)];

    /// <summary>
    /// Toolbox section containing Telerik RadControls for ASP.Net AJAX.
    /// </summary>
    [ResourceEntry("RadControlsDescription", Description = "The description of the RadControls toolbox section in pages toolbox.", LastModified = "2009/09/23", Value = "Toolbox section containing Telerik RadControls for ASP.Net AJAX.")]
    public string RadControlsDescription => this[nameof (RadControlsDescription)];

    /// <summary>RadMenu for ASP.NET AJAX</summary>
    [ResourceEntry("RadMenuTitle", Description = "The title of the RadMenu control in pages toolbox.", LastModified = "2009/11/06", Value = "Menu")]
    public string RadMenuTitle => this[nameof (RadMenuTitle)];

    /// <summary>RadMenu for ASP.NET AJAX.</summary>
    [ResourceEntry("RadMenuDescription", Description = "The description of the RadMenu control in pages toolbox.", LastModified = "2009/11/06", Value = "RadMenu for ASP.NET AJAX")]
    public string RadMenuDescription => this[nameof (RadMenuDescription)];

    /// <summary>RadTooTip for ASP.NET AJAX</summary>
    [ResourceEntry("RadToolTipTitle", Description = "The title of the RadToolTip control in pages toolbox.", LastModified = "2009/11/06", Value = "Tool Tip")]
    public string RadToolTipTitle => this[nameof (RadToolTipTitle)];

    /// <summary>RadToolTip for ASP.NET AJAX.</summary>
    [ResourceEntry("RadToolTipDescription", Description = "The description of the RadToolTip control in pages toolbox.", LastModified = "2009/11/06", Value = "RadToolTip for ASP.NET AJAX")]
    public string RadToolTipDescription => this[nameof (RadToolTipDescription)];

    /// <summary>RadBinaryImage for ASP.NET AJAX</summary>
    [ResourceEntry("RadBinaryImageTitle", Description = "The title of the RadBinaryImage control in pages toolbox.", LastModified = "2009/11/06", Value = "Binary Image")]
    public string RadBinaryImageTitle => this[nameof (RadBinaryImageTitle)];

    /// <summary>RadBinaryImage for ASP.NET AJAX.</summary>
    [ResourceEntry("RadBinaryImageDescription", Description = "The description of the RadBinaryImage control in pages toolbox.", LastModified = "2009/11/06", Value = "RadBinaryImage for ASP.NET AJAX")]
    public string RadBinaryImageDescription => this[nameof (RadBinaryImageDescription)];

    /// <summary>RadCalendar for ASP.NET AJAX</summary>
    [ResourceEntry("RadCalendarTitle", Description = "The title of the RadCalendar control in pages toolbox.", LastModified = "2009/11/06", Value = "Calendar")]
    public string RadCalendartitle => this["RadCalendarTitle"];

    /// <summary>RadCalendar for ASP.NET AJAX.</summary>
    [ResourceEntry("RadCalendarDescription", Description = "The description of the RadCalendar control in pages toolbox.", LastModified = "2009/11/06", Value = "RadCalendar for ASP.NET AJAX")]
    public string RadCalendarDescription => this[nameof (RadCalendarDescription)];

    /// <summary>RadPanelBar for ASP.NET AJAX</summary>
    [ResourceEntry("RadPanelBarTitle", Description = "The title of the RadPanelBar control in pages toolbox.", LastModified = "2009/11/06", Value = "Panel Bar")]
    public string RadPanelBarTitle => this[nameof (RadPanelBarTitle)];

    /// <summary>RadPanelBar for ASP.NET AJAX.</summary>
    [ResourceEntry("RadPanelBarDescription", Description = "The description of the RadPanelBar control in pages toolbox.", LastModified = "2009/11/06", Value = "RadPanelBar for ASP.NET AJAX")]
    public string RadPanelBarDescription => this[nameof (RadPanelBarDescription)];

    /// <summary>RadGrid for ASP.NET AJAX</summary>
    [ResourceEntry("RadGridTitle", Description = "The title of the RadGrid control in pages toolbox.", LastModified = "2009/11/06", Value = "Grid")]
    public string RadGridTitle => this[nameof (RadGridTitle)];

    /// <summary>RadGrid for ASP.NET AJAX.</summary>
    [ResourceEntry("RadGridDescription", Description = "The description of the RadGrid control in pages toolbox.", LastModified = "2009/11/06", Value = "RadGrid for ASP.NET AJAX")]
    public string RadGridDescription => this[nameof (RadGridDescription)];

    /// <summary>RadComboBox for ASP.NET AJAX</summary>
    [ResourceEntry("RadComboBoxTitle", Description = "The title of the RadComboBox control in pages toolbox.", LastModified = "2009/11/06", Value = "Combo Box")]
    public string RadComboBoxTitle => this[nameof (RadComboBoxTitle)];

    /// <summary>RadComboBox for ASP.NET AJAX.</summary>
    [ResourceEntry("RadComboBoxDescription", Description = "The description of the RadComboBox control in pages toolbox.", LastModified = "2009/11/06", Value = "RadComboBox for ASP.NET AJAX")]
    public string RadComboBoxDescription => this[nameof (RadComboBoxDescription)];

    /// <summary>RadListBox for ASP.NET AJAX</summary>
    [ResourceEntry("RadListBoxTitle", Description = "The title of the RadListBox control in pages toolbox.", LastModified = "2009/11/06", Value = "List Box")]
    public string RadListBoxTitle => this[nameof (RadListBoxTitle)];

    /// <summary>RadListBox for ASP.NET AJAX.</summary>
    [ResourceEntry("RadListBoxDescription", Description = "The description of the RadListBox control in pages toolbox.", LastModified = "2009/11/06", Value = "RadComboBox for ASP.NET AJAX")]
    public string RadListBoxDescription => this[nameof (RadListBoxDescription)];

    /// <summary>RadScheduler for ASP.NET AJAX</summary>
    [ResourceEntry("RadSchedulerTitle", Description = "The title of the RadScheduler control in pages toolbox.", LastModified = "2009/11/06", Value = "Scheduler")]
    public string RadSchedulerTitle => this[nameof (RadSchedulerTitle)];

    /// <summary>RadScheduler for ASP.NET AJAX.</summary>
    [ResourceEntry("RadSchedulerDescription", Description = "The description of the RadScheduler control in pages toolbox.", LastModified = "2009/11/06", Value = "RadScheduler for ASP.NET AJAX")]
    public string RadSchedulerDescription => this[nameof (RadSchedulerDescription)];

    /// <summary>RadEditor for ASP.NET AJAX</summary>
    [ResourceEntry("RadEditorTitle", Description = "The title of the RadEditor control in pages toolbox.", LastModified = "2009/11/06", Value = "Editor")]
    public string RadEditorTitle => this[nameof (RadEditorTitle)];

    /// <summary>RadEditor for ASP.NET AJAX.</summary>
    [ResourceEntry("RadEditorDescription", Description = "The description of the RadEditor control in pages toolbox.", LastModified = "2009/11/06", Value = "RadEditor for ASP.NET AJAX")]
    public string RadEditorDescription => this[nameof (RadEditorDescription)];

    /// <summary>RadWindow for ASP.NET AJAX</summary>
    [ResourceEntry("RadWindowTitle", Description = "The title of the RadWindow control in pages toolbox.", LastModified = "2009/11/06", Value = "Window")]
    public string RadWindowTitle => this[nameof (RadWindowTitle)];

    /// <summary>RadWindow for ASP.NET AJAX.</summary>
    [ResourceEntry("RadWindowDescription", Description = "The description of the RadWindow control in pages toolbox.", LastModified = "2009/11/06", Value = "RadWindow for ASP.NET AJAX")]
    public string RadWindowDescription => this[nameof (RadWindowDescription)];

    /// <summary>RadRotator for ASP.NET AJAX</summary>
    [ResourceEntry("RadRotatorTitle", Description = "The title of the RadRotator control in pages toolbox.", LastModified = "2009/11/06", Value = "Rotator")]
    public string RadRotatorTitle => this[nameof (RadRotatorTitle)];

    /// <summary>RadWindow for ASP.NET AJAX.</summary>
    [ResourceEntry("RadRotatorDescription", Description = "The description of the RadRotator control in pages toolbox.", LastModified = "2009/11/06", Value = "RadRotator for ASP.NET AJAX")]
    public string RadRotatorDescription => this[nameof (RadRotatorDescription)];

    /// <summary>Captcha</summary>
    [ResourceEntry("RadCaptchaTitle", Description = "The title of the RadCaptcha control in pages toolbox.", LastModified = "2009/11/06", Value = "Captcha")]
    public string RadCaptchaTitle => this[nameof (RadCaptchaTitle)];

    /// <summary>RadCaptcha for ASP.NET AJAX.</summary>
    [ResourceEntry("RadCaptchaDescription", Description = "The description of the RadCaptcha control in pages toolbox.", LastModified = "2009/11/06", Value = "RadCaptcha for ASP.NET AJAX")]
    public string RadCaptchaDescription => this[nameof (RadCaptchaDescription)];

    /// <summary>Site Map</summary>
    [ResourceEntry("RadSiteMapTitle", Description = "The title of the RadSiteMap control in pages toolbox.", LastModified = "2009/11/06", Value = "Site Map")]
    public string RadSiteMapTitle => this[nameof (RadSiteMapTitle)];

    /// <summary>RadCaptcha for ASP.NET AJAX.</summary>
    [ResourceEntry("RadSiteMapDescription", Description = "The description of the RadSiteMap control in pages toolbox.", LastModified = "2009/11/06", Value = "RadSiteMap for ASP.NET AJAX")]
    public string RadSiteMapDescription => this[nameof (RadSiteMapDescription)];

    /// <summary>RadChart for ASP.NET AJAX</summary>
    [ResourceEntry("RadChartTitle", Description = "The title of the RadChart control in pages toolbox.", LastModified = "2009/11/17", Value = "Chart")]
    public string RadChartTitle => this[nameof (RadChartTitle)];

    /// <summary>RadChart for ASP.NET AJAX.</summary>
    [ResourceEntry("RadChartDescription", Description = "The description of the RadChart control in pages toolbox.", LastModified = "2009/11/17", Value = "RadChart for ASP.NET AJAX")]
    public string RadChartDescription => this[nameof (RadChartDescription)];

    /// <summary>RadColorPicker for ASP.NET AJAX</summary>
    [ResourceEntry("RadColorPickerTitle", Description = "The title of the RadColorPicker control in pages toolbox.", LastModified = "2009/11/17", Value = "Color Picker")]
    public string RadColorPickerTitle => this[nameof (RadColorPickerTitle)];

    /// <summary>RadColorPicker for ASP.NET AJAX.</summary>
    [ResourceEntry("RadColorPickerDescription", Description = "The description of the RadColorPicker control in pages toolbox.", LastModified = "2009/11/17", Value = "RadColorPicker for ASP.NET AJAX")]
    public string RadColorPickerDescription => this[nameof (RadColorPickerDescription)];

    /// <summary>RadDock for ASP.NET AJAX</summary>
    [ResourceEntry("RadDockTitle", Description = "The title of the RadDock control in pages toolbox.", LastModified = "2009/11/17", Value = "Dock")]
    public string RadDockTitle => this[nameof (RadDockTitle)];

    /// <summary>RadDock for ASP.NET AJAX.</summary>
    [ResourceEntry("RadDockDescription", Description = "The description of the RadDock control in pages toolbox.", LastModified = "2009/11/17", Value = "RadDock for ASP.NET AJAX")]
    public string RadDockDescription => this[nameof (RadDockDescription)];

    /// <summary>RadDockZone for ASP.NET AJAX</summary>
    [ResourceEntry("RadDockZoneTitle", Description = "The title of the RadDockZone control in pages toolbox.", LastModified = "2009/11/17", Value = "Dock Zone")]
    public string RadDockZoneTitle => this[nameof (RadDockZoneTitle)];

    /// <summary>RadDockZone for ASP.NET AJAX.</summary>
    [ResourceEntry("RadDockZoneDescription", Description = "The description of the RadDockZone control in pages toolbox.", LastModified = "2009/11/17", Value = "RadDockZone for ASP.NET AJAX")]
    public string RadDockZoneDescription => this[nameof (RadDockZoneDescription)];

    /// <summary>RadDateTimePicker for ASP.NET AJAX</summary>
    [ResourceEntry("RadDateTimePickerTitle", Description = "The title of the RadDateTimePicker control in pages toolbox.", LastModified = "2009/11/17", Value = "Date Time Picker")]
    public string RadDateTimePickerTitle => this[nameof (RadDateTimePickerTitle)];

    /// <summary>RadDateTimePicker for ASP.NET AJAX.</summary>
    [ResourceEntry("RadDateTimePickerDescription", Description = "The description of the RadDateTimePicker control in pages toolbox.", LastModified = "2009/11/17", Value = "RadDateTimePicker for ASP.NET AJAX")]
    public string RadDateTimePickerDescription => this[nameof (RadDateTimePickerDescription)];

    /// <summary>RadFileExplorer for ASP.NET AJAX</summary>
    [ResourceEntry("RadFileExplorerTitle", Description = "The title of the RadFileExplorer control in pages toolbox.", LastModified = "2009/11/17", Value = "File Explorer")]
    public string RadFileExplorerTitle => this[nameof (RadFileExplorerTitle)];

    /// <summary>RadFileExplorer for ASP.NET AJAX.</summary>
    [ResourceEntry("RadFileExplorerDescription", Description = "The description of the RadFileExplorer control in pages toolbox.", LastModified = "2009/11/17", Value = "RadFileExplorer for ASP.NET AJAX")]
    public string RadFileExplorerDescription => this[nameof (RadFileExplorerDescription)];

    /// <summary>RadFormDecorator for ASP.NET AJAX</summary>
    [ResourceEntry("RadFormDecoratorTitle", Description = "The title of the RadFormDecorator control in pages toolbox.", LastModified = "2009/11/17", Value = "Form Decorator")]
    public string RadFormDecoratorTitle => this[nameof (RadFormDecoratorTitle)];

    /// <summary>RadFormDecorator for ASP.NET AJAX.</summary>
    [ResourceEntry("RadFormDecoratorDescription", Description = "The description of the RadFormDecorator control in pages toolbox.", LastModified = "2009/11/17", Value = "RadFormDecorator for ASP.NET AJAX")]
    public string RadFormDecoratorDescription => this[nameof (RadFormDecoratorDescription)];

    /// <summary>RadInputManager for ASP.NET AJAX</summary>
    [ResourceEntry("RadInputManagerTitle", Description = "The title of the RadInputManager control in pages toolbox.", LastModified = "2009/11/17", Value = "Input Manager")]
    public string RadInputManagerTitle => this[nameof (RadInputManagerTitle)];

    /// <summary>RadInputManager for ASP.NET AJAX.</summary>
    [ResourceEntry("RadInputManagerDescription", Description = "The description of the RadInputManager control in pages toolbox.", LastModified = "2009/11/17", Value = "RadInputManager for ASP.NET AJAX")]
    public string RadInputManagerDescription => this[nameof (RadInputManagerDescription)];

    /// <summary>RadTextBox for ASP.NET AJAX</summary>
    [ResourceEntry("RadTextBoxTitle", Description = "The title of the RadTextBox control in pages toolbox.", LastModified = "2009/11/17", Value = "Text Box")]
    public string RadTextBoxTitle => this[nameof (RadTextBoxTitle)];

    /// <summary>RadTextBox for ASP.NET AJAX.</summary>
    [ResourceEntry("RadTextBoxDescription", Description = "The description of the RadTextBox control in pages toolbox.", LastModified = "2009/11/17", Value = "RadTextBoxDescription for ASP.NET AJAX")]
    public string RadTextBoxDescription => this[nameof (RadTextBoxDescription)];

    /// <summary>RadNumericTextBox for ASP.NET AJAX</summary>
    [ResourceEntry("RadNumericTextBoxTitle", Description = "The title of the RadTextBox control in pages toolbox.", LastModified = "2009/11/17", Value = "Numeric Text Box")]
    public string RadNumericTextBoxTitle => this[nameof (RadNumericTextBoxTitle)];

    /// <summary>RadNumeriCtextBox for ASP.NET AJAX.</summary>
    [ResourceEntry("RadNumericTextBoxDescription", Description = "The description of the RadNumericTextBox control in pages toolbox.", LastModified = "2009/11/17", Value = "RadNumericTextBox for ASP.NET AJAX")]
    public string RadNumericTextBoxDescription => this[nameof (RadNumericTextBoxDescription)];

    /// <summary>RadDateInput for ASP.NET AJAX</summary>
    [ResourceEntry("RadDateInputTitle", Description = "The title of the RadDateInput control in pages toolbox.", LastModified = "2009/11/17", Value = "Date Input")]
    public string RadDateInputTitle => this[nameof (RadDateInputTitle)];

    /// <summary>RadDateInput for ASP.NET AJAX.</summary>
    [ResourceEntry("RadDateInputDescription", Description = "The description of the RadDateInput control in pages toolbox.", LastModified = "2009/11/17", Value = "RadDateInput for ASP.NET AJAX")]
    public string RadDateInputDescription => this[nameof (RadDateInputDescription)];

    /// <summary>RadMaskedTextBox for ASP.NET AJAX</summary>
    [ResourceEntry("RadMaskedTextBoxTitle", Description = "The title of the RadMaskedTextBox control in pages toolbox.", LastModified = "2009/11/17", Value = "Masked Text Box")]
    public string RadMaskedTextBoxTitle => this[nameof (RadMaskedTextBoxTitle)];

    /// <summary>RadMaskedTextBox for ASP.NET AJAX.</summary>
    [ResourceEntry("RadMaskedTextBoxDescription", Description = "The description of the RadMaskedTextBox control in pages toolbox.", LastModified = "2009/11/17", Value = "RadMaskedTextBox for ASP.NET AJAX")]
    public string RadMaskedTextBoxDescription => this[nameof (RadMaskedTextBoxDescription)];

    /// <summary>RadListView for ASP.NET AJAX</summary>
    [ResourceEntry("RadListViewTitle", Description = "The title of the RadListView control in pages toolbox.", LastModified = "2009/11/17", Value = "List View")]
    public string RadListViewTitle => this[nameof (RadListViewTitle)];

    /// <summary>RadListView for ASP.NET AJAX.</summary>
    [ResourceEntry("RadListViewDescription", Description = "The description of the RadListView control in pages toolbox.", LastModified = "2009/11/17", Value = "RadListView for ASP.NET AJAX")]
    public string RadListViewDescription => this[nameof (RadListViewDescription)];

    /// <summary>RadSkinManager for ASP.NET AJAX</summary>
    [ResourceEntry("RadSkinManagerTitle", Description = "The title of the RadSkinManager control in pages toolbox.", LastModified = "2009/11/17", Value = "Skin Manager")]
    public string RadSkinManagerTitle => this[nameof (RadSkinManagerTitle)];

    /// <summary>RadSkinManager for ASP.NET AJAX.</summary>
    [ResourceEntry("RadSkinManagerDescription", Description = "The description of the RadSkinManager control in pages toolbox.", LastModified = "2009/11/17", Value = "RadSkinManager for ASP.NET AJAX")]
    public string RadSkinManagerDescription => this[nameof (RadSkinManagerDescription)];

    /// <summary>RadSlider for ASP.NET AJAX</summary>
    [ResourceEntry("RadSliderTitle", Description = "The title of the RadSlider control in pages toolbox.", LastModified = "2009/11/17", Value = "Slider")]
    public string RadSliderTitle => this[nameof (RadSliderTitle)];

    /// <summary>RadSlider for ASP.NET AJAX.</summary>
    [ResourceEntry("RadSliderDescription", Description = "The description of the RadSlider control in pages toolbox.", LastModified = "2009/11/17", Value = "RadSlider for ASP.NET AJAX")]
    public string RadSliderDescription => this[nameof (RadSliderDescription)];

    /// <summary>RadSpell for ASP.NET AJAX</summary>
    [ResourceEntry("RadSpellTitle", Description = "The title of the RadSpell control in pages toolbox.", LastModified = "2009/11/17", Value = "Spell")]
    public string RadSpellTitle => this[nameof (RadSpellTitle)];

    /// <summary>RadSpell for ASP.NET AJAX.</summary>
    [ResourceEntry("RadSpellDescription", Description = "The description of the RadSpell control in pages toolbox.", LastModified = "2009/11/17", Value = "RadSpell for ASP.NET AJAX")]
    public string RadSpellDescription => this[nameof (RadSpellDescription)];

    /// <summary>RadSplitter for ASP.NET AJAX</summary>
    [ResourceEntry("RadSplitterTitle", Description = "The title of the RadSplitter control in pages toolbox.", LastModified = "2009/11/17", Value = "Splitter")]
    public string RadSplitterTitle => this["RadSplitter"];

    /// <summary>RadSplitter for ASP.NET AJAX.</summary>
    [ResourceEntry("RadSplitterDescription", Description = "The description of the RadSplitter control in pages toolbox.", LastModified = "2009/11/17", Value = "RadSplitter for ASP.NET AJAX")]
    public string RadSplitterDescription => this[nameof (RadSplitterDescription)];

    /// <summary>RadStyleSheetManager for ASP.NET AJAX</summary>
    [ResourceEntry("RadStyleSheetManagerTitle", Description = "The title of the RadStyleSheet control in pages toolbox.", LastModified = "2009/11/17", Value = "Style Sheet Manager")]
    public string RadStyleSheetManagerTitle => this[nameof (RadStyleSheetManagerTitle)];

    /// <summary>RadStyleSheetManager for ASP.NET AJAX.</summary>
    [ResourceEntry("RadStyleSheetManagerDescrtiption", Description = "The description of the RadStyleSheetManager control in pages toolbox.", LastModified = "2009/11/17", Value = "RadStyleSheetManager for ASP.NET AJAX")]
    public string RadStyleSheetManagerDescription => this[nameof (RadStyleSheetManagerDescription)];

    /// <summary>RadTabStrip for ASP.NET AJAX</summary>
    [ResourceEntry("RadTabStripTitle", Description = "The title of the RadTabStrip control in pages toolbox.", LastModified = "2009/11/17", Value = "Tab Strip")]
    public string RadTabStripTitle => this[nameof (RadTabStripTitle)];

    /// <summary>RadTabStrip for ASP.NET AJAX.</summary>
    [ResourceEntry("RadTabStripDescription", Description = "The description of the RadTabStrip control in pages toolbox.", LastModified = "2009/11/17", Value = "RadTabStrip for ASP.NET AJAX")]
    public string RadTabStripDescription => this[nameof (RadTabStripDescription)];

    /// <summary>RadToolBar for ASP.NET AJAX</summary>
    [ResourceEntry("RadToolBarTitle", Description = "The title of the  control in pages toolbox.", LastModified = "2009/11/17", Value = "Tool Bar")]
    public string RatToolBarTitle => this["RadToolBarTitle"];

    /// <summary>RadToolBar for ASP.NET AJAX.</summary>
    [ResourceEntry("RadToolBarDescription", Description = "The description of the RadToolBar control in pages toolbox.", LastModified = "2009/11/17", Value = "RadToolBar for ASP.NET AJAX")]
    public string RadToolBarDescription => this[nameof (RadToolBarDescription)];

    /// <summary>RadTreeView for ASP.NET AJAX</summary>
    [ResourceEntry("RadTreeViewTitle", Description = "The title of the RadTreeView control in pages toolbox.", LastModified = "2009/11/17", Value = "Tree View")]
    public string RadTreeViewTitle => this[nameof (RadTreeViewTitle)];

    /// <summary>RadTreeView for ASP.NET AJAX.</summary>
    [ResourceEntry("RadTreeViewDescription", Description = "The description of the RadTreeView control in pages toolbox.", LastModified = "2009/11/17", Value = "RadTreeview for ASP.NET AJAX")]
    public string RadTreeViewDescription => this[nameof (RadTreeViewDescription)];

    /// <summary>RadUpload for ASP.NET AJAX</summary>
    [ResourceEntry("RadUploadTitle", Description = "The title of the RadUpload control in pages toolbox.", LastModified = "2009/11/17", Value = "Upload")]
    public string RadUploadTitle => this[nameof (RadUploadTitle)];

    /// <summary>RadUpload for ASP.NET AJAX.</summary>
    [ResourceEntry("RadUploadDescription", Description = "The description of the RadUpload control in pages toolbox.", LastModified = "2009/11/17", Value = "RadUpload for ASP.NET AJAX")]
    public string RadUploadDescription => this[nameof (RadUploadDescription)];

    /// <summary>RadWindowManager for ASP.NET AJAX</summary>
    [ResourceEntry("RAdWindowManagerTitle", Description = "The title of the RadWindowManager control in pages toolbox.", LastModified = "2009/11/17", Value = "Window Manager")]
    public string RadWindowManagerTitle => this[nameof (RadWindowManagerTitle)];

    /// <summary>RadWindowManager for ASP.NET AJAX.</summary>
    [ResourceEntry("RadWindowManagerDescription", Description = "The description of the RadWindowManager control in pages toolbox.", LastModified = "2009/11/17", Value = "RadWindowManager for ASP.NET AJAX")]
    public string RadWindowManagerDescription => this["RadwindowManagerDescription"];

    /// <summary>RadXmlHttpPanel for ASP.NET AJAX</summary>
    [ResourceEntry("RadXmlHttpPanelTitle", Description = "The title of the RadXmlHttpPanel control in pages toolbox.", LastModified = "2009/11/17", Value = "XmlHttpPanel")]
    public string RadXmlHttpPanelTitle => this[nameof (RadXmlHttpPanelTitle)];

    /// <summary>RadXmlHttpPanel for ASP.NET AJAX.</summary>
    [ResourceEntry("RadXmlHttpPanelDescription", Description = "The description of the RadXmlHttpPanel control in pages toolbox.", LastModified = "2009/11/17", Value = "RadXmlHttpPanel for ASP.NET AJAX")]
    public string RadXmlHttpPanelDescription => this[nameof (RadXmlHttpPanelDescription)];

    /// <summary>Content View control.</summary>
    [ResourceEntry("ContentViewTitle", Description = "The title of the ContentView control in pages toolbox.", LastModified = "2009/11/06", Value = "Content View")]
    public string ContentViewTitle => this[nameof (ContentViewTitle)];

    /// <summary>
    /// The description of the ContentView control in pages toolbox.
    /// </summary>
    [ResourceEntry("ContentViewDescription", Description = "The description of the ContentView control in pages toolbox.", LastModified = "2009/11/06", Value = "Content View control")]
    public string ContentViewDescription => this[nameof (ContentViewDescription)];

    /// <summary>RadControls</summary>
    [ResourceEntry("ContentToolboxSectionTitle", Description = "The title of the Content toolbox section in pages toolbox.", LastModified = "2009/10/28", Value = "Content")]
    public string ContentToolboxSectionTitle => this[nameof (ContentToolboxSectionTitle)];

    /// <summary>
    /// Toolbox section containing Telerik RadControls for ASP.Net AJAX.
    /// </summary>
    [ResourceEntry("ContentToolboxSectionDescription", Description = "The description of the Content toolbox section in pages toolbox.", LastModified = "2009/10/28", Value = "Toolbox section containing Content controls.")]
    public string ContentToolboxSectionDescription => this[nameof (ContentToolboxSectionDescription)];

    /// <summary>The title of the Blogs View control in pages toolbox.</summary>
    [ResourceEntry("BlogsViewTitle", Description = "The title of the Blogs view control in pages toolbox.", LastModified = "2009/12/18", Value = "Blogs View")]
    public string BlogsViewTitle => this[nameof (BlogsViewTitle)];

    /// <summary>
    /// The description of the Blogs View control in pages toolbox.
    /// </summary>
    [ResourceEntry("BlogsViewDescription", Description = "The description of the Blogs View control in pages toolbox.", LastModified = "2009/12/18", Value = "Blogs View control")]
    public string BlogsViewDescription => this[nameof (BlogsViewDescription)];

    /// <summary>The title of the News View control in pages toolbox.</summary>
    [ResourceEntry("NewsViewTitle", Description = "The title of the News view control in pages toolbox.", LastModified = "2009/12/18", Value = "News View")]
    public string NewsViewTitle => this[nameof (NewsViewTitle)];

    /// <summary>
    /// The description of the News View control in pages toolbox.
    /// </summary>
    [ResourceEntry("NewsViewDescription", Description = "The description of the News View control in pages toolbox.", LastModified = "2009/12/18", Value = "News View control")]
    public string NewsViewDescription => this[nameof (NewsViewDescription)];

    /// <summary>
    /// The title of the Events View control in pages toolbox.
    /// </summary>
    [ResourceEntry("EventsViewTitle", Description = "The title of the Events view control in pages toolbox.", LastModified = "2009/12/18", Value = "Events View")]
    public string EventsViewTitle => this[nameof (EventsViewTitle)];

    /// <summary>
    /// The description of the Events View control in pages toolbox.
    /// </summary>
    [ResourceEntry("EventsViewDescription", Description = "The description of the Events View control in pages toolbox.", LastModified = "2009/12/18", Value = "Events View control")]
    public string EventsViewDescription => this[nameof (EventsViewDescription)];

    /// <summary>Rating</summary>
    [ResourceEntry("RatingToolboxSectionTitle", Description = "The title of the Rating toolbox section in pages toolbox.", LastModified = "2009/11/25", Value = "Rating")]
    public string RatingToolboxSectionTitle => this[nameof (RatingToolboxSectionTitle)];

    /// <summary>Toolbox section containing rating controls.</summary>
    [ResourceEntry("RatingToolboxSectionDescription", Description = "The description of the Rating toolbox section in pages toolbox.", LastModified = "2009/11/25", Value = "Toolbox section containing rating controls.")]
    public string RatingToolboxSectionDescription => this[nameof (RatingToolboxSectionDescription)];

    /// <summary>To Parent Directory</summary>
    [ResourceEntry("ToParentDirectory", Description = "Label", LastModified = "2009/08/30", Value = "To Parent Directory")]
    public string ToParentDirectory => this[nameof (ToParentDirectory)];

    /// <summary>Settings</summary>
    [ResourceEntry("Settings", Description = "Word: Settings", LastModified = "2009/04/30", Value = "Advanced Settings")]
    public string Settings => this[nameof (Settings)];

    /// <summary>Users</summary>
    [ResourceEntry("UserPoliciesTitle", Description = "The title of User Policies Handler.", LastModified = "2009/04/30", Value = "Users")]
    public string UserPoliciesTitle => this[nameof (UserPoliciesTitle)];

    /// <summary>Defines configuration settings for individual users.</summary>
    [ResourceEntry("UserPoliciesDescription", Description = "The description of User Policies Handler.", LastModified = "2009/04/30", Value = "Defines configuration settings for individual users.")]
    public string UserPoliciesDescription => this[nameof (UserPoliciesDescription)];

    /// <summary>Roles</summary>
    [ResourceEntry("RolePoliciesTitle", Description = "The title of Role Policies Handler.", LastModified = "2009/04/30", Value = "Roles")]
    public string RolePoliciesTitle => this[nameof (RolePoliciesTitle)];

    /// <summary>Defines configuration settings for individual roles.</summary>
    [ResourceEntry("RolePoliciesDescription", Description = "The description of Role Policies Handler.", LastModified = "2009/04/30", Value = "Defines configuration settings for individual roles.")]
    public string RolePoliciesDescription => this[nameof (RolePoliciesDescription)];

    /// <summary>Pages</summary>
    [ResourceEntry("PagePoliciesTitle", Description = "The title of Page Policies Handler.", LastModified = "2009/04/30", Value = "Pages")]
    public string PagePoliciesTitle => this[nameof (PagePoliciesTitle)];

    /// <summary>Defines configuration settings for individual pages.</summary>
    [ResourceEntry("PagePoliciesDescription", Description = "The description of Page Policies Handler.", LastModified = "2009/04/30", Value = "Defines configuration settings for individual pages.")]
    public string PagePoliciesDescription => this[nameof (PagePoliciesDescription)];

    /// <summary>Page Groups</summary>
    [ResourceEntry("PageGroupPoliciesTitle", Description = "The title of Role Group Policies Handler.", LastModified = "2009/04/30", Value = "Sections")]
    public string PageGroupPoliciesTitle => this[nameof (PageGroupPoliciesTitle)];

    /// <summary>Defines configuration settings for page groups.</summary>
    [ResourceEntry("PageGroupPoliciesDescription", Description = "The description of Section Policies Handler.", LastModified = "2009/04/30", Value = "Defines configuration settings for sections.")]
    public string PageGroupPoliciesDescription => this[nameof (PageGroupPoliciesDescription)];

    /// <summary>All Labels</summary>
    [ResourceEntry("AllLabelsTitle", Description = "LocalizationPanel label.", LastModified = "2009/04/30", Value = "All Labels")]
    public string AllLabelsTitle => this[nameof (AllLabelsTitle)];

    /// <summary>Displays  all labels from all classes.</summary>
    [ResourceEntry("AllLabelsDescription", Description = "Description of \"All Lables\" command.", LastModified = "2009/04/30", Value = "Displays  all labels from all classes.")]
    public string AllLabelsDescription => this[nameof (AllLabelsDescription)];

    /// <summary>Filter Labels by Type</summary>
    [ResourceEntry("LabelsTitle", Description = "The title of Labels Command Panel in Localization control panel.", LastModified = "2009/04/30", Value = "Filter Labels by Type")]
    public string LabelsTitle => this[nameof (LabelsTitle)];

    /// <summary>Labels and Messages</summary>
    [ResourceEntry("LabelsAndMassages", Description = "The title of Sitefinity/Settings/Labels section.", LastModified = "2009/04/29", Value = "Interface Labels & Messages")]
    public string LabelsAndMassages => this[nameof (LabelsAndMassages)];

    /// <summary>Policies</summary>
    [ResourceEntry("PoliciesLabel", Description = "The title of Sitefinity/Admin/Configuraiton/Policies section.", LastModified = "2009/04/29", Value = "Policies")]
    public string PoliciesLabel => this[nameof (PoliciesLabel)];

    /// <summary>Policies Title</summary>
    [ResourceEntry("PoliciesTitle", Description = "The title of Labels Command Panel in Localization control panel.", LastModified = "2009/04/30", Value = "Policies")]
    public string PoliciesTitle => this[nameof (PoliciesTitle)];

    /// <summary>Description for Policies command.</summary>
    [ResourceEntry("PoliciesDescription", Description = "Description of \"Policies\" command.", LastModified = "2009/04/30", Value = "For people who knows how to write config files, here are all settings, listed in sections")]
    public string PoliciesDescription => this[nameof (PoliciesDescription)];

    /// <summary>Settings</summary>
    [ResourceEntry("SettingsUrlName", Description = "The name that appears in the URL.", LastModified = "2010/09/30", Value = "Settings")]
    public string SettingsUrlName => this[nameof (SettingsUrlName)];

    /// <summary>Settings</summary>
    [ResourceEntry("SettingsTitle", Description = "The title of Settings command Panel in Configuration control panel.", LastModified = "2009/07/15", Value = "Settings")]
    public string SettingsTitle => this[nameof (SettingsTitle)];

    /// <summary>Description for Settings menu item</summary>
    [ResourceEntry("SettingsDescriptionMenu", Description = "Description of \"Settings\" command.", LastModified = "2010/09/01", Value = "Provides user interface for managing Sitefinity settings.")]
    public string SettingsDescriptionMenu => this[nameof (SettingsDescriptionMenu)];

    /// <summary>Description for Settings command.</summary>
    [ResourceEntry("SettingsDescription", Description = "Description of \"Settings\" command.", LastModified = "2009/07/15", Value = "For people who know how to write a config files, here are all settings, listed in sections.")]
    public string SettingsDescription => this[nameof (SettingsDescription)];

    /// <summary>BasicSettings</summary>
    [ResourceEntry("BasicSettingsUrlName", Description = "The name that appears in the URL.", LastModified = "2010/09/30", Value = "Basic")]
    public string BasicSettingsUrlName => this[nameof (BasicSettingsUrlName)];

    /// <summary>Basic Settings</summary>
    [ResourceEntry("BasicSettingsTitle", Description = "The title of BasicSettings backend page.", LastModified = "2010/09/30", Value = "Basic Settings")]
    public string BasicSettingsTitle => this[nameof (BasicSettingsTitle)];

    /// <summary>Basic Settings</summary>
    [ResourceEntry("BasicSettingsHtmlTitle", Description = "The html title of the BasicSettings backend page.", LastModified = "2010/09/30", Value = "Basic Settings")]
    public string BasicSettingsHtmlTitle => this[nameof (BasicSettingsHtmlTitle)];

    /// <summary>
    /// Provides more user friendly interface for managing settings.
    /// </summary>
    [ResourceEntry("BasicSettingsDescription", Description = "Description of Basic Settings backend page.", LastModified = "2010/09/30", Value = "Provides more user friendly interface for managing settings.")]
    public string BasicSettingsDescription => this[nameof (BasicSettingsDescription)];

    /// <summary>Word: Dashboard.</summary>
    [ResourceEntry("Dashboard", Description = "word : Dashboard", LastModified = "2009/04/06", Value = "Dashboard")]
    public string Dashboard => this[nameof (Dashboard)];

    /// <summary>Word: Dashboard.</summary>
    [ResourceEntry("DashboardHtmlTitle", Description = "word : Dashboard", LastModified = "2010/02/15", Value = "Dashboard")]
    public string DashboardHtmlTitle => this[nameof (DashboardHtmlTitle)];

    /// <summary>
    /// Message: Displays a summary of current site state and provides shortcuts to common tasks.
    /// </summary>
    [ResourceEntry("DashboardDescription", Description = "Description of the backend dashboard page.", LastModified = "2009/04/22", Value = "Displays a summary of current site state and provides shortcuts to common tasks.")]
    public string DashboardDescription => this[nameof (DashboardDescription)];

    /// <summary>Word: Pages.</summary>
    [ResourceEntry("Pages", Description = "Pages", LastModified = "2009/04/06", Value = "Pages")]
    public string Pages => this[nameof (Pages)];

    /// <summary>Word: Page</summary>
    [ResourceEntry("Page", Description = "Page", LastModified = "2011/02/02", Value = "Page")]
    public string Page => this[nameof (Page)];

    /// <summary>Phrase: And their child pages</summary>
    [ResourceEntry("AndTheirChildPages", Description = "And their child pages", LastModified = "2018/08/16", Value = "And their child pages")]
    public string AndTheirChildPages => this[nameof (AndTheirChildPages)];

    /// <summary>Phrase: And its child pages</summary>
    [ResourceEntry("AndItsChildPages", Description = "And its child pages", LastModified = "2018/09/06", Value = "And its child pages")]
    public string AndItsChildPages => this[nameof (AndItsChildPages)];

    /// <summary>Word: New Pages.</summary>
    [ResourceEntry("NewPages", Description = "NewPages", LastModified = "2009/04/06", Value = "New Pages")]
    public string NewPages => this[nameof (NewPages)];

    /// <summary>Word: Pages.</summary>
    [ResourceEntry("PagesHtmlTitle", Description = "Title of the 'Pages' page in the Sitefinity Backend", LastModified = "2010/02/15", Value = "Pages")]
    public string PagesHtmlTitle => this[nameof (PagesHtmlTitle)];

    /// <summary>
    /// Provides user interface for creating and managing pages.
    /// </summary>
    [ResourceEntry("PagesDescription", Description = "Description of Pages section.", LastModified = "2009/04/22", Value = "Provides user interface for creating and managing pages.")]
    public string PagesDescription => this[nameof (PagesDescription)];

    /// <summary>Word: Templates.</summary>
    [ResourceEntry("Templates", Description = "Templates", LastModified = "2009/04/06", Value = "Templates")]
    public string Templates => this[nameof (Templates)];

    /// <summary>
    /// These templates don't fit to my design. What should I do?
    /// </summary>
    [ResourceEntry("TemplateDoesNotFit", Description = "These templates don't fit to my design. What should I do?", LastModified = "2009/04/06", Value = "These templates don't fit to my design. What should I do?")]
    public string TemplateDoesNotFit => this[nameof (TemplateDoesNotFit)];

    /// <summary>Create your own layout by choosing "No template".</summary>
    [ResourceEntry("CreateOwnLayout", Description = "Create your own layout by choosing \"No template\".", LastModified = "2009/04/06", Value = "Create your own layout by choosing \"No template\".")]
    public string CreateOwnLayout => this[nameof (CreateOwnLayout)];

    /// <summary>Create your own layout by choosing "No template".</summary>
    [ResourceEntry("TehereAreManyElements", Description = "There are many layout elements available that you can combine, resize or nest to achieve the desired result.", LastModified = "2009/04/06", Value = "There are many layout elements available that you can combine, resize or nest to achieve the desired result.")]
    public string TehereAreManyElements => this[nameof (TehereAreManyElements)];

    /// <summary>Create your own layout by choosing "No template".</summary>
    [ResourceEntry("YourOwnMasterFile", Description = " Or, use your own ASP.NET master page (.master file).", LastModified = "2009/04/06", Value = "Or, use your own ASP.NET master page (.master file).")]
    public string YourOwnMasterFile => this[nameof (YourOwnMasterFile)];

    /// <summary>Create your own layout by choosing "No template".</summary>
    [ResourceEntry("UploadMasterFileFTP", Description = " Upload your .master file using FTP to a folder of your choice on the server.", LastModified = "2009/04/06", Value = "Upload your .master file using FTP to a folder of your choice on the server.")]
    public string UploadMasterFileFTP => this[nameof (UploadMasterFileFTP)];

    /// <summary>Page Templates</summary>
    [ResourceEntry("PageTemplatesTitle", Description = "The title of the PageTemplates page.", LastModified = "2009/04/06", Value = "Page Templates")]
    public string PageTemplatesTitle => this[nameof (PageTemplatesTitle)];

    /// <summary>Page Templates</summary>
    [ResourceEntry("PageTemplatesHtmlTitle", Description = "The title of the PageTemplates page that is going to be displayed in the browser's titlebar.", LastModified = "2010/02/15", Value = "Page Templates")]
    public string PageTemplatesHtmlTitle => this[nameof (PageTemplatesHtmlTitle)];

    /// <summary>PageTemplates</summary>
    [ResourceEntry("PageTemplatesUrlName", Description = "The name that appears on the URL.", LastModified = "2009/04/06", Value = "PageTemplates")]
    public string PageTemplatesUrlName => this[nameof (PageTemplatesUrlName)];

    /// <summary>PageTemplates</summary>
    [ResourceEntry("PageTemplatesUrlNameNew", Description = "The name that appears on the URL.", LastModified = "2009/04/06", Value = "PageTemplatesNew")]
    public string PageTemplatesUrlNameNew => this[nameof (PageTemplatesUrlNameNew)];

    /// <summary>
    /// Provides user interface for creating and managing page templates.
    /// </summary>
    [ResourceEntry("PageTemplatesDescription", Description = "Description of Templates section.", LastModified = "2009/04/22", Value = "Provides user interface for creating and managing page templates.")]
    public string PageTemplatesDescription => this[nameof (PageTemplatesDescription)];

    /// <summary>BackendPages</summary>
    [ResourceEntry("BackendPagesUrlName", Description = "The name that appears in the URL.", LastModified = "2009/12/01", Value = "Pages")]
    public string BackendPagesUrlName => this[nameof (BackendPagesUrlName)];

    /// <summary>Backend Pages</summary>
    [ResourceEntry("BackendPagesTitle", Description = "The title of the BackendPages page.", LastModified = "2013/04/01", Value = "Backend pages")]
    public string BackendPagesTitle => this[nameof (BackendPagesTitle)];

    /// <summary>Backend Pages</summary>
    [ResourceEntry("BackendPagesHtmlTitle", Description = "The html title of the BackendPages page.", LastModified = "2010/02/15", Value = "Backend Pages")]
    public string BackendPagesHtmlTitle => this[nameof (BackendPagesHtmlTitle)];

    /// <summary>
    /// Provides user interface for creating and managing backend pages.
    /// </summary>
    [ResourceEntry("BackendPagesDescription", Description = "Description of Backend Pages section.", LastModified = "2009/12/01", Value = "Provides user interface for creating and managing backend pages.")]
    public string BackendPagesDescription => this[nameof (BackendPagesDescription)];

    /// <summary>BackendPagesWarning</summary>
    [ResourceEntry("BackendPagesWarningUrlName", Description = "The name that appears in the URL.", LastModified = "2010/07/21", Value = "Warning")]
    public string BackendPagesWarningUrlName => this[nameof (BackendPagesWarningUrlName)];

    /// <summary>Backend Pages - Warning</summary>
    [ResourceEntry("BackendPagesWarningTitle", Description = "The title of the BackendPages Warning page.", LastModified = "2010/07/21", Value = "Backend Pages - Warning")]
    public string BackendPagesWarningTitle => this[nameof (BackendPagesWarningTitle)];

    /// <summary>Backend Pages - Warning</summary>
    [ResourceEntry("BackendPagesWarningHtmlTitle", Description = "The html title of the BackendPages Warning page.", LastModified = "2010/02/15", Value = "Backend Pages - Warning")]
    public string BackendPagesWarningHtmlTitle => this[nameof (BackendPagesWarningHtmlTitle)];

    /// <summary>BackendTemplates</summary>
    [ResourceEntry("BackendTemplatesUrlName", Description = "The name that appears in the URL.", LastModified = "2009/12/07", Value = "BackendTemplates")]
    public string BackendTemplatesUrlName => this[nameof (BackendTemplatesUrlName)];

    /// <summary>Backend Templates</summary>
    [ResourceEntry("BackendTemplatesTitle", Description = "The title of the BackendTemplates page.", LastModified = "2009/12/07", Value = "Backend Templates")]
    public string BackendTemplatesTitle => this[nameof (BackendTemplatesTitle)];

    /// <summary>Backend Templates</summary>
    [ResourceEntry("BackendTemplatesHtmlTitle", Description = "The html title of the BackendTemplates page.", LastModified = "2010/02/15", Value = "Backend Templates")]
    public string BackendTemplatesHtmlTitle => this[nameof (BackendTemplatesHtmlTitle)];

    /// <summary>
    /// Provides user interface for creating and managing backend templates.
    /// </summary>
    [ResourceEntry("BackendTemplatesDescription", Description = "Description of Backend Templates section.", LastModified = "2009/12/07", Value = "Provides user interface for creating and managing backend templates.")]
    public string BackendTemplatesDescription => this[nameof (BackendTemplatesDescription)];

    /// <summary>ModulesAndServices</summary>
    [ResourceEntry("BackendModulesAndServicesUrlName", Description = "The name that appears in the URL.", LastModified = "2012/06/13", Value = "ModulesAndServices")]
    public string BackendModulesAndServicesUrlName => this[nameof (BackendModulesAndServicesUrlName)];

    [ResourceEntry("BackendModulesAndServicesTitle", Description = "The title of the Modules & Services page.", LastModified = "2013/03/29", Value = "Modules & Services")]
    public string BackendModulesAndServicesTitle => this[nameof (BackendModulesAndServicesTitle)];

    [ResourceEntry("BackendModulesAndServicesHtmlTitle", Description = "The html title of the Modules & Services page.", LastModified = "2012/06/13", Value = "Modules")]
    public string BackendModulesAndServicesHtmlTitle => this[nameof (BackendModulesAndServicesHtmlTitle)];

    /// <summary>Provides user interface for managing system modules.</summary>
    [ResourceEntry("BackendModulesAndServicesDescription", Description = "Description of Modules & Services section.", LastModified = "2012/06/13", Value = "Provides user interface for managing system modules.")]
    public string BackendModulesAndServicesDescription => this[nameof (BackendModulesAndServicesDescription)];

    /// <summary>Types of Content</summary>
    [ResourceEntry("ModulesNodeTitle", Description = "The title of the Modules taxon.", LastModified = "2009/04/06", Value = "Types of Content")]
    public string ModulesNodeTitle => this[nameof (ModulesNodeTitle)];

    /// <summary>Types</summary>
    [ResourceEntry("ModulesNodeUrlName", Description = "The name that appears in the URL.", LastModified = "2009/04/06", Value = "Types")]
    public string ModulesNodeUrlName => this[nameof (ModulesNodeUrlName)];

    /// <summary>Section for working with Sitefinity modules.</summary>
    [ResourceEntry("ModulesNodeDescription", Description = "Description of Modules section.", LastModified = "2009/04/22", Value = "Section for working with Sitefinity modules. ")]
    public string ModulesNodeDescription => this[nameof (ModulesNodeDescription)];

    /// <summary>The title of the single taxonomy navigation node.</summary>
    [ResourceEntry("TaxonomyNodeTitle", Description = "The title of the single taxonomy navigation node.", LastModified = "2010/03/02", Value = "Classification of Content")]
    public string TaxonomyNodeTitle => this[nameof (TaxonomyNodeTitle)];

    /// <summary>Classifications</summary>
    [ResourceEntry("TaxonomyNodeUrlName", Description = "The name of the node that appears in the URL when navigating through taxonomies.", LastModified = "2010/03/02", Value = "Taxonomy")]
    public string TaxonomyNodeUrlName => this[nameof (TaxonomyNodeUrlName)];

    /// <summary>Section for working with Sitefinity taxonomies.</summary>
    [ResourceEntry("TaxonomyNodeDescription", Description = "Description of Modules section.", LastModified = "2010/03/02", Value = "Section for working with specific Sitefinity taxonomy.")]
    public string TaxonomyNodeDescription => this[nameof (TaxonomyNodeDescription)];

    /// <summary>Title of the Discussions's group page.</summary>
    /// <value>Discussions</value>
    [ResourceEntry("DiscussionsGroupPageTitle", Description = "Title of the Discussions's group page.", LastModified = "2013/09/13", Value = "Discussions")]
    public string DiscussionsGroupPageTitle => this[nameof (DiscussionsGroupPageTitle)];

    /// <summary>Urlname of the Discussions's group page.</summary>
    /// <value>Discussions</value>
    [ResourceEntry("DiscussionsGroupPageUrlName", Description = "Urlname of the Discussions's group page.", LastModified = "2013/09/13", Value = "Discussions")]
    public string DiscussionsGroupPageUrlName => this[nameof (DiscussionsGroupPageUrlName)];

    /// <summary>Description of the Comments|Forums module group page.</summary>
    /// <value>This is the Comments|Forums module group page.</value>
    [ResourceEntry("DiscussionsGroupPageDescription", Description = "Description of the Comments|Forums module group page.", LastModified = "2013/09/13", Value = "This is the Comments|Forums module group page.")]
    public string DiscussionsGroupPageDescription => this[nameof (DiscussionsGroupPageDescription)];

    /// <summary>Word: Modules".</summary>
    [ResourceEntry("Modules", Description = "word : Modules", LastModified = "2009/04/06", Value = "Modules")]
    public string Modules => this[nameof (Modules)];

    /// <summary>Word: Marked Items".</summary>
    [ResourceEntry("MarkedItems", Description = "Menu name of the page in Sitefinity's backedn that displays marked items", LastModified = "2010/02/15", Value = "Marked Items")]
    public string MarkedItems => this[nameof (MarkedItems)];

    /// <summary>Word: Marked Items Description</summary>
    [ResourceEntry("MarkedItemsDescription", Description = "Describes the marked itesm description.", LastModified = "2010/02/15", Value = "Marked Items Description")]
    public string MarkedItemsDescription => this[nameof (MarkedItemsDescription)];

    /// <summary>Phrase: Marked Items</summary>
    [ResourceEntry("MarkedItemsHtmlTitle", Description = "Title of the page that displays marked items in Sitefinity's backend.", LastModified = "2010/02/15", Value = "Marked Items")]
    public string MarkedItemsHtmlTitle => this[nameof (MarkedItemsHtmlTitle)];

    /// <summary>Phrase: File manager".</summary>
    [ResourceEntry("Files", Description = "phrase : File manager", LastModified = "2013/04/01", Value = "File manager")]
    public string Files => this[nameof (Files)];

    /// <summary>FilesUrlName.</summary>
    [ResourceEntry("FilesUrlName", Description = "word : Files", LastModified = "2012/02/10", Value = "Files")]
    public string FilesUrlName => this[nameof (FilesUrlName)];

    /// <summary>Word: Files.</summary>
    [ResourceEntry("FilesHtmlTitle", Description = "Title of the page that manages files on the server in Sitefinity's backend.", LastModified = "2010/02/15", Value = "Files")]
    public string FilesHtmlTitle => this[nameof (FilesHtmlTitle)];

    /// <summary>Section for working with the file system.</summary>
    [ResourceEntry("FilesDescription", Description = "Description of Files section.", LastModified = "2009/04/22", Value = "Section for working with the file system. ")]
    public string FilesDescription => this[nameof (FilesDescription)];

    /// <summary>Word: Administration".</summary>
    [ResourceEntry("Admin", Description = "word : Administration", LastModified = "2009/04/06", Value = "Administration")]
    public string Admin => this[nameof (Admin)];

    /// <summary>Word: Administration".</summary>
    [ResourceEntry("AdminTitle", Description = "The title of the Adminisration section.", LastModified = "2009/05/07", Value = "Administration")]
    public string AdminTitle => this[nameof (AdminTitle)];

    /// <summary>
    /// Section for Sitefinity administration such as user management, permissions, system services and etc.
    /// </summary>
    [ResourceEntry("AdminDescription", Description = "Description of Administration section.", LastModified = "2009/04/22", Value = "Section for Sitefinity administration such as user management, permissions, system services and etc.")]
    public string AdminDescription => this[nameof (AdminDescription)];

    /// <summary>
    /// Section which contains Marketing features of Sitefinity CMS
    /// </summary>
    [ResourceEntry("MarketingTitle", Description = "Title of the Marketing section", LastModified = "2012/07/14", Value = "Marketing")]
    public string MarketingTitle => this[nameof (MarketingTitle)];

    /// <summary>Url name of the Marketing section</summary>
    [ResourceEntry("MarketingUrlName", Description = "Url name of the Marketing section", LastModified = "2012/07/14", Value = "marketing")]
    public string MarketingUrlName => this[nameof (MarketingUrlName)];

    /// <summary>Description of the Marketing section.</summary>
    /// <value>Section for Sitefinity marketing features, such as personalization, analytics, email campaigns, etc.</value>
    [ResourceEntry("MarketingDescription", Description = "Description of the Marketing section", LastModified = "2014/03/05", Value = "Section for Sitefinity marketing features, such as personalization, analytics, email campaigns, etc.")]
    public string MarketingDescription => this[nameof (MarketingDescription)];

    /// <summary>Word: Administration".</summary>
    [ResourceEntry("SitefinityTitle", Description = "The title of the Sitefinity section.", LastModified = "2009/05/07", Value = "Sitefinity")]
    public string SitefinityTitle => this[nameof (SitefinityTitle)];

    /// <summary>Word: Content</summary>
    [ResourceEntry("Content", Description = "Word: Content", LastModified = "2009/05/07", Value = "Content")]
    public string Content => this[nameof (Content)];

    /// <summary>Content</summary>
    [ResourceEntry("ContentNodeTitle", Description = "The title of the Content section.", LastModified = "2009/05/07", Value = "Content")]
    public string ContentNodeTitle => this[nameof (ContentNodeTitle)];

    /// <summary>Content</summary>
    [ResourceEntry("ContentNodeUrlName", Description = "The name of the Content taxon that appears on the URL.", LastModified = "2009/05/07", Value = "Content")]
    public string ContentNodeUrlName => this[nameof (ContentNodeUrlName)];

    /// <summary>
    /// A section for managing Sitefinity application, modules, content and taxonomies.
    /// </summary>
    [ResourceEntry("ContentNodeDescription", Description = "The description of the Content taxon.", LastModified = "2009/05/07", Value = "A section for managing Sitefinity application, modules, content and taxonomies.")]
    public string ContentNodeDescription => this[nameof (ContentNodeDescription)];

    /// <summary>Word: Design</summary>
    [ResourceEntry("Design", Description = "Word: Design", LastModified = "2009/05/07", Value = "Design")]
    public string Design => this[nameof (Design)];

    /// <summary>Design</summary>
    [ResourceEntry("DesignNodeTitle", Description = "The title of the Design section.", LastModified = "2009/05/07", Value = "Design")]
    public string DesignNodeTitle => this[nameof (DesignNodeTitle)];

    /// <summary>Design</summary>
    [ResourceEntry("DesignNodeUrlName", Description = "The name of the Design node that appears in the URL.", LastModified = "2009/05/07", Value = "Design")]
    public string DesignNodeUrlName => this[nameof (DesignNodeUrlName)];

    /// <summary>A section for managing Templates and Themes.</summary>
    [ResourceEntry("DesignNodeDescription", Description = "The description of the Design section.", LastModified = "2009/05/07", Value = "A section for managing Templates and Themes.")]
    public string DesignNodeDescription => this[nameof (DesignNodeDescription)];

    /// <summary>Templates</summary>
    [ResourceEntry("TemplatesNodeTitle", Description = "The title of the Templates section.", LastModified = "2009/05/07", Value = "Templates")]
    public string TemplatesNodeTitle => this[nameof (TemplatesNodeTitle)];

    /// <summary>
    /// The name of the Templates node that appears in the URL.
    /// </summary>
    [ResourceEntry("TemplatesNodeUrlName", Description = "The name of the Templates node that appears in the URL.", LastModified = "2009/05/07", Value = "Templates")]
    public string TemplatesNodeUrlName => this[nameof (TemplatesNodeUrlName)];

    /// <summary>A section for managing Templates.</summary>
    [ResourceEntry("TemplatesNodeDescription", Description = "The description of the Templates section.", LastModified = "2009/05/07", Value = "A section for managing Templates.")]
    public string TemplatesNodeDescription => this[nameof (TemplatesNodeDescription)];

    /// <summary>Themes</summary>
    [ResourceEntry("ThemesNodeTitle", Description = "The title of the Themes section.", LastModified = "2009/05/07", Value = "Themes")]
    public string ThemesNodeTitle => this[nameof (ThemesNodeTitle)];

    /// <summary>Themes</summary>
    [ResourceEntry("ThemesNodeUrlName", Description = "The name of the Themes node that appears in the URL.", LastModified = "2009/05/07", Value = "Themes")]
    public string ThemesNodeUrlName => this[nameof (ThemesNodeUrlName)];

    /// <summary>A section for managing Templates and Themes.</summary>
    [ResourceEntry("ThemesNodeDescription", Description = "The description of the Design section.", LastModified = "2009/05/07", Value = "A section for managing Themes.")]
    public string ThemesNodeDescription => this[nameof (ThemesNodeDescription)];

    /// <summary>Settings</summary>
    [ResourceEntry("SettingsNodeTitle", Description = "The title of the Settings section.", LastModified = "2009/05/07", Value = "Advanced Settings")]
    public string SettingsNodeTitle => this[nameof (SettingsNodeTitle)];

    /// <summary>Settings</summary>
    [ResourceEntry("SettingsNodeUrlName", Description = "The name of the Settings node that appears in the URL.", LastModified = "2009/05/07", Value = "Advanced")]
    public string SettingsNodeUrlName => this[nameof (SettingsNodeUrlName)];

    /// <summary>
    /// A section for User Management, System Services, System Settings, Custom Tools and etc.
    /// </summary>
    [ResourceEntry("SettingsNodeDescription", Description = "The description of the Settings section.", LastModified = "2009/05/07", Value = "A section for User Management, System Services, System Settings, Custom Tools and etc.")]
    public string SettingsNodeDescription => this[nameof (SettingsNodeDescription)];

    /// <summary>Help</summary>
    [ResourceEntry("HelpNodeTitle", Description = "The title of the Help site section.", LastModified = "2009/05/07", Value = "Help")]
    public string HelpNodeTitle => this[nameof (HelpNodeTitle)];

    /// <summary>Help</summary>
    [ResourceEntry("HelpNodeUrlName", Description = "The name of the Help node that appears in the URL.", LastModified = "2009/05/07", Value = "Help")]
    public string HelpNodeUrlName => this[nameof (HelpNodeUrlName)];

    /// <summary>A site section help information.</summary>
    [ResourceEntry("HelpNodeDescription", Description = "The description of the Help section.", LastModified = "2009/05/07", Value = "A site section help information.")]
    public string HelpNodeDescription => this[nameof (HelpNodeDescription)];

    /// <summary>User Management</summary>
    [ResourceEntry("UserManagementNodeTitle", Description = "The title of the UserManagement section.", LastModified = "2009/05/07", Value = "User Management")]
    public string UserManagementNodeTitle => this[nameof (UserManagementNodeTitle)];

    /// <summary>UserManagement</summary>
    [ResourceEntry("UserManagementNodeUrlName", Description = "The name of the UserManagement node that appears in the URL.", LastModified = "2009/05/07", Value = "UserManagement")]
    public string UserManagementNodeUrlName => this[nameof (UserManagementNodeUrlName)];

    /// <summary>
    /// Groups user management tools such as users, roles, permissions and etc.
    /// </summary>
    [ResourceEntry("UserManagementNodeDescription", Description = "The description of the UserManagement section.", LastModified = "2009/05/07", Value = "Groups user management tools such as users, roles, permissions and etc.")]
    public string UserManagementNodeDescription => this[nameof (UserManagementNodeDescription)];

    [ResourceEntry("SettingsAndConfigurationsNodeTitle", Description = "The title of the SettingsAndConfigurations section.", LastModified = "2010/09/29", Value = "Settings & Configurations")]
    public string SettingsAndConfigurationsNodeTitle => this[nameof (SettingsAndConfigurationsNodeTitle)];

    /// <summary>SettingsAndConfigurations</summary>
    [ResourceEntry("SettingsAndConfigurationsNodeUrlName", Description = "The name of the SettingsAndConfigurations node that appears in the URL.", LastModified = "2009/05/07", Value = "SettingsAndConfigurations")]
    public string SettingsAndConfigurationsNodeUrlName => this[nameof (SettingsAndConfigurationsNodeUrlName)];

    [ResourceEntry("SettingsAndConfigurationsNodeDescription", Description = "The description of the SettingsAndConfigurations section.", LastModified = "2009/05/07", Value = "Groups settings & configurations")]
    public string SettingsAndConfigurationsNodeDescription => this[nameof (SettingsAndConfigurationsNodeDescription)];

    /// <summary>Services</summary>
    [ResourceEntry("ServicesNodeTitle", Description = "The title of the Services section.", LastModified = "2009/05/07", Value = "Services")]
    public string ServicesNodeTitle => this[nameof (ServicesNodeTitle)];

    /// <summary>Services</summary>
    [ResourceEntry("ServicesNodeUrlName", Description = "The name of the Services node that appears in the URL.", LastModified = "2009/05/07", Value = "Services")]
    public string ServicesNodeUrlName => this[nameof (ServicesNodeUrlName)];

    /// <summary>Groups system service modules.</summary>
    [ResourceEntry("ServicesNodeDescription", Description = "The description of the Services section.", LastModified = "2009/05/07", Value = "Groups system service modules.")]
    public string ServicesNodeDescription => this[nameof (ServicesNodeDescription)];

    /// <summary>System</summary>
    [ResourceEntry("SystemNodeTitle", Description = "The title of the System section.", LastModified = "2009/05/07", Value = "System")]
    public string SystemNodeTitle => this[nameof (SystemNodeTitle)];

    /// <summary>System</summary>
    [ResourceEntry("SystemNodeUrlName", Description = "The name of the System node that appears in the URL.", LastModified = "2009/05/07", Value = "System")]
    public string SystemNodeUrlName => this[nameof (SystemNodeUrlName)];

    /// <summary>
    /// Groups system tools such as Configuration Manager, Resource Manager, File System Explorer and etc.
    /// </summary>
    [ResourceEntry("SystemNodeDescription", Description = "The description of the System section.", LastModified = "2009/05/07", Value = "Groups system tools such as Configuration Manager, Resource Manager, File System Explorer and etc.")]
    public string SystemNodeDescription => this[nameof (SystemNodeDescription)];

    /// <summary>System</summary>
    [ResourceEntry("BackendPagesNodeTitle", Description = "The title of the System section.", LastModified = "2009/05/07", Value = "Backend Pages")]
    public string BackendPagesNodeTitle => this["SystemNodeTitle"];

    /// <summary>System</summary>
    [ResourceEntry("BackendPagesNodeUrlName", Description = "The name of the System node that appears in the URL.", LastModified = "2009/05/07", Value = "BackendPages")]
    public string BackendPagesNodeUrlName => this["SystemNodeUrlName"];

    /// <summary>
    /// Groups system tools such as Configuration Manager, Resource Manager, File System Explorer and etc.
    /// </summary>
    [ResourceEntry("BackendPagesNodeDescription", Description = "The description of the System section.", LastModified = "2009/05/07", Value = "Groups system tools such as Configuration Manager, Resource Manager, File System Explorer and etc.")]
    public string BackendPagesNodeDescription => this["SystemNodeDescription"];

    /// <summary>Tools</summary>
    [ResourceEntry("ToolsNodeTitle", Description = "The title of the Tools section.", LastModified = "2009/05/07", Value = "Tools")]
    public string ToolsNodeTitle => this[nameof (ToolsNodeTitle)];

    /// <summary>Tools</summary>
    [ResourceEntry("ToolsNodeUrlName", Description = "The name of the Tools node that appears in the URL.", LastModified = "2009/05/07", Value = "Tools")]
    public string ToolsNodeUrlName => this[nameof (ToolsNodeUrlName)];

    /// <summary>Groups custom Sitefinity tools.</summary>
    [ResourceEntry("ToolsNodeDescription", Description = "The description of the Tools section.", LastModified = "2009/05/07", Value = "Groups custom Sitefinity tools.")]
    public string ToolsNodeDescription => this[nameof (ToolsNodeDescription)];

    /// <summary>phrase: Title for the Site Sync group page.</summary>
    [ResourceEntry("StagingSyncTitle", Description = "Title for the Site Sync group page.", LastModified = "2016/09/13", Value = "Site Sync")]
    public string StagingSyncTitle => this[nameof (StagingSyncTitle)];

    /// <summary>phrase: Urlname of the Site Sync group page.</summary>
    [ResourceEntry("StagingSyncUrlName", Description = "Urlname of the Site Sync group page.", LastModified = "2016/09/13", Value = "Site Sync")]
    public string StagingSyncUrlName => this[nameof (StagingSyncUrlName)];

    /// <summary>
    /// phrase: This is the page group that contains all pages for Site Sync.
    /// </summary>
    /// <value>Synchronization Page Group Description</value>
    [ResourceEntry("StagingSyncDescription", Description = "Site Sync Page Group Description", LastModified = "2016/09/13", Value = "This is the page group that contains all pages for Site Sync.")]
    public string StagingSyncDescription => this[nameof (StagingSyncDescription)];

    /// <summary>Site section for Sitefinity Backend.</summary>
    [ResourceEntry("SitefinityDescription", Description = "Description of the Sitefinity section.", LastModified = "2012/01/05", Value = "Site section for Sitefinity Backend.")]
    public string SitefinityDescription => this[nameof (SitefinityDescription)];

    /// <summary>Word: Live.</summary>
    [ResourceEntry("Live", Description = "word : Live", LastModified = "2009/04/06", Value = "Live")]
    public string Live => this[nameof (Live)];

    /// <summary>View published pages and content.</summary>
    [ResourceEntry("LiveDescription", Description = "Description of Live section.", LastModified = "2009/04/22", Value = "View published pages and content.")]
    public string LiveDescription => this[nameof (LiveDescription)];

    /// <summary>Phrase: Skip to the main content.</summary>
    [ResourceEntry("SkipToTheMainContent", Description = "phrase : Skip to the main content", LastModified = "2009/04/06", Value = "Skip To The Main Content")]
    public string SkipToTheMainContent => this[nameof (SkipToTheMainContent)];

    /// <summary>Phrase: My preferences.</summary>
    [ResourceEntry("MyPreferences", Description = "phrase : My Preferences", LastModified = "2009/04/06", Value = "My Preferences")]
    public string MyPreferences => this[nameof (MyPreferences)];

    /// <summary>Phrase: My profile.</summary>
    [ResourceEntry("MyProfile", Description = "phrase : My Profile", LastModified = "2009/04/06", Value = "My Profile")]
    public string MyProfile => this[nameof (MyProfile)];

    /// <summary>word: Preferences.</summary>
    [ResourceEntry("Preferences", Description = "word : Preferences", LastModified = "2009/04/06", Value = "Preferences")]
    public string Preferences => this[nameof (Preferences)];

    /// <summary>word: Profile.</summary>
    [ResourceEntry("Profile", Description = "word : Profile", LastModified = "2009/04/06", Value = "Profile")]
    public string Profile => this[nameof (Profile)];

    /// <summary>word: Log Off.</summary>
    [ResourceEntry("LogOff", Description = "word : Log Off", LastModified = "2009/04/06", Value = "Log Off")]
    public string LogOff => this[nameof (LogOff)];

    /// <summary>word: Users.</summary>
    [ResourceEntry("Users", Description = "The name of the Sitefinity/Admin/Users section.", LastModified = "2009/04/26", Value = "Users")]
    public string Users => this[nameof (Users)];

    /// <summary>word: Users.</summary>
    [ResourceEntry("UsersHtmlTitle", Description = "Title of the page in Sitefinity backend that dispays users.", LastModified = "2010/02/15", Value = "Users")]
    public string UsersHtmlTitle => this[nameof (UsersHtmlTitle)];

    /// <summary>word: Roles.</summary>
    [ResourceEntry("Roles", Description = "The name of the Sitefinity/Admin/Roles section.", LastModified = "2009/07/01", Value = "Roles")]
    public string Roles => this[nameof (Roles)];

    /// <summary>word: Roles.</summary>
    [ResourceEntry("RolesHtmltitle", Description = "Name of the page in Sitefinity's backend that displays user roles.", LastModified = "2010/02/15", Value = "Roles")]
    public string RolesHtmltitle => this[nameof (RolesHtmltitle)];

    /// <summary>
    /// Provides user interface for managing Sitefinity Roles.
    /// </summary>
    [ResourceEntry("RolesDescription", Description = "Description of Sitefinity/Admin/Roles section.", LastModified = "2009/07/01", Value = "Provides user interface for managing Sitefinity Roles.")]
    public string RolesDescription => this[nameof (RolesDescription)];

    /// <summary>
    /// Provides user interface for managing Sitefinity Users and Roles.
    /// </summary>
    [ResourceEntry("UsersDescription", Description = "Description of Sitefinity/Admin/Users section.", LastModified = "2009/04/26", Value = "Provides user interface for managing Sitefinity Users and Roles.")]
    public string UsersDescription => this[nameof (UsersDescription)];

    /// <summary>word: Users.</summary>
    [ResourceEntry("Permissions", Description = "The name of the Sitefinity/Admin/Permissions section.", LastModified = "2009/04/26", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>word: Users.</summary>
    [ResourceEntry("PermissionsHtmlTitle", Description = "Name of the page in sitefinity's backend that manages permissions.", LastModified = "2010/02/15", Value = "Permissions")]
    public string PermissionsHtmlTitle => this[nameof (PermissionsHtmlTitle)];

    /// <summary>word: Users.</summary>
    [ResourceEntry("PermissionsTitle", Description = "The title of Permissions command panel in Configuration control panel.", LastModified = "2009/04/26", Value = "Permissions")]
    public string PermissionsTitle => this[nameof (PermissionsTitle)];

    /// <summary>
    /// Provides user interface for managing security permissions.
    /// </summary>
    [ResourceEntry("PermissionsDescription", Description = "Description of Sitefinity/Admin/Permissions section.", LastModified = "2009/04/26", Value = "Provides user interface for managing security permissions.")]
    public string PermissionsDescription => this[nameof (PermissionsDescription)];

    /// <summary>word: Tools.</summary>
    [ResourceEntry("Tools", Description = "The name of the Sitefinity/Admin/Tolls section.", LastModified = "2009/04/22", Value = "Tools")]
    public string Tools => this[nameof (Tools)];

    /// <summary>
    /// Provides access to Sitefinity tools such as Site Template Export.
    /// </summary>
    [ResourceEntry("ToolsDescription", Description = "Description of Sitefinity/Admin/Tolls section.", LastModified = "2009/04/22", Value = "Provides access to Sitefinity tools such as Site Template Export.")]
    public string ToolsDescription => this[nameof (ToolsDescription)];

    /// <summary>word: Services.</summary>
    [ResourceEntry("Services", Description = "The name of the Sitefinity/Admin/Services section.", LastModified = "2009/04/22", Value = "Services")]
    public string Services => this[nameof (Services)];

    /// <summary>
    /// Provides access to Sitefinity system services such as Feeds and Search.
    /// </summary>
    [ResourceEntry("ServicesDescription", Description = "Description of Sitefinity/Admin/Services section.", LastModified = "2009/04/22", Value = "Provides access to Sitefinity system services such as Feeds and Search.")]
    public string ServicesDescription => this[nameof (ServicesDescription)];

    /// <summary>
    /// The name of the Sitefinity/Admin/Configuration section.
    /// </summary>
    [ResourceEntry("Configuration", Description = "The name of the Sitefinity/Admin/Configuration section.", LastModified = "2009/04/22", Value = "Settings")]
    public string Configuration => this[nameof (Configuration)];

    /// <summary>
    /// Provides user interface for managing Sitefinity configurations and personalization.
    /// </summary>
    [ResourceEntry("ConfigurationDescription", Description = "Description of Sitefinity/Admin/Configuration section.", LastModified = "2009/04/22", Value = "Provides user interface for managing Sitefinity configurations and personalization.")]
    public string ConfigurationDescription => this[nameof (ConfigurationDescription)];

    /// <summary>word: Configurations</summary>
    [ResourceEntry("Configurations", Description = "The name of the Sitefinity/Setting/Configurations section.", LastModified = "2009/04/22", Value = "Configurations")]
    public string Configurations => this[nameof (Configurations)];

    /// <summary>word: Configurations</summary>
    [ResourceEntry("ConfigurationsHtmlTitle", Description = "Title of the page in Sitefinity's backend that manages configurations.", LastModified = "2010/02/15", Value = "Configurations")]
    public string ConfigurationsHtmlTitle => this[nameof (ConfigurationsHtmlTitle)];

    /// <summary>
    /// Provides user interface for managing Sitefinity configurations and personalization.
    /// </summary>
    [ResourceEntry("ConfigurationsDescription", Description = "Description of Sitefinity/Setting/Configurations section.", LastModified = "2009/04/22", Value = "Provides user interface for managing Sitefinity configurations and personalization.")]
    public string ConfigurationsDescription => this[nameof (ConfigurationsDescription)];

    /// <summary>word: Localization.</summary>
    [ResourceEntry("Localization", Description = "The name of the Sitefinity/Admin/Localization section.", LastModified = "2013/03/29", Value = "Labels & Messages")]
    public string Localization => this[nameof (Localization)];

    /// <summary>word: Localization.</summary>
    [ResourceEntry("LocalizationHtmlTitle", Description = "Title of the page in Sitefinity's backend that manages the translation of labels.", LastModified = "2010/02/15", Value = "Localization")]
    public string LocalizationHtmlTitle => this[nameof (LocalizationHtmlTitle)];

    /// <summary>
    /// Provides user interface for managing and localizing texts used in Sitefinity such as labels, buttons, warnings, error messages and etc.
    /// </summary>
    [ResourceEntry("LocalizationDescription", Description = "Description of Sitefinity/Admin/Localization section.", LastModified = "2009/04/22", Value = "Provides user interface for managing and localizing texts used in Sitefinity such as labels, buttons, warnings, error messages and etc.")]
    public string LocalizationDescription => this[nameof (LocalizationDescription)];

    /// <summary>phrase: All classifications.</summary>
    [ResourceEntry("AllClassifications", Description = "The name of the Sitefinity/Content/Classifications section.", LastModified = "2010/02/03", Value = "All classifications")]
    public string AllClassifications => this[nameof (AllClassifications)];

    /// <summary>word: Classifications.</summary>
    [ResourceEntry("Classifications", Description = "The name of the Sitefinity/Content/Classifications section.", LastModified = "2010/02/03", Value = "Classifications")]
    public string Classifications => this[nameof (Classifications)];

    /// <summary>word: Classifications.</summary>
    [ResourceEntry("ClassificationsHtmlTitle", Description = "The html title of the Sitefinity/Content/Classifications section.", LastModified = "2010/02/15", Value = "Classifications")]
    public string ClassificationsHtmlTitle => this[nameof (ClassificationsHtmlTitle)];

    /// <summary>
    /// Provides user interface for managing content classifications (Taxonomies).
    /// </summary>
    [ResourceEntry("ClassificationsDescription", Description = "Description of Sitefinity/Content/Classifications section.", LastModified = "2010/02/03", Value = "Provides user interface for managing content classifications (Taxonomies).")]
    public string ClassificationsDescription => this[nameof (ClassificationsDescription)];

    /// <summary>
    /// Provides navigation information about Sitefinity Backend (administration).
    /// </summary>
    [ResourceEntry("BackendSiteMapProviderDescription", Description = "Description of backend site map provider.", LastModified = "2012/01/05", Value = "Provides navigation information about Sitefinity Backend (administration).")]
    public string BackendSiteMapProviderDescription => this[nameof (BackendSiteMapProviderDescription)];

    /// <summary>name: Site Map.</summary>
    [ResourceEntry("SiteMap", Description = "The name of the Sitefinity/Admin/SiteMap section.", LastModified = "2009/04/24", Value = "Site Map")]
    public string SiteMap => this[nameof (SiteMap)];

    /// <summary>
    /// Provides user interface for managing and editing Sitefinity pages.
    /// </summary>
    [ResourceEntry("SiteMapDescription", Description = "Description of Sitefinity/Admin/SiteMap section.", LastModified = "2009/04/24", Value = "Provides user interface for managing and editing Sitefinity pages.")]
    public string SiteMapDescription => this[nameof (SiteMapDescription)];

    /// <summary>
    /// Gets or sets the template defining the layout of the control.
    /// </summary>
    [ResourceEntry("LayoutTemplateDescription", Description = "Description of LayoutTemplate property.", LastModified = "2009/04/26", Value = "Gets or sets the template defining the layout of the control.")]
    public string LayoutTemplateDescription => this[nameof (LayoutTemplateDescription)];

    /// <summary>Description of QuestionTemplate property.</summary>
    /// <value>Description of QuestionTemplate property.</value>
    [ResourceEntry("QuestionTemplateDescription", Description = "Description of QuestionTemplate property", LastModified = "2009/05/26", Value = "Gets the template defining the Question layout of Password Recovery control.")]
    public string QuestionTemplateDescription => this[nameof (QuestionTemplateDescription)];

    /// <summary>Description of UserNameTemplate property.</summary>
    /// <value>Description of UserNameTemplate property.</value>
    [ResourceEntry("UserNameTemplateDescription", Description = "Description of UserNameTemplate property", LastModified = "2009/05/26", Value = "Gets the template defining the UserName layout of Password Recovery control.")]
    public string UserNameTemplateDescription => this[nameof (UserNameTemplateDescription)];

    /// <summary>Description of SuccessTemplate property.</summary>
    /// <value>Description of SuccessTemplate property.</value>
    [ResourceEntry("SuccessTemplateDescription", Description = "Description of SuccessTemplate property", LastModified = "2009/05/26", Value = "Gets the template defining the Success layout of Password Recovery control.")]
    public string SuccessTemplateDescription => this[nameof (SuccessTemplateDescription)];

    /// <summary>Description of ChangePasswordTemplate property.</summary>
    /// <value>Description of ChangePasswordTemplate property.</value>
    [ResourceEntry("ChangePasswordTemplateDescription", Description = "Description of ChangePasswordTemplate property", LastModified = "2009/05/28", Value = "Gets the template defining the ChangePassword layout of ChangePassword control.")]
    public string ChangePasswordTemplateDescription => this[nameof (ChangePasswordTemplateDescription)];

    /// <summary>Description of SuccessTemplate property.</summary>
    /// <value>Description of SuccessTemplate property.</value>
    [ResourceEntry("SuccessTemplateChangePasswordDescription", Description = "Description of SuccessTemplate property", LastModified = "2009/05/28", Value = "Gets the template defining the Success layout of ChangePassword control.")]
    public string SuccessTemplateChangePasswordDescription => this[nameof (SuccessTemplateChangePasswordDescription)];

    /// <summary>Description of CreateUserTemplate property.</summary>
    /// <value>Description of CreateUserTemplate property.</value>
    [ResourceEntry("CreateUserTemplateDescription", Description = "Description of CreateUserTemplate property.", LastModified = "2009/05/28", Value = "Gets the template defining the CreateUser layout of CreateUserWizardForm control.")]
    public string CreateUserTemplateDescription => this[nameof (CreateUserTemplateDescription)];

    /// <summary>Description of CompleteTemplate property.</summary>
    /// <value>Description of CompleteTemplate property.</value>
    [ResourceEntry("CompleteTemplateDescription", Description = "Description of CompleteTemplate property", LastModified = "2009/05/28", Value = "Gets the template defining the Complete layout of CreateUserWizardForm control.")]
    public string CompleteTemplateDescription => this[nameof (CompleteTemplateDescription)];

    /// <summary>Label:Text for the Create User button.</summary>
    /// <value>Text for the Create User button.</value>
    [ResourceEntry("UsersListCreateUser", Description = "Text for the Create User button.", LastModified = "2010/08/19", Value = "Create a user")]
    public string UsersListCreateUser => this[nameof (UsersListCreateUser)];

    /// <summary>Pages</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of the Pages module.", LastModified = "2009/10/06", Value = "Pages")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Create new...</summary>
    [ResourceEntry("CreateNewText", Description = "The text of the Create New toolbar dropdown.", LastModified = "2009/10/13", Value = "Create new...")]
    public string CreateNewText => this[nameof (CreateNewText)];

    /// <summary>Page</summary>
    [ResourceEntry("PageCommandName", Description = "The name of Page command.", LastModified = "2009/11/05", Value = "Page")]
    public string PageCommandName => this[nameof (PageCommandName)];

    /// <summary>You can define content and layout from scratch</summary>
    [ResourceEntry("PageDescription", Description = "The description of Page toolbar button.", LastModified = "2009/10/13", Value = "Create a page from scratch, set its layout and content.")]
    public string PageDescription => this[nameof (PageDescription)];

    /// <summary>Section</summary>
    [ResourceEntry("SectionCommandName", Description = "The name of Section command.", LastModified = "2009/11/05", Value = "Section")]
    public string SectionCommandName => this[nameof (SectionCommandName)];

    /// <summary>A container for other pages</summary>
    [ResourceEntry("SectionDescription", Description = "The description of Section toolbar button.", LastModified = "2009/10/13", Value = "Add a container for pages.")]
    public string SectionDescription => this[nameof (SectionDescription)];

    /// <summary>Blog</summary>
    [ResourceEntry("BlogCommandName", Description = "The name of Blog command.", LastModified = "2009/11/05", Value = "Blog")]
    public string BlogCommandName => this[nameof (BlogCommandName)];

    /// <summary>
    /// Configured pages as a standard blog, containing: List of posts, Single post, Archive, About the author
    /// </summary>
    [ResourceEntry("BlogDescription", Description = "The description of the Blog toolbar button.", LastModified = "2009/10/13", Value = "Create a set of pages that make up a standard blog.")]
    public string BlogDescription => this[nameof (BlogDescription)];

    /// <summary>Photo Gallery</summary>
    [ResourceEntry("PhotoGalleryCommandName", Description = "The name of Photo Gallery command.", LastModified = "2009/11/05", Value = "Photo Gallery")]
    public string PhotoGalleryCommandName => this[nameof (PhotoGalleryCommandName)];

    /// <summary>Configured pages for standard photo gallery</summary>
    [ResourceEntry("PhotoGalleryDescription", Description = "The description of Photo Gallery toolbar button.", LastModified = "2009/10/13", Value = "Create a set of pages that make up a standard photo gallery.")]
    public string PhotoGalleryDescription => this[nameof (PhotoGalleryDescription)];

    /// <summary>
    /// A base for other pages. You can save as templete either page types you create
    /// </summary>
    [ResourceEntry("TemplateDescription", Description = "The description of Template toolbar button.", LastModified = "2009/10/13", Value = "Design a template which contains layout and content elements to be used across many pages.")]
    public string TemplateDescription => this[nameof (TemplateDescription)];

    /// <summary>Save Draft</summary>
    [ResourceEntry("SaveDraft", Description = "The text of the Save Draft toolbar button.", LastModified = "2009/10/13", Value = "Save Draft")]
    public string SaveDraft => this[nameof (SaveDraft)];

    /// <summary>Save Draft</summary>
    [ResourceEntry("Preview", Description = "The text of the Preview toolbar button.", LastModified = "2009/10/13", Value = "Preview")]
    public string Preview => this[nameof (Preview)];

    /// <summary>Delete</summary>
    [ResourceEntry("DeleteText", Description = "The text of Delete toolbar button.", LastModified = "2009/10/13", Value = "Delete")]
    public string DeleteText => this[nameof (DeleteText)];

    /// <summary>More actions...</summary>
    [ResourceEntry("MoreActionsText", Description = "The text of More Actions toolbar dropdown.", LastModified = "2009/10/13", Value = "More actions...")]
    public string MoreActionsText => this[nameof (MoreActionsText)];

    /// <summary>Search pages...</summary>
    [ResourceEntry("SearchPagesText", Description = "The text of Search Pages toolbar button.", LastModified = "2009/10/13", Value = "Search pages...")]
    public string SearchPagesText => this[nameof (SearchPagesText)];

    /// <summary>Edit Pages</summary>
    [ResourceEntry("EditPages", Description = "The text of Edit Pages toolbar button.", LastModified = "2010/07/26", Value = "Edit Pages")]
    public string EditPages => this[nameof (EditPages)];

    /// <summary>Edit Templates</summary>
    [ResourceEntry("EditTemplates", Description = "The text of Edit Templates toolbar button.", LastModified = "2009/11/18", Value = "Edit Templates")]
    public string EditTemplates => this[nameof (EditTemplates)];

    /// <summary>Create new Template</summary>
    [ResourceEntry("CreateNewTemplate", Description = "The text of Create new Template toolbar button.", LastModified = "2010/07/27", Value = "Create a template")]
    public string CreateNewTemplate => this[nameof (CreateNewTemplate)];

    /// <summary>View</summary>
    [ResourceEntry("ViewCommandName", Description = "The name of View command.", LastModified = "2009/10/13", Value = "View")]
    public string ViewCommandName => this[nameof (ViewCommandName)];

    /// <summary>Content</summary>
    [ResourceEntry("ContentCommandName", Description = "The name of Content command.", LastModified = "2009/11/05", Value = "Content")]
    public string ContentCommandName => this[nameof (ContentCommandName)];

    [SuppressMessage("Microsoft.Globalization", "CA1304:SpecifyCultureInfo", MessageId = "Telerik.Sitefinity.Localization.Resource.get_Item(System.String)")]
    [ResourceEntry("TemplateCommandName", Description = "The name of Template command.", LastModified = "2009/11/05", Value = "Template")]
    public string TemplateCommandName => this[nameof (TemplateCommandName)];

    /// <summary>The name of Template command.</summary>
    [ResourceEntry("TemplateNACommandName", Description = "The name of Template command.", LastModified = "2010/04/21", Value = "Template <em>N/A</em>")]
    public string TemplateNACommandName => this[nameof (TemplateNACommandName)];

    /// <summary>Parent Template</summary>
    [ResourceEntry("ParentTemplateCommandName", Description = "The name of Parent Template command.", LastModified = "2009/11/17", Value = "Base Template")]
    public string ParentTemplateCommandName => this[nameof (ParentTemplateCommandName)];

    /// <summary>Properties</summary>
    [ResourceEntry("PropertiesCommandName", Description = "The name of Properties command.", LastModified = "2009/11/05", Value = "Properties")]
    public string PropertiesCommandName => this[nameof (PropertiesCommandName)];

    /// <summary>
    /// Properties <em>N/A</em>
    /// </summary>
    [ResourceEntry("PropertiesNACommandName", Description = "The name of Properties NA command.", LastModified = "2010/04/20", Value = "Properties <em>N/A</em>")]
    public string PropertiesNACommandName => this[nameof (PropertiesNACommandName)];

    /// <summary>Delete</summary>
    [ResourceEntry("DeleteCommandName", Description = "The name of Delete command.", LastModified = "2009/10/13", Value = "Delete")]
    public string DeleteCommandName => this[nameof (DeleteCommandName)];

    /// <summary>Delete</summary>
    [ResourceEntry("DeleteNACommandName", Description = "The name of Delete command.", LastModified = "2010/04/20", Value = "Delete <em>N/A</em>")]
    public string DeleteNACommandName => this[nameof (DeleteNACommandName)];

    /// <summary>Publish</summary>
    [ResourceEntry("PublishCommandName", Description = "The name of Publish command.", LastModified = "2009/11/05", Value = "Publish")]
    public string PublishCommandName => this[nameof (PublishCommandName)];

    /// <summary>
    /// Publish <em>N/A</em>
    /// </summary>
    [ResourceEntry("PublishCommandNameNotReady", Description = "The name of Publish command.", LastModified = "2009/12/17", Value = "Publish <em>N/A</em>")]
    public string PublishCommandNameNotReady => this[nameof (PublishCommandNameNotReady)];

    /// <summary>Duplicate</summary>
    [ResourceEntry("DuplicateCommandName", Description = "The name of Duplicate command.", LastModified = "2009/11/05", Value = "Duplicate <em>N/A</em>")]
    public string DuplicateCommandName => this[nameof (DuplicateCommandName)];

    /// <summary>Set as Homepage</summary>
    [ResourceEntry("SetHomepageCommandName", Description = "The name of Set as Homepage command.", LastModified = "2009/11/05", Value = "Set as Homepage")]
    public string SetHomepageCommandName => this[nameof (SetHomepageCommandName)];

    /// <summary>Reorder pages in this group</summary>
    [ResourceEntry("ReorderPagesCommandName", Description = "The name of Reorder pages in this group command.", LastModified = "2009/11/05", Value = "Reorder pages <em>N/A</em>")]
    public string ReorderPagesCommandName => this[nameof (ReorderPagesCommandName)];

    /// <summary>Titles, Descriptions, Keywords</summary>
    [ResourceEntry("PagePropertiesCommandName", Description = "The name of Titles, Descriptions, Keywords command.", LastModified = "2009/11/05", Value = "<strong>Batch edit... </strong>Title, Description, Keywords<em>N/A</em>")]
    public string PagePropertiesCommandName => this[nameof (PagePropertiesCommandName)];

    /// <summary>
    /// Permissions <em>N/A</em>
    /// </summary>
    [ResourceEntry("PermissionsCommandName", Description = "The name of Permissions command.", LastModified = "2010/03/11", Value = "Permissions")]
    public string PermissionsCommandName => this[nameof (PermissionsCommandName)];

    /// <summary>Review History</summary>
    [ResourceEntry("ReviewHistoryCommandName", Description = "The name of Review History command.", LastModified = "2009/10/13", Value = "Review History")]
    public string ReviewHistoryCommandName => this[nameof (ReviewHistoryCommandName)];

    /// <summary>Owner</summary>
    [ResourceEntry("OwnerCommandName", Description = "The name of Owner command.", LastModified = "2009/11/05", Value = "Owner <em>N/A</em>")]
    public string OwnerCommandName => this[nameof (OwnerCommandName)];

    /// <summary>
    /// Section <em>N/A</em>
    /// </summary>
    [ResourceEntry("ParentCommandName", Description = "The name of Section command.", LastModified = "2009/12/17", Value = "Section <em>N/A</em>")]
    public string ParentCommandName => this[nameof (ParentCommandName)];

    /// <summary>
    /// Parent Section <em>N/A</em>
    /// </summary>
    [ResourceEntry("ParentSectionCommandName", Description = "The name of Parent Section command.", LastModified = "2009/11/24", Value = "Parent Section <em>N/A</em>")]
    public string ParentSectionCommandName => this[nameof (ParentSectionCommandName)];

    /// <summary>Title</summary>
    [ResourceEntry("TitleColumnText", Description = "The text of Title column in the page browser grid.", LastModified = "2009/10/13", Value = "Title")]
    public string TitleColumnText => this[nameof (TitleColumnText)];

    /// <summary>Template</summary>
    [ResourceEntry("TemplateGridColumnHeaderText", Description = "The text of Template column in the page browser grid.", LastModified = "2009/10/14", Value = "Template")]
    public string TemplateGridColumnHeaderText => this[nameof (TemplateGridColumnHeaderText)];

    /// <summary>Author</summary>
    [ResourceEntry("AuthorGridColumnHeaderText", Description = "The text of Author column in the page browser grid.", LastModified = "2009/10/14", Value = "Author")]
    public string AuthorGridColumnHeaderText => this[nameof (AuthorGridColumnHeaderText)];

    /// <summary>Based on</summary>
    [ResourceEntry("BasedOnGridColumnHeaderText", Description = "The text of Based on column in the template browser grid.", LastModified = "2009/11/17", Value = "Based on")]
    public string BasedOnGridColumnHeaderText => this[nameof (BasedOnGridColumnHeaderText)];

    /// <summary>Category</summary>
    [ResourceEntry("CategoryGridColumnHeaderText", Description = "The text of Category column in the template browser grid.", LastModified = "2009/11/17", Value = "Category")]
    public string CategoryGridColumnHeaderText => this[nameof (CategoryGridColumnHeaderText)];

    /// <summary>Date</summary>
    [ResourceEntry("DateGridColumnHeaderText", Description = "The text of Date column in the page browser grid.", LastModified = "2009/10/14", Value = "Date")]
    public string DateGridColumnHeaderText => this[nameof (DateGridColumnHeaderText)];

    /// <summary>Section of {0} pages and {1} sections</summary>
    [ResourceEntry("FullSectionInfo", Description = "The section information displayed in the page browser grid.", LastModified = "2009/10/14", Value = "Section of {0} pages and {1} sections")]
    public string FullSectionInfo => this[nameof (FullSectionInfo)];

    /// <summary>Section of {0} pages</summary>
    [ResourceEntry("SectionInfo", Description = "The section information displayed in the page browser grid.", LastModified = "2009/11/05", Value = "Section of {0} pages")]
    public string SectionInfo => this[nameof (SectionInfo)];

    /// <summary>{0} pages</summary>
    [ResourceEntry("PagesCount", Description = "The page count displayed in the template browser grid.", LastModified = "2009/11/17", Value = "{0} pages")]
    public string PagesCount => this[nameof (PagesCount)];

    /// <summary>1 page</summary>
    [ResourceEntry("PageCount", Description = "The page count displayed in the template browser grid.", LastModified = "2012/11/14", Value = "1 page")]
    public string PageCount => this[nameof (PageCount)];

    /// <summary>Misc.</summary>
    [ResourceEntry("MiscCategory", Description = "The category assigned to uncategorized properties.", LastModified = "2012/01/05", Value = "Misc.")]
    public string MiscCategory => this[nameof (MiscCategory)];

    /// <summary>Actions</summary>
    [ResourceEntry("ActionsLinkText", Description = "The text of the Actions link in the page browser grid.", LastModified = "2009/10/16", Value = "Actions")]
    public string ActionsLinkText => this[nameof (ActionsLinkText)];

    /// <summary>Back to Templates</summary>
    [ResourceEntry("BackToTemplates", Description = "The text of the back to templates link in page editor", LastModified = "2009/10/16", Value = "Back to Templates")]
    public string BackToTemplates => this[nameof (BackToTemplates)];

    /// <summary>Back to Pages</summary>
    [ResourceEntry("BackToPages", Description = "The text of the back to pages link in page editor", LastModified = "2009/10/16", Value = "Back to Pages")]
    public string BackToPages => this[nameof (BackToPages)];

    /// <summary>Back to Pages</summary>
    [ResourceEntry("BackToItems", Description = "The text of the back to pages link in page editor", LastModified = "2010/10/13", Value = "Back to Pages")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>Back to Edit</summary>
    [ResourceEntry("BackToEdit", Description = "The text of the back to edit link when adding new language to which must be appended the title of the page", LastModified = "2011/01/12", Value = "Back to")]
    public string BackToEdit => this[nameof (BackToEdit)];

    /// <summary>phrase: The page {0} has been created!</summary>
    [ResourceEntry("PageCreatedMsg", Description = "phrase", LastModified = "2009/11/11", Value = "The page {0} has been created!")]
    public string PageCreatedMsg => this[nameof (PageCreatedMsg)];

    /// <summary>phrase: The page {0} has been successfully saved!</summary>
    [ResourceEntry("PageSavedMsg", Description = "phrase", LastModified = "2009/11/11", Value = "The page {0} has been successfully saved!")]
    public string PageSavedMsg => this[nameof (PageSavedMsg)];

    /// <summary>phrase: The template {0} has been successfully saved!</summary>
    [ResourceEntry("TemplateSavedMsg", Description = "phrase", LastModified = "2009/11/18", Value = "The template {0} has been successfully saved!")]
    public string TemplateSavedMsg => this[nameof (TemplateSavedMsg)];

    /// <summary>phrase: The section {0} has been successfully saved!</summary>
    [ResourceEntry("SectionSavedMsg", Description = "phrase", LastModified = "2009/11/12", Value = "The section {0} has been successfully saved!")]
    public string SectionSavedMsg => this[nameof (SectionSavedMsg)];

    /// <summary>phrase: The section {0} has been created!</summary>
    [ResourceEntry("SectionCreatedMsg", Description = "phrase", LastModified = "2009/11/11", Value = "The section {0} has been created!")]
    public string SectionCreatedMsg => this[nameof (SectionCreatedMsg)];

    /// <summary>word: Name</summary>
    [ResourceEntry("Name", Description = "word", LastModified = "2009/11/17", Value = "Name")]
    public string Name => this[nameof (Name)];

    /// <summary>phrase: Type page name</summary>
    [ResourceEntry("TitlePlaceholder", Description = "Placehold for page title's placeholder.", LastModified = "2019/3/6", Value = "Type page name")]
    public string TitlePlaceholder => this[nameof (TitlePlaceholder)];

    /// <summary>phrase: All Templates</summary>
    [ResourceEntry("AllTemplates", Description = "phrase", LastModified = "2009/11/20", Value = "All Templates")]
    public string AllTemplates => this[nameof (AllTemplates)];

    /// <summary>phrase: All templates</summary>
    [ResourceEntry("AllTemplatesPascalCase", Description = "phrase", LastModified = "2021/01/29", Value = "All templates")]
    public string AllTemplatesPascalCase => this[nameof (AllTemplatesPascalCase)];

    /// <summary>word: Generic</summary>
    [ResourceEntry("Generic", Description = "word: Generic", LastModified = "2009/11/20", Value = "Generic")]
    public string Generic => this[nameof (Generic)];

    /// <summary>Content blocks</summary>
    [ResourceEntry("ContentBlocksTitle", Description = "The title of the Content Blocks module landing page.", LastModified = "2011/02/01", Value = "Content blocks")]
    public string ContentBlocksTitle => this[nameof (ContentBlocksTitle)];

    /// <summary>Content blocks</summary>
    [ResourceEntry("ContentBlocksHtmlTitle", Description = "The html title of the Content Blocks module landing page.", LastModified = "2011/02/01", Value = "Content blocks")]
    public string ContentBlocksHtmlTitle => this[nameof (ContentBlocksHtmlTitle)];

    /// <summary>Content blocks</summary>
    [ResourceEntry("ContentBlocksUrlName", Description = "The name that appears in the URL when navigating to ContentBlocks control panel.", LastModified = "2011/02/01", Value = "Content blocks")]
    public string ContentBlocksUrlName => this[nameof (ContentBlocksUrlName)];

    /// <summary>
    /// phrase: Represents a landing page for Sitefinity Content Blocks module.
    /// </summary>
    [ResourceEntry("ContentBlocksDescription", Description = "The description of the Content Blocks module landing page.", LastModified = "2012/01/05", Value = "Represents a landing page for Sitefinity Content Blocks module.")]
    public string ContentBlocksDescription => this[nameof (ContentBlocksDescription)];

    /// <summary>Message: Rating</summary>
    [ResourceEntry("RatingTitle", Description = "Title of the Rating control in the control toolbox", LastModified = "2009/11/23", Value = "Rating")]
    public string RatingTitle => this[nameof (RatingTitle)];

    /// <summary>Message: Allows users to rate content items.</summary>
    [ResourceEntry("RatingDescription", Description = "Describes the Rating control in the control toolbox.", LastModified = "2009/11/23", Value = "Allows users to rate content items.")]
    public string RatingDescription => this[nameof (RatingDescription)];

    /// <summary>Control name: Content Block</summary>
    [ResourceEntry("ContentBlock", Description = "Control name: ContentBlock", LastModified = "2009/11/24", Value = "ContentBlock")]
    public string ContentBlock => this[nameof (ContentBlock)];

    /// <summary>Control name: Content Block</summary>
    [ResourceEntry("ContentBlockTitle", Description = "Control title: Content Block", LastModified = "2009/11/24", Value = "Content block")]
    public string ContentBlockTitle => this[nameof (ContentBlockTitle)];

    /// <summary>
    /// Control description: Control description:Control for inserting arbitrary content on the page
    /// </summary>
    [ResourceEntry("ContentBlockDescription", Description = "Control description:Control for inserting arbitrary content on the page", LastModified = "2009/11/24", Value = "Text with rich formatting (bold, italic, bullets, etc.). Images, videos, tables can be inserted, too")]
    public string ContentBlockDescription => this[nameof (ContentBlockDescription)];

    /// <summary>Control name: Archive</summary>
    [ResourceEntry("ArchiveControlTitle", Description = "Control title: Archive", LastModified = "2010/03/02", Value = "Archive")]
    public string ArchiveControlTitle => this[nameof (ArchiveControlTitle)];

    /// <summary>
    /// Control description: A control for displaying archive list of content items of specified content type.
    /// </summary>
    [ResourceEntry("ArchiveControlDescription", Description = "Control description: A control for displaying archive list of content items of specified content type.", LastModified = "2010/03/02", Value = "Listed items of a selected content type")]
    public string ArchiveControlDescription => this[nameof (ArchiveControlDescription)];

    /// <summary>Control name: MediaPlayer</summary>
    [ResourceEntry("MediaPlayerControlTitle", Description = "Control title: MediaPlayerControlTitle", LastModified = "2010/05/21", Value = "Video")]
    public string MediaPlayerControlTitle => this[nameof (MediaPlayerControlTitle)];

    /// <summary>
    /// Control description: A control for displaying rich media content.
    /// </summary>
    [ResourceEntry("MediaPlayerControlDescription", Description = "Control description: A control for displaying rich media content", LastModified = "2010/05/21", Value = "Video uploaded from your computer or selected from Video libraries")]
    public string MediaPlayerControlDescription => this[nameof (MediaPlayerControlDescription)];

    /// <summary>Control name: Document link</summary>
    [ResourceEntry("DocumentLinkControlTitle", Description = "Control title: Document link", LastModified = "2010/06/17", Value = "Document link")]
    public string DocumentLinkControlTitle => this[nameof (DocumentLinkControlTitle)];

    /// <summary>
    /// Control description: A control for displaying a link to download a document.
    /// </summary>
    [ResourceEntry("DocumentLinkControlDescription", Description = "Control description: A control for displaying a link to download a document.", LastModified = "2010/06/17", Value = "Link for downloading a selected document of other file")]
    public string DocumentLinkControlDescription => this[nameof (DocumentLinkControlDescription)];

    /// <summary>Control name: DownloadList</summary>
    [ResourceEntry("DownloadListViewTitle", Description = "Control title: DownloadListViewTitle", LastModified = "2010/07/26", Value = "Download list")]
    public string DownloadListViewTitle => this[nameof (DownloadListViewTitle)];

    /// <summary>
    /// Control description: A control for displaying list with items to download.
    /// </summary>
    [ResourceEntry("DownloadListViewDescription", Description = "Control description: A control for displaying list with items to download in master or detail views.", LastModified = "2010/05/21", Value = "Documents or other files for download")]
    public string DownloadListViewDescription => this[nameof (DownloadListViewDescription)];

    /// <summary>Control name: FeedEmbedControl</summary>
    [ResourceEntry("FeedEmbedControlTitle", Description = "Control title: FeedEmbedControlTitle", LastModified = "2010/07/21", Value = "Feed")]
    public string FeedEmbedControlTitle => this[nameof (FeedEmbedControlTitle)];

    /// <summary>Control description: A link to a published feed.</summary>
    [ResourceEntry("FeedEmbedControlDescription", Description = "Control description: A link to a published feed.", LastModified = "2011/03/11", Value = "Link to a feed (RSS, Twitter, custom)")]
    public string FeedEmbedControlDescription => this[nameof (FeedEmbedControlDescription)];

    /// <summary>Control name: Image</summary>
    [ResourceEntry("ImageControlTitle", Description = "Control title: Image", LastModified = "2010/04/09", Value = "Image")]
    public string ImageControlTitle => this[nameof (ImageControlTitle)];

    /// <summary>
    /// Control description: A control for displaying uploded image with specified properties at properties dialog.
    /// </summary>
    [ResourceEntry("ImageControlDescription", Description = "Control description: A control for displaying uploded image with specified properties at properties dialog.", LastModified = "2010/04/09", Value = "Image uploaded from your computer or selected from image libraries")]
    public string ImageControlDescription => this[nameof (ImageControlDescription)];

    /// <summary>Control name: Image</summary>
    [ResourceEntry("ScriptsAndStylesControlsSectionTitle", Description = "Control title: Scripts and Styles", LastModified = "2010/05/17", Value = "Scripts and Styles")]
    public string ScriptsAndStylesControlsSectionTitle => this[nameof (ScriptsAndStylesControlsSectionTitle)];

    /// <summary>Control name: Taxonomy</summary>
    [ResourceEntry("TaxonomyControlTitle", Description = "Control title: Taxonomy", LastModified = "2010/03/02", Value = "Classification")]
    public string TaxonomyControlTitle => this[nameof (TaxonomyControlTitle)];

    /// <summary>
    /// Control description: A control for displaying list of taxa of specified content type or for the specified content item
    /// </summary>
    [ResourceEntry("TaxonomyControlDescription", Description = "Control description: A control for displaying list of taxa of specified content type or for the specified content item.", LastModified = "2010/03/02", Value = "A control for displaying list of taxa of specified content type or for the specified content item.")]
    public string TaxonomyControlDescription => this[nameof (TaxonomyControlDescription)];

    /// <summary>Save as Template</summary>
    [ResourceEntry("SaveAsTemplCommandName", Description = "The name of Save as Template command.", LastModified = "2009/11/24", Value = "Save as Template <em>N/A</em>")]
    public string SaveAsTemplCommandName => this[nameof (SaveAsTemplCommandName)];

    /// <summary>word: Edit...</summary>
    [ResourceEntry("Edit", Description = "word", LastModified = "2009/11/24", Value = "Edit...")]
    public string Edit => this[nameof (Edit)];

    /// <summary>phrase: Select another...</summary>
    [ResourceEntry("SelectAnother", Description = "phrase", LastModified = "2009/11/24", Value = "Select another...")]
    public string SelectAnother => this[nameof (SelectAnother)];

    /// <summary>Message: This page was successfully set as Homepage.</summary>
    /// <value>Message: This page was successfully set as Homepage.</value>
    [ResourceEntry("HomepageSuccessfullySetMessage", Description = "Message shown to the user when he have set the current page as a homepage.", LastModified = "2010/04/20", Value = "This page was successfully set as Homepage.")]
    public string HomepageSuccessfullySetMessage => this[nameof (HomepageSuccessfullySetMessage)];

    /// <summary>Message: The draft is successfully saved</summary>
    [ResourceEntry("DraftSuccessfullySaved", Description = "positive message after saving the darft", LastModified = "2012/01/05", Value = "The draft is successfully saved")]
    public string DraftSuccessfullySaved => this[nameof (DraftSuccessfullySaved)];

    /// <summary>Message: Page was successfully published.</summary>
    /// <value>Message: Page was successfully published.</value>
    [ResourceEntry("PageSuccessfullyUnpublished", Description = "positive message after unpublishing a page.", LastModified = "2010/07/30", Value = "The page was successfully unpublished.")]
    public string PageSuccessfullyUnpublished => this[nameof (PageSuccessfullyUnpublished)];

    /// <summary>Message: Setting this page as homepage failed.</summary>
    /// <value>Message: Setting this page as homepage failed.</value>
    [ResourceEntry("HomepageSettingFailedMessage", Description = "Message shown to the user when setting this page as homepage failed.", LastModified = "2010/04/20", Value = "Setting this page as homepage failed.")]
    public string HomepageSettingFailedMessage => this[nameof (HomepageSettingFailedMessage)];

    /// <summary>Phrase: set as homepage</summary>
    [ResourceEntry("SetAsHomepage", Description = "phrase: Set as Homepage", LastModified = "2010/04/27", Value = "Set as Homepage")]
    public string SetAsHomepage => this[nameof (SetAsHomepage)];

    /// <summary>Phrase: set homepage</summary>
    [ResourceEntry("SetHomepage", Description = "phrase: Set homepage", LastModified = "2019/04/01", Value = "Set homepage")]
    public string SetHomepage => this[nameof (SetHomepage)];

    /// <summary>word: Personalize</summary>
    [ResourceEntry("Personalize", Description = "word: Personalize", LastModified = "2012/08/06", Value = "Personalize")]
    public string Personalize => this[nameof (Personalize)];

    /// <summary>Phrase: share link</summary>
    [ResourceEntry("ShareLink", Description = "phrase: Share preview link...", LastModified = "2014/01/22", Value = "Share preview link...")]
    public string ShareLink => this[nameof (ShareLink)];

    /// <summary>phrase: Personalize this page</summary>
    [ResourceEntry("PersonalizeThisPage", Description = "phrase: Personalize this page", LastModified = "2012/08/06", Value = "Personalize this page")]
    public string PersonalizeThisPage => this[nameof (PersonalizeThisPage)];

    /// <summary>
    /// Message: Do you want to set the current page as homepage?
    /// </summary>
    /// <value>Message: Do you want to set the current page as homepage?</value>
    [ResourceEntry("SetAsHomepagePrompt", Description = "Message shown to the user as a prompt before setigna a page as homepage.", LastModified = "2010/04/20", Value = "Do you want to set the current page as homepage?")]
    public string SetAsHomepagePrompt => this[nameof (SetAsHomepagePrompt)];

    /// <summary>phrase: Done Creating Pages</summary>
    [ResourceEntry("DoneCreatingPages", Description = "phrase", LastModified = "2009/12/10", Value = "Done Creating Pages")]
    public string DoneCreatingPages => this[nameof (DoneCreatingPages)];

    /// <summary>phrase: Done Creating Sections</summary>
    [ResourceEntry("DoneCreatingSections", Description = "phrase", LastModified = "2009/12/10", Value = "Done Creating Sections")]
    public string DoneCreatingSections => this[nameof (DoneCreatingSections)];

    /// <summary>phrase: Templates by Category</summary>
    [ResourceEntry("TemplatesByCategory", Description = "phrase", LastModified = "2009/12/11", Value = "Templates by category")]
    public string TemplatesByCategory => this[nameof (TemplatesByCategory)];

    /// <summary>phrase: Duplicated Name!</summary>
    [ResourceEntry("DuplicatedName", Description = "phrase", LastModified = "2014/04/04", Value = "Template with the same name already exists. Please use a different name.")]
    public string DuplicatedName => this[nameof (DuplicatedName)];

    /// <summary>
    /// phrase: You cannot change the framework when duplicating a template!
    /// </summary>
    [ResourceEntry("DuplicatedFrameworkIsDifferent", Description = "phrase", LastModified = "2014/03/28", Value = "You cannot change the framework when duplicating a template!")]
    public string DuplicatedFrameworkIsDifferent => this[nameof (DuplicatedFrameworkIsDifferent)];

    /// <summary>phrase: Duplicated URL!</summary>
    [ResourceEntry("DuplicatedUrl", Description = "phrase", LastModified = "2009/12/15", Value = "Duplicated URL!")]
    public string DuplicatedUrl => this[nameof (DuplicatedUrl)];

    /// <summary>phrase: Invalid parent!</summary>
    [ResourceEntry("InvalidParent", Description = "phrase", LastModified = "2009/12/15", Value = "Invalid parent!")]
    public string InvalidParent => this[nameof (InvalidParent)];

    /// <summary>phrase: You need to select a master page!</summary>
    [ResourceEntry("NeedToSelectMaster", Description = "phrase", LastModified = "2009/12/18", Value = "You need to select a master page!")]
    public string NeedToSelectMaster => this[nameof (NeedToSelectMaster)];

    /// <summary>
    /// Cyclic child/parent relationship detected between one or many items.
    /// </summary>
    /// <value>Error message. Shown when the control has a cyclic child/parent relationship.</value>
    [ResourceEntry("CyclicChildParentRelationship", Description = "Error message. Shown when the control has a cyclic child/parent relationship.", LastModified = "2020/03/11", Value = "Cyclic child/parent relationship detected between one or many items.")]
    public string CyclicChildParentRelationship => this[nameof (CyclicChildParentRelationship)];

    /// <summary>Cannot find sibling.</summary>
    /// <value>Error message. Shown when a control's parent cannot be found.</value>
    [ResourceEntry("CannotFindSiblig", Description = "Error message. Shown when a control's parent cannot be found.", LastModified = "2010/01/27", Value = "Cannot find sibling.")]
    public string CannotFindSiblig => this[nameof (CannotFindSiblig)];

    /// <summary>Placeholders of adjacent controls do not match.</summary>
    /// <value>Error message. Shown when a control and its parent are not in the same placeholder.</value>
    [ResourceEntry("AdjacentControlPlaceholdersDoNotMatch", Description = "Error message. Shown when a control and its parent are not in the same placeholder.", LastModified = "2010/01/27", Value = "Placeholders of adjacent controls do not match.")]
    public string AdjacentControlPlaceholdersDoNotMatch => this[nameof (AdjacentControlPlaceholdersDoNotMatch)];

    /// <summary>Navigation controls</summary>
    [ResourceEntry("NavigationControlsSectionTitle", Description = "Navigation controls", LastModified = "2010/02/02", Value = "Navigation")]
    public string NavigationControlsSectionTitle => this[nameof (NavigationControlsSectionTitle)];

    /// <summary>Navigation Controls</summary>
    [ResourceEntry("NavigationControlsSectionDescription", Description = "Navigation Controls", LastModified = "2011/10/27", Value = "Group of links displayed in different ways – horizontal, vertical, dropdown menu, tree, etc.")]
    public string NavigationControlsSectionDescription => this[nameof (NavigationControlsSectionDescription)];

    /// <summary>Title of the Breadcrumb widget</summary>
    [ResourceEntry("BreadcrumbTitle", Description = "Title of the Breadcrumb widget", LastModified = "2012/02/15", Value = "Breadcrumb")]
    public string BreadcrumbTitle => this[nameof (BreadcrumbTitle)];

    /// <summary>
    /// The description of the Breadcrumb widget in the navigation toolbox.
    /// </summary>
    [ResourceEntry("BreadcrumbDescription", Description = "The description of the Breadcrumb widget in the navigation toolbox.", LastModified = "2012/02/15", Value = "Navigation widget which displays the location of the current page.")]
    public string BreadcrumbDescription => this[nameof (BreadcrumbDescription)];

    /// <summary>SiteMap Control</summary>
    [ResourceEntry("SiteMapControlTitle", Description = "SiteMap Control", LastModified = "2010/02/02", Value = "Sitemap")]
    public string SiteMapControlTitle => this[nameof (SiteMapControlTitle)];

    /// <summary>SiteMap Control</summary>
    [ResourceEntry("SiteMapControlDescription", Description = "SiteMap Control", LastModified = "2010/02/02", Value = "SiteMap Control")]
    public string SiteMapControlDescription => this[nameof (SiteMapControlDescription)];

    /// <summary>phrase: SiteMenu Control</summary>
    [ResourceEntry("SiteMenuTitle", Description = "SiteMenu Control", LastModified = "2010/02/02", Value = "Menu")]
    public string SiteMenuTitle => this[nameof (SiteMenuTitle)];

    /// <summary>word: SiteMenu Control</summary>
    [ResourceEntry("SiteMenuDescription", Description = "SiteMenu Control", LastModified = "2010/02/02", Value = "SiteMenu Control")]
    public string SiteMenuDescription => this[nameof (SiteMenuDescription)];

    /// <summary>SiteTree Control</summary>
    [ResourceEntry("SiteTreeTitle", Description = "SiteTree Control", LastModified = "2010/02/02", Value = "Tree")]
    public string SiteTreeTitle => this[nameof (SiteTreeTitle)];

    /// <summary>SiteTree Control</summary>
    [ResourceEntry("SiteTreeDescription", Description = "SiteTree Control", LastModified = "2010/02/02", Value = "SiteTree Control")]
    public string SiteTreeDescription => this[nameof (SiteTreeDescription)];

    /// <summary>
    /// Message: ALL PAGES and TEMPLATES that use this template WILL BE DELETED. Do you want to proceed?
    /// </summary>
    /// <value>Message show to the user as a prompt before deleting a page template.</value>
    [ResourceEntry("DeletePageTemplateConfirm", Description = "Message show to the user as a propmt before deleting a page template.", LastModified = "2010/02/02", Value = "All pages and templates that use this template will be deleted. Do you want to proceed? ")]
    public string DeletePageTemplateConfirm => this[nameof (DeletePageTemplateConfirm)];

    /// <summary>Message: Pages</summary>
    /// <value>Title of the pages root taxonomy.</value>
    [ResourceEntry("PagesTaxonomyTitle", Description = "Title of the pages root taxonomy.", LastModified = "2010/02/15", Value = "Pages")]
    public string PagesTaxonomyTitle => this[nameof (PagesTaxonomyTitle)];

    /// <summary>Contents</summary>
    [ResourceEntry("HelpContentsUrlName", Description = "The name if Help Contents page that appears in the URL.", LastModified = "2009/12/07", Value = "Contents")]
    public string HelpContentsUrlName => this[nameof (HelpContentsUrlName)];

    /// <summary>Contents</summary>
    [ResourceEntry("HelpContentsTitle", Description = "The title of the Help Contents page.", LastModified = "2009/12/07", Value = "Contents")]
    public string HelpContentsTitle => this[nameof (HelpContentsTitle)];

    /// <summary>Sitefinity Documentation</summary>
    [ResourceEntry("HelpContentsHtmlTitle", Description = "The HTML title of the Help Contents page.", LastModified = "2010/02/15", Value = "Sitefinity Documentation")]
    public string HelpContentsHtmlTitle => this[nameof (HelpContentsHtmlTitle)];

    /// <summary>Provides Sitefinity system documentation.</summary>
    [ResourceEntry("HelpContentsDescription", Description = "Description of Help Contents page.", LastModified = "2009/12/07", Value = "Provides Sitefinity system documentation.")]
    public string HelpContentsDescription => this[nameof (HelpContentsDescription)];

    /// <summary>Resources</summary>
    [ResourceEntry("OnlineResourcesUrlName", Description = "The name if Online Resources page that appears in the URL.", LastModified = "2009/12/07", Value = "Resources")]
    public string OnlineResourcesUrlName => this[nameof (OnlineResourcesUrlName)];

    /// <summary>Online Resources</summary>
    [ResourceEntry("OnlineResourcesTitle", Description = "The title of the Online Resources page.", LastModified = "2009/12/07", Value = "Online Resources")]
    public string OnlineResourcesTitle => this[nameof (OnlineResourcesTitle)];

    /// <summary>Sitefinity Online Resources</summary>
    [ResourceEntry("OnlineResourcesHtmlTitle", Description = "The HTML title of the Online Resources page.", LastModified = "2010/02/15", Value = "Sitefinity Online Resources")]
    public string OnlineResourcesHtmlTitle => this[nameof (OnlineResourcesHtmlTitle)];

    /// <summary>
    /// Provides Online help resource links such as forums, knowledge base, videos and etc.
    /// </summary>
    [ResourceEntry("OnlineResourcesDescription", Description = "Description of Help Contents page.", LastModified = "2009/12/07", Value = "Provides Online help resource links such as forums, knowledge base, videos and etc.")]
    public string OnlineResourcesDescription => this[nameof (OnlineResourcesDescription)];

    /// <summary>Support</summary>
    [ResourceEntry("TechnicalSupportUrlName", Description = "The name if Technical Support page that appears in the URL.", LastModified = "2009/12/07", Value = "Support")]
    public string TechnicalSupportUrlName => this[nameof (TechnicalSupportUrlName)];

    /// <summary>Technical Support</summary>
    [ResourceEntry("TechnicalSupportTitle", Description = "The title of the Technical Support page.", LastModified = "2009/12/07", Value = "Technical Support")]
    public string TechnicalSupportTitle => this[nameof (TechnicalSupportTitle)];

    /// <summary>Sitefinity Technical Support</summary>
    [ResourceEntry("TechnicalSupportHtmlTitle", Description = "The HTML title of the Technical Support page.", LastModified = "2010/02/15", Value = "Sitefinity Technical Support")]
    public string TechnicalSupportHtmlTitle => this[nameof (TechnicalSupportHtmlTitle)];

    /// <summary>
    /// Provides forms for submitting technical enquiries, and issue tracking.
    /// </summary>
    [ResourceEntry("TechnicalSupportDescription", Description = "Description of Technical Support page.", LastModified = "2009/12/07", Value = "Provides forms for submitting technical enquiries, and issue tracking.")]
    public string TechnicalSupportDescription => this[nameof (TechnicalSupportDescription)];

    /// <summary>Feedback</summary>
    [ResourceEntry("CustomerFeedbackUrlName", Description = "The name if Customer Feedback page that appears in the URL.", LastModified = "2009/12/07", Value = "Feedback")]
    public string CustomerFeedbackUrlName => this[nameof (CustomerFeedbackUrlName)];

    /// <summary>Customer Feedback</summary>
    [ResourceEntry("CustomerFeedbackTitle", Description = "The title of the Customer Feedback page.", LastModified = "2009/12/07", Value = "Customer Feedback")]
    public string CustomerFeedbackTitle => this[nameof (CustomerFeedbackTitle)];

    /// <summary>Sitefinity Customer Feedback</summary>
    [ResourceEntry("CustomerFeedbackHtmlTitle", Description = "The HTML title of the Customer Feedback page.", LastModified = "2010/02/15", Value = "Sitefinity Customer Feedback")]
    public string CustomerFeedbackHtmlTitle => this[nameof (CustomerFeedbackHtmlTitle)];

    /// <summary>
    /// Provides forms for submitting feedback and feature requests.
    /// </summary>
    [ResourceEntry("CustomerFeedbackDescription", Description = "Description of Customer Feedback page.", LastModified = "2009/12/07", Value = "Provides forms for submitting feedback and feature requests.")]
    public string CustomerFeedbackDescription => this[nameof (CustomerFeedbackDescription)];

    /// <summary>About</summary>
    [ResourceEntry("VersionAndLicensingUrlName", Description = "The name if About Sitefinity page that appears in the URL.", LastModified = "2009/12/07", Value = "VersionAndLicensing")]
    public string VersionAndLicensingUrlName => this[nameof (VersionAndLicensingUrlName)];

    /// <summary>About Sitefinity</summary>
    [ResourceEntry("VersionAndLicensingTitle", Description = "The title of the Version And Licensing Sitefinity page.", LastModified = "2009/12/07", Value = "Version & Licensing")]
    public string VersionAndLicensingTitle => this[nameof (VersionAndLicensingTitle)];

    /// <summary>About Sitefinity</summary>
    [ResourceEntry("VersionAndLicensingHtmlTitle", Description = "The HTML title of the Version And Licensing Sitefinity page.", LastModified = "2010/02/15", Value = "Version And Licensing")]
    public string VersionAndLicensingHtmlTitle => this[nameof (VersionAndLicensingHtmlTitle)];

    /// <summary>Provides system and license information.</summary>
    [ResourceEntry("VersionAndLicensingDescription", Description = "Description of Customer Feedback page.", LastModified = "2009/12/07", Value = "Provides system and license information.")]
    public string VersionAndLicensingDescription => this[nameof (VersionAndLicensingDescription)];

    /// <summary>User's profile page.</summary>
    [ResourceEntry("ProfileUrlName", Description = "The name of the Profile page that appears in the URL.", LastModified = "2010/09/14", Value = "Profile")]
    public string ProfileUrlName => this[nameof (ProfileUrlName)];

    /// <summary>User's profile page.</summary>
    [ResourceEntry("ProfileTitle", Description = "The title of the Profile Sitefinity page.", LastModified = "2010/09/14", Value = "Profile")]
    public string ProfileTitle => this[nameof (ProfileTitle)];

    /// <summary>User's profile page.</summary>
    [ResourceEntry("ProfileHtmlTitle", Description = "The HTML title of the Profile Sitefinity page.", LastModified = "2010/09/14", Value = "Profile")]
    public string ProfileHtmlTitle => this[nameof (ProfileHtmlTitle)];

    /// <summary>User's profile page.</summary>
    [ResourceEntry("ProfileDescription", Description = "Description of Profile page.", LastModified = "2010/09/14", Value = "User's profile information and editing.")]
    public string ProfileDescription => this[nameof (ProfileDescription)];

    /// <summary>phrase: Create a page</summary>
    [ResourceEntry("CreateAPage", Description = "phrase: Create a page", LastModified = "2017/10/12", Value = "How to create a page")]
    public string CreateAPage => this[nameof (CreateAPage)];

    /// <summary>Gets External Link: Create a page</summary>
    [ResourceEntry("ExternalLinkCreateAPage", Description = "phrase: Create a page", LastModified = "2018/10/23", Value = "https://www.progress.com/documentation/sitefinity-cms/create-a-page?ref=sf-dashboard")]
    public string ExternalLinkCreateAPage => this[nameof (ExternalLinkCreateAPage)];

    /// <summary>phrase: Titles, Descriptions, Keywords</summary>
    [ResourceEntry("TitlesDescriptionsKeywords", Description = "phrase: Titles, descriptions, keywords", LastModified = "2010/04/26", Value = "Titles, descriptions, keywords")]
    public string TitlesDescriptionsKeywords => this[nameof (TitlesDescriptionsKeywords)];

    /// <summary>phrase: Names, URLs</summary>
    [ResourceEntry("NamesUrls", Description = "phrase: Names, URLs", LastModified = "2010/04/26", Value = "Names, URLs")]
    public string NamesUrls => this[nameof (NamesUrls)];

    /// <summary>word: Template</summary>
    [ResourceEntry("Template", Description = "word: Template", LastModified = "2010/04/26", Value = "Template")]
    public string Template => this[nameof (Template)];

    /// <summary>word: Owner</summary>
    [ResourceEntry("Owner", Description = "word: Owner", LastModified = "2019/06/24", Value = "Owner")]
    public string Owner => this[nameof (Owner)];

    /// <summary>word: Parent</summary>
    [ResourceEntry("Parent", Description = "word: Parent", LastModified = "2010/04/26", Value = "Parent")]
    public string Parent => this[nameof (Parent)];

    /// <summary>phrase: Filter pages</summary>
    [ResourceEntry("FilterPages", Description = "phrase: Filter pages", LastModified = "2010/04/26", Value = "Filter pages")]
    public string FilterPages => this[nameof (FilterPages)];

    /// <summary>phrase: All pages</summary>
    [ResourceEntry("AllPages", Description = "phrase: All pages", LastModified = "2010/04/26", Value = "All pages")]
    public string AllPages => this[nameof (AllPages)];

    /// <summary>phrase: My pages</summary>
    [ResourceEntry("MyPages", Description = "phrase: My pages", LastModified = "2010/04/26", Value = "My pages")]
    public string MyPages => this[nameof (MyPages)];

    /// <summary>phrase: With no titles</summary>
    [ResourceEntry("WithNoTitles", Description = "phrase: With no Titles", LastModified = "2010/07/26", Value = "With no Titles")]
    public string WithNoTitles => this[nameof (WithNoTitles)];

    /// <summary>phrase: With no descriptions</summary>
    [ResourceEntry("WithNoDescriptions", Description = "phrase: With no descriptions", LastModified = "2010/04/26", Value = "With no Descriptions")]
    public string WithNoDescriptions => this[nameof (WithNoDescriptions)];

    /// <summary>phrase: With no descriptions</summary>
    [ResourceEntry("WithNoDescriptionsAdminApp", Description = "phrase: With no descriptions", LastModified = "2021/02/26", Value = "With no descriptions")]
    public string WithNoDescriptionsAdminApp => this[nameof (WithNoDescriptionsAdminApp)];

    /// <summary>phrase: With no keywords</summary>
    [ResourceEntry("WithNoKeywords", Description = "phrase: With no keywords", LastModified = "2010/04/26", Value = "With no Keywords")]
    public string WithNoKeywords => this[nameof (WithNoKeywords)];

    /// <summary>phrase: by Author</summary>
    [ResourceEntry("ByAuthor", Description = "phrase: by author", LastModified = "2010/04/26", Value = "by <strong>Author</strong>")]
    public string ByAuthor => this[nameof (ByAuthor)];

    /// <summary>phrase: by Date Modified</summary>
    [ResourceEntry("ByDate", Description = "phrase: by Date modified", LastModified = "2010/07/26", Value = "by Date modified...")]
    public string ByDate => this[nameof (ByDate)];

    /// <summary>phrase: by Template</summary>
    [ResourceEntry("ByTemplate", Description = "phrase: by template", LastModified = "2010/04/26", Value = "by <strong>Template</strong>")]
    public string ByTemplate => this[nameof (ByTemplate)];

    /// <summary>phrase: Permissions for pages</summary>
    [ResourceEntry("PermissionsForPages", Description = "phrase: Permissions for pages", LastModified = "2019/03/12", Value = "Permissions for pages")]
    public string PermissionsForPages => this[nameof (PermissionsForPages)];

    /// <summary>phrase: Permissions for all pages</summary>
    [ResourceEntry("PermissionsForAllPages", Description = "phrase: Permissions for all pages", LastModified = "2010/07/26", Value = "Permissions for all pages")]
    public string PermissionsForAllPages => this[nameof (PermissionsForAllPages)];

    /// <summary>phrase: Permissions for all templates</summary>
    [ResourceEntry("PermissionsForAllTemplates", Description = "phrase: Permissions for all templates", LastModified = "2017/12/07", Value = "Permissions for all templates")]
    public string PermissionsForAllTemplates => this[nameof (PermissionsForAllTemplates)];

    /// <summary>phrase: No pages have been created yet.</summary>
    [ResourceEntry("NoPagesHaveBeenCreatedYet", Description = "phrase: No pages have been created yet", LastModified = "2010/07/26", Value = "No pages have been created yet")]
    public string NoPagesHaveBeenCreatedYet => this[nameof (NoPagesHaveBeenCreatedYet)];

    /// <summary>phrase: Create a page</summary>
    [ResourceEntry("CreatePage", Description = "phrase: Create a page", LastModified = "2010/07/26", Value = "Create a page")]
    public string CreatePage => this[nameof (CreatePage)];

    /// <summary>phrase: Create a Template</summary>
    [ResourceEntry("CreateTemplate", Description = "phrase", LastModified = "2009/11/17", Value = "Create a template")]
    public string CreateTemplate => this[nameof (CreateTemplate)];

    /// <summary>phrase: Create a Template</summary>
    [ResourceEntry("DuplicateTemplate", Description = "phrase", LastModified = "2014/02/13", Value = "Duplicate a template")]
    public string DuplicateTemplate => this[nameof (DuplicateTemplate)];

    /// <summary>phrase: on the top level</summary>
    [ResourceEntry("OnTopLevel", Description = "phrase: on the top level", LastModified = "2010/04/28", Value = "on <b class='sfParent'>top level</b>")]
    public string OnTopLevel => this[nameof (OnTopLevel)];

    /// <summary>phrase: No pages matched given criteria.</summary>
    [ResourceEntry("NoPagesMatchedGivenCriteria", Description = "phrase: No pages matched given criteria.", LastModified = "2010/04/29", Value = "No pages matched given criteria.")]
    public string NoPagesMatchedGivenCriteria => this[nameof (NoPagesMatchedGivenCriteria)];

    /// <summary>phrase: Go to all pages</summary>
    [ResourceEntry("GoToAllPages", Description = "phrase: Go to all pages", LastModified = "2010/04/29", Value = "Go to all pages")]
    public string GoToAllPages => this[nameof (GoToAllPages)];

    /// <summary>Control name: Embed CSS</summary>
    [ResourceEntry("CssEmbedControlTitle", Description = "Control title: Embed CSS style sheet", LastModified = "2010/05/10", Value = "CSS")]
    public string CssEmbedControlTitle => this[nameof (CssEmbedControlTitle)];

    /// <summary>
    /// Control description: A control for embedding CSS style sheets
    /// </summary>
    [ResourceEntry("CssEmbedControlDescription", Description = "Control description: A control for embedding CSS style sheets.", LastModified = "2010/05/10", Value = "Embeds CSS files or custom styles in this page")]
    public string CssEmbedControlDescription => this[nameof (CssEmbedControlDescription)];

    /// <summary>phrase: Create a child page</summary>
    [ResourceEntry("CreateChildPage", Description = "phrase: Create a child page", LastModified = "2010/05/12", Value = "Create a child page")]
    public string CreateChildPage => this[nameof (CreateChildPage)];

    /// <summary>phrase: Sibling page before</summary>
    [ResourceEntry("SiblingPageBefore", Description = "phrase: Sibling page before", LastModified = "2010/05/12", Value = "Sibling page before")]
    public string SiblingPageBefore => this[nameof (SiblingPageBefore)];

    /// <summary>phrase: Sibling page after</summary>
    [ResourceEntry("SiblingPageAfter", Description = "phrase: Sibling page after", LastModified = "2010/05/12", Value = "Sibling page after")]
    public string SiblingPageAfter => this[nameof (SiblingPageAfter)];

    /// <summary>Control name: Embed CSS</summary>
    [ResourceEntry("JavaScriptEmbedControlTitle", Description = "Control title: Embed Java Script", LastModified = "2010/05/10", Value = "Java Script")]
    public string JavaScriptEmbedControlTitle => this[nameof (JavaScriptEmbedControlTitle)];

    /// <summary>
    /// Control description: A control for embedding Java Script files/code.
    /// </summary>
    [ResourceEntry("JavaScriptEmbedControlDescription", Description = "Control description: A control for embedding Java Script files/code.", LastModified = "2012/01/05", Value = "Embeds JavaScript files or custom code in this page")]
    public string JavaScriptEmbedControlDescription => this[nameof (JavaScriptEmbedControlDescription)];

    /// <summary>Control name: Embed Google Analytics</summary>
    [ResourceEntry("GoogleAnalyticsEmbedControlTitle", Description = "Control title: Embed Google Analytics", LastModified = "2010/05/10", Value = "Google Analytics")]
    public string GoogleAnalyticsEmbedControlTitle => this[nameof (GoogleAnalyticsEmbedControlTitle)];

    /// <summary>
    /// Control description: A control for embedding Google Analytics code
    /// </summary>
    [ResourceEntry("GoogleAnalyticsEmbedControlDescription", Description = "Control description: A control for embedding Google Analytics code.", LastModified = "2012/01/05", Value = "Embeds Google Analytics code")]
    public string GoogleAnalyticsEmbedControlDescription => this[nameof (GoogleAnalyticsEmbedControlDescription)];

    /// <summary>Phrase: Name cannot be empty</summary>
    [ResourceEntry("NameCannotBeEmpty", Description = "phrase: Title cannot be empty", LastModified = "2010/06/07", Value = "Name cannot be empty")]
    public string NameCannotBeEmpty => this[nameof (NameCannotBeEmpty)];

    /// <summary>phrase: Title cannot be longer than 500 characters.</summary>
    [ResourceEntry("TitleMaxLength", Description = "phrase: Title cannot be longer than 500 characters.", LastModified = "2017/12/12", Value = "Title cannot be longer than 500 characters.")]
    public string TitleMaxLength => this[nameof (TitleMaxLength)];

    /// <summary>The title of the create new page dialog</summary>
    [ResourceEntry("CreateNewItem", Description = "The title of the create new page dialog", LastModified = "2010/07/04", Value = "Create an event")]
    public string CreateNewItem => this[nameof (CreateNewItem)];

    /// <summary>Phrase: URL</summary>
    [ResourceEntry("UrlName", Description = "The title of the url field.", LastModified = "2010/05/30", Value = "URL")]
    public string UrlName => this[nameof (UrlName)];

    /// <summary>Phrase: URL cannot be empty</summary>
    [ResourceEntry("UrlNameCannotBeEmpty", Description = "The message shown when the url is empty.", LastModified = "2010/05/30", Value = "URL cannot be empty")]
    public string UrlNameCannotBeEmpty => this[nameof (UrlNameCannotBeEmpty)];

    /// <summary>Phrase: Title for search engines cannot be empty.</summary>
    [ResourceEntry("SeoTitleCannotBeEmpty", Description = "The message shown when the 'Title for search engines' is empty.", LastModified = "2011/03/11", Value = "Title for search engines cannot be empty.")]
    public string SeoTitleCannotBeEmpty => this[nameof (SeoTitleCannotBeEmpty)];

    /// <summary>
    /// Phrase: 'Title for search engines' contains invalid symbols
    /// </summary>
    [ResourceEntry("SeoTitleInvalidSymbols", Description = "The message shown when the 'Title for search engines' contains invalid symbols.", LastModified = "2011/03/11", Value = "The 'Title for search engines' contains invalid symbols.")]
    public string SeoTitleInvalidSymbols => this[nameof (SeoTitleInvalidSymbols)];

    /// <summary>phrase: URL cannot be longer than 255 characters.</summary>
    [ResourceEntry("UrlMaxLength", Description = "phrase: URL cannot be longer than 255 characters.", LastModified = "2017/12/12", Value = "URL cannot be longer than 255 characters.")]
    public string UrlMaxLength => this[nameof (UrlMaxLength)];

    /// <summary>Phrase: URL contains invalid symbols</summary>
    [ResourceEntry("UrlNameInvalidSymbols", Description = "The message shown when the url contains invalid symbols.", LastModified = "2010/07/13", Value = "The URL contains invalid symbols.")]
    public string UrlNameInvalidSymbols => this[nameof (UrlNameInvalidSymbols)];

    /// <summary>Phrase: Additional URL contains invalid symbols</summary>
    [ResourceEntry("AdditionalUrlNameInvalidSymbols", Description = "The message shown when the url contains invalid symbols.", LastModified = "2011/01/19", Value = "One or more of the specified additional URLs are invalid.")]
    public string AdditionalUrlNameInvalidSymbols => this[nameof (AdditionalUrlNameInvalidSymbols)];

    /// <summary>Phrase: Name contains invalid symbols</summary>
    [ResourceEntry("DevNameInvalidSymbols", Description = "The message shown when the dev name contains invalid symbols.", LastModified = "2010/08/16", Value = "The name contains invalid symbols.")]
    public string DevNameInvalidSymbols => this[nameof (DevNameInvalidSymbols)];

    /// <summary>The SEO title example string at the page dialog</summary>
    [ResourceEntry("SeoTitleExample", Description = "The SEO title example string at the page dialog", LastModified = "2012/01/05", Value = "Displayed in browser title bar and in search results. Make it more descriptive")]
    public string SeoTitleExample => this[nameof (SeoTitleExample)];

    /// <summary>Describes CharacterCounterDescription property.</summary>
    [ResourceEntry("CharacterCounterDescription", Description = "Describes CharacterCounterDescription property.", LastModified = "2013/06/11", Value = "Less than {0} characters are recommended ")]
    public string CharacterCounterDescription => this[nameof (CharacterCounterDescription)];

    /// <summary>The name field example string at the page dialog.</summary>
    [ResourceEntry("NameExample", Description = "The name field example string at the page dialog.", LastModified = "2010/04/07", Value = "Displayed in navigation. <strong>Example:</strong> <em>About Us</em>")]
    public string NameExample => this[nameof (NameExample)];

    /// <summary>
    /// Phrase: Description, Keywords (for search engines only)
    /// </summary>
    [ResourceEntry("DescriptionKeywordsSection", Description = "Phrase: Description, Keywords (for search engines only)", LastModified = "2010/06/07", Value = "Description, Keywords <em class='sfNote'>(for search engines only)</em>")]
    public string DescriptionKeywordsSection => this[nameof (DescriptionKeywordsSection)];

    /// <summary>Word: Description</summary>
    [ResourceEntry("Description", Description = "The 'Description' label.", LastModified = "2010/03/30", Value = "Description")]
    public string Description => this[nameof (Description)];

    /// <summary>Word: Keywords</summary>
    [ResourceEntry("Keywords", Description = "The 'Keywords' label.", LastModified = "2010/03/30", Value = "Keywords")]
    public string Keywords => this[nameof (Keywords)];

    /// <summary>Word: Title for search engines</summary>
    [ResourceEntry("TitleForSearchEngines", Description = "The 'Title for search engines' label.", LastModified = "2010/05/30", Value = "Title for search engines")]
    public string TitleForSearchEngines => this[nameof (TitleForSearchEngines)];

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

    /// <summary>Enable default canonical URL</summary>
    [ResourceEntry("EnableDefaultCanonicalUrl", Description = "The 'Enable default canonical URL' label.", LastModified = "2013/04/29", Value = "Enable default canonical url")]
    public string EnableDefaultCanonicalUrl => this[nameof (EnableDefaultCanonicalUrl)];

    /// <summary>
    /// If set to true and there is no widget that will add a canonical tag a default canonical URL will be added. If set to false (default) the canonical tag will be added only if there is a specific widget on the page and it is not disabled globally.
    /// </summary>
    [ResourceEntry("EnableDefaultCanonicalUrlDescription", Description = "If set to true and there is no widget that will add a canonical tag a default canonical URL will be added. If set to false (default) the canonical tag will be added only if there is a specific widget on the page and it is not disabled globally.", LastModified = "2013/04/29", Value = "If set to true and there is no widget that will add a canonical tag a default canonical URL will be added. If set to false (default) the canonical tag will be added only if there is a specific widget on the page and it is not disabled globally.")]
    public string EnableDefaultCanonicalUrlDescription => this[nameof (EnableDefaultCanonicalUrlDescription)];

    /// <summary>Label: Show in navigation</summary>
    [ResourceEntry("ShowInNavigation", Description = "Label", LastModified = "2010/06/02", Value = "Show in navigation")]
    public string ShowInNavigation => this[nameof (ShowInNavigation)];

    /// <summary>Label: Use this page only to group other pages</summary>
    [ResourceEntry("UseForGroupPage", Description = "Label: Use this page only to group other pages", LastModified = "2010/06/22", Value = "Use this page only to group other pages")]
    public string UseForGroupPage => this[nameof (UseForGroupPage)];

    /// <summary>
    /// Label: This page doesn't have its own content and redirects to the first subpage.
    /// </summary>
    [ResourceEntry("GroupPageDescription", Description = "Label: This page doesn't have its own content and redirects to the first subpage", LastModified = "2010/07/27", Value = "This page doesn't have its own content and redirects to the first subpage")]
    public string GroupPageDescription => this[nameof (GroupPageDescription)];

    /// <summary>Label: This page redirects to another page.</summary>
    [ResourceEntry("ExternalPageFieldLabel", Description = "Label: This page redirects to another page", LastModified = "2011/01/14", Value = "This page redirects to another page")]
    public string ExternalPageFieldLabel => this[nameof (ExternalPageFieldLabel)];

    /// <summary>Label: Please, select a page for the redirection.</summary>
    [ResourceEntry("ExternalPageFieldValidatorMessage", Description = "The error message that shows when the user has marked a page as external but hasn't selected where it will redirect to", LastModified = "2011/01/21", Value = "Please, select a page for the redirection")]
    public string ExternalPageFieldValidatorMessage => this[nameof (ExternalPageFieldValidatorMessage)];

    /// <summary>
    /// Label: This page doesn't have its own content and redirects to another page in the website or to an external page.
    /// </summary>
    [ResourceEntry("ExternalPageFieldDescription", Description = "Label: This page doesn't have its own content and redirects to another page in the website or to an external page", LastModified = "2011/01/14", Value = "This page doesn't have its own content and redirects to another page in the website or to an external page")]
    public string ExternalPageFieldDescription => this[nameof (ExternalPageFieldDescription)];

    /// <summary>Label: Not set.</summary>
    [ResourceEntry("NotSetPageTitle", Description = "Label: When a page title is not set (usually when missing ML page node).", LastModified = "2011/01/24", Value = "Not set")]
    public string NotSetPageTitle => this[nameof (NotSetPageTitle)];

    /// <summary>Label: Set page to redirect to.</summary>
    [ResourceEntry("SetPageToRedirectTo", Description = "Label: Set page to redirect to", LastModified = "2011/01/17", Value = "Set page to redirect to")]
    public string SetPageToRedirectTo => this[nameof (SetPageToRedirectTo)];

    /// <summary>Label: Change.</summary>
    [ResourceEntry("EditPageToRedirectTo", Description = "The label of the open popup button when a page is already selected", LastModified = "2011/01/17", Value = "Change")]
    public string EditPageToRedirectTo => this[nameof (EditPageToRedirectTo)];

    /// <summary>Label: Change.</summary>
    [ResourceEntry("ExternalUrlExample", Description = "The example text below the textbox for external web address", LastModified = "2011/01/18", Value = "<b>Example:</b> http://weather.com")]
    public string ExternalUrlExample => this[nameof (ExternalUrlExample)];

    /// <summary>Label: View this page.</summary>
    [ResourceEntry("ViewThisPage", Description = "Label: View this page", LastModified = "2011/01/17", Value = "View this page")]
    public string ViewThisPage => this[nameof (ViewThisPage)];

    /// <summary>Word: KeywordsDescription</summary>
    [ResourceEntry("KeywordsDescription", Description = "The 'Keywords description' label.", LastModified = "2010/07/26", Value = "Separate keywords with comma.")]
    public string KeywordsDescription => this[nameof (KeywordsDescription)];

    /// <summary>Word: KeywordsExample</summary>
    [ResourceEntry("KeywordsExample", Description = "The 'Keywords example' label.", LastModified = "2010/07/26", Value = "<strong>Example:</strong> <em>music, guitar, song</em>")]
    public string KeywordsExample => this[nameof (KeywordsExample)];

    /// <summary>phrase: Save changes</summary>
    [ResourceEntry("SaveChanges", Description = "Save changes", LastModified = "2010/07/07", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>phrase: Create and go to add content</summary>
    [ResourceEntry("CreateAndAddContent", Description = "Phrase: Create and go to add content", LastModified = "2010/07/07", Value = "Create and go to add content")]
    public string CreateAndAddContent => this[nameof (CreateAndAddContent)];

    /// <summary>word: Cancel</summary>
    [ResourceEntry("Cancel", Description = "The text of the cancel button.", LastModified = "2010/05/30", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>phrase: Delete this image</summary>
    [ResourceEntry("DeleteThisItem", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/07/26", Value = "Delete")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>phrase: ReviewHistory</summary>
    [ResourceEntry("ReviewHistory", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/06/07", Value = "Review history")]
    public string ReviewHistory => this[nameof (ReviewHistory)];

    /// <summary>phrase: Set permissions</summary>
    [ResourceEntry("SetPermissions", Description = "The text of the 'Set permissions' link in the action menu.", LastModified = "2010/03/22", Value = "Set permissions")]
    public string SetPermissions => this[nameof (SetPermissions)];

    /// <summary>Phrase: Create and return to pages</summary>
    [ResourceEntry("CreateAndReturnToPages", Description = "The text of the button for creating a page and returning to pages.", LastModified = "2010/07/07", Value = "Create and return to Pages")]
    public string CreateAndReturnToPages => this[nameof (CreateAndReturnToPages)];

    /// <summary>Phrase: Create and return to templates</summary>
    [ResourceEntry("CreateAndReturnToTemplates", Description = "The text of the button for creating a template and returning to templates.", LastModified = "2010/07/07", Value = "Create and return to Templates")]
    public string CreateAndReturnToTemplates => this[nameof (CreateAndReturnToTemplates)];

    /// <summary>Phrase: Template (You can change it later)</summary>
    [ResourceEntry("TemplateYouCanChangeItLater", Description = "The title of the field for selecting a template.", LastModified = "2019/04/01", Value = "This template is based on…")]
    public string TemplateYouCanChangeItLater => this[nameof (TemplateYouCanChangeItLater)];

    /// <summary>Phrase: Use your own .master file</summary>
    [ResourceEntry("UseYourOwnMasterFile", Description = "The title of the button for selecting a master file.", LastModified = "2010/05/30", Value = "Use your own .master file")]
    public string UseYourOwnMasterFile => this[nameof (UseYourOwnMasterFile)];

    /// <summary>The delete draft message confirm.</summary>
    /// <value>The delete draft message confirm.</value>
    [ResourceEntry("DeleteDraftMessageConfirm", Description = "Used in View Specific page version screen", LastModified = "2010/06/14", Value = "The selected item will be removed. Are you sure you want to continue?")]
    public string DeleteDraftMessageConfirm => this[nameof (DeleteDraftMessageConfirm)];

    /// <summary>The delete draft message confirm.</summary>
    /// <value>The delete draft message confirm.</value>
    [ResourceEntry("DeleteDraftSuccessfull", Description = "Used in View Specific page version screen.", LastModified = "2010/06/14", Value = "The draft was deleted successfully.")]
    public string DeleteDraftSuccessfull => this[nameof (DeleteDraftSuccessfull)];

    /// <summary>Back to page</summary>
    [ResourceEntry("BackToPage", Description = "The text of the back to pages link View Page History in Page editor", LastModified = "2010/06/14", Value = "Back to page")]
    public string BackToPage => this[nameof (BackToPage)];

    /// <summary>Older Page</summary>
    [ResourceEntry("PreviousPage", Description = "Older Page", LastModified = "2010/06/14", Value = "Older version")]
    public string PreviousPage => this[nameof (PreviousPage)];

    /// <summary>Newer Page</summary>
    [ResourceEntry("NextPage", Description = "Newer Page", LastModified = "2010/06/14", Value = "Newer version")]
    public string NextPage => this[nameof (NextPage)];

    /// <summary>Revert to this version</summary>
    [ResourceEntry("CopyAsNew", Description = "Copy As New Page", LastModified = "2010/06/14", Value = "Revert to this version")]
    public string CopyAsNew => this[nameof (CopyAsNew)];

    /// <summary>Previous Page</summary>
    [ResourceEntry("ThisVesrion", Description = "Delete This Version of the page", LastModified = "2010/06/14", Value = "this version")]
    public string ThisVesrion => this[nameof (ThisVesrion)];

    /// <summary>Previous Page</summary>
    [ResourceEntry("Delete", Description = "Delete", LastModified = "2010/06/14", Value = "Delete")]
    public string Delete => this[nameof (Delete)];

    /// <summary>Previous Page</summary>
    [ResourceEntry("Version", Description = "Version Label ", LastModified = "2010/06/14", Value = "Version")]
    public string Version => this[nameof (Version)];

    /// <summary>Previous Page</summary>
    [ResourceEntry("CurrentlyPublishedStatus", Description = "Currently published page status", LastModified = "2010/06/14", Value = "Currently published")]
    public string CurrentlyPublishedStatus => this[nameof (CurrentlyPublishedStatus)];

    /// <summary>Previous Page</summary>
    [ResourceEntry("PreviouslyPublishedStatus", Description = "Previously published page status", LastModified = "2010/06/14", Value = "Previously published")]
    public string PreviouslyPublishedStatus => this[nameof (PreviouslyPublishedStatus)];

    /// <summary>Draft page status.</summary>
    [ResourceEntry("Draft", Description = "Draft page status", LastModified = "2010/08/02", Value = "Draft")]
    public string Draft => this[nameof (Draft)];

    /// <summary>Draft page status - used in versions page.</summary>
    [ResourceEntry("DraftStatus", Description = "Draft page status", LastModified = "2010/06/14", Value = "Draft")]
    public string DraftStatus => this[nameof (DraftStatus)];

    /// <summary>Previous Page</summary>
    [ResourceEntry("IsEditing", Description = "Page version screen label", LastModified = "2010/06/14", Value = "is editing")]
    public string IsEditing => this[nameof (IsEditing)];

    /// <summary>phrase: Under parent page...</summary>
    [ResourceEntry("SelectAParent", Description = "phrase: Under parent page...", LastModified = "2010/06/23", Value = "Under parent page...")]
    public string SelectAParent => this[nameof (SelectAParent)];

    /// <summary>phrase: At top level</summary>
    [ResourceEntry("PagesRootLabel", Description = "At top level", LastModified = "2010/03/06", Value = "At top level")]
    public string PagesRootLabel => this[nameof (PagesRootLabel)];

    /// <summary>phrase: Sort</summary>
    [ResourceEntry("SortPages", Description = "phrase: Sort", LastModified = "2013/01/10", Value = "Sort")]
    public string SortPages => this[nameof (SortPages)];

    /// <summary>phrase: Last modified on top</summary>
    [ResourceEntry("NewModifiedFirst", Description = "Label text.", LastModified = "2010/08/16", Value = "Last modified on top")]
    public string NewModifiedFirst => this[nameof (NewModifiedFirst)];

    /// <summary>phrase: New-created first</summary>
    [ResourceEntry("NewCreatedFirst", Description = "Label text.", LastModified = "2010/07/27", Value = "Last created on top")]
    public string NewCreatedFirst => this[nameof (NewCreatedFirst)];

    /// <summary>phrase: by Hierarchy</summary>
    [ResourceEntry("ByHierarchy", Description = "Label text.", LastModified = "2010/04/06", Value = "by Hierarchy")]
    public string ByHierarchy => this[nameof (ByHierarchy)];

    /// <summary>phrase: by Status</summary>
    [ResourceEntry("ByStatus", Description = "Label text.", LastModified = "2010/04/06", Value = "by Status")]
    public string ByStatus => this[nameof (ByStatus)];

    /// <summary>phrase: by Template</summary>
    [ResourceEntry("SortByTemplate", Description = "Label text.", LastModified = "2010/04/06", Value = "by Template")]
    public string SortByTemplate => this[nameof (SortByTemplate)];

    /// <summary>phrase: Alphabetically (A-Z)</summary>
    [ResourceEntry("AlphabeticallyAsc", Description = "Label text.", LastModified = "2010/04/06", Value = "Alphabetically (A-Z)")]
    public string AlphabeticallyAsc => this[nameof (AlphabeticallyAsc)];

    /// <summary>phrase: Alphabetically (Z-A)</summary>
    [ResourceEntry("AlphabeticallyDesc", Description = "Label text.", LastModified = "2010/04/06", Value = "Alphabetically (Z-A)")]
    public string AlphabeticallyDesc => this[nameof (AlphabeticallyDesc)];

    /// <summary>Phrase: Custom sorting...</summary>
    [ResourceEntry("CustomSorting", Description = "Phrase: Custom sorting...", LastModified = "2010/10/14", Value = "Custom sorting...")]
    public string CustomSorting => this[nameof (CustomSorting)];

    /// <summary>phrase: Create a child of the selected page</summary>
    [ResourceEntry("CreateChildOfTheSelectedPage", Description = "phrase: Create a child of the selected page", LastModified = "2010/06/24", Value = "Create a child of the selected page")]
    public string CreateChildOfTheSelectedPage => this[nameof (CreateChildOfTheSelectedPage)];

    /// <summary>phrase: More actions</summary>
    [ResourceEntry("MoreActions", Description = "phrase: More actions", LastModified = "2010/06/24", Value = "More actions")]
    public string MoreActions => this[nameof (MoreActions)];

    /// <summary>Phrase: Edit a page</summary>
    [ResourceEntry("EditPage", Description = "The label for editing a page.", LastModified = "2010/03/30", Value = "Edit a page")]
    public string EditPage => this[nameof (EditPage)];

    /// <summary>Phrase: Edit a template</summary>
    [ResourceEntry("EditTemplate", Description = "The label for editing a template.", LastModified = "2010/03/30", Value = "Edit a template")]
    public string EditTemplate => this[nameof (EditTemplate)];

    /// <summary>Phrase: Close date</summary>
    [ResourceEntry("CloseDateFilter", Description = "The link for closing the date filter widget in the sidebar.", LastModified = "2010/06/02", Value = "Close dates")]
    public string CloseDateFilter => this[nameof (CloseDateFilter)];

    /// <summary>Phrase: Create this template</summary>
    [ResourceEntry("CreateThisTemplate", Description = "Phrase", LastModified = "2009/11/17", Value = "Create this template")]
    public string CreateThisTemplate => this[nameof (CreateThisTemplate)];

    /// <summary>Phrase: Pages By Date</summary>
    [ResourceEntry("PagesByDate", Description = "phrase: Pages By Date", LastModified = "2010/06/02", Value = "Display pages modified in...")]
    public string ImagesByDate => this["PagesByDate"];

    /// <summary>word: Draft</summary>
    [ResourceEntry("Master", Description = "word: Draft", LastModified = "2010/06/02", Value = "Draft")]
    public string Master => this[nameof (Master)];

    /// <summary>word: Hidden</summary>
    [ResourceEntry("Hidden", Description = "word: Hidden", LastModified = "2010/06/02", Value = "Unpublished")]
    public string Hidden => this[nameof (Hidden)];

    /// <summary>word: Unpublished</summary>
    [ResourceEntry("Unpublished", Description = "word: Unpublished", LastModified = "2018/03/29", Value = "Unpublished")]
    public string Unpublished => this[nameof (Unpublished)];

    /// <summary>word: Locked</summary>
    [ResourceEntry("Locked", Description = "word: Locked", LastModified = "2010/07/30", Value = "Locked")]
    public string Locked => this[nameof (Locked)];

    /// <summary>word: Draft newer than published version</summary>
    [ResourceEntry("PublishedAndDraft", Description = "word: Draft newer than published version", LastModified = "2010/06/02", Value = "Draft newer than published version")]
    public string PublishedAndDraft => this[nameof (PublishedAndDraft)];

    /// <summary>word: Published</summary>
    [ResourceEntry("Published", Description = "word: Published", LastModified = "2010/06/02", Value = "Published")]
    public string Published => this[nameof (Published)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("WaitingForApproval", Description = "The text of the 'Awaiting approval' button in the sidebar.", LastModified = "2010/11/08", Value = "Awaiting approval")]
    public string WaitingForApproval => this[nameof (WaitingForApproval)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("AwaitingReview", Description = "The text of the 'Awaiting review' button in the sidebar.", LastModified = "2018/14/08", Value = "Awaiting review")]
    public string AwaitingReview => this[nameof (AwaitingReview)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("AwaitingPublishing", Description = "The text of the 'Awaiting publishing' button in the sidebar.", LastModified = "2018/14/08", Value = "Awaiting publishing")]
    public string AwaitingPublishing => this[nameof (AwaitingPublishing)];

    /// <summary>phrase: Awaiting approval</summary>
    [ResourceEntry("AwaitingMyAction", Description = "The text of the 'AwaitingMyAction' button in the sidebar.", LastModified = "2018/14/08", Value = "Pages awaiting my action")]
    public string AwaitingMyAction => this[nameof (AwaitingMyAction)];

    /// <summary>phrase: Group page</summary>
    [ResourceEntry("Group", Description = "phrase: Group page", LastModified = "2010/07/08", Value = "Group page")]
    public string Group => this[nameof (Group)];

    /// <summary>phrase: No themes have been uploaded yet</summary>
    [ResourceEntry("NoThemesAvailable", Description = "phrase: No themes have been uploaded yet", LastModified = "2010/07/14", Value = "No themes have been uploaded yet")]
    public string NoThemesAvailable => this[nameof (NoThemesAvailable)];

    /// <summary>phrase: Put this page...</summary>
    [ResourceEntry("PutThisPageDots", Description = "phrase: Put this page...", LastModified = "2010/07/14", Value = "Put this page...")]
    public string PutThisPageDots => this[nameof (PutThisPageDots)];

    /// <summary>phrase: No theme</summary>
    [ResourceEntry("NoTheme", Description = "phrase: -- no theme --", LastModified = "2010/07/28", Value = "-- no theme --")]
    public string NoTheme => this[nameof (NoTheme)];

    /// <summary>Alternative Publishing</summary>
    [ResourceEntry("AlternativePublishingTitle", Description = "The title of the System section.", LastModified = "2009/05/07", Value = "Alternative Publishing")]
    public string AlternativePublishingTitle => this[nameof (AlternativePublishingTitle)];

    /// <summary>AlternativePublishing</summary>
    [ResourceEntry("AlternativePublishingUrlName", Description = "The name of the System node that appears in the URL.", LastModified = "2009/05/07", Value = "AlternativePublishing")]
    public string AlternativePublishingUrlName => this[nameof (AlternativePublishingUrlName)];

    /// <summary>The description of the System section.</summary>
    [ResourceEntry("AlternativePublishingDescription", Description = "The description of the System section.", LastModified = "2009/05/07", Value = "Provides modules related to publishing and consuming content from external systems and channels")]
    public string AlternativePublishingDescription => this[nameof (AlternativePublishingDescription)];

    /// <summary>
    /// Provides modules related to publishing and consuming content from external systems and channels
    /// </summary>
    [ResourceEntry("DraftNewerThanPublishedMessage", Description = "The message that is displayed when you start editing apage that has a draft newer than the published version.", LastModified = "2010/08/02", Value = "The draft is newer than the published page. <a href='{0}' target='_blank'>Preview</a> the published version of the page.")]
    public string DraftNewerThanPublishedMessage => this[nameof (DraftNewerThanPublishedMessage)];

    /// <summary>
    /// Provides modules related to publishing and consuming content from external systems and channels
    /// </summary>
    [ResourceEntry("pageTemplateNotFound", Description = "This message is dispayed when the page template is missing", LastModified = "2010/09/21", Value = "The page template is missing so the page will not be displayed")]
    public string PageTemplateNotFound => this["pageTemplateNotFound"];

    /// <summary>
    /// Provides modules related to publishing and consuming content from external systems and channels
    /// </summary>
    [ResourceEntry("PageThemeNotFound", Description = "This message is dispayed when the page theme is missing.", LastModified = "2010/09/22", Value = "The selected theme could not be found: {0}.")]
    public string PageThemeNotFound => this[nameof (PageThemeNotFound)];

    /// <summary>Template explorer: Not based on other template</summary>
    [ResourceEntry("NotBasedOnOtherTemplate", Description = "Template explorer: Not based on other template.", LastModified = "2010/10/15", Value = "Not based on other template")]
    public string NotBasedOnOtherTemplate => this[nameof (NotBasedOnOtherTemplate)];

    /// <summary>
    /// Template explorer: Click to view which pages use this template.
    /// </summary>
    [ResourceEntry("ClickToViewWhichPagesUseThisTemplate", Description = "Template explorer: Click to view which pages use this template.", LastModified = "2010/10/15", Value = "Click to view which pages use this template.")]
    public string ClickToViewWhichPagesUseThisTemplate => this[nameof (ClickToViewWhichPagesUseThisTemplate)];

    /// <summary>Phrase: Click to view which sites use this template.</summary>
    [ResourceEntry("ClickToViewWhichSitesUseThisTemplate", Description = "Phrase: Click to view which sites use this template.", LastModified = "2012/09/11", Value = "Click to view which sites use this template.")]
    public string ClickToViewWhichSitesUseThisTemplate => this[nameof (ClickToViewWhichSitesUseThisTemplate)];

    /// <summary>Phrase: Time zone</summary>
    [ResourceEntry("BasicSettingsSiteFor", Description = "The label in Basic Settings site dropdown.", LastModified = "2018/09/17", Value = "{0} for...")]
    public string BasicSettingsSiteFor => this[nameof (BasicSettingsSiteFor)];

    /// <summary>Word: General</summary>
    [ResourceEntry("General", Description = "The 'General' link text in Basic Settings sidebar.", LastModified = "2010/09/30", Value = "General")]
    public string General => this[nameof (General)];

    /// <summary>Phrase: Time zone</summary>
    [ResourceEntry("TimeZone", Description = "The 'Time zone' link text in Basic Settings sidebar.", LastModified = "2011/12/12", Value = "Time zone")]
    public string TimeZone => this[nameof (TimeZone)];

    /// <summary>Word: Languages</summary>
    [ResourceEntry("Languages", Description = "The 'Languages' link text in Basic Settings sidebar.", LastModified = "2010/09/30", Value = "Languages")]
    public string Languages => this[nameof (Languages)];

    /// <summary>Word: Comments</summary>
    [ResourceEntry("Comments", Description = "The 'Comments' link text in Basic Settings sidebar.", LastModified = "2010/09/30", Value = "Comments")]
    public string Comments => this[nameof (Comments)];

    /// <summary>Word: Twitter</summary>
    [ResourceEntry("Twitter", Description = "The 'Twitter' link text in Basic Settings sidebar.", LastModified = "2010/09/30", Value = "Twitter")]
    public string Twitter => this[nameof (Twitter)];

    /// <summary>Word: Workflow</summary>
    [ResourceEntry("Workflow", Description = "The 'Workflow' link text in Basic Settings sidebar.", LastModified = "2010/09/30", Value = "Workflow")]
    public string Workflow => this[nameof (Workflow)];

    /// <summary>Word: Advanced</summary>
    [ResourceEntry("Advanced", Description = "The 'Advanced' link text.", LastModified = "2010/09/30", Value = "Advanced")]
    public string Advanced => this[nameof (Advanced)];

    /// <summary>Word: Basic</summary>
    [ResourceEntry("Basic", Description = "The 'Basic' link text.", LastModified = "2010/09/30", Value = "Basic")]
    public string Basic => this[nameof (Basic)];

    /// <summary>Phrase: Use spam protection image (captcha)</summary>
    [ResourceEntry("UseSpamProtectionImage", Description = "Phrase", LastModified = "2010/10/04", Value = "Use spam protection image (captcha)")]
    public string UseSpamProtectionImage => this[nameof (UseSpamProtectionImage)];

    /// <summary>
    /// Phrase: Automatically close comments for items older than
    /// </summary>
    [ResourceEntry("AutomaticallyCloseComments", Description = "Phrase", LastModified = "2010/10/04", Value = "Automatically close comments for items older than")]
    public string AutomaticallyCloseComments => this[nameof (AutomaticallyCloseComments)];

    /// <summary>Word: Email</summary>
    [ResourceEntry("Email", Description = "Word", LastModified = "2010/10/04", Value = "Email")]
    public string Email => this[nameof (Email)];

    /// <summary>Word: Website</summary>
    [ResourceEntry("Website", Description = "Word", LastModified = "2010/10/04", Value = "Website")]
    public string Website => this[nameof (Website)];

    /// <summary>Word: Message</summary>
    [ResourceEntry("Message", Description = "Word", LastModified = "2010/10/04", Value = "Message")]
    public string Message => this[nameof (Message)];

    /// <summary>Word: Required</summary>
    [ResourceEntry("Required", Description = "Word", LastModified = "2010/10/04", Value = "Required")]
    public string Required => this[nameof (Required)];

    /// <summary>Word: days</summary>
    [ResourceEntry("Days", Description = "Word", LastModified = "2010/10/04", Value = "days")]
    public string Days => this[nameof (Days)];

    /// <summary>
    /// Phrase: What fields should contain the form for posting comments?
    /// </summary>
    [ResourceEntry("WhatFieldsShouldContainTheForm", Description = "Phrase", LastModified = "2010/10/04", Value = "What fields should contain the form for posting comments?")]
    public string WhatFieldsShouldContainTheForm => this[nameof (WhatFieldsShouldContainTheForm)];

    /// <summary>
    /// Phrase: You can change particular comment forms if you want to be different.
    /// </summary>
    [ResourceEntry("YouCanChangeParticularCommentForms", Description = "Phrase", LastModified = "2010/10/04", Value = "You can change particular comment forms if you want to be different.")]
    public string YouCanChangeParticularCommentForms => this[nameof (YouCanChangeParticularCommentForms)];

    /// <summary>word: Workflow</summary>
    [ResourceEntry("WorkflowTitle", Description = "word: Workflow", LastModified = "2010/09/28", Value = "Workflow")]
    public string WorkflowTitle => this[nameof (WorkflowTitle)];

    /// <summary>phrase: Page for managing workflows</summary>
    [ResourceEntry("WorkflowDescriptionMenu", Description = "phrase: Page for managing workflows", LastModified = "2010/09/28", Value = "Page for managing workflows")]
    public string WorkflowDescriptionMenu => this[nameof (WorkflowDescriptionMenu)];

    /// <summary>Toolbox title: Language selector</summary>
    [ResourceEntry("LanguageSelectorControlTitle", Description = "Toolbox title: Language selector", LastModified = "2010/10/25", Value = "Language selector")]
    public string LanguageSelectorControlTitle => this[nameof (LanguageSelectorControlTitle)];

    /// <summary>Toolbox description: Language selector</summary>
    [ResourceEntry("LanguageSelectorControlDescription", Description = "Toolbox description: Language selector", LastModified = "2010/10/25", Value = "Switcher between site languages")]
    public string LanguageSelectorControlDescription => this[nameof (LanguageSelectorControlDescription)];

    /// <summary>phrase: Back to Pages</summary>
    [ResourceEntry("BackToAllPages", Description = "phrase: Back to Pages", LastModified = "2010/11/08", Value = "Back to Pages")]
    public string BackToAllPages => this[nameof (BackToAllPages)];

    /// <summary>phrase: Selected pages</summary>
    [ResourceEntry("SelectedPages", Description = "phrase: Selected pages", LastModified = "2018/08/16", Value = "Selected pages")]
    public string SelectedPages => this[nameof (SelectedPages)];

    /// <summary>phrase: Back to Revision History</summary>
    [ResourceEntry("BackToRevisionHistory", Description = "phrase: Back to Revision History", LastModified = "2010/11/08", Value = "Back to Revision History")]
    public string BackToRevisionHistory => this[nameof (BackToRevisionHistory)];

    /// <summary>phrase: Back to Revision History</summary>
    [ResourceEntry("BackToEditor", Description = "phrase: Back to editor", LastModified = "2010/11/08", Value = "Back to editor")]
    public string BackToEditor => this[nameof (BackToEditor)];

    /// <summary>word: Area</summary>
    [ResourceEntry("Area", Description = "word: Area", LastModified = "2010/11/11", Value = "Area")]
    public string Area => this[nameof (Area)];

    /// <summary>phrase: Pages data fields</summary>
    [ResourceEntry("PagesDataFields", Description = "phrase: Pages data fields", LastModified = "2010/11/29", Value = "Pages data fields")]
    public string PagesDataFields => this[nameof (PagesDataFields)];

    /// <summary>
    /// Phrase: Advanced options (Caching, Multiple URLs, Head tags)
    /// </summary>
    [ResourceEntry("AdvancedOptionsSection", Description = "Phrase: Advanced options (Caching, Head tags)", LastModified = "2013/11/18", Value = "Advanced options <em class='sfNote'>(Caching, Head tags)</em>")]
    public string AdvancedOptionsSection => this[nameof (AdvancedOptionsSection)];

    /// <summary>
    /// Phrase: Allow external search engines to index this page and include in Sitemap
    /// </summary>
    /// <value>Allow <strong> external search engines to index</strong> this page and include in Sitemap</value>
    [ResourceEntry("AllowSearchEnginesToIndexThisPage", Description = "Phrase: Allow external search engines to index this page and include in Sitemap", LastModified = "2014/03/06", Value = "Allow <strong> external search engines to index</strong> this page and include in Sitemap")]
    public string AllowSearchEnginesToIndexThisPage => this[nameof (AllowSearchEnginesToIndexThisPage)];

    /// <summary>Phrase: Require SSL</summary>
    [ResourceEntry("RequireSsl", Description = "Phrase: Require SSL", LastModified = "2010/12/21", Value = "Require <strong>SSL</strong>")]
    public string RequireSsl => this[nameof (RequireSsl)];

    /// <summary>Phrase: Enable ViewState</summary>
    [ResourceEntry("EnableViewState", Description = "Phrase: Enable ViewState", LastModified = "2010/12/21", Value = "Enable <strong>ViewState</strong>")]
    public string EnableViewState => this[nameof (EnableViewState)];

    /// <summary>Phrase: Include RadScriptManager</summary>
    [ResourceEntry("IncludeRadScriptManager", Description = "Phrase: Include RadScriptManager", LastModified = "2011/01/10", Value = "Include <strong>RadScriptManager</strong>")]
    public string IncludeRadScriptManager => this[nameof (IncludeRadScriptManager)];

    /// <summary>
    /// Phrase: HTML included in the head tag (except title, keywords, description)
    /// </summary>
    [ResourceEntry("HtmlIncludedInTheHeadTag", Description = "Phrase: HTML included in the head tag (except title, keywords, description)", LastModified = "2010/12/21", Value = "HTML included in the &lt;head&gt; tag (except title, keywords, description)")]
    public string HtmlIncludedInTheHeadTag => this[nameof (HtmlIncludedInTheHeadTag)];

    /// <summary>Phrase: HTML included in the head tag - example</summary>
    [ResourceEntry("HtmlIncludedInTheHeadTagExample", Description = "Phrase: HTML included in the head tag - example", LastModified = "2010/12/21", Value = "<strong>Example:</strong><br/>&lt;meta name=\"author\" content=\"John Smith\" /&gt; <br/> &lt;link rel=\"stylesheet\" type=\"text/css\" media=\"screen and (max-width: 600px)\" href=\"myCssFile.css\" /&gt;<br/>&lt;script type=\"text/javascript\" src=\"myJavascriptFile.js\"&gt;&lt;/script&gt;")]
    public string HtmlIncludedInTheHeadTagExample => this[nameof (HtmlIncludedInTheHeadTagExample)];

    /// <summary>Phrase: HTML included in the head tag - example</summary>
    [ResourceEntry("HtmlIncludedInTheHeadTagExampleNewUI", Description = "Phrase: HTML included in the head tag - example", LastModified = "2020/7/15", Value = "<meta name=\"author\" content=\"John Smith\" /> <link rel=\"stylesheet\" type=\"text/css\" media=\"screen and (max-width: 600px)\" href=\"myCssFile.css\" /><script type=\"text/javascript\" src=\"myJavascriptFile.js\"></script>")]
    public string HtmlIncludedInTheHeadTagExampleNewUI => this[nameof (HtmlIncludedInTheHeadTagExampleNewUI)];

    /// <summary>Phrase: Enable Caching...</summary>
    [ResourceEntry("EnableCaching", Description = "Phrase: Enable Caching...", LastModified = "2010/12/22", Value = "Enable <strong>Caching</strong>...")]
    public string EnableCaching => this[nameof (EnableCaching)];

    /// <summary>Phrase: Use default settings for Caching</summary>
    [ResourceEntry("UseDefaultSettingsForCaching", Description = "Phrase: Use default settings for Caching", LastModified = "2010/12/22", Value = "Use default settings for Caching")]
    public string UseDefaultSettingsForCaching => this[nameof (UseDefaultSettingsForCaching)];

    /// <summary>Phrase: Cache duration (in seconds)</summary>
    [ResourceEntry("CacheDuration", Description = "Phrase: Cache duration (in seconds)", LastModified = "2010/12/22", Value = "Cache duration <span class='sfNote'>(in seconds)</span>")]
    public string CacheDuration => this[nameof (CacheDuration)];

    /// <summary>Phrase: Sliding expiration</summary>
    [ResourceEntry("SlidingExpiration", Description = "Phrase: Sliding expiration", LastModified = "2010/12/22", Value = "Sliding expiration")]
    public string SlidingExpiration => this[nameof (SlidingExpiration)];

    /// <summary>Phrase: Enable multiple URLs for this page</summary>
    /// <value>Enable multiple URLs for this page...</value>
    [ResourceEntry("AllowMultipleUrlsForThisPage", Description = "Phrase: Enable multiple URLs for this page", LastModified = "2013/11/18", Value = "Enable multiple URLs for this page...")]
    public string AllowMultipleUrlsForThisPage => this[nameof (AllowMultipleUrlsForThisPage)];

    /// <summary>Phrase: Additional URLs</summary>
    /// <value><strong>Additional URLs</strong></value>
    [ResourceEntry("AdditionalUrls", Description = "Phrase: Additional URLs", LastModified = "2013/11/18", Value = "<strong>Additional URLs</strong>")]
    public string AdditionalUrls => this[nameof (AdditionalUrls)];

    /// <summary>Phrase: Additional URLs - example</summary>
    /// <value>One per line. <strong>Example:</strong>~/contacts</value>
    [ResourceEntry("AdditionalUrlsExample", Description = "Phrase: Additional URLs - example", LastModified = "2013/11/18", Value = "One per line.  <strong>Example:</strong> ~/contacts")]
    public string AdditionalUrlsExample => this[nameof (AdditionalUrlsExample)];

    /// <summary>Phrase: Additional URLs redirect to the default URL</summary>
    /// <value>Additional URLs redirect to the default URL:</value>
    [ResourceEntry("AllAdditionalUrlsRedirectToTheDefaultOne", Description = "Phrase: Additional URLs redirect to the default URL", LastModified = "2013/11/18", Value = "Additional URLs redirect to the default URL:")]
    public string AllAdditionalUrlsRedirectToTheDefaultOne => this[nameof (AllAdditionalUrlsRedirectToTheDefaultOne)];

    /// <summary>Title of the toolbox section for newsletter control</summary>
    [ResourceEntry("NewslettersToolboxSectionTitle", Description = "Title of the toolbox section for newsletter controls.", LastModified = "2011/07/28", Value = "Email Campaigns")]
    public string NewslettersToolboxSectionTitle => this[nameof (NewslettersToolboxSectionTitle)];

    /// <summary>
    /// Description of the toolbox section for the newsletter controls
    /// </summary>
    [ResourceEntry("NewslettersToolboxSectionDescription", Description = "Description of the toolbox section for the newsletter controls.", LastModified = "2010/12/16", Value = "This section contains public controls of the newsletters module.")]
    public string NewslettersToolboxSectionDescription => this[nameof (NewslettersToolboxSectionDescription)];

    /// <summary>
    /// Error message: You are not authorized to access this page
    /// </summary>
    [ResourceEntry("YouAreNotAuthorizedToAccessThisPage", Description = "Error message: You are not authorized to access this page", LastModified = "2011/01/06", Value = "You are not authorized to access this page")]
    public string YouAreNotAuthorizedToAccessThisPage => this[nameof (YouAreNotAuthorizedToAccessThisPage)];

    /// <summary>
    /// An error message displayed when the user tries to split a group page.
    /// </summary>
    [ResourceEntry("CannotSplitGroupPage", Description = "An error message displayed when the user tries to split a group page.", LastModified = "2011/01/11", Value = "The given page node is a group page node. You can only split standard page nodes.")]
    public string CannotSplitGroupPage => this[nameof (CannotSplitGroupPage)];

    /// <summary>
    /// An error message displayed when the user tries to split a page which is already split.
    /// </summary>
    [ResourceEntry("CannotSplitAlreadySplit", Description = "An error message displayed when the user tries to split a page which is already split.", LastModified = "2011/01/11", Value = "The given page node is already split!")]
    public string CannotSplitAlreadySplit => this[nameof (CannotSplitAlreadySplit)];

    /// <summary>
    /// An error message displayed when the user tries to split a page which has non-split children.
    /// </summary>
    [ResourceEntry("CannotSplitNonSplitChildren", Description = "An error message displayed when the user tries to split a page which has non-split children.", LastModified = "2011/01/11", Value = "The given page node has child nodes. You can only split a node if it does not have child nodes or all of its child nodes are also split.")]
    public string CannotSplitNonSplitChildren => this[nameof (CannotSplitNonSplitChildren)];

    /// <summary>phrase: Redirecting page</summary>
    [ResourceEntry("RedirectingPage", Description = "phrase: Redirecting page", LastModified = "2011/01/19", Value = "Redirecting page")]
    public string RedirectingPage => this[nameof (RedirectingPage)];

    /// <summary>phrase: Status / Page / Location</summary>
    [ResourceEntry("StatusPageLocation", Description = "phrase: Status / Page / Location", LastModified = "2011/01/18", Value = "Status / Page / Location")]
    public string StatusPageLocation => this[nameof (StatusPageLocation)];

    /// <summary>
    /// An error message displayed when the user tries to create a assign a parent to a page, when the parent had no permissions to create child pages.
    /// </summary>
    [ResourceEntry("CannotCreateChildPages", Description = "An error message displayed when the user tries to create a assign a parent to a page, when the parent had no permissions to create child pages.", LastModified = "2011/02/21", Value = "You have no permissions to create pages under {0}.")]
    public string CannotCreateChildPages => this[nameof (CannotCreateChildPages)];

    /// <summary>The title of the ecommerce node</summary>
    [ResourceEntry("EcommerceNodeTitle", Description = "The title of the ecommerce node", LastModified = "2011/03/05", Value = "Ecommerce")]
    public string EcommerceNodeTitle => this[nameof (EcommerceNodeTitle)];

    /// <summary>The url name of the ecommerce node</summary>
    [ResourceEntry("EcommerceNodeUrl", Description = "The url name of the ecommerce node", LastModified = "2011/03/05", Value = "ecommerce")]
    public string EcommerceNodeUrl => this[nameof (EcommerceNodeUrl)];

    /// <summary>The description of the ecommerce node</summary>
    [ResourceEntry("EcommerceNodeDescription", Description = "The description of the ecommerce node", LastModified = "2011/03/05", Value = "Ecommerce module node")]
    public string EcommerceNodeDescription => this[nameof (EcommerceNodeDescription)];

    /// <summary>
    /// An error message displayed when the user tries to modify a page which they are not allowed.
    /// </summary>
    [ResourceEntry("CannotModifyPage", Description = "An error message displayed when the user tries to modify a page which they are not allowed.", LastModified = "2011/02/21", Value = "You have no permissions to modify {0}.")]
    public string CannotModifyPage => this[nameof (CannotModifyPage)];

    /// <summary>Phrase: Code behind type (for ASP.NET developers)</summary>
    [ResourceEntry("CodeBehindType", Description = "Phrase: Code behind type (for ASP.NET developers)", LastModified = "2011/04/29", Value = "<strong>Code behind type</strong> (for ASP.NET developers)")]
    public string CodeBehindType => this[nameof (CodeBehindType)];

    /// <summary>Phrase: Code behind type (for ASP.NET developers)</summary>
    [ResourceEntry("CodeBehindTypeNewUI", Description = "Phrase: Code behind type (for ASP.NET developers)", LastModified = "2020/07/15", Value = "Code behind type (for ASP.NET developers)")]
    public string CodeBehindTypeNewUI => this[nameof (CodeBehindTypeNewUI)];

    /// <summary>Phrase: Use the fully qualified name of the type.</summary>
    [ResourceEntry("UseTheFullyQualifiedNameOfTheType", Description = "Phrase: Use the fully qualified name of the type.", LastModified = "2011/05/30", Value = "Use the fully qualified name of the type.")]
    public string UseTheFullyQualifiedNameOfTheType => this[nameof (UseTheFullyQualifiedNameOfTheType)];

    /// <summary>Code behind type - Example</summary>
    [ResourceEntry("CodeBehindTypeExample", Description = "Code behind type - Example", LastModified = "2011/04/29", Value = "MyNamespace.MyPage, MyAssembly")]
    public string CodeBehindTypeExample => this[nameof (CodeBehindTypeExample)];

    [ResourceEntry("HtmlIncludedInHeadTitle", Description = "Phrase: HTML included in the <head> tag (except title and description)", LastModified = "2019/02/27", Value = "HTML included in the <head> tag (except title and description)")]
    public string HtmlIncludedInHeadTitle => this[nameof (HtmlIncludedInHeadTitle)];

    /// <summary>Phrase: Code behind type (for ASP.NET developers)</summary>
    [ResourceEntry("CodeBehindTypeTitle", Description = "Code behind type (for ASP.NET developers)", LastModified = "2019/02/27", Value = "Code behind type (for ASP.NET developers)")]
    public string CodeBehindTypeTitle => this[nameof (CodeBehindTypeTitle)];

    /// <summary>phrase: Canonical URL</summary>
    [ResourceEntry("CanonicalURLCapitals", Description = "phrase: Canonical URL", LastModified = "2019/02/27", Value = "Canonical URL")]
    public string CanonicalURLCapitals => this[nameof (CanonicalURLCapitals)];

    /// <summary>The title of a warning prompt dialog.</summary>
    [ResourceEntry("PromptTitleWarning", Description = "The title of a warning prompt dialog.", LastModified = "2010/09/30", Value = "Warning")]
    public string PromptTitleWarning => this[nameof (PromptTitleWarning)];

    /// <summary>
    /// The message of the warning dialog displpayed when changing the template of a published page.
    /// </summary>
    [ResourceEntry("PromptMessagePageChangeTemplate", Description = "The message of the warning dialog displayed when changing the template of a single published page.", LastModified = "2010/09/30", Value = "You are about to change the template of a <strong>published</strong> page.<br /><br />A <strong>new draft</strong> will be created for this page and the selected template will be applied to this draft.")]
    public string PromptMessagePageChangeTemplate => this[nameof (PromptMessagePageChangeTemplate)];

    /// <summary>
    /// The message of the warning dialog displpayed when changing the template of a published page in a batch operation.
    /// </summary>
    [ResourceEntry("PromptMessagePageChangeTemplates", Description = "The message of the warning dialog displayed when changing the template of a single published page in a batch operation.", LastModified = "2010/09/30", Value = "You are about to change the template of one or more <strong>published</strong> pages.<br /><br />A <strong>new draft</strong> will be created for those pages and the selected template will be applied to those drafts.")]
    public string PromptMessagePageChangeTemplates => this[nameof (PromptMessagePageChangeTemplates)];

    /// <summary>The title of an Info prompt dialog.</summary>
    [ResourceEntry("PromptTitleInfo", Description = "The title of an Info prompt dialog.", LastModified = "2010/09/30", Value = "Info")]
    public string PromptTitleInfo => this[nameof (PromptTitleInfo)];

    /// <summary>
    /// The message of the dialog displpayed when the template of a page was changed successfully.
    /// </summary>
    [ResourceEntry("PromptMessageTemplateChanged", Description = "The message of the dialog displpayed when the template of a page was changed successfully.", LastModified = "2010/09/30", Value = "The template was successfully changed.")]
    public string PromptMessageTemplateChanged => this[nameof (PromptMessageTemplateChanged)];

    /// <summary>
    /// The message of the dialog displpayed when the template of a page was NOT changed successfully.
    /// </summary>
    [ResourceEntry("PromptMessageTemplateChangeFailed", Description = "The message of the dialog displpayed when the template of a page was NOT changed successfully.", LastModified = "2010/09/30", Value = "The template was not changed. The page is locked by someone else.")]
    public string PromptMessageTemplateChangeFailed => this[nameof (PromptMessageTemplateChangeFailed)];

    /// <summary>
    /// A part of the message displayed when the page is already locked.
    /// </summary>
    [ResourceEntry("IsLockedForEditingBy", Description = "A part of the message displayed when the page is already locked.", LastModified = "2010/10/14", Value = "is locked for editing by")]
    public string IsLockedForEditingBy => this[nameof (IsLockedForEditingBy)];

    /// <summary>
    /// The message displayed when the page which the user tries to edit is not available.
    /// </summary>
    [ResourceEntry("ItemToEditNotAvailable", Description = "The message displayed when the page which the user tries to edit is not available.", LastModified = "2010/10/14", Value = "The item you are trying to edit is no longer available.")]
    public string ItemToEditNotAvailable => this[nameof (ItemToEditNotAvailable)];

    /// <summary>
    /// Button message: Try to reload the current page (in case the draft is no longer available / changed / locked).
    /// </summary>
    [ResourceEntry("TryToReload", Description = "Button message: Try to reload the current page (in case the draft is no longer available / changed / locked).", LastModified = "2011/02/25", Value = "Try to reload")]
    public string TryToReload => this[nameof (TryToReload)];

    /// <summary>
    /// The message of the dialog displpayed when the template of multiple pages was changed successfully.
    /// </summary>
    [ResourceEntry("PromptMessageTemplatesChanged", Description = "The message of the dialog displpayed when the template of multiple pages was changed successfully.", LastModified = "2010/09/30", Value = "The template was successfully applied to {0} pages.")]
    public string PromptMessageTemplatesChanged => this[nameof (PromptMessageTemplatesChanged)];

    /// <summary>
    /// The message of the dialog displpayed when changing the template of multiple pages was NOT successfull for all selected pages.
    /// </summary>
    [ResourceEntry("PromptMessageTemplatesChangeFailed", Description = "The message of the dialog displpayed when changing the template of multiple pages was NOT successfull for all selected pages.", LastModified = "2010/09/30", Value = "The template was successfully applied to {0} pages. The template was not applied to the following pages:<br/>{1}")]
    public string PromptMessageTemplatesChangeFailed => this[nameof (PromptMessageTemplatesChangeFailed)];

    /// <summary>
    /// The message of the warning dialog displpayed when deleting an template that is not used.
    /// </summary>
    [ResourceEntry("PromptMessageTemplateNotUsed", Description = "The message of the warning dialog displpayed when deleting an template that is not used.", LastModified = "2010/09/30", Value = "Are you sure you want to delete this template?")]
    public string PromptMessageTemplateNotUsed => this[nameof (PromptMessageTemplateNotUsed)];

    /// <summary>
    /// Title of the warning dialog displpayed when deleting single template that is used by pages or by other templates.
    /// </summary>
    [ResourceEntry("YouCannotDeleteATemplateInUseTitle", Description = "You cannot delete a template in use.", LastModified = "2010/09/30", Value = "You cannot delete a template in use")]
    public string YouCannotDeleteATemplateInUseTitle => this[nameof (YouCannotDeleteATemplateInUseTitle)];

    /// <summary>
    /// Title of the warning dialog displpayed when the deletion of a page is not allowed.
    /// </summary>
    [ResourceEntry("PageCannotBeDeletedTitle", Description = "Page cannot be deleted", LastModified = "2019/02/01", Value = "Page cannot be deleted")]
    public string PageCannotBeDeletedTitle => this[nameof (PageCannotBeDeletedTitle)];

    /// <summary>
    /// Message shown when a non-admin user tries to delete a page containing children.
    /// </summary>
    [ResourceEntry("PromptMessagePageCannotDeleteChildren", Description = "Message shown when a non-admin user tries to delete a page containing children.", LastModified = "2011/01/11", Value = "To delete this page you should move or delete its child pages first.")]
    public string PromptMessagePageCannotDeleteChildren => this[nameof (PromptMessagePageCannotDeleteChildren)];

    /// <summary>
    /// Title of the message box shown when a non-admin user tries to delete a page containing children.
    /// </summary>
    [ResourceEntry("PromptTitlePageCannotDeleteChildren", Description = "Title of the message box shown when a non-admin user tries to delete a page containing children.", LastModified = "2011/01/11", Value = "You cannot delete a page that has child pages.")]
    public string PromptTitlePageCannotDeleteChildren => this[nameof (PromptTitlePageCannotDeleteChildren)];

    /// <summary>
    /// Title of the message box shown when a non-admin user tries to delete a page containing children.
    /// </summary>
    [ResourceEntry("PromptTitlePageCannotDeleteChildrenAndSuggestion", Description = "Title of the message box shown when a non-admin user tries to delete a page containing children.", LastModified = "2019/02/01", Value = "You cannot delete a page that has child pages.You should move or delete child pages first.")]
    public string PromptTitlePageCannotDeleteChildrenAndSuggestion => this[nameof (PromptTitlePageCannotDeleteChildrenAndSuggestion)];

    /// <summary>
    /// Message shown when an attempt to delete the hompate is made.
    /// </summary>
    [ResourceEntry("PromptMessagePageCannotDeleteHomepage", Description = "Message shown when an attempt to delete the hompate is made.", LastModified = "2011/02/14", Value = "Before you are able to delete this page, you must set another page as a Homepage")]
    public string PromptMessagePageCannotDeleteHomepage => this[nameof (PromptMessagePageCannotDeleteHomepage)];

    /// <summary>
    /// Message shown when an invalid attempt to create a child page for page is made.
    /// </summary>
    [ResourceEntry("PromptMessagePageCannotCreateChildPage", Description = "Message shown when an invalid attempt to create a child page for page is made.", LastModified = "2011/03/21", Value = "Before you are able to create a child page of this page, create a language version for the currently selected language.")]
    public string PromptMessagePageCannotCreateChildPage => this[nameof (PromptMessagePageCannotCreateChildPage)];

    /// <summary>
    /// Title of the message shown when an attempt to delete the hompate is made.
    /// </summary>
    [ResourceEntry("PromptTitlePageCannotDeleteHomepage", Description = "Title of the message shown when an attempt to delete the hompate is made.", LastModified = "2011/02/14", Value = "You cannot delete the Homepage")]
    public string PromptTitlePageCannotDeleteHomepage => this[nameof (PromptTitlePageCannotDeleteHomepage)];

    /// <summary>
    /// Title of the message shown when an attempt to delete the hompate is made.
    /// </summary>
    [ResourceEntry("PromptTitlePageCannotDeleteHomepageWithSuggestion", Description = "Title of the message shown when an attempt to delete the hompate is made.", LastModified = "2019/02/01", Value = "You cannot delete the Homepage.You must set another page as a Homepage first.")]
    public string PromptTitlePageCannotDeleteHomepageWithSuggestion => this[nameof (PromptTitlePageCannotDeleteHomepageWithSuggestion)];

    /// <summary>
    /// Title of the message shown when an invalid attempt to create a child page is made.
    /// </summary>
    [ResourceEntry("PromptTitlePageCannotCreateChildPage", Description = "Title of the message shown when an invalid attempt to create a child page is made.", LastModified = "2011/03/21", Value = "You cannot create a child page for this page")]
    public string PromptTitlePageCannotCreateChildPage => this[nameof (PromptTitlePageCannotCreateChildPage)];

    /// <summary>
    /// Message shown when you have invoked deletion of a given selection of paegs but not all were deleted due to some restriction.
    /// </summary>
    [ResourceEntry("PromptMessageSomePagesWereNotDeleted", Description = "Message shown when you have invoked deletion of a given selection of paegs but not all were deleted due to some restriction.", LastModified = "2011/02/23", Value = "The Homepage and its parent pages were not deleted. Before doing so you have to set another page as a Homepage.")]
    public string PromptMessageSomePagesWereNotDeleted => this[nameof (PromptMessageSomePagesWereNotDeleted)];

    /// <summary>
    /// Title of the message shown when you have invoked deletion of a given selection of paegs but not all were deleted due to some restriction.
    /// </summary>
    [ResourceEntry("PromptTitleSomePagesWereNotDeleted", Description = "Title of the message shown when you have invoked deletion of a given selection of paegs but not all were deleted due to some restriction.", LastModified = "2011/02/23", Value = "Some pages were not deleted.")]
    public string PromptTitleSomePagesWereNotDeleted => this[nameof (PromptTitleSomePagesWereNotDeleted)];

    /// <summary>
    /// The message of the warning dialog displpayed when deleting single template that is used by pages or by other templates.
    /// </summary>
    [ResourceEntry("PromptMessageSingleTemplateInUse", Description = "The message of the warning dialog displpayed when deleting single template that is used by pages or by other templates.", LastModified = "2010/09/30", Value = "<strong>{0} pages and {1} templates</strong> use this template.<br/><br/>Before you are able to delete the template, you must select another template for the pages or templates that use it.")]
    public string PromptMessageSingleTemplateInUse => this[nameof (PromptMessageSingleTemplateInUse)];

    /// <summary>
    /// The message of the warning dialog displpayed when deleting batch templates that are used by other pages or by other temlates.
    /// </summary>
    [ResourceEntry("PromptMessageBatchDeleteNotAllowed", Description = " The message of the warning dialog displpayed when deleting batch templates that are used by other pages or by other temlates.", LastModified = "2010/09/30", Value = "<strong>Some templates you have selected are used by pages or templates.</strong><br /><br/>Before you are able to delete a template, you must select another template for the pages or templates that use it.")]
    public string PromptMessageBatchDeleteNotAllowed => this[nameof (PromptMessageBatchDeleteNotAllowed)];

    /// <summary>
    /// Message displayed when a page is locked displayed by browse and edit.
    /// </summary>
    [ResourceEntry("PageIsLockedBy", Description = "Message displayed when a page is locked displayed by browse and edit", LastModified = "2010/12/16", Value = "The page is locked by {0}")]
    public string PageIsLockedBy => this[nameof (PageIsLockedBy)];

    /// <summary>
    /// Message displayed when a page is locked displayed by browse and edit.
    /// </summary>
    [ResourceEntry("PageHasBeenChangedInBackend", Description = "Message displayed when a page has been changed after loaded for browse & edit", LastModified = "2010/12/16", Value = "The page has been changed in the backend.")]
    public string PageHasBeenChangedInBackend => this[nameof (PageHasBeenChangedInBackend)];

    /// <summary>
    /// Message displayed when a page is locked displayed by browse and edit.
    /// </summary>
    [ResourceEntry("PageHasBeenDeletedInBackend", Description = "Message displayed when a page has been deleted after loaded for browse & edit", LastModified = "2010/12/16", Value = "The page has been deleted in the backend.")]
    public string PageHasBeenDeletedInBackend => this[nameof (PageHasBeenDeletedInBackend)];

    /// <summary>
    /// The message of the warning dialog when reverting a sycned page.
    /// </summary>
    /// <value><p>You are about to revert this item to an older version where some of its translations do not exist.</p><p>Are you sure you want to revert to this version? </p></value>
    [ResourceEntry("PromptRevertVersion", Description = "The message of the warning dialog when reverting a sycned page.", LastModified = "2015/03/26", Value = "<p>You are about to revert this item to an older version where some of its translations do not exist.</br>If you revert to this version, these translations will be lost.</p><p>Are you sure you want to revert to this version? </p>")]
    public string PromptRevertVersion => this[nameof (PromptRevertVersion)];

    [ResourceEntry("ExistingTranslations", Description = "existing translations", LastModified = "2020/02/07", Value = "translations")]
    public string ExistingTranslations => this[nameof (ExistingTranslations)];

    /// <summary>The title of the button in the revert prompt dialog.</summary>
    /// <value>Yes, Revert to this version</value>
    [ResourceEntry("PromptRevertButtonText", Description = "The title of the button in the revert prompt dialog.", LastModified = "2015/03/26", Value = "Yes, Revert to this version")]
    public string PromptRevertButtonText => this[nameof (PromptRevertButtonText)];

    /// <summary>phrase: Copy widget settings from another translation</summary>
    [ResourceEntry("CopyWidgetSettings", Description = "phrase: Copy widget settings from another translation", LastModified = "2011/05/19", Value = "Copy widget settings from another translation")]
    public string CopyWidgetSettings => this[nameof (CopyWidgetSettings)];

    /// <summary>phrase: Settings copied successfully.</summary>
    [ResourceEntry("SettingsCopiedSuccessfully", Description = "phrase: Settings copied successfully.", LastModified = "2020/07/27", Value = "Settings copied successfully.")]
    public string SettingsCopiedSuccessfully => this[nameof (SettingsCopiedSuccessfully)];

    /// <summary>phrase: Settings pasted successfully.</summary>
    [ResourceEntry("SettingsPastedSuccessfully", Description = "phrase: Settings pasted successfully.", LastModified = "2020/07/27", Value = "Settings pasted successfully.")]
    public string SettingsPastedSuccessfully => this[nameof (SettingsPastedSuccessfully)];

    /// <summary>phrase: Caching options</summary>
    [ResourceEntry("CachingOptions", Description = "phrase: Caching options", LastModified = "2011/03/30", Value = "Caching options")]
    public string CachingOptions => this[nameof (CachingOptions)];

    /// <summary>phrase: As set for the whole site</summary>
    [ResourceEntry("AsForWholeSite", Description = "phrase: As set for the whole site", LastModified = "2011/03/31", Value = "As set for the whole site")]
    public string AsForWholeSite => this[nameof (AsForWholeSite)];

    /// <summary>
    /// phrase: All language versions of the template will be synced as one template
    /// </summary>
    [ResourceEntry("PromptMessageTranslationSyncedWarning", Description = "phrase: All language versions of the template will be synced as one template", LastModified = "2011/04/28", Value = "All language versions of the template will be synced as one template. Changes made to widgets, layouts and base templates in one template will be reflected to all language versions of the template.")]
    public string PromptMessageTranslationSyncedWarning => this[nameof (PromptMessageTranslationSyncedWarning)];

    /// <summary>phrase: All translations of this template are synced</summary>
    [ResourceEntry("PromptTitleTranslationSyncedWarning", Description = "phrase: All translations of this template are synced", LastModified = "2011/04/28", Value = "All translations of this template are synced")]
    public string PromptTitleTranslationSyncedWarning => this[nameof (PromptTitleTranslationSyncedWarning)];

    /// <summary>Backend page: Help and Resources</summary>
    [ResourceEntry("HelpAndResources", Description = "phrase: Help & Resources", LastModified = "2011/07/25", Value = "Help &amp; Resources")]
    public string HelpAndResources => this[nameof (HelpAndResources)];

    /// <summary>Gets External Link: Help and Resources</summary>
    [ResourceEntry("ExternalLinkHelpAndResources", Description = "External Link: Help and Resources", LastModified = "2018/10/23", Value = "https://www.progress.com/documentation/sitefinity-cms?ref=sf-dashboard")]
    public string ExternalLinkHelpAndResources => this[nameof (ExternalLinkHelpAndResources)];

    /// <summary>Backend page: Help and Resources</summary>
    [ResourceEntry("HelpAndResourcesUrlName", Description = "HelpAndResources", LastModified = "2011/07/25", Value = "HelpAndResources")]
    public string HelpAndResourcesUrlName => this[nameof (HelpAndResourcesUrlName)];

    /// <summary>Backend page: Help and Resources</summary>
    [ResourceEntry("HelpAndResourcesTitle", Description = "Help &amp; Resources", LastModified = "2011/07/25", Value = "Help &amp; Resources")]
    public string HelpAndResourcesTitle => this[nameof (HelpAndResourcesTitle)];

    /// <summary>Backend page: Help and Resources</summary>
    [ResourceEntry("HelpAndResourcesHtmlTitle", Description = "Help &amp; Resources", LastModified = "2011/07/25", Value = "Help &amp; Resources")]
    public string HelpAndResourcesHtmlTitle => this[nameof (HelpAndResourcesHtmlTitle)];

    /// <summary>Backend page: Help and Resources</summary>
    [ResourceEntry("HelpAndResourcesDescription", Description = "Help &amp; Resources", LastModified = "2011/07/25", Value = "Help &amp; Resources")]
    public string HelpAndResourcesDescription => this[nameof (HelpAndResourcesDescription)];

    /// <summary>phrase: Canonical Url</summary>
    [ResourceEntry("CanonicalUrl", Description = "phrase: Canonical Url", LastModified = "2013/05/17", Value = "Canonical Url")]
    public string CanonicalUrl => this[nameof (CanonicalUrl)];

    /// <summary>phrase: Enabled</summary>
    [ResourceEntry("CanonicalUrlEnabled", Description = "phrase: Enabled", LastModified = "2013/05/17", Value = "Enabled")]
    public string CanonicalUrlEnabled => this[nameof (CanonicalUrlEnabled)];

    /// <summary>phrase: Disabled</summary>
    [ResourceEntry("CanonicalUrlDisabled", Description = "phrase: Disabled", LastModified = "2013/05/17", Value = "Disabled")]
    public string CanonicalUrlDisabled => this[nameof (CanonicalUrlDisabled)];

    /// <summary>
    /// Text used in content blocks grid to show that a content block is not used on any page
    /// </summary>
    /// <value>Translated version of 'Not used'</value>
    [ResourceEntry("ContentBlockNotUsedViewInstructions", Description = "Text used in content blocks grid to show that a content block is not used on any page", LastModified = "2011/07/25", Value = "Not used")]
    public string ContentBlockNotUsedViewInstructions => this[nameof (ContentBlockNotUsedViewInstructions)];

    /// <summary>
    /// Text used in content blocks grid to open a dialog of pages using that content block
    /// </summary>
    /// <value>Translated version of 'Used'</value>
    [ResourceEntry("ContentBlockIsUsedViewInstructions", Description = "Text used in content blocks grid to open a dialog of pages using that content block", LastModified = "2011/07/25", Value = "Used")]
    public string ContentBlockIsUsedViewInstructions => this[nameof (ContentBlockIsUsedViewInstructions)];

    /// <summary>
    /// Text indicating that a content block is not used in the grid of the shared content selector of the content block designer
    /// </summary>
    /// <value>Translated version of 'Not used on any page'</value>
    [ResourceEntry("ContentBlockNotUsed", Description = "Text indicating that a content block is not used in the grid of the shared content selector of the content block designer", LastModified = "2011/07/25", Value = "Not used on any page")]
    public string ContentBlockNotUsed => this[nameof (ContentBlockNotUsed)];

    /// <summary>
    /// Text indicating that a content block is used in the grid of the shared content selector of the content block designer
    /// </summary>
    /// <value>Translated version of 'Used on at least one page'</value>
    [ResourceEntry("ContentBlockIsUsed", Description = "Text indicating that a content block is used in the grid of the shared content selector of the content block designer", LastModified = "2011/07/25", Value = "Used on at least one page")]
    public string ContentBlockIsUsed => this[nameof (ContentBlockIsUsed)];

    /// <summary>phrase: Allow parameter validation</summary>
    [ResourceEntry("AllowParameterValidation", Description = "phrase: Allow parameter validation", LastModified = "2011/09/14", Value = "Allow parameter validation")]
    public string AllowParameterValidation => this[nameof (AllowParameterValidation)];

    /// <summary>phrase: Site Sync</summary>
    [ResourceEntry("Synchronization", Description = "The 'Site Sync' link text in Basic Settings sidebar.", LastModified = "2011/12/14", Value = "Site Sync")]
    public string Synchronization => this[nameof (Synchronization)];

    /// <summary>Word: User authentication</summary>
    [ResourceEntry("UserAuthentication", Description = "The 'User authentication' link text in Basic Settings sidebar.", LastModified = "2011/11/24", Value = "User Authentication")]
    public string UserAuthentication => this[nameof (UserAuthentication)];

    /// <summary>Login node.</summary>
    [ResourceEntry("LoginNodeUrlName", Description = "The name of the Login node that appears in the URL.", LastModified = "2011/12/01", Value = "LoginPages")]
    public string LoginNodeUrlName => this[nameof (LoginNodeUrlName)];

    /// <summary>Login node.</summary>
    [ResourceEntry("LoginNodeTitle", Description = "The title of the Login node.", LastModified = "2011/12/01", Value = "Login")]
    public string LoginNodeTitle => this[nameof (LoginNodeTitle)];

    /// <summary>Login node.</summary>
    [ResourceEntry("LoginNodeDescription", Description = "Description of Login node.", LastModified = "2011/12/01", Value = "Login pages for the backend.")]
    public string LoginNodeDescription => this[nameof (LoginNodeDescription)];

    /// <summary>Login form page.</summary>
    [ResourceEntry("LoginFormUrlName", Description = "The name of the Login form page that appears in the URL.", LastModified = "2011/12/01", Value = "LoginForm")]
    public string LoginFormUrlName => this[nameof (LoginFormUrlName)];

    /// <summary>Login form page.</summary>
    [ResourceEntry("LoginFormTitle", Description = "The title of the Login form page.", LastModified = "2011/12/02", Value = "Login")]
    public string LoginFormTitle => this[nameof (LoginFormTitle)];

    /// <summary>Login form page.</summary>
    [ResourceEntry("LoginFormDescription", Description = "Description of Login form page.", LastModified = "2011/12/01", Value = "Login form for the backend.")]
    public string LoginFormDescription => this[nameof (LoginFormDescription)];

    /// <summary>User Limit Reached page</summary>
    [ResourceEntry("UserLimitReachedUrlName", Description = "The name of the User limit reached page that appears in the URL.", LastModified = "2011/12/02", Value = "UserLimitReached")]
    public string UserLimitReachedUrlName => this[nameof (UserLimitReachedUrlName)];

    /// <summary>User Limit Reached page</summary>
    [ResourceEntry("UserLimitReachedTitle", Description = "The title of the User Limit Reached page.", LastModified = "2011/12/02", Value = "User Limit Reached")]
    public string UserLimitReachedTitle => this[nameof (UserLimitReachedTitle)];

    /// <summary>User Limit Reached page</summary>
    [ResourceEntry("UserLimitReachedDescription", Description = "Description of User limit reached page.", LastModified = "2011/12/02", Value = "Page that handles the User limit reached message upon login.")]
    public string UserLimitReachedDescription => this[nameof (UserLimitReachedDescription)];

    /// <summary>User Already Logged In page</summary>
    [ResourceEntry("UserAlreadyLoggedInUrlName", Description = "The name of the User Already Logged In page that appears in the URL.", LastModified = "2011/12/05", Value = "UserAlreadyLoggedIn")]
    public string UserAlreadyLoggedInUrlName => this[nameof (UserAlreadyLoggedInUrlName)];

    /// <summary>User Already Logged In page</summary>
    [ResourceEntry("UserAlreadyLoggedInTitle", Description = "The title of the User Already Logged In page.", LastModified = "2011/12/05", Value = "User Already Logged In")]
    public string UserAlreadyLoggedInTitle => this[nameof (UserAlreadyLoggedInTitle)];

    /// <summary>User Already Logged In page</summary>
    [ResourceEntry("UserAlreadyLoggedInDescription", Description = "Description of User Already Logged In page.", LastModified = "2011/12/05", Value = "Page that handles the User Already Logged In message upon login.")]
    public string UserAlreadyLoggedInDescription => this[nameof (UserAlreadyLoggedInDescription)];

    /// <summary>Need Admin Rights In page</summary>
    [ResourceEntry("NeedAdminRightsUrlName", Description = "The name of the Need Admin Rights page that appears in the URL.", LastModified = "2011/12/19", Value = "NeedAdminRights")]
    public string NeedAdminRightsUrlName => this[nameof (NeedAdminRightsUrlName)];

    /// <summary>Need Admin Rights In page</summary>
    [ResourceEntry("NeedAdminRightsTitle", Description = "The title of the Need Admin Rights page.", LastModified = "2011/12/19", Value = "Need Admin Rights")]
    public string NeedAdminRightsTitle => this[nameof (NeedAdminRightsTitle)];

    /// <summary>Need Admin Rights In page</summary>
    [ResourceEntry("NeedAdminRightsDescription", Description = "Description of Need Admin Rights page.", LastModified = "2011/12/19", Value = "Page that handles the Need Admin Rights message.")]
    public string NeedAdminRightsDescription => this[nameof (NeedAdminRightsDescription)];

    /// <summary>Site Not Accessible In page</summary>
    [ResourceEntry("SiteNotAccessibleUrlName", Description = "The name of the Site Not Accessible page that appears in the URL.", LastModified = "2018/02/08", Value = "SiteNotAccessible")]
    public string SiteNotAccessibleUrlName => this[nameof (SiteNotAccessibleUrlName)];

    /// <summary>Site Not Accessible In page</summary>
    [ResourceEntry("SiteNotAccessibleTitle", Description = "The title of the Site Not Accessible page.", LastModified = "2018/02/08", Value = "Site Not Accessible")]
    public string SiteNotAccessibleTitle => this[nameof (SiteNotAccessibleTitle)];

    /// <summary>Site Not Accessible In page</summary>
    [ResourceEntry("SiteNotAccessibleDescription", Description = "Description of Site Not Accessible page.", LastModified = "2018/02/08", Value = "Page that handles the Site Not Accessible message.")]
    public string SiteNotAccessibleDescription => this[nameof (SiteNotAccessibleDescription)];

    /// <summary>phrase: Text editor</summary>
    [ResourceEntry("TextEditor", Description = "The 'Text editor' link text in Basic Settings sidebar.", LastModified = "2011/11/22", Value = "Text editor")]
    public string TextEditor => this[nameof (TextEditor)];

    /// <summary>
    /// Error message: The specified template has other templates based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing templates: {0}
    /// </summary>
    [ResourceEntry("TemplateUsedByOTherTemplates", Description = "Error message: The specified template has other templates based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing templates: {0}", LastModified = "2012/03/14", Value = "The specified template has other templates based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing templates: {0}")]
    public string TemplateUsedByOTherTemplates => this[nameof (TemplateUsedByOTherTemplates)];

    /// <summary>
    /// Error message: The specified template has pages based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing pages: {0}
    /// </summary>
    [ResourceEntry("TemplateUsedByPages", Description = "Error message: The specified template has pages based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing pages: {0}", LastModified = "2012/03/14", Value = "The specified template has pages based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing pages: {0}")]
    public string TemplateUsedByPages => this[nameof (TemplateUsedByPages)];

    /// <summary>
    /// Error message: The specified template has email campaigns based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing campaigns: {0}
    /// </summary>
    [ResourceEntry("TemplateUsedByCampaigns", Description = "Error message: The specified template has email campaigns based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing campaigns: {0}", LastModified = "2012/03/14", Value = "The specified template has email campaigns based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing campaigns: {0}")]
    public string TemplateUsedByCampaigns => this[nameof (TemplateUsedByCampaigns)];

    /// <summary>
    /// Error message: The specified template has pages and email campaigns based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing pages/campaigns: {0}
    /// </summary>
    [ResourceEntry("TemplateUsedByPagesAndCampaigns", Description = "Error message: The specified template has pages and email campaigns based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing pages/campaigns: {0}", LastModified = "2012/03/14", Value = "The specified template has pages and email campaigns based on it. You can only delete a template when no pages, email campaigns or templates reference it. Referencing pages/campaigns: {0}")]
    public string TemplateUsedByPagesAndCampaigns => this[nameof (TemplateUsedByPagesAndCampaigns)];

    /// <summary>phrase: Template (You can change it later)</summary>
    [ResourceEntry("PageTemplateYouCanChangeItLater", Description = "phrase: Template (You can change it later)", LastModified = "2012/04/05", Value = "Template <span class='sfLblNote'>(You can change it later)</span>")]
    public string PageTemplateYouCanChangeItLater => this[nameof (PageTemplateYouCanChangeItLater)];

    /// <summary>Title of the Web framework setting</summary>
    [ResourceEntry("WebFrameworkTitle", Description = "Title of the Web framework setting", LastModified = "2012/06/25", Value = "Web framework")]
    public string WebFrameworkTitle => this[nameof (WebFrameworkTitle)];

    /// <summary>Description of the Web framework setting</summary>
    [ResourceEntry("WebFrameworkDescription", Description = "Description of the Web framework setting", LastModified = "2012/06/25", Value = "What type of widgets will be available in the toolbox")]
    public string WebFrameworkDescription => this[nameof (WebFrameworkDescription)];

    /// <summary>Title of the hybrid mode of page template</summary>
    [ResourceEntry("WebFormsAndMVC", Description = "Title of the hybrid mode of page template", LastModified = "2012/06/25", Value = "Web Forms and MVC (hybrid)")]
    public string WebFormsAndMVC => this[nameof (WebFormsAndMVC)];

    /// <summary>Title of the Web Forms only mode of page template</summary>
    [ResourceEntry("WebFormsOnly", Description = "Title of the Web Forms only mode of page template", LastModified = "2012/06/25", Value = "Web Forms only")]
    public string WebFormsOnly => this[nameof (WebFormsOnly)];

    /// <summary>MVC only</summary>
    [ResourceEntry("MVCOnly", Description = "Title of the MVC only mode of page template", LastModified = "2012/06/25", Value = "MVC only")]
    public string MVCOnly => this[nameof (MVCOnly)];

    /// <summary>Phrase: Google Maps</summary>
    [ResourceEntry("GoogleMaps", Description = "The 'Google Maps' link text in Basic Settings sidebar.", LastModified = "2013/03/27", Value = "Google Maps")]
    public string GoogleMaps => this[nameof (GoogleMaps)];

    /// <summary>
    /// Message displayed when the user navigates away from the Page Editor with unsaved changes
    /// </summary>
    /// <value>By navigating away from the editor your changes will not be canceled and the item will remain locked. You can use 'Back to Pages/Templates' link to leave and unlock.</value>
    [ResourceEntry("PromptNavigateFromEditorToolBar", Description = "Message displayed when the user navigates away from the Page Editor with unsaved changes", LastModified = "2013/06/27", Value = "By navigating away from the editor your changes will not be canceled and the item will remain locked. You can use 'Back to Pages/Templates' link to leave and unlock. ")]
    public string PromptNavigateFromEditorToolBar => this[nameof (PromptNavigateFromEditorToolBar)];

    /// <summary>
    /// Message displayed when the user navigates away from the Page Editor when the page is locked
    /// </summary>
    /// <value>By navigating away from the editor the item will remain locked. You can use 'Back to Pages/Templates' link to leave and unlock.</value>
    [ResourceEntry("PromptNavigateFromEditorToolBarLocked", Description = "Message displayed when the user navigates away from the Page Editor when the page is locked", LastModified = "2013/06/27", Value = "By navigating away from the editor the item will remain locked. You can use 'Back to Pages/Templates' link to leave and unlock.")]
    public string PromptNavigateFromEditorToolBarLocked => this[nameof (PromptNavigateFromEditorToolBarLocked)];

    /// <summary>
    /// Message displayed when the user leaves the page editor.
    /// </summary>
    /// <value>Are you sure you want to leave the editor?</value>
    [ResourceEntry("PromptLeavePageEditor", Description = "Message displayed when the user leaves the page editor.", LastModified = "2013/06/27", Value = "Are you sure you want to leave the editor?")]
    public string PromptLeavePageEditor => this[nameof (PromptLeavePageEditor)];

    /// <summary>Phrase: Responsive design transformations</summary>
    [ResourceEntry("ResponsiveDesignTransformations", Description = "The 'Responsive design transformations' link text in Basic Settings sidebar.", LastModified = "2013/06/18", Value = "Responsive design")]
    public string ResponsiveDesignTransformations => this[nameof (ResponsiveDesignTransformations)];

    /// <summary>The title of the new dashboards group page</summary>
    /// <value>Dashboards</value>
    [ResourceEntry("DashboardGroupPageTitle", Description = "The title of the new dashboards group page", LastModified = "2013/09/12", Value = "Dashboard")]
    public string DashboardGroupPageTitle => this[nameof (DashboardGroupPageTitle)];

    /// <summary>The url name of the dashboards group page</summary>
    /// <value>dashboards</value>
    [ResourceEntry("DashboardGroupPageUrlName", Description = "The url name of the dashboards group page", LastModified = "2013/09/20", Value = "dashboard")]
    public string DashboardGroupPageUrlName => this[nameof (DashboardGroupPageUrlName)];

    /// <summary>A description for the dashboards group page</summary>
    /// <value>A dashboard group page which contains the default dashboard and some custom Sitefinity dashboards</value>
    [ResourceEntry("DashboardGroupPageDescription", Description = "A description for the dashboards group page", LastModified = "2013/09/12", Value = "A dashboard group page which contains the default dashboard and some custom Sitefinity dashboards")]
    public string DashboardGroupPageDescription => this[nameof (DashboardGroupPageDescription)];

    /// <summary>The url name of the legacy dashboard</summary>
    /// <value>legacy-dashboard</value>
    [ResourceEntry("LegacyDashboardUrlName", Description = "The url name of the legacy dashboard", LastModified = "2013/09/18", Value = "legacy-dashboard")]
    public string LegacyDashboardUrlName => this[nameof (LegacyDashboardUrlName)];

    /// <summary>The title of the legacy dashboard.</summary>
    /// <value>Legacy Dashboard</value>
    [ResourceEntry("LegacyDashboardTitle", Description = "The title of the legacy dashboard.", LastModified = "2013/09/18", Value = "Legacy Dashboard")]
    public string LegacyDashboardTitle => this[nameof (LegacyDashboardTitle)];

    /// <summary>The HTML title of the legacy dashboard.</summary>
    /// <value>Legacy Dashboard</value>
    [ResourceEntry("LegacyDashboardHtmlTitle", Description = "The HTML title of the legacy dashboard.", LastModified = "2013/09/18", Value = "Legacy Dashboard")]
    public string LegacyDashboardHtmlTitle => this[nameof (LegacyDashboardHtmlTitle)];

    /// <summary>The title of the Connectivity section.</summary>
    /// <value>Connectivity</value>
    [ResourceEntry("ConnectivityNodeTitle", Description = "The title of the Connectivity section.", LastModified = "2013/10/31", Value = "Connectivity")]
    public string ConnectivityNodeTitle => this[nameof (ConnectivityNodeTitle)];

    /// <summary>
    /// The name of the Connectivity node that appears in the URL.
    /// </summary>
    /// <value>Connectivity</value>
    [ResourceEntry("ConnectivityNodeUrlName", Description = "The name of the Connectivity node that appears in the URL.", LastModified = "2013/10/31", Value = "Connectivity")]
    public string ConnectivityNodeUrlName => this[nameof (ConnectivityNodeUrlName)];

    /// <summary>The description of the Connectivity section.</summary>
    /// <value>Groups custom connectors and connectivity tools.</value>
    [ResourceEntry("ConnectivityNodeDescription", Description = "The description of the Connectivity section.", LastModified = "2013/10/31", Value = "Groups custom connectors and connectivity tools.")]
    public string ConnectivityNodeDescription => this[nameof (ConnectivityNodeDescription)];

    /// <summary>phrase: Restore to default structure</summary>
    /// <value>Restore to default structure</value>
    [ResourceEntry("RestoreToDefaultStructure", Description = "phrase: Restore to default structure", LastModified = "2013/10/28", Value = "Restore to default structure")]
    public string RestoreToDefaultStructure => this[nameof (RestoreToDefaultStructure)];

    /// <summary>phrase: Create custom URL</summary>
    /// <value>Create custom URL</value>
    [ResourceEntry("CreateCustomUrl", Description = "phrase: Edit URL structure", LastModified = "2013/11/18", Value = "Edit URL structure")]
    public string CreateCustomUrl => this[nameof (CreateCustomUrl)];

    /// <summary>phrase: (Custom)</summary>
    /// <value>(Custom)</value>
    [ResourceEntry("CustomTextInBrackets", Description = "phrase: (Custom)", LastModified = "2013/10/28", Value = "(Custom)")]
    public string CustomTextInBrackets => this[nameof (CustomTextInBrackets)];

    /// <summary>
    /// The label for the site and language selector when duplicating a page.
    /// </summary>
    [ResourceEntry("SiteAndLanguageSelectorFieldTitle", Description = "The label for the site and language selector when duplicating a page.", LastModified = "2014/09/02", Value = "Site & Language")]
    public string SiteAndLanguageSelectorFieldTitle => this[nameof (SiteAndLanguageSelectorFieldTitle)];

    /// <summary>
    /// The label that is displayed for the language selector when duplicating a page.
    /// </summary>
    [ResourceEntry("DuplicatePageToLanguageTitle", Description = "The label that is displayed for the language selector when duplicating a page.", LastModified = "2014/09/08", Value = "Language")]
    public string DuplicatePageToLanguageTitle => this[nameof (DuplicatePageToLanguageTitle)];

    /// <summary>
    /// The tooltip message that appears next to the language when you duplicate a page.
    /// </summary>
    [ResourceEntry("DuplicatePageToLanguageTooltip", Description = "The tooltip message that appears next to the language when you duplicate a page.", LastModified = "2019/05/29", Value = "You are able to duplicate this page in a different site than the current one. You can also duplicate the page to a different language. E.g.English translation can be duplicated to French.")]
    public string DuplicatePageToLanguageTooltip => this[nameof (DuplicatePageToLanguageTooltip)];

    /// <summary>The title of the duplicate field.</summary>
    [ResourceEntry("DuplicateTo", Description = "The title of the duplicate field.", LastModified = "2019/05/29", Value = "Duplicate to...")]
    public string DuplicateTo => this[nameof (DuplicateTo)];

    /// <summary>
    /// A generic title for links used to show contextual help.
    /// </summary>
    [ResourceEntry("WhatsThis", Description = "A generic title for links used to show contextual help.", LastModified = "2014/09/02", Value = "What's this?")]
    public string WhatsThis => this[nameof (WhatsThis)];

    /// <summary>
    /// The message that will be displayed when a duplicate page url has been found.
    /// </summary>
    [ResourceEntry("PageDuplicateMessage", Description = "The message that will be displayed when a duplicate page url has been found.", LastModified = "2013/11/07", Value = "The specified URL already exists in page: {0}")]
    public string PageDuplicateMessage => this[nameof (PageDuplicateMessage)];

    /// <summary>
    /// The message that will be displayed when a duplicate page url has been found in the child nodes.
    /// </summary>
    [ResourceEntry("ChildPageDuplicateMessage", Description = "The message that will be displayed when a duplicate page url has been found in the child nodes.", LastModified = "2013/12/05", Value = "You cannot save your changes, because a child page will have URL '{0}' that already exists.")]
    public string ChildPageDuplicateMessage => this[nameof (ChildPageDuplicateMessage)];

    /// <summary>
    /// Message displayed when a user enters a slash('/') in the urlname of the field and the url is not custom.
    /// </summary>
    /// <value>The URL contains invalid symbols. You can use slash ( / ) in the custom URL structure.</value>
    [ResourceEntry("CustomUrlValidationMessage", Description = "Message displayed when a user enters a slash('/') in the urlname of the field and the url is not custom.", LastModified = "2013/11/22", Value = "The URL contains invalid symbols. You can use slash ( / ) in the custom URL structure.")]
    public string CustomUrlValidationMessage => this[nameof (CustomUrlValidationMessage)];

    /// <summary>The description of this element</summary>
    /// <value>The validation message that shows up when the normal url contains slashes ('/')</value>
    [ResourceEntry("CustomUrlValidationMessageDescription", Description = "The description of this element", LastModified = "2013/11/22", Value = "The validation message that shows up when the normal url contains slashes ('/')")]
    public string CustomUrlValidationMessageDescription => this[nameof (CustomUrlValidationMessageDescription)];

    /// <summary>The description of this element.</summary>
    /// <value>The custom url validation message</value>
    [ResourceEntry("CustomUrlValidationMessageTitle", Description = "The description of this element.", LastModified = "2013/11/22", Value = "The custom url validation message")]
    public string CustomUrlValidationMessageTitle => this[nameof (CustomUrlValidationMessageTitle)];

    [ResourceEntry("ShareDialogCaption", Description = "", LastModified = "2014/01/23", Value = "Share secure link to this item")]
    public string ShareDialogCaption => this[nameof (ShareDialogCaption)];

    [ResourceEntry("ShareDialogLinkDescription", Description = "", LastModified = "2014/01/23", Value = "Link <em class='sfNote'>(paste in email or IM)</em>")]
    public string ShareDialogLinkDescription => this[nameof (ShareDialogLinkDescription)];

    [ResourceEntry("ShareDialogLinkExpirationDescription", Description = "", LastModified = "2014/01/23", Value = "Link will expire in {0}.")]
    public string ShareDialogLinkExpirationDescription => this[nameof (ShareDialogLinkExpirationDescription)];

    /// <summary>
    /// Message displayed when expiration time for shared links is out of range.
    /// </summary>
    /// <value>Expiration time for shared links is out of range.</value>
    [ResourceEntry("SharedLinkExpirationTimeOutOfRangeMessage", Description = "Message displayed when expiration time for shared links is out of range.", LastModified = "2014/01/31", Value = "Expiration time for shared links is out of range.")]
    public string SharedLinkExpirationTimeOutOfRangeMessage => this[nameof (SharedLinkExpirationTimeOutOfRangeMessage)];

    /// <summary>word: Site</summary>
    [ResourceEntry("Site", Description = "word: Site", LastModified = "2014/02/27", Value = "Site")]
    public string Site => this[nameof (Site)];

    /// <summary>The description of this element.</summary>
    /// <value>Drag widgets here</value>
    [ResourceEntry("ZoneEditorEmptyZoneContentDragMessage", Description = "The description of this element.", LastModified = "2014/02/24", Value = "Drag widgets here")]
    public string ZoneEditorEmptyZoneContentDragMessage => this[nameof (ZoneEditorEmptyZoneContentDragMessage)];

    /// <summary>The description of this element.</summary>
    /// <value>Want to divide this box into columns?</value>
    [ResourceEntry("ZoneEditorEmptyZoneLayoutCaption", Description = "The description of this element.", LastModified = "2014/02/24", Value = "Want to divide this box into columns?")]
    public string ZoneEditorEmptyZoneLayoutCaption => this[nameof (ZoneEditorEmptyZoneLayoutCaption)];

    /// <summary>The description of this element.</summary>
    /// <value>Drag layout elements</value>
    [ResourceEntry("ZoneEditorEmptyZoneLayoutDragMessage", Description = "The description of this element.", LastModified = "2014/02/24", Value = "Drag layout elements")]
    public string ZoneEditorEmptyZoneLayoutDragMessage => this[nameof (ZoneEditorEmptyZoneLayoutDragMessage)];

    /// <summary>The description of this element.</summary>
    /// <value>Drop here</value>
    [ResourceEntry("ZoneEditorEmptyZoneDraggingText", Description = "The description of this element.", LastModified = "2014/02/25", Value = "Drop here")]
    public string ZoneEditorEmptyZoneDraggingText => this[nameof (ZoneEditorEmptyZoneDraggingText)];

    /// <summary>phrase: (Where to duplicate this page)</summary>
    [ResourceEntry("DuplicatePageSiteFieldDescription", Description = "phrase: (Where to duplicate this page)", LastModified = "2014/03/26", Value = "(Where to duplicate this page)")]
    public string DuplicatePageSiteFieldDescription => this[nameof (DuplicatePageSiteFieldDescription)];

    /// <summary>phrase: Duplicate child pages as well</summary>
    [ResourceEntry("DuplicateChildPages", Description = "phrase: Duplicate child pages as well", LastModified = "2014/03/26", Value = "Duplicate child pages as well")]
    public string DuplicateChildPages => this[nameof (DuplicateChildPages)];

    /// <summary>phrase: Duplicate a page</summary>
    [ResourceEntry("DuplicatePage", Description = "phrase: Duplicate a page", LastModified = "2014/03/11", Value = "Duplicate a page")]
    public string DuplicatePage => this[nameof (DuplicatePage)];

    /// <summary>
    /// Title of the message shown when an invalid attempt to create a child page is made.
    /// </summary>
    [ResourceEntry("PromptTitleDuplicatePageWithUnavailableCulture", Description = "Title of the message shown when an invalid attempt to duplicate a page in a site where the culture is unavailable is made.", LastModified = "2014/03/14", Value = "Cannot duplicate a page.")]
    public string PromptTitleDuplicatePageWithUnavailableCulture => this[nameof (PromptTitleDuplicatePageWithUnavailableCulture)];

    /// <summary>
    /// Message shown when you have invoked deletion of a given selection of paegs but not all were deleted due to some restriction.
    /// </summary>
    [ResourceEntry("PromptMessageDuplicatePageWithUnavailableCulture", Description = "Message shown when an invalid attempt to duplicate a page in a site where the culture is unavailable is made.", LastModified = "2014/03/14", Value = "The page cannot be duplicated because it is in translation that does not exist in the target site. To be able to duplicate it you need to go to the target site and enable the respective language translation there.")]
    public string PromptMessageDuplicatePageWithUnavailableCulture => this[nameof (PromptMessageDuplicatePageWithUnavailableCulture)];

    /// <summary>phrase: Allow site search to index this page</summary>
    [ResourceEntry("AllowSiteSearchToIndexThisPage", Description = "phrase: Allow site search to index this page", LastModified = "2014/03/12", Value = "Allow <strong> site search to index </strong> this page")]
    public string AllowSiteSearchToIndexThisPage => this[nameof (AllowSiteSearchToIndexThisPage)];

    /// <summary>Title of the Marketing tools section</summary>
    /// <value>Tools</value>
    [ResourceEntry("MarketingToolsTitle", Description = "Title of the Marketing tools section", LastModified = "2014/03/05", Value = "Tools")]
    public string MarketingToolsTitle => this[nameof (MarketingToolsTitle)];

    /// <summary>Url name of the Marketing tools section</summary>
    /// <value>tools</value>
    [ResourceEntry("MarketingToolsUrlName", Description = "Url name of the Marketing tools section", LastModified = "2014/03/05", Value = "tools")]
    public string MarketingToolsUrlName => this[nameof (MarketingToolsUrlName)];

    /// <summary>Description of the Marketing tools section</summary>
    /// <value>Section for Sitefinity marketing tools, such as personalization, analytics, etc.</value>
    [ResourceEntry("MarketingToolsDescription", Description = "Description of the Marketing tools section", LastModified = "2014/03/05", Value = "Section for Sitefinity marketing tools, such as personalization, analytics, etc.")]
    public string MarketingToolsDescription => this[nameof (MarketingToolsDescription)];

    /// <summary>phrase: this site</summary>
    /// <value>this site</value>
    [ResourceEntry("ThisSite", Description = "phrase: this site", LastModified = "2014/03/21", Value = "this site")]
    public string ThisSite => this[nameof (ThisSite)];

    /// <summary>phrase: Incorect characters. Only floats are allowed!</summary>
    [ResourceEntry("FloatOnlyViolationMessage", Description = "phrase: Only floats are allowed!", LastModified = "2014/05/09", Value = "Incorect characters. Only floats are allowed!")]
    public string FloatOnlyViolationmessage => this["FloatOnlyViolationMessage"];

    /// <summary>
    /// phrase: You have entered an invalid number. Valid values range from 0.0 to 1.0
    /// </summary>
    [ResourceEntry("PriorityRangeViolationMessage", Description = "phrase: You have entered an invalid number. Valid values range from 0.0 to 1.0", LastModified = "2014/05/09", Value = "You have entered an invalid number. Valid values range from 0.0 to 1.0")]
    public string PriorityRangeViolationMessage => this[nameof (PriorityRangeViolationMessage)];

    /// <summary>phrase: Priority in Sitemap</summary>
    [ResourceEntry("Priority", Description = "phrase: Priority in Sitemap", LastModified = "2014/05/09", Value = "Priority in Sitemap")]
    public string Priority => this[nameof (Priority)];

    /// <summary>phrase: Include in sitemap</summary>
    [ResourceEntry("IncludeInSitemap", Description = "phrase: Include in sitemap", LastModified = "2014/05/09", Value = "Include in sitemap")]
    public string IncludeInSitemap => this[nameof (IncludeInSitemap)];

    /// <summary>phrase: Include in sitemap</summary>
    [ResourceEntry("PriorityToolTipMessage", Description = "phrase: The priority lets the search engines know which pages in Sitemap you deem most important for the crawlers. Valid values range from 0.0 to 1.0. You can manage sitemap generation in Basic Settings", LastModified = "2014/05/09", Value = "The priority lets the search engines know which pages in Sitemap you deem most important for the crawlers. Valid values range from 0.0 to 1.0. You can manage sitemap generation in Basic Settings")]
    public string PriorityToolTipMessage => this[nameof (PriorityToolTipMessage)];

    /// <summary>The entered URL is invalid.</summary>
    /// <value>The entered URL is invalid.</value>
    [ResourceEntry("InvalidUrl", Description = "The entered URL is invalid.", LastModified = "2014/08/19", Value = "The entered URL is invalid.")]
    public string InvalidUrl => this[nameof (InvalidUrl)];

    /// <summary>The C# regular expression for URL validation.</summary>
    /// <value>The regular expression.</value>
    [ResourceEntry("UrlValidationRegex", Description = "The C# regular expression for URL validation.", LastModified = "2018/03/28", Value = "(?i)^(((https?|s?ftp):\\/\\/(((([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:)*@)?(((\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5]))|((([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?)(:\\d*)?)(\\/((([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)+(\\/(([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)*)*)?)?(\\?((([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)|[\\uE000-\\uF8FF]|\\/|\\?)*)?(#((([a-z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)|\\/|\\?)*)?$)|\\/[A-Za-z0-9\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF-._~!$&'+,;=:@#%\\/]*)")]
    public string UrlValidationRegex => this[nameof (UrlValidationRegex)];

    /// <summary>The JS regular expression for URL validation.</summary>
    /// <value>The regular expression.</value>
    [ResourceEntry("UrlValidationRegexJS", Description = "The JS regular expression for URL validation.", LastModified = "2018/05/03", Value = "^((https?|s?ftp):\\/\\/(((([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:)*@)?(((\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5])\\.(\\d|[1-9]\\d|1\\d\\d|2[0-4]\\d|25[0-5]))|((([a-zA-Z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-zA-Z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-zA-Z]|\\d|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.)+(([a-zA-Z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(([a-zA-Z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])*([a-zA-Z]|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])))\\.?)(:\\d*)?)(\\/((([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)+(\\/(([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)*)*)?)?(\\?((([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)|[\\uE000-\\uF8FF]|\\/|\\?)*)?(#((([a-zA-Z]|\\d|-|\\.|_|~|[\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF])|(%[\\da-f]{2})|[!\\$&'\\(\\)\\*\\+,;=]|:|@)|\\/|\\?)*)?$|\\/[A-Za-z0-9\\u00A0-\\uD7FF\\uF900-\\uFDCF\\uFDF0-\\uFFEF-._~!$&'+,;=:@#%\\/]*)")]
    public string UrlValidationRegexJS => this[nameof (UrlValidationRegexJS)];

    /// <summary>Label: Azure Search</summary>
    /// <value>Azure Search</value>
    [ResourceEntry("Search", Description = "Label: Search", LastModified = "2014/10/10", Value = "Search")]
    public string Search => this[nameof (Search)];

    /// <summary>Word</summary>
    /// <value>Compilation</value>
    [ResourceEntry("CompilationElementTitle", Description = "Word", LastModified = "2015/02/05", Value = "Compilation")]
    public string CompilationElementTitle => this[nameof (CompilationElementTitle)];

    /// <summary>Settings for precompiling pages.</summary>
    /// <value>Settings for precompiling pages.</value>
    [ResourceEntry("CompilationElementDescription", Description = "Settings for precompiling pages.", LastModified = "2015/02/05", Value = "Settings for precompiling pages.")]
    public string CompilationElementDescription => this[nameof (CompilationElementDescription)];

    /// <summary>Word</summary>
    /// <value>Authentication key</value>
    [ResourceEntry("AuthKeyTitle", Description = "Word", LastModified = "2015/02/05", Value = "Authentication key")]
    public string AuthKeyTitle => this[nameof (AuthKeyTitle)];

    /// <summary>
    /// The authentication key, which the Sitefinity precompiler tool will use to authenticate to Sitefinity.
    /// </summary>
    /// <value>The authentication key, which the Sitefinity precompiler tool will use to authenticate to Sitefinity.</value>
    [ResourceEntry("AuthKeyDescription", Description = "The authentication key, which the Sitefinity precompiler tool will use to authenticate to Sitefinity.", LastModified = "2015/02/05", Value = "The authentication key, which the Sitefinity precompiler tool will use to authenticate to Sitefinity.")]
    public string AuthKeyDescription => this[nameof (AuthKeyDescription)];

    /// <summary>Phrase: Name for developers</summary>
    [ResourceEntry("NameForDevelopers", Description = "Phrase: Name for developers", LastModified = "2015/07/09", Value = "Name for developers")]
    public string NameForDevelopers => this[nameof (NameForDevelopers)];

    /// <summary>Phrase: Name for developers</summary>
    [ResourceEntry("DeveloperNameLabel", Description = "Phrase: Name for developers", LastModified = "2013/06/14", Value = "Developer name <em class=\"sfLightTxt sfFontNormal\">(used in code)</em> ")]
    public string DeveloperNameLabel => this[nameof (DeveloperNameLabel)];

    /// <summary>Log compilation title.</summary>
    /// <value>Log compilation</value>
    [ResourceEntry("LogCompilationTitle", Description = "Log compilation title", LastModified = "2016/04/26", Value = "Log compilation")]
    public string LogCompilationTitle => this[nameof (LogCompilationTitle)];

    /// <summary>Log compilation description.</summary>
    /// <value>Indicates whether logging of pages compilation is turned on. The log shows compilation time and page information.</value>
    [ResourceEntry("LogCompilationDescription", Description = "Log compilation description.", LastModified = "2016/04/26", Value = "Indicates whether logging of pages compilation is turned on. The log shows compilation time and page information.")]
    public string LogCompilationDescription => this[nameof (LogCompilationDescription)];

    /// <summary>label: Login failed</summary>
    /// <value>Login failed</value>
    [ResourceEntry("LoginFailedTitle", Description = "label: Login failed", LastModified = "2017/02/02", Value = "Login failed")]
    public string LoginFailedTitle => this[nameof (LoginFailedTitle)];

    /// <summary>Phrase: Tracking consent</summary>
    [ResourceEntry("TrackingConsent", Description = "The 'Tracking consent' link text in Basic Settings sidebar.", LastModified = "2017/07/18", Value = "Tracking consent")]
    public string TrackingConsent => this[nameof (TrackingConsent)];

    /// <summary>Phrase: deleted page</summary>
    [ResourceEntry("DeletedPage", Description = "Phrase: deleted page", LastModified = "2017/09/20", Value = "deleted page")]
    public string DeletedPage => this[nameof (DeletedPage)];

    /// <summary>Gets External Link: Create page layout</summary>
    [ResourceEntry("ExternalLinkCreatePageLayout", Description = "External Link: Create page layout", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/design-page-templates-in-the-layout-editor")]
    public string ExternalLinkCreatePageLayout => this[nameof (ExternalLinkCreatePageLayout)];

    /// <summary>Gets External Link: Create page with widgets</summary>
    [ResourceEntry("ExternalLinkCreatePageWithWidgets", Description = "External Link: Create page with widgets", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/add-widgets-on-pages-and-templates")]
    public string ExternalLinkCreatePageWithWidgets => this[nameof (ExternalLinkCreatePageWithWidgets)];

    /// <summary>
    /// Gets External Link: How to use content management revision history
    /// </summary>
    [ResourceEntry("ExternalLinkContentManagementRevisionHistory", Description = "External Link: How to use revision history", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/revision-history-for-content-items")]
    public string ExternalLinkContentManagementRevisionHistory => this[nameof (ExternalLinkContentManagementRevisionHistory)];

    /// <summary>Gets External Link: Create page layouts</summary>
    [ResourceEntry("ExternalLinkCreatePageLayouts", Description = "External Link: Create page layouts", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/design-page-templates-in-the-layout-editor")]
    public string ExternalLinkCreatePageLayouts => this[nameof (ExternalLinkCreatePageLayouts)];

    /// <summary>
    /// Gets External Link: Add widgets to pages and templates
    /// </summary>
    [ResourceEntry("ExternalLinkAddWidgets", Description = "External Link: Add widgets to pages and templates", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/add-widgets-on-pages-and-templates")]
    public string ExternalLinkAddWidgets => this[nameof (ExternalLinkAddWidgets)];

    /// <summary>Gets External Link Label: Create page layouts</summary>
    [ResourceEntry("ExternalLinkLabelCreatePageLayouts", Description = "External Link Label: Create page layouts", LastModified = "2018/08/29", Value = "Create page layouts")]
    public string ExternalLinkLabelCreatePageLayouts => this[nameof (ExternalLinkLabelCreatePageLayouts)];

    /// <summary>
    /// Gets External Link Label: Add widgets to pages and templates
    /// </summary>
    [ResourceEntry("ExternalLinkLabelAddWidgets", Description = "External Link Label: Add widgets to pages and templates", LastModified = "2018/08/29", Value = "Add widgets to pages and templates")]
    public string ExternalLinkLabelAddWidgets => this[nameof (ExternalLinkLabelAddWidgets)];

    /// <summary>Label: Learn how to...</summary>
    [ResourceEntry("LearnHowTo", Description = "Label: Learn how to...", LastModified = "2018/08/29", Value = "Learn how to...")]
    public string LearnHowTo => this[nameof (LearnHowTo)];

    /// <summary>Gets External Link Label: Work with revision history</summary>
    [ResourceEntry("ExternalLinkLabelWorkWithRevisionHistory", Description = "External Link Label: Work with revision history", LastModified = "2018/09/26", Value = "Work with revision history")]
    public string ExternalLinkLabelWorkWithRevisionHistory => this[nameof (ExternalLinkLabelWorkWithRevisionHistory)];

    /// <summary>Gets Revision history basic settings title</summary>
    [ResourceEntry("RevisionHistory", Description = "Revision history", LastModified = "2018/10/31", Value = "Revision history")]
    public string RevisionHistory => this[nameof (RevisionHistory)];

    /// <summary>Gets error message for unlocking page.</summary>
    [ResourceEntry("CannotUnlockPage", Description = "The page cannot be unlocked.", LastModified = "2018/11/20", Value = "The page cannot be unlocked.")]
    public string CannotUnlockPage => this[nameof (CannotUnlockPage)];

    /// <summary>Gets error message for unlocking page.</summary>
    [ResourceEntry("CannotDiscardPageDraft", Description = "The current user cannot discard this draft because he is not the owner or has admin privileges.", LastModified = "2018/11/20", Value = "The current user cannot discard this draft because he is not the owner or has admin privileges.")]
    public string CannotDiscardPageDraft => this[nameof (CannotDiscardPageDraft)];

    /// <summary>Gets error message for unlocking page template.</summary>
    [ResourceEntry("CannotUnlockPageTemplate", Description = "The current user does not have permission to unlock this template.", LastModified = "2018/11/20", Value = "The current user does not have permission to unlock this template.")]
    public string CannotUnlockPageTemplate => this[nameof (CannotUnlockPageTemplate)];
  }
}
