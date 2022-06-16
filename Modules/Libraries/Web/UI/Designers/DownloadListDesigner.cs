// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DownloadListDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Represents the control designer for the ImageGallery public control, part of the Images module
  /// </summary>
  public class DownloadListDesigner : ContentViewDesignerBase
  {
    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ContentViewDesignerBase).FullName;

    /// <summary>Adds the views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      DownloadListSelectorDesignerView selectorDesignerView = new DownloadListSelectorDesignerView();
      DownloadListSettingsDesignerView settingsDesignerView = new DownloadListSettingsDesignerView();
      views.Add(selectorDesignerView.ViewName, (ControlDesignerView) selectorDesignerView);
      views.Add(settingsDesignerView.ViewName, (ControlDesignerView) settingsDesignerView);
    }
  }
}
