// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Configuration.CultureElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Globalization;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Localization.Configuration
{
  /// <summary>
  /// Represents a configuration element that defines a supported culture.
  /// </summary>
  [DataContract]
  public class CultureElement : ConfigElement
  {
    private string displayName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Configuration.CultureElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CultureElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal CultureElement()
      : base(false)
    {
    }

    internal CultureElement(string key, string culture, string uiCulture)
      : this()
    {
      this.Key = key;
      this.Culture = culture;
      this.UICulture = uiCulture;
    }

    /// <summary>Gets or sets the key used to identify the culture</summary>
    /// <remarks>
    /// Key is the token that will be visible in the URL as path prefix, query string or sub domain
    /// and will be used to identify the culture.
    /// </remarks>
    [ConfigurationProperty("key", DefaultValue = "", IsKey = true, IsRequired = true)]
    public virtual string Key
    {
      get => (string) this["key"];
      set => this["key"] = (object) value;
    }

    /// <summary>Gets or sets the specific name of the culture</summary>
    [ConfigurationProperty("culture", DefaultValue = "", IsRequired = true)]
    [DataMember]
    public virtual string Culture
    {
      get => (string) this["culture"];
      set => this.TyrSetCulture("culture", value);
    }

    /// <summary>Gets or sets the specific name of the culture</summary>
    [ConfigurationProperty("uiCulture", DefaultValue = "", IsRequired = true)]
    [DataMember]
    public virtual string UICulture
    {
      get => (string) this["uiCulture"];
      set => this.TyrSetCulture("uiCulture", value);
    }

    /// <summary>
    /// Gets or sets the field suffix Suffix used to append to the field name for generating language specific columns in the database.
    /// </summary>
    [ConfigurationProperty("fieldSuffix", DefaultValue = "")]
    public string FieldSuffix
    {
      get => (string) this["fieldSuffix"];
      set => this["fieldSuffix"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the display name of the culture represented by this element.
    /// </summary>
    /// <remarks>This property is mostly used for the serialization purposes, where
    /// it is not possible to serialize the whole CultureInfo object.
    /// </remarks>
    [DataMember]
    public virtual string DisplayName
    {
      get
      {
        if (string.IsNullOrEmpty(this.displayName))
          this.displayName = (string.IsNullOrEmpty(this.UICulture) || this.UICulture.ToUpperInvariant() == "INVARIANT" ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(this.UICulture)).DisplayName;
        return this.displayName;
      }
      set => this.displayName = value;
    }

    private void TyrSetCulture(string propertyName, string value)
    {
      try
      {
        CultureInfo.GetCultureInfo(value);
        this[propertyName] = (object) value;
      }
      catch (ArgumentException ex)
      {
        throw new InvalidOperationException(string.Format("The value '{0}' is not a valid culture value.", (object) value), (Exception) ex);
      }
    }
  }
}
