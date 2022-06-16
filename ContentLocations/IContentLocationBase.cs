// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.IContentLocationBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>Provides content location information in Sitefinity.</summary>
  public interface IContentLocationBase
  {
    /// <summary>Gets the ID if the page, where the item is located.</summary>
    /// <value>The page id.</value>
    Guid PageId { get; }

    /// <summary>Gets the ID of the site, where the item is located.</summary>
    /// <value>The site id.</value>
    Guid SiteId { get; }
  }
}
