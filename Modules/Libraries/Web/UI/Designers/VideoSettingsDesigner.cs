// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.VideoSettingsDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Videos;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>A designer for the Video control</summary>
  public class VideoSettingsDesigner : ContentViewDesignerBase
  {
    internal const string videoSettingsDesignerScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.VideoSettingsDesigner.js";
    private bool isProviderCorrect = true;
    private UploadVideoDesignerView uploadVideoDesignerView;
    private VideoSelectorDesignerView videoSelectorDesignerView;
    private LibrariesManager manager;
    private string providerName;

    private string ProviderName
    {
      get
      {
        if (this.providerName == null)
          this.providerName = !(this.PropertyEditor.Control is MediaPlayerControl control) ? string.Empty : control.ProviderName;
        return this.providerName;
      }
    }

    private LibrariesManager Manager
    {
      get
      {
        if (this.manager == null)
          this.InitializeManager();
        return this.manager;
      }
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.isProviderCorrect)
      {
        this.TopMessageText = Res.Get<Labels>().DefinedProviderNotAvailable;
        this.TopMessageType = MessageType.Negative;
      }
      base.InitializeControls(container);
    }

    /// <inheritdoc />
    protected override void OnInit(EventArgs e)
    {
      this.PropertyEditor.InitializeProvidersSelector((IManager) this.Manager, this.ProviderName);
      base.OnInit(e);
    }

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (VideoSettingsDesigner).FullName;

    /// <summary>Adds the designer views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      this.uploadVideoDesignerView = new UploadVideoDesignerView();
      this.uploadVideoDesignerView.ProviderName = this.ProviderName.IsNullOrEmpty() ? this.Manager.Provider.Name : this.ProviderName;
      this.uploadVideoDesignerView.BindOnLoad = this.isProviderCorrect;
      views.Add(this.uploadVideoDesignerView.ViewName, (ControlDesignerView) this.uploadVideoDesignerView);
      this.videoSelectorDesignerView = new VideoSelectorDesignerView();
      this.videoSelectorDesignerView.ProviderName = this.ProviderName;
      this.videoSelectorDesignerView.BindOnLoad = this.isProviderCorrect;
      views.Add(this.videoSelectorDesignerView.ViewName, (ControlDesignerView) this.videoSelectorDesignerView);
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (VideoSettingsDesigner).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.VideoSettingsDesigner.js", assembly)
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("uploadVideoView", this.uploadVideoDesignerView.ClientID);
      controlDescriptor.AddComponentProperty("videoSelectorView", this.videoSelectorDesignerView.ClientID);
      controlDescriptor.AddComponentProperty("message", this.Message.ClientID);
      controlDescriptor.AddProperty("isProviderCorrect", (object) this.isProviderCorrect);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>Initializes the manager.</summary>
    protected virtual void InitializeManager()
    {
      if (this.PropertyEditor.Control is MediaPlayerControl control)
      {
        this.manager = control.Manager;
        this.isProviderCorrect = control.Manager.Provider.Name == control.ProviderName || control.ProviderName.IsNullOrEmpty();
      }
      else
        this.manager = LibrariesManager.GetManager();
    }
  }
}
