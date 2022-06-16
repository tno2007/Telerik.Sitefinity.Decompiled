// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentItemLocation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.ContentLocations
{
  internal class ContentItemLocation : IContentItemLocation, IContentLocationBase
  {
    /// <inheritdoc />
    public string ItemAbsoluteUrl { get; set; }

    /// <inheritdoc />
    public Guid PageId { get; set; }

    /// <inheritdoc />
    public Guid SiteId { get; set; }

    /// <summary>
    /// Gets the ID of the control, where the item is located.
    /// </summary>
    /// <value>The site id.</value>
    public Guid ControlId { get; set; }

    /// <inheritdoc />
    public bool IsDefault { get; set; }
  }
}
