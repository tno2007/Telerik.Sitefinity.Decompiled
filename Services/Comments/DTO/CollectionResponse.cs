// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CollectionResponse`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  public class CollectionResponse<T>
  {
    /// <summary>Gets or sets the total count.</summary>
    /// <value>The total count.</value>
    public int TotalCount { get; set; }

    /// <summary>Gets or sets the items.</summary>
    /// <value>The items.</value>
    public IEnumerable<T> Items { get; set; }
  }
}
