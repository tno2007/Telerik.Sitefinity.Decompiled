// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSettingsDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A designer view that lets you configure how to display a media content item.
  /// </summary>
  public class SingleMediaContentItemSettingsDesignerView : ContentViewDesignerView
  {
    internal const string MediaContentJs = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemSettingsDesignerView.js";
    internal const string IDesignerViewControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.SingleMediaContentItemSettingsDesignerView.ascx");
    private string providerName;
    private bool providerNameSet;
    private bool bindOnLoad = true;

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => nameof (SingleMediaContentItemSettingsDesignerView);

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<LibrariesResources>().Settings;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SingleMediaContentItemSettingsDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName
    {
      get
      {
        if (!this.providerNameSet)
        {
          if (this.CurrentContentView != null)
            this.providerName = this.CurrentContentView.ControlDefinition.ProviderName;
          this.providerNameSet = true;
        }
        return this.providerName;
      }
      set
      {
        this.providerName = value;
        this.providerNameSet = true;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control will be bound on load.
    /// Default value is true.
    /// </summary>
    /// <value>
    ///   <c>true</c> if the control is bind on load; otherwise, <c>false</c>.
    /// </value>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets or sets the name of the media item type.</summary>
    /// <value>The name of the media item type.</value>
    public string MediaItemTypeName { get; set; }

    /// <summary>
    /// Gets the reference to the SingleMediaContentItemSettingsView control.
    /// </summary>
    /// <value>The single item image settings view.</value>
    protected internal virtual SingleMediaContentItemSettingsView SingleMediaContentItemSettingsView => this.Container.GetControl<SingleMediaContentItemSettingsView>("settingsView", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.SingleMediaContentItemSettingsView.MediaItemTypeName = this.MediaItemTypeName;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
    {
      this.GetUploadMediaContentDesignerViewDescriptor()
    };

    /// <summary>
    /// Gets the upload media content designer view script descriptor. To be used by child classes
    /// </summary>
    protected ScriptDescriptor GetUploadMediaContentDesignerViewDescriptor()
    {
      ScriptControlDescriptor designerViewDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorTypeName, this.ClientID);
      if (!this.ProviderName.IsNullOrEmpty())
        designerViewDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      designerViewDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      designerViewDescriptor.AddComponentProperty("settingsView", this.SingleMediaContentItemSettingsView.ClientID);
      return (ScriptDescriptor) designerViewDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (SingleMediaContentItemSettingsDesignerView).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemSettingsDesignerView.js", assembly)
      };
    }
  }
}
