// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentLinkDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>A designer for the DocumentLink control</summary>
  public class DocumentLinkDesigner : ContentViewDesignerBase
  {
    internal const string documentSettingsDesignerScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.DocumentLinkDesigner.js";
    private bool isProviderCorrect = true;
    private UploadDocumentDesignerView uploadDocumentDesignerView;
    private DocumentSelectorDesignerView documentSelectorDesignerView;

    /// <inheritdoc />
    protected override string ScriptDescriptorTypeName => typeof (DocumentLinkDesigner).FullName;

    /// <summary>Gets the current DocumentLink control.</summary>
    protected DocumentLink DocumentLinkControl => this.PropertyEditor.Control as DocumentLink;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      if (!this.DocumentLinkControl.ProviderName.IsNullOrEmpty() && this.DocumentLinkControl.ProviderName != this.DocumentLinkControl.Manager.Provider.Name)
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
      this.PropertyEditor.InitializeProvidersSelector((IManager) this.DocumentLinkControl.Manager, this.DocumentLinkControl.ProviderName);
      base.OnInit(e);
    }

    /// <summary>Adds the views.</summary>
    /// <param name="views">The views.</param>
    protected override void AddViews(Dictionary<string, ControlDesignerView> views)
    {
      this.uploadDocumentDesignerView = new UploadDocumentDesignerView();
      this.uploadDocumentDesignerView.ProviderName = this.DocumentLinkControl.ProviderName.IsNullOrEmpty() ? this.DocumentLinkControl.Manager.Provider.Name : this.DocumentLinkControl.ProviderName;
      this.uploadDocumentDesignerView.BindOnLoad = this.isProviderCorrect;
      views.Add(this.uploadDocumentDesignerView.ViewName, (ControlDesignerView) this.uploadDocumentDesignerView);
      this.documentSelectorDesignerView = new DocumentSelectorDesignerView();
      this.documentSelectorDesignerView.ProviderName = this.DocumentLinkControl.ProviderName;
      this.documentSelectorDesignerView.BindOnLoad = this.isProviderCorrect;
      views.Add(this.documentSelectorDesignerView.ViewName, (ControlDesignerView) this.documentSelectorDesignerView);
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (DocumentLinkDesigner).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.DocumentLinkDesigner.js", assembly)
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) source.Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("uploadDocumentView", this.uploadDocumentDesignerView.ClientID);
      controlDescriptor.AddComponentProperty("documentSelectorView", this.documentSelectorDesignerView.ClientID);
      controlDescriptor.AddComponentProperty("message", this.Message.ClientID);
      controlDescriptor.AddProperty("isProviderCorrect", (object) this.isProviderCorrect);
      return (IEnumerable<ScriptDescriptor>) source;
    }
  }
}
