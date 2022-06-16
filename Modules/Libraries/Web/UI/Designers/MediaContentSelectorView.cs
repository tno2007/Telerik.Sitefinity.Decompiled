// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.MediaContentSelectorView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Folders.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  /// <summary>Base view for selecting a single media content item.</summary>
  public class MediaContentSelectorView : SimpleScriptView
  {
    private bool? addCultureToFilter;
    private const string itemFilter = "Visible == true AND Status == Live";
    private bool showOpenOriginalSizeCheckBox = true;
    public Guid sourceLibraryId = Guid.Empty;
    private bool bindOnLoad = true;
    private const string viewScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaContentSelectorView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.MediaContentSelectorView.ascx");
    private bool showLibFilterWrp = true;
    private bool showBreadcrumb = true;
    private bool showSearchBox = true;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MediaContentSelectorView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the type of the content.</summary>
    /// <value>The type of the content.</value>
    public string ContentType { get; set; }

    /// <summary>Gets or sets the type of the parent.</summary>
    /// <value>The type of the parent.</value>
    public string ParentType { get; set; }

    /// <summary>Gets or sets the media content binder service URL.</summary>
    public string MediaContentBinderServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the ItemDescription template.
    /// </summary>
    public string MediaContentItemsListDescriptionTemplate { get; set; }

    /// <summary>
    /// A localized string representing the item in plural (for example Images).
    /// </summary>
    public string ItemsName { get; set; }

    /// <summary>
    /// A localized string representing the item in singular (for example Image).
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// A localized string representing the item in singular with an article in front of it (for example an image).
    /// </summary>
    protected internal virtual string ItemNameWithArticle { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether ResizingOptionsControl control should be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if ResizingOptionsControl control should be displayed; otherwise, <c>false</c>.
    /// </value>
    public bool DisplayResizingOptionsControl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the "open original size" check box.
    /// </summary>
    public bool ShowOpenOriginalSizeCheckBox
    {
      get => this.showOpenOriginalSizeCheckBox;
      set => this.showOpenOriginalSizeCheckBox = value;
    }

    /// <summary>
    /// Gets or sets the id where the uploaded content will be displayed.
    /// </summary>
    /// <value>The id where of the library from which to show uploaded content.</value>
    public Guid SourceLibraryId
    {
      get => this.sourceLibraryId;
      set => this.sourceLibraryId = value;
    }

    /// <summary>Gets or sets the provider name.</summary>
    /// <value>The type of the content.</value>
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

    /// <summary>
    /// Gets or sets a value indicating whether to add the culture of the property editor to the filter.
    /// </summary>
    /// <value><c>true</c> if the culture will be added to the filter; otherwise, <c>false</c>.</value>
    /// <remarks>
    /// If not set a value, the property will return the value of the <see cref="M:Telerik.Sitefinity.Model.IAppSettings.Multilingual" />.;
    /// </remarks>
    public bool AddCultureToFilter
    {
      get
      {
        if (!this.addCultureToFilter.HasValue)
          this.addCultureToFilter = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual);
        return this.addCultureToFilter.Value;
      }
      set => this.addCultureToFilter = new bool?(value);
    }

    private string CurrentCulture => this.Page == null ? (string) null : this.Page.Request.QueryString["propertyValueCulture"];

    /// <summary>Gets or sets the service URL used for the library.</summary>
    public string LibraryServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets whether library filter wrapper should be visible
    /// </summary>
    internal bool ShowLibFilterWrp
    {
      get => this.showLibFilterWrp;
      set => this.showLibFilterWrp = value;
    }

    /// <summary>Gets or sets whether breadcrumb should be visible</summary>
    internal bool ShowBreadcrumb
    {
      get => this.showBreadcrumb;
      set => this.showBreadcrumb = value;
    }

    /// <summary>Gets or sets whether search box should be visible</summary>
    internal bool ShowSearchBox
    {
      get => this.showSearchBox;
      set => this.showSearchBox = value;
    }

    /// <summary>Gets a reference to the RolesPanel div panel.</summary>
    public virtual Panel LibraryFilterWrp => this.Container.GetControl<Panel>("libraryFilterWrp", true);

    /// <summary>Gets the reference to mediaContentItemsList control.</summary>
    protected virtual ItemsList MediaContentItemsList => this.Container.GetControl<ItemsList>("mediaContentItemsList", true);

    /// <summary>Gets the reference to resizingOptionsControl.</summary>
    protected virtual ResizingOptionsControl ResizingOptionsControl => this.Container.GetControl<ResizingOptionsControl>("resizingOptionsControl", true);

    /// <summary>Gets the reference to the searchBox control.</summary>
    protected virtual BinderSearchBox SearchBox => this.Container.GetControl<BinderSearchBox>("searchBox", true);

    /// <summary>Gets the reference to the page selector control.</summary>
    protected internal virtual GenericPageSelector LibrarySelector => this.Container.GetControl<GenericPageSelector>("librarySelector", true);

    /// <summary>Gets the command bar.</summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar CommandBar => this.Container.GetControl<CommandBar>("commandBar", true);

    /// <summary>Gets the breadcrumb</summary>
    protected internal virtual MediaContentBreadcrumb BreadCrumb => this.Container.GetControl<MediaContentBreadcrumb>("mediaContentBreadcrumb", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InitializeLists();
      if (this.AddCultureToFilter && this.CurrentCulture != null)
      {
        this.MediaContentItemsList.ConstantFilter = "Visible == true AND Status == Live" + string.Format(" AND Culture == {0}", (object) this.CurrentCulture);
        this.MediaContentItemsList.UICulture = this.CurrentCulture;
      }
      else
        this.MediaContentItemsList.ConstantFilter = "Visible == true AND Status == Live";
      if (this.ShowLibFilterWrp)
      {
        string str = VirtualPathUtility.AppendTrailingSlash(this.LibraryServiceUrl);
        this.LibrarySelector.BindOnLoad = true;
        this.LibrarySelector.WebServiceUrl = str;
        this.LibrarySelector.OrginalServiceBaseUrl = str;
        this.LibrarySelector.ServiceChildItemsBaseUrl = str;
        this.LibrarySelector.ServicePredecessorBaseUrl = str + "predecessors/";
        this.LibrarySelector.ServiceTreeUrl = str + "tree/";
        this.LibrarySelector.AllowSearch = false;
        this.LibrarySelector.ConstantFilter = (string) null;
        foreach (ToolboxItemBase command in this.CommandBar.Commands)
        {
          if (command is ICommandButton)
          {
            CommandToolboxItem commandToolboxItem = (CommandToolboxItem) command;
            string commandName = commandToolboxItem.CommandName;
            if (!(commandName == "showRecent"))
            {
              if (!(commandName == "showMy"))
              {
                if (commandName == "showAll")
                  commandToolboxItem.Text = string.Format(Res.Get<LibrariesResources>().ShowAll, (object) this.ItemsName.ToLower());
              }
              else
                commandToolboxItem.Text = string.Format(Res.Get<LibrariesResources>().ShowMy, (object) this.ItemsName.ToLower());
            }
            else
              commandToolboxItem.Text = string.Format(Res.Get<LibrariesResources>().ShowRecent, (object) this.ItemsName.ToLower());
          }
        }
      }
      else
        this.LibraryFilterWrp.Visible = false;
      if (this.ShowBreadcrumb)
      {
        this.BreadCrumb.BaseServiceUrl = this.LibraryServiceUrl;
        this.BreadCrumb.RootFolderName = string.Format(Res.Get<LibrariesResources>().ShowAll, (object) this.ItemsName.ToLower());
      }
      else
        this.BreadCrumb.Visible = false;
      if (!this.ShowSearchBox)
        this.SearchBox.Visible = false;
      this.LibrarySelector.BindOnLoad = this.BindOnLoad;
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
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (MediaContentSelectorView).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("mediaContentItemsList", this.MediaContentItemsList.ClientID);
      controlDescriptor.AddComponentProperty("mediaContentBinder", this.MediaContentItemsList.Binder.ClientID);
      if (this.DisplayResizingOptionsControl)
        controlDescriptor.AddComponentProperty("resizingOptionsControl", this.ResizingOptionsControl.ClientID);
      if (this.ShowSearchBox)
        controlDescriptor.AddComponentProperty("searchBox", this.SearchBox.ClientID);
      controlDescriptor.AddProperty("_parentType", (object) this.ParentType);
      controlDescriptor.AddProperty("_contentType", (object) this.ContentType);
      controlDescriptor.AddProperty("_provider", (object) this.ProviderName);
      controlDescriptor.AddProperty("_showLibFilterWrp", (object) this.ShowLibFilterWrp);
      if (!string.IsNullOrEmpty(this.MediaContentBinderServiceUrl))
      {
        string absolute = VirtualPathUtility.ToAbsolute(VirtualPathUtility.RemoveTrailingSlash(this.MediaContentBinderServiceUrl));
        controlDescriptor.AddProperty("_mediaContentBinderServiceUrl", (object) absolute);
        string str = absolute + "/parent/";
        controlDescriptor.AddProperty("_mediaContentBinderServiceChildItemsUrl", (object) str);
      }
      controlDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      if (this.ShowLibFilterWrp)
      {
        controlDescriptor.AddComponentProperty("librarySelector", this.LibrarySelector.ClientID);
        controlDescriptor.AddComponentProperty("commandBar", this.CommandBar.ClientID);
      }
      controlDescriptor.AddProperty("currentUserId", (object) SecurityManager.GetCurrentUserId());
      if (this.ShowBreadcrumb)
        controlDescriptor.AddComponentProperty("mediaContentBreadcrumb", this.BreadCrumb.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.MediaContentSelectorView.js", typeof (MediaContentSelectorView).Assembly.FullName)
    };

    private void InitializeLists()
    {
      if (this.DisplayResizingOptionsControl)
      {
        this.ResizingOptionsControl.ItemName = this.ItemName;
        this.ResizingOptionsControl.ItemsName = this.ItemsName;
        this.ResizingOptionsControl.ShowOpenOriginalSizeCheckBox = this.ShowOpenOriginalSizeCheckBox;
      }
      else
        this.ResizingOptionsControl.Visible = false;
      Assembly assembly = Assembly.Load("Telerik.Sitefinity.Resources");
      string str = string.Empty;
      string descriptionTemplate = this.MediaContentItemsListDescriptionTemplate;
      using (Stream manifestResourceStream = assembly.GetManifestResourceStream(descriptionTemplate))
        str = new StreamReader(manifestResourceStream).ReadToEnd();
      ItemDescription itemDescription = this.MediaContentItemsList.Items.Where<ItemDescription>((Func<ItemDescription, bool>) (d => d.Name == "ItemDescription1")).FirstOrDefault<ItemDescription>();
      if (itemDescription == null)
        return;
      itemDescription.Markup = str;
    }
  }
}
