// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.CultureSettingViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Localization.Configuration;

namespace Telerik.Sitefinity.Configuration.Web
{
  /// <summary>
  /// A view model class representing culture setting config element.
  /// </summary>
  [DataContract]
  public class CultureSettingViewModel
  {
    private ResourcesConfig resourcesConfig;
    private string displayName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.CultureSettingViewModel" /> class.
    /// </summary>
    public CultureSettingViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.CultureSettingViewModel" /> class.
    /// </summary>
    /// <param name="configElement">The config element.</param>
    public CultureSettingViewModel(CultureSettingElement configElement)
    {
      this.Key = configElement.CultureKey;
      this.Setting = configElement.Setting;
    }

    /// <summary>Gets or sets the culture key.</summary>
    /// <value>The key.</value>
    [DataMember]
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is default.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsDefault { get; set; }

    /// <summary>Gets or sets the setting.</summary>
    /// <value>The setting.</value>
    [DataMember]
    public string Setting { get; set; }

    /// <summary>Gets or sets the display name.</summary>
    /// <value>The display name.</value>
    [DataMember]
    public string DisplayName
    {
      get
      {
        if (string.IsNullOrEmpty(this.displayName))
        {
          CultureElement culture = this.ResourcesConfig.Cultures[this.Key];
          CultureInfo cultureInfo;
          if (culture == null)
          {
            cultureInfo = CultureInfo.InvariantCulture;
          }
          else
          {
            string name = this.ShowSpecificName ? culture.Culture : culture.UICulture;
            cultureInfo = string.IsNullOrEmpty(name) || name.ToUpperInvariant() == "INVARIANT" ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(name);
          }
          this.displayName = cultureInfo.EnglishName;
        }
        return this.displayName;
      }
      set => this.displayName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the name of the specific culture.
    /// </summary>
    /// <value><c>true</c> if the name of the specific culture is shown; otherwise, <c>false</c>.</value>
    public bool ShowSpecificName { get; set; }

    /// <summary>Gets or sets the resources config.</summary>
    /// <value>The resources config.</value>
    public ResourcesConfig ResourcesConfig
    {
      get
      {
        if (this.resourcesConfig == null)
          this.resourcesConfig = ConfigManager.GetManager().GetSection<ResourcesConfig>();
        return this.resourcesConfig;
      }
      set => this.resourcesConfig = value;
    }
  }
}
