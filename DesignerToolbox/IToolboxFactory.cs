// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DesignerToolbox.IToolboxFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.DesignerToolbox
{
  /// <summary>
  /// Defines an interface toolbox factory implementes. Toolbox factory is used to load various
  /// design time toolboxes in Sitefinity.
  /// </summary>
  public interface IToolboxFactory
  {
    /// <summary>Resolves the toolbox with the specified name.</summary>
    /// <param name="toolboxName">The name of the toolbox to be resolved.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// Thrown if toolbox name is not provided.
    /// </exception>
    /// <returns>
    /// An <see cref="T:Telerik.Sitefinity.DesignerToolbox.IToolbox" /> instance or <c>null</c>, if not found.
    /// </returns>
    IToolbox ResolveToolbox(string toolboxName);
  }
}
