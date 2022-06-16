// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Fluent.IMasterLifecycleFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Lifecycle.Fluent
{
  /// <summary>
  /// When working with master lifecycle version of content item.
  /// </summary>
  public interface IMasterLifecycleFacade
  {
    /// <summary>Checks whether the item has a temp lifecycle version.</summary>
    /// <returns>True if it has, false if it does not.</returns>
    bool IsCheckedOut();

    /// <summary>
    /// Creates a live lifecycle version or copies the master to the live lifecycle version.
    /// </summary>
    /// <returns>The live facade.</returns>
    ILiveLifecycleFacade Publish();
  }
}
