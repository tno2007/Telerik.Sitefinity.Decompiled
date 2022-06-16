// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public class TemplateSelectorDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.TemplateSelectorDialog.ascx");
    private const string dialogScript = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.TemplateSelectorDialog.js";
    /// <summary>
    /// The identifier of the basic custom templates category.
    /// </summary>
    public static readonly Guid BasicTemplatesCategoryId = new Guid("F669D9A7-009D-4d83-AABB-000000000001");
    /// <summary>The identifier of the Backend taxon.</summary>
    public static readonly Guid BackendTaxonId = SiteInitializer.BackendTemplatesCategoryId;
    /// <summary>The path to NoTemplate image.</summary>
    public const string NoTemplateImagePath = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.NoTemplate.gif";
    private string pageId;
    private string makeTemplateFromMasterFileServiceUrl = "~/Sitefinity/Services/Pages/PagesService.svc/template/createFromMaster/";
    private string getTemplateByPageDataServiceUrl = "/Services/Pages/PagesService.svc/template/";
    private TaxonomyManager taxonomyManager;
    private RootTaxonType rootTaxon;
    private PageTemplate currentTemplate;
    private PageManager pageManager;
    private DialogModes mode;
    private Dictionary<Control, PageTemplate> templateItemElements;
    private CultureInfo language;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (TemplateSelectorDialog).FullName;

    /// <summary>Gets or sets the taxonomy manager.</summary>
    /// <value>The taxonomy manager.</value>
    public TaxonomyManager TaxonomyManager
    {
      get
      {
        if (this.taxonomyManager == null)
          this.taxonomyManager = TaxonomyManager.GetManager();
        return this.taxonomyManager;
      }
      set => this.taxonomyManager = value;
    }

    /// <summary>
    /// Gets or sets the taxon used as root for page navigation.
    /// </summary>
    /// <value>The root taxon.</value>
    public RootTaxonType RootTaxon
    {
      get
      {
        NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
        if (!string.IsNullOrEmpty(queryString["rootTaxonType"]))
          this.rootTaxon = (RootTaxonType) Enum.Parse(typeof (RootTaxonType), queryString["rootTaxonType"]);
        return this.rootTaxon;
      }
      set => this.rootTaxon = value;
    }

    /// <summary>Gets or sets the current template.</summary>
    /// <value>The current template.</value>
    public PageTemplate CurrentTemplate
    {
      get => this.currentTemplate;
      set => this.currentTemplate = value;
    }

    /// <summary>Gets or sets the page manager.</summary>
    /// <value>The page manager.</value>
    public PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager();
        return this.pageManager;
      }
      set => this.pageManager = value;
    }

    /// <summary>Gets or sets the mode of the Select Template dialog.</summary>
    /// <value>The mode.</value>
    public DialogModes Mode
    {
      get => this.mode;
      set => this.mode = value;
    }

    /// <summary>
    /// Specifies wheter to show or hide the otion for selecting blank (no template)
    /// </summary>
    public bool ShowEmptyTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if to create template for master page.
    /// The flag is used when creating template based on master page to indicate not to create template for master page.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [do not create template for master page]; otherwise, <c>false</c>.
    /// </value>
    public bool NotCreateTemplateForMasterPage { get; set; }

    protected PageTemplate NewTemplate { get; set; }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TemplateSelectorDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the service url for the service that creates templates from master files.
    /// </summary>
    public string MakeTemplateFromMasterFileServiceUrl
    {
      get => VirtualPathUtility.ToAbsolute(this.makeTemplateFromMasterFileServiceUrl);
      set => this.makeTemplateFromMasterFileServiceUrl = value;
    }

    /// <summary>
    /// Contains all the bound templates (controls) and their corresponding ids
    /// </summary>
    protected virtual Dictionary<Control, PageTemplate> TemplateItemElements
    {
      get
      {
        if (this.templateItemElements == null)
          this.templateItemElements = new Dictionary<Control, PageTemplate>();
        return this.templateItemElements;
      }
    }

    /// <summary>Gets the templates repeater</summary>
    protected virtual Repeater TaxonomiesRepeater => this.Container.GetControl<Repeater>("taxonomiesRepeater", true);

    /// <summary>Gets the warning message control.</summary>
    /// <value>The warning message control.</value>
    protected virtual HtmlControl WarningMessageControl => this.Container.GetControl<HtmlControl>("warningMessage", true);

    /// <summary>Gets the templates list</summary>
    protected virtual ItemsList TemplatesList => this.Container.GetControl<ItemsList>("templatesList", true);

    /// <summary>
    /// Gets the reference to the control that represents the command bar.
    /// </summary>
    /// <value>The command bar.</value>
    protected internal virtual CommandBar BottomCommandBar => this.Container.GetControl<CommandBar>("bottomCommandBar", true);

    /// <summary>Gets the master page selector dialog.</summary>
    /// <value>The master page selector dialog.</value>
    protected virtual Telerik.Web.UI.RadWindow MasterPageSelectorDialog => this.Container.GetControl<Telerik.Web.UI.RadWindow>("masterPageSelectorDialog", true);

    /// <summary>
    /// Gets the reference to the "Use this template as default" checkbox.
    /// </summary>
    protected virtual CheckBox UseAsDefaultTemplateCheckbox => this.Container.GetControl<CheckBox>("useAsDefaultTemplateCheckBox", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string masterPage = this.Page.Request.QueryString["masterPage"];
      if (!string.IsNullOrEmpty(masterPage))
        this.NewTemplate = this.CreateTemplate(masterPage);
      string str1 = this.Page.Request.QueryString["pageId"];
      if (!string.IsNullOrEmpty(str1))
        this.pageId = str1;
      string name = this.Page.Request.QueryString["language"];
      if (!string.IsNullOrEmpty(name))
        this.language = new CultureInfo(name);
      string str2 = this.Page.Request.QueryString["showEmptyTemplate"];
      if (!string.IsNullOrEmpty(str2))
        this.ShowEmptyTemplate = bool.Parse(str2);
      string str3 = this.Page.Request.QueryString["notCreateTemplateForMasterPage"];
      if (!string.IsNullOrEmpty(str3))
        this.NotCreateTemplateForMasterPage = bool.Parse(str3);
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      string str4 = this.Page.Request.QueryString["framework"];
      if (!string.IsNullOrEmpty(str4) && !pagesConfig.AllowPageTemplatesFrameworkChange)
        this.Framework = new PageTemplateFramework?((PageTemplateFramework) Enum.Parse(typeof (PageTemplateFramework), str4));
      string str5 = this.Page.Request.QueryString["showAllBasicTemplates"];
      this.ShowAllBasicTemplates = !string.IsNullOrEmpty(str5) && bool.Parse(str5);
      string pageTaxonomyName = pagesConfig.PageTemplatesTaxonomyName;
      HierarchicalTaxonomy hierarchicalTaxonomy = this.TaxonomyManager.GetTaxonomies<HierarchicalTaxonomy>().Single<HierarchicalTaxonomy>((Expression<Func<HierarchicalTaxonomy, bool>>) (t => t.Name == pageTaxonomyName));
      List<Taxon> list;
      if (this.RootTaxon == RootTaxonType.Backend)
      {
        list = hierarchicalTaxonomy.Taxa.Where<Taxon>((Func<Taxon, bool>) (t => t.Id == TemplateSelectorDialog.BackendTaxonId)).ToList<Taxon>();
      }
      else
      {
        list = hierarchicalTaxonomy.Taxa.Where<Taxon>((Func<Taxon, bool>) (t => t.Id != TemplateSelectorDialog.BackendTaxonId)).OrderBy<Taxon, float>((Func<Taxon, float>) (t => t.Ordinal)).ToList<Taxon>();
        int index = list.FindIndex((Predicate<Taxon>) (x => x.Id == SiteInitializer.BasicTemplatesCategoryId));
        if (index >= 0)
        {
          Taxon taxon = list[index];
          list.RemoveAt(index);
          list.Add(taxon);
        }
        PageTemplatesAvailability templatesFrameworks = Config.Get<PagesConfig>().PageTemplatesFrameworks;
        if (this.ShowAllBasicTemplates)
        {
          PageTemplateFramework? framework;
          if (templatesFrameworks == PageTemplatesAvailability.All)
          {
            framework = this.Framework;
            if (!framework.HasValue)
              goto label_20;
          }
          framework = this.Framework;
          PageTemplateFramework templateFramework = PageTemplateFramework.WebForms;
          if (!(framework.GetValueOrDefault() == templateFramework & framework.HasValue))
            goto label_21;
label_20:
          HierarchicalTaxon taxon = this.TaxonomyManager.CreateTaxon<HierarchicalTaxon>(SiteInitializer.WebFormsBasicTemplatesCategoryId);
          taxon.Id = SiteInitializer.WebFormsBasicTemplatesCategoryId;
          taxon.Title = (Lstring) Res.Get<PageResources>().BasicWebformsTemplates;
          list.Add((Taxon) taxon);
        }
      }
label_21:
      this.TaxonomiesRepeater.ItemCreated += new RepeaterItemEventHandler(this.TaxonomiesRepeater_ItemCreated);
      this.TaxonomiesRepeater.DataSource = (object) list;
      this.TaxonomiesRepeater.DataBind();
      if (AppPermission.Root.IsGranted("Backend", "ChangeConfigurations"))
        return;
      this.UseAsDefaultTemplateCheckbox.Attributes.Add("style", "display:none");
    }

    private void TaxonomiesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      HierarchicalTaxon dataItem = (HierarchicalTaxon) e.Item.DataItem;
      if (dataItem == null)
        return;
      List<PageTemplate> source = this.GetTemplates(dataItem.Id);
      Repeater control1 = (Repeater) e.Item.FindControl("templatesRepeater");
      HtmlControl control2 = (HtmlControl) e.Item.FindControl("templatesWrapper");
      if (dataItem.Id == TemplateSelectorDialog.BasicTemplatesCategoryId && source.Count > 0 && this.ShowEmptyTemplate && !source.Any<PageTemplate>((Func<PageTemplate, bool>) (x => x.Id == SiteInitializer.HybridEmptyTemplateId)))
      {
        PageTemplate basicEmptyTemplate = PageTemplateHelper.CheckCreateOrMockAndReturnBasicEmptyTemplate(SiteInitializer.HybridEmptyTemplateId, false);
        source = source.Prepend<PageTemplate>(basicEmptyTemplate).ToList<PageTemplate>();
        if (control2 != null)
          control2.Visible = true;
      }
      if (source.Count <= 0 || control2 == null)
        return;
      control2.Visible = true;
      ITextControl control3 = (ITextControl) e.Item.FindControl("taxonomyName");
      if (control3 != null)
        control3.Text = (string) dataItem.Title;
      ITextControl control4 = (ITextControl) e.Item.FindControl("taxonomySubtitle");
      if (dataItem.Id != SiteInitializer.CustomTemplatesCategoryId && control4 != null)
      {
        string categorySubtitle = this.GetTemplateCategorySubtitle(source.First<PageTemplate>().Framework);
        control4.Text = categorySubtitle;
      }
      if (control1 == null)
        return;
      control1.ItemCreated += new RepeaterItemEventHandler(this.TemplatesRepeater_ItemCreated);
      control1.DataSource = (object) source;
    }

    private string GetTemplateCategorySubtitle(PageTemplateFramework framework)
    {
      if (framework == PageTemplateFramework.Mvc)
        return Res.Get<PageResources>().ForMvcWidgetsLabel;
      return framework == PageTemplateFramework.WebForms ? Res.Get<PageResources>().ForWebformsWidgetsLabel : Res.Get<PageResources>().ForAllWidgetsLabel;
    }

    private void TemplatesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      PageTemplate dataItem = (PageTemplate) e.Item.DataItem;
      if (dataItem == null)
        return;
      this.TemplateItemElements.Add(e.Item.FindControl("templItem"), dataItem);
      System.Web.UI.WebControls.Image control1 = (System.Web.UI.WebControls.Image) e.Item.FindControl("screenshot");
      if (control1 != null)
        control1.ImageUrl = !PageTemplateHelper.IsOnDemandTempalteId(dataItem.Id) ? (!(dataItem.Category == SiteInitializer.WebFormsBasicTemplatesCategoryId) ? dataItem.GetBigThumbnailUrl((Telerik.Sitefinity.Libraries.Model.Image) null, this.Page) : PageTemplateHelper.GetMockedTemplateImage(dataItem.Id)) : ControlUtilities.ResolveResourceUrl("Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.NoTemplate.gif", this.Page);
      ITextControl control2 = (ITextControl) e.Item.FindControl("templateName");
      if (control2 != null)
      {
        string str = this.language == null ? (string) dataItem.Title : dataItem.Title.GetString(this.language, true);
        control2.Text = str;
      }
      ((HtmlControl) e.Item.FindControl("templateFramework"))?.Attributes.Add("class", "sfFramework" + (object) (int) dataItem.Framework);
      this.SetTemplatePagesCount(e, dataItem);
    }

    private void SetTemplatePagesCount(RepeaterItemEventArgs e, PageTemplate template)
    {
      HtmlAnchor control = (HtmlAnchor) e.Item.FindControl("templatePagesLink");
      if (control == null)
        return;
      int basesPagesCount = template.GetBasesPagesCount(SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId);
      if (basesPagesCount > 0)
      {
        string str = "openTemplatePagesDialog('" + RouteHelper.ResolveUrl("~/Sitefinity/Dialog/TemplatePagesDialog" + "?rootTaxon=" + (object) this.RootTaxon + "&id=" + (object) template.Id, UrlResolveOptions.Rooted) + "');";
        control.Attributes.Add("onclick", str);
      }
      if (basesPagesCount == 1)
        control.InnerHtml = string.Format(Res.Get<PageResources>().PageCount, (object) basesPagesCount);
      else
        control.InnerHtml = string.Format(Res.Get<PageResources>().PagesCount, (object) basesPagesCount);
    }

    /// <summary>
    /// Gets the templates by category and sorts them by title in asceding order.
    /// </summary>
    /// <param name="category">The category.</param>
    /// <param name="templateId">The currently created/edited template pageId.</param>
    /// <returns></returns>
    protected List<PageTemplate> GetTemplates(Guid category)
    {
      if (category == SiteInitializer.WebFormsBasicTemplatesCategoryId)
        return PageTemplateHelper.CreateWebformsBasicTemplates(SystemManager.MultisiteContext != null ? SystemManager.MultisiteContext.CurrentSite.Id : Guid.Empty).OrderBy<PageTemplate, short>((Func<PageTemplate, short>) (x => x.Ordinal)).ToList<PageTemplate>();
      bool flag = this.RootTaxon == RootTaxonType.Backend;
      IQueryable<PageTemplate> queryable = SystemManager.MultisiteContext == null ? this.PageManager.GetTemplates() : this.PageManager.GetTemplates(SystemManager.MultisiteContext.CurrentSite.Id);
      if (!flag && !this.Framework.HasValue)
        queryable = PageTemplateHelper.FilterFrameworkSpecificTemplates(queryable);
      else if (!flag && this.Framework.HasValue)
        queryable = queryable.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => (int?) t.Framework == (int?) this.Framework));
      List<PageTemplate> list = queryable.Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category == category)).OrderBy<PageTemplate, Lstring>((Expression<Func<PageTemplate, Lstring>>) (t => t.Title)).ToList<PageTemplate>();
      int index = list.FindIndex((Predicate<PageTemplate>) (x => x.Title == (Lstring) "default"));
      if (index > 0)
      {
        PageTemplate element = list[index];
        list.RemoveAt(index);
        list = list.Prepend<PageTemplate>(element).ToList<PageTemplate>();
      }
      return list;
    }

    public static WcfPageTemplate GetNoTemplateItem(Page page) => new WcfPageTemplate()
    {
      TemplateIconUrl = ControlUtilities.ResolveResourceUrl("Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.NoTemplate.gif", page),
      Id = Guid.Empty,
      Title = "No template"
    };

    protected PageTemplate CreateTemplate(string masterPage)
    {
      masterPage = VirtualPathUtility.ToAppRelative(masterPage);
      PageTemplate template;
      if (!this.PageManager.GetTemplates().Any<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.MasterPage == masterPage)))
      {
        string str = VirtualPathUtility.GetFileName(masterPage);
        int num = str.LastIndexOf(".");
        if (num > -1)
          str = str.Sub(0, num - 1);
        template = this.PageManager.CreateTemplate();
        template.Name = str;
        template.Title = (Lstring) str;
        template.MasterPage = masterPage;
        template.Category = this.RootTaxon != RootTaxonType.Backend ? SiteInitializer.CustomTemplatesCategoryId : SiteInitializer.BackendTemplatesCategoryId;
        if (this.PageManager.TransactionName.IsNullOrEmpty())
          this.PageManager.SaveChanges();
        else
          TransactionManager.CommitTransaction(this.PageManager.TransactionName);
      }
      else
        template = this.PageManager.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.MasterPage == masterPage));
      return template;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    internal PageTemplateFramework? Framework { get; private set; }

    internal bool ShowAllBasicTemplates { get; private set; }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = this.GetBaseScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_webServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Pages/PagesService.svc/"));
      controlDescriptor.AddProperty("_useAsDefaultTemplateCheckboxId", (object) this.UseAsDefaultTemplateCheckbox.ClientID);
      if (this.pageId != null)
        controlDescriptor.AddProperty("_pageId", (object) this.pageId);
      if (this.NewTemplate != null)
        controlDescriptor.AddProperty("newTemplate", (object) new WcfPageTemplate(this.NewTemplate));
      controlDescriptor.AddProperty("_templateItemElements", (object) this.TemplateItemElements.ToDictionary<KeyValuePair<Control, PageTemplate>, string, WcfPageTemplate>((Func<KeyValuePair<Control, PageTemplate>, string>) (k => k.Key.ClientID), (Func<KeyValuePair<Control, PageTemplate>, WcfPageTemplate>) (v => v.Value == null ? new WcfPageTemplate() : new WcfPageTemplate(v.Value))));
      controlDescriptor.AddProperty("showEmptyTemplate", (object) this.ShowEmptyTemplate);
      controlDescriptor.AddProperty("notCreateTemplateForMasterPage", (object) this.NotCreateTemplateForMasterPage);
      string str = "{0}?rootTaxonType={1}".Arrange((object) this.MakeTemplateFromMasterFileServiceUrl, (object) this.RootTaxon);
      controlDescriptor.AddProperty("_makeTemplateFromMasterFileServiceUrl", (object) str);
      controlDescriptor.AddProperty("_getTemplateByPageDataServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity" + this.getTemplateByPageDataServiceUrl));
      controlDescriptor.AddComponentProperty("bottomCommandBar", this.BottomCommandBar.ClientID);
      controlDescriptor.AddComponentProperty("masterPageSelectorDialog", this.MasterPageSelectorDialog.ClientID);
      controlDescriptor.AddComponentProperty("warningMessageControl", this.WarningMessageControl.ClientID);
      PageTemplatesAvailability templatesAvailability = Config.Get<PagesConfig>().PageTemplatesFrameworks;
      PageTemplateFramework? framework = this.Framework;
      if (framework.HasValue)
      {
        framework = this.Framework;
        PageTemplateFramework templateFramework1 = PageTemplateFramework.Mvc;
        if (framework.GetValueOrDefault() == templateFramework1 & framework.HasValue)
        {
          templatesAvailability = PageTemplatesAvailability.MvcOnly;
        }
        else
        {
          framework = this.Framework;
          PageTemplateFramework templateFramework2 = PageTemplateFramework.WebForms;
          templatesAvailability = !(framework.GetValueOrDefault() == templateFramework2 & framework.HasValue) ? PageTemplatesAvailability.HybridAndMvc : PageTemplatesAvailability.All;
        }
      }
      controlDescriptor.AddProperty("_availableFrameworks", (object) templatesAvailability);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (TemplateSelectorDialog).Assembly.FullName;
      ScriptReference scriptReference = new ScriptReference()
      {
        Assembly = fullName,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      };
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(this.GetBaseScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.TemplateSelectorDialog.js", fullName),
        scriptReference
      };
    }

    internal virtual List<ScriptDescriptor> GetBaseScriptDescriptors() => new List<ScriptDescriptor>(base.GetScriptDescriptors());

    internal virtual IEnumerable<ScriptReference> GetBaseScriptReferences() => base.GetScriptReferences();
  }
}
