// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Restriction.UnrestrictedModeRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Restriction
{
  /// <summary>
  /// Defines a code block with unrestricted access relying on RestrictionLevel.
  /// </summary>
  public class UnrestrictedModeRegion : IDisposable
  {
    private readonly bool prevMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Restriction.UnrestrictedModeRegion" /> class.
    /// </summary>
    public UnrestrictedModeRegion()
    {
      this.prevMode = Config.SuppressRestriction;
      Config.SuppressRestriction = true;
    }

    void IDisposable.Dispose() => Config.SuppressRestriction = this.prevMode;
  }
}
