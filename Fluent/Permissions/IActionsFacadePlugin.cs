// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Permissions.IActionsFacadePlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Fluent.Permissions
{
  /// <summary>
  /// Defines a common contract for permissions facades that modify actions on a secured object.
  /// The plug-ins can alter the facade behavior mostly through event interception.
  /// </summary>
  internal interface IActionsFacadePlugin
  {
    /// <summary>
    /// Subscribes to the available events of the specified <paramref name="actionsFacadeBase" />.
    /// </summary>
    /// <param name="actionsFacadeBase">The actions facade base.</param>
    void Subscribe(ActionsFacadeBase actionsFacadeBase);
  }
}
