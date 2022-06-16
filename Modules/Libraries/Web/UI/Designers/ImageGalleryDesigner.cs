// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageGalleryDesigner
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
  /// Represents the control designer for the ImageGallery public control, part of the Images module
  /// </summary>
  public class ImageGalleryDesigner : ContentViewDesignerBase
  {
    internal const string controlScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ImageGalleryDesigner.js";

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (ImageGalleryDesigner).FullName;

    /// <summary>Adds the views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      ImageGallerySelectorsDesignerView selectorsDesignerView = new ImageGallerySelectorsDesignerView();
      ImageGallerySettingsDesignerView settingsDesignerView = new ImageGallerySettingsDesignerView();
      views.Add(selectorsDesignerView.ViewName, (ControlDesignerView) selectorsDesignerView);
      views.Add(settingsDesignerView.ViewName, (ControlDesignerView) settingsDesignerView);
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("libraryType", (object) typeof (Album).FullName);
      controlDescriptor.AddProperty("selectorViewName", (object) "imageGallerySelectorDesignerView");
      controlDescriptor.AddProperty("settingsViewName", (object) "imageGallerySettingsDesignerView");
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ImageGalleryDesigner.js", typeof (ImageGalleryDesigner).Assembly.FullName)
    };
  }
}
