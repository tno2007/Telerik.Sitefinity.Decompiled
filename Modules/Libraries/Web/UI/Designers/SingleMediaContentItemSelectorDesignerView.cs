// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemSelectorDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A designer view which lets you you upload/select and manage a media content item.
  /// </summary>
  public class SingleMediaContentItemSelectorDesignerView : ContentViewDesignerView
  {
    internal const string DesignerViewJS = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemSelectorDesignerView.js";
    internal const string IDesignerViewControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.SingleMediaContentItemSelectorDesignerView.ascx");
    private string providerName;
    private bool providerNameSet;
    private bool bindOnLoad = true;

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => nameof (SingleMediaContentItemSelectorDesignerView);

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle
    {
      get
      {
        string viewTitle = string.Empty;
        if (this.MediaItemTypeName == typeof (Image).FullName)
          viewTitle = Res.Get<LibrariesResources>().ImageItemName;
        else if (this.MediaItemTypeName == typeof (Document).FullName)
          viewTitle = Res.Get<LibrariesResources>().DocumentItemName;
        else if (this.MediaItemTypeName == typeof (Video).FullName)
          viewTitle = Res.Get<LibrariesResources>().VideoItemName;
        return viewTitle;
      }
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SingleMediaContentItemSelectorDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
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

    /// <summary>Gets or sets whether the media item is published.</summary>
    /// <value>Gets or sets whether the media item is published.</value>
    public bool IsMediaItemPublished { get; set; }

    /// <summary>
    /// Gets the reference to the SingleMediaContentItemView control.
    /// </summary>
    /// <value>The singe item data view.</value>
    protected virtual SingleMediaContentItemView SingleItemDataView => this.Container.GetControl<SingleMediaContentItemView>("imageDataView", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SingleItemDataView.UICulture = this.GetUICulture();
      this.SingleItemDataView.ProviderName = this.ProviderName;
      this.SingleItemDataView.MediaItemTypeName = this.MediaItemTypeName;
      this.SingleItemDataView.IsMediaItemPublished = this.IsMediaItemPublished;
    }

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
      designerViewDescriptor.AddComponentProperty("imageDataView", this.SingleItemDataView.ClientID);
      return (ScriptDescriptor) designerViewDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => this.GetUploadMediaContentDesignerViewScriptReferences();

    /// <summary>
    /// Gets the upload media content designer view script references. To be used by child classes
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<ScriptReference> GetUploadMediaContentDesignerViewScriptReferences()
    {
      string assembly = typeof (SingleMediaContentItemSelectorDesignerView).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.SingleMediaContentItemSelectorDesignerView.js", assembly)
      };
    }

    /// <summary>Gets the file extension filter.</summary>
    /// <param name="contentType">Type of the content.</param>
    /// <returns></returns>
    protected string GetFileExtensionFilter(string contentType)
    {
      string fileExtensionFilter = string.Empty;
      LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
      if (contentType != null)
      {
        string lower = contentType.ToLower();
        if (!(lower == "image"))
        {
          if (!(lower == "video"))
          {
            if (lower == "document")
            {
              bool? allowedExensions = librariesConfig.Documents.AllowedExensions;
              bool flag = true;
              if (allowedExensions.GetValueOrDefault() == flag & allowedExensions.HasValue)
                fileExtensionFilter = librariesConfig.Documents.AllowedExensionsSettings;
            }
          }
          else
            fileExtensionFilter = librariesConfig.Videos.AllowedExensionsSettings;
        }
        else
          fileExtensionFilter = librariesConfig.Images.AllowedExensionsSettings;
      }
      return fileExtensionFilter;
    }
  }
}
