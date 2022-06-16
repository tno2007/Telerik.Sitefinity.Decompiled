// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentUsages.Model.ContentItemUsage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;

namespace Telerik.Sitefinity.ContentUsages.Model
{
  /// <summary>
  /// Provides information about a specific content item source.
  /// </summary>
  internal class ContentItemUsage : IContentItemUsage
  {
    /// <inheritdoc />
    public Guid ItemId { get; set; }

    /// <inheritdoc />
    public string ItemType { get; set; }

    /// <inheritdoc />
    public string ItemProvider { get; set; }

    /// <inheritdoc />
    public CultureInfo Culture { get; set; }

    /// <inheritdoc />
    public string LiveUrl { get; set; }
  }
}
