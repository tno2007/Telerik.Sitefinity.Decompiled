// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.IPullPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Pull pipe interface</summary>
  public interface IPullPipe
  {
    /// <summary>
    /// Returns an <see cref="T:System.Linq.IQueryable`1" /> which can be used to "pull" all the items in the pipe.
    /// </summary>
    IQueryable<WrapperObject> GetData();
  }
}
