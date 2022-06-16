// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.Fluent.ITempLifecycleFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Lifecycle.Fluent
{
  /// <summary>
  /// When working with temp lifecycle version of content items.
  /// </summary>
  public interface ITempLifecycleFacade
  {
    /// <summary>Copies the temp lifecycle version to the master one.</summary>
    /// <param name="excludeVersioning">Indicates whether revision history point will be created.</param>
    /// <returns>The master facade.</returns>
    IMasterLifecycleFacade CopyToMaster(bool excludeVersioning);

    /// <summary>
    /// Same as <see cref="M:Telerik.Sitefinity.Lifecycle.Fluent.ITempLifecycleFacade.CopyToMaster(System.Boolean)" /> but also deletes the temp lifecycle version.
    /// </summary>
    /// <param name="excludeVersioning">Indicates whether revision history point will be created.</param>
    /// <returns>The master facade.</returns>
    IMasterLifecycleFacade CheckIn(bool excludeVersioning);
  }
}
