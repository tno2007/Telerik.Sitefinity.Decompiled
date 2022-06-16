// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxonList
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>
  /// Abstract class which provides basic functionality for all taxon list controls
  /// </summary>
  public abstract class TaxonList : ViewModeControl<TaxonomyBasePanel>
  {
    private IAppSettings appSettings;
    private bool? supportsMultiligual;

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multiligual.
    /// </summary>
    /// <value><c>true</c> if supports multiligual; otherwise, <c>false</c>.</value>
    public bool SupportsMultiligual
    {
      get
      {
        if (!this.supportsMultiligual.HasValue)
          this.supportsMultiligual = new bool?(this.AppSettings.Multilingual);
        return this.supportsMultiligual.Value;
      }
      set => this.supportsMultiligual = new bool?(value);
    }

    protected virtual IAppSettings AppSettings
    {
      get
      {
        if (this.appSettings == null)
          this.appSettings = (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;
        return this.appSettings;
      }
    }

    /// <summary>
    /// Gets the reference to the decision screen control present on all taxon list controls
    /// </summary>
    protected virtual DecisionScreen DecisionScreen => this.Container.GetControl<DecisionScreen>("decisionScreen", true);

    /// <summary>Gets the items list.</summary>
    public abstract ItemsListBase ItemsList { get; }

    /// <summary>Gets the show translations link.</summary>
    protected virtual HyperLink ShowTranslationsLink => this.Container.GetControl<HyperLink>("showMoreTranslations", false);

    /// <summary>Gets the hide translations link.</summary>
    protected virtual HyperLink HideTranslationsLink => this.Container.GetControl<HyperLink>("hideMoreTranslations", false);

    /// <summary>Gets the show/hide translations links container.</summary>
    protected virtual Panel TranslationsLinksContainer => this.Container.GetControl<Panel>("translationsLinksContainer", false);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    /// <param name="viewContainer">The control that will host the current view.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      if ((this.Page.Request.QueryString["status"] == null ? 0 : (this.Page.Request.QueryString["status"] == "created" ? 1 : 0)) != 0)
      {
        this.DecisionScreen.MessageType = MessageType.Positive;
        this.DecisionScreen.MessageText = Res.Get<TaxonomyResources>().ClassificationHasBeenSuccessfullyCreated.Arrange((object) HttpUtility.HtmlEncode((string) this.Host.Taxonomy.Title));
      }
      else
      {
        this.DecisionScreen.MessageType = MessageType.Neutral;
        this.DecisionScreen.MessageText = Res.Get<Labels>().NoItemsHaveBeenCreatedYet.Arrange((object) HttpUtility.HtmlEncode(this.Host.Taxonomy.Title.ToLower()));
      }
      this.DecisionScreen.ActionItems.First<ActionItem>().Title = Res.Get<Labels>().CreateYourFirstItem.Arrange((object) this.Host.Taxonomy.TaxonName.ToLower());
      this.Page.Title = this.Host.Title;
      this.SetTranslations();
      this.SetLocalizationControlsVisibility();
    }

    protected string GetMarkedItemsUrl()
    {
      string str = string.Empty;
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(SiteInitializer.MarkedItemsPageId, false);
      if (siteMapNode != null)
        str = RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
      return str + "/{{UrlPath}}";
    }

    /// <summary>Sets the translations column.</summary>
    private void SetTranslations()
    {
      ItemDescription itemDescription = this.ItemsList.Items.Where<ItemDescription>((Func<ItemDescription, bool>) (i => i.Name == "Translations")).FirstOrDefault<ItemDescription>();
      if (itemDescription == null)
        return;
      if (this.SupportsMultiligual)
        itemDescription.Markup = new LanguagesColumnMarkupGenerator()
        {
          LanguageSource = LanguageSource.Frontend,
          ItemsInGroupCount = 6,
          ContainerTag = "div",
          GroupTag = "div",
          ItemTag = "div",
          ContainerClass = string.Empty,
          GroupClass = string.Empty,
          ItemClass = string.Empty
        }.GetMarkup();
      else
        this.ItemsList.Items.Remove(itemDescription);
    }

    private void SetLocalizationControlsVisibility()
    {
      if (this.ShowTranslationsLink == null || this.HideTranslationsLink == null)
        return;
      if (this.AppSettings.DefinedFrontendLanguages.Length > 6)
      {
        this.ShowTranslationsLink.Visible = this.SupportsMultiligual;
        this.HideTranslationsLink.Visible = this.SupportsMultiligual;
        if (!this.SupportsMultiligual)
          return;
        this.HideTranslationsLink.Style.Add(HtmlTextWriterStyle.Display, "none");
      }
      else
      {
        if (this.TranslationsLinksContainer == null)
          return;
        this.TranslationsLinksContainer.Visible = false;
      }
    }
  }
}
