// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Locations.StateProvinceElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Locations
{
  /// <summary>
  /// Configuration element which holds information about a state.
  /// </summary>
  public class StateProvinceElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public StateProvinceElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Locations.StateProvinceElement" /> class.
    /// </summary>
    internal StateProvinceElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the name of the state or province.</summary>
    [ConfigurationProperty("name", DefaultValue = "", IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the abbreviation of the name of the state or province.
    /// </summary>
    [ConfigurationProperty("abbreviation", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string Abbreviation
    {
      get => (string) this["abbreviation"];
      set => this["abbreviation"] = (object) value;
    }

    [ConfigurationProperty("stateIsActive", DefaultValue = true)]
    public bool StateIsActive
    {
      get => (bool) this["stateIsActive"];
      set => this["stateIsActive"] = (object) value;
    }

    /// <summary>Gets or sets the state latitude</summary>
    [ConfigurationProperty("latitude", DefaultValue = 0.0)]
    public double Latitude
    {
      get => double.Parse(this["latitude"].ToString());
      set => this["latitude"] = (object) value;
    }

    /// <summary>Gets or sets the state longitude</summary>
    [ConfigurationProperty("longitude", DefaultValue = 0.0)]
    public double Longitude
    {
      get => double.Parse(this["longitude"].ToString());
      set => this["longitude"] = (object) value;
    }
  }
}
