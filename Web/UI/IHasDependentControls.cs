// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IHasDependentControls
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Allows controls the ability to reload other controls in ZoneEditor
  /// when it is processed by the ZoneEditor.
  /// </summary>
  public interface IHasDependentControls
  {
    /// <summary>
    /// Gets keys of controls that will be reloaded when this control is added to the ZoneEditor
    /// </summary>
    [Browsable(false)]
    IEnumerable<string> CreateDependentKeys { get; }

    /// <summary>
    ///  Gets keys of controls that will be reloaded when this control is removed from the ZoneEditor
    /// </summary>
    [Browsable(false)]
    IEnumerable<string> DeleteDependentKeys { get; }

    /// <summary>
    ///  Gets keys of controls that will be reloaded when this control is moved in the ZoneEditor
    /// </summary>
    [Browsable(false)]
    IEnumerable<string> IndexChangeDependentKeys { get; }

    /// <summary>
    ///  Gets keys of controls that will be reloaded when this control is reloaded in the ZoneEditor
    /// </summary>
    [Browsable(false)]
    IEnumerable<string> ReloadDependentKeys { get; }
  }
}
