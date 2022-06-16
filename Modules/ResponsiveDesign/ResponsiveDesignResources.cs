// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign
{
  /// <summary>
  /// Represents the string resources for the responsive design module.
  /// </summary>
  [ObjectInfo("ResponsiveDesignResources", ResourceClassId = "ResponsiveDesignResources")]
  public class ResponsiveDesignResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignResources" /> class with
    /// the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ResponsiveDesignResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.ResponsiveDesignResources" /> class with the
    /// provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider">
    /// <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />
    /// </param>
    public ResponsiveDesignResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Responsive design resources</summary>
    [ResourceEntry("ResponsiveDesignResourcesTitle", Description = "The title of this class.", LastModified = "2012/01/10", Value = "Responsive Design resources")]
    public string ResponsiveDesignResourcesTitle => this[nameof (ResponsiveDesignResourcesTitle)];

    /// <summary>Responsive design resources</summary>
    [ResourceEntry("ResponsiveDesignResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2012/01/10", Value = "Responsive Design resources")]
    public string ResponsiveDesignResourcesTitlePlural => this[nameof (ResponsiveDesignResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Responsive Design module.
    /// </summary>
    [ResourceEntry("ResponsiveDesignResourcesDescription", Description = "The description of this class.", LastModified = "2009/04/30", Value = "Contains localizable resources for Responsive Design module.")]
    public string ResponsiveDesignResourcesDescription => this[nameof (ResponsiveDesignResourcesDescription)];

    /// <summary>phrase: Access responsive design</summary>
    [ResourceEntry("AccessResponsiveDesign", Description = "phrase: Access responsive design", LastModified = "2012/02/23", Value = "Access responsive design")]
    public string AccessResponsiveDesign => this[nameof (AccessResponsiveDesign)];

    /// <summary>
    /// phrase: Action which either grants or denies users to access responsive design module.
    /// </summary>
    [ResourceEntry("AccessResponsiveDesignDescription", Description = "phrase: Action which either grants or denies users to access responsive design module.", LastModified = "2012/02/23", Value = "Action which either grants or denies users to access responsive design module.")]
    public string AccessResponsiveDesignDescription => this[nameof (AccessResponsiveDesignDescription)];

    /// <summary>Title of the Responsive Design module.</summary>
    [ResourceEntry("ModuleTitle", Description = "Title of the Responsive Design module", LastModified = "2012/01/10", Value = "Responsive Design")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Description of the Responsive Design module.</summary>
    [ResourceEntry("ModuleDescription", Description = "Description of the Responsive Design module", LastModified = "2012/01/10", Value = "The module which let's define responsive design of the pages based on the client capabilities.")]
    public string ModuleDescription => this[nameof (ModuleDescription)];

    /// <summary>Title of the Responsive Design configuration section.</summary>
    [ResourceEntry("ResponsiveDesignConfigCaption", Description = "Title of the Responsive Design configuration section.", LastModified = "2012/01/10", Value = "Responsive Design configuration")]
    public string ResponsiveDesignConfigCaption => this[nameof (ResponsiveDesignConfigCaption)];

    /// <summary>
    /// Description of the Responsive Design configuration section.
    /// </summary>
    [ResourceEntry("ResponsiveDesignConfigDescription", Description = "Description of the Responsive Design configuration section.", LastModified = "2012/01/10", Value = "Configuration section for the Responsive Design module.")]
    public string ResponsiveDesignConfigDescription => this[nameof (ResponsiveDesignConfigDescription)];

    /// <summary>
    /// Url name of the responsive and mobile design page node.
    /// </summary>
    [ResourceEntry("ResponsiveAndMobileDesignNodeUrlName", Description = "Url name of the responsive and mobile design page node.", LastModified = "2012/1/10", Value = "responsive-and-mobile-design")]
    public string ResponsiveAndMobileDesignNodeUrlName => this[nameof (ResponsiveAndMobileDesignNodeUrlName)];

    /// <summary>Title of the responsive and mobile design page node.</summary>
    [ResourceEntry("ResponsiveAndMobileDesignNodeTitle", Description = "Title of the responsive and mobile design page node.", LastModified = "2012/01/27", Value = "Responsive & Mobile design")]
    public string ResponsiveAndMobileDesignNodeTitle => this[nameof (ResponsiveAndMobileDesignNodeTitle)];

    /// <summary>
    /// phrase: The page which let's use manage settings for responsive and mobile design.
    /// </summary>
    [ResourceEntry("ResponsiveAndMobileDesignNodeDescription", Description = "phrase: The page which let's use manage settings for responsive and mobile design.", LastModified = "2012/01/27", Value = "The page which let's use manage settings for responsive and mobile design.")]
    public string ResponsiveAndMobileDesignNodeDescription => this[nameof (ResponsiveAndMobileDesignNodeDescription)];

    /// <summary>
    /// Url name of the responsive design module rule groups page
    /// </summary>
    [ResourceEntry("RuleGroupsUrlName", Description = "Url name of the responsive design module rule groups page", LastModified = "2012/01/27", Value = "rule-groups")]
    public string RuleGroupsUrlName => this[nameof (RuleGroupsUrlName)];

    [ResourceEntry("ResponsiveAndMobileDesign", Description = "phrase: Responsive & Mobile Design", LastModified = "2012/01/27", Value = "Responsive & Mobile Design")]
    public string ResponsiveAndMobileDesign => this[nameof (ResponsiveAndMobileDesign)];

    /// <summary>
    /// phrase: Warning that this module applies for WebForms and Hybrid page templates
    /// </summary>
    [ResourceEntry("ResponsiveAndMobileDesignCompatibilityWarning", Description = "phrase: Responsive & Mobile Design", LastModified = "2019/02/27", Value = "<p class=\"sfInformation sfMLeft35 sfMRight35 sfMBottom30\">The following rules are applied only to pages based on Hybrid and Web Forms templates.</p>")]
    public string ResponsiveAndMobileDesignCompatibilityWarning => this[nameof (ResponsiveAndMobileDesignCompatibilityWarning)];

    /// <summary>
    /// Title of the screen for creating a new group of rules.
    /// </summary>
    [ResourceEntry("CreateAGroupOfRules", Description = "Title of the screen for creating a new group of rules.", LastModified = "2012/01/13", Value = "Create a group of rules")]
    public string CreateAGroupOfRules => this[nameof (CreateAGroupOfRules)];

    /// <summary>phrase: ModifyAGroupOfRules</summary>
    [ResourceEntry("ModifyAGroupOfRules", Description = "phrase: ModifyAGroupOfRules", LastModified = "2012/02/02", Value = "Modify a group of rules")]
    public string ModifyAGroupOfRules => this[nameof (ModifyAGroupOfRules)];

    /// <summary>
    /// The title of the media query name field in the form for creating and editing media queries.
    /// </summary>
    [ResourceEntry("MediaQueryNameFieldTitle", Description = "The title of the media query name field in the form for creating and editing media queries.", LastModified = "2012/01/13", Value = "Name")]
    public string MediaQueryNameFieldTitle => this[nameof (MediaQueryNameFieldTitle)];

    /// <summary>
    /// The description of the media query name field in the form for creating and editing media queries.
    /// </summary>
    [ResourceEntry("MediaQueryNameFieldDescription", Description = "The description of the media query name field in the form for creating and editing media queries.", LastModified = "2012/01/13", Value = "Will be used only in the backend user interface.")]
    public string MediaQueryNameFieldDescription => this[nameof (MediaQueryNameFieldDescription)];

    /// <summary>
    /// The example of the media query name field in the form for creating and editing media queries.
    /// </summary>
    [ResourceEntry("MediaQueryNameFieldExample", Description = "The example of the media query name field in the form for creating and editing media queries.", LastModified = "2012/01/13", Value = "<strong>Example:</strong> <em>Tablets</em>. This name is for your convenience only")]
    public string MediaQueryNameFieldExample => this[nameof (MediaQueryNameFieldExample)];

    /// <summary>
    /// Validation message displayed when the media query name field is not entered.
    /// </summary>
    [ResourceEntry("MediaQueryNameCannotBeEmpty", Description = "Validation message displayed when the media query name field is not entered.", LastModified = "2012/01/13", Value = "The name of the media query is a required field.")]
    public string MediaQueryNameCannotBeEmpty => this[nameof (MediaQueryNameCannotBeEmpty)];

    /// <summary>phrase: Create media query</summary>
    [ResourceEntry("CreateMediaQuery", Description = "phrase: Create media query", LastModified = "2012/01/13", Value = "Create media query")]
    public string CreateMediaQuery => this[nameof (CreateMediaQuery)];

    /// <summary>phrase: Back to media queries</summary>
    [ResourceEntry("BackToMediaQueries", Description = "phrase: Back to media queries", LastModified = "2012/01/13", Value = "Back to media queries")]
    public string BackToMediaQueries => this[nameof (BackToMediaQueries)];

    /// <summary>phrase: This group of rules is active</summary>
    [ResourceEntry("ThisGroupOfRulesIsActive", Description = "phrase: This group of rules is active", LastModified = "2012/01/31", Value = "This group of rules is active")]
    public string ThisGroupOfRulesIsActive => this[nameof (ThisGroupOfRulesIsActive)];

    /// <summary>phrase: Apply behavior to...</summary>
    [ResourceEntry("ApplyBehaviorTo", Description = "phrase: Apply behavior to...", LastModified = "2012/01/31", Value = "Apply behavior to...")]
    public string ApplyBehaviorTo => this[nameof (ApplyBehaviorTo)];

    /// <summary>phrase: Transform the layout</summary>
    [ResourceEntry("TransformTheLayout", Description = "phrase: Transform the layout", LastModified = "2012/01/31", Value = "Transform the layout")]
    public string TransformTheLayout => this[nameof (TransformTheLayout)];

    /// <summary>
    /// phrase: Open a specially prepared site (e.g. mobile version of your site)...
    /// </summary>
    [ResourceEntry("OpenSpeciallyPreparedSite", Description = "phrase: Open a specially prepared site (e.g. mobile version of your site)...", LastModified = "2012/01/31", Value = "Open a specially prepared site (e.g. mobile version of your site)...")]
    public string OpenSpeciallyPreparedSite => this[nameof (OpenSpeciallyPreparedSite)];

    /// <summary>
    /// phrase: System settings: Fine tune default layout elements
    /// </summary>
    [ResourceEntry("LayoutTransformationSectionTitle", Description = "phrase: System settings: Fine tune default layout elements", LastModified = "2012/01/31", Value = "System settings: Fine tune default layout elements")]
    public string LayoutTransformationSectionTitle => this[nameof (LayoutTransformationSectionTitle)];

    /// <summary>phrase: Root page of the specially prepared site</summary>
    [ResourceEntry("SiteSelectorFieldTitle", Description = "phrase: Root page of the specially prepared site", LastModified = "2012/02/01", Value = "Root page of the specially prepared site")]
    public string SiteSelectorFieldTitle => this[nameof (SiteSelectorFieldTitle)];

    /// <summary>
    /// phrase: When detected, a device with characteristics like those set above, always redirect to this page
    /// </summary>
    [ResourceEntry("SiteSelectorFieldDescription", Description = "phrase: When detected, a device with characteristics like those set above, always redirect to this page", LastModified = "2013/07/16", Value = "When detected, a device with characteristics like those set above, always redirect to this page")]
    public string SiteSelectorFieldDescription => this[nameof (SiteSelectorFieldDescription)];

    /// <summary>
    /// phrase: Specific CSS file for transforming layout and design
    /// </summary>
    [ResourceEntry("CssSelectorTitle", Description = "phrase: Specific CSS file for transforming layout and design", LastModified = "2012/02/01", Value = "Specific CSS file for transforming layout and design")]
    public string CssSelectorTitle => this[nameof (CssSelectorTitle)];

    /// <summary>
    /// phrase:When detect a device with characteristics like those set above, always load this CSS file
    /// </summary>
    [ResourceEntry("CssSelectorDescription", Description = "phrase:When detect a device with characteristics like those set above, always load this CSS file", LastModified = "2013/07/16", Value = "When detect a device with characteristics like those set above, always load this CSS file")]
    public string CssSelectorDescription => this[nameof (CssSelectorDescription)];

    /// <summary>phrase: Select a CSS file</summary>
    [ResourceEntry("SelectACssFile", Description = "phrase: Select a CSS file", LastModified = "V4", Value = "Select a CSS file")]
    public string SelectACssFile => this[nameof (SelectACssFile)];

    /// <summary>phrase: Group of rules</summary>
    [ResourceEntry("MediaQueryNameGrid", Description = "phrase: Group of rules", LastModified = "2012/02/02", Value = "Group of rules")]
    public string MediaQueryNameGrid => this[nameof (MediaQueryNameGrid)];

    /// <summary>word: Active</summary>
    [ResourceEntry("Active", Description = "word: Active", LastModified = "2012/02/02", Value = "Active")]
    public string Active => this[nameof (Active)];

    /// <summary>word: Inactive</summary>
    [ResourceEntry("Inactive", Description = "word: Inactive", LastModified = "2012/02/02", Value = "Inactive")]
    public string Inactive => this[nameof (Inactive)];

    /// <summary>phrase: Transform layout</summary>
    [ResourceEntry("TransformLayout", Description = "phrase: Transform layout", LastModified = "2012/02/02", Value = "Transform layout")]
    public string TransformLayout => this[nameof (TransformLayout)];

    /// <summary>phrase: Open a specially prepared</summary>
    [ResourceEntry("OpenASpeciallyPreparedSite", Description = "phrase: Open a specially prepared site", LastModified = "2012/02/02", Value = "Open a specially prepared site")]
    public string OpenASpeciallyPreparedSite => this[nameof (OpenASpeciallyPreparedSite)];

    /// <summary>word: Behavior</summary>
    [ResourceEntry("Behavior", Description = "word: Behavior", LastModified = "2012/02/02", Value = "Behavior")]
    public string Behavior => this[nameof (Behavior)];

    /// <summary>table header: Applied To</summary>
    [ResourceEntry("ContentBlocksAppliedToColumnHeaderText", Description = "table header: Applied To", LastModified = "2013/02/01", Value = "Applied To")]
    public string ContentBlocksAppliedToColumnHeaderText => this[nameof (ContentBlocksAppliedToColumnHeaderText)];

    /// <summary>
    /// label: Any changes in this media query will be reflected on all pages and templates where it is used
    /// </summary>
    [ResourceEntry("MediaQueryDialogDescription", Description = "label", LastModified = "2013/02/04", Value = "Any changes in this media query will be reflected on all pages and templates where it is used")]
    public string MediaQueryDialogDescription => this[nameof (MediaQueryDialogDescription)];

    /// <summary>label: Media Query successfully updated in {0}.</summary>
    [ResourceEntry("SuccessfullyUpdatedMediaQuery", Description = "label: Media Query successfully updated in {0}.", LastModified = "2013/02/04", Value = "Media Query successfully updated in {0}.")]
    public string SuccessfullyUpdatedMediaQuery => this[nameof (SuccessfullyUpdatedMediaQuery)];

    /// <summary>
    /// Title of the warning dialog displpayed when deleting single rule that is used by pages or by other templates.
    /// </summary>
    [ResourceEntry("YouCannotDeleteARuleInUseTitle", Description = "You cannot delete a rule in use.", LastModified = "2019/01/15", Value = "You cannot delete a rule in use")]
    public string YouCannotDeleteARuleInUseTitle => this[nameof (YouCannotDeleteARuleInUseTitle)];

    /// <summary>
    /// The message of the warning dialog displpayed when deleting single rule that is used by pages or templates.
    /// </summary>
    [ResourceEntry("PromptMessageSingleRuleInUse", Description = "The message of the warning dialog displpayed when deleting single rule that is used by pages or templates.", LastModified = "2019/01/15", Value = "<strong>{0} pages and {1} templates</strong> use this rule.<br/><br/>Before you are able to delete the rule, you must remove it from pages or templates that use it.")]
    public string PromptMessageSingleRuleInUse => this[nameof (PromptMessageSingleRuleInUse)];

    /// <summary>phrase:ForDevicesWithTheseCharacteristics</summary>
    [ResourceEntry("ForDevicesWithTheseCharacteristics", Description = "phrase:ForDevicesWithTheseCharacteristics", LastModified = "2012/01/30", Value = "For devices with these characteristics...")]
    public string ForDevicesWithTheseCharacteristics => this[nameof (ForDevicesWithTheseCharacteristics)];

    /// <summary>word: Type</summary>
    [ResourceEntry("DeviceType", Description = "word: Type", LastModified = "2012/01/26", Value = "Type")]
    public string DeviceType => this[nameof (DeviceType)];

    /// <summary>phrase: Add custom...</summary>
    [ResourceEntry("AddCustom", Description = "phrase: Add custom...", LastModified = "2012/01/16", Value = "Add custom...")]
    public string AddCustom => this[nameof (AddCustom)];

    /// <summary>phrase: Add a custom rule (media query)</summary>
    [ResourceEntry("AddACustomRule", Description = "phrase: Add a custom rule (media query)", LastModified = "2012/01/30", Value = "Add a custom rule (media query)")]
    public string AddACustomRule => this[nameof (AddACustomRule)];

    /// <summary>phrase: Tablets and small screens</summary>
    [ResourceEntry("TabletsAndSmallScreens", Description = "phrase: Tablets and small screens", LastModified = "2012/01/30", Value = "Tablets and small screens")]
    public string TabletsAndSmallScreens => this[nameof (TabletsAndSmallScreens)];

    /// <summary>word: Smartphones</summary>
    [ResourceEntry("Smartphones", Description = "phrase: Smartphones", LastModified = "2012/01/30", Value = "Smartphones")]
    public string Smartphones => this[nameof (Smartphones)];

    /// <summary>phrase: Large screens</summary>
    [ResourceEntry("LargeScreens", Description = "phrase: Large screens", LastModified = "2012/01/30", Value = "Large screens")]
    public string LargeScreens => this[nameof (LargeScreens)];

    /// <summary>word: Description</summary>
    [ResourceEntry("Description", Description = "word: Description", LastModified = "2012/01/30", Value = "Description")]
    public string Description => this[nameof (Description)];

    /// <summary>Example: 1024x768 (e.g. iPad, iPad2)</summary>
    [ResourceEntry("DescriptionExample", Description = "Example: 1024x768 (e.g. iPad, iPad2)", LastModified = "2012/01/30", Value = "<strong>Example:</strong> <em>1024x768 (e.g. iPad, iPad2)</em>")]
    public string DescriptionExample => this[nameof (DescriptionExample)];

    /// <summary>word: Size</summary>
    [ResourceEntry("Size", Description = "word: Size", LastModified = "2012/01/30", Value = "Size")]
    public string Size => this[nameof (Size)];

    /// <summary>word: Width</summary>
    [ResourceEntry("Width", Description = "word: Width", LastModified = "2012/01/30", Value = "Width")]
    public string Width => this[nameof (Width)];

    /// <summary>phrase: Range(min, max-width)</summary>
    [ResourceEntry("RangeWidth", Description = "phrase: Range(min, max-width)", LastModified = "2012/01/30", Value = "Range (min, max-width)")]
    public string RangeWidth => this[nameof (RangeWidth)];

    /// <summary>phrase: Device width</summary>
    [ResourceEntry("DeviceWidth", Description = "phrase: Device width", LastModified = "2012/01/30", Value = "Device width")]
    public string DeviceWidth => this[nameof (DeviceWidth)];

    /// <summary>phrase: Range of device width (min, max)</summary>
    [ResourceEntry("RangeDeviceWidth", Description = "phrase: Range of device width (min, max)", LastModified = "2012/01/30", Value = "Range of device width (min, max)")]
    public string RangeDeviceWidth => this[nameof (RangeDeviceWidth)];

    /// <summary>word: Height</summary>
    [ResourceEntry("Height", Description = "word: Height", LastModified = "2012/01/30", Value = "Height")]
    public string Height => this[nameof (Height)];

    /// <summary>phrase: Range (min, max-height)</summary>
    [ResourceEntry("RangeHeight", Description = "phrase: Range (min, max-height)", LastModified = "2012/01/30", Value = "Range (min, max-height)")]
    public string RangeHeight => this[nameof (RangeHeight)];

    /// <summary>phrase: Device height</summary>
    [ResourceEntry("DeviceHeight", Description = "phrase: Device height", LastModified = "2012/01/30", Value = "Device height")]
    public string DeviceHeight => this[nameof (DeviceHeight)];

    /// <summary>phrase: Range of device height (min, max)</summary>
    [ResourceEntry("RangeDeviceHeight", Description = "phrase: Range of device height (min, max)", LastModified = "2012/01/30", Value = "Range of device height (min, max)")]
    public string RangeDeviceHeight => this[nameof (RangeDeviceHeight)];

    /// <summary>word: Min-width</summary>
    [ResourceEntry("MinWidth", Description = "word: Min-width", LastModified = "2012/01/30", Value = "Min-width")]
    public string MinWidth => this[nameof (MinWidth)];

    /// <summary>word: Max-width</summary>
    [ResourceEntry("MaxWidth", Description = "word: Max-width", LastModified = "2012/01/30", Value = "Max-width")]
    public string MaxWidth => this[nameof (MaxWidth)];

    /// <summary>phrase: Min-height</summary>
    [ResourceEntry("MinHeight", Description = "phrase: Min-height", LastModified = "2012/01/30", Value = "Min-height")]
    public string MinHeight => this[nameof (MinHeight)];

    /// <summary>word: Max-height</summary>
    [ResourceEntry("MaxHeight", Description = "word: Max-height", LastModified = "2012/01/30", Value = "Max-height")]
    public string MaxHeight => this[nameof (MaxHeight)];

    /// <summary>word: Orientation</summary>
    [ResourceEntry("Orientation", Description = "word: Orientation", LastModified = "2012/01/30", Value = "Orientation")]
    public string Orientation => this[nameof (Orientation)];

    /// <summary>word: Portrait</summary>
    [ResourceEntry("Portrait", Description = "word: Portrait", LastModified = "2012/01/30", Value = "Portrait")]
    public string Portrait => this[nameof (Portrait)];

    /// <summary>word: Landscape</summary>
    [ResourceEntry("Landscape", Description = "word: Landscape", LastModified = "2012/01/30", Value = "Landscape")]
    public string Landscape => this[nameof (Landscape)];

    /// <summary>word: Both</summary>
    [ResourceEntry("Both", Description = "word: Both", LastModified = "2012/01/30", Value = "Both")]
    public string Both => this[nameof (Both)];

    /// <summary>phrase: Aspect ratio</summary>
    [ResourceEntry("AspectRatio", Description = "phrase: Aspect ratio", LastModified = "2012/01/30", Value = "Aspect ratio")]
    public string AspectRatio => this[nameof (AspectRatio)];

    /// <summary>word: Resolution</summary>
    [ResourceEntry("Resolution", Description = "word: Resolution", LastModified = "2012/01/30", Value = "Resolution")]
    public string Resolution => this[nameof (Resolution)];

    /// <summary>word: Monochrome</summary>
    [ResourceEntry("Monochrome", Description = "word: Monochrome", LastModified = "2012/01/30", Value = "Monochrome")]
    public string Monochrome => this[nameof (Monochrome)];

    /// <summary>word: Grid</summary>
    [ResourceEntry("Grid", Description = "word: Grid", LastModified = "2012/01/30", Value = "Grid")]
    public string Grid => this[nameof (Grid)];

    /// <summary>phrase: Result: media query css</summary>
    [ResourceEntry("ResultMediaQueryCss", Description = "phrase: Result: media query css", LastModified = "2012/01/30", Value = "Result: media query css")]
    public string ResultMediaQueryCss => this[nameof (ResultMediaQueryCss)];

    /// <summary>word: Edit</summary>
    [ResourceEntry("Edit", Description = "word: Edit", LastModified = "2012/01/30", Value = "Edit")]
    public string Edit => this[nameof (Edit)];

    /// <summary>word: Reset</summary>
    [ResourceEntry("Reset", Description = "word: Reset", LastModified = "2012/01/30", Value = "Reset")]
    public string Reset => this[nameof (Reset)];

    /// <summary>
    /// Message displayed when user does not enter the description of the media query rule
    /// </summary>
    [ResourceEntry("RuleDescriptionRequired", Description = "Message displayed when user does not enter the description of the media query rule", LastModified = "2012/02/08", Value = "Description cannot be empty")]
    public string RuleDescriptionRequired => this[nameof (RuleDescriptionRequired)];

    /// <summary>
    /// Message displayed when user does not enter the whole number in a field that expects it
    /// </summary>
    [ResourceEntry("ValueMustBeAWholeNumber", Description = "Message displayed when user does not enter the whole number in a field that expects it", LastModified = "2012/02/08", Value = "Value must be a whole number")]
    public string ValueMustBeAWholeNumber => this[nameof (ValueMustBeAWholeNumber)];

    [ResourceEntry("MobilePreviewTitle", Description = "phrase: Preview for Smartphones & Tablets", LastModified = "2012/02/04", Value = "Preview for<br />Smartphones &amp; Tablets")]
    public string MobilePreviewTitle => this[nameof (MobilePreviewTitle)];

    /// <summary>phrase: Name of the preview device</summary>
    [ResourceEntry("PreviewDeviceNameConfig", Description = "phrase: Name of the preview device", LastModified = "2012/02/04", Value = "Name of the preview device")]
    public string PreviewDeviceNameConfig => this[nameof (PreviewDeviceNameConfig)];

    /// <summary>phrase: Title of the preview device</summary>
    [ResourceEntry("PreviewDeviceTitleConfig", Description = "phrase: Title of the preview device", LastModified = "2012/02/04", Value = "Title of the preview device")]
    public string PreviewDeviceTitleConfig => this[nameof (PreviewDeviceTitleConfig)];

    /// <summary>phrase: Preview devices</summary>
    [ResourceEntry("PreviewDevicesConfig", Description = "phrase: Preview devices", LastModified = "2012/02/04", Value = "Preview devices")]
    public string PreviewDevicesConfig => this[nameof (PreviewDevicesConfig)];

    /// <summary>phrase: Css class associated with the preview device</summary>
    [ResourceEntry("PreviewDeviceCssClassConfig", Description = "phrase: Css class associated with the preview device", LastModified = "2012/02/04", Value = "Css class associated with the preview device")]
    public string PreviewDeviceCssClassConfig => this[nameof (PreviewDeviceCssClassConfig)];

    /// <summary>phrase: The width of the preview device in pixels</summary>
    [ResourceEntry("PreviewDeviceWidthConfig", Description = "phrase: The width of the preview device in pixels", LastModified = "2012/02/04", Value = "The width of the preview device in pixels")]
    public string PreviewDeviceWidthConfig => this[nameof (PreviewDeviceWidthConfig)];

    /// <summary>phrase: The height of the preview device in pixels</summary>
    [ResourceEntry("PreviewDeviceHeightConfig", Description = "phrase: The height of the preview device in pixels", LastModified = "2012/02/04", Value = "The height of the preview device in pixels")]
    public string PreviewDeviceHeightConfig => this[nameof (PreviewDeviceHeightConfig)];

    /// <summary>phrase: The width of the preview device's viewport</summary>
    [ResourceEntry("PreviewDeviceViewportWidthConfig", Description = "phrase: The width of the preview device's viewport", LastModified = "2012/02/04", Value = "The width of the preview device's viewport")]
    public string PreviewDeviceViewportWidthConfig => this[nameof (PreviewDeviceViewportWidthConfig)];

    /// <summary>phrase: The height of the preview device's viewport</summary>
    [ResourceEntry("PreviewDeviceViewportHeightConfig", Description = "phrase: The height of the preview device's viewport", LastModified = "2012/02/04", Value = "The height of the preview device's viewport")]
    public string PreviewDeviceViewportHeightConfig => this[nameof (PreviewDeviceViewportHeightConfig)];

    /// <summary>
    /// phrase: Offset on the X axis of the viewport in the portrait mode.
    /// </summary>
    [ResourceEntry("PreviewDeviceOffsetXConfig", Description = "phrase: Offset on the X axis of the viewport in the portrait mode.", LastModified = "2012/02/04", Value = "Offset on the X axis of the viewport in the portrait mode.")]
    public string PreviewDeviceOffsetXConfig => this[nameof (PreviewDeviceOffsetXConfig)];

    /// <summary>
    /// phrase: Offset on the Y axis of the viewport in the portrait mode.
    /// </summary>
    [ResourceEntry("PreviewDeviceOffsetYConfig", Description = "phrase: Offset on the Y axis of the viewport in the portrait mode.", LastModified = "2012/02/04", Value = "Offset on the Y axis of the viewport in the portrait mode.")]
    public string PreviewDeviceOffsetYConfig => this[nameof (PreviewDeviceOffsetYConfig)];

    /// <summary>
    /// phrase: Offset on the X asix of the viewport in the landscape mode.
    /// </summary>
    [ResourceEntry("PreviewDeviceOffsetXLandscapeConfig", Description = "phrase: Offset on the X asix of the viewport in the landscape mode.", LastModified = "2012/02/04", Value = "Offset on the X asix of the viewport in the landscape mode.")]
    public string PreviewDeviceOffsetXLandscapeConfig => this[nameof (PreviewDeviceOffsetXLandscapeConfig)];

    /// <summary>
    /// phrase: Offset on the Y axis of the viewport in the ladscape mode.
    /// </summary>
    [ResourceEntry("PreviewDeviceOffsetYLandscapeConfig", Description = "phrase: Offset on the Y axis of the viewport in the ladscape mode.", LastModified = "2012/02/04", Value = "Offset on the Y axis of the viewport in the ladscape mode.")]
    public string PreviewDeviceOffsetYLandscapeConfig => this[nameof (PreviewDeviceOffsetYLandscapeConfig)];

    /// <summary>phrase: All groups of rules are applied</summary>
    [ResourceEntry("AllGroupsAreApplied", Description = "phrase: All groups of rules are applied", LastModified = "2013/02/01", Value = "All groups of rules are applied")]
    public string AllGroupsAreApplied => this[nameof (AllGroupsAreApplied)];

    /// <summary>phrase: No groups of rules are applied</summary>
    [ResourceEntry("NoGroupsAreApplied", Description = "phrase: No groups of rules are applied", LastModified = "2013/02/01", Value = "No groups of rules are applied")]
    public string NoGroupsAreApplied => this[nameof (NoGroupsAreApplied)];

    /// <summary>phrase: Only selected groups of rules are applied</summary>
    [ResourceEntry("SelectedRulesAreApplied", Description = "phrase: Only selected groups of rules are applied", LastModified = "2013/02/01", Value = "Only selected groups of rules are applied")]
    public string SelectedRulesAreApplied => this[nameof (SelectedRulesAreApplied)];

    /// <summary>phrase: Rules are inherited from the parent</summary>
    [ResourceEntry("InheritFromParent", Description = "phrase: Rules are inherited from the parent", LastModified = "2013/02/01", Value = "Rules are inherited from the parent")]
    public string InheritFromParent => this[nameof (InheritFromParent)];

    /// <summary>phrase: As set in template</summary>
    [ResourceEntry("AsSetInTemplate", Description = "phrase: As set in template", LastModified = "2013/02/05", Value = "As set in template")]
    public string AsSetInTemplate => this[nameof (AsSetInTemplate)];

    /// <summary>phrase: As set in base template</summary>
    [ResourceEntry("AsSetInBaseTemplate", Description = "phrase: As set in base template", LastModified = "2013/02/06", Value = "As set in base template")]
    public string AsSetInBaseTemplate => this[nameof (AsSetInBaseTemplate)];

    /// <summary>phrase: Navigation transformations</summary>
    [ResourceEntry("NavigationTransformationsSectionTitle", Description = "phrase: Navigation transformations", LastModified = "2013/06/07", Value = "Navigation transformations")]
    public string NavigationTransformationsSectionTitle => this[nameof (NavigationTransformationsSectionTitle)];

    /// <summary>phrase: Toggle vertical menu</summary>
    [ResourceEntry("ToToggleMenu", Description = "phrase: Toggle vertical menu", LastModified = "2013/06/10", Value = "Toggle vertical menu")]
    public string ToToggleMenu => this[nameof (ToToggleMenu)];

    /// <summary>phrase: Dropdown list</summary>
    [ResourceEntry("ToDropDown", Description = "phrase: Dropdown list", LastModified = "2013/06/10", Value = "Dropdown list")]
    public string ToDropDown => this[nameof (ToDropDown)];

    /// <summary>phrase: Hide navigation</summary>
    [ResourceEntry("HideNavigation", Description = "phrase: Hide navigation", LastModified = "2013/06/10", Value = "Hide navigation")]
    public string HideNavigation => this[nameof (HideNavigation)];

    /// <summary>phrase: transform to</summary>
    [ResourceEntry("TransformTo", Description = "phrase: transform to", LastModified = "2013/06/11", Value = "transform to")]
    public string TransformTo => this[nameof (TransformTo)];

    /// <summary>phrase: Add a transformation</summary>
    [ResourceEntry("AddTransformation", Description = "phrase: Add a transformation", LastModified = "2013/06/11", Value = "Add a transformation")]
    public string AddTransformation => this[nameof (AddTransformation)];

    /// <summary>phrase: All navigation widgets</summary>
    [ResourceEntry("AllNavigationWidgets", Description = "phrase: All navigation widgets", LastModified = "2013/06/11", Value = "All navigation widgets")]
    public string AllNavigationWidgets => this[nameof (AllNavigationWidgets)];

    /// <summary>phrase: Navigation widgets with class</summary>
    [ResourceEntry("NavigationWidgetsWithClass", Description = "phrase: Navigation widgets with class", LastModified = "2013/06/11", Value = "Navigation widgets with class")]
    public string NavigationWidgetsWithClass => this[nameof (NavigationWidgetsWithClass)];

    /// <summary>word: Enabled</summary>
    [ResourceEntry("EnabledConfig", Description = "word: Enabled", LastModified = "2012/02/10", Value = "Enabled")]
    public string EnabledConfig => this[nameof (EnabledConfig)];

    /// <summary>phrase: Device types</summary>
    [ResourceEntry("DeviceTypesConfig", Description = "phrase: Device types", LastModified = "2012/01/22", Value = "Device types")]
    public string DeviceTypesConfig => this[nameof (DeviceTypesConfig)];

    /// <summary>phrase: Device type name</summary>
    [ResourceEntry("DeviceTypeNameConfig", Description = "phrase: Device type name", LastModified = "2012/01/22", Value = "Device type name")]
    public string DeviceTypeNameConfig => this[nameof (DeviceTypeNameConfig)];

    /// <summary>phrase: Device type title</summary>
    [ResourceEntry("DeviceTypeTitleConfig", Description = "phrase: Device type title", LastModified = "2012/01/22", Value = "Device type title")]
    public string DeviceTypeTitleConfig => this[nameof (DeviceTypeTitleConfig)];

    /// <summary>phrase: Default media query rules</summary>
    [ResourceEntry("DefaultRulesConfig", Description = "phrase: Default media query rules", LastModified = "2012/01/22", Value = "Default media query rules")]
    public string DefaultRulesConfig => this[nameof (DefaultRulesConfig)];

    /// <summary>phrase: Description of the rule</summary>
    [ResourceEntry("RuleDescriptionConfig", Description = "phrase: Description of the rule", LastModified = "2012/01/22", Value = "Description of the rule")]
    public string RuleDescriptionConfig => this[nameof (RuleDescriptionConfig)];

    /// <summary>phrase: The type of width being defined</summary>
    [ResourceEntry("WidthTypeConfig", Description = "phrase: The type of width being defined", LastModified = "2012/02/13", Value = "The type of width being defined")]
    public string WidthTypeConfig => this[nameof (WidthTypeConfig)];

    /// <summary>phrase: Exact width</summary>
    [ResourceEntry("ExactWidthConfig", Description = "phrase: Exact width", LastModified = "2012/01/22", Value = "Exact width")]
    public string ExactWidthConfig => this[nameof (ExactWidthConfig)];

    /// <summary>phrase: Minimum width</summary>
    [ResourceEntry("MinWidthConfig", Description = "phrase: Minimum width", LastModified = "2012/01/22", Value = "Minimum width")]
    public string MinWidthConfig => this[nameof (MinWidthConfig)];

    /// <summary>phrase: Maximum width</summary>
    [ResourceEntry("MaxWidthConfig", Description = "phrase: Maximum width", LastModified = "2012/01/22", Value = "Maximum width")]
    public string MaxWidthConfig => this[nameof (MaxWidthConfig)];

    /// <summary>phrase: Gets or sets the type of the height rule</summary>
    [ResourceEntry("HeightTypeConfig", Description = "phrase: Gets or sets the type of the height rule", LastModified = "2012/02/13", Value = "Gets or sets the type of the height rule")]
    public string HeightTypeConfig => this[nameof (HeightTypeConfig)];

    /// <summary>phrase: Exact height</summary>
    [ResourceEntry("ExactHeightConfig", Description = "phrase: Exact height", LastModified = "2012/01/22", Value = "Exact height")]
    public string ExactHeightConfig => this[nameof (ExactHeightConfig)];

    /// <summary>phrase: Minimum height</summary>
    [ResourceEntry("MinHeightConfig", Description = "phrase: Minimum height", LastModified = "2012/01/22", Value = "Minimum height")]
    public string MinHeightConfig => this[nameof (MinHeightConfig)];

    /// <summary>phrase: Maximum height</summary>
    [ResourceEntry("MaxHeightConfig", Description = "phrase: Maximum height", LastModified = "2012/01/22", Value = "Maximum height")]
    public string MaxHeightConfig => this[nameof (MaxHeightConfig)];

    /// <summary>word: Orientation</summary>
    [ResourceEntry("OrientationConfig", Description = "word: Orientation", LastModified = "2012/01/22", Value = "Orientation")]
    public string OrientationConfig => this[nameof (OrientationConfig)];

    /// <summary>phrase: Aspect ratio</summary>
    [ResourceEntry("AspectRatioConfig", Description = "phrase: Aspect ratio", LastModified = "2012/01/22", Value = "Aspect ratio")]
    public string AspectRatioConfig => this[nameof (AspectRatioConfig)];

    /// <summary>word: Monochrome?</summary>
    [ResourceEntry("IsMonochromeConfig", Description = "word: Monochrome?", LastModified = "2012/01/22", Value = "Monochrome?")]
    public string IsMonochromeConfig => this[nameof (IsMonochromeConfig)];

    /// <summary>word: Grid?</summary>
    [ResourceEntry("IsGridConfig", Description = "word: Grid?", LastModified = "2012/01/22", Value = "Grid?")]
    public string IsGridConfig => this[nameof (IsGridConfig)];

    /// <summary>word: Resolution</summary>
    [ResourceEntry("ResolutionConfig", Description = "word: Resolution", LastModified = "2012/01/22", Value = "Resolution")]
    public string ResolutionConfig => this[nameof (ResolutionConfig)];

    /// <summary>phrase: Resulting media query rule</summary>
    [ResourceEntry("ResultingRuleConfig", Description = "phrase: Resulting media query rule", LastModified = "2012/02/13", Value = "Resulting media query rule")]
    public string ResultingRuleConfig => this[nameof (ResultingRuleConfig)];

    /// <summary>word: Done</summary>
    [ResourceEntry("CreateRulesGroup", Description = "word: Done", LastModified = "2012/01/27", Value = "Done")]
    public string CreateRulesGroup => this[nameof (CreateRulesGroup)];

    /// <summary>word: Cancel</summary>
    [ResourceEntry("Cancel", Description = "word: Cancel", LastModified = "2012/01/27", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>
    /// phrase: The name of the resource class to be used to localize the labels
    /// </summary>
    [ResourceEntry("ResourceClassNameConfig", Description = "phrase: The name of the resource class to be used to localize the labels", LastModified = "2012/01/30", Value = "The name of the resource class to be used to localize the labels")]
    public string ResourceClassNameConfig => this[nameof (ResourceClassNameConfig)];

    /// <summary>phrase: Name of the layout element</summary>
    [ResourceEntry("LayoutElementNameConfig", Description = "phrase: Name of the layout element", LastModified = "2012/01/31", Value = "Name of the layout element")]
    public string LayoutElementNameConfig => this[nameof (LayoutElementNameConfig)];

    /// <summary>phrase: LayoutElementGroupNameConfig</summary>
    [ResourceEntry("LayoutElementGroupNameConfig", Description = "phrase: LayoutElementGroupNameConfig", LastModified = "2012/02/03", Value = "Name of the group to which the layout element belongs to")]
    public string LayoutElementGroupNameConfig => this[nameof (LayoutElementGroupNameConfig)];

    /// <summary>phrase: Title of the layout element</summary>
    [ResourceEntry("LayoutElementTitleConfig", Description = "phrase: Title of the layout element", LastModified = "2012/01/31", Value = "Title of the layout element")]
    public string LayoutElementTitleConfig => this[nameof (LayoutElementTitleConfig)];

    /// <summary>phrase: Alternate layouts</summary>
    [ResourceEntry("AlternateLayoutsConfig", Description = "phrase: Alternate layouts", LastModified = "2012/01/31", Value = "Alternate layouts")]
    public string AlternateLayoutsConfig => this[nameof (AlternateLayoutsConfig)];

    /// <summary>phrase: Layout elements</summary>
    [ResourceEntry("LayoutElementsConfig", Description = "phrase: Layout elements", LastModified = "2012/01/31", Value = "Layout elements")]
    public string LayoutElementsConfig => this[nameof (LayoutElementsConfig)];

    /// <summary>phrase: CSS for the layout element.</summary>
    [ResourceEntry("LayoutCssConfig", Description = "phrase: CSS for the layout element.", LastModified = "2012/02/05", Value = "CSS for the layout element.")]
    public string LayoutCssConfig => this[nameof (LayoutCssConfig)];

    /// <summary>phrase: Hidden layouts CSS</summary>
    [ResourceEntry("HiddenLayoutCss", Description = "phrase: Hidden layouts CSS", LastModified = "2013/04/11", Value = "Hidden layouts CSS")]
    public string HiddenLayoutCss => this[nameof (HiddenLayoutCss)];

    /// <summary>phrase: Control transformations</summary>
    [ResourceEntry("ControlTransformationsConfig", Description = "phrase: Control transformations", LastModified = "2013/05/29", Value = "Control transformations")]
    public string ControlTransformationsConfig => this[nameof (ControlTransformationsConfig)];

    /// <summary>phrase: Name of the navigation transformation</summary>
    [ResourceEntry("NavigationTransformationNameConfig", Description = "phrase: Name of the navigation transformation", LastModified = "2013/05/29", Value = "Name of the navigation transformation")]
    public string NavigationTransformationNameConfig => this[nameof (NavigationTransformationNameConfig)];

    /// <summary>phrase: CSS of the control transformation</summary>
    [ResourceEntry("TransformationCssConfig", Description = "phrase: CSS of the control transformation", LastModified = "2013/05/29", Value = "CSS of the control transformation")]
    public string TransformationCssConfig => this[nameof (TransformationCssConfig)];

    /// <summary>phrase: Title</summary>
    [ResourceEntry("NavigationTransformationTitleConfig", Description = "phrase: Title", LastModified = "2013/06/18", Value = "Title")]
    public string NavigationTransformationTitleConfig => this[nameof (NavigationTransformationTitleConfig)];

    /// <summary>phrase: ResouceClassId for localization</summary>
    [ResourceEntry("NavigationTransformationResourceClassIdConfig", Description = "phrase: ResouceClassId for localization", LastModified = "2013/06/18", Value = "ResouceClassId for localization")]
    public string NavigationTransformationResourceClassIdConfig => this[nameof (NavigationTransformationResourceClassIdConfig)];

    /// <summary>phrase: Is Active</summary>
    [ResourceEntry("NavigationTransformationIsActiveConfig", Description = "phrase: Is Active", LastModified = "2013/06/18", Value = "Is Active")]
    public string NavigationTransformationIsActiveConfig => this[nameof (NavigationTransformationIsActiveConfig)];

    /// <summary>
    /// phrase: Responsive design settings for Navigation widget
    /// </summary>
    [ResourceEntry("ResponsiveDesignBasicSettingsTitle", Description = "phrase: Responsive design settings for Navigation widget", LastModified = "2013/06/19", Value = "Responsive design settings for Navigation widget")]
    public string ResponsiveDesignBasicSettingsTitle => this[nameof (ResponsiveDesignBasicSettingsTitle)];

    /// <summary>
    /// phrase: Predefine transformation settings for the Navigation widget. You can associate these settings with responsive design rules.
    /// </summary>
    [ResourceEntry("ResponsiveDesignBasicSettingsInfo", Description = "phrase: Predefine transformation settings for the Navigation widget. You can associate these settings with responsive design rules.", LastModified = "2013/06/19", Value = "Predefine transformation settings for the Navigation widget. You can associate these settings with responsive design rules.")]
    public string ResponsiveDesignBasicSettingsInfo => this[nameof (ResponsiveDesignBasicSettingsInfo)];

    /// <summary>phrase: Name</summary>
    [ResourceEntry("BasicSettingsGridFirstCol", Description = "phrase: Name", LastModified = "2013/06/20", Value = "Name")]
    public string BasicSettingsGridFirstCol => this[nameof (BasicSettingsGridFirstCol)];

    /// <summary>phrase: Used in</summary>
    [ResourceEntry("BasicSettingsGridSecondCol", Description = "phrase: Used in", LastModified = "2013/06/20", Value = "Used in")]
    public string BasicSettingsGridSecondCol => this[nameof (BasicSettingsGridSecondCol)];

    /// <summary>phrase: Add new...</summary>
    [ResourceEntry("AddNew", Description = "phrase: Add new...", LastModified = "2013/06/20", Value = "Add new...")]
    public string AddNew => this[nameof (AddNew)];
  }
}
