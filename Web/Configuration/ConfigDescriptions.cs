// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ConfigDescriptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Web.Configuration
{
  /// <summary>
  /// Represents the string resources for the backend configurations.
  /// </summary>
  [ObjectInfo("ConfigDescriptions", ResourceClassId = "ConfigDescriptions")]
  public class ConfigDescriptions : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.Configuration.ConfigDescriptions" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ConfigDescriptions()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.Configuration.ConfigDescriptions" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public ConfigDescriptions(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Configuration elment singluar title</summary>
    [ResourceEntry("ConfigDescriptionsTitle", Description = "The title of this class.", LastModified = "2009/04/30", Value = "Configuration Description")]
    public string ConfigDescriptionsTitle => this[nameof (ConfigDescriptionsTitle)];

    /// <summary>Configuration element plural title</summary>
    [ResourceEntry("ConfigDescriptionsTitlePlural", Description = "The title of this class.", LastModified = "2009/04/30", Value = "Configuration Descriptions")]
    public string ConfigDescriptionsTitlePlural => this[nameof (ConfigDescriptionsTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Sitefinity configuration descriptions.
    /// </summary>
    [ResourceEntry("ConfigDescriptionsDescription", Description = "The description of this class.", LastModified = "2009/04/30", Value = "Contains localizable resources for Sitefinity configuration descriptions.")]
    public string ConfigDescriptionsDescription => this[nameof (ConfigDescriptionsDescription)];

    /// <summary>phrase: Master definition config</summary>
    [ResourceEntry("MasterDefinitionCaption", Description = "phrase: Master definition config", LastModified = "2010/10/22", Value = "Master definition config")]
    public string MasterDefinitionCaption => this[nameof (MasterDefinitionCaption)];

    /// <summary>phrase: Master definition.</summary>
    [ResourceEntry("MasterDefinitionDescription", Description = "phrase: Master definition.", LastModified = "2010/10/22", Value = "Master definition.")]
    public string MasterDefinitionDescription => this[nameof (MasterDefinitionDescription)];

    /// <summary>phrase: Detail definition config</summary>
    [ResourceEntry("DetailDefinitionCaption", Description = "phrase: Detail definition config", LastModified = "2010/10/22", Value = "Detail definition config")]
    public string DetailDefinitionCaption => this[nameof (DetailDefinitionCaption)];

    /// <summary>phrase: Detail definition.</summary>
    [ResourceEntry("DetailDefinitionDescription", Description = "phrase: Detail definition.", LastModified = "2010/10/22", Value = "Detail definition.")]
    public string DetailDefinitionDescription => this[nameof (DetailDefinitionDescription)];

    /// <summary>Control Designers</summary>
    [ResourceEntry("ControlDesignersTitle", Description = "The caption of the ControlDesigners collection in the SystemConfig configuration.", LastModified = "2010/01/09", Value = "Control Designers")]
    public string ControlDesignersTitle => this[nameof (ControlDesignersTitle)];

    /// <summary>
    /// The description of the ControlDesigners collection in the SystemConfig configuration.
    /// </summary>
    [ResourceEntry("ControlDesignersDescription", Description = "The description of the ControlDesigners collection in the SystemConfig configuration.", LastModified = "2010/01/09", Value = "Gets a collection of Sitefinity control designer settings. This maps specific controls to control designer classes.")]
    public string ControlDesignersDescription => this[nameof (ControlDesignersDescription)];

    /// <summary>Message: Model Title</summary>
    [ResourceEntry("TransactionModelTitle", Description = "Describes configuration element.", LastModified = "2010/01/09", Value = "Model Title")]
    public string TransactionModelTitle => this[nameof (TransactionModelTitle)];

    /// <summary>
    /// Message: Defines the transaction model for data providers.
    /// </summary>
    [ResourceEntry("TransactionModelDescription", Description = "Describes configuration element.", LastModified = "2010/01/09", Value = "Defines the transaction model for data providers.")]
    public string TransactionModelDescription => this[nameof (TransactionModelDescription)];

    /// <summary>Message: Backend Page</summary>
    [ResourceEntry("BackendPageElementTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Backend Page")]
    public string BackendPageElementTitle => this[nameof (BackendPageElementTitle)];

    /// <summary>
    /// Message: Holds predefined information for known backend pages.
    /// </summary>
    [ResourceEntry("BackendPageElementDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Holds predefined information for known backend pages.")]
    public string BackendPageElementDescription => this[nameof (BackendPageElementDescription)];

    /// <summary>Message: Role Providers</summary>
    [ResourceEntry("RoleProvidersTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Role Providers")]
    public string RoleProvidersTitle => this[nameof (RoleProvidersTitle)];

    /// <summary>Message: Defines a collection of role data providers.</summary>
    [ResourceEntry("RoleProvidersDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines a collection of role data providers.")]
    public string RoleProvidersDescription => this[nameof (RoleProvidersDescription)];

    /// <summary>Message: Profile Providers</summary>
    [ResourceEntry("ProfileProvidersTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Profile Providers")]
    public string ProfileProvidersTitle => this[nameof (ProfileProvidersTitle)];

    /// <summary>
    /// Message: Defines a collection of user profile data providers.
    /// </summary>
    [ResourceEntry("ProfileProvidersDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines a collection of user profile data providers.")]
    public string ProfileProvidersDescription => this[nameof (ProfileProvidersDescription)];

    /// <summary>Message: Membership Providers</summary>
    [ResourceEntry("MembershipProvidersTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Membership Providers")]
    public string MembershipProvidersTitle => this[nameof (MembershipProvidersTitle)];

    /// <summary>
    /// Message: Defines a collection of user membership data providers.
    /// </summary>
    [ResourceEntry("MembershipProvidersDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines a collection of user membership data providers.")]
    public string MembershipProvidersDescription => this[nameof (MembershipProvidersDescription)];

    /// <summary>Message: Security Providers</summary>
    [ResourceEntry("SecurityProvidersTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Security Providers")]
    public string SecurityProvidersTitle => this[nameof (SecurityProvidersTitle)];

    /// <summary>
    /// Message: Defines a collection of security data providers.
    /// </summary>
    [ResourceEntry("SecurityProvidersDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines a collection of security data providers.")]
    public string SecurityProvidersDescription => this[nameof (SecurityProvidersDescription)];

    /// <summary>Message: Script Manager</summary>
    [ResourceEntry("ScriptManagerElementTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Script Manager")]
    public string ScriptManagerElementTitle => this[nameof (ScriptManagerElementTitle)];

    /// <summary>Message: Defines the default ScriptManager settings.</summary>
    [ResourceEntry("ScriptManagerElementDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines the default ScriptManager settings.")]
    public string ScriptManagerElementDescription => this[nameof (ScriptManagerElementDescription)];

    /// <summary>Message: Script Path</summary>
    [ResourceEntry("ScriptManagerPathTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Script Path")]
    public string ScriptManagerPathTitle => this[nameof (ScriptManagerPathTitle)];

    /// <summary>
    /// Message: Specifies the root path of the location that is used to build the paths to ASP.NET AJAX and custom script files.
    /// </summary>
    [ResourceEntry("ScriptManagerPathDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Specifies the root path of the location that is used to build the paths to ASP.NET AJAX and custom script files.")]
    public string ScriptManagerPathDescription => this[nameof (ScriptManagerPathDescription)];

    /// <summary>Message: Debug</summary>
    [ResourceEntry("ScriptManagerDebugTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Debug")]
    public string ScriptManagerDebugTitle => this[nameof (ScriptManagerDebugTitle)];

    /// <summary>
    /// Message: Indicates whether uncompressed versions of the script files should be used.
    /// </summary>
    [ResourceEntry("ScriptManagerDebugDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Indicates whether uncompressed versions of the script files should be used.")]
    public string ScriptManagerDebugDescription => this[nameof (ScriptManagerDebugDescription)];

    /// <summary>Message: Enable Script Localization</summary>
    [ResourceEntry("ScriptManagerEnableScriptLocalizationTitle", Description = "Describes configuration element.", LastModified = "2013/02/06", Value = "Enable Script Localization")]
    public string ScriptManagerEnableScriptLocalizationTitle => this[nameof (ScriptManagerEnableScriptLocalizationTitle)];

    /// <summary>
    /// Message: Indicates whether the ScriptManager control renders localized versions of script files. Applies only for frontend.
    /// </summary>
    [ResourceEntry("ScriptManagerEnableScriptLocalizationDescription", Description = "Describes configuration element.", LastModified = "2013/02/06", Value = "Indicates whether the ScriptManager control renders localized versions of script files. Applies only for frontend.")]
    public string ScriptManagerEnableScriptLocalizationDescription => this[nameof (ScriptManagerEnableScriptLocalizationDescription)];

    /// <summary>Message: Enable CDN</summary>
    [ResourceEntry("ScriptManagerCdnTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Enable CDN")]
    public string ScriptManagerCdnTitle => this[nameof (ScriptManagerCdnTitle)];

    /// <summary>
    /// Message: Determines whether the current page loads client script references from CDN (Content Delivery Network) paths. Applies only for frontend.
    /// </summary>
    [ResourceEntry("ScriptManagerCdnDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Determines whether the current page loads client script references from CDN (Content Delivery Network) paths. Applies only for frontend.")]
    public string ScriptManagerCdnDescription => this[nameof (ScriptManagerCdnDescription)];

    /// <summary>Message: Enable script combining</summary>
    [ResourceEntry("CombineTitle", Description = "Describes configuration element.", LastModified = "2012/11/22", Value = "Enable script combining")]
    public string CombineTitle => this[nameof (CombineTitle)];

    /// <summary>
    /// Message: Determines whether the current page loads client script references in combined script.
    /// </summary>
    [ResourceEntry("CombineDescription", Description = "Describes configuration element.", LastModified = "2012/11/22", Value = "Determines whether the current page loads client script references in combined script")]
    public string CombineDescription => this[nameof (CombineDescription)];

    /// <summary>Message: Enable script combining</summary>
    [ResourceEntry("OutputPositionTitle", Description = "Describes configuration element.", LastModified = "2012/11/22", Value = "Output Position")]
    public string OutputPositionTitle => this[nameof (OutputPositionTitle)];

    /// <summary>
    /// Message: Determines whether the current page loads client script references in combined script.
    /// </summary>
    [ResourceEntry("OutputPositionDescription", Description = "Describes configuration element.", LastModified = "2012/11/22", Value = "Determines the output position of the referred script")]
    public string OutputPositionDescription => this[nameof (OutputPositionDescription)];

    /// <summary>Message: Script References</summary>
    [ResourceEntry("ScriptManagerScriptsTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Script References")]
    public string ScriptManagerScriptsTitle => this[nameof (ScriptManagerScriptsTitle)];

    /// <summary>
    /// Message: Defines script references, each of which represents a script file, that can be used with ScriptManager.
    /// </summary>
    [ResourceEntry("ScriptManagerScriptsDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines script references, each of which represents a script file, that can be used with ScriptManager.")]
    public string ScriptManagerScriptsDescription => this[nameof (ScriptManagerScriptsDescription)];

    /// <summary>Message: Script Reference</summary>
    [ResourceEntry("ScriptReferenceElementTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Script Reference")]
    public string ScriptReferenceElementTitle => this[nameof (ScriptReferenceElementTitle)];

    /// <summary>
    /// Message: Defines a resource reference to a script file.
    /// </summary>
    [ResourceEntry("ScriptReferenceElementDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines a resource reference to a script file.")]
    public string ScriptReferenceElementDescription => this[nameof (ScriptReferenceElementDescription)];

    /// <summary>Message: Path</summary>
    [ResourceEntry("ScriptReferencePathTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Path")]
    public string ScriptReferencePathTitle => this[nameof (ScriptReferencePathTitle)];

    /// <summary>
    /// Message: Defines the path of the referenced client script file, relative to the Web page or absolute for CDN (Content Delivery Network) location.
    /// </summary>
    [ResourceEntry("ScriptReferencePathDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines the path of the referenced client script file, relative to the Web page or absolute for CDN (Content Delivery Network) location.")]
    public string ScriptReferencePathDescription => this[nameof (ScriptReferencePathDescription)];

    /// <summary>Message: Debug Path</summary>
    [ResourceEntry("ScriptReferenceDebugPathTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Debug Path")]
    public string ScriptReferenceDebugPathTitle => this[nameof (ScriptReferenceDebugPathTitle)];

    /// <summary>
    /// Message: Defines the path of the debug version of the referenced client script file, relative to the Web page or absolute for CDN (Content Delivery Network) location.
    /// </summary>
    [ResourceEntry("ScriptReferenceDebugPathDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines the path of the debug version of the referenced client script file, relative to the Web page or absolute for CDN (Content Delivery Network) location.")]
    public string ScriptReferenceDebugPathDescription => this[nameof (ScriptReferenceDebugPathDescription)];

    /// <summary>Message: Assembly</summary>
    [ResourceEntry("ScriptReferenceAssemblyTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Assembly")]
    public string ScriptReferenceAssemblyTitle => this[nameof (ScriptReferenceAssemblyTitle)];

    /// <summary>
    /// Message: Defines the assembly that contains the client script file as an embedded resource. Assembly name is not required if Path is specified.
    /// </summary>
    [ResourceEntry("ScriptReferenceAssemblyDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Defines the assembly that contains the client script file as an embedded resource. Assembly name is not required if Path is specified.")]
    public string ScriptReferenceAssemblyDescription => this[nameof (ScriptReferenceAssemblyDescription)];

    /// <summary>Message: Ignore Script Path</summary>
    [ResourceEntry("ScriptReferenceIgnorePathTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Ignore Script Path")]
    public string ScriptReferenceIgnorePathTitle => this[nameof (ScriptReferenceIgnorePathTitle)];

    /// <summary>
    /// Message: Indicates whether the ScriptManager.ScriptPath property is included in the URL when you register a client script file from a resource.
    /// </summary>
    [ResourceEntry("ScriptReferenceIgnorePathDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Indicates whether the ScriptManager.ScriptPath property is included in the URL when you register a client script file from a resource.")]
    public string ScriptReferenceIgnorePathDescription => this[nameof (ScriptReferenceIgnorePathDescription)];

    /// <summary>Message: Layout Template</summary>
    [ResourceEntry("LayoutTemplateTitle", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Layout Template")]
    public string LayoutTemplateTitle => this[nameof (LayoutTemplateTitle)];

    /// <summary>
    /// Message: Specifies the name of an embedded layout template or the path to an external (.ascx) template.
    /// </summary>
    [ResourceEntry("LayoutTemplateDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Specifies the name of an embedded layout template or the path to an external (.ascx) template.")]
    public string LayoutTemplateDescription => this[nameof (LayoutTemplateDescription)];

    /// <summary>Message: Enabled</summary>
    [ResourceEntry("EnabledCaption", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Enabled")]
    public string EnabledCaption => this[nameof (EnabledCaption)];

    /// <summary>
    /// Message: Indicates whether a section of toolbox items should appear in the toolbox.
    /// </summary>
    [ResourceEntry("ToolboxSectionEnabledDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Indicates whether a section of toolbox items should appear in the toolbox.")]
    public string ToolboxSectionEnabledDescription => this[nameof (ToolboxSectionEnabledDescription)];

    /// <summary>
    /// Message: Indicates whether a defined toolbox item should appear in the toolbox.
    /// </summary>
    [ResourceEntry("ToolboxItemEnabledDescription", Description = "Describes configuration element.", LastModified = "2009/09/23", Value = "Indicates whether a defined toolbox item should appear in the toolbox.")]
    public string ToolboxItemEnabledDescription => this[nameof (ToolboxItemEnabledDescription)];

    /// <summary>Message: Max Page Nodes</summary>
    [ResourceEntry("MaxPageSiteMapNodesTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Max Page Nodes")]
    public string MaxPageSiteMapNodesTitle => this[nameof (MaxPageSiteMapNodesTitle)];

    /// <summary>Message:</summary>
    [ResourceEntry("MaxPageSiteMapNodesDescription", Description = "Describes configuration element.", LastModified = "2012/01/05", Value = "Specifies the maximum page nodes allowed for a given parent node. If this number is different from 0, the number of retrieved page nodes will be up to the specified value. If 0 is set, all pages for the parent node will be retrieved. The default value is 100.")]
    public string MaxPageSiteMapNodesDescription => this[nameof (MaxPageSiteMapNodesDescription)];

    /// <summary>Message: Toolboxes</summary>
    [ResourceEntry("ToolboxesConfigTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Toolboxes")]
    public string ToolboxesConfigTitle => this[nameof (ToolboxesConfigTitle)];

    /// <summary>
    /// Message: Represents a configuration section for Sitefinity toolboxes.
    /// </summary>
    [ResourceEntry("ToolboxesConfigDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Represents a configuration section for Sitefinity toolboxes.")]
    public string ToolboxesConfigDescription => this[nameof (ToolboxesConfigDescription)];

    /// <summary>Message: Toolboxes</summary>
    [ResourceEntry("ToolboxesTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Toolboxes")]
    public string ToolboxesTitle => this[nameof (ToolboxesTitle)];

    /// <summary>Message: A collection of toolboxes.</summary>
    [ResourceEntry("ToolboxesDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "A collection of toolboxes.")]
    public string ToolboxesDescription => this[nameof (ToolboxesDescription)];

    /// <summary>Message: Sections</summary>
    [ResourceEntry("ToolboxSectionsTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Sections")]
    public string ToolboxSectionsTitle => this[nameof (ToolboxSectionsTitle)];

    /// <summary>Message: A collection of toolbox sections.</summary>
    [ResourceEntry("ToolboxSectionsDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "A collection of toolbox sections.")]
    public string ToolboxSectionsDescription => this[nameof (ToolboxSectionsDescription)];

    /// <summary>
    /// Message: Describes the type and the purpose of the controls contained in this toolbox.
    /// </summary>
    [ResourceEntry("ToolboxDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Describes the type and the purpose of the controls contained in this toolbox.")]
    public string ToolboxDescription => this[nameof (ToolboxDescription)];

    /// <summary>Message: Toolbox</summary>
    [ResourceEntry("ToolboxElementTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Toolbox")]
    public string ToolboxElementTitle => this[nameof (ToolboxElementTitle)];

    /// <summary>
    /// Message: Represents configuration element for Sitefinity toolbox. Toolbox is a container of controls used to build user interfaces such as pages, forms, newsletters and etc.
    /// </summary>
    /// <value>The toolbox section element description.</value>
    [ResourceEntry("ToolboxElementDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Represents configuration element for Sitefinity toolbox. Toolbox is a container of controls used to build user interfaces such as pages, forms, newsletters and etc.")]
    public string ToolboxElementDescription => this[nameof (ToolboxElementDescription)];

    /// <summary>Message: Toolbox Section</summary>
    [ResourceEntry("ToolboxSectionElementTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Toolbox Section")]
    public string ToolboxSectionElementTitle => this[nameof (ToolboxSectionElementTitle)];

    /// <summary>Message: Defines a section of toolbox items.</summary>
    /// <value>The toolbox section element description.</value>
    [ResourceEntry("ToolboxSectionElementDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Defines a section of toolbox items.")]
    public string ToolboxSectionElementDescription => this[nameof (ToolboxSectionElementDescription)];

    /// <summary>Message: Tools</summary>
    [ResourceEntry("ToolsTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Tools")]
    public string ToolsTitle => this[nameof (ToolsTitle)];

    /// <summary>Message: Defines a collection of toolbox items.</summary>
    [ResourceEntry("ToolsDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Defines a collection of toolbox items.")]
    public string ToolsDescription => this[nameof (ToolsDescription)];

    /// <summary>Message: Toolbox Item</summary>
    [ResourceEntry("ToolboxItemElementTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Toolbox Item")]
    public string ToolboxItemElementTitle => this[nameof (ToolboxItemElementTitle)];

    /// <summary>Message: Provides information about controls.</summary>
    /// <value>The toolbox item element description.</value>
    [ResourceEntry("ToolboxItemElementDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Provides information about controls.")]
    public string ToolboxItemElementDescription => this[nameof (ToolboxItemElementDescription)];

    /// <summary>Message: Describes the toolbox items section.</summary>
    /// <value>The toolbox item element description.</value>
    [ResourceEntry("ToolboxSectionDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Describes the toolbox items section.")]
    public string ToolboxSectionDescription => this[nameof (ToolboxSectionDescription)];

    /// <summary>
    /// Message: Describes the purpose of the control represented by this toolbox item.
    /// </summary>
    [ResourceEntry("ToolboxItemDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Describes the purpose of the control represented by this toolbox item.")]
    public string ToolboxItemDescription => this[nameof (ToolboxItemDescription)];

    /// <summary>Message: Control CLR Type or Virtual Path</summary>
    [ResourceEntry("ControlTypeTitle", Description = "The title of configuration element.", LastModified = "2009/08/23", Value = "Control CLR Type or Virtual Path")]
    public string ControlTypeTitle => this[nameof (ControlTypeTitle)];

    /// <summary>
    /// Message: Specifies CLR type for custom controls or the virtual path for user controls.
    /// </summary>
    [ResourceEntry("ControlTypeDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Specifies CLR type for custom controls or the virtual path for user controls.")]
    public string ControlTypeDescription => this[nameof (ControlTypeDescription)];

    /// <summary>Controller CLR type</summary>
    [ResourceEntry("ControllerTypeTitle", Description = "The title of the configuration element.", LastModified = "2012/03/24", Value = "Controller CLR type")]
    public string ControllerTypeTitle => this[nameof (ControllerTypeTitle)];

    /// <summary>Specifies controller CLR type</summary>
    [ResourceEntry("ControllerTypeDescription", Description = "Describes configuration element.", LastModified = "2012/03/24", Value = "Specifies controller CLR type.")]
    public string ControllerTypeDescription => this[nameof (ControllerTypeDescription)];

    /// <summary>Message: Default Theme</summary>
    [ResourceEntry("DefaultThemeTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Default Theme")]
    public string DefaultThemeTitle => this[nameof (DefaultThemeTitle)];

    /// <summary>Message: Page Delimiter</summary>
    [ResourceEntry("PageUrlDelimiterTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Page Delimiter")]
    public string PageUrlDelimiterTitle => this[nameof (PageUrlDelimiterTitle)];

    /// <summary>
    /// Message: Used to delimit page path from the parameters
    /// </summary>
    [ResourceEntry("PageUrlDelimiterDesdcription", Description = "Describes configuration element.", LastModified = "2012/01/05", Value = "Used to delimit page path from the parameters")]
    public string PageUrlDelimiterDesdcription => this[nameof (PageUrlDelimiterDesdcription)];

    /// <summary>
    /// Message: Defines the theme that will be used for pages and templates that do not have explicitly set themes.
    /// </summary>
    [ResourceEntry("DefaultThemeDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Defines the theme that will be used for pages and templates that do not have explicitly set themes.")]
    public string DefaultThemeDescription => this[nameof (DefaultThemeDescription)];

    /// <summary>Message: Home Page ID</summary>
    [ResourceEntry("HomePageIdTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Home Page ID")]
    public string HomePageIdTitle => this[nameof (HomePageIdTitle)];

    /// <summary>
    /// Message: Defines which page should be loaded when a user navigates directly to the site root without specifying particular page.
    /// </summary>
    [ResourceEntry("HomePageIdDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Defines which page should be loaded when a user navigates directly to the site root without specifying particular page.")]
    public string HomePageIdDescription => this[nameof (HomePageIdDescription)];

    /// <summary>Message: HomePageId</summary>
    [ResourceEntry("HomePageId", Description = "Describes configuration element.", LastModified = "2020/08/17", Value = "HomePageId")]
    public string HomePageId => this[nameof (HomePageId)];

    /// <summary>Message: FrontEndLoginPageId</summary>
    [ResourceEntry("FrontEndLoginPageId", Description = "Describes configuration element.", LastModified = "2020/08/17", Value = "FrontEndLoginPageId")]
    public string FrontEndLoginPageId => this[nameof (FrontEndLoginPageId)];

    /// <summary>Message: FrontEndLoginPageUrl</summary>
    [ResourceEntry("FrontEndLoginPageUrl", Description = "Describes configuration element.", LastModified = "2020/08/17", Value = "FrontEndLoginPageUrl")]
    public string FrontEndLoginPageUrl => this[nameof (FrontEndLoginPageUrl)];

    /// <summary>Message: Combine script resources</summary>
    [ResourceEntry("CombineScriptsCaption", Description = "Describes configuration element.", LastModified = "2010/12/11", Value = "Combine script resources")]
    public string CombineScriptsCaption => this[nameof (CombineScriptsCaption)];

    /// <summary>Message: Combine frontend script resources</summary>
    [ResourceEntry("CombineScriptsCaptionFrontEnd", Description = "Describes configuration element.", LastModified = "2013/01/11", Value = "Combine frontend script resources")]
    public string CombineScriptsCaptionFrontEnd => this[nameof (CombineScriptsCaptionFrontEnd)];

    /// <summary>Message: Combine backend script resources</summary>
    [ResourceEntry("CombineScriptsCaptionBackEnd", Description = "Describes configuration element.", LastModified = "2013/01/11", Value = "Combine backend script resources")]
    public string CombineScriptsCaptionBackEnd => this[nameof (CombineScriptsCaptionBackEnd)];

    /// <summary>
    /// Message: Defines whether to combine script resources so that less server calls are needed when rendering pages.
    /// </summary>
    [ResourceEntry("CombineScriptsDescription", Description = "Describes configuration element.", LastModified = "2010/12/11", Value = "Defines whether to combine script resources so that less server calls are needed when rendering pages.")]
    public string CombineScriptsDescription => this[nameof (CombineScriptsDescription)];

    /// <summary>
    /// Message: Defines whether to combine script resources so that less server calls are needed when rendering frontend pages.
    /// </summary>
    [ResourceEntry("CombineScriptsDescriptionFrontEnd", Description = "Describes configuration element.", LastModified = "2013/01/11", Value = "Defines whether to combine script resources so that less server calls are needed when rendering frontend pages.")]
    public string CombineScriptsDescriptionFrontEnd => this[nameof (CombineScriptsDescriptionFrontEnd)];

    /// <summary>
    /// Message: Defines whether to combine script resources so that less server calls are needed when rendering backend pages.
    /// </summary>
    [ResourceEntry("CombineScriptsDescriptionBackEnd", Description = "Describes configuration element.", LastModified = "2013/01/11", Value = "Defines whether to combine script resources so that less server calls are needed when rendering backend pages.")]
    public string CombineScriptsDescriptionBackEnd => this[nameof (CombineScriptsDescriptionBackEnd)];

    /// <summary>Message: Default cache duration resources</summary>
    [ResourceEntry("DefaultCacheDurationCaption", Description = "Describes configuration element.", LastModified = "2010/12/23", Value = "The duration of the cache")]
    public string DefaultCacheDurationCaption => this[nameof (DefaultCacheDurationCaption)];

    /// <summary>Message: Defines what is the cache duration.</summary>
    [ResourceEntry("DefaultCacheDurationDescription", Description = "Describes configuration element.", LastModified = "2010/12/23", Value = "Defines the default cache duration.")]
    public string DefaultCacheDurationDescription => this[nameof (DefaultCacheDurationDescription)];

    /// <summary>Message: View state mode resources</summary>
    [ResourceEntry("ViewStateModeCaption", Description = "Describes configuration element.", LastModified = "2011/01/05", Value = "The flag, if view state will be enabled")]
    public string ViewStateModeCaption => this[nameof (ViewStateModeCaption)];

    /// <summary>Message: Defines what is the view state mode.</summary>
    [ResourceEntry("ViewStateModeDescription", Description = "Describes configuration element.", LastModified = "2011/01/05", Value = "Defines if view state will be enabled by default.")]
    public string ViewStateModeDescription => this[nameof (ViewStateModeDescription)];

    /// <summary>
    /// phrase: The flag that switches on the old algorithm for ordering controls
    /// </summary>
    [ResourceEntry("UseOldControlsOrderAlgorithmCaption", Description = "Describes configuration element.", LastModified = "2011/07/27", Value = "The flag that switches on the old algorithm for ordering controls")]
    public string UseOldControlsOrderAlgorithmCaption => this[nameof (UseOldControlsOrderAlgorithmCaption)];

    /// <summary>
    /// phrase: Defines if the old algorithm for ordering controls will be used when rendering pages. By default the value is false. If you change this value then save/publish the page in order for the change to take effect.
    /// </summary>
    [ResourceEntry("UseOldControlsOrderAlgorithmDescription", Description = "Describes configuration element.", LastModified = "2011/07/27", Value = "Defines if the old algorithm for ordering controls will be used when rendering pages. By default the value is false. If you change this value then save/publish the page in order for the change to take effect.")]
    public string UseOldControlsOrderAlgorithmDescription => this[nameof (UseOldControlsOrderAlgorithmDescription)];

    /// <summary>Message: Combine style sheet resources</summary>
    [ResourceEntry("CombineStyleSheetsCaption", Description = "Describes configuration element.", LastModified = "2010/12/11", Value = "Combine style sheet resources")]
    public string CombineStyleSheetsCaption => this[nameof (CombineStyleSheetsCaption)];

    /// <summary>
    /// Message: Defines whether to combine script resources so that less server calls are needed when rendering pages.
    /// </summary>
    [ResourceEntry("CombineStyleSheetsDescription", Description = "Describes configuration element.", LastModified = "2010/12/11", Value = "Defines whether to combine style sheet resources so that less server calls are needed when rendering pages.")]
    public string CombineStyleSheetsDescription => this[nameof (CombineStyleSheetsDescription)];

    /// <summary>Message: Combine style sheet resources</summary>
    [ResourceEntry("EnableBrowseAndEditCaption", Description = "Describes configuration element.", LastModified = "2011/01/04", Value = "Enable in-line editing.")]
    public string EnableBrowseAndEditCaption => this[nameof (EnableBrowseAndEditCaption)];

    /// <summary>
    /// Message: Defines whether to combine script resources so that less server calls are needed when rendering pages.
    /// </summary>
    [ResourceEntry("EnableBrowseAndEditDescription", Description = "Describes configuration element.", LastModified = "2011/01/04", Value = "Specifies whether to enable or disable the in-line editing functionality.")]
    public string EnableBrowseAndEditDescription => this[nameof (EnableBrowseAndEditDescription)];

    [Obsolete("The described configuration property is obsolete.")]
    [ResourceEntry("EnableWidgetTranslationsCaption", Description = "Describes configuration element.", LastModified = "2011/02/05", Value = "Enable widget translations")]
    public string EnableWidgetTranslationsCaption => this[nameof (EnableWidgetTranslationsCaption)];

    [Obsolete("The described configuration property is obsolete.")]
    [ResourceEntry("EnableWidgetTranslationsDescription", Description = "Describes configuration element.", LastModified = "2011/02/05", Value = "Defines whether to enable the translations of widget properties in synchronized localization mode for pages, templates and forms.")]
    public string EnableWidgetTranslationsDescription => this[nameof (EnableWidgetTranslationsDescription)];

    /// <summary>Message: Backend Pages</summary>
    [ResourceEntry("BackendPagesTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Backend Pages")]
    public string BackendPagesTitle => this[nameof (BackendPagesTitle)];

    /// <summary>
    /// Message: Represents a collection of predefined backend pages.
    /// </summary>
    [ResourceEntry("BackendPagesDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Represents a collection of predefined backend pages.")]
    public string BackendPagesDescription => this[nameof (BackendPagesDescription)];

    /// <summary>Message: URL</summary>
    [ResourceEntry("UrlNameTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "URL")]
    public string UrlNameTitle => this[nameof (UrlNameTitle)];

    /// <summary>
    /// Message: Gets or sets the name of the item that appears in the URL.
    /// </summary>
    [ResourceEntry("UrlNameDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Gets or sets the name of the item that appears in the URL.")]
    public string UrlNameDescription => this[nameof (UrlNameDescription)];

    /// <summary>Message: Menu Name</summary>
    [ResourceEntry("MenuNameTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Menu Name")]
    public string MenuNameTitle => this[nameof (MenuNameTitle)];

    /// <summary>
    /// Message: Gets or sets the name that appears on navigational controls such as site menu.
    /// </summary>
    [ResourceEntry("MenuNameDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Gets or sets the name that appears on navigational controls such as site menu.")]
    public string MenuNameDescription => this[nameof (MenuNameDescription)];

    /// <summary>Message: Template Name</summary>
    [ResourceEntry("TemplateNameTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Template Name")]
    public string TemplateNameTitle => this[nameof (TemplateNameTitle)];

    /// <summary>
    /// Message: Defines the template to set for the configured page.
    /// </summary>
    [ResourceEntry("TemplateNameDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Defines the template to set for the configured page.")]
    public string TemplateNameDescription => this[nameof (TemplateNameDescription)];

    /// <summary>
    /// Message: Describes the purpose of the page represented by this configuration element.
    /// </summary>
    [ResourceEntry("PageElementDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Describes the purpose of the page represented by this configuration element.")]
    public string PageElementDescription => this[nameof (PageElementDescription)];

    /// <summary>Message: Page ID</summary>
    [ResourceEntry("PageIdTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Page ID")]
    public string PageIdTitle => this[nameof (PageIdTitle)];

    /// <summary>Message: Specifies the ID number for the known page.</summary>
    [ResourceEntry("PageIdDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Specifies the ID number for the known page.")]
    public string PageIdDescription => this[nameof (PageIdDescription)];

    /// <summary>Message: Install</summary>
    [ResourceEntry("InstallAttributeTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Install")]
    public string InstallAttributeTitle => this[nameof (InstallAttributeTitle)];

    /// <summary>
    /// Message: Indicates whether this module should be installed on the next application restart.
    /// </summary>
    [ResourceEntry("InstallAttributeDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Indicates whether this module should be installed on the next application restart.")]
    public string InstallAttributeDescription => this[nameof (InstallAttributeDescription)];

    /// <summary>Message: Install</summary>
    [ResourceEntry("VersionAttributeTitle", Description = "Describes configuration element.", LastModified = "2009/10/12", Value = "Version")]
    public string VersionAttributeTitle => this[nameof (VersionAttributeTitle)];

    /// <summary>
    /// Message: Indicates the version of the installed module.
    /// </summary>
    [ResourceEntry("VersionAttributeDescription", Description = "Describes configuration element.", LastModified = "2009/10/12", Value = "Indicates the version of the installed module")]
    public string VersionAttributeDescription => this[nameof (VersionAttributeDescription)];

    /// <summary>Message: Backend Home Page ID</summary>
    [ResourceEntry("BackendHomePageIdTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Backend Home Page ID")]
    public string BackendHomePageIdTitle => this[nameof (BackendHomePageIdTitle)];

    /// <summary>
    /// Message: Defines which page should be loaded when a user navigates directly to the backend area without specifying particular page.
    /// </summary>
    [ResourceEntry("BackendHomePageIdDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Defines which page should be loaded when a user navigates directly to the backend area without specifying particular page.")]
    public string BackendHomePageIdDescription => this[nameof (BackendHomePageIdDescription)];

    /// <summary>Message: Default Frontend Template ID</summary>
    [ResourceEntry("DefaultFrontendTemplateIdTitle", Description = "Describes configuration element.", LastModified = "2010/07/14", Value = "Default Frontend Template ID")]
    public string DefaultFrontendTemplateIdTitle => this[nameof (DefaultFrontendTemplateIdTitle)];

    /// <summary>Message: Default Frontend Template ID Description</summary>
    [ResourceEntry("DefaultFrontendTemplateIdDescription", Description = "Describes configuration element.", LastModified = "2010/07/14", Value = "Defines which template should be selected by default when creating a new frontend page.")]
    public string DefaultFrontendTemplateIdDescription => this[nameof (DefaultFrontendTemplateIdDescription)];

    /// <summary>Message: Default Backend Template ID</summary>
    [ResourceEntry("DefaultBackendTemplateIdTitle", Description = "Describes configuration element.", LastModified = "2010/07/14", Value = "Default Backend Template ID")]
    public string DefaultBackendTemplateIdTitle => this[nameof (DefaultBackendTemplateIdTitle)];

    /// <summary>Message: Default Backend Template ID Description</summary>
    [ResourceEntry("DefaultBackendTemplateIdDescription", Description = "Describes configuration element.", LastModified = "2010/07/14", Value = "Defines which template should be selected by default when creating a new backend page.")]
    public string DefaultBackendTemplateIdDescription => this[nameof (DefaultBackendTemplateIdDescription)];

    /// <summary>Message: Page Taxonomy</summary>
    [ResourceEntry("PageTaxonomyTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Page Taxonomy")]
    public string PageTaxonomyTitle => this[nameof (PageTaxonomyTitle)];

    /// <summary>
    /// Message: Gets or sets the default taxonomy used for page navigation. The taxonomy has to be hierarchical type.
    /// </summary>
    [ResourceEntry("PageTaxonomyDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Gets or sets the default taxonomy used for page navigation. The taxonomy has to be hierarchical type.")]
    public string PageTaxonomyDescription => this[nameof (PageTaxonomyDescription)];

    /// <summary>Message: Page Templates Taxonomy</summary>
    [ResourceEntry("PageTemplatesTaxonomyTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Page Templates Taxonomy")]
    public string PageTemplatesTaxonomyTitle => this[nameof (PageTemplatesTaxonomyTitle)];

    /// <summary>
    /// Message: Gets or sets the default taxonomy used for page templates categorization. The taxonomy has to be hierarchical type.
    /// </summary>
    [ResourceEntry("PageTemplatesTaxonomyDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Gets or sets the default taxonomy used for page templates categorization. The taxonomy has to be hierarchical type.")]
    public string PageTemplatesTaxonomyDescription => this[nameof (PageTemplatesTaxonomyDescription)];

    /// <summary>Message: Page Taxonomy Provider</summary>
    [ResourceEntry("PageTaxonomyProviderTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Page Taxonomy Provider")]
    public string PageTaxonomyProviderTitle => this[nameof (PageTaxonomyProviderTitle)];

    /// <summary>
    /// Message: Defines the Taxonomy data provider that is used to store page structures. If this value is left empty the default Taxonomy provider will be used.
    /// </summary>
    [ResourceEntry("PageTaxonomyProviderDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Defines the Taxonomy data provider that is used to store page structures. If this value is left empty the default Taxonomy provider will be used.")]
    public string PageTaxonomyProviderDescription => this[nameof (PageTaxonomyProviderDescription)];

    /// <summary>Message: Frontend Root Node</summary>
    [ResourceEntry("RootNodeTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Frontend Root Node")]
    public string RootNodeTitle => this[nameof (RootNodeTitle)];

    /// <summary>
    /// Message: The node used as root for page navigation for the frontend.
    /// </summary>
    [ResourceEntry("RootNodeDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "The node used as root for page navigation for the frontend.")]
    public string RootNodeDescription => this[nameof (RootNodeDescription)];

    /// <summary>Message: Backend Root Taxon</summary>
    [ResourceEntry("BackendRootNodeTitle", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "Backend Root Node")]
    public string BackendRootNodeTitle => this[nameof (BackendRootNodeTitle)];

    /// <summary>
    /// Message: The taxon used as root for page navigation for the backend.
    /// </summary>
    [ResourceEntry("BackendRootNodeDescription", Description = "Describes configuration element.", LastModified = "2009/08/23", Value = "The node used as root for page navigation for the backend.")]
    public string BackendRootNodeDescription => this[nameof (BackendRootNodeDescription)];

    /// <summary>Message: Is Partial Route Handler</summary>
    [ResourceEntry("IsPartialRouteHandlerTitle", Description = "Describes configuration element.", LastModified = "2009/07/29", Value = "Is Partial Route Handler")]
    public string IsPartialRouteHandlerTitle => this[nameof (IsPartialRouteHandlerTitle)];

    /// <summary>
    /// Message: Indicates whether the control panel for this module is partial route handler.
    /// When settings a module, this value must be true if the control panel of the module implements <see cref="T:Telerik.Sitefinity.Web.IPartialRouteHandler" /> interface.
    /// This is required as modules are usually instantly initialized on demand but this information is needed on application start.
    /// </summary>
    [ResourceEntry("IsPartialRouteHandlerDescription", Description = "Describes configuration element.", LastModified = "2009/07/29", Value = "Indicates whether the control panel for this module is partial route handler. When settings a module, this value must be true if the control panel of the module implements IPartialRouteHandler interface. This is required as modules are usually instantly initialized on demand but this information is needed on application start.")]
    public string IsPartialRouteHandlerDescription => this[nameof (IsPartialRouteHandlerDescription)];

    /// <summary>
    /// Message: Defines a collection of Sitefinity system services settings.
    /// System services are processes that run in the background and do not have specific user interface
    /// such as schedule timers, indexers and etc.
    /// </summary>
    [ResourceEntry("SystemServices", Description = "Describes configuration element.", LastModified = "2011/03/11", Value = "Defines a collection of Sitefinity system services settings. System services are processes that run in the background and do not have specific user interface such as schedule timers, indexers and etc.")]
    public string SystemServices => this[nameof (SystemServices)];

    /// <summary>
    /// Message: Defines a collection of Sitefinity service modules settings.
    /// Service modules are logically self-contained, pluggable applications that serve other modules.
    /// </summary>
    [ResourceEntry("ServicesModules", Description = "Describes configuration element.", LastModified = "2010/01/05", Value = "Defines a collection of Sitefinity service modules settings. Service modules are logically self-contained, pluggable applications that serve other modules.")]
    public string ServicesModules => this[nameof (ServicesModules)];

    /// <summary>
    /// Message: Defines a collection of Sitefinity service application modules settings.
    /// Application modules are logically self-contained, pluggable applications.
    /// </summary>
    [ResourceEntry("ApplicationModules", Description = "Describes configuration element.", LastModified = "2012/01/05", Value = "Defines a collection of Sitefinity service application modules settings. Application modules are logically self-contained, pluggable applications.")]
    public string ApplicationModules => this[nameof (ApplicationModules)];

    /// <summary>
    /// Message: Defines configuration settings for Sitefinity module or system service.
    /// </summary>
    [ResourceEntry("ModuleSettings", Description = "Describes configuration element.", LastModified = "2009/07/20", Value = "Defines configuration settings for Sitefinity module or system service.")]
    public string ModuleSettings => this[nameof (ModuleSettings)];

    /// <summary>
    /// Message: Provides short description of the module or system service.
    /// </summary>
    [ResourceEntry("ModuleSettingsDescription", Description = "Describes configuration element.", LastModified = "2009/07/20", Value = "Provides short description of the module or system service.")]
    public string ModuleSettingsDescription => this[nameof (ModuleSettingsDescription)];

    /// <summary>word: Name</summary>
    [ResourceEntry("ModuleSettingsNameTitle", Description = "word: Name", LastModified = "2011/09/12", Value = "Name")]
    public string ModuleSettingsNameTitle => this[nameof (ModuleSettingsNameTitle)];

    /// <summary>
    /// phrase: Unique identifier of the module (not interpreted in any other way).
    /// </summary>
    [ResourceEntry("ModuleSettingsNameDescription", Description = "phrase: Unique identifier of the module (not interpreted in any other way).", LastModified = "2011/09/12", Value = "Unique identifier of the module (not interpreted in any other way).")]
    public string ModuleSettingsNameDescription => this[nameof (ModuleSettingsNameDescription)];

    /// <summary>word: Type</summary>
    [ResourceEntry("ModuleSettingsTypeTitle", Description = "word: Type", LastModified = "2011/09/12", Value = "Type")]
    public string ModuleSettingsTypeTitle => this[nameof (ModuleSettingsTypeTitle)];

    /// <summary>
    /// phrase: The full name of the .NET class that implements the module (should implement the Telerik.Sitefinity.Services.IModule interface).
    /// </summary>
    [ResourceEntry("ModuleSettingsTypeDescription", Description = "phrase: The full name of the .NET class that implements the module (should implement the Telerik.Sitefinity.Services.IModule interface).", LastModified = "2011/09/12", Value = "The full name of the .NET class that implements the module (should implement the Telerik.Sitefinity.Services.IModule interface).")]
    public string ModuleSettingsTypeDescription => this[nameof (ModuleSettingsTypeDescription)];

    /// <summary>phrase: Module Name</summary>
    [ResourceEntry("ToolboxItemModuleNameTitle", Description = "The display title of this configuration element.", LastModified = "2009/07/20", Value = "Module Name")]
    public string ToolboxItemModuleNameTitle => this[nameof (ToolboxItemModuleNameTitle)];

    /// <summary>
    /// phrase: Specifies the unique name of the module to which an item belongs.
    /// </summary>
    [ResourceEntry("ToolboxItemModuleNameDescription", Description = "Describes this configuration element.", LastModified = "2009/07/20", Value = "Specifies the unique name of the module to which an item belongs.")]
    public string ToolboxItemModuleNameDescription => this[nameof (ToolboxItemModuleNameDescription)];

    /// <summary>
    /// Message: Defines the startup type of the service. The default value is OnApplicationStart.
    /// </summary>
    [ResourceEntry("StartupType", Description = "Describes configuration element.", LastModified = "2009/07/20", Value = "Defines the startup type of the service. The default value is OnFirstCall.")]
    public string StartupType => this[nameof (StartupType)];

    /// <summary>
    /// Message: Gets a collection of cache dependency handlers settings.
    /// </summary>
    [ResourceEntry("CacheDependencyHandlers", Description = "Describes configuration element.", LastModified = "2009/06/24", Value = "Gets a collection of cache dependency handlers settings.")]
    public string CacheDependencyHandlers => this[nameof (CacheDependencyHandlers)];

    /// <summary>Message: Defines system configuration settings.</summary>
    [ResourceEntry("SystemConfig", Description = "Describes configuration element.", LastModified = "2009/06/24", Value = "Defines system configuration settings.")]
    public string SystemConfig => this[nameof (SystemConfig)];

    /// <summary>
    /// Message: Defines Generic Content configuration settings.
    /// </summary>
    [ResourceEntry("ContentConfig", Description = "Describes configuration element.", LastModified = "2009/06/24", Value = "Defines Generic Content configuration settings.")]
    public string ContentConfig => this[nameof (ContentConfig)];

    /// <summary>Message: Defines pages configuration settings.</summary>
    [ResourceEntry("PagesConfig", Description = "Describes configuration element.", LastModified = "2009/06/24", Value = "Defines pages configuration settings.")]
    public string PagesConfig => this[nameof (PagesConfig)];

    /// <summary>Message: Defines metadata configuration settings.</summary>
    [ResourceEntry("MetadataConfig", Description = "Describes configuration element.", LastModified = "2009/06/24", Value = "Defines metadata configuration settings.")]
    public string MetadataConfig => this[nameof (MetadataConfig)];

    /// <summary>Message: Defines taxonomy configuration settings.</summary>
    [ResourceEntry("TaxonomyConfig", Description = "Describes configuration element.", LastModified = "2009/05/29", Value = "Defines taxonomy configuration settings.")]
    public string TaxonomyConfig => this[nameof (TaxonomyConfig)];

    /// <summary>
    /// Message: Defines the ID of the root security object for this permission.
    /// </summary>
    [ResourceEntry("RootObjectId", Description = "Describes configuration element.", LastModified = "2009/05/22", Value = "Defines the ID of the root security object for this permission.")]
    public string RootObjectId => this[nameof (RootObjectId)];

    /// <summary>
    /// Message: Specifies the date when this project was last modified.
    /// </summary>
    [ResourceEntry("LastModified", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Specifies the date when this project was last modified.")]
    public string LastModified => this[nameof (LastModified)];

    /// <summary>Date Created</summary>
    [ResourceEntry("ProjectDateCreatedTitle", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Created on")]
    public string ProjectDateCreatedTitle => this[nameof (ProjectDateCreatedTitle)];

    /// <summary>Message: Specifies the date this project was created.</summary>
    [ResourceEntry("ProjectDateCreatedDescription", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Specifies the date this project was created.")]
    public string ProjectDateCreatedDescription => this[nameof (ProjectDateCreatedDescription)];

    /// <summary>Project Version</summary>
    [ResourceEntry("ProjectVersionTitle", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Project Version")]
    public string ProjectVersionTitle => this[nameof (ProjectVersionTitle)];

    /// <summary>Defines the version number of this project.</summary>
    [ResourceEntry("ProjectVersionDescription", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Defines the version number of this project. ")]
    public string ProjectVersionDescription => this[nameof (ProjectVersionDescription)];

    /// <summary>Project Description</summary>
    [ResourceEntry("ProjectDescriptionTitle", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Description")]
    public string ProjectDescriptionTitle => this[nameof (ProjectDescriptionTitle)];

    /// <summary>Message: Provides summary of the project.</summary>
    [ResourceEntry("ProjectDescription", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Provides summary of the project.")]
    public string ProjectDescription => this[nameof (ProjectDescription)];

    /// <summary>Project Name</summary>
    [ResourceEntry("ProjectNameTitle", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Project Name")]
    public string ProjectNameTitle => this[nameof (ProjectNameTitle)];

    /// <summary>Message: Defines the name of this project.</summary>
    [ResourceEntry("ProjectNameDescription", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Defines the name of this project.")]
    public string ProjectNameDescription => this[nameof (ProjectNameDescription)];

    /// <summary>Message: Defines project configuration settings.</summary>
    [ResourceEntry("ProjectConfig", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Defines project configuration settings.")]
    public string ProjectConfig => this[nameof (ProjectConfig)];

    /// <summary>
    /// Message: Configuration settings which define the appearance of the Sitefinity backend.
    /// </summary>
    [ResourceEntry("AppearanceConfig", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Configuration settings which define the appearance of the Sitefinity backend.")]
    public string AppearanceConfig => this[nameof (AppearanceConfig)];

    /// <summary>Message: Appearance</summary>
    [ResourceEntry("AppearanceTitle", Description = "Describes configuration element.", LastModified = "2009/07/28", Value = "Appearance")]
    public string AppearanceTitle => this[nameof (AppearanceTitle)];

    /// <summary>
    /// Message: Defines the URL to the login page for this permission.
    /// </summary>
    [ResourceEntry("LoginUrl", Description = "Describes configuration element.", LastModified = "2009/05/21", Value = "Defines the URL to the login page for this permission.")]
    public string LoginUrl => this[nameof (LoginUrl)];

    /// <summary>
    /// Message: Gets or sets the URL to the login page used by Ajax services
    /// </summary>
    [ResourceEntry("AjaxLoginUrl", Description = "Describes configuration element.", LastModified = "2012/01/05", Value = "Gets or sets the URL to the login page used by Ajax services")]
    public string AjaxLoginUrl => this[nameof (AjaxLoginUrl)];

    /// <summary>
    /// Message: Defines which roles will considered as administrative roles. Members of administrative roles always have full rights on the entire application.
    /// </summary>
    [ResourceEntry("AdministrativeRoles", Description = "Describes configuration element.", LastModified = "2009/05/19", Value = "Defines which roles will be considered as administrative roles. Members of administrative roles always have full rights on the entire application. ")]
    public string AdministrativeRoles => this[nameof (AdministrativeRoles)];

    /// <summary>
    /// Message: Defines the name of the cookie used to cache user roles.
    /// </summary>
    [ResourceEntry("RolesCookieName", Description = "Describes configuration element.", LastModified = "2009/05/18", Value = "Defines the name of the cookie used to cache user roles.")]
    public string RolesCookieName => this[nameof (RolesCookieName)];

    /// <summary>
    /// Message: Specifies whether users can be manually (through the user interface) assigned to this application role.
    /// </summary>
    [ResourceEntry("AllowManualUserAssignment", Description = "Describes configuration element.", LastModified = "2009/05/18", Value = "Specifies whether users can be manually (through the user interface) assigned to this application role.")]
    public string AllowManualUserAssignment => this[nameof (AllowManualUserAssignment)];

    /// <summary>Message: Application Roles</summary>
    [ResourceEntry("ApplicationRolesTitle", Description = "Describes configuration element.", LastModified = "2009/05/18", Value = "Application Roles")]
    public string ApplicationRolesTitle => this[nameof (ApplicationRolesTitle)];

    /// <summary>
    /// Message: Defines a collection of application roles. Describes the purpose of the application role. Application roles are like user roles, except that they are assigned to users automatically based on predefined conditions.
    /// </summary>
    [ResourceEntry("ApplicationRolesDescription", Description = "Describes configuration element.", LastModified = "2009/05/18", Value = "Defines a collection of application roles. Describes the purpose of the application role. Application roles are like user roles, except that they are assigned to users automatically based on predefined conditions.")]
    public string ApplicationRolesDescription => this[nameof (ApplicationRolesDescription)];

    /// <summary>
    /// Message: Describes the purpose of the application role.
    /// </summary>
    [ResourceEntry("ApplicationRoleDescriptionDescription", Description = "Describes configuration element.", LastModified = "2009/05/18", Value = "Describes the purpose of the application role.")]
    public string ApplicationRoleDescriptionDescription => this[nameof (ApplicationRoleDescriptionDescription)];

    /// <summary>
    /// Message: Defines application role. Application roles are like user roles, except that they are assigned to users automatically based on predefined conditions.
    /// </summary>
    [ResourceEntry("ApplicationRoleDescription", Description = "Describes configuration element.", LastModified = "2009/05/18", Value = "Defines application role. Application roles are like user roles, except that they are assigned to users automatically based on predefined conditions.")]
    public string ApplicationRoleDescription => this[nameof (ApplicationRoleDescription)];

    /// <summary>
    /// Message: Specifies the length of time, in minutes, before a user is no longer considered to be online.
    /// </summary>
    [ResourceEntry("UserIsOnlineTimeWindow", Description = "Describes configuration element.", LastModified = "2009/05/16", Value = "Specifies the length of time, in minutes, before a user is no longer considered to be online.")]
    public string UserIsOnlineTimeWindow => this[nameof (UserIsOnlineTimeWindow)];

    /// <summary>
    /// Message: A collection of security actions that can be allowed or denied by a permission.
    /// </summary>
    [ResourceEntry("ActionsCollection", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "A collection of security actions that can be allowed or denied by a permission.")]
    public string ActionsCollection => this[nameof (ActionsCollection)];

    /// <summary>Resource strings for security action class.</summary>
    [ResourceEntry("SecurityActionCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "SecurityAction class")]
    public string SecurityActionCaption => this[nameof (SecurityActionCaption)];

    /// <summary>Message: Description of the action.</summary>
    [ResourceEntry("SecurityActionDescription", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Description of the action.")]
    public string SecurityActionDescription => this[nameof (SecurityActionDescription)];

    /// <summary>
    /// Defines the domain URL for resolving absolute URLs, e.g. http://www.domain.com
    /// </summary>
    [ResourceEntry("DomainUrl", Description = "Leave this element empty, if you want the absolute URLs to be resolved with the current domain.", LastModified = "2009/05/21", Value = "Defines the domain URL for resolving absolute URLs, e.g. http://www.domain.com")]
    public string DomainUrl => this[nameof (DomainUrl)];

    /// <summary>
    /// Message: A collection specific permission set actions, customized for specific secured object types.
    /// </summary>
    [ResourceEntry("CustomPermissionsDisplaySettings", Description = "Describes configuration element.", LastModified = "2010/06/10", Value = "A collection specific permission set actions, customized for specific secured object types.")]
    public string CustomPermissionsDisplaySettings => this[nameof (CustomPermissionsDisplaySettings)];

    /// <summary>Message: A collection of data provider settings.</summary>
    [ResourceEntry("Permissions", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "A collection of defined permission sets.")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>
    /// phrase: A collection of security actions that can be allowed or denied by a permission.
    /// </summary>
    [ResourceEntry("CustomSecurityActions", Description = "phrase: A collection of security actions that can be allowed or denied by a permission.", LastModified = "2010/10/29", Value = "A collection of security actions that can be allowed or denied by a permission.")]
    public string CustomSecurityActions => this[nameof (CustomSecurityActions)];

    /// <summary>
    /// phrase: A collection of security actions that can be allowed or denied by a permission.
    /// </summary>
    [ResourceEntry("SecuredObjectCustomPermissionSets", Description = "phrase: A collection of security actions that can be allowed or denied by a permission.", LastModified = "2010/10/29", Value = "A collection of security actions that can be allowed or denied by a permission.")]
    public string SecuredObjectCustomPermissionSets => this[nameof (SecuredObjectCustomPermissionSets)];

    /// <summary>Message: A collection of data provider settings.</summary>
    [ResourceEntry("Providers", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "A collection of data provider settings.")]
    public string Providers => this[nameof (Providers)];

    /// <summary>Providers</summary>
    [ResourceEntry("ProvidersTitle", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Providers")]
    public string ProvidersTitle => this[nameof (ProvidersTitle)];

    /// <summary>Message: A collection of data provider settings.</summary>
    [ResourceEntry("ProvidersDescription", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "A collection of data provider settings.")]
    public string ProvidersDescription => this[nameof (ProvidersDescription)];

    /// <summary>
    /// Message: A collection of Sharepoint Connections settings.
    /// </summary>
    [ResourceEntry("SPConnections", Description = "Describes configuration element.", LastModified = "2009/12/11", Value = "A collection of Sharepoint Connection settings.")]
    public string SPConnections => this[nameof (SPConnections)];

    /// <summary>Message: Defines the default data provider.</summary>
    [ResourceEntry("DefaultProvider", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the default data provider.")]
    public string DefaultProvider => this[nameof (DefaultProvider)];

    /// <summary>Default Provider</summary>
    [ResourceEntry("DefaultProviderTitle", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Default Provider")]
    public string DefaultProviderTitle => this[nameof (DefaultProviderTitle)];

    /// <summary>Message: Defines the default data provider.</summary>
    [ResourceEntry("DefaultProviderDescription", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the default data provider.")]
    public string DefaultProviderDescription => this[nameof (DefaultProviderDescription)];

    /// <summary>
    /// Message: Defines the default membership provider for public users.
    /// </summary>
    [ResourceEntry("DefaultFrontendMembershipProvider", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the default membership provider for public users.")]
    public string DefaultFrontendMembershipProvider => this[nameof (DefaultFrontendMembershipProvider)];

    /// <summary>
    /// Message: Defines the default membership provider for backend users (site administrators).
    /// </summary>
    [ResourceEntry("DefaultBackendMembershipProvider", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the default membership provider for backend users (site administrators).")]
    public string DefaultBackendMembershipProvider => this[nameof (DefaultBackendMembershipProvider)];

    /// <summary>
    /// Message: Defines the default role provider for public users.
    /// </summary>
    [ResourceEntry("DefaultFrontendRoleProvider", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the default role provider for public users.")]
    public string DefaultFrontendRoleProvider => this[nameof (DefaultFrontendRoleProvider)];

    /// <summary>
    /// Message: Defines the default role provider for backend users (site administrators).
    /// </summary>
    [ResourceEntry("DefaultBackendRoleProvider", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the default role provider for backend users (site administrators).")]
    public string DefaultBackendRoleProvider => this[nameof (DefaultBackendRoleProvider)];

    /// <summary>Message: Defines security configuration settings.</summary>
    [ResourceEntry("SecurityConfig", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines security configuration settings.")]
    public string SecurityConfig => this[nameof (SecurityConfig)];

    /// <summary>Word: Visible</summary>
    [ResourceEntry("VisibleCaption", Description = "Specifies the caption of a configuration element.", LastModified = "2009/05/30", Value = "Visible")]
    public string VisibleCaption => this[nameof (VisibleCaption)];

    /// <summary>
    /// Message: Defines if the configuration element will be initially visible.
    /// </summary>
    [ResourceEntry("VisibleDescription", Description = "Describes configuration element.", LastModified = "2012/09/19", Value = "Defines if the configuration element will be initially visible.")]
    public string VisibleDescription => this[nameof (VisibleDescription)];

    /// <summary>phrase: Global resource class ID</summary>
    [ResourceEntry("ResourceClassIdCaption", Description = "phrase: Global resource class ID", LastModified = "2011/06/29", Value = "Global resource class ID")]
    public string ResourceClassIdCaption => this[nameof (ResourceClassIdCaption)];

    /// <summary>
    /// phrase: Defines global resource class ID for retrieving localized strings.
    /// </summary>
    [ResourceEntry("ResourceClassIdDescription", Description = "phrase: Defines global resource class ID for retrieving localized strings.", LastModified = "2011/06/29", Value = "Defines global resource class ID for retrieving localized strings.")]
    public string ResourceClassIdDescription => this[nameof (ResourceClassIdDescription)];

    /// <summary>Message: Action Type</summary>
    [ResourceEntry("SecurityActionTypeCaption", Description = "Specifies the caption of a configuration element.", LastModified = "2010/06/03", Value = "Action Type")]
    public string SecurityActionTypeCaption => this[nameof (SecurityActionTypeCaption)];

    /// <summary>Message: Generic CRUD type of this security action</summary>
    [ResourceEntry("SecurityActionTypeDescription", Description = "Describes configuration element.", LastModified = "2010/06/03", Value = "Generic CRUD type of this security action.")]
    public string SecurityActionTypeDescription => this[nameof (SecurityActionTypeDescription)];

    /// <summary>Message: Defines permission settings.</summary>
    [ResourceEntry("PermissionConfigDescription", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines permission settings.")]
    public string PermissionConfigDescription => this[nameof (PermissionConfigDescription)];

    /// <summary>Message: Name</summary>
    [ResourceEntry("ItemNameCaption", Description = "Specifies the name of configuration element.", LastModified = "2009/05/13", Value = "Name")]
    public string ItemNameCaption => this[nameof (ItemNameCaption)];

    /// <summary>
    /// Message: Defines the programmatic name of the item. This is the name used to access the item.
    /// </summary>
    [ResourceEntry("ItemName", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the programmatic name of the item. This is the name used to access the item.")]
    public string ItemName => this[nameof (ItemName)];

    /// <summary>Message: Title</summary>
    [ResourceEntry("ItemTitleCaption", Description = "Specifies the title caption of configuration element.", LastModified = "2009/05/13", Value = "Title")]
    public string ItemTitleCaption => this[nameof (ItemTitleCaption)];

    /// <summary>
    /// Message: Defines what name will be displayed for the item on the user interface.
    /// </summary>
    [ResourceEntry("ItemTitle", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines what name will be displayed for the item on the user interface.")]
    public string ItemTitle => this[nameof (ItemTitle)];

    /// <summary>Message: Description</summary>
    [ResourceEntry("ItemDescriptionCaption", Description = "The description caption of a configuration element.", LastModified = "2009/08/23", Value = "Description")]
    public string ItemDescriptionCaption => this[nameof (ItemDescriptionCaption)];

    /// <summary>Message: Description of the permission.</summary>
    [ResourceEntry("PermissionDescription", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Description of the permission.")]
    public string PermissionDescription => this[nameof (PermissionDescription)];

    /// <summary>
    /// Message: Gets or sets the default section for Sitefinity backend. The default value is "~/Sitefinity/Dashboard".
    /// </summary>
    [ResourceEntry("DefaultBackendSection", Description = "Describes configuration element.", LastModified = "2012/01/05", Value = "Gets or sets the default section for Sitefinity backend. The default value is \"~/Sitefinity/Dashboard\".")]
    public string DefaultBackendSection => this[nameof (DefaultBackendSection)];

    /// <summary>
    /// Message: Gets or sets the default sub section for Pages section.
    /// The default value is "~/Sitefinity/Pages/SiteMap".
    /// </summary>
    [ResourceEntry("DefaultPagesSection", Description = "Describes configuration element.", LastModified = "2009/04/26", Value = "Gets or sets the default sub section for Pages section. The default value is \"~/Sitefinity/Pages/SiteMap\".")]
    public string DefaultPagesSection => this[nameof (DefaultPagesSection)];

    /// <summary>
    /// Message: Gets or sets the default sub section for Modules section.
    /// The default value is "~/Sitefinity/Modules/Generic_Content".
    /// </summary>
    [ResourceEntry("DefaultModuleSection", Description = "Describes configuration element.", LastModified = "2009/04/26", Value = "Gets or sets the default sub section for Modules section. The default value is \"~/Sitefinity/Modules/Generic_Content\".")]
    public string DefaultModuleSection => this[nameof (DefaultModuleSection)];

    /// <summary>
    /// Message: Gets or sets the default sub section for Administration section.
    /// The default value is "~/Sitefinity/Admin/Users".
    /// </summary>
    [ResourceEntry("DefaultAdminSection", Description = "Describes configuration element.", LastModified = "2009/04/26", Value = "Gets or sets the default sub section for Administration section. The default value is \"~/Sitefinity/Admin/Users\".")]
    public string DefaultAdminSection => this[nameof (DefaultAdminSection)];

    /// <summary>
    /// Gets the description of the Settings -&gt; Advanced -&gt; Security -&gt; StsSignout property.
    /// </summary>
    /// <value>The STS signout description.</value>
    [ResourceEntry("StsSignoutDescription", Description = "Describes configuration property SecurityConfig.StsSignout.", LastModified = "2015/05/26", Value = "Gets or sets a value indicating whether to sign out from the STS, otherwise it the sign out from the relying party.")]
    public string StsSignoutDescription => this[nameof (StsSignoutDescription)];

    /// <summary>
    /// Defines the name of the cookie used for authentication tickets caching.
    /// </summary>
    [ResourceEntry("AuthCookieNameDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Defines the name of the cookie used for authentication tickets caching.")]
    public string AuthCookieNameDescription => this[nameof (AuthCookieNameDescription)];

    /// <summary>
    /// Defines the name of the cookie used for logout reason.
    /// </summary>
    /// <value>The logging cookie name description.</value>
    [ResourceEntry("LoggingCookieNameDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Defines the name of the cookie used for logout reason.")]
    public string LoggingCookieNameDescription => this[nameof (LoggingCookieNameDescription)];

    /// <summary>
    /// Gets or sets the virtual path of the cookie that is used to cache authentication tickets.
    /// </summary>
    [ResourceEntry("AuthCookiePathDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets the virtual path of the cookie that is used to cache authentication tickets.")]
    public string AuthCookiePathDescription => this[nameof (AuthCookiePathDescription)];

    /// <summary>
    /// Gets or sets the name of the domain that is associated with the cookie used for caching authentication tickets.
    /// </summary>
    [ResourceEntry("AuthCookieDomainDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets the name of the domain that is associated with the cookie used for caching authentication tickets.")]
    public string AuthCookieDomainDescription => this[nameof (AuthCookieDomainDescription)];

    /// <summary>
    /// Gets or sets a value indicating whether the cookie that is used to cache authentication tickets will be reset periodically.
    /// </summary>
    [ResourceEntry("AuthCookieSlidingExpirationDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets a value indicating whether the cookie that is used to cache authentication tickets will be reset periodically.")]
    public string AuthCookieSlidingExpirationDescription => this[nameof (AuthCookieSlidingExpirationDescription)];

    /// <summary>
    /// In Forms authentication gets or sets the number of minutes before the cookie that is used to cache authentication tickets expires. In Claims authentication this setting controls the expiration time of the security token that is issued by the Security Token Service (STS). The token is contained in a cookie which expiration time is also configured through this value if AuthCookieIsPersistent setting is enabled. This does not configure the expiration time if the STS cookie. This can be done in the web.config of the STS application by configuring ASP.NET's Forms cookie timeout.
    /// </summary>
    [ResourceEntry("AuthCookieTimeoutDescription", Description = "Describes configuration element.", LastModified = "2013/10/10", Value = "In Forms authentication gets or sets the number of minutes before the cookie that is used to cache authentication tickets expires. In Claims authentication this setting controls the expiration time of the security token that is issued by the Security Token Service (STS). The token is contained in a cookie which expiration time is also configured through this value if AuthCookieIsPersistent setting is enabled. This does not configure the expiration time if the STS cookie. This can be done in the web.config of the STS application by configuring ASP.NET's Forms cookie timeout.")]
    public string AuthCookieTimeoutDescription => this[nameof (AuthCookieTimeoutDescription)];

    /// <summary>
    /// Gets or sets a value indicating whether the cookie that is used to cache authentication tickets requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.
    /// </summary>
    [ResourceEntry("AuthCookieRequireSslDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets a value indicating whether the cookie that is used to cache authentication tickets requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.")]
    public string AuthCookieRequireSslDescription => this[nameof (AuthCookieRequireSslDescription)];

    /// <summary>
    /// Gets or sets the virtual path of the cookie that is used to cache role names.
    /// </summary>
    [ResourceEntry("RolesCookiePathDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets the virtual path of the cookie that is used to cache role names.")]
    public string RolesCookiePathDescription => this[nameof (RolesCookiePathDescription)];

    /// <summary>
    /// Gets or sets the name of the domain that is associated with the cookie that is used to cache role names.
    /// </summary>
    [ResourceEntry("RolesCookieDomainDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets the name of the domain that is associated with the cookie that is used to cache role names.")]
    public string RolesCookieDomainDescription => this[nameof (RolesCookieDomainDescription)];

    /// <summary>
    /// Gets or sets a value indicating whether the cookie that is used to cache role names will be reset periodically.
    /// </summary>
    [ResourceEntry("RolesCookieSlidingExpirationDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets a value indicating whether the cookie that is used to cache role names will be reset periodically.")]
    public string RolesCookieSlidingExpirationDescription => this[nameof (RolesCookieSlidingExpirationDescription)];

    /// <summary>
    /// Gets or sets the number of minutes before the cookie that is used to cache role names expires.
    /// </summary>
    [ResourceEntry("RolesCookieTimeoutDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets the number of minutes before the cookie that is used to cache role names expires.")]
    public string RolesCookieTimeoutDescription => this[nameof (RolesCookieTimeoutDescription)];

    /// <summary>
    /// Gets or sets a value indicating whether the cookie that is used to cache role names requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.
    /// </summary>
    [ResourceEntry("RolesCookieRequireSslDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets a value indicating whether the cookie that is used to cache role names requires a Secure Sockets Layer (SSL) connection in order to be returned to the server.")]
    public string RolesCookieRequireSslDescription => this[nameof (RolesCookieRequireSslDescription)];

    /// <summary>
    /// Gets or sets the key that is used to validate encrypted data, or the process by which the key is generated.
    /// </summary>
    [ResourceEntry("ValidationKeyDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets the key that is used to validate encrypted data, or the process by which the key is generated.")]
    public string ValidationKeyDescription => this[nameof (ValidationKeyDescription)];

    /// <summary>
    /// Gets or sets the key that is used to encrypt and decrypt data, or the process by which the key is generated.
    /// </summary>
    [ResourceEntry("DecryptionKeyDescription", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets the key that is used to encrypt and decrypt data, or the process by which the key is generated.")]
    public string DecryptionKeyDescription => this[nameof (DecryptionKeyDescription)];

    /// <summary>
    /// Gets or sets the key that is used to encrypt and decrypt data, or the process by which the key is generated.
    /// </summary>
    [ResourceEntry("DesKeyCaption", Description = "The caption of the DesKey configuration property.", LastModified = "2011/07/06", Value = "DES key")]
    public string DesKeyCaption => this[nameof (DesKeyCaption)];

    /// <summary>
    /// Gets or sets the key that is used to encrypt and decrypt data, or the process by which the key is generated.
    /// </summary>
    [ResourceEntry("DesKeyDescription", Description = "Describes configuration element.", LastModified = "2011/07/06", Value = "Gets or sets the key that is used to encrypt and decrypt license related data. Do not change.")]
    public string DesKeyDescription => this[nameof (DesKeyDescription)];

    /// <summary>
    /// Gets or sets a value indicating whether to show forgot password link in login form.
    /// </summary>
    [ResourceEntry("ShowForgotPasswordLinkInLoginForm", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets a value indicating whether to show forgot password link in login form.")]
    public string ShowForgotPasswordLinkInLoginForm => this[nameof (ShowForgotPasswordLinkInLoginForm)];

    /// <summary>
    /// Gets or sets a value indicating whether to show change password link in the login form.
    /// </summary>
    [ResourceEntry("ShowChangePasswordLinkInLoginForm", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets a value indicating whether to show change password link in the login form.")]
    public string ShowChangePasswordLinkInLoginForm => this[nameof (ShowChangePasswordLinkInLoginForm)];

    /// <summary>
    /// Gets or sets a value indicating whether to show register link in the login form.
    /// </summary>
    [ResourceEntry("ShowRegisterLinkInLoginForm", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets a value indicating whether to show register link in the login form.")]
    public string ShowRegisterLinkInLoginForm => this[nameof (ShowRegisterLinkInLoginForm)];

    /// <summary>
    /// Gets or sets a value indicating whether to show help link in the login form.
    /// </summary>
    [ResourceEntry("ShowHelpLinkInLoginForm", Description = "Describes configuration element.", LastModified = "2009/06/08", Value = "Gets or sets a value indicating whether to show help link in the login form.")]
    public string ShowHelpLinkInLoginForm => this[nameof (ShowHelpLinkInLoginForm)];

    /// <summary>Gets or set the type of the plugin.</summary>
    [ResourceEntry("PluginType", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Type of the content view control plugin")]
    public string PluginType => this[nameof (PluginType)];

    /// <summary>Gets or sets the name of the plugin.</summary>
    [ResourceEntry("PluginName", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Name of the plugin")]
    public string PluginName => this[nameof (PluginName)];

    /// <summary>Gets or sets the plugin placeholder.</summary>
    [ResourceEntry("PluginPlaceholder", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "The ID of the placeholder where the plugin will appear")]
    public string PluginPlaceholder => this[nameof (PluginPlaceholder)];

    /// <summary>Gets or sets the name of the content view control.</summary>
    [ResourceEntry("ContentViewControlName", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Specifies the name of the content view control")]
    public string ContentViewControlName => this[nameof (ContentViewControlName)];

    /// <summary>Gets or sets the sort expression.</summary>
    [ResourceEntry("SortExpression", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Specifies sort expression for the items in the control")]
    public string SortExpression => this[nameof (SortExpression)];

    /// <summary>Gets or sets the filter expression.</summary>
    [ResourceEntry("FilterExpression", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Specifies filter expression for the items in the control")]
    public string FilterExpression => this[nameof (FilterExpression)];

    /// <summary>Gets or sets the paging</summary>
    [ResourceEntry("AllowPaging", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Specifies if the control allows paging")]
    public string AllowPaging => this[nameof (AllowPaging)];

    /// <summary>Gets or sets the items per page.</summary>
    [ResourceEntry("ItemsPerPage", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Specifies the numbers of the items shown by the control")]
    public string ItemsPerPage => this[nameof (ItemsPerPage)];

    /// <summary>Gets or sets the type of the content view control.</summary>
    [ResourceEntry("ContentViewControlType", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "The type of the content view control")]
    public string ContentViewControlType => this[nameof (ContentViewControlType)];

    /// <summary>Gets the content views.</summary>
    /// <value>The content views.</value>
    [ResourceEntry("ContentViews", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "A collection of different content views")]
    public string ContentViews => this[nameof (ContentViews)];

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    [ResourceEntry("ViewName", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Name of the content view")]
    public string ViewName => this[nameof (ViewName)];

    /// <summary>Gets/sets the type of the view.</summary>
    /// <value>The type of the view.</value>
    [ResourceEntry("ViewType", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Type of the view")]
    public string ViewType => this[nameof (ViewType)];

    /// <summary>Gets or sets the view show mode.</summary>
    /// <value>The view show mode.</value>
    [ResourceEntry("ViewShowMode", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "The type of the view - master or detail")]
    public string ViewShowMode => this[nameof (ViewShowMode)];

    /// <summary>Gets or sets the title of the view</summary>
    /// <value>The title.</value>
    [ResourceEntry("Title", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Gets or sets the title of the view")]
    public string Title => this[nameof (Title)];

    /// <summary>Gets or sets name of the layout for the view</summary>
    /// <value>The name of the layout template.</value>
    [ResourceEntry("LayoutTemplateName", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Gets or sets name of the layout for the view")]
    public string LayoutTemplateName => this[nameof (LayoutTemplateName)];

    /// <summary>Gets the type of the content - rss, news, etc.</summary>
    /// <value>The type of the content.</value>
    [ResourceEntry("ContentType", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Specifies the content type of the view - rss, news, etc. ")]
    public string ContentType => this[nameof (ContentType)];

    /// <summary>Gets the plugins collection for the view</summary>
    [ResourceEntry("Plugins", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Collection of plugins for the view")]
    public string Plugins => this[nameof (Plugins)];

    /// <summary>phrase: Describes the configuration element.</summary>
    [ResourceEntry("Profile", Description = "Describes the configuration element.", LastModified = "2009/09/25", Value = "Profile")]
    public string Profile => this[nameof (Profile)];

    /// <summary>
    /// Message: Group of settings associated with the Rating plugin
    /// </summary>
    /// <value>Group of settings associated with the Rating plugin</value>
    [ResourceEntry("Rating", Description = "Describes configuration element. ", LastModified = "2009/10/22", Value = "Group of settings associated with the Rating plugin")]
    public string Rating => this[nameof (Rating)];

    /// <summary>
    /// Message: When a user votes and their vote is recorded, they cannot vote for a certain amount of time to avoid cheating.
    /// Once this time expires, they will be able to vote again.
    /// </summary>
    [ResourceEntry("Rating_VoteTimeout", Description = "Describes configuration element. ", LastModified = "2012/01/05", Value = "When a user votes and their vote is recorded, they cannot vote for a certain amount of time to avoid cheating. Once this time expires, they will be able to vote again.")]
    public string Rating_VoteTimeout => this[nameof (Rating_VoteTimeout)];

    /// <summary>
    /// Message: Determines whether non-authenticated users can vote. In that case, IP-blacklist will be used
    /// instead of user-blacklist to prevent users from voting multiple times.
    /// </summary>
    /// <value>
    /// Determines whether non-authenticated users can vote. In that case, IP-blacklist will be used
    /// instead of user-blacklist to prevent users from voting multiple times.
    /// </value>
    [ResourceEntry("Rating_CanNonAuthenticatedUsersVote", Description = "Describes configuration element. ", LastModified = "2009/10/22", Value = "Determines whether non-authenticated users can vote. In that case, IP-blacklist will be used instead of user-blacklist to prevent users from voting multiple times.")]
    public string Rating_CanNonAuthenticatedUsersVote => this[nameof (Rating_CanNonAuthenticatedUsersVote)];

    /// <summary>
    /// Message: Determines whether users are allowed to vote only once (use user/ip blacklisting), or not.
    /// </summary>
    /// <value>Determines whether users are allowed to vote only once (use user/ip blacklisting), or not.</value>
    [ResourceEntry("Rating_CanUsersVoteOnlyOnce", Description = "Describes configuration element. ", LastModified = "2009/10/22", Value = "Determines whether users are allowed to vote only once (use user/ip blacklisting), or not.")]
    public string Rating_CanUsersVoteOnlyOnce => this[nameof (Rating_CanUsersVoteOnlyOnce)];

    /// <summary>
    /// Message: Determines whether only accepted comments will appear on the public side or not
    /// </summary>
    /// <value>Determines whether only accepted comments will appear on the public side or not</value>
    [ResourceEntry("CommentsList_ModerateComments", Description = "Describes configuration element. ", LastModified = "2009/11/24", Value = "Determines whether only accepted comments will appear on the public side or not")]
    public string CommentsList_ModerateComments => this[nameof (CommentsList_ModerateComments)];

    /// <summary>
    /// Message: Determines whether vote/rate blocking is performed by IP address or not
    /// </summary>
    /// <value>Determines whether vote/rate blocking is performed by IP address or not</value>
    [ResourceEntry("RatingPlugin_VoteBlockByIpAddress", Description = "Describes configuration element. ", LastModified = "2009/11/26", Value = "Determines whether vote/rate blocking is performed by IP address or not")]
    public string RatingPlugin_VoteBlockByIpAddress => this[nameof (RatingPlugin_VoteBlockByIpAddress)];

    /// <summary>
    /// Message: Determines whether vote/rate blocking is performed by user's ID or not
    /// </summary>
    /// <value>Determines whether vote/rate blocking is performed by user's ID or not</value>
    [ResourceEntry("RatingPlugin_VoteBlockByUserId", Description = "Describes configuration element. ", LastModified = "2009/11/26", Value = "Determines whether vote/rate blocking is performed by user's ID or not")]
    public string RatingPlugin_VoteBlockByUserId => this[nameof (RatingPlugin_VoteBlockByUserId)];

    /// <summary>
    /// Message: Defines whether the ContentView controls should load data items on the server in InitializeControls.
    /// </summary>
    /// <value>Defines whether the ContentView controls should load data items on the server in InitializeControls.</value>
    [ResourceEntry("LoadDataOnServerInitialization", Description = "Describes configuration element. ", LastModified = "2009/12/01", Value = "Defines whether the ContentView controls should load data items on the server in InitializeControls.")]
    public string LoadDataOnServerInitialization => this[nameof (LoadDataOnServerInitialization)];

    /// <summary>Message: HTML Title.</summary>
    /// <value>Defines what Sitefinity should put in the &lt;title&gt;&lt;/title&gt; tag inside the page's &lt;head&gt;&lt;/head&gt;.</value>
    [ResourceEntry("PageElement_HtmlTitle_Description", Description = "Defines what Sitefinity should put in the <title></title> tag inside the page's <head></head>.", LastModified = "2010/02/15", Value = "HTML Title")]
    public string PageElement_HtmlTitle_Description => this[nameof (PageElement_HtmlTitle_Description)];

    /// <summary>
    /// Message: Defines what Sitefinity should put in the &lt;title&gt;&lt;/title&gt; tag inside the page's &lt;head&gt;&lt;/head&gt;.
    /// </summary>
    /// <value>Describes the purpose of a PageElement's HtmlTitle.</value>
    [ResourceEntry("PageElement_HtmlTitle_Title", Description = "Describes the purpose of a PageElement's HtmlTitle.", LastModified = "2010/02/15", Value = "HTML Defines what Sitefinity should put in the <title></title> tag inside the page's <head></head>.")]
    public string PageElement_HtmlTitle_Title => this[nameof (PageElement_HtmlTitle_Title)];

    /// <summary>
    /// Gets or sets the number of minutes before the cookie that is used to cache authentication tickets expires.
    /// </summary>
    [ResourceEntry("BackendUsersSessionTimeoutDescription", Description = "Describes configuration element.", LastModified = "2010/04/20", Value = "Gets or sets the number of minutes before the backend users server session will expire.")]
    public string BackendUsersSessionTimeoutDescription => this[nameof (BackendUsersSessionTimeoutDescription)];

    /// <summary>Message: Name</summary>
    [ResourceEntry("DebugNameCaption", Description = "Specifies the name of configuration element to use when in debug mode.", LastModified = "2010/04/16", Value = "Debug name")]
    public string DebugNameCaption => this[nameof (DebugNameCaption)];

    /// <summary>
    /// Message: Defines the programmatic name of the item to use when in debug mode.
    /// </summary>
    [ResourceEntry("DebugNameDescription", Description = "Describes configuration element.", LastModified = "2010/04/16", Value = "Defines the programmatic name of the item to use when in debug mode.")]
    public string DebugNameDescription => this[nameof (DebugNameDescription)];

    /// <summary>Message: Name</summary>
    [ResourceEntry("MicrosoftAjaxCdnBaseUrlTitle", Description = "Specifies the title of MicrosoftAjaxCdnBaseUrl configuration property.", LastModified = "2010/04/16", Value = "Microsoft Ajax CDN Base Url: http://ajax.aspnetcdn.com/ajax/4.0/1/")]
    public string MicrosoftAjaxCdnBaseUrlTitle => this[nameof (MicrosoftAjaxCdnBaseUrlTitle)];

    /// <summary>
    /// Defines the description for the MicrosoftAjaxCdnBaseUrl setting
    /// </summary>
    [ResourceEntry("MicrosoftAjaxCdnBaseUrlDescription", Description = "Specifies the description of MicrosoftAjaxCdnBaseUrl configuration property.", LastModified = "2010/04/28", Value = "Defines the description for the MicrosoftAjaxCdnBaseUrl - the base url for MicrosoftAjaxLibrary CDN.")]
    public string MicrosoftAjaxCdnBaseUrlDescription => this[nameof (MicrosoftAjaxCdnBaseUrlDescription)];

    /// <summary>
    /// Title of <see cref="!:ToolboxSection.Tags" />
    /// </summary>
    /// <value>Tags</value>
    [ResourceEntry("ToolboxTagsTitle", Description = "Title of ToolboxSection.Tags", LastModified = "2013/10/01", Value = "Tags")]
    public string ToolboxTagsTitle => this[nameof (ToolboxTagsTitle)];

    /// <summary>
    /// Description of <see cref="!:ToolboxSection.Tags" />
    /// </summary>
    [ResourceEntry("ToolboxTagsDescription", Description = "Description of ToolboxSection.Tags", LastModified = "2013/10/01", Value = "A comma-separated list of tags.")]
    public string ToolboxTagsDescription => this[nameof (ToolboxTagsDescription)];

    /// <summary>
    /// Title of <see cref="!:ToolboxSection.Ordinal" />
    /// </summary>
    [ResourceEntry("ToolboxOrdinalTitle", Description = "Title of ToolboxSection.Ordinal", LastModified = "2013/10/01", Value = "Ordinal")]
    public string ToolboxOrdinalTitle => this[nameof (ToolboxOrdinalTitle)];

    /// <summary>
    /// Description of <see cref="!:ToolboxSection.Ordinal" />
    /// </summary>
    [ResourceEntry("ToolboxOrdinalDescription", Description = "Description of ToolboxSection.Ordinal", LastModified = "2013/10/01", Value = "A floating point value used for sorting.")]
    public string ToolboxOrdinalDescription => this[nameof (ToolboxOrdinalDescription)];

    /// <summary>
    /// The title of the check box that disables the limit of active simultaneous backend users.
    /// </summary>
    [ResourceEntry("DisableActiveUserLoginsLimitationTitle", Description = "The title of the check box that disables the limit of active simultaneous backend users.", LastModified = "2014/03/06", Value = "Disable the limit of active simultaneous backend users")]
    public string DisableActiveUserLoginsLimitationTitle => this[nameof (DisableActiveUserLoginsLimitationTitle)];

    /// <summary>
    /// Specifies the description of of the check box that disables the limit of active simultaneous backend users.
    /// </summary>
    [ResourceEntry("DisableActiveUserLoginsLimitationDescription", Description = "Specifies the description of of the check box that disables the limit of active simultaneous backend users.", LastModified = "2014/03/06", Value = "Disables the limit of active simultaneous backend users if the site is operating with unlimited users license or has Security Token Service(STS) with windows authentication. If the backend user is logged in from a browser this will prevent the self logout (you are logged in from another machine) dialog from appearing.")]
    public string DisableActiveUserLoginsLimitationDescription => this[nameof (DisableActiveUserLoginsLimitationDescription)];

    /// <summary>
    /// Specifies the title of the check box that controls if the self logout dialog will be displayed on login screen.
    /// </summary>
    [ResourceEntry("LogOutUsersFromDifferentClientsOnLoginTitle", Description = "Specifies the title of the check box that controls if the self logout dialog will be displayed on login screen.", LastModified = "2014/03/06", Value = "Automatically logout backend users from other HTTP clients on login")]
    public string LogOutUsersFromDifferentClientsOnLoginTitle => this[nameof (LogOutUsersFromDifferentClientsOnLoginTitle)];

    /// <summary>
    /// Specifies the description of the check box that controls if the self logout dialog will be displayed on login screen.
    /// </summary>
    [ResourceEntry("LogOutUsersFromDifferentClientsOnLoginDescription", Description = "Specifies whether the self logout (you are logged in from another computer) dialog will be displayed on login screen.", LastModified = "2014/03/06", Value = "If enabled every login request will automatically logout the user from other HTTP clients session.")]
    public string LogOutUsersFromDifferentClientsOnLoginDescription => this[nameof (LogOutUsersFromDifferentClientsOnLoginDescription)];

    /// <summary>Disable request source validation</summary>
    [ResourceEntry("DisableRequestSourceValidationTitle", Description = "Specifies the title of the check box that controls if the CSRF protection should be disabled.", LastModified = "2014/09/30", Value = "Disable request source validation")]
    public string DisableRequestSourceValidationTitle => this[nameof (DisableRequestSourceValidationTitle)];

    /// <summary>
    /// Disables the validation of http headers preventing performing requests from non trusted domains. This is a CSRF protection.
    /// </summary>
    [ResourceEntry("DisableRequestSourceValidationDescription", Description = "Specifies the description of the check box that controls if the CSRF protection should be enabled.", LastModified = "2014/09/30", Value = "Disables the validation of http headers preventing performing requests from non trusted domains. This is a CSRF protection.")]
    public string DisableRequestSourceValidationDescription => this[nameof (DisableRequestSourceValidationDescription)];

    /// <summary>Trusted domains</summary>
    [ResourceEntry("TrustedLoginDomainsTitle", Description = "Specifies the title of the text box containing white-listed domains.", LastModified = "2014/12/08", Value = "Trusted domains")]
    public string TrustedLoginDomainsTitle => this[nameof (TrustedLoginDomainsTitle)];

    /// <summary>
    /// Comma separated list of white-listed domains that are allowed to login to the site.
    /// </summary>
    [ResourceEntry("TrustedLoginDomainsDescription", Description = "Specifies the description of the text box containing white-listed domains.", LastModified = "2014/12/08", Value = "Comma separated list of white-listed domains that are allowed to login to the site.")]
    public string TrustedLoginDomainsDescription => this[nameof (TrustedLoginDomainsDescription)];

    /// <summary>
    /// The title of the remember me checkbox in login screen has check.
    /// </summary>
    [ResourceEntry("DefaultRememberMeLoginCheckBoxValueTitle", Description = "The title of the remember me checkbox in login screen has check.", LastModified = "2014/03/06", Value = "The default remember me check box value")]
    public string DefaultRememberMeLoginCheckBoxValueTitle => this[nameof (DefaultRememberMeLoginCheckBoxValueTitle)];

    /// <summary>
    /// The description of the remember me checkbox in login screen has check.
    /// </summary>
    [ResourceEntry("DefaultRememberMeLoginCheckBoxValueDescription", Description = "The description of the remember me checkbox in login screen has check.", LastModified = "2014/03/06", Value = "Specifies the login screen's remember me checkbox value")]
    public string DefaultRememberMeLoginCheckBoxValueDescription => this[nameof (DefaultRememberMeLoginCheckBoxValueDescription)];

    /// <summary>
    /// Title of the PagesConfig.IsSitemapNodeFilteringEnabled configuration property
    /// </summary>
    [ResourceEntry("PagesIsSitemapNodeFilteringEnabledTitle", Description = "Title of the PagesConfig.IsSitemapNodeFilteringEnabled configuration property", LastModified = "2012/01/05", Value = "Sets of sitemap node filtering is enabled")]
    public string PagesIsSitemapNodeFilteringEnabledTitle => this[nameof (PagesIsSitemapNodeFilteringEnabledTitle)];

    /// <summary>
    /// Description of the PagesConfig.IsSitemapNodeFilteringEnabled configuration property
    /// </summary>
    [ResourceEntry("PagesIsSitemapNodeFilteringEnabledDescription", Description = "Description of the PagesConfig.IsSitemapNodeFilteringEnabled configuration property", LastModified = "2011/05/24", Value = "Globally turn on or off sitemap filtering")]
    public string PagesIsSitemapNodeFilteringEnabledDescription => this[nameof (PagesIsSitemapNodeFilteringEnabledDescription)];

    /// <summary>
    /// Title of the PagesConfig.IsToRedirectFromGroupPageTitle configuration property
    /// </summary>
    [ResourceEntry("IsToRedirectFromGroupPageTitle", Description = "Title of the PagesConfig.IsToRedirectFromGroupPageTitle configuration property", LastModified = "2011/07/08", Value = "Redirect to the first accessible page inside a group page")]
    public string IsToRedirectFromGroupPageTitle => this[nameof (IsToRedirectFromGroupPageTitle)];

    /// <summary>
    /// Description of the PagesConfig.IsToRedirectFromGroupPageDescription configuration property
    /// </summary>
    [ResourceEntry("IsToRedirectFromGroupPageDescription", Description = "Description of the PagesConfig.IsToRedirectFromGroupPageDescription configuration property.", LastModified = "2011/07/08", Value = "Setting indicating whether to redirect or rewrite to the first accessible node(page) inside a the accessed group page. If true - redirect if false -rewrite.")]
    public string IsToRedirectFromGroupPageDescription => this[nameof (IsToRedirectFromGroupPageDescription)];

    /// <summary>
    /// Title of the PagesConfig.SitemapNodeFilters configuration property
    /// </summary>
    [ResourceEntry("PagesSitemapNodeFiltersTitle", Description = "Title of the PagesConfig.SitemapNodeFilters configuration property", LastModified = "2011/05/24", Value = "Sitemap filters")]
    public string PagesSitemapNodeFiltersTitle => this[nameof (PagesSitemapNodeFiltersTitle)];

    /// <summary>
    /// Description of the PagesConfig.SitemapNodeFilters configuration property
    /// </summary>
    [ResourceEntry("PagesSitemapNodeFiltersDescription", Description = "Description of the PagesConfig.SitemapNodeFilters configuration property", LastModified = "2011/05/24", Value = "Configure individual sitemap filters (e.g. turn them off and on)")]
    public string PagesSitemapNodeFiltersDescription => this[nameof (PagesSitemapNodeFiltersDescription)];

    /// <summary>
    /// Title of the NotAllowedPageExtensionsTitle configuration property
    /// </summary>
    [ResourceEntry("NotAllowedPageExtensionsTitle", Description = "Title of the NotAllowedPageExtensionsTitle configuration property", LastModified = "2011/08/03", Value = "Not allowed page extensions")]
    public string NotAllowedPageExtensionsTitle => this[nameof (NotAllowedPageExtensionsTitle)];

    /// <summary>
    /// Description of the NotAllowedPageExtensionsTitle configuration property
    /// </summary>
    [ResourceEntry("NotAllowedPageExtensionsDescription", Description = "Description of the NotAllowedPageExtensionsTitle configuration property", LastModified = "2011/08/03", Value = "Comma separated extensions that will be not processed by the sitemap (e.g. .jpg, .gif, .jpg)")]
    public string NotAllowedPageExtensionsDescription => this[nameof (NotAllowedPageExtensionsDescription)];

    /// <summary>
    /// Title of the KnownPageExtensions configuration property
    /// </summary>
    [ResourceEntry("KnownPageExtensionsTitle", Description = "Title of the KnownPageExtensions configuration property", LastModified = "2012/10/25", Value = "Known page extensions")]
    public string KnownPageExtensionsTitle => this[nameof (KnownPageExtensionsTitle)];

    /// <summary>
    /// Description of the KnownPageExtensions configuration property
    /// </summary>
    [ResourceEntry("KnownPageExtensionsDescription", Description = "Description of the AllowedPageExtensions configuration property", LastModified = "2012/10/25", Value = "Comma separated extensions that will be processed by the sitemap (e.g. .aspx, .html) and will be handled by the Page manager as extension property")]
    public string KnownPageExtensionsDescription => this[nameof (KnownPageExtensionsDescription)];

    /// <summary>Title of the DefaultExtension configuration property</summary>
    [ResourceEntry("DefaultExtension", Description = "Title of the DefaultExtension configuration property", LastModified = "2013/06/17", Value = "Default page extension")]
    public string DefaultExtension => this[nameof (DefaultExtension)];

    /// <summary>
    /// Description of the DefaultExtension configuration property
    /// </summary>
    [ResourceEntry("DefaultExtensionDescription", Description = "Title of the DefaultExtension configuration property", LastModified = "2013/07/11", Value = "The extension (e.g. .aspx) will be automatically added in the URL of all the pages you create")]
    public string DefaultExtensionDescription => this[nameof (DefaultExtensionDescription)];

    /// <summary>
    /// Title of the PagesConfig -&gt; OpenHomePageAsSiteRoot configuration property.
    /// </summary>
    [ResourceEntry("OpenHomePageAsSiteRootTitle", Description = "Title of the PagesConfig -> OpenHomePageAsSiteRoot configuration property.", LastModified = "2014/04/01", Value = "If the home url will redirect permanently to the site root")]
    public string OpenHomePageAsSiteRootTitle => this[nameof (OpenHomePageAsSiteRootTitle)];

    /// <summary>
    /// Description of the PagesConfig -&gt; OpenHomePageAsSiteRoot configuration property.
    /// </summary>
    [ResourceEntry("OpenHomePageAsSiteRootDescription", Description = "Description of the PagesConfig -> OpenHomePageAsSiteRoot configuration property.", LastModified = "2014/04/01", Value = "Performs a permanent redirect to site root when the full page url is used. E.g. if the site root is www.sitefinity.com and the home page name is 'home', a request to www.sitefinity.com/home will redirect to www.sitefinity.com. Requrires application restart.")]
    public string OpenHomePageAsSiteRootDescription => this[nameof (OpenHomePageAsSiteRootDescription)];

    /// <summary>
    /// Description of the DefaultExtension configuration property
    /// </summary>
    [ResourceEntry("DisableInvalidateOutputCacheOnPermissionsChange", Description = "Title of the InvalidateOutputCacheOnPermissionsChange configuration property", LastModified = "2013/08/20", Value = "Disable Output cache invalidation when permission is changed")]
    public string DisableInvalidateOutputCacheOnPermissionsChange => this[nameof (DisableInvalidateOutputCacheOnPermissionsChange)];

    /// <summary>
    /// Description of the DefaultExtension configuration property
    /// </summary>
    [ResourceEntry("DisableInvalidateOutputCacheOnPermissionsChangeDescription", Description = "Title of the InvalidateOutputCacheOnPermissionsChange configuration property", LastModified = "2013/08/20", Value = "Disable Output Cache invalidation when any permission is changed")]
    public string DisableInvalidateOutputCacheOnPermissionsChangeDescription => this[nameof (DisableInvalidateOutputCacheOnPermissionsChangeDescription)];

    /// <summary>
    /// Title of the CustomUrlRegularExpression config property.
    /// </summary>
    /// <value>Custom URL regular expression.</value>
    [ResourceEntry("CustomUrlRegularExpressionTitle", Description = "Title of the CustomUrlRegularExpression config property.", LastModified = "2013/10/30", Value = "Custom URL regular expression.")]
    public string CustomUrlRegularExpressionTitle => this[nameof (CustomUrlRegularExpressionTitle)];

    /// <summary>
    /// Description of the CustomUrlRegularExpression config property.
    /// </summary>
    /// <value>The regular expression used for validating the custom page url.</value>
    [ResourceEntry("CustomUrlRegularExpressionDescription", Description = "Description of the CustomUrlRegularExpression config property.", LastModified = "2013/10/30", Value = "The regular expression used for validating the custom page url.")]
    public string CustomUrlRegularExpressionDescription => this[nameof (CustomUrlRegularExpressionDescription)];

    [ResourceEntry("EnableLinkSharingTitle", Description = "", LastModified = "2014/01/23", Value = "Allow backend users to share link to preview pages")]
    public string EnableLinkSharingTitle => this[nameof (EnableLinkSharingTitle)];

    [ResourceEntry("EnableLinkSharingDescription", Description = "", LastModified = "2014/01/23", Value = "Allows backend users to generate link for the selected page from 'Actions' menu.")]
    public string EnableLinkSharingDescription => this[nameof (EnableLinkSharingDescription)];

    [ResourceEntry("SharedLinkExpirationTimeTitle", Description = "", LastModified = "2014/01/23", Value = "Expiration time for shared links")]
    public string SharedLinkExpirationTimeTitle => this[nameof (SharedLinkExpirationTimeTitle)];

    [ResourceEntry("SharedLinkExpirationTimeDescription", Description = "", LastModified = "2014/01/23", Value = "Defined in hours. Example: 24 (1 day)")]
    public string SharedLinkExpirationTimeDescription => this[nameof (SharedLinkExpirationTimeDescription)];

    [ResourceEntry("RedirectCacheExpiredTitle", Description = "", LastModified = "2015/02/26", Value = "Expiration time for permanent redirect")]
    public string RedirectCacheExpiredTitle => this[nameof (RedirectCacheExpiredTitle)];

    [ResourceEntry("RedirectCacheExpiredDescription", Description = "", LastModified = "2015/02/26", Value = "Defined in seconds. Example: 86400 (1 day)")]
    public string RedirectCacheExpiredDescription => this[nameof (RedirectCacheExpiredDescription)];

    [ResourceEntry("PageTemplatesFrameworksTitle", Description = "", LastModified = "2019/02/12", Value = "Page templates framework")]
    public string PageTemplatesFrameworksTitle => this["pageTemplatesFrameworksTitle"];

    [ResourceEntry("PageTemplatesFrameworksDescription", Description = "", LastModified = "2019/04/01", Value = "Which frameworks are available for page templates.")]
    public string PageTemplatesFrameworksDescription => this["pageTemplatesFrameworksDescription"];

    [ResourceEntry("AllowPageTemplatesFrameworkChangeTitle", Description = "", LastModified = "2019/04/01", Value = "Allow changing page template framework")]
    public string AllowPageTemplatesFrameworkChangeTitle => this["аllowPageTemplatesFrameworkChangeTitle"];

    [ResourceEntry("AllowPageTemplatesFrameworkChangeDescription", Description = "", LastModified = "2019/04/01", Value = "Enables selecting templates from all available frameworks (MVC, Hybrid, Webforms) when changing a base template. For example, when this setting is enabled, you can change the base template of an existing MVC template to a Hybrid template.")]
    public string AllowPageTemplatesFrameworkChangeDescription => this["аllowPageTemplatesFrameworkChangeTitle"];

    [ResourceEntry("AllowChangePageThemeAtRuntimeTitle", Description = "", LastModified = "2019/04/04", Value = "Allow changing page theme at runtime")]
    public string AllowChangePageThemeAtRuntimeTitle => this[nameof (AllowChangePageThemeAtRuntimeTitle)];

    [ResourceEntry("AllowChangePageThemeAtRuntimeDescription", Description = "", LastModified = "2019/04/04", Value = "Allows to set the page theme/package at runtime via Query String param. Please consider this setting only for development purpose, as it might cause performance and security issues when using on production.")]
    public string AllowChangePageThemeAtRuntimeDescription => this[nameof (AllowChangePageThemeAtRuntimeDescription)];

    [ResourceEntry("EnableAdminAppWidgetEditorsTitle", Description = "", LastModified = "2019/04/23", Value = "Enable new Admin App widget designers")]
    public string EnableAdminAppWidgetEditorsTitle => this[nameof (EnableAdminAppWidgetEditorsTitle)];

    [ResourceEntry("EnableAdminAppWidgetEditorsDescription", Description = "", LastModified = "2019/04/23", Value = "Enables the new Admin App widget designers to be displayed instead of the classic webforms and mvc ones.")]
    public string EnableAdminAppWidgetEditorsDescription => this[nameof (EnableAdminAppWidgetEditorsDescription)];

    [ResourceEntry("WhitelistedAdminAppWidgetEditorsTitle", Description = "", LastModified = "2019/06/05", Value = "Enable new Admin App widget designers for specific widgets")]
    public string WhitelistedAdminAppWidgetEditorsTitle => this[nameof (WhitelistedAdminAppWidgetEditorsTitle)];

    [ResourceEntry("WhitelistedAdminAppWidgetEditorsDescription", Description = "", LastModified = "2019/06/05", Value = "Enables the new Admin App widget designers to be displayed instead of the classic mvc ones for the whitelisted widgets. Divide the widget names with commas.")]
    public string WhitelistedAdminAppWidgetEditorsDescription => this[nameof (WhitelistedAdminAppWidgetEditorsDescription)];

    [ResourceEntry("IgnoreOutputCacheUserGroupTitle", Description = "", LastModified = "2019/04/15", Value = "Disable output cache for:")]
    public string IgnoreOutputCacheUserGroupTitle => this[nameof (IgnoreOutputCacheUserGroupTitle)];

    [ResourceEntry("VaryByUserAgentTitle", Description = "", LastModified = "2015/11/11", Value = "Vary by user agent")]
    public string VaryByUserAgentTitle => this[nameof (VaryByUserAgentTitle)];

    [ResourceEntry("VaryByUserAgentDescription", Description = "", LastModified = "2015/11/11", Value = "Indicate whether the cache should vary by user agent (browser) header.  Applies to Response.Cache.VaryByHeaders.UserAgent ASP.NET property.")]
    public string VaryByUserAgentDescription => this["EnableOutputCacheForAuthenticatedUsersDescription"];

    [ResourceEntry("VaryByHostTitle", Description = "", LastModified = "2015/12/14", Value = "Vary by host")]
    public string VaryByHostTitle => this[nameof (VaryByHostTitle)];

    [ResourceEntry("VaryByHostDescription", Description = "", LastModified = "2016/12/23", Value = "Indicate whether the cache should vary by host header. Applies to Response.Cache.VaryByHeaders[\"host\"] ASP.NET property.")]
    public string VaryByHostDescription => this[nameof (VaryByHostDescription)];

    [ResourceEntry("VaryByCustomTitle", Description = "", LastModified = "2015/12/14", Value = "Vary by custom")]
    public string VaryByCustomTitle => this[nameof (VaryByCustomTitle)];

    [ResourceEntry("VaryByCustomDescription", Description = "", LastModified = "2016/12/23", Value = "Gets a custom string key that allows to specify varying the output cache by specific context information. The key can be used later in the global application class by overriding GetVaryByCustomString method and specifying the behavior of the output cache for the custom string.")]
    public string VaryByCustomDescription => this[nameof (VaryByCustomDescription)];

    /// <summary>Gets a message: Custom error pages</summary>
    [ResourceEntry("ErrorPagesModeTitle", Description = "Indicates whether Custom Error Pages are enabled", LastModified = "2019/09/13", Value = "Custom error pages")]
    public string ErrorPagesModeTitle => this[nameof (ErrorPagesModeTitle)];

    /// <summary>
    /// Gets a message: If you want your site to use or not to use custom error pages, select "Enabled" or "Disabled", respectively. To display custom error pages to remote users, but to display the default ASP.NET error pages to localhost users, select "RemoteOnly".
    /// </summary>
    [ResourceEntry("ErrorPagesModeDescription", Description = "Indicates whether Custom Error Pages are enabled", LastModified = "2019/09/05", Value = "If you want your site to use or not to use custom error pages, select \"Enabled\" or \"Disabled\", respectively. To display custom error pages to remote users, but to display the default ASP.NET error pages to localhost users, select \"RemoteOnly\".")]
    public string ErrorPagesModeDescription => this[nameof (ErrorPagesModeDescription)];

    /// <summary>Gets a message: Error types</summary>
    [ResourceEntry("ErrorTypesTitle", Description = "Indicates whether Custom Error Pages are enabled", LastModified = "2019/10/03", Value = "Error types")]
    public string ErrorTypesTitle => this[nameof (ErrorTypesTitle)];

    /// <summary>Gets a message: Custom Error Pages</summary>
    [ResourceEntry("ErrorPagesElementTitle", Description = "Describes configuration element.", LastModified = "2019/08/22", Value = "Custom error pages")]
    public string ErrorPagesElementTitle => this[nameof (ErrorPagesElementTitle)];

    /// <summary>
    /// Gets a message: Specifies page settings for different HTTP errors, such as 404 or 500
    /// </summary>
    [ResourceEntry("ErrorPagesElementDescription", Description = "Describes configuration element.", LastModified = "2019/09/13", Value = "Specifies page settings for different HTTP errors, such as 404 or 500")]
    public string ErrorPagesElementDescription => this[nameof (ErrorPagesElementDescription)];

    /// <summary>Gets a message: Http Status Code</summary>
    [ResourceEntry("HttpStatusCodeTitle", Description = "Describes configuration element.", LastModified = "2019/10/09", Value = "HTTP status code")]
    public string HttpStatusCodeTitle => this[nameof (HttpStatusCodeTitle)];

    /// <summary>Gets a message: Redirect</summary>
    [ResourceEntry("RedirectToErrorPageTitle", Description = "Describes configuration element.", LastModified = "2019/08/22", Value = "Redirect")]
    public string RedirectToErrorPageTitle => this[nameof (RedirectToErrorPageTitle)];

    [ResourceEntry("InvalidErrorCode", Description = "Describes configuration element.", LastModified = "2019/10/09", Value = "Enter valid HTTP status code. See <a href=\"https://www.progress.com/documentation/sitefinity-cms/administration-custom-error-pages#supported-error-codes\" target=\"_blank\">all valid error HTTP status codes</a> supported by Sitefinity.")]
    public string InvalidErrorCode => this[nameof (InvalidErrorCode)];

    /// <summary>
    /// Gets a message: If selected, the user is redirected to the actual error page's URL, for example, ~/404. If not selected, the error page is rendered on the URL that the user is currently trying to open (recommended).
    /// </summary>
    [ResourceEntry("RedirectToErrorPageDescription", Description = "Describes configuration element.", LastModified = "2019/08/22", Value = "If selected, the user is redirected to the actual error page's URL, for example, ~/404. If not selected, the error page is rendered on the URL that the user is currently trying to open (recommended).")]
    public string RedirectToErrorPageDescription => this[nameof (RedirectToErrorPageDescription)];

    /// <summary>Gets a message: Error page URL name</summary>
    [ResourceEntry("PageNameTitle", Description = "Describes configuration element.", LastModified = "2019/09/13", Value = "Error page URL name")]
    public string PageNameTitle => this[nameof (PageNameTitle)];

    /// <summary>
    /// Gets a message: Example: 404. On the top level of each site, create a page with the same URL name. This page will be automatically used as an error page for the respective site. Details: [URL to documentation article]
    /// </summary>
    [ResourceEntry("PageNameDescription", Description = "Describes configuration element.", LastModified = "2019/09/13", Value = "Example: 404. On the top level of each site, create a page with the same URL name. This page will be automatically used as an error page for the respective site. Details: progress.com/sf-error-pages")]
    public string PageNameDescription => this[nameof (PageNameDescription)];

    /// <summary>Gets the data config description.</summary>
    /// <value>The data config description.</value>
    [ResourceEntry("DataConfigDescription", Description = "Describes configuration element.", LastModified = "2009/11/03", Value = "Data related configuration")]
    public string DataConfigDescription => this[nameof (DataConfigDescription)];

    /// <summary>Gets the data config title.</summary>
    /// <value>The data config title.</value>
    [ResourceEntry("DataConfigTitle", Description = "Describes configuration element.", LastModified = "2009/11/03", Value = "Data related configuration")]
    public string DataConfigTitle => this[nameof (DataConfigTitle)];

    /// <summary>Data Resolvers</summary>
    /// <value>The resolvers title.</value>
    [ResourceEntry("ResolversTitle", Description = "The caption displayed for Resolvers collection property.", LastModified = "2009/11/03", Value = "Data Resolvers")]
    public string ResolversTitle => this[nameof (ResolversTitle)];

    /// <summary>
    /// A collection of resolvers that help resolving data prosperities for data binding.
    /// </summary>
    /// <value>The resolvers description.</value>
    [ResourceEntry("ResolversDescription", Description = "The description of ResolversDescription configuration element.", LastModified = "2009/11/03", Value = "A collection of resolvers that help resolving data prosperities for data binding.")]
    public string ResolversDescription => this[nameof (ResolversDescription)];

    /// <summary>URL Evaluators</summary>
    /// <value>The URL evaluators title.</value>
    [ResourceEntry("UrlEvaluatorsTitle", Description = "The caption displayed for UrlEvaluators collection property.", LastModified = "2009/11/03", Value = "URL Evaluators")]
    public string UrlEvaluatorsTitle => this[nameof (UrlEvaluatorsTitle)];

    /// <summary>Data Processors</summary>
    /// <value>The caption displayed for DataProcessors collection property in advanced settings backend page.</value>
    [ResourceEntry("DataProcessorsTitle", Description = "The caption displayed for DataProcessors collection property in advanced settings backend page.", LastModified = "2017/04/14", Value = "Data Processors")]
    public string DataProcessorsTitle => this[nameof (DataProcessorsTitle)];

    /// <summary>
    /// Data Processors are executed every time a change is made to a field in the database. They are executed and filtered against the database model object`s properties.
    /// </summary>
    /// <value>The description displayed for DataProcessors collection property in advanced settings backend page.</value>
    [ResourceEntry("DataProcessorsDescription", Description = "The description displayed for DataProcessors collection property in advanced settings backend page.", LastModified = "2017/04/14", Value = "Data Processors are executed every time a change is made to a field in the database. They are executed and filtered against the database model object`s properties.")]
    public string DataProcessorsDescription => this[nameof (DataProcessorsDescription)];

    /// <summary>
    /// A collection of URL evaluators that help converting URLs to filter expressions.
    /// </summary>
    /// <value>The URL evaluators description.</value>
    [ResourceEntry("UrlEvaluatorsDescription", Description = "The description of the URL Evaluator collection property.", LastModified = "2009/11/03", Value = "A collection of URL evaluators that help converting URLs to filter expressions.")]
    public string UrlEvaluatorsDescription => this[nameof (UrlEvaluatorsDescription)];

    /// <summary>Gets the data config connection strings description.</summary>
    /// <value>The data config connection strings description.</value>
    [ResourceEntry("DataConfigConnectionStringsDescription", Description = "", LastModified = "2009/11/03", Value = "Contains connection")]
    public string DataConfigConnectionStringsDescription => this[nameof (DataConfigConnectionStringsDescription)];

    /// <summary>Gets the data config connection strings title.</summary>
    /// <value>The data config connection strings title.</value>
    [ResourceEntry("DataConfigConnectionStringsTitle", Description = "Describes configuration element.", LastModified = "2009/11/03", Value = "Connection strings")]
    public string DataConfigConnectionStringsTitle => this[nameof (DataConfigConnectionStringsTitle)];

    /// <summary>Gets the connection string literal.</summary>
    /// <value>The data config connection strings title.</value>
    [ResourceEntry("ConnectionStringTitle", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Connection string")]
    public string ConnectionStringTitle => this[nameof (ConnectionStringTitle)];

    /// <summary>In memory</summary>
    /// <value>The data config connection strings title.</value>
    [ResourceEntry("InMemoryCacheProviderTitle", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "In memory")]
    public string InMemoryCacheProviderTitle => this[nameof (InMemoryCacheProviderTitle)];

    /// <summary>Gets the data config EnableDataCaching description.</summary>
    [ResourceEntry("EnableDataCachingTitle", Description = "", LastModified = "2011/04/06", Value = "Enable data caching")]
    public string EnableDataCachingTitle => this[nameof (EnableDataCachingTitle)];

    /// <summary>Gets the data config EnableDataCaching description.</summary>
    [ResourceEntry("EnableDataCachingDescription", Description = "Describes configuration element.", LastModified = "2014/07/24", Value = "Enables Open Access data caching. By default it is turned on.")]
    public string EnableDataCachingDescription => this[nameof (EnableDataCachingDescription)];

    /// <summary>
    /// The title of the configuration element GuidGenerationStrategy in the DataConfig..
    /// </summary>
    [ResourceEntry("GuidGenerationStrategyTitle", Description = "The title of the configuration element GuidGenerationStrategy in the DataConfig.", LastModified = "2012/11/15", Value = "Guid generation strategy (Incremental or Random)")]
    public string GuidGenerationStrategyTitle => this[nameof (GuidGenerationStrategyTitle)];

    /// <summary>
    /// The description of the configuration element GuidGenerationStrategy in the DataConfig..
    /// </summary>
    [ResourceEntry("GuidGenerationStrategyDescription", Description = "The description of the configuration element GuidGenerationStrategy in the DataConfig.", LastModified = "2012/11/15", Value = "Specify the strategy, used by OpenAccess providers for generating new GUIDs for the persisted items. There are two options:  1) Incremental - generates an incremental unique identifier value (controlled by OpenAccess);  2) Random (the default) - generates a random unique identifier value with System.Guid.NewGuid().")]
    public string GuidGenerationStrategyDescription => this[nameof (GuidGenerationStrategyDescription)];

    /// <summary>phrase: Incremental GUID range</summary>
    /// <value>Incremental GUID range</value>
    [ResourceEntry("IncrementalGuidRangeTitle", Description = "phrase: Incremental GUID range", LastModified = "2012/12/14", Value = "Incremental GUID range")]
    public string IncrementalGuidRangeTitle => this[nameof (IncrementalGuidRangeTitle)];

    /// <summary>
    /// phrase: The value of the 'range' byte used for generation of incremental GUIDs
    /// </summary>
    [ResourceEntry("IncrementalGuidRangeDescription", Description = "phrase: The value of the 'range' byte used for generation of incremental GUIDs", LastModified = "2012/12/14", Value = "The value of the 'range' byte used for generation of incremental GUIDs")]
    public string IncrementalGuidRangeDescription => this[nameof (IncrementalGuidRangeDescription)];

    /// <summary>phrase: Disable meta type cache</summary>
    [ResourceEntry("DisableMetaTypeCacheTitle", Description = "phrase: Disable meta type cache", LastModified = "2015/04/02", Value = "Disable meta type cache")]
    public string DisableMetaTypeCacheTitle => this[nameof (DisableMetaTypeCacheTitle)];

    /// <summary>phrase: When checked the meta types are not cached.</summary>
    [ResourceEntry("DisableMetaTypeCacheDescription", Description = "phrase: When checked the meta types are not cached.", LastModified = "2015/04/02", Value = "When checked the meta types are not cached.")]
    public string DisableMetaTypeCacheDescription => this[nameof (DisableMetaTypeCacheDescription)];

    /// <summary>Gets the database mapping options title.</summary>
    /// <value>The database mapping options title.</value>
    [ResourceEntry("DatabaseMappingOptionsTitle", Description = "", LastModified = "2013/01/25", Value = "Database Mapping Options")]
    public string DatabaseMappingOptionsTitle => this[nameof (DatabaseMappingOptionsTitle)];

    /// <summary>Gets the use split tables for multilingual title.</summary>
    /// <value>The use split tables for multilingual title.</value>
    [ResourceEntry("UseMultilingualSplitTablesTitle", Description = "", LastModified = "2013/01/25", Value = "Use Split Tables")]
    public string UseMultilingualSplitTablesTitle => this[nameof (UseMultilingualSplitTablesTitle)];

    /// <summary>
    /// Gets the use split tables for multilingual description.
    /// </summary>
    /// <value>The use split tables for multilingual description.</value>
    [ResourceEntry("UseMultilingualSplitTablesDescription", Description = "Describes configuration element.", LastModified = "2013/01/25", Value = "Use split tables for multilingual fields. For backward compatability. It is not recommended to change this setting, as there is no migration path for already existing localizable fields.")]
    public string UseMultilingualSplitTablesDescription => this[nameof (UseMultilingualSplitTablesDescription)];

    /// <summary>Gets the use multilingual fetch strategy title.</summary>
    /// <value>The use multilingual fetch strategy title.</value>
    [ResourceEntry("UseMultilingualFetchStrategyTitle", Description = "", LastModified = "2013/01/25", Value = "Use Fetch Strategy")]
    public string UseMultilingualFetchStrategyTitle => this[nameof (UseMultilingualFetchStrategyTitle)];

    /// <summary>Gets the use multilingual fetch strategy description.</summary>
    /// <value>The use multilingual fetch strategy description.</value>
    [ResourceEntry("UseMultilingualFetchStrategyDescription", Description = "Describes configuration element.", LastModified = "2013/01/25", Value = "Use fetch strategy for multilingual fields. For backward compatability. The recommended value is true.")]
    public string UseMultilingualFetchStrategyDescription => this[nameof (UseMultilingualFetchStrategyDescription)];

    /// <summary>Gets the split tables ignored cultures title.</summary>
    /// <value>The split tables ignored cultures title.</value>
    [ResourceEntry("SplitTablesIgnoredCulturesTitle", Description = "Describes configuration element.", LastModified = "2018/11/23", Value = "Languages and cultures not persisted in split tables")]
    public string SplitTablesIgnoredCulturesTitle => this[nameof (SplitTablesIgnoredCulturesTitle)];

    /// <summary>Gets the split tables ignored cultures description.</summary>
    /// <value>The split tables ignored cultures description.</value>
    [ResourceEntry("SplitTablesIgnoredCulturesDescription", Description = "Describes configuration element.", LastModified = "2018/11/23", Value = "Enter in a comma separated list the languages and cultures that you do not want to be persisted in split tables. Example: en,de, \n de-de,fr,en-us \n First 5 languages are automatically added.Do not delete any of them.")]
    public string SplitTablesIgnoredCulturesDescription => this[nameof (SplitTablesIgnoredCulturesDescription)];

    /// <summary>Describes configuration element.</summary>
    /// <value>CLR Type</value>
    [ResourceEntry("L2CacheMappingTypeNameTitle", Description = "Describes configuration element.", LastModified = "2014/09/10", Value = "CLR Type")]
    public string L2CacheMappingTypeNameTitle => this[nameof (L2CacheMappingTypeNameTitle)];

    /// <summary>Describes configuration element.</summary>
    /// <value>The CLR type name to be configured.</value>
    [ResourceEntry("L2CacheMappingTypeNameDescription", Description = "Describes configuration element.", LastModified = "2014/09/10", Value = "The CLR type name to be configured.")]
    public string L2CacheMappingTypeNameDescription => this[nameof (L2CacheMappingTypeNameDescription)];

    /// <summary>Describes configuration element.</summary>
    /// <value>Cache Strategy</value>
    [ResourceEntry("L2CacheMappingCacheStrategyTitle", Description = "Describes configuration element.", LastModified = "2014/09/10", Value = "Cache Strategy")]
    public string L2CacheMappingCacheStrategyTitle => this[nameof (L2CacheMappingCacheStrategyTitle)];

    /// <summary>Describes configuration element.</summary>
    [ResourceEntry("L2CacheMappingCacheStrategyDescription", Description = "Describes configuration element.", LastModified = "2014/09/10", Value = "The cache strategy for the selected CLR type to be applied. Yes -> L2 cache is enabled for this type. No -> L2 cache is disabled for  this type. The Default value is equivalent to Yes.")]
    public string L2CacheMappingCacheStrategyDescription => this[nameof (L2CacheMappingCacheStrategyDescription)];

    /// <summary>Describes configuration element.</summary>
    /// <value>L2 Cache Strategies</value>
    [ResourceEntry("L2CacheTypeMappingsTitle", Description = "Describes configuration element.", LastModified = "2014/09/10", Value = "L2 Cache Strategies")]
    public string L2CacheTypeMappingsTitle => this[nameof (L2CacheTypeMappingsTitle)];

    /// <summary>Describes configuration element.</summary>
    /// <value>Allows configuration of L2 Cache for persistent types.</value>
    [ResourceEntry("L2CacheTypeMappingsDescription", Description = "Describes configuration element.", LastModified = "2014/09/10", Value = "Allows configuration of L2 Cache for persistent types.")]
    public string L2CacheTypeMappingsDescription => this[nameof (L2CacheTypeMappingsDescription)];

    /// <summary>Resource strings for widget class.</summary>
    [ResourceEntry("BlogsConfigCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Blogs class")]
    public string BlogsConfigCaption => this[nameof (BlogsConfigCaption)];

    /// <summary>Resource strings for blogs class.</summary>
    [ResourceEntry("BlogsConfigDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for blogs class.")]
    public string BlogsConfigDescription => this[nameof (BlogsConfigDescription)];

    /// <summary>Message: Blogs</summary>
    /// <value>The title of this class.</value>
    [ResourceEntry("BlogResourcesTitle", Description = "The title of this class.", LastModified = "2009/12/01", Value = "Blogs")]
    public string BlogResourcesTitle => this[nameof (BlogResourcesTitle)];

    /// <summary>
    /// Message: Contains localizable resources for the blogs module.
    /// </summary>
    /// <value>The description of this class</value>
    [ResourceEntry("BlogResourcesDescription", Description = "The description of this class.", LastModified = "2009/12/01", Value = "Contains localizable resources for the blogs module.")]
    public string BlogResourcesDescription => this[nameof (BlogResourcesDescription)];

    /// <summary>Resource strings for widget class.</summary>
    [ResourceEntry("ListsConfigCaption", Description = "The title of this class.", LastModified = "2011/02/25", Value = "Lists class")]
    public string ListsConfigCaption => this[nameof (ListsConfigCaption)];

    /// <summary>Resource strings for blogs class.</summary>
    [ResourceEntry("ListsConfigDescription", Description = "The description of this class.", LastModified = "2011/02/25", Value = "Resource strings for List class.")]
    public string ListsConfigDescription => this[nameof (ListsConfigDescription)];

    /// <summary>Gets the lists URL root caption.</summary>
    /// <value>The lists URL root caption.</value>
    [ResourceEntry("ListsUrlRootCaption", Description = "The title of the Url root.", LastModified = "2011/04/08", Value = "Url Root")]
    public string ListsUrlRootCaption => this[nameof (ListsUrlRootCaption)];

    /// <summary>phrase: Root constant in the list URL</summary>
    [ResourceEntry("ListsUrlRootDescription", Description = "The description of this class.", LastModified = "2011/04/08", Value = "Root constant in the list URL")]
    public string ListsUrlRootDescription => this[nameof (ListsUrlRootDescription)];

    /// <summary>Gets the lists URL root caption.</summary>
    /// <value>The lists URL root caption.</value>
    [ResourceEntry("ListItemsUrlRootCaption", Description = "The title of the Url root.", LastModified = "2011/04/08", Value = "Url Root")]
    public string ListItemsUrlRootCaption => this[nameof (ListItemsUrlRootCaption)];

    /// <summary>phrase: Root constant in the list item URL</summary>
    [ResourceEntry("ListItemsUrlRootDescription", Description = "The description of this class.", LastModified = "2011/04/08", Value = "Root constant in the list item URL")]
    public string ListItemsUrlRootDescription => this[nameof (ListItemsUrlRootDescription)];

    /// <summary>Resource strings for news class.</summary>
    [ResourceEntry("NewsConfigCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "News class")]
    public string NewsConfigCaption => this[nameof (NewsConfigCaption)];

    /// <summary>Resource strings for news class.</summary>
    [ResourceEntry("NewsConfigDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for news class.")]
    public string NewsConfigDescription => this[nameof (NewsConfigDescription)];

    /// <summary>Message: News</summary>
    /// <value>The title of this class.</value>
    [ResourceEntry("NewsResourcesTitle", Description = "The title of this class.", LastModified = "2009/12/11", Value = "News")]
    public string NewsResourcesTitle => this[nameof (NewsResourcesTitle)];

    /// <summary>
    /// Message: Contains localizable resources for the news module.
    /// </summary>
    /// <value>The description of this class</value>
    [ResourceEntry("NewsResourcesDescription", Description = "The description of this class.", LastModified = "2009/12/11", Value = "Contains localizable resources for the news module.")]
    public string NewsResourcesDescription => this[nameof (NewsResourcesDescription)];

    /// <summary>Resource strings for events class.</summary>
    [ResourceEntry("EventsConfigCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Events class")]
    public string EventsConfigCaption => this[nameof (EventsConfigCaption)];

    /// <summary>Resource strings for events class.</summary>
    [ResourceEntry("EventsConfigDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for events class.")]
    public string EventsConfigDescription => this[nameof (EventsConfigDescription)];

    /// <summary>AllowChangeCalendarViewCaption.</summary>
    [ResourceEntry("AllowChangeCalendarViewCaption", Description = "Resource for property: AllowChangeCalendarViewCaption", LastModified = "2013/03/29", Value = "Resource for property: AllowChangeCalendarViewCaption")]
    public string AllowChangeCalendarViewCaption => this[nameof (AllowChangeCalendarViewCaption)];

    /// <summary>AllowChangeCalendarViewDescription.</summary>
    [ResourceEntry("AllowChangeCalendarViewDescription", Description = "Description for property: AllowChangeCalendarViewDescription", LastModified = "2013/03/29", Value = "Description for property: AllowChangeCalendarViewDescription")]
    public string AllowChangeCalendarViewDescription => this[nameof (AllowChangeCalendarViewDescription)];

    /// <summary>AllowCreateEventsCaption.</summary>
    [ResourceEntry("AllowCreateEventsCaption", Description = "Resource for property: AllowCreateEventsCaption", LastModified = "2013/03/29", Value = "Resource for property: AllowCreateEventsCaption")]
    public string AllowCreateEventsCaption => this[nameof (AllowCreateEventsCaption)];

    /// <summary>AllowCreateEventsDescription.</summary>
    [ResourceEntry("AllowCreateEventsDescription", Description = "Description for property: AllowCreateEventsDescription", LastModified = "2013/03/29", Value = "Description for property: AllowCreateEventsDescription")]
    public string AllowCreateEventsDescription => this[nameof (AllowCreateEventsDescription)];

    /// <summary>AllowCreateCalendarsCaption.</summary>
    [ResourceEntry("AllowCreateCalendarsCaption", Description = "Resource for property: AllowCreateCalendarsCaption", LastModified = "2013/03/29", Value = "Resource for property: AllowCreateCalendarsCaption")]
    public string AllowCreateCalendarsCaption => this[nameof (AllowCreateCalendarsCaption)];

    /// <summary>AllowCreateCalendarsDescription.</summary>
    [ResourceEntry("AllowCreateCalendarsDescription", Description = "Description for property: AllowCreateCalendarsDescription", LastModified = "2013/03/29", Value = "Description for property: AllowCreateCalendarsDescription")]
    public string AllowCreateCalendarsDescription => this[nameof (AllowCreateCalendarsDescription)];

    /// <summary>AllowCalendarExportCaption.</summary>
    [ResourceEntry("AllowCalendarExportCaption", Description = "Resource for property: AllowCalendarExportCaption", LastModified = "2013/04/03", Value = "Resource for property: AllowCalendarExportCaption")]
    public string AllowCalendarExportCaption => this[nameof (AllowCalendarExportCaption)];

    /// <summary>AllowCalendarExportDescription.</summary>
    [ResourceEntry("AllowCalendarExportDescription", Description = "Description for property: AllowCalendarExportDescription", LastModified = "2013/04/03", Value = "Description for property: AllowCalendarExportDescription")]
    public string AllowCalendarExportDescription => this[nameof (AllowCalendarExportDescription)];

    /// <summary>DefaultCalendarViewCaption.</summary>
    [ResourceEntry("DefaultCalendarViewCaption", Description = "Resource for property: DefaultCalendarViewCaption", LastModified = "2013/03/29", Value = "Resource for property: DefaultCalendarViewCaption")]
    public string DefaultCalendarViewCaption => this[nameof (DefaultCalendarViewCaption)];

    /// <summary>DefaultCalendarViewDescription.</summary>
    [ResourceEntry("DefaultCalendarViewDescription", Description = "Description for property: DefaultCalendarViewDescription", LastModified = "2013/03/29", Value = "Description for property: DefaultCalendarViewDescription")]
    public string DefaultCalendarViewDescription => this[nameof (DefaultCalendarViewDescription)];

    /// <summary>Resource for property: FirstDayOfWeek</summary>
    /// <value>First day of week</value>
    [ResourceEntry("FirstDayOfWeekCaption", Description = "Resource for property: FirstDayOfWeek", LastModified = "2013/04/02", Value = "First day of week")]
    public string FirstDayOfWeekCaption => this[nameof (FirstDayOfWeekCaption)];

    /// <summary>Description for property: FirstDayOfWeek</summary>
    /// <value>Specifies the first day of the week of the events calendar</value>
    [ResourceEntry("FirstDayOfWeekDescription", Description = "Description for property: FirstDayOfWeek", LastModified = "2013/04/02", Value = "Specifies the first day of the week of the events calendar")]
    public string FirstDayOfWeekDescription => this[nameof (FirstDayOfWeekDescription)];

    /// <summary>Resource for property: LastDayOfWeek</summary>
    /// <value>Last day of week</value>
    [ResourceEntry("LastDayOfWeekCaption", Description = "Resource for property: LastDayOfWeek", LastModified = "2013/04/02", Value = "Last day of week")]
    public string LastDayOfWeekCaption => this[nameof (LastDayOfWeekCaption)];

    /// <summary>Description for property: LastDayOfWeek</summary>
    /// <value>Specifies the last day of the week of the events calendar</value>
    [ResourceEntry("LastDayOfWeekDescription", Description = "Description for property: LastDayOfWeek", LastModified = "2013/04/02", Value = "Specifies the last day of the week of the events calendar")]
    public string LastDayOfWeekDescription => this[nameof (LastDayOfWeekDescription)];

    /// <summary>EnableNotifications.</summary>
    [ResourceEntry("EnableNotifications", Description = "The title of the configuration element.", LastModified = "2018/11/19", Value = "Enable notifications")]
    public string EnableNotifications => this[nameof (EnableNotifications)];

    /// <summary>EnableFormNotificationsDescription.</summary>
    [ResourceEntry("EnableFormNotificationsDescription", Description = "The title of the configuration element.", LastModified = "2018/11/19", Value = "Enable email notifications for submitted or modified form responses. Recepients of notifications can be specified for each form.")]
    public string EnableFormNotificationsDescription => this[nameof (EnableFormNotificationsDescription)];

    /// <summary>SenderProfile.</summary>
    [ResourceEntry("SenderProfile", Description = "The title of the configuration element.", LastModified = "2018/11/19", Value = "Sender profile")]
    public string SenderProfile => this[nameof (SenderProfile)];

    /// <summary>EnableDetailedNotificationMessageTitle.</summary>
    [ResourceEntry("EnableDetailedNotificationMessageTitle", Description = "The title of the configuration element.", LastModified = "2018/11/19", Value = "Include a copy of the submitted information in the notification message")]
    public string EnableDetailedNotificationMessageTitle => this[nameof (EnableDetailedNotificationMessageTitle)];

    /// <summary>EnableDetailedNotificationMessageDescription.</summary>
    [ResourceEntry("EnableDetailedNotificationMessageDescription", Description = "Describes configuration element.", LastModified = "2018/11/19", Value = "In case email notifications are enabled and this option is selected, all the information submitted by the user will be included in the notification email. Be careful not to expose sensitive information.")]
    public string EnableDetailedNotificationMessageDescription => this[nameof (EnableDetailedNotificationMessageDescription)];

    /// <summary>FormEntrySubmittedNotificationTemplateIdTitle.</summary>
    [ResourceEntry("FormEntrySubmittedNotificationTemplateIdTitle", Description = "The title of the configuration element.", LastModified = "2018/11/22", Value = "Notification template for a new form response")]
    public string FormEntrySubmittedNotificationTemplateIdTitle => this[nameof (FormEntrySubmittedNotificationTemplateIdTitle)];

    /// <summary>UpdatedFormEntrySubmittedNotificationTemplateIdTitle.</summary>
    [ResourceEntry("UpdatedFormEntrySubmittedNotificationTemplateIdTitle", Description = "The title of the configuration element.", LastModified = "2018/11/22", Value = "Notification template for a modified form response")]
    public string UpdatedFormEntrySubmittedNotificationTemplateIdTitle => this[nameof (UpdatedFormEntrySubmittedNotificationTemplateIdTitle)];

    /// <summary>Resource strings for FormsConfig class.</summary>
    [ResourceEntry("FormsConfigCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "FormsConfig class")]
    public string FormsConfigCaption => this[nameof (FormsConfigCaption)];

    /// <summary>Resource strings for FormsConfig class.</summary>
    [ResourceEntry("FormsConfigDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for FormsConfig class.")]
    public string FormsConfigDescription => this[nameof (FormsConfigDescription)];

    /// <summary>
    /// Message: A collection of metafield to database field mappings.
    /// </summary>
    [ResourceEntry("DatabaseMappings", Description = "Describes configuration element.", LastModified = "2010/07/20", Value = "A collection of metafield to database field mappings.")]
    public string DatabaseMappings => this[nameof (DatabaseMappings)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DatabaseMappingsElementTitle", Description = "The title of this class.", LastModified = "2010/07/20", Value = "DatabaseMappingsElement class.")]
    public string DatabaseMappingsElementTitle => this[nameof (DatabaseMappingsElementTitle)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DatabaseMappingsElementDescription", Description = "The descripotion of this class.", LastModified = "2010/07/20", Value = "Resource strings for DatabaseMappingsElement class.")]
    public string DatabaseMappingsElementDescription => this[nameof (DatabaseMappingsElementDescription)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("ClrTypeCaption", Description = "The title of the ClrType property", LastModified = "2010/07/20", Value = "CLR type")]
    public string ClrTypeCaption => this[nameof (ClrTypeCaption)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("ClrTypeDescription", Description = "The description of the ClrType property.", LastModified = "2010/07/20", Value = "CLR type of the field")]
    public string ClrTypeDescription => this[nameof (ClrTypeDescription)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DbLengthCaption", Description = "The title of DbLength property.", LastModified = "2010/07/20", Value = "Database length")]
    public string DbLengthCaption => this[nameof (DbLengthCaption)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DbLengthDescription", Description = "The description of the  DbLength property.", LastModified = "2012/01/05", Value = "The maximum allowed length of the field.")]
    public string DbLengthDescription => this[nameof (DbLengthDescription)];

    /// <summary>phrase: Field precision</summary>
    [ResourceEntry("DbPrecisionCaption", Description = "phrase: Field precision", LastModified = "2010/12/10", Value = "Field precision")]
    public string DbPrecisionCaption => this[nameof (DbPrecisionCaption)];

    /// <summary>phrase: The precision of the numeric DB type.</summary>
    [ResourceEntry("DbPrecisionDescription", Description = "phrase: The precision of the numeric DB type.", LastModified = "2010/12/10", Value = "The precision of the numeric DB type.")]
    public string DbPrecisionDescription => this[nameof (DbPrecisionDescription)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DbScaleCaption", Description = "The title ofthe  DbScale property", LastModified = "2010/07/20", Value = "Field scale.")]
    public string DbScaleCaption => this[nameof (DbScaleCaption)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DbScaleDescription", Description = "The description of the DbScale property", LastModified = "2010/07/20", Value = "The scale of the field.")]
    public string DbScaleDescription => this[nameof (DbScaleDescription)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DbSqlTypeCaption", Description = "The title of the DbSqlType property", LastModified = "2010/07/20", Value = "SQL specific type.")]
    public string DbSqlTypeCaption => this[nameof (DbSqlTypeCaption)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DbSqlTypeDescription", Description = "The description of the DbSqlType property", LastModified = "2010/07/20", Value = "Specifies SQL Server-specific data type of a field.")]
    public string DbSqlTypeDescription => this[nameof (DbSqlTypeDescription)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DbTypeCaption", Description = "The title of the DbType property", LastModified = "2010/07/20", Value = "Data type.")]
    public string DbTypeCaption => this[nameof (DbTypeCaption)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("DbTypeDescription", Description = "The description of the DbType property.", LastModified = "2010/07/20", Value = "Specifies the data type of a field.")]
    public string DbTypeDescription => this[nameof (DbTypeDescription)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("NullableCaption", Description = "The title of the Nullable property.", LastModified = "2010/07/20", Value = "Nullable")]
    public string NullableCaption => this[nameof (NullableCaption)];

    /// <summary>Resource strings for DatabaseMappingsElement</summary>
    [ResourceEntry("NullableDescription", Description = "The description of the Nullable property.", LastModified = "2010/07/20", Value = "Determines whether the column allows null values.")]
    public string NullableDescription => this[nameof (NullableDescription)];

    /// <summary>phrase: Include in indices</summary>
    [ResourceEntry("IndexedCaption", Description = "phrase: Include in indices", LastModified = "2010/12/10", Value = "Include in indices")]
    public string IndexedCaption => this[nameof (IndexedCaption)];

    /// <summary>
    /// phrase: Indicates whether the DB column should be indexed.
    /// </summary>
    [ResourceEntry("IndexedDescription", Description = "phrase: Indicates whether the DB column should be indexed.", LastModified = "2012/01/05", Value = "Indicates whether the DB column should be indexed.")]
    public string IndexedDescription => this[nameof (IndexedDescription)];

    /// <summary>phrase: Column name</summary>
    [ResourceEntry("ColumnNameCaption", Description = "phrase: Column name", LastModified = "2010/12/10", Value = "Column name")]
    public string ColumnNameCaption => this[nameof (ColumnNameCaption)];

    /// <summary>phrase: The name of the DB column.</summary>
    [ResourceEntry("ColumnNameDescription", Description = "phrase: The name of the DB column.", LastModified = "2010/12/10", Value = "The name of the DB column.")]
    public string ColumnNameDescription => this[nameof (ColumnNameDescription)];

    /// <summary>Resource strings for widget class.</summary>
    [ResourceEntry("LibrariesConfigCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Libraries class")]
    public string LibrariesConfigCaption => this[nameof (LibrariesConfigCaption)];

    /// <summary>Resource strings for Libraries class.</summary>
    [ResourceEntry("LibrariesConfigDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for Libraries class.")]
    public string LibrariesConfigDescription => this[nameof (LibrariesConfigDescription)];

    /// <summary>AllowedExtensionSettings</summary>
    [ResourceEntry("AllowedExtensionSettingsTitle", Description = "The title of the configuration element.", LastModified = "2011/10/24", Value = "Allowed file extensions")]
    public string AllowedExtensionSettings => this[nameof (AllowedExtensionSettings)];

    /// <summary>
    /// Represents allowed file name extensions property collection as comma separated string.
    /// </summary>
    [ResourceEntry("AllowedExtensionSettingsDescription", Description = "Describes configuration element", LastModified = "2010/02/23", Value = "Represents allowed file name extensions property collection as comma separated string.")]
    public string AllowedExtensionSettingsDescription => this[nameof (AllowedExtensionSettingsDescription)];

    /// <summary>phrase: Allowed file types</summary>
    [ResourceEntry("AllowedTempFileFoldersSettingsTitle", Description = "phrase: Allowed folders for storing temp files", LastModified = "2018/11/26", Value = "Allowed folders for storing temp files")]
    public string AllowedTempFileFoldersSettingsTitle => this[nameof (AllowedTempFileFoldersSettingsTitle)];

    /// <summary>
    /// phrase: A comma separated list of the allowed attachment file extensions (leading dots included).
    /// </summary>
    [ResourceEntry("AllowedTempFileFoldersSettingsDescription", Description = "phrase: A comma separated list of allowed folders for storing temp files. For example: C:\\tempFiles, D:\\tempFiles.", LastModified = "2018/11/26", Value = "A comma separated list of allowed folders for storing temp files. For example: C:\\tempFiles, D:\\tempFiles.")]
    public string AllowedTempFileFoldersSettingsDescription => this[nameof (AllowedTempFileFoldersSettingsDescription)];

    /// <summary>Maximum allowed size</summary>
    [ResourceEntry("AllowedMaxSizeTitle", Description = "Maximum allowed size", LastModified = "2016/07/22", Value = "Maximum allowed size")]
    public string AllowedMaxSizeTitle => this[nameof (AllowedMaxSizeTitle)];

    /// <summary>Maximum allowed size description</summary>
    [ResourceEntry("AllowedMaxSizeDescription", Description = "Represents maximum allowed size of single library element in KiloBytes. This value is used only if maximum element size is not explicity specified during library creation. If value is set to 0, maximum size is not limited.", LastModified = "2016/07/22", Value = "Represents maximum allowed size of single library element in KiloBytes. This value is used only if maximum element size is not explicity specified during library creation. If value is set to 0, maximum size is not limited.")]
    public string AllowedMaxSizeDescription => this[nameof (AllowedMaxSizeDescription)];

    /// <summary>MaxFileSize</summary>
    [ResourceEntry("MaxFileSizeTitle", Description = "The title of the configuration element.", LastModified = "2012/02/18", Value = "Max file upload size")]
    public string MaxFileSizeTitle => this[nameof (MaxFileSizeTitle)];

    /// <summary>MaxFileSize</summary>
    [ResourceEntry("MaxFileSizeDescription", Description = "Describes configuration element", LastModified = "2012/02/18", Value = "Represents allowed file upload size.")]
    public string MaxFileSizeDescription => this[nameof (MaxFileSizeDescription)];

    /// <summary>DefaultForumAttachmentsBlobStorageProviderTitle</summary>
    [ResourceEntry("DefaultForumAttachmentsBlobStorageProviderTitle", Description = "The title of the configuration element.", LastModified = "2012/05/18", Value = "The name of the blob storage provider for forum attachments.")]
    public string DefaultForumAttachmentsBlobStorageProviderTitle => this[nameof (DefaultForumAttachmentsBlobStorageProviderTitle)];

    /// <summary>DefaultForumAttachmentsBlobStorageProviderDescription</summary>
    [ResourceEntry("DefaultForumAttachmentsBlobStorageProviderDescription", Description = "Describes configuration element", LastModified = "2012/05/18", Value = "The name of the blob storage provider for forum attachments.")]
    public string DefaultForumAttachmentsBlobStorageProviderDescription => this[nameof (DefaultForumAttachmentsBlobStorageProviderDescription)];

    /// <summary>AllowedExensions</summary>
    [ResourceEntry("AllowedExensionsCaption", Description = "The title of the configuration element.", LastModified = "2011/10/24", Value = "Enable extension filtering")]
    public string AllowedExensionsCaption => this[nameof (AllowedExensionsCaption)];

    /// <summary>
    /// Represents a value indicating whether to enable allowed file extensions.
    /// </summary>
    [ResourceEntry("AllowedExensionsDescription", Description = "Describes configuration element", LastModified = "2011/10/24", Value = "When True, the allowed extensions filter will be enabled.")]
    public string AllowedExensionsDescription => this[nameof (AllowedExensionsDescription)];

    /// <summary>AllowedExensions</summary>
    [ResourceEntry("AllowDynamicResizingTitle", Description = "The title of the configuration element AllowDynamicResizing", LastModified = "2012/04/04", Value = "Allow dynamic resizing of images")]
    public string AllowDynamicResizingTitle => this[nameof (AllowDynamicResizingTitle)];

    /// <summary>
    /// Describes the configuration element AllowDynamicResizing
    /// </summary>
    /// <value>Allows to generate image thumbnails by URL parameters or thumbnail profile name</value>
    [ResourceEntry("AllowDynamicResizingDescription", Description = "Describes the configuration element AllowDynamicResizing", LastModified = "2014/01/29", Value = "Allows to generate image thumbnails by URL parameters or thumbnail profile name")]
    public string AllowDynamicResizingDescription => this[nameof (AllowDynamicResizingDescription)];

    /// <summary>Allow unsigned dynamic resizing of images</summary>
    [ResourceEntry("AllowUnsignedDynamicResizingTitle", Description = "The title of the configuration element AllowUnsignedDynamicResizingTitle", LastModified = "2019/08/06", Value = "Allow unsigned dynamic resizing of images")]
    public string AllowUnsignedDynamicResizingTitle => this[nameof (AllowUnsignedDynamicResizingTitle)];

    /// <summary>
    /// Describes the configuration element AllowUnsignedDynamicResizingDescription
    /// </summary>
    /// <value>Allows to generate image thumbnails by URL parameters or thumbnail profile name</value>
    [ResourceEntry("AllowUnsignedDynamicResizingDescription", Description = "Describes the configuration element AllowUnsignedDynamicResizingDescription", LastModified = "2019/08/06", Value = "Allows to generate image by passing URL parameter size(without a signiture). This is legacy functionality and if is enables it may cause performance problems. We don't recommend to enable this configuration!")]
    public string AllowUnsignedDynamicResizingDescription => this[nameof (AllowUnsignedDynamicResizingDescription)];

    /// <summary>
    /// Describes the configuration element EnableImageUrlSignatureTitle
    /// </summary>
    /// <value>Enable signature in the image URL</value>
    [ResourceEntry("EnableImageUrlSignatureTitle", Description = "", LastModified = "2014/01/29", Value = "Enable signature in the image URL")]
    public string EnableImageUrlSignatureTitle => this[nameof (EnableImageUrlSignatureTitle)];

    /// <summary>Enable or disable search on all languages</summary>
    /// <value>Enable multilingual search results in media libraries</value>
    [ResourceEntry("EnableAllLanguagesSearchTitle", Description = "", LastModified = "2014/07/10", Value = "Enable multilingual search results in media libraries")]
    public string EnableAllLanguagesSearchTitle => this[nameof (EnableAllLanguagesSearchTitle)];

    /// <summary>The description of this class.</summary>
    /// <value>Gets or sets whether the search results will include language versions of media files, independently of the currently selected language.</value>
    [ResourceEntry("EnableAllLanguagesSearchDescription", Description = "The description of this class.", LastModified = "2014/07/15", Value = "Gets or sets whether the search results will include language versions of media files, independently of the currently selected language.")]
    public string EnableAllLanguagesSearchDescription => this[nameof (EnableAllLanguagesSearchDescription)];

    [ResourceEntry("ImageUrlSignatureHashAlgorithmTitle", Description = "", LastModified = "2015/01/13", Value = "Image URL Signature hash algorithm")]
    public string ImageUrlSignatureHashAlgorithmTitle => this[nameof (ImageUrlSignatureHashAlgorithmTitle)];

    [ResourceEntry("ImageUrlSignatureHashAlgorithmDescription", Description = "", LastModified = "2015/01/13", Value = "Sets the hash algorithm for image url signature. Currently supported algorithms are MD5 and SHA1")]
    public string ImageUrlSignatureHashAlgorithmDescription => this[nameof (ImageUrlSignatureHashAlgorithmDescription)];

    /// <summary>
    /// Enable or disable search only in selected folder and its subfolders
    /// </summary>
    /// <value>Enable search results only in selected folder and its subfolders</value>
    [ResourceEntry("EnableSelectedFolderSearchTitle", Description = "", LastModified = "2014/11/24", Value = "Enable search results only in selected folder and its subfolders")]
    public string EnableSelectedFolderSearchTitle => this[nameof (EnableSelectedFolderSearchTitle)];

    /// <summary>The description of this class.</summary>
    /// <value>Gets or sets whether the search results will include media files only in the selected folder and its subfolders.</value>
    [ResourceEntry("EnableSelectedFolderSearchDescription", Description = "The description of this class.", LastModified = "2014/11/24", Value = "Gets or sets whether the search results will include media files only in the selected folder and its subfolders.")]
    public string EnableSelectedFolderSearchDescription => this[nameof (EnableSelectedFolderSearchDescription)];

    /// <summary>Enable or disable one click upload</summary>
    /// <value>Enable one click upload</value>
    [ResourceEntry("EnableOneClickUploadTitle", Description = "", LastModified = "2014/11/27", Value = "Enable one click upload")]
    public string EnableOneClickUploadTitle => this[nameof (EnableOneClickUploadTitle)];

    /// <summary>The description of this class.</summary>
    /// <value>Gets or sets whether to upload media with one click.</value>
    [ResourceEntry("EnableOneClickUploadDescription", Description = "The description of this class.", LastModified = "2014/11/27", Value = "Gets or sets whether to upload media with one click.")]
    public string EnableOneClickUploadDescription => this[nameof (EnableOneClickUploadDescription)];

    /// <summary>Gets the libraries URL root caption.</summary>
    /// <value>The libraries URL root caption.</value>
    [ResourceEntry("LibrariesUrlRootCaption", Description = "The title of the Url root.", LastModified = "2010/02/22", Value = "Url Root")]
    public string LibrariesUrlRootCaption => this[nameof (LibrariesUrlRootCaption)];

    /// <summary>The description of this class.</summary>
    [ResourceEntry("LibrariesUrlRootDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Root constant in the items URL")]
    public string LibrariesUrlRootDescription => this[nameof (LibrariesUrlRootDescription)];

    /// <summary>
    /// Whether additional library URLs point directly to the files.
    /// </summary>
    /// <value>Additional URLs point to files</value>
    [ResourceEntry("AdditionalUrlsToFilesTitle", Description = "", LastModified = "2015/03/30", Value = "Media items additional URLs relative to site root (not to a containing page).")]
    public string AdditionalUrlsToFilesTitle => this[nameof (AdditionalUrlsToFilesTitle)];

    /// <summary>The description of this checkbox.</summary>
    [ResourceEntry("AdditionalUrlsToFilesDescription", Description = "The description of this checkbox.", LastModified = "2015/03/30", Value = "Whether additional URLs of a library item will directly open the file, no matter the format of the URL. \r\nWhen additional URL is accessed, then the server will respond with a permanent redirect to the primary URL of the file resource. \r\nThis option requires restart.")]
    public string AdditionalUrlsToFilesDescription => this[nameof (AdditionalUrlsToFilesDescription)];

    /// <summary>
    /// Gets the number of hours to be set at the 'Expire' http header.
    /// </summary>
    /// <value>The number of hours to be set at the 'Expire' http header.</value>
    [ResourceEntry("ExpireCacheAfterHoursCaption", Description = "The number of hours to be set at the 'Expire' http header.", LastModified = "2011/02/22", Value = "ExpireCacheAfterHours")]
    public string ExpireCacheAfterHoursCaption => this[nameof (ExpireCacheAfterHoursCaption)];

    /// <summary>The description of this class.</summary>
    [ResourceEntry("ExpireCacheAfterHoursDescription", Description = "The description of this class.", LastModified = "2011/02/22", Value = "The number of hours to be set at the 'Expire' http header")]
    public string ExpireCacheAfterHoursDescription => this[nameof (ExpireCacheAfterHoursDescription)];

    /// <summary>Resource strings for thmbnail types</summary>
    [ResourceEntry("ThumbnailTypeCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Thumbnail type")]
    public string ThumbnailTypeCaption => this[nameof (ThumbnailTypeCaption)];

    /// <summary>Resource strings for thmbnail types</summary>
    [ResourceEntry("ThumbnailTypeDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Thumbnail types for the master view")]
    public string ThumbnailTypeDescription => this[nameof (ThumbnailTypeDescription)];

    /// <summary>phrase: Show download link above description</summary>
    [ResourceEntry("ShowDownloadLinkAboveDescriptionCaption", Description = "phrase: Show download link above description", LastModified = "2010/10/22", Value = "Show download link above description")]
    public string ShowDownloadLinkAboveDescriptionCaption => this[nameof (ShowDownloadLinkAboveDescriptionCaption)];

    /// <summary>
    /// phrase: A value indicating whether to displays a download link above the description.
    /// </summary>
    [ResourceEntry("ShowDownloadLinkAboveDescriptionDescription", Description = "phrase: A value indicating whether to displays a download link above the description.", LastModified = "2011/03/11", Value = "A value indicating whether to displays a download link above the description.")]
    public string ShowDownloadLinkAboveDescriptionDescription => this[nameof (ShowDownloadLinkAboveDescriptionDescription)];

    /// <summary>phrase: Show download link below description</summary>
    [ResourceEntry("ShowDownloadLinkBelowDescriptionCaption", Description = "phrase: Show download link below description", LastModified = "2010/10/22", Value = "Show download link below description")]
    public string ShowDownloadLinkBelowDescriptionCaption => this[nameof (ShowDownloadLinkBelowDescriptionCaption)];

    /// <summary>
    /// phrase: A value indicating whether to displays a download link below the description.
    /// </summary>
    [ResourceEntry("ShowDownloadLinkBelowDescriptionDescription", Description = "phrase: A value indicating whether to displays a download link below the description.", LastModified = "2011/03/11", Value = "A value indicating whether to displays a download link below the description.")]
    public string ShowDownloadLinkBelowDescriptionDescription => this[nameof (ShowDownloadLinkBelowDescriptionDescription)];

    /// <summary>Resource strings for SingleItemWidth property.</summary>
    [ResourceEntry("SingleItemWidthCaption", Description = "The title SingleItemWidth property.", LastModified = "2010/02/22", Value = "Single item width")]
    public string SingleItemWidthCaption => this[nameof (SingleItemWidthCaption)];

    /// <summary>Gets or sets the width of single item (video, image)</summary>
    [ResourceEntry("SingleItemWidthDescription", Description = "The description of SingleItemWidth class.", LastModified = "2010/02/22", Value = "Gets or sets the width of single item (video, image)")]
    public string SingleItemWidthDescription => this[nameof (SingleItemWidthDescription)];

    /// <summary>
    /// Resource strings for SingleItemThumbnailName property.
    /// </summary>
    [ResourceEntry("SingleItemThumbnailsNameCaption", Description = "The title SingleItemThumbnailName property.", LastModified = "2013/06/20", Value = "Single item thumbnail name of the settings(profile/size)")]
    public string SingleItemThumbnailsNameCaption => this[nameof (SingleItemThumbnailsNameCaption)];

    /// <summary>
    /// Gets or sets the thumbnail name of the single item (video, image)
    /// </summary>
    [ResourceEntry("SingleItemThumbnailsNameDescription", Description = "The description of SingleItemThumbnailsName property.", LastModified = "2013/06/20", Value = "Gets or sets the thumbnail name of the single item (video, image)")]
    public string SingleItemThumbnailsNameDescription => this[nameof (SingleItemThumbnailsNameDescription)];

    /// <summary>Resource strings for the "Thumbnails width" property</summary>
    [ResourceEntry("ThumbnailsWidthCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Thumbnails width")]
    public string ThumbnailsWidthCaption => this[nameof (ThumbnailsWidthCaption)];

    /// <summary>Resource strings for the "Thumbnails width" property</summary>
    [ResourceEntry("ThumbnailsWidthDescription", Description = "The title of this class.", LastModified = "2011/03/11", Value = "Thumbnails width description")]
    public string ThumbnailsWidthDescription => this[nameof (ThumbnailsWidthDescription)];

    /// <summary>Resource strings for the "Thumbnails name" property</summary>
    [ResourceEntry("ThumbnailsNameCaption", Description = "The title of this class.", LastModified = "2013/06/18", Value = "Thumbnails name")]
    public string ThumbnailsNameCaption => this[nameof (ThumbnailsNameCaption)];

    /// <summary>Resource strings for the "Thumbnails name" property</summary>
    [ResourceEntry("ThumbnailNamesDescription", Description = "The title of this class.", LastModified = "2013/06/18", Value = "The configured thumbnails name.")]
    public string ThumbnailNamesDescription => this[nameof (ThumbnailNamesDescription)];

    /// <summary>Resource strings for the "Canvas Height" property</summary>
    [ResourceEntry("CanvasHeightCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Canvas Height")]
    public string CanvasHeightCaption => this[nameof (CanvasHeightCaption)];

    /// <summary>Resource strings for the "Canvas Height" property</summary>
    [ResourceEntry("CanvasHeightDescription", Description = "The title of this class.", LastModified = "2011/03/11", Value = "Canvas width description")]
    public string CanvasHeightDescription => this[nameof (CanvasHeightDescription)];

    /// <summary>Resource strings for the "Thumbnails Height" property</summary>
    [ResourceEntry("ThumbnailsHeightCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Thumbnails Height")]
    public string ThumbnailsHeightCaption => this[nameof (ThumbnailsHeightCaption)];

    /// <summary>Resource strings for the "Thumbnails Height" property</summary>
    [ResourceEntry("ThumbnailsHeightDescription", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Thumbnails width description")]
    public string ThumbnailsHeightDescription => this[nameof (ThumbnailsHeightDescription)];

    /// <summary>
    /// Resource strings for the "Number Of Chunks To Commit" property
    /// </summary>
    [ResourceEntry("NumberOfChunksToCommitTitle", Description = "The title of this class.", LastModified = "2011/02/22", Value = "Number Of Chunks To Commit")]
    public string NumberOfChunksToCommitTitle => this[nameof (NumberOfChunksToCommitTitle)];

    /// <summary>
    /// Resource strings for the "Number Of Chunks To Commit" property
    /// </summary>
    [ResourceEntry("NumberOfChunksToCommitDescription", Description = "The title of this class.", LastModified = "2012/01/05", Value = "Number of chunks to commit when uploading media data.")]
    public string NumberOfChunksToCommitDescription => this[nameof (NumberOfChunksToCommitDescription)];

    /// <summary>
    /// Resource strings for the "Size Of Chunk(In bytes)" property
    /// </summary>
    [ResourceEntry("SizeOfChunkTitle", Description = "The title of this class.", LastModified = "2011/02/22", Value = "The size of a chunk in bytes when upload media data")]
    public string SizeOfChunkTitle => this[nameof (SizeOfChunkTitle)];

    /// <summary>
    /// Resource strings for the "Size Of Chunk(In bytes)" property
    /// </summary>
    [ResourceEntry("SizeOfChunkDescription", Description = "The title of this class.", LastModified = "2011/04/27", Value = "The size of a chunk in bytes when upload media data. It should be set to a value lower than the buffer size setting of the database engine. (e.g. MySQL database engine supports lower than 1MB by default).")]
    public string SizeOfChunkDescription => this[nameof (SizeOfChunkDescription)];

    /// <summary>Message: Defines the mime mappings.</summary>
    [ResourceEntry("MimeMappings", Description = "Describes configuration element.", LastModified = "2011/04/04", Value = "Defines the mime mappings configuration.")]
    public string MimeMappings => this[nameof (MimeMappings)];

    /// <summary>Message: Defines the byte range configurations.</summary>
    [ResourceEntry("ByteRangeSettings", Description = "Describes configuration element.", LastModified = "2015/11/26", Value = "Defines the byte range configurations.")]
    public string ByteRangeSettings => this[nameof (ByteRangeSettings)];

    /// <summary>Resource strings for the "Mime type" property</summary>
    [ResourceEntry("MimeTypeTitle", Description = "The title of this class.", LastModified = "2011/04/04", Value = "The mime type when upload media data")]
    public string MimeTypeTitle => this[nameof (MimeTypeTitle)];

    /// <summary>Resource strings for the "Mime type" property</summary>
    [ResourceEntry("MimeTypeDescription", Description = "The title of this class.", LastModified = "2011/04/04", Value = "The mime type of an media item when upload media data.")]
    public string MimeTypeDescription => this[nameof (MimeTypeDescription)];

    /// <summary>Resource strings for the "File extension" property</summary>
    [ResourceEntry("FileExtensionTitle", Description = "The title of this class.", LastModified = "2011/04/04", Value = "The file extension of a mime mapping settings.")]
    public string FileExtensionTitle => this[nameof (FileExtensionTitle)];

    /// <summary>
    /// The file extension property of a mime mapping settings
    /// </summary>
    [ResourceEntry("FileExtensionDescription", Description = "The title of this class.", LastModified = "2011/04/04", Value = "The file extension property of a mime mapping settings.")]
    public string FileExtensionDescription => this[nameof (FileExtensionDescription)];

    /// <summary>Gets the handler file system fallback title.</summary>
    /// <value>The handler file system fallback title.</value>
    [ResourceEntry("EnableFileSystemFallbackTitle", Description = "The title of HandlerFileSystemFallback.", LastModified = "2011/05/11", Value = "Enable file system fallback")]
    public string EnableFileSystemFallbackTitle => this[nameof (EnableFileSystemFallbackTitle)];

    /// <summary>Gets the handler file system fallback description.</summary>
    /// <value>The handler file system fallback description.</value>
    [ResourceEntry("EnableFileSystemFallbackDescription", Description = "The description of HandlerFileSystemFallback.", LastModified = "2012/01/05", Value = "If true and no media content item is found in the provider, the system will check for an existing file in the file system with the same path, and will throw HTTP exception otherwise.")]
    public string HandlerFileSystemFaEnableFileSystemFallbackDescriptionllbackDescription => this["EnableFileSystemFallbackDescription"];

    /// <summary>
    /// The title of the configuration property 'ShowOnlySystemLibraries'.
    /// </summary>
    [ResourceEntry("ShowOnlySystemLibrariesCaption", Description = "The title of the configuration property 'ShowOnlySystemLibraries'.", LastModified = "2012/02/16", Value = "Show only system libraries element")]
    public string ShowOnlySystemLibrariesCaption => this[nameof (ShowOnlySystemLibrariesCaption)];

    /// <summary>
    /// Describes the configuration property 'ShowOnlySystemLibraries'.
    /// </summary>
    [ResourceEntry("ShowOnlySystemLibrariesDescription", Description = "Describes the configuration property 'ShowOnlySystemLibraries'.", LastModified = "2012/02/16", Value = "The object that defines the ShowOnlySystemLibrariesDescription field.")]
    public string ShowOnlySystemLibrariesDescription => this[nameof (ShowOnlySystemLibrariesDescription)];

    /// <summary>The title of this class.</summary>
    /// <value>Profiles</value>
    [ResourceEntry("ThumbnailProfilesTitle", Description = "The title of this class.", LastModified = "2013/07/04", Value = "Profiles")]
    public string ThumbnailProfilesTitle => this[nameof (ThumbnailProfilesTitle)];

    /// <summary>The items count title</summary>
    [ResourceEntry("ItemsCountTitle", Description = "How many items should be displayed on the first load.", LastModified = "2015/02/04", Value = "Number of items to load")]
    public string ItemsCountTitle => this[nameof (ItemsCountTitle)];

    /// <summary>Description of the Libraries Items Count.</summary>
    [ResourceEntry("ItemsCountDescription", Description = "How many items should be displayed on the first load.", LastModified = "2015/02/04", Value = "How many items should be displayed on the first load. This configuration enables the control to load items only when required instead of loading all data. Default value is 0 - all items are loaded.")]
    public string ItemsCountDescription => this[nameof (ItemsCountDescription)];

    /// <summary>The description of this class.</summary>
    /// <value>Collection of different thumbnail configurations.</value>
    [ResourceEntry("ThumbnailProfilesTitleDescription", Description = "The description of this class.", LastModified = "2013/05/29", Value = "Collection of different thumbnail configurations.")]
    public string ThumbnailProfilesTitleDescription => this[nameof (ThumbnailProfilesTitleDescription)];

    /// <summary>The title for this element.</summary>
    /// <value>Title</value>
    [ResourceEntry("ThumbnailProfileTitle", Description = "The name for this element.", LastModified = "2013/05/29", Value = "Name")]
    public string ThumbnailProfileTitle => this[nameof (ThumbnailProfileTitle)];

    /// <summary>The description of this element.</summary>
    /// <value>The title of the set when displayed in the backend, i.e. “Thumbnails 16X160”.</value>
    [ResourceEntry("ThumbnailProfileTitleDescription", Description = "The description of this element.", LastModified = "2013/05/29", Value = "The title of the set when displayed in the backend, i.e. “Thumbnails 16X160”.")]
    public string ThumbnailProfileTitleDescription => this[nameof (ThumbnailProfileTitleDescription)];

    /// <summary>The developer name for the thumbnail.</summary>
    /// <value>Name</value>
    [ResourceEntry("ThumbnailProfileName", Description = "The developer name for the thumbnail.", LastModified = "2013/05/29", Value = "Name")]
    public string ThumbnailProfileName => this[nameof (ThumbnailProfileName)];

    /// <summary>The developer name for this thumbnail configuration.</summary>
    /// <value>This name will be used for generating thumbnail URL. It should be unique across Image libraries. No spaces allowed.</value>
    [ResourceEntry("ThumbnailProfileNameDescription", Description = "The developer name for this thumbnail configuration.", LastModified = "2013/05/29", Value = "This name will be used for generating thumbnail URL. It should be unique across Image libraries. No spaces allowed. Up to 10 symbols.")]
    public string ThumbnailProfileNameDescription => this[nameof (ThumbnailProfileNameDescription)];

    /// <summary>Sets the level of compression of the thumbnail .</summary>
    /// <value>Quality</value>
    [ResourceEntry("ThumbnailQualityTitle", Description = "Sets the level of compression of the thumbnail .", LastModified = "2013/05/27", Value = "Quality")]
    public string ThumbnailQualityTitle => this[nameof (ThumbnailQualityTitle)];

    /// <summary>The description of this element.</summary>
    /// <value>Sets the level of compression of the thumbnail.</value>
    [ResourceEntry("ThumbnailQualityDescription", Description = "The description of this element.", LastModified = "2013/05/27", Value = "Sets the level of compression of the thumbnail.")]
    public string ThumbnailQualityDescription => this[nameof (ThumbnailQualityDescription)];

    /// <summary>
    /// The resize method that is used when resizing the thumbnail.
    /// </summary>
    /// <value>Resize Method</value>
    [ResourceEntry("ThumbnailTransformMethodTitle", Description = "The resize method that is used when resizing the thumbnail. ", LastModified = "2013/05/29", Value = "Resize Method")]
    public string ThumbnailTransformMethodTitle => this[nameof (ThumbnailTransformMethodTitle)];

    /// <summary>The description of this element.</summary>
    /// <value>The resize method that is used when resizing the thumbnail.</value>
    [ResourceEntry("ThumbnailTransformMethodTitleDescription", Description = "The description of this element.", LastModified = "2013/05/29", Value = "The resize method that is used when resizing the thumbnail. ")]
    public string ThumbnailTransformMethodTitleDescription => this[nameof (ThumbnailTransformMethodTitleDescription)];

    /// <summary>
    /// Marks a profile as a default one when creating a new library.
    /// </summary>
    /// <value>IsDefault</value>
    [ResourceEntry("ThumbnailProfileDefaultTitle", Description = "Marks a profile as a default one when creating a new library.", LastModified = "2013/06/06", Value = "Marks a profile as a default one when creating a new library.")]
    public string ThumbnailProfileDefaultTitle => this[nameof (ThumbnailProfileDefaultTitle)];

    /// <summary>
    /// Marks a profile as a default for thumbnail generation when creating a new library.
    /// </summary>
    /// <value>IsDefault</value>
    [ResourceEntry("ThumbnailProfileDefaultDescription", Description = "Marks a profile as a default for thumbnail generation when creating a new library.", LastModified = "2013/06/06", Value = "Marks a profile as a default for thumbnail generation when creating a new library.")]
    public string ThumbnailProfileDefaultDescription => this[nameof (ThumbnailProfileDefaultDescription)];

    /// <summary>Message: Summary</summary>
    /// <value>The title of this class.</value>
    [ResourceEntry("SummaryResourcesTitle", Description = "The title of this class.", LastModified = "2009/12/03", Value = "Summary")]
    public string SummaryResourcesTitle => this[nameof (SummaryResourcesTitle)];

    [ResourceEntry("EnabledByteRangeTitle", Description = "", LastModified = "2014/03/26", Value = "Enabled")]
    public string EnabledByteRangeTitle => this[nameof (EnabledByteRangeTitle)];

    [ResourceEntry("EnabledByteRangeDescription", Description = "", LastModified = "2014/03/26", Value = "Enables byte range serving")]
    public string EnabledByteRangeDescription => this[nameof (EnabledByteRangeDescription)];

    /// <summary>
    /// Phrase: Html tag that will be used to display video in a content block. Allowed values: Embed, Video.
    /// </summary>
    /// <value>Html tag that will be used to display video in a content block. Allowed values: Embed, Video.</value>
    [ResourceEntry("ContentBlockVideoTag", Description = "Phrase: Html tag that will be used to display video in a content block. Allowed values: Object, Video.", LastModified = "2014/03/19", Value = "Html tag that will be used to display video in a content block. Allowed values: Embed, Video.")]
    public string ContentBlockVideoTag => this[nameof (ContentBlockVideoTag)];

    /// <summary>The title of the configuration property 'AutoPlay'.</summary>
    /// <value>AutoPlay</value>
    [ResourceEntry("AutoPlayTitle", Description = "The title of the configuration property 'AutoPlay'.", LastModified = "2015/06/24", Value = "AutoPlay")]
    public string AutoPlayTitle => this[nameof (AutoPlayTitle)];

    /// <summary>Describes the configuration property 'AutoPlay'.</summary>
    /// <value>Gets or sets whether to video to be automatically played.</value>
    [ResourceEntry("AutoPlayDescription", Description = "Describes the configuration property 'AutoPlay'.", LastModified = "2015/06/24", Value = "Gets or sets whether to video to be automatically played.")]
    public string AutoPlayDescription => this[nameof (AutoPlayDescription)];

    /// <summary>phrase: Sender profile</summary>
    [ResourceEntry("SenderProfileTitle", Description = "phrase: Sender profile", LastModified = "2019/06/28", Value = "Sender profile")]
    public string SenderProfileTitle => this[nameof (SenderProfileTitle)];

    /// <summary>
    /// phrase: If you leave this field blank, notifications are sent using the default notification profile and settings (recommended).
    /// </summary>
    [ResourceEntry("SenderProfileDesciption", Description = "phrase: If you leave this field blank, notifications are sent using the default notification profile and settings (recommended).", LastModified = "2019/06/28", Value = "If you leave this field blank, notifications are sent using the default notification profile and settings (recommended).")]
    public string SenderProfileDesciption => this[nameof (SenderProfileDesciption)];

    /// <summary>
    /// phrase: Enable users to subscribe for email notifications
    /// </summary>
    [ResourceEntry("NotificationsEnabledTitle", Description = "phrase: Enable users to subscribe for email notifications", LastModified = "2019/06/28", Value = "Enable users to subscribe for email notifications")]
    public string NotificationsEnabledTitle => this[nameof (NotificationsEnabledTitle)];

    /// <summary>
    /// phrase: You can modify the notification email templates in Administration &gt; Settings &gt; System email templates.
    /// </summary>
    [ResourceEntry("NotificationsEnabledDesciption", Description = "phrase: You can modify the notification email templates in Administration > Settings > System email templates.", LastModified = "2019/06/28", Value = "You can modify the notification email templates in Administration > Settings > System email templates.")]
    public string NotificationsEnabledDesciption => this[nameof (NotificationsEnabledDesciption)];

    /// <summary>Enable FileProcessor</summary>
    [ResourceEntry("EnableFileProcessor", Description = "Gets the phrase: Enabled", LastModified = "2017/02/13", Value = "Enabled")]
    public string EnableFileProcessor => this[nameof (EnableFileProcessor)];

    /// <summary>FileProcessor Name</summary>
    [ResourceEntry("FileProcessorName", Description = "The name of the configuration element.", LastModified = "2017/02/13", Value = "Name")]
    public string FileProcessorName => this[nameof (FileProcessorName)];

    /// <summary>FileProcessor description</summary>
    [ResourceEntry("FileProcessorDescription", Description = "The description of the configuration element.", LastModified = "2017/02/13", Value = "Description")]
    public string FileProcessorDescription => this[nameof (FileProcessorDescription)];

    /// <summary>FileProcessor type</summary>
    [ResourceEntry("FileProcessorType", Description = "The type of the configuration element.", LastModified = "2017/02/13", Value = "Type")]
    public string FileProcessorType => this[nameof (FileProcessorType)];

    /// <summary>Gets the phrase: Gets FileProcessors parameters</summary>
    /// <value>Parameters</value>
    [ResourceEntry("FileProcessorsParameters", Description = "Gets the phrase: Parameters", LastModified = "2017/02/13", Value = "Parameters")]
    public string FileProcessorsParameters => this[nameof (FileProcessorsParameters)];

    /// <summary>
    /// Message: Contains localizable resources for the blogs module.
    /// </summary>
    /// <value>The description of this class</value>
    [ResourceEntry("SummaryResourcesDescription", Description = "The description of this class.", LastModified = "2009/12/03", Value = "Contains localizable resources for summary parser.")]
    public string SummaryResourcesDescription => this[nameof (SummaryResourcesDescription)];

    /// <summary>
    /// Title for the ContentViewDefinitionElement.LabelsConfig config element property
    /// </summary>
    [ResourceEntry("LabelsConfigCaption", Description = "Title for the ContentViewDefinitionElement.LabelsConfig config element property", LastModified = "2011/05/23", Value = "Labels")]
    public string LabelsConfigCaption => this[nameof (LabelsConfigCaption)];

    /// <summary>
    /// Description for the ContentViewDefinitionElement.LabelsConfig config element property
    /// </summary>
    [ResourceEntry("LabelsConfigDescirption", Description = "Description for the ContentViewDefinitionElement.LabelsConfig config element property", LastModified = "2012/01/05", Value = "Defines a collection of resource strings (labels) which will be available on the client via ClientLabelManager")]
    public string LabelsConfigDescirption => this[nameof (LabelsConfigDescirption)];

    /// <summary>Resource strings for ContentViewSection class.</summary>
    [ResourceEntry("ContentViewSectionElementCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Title")]
    public string ContentViewSectionElementCaption => this[nameof (ContentViewSectionElementCaption)];

    /// <summary>Resource strings for ContentViewSection class.</summary>
    [ResourceEntry("ContentViewSectionElementDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for description of ContentViewSection class.")]
    public string ContentViewSectionElementDescription => this[nameof (ContentViewSectionElementDescription)];

    /// <summary>Resource strings for ContentViewControl class.</summary>
    [ResourceEntry("ContentViewControlTitle", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Title")]
    public string ContentViewControlTitle => this[nameof (ContentViewControlTitle)];

    /// <summary>Resource strings for ContentViewControl class.</summary>
    [ResourceEntry("ContentViewControlDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for description of ContentViewControl class.")]
    public string ContentViewControlDescription => this[nameof (ContentViewControlDescription)];

    /// <summary>The content view controls configuration settings.</summary>
    [ResourceEntry("ContentViewConfig", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Defines content view controls settings.")]
    public string ContentViewConfig => this[nameof (ContentViewConfig)];

    /// <summary>The content view controls configuration settings.</summary>
    [ResourceEntry("ContentViewConfigDescription", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Defines content view controls settings.")]
    public string ContentViewConfigDescription => this[nameof (ContentViewConfigDescription)];

    /// <summary>Gets the content view controls.</summary>
    [ResourceEntry("ContentViewControls", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Controls")]
    public string ContentViewControls => this[nameof (ContentViewControls)];

    /// <summary>Gets the content view controls.</summary>
    [ResourceEntry("ContentViewControlsDescription", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Collection of content view control settings")]
    public string ContentViewControlsDescription => this[nameof (ContentViewControlsDescription)];

    /// <summary>Gets the content pipe types.</summary>
    [ResourceEntry("ContentPipeTypes", Description = "Describes configuration element.", LastModified = "2011/06/28", Value = "Types")]
    public string ContentPipeTypes => this[nameof (ContentPipeTypes)];

    /// <summary>Gets the content pipe types.</summary>
    [ResourceEntry("ContentPipeTypesDescription", Description = "Describes configuration element.", LastModified = "2011/06/28", Value = "Collection of types used in content pipes")]
    public string ContentPipeTypesDescription => this[nameof (ContentPipeTypesDescription)];

    /// <summary>Message: Additional view settings</summary>
    [ResourceEntry("ViewSettingsTitle", Description = "", LastModified = "2009/12/18", Value = "Additional view settings")]
    public string ViewSettingsTitle => this[nameof (ViewSettingsTitle)];

    /// <summary>Message: ContentViewControl definition name</summary>
    [ResourceEntry("ContentViewControlDefinitionName", Description = "", LastModified = "2009/12/18", Value = "ContentView control definition name")]
    public string ContentViewControlDefinitionName => this[nameof (ContentViewControlDefinitionName)];

    /// <summary>Message: ContentView control definition name</summary>
    [ResourceEntry("ContentViewControlDefinitionNameDescription", Description = "", LastModified = "2009/12/18", Value = "ContentView control definition name")]
    public string ContentViewControlDefinitionNameDescription => this[nameof (ContentViewControlDefinitionNameDescription)];

    /// <summary>Message: ContentViewControl definition name</summary>
    [ResourceEntry("ContentViewControlViews", Description = "", LastModified = "2009/12/18", Value = "Views")]
    public string ContentViewControlViews => this[nameof (ContentViewControlViews)];

    /// <summary>Message: ContentViewControl definition name</summary>
    [ResourceEntry("ContentViewControlViewsDescription", Description = "", LastModified = "2009/12/18", Value = "Defines the views of the ContentView control")]
    public string ContentViewControlViewsDescription => this[nameof (ContentViewControlViewsDescription)];

    /// <summary>Message: The type of the Content items</summary>
    [ResourceEntry("ContentViewControlContentType", Description = "", LastModified = "2009/12/18", Value = "The type of the Content items")]
    public string ContentViewControlContentType => this[nameof (ContentViewControlContentType)];

    /// <summary>Message: The type of the Content items</summary>
    [ResourceEntry("ContentViewControlContentTypeDescription", Description = "", LastModified = "2009/12/18", Value = "The type of the Content items")]
    public string ContentViewControlContentTypeDescription => this[nameof (ContentViewControlContentTypeDescription)];

    /// <summary>Message: The type of the manager</summary>
    [ResourceEntry("ContentViewControlManagerType", Description = "", LastModified = "2010/03/10", Value = "The type of the manager")]
    public string ContentViewControlManagerType => this[nameof (ContentViewControlManagerType)];

    /// <summary>Message: The type of the manager</summary>
    [ResourceEntry("ContentViewControlManagerTypeDescription", Description = "", LastModified = "2010/03/10", Value = "The type of the manager")]
    public string ContentViewControlManagerTypeDescription => this[nameof (ContentViewControlManagerTypeDescription)];

    /// <summary>Message: Define any custom settings for the view.</summary>
    [ResourceEntry("ViewSettingsDescription", Description = "", LastModified = "2009/12/18", Value = "Define any custom settings for the view")]
    public string ViewSettingsDescription => this[nameof (ViewSettingsDescription)];

    /// <summary>phrase: Content data provider</summary>
    [ResourceEntry("ContentProviderTitle", Description = "phrase: Content data provider", LastModified = "2009/12/22", Value = "Content data provider")]
    public string ContentProviderTitle => this[nameof (ContentProviderTitle)];

    /// <summary>phrase: Use workflow</summary>
    [ResourceEntry("UseWorkflowTitle", Description = "phrase: Use workflow", LastModified = "2010/11/14", Value = "Use workflow")]
    public string UseWorkflowTitle => this[nameof (UseWorkflowTitle)];

    /// <summary>
    /// phrase: Determines whether control ought to use workflow.S
    /// </summary>
    [ResourceEntry("UseWorkflowDescription", Description = "phrase: Determines whether control ought to use workflow.", LastModified = "2011/03/11", Value = "Determines whether control ought to use workflow")]
    public string UseWorkflowDescription => this[nameof (UseWorkflowDescription)];

    [ResourceEntry("ContentProviderDescription", Description = "", LastModified = "2012/01/05", Value = "Sets which content data provider should be used.")]
    public string ContentProviderDescription => this[nameof (ContentProviderDescription)];

    /// <summary>phrase: Allow paging</summary>
    [ResourceEntry("ContentViewMasterAllowPagingCaption", Description = "phrase: Allow paging", LastModified = "2010/01/25", Value = "Allow paging")]
    public string ContentViewMasterAllowPagingCaption => this[nameof (ContentViewMasterAllowPagingCaption)];

    /// <summary>
    /// phrase: Sets if paging is allowed for this master view.
    /// </summary>
    [ResourceEntry("ContentViewMasterAllowPagingDescription", Description = "phrase: Sets if paging is allowed for this master view.", LastModified = "2012/01/05", Value = "Sets if paging is allowed for this master view.")]
    public string ContentViewMasterAllowPagingDescription => this[nameof (ContentViewMasterAllowPagingDescription)];

    /// <summary>Allow Url Queries</summary>
    /// <value>The allow URL queries caption.</value>
    [ResourceEntry("AllowUrlQueriesCaption", Description = "Specifies the caption of the configuration element.", LastModified = "2010/01/25", Value = "Allow Url Queries")]
    public string AllowUrlQueriesCaption => this[nameof (AllowUrlQueriesCaption)];

    /// <summary>
    /// Specifies whether URL queries are allowed for the master view.
    /// </summary>
    /// <value>The allow URL queries description.</value>
    [ResourceEntry("AllowUrlQueriesDescription", Description = "Describes the configuration element.", LastModified = "2010/01/25", Value = "Specifies whether URL queries are allowed for the master view.")]
    public string AllowUrlQueriesDescription => this[nameof (AllowUrlQueriesDescription)];

    /// <summary>phrase: Disable sorting</summary>
    [ResourceEntry("ContentViewMasterDisableSortingCaption", Description = "phrase: Disable sorting", LastModified = "2010/01/25", Value = "Disable sorting")]
    public string ContentViewMasterDisableSortingCaption => this[nameof (ContentViewMasterDisableSortingCaption)];

    /// <summary>phrase: Is sorting disabled for this master view.</summary>
    [ResourceEntry("ContentViewMasterDisableSortingDescription", Description = "phrse: Is sorting disabled for this master view.", LastModified = "2010/01/25", Value = "Is sorting disabled for this master view.")]
    public string ContentViewMasterDisableSortingDescription => this[nameof (ContentViewMasterDisableSortingDescription)];

    /// <summary>phrase: Filter expression</summary>
    [ResourceEntry("ContentViewMasterFilterExpressionCaption", Description = "phrase: Filter expression", LastModified = "2010/01/25", Value = "Filter expression")]
    public string ContentViewMasterFilterExpressionCaption => this[nameof (ContentViewMasterFilterExpressionCaption)];

    /// <summary>phrase: The filter expression for this master view.</summary>
    [ResourceEntry("ContentViewMasterFilterExpressionDescription", Description = "phrase: The filter expression for this master view.", LastModified = "2010/01/25", Value = "The filter expression for this master view")]
    public string ContentViewMasterFilterExpressionDescription => this[nameof (ContentViewMasterFilterExpressionDescription)];

    /// <summary>phrase: Items per page</summary>
    [ResourceEntry("ContentViewMasterItemsPerPageCaption", Description = "phrase: Items per page", LastModified = "2010/01/25", Value = "Items per page")]
    public string ContentViewMasterItemsPerPageCaption => this[nameof (ContentViewMasterItemsPerPageCaption)];

    /// <summary>phrase: Allow users to set number of items per page</summary>
    [ResourceEntry("CanUsersSetItemsPerPageCaption", Description = "phrase: Allow users to set number of items per page", LastModified = "2011/06/20", Value = "Allow users to set number of items per page")]
    public string CanUsersSetItemsPerPageCaption => this[nameof (CanUsersSetItemsPerPageCaption)];

    /// <summary>
    /// phrase: How many items per page should be displayed in the master view (when using paging)
    /// </summary>
    [ResourceEntry("ContentViewMasterItemsPerPageDescription", Description = "phrase: How many items per page should be displayed in the master view (when using paging)", LastModified = "2010/01/25", Value = "How many items per page should be displayed in the master view (when using paging)")]
    public string ContentViewMasterItemsPerPageDescription => this[nameof (ContentViewMasterItemsPerPageDescription)];

    /// <summary>
    /// phrase: Can users customize the number of items to display per page?
    /// </summary>
    [ResourceEntry("CanUsersSetItemsPerPageDescription", Description = "phrase: Can users customize the number of items to display per page?", LastModified = "2011/06/20", Value = "Can users customize the number of items to display per page?")]
    public string CanUsersSetItemsPerPageDescription => this[nameof (CanUsersSetItemsPerPageDescription)];

    /// <summary>phrase: Sort expression</summary>
    [ResourceEntry("ContentViewMasterSortExpressionCaption", Description = "phrase: Sort expression", LastModified = "2010/01/25", Value = "Sort expression")]
    public string ContentViewMasterSortExpressionCaption => this[nameof (ContentViewMasterSortExpressionCaption)];

    /// <summary>phrase: Sort expression for the master view</summary>
    [ResourceEntry("ContentViewMasterSortExpressionDescription", Description = "phrase: Sort expression for the master view", LastModified = "2010/01/25", Value = "Sort expression for the master view")]
    public string ContentViewMasterSortExpressionDescription => this[nameof (ContentViewMasterSortExpressionDescription)];

    /// <summary>Master Page ID</summary>
    /// <value>The master page pageId caption.</value>
    [ResourceEntry("MasterPageIdCaption", Description = "The caption displayed for MasterPageId property in ContentViewDetailElement.", LastModified = "2010/01/25", Value = "Master Page ID")]
    public string MasterPageIdCaption => this[nameof (MasterPageIdCaption)];

    /// <summary>
    /// Specifies the ID of the page that should display the master view. If this ID is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId description.</value>
    [ResourceEntry("MasterPageIdDescription", Description = "The description of MasterPageId configuration.", LastModified = "2010/01/25", Value = "Specifies the ID of the page that should display the master view. If this ID is not set the current page is assumed.")]
    public string MasterPageIdDescription => this[nameof (MasterPageIdDescription)];

    /// <summary>Details Page ID</summary>
    /// <value>The details page ID caption.</value>
    [ResourceEntry("DetailsPageIdCaption", Description = "The caption displayed for DetailsPageId property in ContentViewMasterElement.", LastModified = "2010/01/25", Value = "Details Page ID")]
    public string DetailsPageIdCaption => this[nameof (DetailsPageIdCaption)];

    /// <summary>
    /// Specifies the ID of the page that should display the details view. If this ID is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId description.</value>
    [ResourceEntry("DetailsPageIdDescription", Description = "The description of DetailsPageId configuration.", LastModified = "2010/01/25", Value = "Specifies the ID of the page that should display the details view. If this ID is not set the current page is assumed.")]
    public string DetailsPageIdDescription => this[nameof (DetailsPageIdDescription)];

    /// <summary>Parent</summary>
    /// <value>The details page ID caption.</value>
    [ResourceEntry("ItemsParentIdCaption", Description = "The caption displayed for ItemsParentId property in ContentViewMasterElement.", LastModified = "2010/06/04", Value = "Parent ID")]
    public string ItemsParentIdCaption => this[nameof (ItemsParentIdCaption)];

    /// <summary>Parent</summary>
    /// <value>The details page ID caption.</value>
    [ResourceEntry("ItemsParentsIdsCaption", Description = "The caption displayed for ItemsParentsIdsCaption property in ContentViewMasterElement.", LastModified = "2011/04/05", Value = "Parents IDs")]
    public string ItemsParentsIdsCaption => this[nameof (ItemsParentsIdsCaption)];

    /// <summary>ID of the parent item</summary>
    /// <value>The details page ID caption.</value>
    [ResourceEntry("ItemsParentIdDescription", Description = "The description displayed for ItemsParentId property in ContentViewMasterElement.", LastModified = "2010/06/04", Value = "ID of the parent item")]
    public string ItemsParentIdDescription => this[nameof (ItemsParentIdDescription)];

    /// <summary>
    /// The caption displayed for RenderLinksInMasterView property in ContentViewMasterElement.
    /// </summary>
    [ResourceEntry("RenderLinksInMasterViewCaption", Description = "The caption displayed for RenderLinksInMasterView property in ContentViewMasterElement.", LastModified = "2011/03/18", Value = "Render links in Master View")]
    public string RenderLinksInMasterViewCaption => this[nameof (RenderLinksInMasterViewCaption)];

    /// <summary>
    /// The description displayed for RenderLinksInMasterView property in ContentViewMasterElement.
    /// </summary>
    [ResourceEntry("RenderLinksInMasterViewDescription", Description = "The description displayed for RenderLinksInMasterView property in ContentViewMasterElement.", LastModified = "2011/04/05", Value = "Render links in Master View")]
    public string RenderLinksInMasterViewDescription => this[nameof (RenderLinksInMasterViewDescription)];

    /// <summary>
    /// The description displayed for RenderLinksInMasterView property in ContentViewMasterElement.
    /// </summary>
    [ResourceEntry("ItemsParentsIdsDescription", Description = "The description displayed for ItemsParentsIdsDescription property in ContentViewMasterElement.", LastModified = "2010/03/18", Value = "Render links to detail views in Master View")]
    public string ItemsParentsIdsDescription => this[nameof (ItemsParentsIdsDescription)];

    /// <summary>Data Item ID</summary>
    /// <value>The data item pageId caption.</value>
    [ResourceEntry("DataItemIdCaption", Description = "The caption displayed for DataItemId property in ContentViewDetailElement.", LastModified = "2010/01/25", Value = "Data Item ID")]
    public string DataItemIdCaption => this[nameof (DataItemIdCaption)];

    /// <summary>
    /// Gets or sets the data ID of the content item that should be displayed.
    /// </summary>
    /// <value>The data item pageId description.</value>
    [ResourceEntry("DataItemIdDescription", Description = "The description of DataItemId configuration.", LastModified = "2010/01/25", Value = "Gets or sets the data ID of the content item that should be displayed.")]
    public string DataItemIdDescription => this[nameof (DataItemIdDescription)];

    /// <summary>The resource string for EnablePrevNextLinks property.</summary>
    [ResourceEntry("EnablePrevNextLinksCaption", Description = "The caption displayed for EnablePrevNextLinks property in ContentViewDetailElement.", LastModified = "2010/05/31", Value = "Enable Previous and Next links")]
    public string EnablePrevNextLinksCaption => this[nameof (EnablePrevNextLinksCaption)];

    /// <summary>
    /// Gets or sets a value indicating whether previous and next links are enabled in details mode of the content view.
    /// </summary>
    [ResourceEntry("EnablePrevNextLinksDescription", Description = "The description of EnablePrevNextLinks configuration.", LastModified = "2010/05/31", Value = "Gets or sets a value indicating whether previous and next links are enabled in details mode of the content view.")]
    public string EnablePrevNextLinksDescription => this[nameof (EnablePrevNextLinksDescription)];

    /// <summary>
    /// The resource string for PrevNextLinksDisplayMode property.
    /// </summary>
    [ResourceEntry("PrevNextLinksDisplayModeCaption", Description = "The caption displayed for PrevNextLinksDisplayMode property in ContentViewDetailElement.", LastModified = "2010/05/31", Value = "The display mode of Previous and Next links")]
    public string PrevNextLinksDisplayModeCaption => this[nameof (PrevNextLinksDisplayModeCaption)];

    /// <summary>
    /// Gets or sets the display mode of the previous and next links.
    /// </summary>
    [ResourceEntry("PrevNextLinksDisplayModeDescription", Description = "The description of PrevNextLinksDisplayMode configuration.", LastModified = "2010/05/31", Value = "Gets or sets the display mode of the previous and next links.")]
    public string PrevNextLinksDisplayModeDescription => this[nameof (PrevNextLinksDisplayModeDescription)];

    /// <summary>The resource string for ShowEmbeddingOption property.</summary>
    [ResourceEntry("ShowEmbeddingOptionCaption", Description = "The caption displayed for ShowEmbeddingOption property.", LastModified = "2010/06/08", Value = "Show option for embedding")]
    public string ShowEmbeddingOptionCaption => this[nameof (ShowEmbeddingOptionCaption)];

    /// <summary>
    /// Gets or sets a value indicating whether the option for embedding will be shown.
    /// </summary>
    [ResourceEntry("ShowEmbeddingOptionDescription", Description = "The description of ShowEmbeddingOptionDescription configuration.", LastModified = "2010/06/08", Value = "Gets or sets a value indicating whether the option for embedding will be shown.")]
    public string ShowEmbeddingOptionDescription => this[nameof (ShowEmbeddingOptionDescription)];

    /// <summary>The resource string for ShowRelatedVideos property.</summary>
    [ResourceEntry("ShowRelatedVideosCaption", Description = "The caption displayed for ShowRelatedVideos property.", LastModified = "2010/06/08", Value = "Show related videos")]
    public string ShowRelatedVideosCaption => this[nameof (ShowRelatedVideosCaption)];

    /// <summary>
    /// Gets or sets a value indicating whether the related videos will be shown.
    /// </summary>
    [ResourceEntry("ShowRelatedVideosDescription", Description = "The description of ShowRelatedVideos configuration.", LastModified = "2010/06/08", Value = "Gets or sets a value indicating whether the related videos will be shown.")]
    public string ShowRelatedVideosDescription => this[nameof (ShowRelatedVideosDescription)];

    /// <summary>The resource string for AllowFullSize property.</summary>
    [ResourceEntry("AllowFullSizeCaption", Description = "The caption displayed for AllowFullSize property.", LastModified = "2010/06/08", Value = "Allow full size")]
    public string AllowFullSizeCaption => this[nameof (AllowFullSizeCaption)];

    /// <summary>
    /// Gets or sets a value indicating whether the full size of the video player is allowed.
    /// </summary>
    [ResourceEntry("AllowFullSizeDescription", Description = "The description of AllowFullSize configuration.", LastModified = "2010/06/08", Value = "Gets or sets a value indicating whether the full size of the video player is allowed.")]
    public string AllowFullSizeDescription => this[nameof (AllowFullSizeDescription)];

    /// <summary>A collection of sections displayed in the view.</summary>
    /// <value>Sections</value>
    [ResourceEntry("SectionsConfigCaption", Description = "A collection of sections displayed in the view.", LastModified = "2010/01/25", Value = "Sections")]
    public string SectionsConfigCaption => this[nameof (SectionsConfigCaption)];

    /// <summary>A collection of sections displayed in the view.</summary>
    /// <value>A collection of sections displayed in the view.</value>
    [ResourceEntry("SectionsConfigDescription", Description = "A collection of sections displayed in the view.", LastModified = "2010/01/25", Value = "A collection of sections displayed in the view.")]
    public string SectionsConfigDescription => this[nameof (SectionsConfigDescription)];

    /// <summary>The Css Class of the section element.</summary>
    [ResourceEntry("CssClassCaption", Description = "The Css Class of the section element.", LastModified = "2010/01/24", Value = "CSS Class")]
    public string CssClassCaption => this[nameof (CssClassCaption)];

    /// <summary>phrase: The CSS Class of the section element.</summary>
    [ResourceEntry("CssClassDescription", Description = "The CSS Class of the section element.", LastModified = "2012/01/05", Value = "The CSS Class of the section element.")]
    public string CssClassDescription => this[nameof (CssClassDescription)];

    /// <summary>The hidden value of the section element.</summary>
    [ResourceEntry("HiddenCaption", Description = "The Hidden value of the section element.", LastModified = "2010/01/24", Value = "Hidden")]
    public string HiddenCaption => this[nameof (HiddenCaption)];

    /// <summary>
    /// A value indicating if the field element should be hidden.
    /// </summary>
    [ResourceEntry("HiddenDescription", Description = "A value indicating if the field element should be hidden.", LastModified = "2010/01/24", Value = "A value indicating if the field element should be hidden.")]
    public string HiddenDescription => this[nameof (HiddenDescription)];

    /// <summary>Determines the mode the section will be displayed in.</summary>
    [ResourceEntry("DisplayModeCaption", Description = "Determines the mode the section will be displayed in.", LastModified = "2010/01/24", Value = "Display Mode")]
    public string DisplayModeCaption => this[nameof (DisplayModeCaption)];

    /// <summary>Determines the mode the section will be displayed in.</summary>
    [ResourceEntry("DisplayModeDescription", Description = "Determines the mode the section will be displayed in.", LastModified = "2010/01/24", Value = "Determines the mode the section will be displayed in.")]
    public string DisplayModeDescription => this[nameof (DisplayModeDescription)];

    /// <summary>The name of the section</summary>
    [ResourceEntry("NameCaption", Description = "The name of the element.", LastModified = "2010/01/24", Value = "Name")]
    public string NameCaption => this[nameof (NameCaption)];

    /// <summary>The name of the section.</summary>
    [ResourceEntry("NameDescription", Description = "The name of the element.", LastModified = "2010/01/24", Value = "The name of the element.")]
    public string NameDescription => this[nameof (NameDescription)];

    /// <summary>
    /// Represents the order number of Content View section element.
    /// </summary>
    [ResourceEntry("OrdinalCaption", Description = "Represents the order number of Content View section element.", LastModified = "2010/01/25", Value = "Ordinal")]
    public string OrdinalCaption => this[nameof (OrdinalCaption)];

    /// <summary>
    /// Represents the order number of Content View section element.
    /// </summary>
    [ResourceEntry("OrdinalDescription", Description = "Represents the order number of Content View section element.", LastModified = "2010/01/25", Value = "Represents the order number of Content View section element.")]
    public string OrdinalDescription => this[nameof (OrdinalDescription)];

    /// <summary>
    /// Determines whether to display the title of the section.
    /// </summary>
    [ResourceEntry("ShowTitleCaption", Description = "Determines whether to display the title of the section.", LastModified = "2010/01/25", Value = "Show title")]
    public string ShowTitleCaption => this[nameof (ShowTitleCaption)];

    /// <summary>
    /// Determines whether to display the title of the section.
    /// </summary>
    [ResourceEntry("ShowTitleDescription", Description = "Determines whether to display the title of the section.", LastModified = "2010/01/25", Value = "Determines whether to display the title of the section.")]
    public string ShowTitleDescription => this[nameof (ShowTitleDescription)];

    /// <summary>
    /// Determines whether to display the title of the section.
    /// </summary>
    [ResourceEntry("TitleCaption", Description = "The title of the section.", LastModified = "2010/01/25", Value = "Title")]
    public string TitleCaption => this[nameof (TitleCaption)];

    /// <summary>
    /// Determines whether to display the title of the section.
    /// </summary>
    [ResourceEntry("TitleDescription", Description = "The title of the section.", LastModified = "2010/01/25", Value = "The title of the section.")]
    public string TitleDescription => this[nameof (TitleDescription)];

    /// <summary>The title of the WebServiceBaseUrlCaption property</summary>
    [ResourceEntry("WebServiceBaseUrlCaption", Description = "The title of the configuration property", LastModified = "2010/04/27", Value = "WebServiceBaseUrl")]
    public string WebServiceBaseUrlCaption => this[nameof (WebServiceBaseUrlCaption)];

    /// <summary>
    /// Description of the WebServiceBaseUrl configuration property
    /// </summary>
    [ResourceEntry("WebServiceBaseUrlDescription", Description = "Description of the WebServiceBaseUrl configuration property", LastModified = "2010/04/27", Value = "The base url of the web service to be used.")]
    public string WebServiceBaseUrlDescription => this[nameof (WebServiceBaseUrlDescription)];

    /// <summary>The title of the IsToRenderTranslationView property</summary>
    [ResourceEntry("IsToRenderTranslationViewCaption", Description = "The title of the configuration property", LastModified = "2010/10/20", Value = "Is to render translation view")]
    public string IsToRenderTranslationViewCaption => this[nameof (IsToRenderTranslationViewCaption)];

    /// <summary>
    /// Description of the IsToRenderTranslationView configuration property
    /// </summary>
    [ResourceEntry("IsToRenderTranslationViewDescription", Description = "Description of the IsToRenderTranslationView configuration property", LastModified = "2010/10/20", Value = "A value indicating whether to render the translation view.")]
    public string IsToRenderTranslationViewDescription => this[nameof (IsToRenderTranslationViewDescription)];

    /// <summary>The title of the AlternativeTitle property</summary>
    [ResourceEntry("AlternativeTitleCaption", Description = "The title of the AlternativeTitle configuration property", LastModified = "2010/11/23", Value = "Alternative title")]
    public string AlternativeTitleCaption => this[nameof (AlternativeTitleCaption)];

    /// <summary>
    /// Description of the AlternativeTitle configuration property
    /// </summary>
    [ResourceEntry("AlternativeTitleDescription", Description = "Description of the AlternativeTitle configuration property", LastModified = "2010/11/23", Value = "The alternative title of the view.")]
    public string AlternativeTitleDescription => this[nameof (AlternativeTitleDescription)];

    /// <summary>The title of the ItemTemplate property</summary>
    [ResourceEntry("ItemTemplateCaption", Description = "The title of the ItemTemplate configuration property", LastModified = "2010/12/10", Value = "Item template")]
    public string ItemTemplateCaption => this[nameof (ItemTemplateCaption)];

    /// <summary>
    /// Description of the ItemTemplate configuration property
    /// </summary>
    [ResourceEntry("ItemTemplateDescription", Description = "Description of the ItemTemplate configuration property", LastModified = "2010/12/10", Value = "The alternative title of the view.")]
    public string ItemTemplateDescription => this[nameof (ItemTemplateDescription)];

    /// <summary>The title of the DoNotUseContentItemContext property</summary>
    [ResourceEntry("DoNotUseContentItemContextCaption", Description = "The title of the DoNotUseContentItemContext configuration property", LastModified = "2011/05/09", Value = "Do not use ContentItemContext")]
    public string DoNotUseContentItemContextCaption => this[nameof (DoNotUseContentItemContextCaption)];

    /// <summary>
    /// Description of the DoNotUseContentItemContext configuration property
    /// </summary>
    [ResourceEntry("DoNotUseContentItemContextDescription", Description = "Description of the DoNotUseContentItemContext configuration property", LastModified = "2011/05/09", Value = "Determines whether the generated JSON by FieldControlsBinder will be put inside an 'Item' property of a context object (false), or if it will be passed to the server as is (true).")]
    public string DoNotUseContentItemContextDescription => this[nameof (DoNotUseContentItemContextDescription)];

    /// <summary>The title of the PromptDialogs property</summary>
    [ResourceEntry("DetailViewPromptDialogsCaption", Description = "The title of the PromptDialogs configuration property", LastModified = "2010/12/10", Value = "Prompt Dialogs")]
    public string DetailViewPromptDialogsCaption => this[nameof (DetailViewPromptDialogsCaption)];

    /// <summary>
    /// Description of the PromptDialogs configuration property
    /// </summary>
    [ResourceEntry("DetailViewPromptDialogsDescription", Description = "Description of the PromptDialogs configuration property", LastModified = "2010/12/10", Value = "A collection of prompt dialogs of the view")]
    public string DetailViewPromptDialogsDescription => this[nameof (DetailViewPromptDialogsDescription)];

    /// <summary>Delete Single Item Confirmation Message</summary>
    [ResourceEntry("DeleteSingleConfirmationMessage", Description = "The message to be shown when propting the user if they are sure they want to delete single item", LastModified = "2011/02/18", Value = "Delete Single Item Confirmation Message")]
    public string DeleteSingleConfirmationMessage => this[nameof (DeleteSingleConfirmationMessage)];

    /// <summary>
    /// Description of the Delete Single Item Confirmation Message
    /// </summary>
    [ResourceEntry("DeleteSingleConfirmationMessageDescription", Description = "Description of the Delete Single Item Confirmation Message", LastModified = "2012/01/05", Value = "The message to be shown when prompting the user if they are sure they want to delete a single item.")]
    public string DeleteSingleConfirmationMessageDescription => this[nameof (DeleteSingleConfirmationMessageDescription)];

    /// <summary>Item language fallback</summary>
    [ResourceEntry("ItemLanguageFallbackTitle", Description = "Configuration property title.", LastModified = "2011/04/07", Value = "Item language fallback")]
    public string ItemLanguageFallbackTitle => this[nameof (ItemLanguageFallbackTitle)];

    /// <summary>
    /// When in multilingual mode gets or sets whether items with no translation for the current language will be shown.
    /// </summary>
    [ResourceEntry("ItemLanguageFallbackDescription", Description = "Description of the ItemLanguageFallback configuration property", LastModified = "2011/04/07", Value = "When in multilingual mode gets or sets whether items with no translation for the current language will be shown.")]
    public string ItemLanguageFallbackDescription => this[nameof (ItemLanguageFallbackDescription)];

    /// <summary>Delete Multiple Items Confirmation Message</summary>
    [ResourceEntry("DeleteMultipleConfirmationMessage", Description = "The message to be shown when propting the user if they are sure they want to delete multiple items", LastModified = "2011/02/18", Value = "Delete Multiple Items Confirmation Message")]
    public string DeleteMultipleConfirmationMessage => this[nameof (DeleteMultipleConfirmationMessage)];

    /// <summary>
    /// Description of the Delete Multiple Items Confirmation Message
    /// </summary>
    [ResourceEntry("DeleteMultipleConfirmationMessageDescription", Description = "Description of the Delete Multiple Items Confirmation Message", LastModified = "2012/01/05", Value = "The message to be shown when prompting the user if they are sure they want to delete multiple items.")]
    public string DeleteMultipleConfirmationMessageDescription => this[nameof (DeleteMultipleConfirmationMessageDescription)];

    /// <summary>
    /// Caption for the MasterGridViewElement.DoNotBindOnClientWhenPageIsLoaded property
    /// </summary>
    [ResourceEntry("DoNotBindOnClientWhenPageIsLoadedCaption", Description = "Caption for the MasterGridViewElement.DoNotBindOnClientWhenPageIsLoaded property", LastModified = "2011/05/19", Value = "Do not bind on client when page is loaded")]
    public string DoNotBindOnClientWhenPageIsLoadedCaption => this[nameof (DoNotBindOnClientWhenPageIsLoadedCaption)];

    /// <summary>
    /// Description for the MasterGridViewElement.DoNotBindOnClientWhenPageIsLoaded property
    /// </summary>
    [ResourceEntry("DoNotBindOnClientWhenPageIsLoadedDescription", Description = "Description for the MasterGridViewElement.DoNotBindOnClientWhenPageIsLoaded property", LastModified = "2011/05/19", Value = "If set to true, the MasterGridView will not bind the current ItemsListBase on the client when the page is loaded. Default is false, i.e. it will bind. This is different from the current ItemsListBase.ClientBinder.BindOnLoad, which is always false and not affected by this property")]
    public string DoNotBindOnClientWhenPageIsLoadedDescription => this[nameof (DoNotBindOnClientWhenPageIsLoadedDescription)];

    /// <summary>
    /// Caption for the MasterGridViewElement.LandingPageId property
    /// </summary>
    [ResourceEntry("LandingPageIdCaption", Description = "Caption for the MasterGridViewElement.LandingPageId property", LastModified = "2012/10/09", Value = "Landing Page Id")]
    public string LandingPageIdCaption => this[nameof (LandingPageIdCaption)];

    /// <summary>
    /// Description for the MasterGridViewElement.LandingPageId property
    /// </summary>
    [ResourceEntry("LandingPageIdDescription", Description = "Description for the MasterGridViewElement.LandingPageId property", LastModified = "2012/10/09", Value = "Gets or sets the ID of the page displaying the master view.")]
    public string LandingPageIdDescription => this[nameof (LandingPageIdDescription)];

    /// <summary>The title of the FormName property</summary>
    [ResourceEntry("FormNameCaption", Description = "The title of the configuration property", LastModified = "2010/07/24", Value = "Form name")]
    public string FormNameCaption => this[nameof (FormNameCaption)];

    /// <summary>Description of the FormName configuration property</summary>
    [ResourceEntry("FormNameDescription", Description = "Description of the FormName configuration property", LastModified = "2010/07/24", Value = "The name of the form to show.")]
    public string FormNameDescription => this[nameof (FormNameDescription)];

    /// <summary>Title of the template evaluation mode property</summary>
    [ResourceEntry("TemplateEvaluationModeCaption", Description = "Title of the template evaluation mode property", LastModified = "2010/04/30", Value = "Template evaluation mode")]
    public string TemplateEvaluationModeCaption => this[nameof (TemplateEvaluationModeCaption)];

    /// <summary>Description of the template evaluation mode property</summary>
    [ResourceEntry("TemplateEvaluationModeDescription", Description = "Description of the template evaluation mode property", LastModified = "2010/04/30", Value = "Determines the way in which the conditional template will evaluate templates.")]
    public string TemplateEvaluationModeDescription => this[nameof (TemplateEvaluationModeDescription)];

    /// <summary>Title of EnableDragAndDrop property</summary>
    [ResourceEntry("EnableDragAndDropCaption", Description = "Title of the EnableDragAndDrop property", LastModified = "2010/07/05", Value = "Enable drag-and-drop functionality")]
    public string EnableDragAndDropCaption => this[nameof (EnableDragAndDropCaption)];

    /// <summary>Description of the EnableDragAndDrop property</summary>
    [ResourceEntry("EnableDragAndDropDescription", Description = "Description of the EnableDragAndDrop property", LastModified = "2010/07/05", Value = "When set to true enables drag-and-drop functionality")]
    public string EnableDragAndDropDescription => this[nameof (EnableDragAndDropDescription)];

    /// <summary>Title of EnableInitialExpanding property</summary>
    [ResourceEntry("EnableInitialExpandingCaption", Description = "Title of the EnableInitialExpanding property", LastModified = "2010/07/08", Value = "Enable initial expanding")]
    public string EnableInitialExpandingCaption => this[nameof (EnableInitialExpandingCaption)];

    /// <summary>Description of the EnableInitialExpanding property</summary>
    [ResourceEntry("EnableInitialExpandingDescription", Description = "Description of the EnableInitialExpanding property", LastModified = "2010/07/08", Value = "Defining whether to store the expansion of the tree per user.")]
    public string EnableInitialExpandingDescription => this[nameof (EnableInitialExpandingDescription)];

    /// <summary>Title of ExpandedNodesCookieName property</summary>
    [ResourceEntry("ExpandedNodesCookieNameCaption", Description = "Title of the ExpandedNodesCookieName property", LastModified = "2010/07/08", Value = "Expanded nodes cookie name")]
    public string ExpandedNodesCookieNameCaption => this[nameof (ExpandedNodesCookieNameCaption)];

    /// <summary>Description of the ExpandedNodesCookieName property</summary>
    [ResourceEntry("ExpandedNodesCookieNameDescription", Description = "Description of the ExpandedNodesCookieName property", LastModified = "2010/07/09", Value = "The name of the cookie that will contain the information of the expanded nodes.")]
    public string ExpandedNodesCookieNameDescription => this[nameof (ExpandedNodesCookieNameDescription)];

    /// <summary>phrase: Additional filter</summary>
    [ResourceEntry("AdditionalFilterCaption", Description = "phrase: Additional filter", LastModified = "2010/09/07", Value = "Additional filter")]
    public string AdditionalFilterCaption => this[nameof (AdditionalFilterCaption)];

    /// <summary>
    /// phrase: The additional filter expression for this master view.
    /// </summary>
    [ResourceEntry("AdditionalFilterDescription", Description = "phrase: The additional filter expression for this master view.", LastModified = "2010/09/07", Value = "The additional filter expression for this master view")]
    public string AdditionalFilterDescription => this[nameof (AdditionalFilterDescription)];

    /// <summary>phrase: Client script</summary>
    [ResourceEntry("ClientScriptTitle", Description = "phrase: Client script", LastModified = "2011/07/29", Value = "Client script")]
    public string ClientScriptTitle => this[nameof (ClientScriptTitle)];

    /// <summary>The information about client script</summary>
    [ResourceEntry("ClientScriptDescription", Description = "The information about client script", LastModified = "2011/07/29", Value = "Client script description")]
    public string ClientScriptDescription => this[nameof (ClientScriptDescription)];

    /// <summary>phrase: Script location</summary>
    [ResourceEntry("ScriptLocationTitle", Description = "phrase: Script location", LastModified = "2011/07/29", Value = "Script location")]
    public string ScriptLocationTitle => this[nameof (ScriptLocationTitle)];

    /// <summary>Describes the location of the script.</summary>
    [ResourceEntry("ScriptLocationDescription", Description = "Describes the location of the script.", LastModified = "2011/07/29", Value = "Describes the location of the script")]
    public string ScriptLocationDescription => this[nameof (ScriptLocationDescription)];

    /// <summary>Name of the load method</summary>
    [ResourceEntry("LoadMethodNameTitle", Description = "Name of the load method", LastModified = "2011/07/29", Value = "Name of the load method")]
    public string LoadMethodNameTitle => this[nameof (LoadMethodNameTitle)];

    /// <summary>phrase: Description of the load method name property</summary>
    [ResourceEntry("LoadMethodNameDescription", Description = "phrase: Description of the load method name property", LastModified = "2011/07/29", Value = "Description of the load method name property")]
    public string LoadMethodNameDescription => this[nameof (LoadMethodNameDescription)];

    /// <summary>Resource strings for news class.</summary>
    [ResourceEntry("ContentViewFieldElementCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "ContentViewField class")]
    public string ContentViewFieldElementCaption => this[nameof (ContentViewFieldElementCaption)];

    /// <summary>Resource strings for ContentViewField class.</summary>
    [ResourceEntry("ContentViewFieldElementDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for ContentViewField class.")]
    public string ContentViewFieldElementDescription => this[nameof (ContentViewFieldElementDescription)];

    /// <summary>Message: collapsed value</summary>
    /// <value>The collapsed value  of this class.</value>
    [ResourceEntry("CollapsedCaption", Description = "The collapsed value of this class.", LastModified = "2009/12/11", Value = "Collapsed")]
    public string CollapsedCaption => this[nameof (CollapsedCaption)];

    /// <summary>
    /// Message: Contains localizable resources for the collapsed value.
    /// </summary>
    /// <value>The collapsed value of this class</value>
    [ResourceEntry("CollapsedDescription", Description = "The description of this class.", LastModified = "2009/12/11", Value = "Contains localizable resources for the collapsed value.")]
    public string CollapsedDescription => this[nameof (CollapsedDescription)];

    /// <summary>Message: Description</summary>
    /// <value>The description of this class.</value>
    [ResourceEntry("DescriptionCaption", Description = "The description value of this class.", LastModified = "2009/12/11", Value = "Description")]
    public string DescriptionCaption => this[nameof (DescriptionCaption)];

    /// <summary>Message: The field control type.</summary>
    /// <value>The field control type of content view field class.</value>
    [ResourceEntry("FieldControlTypeCaption", Description = "The field control type of content view field class.", LastModified = "2009/12/11", Value = "FieldControlType")]
    public string FieldControlTypeCaption => this[nameof (FieldControlTypeCaption)];

    /// <summary>
    /// Message: Contains localizable resources for the field control type.
    /// </summary>
    /// <value>The field control type of this class</value>
    [ResourceEntry("FieldControlTypeDescription", Description = "The description of this class.", LastModified = "2009/12/11", Value = "Contains localizable resources for the field control type.")]
    public string FieldControlTypeDescription => this[nameof (FieldControlTypeDescription)];

    /// <summary>
    /// Message: FieldDataAppearance enum value of conent view field class.
    /// </summary>
    /// <value>FieldDataAppearance enum value of conent view field class.</value>
    [ResourceEntry("FieldDataAppearanceCaption", Description = "FieldDataAppearance enum value of conent view field class.", LastModified = "2009/12/11", Value = "FieldDataAppearance")]
    public string FieldDataAppearanceCaption => this[nameof (FieldDataAppearanceCaption)];

    /// <summary>
    /// Message: Contains localizable resources for FieldDataAppearance enum value of the field control type.
    /// </summary>
    /// <value>The field control type of this class</value>
    [ResourceEntry("FieldDataAppearanceDescription", Description = "Contains localizable resources for FieldDataAppearance enum value of the field control type.", LastModified = "2009/12/11", Value = "Contains localizable resources for FieldDataAppearance enum value of the field control type.")]
    public string FieldDataAppearanceDescription => this[nameof (FieldDataAppearanceDescription)];

    /// <summary>The title of Content View PlugIn.</summary>
    [ResourceEntry("ContentViewPlugInTitle", Description = "The title of Content View PlugIn.", LastModified = "2010/01/25", Value = "Content View PlugIn Title")]
    public string ContentViewPlugInTitle => this[nameof (ContentViewPlugInTitle)];

    /// <summary>The description of Literal Widget control.</summary>
    [ResourceEntry("ContentViewPlugInDescription", Description = "The description of Content View PlugIn control.", LastModified = "2010/01/24", Value = "Content View PlugIn description.")]
    public string ContentViewPlugInDescription => this[nameof (ContentViewPlugInDescription)];

    /// <summary>phrase: name</summary>
    [ResourceEntry("ContentViewPlugInNameCaption", Description = "phrase: name", LastModified = "2010/01/25", Value = "Name")]
    public string ContentViewPlugInNameCaption => this[nameof (ContentViewPlugInNameCaption)];

    /// <summary>
    /// phrase:Represents a name of Content View PlugIn control.
    /// </summary>
    [ResourceEntry("ContentViewPlugInNameDescription", Description = "phrase:Represents a name of Content View PlugIn control.", LastModified = "2010/01/25", Value = "Represents a name of Content View PlugIn control.")]
    public string ContentViewPlugInNameDescription => this[nameof (ContentViewPlugInNameDescription)];

    /// <summary>phrase: ordinal</summary>
    [ResourceEntry("ContentViewPlugInOrdinalCaption", Description = "phrase: ordinal", LastModified = "2010/01/25", Value = "Name")]
    public string ContentViewPlugInOrdinalCaption => this[nameof (ContentViewPlugInOrdinalCaption)];

    /// <summary>
    /// phrase:Represents the order number of Content View PlugIn control.
    /// </summary>
    [ResourceEntry("ContentViewPlugInOrdinalDescription", Description = "phrase:Represents the order number of Content View PlugIn control.", LastModified = "2010/01/25", Value = "Represents the order number of Content View PlugIn control.")]
    public string ContentViewPlugInOrdinalDescription => this["ContentViewPlugInNameDescription"];

    /// <summary>phrase: placeholderid</summary>
    [ResourceEntry("ContentViewPlugInPlaceHolderIdCaption", Description = "phrase: placeholderid", LastModified = "2010/01/25", Value = "Name")]
    public string ContentViewPlugInPlaceHolderIdCaption => this[nameof (ContentViewPlugInPlaceHolderIdCaption)];

    /// <summary>
    /// phrase:Represents the placeholder pageId for Content View PlugIn control.
    /// </summary>
    [ResourceEntry("ContentViewPlugInPlaceHolderIdDescription", Description = "phrase:Represents the placeholder id for Content View PlugIn control.", LastModified = "2010/01/25", Value = "Represents the placeholder id for Content View PlugIn control.")]
    public string ContentViewPlugInPlaceHolderIdDescription => this[nameof (ContentViewPlugInPlaceHolderIdDescription)];

    /// <summary>phrase: virtual path</summary>
    [ResourceEntry("ContentViewPlugInVirtualPathCaption", Description = "phrase: virtual path", LastModified = "2010/01/25", Value = "Name")]
    public string ContentViewPlugInVirtualPathCaption => this[nameof (ContentViewPlugInVirtualPathCaption)];

    /// <summary>
    /// phrase:Represents the virtual path for Content View PlugIn control.
    /// </summary>
    [ResourceEntry("ContentViewPlugInVirtualPathDescription", Description = "phrase:Represents the virtual path for Content View PlugIn control.", LastModified = "2010/01/25", Value = "Represents the virtual path for Content View PlugIn control.")]
    public string ContentViewPlugInVirtualPathDescription => this[nameof (ContentViewPlugInVirtualPathDescription)];

    /// <summary>phrase: plugIn type</summary>
    [ResourceEntry("ContentViewPlugInPlugInTypeCaption", Description = "phrase: plugin type", LastModified = "2010/01/25", Value = "Name")]
    public string ContentViewPlugInPlugInTypeCaption => this[nameof (ContentViewPlugInPlugInTypeCaption)];

    /// <summary>
    /// phrase:Represents the plugin type for Content View PlugIn control.
    /// </summary>
    [ResourceEntry("ContentViewPlugInPlugInTypeDescription", Description = "phrase:Represents the plugin type for Content View PlugIn control.", LastModified = "2010/01/25", Value = "Represents the plugin type for Content View PlugIn control.")]
    public string ContentViewPlugInPlugInTypeDescription => this[nameof (ContentViewPlugInPlugInTypeDescription)];

    /// <summary>phrase: Decision screens</summary>
    [ResourceEntry("BackendGridDecisionScreensCaption", Description = "phrase: Decision screens", LastModified = "2010/01/23", Value = "Decision screens")]
    public string BackendGridDecisionScreensCaption => this[nameof (BackendGridDecisionScreensCaption)];

    /// <summary>
    /// phrase: Defines the collection of decisions screens to be used by the view.
    /// </summary>
    [ResourceEntry("BackendGridDecisionScreensDescription", Description = "phrase: Defines the collection of decisions screens to be used by the view.", LastModified = "2010/01/23", Value = "Defines the collection of decisions screens to be used by the view.")]
    public string BackendGridDecisionScreensDescription => this[nameof (BackendGridDecisionScreensDescription)];

    /// <summary>word: Dialogs</summary>
    [ResourceEntry("BackendGridDialogsCaption", Description = "word: Dialogs", LastModified = "2010/01/23", Value = "Dialogs")]
    public string BackendGridDialogsCaption => this[nameof (BackendGridDialogsCaption)];

    /// <summary>
    /// phrase: Defines the collection of dialogs to be used by the view.
    /// </summary>
    [ResourceEntry("BackendGridDialogsDescription", Description = "phrase: Defines the collection of dialogs to be used by the view.", LastModified = "2010/01/23", Value = "Defines the collection of dialogs to be used by the view.")]
    public string BackendGridDialogsDescription => this[nameof (BackendGridDialogsDescription)];

    /// <summary>word: Dialogs</summary>
    [ResourceEntry("FrontendGridDialogsCaption", Description = "word: Dialogs", LastModified = "2010/01/23", Value = "Dialogs")]
    public string FrontendGridDialogsCaption => this[nameof (FrontendGridDialogsCaption)];

    /// <summary>
    /// phrase: Defines the collection of dialogs to be used in the frontend.
    /// </summary>
    [ResourceEntry("FrontendGridDialogsDescription", Description = "phrase: Defines the collection of dialogs to be used in the frontend.", LastModified = "2010/01/23", Value = "Defines the collection of dialogs to be used in the frontend.")]
    public string FrontendGridDialogsDescription => this[nameof (FrontendGridDialogsDescription)];

    /// <summary>The title of the SupportMultilingual property</summary>
    [ResourceEntry("MultilingualModeCaption", Description = "The title of the MultilingualMode configuration property", LastModified = "2012/03/28", Value = "Multilingual Mode")]
    public string MultilingualModeCaption => this[nameof (MultilingualModeCaption)];

    /// <summary>
    /// Description of the SupportMultilingual configuration property
    /// </summary>
    [ResourceEntry("MultilingualModeDescription", Description = "Description of the MultilingualMode configuration property", LastModified = "2012/03/28", Value = "Specifies whether a detail view should display data in a multilingual environment.")]
    public string MultilingualModeDescription => this[nameof (MultilingualModeDescription)];

    /// <summary>
    /// The title of the AdditionalCreateCommandsCaption property
    /// </summary>
    [ResourceEntry("AdditionalCreateCommandsCaption", Description = "The title of the AdditionalCreateCommandsCaption configuration property", LastModified = "2012/04/02", Value = "Additional Create commands")]
    public string AdditionalCreateCommandsCaption => this[nameof (AdditionalCreateCommandsCaption)];

    /// <summary>
    /// Description of the AdditionalCreateCommandsDescription configuration property
    /// </summary>
    [ResourceEntry("AdditionalCreateCommandsDescription", Description = "Description of the AdditionalCreateCommandsDescription configuration property", LastModified = "2012/11/22", Value = "A comma-delimited list of custom Create commands used internally by given module.")]
    public string AdditionalCreateCommandsDescription => this[nameof (AdditionalCreateCommandsDescription)];

    /// <summary>word: Dialogs</summary>
    [ResourceEntry("BackendGridPromptDialogsCaption", Description = "word: Dialogs", LastModified = "2010/01/23", Value = "Prompt Dialogs")]
    public string BackendGridPromptDialogsCaption => this[nameof (BackendGridPromptDialogsCaption)];

    /// <summary>
    /// phrase: Defines the collection of dialogs to be used by the view.
    /// </summary>
    [ResourceEntry("BackendGridPromptDialogsDescription", Description = "phrase: Defines the collection of prompt dialogs to be used by the view.", LastModified = "2010/01/23", Value = "Defines the collection of prompt dialogs to be used by the view.")]
    public string BackendGridPromptDialogsDescription => this[nameof (BackendGridPromptDialogsDescription)];

    /// <summary>word: Columns</summary>
    [ResourceEntry("BackendGridColumnsCaption", Description = "word: Columns", LastModified = "2010/01/23", Value = "Columns")]
    public string BackendGridColumnsCaption => this[nameof (BackendGridColumnsCaption)];

    /// <summary>
    /// phrase: Defines the collection of columns to be used by the view.
    /// </summary>
    [ResourceEntry("BackendGridColumnsDescription", Description = "phrase: Defines the collection of columns to be used by the view.", LastModified = "2010/01/23", Value = "Defines the collection of columns to be used by the view.")]
    public string BackendGridColumnsDescription => this[nameof (BackendGridColumnsDescription)];

    /// <summary>
    /// The title of the configuration collenction of view modes used in the backend master view.
    /// </summary>
    [ResourceEntry("BackendMasterViewModesCaption", Description = "The title of the configuration collenction of view modes used in the backend master view.", LastModified = "2010/01/23", Value = "View Modes")]
    public string BackendMasterViewModesCaption => this[nameof (BackendMasterViewModesCaption)];

    /// <summary>
    /// The title of the configuration collenction of view modes used in the backend master view.
    /// </summary>
    [ResourceEntry("BackendMasterViewModesDescription", Description = "The description of the configuration collenction of view modes used in the backend master view.", LastModified = "2010/01/23", Value = "Defines the collection of view modes.")]
    public string BackendMasterViewModesDescription => this[nameof (BackendMasterViewModesDescription)];

    /// <summary>word: Links</summary>
    [ResourceEntry("BackendGridLinksCaption", Description = "word: Links", LastModified = "2010/01/23", Value = "Links")]
    public string BackendGridLinksCaption => this[nameof (BackendGridLinksCaption)];

    /// <summary>
    /// phrase: Defines the collection of links to be used by the view.
    /// </summary>
    [ResourceEntry("BackendGridLinksDescription", Description = "phrase: Defines the collection of links to be used by the view.", LastModified = "2010/01/23", Value = "Defines the collection of links to be used by the view.")]
    public string BackendGridLinksDescription => this[nameof (BackendGridLinksDescription)];

    /// <summary>phrase: Toolbar</summary>
    [ResourceEntry("ToolbarCaption", Description = "phrase: Toolbar", LastModified = "2010/01/23", Value = "Toolbar")]
    public string ToolbarCaption => this[nameof (ToolbarCaption)];

    /// <summary>phrase: Defines the toolbar above the grid.</summary>
    [ResourceEntry("ToolbarDescription", Description = "phrase: Defines the toolbar above the grid.", LastModified = "2010/01/23", Value = "Defines the toolbar above the grid.")]
    public string ToolbarDescription => this[nameof (ToolbarDescription)];

    /// <summary>phrase: Toolbar</summary>
    [ResourceEntry("SidebarCaption", Description = "phrase: Sidebar", LastModified = "2010/01/23", Value = "Sidebar")]
    public string SidebarCaption => this[nameof (SidebarCaption)];

    /// <summary>phrase: Defines the sidebar right to the grid.</summary>
    [ResourceEntry("SidebarDescription", Description = "phrase: Defines the sidebar right to the grid.", LastModified = "2010/01/23", Value = "Defines the sidebar right to the grid.")]
    public string SidebarDescription => this[nameof (SidebarDescription)];

    /// <summary>phrase: Context bar</summary>
    [ResourceEntry("ContextBarCaption", Description = "phrase: Context bar", LastModified = "2010/05/18", Value = "Context bar")]
    public string ContextBarCaption => this[nameof (ContextBarCaption)];

    /// <summary>
    /// phrase: Defines the context bar, which appears as a header in the backend.
    /// </summary>
    [ResourceEntry("ContextBarDescription", Description = "phrase: Defines the context bar, which appears as a header in the backend.", LastModified = "2010/05/18", Value = "Defines the context bar, which appears as a header in the backend.")]
    public string ContextBarDescription => this[nameof (ContextBarDescription)];

    /// <summary>Describes the commands near the title.</summary>
    [ResourceEntry("TitleWidgetCaption", Description = "Describes the commands near the title.", LastModified = "2010/01/23", Value = "Title Widget")]
    public string TitleWidgetCaption => this[nameof (TitleWidgetCaption)];

    [ResourceEntry("TitleWidgetDescription", Description = "", LastModified = "2010/01/23", Value = "Describes the commands near the title.")]
    public string TitleWidgetDescription => this[nameof (TitleWidgetDescription)];

    /// <summary>Phrase: Sort expression</summary>
    [ResourceEntry("ColumnElementSortExpressionCaption", Description = "Phrase: Sort expression", LastModified = "2010/11/23", Value = "Sort expression")]
    public string ColumnElementSortExpressionCaption => this[nameof (ColumnElementSortExpressionCaption)];

    /// <summary>
    /// Phrase: The sort expression for the particular column in the grid
    /// </summary>
    [ResourceEntry("ColumnElementSortExpressionDescription", Description = "Phrase: The sort expression for the particular column in the grid", LastModified = "2010/11/23", Value = "The sort expression for the particular column in the grid")]
    public string ColumnElementSortExpressionDescription => this[nameof (ColumnElementSortExpressionDescription)];

    /// <summary>The title of this class</summary>
    [ResourceEntry("ActionMenuColumnTitle", Description = "The title of this class", LastModified = "2010/01/24", Value = "Dialog")]
    public string ActionMenuColumnTitle => this[nameof (ActionMenuColumnTitle)];

    /// <summary>The description of action menu column class."</summary>
    [ResourceEntry("ActionMenuColumnDescription", Description = "The description of action menu column class.", LastModified = "2010/01/24", Value = "Action menu column description.")]
    public string ActionMenuColumnDescription => this[nameof (ActionMenuColumnDescription)];

    /// <summary>Resource strings for ContentViewDetail class.</summary>
    [ResourceEntry("ContentViewDetailCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "ContentViewDetail class")]
    public string ContentViewDetailCaption => this[nameof (ContentViewDetailCaption)];

    /// <summary>Resource strings for ContentViewDetail class.</summary>
    [ResourceEntry("ContentViewDetailDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for ContentViewDetail class.")]
    public string ContentViewDetailDescription => this[nameof (ContentViewDetailDescription)];

    /// <summary>Resource strings for ControlDesignerSettings class.</summary>
    [ResourceEntry("ControlDesignerSettingsCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "ControlDesignerSettings class")]
    public string ControlDesignerSettingsCaption => this[nameof (ControlDesignerSettingsCaption)];

    /// <summary>Resource strings for ControlDesignerSettings class.</summary>
    [ResourceEntry("ControlDesignerSettingsDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for ControlDesignerSettings class.")]
    public string ControlDesignerSettingsDescription => this[nameof (ControlDesignerSettingsDescription)];

    /// <summary>
    /// Title of the AdditionalControlData configuration property
    /// </summary>
    [ResourceEntry("AdditionalControlDataCaption", Description = "Title of the AdditionalControlData configuration property", LastModified = "2011/10/17", Value = "Additional Data")]
    public string AdditionalControlDataCaption => this[nameof (AdditionalControlDataCaption)];

    /// <summary>
    /// Description of the AdditionalControlData configuration property
    /// </summary>
    [ResourceEntry("AdditionalControlDataDescription", Description = "Description of the AdditionalControlData configuration property", LastModified = "2011/10/17", Value = "Additional control-specific data")]
    public string AdditionalControlDataDescription => this[nameof (AdditionalControlDataDescription)];

    /// <summary>Label: Module name</summary>
    [ResourceEntry("ModuleNameCaption", Description = "Module name", LastModified = "2012/09/18", Value = "Module name")]
    public string ModuleNameCaption => this[nameof (ModuleNameCaption)];

    /// <summary>
    /// Phrase: The name of the module the element depends on.
    /// </summary>
    [ResourceEntry("ModuleNameDescription", Description = "The name of the module the element depends on", LastModified = "2012/09/18", Value = "The name of the module the element depends on")]
    public string ModuleNameDescription => this[nameof (ModuleNameDescription)];

    /// <summary>The title of this class</summary>
    [ResourceEntry("BoundPropertyNameCaption", Description = "The title of this class", LastModified = "2010/01/24", Value = "Bound Property Name")]
    public string BoundPropertyNameCaption => this[nameof (BoundPropertyNameCaption)];

    /// <summary>The description of this class."</summary>
    [ResourceEntry("BoundPropertyNameDescription", Description = "The description of this class", LastModified = "2012/01/05", Value = "The name of the property the column will be bound to.")]
    public string BoundPropertyNameDescription => this[nameof (BoundPropertyNameDescription)];

    /// <summary>label: Client Template</summary>
    [ResourceEntry("ClientTemplateCaption", Description = "TClient Template", LastModified = "2010/01/24", Value = "Client Template")]
    public string ClientTemplateCaption => this[nameof (ClientTemplateCaption)];

    /// <summary>
    /// phrase: A formatted text/html for the column template.
    /// </summary>
    [ResourceEntry("ClientTemplateDescription", Description = "A formatted text/html for the column template.", LastModified = "2012/01/05", Value = "A formatted text/html for the column template.")]
    public string ClientTemplateDescription => this[nameof (ClientTemplateDescription)];

    /// <summary>label: Disable Sorting</summary>
    [ResourceEntry("DataColumnDisableSortingCaption", Description = "DisableSortingCaption", LastModified = "2010/01/24", Value = "Disable Sorting")]
    public string DataColumnDisableSortingCaption => this[nameof (DataColumnDisableSortingCaption)];

    /// <summary>phrase: A flag for disabling sorting at data column.</summary>
    [ResourceEntry("DataColumnDisableSortingDescription", Description = "A flag  for disabling sorting at data column.", LastModified = "2010/01/24", Value = "A flag  for disabling sorting at data column.")]
    public string DataColumnDisableSortingDescription => this[nameof (DataColumnDisableSortingDescription)];

    /// <summary>phrase: Client template is dynamic</summary>
    [ResourceEntry("IsClientTemplateDynamicCaption", Description = "phrase: Client template is dynamic", LastModified = "2010/10/18", Value = "Client template is dynamic")]
    public string IsClientTemplateDynamicCaption => this[nameof (IsClientTemplateDynamicCaption)];

    /// <summary>
    /// phrase: Defines whether the client template is dynamic.
    /// </summary>
    [ResourceEntry("IsClientTemplateDynamicDescription", Description = "phrase: Defines whether the client template is dynamic.", LastModified = "2010/10/18", Value = "Defines whether the client template is dynamic.")]
    public string IsClientTemplateDynamicDescription => this[nameof (IsClientTemplateDynamicDescription)];

    /// <summary>phrase: Template virtual path</summary>
    [ResourceEntry("VirtualPathCaption", Description = "phrase: Template virtual path", LastModified = "2010/10/18", Value = "Template virtual path")]
    public string VirtualPathCaption => this[nameof (VirtualPathCaption)];

    /// <summary>phrase: Defines the virtual path of the template.</summary>
    [ResourceEntry("VirtualPathDescription", Description = "phrase: Defines the virtual path of the template.", LastModified = "2010/10/18", Value = "Defines the virtual path of the template.")]
    public string VirtualPathDescription => this[nameof (VirtualPathDescription)];

    /// <summary>phrase: Template resource name</summary>
    [ResourceEntry("ResourceFileNameCaption", Description = "phrase: Template resource name", LastModified = "2010/10/18", Value = "Template resource name")]
    public string ResourceFileNameCaption => this[nameof (ResourceFileNameCaption)];

    /// <summary>phrase: Defines the resource name of the template.</summary>
    [ResourceEntry("ResourceFileNameDescription", Description = "phrase: Defines the resource name of the template.", LastModified = "2010/10/18", Value = "Defines the resource name of the template.")]
    public string ResourceFileNameDescription => this[nameof (ResourceFileNameDescription)];

    /// <summary>phrase: Assembly name</summary>
    [ResourceEntry("AssemblyNameCaption", Description = "phrase: Assembly name", LastModified = "2010/10/18", Value = "Assembly name")]
    public string AssemblyNameCaption => this[nameof (AssemblyNameCaption)];

    /// <summary>
    /// phrase: Defines the name of the assembly with the resource containing the template.
    /// </summary>
    [ResourceEntry("AssemblyNameDescription", Description = "phrase: Defines the name of the assembly with the resource containing the template.", LastModified = "2010/10/18", Value = "Defines the name of the assembly with the resource containing the template.")]
    public string AssemblyNameDescription => this[nameof (AssemblyNameDescription)];

    /// <summary>phrase: Assembly info</summary>
    [ResourceEntry("AssemblyInfoCaption", Description = "phrase: Assembly info", LastModified = "2010/10/18", Value = "Assembly info")]
    public string AssemblyInfoCaption => this[nameof (AssemblyInfoCaption)];

    /// <summary>
    /// phrase: Defines a type from the assembly containing the resource file.
    /// </summary>
    [ResourceEntry("AssemblyInfoDescription", Description = "phrase: Defines a type from the assembly containing the resource file.", LastModified = "2010/10/18", Value = "Defines a type from the assembly containing the resource file.")]
    public string AssemblyInfoDescription => this[nameof (AssemblyInfoDescription)];

    /// <summary>The title of this class</summary>
    [ResourceEntry("DialogTitle", Description = "The title of this class", LastModified = "2010/01/24", Value = "Dialog")]
    public string DialogTitle => this[nameof (DialogTitle)];

    /// <summary>The description of this class."</summary>
    [ResourceEntry("DialogDescription", Description = "The description of this class", LastModified = "2010/01/24", Value = "Dialog description.")]
    public string DialogDescription => this[nameof (DialogDescription)];

    /// <summary>phrase: name</summary>
    [ResourceEntry("DialogNameCaption", Description = "phrase: name", LastModified = "2010/01/24", Value = "Name")]
    public string DialogNameCaption => this[nameof (DialogNameCaption)];

    /// <summary>
    /// phrase:The name of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogNameDescription", Description = "phrase:The name of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The name of the dialog that user is presented with.")]
    public string DialogNameDescription => this[nameof (DialogNameDescription)];

    /// <summary>phrase: command name</summary>
    [ResourceEntry("DialogCommandNameCaption", Description = "phrase: command name", LastModified = "2010/01/24", Value = "Name")]
    public string DialogCommandNameCaption => this[nameof (DialogCommandNameCaption)];

    /// <summary>phrase:The command name.</summary>
    [ResourceEntry("DialogCommandNameDescription", Description = "phrase:The command name.", LastModified = "2010/01/24", Value = "The command name that user is presented with.")]
    public string DialogCommandNameDescription => this[nameof (DialogCommandNameDescription)];

    /// <summary>phrase: navigate url</summary>
    [ResourceEntry("DialogNavigateUrlCaption", Description = "phrase: navigate url", LastModified = "2010/01/24", Value = "Navigate url")]
    public string DialogNavigateUrlCaption => this[nameof (DialogNavigateUrlCaption)];

    /// <summary>phrase:The navigate url that user is presented with.</summary>
    [ResourceEntry("DialogNavigateUrlDescription", Description = "phrase:The navigate url that user is presented with.", LastModified = "2010/01/24", Value = "The navigate url that user is presented with.")]
    public string DialogNavigateUrlDescription => this[nameof (DialogNavigateUrlDescription)];

    /// <summary>phrase: height</summary>
    [ResourceEntry("DialogHeightCaption", Description = "phrase: height", LastModified = "2010/01/24", Value = "height")]
    public string DialogHeightCaption => this[nameof (DialogHeightCaption)];

    /// <summary>
    /// phrase:The height of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogHeightDescription", Description = "phrase:The height of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The height of the dialog that user is presented with.")]
    public string DialogHeightDescription => this[nameof (DialogHeightDescription)];

    /// <summary>phrase: Width</summary>
    [ResourceEntry("DialogWidthCaption", Description = "phrase: Width", LastModified = "2010/01/24", Value = "Width")]
    public string DialogWidthCaption => this[nameof (DialogWidthCaption)];

    /// <summary>
    /// phrase:The Width of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogWidthDescription", Description = "phrase:The Width of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The Width of the dialog that user is presented with.")]
    public string DialogWidthDescription => this[nameof (DialogWidthDescription)];

    /// <summary>phrase: InitialBehaviors</summary>
    [ResourceEntry("DialogInitialBehaviorsCaption", Description = "phrase: InitialBehaviors", LastModified = "2010/01/24", Value = "InitialBehaviors")]
    public string DialogInitialBehaviorsCaption => this[nameof (DialogInitialBehaviorsCaption)];

    /// <summary>
    /// phrase:The InitialBehaviors of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogInitialBehaviorsDescription", Description = "phrase:The InitialBehaviors of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The InitialBehaviors of the dialog that user is presented with.")]
    public string DialogInitialBehaviorsDescription => this[nameof (DialogInitialBehaviorsDescription)];

    /// <summary>phrase: Behaviors</summary>
    [ResourceEntry("DialogBehaviorsCaption", Description = "phrase: Behaviors", LastModified = "2010/01/24", Value = "Behaviors")]
    public string DialogBehaviorsCaption => this[nameof (DialogBehaviorsCaption)];

    /// <summary>
    /// phrase:The Behaviors of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogBehaviorsDescription", Description = "phrase:The Behaviors of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The Behaviors of the dialog that user is presented with.")]
    public string DialogBehaviorsDescription => this[nameof (DialogBehaviorsDescription)];

    /// <summary>phrase: AutoSizeBehaviors</summary>
    [ResourceEntry("DialogAutoSizeBehaviorsCaption", Description = "phrase: AutoSizeBehaviors", LastModified = "2012/03/20", Value = "AutoSizeBehaviors")]
    public string DialogAutoSizeBehaviorsCaption => this[nameof (DialogAutoSizeBehaviorsCaption)];

    /// <summary>
    /// phrase:The AutoSizeBehaviors of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogAutoSizeBehaviorsDescription", Description = "phrase:The AutoSizeBehaviors of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The AutoSizeBehaviors of the dialog that user is presented with.")]
    public string DialogAutoSizeBehaviorsDescription => this[nameof (DialogAutoSizeBehaviorsDescription)];

    /// <summary>phrase: Is Full Screen</summary>
    [ResourceEntry("DialogIsFullScreenCaption", Description = "phrase: Is Full Screen", LastModified = "2010/01/24", Value = "IsFullScreen")]
    public string DialogIsFullScreenCaption => this[nameof (DialogIsFullScreenCaption)];

    /// <summary>
    /// phrase:Is FullScreen dialog mode that user is presented with.
    /// </summary>
    [ResourceEntry("DialogIsFullScreenDescription", Description = "phrase:Is FullScreen dialog mode that user is presented with.", LastModified = "2010/01/24", Value = "Is FullScreen dialog mode that user is presented with.")]
    public string DialogIsFullScreenDescription => this[nameof (DialogIsFullScreenDescription)];

    /// <summary>phrase: VisibleStatusBar</summary>
    [ResourceEntry("DialogVisibleStatusBarCaption", Description = "phrase: VisibleStatusBar", LastModified = "2010/01/24", Value = "VisibleStatusBar")]
    public string DialogVisibleStatusBarCaption => this[nameof (DialogVisibleStatusBarCaption)];

    /// <summary>
    /// phrase:Visibility of StatusBar that user is presented with.
    /// </summary>
    [ResourceEntry("DialogVisibleStatusBarDescription", Description = "phrase:Visibility of StatusBar that user is presented with.", LastModified = "2010/01/24", Value = "Visibility of StatusBar that user is presented with.")]
    public string DialogVisibleStatusBarDescription => this[nameof (DialogVisibleStatusBarDescription)];

    /// <summary>phrase: VisibleTitleBar</summary>
    [ResourceEntry("DialogVisibleTitleBarCaption", Description = "phrase: VisibleTitleBar", LastModified = "2010/01/24", Value = "VisibleTitleBar")]
    public string DialogVisibleTitleBarCaption => this[nameof (DialogVisibleTitleBarCaption)];

    /// <summary>
    /// phrase:Visibility of TitleBar that user is presented with.
    /// </summary>
    [ResourceEntry("DialogVisibleTitleBarDescription", Description = "phrase:Visibility of TitleBar that user is presented with.", LastModified = "2010/01/24", Value = "Visibility of TitleBar that user is presented with.")]
    public string DialogVisibleTitleBarDescription => this[nameof (DialogVisibleTitleBarDescription)];

    /// <summary>phrase: Parameters</summary>
    [ResourceEntry("DialogParametersCaption", Description = "phrase: Parameters", LastModified = "2010/01/24", Value = "Parameters")]
    public string DialogParametersCaption => this[nameof (DialogParametersCaption)];

    /// <summary>
    /// phrase:The Parameters of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogParametersDescription", Description = "phrase:The Parameters of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The Parameters of the dialog that user is presented with.")]
    public string DialogParametersDescription => this[nameof (DialogParametersDescription)];

    /// <summary>phrase: Skin</summary>
    [ResourceEntry("DialogSkinCaption", Description = "phrase: Skin", LastModified = "2010/01/24", Value = "Skin")]
    public string DialogSkinCaption => this[nameof (DialogSkinCaption)];

    /// <summary>
    /// phrase:The Skin of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogSkinDescription", Description = "phrase:The Skin of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The Skin of the dialog that user is presented with.")]
    public string DialogSkinDescription => this[nameof (DialogSkinDescription)];

    /// <summary>phrase: IsModal</summary>
    [ResourceEntry("DialogIsModalCaption", Description = "phrase: IsModal", LastModified = "2010/01/24", Value = "IsModal")]
    public string DialogIsModalCaption => this[nameof (DialogIsModalCaption)];

    /// <summary>
    /// phrase:The IsModal of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogIsModalDescription", Description = "phrase:The IsModal display mode of the dialog that user is presented with.", LastModified = "2010/01/24", Value = "The IsModal display mode of the dialog that user is presented with.")]
    public string DialogIsModalDescription => this[nameof (DialogIsModalDescription)];

    /// <summary>phrase: Destroy on close</summary>
    [ResourceEntry("DialogDestroyOnCloseCaption", Description = "phrase: Destroy on close", LastModified = "2010/08/20", Value = "Destroy on close")]
    public string DialogDestroyOnCloseCaption => this[nameof (DialogDestroyOnCloseCaption)];

    /// <summary>
    /// phrase: Gets or sets a value indicating whether the dialog's window will be disposed and made inaccessible once it is closed.
    /// </summary>
    [ResourceEntry("DialogDestroyOnCloseDescription", Description = "phrase: Gets or sets a value indicating whether the dialog's window will be disposed and made inaccessible once it is closed.", LastModified = "2010/08/20", Value = "Gets or sets a value indicating whether the dialog's window will be disposed and made inaccessible once it is closed.")]
    public string DialogDestroyOnCloseDescription => this[nameof (DialogDestroyOnCloseDescription)];

    /// <summary>phrase: Reload on show</summary>
    [ResourceEntry("DialogReloadOnShowCaption", Description = "phrase: Reload on show", LastModified = "2010/08/20", Value = "Reload on show")]
    public string DialogReloadOnShowCaption => this[nameof (DialogReloadOnShowCaption)];

    /// <summary>
    /// phrase: Gets or sets a value indicating whether the page that is loaded in the dialog's window should be loaded every time from the server or will leave the browser default behaviour.
    /// </summary>
    [ResourceEntry("DialogReloadOnShowDescription", Description = "phrase: Gets or sets a value indicating whether the page that is loaded in the dialog's window should be loaded every time from the server or will leave the browser default behaviour.", LastModified = "2012/01/05", Value = "Gets or sets a value indicating whether the page that is loaded in the dialog's window should be loaded every time from the server or will leave the browser default behaviour.")]
    public string DialogReloadOnShowDescription => this[nameof (DialogReloadOnShowDescription)];

    /// <summary>phrase: CssClass</summary>
    [ResourceEntry("DialogCssClassCaption", Description = "phrase: CssClass", LastModified = "2010/01/24", Value = "CssClass")]
    public string DialogCssClassCaption => this[nameof (DialogCssClassCaption)];

    /// <summary>
    /// phrase: The CSS Class of the dialog that user is presented with.
    /// </summary>
    [ResourceEntry("DialogCssClassDescription", Description = "phrase: The CSS Class of the dialog that user is presented with.", LastModified = "2012/01/05", Value = "The CSS Class of the dialog that user is presented with.")]
    public string DialogCssClassDescription => this[nameof (DialogCssClassDescription)];

    /// <summary>phrase: IsBlackListed</summary>
    [ResourceEntry("IsBlackListedCaption", Description = "phrase: IsBlackListed", LastModified = "2012/09/24", Value = "IsBlackListed")]
    public string IsBlackListedCaption => this[nameof (IsBlackListedCaption)];

    /// <summary>
    /// phrase: Gets or sets a value indicating whether the dialog is added to the black list.
    /// </summary>
    [ResourceEntry("IsBlackListedDescription", Description = "phrase: Gets or sets a value indicating whether the dialog is added to the black list.", LastModified = "2012/09/24", Value = "Gets or sets a value indicating whether the dialog is added to the black list.")]
    public string IsBlackListedDescription => this[nameof (IsBlackListedDescription)];

    /// <summary>The title of this class</summary>
    [ResourceEntry("PromptDialogTitle", Description = "The title of this class", LastModified = "2010/10/01", Value = "Prompt Dialog")]
    public string PromptDialogTitle => this[nameof (PromptDialogTitle)];

    /// <summary>The description of this class."</summary>
    [ResourceEntry("PromptDialogDescription", Description = "The description of this class", LastModified = "2010/10/01", Value = "Prompt dialog description.")]
    public string PromptDialogDescription => this[nameof (PromptDialogDescription)];

    /// <summary>Caption for Dialog Name property.</summary>
    [ResourceEntry("PromptDialogNameCaption", Description = "Caption for Dialog Name property.", LastModified = "2010/10/01", Value = "Dialog Name")]
    public string PromptDialogNameCaption => this[nameof (PromptDialogNameCaption)];

    /// <summary>Description for Dialog Name property.</summary>
    [ResourceEntry("PromptDialogNameDescription", Description = "Description for Dialog Name property.", LastModified = "2010/10/01", Value = "The name of the dialog.")]
    public string PromptDialogNameDescription => this[nameof (PromptDialogNameDescription)];

    /// <summary>Caption for Command Name property.</summary>
    [ResourceEntry("PromptDialogCommandNameCaption", Description = "Caption for Command Name property.", LastModified = "2010/10/01", Value = "Command Name")]
    public string PromptDialogCommandNameCaption => this[nameof (PromptDialogCommandNameCaption)];

    /// <summary>Description for Command Name property.</summary>
    [ResourceEntry("PromptDialogCommandNameDescription", Description = "Description for Command Name property.", LastModified = "2010/10/01", Value = "The name of the command that opens the dialog.")]
    public string PromptDialogCommandNameDescription => this[nameof (PromptDialogCommandNameDescription)];

    /// <summary>Caption for Allow Close Button property.</summary>
    [ResourceEntry("PromptDialogAllowCloseButtonCaption", Description = "Caption for Allow Close Button property.", LastModified = "2010/10/01", Value = "Allow close button")]
    public string PromptDialogAllowCloseButtonCaption => this[nameof (PromptDialogAllowCloseButtonCaption)];

    /// <summary>Caption for Allow Close Button property.</summary>
    [ResourceEntry("PromptDialogAllowCloseButtonDescription", Description = "Description for Allow Close Button property.", LastModified = "2010/10/01", Value = "Whether to allow close button on the dialog.")]
    public string PromptDialogAllowCloseButtonDescription => this[nameof (PromptDialogAllowCloseButtonDescription)];

    /// <summary>Caption for Commands property.</summary>
    [ResourceEntry("PromptDialogCommandsCaption", Description = "Caption for Commands property.", LastModified = "2010/10/01", Value = "Commands")]
    public string PromptDialogCommandsCaption => this[nameof (PromptDialogCommandsCaption)];

    /// <summary>Description for Commands property.</summary>
    [ResourceEntry("PromptDialogCommandsDescription", Description = "Description for Commands property.", LastModified = "2010/10/01", Value = "The commands for the dialog.")]
    public string PromptDialogCommandsDescription => this[nameof (PromptDialogCommandsDescription)];

    /// <summary>Caption for DefaultInputText property.</summary>
    [ResourceEntry("PromptDialogDefaultInputTextCaption", Description = "Caption for DefaultInputText property.", LastModified = "2010/10/01", Value = "Default Input Text")]
    public string PromptDialogDefaultInputTextCaption => this[nameof (PromptDialogDefaultInputTextCaption)];

    /// <summary>Description for DefaultInputText property.</summary>
    [ResourceEntry("PromptDialogDefaultInputTextDescription", Description = "Description for DefaultInputText property.", LastModified = "2010/10/01", Value = "The default text in the input field.")]
    public string PromptDialogDefaultInputTextDescription => this[nameof (PromptDialogDefaultInputTextDescription)];

    /// <summary>Caption for Displayed property.</summary>
    [ResourceEntry("PromptDialogDisplayedCaption", Description = "Caption for Displayed property.", LastModified = "2010/10/01", Value = "Displayed")]
    public string PromptDialogDisplayedCaption => this[nameof (PromptDialogDisplayedCaption)];

    /// <summary>Description for Displayed property.</summary>
    [ResourceEntry("PromptDialogDisplayedDescription", Description = "Description for Displayed property.", LastModified = "2010/10/01", Value = "Sets whether the dialog is displayed.")]
    public string PromptDialogDisplayedDescription => this[nameof (PromptDialogDisplayedDescription)];

    /// <summary>Caption for Height property.</summary>
    [ResourceEntry("PromptDialogHeightCaption", Description = "Caption for Height property.", LastModified = "2010/10/01", Value = "Height")]
    public string PromptDialogHeightCaption => this[nameof (PromptDialogHeightCaption)];

    /// <summary>Description for Height property.</summary>
    [ResourceEntry("PromptDialogHeightDescription", Description = "Description for Height property.", LastModified = "2010/10/01", Value = "The height of the dialog.")]
    public string PromptDialogHeightDescription => this[nameof (PromptDialogHeightDescription)];

    /// <summary>Caption for Width property.</summary>
    [ResourceEntry("PromptDialogWidthCaption", Description = "Caption for Width property.", LastModified = "2010/10/01", Value = "Width")]
    public string PromptDialogWidthCaption => this[nameof (PromptDialogWidthCaption)];

    /// <summary>Description for Width property.</summary>
    [ResourceEntry("PromptDialogWidthDescription", Description = "Description for Width property.", LastModified = "2010/10/01", Value = "The width of the dialog.")]
    public string PromptDialogWidthDescription => this[nameof (PromptDialogWidthDescription)];

    /// <summary>Caption for InputRows property.</summary>
    [ResourceEntry("PromptDialogInputRowsCaption", Description = "Caption for InputRows property.", LastModified = "2010/10/01", Value = "Input Rows")]
    public string PromptDialogInputRowsCaption => this[nameof (PromptDialogInputRowsCaption)];

    /// <summary>Description for InputRows property.</summary>
    [ResourceEntry("PromptDialogInputRowsDescription", Description = "Description for InputRows property.", LastModified = "2010/10/01", Value = "The number of input rows for the input field.")]
    public string PromptDialogInputRowsDescription => this[nameof (PromptDialogInputRowsDescription)];

    /// <summary>Caption for ItemTag property.</summary>
    [ResourceEntry("PromptDialogItemTagCaption", Description = "Caption for ItemTag property.", LastModified = "2010/10/01", Value = "ItemTag")]
    public string PromptDialogItemTagCaption => this[nameof (PromptDialogItemTagCaption)];

    /// <summary>Description for ItemTag property.</summary>
    [ResourceEntry("PromptDialogItemTagDescription", Description = "Description for ItemTag property.", LastModified = "2010/10/01", Value = "The HTML tag for the command items.")]
    public string PromptDialogItemTagDescription => this[nameof (PromptDialogItemTagDescription)];

    /// <summary>Caption for Message property.</summary>
    [ResourceEntry("PromptDialogMessageCaption", Description = "Caption for Message property.", LastModified = "2010/10/01", Value = "Message")]
    public string PromptDialogMessageCaption => this[nameof (PromptDialogMessageCaption)];

    /// <summary>Description for Message property.</summary>
    [ResourceEntry("PromptDialogMessageDescription", Description = "Description for Message property.", LastModified = "2010/10/01", Value = "The message displayed to the user.")]
    public string PromptDialogMessageDescription => this[nameof (PromptDialogMessageDescription)];

    /// <summary>Caption for PromptMode property.</summary>
    [ResourceEntry("PromptDialogPromptModeCaption", Description = "Caption for PromptMode property.", LastModified = "2010/10/01", Value = "Prompt Mode")]
    public string PromptDialogPromptModeCaption => this[nameof (PromptDialogPromptModeCaption)];

    /// <summary>Description for PromptMode property.</summary>
    [ResourceEntry("PromptDialogPromptModeDescription", Description = "Description for PromptMode property.", LastModified = "2010/10/01", Value = "The prompt mode of the dialog.")]
    public string PromptDialogPromptModeDescription => this[nameof (PromptDialogPromptModeDescription)];

    /// <summary>Caption for OnClientCommand property.</summary>
    [ResourceEntry("PromptDialogOnClientCommandCaption", Description = "Caption for OnClientCommand property.", LastModified = "2010/10/01", Value = "On Client Command")]
    public string PromptDialogOnClientCommandCaption => this[nameof (PromptDialogOnClientCommandCaption)];

    /// <summary>Description for OnClientCommand property.</summary>
    [ResourceEntry("PromptDialogOnClientCommandDescription", Description = "Description for OnClientCommand property.", LastModified = "2010/10/01", Value = "The name of the client side function to be fired when client raises a command.")]
    public string PromptDialogOnClientCommandDescription => this[nameof (PromptDialogOnClientCommandDescription)];

    /// <summary>Caption for ShowOnLoad property.</summary>
    [ResourceEntry("PromptDialogShowOnLoadCaption", Description = "Caption for ShowOnLoad property.", LastModified = "2010/10/01", Value = "Show On Load")]
    public string PromptDialogShowOnLoadCaption => this[nameof (PromptDialogShowOnLoadCaption)];

    /// <summary>Description for ShowOnLoad property.</summary>
    [ResourceEntry("PromptDialogShowOnLoadDescription", Description = "Description for ShowOnLoad property.", LastModified = "2010/10/01", Value = "Whether to show the dialog when the page loads.")]
    public string PromptDialogShowOnLoadDescription => this[nameof (PromptDialogShowOnLoadDescription)];

    /// <summary>Caption for TextFieldExample property.</summary>
    [ResourceEntry("PromptDialogTextFieldExampleCaption", Description = "Caption for TextFieldExample property.", LastModified = "2010/10/01", Value = "Text Field Example")]
    public string PromptDialogTextFieldExampleCaption => this[nameof (PromptDialogTextFieldExampleCaption)];

    /// <summary>Description for TextFieldExample property.</summary>
    [ResourceEntry("PromptDialogTextFieldExampleDescription", Description = "Description for TextFieldExample property.", LastModified = "2010/10/01", Value = "The example text of the input field.")]
    public string PromptDialogTextFieldExampleDescription => this[nameof (PromptDialogTextFieldExampleDescription)];

    /// <summary>Caption for TextFieldTitle property.</summary>
    [ResourceEntry("PromptDialogTextFieldTitleCaption", Description = "Caption for TextFieldTitle property.", LastModified = "2010/10/01", Value = "Text Field Title")]
    public string PromptDialogTextFieldTitleCaption => this[nameof (PromptDialogTextFieldTitleCaption)];

    /// <summary>Description for TextFieldTitle property.</summary>
    [ResourceEntry("PromptDialogTextFieldTitleDescription", Description = "Description for TextFieldTitle property.", LastModified = "2012/01/05", Value = "The title of the input field.")]
    public string PromptDialogTextFieldTitleDescription => this[nameof (PromptDialogTextFieldTitleDescription)];

    /// <summary>Caption for Title property.</summary>
    [ResourceEntry("PromptDialogTitleCaption", Description = "Caption for Title property.", LastModified = "2010/10/01", Value = "Title")]
    public string PromptDialogTitleCaption => this[nameof (PromptDialogTitleCaption)];

    /// <summary>Description for Title property.</summary>
    [ResourceEntry("PromptDialogTitleDescription", Description = "Description for Title property.", LastModified = "2010/10/01", Value = "The title of the dialog.")]
    public string PromptDialogTitleDescription => this[nameof (PromptDialogTitleDescription)];

    /// <summary>Caption for ValidatorDefinition property.</summary>
    [ResourceEntry("PromptDialogValidatorDefinitionCaption", Description = "Caption for ValidatorDefinition property.", LastModified = "2010/10/01", Value = "Validator Definition")]
    public string PromptDialogValidatorDefinitionCaption => this[nameof (PromptDialogValidatorDefinitionCaption)];

    /// <summary>Description for ValidatorDefinition property.</summary>
    [ResourceEntry("PromptDialogValidatorDefinitionDescription", Description = "Description for ValidatorDefinition property.", LastModified = "2010/10/01", Value = "The validator for the input field.")]
    public string PromptDialogValidatorDefinitionDescription => this[nameof (PromptDialogValidatorDefinitionDescription)];

    /// <summary>Caption for WrapperCssClass property.</summary>
    [ResourceEntry("PromptDialogWrapperCssClassCaption", Description = "Caption for WrapperCssClass property.", LastModified = "2012/01/05", Value = "Wrapper CSS Class")]
    public string PromptDialogWrapperCssClassCaption => this[nameof (PromptDialogWrapperCssClassCaption)];

    /// <summary>Description for WrapperCssClass property.</summary>
    [ResourceEntry("PromptDialogWrapperCssClassDescription", Description = "Description for WrapperCssClass property.", LastModified = "2010/10/01", Value = "The CSS class of the wrapper.")]
    public string PromptDialogWrapperCssClassDescription => this[nameof (PromptDialogWrapperCssClassDescription)];

    /// <summary>Caption for WrapperTag property.</summary>
    [ResourceEntry("PromptDialogWrapperTagCaption", Description = "Caption for WrapperTag property.", LastModified = "2010/10/01", Value = "Wrapper Tag")]
    public string PromptDialogWrapperTagCaption => this[nameof (PromptDialogWrapperTagCaption)];

    /// <summary>Description for WrapperTag property.</summary>
    [ResourceEntry("PromptDialogWrapperTagDescription", Description = "Description for WrapperTag property.", LastModified = "2010/10/01", Value = "The HTML tag of the wrapper.")]
    public string PromptDialogWrapperTagDescription => this[nameof (PromptDialogWrapperTagDescription)];

    /// <summary>The title of this class</summary>
    [ResourceEntry("CommandToolboxItemTitle", Description = "The title of this class", LastModified = "2010/10/01", Value = "Command Toolbox Item")]
    public string CommandToolboxItemTitle => this[nameof (CommandToolboxItemTitle)];

    /// <summary>The description of this class."</summary>
    [ResourceEntry("CommandToolboxItemDescription", Description = "The description of this class", LastModified = "2010/10/01", Value = "Command Toolbox Item description.")]
    public string CommandToolboxItemDescription => this[nameof (CommandToolboxItemDescription)];

    /// <summary>Caption for CausesValidation property.</summary>
    [ResourceEntry("CommandToolboxItemCausesValidationCaption", Description = "Caption for CausesValidation property.", LastModified = "2010/10/01", Value = "Causes Validation")]
    public string CommandToolboxItemCausesValidationCaption => this[nameof (CommandToolboxItemCausesValidationCaption)];

    /// <summary>Description for CausesValidation property.</summary>
    [ResourceEntry("CommandToolboxItemCausesValidationDescription", Description = "Description for CausesValidation property.", LastModified = "2010/10/01", Value = "Whether the command causes client-side validation.")]
    public string CommandToolboxItemCausesValidationDescription => this[nameof (CommandToolboxItemCausesValidationDescription)];

    /// <summary>Caption for ItemCommandName property.</summary>
    [ResourceEntry("CommandToolboxItemCommandNameCaption", Description = "Caption for ItemCommandName property.", LastModified = "2010/10/01", Value = "Command Name")]
    public string CommandToolboxItemCommandNameCaption => this[nameof (CommandToolboxItemCommandNameCaption)];

    /// <summary>Description for ItemCommandName property.</summary>
    [ResourceEntry("CommandToolboxItemCommandNameDescription", Description = "Description for ItemCommandName property.", LastModified = "2010/10/01", Value = "The name of the command for this item.")]
    public string CommandToolboxItemCommandNameDescription => this[nameof (CommandToolboxItemCommandNameDescription)];

    /// <summary>Caption for ItemCommandType property.</summary>
    [ResourceEntry("CommandToolboxItemCommandTypeCaption", Description = "Caption for CausesValidation property.", LastModified = "2010/10/01", Value = "Command Type")]
    public string CommandToolboxItemCommandTypeCaption => this[nameof (CommandToolboxItemCommandTypeCaption)];

    /// <summary>Description for ItemCommandType property.</summary>
    [ResourceEntry("CommandToolboxItemCommandTypeDescription", Description = "Description for ItemCommandType property.", LastModified = "2010/10/01", Value = "The type of the command.")]
    public string CommandToolboxItemCommandTypeDescription => this[nameof (CommandToolboxItemCommandTypeDescription)];

    /// <summary>Caption for ItemText property.</summary>
    [ResourceEntry("CommandToolboxItemTextCaption", Description = "Caption for ItemText property.", LastModified = "2010/10/01", Value = "Text")]
    public string CommandToolboxItemTextCaption => this[nameof (CommandToolboxItemTextCaption)];

    /// <summary>Description for ItemText property.</summary>
    [ResourceEntry("CommandToolboxItemTextDescription", Description = "Description for ItemText property.", LastModified = "2010/10/01", Value = "The text displayed for this command.")]
    public string CommandToolboxItemTextDescription => this[nameof (CommandToolboxItemTextDescription)];

    /// <summary>The title of this class</summary>
    [ResourceEntry("ToolboxItemBaseTitle", Description = "The title of this class", LastModified = "2010/10/01", Value = "Toolbox Item Base")]
    public string ToolboxItemBaseTitle => this[nameof (ToolboxItemBaseTitle)];

    /// <summary>The description of this class.</summary>
    [ResourceEntry("ToolboxItemBaseDescription", Description = "The description of this class", LastModified = "2010/10/01", Value = "Toolbox Item Base description.")]
    public string ToolboxItemBaseDescription => this[nameof (ToolboxItemBaseDescription)];

    /// <summary>Caption for ContainerId property.</summary>
    [ResourceEntry("ToolboxItemBaseContainerIdCaption", Description = "Caption for ContainerId property.", LastModified = "2010/10/01", Value = "Container Id")]
    public string ToolboxItemBaseContainerIdCaption => this[nameof (ToolboxItemBaseContainerIdCaption)];

    /// <summary>Description for ContainerId property.</summary>
    [ResourceEntry("ToolboxItemBaseContainerIdDescription", Description = "Description for CausesValidation property.", LastModified = "2010/10/01", Value = "The Id of the server control in which the toolbox item ought to be instantiated.")]
    public string ToolboxItemBaseContainerIdDescription => this[nameof (ToolboxItemBaseContainerIdDescription)];

    /// <summary>Caption for CssClass property.</summary>
    [ResourceEntry("ToolboxItemBaseCssClassCaption", Description = "Caption for CssClass property.", LastModified = "2012/01/05", Value = "CSS Class")]
    public string ToolboxItemBaseCssClassCaption => this[nameof (ToolboxItemBaseCssClassCaption)];

    /// <summary>Description for CssClass property.</summary>
    [ResourceEntry("ToolboxItemBaseCssClassDescription", Description = "Description for CssClass property.", LastModified = "2010/10/01", Value = "The CSS class for the element.")]
    public string ToolboxItemBaseCssClassDescription => this[nameof (ToolboxItemBaseCssClassDescription)];

    /// <summary>Caption for ItemTemplatePath property.</summary>
    [ResourceEntry("ToolboxItemBaseItemTemplatePathCaption", Description = "Caption for ItemTemplatePath property.", LastModified = "2010/10/01", Value = "Item Template Path")]
    public string ToolboxItemBaseItemTemplatePathCaption => this[nameof (ToolboxItemBaseItemTemplatePathCaption)];

    /// <summary>Description for ItemTemplatePath property.</summary>
    [ResourceEntry("ToolboxItemBaseItemTemplatePathDescription", Description = "Description for ItemTemplatePath property.", LastModified = "2010/10/01", Value = "The path of the item template.")]
    public string ToolboxItemBaseItemTemplatePathDescription => this[nameof (ToolboxItemBaseItemTemplatePathDescription)];

    /// <summary>Caption for Visible property.</summary>
    [ResourceEntry("ToolboxItemBaseVisibleCaption", Description = "Caption for Visible property.", LastModified = "2010/10/01", Value = "Visible")]
    public string ToolboxItemBaseVisibleCaption => this[nameof (ToolboxItemBaseVisibleCaption)];

    /// <summary>Description for Visible property.</summary>
    [ResourceEntry("ToolboxItemBaseVisibleDescription", Description = "Description for Visible property.", LastModified = "2010/10/01", Value = "Whether the item is visible.")]
    public string ToolboxItemBaseVisibleDescription => this[nameof (ToolboxItemBaseVisibleDescription)];

    /// <summary>Caption for WrapperTagCssClass property.</summary>
    [ResourceEntry("ToolboxItemBaseWrapperTagCssClassCaption", Description = "Caption for WrapperTagCssClass property.", LastModified = "2012/01/05", Value = "Wrapper Tag CSS Class")]
    public string ToolboxItemBaseWrapperTagCssClassCaption => this[nameof (ToolboxItemBaseWrapperTagCssClassCaption)];

    /// <summary>Description for WrapperTagCssClass property.</summary>
    [ResourceEntry("ToolboxItemBaseWrapperTagCssClassDescription", Description = "Description for WrapperTagCssClass property.", LastModified = "2010/10/01", Value = "The CSS class of the wrapper HTML tag.")]
    public string ToolboxItemBaseWrapperTagCssClassDescription => this[nameof (ToolboxItemBaseWrapperTagCssClassDescription)];

    /// <summary>Caption for WrapperTagId property.</summary>
    [ResourceEntry("ToolboxItemBaseWrapperTagIdCaption", Description = "Caption for WrapperTagId property.", LastModified = "2010/10/01", Value = "Wrapper Tag Id")]
    public string ToolboxItemBaseWrapperTagIdCaption => this[nameof (ToolboxItemBaseWrapperTagIdCaption)];

    /// <summary>Description for WrapperTagId property.</summary>
    [ResourceEntry("ToolboxItemBaseWrapperTagIdDescription", Description = "Description for WrapperTagId property.", LastModified = "2010/10/01", Value = "The Id of the wrapper tag.")]
    public string ToolboxItemBaseWrapperTagIdDescription => this[nameof (ToolboxItemBaseWrapperTagIdDescription)];

    /// <summary>Caption for WrapperTagId property.</summary>
    [ResourceEntry("ToolboxItemBaseWrapperTagNameCaption", Description = "Caption for WrapperTagId property.", LastModified = "2010/10/01", Value = "Wrapper Tag Name")]
    public string ToolboxItemBaseWrapperTagNameCaption => this[nameof (ToolboxItemBaseWrapperTagNameCaption)];

    /// <summary>Description for WrapperTagId property.</summary>
    [ResourceEntry("ToolboxItemBaseWrapperTagNameDescription", Description = "Description for WrapperTagId property.", LastModified = "2010/10/01", Value = "The name of the wrapper tag.")]
    public string ToolboxItemBaseWrapperTagNameDescription => this[nameof (ToolboxItemBaseWrapperTagNameDescription)];

    /// <summary>phrase: ShowTopToolbar</summary>
    [ResourceEntry("ShowTopToolbarCaption", Description = "phrase: ShowTopToolbar", LastModified = "2010/03/12", Value = "ShowTopToolbar")]
    public string ShowTopToolbarCaption => this[nameof (ShowTopToolbarCaption)];

    /// <summary>phrase: Defines if to show the top toolbar.</summary>
    [ResourceEntry("ShowTopToolbarDescription", Description = "phrase: Defines if to show the top toolbar.", LastModified = "2010/03/12", Value = "Defines the toolbar above the grid.")]
    public string ShowTopToolbarDescription => this[nameof (ShowTopToolbarDescription)];

    /// <summary>phrase: Show navigation</summary>
    [ResourceEntry("ShowNavigationCaption", Description = "phrase: Show navigation", LastModified = "2010/10/22", Value = "Show navigation")]
    public string ShowNavigationCaption => this[nameof (ShowNavigationCaption)];

    /// <summary>
    /// phrase: A value indicating whether the view will show the previous/next navigation buttons.
    /// </summary>
    [ResourceEntry("ShowNavigationDescription", Description = "phrase: A value indicating whether the view will show the previous/next navigation buttons.", LastModified = "2010/10/22", Value = "A value indicating whether the view will show the previous/next navigation buttons.")]
    public string ShowNavigationDescription => this[nameof (ShowNavigationDescription)];

    /// <summary>phrase: Create blank item</summary>
    [ResourceEntry("CreateBlankItemCaption", Description = "phrase: Create blank item", LastModified = "2010/10/22", Value = "Create blank item")]
    public string CreateBlankItemCaption => this[nameof (CreateBlankItemCaption)];

    /// <summary>
    /// phrase: A value indicating whether the view creates a blank item - server side.
    /// </summary>
    [ResourceEntry("CreateBlankItemDescription", Description = "phrase: A value indicating whether the view creates a blank item - server side.", LastModified = "2012/01/05", Value = "A value indicating whether the view creates a blank item - server side.")]
    public string CreateBlankItemDescription => this[nameof (CreateBlankItemDescription)];

    /// <summary>phrase: Unlock detail item on exit</summary>
    [ResourceEntry("UnlockDetailItemOnExitCaption", Description = "phrase: Unlock detail item on exit", LastModified = "2010/10/22", Value = "Unlock detail item on exit")]
    public string UnlockDetailItemOnExitCaption => this[nameof (UnlockDetailItemOnExitCaption)];

    /// <summary>
    /// phrase: A value indicating whether the system will automatically unlock the currently edited item, when exiting the detail screen.
    /// </summary>
    [ResourceEntry("UnlockDetailItemOnExitDescription", Description = "phrase: A value indicating whether the system will automatically unlock the currently edited item, when exiting the detail screen.", LastModified = "2010/10/22", Value = "A value indicating whether the system will automatically unlock the currently edited item, when exiting the detail screen.")]
    public string UnlockDetailItemOnExitDescription => this[nameof (UnlockDetailItemOnExitDescription)];

    /// <summary>The title of State Command Widget control</summary>
    [ResourceEntry("StateCommandWidgetTitle", Description = "The title of State Command Widget control", LastModified = "2010/01/24", Value = "State Command Widget Title")]
    public string StateCommandWidgetTitle => this[nameof (StateCommandWidgetTitle)];

    /// <summary>The description of StateCommandWidget control."</summary>
    [ResourceEntry("StateCommandWidgetDescription", Description = "The description of StateCommandWidget control.", LastModified = "2010/01/24", Value = "StateCommandWidget description.")]
    public string StateCommandWidgetDescription => this[nameof (StateCommandWidgetDescription)];

    /// <summary>phrase: is selected</summary>
    [ResourceEntry("StateCommandWidgetIsSelectedCaption", Description = "phrase: is selected", LastModified = "2010/01/24", Value = "IsSelected")]
    public string StateCommandWidgetIsSelectedCaption => this[nameof (StateCommandWidgetIsSelectedCaption)];

    /// <summary>
    /// phrase:Represents the selected state of the command widget.
    /// </summary>
    [ResourceEntry("StateCommandWidgetIsSelectedDescription", Description = "phrase:Represents the selected state of the command widget.", LastModified = "2010/01/24", Value = "Represents the selected state of the command widget.")]
    public string StateCommandWidgetIsSelectedDescription => this[nameof (StateCommandWidgetIsSelectedDescription)];

    /// <summary>The title of State Widget control</summary>
    [ResourceEntry("StateWidgetTitle", Description = "The title of State Widget control", LastModified = "2010/01/24", Value = "State Widget Title")]
    public string StateWidgetTitle => this[nameof (StateWidgetTitle)];

    /// <summary>The description of State Widget control.</summary>
    [ResourceEntry("StateWidgetDescription", Description = "The description of State Widget control.", LastModified = "2010/01/24", Value = "State Widget description.")]
    public string StateWidgetDescription => this[nameof (StateWidgetDescription)];

    /// <summary>phrase: states</summary>
    [ResourceEntry("StateWidgetStatesCaption", Description = "phrase: states", LastModified = "2010/01/24", Value = "States")]
    public string StateWidgetStatesCaption => this[nameof (StateWidgetStatesCaption)];

    /// <summary>phrase:Represents state collection for a widget.</summary>
    [ResourceEntry("StateWidgetStatesDescription", Description = "phrase:Represents state collection for a widget.", LastModified = "2010/01/24", Value = "Represents state collection for a widget.")]
    public string StateWidgetStatesDescription => this[nameof (StateWidgetStatesDescription)];

    /// <summary>The title of State Widget control</summary>
    [ResourceEntry("ModeStateWidgetTitle", Description = "The title of State Widget control", LastModified = "2010/04/26", Value = "Mode state widget")]
    public string ModeStateWidgetTitle => this[nameof (ModeStateWidgetTitle)];

    /// <summary>The description of Mode state widget control.</summary>
    [ResourceEntry("ModeStateWidgetDescription", Description = "The description of the Mode State Widget control.", LastModified = "2010/04/26", Value = "The configuration element for mode state widgets")]
    public string ModeStateWidgetDescription => this[nameof (ModeStateWidgetDescription)];

    /// <summary>The title of Literal Widget control</summary>
    [ResourceEntry("LiteralWidgetTitle", Description = "The title of Literal Widget control", LastModified = "2010/01/24", Value = "Literal Widget Title")]
    public string LiteralWidgetTitle => this[nameof (LiteralWidgetTitle)];

    /// <summary>The description of Literal Widget control.</summary>
    [ResourceEntry("LiteralWidgetDescription", Description = "The description of Literal Widget control.", LastModified = "2010/01/24", Value = "Literal Widget description.")]
    public string LiteralWidgetDescription => this[nameof (LiteralWidgetDescription)];

    /// <summary>phrase: text</summary>
    [ResourceEntry("LiteralWidgetTextCaption", Description = "phrase: text", LastModified = "2010/01/24", Value = "Text")]
    public string LiteralWidgetTextCaption => this[nameof (LiteralWidgetTextCaption)];

    /// <summary>phrase:Represents a text of literal widget control.</summary>
    [ResourceEntry("LiteralWidgetTextDescription", Description = "phrase:Represents a text of literal widget control.", LastModified = "2010/01/24", Value = "Represents a text of literal widget control.")]
    public string LiteralWidgetTextDescription => this[nameof (LiteralWidgetTextDescription)];

    /// <summary>phrase: Decision type</summary>
    [ResourceEntry("DecisionScreenDecisionTypeCaption", Description = "phrase: Decision type", LastModified = "2010/01/23", Value = "Decision type")]
    public string DecisionScreenDecisionTypeCaption => this[nameof (DecisionScreenDecisionTypeCaption)];

    /// <summary>
    /// phrase: Type of the decision that user is presented with.
    /// </summary>
    [ResourceEntry("DecisionScreenDecisionTypeDescription", Description = "phrase: Type of the decision that user is presented with.", LastModified = "2010/01/23", Value = "Type of the decision that user is presented with.")]
    public string DecisionScreenDecisionTypeDescription => this[nameof (DecisionScreenDecisionTypeDescription)];

    /// <summary>word: Displayed?</summary>
    [ResourceEntry("DecisionScreenDisplayedCaption", Description = "word: Displayed?", LastModified = "2010/01/23", Value = "Displayed?")]
    public string DecisionScreenDisplayedCaption => this[nameof (DecisionScreenDisplayedCaption)];

    /// <summary>
    /// phrase: Determines whether the screen should be displayed or not.
    /// </summary>
    [ResourceEntry("DecisionScreenDisplayedDescription", Description = "phrase: Determines whether the screen should be displayed or not.", LastModified = "2010/01/23", Value = "Determines whether the screen should be displayed or not.")]
    public string DecisionScreenDisplayedDescription => this[nameof (DecisionScreenDisplayedDescription)];

    /// <summary>phrase: Message text</summary>
    [ResourceEntry("DecisionScreenMessageTextCaption", Description = "phrase: Message text", LastModified = "2010/01/23", Value = "Message text")]
    public string DecisionScreenMessageTextCaption => this[nameof (DecisionScreenMessageTextCaption)];

    /// <summary>phrase: Text of the message user is presented with</summary>
    [ResourceEntry("DecisionScreenMessageTextDescription", Description = "phrase: Text of the message user is presented with.", LastModified = "2010/01/23", Value = "Text of the message user is presented with.")]
    public string DecisionScreenMessageTextDescription => this[nameof (DecisionScreenMessageTextDescription)];

    /// <summary>phrase: Message type</summary>
    [ResourceEntry("DecisionScreenMessageTypeCaption", Description = "phrase: Message type", LastModified = "2010/01/23", Value = "Message type")]
    public string DecisionScreenMessageTypeCaption => this[nameof (DecisionScreenMessageTypeCaption)];

    /// <summary>phrase: Type of the message user is presented with.</summary>
    [ResourceEntry("DecisionScreenMessageTypeDescription", Description = "phrase: Type of the message user is presented with.", LastModified = "2010/01/23", Value = "Type of the message user is presented with.")]
    public string DecisionScreenMessageTypeDescription => this[nameof (DecisionScreenMessageTypeDescription)];

    /// <summary>word: Title</summary>
    [ResourceEntry("DecisionScreenTitleCaption", Description = "word: Title", LastModified = "2010/01/23", Value = "Title")]
    public string DecisionScreenTitleCaption => this[nameof (DecisionScreenTitleCaption)];

    /// <summary>phrase: Title of the decision screen.</summary>
    [ResourceEntry("DecisionScreenTitleDescription", Description = "phrase: Title of the decision screen.", LastModified = "2010/01/23", Value = "Title of the decision screen.")]
    public string DecisionScreenTitleDescription => this[nameof (DecisionScreenTitleDescription)];

    /// <summary>word: Actions</summary>
    [ResourceEntry("DecisionScreenActionsCaption", Description = "word: Actions", LastModified = "2010/01/23", Value = "Actions")]
    public string DecisionScreenActionsCaption => this[nameof (DecisionScreenActionsCaption)];

    /// <summary>
    /// phrase: Collection of command definitions that represent the possible actions of decision screen.
    /// </summary>
    [ResourceEntry("DecisionScreenActionsDescription", Description = "phrase: Collection of command definitions that represent the possible actions of decision screen.", LastModified = "2010/01/23", Value = "Collection of command definitions that represent the possible actions of decision screen.")]
    public string DecisionScreenActionsDescription => this[nameof (DecisionScreenActionsDescription)];

    /// <summary>phrase: Command name</summary>
    [ResourceEntry("WidgetBarTitle", Description = "phrase: Title", LastModified = "2010/01/23", Value = "Title")]
    public string WidgetBarTitle => this[nameof (WidgetBarTitle)];

    /// <summary>phrase: Name of the command that widget fires.</summary>
    [ResourceEntry("WidgetBarTitleDescription", Description = "phrase: The title of the widget bar.", LastModified = "2010/01/23", Value = "The title of the widget bar")]
    public string WidgetBarTitleDescription => this[nameof (WidgetBarTitleDescription)];

    /// <summary>phrase: WidgetBarTitleWrapperTagName</summary>
    [ResourceEntry("WidgetBarTitleWrapperTagName", Description = "phrase: WidgetBarTitleWrapperTagName", LastModified = "2010/01/23", Value = "WidgetBarTitleWrapperTagName")]
    public string WidgetBarTitleWrapperTagName => this[nameof (WidgetBarTitleWrapperTagName)];

    /// <summary>phrase: The wrapper tag of title of the widget bar.</summary>
    [ResourceEntry("WidgetBarTitleWrapperTagNameDescription", Description = "phrase: The wrapper tag of title of the widget bar.", LastModified = "2010/01/23", Value = "The wrapper tag of title of the widget bar.")]
    public string WidgetBarTitleWrapperTagNameDescription => this[nameof (WidgetBarTitleWrapperTagNameDescription)];

    /// <summary>phrase: WrapperTagId</summary>
    [ResourceEntry("WidgetBarWrapperTagIdCaption", Description = "phrase: WrapperTagId", LastModified = "2010/01/23", Value = "WrapperTagId")]
    public string WidgetBarWrapperTagIdCaption => this[nameof (WidgetBarWrapperTagIdCaption)];

    /// <summary>phrase: tag pageId of the widget bar.</summary>
    [ResourceEntry("WidgetBarWrapperTagIdDescription", Description = "phrase: The tag id of the widget bar.", LastModified = "2010/01/23", Value = "The tag id of the widget bar")]
    public string WidgetBarWrapperTagIdDescription => this[nameof (WidgetBarWrapperTagIdDescription)];

    /// <summary>phrase: WrapperTagName</summary>
    [ResourceEntry("WidgetBarWrapperTagNameCaption", Description = "phrase: WrapperTagName", LastModified = "2010/01/23", Value = "WrapperTagName")]
    public string WidgetBarWrapperTagNameCaption => this[nameof (WidgetBarWrapperTagNameCaption)];

    /// <summary>phrase: The tag name of the widget bar.</summary>
    [ResourceEntry("WidgetBarWrapperTagNameDescription", Description = "phrase: The tag name of the widget bar.", LastModified = "2010/01/23", Value = "The tag name of the widget bar.")]
    public string WidgetBarWrapperTagNameDescription => this[nameof (WidgetBarWrapperTagNameDescription)];

    /// <summary>The tag name of the widget element.</summary>
    [ResourceEntry("WrapperTagNameCaption", Description = "The tag name of the widget element.", LastModified = "2010/01/23", Value = "Wrapper tag name")]
    public string WrapperTagNameCaption => this[nameof (WrapperTagNameCaption)];

    /// <summary>phrase: The tag name of the widget element.</summary>
    [ResourceEntry("WrapperTagNameDescription", Description = "The tag name of the widget element.", LastModified = "2010/01/23", Value = "The tag name of the widget element.")]
    public string WrapperTagNameDescription => this[nameof (WrapperTagNameDescription)];

    /// <summary>Wrapper CSS class name.</summary>
    [ResourceEntry("WrapperCssClassCaption", Description = "Wrapper CSS class name.", LastModified = "2010/01/23", Value = "Wrapper CSS class name")]
    public string WrapperCssClassCaption => this[nameof (WrapperCssClassCaption)];

    /// <summary>Wrapper CSS class name.</summary>
    [ResourceEntry("WrapperCssClassDescription", Description = "Wrapper CSS class name.", LastModified = "2010/01/23", Value = "Wrapper CSS class name.")]
    public string WrapperCssClassDescription => this[nameof (WrapperCssClassDescription)];

    /// <summary>Field CSS class name.</summary>
    [ResourceEntry("FieldCssClassCaption", Description = "Field CSS class name.", LastModified = "2010/01/23", Value = "Field CSS class name.")]
    public string FieldCssClassCaption => this[nameof (FieldCssClassCaption)];

    /// <summary>Field CSS class name.</summary>
    [ResourceEntry("FieldCssClassDescription", Description = "Field CSS class name.", LastModified = "2010/01/23", Value = "Field CSS class name.")]
    public string FieldCssClassDescription => this[nameof (FieldCssClassDescription)];

    /// <summary>
    /// The name of the class applied to the section HTML element.
    /// </summary>
    [ResourceEntry("SectionCssClassCaption", Description = "The name of the class applied to the section HTML element.", LastModified = "2010/01/23", Value = "Section CSS class name")]
    public string SectionCssClassCaption => this[nameof (SectionCssClassCaption)];

    /// <summary>
    /// The name of the class applied to the section HTML element.
    /// </summary>
    [ResourceEntry("SectionCssClassDescription", Description = "The name of the class applied to the section HTML element.", LastModified = "2010/01/23", Value = "The name of the class applied to the section HTML element.")]
    public string SectionCssClassDescription => this[nameof (SectionCssClassDescription)];

    /// <summary>Determines whether to show sections.</summary>
    [ResourceEntry("ShowSectionsCaption", Description = "Determines whether to show sections.", LastModified = "2011/03/11", Value = "Show sections")]
    public string ShowSectionsCaption => this[nameof (ShowSectionsCaption)];

    /// <summary>Determines whether to show sections.</summary>
    [ResourceEntry("ShowSectionsDescription", Description = "Determines whether to show sections.", LastModified = "2011/03/11", Value = "Determines whether to show sections.")]
    public string ShowSectionsDescription => this[nameof (ShowSectionsDescription)];

    /// <summary>phrase: WrapperCssClass</summary>
    [ResourceEntry("WidgetBarWrapperCssClassCaption", Description = "phrase: WrapperCssClass", LastModified = "2010/01/23", Value = "WrapperCssClass")]
    public string WidgetBarWrapperCssClassCaption => this[nameof (WidgetBarWrapperCssClassCaption)];

    /// <summary>phrase: The wrapper CSS class of the widget bar.</summary>
    [ResourceEntry("WidgetBarWrapperCssClassDescription", Description = "phrase: The wrapper CSS class of the widget bar.", LastModified = "2012/01/05", Value = "The wrapper CSS class of the widget bar.")]
    public string WidgetBarWrapperCssClassDescription => this[nameof (WidgetBarWrapperCssClassDescription)];

    /// <summary>phrase: Name of the command that widget fires.</summary>
    [ResourceEntry("WidgetBarSectionTitleWrapperTagName", Description = "phrase: The title of the widget bar section.", LastModified = "2010/01/23", Value = "Wrapper tag name")]
    public string WidgetBarSectionTitleWrapperTagName => this[nameof (WidgetBarSectionTitleWrapperTagName)];

    /// <summary>phrase: Name of the command that widget fires.</summary>
    [ResourceEntry("WidgetBarSectionTitleDescription", Description = "phrase: The title of the widget bar section.", LastModified = "2010/01/23", Value = "The title of the widget bar")]
    public string WidgetBarSectionTitleDescription => this[nameof (WidgetBarSectionTitleDescription)];

    /// <summary>phrase: The title wrapper tag name of the widget bar.</summary>
    [ResourceEntry("WidgetBarSectionWrapperTagName", Description = "phrase: The title wrapper tag name of the widget bar.", LastModified = "2010/01/23", Value = "The title wrapper tag name of the widget bar")]
    public string WidgetBarSectionWrapperTagName => this[nameof (WidgetBarSectionWrapperTagName)];

    /// <summary>
    /// phrase:Represents a wrapper tag name of WidgetBarSection.
    /// </summary>
    [ResourceEntry("WidgetBarSectionWrapperTagNameDescription", Description = "phrase:Represents a wrapper tag name of WidgetBarSection.", LastModified = "2010/01/24", Value = "Represents a wrapper tag name of WidgetBarSection.")]
    public string WidgetBarSectionWrapperTagNameDescription => this[nameof (WidgetBarSectionWrapperTagNameDescription)];

    /// <summary>
    /// phrase: The section wrapper tag name of the widget bar.
    /// </summary>
    [ResourceEntry("WidgetBarTitleSectionWrapperTagName", Description = "phrase: The section wrapper tag name of the widget bar.", LastModified = "2010/01/23", Value = "The section wrapper tag name of the widget bar.")]
    public string WidgetBarTitleSectionWrapperTagName => this[nameof (WidgetBarTitleSectionWrapperTagName)];

    /// <summary>
    /// phrase:Represents a wrapper tag name of title of WidgetBarSection.
    /// </summary>
    [ResourceEntry("WidgetBarSectionTitleWrapperTagNameDescription", Description = "phrase:Represents a wrapper tag name of title of WidgetBarSection.", LastModified = "2010/01/24", Value = "Represents a wrapper tag name of title of WidgetBarSection.")]
    public string WidgetBarSectionTitleWrapperTagNameDescription => this[nameof (WidgetBarSectionTitleWrapperTagNameDescription)];

    /// <summary>phrase: WrapperTagId</summary>
    [ResourceEntry("WidgetBarSectionWrapperTagId", Description = "phrase: WrapperTagId", LastModified = "2010/01/24", Value = "WrapperTagId")]
    public string WidgetBarSectionWrapperTagId => this[nameof (WidgetBarSectionWrapperTagId)];

    /// <summary>
    /// phrase:Represents a wrapper tag pageId of WidgetBarSection..
    /// </summary>
    [ResourceEntry("WidgetBarSectionWrapperTagIdDescription", Description = "phrase:Represents a wrapper tag id of WidgetBarSection..", LastModified = "2010/01/24", Value = "Represents a wrapper tag id of WidgetBarSection.")]
    public string WidgetBarSectionWrapperTagIdDescription => this[nameof (WidgetBarSectionWrapperTagIdDescription)];

    /// <summary>phrase: Command name</summary>
    [ResourceEntry("CommandNameCaption", Description = "phrase: Command name", LastModified = "2010/01/23", Value = "Command name")]
    public string CommandNameCaption => this[nameof (CommandNameCaption)];

    /// <summary>phrase: Name of the command that widget fires.</summary>
    [ResourceEntry("CommandNameDescription", Description = "phrase: Name of the command that widget fires.", LastModified = "2010/01/23", Value = "Name of the command that widget fires.")]
    public string CommandNameDescription => this[nameof (CommandNameDescription)];

    /// <summary>phrase: Command action name</summary>
    [ResourceEntry("CommandActionNameCaption", Description = "phrase: Command action name", LastModified = "2010/10/22", Value = "Command action name")]
    public string CommandActionNameCaption => this[nameof (CommandActionNameCaption)];

    /// <summary>
    /// phrase: The name of the action which represents this widget's command.
    /// </summary>
    [ResourceEntry("CommandActionNameDescription", Description = "phrase: The name of the action which represents this widget's command.", LastModified = "2010/10/22", Value = "The name of the action which represents this widget's command.")]
    public string CommandActionNameDescription => this[nameof (CommandActionNameDescription)];

    /// <summary>phrase: Command permissions set</summary>
    [ResourceEntry("CommandPermissionSetCaption", Description = "phrase: Command permissions set", LastModified = "2010/10/22", Value = "Command permissions set")]
    public string CommandPermissionSetCaption => this[nameof (CommandPermissionSetCaption)];

    /// <summary>
    /// phrase: The permission set related to the security action which represents this widget's command.
    /// </summary>
    [ResourceEntry("CommandPermissionSetDescription", Description = "phrase: The permission set related to the security action which represents this widget's command.", LastModified = "2010/10/22", Value = "The permission set related to the security action which represents this widget's command.")]
    public string CommandPermissionSetDescription => this[nameof (CommandPermissionSetDescription)];

    /// <summary>phrase: Button CSS class</summary>
    [ResourceEntry("CommandButtonCssClassCaption", Description = "phrase: Button CSS class", LastModified = "2010/10/22", Value = "Button CSS class")]
    public string CommandButtonCssClassCaption => this[nameof (CommandButtonCssClassCaption)];

    /// <summary>phrase: The CSS class for this widget's button.</summary>
    [ResourceEntry("CommandButtonCssClassDescription", Description = "phrase: The CSS class for this widget's button.", LastModified = "2010/10/22", Value = "The CSS class for this widget's button.")]
    public string CommandButtonCssClassDescription => this[nameof (CommandButtonCssClassDescription)];

    /// <summary>phrase: tooltip</summary>
    [ResourceEntry("ToolTipCaption", Description = "phrase: tooltip", LastModified = "2011/03/07", Value = "tooltip")]
    public string ToolTipCaption => this[nameof (ToolTipCaption)];

    /// <summary>phrase: The tooltip for this widget's button.</summary>
    [ResourceEntry("ToolTipDescription", Description = "phrase: The tooltip for this widget's button.", LastModified = "2011/03/07", Value = "The tooltip for this widget's button.")]
    public string ToolTipDescription => this[nameof (ToolTipDescription)];

    /// <summary>phrase: Close Search Command name</summary>
    [ResourceEntry("CloseSearchCommandNameCaption", Description = "phrase: Close Search Command name", LastModified = "2010/07/23", Value = "Close Search Command name")]
    public string CloseSearchCommandNameCaption => this[nameof (CloseSearchCommandNameCaption)];

    /// <summary>
    /// phrase: Name of the command that widget fires when closing search.
    /// </summary>
    [ResourceEntry("CloseSearchCommandNameDescription", Description = "phrase: Name of the command that widget fires when closing search.", LastModified = "2010/07/23", Value = "Name of the command that widget fires when closing search.")]
    public string CloseSearchCommandNameDescription => this[nameof (CloseSearchCommandNameDescription)];

    /// <summary>phrase: Command button type</summary>
    [ResourceEntry("CommandButtonTypeCaption", Description = "phrase: Command button type.", LastModified = "2010/01/23", Value = "Command button type")]
    public string CommandButtonTypeCaption => this[nameof (CommandButtonTypeCaption)];

    /// <summary>
    /// phrase: Type of the button that represents the command widget.
    /// </summary>
    [ResourceEntry("CommandButtonTypeDescription", Description = "phrase: Type of the button that represents the command widget.", LastModified = "2010/01/23", Value = "Type of the button that represents the command widget.")]
    public string CommandButtonTypeDescription => this[nameof (CommandButtonTypeDescription)];

    /// <summary>phrase: Command argument</summary>
    [ResourceEntry("CommandArgumentCaption", Description = "phrase: Command argument", LastModified = "2010/10/22", Value = "Command argument")]
    public string CommandArgumentCaption => this[nameof (CommandArgumentCaption)];

    /// <summary>
    /// phrase: The argument of the command that the widget fires.
    /// </summary>
    [ResourceEntry("CommandArgumentDescription", Description = "phrase: The argument of the command that the widget fires.", LastModified = "2010/10/22", Value = "The argument of the command that the widget fires.")]
    public string CommandArgumentDescription => this[nameof (CommandArgumentDescription)];

    /// <summary>word: Text</summary>
    [ResourceEntry("CommandTextCaption", Description = "word: Text", LastModified = "2010/01/23", Value = "Text")]
    public string CommandTextCaption => this[nameof (CommandTextCaption)];

    /// <summary>phrase: Text of the command widget</summary>
    [ResourceEntry("CommandTextDescription", Description = "phrase: Text of the command widget.", LastModified = "2010/01/23", Value = "Text of the command widget.")]
    public string CommandTextDescription => this[nameof (CommandTextDescription)];

    /// <summary>word: NavigateUrl</summary>
    [ResourceEntry("CommandNavigateUrlCaption", Description = "word: NavigateUrl", LastModified = "2010/01/28", Value = "NavigateUrl")]
    public string CommandNavigateUrlCaption => this[nameof (CommandNavigateUrlCaption)];

    /// <summary>phrase: Navigate url of the command widget.</summary>
    [ResourceEntry("CommandNavigateUrlDescription", Description = "phrase: Navigate url of the command widget.", LastModified = "2010/01/28", Value = "Navigate url of the command widget.")]
    public string CommandNavigateUrlDescription => this[nameof (CommandNavigateUrlDescription)];

    /// <summary>Translated string, similar to "Is filter command"</summary>
    /// <value>CommandWidget.IsFilterCommand property title</value>
    [ResourceEntry("CommandIsFilterCommandCaption", Description = "CommandWidget.IsFilterCommand property title", LastModified = "2010/02/09", Value = "Is filter command")]
    public string CommandIsFilterCommandCaption => this[nameof (CommandIsFilterCommandCaption)];

    /// <summary>
    /// Translated string, similar to "Should be true if this command will be used as a filter. Used in MasterGridView to select the currently active filter."
    /// </summary>
    /// <value>CommandWidget.IsFilterCommand property description</value>
    [ResourceEntry("CommandIsFilterCommandDescription", Description = "CommandWidget.IsFilterCommand property description", LastModified = "2010/02/09", Value = "Should be true if this command will be used as a filter. Used in MasterGridView to select the currently active filter.")]
    public string CommandIsFilterCommandDescription => this[nameof (CommandIsFilterCommandDescription)];

    /// <summary>phrase: Persistent type to search</summary>
    [ResourceEntry("PersistentTypeToSearchCaption", Description = "phrase: Persistent type to search", LastModified = "2010/10/22", Value = "Persistent type to search")]
    public string PersistentTypeToSearchCaption => this[nameof (PersistentTypeToSearchCaption)];

    /// <summary>phrase: The persistent type to search.</summary>
    [ResourceEntry("PersistentTypeToSearchDescription", Description = "phrase: The persistent type to search.", LastModified = "2010/10/22", Value = "The persistent type to search.")]
    public string PersistentTypeToSearchDescription => this[nameof (PersistentTypeToSearchDescription)];

    /// <summary>Describes the mode of the Search Widget Definition.</summary>
    [ResourceEntry("SearchModeCaption", Description = "Describes the mode of the Search Widget Definition.", LastModified = "2010/01/28", Value = "Search mode")]
    public string SearchModeCaption => this[nameof (SearchModeCaption)];

    /// <summary>Describes the mode of the Search Widget Definition.</summary>
    [ResourceEntry("SearchModeDescription", Description = "Describes the mode of the Search Widget Definition.", LastModified = "2010/01/28", Value = "Specify the mode of the Search Widget Definition: NotSet, None, Basic, Advanced, Both")]
    public string SearchModeDescription => this[nameof (SearchModeDescription)];

    /// <summary>word: Default page size</summary>
    [ResourceEntry("DynamicCommandDefaultPageSizeCaption", Description = "word: Default page size", LastModified = "2010/03/22", Value = "Default page size")]
    public string DynamicCommandDefaultPageSizeCaption => this[nameof (DynamicCommandDefaultPageSizeCaption)];

    /// <summary>
    /// word: Specifies how many items appear when the list is first loaded
    /// </summary>
    [ResourceEntry("DynamicCommandDefaultPageSizeDescription", Description = "word: Specifies how many items appear when the list is first loaded", LastModified = "2010/03/22", Value = "Default page size")]
    public string DynamicCommandDefaultPageSizeDescription => this[nameof (DynamicCommandDefaultPageSizeDescription)];

    /// <summary>Phrase: Header text</summary>
    [ResourceEntry("DynamicCommandHeaderTextCaption", Description = "Phrase: Header text", LastModified = "2010/05/18", Value = "Header text")]
    public string DynamicCommandHeaderTextCaption => this[nameof (DynamicCommandHeaderTextCaption)];

    /// <summary>
    /// Phrase: The text displayed before the list of commands
    /// </summary>
    [ResourceEntry("DynamicCommandHeaderTextDescription", Description = "Phrase: The text displayed before the list of commands", LastModified = "2010/05/18", Value = "The text displayed before the list of commands")]
    public string DynamicCommandHeaderTextDescription => this[nameof (DynamicCommandHeaderTextDescription)];

    /// <summary>Phrase: Header text CSS class</summary>
    [ResourceEntry("DynamicCommandHeaderTextCssClassCaption", Description = "Phrase: Header text CSS class", LastModified = "2012/01/05", Value = "Header text CSS class")]
    public string DynamicCommandHeaderTextCssClassCaption => this[nameof (DynamicCommandHeaderTextCssClassCaption)];

    /// <summary>
    /// Phrase: The css class for the control displaying the header text
    /// </summary>
    [ResourceEntry("DynamicCommandHeaderTextCssClassDescription", Description = "Phrase: The css class for the control displaying the header text", LastModified = "2010/05/18", Value = "The css class for the control displaying the header text")]
    public string DynamicCommandHeaderTextCssClassDescription => this[nameof (DynamicCommandHeaderTextCssClassDescription)];

    /// <summary>word: More link text</summary>
    [ResourceEntry("DynamicCommandMoreLinkTextCaption", Description = "word: More link text", LastModified = "2010/03/22", Value = "More link text")]
    public string DynamicCommandMoreLinkTextCaption => this[nameof (DynamicCommandMoreLinkTextCaption)];

    /// <summary>word: Less link text</summary>
    [ResourceEntry("DynamicCommandLessLinkTextCaption", Description = "word: Less link text", LastModified = "2010/05/13", Value = "Less link text")]
    public string DynamicCommandLessLinkTextCaption => this[nameof (DynamicCommandLessLinkTextCaption)];

    /// <summary>word: The text of the link to show more items</summary>
    [ResourceEntry("DynamicCommandMoreLinkTextDescription", Description = "word: The text of the link to show more items", LastModified = "2010/03/22", Value = "The text of the link to show more items")]
    public string DynamicCommandMoreLinkTextDescription => this[nameof (DynamicCommandMoreLinkTextDescription)];

    /// <summary>word: The text of the link to show less items</summary>
    [ResourceEntry("DynamicCommandLessLinkTextDescription", Description = "word: The text of the link to show less items", LastModified = "2010/05/13", Value = "The text of the link to show less items")]
    public string DynamicCommandLessLinkTextDescription => this[nameof (DynamicCommandLessLinkTextDescription)];

    /// <summary>Phrase: More link CSS class</summary>
    [ResourceEntry("DynamicCommandMoreLinkCssClassCaption", Description = "Phrase: More link CSS class", LastModified = "2012/01/05", Value = "More link CSS class")]
    public string DynamicCommandMoreLinkCssClassCaption => this[nameof (DynamicCommandMoreLinkCssClassCaption)];

    /// <summary>Phrase: Less link CSS class</summary>
    [ResourceEntry("DynamicCommandLessLinkCssClassCaption", Description = "Phrase: Less link CSS class", LastModified = "2012/01/05", Value = "Less link CSS class")]
    public string DynamicCommandLessLinkCssClassCaption => this[nameof (DynamicCommandLessLinkCssClassCaption)];

    /// <summary>Phrase: The CSS class of the link to show more items</summary>
    [ResourceEntry("DynamicCommandMoreLinkCssClassDescription", Description = "Phrase: The CSS class of the link to show more items", LastModified = "2012/01/05", Value = "The CSS class of the link to show more items")]
    public string DynamicCommandMoreLinkCssClassDescription => this[nameof (DynamicCommandMoreLinkCssClassDescription)];

    /// <summary>Phrase: The CSS class of the link to show less items</summary>
    [ResourceEntry("DynamicCommandLessLinkCssClassDescription", Description = "Phrase: The CSS class of the link to show less items", LastModified = "2012/01/05", Value = "The CSS class of the link to show less items")]
    public string DynamicCommandLessLinkCssClassDescription => this[nameof (DynamicCommandLessLinkCssClassDescription)];

    /// <summary>phrase: Selected item CSS class</summary>
    [ResourceEntry("DynamicCommandSelectedItemCssClassCaption", Description = "phrase: Selected item CSS class", LastModified = "2012/01/05", Value = "Selected item CSS class")]
    public string DynamicCommandSelectedItemCssClassCaption => this[nameof (DynamicCommandSelectedItemCssClassCaption)];

    /// <summary>word: The css class of the selected item</summary>
    [ResourceEntry("DynamicCommandSelectedItemCssClassDescription", Description = "word: The css class of the selected item", LastModified = "2010/03/22", Value = "The css class of the selected item")]
    public string DynamicCommandSelectedItemCssClassDescription => this[nameof (DynamicCommandSelectedItemCssClassDescription)];

    /// <summary>word: Web service url</summary>
    [ResourceEntry("DynamicCommandWebServiceUrlCaption", Description = "word: Web service url", LastModified = "2011/03/11", Value = "Web service Url")]
    public string DynamicCommandWebServiceUrlCaption => this[nameof (DynamicCommandWebServiceUrlCaption)];

    /// <summary>word: The url of the web service used for binding</summary>
    [ResourceEntry("DynamicCommandWebServiceUrlDescription", Description = "word: The url of the web service used for binding", LastModified = "2010/03/22", Value = "The url of the web service used for binding")]
    public string DynamicCommandWebServiceUrlDescription => this[nameof (DynamicCommandWebServiceUrlDescription)];

    /// <summary>word: Child items service URL</summary>
    [ResourceEntry("DynamicCommandChildItemsServiceUrlCaption", Description = "word: Child items service URL", LastModified = "2010/06/03", Value = "Child items service URL")]
    public string DynamicCommandChildItemsServiceUrlCaption => this[nameof (DynamicCommandChildItemsServiceUrlCaption)];

    /// <summary>
    /// word: The URL of the service used to get child items when binding to a hierarchical source
    /// </summary>
    [ResourceEntry("DynamicCommandChildItemsServiceUrlDescription", Description = "word: The URL of the service used to get child items when binding to a hierarchical source", LastModified = "2010/06/03", Value = "The URL of the service used to get child items when binding to a hierarchical source")]
    public string DynamicCommandChildItemsServiceUrlDescription => this[nameof (DynamicCommandChildItemsServiceUrlDescription)];

    /// <summary>word: Predecessor service URL</summary>
    [ResourceEntry("DynamicCommandPredecessorServiceUrlCaption", Description = "word: Predecessor service URL", LastModified = "2010/06/03", Value = "Predecessor service URL")]
    public string DynamicCommandPredecessorServiceUrlCaption => this[nameof (DynamicCommandPredecessorServiceUrlCaption)];

    /// <summary>
    /// word: The URL of the service used to get predecessors of an item when binding to a hierarchical source
    /// </summary>
    [ResourceEntry("DynamicCommandPredecessorServiceUrlDescription", Description = "word: The URL of the service used to get predecessors of an item when binding to a hierarchical source", LastModified = "2010/06/03", Value = "The URL of the service used to get predecessors of an item when binding to a hierarchical source")]
    public string DynamicCommandPredecessorServiceUrlDescription => this[nameof (DynamicCommandPredecessorServiceUrlDescription)];

    /// <summary>Phrase: Url Parameters</summary>
    [ResourceEntry("DynamicCommandUrlParametersCaption", Description = "Phrase: Url Parameters", LastModified = "2010/04/30", Value = "Url Parameters")]
    public string DynamicCommandUrlParametersCaption => this[nameof (DynamicCommandUrlParametersCaption)];

    /// <summary>
    /// Phrase: The url parameters passed to the service when binding on the client
    /// </summary>
    [ResourceEntry("DynamicCommandUrlParametersDescription", Description = "Phrase: The url parameters passed to the service when binding on the client", LastModified = "2010/04/30", Value = "The url parameters passed to the service when binding on the client")]
    public string DynamicCommandUrlParametersDescription => this[nameof (DynamicCommandUrlParametersDescription)];

    /// <summary>Phrase: Data source</summary>
    [ResourceEntry("DynamicCommandDataSourceCaption", Description = "Phrase: Data source", LastModified = "2010/04/30", Value = "Data source")]
    public string DynamicCommandDataSourceCaption => this[nameof (DynamicCommandDataSourceCaption)];

    /// <summary>
    /// Phrase: The collection to bind to when binding on the server
    /// </summary>
    [ResourceEntry("DynamicCommandDataSourceDescription", Description = "Phrase: The collection to bind to when binding on the server", LastModified = "2010/04/30", Value = "The collection to bind to when binding on the server")]
    public string DynamicCommandDataSourceDescription => this[nameof (DynamicCommandDataSourceDescription)];

    /// <summary>Phrase: Data text field</summary>
    [ResourceEntry("DynamicCommandDataTextFieldCaption", Description = "Phrase: Data text field", LastModified = "2010/04/30", Value = "Data text field")]
    public string DynamicCommandDataTextFieldCaption => this[nameof (DynamicCommandDataTextFieldCaption)];

    /// <summary>Phrase: The data text field (displayed in the UI)</summary>
    [ResourceEntry("DynamicCommandDataTextFieldDescription", Description = "Phrase: The data text field (displayed in the UI)", LastModified = "2012/01/05", Value = "The data text field (displayed in the UI)")]
    public string DynamicCommandDataTextFieldDescription => this[nameof (DynamicCommandDataTextFieldDescription)];

    /// <summary>Phrase: Data value field</summary>
    [ResourceEntry("DynamicCommandDataValueFieldCaption", Description = "Phrase: Data value field", LastModified = "2010/04/30", Value = "Data value field")]
    public string DynamicCommandDataValueFieldCaption => this[nameof (DynamicCommandDataValueFieldCaption)];

    /// <summary>
    /// Phrase: The data value field (used as value, not in UI)
    /// </summary>
    [ResourceEntry("DynamicCommandDataValueFieldDescription", Description = "Phrase: The data value field (used as value, not in UI)", LastModified = "2010/04/30", Value = "The data value field (used as value, not in UI)")]
    public string DynamicCommandDataValueFieldDescription => this[nameof (DynamicCommandDataValueFieldDescription)];

    /// <summary>Phrase: Bind to</summary>
    [ResourceEntry("DynamicCommandBindToCaption", Description = "Phrase: Bind to", LastModified = "2010/04/30", Value = "Bind to")]
    public string DynamicCommandBindToCaption => this[nameof (DynamicCommandBindToCaption)];

    /// <summary>
    /// Phrase: Determines the binding mode (server, or client) and representation
    /// </summary>
    [ResourceEntry("DynamicCommandBindToDescription", Description = "Phrase: Determines the binding mode (server, or client) and representation", LastModified = "2010/04/30", Value = "Determines the binding mode (server, or client) and representation")]
    public string DynamicCommandBindToDescription => this[nameof (DynamicCommandBindToDescription)];

    /// <summary>Phrase: Client item template</summary>
    [ResourceEntry("DynamicCommandClientItemTemplateCaption", Description = "Phrase: Client item template", LastModified = "2010/06/03", Value = "Client item template")]
    public string DynamicCommandClientItemTemplateCaption => this[nameof (DynamicCommandClientItemTemplateCaption)];

    /// <summary>
    /// Phrase: The item template used when binding on the client (in client and hierarchical mode)
    /// </summary>
    [ResourceEntry("DynamicCommandClientItemTemplateDescription", Description = "Phrase: The item template used when binding on the client (in client and hierarchical mode)", LastModified = "2010/06/03", Value = "The item template used when binding on the client (in client and hierarchical mode)")]
    public string DynamicCommandClientItemTemplateDescription => this[nameof (DynamicCommandClientItemTemplateDescription)];

    /// <summary>Phrase: Content type</summary>
    [ResourceEntry("DynamicCommandContentTypeCaption", Description = "Phrase: Content type", LastModified = "2010/06/03", Value = "Content type")]
    public string DynamicCommandContentTypeCaption => this[nameof (DynamicCommandContentTypeCaption)];

    /// <summary>
    /// Phrase: The item template used when binding on the client (in client and hierarchical mode)
    /// </summary>
    [ResourceEntry("DynamicCommandContentTypeDescription", Description = "Phrase: The item template used when binding on the client (in client and hierarchical mode)", LastModified = "2010/06/03", Value = "The content type used when binding on the client (in client and hierarchical mode)")]
    public string DynamicCommandContentTypeDescription => this[nameof (DynamicCommandContentTypeDescription)];

    /// <summary>Phrase: Dynamic module type Id</summary>
    [ResourceEntry("DynamicCommandDynamicModuleTypeIdCaption", Description = "Phrase: Dynamic module type Id", LastModified = "2014/01/31", Value = "Dynamic module type Id")]
    public string DynamicCommandDynamicModuleTypeIdCaption => this[nameof (DynamicCommandDynamicModuleTypeIdCaption)];

    /// <summary>
    /// Phrase: The dynamic type Id used by the sorting in the toobar.
    /// </summary>
    [ResourceEntry("DynamicCommandDynamicModuleTypeIdDescription", Description = "Phrase: The dynamic type Id used by the sorting in the toobar.", LastModified = "2014/01/31", Value = "The dynamic type Id used by the sorting in the toobar.")]
    public string DynamicCommandDynamicModuleTypeIdDescription => this[nameof (DynamicCommandDynamicModuleTypeIdDescription)];

    /// <summary>Phrase: Custom commands</summary>
    [ResourceEntry("DynamicCommandCustomCommandsCaption", Description = "Phrase: Custom commands", LastModified = "2010/04/30", Value = "Custom commands")]
    public string DynamicCommandCustomCommandsCaption => this[nameof (DynamicCommandCustomCommandsCaption)];

    /// <summary>
    /// Phrase: A list of additional commands to be displayed as items in the widget
    /// </summary>
    [ResourceEntry("DynamicCommandCustomCommandsDescription", Description = "Phrase: A list of additional commands to be displayed as items in the widget", LastModified = "2010/04/30", Value = "A list of additional commands to be displayed as items in the widget")]
    public string DynamicCommandCustomCommandsDescription => this[nameof (DynamicCommandCustomCommandsDescription)];

    /// <summary>Phrase: Command name</summary>
    [ResourceEntry("DynamicCommandCommandNameCaption", Description = "Phrase: Command name", LastModified = "2010/07/22", Value = "Command name")]
    public string DynamicCommandCommandNameCaption => this[nameof (DynamicCommandCommandNameCaption)];

    /// <summary>
    /// Phrase: The name of the command that is fired by the dynamic command widget
    /// </summary>
    [ResourceEntry("DynamicCommandCommandNameDescription", Description = "Phrase: The name of the command that is fired by the dynamic command widget", LastModified = "2010/07/22", Value = "The name of the command that is fired by the dynamic command widget")]
    public string DynamicCommandCommandNameDescription => this[nameof (DynamicCommandCommandNameDescription)];

    /// <summary>Phrase: Content item type</summary>
    [ResourceEntry("ContentItemWidgetItemTypeCaption", Description = "Phrase: Content item type", LastModified = "2010/05/20", Value = "Content item type")]
    public string ContentItemWidgetItemTypeCaption => this[nameof (ContentItemWidgetItemTypeCaption)];

    /// <summary>
    /// Phrase: The type of the item to be displayed by the widget
    /// </summary>
    [ResourceEntry("ContentItemWidgetItemTypeDescription", Description = "Phrase: The type of the item to be displayed by the widget", LastModified = "2010/05/20", Value = "The type of the item to be displayed by the widget")]
    public string ContentItemWidgetItemTypeDescription => this[nameof (ContentItemWidgetItemTypeDescription)];

    /// <summary>word: Web service url</summary>
    [ResourceEntry("ContentItemWidgetServiceBaseUrlCaption", Description = "word: Web service url", LastModified = "2011/03/11", Value = "Web service Url")]
    public string ContentItemWidgetServiceBaseUrlCaption => this[nameof (ContentItemWidgetServiceBaseUrlCaption)];

    /// <summary>word: The url of the web service used for binding</summary>
    [ResourceEntry("ContentItemWidgetServiceBaseUrlDescription", Description = "word: The url of the web service used for binding", LastModified = "2010/09/23", Value = "The url of the web service used for binding")]
    public string ContentItemWidgetServiceBaseUrlDescription => this[nameof (ContentItemWidgetServiceBaseUrlDescription)];

    /// <summary>Phrase: Show actions menu</summary>
    [ResourceEntry("LibraryWidgetShowActionMenuCaption", Description = "Phrase: Show actions menu", LastModified = "2010/05/28", Value = "Show actions menu")]
    public string LibraryWidgetShowActionMenuCaption => this[nameof (LibraryWidgetShowActionMenuCaption)];

    /// <summary>
    /// Phrase: Specifies whether the action menu should be shown in the library widget
    /// </summary>
    [ResourceEntry("LibraryWidgetShowActionMenuDescription", Description = "Phrase: Specifies whether the action menu should be shown in the library widget", LastModified = "2010/05/28", Value = "Specifies whether the action menu should be shown in the library widget")]
    public string LibraryWidgetShowActionMenuDescription => this[nameof (LibraryWidgetShowActionMenuDescription)];

    /// <summary>Phrase: Single item name</summary>
    [ResourceEntry("LibraryWidgetItemNameCaption", Description = "Phrase: Single item name", LastModified = "2010/05/28", Value = "Single item name")]
    public string LibraryWidgetItemNameCaption => this[nameof (LibraryWidgetItemNameCaption)];

    /// <summary>
    /// Phrase: The name of a single item in the library represented by the widget
    /// </summary>
    [ResourceEntry("LibraryWidgetItemNameDescription", Description = "Phrase: The name of a single item in the library represented by the widget", LastModified = "2010/05/28", Value = "The name of a single item in the library represented by the widget")]
    public string LibraryWidgetItemNameDescription => this[nameof (LibraryWidgetItemNameDescription)];

    /// <summary>Phrase: Items name</summary>
    [ResourceEntry("LibraryWidgetItemsNameCaption", Description = "Phrase: Items name", LastModified = "2010/05/28", Value = "Items name")]
    public string LibraryWidgetItemsNameCaption => this[nameof (LibraryWidgetItemsNameCaption)];

    /// <summary>
    /// Phrase: The plural name of items in the library represented by the widget
    /// </summary>
    [ResourceEntry("LibraryWidgetItemsNameDescription", Description = "Phrase: The plural name of items in the library represented by the widget", LastModified = "2010/05/28", Value = "The plural name of items in the library represented by the widget")]
    public string LibraryWidgetItemsNameDescription => this[nameof (LibraryWidgetItemsNameDescription)];

    /// <summary>Phrase: Library name</summary>
    [ResourceEntry("LibraryWidgetLibraryNameCaption", Description = "Phrase: Library name", LastModified = "2010/05/28", Value = "Library name")]
    public string LibraryWidgetLibraryNameCaption => this[nameof (LibraryWidgetLibraryNameCaption)];

    /// <summary>
    /// Phrase: The UI name of the library represented by the widget
    /// </summary>
    [ResourceEntry("LibraryWidgetLibraryNameDescription", Description = "Phrase: The UI name of the library represented by the widget", LastModified = "2010/05/28", Value = "The UI name of the library represented by the widget")]
    public string LibraryWidgetLibraryNameDescription => this[nameof (LibraryWidgetLibraryNameDescription)];

    /// <summary>Phrase: SupportsReordering</summary>
    [ResourceEntry("LibraryWidgetSupportsReorderingCaption", Description = "Phrase: SupportsReordering", LastModified = "2010/09/24", Value = "SupportsReordering")]
    public string LibraryWidgetSupportsReorderingCaption => this[nameof (LibraryWidgetSupportsReorderingCaption)];

    /// <summary>
    /// Phrase: Gets or sets a value indicating whether widget supports reordering.
    /// </summary>
    [ResourceEntry("LibraryWidgetSupportsReorderingDescription", Description = "Phrase: Gets or sets a value indicating whether widget supports reordering", LastModified = "2010/09/24", Value = "Gets or sets a value indicating whether widget supports reordering")]
    public string LibraryWidgetSupportsReorderingDescription => this[nameof (LibraryWidgetSupportsReorderingDescription)];

    /// <summary>phrase: Pre-defined filtering ranges</summary>
    [ResourceEntry("PredefinedFilteringRangesCaption", Description = "phrase: Pre-defined filtering ranges", LastModified = "2010/10/22", Value = "Pre-defined filtering ranges")]
    public string PredefinedFilteringRangesCaption => this[nameof (PredefinedFilteringRangesCaption)];

    /// <summary>phrase: Pre-defined filtering ranges.</summary>
    [ResourceEntry("PredefinedFilteringRangesDescription", Description = "phrase: Pre-defined filtering ranges.", LastModified = "2010/10/22", Value = "Pre-defined filtering ranges.")]
    public string PredefinedFilteringRangesDescription => this[nameof (PredefinedFilteringRangesDescription)];

    /// <summary>phrase: Property name to filter</summary>
    [ResourceEntry("PropertyNameToFilterCaption", Description = "phrase: Property name to filter", LastModified = "2010/10/22", Value = "Property name to filter")]
    public string PropertyNameToFilterCaption => this[nameof (PropertyNameToFilterCaption)];

    /// <summary>phrase: The name of the property to be filtered.</summary>
    [ResourceEntry("PropertyNameToFilterDescription", Description = "phrase: The name of the property to be filtered.", LastModified = "2010/10/22", Value = "The name of the property to be filtered.")]
    public string PropertyNameToFilterDescription => this[nameof (PropertyNameToFilterDescription)];

    /// <summary>Resource strings for ProvidersListWidget class.</summary>
    [ResourceEntry("ProvidersListWidgetCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "ProvidersListWidget class")]
    public string ProvidersListWidgetCaption => this[nameof (ProvidersListWidgetCaption)];

    /// <summary>Resource strings for ProvidersListWidget class.</summary>
    [ResourceEntry("ProvidersListWidgetDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for ProvidersListWidget class.")]
    public string ProvidersListWidgetDescription => this[nameof (ProvidersListWidgetDescription)];

    /// <summary>The type of the data item.</summary>
    [ResourceEntry("DataItemTypeCaption", Description = "The type of the data item.", LastModified = "2010/02/22", Value = "DataItemTypeCaption")]
    public string DataItemTypeCaption => this[nameof (DataItemTypeCaption)];

    /// <summary>Resource strings for data item type.</summary>
    [ResourceEntry("DataItemTypeDescription", Description = "Resource strings for data item type.", LastModified = "2010/02/22", Value = "Resource strings for data item type.")]
    public string DataItemTypeDescription => this[nameof (DataItemTypeDescription)];

    /// <summary>The type of the manager.</summary>
    [ResourceEntry("ManagerTypeCaption", Description = "The type of the manager.", LastModified = "2010/02/22", Value = "ManagerTypeCaption")]
    public string ManagerTypeCaption => this[nameof (ManagerTypeCaption)];

    /// <summary>Resource strings for type of the manager.</summary>
    [ResourceEntry("ManagerTypeDescription", Description = "Resource strings for type of the manager.", LastModified = "2010/02/22", Value = "Resource strings for type of the manager.")]
    public string ManagerTypeDescription => this[nameof (ManagerTypeDescription)];

    /// <summary>The select provider message.</summary>
    [ResourceEntry("SelectProviderMessageCaption", Description = "The select provider message.", LastModified = "2010/02/22", Value = "SelectProviderMessage")]
    public string SelectProviderMessageCaption => this[nameof (SelectProviderMessageCaption)];

    /// <summary>Resource strings for select provider message.</summary>
    [ResourceEntry("SelectProviderMessageDescription", Description = "Resource strings for select provider message.", LastModified = "2010/02/22", Value = "Resource strings for select provider message.")]
    public string SelectProviderMessageDescription => this[nameof (SelectProviderMessageDescription)];

    /// <summary>The select provider message css class.</summary>
    [ResourceEntry("SelectProviderMessageCssClassCaption", Description = "The select provider message css class.", LastModified = "2010/02/22", Value = "SelectProviderMessageCssClass")]
    public string SelectProviderMessageCssClassCaption => this[nameof (SelectProviderMessageCssClassCaption)];

    /// <summary>
    /// Resource strings for select provider message CSS class.
    /// </summary>
    [ResourceEntry("SelectProviderMessageCssClassDescription", Description = "Resource strings for select provider message CSS class.", LastModified = "2012/01/05", Value = "Resource strings for select provider message CSS class.")]
    public string SelectProviderMessageCssClassDescription => this[nameof (SelectProviderMessageCssClassDescription)];

    /// <summary>The type of the secured object related to the widget</summary>
    [ResourceEntry("RelatedSecuredObjectTypeNameCaption", Description = "The type of the secured object related to the widget.", LastModified = "2011/07/28", Value = "The type of the secured object related to the widget")]
    public string RelatedSecuredObjectTypeNameCaption => this[nameof (RelatedSecuredObjectTypeNameCaption)];

    /// <summary>
    /// Description for the type of the secured object related to the widget.
    /// </summary>
    [ResourceEntry("RelatedSecuredObjectTypeNameDescription", Description = "Description for the type of the secured object related to the widget.", LastModified = "2011/07/28", Value = "Description for the type of the secured object related to the widget.")]
    public string RelatedSecuredObjectTypeNameDescription => this[nameof (RelatedSecuredObjectTypeNameDescription)];

    /// <summary>The Id of the secured object related to the widget</summary>
    [ResourceEntry("RelatedSecuredObjectIdCaption", Description = "The Id of the secured object related to the widget.", LastModified = "2011/07/28", Value = "The Id of the secured object related to the widget")]
    public string RelatedSecuredObjectIdCaption => this[nameof (RelatedSecuredObjectIdCaption)];

    /// <summary>
    /// Description for the Id of the secured object related to the widget.
    /// </summary>
    [ResourceEntry("RelatedSecuredObjectIdDescription", Description = "Description for the Id of the secured object related to the widget.", LastModified = "2011/07/28", Value = "Description for the Id of the secured object related to the widget.")]
    public string RelatedSecuredObjectIdDescription => this[nameof (RelatedSecuredObjectIdDescription)];

    /// <summary>The provider name of the object related to the widget</summary>
    [ResourceEntry("ObjectProviderNameCaption", Description = "The provider name of the object related to the widget.", LastModified = "2013/03/05", Value = "The provider name of the object related to the widget")]
    public string ObjectProviderNameCaption => this[nameof (ObjectProviderNameCaption)];

    /// <summary>
    /// Description for the provider name of the object related to the widget.
    /// </summary>
    [ResourceEntry("ObjectProviderNameDescription", Description = "Description for the provider name of the object related to the widget.", LastModified = "2013/03/05", Value = "Description for the provider name of the object related to the widget.")]
    public string ObjectProviderNameDescription => this[nameof (ObjectProviderNameDescription)];

    /// <summary>
    /// The open in same window name of the object related to the widget
    /// </summary>
    [ResourceEntry("OpenInSameWindowCaption", Description = "CommandWidget.OpenInSameWindow property title", LastModified = "2014/07/04", Value = "Is open in new window command")]
    public string OpenInSameWindowCaption => this[nameof (OpenInSameWindowCaption)];

    /// <summary>
    /// Description for the open in same window name of the object related to the widget
    /// </summary>
    [ResourceEntry("OpenInSameWindowDescription", Description = "CommandWidget.OpenInSameWindow property title", LastModified = "2014/07/04", Value = "Is open in new window command")]
    public string OpenInSameWindowDescription => this[nameof (OpenInSameWindowDescription)];

    /// <summary>
    /// The title for condition of the command widget definition.
    /// </summary>
    [ResourceEntry("ConditionCaption", Description = "CommandWidget.Condition property title", LastModified = "2014/07/04", Value = "Condition")]
    public string ConditionCaption => this[nameof (ConditionCaption)];

    /// <summary>
    /// Description for for condition of the command widget definition.
    /// </summary>
    [ResourceEntry("ConditionDescription", Description = "CommandWidget.Condition property description", LastModified = "2014/07/04", Value = "An expression to determin whether the item should be visible")]
    public string ConditionDescription => this[nameof (ConditionDescription)];

    /// <summary>
    /// Description for the provider name of the secured object related to the widget.
    /// </summary>
    /// <value>The provider name of the secured object related to the widget.</value>
    [ResourceEntry("RelatedSecuredObjectProviderNameDescription", Description = "Description for the provider name of the secured object related to the widget.", LastModified = "2015/11/25", Value = "The provider name of the secured object related to the widget.")]
    public string RelatedSecuredObjectProviderNameDescription => this[nameof (RelatedSecuredObjectProviderNameDescription)];

    /// <summary>
    /// The name of the manager type of the secured object related to the widget
    /// </summary>
    [ResourceEntry("RelatedSecuredObjectManagerTypeNameCaption", Description = "The name of the manager type of the secured object related to the widget", LastModified = "2011/07/28", Value = "The name of the manager type of the secured object related to the widget")]
    public string RelatedSecuredObjectManagerTypeNameCaption => this[nameof (RelatedSecuredObjectManagerTypeNameCaption)];

    /// <summary>
    /// Description for the manager type of the secured object related to the widget.
    /// </summary>
    [ResourceEntry("RelatedSecuredObjectManagerTypeNameDescription", Description = "Description for the manager type of the secured object related to the widget.", LastModified = "2011/07/28", Value = "Description for the manager type of the secured object related to the widget.")]
    public string RelatedSecuredObjectManagerTypeNameDescription => this[nameof (RelatedSecuredObjectManagerTypeNameDescription)];

    /// <summary>Resource strings for widget class.</summary>
    [ResourceEntry("WidgetCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Widget class")]
    public string WidgetCaption => this[nameof (WidgetCaption)];

    /// <summary>Resource strings for widget class.</summary>
    [ResourceEntry("WidgetDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for widget class.")]
    public string WidgetDescription => this[nameof (WidgetDescription)];

    /// <summary>Describes the mode of the Search Widget Definition.</summary>
    [ResourceEntry("WidgetNameCaption", Description = "Describes the name of the widget.", LastModified = "2010/01/28", Value = "Name")]
    public string WidgetNameCaption => this[nameof (WidgetNameCaption)];

    /// <summary>Describes the mode of the Search Widget Definition.</summary>
    [ResourceEntry("WidgetNameDescription", Description = "Describes the name of the widget.", LastModified = "2010/01/28", Value = "Describes the name of the widget.")]
    public string WidgetNameDescription => this[nameof (WidgetNameDescription)];

    /// <summary>Describes the container pageId of the widget.</summary>
    [ResourceEntry("WidgetContainerIdCaption", Description = "Describes the container id of the widget.", LastModified = "2010/01/28", Value = "ContainerId")]
    public string WidgetContainerIdCaption => this[nameof (WidgetContainerIdCaption)];

    /// <summary>Describes the container pageId of the widget.</summary>
    [ResourceEntry("WidgetContainerIdDescription", Description = "Describes the container id of the widget.", LastModified = "2010/01/28", Value = "Describes the container id of the widget.")]
    public string WidgetContainerIdDescription => this[nameof (WidgetContainerIdDescription)];

    /// <summary>Describes the container css class of the widget.</summary>
    [ResourceEntry("WidgetCssClassCaption", Description = "Describes the css class of the widget.", LastModified = "2010/01/28", Value = "CssClass")]
    public string WidgetCssClassCaption => this[nameof (WidgetCssClassCaption)];

    /// <summary>Describes the CSS class of the widget.</summary>
    [ResourceEntry("WidgetCssClassDescription", Description = "Describes the CSS class of the widget.", LastModified = "2012/01/05", Value = "Describes the CSS class of the widget.")]
    public string WidgetCssClassDescription => this[nameof (WidgetCssClassDescription)];

    /// <summary>Describes the command text of the widget.</summary>
    [ResourceEntry("WidgetCommandTextCaption", Description = "Describes the command text of the widget.", LastModified = "2010/01/28", Value = "CommandText")]
    public string WidgetCommandTextCaption => this[nameof (WidgetCommandTextCaption)];

    /// <summary>Describes the command text of the widget.</summary>
    [ResourceEntry("WidgetCommandTextDescription", Description = "Describes the command text of the widget.", LastModified = "2010/01/28", Value = "Describes the command text of the widget.")]
    public string WidgetCommandTextDescription => this[nameof (WidgetCommandTextDescription)];

    /// <summary>Describes the virtual path of the widget.</summary>
    [ResourceEntry("WidgetVirtualPathCaption", Description = "Describes the virtual path of the widget.", LastModified = "2010/01/28", Value = "VirtualPath")]
    public string WidgetVirtualPathCaption => this[nameof (WidgetVirtualPathCaption)];

    /// <summary>Describes the virtual path of the widget.</summary>
    [ResourceEntry("WidgetVirtualPathDescription", Description = "Describes the virtual path of the widget.", LastModified = "2010/01/28", Value = "Describes the virtual path of the widget.")]
    public string WidgetVirtualPathDescription => this[nameof (WidgetVirtualPathDescription)];

    /// <summary>Describes the wrapper tag Id of the widget.</summary>
    [ResourceEntry("WidgetWrapperTagIdCaption", Description = "Describes the wrapper tag Id of the widget.", LastModified = "2010/01/28", Value = "WrapperTagId")]
    public string WidgetWrapperTagIdCaption => this[nameof (WidgetWrapperTagIdCaption)];

    /// <summary>Describes the wrapper tag Id of the widget.</summary>
    [ResourceEntry("WidgetWrapperTagIdDescription", Description = "Describes the wrapper tag Id of the widget.", LastModified = "2010/01/28", Value = "Describes the wrapper tag Id of the widget.")]
    public string WidgetWrapperTagIdDescription => this[nameof (WidgetWrapperTagIdDescription)];

    /// <summary>Describes the wrapper tag name of the widget.</summary>
    [ResourceEntry("WidgetWrapperTagNameCaption", Description = "Describes the wrapper tag name of the widget.", LastModified = "2010/01/28", Value = "WrapperTagName")]
    public string WidgetWrapperTagNameCaption => this[nameof (WidgetWrapperTagNameCaption)];

    /// <summary>Describes the wrapper tag name of the widget.</summary>
    [ResourceEntry("WidgetWrapperTagNameDescription", Description = "Describes the wrapper tag name of the widget.", LastModified = "2010/01/28", Value = "Describes the wrapper tag name of the widget.")]
    public string WidgetWrapperTagNameDescription => this[nameof (WidgetWrapperTagNameDescription)];

    /// <summary>Describes the type of the widget.</summary>
    [ResourceEntry("WidgetTypeCaption", Description = "Describes the type of the widget.", LastModified = "2010/01/28", Value = "Type")]
    public string WidgetTypeCaption => this[nameof (WidgetTypeCaption)];

    /// <summary>Describes the type of the widget.</summary>
    [ResourceEntry("WidgetTypeDescription", Description = "Describes the type of the widget.", LastModified = "2010/01/28", Value = "Describes the type of the widget.")]
    public string WidgetTypeDescription => this[nameof (WidgetTypeDescription)];

    /// <summary>Describes the IsSeparator property of the widget.</summary>
    [ResourceEntry("WidgetIsSeparatorCaption", Description = "Describes the IsSeparator property of the widget.", LastModified = "2010/01/28", Value = "Type")]
    public string WidgetIsSeparatorCaption => this[nameof (WidgetIsSeparatorCaption)];

    /// <summary>Describes the IsSeparator property of the widget.</summary>
    [ResourceEntry("WidgetIsSeparatorDescription", Description = "Describes the IsSeparator property of the widget.", LastModified = "2010/01/28", Value = "Describes the IsSeparator property of the widget.")]
    public string WidgetIsSeparatorDescription => this[nameof (WidgetIsSeparatorDescription)];

    /// <summary>
    /// Title for ItemSelectorDataMemberElement configuration element
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberElementTitle", Description = "Title for ItemSelectorDataMemberElement configuration element", LastModified = "2010/02/17", Value = "ItemSelectorDataMember")]
    public string ItemSelectorDataMemberElementTitle => this[nameof (ItemSelectorDataMemberElementTitle)];

    /// <summary>
    /// Description for ItemSelectorDataMemberElement configuration element
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberElementDescription", Description = "Description for ItemSelectorDataMemberElement configuration element", LastModified = "2010/02/17", Value = "Configuration element that represents a data item in an item selector.")]
    public string ItemSelectorDataMemberElementDescription => this[nameof (ItemSelectorDataMemberElementDescription)];

    /// <summary>
    /// Title for the ColumnTemplate property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberElementColumnTemplateTitle", Description = "Title for the ColumnTemplate property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/17", Value = "Column Template")]
    public string ItemSelectorDataMemberElementColumnTemplateTitle => this[nameof (ItemSelectorDataMemberElementColumnTemplateTitle)];

    /// <summary>
    /// Description for the ColumnTemplate property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberElementColumnTemplateDescription", Description = "Description for the ColumnTemplate property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Template of the data member.")]
    public string ItemSelectorDataMemberElementColumnTemplateDescription => this[nameof (ItemSelectorDataMemberElementColumnTemplateDescription)];

    /// <summary>
    /// Title for the ElementHeader property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberElementHeaderTextTitle", Description = "Title for the ElementHeader property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Element Header")]
    public string ItemSelectorDataMemberElementHeaderTextTitle => this[nameof (ItemSelectorDataMemberElementHeaderTextTitle)];

    /// <summary>
    /// Description for the ElementHeader property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberElementHeaderTextDescription", Description = "Description for the ElementHeader property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Gets or sets the header label for the data member")]
    public string ItemSelectorDataMemberElementHeaderTextDescription => this[nameof (ItemSelectorDataMemberElementHeaderTextDescription)];

    /// <summary>
    /// Title for the IsSearchField property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberIsSearchFieldTitle", Description = "Title for the IsSearchField property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Is Search Field")]
    public string ItemSelectorDataMemberIsSearchFieldTitle => this[nameof (ItemSelectorDataMemberIsSearchFieldTitle)];

    /// <summary>
    /// Description for the IsSearchField property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberElementIsSearchFieldDescription", Description = "Description for the IsSearchField property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Gets or sets value indicating whether this data member is a search field")]
    public string ItemSelectorDataMemberElementIsSearchFieldDescription => this[nameof (ItemSelectorDataMemberElementIsSearchFieldDescription)];

    /// <summary>
    /// Title for the IsExtendedSearchField property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberIsExtendedSearchFieldTitle", Description = "Title for the IsExtendedSearchField property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Is Search Field of Type Lstring")]
    public string ItemSelectorDataMemberIsExtendedSearchFieldTitle => this[nameof (ItemSelectorDataMemberIsExtendedSearchFieldTitle)];

    /// <summary>
    /// Description for the IsExtendedSearchField property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberElementIsExtendedSearchFieldDescription", Description = "Description for the IsExtendedSearchField property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Gets or sets value indicating whether this data member is a search field of type Lstring")]
    public string ItemSelectorDataMemberElementIsExtendedSearchFieldDescription => this[nameof (ItemSelectorDataMemberElementIsExtendedSearchFieldDescription)];

    /// <summary>
    /// Title for the Name property of the ItemSelectorDataMemberElement configuration element.
    /// </summary>
    [ResourceEntry("ItemSelectorDataMemberNameTitle", Description = "Title for the Name property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Name")]
    public string ItemSelectorDataMemberNameTitle => this[nameof (ItemSelectorDataMemberNameTitle)];

    /// <summary>Gets or sets the data member name.</summary>
    [ResourceEntry("ItemSelectorDataMemberElementNameDescription", Description = "Description for the Name property of the ItemSelectorDataMemberElement configuration element.", LastModified = "2010/02/18", Value = "Gets or sets the data member name")]
    public string ItemSelectorDataMemberElementNameDescription => this[nameof (ItemSelectorDataMemberElementNameDescription)];

    /// <summary>
    /// Resource strings for the text of "all items" filter button class.
    /// </summary>
    [ResourceEntry("ItemSelectorElementBaseAllItemsTextTitle", Description = "Resource strings for the text of 'all items' filter button class.", LastModified = "2010/02/22", Value = "ItemSelectorElementBaseAllItemsTextTitle")]
    public string ItemSelectorElementBaseAllItemsTextTitle => this[nameof (ItemSelectorElementBaseAllItemsTextTitle)];

    /// <summary>
    /// Resource strings for the text of "all items" filter button class.
    /// </summary>
    [ResourceEntry("ItemSelectorElementBaseAllItemsTextDescription", Description = "Resource strings for the text of 'all items' filter button class.", LastModified = "2010/02/22", Value = "Resource strings for the text of 'all items' filter button class.")]
    public string ItemSelectorElementBaseAllItemsTextDescription => this[nameof (ItemSelectorElementBaseAllItemsTextDescription)];

    /// <summary>Resource strings for Link class.</summary>
    [ResourceEntry("LinkConfigCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "Link class")]
    public string LinkConfigCaption => this[nameof (LinkConfigCaption)];

    /// <summary>Resource strings for Link class.</summary>
    [ResourceEntry("LinkConfigDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for Link class.")]
    public string LinkConfigDescription => this[nameof (LinkConfigDescription)];

    /// <summary>Resource strings for NavigateUrl.</summary>
    [ResourceEntry("LinkConfigNavigateUrlTitle", Description = "The NavigateUrl of this class.", LastModified = "2010/02/22", Value = "NavigateUrl")]
    public string LinkConfigNavigateUrlTitle => this[nameof (LinkConfigNavigateUrlTitle)];

    /// <summary>Resource strings for NavigateUrl of Link class.</summary>
    [ResourceEntry("LinkConfigNavigateUrlDescription", Description = "Resource strings for NavigateUrl of Link class", LastModified = "2010/02/22", Value = "Resource strings for NavigateUrl of Link class")]
    public string LinkConfigNavigateUrlDescription => this[nameof (LinkConfigNavigateUrlDescription)];

    /// <summary>Resource strings for CommandName.</summary>
    [ResourceEntry("LinkConfigCommandNameTitle", Description = "The CommandName of this class.", LastModified = "2010/02/22", Value = "NavigateUrl")]
    public string LinkConfigCommandNameTitle => this[nameof (LinkConfigCommandNameTitle)];

    /// <summary>Resource strings for CommandName of Link class.</summary>
    [ResourceEntry("LinkConfigCommandNameDescription", Description = "Resource strings for CommandName of Link class", LastModified = "2010/02/22", Value = "Resource strings for CommandName of Link class")]
    public string LinkConfigCommandNameDescription => this[nameof (LinkConfigCommandNameDescription)];

    /// <summary>Resource strings for Name of Link.</summary>
    [ResourceEntry("LinkConfigNameTitle", Description = "Resource strings for Name of Link.", LastModified = "2010/02/22", Value = "Name")]
    public string LinkConfigNameTitle => this[nameof (LinkConfigNameTitle)];

    /// <summary>Resource strings for Name of Link class.</summary>
    [ResourceEntry("LinkConfigNameDescription", Description = "Resource strings for Name of Link class", LastModified = "2010/02/22", Value = "Resource strings for Name of Link class")]
    public string LinkConfigNameDescription => this[nameof (LinkConfigNameDescription)];

    /// <summary>phrase: Expandable extender definition</summary>
    [ResourceEntry("ExpandableDefinitionCaption", Description = "phrase: Expandable extender definition", LastModified = "2009/01/14", Value = "Expandable extender definition")]
    public string ExpandableDefinitionCaption => this[nameof (ExpandableDefinitionCaption)];

    /// <summary>
    /// phrase: Defines the behavior of the expandable control extender.
    /// </summary>
    [ResourceEntry("ExpandableDefinitionDescription", Description = "phrase: Defines the behavior of the expandable control extender.", LastModified = "2009/01/14", Value = "Defines the behavior of the expandable control extender.")]
    public string ExpandableDefinitionDescription => this[nameof (ExpandableDefinitionDescription)];

    /// <summary>Message: LDAP Settings</summary>
    /// <value>LDAP Settings</value>
    [ResourceEntry("LdapConnectionsConfigTitle", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "LDAP Settings")]
    public string LdapConnectionsConfigTitle => this[nameof (LdapConnectionsConfigTitle)];

    /// <summary>Message: LDAP connection settings</summary>
    /// <value>LDAP connection settings</value>
    [ResourceEntry("LdapConnectionsDescription", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "LDAP connection settings")]
    public string LdapConnectionsDescription => this[nameof (LdapConnectionsDescription)];

    /// <summary>Message: Defines the default ldap connection name</summary>
    [ResourceEntry("DefaultLdapConnection", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the default ldap connection name.")]
    public string DefaultLdapConnection => this[nameof (DefaultLdapConnection)];

    /// <summary>Message: Defines the default ldap connection name</summary>
    [ResourceEntry("DefaultLdapConnectionDescription", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines the default ldap connection name.")]
    public string DefaultLdapConnectionDescription => this[nameof (DefaultLdapConnectionDescription)];

    /// <summary>Message: LDAP Connections</summary>
    /// <value>LDAP Connections</value>
    [ResourceEntry("LdapConnectionsTitle", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "LDAP Connections")]
    public string LdapConnectionsTitle => this[nameof (LdapConnectionsTitle)];

    /// <summary>Message: LDAP Connections</summary>
    /// <value>LDAP Connections</value>
    [ResourceEntry("LdapResultCacheExpirationTime", Description = "Describes configuration element. ", LastModified = "2009/01/29", Value = "Result Cache ExpirationTime")]
    public string LdapResultCacheExpirationTime => this[nameof (LdapResultCacheExpirationTime)];

    /// <summary>Message: LDAP Server address</summary>
    /// <value>LdapServerName</value>
    [ResourceEntry("LdapServerName", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "LDAP Server address")]
    public string LdapServerName => this[nameof (LdapServerName)];

    /// <summary>Message:  LDAP Server port</summary>
    /// <value>LdapServerPort</value>
    [ResourceEntry("LdapServerPort", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "LDAP Server port")]
    public string LdapServerPort => this[nameof (LdapServerPort)];

    /// <summary>
    /// Message: The domain used in addition to the user name (telerik\user)
    /// </summary>
    /// <value>LdapConnectionDomain</value>
    [ResourceEntry("LdapConnectionDomain", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "The domain used in addition to the user name (telerik\\user)")]
    public string LdapConnectionDomain => this[nameof (LdapConnectionDomain)];

    /// <summary>Message: User name used to connect to the LDAP server</summary>
    /// <value>LdapConnectionUsername</value>
    [ResourceEntry("LdapConnectionUsername", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "User name used to connect to the LDAP server")]
    public string LdapConnectionUsername => this[nameof (LdapConnectionUsername)];

    /// <summary>Message: Password used to connect to the LDAP server</summary>
    /// <value>Password used to connect to the LDAP server</value>
    [ResourceEntry("LdapConnectionPassword", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "Password used to connect to the LDAP server")]
    public string LdapConnectionPassword => this[nameof (LdapConnectionPassword)];

    /// <summary>Message: Whether to use SSL for the connection</summary>
    /// <value>Whether to use SSL for the connection</value>
    [ResourceEntry("LdapUseSSL", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "Use SSL for the connection")]
    public string LdapUseSSL => this[nameof (LdapUseSSL)];

    /// <summary>
    /// Message: Maximum number of users that are return at once
    /// </summary>
    /// <value>Maximum number of users that are return at once</value>
    [ResourceEntry("LdapMaxReturnedUsers", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "Maximum number of users to be returned")]
    public string LdapMaxReturnedUsers => this[nameof (LdapMaxReturnedUsers)];

    /// <summary>
    /// Message: Root distinguished name used to retrieve the users
    /// </summary>
    /// <value>Root distinguished name used to retrieve the users</value>
    [ResourceEntry("LdapUserDNs", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "Root distinguished name used to retrieve the users")]
    public string LdapUserDNs => this[nameof (LdapUserDNs)];

    /// <summary>Message: LDAP filter used when getting the users</summary>
    /// <value>LDAP filter used when getting the users</value>
    [ResourceEntry("LdapUserFilter", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "LDAP filter used when getting the users")]
    public string LdapUserFilter => this[nameof (LdapUserFilter)];

    /// <summary>Message: Maximum number of the returned roles</summary>
    /// <value>Maximum number of the returned roles</value>
    [ResourceEntry("LdapMaxReturnedRoles", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "Maximum number of the returned roles")]
    public string LdapMaxReturnedRoles => this[nameof (LdapMaxReturnedRoles)];

    /// <summary>
    /// Message: Root distinguished name used to retrieve the user roles
    /// </summary>
    /// <value>Root distinguished name used to retrieve the user roles</value>
    [ResourceEntry("LdapRolesDNs", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "Root distinguished name used to retrieve the user roles")]
    public string LdapRolesDNs => this[nameof (LdapRolesDNs)];

    /// <summary>Message: LDAP filter used when getting the user roles</summary>
    /// <value>LDAP filter used when getting the user roles</value>
    [ResourceEntry("LdapRolesFilter", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "LDAP filter used when getting the user roles")]
    public string LdapRolesFilter => this[nameof (LdapRolesFilter)];

    /// <summary>
    /// Message: If set the connection to the LDAP server is made with the current user credentials
    /// </summary>
    /// <value>If set the connection to the LDAP server is made with the current user credentials</value>
    [ResourceEntry("LdapConnectWithLoginCredentials", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "If set the connection to the LDAP server is made with the current user credentials")]
    public string LdapConnectWithLoginCredentials => this[nameof (LdapConnectWithLoginCredentials)];

    /// <summary>Message:LDAP protocol version</summary>
    /// <value>LDAP protocol version</value>
    [ResourceEntry("LdapVersion", Description = "Describes configuration element. ", LastModified = "2016/07/19", Value = "LDAP protocol version")]
    public string LdapVersion => this["ldapVersion"];

    /// <summary>
    /// Message:Defines the version of LDAP protocol (the default value is 2)
    /// </summary>
    /// <value>Defines the version of LDAP protocol (the default value is 2)</value>
    [ResourceEntry("LdapVersionDescription", Description = "Defines the version of LDAP protocol (the default value is 2).", LastModified = "2016/07/19", Value = "LDAP protocol version")]
    public string LdapVersionDescription => this["ldapVersionDescription"];

    /// <summary>
    /// Message:LDAP authentication type: Anonymous, Basic, Digest, Dpa, External, Kerberos, Msn, Negotiate, Ntlm or Sicily
    /// </summary>
    /// <value>LDAP authentication type: Anonymous, Basic, Digest, Dpa, External, Kerberos, Msn, Negotiate, Ntlm or Sicily</value>
    [ResourceEntry("LdapAuthenticationType", Description = "Describes configuration element. ", LastModified = "2009/01/14", Value = "LDAP authentication type: Anonymous, Basic, Digest, Dpa, External, Kerberos, Msn, Negotiate, Ntlm or Sicily")]
    public string LdapAuthenticationType => this[nameof (LdapAuthenticationType)];

    /// <summary>
    /// Message:LDAP authentication type: Anonymous, Basic, Digest, Dpa, External, Kerberos, Msn, Negotiate, Ntlm or Sicily
    /// </summary>
    /// <value>LDAP authentication type: Anonymous, Basic, Digest, Dpa, External, Kerberos, Msn, Negotiate, Ntlm or Sicily</value>
    [ResourceEntry("Description", Description = "Describes configuration element.", LastModified = "2009/01/14", Value = "Description")]
    public string Description => this[nameof (Description)];

    /// <summary>Message: Defines security configuration settings.</summary>
    [ResourceEntry("LdapConnectionsConfig", Description = "Describes configuration element.", LastModified = "2009/05/13", Value = "Defines LDAP configuration settings.")]
    public string LdapConnectionsConfig => this[nameof (LdapConnectionsConfig)];

    /// <summary>
    /// Message: Defines the Sitefinity types which are mapped to LDAP objects.
    /// </summary>
    [ResourceEntry("LdapTypesMapping", Description = "Describes configuration element.", LastModified = "2010/01/03", Value = "LDAP Types Mapping")]
    public string LdapTypesMapping => this[nameof (LdapTypesMapping)];

    /// <summary>
    /// Message: Defines the Sitefinity types which are mapped to LDAP objects.
    /// </summary>
    [ResourceEntry("LdapPropertiesMapping", Description = "Describes configuration element.", LastModified = "2010/01/03", Value = "LDAP Properties Mapping")]
    public string LdapPropertiesMapping => this[nameof (LdapPropertiesMapping)];

    /// <summary>
    /// Message: Defines the Sitefinity types which are mapped to LDAP objects.
    /// </summary>
    [ResourceEntry("LDAPMapping", Description = "Describes configuration element.", LastModified = "2010/01/03", Value = "LDAP Mapping")]
    public string LdapMapping => this["LDAPMapping"];

    /// <summary>Output Cache Settings</summary>
    [ResourceEntry("OutputCacheElementTitle", Description = "The title of a configuration element.", LastModified = "2011/01/05", Value = "Output Cache Settings")]
    public string OutputCacheElementTitle => this[nameof (OutputCacheElementTitle)];

    /// <summary>
    /// Configure how output cache settings are applied to pages and libraries.
    /// </summary>
    [ResourceEntry("OutputCacheElementDescription", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Configure how output cache settings are applied to pages and libraries.")]
    public string OutputCacheElementDescription => this[nameof (OutputCacheElementDescription)];

    /// <summary>Enable Client Cache</summary>
    [ResourceEntry("EnableClientCacheTitle", Description = "The title of a configuration element.", LastModified = "2011/03/15", Value = "Enable Client Cache")]
    public string EnableClientCacheTitle => this[nameof (EnableClientCacheTitle)];

    /// <summary>
    /// phrase: Controls whether client cache is globally enabled. If you disable client cache, no resources are cached on the client browser. By default, client cache is enabled.
    /// </summary>
    [ResourceEntry("EnableClientCacheDescription", Description = "phrase: Controls whether client cache is globally enabled. If you disable client cache, no resources are cached on the client browser. By default, client cache is enabled.", LastModified = "2018/09/27", Value = "Controls whether client cache is globally enabled. If you disable client cache, no resources are cached on the client browser. By default, client cache is enabled.")]
    public string EnableClientCacheDescription => this[nameof (EnableClientCacheDescription)];

    /// <summary>Enable Output Cache</summary>
    [ResourceEntry("EnableOutputCacheTitle", Description = "The title of a configuration element.", LastModified = "2011/01/05", Value = "Enable Output Cache")]
    public string EnableOutputCacheTitle => this[nameof (EnableOutputCacheTitle)];

    /// <summary>
    /// Enables/disables the page and control output cache.
    /// If disabled, no pages are cached regardless of the programmatic or declarative settings.
    /// Default value is true.
    /// </summary>
    [ResourceEntry("EnableOutputCacheDescription", Description = "Describes configuration element.", LastModified = "2011/01/05", Value = "Controls the page and widget output cache. If you disable output cache, no pages are cached regardless of the programmatic or declarative settings. By default, output cache is enabled.")]
    public string EnableOutputCacheDescription => this[nameof (EnableOutputCacheDescription)];

    /// <summary>Default Output Cache Profile</summary>
    [ResourceEntry("DefaultOutputProfileTitle", Description = "The title of a configuration element.", LastModified = "2011/01/05", Value = "Default Output Cache Profile")]
    public string DefaultOutputProfileTitle => this[nameof (DefaultOutputProfileTitle)];

    /// <summary>Applied on all pages.</summary>
    [ResourceEntry("DefaultOutputProfileDescription", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Applied on all pages.")]
    public string DefaultOutputProfileDescription => this[nameof (DefaultOutputProfileDescription)];

    /// <summary>Word: Images</summary>
    [ResourceEntry("Images", Description = "Word: Images", LastModified = "2017/01/30", Value = "Images")]
    public string Images => this[nameof (Images)];

    /// <summary>Word: Documents</summary>
    [ResourceEntry("Documents", Description = "Word: Documents", LastModified = "2017/01/30", Value = "Documents")]
    public string Documents => this[nameof (Documents)];

    /// <summary>Word: Images</summary>
    [ResourceEntry("Videos", Description = "Word: Videos", LastModified = "2017/01/30", Value = "Videos")]
    public string Videos => this[nameof (Videos)];

    /// <summary>Default cache profile for image libraries</summary>
    [ResourceEntry("DefaultImageProfileTitle", Description = "The title of a configuration element.", LastModified = "2018/09/27", Value = "Default cache profile for image libraries")]
    public string DefaultImageProfileTitle => this[nameof (DefaultImageProfileTitle)];

    /// <summary>
    /// Specifies the default image cache profile description.
    /// </summary>
    [ResourceEntry("DefaultImageProfileDescription", Description = "Describes configuration element.", LastModified = "2017/01/30", Value = "Specifies the default image library cache profile.")]
    public string DefaultImageProfileDescription => this[nameof (DefaultImageProfileDescription)];

    /// <summary>Default cache profile for document libraries</summary>
    [ResourceEntry("DefaultDocumentProfileTitle", Description = "The title of a configuration element.", LastModified = "2018/09/27", Value = "Default cache profile for document libraries")]
    public string DefaultDocumentProfileTitle => this[nameof (DefaultDocumentProfileTitle)];

    /// <summary>
    /// Specifies the default document library cache profile description.
    /// </summary>
    [ResourceEntry("DefaultDocumentProfileDescription", Description = "Describes configuration element.", LastModified = "2017/01/30", Value = "Specifies the default document library cache profile.")]
    public string DefaultDocumentProfileDescription => this[nameof (DefaultDocumentProfileDescription)];

    /// <summary>Default cache profile for video libraries</summary>
    [ResourceEntry("DefaultVideoProfileTitle", Description = "The title of a configuration element.", LastModified = "2018/09/27", Value = "Default cache profile for video libraries")]
    public string DefaultVideoProfileTitle => this[nameof (DefaultVideoProfileTitle)];

    /// <summary>
    /// Specifies the default video library cache profile description.
    /// </summary>
    [ResourceEntry("DefaultVideoProfileDescription", Description = "Describes configuration element.", LastModified = "2017/01/26", Value = "Specifies the default video library cache profile.")]
    public string DefaultVideoProfileDescription => this[nameof (DefaultVideoProfileDescription)];

    /// <summary>Output Cache Profiles</summary>
    [ResourceEntry("OutputCacheProfilesTitle", Description = "The title of a configuration element.", LastModified = "2017/02/02", Value = "Page Cache Profiles")]
    public string OutputCacheProfilesTitle => this[nameof (OutputCacheProfilesTitle)];

    /// <summary>Collection of named output cache configurations.</summary>
    [ResourceEntry("OutputCacheProfilesDescription", Description = "Describes configuration element.", LastModified = "2011/01/05", Value = "Collection of named page output cache configurations.")]
    public string OutputCacheProfilesDescription => this[nameof (OutputCacheProfilesDescription)];

    /// <summary>Client Cache Profiles</summary>
    [ResourceEntry("ClientCacheProfilesTitle", Description = "The title of a configuration element.", LastModified = "2017/02/02", Value = "Media Cache Profiles")]
    public string ClientCacheProfilesTitle => this[nameof (ClientCacheProfilesTitle)];

    /// <summary>Collection of named output client configurations.</summary>
    [ResourceEntry("ClientCacheProfilesDescription", Description = "Describes configuration element.", LastModified = "2017/02/02", Value = "Collection of named media cache configurations.")]
    public string ClientCacheProfilesDescription => this[nameof (ClientCacheProfilesDescription)];

    /// <summary>phrase: Output cache profile element</summary>
    [ResourceEntry("OutputCacheProfileElementTitle", Description = "phrase: Output cache profile element", LastModified = "2011/01/24", Value = "Output cache profile element")]
    public string OutputCacheProfileElementTitle => this[nameof (OutputCacheProfileElementTitle)];

    /// <summary>
    /// phrase: Configures the output cache profile that can be used by the application pages and controls.
    /// </summary>
    [ResourceEntry("OutputCacheProfileElementDescription", Description = "phrase: Configures the output cache profile that can be used by the application pages and controls.", LastModified = "2011/01/24", Value = "Configures the output cache profile that can be used by the application pages and controls.")]
    public string OutputCacheProfileElementDescription => this[nameof (OutputCacheProfileElementDescription)];

    /// <summary>phrase: Client cache profile element</summary>
    [ResourceEntry("ClientCacheProfileElementTitle", Description = "phrase: Client cache profile element", LastModified = "2011/03/15", Value = "Output cache profile element")]
    public string ClientCacheProfileElementTitle => this[nameof (ClientCacheProfileElementTitle)];

    /// <summary>phrase: Configures the client cache profile.</summary>
    [ResourceEntry("ClientCacheProfileElementDescription", Description = "phrase: Configures the client cache profile.", LastModified = "2011/03/15", Value = "Configures the client cache profile.")]
    public string ClientCacheProfileElementDescription => this[nameof (ClientCacheProfileElementDescription)];

    /// <summary>Enabled</summary>
    [ResourceEntry("OutputCacheProfileEnabledTitle", Description = "The title of a configuration element.", LastModified = "2011/01/05", Value = "Enabled")]
    public string OutputCacheProfileEnabledTitle => this[nameof (OutputCacheProfileEnabledTitle)];

    /// <summary>
    /// A value indicating whether caching is enabled by this profile.
    /// </summary>
    [ResourceEntry("OutputCacheProfileEnabledDescription", Description = "Describes configuration element.", LastModified = "2011/01/05", Value = "A value indicating whether caching is enabled by this profile.")]
    public string OutputCacheProfileEnabledDescription => this[nameof (OutputCacheProfileEnabledDescription)];

    /// <summary>Enabled</summary>
    [ResourceEntry("ClientCacheProfileEnabledTitle", Description = "The title of a configuration element.", LastModified = "2011/01/05", Value = "Enabled")]
    public string ClientCacheProfileEnabledTitle => this[nameof (ClientCacheProfileEnabledTitle)];

    /// <summary>
    /// True - 'Cache-Control: public' with a specified 'max-age'; False - 'Cache-Control: no-cache'; not specified - no 'Cache-Control' header is sent to the client.
    /// </summary>
    [ResourceEntry("ClientCacheProfileEnabledDescription", Description = "phrase: True - 'Cache-Control: public' with a specified 'max-age'; False - 'Cache-Control: no-cache'; not specified - no 'Cache-Control' header is sent to the client.", LastModified = "2011/11/24", Value = "True - 'Cache-Control: public' with a specified 'max-age'; False - 'Cache-Control: no-cache'; not specified - no 'Cache-Control' header is sent to the client.")]
    public string ClientCacheProfileEnabledDescription => this[nameof (ClientCacheProfileEnabledDescription)];

    /// <summary>Duration</summary>
    [ResourceEntry("OutputCacheDurationTitle", Description = "The title of a configuration element.", LastModified = "2011/01/05", Value = "Duration")]
    public string OutputCacheDurationTitle => this[nameof (OutputCacheDurationTitle)];

    /// <summary>
    /// The time duration, in seconds, during which the page or control is cached.
    /// </summary>
    [ResourceEntry("OutputCacheDurationDescription", Description = "Describes configuration element.", LastModified = "2011/01/05", Value = "The time duration, in seconds, during which the page or control is cached.")]
    public string OutputCacheDurationDescription => this[nameof (OutputCacheDurationDescription)];

    /// <summary>Location title</summary>
    [ResourceEntry("OutputCacheProfileLocationTitle", Description = "The title of a configuration property Location of element OutputCacheProfileElement", LastModified = "2016/10/20", Value = "Location")]
    public string OutputCacheProfileLocationTitle => this[nameof (OutputCacheProfileLocationTitle)];

    /// <summary>Location title</summary>
    [ResourceEntry("OutputCacheProfileLocationDescription", Description = "The description of a configuration property Location of element OutputCacheProfileElement", LastModified = "2016/10/20", Value = "Specifies the location of the output-cached HTTP response. One of the following values are supported: Server - (default) The output cache is located on the Web server where the request was processed; Any - The output cache can be located on the browser client (where the request originated), on a proxy server (or any other server) participating in the request, or on the server where the request was processed;")]
    public string OutputCacheProfileLocationDescription => this[nameof (OutputCacheProfileLocationDescription)];

    /// <summary>MaxAge title</summary>
    [ResourceEntry("MaxAgeTitle", Description = "The title of a configuration property ClientMaxAge of element OutputCacheProfileElement", LastModified = "2016/10/20", Value = "Client Max Age")]
    public string MaxAgeTitle => this[nameof (MaxAgeTitle)];

    /// <summary>MaxAge description</summary>
    [ResourceEntry("MaxAgeDesctiption", Description = "The description of a configuration property ClientMaxAge of element OutputCacheProfileElement", LastModified = "2016/10/20", Value = "Specifies the maximum time in seconds that the fetched response is allowed to be reused from the time of the request. Corresponds to the 'max-age' directive of the Cache-Control header. If not specified the client caching is used, the Duration property will set the client cache max age.")]
    public string MaxAgeDesctiption => this[nameof (MaxAgeDesctiption)];

    /// <summary>MaxAge description</summary>
    [ResourceEntry("MaxAgeForStaticResourcesDescription", Description = "The description of a configuration property ClientMaxAge of element OutputCacheProfileElement", LastModified = "2018/09/27", Value = "Мaximum time in seconds that the fetched response is allowed to be reused from the time of the request for static resources like JavaScript or CSS. Corresponds to the max-age directive of the cache-control header. If not specified, the system uses the client caching and the Duration property sets the client cache max age.")]
    public string MaxAgeForStaticResourcesDescription => this[nameof (MaxAgeForStaticResourcesDescription)];

    /// <summary>ProxyMaxAge title</summary>
    [ResourceEntry("ProxyMaxAgeTitle", Description = "The title of a configuration property ProxyMaxAge of element OutputCacheProfileElement", LastModified = "2016/10/20", Value = "Proxy Max Age")]
    public string ProxyMaxAgeTitle => this[nameof (ProxyMaxAgeTitle)];

    /// <summary>ProxyMaxAge description</summary>
    [ResourceEntry("ProxyMaxAgeDesctiption", Description = "The description of a configuration property ProxyMaxAge of element OutputCacheProfileElement", LastModified = "2016/10/20", Value = "This setting corresponds to 's-maxage' cache-control directive and it is explicitly for proxy servers and CDNs. It overrides the max-age directive and expires header field when present.")]
    public string ProxyMaxAgeDesctiption => this[nameof (ProxyMaxAgeDesctiption)];

    /// <summary>Duration</summary>
    [ResourceEntry("ClientCacheDurationTitle", Description = "The title of a configuration element.", LastModified = "2011/03/15", Value = "Duration")]
    public string ClientCacheDurationTitle => this[nameof (ClientCacheDurationTitle)];

    [ResourceEntry("ClientCacheDurationDescription", Description = "phrase: The time duration, in seconds, during which the image or control is cached (Cache-Control: public, max-age=<duration>).", LastModified = "2011/11/24", Value = "The time duration, in seconds, during which the image or control is cached (Cache-Control: public, max-age=<duration>).")]
    public string ClientCacheDurationDescription => this[nameof (ClientCacheDurationDescription)];

    /// <summary>OutputCacheMaxSizeTitle</summary>
    [ResourceEntry("OutputCacheMaxSizeTitle", Description = "The title of a configuration element.", LastModified = "2011/03/15", Value = "Indicates max size in KB of item to be cached. The items that exceed that limit are not cached.")]
    public string OutputCacheMaxSizeTitle => this[nameof (OutputCacheMaxSizeTitle)];

    /// <summary>
    /// Indicates max size in KB of item to be cached. The items that exceed that limit are not cached.
    /// </summary>
    [ResourceEntry("OutputCacheMaxSizeDescription", Description = "Describes configuration element.", LastModified = "2012/10/17", Value = "Indicates max size in KB of item to be cached. The items that exceed that limit are not cached. This setting affects Media(image,doc,video) caching, but doesn't affect Pages output caching! For media libraries using File system blob storage it is advisable to turn off library output caching")]
    public string OutputCacheMaxSizeDescription => this[nameof (OutputCacheMaxSizeDescription)];

    /// <summary>Sliding expiration</summary>
    [ResourceEntry("OutputCacheSlidingExpirationTitle", Description = "The title of a configuration element.", LastModified = "2017/02/15", Value = "Sliding expiration")]
    public string OutputCacheSlidingExpirationTitle => this[nameof (OutputCacheSlidingExpirationTitle)];

    /// <summary>
    /// Indicates whether the expiration time should be reset on every request.
    /// </summary>
    [ResourceEntry("OutputCacheSlidingExpirationDescription", Description = "Describes configuration element.", LastModified = "2011/01/05", Value = "Indicates whether the expiration time should be reset on every request.")]
    public string OutputCacheSlidingExpirationDescription => this[nameof (OutputCacheSlidingExpirationDescription)];

    /// <summary>phrase: Cache profile element</summary>
    [ResourceEntry("CacheProfileElementTitle", Description = "phrase: Cache profile element", LastModified = "2013/01/16", Value = "Cache profile element")]
    public string CacheProfileElementTitle => this[nameof (CacheProfileElementTitle)];

    /// <summary>
    /// phrase: Configures the cache profile that can be used by Sitefinity caches (e.g. users cache).
    /// </summary>
    [ResourceEntry("CacheProfileElementDescription", Description = "phrase: Configures the cache profile that can be used by Sitefinity caches (e.g. users cache).", LastModified = "2013/01/16", Value = "Configures the cache profile that can be used by Sitefinity caches (e.g. users cache).")]
    public string CacheProfileElementDescription => this[nameof (CacheProfileElementDescription)];

    /// <summary>phrase: Wait for page OutputCache to fill</summary>
    [ResourceEntry("WaitForPageOutputCacheToFillTitle", Description = "phrase: Wait for page OutputCache to fill", LastModified = "2014/11/27", Value = "Wait for page OutputCache to fill")]
    public string WaitForPageOutputCacheToFillTitle => this[nameof (WaitForPageOutputCacheToFillTitle)];

    /// <summary>
    /// phrase: Wait for page OutputCache to fill - Description
    /// </summary>
    [ResourceEntry("WaitForPageOutputCacheToFillDescription", Description = "phrase: Indicates whether when requested for the first time, pages are served only once before their output cache is filled. If not enabled, pages can be served and compiled directly from the database more than once before they enter the output cache", LastModified = "2018/09/27", Value = "Indicates whether when requested for the first time, pages are served only once before their output cache is filled. If not enabled, pages can be served and compiled directly from the database more than once before they enter the output cache")]
    public string WaitForPageOutputCacheToFillDescription => this[nameof (WaitForPageOutputCacheToFillDescription)];

    /// <summary>Caption for Output Cache config element.</summary>
    [ResourceEntry("MemcachedSettingsCaption", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Memcached")]
    public string MemcachedSettingsCaption => this[nameof (MemcachedSettingsCaption)];

    /// <summary>Description of Output Cache config element</summary>
    [ResourceEntry("MemcachedSettingsDescription", Description = "Describes configuration element.", LastModified = "2018/06/28", Value = "Defines settings for Memcached.")]
    public string MemcachedSettingsDescription => this[nameof (MemcachedSettingsDescription)];

    /// <summary>Caption for Output Cache config element.</summary>
    [ResourceEntry("SqlServerSettingsCaption", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "SQL Server")]
    public string SqlServerSettingsCaption => this[nameof (SqlServerSettingsCaption)];

    /// <summary>Description of Output Cache config element</summary>
    [ResourceEntry("SqlServerSettingsDescription", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Defines settings for SQL Server.")]
    public string SqlServerSettingsDescription => this[nameof (SqlServerSettingsDescription)];

    /// <summary>Caption for Output Cache config element.</summary>
    [ResourceEntry("AwsDynamoDBSettingsCaption", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Aws Dynamo DB")]
    public string AwsDynamoDBSettingsCaption => this[nameof (AwsDynamoDBSettingsCaption)];

    /// <summary>Description of Output Cache config element</summary>
    [ResourceEntry("AwsDynamoDBSettingsDescription", Description = "Describes configuration element.", LastModified = "2018/06/28", Value = "Defines settings for AwsDynamoDB.")]
    public string AwsDynamoDBSettingsDescription => this[nameof (AwsDynamoDBSettingsDescription)];

    /// <summary>Description of Output Cache config element</summary>
    [ResourceEntry("AwsDynamoDBAccessKeyTitle", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Access key")]
    public string AwsDynamoDBAccessKeyTitle => this[nameof (AwsDynamoDBAccessKeyTitle)];

    /// <summary>Description of Output Cache config element</summary>
    [ResourceEntry("AwsDynamoDBSecretKeyTitle", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Secret key")]
    public string AwsDynamoDBSecretKeyTitle => this[nameof (AwsDynamoDBSecretKeyTitle)];

    /// <summary>
    /// Title of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation
    /// </summary>
    [ResourceEntry("PageCacheInvalidationTitle", Description = "Describes configuration element.", LastModified = "2019/03/31", Value = "Page Output Cache Invalidation Settings")]
    public string PageCacheInvalidationTitle => this[nameof (PageCacheInvalidationTitle)];

    /// <summary>
    /// Description of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation
    /// </summary>
    [ResourceEntry("PageCacheInvalidationDescription", Description = "Describes configuration element.", LastModified = "2019/03/31", Value = "Configure the strategy how the page output cache is invalidating.")]
    public string PageCacheInvalidationDescription => this[nameof (PageCacheInvalidationDescription)];

    /// <summary>
    /// Title of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation &gt; Expiration max delay
    /// </summary>
    [ResourceEntry("CacheExpirationMaxDelayTitle", Description = "Describes configuration element.", LastModified = "2019/03/31", Value = "Expiration max delay")]
    public string CacheExpirationMaxDelayTitle => this[nameof (CacheExpirationMaxDelayTitle)];

    /// <summary>
    /// Description of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation &gt; Expiration max delay
    /// </summary>
    [ResourceEntry("CacheExpirationMaxDelayDescription", Description = "Describes configuration element.", LastModified = "2019/03/31", Value = "This setting defines the maximum amount of time (in seconds) Sitefinity CMS can serve the current cached version of a page in case of cache invalidation.")]
    public string CacheExpirationMaxDelayDescription => this[nameof (CacheExpirationMaxDelayDescription)];

    /// <summary>
    /// Title of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation Settings &gt; Batch size
    /// </summary>
    [ResourceEntry("BatchSizeTitle", Description = "Describes configuration element.", LastModified = "2019/09/19", Value = "Batch size")]
    public string BatchSizeTitle => this[nameof (BatchSizeTitle)];

    /// <summary>
    /// Description of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation Settings &gt; Batch size
    /// </summary>
    [ResourceEntry("BatchSizeDescription", Description = "Describes configuration element.", LastModified = "2019/09/19", Value = "The number of cache items invalidated in a single batch.")]
    public string BatchSizeDescription => this[nameof (BatchSizeDescription)];

    /// <summary>
    /// Title of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation Settings &gt; Batch size
    /// </summary>
    [ResourceEntry("CacheItemHostHeaderNameTitle", Description = "Describes configuration element.", LastModified = "2019/11/25", Value = "Cache item host transportation header name")]
    public string CacheItemHostHeaderNameTitle => this[nameof (CacheItemHostHeaderNameTitle)];

    /// <summary>
    /// Description of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation Settings &gt; Batch size
    /// </summary>
    [ResourceEntry("CacheItemHostHeaderNameDescription", Description = "Describes configuration element.", LastModified = "2019/11/25", Value = "The name of the header that will be used to send the value of the cache item host. For example, there are two multisite websites - abc.com and xyz.com. In this case, a warmup request can be sent to abc.com with host value xyz.com. This setting allows you to send the host value with another header - for example, in a setup where the host header is rewritten by 'X-Original-Host'.")]
    public string CacheItemHostHeaderNameDescription => this[nameof (CacheItemHostHeaderNameDescription)];

    /// <summary>
    /// Title of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation &gt; Warmup
    /// </summary>
    [ResourceEntry("CacheInvalidationWarmupTitle", Description = "Describes configuration element.", LastModified = "2019/03/31", Value = "Warmup")]
    public string CacheInvalidationWarmupTitle => this[nameof (CacheInvalidationWarmupTitle)];

    /// <summary>
    /// Description of the config element: System &gt; Output Cache Settings &gt; Page Cache Invalidation &gt; Warmup
    /// </summary>
    [ResourceEntry("CacheInvalidationWarmupDescription", Description = "Describes configuration element.", LastModified = "2019/03/31", Value = "Warmup the invalidated URLs. During that time the previous Output item is served to the user.")]
    public string CacheInvalidationWarmupDescription => this[nameof (CacheInvalidationWarmupDescription)];

    /// <summary>Duration</summary>
    [ResourceEntry("CacheDurationTitle", Description = "The title of a configuration element.", LastModified = "2013/01/16", Value = "Duration")]
    public string CacheDurationTitle => this[nameof (CacheDurationTitle)];

    /// <summary>
    /// The time duration, in seconds, during which the item will be cached.
    /// </summary>
    [ResourceEntry("CacheDurationDescription", Description = "Describes configuration element.", LastModified = "2013/01/16", Value = "The time duration, in seconds, during which the item will be cached.")]
    public string CacheDurationDescription => this[nameof (CacheDurationDescription)];

    /// <summary>Sliding Expiration</summary>
    [ResourceEntry("CacheSlidingExpirationTitle", Description = "The title of a configuration element.", LastModified = "2013/01/16", Value = "Sliding Expiration")]
    public string CacheSlidingExpirationTitle => this[nameof (CacheSlidingExpirationTitle)];

    /// <summary>
    /// Indicates whether the expiration time should be reset on every request.
    /// </summary>
    [ResourceEntry("CacheSlidingExpirationDescription", Description = "Describes configuration element.", LastModified = "2013/01/16", Value = "Indicates whether the expiration time should be reset on every request.")]
    public string CacheSlidingExpirationDescription => this[nameof (CacheSlidingExpirationDescription)];

    /// <summary>phrase: Users cache</summary>
    [ResourceEntry("UsersCacheConfigTitle", Description = "phrase: Users cache", LastModified = "2013/01/16", Value = "Users cache")]
    public string UsersCacheConfigTitle => this[nameof (UsersCacheConfigTitle)];

    /// <summary>
    /// phrase: Defines the settings for the cache that will store user information.
    /// </summary>
    [ResourceEntry("UsersCacheConfigDescription", Description = "phrase: Defines the settings for the cache that will store user information.", LastModified = "2013/01/16", Value = "Defines the settings for the cache that will store user information.")]
    public string UsersCacheConfigDescription => this[nameof (UsersCacheConfigDescription)];

    /// <summary>phrase: Content links cache</summary>
    [ResourceEntry("ContentLinksCacheConfigTitle", Description = "phrase: Content links cache", LastModified = "2013/01/21", Value = "Content links cache")]
    public string ContentLinksCacheConfigTitle => this[nameof (ContentLinksCacheConfigTitle)];

    /// <summary>
    /// phrase: Defines the settings for the cache that will store content link information. This cache will be used if there is no provider level cache for content links.
    /// </summary>
    [ResourceEntry("ContentLinksCacheConfigDescription", Description = "phrase: Defines the settings for the cache that will store content link information. This cache will be used if there is no provider level cache for content links.", LastModified = "2013/01/22", Value = "Defines the settings for the cache that will store content link information. This cache will be used if there is no provider level cache for content links.")]
    public string ContentLinksCacheConfigDescription => this[nameof (ContentLinksCacheConfigDescription)];

    /// <summary>Field definition settings.</summary>
    [ResourceEntry("FieldDefinitionTitle", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Field definition settings.")]
    public string FieldDefinitionTitle => this[nameof (FieldDefinitionTitle)];

    /// <summary>
    /// Description of field definition configuration settings.
    /// </summary>
    [ResourceEntry("FieldDefinitionDescription", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Description of field definition configuration settings.")]
    public string FieldDefinitionDescription => this[nameof (FieldDefinitionDescription)];

    /// <summary>word: ID</summary>
    [ResourceEntry("FieldControlIDCaption", Description = "word: ID", LastModified = "2010/10/22", Value = "ID")]
    public string FieldControlIDCaption => this[nameof (FieldControlIDCaption)];

    /// <summary>
    /// phrase: The programmatic identifier assigned to the field control.
    /// </summary>
    [ResourceEntry("FieldControlIDDescription", Description = "phrase: The programmatic identifier assigned to the field control.", LastModified = "2010/10/22", Value = "The programmatic identifier assigned to the field control.")]
    public string FieldControlIDDescription => this[nameof (FieldControlIDDescription)];

    /// <summary>Field control definition settings.</summary>
    [ResourceEntry("FieldControlDefinitionTitle", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Field control definition settings.")]
    public string FieldControlDefinitionTitle => this[nameof (FieldControlDefinitionTitle)];

    /// <summary>
    /// Description of field control definition configuration settings.
    /// </summary>
    [ResourceEntry("FieldControlDefinitionDescription", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Description of field control definition configuration settings.")]
    public string FieldControlDefinitionDescription => this[nameof (FieldControlDefinitionDescription)];

    /// <summary>phrase: DataFieldName</summary>
    [ResourceEntry("FieldDefinitionDataFieldNameCaption", Description = "phrase: DataFieldName", LastModified = "2010/02/18", Value = "DataFieldName")]
    public string FieldDefinitionDataFieldNameCaption => this[nameof (FieldDefinitionDataFieldNameCaption)];

    /// <summary>
    /// phrase:Represents a DataFieldName of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("FieldDefinitionDataFieldNameDescription", Description = "phrase:Represents a DataFieldName of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents a DataFieldName of each control inheriting from field control.")]
    public string FieldDefinitionDataFieldNameDescription => this[nameof (FieldDefinitionDataFieldNameDescription)];

    /// <summary>phrase: Value</summary>
    [ResourceEntry("FieldDefinitionValueCaption", Description = "phrase: Value", LastModified = "2010/02/18", Value = "Value")]
    public string FieldDefinitionValueCaption => this[nameof (FieldDefinitionValueCaption)];

    /// <summary>
    /// phrase:Represents a Value of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("FieldDefinitionValueDescription", Description = "phrase:Represents a Value of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents a Value of each control inheriting from field control.")]
    public string FieldDefinitionValueDescription => this[nameof (FieldDefinitionValueDescription)];

    /// <summary>phrase: DisplayMode</summary>
    [ResourceEntry("FieldDefinitionDisplayModeCaption", Description = "phrase: DisplayMode", LastModified = "2010/02/18", Value = "DisplayMode")]
    public string FieldDefinitionDisplayModeCaption => this[nameof (FieldDefinitionDisplayModeCaption)];

    /// <summary>
    /// phrase:Represents a DisplayMode of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("FieldDefinitionDisplayModeDescription", Description = "phrase:Represents a DisplayMode of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents a DisplayMode of each control inheriting from field control.")]
    public string FieldDefinitionDisplayModeDescription => this[nameof (FieldDefinitionDisplayModeDescription)];

    /// <summary>phrase: Validation</summary>
    [ResourceEntry("FieldDefinitionValidationCaption", Description = "phrase: Validation", LastModified = "2010/02/18", Value = "Validation")]
    public string FieldDefinitionValidationCaption => this[nameof (FieldDefinitionValidationCaption)];

    /// <summary>
    /// phrase:Represents a validation of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("FieldDefinitionValidationDescription", Description = "phrase:Represents a Validation of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents a validation of each control inheriting from field control.")]
    public string FieldDefinitionValidationDescription => this[nameof (FieldDefinitionValidationDescription)];

    /// <summary>phrase: Is to override Rad Editor dialogs</summary>
    [ResourceEntry("IsToOverrideDialogsCaption", Description = "phrase: Is to override Rad Editor dialogs", LastModified = "2010/08/05", Value = "Is to override Rad Editor dialogs")]
    public string IsToOverrideDialogsCaption => this[nameof (IsToOverrideDialogsCaption)];

    /// <summary>
    /// phrase:Value indicating whether to override the RadEditor dialogs.
    /// </summary>
    [ResourceEntry("IsToOverrideDialogsDescription", Description = "phrase:Value indicating whether to override the RadEditor dialogs.", LastModified = "2010/08/05", Value = "Value indicating whether to override the RadEditor dialogs.")]
    public string IsToOverrideDialogsDescription => this[nameof (IsToOverrideDialogsDescription)];

    /// <summary>phrase: Bind on server</summary>
    [ResourceEntry("BindOnServerCaption", Description = "phrase: Bind on server", LastModified = "2010/10/22", Value = "Bind on server")]
    public string BindOnServerCaption => this[nameof (BindOnServerCaption)];

    /// <summary>
    /// phrase: A value indicating whether to bind the field on the server.
    /// </summary>
    [ResourceEntry("BindOnServerDescription", Description = "phrase: A value indicating whether to bind the field on the server.", LastModified = "2010/10/22", Value = "A value indicating whether to bind the field on the server.")]
    public string BindOnServerDescription => this[nameof (BindOnServerDescription)];

    /// <summary>phrase: Allow creating new taxa</summary>
    [ResourceEntry("AllowCreatingCaption", Description = "phrase: Allow creating new taxa", LastModified = "2010/12/13", Value = "Allow creating new taxa")]
    public string AllowCreatingCaption => this[nameof (AllowCreatingCaption)];

    /// <summary>
    /// phrase: Setting this property to true will allow you to create new taxa when selecting
    /// </summary>
    [ResourceEntry("AllowCreatingDescription", Description = "phrase: Setting this property to true will allow you to create new taxa when selecting.", LastModified = "2011/12/13", Value = "Setting this property to true will allow you to create new taxa when selecting.")]
    public string AllowCreatingDescription => this[nameof (AllowCreatingDescription)];

    /// <summary>phrase: Validation</summary>
    [ResourceEntry("WrapperTagCaption", Description = "phrase: Wrapper Tag", LastModified = "2010/03/02", Value = "Wrapper tag")]
    public string WrapperTagCaption => this[nameof (WrapperTagCaption)];

    /// <summary>phrase: The tag that will be rendered as a wrapper.</summary>
    [ResourceEntry("WrapperTagDescription", Description = "phrase:The tag that will be rendered as a wrapper.", LastModified = "2010/03/02", Value = "The tag that will be rendered as a wrapper.")]
    public string WrapperTagDescription => this[nameof (WrapperTagDescription)];

    /// <summary>phrase: Is hidden in translation mode.</summary>
    [ResourceEntry("IsHiddenInTranslationModeCaption", Description = "phrase: Is hidden in translation mode", LastModified = "2011/10/19", Value = "Is hidden in translation mode")]
    public string IsHiddenInTranslationModeCaption => this[nameof (IsHiddenInTranslationModeCaption)];

    /// <summary>
    /// phrase: Hides the control when its in read-only mode when displaying a translation.
    /// </summary>
    [ResourceEntry("IsHiddenInTranslationModeDescription", Description = "phrase: Hides the control when its in read-only mode when displaying a translation.", LastModified = "2011/10/19", Value = "Hides the control when its in read-only mode when displaying a translation.")]
    public string IsHiddenInTranslationModeDescription => this[nameof (IsHiddenInTranslationModeDescription)];

    /// <summary>phrase: Control ID</summary>
    [ResourceEntry("ControlIdCaption", Description = "phrase: Control ID", LastModified = "2011/05/20", Value = "Control ID")]
    public string ControlIdCaption => this[nameof (ControlIdCaption)];

    /// <summary>
    /// phrase: The value for the ID property of the control that will be constructed based on this definition.
    /// </summary>
    [ResourceEntry("ControlIdDescription", Description = "phrase:The value for the ID property of the control that will be constructed based on this definition.", LastModified = "2011/05/20", Value = "The value for the ID property of the control that will be constructed based on this definition.")]
    public string ControlIdDescription => this[nameof (ControlIdDescription)];

    /// <summary>phrase: FieldType</summary>
    [ResourceEntry("FieldDefinitionFieldTypeCaption", Description = "phrase: FieldType", LastModified = "2010/02/18", Value = "FieldType")]
    public string FieldDefinitionFieldTypeCaption => this[nameof (FieldDefinitionFieldTypeCaption)];

    /// <summary>
    /// phrase: Represents a FieldType of each control implementing IField interface.
    /// </summary>
    [ResourceEntry("FieldDefinitionFieldTypeDescription", Description = "phrase : Represents a FieldType of each control implementing IField interface.", LastModified = "2010/02/18", Value = "Represents a FieldType of each control implementing IField interface.")]
    public string FieldDefinitionFieldTypeDescription => this[nameof (FieldDefinitionFieldTypeDescription)];

    /// <summary>phrase: FieldVirtualPath</summary>
    [ResourceEntry("FieldDefinitionFieldVirtualPathCaption", Description = "phrase: FieldVirtualPath", LastModified = "2010/02/18", Value = "FieldVirtualPath")]
    public string FieldDefinitionFieldVirtualPathCaption => this[nameof (FieldDefinitionFieldVirtualPathCaption)];

    /// <summary>
    /// phrase : Represents a virtual path of each control implementing IField interface.
    /// </summary>
    [ResourceEntry("FieldDefinitionFieldVirtualPathDescription", Description = "phrase: Represents a virtual path of each control implementing IField interface.", LastModified = "2010/02/18", Value = "Represents a virtual path of each control implementing IField interface.")]
    public string FieldDefinitionFieldVirtualPathDescription => this[nameof (FieldDefinitionFieldVirtualPathDescription)];

    /// <summary>phrase: FieldDefinitionDescriptionCaption</summary>
    [ResourceEntry("FieldDefinitionDescriptionCaption", Description = "phrase: FieldDefinitionDescriptionCaption", LastModified = "2010/02/18", Value = "FieldDefinitionDescriptionCaption")]
    public string FieldDefinitionDescriptionCaption => this[nameof (FieldDefinitionDescriptionCaption)];

    /// <summary>
    /// phrase:Represents a description of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("FieldDefinitionDescriptionDescription", Description = "phrase:Represents a description of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents a description of each control inheriting from field control.")]
    public string FieldDefinitionDescriptionDescription => this[nameof (FieldDefinitionDescriptionDescription)];

    /// <summary>phrase: FieldDefinitionExampleCaption</summary>
    [ResourceEntry("FieldDefinitionExampleCaption", Description = "phrase: FieldDefinitionExampleCaption", LastModified = "2010/02/18", Value = "FieldDefinitionExampleCaption")]
    public string FieldDefinitionExampleCaption => this[nameof (FieldDefinitionExampleCaption)];

    /// <summary>
    /// phrase:Represents an example text of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("FieldDefinitionExampleDescription", Description = "phrase:Represents an example text of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents an example text of each control inheriting from field control.")]
    public string FieldDefinitionExampleDescription => this[nameof (FieldDefinitionExampleDescription)];

    /// <summary>phrase: Field name</summary>
    [ResourceEntry("FieldDefinitionNameCaption", Description = "phrase: Field name", LastModified = "2010/02/18", Value = "Field name")]
    public string FieldDefinitionNameCaption => this[nameof (FieldDefinitionNameCaption)];

    /// <summary>
    /// phrase: Name of the field which is used to identify the field programmatically.
    /// </summary>
    [ResourceEntry("FieldDefinitionNameDescription", Description = "phrase: Name of the field which is used to identify the field programmatically.", LastModified = "2011/01/05", Value = "Name of the field which is used to identify the field programmatically.")]
    public string FieldDefinitionNameDescription => this[nameof (FieldDefinitionNameDescription)];

    /// <summary>phrase: FieldDefinitionTitleCaption</summary>
    [ResourceEntry("FieldDefinitionTitleCaption", Description = "phrase: FieldDefinitionTitleCaption", LastModified = "2010/02/18", Value = "FieldDefinitionTitleCaption")]
    public string FieldDefinitionTitleCaption => this[nameof (FieldDefinitionTitleCaption)];

    /// <summary>
    /// phrase:Represents a title text of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("FieldDefinitionTitleDescription", Description = "phrase:Represents a title text of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents a title text of each control inheriting from field control.")]
    public string FieldDefinitionTitleDescription => this[nameof (FieldDefinitionTitleDescription)];

    /// <summary>word: Title</summary>
    [ResourceEntry("CompositeFieldDefinitionTitleCaption", Description = "word: Title", LastModified = "2010/10/22", Value = "Title")]
    public string CompositeFieldDefinitionTitleCaption => this[nameof (CompositeFieldDefinitionTitleCaption)];

    /// <summary>phrase: The title of the field element.</summary>
    [ResourceEntry("CompositeFieldDefinitionTitleDescription", Description = "phrase: The title of the field element.", LastModified = "2010/10/22", Value = "The title of the field element.")]
    public string CompositeFieldDefinitionTitleDescription => this[nameof (CompositeFieldDefinitionTitleDescription)];

    /// <summary>word: Description</summary>
    [ResourceEntry("CompositeFieldDefinitionDescriptionCaption", Description = "word: Description", LastModified = "2010/10/22", Value = "Description")]
    public string CompositeFieldDefinitionDescriptionCaption => this[nameof (CompositeFieldDefinitionDescriptionCaption)];

    /// <summary>phrase: The description of the field element.</summary>
    [ResourceEntry("CompositeFieldDefinitionDescriptionDescription", Description = "phrase: The description of the field element.", LastModified = "2010/10/22", Value = "The description of the field element.")]
    public string CompositeFieldDefinitionDescriptionDescription => this[nameof (CompositeFieldDefinitionDescriptionDescription)];

    /// <summary>word: Example</summary>
    [ResourceEntry("CompositeFieldDefinitionExampleCaption", Description = "word: Example", LastModified = "2010/10/22", Value = "Example")]
    public string CompositeFieldDefinitionExampleCaption => this[nameof (CompositeFieldDefinitionExampleCaption)];

    /// <summary>phrase: The example of the field element.</summary>
    [ResourceEntry("CompositeFieldDefinitionExampleDescription", Description = "phrase: The example of the field element.", LastModified = "2010/10/22", Value = "The example of the field element.")]
    public string CompositeFieldDefinitionExampleDescription => this[nameof (CompositeFieldDefinitionExampleDescription)];

    /// <summary>
    /// The title of the configuration property 'AllowMultipleSelection'.
    /// </summary>
    /// <value>The allow multiple selection caption.</value>
    [ResourceEntry("AllowMultipleSelectionCaption", Description = "The title of the configuration property 'AllowMultipleSelection'.", LastModified = "2010/02/18", Value = "Allow multiple selection")]
    public string AllowMultipleSelectionCaption => this[nameof (AllowMultipleSelectionCaption)];

    /// <summary>
    /// Describes the configuration property 'AllowMultipleSelection'.
    /// </summary>
    /// <value>The allow multiple selection description.</value>
    [ResourceEntry("AllowMultipleSelectionDescription", Description = "Describes the configuration property 'AllowMultipleSelection'", LastModified = "2010/02/18", Value = "Determines whether the field control allows multiple selection.")]
    public string AllowMultipleSelectionDescription => this[nameof (AllowMultipleSelectionDescription)];

    /// <summary>The title of the configuration property 'TaxonomyId'.</summary>
    /// <value>The taxonomy pageId caption.</value>
    [ResourceEntry("TaxonomyIdCaption", Description = "The title of the configuration property 'TaxonomyId'.", LastModified = "2010/02/18", Value = "Taxonomy ID")]
    public string TaxonomyIdCaption => this[nameof (TaxonomyIdCaption)];

    /// <summary>Describes the configuration property 'TaxonomyId'.</summary>
    /// <value>The taxonomy pageId description.</value>
    [ResourceEntry("TaxonomyIdDescription", Description = "Describes the configuration property 'TaxonomyId'.", LastModified = "2010/02/18", Value = "The GUID that uniquely specify the taxonomy.")]
    public string TaxonomyIdDescription => this[nameof (TaxonomyIdDescription)];

    /// <summary>
    /// The title of the configuration property 'TaxonomyProvider'.
    /// </summary>
    /// <value>The taxonomy provider caption.</value>
    [ResourceEntry("TaxonomyProviderCaption", Description = "The title of the configuration property 'TaxonomyProvider'.", LastModified = "2010/02/18", Value = "The taxonomy provider")]
    public string TaxonomyProviderCaption => this[nameof (TaxonomyProviderCaption)];

    /// <summary>
    /// Describes the configuration property 'TaxonomyProvider'.
    /// </summary>
    /// <value>The taxonomy provider description.</value>
    [ResourceEntry("TaxonomyProviderDescription", Description = "Describes the configuration property 'TaxonomyProvider'.", LastModified = "2010/02/18", Value = "The Taxonomy provider.")]
    public string TaxonomyProviderDescription => this[nameof (TaxonomyProviderDescription)];

    /// <summary>
    /// The title of the configuration property 'BaseServiceUrl'.
    /// </summary>
    [ResourceEntry("WebServiceUrlCaption", Description = "The title of the configuration property 'WebServiceUrl'.", LastModified = "2010/02/18", Value = "Web Service URL")]
    public string WebServiceUrlCaption => this[nameof (WebServiceUrlCaption)];

    /// <summary>
    /// Describes the configuration property 'BaseServiceUrl'.
    /// </summary>
    [ResourceEntry("WebServiceUrlDescription", Description = "Describes the configuration property 'WebServiceUrl'.", LastModified = "2010/02/18", Value = "The URL of the WCF service used to bind the taxonomy selector.")]
    public string WebServiceUrlDescription => this[nameof (WebServiceUrlDescription)];

    /// <summary>The title of the configuration property 'Rows'.</summary>
    [ResourceEntry("RowsCaption", Description = "The title of the configuration property 'Rows'.", LastModified = "2010/02/22", Value = "Rows number")]
    public string RowsCaption => this[nameof (RowsCaption)];

    /// <summary>Describes the configuration property 'Rows'.</summary>
    [ResourceEntry("RowsDescription", Description = "Describes the configuration property 'Rows'.", LastModified = "2010/02/22", Value = "The number of rows displayed in a multiline text box.")]
    public string RowsDescription => this[nameof (RowsDescription)];

    /// <summary>
    /// The title of the configuration property 'IsPasswordMode'.
    /// </summary>
    [ResourceEntry("IsPasswordModeCaption", Description = "The title of the configuration property 'IsPasswordMode'.", LastModified = "2011/02/18", Value = "Is password mode")]
    public string IsPasswordModeCaption => this[nameof (IsPasswordModeCaption)];

    /// <summary>
    /// Describes the configuration property 'IsPasswordMode'.
    /// </summary>
    [ResourceEntry("IsPasswordModeDescription", Description = "Describes the configuration property 'IsPasswordMode'.", LastModified = "2011/02/18", Value = "A value indicating if the text field is used as password field.")]
    public string IsPasswordModeDescription => this[nameof (IsPasswordModeDescription)];

    /// <summary>
    /// The title of the configuration property 'HideIfValue'.
    /// </summary>
    [ResourceEntry("HideIfValueCaption", Description = "The title of the configuration property 'HideIfValue'.", LastModified = "2010/02/22", Value = "Hide if value")]
    public string HideIfValueCaption => this[nameof (HideIfValueCaption)];

    /// <summary>Describes the configuration property 'HideIfValue'.</summary>
    [ResourceEntry("HideIfValueDescription", Description = "Describes the configuration property 'HideIfValue'.", LastModified = "2010/02/22", Value = "The value which compared with the actual value of the Text Field, if equal hides the text.")]
    public string HideIfValueDescription => this[nameof (HideIfValueDescription)];

    /// <summary>
    /// The title of the configuration property 'ExpandableControlElement'.
    /// </summary>
    [ResourceEntry("ExpandableControlElementCaption", Description = "The title of the configuration property 'ExpandableControlElement'.", LastModified = "2010/02/22", Value = "Expandable Control Element")]
    public string ExpandableControlElementCaption => this[nameof (ExpandableControlElementCaption)];

    /// <summary>
    /// Describes the configuration property 'ExpandableControlElement'.
    /// </summary>
    [ResourceEntry("ExpandableControlElementDescription", Description = "Describes the configuration property 'ExpandableControlElement'.", LastModified = "2010/02/22", Value = "The object that defines the expandable behavior of the text field.")]
    public string ExpandableControlElementDescription => this[nameof (ExpandableControlElementDescription)];

    /// <summary>
    /// The title of the configuration property 'AllowRootSelection'.
    /// </summary>
    [ResourceEntry("AllowRootSelectionCaption", Description = "The title of the configuration property 'AllowRootSelection'.", LastModified = "2010/06/10", Value = "Allow root selection")]
    public string AllowRootSelectionCaption => this[nameof (AllowRootSelectionCaption)];

    /// <summary>
    /// Describes the configuration property 'AllowRootSelection'.
    /// </summary>
    [ResourceEntry("AllowRootSelectionDescription", Description = "Describes the configuration property 'AllowRootSelection'.", LastModified = "2010/10/06", Value = "A value indicating whether to allow root selection.")]
    public string AllowRootSelectionDescription => this[nameof (AllowRootSelectionDescription)];

    /// <summary>
    /// The title of the configuration property 'ShowDoneSelectingButton'.
    /// </summary>
    [ResourceEntry("ShowDoneSelectingButtonCaption", Description = "The title of the configuration property 'ShowDoneSelectingButton'.", LastModified = "2010/06/10", Value = "Show done selecting button")]
    public string ShowDoneSelectingButtonCaption => this[nameof (ShowDoneSelectingButtonCaption)];

    /// <summary>
    /// Describes the configuration property 'ShowDoneSelectingButton'.
    /// </summary>
    [ResourceEntry("ShowDoneSelectingButtonDescription", Description = "Describes the configuration property 'ShowDoneSelectingButton'.", LastModified = "2010/02/22", Value = "A value indicating whether to show the done-selecting button.")]
    public string ShowDoneSelectingButtonDescription => this[nameof (ShowDoneSelectingButtonDescription)];

    /// <summary>
    /// The title of the configuration property 'ShowCreateNewTaxonButton'.
    /// </summary>
    [ResourceEntry("ShowCreateNewTaxonButtonCaption", Description = "The title of the configuration property 'ShowCreateNewTaxonButton'.", LastModified = "2010/06/10", Value = "Show create new taxon button")]
    public string ShowCreateNewTaxonButtonCaption => this[nameof (ShowCreateNewTaxonButtonCaption)];

    /// <summary>
    /// Describes the configuration property 'ShowCreateNewTaxonButton'.
    /// </summary>
    [ResourceEntry("ShowCreateNewTaxonButtonDescription", Description = "Describes the configuration property 'ShowCreateNewTaxonButton'.", LastModified = "2010/06/10", Value = "A value indicating whether to show the create-new-taxon button.")]
    public string ShowCreateNewTaxonButtonDescription => this[nameof (ShowCreateNewTaxonButtonDescription)];

    /// <summary>
    /// The title of the configuration property 'RootTaxonID'.
    /// </summary>
    [ResourceEntry("RootTaxonIDCaption", Description = "The title of the configuration property 'RootTaxonID'.", LastModified = "2010/06/14", Value = "Root taxon ID")]
    public string RootTaxonIDCaption => this[nameof (RootTaxonIDCaption)];

    /// <summary>
    /// Describes the configuration property 'ShowCreateNewTaxonButton'.
    /// </summary>
    [ResourceEntry("RootTaxonIDDescription", Description = "Describes the configuration property 'RootTaxonID'.", LastModified = "2010/06/14", Value = "The ID of the taxon from which the binding should start.")]
    public string RootTaxonIDDescription => this[nameof (RootTaxonIDDescription)];

    /// <summary>Describes the configuration property 'IsLocalizable'</summary>
    /// <value>Is localizable</value>
    [ResourceEntry("IsLocalizableCaption", Description = "Describes the configuration property 'IsLocalizable'", LastModified = "2012/11/19", Value = "Is localizable")]
    public string IsLocalizableCaption => this[nameof (IsLocalizableCaption)];

    /// <summary>Describes the configuration property 'IsLocalizable'</summary>
    /// <value>A value indicating whether the field is localizable</value>
    [ResourceEntry("IsLocalizableDescription", Description = "Describes the configuration property 'IsLocalizable'", LastModified = "2012/11/19", Value = "A value indicating whether the field is localizable")]
    public string IsLocalizableDescription => this[nameof (IsLocalizableDescription)];

    /// <summary>Describes ShowCharacterCount property.</summary>
    [ResourceEntry("ShowCharacterCounterCaption", Description = "Describes ShowCharacterCount property.", LastModified = "2013/06/10", Value = "Show Character Counter")]
    public string ShowCharacterCounterCaption => this[nameof (ShowCharacterCounterCaption)];

    /// <summary>Describes ShowCharacterCount property.</summary>
    [ResourceEntry("ShowCharacterCounterDescription", Description = "Describes ShowCharacterCount property.", LastModified = "2013/06/10", Value = "Show Character Counter")]
    public string ShowCharacterCounterDescription => this[nameof (ShowCharacterCounterDescription)];

    /// <summary>RecommendedCharactersCount property.</summary>
    [ResourceEntry("RecommendedCharactersCountCaption", Description = "RecommendedCharactersCount property.", LastModified = "2013/06/10", Value = "Recommended Characters Count")]
    public string RecommendedCharactersCountCaption => this[nameof (RecommendedCharactersCountCaption)];

    /// <summary>Describes RecommendedCharactersCount property.</summary>
    [ResourceEntry("RecommendedCharactersCountDescription", Description = "Describes RecommendedCharactersCount property.", LastModified = "2013/06/10", Value = "Recommended Characters Count")]
    public string RecommendedCharactersCountDescription => this[nameof (RecommendedCharactersCountDescription)];

    /// <summary>CharacterCounterDescriptionCaption property.</summary>
    [ResourceEntry("CharacterCounterDescriptionCaption", Description = "CharacterCounterDescriptionCaption property.", LastModified = "2013/07/11", Value = "Character Counter Description")]
    public string CharacterCounterDescriptionCaption => this[nameof (CharacterCounterDescriptionCaption)];

    /// <summary>
    /// Describes CharacterCounterDescriptionDescription property.
    /// </summary>
    [ResourceEntry("CharacterCounterDescriptionDescription", Description = "Describes CharacterCounterDescriptionDescription property.", LastModified = "2013/06/11", Value = "Describes CharacterCounterDescription property.")]
    public string CharacterCounterDescriptionDescription => this[nameof (CharacterCounterDescriptionDescription)];

    /// <summary>
    /// The title of the configuration property TrimSpacesCaption property.
    /// </summary>
    [ResourceEntry("TrimSpacesCaption", Description = "The title of the configuration property TrimSpacesCaption property.", LastModified = "2013/07/17", Value = "Trim Spaces")]
    public string TrimSpacesCaption => this[nameof (TrimSpacesCaption)];

    /// <summary>Describes TrimSpacesDescription property.</summary>
    [ResourceEntry("TrimSpacesDescription", Description = "Describes TrimSpacesDescription property.", LastModified = "2013/06/17", Value = "Whether to trim spaces before and after the text.")]
    public string TrimSpacesDescription => this[nameof (TrimSpacesDescription)];

    /// <summary>Describes MaxCharactersCount property.</summary>
    /// <value>Maximum Characters Count</value>
    [ResourceEntry("MaxCharactersCountCaption", Description = "Describes MaxCharactersCount property.", LastModified = "2013/06/24", Value = "Maximum Characters Count")]
    public string MaxCharactersCountCaption => this[nameof (MaxCharactersCountCaption)];

    /// <summary>Describes MaxCharactersCountDescription property.</summary>
    /// <value>Describes MaxCharactersCount property.</value>
    [ResourceEntry("MaxCharactersCountDescription", Description = "Describes MaxCharactersCountDescription property.", LastModified = "2013/06/24", Value = "Describes MaxCharactersCount property.")]
    public string MaxCharactersCountDescription => this[nameof (MaxCharactersCountDescription)];

    /// <summary>RegularExpressionFilter</summary>
    [ResourceEntry("RegularExpressionFilterTitle", Description = "The title of the configuration element.", LastModified = "2010/02/18", Value = "RegularExpressionFilter")]
    public string RegularExpressionFilterTitle => this[nameof (RegularExpressionFilterTitle)];

    /// <summary>
    /// Represents RegularExpressionFilter property of each mirror text field control.
    /// </summary>
    /// <value>Defines what values will be replaced</value>
    [ResourceEntry("RegularExpressionFilterDescription", Description = "Describes configuration element.", LastModified = "2013/11/25", Value = "Defines what values will be replaced")]
    public string RegularExpressionFilterDescription => this[nameof (RegularExpressionFilterDescription)];

    /// <summary>The title of the ReplaceWith configuration element.</summary>
    [ResourceEntry("ReplaceWithTitle", Description = "The title of the configuration element.", LastModified = "2010/03/10", Value = "Replace with")]
    public string ReplaceWithTitle => this[nameof (ReplaceWithTitle)];

    /// <summary>Describes the ReplaceWith configuration element.</summary>
    /// <value>Defines the value that will be used when replacing the string with the regex.</value>
    [ResourceEntry("ReplaceWithDescription", Description = "Describes configuration element.", LastModified = "2013/11/25", Value = "Defines the value that will be used when replacing the string with the regex.")]
    public string ReplaceWithDescription => this[nameof (ReplaceWithDescription)];

    /// <summary>
    /// The title of the MirroredControlId configuration element.
    /// </summary>
    [ResourceEntry("MirroredControlIdTitle", Description = "The title of the MirroredControlId configuration element.", LastModified = "2010/03/10", Value = "Mirrored control id")]
    public string MirroredControlIdTitle => this[nameof (MirroredControlIdTitle)];

    /// <summary>
    /// Describes the MirroredControlId configuration element.
    /// </summary>
    [ResourceEntry("MirroredControlIdDescription", Description = "Describes the MirroredControlId configuration element.", LastModified = "2010/03/10", Value = "Represents the id of the mirrored control id.")]
    public string MirroredControlIdDescription => this[nameof (MirroredControlIdDescription)];

    /// <summary>
    /// The title of the EnableChangeButton configuration element.
    /// </summary>
    [ResourceEntry("EnableChangeButtonTitle", Description = "The title of the EnableChangeButton configuration element.", LastModified = "2010/03/10", Value = "Enable change button")]
    public string EnableChangeButtonTitle => this[nameof (EnableChangeButtonTitle)];

    /// <summary>
    /// Describes the EnableChangeButton configuration element.
    /// </summary>
    [ResourceEntry("EnableChangeButtonDescription", Description = "Describes the EnableChangeButton configuration element.", LastModified = "2010/03/10", Value = "Specifies whether to show a button that must be clicked in order to change the value of the control.")]
    public string EnableChangeButtonDescription => this[nameof (EnableChangeButtonDescription)];

    /// <summary>
    /// The title of the ChangeButtonText configuration element.
    /// </summary>
    [ResourceEntry("ChangeButtonTextTitle", Description = "The title of the ChangeButtonText configuration element.", LastModified = "2010/03/10", Value = "Change button text")]
    public string ChangeButtonTextTitle => this[nameof (ChangeButtonTextTitle)];

    /// <summary>Describes the ChangeButtonText configuration element.</summary>
    [ResourceEntry("ChangeButtonTextDescription", Description = "Describes the ChangeButtonText configuration element.", LastModified = "2010/03/10", Value = "Represents the text of the change button.")]
    public string ChangeButtonTextDescription => this[nameof (ChangeButtonTextDescription)];

    /// <summary>The title of the ToLower configuration element.</summary>
    [ResourceEntry("ToLowerTitle", Description = "The title of the ToLower configuration element.", LastModified = "2010/03/10", Value = "ToLower")]
    public string ToLowerTitle => this[nameof (ToLowerTitle)];

    /// <summary>Describes the ToLower configuration element.</summary>
    /// <value>Sets that all capital letter symbols should be replaced with lower letters</value>
    [ResourceEntry("ToLowerDescription", Description = "Describes the ToLower configuration element.", LastModified = "2013/11/25", Value = "Sets that all capital letter symbols should be replaced with lower letters")]
    public string ToLowerDescription => this[nameof (ToLowerDescription)];

    /// <summary>The title of the Trim configuration element.</summary>
    [ResourceEntry("TrimTitle", Description = "The title of the Trim configuration element.", LastModified = "2010/03/10", Value = "Trim")]
    public string TrimTitle => this[nameof (TrimTitle)];

    /// <summary>
    /// Gets or sets a value indicating whether to lower the value of the control.
    /// </summary>
    /// <value>Specifies whether to trim the whitespaces of the value.</value>
    [ResourceEntry("TrimDescription", Description = "Describes the Trim configuration element.", LastModified = "2013/11/25", Value = "Specifies whether to trim the whitespaces of the value.")]
    public string TrimDescription => this[nameof (TrimDescription)];

    /// <summary>prahse: Prefix text</summary>
    [ResourceEntry("PrefixTextTitle", Description = "prahse: Prefix text", LastModified = "2011/01/10", Value = "Prefix text")]
    public string PrefixTextTitle => this[nameof (PrefixTextTitle)];

    /// <summary>Describes the PrefixText configuration element.</summary>
    [ResourceEntry("PrefixTextDescription", Description = "Describes the PrefixText configuration element.", LastModified = "2011/01/10", Value = "Specifies the prefix that will be appended to the mirrored value.")]
    public string PrefixTextDescription => this[nameof (PrefixTextDescription)];

    /// <summary>Url control id</summary>
    [ResourceEntry("UrlControlIdTitle", Description = ".", LastModified = "2010/07/14", Value = "Url control id")]
    public string UrlControlIdTitle => this[nameof (UrlControlIdTitle)];

    /// <summary>Represents the UrlControlId configuration element.</summary>
    [ResourceEntry("UrlControlIdDescription", Description = "Describes configuration element.", LastModified = "2010/07/14", Value = "Represents the UrlControlId configuration element.")]
    public string UrlControlIdDescription => this[nameof (UrlControlIdDescription)];

    /// <summary>Collapsible Field definition controls settings.</summary>
    [ResourceEntry("CollapsibleFieldTitle", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Collapsible Field definition controls settings.")]
    public string CollapsibleFieldTitle => this[nameof (CollapsibleFieldTitle)];

    /// <summary>
    /// Description of collapsible field definition controls configuration settings.
    /// </summary>
    [ResourceEntry("CollapsibleFieldDescription", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Description of collapsible field definition controls configuration settings.")]
    public string CollapsibleFieldDescription => this[nameof (CollapsibleFieldDescription)];

    /// <summary>phrase: AutoExpandIfValueSet</summary>
    [ResourceEntry("CollapsibleFieldAutoExpandIfValueSetCaption", Description = "phrase: AutoExpandIfValueSet", LastModified = "2010/02/18", Value = "AutoExpandIfValueSet")]
    public string CollapsibleFieldAutoExpandIfValueSetCaption => this[nameof (CollapsibleFieldAutoExpandIfValueSetCaption)];

    /// <summary>
    /// phrase:Represents AutoExpand value of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("CollapsibleFieldAutoExpandIfValueSetDescription", Description = "phrase:Represents AutoExpand value of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents AutoExpand value of each control inheriting from field control.")]
    public string CollapsibleFieldAutoExpandIfValueSetDescription => this[nameof (CollapsibleFieldAutoExpandIfValueSetDescription)];

    /// <summary>phrase: CollapseText</summary>
    [ResourceEntry("CollapsibleFieldCollapseTextCaption", Description = "phrase: CollapseText", LastModified = "2010/02/18", Value = "CollapseText")]
    public string CollapsibleFieldCollapseTextCaption => this[nameof (CollapsibleFieldCollapseTextCaption)];

    /// <summary>
    /// phrase:Represents collapse text value of each control inheriting from field control.
    /// </summary>
    [ResourceEntry("CollapsibleFieldCollapseTextDescription", Description = "phrase:Represents collapse text value of each control inheriting from field control.", LastModified = "2010/02/18", Value = "Represents collapse text value of each control inheriting from field control.")]
    public string CollapsibleFieldCollapseTextDescription => this[nameof (CollapsibleFieldCollapseTextDescription)];

    /// <summary>phrase: Expanded</summary>
    [ResourceEntry("CollapsibleFieldExpandedCaption", Description = "phrase: Expanded", LastModified = "2010/02/18", Value = "Expanded")]
    public string CollapsibleFieldExpandedCaption => this[nameof (CollapsibleFieldExpandedCaption)];

    /// <summary>
    /// phrase:Represents expanded indication for a field control.
    /// </summary>
    [ResourceEntry("CollapsibleFieldExpandedDescription", Description = "phrase:Represents expanded indication for a field control.", LastModified = "2010/02/18", Value = "Represents expanded indication for a field control.")]
    public string CollapsibleFieldExpandedDescription => this[nameof (CollapsibleFieldExpandedDescription)];

    /// <summary>phrase: Expanded</summary>
    [ResourceEntry("CollapsibleFieldExpandTextCaption", Description = "phrase: ExpandText", LastModified = "2010/02/18", Value = "ExpandText")]
    public string CollapsibleFieldExpandTextCaption => this[nameof (CollapsibleFieldExpandTextCaption)];

    /// <summary>
    /// phrase:Represents expand text for a field control control.
    /// </summary>
    [ResourceEntry("CollapsibleFieldExpandTextDescription", Description = "phrase:Represents expand text for a field control control.", LastModified = "2010/02/18", Value = "Represents expand text for a field control.")]
    public string CollapsibleFieldExpandTextDescription => this[nameof (CollapsibleFieldExpandTextDescription)];

    /// <summary>Text</summary>
    [ResourceEntry("TextTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "Text")]
    public string TextTitle => this[nameof (TextTitle)];

    /// <summary>Represents Text property of each choice item.</summary>
    [ResourceEntry("TextDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents Text property of each choice item.")]
    public string TextDescription => this[nameof (TextDescription)];

    /// <summary>Value</summary>
    [ResourceEntry("ValueTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "Value")]
    public string ValueTitle => this[nameof (ValueTitle)];

    /// <summary>Represents Value property of each choice item.</summary>
    [ResourceEntry("ValueDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents Value property of each choice item.")]
    public string ValueDescription => this[nameof (ValueDescription)];

    /// <summary>Description</summary>
    [ResourceEntry("DescriptionTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "Description")]
    public string DescriptionTitle => this[nameof (DescriptionTitle)];

    /// <summary>Represents Description property of each choice item.</summary>
    [ResourceEntry("DescriptionDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents Description property of each choice item.")]
    public string DescriptionDescription => this[nameof (DescriptionDescription)];

    /// <summary>Enabled</summary>
    [ResourceEntry("EnabledTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "Enabled")]
    public string EnabledTitle => this[nameof (EnabledTitle)];

    /// <summary>Represents Enabled property of each choice item.</summary>
    [ResourceEntry("EnabledDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents Enabled property of each choice item.")]
    public string EnabledDescription => this[nameof (EnabledDescription)];

    /// <summary>Selected</summary>
    [ResourceEntry("SelectedTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "Selected")]
    public string SelectedTitle => this[nameof (SelectedTitle)];

    /// <summary>Represents Selected property of each choice item.</summary>
    [ResourceEntry("SelectedDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents Selected property of each choice item.")]
    public string SelectedDescription => this[nameof (SelectedDescription)];

    /// <summary>ChoicesConfig</summary>
    [ResourceEntry("ChoicesConfigTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "ChoicesConfig")]
    public string ChoicesConfigTitle => this[nameof (ChoicesConfigTitle)];

    /// <summary>Represents ChoicesConfig property.</summary>
    [ResourceEntry("ChoicesConfigDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents ChoicesConfig property.")]
    public string ChoicesConfigDescription => this[nameof (ChoicesConfigDescription)];

    /// <summary>Choices</summary>
    [ResourceEntry("ChoicesTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "Choices")]
    public string ChoicesTitle => this[nameof (ChoicesTitle)];

    /// <summary>
    /// Represents Choices property of each choice field control.
    /// </summary>
    [ResourceEntry("ChoicesDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents Choices property of each choice field control.")]
    public string ChoicesDescription => this[nameof (ChoicesDescription)];

    /// <summary>MutuallyExclusive</summary>
    [ResourceEntry("MutuallyExclusiveTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "MutuallyExclusive")]
    public string MutuallyExclusiveTitle => this[nameof (MutuallyExclusiveTitle)];

    /// <summary>
    /// Represents MutuallyExclusive property of each choice field control.
    /// </summary>
    [ResourceEntry("MutuallyExclusiveDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents MutuallyExclusive property of each choice field control.")]
    public string MutuallyExclusiveDescription => this[nameof (MutuallyExclusiveDescription)];

    /// <summary>RenderChoicesAs</summary>
    [ResourceEntry("RenderChoiceAsTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "RenderChoiceAs")]
    public string RenderChoiceAsTitle => this[nameof (RenderChoiceAsTitle)];

    /// <summary>
    /// Represents RenderChoicesAs property of each choice field control.
    /// </summary>
    [ResourceEntry("RenderChoiceAsDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents RenderChoiceAs property of each choice field control.")]
    public string RenderChoiceAsDescription => this[nameof (RenderChoiceAsDescription)];

    /// <summary>SelectedChoicesIndex</summary>
    [ResourceEntry("SelectedChoicesIndexTitle", Description = "The title of the configuration element.", LastModified = "2010/02/19", Value = "SelectedChoicesIndex")]
    public string SelectedChoicesIndexTitle => this[nameof (SelectedChoicesIndexTitle)];

    /// <summary>
    /// Represents SelectedChoicesIndex property of each choice field control.
    /// </summary>
    [ResourceEntry("SelectedChoicesIndexDescription", Description = "Describes configuration element.", LastModified = "2010/02/19", Value = "Represents SelectedChoicesIndex property of each choice field control.")]
    public string SelectedChoicesIndexDescription => this[nameof (SelectedChoicesIndexDescription)];

    /// <summary>phrase: Hide title</summary>
    [ResourceEntry("HideTitleCaption", Description = "phrase: Hide title", LastModified = "2010/12/16", Value = "Hide title")]
    public string HideTitleCaption => this[nameof (HideTitleCaption)];

    /// <summary>
    /// phrase: When 'true' the title is hidden. Useful for SingleCheckBox choice fields.
    /// </summary>
    [ResourceEntry("HideTitleDescription", Description = "phrase: When 'true' the title is hidden. Useful for SingleCheckBox choice fields.", LastModified = "2010/12/16", Value = "When 'true' the title is hidden. Useful for SingleCheckBox choice fields.")]
    public string HideTitleDescription => this[nameof (HideTitleDescription)];

    /// <summary>
    /// The title of the configuration property 'ExpectedFormat'.
    /// </summary>
    [ResourceEntry("ExpectedFormatCaption", Description = "The title of the configuration property 'ExpectedFormat'.", LastModified = "2010/02/19", Value = "Expected format")]
    public string ExpectedFormatCaption => this[nameof (ExpectedFormatCaption)];

    /// <summary>
    /// Describes the configuration property 'ExpectedFormat'.
    /// </summary>
    [ResourceEntry("ExpectedFormatDescription", Description = "Describes the configuration property 'ExpectedFormat'.", LastModified = "2010/02/19", Value = "The expected validation format of the validated value.")]
    public string ExpectedFormatDescription => this[nameof (ExpectedFormatDescription)];

    /// <summary>The title of the configuration property 'MaxLength'.</summary>
    [ResourceEntry("MaxLengthCaption", Description = "The title of the configuration property 'MaxLength'.", LastModified = "2010/02/19", Value = "Maximum length")]
    public string MaxLengthCaption => this[nameof (MaxLengthCaption)];

    /// <summary>Describes the configuration property 'MaxLength'.</summary>
    [ResourceEntry("MaxLengthDescription", Description = "Describes the configuration property 'MaxLength'.", LastModified = "2010/02/19", Value = "The maximum length of the value that is going to be validated.")]
    public string MaxLengthDescription => this[nameof (MaxLengthDescription)];

    /// <summary>The title of the configuration property 'MinLength'.</summary>
    [ResourceEntry("MinLengthCaption", Description = "The title of the configuration property 'MinLength'.", LastModified = "2010/02/19", Value = "Minimum length")]
    public string MinLengthCaption => this[nameof (MinLengthCaption)];

    /// <summary>Describes the configuration property 'MinLength'.</summary>
    [ResourceEntry("MinLengthDescription", Description = "Describes the configuration property 'MinLength'.", LastModified = "2010/02/19", Value = "The minimum length of the value that is going to be validated.")]
    public string MinLengthDescription => this[nameof (MinLengthDescription)];

    /// <summary>The title of the configuration property 'MaxValue'.</summary>
    [ResourceEntry("MaxValueCaption", Description = "The title of the configuration property 'MaxValue'.", LastModified = "2010/02/19", Value = "Maximum length")]
    public string MaxValueCaption => this[nameof (MaxValueCaption)];

    /// <summary>Describes the configuration property 'MaxValue'.</summary>
    [ResourceEntry("MaxValueDescription", Description = "Describes the configuration property 'MaxValue'.", LastModified = "2011/03/11", Value = "The maximum value of the data.")]
    public string MaxValueDescription => this[nameof (MaxValueDescription)];

    /// <summary>
    /// The title of the configuration property 'RecommendedCharacters'.
    /// </summary>
    [ResourceEntry("RecommendedCharactersCaption", Description = "The title of the configuration property 'RecommendedCharacters'.", LastModified = "2018/09/18", Value = "Recommended characters")]
    public string RecommendedCharactersCaption => this[nameof (RecommendedCharactersCaption)];

    /// <summary>
    /// Describes the configuration property 'RecommendedCharacters'.
    /// </summary>
    [ResourceEntry("RecommendedCharactersDescription", Description = "Describes the configuration property 'RecommendedCharacters'.", LastModified = "2018/09/18", Value = "Displayed in a counter below the field.")]
    public string RecommendedCharactersDescription => this["RecommendedCharacters"];

    /// <summary>The title of the configuration property 'MinValue'.</summary>
    [ResourceEntry("MinValueCaption", Description = "The title of the configuration property 'MinValue'.", LastModified = "2010/02/19", Value = "Maximum length")]
    public string MinValueCaption => this[nameof (MinValueCaption)];

    /// <summary>Describes the configuration property 'MinValue'.</summary>
    [ResourceEntry("MinValueDescription", Description = "Describes the configuration property 'MinValue'.", LastModified = "2010/02/19", Value = "The minimum value of the data.")]
    public string MinValueDescription => this[nameof (MinValueDescription)];

    /// <summary>
    /// The title of the configuration property 'RegularExpression'.
    /// </summary>
    [ResourceEntry("RegularExpressionCaption", Description = "The title of the configuration property 'RegularExpression'.", LastModified = "2010/02/19", Value = "Regular expression")]
    public string RegularExpressionCaption => this[nameof (RegularExpressionCaption)];

    /// <summary>
    /// Describes the configuration property 'RegularExpression'.
    /// </summary>
    [ResourceEntry("RegularExpressionDescription", Description = "Describes the configuration property 'RegularExpression'.", LastModified = "2011/03/11", Value = "The regular expression to use when evaluating a string.")]
    public string RegularExpressionDescription => this[nameof (RegularExpressionDescription)];

    /// <summary>
    /// The title of the configuration property 'RegularExpressionSeparator'.
    /// </summary>
    [ResourceEntry("RegularExpressionSeparatorCaption", Description = "The title of the configuration property 'RegularExpressionSeparator'.", LastModified = "2011/01/19", Value = "Regular expression separator")]
    public string RegularExpressionSeparatorCaption => this[nameof (RegularExpressionSeparatorCaption)];

    /// <summary>
    /// Describes the configuration property 'RegularExpressionSeparator'.
    /// </summary>
    [ResourceEntry("RegularExpressionSeparatorDescription", Description = "Describes the configuration property 'RegularExpressionSeparator'.", LastModified = "2011/01/19", Value = "The separator to use when evaluating custom regular expressions.")]
    public string RegularExpressionSeparatorDescription => this[nameof (RegularExpressionSeparatorDescription)];

    /// <summary>The title of the configuration property 'Required'.</summary>
    [ResourceEntry("RequiredCaption", Description = "The title of the configuration property 'Required'.", LastModified = "2010/02/19", Value = "Required")]
    public string RequiredCaption => this[nameof (RequiredCaption)];

    /// <summary>Describes the configuration property 'Required'.</summary>
    [ResourceEntry("RequiredDescription", Description = "Describes the configuration property 'Required'.", LastModified = "2010/02/19", Value = "Specifies whether this field control's data must have a value.")]
    public string RequiredDescription => this[nameof (RequiredDescription)];

    /// <summary>
    /// The title of the configuration property 'AlphaNumericViolationMessage'.
    /// </summary>
    [ResourceEntry("AlphaNumericViolationMessageCaption", Description = "The title of the configuration property 'AlphaNumericViolationMessage'.", LastModified = "2010/02/24", Value = "AlphaNumeric violation message")]
    public string AlphaNumericViolationMessageCaption => this[nameof (AlphaNumericViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'AlphaNumericViolationMessage'.
    /// </summary>
    [ResourceEntry("AlphaNumericViolationMessageDescription", Description = "Describes the configuration property 'AlphaNumericViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when alpha numeric validation has failed.")]
    public string AlphaNumericViolationMessageDescription => this[nameof (AlphaNumericViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'CurrencyViolationMessage'.
    /// </summary>
    [ResourceEntry("CurrencyViolationMessageCaption", Description = "The title of the configuration property 'CurrencyViolationMessage'.", LastModified = "2010/02/24", Value = "Currency violation message")]
    public string CurrencyViolationMessageCaption => this[nameof (CurrencyViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'CurrencyViolationMessage'.
    /// </summary>
    [ResourceEntry("CurrencyViolationMessageDescription", Description = "Describes the configuration property 'CurrencyViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when currency validation has failed.")]
    public string CurrencyViolationMessageDescription => this[nameof (CurrencyViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'EmailAddressViolationMessage'.
    /// </summary>
    [ResourceEntry("EmailAddressViolationMessageCaption", Description = "The title of the configuration property 'EmailAddressViolationMessage'.", LastModified = "2010/02/24", Value = "Email address violation message")]
    public string EmailAddressViolationMessageCaption => this[nameof (EmailAddressViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'EmailAddressViolationMessage'.
    /// </summary>
    [ResourceEntry("EmailAddressViolationMessageDescription", Description = "Describes the configuration property 'EmailAddressViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when email address validation has failed.")]
    public string EmailAddressViolationMessageDescription => this[nameof (EmailAddressViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'IntegerViolationMessage'.
    /// </summary>
    [ResourceEntry("IntegerViolationMessageCaption", Description = "The title of the configuration property 'IntegerViolationMessage'.", LastModified = "2010/02/24", Value = "Integer violation message")]
    public string IntegerViolationMessageCaption => this[nameof (IntegerViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'IntegerViolationMessage'.
    /// </summary>
    [ResourceEntry("IntegerViolationMessageDescription", Description = "Describes the configuration property 'IntegerViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when integer validation has failed.")]
    public string IntegerViolationMessageDescription => this[nameof (IntegerViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'InternetUrlViolationMessage'.
    /// </summary>
    [ResourceEntry("InternetUrlViolationMessageCaption", Description = "The title of the configuration property 'InternetUrlViolationMessage'.", LastModified = "2010/02/24", Value = "Internet URL violation message")]
    public string InternetUrlViolationMessageCaption => this[nameof (InternetUrlViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'InternetUrlViolationMessage'.
    /// </summary>
    [ResourceEntry("InternetUrlViolationMessageDescription", Description = "Describes the configuration property 'InternetUrlViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when internet url validation has failed.")]
    public string InternetUrlViolationMessageDescription => this[nameof (InternetUrlViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'MaxLengthViolationMessage'.
    /// </summary>
    [ResourceEntry("MaxLengthViolationMessageCaption", Description = "The title of the configuration property 'MaxLengthViolationMessage'.", LastModified = "2010/02/24", Value = "Maximum length violation message")]
    public string MaxLengthViolationMessageCaption => this[nameof (MaxLengthViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'MaxLengthViolationMessage'.
    /// </summary>
    [ResourceEntry("MaxLengthViolationMessageDescription", Description = "Describes the configuration property 'MaxLengthViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when maximum length validation has failed.")]
    public string MaxLengthViolationMessageDescription => this[nameof (MaxLengthViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'MaxValueViolationMessage'.
    /// </summary>
    [ResourceEntry("MaxValueViolationMessageCaption", Description = "The title of the configuration property 'MaxValueViolationMessage'.", LastModified = "2010/02/24", Value = "Maximum value violation message")]
    public string MaxValueViolationMessageCaption => this[nameof (MaxValueViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'MaxValueViolationMessage'.
    /// </summary>
    [ResourceEntry("MaxValueViolationMessageDescription", Description = "Describes the configuration property 'MaxValueViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when max value validation has failed.")]
    public string MaxValueViolationMessageDescription => this[nameof (MaxValueViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'MessageCssClass'.
    /// </summary>
    [ResourceEntry("MessageCssClassCaption", Description = "The title of the configuration property 'MessageCssClass'.", LastModified = "2010/02/24", Value = "Message CSS class caption")]
    public string MessageCssClassCaption => this[nameof (MessageCssClassCaption)];

    /// <summary>
    /// Describes the configuration property 'MessageCssClass'.
    /// </summary>
    [ResourceEntry("MessageCssClassDescription", Description = "Describes the configuration property 'MessageCssClass'.", LastModified = "2010/02/24", Value = "The violation message CSS class.")]
    public string MessageCssClassDescription => this[nameof (MessageCssClassDescription)];

    /// <summary>
    /// The title of the configuration property 'MessageTagName'.
    /// </summary>
    [ResourceEntry("MessageTagNameCaption", Description = "The title of the configuration property 'MessageTagName'.", LastModified = "2010/02/24", Value = "Message tag name")]
    public string MessageTagNameCaption => this[nameof (MessageTagNameCaption)];

    /// <summary>
    /// Describes the configuration property 'MessageTagName'.
    /// </summary>
    [ResourceEntry("MessageTagNameDescription", Description = "Describes the configuration property 'MessageTagName'.", LastModified = "2010/02/24", Value = "The name of the violation message tag.")]
    public string MessageTagNameDescription => this[nameof (MessageTagNameDescription)];

    /// <summary>
    /// The title of the configuration property 'MinLengthViolationMessage'.
    /// </summary>
    [ResourceEntry("MinLengthViolationMessageCaption", Description = "The title of the configuration property 'MinLengthViolationMessage'.", LastModified = "2010/02/24", Value = "Minimum length violation message")]
    public string MinLengthViolationMessageCaption => this[nameof (MinLengthViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'MinLengthViolationMessage'.
    /// </summary>
    [ResourceEntry("MinLengthViolationMessageDescription", Description = "Describes the configuration property 'MinLengthViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when minimum length validation has failed.")]
    public string MinLengthViolationMessageDescription => this[nameof (MinLengthViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'MinValueViolationMessage'.
    /// </summary>
    [ResourceEntry("MinValueViolationMessageCaption", Description = "The title of the configuration property 'MinValueViolationMessage'.", LastModified = "2010/02/24", Value = "Minimum value violation message")]
    public string MinValueViolationMessageCaption => this[nameof (MinValueViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'MinValueViolationMessage'.
    /// </summary>
    [ResourceEntry("MinValueViolationMessageDescription", Description = "Describes the configuration property 'MinValueViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when minimum value validation has failed.")]
    public string MinValueViolationMessageDescription => this[nameof (MinValueViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'NonAlphaNumericViolationMessage'.
    /// </summary>
    [ResourceEntry("NonAlphaNumericViolationMessageCaption", Description = "The title of the configuration property 'NonAlphaNumericViolationMessage'.", LastModified = "2010/02/24", Value = "Non alphaNumeric violation message")]
    public string NonAlphaNumericViolationMessageCaption => this[nameof (NonAlphaNumericViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'NonAlphaNumericViolationMessage'.
    /// </summary>
    [ResourceEntry("NonAlphaNumericViolationMessageDescription", Description = "Describes the configuration property 'NonAlphaNumericViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when non alphanumeric validation has failed.")]
    public string NonAlphaNumericViolationMessageDescription => this[nameof (NonAlphaNumericViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'NumericViolationMessage'.
    /// </summary>
    [ResourceEntry("NumericViolationMessageCaption", Description = "The title of the configuration property 'NumericViolationMessage'.", LastModified = "2010/02/24", Value = "Numeric violation message")]
    public string NumericViolationMessageCaption => this[nameof (NumericViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'NumericViolationMessage'.
    /// </summary>
    [ResourceEntry("NumericViolationMessageDescription", Description = "Describes the configuration property 'NumericViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when numeric validation has failed.")]
    public string NumericViolationMessageDescription => this[nameof (NumericViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'PercentageViolationMessage'.
    /// </summary>
    [ResourceEntry("PercentageViolationMessageCaption", Description = "The title of the configuration property 'PercentageViolationMessage'.", LastModified = "2010/02/24", Value = "Percentage violation message")]
    public string PercentageViolationMessageCaption => this[nameof (PercentageViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'PercentageViolationMessage'.
    /// </summary>
    [ResourceEntry("PercentageViolationMessageDescription", Description = "Describes the configuration property 'PercentageViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when percentage validation has failed.")]
    public string PercentageViolationMessageDescription => this[nameof (PercentageViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'RegularExpressionViolationMessage'.
    /// </summary>
    [ResourceEntry("RegularExpressionViolationMessageCaption", Description = "The title of the configuration property 'RegularExpressionViolationMessage'.", LastModified = "2010/02/24", Value = "RegularExpression violation message")]
    public string RegularExpressionViolationMessageCaption => this[nameof (RegularExpressionViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'RegularExpressionViolationMessage'.
    /// </summary>
    [ResourceEntry("RegularExpressionViolationMessageDescription", Description = "Describes the configuration property 'RegularExpressionViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when regular expression validation has failed.")]
    public string RegularExpressionViolationMessageDescription => this[nameof (RegularExpressionViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'RequiredViolationMessage'.
    /// </summary>
    [ResourceEntry("RequiredViolationMessageCaption", Description = "The title of the configuration property 'RequiredViolationMessage'.", LastModified = "2010/02/24", Value = "Required violation message")]
    public string RequiredViolationMessageCaption => this[nameof (RequiredViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'RequiredViolationMessage'.
    /// </summary>
    [ResourceEntry("RequiredViolationMessageDescription", Description = "Describes the configuration property 'RequiredViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when required validation has failed.")]
    public string RequiredViolationMessageDescription => this[nameof (RequiredViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'USSocialSecurityNumberViolationMessage'.
    /// </summary>
    [ResourceEntry("USSocialSecurityNumberViolationMessageCaption", Description = "The title of the configuration property 'USSocialSecurityNumberViolationMessage'.", LastModified = "2010/02/24", Value = "US social security number violation message")]
    public string USSocialSecurityNumberViolationMessageCaption => this[nameof (USSocialSecurityNumberViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'USSocialSecurityNumberViolationMessage'.
    /// </summary>
    [ResourceEntry("USSocialSecurityNumberViolationMessageDescription", Description = "Describes the configuration property 'USSocialSecurityNumberViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when US social security number validation has failed.")]
    public string USSocialSecurityNumberViolationMessageDescription => this[nameof (USSocialSecurityNumberViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'USZipCodeViolationMessage'.
    /// </summary>
    [ResourceEntry("USZipCodeViolationMessageCaption", Description = "The title of the configuration property 'USZipCodeViolationMessage'.", LastModified = "2010/02/24", Value = "US zip code violation message")]
    public string USZipCodeViolationMessageCaption => this[nameof (USZipCodeViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'USZipCodeViolationMessage'.
    /// </summary>
    [ResourceEntry("USZipCodeViolationMessageDescription", Description = "Describes the configuration property 'USZipCodeViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message shown when US zip code validation has failed.")]
    public string USZipCodeViolationMessageDescription => this[nameof (USZipCodeViolationMessageDescription)];

    /// <summary>
    /// The title of the configuration property 'ValidateIfInvisible'.
    /// </summary>
    [ResourceEntry("ValidateIfInvisibleCaption", Description = "The title of the configuration property 'ValidateIfInvisible'.", LastModified = "2010/03/30", Value = "Validate if invisible")]
    public string ValidateIfInvisibleCaption => this[nameof (ValidateIfInvisibleCaption)];

    /// <summary>
    /// Describes the configuration property 'ValidateIfInvisible'.
    /// </summary>
    [ResourceEntry("ValidateIfInvisibleDescription", Description = "Describes the configuration property 'ValidateIfInvisible'.", LastModified = "2010/03/30", Value = "Specifies whether to validate if the validated control is invisible.")]
    public string ValidateIfInvisibleDescription => this[nameof (ValidateIfInvisibleDescription)];

    /// <summary>
    /// The title of the configuration property 'ComparingValidatorDefinitions'.
    /// </summary>
    [ResourceEntry("ComparingValidatorDefinitionsCaption", Description = "The title of the configuration property 'ComparingValidatorDefinitions'.", LastModified = "2010/03/03", Value = "Comparing validations")]
    public string ComparingValidatorDefinitionsCaption => this[nameof (ComparingValidatorDefinitionsCaption)];

    /// <summary>
    /// Describes the configuration property 'ComparingValidatorDefinitions'.
    /// </summary>
    [ResourceEntry("ComparingValidatorDefinitionsDescription", Description = "Describes the configuration property 'ComparingValidatorDefinitions'.", LastModified = "2010/02/24", Value = "Specifies the validation requirements that are going to be used when comparing against other controls' values.")]
    public string ComparingValidatorDefinitionsDescription => this[nameof (ComparingValidatorDefinitionsDescription)];

    /// <summary>
    /// The title of the configuration property 'ControlToCompare'.
    /// </summary>
    [ResourceEntry("ControlToCompareCaption", Description = "The title of the configuration property 'ControlToCompare'.", LastModified = "2010/03/03", Value = "Control to compare")]
    public string ControlToCompareCaption => this[nameof (ControlToCompareCaption)];

    /// <summary>
    /// Describes the configuration property 'ControlToCompare'.
    /// </summary>
    [ResourceEntry("ControlToCompareDescription", Description = "Describes the configuration property 'ControlToCompare'.", LastModified = "2010/02/24", Value = "Specifies the id of the control which value is going to be used as range limitation.")]
    public string ControlToCompareDescription => this[nameof (ControlToCompareDescription)];

    /// <summary>The title of the configuration property 'Operator'.</summary>
    [ResourceEntry("OperatorCaption", Description = "The title of the configuration property 'Operator'.", LastModified = "2010/03/03", Value = "Operator")]
    public string OperatorCaption => this[nameof (OperatorCaption)];

    /// <summary>Describes the configuration property 'Operator'.</summary>
    [ResourceEntry("OperatorDescription", Description = "Describes the configuration property 'Operator'.", LastModified = "2010/02/24", Value = "Specifies the comparison operation to perform.")]
    public string OperatorDescription => this[nameof (OperatorDescription)];

    /// <summary>The title of the configuration property 'Operator'.</summary>
    [ResourceEntry("ValidationViolationMessageCaption", Description = "The title of the configuration property 'ValidationViolationMessage'.", LastModified = "2010/03/03", Value = "Validation violation message")]
    public string ValidationViolationMessageCaption => this[nameof (ValidationViolationMessageCaption)];

    /// <summary>
    /// Describes the configuration property 'ValidationViolationMessage'.
    /// </summary>
    [ResourceEntry("ValidationViolationMessageDescription", Description = "Describes the configuration property 'ValidationViolationMessage'.", LastModified = "2010/02/24", Value = "Specifies the message to show if the validation failed.")]
    public string ValidationViolationMessageDescription => this[nameof (ValidationViolationMessageDescription)];

    /// <summary>Collapsible Field definition controls settings.</summary>
    [ResourceEntry("StatusFieldElementTitle", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Status Field definition controls settings.")]
    public string StatusFieldElementTitle => this[nameof (StatusFieldElementTitle)];

    /// <summary>
    /// Description of status field definition controls configuration settings.
    /// </summary>
    [ResourceEntry("StatusFieldElementDescription", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Description of status field definition controls configuration settings.")]
    public string StatusFieldElementDescription => this[nameof (StatusFieldElementDescription)];

    /// <summary>Publication date definition controls settings.</summary>
    [ResourceEntry("StatusFieldElementPublicationDateFieldDefinitionCaption", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Publication date definition controls settings.")]
    public string StatusFieldElementPublicationDateFieldDefinitionCaption => this[nameof (StatusFieldElementPublicationDateFieldDefinitionCaption)];

    /// <summary>
    /// Description of publication date field definition of status controls configuration settings.
    /// </summary>
    [ResourceEntry("StatusFieldElementPublicationDateFieldDefinitionDescription", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Description of publication date field definition of status controls configuration settings.")]
    public string StatusFieldElementPublicationDateFieldDefinitionDescription => this[nameof (StatusFieldElementPublicationDateFieldDefinitionDescription)];

    /// <summary>Expiration date definition controls settings.</summary>
    [ResourceEntry("StatusFieldElementExpirationDateFieldDefinitionCaption", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Expiration date definition controls settings.")]
    public string StatusFieldElementExpirationDateFieldDefinitionCaption => this[nameof (StatusFieldElementExpirationDateFieldDefinitionCaption)];

    /// <summary>
    /// Description of expiration date field definition of status controls configuration settings.
    /// </summary>
    [ResourceEntry("StatusFieldElementExpirationDateFieldDefinitionDescription", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Description of expiration date field definition of status controls configuration settings.")]
    public string StatusFieldElementExpirationDateFieldDefinitionDescription => this[nameof (StatusFieldElementExpirationDateFieldDefinitionDescription)];

    /// <summary>Status field control definition controls settings.</summary>
    [ResourceEntry("StatusFieldElementStatusFieldControlDefinitionCaption", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Status field control definition controls settings.")]
    public string StatusFieldElementStatusFieldControlDefinitionCaption => this[nameof (StatusFieldElementStatusFieldControlDefinitionCaption)];

    /// <summary>
    /// Description of status field control definition of status controls configuration settings.
    /// </summary>
    [ResourceEntry("StatusFieldElementStatusFieldControlDefinitionDescription", Description = "Describes configuration element.", LastModified = "2010/02/18", Value = "Description of status field control definition of status controls configuration settings.")]
    public string StatusFieldElementStatusFieldControlDefinitionDescription => this[nameof (StatusFieldElementStatusFieldControlDefinitionDescription)];

    /// <summary>phrase: Item name</summary>
    [ResourceEntry("FileFieldItemNameCaption", Description = "phrase: Item name", LastModified = "2010/03/16", Value = "Item name")]
    public string FileFieldItemNameCaption => this[nameof (FileFieldItemNameCaption)];

    /// <summary>
    /// phrase: Configuration element that is used to specify the name of the item being managed by the field control.
    /// </summary>
    [ResourceEntry("FileFieldItemNameDescription", Description = "phrase: Configuration element that is used to specify the name of the item being managed by the field control.", LastModified = "2011/03/11", Value = "Configuration element that is used to specify the name of the item being managed by the field control.")]
    public string FileFieldItemNameDescription => this[nameof (FileFieldItemNameDescription)];

    /// <summary>phrase: Item name plural</summary>
    [ResourceEntry("FileFieldItemNamePluralCaption", Description = "phrase: Item name plural", LastModified = "2010/03/16", Value = "Item name plural")]
    public string FileFieldItemNamePluralCaption => this[nameof (FileFieldItemNamePluralCaption)];

    /// <summary>
    /// phrase: Configuration element that is used to specify the name of the item being managed by the field control in plural.
    /// </summary>
    [ResourceEntry("FileFieldItemNamePluralDescription", Description = "phrase: Configuration element that is used to specify the name of the item being managed by the field control in plural.", LastModified = "2011/03/11", Value = "Configuration element that is used to specify the name of the item being managed by the field control in plural.")]
    public string FileFieldItemNamePluralDescription => this[nameof (FileFieldItemNamePluralDescription)];

    /// <summary>phrase: Library Content Type</summary>
    [ResourceEntry("LibraryContentTypeCaption", Description = "phrase: Library Content Type", LastModified = "2010/05/20", Value = "Library Content Type")]
    public string LibraryContentTypeCaption => this[nameof (LibraryContentTypeCaption)];

    /// <summary>
    /// phrase: Configuration element that is used to specify the library content type being managed by the field control.
    /// </summary>
    [ResourceEntry("LibraryContentTypeDescription", Description = " phrase: Configuration element that is used to specify the library content type being managed by the field control.", LastModified = "2010/05/20", Value = "Configuration element that is used to specify the library content type being managed by the field control.")]
    public string LibraryContentTypeDescription => this[nameof (LibraryContentTypeDescription)];

    /// <summary>phrase: IsMultiselect</summary>
    [ResourceEntry("IsMultiselectCaption", Description = "phrase: IsMultiselect", LastModified = "2010/05/20", Value = "IsMultiselect")]
    public string IsMultiselectCaption => this[nameof (IsMultiselectCaption)];

    /// <summary>
    /// phrase: Configuration element that is used to specify whether the uploader allows multiple files to be selected.
    /// </summary>
    [ResourceEntry("IsMultiselectDescription", Description = " phrase: Configuration element that is used to specify whether the uploader allows multiple files to be selected.", LastModified = "2010/05/20", Value = "Configuration element that is used to specify whether the uploader allows multiple files to be selected.")]
    public string IsMultiselectDescription => this[nameof (IsMultiselectDescription)];

    /// <summary>phrase: Max File Count</summary>
    [ResourceEntry("MaxFileCountCaption", Description = "phrase: Max File Count", LastModified = "2010/05/21", Value = "Max File Count")]
    public string MaxFileCountCaption => this[nameof (MaxFileCountCaption)];

    /// <summary>
    /// phrase: Configuration element that is used to specify maximum allowed number of files selected in the uploader.
    /// </summary>
    [ResourceEntry("MaxFileCountDescription", Description = " phrase: Configuration element that is used to specify  maximum allowed number of files selected in the uploader.", LastModified = "2010/05/21", Value = "Configuration element that is used to specify maximum allowed number of files selected in the uploader.")]
    public string MaxFileCountDescription => this[nameof (MaxFileCountDescription)];

    /// <summary>Status field control definition controls settings.</summary>
    [ResourceEntry("ChoiceFieldElementSizesChoiceFieldDefinitionCaption", Description = "Describes configuration element.", LastModified = "2010/04/29", Value = "Sizes choice field control definition controls settings.")]
    public string ChoiceFieldElementSizesChoiceFieldDefinitionCaption => this[nameof (ChoiceFieldElementSizesChoiceFieldDefinitionCaption)];

    /// <summary>
    /// Description of status field control definition of status controls configuration settings.
    /// </summary>
    [ResourceEntry("ChoiceFieldElementSizesChoiceFieldDefinitionDescription", Description = "Describes configuration element.", LastModified = "2010/04/29", Value = "Description of sizes choice field control definition controls settings.")]
    public string ChoiceFieldElementSizesChoiceFieldDefinitionDescription => this[nameof (ChoiceFieldElementSizesChoiceFieldDefinitionDescription)];

    /// <summary>Status field control definition controls settings.</summary>
    [ResourceEntry("EmbedStringTemplateCaption", Description = "Describes configuration element.", LastModified = "2010/04/29", Value = "Embed string template.")]
    public string EmbedStringTemplateCaption => this[nameof (EmbedStringTemplateCaption)];

    /// <summary>
    /// Description of status field control definition of status controls configuration settings.
    /// </summary>
    [ResourceEntry("EmbedStringTemplateDescription", Description = "Describes configuration element.", LastModified = "2010/04/29", Value = "Description of embed string template.")]
    public string EmbedStringTemplateDescription => this[nameof (EmbedStringTemplateDescription)];

    /// <summary>Status field control definition controls settings.</summary>
    [ResourceEntry("CustomizeButtonTitleCaption", Description = "Describes configuration element.", LastModified = "2010/04/29", Value = "Customize button title.")]
    public string CustomizeButtonTitleCaption => this[nameof (CustomizeButtonTitleCaption)];

    /// <summary>
    /// Description of status field control definition of status controls configuration settings.
    /// </summary>
    [ResourceEntry("CustomizeButtonTitleDescription", Description = "Describes configuration element.", LastModified = "2010/04/29", Value = "Description of customize button title.")]
    public string CustomizeButtonTitleDescription => this[nameof (CustomizeButtonTitleDescription)];

    /// <summary>Status field control definition controls settings.</summary>
    [ResourceEntry("HideEmbedTextBoxCaption", Description = "Describes configuration element.", LastModified = "2010/04/29", Value = "Hide embed text box.")]
    public string HideEmbedTextBoxCaption => this[nameof (HideEmbedTextBoxCaption)];

    /// <summary>
    /// Description of status field control definition of status controls configuration settings.
    /// </summary>
    [ResourceEntry("HideEmbedTextBoxDescription", Description = "Describes configuration element.", LastModified = "2010/04/29", Value = "Description of the hide embed text box element of the embed control.")]
    public string HideEmbedTextBoxDescription => this[nameof (HideEmbedTextBoxDescription)];

    /// <summary>Resource strings for PageFieldElement class.</summary>
    [ResourceEntry("RootNodeIDCaption", Description = "A value indicating the ID of the page node from which the binding should start.", LastModified = "2010/08/02", Value = "A value indicating the ID of the page node from which the binding should start.")]
    public string RootNodeIDCaption => this[nameof (RootNodeIDCaption)];

    /// <summary>Resource strings for PageFieldElement class.</summary>
    [ResourceEntry("RootNodeIDDescription", Description = "A value indicating the ID of the page node from which the binding should start.", LastModified = "2010/07/29", Value = "A value indicating the ID of the page node from which the binding should start.")]
    public string RootNodeIDDescription => this[nameof (RootNodeIDDescription)];

    /// <summary>phrase: Provdier name</summary>
    [ResourceEntry("ProviderNameCaption", Description = "phrase: Provdier name", LastModified = "2010/08/03", Value = "The name of the provider.")]
    public string ProviderNameCaption => this[nameof (ProviderNameCaption)];

    /// <summary>phrase: Provdier name</summary>
    [ResourceEntry("ProviderNameDescription", Description = "phrase: Provdier name", LastModified = "2010/08/03", Value = "The name of the provider.")]
    public string ProviderNameDescription => this[nameof (ProviderNameDescription)];

    /// <summary>phrase: Bind on Load</summary>
    [ResourceEntry("BindOnLoadCaption", Description = "phrase: Bind on Load", LastModified = "2012/01/24", Value = "Bind on Load")]
    public string BindOnLoadCaption => this[nameof (BindOnLoadCaption)];

    /// <summary>
    /// phrase: Gets or sets a value indicating whether to automatically bind the selector on load.
    /// </summary>
    [ResourceEntry("BindOnLoadDescription", Description = "phrase: Gets or sets a value indicating whether to automatically bind the selector on load.", LastModified = "2012/01/24", Value = "Gets or sets a value indicating whether to automatically bind the selector on load.")]
    public string BindOnLoadDescription => this[nameof (BindOnLoadDescription)];

    /// <summary>
    /// Message: Configurable flag not to create template for master page.
    /// </summary>
    /// <value>ShouldNotCreateTemplateForMasterPageCaption</value>
    [ResourceEntry("ShouldNotCreateTemplateForMasterPageCaption", Description = "Configurable flag not to create template for master page.", LastModified = "2009/01/14", Value = "Configurable flag not to create template for master page.")]
    public string ShouldNotCreateTemplateForMasterPageCaption => this[nameof (ShouldNotCreateTemplateForMasterPageCaption)];

    /// <summary>
    /// phrase: Configurable flag not to create template for master page.
    /// </summary>
    [ResourceEntry("ShouldNotCreateTemplateForMasterPageDescription", Description = "phrase: Configurable flag not to create template for master page.", LastModified = "2010/08/03", Value = "Configurable flag not to create template for master page.")]
    public string ShouldNotCreateTemplateForMasterPageDescription => this[nameof (ShouldNotCreateTemplateForMasterPageDescription)];

    /// <summary>
    /// Message: Configurable flag specifying if template is for backend page.
    /// </summary>
    /// <value>Configurable flag specifying if template is for backend page.</value>
    [ResourceEntry("IsBackendTemplateCaption", Description = "Configurable flag specifying if template is for backend page.", LastModified = "2009/01/14", Value = "Configurable flag specifying if template is for backend page.")]
    public string IsBackendTemplateCaption => this[nameof (IsBackendTemplateCaption)];

    /// <summary>
    /// phrase: Configurable flag specifying if template is for backend page.
    /// </summary>
    [ResourceEntry("IsBackendTemplateDescription", Description = "phrase: Configurable flag not to create template for master page.", LastModified = "2010/08/03", Value = "Configurable flag specifying if template is for backend page.")]
    public string IsBackendTemplateDescription => this[nameof (IsBackendTemplateDescription)];

    /// <summary>
    /// Message: Configurable flag specifying if template is for backend page.
    /// </summary>
    /// <value>Configurable flag specifying if template is for backend page.</value>
    [ResourceEntry("ShowEmptyTemplateCaption", Description = "Configurable flag specifying if an empty template is shown.", LastModified = "2019/02/14", Value = "Show empty template")]
    public string ShowEmptyTemplateCaption => this[nameof (ShowEmptyTemplateCaption)];

    /// <summary>
    /// phrase: Configurable flag specifying if template is for backend page.
    /// </summary>
    [ResourceEntry("ShowEmptyTemplateDescription", Description = "phrase: Configurable flag specifying if an empty template is shown.", LastModified = "2019/02/14", Value = "Configurable flag specifying if an empty template is shown.")]
    public string ShowEmptyTemplateDescription => this[nameof (ShowEmptyTemplateDescription)];

    [ResourceEntry("ShowAllBasicTemplatesCaption", Description = "phrase: Configurable flag specifying if an extended list of templates should be shown in the template selector.", LastModified = "2019/03/12", Value = "Show all Basic templates.")]
    public string ShowAllBasicTemplatesCaption => this[nameof (ShowAllBasicTemplatesCaption)];

    [ResourceEntry("ShowAllBasicTemplatesDescription", Description = "phrase: Configurable flag specifying if an extended list of templates should be shown in the template selector.", LastModified = "2019/03/12", Value = "Configurable flag specifying if an extended list of templates should be shown in the template selector.")]
    public string ShowAllBasicTemplatesDescription => this["ShowAllBasicTemplatesDesctription"];

    /// <summary>
    /// phrase: Configurable field for the title of the create hyperlink.
    /// </summary>
    [ResourceEntry("CreateHyperLinkTitleDescription", Description = "phrase: Configurable field for the title of the create hyperlink", LastModified = "2011/01/25", Value = "Configurable field for the title of the create hyperlink")]
    public string CreateHyperLinkTitleDescription => this[nameof (CreateHyperLinkTitleDescription)];

    /// <summary>
    /// phrase: Configurable field for the title of the create hyperlink.
    /// </summary>
    [ResourceEntry("CreateHyperLinkTitleCaption", Description = "phrase: Configurable field for the title of the create hyperlink.", LastModified = "2011/01/25", Value = "Configurable field for the title of the create hyperlink.")]
    public string CreateHyperLinkTitleCaption => this[nameof (CreateHyperLinkTitleCaption)];

    /// <summary>
    /// phrase: Configurable field for the title of the example hyperlink.
    /// </summary>
    [ResourceEntry("ExampleHyperLinkTitleDescription", Description = "phrase: Configurable field for the title of the example hyperlink", LastModified = "2012/02/16", Value = "Configurable field for the title of the example hyperlink")]
    public string ExampleHyperLinkTitleDescription => this[nameof (ExampleHyperLinkTitleDescription)];

    /// <summary>
    /// phrase: Configurable value for the property of the selected Wcf object to be displayed.
    /// </summary>
    /// <value>Conifgurable value for the property of the selected Wcf object to be displayed.</value>
    [ResourceEntry("SelectedNodeDataFieldNameCaption", Description = "phrase: Configurable value for the property of the selected Wcf object to be displayed.", LastModified = "2014/01/14", Value = "Configurable value for the property of the selected Wcf object to be displayed.")]
    public string SelectedNodeDataFieldNameCaption => this[nameof (SelectedNodeDataFieldNameCaption)];

    /// <summary>
    /// phrase: Configurable value for the property of the selected Wcf object to be displayed.
    /// </summary>
    /// <value>Configurable value for the property of the selected Wcf object to be displayed.</value>
    [ResourceEntry("SelectedNodeDataFieldNameDescription", Description = "phrase: Configurable value for the property of the selected Wcf object to be displayed.", LastModified = "2014/01/14", Value = "Configurable value for the property of the selected Wcf object to be displayed.")]
    public string SelectedNodeDataFieldNameDescription => this[nameof (SelectedNodeDataFieldNameDescription)];

    /// <summary>
    /// phrase: Configurable field for the title of the example hyperlink.
    /// </summary>
    [ResourceEntry("ExampleHyperLinkTitleCaption", Description = "phrase: Configurable field for the title of the example hyperlink.", LastModified = "2012/02/16", Value = "Configurable field for the title of the example hyperlink.")]
    public string ExampleHyperLinkTitleCaption => this[nameof (ExampleHyperLinkTitleCaption)];

    /// <summary>
    /// The title of the configuration property 'ExpandField'.
    /// </summary>
    [ResourceEntry("ExpandFieldCaption", Description = "The title of the configuration property 'ExpandField'.", LastModified = "2010/09/14", Value = "Expand Field")]
    public string ExpandFieldCaption => this[nameof (ExpandFieldCaption)];

    /// <summary>Describes the configuration property 'ExpandField'.</summary>
    [ResourceEntry("ExpandFieldDescription", Description = "Describes the configuration property 'ExpandField'.", LastModified = "2010/09/14", Value = "Gets the reference to the control that when clicked expands the hidden part of the whole control.")]
    public string ExpandFieldDescription => this[nameof (ExpandFieldDescription)];

    /// <summary>
    /// The title of the configuration property 'ExpandableFields'.
    /// </summary>
    [ResourceEntry("ExpandableFieldsCaption", Description = "The title of the configuration property 'ExpandableFields'.", LastModified = "2010/09/14", Value = "Expandable Fields")]
    public string ExpandableFieldsCaption => this[nameof (ExpandableFieldsCaption)];

    /// <summary>
    /// Describes the configuration property 'ExpandableFields'.
    /// </summary>
    [ResourceEntry("ExpandableFieldsDescription", Description = "Describes the configuration property 'ExpandableFields'.", LastModified = "2010/09/14", Value = "Defines a collection of field definitions that belong to expandable field.")]
    public string ExpandableFieldsDescription => this[nameof (ExpandableFieldsDescription)];

    /// <summary>phrase: Edit button text resources</summary>
    [ResourceEntry("EditButtonTextResourceCaption", Description = "phrase: Edit button text resources", LastModified = "2010/10/26", Value = "Edit button text resources")]
    public string EditButtonTextResourceCaption => this[nameof (EditButtonTextResourceCaption)];

    /// <summary>
    /// phrase: Defines to the resource containing the localized text to appear on the 'Change data structure...' button.
    /// </summary>
    [ResourceEntry("EditButtonTextResourceDescription", Description = "phrase: Defines to the resource containing the localized text to appear on the 'Change data structure...' button.", LastModified = "2010/10/26", Value = "Defines to the resource containing the localized text to appear on the 'Change data structure...' button.")]
    public string EditButtonTextResourceDescription => this[nameof (EditButtonTextResourceDescription)];

    /// <summary>
    /// The title of the configuration property 'CacheDurationTextFieldDefinition'.
    /// </summary>
    [ResourceEntry("CacheDurationTextFieldDefinitionCaption", Description = "phrase: The title of the configuration property 'CacheDurationTextFieldDefinition'.", LastModified = "2010/12/22", Value = "The cache duration text field")]
    public string CacheDurationTextFieldDefinitionCaption => this[nameof (CacheDurationTextFieldDefinitionCaption)];

    /// <summary>
    /// The description of the configuration property 'CacheDurationTextFieldDefinition'.
    /// </summary>
    [ResourceEntry("CacheDurationTextFieldDefinitionDescription", Description = "phrase: Describes the configuration property 'CacheDurationTextFieldDefinition'.", LastModified = "2010/12/22", Value = "Gets the reference to the text field control that contains the cache duration.")]
    public string CacheDurationTextFieldDefinitionDescription => this[nameof (CacheDurationTextFieldDefinitionDescription)];

    /// <summary>
    /// The title of the configuration property 'SlidingExpirationChoiceFieldDefinition'.
    /// </summary>
    [ResourceEntry("SlidingExpirationChoiceFieldDefinitionCaption", Description = "phrase: The title of the configuration property 'SlidingExpirationChoiceFieldDefinition'.", LastModified = "2010/12/22", Value = "The flag, indicating if sliding expiration is enabled")]
    public string SlidingExpirationChoiceFieldDefinitionCaption => this[nameof (SlidingExpirationChoiceFieldDefinitionCaption)];

    /// <summary>
    /// The description of the configuration property 'SlidingExpirationChoiceFieldDefinition'.
    /// </summary>
    [ResourceEntry("SlidingExpirationChoiceFieldDefinitionDescription", Description = "phrase: Describes the configuration property 'SlidingExpirationChoiceFieldDefinition'.", LastModified = "2010/12/22", Value = "Gets the reference to the choice field control indicating if sliding expiration is enabled.")]
    public string SlidingExpirationChoiceFieldDefinitionDescription => this[nameof (SlidingExpirationChoiceFieldDefinitionDescription)];

    /// <summary>
    /// The title of the configuration property 'EnableCachingFieldDefinition'.
    /// </summary>
    [ResourceEntry("EnableCachingFieldDefinitionCaption", Description = "phrase: The title of the configuration property 'EnableCachingFieldDefinition'.", LastModified = "2010/12/22", Value = "The flag, indicating if caching is enabled")]
    public string EnableCachingFieldDefinitionCaption => this[nameof (EnableCachingFieldDefinitionCaption)];

    /// <summary>
    /// The description of the configuration property 'EnableCachingFieldDefinition'.
    /// </summary>
    [ResourceEntry("EnableCachingFieldDefinitionDescription", Description = "phrase: Describes the configuration property 'EnableCachingFieldDefinition'.", LastModified = "2010/12/22", Value = "Gets the reference to the choice field control indicating if caching is enabled.")]
    public string EnableCachingFieldDefinitionDescription => this[nameof (EnableCachingFieldDefinitionDescription)];

    /// <summary>
    /// The title of the configuration property 'UseDefaultSettingsForCachingFieldDefinition'.
    /// </summary>
    [ResourceEntry("UseDefaultSettingsForCachingFieldDefinitionCaption", Description = "phrase: The title of the configuration property 'UseDefaultSettingsForCachingFieldDefinition'.", LastModified = "2011/01/06", Value = "The flag, indicating if 'use default' is enabled for cache settings.")]
    public string UseDefaultSettingsForCachingFieldDefinitionCaption => this[nameof (UseDefaultSettingsForCachingFieldDefinitionCaption)];

    /// <summary>
    /// The description of the configuration property 'UseDefaultSettingsForCachingFieldDefinition'.
    /// </summary>
    [ResourceEntry("UseDefaultSettingsForCachingFieldDefinitionDescription", Description = "phrase: Describes the configuration property 'UseDefaultSettingsForCachingFieldDefinition'.", LastModified = "2011/01/06", Value = "Gets the reference to the choice field control indicating if 'use default' is enabled for cache settings.")]
    public string UseDefaultSettingsForCachingFieldDefinitionDescription => this[nameof (UseDefaultSettingsForCachingFieldDefinitionDescription)];

    /// <summary>
    /// The title of the configuration property 'IsOutputCache'.
    /// </summary>
    [ResourceEntry("IsOutputCacheFieldDefinitionCaption", Description = "phrase: The title of the configuration property 'IsOutputCache'.", LastModified = "2011/03/16", Value = "The flag, indicating if cache settings field definition is about output caching.")]
    public string IsOutputCacheFieldDefinitionCaption => this[nameof (IsOutputCacheFieldDefinitionCaption)];

    /// <summary>
    /// The description of the configuration property 'IsOutputCache'.
    /// </summary>
    [ResourceEntry("IsOutputCacheFieldDefinitionDescription", Description = "phrase: Describes the configuration property 'IsOutputCacheFieldDefinitionDescription'.", LastModified = "2011/01/06", Value = "The flag, indicating if cache settings field definition is about output caching.")]
    public string IsOutputCacheFieldDefinitionDescription => this[nameof (IsOutputCacheFieldDefinitionDescription)];

    /// <summary>
    /// The title of the configuration property 'ProfileChoiceFieldDefinitionCaption'.
    /// </summary>
    [ResourceEntry("ProfileChoiceFieldDefinitionCaption", Description = "The title of the configuration property 'ProfileChoiceFieldDefinitionCaption'.", LastModified = "2011/03/31", Value = "The cache profile choice field")]
    public string ProfileChoiceFieldDefinitionCaption => this[nameof (ProfileChoiceFieldDefinitionCaption)];

    /// <summary>
    /// The description of the configuration property 'ProfileChoiceFieldDefinitionDescription'.
    /// </summary>
    [ResourceEntry("ProfileChoiceFieldDefinitionDescription", Description = "Describes the configuration property 'ProfileChoiceFieldDefinitionDescription'.", LastModified = "2011/03/31", Value = "Gets the reference to the choice field control that contains the cache profile.")]
    public string ProfileChoiceFieldDefinitionDescription => this[nameof (ProfileChoiceFieldDefinitionDescription)];

    /// <summary>
    /// The title of the configuration property 'IsOutputCache'.
    /// </summary>
    [ResourceEntry("IsOutputCacheCaption", Description = "The title of the configuration property 'IsOutputCache'.", LastModified = "2011/03/31", Value = "The flag, indicating if the cache profile field definition is about output caching.")]
    public string IsOutputCacheCaption => this[nameof (IsOutputCacheCaption)];

    /// <summary>
    /// The description of the configuration property 'IsOutputCache'.
    /// </summary>
    [ResourceEntry("IsOutputCacheDescription", Description = "Describes the configuration property 'IsOutputCache'.", LastModified = "2011/01/06", Value = "The flag, indicating if the cache profile field definition is about output caching.")]
    public string IsOutputCacheDescription => this[nameof (IsOutputCacheDescription)];

    /// <summary>
    /// The title of the configuration property 'CacheSettingsLocation'.
    /// </summary>
    [ResourceEntry("CacheSettingsLocationCaption", Description = "The title of the configuration property 'CacheSettingsLocation'.", LastModified = "2011/04/01", Value = "Gets or sets the cache settings location in the administration.")]
    public string CacheSettingsLocationCaption => this[nameof (CacheSettingsLocationCaption)];

    /// <summary>
    /// The description of the configuration property 'CacheSettingsLocation'.
    /// </summary>
    [ResourceEntry("CacheSettingsLocationDescription", Description = "Describes the configuration property 'CacheSettingsLocation'.", LastModified = "2011/04/01", Value = "Gets or sets the cache settings location in the administration.")]
    public string CacheSettingsLocationDescription => this[nameof (CacheSettingsLocationDescription)];

    /// <summary>
    /// The description of the configuration property 'CanonicalUrlSettingsChoiceFieldDefinitionDescription'.
    /// </summary>
    [ResourceEntry("CanonicalUrlSettingsChoiceFieldDefinitionDescription", Description = "Describes the configuration property 'CanonicalUrlSettingsChoiceFieldDefinitionDescription'.", LastModified = "2013/05/15", Value = "Gets the reference to the choice field control that contains the canonical url settings.")]
    public string CanonicalUrlSettingsChoiceFieldDefinitionDescription => this[nameof (CanonicalUrlSettingsChoiceFieldDefinitionDescription)];

    /// <summary>
    /// The title of the configuration property 'CanonicalUrlSettingsChoiceFieldDefinitionCaption'.
    /// </summary>
    [ResourceEntry("CanonicalUrlSettingsChoiceFieldDefinitionCaption", Description = "The title of the configuration property 'CanonicalUrlSettingsChoiceFieldDefinitionCaption'.", LastModified = "2013/05/15", Value = "The canonical url settings choice field")]
    public string CanonicalUrlSettingsChoiceFieldDefinitionCaption => this[nameof (CanonicalUrlSettingsChoiceFieldDefinitionCaption)];

    /// <summary>
    /// The title of the configuration property 'IsExternalPageChoiceFieldDefinition'.
    /// </summary>
    [ResourceEntry("IsExternalPageChoiceFieldDefinitionCaption", Description = "phrase: The title of the configuration property 'IsExternalPageChoiceFieldDefinition'.", LastModified = "2011/01/13", Value = "The flag, indicating if the page is external")]
    public string IsExternalPageChoiceFieldDefinitionCaption => this[nameof (IsExternalPageChoiceFieldDefinitionCaption)];

    /// <summary>
    /// The description of the configuration property 'IsExternalPageChoiceFieldDefinition'.
    /// </summary>
    [ResourceEntry("IsExternalPageChoiceFieldDefinitionDescription", Description = "phrase: Describes the configuration property 'IsExternalPageChoiceFieldDefinition'.", LastModified = "2011/01/13", Value = "Gets the reference to the choice field control indicating if the page is external.")]
    public string IsExternalPageChoiceFieldDefinitionDescription => this[nameof (IsExternalPageChoiceFieldDefinitionDescription)];

    /// <summary>The title of the configuration property 'MaxWidth'.</summary>
    [ResourceEntry("MaxWidthCaption", Description = "The title of the configuration property 'MaxWidth'.", LastModified = "2011/03/11", Value = "Maximal width")]
    public string MaxWidthCaption => this[nameof (MaxWidthCaption)];

    /// <summary>Describes the configuration property 'MaxWidth'.</summary>
    [ResourceEntry("MaxWidthDescription", Description = "Describes the configuration property 'MaxWidth'.", LastModified = "2011/03/11", Value = "The maximal width in pixels of the image.")]
    public string MaxWidthDescription => this[nameof (MaxWidthDescription)];

    /// <summary>The title of the configuration property 'MaxHeight'.</summary>
    [ResourceEntry("MaxHeightCaption", Description = "The title of the configuration property 'MaxHeight'.", LastModified = "2011/03/11", Value = "Maximal height")]
    public string MaxHeightCaption => this[nameof (MaxHeightCaption)];

    /// <summary>Describes the configuration property 'MaxHeight'.</summary>
    [ResourceEntry("MaxHeightDescription", Description = "Describes the configuration property 'MaxHeight'.", LastModified = "2011/03/11", Value = "The maximal height in pixels of the image.")]
    public string MaxHeightDescription => this[nameof (MaxHeightDescription)];

    /// <summary>
    /// The title of the configuration property 'ImageFieldMode'.
    /// </summary>
    [ResourceEntry("ImageFieldModeCaption", Description = "The title of the configuration property 'ImageFieldMode'.", LastModified = "2011/03/18", Value = "Image field mode")]
    public string ImageFieldModeCaption => this[nameof (ImageFieldModeCaption)];

    /// <summary>
    /// Describes the configuration property 'ImageFieldMode'.
    /// </summary>
    [ResourceEntry("ImageFieldModeDescription", Description = "Describes the configuration property 'ImageFieldMode'.", LastModified = "2011/03/18", Value = "The mode for the image field.")]
    public string ImageFieldModeDescription => this[nameof (ImageFieldModeDescription)];

    /// <summary>
    /// The title of the configuration property 'ProviderNameForDefaultImage'.
    /// </summary>
    [ResourceEntry("ProviderNameForDefaultImageCaption", Description = "The title of the configuration property 'ProviderNameForDefaultImage'.", LastModified = "2011/03/14", Value = "Provider name for the default image.")]
    public string ProviderNameForDefaultImageCaption => this[nameof (ProviderNameForDefaultImageCaption)];

    /// <summary>
    /// Describes the configuration property 'ProviderNameForDefaultImage'.
    /// </summary>
    [ResourceEntry("ProviderNameForDefaultImageDescription", Description = "Describes the configuration property 'ProviderNameForDefaultImage'.", LastModified = "2012/01/05", Value = "The name of the provider used for the default image. If null, will use the default provider for the system.")]
    public string ProviderNameForDefaultImageDescription => this[nameof (ProviderNameForDefaultImageDescription)];

    /// <summary>
    /// The title of the configuration property 'ProviderNameForDefaultImage'.
    /// </summary>
    [ResourceEntry("DefaultImageIdCaption", Description = "The title of the configuration property 'DefaultImageIdCaption'.", LastModified = "2011/03/14", Value = "Default image id.")]
    public string DefaultImageIdCaption => this[nameof (DefaultImageIdCaption)];

    /// <summary>
    /// Describes the configuration property 'DefaultImageIdDescription'.
    /// </summary>
    [ResourceEntry("DefaultImageIdDescription", Description = "Describes the configuration property 'DefaultImageIdDescription'.", LastModified = "2011/03/14", Value = "Id of the default image that is shown when the field is not bound to an image.")]
    public string DefaultImageIdDescription => this[nameof (DefaultImageIdDescription)];

    /// <summary>
    /// The title of the configuration property 'ImageProviderNameCaption'.
    /// </summary>
    [ResourceEntry("ImageProviderNameCaption", Description = "The title of the configuration property 'ImageProviderNameCaption'.", LastModified = "2011/03/14", Value = "Image provider name.")]
    public string ImageProviderNameCaption => this[nameof (ImageProviderNameCaption)];

    /// <summary>
    /// Describes the configuration property 'DefaultImageIdDescription'.
    /// </summary>
    [ResourceEntry("ImageProviderNameDescription", Description = "Describes the configuration property 'ImageProviderNameDescription'.", LastModified = "2011/03/14", Value = "Id of the default image that is shown when the field is not bound to an image.")]
    public string ImageProviderNameDescription => this[nameof (ImageProviderNameDescription)];

    /// <summary>
    /// The title of the configuration property 'DataFieldTypeCaption'.
    /// </summary>
    [ResourceEntry("DataFieldTypeCaption", Description = "The title of the configuration property 'DataFieldTypeCaption'.", LastModified = "2011/03/14", Value = "Image provider name.")]
    public string DataFieldTypeCaption => this[nameof (DataFieldTypeCaption)];

    /// <summary>
    /// Describes the configuration property 'DefaultImageIdDescription'.
    /// </summary>
    [ResourceEntry("DataFieldTypeDescription", Description = "Describes the configuration property 'DataFieldTypeDescription'.", LastModified = "2011/03/14", Value = "Id of the default image that is shown when the field is not bound to an image.")]
    public string DataFieldTypeDescription => this[nameof (DataFieldTypeDescription)];

    /// <summary>
    /// The title of the configuration property 'DataFieldTypeCaption'.
    /// </summary>
    [ResourceEntry("ImageFieldUploadModeCaption", Description = "The title of the configuration property 'ImageFieldUploadModeCaption'.", LastModified = "2011/03/18", Value = "Upload mode for the image field - input or dialog.")]
    public string ImageFieldUploadModeCaption => this[nameof (ImageFieldUploadModeCaption)];

    /// <summary>
    /// Describes the configuration property 'ImageFieldUploadModeDescription'.
    /// </summary>
    [ResourceEntry("ImageFieldUploadModeDescription", Description = "Describes the configuration property 'ImageFieldUploadModeDescription'.", LastModified = "2011/03/18", Value = " Gets or sets the upload mode of the image field - dialog with selector and async upload or input field.")]
    public string ImageFieldUploadModeDescription => this[nameof (ImageFieldUploadModeDescription)];

    /// <summary>
    /// The title of the configuration property 'DefaultSrcCaption'.
    /// </summary>
    [ResourceEntry("DefaultSrcCaption", Description = "The title of the configuration property 'DefaultSrcCaption'.", LastModified = "2012/01/05", Value = "Default Source")]
    public string DefaultSrcCaption => this[nameof (DefaultSrcCaption)];

    /// <summary>
    /// Describes the configuration property 'DefaultSrcDescription'.
    /// </summary>
    [ResourceEntry("DefaultSrcDescription", Description = "Describes the configuration property 'DefaultSrcDescription'.", LastModified = "2012/01/05", Value = "The default source for the image.")]
    public string DefaultSrcDescription => this[nameof (DefaultSrcDescription)];

    /// <summary>
    /// The title of the configuration property 'SizeInPxCaption'.
    /// </summary>
    [ResourceEntry("SizeInPxCaption", Description = "The title of the configuration property 'SizeInPxCaption'.", LastModified = "2011/05/20", Value = "Size in pixels")]
    public string SizeInPxCaption => this[nameof (SizeInPxCaption)];

    /// <summary>
    /// Describes the configuration property 'SizeInPxDescription'.
    /// </summary>
    [ResourceEntry("SizeInPxDescription", Description = "Describes the configuration property 'SizeInPxDescription'.", LastModified = "2012/01/05", Value = "The size of the img tag in pixels. This is used for the size of the smaller of the two dimensions of the image (width or height).")]
    public string SizeInPxDescription => this[nameof (SizeInPxDescription)];

    /// <summary>
    /// The title of the configuration property 'DisplayEmptyImageCaption'.
    /// </summary>
    [ResourceEntry("DisplayEmptyImageCaption", Description = "The title of the configuration property 'DisplayEmptyImageCaption'.", LastModified = "2011/05/20", Value = "Display empty image")]
    public string DisplayEmptyImageCaption => this[nameof (DisplayEmptyImageCaption)];

    /// <summary>
    /// Describes the configuration property 'DisplayEmptyImageDescription'.
    /// </summary>
    [ResourceEntry("DisplayEmptyImageDescription", Description = "Describes the configuration property 'DisplayEmptyImageDescription'.", LastModified = "2011/05/20", Value = "Gets or sets whether to display the image placeholder. The default behavior is to display the placeholder.")]
    public string DisplayEmptyImageDescription => this[nameof (DisplayEmptyImageDescription)];

    /// <summary>
    /// The title of the configuration property 'HeaderTextCaption'.
    /// </summary>
    [ResourceEntry("HeaderTextCaption", Description = "The title of the configuration property 'HeaderTextCaption'.", LastModified = "2011/05/20", Value = "Header text")]
    public string HeaderTextCaption => this[nameof (HeaderTextCaption)];

    /// <summary>
    /// Describes the configuration property 'HeaderTextDescription'.
    /// </summary>
    [ResourceEntry("HeaderTextDescription", Description = "Describes the configuration property 'HeaderTextDescription'.", LastModified = "2011/05/20", Value = "Gets or sets the text displayed at the top of the control. Not setting this value will make the header not appear at the top of the control.")]
    public string HeaderTextDescription => this[nameof (HeaderTextDescription)];

    /// <summary>Phrase: Parent item type</summary>
    [ResourceEntry("ParentSelectorItemsTypeTitle", Description = "Phrase: Parent item type", LastModified = "2012/07/03", Value = "Parent item type")]
    public string ParentSelectorItemsTypeTitle => this[nameof (ParentSelectorItemsTypeTitle)];

    /// <summary>
    /// Phrase: The type of the items that are displayed by the selector
    /// </summary>
    [ResourceEntry("ParentSelectorItemsTypeDescription", Description = "Phrase: The type of the items that are displayed by the selector", LastModified = "2012/07/03", Value = "The type of the items that are displayed by the selector")]
    public string ParentSelectorItemsTypeDescription => this[nameof (ParentSelectorItemsTypeDescription)];

    /// <summary>Phrase: Web service url</summary>
    [ResourceEntry("ParentSelectorWebServiceUrlTitle", Description = "word: Web service url", LastModified = "2012/07/03", Value = "Web service url")]
    public string ParentSelectorWebServiceUrlTitle => this[nameof (ParentSelectorWebServiceUrlTitle)];

    /// <summary>Phrase: The url of the web service used for binding</summary>
    [ResourceEntry("ParentSelectorWebServiceUrlDescription", Description = "word: The url of the web service used for binding", LastModified = "2012/07/03", Value = "The url of the web service used for binding")]
    public string ParentSelectorWebServiceUrlDescription => this[nameof (ParentSelectorWebServiceUrlDescription)];

    /// <summary>Phrase: The main field of the items type</summary>
    [ResourceEntry("ParentSelectorMainFieldTitle", Description = "word: The main field of the items type", LastModified = "2012/07/03", Value = "The main field of the items type")]
    public string ParentSelectorMainFieldTitle => this[nameof (ParentSelectorMainFieldTitle)];

    /// <summary>
    /// Phrase: The main field of the items that is displayed in the selector
    /// </summary>
    [ResourceEntry("ParentSelectorMainFieldDescription", Description = "word: The main field of the items that is displayed in the selector", LastModified = "2012/07/03", Value = "The main field of the items that is displayed in the selector")]
    public string ParentSelectorMainFieldDescription => this[nameof (ParentSelectorMainFieldDescription)];

    /// <summary>Phrase: The data key names</summary>
    [ResourceEntry("ParentSelectorDataKeyNamesTitle", Description = "word: The data key names", LastModified = "2012/07/06", Value = "The data key names")]
    public string ParentSelectorDataKeyNamesTitle => this[nameof (ParentSelectorDataKeyNamesTitle)];

    /// <summary>
    /// Phrase: The data key names used by the selector data source
    /// </summary>
    [ResourceEntry("ParentSelectorDataKeyNamesDescription", Description = "word: The data key names used by the selector data source", LastModified = "2012/07/06", Value = "The data key names used by the selector data source")]
    public string ParentSelectorDataKeyNamesDescription => this[nameof (ParentSelectorDataKeyNamesDescription)];

    /// <summary>Phrase: The allow searching value</summary>
    [ResourceEntry("ParentSelectorAllowSearchingTitle", Description = "Phrase: The allow searching value", LastModified = "2012/07/06", Value = "The allow searching value")]
    public string ParentSelectorAllowSearchingTitle => this[nameof (ParentSelectorAllowSearchingTitle)];

    /// <summary>
    /// Phrase: Determining whether items in selector can be searched
    /// </summary>
    [ResourceEntry("ParentSelectorAllowSearchingDescription", Description = "Phrase: Determining whether items in selector can be searched", LastModified = "2012/07/06", Value = "Determining whether items in selector can be searched")]
    public string ParentSelectorAllowSearchingDescription => this[nameof (ParentSelectorAllowSearchingDescription)];

    /// <summary>
    /// Message: The text that appears for No parent library option.
    /// </summary>
    /// <value>ShouldNotCreateTemplateForMasterPageCaption</value>
    [ResourceEntry("NoParentLibCaption", Description = "The text that appears for No parent library option.", LastModified = "2013/02/28", Value = "The text that appears for No parent library option.")]
    public string NoParentLibCaption => this[nameof (NoParentLibCaption)];

    /// <summary>
    /// phrase: The text that appears for No parent library option.
    /// </summary>
    [ResourceEntry("NoParentLibDescription", Description = "phrase: The text that appears for No parent library option.", LastModified = "2013/02/28", Value = "The text that appears for No parent library option.")]
    public string NoParentLibDescription => this[nameof (NoParentLibDescription)];

    /// <summary>
    /// Message: The text that appears for Select parent library option.
    /// </summary>
    /// <value>ShouldNotCreateTemplateForMasterPageCaption</value>
    [ResourceEntry("SelectedParentLibCaption", Description = "The text that appears for Select parent library option.", LastModified = "2013/02/28", Value = "The text that appears for Select parent library option.")]
    public string SelectedParentLibCaption => this[nameof (SelectedParentLibCaption)];

    /// <summary>
    /// phrase: The text that appears for Select parent library option.
    /// </summary>
    [ResourceEntry("SelectedParentLibDescription", Description = "phrase: The text that appears for Select parent library option.", LastModified = "2013/02/28", Value = "The text that appears for Select parent library option.")]
    public string SelectedParentLibDescription => this[nameof (SelectedParentLibDescription)];

    /// <summary>Message: The title of the library item name.</summary>
    /// <value>ShouldNotCreateTemplateForMasterPageCaption</value>
    [ResourceEntry("LibraryItemNameCaption", Description = "The title of the library item name.", LastModified = "2013/02/28", Value = "The title of the library item name.")]
    public string LibraryItemNameCaption => this[nameof (LibraryItemNameCaption)];

    /// <summary>phrase: The title of the library item name.</summary>
    [ResourceEntry("LibraryItemNameDescription", Description = "phrase: The title of the library item name.", LastModified = "2013/02/28", Value = "The title of the library item name.")]
    public string LibraryItemNameDescription => this[nameof (LibraryItemNameDescription)];

    /// <summary>EditorConfigurations</summary>
    [ResourceEntry("EditorConfigurationsTitle", Description = "The title of the configuration element.", LastModified = "2010/02/23", Value = "EditorConfigurations")]
    public string EditorConfigurationsTitle => this[nameof (EditorConfigurationsTitle)];

    /// <summary>Represents EditorConfigurations property collection.</summary>
    [ResourceEntry("EditorConfigurationsDescription", Description = "Describes configuration element", LastModified = "2010/02/23", Value = "Represents EditorConfigurations property collection.")]
    public string EditorConfigurationsDescription => this[nameof (EditorConfigurationsDescription)];

    /// <summary>phrase: Frontend themes</summary>
    [ResourceEntry("FrontendThemesTitle", Description = "Title of the configuration element", LastModified = "2010/07/12", Value = "Frontend themes")]
    public string FrontendThemesTitle => this[nameof (FrontendThemesTitle)];

    /// <summary>phrase: For Hybrid and Web Forms templates</summary>
    [ResourceEntry("FrontendThemesDescription", Description = "For Hybrid and Web Forms templates", LastModified = "2019/02/27", Value = "For Hybrid and Web Forms templates")]
    public string FrontendThemesDescription => this[nameof (FrontendThemesDescription)];

    /// <summary>phrase: Backend themes</summary>
    [ResourceEntry("BackendThemesTitle", Description = "Title of the configuration element", LastModified = "2010/07/12", Value = "Backend themes")]
    public string BackendThemesTitle => this[nameof (BackendThemesTitle)];

    /// <summary>phrase: Provides the collection of backend themes</summary>
    [ResourceEntry("BackendThemesDescription", Description = "Describes the configuration element", LastModified = "2010/07/12", Value = "Provides the collection of backend themes")]
    public string BackendThemesDescription => this[nameof (BackendThemesDescription)];

    /// <summary>phrase: Rad Editor's content area CSS file</summary>
    [ResourceEntry("EditorContentAreaCssFileCaption", Description = "Title of the configuration element", LastModified = "2010/10/21", Value = "Rad Editor's content area CSS file")]
    public string EditorContentAreaCssFileCaption => this[nameof (EditorContentAreaCssFileCaption)];

    /// <summary>
    /// phrase: Gets or sets a string, containing the location of the Rad Editor's content area CSS styles.
    /// </summary>
    [ResourceEntry("EditorContentAreaCssFileDescription", Description = "Describes the configuration element", LastModified = "2010/10/21", Value = "Gets or sets a string, containing the location of the Rad Editor's content area CSS styles.")]
    public string EditorContentAreaCssFileDescription => this[nameof (EditorContentAreaCssFileDescription)];

    /// <summary>phrase: HTML field edit modes</summary>
    [ResourceEntry("HtmlFieldEditModesCaption", Description = "phrase: HTML field edit modes", LastModified = "2010/10/22", Value = "HTML field edit modes")]
    public string HtmlFieldEditModesCaption => this[nameof (HtmlFieldEditModesCaption)];

    /// <summary>
    /// phrase: A value indicating in which mode the RadEditor loads (HTML/Design/Preview/All).
    /// </summary>
    [ResourceEntry("HtmlFieldEditModesDescription", Description = "phrase: A value indicating in which mode the RadEditor loads (HTML/Design/Preview/All).", LastModified = "2010/10/22", Value = "A value indicating in which mode the RadEditor loads (HTML/Design/Preview/All).")]
    public string HtmlFieldEditModesDescription => this[nameof (HtmlFieldEditModesDescription)];

    /// <summary>phrase: ContentBlock widget HtmlField filters</summary>
    [ResourceEntry("ContentBlockFiltersCaption", Description = "Title of the configuration element", LastModified = "2012/05/17", Value = "RadEditor filters for Content block widget")]
    public string ContentBlockFiltersCaption => this["EditorContentFiltersCaption"];

    /// <summary>
    /// phrase: Gets or sets a value indicating which content filters will be active when the
    /// RadEditor is loaded in the browser.
    /// </summary>
    [ResourceEntry("ContentBlockFiltersDescription", Description = "Describes the configuration element", LastModified = "2012/05/17", Value = "Gets or sets a value indicating which content filters will be active when RadEditor is initialized inside Content Block widget")]
    public string ContentBlockFiltersDescription => this["EditorContentFiltersDescription"];

    /// <summary>phrase: Rad Editor's CSS class file</summary>
    [ResourceEntry("EditorCssClassCaption", Description = "Title of the configuration element", LastModified = "2010/10/26", Value = "Rad Editor's CSS class file")]
    public string EditorCssClassCaption => this[nameof (EditorCssClassCaption)];

    /// <summary>phrase:  Gets or sets the CSS class of the RadEditor.</summary>
    [ResourceEntry("EditorCssClassDescription", Description = "Describes the configuration element", LastModified = "2010/10/26", Value = " Gets or sets the CSS class of the RadEditor.")]
    public string EditorCssClassDescription => this[nameof (EditorCssClassDescription)];

    /// <summary>phrase: Rad Editor's skin</summary>
    [ResourceEntry("EditorSkinCaption", Description = "Title of the configuration element", LastModified = "2010/10/26", Value = "Rad Editor's skin")]
    public string EditorSkinCaption => this[nameof (EditorSkinCaption)];

    /// <summary>
    /// phrase: Gets or sets the skin name for the RadEditor control user interface.
    /// </summary>
    [ResourceEntry("EditorSkinDescription", Description = "Describes the configuration element", LastModified = "2010/10/26", Value = "Gets or sets the skin name for the RadEditor control user interface.")]
    public string EditorSkinDescription => this[nameof (EditorSkinDescription)];

    /// <summary>phrase: Rad Editor's content filters</summary>
    [ResourceEntry("EditorContentFiltersCaption", Description = "Title of the configuration element", LastModified = "2010/10/26", Value = "Rad Editor's content filters")]
    public string EditorContentFiltersCaption => this[nameof (EditorContentFiltersCaption)];

    /// <summary>
    /// phrase: Gets or sets a value indicating which content filters will be active when the
    /// RadEditor is loaded in the browser.
    /// </summary>
    [ResourceEntry("EditorContentFiltersDescription", Description = "Describes the configuration element", LastModified = "2010/10/26", Value = "Gets or sets a value indicating which content filters will be active when the RadEditor is loaded in the browser.")]
    public string EditorContentFiltersDescription => this[nameof (EditorContentFiltersDescription)];

    /// <summary>phrase: Rad Editor new line 'br' tags</summary>
    [ResourceEntry("EditorNewLineBrCaption", Description = "Title of the configuration element", LastModified = "2010/10/26", Value = "Rad Editor new line 'br' tags")]
    public string EditorNewLineBrCaption => this[nameof (EditorNewLineBrCaption)];

    /// <summary>
    /// phrase: Gets or sets the value indicating whether the RadEditor will insert a new line
    /// or
    /// a paragraph when the [Enter] key is pressed.
    /// </summary>
    [ResourceEntry("EditorNewLineBrDescription", Description = "Describes the configuration element", LastModified = "2010/10/26", Value = "Gets or sets the value indicating whether the RadEditor will insert a new line or a paragraph when the [Enter] key is pressed.")]
    public string EditorNewLineBrDescription => this[nameof (EditorNewLineBrDescription)];

    /// <summary>phrase: Rad Editor strip formatting on paste</summary>
    [ResourceEntry("EditorStripFormattingOnPasteCaption", Description = "Title of the configuration element", LastModified = "2010/10/26", Value = "Rad Editor strip formatting on paste")]
    public string EditorStripFormattingOnPasteCaption => this[nameof (EditorStripFormattingOnPasteCaption)];

    /// <summary>
    /// phrase: This property is obsolete. Please, use the StripFormattingOptions property instead.
    /// </summary>
    [ResourceEntry("EditorStripFormattingOnPasteDescription", Description = "Describes the configuration element", LastModified = "2010/10/26", Value = "This property is obsolete. Please, use the StripFormattingOptions property instead.")]
    public string EditorStripFormattingOnPasteDescription => this[nameof (EditorStripFormattingOnPasteDescription)];

    /// <summary>phrase: Rad Editor strip formatting options</summary>
    [ResourceEntry("EditorStripFormattingOptionsCaption", Description = "Title of the configuration element", LastModified = "2010/10/26", Value = "Rad Editor strip formatting options")]
    public string EditorStripFormattingOptionsCaption => this[nameof (EditorStripFormattingOptionsCaption)];

    /// <summary>
    /// phrase: Gets or sets the value indicating how the RadEditor should clear the HTML formatting when the user pastes data into the content area.
    /// </summary>
    [ResourceEntry("EditorStripFormattingOptionsDescription", Description = "Describes the configuration element", LastModified = "2010/10/26", Value = "Gets or sets the value indicating how the RadEditor should clear the HTML formatting when the user pastes data into the content area.")]
    public string EditorStripFormattingOptionsDescription => this[nameof (EditorStripFormattingOptionsDescription)];

    /// <summary>phrase: Rad Editor toolset configuration</summary>
    [ResourceEntry("EditorToolsConfigurationCaption", Description = "Title of the configuration element", LastModified = "2011/12/07", Value = "Rad Editor toolset configuration")]
    public string EditorToolsConfigurationCaption => this[nameof (EditorToolsConfigurationCaption)];

    /// <summary>
    /// phrase: "Gets or sets the toolset loaded in the editor. Standard loads the StandardEditorConfiguration toolset. Minimal loads the MinimalEditorConfiguration toolset. Custom loads a custom toolset defined by the EditorToolsConfigurationKey property value."
    /// </summary>
    [ResourceEntry("EditorToolsConfigurationDescription", Description = "Describes the configuration element", LastModified = "2011/12/06", Value = "Gets or sets the toolset loaded in the editor. Standard loads the StandardEditorConfiguration toolset. Minimal loads the MinimalEditorConfiguration toolset. Custom loads a custom toolset defined by the EditorToolsConfigurationKey property value.")]
    public string EditorToolsConfigurationDescription => this[nameof (EditorToolsConfigurationDescription)];

    /// <summary>phrase: Rad Editor custom toolset configuration key.</summary>
    [ResourceEntry("EditorToolsConfigurationKeyCaption", Description = "Title of the configuration element", LastModified = "2011/12/06", Value = "Rad Editor custom toolset configuration key")]
    public string EditorToolsConfigurationKeyCaption => this[nameof (EditorToolsConfigurationKeyCaption)];

    /// <summary>
    /// phrase: Gets or sets the key for searching a custom toolset configuration, when Rad Editor tools configuration is set to Custom.
    /// </summary>
    [ResourceEntry("EditorToolsConfigurationKeyDescription", Description = "Describes the configuration element", LastModified = "2011/12/06", Value = "Gets or sets the key for searching a custom toolset configuration, when Rad Editor tools configuration is set to Custom")]
    public string EditorToolsConfigurationKeyDescription => this[nameof (EditorToolsConfigurationKeyDescription)];

    /// <summary>phrase: Status commands</summary>
    [ResourceEntry("StatusCommandsTitle", Description = "Title of the configuration element", LastModified = "2010/07/12", Value = "Status commands")]
    public string StatusCommandsTitle => this[nameof (StatusCommandsTitle)];

    /// <summary>phrase: Provides the collection of status commands</summary>
    [ResourceEntry("StatusCommandsDescription", Description = "Describes the configuration element", LastModified = "2010/07/12", Value = "Provides the collection of status commands")]
    public string StatusCommandsDescription => this[nameof (StatusCommandsDescription)];

    /// <summary>StandardEditorConfiguration</summary>
    [ResourceEntry("StandardEditorConfigurationTitle", Description = "The title of the configuration element.", LastModified = "2010/02/23", Value = "StandardEditorConfiguration")]
    public string StandardEditorConfigurationTitle => this[nameof (StandardEditorConfigurationTitle)];

    /// <summary>
    /// Gets or sets the default tool set used in all backend content types."
    /// </summary>
    [ResourceEntry("StandardEditorConfigurationDescription", Description = "Describes configuration element", LastModified = "2010/02/23", Value = "Gets or sets the default tool set used in all backend content types.")]
    public string StandardEditorConfigurationDescription => this[nameof (StandardEditorConfigurationDescription)];

    /// <summary>ForumsEditorConfiguration</summary>
    [ResourceEntry("ForumsEditorConfigurationTitle", Description = "The title of the configuration element.", LastModified = "2012/01/23", Value = "ForumsEditorConfigurationTitle")]
    public string ForumsEditorConfigurationTitle => this[nameof (ForumsEditorConfigurationTitle)];

    /// <summary>
    /// Gets or sets the default tool set used in Forums content type."
    /// </summary>
    [ResourceEntry("ForumsEditorConfigurationDescription", Description = "Describes configuration element", LastModified = "2012/01/23", Value = "Gets or sets the default tool set used in Forums content type.")]
    public string ForumsEditorConfigurationDescription => this[nameof (ForumsEditorConfigurationDescription)];

    /// <summary>MinimalEditorConfiguration</summary>
    [ResourceEntry("MinimalEditorConfigurationTitle", Description = "The title of the configuration element.", LastModified = "2010/02/23", Value = "MinimalEditorConfiguration")]
    public string MinimalEditorConfigurationTitle => this[nameof (MinimalEditorConfigurationTitle)];

    /// <summary>
    /// Gets or sets the tool set used in the public site where there are comments."
    /// </summary>
    [ResourceEntry("MinimalEditorConfigurationDescription", Description = "Describes configuration element", LastModified = "2010/02/23", Value = "Gets or sets the tool set used in the public site where there are comments.")]
    public string MinimalEditorConfigurationDescription => this[nameof (MinimalEditorConfigurationDescription)];

    /// <summary>
    /// Gets or sets the time duration before the web resources used in themes expire. The value is in minutes.
    /// </summary>
    [ResourceEntry("FrontendResourcesCacheDurationDescription", Description = "Describes configuration element.", LastModified = "2011/02/28", Value = "Gets or sets the time duration before the web resources used in themes expire. The value is in minutes.")]
    public string FrontendResourcesCacheDurationDescription => this[nameof (FrontendResourcesCacheDurationDescription)];

    /// <summary>Resource strings for QueryBuilderConfig class.</summary>
    [ResourceEntry("QueryBuilderConfigCaption", Description = "The title of this class.", LastModified = "2010/02/22", Value = "QueryBuilderConfig class")]
    public string QueryBuilderConfigCaption => this[nameof (QueryBuilderConfigCaption)];

    /// <summary>Resource strings for QueryBuilderConfig class.</summary>
    [ResourceEntry("QueryBuilderConfigDescription", Description = "The description of this class.", LastModified = "2010/02/22", Value = "Resource strings for QueryBuilderConfig class.")]
    public string QueryBuilderConfigDescription => this[nameof (QueryBuilderConfigDescription)];

    /// <summary>Resource strings for QueryBuilderConfig class.</summary>
    [ResourceEntry("LicenseConfigCaption", Description = "Licensing configuration caption", LastModified = "2010/03/23", Value = "Licensing")]
    public string LicenseConfigCaption => this[nameof (LicenseConfigCaption)];

    /// <summary>Resource strings for QueryBuilderConfig class.</summary>
    [ResourceEntry("LicenseConfigDescription", Description = "Licensing configuration description.", LastModified = "2010/02/22", Value = "Defines configuration settings for Licensing")]
    public string LicenseConfigDescription => this[nameof (LicenseConfigDescription)];

    /// <summary>Resource strings for QueryBuilderConfig class.</summary>
    [ResourceEntry("LicenseServiceUrl", Description = "Property caption", LastModified = "2010/03/23", Value = "the address of the license service")]
    public string LicenseServiceUrl => this[nameof (LicenseServiceUrl)];

    /// <summary>Property caption.</summary>
    [ResourceEntry("LicenseServiceConnectionTimeOut", Description = "Property caption.", LastModified = "2010/03/23", Value = "Update service connection time out")]
    public string LicenseServiceConnectionTimeOut => this[nameof (LicenseServiceConnectionTimeOut)];

    /// <summary>Licensing configuration.</summary>
    [ResourceEntry("Licensing", Description = "Licensing configuration.", LastModified = "2011/03/11", Value = "Licensing configuration")]
    public string Licensing => this[nameof (Licensing)];

    /// <summary>License providers</summary>
    [ResourceEntry("LicenseProvidersTitle", Description = "License providers", LastModified = "2020/06/22", Value = "License providers")]
    public string LicenseProvidersTitle => this[nameof (LicenseProvidersTitle)];

    /// <summary>A collection of defined license providers.</summary>
    [ResourceEntry("LicenseProvidersDescription", Description = "A collection of defined license providers.", LastModified = "2020/06/22", Value = "A collection of defined license providers.")]
    public string LicenseProvidersDescription => this[nameof (LicenseProvidersDescription)];

    /// <summary>License provider</summary>
    [ResourceEntry("LicenseProviderTitle", Description = "License provider", LastModified = "2020/06/22", Value = "License provider")]
    public string LicenseProviderTitle => this[nameof (LicenseProviderTitle)];

    /// <summary>Name</summary>
    [ResourceEntry("LicenseProviderNameTitle", Description = "Name", LastModified = "2020/06/22", Value = "Name")]
    public string LicenseProviderNameTitle => this[nameof (LicenseProviderNameTitle)];

    /// <summary>Unique name for the provider, used as a key.</summary>
    [ResourceEntry("LicenseProviderNameDescription", Description = "Unique name for the provider, used as a key.", LastModified = "2020/06/22", Value = "Unique name for the provider, used as a key.")]
    public string LicenseProviderNameDescription => this[nameof (LicenseProviderNameDescription)];

    /// <summary>Provider type</summary>
    [ResourceEntry("LicenseProviderTypeTitle", Description = "Provider type", LastModified = "2020/06/22", Value = "Provider type")]
    public string LicenseProviderTypeTitle => this[nameof (LicenseProviderTypeTitle)];

    /// <summary>
    /// The full name of the .NET class that implements this license provider.
    /// </summary>
    [ResourceEntry("LicenseProviderTypeDescription", Description = "The full name of the .NET class that implements this license provider.", LastModified = "2020/06/22", Value = "The full name of the .NET class that implements this license provider.")]
    public string LicenseProviderTypeDescription => this[nameof (LicenseProviderTypeDescription)];

    /// <summary>Default license provider</summary>
    [ResourceEntry("DefaultLicenseProviderTitle", Description = "Default license provider", LastModified = "2020/06/22", Value = "Default license provider")]
    public string DefaultLicenseProviderTitle => this[nameof (DefaultLicenseProviderTitle)];

    /// <summary>
    /// The name (key) of the default license provider in case there are multiple providers.
    /// </summary>
    [ResourceEntry("DefaultLicenseProviderDescription", Description = "The name (key) of the default license provider in case there are multiple providers.", LastModified = "2020/06/22", Value = "The name (key) of the default license provider in case there are multiple providers.")]
    public string DefaultLicenseProviderDescription => this[nameof (DefaultLicenseProviderDescription)];

    /// <summary>SortingExpressionSettings</summary>
    [ResourceEntry("SortingExpressionSettingsTitle", Description = "The title of the configuration element.", LastModified = "2010/02/23", Value = "SortingExpressionSettings")]
    public string SortingExpressionSettingsTitle => this[nameof (SortingExpressionSettingsTitle)];

    /// <summary>Represents SortingExpression property collection.</summary>
    [ResourceEntry("SortingExpressionSettingsDescription", Description = "Describes configuration element", LastModified = "2010/02/23", Value = "Represents SortingExpression property collection.")]
    public string SortingExpressionSettingsDescription => this[nameof (SortingExpressionSettingsDescription)];

    /// <summary>Resource strings for SortingExpressionElement class.</summary>
    [ResourceEntry("SortingExpressionTitle", Description = "The title of sorting expression", LastModified = "2010/04/23", Value = "The title of sorting expression")]
    public string SortingExpressionTitle => this[nameof (SortingExpressionTitle)];

    /// <summary>Resource strings for SortingExpressionElement class.</summary>
    [ResourceEntry("SortingExpressionTitleDescription", Description = "The title of sorting expression configuration description.", LastModified = "2010/04/23", Value = "The title of sorting expression configuration description")]
    public string SortingExpressionTitleDescription => this[nameof (SortingExpressionTitleDescription)];

    /// <summary>phrase: Sorting expression</summary>
    [ResourceEntry("SortingExpressionElementCaption", Description = "phrase: Sorting expression", LastModified = "2010/10/22", Value = "Sorting expression")]
    public string SortingExpressionElementCaption => this[nameof (SortingExpressionElementCaption)];

    /// <summary>phrase: Sorting expression configuration description.</summary>
    [ResourceEntry("SortingExpressionElementDescription", Description = "phrase: Sorting expression configuration description.", LastModified = "2010/04/23", Value = "Sorting expression configuration data")]
    public string SortingExpressionElementDescription => this[nameof (SortingExpressionElementDescription)];

    /// <summary>
    /// phrase: A value indicating if sorting expression is custom.
    /// </summary>
    [ResourceEntry("IsCustomTitle", Description = "phrase: A value indicating if sorting expression is custom.", LastModified = "2010/04/27", Value = "A value indicating if sorting expression is custom.")]
    public string IsCustomTitle => this[nameof (IsCustomTitle)];

    /// <summary>
    /// phrase: A value indicating if sorting expression is custom.
    /// </summary>
    [ResourceEntry("IsCustomDescription", Description = "phrase: A value indicating if sorting expression is custom.", LastModified = "2010/04/27", Value = "A value indicating if sorting expression is custom.")]
    public string IsCustomDescription => this["IsCustomTitle"];

    /// <summary>phrase: The sorting expression value.</summary>
    [ResourceEntry("SortingExpressionValueTitle", Description = "phrase: The sorting expression value.", LastModified = "2010/04/27", Value = "The sorting expression value.")]
    public string SortingExpressionValueTitle => this[nameof (SortingExpressionValueTitle)];

    /// <summary>
    /// phrase: The value of sorting expression configuration data.
    /// </summary>
    [ResourceEntry("SortingExpressionValueDescription", Description = "phrase: The value of sorting expression configuration data.", LastModified = "2010/04/27", Value = "The value of sorting expression configuration data.")]
    public string SortingExpressionValueDescription => this[nameof (SortingExpressionValueDescription)];

    /// <summary>Resource strings for SortingExpressionElement class.</summary>
    [ResourceEntry("ContentTypeTitle", Description = "The title of content type for the sorting expression", LastModified = "2010/04/23", Value = "The title of content type for the sorting expression")]
    public string ContentTypeTitle => this[nameof (ContentTypeTitle)];

    /// <summary>Resource strings for SortingExpressionElement class.</summary>
    [ResourceEntry("ContentTypeDescription", Description = "The content type of sorting expression configuration data", LastModified = "2010/04/23", Value = "The content type of sorting expression configuration data")]
    public string ContentTypeDescription => this["SortingExpressionElementDescription"];

    /// <summary>Translated message, similar to "SMTP"</summary>
    /// <value>SMTP (Email) Settings for Sitefinity</value>
    [ResourceEntry("SmtpConfigCaption", Description = "SMTP (Email) Settings for Sitefinity", LastModified = "2010/03/29", Value = "SMTP (Email Settings)")]
    public string SmtpConfigCaption => this[nameof (SmtpConfigCaption)];

    /// <summary>
    /// Translated message, similar to "provides a set of configuration options that Sitefinity will use to send emails."
    /// </summary>
    /// <value>Describes the purpose of the SMTP configuration element.</value>
    [ResourceEntry("SmtpConfigDescription", Description = "Describes the purpose of the SMTP configuration element.", LastModified = "2010/03/29", Value = "Provides a set of configuration options that Sitefinity will use to send emails.")]
    public string SmtpConfigDescription => this[nameof (SmtpConfigDescription)];

    /// <summary>
    /// Translated message, similar to "Gets or sets the name or IP address of the host used for SMTP transactions."
    /// </summary>
    [ResourceEntry("SmtpHost", Description = "Property Caption", LastModified = "2010/03/29", Value = "Gets or sets the name or IP address of the host used for SMTP transactions.")]
    public string SmtpHost => this[nameof (SmtpHost)];

    /// <summary>phrase: Email settings</summary>
    [ResourceEntry("EmailSettings", Description = "phrase: Email settings", LastModified = "2019/01/21", Value = "Email settings")]
    public string EmailSettings => this[nameof (EmailSettings)];

    /// <summary>phrase: FAQ</summary>
    [ResourceEntry("EmailSettingsBasicSettingsFaqTitle", Description = "phrase: FAQ", LastModified = "2019/01/21", Value = "FAQ")]
    public string EmailSettingsBasicSettingsFaqTitle => this[nameof (EmailSettingsBasicSettingsFaqTitle)];

    /// <summary>
    /// phrase: How to setup external mail services (e.g. Sendgrid, Mailchimp, Mailgun)?
    /// </summary>
    [ResourceEntry("EmailSettingsBasicSettingsFaqQuestion", Description = "phrase: How to setup external mail services (e.g. Sendgrid, Mailchimp, Mailgun)?", LastModified = "2019/01/21", Value = "How to setup external mail services (e.g. Sendgrid, Mailchimp, Mailgun)?")]
    public string EmailSettingsBasicSettingsFaqQuestion => this[nameof (EmailSettingsBasicSettingsFaqQuestion)];

    /// <summary>
    /// phrase: Follow the instructions for setting up the most popular mail services:
    /// </summary>
    [ResourceEntry("EmailSettingsBasicSettingsFaqAnswer", Description = "phrase: Follow the instructions for setting up the most popular mail services:", LastModified = "2019/01/21", Value = "Follow the instructions for setting up the most popular mail services:\r\n                            <ul>\r\n                                <li><a target='_blank' href='https://www.progress.com/documentation/sitefinity-cms/predefined-notification-profiles#sendgrid'> Sendgrid </a></li>\r\n                                <li><a target='_blank' href='https://www.progress.com/documentation/sitefinity-cms/predefined-notification-profiles#mandrill'> Mandrill </a></li>\r\n                                <li><a target='_blank' href='https://www.progress.com/documentation/sitefinity-cms/predefined-notification-profiles#mailgun'> Mailgun </a></li>\r\n                                <li><a target='_blank' href='https://www.progress.com/documentation/sitefinity-cms/predefined-notification-profiles#configure-other-external-email-services '> Gmail </a></li>\r\n                            </ul>")]
    public string EmailSettingsBasicSettingsFaqAnswer => this[nameof (EmailSettingsBasicSettingsFaqAnswer)];

    /// <summary>
    /// phrase: The default SMTP settings used by Email campaigns and all Sitefinity CMS components that send email notifications (e.g. Comments, Forums, Forms). If you need different settings for a specific content type, learn <a target="_blank" href="https://www.progress.com/documentation/sitefinity-cms/administration-configure-notification-profiles">how to setup notification profiles</a>.
    /// </summary>
    [ResourceEntry("EmailSettingsBasicSettingsInfo", Description = "phrase: The default SMTP settings used by Email campaigns and all Sitefinity CMS components that send email notifications (e.g. Comments, Forums, Forms). If you need different settings for a specific content type, learn <a target='_blank' href='https://www.progress.com/documentation/sitefinity-cms/administration-configure-notification-profiles'>how to setup notification profiles</a>.", LastModified = "2019/01/21", Value = "The default SMTP settings used by Email campaigns and all Sitefinity CMS components that send email notifications (e.g. Comments, Forums, Forms). If you need different settings for a specific content type, learn <a target='_blank' href='https://www.progress.com/documentation/sitefinity-cms/administration-configure-notification-profiles'>how to setup notification profiles</a>.")]
    public string EmailSettingsBasicSettingsInfo => this[nameof (EmailSettingsBasicSettingsInfo)];

    /// <summary>phrase: Email address</summary>
    [ResourceEntry("EmailAddress", Description = "phrase: Email address", LastModified = "2019/01/22", Value = "Email address")]
    public string EmailAddress => this[nameof (EmailAddress)];

    /// <summary>phrase: Send test</summary>
    [ResourceEntry("SendTest", Description = "phrase: Send test", LastModified = "2019/01/21", Value = "Send test")]
    public string SendTest => this[nameof (SendTest)];

    /// <summary>phrase: Send test email</summary>
    [ResourceEntry("SendTestEmail", Description = "phrase: Send test email", LastModified = "2019/01/21", Value = "Send test email")]
    public string SendTestEmail => this[nameof (SendTestEmail)];

    /// <summary>
    /// phrase: You can enter multiple addresses separated by comma
    /// </summary>
    [ResourceEntry("SendTestHint", Description = "phrase: You can enter multiple addresses separated by comma", LastModified = "2019/01/22", Value = "You can enter multiple addresses separated by comma")]
    public string SendTestHint => this[nameof (SendTestHint)];

    /// <summary>phrase: Email cannot be sent</summary>
    [ResourceEntry("EmailCannotBeSent", Description = "phrase: Email cannot be sent", LastModified = "2019/07/03", Value = "Email cannot be sent")]
    public string EmailCannotBeSent => this[nameof (EmailCannotBeSent)];

    [ResourceEntry("EmailCannotBeSentDescription", Description = "phrase: <span class=\"sfDisplayBlock\">The system is not configured to send emails.</span><span class=\"sfDisplayBlock\"><a target=\"_blank\" href=\"{0}\">Configure email settings</a> or ask your administrator for assistance.</span>", LastModified = "2019/07/03", Value = "<span class=\"sfDisplayBlock\">The system is not configured to send emails.</span><span class=\"sfDisplayBlock\"><a target=\"_blank\" href=\"{0}\">Configure email settings</a> or ask your administrator for assistance.</span>")]
    public string EmailCannotBeSentDescription => this[nameof (EmailCannotBeSentDescription)];

    /// <summary>word: Host</summary>
    [ResourceEntry("Host", Description = "word: Host", LastModified = "2019/01/22", Value = "Host")]
    public string Host => this[nameof (Host)];

    /// <summary>word: Port</summary>
    [ResourceEntry("Port", Description = "word: Port", LastModified = "2019/01/22", Value = "Port")]
    public string Port => this[nameof (Port)];

    /// <summary>word: Username</summary>
    [ResourceEntry("Username", Description = "word: Username", LastModified = "2019/01/22", Value = "Username")]
    public string Username => this[nameof (Username)];

    /// <summary>word: Password</summary>
    [ResourceEntry("Password", Description = "word: Password", LastModified = "2019/01/22", Value = "Password")]
    public string Password => this[nameof (Password)];

    /// <summary>
    /// Translated message, similar to "Gets or sets the port used for SMTP transactions"
    /// </summary>
    [ResourceEntry("SmtpPort", Description = "Property Caption.", LastModified = "2010/03/29", Value = "Gets or sets the port used for SMTP transactions")]
    public string SmtpPort => this[nameof (SmtpPort)];

    /// <summary>
    /// Translated message, similar to "Gets or sets the user name associated with the credentials.."
    /// </summary>
    [ResourceEntry("SmtpUserName", Description = "Property Caption", LastModified = "2010/03/29", Value = "Gets or sets the user name associated with the credentials.")]
    public string SmtpUserName => this[nameof (SmtpUserName)];

    /// <summary>
    /// Translated message, similar to "Gets or sets the password for the user name associated with the credentials"
    /// </summary>
    [ResourceEntry("SmtpPassword", Description = "Property Caption.", LastModified = "2010/03/29", Value = "Gets or sets the password for the user name associated with the credentials")]
    public string SmtpPassword => this[nameof (SmtpPassword)];

    /// <summary>Translated message, similar to "Domain"</summary>
    /// <value>Gets or sets the domain or computer name that verifies the credentials</value>
    [ResourceEntry("SmtpDomain", Description = "Property Caption", LastModified = "2010/03/29", Value = "Gets or sets the domain or computer name that verifies the credentials")]
    public string SmtpDomain => this[nameof (SmtpDomain)];

    /// <summary>
    /// Translated message, similar to "Specifies how outgoing email messages will be handled."
    /// </summary>
    [ResourceEntry("SmtpDeliveryMethod", Description = "Property Caption.", LastModified = "2010/03/29", Value = "Specifies how outgoing email messages will be handled.")]
    public string SmtpDeliveryMethod => this[nameof (SmtpDeliveryMethod)];

    /// <summary>
    /// Translated message, similar to "Specify whether Sitefinity will use Secure Sockets Layer (SSL) to encrypt the connection."
    /// </summary>
    [ResourceEntry("SmtpEnableSSL", Description = "Property Caption", LastModified = "2010/03/29", Value = "Specify whether Sitefinity will use Secure Sockets Layer (SSL) to encrypt the connection.")]
    public string SmtpEnableSSL => this[nameof (SmtpEnableSSL)];

    /// <summary>
    /// Translated message, similar to "Gets or sets a value that specifies the amount of time after which a synchronous mail sending times out."
    /// </summary>
    [ResourceEntry("SmtpTimeout", Description = "Property Caption", LastModified = "2010/03/29", Value = "Gets or sets a value that specifies the amount of time after which a synchronous mail sending times out.")]
    public string SmtpTimeout => this[nameof (SmtpTimeout)];

    /// <summary>
    /// Translated message, similar to "Gets or sets the folder where applications save mail messages to be processed by the local SMTP server."
    /// </summary>
    /// <value>Gets or sets the folder where applications save mail messages to be processed by the local SMTP server.</value>
    [ResourceEntry("SmtpPickupDirectoryLocation", Description = "Property Caption.", LastModified = "2010/03/29", Value = "Gets or sets the folder where applications save mail messages to be processed by the local SMTP server.")]
    public string SmtpPickupDirectoryLocation => this[nameof (SmtpPickupDirectoryLocation)];

    /// <summary>
    /// Translated message, similar to "Default sender email address."
    /// </summary>
    /// <value>Gets or sets default sender email address.</value>
    [ResourceEntry("DefaultSenderEmailAddress", Description = "Property Caption.", LastModified = "2019/01/22", Value = "Default sender email address")]
    public string DefaultSenderEmailAddress => this[nameof (DefaultSenderEmailAddress)];

    /// <summary>Translated message, similar to "Default sender name."</summary>
    /// <value>Gets or sets default sender name.</value>
    [ResourceEntry("DefaultSenderName", Description = "Property Caption.", LastModified = "2019/06/27", Value = "Default sender name")]
    public string DefaultSenderName => this[nameof (DefaultSenderName)];

    /// <summary>
    /// Translated message, similar to "Sender email address."
    /// </summary>
    /// <value>Gets or sets sender email address.</value>
    [ResourceEntry("SenderEmailAddress", Description = "Property Caption.", LastModified = "2019/06/11", Value = "Sender email address")]
    public string SenderEmailAddress => this[nameof (SenderEmailAddress)];

    /// <summary>Translated message, similar to "Sender name"</summary>
    /// <value>Gets or sets sender email address.</value>
    [ResourceEntry("SenderName", Description = "Property Caption.", LastModified = "2019/06/11", Value = "Sender name")]
    public string SenderName => this[nameof (SenderName)];

    /// <summary>Translated message, similar to "Save changes."</summary>
    /// <value>Gets or sets save changes caption.</value>
    [ResourceEntry("SaveChanges", Description = "Property Caption.", LastModified = "2019/06/11", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>Translated message, similar to "Use SSL."</summary>
    /// <value>Gets or sets if the smtp server will use SSL.</value>
    [ResourceEntry("UseSsl", Description = "Property Caption.", LastModified = "2019/01/22", Value = "Use SSL")]
    public string UseSsl => this[nameof (UseSsl)];

    /// <summary>
    /// phrase: Specifies the web name of the encoding used for the subjects of email messages. If no value is set then UTF-8 will be used.
    /// </summary>
    [ResourceEntry("EmailSubjectEncoding", Description = "Subject encoding for the mail message", LastModified = "2011/03/30", Value = "Specifies the web name of the encoding used for the subjects of email messages. If no value is set then UTF-8 will be used.")]
    public string EmailSubjectEncoding => this[nameof (EmailSubjectEncoding)];

    /// <summary>
    /// phrase: Specifies the web name of the encoding used for the bodies of email messages. If no value is set then UTF-8 will be used.
    /// </summary>
    [ResourceEntry("EmailBodyEncoding", Description = "Body encoding for the mail message", LastModified = "2011/03/30", Value = "Specifies the web name of the encoding used for the bodies of email messages. If no value is set then UTF-8 will be used.")]
    public string EmailBodyEncoding => this[nameof (EmailBodyEncoding)];

    /// <summary>Translated message, similar to "SMTP Settings"</summary>
    /// <value>Gets or sets the SMTP client settings used by Sitefinity for sending emails (e.g. recovering passwords)</value>
    [ResourceEntry("SmtpSettings", Description = "Gets or sets the SMTP client settings used by Sitefinity for sending emails (e.g. recovering passwords)", LastModified = "2010/03/29", Value = "SMTP Settings")]
    public string SmtpSettings => this[nameof (SmtpSettings)];

    /// <summary>
    /// Gets or sets the configuration value indicating weather newsletters smtp server uses authentication.
    /// </summary>
    [ResourceEntry("UseSmtpAuthentication", Description = "Gets or sets the configuration value indicating weather newsletters smtp server uses authentication.", LastModified = "2010/12/14", Value = "Use SMTP authentication")]
    public string UseSmtpAuthentication => this[nameof (UseSmtpAuthentication)];

    /// <summary>NotificationsSmtpProfileTitle</summary>
    [ResourceEntry("NotificationsSmtpProfileTitle", Description = "NotificationsSmtpProfileTitle", LastModified = "2012/07/02", Value = "Notifications SMTP Profile")]
    public string NotificationsSmtpProfileTitle => this[nameof (NotificationsSmtpProfileTitle)];

    /// <summary>
    /// Gets or sets the name of the notificaitons service to use for sending campaign issues.
    /// </summary>
    [ResourceEntry("NotificationsSmtpProfile", Description = "Gets or sets the name of the notificaitons service to use for sending campaign issues.", LastModified = "2012/06/12", Value = "Notifications SMTP Profile")]
    public string NotificationsSmtpProfile => this[nameof (NotificationsSmtpProfile)];

    /// <summary>Translated message, similar to "Background Tasks"</summary>
    /// <value>Background Tasks</value>
    [ResourceEntry("BackgroundTasksCaption", Description = "Background Tasks Settings for Sitefinity", LastModified = "2012/04/16", Value = "Background Tasks")]
    public string BackgroundTasksCaption => this[nameof (BackgroundTasksCaption)];

    /// <summary>
    /// Translated message, similar to "Provides a set of configuration options that Sitefinity will use to execute tasks in background."
    /// </summary>
    /// <value>Describes the purpose of the BackgroundTasks configuration element.</value>
    [ResourceEntry("BackgroundTasksConfigDescription", Description = "Describes the purpose of the BackgroundTasks configuration element.", LastModified = "2012/04/16", Value = "Provides a set of configuration options that Sitefinity will use to execute tasks in background.")]
    public string BackgroundTasksConfigDescription => this[nameof (BackgroundTasksConfigDescription)];

    /// <summary>
    /// Translated message, similar to "Determines the maximum number of background tasks that can be started in parallel on a particular app node. The default value '0' means that only half the number of processors will be used."
    /// </summary>
    [ResourceEntry("MaxBackgroundParallelTasksPerNodeDescription", Description = "Maximum number of background tasks", LastModified = "2020/06/10", Value = " Determines the maximum number of background tasks that can be started in parallel on a particular app node. The default value '0' means that only half the number of processors will be used.")]
    public string MaxBackgroundParallelTasksPerNodeDescription => this[nameof (MaxBackgroundParallelTasksPerNodeDescription)];

    /// <summary>Translated message, similar to "SSL Offloading"</summary>
    /// <value>SSL Offloading Settings for Sitefinity</value>
    [ResourceEntry("SSLOffloadingConfigCaption", Description = "SSL offloading relieves a Web server of the processing burden of encrypting and/or decrypting traffic sent via SSL, the security protocol that is implemented in every Web browser. The processing is offloaded to a separate device designed specifically to performSSL acceleration or SSL termination.", LastModified = "2015/08/04", Value = "SSL Offloading")]
    public string SSLOffloadingConfigCaption => this[nameof (SSLOffloadingConfigCaption)];

    /// <summary>
    /// Translated message, similar to "SSL Offloading enable"
    /// </summary>
    /// <value>Enable SSL Offloading Settings for Sitefinity</value>
    [ResourceEntry("EnableSslOffloading", Description = "EnableSslOffloading", LastModified = "2015/08/04", Value = "Enable SSL Offloading.")]
    public string EnableSslOffloading => this[nameof (EnableSslOffloading)];

    /// <summary>Translated message, similar to "SSL Offloading"</summary>
    /// <value>SSL Offloading Settings for Sitefinity</value>
    [ResourceEntry("SSLOffloadingConfigDescription", Description = "SSL offloading settings", LastModified = "2015/08/04", Value = "SSL offloading relieves a Web server of the processing burden of encrypting and/or decrypting traffic sent via SSL, the security protocol that is implemented in every Web browser. The processing is offloaded to a separate device designed specifically to performSSL acceleration or SSL termination.")]
    public string SSLOffloadingConfigDescription => this[nameof (SSLOffloadingConfigDescription)];

    /// <summary>Translated message, similar to "SSL Offloading"</summary>
    /// <value>SSL Offloading Settings for Sitefinity</value>
    [ResourceEntry("HttpHeaderFieldName", Description = "HttpHeaderFieldName description", LastModified = "2015/08/04", Value = "HTTP header field name for identifying the originating protocol of an HTTP request, since a reverse proxy (load balancer) may communicate with a web server using HTTP even if the request to the reverse proxy is HTTPS")]
    public string HttpHeaderFieldName => this[nameof (HttpHeaderFieldName)];

    /// <summary>Translated message, similar to "SSL Offloading"</summary>
    /// <value>SSL Offloading Settings for Sitefinity</value>
    [ResourceEntry("HttpHeaderFieldValue", Description = "HttpHeaderFieldValue description", LastModified = "2015/08/04", Value = "HTTP header field value.")]
    public string HttpHeaderFieldValue => this[nameof (HttpHeaderFieldValue)];

    /// <summary>phrase: Test message subject</summary>
    [ResourceEntry("TestMessageSubject", Description = "phrase: the message subject when sending a test message", LastModified = "2019/02/18", Value = "Sitefinity test message")]
    public string TestMessageSubject => this[nameof (TestMessageSubject)];

    /// <summary>phrase: Test message body</summary>
    [ResourceEntry("TestMessageBody", Description = "phrase: the message body when sending a test message", LastModified = "2019/02/18", Value = "This is a test message sent from Sitefinity to verify your SMTP settings. Your SMTP settings are working properly.")]
    public string TestMessageBody => this[nameof (TestMessageBody)];

    /// <summary>phrase: System email templates</summary>
    [ResourceEntry("SystemEmailTemplates", Description = "phrase: System email templates", LastModified = "2019/05/22", Value = "System email templates")]
    public string SystemEmailTemplates => this[nameof (SystemEmailTemplates)];

    /// <summary>phrase: System emails for...</summary>
    [ResourceEntry("SystemEmailsFor", Description = "phrase: System emails for...", LastModified = "2019/05/22", Value = "System emails for...")]
    public string SystemEmailsFor => this[nameof (SystemEmailsFor)];

    /// <summary>phrase: Subject</summary>
    [ResourceEntry("Subject", Description = "phrase: Subject", LastModified = "2019/05/22", Value = "Subject")]
    public string Subject => this[nameof (Subject)];

    /// <summary>phrase: Used in</summary>
    [ResourceEntry("UsedIn", Description = "phrase: Used in", LastModified = "2019/05/22", Value = "Used in")]
    public string UsedIn => this[nameof (UsedIn)];

    /// <summary>phrase: Edit system email template</summary>
    [ResourceEntry("TemplateEditorTitle", Description = "phrase: Edit system email template", LastModified = "2019/05/22", Value = "Edit system email template")]
    public string TemplateEditorTitle => this[nameof (TemplateEditorTitle)];

    /// <summary>phrase: Modified</summary>
    [ResourceEntry("Modified", Description = "phrase: Modified", LastModified = "2019/05/22", Value = "Modified")]
    public string Modified => this[nameof (Modified)];

    /// <summary>phrase: Back to System email templates</summary>
    [ResourceEntry("TemplateEditorBackButtonTitle", Description = "phrase: Back to System email templates", LastModified = "2019/05/22", Value = "Back to System email templates")]
    public string TemplateEditorBackButtonTitle => this[nameof (TemplateEditorBackButtonTitle)];

    /// <summary>phrase: Last modified</summary>
    [ResourceEntry("LastModifiedTitle", Description = "phrase: Last modified", LastModified = "2019/05/22", Value = "Last modified")]
    public string LastModifiedTitle => this[nameof (LastModifiedTitle)];

    /// <summary>phrase: by</summary>
    [ResourceEntry("LastModifiedBy", Description = "phrase: by", LastModified = "2019/05/22", Value = "by")]
    public string LastModifiedBy => this[nameof (LastModifiedBy)];

    /// <summary>phrase: on</summary>
    [ResourceEntry("LastModifiedOn", Description = "phrase: on", LastModified = "2019/05/22", Value = "on")]
    public string LastModifiedOn => this[nameof (LastModifiedOn)];

    /// <summary>phrase: Restore to original</summary>
    [ResourceEntry("RestoreToOriginal", Description = "phrase: Restore to original", LastModified = "2019/05/22", Value = "Restore to original")]
    public string RestoreToOriginal => this[nameof (RestoreToOriginal)];

    /// <summary>phrase: Insert</summary>
    [ResourceEntry("Insert", Description = "phrase: Insert", LastModified = "2019/05/22", Value = "Insert")]
    public string Insert => this[nameof (Insert)];

    /// <summary>phrase: Message text</summary>
    [ResourceEntry("TemplateText", Description = "phrase: Message text", LastModified = "2019/05/22", Value = "Message text")]
    public string TemplateText => this[nameof (TemplateText)];

    /// <summary>
    /// phrase: Original system email for this site will be used
    /// </summary>
    [ResourceEntry("ConfirmRestoreDescription", Description = "phrase: Original system email for this site will be used", LastModified = "2019/05/22", Value = "<span class=\"sfDisplayBlock\">Original system email for this site will be used. Any changes of this template will be discarded.</span><span class=\"sfDisplayBlock sfMTop15\">Do you want to proceed?</span>")]
    public string ConfirmRestoreDescription => this[nameof (ConfirmRestoreDescription)];

    /// <summary>Gets the phrase: SEO and OpenGraph properties</summary>
    /// <value>SEO and OpenGraph properties</value>
    [ResourceEntry("SeoAndOpenGraphPropertiesCaption", Description = "Gets the phrase: SEO and OpenGraph", LastModified = "2018/08/21", Value = "SEO and OpenGraph")]
    public string SeoAndOpenGraphPropertiesCaption => this[nameof (SeoAndOpenGraphPropertiesCaption)];

    /// <summary>Gets the phrase: Enable SEO properties</summary>
    /// <value>Enable SEO properties</value>
    [ResourceEntry("EnableSeoMetadataPropertiesCaption", Description = "Gets the phrase: Enable SEO properties", LastModified = "2017/10/18", Value = "Enable SEO properties")]
    public string EnableSeoMetadataPropertiesCaption => this[nameof (EnableSeoMetadataPropertiesCaption)];

    [ResourceEntry("EnableSeoMetadataPropertiesDescription", Description = "Gets the phrase: Allows adding SEO metadata properties to content items. Properties are automatically included in the <head> tag of a page with MVC widgets.", LastModified = "2017/10/18", Value = "Allows adding SEO metadata properties to content items. Properties are automatically included in the <head> tag of a page with MVC widgets.")]
    public string EnableSeoMetadataPropertiesDescription => this[nameof (EnableSeoMetadataPropertiesDescription)];

    /// <summary>Gets the phrase: Enable Open Graph properties</summary>
    /// <value>Enable Open Graph properties</value>
    [ResourceEntry("EnableOpenGraphMetadataPropertiesCaption", Description = "Gets the phrase: Enable Open Graph properties", LastModified = "2017/10/18", Value = "Enable Open Graph properties")]
    public string EnableOpenGraphMetadataPropertiesCaption => this[nameof (EnableOpenGraphMetadataPropertiesCaption)];

    [ResourceEntry("EnableOpenGraphMetadataPropertiesDescription", Description = "Gets the phrase: Allows adding Open Graph metadata properties to content items. Properties are automatically included in the <head> tag of a page with MVC widgets.", LastModified = "2017/10/18", Value = "Allows adding Open Graph metadata properties to content items. Properties are automatically included in the <head> tag of a page with MVC widgets.")]
    public string EnableOpenGraphMetadataPropertiesDescription => this[nameof (EnableOpenGraphMetadataPropertiesDescription)];

    /// <summary>
    /// Gets the phrase: Append \"nofollow\" attribute to links in untrusted content
    /// </summary>
    /// <value>Append \"nofollow\" attribute to links in untrusted content</value>
    [ResourceEntry("AppendNoFollowLinksForUntrustedContentCaption", Description = "Gets the phrase: Append \"nofollow\" attribute to links in untrusted content", LastModified = "2018/08/30", Value = "Append \"nofollow\" attribute to links in untrusted content")]
    public string AppendNoFollowLinksForUntrustedContentCaption => this[nameof (AppendNoFollowLinksForUntrustedContentCaption)];

    /// <summary>
    /// Gets the phrase: Append a rel=\"nofollow\" attribute to hyperlinks present in untrusted content. Untrusted content represents comments posted by site visitors. For example, comments for News, Blogs, Forum posts and so on.
    /// </summary>
    /// <value>Append a rel=\"nofollow\" attribute to hyperlinks present in untrusted content. Untrusted content represents comments posted by site visitors. For example, comments for News, Blogs, Forum posts and so on.</value>
    [ResourceEntry("AppendNoFollowLinksForUntrustedContentDescription", Description = "Gets the phrase: Append a rel=\"nofollow\" attribute to hyperlinks present in untrusted content. Untrusted content represents comments posted by site visitors. For example, comments for News, Blogs, Forum posts and so on.", LastModified = "2018/08/30", Value = "Append a rel=\"nofollow\" attribute to hyperlinks present in untrusted content. Untrusted content represents comments posted by site visitors. For example, comments for News, Blogs, Forum posts and so on.")]
    public string AppendNoFollowLinksForUntrustedContentDescription => this[nameof (AppendNoFollowLinksForUntrustedContentDescription)];

    /// <summary>Gets the phrase: Trusted domains</summary>
    /// <value>Trusted domains</value>
    [ResourceEntry("TrustedDomainsCaption", Description = "Gets the phrase: Trusted domains", LastModified = "2018/08/15", Value = "Trusted domains")]
    public string TrustedDomainsCaption => this[nameof (TrustedDomainsCaption)];

    /// <summary>
    /// Gets the phrase: A comma separated list of domains that Sitefinity CMS should consider trusted. Subdomains must be added as separate entries, for example mysite.com, marketing.mysite.com. Hyperlinks to a domain present in this list will not get a rel=“nofollow” attribute appended when untrusted content is screened. By default, all domains registered in your Sitefinity CMS license (including Single site and Multisite domains) are considered trusted.
    /// </summary>
    /// <value>A comma separated list of domains that Sitefinity CMS should consider trusted. Subdomains must be added as separate entries, for example mysite.com, marketing.mysite.com. Hyperlinks to a domain present in this list will not get a rel=“nofollow” attribute appended when untrusted content is screened. By default, all domains registered in your Sitefinity CMS license (including Single site and Multisite domains) are considered trusted.</value>
    [ResourceEntry("TrustedDomainsDescription", Description = "Gets the phrase: A comma separated list of domains that Sitefinity CMS should consider trusted. Subdomains must be added as separate entries, for example mysite.com, marketing.mysite.com. Hyperlinks to a domain present in this list will not get a rel=“nofollow” attribute appended when untrusted content is screened. By default, all domains registered in your Sitefinity CMS license (including Single site and Multisite domains) are considered trusted.", LastModified = "2018/08/30", Value = "A comma separated list of domains that Sitefinity CMS should consider trusted. Subdomains must be added as separate entries, for example mysite.com, marketing.mysite.com. Hyperlinks to a domain present in this list will not get a rel=“nofollow” attribute appended when untrusted content is screened. By default, all domains registered in your Sitefinity CMS license (including Single site and Multisite domains) are considered trusted.")]
    public string TrustedDomainsDescription => this[nameof (TrustedDomainsDescription)];

    /// <summary>Gets the phrase: Health check service</summary>
    /// <value>Health check service</value>
    [ResourceEntry("HealthCheckCaption", Description = "Gets the phrase: Health check service", LastModified = "2016/10/19", Value = "Health check service")]
    public string HealthCheckCaption => this[nameof (HealthCheckCaption)];

    /// <summary>
    /// Gets the phrase: With the Health check service, you run background checks on whether your Sitefinity CMS application is operational, responsive, and set up properly.
    /// </summary>
    /// <value>With the Health check service, you run background checks on whether your Sitefinity CMS application is operational, responsive, and set up properly.</value>
    [ResourceEntry("HealthCheckDescription", Description = "Gets the phrase: With the Health check service, you run background checks on whether your Sitefinity CMS application is operational, responsive, and set up properly.", LastModified = "2016/10/19", Value = "With the Health check service, you run background checks on whether your Sitefinity CMS application is operational, responsive, and set up properly.")]
    public string HealthCheckDescription => this[nameof (HealthCheckDescription)];

    /// <summary>Gets the phrase: Gets Health check name</summary>
    /// <value>Health check name</value>
    [ResourceEntry("HealthCheckNamePropNameCaption", Description = "Gets the phrase: Gets Health check name", LastModified = "2016/10/19", Value = "Health check name")]
    public string HealthCheckNamePropNameCaption => this[nameof (HealthCheckNamePropNameCaption)];

    /// <summary>Gets the phrase: Gets Health check timeout</summary>
    /// <value>Timeout</value>
    [ResourceEntry("HealthCheckTimeoutSecondsCaption", Description = "Gets the phrase: Gets Health check timeout", LastModified = "2016/10/19", Value = "Timeout")]
    public string HealthCheckTimeoutSecondsCaption => this[nameof (HealthCheckTimeoutSecondsCaption)];

    /// <summary>
    /// Gets the phrase: Gets or sets Health check timeout in seconds
    /// </summary>
    /// <value>In seconds</value>
    [ResourceEntry("HealthCheckTimeoutSecondsDescription", Description = "Gets the phrase: Gets or sets Health check timeout in seconds", LastModified = "2016/10/19", Value = "In seconds")]
    public string HealthCheckTimeoutSecondsDescription => this[nameof (HealthCheckTimeoutSecondsDescription)];

    /// <summary>Gets the phrase: Gets Health check groups</summary>
    /// <value>Groups</value>
    [ResourceEntry("HealthCheckGroupsCaption", Description = "Gets the phrase: Gets Health check groups", LastModified = "2016/10/19", Value = "Groups")]
    public string HealthCheckGroupsCaption => this[nameof (HealthCheckGroupsCaption)];

    /// <summary>
    /// Gets the phrase: Enter one or more groups, separated by a comma. Groups filter health check results to get a specific set of results
    /// </summary>
    /// <value>Enter one or more groups, separated by a comma. Groups filter health check results to get a specific set of results</value>
    [ResourceEntry("HealthCheckGroupsDescription", Description = "Gets the phrase: Enter one or more groups, separated by a comma. Groups filter health check results to get a specific set of results", LastModified = "2016/10/19", Value = "Enter one or more groups, separated by a comma. Groups filter health check results to get a specific set of results")]
    public string HealthCheckGroupsDescription => this[nameof (HealthCheckGroupsDescription)];

    /// <summary>Gets the phrase: Gets Health check type</summary>
    /// <value>Type</value>
    [ResourceEntry("HealthCheckTypeCaption", Description = "Gets the phrase: Gets Health check type", LastModified = "2016/10/19", Value = "Type")]
    public string HealthCheckTypeCaption => this[nameof (HealthCheckTypeCaption)];

    /// <summary>
    /// Gets the phrase: Enter the specific type, associated with this health check
    /// </summary>
    /// <value>Enter the specific type, associated with this health check</value>
    [ResourceEntry("HealthCheckTypeDescription", Description = "Gets the phrase: Enter the specific type, associated with this health check", LastModified = "2016/10/19", Value = "Enter the specific type, associated with this health check")]
    public string HealthCheckTypeDescription => this[nameof (HealthCheckTypeDescription)];

    /// <summary>Gets the phrase: Enable health check</summary>
    /// <value>Enable health check</value>
    [ResourceEntry("HealthCheckEnabledCaption", Description = "Gets the phrase: Enable health check", LastModified = "2016/10/19", Value = "Enable health check")]
    public string HealthCheckEnabledCaption => this[nameof (HealthCheckEnabledCaption)];

    /// <summary>Gets the phrase: Gets Health check parameters</summary>
    /// <value>Parameters</value>
    [ResourceEntry("HealthCheckParametersCaption", Description = "Gets the phrase: Gets Health check parameters", LastModified = "2016/10/19", Value = "Parameters")]
    public string HealthCheckParametersCaption => this[nameof (HealthCheckParametersCaption)];

    /// <summary>Gets the phrase: Health checks</summary>
    /// <value>Health checks</value>
    [ResourceEntry("HealthChecksCaption", Description = "Gets the phrase: Health checks", LastModified = "2016/10/19", Value = "Health checks")]
    public string HealthChecksCaption => this[nameof (HealthChecksCaption)];

    /// <summary>Gets the phrase: Gets Health check critical</summary>
    /// <value>Critical</value>
    [ResourceEntry("HealthCheckCriticalCaption", Description = "Gets the phrase: Gets Health check critical", LastModified = "2016/10/19", Value = "Critical")]
    public string HealthCheckCriticalCaption => this[nameof (HealthCheckCriticalCaption)];

    /// <summary>
    /// Gets the phrase: Defines whether  this check affects the end result of the overall health check service
    /// </summary>
    /// <value>Defines whether  this check affects the end result of the overall health check service</value>
    [ResourceEntry("HealthCheckCriticalDescription", Description = "Gets the phrase: Defines whether  this check affects the end result of the overall health check service", LastModified = "2016/10/19", Value = "Defines whether  this check affects the end result of the overall health check service")]
    public string HealthCheckCriticalDescription => this[nameof (HealthCheckCriticalDescription)];

    /// <summary>Gets the phrase: Enable Health check service</summary>
    /// <value>Enable Health check service</value>
    [ResourceEntry("HealthChecksEnabledCaption", Description = "Gets the phrase: Enable Health check service", LastModified = "2016/10/19", Value = "Enable Health check service")]
    public string HealthChecksEnabledCaption => this[nameof (HealthChecksEnabledCaption)];

    /// <summary>Gets the phrase: Enable logging of health check tasks</summary>
    /// <value>Enable logging of health check tasks</value>
    [ResourceEntry("HealthCheckLoggingCaption", Description = "Gets the phrase: Enable logging of health check tasks", LastModified = "2016/10/19", Value = "Enable logging of health check tasks")]
    public string HealthCheckLoggingCaption => this[nameof (HealthCheckLoggingCaption)];

    /// <summary>
    /// Gets the phrase: Store responses, messages, and timestamps in a dedicated log file
    /// </summary>
    /// <value>Store responses, messages, and timestamps in a dedicated log file</value>
    [ResourceEntry("HealthCheckLoggingDescription", Description = "Gets the phrase: Store responses, messages, and timestamps in a dedicated log file", LastModified = "2016/10/19", Value = "Store responses, messages, and timestamps in a dedicated log file")]
    public string HealthCheckLoggingDescription => this[nameof (HealthCheckLoggingDescription)];

    /// <summary>Gets the phrase: Enable HTTP status codes</summary>
    /// <value>Enable HTTP status codes</value>
    [ResourceEntry("HealthCheckReturnHttpErrorStatusCodeCaption", Description = "Gets the phrase: Enable HTTP status codes", LastModified = "2016/12/06", Value = "Enable HTTP status codes")]
    public string HealthCheckReturnHttpErrorStatusCodeCaption => this[nameof (HealthCheckReturnHttpErrorStatusCodeCaption)];

    /// <summary>
    /// Gets the phrase: Provides you with status code 500 in case the health check does not pass
    /// </summary>
    /// <value>Provides you with status code 500 in case the health check does not pass</value>
    [ResourceEntry("HealthCheckReturnHttpErrorStatusCodeDescription", Description = "Gets the phrase: Provides you with status code 500 in case the health check does not pass", LastModified = "2020/03/31", Value = "This property is deprecated. In order to set the status code that should be returned when the healthy check does not pass, use web configuration app setting with key \"sf:HealthCheckUnhealthyStatusCode\", e.g. <add key=\"sf: HealthCheckUnhealthyStatusCode\" value=\"500\" />")]
    public string HealthCheckReturnHttpErrorStatusCodeDescription => this[nameof (HealthCheckReturnHttpErrorStatusCodeDescription)];

    /// <summary>Gets the phrase: Authentication key</summary>
    /// <value>Authentication key</value>
    [ResourceEntry("HealthCheckAuthenticationKeyCaption", Description = "Gets the phrase: Authentication key", LastModified = "2016/12/09", Value = "Authentication key")]
    public string HealthCheckAuthenticationKeyCaption => this[nameof (HealthCheckAuthenticationKeyCaption)];

    /// <summary>
    /// Gets the phrase: You use the key in the header of the HTTP request
    /// </summary>
    /// <value>You use the key in the header of the HTTP request</value>
    [ResourceEntry("HealthCheckAuthenticationKeyDescription", Description = "Gets the phrase: You use the key in the header of the HTTP request", LastModified = "2016/12/09", Value = "You use the key in the header of the HTTP request")]
    public string HealthCheckAuthenticationKeyDescription => this[nameof (HealthCheckAuthenticationKeyDescription)];

    /// <summary>
    /// Translated message, similar to "The value should be greater than or equal to 0."
    /// </summary>
    [ResourceEntry("BackgroundTasksServiceNonNegativeValueMessage", Description = "Property error message for non negative number validation.", LastModified = "2012/04/17", Value = "The value should be greater than or equal to 0.")]
    public string BackgroundTasksServiceNonNegativeValueMessage => this[nameof (BackgroundTasksServiceNonNegativeValueMessage)];

    /// <summary>
    /// Translated message, similar to "Determines the maximum timeout in milliseconds that will be given to the tasks to finish before they are aborted. Default value 0."
    /// </summary>
    [ResourceEntry("WaitBeforeAbortTasksDescription", Description = "Determines the maximum timeout in milliseconds", LastModified = "2012/04/17", Value = "Determines the maximum timeout in milliseconds that will be given to the tasks to finish before they are aborted. Default value 0.")]
    public string WaitBeforeAbortTasksDescription => this[nameof (WaitBeforeAbortTasksDescription)];

    /// <summary>Gets the UI time zone config descriptions.</summary>
    /// <value>The UI time zone config descriptions.</value>
    [ResourceEntry("UITimeZoneConfigDescriptions", Description = "UI Time Zone Configuration for Sitefinity", LastModified = "2010/03/29", Value = "UI Time Zone Config ")]
    public string UITimeZoneConfigDescriptions => this[nameof (UITimeZoneConfigDescriptions)];

    /// <summary>phrase: Time Zone ID</summary>
    [ResourceEntry("TimeZoneId", Description = "phrase: Time Zone ID", LastModified = "2010/11/19", Value = "Time Zone ID")]
    public string TimeZoneId => this[nameof (TimeZoneId)];

    /// <summary>Gets the Elmah config descriptions.</summary>
    /// <value>The Elmah config descriptions.</value>
    [ResourceEntry("UIElmahConfigDescriptions", Description = "UI Elmah Configuration for Sitefinity", LastModified = "2013/12/10", Value = "UI Elmah Config")]
    public string UIElmahConfigDescriptions => this[nameof (UIElmahConfigDescriptions)];

    /// <summary>phrase: Turn Elmah logging On/Off</summary>
    [ResourceEntry("TurnElmahLogging", Description = "phrase: Turn Elmah logging On/Off", LastModified = "2014/01/14", Value = "Turn Elmah logging On/Off")]
    public string TurnElmahLogging => this[nameof (TurnElmahLogging)];

    /// <summary>Use User Browser settings for calculating dates</summary>
    [ResourceEntry("UserBrowserSettingsForCalculatingDates", Description = "Use User Browser settings for calculating dates", LastModified = "2010/03/29", Value = "Use User Browser settings for calculating dates")]
    public string UserBrowserSettingsForCalculatingDates => this[nameof (UserBrowserSettingsForCalculatingDates)];

    /// <summary>phrase: Toolbox item parameters.</summary>
    [ResourceEntry("ToolboxItemParamsTitle", Description = "phrase: Toolbox item parameters.", LastModified = "2010/04/13", Value = "Toolbox item parameters.")]
    public string ToolboxItemParamsTitle => this[nameof (ToolboxItemParamsTitle)];

    /// <summary>
    /// phrase: Parameters with which the toolbox item should be initialized.
    /// </summary>
    [ResourceEntry("ToolboxItemParamsDescription", Description = "phrase: Prededefined values of widget properties.", LastModified = "2019/06/25", Value = "Prededefined values of widget properties.")]
    public string ToolboxItemParamsDescription => this[nameof (ToolboxItemParamsDescription)];

    /// <summary>phrase: Toolbox item visibility mode.</summary>
    [ResourceEntry("ToolboxItemVisibilityModeTitle", Description = "phrase: Toolbox item visibility mode.", LastModified = "2011/02/16", Value = "Toolbox item visibility mode.")]
    public string ToolboxItemVisibilityModeTitle => this[nameof (ToolboxItemVisibilityModeTitle)];

    /// <summary>
    /// phrase: Specifies if the toolbox item will be visible only for a specific media type.
    /// </summary>
    [ResourceEntry("ToolboxItemVisibilityModeDescription", Description = "phrase: Specifies if the toolbox item will be visible only for a specific media type.", LastModified = "2011/02/16", Value = "Specifies if the toolbox item will be visible only for a specific media type.")]
    public string ToolboxItemVisibilityModeDescription => this[nameof (ToolboxItemVisibilityModeDescription)];

    /// <summary>Message: Template Name</summary>
    [ResourceEntry("BulkEditFieldDefinitionTemplateNameCaption", Description = "Describes configuration element.", LastModified = "2010/06/15", Value = "Template Name")]
    public string BulkEditFieldDefinitionTemplateNameCaption => this[nameof (BulkEditFieldDefinitionTemplateNameCaption)];

    /// <summary>
    /// Message: Defines the template to set for the configured BulkEditField control.
    /// </summary>
    [ResourceEntry("BulkEditFieldDefinitionTemplateNameDescription", Description = "Describes configuration element.", LastModified = "2010/06/15", Value = "Defines the template to set for the configured BulkEditField control.")]
    public string BulkEditFieldDefinitionTemplateNameDescription => this[nameof (BulkEditFieldDefinitionTemplateNameDescription)];

    /// <summary>Message: Template Path</summary>
    [ResourceEntry("BulkEditFieldDefinitionTemplatePathCaption", Description = "Describes configuration element.", LastModified = "2011/03/19", Value = "Template Path")]
    public string BulkEditFieldDefinitionTemplatePathCaption => this[nameof (BulkEditFieldDefinitionTemplatePathCaption)];

    /// <summary>
    /// Message: Defines the template to set for the configured BulkEditField control.
    /// </summary>
    [ResourceEntry("BulkEditFieldDefinitionTemplatePathDescription", Description = "Describes configuration element.", LastModified = "2011/03/19", Value = "Defines the template to set for the configured BulkEditField control.")]
    public string BulkEditFieldDefinitionTemplatePathDescription => this[nameof (BulkEditFieldDefinitionTemplatePathDescription)];

    /// <summary>Message: Parent item type</summary>
    [ResourceEntry("ParentTypeCaption", Description = "Describes configuration element.", LastModified = "2010/06/17", Value = "Parent item type")]
    public string ParentTypeCaption => this[nameof (ParentTypeCaption)];

    /// <summary>Message: The type of the parent item</summary>
    [ResourceEntry("ParentTypeDescription", Description = "Describes configuration element.", LastModified = "2010/06/17", Value = "The type of the parent item")]
    public string ParentTypeDescription => this[nameof (ParentTypeDescription)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("DisableRemovingCaption", Description = "A value indicating whether removing existing pipes is possible.", LastModified = "2010/07/28", Value = "A value indicating whether removing existing pipes is possible.")]
    public string DisableRemovingCaption => this[nameof (DisableRemovingCaption)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("DisableRemovingDescription", Description = "A value indicating whether removing existing pipes is possible.", LastModified = "2010/07/28", Value = "A value indicating whether removing existing pipes is possible.")]
    public string DisableRemovingDescription => this[nameof (DisableRemovingDescription)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("DisableAddingCaption", Description = "A value indicating whether adding new pipes is possible.", LastModified = "2010/07/28", Value = "A value indicating whether adding new pipes is possible.")]
    public string DisableAddingCaption => this[nameof (DisableAddingCaption)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("DisableAddingDescription", Description = "A value indicating whether adding new pipes is possible.", LastModified = "2010/07/28", Value = "A value indicating whether adding new pipes is possible.")]
    public string DisableAddingDescription => this[nameof (DisableAddingDescription)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("AddPipeTextCaption", Description = "The text of the button for adding pipes.", LastModified = "2010/07/28", Value = "The text of the button for adding pipes.")]
    public string AddPipeTextCaption => this[nameof (AddPipeTextCaption)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("AddPipeTextDescription", Description = "The text of the button for adding pipes.", LastModified = "2010/07/28", Value = "The text of the button for adding pipes.")]
    public string AddPipeTextDescription => this[nameof (AddPipeTextDescription)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("ChangePipeTextCaption", Description = "The text of the button for changing pipes.", LastModified = "2010/07/28", Value = "The text of the button for changing pipes.")]
    public string ChangePipeTextCaption => this[nameof (ChangePipeTextCaption)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("ChangePipeTextDescription", Description = "The text of the button for changing pipes.", LastModified = "2010/07/28", Value = "The text of the button for changing pipes.")]
    public string ChangePipeTextDescription => this[nameof (ChangePipeTextDescription)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("DisableActivationCaption", Description = "A value indicating whether activation of pipes is possible.", LastModified = "2010/07/28", Value = "A value indicating whether activation of pipes is possible.")]
    public string DisableActivationCaption => this[nameof (DisableActivationCaption)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("DisableActivationDescription", Description = "A value indicating whether activation of pipes is possible.", LastModified = "2010/07/28", Value = "A value indicating whether activation of pipes is possible.")]
    public string DisableActivationDescription => this[nameof (DisableActivationDescription)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("DefaultPipeNameCaption", Description = "The default name of the pipe settings to add.", LastModified = "2010/07/28", Value = "The default name of the pipe settings to add.")]
    public string DefaultPipeNameCaption => this[nameof (DefaultPipeNameCaption)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("DefaultPipeNameDescription", Description = "The default name of the pipe settings to add.", LastModified = "2010/07/28", Value = "The default name of the pipe settings to add.")]
    public string DefaultPipeNameDescription => this[nameof (DefaultPipeNameDescription)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("ShowDefaultPipesCaption", Description = "A value indicating whether to show default pipes.", LastModified = "2010/07/29", Value = "A value indicating whether to show default pipes.")]
    public string ShowDefaultPipesCaption => this[nameof (ShowDefaultPipesCaption)];

    /// <summary>
    /// Resource strings for ListPipeSettingsFieldDefinitionElement class.
    /// </summary>
    [ResourceEntry("ShowDefaultPipesDescription", Description = "A value indicating whether to show default pipes.", LastModified = "2010/07/29", Value = "A value indicating whether to show default pipes.")]
    public string ShowDefaultPipesDescription => this[nameof (ShowDefaultPipesDescription)];

    /// <summary>phrase: Work with outbound pipes</summary>
    [ResourceEntry("WorkWithOutboundPipesCaption", Description = "phrase: Work with outbound pipes", LastModified = "2010/10/22", Value = "Work with outbound pipes")]
    public string WorkWithOutboundPipesCaption => this[nameof (WorkWithOutboundPipesCaption)];

    /// <summary>
    /// phrase: A value indicating whether to work with outbound pipes.
    /// </summary>
    [ResourceEntry("WorkWithOutboundPipesDescription", Description = "phrase: A value indicating whether to work with outbound pipes.", LastModified = "2010/10/22", Value = "A value indicating whether to work with outbound pipes.")]
    public string WorkWithOutboundPipesDescription => this[nameof (WorkWithOutboundPipesDescription)];

    /// <summary>phrase: Show content location</summary>
    [ResourceEntry("ShowContentLocationCaption", Description = "phrase: Show content location", LastModified = "2010/12/13", Value = "Show content location")]
    public string ShowContentLocationCaption => this[nameof (ShowContentLocationCaption)];

    /// <summary>
    /// phrase: Gets or sets a value indicating whether to show content location (default page url) inside the main list of pipes
    /// This is relevant only for the content pipes
    /// </summary>
    [ResourceEntry("ShowContentLocationDescription", Description = "phrase: Gets or sets a value indicating whether to show content location (default page url) inside the main list of pipes. This is relevant only for the content pipes", LastModified = "2010/12/13", Value = "Gets or sets a value indicating whether to show content location (default page url) inside the main list of pipes. This is relevant only for the content pipes")]
    public string ShowContentLocationDescription => this[nameof (ShowContentLocationDescription)];

    /// <summary>phrase: Publishing provider name</summary>
    [ResourceEntry("PublishingProviderNameCaption", Description = "phrase: Publishing provider name", LastModified = "2010/12/13", Value = "Publishing provider name")]
    public string PublishingProviderNameCaption => this[nameof (PublishingProviderNameCaption)];

    /// <summary>phrase: Name of the publishing provider to use</summary>
    [ResourceEntry("PublishingProviderNameDescription", Description = "phrase: Name of the publishing provider to use", LastModified = "2010/12/13", Value = "Name of the publishing provider to use")]
    public string PublishingProviderNameDescription => this[nameof (PublishingProviderNameDescription)];

    /// <summary>phrase: PostRightsCaption</summary>
    [ResourceEntry("PostRightsCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "PostRights")]
    public string PostRightsCaption => this[nameof (PostRightsCaption)];

    /// <summary>
    /// Defines who is allowed to post comments on public site. Possible values: Everyone and RegisteredUsers.
    /// </summary>
    [ResourceEntry("PostRightsDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines who is allowed to post comments on public site. Possible values: Everyone and RegisteredUsers.")]
    public string PostRightsDescription => this[nameof (PostRightsDescription)];

    /// <summary>phrase: HideCommentsAfterNumberOfDaysCaption</summary>
    [ResourceEntry("HideCommentsAfterNumberOfDaysCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "HideCommentsAfterNumberOfDays")]
    public string HideCommentsAfterNumberOfDaysCaption => this[nameof (HideCommentsAfterNumberOfDaysCaption)];

    /// <summary>
    /// Defines flag which determines if comment items will be hidden after specified number of days.
    /// </summary>
    [ResourceEntry("HideCommentsAfterNumberOfDaysDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines flag which determines if comment items will be hidden after specified number of days.")]
    public string HideCommentsAfterNumberOfDaysDescription => this[nameof (HideCommentsAfterNumberOfDaysDescription)];

    /// <summary>phrase: NumberOfDaysToHideCommentsCaption</summary>
    [ResourceEntry("NumberOfDaysToHideCommentsCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "NumberOfDaysToHideComments")]
    public string NumberOfDaysToHideCommentsCaption => this[nameof (NumberOfDaysToHideCommentsCaption)];

    /// <summary>
    /// Defines number of days after which comment items older than this number will be hidden, if configured so with HideCommentsAfterNumberOfDays flag.
    /// </summary>
    [ResourceEntry("NumberOfDaysToHideCommentsDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines number of days after which comment items older than this number will be hidden, if configured so with HideCommentsAfterNumberOfDays flag.")]
    public string NumberOfDaysToHideCommentsDescription => this[nameof (NumberOfDaysToHideCommentsDescription)];

    /// <summary>phrase: DateFormatCaption</summary>
    [ResourceEntry("DateFormatCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "DateFormat")]
    public string DateFormatCaption => this[nameof (DateFormatCaption)];

    /// <summary>Defines format of dates to be used on public site.</summary>
    [ResourceEntry("DateFormatDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines format of dates to be used on public site.")]
    public string DateFormatDescription => this[nameof (DateFormatDescription)];

    /// <summary>phrase: TrackBackCaption</summary>
    [ResourceEntry("TrackBackCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "TrackBack")]
    public string TrackBackCaption => this[nameof (TrackBackCaption)];

    /// <summary>
    /// Defines if trackback between comments of the blogs will be used on public site.
    /// </summary>
    [ResourceEntry("TrackBackDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines if trackback between comments of the blogs will be used on public site.")]
    public string TrackBackDescription => this[nameof (TrackBackDescription)];

    /// <summary>phrase: AllowCommentsCaption</summary>
    [ResourceEntry("AllowCommentsCaption", Description = "The display title of this configuration element.", LastModified = "2018/08/14", Value = "Allow comments")]
    public string AllowCommentsCaption => this[nameof (AllowCommentsCaption)];

    /// <summary>Defines if content item supports comments.</summary>
    [ResourceEntry("AllowCommentsDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines if content item supports comments.")]
    public string AllowCommentsDescription => this[nameof (AllowCommentsDescription)];

    /// <summary>phrase: EmailAuthorCaption</summary>
    [ResourceEntry("EmailAuthorCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "EmailAuthor")]
    public string EmailAuthorCaption => this[nameof (EmailAuthorCaption)];

    /// <summary>
    /// Defines if author of the post will be notified via email when a new comment is submitted.
    /// </summary>
    [ResourceEntry("EmailAuthorDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines if author of the post will be notified via email when a new comment is submitted")]
    public string EmailAuthorDescription => this[nameof (EmailAuthorDescription)];

    /// <summary>phrase: ApproveCommentsCaption</summary>
    [ResourceEntry("ApproveCommentsCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "ApproveComments")]
    public string ApproveCommentsCaption => this[nameof (ApproveCommentsCaption)];

    /// <summary>
    /// Defines if the comments have to be approved in order to be displayed.
    /// </summary>
    [ResourceEntry("ApproveCommentsDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines if the comments have to be approved in order to be displayed.")]
    public string ApproveCommentsDescription => this[nameof (ApproveCommentsDescription)];

    /// <summary>
    /// Message: Comments have to be approved before appear on the site.
    /// </summary>
    /// <value>Comments have to be approved before appear on the site.</value>
    [ResourceEntry("ApproveComments", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Comments have to be approved before appearing on the site")]
    public string ApproveComments => this[nameof (ApproveComments)];

    /// <summary>Message: Use spam protection image (captcha).</summary>
    /// <value>Use spam protection image (captcha).</value>
    [ResourceEntry("UseSpamProtectionImage", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Use spam protection image (captcha)")]
    public string UseSpamProtectionImage => this[nameof (UseSpamProtectionImage)];

    /// <summary>Message: Number of days to close comments if set.</summary>
    /// <value>Number of days to close comments if set.</value>
    [ResourceEntry("NumberOfDaysToHideComments", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Number of days to close comments if set at property 'HideCommentsAfterNumberOfDays'.")]
    public string NumberOfDaysToHideComments => this[nameof (NumberOfDaysToHideComments)];

    /// <summary>
    /// Message: Automatically close comments for items older than specified days at property 'NumberOfDaysToHideComments'.
    /// </summary>
    /// <value>Automatically close comments for items older than specified days at property 'NumberOfDaysToHideComments'.</value>
    [ResourceEntry("HideCommentsAfterNumberOfDays", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Automatically close comments for items older than specified days at property 'NumberOfDaysToHideComments'.")]
    public string HideCommentsAfterNumberOfDays => this[nameof (HideCommentsAfterNumberOfDays)];

    /// <summary>
    /// Message: Display Name field on the form for posting comments.
    /// </summary>
    /// <value>Display Name field on the form for posting comments.</value>
    [ResourceEntry("DisplayNameField", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Display Name field on the form for posting comments.")]
    public string DisplayNameField => this[nameof (DisplayNameField)];

    /// <summary>
    /// Message: Display Email field on the form for posting comments.
    /// </summary>
    /// <value>Display Email field on the form for posting comments.</value>
    [ResourceEntry("DisplayEmailField", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Display Email field on the form for posting comments.")]
    public string DisplayEmailField => this[nameof (DisplayEmailField)];

    /// <summary>
    /// Message: Display Website field on the form for posting comments.
    /// </summary>
    [ResourceEntry("DisplayWebSiteField", Description = "Describes configuration element. ", LastModified = "2012/01/05", Value = "Display Website field on the form for posting comments.")]
    public string DisplayWebSiteField => this[nameof (DisplayWebSiteField)];

    /// <summary>
    /// Message: Display Message field on the form for posting comments.
    /// </summary>
    /// <value>Display Message field on the form for posting comments.</value>
    [ResourceEntry("DisplayMessageField", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Display Message field on the form for posting comments.")]
    public string DisplayMessageField => this[nameof (DisplayMessageField)];

    /// <summary>
    /// Message: Determines if Email field is required on the form for posting comments.
    /// </summary>
    [ResourceEntry("IsEmailFieldRequired", Description = "Describes configuration element. ", LastModified = "2012/01/05", Value = "Determines if Email field is required on the form for posting comments.")]
    public string IsEmailFieldRequired => this[nameof (IsEmailFieldRequired)];

    /// <summary>
    /// Message: Determines if Name field is required on the form for posting comments.
    /// </summary>
    /// <value>Determines if Name field is required on the form for posting comments.</value>
    [ResourceEntry("IsNameFieldRequired", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Determines if Name field is required on the form for posting comments.")]
    public string IsNameFieldRequired => this[nameof (IsNameFieldRequired)];

    /// <summary>
    /// Message: Determines if Message field is required on the form for posting comments.
    /// </summary>
    /// <value>Determines if Message field is required on the form for posting comments.</value>
    [ResourceEntry("IsMessageFieldRequired", Description = "Describes configuration element. ", LastModified = "2010/08/18", Value = "Determines if Message field is required on the form for posting comments.")]
    public string IsMessageFieldRequired => this[nameof (IsMessageFieldRequired)];

    /// <summary>
    /// Message: Determines if Website field is required on the form for posting comments.
    /// </summary>
    [ResourceEntry("IsWebSiteFieldRequired", Description = "Describes configuration element. ", LastModified = "2012/01/05", Value = "Determines if Website field is required on the form for posting comments.")]
    public string IsWebSiteFieldRequired => this[nameof (IsWebSiteFieldRequired)];

    /// <summary>phrase: ModerateCommentsCaption</summary>
    [ResourceEntry("ModerateCommentsCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "ModerateComments")]
    public string ModerateCommentsCaption => this[nameof (ModerateCommentsCaption)];

    /// <summary>
    /// Determines if only approved comments will be displayed on public content view controls for comments.
    /// </summary>
    [ResourceEntry("ModerateCommentsDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Determines if only approved comments will be displayed on public content view controls for comments.")]
    public string ModerateCommentsDescription => this[nameof (ModerateCommentsDescription)];

    /// <summary>phrase: DisplayNameFieldCaption</summary>
    [ResourceEntry("DisplayNameFieldCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "DisplayNameField")]
    public string DisplayNameFieldCaption => this[nameof (DisplayNameFieldCaption)];

    /// <summary>
    /// Determines if Name field will be displayed on public content view controls for comments.
    /// </summary>
    [ResourceEntry("DisplayNameFieldDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Determines if Name field will be displayed on public content view controls for comments.")]
    public string DisplayNameFieldDescription => this[nameof (DisplayNameFieldDescription)];

    /// <summary>phrase: DisplayEmailFieldCaption</summary>
    [ResourceEntry("DisplayEmailFieldCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "DisplayEmailField")]
    public string DisplayEmailFieldCaption => this[nameof (DisplayEmailFieldCaption)];

    /// <summary>
    /// Determines if Email field will be displayed on public content view controls for comments.
    /// </summary>
    [ResourceEntry("DisplayEmailFieldDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Determines if Email field will be displayed on public content view controls for comments.")]
    public string DisplayEmailFieldDescription => this[nameof (DisplayEmailFieldDescription)];

    /// <summary>phrase: DisplayWebSiteFieldCaption</summary>
    [ResourceEntry("DisplayWebSiteFieldCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "DisplayWebSiteField")]
    public string DisplayWebSiteFieldCaption => this[nameof (DisplayWebSiteFieldCaption)];

    /// <summary>
    /// phares: Determines if Website field will be displayed on public content view controls for comments.
    /// </summary>
    [ResourceEntry("DisplayWebSiteFieldDescription", Description = "Describes this configuration element.", LastModified = "2012/01/05", Value = "Determines if Website field will be displayed on public content view controls for comments.")]
    public string DisplayWebSiteFieldDescription => this[nameof (DisplayWebSiteFieldDescription)];

    /// <summary>phrase: DisplayMessageFieldCaption</summary>
    [ResourceEntry("DisplayMessageFieldCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "DisplayMessageField")]
    public string DisplayMessageFieldCaption => this[nameof (DisplayMessageFieldCaption)];

    /// <summary>
    /// Determines if Message field will be displayed on public content view controls for comments.
    /// </summary>
    [ResourceEntry("DisplayMessageFieldDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Determines if Message field will be displayed on public content view controls for comments.")]
    public string DisplayMessageFieldDescription => this[nameof (DisplayMessageFieldDescription)];

    /// <summary>phrase: IsNameFieldRequiredCaption</summary>
    [ResourceEntry("IsNameFieldRequiredCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "IsNameFieldRequired")]
    public string IsNameFieldRequiredCaption => this[nameof (IsNameFieldRequiredCaption)];

    /// <summary>
    /// Determines if only name field will be required on public content view controls for comments.
    /// </summary>
    [ResourceEntry("IsNameFieldRequiredDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines if only name field will be required on public content view controls for comments.")]
    public string IsNameFieldRequiredDescription => this[nameof (IsNameFieldRequiredDescription)];

    /// <summary>phrase: IsEmailFieldRequiredCaption</summary>
    [ResourceEntry("IsEmailFieldRequiredCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "IsEmailFieldRequired")]
    public string IsEmailFieldRequiredCaption => this[nameof (IsEmailFieldRequiredCaption)];

    /// <summary>
    /// Defines if only Email field will be required on public content view controls for comments.
    /// </summary>
    [ResourceEntry("IsEmailFieldRequiredDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines if only Email field will be required on public content view controls for comments.")]
    public string IsEmailFieldRequiredDescription => this[nameof (IsEmailFieldRequiredDescription)];

    /// <summary>phrase: IsMessageFieldRequiredCaption</summary>
    [ResourceEntry("IsMessageFieldRequiredCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "IsMessageFieldRequired")]
    public string IsMessageFieldRequiredCaption => this[nameof (IsMessageFieldRequiredCaption)];

    /// <summary>
    /// Defines if only Message field will be required on public content view controls for comments.
    /// </summary>
    [ResourceEntry("IsMessageFieldRequiredDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines if only Message field will be required on public content view controls for comments.")]
    public string IsMessageFieldRequiredDescription => this[nameof (IsMessageFieldRequiredDescription)];

    /// <summary>phrase: IsWebSiteFieldRequiredCaption</summary>
    [ResourceEntry("IsWebSiteFieldRequiredCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "IsWebSiteFieldRequired")]
    public string IsWebSiteFieldRequiredCaption => this[nameof (IsWebSiteFieldRequiredCaption)];

    /// <summary>
    /// phrase: Defines if only Website field will be required on public content view controls for comments.
    /// </summary>
    [ResourceEntry("IsWebSiteFieldRequiredDescription", Description = "Describes this configuration element.", LastModified = "2012/01/05", Value = "Defines if only Website field will be required on public content view controls for comments.")]
    public string IsWebSiteFieldRequiredDescription => this[nameof (IsWebSiteFieldRequiredDescription)];

    /// <summary>phrase: UseSpamProtectionImageCaption</summary>
    [ResourceEntry("UseSpamProtectionImageCaption", Description = "The display title of this configuration element.", LastModified = "2010/08/18", Value = "UseSpamProtectionImage")]
    public string UseSpamProtectionImageCaption => this[nameof (UseSpamProtectionImageCaption)];

    /// <summary>
    /// Defines if spam protection will be used on public content view controls for comments.
    /// </summary>
    [ResourceEntry("UseSpamProtectionImageDescription", Description = "Describes this configuration element.", LastModified = "2010/08/18", Value = "Defines if spam protection will be used on public content view controls for comments.")]
    public string UseSpamProtectionImageDescription => this[nameof (UseSpamProtectionImageDescription)];

    /// <summary>The comments view controls configuration settings.</summary>
    [ResourceEntry("CommentsViewConfig", Description = "Describes configuration element.", LastModified = "2010/08/10", Value = "Defines Comments view controls settings.")]
    public string CommentsViewConfig => this[nameof (CommentsViewConfig)];

    /// <summary>The comments view controls configuration settings.</summary>
    [ResourceEntry("CommentsViewConfigDescription", Description = "Describes configuration element.", LastModified = "2010/08/10", Value = "Defines comments view controls settings.")]
    public string CommentsViewConfigDescription => this[nameof (CommentsViewConfigDescription)];

    /// <summary>Message: Comments configuration settings</summary>
    [ResourceEntry("CommentsSettingsTitle", Description = "Describes configuration element.", LastModified = "2010/09/02", Value = "Comments Settings")]
    public string CommentsSettingsTitle => this[nameof (CommentsSettingsTitle)];

    /// <summary>Message: Defines configuration settings for comments.</summary>
    [ResourceEntry("CommentsSettingsDescription", Description = "Describes configuration element.", LastModified = "2010/09/02", Value = "Defines configuration settings for comments")]
    public string CommentsSettingsDescription => this[nameof (CommentsSettingsDescription)];

    /// <summary>DateFormatSettings</summary>
    [ResourceEntry("DateFormatSettingsTitle", Description = "The title of the configuration element.", LastModified = "2010/02/23", Value = "DateFormatSettings")]
    public string DateFormatSettingsTitle => this[nameof (DateFormatSettingsTitle)];

    /// <summary>Represents DateFormat property collection.</summary>
    [ResourceEntry("DateFormatSettingsDescription", Description = "Describes configuration element", LastModified = "2010/02/23", Value = "RepresentsDateFormat property collection.")]
    public string DateFormatSettingsDescription => this[nameof (DateFormatSettingsDescription)];

    /// <summary>Resource strings for DateFormatElement class.</summary>
    [ResourceEntry("DateFormatTitle", Description = "The title of sorting expression", LastModified = "2010/04/23", Value = "The title of sorting expression")]
    public string DateFormatTitle => this[nameof (DateFormatTitle)];

    /// <summary>Resource strings for DateFormatElement class.</summary>
    [ResourceEntry("DateFormatTitleDescription", Description = "The title of sorting expression configuration description.", LastModified = "2010/04/23", Value = "The title of sorting expression configuration description")]
    public string DateFormatTitleDescription => this[nameof (DateFormatTitleDescription)];

    /// <summary>phrase: Date format</summary>
    [ResourceEntry("DateFormatElementCaption", Description = "phrase: Date format", LastModified = "2010/10/22", Value = "Date format")]
    public string DateFormatElementCaption => this[nameof (DateFormatElementCaption)];

    /// <summary>Resource strings for DateFormatElement class.</summary>
    [ResourceEntry("DateFormatElementDescription", Description = "Sorting expression configuration description.", LastModified = "2010/04/23", Value = "Sorting expression configuration data")]
    public string DateFormatElementDescription => this[nameof (DateFormatElementDescription)];

    /// <summary>phrase: The sorting expression value.</summary>
    [ResourceEntry("DateFormatValueTitle", Description = "phrase: The sorting expression value.", LastModified = "2010/04/27", Value = "The sorting expression value.")]
    public string DateFormatValueTitle => this[nameof (DateFormatValueTitle)];

    /// <summary>
    /// phrase: The value of sorting expression configuration data.
    /// </summary>
    [ResourceEntry("DateFormatValueDescription", Description = "phrase: The value of sorting expression configuration data.", LastModified = "2010/04/27", Value = "The value of sorting expression configuration data.")]
    public string DateFormatValueDescription => this[nameof (DateFormatValueDescription)];

    /// <summary>
    /// Message: Provides information who can post comments. List of possible values: Everyone, RegisteredUsers. The default value is Everyone.
    /// </summary>
    [ResourceEntry("PostRights", Description = "Describes configuration element.", LastModified = "2010/08/20", Value = "Provides information who can post comments. List of possible values: Everyone, RegisteredUsers. The default value is Everyone.")]
    public string PostRights => this[nameof (PostRights)];

    /// <summary>Caption for Url Shortener Service</summary>
    [ResourceEntry("ShortenerServiceUrlCaption", Description = "Describes the shortener service url title", LastModified = "2010/09/29", Value = "Shortener Service Url")]
    public string ShortenerServiceUrlCaption => this[nameof (ShortenerServiceUrlCaption)];

    /// <summary>
    /// Description of field definition configuration settings.
    /// </summary>
    [ResourceEntry("ShortenerServiceUrlDescription", Description = "Describes configuration element.", LastModified = "2010/09/29", Value = "Defines the url that is going to be used for url shortening.")]
    public string ShortenerServiceUrlDescription => this[nameof (ShortenerServiceUrlDescription)];

    /// <summary>
    /// Description of field definition configuration settings.
    /// </summary>
    [ResourceEntry("EnabledUrlShortenerElementDescription", Description = "Describes the currently enabled shortener configuration settings.", LastModified = "2010/09/30", Value = "Defines the currently enabled settings used for url shortening.")]
    public string EnabledUrlShortenerElementDescription => this[nameof (EnabledUrlShortenerElementDescription)];

    /// <summary>Caption of field definition configuration settings.</summary>
    [ResourceEntry("ShortenerServiceSettingsCaption", Description = "Describes the shortener configuration settings.", LastModified = "2010/09/30", Value = "Defines the settings for url shortening.")]
    public string ShortenerServiceSettingsCaption => this[nameof (ShortenerServiceSettingsCaption)];

    /// <summary>
    /// Description of field definition configuration settings.
    /// </summary>
    [ResourceEntry("ShortenerServiceSettingsDescription", Description = "Defines the configuration settings for url shortening.", LastModified = "2010/09/30", Value = "Defines the settings used for url shortening.")]
    public string ShortenerServiceSettingsDescription => this[nameof (ShortenerServiceSettingsDescription)];

    /// <summary>
    /// Description of field definition configuration settings.
    /// </summary>
    [ResourceEntry("UrlShortenerConfigDescriptions", Description = "Defines the configuration settings for shortening.", LastModified = "2010/10/01", Value = "The configuration descriptions for shortening url-s.")]
    public string UrlShortenerConfigDescriptions => this[nameof (UrlShortenerConfigDescriptions)];

    /// <summary>DataProviderConfigurationDescriptions</summary>
    [ResourceEntry("DataProviderConfiguration", Description = "Contains the data provider configuration", LastModified = "2010/09/29", Value = "Data provider configuration")]
    public string DataProviderConfiguration => this[nameof (DataProviderConfiguration)];

    /// <summary>phrase: Blob storage</summary>
    [ResourceEntry("BlobStorageTitle", Description = "phrase: Blob storage", LastModified = "2011/06/24", Value = "Blob storage")]
    public string BlobStorageTitle => this[nameof (BlobStorageTitle)];

    /// <summary>phrase: Blob storage configuration.</summary>
    [ResourceEntry("BlobStorageDescription", Description = "phrase: Blob storage configuration.", LastModified = "2011/06/24", Value = "Blob storage configuration.")]
    public string BlobStorageDescription => this[nameof (BlobStorageDescription)];

    /// <summary>phrase: Default blob storage provider</summary>
    [ResourceEntry("BlobStorageDefaultProviderTitle", Description = "phrase: Default blob storage provider", LastModified = "2011/06/24", Value = "Default blob storage provider")]
    public string BlobStorageDefaultProviderTitle => this[nameof (BlobStorageDefaultProviderTitle)];

    /// <summary>phrase: The key of the default blob storage provider.</summary>
    [ResourceEntry("BlobStorageDefaultProviderDescription", Description = "phrase: The key of the default blob storage provider.", LastModified = "2011/06/24", Value = "The key of the default blob storage provider.")]
    public string BlobStorageDefaultProviderDescription => this[nameof (BlobStorageDefaultProviderDescription)];

    /// <summary>phrase: Blob storage providers</summary>
    [ResourceEntry("BlobStorageProvidersTitle", Description = "phrase: Blob storage providers", LastModified = "2011/06/24", Value = "Blob storage providers")]
    public string BlobStorageProvidersTitle => this[nameof (BlobStorageProvidersTitle)];

    /// <summary>
    /// phrase: Concrete physical blob storage locations, represented by parameterized instances of the supported blob storage provider types.
    /// </summary>
    [ResourceEntry("BlobStorageProvidersDescription", Description = "phrase: Concrete physical blob storage locations, represented by parameterized instances of the supported blob storage provider types.", LastModified = "2011/06/24", Value = "Concrete physical blob storage locations, represented by parameterized instances of the supported blob storage provider types.")]
    public string BlobStorageProvidersDescription => this[nameof (BlobStorageProvidersDescription)];

    /// <summary>word: Name</summary>
    [ResourceEntry("BlobStorageTypeNameTitle", Description = "word: Name", LastModified = "2011/06/29", Value = "Name")]
    public string BlobStorageTypeNameTitle => this[nameof (BlobStorageTypeNameTitle)];

    /// <summary>phrase: Unique name for the provider, used as a key.</summary>
    [ResourceEntry("BlobStorageTypeNameDescription", Description = "phrase: Unique name for the provider, used as a key.", LastModified = "2011/06/29", Value = "Unique name for the provider, used as a key.")]
    public string BlobStorageTypeNameDescription => this[nameof (BlobStorageTypeNameDescription)];

    /// <summary>phrase: Blob storage types</summary>
    [ResourceEntry("BlobStorageTypesTitle", Description = "phrase: Blob storage types", LastModified = "2011/06/24", Value = "Blob storage types")]
    public string BlobStorageTypesTitle => this[nameof (BlobStorageTypesTitle)];

    /// <summary>
    /// phrase: The kinds of blob storage that the Libraries module can use.
    /// </summary>
    [ResourceEntry("BlobStorageTypesDescription", Description = "phrase: The kinds of blob storage that the Libraries module can use.", LastModified = "2011/06/24", Value = "The kinds of blob storage that the Libraries module can use.")]
    public string BlobStorageTypesDescription => this[nameof (BlobStorageTypesDescription)];

    /// <summary>phrase: Blob storage type</summary>
    [ResourceEntry("BlobStorageTypeTitle", Description = "phrase: Blob storage type", LastModified = "2011/06/24", Value = "Blob storage type")]
    public string BlobStorageTypeTitle => this[nameof (BlobStorageTypeTitle)];

    /// <summary>phrase: A kind of blob storage.</summary>
    [ResourceEntry("BlobStorageTypeDescription", Description = "phrase: A kind of blob storage.", LastModified = "2011/06/24", Value = "A kind of blob storage.")]
    public string BlobStorageTypeDescription => this[nameof (BlobStorageTypeDescription)];

    /// <summary>phrase: Provider type</summary>
    [ResourceEntry("BlobStorageTypeProviderTypeTitle", Description = "phrase: Provider type", LastModified = "2011/06/24", Value = "Provider type")]
    public string BlobStorageTypeProviderTypeTitle => this[nameof (BlobStorageTypeProviderTypeTitle)];

    /// <summary>
    /// phrase: The .NET class that implements this kind of blob storage.
    /// </summary>
    [ResourceEntry("BlobStorageTypeProviderTypeDescription", Description = "phrase: The .NET class that implements this kind of blob storage.", LastModified = "2011/06/24", Value = "The .NET class that implements this kind of blob storage.")]
    public string BlobStorageTypeProviderTypeDescription => this[nameof (BlobStorageTypeProviderTypeDescription)];

    /// <summary>phrase: Title</summary>
    [ResourceEntry("BlobStorageTypeTitleTitle", Description = "phrase: Title", LastModified = "2011/06/24", Value = "Title")]
    public string BlobStorageTypeTitleTitle => this[nameof (BlobStorageTypeTitleTitle)];

    /// <summary>
    /// phrase: User friendly name for this kind of blob storage.
    /// </summary>
    [ResourceEntry("BlobStorageTypeTitleDescription", Description = "phrase: User friendly name for this kind of blob storage.", LastModified = "2011/06/24", Value = "User friendly name for this kind of blob storage.")]
    public string BlobStorageTypeTitleDescription => this[nameof (BlobStorageTypeTitleDescription)];

    /// <summary>phrase: Description</summary>
    [ResourceEntry("BlobStorageTypeDescriptionTitle", Description = "phrase: Description", LastModified = "2011/06/29", Value = "Description")]
    public string BlobStorageTypeDescriptionTitle => this[nameof (BlobStorageTypeDescriptionTitle)];

    /// <summary>phrase: Describes this kind of blob storage.</summary>
    [ResourceEntry("BlobStorageTypeDescriptionDescription", Description = "phrase: Describes this kind of blob storage.", LastModified = "2011/06/29", Value = "Describes this kind of blob storage.")]
    public string BlobStorageTypeDescriptionDescription => this[nameof (BlobStorageTypeDescriptionDescription)];

    /// <summary>VersioningConfigurationDescriptions</summary>
    [ResourceEntry("VersioningConfigurationDescriptions", Description = "Versioning configuration", LastModified = "2010/09/29", Value = "Versioning configuration")]
    public string VersioningConfigurationDescriptions => this[nameof (VersioningConfigurationDescriptions)];

    /// <summary>Default Catalogues Folder</summary>
    [ResourceEntry("DefaultCataloguesFolder", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Default Catalogues Folder")]
    public string DefaultCataloguesFolder => this[nameof (DefaultCataloguesFolder)];

    /// <summary>
    /// Defines the default storage location for index catalogues.
    /// </summary>
    [ResourceEntry("DefaultCataloguesFolderDescription", Description = "Describes configuration element.", LastModified = "2009/09/10", Value = "Defines the default storage location for index catalogues.")]
    public string DefaultCataloguesFolderDescription => this[nameof (DefaultCataloguesFolderDescription)];

    /// <summary>Search stop words</summary>
    [ResourceEntry("StopWordsTitle", Description = "Describes configuration element.", LastModified = "2011/09/08", Value = "Search stop words.")]
    public string StopWordsTitle => this[nameof (StopWordsTitle)];

    /// <summary>Defines the search stop words.</summary>
    [ResourceEntry("StopWordsDescription", Description = "Describes configuration element.", LastModified = "2011/09/08", Value = "Defines the search stop words.")]
    public string StopWordsDescription => this[nameof (StopWordsDescription)];

    /// <summary>
    /// Translated value, similar to "Sitemap node permissions"
    /// </summary>
    /// <value>Caption of the SitemapNodePermissionsConfig configuration section</value>
    [ResourceEntry("SitemapNodePermissionsConfigCaption", Description = "Caption of the SitemapNodePermissionsConfig configuration section", LastModified = "2011/01/12", Value = "Sitemap node permissions")]
    public string SitemapNodePermissionsConfigCaption => this[nameof (SitemapNodePermissionsConfigCaption)];

    /// <summary>
    /// Translated value, similar to "Allows the user to add additional security checks for individual page nodes"
    /// </summary>
    /// <value>Caption of the SitemapNodePermissionsConfig configuration section</value>
    [ResourceEntry("SitemapNodePermissionsConfigDescription", Description = "Description of the SitemapNodePermissionsConfig configuration section", LastModified = "2011/01/12", Value = "Allows the user to add additional security checks for individual page nodes")]
    public string SitemapNodePermissionsConfigDescription => this[nameof (SitemapNodePermissionsConfigDescription)];

    /// <summary>
    /// Translated value, similar to "Sitemap node permissions"
    /// </summary>
    /// <value>Title of the SitemapNodePermissionsConfig.NodePermissionMappings property</value>
    [ResourceEntry("SitemapNodePermissionMappingsTitle", Description = "Title of the SitemapNodePermissionsConfig.NodePermissionMappings property", LastModified = "2011/01/12", Value = "Sitemap node permissions")]
    public string SitemapNodePermissionMappingsTitle => this[nameof (SitemapNodePermissionMappingsTitle)];

    /// <summary>Title of the WriteLockRemoveInterval property</summary>
    [ResourceEntry("WriteLockRemoveIntervalTitle", Description = "Title of the WriteLockRemoveInterval property", LastModified = "2013/07/01", Value = "Remove interval (in seconds) for write.lock file in search catalogs")]
    public string WriteLockRemoveIntervalTitle => this[nameof (WriteLockRemoveIntervalTitle)];

    /// <summary>
    /// Translated value, similar to "Allows the user to add additional security checks for individual page nodes"
    /// </summary>
    /// <value>Caption of the SitemapNodePermissionsConfig configuration section</value>
    [ResourceEntry("WriteLockRemoveIntervalDescription", Description = "Description of the WriteLockRemoveInterval property", LastModified = "2013/07/01", Value = "Defines how long the write.lock file in search catalog should be kept before it can be safely deleted.")]
    public string WriteLockRemoveIntervalDescription => this[nameof (WriteLockRemoveIntervalDescription)];

    /// <summary>Title of the EnableFilterByViewPermissions property</summary>
    [ResourceEntry("EnableFilterByViewPermissionsTitle", Description = "Title of the EnableFilterByViewPermissions property", LastModified = "2014/10/07", Value = "Enable filtering search results by view permissions")]
    public string EnableFilterByViewPermissionsTitle => this[nameof (EnableFilterByViewPermissionsTitle)];

    /// <summary>
    /// When enabled Search results will display only content for which the user has view permission
    /// </summary>
    /// <value>When enabled Search results will display only content for which the user has view permission</value>
    [ResourceEntry("EnableFilterByViewPermissionsDescription", Description = "Description of the EnableFilterByViewPermissions property", LastModified = "2014/10/07", Value = "When enabled Search results will display only content for which the user has view permission")]
    public string EnableFilterByViewPermissionsDescription => this[nameof (EnableFilterByViewPermissionsDescription)];

    /// <summary>Label: Azure Search</summary>
    /// <value>Azure Search</value>
    [ResourceEntry("AzureSearch", Description = "Label: Azure Search", LastModified = "2014/10/10", Value = "Azure Search")]
    public string AzureSearch => this[nameof (AzureSearch)];

    /// <summary>Label: Lucene Search</summary>
    /// <value>Lucene Search</value>
    [ResourceEntry("LuceneSearch", Description = "Label: Lucene Search", LastModified = "2014/10/10", Value = "Lucene Search")]
    public string LuceneSearch => this[nameof (LuceneSearch)];

    /// <summary>Label: API version</summary>
    /// <value>API version</value>
    [ResourceEntry("ApiVersionTitle", Description = "Label: API version", LastModified = "2014/10/10", Value = "API version")]
    public string ApiVersionTitle => this[nameof (ApiVersionTitle)];

    /// <summary>Label: Index Folder</summary>
    /// <value>Index Folder</value>
    [ResourceEntry("IndexFolder", Description = "Label: Index Folder", LastModified = "2014/10/10", Value = "Index Folder")]
    public string IndexFolder => this[nameof (IndexFolder)];

    /// <summary>Phrase: Index buffer capacity</summary>
    /// <value>Index buffer capacity</value>
    [ResourceEntry("IndexBufferCapacityTitle", Description = "Phrase: Index buffer capacity", LastModified = "2014/06/11", Value = "Index buffer capacity")]
    public string IndexBufferCapacityTitle => this[nameof (IndexBufferCapacityTitle)];

    /// <summary>
    /// Phrase: The capacity of the index buffer. When the buffer is full, it will be automatically flushed.
    /// </summary>
    /// <value>Index Folder</value>
    [ResourceEntry("IndexBufferCapacityDescription", Description = "Phrase: The capacity of the index buffer. When the buffer is full, it will be automatically flushed.", LastModified = "2014/06/11", Value = "The capacity of the index buffer. When the buffer is full, it will be automatically flushed.")]
    public string IndexBufferCapacityDescription => this[nameof (IndexBufferCapacityDescription)];

    /// <summary>Phrase: Index buffer flush interval</summary>
    /// <value>Index buffer flush interval</value>
    [ResourceEntry("IndexBufferFlushIntervalTitle", Description = "Phrase: Index buffer flush interval", LastModified = "2014/06/11", Value = "Index buffer flush interval")]
    public string IndexBufferFlushIntervalTitle => this[nameof (IndexBufferFlushIntervalTitle)];

    /// <summary>
    /// Phrase: The interval for flushing the index buffer in milliseconds.
    /// </summary>
    /// <value>Index Folder</value>
    [ResourceEntry("IndexBufferFlushIntervalDescription", Description = "Phrase: The interval for flushing the index buffer in milliseconds.", LastModified = "2014/06/11", Value = "The interval for flushing the index buffer in milliseconds.")]
    public string IndexBufferFlushIntervalDescription => this[nameof (IndexBufferFlushIntervalDescription)];

    /// <summary>
    /// Translated value, similar to "Maps page nodes to security actions"
    /// </summary>
    /// <value>Description of the SitemapNodePermissionsConfig.NodePermissionMappings property</value>
    [ResourceEntry("SitemapNodePermissionMappingsDescription", Description = "Description of the SitemapNodePermissionsConfig.NodePermissionMappings property", LastModified = "2011/01/12", Value = "Maps page nodes to security actions")]
    public string SitemapNodePermissionMappingsDescription => this[nameof (SitemapNodePermissionMappingsDescription)];

    /// <summary>
    /// Translated value, similar to "Sitemap node permissions"
    /// </summary>
    /// <value>Caption of the SitemapNodePermissionElement configuration element</value>
    [ResourceEntry("SitemapNodePermissionElementCaption", Description = "Caption of the SitemapNodePermissionElement configuration element", LastModified = "2011/01/12", Value = "Sitemap node permissions")]
    public string SitemapNodePermissionElementCaption => this[nameof (SitemapNodePermissionElementCaption)];

    /// <summary>
    /// Translated value, similar to "Allows the user to add additional security checks for individual page nodes"
    /// </summary>
    /// <value>Description of the SitemapNodePermissionElement configuration element</value>
    [ResourceEntry("SitemapNodePermissionElementDescription", Description = "Description of the SitemapNodePermissionElement configuration element", LastModified = "2011/01/12", Value = "Allows the user to add additional security checks for individual page nodes")]
    public string SitemapNodePermissionElementDescription => this[nameof (SitemapNodePermissionElementDescription)];

    /// <summary>Translated value, similar to "Page node ID"</summary>
    /// <value>Title of the SitemapNodePermissionElement.PageNodeId property</value>
    [ResourceEntry("SitemapNodeMappingNodeIdTitle", Description = "Title of the SitemapNodePermissionElement.PageNodeId property", LastModified = "2011/01/12", Value = "Page node ID")]
    public string SitemapNodeMappingNodeIdTitle => this[nameof (SitemapNodeMappingNodeIdTitle)];

    /// <summary>
    /// Translated value, similar to "ID of the page node that is mapped to a (set, action) pair"
    /// </summary>
    /// <value>Description of the SitemapNodePermissionElement.PageNodeId property</value>
    [ResourceEntry("SitemapNodeMappingNodeIdDescription", Description = "Description of the SitemapNodePermissionElement.PageNodeId property", LastModified = "2011/01/12", Value = "ID of the page node that is mapped to a (set, action) pair")]
    public string SitemapNodeMappingNodeIdDescription => this[nameof (SitemapNodeMappingNodeIdDescription)];

    /// <summary>Translated value, similar to "Permission set name"</summary>
    /// <value>Title of the SitemapNodePermissionElement.SetName property</value>
    [ResourceEntry("SitemapNodeMappingSetNameTitle", Description = "Title of the SitemapNodePermissionElement.SetName property", LastModified = "2011/01/12", Value = "Permission set name")]
    public string SitemapNodeMappingSetNameTitle => this[nameof (SitemapNodeMappingSetNameTitle)];

    /// <summary>
    /// Translated value, similar to "Name of the permission set"
    /// </summary>
    /// <value>Description of the SitemapNodePermissionElement.SetName property</value>
    [ResourceEntry("SitemapNodeMappingSetNameDescription", Description = "Description of the SitemapNodePermissionElement.SetName property", LastModified = "2011/01/12", Value = "Name of the permission set")]
    public string SitemapNodeMappingSetNameDescription => this[nameof (SitemapNodeMappingSetNameDescription)];

    /// <summary>Translated value, similar to "Security action name"</summary>
    /// <value>Title of the SitemapNodePermissionElement.ActionName property</value>
    [ResourceEntry("SitemapNodeMappingActionNameTitle", Description = "Title of the SitemapNodePermissionElement.ActionName property", LastModified = "2011/01/12", Value = "Security action name")]
    public string SitemapNodeMappingActionNameTitle => this[nameof (SitemapNodeMappingActionNameTitle)];

    /// <summary>
    /// Translated value, similar to "Security action name that is part of the specified permission set"
    /// </summary>
    /// <value>Description of the SitemapNodePermissionElement.ActionName property</value>
    [ResourceEntry("SitemapNodeMappingActionNameDescription", Description = "Description of the SitemapNodePermissionElement.ActionName property", LastModified = "2011/01/12", Value = "Security action name that is part of the specified permission set")]
    public string SitemapNodeMappingActionNameDescription => this[nameof (SitemapNodeMappingActionNameDescription)];

    /// <summary>phrase: Type implementations mapping</summary>
    [ResourceEntry("TypeImplementationsMappingTitle", Description = "phrase: Type implementations mapping", LastModified = "2011/01/14", Value = "Type implementations mapping")]
    public string TypeImplementationsMappingTitle => this[nameof (TypeImplementationsMappingTitle)];

    /// <summary>
    /// phrase: Maps a, possibly abstract, type or interface to a list of types that inherit/implement it. This setting is used primarily to suggest non-abstract configuration collection element types on element creation in the advanced settings UI.
    /// </summary>
    [ResourceEntry("TypeImplementationsMappingDescription", Description = "phrase: Maps a, possibly abstract, type or interface to types that inherit/implement it. This setting is used primarily to suggest non-abstract configuration collection element types on element creation in the advanced settings UI.", LastModified = "2011/01/14", Value = "Maps a, possibly abstract, type or interface to types that inherit/implement it. This setting is used primarily to suggest non-abstract configuration collection element types on element creation in the advanced settings UI.")]
    public string TypeImplementationsMappingDescription => this[nameof (TypeImplementationsMappingDescription)];

    /// <summary>phrase: Type</summary>
    [ResourceEntry("TypeImplementationsMappingElementTypeTitle", Description = "phrase: Type", LastModified = "2011/01/14", Value = "Type")]
    public string TypeImplementationsMappingElementTypeTitle => this[nameof (TypeImplementationsMappingElementTypeTitle)];

    /// <summary>phrase: A, possibly abstract, type or interface.</summary>
    [ResourceEntry("TypeImplementationsMappingElementTypeDescription", Description = "phrase: A, possibly abstract, type or interface.", LastModified = "2011/01/14", Value = "A, possibly abstract, type or interface.")]
    public string TypeImplementationsMappingElementTypeDescription => this[nameof (TypeImplementationsMappingElementTypeDescription)];

    /// <summary>phrase: Implementations</summary>
    [ResourceEntry("TypeImplementationsMappingElementImplementationsTitle", Description = "phrase: Implementations", LastModified = "2011/01/14", Value = "Implementations")]
    public string TypeImplementationsMappingElementImplementationsTitle => this[nameof (TypeImplementationsMappingElementImplementationsTitle)];

    /// <summary>
    /// phrase: List of types that inherit/implement the type specified in the Type property.
    /// </summary>
    [ResourceEntry("TypeImplementationsMappingElementImplementationsDescription", Description = "phrase: List of types that inherit/implement the type specified in the Type property.", LastModified = "2011/01/14", Value = "List of types that inherit/implement the type specified in the Type property.")]
    public string TypeImplementationsMappingElementImplementationsDescription => this[nameof (TypeImplementationsMappingElementImplementationsDescription)];

    /// <summary>phrase: Type</summary>
    [ResourceEntry("TypeImplementationsElementTypeTitle", Description = "phrase: Type", LastModified = "2011/01/14", Value = "Type")]
    public string TypeImplementationsElementTypeTitle => this[nameof (TypeImplementationsElementTypeTitle)];

    /// <summary>
    /// phrase: A types that inherit/implement a, possibly abstract, type or interface.
    /// </summary>
    [ResourceEntry("TypeImplementationsElementTypeDescription", Description = "phrase: A types that inherit/implement a, possibly abstract, type or interface.", LastModified = "2011/01/14", Value = "A types that inherit/implement a, possibly abstract, type or interface.")]
    public string TypeImplementationsElementTypeDescription => this[nameof (TypeImplementationsElementTypeDescription)];

    /// <summary>phrase: Virtual path settings</summary>
    [ResourceEntry("VirtualPathSettingsTitle", Description = "phrase: Virtual path settings", LastModified = "2011/01/24", Value = "Virtual path settings")]
    public string VirtualPathSettingsTitle => this[nameof (VirtualPathSettingsTitle)];

    /// <summary>
    /// phrase: Defines the virtual path definitions for the embedded resources.
    /// </summary>
    [ResourceEntry("VirtualPathSettingsDescription", Description = "phrase: Defines the virtual path definitions for the embedded resources.", LastModified = "2011/01/24", Value = "Defines the virtual path definitions for the embedded resources.")]
    public string VirtualPathSettingsDescription => this[nameof (VirtualPathSettingsDescription)];

    /// <summary>phrase: Virtual paths</summary>
    [ResourceEntry("VirtualPathsTitle", Description = "phrase: Virtual paths", LastModified = "2011/01/24", Value = "Virtual paths")]
    public string VirtualPathsTitle => this[nameof (VirtualPathsTitle)];

    /// <summary>phrase: A collection of registered virtual paths.</summary>
    [ResourceEntry("VirtualPathsDescription", Description = "phrase: A collection of registered virtual paths.", LastModified = "2011/01/24", Value = "A collection of registered virtual paths.")]
    public string VirtualPathsDescription => this[nameof (VirtualPathsDescription)];

    /// <summary>phrase: Allow separate users per site</summary>
    [ResourceEntry("SeparateUsersPerSiteTitle", Description = "phrase: Allow separate users per site", LastModified = "2021/01/06", Value = "Allow separate users per site")]
    public string SeparateUsersPerSiteTitle => this[nameof (SeparateUsersPerSiteTitle)];

    /// <summary>
    /// phrase: By default, all users can manage all sites, depending on their permissions and roles. Select this option to allow different sites to be managed by different user groups. Details: progress.com/sf-users-per-site
    /// </summary>
    [ResourceEntry("SeparateUsersPerSiteSettingsDescription", Description = "By default, all users can manage all sites, depending on their permissions and roles. Select this option to allow different sites to be managed by different user groups. Details: progress.com/sf-users-per-site", LastModified = "2021/01/06", Value = "By default, all users can manage all sites, depending on their permissions and roles. Select this option to allow different sites to be managed by different user groups. Details: progress.com/sf-users-per-site")]
    public string SeparateUsersPerSiteSettingsDescription => this[nameof (SeparateUsersPerSiteSettingsDescription)];

    /// <summary>phrase: Users per site</summary>
    [ResourceEntry("UsersPerSiteSettingsTitle", Description = "phrase: Users per site", LastModified = "2021/01/06", Value = "Users per site")]
    public string UsersPerSiteSettingsTitle => this[nameof (UsersPerSiteSettingsTitle)];

    /// <summary>
    /// phrase: Applies only to Sitefinity CMS Enterprise Edition. To see your edition, go to Administration &gt; Version &amp; Licensing.
    /// </summary>
    [ResourceEntry("UsersPerSiteSettingsDescription", Description = "phrase: Applies only to Sitefinity CMS Enterprise Edition. To see your edition, go to Administration &gt; Version &amp; Licensing.", LastModified = "2021/03/05", Value = "Applies only to Sitefinity CMS Enterprise Edition. To see your edition, go to Administration > Version & Licensing.")]
    public string UsersPerSiteSettingsDescription => this[nameof (UsersPerSiteSettingsDescription)];

    /// <summary>phrase: Virtual wildcards</summary>
    [ResourceEntry("VirtualWildcardsTitle", Description = "phrase: Virtual wildcards", LastModified = "2011/01/24", Value = "Virtual wildcards")]
    public string VirtualWildcardsTitle => this[nameof (VirtualWildcardsTitle)];

    /// <summary>phrase: A collection of registered virtual wildcards.</summary>
    [ResourceEntry("VirtualWildcardsDescription", Description = "phrase: A collection of registered virtual wildcards.", LastModified = "2011/01/24", Value = "A collection of registered virtual wildcards.")]
    public string VirtualWildcardsDescription => this[nameof (VirtualWildcardsDescription)];

    /// <summary>phrase: Newsletters settings</summary>
    [ResourceEntry("NewslettersConfigTitle", Description = "phrase: Newsletters settings", LastModified = "2011/01/24", Value = "Newsletters settings")]
    public string NewslettersConfigTitle => this[nameof (NewslettersConfigTitle)];

    /// <summary>phrase: Configuration section for newsletters.</summary>
    [ResourceEntry("NewslettersConfigDescription", Description = "phrase: Configuration section for newsletters.", LastModified = "2011/01/24", Value = "Configuration section for newsletters.")]
    public string NewslettersConfigDescription => this[nameof (NewslettersConfigDescription)];

    /// <summary>phrase: Collect bounces every (minutes)</summary>
    /// <value>Collect bounces every (minutes)</value>
    [ResourceEntry("NewslettersBounceCollectionIntervalMinutesTitle", Description = "phrase: Collect bounces every (minutes)", LastModified = "2015/06/12", Value = "Collect bounces every (minutes)")]
    public string NewslettersBounceCollectionIntervalMinutesTitle => this[nameof (NewslettersBounceCollectionIntervalMinutesTitle)];

    /// <summary>
    /// phrase: The amount of interval in minutes to retry after an issue was sent to collect bounce messages from the POP3 account.
    /// </summary>
    /// <value>The amount of interval in minutes to retry after an issue was sent to collect bounce messages from the POP3 account.</value>
    [ResourceEntry("NewslettersBounceCollectionIntervalMinutesDescription", Description = "phrase: The amount of interval in minutes to retry after an issue was sent to collect bounce messages from the POP3 account.", LastModified = "2015/06/12", Value = "The amount of interval in minutes to retry after an issue was sent to collect bounce messages from the POP3 account.")]
    public string NewslettersBounceCollectionIntervalMinutesDescription => this[nameof (NewslettersBounceCollectionIntervalMinutesDescription)];

    /// <summary>Caption for load balancing config element</summary>
    [ResourceEntry("WebServerUrlsCaption", Description = "Describes the web server urls title", LastModified = "2011/01/24", Value = "Web server urls")]
    public string WebServerUrlsCaption => this[nameof (WebServerUrlsCaption)];

    /// <summary>Description of load balancing config element</summary>
    [ResourceEntry("WebServerUrlsDescription", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Defines the urls for the web farm.")]
    public string WebServerUrlsDescription => this[nameof (WebServerUrlsDescription)];

    /// <summary>Caption for load balancing config element.</summary>
    [ResourceEntry("URLsCaption", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "WebServerUrls")]
    public string URLsCaption => this[nameof (URLsCaption)];

    /// <summary>Description of load balancing config element</summary>
    [ResourceEntry("URLsDescription", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Defines the list of base urls of web servers that are part of load balanced environment.")]
    public string URLsDescription => this[nameof (URLsDescription)];

    /// <summary>phrase: Disable host header inference</summary>
    [ResourceEntry("LoadBalancingDisableHostHeadersTitle", Description = "phrase: Disable host header inference", LastModified = "2012/11/09", Value = "Disable host header inference")]
    public string LoadBalancingDisableHostHeadersTitle => this[nameof (LoadBalancingDisableHostHeadersTitle)];

    /// <summary>RequestTimeout</summary>
    [ResourceEntry("WebServiceSenderRequestTimeoutTitle", Description = "Describes configuration element.", LastModified = "2013/12/04", Value = "RequestTimeout")]
    public string WebServiceSenderRequestTimeoutTitle => this[nameof (WebServiceSenderRequestTimeoutTitle)];

    /// <summary>
    /// Timeout in milliseconds for the system messages requests.
    /// </summary>
    [ResourceEntry("WebServiceSenderRequestTimeoutDescription", Description = "Describes configuration element.", LastModified = "2013/12/04", Value = "Timeout in milliseconds for the system messages requests.")]
    public string WebServiceSenderRequestTimeoutDescription => this[nameof (WebServiceSenderRequestTimeoutDescription)];

    /// <summary>Cache Provider</summary>
    [ResourceEntry("DefaultCacheProviderTitle", Description = "Describes configuration element.", LastModified = "2018/06/27", Value = "Cache Provider")]
    public string DefaultCacheProviderTitle => this[nameof (DefaultCacheProviderTitle)];

    /// <summary>phrase: Disable host header inference</summary>
    [ResourceEntry("DefaultCacheProviderDescription", Description = "Describes configuration element.", LastModified = "2018/06/27", Value = "Defines the default cache provider.")]
    public string DefaultCacheProviderDescription => this[nameof (DefaultCacheProviderDescription)];

    /// <summary>
    /// phrase: Should be disabled in most cases. When enabled (the checkbox cleared) the value of the 'Host' HTTP header sent in the synchronization requests is set to the host URL part of the first request to the server (hopefully one to the load balancer). This is required in cases when multiple sites share the same instance IP and have no DNS instance names.
    /// </summary>
    [ResourceEntry("LoadBalancingDisableHostHeadersDescription", Description = "Should be disabled in most cases. When enabled (the checkbox cleared) the value of the 'Host' HTTP header sent in the synchronization requests is set to the host URL part of the first request to the server (hopefully one to the load balancer). This is required in cases when multiple sites share the same instance IP and have no DNS instance names.", LastModified = "2012/11/09", Value = "Should be disabled in most cases. When enabled (the checkbox cleared) the value of the 'Host' HTTP header sent in the synchronization requests is set to the host URL part of the first request to the server (hopefully one to the load balancer). This is required in cases when multiple sites share the same instance IP and have no DNS instance names.")]
    public string LoadBalancingDisableHostHeadersDescription => this[nameof (LoadBalancingDisableHostHeadersDescription)];

    /// <summary>Caption for load balancing config element.</summary>
    [ResourceEntry("MsmqSettingsCaption", Description = "Describes configuration element.", LastModified = "2011/04/20", Value = "MSMQ Settings")]
    public string MsmqSettingsCaption => this[nameof (MsmqSettingsCaption)];

    /// <summary>Description of load balancing config element</summary>
    [ResourceEntry("MsmqSettingsDescription", Description = "Describes configuration element.", LastModified = "2011/04/20", Value = "Defines settings for MSMQ.")]
    public string MsmqSettingsDescription => this[nameof (MsmqSettingsDescription)];

    /// <summary>Caption for load balancing config element.</summary>
    [ResourceEntry("RedisSettingsCaption", Description = "Describes configuration element.", LastModified = "2018/09/27", Value = "Redis")]
    public string RedisSettingsCaption => this[nameof (RedisSettingsCaption)];

    /// <summary>Description of load balancing config element</summary>
    [ResourceEntry("RedisSettingsDescription", Description = "Describes configuration element.", LastModified = "2015/06/20", Value = "Defines settings for Redis.")]
    public string RedisSettingsDescription => this[nameof (RedisSettingsDescription)];

    /// <summary>Caption for load balancing config element.</summary>
    [ResourceEntry("SendersCaption", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Senders")]
    public string SendersCaption => this[nameof (SendersCaption)];

    /// <summary>Description of load balancing config element</summary>
    [ResourceEntry("SendersDescription", Description = "Describes configuration element.", LastModified = "2012/01/05", Value = "Defines the list full type names of web service system senders of notification messages")]
    public string SendersDescription => this[nameof (SendersDescription)];

    /// <summary>Caption for load balancing config element.</summary>
    [ResourceEntry("HandlersCaption", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Handlers")]
    public string HandlersCaption => this[nameof (HandlersCaption)];

    /// <summary>Description of load balancing config element</summary>
    [ResourceEntry("HandlersDescription", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Defines the list of full type names of system handlers for processing system notification messages.")]
    public string HandlersDescription => this[nameof (HandlersDescription)];

    /// <summary>Caption for load balancing config element</summary>
    [ResourceEntry("IsWebFarmCaption", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Defines if website will be web farm.")]
    public string IsWebFarmCaption => this[nameof (IsWebFarmCaption)];

    /// <summary>Description of load balancing config element</summary>
    [ResourceEntry("IsWebFarmDescription", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Defines this website will be web farm - multiple servers.")]
    public string IsWebFarmDescription => this[nameof (IsWebFarmDescription)];

    /// <summary>Caption for load balancing config element</summary>
    [ResourceEntry("IsWebGardenCaption", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Defines if website will be web garden.")]
    public string IsWebGardenCaption => this[nameof (IsWebGardenCaption)];

    /// <summary>Description of load balancing config element</summary>
    [ResourceEntry("IsWebGardenDescription", Description = "Describes configuration element.", LastModified = "2011/01/24", Value = "Defines this website will be web garden - multiple processes.")]
    public string IsWebGardenDescription => this[nameof (IsWebGardenDescription)];

    /// <summary>Site URL Settings</summary>
    [ResourceEntry("SiteUrlSettingsCaption", Description = "Describes the title of this class.", LastModified = "2011/02/02", Value = "Site URL Settings")]
    public string SiteUrlSettingsCaption => this[nameof (SiteUrlSettingsCaption)];

    /// <summary>Defines the URL of the site.</summary>
    [ResourceEntry("SiteUrlSettingsDescription", Description = "The description of this class.", LastModified = "2011/02/02", Value = "Defines the URL of the site.")]
    public string SiteUrlSettingsDescription => this[nameof (SiteUrlSettingsDescription)];

    /// <summary>Enable non-default Site URL Settings</summary>
    [ResourceEntry("EnableNonDefaultSiteUrlSettingsTitle", Description = "The caption of the EnableNonDefaultSiteUrlSettingsTitle property in the SiteUrlSettings configuration.", LastModified = "2011/02/02", Value = "Enable non-default Site URL Settings")]
    public string EnableNonDefaultSiteUrlSettingsTitle => this[nameof (EnableNonDefaultSiteUrlSettingsTitle)];

    /// <summary>
    /// Defines whether to enable non-default the site URL settings: domain, http and https ports.
    /// </summary>
    [ResourceEntry("EnableNonDefaultSiteUrlSettingsDescription", Description = "The description of the EnableNonDefaultSiteUrlSettingsTitle property in the SiteUrlSettings configuration.", LastModified = "2011/02/02", Value = "Defines whether to enable non-default site URL settings: domain, http and https ports. If not enabled, host is determined by the first request, for http and https default ports are used.")]
    public string EnableNonDefaultSiteUrlSettingsDescription => this[nameof (EnableNonDefaultSiteUrlSettingsDescription)];

    /// <summary>Remove ssl when the page does not require it.</summary>
    [ResourceEntry("RemoveNonRequiredSslSettingsTitle", Description = "Remove ssl when the page does not require it.", LastModified = "2013/09/03", Value = "Remove ssl when the page does not require it")]
    public string RemoveNonRequiredSslSettingsTitle => this[nameof (RemoveNonRequiredSslSettingsTitle)];

    /// <summary>
    /// When enabled the pages that does not require ssl will redirect to http explicitly.
    /// </summary>
    [ResourceEntry("RemoveNonRequiredSslSettingsDescription", Description = "The description of the RemoveNonRequiredSslSettingsDescription property in the SiteUrlSettings configuration.", LastModified = "2013/09/03", Value = "When enabled the pages that does not require ssl will redirect to http explicitly.")]
    public string RemoveNonRequiredSslSettingsDescription => this[nameof (RemoveNonRequiredSslSettingsDescription)];

    /// <summary>
    /// The host component of the site, if any when non-default settings are enabled. If not specified, it is determined by the first request.
    /// </summary>
    [ResourceEntry("HostTitle", Description = "The caption of the Host property in the SiteUrlSettings configuration.", LastModified = "2012/02/01", Value = "The Host")]
    public string HostTitle => this[nameof (HostTitle)];

    /// <summary>
    /// Defines the host component of the site, if any when non-default settings are enabled. If not specified, it is determined by the first request.
    /// </summary>
    [ResourceEntry("HostDescription", Description = "The description of the Host property in the SiteUrlSettings configuration.", LastModified = "2011/02/02", Value = "Defines the host component of the site, if any when non-default settings are enabled. If not specified, it is determined by the first request.")]
    public string HostDescription => this[nameof (HostDescription)];

    /// <summary>Non-default HTTP port</summary>
    [ResourceEntry("NonDefaultHttpPortTitle", Description = "The caption of the NonDefaultHttpPort property in the SiteUrlSettings configuration.", LastModified = "2012/02/01", Value = "Non-default HTTP port")]
    public string NonDefaultHttpPortTitle => this[nameof (NonDefaultHttpPortTitle)];

    /// <summary>
    /// Defines the non-default port of Site URL over HTTP protocol
    /// </summary>
    [ResourceEntry("NonDefaultHttpPortDescription", Description = "The description of the NonDefaultHttpPort property in the SiteUrlSettings configuration.", LastModified = "2012/02/01", Value = "Defines the port of site URL when constructing absolute URLs over HTTP protocol and non-default port is used.")]
    public string NonDefaultHttpPortDescription => this[nameof (NonDefaultHttpPortDescription)];

    /// <summary>Non-default HTTPS port</summary>
    [ResourceEntry("NonDefaultHttpsPortTitle", Description = "The caption of the NonDefaultHttpsPort property in the SiteUrlSettings configuration.", LastModified = "2012/02/01", Value = "Non-default HTTPS port")]
    public string NonDefaultHttpsPortTitle => this[nameof (NonDefaultHttpsPortTitle)];

    /// <summary>
    /// Defines the non-default port of site URL over HTTPS protocol.
    /// </summary>
    [ResourceEntry("NonDefaultHttpsPortDescription", Description = "The description of the NonDefaultHttpsPort property in the SiteUrlSettings configuration.", LastModified = "2012/02/01", Value = "Defines the port of site URL when constructing absolute URLs over HTTPS protocol and non-default port is used.")]
    public string NonDefaultHttpsPortDescription => this[nameof (NonDefaultHttpsPortDescription)];

    /// <summary>Gets the site URL configuration descriptions.</summary>
    [ResourceEntry("SiteUrlSettingsConfigDescriptions", Description = "The site URL configuration", LastModified = "2011/02/02", Value = "The site URL configuration")]
    public string SiteUrlSettingsConfigDescriptions => this[nameof (SiteUrlSettingsConfigDescriptions)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("ProfileTypesSettings", Description = "Describes configuration element.", LastModified = "2011/02/10", Value = "Profile types settings")]
    public string ProfileTypesSettings => this[nameof (ProfileTypesSettings)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("UserProfilesConfig", Description = "Describes the configuration element.", LastModified = "2011/04/05", Value = "User profiles settings")]
    public string UserProfilesConfig => this[nameof (UserProfilesConfig)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("ProfileTypeFullNameCaption", Description = "Caption of the ProfileTypeFullName configuration element.", LastModified = "2011/04/05", Value = "ProfileTypeFullName")]
    public string ProfileTypeFullNameCaption => this[nameof (ProfileTypeFullNameCaption)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("ProfileTypeFullNameDescription", Description = "Description of the ProfileTypeFullName configuration element.", LastModified = "2011/04/05", Value = "The full type name of the profile type")]
    public string ProfileTypeFullNameDescription => this[nameof (ProfileTypeFullNameDescription)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("ProviderCaption", Description = "Caption of the Provider configuration element.", LastModified = "2011/04/05", Value = "ProviderCaption")]
    public string ProviderCaption => this[nameof (ProviderCaption)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("ProviderDescription", Description = "Description of the Provider configuration element.", LastModified = "2011/04/05", Value = "The data provider")]
    public string ProviderDescription => this[nameof (ProviderDescription)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("UserIdCaption", Description = "Caption of the UserIdCaption configuration element.", LastModified = "2011/04/05", Value = "UserIdCaption")]
    public string UserIdCaption => this[nameof (UserIdCaption)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("UserIdDescription", Description = "Description of the UserIdDescription configuration element.", LastModified = "2011/04/05", Value = "The id of the user to show")]
    public string UserIdDescription => this[nameof (UserIdDescription)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("UsersDisplayModeCaption", Description = "Caption of the UsersDisplayMode configuration element.", LastModified = "2011/04/05", Value = "UsersDisplayModeCaption")]
    public string UsersDisplayModeCaption => this[nameof (UsersDisplayModeCaption)];

    /// <summary>
    /// Message: Defines user profiles configuration settings.
    /// </summary>
    [ResourceEntry("UsersDisplayModeDescription", Description = "Description of the UsersDisplayMode configuration element.", LastModified = "2011/04/05", Value = "Which set of users to show")]
    public string UsersDisplayModeDescription => this[nameof (UsersDisplayModeDescription)];

    /// <summary>Message: RolesIdsCaption.</summary>
    [ResourceEntry("RolesIdsCaption", Description = "Caption of the RolesConfig configuration element.", LastModified = "2011/04/05", Value = "RolesIdsCaption")]
    public string RolesIdsCaption => this[nameof (RolesIdsCaption)];

    /// <summary>Message: Array of the roles as ItemInfos to show.</summary>
    [ResourceEntry("RolesIdsDescription", Description = "Description of the RolesConfig configuration element.", LastModified = "2011/04/05", Value = "Array of the roles as ItemInfos to show")]
    public string RolesIdsDescription => this[nameof (RolesIdsDescription)];

    /// <summary>Message: ProfileProviderCaption.</summary>
    [ResourceEntry("ProfileProviderCaption", Description = "Caption of the ProfileProvider configuration element.", LastModified = "2011/04/05", Value = "ProfileProviderCaption")]
    public string ProfileProviderCaption => this[nameof (ProfileProviderCaption)];

    /// <summary>
    /// Message: The name of the default data provider that is used to manage security.
    /// </summary>
    [ResourceEntry("ProfileProviderDescription", Description = "Description of the ProfileProvider configuration element.", LastModified = "2011/04/05", Value = "The name of the default data provider that is used to manage security")]
    public string ProfileProviderDescription => this[nameof (ProfileProviderDescription)];

    /// <summary>Message: UseAllMembershipProvidersCaption.</summary>
    [ResourceEntry("UseAllMembershipProvidersCaption", Description = "Caption of the UseAllMembershipProviders configuration element.", LastModified = "2011/04/05", Value = "UseAllMembershipProvidersCaption")]
    public string UseAllMembershipProvidersCaption => this[nameof (UseAllMembershipProvidersCaption)];

    /// <summary>
    /// Message: A value indicating whether the profile type can use all membership providers.
    /// </summary>
    [ResourceEntry("UseAllMembershipProvidersDescription", Description = "Description of the UseAllMembershipProviders configuration element.", LastModified = "2011/04/05", Value = "A value indicating whether the profile type can use all membership providers.")]
    public string UseAllMembershipProvidersDescription => this[nameof (UseAllMembershipProvidersDescription)];

    /// <summary>Message: SubmittingUserProfileSuccessActionCaption.</summary>
    [ResourceEntry("SubmittingUserProfileSuccessActionCaption", Description = "Caption of the SubmittingUserProfileSuccessAction configuration element.", LastModified = "2011/04/05", Value = "SubmittingUserProfileSuccessActionCaption")]
    public string SubmittingUserProfileSuccessActionCaption => this[nameof (SubmittingUserProfileSuccessActionCaption)];

    /// <summary>
    /// Message: The action that will be executed on successful user profile submission.
    /// </summary>
    [ResourceEntry("SubmittingUserProfileSuccessActionDescription", Description = "Description of the SubmittingUserProfileSuccessAction configuration element.", LastModified = "2011/04/05", Value = "The action that will be executed on successful user profile submission.")]
    public string SubmittingUserProfileSuccessActionDescription => this[nameof (SubmittingUserProfileSuccessActionDescription)];

    /// <summary>Message: SubmitSuccessMessageCaption.</summary>
    [ResourceEntry("SubmitSuccessMessageCaption", Description = "Caption of the SubmitSuccessMessage configuration element.", LastModified = "2011/04/05", Value = "SubmitSuccessMessageCaption")]
    public string SubmitSuccessMessageCaption => this[nameof (SubmitSuccessMessageCaption)];

    /// <summary>
    /// Message: The message to show on successful user profile submission.
    /// </summary>
    [ResourceEntry("SubmitSuccessMessageDescription", Description = "Description of the SubmitSuccessMessage configuration element.", LastModified = "2011/04/05", Value = "The action that will be executed on successful user profile submission.")]
    public string SubmitSuccessMessageDescription => this[nameof (SubmitSuccessMessageDescription)];

    /// <summary>Message: NoUserTemplateNameCaption.</summary>
    [ResourceEntry("NoUserTemplateNameCaption", Description = "Caption of the NoUserTemplatePath configuration element.", LastModified = "2011/04/05", Value = "NoUserTemplateNameCaption")]
    public string NoUserTemplateNameCaption => this[nameof (NoUserTemplateNameCaption)];

    /// <summary>
    /// Message: The name of the template that will be instantiated when the view is displayed for a not logged in user.
    /// </summary>
    [ResourceEntry("NoUserTemplateNameDescription", Description = "Description of the NoUserTemplatePath configuration element.", LastModified = "2012/01/05", Value = "The name of the template that will be instantiated when the view is displayed for a not logged in user.")]
    public string NoUserTemplateNameDescription => this[nameof (NoUserTemplateNameDescription)];

    /// <summary>Message: EditProfilePageIdCaption.</summary>
    [ResourceEntry("EditProfilePageIdCaption", Description = "Caption of the EditProfilePageId configuration element.", LastModified = "2011/04/05", Value = "EditProfilePageIdCaption")]
    public string EditProfilePageIdCaption => this[nameof (EditProfilePageIdCaption)];

    /// <summary>
    /// Message: The page id for the page responsible for showing the edit profile widget.
    /// </summary>
    [ResourceEntry("EditProfilePageIdDescription", Description = "Description of the EditProfilePageId configuration element.", LastModified = "2011/04/05", Value = "The page id for the page responsible for showing the edit profile widget.")]
    public string EditProfilePageIdDescription => this[nameof (EditProfilePageIdDescription)];

    /// <summary>Message: ChangePasswordPageIdCaption.</summary>
    [ResourceEntry("ChangePasswordPageIdCaption", Description = "Caption of the ChangePasswordPageId configuration element.", LastModified = "2011/04/05", Value = "ChangePasswordPageIdCaption")]
    public string ChangePasswordPageIdCaption => this[nameof (ChangePasswordPageIdCaption)];

    /// <summary>
    /// Message: The id of the page responsible for showing the change password widget.
    /// </summary>
    [ResourceEntry("ChangePasswordPageIdDescription", Description = "Description of the ChangePasswordPageId configuration element.", LastModified = "2011/04/05", Value = "The id of the page responsible for showing the change password widget.")]
    public string ChangePasswordPageIdDescription => this[nameof (ChangePasswordPageIdDescription)];

    /// <summary>Message: RedirectOnSubmitPageIdCaption.</summary>
    [ResourceEntry("RedirectOnSubmitPageIdCaption", Description = "Caption of the ChangePasswordPageId configuration element.", LastModified = "2011/04/05", Value = "RedirectOnSubmitPageIdCaption")]
    public string RedirectOnSubmitPageIdCaption => this[nameof (RedirectOnSubmitPageIdCaption)];

    /// <summary>
    /// Message: The id of the page responsible for showing the change password widget.
    /// </summary>
    [ResourceEntry("RedirectOnSubmitPageIdDescription", Description = "Description of the ChangePasswordPageId configuration element.", LastModified = "2011/04/05", Value = "The id of the page responsible for showing the change password widget.")]
    public string RedirectOnSubmitPageIdDescription => this[nameof (RedirectOnSubmitPageIdDescription)];

    /// <summary>Message: NotLoggedTemplateKeyCaption.</summary>
    [ResourceEntry("NotLoggedTemplateKeyCaption", Description = "Caption of the NotLoggedTemplateKey configuration element.", LastModified = "2011/04/05", Value = "RedirectOnSubmitPageIdCaption")]
    public string NotLoggedTemplateKeyCaption => this[nameof (NotLoggedTemplateKeyCaption)];

    /// <summary>Message: The not logged template key.</summary>
    [ResourceEntry("NotLoggedTemplateKeyDescription", Description = "Description of the NotLoggedTemplateKey configuration element.", LastModified = "2011/04/05", Value = "The not logged template key.")]
    public string NotLoggedTemplateKeyDescription => this[nameof (NotLoggedTemplateKeyDescription)];

    /// <summary>Message: ShowAdditionalModesLinksCaption.</summary>
    [ResourceEntry("ShowAdditionalModesLinksCaption", Description = "Caption of the ShowAdditionalModesLinks configuration element.", LastModified = "2011/04/05", Value = "ShowAdditionalModesLinksCaption")]
    public string ShowAdditionalModesLinksCaption => this[nameof (ShowAdditionalModesLinksCaption)];

    /// <summary>Message: The show additional modes links.</summary>
    [ResourceEntry("ShowAdditionalModesLinksDescription", Description = "Description of the ShowAdditionalModesLinks configuration element.", LastModified = "2011/04/05", Value = "The show additional modes links.")]
    public string ShowAdditionalModesLinksDescription => this[nameof (ShowAdditionalModesLinksDescription)];

    /// <summary>Message: OpenViewsInExternalPagesCaption.</summary>
    [ResourceEntry("OpenViewsInExternalPagesCaption", Description = "Caption of the OpenViewsInExternalPages configuration element.", LastModified = "2011/04/05", Value = "OpenViewsInExternalPagesCaption")]
    public string OpenViewsInExternalPagesCaption => this[nameof (OpenViewsInExternalPagesCaption)];

    /// <summary>Message: The open views in external pages.</summary>
    [ResourceEntry("OpenViewsInExternalPagesDescription", Description = "Description of the OpenViewsInExternalPages configuration element.", LastModified = "2011/04/05", Value = "The open views in external pages.")]
    public string OpenViewsInExternalPagesDescription => this[nameof (OpenViewsInExternalPagesDescription)];

    /// <summary>
    /// Title of the SitemapNodeFilterElement configuration element
    /// </summary>
    [ResourceEntry("SitemapNodeFilterElementTitle", Description = "Title of the SitemapNodeFilterElement configuration element", LastModified = "2011/05/24", Value = "Sitemap node filter")]
    public string SitemapNodeFilterElementTitle => this[nameof (SitemapNodeFilterElementTitle)];

    /// <summary>
    /// Description of the SitemapNodeFilterElement configuration element
    /// </summary>
    [ResourceEntry("SitemapNodeFilterElementDescription", Description = "Description of the SitemapNodeFilterElement configuration element", LastModified = "2011/05/24", Value = "Holds information about the availability of a sitemap node filter and optional parameters")]
    public string SitemapNodeFilterElementDescription => this[nameof (SitemapNodeFilterElementDescription)];

    /// <summary>
    /// Title of the SitemapNodeFilterElement.FilterName configuration property
    /// </summary>
    [ResourceEntry("SitempNodeFilterNameTitle", Description = "Title of the SitemapNodeFilterElement.FilterName configuration property", LastModified = "2011/05/24", Value = "Filter name")]
    public string SitempNodeFilterNameTitle => this[nameof (SitempNodeFilterNameTitle)];

    /// <summary>
    /// Description of the SitemapNodeFilterElement.FilterName configuration property
    /// </summary>
    [ResourceEntry("SitempNodeFilterNameDescription", Description = "Description of the SitemapNodeFilterElement.FilterName configuration property", LastModified = "2012/01/05", Value = "Name of the sitemap node filter, as registered in ObjectFactory. Must be unique within the parent collection")]
    public string SitempNodeFilterNameDescription => this[nameof (SitempNodeFilterNameDescription)];

    /// <summary>
    /// Title of the SitemapNodeFilterElement.IsEnabled configuration property
    /// </summary>
    [ResourceEntry("SitempNodeFilterEnabledTitle", Description = "Title of the SitemapNodeFilterElement.IsEnabled configuration property", LastModified = "2011/05/24", Value = "Enabled")]
    public string SitempNodeFilterEnabledTitle => this[nameof (SitempNodeFilterEnabledTitle)];

    /// <summary>
    /// Description of the SitemapNodeFilterElement.IsEnabled configuration property
    /// </summary>
    [ResourceEntry("SitempNodeFilterEnabledDescription", Description = "Description of the SitemapNodeFilterElement.IsEnabled configuration property", LastModified = "2011/05/24", Value = "True if the filter is active, false to turn it off")]
    public string SitempNodeFilterEnabledDescription => this[nameof (SitempNodeFilterEnabledDescription)];

    /// <summary>
    /// Title of the SitemapNodeFilterElement.Parameters configuration property
    /// </summary>
    [ResourceEntry("SitemapNodeFilterParametersTitle", Description = "Title of the SitemapNodeFilterElement.Parameters configuration property", LastModified = "2011/05/24", Value = "Parameters")]
    public string SitemapNodeFilterParametersTitle => this[nameof (SitemapNodeFilterParametersTitle)];

    /// <summary>
    /// Description of the SitemapNodeFilterElement.Parameters configuration property
    /// </summary>
    [ResourceEntry("SitemapNodeFilterParametersDescription", Description = "Description of the SitemapNodeFilterElement.Parameters configuration property", LastModified = "2011/05/24", Value = "Collection of any custom parameters a filter needs to operate properly")]
    public string SitemapNodeFilterParametersDescription => this[nameof (SitemapNodeFilterParametersDescription)];

    /// <summary>
    /// Title of the SitemapNodeFilterParameterElement configuration element
    /// </summary>
    [ResourceEntry("SitemapNodeFilterParameterTitle", Description = "Title of the SitemapNodeFilterParameterElement configuration element", LastModified = "2011/05/24", Value = "Sitemap node filter parameter")]
    public string SitemapNodeFilterParameterTitle => this[nameof (SitemapNodeFilterParameterTitle)];

    /// <summary>
    /// Description of the SitemapNodeFilterParameterElement configuration element
    /// </summary>
    [ResourceEntry("SitemapNodeFilterParameterDescription", Description = "Description of the SitemapNodeFilterParameterElement configuration element", LastModified = "2011/05/24", Value = "Holds information about a custom parameter of a sitemap node filter")]
    public string SitemapNodeFilterParameterDescription => this[nameof (SitemapNodeFilterParameterDescription)];

    /// <summary>
    /// Title of the SitemapNodeFilterParameterElement.ParameterName configuration property
    /// </summary>
    [ResourceEntry("SitemapNodeFilterParameterNameTitle", Description = "Title of the SitemapNodeFilterParameterElement.ParameterName configuration property", LastModified = "2011/05/24", Value = "Name")]
    public string SitemapNodeFilterParameterNameTitle => this[nameof (SitemapNodeFilterParameterNameTitle)];

    /// <summary>
    /// Description of the SitemapNodeFilterParameterElement.ParameterName configuration property
    /// </summary>
    [ResourceEntry("SitemapNodeFilterParameterNameDescription", Description = "Description of the SitemapNodeFilterParameterElement.ParameterName configuration property", LastModified = "2011/05/24", Value = "Name of the sitemap node filter parameter. Must be unique within the parent collection")]
    public string SitemapNodeFilterParameterNameDescription => this[nameof (SitemapNodeFilterParameterNameDescription)];

    /// <summary>
    /// Title of the SitemapNodeFilterParameterElement.ParamterValue configuration property
    /// </summary>
    [ResourceEntry("SitempNodeFilterParameterValueTitle", Description = "Title of the SitemapNodeFilterParameterElement.ParamterValue configuration property", LastModified = "2011/05/24", Value = "Value")]
    public string SitempNodeFilterParameterValueTitle => this[nameof (SitempNodeFilterParameterValueTitle)];

    /// <summary>
    /// Description of the SitemapNodeFilterParameterElement.ParamterValue configuration property
    /// </summary>
    [ResourceEntry("SitempNodeFilterParameterValueDescription", Description = "Description of the SitemapNodeFilterParameterElement.ParamterValue configuration property", LastModified = "2011/05/24", Value = "Value of the sitemap node filter parameter")]
    public string SitempNodeFilterParameterValueDescription => this[nameof (SitempNodeFilterParameterValueDescription)];

    /// <summary>
    /// Gets or sets a value indicating whether to transform RSS XML to HTML when render rss feed at a browser.
    /// </summary>
    /// <value>Gets or sets a value indicating whether to transform RSS XML to HTML when render rss feed at a browser.</value>
    [ResourceEntry("TransformRssXmlToHtml", Description = "A value indicating whether to transform RSS XML to HTML when render rss feed at a browser.", LastModified = "2011/08/31", Value = "Transform RSS XML to HTML.")]
    public string TransformRssXmlToHtml => this[nameof (TransformRssXmlToHtml)];

    /// <summary>
    /// Gets or sets a value indicating whether to enable DTD processing to parse the settings into XmlReader.
    /// </summary>
    /// <value>Gets or sets a value indicating whether to enable DTD processing to parse the settings into XmlReader.</value>
    [ResourceEntry("EnableDtdProcessing", Description = "A value indicating whether to enable DTD processing to parse the settings into XmlReader.", LastModified = "2013/06/17", Value = "Enable DTD processing")]
    public string EnableDtdProcessing => this[nameof (EnableDtdProcessing)];

    /// <summary>
    /// Message: The configuration element for the ItemInfoDefinition.
    /// </summary>
    [ResourceEntry("EnableDtdProcessingDescription", Description = "Description of the EnableDtdProcessing configuration element.", LastModified = "2013/06/17", Value = "A value indicating whether to enable DTD processing to parse the settings into XmlReader.")]
    public string EnableDtdProcessingDescription => this[nameof (EnableDtdProcessingDescription)];

    /// <summary>
    /// Message: The configuration element for the ItemInfoDefinition.
    /// </summary>
    [ResourceEntry("TransformRssXmlToHtmlDescription", Description = "Description of the TransformRssXmlToHtml configuration element.", LastModified = "2011/08/31", Value = "A value indicating whether to transform RSS XML to HTML when render rss feed at a browser.")]
    public string TransformRssXmlToHtmlDescription => this[nameof (TransformRssXmlToHtmlDescription)];

    /// <summary>
    /// Message: Defines ItemInfoDefinition configuration settings.
    /// </summary>
    [ResourceEntry("ItemInfoConfigCaption", Description = "Caption of the ItemInfoElement configuration element.", LastModified = "2011/04/11", Value = "ItemInfoConfigCaption")]
    public string ItemInfoConfigCaption => this[nameof (ItemInfoConfigCaption)];

    /// <summary>
    /// Message: The configuration element for the ItemInfoDefinition.
    /// </summary>
    [ResourceEntry("ItemInfoConfigDescription", Description = "Description of the ItemInfoElement configuration element.", LastModified = "2011/04/11", Value = "The full type name of the profile type")]
    public string ItemInfoConfigDescription => this[nameof (ItemInfoConfigDescription)];

    /// <summary>Message: ItemInfoConfigProviderNameTitle.</summary>
    [ResourceEntry("ItemInfoConfigProviderNameTitle", Description = "Caption of the ProviderName configuration element.", LastModified = "2011/04/11", Value = "ItemInfoConfigProviderNameTitle")]
    public string ItemInfoConfigProviderNameTitle => this[nameof (ItemInfoConfigProviderNameTitle)];

    /// <summary>
    /// Message: The name of the provider to retrieve the content element.
    /// </summary>
    [ResourceEntry("ItemInfoConfigProviderNameDescription", Description = "Description of the ProviderName configuration element.", LastModified = "2011/04/11", Value = "The name of the provider to retrieve the content element.")]
    public string ItemInfoConfigProviderNameDescription => this[nameof (ItemInfoConfigProviderNameDescription)];

    /// <summary>Message: ItemInfoConfigItemIdTitle.</summary>
    [ResourceEntry("ItemInfoConfigItemIdTitle", Description = "Caption of the ItemId configuration element.", LastModified = "2011/04/11", Value = "ItemInfoConfigItemIdTitle")]
    public string ItemInfoConfigItemIdTitle => this[nameof (ItemInfoConfigItemIdTitle)];

    /// <summary>Message: The ID of the content element.</summary>
    [ResourceEntry("ItemInfoConfigItemIdDescription", Description = "Description of the ItemId configuration element.", LastModified = "2011/04/11", Value = "The ID of the content element.")]
    public string ItemInfoConfigItemIdDescription => this[nameof (ItemInfoConfigItemIdDescription)];

    /// <summary>Message: ItemInfoConfigTitleTitle.</summary>
    [ResourceEntry("ItemInfoConfigTitleTitle", Description = "Caption of the Title configuration element.", LastModified = "2011/04/11", Value = "ItemInfoConfigTitleTitle")]
    public string ItemInfoConfigTitleTitle => this[nameof (ItemInfoConfigTitleTitle)];

    /// <summary>Message: The title of the content element.</summary>
    [ResourceEntry("ItemInfoConfigTitleDescription", Description = "Description of the Title configuration element.", LastModified = "2011/04/11", Value = "The title of the content element.")]
    public string ItemInfoConfigTitleDescription => this[nameof (ItemInfoConfigTitleDescription)];

    /// <summary>
    /// Message: Defines BindingTimeouts configuration settings.
    /// </summary>
    [ResourceEntry("BindingTimeoutsConfigCaption", Description = "Caption of the BindingTimeouts configuration element.", LastModified = "2011/06/27", Value = "BindingTimeoutsConfigCaption")]
    public string BindingTimeoutsConfigCaption => this[nameof (BindingTimeoutsConfigCaption)];

    /// <summary>
    /// Message: The configuration element for the BindingTimeouts.
    /// </summary>
    [ResourceEntry("BindingTimeoutsConfigDescription", Description = "Description of the BindingTimeouts configuration element.", LastModified = "2011/06/27", Value = "The binding timeouts configuration")]
    public string BindingTimeoutsConfigDescription => this[nameof (BindingTimeoutsConfigDescription)];

    /// <summary>
    /// Message: Defines CloseTimeout binding configuration settings.
    /// </summary>
    [ResourceEntry("CloseTimeoutCaption", Description = "Caption of the CloseTimeout binding configuration element.", LastModified = "2011/06/27", Value = "CloseTimeoutCaption")]
    public string CloseTimeoutCaption => this[nameof (CloseTimeoutCaption)];

    /// <summary>
    /// Message: The configuration element for the CloseTimeout.
    /// </summary>
    [ResourceEntry("CloseTimeoutDescription", Description = "Description of the CloseTimeout configuration element.", LastModified = "2011/06/27", Value = "Defines the interval of time in milliseconds for a connection to close before the transport raises an exception.If the value of this property is not set the value of AllPropertiesTimeoutDefaultValue will be applied.")]
    public string CloseTimeoutDescription => this[nameof (CloseTimeoutDescription)];

    /// <summary>
    /// Message: Defines OpenTimeout binding configuration settings.
    /// </summary>
    [ResourceEntry("OpenTimeoutCaption", Description = "Caption of the OpenTimeout binding configuration element.", LastModified = "2011/06/27", Value = "OpenTimeoutCaption")]
    public string OpenTimeoutCaption => this[nameof (OpenTimeoutCaption)];

    /// <summary>
    /// Message: The configuration element for the OpenTimeout.
    /// </summary>
    [ResourceEntry("OpenTimeoutDescription", Description = "Description of the OpenTimeout configuration element.", LastModified = "2011/06/27", Value = "Defines the interval of time in milliseconds provided for a connection to open before the transport raises an exception.If the value of this property is not set the value of AllPropertiesTimeoutDefaultValue will be applied.")]
    public string OpenTimeoutDescription => this[nameof (OpenTimeoutDescription)];

    /// <summary>
    /// Message: Defines ReceiveTimeout binding configuration settings.
    /// </summary>
    [ResourceEntry("ReceiveTimeoutCaption", Description = "Caption of the ReceiveTimeout binding configuration element.", LastModified = "2011/06/27", Value = "ReceiveTimeoutCaption")]
    public string ReceiveTimeoutCaption => this[nameof (ReceiveTimeoutCaption)];

    /// <summary>
    /// Message: The configuration element for the ReceiveTimeout.
    /// </summary>
    [ResourceEntry("ReceiveTimeoutDescription", Description = "Description of the ReceiveTimeout configuration element.", LastModified = "2012/01/05", Value = "Defines the interval of time in milliseconds that a connection can remain inactive, during which no application messages are received, before it is dropped.If the value of this property is not set the value of AllPropertiesTimeoutDefaultValue will be applied.")]
    public string ReceiveTimeoutDescription => this[nameof (ReceiveTimeoutDescription)];

    /// <summary>
    /// Message: Defines SendTimeout binding configuration settings.
    /// </summary>
    [ResourceEntry("SendTimeoutCaption", Description = "Caption of the SendTimeout binding configuration element.", LastModified = "2011/06/27", Value = "SendTimeoutCaption")]
    public string SendTimeoutCaption => this[nameof (SendTimeoutCaption)];

    /// <summary>
    /// Message: The configuration element for the SendTimeout.
    /// </summary>
    [ResourceEntry("SendTimeoutDescription", Description = "Description of the SendTimeout configuration element.", LastModified = "2011/06/27", Value = "Defines the interval of time in milliseconds provided for a write operation to complete before the transport raises an exception.If the value of this property is not set the value of AllPropertiesTimeoutDefaultValue will be applied.")]
    public string SendTimeoutDescription => this[nameof (SendTimeoutDescription)];

    /// <summary>
    /// Message: Defines Timeout default value for Open, Close, Send and Receive binding timeout configuration settings.
    /// </summary>
    [ResourceEntry("AllPropertiesTimeoutDefaultValueCaption", Description = "Caption of the AllPropertiesTimeoutDefaultValue binding configuration element.", LastModified = "2011/06/27", Value = "Defines Timeout default value for Open, Close, Send and Receive binding timeout configuration settings")]
    public string AllPropertiesTimeoutDefaultValueCaption => this[nameof (AllPropertiesTimeoutDefaultValueCaption)];

    /// <summary>
    /// Message: Defines Timeout default value for Open, Close, Send and Receive binding timeout configuration settings.
    /// </summary>
    [ResourceEntry("AllPropertiesTimeoutDefaultValueDescription", Description = "Description of the AllPropertiesTimeoutDefaultValue configuration element.", LastModified = "2011/06/27", Value = "Defines Timeout default value for Open, Close, Send and Receive binding timeout configuration settings.")]
    public string AllPropertiesTimeoutDefaultValueDescription => this[nameof (AllPropertiesTimeoutDefaultValueDescription)];

    /// <summary>phrase: Use the external endpoint on Windows Azure</summary>
    [ResourceEntry("WorkflowUseExternalEndpointOnAzureTitle", Description = "phrase: Use the external endpoint on Windows Azure", LastModified = "2011/11/14", Value = "Use the external endpoint on Windows Azure")]
    public string WorkflowUseExternalEndpointOnAzureTitle => this[nameof (WorkflowUseExternalEndpointOnAzureTitle)];

    /// <summary>
    /// phrase: When set to true all requests to the workflow service are sent to the public/input endpoint, thus going through the load balancer and possibly landing to a different instance. This is required to workaround the inability to set the Host header in the WCF request, when multiple sites per instance are sharing the internal endpoint.
    /// </summary>
    [ResourceEntry("WorkflowUseExternalEndpointOnAzureDescription", Description = "phrase: When set to true all requests to the workflow service are sent to the public/input endpoint, thus going through the load balancer and possibly landing to a different instance. This is required to work around the inability to set the Host header in the WCF request, when multiple sites per instance are sharing the internal endpoint.", LastModified = "2012/01/05", Value = "When set to true all requests to the workflow service are sent to the public/input endpoint, thus going through the load balancer and possibly landing to a different instance. This is required to workaround the inability to set the Host header in the WCF request, when multiple sites per instance are sharing the internal endpoint.")]
    public string WorkflowUseExternalEndpointOnAzureDescription => this[nameof (WorkflowUseExternalEndpointOnAzureDescription)];

    /// <summary>
    /// phrase: Windows Azure external endpoint binding timeouts
    /// </summary>
    [ResourceEntry("WindowsAzureExternalEndpointBindingTimeoutsTitle", Description = "phrase: Windows Azure external endpoint binding timeouts", LastModified = "2011/11/14", Value = "Windows Azure external endpoint binding timeouts")]
    public string WindowsAzureExternalEndpointBindingTimeoutsTitle => this[nameof (WindowsAzureExternalEndpointBindingTimeoutsTitle)];

    /// <summary>
    /// phrase: Workflow binding timeouts for the case when the requests to the workflow service are sent to the public/input endpoint.
    /// </summary>
    [ResourceEntry("WindowsAzureExternalEndpointBindingTimeoutsDescription", Description = "phrase: Workflow binding timeouts for the case when the requests to the workflow service are sent to the public/input endpoint.", LastModified = "2011/11/14", Value = "Workflow binding timeouts for the case when the requests to the workflow service are sent to the public/input endpoint.")]
    public string WindowsAzureExternalEndpointBindingTimeoutsDescription => this[nameof (WindowsAzureExternalEndpointBindingTimeoutsDescription)];

    /// <summary>
    /// Title of the LabelDefinitionElement configuration element
    /// </summary>
    [ResourceEntry("LabelDefinitionElementTitle", Description = "Title of the LabelDefinitionElement configuration element", LastModified = "2011/05/23", Value = "Labels")]
    public string LabelDefinitionElementTitle => this[nameof (LabelDefinitionElementTitle)];

    /// <summary>
    /// Description of the LabelDefinitionElement configuration element
    /// </summary>
    [ResourceEntry("LabelDefinitionElementDescription", Description = "Description of the LabelDefinitionElement configuration element", LastModified = "2011/05/23", Value = "Stores information about a localizable string - resource, message pair (e.g. 'Labels', 'MoreItems')")]
    public string LabelDefinitionElementDescription => this[nameof (LabelDefinitionElementDescription)];

    /// <summary>Title of the CompoundKey configuration property</summary>
    [ResourceEntry("CompoundKeyTitle", Description = "Title of the CompoundKey configuration property", LastModified = "2011/05/23", Value = "Compound key")]
    public string CompoundKeyTitle => this[nameof (CompoundKeyTitle)];

    /// <summary>Description of the CompoundKey configuration property</summary>
    [ResourceEntry("CompoundKeyDescription", Description = "Description of the CompoundKey configuration property", LastModified = "2011/05/23", Value = "Must be unique within the parent collection. ClassId + MessageKey is a reasonable default")]
    public string CompoundKeyDescription => this[nameof (CompoundKeyDescription)];

    /// <summary>Title of the ClassId configuration property</summary>
    [ResourceEntry("ClassIdTitle", Description = "Title of the ClassId configuration property", LastModified = "2011/05/23", Value = "Resource class ID")]
    public string ClassIdTitle => this[nameof (ClassIdTitle)];

    /// <summary>workflow configuration label</summary>
    [ResourceEntry("RunWorkflowAsServiceTitle", Description = "workflow configuration label", LastModified = "2013/11/21", Value = "Run workflow as WCF Service")]
    public string RunWorkflowAsServiceTitle => this[nameof (RunWorkflowAsServiceTitle)];

    /// <summary>workflow configuration label</summary>
    [ResourceEntry("RunWorkflowAsServiceDescription", Description = "workflow configuration label", LastModified = "2013/11/21", Value = "If checked workflows will be called as external WCF XAMLX services. By default workflow is run as InProcess Workflow Application.")]
    public string RunWorkflowAsServiceDescription => this[nameof (RunWorkflowAsServiceDescription)];

    /// <summary>Description of the ClassId configuration property</summary>
    [ResourceEntry("ClassIdDescription", Description = "Description of the ClassId configuration property", LastModified = "2011/05/23", Value = "Resource class ID. E.g. 'Taxonomies', 'Labels', etc.")]
    public string ClassIdDescription => this[nameof (ClassIdDescription)];

    /// <summary>Title of the MessageKey configuration property</summary>
    [ResourceEntry("MessageKeyTitle", Description = "Title of the MessageKey configuration property", LastModified = "2011/05/23", Value = "Message key")]
    public string MessageKeyTitle => this[nameof (MessageKeyTitle)];

    /// <summary>Description of the MessageKey configuration property</summary>
    [ResourceEntry("MessageKeyDescription", Description = "Description of the MessageKey configuration property", LastModified = "2012/01/05", Value = "ID of the label within the resource class specified by ClassId")]
    public string MessageKeyDescription => this[nameof (MessageKeyDescription)];

    /// <summary>Site Sync settings.</summary>
    /// <value>Site Sync settings</value>
    [ResourceEntry("SiteSyncConfigCaption", Description = "The title of this class.", LastModified = "2016/09/13", Value = "Site Sync settings")]
    public string SiteSyncConfigCaption => this[nameof (SiteSyncConfigCaption)];

    /// <summary>Resource strings for Site Sync settings.</summary>
    /// <value>Resource strings for Site Sync settings.</value>
    [ResourceEntry("SiteSyncConfigDescription", Description = "The description of this class.", LastModified = "2016/09/13", Value = "Resource strings for Site Sync settings.")]
    public string SiteSyncConfigDescription => this[nameof (SiteSyncConfigDescription)];

    /// <summary>phrase: Receiving servers</summary>
    [ResourceEntry("ReceivingServersCaption", Description = "phrase: Receiving servers", LastModified = "2011/10/05", Value = "Receiving servers")]
    public string ReceivingServersCaption => this[nameof (ReceivingServersCaption)];

    /// <summary>phrase: Gets the list of receiving servers</summary>
    [ResourceEntry("ReceivingServersDescription", Description = "phrase: Gets the list of receiving servers", LastModified = "2011/10/05", Value = "Gets the list of receiving servers.")]
    public string ReceivingServersDescription => this[nameof (ReceivingServersDescription)];

    /// <summary>phrase: Dynamic Field Containers</summary>
    [ResourceEntry("DynamicFieldContainerSyncCaption", Description = "phrase: Dynamic Field Containers", LastModified = "2014/01/14", Value = "Dynamic Field Containers")]
    public string DynamicFieldContainerSyncCaption => this[nameof (DynamicFieldContainerSyncCaption)];

    /// <summary>
    /// phrase: The configuration for IDyanamicFieldContainer modules synchronization
    /// </summary>
    [ResourceEntry("DynamicFieldContainerSyncDescription", Description = "phrase: The configuration for IDyanamicFieldContainer modules synchronization", LastModified = "2013/11/07", Value = "The configuration for IDyanamicFieldContainer modules synchronization")]
    public string DynamicFieldContainerSyncDescription => this[nameof (DynamicFieldContainerSyncDescription)];

    /// <summary>phrase: Destination server response timeout</summary>
    /// <value>Destination server response timeout</value>
    [ResourceEntry("SiteSyncClientRequestTimeoutTitle", Description = "phrase: Destination server response timeout", LastModified = "2018/11/23", Value = "Destination server response timeout")]
    public string SiteSyncClientRequestTimeoutTitle => this[nameof (SiteSyncClientRequestTimeoutTitle)];

    /// <summary>
    /// phrase: Specifies (in minutes) how long the request, used to get a response from the destination server, should last before timing out.
    /// </summary>
    /// <value>Specifies (in minutes) how long the request, used to get a response from the destination server, should last before timing out.</value>
    [ResourceEntry("SiteSyncClientRequestTimeoutDescription", Description = "phrase: Specifies (in minutes) how long the request, used to get a response from the destination server, should last before timing out.", LastModified = "2018/11/23", Value = "Specifies (in minutes) how long the request, used to get a response from the destination server, should last before timing out.")]
    public string SiteSyncClientRequestTimeoutDescription => this[nameof (SiteSyncClientRequestTimeoutDescription)];

    /// <summary>phrase: Promote blob data request timeout</summary>
    /// <value>Promote blob data request timeout</value>
    [ResourceEntry("SiteSyncClientBlobRequestTimeoutTitle", Description = "phrase: Promote blob data request timeout", LastModified = "2018/11/23", Value = "Promote blob data request timeout")]
    public string SiteSyncClientBlobRequestTimeoutTitle => this[nameof (SiteSyncClientBlobRequestTimeoutTitle)];

    /// <summary>
    /// phrase: Specifies (in minutes) how long the request, used to send blob data to the destination server, should last before timing out.
    /// </summary>
    /// <value>Specifies (in minutes) how long the request, used to send blob data to the destination server, should last before timing out.</value>
    [ResourceEntry("SiteSyncClientBlobRequestTimeoutDescription", Description = "phrase: Specifies (in minutes) how long the request, used to send blob data to the destination server, should last before timing out.", LastModified = "2018/11/23", Value = "Specifies (in minutes) how long the request, used to send blob data to the destination server, should last before timing out.")]
    public string SiteSyncClientBlobRequestTimeoutDescription => this[nameof (SiteSyncClientBlobRequestTimeoutDescription)];

    /// <summary>Gets the properties to skip title.</summary>
    [ResourceEntry("PropertiesToSkip", Description = "Describes configuration element. ", LastModified = "2012/01/05", Value = "Properties to be skipped")]
    public string PropertiesToSkip => this[nameof (PropertiesToSkip)];

    /// <summary>
    /// Description of the PropertiesToSkip configuration element.
    /// </summary>
    [ResourceEntry("PropertiesToSkipDiscription", Description = "Describes configuration element.", LastModified = "2012/01/05", Value = "Properties which should not be copied across to widgets during the migration")]
    public string PropertiesToSkipDiscription => this[nameof (PropertiesToSkipDiscription)];

    /// <summary>Gets the migrate custom providers as dynamic modules.</summary>
    /// <value>The migrate custom providers as dynamic modules.</value>
    [ResourceEntry("MigrateCustomProvidersAsDynamicModules", Description = "Describes configuration element. ", LastModified = "2012/03/21", Value = "Migrate custom providers as dynamic modules")]
    public string MigrateCustomProvidersAsDynamicModules => this[nameof (MigrateCustomProvidersAsDynamicModules)];

    /// <summary>
    /// Gets the migrate custom providers as dynamic modules discription.
    /// </summary>
    /// <value>The migrate custom providers as dynamic modules discription.</value>
    [ResourceEntry("MigrateCustomProvidersAsDynamicModulesDiscription", Description = "Describes configuration element.", LastModified = "2012/03/21", Value = "If this is not checked all custom provider items will be migrated in one place, otherwise for each custom provider will be created a dynamic module.")]
    public string MigrateCustomProvidersAsDynamicModulesDiscription => this[nameof (MigrateCustomProvidersAsDynamicModulesDiscription)];

    /// <summary>phrase: Enable users and roles synchronization</summary>
    [ResourceEntry("SecuritySnapInSyncEnabledTitle", Description = "phrase: Enable users and roles synchronization", LastModified = "2018/11/23", Value = "Enable users and roles synchronization")]
    public string SecuritySnapInSyncEnabledTitle => this[nameof (SecuritySnapInSyncEnabledTitle)];

    /// <summary>
    /// phrase: Controls whether the option to promote users and roles to the destination environment will be visible on the SiteSync module screen. This option is automatically enabled in single to multisite migration mode.
    /// </summary>
    [ResourceEntry("SecuritySnapInSyncEnabledDescription", Description = "phrase: Controls whether the option to promote users and roles to the destination environment will be visible on the SiteSync module screen. This option is automatically enabled in single to multisite migration mode.", LastModified = "2018/11/23", Value = "Controls whether the option to promote users and roles to the destination environment will be visible on the SiteSync module screen. This option is automatically enabled in single to multisite migration mode.")]
    public string SecuritySnapInSyncEnabledDescription => this[nameof (SecuritySnapInSyncEnabledDescription)];

    /// <summary>phrase: Enable the security snap-in in sync mode</summary>
    [ResourceEntry("SiteSyncCompletedSubmittedNotificationTemplateIdTitle", Description = "The title of the configuration element.", LastModified = "2015/04/29", Value = "SyncNotificationTemplateId")]
    public string SiteSyncCompletedSubmittedNotificationTemplateIdTitle => this[nameof (SiteSyncCompletedSubmittedNotificationTemplateIdTitle)];

    /// <summary>phrase: Enable the security snap-in in sync mode</summary>
    [ResourceEntry("SiteSyncCompletedNotificationTemplateIdDescription", Description = "Describes configuration element.", LastModified = "2015/04/29", Value = "Represents the id of the mail message template used for sending notifications to the subscribed user when a sync completes.")]
    public string SiteSyncCompletedNotificationTemplateIdDescription => this[nameof (SiteSyncCompletedNotificationTemplateIdDescription)];

    /// <summary>
    /// Gets the 3.7 custom pages url are migrated as custom url description.
    /// </summary>
    /// <value>The 3.7 custom pages url are migrated as custom url description</value>
    [ResourceEntry("Migrate37CustomPagesUrlAsCustomUrlDescription", Description = "Describes configuration element. ", LastModified = "2014/04/28", Value = "Migrate all pages with custom 3.7 URLs as custom URLs. This means that the hierarchical structure in the pages URLs will be lost, and if someone in the future changes the URL of some parent page that has a custom url the changes won't affect the child pages.")]
    public string Migrate37CustomPagesUrlAsCustomUrlDescription => this[nameof (Migrate37CustomPagesUrlAsCustomUrlDescription)];

    /// <summary>
    /// Gets the 3.7 custom pages url are migrated as custom url.
    /// </summary>
    /// <value>The 3.7 custom pages url are migrated as custom url</value>
    [ResourceEntry("Migrate37CustomPagesUrlAsCustomUrl", Description = "Describes configuration element. ", LastModified = "2014/04/28", Value = "Migrate all pages with 3.7 custom URLs as custom URLs.")]
    public string Migrate37CustomPagesUrlAsCustomUrl => this[nameof (Migrate37CustomPagesUrlAsCustomUrl)];

    /// <summary>Gets the original urls are migrated as additional.</summary>
    /// <value>The original urls are migrated as additional.</value>
    [ResourceEntry("OriginalUrlsAreMigratedAsAdditional", Description = "Describes configuration element. ", LastModified = "2012/03/30", Value = "Migrate original urls as additional")]
    public string OriginalUrlsAreMigratedAsAdditional => this[nameof (OriginalUrlsAreMigratedAsAdditional)];

    /// <summary>
    /// Gets the original urls are migrated as additional discription.
    /// </summary>
    /// <value>The original urls are migrated as additional discription.</value>
    [ResourceEntry("OriginalUrlsAreMigratedAsAdditionalDiscription", Description = "Describes configuration element.", LastModified = "2012/03/30", Value = "This setting does not affect pages.")]
    public string OriginalUrlsAreMigratedAsAdditionalDiscription => this[nameof (OriginalUrlsAreMigratedAsAdditionalDiscription)];

    /// <summary>Determines whether counters should be displayed.</summary>
    [ResourceEntry("DisplayCountersTitle", Description = "Determines whether counters should be displayed.", LastModified = "2011/12/12", Value = "Display counters")]
    public string DisplayCountersTitle => this["DisplayCountersTitle "];

    /// <summary>phrase: The sort expression for this control.</summary>
    [ResourceEntry("SortExpressionDescription", Description = "phrase: The sort expression for this control.", LastModified = "2012/01/31", Value = "The sort expression for this control.")]
    public string SortExpressionDescription => this[nameof (SortExpressionDescription)];

    /// <summary>phrase: Title of the create prompt dialog</summary>
    [ResourceEntry("CreatePromptTitleCaption", Description = "phrase: Title of the create prompt dialog", LastModified = "2012/02/07", Value = "Title of the create prompt dialog")]
    public string CreatePromptTitleCaption => this[nameof (CreatePromptTitleCaption)];

    /// <summary>
    /// phrase: Description of the title of the create prompt dialog.
    /// </summary>
    [ResourceEntry("CreatePromptTitleDescription", Description = "phrase: Description of the title of the create prompt dialog.", LastModified = "2012/02/07", Value = "Description of the title of the create prompt dialog.")]
    public string CreatePromptTitleDescription => this[nameof (CreatePromptTitleDescription)];

    /// <summary>
    /// phrase: Title of the main textbox of the create prompt dialog
    /// </summary>
    [ResourceEntry("CreatePromptTextFieldTitleCaption", Description = "phrase: Title of the main textbox of the create prompt dialog", LastModified = "2012/02/07", Value = "Title of the main textbox of the create prompt dialog")]
    public string CreatePromptTextFieldTitleCaption => this[nameof (CreatePromptTextFieldTitleCaption)];

    /// <summary>
    /// phrase: Description of the title of the main textbox of the create prompt dialog.
    /// </summary>
    [ResourceEntry("CreatePromptTextFieldTitleDescription", Description = "phrase: Description of the title of the main textbox of the create prompt dialog.", LastModified = "2012/02/07", Value = "Description of the title of the main textbox of the create prompt dialog.")]
    public string CreatePromptTextFieldTitleDescription => this[nameof (CreatePromptTextFieldTitleDescription)];

    /// <summary>
    /// phrase: Example text of the main textbox of the create prompt dialog
    /// </summary>
    [ResourceEntry("CreatePromptExampleTextCaption", Description = "phrase: Example text of the main textbox of the create prompt dialog", LastModified = "2012/02/07", Value = "Example text of the main textbox of the create prompt dialog")]
    public string CreatePromptExampleTextCaption => this[nameof (CreatePromptExampleTextCaption)];

    /// <summary>
    /// phrase: Description of the example text of the main textbox of the create prompt dialog.
    /// </summary>
    [ResourceEntry("CreatePromptExampleTextDescription", Description = "phrase: Description of the example text of the main textbox of the create prompt dialog.", LastModified = "2012/02/07", Value = "Description of the example text of the main textbox of the create prompt dialog.")]
    public string CreatePromptExampleTextDescription => this[nameof (CreatePromptExampleTextDescription)];

    /// <summary>
    /// phrase: Text for the create button of the create prompt dialog
    /// </summary>
    [ResourceEntry("CreatePromptCreateButtonTitleCaption", Description = "phrase: Text for the create button of the create prompt dialog", LastModified = "2012/02/07", Value = "Text for the create button of the create prompt dialog")]
    public string CreatePromptCreateButtonTitleCaption => this[nameof (CreatePromptCreateButtonTitleCaption)];

    /// <summary>
    /// phrase: Description of the text for the create button of the create prompt dialog.
    /// </summary>
    [ResourceEntry("CreatePromptCreateButtonTitleDescription", Description = "phrase: Description of the text for the create button of the create prompt dialog.", LastModified = "2012/02/07", Value = "Description of the text for the create button of the create prompt dialog.")]
    public string CreatePromptCreateButtonTitleDescription => this[nameof (CreatePromptCreateButtonTitleDescription)];

    /// <summary>
    /// phrase: URL of the service to be used by the create prompt dialog for generating new items
    /// </summary>
    [ResourceEntry("CreateNewItemServiceUrlCaption", Description = "phrase: URL of the service to be used by the create prompt dialog for generating new items", LastModified = "2012/02/07", Value = "URL of the service to be used by the create prompt dialog for generating new items")]
    public string CreateNewItemServiceUrlCaption => this[nameof (CreateNewItemServiceUrlCaption)];

    /// <summary>
    /// phrase: Description of the URL of the service to be used by the create prompt dialog for generating new items.
    /// </summary>
    [ResourceEntry("CreateNewItemServiceUrlDescription", Description = "phrase: Description of the URL of the service to be used by the create prompt dialog for generating new items.", LastModified = "2012/02/07", Value = "Description of the URL of the service to be used by the create prompt dialog for generating new items.")]
    public string CreateNewItemServiceUrlDescription => this[nameof (CreateNewItemServiceUrlDescription)];

    /// <summary>
    /// phrase: A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).
    /// </summary>
    [ResourceEntry("ProvidersGroupsMessage", Description = "phrase: A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).", LastModified = "2012/02/23", Value = "A comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).")]
    public string ProvidersGroupsMessage => this[nameof (ProvidersGroupsMessage)];

    /// <summary>
    /// phrase: Description of the comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).
    /// </summary>
    [ResourceEntry("ProvidersGroupsDescription", Description = "phrase: Description of the comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).", LastModified = "2012/02/23", Value = "Description of the comma-delimited list of the names of the groups of providers valid for this master list (e.g. System providers).")]
    public string ProvidersGroupsDescription => this[nameof (ProvidersGroupsDescription)];

    /// <summary>Defines user profiles options.</summary>
    [ResourceEntry("UserProfilesConfigTitle", Description = "Defines user profiles options.", LastModified = "2012/04/05", Value = "User profiles configuration")]
    public string UserProfilesConfigTitle => this[nameof (UserProfilesConfigTitle)];

    /// <summary>Defines user profiles options.</summary>
    [ResourceEntry("UserProfilesConfigDescription", Description = "Defines user profiles options.", LastModified = "2012/04/05", Value = "User profiles configuration")]
    public string UserProfilesConfigDescription => this[nameof (UserProfilesConfigDescription)];

    /// <summary>Avatars upload path.</summary>
    [ResourceEntry("AvatarsUploadPathTitle", Description = "Avatars upload path.", LastModified = "2012/04/05", Value = "Avatars upload path")]
    public string AvatarsUploadPathTitle => this[nameof (AvatarsUploadPathTitle)];

    /// <summary>Defines avatars upload path.</summary>
    [ResourceEntry("AvatarsUploadPathDescription", Description = "Defines avatars upload path.", LastModified = "2012/04/05", Value = "Avatars upload path")]
    public string AvatarsUploadPathDescription => this[nameof (AvatarsUploadPathDescription)];

    /// <summary>Defines avatars host title.</summary>
    [ResourceEntry("AvatarsHostTitle", Description = "Defines avatars host title.", LastModified = "2012/04/05", Value = "Avatars host")]
    public string AvatarsHostTitle => this[nameof (AvatarsHostTitle)];

    /// <summary>Defines avatars host title.</summary>
    [ResourceEntry("AvatarsHostDescription", Description = "Defines avatars host title.", LastModified = "2012/04/05", Value = "Avatars host")]
    public string AvatarsHostDescription => this[nameof (AvatarsHostDescription)];

    /// <summary>Resource strings for widget class.</summary>
    [ResourceEntry("SystemFoldersConfigCaption", Description = "The title of this class.", LastModified = "2012/01/26", Value = "System folders class")]
    public string SystemFoldersConfigCaption => this[nameof (SystemFoldersConfigCaption)];

    /// <summary>Resource strings for Libraries class.</summary>
    [ResourceEntry("SystemFoldersConfigDescription", Description = "The description of this class.", LastModified = "2012/01/26", Value = "Resource strings for System folders class.")]
    public string SystemFoldersConfigDescription => this[nameof (SystemFoldersConfigDescription)];

    /// <summary>
    /// Title of SecurityConfig -&gt; FilterQueriesByViewPermissions configuration property.
    /// </summary>
    [ResourceEntry("FilterQueriesByViewPermissions", Description = "Title of SecurityConfig -> FilterQueriesByViewPermissions configuration property.", LastModified = "2012/02/22", Value = "Enable filtering queries by view permissions")]
    public string FilterQueriesByViewPermissions => this[nameof (FilterQueriesByViewPermissions)];

    /// <summary>
    /// Description of SecurityConfig -&gt; FilterQueriesByViewPermissions configuration property.
    /// </summary>
    [ResourceEntry("FilterQueriesByViewPermissionsDescription", Description = "Description of SecurityConfig -> FilterQueriesByViewPermissions configuration property.", LastModified = "2014/12/05", Value = "This option could be also configured on provider level. If it is not set in the provider, this setting will be used. (This setting will take effect after application restart.)")]
    public string FilterQueriesByViewPermissionsDescription => this[nameof (FilterQueriesByViewPermissionsDescription)];

    /// <summary>
    /// Describes the supported countries configuration property.
    /// </summary>
    [ResourceEntry("SupportedCountriesConfig", Description = "Describes the supported countries configuration property.", LastModified = "2013/02/28", Value = "Supported countries.")]
    public string SupportedCountriesConfig => this[nameof (SupportedCountriesConfig)];

    /// <summary>
    /// Describes the supported states and provinces configuration property.
    /// </summary>
    [ResourceEntry("SupportedStatesConfig", Description = "Describes the supported states and provinces configuration property.", LastModified = "2013/02/28", Value = "Supported states and provinces.")]
    public string SupportedStatesConfig => this[nameof (SupportedStatesConfig)];

    /// <summary>Caption of the ManagerType property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetManagerTypeCaption", Description = "Caption of the ManagerType property.", LastModified = "2013/03/04", Value = "ManagerType")]
    public string FolderBreadcrumbWidgetManagerTypeCaption => this[nameof (FolderBreadcrumbWidgetManagerTypeCaption)];

    /// <summary>Description of the ManagerType property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetManagerTypeDescription", Description = "Description of the ManagerType property.", LastModified = "2013/03/04", Value = "The type of the manager that is going to be used to get folders.")]
    public string FolderBreadcrumbWidgetManagerTypeDescription => this[nameof (FolderBreadcrumbWidgetManagerTypeDescription)];

    /// <summary>Defines the CORS policy.</summary>
    [ResourceEntry("CorsDescription", Description = "Describes configuration element.", LastModified = "2021/03/01", Value = "Allow HTTP access control (CORS) for specific domains. Enter domain, or '*' for all domains. Example: http://www.mytrustedhost.com . This setting will conflict with a custom header in the web.config file for Access-Control-Allow-Origin.")]
    public string CorsDescription => this[nameof (CorsDescription)];

    /// <summary>Caption of the NavigationPageId property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetNavigationPageIdCaption", Description = "Caption of the NavigationPageId property.", LastModified = "2013/03/04", Value = "NavigationPageId")]
    public string FolderBreadcrumbWidgetNavigationPageIdCaption => this[nameof (FolderBreadcrumbWidgetNavigationPageIdCaption)];

    /// <summary>Description of the NavigationPageId property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetNavigationPageIdDescription", Description = "Description of the NavigationPageId property.", LastModified = "2013/03/04", Value = "The navigation page id.")]
    public string FolderBreadcrumbWidgetNavigationPageIdDescription => this[nameof (FolderBreadcrumbWidgetNavigationPageIdDescription)];

    /// <summary>Caption of the RootPageId property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetRootPageIdCaption", Description = "Caption of the RootPageId property.", LastModified = "2013/03/04", Value = "RootPageId")]
    public string FolderBreadcrumbWidgetRootPageIdCaption => this[nameof (FolderBreadcrumbWidgetRootPageIdCaption)];

    /// <summary>Description of the RootPageId property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetRootPageIdDescription", Description = "Description of the RootPageId property.", LastModified = "2013/03/04", Value = "The root page id.")]
    public string FolderBreadcrumbWidgetRootPageIdDescription => this[nameof (FolderBreadcrumbWidgetRootPageIdDescription)];

    /// <summary>Caption of the RootTitle property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetRootTitleCaption", Description = "Caption of the RootTitle property.", LastModified = "2013/03/04", Value = "RootTitle")]
    public string FolderBreadcrumbWidgetRootTitleCaption => this[nameof (FolderBreadcrumbWidgetRootTitleCaption)];

    /// <summary>Description of the RootTitle property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetRootTitleDescription", Description = "Description of the RootTitle property.", LastModified = "2013/03/04", Value = "The title for the root link.")]
    public string FolderBreadcrumbWidgetRootTitleDescription => this[nameof (FolderBreadcrumbWidgetRootTitleDescription)];

    /// <summary>Caption of the AppendRootUrl property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetAppendRootUrlCaption", Description = "Caption of the AppendRootUrl property.", LastModified = "2013/03/04", Value = "AppendRootUrl")]
    public string FolderBreadcrumbWidgetAppendRootUrlCaption => this[nameof (FolderBreadcrumbWidgetAppendRootUrlCaption)];

    /// <summary>Description of the AppendRootUrl property.</summary>
    [ResourceEntry("FolderBreadcrumbWidgetAppendRootUrlDescription", Description = "Description of the AppendRootUrl property.", LastModified = "2013/03/04", Value = "Whether to append the root item URL.")]
    public string FolderBreadcrumbWidgetAppendRootUrlDescription => this[nameof (FolderBreadcrumbWidgetAppendRootUrlDescription)];

    /// <summary>Describes the country IsoCode configuration property.</summary>
    [ResourceEntry("CountryIsoCodeConfig", Description = "Describes the country IsoCode configuration property.", LastModified = "2013/02/28", Value = "Gets or sets the ISO code (e.g. US, DE) for the country.")]
    public string CountryIsoCodeConfig => this[nameof (CountryIsoCodeConfig)];

    /// <summary>Caption of the ParentDataKeyName property.</summary>
    [ResourceEntry("DynamicCommandParentDataKeyNameCaption", Description = "Caption of the ParentDataKeyName property.", LastModified = "2013/03/07", Value = "ParentDataKeyName")]
    public string DynamicCommandParentDataKeyNameCaption => this[nameof (DynamicCommandParentDataKeyNameCaption)];

    /// <summary>Description of the ParentDataKeyName property.</summary>
    [ResourceEntry("DynamicCommandParentDataKeyNameDescription", Description = "Description of the ParentDataKeyName property.", LastModified = "2013/03/07", Value = "The name of the property that references the parent.")]
    public string DynamicCommandParentDataKeyNameDescription => this[nameof (DynamicCommandParentDataKeyNameDescription)];

    /// <summary>ShowIcon</summary>
    [ResourceEntry("ShowIconTitle", Description = "The title of the configuration element.", LastModified = "2013/03/18", Value = "ShowIcon")]
    public string ShowIconTitle => this[nameof (ShowIconTitle)];

    /// <summary>
    /// Represents ShowIcon property of each color picker field control.
    /// </summary>
    [ResourceEntry("ShowIconDescription", Description = "Describes configuration element.", LastModified = "2013/03/18", Value = "Represents ShowIcon property of each color picker control.")]
    public string ShowIconDescription => this[nameof (ShowIconDescription)];

    /// <summary>Preset</summary>
    [ResourceEntry("PresetTitle", Description = "The title of the configuration element.", LastModified = "2013/03/18", Value = "Preset")]
    public string PresetTitle => this["ShowIconTitle"];

    /// <summary>
    /// Represents Preset property of each color picker field control.
    /// </summary>
    [ResourceEntry("PresetDescription", Description = "Describes configuration element.", LastModified = "2013/03/18", Value = "Represents Preset property of each color picker control.")]
    public string PresetDescription => this[nameof (PresetDescription)];

    /// <summary>ShowEmptyColor</summary>
    [ResourceEntry("ShowEmptyColorTitle", Description = "The title of the configuration element.", LastModified = "2013/03/18", Value = "ShowEmptyColor")]
    public string ShowEmptyColorTitle => this[nameof (ShowEmptyColorTitle)];

    /// <summary>
    /// Represents ShowEmptyColor property of each color picker field control.
    /// </summary>
    [ResourceEntry("ShowEmptyColorDescription", Description = "Describes configuration element.", LastModified = "2013/03/18", Value = "Represents ShowEmptyColor property of each color picker control.")]
    public string ShowEmptyColorDescription => this[nameof (ShowEmptyColorDescription)];

    /// <summary>EnableColorPreview</summary>
    [ResourceEntry("EnableColorPreviewTitle", Description = "The title of the configuration element.", LastModified = "2013/03/18", Value = "EnableColorPreview")]
    public string EnableColorPreviewTitle => this[nameof (EnableColorPreviewTitle)];

    /// <summary>
    /// Represents EnableColorPreview property of each color picker field control.
    /// </summary>
    [ResourceEntry("EnableColorPreviewDescription", Description = "Describes configuration element.", LastModified = "2013/03/18", Value = "Represents EnableColorPreview property of each color picker control.")]
    public string EnableColorPreviewDescription => this[nameof (EnableColorPreviewDescription)];

    /// <summary>ShowRecentlyUsedColors</summary>
    [ResourceEntry("ShowRecentlyUsedColorsTitle", Description = "The title of the configuration element.", LastModified = "2013/03/18", Value = "ShowRecentlyUsedColors")]
    public string ShowRecentlyUsedColorsTitle => this[nameof (ShowRecentlyUsedColorsTitle)];

    /// <summary>
    /// Represents ShowRecentlyUsedColors property of each color picker field control.
    /// </summary>
    [ResourceEntry("ShowRecentlyUsedColorsDescription", Description = "Describes configuration element.", LastModified = "2013/03/18", Value = "Represents ShowRecentlyUsedColors property of each color picker control.")]
    public string ShowRecentlyUsedColorsDescription => this[nameof (ShowRecentlyUsedColorsDescription)];

    /// <summary>ShowIcon</summary>
    [ResourceEntry("EnableCustomColorTitle", Description = "The title of the configuration element.", LastModified = "2013/03/18", Value = "EnableCustomColor")]
    public string EnableCustomColorTitle => this[nameof (EnableCustomColorTitle)];

    /// <summary>
    /// Represents EnableCustomColor property of each color picker field control.
    /// </summary>
    [ResourceEntry("EnableCustomColorDescription", Description = "Describes configuration element.", LastModified = "2013/03/18", Value = "Represents EnableCustomColor property of each color picker control.")]
    public string EnableCustomColorDescription => this[nameof (EnableCustomColorDescription)];

    /// <summary>ChoicesConfig</summary>
    [ResourceEntry("ColorPickerConfigTitle", Description = "The title of the configuration element.", LastModified = "2013/03/18", Value = "ColorPickerConfig")]
    public string ColorPickerConfigTitle => this[nameof (ColorPickerConfigTitle)];

    /// <summary>Represents ColorPickerConfig property.</summary>
    [ResourceEntry("ColorPickerConfigDescription", Description = "Describes configuration element.", LastModified = "2013/03/18", Value = "Represents ColorPickerConfig property.")]
    public string ColorPickerConfigDescription => this[nameof (ColorPickerConfigDescription)];

    /// <summary>Text for the RangeStartText property.</summary>
    [ResourceEntry("RecurrencyFieldRangeStartTextTitle", Description = "Text for the RangeStartText property.", LastModified = "2013/03/21", Value = "RangeStartText")]
    public string RecurrencyFieldRangeStartTextTitle => this[nameof (RecurrencyFieldRangeStartTextTitle)];

    /// <summary>Description for the RangeStartText property.</summary>
    [ResourceEntry("RecurrencyFieldRangeStartTextDescription", Description = "Description for the RangeStartText property.", LastModified = "2013/03/21", Value = "The text for recurrency range start.")]
    public string RecurrencyFieldRangeStartTextDescription => this[nameof (RecurrencyFieldRangeStartTextDescription)];

    /// <summary>Text for the RangeEndText property.</summary>
    [ResourceEntry("RecurrencyFieldRangeEndTextTitle", Description = "Text for the RangeEndText property.", LastModified = "2013/03/21", Value = "RangeEndText")]
    public string RecurrencyFieldRangeEndTextTitle => this[nameof (RecurrencyFieldRangeEndTextTitle)];

    /// <summary>Description for the RangeEndText property.</summary>
    [ResourceEntry("RecurrencyFieldRangeEndTextDescription", Description = "Description for the RangeEndText property.", LastModified = "2013/03/21", Value = "The text for recurrency range end.")]
    public string RecurrencyFieldRangeEndTextDescription => this[nameof (RecurrencyFieldRangeEndTextDescription)];

    /// <summary>Phrase: Single item name.</summary>
    [ResourceEntry("FolderFieldItemNameCaption", Description = "Phrase: Single item name", LastModified = "2013/03/28", Value = "Single item name")]
    public string FolderFieldItemNameCaption => this[nameof (FolderFieldItemNameCaption)];

    /// <summary>Phrase: The name of a single item</summary>
    [ResourceEntry("FolderFieldItemNameDescription", Description = "Phrase: The name of a single item", LastModified = "2013/03/28", Value = "The name of a single item")]
    public string FolderFieldItemNameDescription => this[nameof (FolderFieldItemNameDescription)];

    /// <summary>
    /// API key for loading Google Maps API used in Address fields.
    /// </summary>
    [ResourceEntry("GoogleMapApiV3KeyDescription", Description = "API key for loading Google Maps API used in Address fields.", LastModified = "2013/03/26", Value = "API key for loading Google Maps API used in Address fields.")]
    public string GoogleMapApiV3KeyDescription => this[nameof (GoogleMapApiV3KeyDescription)];

    /// <summary>
    /// API key for loading Google Maps API used in Address fields.
    /// </summary>
    [ResourceEntry("GoogleMapApiV3KeyTitle", Description = "API key for loading Google Maps API used in Address fields.", LastModified = "2013/04/02", Value = "Google Map Api V3 Key")]
    public string GoogleMapApiV3KeyTitle => this[nameof (GoogleMapApiV3KeyTitle)];

    [ResourceEntry("EnableGeoLocationSearchTitle", Description = "", LastModified = "2013/04/02", Value = "Enable Geo Location Search")]
    public string EnableGeoLocationSearchTitle => this[nameof (EnableGeoLocationSearchTitle)];

    [ResourceEntry("EnableGeoLocationSearchDescription", Description = "", LastModified = "2013/04/02", Value = "Enable the option to search for the nearest locations")]
    public string EnableGeoLocationSearchDescription => this[nameof (EnableGeoLocationSearchDescription)];

    [ResourceEntry("GeoLocationSettingsElementTitle", Description = "", LastModified = "2013/04/02", Value = "Geo Location Settings")]
    public string GeoLocationSettingsElementTitle => this[nameof (GeoLocationSettingsElementTitle)];

    [ResourceEntry("GeoLocationSettingsElementDescription", Description = "", LastModified = "2013/04/02", Value = "Configuration element for geo location settings.")]
    public string GeoLocationSettingsElementDescription => this[nameof (GeoLocationSettingsElementDescription)];

    /// <summary>Global settings for using content locations service</summary>
    [ResourceEntry("ContentLocationsSettingsTitle", Description = "Global settings for using content locations service", LastModified = "2013/04/02", Value = "Content Locations")]
    public string ContentLocationsSettingsTitle => this[nameof (ContentLocationsSettingsTitle)];

    /// <summary>Global content locations settings.</summary>
    [ResourceEntry("ContentLocationsSettingsDescription", Description = "Global content locations settings.", LastModified = "2013/05/23", Value = "Global content locations settings.")]
    public string ContentLocationsSettingsDescription => this[nameof (ContentLocationsSettingsDescription)];

    [ResourceEntry("DisableCanonicalUrlsTitle", Description = "", LastModified = "2013/04/02", Value = "Disable Canonical URLs")]
    public string DisableCanonicalUrlsTitle => this[nameof (DisableCanonicalUrlsTitle)];

    /// <summary>
    /// Turns on/off the canonical meta tags generation for all pages (static and dynamic). If disabled it can not be changed on a lower level. If it is enabled you can turn it off on a lower level (e.g. only for a particular page).
    /// </summary>
    [ResourceEntry("DisableCanonicalUrlsDescription", Description = "Turns on/off the canonical meta tags generation for all pages (static and dynamic). If disabled it can not be changed on a lower level. If it is enabled you can turn it off on a lower level (e.g. only for a particular page).", LastModified = "2013/05/23", Value = "Turns on/off the canonical meta tags generation for all pages (static and dynamic). If disabled it can not be changed on a lower level. If it is enabled you can turn it off on a lower level (e.g. only for a particular page).")]
    public string DisableCanonicalUrlsDescription => this[nameof (DisableCanonicalUrlsDescription)];

    /// <summary>
    /// Global settings for default canonical url on static pages.
    /// </summary>
    [ResourceEntry("PageDefaultCanonicalUrlSettingsElementTitle", Description = "Global settings for default canonical url on static pages.", LastModified = "2013/05/14", Value = "Global settings for default canonical url on static pages.")]
    public string PageDefaultCanonicalUrlSettingsElementTitle => this[nameof (PageDefaultCanonicalUrlSettingsElementTitle)];

    /// <summary>
    /// Global settings for default canonical url on static pages.
    /// </summary>
    [ResourceEntry("PageDefaultCanonicalUrlSettingsElementDescription", Description = "Global settings for default canonical url on static pages.", LastModified = "2013/05/14", Value = "Global settings for default canonical url on static pages.")]
    public string PageDefaultCanonicalUrlSettingsElementDescription => this[nameof (PageDefaultCanonicalUrlSettingsElementDescription)];

    /// <summary>
    /// Configure if each static page should have a canonical url by default. This setting could be overridden on each page.
    /// </summary>
    [ResourceEntry("EnablePageDefaultCanonicalUrlsTitle", Description = "Configure if each static page should have a canonical url by default. This setting could be overridden on each page.", LastModified = "2013/05/14", Value = "Enable Pages default canonical url tag.")]
    public string EnablePageDefaultCanonicalUrlsTitle => this[nameof (EnablePageDefaultCanonicalUrlsTitle)];

    /// <summary>
    /// White-list of query string parameters that will be set in the canonical url of the page from the current request.
    /// </summary>
    [ResourceEntry("AllowedCanonicalUrlQueryStringParametersDescription", Description = "White-list of query string parameters that will be set in the canonical url of the page from the current request.", LastModified = "2013/05/14", Value = "Specifies the allowed canonical url parameters when generating a canonical url from the current request. This is a white list of items that will be preserved in the canonical url as they reflect the page content. Each parameter that changes the page content should be added here.")]
    public string AllowedCanonicalUrlQueryStringParametersDescription => this[nameof (AllowedCanonicalUrlQueryStringParametersDescription)];

    /// <summary>
    /// Settings for the default canonical url on static pages.
    /// </summary>
    [ResourceEntry("PageDefaultCanonicalUrlSettingsTitle", Description = "Settings for the default canonical url on static pages.", LastModified = "2013/05/23", Value = "Page Default Canonical Url Settings")]
    public string PageDefaultCanonicalUrlSettingsTitle => this[nameof (PageDefaultCanonicalUrlSettingsTitle)];

    /// <summary>
    /// Settings for the default canonical url on static pages.
    /// </summary>
    [ResourceEntry("PageDefaultCanonicalUrlSettingsDescription", Description = "Settings for the default canonical url on static pages.", LastModified = "2013/05/23", Value = "Settings for the default canonical url on static pages (e.g. pages that do not have an item appendix in the url - http://mysite.com/home but not http://mysite.com/home/newsItem1).")]
    public string PageDefaultCanonicalUrlSettingsDescription => this[nameof (PageDefaultCanonicalUrlSettingsDescription)];

    /// <summary>
    /// Configure if each static page should have a canonical url by default. This setting could be overridden by each page.
    /// </summary>
    [ResourceEntry("EnablePageDefaultCanonicalUrlsDescription", Description = "Configure if each static page should have a canonical url by default. This setting could be overridden by each page.", LastModified = "2013/05/14", Value = "Configure if each static page should have a canonical url by default. This setting could be overridden by each page. This setting won't be applied if the whole feature has been disabled - one level up - Settings > Advanced > System > Content Locations > Disable canonical URLs.")]
    public string EnablePageDefaultCanonicalUrlsDescription => this[nameof (EnablePageDefaultCanonicalUrlsDescription)];

    /// <summary>
    /// Specifies the allowed canonical url parameters when generating a canonical url for the static page from the current request. This is a white list of items that will be preserved in the canonical url as they reflect the page content. Each parameter that changes the page content should be added here.
    /// </summary>
    [ResourceEntry("AllowedCanonicalUrlQueryStringParametersTitle", Description = "Specifies the allowed canonical url parameters when generating a canonical url for the static page from the current request. This is a white list of items that will be preserved in the canonical url as they reflect the page content. Each parameter that changes the page content should be added here.", LastModified = "2013/05/23", Value = "Allowed Canonical Url Query String Parameters")]
    public string AllowedCanonicalUrlQueryStringParametersTitle => this[nameof (AllowedCanonicalUrlQueryStringParametersTitle)];

    /// <summary>The query string parameter.</summary>
    [ResourceEntry("CanonicalUrlQyeryStringParameterNameTitle", Description = "The query string parameter.", LastModified = "2013/05/23", Value = "Parameter Name")]
    public string CanonicalUrlQyeryStringParameterNameTitle => this[nameof (CanonicalUrlQyeryStringParameterNameTitle)];

    /// <summary>
    /// The description displayed for the CanonicalUrlQyeryStringParameterNameDescription property in Content Locations QueryStrung parameters section.
    /// </summary>
    [ResourceEntry("CanonicalUrlQyeryStringParameterNameDescription", Description = "The description displayed for the CanonicalUrlQyeryStringParameterNameDescription property in Content Locations QueryStrung parameters section.", LastModified = "2013/05/23", Value = "The name of the query string parameter that will be preserved in the canonical url from the current web request.")]
    public string CanonicalUrlQyeryStringParameterNameDescription => this[nameof (CanonicalUrlQyeryStringParameterNameDescription)];

    /// <summary>The query string parameter and its settings.</summary>
    [ResourceEntry("CanonicalUrlQueryStringParameterElementTitle", Description = "The query string parameter and its settings.", LastModified = "2013/05/14", Value = "Specifies the allowed query string parameter in canonical url and its settings.")]
    public string CanonicalUrlQueryStringParameterElementTitle => this[nameof (CanonicalUrlQueryStringParameterElementTitle)];

    /// <summary>
    /// Specifies the title for canonical url resolver mode settings.
    /// </summary>
    [ResourceEntry("CanonicalUrlResolverModeTitle", Description = "Specifies the title for canonical url resolver mode settings.", LastModified = "2020/07/16", Value = "Canonical url resolver")]
    public string CanonicalUrlResolverModeTitle => this[nameof (CanonicalUrlResolverModeTitle)];

    /// <summary>
    /// Specifies the description for canonical url resolver mode settings.
    /// </summary>
    [ResourceEntry("CanonicalUrlResolverModeDescription", Description = "Specifies the description for canonical url resolver mode settings.", LastModified = "2020/07/16", Value = "Render canonical url based on selected item:\r\n Auto - Render url based on VaryByHost parameter from Page Cache Profiles and number of domain aliases. \r\n Dynamic - Render url based on domain resolving from current request. If page caching is enabled, VaryByHost parameter must be added on Page Cache Profiles. \r\n Static - Render url based on site domain. All domain aliases will be ignored.")]
    public string CanonicalUrlResolverModeDescription => this[nameof (CanonicalUrlResolverModeDescription)];

    /// <summary>Enable SingleItemMode widgets to modify page settings</summary>
    [ResourceEntry("EnableSingleItemModeWidgetsBackwardCompatibilityModeTitle", Description = "Enable SingleItemMode widgets to modify page settings", LastModified = "2013/05/23", Value = "Enable SingleItemMode widgets to modify page settings")]
    public string EnableSingleItemModeWidgetsBackwardCompatibilityModeTitle => this[nameof (EnableSingleItemModeWidgetsBackwardCompatibilityModeTitle)];

    /// <summary>
    /// Gets or sets a value indicating whether widget in single item mode should change page properties (e.g. page title, canonical url etc.)
    /// </summary>
    [ResourceEntry("EnableSingleItemModeWidgetsBackwardCompatibilityModeDescription", Description = "Gets or sets a value indicating whether widget in single item mode should change page properties (e.g. page title, canonical url etc.)", LastModified = "2013/05/23", Value = "Gets or sets a value indicating whether widget in single item mode should change page properties (e.g. page title, canonical url etc.)")]
    public string EnableSingleItemModeWidgetsBackwardCompatibilityModeDescription => this[nameof (EnableSingleItemModeWidgetsBackwardCompatibilityModeDescription)];

    /// <summary>
    /// Text that describes DeleteModulesIfNotInPackage property
    /// </summary>
    /// <value>When set to true, modules not present in the import packages will be deleted.</value>
    [ResourceEntry("DeleteModulesIfNotInPackage", Description = "Text that describes DeleteModulesIfNotInPackage property", LastModified = "2015/12/21", Value = "When set to true, modules not present in the import packages will be deleted.")]
    public string DeleteModulesIfNotInPackage => this[nameof (DeleteModulesIfNotInPackage)];

    /// <summary>
    /// Text that describes DeleteModulesIfNotInPackage property
    /// </summary>
    /// <value>When set to true, modules not present in the import packages will be deleted.</value>
    [ResourceEntry("DisableImport", Description = "Text that describes DisableImport property", LastModified = "2020/05/18", Value = "When set to true, changes in the deployment and addons folders will not be imported.")]
    public string DisableImport => this[nameof (DisableImport)];

    /// <summary>Text that describes RootFolderPath property</summary>
    /// <value>The root path where generated packages are stored.</value>
    [ResourceEntry("RootFolderPathDescription", Description = "Text that describes RootFolderPath property", LastModified = "2016/02/11", Value = "The root path where generated packages are stored.")]
    public string RootFolderPathDescription => this[nameof (RootFolderPathDescription)];

    /// <summary>Gets the packaging mode.</summary>
    /// <value>The packaging mode.</value>
    [ResourceEntry("PackagingMode", Description = "Text that describes PackagingMode property", LastModified = "2016/03/10", Value = "When set to 'Source' all functionality with packaging implemented is visible, when set to 'Target' this functionality is hidden from the UI.")]
    public string PackagingMode => this[nameof (PackagingMode)];

    /// <summary>
    /// Text that describes DisableMultisiteImportExport property
    /// </summary>
    [ResourceEntry("DisableMultisiteImportExport", Description = "Text that describes DisableMultisiteImportExport property", LastModified = "2016/09/20", Value = "When set to true, multisite sites will not be exported or imported on deployment.")]
    public string DisableMultisiteImportExport => this[nameof (DisableMultisiteImportExport)];

    /// <summary>Describes configuration element.</summary>
    /// <value>A collection of data provider settings.</value>
    [ResourceEntry("AccessTokenProviders", Description = "Describes configuration element.", LastModified = "2016/12/19", Value = "A collection of data provider settings.")]
    public string AccessTokenProviders => this[nameof (AccessTokenProviders)];

    /// <summary>Describes configuration element.</summary>
    /// <value>Specifies the default access token provider.</value>
    [ResourceEntry("DefaultAccessTokenProvider", Description = "Describes configuration element.", LastModified = "2016/12/19", Value = "Specifies the default access token provider.")]
    public string DefaultAccessTokenProvider => this[nameof (DefaultAccessTokenProvider)];

    /// <summary>Describes configuration element.</summary>
    /// <value>A collection of access token issuer settings.</value>
    [ResourceEntry("AccessTokenIssuers", Description = "Describes configuration element.", LastModified = "2016/12/23", Value = "A collection of access token issuer settings.")]
    public string AccessTokenIssuers => this[nameof (AccessTokenIssuers)];

    /// <summary>Describes configuration element.</summary>
    /// <value>A collection of access token issuer settings.</value>
    [ResourceEntry("EnableForAllSites", Description = "A value indicating whether to enable site shield for all sites.", LastModified = "2021/02/25", Value = "Enable Shield for all sites.")]
    public string EnableForAllSites => this[nameof (EnableForAllSites)];

    /// <summary>Describes configuration element.</summary>
    /// <value>Specifies the default access token issuer.</value>
    [ResourceEntry("DefaultAccessTokenIssuer", Description = "Describes configuration element.", LastModified = "2016/12/23", Value = "Specifies the default access token issuer.")]
    public string DefaultAccessTokenIssuer => this[nameof (DefaultAccessTokenIssuer)];

    /// <summary>
    /// Settings for the requirejs section in Inline Editing section.
    /// </summary>
    /// <value>Requirejs Modules</value>
    [ResourceEntry("RequireJsModulesTitle", Description = "Settings for the requirejs section in Inline Editing section.", LastModified = "2013/07/10", Value = "Requirejs Modules")]
    public string RequireJsModulesTitle => this[nameof (RequireJsModulesTitle)];

    /// <summary>
    /// Settings for the requirejs section in Inline Editing section.
    /// </summary>
    /// <value>Represents requirejs configuration collection, where the Key represents the module name and the Value represents the relative path to load the module javascript file.</value>
    [ResourceEntry("RequireJsModulesDescription", Description = "Settings for the requirejs section in Inline Editing section.", LastModified = "2013/07/10", Value = "Represents requirejs configuration collection, where the Key represents the module name and the Value represents the relative path to load the module javascript file.")]
    public string RequireJsModulesDescription => this[nameof (RequireJsModulesDescription)];

    /// <summary>phrase: URl fields per type</summary>
    /// <value>URl fields per type</value>
    [ResourceEntry("UrlFieldsTitle", Description = "phrase: URl fields per type", LastModified = "2013/09/03", Value = "URl fields per type")]
    public string UrlFieldsTitle => this[nameof (UrlFieldsTitle)];

    /// <summary>Text that describes UrlFields property</summary>
    /// <value>Specifies which field to be used for URL generation when its value is modified through Inline Editing. Where the Key is the full name of the item type(for example: Telerik.Sitefinity.News.Model.NewsItem) and the Value is the field name(for example: Title).</value>
    [ResourceEntry("UrlFieldsDescription", Description = "Text that describes UrlFields property", LastModified = "2013/09/03", Value = "Specifies which field to be used for URL generation when its value is modified through Inline Editing. Where the Key is the full name of the item type(for example: Telerik.Sitefinity.News.Model.NewsItem) and the Value is the field name(for example: Title).")]
    public string UrlFieldsDescription => this[nameof (UrlFieldsDescription)];

    /// <summary>Resource strings for comments class.</summary>
    [ResourceEntry("CommentsConfigCaption", Description = "The title of this class.", LastModified = "2013/08/13", Value = "Comments class")]
    public string CommentsConfigCaption => this[nameof (CommentsConfigCaption)];

    /// <summary>Resource strings for comments class.</summary>
    [ResourceEntry("CommentsConfigDescription", Description = "The description of this class.", LastModified = "2013/08/13", Value = "Resource strings for news class.")]
    public string CommentsConfigDescription => this[nameof (CommentsConfigDescription)];

    /// <summary>Gets the view controls.</summary>
    [ResourceEntry("ViewControls", Description = "Describes configuration element.", LastModified = "2013/08/20", Value = "Controls")]
    public string ViewControls => this[nameof (ViewControls)];

    /// <summary>Gets the view controls.</summary>
    [ResourceEntry("ViewControlsDescription", Description = "Describes configuration element.", LastModified = "2013/08/20", Value = "Collection of view control settings")]
    public string ViewControlsDescription => this[nameof (ViewControlsDescription)];

    /// <summary>Message: ViewContainer views</summary>
    [ResourceEntry("ViewContainerViews", Description = "", LastModified = "2013/08/21", Value = "Views")]
    public string ViewContainerViews => this[nameof (ViewContainerViews)];

    /// <summary>Message: ViewContainer definition name</summary>
    [ResourceEntry("ViewContainerViewsDescription", Description = "", LastModified = "2013/08/21", Value = "Defines the views of the ViewContainer control")]
    public string ViewContainerViewsDescription => this[nameof (ViewContainerViewsDescription)];

    /// <summary>The title of this class.</summary>
    [ResourceEntry("ViewContainerTitle", Description = "The title of this class.", LastModified = "2013/08/21", Value = "Title")]
    public string ViewContainerTitle => this[nameof (ViewContainerTitle)];

    /// <summary>The description of this class.</summary>
    [ResourceEntry("ViewContainerDescription", Description = "The description of this class.", LastModified = "2013/08/21", Value = "Description of the ViewContainer class.")]
    public string ViewContainerDescription => this[nameof (ViewContainerDescription)];

    /// <summary>Message: ViewContainer Control definition name</summary>
    [ResourceEntry("ViewContainerDefinitionName", Description = "", LastModified = "2013/08/21", Value = "ViewContainer control definition name")]
    public string ViewContainerDefinitionName => this[nameof (ViewContainerDefinitionName)];

    /// <summary>Message: ViewContainer control definition name</summary>
    [ResourceEntry("ViewContainerDefinitionNameDescription", Description = "", LastModified = "2013/08/21", Value = "ViewContainer control definition name")]
    public string ViewContainerDefinitionNameDescription => this[nameof (ViewContainerDefinitionNameDescription)];

    /// <summary>Commentable types</summary>
    [ResourceEntry("CommentableTypesCaption", Description = "Commentable types", LastModified = "2013/09/12", Value = "Commentable types")]
    public string CommentableTypesCaption => this[nameof (CommentableTypesCaption)];

    /// <summary>
    /// Collection that specifies which item types can be commented and what are the default options on the threads.
    /// </summary>
    [ResourceEntry("CommentableTypesDescription", Description = "Collection that specifies which item types can be commented and what are the default options on the threads.", LastModified = "2013/09/12", Value = "Collection that specifies which item types can be commented and what are the default options on the threads.")]
    public string CommentableTypesDescription => this[nameof (CommentableTypesDescription)];

    /// <summary>Friendly name</summary>
    [ResourceEntry("CommentableTypeFriendlyNameCaption", Description = "Friendly name", LastModified = "2013/09/12", Value = "Friendly name")]
    public string CommentableTypeFriendlyNameCaption => this[nameof (CommentableTypeFriendlyNameCaption)];

    /// <summary>
    /// Friendly name of the commentable type that will be used in the UI.
    /// </summary>
    [ResourceEntry("CommentableTypeFriendlyNameDescription", Description = "Friendly name of the commentable type that will be used in the UI.", LastModified = "2013/09/12", Value = "Friendly name of the commentable type that will be used in the UI.")]
    public string CommentableTypeFriendlyNameDescription => this[nameof (CommentableTypeFriendlyNameDescription)];

    /// <summary>Item type</summary>
    [ResourceEntry("CommentableTypeItemTypeCaption", Description = "Item type", LastModified = "2013/09/12", Value = "Item type")]
    public string CommentableTypeItemTypeCaption => this[nameof (CommentableTypeItemTypeCaption)];

    /// <summary>Item type that will be commentable.</summary>
    [ResourceEntry("CommentableTypeItemTypeDescription", Description = "Item type that will be commentable.", LastModified = "2013/09/12", Value = "Item type that will be commentable.")]
    public string CommentableTypeItemTypeDescription => this[nameof (CommentableTypeItemTypeDescription)];

    /// <summary>Requires authentication</summary>
    [ResourceEntry("CommentableTypeRequiresAuthenticationCaption", Description = "Requires authentication", LastModified = "2013/09/12", Value = "Requires authentication")]
    public string CommentableTypeRequiresAuthenticationCaption => this[nameof (CommentableTypeRequiresAuthenticationCaption)];

    /// <summary>
    /// If checked only registered users will be able to comment by default.
    /// </summary>
    [ResourceEntry("CommentableTypeRequiresAuthenticationDescription", Description = "If checked only registered users will be able to comment by default.", LastModified = "2013/09/12", Value = "If checked only registered users will be able to comment by default.")]
    public string CommentableTypeRequiresAuthenticationDescription => this[nameof (CommentableTypeRequiresAuthenticationDescription)];

    /// <summary>Requires approval</summary>
    [ResourceEntry("CommentableTypeRequiresApprovalCaption", Description = "Requires approval", LastModified = "2013/09/12", Value = "Requires approval")]
    public string CommentableTypeRequiresApprovalCaption => this[nameof (CommentableTypeRequiresApprovalCaption)];

    /// <summary>
    /// If checked comments have to be approved before they are published.
    /// </summary>
    [ResourceEntry("CommentableTypeRequiresApprovalDescription", Description = "If checked comments have to be approved before they are published.", LastModified = "2013/09/12", Value = "If checked comments have to be approved before they are published.")]
    public string CommentableTypeRequiresApprovalDescription => this[nameof (CommentableTypeRequiresApprovalDescription)];

    /// <summary>Enable paging</summary>
    [ResourceEntry("CommentsEnablePagingCaption", Description = "Enable paging", LastModified = "2013/09/12", Value = "Enable paging")]
    public string CommentsEnablePagingCaption => this[nameof (CommentsEnablePagingCaption)];

    /// <summary>Sets whether comments will be displayed on pages.</summary>
    [ResourceEntry("CommentsEnablePagingDescription", Description = "Sets whether comments will be displayed on pages.", LastModified = "2013/09/12", Value = "Sets whether comments will be displayed on pages.")]
    public string CommentsEnablePagingDescription => this[nameof (CommentsEnablePagingDescription)];

    /// <summary>Comments per page</summary>
    [ResourceEntry("CommentsPerPageCaption", Description = "Comments per page", LastModified = "2013/09/12", Value = "Comments per page")]
    public string CommentsPerPageCaption => this[nameof (CommentsPerPageCaption)];

    /// <summary>Sets how many comments will be displayed on a page.</summary>
    [ResourceEntry("CommentsPerPageDescription", Description = "Sets how many comments will be displayed on a page.", LastModified = "2013/09/12", Value = "Sets how many comments will be displayed on a page.")]
    public string CommentsPerPageDescription => this[nameof (CommentsPerPageDescription)];

    /// <summary>Are newest on top</summary>
    [ResourceEntry("CommentsAreNewestOnTopCaption", Description = "Are newest on top", LastModified = "2013/09/12", Value = "Are newest on top")]
    public string CommentsAreNewestOnTopCaption => this[nameof (CommentsAreNewestOnTopCaption)];

    /// <summary>
    /// Sets whether the newest comments will be displayed on top. Otherwise the oldest will be on top.
    /// </summary>
    [ResourceEntry("CommentsAreNewestOnTopDescription", Description = "Sets whether the newest comments will be displayed on top. Otherwise the oldest will be on top.", LastModified = "2013/09/12", Value = "Sets whether the newest comments will be displayed on top. Otherwise the oldest will be on top.")]
    public string CommentsAreNewestOnTopDescription => this[nameof (CommentsAreNewestOnTopDescription)];

    /// <summary>Default settings</summary>
    [ResourceEntry("CommentsDefaultSettingsCaption", Description = "Default settings", LastModified = "2013/09/13", Value = "Default settings")]
    public string CommentsDefaultSettingsCaption => this[nameof (CommentsDefaultSettingsCaption)];

    /// <summary>
    /// Default settings for comment threads. Used when the comments are not on any of the defined commentable types.
    /// </summary>
    [ResourceEntry("CommentsDefaultSettingsDescription", Description = "Default settings for comment threads. Used when the comments are not on any of the defined commentable types.", LastModified = "2013/09/13", Value = "Default settings for comment threads. Used when the comments are not on any of the defined commentable types.")]
    public string CommentsDefaultSettingsDescription => this[nameof (CommentsDefaultSettingsDescription)];

    /// <summary>Notifications</summary>
    [ResourceEntry("CommentsNotificationsCaption", Description = "Notifications", LastModified = "2013/10/22", Value = "Notifications")]
    public string CommentsNotificationsCaption => this[nameof (CommentsNotificationsCaption)];

    /// <summary>Notifications for actions on threads/comments.</summary>
    [ResourceEntry("CommentsNotificationsDescription", Description = "Notifications for actions on threads or comments.", LastModified = "2013/10/22", Value = "Notifications for actions on threads/comments.")]
    public string CommentsNotificationsDescription => this[nameof (CommentsNotificationsDescription)];

    /// <summary>Profile</summary>
    [ResourceEntry("CommentsProfileCaption", Description = "Profile", LastModified = "2013/10/22", Value = "Profile")]
    public string CommentsProfilesCaption => this[nameof (CommentsProfilesCaption)];

    /// <summary>
    /// This is the SMTP profile used for sending comments notifications.
    /// </summary>
    [ResourceEntry("CommentsProfileDescription", Description = "This is the SMTP profile used for sending comments notifications.", LastModified = "2013/10/22", Value = "This is the SMTP profile used for sending comments notifications.")]
    public string CommentsProfileDescription => this[nameof (CommentsProfileDescription)];

    /// <summary>Enable email subscription</summary>
    [ResourceEntry("CommentsAllowSubscriptionCaption", Description = "Enable email subscription", LastModified = "2013/10/22", Value = "Enable email subscription")]
    public string CommentsAllowSubscriptionCaption => this[nameof (CommentsAllowSubscriptionCaption)];

    /// <summary>
    /// When enabled, it allows users to subscribe for email notifications.
    /// </summary>
    [ResourceEntry("CommentsAllowSubscriptionDescription", Description = "When enabled, it allows users to subscribe for email notifications.", LastModified = "2013/10/22", Value = "When enabled, it allows users to subscribe for email notifications.")]
    public string CommentsAllowSubscriptionDescription => this[nameof (CommentsAllowSubscriptionDescription)];

    /// <summary>Delete associated item comments</summary>
    [ResourceEntry("DeleteAssociatedItemCommentsCaption", Description = "Delete associated item comments", LastModified = "2013/09/16", Value = "Delete associated item comments")]
    public string DeleteAssociatedItemCommentsCaption => this[nameof (DeleteAssociatedItemCommentsCaption)];

    /// <summary>
    /// Sets whether the module should delete comments on deleted items.
    /// </summary>
    [ResourceEntry("DeleteAssociatedItemCommentsDescription", Description = "Sets whether the module should delete comments on deleted items.", LastModified = "2013/09/16", Value = "Sets whether the module should delete comments on deleted items.")]
    public string DeleteAssociatedItemCommentsDescription => this[nameof (DeleteAssociatedItemCommentsDescription)];

    /// <summary>Always use UTC</summary>
    [ResourceEntry("AlwaysUseUTCCaption", Description = "Always use UTC", LastModified = "2019/10/21", Value = "Always use UTC")]
    public string AlwaysUseUTCCaption => this[nameof (AlwaysUseUTCCaption)];

    /// <summary>
    /// Sets whether the web service works only with UTC dates
    /// </summary>
    [ResourceEntry("AlwaysUseUTCDescription", Description = "Sets whether the web service works only with UTC dates.", LastModified = "2019/10/21", Value = "Sets whether the web service works only with UTC dates.")]
    public string AlwaysUseUTCDescription => this[nameof (AlwaysUseUTCDescription)];

    /// <summary>Phrase: Compress frontend script resources</summary>
    /// <value>Compress frontend script resources</value>
    [ResourceEntry("CompressScriptResourcesFrontEndCaption", Description = "Phrase: Compress frontend script resources", LastModified = "2013/09/10", Value = "Compress frontend script resources")]
    public string CompressScriptResourcesFrontEndCaption => this[nameof (CompressScriptResourcesFrontEndCaption)];

    /// <summary>
    /// A value describing how the script compression setting is used
    /// </summary>
    /// <value>Defines whether script resources loaded through a script manager will be compress and the requests that are used for them will use gzip encoding (Setting this to true will reduce the size of scripts, but will increase processing time when they are loaded for the first time)</value>
    [ResourceEntry("CompressScriptResourcesFrontEndDescription", Description = "A value describing how the script compression setting is used", LastModified = "2013/09/10", Value = "Defines whether script resources loaded through a script manager will be compress and the requests that are used for them will use gzip encoding (Enabling this will reduce the size of scripts, but will increase processing time when they are loaded for the first time).\r\n                Possible choices: Disabled does not compress scripts (the default), Forced always compresses scripts, Automatic only compresses scripts if IIS compression is disabled")]
    public string CompressScriptResourcesFrontEndDescription => this[nameof (CompressScriptResourcesFrontEndDescription)];

    /// <summary>
    /// The title of the SystemConfig -&gt; SiteUrlSettings -&gt; GenerateAbsoluteUrls property.
    /// </summary>
    /// <value>URLRulesClient</value>
    [ResourceEntry("GenerateAbsoluteUrlsTitle", Description = "The title of the SystemConfig -> SiteUrlSettings -> GenerateAbsoluteUrls property.", LastModified = "2013/11/26", Value = "Generate Absolute URLs")]
    public string GenerateAbsoluteUrlsTitle => this[nameof (GenerateAbsoluteUrlsTitle)];

    /// <summary>
    /// The description of the SystemConfig -&gt; SiteUrlSettings -&gt; GenerateAbsoluteUrls property.
    /// </summary>
    /// <value>URLRulesClient</value>
    [ResourceEntry("GenerateAbsoluteUrlsDescription", Description = "The description of the SystemConfig -> SiteUrlSettings -> GenerateAbsoluteUrls property.", LastModified = "2013/11/26", Value = "If true - always generate absolute URLs in format 'protocol://domain/path' instead of '/path'")]
    public string GenerateAbsoluteUrlsDescription => this[nameof (GenerateAbsoluteUrlsDescription)];

    /// <summary>
    /// Rules for generating URL from Title via the interface (using Content Backend).
    /// </summary>
    /// <value>URLRulesClient</value>
    [ResourceEntry("ClientUrlTransformationsTitle", Description = "Rules for generating URL from Title via the interface (using Content Backend).", LastModified = "2013/11/25", Value = "URLRulesClient")]
    public string ClientUrlTransformationsTitle => this[nameof (ClientUrlTransformationsTitle)];

    /// <summary>
    /// Rules for generating URL from Title via API (using Fluent API).
    /// </summary>
    /// <value>URLRulesServer</value>
    [ResourceEntry("ServerUrlTransformationsTitle", Description = "Rules for generating URL from Title via API (using Fluent API).", LastModified = "2013/11/25", Value = "URLRulesServer")]
    public string ServerUrlTransformationsTitle => this[nameof (ServerUrlTransformationsTitle)];

    [ResourceEntry("ClientUrlTransformationsDescription", Description = "", LastModified = "2013/11/25", Value = "Rules for generating URL from Title via the interface (using Content Backend).")]
    public string ClientUrlTransformationsDescription => this[nameof (ClientUrlTransformationsDescription)];

    /// <summary>The description of this element.</summary>
    /// <value>Rules for generating URL from Title via API (using Fluent API)</value>
    [ResourceEntry("ServerUrlTransformationsDescription", Description = "The description of this element.", LastModified = "2013/11/25", Value = "Rules for generating URL from Title via API (using Fluent API)")]
    public string ServerUrlTransformationsDescription => this[nameof (ServerUrlTransformationsDescription)];

    /// <summary>
    /// phrase: Skip Site Sync structure validation (in data-only sync mode)
    /// </summary>
    [ResourceEntry("SkipSiteSyncStructureValidationCaption", Description = "phrase: Skip Site Sync structure validation (in data-only sync mode)", LastModified = "2013/11/15", Value = "Skip Site Sync structure validation (in data-only sync mode)")]
    public string SkipSiteSyncStructureValidationCaption => this[nameof (SkipSiteSyncStructureValidationCaption)];

    /// <summary>
    /// phrase: Takes effect only during data-only sync. If true, meta type hash equality check is not performed.
    /// </summary>
    [ResourceEntry("SkipSiteSyncStructureValidationDescription", Description = "phrase: Takes effect only during data-only sync. If true, meta type hash equality check is not performed.", LastModified = "2013/11/15", Value = "Takes effect only during data-only sync. If true, meta type hash equality check is not performed.")]
    public string SkipSiteSyncStructureValidationDescription => this[nameof (SkipSiteSyncStructureValidationDescription)];

    /// <summary>phrase: Allow the addition of file extensions</summary>
    /// <value>Allow the addition of file extensions</value>
    [ResourceEntry("AllowExtensionsTitle", Description = "phrase: Allow the addition of file extensions", LastModified = "2013/11/14", Value = "Allow the addition of file extensions")]
    public string AllowExtensionsTitle => this[nameof (AllowExtensionsTitle)];

    /// <summary>
    /// phrase: Indicates whether the addition of file extensions for attachments is possible when creating a new forum
    /// </summary>
    /// <value>Indicates whether the addition of file extensions for attachments is possible when creating a new forum</value>
    [ResourceEntry("AllowExtensionsDescription", Description = "phrase: Indicates whether the addition of file extensions for attachments is possible when creating a new forum", LastModified = "2013/11/14", Value = "Indicates whether the addition of file extensions for attachments is possible when creating a new forum")]
    public string AllowExtensionsDescription => this[nameof (AllowExtensionsDescription)];

    /// <summary>Title of the IncludeDescendantItems property.</summary>
    [ResourceEntry("IncludeDescendantItemsCaption", Description = "Title of the IncludeDescendantItems property.", LastModified = "2013/11/29", Value = "Include descendant items")]
    public string IncludeDescendantItemsCaption => this[nameof (IncludeDescendantItemsCaption)];

    /// <summary>Description of the 'IncludeDescendantItems' property.</summary>
    [ResourceEntry("IncludeDescendantItemsDescription", Description = "Description of the 'IncludeDescendantItems' property.", LastModified = "2013/11/29", Value = "Value indicating whether to include all descendant items of the parent id, instead of just the children.")]
    public string IncludeDescendantItemsDescription => this[nameof (IncludeDescendantItemsDescription)];

    /// <summary>The display title of this configuration element</summary>
    /// <value>Enable user ratings as part of the comments</value>
    [ResourceEntry("EnableRatingsCaption", Description = "Describes this configuration element", LastModified = "2019/06/21", Value = "Enable user ratings as part of the comments")]
    public string EnableRatingsCaption => this[nameof (EnableRatingsCaption)];

    /// <summary>The display title of this configuration element</summary>
    /// <value>Enable user ratings as part of the comments</value>
    [ResourceEntry("EnableThreadCreationByConvension", Description = "The display title of this configuration element", LastModified = "2013/12/19", Value = "Enable user to create Thread only by convension")]
    public string EnableThreadCreationByConvension => this[nameof (EnableThreadCreationByConvension)];

    /// <summary>Describes this configuration element.</summary>
    /// <value>Defines whether user ratings are allowed as part of the comments. This configuration is used only if comments are allowed. Note: All existing comments without rating will be hidden.</value>
    [ResourceEntry("EnableRatingsDescription", Description = "Describes this configuration element.", LastModified = "2013/12/19", Value = "Defines whether user ratings are allowed as part of the comments. This configuration is used only if comments are allowed. Note: All existing comments without rating will be hidden.")]
    public string EnableRatingsDescription => this[nameof (EnableRatingsDescription)];

    [ResourceEntry("AllowLazyThumbnailGeneraitonDescription", Description = "", LastModified = "2014/01/08", Value = "Allows to use a size parameter in the image url for dynamic resizing of images")]
    public string AllowLazyThumbnailGeneraitonDescription => this[nameof (AllowLazyThumbnailGeneraitonDescription)];

    [ResourceEntry("EnableImageUrlSignatureDescription", Description = "", LastModified = "2014/01/29", Value = "Adds an unique signature parameter to each image URL. This is used to prevent DoS attacks. If you unable this option existing thumbnail URLs without signature will no longer be resolved")]
    public string EnableImageUrlSignatureDescription => this[nameof (EnableImageUrlSignatureDescription)];

    /// <summary>Visible in widgets</summary>
    /// <value>Visible in widgets</value>
    [ResourceEntry("TagsName", Description = "Tags", LastModified = "2014/01/09", Value = "Tags")]
    public string TagsName => this[nameof (TagsName)];

    [ResourceEntry("TagsDescription", Description = "", LastModified = "2014/01/09", Value = "This profile will be available for selection only in the widgets containing this tag or no tag at all. When left empty it will be shown on all widgets.")]
    public string TagsDescription => this[nameof (TagsDescription)];

    [ResourceEntry("DynamicResizingThreadsCountTitle", Description = "", LastModified = "2014/01/29", Value = "Maximum dynamic generation of images simultaneously")]
    public string DynamicResizingThreadsCountTitle => this[nameof (DynamicResizingThreadsCountTitle)];

    [ResourceEntry("DynamicResizingThreadsCountDescription", Description = "", LastModified = "2014/01/29", Value = "Value of 0 means to use the number of processors available on the server. Changing this value requires restart.")]
    public string DynamicResizingThreadsCountDescription => this[nameof (DynamicResizingThreadsCountDescription)];

    [ResourceEntry("StoreDynamicResizedImagesAsThumbnailsTitle", Description = "", LastModified = "2014/01/29", Value = "Keep dynamically generated images in storage")]
    public string StoreDynamicResizedImagesAsThumbnailsTitle => this[nameof (StoreDynamicResizedImagesAsThumbnailsTitle)];

    [ResourceEntry("StoreDynamicResizedImagesAsThumbnailsDescription", Description = "", LastModified = "2014/01/29", Value = "It saves CPU on each request. Highly recommended unless you use CDN")]
    public string StoreDynamicResizedImagesAsThumbnailsDescription => this[nameof (StoreDynamicResizedImagesAsThumbnailsDescription)];

    [ResourceEntry("CheckRelatingDataWarningTitle", Description = "", LastModified = "2014/03/18", Value = "Display a warning message to prevent users from deleting items that are related to another items")]
    public string CheckRelatingDataWarningTitle => this[nameof (CheckRelatingDataWarningTitle)];

    [ResourceEntry("CheckRelatingDataWarningDescription", Description = "", LastModified = "2014/03/18", Value = "When disabled a warning message will not be displayed")]
    public string CheckRelatingDataWarningDescription => this[nameof (CheckRelatingDataWarningDescription)];

    /// <summary>Audit Trail settings.</summary>
    [ResourceEntry("AuditTrailConfigCaption", Description = "The title of this class.", LastModified = "2014/04/25", Value = "Audit Trail settings")]
    public string AuditTrailConfigCaption => this[nameof (AuditTrailConfigCaption)];

    /// <summary>Resource strings for Audit Trail settings.</summary>
    [ResourceEntry("AuditTrailConfigDescription", Description = "The description of this class.", LastModified = "2014/04/25", Value = "Resource strings for Audit Trail settings.")]
    public string AuditTrailConfigDescription => this[nameof (AuditTrailConfigDescription)];

    /// <summary>Gets a caption for Recycle bin settings.</summary>
    [ResourceEntry("EnableRecycleBinCaption", Description = "The description of this class.", LastModified = "2014/05/19", Value = "Enable Recycle Bin")]
    public string EnableRecycleBinCaption => this[nameof (EnableRecycleBinCaption)];

    /// <summary>
    /// Gets the description of the Enable Recycle Bin configuration setting.
    /// </summary>
    [ResourceEntry("EnableRecycleBinDescription", Description = "The description of the Enable Recycle Bin configuration setting.", LastModified = "2014/06/06", Value = "Specifies whether the UI of all modules supporting Recycle Bin will use it when deleting instead of destroying the information.")]
    public string EnableRecycleBinDescription => this[nameof (EnableRecycleBinDescription)];

    /// <summary>Caption for Recycle bin period.</summary>
    [ResourceEntry("RetentionPeriodCaption", Description = "The title of the RetentionPeriod configuration property.", LastModified = "2014/05/19", Value = "Recycle Bin retention period")]
    public string RetentionPeriodCaption => this[nameof (RetentionPeriodCaption)];

    /// <summary>Description for Recycle bin period.</summary>
    [ResourceEntry("RetentionPeriodDescription", Description = "The description of the RetentionPeriod configuration property.", LastModified = "2014/05/19", Value = "Keep items in the Recycle Bin no longer than the specified value in days.")]
    public string RetentionPeriodDescription => this[nameof (RetentionPeriodDescription)];

    /// <summary>
    /// The title for the AggregationInterval configuration property for AuditTrail
    /// </summary>
    /// <value>Aggregation interval</value>
    [ResourceEntry("AggregationIntervalTitle", Description = "The title for the AggregationInterval configuration property for AuditTrail", LastModified = "2014/07/01", Value = "Aggregation interval")]
    public string AggregationIntervalTitle => this[nameof (AggregationIntervalTitle)];

    /// <summary>
    /// The description for the AggregationInterval configuration property for AuditTrail
    /// </summary>
    /// <value>This value (in seconds) describes in what interval the actions related to a single item should be aggregated into one audit record.</value>
    [ResourceEntry("AggregationIntervalDescription", Description = "The description for the AggregationInterval configuration property for AuditTrail", LastModified = "2014/07/01", Value = "This value (in seconds) describes in what interval the actions related to a single item should be aggregated into one audit record.")]
    public string AggregationIntervalDescription => this[nameof (AggregationIntervalDescription)];

    /// <summary>
    /// The title for the ElasticsearchUri configuration property for AuditTrail
    /// </summary>
    /// <value>Elasticsearch URI</value>
    [ResourceEntry("ElasticsearchUriTitle", Description = "The title for the ElasticsearchUri configuration property for AuditTrail", LastModified = "2014/07/02", Value = "Elasticsearch URI")]
    public string ElasticsearchUriTitle => this[nameof (ElasticsearchUriTitle)];

    /// <summary>
    /// The description for the ElasticsearchUri configuration property for AuditTrail
    /// </summary>
    /// <value>The address of the Elasticsearch server.</value>
    [ResourceEntry("ElasticsearchUriDescription", Description = "The description for the ElasticsearchUri configuration property for AuditTrail", LastModified = "2014/07/02", Value = "The address of the Elasticsearch server.")]
    public string ElasticsearchUriDescription => this[nameof (ElasticsearchUriDescription)];

    /// <summary>
    /// The title for the EnableIpAddressLogging configuration property for AuditTrail
    /// </summary>
    /// <value>Enable IP address logging</value>
    [ResourceEntry("EnableIpAddressLoggingTitle", Description = "The title for the EnableIpAddressLogging configuration property for AuditTrail", LastModified = "2014/07/02", Value = "Enable IP address logging")]
    public string EnableIpAddressLoggingTitle => this[nameof (EnableIpAddressLoggingTitle)];

    /// <summary>
    /// The description for the EnableIpAddressLogging configuration property for AuditTrail
    /// </summary>
    /// <value>Indicates whether the IP address logging is enabled.</value>
    [ResourceEntry("EnableIpAddressLoggingDescription", Description = "The description for the EnableIpAddressLogging configuration property for AuditTrail", LastModified = "2014/07/02", Value = "Indicates whether the IP address logging is enabled.")]
    public string EnableIpAddressLoggingDescription => this[nameof (EnableIpAddressLoggingDescription)];

    /// <summary>
    /// The title for the EnableJsonLogging configuration property for AuditTrail
    /// </summary>
    /// <value>Enable JSON logging</value>
    [ResourceEntry("EnableJsonLoggingTitle", Description = "The title for the EnableJsonLogging configuration property for AuditTrail", LastModified = "2014/07/02", Value = "Enable JSON logging")]
    public string EnableJsonLoggingTitle => this[nameof (EnableJsonLoggingTitle)];

    /// <summary>
    /// The description for the EnableJsonLogging configuration property for AuditTrail
    /// </summary>
    /// <value>Indicates whether the JSON logging is enabled.</value>
    [ResourceEntry("EnableJsonLoggingDescription", Description = "The description for the EnableJsonLogging configuration property for AuditTrail", LastModified = "2014/07/02", Value = "Indicates whether the JSON logging is enabled.")]
    public string EnableJsonLoggingDescription => this[nameof (EnableJsonLoggingDescription)];

    /// <summary>
    /// The title for the TextSize configuration property for Audit Trail
    /// </summary>
    /// <value>Set the number of words of the content that is being logged.</value>
    [ResourceEntry("TextSizeTitle", Description = "The title for the TextSize configuration property for Audit Trail", LastModified = "2014/07/25", Value = "Set the number of words of the content that is being logged")]
    public string TextSizeTitle => this[nameof (TextSizeTitle)];

    /// <summary>
    /// The description for the TextSize configuration property for Audit Trail
    /// </summary>
    /// <value>Indicates what is the number of words of the content being logged in Audit Trail.</value>
    [ResourceEntry("TextSizeDescription", Description = "The description for the TextSize configuration property for Audit Trail", LastModified = "2014/07/25", Value = "Indicates what is the number of words of the content being logged in Audit Trail.")]
    public string TextSizeDescription => this[nameof (TextSizeDescription)];

    /// <summary>Permissions filter cache</summary>
    [ResourceEntry("PermissionFilterConfigurationsTitle", Description = "The title of the configuration element.", LastModified = "2014/10/06", Value = "Permissions filter cache")]
    public string PermissionFilterConfigurationsTitle => this[nameof (PermissionFilterConfigurationsTitle)];

    /// <summary>
    /// Represents PermissionFilterCacheConfigurations property collection.
    /// </summary>
    [ResourceEntry("PermissionFilterConfigurationsDescription", Description = "Describes configuration element", LastModified = "2014/10/06", Value = "Represents PermissionFilterCacheConfigurations property collection.")]
    public string PermissionFilterConfigurationsDescription => this[nameof (PermissionFilterConfigurationsDescription)];

    /// <summary>The title for PermissionsCacheSlidingExpirationTime</summary>
    /// <value>Permissions cache sliding expiration time</value>
    [ResourceEntry("PermissionsCacheSlidingExpirationTimeTitle", Description = "The title for PermissionsCacheSlidingExpirationTime", LastModified = "2014/10/06", Value = "Permissions cache sliding expiration time")]
    public string PermissionsCacheSlidingExpirationTimeTitle => this[nameof (PermissionsCacheSlidingExpirationTimeTitle)];

    /// <summary>
    /// The description for PermissionsCacheSlidingExpirationTime
    /// </summary>
    [ResourceEntry("PermissionsCacheSlidingExpirationTimeDescription", Description = "The description for PermissionsCacheSlidingExpirationTime", LastModified = "2014/10/06", Value = "This value (in minutes) indicates the sliding expiration time for the permissions cache")]
    public string PermissionsCacheSlidingExpirationTimeDescription => this[nameof (PermissionsCacheSlidingExpirationTimeDescription)];

    /// <summary>TypePermissionsCacheMaxTotalCount</summary>
    [ResourceEntry("TypePermissionsCacheMaxTotalCountTitle", Description = "The title of a configuration element.", LastModified = "2014/10/06", Value = "Type permissions cache max total count")]
    public string TypePermissionsCacheMaxTotalCountTitle => this[nameof (TypePermissionsCacheMaxTotalCountTitle)];

    /// <summary>
    /// The number of permission item types that will be cached
    /// </summary>
    [ResourceEntry("TypePermissionsCacheMaxTotalCountDescription", Description = "Describes configuration element.", LastModified = "2014/10/06", Value = "The number of permission item types that will be cached.")]
    public string TypePermissionsCacheMaxTotalCountDescription => this[nameof (TypePermissionsCacheMaxTotalCountDescription)];

    /// <summary>ItemPermissionsCacheMaxTotalCount</summary>
    [ResourceEntry("ItemPermissionsCacheMaxTotalCountTitle", Description = "The title of a configuration element.", LastModified = "2014/10/06", Value = "Item permissions cache max total count")]
    public string ItemPermissionsCacheMaxTotalCountTitle => this[nameof (ItemPermissionsCacheMaxTotalCountTitle)];

    /// <summary>The number of permission items that will be cached</summary>
    [ResourceEntry("ItemPermissionsCacheMaxTotalCountCountDescription", Description = "Describes configuration element.", LastModified = "2014/10/06", Value = "The number of permission items that will be cached.")]
    public string ItemPermissionsCacheMaxTotalCountCountDescription => this[nameof (ItemPermissionsCacheMaxTotalCountCountDescription)];

    /// <summary>phrase: Permissions cache filter element</summary>
    [ResourceEntry("PermissionsFilterCacheElementTitle", Description = "phrase: Permissions cache filter element", LastModified = "2014/10/06", Value = "Permissions cache filter element")]
    public string PermissionsFilterCacheElementTitle => this[nameof (PermissionsFilterCacheElementTitle)];

    /// <summary>
    /// phrase: Configures the permission cache filtering settings.
    /// </summary>
    [ResourceEntry("PermissionsFilterCacheElementDescription", Description = "phrase: Configures the permission cache filtering settings.", LastModified = "2014/10/06", Value = "Configures the permission cache filtering settings.")]
    public string PermissionsFilterCacheElementDescription => this[nameof (PermissionsFilterCacheElementDescription)];

    /// <summary>Label: Search services</summary>
    /// <value>Search Services</value>
    [ResourceEntry("SearchServices", Description = "Label: Search services", LastModified = "2014/10/17", Value = "Search Services")]
    public string SearchServices => this[nameof (SearchServices)];

    /// <summary>
    /// Label: Defines a collection of search services that could be used for indexing.
    /// </summary>
    /// <value>Defines a collection of search services that could be used for indexing.</value>
    [ResourceEntry("SearchServicesDescription", Description = "Label: Defines a collection of search services that could be used for indexing.", LastModified = "2014/10/17", Value = "Defines a collection of search services that could be used for indexing.")]
    public string SearchServicesDescription => this[nameof (SearchServicesDescription)];

    /// <summary>phrase: Server session token lifetime</summary>
    /// <value>Server session token lifetime</value>
    [ResourceEntry("ServerSessionTokenLifetimeTitle", Description = "phrase: Server session token lifetime", LastModified = "2015/01/06", Value = "Server session token lifetime")]
    public string ServerSessionTokenLifetimeTitle => this[nameof (ServerSessionTokenLifetimeTitle)];

    /// <summary>
    /// phrase: Defines the server session token lifetime in minutes (valid in claims authentication mode only). Default is 1440 mins (24h).
    /// </summary>
    /// <value>Defines the server session token lifetime in minutes (valid in claims authentication mode only). Default is 1440 mins (24h).</value>
    [ResourceEntry("ServerSessionTokenLifetimeDescription", Description = "phrase: Defines the server session token lifetime in minutes (valid in claims authentication mode only). Default is 1440 mins (24h).", LastModified = "2015/01/06", Value = "Defines the server session token lifetime in minutes (valid in claims authentication mode only). Default is 1440 mins (24h).")]
    public string ServerSessionTokenLifetimeDescription => this[nameof (ServerSessionTokenLifetimeDescription)];

    /// <summary>
    /// Indicates whether to always redirect not authenticated requests to the frontend login page.
    /// </summary>
    [ResourceEntry("AuthenticateOnFrontendLoginPageTitle", Description = "phrase: AuthenticateOnFrontendLoginPage", LastModified = "2020/04/22", Value = "AuthenticateOnFrontendLoginPage")]
    public string AuthenticateOnFrontendLoginPageTitle => this[nameof (AuthenticateOnFrontendLoginPageTitle)];

    /// <summary>
    /// phrase: Indicates whether to redirect not authenticated requests to the frontend login page instead of Sitefinity backend page.
    /// </summary>
    /// <value>Indicates whether to redirect not authenticated requests to the frontend login page instead of Sitefinity backend page.</value>
    [ResourceEntry("AuthenticateOnFrontendLoginPageDescription", Description = "phrase: Indicates whether to redirect not authenticated requests to the frontend login page instead of Sitefinity backend page.", LastModified = "2015/09/23", Value = "Indicates whether to redirect not authenticated requests to the frontend login page instead of Sitefinity backend page.")]
    public string AuthenticateOnFrontendLoginPageDescription => this[nameof (AuthenticateOnFrontendLoginPageDescription)];

    /// <summary>phrase: Disable HTML sanitization.</summary>
    /// <value>Disable HTML sanitization.</value>
    [ResourceEntry("DisableHtmlSanitizationTitle", Description = "phrase: Disable HTML sanitization", LastModified = "2017/10/26", Value = "Disable HTML sanitization")]
    public string DisableHtmlSanitizationTitle => this[nameof (DisableHtmlSanitizationTitle)];

    /// <summary>
    /// phrase: Indicates whether the sanitizer for HTML fields should be disabled.
    /// </summary>
    /// <value>Indicates whether the sanitizer for HTML fields should be disabled.</value>
    [ResourceEntry("DisableHtmlSanitizationDescription", Description = "phrase: Indicates whether the sanitizer for HTML fields should be disabled.", LastModified = "2017/10/26", Value = "Indicates whether the sanitizer for HTML fields should be disabled.")]
    public string DisableHtmlSanitizationDescription => this[nameof (DisableHtmlSanitizationDescription)];

    /// <summary>phrase: Disable browser autocomplete</summary>
    /// <value>Disable browser autocomplete</value>
    [ResourceEntry("DisableBrowserAutocompleteTitle", Description = "phrase: Disable browser autocomplete", LastModified = "2015/01/07", Value = "Disable browser autocomplete")]
    public string DisableBrowserAutocompleteTitle => this[nameof (DisableBrowserAutocompleteTitle)];

    /// <summary>
    /// phrase: Indicates if the browser should be allowed to autofill the login fields
    /// </summary>
    /// <value>Indicates if the browser should be allowed to autofill the login fields</value>
    [ResourceEntry("DisableBrowserAutocompleteDescription", Description = "phrase: Indicates if the browser should be allowed to autofill the login fields", LastModified = "2015/01/07", Value = "Indicates if the browser should be allowed to autofill the login fields")]
    public string DisableBrowserAutocompleteDescription => this[nameof (DisableBrowserAutocompleteDescription)];

    /// <summary>phrase: Azure specific database options.</summary>
    /// <value>Azure specific database options.</value>
    [ResourceEntry("AzureOptionsTitle", Description = "word: Azure", LastModified = "2015/01/19", Value = "Azure")]
    public string AzureOptionsTitle => this[nameof (AzureOptionsTitle)];

    /// <summary>
    /// phrase: Provides specific database mapping options for Azure DB's.
    /// </summary>
    /// <value>Provides specific database mapping options for Azure DB's.</value>
    [ResourceEntry("AzureOptionsDescription", Description = "phrase: Provides specific database mapping options for Azure DBs.", LastModified = "2015/01/19", Value = "Provides specific database mapping options for Azure DBs.")]
    public string AzureOptionsDescription => this[nameof (AzureOptionsDescription)];

    /// <summary>phrase: Use Sql Server identifier limitations</summary>
    /// <value>Use Sql Server identifier limitations</value>
    [ResourceEntry("UseMsSqlIdentifierLimitationsTitle", Description = "phrase: Use Sql Server identifier limitations", LastModified = "2015/01/19", Value = "Use Sql Server identifier limitations")]
    public string UseMsSqlIdentifierLimitationsTitle => this[nameof (UseMsSqlIdentifierLimitationsTitle)];

    /// <summary>
    /// phrase: Spcifies whether the limitations for the table, column name and other identifier generation are the same for Azure as they are for SQL Server.
    /// </summary>
    /// <value>Spcifies whether the limitations for the table, column name and other identifier generation are the same for Azure as they are for SQL Server.</value>
    [ResourceEntry("UseMsSqlIdentifierLimitationsDescription", Description = "phrase: Specifies whether the limitations for the table, column name and other identifier generation are the same for Azure as they are for SQL Server.", LastModified = "2015/01/19", Value = "Specifies whether the limitations for the table, column name and other identifier generation are the same for Azure as they are for SQL Server.")]
    public string UseMsSqlIdentifierLimitationsDescription => this[nameof (UseMsSqlIdentifierLimitationsDescription)];

    /// <summary>phrase: Disable permissions synchronization</summary>
    /// <value>Disable permissions synchronization</value>
    [ResourceEntry("DisablePermissionsSyncTitle", Description = "phrase: Disable permissions synchronization", LastModified = "2018/11/23", Value = "Disable permissions synchronization")]
    public string DisablePermissionsSyncTitle => this[nameof (DisablePermissionsSyncTitle)];

    /// <summary>
    /// phrase: Controls whether permissions will be skipped when promoting content items to the destination environment. By default, content items are promoted with their respective permissions.
    /// </summary>
    /// <value>Controls whether permissions will be skipped when promoting content items to the destination environment. By default, content items are promoted with their respective permissions.</value>
    [ResourceEntry("DisablePermissionsSyncDescription", Description = "phrase: Controls whether permissions will be skipped when promoting content items to the destination environment. By default, content items are promoted with their respective permissions.", LastModified = "2018/11/23", Value = "Controls whether permissions will be skipped when promoting content items to the destination environment. By default, content items are promoted with their respective permissions.")]
    public string DisablePermissionsSyncDescription => this[nameof (DisablePermissionsSyncDescription)];

    /// <summary>
    /// phrase: Defines the default page size for Hierarchical Taxonomies.
    /// </summary>
    [ResourceEntry("DefaultPageSize", Description = "phrase: Defines the default page size for Hierarchical Taxonomies.", LastModified = "2015/03/25", Value = "Defines the default page size for Hierarchical Taxonomies.")]
    public string DefaultPageSize => this[nameof (DefaultPageSize)];

    /// <summary>phrase: Collect bounces retry count</summary>
    /// <value>Collect bounces retry count</value>
    [ResourceEntry("BounceCollectionRetryCountTitle", Description = "phrase: Collect bounces retry count", LastModified = "2015/06/12", Value = "Collect bounces retry count")]
    public string BounceCollectionRetryCountTitle => this[nameof (BounceCollectionRetryCountTitle)];

    /// <summary>
    /// phrase: The amount of retries after an issue was sent to collect bounce messages from the POP3 account.
    /// </summary>
    /// <value>The amount of retries after an issue was sent to collect bounce messages from the POP3 account.</value>
    [ResourceEntry("BounceCollectionRetryCountDescription", Description = "phrase: The amount of retries after an issue was sent to collect bounce messages from the POP3 account.", LastModified = "2015/06/12", Value = "The amount of retries after an issue was sent to collect bounce messages from the POP3 account.")]
    public string BounceCollectionRetryCountDescription => this[nameof (BounceCollectionRetryCountDescription)];

    /// <summary>phrase: Bounced messages retry interval</summary>
    /// <value>Bounced messages retry interval</value>
    [ResourceEntry("BouncedMessagesRetryIntervalMinutesTitle", Description = "phrase: Bounced messages retry interval", LastModified = "2015/06/15", Value = "Bounced messages retry interval")]
    public string BouncedMessagesRetryIntervalMinutesTitle => this[nameof (BouncedMessagesRetryIntervalMinutesTitle)];

    /// <summary>
    /// phrase: The interval in minutes to retry the sending of a bounced message with bounce action RetryLater
    /// </summary>
    /// <value>The interval in minutes to retry the sending of a bounced message with bounce action RetryLater</value>
    [ResourceEntry("BouncedMessagesRetryIntervalMinutesDescription", Description = "phrase: The interval in minutes to retry the sending of a bounced message with bounce action RetryLater", LastModified = "2015/06/15", Value = "The interval in minutes to retry the sending of a bounced message with bounce action RetryLater")]
    public string BouncedMessagesRetryIntervalMinutesDescription => this[nameof (BouncedMessagesRetryIntervalMinutesDescription)];

    /// <summary>phrase: Bounced messages retry count</summary>
    /// <value>Bounced messages retry count</value>
    [ResourceEntry("BouncedMessagesRetryCountTitle", Description = "phrase: Bounced messages retry count", LastModified = "2015/06/15", Value = "Bounced messages retry count")]
    public string BouncedMessagesRetryCountTitle => this[nameof (BouncedMessagesRetryCountTitle)];

    /// <summary>
    /// phrase: The amount of retries per message with bounce action RetryLater.
    /// </summary>
    /// <value>The amount of retries per message with bounce action RetryLater.</value>
    [ResourceEntry("BouncedMessagesRetryCountDescription", Description = "phrase: The amount of retries per message with bounce action RetryLater.", LastModified = "2015/06/15", Value = "The amount of retries per message with bounce action RetryLater.")]
    public string BouncedMessagesRetryCountDescription => this[nameof (BouncedMessagesRetryCountDescription)];

    /// <summary>phrase: Disable Sitefinity email headers</summary>
    /// <value>Disable Sitefinity email headers</value>
    [ResourceEntry("DisableSitefinityEmailHeadersTitle", Description = "phrase: Disable Sitefinity email headers", LastModified = "2015/06/18", Value = "Disable Sitefinity email headers")]
    public string DisableSitefinityEmailHeadersTitle => this[nameof (DisableSitefinityEmailHeadersTitle)];

    /// <summary>
    /// phrase: A value, indicating if the Sitefinity email headers should be disabled
    /// </summary>
    /// <value>A value, indicating if the Sitefinity email headers should be disabled</value>
    [ResourceEntry("DisableSitefinityEmailHeadersDescription", Description = "phrase: A value, indicating if the Sitefinity email headers should be disabled", LastModified = "2015/06/18", Value = "A value, indicating if the Sitefinity email headers should be disabled")]
    public string DisableSitefinityEmailHeadersDescription => this[nameof (DisableSitefinityEmailHeadersDescription)];

    /// <summary>phrase: A value, indicating taxon synonyms separator.</summary>
    /// <value>A value, indicating taxon synonyms separator.</value>
    [ResourceEntry("TaxonSynonymsSeparator", Description = "phrase: A value, indicating taxon synonyms separator.", LastModified = "2020/03/27", Value = "A value, indicating taxon synonyms separator. The default value is comma.")]
    public string TaxonSynonymsSeparator => this[nameof (TaxonSynonymsSeparator)];

    /// <summary>
    /// phrase: The length of the message subject. The default value is 78 in order to meet this standard: http://www.faqs.org/rfcs/rfc2822.html. The maximum allowed length of the field is 255 symbols.
    /// </summary>
    /// <value>The length of the message subject. The default value is 78 in order to meet this standard: http://www.faqs.org/rfcs/rfc2822.html. The maximum allowed length of the field is 255 symbols.</value>
    [ResourceEntry("CampaignSubjectLengthDescription", Description = "phrase: The length of the message subject. The default value is 78 in order to meet this standard: http://www.faqs.org/rfcs/rfc2822.html. The maximum allowed length of the field is 255 symbols.", LastModified = "2015/11/23", Value = "The length of the message subject. The default value is 78 in order to meet this standard: http://www.faqs.org/rfcs/rfc2822.html. The maximum allowed length of the field is 255 symbols.")]
    public string CampaignSubjectLengthDescription => this[nameof (CampaignSubjectLengthDescription)];

    /// <summary>phrase: Campaign subject length</summary>
    /// <value>Campaign subject length</value>
    [ResourceEntry("CampaignSubjectLengthTitle", Description = "phrase: Campaign subject length", LastModified = "2015/11/23", Value = "Campaign subject length")]
    public string CampaignSubjectLengthTitle => this[nameof (CampaignSubjectLengthTitle)];

    /// <summary>phrase: Related secured object provider name</summary>
    /// <value>Related secured object provider name</value>
    [ResourceEntry("RelatedSecuredObjectProviderNameCaption", Description = "phrase: Related secured object provider name", LastModified = "2015/11/25", Value = "Related secured object provider name")]
    public string RelatedSecuredObjectProviderNameCaption => this[nameof (RelatedSecuredObjectProviderNameCaption)];

    /// <summary>Message: Item Configuration Element</summary>
    [ResourceEntry("ItemConfigElementTitle", Description = "Describes item configuration element.", LastModified = "2016/02/26", Value = "Item Configuration Element")]
    public string ItemConfigElementTitle => this[nameof (ItemConfigElementTitle)];

    /// <summary>Message: Holds item configuration element.</summary>
    [ResourceEntry("ItemConfigElementDescription", Description = "Describes item configuration element.", LastModified = "2016/02/26", Value = "Holds item configuration element.")]
    public string ItemConfigElementDescription => this[nameof (ItemConfigElementDescription)];

    /// <summary>Message: Defines migration configuration data.</summary>
    [ResourceEntry("MigrationConfig", Description = "Describes item configuration element.", LastModified = "2016/02/25", Value = "Defines migration configuration data.")]
    public string MigrationConfig => this[nameof (MigrationConfig)];

    /// <summary>Gets or sets configuration items.</summary>
    [ResourceEntry("ItemConfigElements", Description = "Describes item configuration element.", LastModified = "2016/02/26", Value = "Gets or sets configuration items.")]
    public string ItemConfigElements => this[nameof (ItemConfigElements)];

    /// <summary>Message: Data</summary>
    [ResourceEntry("ItemDataTitle", Description = "Describes item configuration element.", LastModified = "2016/02/26", Value = "Data")]
    public string ItemDataTitle => this[nameof (ItemDataTitle)];

    /// <summary>
    /// Message: Gets or sets the data of the configuration element.
    /// </summary>
    [ResourceEntry("ItemDataDescription", Description = "Describes item configuration element.", LastModified = "2016/02/26", Value = "Gets or sets the data of the configuration element.")]
    public string ItemDataDescription => this[nameof (ItemDataDescription)];

    /// <summary>Message: Version</summary>
    [ResourceEntry("VersionStringTitle", Description = "Describes item configuration element.", LastModified = "2016/02/26", Value = "Version")]
    public string VersionStringTitle => this[nameof (VersionStringTitle)];

    /// <summary>
    /// Message: Gets or sets the version of the configuration element.
    /// </summary>
    [ResourceEntry("VersionStringDescription", Description = "Describes item configuration element.", LastModified = "2016/02/26", Value = "Gets or sets the version of the configuration element.")]
    public string VersionStringDescription => this[nameof (VersionStringDescription)];

    /// <summary>Message: Defines restriction level.</summary>
    [ResourceEntry("RestrictionLevelCaption", Description = "Describes configuration element.", LastModified = "2016/01/27", Value = "Defines restriction level.")]
    public string RestrictionLevelCaption => this[nameof (RestrictionLevelCaption)];

    /// <summary>Message: Defines restriction level.</summary>
    [ResourceEntry("RestrictionLevelDescription", Description = "Describes configuration element.", LastModified = "2016/01/27", Value = "Defines restriction level.")]
    public string RestrictionLevelDescription => this[nameof (RestrictionLevelDescription)];

    /// <summary>phrase: Authentication protocol</summary>
    /// <value>Authentication protocol</value>
    [ResourceEntry("AuthenticationProtocolCaption", Description = "phrase: Authentication protocol", LastModified = "2016/10/24", Value = "Authentication protocol")]
    public string AuthenticationProtocolCaption => this[nameof (AuthenticationProtocolCaption)];

    /// <summary>
    /// phrase: The protocol used to authenticate in the system.
    /// </summary>
    /// <value>The protocol used to authenticate in the system.</value>
    [ResourceEntry("AuthenticationProtocolDescription", Description = "phrase: The protocol used to authenticate in the system.", LastModified = "2016/10/24", Value = "The protocol used to authenticate in the system.")]
    public string AuthenticationProtocolDescription => this[nameof (AuthenticationProtocolDescription)];

    /// <summary>phrase: STS Url</summary>
    /// <value>STS Url</value>
    [ResourceEntry("StsUrlCaption", Description = "phrase: STS Url", LastModified = "2016/10/13", Value = "STS Url")]
    public string StsUrlCaption => this[nameof (StsUrlCaption)];

    /// <summary>
    /// phrase: The url of the external STS that will be used for authentication e.g. 'https://mysts.com'.
    /// </summary>
    /// <value>The url of the external STS that will be used for authentication e.g. 'https://mysts.com'.</value>
    [ResourceEntry("StsUrlDescription", Description = "phrase: The url of the external STS that will be used for authentication e.g. 'https://mysts.com'.", LastModified = "2016/10/13", Value = "The url of the external STS that will be used for authentication e.g. 'https://mysts.com'.")]
    public string StsUrlDescription => this[nameof (StsUrlDescription)];

    /// <summary>phrase: Require Https</summary>
    /// <value>Require Https</value>
    [ResourceEntry("RequireHttpsCaption", Description = "phrase: Require Https", LastModified = "2017/01/04", Value = "Require Https")]
    public string RequireHttpsCaption => this[nameof (RequireHttpsCaption)];

    /// <summary>
    /// phrase: Gets or sets a value indicating whether https is required
    /// </summary>
    /// <value>Gets or sets a value indicating whether https is required</value>
    [ResourceEntry("RequireHttpsDescription", Description = "phrase: Gets or sets a value indicating whether https is required", LastModified = "2017/01/04", Value = "Gets or sets a value indicating whether https is required")]
    public string RequireHttpsDescription => this[nameof (RequireHttpsDescription)];

    /// <summary>phrase: Application client name</summary>
    /// <value>Application client name</value>
    [ResourceEntry("ApplicationClientNameCaption", Description = "phrase: Application client name", LastModified = "2016/10/14", Value = "Application client name")]
    public string ApplicationClientNameCaption => this[nameof (ApplicationClientNameCaption)];

    /// <summary>phrase: Client name</summary>
    /// <value>Client name</value>
    [ResourceEntry("ClientNameCaption", Description = "phrase: Client name", LastModified = "2016/10/14", Value = "Client name")]
    public string ClientNameCaption => this[nameof (ClientNameCaption)];

    /// <summary>phrase: The name of the client</summary>
    /// <value>The name of the client</value>
    [ResourceEntry("ClientNameDescription", Description = "phrase: The name of the client", LastModified = "2016/10/14", Value = "The name of the client")]
    public string ClientNameDescription => this[nameof (ClientNameDescription)];

    /// <summary>phrase: Determines if the client is enabled</summary>
    /// <value>Determines if the client is enabled</value>
    [ResourceEntry("ClientEnabledDescription", Description = "phrase: Determines if the client is enabled", LastModified = "2016/10/14", Value = "Determines if the client is enabled")]
    public string ClientEnabledDescription => this[nameof (ClientEnabledDescription)];

    /// <summary>phrase: Allow access to all scopes</summary>
    /// <value>Allow access to all scopes</value>
    [ResourceEntry("AllowAccessToAllScopesCaption", Description = "phrase: Allow access to all scopes", LastModified = "2016/10/14", Value = "Allow access to all scopes")]
    public string AllowAccessToAllScopesCaption => this[nameof (AllowAccessToAllScopesCaption)];

    /// <summary>
    /// phrase: Determines if all scopes will be allowed (e.g. Name, Email, Profile, etc.)
    /// </summary>
    /// <value>Determines if all scopes will be allowed (e.g. Name, Email, Profile, etc.)</value>
    [ResourceEntry("AllowAccessToAllScopesDescription", Description = "phrase: Determines if all scopes will be allowed (e.g. Name, Email, Profile, etc.)", LastModified = "2016/10/14", Value = "Determines if all scopes will be allowed (e.g. Name, Email, Profile, etc.)")]
    public string AllowAccessToAllScopesDescription => this[nameof (AllowAccessToAllScopesDescription)];

    /// <summary>phrase: Require consent</summary>
    /// <value>Require consent</value>
    [ResourceEntry("RequireConsentCaption", Description = "phrase: Require consent", LastModified = "2016/10/14", Value = "Require consent")]
    public string RequireConsentCaption => this[nameof (RequireConsentCaption)];

    /// <summary>
    /// phrase: Defines if the consent will be required (consent screen will be displayed to choose which scopes to allow)
    /// </summary>
    /// <value>Defines if the consent will be required (consent screen will be displayed to choose which scopes to allow)</value>
    [ResourceEntry("RequireConsentDescription", Description = "phrase: Defines if the consent will be required (consent screen will be displayed to choose which scopes to allow)", LastModified = "2016/10/14", Value = "Defines if the consent will be required (consent screen will be displayed to choose which scopes to allow)")]
    public string RequireConsentDescription => this[nameof (RequireConsentDescription)];

    /// <summary>phrase: Sliding refresh token lifetime</summary>
    /// <value>Sliding refresh token lifetime</value>
    [ResourceEntry("SlidingRefreshTokenLifetimeCaption", Description = "phrase: Sliding refresh token lifetime", LastModified = "2016/10/14", Value = "Sliding refresh token lifetime")]
    public string SlidingRefreshTokenLifetimeCaption => this[nameof (SlidingRefreshTokenLifetimeCaption)];

    /// <summary>
    /// phrase: Defines the sliding lifetime of the refresh token in seconds
    /// </summary>
    /// <value>Defines the sliding lifetime of the refresh token in seconds</value>
    [ResourceEntry("SlidingRefreshTokenLifetimeDescription", Description = "phrase: Defines the sliding lifetime of the refresh token in seconds", LastModified = "2016/10/14", Value = "Defines the sliding lifetime of the refresh token in seconds")]
    public string SlidingRefreshTokenLifetimeDescription => this[nameof (SlidingRefreshTokenLifetimeDescription)];

    /// <summary>phrase: Identity token lifetime</summary>
    /// <value>Identity token lifetime</value>
    [ResourceEntry("IdentityTokenLifetimeCaption", Description = "phrase: Identity token lifetime", LastModified = "2016/10/17", Value = "Identity token lifetime")]
    public string IdentityTokenLifetimeCaption => this[nameof (IdentityTokenLifetimeCaption)];

    /// <summary>
    /// phrase: Defines the identity token lifetime in seconds
    /// </summary>
    /// <value>Defines the identity token lifetime in seconds</value>
    [ResourceEntry("IdentityTokenLifetimeDescription", Description = "phrase: Defines the identity token lifetime in seconds", LastModified = "2016/10/17", Value = "Defines the identity token lifetime in seconds")]
    public string IdentityTokenLifetimeDescription => this[nameof (IdentityTokenLifetimeDescription)];

    /// <summary>phrase: Access token lifetime</summary>
    /// <value>Access token lifetime</value>
    [ResourceEntry("AccessTokenLifetimeCaption", Description = "phrase: Access token lifetime", LastModified = "2016/10/17", Value = "Access token lifetime")]
    public string AccessTokenLifetimeCaption => this[nameof (AccessTokenLifetimeCaption)];

    /// <summary>
    /// phrase: Defines the lifetime of the access token in seconds
    /// </summary>
    /// <value>Defines the lifetime of the access token in seconds</value>
    [ResourceEntry("AccessTokenLifetimeDescription", Description = "phrase: Defines the lifetime of the access token in seconds", LastModified = "2016/10/17", Value = "Defines the lifetime of the access token in seconds")]
    public string AccessTokenLifetimeDescription => this[nameof (AccessTokenLifetimeDescription)];

    /// <summary>phrase: Authorization code lifetime</summary>
    /// <value>Authorization code lifetime</value>
    [ResourceEntry("AuthorizationCodeLifetimeCaption", Description = "phrase: Authorization code lifetime", LastModified = "2016/10/17", Value = "Authorization code lifetime")]
    public string AuthorizationCodeLifetimeCaption => this[nameof (AuthorizationCodeLifetimeCaption)];

    /// <summary>
    /// phrase: Defines the lifetime of the authorization code in seconds
    /// </summary>
    /// <value>Defines the lifetime of the authorization code in seconds</value>
    [ResourceEntry("AuthorizationCodeLifetimeDescription", Description = "phrase: Defines the lifetime of the authorization code in seconds", LastModified = "2016/10/17", Value = "Defines the lifetime of the authorization code in seconds")]
    public string AuthorizationCodeLifetimeDescription => this[nameof (AuthorizationCodeLifetimeDescription)];

    /// <summary>phrase: Allowed cors origins</summary>
    /// <value>Allowed cors origins</value>
    [ResourceEntry("AllowedCorsOriginCaption", Description = "phrase: Allowed cors origins", LastModified = "2016/12/08", Value = "Allowed cors origins")]
    public string AllowedCorsOriginCaption => this[nameof (AllowedCorsOriginCaption)];

    /// <summary>phrase: Defines the allowed cors origins</summary>
    /// <value>Defines the allowed cors origins</value>
    [ResourceEntry("AllowedCorsOriginDescription", Description = "phrase: Defines the allowed cors origin", LastModified = "2016/12/08", Value = "Defines the allowed cors origin")]
    public string AllowedCorsOriginDescription => this[nameof (AllowedCorsOriginDescription)];

    /// <summary>phrase: Client flow</summary>
    /// <value>Client flow</value>
    [ResourceEntry("FlowCaption", Description = "phrase: Client flow", LastModified = "2016/12/08", Value = "Client flow")]
    public string FlowCaption => this[nameof (FlowCaption)];

    /// <summary>phrase: Defines the client flow</summary>
    /// <value>Defines the client flow</value>
    [ResourceEntry("FlowDescription", Description = "phrase: Defines the client flow", LastModified = "2016/12/08", Value = "Defines the client flow")]
    public string FlowDescription => this[nameof (FlowDescription)];

    /// <summary>phrase: Client id</summary>
    /// <value>Client id</value>
    [ResourceEntry("ClientIdCaption", Description = "phrase: Client Id", LastModified = "2016/12/08", Value = "Client Id")]
    public string ClientIdCaption => this[nameof (ClientIdCaption)];

    /// <summary>phrase: Defines the client id</summary>
    /// <value>Defines the client id</value>
    [ResourceEntry("ClientIdDescription", Description = "phrase: Defines the client id", LastModified = "2016/12/08", Value = "Defines the client id")]
    public string ClientIdDescription => this[nameof (ClientIdDescription)];

    /// <summary>phrase: Client secret</summary>
    /// <value>Client secret</value>
    [ResourceEntry("ClientSecretsCaption", Description = "phrase: Client secrets", LastModified = "2016/12/08", Value = "Client secrets")]
    public string ClientSecretsCaption => this[nameof (ClientSecretsCaption)];

    /// <summary>phrase: Defines the client secret</summary>
    /// <value>Defines the client secret</value>
    [ResourceEntry("ClientSecretsDescription", Description = "phrase: Defines the client secrets", LastModified = "2016/12/08", Value = "Defines the client secrets")]
    public string ClientSecretsDescription => this[nameof (ClientSecretsDescription)];

    /// <summary>
    /// phrase: The name of this application, when used as a client for an external STS
    /// </summary>
    /// <value>The name of this application, when used as a client for an external STS</value>
    [ResourceEntry("ApplicationClientNameDescription", Description = "phrase: The name of this application, when used as a client for an external STS", LastModified = "2016/10/17", Value = "The name of this application, when used as a client for an external STS")]
    public string ApplicationClientNameDescription => this[nameof (ApplicationClientNameDescription)];

    /// <summary>phrase: Sign-in message query string</summary>
    /// <value>Sign-in message query string</value>
    [ResourceEntry("SignInQueryStringCaption", Description = "phrase: Sign-in message query string", LastModified = "2016/10/27", Value = "Sign-in message query string")]
    public string SignInQueryStringCaption => this[nameof (SignInQueryStringCaption)];

    /// <summary>
    /// phrase: A query string that contains any additional parameters to be sent in sign-in requests
    /// </summary>
    /// <value>A query string that contains any additional parameters to be sent in sign-in requests</value>
    [ResourceEntry("SignInQueryStringDescription", Description = "phrase: A query string that contains any additional parameters to be sent in sign-in requests", LastModified = "2016/10/27", Value = "A query string that contains any additional parameters to be sent in sign-in requests")]
    public string SignInQueryStringDescription => this[nameof (SignInQueryStringDescription)];

    /// <summary>phrase: Sign-out message query string</summary>
    /// <value>Sign-out message query string</value>
    [ResourceEntry("SignOutQueryStringCaption", Description = "phrase: Sign-out message query string", LastModified = "2016/10/27", Value = "Sign-out message query string")]
    public string SignOutQueryStringCaption => this[nameof (SignOutQueryStringCaption)];

    /// <summary>
    /// phrase: A query string that contains any additional parameters to be sent in sign-out requests
    /// </summary>
    /// <value>A query string that contains any additional parameters to be sent in sign-out requests</value>
    [ResourceEntry("SignOutQueryStringDescription", Description = "phrase: A query string that contains any additional parameters to be sent in sign-out requests", LastModified = "2016/10/27", Value = "A query string that contains any additional parameters to be sent in sign-out requests")]
    public string SignOutQueryStringDescription => this[nameof (SignOutQueryStringDescription)];

    /// <summary>phrase: Reply address</summary>
    /// <value>Reply address</value>
    [ResourceEntry("ReplyAddressCaption", Description = "phrase: Reply address", LastModified = "2016/10/27", Value = "Reply address")]
    public string ReplyAddressCaption => this[nameof (ReplyAddressCaption)];

    /// <summary>
    /// phrase: A URL that identifies the address at which the relying party (RP) application would like to receive replies from the Security Token Service (STS)
    /// </summary>
    /// <value>A URL that identifies the address at which the relying party (RP) application would like to receive replies from the Security Token Service (STS)</value>
    [ResourceEntry("ReplyAddressDescription", Description = "phrase: A URL that identifies the address at which the relying party (RP) application would like to receive replies from the Security Token Service (STS)", LastModified = "2016/10/27", Value = "A URL that identifies the address at which the relying party (RP) application would like to receive replies from the Security Token Service (STS)")]
    public string ReplyAddressDescription => this[nameof (ReplyAddressDescription)];

    /// <summary>phrase: Audience URI mode</summary>
    /// <value>Audience URI mode</value>
    [ResourceEntry("AudienceUriModeCaption", Description = "phrase: Audience URI mode", LastModified = "2016/10/27", Value = "Audience URI mode")]
    public string AudienceUriModeCaption => this[nameof (AudienceUriModeCaption)];

    /// <summary>
    /// phrase: the Audience Uri mode for the security token handler
    /// </summary>
    /// <value>the Audience Uri mode for the security token handler</value>
    [ResourceEntry("AudienceUriModeDescription", Description = "phrase: the Audience Uri mode for the security token handler", LastModified = "2016/10/27", Value = "the Audience Uri mode for the security token handler")]
    public string AudienceUriModeDescription => this[nameof (AudienceUriModeDescription)];

    /// <summary>phrase: STS provider mappings</summary>
    /// <value>STS provider mappings</value>
    [ResourceEntry("StsProviderMappingsCaption", Description = "phrase: STS provider mappings", LastModified = "2016/11/29", Value = "STS provider mappings")]
    public string StsProviderMappingsCaption => this[nameof (StsProviderMappingsCaption)];

    /// <summary>
    /// phrase: The mappings between the STS and the local providers
    /// </summary>
    /// <value>The mappings between the STS and the local providers</value>
    [ResourceEntry("StsProviderMappingsDescription", Description = "phrase: The mappings between the STS and the local providers", LastModified = "2016/11/29", Value = "The mappings between the STS and the local providers")]
    public string StsProviderMappingsDescription => this[nameof (StsProviderMappingsDescription)];

    /// <summary>phrase: STS provider name</summary>
    /// <value>STS provider name</value>
    [ResourceEntry("StsProviderNameCaption", Description = "phrase: STS provider name", LastModified = "2016/11/29", Value = "STS provider name")]
    public string StsProviderNameCaption => this[nameof (StsProviderNameCaption)];

    /// <summary>phrase: The name of the STS provider</summary>
    /// <value>The name of the STS provider</value>
    [ResourceEntry("StsProviderNameDescription", Description = "phrase: The name of the STS provider", LastModified = "2016/11/29", Value = "The name of the STS provider")]
    public string StsProviderNameDescription => this[nameof (StsProviderNameDescription)];

    /// <summary>phrase: Local provider name</summary>
    /// <value>Local provider name</value>
    [ResourceEntry("LocalProviderNameCaption", Description = "phrase: Local provider name", LastModified = "2016/11/29", Value = "Local provider name")]
    public string LocalProviderNameCaption => this[nameof (LocalProviderNameCaption)];

    /// <summary>phrase: The name of the local provider</summary>
    /// <value>The name of the local provider</value>
    [ResourceEntry("LocalProviderNameDescription", Description = "phrase: The name of the local provider", LastModified = "2016/11/29", Value = "The name of the local provider")]
    public string LocalProviderNameDescription => this[nameof (LocalProviderNameDescription)];

    /// <summary>phrase: Claims to roles mappings</summary>
    /// <value>Claims to roles mappings</value>
    [ResourceEntry("ClaimsToRolesMappingsCaption", Description = "phrase: Claims to roles mappings", LastModified = "2016/12/02", Value = "Claims to roles mappings")]
    public string ClaimsToRolesMappingsCaption => this[nameof (ClaimsToRolesMappingsCaption)];

    /// <summary>label: Claim type</summary>
    /// <value>Claim type</value>
    [ResourceEntry("ClaimTypeCaption", Description = "label: Claim type", LastModified = "2016/12/02", Value = "Claim type")]
    public string ClaimTypeCaption => this[nameof (ClaimTypeCaption)];

    /// <summary>phrase: The type of the claim</summary>
    /// <value>The type of the claim</value>
    [ResourceEntry("ClaimTypeDescription", Description = "phrase: The type of the claim", LastModified = "2016/12/02", Value = "The type of the claim")]
    public string ClaimTypeDescription => this[nameof (ClaimTypeDescription)];

    /// <summary>label: Claim value</summary>
    /// <value>Claim value</value>
    [ResourceEntry("ClaimValueCaption", Description = "label: Claim value", LastModified = "2016/12/02", Value = "Claim value")]
    public string ClaimValueCaption => this[nameof (ClaimValueCaption)];

    /// <summary>phrase: The value of the claim</summary>
    /// <value>The value of the claim</value>
    [ResourceEntry("ClaimValueDescription", Description = "phrase: The value of the claim", LastModified = "2016/12/02", Value = "The value of the claim")]
    public string ClaimValueDescription => this[nameof (ClaimValueDescription)];

    /// <summary>label: Mapped roles</summary>
    /// <value>Mapped roles</value>
    [ResourceEntry("MappedRolesCaption", Description = "label: Mapped roles", LastModified = "2016/12/02", Value = "Mapped roles")]
    public string MappedRolesCaption => this[nameof (MappedRolesCaption)];

    /// <summary>
    /// phrase: Defines the mapped roles, separated with comma e.g. 'BackendUsers, Editors'
    /// </summary>
    /// <value>Defines the mapped roles, separated with comma e.g. 'BackendUsers, Editors'</value>
    [ResourceEntry("MappedRolesDescription", Description = "phrase: Defines the mapped roles, separated with comma e.g. 'BackendUsers, Editors'", LastModified = "2016/12/02", Value = "Defines the mapped roles, separated with comma e.g. 'BackendUsers, Editors'")]
    public string MappedRolesDescription => this[nameof (MappedRolesDescription)];

    /// <summary>
    /// phrase: Defines a collection of mappings between claim type and value to user roles
    /// </summary>
    /// <value>Defines a collection of mappings between claim type and value to user roles</value>
    [ResourceEntry("ClaimsToRolesMappingsDescription", Description = "phrase: Defines a collection of mappings between claim type and value to user roles", LastModified = "2016/12/02", Value = "Defines a collection of mappings between claim type and value to user roles")]
    public string ClaimsToRolesMappingsDescription => this[nameof (ClaimsToRolesMappingsDescription)];

    [ResourceEntry("StoreNameCaption", Description = "", LastModified = "2016/12/02", Value = "Certificate store name")]
    public string StoreNameCaption => this[nameof (StoreNameCaption)];

    [ResourceEntry("StoreNameDescription", Description = "", LastModified = "2016/12/02", Value = "Name of the store in which the certificate was installed.")]
    public string StoreNameDescription => this[nameof (StoreNameDescription)];

    [ResourceEntry("StoreLocationCaption", Description = "", LastModified = "2016/12/02", Value = "Store location")]
    public string StoreLocationCaption => this[nameof (StoreLocationCaption)];

    [ResourceEntry("StoreLocationDescription", Description = "", LastModified = "2016/12/02", Value = "Location of the security store")]
    public string StoreLocationDescription => this[nameof (StoreLocationDescription)];

    [ResourceEntry("SubjectNameCaption", Description = "", LastModified = "2016/12/02", Value = "SubjectName")]
    public string SubjectNameCaption => this[nameof (SubjectNameCaption)];

    [ResourceEntry("SubjectNameDescription", Description = "", LastModified = "2016/12/02", Value = "The name of the subject to which the certificate was issued")]
    public string SubjectNameDescription => this[nameof (SubjectNameDescription)];

    /// <summary>
    /// phrase: Authentication cookie from bearer token endpoint
    /// </summary>
    /// <value>Authentication cookie from bearer token endpoint</value>
    [ResourceEntry("AuthCookieFromBearerTokenEndpointCaption", Description = "phrase: Authentication cookie from bearer token endpoint", LastModified = "2016/12/07", Value = "Authentication cookie from bearer token endpoint")]
    public string AuthCookieFromBearerTokenEndpointCaption => this[nameof (AuthCookieFromBearerTokenEndpointCaption)];

    /// <summary>
    /// phrase: The endpoint that allows retrieving authentication cookie using bearer token. If the value is empty, no endpoint is exposed.
    /// </summary>
    /// <value>The endpoint that allows retrieving authentication cookie using bearer token. If the value is empty, no endpoint is exposed.</value>
    [ResourceEntry("AuthCookieFromBearerTokenEndpointDescription", Description = "phrase: The endpoint that allows retrieving authentication cookie using bearer token. If the value is empty, no endpoint is exposed.", LastModified = "2016/12/07", Value = "The endpoint that allows retrieving authentication cookie using bearer token. If the value is empty, no endpoint is exposed.")]
    public string AuthCookieFromBearerTokenEndpointDescription => this[nameof (AuthCookieFromBearerTokenEndpointDescription)];

    /// <summary>Phrase: Cache profiles</summary>
    /// <value>Cache profiles</value>
    [ResourceEntry("CacheProfiles", Description = "Phrase: Cache profiles", LastModified = "2017/01/18", Value = "Cache profiles")]
    public string CacheProfiles => this[nameof (CacheProfiles)];

    /// <summary>Phrase: None</summary>
    /// <value>None</value>
    [ResourceEntry("None", Description = "Phrase: None", LastModified = "2017/01/18", Value = "None")]
    public string None => this[nameof (None)];

    /// <summary>Phrase: Server</summary>
    /// <value>Server</value>
    [ResourceEntry("Server", Description = "Phrase: Server", LastModified = "2017/01/18", Value = "Server")]
    public string Server => this[nameof (Server)];

    /// <summary>Phrase: Browser, proxy server and local server</summary>
    /// <value>Browser, proxy server and local server</value>
    [ResourceEntry("Any", Description = "Phrase: Browser, proxy server and local server", LastModified = "2017/01/18", Value = "Browser, proxy server and local server")]
    public string Any => this[nameof (Any)];

    /// <summary>Phrase: Browser only</summary>
    /// <value>Browser only</value>
    [ResourceEntry("Client", Description = "Phrase: Browser only", LastModified = "2017/01/18", Value = "Browser only")]
    public string Client => this[nameof (Client)];

    /// <summary>Phrase: Browser and server</summary>
    /// <value>Browser and server</value>
    [ResourceEntry("ServerAndClient", Description = "Phrase: Browser and server", LastModified = "2017/01/18", Value = "Browser and server")]
    public string ServerAndClient => this[nameof (ServerAndClient)];

    /// <summary>Word: Default</summary>
    /// <value>Default</value>
    [ResourceEntry("Default", Description = "Word: Default", LastModified = "2017/01/18", Value = "Default")]
    public string Default => this[nameof (Default)];

    /// <summary>Formatted string: Default for {0}</summary>
    /// <value>Default for {0}</value>
    [ResourceEntry("DefaultFor", Description = "Formatted string: Default for {0}", LastModified = "2017/01/30", Value = "Default for {0}")]
    public string DefaultFor => this[nameof (DefaultFor)];

    /// <summary>Title of the "Server Max Age" cache configuration.</summary>
    /// <value>Server Max Age</value>
    [ResourceEntry("ServerMaxAgeTitle", Description = "Title of the \"Server Max Age\" cache configuration.", LastModified = "2017/01/19", Value = "Server Max Age")]
    public string ServerMaxAgeTitle => this[nameof (ServerMaxAgeTitle)];

    /// <summary>
    /// Description of the "Server Max Age" cache configuration.
    /// </summary>
    /// <value>Time in seconds that a cache entry will stay on the server.</value>
    [ResourceEntry("ServerMaxAgeDescription", Description = "Description of the \"Server Max Age\" cache configuration.", LastModified = "2017/02/15", Value = "Time that a cache entry will stay on the server.")]
    public string ServerMaxAgeDescription => this[nameof (ServerMaxAgeDescription)];

    /// <summary>Phrase: Cache profiles for pages</summary>
    /// <value>Cache profiles for pages</value>
    [ResourceEntry("CacheProfilesForPages", Description = "Phrase: Cache profiles for pages", LastModified = "2017/01/20", Value = "Cache profiles for pages")]
    public string CacheProfilesForPages => this[nameof (CacheProfilesForPages)];

    /// <summary>
    /// Phrase: Cache profiles for image, video and document libraries
    /// </summary>
    /// <value>Cache profiles for image, video and document libraries</value>
    [ResourceEntry("CacheProfilesForMedia", Description = "Phrase: Cache profiles for image, video and document libraries", LastModified = "2017/01/20", Value = "Cache profiles for image, video and document libraries")]
    public string CacheProfilesForMedia => this[nameof (CacheProfilesForMedia)];

    /// <summary>Phrase: Default profile</summary>
    /// <value>Default profile</value>
    [ResourceEntry("DefaultProfile", Description = "Phrase: Default profile", LastModified = "2017/01/20", Value = "Default profile")]
    public string DefaultProfile => this[nameof (DefaultProfile)];

    /// <summary>
    /// Error message: Cache profile with name \"{profile name}\" was not found.
    /// </summary>
    /// <value>Cache profile with name \"{0}\" was not found.</value>
    [ResourceEntry("CacheProfileNotFound", Description = "Error message: Cache profile with name \"{profile name}\" was not found.", LastModified = "2017/01/23", Value = "Cache profile with name \"{0}\" was not found.")]
    public string CacheProfileNotFound => this[nameof (CacheProfileNotFound)];

    /// <summary>
    /// Error message: Cannot delete cache profile with name \"{profile name}\" because it is set as a default cache profile.
    /// </summary>
    /// <value>Cannot delete cache profile with name \"{0}\" because it is set as a default cache profile.</value>
    [ResourceEntry("CacheProfileIsDefault", Description = "Error message: Cannot delete cache profile with name \"{profile name}\" because it is set as a default cache profile.", LastModified = "2017/01/23", Value = "Cannot delete cache profile with name \"{0}\" because it is set as a default cache profile.")]
    public string CacheProfileIsDefault => this[nameof (CacheProfileIsDefault)];

    /// <summary>Phrase: Create profile for pages</summary>
    /// <value>Create profile for pages</value>
    [ResourceEntry("CreateProfileForPages", Description = "Phrase: Create profile for pages", LastModified = "2017/01/23", Value = "Create profile for pages")]
    public string CreateProfileForPages => this[nameof (CreateProfileForPages)];

    /// <summary>
    /// Error message: Cache profile with name \"{profile name}\" already exists.
    /// </summary>
    /// <value>Cache profile with name \"{0}\" already exists.</value>
    [ResourceEntry("CacheProfileAlreadyExists", Description = "Error message: Cache profile with name \"{profile name}\" already exists.", LastModified = "2017/01/24", Value = "Cache profile with name \"{0}\" already exists.")]
    public string CacheProfileAlreadyExists => this[nameof (CacheProfileAlreadyExists)];

    /// <summary>
    /// Error message: Name of the cache profile is a required field.
    /// </summary>
    /// <value>Name of the cache profile is a required field.</value>
    [ResourceEntry("CacheProfileNameRequired", Description = "Error message: Name of the cache profile is a required field.", LastModified = "2017/01/24", Value = "Name of the cache profile is a required field.")]
    public string CacheProfileNameRequired => this[nameof (CacheProfileNameRequired)];

    /// <summary>Phrase: Create profile for libraries</summary>
    /// <value>Create profile for libraries</value>
    [ResourceEntry("CreateProfileForMedia", Description = "Phrase: Create profile for libraries", LastModified = "2017/01/24", Value = "Create profile for libraries")]
    public string CreateProfileForMedia => this[nameof (CreateProfileForMedia)];

    /// <summary>Phrase: Create cache profile for pages</summary>
    /// <value>Create cache profile for pages</value>
    [ResourceEntry("CreateCacheProfileForPages", Description = "Phrase: Create cache profile for pages", LastModified = "2017/01/25", Value = "Create cache profile for pages")]
    public string CreateCacheProfileForPages => this[nameof (CreateCacheProfileForPages)];

    /// <summary>Phrase: Edit cache profile for pages</summary>
    /// <value>Edit cache profile for pages</value>
    [ResourceEntry("EditCacheProfileForPages", Description = "Phrase: Edit cache profile for pages", LastModified = "2017/01/25", Value = "Edit cache profile for pages")]
    public string EditCacheProfileForPages => this[nameof (EditCacheProfileForPages)];

    /// <summary>Phrase: Cache location</summary>
    /// <value>Cache location</value>
    [ResourceEntry("CacheLocation", Description = "Phrase: Cache location", LastModified = "2017/01/25", Value = "Cache location")]
    public string CacheLocation => this[nameof (CacheLocation)];

    /// <summary>Phrase: HTTP header preview</summary>
    /// <value>HTTP header preview</value>
    [ResourceEntry("HttpHeaderPreview", Description = "Phrase: HTTP header preview", LastModified = "2017/01/25", Value = "HTTP header preview")]
    public string HttpHeaderPreview => this[nameof (HttpHeaderPreview)];

    /// <summary>Word: Pages</summary>
    /// <value>Pages</value>
    [ResourceEntry("Pages", Description = "Word: Pages", LastModified = "2017/01/25", Value = "Pages")]
    public string Pages => this[nameof (Pages)];

    /// <summary>Phrase: Set as default profile for...</summary>
    /// <value>Set as default profile for...</value>
    [ResourceEntry("SetAsDefaultProfileFor", Description = "Phrase: Set as default profile for...", LastModified = "2017/01/25", Value = "Set as default profile for...")]
    public string SetAsDefaultProfileFor => this[nameof (SetAsDefaultProfileFor)];

    /// <summary>Word: seconds</summary>
    /// <value>seconds</value>
    [ResourceEntry("Seconds", Description = "Word: seconds", LastModified = "2017/01/25", Value = "seconds")]
    public string Seconds => this[nameof (Seconds)];

    /// <summary>Phrase: Server max age</summary>
    /// <value>Server max age</value>
    [ResourceEntry("ServerMaxAge", Description = "Phrase: Server max age", LastModified = "2017/01/25", Value = "Server max age")]
    public string ServerMaxAge => this[nameof (ServerMaxAge)];

    /// <summary>Phrase: Browser max age</summary>
    /// <value>Browser max age</value>
    [ResourceEntry("BrowserMaxAge", Description = "Phrase: Browser max age", LastModified = "2017/01/26", Value = "Browser max age")]
    public string BrowserMaxAge => this[nameof (BrowserMaxAge)];

    /// <summary>Phrase: Browser max age description</summary>
    /// <value>Browser max age description</value>
    [ResourceEntry("BrowserMaxAgeDescription", Description = "Phrase: Browser max age description", LastModified = "2017/02/03", Value = "Specifies the maximum time in seconds that the fetched response is allowed to be reused from the time of the request. Corresponds to the 'max-age' directive of the Cache-Control header. If not specified the max age property will set the browser cache max age.")]
    public string BrowserMaxAgeDescription => this[nameof (BrowserMaxAgeDescription)];

    /// <summary>Phrase: Proxy or CDN max age</summary>
    /// <value>Proxy or CDN max age</value>
    [ResourceEntry("ProxyCdnMaxAge", Description = "Phrase: Proxy or CDN max age", LastModified = "2017/01/26", Value = "Proxy or CDN max age")]
    public string ProxyCdnMaxAge => this[nameof (ProxyCdnMaxAge)];

    /// <summary>Phrase: Proxy or CDN max age description</summary>
    /// <value>Proxy or CDN max age description</value>
    [ResourceEntry("ProxyCdnMaxAgeDescription", Description = "Phrase: Proxy or CDN max age description", LastModified = "2017/02/03", Value = "The setting corresponds to the 's-maxage' directive of the Cache-Control header. It overrides the max-age directive and expires header fields when present.")]
    public string ProxyCdnMaxAgeDescription => this[nameof (ProxyCdnMaxAgeDescription)];

    /// <summary>Phrase: Reset expiration time on every request</summary>
    /// <value>Reset expiration time on every request</value>
    [ResourceEntry("SlidingExpirationDescription", Description = "Phrase: Reset expiration time on every request", LastModified = "2017/01/26", Value = "Reset expiration time on every request")]
    public string SlidingExpirationDescription => this[nameof (SlidingExpirationDescription)];

    /// <summary>Violation message for missing Name field value.</summary>
    /// <value>Name is a required field</value>
    [ResourceEntry("NameIsRequiredViolationMessage", Description = "Violation message for missing Name field value.", LastModified = "2017/01/26", Value = "Name is a required field.")]
    public string NameIsRequiredViolationMessage => this[nameof (NameIsRequiredViolationMessage)];

    /// <summary>
    /// Violation message for out of range server max age value.
    /// </summary>
    /// <value>Server max age must be greater than or equal to 0.</value>
    [ResourceEntry("ServerMaxAgeRangeViolationMessage", Description = "Violation message for out of range server max age value.", LastModified = "2017/02/15", Value = "Server max age must be greater than or equal to 0.")]
    public string ServerMaxAgeRangeViolationMessage => this[nameof (ServerMaxAgeRangeViolationMessage)];

    /// <summary>
    /// Violation message for out of range browser max age value.
    /// </summary>
    /// <value>Browser max age must be greater than or equal to 0.</value>
    [ResourceEntry("BrowserMaxAgeRangeViolationMessage", Description = "Violation message for out of range browser max age value.", LastModified = "2017/02/15", Value = "Browser max age must be greater than or equal to 0.")]
    public string BrowserMaxAgeRangeViolationMessage => this[nameof (BrowserMaxAgeRangeViolationMessage)];

    /// <summary>
    /// Violation message for out of range proxy max age value.
    /// </summary>
    /// <value>Proxy max age must be greater than or equal to 0.</value>
    [ResourceEntry("ProxyCdnMaxAgeRangeViolationMessage", Description = "Violation message for out of range proxy max age value.", LastModified = "2017/02/15", Value = "Proxy max age must be greater than or equal to 0.")]
    public string ProxyCdnMaxAgeRangeViolationMessage => this[nameof (ProxyCdnMaxAgeRangeViolationMessage)];

    /// <summary>
    /// Violation message for out of range item max size value.
    /// </summary>
    /// <value>Item max size must be greater than or equal to 0.</value>
    [ResourceEntry("ItemMaxSizeRangeViolationMessage", Description = "Violation message for out of range item max size value.", LastModified = "2017/02/15", Value = "item max size must be greater than or equal to 0.")]
    public string ItemMaxSizeRangeViolationMessage => this[nameof (ItemMaxSizeRangeViolationMessage)];

    /// <summary>Phrase: Create cache profile for libraries</summary>
    /// <value>Create cache profile for libraries</value>
    [ResourceEntry("CreateCacheProfileForLibraries", Description = "Phrase: Create cache profile for libraries", LastModified = "2017/01/26", Value = "Create cache profile for libraries")]
    public string CreateCacheProfileForLibraries => this[nameof (CreateCacheProfileForLibraries)];

    /// <summary>Phrase: Edit cache profile for libraries</summary>
    /// <value>Edit cache profile for libraries</value>
    [ResourceEntry("EditCacheProfileForLibraries", Description = "Phrase: Edit cache profile for libraries", LastModified = "2017/01/26", Value = "Edit cache profile for libraries")]
    public string EditCacheProfileForLibraries => this[nameof (EditCacheProfileForLibraries)];

    /// <summary>Phrase: Item max size to be cached</summary>
    /// <value>Item max size to be cached</value>
    [ResourceEntry("ItemMaxSize", Description = "Phrase: Item max size to be cached", LastModified = "2017/01/26", Value = "Item max size to be cached")]
    public string ItemMaxSize => this[nameof (ItemMaxSize)];

    /// <summary>Phrase: Item max size to be cached description</summary>
    /// <value>Item max size to be cached description</value>
    [ResourceEntry("ItemMaxSizeDescription", Description = "Phrase: Item max size to be cached description", LastModified = "2017/02/03", Value = "Indicates max size in KB of item to be cached. The items that exceed that limit are not cached. This setting affects Media (images, documents, videos) caching but doesn't affect Page output caching! For media libraries using File system blob storage it is advisable to turn off library output caching.")]
    public string ItemMaxSizeDescription => this[nameof (ItemMaxSizeDescription)];

    /// <summary>Label: Required permission set</summary>
    /// <value>Required permission set</value>
    [ResourceEntry("RequiredPermissionSetCaption", Description = "Label: Required permission set", LastModified = "2017/07/27", Value = "Required permission set")]
    public string RequiredPermissionSetCaption => this[nameof (RequiredPermissionSetCaption)];

    /// <summary>
    /// Label: The required permission set for user to see specific widget
    /// </summary>
    /// <value>The required permission set for user to see specific widget</value>
    [ResourceEntry("RequiredPermissionSetDescription", Description = "Label: The required permission set for user to see specific widget", LastModified = "2017/07/27", Value = "The required permission set for user to see specific widget")]
    public string RequiredPermissionSetDescription => this[nameof (RequiredPermissionSetDescription)];

    /// <summary>Label: Required actions</summary>
    /// <value>Required actions </value>
    [ResourceEntry("RequiredActionsCaption", Description = "Label: Required actions ", LastModified = "2017/07/27", Value = "Required actions ")]
    public string RequiredActionsCaption => this[nameof (RequiredActionsCaption)];

    /// <summary>
    /// Label: Required actions needed for user to view specific widget
    /// </summary>
    /// <value>Required actions needed for user to view specific widget</value>
    [ResourceEntry("RequiredActions", Description = "Label: Required actions needed for user to view specific widget", LastModified = "2017/07/27", Value = "Required actions needed for user to view specific widget")]
    public string RequiredActions => this[nameof (RequiredActions)];

    /// <summary>
    /// Label: Required Actions needed for users to view specific widget
    /// </summary>
    /// <value>Required Actions needed for users to view specific widget</value>
    [ResourceEntry("RequiredActionsDescription", Description = "Label: Required Actions needed for users to view specific widget", LastModified = "2017/07/27", Value = "Required Actions needed for users to view specific widget")]
    public string RequiredActionsDescription => this[nameof (RequiredActionsDescription)];

    /// <summary>Label: Indicates if all requests should use https</summary>
    /// <value>Indicates if all requests should use https</value>
    [ResourceEntry("RequireHttpsForAllRequests", Description = "Label: Indicates if all requests should use https", LastModified = "2017/11/29", Value = "Indicates if all requests should use https")]
    public string RequireHttpsForAllRequests => this[nameof (RequireHttpsForAllRequests)];

    /// <summary>
    /// Label: This property is obsolete. Go to Manage sites &gt; Site properties to configure it.
    /// </summary>
    /// <value>This property is obsolete. Go to Manage sites &gt; Site properties to configure it.</value>
    [ResourceEntry("SiteSettingsObsoleteProperty", Description = "Label: This property is obsolete. Go to Manage sites > Site properties to configure it.", LastModified = "2020/08/17", Value = "This property is obsolete. Go to Manage sites > Site properties to configure it.")]
    public string SiteSettingsObsoleteProperty => this[nameof (SiteSettingsObsoleteProperty)];

    /// <summary>Gets the tracking consent config descriptions.</summary>
    /// <value>The tracking consent config descriptions.</value>
    [ResourceEntry("TrackingConsentConfigDescriptions", Description = "Tracking Consent Configuration for Sitefinity", LastModified = "2017/07/18", Value = "Tracking consent")]
    public string TrackingConsentConfigDescriptions => this[nameof (TrackingConsentConfigDescriptions)];

    /// <summary>
    /// Gets the tracking consent required config descriptions.
    /// </summary>
    [ResourceEntry("ConsentIsRequiredDescriptions", Description = "phrase: User should provide consent for his activities being tracked.", LastModified = "2010/07/18", Value = "User should provide consent for his activities being tracked.")]
    public string ConsentIsRequiredDescriptions => this[nameof (ConsentIsRequiredDescriptions)];

    /// <summary>Gets the description for tracking dialog field.</summary>
    [ResourceEntry("ConsentDialogDescriptions", Description = "phrase: Relative path to file containing consent dialog html.", LastModified = "2010/08/29", Value = "Relative path to file containing consent dialog html.")]
    public string ConsentDialogDescriptions => this[nameof (ConsentDialogDescriptions)];

    /// <summary>Gets the html of tracking consent dialog.</summary>
    [ResourceEntry("DomainOverridesDescriptions", Description = "phrase: Overrides for particular domains.", LastModified = "2017/08/29", Value = "Domain overrides")]
    public string DomainOverridesDescriptions => this[nameof (DomainOverridesDescriptions)];

    [ResourceEntry("EnableUsageTrackingTitle", Description = "", LastModified = "2019/05/30", Value = "Enable sending the usage tracking information from Sitefinity")]
    public string EnableUsageTrackingTitle => this[nameof (EnableUsageTrackingTitle)];

    [ResourceEntry("EnableUsageTrackingDescription", Description = "", LastModified = "2019/05/30", Value = "When disabled the usage tracking information from Sitefinity will not be send")]
    public string EnableUsageTrackingDescription => this[nameof (EnableUsageTrackingDescription)];

    /// <summary>Resource strings Usage Tracking</summary>
    [ResourceEntry("UsageTrackingConfigCaption", Description = "Usage Tracking configuration caption", LastModified = "2019/06/25", Value = "Usage Tracking")]
    public string UsageTrackingConfigCaption => this[nameof (UsageTrackingConfigCaption)];

    /// <summary>Resource strings for Usage tracking description.</summary>
    [ResourceEntry("UsageTrackingConfigDescription", Description = "Usage tracking configuration description.", LastModified = "2020/07/03", Value = "Defines configuration settings for Usage tracking")]
    public string UsageTrackingConfigDescription => this[nameof (UsageTrackingConfigDescription)];

    /// <summary>Gets AutoSyncCronSpec</summary>
    /// <value>AutoSync cron specification</value>
    [ResourceEntry("AutoSyncCronSpecTitle", Description = "phrase: AutoSyncCronSpec", LastModified = "2019/05/30", Value = "AutoSync cron specification")]
    public string AutoSyncCronSpecTitle => this[nameof (AutoSyncCronSpecTitle)];

    /// <summary>Gets cron spec</summary>
    /// <value>A configuration that specifies commands to run periodically on a given schedule. For example: 5 * * * * (run on the fifth minute of every hour)</value>
    [ResourceEntry("AutoSyncCronSpecDescription", Description = "phrase: cron spec", LastModified = "2019/05/30", Value = "A configuration that specifies commands to run periodically on a given schedule. For example: 5 * * * * (run on the fifth minute of every hour)")]
    public string AutoSyncCronSpecDescription => this[nameof (AutoSyncCronSpecDescription)];

    /// <summary>Resource strings - temp items cleanup</summary>
    [ResourceEntry("TempItemsCleanupConfigCaption", Description = "Temp items cleanup configuration caption", LastModified = "2020/07/03", Value = "Temp items cleanup")]
    public string TempItemsCleanupConfigCaption => this[nameof (TempItemsCleanupConfigCaption)];

    /// <summary>Resource strings for temp items cleanup.</summary>
    [ResourceEntry("TempItemsCleanupConfigDescription", Description = "Temp items cleanup configuration description.", LastModified = "2020/07/03", Value = "Defines configuration settings for temp items cleanup")]
    public string TempItemsCleanupConfigDescription => this[nameof (TempItemsCleanupConfigDescription)];

    [ResourceEntry("EnableTempItemsCleanupTitle", Description = "label: Enable temp items automatic cleanup", LastModified = "2020/07/03", Value = "Enable temp items automatic cleanup")]
    public string EnableTempItemsCleanupTitle => this[nameof (EnableTempItemsCleanupTitle)];

    [ResourceEntry("EnableTempItemsCleanupDescription", Description = "label: When enabled, Sitefinity will perform regular automatic cleanup for unused temp items", LastModified = "2020/07/03", Value = "When enabled, Sitefinity will perform regular automatic cleanup for unused temp items")]
    public string EnableTempItemsCleanupDescription => this[nameof (EnableTempItemsCleanupDescription)];

    /// <summary>Gets AutoCleanupCronSpecTitle</summary>
    /// <value>Automatic temp items cleanup cron specification</value>
    [ResourceEntry("AutoCleanupCronSpecTitle", Description = "phrase: AutoCleanupCronSpecTitle", LastModified = "2020/07/03", Value = "Automatic temp items cleanup cron specification")]
    public string AutoCleanupCronSpecTitle => this[nameof (AutoCleanupCronSpecTitle)];

    /// <summary>Gets cron spec</summary>
    /// <value>A configuration that specifies when to schedule automatic temp items cleanup.</value>
    [ResourceEntry("AutoCleanupCronSpecDescription", Description = "phrase: cron spec", LastModified = "2020/07/03", Value = "A configuration that specifies when to schedule automatic temp items cleanup.")]
    public string AutoCleanupCronSpecDescription => this[nameof (AutoCleanupCronSpecDescription)];

    /// <summary>Gets TempItemsMaxAgeTitle</summary>
    /// <value>Maximum temp item age</value>
    [ResourceEntry("TempItemsMaxAgeTitle", Description = "phrase: AutoCleanupCronSpecTitle", LastModified = "2020/07/03", Value = "Maximum temp item age")]
    public string TempItemsMaxAgeTitle => this[nameof (TempItemsMaxAgeTitle)];

    /// <summary>Gets TempItemsMaxAgeDescription</summary>
    /// <value>Specify for how many days unused temp items should be kept in database before being removed with automatic temp items cleanup.</value>
    [ResourceEntry("TempItemsMaxAgeDescription", Description = "phrase: TempItemsMaxAgeDescription", LastModified = "2020/07/24", Value = "Specify for how many days unused temp items should be kept in database before being removed with automatic temp items cleanup.")]
    public string TempItemsMaxAgeDescription => this[nameof (TempItemsMaxAgeDescription)];

    /// <summary>Gets the phrase: Enable output cache web service</summary>
    [ResourceEntry("CacheServiceEnabledCaption", Description = "Gets the phrase: Enable output cache web service", LastModified = "2019/07/04", Value = "Enable output cache web service")]
    public string CacheServiceEnabledCaption => this[nameof (CacheServiceEnabledCaption)];

    /// <summary>
    /// Gets the phrase: Provides a service endpoint to manipulate items stored in output cache.
    /// </summary>
    [ResourceEntry("CacheServiceEnabledDescription", Description = "Gets the phrase: Provides a service endpoint to manipulate items stored in output cache.", LastModified = "2019/07/04", Value = "Provides a service endpoint to manipulate items stored in output cache.")]
    public string CacheServiceEnabledDescription => this[nameof (CacheServiceEnabledDescription)];

    /// <summary>Gets the phrase: Output cache web service</summary>
    [ResourceEntry("CacheServiceSettingsCaption", Description = "Gets the phrase: Output cache web service", LastModified = "2019/07/04", Value = "Output cache web service")]
    public string CacheServiceSettingsCaption => this[nameof (CacheServiceSettingsCaption)];

    /// <summary>Gets the phrase: Authentication key.</summary>
    [ResourceEntry("CacheServiceAuthenticationKeyCaption", Description = "Gets the phrase: Authentication key", LastModified = "2019/07/04", Value = "Authentication key")]
    public string CacheServiceAuthenticationKeyCaption => this[nameof (CacheServiceAuthenticationKeyCaption)];

    /// <summary>
    /// Gets the phrase: You use the key in the header of the HTTP request.
    /// </summary>
    [ResourceEntry("CacheServiceAuthenticationKeyDescription", Description = "Gets the phrase: You use the key in the header of the HTTP request.", LastModified = "2019/07/04", Value = "You use the key in the header of the HTTP request.")]
    public string CacheServiceAuthenticationKeyDescription => this[nameof (CacheServiceAuthenticationKeyDescription)];

    /// <summary>Gets the phrase: Require Https for all requests</summary>
    [ResourceEntry("CacheServiceRequireHttpsTitle", Description = "Gets the phrase: Require Https for all requests", LastModified = "2019/07/04", Value = "Require Https for all requests")]
    public string CacheServiceRequireHttpsTitle => this[nameof (CacheServiceRequireHttpsTitle)];

    /// <summary>
    /// Gets the phrase: Indicates if all requests should use https.
    /// </summary>
    [ResourceEntry("CacheServiceRequireHttpsDecription", Description = "Gets the phrase: Indicates if all requests should use https.", LastModified = "2019/07/04", Value = "Indicates if all requests should use https.")]
    public string CacheServiceRequireHttpsDecription => this[nameof (CacheServiceRequireHttpsDecription)];

    /// <summary>Gets the label: Lifecycle</summary>
    [ResourceEntry("LifecycleConfigCaption", Description = "Gets the phrase: Lifecycle", LastModified = "2020/07/06", Value = "Lifecycle")]
    public string LifecycleConfigCaption => this[nameof (LifecycleConfigCaption)];

    /// <summary>Gets the phrase: Lifecycle configuration section</summary>
    [ResourceEntry("LifecycleConfigDescription", Description = "Gets the phrase: Lifecycle configuration section", LastModified = "2020/07/06", Value = "Lifecycle configuration section")]
    public string LifecycleConfigDescription => this[nameof (LifecycleConfigDescription)];
  }
}
