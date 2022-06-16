// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Represents a content view designer for uploading or selecting single media item public controls.
  /// </summary>
  public class SingleMediaContentItemDesigner : ContentViewDesignerBase
  {
    internal const string designerScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemDesigner.js";
    private bool isProviderCorrect = true;
    private SingleMediaContentItemSelectorDesignerView selectorDesignerView;
    private SingleMediaContentItemSettingsDesignerView settingsDesignerView;

    protected object Control => this.PropertyEditor.Control;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (((IWidgetData) this.Control).DefinedProviderNotAvailable())
      {
        this.TopMessageText = Res.Get<Labels>().DefinedProviderNotAvailable;
        this.TopMessageType = MessageType.Negative;
        this.isProviderCorrect = false;
      }
      base.InitializeControls(container);
    }

    /// <inheritdoc />
    protected override void OnInit(EventArgs e) => base.OnInit(e);

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      this.selectorDesignerView = new SingleMediaContentItemSelectorDesignerView();
      IWidgetData control = (IWidgetData) this.Control;
      this.selectorDesignerView.ProviderName = control.GetProviderName();
      this.selectorDesignerView.BindOnLoad = this.isProviderCorrect;
      this.selectorDesignerView.MediaItemTypeName = control.ContentType;
      this.selectorDesignerView.IsMediaItemPublished = this.IsMediaItemPublished(this.Control);
      views.Add(this.selectorDesignerView.ViewName, (ControlDesignerView) this.selectorDesignerView);
      this.settingsDesignerView = new SingleMediaContentItemSettingsDesignerView();
      this.settingsDesignerView.ProviderName = control.GetProviderName();
      this.settingsDesignerView.BindOnLoad = this.isProviderCorrect;
      this.settingsDesignerView.MediaItemTypeName = control.ContentType;
      views.Add(this.settingsDesignerView.ViewName, (ControlDesignerView) this.settingsDesignerView);
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (SingleMediaContentItemDesigner).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemDesigner.js", assembly)
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
      controlDescriptor.AddProperty("stringImageTitle", (object) this.GetDesignerTitle());
      controlDescriptor.AddProperty("isProviderCorrect", (object) this.isProviderCorrect);
      controlDescriptor.AddComponentProperty("uploadImageView", this.selectorDesignerView.ClientID);
      controlDescriptor.AddComponentProperty("imageSelectorView", this.settingsDesignerView.ClientID);
      controlDescriptor.AddComponentProperty("message", this.Message.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <inheritdoc />
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.JQueryUI;

    /// <summary>Gets the designer title.</summary>
    /// <returns></returns>
    private string GetDesignerTitle()
    {
      string designerTitle = string.Empty;
      string contentType = ((IWidgetData) this.Control).ContentType;
      if (contentType == typeof (Image).FullName)
        designerTitle = Res.Get<LibrariesResources>().ImageItemName;
      else if (contentType == typeof (Document).FullName)
        designerTitle = Res.Get<LibrariesResources>().DocumentItemName;
      else if (contentType == typeof (Video).FullName)
        designerTitle = Res.Get<LibrariesResources>().VideoItemName;
      return designerTitle;
    }

    private bool IsMediaItemPublished(object item)
    {
      ImageControl image = item as ImageControl;
      DocumentLink document = item as DocumentLink;
      MediaPlayerControl video = item as MediaPlayerControl;
      LibrariesManager manager = LibrariesManager.GetManager();
      bool flag = false;
      if (image != null)
      {
        if (image.ImageId == Guid.Empty)
          return true;
        flag = manager.GetImages().Where<Image>((Expression<Func<Image, bool>>) (i => i.Id == image.ImageId)).Any<Image>(PredefinedFilters.PublishedItemsFilter<Image>());
      }
      else if (document != null)
      {
        if (document.DocumentId == Guid.Empty)
          return true;
        flag = manager.GetDocuments().Where<Document>((Expression<Func<Document, bool>>) (d => d.Id == document.DocumentId)).Any<Document>(PredefinedFilters.PublishedItemsFilter<Document>());
      }
      else if (video != null)
      {
        if (video.MediaContentId == Guid.Empty)
          return true;
        flag = manager.GetVideos().Where<Video>((Expression<Func<Video, bool>>) (d => d.Id == video.MediaContentId)).Any<Video>(PredefinedFilters.PublishedItemsFilter<Video>());
      }
      return flag;
    }
  }
}
