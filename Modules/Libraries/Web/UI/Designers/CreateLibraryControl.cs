// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.CreateLibraryControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  public class CreateLibraryControl : SimpleScriptView
  {
    private static RegexStrategy regexStrategy = (RegexStrategy) null;
    private string libraryTypeName;
    private Type libraryType;
    private const string createLibraryServicePath = "~/Sitefinity/Services/Content/AlbumService.svc/";
    private const string selectorScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.CreateLibraryControl.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.CreateLibraryControl.ascx");

    public CreateLibraryControl() => this.CreateLibraryServicePath = "~/Sitefinity/Services/Content/AlbumService.svc/";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => base.LayoutTemplatePath ?? CreateLibraryControl.layoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the name of the library type.</summary>
    public virtual string LibraryTypeName
    {
      get => this.libraryTypeName;
      set
      {
        this.libraryTypeName = value;
        if (string.IsNullOrEmpty(value))
          return;
        this.libraryType = TypeResolutionService.ResolveType(value);
      }
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the folders selector service URL.</summary>
    public string FoldersSelectorServiceUrl { get; set; }

    /// <summary>Gets or sets the create library service url.</summary>
    public virtual string CreateLibraryServicePath { get; set; }

    private Literal TitleLiteral => this.Container.GetControl<Literal>("Title", true);

    private TextField CreateLibraryTxt => this.Container.GetControl<TextField>("libraryTitle", true);

    private LinkButton CreateButton => this.Container.GetControl<LinkButton>("lnkCreateLibrary", true);

    private Literal CreateButtonText => this.Container.GetControl<Literal>("CreateLibraryLiteral", true);

    private LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>
    /// Gets the reference to the radio button control
    /// property is set to <see cref="!:RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual RadioButton NoParent => this.Container.GetControl<RadioButton>("noParent", true);

    /// <summary>
    /// Gets the reference to the radio button control
    /// property is set to <see cref="!:RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual RadioButton SelectParent => this.Container.GetControl<RadioButton>("selectParent", true);

    /// <summary>
    /// Gets the reference to the Folder field control
    /// property is set to <see cref="!:RenderChoicesAs.RadioButtons" />
    /// </summary>
    protected internal virtual FolderSelector ParentLibrarySelector => this.Container.GetControl<FolderSelector>("parentLibrarySelector", true);

    public virtual bool CanCreateLibrary()
    {
      try
      {
        int num;
        if (!string.IsNullOrEmpty(this.libraryType.Name))
          num = LibrariesManager.GetManager(this.ProviderName).Provider.SecurityRoot.IsGranted(this.libraryType.Name, "Create" + this.libraryType.Name) ? 1 : 0;
        else
          num = 0;
        return num != 0;
      }
      catch
      {
        return false;
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      LibrariesResources librariesResources = Res.Get<LibrariesResources>();
      this.CreateButtonText.Text = this.TitleLiteral.Text = string.Format(librariesResources.CreateThisLibrary, (object) librariesResources.Library.ToLower());
      this.CreateLibraryTxt.Title = string.Format(librariesResources.LibraryNameText, (object) librariesResources.Library);
      this.CreateLibraryTxt.Example = librariesResources.LibraryNameExample;
      this.ParentLibrarySelector.WebServiceUrl = this.FoldersSelectorServiceUrl;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (CreateLibraryControl).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("createButton", this.CreateButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddComponentProperty("createLibraryTxt", this.CreateLibraryTxt.ClientID);
      controlDescriptor.AddProperty("_createLibraryWebServiceUrl", (object) VirtualPathUtility.ToAbsolute(this.CreateLibraryServicePath));
      controlDescriptor.AddProperty("_libraryType", (object) this.libraryType.FullName);
      controlDescriptor.AddProperty("_provider", (object) this.ProviderName);
      controlDescriptor.AddProperty("_albumNameExpression", (object) ("[^" + CreateLibraryControl.RgxStrategy.UrlRegExBase + "]+|\\.+$"));
      controlDescriptor.AddComponentProperty("parentLibrarySelector", this.ParentLibrarySelector.ClientID);
      controlDescriptor.AddElementProperty("noParent", this.NoParent.ClientID);
      controlDescriptor.AddElementProperty("selectParent", this.SelectParent.ClientID);
      if (this.CanCreateLibrary())
        controlDescriptor.AddProperty("_blankLibraryDataItem", (object) this.CreateBlankDataItem(this.libraryType).ToJson(this.libraryType));
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.CreateLibraryControl.js", typeof (CreateLibraryControl).Assembly.FullName)
    };

    private object CreateBlankDataItem(Type contentType) => !this.CanCreateLibrary() ? (object) null : ManagerBase.GetMappedManager(contentType).CreateItem(contentType, Guid.Empty);

    private static RegexStrategy RgxStrategy
    {
      get
      {
        if (CreateLibraryControl.regexStrategy == null)
          CreateLibraryControl.regexStrategy = ObjectFactory.Resolve<RegexStrategy>();
        return CreateLibraryControl.regexStrategy;
      }
    }
  }
}
