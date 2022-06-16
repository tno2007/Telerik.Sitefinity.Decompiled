// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.DataEventAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// Pseudo-enumeration (actually a class with <c>static readonly</c> fields) of the possible C[R]UD action names used in <see cref="T:Telerik.Sitefinity.Data.Events.IDataEvent" />s.
  /// </summary>
  public static class DataEventAction
  {
    public static readonly string Created = SecurityConstants.TransactionActionType.New.ToString();
    public static readonly string Updated = SecurityConstants.TransactionActionType.Updated.ToString();
    public static readonly string Deleted = SecurityConstants.TransactionActionType.Deleted.ToString();
  }
}
