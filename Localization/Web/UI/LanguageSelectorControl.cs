// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Localization.Web.UI.Designers;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Localization.Web.UI
{
  /// <summary>Public control for displaying a form.</summary>
  [ControlDesigner(typeof (LanguageSelectorDesigner))]
  [PropertyEditorTitle(typeof (Labels), "LanguageSelectorDesignerEditorTitle")]
  [RequireScriptManager]
  [IndexRenderMode(IndexRenderModes.NoOutput)]
  public class LanguageSelectorControl : SimpleView
  {
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.LanguageSelectorControl.ascx");
    protected UrlLocalizationService urlService;
    protected List<CultureInfo> usedLanguages;
    protected PageNode node;
    protected PageNode homePageNode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.Web.UI.LanguageSelectorControl" /> class.
    /// </summary>
    public LanguageSelectorControl()
    {
      this.urlService = ObjectFactory.Resolve<UrlLocalizationService>();
      this.LayoutTemplatePath = LanguageSelectorControl.layoutTemplatePath;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the type of the selector element - vertical list, horizontal list, drop-down, etc.
    /// </summary>
    /// <value>The type of the selector element - vertical list, horizontal list, drop-down, etc.</value>
    public LanguageSelectorType SelectorType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to include the current language in the list of languages.
    /// </summary>
    /// <value><c>true</c> in order to include the current language in the list of languages; otherwise, <c>false</c>.</value>
    public bool ShowCurrentLanguage { get; set; }

    /// <summary>
    /// Gets or sets the behavior of the control for missing translations.
    /// </summary>
    /// <value>The behavior of the control for missing translations.</value>
    public NoTranslationAction MissingTranslationAction { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the label containing the success message.</summary>
    /// <value>The success message label.</value>
    protected HtmlControl Wrapper => this.Container.GetControl<HtmlControl>("controlWrapper", true);

    /// <summary>Panel that represents the error messages</summary>
    protected Panel ErrorsPanel => this.Container.GetControl<Panel>("errorsPanel", true);

    /// <summary>Panel that represents the error messages</summary>
    protected Repeater LanguagesRepeater => this.Container.GetControl<Repeater>(this.SelectorType != LanguageSelectorType.Vertical ? (this.SelectorType != LanguageSelectorType.Horizontal ? (string) null : "languagesRepeater_horizontal") : "languagesRepeater_vertical", true);

    /// <summary>
    /// The drop down used to select current language. Only available when in DropDown mode.
    /// </summary>
    protected DropDownList LanguagesDropDown => this.Container.GetControl<DropDownList>("langsSelect", true);

    /// <summary>
    /// Overridden. Calls Evaluate on the conditional template container to correctly use the controls inside of the templates
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      ConditionalTemplateContainer control = container.GetControl<ConditionalTemplateContainer>();
      if (control == null)
        return container;
      control.Evaluate((object) this);
      return container;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.GetIndexRenderMode() == IndexRenderModes.NoOutput)
        return;
      bool flag = false;
      if (this.Page.Items.Contains((object) "IsTemplate"))
        flag = (bool) this.Page.Items[(object) "IsTemplate"];
      else if (this.Page.Items.Contains((object) "DesignMediaType"))
        flag = (DesignMediaType) this.Page.Items[(object) "DesignMediaType"] == DesignMediaType.Template;
      if ((!flag ? 1 : (!this.IsDesignMode() ? 1 : 0)) == 0)
        return;
      if (!SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (this.IsDesignMode())
          this.ErrorsPanel.Controls.Add((Control) new Label()
          {
            Text = Res.Get<Labels>().OnlyAvailableInMultilingualMode
          });
        this.Visible = false;
      }
      else
      {
        this.ErrorsPanel.Visible = false;
        this.InitializeContainers();
      }
    }

    /// <summary>Initializes the containers displaying the languages</summary>
    protected virtual void InitializeContainers() => this.BindLanguageContainers(this.GetLanguagesToDisplay());

    /// <inheritDoc />
    public override void RenderControl(HtmlTextWriter writer)
    {
      if (this.SelectorType == LanguageSelectorType.DropDown)
      {
        foreach (ListItem listItem in this.LanguagesDropDown.Items)
          listItem.Value = this.AppendDetailItemAndParamsToUrl(listItem.Value, CultureInfo.GetCultureInfo(listItem.Attributes["lang"]));
      }
      else
      {
        foreach (Control control1 in this.LanguagesRepeater.Items)
        {
          HtmlAnchor control2 = (HtmlAnchor) control1.FindControl("langLink");
          if (control2 != null)
          {
            string attribute = control2.Attributes["lang"];
            string href = control2.HRef;
            control2.HRef = this.AppendDetailItemAndParamsToUrl(href, CultureInfo.GetCultureInfo(attribute));
          }
        }
      }
      base.RenderControl(writer);
    }

    /// <summary>
    /// Binds the langauge containers (repeater or drop down) with the specified languages
    /// </summary>
    /// <param name="shownLanguages">The languages.</param>
    protected virtual void BindLanguageContainers(IEnumerable<CultureInfo> languages)
    {
      if (this.SelectorType == LanguageSelectorType.DropDown)
      {
        DropDownList languagesDropDown = this.LanguagesDropDown;
        if (!this.IsDesignMode())
          languagesDropDown.Attributes["onChange"] = "document.location.href = this.value;";
        foreach (CultureInfo language in languages)
        {
          ListItem listItem = new ListItem(this.GetDisplayedLanguageName(language), RouteHelper.ResolveUrl(this.GetUrlForLanguage(language), UrlResolveOptions.Absolute));
          listItem.Selected = language.Equals((object) SystemManager.CurrentContext.Culture);
          listItem.Attributes.Add("lang", language.Name);
          languagesDropDown.Items.Add(listItem);
        }
      }
      else
      {
        this.LanguagesRepeater.ItemCreated += new RepeaterItemEventHandler(this.LanguagesRepeater_ItemCreated);
        this.LanguagesRepeater.DataSource = (object) languages;
        this.LanguagesRepeater.DataBind();
      }
    }

    /// <summary>Appends the detail item and parameters to URL.</summary>
    /// <param name="url">The URL.</param>
    /// <param name="ci">The culture info.</param>
    /// <returns></returns>
    protected string AppendDetailItemAndParamsToUrl(string url, CultureInfo ci)
    {
      string query = this.Page.Request.Url.Query;
      object obj = SystemManager.CurrentHttpContext.Items[(object) "detailItem"];
      if (!(obj is ILocatableExtended locatableExtended) || !(obj is ILifecycleDataItem))
        return url + query;
      string empty = string.Empty;
      if (((ILifecycleDataItem) obj).IsPublishedInCulture(ci))
        empty = locatableExtended.ItemDefaultUrl.GetString(ci, false);
      return url + empty + query;
    }

    /// <summary>Gets the languages to display.</summary>
    /// <returns></returns>
    protected virtual IEnumerable<CultureInfo> GetLanguagesToDisplay()
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      PageSiteNode actualSitemapNode = SiteMapBase.GetActualCurrentNode();
      PageManager manager = PageManager.GetManager();
      Guid homePageId = SystemManager.CurrentContext.CurrentSite.HomePageId;
      Guid id = actualSitemapNode == null ? homePageId : actualSitemapNode.Id;
      this.node = manager.GetPageNode(id);
      IEnumerable<CultureInfo> source1 = actualSitemapNode == null ? (IEnumerable<CultureInfo>) this.node.AvailableCultures : (IEnumerable<CultureInfo>) actualSitemapNode.AvailableLanguages;
      CultureInfo currentLanguage = SystemManager.CurrentContext.Culture;
      this.usedLanguages = new List<CultureInfo>();
      if (actualSitemapNode != null)
        source1.ToList<CultureInfo>().ForEach((Action<CultureInfo>) (ci =>
        {
          if (ci.Equals((object) CultureInfo.InvariantCulture) || actualSitemapNode.IsHidden(ci))
            return;
          this.usedLanguages.Add(ci);
        }));
      List<CultureInfo> source2 = new List<CultureInfo>();
      if (this.MissingTranslationAction == NoTranslationAction.HideLink)
      {
        source2.AddRange((IEnumerable<CultureInfo>) this.usedLanguages);
      }
      else
      {
        source2.AddRange((IEnumerable<CultureInfo>) appSettings.DefinedFrontendLanguages);
        if (homePageId != Guid.Empty)
          this.homePageNode = manager.GetPageNode(homePageId);
      }
      return this.ShowCurrentLanguage || this.SelectorType == LanguageSelectorType.DropDown ? (IEnumerable<CultureInfo>) source2 : source2.Where<CultureInfo>((Func<CultureInfo, bool>) (ci => !ci.Equals((object) currentLanguage)));
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    protected virtual string GetDisplayedLanguageName(CultureInfo lang) => lang.NativeName;

    protected virtual string GetUrlForLanguage(CultureInfo ci)
    {
      string urlForLanguage = (string) null;
      if (this.MissingTranslationAction == NoTranslationAction.RedirectToPage && !this.usedLanguages.Contains(ci))
        urlForLanguage = this.GetMissingLanguageUrl(ci);
      if (urlForLanguage == null)
      {
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        SystemManager.CurrentContext.Culture = ci;
        try
        {
          SiteMapNode siteMapNodeFromKey = SiteMapBase.GetCurrentProvider().FindSiteMapNodeFromKey(this.node.Id.ToString());
          urlForLanguage = siteMapNodeFromKey == null ? this.urlService.ResolveUrl("~/", ci) : siteMapNodeFromKey.Url;
        }
        finally
        {
          SystemManager.CurrentContext.Culture = culture;
        }
      }
      return urlForLanguage;
    }

    /// <summary>
    /// Returns the URL used for links to missing language version. You can override this for custom behavior.
    /// </summary>
    /// <param name="ci">The language that is missing.</param>
    /// <returns></returns>
    protected virtual string GetMissingLanguageUrl(CultureInfo ci) => this.homePageNode != null ? this.urlService.ResolvePageUrl(this.homePageNode, ci, true) : (string) null;

    private void LanguagesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      CultureInfo dataItem = (CultureInfo) e.Item.DataItem;
      string displayedLanguageName = this.GetDisplayedLanguageName(dataItem);
      string urlForLanguage = this.GetUrlForLanguage(dataItem);
      if (this.SelectorType == LanguageSelectorType.DropDown)
      {
        HtmlGenericControl control = (HtmlGenericControl) e.Item.FindControl("langOption");
        control.Attributes.Add("lang", dataItem.Name);
        control.Attributes.Add("class", "sflangOption_" + dataItem.Name);
        control.InnerHtml = displayedLanguageName;
      }
      else
      {
        HtmlGenericControl control1 = (HtmlGenericControl) e.Item.FindControl("langHolder");
        string attribute = control1.Attributes["class"];
        string str1 = "sflang_" + dataItem.Name;
        string str2 = string.IsNullOrEmpty(attribute) ? str1 : attribute + " " + str1;
        if (dataItem.Equals((object) SystemManager.CurrentContext.Culture))
          str2 += " sflangSelected";
        control1.Attributes.Add("class", str2);
        HtmlAnchor control2 = (HtmlAnchor) e.Item.FindControl("langLink");
        control2.HRef = urlForLanguage;
        control2.Attributes.Add("lang", dataItem.Name);
        ((HtmlContainerControl) e.Item.FindControl("langName")).InnerHtml = displayedLanguageName;
      }
    }
  }
}
