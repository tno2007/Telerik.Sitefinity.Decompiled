// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.PackagingResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Packaging
{
  /// <summary>Resource class for the Packaging module</summary>
  [ObjectInfo(typeof (PackagingResources), Description = "PackagingResourcesDescription", Title = "PackagingResourcesTitle")]
  internal class PackagingResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public PackagingResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.PackagingResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider">The <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public PackagingResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Gets the title of this class.</summary>
    [ResourceEntry("PackagingResourcesTitle", Description = "The title of this class.", LastModified = "2015/03/18", Value = "Packaging")]
    public string PackagingResourcesTitle => this[nameof (PackagingResourcesTitle)];

    /// <summary>Gets the description of this class.</summary>
    [ResourceEntry("PackagingResourcesDescription", Description = "The description of this class.", LastModified = "2015/03/18", Value = "Contains localizable resources for Packaging module.")]
    public string PackagingResourcesDescription => this[nameof (PackagingResourcesDescription)];

    /// <summary>Gets the Packaging settings configuration title.</summary>
    [ResourceEntry("PackagingsConfigCaption", Description = "The title of this class.", LastModified = "2015/12/08", Value = "Packaging settings")]
    public string PackagingsConfigTitle => this["PackagingsConfigCaption"];

    /// <summary>
    /// Gets the Packaging settings configuration description.
    /// </summary>
    [ResourceEntry("PackagingsConfigDescription", Description = "The description of this class.", LastModified = "2015/12/08", Value = "Resource strings for Packaging settings.")]
    public string PackagingsConfigDescription => this[nameof (PackagingsConfigDescription)];

    /// <summary>Gets the packaging import for archive page title</summary>
    /// <value>Import from archive</value>
    [ResourceEntry("ImportFromArchivePageTitle", Description = "Gets the packaging import from archive page title", LastModified = "2016/02/11", Value = "Import")]
    public string ImportFromArchivePageTitle => this[nameof (ImportFromArchivePageTitle)];

    /// <summary>Gets the packaging import for archive page header</summary>
    /// <value>Import data</value>
    [ResourceEntry("ImportFromArchivePageHeader", Description = "Gets the packaging import from archive page header", LastModified = "2016/06/12", Value = "Import data")]
    public string ImportFromArchivePageHeader => this[nameof (ImportFromArchivePageHeader)];

    /// <summary>Gets the packaging import for archive page url</summary>
    /// <value>Import from archive</value>
    [ResourceEntry("ImportFromArchivePageUrl", Description = "Gets the packaging import from archive page url", LastModified = "2016/01/12", Value = "Archive")]
    public string ImportFromArchivePageUrl => this[nameof (ImportFromArchivePageUrl)];

    /// <summary>Gets the archive export page title</summary>
    /// <value>text: Download ZIP file</value>
    [ResourceEntry("ExportForArchivePageTitle", Description = "Gets the archive export page title", LastModified = "2016/02/10", Value = "Download ZIP file")]
    public string ExportForArchivePageTitle => this[nameof (ExportForArchivePageTitle)];

    /// <summary>Gets the packaging export page description</summary>
    /// <value>Export the structure of Sitefinity modules (News, Blogs, Events, etc. or modules created with Module builder).</value>
    [ResourceEntry("ExportForArchivePageDescription", Description = "Gets the packaging export for archive page description", LastModified = "2016/01/29", Value = "Export the structure and content of Sitefinity modules (News, Blogs, Events, etc. or modules created with Module builder) into an archive.")]
    public string ExportForArchivePageDescription => this[nameof (ExportForArchivePageDescription)];

    /// <summary>Gets packaging module page URL name</summary>
    /// <value>text: Export-for-deployment</value>
    [ResourceEntry("ExportForArchivePageUrlName", Description = "Packaging for archive module page URL name", LastModified = "2016/02/10", Value = "Export-for-archive")]
    public string ExportForArchivePageUrlName => this[nameof (ExportForArchivePageUrlName)];

    /// <summary>Gets the packaging export page title</summary>
    /// <value>text: Export for deployment</value>
    [ResourceEntry("PackagingModulePageTitle", Description = "Gets the packaging export page title", LastModified = "2016/02/10", Value = "Export for deployment")]
    public string PackagingModulePageTitle => this[nameof (PackagingModulePageTitle)];

    /// <summary>Gets the packaging export page title</summary>
    /// <value>text: Export modules for deployment</value>
    [ResourceEntry("ExportForDeploymentPageTitle", Description = "Gets the packaging export page title", LastModified = "2016/02/10", Value = "Export modules for deployment")]
    public string ExportForDeploymentPageTitle => this[nameof (ExportForDeploymentPageTitle)];

    /// <summary>Gets the packaging export page description</summary>
    /// <value>Export the structure of Sitefinity modules (News, Blogs, Events, etc. or modules created with Module builder).</value>
    [ResourceEntry("PackagingModulePageDescription", Description = "Gets the packaging export page description", LastModified = "2016/11/11", Value = "Export the structure of Sitefinity modules (News, Blogs, Events, etc. or modules created with Module builder).")]
    public string PackagingModulePageDescription => this[nameof (PackagingModulePageDescription)];

    /// <summary>Gets packaging module page URL name</summary>
    /// <value>text: Export-for-deployment</value>
    [ResourceEntry("PackagingModulePageUrlName", Description = "Packaging module page URL name", LastModified = "2016/02/10", Value = "Deployment")]
    public string PackagingModulePageUrlName => this[nameof (PackagingModulePageUrlName)];

    /// <summary>Gets the header in export for deployment page</summary>
    /// <value>Export Sitefinity modules for deployment</value>
    [ResourceEntry("ExportForDeploymentPageHeader", Description = "Gets the header in export for deployment page", LastModified = "2016/02/01", Value = "Export Sitefinity modules for deployment")]
    public string ExportForDeploymentPageHeader => this[nameof (ExportForDeploymentPageHeader)];

    /// <summary>Gets the description in export for deployment page</summary>
    /// <value>Sitefinity Export</value>
    [ResourceEntry("ExportForDeploymentPageDescription", Description = "Gets the description in export for deployment page", LastModified = "2016/02/01", Value = "All modules fields and configurations, related taxonomies and widget templates from the database will be exported for the purpose of deployment process.")]
    public string ExportForDeploymentPageDescription => this[nameof (ExportForDeploymentPageDescription)];

    /// <summary>Gets "Learn more" phrase</summary>
    /// <value>Learn more</value>
    [ResourceEntry("LearnMore", Description = "Learn more phrase", LastModified = "2016/02/11", Value = "Learn more")]
    public string LearnMore => this[nameof (LearnMore)];

    /// <summary>Gets External Link: Active and deactivate an add-on</summary>
    /// <value>Learn more</value>
    [ResourceEntry("ExternalLinkActiveDeactiveAddon", Description = "External Link: Active and deactivate an add-on", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/activate-and-deactivate-add-ons")]
    public string ExternalLinkActiveDeactiveAddon => this[nameof (ExternalLinkActiveDeactiveAddon)];

    /// <summary>
    /// Gets External Link: Continuous delivery export and import of data structures
    /// </summary>
    /// <value>Learn more</value>
    [ResourceEntry("ExternalLinkContinuousDeliveryExportImportDataStructures", Description = "External Link: Continuous delivery export and import of data structures", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/export-and-deploy-code-changes")]
    public string ExternalLinkContinuousDeliveryExportImportDataStructures => this[nameof (ExternalLinkContinuousDeliveryExportImportDataStructures)];

    /// <summary>Gets "All modules will be exported to..." phrase</summary>
    /// <value>All modules will be exported to...</value>
    [ResourceEntry("AllModulesWillBeExportedTo", Description = "All modules will be exported to... phrase", LastModified = "2016/01/12", Value = "All modules will be exported to...")]
    public string AllModulesWillBeExportedTo => this[nameof (AllModulesWillBeExportedTo)];

    /// <summary>Gets "Data have been successfully exported to" phrase</summary>
    /// <value>Data have been successfully exported to</value>
    [ResourceEntry("TheModulesHaveBeenSuccessfullyExported", Description = "Data have been successfully exported to phrase", LastModified = "2016/14/12", Value = "Data have been successfully exported to")]
    public string TheModulesHaveBeenSuccessfullyExported => this[nameof (TheModulesHaveBeenSuccessfullyExported)];

    /// <summary>
    /// Gets "Download will start automatically in 5 sec, but if not, " phrase
    /// </summary>
    /// <value>Download will start automatically in 5 sec, but if not, </value>
    [ResourceEntry("DownloadWillBeginShortly", Description = "Download will start automatically in 5 sec, but if not,  phrase", LastModified = "2016/14/12", Value = "Download will start automatically in 5 sec, but if not, ")]
    public string DownloadWillBeginShortly => this[nameof (DownloadWillBeginShortly)];

    /// <summary>Gets "click here" phrase</summary>
    /// <value>click here</value>
    [ResourceEntry("ClickHereMessage", Description = "click here phrase", LastModified = "2016/14/12", Value = "click here")]
    public string ClickHereMessage => this[nameof (ClickHereMessage)];

    /// <summary>Gets "Data have been successfully exported" phrase</summary>
    /// <value>Data have been successfully exported</value>
    [ResourceEntry("SuccessfullExportMessage", Description = "Data have been successfully exported", LastModified = "2020/15/06", Value = "Data have been successfully exported")]
    public string SuccessfullExportMessage => this[nameof (SuccessfullExportMessage)];

    /// <summary>
    /// Gets "Data have not been successfully exported" phrase
    /// </summary>
    /// <value>Data have not been successfully exported</value>
    [ResourceEntry("TheModulesHaveNOTBeenSuccessfullyExported", Description = "Data have not been successfully exported phrase", LastModified = "2016/02/11", Value = "Data have not been successfully exported")]
    public string TheModulesHaveNOTBeenSuccessfullyExported => this[nameof (TheModulesHaveNOTBeenSuccessfullyExported)];

    /// <summary>Gets "Data have been successfully imported" phrase</summary>
    /// <value>Data have been successfully imported</value>
    [ResourceEntry("TheModulesHaveBeenSuccessfullyImported", Description = "Data have been successfully imported phrase", LastModified = "2016/02/11", Value = "Data have been successfully imported")]
    public string TheModulesHaveBeenSuccessfullyImported => this[nameof (TheModulesHaveBeenSuccessfullyImported)];

    /// <summary>
    /// Gets "Data have not been successfully imported" phrase
    /// </summary>
    /// <value>Data have not been successfully imported</value>
    [ResourceEntry("TheModulesHaveNOTBeenSuccessfullyImported", Description = "Data have not been successfully imported phrase", LastModified = "2016/02/11", Value = "Data have not been successfully imported")]
    public string TheModulesHaveNOTBeenSuccessfullyImported => this[nameof (TheModulesHaveNOTBeenSuccessfullyImported)];

    /// <summary>
    /// Gets "Selected file exceeds the 2Gb upload size limit" phrase
    /// </summary>
    /// <value>Selected file exceeds the 2Gb upload size limit</value>
    [ResourceEntry("FileSizeExceedsLimit", Description = "Selected file exceeds the 2Gb upload size limit phrase", LastModified = "2016/12/07", Value = "Selected file exceeds the 2Gb upload size limit")]
    public string FileSizeExceedsLimit => this[nameof (FileSizeExceedsLimit)];

    /// <summary>Gets "Invalid file format" phrase</summary>
    /// <value>Invalid file format</value>
    [ResourceEntry("InvalidFileFormat", Description = "Invalid file format phrase", LastModified = "2016/12/07", Value = "Invalid file format")]
    public string InvalidFileFormat => this[nameof (InvalidFileFormat)];

    /// <summary>Gets "Import" phrase</summary>
    /// <value>Export phrase</value>
    [ResourceEntry("Import", Description = "Import phrase", LastModified = "2016/02/11", Value = "Import")]
    public string Import => this[nameof (Import)];

    /// <summary>Gets "Export" phrase</summary>
    /// <value>Export phrase</value>
    [ResourceEntry("Export", Description = "Export phrase", LastModified = "2016/02/11", Value = "Export")]
    public string Export => this[nameof (Export)];

    /// <summary>Gets "Export / Import" phrase</summary>
    /// <value>Export / Import phrase</value>
    [ResourceEntry("PackagingNodeTitle", Description = "Export / Import phrase", LastModified = "2016/02/11", Value = "Export / Import")]
    public string PackagingNodeTitle => this[nameof (PackagingNodeTitle)];

    /// <summary>Gets "Packaging" phrase</summary>
    /// <value>Packaging phrase</value>
    [ResourceEntry("PackagingNodeUrl", Description = "Packaging phrase", LastModified = "2016/02/11", Value = "Packaging")]
    public string PackagingNodeUrl => this[nameof (PackagingNodeUrl)];

    /// <summary>Gets "Export ZIP file" phrase</summary>
    /// <value>Export ZIP file phrase</value>
    [ResourceEntry("ExportForArchive", Description = "Export ZIP file phrase", LastModified = "2016/10/21", Value = "Export ZIP file")]
    public string ExportForArchive => this[nameof (ExportForArchive)];

    /// <summary>Gets the header in export page</summary>
    /// <value>Export Sitefinity modules for deployment</value>
    [ResourceEntry("ExportPageHeader", Description = "Gets the header in export page", LastModified = "2016/06/20", Value = "What do you want to export from this site?")]
    public string ExportPageHeader => this[nameof (ExportPageHeader)];

    /// <summary>Gets the description in export for deployment page</summary>
    /// <value>Sitefinity Export</value>
    [ResourceEntry("ExportPageDescription", Description = "Gets the description in export page", LastModified = "2016/01/12", Value = "You can export modules and content from the current site to create an add-on")]
    public string ExportPageDescription => this[nameof (ExportPageDescription)];

    /// <summary>Gets the description in export for addon page</summary>
    /// <value>Sitefinity Export</value>
    [ResourceEntry("WhatToExportInAddonMessage", Description = "Gets the description in export page", LastModified = "2016/01/12", Value = "What to export?")]
    public string WhatToExportInAddonMessage => this[nameof (WhatToExportInAddonMessage)];

    /// <summary>
    /// Gets "Export Sitefinity for deployment purposes" phrase
    /// </summary>
    /// <value>Export Sitefinity for deployment purposes</value>
    [ResourceEntry("ExportSitefinityForDeploymentPhrase", Description = "Export Sitefinity for deployment purposes phrase", LastModified = "2016/06/13", Value = "Export Sitefinity for deployment purposes")]
    public string ExportSitefinityForDeploymentPhrase => this[nameof (ExportSitefinityForDeploymentPhrase)];

    /// <summary>Gets "Data will be exported to..." phrase</summary>
    /// <value>Data will be exported to...</value>
    [ResourceEntry("ExportFolderLabel", Description = "Where the data will be exported to phrase", LastModified = "2016/06/13", Value = "Data will be exported to...")]
    public string ExportFolderLabel => this[nameof (ExportFolderLabel)];

    /// <summary>Gets "Folder name" phrase</summary>
    /// <value>Folder name</value>
    [ResourceEntry("ExportFolderNameLabel", Description = "Folder name phrase", LastModified = "2016/06/13", Value = "Folder name")]
    public string ExportFolderNameLabel => this[nameof (ExportFolderNameLabel)];

    /// <summary>Gets "Back to Export" phrase</summary>
    /// <value>Back to Export</value>
    [ResourceEntry("BackToExport", Description = "Back to Export phrase", LastModified = "2016/02/11", Value = "Back to Export")]
    public string BackToExport => this[nameof (BackToExport)];

    /// <summary>Gets "Import another file" phrase</summary>
    /// <value>Import another file</value>
    [ResourceEntry("ImportAnotherFile", Description = "Import another file phrase", LastModified = "2016/06/12", Value = "Import another file")]
    public string ImportAnotherFile => this[nameof (ImportAnotherFile)];

    /// <summary>Gets "Continue anyway" phrase</summary>
    /// <value>Continue anyway</value>
    [ResourceEntry("ContinueAnyway", Description = "Continue anyway phrase", LastModified = "2016/07/12", Value = "Continue anyway")]
    public string ContinueAnyway => this[nameof (ContinueAnyway)];

    /// <summary>
    /// Gets "If you have opened a particular node by URL you may proceed using the feature." phrase
    /// </summary>
    /// <value>If you have opened a particular node by URL you may proceed using the feature.</value>
    [ResourceEntry("LoadBalancingModeWarning", Description = "Operation may fail in case your App_Data folder is not stored on a shared location. phrase", LastModified = "2019/11/27", Value = "Operation may fail in case your App_Data folder is not stored on a shared location.")]
    public string LoadBalancingModeWarning => this[nameof (LoadBalancingModeWarning)];

    /// <summary>Gets "To export you need write permission for" phrase</summary>
    /// <value>To export you need write permission for</value>
    [ResourceEntry("ExportNoFileSystemPermissionsWarning", Description = "To export you need write permission for phrase", LastModified = "2016/14/12", Value = "To export you need write permission for")]
    public string ExportNoFileSystemPermissionsWarning => this[nameof (ExportNoFileSystemPermissionsWarning)];

    /// <summary>Gets "To import you need write permission for" phrase</summary>
    /// <value>To import you need write permission for</value>
    [ResourceEntry("ImportNoFileSystemPermissionsWarning", Description = "To import you need write permission for phrase", LastModified = "2016/14/12", Value = "To import you need write permission for")]
    public string ImportNoFileSystemPermissionsWarning => this[nameof (ImportNoFileSystemPermissionsWarning)];

    /// <summary>Gets "Export again" phrase</summary>
    /// <value>Export again</value>
    [ResourceEntry("ExportAgain", Description = "Export again phrase", LastModified = "2016/02/11", Value = "Export again")]
    public string ExportAgain => this[nameof (ExportAgain)];

    /// <summary>Gets "Back to Import" phrase</summary>
    /// <value>Back to Export</value>
    [ResourceEntry("BackToImportOfArchive", Description = "Back to Import phrase", LastModified = "2016/02/11", Value = "Back to Import")]
    public string BackToImportOfArchive => this[nameof (BackToImportOfArchive)];

    /// <summary>Gets the value of the Add-ons page title</summary>
    /// <value>Add-ons</value>
    [ResourceEntry("AddOns", Description = "Add-ons page title", LastModified = "2016/06/03", Value = "Add-ons")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string AddOns => this[nameof (AddOns)];

    /// <summary>Gets the value of the Url of the add-ons page</summary>
    /// <value>add-ons</value>
    [ResourceEntry("AddOnsUrl", Description = "Url of the add-ons page", LastModified = "2016/06/03", Value = "add-ons")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string AddOnsUrl => this[nameof (AddOnsUrl)];

    /// <summary>Gets the value of the Url of the add-on page</summary>
    /// <value>add-ons</value>
    [ResourceEntry("AddOnUrl", Description = "Url of the add-on page", LastModified = "2016/06/13", Value = "add-on")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string AddOnUrl => this[nameof (AddOnUrl)];

    /// <summary>Gets the value of the Add-on page title</summary>
    /// <value>Add-on</value>
    [ResourceEntry("AddOn", Description = "Add-on page title", LastModified = "2016/06/15", Value = "Add-on")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string AddOn => this[nameof (AddOn)];

    /// <summary>Gets "All add-ons" phrase</summary>
    /// <value>All add-ons</value>
    [ResourceEntry("BackToAllAddons", Description = "All add-ons phrase", LastModified = "2016/06/20", Value = "All add-ons")]
    public string BackToAllAddons => this[nameof (BackToAllAddons)];

    /// <summary>Gets "Module" phrase</summary>
    /// <value>Module</value>
    [ResourceEntry("SelectAllModulesLabel", Description = "Module phrase", LastModified = "2016/06/20", Value = "Module")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string SelectAllModulesLabel => this[nameof (SelectAllModulesLabel)];

    /// <summary>Gets "No add-ons have been installed yet" phrase</summary>
    /// <value>No add-ons have been installed yet</value>
    [ResourceEntry("NoInstalledAddons", Description = "No add-ons have been installed yet phrase", LastModified = "2016/06/20", Value = "No add-ons have been installed yet")]
    public string NoInstalledAddons => this[nameof (NoInstalledAddons)];

    /// <summary>Gets "How to install an add-on?" phrase</summary>
    /// <value>How to install an add-on?</value>
    [ResourceEntry("HowToInstallAddon", Description = "How to install an add-on? phrase", LastModified = "2016/06/20", Value = "How to install an add-on?")]
    public string HowToInstallAddon => this[nameof (HowToInstallAddon)];

    /// <summary>Gets External Link: How to install an add-on</summary>
    [ResourceEntry("ExternalLinkHowToInstallAddon", Description = "External Link: How to install an add-on", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/install-add-ons")]
    public string ExternalLinkHowToInstallAddon => this[nameof (ExternalLinkHowToInstallAddon)];

    /// <summary>Gets "Not imported to" phrase</summary>
    /// <value>Not imported to</value>
    [ResourceEntry("NotImportedTo", Description = "Not imported to phrase", LastModified = "2016/07/08", Value = "Not imported to")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string NotImportedTo => this[nameof (NotImportedTo)];

    /// <summary>Gets "Imported to" phrase</summary>
    /// <value>Imported to</value>
    [ResourceEntry("ImportedTo", Description = "Imported to phrase", LastModified = "2016/07/08", Value = "Imported to")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ImportedTo => this[nameof (ImportedTo)];

    /// <summary>Gets "Imported" phrase</summary>
    /// <value>Imported</value>
    [ResourceEntry("Imported", Description = "Imported phrase", LastModified = "2016/07/13", Value = "Imported")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string Imported => this[nameof (Imported)];

    /// <summary>Gets "Not imported" phrase</summary>
    /// <value>Not imported</value>
    [ResourceEntry("NotImported", Description = "Not imported phrase", LastModified = "2016/07/13", Value = "Not imported")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string NotImported => this[nameof (NotImported)];

    /// <summary>Gets "Structures and data" phrase</summary>
    /// <value>Structures and data</value>
    [ResourceEntry("StructuresAndData", Description = "Structures and data phrase", LastModified = "2016/07/06", Value = "Structures and data")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string StructuresAndData => this[nameof (StructuresAndData)];

    /// <summary>Gets "Structure only" phrase</summary>
    /// <value>Structure only</value>
    [ResourceEntry("StructureOnly", Description = "Structure only phrase", LastModified = "2016/07/06", Value = "Structure only")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string StructureOnly => this[nameof (StructureOnly)];

    /// <summary>Gets "Enter valid folder name" phrase</summary>
    /// <value>Enter valid folder name</value>
    [ResourceEntry("ValidFolderNameMessage", Description = "Enter valid folder name phrase", LastModified = "2016/07/06", Value = "Enter valid folder name")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ValidFolderNameMessage => this[nameof (ValidFolderNameMessage)];

    /// <summary>Gets "(Custom only)" phrase</summary>
    /// <value>(Custom only)</value>
    [ResourceEntry("CustomOnlyMessage", Description = "Phrase that indicates only custom items will be used", LastModified = "2016/07/28", Value = "(Custom only)")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string CustomOnlyMessage => this[nameof (CustomOnlyMessage)];

    /// <summary>Gets "Activation in progress... Please wait" phrase</summary>
    /// <value>Activation in progress... Please wait</value>
    [ResourceEntry("ActivationInProgressMessage", Description = "Phrase that indicates the activation of an addon is in progress", LastModified = "2016/07/28", Value = "Activation in progress... Please wait")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ActivationInProgressMessage => this[nameof (ActivationInProgressMessage)];

    /// <summary>Gets "Deactivation in progress... Please wait" phrase</summary>
    /// <value>Deactivation in progress... Please wait</value>
    [ResourceEntry("DeactivationInProgressMessage", Description = "Phrase that indicates the deactivation of an addon is in progress", LastModified = "2016/07/28", Value = "Deactivation in progress... Please wait")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeactivationInProgressMessage => this[nameof (DeactivationInProgressMessage)];

    /// <summary>Gets "Activate for this site" phrase</summary>
    /// <value>Activate for this site</value>
    [ResourceEntry("ActivateForThisSiteMessage", Description = "Activate for this site phrase", LastModified = "2016/07/28", Value = "Activate for this site")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ActivateForThisSiteMessage => this[nameof (ActivateForThisSiteMessage)];

    /// <summary>Gets "Deactivate for this site" phrase</summary>
    /// <value>Deactivate for this site</value>
    [ResourceEntry("DeactivateForThisSiteMessage", Description = "Deactivate for this site phrase", LastModified = "2016/07/28", Value = "Deactivate for this site")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeactivateForThisSiteMessage => this[nameof (DeactivateForThisSiteMessage)];

    /// <summary>Gets "This add-on contains..." phrase</summary>
    /// <value>This add-on contains...</value>
    [ResourceEntry("AddonContainsMessage", Description = "This add-on contains... phrase", LastModified = "2016/07/28", Value = "This add-on contains...")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string AddonContainsMessage => this[nameof (AddonContainsMessage)];

    /// <summary>Gets "(activated for all sites)" phrase</summary>
    /// <value>(activated for all sites)</value>
    [ResourceEntry("ActivatedForAllSitesMessage", Description = "(activated for all sites) phrase", LastModified = "2016/07/28", Value = "(activated for all sites)")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ActivatedForAllSitesMessage => this[nameof (ActivatedForAllSitesMessage)];

    /// <summary>Gets "Content and pages" phrase</summary>
    /// <value>Content and pages</value>
    [ResourceEntry("ContentAndPages", Description = "Content and pages phrase", LastModified = "2016/07/28", Value = "Content and pages")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ContentAndPages => this[nameof (ContentAndPages)];

    /// <summary>Gets "(optional)" phrase</summary>
    /// <value>(optional)</value>
    [ResourceEntry("OptionalMessage", Description = "(optional) phrase", LastModified = "2016/07/28", Value = "(optional)")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string OptionalMessage => this[nameof (OptionalMessage)];

    /// <summary>
    /// Gets "Import sample data to this site to help you getting started. You can delete imported data anytime" phrase
    /// </summary>
    /// <value>Import sample data to this site to help you getting started. You can delete imported data anytime</value>
    [ResourceEntry("ImportExampleDataMessage", Description = "Import sample data to this site to help you getting started. You can delete imported data anytime phrase", LastModified = "2016/08/02", Value = "Import sample data to this site to help you getting started. You can delete imported data anytime")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ImportExampleDataMessage => this[nameof (ImportExampleDataMessage)];

    /// <summary>Gets "Import sample content and pages" phrase</summary>
    /// <value>Import sample content and pages</value>
    [ResourceEntry("ImportExampleContentMessage", Description = "Import sample content and pages phrase", LastModified = "2016/08/02", Value = "Import sample content and pages")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ImportExampleContentMessage => this[nameof (ImportExampleContentMessage)];

    /// <summary>
    /// Gets "You cannot import the data to this site, because it is already imported to " phrase
    /// </summary>
    /// <value>You cannot import the data to this site, because it is already imported to </value>
    [ResourceEntry("DataAlreadyImportedMessage", Description = "You cannot import the data to this site, because it is already imported to  phrase", LastModified = "2016/07/28", Value = "You cannot import the data to this site, because it is already imported to ")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DataAlreadyImportedMessage => this[nameof (DataAlreadyImportedMessage)];

    /// <summary>Gets "Delete imported content and pages" phrase</summary>
    /// <value>Delete imported content and pages</value>
    [ResourceEntry("DeleteImportedDataAndPagesMessage", Description = "Delete imported content and pages phrase", LastModified = "2016/08/02", Value = "Delete imported content and pages")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeleteImportedDataAndPagesMessage => this[nameof (DeleteImportedDataAndPagesMessage)];

    /// <summary>Gets "Delete imported content and pages" phrase</summary>
    /// <value>Delete imported content and pages</value>
    [ResourceEntry("DeleteImportedContentAndPagesMessage", Description = "Delete imported content and pages phrase", LastModified = "2016/07/28", Value = "Delete imported content and pages")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeleteImportedContentAndPagesMessage => this[nameof (DeleteImportedContentAndPagesMessage)];

    /// <summary>
    /// Gets "Imported sample content and pages will be deleted and any changes will be lost." phrase
    /// </summary>
    /// <value>Imported sample content and pages will be deleted and any changes will be lost.</value>
    [ResourceEntry("ImportedContentDeleteMessage", Description = "Imported sample content and pages will be deleted and any changes will be lost. phrase", LastModified = "2016/08/02", Value = "Imported sample content and pages will be deleted and any changes will be lost.")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ImportedContentDeleteMessage => this[nameof (ImportedContentDeleteMessage)];

    /// <summary>Gets "Importing in progress... Please wait" phrase</summary>
    /// <value>Importing in progress... Please wait</value>
    [ResourceEntry("ImportInProgressMessage", Description = "Importing in progress... Please wait phrase", LastModified = "2016/07/28", Value = "Importing in progress... Please wait")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ImportInProgressMessage => this[nameof (ImportInProgressMessage)];

    /// <summary>Gets "Deleting in progress... Please wait" phrase</summary>
    /// <value>Deleting in progress... Please wait</value>
    [ResourceEntry("DeleteInProgressMessage", Description = "Deleting in progress... Please wait phrase", LastModified = "2016/07/28", Value = "Deleting in progress... Please wait")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeleteInProgressMessage => this[nameof (DeleteInProgressMessage)];

    /// <summary>
    /// Gets "Are you sure you want to deactivate this add-on?" phrase
    /// </summary>
    /// <value>Are you sure you want to deactivate this add-on?</value>
    [ResourceEntry("DeactivateAddonPrompt", Description = "Are you sure you want to deactivate this add-on? phrase", LastModified = "2016/07/28", Value = "Are you sure you want to deactivate this add-on?")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeactivateAddonPrompt => this[nameof (DeactivateAddonPrompt)];

    /// <summary>Gets "Activate Add-on" phrase</summary>
    /// <value>Activate Add-on</value>
    [ResourceEntry("ActivateAddon", Description = "Activate Add-on phrase", LastModified = "2016/07/28", Value = "Activate Add-on")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ActivateAddon => this[nameof (ActivateAddon)];

    /// <summary>Gets "Deactivate Add-on" phrase</summary>
    /// <value>Deactivate Add-on</value>
    [ResourceEntry("DeactivateAddon", Description = "Deactivate Add-on phrase", LastModified = "2016/07/28", Value = "Deactivate Add-on")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeactivateAddon => this[nameof (DeactivateAddon)];

    /// <summary>Gets "This add-on will be activated to " phrase</summary>
    /// <value>This add-on will be activated to </value>
    [ResourceEntry("AddonWillBeActivatedMessage", Description = "This add-on will be activated to  phrase", LastModified = "2016/07/28", Value = "This add-on will be activated to ")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string AddonWillBeActivatedMessage => this[nameof (AddonWillBeActivatedMessage)];

    /// <summary>Gets "This add-on will be deactivated for " phrase</summary>
    /// <value>This add-on will be deactivated for </value>
    [ResourceEntry("AddonWillBeDeactivatedMessage", Description = "This add-on will be deactivated for  phrase", LastModified = "2016/07/28", Value = "This add-on will be deactivated for ")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string AddonWillBeDeactivatedMessage => this[nameof (AddonWillBeDeactivatedMessage)];

    /// <summary>
    /// Gets Message shown when an addon is activated but it's folder is missing
    /// </summary>
    /// <value>The add-on files are missing or corrupted. If deactivated it cannot be activated again</value>
    [ResourceEntry("DeletedAddonFolderOfActivatedAddon", Description = "Gets Message shown when an addon is activated but it's folder is missing", LastModified = "2016/08/10", Value = "The add-on files are missing or corrupted. If deactivated it cannot be activated again")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeletedAddonFolderOfActivatedAddon => this[nameof (DeletedAddonFolderOfActivatedAddon)];

    /// <summary>
    /// Gets Message shown when an addon is not activated and its files are missing or corrupted
    /// </summary>
    /// <value>This add-on cannot be activated, because its files are missing or corrupted</value>
    [ResourceEntry("DeletedAddonFolderOfInactiveAddon", Description = "Gets Message shown when an addon is not activated and it's folder is missing", LastModified = "2016/08/10", Value = "This add-on cannot be activated, because its files are missing or corrupted")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string DeletedAddonFolderOfInactiveAddon => this[nameof (DeletedAddonFolderOfInactiveAddon)];

    /// <summary>
    /// Gets Error message when deactivating add-on has failed
    /// </summary>
    /// <value>This add-on has not been deactivated successfully. Please check the log for further information</value>
    [ResourceEntry("ErrorDeactivating", Description = "Gets Error message when deactivating add-on has failed", LastModified = "2016/07/29", Value = "This add-on has not been deactivated successfully. Please check the log for further information")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ErrorDeactivating => this[nameof (ErrorDeactivating)];

    /// <summary>Gets Error message when activating add-on has failed</summary>
    /// <value>This add-on has not been activated successfully. Please check the log for further information</value>
    [ResourceEntry("ErrorActivating", Description = "Error message when activating add-on has failed", LastModified = "2016/07/29", Value = "This add-on has not been activated successfully. Please check the log for further information")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ErrorActivating => this[nameof (ErrorActivating)];

    /// <summary>Gets Error message when importing content has failed</summary>
    /// <value>Some content and pages have not been imported successfully. Please check the log for further information</value>
    [ResourceEntry("ErrorImportingContent", Description = "Error message when importing content has failed", LastModified = "2016/07/29", Value = "Some content and pages have not been imported successfully. Please check the log for further information")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ErrorImportingContent => this[nameof (ErrorImportingContent)];

    /// <summary>
    /// Gets Error message when importing structure has failed
    /// </summary>
    /// <value>Some modules have not been imported successfully. Please check the log for further information</value>
    [ResourceEntry("ErrorImportingStructure", Description = "Error message when importing structure has failed", LastModified = "2016/08/08", Value = "Some modules have not been imported successfully. Please check the log for further information")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ErrorImportingStructure => this[nameof (ErrorImportingStructure)];

    /// <summary>
    /// Gets Error message when activating some modules has failed
    /// </summary>
    /// <value>Some modules have not been activated successfully. Please check the log for further information</value>
    [ResourceEntry("ErrorActivatingSomeModules", Description = "Error message when activating some modules has failed", LastModified = "2016/08/08", Value = "Some modules have not been activated successfully. Please check the log for further information")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ErrorActivatingSomeModules => this[nameof (ErrorActivatingSomeModules)];

    /// <summary>Gets Error message when deleting content has failed</summary>
    /// <value>Some content and pages have not been deleted successfully. Please check the log for further information</value>
    [ResourceEntry("ErrorDeletingContent", Description = "Error message when deleting content has failed", LastModified = "2016/07/29", Value = "Some content and pages have not been deleted successfully. Please check the log for further information")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ErrorDeletingContent => this[nameof (ErrorDeletingContent)];

    /// <summary>Gets "Web services" phrase</summary>
    /// <value>Web services</value>
    [ResourceEntry("WebServices", Description = "Web services phrase", LastModified = "2016/08/11", Value = "Web services")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string WebServices => this[nameof (WebServices)];

    /// <summary>Gets "Modules and sample content" phrase</summary>
    /// <value>Modules and sample content</value>
    [ResourceEntry("ModulesAndSampleContent", Description = "Modules and sample content phrase", LastModified = "2016/09/10", Value = "Modules and sample content")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string ModulesAndSampleContent => this[nameof (ModulesAndSampleContent)];

    /// <summary>Gets "Blank NuGet template for add-on" phrase</summary>
    /// <value>Blank NuGet template for add-on</value>
    [ResourceEntry("BlankNugetTemplateForAddon", Description = "Blank NuGet template for add-on phrase", LastModified = "2016/09/10", Value = "Blank NuGet template for add-on")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1630:DocumentationTextMustContainWhitespace", Justification = "Spelling conflicts.")]
    public string BlankNugetTemplateForAddon => this[nameof (BlankNugetTemplateForAddon)];

    /// <summary>Gets "Download ZIP file" phrase</summary>
    /// <value>Download ZIP file phrase</value>
    [ResourceEntry("DownloadZipFilePageTitle", Description = "Download ZIP file phrase", LastModified = "2016/01/12", Value = "Download ZIP file")]
    public string DownloadZipFilePageTitle => this[nameof (DownloadZipFilePageTitle)];

    /// <summary>Gets "Archive" phrase</summary>
    /// <value>Archive phrase</value>
    [ResourceEntry("DownloadZipFilePageUrl", Description = "Archive phrase", LastModified = "2016/01/12", Value = "Archive")]
    public string DownloadZipFilePageUrl => this[nameof (DownloadZipFilePageUrl)];

    /// <summary>
    /// Gets "You can export modules and content from the current site and download it as a ZIP file (Up to 2Gb)" phrase
    /// </summary>
    /// <value>You can export modules and content from the current site and download it as a ZIP file (Up to 2Gb) phrase</value>
    [ResourceEntry("ExportToZipFileSizeWarningMessage", Description = "You can export modules and content from the current site and download it as a ZIP file (Up to 2Gb) phrase", LastModified = "2016/01/12", Value = "You can export modules and content from the current site and download it as a ZIP file (Up to 2Gb)")]
    public string ExportToZipFileSizeWarningMessage => this[nameof (ExportToZipFileSizeWarningMessage)];

    /// <summary>
    /// Gets "Export failed because ZIP file exceeds the 2Gb size limit" phrase
    /// </summary>
    /// <value>Export failed because ZIP file exceeds the 2Gb size limit phrase</value>
    [ResourceEntry("ExportedArchiveExceedsFileLimitMessage", Description = "Export failed because ZIP file exceeds the 2Gb size limit phrase", LastModified = "2016/13/12", Value = "Export failed because ZIP file exceeds the 2Gb size limit")]
    public string ExportedArchiveExceedsFileLimitMessage => this[nameof (ExportedArchiveExceedsFileLimitMessage)];

    /// <summary>
    /// Gets "Upload ZIP file with already exported modules or content and it will be imported in this project" phrase
    /// </summary>
    /// <value>Upload ZIP file with already exported modules or content and it will be imported in this project phrase</value>
    [ResourceEntry("UploadZipPageHeaderMessage", Description = "Upload ZIP file with already exported modules or content and it will be imported in this project phrase", LastModified = "2016/06/12", Value = "Upload ZIP file with already exported modules or content and it will be imported in this project")]
    public string UploadZipPageHeaderMessage => this[nameof (UploadZipPageHeaderMessage)];

    /// <summary>Gets "ZIP file to upload (Up to 2Gb)" phrase</summary>
    /// <value>ZIP file to upload (Up to 2Gb) phrase</value>
    [ResourceEntry("UploadZipLabelMessage", Description = "ZIP file to upload (Up to 2Gb) phrase", LastModified = "2016/15/12", Value = "ZIP file to upload (Up to 2Gb)")]
    public string UploadZipLabelMessage => this[nameof (UploadZipLabelMessage)];

    /// <summary>Gets "Uploading..." phrase</summary>
    /// <value>Uploading... phrase</value>
    [ResourceEntry("UploadingMessage", Description = "Uploading... phrase", LastModified = "2016/15/12", Value = "Uploading...")]
    public string UploadingMessage => this[nameof (UploadingMessage)];

    /// <summary>Gets "Export for add-on" phrase</summary>
    /// <value>Export for add-on phrase</value>
    [ResourceEntry("ExportForMakingAddonPageTitle", Description = "Export for add-on phrase", LastModified = "2016/01/12", Value = "Export for add-on")]
    public string ExportForMakingAddonPageTitle => this[nameof (ExportForMakingAddonPageTitle)];

    /// <summary>Gets "Addon" phrase</summary>
    /// <value>Export for Making add-on phrase</value>
    [ResourceEntry("ExportForMakingAddonPageUrl", Description = "Addon phrase", LastModified = "2016/01/12", Value = "Addon")]
    public string ExportForMakingAddonPageUrl => this[nameof (ExportForMakingAddonPageUrl)];

    /// <summary>Gets "Import ZIP file" phrase</summary>
    /// <value>Import ZIP file phrase</value>
    [ResourceEntry("ImportZipFilePageTitle", Description = "Import ZIP file phrase", LastModified = "2016/01/12", Value = "Import ZIP file")]
    public string ImportZipFilePageTitle => this[nameof (ImportZipFilePageTitle)];

    /// <summary>Gets "Export / Import" phrase</summary>
    /// <value>Export / Import phrase</value>
    [ResourceEntry("ExportImportPageTitle", Description = "Export / Import phrase", LastModified = "2016/01/12", Value = "Export / Import")]
    public string ExportImportPageTitle => this[nameof (ExportImportPageTitle)];
  }
}
