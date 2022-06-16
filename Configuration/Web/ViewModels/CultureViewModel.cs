// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.CultureViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Localization.Configuration;

namespace Telerik.Sitefinity.Configuration.Web
{
  /// <summary>
  /// A view model class representing culture config element.
  /// </summary>
  [DataContract]
  public class CultureViewModel
  {
    private string displayName;
    private string shortName;
    private string[] sitesNames = new string[0];
    private string[] sitesUsingCultureAsDefault = new string[0];

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.CultureViewModel" /> class.
    /// </summary>
    public CultureViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.CultureViewModel" /> class.
    /// </summary>
    /// <param name="cultureInfo">The culture info.</param>
    public CultureViewModel(CultureInfo cultureInfo)
    {
      this.Key = CulturesConfig.GenerateCultureKey(cultureInfo, cultureInfo);
      this.UICulture = cultureInfo.Name;
      this.Culture = cultureInfo.Name;
      this.ShortName = cultureInfo.Name;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.CultureViewModel" /> class.
    /// </summary>
    /// <param name="cultureElement">The culture element.</param>
    public CultureViewModel(CultureElement cultureElement)
    {
      this.Key = cultureElement.Key;
      this.Culture = cultureElement.Culture;
      this.UICulture = cultureElement.UICulture;
      this.FieldSuffix = cultureElement.FieldSuffix;
    }

    /// <summary>Gets or sets the culture key.</summary>
    /// <value>The key.</value>
    [DataMember]
    public string Key { get; set; }

    /// <summary>Gets or sets the culture.</summary>
    /// <value>The culture.</value>
    [DataMember]
    public string Culture { get; set; }

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    [DataMember]
    public string UICulture { get; set; }

    /// <summary>
    /// Gets or sets the field suffix Suffix used to append to the field name for generating language specific columns in the database.
    /// </summary>
    /// <value>The UI culture.</value>
    [DataMember]
    public string FieldSuffix { get; set; }

    /// <summary>Gets or sets the display name.</summary>
    /// <value>The display name.</value>
    [DataMember]
    public string DisplayName
    {
      get
      {
        if (string.IsNullOrEmpty(this.displayName))
          this.displayName = this.GetInstanceCulture().EnglishName;
        return this.displayName;
      }
      set => this.displayName = value;
    }

    /// <summary>
    /// Gets or sets the TwoLetterISOLanguageName of the culture.
    /// </summary>
    [DataMember]
    public string ShortName
    {
      get
      {
        if (string.IsNullOrEmpty(this.shortName))
          this.shortName = CultureInfo.GetCultureInfo(this.Culture).Name;
        return this.shortName;
      }
      set => this.shortName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is default.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is default; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsDefault { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the name of the specific culture.
    /// </summary>
    /// <value><c>true</c> if the name of the specific culture is shown; otherwise, <c>false</c>.</value>
    public bool ShowSpecificName { get; set; }

    /// <summary>
    /// Gets or sets the names of the sites in which this culture is used.
    /// </summary>
    /// <value>The site names.</value>
    [DataMember]
    public string[] SitesNames
    {
      get => this.sitesNames;
      set => this.sitesNames = value;
    }

    /// <summary>Gets or sets the sites using culture as default.</summary>
    /// <value>The sites using culture as default.</value>
    [DataMember]
    public string[] SitesUsingCultureAsDefault
    {
      get => this.sitesUsingCultureAsDefault;
      set => this.sitesUsingCultureAsDefault = value;
    }

    private CultureInfo GetInstanceCulture()
    {
      string name = this.ShowSpecificName ? this.Culture : this.UICulture;
      return !string.IsNullOrEmpty(name) && !(name.ToUpperInvariant() == "INVARIANT") ? CultureInfo.GetCultureInfo(name) : CultureInfo.InvariantCulture;
    }
  }
}
