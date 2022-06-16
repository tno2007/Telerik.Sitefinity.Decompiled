// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ViewsModuleConfigBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules
{
  internal abstract class ViewsModuleConfigBase : ConfigSection
  {
    /// <summary>Gets a collection of data View Controls.</summary>
    [ConfigurationProperty("viewControls")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ViewControlsDescription", Title = "ViewControls")]
    [ConfigurationCollection(typeof (ViewContainerElement), AddItemName = "viewControl")]
    public ConfigElementDictionary<string, ViewContainerElement> ViewControls => (ConfigElementDictionary<string, ViewContainerElement>) this["viewControls"];

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.InitializeDefaultViews(this.ViewControls);
    }

    /// <summary>Initializes the default views.</summary>
    protected abstract void InitializeDefaultViews(
      ConfigElementDictionary<string, ViewContainerElement> viewControls);
  }
}
