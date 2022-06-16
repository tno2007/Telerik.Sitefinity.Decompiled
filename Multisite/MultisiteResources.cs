// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.MultisiteResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("MultisiteResources", ResourceClassId = "MultisiteResources", TitlePlural = "MultisiteResourcesTitlePlural")]
  public sealed class MultisiteResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Multisite.MultisiteResources" /> class with the default <see cref="T:Telerik.Sitefinity.Multisite.MultisiteDataProvider" />.
    /// </summary>
    public MultisiteResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Multisite.MultisiteResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Multisite.MultisiteDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public MultisiteResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Multisite management title.</summary>
    [ResourceEntry("MultisiteResourcesTitle", Description = "The title of this class.", LastModified = "2012/07/17", Value = "Multisite management")]
    public string MultisiteResourcesTitle => this[nameof (MultisiteResourcesTitle)];

    /// <summary>Contains description for multisite management.</summary>
    [ResourceEntry("MultisiteResourcesDescription", Description = "The description of this class.", LastModified = "2012/07/17", Value = "Contains localizable resources for Multisite management user interface.")]
    public string MultisiteResourcesDescription => this[nameof (MultisiteResourcesDescription)];

    /// <summary>Multisite management</summary>
    /// <value>Title plural for the Multisite management module.</value>
    [ResourceEntry("MultisiteResourcesTitlePlural", Description = "Title plural for the Multisite management module.", LastModified = "2012/07/17", Value = "Multisite management")]
    public string MultisiteResourcesTitlePlural => this[nameof (MultisiteResourcesTitlePlural)];

    /// <summary>Multisite management</summary>
    /// <value>Module title.</value>
    [ResourceEntry("ModuleTitle", Description = "Module title.", LastModified = "2012/08/07", Value = "Multisite management")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Word: Sites</summary>
    [ResourceEntry("Sites", Description = "Word: Sites", LastModified = "2012/07/18", Value = "Sites")]
    public string Sites => this[nameof (Sites)];

    /// <summary>Word: SitesLower</summary>
    [ResourceEntry("SitesLower", Description = "Word: sites", LastModified = "2012/09/12", Value = "sites")]
    public string SitesLower => this[nameof (SitesLower)];

    /// <summary>Word: site</summary>
    [ResourceEntry("SiteLower", Description = "Word: site", LastModified = "2012/09/12", Value = "site")]
    public string SiteLower => this[nameof (SiteLower)];

    /// <summary>Phrase: Create a site</summary>
    [ResourceEntry("CreateSite", Description = "Phrase: Create a site", LastModified = "2012/07/18", Value = "Create a site")]
    public string CreateSite => this[nameof (CreateSite)];

    /// <summary>Phrase: Edit a site</summary>
    [ResourceEntry("EditSite", Description = "Phrase: Edit a site", LastModified = "2012/07/19", Value = "Edit a site")]
    public string EditSite => this[nameof (EditSite)];

    /// <summary>Phrase: General settings</summary>
    [ResourceEntry("GeneralSettings", Description = "Phrase: General settings", LastModified = "2016/03/29", Value = "General settings")]
    public string GeneralSettings => this[nameof (GeneralSettings)];

    /// <summary>Phrase: Set website offline</summary>
    [ResourceEntry("SetOffline", Description = "Word: Set website offline", LastModified = "2012/07/23", Value = "Set website offline")]
    public string SetOffline => this[nameof (SetOffline)];

    /// <summary>Word: Set online</summary>
    [ResourceEntry("SetOnline", Description = "Word: Set online", LastModified = "2012/07/23", Value = "Set online")]
    public string SetOnline => this[nameof (SetOnline)];

    /// <summary>Phrase: Configure modules</summary>
    [ResourceEntry("ConfigureModules", Description = "Phrase: Configure modules", LastModified = "2021/01/21", Value = "Configure modules")]
    public string ConfigureModules => this[nameof (ConfigureModules)];

    /// <summary>Phrase: Configure modules and access</summary>
    [ResourceEntry("ConfigureModulesAndAccess", Description = "Phrase: Configure modules and access", LastModified = "2021/01/21", Value = "Configure modules and access")]
    public string ConfigureModulesAndAccess => this[nameof (ConfigureModulesAndAccess)];

    /// <summary>Word: Offline</summary>
    [ResourceEntry("Offline", Description = "Word: Offline", LastModified = "2012/07/19", Value = "Offline")]
    public string Offline => this[nameof (Offline)];

    /// <summary>Word: Online</summary>
    [ResourceEntry("Online", Description = "Word: Online", LastModified = "2012/08/08", Value = "Online")]
    public string Online => this[nameof (Online)];

    /// <summary>Phrase: Back to Sites</summary>
    [ResourceEntry("BackToSites", Description = "Phrase: Back to Sites", LastModified = "2012/07/19", Value = "Back to Sites")]
    public string BackToSites => this[nameof (BackToSites)];

    /// <summary>Phrase: Back to site</summary>
    [ResourceEntry("BackToSite", Description = "Phrase: Back to site", LastModified = "2020/02/05", Value = "Back to site")]
    public string BackToSite => this[nameof (BackToSite)];

    /// <summary>Phrase: Back to Site properties</summary>
    [ResourceEntry("BackToSiteProperties", Description = "Phrase: Back to Site properties", LastModified = "2020/01/31", Value = "Back to Site properties")]
    public string BackToSiteProperties => this[nameof (BackToSiteProperties)];

    /// <summary>Phrase: Example: My site - Australia</summary>
    [ResourceEntry("SiteNameExample", Description = "Example: My site - Australia", LastModified = "2012/07/19", Value = "<strong>Example:</strong> <em>My site &mdash; Australia</em>")]
    public string SiteNameExample => this[nameof (SiteNameExample)];

    /// <summary>Word: Name</summary>
    [ResourceEntry("Name", Description = "Word: Name", LastModified = "2012/07/19", Value = "Name")]
    public string Name => this[nameof (Name)];

    /// <summary>Are you sure you want to delete this website?</summary>
    [ResourceEntry("DeleteSiteTitle", Description = "The text to be shown for deleting a website", LastModified = "2012/07/23", Value = "Are you sure you want to delete this website?")]
    public string DeleteSiteTitle => this[nameof (DeleteSiteTitle)];

    /// <summary>
    /// All the data, users and files used in this website will be also deleted.
    /// </summary>
    [ResourceEntry("DeleteSiteMessage", Description = "The text message to be shown for deleting a site.", LastModified = "2012/07/23", Value = "All the data, users and files used in this website will be also deleted.")]
    public string DeleteSiteMessage => this[nameof (DeleteSiteMessage)];

    /// <summary>Are you sure you want to set this site offline?</summary>
    [ResourceEntry("SetOfflineSiteTitle", Description = "The text to be shown for setting a site offline", LastModified = "2012/07/26", Value = "Are you sure you want to set this site offline?")]
    public string SetOfflineSiteTitle => this[nameof (SetOfflineSiteTitle)];

    /// <summary>Stops this site from the list of sites.</summary>
    [ResourceEntry("SetOfflineSiteMessage", Description = "The text message to be shown for stopping a site.", LastModified = "2012/07/23", Value = "Stop this site from the list of sites.")]
    public string SetOfflineSiteMessage => this[nameof (SetOfflineSiteMessage)];

    /// <summary>Phrase: This site is in offline mode</summary>
    [ResourceEntry("ThisSiteIsInOfflineMode", Description = "Phrase: This site is in offline mode", LastModified = "2012/07/20", Value = "This site is in offline mode")]
    public string ThisSiteIsInOfflineMode => this[nameof (ThisSiteIsInOfflineMode)];

    /// <summary>Delete this website.</summary>
    [ResourceEntry("DeleteSite", Description = "Delete this website.", LastModified = "2012/07/23", Value = "Delete this website")]
    public string DeleteSite => this[nameof (DeleteSite)];

    /// <summary>Phrase: Configure modules for</summary>
    [ResourceEntry("ConfigureModulesFor", Description = "Phrase: Configure modules for", LastModified = "2012/07/23", Value = "Configure modules for")]
    public string ConfigureModulesFor => this[nameof (ConfigureModulesFor)];

    /// <summary>Phrase: Configure modules and access for</summary>
    [ResourceEntry("ConfigureModulesAndAccessFor", Description = "Phrase: Configure modules and access for", LastModified = "2021/01/21", Value = "Configure modules and access for")]
    public string ConfigureModulesAndAccessFor => this[nameof (ConfigureModulesAndAccessFor)];

    /// <summary>Phrase: This site has:</summary>
    [ResourceEntry("ThisSiteHas", Description = "Phrase: This site has:", LastModified = "2012/07/25", Value = "This site has:")]
    public string ThisSiteHas => this[nameof (ThisSiteHas)];

    /// <summary>Phrase: This site can use data from...</summary>
    [ResourceEntry("ThisSiteCanUseDataFrom", Description = "Phrase: This site can use data from...", LastModified = "2012/07/25", Value = "This site can use data from...")]
    public string ThisSiteCanUseDataFrom => this[nameof (ThisSiteCanUseDataFrom)];

    /// <summary>Phrase: Module</summary>
    [ResourceEntry("ModuleColumnTitle", Description = "Phrase: Module", LastModified = "2020/01/18", Value = "Module")]
    public string ModuleColumnTitle => this[nameof (ModuleColumnTitle)];

    /// <summary>Phrase: Source</summary>
    [ResourceEntry("SourceColumnTitle", Description = "Phrase: Source", LastModified = "2020/01/18", Value = "Source")]
    public string SourceColumnTitle => this[nameof (SourceColumnTitle)];

    /// <summary>Phrase: User groups for this site</summary>
    [ResourceEntry("UserGroupsHeaderTitle", Description = "Phrase: User groups for this site", LastModified = "2021/01/06", Value = "User groups for this site")]
    public string UserGroupsHeaderTitle => this[nameof (UserGroupsHeaderTitle)];

    /// <summary>Phrase: Select user groups</summary>
    [ResourceEntry("SelectUserGroups", Description = "Phrase: Select user groups", LastModified = "2021/01/06", Value = "Select user groups")]
    public string SelectUserGroups => this[nameof (SelectUserGroups)];

    /// <summary>User group</summary>
    [ResourceEntry("UserGroup", Description = "User group", LastModified = "2021/01/06", Value = "User group")]
    public string UserGroup => this[nameof (UserGroup)];

    /// <summary>Users of international sites</summary>
    [ResourceEntry("UserGroupNameExample", Description = "Users of international sites", LastModified = "2021/01/06", Value = "Users of international sites")]
    public string UserGroupNameExample => this[nameof (UserGroupNameExample)];

    /// <summary>Access to</summary>
    [ResourceEntry("AccessTo", Description = "Access to", LastModified = "2021/01/06", Value = "Access to")]
    public string AccessTo => this[nameof (AccessTo)];

    /// <summary>Create user group</summary>
    [ResourceEntry("CreateNewUserGroup", Description = "Create user group", LastModified = "2021/01/18", Value = "Create user group")]
    public string CreateNewUserGroup => this[nameof (CreateNewUserGroup)];

    /// <summary>User group name</summary>
    [ResourceEntry("UserGroupName", Description = "User group name", LastModified = "2021/01/06", Value = "User group name")]
    public string UserGroupName => this[nameof (UserGroupName)];

    /// <summary>
    /// Phrase: The selected user groups are functionally user data sources that can be managed and could be provided with backend access to this site. Users from a user group marked as \"global\" have access to the backend of all sites, regardless if this group is selected here or not. If such а group is selected, in addition to accessing the site backend, its users can also be managed in this site. Learn more.
    /// </summary>
    [ResourceEntry("UserGroupsTooltipDescription", Description = "Phrase: The selected user groups are functionally user data sources that can be managed and could be provided with backend access to this site. Users from a user group marked as \"global\" have access to the backend of all sites, regardless if this group is selected here or not. If such а group is selected, in addition to accessing the site backend, its users can also be managed in this site.", LastModified = "2021/01/18", Value = "The selected user groups are functionally user data sources that can be managed and could be provided with backend access to this site. Users from a user group marked as \"global\" have access to the backend of all sites, regardless if this group is selected here or not. If such а group is selected, in addition to accessing the site backend, its users can also be managed in this site. <a href=\"https://www.progress.com/documentation/sitefinity-cms/user-groups\" target=\"_blank\">Learn more</a>")]
    public string UserGroupsTooltipDescription => this[nameof (UserGroupsTooltipDescription)];

    /// <summary>
    /// Phrase: Data source for content items of a module. By default, each module uses a separate source for each site. If you need shared content between sites (e.g. shared News), you can select sources from other sites for their respective module. Any changes to the items of a shared source are reflected and visible on every site this source is shared on.
    /// </summary>
    [ResourceEntry("SourceColumnTooltipDescription", Description = "Phrase: Data source for content items of a module. By default, each module uses a separate source for each site. If you need shared content between sites (e.g. shared News), you can select sources from other sites for their respective module. Any changes to the items of a shared source are reflected and visible on every site this source is shared on.", LastModified = "2021/01/18", Value = "Data source for content items of a module. By default, each module uses a separate source for each site. If you need shared content between sites (e.g. shared News), you can select sources from other sites for their respective module. Any changes to the items of a shared source are reflected and visible on every site this source is shared on.")]
    public string SourceColumnTooltipDescription => this[nameof (SourceColumnTooltipDescription)];

    /// <summary>Phrase: User groups</summary>
    [ResourceEntry("UserGroups", Description = "Phrase: User groups", LastModified = "2021/01/06", Value = "User groups")]
    public string UserGroups => this[nameof (UserGroups)];

    /// <summary>Phrase: (global)</summary>
    [ResourceEntry("GlobalUserGroupNote", Description = "(global)", LastModified = "2021/01/06", Value = "(global)")]
    public string GlobalUserGroupNote => this[nameof (GlobalUserGroupNote)];

    /// <summary>Phrase: Change user groups</summary>
    [ResourceEntry("ChangeUserGroups", Description = "Phrase: Change user groups", LastModified = "2021/01/06", Value = "Change user groups")]
    public string ChangeUserGroups => this[nameof (ChangeUserGroups)];

    /// <summary>
    /// Phrase: Backend users of this user group can access the backend of all sites in the system.
    /// </summary>
    [ResourceEntry("DefaultProviderUserGroupInformationMessage", Description = "Phrase: Backend users of this user group can access the backend of all sites in the system.", LastModified = "2021/01/18", Value = "Backend users of this user group can access the backend of all sites in the system.")]
    public string DefaultProviderUserGroupInformationMessage => this[nameof (DefaultProviderUserGroupInformationMessage)];

    /// <summary>
    /// Phrase: Some backend functionality is not available for users in this user group regardless of their role. Learn more
    /// </summary>
    [ResourceEntry("NonDefaultProviderRolesInformationMessage", Description = "Phrase: Some backend functionality is not available for users in this user group regardless of their role. Learn more", LastModified = "2021/01/18", Value = "Some backend functionality is not available for users in this user group regardless of their role. <a href=\"https://www.progress.com/documentation/sitefinity-cms/user-groups\" target=\"_blank\">Learn more</a>")]
    public string NonDefaultProviderRolesInformationMessage => this[nameof (NonDefaultProviderRolesInformationMessage)];

    /// <summary>Phrase: Learn more</summary>
    [ResourceEntry("LearnMore", Description = "Phrase: Learn more", LastModified = "2021/01/06", Value = "Learn more")]
    public string LearnMore => this[nameof (LearnMore)];

    /// <summary>Phrase: Domain</summary>
    [ResourceEntry("Domain", Description = "Phrase: Domain", LastModified = "2012/07/23", Value = "Domain")]
    public string Domain => this[nameof (Domain)];

    /// <summary>
    /// Phrase: Example: mysite.com, marketing.mysite.com, mysite.com:80
    /// </summary>
    [ResourceEntry("DomainExample", Description = "Phrase: Example: mysite.com, marketing.mysite.com, mysite.com:80", LastModified = "2012/07/23", Value = "<strong>Example:</strong> <div class='sfAlignTop sfInlineBlock sfMLeft5'>mysite.com<br />marketing.mysite.com<br />mysite.com:80</div>")]
    public string DomainExample => this[nameof (DomainExample)];

    /// <summary>phrase: Domain cannot be empty.</summary>
    [ResourceEntry("DomainCannotBeEmpty", Description = "phrase: Domain cannot be empty.", LastModified = "2012/07/23", Value = "Domain cannot be empty.")]
    public string DomainCannotBeEmpty => this[nameof (DomainCannotBeEmpty)];

    /// <summary>phrase: Testing domain cannot be empty.</summary>
    [ResourceEntry("TestingDomainCannotBeEmpty", Description = "phrase: Testing domain cannot be empty.", LastModified = "2012/10/01", Value = "Testing domain cannot be empty.")]
    public string TestingDomainCannotBeEmpty => this[nameof (TestingDomainCannotBeEmpty)];

    /// <summary>
    /// Phrase: Testing domain (used while in process of development)
    /// </summary>
    [ResourceEntry("TestingDomain", Description = "Phrase: Testing domain (used while in process of development)", LastModified = "2012/07/27", Value = "Testing domain <span class='sfLblNote'>(used while in process of development)</span>")]
    public string TestingDomain => this[nameof (TestingDomain)];

    /// <summary>phrase: Domain aliases (one per line)</summary>
    [ResourceEntry("DomainAliases", Description = "phrase: Domain aliases (one per line)", LastModified = "2012/07/23", Value = "Domain aliases (one per line)")]
    public string DomainAliases => this[nameof (DomainAliases)];

    /// <summary>phrase: Example: mysite.com, my-legacy-site.com</summary>
    [ResourceEntry("DomainAliasesExample", Description = "phrase: Example: mysite.com, my-legacy-site.com", LastModified = "2012/07/23", Value = "<strong>Example:</strong> <div class='sfAlignTop sfInlineBlock sfMLeft5'>mysite.com<br/> my-legacy-site.com</div>")]
    public string DomainAliasesExample => this[nameof (DomainAliasesExample)];

    /// <summary>phrase: Default protocol</summary>
    [ResourceEntry("Protocol", Description = "phrase: Protocol", LastModified = "2012/08/27", Value = "Protocol")]
    public string DefaultProtocol => this["Protocol"];

    /// <summary>Word: Change</summary>
    [ResourceEntry("Change", Description = "Word: Change", LastModified = "2012/07/24", Value = "Change")]
    public string Change => this[nameof (Change)];

    /// <summary>phrase: Create empty site</summary>
    [ResourceEntry("CreateEmptySite", Description = "phrase: Create empty site", LastModified = "2012/07/23", Value = "Create empty site")]
    public string CreateEmptySite => this[nameof (CreateEmptySite)];

    /// <summary>
    /// phrase: Duplicate pages and settings from existing site...
    /// </summary>
    [ResourceEntry("DuplicatePagesAndSettingsFromExistingSite", Description = "phrase: Duplicate pages and settings from existing site...", LastModified = "2012/07/23", Value = "Duplicate pages and settings from existing site...")]
    public string DuplicatePagesAndSettingsFromExistingSite => this[nameof (DuplicatePagesAndSettingsFromExistingSite)];

    /// <summary>phrase: Show message</summary>
    [ResourceEntry("ShowMessage", Description = "phrase: Show message", LastModified = "2012/07/24", Value = "Show message")]
    public string ShowMessage => this[nameof (ShowMessage)];

    /// <summary>phrase: Redirect to specific page...</summary>
    [ResourceEntry("RedirectToPage", Description = "phrase: Redirect to specific page...", LastModified = "2012/07/24", Value = "Redirect to specific page...")]
    public string RedirectToPage => this[nameof (RedirectToPage)];

    /// <summary>
    /// phrase: If someone tries to open a page from this site...
    /// </summary>
    [ResourceEntry("IfSomeoneTriesToOpenPage", Description = "phrase: If someone tries to open a page from this site...", LastModified = "2012/07/24", Value = "If someone tries to open a page from this site...")]
    public string IfSomeoneTriesToOpenPage => this[nameof (IfSomeoneTriesToOpenPage)];

    /// <summary>Word: Customize</summary>
    [ResourceEntry("Customize", Description = "word: Customize", LastModified = "2012/07/19", Value = "Customize")]
    public string Customize => this[nameof (Customize)];

    /// <summary>Phrase: Select sources</summary>
    [ResourceEntry("SelectSources", Description = "Phrase: Select sources", LastModified = "2012/07/20", Value = "Select sources")]
    public string SelectSources => this[nameof (SelectSources)];

    /// <summary>phrase: All {0} sites</summary>
    [ResourceEntry("AllSites", Description = "phrase: All {0} sites", LastModified = "2012/07/23", Value = "All {0} sites")]
    public string AllSites => this[nameof (AllSites)];

    /// <summary>Phrase: Select sites</summary>
    [ResourceEntry("SelectSites", Description = "Phrase: Select sites", LastModified = "2012/07/23", Value = "Select sites")]
    public string SelectSites => this[nameof (SelectSites)];

    /// <summary>Phrase: Select site</summary>
    [ResourceEntry("SelectSite", Description = "Phrase: Select site", LastModified = "2012/07/23", Value = "Select site")]
    public string SelectSite => this[nameof (SelectSite)];

    /// <summary>
    /// Phrase: You can customize the menu and select which siets you want to see.
    /// </summary>
    [ResourceEntry("SelectSitesExplanationLine1", Description = "Phrase: You can customize the menu and select which sites you want to see.", LastModified = "2012/07/20", Value = "You can customize the menu and select which sites you want to see.")]
    public string SelectSitesExplanationLine1 => this[nameof (SelectSitesExplanationLine1)];

    /// <summary>
    /// Phrase: For example, if you manage 5 of 45 sites you can select only them to appear in the menu.
    /// </summary>
    [ResourceEntry("SelectSitesExplanationLine2", Description = "Phrase: For example, if you manage 5 of 45 sites you can select only them to appear in the menu.", LastModified = "2012/07/20", Value = "For example, if you manage 5 of 45 sites you can select only them to appear in the menu.")]
    public string SelectSitesExplanationLine2 => this[nameof (SelectSitesExplanationLine2)];

    /// <summary>Phrase: This page is not accessible</summary>
    [ResourceEntry("ThisPageIsNotAccessible", Description = "Phrase: This page is not accessible", LastModified = "2012/07/25", Value = "This page is not accessible")]
    public string ThisPageIsNotAccessible => this[nameof (ThisPageIsNotAccessible)];

    /// <summary>Phrase: This site can use {0} from...</summary>
    [ResourceEntry("ThisSiteCanUseModuleFrom", Description = "Phrase: This site can use {0} from...", LastModified = "2012/07/30", Value = "This site can use {0} from...")]
    public string ThisSiteCanUseModuleFrom => this[nameof (ThisSiteCanUseModuleFrom)];

    /// <summary>phrase: Used also by</summary>
    [ResourceEntry("UsedAlsoBy", Description = "phrase: Used also by", LastModified = "2012/07/31", Value = "Used also by")]
    public string UsedAlsoBy => this[nameof (UsedAlsoBy)];

    /// <summary>phrase: this site</summary>
    [ResourceEntry("ThisSite", Description = "phrase: this site", LastModified = "2012/08/01", Value = "this site")]
    public string ThisSite => this[nameof (ThisSite)];

    /// <summary>phrase: This site</summary>
    [ResourceEntry("ThisSiteCapital", Description = "phrase: This site", LastModified = "2012/09/21", Value = "This site")]
    public string ThisSiteCapital => this[nameof (ThisSiteCapital)];

    /// <summary>Phrase: Back to Create a site</summary>
    [ResourceEntry("BackToCreateSite", Description = "Phrase: Back to Create a site", LastModified = "2012/08/01", Value = "Back to Create a site")]
    public string BackToCreateSite => this[nameof (BackToCreateSite)];

    /// <summary>MultisiteManagement</summary>
    [ResourceEntry("MultisiteManagementUrlName", Description = "The name that appears in the URL.", LastModified = "2012/07/17", Value = "MultisiteManagement")]
    public string MultisiteManagementUrlName => this[nameof (MultisiteManagementUrlName)];

    /// <summary>Multisite management</summary>
    [ResourceEntry("MultisiteManagementTitle", Description = "The title of the Multisite management page.", LastModified = "2012/07/17", Value = "Multisite management")]
    public string MultisiteManagementTitle => this[nameof (MultisiteManagementTitle)];

    /// <summary>Multisite management</summary>
    [ResourceEntry("MultisiteManagementHtmlTitle", Description = "The html title of the Multisite management page.", LastModified = "2012/07/17", Value = "Multisite management")]
    public string MultisiteManagementHtmlTitle => this[nameof (MultisiteManagementHtmlTitle)];

    /// <summary>ManageSite</summary>
    [ResourceEntry("ManageSiteUrlName", Description = "The name that appears in the URL.", LastModified = "2020/02/27", Value = "ManageSite")]
    public string ManageSiteUrlName => this[nameof (ManageSiteUrlName)];

    /// <summary>Manage site</summary>
    [ResourceEntry("ManageSiteTitle", Description = "The title of the Manage site page.", LastModified = "2020/02/27", Value = "Manage site")]
    public string ManageSiteTitle => this[nameof (ManageSiteTitle)];

    /// <summary>Manage site</summary>
    [ResourceEntry("ManageSiteHtmlTitle", Description = "The html title of the Manage site page.", LastModified = "2020/02/27", Value = "Manage site")]
    public string ManageSiteHtmlTitle => this[nameof (ManageSiteHtmlTitle)];

    /// <summary>Site Settings</summary>
    [ResourceEntry("SiteSettingsTitle", Description = "The title of the site settings page.", LastModified = "2016/03/18", Value = "Site Settings")]
    public string SiteSettingsTitle => this[nameof (SiteSettingsTitle)];

    /// <summary>SiteSettings</summary>
    [ResourceEntry("SiteSettingsUrlName", Description = "The name that appears in the URL.", LastModified = "2016/03/18", Value = "SiteSettings")]
    public string SiteSettingsUrlName => this[nameof (SiteSettingsUrlName)];

    /// <summary>Provides user interface for site settings.</summary>
    [ResourceEntry("SiteSettingsDescription", Description = "Description of site settings section.", LastModified = "2016/03/18", Value = "Provides user interface for site settings.")]
    public string SiteSettingsDescription => this[nameof (SiteSettingsDescription)];

    /// <summary>SiteSettings</summary>
    [ResourceEntry("SiteSettingsHtmlTitle", Description = "The html title of the site settings page.", LastModified = "2016/03/18", Value = "Site Settings")]
    public string SiteSettingsHtmlTitle => this[nameof (SiteSettingsHtmlTitle)];

    /// <summary>Configure providers</summary>
    [ResourceEntry("ProvidersTitle", Description = "The title of the configure providers page.", LastModified = "2016/03/18", Value = "Configure providers")]
    public string ProvidersTitle => this[nameof (ProvidersTitle)];

    /// <summary>Providers</summary>
    [ResourceEntry("ProvidersUrlName", Description = "The name that appears in the URL.", LastModified = "2016/03/18", Value = "Providers")]
    public string ProvidersUrlName => this[nameof (ProvidersUrlName)];

    /// <summary>Provides user interface for providers.</summary>
    [ResourceEntry("ProvidersDescription", Description = "Description of providers section.", LastModified = "2016/03/18", Value = "Provides user interface for providers.")]
    public string ProvidersDescription => this[nameof (ProvidersDescription)];

    /// <summary>Providers</summary>
    [ResourceEntry("ProvidersHtmlTitle", Description = "The html title of the providers page.", LastModified = "2016/03/18", Value = "Providers")]
    public string ProvidersHtmlTitle => this[nameof (ProvidersHtmlTitle)];

    /// <summary>Url name of the dashboard page of the custom modules</summary>
    [ResourceEntry("MultisiteDashboardUrlName", Description = "Url name of the dashboard page of the multisite", LastModified = "2011/09/22", Value = "dashboard")]
    public string ContentTypesDashboardUrlName => this[nameof (ContentTypesDashboardUrlName)];

    /// <summary>Provides user interface for multisite management.</summary>
    [ResourceEntry("MultisiteManagementDescription", Description = "Description of Multisite management section.", LastModified = "2012/07/17", Value = "Provides user interface for multisite management.")]
    public string MultisiteManagementDescription => this[nameof (MultisiteManagementDescription)];

    /// <summary>Provides user interface for site management.</summary>
    [ResourceEntry("ManageSiteDescription", Description = "Description of site management section.", LastModified = "2020/02/27", Value = "Provides user interface for site management.")]
    public string ManageSiteDescription => this[nameof (ManageSiteDescription)];

    /// <summary>The name of the SiteSelectorControl Widget</summary>
    [ResourceEntry("SiteSelectorControlName", Description = "The name of the SiteSelectorControl Widget", LastModified = "2012/08/14", Value = "SiteSelectorControl")]
    public string SiteSelectorControlName => this[nameof (SiteSelectorControlName)];

    /// <summary>The title of the SiteSelectorControl Widget</summary>
    [ResourceEntry("SiteSelectorControlTitle", Description = "The title of the SiteSelectorControl Widget", LastModified = "2012/08/14", Value = "Site selector")]
    public string SiteSelectorControlTitle => this[nameof (SiteSelectorControlTitle)];

    /// <summary>The description of the SiteSelectorControl Widget</summary>
    [ResourceEntry("SiteSelectorControlDescription", Description = "The description of the SiteSelectorControl Widget", LastModified = "2012/08/14", Value = "Front end Site selector widget")]
    public string SiteSelectorControlDescription => this[nameof (SiteSelectorControlDescription)];

    /// <summary>Phrase: Select a site</summary>
    [ResourceEntry("SiteSelectorControlDefaultLabel", Description = "Phrase: Select a site", LastModified = "2012/08/14", Value = "Select a site")]
    public string SiteSelectorControlDefaultLabel => this[nameof (SiteSelectorControlDefaultLabel)];

    /// <summary>Phrase: Display as...</summary>
    [ResourceEntry("DisplayAsLabel", Description = "Phrase: Display as...", LastModified = "2012/08/14", Value = "Display as...")]
    public string DisplayAsLabel => this[nameof (DisplayAsLabel)];

    /// <summary>Phrase: Drop-down menu</summary>
    [ResourceEntry("DropDownViewMenuLabel", Description = "Phrase: Drop-down menu", LastModified = "2012/08/14", Value = "Drop-down menu")]
    public string DropDownViewMenuLabel => this[nameof (DropDownViewMenuLabel)];

    /// <summary>Phrase: List of links</summary>
    [ResourceEntry("ListOfLinksViewMenuLabel", Description = "Phrase: List of links", LastModified = "2012/08/14", Value = "List of links")]
    public string ListOfLinksViewMenuLabel => this[nameof (ListOfLinksViewMenuLabel)];

    /// <summary>Word: Settings</summary>
    [ResourceEntry("Settings", Description = "Word: Settings", LastModified = "2012/08/14", Value = "Settings")]
    public string Settings => this[nameof (Settings)];

    /// <summary>Word: Label</summary>
    [ResourceEntry("SiteSelectorDefaultLabelName", Description = "Word: Label", LastModified = "2012/08/14", Value = "Label")]
    public string SiteSelectorDefaultLabelName => this[nameof (SiteSelectorDefaultLabelName)];

    /// <summary>Phrase: Select a site</summary>
    [ResourceEntry("SiteSelectorDefaultLabel", Description = "Phrase: Select a site", LastModified = "2012/08/14", Value = "Select a site")]
    public string SiteSelectorDefaultLabel => this[nameof (SiteSelectorDefaultLabel)];

    /// <summary>Phrase: Include the current site</summary>
    [ResourceEntry("IncludeCurrentSite", Description = "Phrase: Include the current site", LastModified = "2012/08/14", Value = "Include the current site")]
    public string IncludeCurrentSite => this[nameof (IncludeCurrentSite)];

    /// <summary>
    /// Phrase: Otherwise it will be something like "Other sites"
    /// </summary>
    [ResourceEntry("IncludeCurrentSiteDescription", Description = "Phrase: Otherwise it will be something like \"Other sites\"", LastModified = "2012/08/14", Value = "Otherwise it will be something like \"Other sites\"")]
    public string IncludeCurrentSiteDescription => this[nameof (IncludeCurrentSiteDescription)];

    /// <summary>
    /// Phrase: Display each language version as a separate site
    /// </summary>
    [ResourceEntry("DisplayLanguageVersions", Description = "Phrase: Display each language version as a separate site", LastModified = "2012/08/14", Value = "Display each language version as a separate site")]
    public string DisplayLanguageVersions => this[nameof (DisplayLanguageVersions)];

    /// <summary>Phrase: Show site names and languages</summary>
    [ResourceEntry("ShowSiteNamesAndLanguagesOption", Description = "Phrase: Show site names and languages", LastModified = "2012/08/14", Value = "Show site names and languages")]
    public string ShowSiteNamesAndLanguagesOption => this[nameof (ShowSiteNamesAndLanguagesOption)];

    /// <summary>Phrase: Show languages only</summary>
    [ResourceEntry("ShowLanguagesOnly", Description = "Phrase: Show languages only", LastModified = "2012/08/14", Value = "Show languages only")]
    public string ShowLanguagesOnly => this[nameof (ShowLanguagesOnly)];

    /// <summary>Phrase: Live url</summary>
    [ResourceEntry("LiveUrl", Description = "Phrase: Live url", LastModified = "2012/08/15", Value = "Live url")]
    public string LiveUrl => this[nameof (LiveUrl)];

    /// <summary>Phrase: Staging url</summary>
    [ResourceEntry("StagingUrl", Description = "Phrase: Staging url", LastModified = "2012/08/15", Value = "Staging url")]
    public string StagingUrl => this[nameof (StagingUrl)];

    /// <summary>Phrase: Main url</summary>
    [ResourceEntry("UrlTypeLabel", Description = "Phrase: Main url", LastModified = "2012/08/15", Value = "Main url")]
    public string UrlTypeLabel => this[nameof (UrlTypeLabel)];

    /// <summary>
    /// By default, all modules use content from this site only
    /// </summary>
    [ResourceEntry("ConfigureModulesDefaultsDescription", Description = "Text: By default, all modules use content from this site only", LastModified = "2012/08/13", Value = "By default, all modules use content from this site only")]
    public string ConfigureModulesDefaultsDescription => this[nameof (ConfigureModulesDefaultsDescription)];

    /// <summary>Phrase: Other sites</summary>
    [ResourceEntry("OtherSites", Description = "Phrase: Other sites", LastModified = "2012/08/15", Value = "Other sites")]
    public string OtherSites => this[nameof (OtherSites)];

    /// <summary>Select modules for this site</summary>
    [ResourceEntry("ConfigureModulesDescription", Description = "Text: Select modules for this site", LastModified = "2020/01/18", Value = "Select modules for this site")]
    public string ConfigureModulesDescription => this[nameof (ConfigureModulesDescription)];

    /// <summary>Create this site</summary>
    [ResourceEntry("CreateThisSite", Description = "Phrase: Create this site", LastModified = "2012/08/13", Value = "Create this site")]
    public string CreateThisSite => this[nameof (CreateThisSite)];

    /// <summary>Show more</summary>
    [ResourceEntry("MoreText", Description = "phrase: Show more", LastModified = "2012/08/16", Value = "Show more...")]
    public string MoreText => this[nameof (MoreText)];

    /// <summary>Show less</summary>
    [ResourceEntry("LessText", Description = "phrase: Show less", LastModified = "2012/08/16", Value = "Show less")]
    public string LessText => this[nameof (LessText)];

    /// <summary>Make Default</summary>
    [ResourceEntry("MakeDefault", Description = "phrase: Make Default", LastModified = "2012/08/22", Value = "Make Default")]
    public string MakeDefault => this[nameof (MakeDefault)];

    /// <summary>Phrase: This site is in process of development</summary>
    [ResourceEntry("ThisSiteIsInProcessOfDevelopment", Description = "phrase: This site is in process of development", LastModified = "2012/09/03", Value = "This site is in process of development")]
    public string ThisSiteIsInProcessOfDevelopment => this[nameof (ThisSiteIsInProcessOfDevelopment)];

    /// <summary>Phrase: Enter URL</summary>
    [ResourceEntry("EnterUrl", Description = "phrase: Enter URL", LastModified = "2012/09/04", Value = "Enter URL")]
    public string EnterUrl => this[nameof (EnterUrl)];

    /// <summary>Phrase: Address for public users login</summary>
    [ResourceEntry("AddressForPublicUsersLogin", Description = "phrase: Address for public users login", LastModified = "2012/09/04", Value = "Address for public users login")]
    public string AddressForPublicUserLogin => this[nameof (AddressForPublicUserLogin)];

    /// <summary>phrase: Enter URL from the same domain</summary>
    [ResourceEntry("PublicLoginPageDescription", Description = "phrase: Enter URL from the same domain", LastModified = "2012/09/04", Value = "Enter URL from the same domain")]
    public string PublicLoginPageDescription => this[nameof (PublicLoginPageDescription)];

    /// <summary>
    /// Error message: Domain "{0}" is already used by site "{1}"
    /// </summary>
    [ResourceEntry("DomainAlreadyUsedByAnotherSite", Description = "Error message: Domain \"{0}\" is already used by site \"{1}\"", LastModified = "2012/09/05", Value = "Domain \"{0}\" is already used by site \"{1}\"")]
    public string DomainAlreadyUsedByAnotherSite => this[nameof (DomainAlreadyUsedByAnotherSite)];

    /// <summary>Error message: Site with name "{0}" already exists</summary>
    [ResourceEntry("DuplicateSiteName", Description = "Site with name \"{0}\" already exists", LastModified = "2012/09/11", Value = "Site with name \"{0}\" already exists")]
    public string DuplicateSiteName => this[nameof (DuplicateSiteName)];

    /// <summary>Error message: Site's domain cannot contain spaces</summary>
    [ResourceEntry("SiteDomainCannotContainSpaces", Description = "Site's domain cannot contain spaces", LastModified = "2012/09/14", Value = "Site's domain cannot contain spaces")]
    public string SiteDomainCannotContainSpaces => this[nameof (SiteDomainCannotContainSpaces)];

    /// <summary>
    /// Error message: Site's domain "{0}" cannot contain spaces
    /// </summary>
    [ResourceEntry("SiteSpecificDomainCannotContainSpaces", Description = "Site's domain \"{0}\" cannot contain spaces", LastModified = "2012/09/14", Value = "Site's domain \"{0}\" cannot contain spaces")]
    public string SiteSpecificDomainCannotContainSpaces => this[nameof (SiteSpecificDomainCannotContainSpaces)];

    /// <summary>
    /// Error message: Site's domain "{0}" cannot contain a path
    /// </summary>
    [ResourceEntry("SiteSpecificDomainCannotContainPath", Description = "Site's domain \"{0}\" cannot contain more than one levels in the path", LastModified = "2013/01/04", Value = "Site's domain \"{0}\" cannot contain more than one levels in the path")]
    public string SiteSpecificDomainCannotContainPath => this[nameof (SiteSpecificDomainCannotContainPath)];

    /// <summary>Error message: Site's domain cannot contain a path</summary>
    [ResourceEntry("SiteDomainCannotContainPath", Description = "Site's domain cannot contain a subpath in the path", LastModified = "2013/01/04", Value = "Site's domain cannot contain more than one levels in the path")]
    public string SiteDomainCannotContainPath => this[nameof (SiteDomainCannotContainPath)];

    /// <summary>Error message: Site's domain "{0}" is invalid</summary>
    [ResourceEntry("SiteSpecificDomainIsInvalid", Description = "Site's domain \"{0}\" is invalid", LastModified = "2012/10/03", Value = "Site's domain \"{0}\" is invalid")]
    public string SiteSpecificDomainIsInvalid => this[nameof (SiteSpecificDomainIsInvalid)];

    /// <summary>
    /// Error message: Testing domain "{0}" is already used by site "{1}"
    /// </summary>
    [ResourceEntry("TestingDomainAlreadyUsedByAnotherSite", Description = "Error message: Testing domain \"{0}\" is already used by site \"{1}\"", LastModified = "2012/09/10", Value = "Testing domain \"{0}\" is already used by site \"{1}\"")]
    public string TestingDomainAlreadyUsedByAnotherSite => this[nameof (TestingDomainAlreadyUsedByAnotherSite)];

    /// <summary>
    /// Error message: Domain alias "{0}" is already used by site "{1}"
    /// </summary>
    [ResourceEntry("DomainAliasAlreadyUsedByAnotherSite", Description = "Error message: Domain alias \"{0}\" is already used by site \"{1}\"", LastModified = "2012/09/10", Value = "Domain alias \"{0}\" is already used by site \"{1}\"")]
    public string DomainAliasAlreadyUsedByAnotherSite => this[nameof (DomainAliasAlreadyUsedByAnotherSite)];

    /// <summary>phrase: in {0} sites</summary>
    [ResourceEntry("SiteCountFormat", Description = "phrase: in {0} sites", LastModified = "2012/09/11", Value = "in {0} sites")]
    public string SiteCountFormat => this[nameof (SiteCountFormat)];

    /// <summary>phrase: in this site</summary>
    [ResourceEntry("InThisSite", Description = "phrase: in this site", LastModified = "2012/09/11", Value = "in this site")]
    public string InThisSite => this[nameof (InThisSite)];

    /// <summary>phrase: in this site</summary>
    [ResourceEntry("InOneSite", Description = "phrase: in 1 site", LastModified = "2012/09/18", Value = "in 1 site")]
    public string InOneSite => this[nameof (InOneSite)];

    /// <summary>phrase: This template is shared with {0} sites</summary>
    [ResourceEntry("TemplateUsageTitle", Description = "phrase: This template is shared with {0} sites", LastModified = "2012/09/12", Value = "This template is shared with {0} sites")]
    public string TemplateUsageTitle => this[nameof (TemplateUsageTitle)];

    /// <summary>phrase: This template is shared with 1 site</summary>
    [ResourceEntry("TemplateUsageTitleSingular", Description = "phrase: This template is shared with 1 site", LastModified = "2012/09/12", Value = "This template is shared with 1 site")]
    public string TemplateUsageTitleSingular => this[nameof (TemplateUsageTitleSingular)];

    /// <summary>
    /// phrase: Template changes will affect all of these sites:
    /// </summary>
    [ResourceEntry("TemplateUsageDescription", Description = "phrase: Template changes will affect all of these sites:", LastModified = "2012/09/12", Value = "Template changes will affect all of these sites:")]
    public string TemplateUsageDescription => this[nameof (TemplateUsageDescription)];

    /// <summary>phrase: Share this template with selected sites</summary>
    [ResourceEntry("ShareTemplateTitle", Description = "phrase: Share this template with selected sites", LastModified = "2012/09/13", Value = "Share this template with selected sites")]
    public string ShareTemplateTitle => this[nameof (ShareTemplateTitle)];

    /// <summary>phrase: Site properties</summary>
    [ResourceEntry("SiteProperties", Description = "phrase: Site properties", LastModified = "2020/01/21", Value = "Site properties")]
    public string SiteProperties => this[nameof (SiteProperties)];

    /// <summary>phrase: Manage site</summary>
    [ResourceEntry("ManageSite", Description = "phrase: Manage site", LastModified = "2020/01/21", Value = "Manage site")]
    public string ManageSite => this[nameof (ManageSite)];

    /// <summary>phrase: Manage sites</summary>
    [ResourceEntry("ManageSites", Description = "phrase: Manage sites", LastModified = "2012/09/14", Value = "Manage sites")]
    public string ManageSites => this[nameof (ManageSites)];

    /// <summary>phrase: Global settings</summary>
    [ResourceEntry("GlobalSettings", Description = "phrase: Settings", LastModified = "2018/09/25", Value = "Settings")]
    public string GlobalSettings => this[nameof (GlobalSettings)];

    /// <summary>phrase: Properties</summary>
    [ResourceEntry("Properties", Description = "phrase: Properties", LastModified = "2012/09/17", Value = "Properties")]
    public string Properties => this[nameof (Properties)];

    /// <summary>phrase: Default</summary>
    [ResourceEntry("Default", Description = "phrase: Default", LastModified = "2012/09/17", Value = "Default")]
    public string Default => this[nameof (Default)];

    /// <summary>phrase: Set as default</summary>
    [ResourceEntry("SetDefault", Description = "phrase: Set as default", LastModified = "2012/09/18", Value = "Set as default")]
    public string SetDefault => this[nameof (SetDefault)];

    /// <summary>phrase: DevelopmentContent</summary>
    [ResourceEntry("DevelopmentContent", Description = "phrase: The text displayed after clicking the 'Details' link.", LastModified = "2012/09/20", Value = "Select this option if your site is in process of development and add a testing domain to be able to make a preview of the site while working on it. ")]
    public string DevelopmentContent => this[nameof (DevelopmentContent)];

    /// <summary>phrase: Not shared with any site</summary>
    [ResourceEntry("NotSharedWithAnySite", Description = "phrase: Not shared with any site", LastModified = "2012/09/21", Value = "Not shared with any site")]
    public string NotSharedWithAnySite => this[nameof (NotSharedWithAnySite)];

    /// <summary>phrase: Used in</summary>
    [ResourceEntry("SharedWith", Description = "phrase: Used in", LastModified = "2015/02/16", Value = "USED IN")]
    public string SharedWith => this[nameof (SharedWith)];

    /// <summary>phrase: {0} are used in...</summary>
    [ResourceEntry("TaxonomyUsedIn", Description = "phrase: {0} are used in...", LastModified = "2015/02/05", Value = "{0} are used in...")]
    public string TaxonomyUsedIn => this[nameof (TaxonomyUsedIn)];

    /// <summary>
    /// phrase: "Removing this source will hide it from the interface but all the data that contains will remain in the database.
    ///          You can enable this source later from Advanced settings.
    ///          Are you sure you want to remove this source?"
    /// </summary>
    [ResourceEntry("AreYouSureYouWantToDisableThisDataSource", Description = "phrase: Removing this source will hide it from the interface but all the data that contains will remain in the database.<br/>You can enable this source later from Advanced settings.<br/>Are you sure you want to remove this source?", LastModified = "2012/10/25", Value = "Removing this source will hide it from the interface but all the data that it contains will remain in the database.<br/>You can enable this source later from Advanced settings.<br/>Are you sure you want to remove this source?")]
    public string AreYouSureYouWantToDisableThisDataSource => this[nameof (AreYouSureYouWantToDisableThisDataSource)];

    /// <summary>
    /// phrase for the 'Remove source' confirmation dialog button: Remove source
    /// </summary>
    [ResourceEntry("RemoveSource", Description = "phrase for the 'Remove source' confirmation dialog button: Remove source", LastModified = "2012/10/25", Value = "Remove source")]
    public string RemoveSource => this[nameof (RemoveSource)];

    /// <summary>
    /// phrase for the 'Remove source' confirmation dialog title: Remove a source
    /// </summary>
    [ResourceEntry("RemoveASource", Description = "phrase for the 'Remove source' confirmation dialog title: Remove a source", LastModified = "2012/10/25", Value = "Remove a source")]
    public string RemoveASource => this[nameof (RemoveASource)];

    /// <summary>phrase: How can I use this module in other sites?</summary>
    [ResourceEntry("UseModuleInSitesTitle", Description = "phrase: How can I use this module in other sites?", LastModified = "2012/10/24", Value = "How can I use this module in other sites?")]
    public string UseModuleInSitesTitle => this[nameof (UseModuleInSitesTitle)];

    /// <summary>
    /// phrase: Go to Manage sites &gt; Site actions menu &gt; Configure modules to enable this module for a particular site.
    /// </summary>
    [ResourceEntry("UseModuleInSitesDescription", Description = "phrase: Go to Manage sites > Site actions menu > Configure modules to enable this module for a particular site.", LastModified = "2012/10/24", Value = "Go to Manage sites > Site actions menu > Configure modules to enable this module for a particular site.")]
    public string UseModuleInSitesDescription => this[nameof (UseModuleInSitesDescription)];

    /// <summary>phrase: How can I add languages per sites?</summary>
    [ResourceEntry("LanguagePerSite", Description = "phrase: How can I add languages per site?", LastModified = "2012/10/29", Value = "How can I add languages per site?")]
    public string LanguagePerSite => this[nameof (LanguagePerSite)];

    /// <summary>
    /// phrase: Go to Manage sites &gt; Properties and select the languages you want to be used in the respective site.
    /// </summary>
    [ResourceEntry("LanguagePerSiteDescription", Description = "phrase: Go to Manage sites > Properties and select the languages you want to be used in the respective site.", LastModified = "2012/10/24", Value = "Go to Manage sites > Properties and select the languages you want to be used in the respective site. Note that languages from here will not be automatically added in the existsing sites.")]
    public string LanguagePerSiteDescription => this[nameof (LanguagePerSiteDescription)];

    /// <summary>abbreviation: FAQ</summary>
    [ResourceEntry("Faq", Description = "abbreviation : FAQ", LastModified = "2012/10/24", Value = "FAQ")]
    public string Faq => this[nameof (Faq)];

    [ResourceEntry("EnableSubFolderSiteFallbackTitle", Description = "", LastModified = "2013/07/25", Value = "Enable Domain Site Fallback")]
    public string EnableSubFolderSiteFallbackTitle => this[nameof (EnableSubFolderSiteFallbackTitle)];

    [ResourceEntry("EnableSubFolderSiteFallbackDescription", Description = "", LastModified = "2013/07/25", Value = "If true and no page is found in the resolved sub-folder site, the system will check for a page with the same URL in the domain site, and will throw 404 Page Not Found HTTP exception otherwise")]
    public string EnableSubFolderSiteFallbackDescription => this[nameof (EnableSubFolderSiteFallbackDescription)];

    /// <summary>word: Site</summary>
    /// <value>Site</value>
    [ResourceEntry("Site", Description = "word: Site", LastModified = "2015/01/22", Value = "Site")]
    public string Site => this[nameof (Site)];

    /// <summary>SelectedSites</summary>
    /// <value>Selected sites...</value>
    [ResourceEntry("SelectedSites", Description = "SelectedSites", LastModified = "2015/02/16", Value = "Selected sites...")]
    public string SelectedSites => this[nameof (SelectedSites)];

    [ResourceEntry("SitesAll", Description = "", LastModified = "2015/02/16", Value = "All sites")]
    public string SitesAll => this[nameof (SitesAll)];

    [ResourceEntry("RemoveFromThisSite", Description = "Remove from this site", LastModified = "2021/02/10", Value = "Remove from this site")]
    public string RemoveFromThisSite => this[nameof (RemoveFromThisSite)];

    /// <summary>phrase: Selected sites</summary>
    /// <value>Selected sites</value>
    [ResourceEntry("SelectedSitesNoDots", Description = "phrase: Selected sites", LastModified = "2015/02/20", Value = "Selected sites")]
    public string SelectedSitesNoDots => this[nameof (SelectedSitesNoDots)];

    /// <summary>Multisite Administration Group</summary>
    [ResourceEntry("AdministrationGroupPageTitle", Description = "The title of the Multisite administration group page.", LastModified = "2016/03/24", Value = "Multisite Administration Group")]
    public string AdministrationGroupPageTitle => this[nameof (AdministrationGroupPageTitle)];

    /// <summary>Multisite</summary>
    [ResourceEntry("AdministrationGroupPageUrlName", Description = "The name that appears in the URL.", LastModified = "2016/03/24", Value = "Multisite")]
    public string AdministrationGroupPageUrlName => this[nameof (AdministrationGroupPageUrlName)];

    /// <summary>
    /// Provides user interface for multisite administration group page.
    /// </summary>
    [ResourceEntry("AdministrationGroupPageDescription", Description = "Description of Multisite administration group page.", LastModified = "2016/03/24", Value = "Provides user interface for multisite administration group page.")]
    public string AdministrationGroupPageDescription => this[nameof (AdministrationGroupPageDescription)];

    /// <summary>phrase: Not found</summary>
    /// <value>Not found</value>
    [ResourceEntry("NotFound", Description = "phrase: Not found", LastModified = "2016/06/01", Value = "Not found")]
    public string NotFound => this[nameof (NotFound)];

    /// <summary>
    /// The site properties and modules are configured manually message.
    /// </summary>
    /// <value>The site properties and modules are configured manually</value>
    [ResourceEntry("SiteConfiguredManuallyMessage", Description = "The site properties and modules are configured manually message.", LastModified = "2016/08/04", Value = "The site properties and modules are configured manually")]
    public string SiteConfiguredManuallyMessage => this[nameof (SiteConfiguredManuallyMessage)];

    /// <summary>Configure by deployment button text</summary>
    /// <value>Configure by deployment</value>
    [ResourceEntry("ConfigureByDeploymentBtn", Description = "Configure by deployment button text", LastModified = "2016/08/04", Value = "Configure by deployment")]
    public string ConfigureByDeploymentBtn => this[nameof (ConfigureByDeploymentBtn)];

    /// <summary>
    /// The site properties and modules are configured as a part of the deployment process message
    /// </summary>
    /// <value>The site properties and modules are configured as a part of the deployment process</value>
    [ResourceEntry("SiteConfiguredByDeploymentMessage", Description = "The site properties and modules are configured as a part of the deployment process message", LastModified = "2016/08/04", Value = "The site properties and modules are configured as a part of the deployment process")]
    public string SiteConfiguredByDeploymentMessage => this[nameof (SiteConfiguredByDeploymentMessage)];

    /// <summary>Configure manually button text</summary>
    /// <value>Configure manually</value>
    [ResourceEntry("ConfigureManuallyBtn", Description = "Configure manually button text", LastModified = "2016/08/04", Value = "Configure manually")]
    public string ConfigureManuallyBtn => this[nameof (ConfigureManuallyBtn)];

    /// <summary>Configuration column text value</summary>
    /// <value>Configuration</value>
    [ResourceEntry("Configuration", Description = "Configuration column text value", LastModified = "2016/08/04", Value = "Configuration")]
    public string Configuration => this[nameof (Configuration)];

    /// <summary>Manual configuration mode text</summary>
    /// <value>Manually</value>
    [ResourceEntry("ManualConfigurationMode", Description = "Manual configuration mode text", LastModified = "2016/08/04", Value = "Manually")]
    public string ManualConfigurationMode => this[nameof (ManualConfigurationMode)];

    /// <summary>Configuration by deployment mode text</summary>
    /// <value>By deployment</value>
    [ResourceEntry("ConfigurationByDeploymentMode", Description = "Configuration by deployment mode text", LastModified = "2016/08/04", Value = "By deployment")]
    public string ConfigurationByDeploymentMode => this[nameof (ConfigurationByDeploymentMode)];

    /// <summary>Gets the permissions sites usage single.</summary>
    /// <value>The permissions sites usage single.</value>
    [ResourceEntry("PermissionsSitesUsageSingle", Description = "Permissions used in single site", LastModified = "2016/12/06", Value = "{0} is used in {1} site")]
    public string PermissionsSitesUsageSingle => this[nameof (PermissionsSitesUsageSingle)];

    /// <summary>Gets the permissions sites usage multiple.</summary>
    /// <value>The permissions sites usage multiple.</value>
    [ResourceEntry("PermissionsSitesUsageMultiple", Description = "Permissions used in multiple sites", LastModified = "2016/12/06", Value = "{0} is used in {1} sites")]
    public string PermissionsSitesUsageMultiple => this[nameof (PermissionsSitesUsageMultiple)];

    /// <summary>Error message: Invalid site name.</summary>
    [ResourceEntry("InvalidSiteName", Description = "Invalid site name.", LastModified = "2017/08/03", Value = "Invalid site name.")]
    public string InvalidSiteName => this[nameof (InvalidSiteName)];

    /// <summary>Error message: Invalid domain name.</summary>
    [ResourceEntry("InvalidDomainName", Description = "Invalid domain name.", LastModified = "2017/08/03", Value = "Invalid domain name.")]
    public string InvalidDomainName => this[nameof (InvalidDomainName)];

    /// <summary>Error message: Select at least one source.</summary>
    [ResourceEntry("SourcesAreNotSelectedError", Description = "Select at least one source.", LastModified = "2020/02/18", Value = "Select at least one source.")]
    public string SourcesAreNotSelectedError => this[nameof (SourcesAreNotSelectedError)];

    /// <summary>Error message: Source name already exists</summary>
    [ResourceEntry("SourceAlreadyExistsError", Description = "Source name already exists", LastModified = "2025/02/18", Value = "Source name already exists")]
    public string SourceAlreadyExistsError => this[nameof (SourceAlreadyExistsError)];

    /// <summary>
    /// Error message: User group with this name already exists
    /// </summary>
    [ResourceEntry("UserGroupAlreadyExistsError", Description = "User group with this name already exists", LastModified = "2021/02/16", Value = "User group with this name already exists")]
    public string UserGroupAlreadyExistsError => this[nameof (UserGroupAlreadyExistsError)];

    /// <summary>Error message: Source name cannot be empty.</summary>
    [ResourceEntry("EmptySourceError", Description = "Source name cannot be empty.", LastModified = "2025/02/18", Value = "Source name cannot be empty.")]
    public string EmptySourceError => this[nameof (EmptySourceError)];

    /// <summary>
    /// Error message: Source name must be less than 255 characters
    /// </summary>
    /// <value>Source name must be less than 255 characters</value>
    [ResourceEntry("SourceNameLengthMessage", Description = "Source name must be less than 255 characters", LastModified = "2020/02/26", Value = "Source name must be less than 255 characters")]
    public string SourceNameLengthMessage => this[nameof (SourceNameLengthMessage)];

    /// <summary>Add source</summary>
    [ResourceEntry("AddSource", Description = "Add source", LastModified = "2020/02/20", Value = "Add source")]
    public string AddSource => this[nameof (AddSource)];

    /// <summary>Create new source</summary>
    [ResourceEntry("CreateNewSource", Description = "Create new source", LastModified = "2020/02/21", Value = "Create new source")]
    public string CreateNewSource => this[nameof (CreateNewSource)];

    /// <summary>Create new source for</summary>
    [ResourceEntry("CreateNewSourceFor", Description = "Create new source for {0}", LastModified = "2020/02/21", Value = "Create new source for {0}")]
    public string CreateNewSourceFor => this[nameof (CreateNewSourceFor)];

    /// <summary>Source name</summary>
    [ResourceEntry("SourceName", Description = "Source name", LastModified = "2020/03/04", Value = "Source name")]
    public string SourceName => this[nameof (SourceName)];

    /// <summary>International events</summary>
    [ResourceEntry("SourceNameExample", Description = "International events", LastModified = "2020/03/04", Value = "International events")]
    public string SourceNameExample => this[nameof (SourceNameExample)];

    /// <summary>
    /// Phrase: Create a new source only if you need a strong separation between groups of content items in this module.
    /// </summary>
    [ResourceEntry("SourceInformation", Description = "Phrase: Create a new source only if you need a strong separation between groups of content items in this module.", LastModified = "2020/03/04", Value = "Create a new source only if you need a strong separation between groups of content items in this module.")]
    public string SourceInformation => this[nameof (SourceInformation)];

    /// <summary>External Link: Source information</summary>
    [ResourceEntry("ExternalLinkSourceInformation", Description = "External Link: Source information", LastModified = "2020/03/05", Value = "https://www.progress.com/documentation/sitefinity-cms/share-content-providers-of-modules")]
    public string ExternalLinkSourceInformation => this[nameof (ExternalLinkSourceInformation)];

    /// <summary>
    /// Phrase: Create new user groups only if you need different sites to be managed by different users.
    /// </summary>
    [ResourceEntry("UserGroupInformation", Description = "Phrase: Create new user groups only if you need different sites to be managed by different users.", LastModified = "2021/02/09", Value = "Create new user groups only if you need different sites to be managed by different users.")]
    public string UserGroupInformation => this[nameof (UserGroupInformation)];

    /// <summary>External Link: User group information</summary>
    [ResourceEntry("ExternalLinkUserGroupInformation", Description = "External Link: User group information", LastModified = "2021/02/09", Value = "https://www.progress.com/documentation/sitefinity-cms/user-groups")]
    public string ExternalLinkUserGroupInformation => this[nameof (ExternalLinkUserGroupInformation)];
  }
}
