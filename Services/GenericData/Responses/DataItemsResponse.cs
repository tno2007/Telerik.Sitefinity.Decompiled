﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.GenericData.Responses.DataItemsResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;

namespace Telerik.Sitefinity.Services.GenericData.Responses
{
  /// <summary>Represents the response returned for related item.</summary>
  public class DataItemsResponse
  {
    /// <summary>Gets the response items.</summary>
    public IEnumerable Items { get; set; }

    /// <summary>Gets or sets the total count of the items.</summary>
    public int TotalCount { get; set; }
  }
}
