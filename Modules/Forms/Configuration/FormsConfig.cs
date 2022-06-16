// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Configuration.FormsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Data;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.Forms.Configuration
{
  /// <summary>Defines Forms configuration settings.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "FormsConfigDescription", Title = "FormsConfigCaption")]
  public class FormsConfig : ContentModuleConfigBase
  {
    /// <summary>Gets the notifications settings.</summary>
    /// <value>The notifications.</value>
    [ConfigurationProperty("notifications")]
    public FormsNotificationsSettings Notifications => (FormsNotificationsSettings) this["notifications"];

    /// <summary>Gets or sets the parameters settings.</summary>
    /// <value>The parameters.</value>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.CommentsSettings.AllowComments = new bool?(false);
    }

    /// <summary>Initializes the default providers.</summary>
    /// <param name="providers"></param>
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores content data in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessFormsProvider),
        Parameters = new NameValueCollection()
        {
          {
            "connectionString",
            "_Sitefinity_Forms"
          }
        }
      });
    }

    /// <summary>Initializes the default views.</summary>
    /// <param name="contentViewControls"></param>
    protected override void InitializeDefaultViews(
      ConfigElementDictionary<string, ContentViewControlElement> contentViewControls)
    {
      contentViewControls.Add(FormsDefinitions.DefineFormsBackendContentView((ConfigElement) contentViewControls));
      contentViewControls.Add(FormsDefinitions.DefineFormsFrontendContentView((ConfigElement) contentViewControls));
    }
  }
}
