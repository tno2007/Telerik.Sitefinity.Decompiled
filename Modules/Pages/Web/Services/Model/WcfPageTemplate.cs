// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>WCF data transfer class for the page template object.</summary>
  [DataContract]
  public class WcfPageTemplate
  {
    private string _duplicateTitle = string.Empty;
    private const string DuplicatePageTitleSuffix = "_copy";

    public WcfPageTemplate()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageTemplate" /> class.
    /// </summary>
    /// <param name="pageTemplate">The page template.</param>
    public WcfPageTemplate(PageTemplate pageTemplate)
    {
      this.Id = pageTemplate.Id;
      this.Title = HttpUtility.HtmlEncode((string) pageTemplate.Title);
      this.Framework = pageTemplate.Framework;
      if (pageTemplate.Id != Guid.Empty)
      {
        Image relatedImage = pageTemplate.GetRelatedImage();
        this.TemplateIconUrl = pageTemplate.GetBigThumbnailUrl(relatedImage);
        this.TemplateSmallIconUrl = pageTemplate.GetSmallThumbnailUrl(relatedImage);
      }
      this.EditUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Template/" + (object) pageTemplate.Id);
      this.MasterPage = pageTemplate.MasterPage;
      this.AvailableLanguages = pageTemplate.AvailableLanguages;
      this.Name = pageTemplate.Name;
      IRendererCommonData rendererCommonData = (IRendererCommonData) pageTemplate;
      if (rendererCommonData == null)
        return;
      this.Renderer = rendererCommonData.Renderer;
      this.TemplateName = rendererCommonData.TemplateName;
    }

    /// <summary>Gets or sets the template id</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the title of the template</summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the title of the template</summary>
    [DataMember]
    public string DuplicateTitle
    {
      get
      {
        if (string.IsNullOrEmpty(this._duplicateTitle))
          this._duplicateTitle = string.Format("{0}{1}", (object) this.Title, (object) "_copy");
        return this._duplicateTitle;
      }
      set => this._duplicateTitle = value;
    }

    /// <summary>Gets or sets the framework of the page template.</summary>
    [DataMember]
    public PageTemplateFramework Framework { get; set; }

    /// <summary>Gets or sets the template icon URL.</summary>
    /// <value>The template icon URL.</value>
    [DataMember]
    public string TemplateIconUrl { get; set; }

    /// <summary>Gets or sets the template small icon URL.</summary>
    /// <value>The template small icon URL.</value>
    [DataMember]
    public string TemplateSmallIconUrl { get; set; }

    /// <summary>Gets or sets the edit URL.</summary>
    [DataMember]
    public string EditUrl { get; set; }

    /// <summary>Gets or sets the master page.</summary>
    /// <value>The master page.</value>
    [DataMember]
    public string MasterPage { get; set; }

    /// <summary>
    /// This flag is used when master page is selected as parent template and no template has to be created as a parent.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is dummy parrent template; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool NotCreateTemplateForMasterPage { get; set; }

    /// <summary>Gets or sets the type of the root taxon.</summary>
    /// <value>The type of the root taxon.</value>
    [DataMember]
    public string RootTaxonType { get; set; }

    /// <summary>Gets languages available for this item.</summary>
    /// <value>The available languages.</value>
    [DataMember]
    public virtual string[] AvailableLanguages { get; set; }

    /// <summary>
    /// Gets or sets the id of the pageData to which the data in this object is a translation.
    /// Only used in multilingual environment. Only set when creating a translated version of
    /// the page.
    /// </summary>
    /// <value>The source language page id.</value>
    [DataMember]
    public Guid SourceLanguagePageId { get; set; }

    /// <summary>
    /// Gets or sets the name of the language from which to copy properties. Only used in when
    /// creating a language version of existing template. If null, no properties are copied.
    /// </summary>
    /// <value>The name of the language.</value>
    [DataMember]
    public string SourceLanguage { get; set; }

    /// <summary>Gets or sets the name for developers.</summary>
    /// <value>The developer name.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the name of renderer used for the page.</summary>
    [DataMember]
    public string Renderer { get; set; }

    /// <summary>Gets or sets the name of the template.</summary>
    /// <remarks>This is intended for use in the external render <seealso cref="P:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageTemplate.Renderer" /></remarks>
    [DataMember]
    public string TemplateName { get; set; }
  }
}
