// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.OperationReasonResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// This class contains resources for the Operation Reasons.
  /// </summary>
  [ObjectInfo(typeof (OperationReasonResources), Description = "OperationReasonResourcesClassDescription", Title = "OperationReasonResourcesClassTitle")]
  public class OperationReasonResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReasonResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public OperationReasonResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReasonResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider">The data provider of type<see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public OperationReasonResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Gets the title of this class</summary>
    [ResourceEntry("OperationReasonResourcesClassTitle", Description = "The title of this class.", LastModified = "2016/11/07", Value = "Operation reason labels")]
    public string OperationReasonResourcesClassTitle => this[nameof (OperationReasonResourcesClassTitle)];

    /// <summary>
    /// Gets localizable resources for the restart application reasons.
    /// </summary>
    [ResourceEntry("OperationReasonResourcesClassDescription", Description = "The description of this class.", LastModified = "2016/11/07", Value = "Contains localizable resources for the restart application reasons.")]
    public string OperationReasonResourcesClassDescription => this[nameof (OperationReasonResourcesClassDescription)];

    /// <summary>
    /// Gets the message displayed when restart occur and the reason is unknown.
    /// </summary>
    [ResourceEntry("UnknownReason", Description = "The message displayed when restart occur and the reason is unknown.", LastModified = "2016/11/07", Value = "Unknown Reason")]
    public string UnknownReason => this[nameof (UnknownReason)];

    /// <summary>Gets the title of this class</summary>
    [ResourceEntry("The message displayed when restart occur and the reason is update of the statistics module.", Description = "The message displayed when restart occur and the reason is update of the statistics module.", LastModified = "2016/11/07", Value = "Update of the statistic module")]
    public string StaticModulesUpdate => this[nameof (StaticModulesUpdate)];

    /// <summary>
    /// Gets the message displayed when restart occur and the reason is change in the localization settings.
    /// </summary>
    [ResourceEntry("LocalizationChange", Description = "The message displayed when restart occur and the reason is change in the localization settings.", LastModified = "2016/11/07", Value = "Change in the localization settings")]
    public string LocalizationChange => this[nameof (LocalizationChange)];

    /// <summary>
    /// Gets the message displayed when restart occur and the reason is change in update of the license.
    /// </summary>
    [ResourceEntry("LicenseUpdate", Description = "The message displayed when restart occur and the reason is update of the license.", LastModified = "2016/11/07", Value = "Update of the license")]
    public string LicenseUpdate => this[nameof (LicenseUpdate)];

    /// <summary>
    /// Gets the message displayed when restart occur during the import of a dynamic module.
    /// </summary>
    [ResourceEntry("DynamicModuleImport", Description = "The message displayed when restart occur during the import of a dynamic module.", LastModified = "2016/11/07", Value = "Import of dynamic module")]
    public string DynamicModuleImport => this[nameof (DynamicModuleImport)];

    /// <summary>
    /// Gets the message displayed when restart occur during the install of a dynamic module.
    /// </summary>
    [ResourceEntry("DynamicModuleInstall", Description = "The message displayed when restart occur during the install of a dynamic module.", LastModified = "2016/11/07", Value = "Install of dynamic module")]
    public string DynamicModuleInstall => this[nameof (DynamicModuleInstall)];

    /// <summary>
    /// Gets the message displayed when restart occur during the uninstall of a dynamic module.
    /// </summary>
    [ResourceEntry("DynamicModuleUninstall", Description = "The message displayed when restart occur during the uninstall of a dynamic module.", LastModified = "2016/11/07", Value = "Uninstall of dynamic module")]
    public string DynamicModuleUninstall => this[nameof (DynamicModuleUninstall)];

    /// <summary>
    /// Gets the message displayed when restart occur during the delete of a dynamic module.
    /// </summary>
    [ResourceEntry("DynamicModuleDelete", Description = "The message displayed when restart occur during the delete of a dynamic module.", LastModified = "2016/11/07", Value = "Delete of dynamic module")]
    public string DynamicModuleDelete => this[nameof (DynamicModuleDelete)];

    /// <summary>
    /// Gets the message displayed when restart occur during the install of a dynamic module type.
    /// </summary>
    [ResourceEntry("DynamicModuleTypeInstall", Description = "The message displayed when restart occur during the install of a dynamic module type.", LastModified = "2016/11/07", Value = "Install of dynamic module type")]
    public string DynamicModuleTypeInstall => this[nameof (DynamicModuleTypeInstall)];

    /// <summary>
    /// Gets the message displayed when restart occur during the delete of a dynamic module type.
    /// </summary>
    [ResourceEntry("DynamicModuleTypeDelete", Description = "The message displayed when restart occur during the delete of a dynamic module type.", LastModified = "2016/11/07", Value = "Delete of dynamic module type")]
    public string DynamicModuleTypeDelete => this[nameof (DynamicModuleTypeDelete)];

    /// <summary>
    /// Gets the message displayed when restart occur during change in the metadata.
    /// </summary>
    [ResourceEntry("MetaDataChange", Description = "The message displayed when restart occur during change in the metadata.", LastModified = "2016/11/07", Value = "Meta data change")]
    public string MetaDataChange => this[nameof (MetaDataChange)];

    /// <summary>
    /// Gets the message displayed when restart occur because of change in the configurations.
    /// </summary>
    [ResourceEntry("ConfigChange", Description = "The message displayed when restart occur because of change in the configurations.", LastModified = "2016/11/07", Value = "Change of the configurations.")]
    public string ConfigChange => this[nameof (ConfigChange)];

    /// <summary>
    /// Gets the message displayed during the first system set up.
    /// </summary>
    [ResourceEntry("SystemSetup", Description = "The message displayed during the first system set up.", LastModified = "2016/11/07", Value = "First system setup.")]
    public string SystemSetup => this[nameof (SystemSetup)];

    /// <summary>
    /// Gets the message displayed during performance optimization.
    /// </summary>
    [ResourceEntry("PerformanceOptimizationTask", Description = "The message displayed during performance optimization.", LastModified = "2016/11/07", Value = "Performance optimization started")]
    public string PerformanceOptimizationTask => this[nameof (PerformanceOptimizationTask)];

    /// <summary>Gets the message displayed during restart on upgrade.</summary>
    [ResourceEntry("InternalRestartOnUpgrade", Description = "Gets the message displayed during restart on upgrade.", LastModified = "2016/11/07", Value = "Update of the site")]
    public string InternalRestartOnUpgrade => this[nameof (InternalRestartOnUpgrade)];

    /// <summary>
    /// Gets the message displayed during database clean after dynamic types or fields are deleted.
    /// </summary>
    [ResourceEntry("DatabaseCleanAfterDynamicTypesOrFieldsAreDeleted", Description = "Gets the message displayed during database clean after dynamic types or fields are deleted.", LastModified = "2016/11/07", Value = "Database clean after dynamic types or fields are deleted")]
    public string DatabaseCleanAfterDynamicTypesOrFieldsAreDeleted => this[nameof (DatabaseCleanAfterDynamicTypesOrFieldsAreDeleted)];

    /// <summary>
    /// Gets the message displayed during change in the responsive design settings.
    /// </summary>
    [ResourceEntry("ResponsiveDesignSettingsChange", Description = "The message displayed during change in the responsive design settings.", LastModified = "2016/11/07", Value = "Change in responsive design settings")]
    public string ResponsiveDesignSettingsChange => this[nameof (ResponsiveDesignSettingsChange)];

    /// <summary>
    /// Gets the message displayed during install of the Marketo connector module.
    /// </summary>
    [ResourceEntry("MarketoConnectorModuleInstall", Description = "The message displayed during install of the Marketo connector module.", LastModified = "2016/11/07", Value = "Install of Marketo Connector module")]
    public string MarketoConnectorModuleInstall => this[nameof (MarketoConnectorModuleInstall)];

    /// <summary>
    /// Gets the message displayed during upgrade of the Marketo connector module.
    /// </summary>
    [ResourceEntry("MarketoConnectorModuleUpgrade", Description = "The message displayed during upgrade of the Marketo connector module.", LastModified = "2016/11/07", Value = "Upgrade of Marketo Connector module")]
    public string MarketoConnectorModuleUpgrade => this[nameof (MarketoConnectorModuleUpgrade)];

    /// <summary>
    /// Gets the message displayed during change in the SharePoint module.
    /// </summary>
    [ResourceEntry("SharepointModuleChange", Description = "The message displayed during change in the Sharepoint module.", LastModified = "2016/11/07", Value = "Change in the SharePoint module")]
    public string SharepointModuleChange => this[nameof (SharepointModuleChange)];
  }
}
