// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Modules.ModuleNodeFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Modules
{
  /// <summary>Fluent API facade for working with a module node.</summary>
  public class ModuleNodeFacade<TParentFacade> : ModuleFacadeBase<ModuleNodeFacade<TParentFacade>>
  {
    private PageNode pageNode;
    private PageNode parentNode;
    private string nodeName;
    private Guid nodeId;
    private string moduleName;
    private PageManager pageManager;
    private TParentFacade parentFacade;

    /// <summary>
    /// Creates a new instance of the <see cref="!:ModuleNodeFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageManager" /> to be used in the current fluent API call.
    /// </param>
    /// <param name="moduleName">Name of the module for which the module node is being created.</param>
    /// <param name="nodeId">Id of the module node.</param>
    /// <param name="nodeName">Name of the module node.</param>
    /// <param name="parentFacade">
    /// Instance of the parent facade that invoked this facade.
    /// </param>
    public ModuleNodeFacade(
      PageManager pageManager,
      string moduleName,
      Guid nodeId,
      string nodeName,
      TParentFacade parentFacade)
      : this(pageManager, moduleName, nodeId, (PageNode) null, nodeName, parentFacade)
    {
    }

    /// <summary>
    /// Creates a new instance of the <see cref="!:ModuleNodeFacade" />.
    /// </summary>
    /// <param name="pageManager">
    /// The instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageManager" /> to be used in the current fluent API call.
    /// </param>
    /// <param name="parentFacade">
    /// Instance of the parent facade that invoked this facade.
    /// </param>
    public ModuleNodeFacade(
      PageManager pageManager,
      string moduleName,
      Guid nodeId,
      PageNode parentNode,
      string nodeName,
      TParentFacade parentFacade)
    {
      if (pageManager == null)
        throw new ArgumentNullException(nameof (pageManager), "pageManager argument cannot be null.");
      if (string.IsNullOrEmpty(moduleName))
        throw new ArgumentNullException(nameof (moduleName), "moduleName argument cannot be null.");
      if (nodeId == Guid.Empty)
        throw new ArgumentNullException("nodeId argument cannot be an empty Guid.", nameof (nodeId));
      if (string.IsNullOrEmpty(nodeName))
        throw new ArgumentNullException(nameof (nodeName), "nodeName argument cannot be null.");
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade), "parentFacade argument cannot be null.");
      this.pageManager = pageManager;
      this.moduleName = moduleName;
      this.nodeId = nodeId;
      this.nodeName = nodeName;
      this.parentFacade = parentFacade;
      this.parentNode = parentNode;
      if (parentNode == null || this.ModuleNode == null)
        return;
      this.pageManager.ChangeParent(this.ModuleNode, parentNode);
    }

    /// <summary>
    /// Gets the instance of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> which represents the state of this facade.
    /// If the state is null, a new instance of <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> will be created.
    /// </summary>
    protected PageNode ModuleNode
    {
      get
      {
        if (this.pageNode == null)
        {
          PageNode pageNode = this.pageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => pn.Id == this.nodeId)).SingleOrDefault<PageNode>();
          this.pageNode = pageNode == null ? this.pageManager.CreatePageNode(this.nodeId) : pageNode;
          this.pageNode.Name = this.nodeName;
          this.pageNode.NodeType = NodeType.Group;
          this.pageNode.ModuleName = this.moduleName;
          this.pageNode.RenderAsLink = true;
        }
        return this.pageNode;
      }
    }

    /// <summary>
    /// Sets the title of the module node. This method does not uses localization.
    /// </summary>
    /// <param name="title">
    /// Title of the module node; title is the text that will be used in navigational controls to represent this
    /// module node.
    /// </param>
    /// <returns>The current instance of the facade.</returns>
    public ModuleNodeFacade<TParentFacade> SetTitle(string title)
    {
      this.ModuleNode.Title = (Lstring) title;
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the title of the module node with the specified
    /// localization class to be used.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// Type of the localization class to be used for retrieving the localized resource.
    /// </typeparam>
    /// <param name="titleKey">
    /// Localization key of the title of the module node; title is the text that will be used in navigational controls
    /// to represent this module node.
    /// </param>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetTitleLocalized<TResourceClass>(
      string titleKey)
    {
      this.ModuleNode.Title = (Lstring) Res.Expression(typeof (TResourceClass).Name, titleKey);
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the title of the module node. The resorce will be pulled
    /// from the localization class declared in the <see cref="!:LocalizeUsing" /> method.
    /// </summary>
    /// <param name="titleKey">
    /// Localization key of the title of the module node; title is the text that will be used in navigational controls
    /// to represent this module node.
    /// </param>
    /// <exception cref="!:InvalidOperation">thrown if called before method LocalizeUsing method.</exception>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetTitleLocalized(string titleKey)
    {
      this.ModuleNode.Title = (Lstring) Res.Expression(this.ResourceClassType.Name, titleKey);
      return this;
    }

    /// <summary>
    /// Sets the url name of the module node. This method does not uses localization.
    /// </summary>
    /// <param name="urlName">
    /// Url name of the module node; url name is the string that will be used as a
    /// distinguishing part of the url for the module node.
    /// </param>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetUrlName(string urlName)
    {
      this.ModuleNode.UrlName = (Lstring) urlName;
      return this;
    }

    /// <summary>
    /// Sets the page to redirect to an external url. The url should include the protocol - http etc.
    /// </summary>
    /// <param name="url">The URL to redirect to. It should include the protocol. For example http://www.mysite.com</param>
    /// <returns></returns>
    public ModuleNodeFacade<TParentFacade> RedirectTo(string url)
    {
      this.ModuleNode.NodeType = NodeType.OuterRedirect;
      this.ModuleNode.RedirectUrl = (Lstring) url;
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the url name of the module node with the specified
    /// localization class to be used.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// Type of the localization class to be used for retrieving the localized resource.
    /// </typeparam>
    /// <param name="urlNameKey">
    /// Localization key of the url name of the module node; url name is the string that will be used as a
    /// distinguishing part of the url for the module node.
    /// </param>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetUrlNameLocalized<TResourceClass>(
      string urlNameKey)
    {
      this.ModuleNode.UrlName = (Lstring) Res.Expression(typeof (TResourceClass).Name, urlNameKey);
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the url name of the module node. The resorce will be pulled
    /// from the localization class declared in the <see cref="!:LocalizeUsing" /> method.
    /// </summary>
    /// <param name="urlNameKey">
    /// Localization key of the url name of the module node; url name is the string that will be used as a
    /// distinguishing part of the url of the module node.
    /// </param>
    /// <exception cref="!:InvalidOperation">thrown if called before method LocalizeUsing method.</exception>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetUrlNameLocalized(string urlNameKey)
    {
      this.ModuleNode.UrlName = (Lstring) Res.Expression(this.ResourceClassType.Name, urlNameKey);
      return this;
    }

    /// <summary>
    /// Sets the description of the module node. This method does not uses localization.
    /// </summary>
    /// <param name="description">
    /// Description of the module node; description is the string that will be used in the SitemapNode in
    /// order to describe the node.
    /// </param>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetDescription(string description)
    {
      this.ModuleNode.Description = (Lstring) description;
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the description of the module node with the specified
    /// localization class to be used.
    /// </summary>
    /// <typeparam name="TResourceClass">
    /// Type of the localization class to be used for retrieving the localized resource.
    /// </typeparam>
    /// <param name="descriptionKey">
    /// Localization key of the description of the module node; description is the string that will
    /// be used in the SitemapNode in order to describe the node.
    /// </param>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetDescriptionLocalized<TResourceClass>(
      string descriptionKey)
    {
      this.ModuleNode.Description = (Lstring) Res.Expression(typeof (TResourceClass).Name, descriptionKey);
      return this;
    }

    /// <summary>
    /// Sets the key of the localized resource that represents the description of the module node. The resorce will be pulled
    /// from the localization class declared in the <see cref="!:LocalizeUsing" /> method.
    /// </summary>
    /// <param name="descriptionKey">
    /// Localization key of the description of the module node; description is the string that will
    /// be used in the SitemapNode in order to describe the node.
    /// </param>
    /// <exception cref="!:InvalidOperation">thrown if called before method LocalizeUsing method.</exception>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetDescriptionLocalized(
      string descriptionKey)
    {
      this.ModuleNode.Description = (Lstring) Res.Expression(this.ResourceClassType.Name, descriptionKey);
      return this;
    }

    /// <summary>Performs a custom action on the module node.</summary>
    /// <param name="setAction">The delegate that ought to be executed on the module node.</param>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> Do(Action<PageNode> setAction)
    {
      setAction(this.ModuleNode);
      return this;
    }

    /// <summary>
    /// Makes the module node visible in the navigation controls.
    /// </summary>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> ShowInNavigation()
    {
      this.ModuleNode.ShowInNavigation = true;
      return this;
    }

    /// <summary>
    /// Makes the module node invisible in the navigation controls.
    /// </summary>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> HideFromNavigation()
    {
      this.ModuleNode.ShowInNavigation = false;
      return this;
    }

    public ModuleNodeFacade<TParentFacade> RenderAsLink()
    {
      this.ModuleNode.RenderAsLink = true;
      return this;
    }

    public ModuleNodeFacade<TParentFacade> RenderAsText()
    {
      this.ModuleNode.RenderAsLink = false;
      return this;
    }

    public ModuleNodeFacade<TParentFacade> InheritPermissions()
    {
      this.ModuleNode.CanInheritPermissions = true;
      this.ModuleNode.InheritsPermissions = true;
      return this;
    }

    /// <summary>
    /// Sets the order of the page in the current sitemap level.
    /// </summary>
    /// <param name="ordinal">The ordinal at which the module node should be placed.</param>
    /// <returns>The current instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetOrdinal(int ordinal)
    {
      this.ModuleNode.Ordinal = (float) ordinal;
      return this;
    }

    /// <summary>
    /// Sets the order of the page in the current sitemap level.
    /// </summary>
    /// <param name="ordinal">The ordinal at which the module node should be placed.</param>
    /// <returns>The current instance of the module page facade.</returns>
    public ModuleNodeFacade<TParentFacade> SetOrdinal(float ordinal)
    {
      this.ModuleNode.Ordinal = ordinal;
      return this;
    }

    /// <summary>
    /// Determines under which of predefined nodes the module node should be placed.
    /// </summary>
    /// <param name="commonNode">One of the predefined Sitefinity common page nodes.</param>
    /// <returns>The instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> PlaceUnder(CommonNode commonNode)
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
    /// <returns>The instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> PlaceUnder(Guid parentNodeId)
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
    /// <returns>The instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> PlaceUnder(PageNode parentNode)
    {
      if (parentNode == null)
        throw new ArgumentNullException(nameof (parentNode));
      this.pageManager.ChangeParent(this.ModuleNode, parentNode);
      return this;
    }

    /// <summary>Configures the node to be shared between modules.</summary>
    /// <returns>An instance of type <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> AsShared()
    {
      this.ModuleNode.ModuleName = (string) null;
      return this;
    }

    /// <summary>Adds an attribute to a module page node.</summary>
    /// <param name="key">Key of the attribute.</param>
    /// <param name="value">Value of the attribute.</param>
    /// <returns>The instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> AddAttribute(string key, string value)
    {
      if (!this.ModuleNode.Attributes.ContainsKey(key))
        this.ModuleNode.Attributes.Add(key, value);
      return this;
    }

    /// <summary>Removes an attribute from the module page node.</summary>
    /// <param name="key">Key of the attribute to be removed.</param>
    /// <returns>The instance of the <see cref="!:ModuleNodeFacade" /></returns>
    public ModuleNodeFacade<TParentFacade> RemoveAttribute(string key)
    {
      if (this.ModuleNode.Attributes.ContainsKey(key))
        this.ModuleNode.Attributes.Remove(key);
      return this;
    }

    /// <summary>
    /// Sets a value indicating if the page node should generate a canonical tag by default
    /// (if no widget that could add such tag has already added).
    /// </summary>
    /// <param name="value">If set to true and the system allows canonical URLs and there is no widget that will set the canonical tag - a default one will be generated.</param>
    /// <returns>The instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SetEnableDefaultCanonicalUrl(bool value)
    {
      this.ModuleNode.EnableDefaultCanonicalUrl = new bool?(value);
      return this;
    }

    /// <summary>
    /// Exists the module node fluent API and goes back to module installation fluent API.
    /// </summary>
    /// <returns>The instance of the <see cref="T:Telerik.Sitefinity.Fluent.Modules.ModuleInstallFacade" />.</returns>
    public TParentFacade Done() => this.parentFacade;

    /// <summary>
    /// Commits all the items that have been placed into the transaction while working with the fluent API.
    /// </summary>
    /// <returns>The instance of the <see cref="!:ModuleNodeFacade" />.</returns>
    public ModuleNodeFacade<TParentFacade> SaveChanges()
    {
      this.pageManager.SaveChanges();
      return this;
    }

    public ModuleNodeFacade<ModuleNodeFacade<TParentFacade>> AddChildGroup(
      Guid nodeId,
      string nodeName)
    {
      return new ModuleNodeFacade<ModuleNodeFacade<TParentFacade>>(this.pageManager, this.moduleName, nodeId, this.ModuleNode, nodeName, this);
    }

    public ModulePageFacade<ModuleNodeFacade<TParentFacade>> AddChildPage(
      Guid pageId,
      string pageName)
    {
      return new ModulePageFacade<ModuleNodeFacade<TParentFacade>>(this.pageManager, this.moduleName, pageId, this.ModuleNode, pageName, this);
    }
  }
}
