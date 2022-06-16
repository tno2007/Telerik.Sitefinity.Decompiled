// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Archive.TrackedInterval
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.GenericContent.Archive
{
  /// <summary>
  /// The class represents a time frame interval marked with start and end date.
  /// </summary>
  public class TrackedInterval
  {
    /// <summary>Gets or sets the start date of time interval.</summary>
    /// <value>The start date.</value>
    public DateTime StartDate { get; set; }

    /// <summary>Gets or sets the end date of time interval.</summary>
    /// <value>The end date.</value>
    public DateTime EndDate { get; set; }
  }
}
