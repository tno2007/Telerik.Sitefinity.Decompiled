// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadMediaContentDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A base upload view for all media content controls, letting you upload a new file
  /// </summary>
  public class UploadMediaContentDesignerView : ContentViewDesignerView
  {
    internal const string uploadServiceUrl = "~/Telerik.Sitefinity.AsyncImageUploadHandler.ashx";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";
    internal const string MediaContentJs = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.UploadMediaContentDesignerView.js";
    internal const string IDesignerViewControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    internal const string AjaxUploadJs = "Telerik.Sitefinity.Resources.Scripts.ajaxupload.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Images.UploadImageDesignerView.ascx");
    private string providerName;
    private bool providerNameSet;
    private bool bindOnLoad = true;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? UploadMediaContentDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
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

    /// <summary>Text box that shows the selected file name</summary>
    protected internal virtual TextBox FileNameTextBox => this.Container.GetControl<TextBox>("fileNameTextBox", true);

    /// <summary>The button that invokes the select file functionality</summary>
    protected internal virtual LinkButton SelectFileButton => this.Container.GetControl<LinkButton>("selectFileButton", true);

    /// <summary>
    /// Represents the label of the library selector
    /// Represents the label of the library selector
    /// </summary>
    protected virtual Label LibrarySelectorTitle => this.Container.GetControl<Label>("librarySelectorTitle", true);

    /// <summary>Gets a reference to the parent library selector.</summary>
    protected virtual FolderField ParentLibrarySelector => this.Container.GetControl<FolderField>("parentLibrarySelector", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ParentLibrarySelector.ProviderName = this.ProviderName;

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
      ScriptControlDescriptor designerViewDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      if (this.CurrentContentView != null && !string.IsNullOrEmpty(this.CurrentContentView.ControlDefinition.ContentType.FullName))
        designerViewDescriptor.AddProperty("_contentType", (object) this.CurrentContentView.ControlDefinition.ContentType.FullName);
      if (!this.ProviderName.IsNullOrEmpty())
        designerViewDescriptor.AddProperty("_providerName", (object) this.ProviderName);
      designerViewDescriptor.AddProperty("_uploadServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Telerik.Sitefinity.AsyncImageUploadHandler.ashx"));
      designerViewDescriptor.AddProperty("_cantUploadFilesErrorMessage", (object) Res.Get<LibrariesResources>().CantUploadFiles);
      designerViewDescriptor.AddProperty("_libraryNotSelectedErrorMessage", (object) Res.Get<LibrariesResources>().LibraryNotSelected);
      designerViewDescriptor.AddElementProperty("fileNameTextBox", this.FileNameTextBox.ClientID);
      designerViewDescriptor.AddElementProperty("selectFileButton", this.SelectFileButton.ClientID);
      designerViewDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      designerViewDescriptor.AddComponentProperty("parentLibrarySelector", this.ParentLibrarySelector.ClientID);
      return (ScriptDescriptor) designerViewDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) this.GetUploadMediaContentDesignerViewScriptReferences().ToArray<ScriptReference>();

    /// <summary>
    /// Gets the upload media content designer view script references. To be used by child classes
    /// </summary>
    /// <returns></returns>
    protected IEnumerable<ScriptReference> GetUploadMediaContentDesignerViewScriptReferences()
    {
      string assembly = typeof (UploadMediaContentDesignerView).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources"),
        new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.UploadMediaContentDesignerView.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.ajaxupload.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name)
      };
    }

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
