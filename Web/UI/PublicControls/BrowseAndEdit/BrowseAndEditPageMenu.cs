// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditPageMenu
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  public class BrowseAndEditPageMenu : SimpleScriptView, IBrowseAndEditToolbar
  {
    private Dictionary<Control, BrowseAndEditCommand> toolbarControls;
    private bool currentSiteMapNodeSet;
    private PageSiteNode currentSiteMapNode;
    private bool currentPageNodeSet;
    private PageNode currentPageNode;
    private const string toggleBrowseAndEditCommandName = "toggleBrowseAndEdit";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.BrowseAndEditPageMenu.ascx");

    public IDictionary<Control, BrowseAndEditCommand> ToolbarControls
    {
      get
      {
        if (this.toolbarControls == null)
        {
          this.toolbarControls = new Dictionary<Control, BrowseAndEditCommand>();
          this.toolbarControls[(Control) this.ToggleEditingTools] = new BrowseAndEditCommand()
          {
            CommandName = "toggleBrowseAndEdit"
          };
        }
        return (IDictionary<Control, BrowseAndEditCommand>) this.toolbarControls;
      }
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? BrowseAndEditPageMenu.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the current site map node.</summary>
    /// <value>The current site map node.</value>
    protected PageSiteNode CurrentSiteMapNode
    {
      get
      {
        if (!this.currentSiteMapNodeSet)
        {
          this.currentSiteMapNode = SiteMapBase.GetActualCurrentNode();
          this.currentSiteMapNodeSet = true;
        }
        return this.currentSiteMapNode;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> object for the current site map node.
    /// </summary>
    /// <value>The current PageNode object.</value>
    protected PageNode CurrentPageNode
    {
      get
      {
        if (!this.currentPageNodeSet)
        {
          PageSiteNode currentSiteMapNode = this.CurrentSiteMapNode;
          if (currentSiteMapNode != null)
            this.currentPageNode = PageManager.GetManager().GetPageNode(currentSiteMapNode.Id);
          this.currentPageNodeSet = true;
        }
        return this.currentPageNode;
      }
    }

    /// <summary>Gets a reference to the container of the menu.</summary>
    public HtmlGenericControl BrowseAndEditMenu => this.Container.GetControl<HtmlGenericControl>("browseAndEditMenu", true);

    /// <summary>
    /// Gets a reference to the button that opens the page in the backend edit screen.
    /// </summary>
    public HtmlAnchor EditPageLink => this.Container.GetControl<HtmlAnchor>("editPage", true);

    /// <summary>
    /// Gets a reference to the container control of the button that opens the page in the backend edit screen.
    /// </summary>
    public HtmlGenericControl EditPageContainer => this.Container.GetControl<HtmlGenericControl>("editPageCnt", true);

    /// <summary>
    /// Gets a reference to the button that switches the editing toolbars.
    /// </summary>
    public HtmlAnchor ToggleEditingTools => this.Container.GetControl<HtmlAnchor>("toggleEditingTools", true);

    /// <summary>Gets a reference to the button that opens the menu.</summary>
    /// <value>The open menu button.</value>
    public HtmlAnchor OpenMenuButton => this.Container.GetControl<HtmlAnchor>("openMenu", true);

    /// <summary>Gets the client label manager.</summary>
    /// <value>The client label manager.</value>
    public ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.IsContentEditable(this.CurrentPageNode))
        this.EditPageLink.HRef = this.GetPageEditUrl();
      else
        this.EditPageContainer.Visible = false;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.TelerikSitefinity | ScriptRef.JQueryCookie;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddElementProperty("toggleEditingTools", this.ToggleEditingTools.ClientID);
      behaviorDescriptor.AddElementProperty("openMenu", this.OpenMenuButton.ClientID);
      behaviorDescriptor.AddElementProperty("menuContainer", this.BrowseAndEditMenu.ClientID);
      behaviorDescriptor.AddProperty("toggleBrowseAndEditCommandName", (object) "toggleBrowseAndEdit");
      behaviorDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      behaviorDescriptor.AddProperty("browseAndEditCookieName", (object) BrowseAndEditManager.BrowseAndEditCookieName);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string str = this.GetType().Assembly.GetName().ToString();
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      });
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = name,
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js"
      });
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.Scripts.BrowseAndEditPageMenu.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Gets the page edit URL in the backend.</summary>
    /// <returns></returns>
    protected string GetPageEditUrl()
    {
      string pageEditUrl = string.Empty;
      PageSiteNode currentSiteMapNode = this.CurrentSiteMapNode;
      PageNode currentPageNode = this.CurrentPageNode;
      if (currentSiteMapNode != null)
      {
        string pageUrl = currentPageNode == null ? currentSiteMapNode.Url : RouteHelper.ResolveUrl(currentPageNode.GetUrl(), UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
        CultureInfo language = (CultureInfo) null;
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          language = SystemManager.CurrentContext.Culture;
        pageEditUrl = DraftProxyBase.GetPageEditUrl(pageUrl, language);
      }
      return pageEditUrl;
    }

    /// <summary>
    /// Determines whether the user can edit the contents of the specified page.
    /// </summary>
    /// <param name="page">The page node to be checked.</param>
    /// <returns>
    /// 	<c>true</c> if the user can edit the contents of the specified page; otherwise, <c>false</c>.
    /// </returns>
    protected bool IsContentEditable(PageNode page)
    {
      if (page == null)
        return true;
      return page.IsGranted("Pages", "EditContent");
    }
  }
}
