// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.DocumentSelectorDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Documents;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A designer view for the DocumentLink control, letting you select from already uploaded documents
  /// </summary>
  public class DocumentSelectorDesignerView : ContentViewDesignerView
  {
    private bool bindOnLoad = true;
    internal const string IDesignerViewControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";
    private const string documentLibraryServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
    internal const string DocumentSelectorJs = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.DocumentSelectorDesignerView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Documents.DocumentSelectorDesignerView.ascx");

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "documentSelectorDesignerView";

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<DocumentsResources>().FromAlreadyUploadedDocuments;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DocumentSelectorDesignerView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

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

    /// <summary>Gets the reference to the selectorView control.</summary>
    protected virtual MediaContentSelectorView MediaSelectorView => this.Container.GetControl<MediaContentSelectorView>("selectorView", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.MediaSelectorView.LibraryServiceUrl = "~/Sitefinity/Services/Content/DocumentLibraryService.svc/folders/";
      this.MediaSelectorView.ProviderName = this.ProviderName;
      this.MediaSelectorView.ContentType = typeof (Document).FullName;
      this.MediaSelectorView.ParentType = typeof (DocumentLibrary).FullName;
      this.MediaSelectorView.ItemsName = Res.Get<DocumentsResources>().Documents;
      this.MediaSelectorView.ItemName = Res.Get<DocumentsResources>().Document;
      this.MediaSelectorView.ItemNameWithArticle = Res.Get<DocumentsResources>().DocumentWithArticle;
      this.MediaSelectorView.BindOnLoad = this.BindOnLoad;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (DocumentSelectorDesignerView).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("mediaContentSelector", this.MediaSelectorView.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (DocumentSelectorDesignerView).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.DocumentSelectorDesignerView.js"
        }
      };
    }
  }
}
