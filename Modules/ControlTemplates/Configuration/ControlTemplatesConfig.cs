// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.Configuration.ControlTemplatesConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Modules.ControlTemplates.Configuration
{
  /// <summary>Main config for control templates module</summary>
  public class ControlTemplatesConfig : ConfigSection, IContentViewConfig
  {
    /// <summary>Gets a collection of data Content View Controls.</summary>
    [ConfigurationProperty("contentViewControls")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewControlsDescription", Title = "ContentViewControls")]
    [ConfigurationCollection(typeof (ContentViewControlElement), AddItemName = "contentViewControl")]
    public ConfigElementDictionary<string, ContentViewControlElement> ContentViewControls => (ConfigElementDictionary<string, ContentViewControlElement>) this["contentViewControls"];

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.ContentViewControls.Add(ControlTemplatesDefinitions.DefineControlTemplatesBackendContentView((ConfigElement) this.ContentViewControls));
    }
  }
}
