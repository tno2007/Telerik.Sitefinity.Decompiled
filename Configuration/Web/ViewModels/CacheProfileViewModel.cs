// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.ViewModels.CacheProfileViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace Telerik.Sitefinity.Configuration.Web.ViewModels
{
  /// <summary>View model for a cache profile.</summary>
  [DataContract]
  internal class CacheProfileViewModel
  {
    private IEnumerable<string> defaultItemTypes;
    private IEnumerable<string> availableItemTypes;
    public static readonly IEnumerable<string> PageCacheItemTypes = (IEnumerable<string>) new string[1]
    {
      "Page"
    };
    public static readonly IEnumerable<string> MediaCacheItemTypes = (IEnumerable<string>) new string[3]
    {
      "Image",
      "Document",
      "Video"
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.ViewModels.CacheProfileViewModel" /> class.
    /// </summary>
    public CacheProfileViewModel()
    {
      this.AvailableItemTypes = (IEnumerable<string>) new List<string>();
      this.DefaultItemTypes = (IEnumerable<string>) new List<string>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Configuration.Web.ViewModels.CacheProfileViewModel" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    /// <param name="availableItemTypes">The types for which the cache profile can be default.</param>
    public CacheProfileViewModel(
      OutputCacheProfileElement element,
      IEnumerable<string> availableItemTypes)
      : this()
    {
      this.Name = element.Name;
      this.Location = element.Location.ToString();
      this.MaxAge = element.Duration;
      this.BrowserMaxAge = element.ClientMaxAge;
      this.ProxyCdnMaxAge = element.ProxyMaxAge;
      this.SlidingExpiration = element.SlidingExpiration;
      this.AvailableItemTypes = (IEnumerable<string>) availableItemTypes.ToList<string>();
      this.ItemMaxSize = new int?(element.MaxSize);
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the location.</summary>
    /// <value>The location.</value>
    [DataMember]
    public string Location { get; set; }

    /// <summary>Gets or sets the max age.</summary>
    /// <value>The max age.</value>
    [DataMember]
    public int MaxAge { get; set; }

    /// <summary>Gets or sets the browser max age.</summary>
    /// <value>The browser max age.</value>
    [DataMember]
    public int? BrowserMaxAge { get; set; }

    /// <summary>Gets or sets the proxy max age.</summary>
    /// <value>The proxy max age.</value>
    [DataMember]
    public int? ProxyCdnMaxAge { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether sliding expiration is enabled..
    /// </summary>
    /// <value>
    ///   <c>true</c> if sliding expiration is enabled; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool SlidingExpiration { get; set; }

    /// <summary>Gets or sets the maximum size of the item.</summary>
    /// <value>The maximum size of the item.</value>
    [DataMember]
    public int? ItemMaxSize { get; set; }

    /// <summary>Gets or sets the types this profile is default for.</summary>
    /// <value>The types this profile is default for.</value>
    [DataMember]
    public IEnumerable<string> DefaultItemTypes
    {
      get => this.defaultItemTypes;
      set => this.defaultItemTypes = value;
    }

    /// <summary>
    /// Gets or sets the available types this profile can be default for.
    /// </summary>
    /// <value>The available types this profile cane be default for.</value>
    [DataMember]
    public IEnumerable<string> AvailableItemTypes
    {
      get => this.availableItemTypes;
      set => this.availableItemTypes = value;
    }
  }
}
