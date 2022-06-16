// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.ImageSettingsDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Represents a content view designer for the Telerik.Sitefinity.Web.UI.PublicControls.ImageControl class.
  /// </summary>
  public class ImageSettingsDesigner : ContentViewDesignerBase
  {
    internal const string imageSettingsDesignerScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ImageSettingsDesigner.js";
    private string[] hiddenViews = new string[1];
    private bool isProviderCorrect = true;
    private UploadImageDesignerView uploadImageDesignerView;
    private ImageSelectorDesignerView imageSelectorDesignerView;

    protected override string[] HiddenDesignerViewNames => this.hiddenViews;

    /// <summary>Gets the current image control.</summary>
    protected ImageControl ImageControl => this.PropertyEditor.Control as ImageControl;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.ImageControl.ProviderName.IsNullOrEmpty() && this.ImageControl.ProviderName != this.ImageControl.Manager.Provider.Name)
      {
        this.TopMessageText = Res.Get<Labels>().DefinedProviderNotAvailable;
        this.TopMessageType = MessageType.Negative;
        this.isProviderCorrect = false;
      }
      base.InitializeControls(container);
    }

    /// <inheritdoc />
    protected override void OnInit(EventArgs e)
    {
      this.PropertyEditor.InitializeProvidersSelector((IManager) this.ImageControl.Manager, this.ImageControl.ProviderName);
      base.OnInit(e);
    }

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      this.uploadImageDesignerView = new UploadImageDesignerView();
      this.uploadImageDesignerView.ProviderName = this.ImageControl.ProviderName.IsNullOrEmpty() ? this.ImageControl.Manager.Provider.Name : this.ImageControl.ProviderName;
      this.uploadImageDesignerView.BindOnLoad = this.isProviderCorrect;
      views.Add(this.uploadImageDesignerView.ViewName, (ControlDesignerView) this.uploadImageDesignerView);
      this.imageSelectorDesignerView = new ImageSelectorDesignerView();
      this.imageSelectorDesignerView.ProviderName = this.ImageControl.ProviderName;
      this.imageSelectorDesignerView.BindOnLoad = this.isProviderCorrect;
      views.Add(this.imageSelectorDesignerView.ViewName, (ControlDesignerView) this.imageSelectorDesignerView);
      InsertEditImageDesignerView imageDesignerView = new InsertEditImageDesignerView();
      views.Add(imageDesignerView.ViewName, (ControlDesignerView) imageDesignerView);
      this.hiddenViews[0] = imageDesignerView.ViewName;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (ImageSettingsDesigner).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.ImageSettingsDesigner.js", assembly)
      };
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("stringInsertImage", (object) Res.Get<LibrariesResources>().ImageControlPropertyEditorTitle);
      controlDescriptor.AddProperty("stringEditImage", (object) Res.Get<ImagesResources>().EditImage);
      controlDescriptor.AddProperty("stringSelectImage", (object) Res.Get<ImagesResources>().SelectAnImage);
      controlDescriptor.AddProperty("isProviderCorrect", (object) this.isProviderCorrect);
      controlDescriptor.AddComponentProperty("uploadImageView", this.uploadImageDesignerView.ClientID);
      controlDescriptor.AddComponentProperty("imageSelectorView", this.imageSelectorDesignerView.ClientID);
      controlDescriptor.AddComponentProperty("message", this.Message.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }
  }
}
