// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsRegistration
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>
  /// this record is used by the basic settings panel to create a basic settings view
  /// </summary>
  internal class BasicSettingsRegistration
  {
    public BasicSettingsRegistration()
    {
    }

    public BasicSettingsRegistration(
      string settingsName,
      Type viewType,
      string settingsTitle,
      string settingsResourceClass)
    {
      this.SettingsName = settingsName;
      this.ViewType = viewType;
      this.SettingsTitle = settingsTitle;
      this.SettingsResourceClass = settingsResourceClass;
    }

    /// <summary>Gets or sets the type of the view.</summary>
    /// <value>The type of the view.</value>
    public Type ViewType { get; set; }

    /// <summary>Gets or sets the name of the settings.</summary>
    /// <value>The name of the settings.</value>
    public string SettingsName { get; set; }

    /// <summary>Gets or sets the settings title.</summary>
    /// <value>The settings title.</value>
    public string SettingsTitle { get; set; }

    /// <summary>Gets or sets the settings resource class.</summary>
    /// <value>The settings resource class.</value>
    public string SettingsResourceClass { get; set; }

    /// <summary>Gets or sets the type of the data contract.</summary>
    /// <value>The type of the data contract.</value>
    public Type DataContractType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [allow settings per site].
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [allow settings per site]; otherwise, <c>false</c>.
    /// </value>
    public bool AllowSettingsPerSite { get; set; }
  }
}
