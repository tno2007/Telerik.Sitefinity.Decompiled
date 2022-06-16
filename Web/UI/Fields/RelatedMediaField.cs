// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ModuleEditor;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.PublicControls.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// This class extends the AssetsField class with a functionality specific for related media.
  /// </summary>
  [FieldDefinitionElement(typeof (RelatedMediaFieldDefinitionElement))]
  public class RelatedMediaField : AssetsField
  {
    private string imageGalleryMasterViewName;
    private const string ContentLinkChangeStateScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentLinkChangeState.js";
    internal const string ScriptReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.RelatedMediaField.js";
    internal const string RelatedDataFieldScript = "Telerik.Sitefinity.Web.UI.Fields.Scripts.IRelatedDataField.js";

    /// <summary>
    /// Gets or sets a value indicating whether the single image should be shown as thumbnail.
    /// </summary>
    public bool IsThumbnail { get; set; }

    /// <summary>
    /// Gets or sets whether the single image should be shown as thumbnail with the specified name.
    /// </summary>
    public string ThumbnailName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether when click the control will open the original image
    /// </summary>
    public bool OpenOriginalImageOnClick { get; set; }

    /// <summary>
    /// Gets or sets the MasterViewName of the gallery. By default set to ImagesDefinitions.FrontendMasterThumbnailLightBoxViewName
    /// </summary>
    public string ImageGalleryMasterViewName
    {
      get
      {
        if (string.IsNullOrEmpty(this.imageGalleryMasterViewName))
          this.imageGalleryMasterViewName = "ImagesFrontendThumbnailsListLightBox";
        return this.imageGalleryMasterViewName;
      }
      set => this.imageGalleryMasterViewName = value;
    }

    /// <summary>Gets or sets the related data provider.</summary>
    /// <value>The provider name.</value>
    internal string RelatedDataProvider { get; set; }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.IsModuleEnabled)
        return;
      this.SelectedAssetsList.Visible = false;
      if (this.SelectAssetsButton != null)
        this.SelectAssetsButton.Visible = false;
      if (this.Selector != null)
        this.Selector.Visible = false;
      SitefinityLabel child = new SitefinityLabel();
      child.CssClass = "sfFailure";
      child.Text = Res.Get<ModuleEditorResources>().DeletedModuleWarningForField;
      this.Controls.Add((Control) child);
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      if (this.Selector != null)
        this.Selector.HideProvidersSelector = this.RelatedDataProvider != "sf-any-site-provider";
      this.IsModuleEnabled = RelatedDataHelper.IsModuleEnabledForCurrentSite(typeof (LibrariesManager).FullName, "Libraries", this.RelatedDataProvider);
    }

    /// <inheritdoc />
    internal override void DisplaySingleImageReadMode()
    {
      base.DisplaySingleImageReadMode();
      if (!this.ImageControlReadMode.Visible)
        return;
      this.ImageControlReadMode.ThumbnailName = this.ThumbnailName;
      this.ImageControlReadMode.OpenOriginalImageOnClick = this.OpenOriginalImageOnClick;
      if (!this.IsThumbnail)
        return;
      this.ImageControlReadMode.DisplayMode = ImageDisplayMode.Thumbnail;
    }

    /// <inheritdoc />
    internal override void DisplaysMultipleImagesReadMode()
    {
      base.DisplaysMultipleImagesReadMode();
      this.ImagesViewControl.MasterViewName = this.ImageGalleryMasterViewName;
    }

    /// <inheritdoc />
    protected override string ScriptDescriptorType => typeof (RelatedMediaField).FullName;

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences()
    {
      if (this.DisplayMode == FieldDisplayMode.Read && !this.IsBackend())
        return (IEnumerable<System.Web.UI.ScriptReference>) new System.Web.UI.ScriptReference[0];
      string fullName = typeof (RelatedMediaField).Assembly.FullName;
      return (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
      {
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.IRelatedDataField.js", fullName),
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.RelatedMediaField.js", fullName),
        new System.Web.UI.ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.ContentLinkChangeState.js", fullName)
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      if (this.DisplayMode == FieldDisplayMode.Read && !this.IsBackend())
        return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[0];
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      if (this.IsModuleEnabled)
      {
        controlDescriptor.AddProperty("_relatedDataService", (object) VirtualPathUtility.ToAbsolute("~/restapi/sitefinity/related-data"));
        controlDescriptor.AddProperty("_relatedDataProvider", (object) this.RelatedDataProvider);
        return scriptDescriptors;
      }
      controlDescriptor.AddProperty("_initializeControl", (object) false);
      return scriptDescriptors;
    }

    /// <inheritdoc />
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IRelatedMediaFieldDefinition definition1))
        return;
      this.RelatedDataProvider = definition1.RelatedDataProvider;
      if (this.RelatedDataProvider == "sf-site-default-provider" || this.RelatedDataProvider == "sf-any-site-provider")
      {
        string str = RelatedDataHelper.ResolveProvider(RelatedMediaField.GetTypeName(definition1));
        this.SelectViewProviderName = str;
        this.UploadViewProviderName = str;
        if (!(this.RelatedDataProvider == "sf-site-default-provider"))
          return;
        this.RelatedDataProvider = str;
      }
      else
      {
        this.SelectViewProviderName = this.RelatedDataProvider;
        this.UploadViewProviderName = this.RelatedDataProvider;
      }
    }

    internal static string GetTypeName(IRelatedMediaFieldDefinition definition)
    {
      string typeName = (string) null;
      AssetsWorkMode? workMode = definition.WorkMode;
      if (workMode.HasValue)
      {
        switch (workMode.GetValueOrDefault())
        {
          case AssetsWorkMode.SingleImage:
          case AssetsWorkMode.MultipleImages:
            typeName = typeof (Image).FullName;
            break;
          case AssetsWorkMode.SingleDocument:
          case AssetsWorkMode.MultipleDocuments:
            typeName = typeof (Document).FullName;
            break;
          case AssetsWorkMode.SingleVideo:
          case AssetsWorkMode.MultipleVideos:
            typeName = typeof (Video).FullName;
            break;
        }
      }
      return typeName;
    }

    private bool IsModuleEnabled { get; set; }
  }
}
