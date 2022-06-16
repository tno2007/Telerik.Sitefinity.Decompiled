// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.UploadDocumentDesignerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>
  /// A designer view for the DocumentLink designer, letting you upload a new document
  /// </summary>
  public class UploadDocumentDesignerView : UploadMediaContentDesignerView
  {
    private string templatePath;
    internal const string DocumentJs = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.UploadDocumentDesignerView.js";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.Documents.UploadDocumentDesignerView.ascx");

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => "uploadDocumentDesignerView";

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<LibrariesResources>().FromYourComputer;

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
      get => string.IsNullOrEmpty(this.templatePath) ? UploadDocumentDesignerView.layoutTemplatePath : this.templatePath;
      set => this.templatePath = value;
    }

    /// <summary>Gets the document settings panel.</summary>
    /// <value>The document settings panel.</value>
    protected internal virtual Panel DocumentSettingsPanel => this.Container.GetControl<Panel>("documentSettingsPanel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.DocumentSettingsPanel.Style.Add("display", "none");
      this.ParentLibrarySelector.ItemName = Res.Get<LibrariesResources>().DocumentItemName;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor designerViewDescriptor = (ScriptControlDescriptor) this.GetUploadMediaContentDesignerViewDescriptor();
      designerViewDescriptor.AddElementProperty("settingsPanel", this.DocumentSettingsPanel.ClientID);
      Type type = typeof (Document);
      designerViewDescriptor.AddProperty("_contentType", (object) type.FullName);
      designerViewDescriptor.AddProperty("_libraryItemType", (object) typeof (DocumentLibrary).FullName);
      designerViewDescriptor.AddProperty("_fileAllowedExtensions", (object) this.GetFileExtensionFilter(type.Name));
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        designerViewDescriptor
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
      IList<ScriptReference> scriptReferences = (IList<ScriptReference>) this.GetUploadMediaContentDesignerViewScriptReferences();
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.UploadDocumentDesignerView.js", typeof (UploadDocumentDesignerView).Assembly.GetName().ToString()));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
