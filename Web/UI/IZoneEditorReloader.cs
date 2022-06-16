// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IZoneEditorReloader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Allows controls the ability to reload other controls in ZoneEditor
  /// when it is processed by the ZoneEditor.
  /// </summary>
  public interface IZoneEditorReloader
  {
    /// <summary>
    /// Defines whether controls of same key will be
    /// reloaded when this control is updated in the ZoneEditor
    /// </summary>
    /// <returns>True if a reload is required</returns>
    [Browsable(false)]
    bool ShouldReloadControlsWithSameKey();

    /// <summary>
    /// Gets unique reload data (i.e. all controls with the same key will get reloaded)
    /// </summary>
    [Browsable(false)]
    string Key { get; }
  }
}
