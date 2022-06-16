// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.ContentModuleConfigBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>
  /// Defines the common configuration settings for content modules
  /// </summary>
  public abstract class ContentModuleConfigBase : 
    ModuleConfigBase,
    IContentViewConfig,
    IPermissionsConfig
  {
    /// <summary>
    /// Gets the configured comments settings for this module.
    /// </summary>
    /// <value>The comments settings.</value>
    [ConfigurationProperty("commentsSettings")]
    [Browsable(false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsSettingsDescription", Title = "CommentsSettingsTitle")]
    public CommentsSettings CommentsSettings
    {
      get => (CommentsSettings) this["commentsSettings"];
      set => this["commentsSettings"] = (object) value;
    }

    /// <summary>Gets a collection of data Content View Controls.</summary>
    [ConfigurationProperty("contentViewControls")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlsDescription", Title = "ContentViewControls")]
    [ConfigurationCollection(typeof (ContentViewControlElement), AddItemName = "contentViewControl")]
    public ConfigElementDictionary<string, ContentViewControlElement> ContentViewControls => (ConfigElementDictionary<string, ContentViewControlElement>) this["contentViewControls"];

    /// <summary>A collection of defined permission sets.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Permissions")]
    [ConfigurationProperty("permissions")]
    [ConfigurationCollection(typeof (Permission), AddItemName = "permission")]
    public virtual ConfigElementDictionary<string, Permission> Permissions => (ConfigElementDictionary<string, Permission>) this["permissions"];

    /// <summary>
    /// A collection of customized actions defined per secured object type and specific permission sets.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "CustomPermissionsDisplaySettings")]
    [ConfigurationProperty("customPermissionsDisplaySettings")]
    [ConfigurationCollection(typeof (SecuredObjectCustomPermissionSet), AddItemName = "customSet")]
    public virtual ConfigElementDictionary<string, CustomPermissionsDisplaySettingsConfig> CustomPermissionsDisplaySettings => (ConfigElementDictionary<string, CustomPermissionsDisplaySettingsConfig>) this["customPermissionsDisplaySettings"];

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.InitializeDefaultViews(this.ContentViewControls);
      this.CommentsSettings.propertyResolver = (PropertyResolverBase) new CommentsPropertyResolver();
    }

    /// <summary>Initializes the default views.</summary>
    protected abstract void InitializeDefaultViews(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls);
  }
}
