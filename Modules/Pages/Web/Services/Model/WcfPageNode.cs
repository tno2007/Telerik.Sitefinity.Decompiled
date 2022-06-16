// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageNode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>Defines the basic properties for page node</summary>
  [DataContract]
  public class WcfPageNode
  {
    private LstringSingleViewModel title;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageNode" /> class.
    /// </summary>
    public WcfPageNode()
    {
    }

    public WcfPageNode(PageNode pageNode, PageManager pm)
    {
      if (pageNode.Id != Guid.Empty)
        this.InitFromSiteNode(pageNode.GetSiteMapNode());
      else
        this.AllowParameterValidation = true;
    }

    internal WcfPageNode(PageSiteNode pageNode) => this.InitFromSiteNode(pageNode);

    private void InitFromSiteNode(PageSiteNode siteNode)
    {
      this.Id = siteNode.Id;
      this.HasChildren = siteNode.HasAnyChildNodes;
      if (!siteNode.ParentKey.IsNullOrEmpty())
        this.ParentId = Guid.Parse(siteNode.ParentKey);
      this.RootId = siteNode.RootKey;
      this.AllowMultipleUrls = siteNode.AllowMultipleUrls;
      this.AllowParameterValidation = siteNode.AllowParametersValidation;
      this.AdditionalUrlsRedirectToDefaultOne = siteNode.AdditionalUrlsRedirectToDefault;
      CultureInfo culture = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture : CultureInfo.InvariantCulture;
      this.Path = string.Join(" > ", ((IEnumerable<string>) siteNode.GetUrl(culture, true, false).TrimStart('~').Split(new char[1]
      {
        '/'
      }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>().ToArray());
      this.FullUrl = RouteHelper.ResolveUrl(siteNode.GetLiveUrl(false), UrlResolveOptions.Rooted);
      this.TitlesPath = siteNode.GetTitlesPath(culture: culture);
      this.NodeType = siteNode.NodeType;
      this.Extension = siteNode.Extension;
      this.IsExternal = siteNode.NodeType == NodeType.Rewriting || siteNode.NodeType == NodeType.InnerRedirect || siteNode.NodeType == NodeType.OuterRedirect;
      if (siteNode.NodeType == NodeType.InnerRedirect)
      {
        Guid linkedNodeId = siteNode.LinkedNodeId;
        this.LinkedNodeId = siteNode.LinkedNodeId;
        this.LinkedNodeProvider = siteNode.LinkedNodeProvider;
        this.LinkedNodeTitle = "";
        SiteMapNode siteMapNodeFromKey = SiteMapBase.GetCurrentProvider().FindSiteMapNodeFromKey(this.LinkedNodeId.ToString());
        if (siteMapNodeFromKey != null)
        {
          this.LinkedNodeTitle = siteMapNodeFromKey.Title;
          this.LinkedNodeFullUrl = RouteHelper.ResolveUrl(siteMapNodeFromKey.Url, UrlResolveOptions.Rooted);
        }
        this.RedirectUrl = string.Empty;
        this.OpenNewWindow = false;
      }
      if (siteNode.NodeType == NodeType.OuterRedirect)
      {
        this.LinkedNodeId = Guid.Empty;
        this.LinkedNodeProvider = string.Empty;
        this.RedirectUrl = siteNode.RedirectUrl;
        this.OpenNewWindow = siteNode.Attributes["target"] != null && siteNode.Attributes["target"].Equals("_blank");
      }
      CultureInfo[] availableLanguages = siteNode.AvailableLanguages;
      this.Title = new LstringSingleViewModel(siteNode.GetTitle());
      this.AvailableLanguages = ((IEnumerable<CultureInfo>) availableLanguages).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)).ToArray<string>();
      this.LocalizationStrategy = siteNode.LocalizationStrategy;
      this.HasTranslationSiblings = (uint) this.AvailableLanguages.Length > 0U;
      this.WasPublished = siteNode.IsPublished(culture);
      this.IsBackend = siteNode.IsBackend;
      this.ProviderName = siteNode.PageProviderName;
      this.Renderer = siteNode.CurrentPageDataItem.Renderer;
      this.TemplateName = siteNode.CurrentPageDataItem.TemplateName;
    }

    /// <summary>Gets or sets the template id</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the was published.</summary>
    [DataMember]
    public bool WasPublished { get; set; }

    /// <summary>Gets or sets the title of the template</summary>
    [DataMember]
    public LstringSingleViewModel Title
    {
      get
      {
        if (this.title == null)
          this.title = new LstringSingleViewModel();
        return this.title;
      }
      set => this.title = value;
    }

    /// <summary>Gets or sets the title of the template</summary>
    [DataMember]
    public string Path { get; set; }

    /// <summary>Gets or sets the url of the page node</summary>
    [DataMember]
    public string FullUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has children.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has children; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool HasChildren { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [allow parameters validation].
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [allow parameters validation]; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool AllowParameterValidation { get; set; }

    /// <summary>Gets or sets the parent id.</summary>
    /// <value>The parent id.</value>
    [DataMember]
    public Guid ParentId { get; set; }

    /// <summary>Gets or sets the root location id.</summary>
    /// <value>The root id.</value>
    [DataMember]
    public Guid RootId { get; set; }

    /// <summary>Gets or sets the path of titles for the page</summary>
    [DataMember]
    public string TitlesPath { get; set; }

    /// <summary>
    /// Gets or sets the flag if multiple urls can be used for one page.
    /// </summary>
    /// <value>The flag if multiple urls for the page will be used.</value>
    [DataMember]
    public bool AllowMultipleUrls { get; set; }

    /// <summary>
    /// Gets or sets the flag if multiple urls can be used for one page.
    /// </summary>
    /// <value>The flag if multiple urls for the page will be used.</value>
    [DataMember]
    public string MultipleNavigationNodes { get; set; }

    /// <summary>
    /// Gets or sets the flag if multiple urls can be used for one page.
    /// </summary>
    /// <value>The flag if multiple urls can be used for one page.</value>
    [DataMember]
    public bool AdditionalUrlsRedirectToDefaultOne { get; set; }

    /// <summary>Gets languages available for this item.</summary>
    /// <value>The available languages.</value>
    [DataMember]
    public virtual string[] AvailableLanguages { get; set; }

    /// <summary>Gets or sets the localization strategy.</summary>
    /// <value>The localization strategy.</value>
    [DataMember]
    public LocalizationStrategy LocalizationStrategy { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the page is an external page.
    /// </summary>
    /// <value>The flag, if the page is a redirect to an external page.</value>
    [DataMember]
    public bool IsExternal { get; set; }

    /// <summary>
    /// Gets or sets the Id of the linked page that will be used for redirection.
    /// </summary>
    [DataMember]
    public Guid LinkedNodeId { get; set; }

    /// <summary>
    /// Gets or sets the title of the linked page that will be used for redirection.
    /// </summary>
    [DataMember]
    public string LinkedNodeTitle { get; set; }

    /// <summary>
    /// Gets or sets the full url of the linked page that will be used for redirection.
    /// </summary>
    [DataMember]
    public string LinkedNodeFullUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider for the linked page id.
    /// </summary>
    [DataMember]
    public string LinkedNodeProvider { get; set; }

    /// <summary>
    /// Gets or sets the url to redirect to, for external pages.
    /// </summary>
    [DataMember]
    public string RedirectUrl { get; set; }

    /// <summary>
    /// Gets or sets a flag indicating whether the page, if external, will be opened in a new window.
    /// </summary>
    [DataMember]
    public bool OpenNewWindow { get; set; }

    /// <summary>
    /// Gets or sets the name of the data provider this item was instantiated with.
    /// </summary>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>
    /// Represents the node type - group, external, outer redirect, standard, etc
    /// </summary>
    [DataMember]
    public NodeType NodeType { get; set; }

    /// <summary>
    /// Specifies if the node has nodes that represents the different language translations
    /// </summary>
    [DataMember]
    public bool HasTranslationSiblings { get; set; }

    /// <summary>
    /// Gets a value indicating whether this node is a backend node.
    /// </summary>
    public bool IsBackend { get; set; }

    /// <summary>
    /// Represents an extension to be used with the current page, with the dot (".") - .aspx, .html, .php etc
    /// </summary>
    [DataMember]
    public string Extension { get; set; }

    /// <summary>Gets or sets the name of renderer used for the page.</summary>
    [DataMember]
    public string Renderer { get; set; }

    /// <summary>Gets or sets the name of the template.</summary>
    /// <remarks>This is intended for use in the external render <seealso cref="P:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageNode.Renderer" /></remarks>
    [DataMember]
    public string TemplateName { get; set; }
  }
}
