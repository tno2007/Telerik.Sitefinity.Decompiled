// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.ModulePageFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Fluent.Modules
{
  /// <summary>Fluent API for the module pages.</summary>
  public class ModulePageFacade<TParentFacade> : ModuleFacadeBase<ModulePageFacade<TParentFacade>>
  {
    private PageManager pageManager;
    private PageNode moduleNode;
    private PageData modulePage;
    private TParentFacade parentFacade;
    private Guid nodeId;
    private PageNode parentNode;
    private string nodeName;
    private string moduleName;

    /// <summary>
    /// Creates a new instance of the <see cref="!:ModulePageFacade" />.
    /// </summary>
    /// <param name="pageManager">The instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageManager" /> used for the current fluent API call.</param>
    /// <param name="moduleName">The name of the module to which the page belongs to.</param>
    /// <param name="nodeId">Id of the module node.</param>
    /// <param name="nodeName">Name of the module node.</param>
    /// <param name="parentFacade">Instance of the parent facade that instantiated this facade.</param>
    public ModulePageFacade(
      PageManager pageManager,
      string moduleName,
      Guid nodeId,
      string nodeName,
      TParentFacade parentFacade)
      : this(pageManager, moduleName, nodeId, (PageNode) null, nodeName, parentFacade)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="!:ModulePageFacade" />.
    /// </summary>
    /// <param name="pageManager">The instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageManager" /> used for the current fluent API call.</param>
    /// <param name="moduleName">The name of the module to which the module page belongs to.</param>
    /// <param name="nodeId">Id of the module node.</param>
    /// <param name="parentNode">Instance of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> that is the parent of the node that this facade is working with.</param>
    /// <param name="nodeName">Name of the module node.</param>
    /// <param name="parentFacade">Instance of the parent facade that instantiated this facade.</param>
    public ModulePageFacade(
      PageManager pageManager,
      string moduleName,
      Guid nodeId,
      PageNode parentNode,
      string nodeName,
      TParentFacade parentFacade)
    {
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager));
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName));
      if (nodeId == Guid.Empty)
        throw new ArgumentException("nodeId paramater cannot be an empty GUID", nameof (nodeId));
      if (string.IsNullOrEmpty(nodeName))
        throw new ArgumentNullException(nameof (nodeName));
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      this.pageManager = pageManager;
      this.moduleName = moduleName;
      this.nodeId = nodeId;
      this.parentNode = parentNode;
      this.nodeName = nodeName;
      this.parentFacade = parentFacade;
      if (parentNode == null || this.ModuleNode == null)
        return;
      this.pageManager.ChangeParent(this.ModuleNode, parentNode);
    }

    /// <summary>
    /// Gets the module node that is also the state of this facade.
    /// </summary>
    protected PageNode ModuleNode
    {
      get
      {
        if (this.moduleNode == null)
        {
          PageNode pageNode = this.pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == this.nodeId)).SingleOrDefault<PageNode>();
          this.moduleNode = pageNode == null ? this.pageManager.CreatePageNode(this.nodeId) : pageNode;
          this.moduleNode.Name = this.nodeName;
          this.moduleNode.NodeType = NodeType.Standard;
          this.moduleNode.Crawlable = false;
          this.moduleNode.ModuleName = this.moduleName;
          this.ModulePage.NavigationNode = this.moduleNode;
          this.moduleNode.ShowInNavigation = typeof (TParentFacade) == typeof (ModuleInstallFacade);
          if (this.parentNode != null)
            this.pageManager.ChangeParent(this.moduleNode, this.parentNode);
        }
        return this.moduleNode;
      }
    }

    /// <summary>
    /// Gets the module page that is also the state of this facade.
    /// </summary>
    protected PageData ModulePage
    {
      get
      {
        if (this.modulePage == null)
        {
          this.modulePage = this.pageManager.CreatePageData();
          this.modulePage.Status = ContentLifecycleStatus.Live;
          this.modulePage.Visible = true;
          this.modulePage.Version = 1;
          this.modulePage.Attributes["ModuleName"] = this.moduleName;
          this.modulePage.IncludeScriptManager = true;
          this.modulePage.LanguageData.Add(this.pageManager.CreatePublishedLanguageData());
          this.modulePage.Template = this.pageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Name == "DefaultBackend")).SingleOrDefault<PageTemplate>();
        }
        return this.modulePage;
      }
    }

    /// <summary>
    /// Sets the title of the module page node. This method does not uses localization.
    /// </summary>
    /// <param name="pageNodeTitle">
    /// Title of the page node; title is the text that will be used in navigational controls to represent this
    /// page node.
    /// </param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetTitle(string title)
    {
      this.ModuleNode.Title = (Lstring) title;
      this.ModulePage.Title = (Lstring) title;
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the title of the page node with the specified
    /// localization class to be used.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// Type of the localization class to be used for retrieving the localized resource.
    /// </typeparam>
    /// <param name="titleKey">
    /// Localization key of the title of the page node; title is the text that will be used in navigational controls
    /// to represent this page node.
    /// </param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetTitleLocalized<TResourceClass>(
      string titleKey)
    {
      this.ModuleNode.Title = (Lstring) Res.Expression(typeof (TResourceClass).Name, titleKey);
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the title of the page  node. The resorce will be pulled
    /// from the localization class declared in the <see cref="!:LocalizeUsing" /> method.
    /// </summary>
    /// <param name="titleKey">
    /// Localization key of the title of the page node; title is the text that will be used in navigational controls
    /// to represent this module node.
    /// </param>
    /// <exception cref="!:InvalidOperation">thrown if called before method LocalizeUsing method.</exception>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetTitleLocalized(string titleKey)
    {
      this.ModuleNode.Title = (Lstring) Res.Expression(this.ResourceClassType.Name, titleKey);
      return this;
    }

    /// <summary>
    /// Sets the url name of the module page node. This method does not uses localization.
    /// </summary>
    /// <param name="urlName">
    /// The url name of the page node; the string that will be used to generate the unique url to identify the node.
    /// </param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetUrlName(string urlName)
    {
      this.ModuleNode.UrlName = (Lstring) urlName;
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the url name of the module page node with the specified
    /// localization class to be used.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// Type of the localization class to be used for retrieving the localized resource.
    /// </typeparam>
    /// <param name="urlNameKey">
    /// Localization key of the url name of the page node; the string that will be used to generate the unique url to identify the node.
    /// </param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetUrlNameLocalized<TResourceClass>(
      string urlNameKey)
    {
      this.ModuleNode.UrlName = (Lstring) Res.Expression(typeof (TResourceClass).Name, urlNameKey);
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the url name of the module page. The resorce will be pulled
    /// from the localization class declared in the <see cref="!:LocalizeUsing" /> method.
    /// </summary>
    /// <param name="urlNameKey">
    /// Localization key of the url name of the page node; the string that will be used to generate the unique url to identify the node.
    /// </param>
    /// <exception cref="!:InvalidOperation">thrown if called before method LocalizeUsing method.</exception>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetUrlNameLocalized(string urlNameKey)
    {
      this.ModuleNode.UrlName = (Lstring) Res.Expression(this.ResourceClassType.Name, urlNameKey);
      return this;
    }

    /// <summary>
    /// Sets the description of the module page node. This method does not uses localization.
    /// </summary>
    /// <param name="urlName">
    /// The description of the page node; the string that is used to describe the page node in the sitemap.
    /// </param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetDescription(string description)
    {
      this.ModuleNode.Description = (Lstring) description;
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the description of the module page node with the specified
    /// localization class to be used.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// Type of the localization class to be used for retrieving the localized resource.
    /// </typeparam>
    /// <param name="descriptionKey">
    /// Localization key of the description of the page node; the string that is used to describe the page node in the sitemap.
    /// </param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetDescriptionLocalized<TResourceClass>(
      string descriptionKey)
    {
      this.ModuleNode.Description = (Lstring) Res.Expression(typeof (TResourceClass).Name, descriptionKey);
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the description of the module page. The resorce will be pulled
    /// from the localization class declared in the <see cref="!:LocalizeUsing" /> method.
    /// </summary>
    /// <param name="descriptionKey">
    /// Localization key of the description of the page node; the string that is used to describe the page node in the sitemap.
    /// </param>
    /// <exception cref="!:InvalidOperation">thrown if called before method LocalizeUsing method.</exception>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetDescriptionLocalized(
      string descriptionKey)
    {
      this.ModuleNode.Description = (Lstring) Res.Expression(this.ResourceClassType.Name, descriptionKey);
      return this;
    }

    /// <summary>
    /// Sets the html title of the module page. This method does not uses localization.
    /// </summary>
    /// <param name="htmlTitle">
    /// The html title of the module page; the string that will be used for the title tag in the html of the page.
    /// </param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetHtmlTitle(string htmlTitle)
    {
      this.ModulePage.HtmlTitle = (Lstring) htmlTitle;
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the html title of the module page with the specified
    /// localization class to be used.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// Type of the localization class to be used for retrieving the localized resource.
    /// </typeparam>
    /// <param name="descriptionKey">
    /// Localization key of the html title of the module page; the string that will be used for the title tag in the html of the page.
    /// </param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetHtmlTitleLocalized<TResourceClass>(
      string htmlTitleKey)
    {
      this.modulePage.HtmlTitle = (Lstring) Res.Expression(typeof (TResourceClass).Name, htmlTitleKey);
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the html title of the module page. The resorce will be pulled
    /// from the localization class declared in the <see cref="!:LocalizeUsing" /> method.
    /// </summary>
    /// <param name="descriptionKey">
    /// Localization key of the html title of the module page; the string that will be used for the title tag in the html of the page.
    /// </param>
    /// <exception cref="!:InvalidOperation">thrown if called before method LocalizeUsing method.</exception>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetHtmlTitleLocalized(string htmlTitleKey)
    {
      this.ModulePage.HtmlTitle = (Lstring) Res.Expression(this.ResourceClassType.Name, htmlTitleKey);
      return this;
    }

    /// <summary>Enables ASP.NET Ajax on the page; enabled by default.</summary>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> IncludeScriptManager()
    {
      this.ModulePage.IncludeScriptManager = true;
      return this;
    }

    /// <summary>
    /// Disables ASP.NET Ajax on the page; enabled by default.
    /// </summary>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> ExcludeScriptManager()
    {
      this.ModulePage.IncludeScriptManager = false;
      return this;
    }

    /// <summary>Makes the module page visible in the navigation.</summary>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> ShowInNavigation()
    {
      this.ModuleNode.ShowInNavigation = true;
      return this;
    }

    /// <summary>Makes the module page hidden from the navigation.</summary>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> HideFromNavigation()
    {
      this.ModuleNode.ShowInNavigation = false;
      return this;
    }

    /// <summary>
    /// Sets the order of the page in the current sitemap level.
    /// </summary>
    /// <param name="ordinal">The ordinal at which the module node should be placed.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetOrdinal(int ordinal)
    {
      this.ModuleNode.Ordinal = (float) ordinal;
      return this;
    }

    /// <summary>
    /// Sets the order of the page in the current sitemap level.
    /// </summary>
    /// <param name="ordinal">The ordinal at which the module node should be placed.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetOrdinal(float ordinal)
    {
      this.ModuleNode.Ordinal = ordinal;
      return this;
    }

    /// <summary>Enables the viewstate on the module page.</summary>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> EnableViewState()
    {
      this.ModulePage.EnableViewState = true;
      return this;
    }

    /// <summary>Sets the template that the module page ought to use.</summary>
    /// <param name="templateId">Id of the template to be set.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetTemplate(Guid templateId)
    {
      this.ModulePage.Template = this.pageManager.GetTemplate(templateId);
      return this;
    }

    /// <summary>Sets the template that the module page ought to use.</summary>
    /// <param name="pageTemplate">The instance of the template to be set.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetTemplate(PageTemplate pageTemplate)
    {
      this.ModulePage.Template = pageTemplate;
      return this;
    }

    /// <summary>Sets the template that the module page ought to use.</summary>
    /// <param name="pageTemplateName">Name of the template to be set.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> SetTemplate(string pageTemplateName)
    {
      this.ModulePage.Template = this.pageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Name == pageTemplateName)).SingleOrDefault<PageTemplate>();
      return this;
    }

    /// <summary>
    /// Adds the content view control into the default placeholder named "Content".
    /// </summary>
    /// <param name="controlDefinition">The control definition.</param>
    /// <param name="controlId">The control id.</param>
    /// <returns></returns>
    public ModulePageFacade<TParentFacade> AddContentView(
      string controlDefinition,
      string controlId = null)
    {
      BackendContentView backendContentView1 = new BackendContentView();
      backendContentView1.ModuleName = this.moduleName;
      backendContentView1.ControlDefinitionName = controlDefinition;
      BackendContentView backendContentView2 = backendContentView1;
      if (controlId != null)
        backendContentView2.ID = controlId;
      return this.AddControl((Control) backendContentView2);
    }

    /// <summary>
    /// Adds the content view control into the default placeholder named "Content".
    /// </summary>
    /// <param name="setAction">The set action.</param>
    /// <returns></returns>
    public ModulePageFacade<TParentFacade> AddContentView(
      Action<ContentView> setAction)
    {
      return this.AddContentView<BackendContentView>((Action<BackendContentView>) setAction);
    }

    /// <summary>
    /// Adds the content view control into the default placeholder named "Content".
    /// </summary>
    /// <param name="setAction">The set action.</param>
    /// <returns></returns>
    public ModulePageFacade<TParentFacade> AddContentView<TContentView>(
      Action<TContentView> setAction)
      where TContentView : ContentView, new()
    {
      if (setAction == null)
        throw new ArgumentNullException(nameof (setAction));
      TContentView contentView1 = new TContentView();
      contentView1.ModuleName = this.moduleName;
      TContentView contentView2 = contentView1;
      setAction(contentView2);
      return this.AddControl((Control) contentView2);
    }

    /// <summary>
    /// Adds the control to the module page into the default placeholder named "Content".
    /// </summary>
    /// <param name="control">Instance of the control to add to the module page.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> AddControl(Control control) => this.AddControl(control, "Content");

    /// <summary>
    /// Adds the control to the module page into the specified placeholder.
    /// </summary>
    /// <param name="control">Instance of the control to add to the module page.</param>
    /// <param name="placeholderName">Name of the placeholder at which to add the control.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> AddControl(
      Control control,
      string placeholderName)
    {
      return this.AddControl(control, placeholderName, (Action<PageControl>) null);
    }

    /// <summary>
    /// Adds the control to the module page into the specified placeholder with the ability to change the controlData properties through a delegate.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="placeholderName"></param>
    /// <param name="setControlDataProperties"></param>
    /// <returns></returns>
    public ModulePageFacade<TParentFacade> AddControl(
      Control control,
      string placeholderName,
      Action<PageControl> setControlDataProperties)
    {
      PageControl control1 = this.pageManager.CreateControl<PageControl>(false);
      control1.IsLayoutControl = false;
      control1.ObjectType = control.GetType().FullName;
      control1.PlaceHolder = placeholderName;
      this.pageManager.ReadProperties((object) control, (ObjectData) control1);
      control1.SetDefaultPermissions((IControlManager) this.pageManager);
      if (setControlDataProperties != null)
        setControlDataProperties(control1);
      this.ModulePage.Controls.Add(control1);
      return this;
    }

    /// <summary>
    /// Adds the user control to the module page into the default placeholder named "Content".
    /// </summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> AddUserControl(string virtualPath) => this.AddUserControl(virtualPath, "Content");

    /// <summary>
    /// Adds the user control to the module page into the specified placeholder.
    /// </summary>
    /// <param name="virtualPath">The virtual path.</param>
    /// <param name="placeholderName">Name of the placeholder.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModulePageFacade<TParentFacade> AddUserControl(
      string virtualPath,
      string placeholderName)
    {
      PageControl control = this.pageManager.CreateControl<PageControl>(false);
      control.IsLayoutControl = false;
      control.ObjectType = virtualPath;
      control.PlaceHolder = placeholderName;
      control.SetDefaultPermissions((IControlManager) this.pageManager);
      this.ModulePage.Controls.Add(control);
      return this;
    }

    /// <summary>
    /// Determines under which of predefined nodes the module node should be placed.
    /// </summary>
    /// <param name="commonNode">One of the predefined Sitefinity common page nodes.</param>
    /// <returns>The current instance of the facade.</returns>
    public ModulePageFacade<TParentFacade> PlaceUnder(CommonNode commonNode)
    {
      PageNode pageNode = this.GetPageNode(this.pageManager, commonNode);
      if (pageNode != null)
        this.pageManager.ChangeParent(this.ModuleNode, pageNode);
      return this;
    }

    /// <summary>
    /// Determines under which of predefined nodes the module node should be placed.
    /// </summary>
    /// <param name="parentNodeId">Id of the parent node.</param>
    /// <returns>The current instance of the facade.</returns>
    public ModulePageFacade<TParentFacade> PlaceUnder(Guid parentNodeId)
    {
      PageNode newParent = !(parentNodeId == Guid.Empty) ? this.pageManager.GetPageNode(parentNodeId) : throw new ArgumentException("parentNodeId argument cannot be an empty GUID.");
      if (newParent != null)
        this.pageManager.ChangeParent(this.ModuleNode, newParent);
      return this;
    }

    /// <summary>
    /// Determines under which of predefined nodes the module node should be placed.
    /// </summary>
    /// <param name="parentNode">The instance of the parent node.</param>
    /// <returns>The current instance of the facade.</returns>
    public ModulePageFacade<TParentFacade> PlaceUnder(PageNode parentNode)
    {
      if (parentNode == null)
        throw new ArgumentNullException(nameof (parentNode));
      this.pageManager.ChangeParent(this.ModuleNode, parentNode);
      return this;
    }

    public ModulePageFacade<TParentFacade> InheritPermissions()
    {
      this.moduleNode.CanInheritPermissions = true;
      this.moduleNode.InheritsPermissions = true;
      return this;
    }

    /// <summary>
    /// Allows chaining of arbitrary <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> manipulation.
    /// </summary>
    /// <param name="action">An action delegate on the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> being worked with.</param>
    /// <returns>An instance of the current <see cref="!:ModulePageFacade" />.</returns>
    public ModulePageFacade<TParentFacade> Do(Action<PageNode> action)
    {
      action(this.ModuleNode);
      return this;
    }

    /// <summary>
    /// Allows chaining of arbitrary manipulation of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> and its <see cref="T:Telerik.Sitefinity.Pages.Model.PageData" />.
    /// </summary>
    /// <param name="action">An action delegate on the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> and the <see cref="T:Telerik.Sitefinity.Pages.Model.PageData" /> being worked with.</param>
    /// <returns>An instance of the current <see cref="!:ModulePageFacade" />.</returns>
    public ModulePageFacade<TParentFacade> Do(Action<PageNode, PageData> action)
    {
      action(this.ModuleNode, this.ModulePage);
      return this;
    }

    /// <summary>
    /// Exists the current fluent API facade and returns to the parent facade that invoked this facade.
    /// </summary>
    /// <returns>The instance of the parent facade.</returns>
    public TParentFacade Done() => this.parentFacade;

    /// <summary>
    /// Commits all the items that have been placed into the transaction while working with the fluent API.
    /// </summary>
    /// <returns>The current instance of the facade.</returns>
    public ModulePageFacade<TParentFacade> SaveChanges()
    {
      this.pageManager.SaveChanges();
      return this;
    }

    /// <summary>
    /// Sets the page to redirect to an external url. The url should include the protocol - http etc.
    /// </summary>
    /// <param name="url">The URL to redirect to. It should include the protocol. For example http://www.mysite.com</param>
    /// <returns></returns>
    public ModulePageFacade<TParentFacade> RedirectTo(string url)
    {
      this.ModuleNode.NodeType = NodeType.OuterRedirect;
      this.ModuleNode.RedirectUrl = (Lstring) url;
      return this;
    }

    /// <summary>
    /// Sets the page to redirect to an external url in a new tab. The url should include the protocol - http etc.
    /// </summary>
    /// <param name="url">The URL to redirect to. It should include the protocol. For example http://www.mysite.com</param>
    /// <returns></returns>
    public ModulePageFacade<TParentFacade> RedirectInNewTab(string url)
    {
      this.ModuleNode.OpenNewWindow = true;
      this.ModuleNode.NodeType = NodeType.OuterRedirect;
      this.ModuleNode.RedirectUrl = (Lstring) url;
      this.ModuleNode.Page = (PageData) null;
      return this;
    }

    internal object RedirectTo() => throw new NotImplementedException();
  }
}
