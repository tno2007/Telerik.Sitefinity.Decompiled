// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.ControlDataExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Provide extension methods for ControlData</summary>
  public static class ControlDataExtensions
  {
    /// <summary>
    /// Set default permissions for controls (they do not inherit, but have hard-coded preset ones)
    /// </summary>
    /// <param name="ctrlData">The CTRL data.</param>
    /// <param name="manager">The manager.</param>
    public static void SetDefaultPermissions(this ControlData ctrlData, IControlManager manager) => manager.SetControlDefaultPermissions(ctrlData);
  }
}
