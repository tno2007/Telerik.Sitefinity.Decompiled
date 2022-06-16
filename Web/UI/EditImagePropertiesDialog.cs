// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.EditImagePropertiesDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// RadEditor Manager dialog for inserting image, document and video.
  /// </summary>
  public class EditImagePropertiesDialog : AjaxDialogBase
  {
    private const string imageServiceBaseUrl = "/Sitefinity/Services/Content/ImageService.svc";
    private string bodyCssClass = "sfSelectorDialog";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.EditImagePropertiesDialog.ascx");
    internal const string script = "Telerik.Sitefinity.Web.Scripts.EditImagePropertiesDialog.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EditImagePropertiesDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Represents css class to be added to the body tag of the dialog
    /// </summary>
    public string BodyCssClass
    {
      get => this.bodyCssClass;
      set => this.bodyCssClass = value;
    }

    /// <summary>Gets the reference to the save link button.</summary>
    protected virtual HyperLink SaveLink => this.Container.GetControl<HyperLink>("saveLink", true);

    /// <summary>Gets the reference to the cancel link button.</summary>
    /// <value>The cancel link button.</value>
    protected virtual HyperLink CancelLink => this.Container.GetControl<HyperLink>("cancelLink", true);

    /// <summary>Gets the reference to the librarySelector control.</summary>
    protected virtual LibrarySelectorField LibrarySelector => this.Container.GetControl<LibrarySelectorField>("librarySelector", true);

    /// <summary>
    /// Gets a reference to the div which contains the inputs that correspond to the image properties.
    /// </summary>
    protected virtual Control ExtendedDetailsPanel => this.Container.GetControl<Control>("extendedDetailsPanel", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      this.Controls.Add((Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string name = typeof (ImagesResources).Name;
      this.LibrarySelector.ContentType = typeof (Album);
      this.LibrarySelector.DisplayMode = FieldDisplayMode.Write;
      this.LibrarySelector.CssClass = "sfChangeAlbum";
      this.LibrarySelector.DataFieldName = "Album";
      this.LibrarySelector.Title = Res.Get<ImagesResources>().Album;
      this.LibrarySelector.ExpandableControlDefinition.Expanded = new bool?(false);
      this.LibrarySelector.ExpandableControlDefinition.ExpandText = Res.Get<ImagesResources>().ChangeAlbumInSpan;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.Type = this.GetType().FullName;
      controlDescriptor.AddElementProperty("saveLink", this.SaveLink.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      controlDescriptor.AddElementProperty("librarySelectorField", this.LibrarySelector.ClientID);
      controlDescriptor.AddElementProperty("extendedDetailsPanel", this.ExtendedDetailsPanel.ClientID);
      if (!string.IsNullOrEmpty(this.BodyCssClass))
        controlDescriptor.AddProperty("_bodyCssClass", (object) this.BodyCssClass);
      controlDescriptor.AddProperty("_serviceBase", (object) "/Sitefinity/Services/Content/ImageService.svc");
      controlDescriptor.AddProperty("_itemType", (object) typeof (Telerik.Sitefinity.Libraries.Model.Image).FullName);
      controlDescriptor.AddProperty("_parentItemType", (object) typeof (Album).FullName);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.EditImagePropertiesDialog.js", typeof (EditImagePropertiesDialog).Assembly.FullName)
    };
  }
}
