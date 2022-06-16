// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.WcfThumbnailProfile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// The DTO for the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.ThumbnailProfileConfigElement" />
  /// that is used for sending data to the client.
  /// </summary>
  [DataContract]
  public class WcfThumbnailProfile
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.WcfThumbnailProfile" /> class.
    /// </summary>
    public WcfThumbnailProfile()
    {
    }

    public WcfThumbnailProfile(ThumbnailProfileConfigElement config, string libraryType)
    {
      this.Id = config.Name;
      this.Title = config.Title;
      this.IsDefault = config.IsDefault;
      foreach (string providerName in LibrariesManager.GetManager().GetProviderNames(ProviderBindingOptions.SkipSystem))
      {
        LibrariesManager manager = LibrariesManager.GetManager(providerName);
        IQueryable<Library> source = (IQueryable<Library>) null;
        if (libraryType == typeof (Album).FullName)
          source = (IQueryable<Library>) manager.GetAlbums();
        else if (libraryType == typeof (VideoLibrary).FullName)
          source = (IQueryable<Library>) manager.GetVideoLibraries();
        if (source != null)
          this.LibrariesCount += source.Where<Library>((Expression<Func<Library, bool>>) (l => l.ThumbnailProfiles.Contains(this.Id))).Count<Library>();
      }
      this.Size = ObjectFactory.Resolve<IImageProcessor>().Methods[config.Method].GetSizeMessage(config.Parameters);
    }

    /// <summary>Gets or sets the id.</summary>
    [DataMember]
    public string Id { get; set; }

    /// <summary>The user friendly name of the thumbnail profile.</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>The size of the thumbnail.</summary>
    [DataMember]
    public string Size { get; set; }

    /// <summary>
    /// A value indicating whether the provider is the default one.
    /// </summary>
    [DataMember]
    public bool IsDefault { get; set; }

    /// <summary>
    /// A value indicating how much libraries have this thumbnail set.
    /// </summary>
    [DataMember]
    public int LibrariesCount { get; set; }
  }
}
