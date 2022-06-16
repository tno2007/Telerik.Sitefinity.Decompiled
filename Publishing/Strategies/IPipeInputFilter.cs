// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Strategies.IPipeInputFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Publishing.Strategies
{
  /// <summary>
  /// The interface that should be implemented by all pipes that can's process all incoming item types
  /// </summary>
  public interface IPipeInputFilter
  {
    /// <summary>Checks if an object can be processed by the pipe</summary>
    /// <param name="obj">The object to be checked if it can be processed</param>
    /// <returns>True if the item can be processed, otherwise false</returns>
    bool ShouldProcessItem(IPublishingEvent obj);
  }
}
