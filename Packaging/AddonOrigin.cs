// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.AddonOrigin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;

namespace Telerik.Sitefinity.Packaging
{
  /// <summary>Class representing add-on origin string</summary>
  internal class AddonOrigin
  {
    private const string AddonOriginKey = "addon";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Packaging.AddonOrigin" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="path">The path.</param>
    [JsonConstructor]
    public AddonOrigin(string name, string path)
    {
      this.Name = name;
      this.Path = path;
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>Gets or sets the path.</summary>
    /// <value>The path.</value>
    public string Path { get; set; }

    /// <summary>Parses the specified origin.</summary>
    /// <param name="origin">The origin.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Packaging.AddonOrigin" /> object</returns>
    /// <exception cref="T:System.ArgumentException">origin is not AddonOrigin</exception>
    public static AddonOrigin Parse(OriginWrapperObject origin) => AddonOrigin.IsAddonOrigin(origin) ? JsonConvert.DeserializeObject<AddonOrigin>(origin.Value) : throw new ArgumentException("origin is not AddonOrigin");

    /// <summary>Parses the specified origin.</summary>
    /// <param name="origin">The origin.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Packaging.AddonOrigin" /> object</returns>
    /// <exception cref="T:System.ArgumentException">origin is in incorrect format for AddonOrigin</exception>
    public static AddonOrigin Parse(string origin)
    {
      OriginWrapperObject originWrapperObject = JsonConvert.DeserializeObject<OriginWrapperObject>(origin);
      if (originWrapperObject == null)
        throw new ArgumentException("origin is not in correct format");
      return (!(originWrapperObject.Key != "addon") ? JsonConvert.DeserializeObject<AddonOrigin>(originWrapperObject.Value) : throw new ArgumentException("origin is not AddonOrigin")) ?? throw new ArgumentException("origin is not in correct format for AddonOrigin");
    }

    /// <summary>
    /// Tries to parse the given origin to <see cref="T:Telerik.Sitefinity.Packaging.AddonOrigin" />.
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <param name="addonOrigin">The addon origin.</param>
    /// <returns><c>true</c> if parsing was successful, <c>false</c> otherwise</returns>
    public static bool TryParse(string origin, out AddonOrigin addonOrigin)
    {
      try
      {
        addonOrigin = AddonOrigin.Parse(origin);
        return true;
      }
      catch (Exception ex)
      {
        addonOrigin = (AddonOrigin) null;
        return false;
      }
    }

    /// <summary>Checks if add-on names are equal.</summary>
    /// <param name="originA">The origin a.</param>
    /// <param name="originB">The origin b.</param>
    /// <returns>Value indicating whether add-on names are equal.</returns>
    public static bool AddonNamesEqual(string originA, string originB)
    {
      AddonOrigin addonOrigin1;
      AddonOrigin addonOrigin2;
      return AddonOrigin.TryParse(originA, out addonOrigin1) & AddonOrigin.TryParse(originB, out addonOrigin2) && addonOrigin1.Name == addonOrigin2.Name;
    }

    /// <summary>
    /// Determines whether [is addon origin] [the specified origin].
    /// </summary>
    /// <param name="origin">The origin.</param>
    /// <returns>
    ///   <c>true</c> if [is addon origin] [the specified origin]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsAddonOrigin(OriginWrapperObject origin) => origin != null && origin.Key == "addon";

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public override string ToString() => JsonConvert.SerializeObject((object) new OriginWrapperObject("addon", JsonConvert.SerializeObject((object) this)));
  }
}
