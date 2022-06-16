// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SitefinityExceptions.ConflictException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.SitefinityExceptions
{
  /// <summary>Conflict exception</summary>
  internal class ConflictException : ArgumentException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SitefinityExceptions.ConflictException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public ConflictException(string message)
      : base(message)
    {
    }
  }
}
