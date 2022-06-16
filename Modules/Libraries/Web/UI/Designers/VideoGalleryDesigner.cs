// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoGalleryDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Represents the control designer for the VideoGallery public control, part of the Videos module
  /// </summary>
  public class VideoGalleryDesigner : ContentViewDesignerBase
  {
    internal const string controlScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.VideoGalleryDesigner.js";

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (VideoGalleryDesigner).FullName;

    /// <summary>Adds the views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      VideoGallerySelectorsDesignerView selectorsDesignerView = new VideoGallerySelectorsDesignerView();
      VideoGallerySettingsDesignerView settingsDesignerView = new VideoGallerySettingsDesignerView();
      views.Add(selectorsDesignerView.ViewName, (ControlDesignerView) selectorsDesignerView);
      views.Add(settingsDesignerView.ViewName, (ControlDesignerView) settingsDesignerView);
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("libraryType", (object) typeof (VideoLibrary).FullName);
      controlDescriptor.AddProperty("selectorViewName", (object) "videoGallerySelectorDesignerView");
      controlDescriptor.AddProperty("settingsViewName", (object) "videoGallerySettingsDesignerView");
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.VideoGalleryDesigner.js", typeof (VideoGalleryDesigner).Assembly.FullName)
    };
  }
}
