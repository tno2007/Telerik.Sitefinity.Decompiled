// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ITracker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data
{
  /// <summary>Provides common abilities of an item tracker.</summary>
  internal interface ITracker
  {
    /// <summary>Checks if the tracker has stored any changes.</summary>
    /// <returns></returns>
    bool HasChanges();

    /// <summary>Persists the changes for the tracked items.</summary>
    void SaveChanges();

    /// <summary>Adds an item in the collection of tracked items.</summary>
    /// <param name="item"></param>
    /// <param name="provider"></param>
    void Track(object item, DataProviderBase provider);
  }
}
