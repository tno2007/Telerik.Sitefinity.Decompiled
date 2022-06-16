// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>Defines the properties for the page</summary>
  [KnownType(typeof (WcfPageTemplate))]
  [KnownType(typeof (WcfPage))]
  [DataContract]
  public class WcfPage : WcfPageNode
  {
    private DictionaryObjectViewModel attributes;
    private PageCustomFieldsViewModel customFields;
    private LstringSingleViewModel seoTitle;

    public WcfPage()
    {
    }

    public WcfPage(PageNode pageNode)
      : this(pageNode, (PageManager) null)
    {
    }

    public WcfPage(PageNode pageNode, PageManager manager)
      : base(pageNode, manager)
    {
      this.UrlName = (string) pageNode.UrlName + (string) pageNode.Extension;
      this.ShowInNavigation = pageNode.ShowInNavigation;
      this.IsGroup = pageNode.NodeType == NodeType.Group;
      this.ApplyCanonicalUrlSettings(pageNode);
      this.PopulateCustomFields(pageNode);
      if (pageNode.Parent != null)
        this.Parent = new WcfPageNode(pageNode.Parent, manager);
      this.Crawlable = pageNode.Crawlable;
      this.RequireSsl = pageNode.RequireSsl;
      this.IncludeInSearchIndex = pageNode.IncludeInSearchIndex;
      PageData pageData = pageNode.GetPageData();
      if (pageData != null)
      {
        if (pageData.Template != null)
          this.Template = new WcfPageTemplate(pageData.Template);
        this.IncludeScriptManager = pageData.IncludeScriptManager;
        this.EnableViewState = pageData.EnableViewState;
        this.CacheOutput = pageData.CacheOutput;
        this.CacheDuration = pageData.CacheDuration;
        this.SlidingExpiration = pageData.SlidingExpiration;
        this.OutputCacheProfile = pageData.OutputCacheProfile;
        this.CodeBehindType = pageData.CodeBehindType;
        this.HeadTagContent = pageData.HeadTagContent;
        this.Keywords = new LstringSingleViewModel(pageData.Keywords);
        this.Description = new LstringSingleViewModel(pageData.Description);
        this.SeoTitle = new LstringSingleViewModel(pageData.HtmlTitle);
        this.Language = pageData.Culture;
      }
      this.Attributes = new DictionaryObjectViewModel(pageNode.Attributes);
      string str = string.Empty;
      if (pageNode.Urls != null)
      {
        int lcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
        IEnumerable<PageUrlData> source = (IEnumerable<PageUrlData>) pageNode.Urls;
        IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
        if (appSettings.Multilingual)
        {
          if (!SystemManager.CurrentContext.Culture.Equals(pageNode.IsBackend ? (object) appSettings.DefaultBackendLanguage : (object) appSettings.DefaultFrontendLanguage))
          {
            source = source.Where<PageUrlData>((Func<PageUrlData, bool>) (url => url.Culture == lcid));
          }
          else
          {
            int invLcid = CultureInfo.InvariantCulture.LCID;
            source = source.Where<PageUrlData>((Func<PageUrlData, bool>) (url => url.Culture == lcid || url.Culture == invLcid));
          }
        }
        str = string.Join("\r\n", source.Where<PageUrlData>((Func<PageUrlData, bool>) (url => !url.IsDefault)).Select<PageUrlData, string>((Func<PageUrlData, string>) (url => url.Url)).Distinct<string>());
      }
      this.MultipleNavigationNodes = str;
      this.AllowMultipleUrls = !string.IsNullOrEmpty(str);
      this.NavigateUrl = VirtualPathUtility.ToAbsolute(pageNode.GetBackendUrl("Edit"));
      this.Priority = pageNode.Priority;
    }

    private void ApplyCanonicalUrlSettings(PageNode pageNode)
    {
      if (!pageNode.EnableDefaultCanonicalUrl.HasValue)
        this.EnableDefaultCanonicalUrl = WcfPage.CanonicalUrlSettings.Default;
      else if (pageNode.EnableDefaultCanonicalUrl.Value)
        this.EnableDefaultCanonicalUrl = WcfPage.CanonicalUrlSettings.Enabled;
      else
        this.EnableDefaultCanonicalUrl = WcfPage.CanonicalUrlSettings.Disabled;
    }

    private void PopulateCustomFields(PageNode node)
    {
      if (!(node.Id != Guid.Empty))
        return;
      this.CustomFields = new PageCustomFieldsViewModel(node);
    }

    /// <summary>Gets or sets the name of the URL.</summary>
    /// <value>The name of the URL.</value>
    [DataMember]
    public string UrlName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [show in navigation].
    /// </summary>
    /// <value><c>true</c> if [show in navigation]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool ShowInNavigation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is group.
    /// </summary>
    /// <value><c>true</c> if this instance is group; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool IsGroup { get; set; }

    /// <summary>Gets or sets the language of the page.</summary>
    /// <value>The language.</value>
    [DataMember]
    public string Language { get; set; }

    /// <summary>
    /// Gets or sets the id of the pageData to which the data in this object is a translation.
    /// Only used in multilingual environment. Only set when creating a translated version of
    /// the page.
    /// </summary>
    /// <value>The source language page id.</value>
    [DataMember]
    public Guid SourceLanguagePageId { get; set; }

    /// <summary>Gets or sets the parent.</summary>
    /// <value>The parent.</value>
    [DataMember]
    public WcfPageNode Parent { get; set; }

    /// <summary>Gets or sets the template.</summary>
    /// <value>The template.</value>
    [DataMember]
    public WcfPageTemplate Template { get; set; }

    /// <summary>Gets or sets the keywords.</summary>
    /// <value>The keywords.</value>
    [DataMember]
    public LstringSingleViewModel Keywords { get; set; }

    /// <summary>Gets or sets the description.</summary>
    /// <value>The description.</value>
    [DataMember]
    public LstringSingleViewModel Description { get; set; }

    /// <summary>Gets or sets Title for search engines.</summary>
    /// <value>Title for search engines.</value>
    /// <remarks>If none is set the title value for the page will be returned.</remarks>
    [DataMember]
    public LstringSingleViewModel SeoTitle { get; set; }

    /// <summary>
    /// Gets or sets value indicating if the canonical url tag will be added on this page
    /// if there are no widgets on it that will add it automatically.
    /// </summary>
    /// <value>If set to true - canonical url tag will be added by default.
    /// If false - no canonical tag will be added by default.
    /// If not set (null) the default global settings will take place.</value>
    [DataMember]
    public WcfPage.CanonicalUrlSettings EnableDefaultCanonicalUrl { get; set; }

    /// <summary>Gets or sets the URL for editing the page.</summary>
    /// <value>The URL for editing the page.</value>
    [DataMember]
    public string NavigateUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this page is crawlable.
    /// </summary>
    /// <value><c>true</c> if [crawlable]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool Crawlable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this page requires SSL.
    /// </summary>
    /// <value><c>true</c> if [require SSL]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool RequireSsl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the view state of this page is enabled.
    /// The default value is false as opposed to standard ASP.Net page.
    /// </summary>
    /// <value><c>true</c> if [enable view state]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool EnableViewState { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically include script manager on the page.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [include script manager]; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IncludeScriptManager { get; set; }

    /// <summary>
    /// Gets or sets a value for custom content to be inserted in head tag.
    /// </summary>
    /// <value>The value for custom content to be inserted in head tag</value>
    [DataMember]
    public string HeadTagContent { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to cache the rendered output of this page.
    /// </summary>
    /// <value><c>true</c> if [cache output]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool CacheOutput { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the duration of the cached output is calculated from the last request.
    /// If the value is false the cache will expire at absolute time intervals.
    /// </summary>
    /// <value><c>true</c> if [sliding expiration]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool SlidingExpiration { get; set; }

    /// <summary>
    /// Gets or sets the time, in seconds, that the page is cached.
    /// </summary>
    /// <value>The duration of the cache.</value>
    [DataMember]
    public int CacheDuration { get; set; }

    /// <summary>Gets or sets the name of the output cache profile.</summary>
    /// <value>The output cache profile name.</value>
    [DataMember]
    public string OutputCacheProfile { get; set; }

    /// <summary>Gets or sets the name of code behind type.</summary>
    /// <value>The name of code behind type.</value>
    [DataMember]
    public string CodeBehindType { get; set; }

    /// <summary>Gets or sets the page node attributes.</summary>
    /// <value>The attributes.</value>
    [DataMember]
    public DictionaryObjectViewModel Attributes
    {
      get
      {
        if (this.attributes == null)
          this.attributes = new DictionaryObjectViewModel();
        return this.attributes;
      }
      set => this.attributes = value;
    }

    /// <summary>Gets or sets the page node attributes.</summary>
    /// <value>The attributes.</value>
    [DataMember]
    public PageCustomFieldsViewModel CustomFields
    {
      get
      {
        if (this.customFields == null)
          this.customFields = new PageCustomFieldsViewModel();
        return this.customFields;
      }
      set => this.customFields = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to exclude the page from the Sitefinity search engine.
    /// </summary>
    [DataMember]
    public bool IncludeInSearchIndex { get; set; }

    /// <summary>Gets or sets the where to duplicate the page.</summary>
    [DataMember]
    public Guid TargetSiteId { get; set; }

    /// <summary>Gets or sets whether to duplicate all childs.</summary>
    [DataMember]
    public bool DuplicateChildren { get; set; }

    /// <summary>Gets or sets the priority in the sitemap.</summary>
    [DataMember]
    public float Priority { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include the page in the sitemap.
    /// </summary>
    [DataMember]
    public bool IncludeInSitemap { get; set; }

    /// <summary>
    /// Settings for the canonical url configuration for a page.
    /// </summary>
    public enum CanonicalUrlSettings : byte
    {
      /// <summary>As set for the entire site.</summary>
      Default,
      /// <summary>
      /// Explicitly disabled no matter what the default site settings are.
      /// </summary>
      Disabled,
      /// <summary>
      /// Explicitly enabled no matter what the default site settings are.
      /// </summary>
      Enabled,
    }
  }
}
