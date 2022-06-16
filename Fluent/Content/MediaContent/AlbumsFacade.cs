// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Content.AlbumsFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;

namespace Telerik.Sitefinity.Fluent.Content
{
  [Obsolete]
  public class AlbumsFacade : ContentsFacade<AlbumsFacade, Album, AlbumsFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.AlbumsFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public AlbumsFacade(AppSettings appSettings)
      : base(appSettings)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.AlbumsFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public AlbumsFacade(AppSettings appSettings, AlbumsFacade parentFacade)
      : base(appSettings, parentFacade)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Content.AlbumsFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    /// <param name="items">The items that the facade will work with.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public AlbumsFacade(
      AppSettings appSettings,
      IQueryable<Album> items,
      AlbumsFacade parentFacade)
      : base(appSettings, items, parentFacade)
    {
    }
  }
}
