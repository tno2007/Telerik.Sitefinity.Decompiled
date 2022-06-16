// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Data.ResourceEntry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Localization.Data
{
  /// <summary>Represents resource data entry persistent class.</summary>
  [DataContract]
  [KnownType(typeof (XmlResourceEntry))]
  public class ResourceEntry
  {
    private string classId;
    private CultureInfo culture;
    private string cultureDisplayName;
    private string cultureName;
    private string key;
    private string value;
    private string description;
    private DateTime lastModified;
    private bool builtIn;

    /// <summary>
    /// Default constructor for creating instance <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> class.
    /// </summary>
    public ResourceEntry()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" />
    /// </summary>
    /// <param name="classId">The key of the resource set this entry belongs to.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <param name="key">The key by which this entry is accessed.</param>
    public ResourceEntry(string classId, CultureInfo culture, string key)
    {
      this.classId = classId;
      this.culture = culture;
      this.key = key;
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" />
    /// </summary>
    /// <param name="classId">The key of the resource set this entry belongs to.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <param name="key">The key by which this entry is accessed.</param>
    /// <param name="value">The localized string.</param>
    /// <param name="description">The value for this entry.</param>
    /// <param name="lastModified">The date this entry was modified.</param>
    public ResourceEntry(
      string classId,
      CultureInfo culture,
      string key,
      string value,
      string description,
      DateTime lastModified)
    {
      this.classId = classId;
      this.culture = culture;
      this.key = key;
      this.value = value;
      this.description = description;
      this.lastModified = lastModified;
    }

    /// <summary>The key of the resource set this entry belongs to.</summary>
    [DataMember]
    public virtual string ClassId
    {
      get => this.classId;
      protected set => this.classId = value;
    }

    /// <summary>
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </summary>
    public virtual CultureInfo Culture
    {
      get => this.culture;
      protected set => this.culture = value;
    }

    /// <summary>Gets or sets the display name of the culture.</summary>
    /// <remarks>
    /// This field is used as a adapter for Culture property which cannot be serialized
    /// through the standard DataContractSerializer.
    /// </remarks>
    [DataMember]
    public string CultureDisplayName
    {
      get
      {
        if (string.IsNullOrEmpty(this.cultureDisplayName))
          this.cultureDisplayName = this.Culture.DisplayName;
        return this.cultureDisplayName;
      }
      set => this.cultureDisplayName = value;
    }

    /// <summary>Gets or sets the full name of the culture.</summary>
    /// <remarks>
    /// This property is mainly used for the WCF services and serialization
    /// purposes.
    /// </remarks>
    [DataMember]
    public string CultureName
    {
      get
      {
        if (string.IsNullOrEmpty(this.cultureName))
          this.cultureName = this.Culture.Name;
        return this.cultureName;
      }
      set => this.cultureName = value;
    }

    /// <summary>Gets or sets the key by which this entry is accessed.</summary>
    [DataMember]
    public virtual string Key
    {
      get => this.key;
      protected set => this.key = value;
    }

    /// <summary>Gets or sets the value for this entry.</summary>
    [DataMember]
    public virtual string Value
    {
      get => this.value;
      set => this.value = value;
    }

    /// <summary>Gets or sets description for this entry.</summary>
    [DataMember]
    public virtual string Description
    {
      get => this.description;
      set => this.description = value;
    }

    /// <summary>Gets or sets the date this entry was modified.</summary>
    [DataMember]
    public virtual DateTime LastModified
    {
      get => this.lastModified;
      protected internal set
      {
        this.lastModified = value;
        if (this.lastModified.Kind == DateTimeKind.Utc)
          return;
        this.lastModified = this.lastModified.ToUniversalTime();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the resource entry is [built in].
    /// </summary>
    /// <value><c>true</c> if [built in]; otherwise, <c>false</c>.</value>
    [DataMember]
    public virtual bool BuiltIn
    {
      get => this.builtIn;
      set => this.builtIn = value;
    }

    internal static string GetUniqueKey(string classId, string key, CultureInfo culture) => string.Format("{0}+{1}+{2}", (object) classId, (object) key, (object) culture.Name);

    internal string GetUniqueKey() => ResourceEntry.GetUniqueKey(this.ClassId, this.Key, this.Culture);

    internal static void ParseKey(
      string uniqueKey,
      out string classId,
      out string key,
      out CultureInfo culture)
    {
      string[] strArray = uniqueKey.Split('+');
      classId = strArray[0];
      key = strArray[1];
      if (string.IsNullOrEmpty(strArray[2]))
        culture = CultureInfo.InvariantCulture;
      else
        culture = CultureInfo.GetCultureInfo(strArray[2]);
    }
  }
}
