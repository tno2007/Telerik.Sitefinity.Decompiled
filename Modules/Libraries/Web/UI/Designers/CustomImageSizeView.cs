// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CustomImageSizeView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.ImageProcessing;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// Component that represents a simple script view for custom image size properties
  /// </summary>
  public class CustomImageSizeView : SimpleScriptView
  {
    private const string viewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.CustomImageSizeView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.CustomImageSizeView.ascx");
    private IImageProcessor imageProcessor;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CustomImageSizeView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The tag name of the base HTML element rendered by the control.
    /// </summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private IImageProcessor ImageProcessor
    {
      get
      {
        if (this.imageProcessor == null)
          this.imageProcessor = ObjectFactory.Resolve<IImageProcessor>();
        return this.imageProcessor;
      }
    }

    /// <summary>
    /// Represents the DIV containing the custom image size controls.
    /// </summary>
    protected virtual HtmlGenericControl CustomImgSizeWrp => this.Container.GetControl<HtmlGenericControl>("customImgSizeWrp", true);

    /// <summary>
    /// Represents the drop down list from which the image processing method is chosen as part of the Custom size...
    /// </summary>
    protected virtual ChoiceField SelectImageProcessingMethodControl => this.Container.GetControl<ChoiceField>("selectImageProcessingMethod", true);

    /// <summary>
    /// Represents the DIV containing the processing method parameter fields
    /// </summary>
    protected virtual HtmlGenericControl ProcessingMethodParamsWrp => this.Container.GetControl<HtmlGenericControl>("processingMethodParamsWrp", true);

    /// <summary>
    /// The link for display of more details for the image processing method
    /// </summary>
    protected virtual HyperLink MethodMoreDetailsLink => this.Container.GetControl<HyperLink>("methodMoreDetails", true);

    /// <summary>
    /// Represents the DIV containing the image processing method details.
    /// </summary>
    protected virtual HtmlGenericControl MethodDetailsPopupControl => this.Container.GetControl<HtmlGenericControl>("methodDetailsPopup", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (CustomImageSizeView).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("customImgSizeWrp", this.CustomImgSizeWrp.ClientID);
      controlDescriptor.AddComponentProperty("selectImageProcessingMethod", this.SelectImageProcessingMethodControl.ClientID);
      controlDescriptor.AddElementProperty("processingMethodParamsWrp", this.ProcessingMethodParamsWrp.ClientID);
      controlDescriptor.AddProperty("libraryType", (object) typeof (Album).FullName);
      string virtualPath = string.Format("~/{0}", (object) "Sitefinity/Services/ThumbnailService.svc");
      controlDescriptor.AddProperty("thumbnailServiceUrl", (object) VirtualPathUtility.ToAbsolute(virtualPath));
      controlDescriptor.AddProperty("imageProcessingMethods", this.GetImageProcessingMethodsDetails());
      controlDescriptor.AddElementProperty("methodMoreDetailsLink", this.MethodMoreDetailsLink.ClientID);
      controlDescriptor.AddElementProperty("methodDetailsPopupControl", this.MethodDetailsPopupControl.ClientID);
      controlDescriptor.AddProperty("requiredFieldMessage", (object) Res.Get<ImagesResources>().RequiredField);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.CustomImageSizeView.js", typeof (CustomImageSizeView).Assembly.GetName().ToString())
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      foreach (ImageProcessingMethod processingMethod in (IEnumerable<ImageProcessingMethod>) this.ImageProcessor.Methods.Values.Where<ImageProcessingMethod>((Func<ImageProcessingMethod, bool>) (m => m.IsBrowsable)).ToList<ImageProcessingMethod>())
        this.SelectImageProcessingMethodControl.Choices.Add(new ChoiceItem()
        {
          Value = processingMethod.MethodKey,
          Text = processingMethod.GetTitle()
        });
    }

    private object GetImageProcessingMethodsDetails() => (object) this.ImageProcessor.Methods.Values.Where<ImageProcessingMethod>((Func<ImageProcessingMethod, bool>) (m => m.IsBrowsable)).Select(m => new
    {
      MethodKey = m.MethodKey,
      Title = m.GetTitle(),
      DescriptionText = m.DescriptionText,
      DescriptionImageUrl = !string.IsNullOrEmpty(m.DescriptionImageResourceName) ? this.Page.ClientScript.GetWebResourceUrl(m.AssemblyInfo, m.DescriptionImageResourceName) : (string) null
    }).ToList();
  }
}
