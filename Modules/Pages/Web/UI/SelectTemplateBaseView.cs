// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.SelectTemplateBaseView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
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
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Represents the base view for selecting templates.</summary>
  public abstract class SelectTemplateBaseView : SimpleView
  {
    private TaxonomyManager taxonomyManager;
    private PageManager pageManager;
    private ScriptManager scriptManager;
    private DialogModes mode;
    private PageData currentPage;
    private PageTemplate currentTemplate;
    private RootTaxonType rootTaxon;
    private static readonly object EventReturnToPropertiesCommand;
    /// <summary>Gets the path of resource file representing the view.</summary>
    public static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.SelectTemplateView.ascx");
    /// <summary>
    /// The identifier of the basic custom templates category.
    /// </summary>
    public static readonly Guid BasicTemplatesCategoryId = new Guid("F669D9A7-009D-4d83-AABB-000000000001");
    /// <summary>The identifier of the Backend taxon.</summary>
    public static readonly Guid BackendTaxonId = SiteInitializer.BackendTemplatesCategoryId;
    /// <summary>The path to NoTemplate image.</summary>
    public const string NoTemplateImagePath = "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.NoTemplate.gif";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.SelectTemplateBaseView" /> class.
    /// </summary>
    public SelectTemplateBaseView() => this.LayoutTemplatePath = SelectTemplateBaseView.TemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the taxonomy manager.</summary>
    /// <value>The taxonomy manager.</value>
    public TaxonomyManager TaxonomyManager
    {
      get => this.taxonomyManager;
      set => this.taxonomyManager = value;
    }

    /// <summary>Gets or sets the page manager.</summary>
    /// <value>The page manager.</value>
    public PageManager PageManager
    {
      get => this.pageManager;
      set => this.pageManager = value;
    }

    /// <summary>Gets or sets the current page object.</summary>
    /// <value>The current page object.</value>
    public PageData CurrentPage
    {
      get => this.currentPage;
      set => this.currentPage = value;
    }

    /// <summary>Gets or sets the current template.</summary>
    /// <value>The current template.</value>
    public PageTemplate CurrentTemplate
    {
      get => this.currentTemplate;
      set => this.currentTemplate = value;
    }

    /// <summary>Gets or sets the master page.</summary>
    /// <value>The master page.</value>
    public string MasterPage
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.MasterPage;
        return this.CurrentTemplate != null ? this.CurrentTemplate.MasterPage : (string) null;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.MasterPage = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.MasterPage = value;
        }
      }
    }

    /// <summary>Gets or sets the template.</summary>
    /// <value>The template.</value>
    public PageTemplate Template
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.Template;
        return this.CurrentTemplate != null ? this.CurrentTemplate.ParentTemplate : (PageTemplate) null;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.Template = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.SetParentTemplate(value);
          if (value == null)
            return;
          this.CurrentTemplate.Framework = value.Framework;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this page requires SSL.
    /// </summary>
    /// <value><c>true</c> if [require SSL]; otherwise, <c>false</c>.</value>
    public bool RequireSsl
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.RequireSsl;
        return this.CurrentTemplate != null && this.CurrentTemplate.RequireSsl;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.RequireSsl = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.RequireSsl = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the view state of this page is enabled.
    /// The default value is false as opposed to standard ASP.Net page.
    /// </summary>
    /// <value><c>true</c> if [enable view state]; otherwise, <c>false</c>.</value>
    public bool PageEnableViewState
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.EnableViewState;
        return this.CurrentTemplate != null && this.CurrentTemplate.EnableViewState;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.EnableViewState = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.EnableViewState = value;
        }
      }
    }

    /// <summary>
    /// Indicates that ASP.NET should verify message authentication codes (MAC) in the page's view state when the page is
    /// posted back from the client. true if view state should be MAC checked; otherwise, false. The default is true.
    /// </summary>
    /// <value><c>true</c> if [enable view state MAC]; otherwise, <c>false</c>.</value>
    public bool EnableViewStateMac
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.EnableViewStateMac;
        return this.CurrentTemplate != null && this.CurrentTemplate.EnableViewStateMac;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.EnableViewStateMac = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.EnableViewStateMac = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to cache the rendered output of this page.
    /// </summary>
    /// <value><c>true</c> if [cache output]; otherwise, <c>false</c>.</value>
    public bool CacheOutput
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.CacheOutput;
        return this.CurrentTemplate != null && this.CurrentTemplate.CacheOutput;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.CacheOutput = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.CacheOutput = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the duration of the cached output is calculated from the last request.
    /// If the value is false the cache will expire at absolute time intervals.
    /// </summary>
    /// <value><c>true</c> if [sliding expiration]; otherwise, <c>false</c>.</value>
    public bool SlidingExpiration
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.SlidingExpiration;
        return this.CurrentTemplate != null && this.CurrentTemplate.SlidingExpiration;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.SlidingExpiration = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.SlidingExpiration = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets the time, in seconds, that the page is cached.
    /// </summary>
    /// <value>The duration of the cache.</value>
    public int CacheDuration
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.CacheDuration;
        return this.CurrentTemplate != null ? this.CurrentTemplate.CacheDuration : 0;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.CacheDuration = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.CacheDuration = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether validation of events in postback and callback scenarios is enabled.
    /// true if events are being validated; otherwise, false. The default is true.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [enable event validation]; otherwise, <c>false</c>.
    /// </value>
    public bool EnableEventValidation
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.EnableEventValidation;
        return this.CurrentTemplate != null && this.CurrentTemplate.EnableEventValidation;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.EnableEventValidation = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.EnableEventValidation = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether session state is enabled for this page.
    /// The default value is true.
    /// </summary>
    /// <value><c>true</c> if [enable session state]; otherwise, <c>false</c>.</value>
    public bool EnableSessionState
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.EnableSessionState;
        return this.CurrentTemplate != null && this.CurrentTemplate.EnableSessionState;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.EnableSessionState = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.EnableSessionState = value;
        }
      }
    }

    /// <summary>
    /// Indicates whether themes are used on the page. true if themes are used; otherwise, false. The default is true.
    /// </summary>
    /// <value><c>true</c> if themes are enabled; otherwise, <c>false</c>.</value>
    public bool PageEnableTheming
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.EnableTheming;
        return this.CurrentTemplate != null && this.CurrentTemplate.EnableTheming;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.EnableTheming = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.EnableTheming = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a target URL for redirection if an unhandled page exception occurs.
    /// </summary>
    /// <value>The error page.</value>
    public string ErrorPage
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.ErrorPage;
        return this.CurrentTemplate != null ? this.CurrentTemplate.ErrorPage : (string) null;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.ErrorPage = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.ErrorPage = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether request validation should occur.
    /// If true, request validation checks all input data against a hard-coded list of potentially dangerous values.
    /// If a match occurs, an HttpRequestValidationException exception is thrown. The default is true.
    /// </summary>
    /// <value><c>true</c> if [validate request]; otherwise, <c>false</c>.</value>
    public bool ValidateRequest
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.ValidateRequest;
        return this.CurrentTemplate != null && this.CurrentTemplate.ValidateRequest;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.ValidateRequest = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.ValidateRequest = value;
        }
      }
    }

    /// <summary>Gets or sets the view state encryption.</summary>
    /// <value>The view state encryption.</value>
    public ViewStateEncryptionMode ViewStateEncryption
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.ViewStateEncryption;
        return this.CurrentTemplate != null ? this.CurrentTemplate.ViewStateEncryption : ViewStateEncryptionMode.Auto;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.ViewStateEncryption = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.ViewStateEncryption = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets the culture setting for the page.
    /// The value of this attribute must be a valid culture ID.
    /// </summary>
    /// <value>The culture.</value>
    public string Culture
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.Culture;
        return this.CurrentTemplate != null ? this.CurrentTemplate.Culture : (string) null;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.Culture = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.Culture = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets the user interface (UI) culture setting to use for the page.
    /// Supports any valid UI culture value.
    /// </summary>
    /// <value>The UI culture.</value>
    public string UiCulture
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.Culture;
        return this.CurrentTemplate != null ? this.CurrentTemplate.UiCulture : (string) null;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.Culture = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.UiCulture = value;
        }
      }
    }

    /// <summary>
    ///  the name of the encoding scheme used for the HTTP response that contains a page's content.
    ///  The value assigned to this attribute must be a valid encoding name.
    ///  For a list of possible encoding names, see the <see cref="T:System.Text.Encoding" /> class.
    ///  You can also call the GetEncodings method for a list of possible encoding names and IDs.
    /// </summary>
    /// <value>The response encoding.</value>
    public string ResponseEncoding
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.ResponseEncoding;
        return this.CurrentTemplate != null ? this.CurrentTemplate.ResponseEncoding : (string) null;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.ResponseEncoding = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.ResponseEncoding = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether HTTP response buffering is enabled. true if page buffering is enabled; otherwise, false.
    /// The default is true.
    /// </summary>
    /// <value><c>true</c> if [buffer output]; otherwise, <c>false</c>.</value>
    public bool BufferOutput
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.BufferOutput;
        return this.CurrentTemplate != null && this.CurrentTemplate.BufferOutput;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.BufferOutput = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.BufferOutput = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to return the user to the same position in the client browser after postback.
    /// true if users should be returned to the same position; otherwise, false. The default is false.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [maintain scroll position on postback]; otherwise, <c>false</c>.
    /// </value>
    public bool MaintainScrollPositionOnPostback
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.MaintainScrollPositionOnPostback;
        return this.CurrentTemplate != null && this.CurrentTemplate.MaintainScrollPositionOnPostback;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.MaintainScrollPositionOnPostback = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.MaintainScrollPositionOnPostback = value;
        }
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically include script manager on the page.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [include script manager]; otherwise, <c>false</c>.
    /// </value>
    public bool IncludeScriptManager
    {
      get
      {
        if (this.CurrentPage != null)
          return this.CurrentPage.IncludeScriptManager;
        return this.CurrentTemplate != null && this.CurrentTemplate.IncludeScriptManager;
      }
      set
      {
        if (this.CurrentPage != null)
        {
          this.CurrentPage.IncludeScriptManager = value;
        }
        else
        {
          if (this.CurrentTemplate == null)
            return;
          this.CurrentTemplate.IncludeScriptManager = value;
        }
      }
    }

    /// <summary>Gets or sets the mode of the Select Template dialog.</summary>
    /// <value>The mode.</value>
    public DialogModes Mode
    {
      get => this.mode;
      set => this.mode = value;
    }

    /// <summary>Occurs when returnToProperties button is clicked.</summary>
    public event CommandEventHandler ReturnToPropertiesCommand
    {
      add => this.Events.AddHandler(SelectTemplateBaseView.EventReturnToPropertiesCommand, (Delegate) value);
      remove => this.Events.RemoveHandler(SelectTemplateBaseView.EventReturnToPropertiesCommand, (Delegate) value);
    }

    /// <summary>
    /// Gets or sets the taxon used as root for page navigation.
    /// </summary>
    /// <value>The root taxon.</value>
    public RootTaxonType RootTaxon
    {
      get => this.rootTaxon;
      set => this.rootTaxon = value;
    }

    private ScriptManager CurrentScriptManager
    {
      get
      {
        if (this.scriptManager == null)
          this.scriptManager = ScriptManager.GetCurrent(this.Page);
        return this.scriptManager;
      }
    }

    /// <summary>Gets the returnToProperties button.</summary>
    /// <value>The returnToProperties button.</value>
    protected virtual IButtonControl ReturnToProperties => this.Container.GetControl<IButtonControl>("returnToProperties", true);

    /// <summary>Gets the taxonomiesRepeater repeater.</summary>
    /// <value>The taxonomiesRepeater repeater.</value>
    protected virtual Repeater TaxonomiesRepeater => this.Container.GetControl<Repeater>("taxonomiesRepeater", true);

    /// <summary>Gets the viewTitle control.</summary>
    /// <value>The viewTitle control.</value>
    protected virtual ITextControl ViewTitle => this.Container.GetControl<ITextControl>("viewTitle", true);

    /// <summary>Gets the masterPageFile control.</summary>
    /// <value>The masterPageFile control.</value>
    protected virtual HiddenField MasterPageFile => this.Container.GetControl<HiddenField>("masterPageFile", true);

    /// <summary>Gets the button control.</summary>
    /// <value>The button control.</value>
    protected virtual IButtonControl PostBackButton => this.Container.GetControl<IButtonControl>("postBackButton", true);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      string format = "var masterPageFileClientID = '{0}';" + "var postBackBtnUniqueID = '{1}';";
      ScriptManager.RegisterClientScriptBlock((Control) this, this.GetType(), "declareGlobalVars", string.Format(format, (object) this.MasterPageFile.ClientID, (object) ((Control) this.PostBackButton).UniqueID), true);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      string taxonomyName = Config.Get<PagesConfig>().PageTemplatesTaxonomyName;
      HierarchicalTaxonomy hierarchicalTaxonomy = this.TaxonomyManager.GetTaxonomies<HierarchicalTaxonomy>().Single<HierarchicalTaxonomy>((Expression<Func<HierarchicalTaxonomy, bool>>) (t => t.Name == taxonomyName));
      List<Taxon> list;
      if (this.RootTaxon == RootTaxonType.Backend)
      {
        list = hierarchicalTaxonomy.Taxa.Where<Taxon>((Func<Taxon, bool>) (t => t.Id == SelectTemplateBaseView.BackendTaxonId)).ToList<Taxon>();
      }
      else
      {
        list = hierarchicalTaxonomy.Taxa.Where<Taxon>((Func<Taxon, bool>) (t => t.Id != SelectTemplateBaseView.BackendTaxonId)).OrderBy<Taxon, float>((Func<Taxon, float>) (t => t.Ordinal)).ToList<Taxon>();
        int index = list.FindIndex((Predicate<Taxon>) (x => x.Id == SiteInitializer.BasicTemplatesCategoryId));
        if (index >= 0)
        {
          Taxon taxon = list[index];
          list.RemoveAt(index);
          list.Add(taxon);
        }
      }
      this.TaxonomiesRepeater.ItemCreated += new RepeaterItemEventHandler(this.TaxonomiesRepeater_ItemCreated);
      this.TaxonomiesRepeater.DataSource = (object) list;
      this.TaxonomiesRepeater.DataBind();
      if (this.CurrentScriptManager != null)
        this.CurrentScriptManager.RegisterAsyncPostBackControl((Control) this.ReturnToProperties);
      this.ReturnToProperties.Command += new CommandEventHandler(this.ReturnToProperties_Command);
      this.PostBackButton.Click += new EventHandler(this.PostBackButton_Click);
      if (this.CurrentScriptManager == null)
        return;
      this.CurrentScriptManager.RegisterPostBackControl((Control) this.PostBackButton);
    }

    /// <summary>
    /// Raises the <see cref="E:ReturnToPropertiesCommand" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
    protected virtual void OnReturnToPropertiesCommand(CommandEventArgs e)
    {
      CommandEventHandler commandEventHandler = (CommandEventHandler) this.Events[SelectTemplateBaseView.EventReturnToPropertiesCommand];
      if (commandEventHandler == null)
        return;
      commandEventHandler((object) this, e);
    }

    private void RegisterRedirectScript()
    {
      string script = string.Format("closeDialog('{0}', true);", (object) this.GetEditUrl());
      this.Page.ClientScript.RegisterStartupScript(this.GetType(), "closeAndRedirect", script, true);
    }

    /// <summary>Gets the edit URL.</summary>
    /// <returns></returns>
    protected virtual string GetEditUrl() => string.Empty;

    /// <summary>Gets the templates by category.</summary>
    /// <param name="category">The category.</param>
    /// <param name="templateId">The currently created/edited template pageId.</param>
    /// <returns></returns>
    protected abstract List<PageTemplate> GetTemplates(
      Guid category,
      Guid templateId);

    private PageTemplate CreateTemplate(string masterPage)
    {
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
        TransactionManager.CommitTransaction(this.PageManager.TransactionName);
      }
      else
        template = this.PageManager.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.MasterPage == masterPage));
      return template;
    }

    private void SetEmptyTemplateControl(RepeaterItemEventArgs eventArgs)
    {
      System.Web.UI.WebControls.Image control1 = (System.Web.UI.WebControls.Image) eventArgs.Item.FindControl("noTemplScreenshot");
      if (control1 != null)
        control1.ImageUrl = ControlUtilities.ResolveResourceUrl("Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.NoTemplate.gif", this.Page);
      IButtonControl control2 = (IButtonControl) eventArgs.Item.FindControl("select");
      if (control2 == null)
        return;
      HtmlControl control3 = (HtmlControl) eventArgs.Item.FindControl("templItem");
      if (control3 != null)
        control3.Visible = true;
      if ((this.Mode != DialogModes.ChangeTemplate || this.Template != null ? 0 : (string.IsNullOrEmpty(this.MasterPage) ? 1 : 0)) != 0)
        this.SetAsSelectedTemplateControl(eventArgs, control3);
      this.RegisterForPostBack(control2, Guid.Empty.ToString());
    }

    private void SetAsSelectedTemplateControl(RepeaterItemEventArgs eventArgs, HtmlControl li)
    {
      ITextControl control = (ITextControl) eventArgs.Item.FindControl("selected");
      if (control != null)
        ((Control) control).Visible = true;
      li?.Attributes.Add("class", "sfSel sfSimpleTemplate");
    }

    private void RegisterForPostBack(IButtonControl select, string commandArgument)
    {
      select.Command += new CommandEventHandler(this.Select_Command);
      select.CommandArgument = commandArgument;
      if (this.CurrentScriptManager == null)
        return;
      this.CurrentScriptManager.RegisterPostBackControl((Control) select);
    }

    private void SetTemplatePagesCount(RepeaterItemEventArgs e, PageTemplate template)
    {
      HtmlAnchor control = (HtmlAnchor) e.Item.FindControl("templatePagesLink");
      if (control == null)
        return;
      int num = this.PageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Template.Id == template.Id)).Count<PageData>();
      if (num > 0)
      {
        string str = "openTemplatePagesDialog('" + RouteHelper.ResolveUrl("~/Sitefinity/Dialog/TemplatePagesDialog" + "?rootTaxon=" + (object) this.RootTaxon + "&id=" + (object) template.Id, UrlResolveOptions.Rooted) + "');";
        control.Attributes.Add("onclick", str);
      }
      if (num == 1)
        control.InnerHtml = string.Format(Res.Get<PageResources>().PageCount, (object) num);
      else
        control.InnerHtml = string.Format(Res.Get<PageResources>().PagesCount, (object) num);
    }

    /// <summary>
    /// Handles the Command event of the ReturnToProperties control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> instance containing the event data.</param>
    private void ReturnToProperties_Command(object sender, CommandEventArgs e)
    {
      if (this.Mode == DialogModes.ChangeTemplate)
      {
        ScriptManager.RegisterStartupScript((Control) this.ReturnToProperties, this.GetType(), "close", "closeDialog('reload');", true);
      }
      else
      {
        if (this.Mode != DialogModes.SelectTemplate)
          return;
        this.OnReturnToPropertiesCommand(e);
      }
    }

    private string GetTemplateCategorySubtitle(PageTemplateFramework framework)
    {
      if (framework == PageTemplateFramework.Mvc)
        return Res.Get<PageResources>().ForMvcWidgetsLabel;
      return framework == PageTemplateFramework.WebForms ? Res.Get<PageResources>().ForWebformsWidgetsLabel : Res.Get<PageResources>().ForAllWidgetsLabel;
    }

    private void TaxonomiesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      HierarchicalTaxon dataItem = (HierarchicalTaxon) e.Item.DataItem;
      if (dataItem == null)
        return;
      Guid id = dataItem.Id;
      Guid templateId = Guid.Empty;
      if (this.CurrentTemplate != null)
        templateId = this.CurrentTemplate.Id;
      List<PageTemplate> templates = this.GetTemplates(id, templateId);
      Repeater control1 = (Repeater) e.Item.FindControl("templatesRepeater");
      HtmlControl control2 = (HtmlControl) e.Item.FindControl("templatesWrapper");
      if (dataItem.Id == SelectTemplateBaseView.BasicTemplatesCategoryId && templates.Count > 0)
      {
        this.SetEmptyTemplateControl(e);
        if (control2 != null)
          control2.Visible = true;
      }
      if (templates.Count <= 0 || control2 == null)
        return;
      control2.Visible = true;
      ITextControl control3 = (ITextControl) e.Item.FindControl("taxonomyName");
      if (control3 != null)
        control3.Text = (string) dataItem.Title;
      ITextControl control4 = (ITextControl) e.Item.FindControl("taxonomySubtitle");
      if (dataItem.Id != SiteInitializer.CustomTemplatesCategoryId && control4 != null)
      {
        string categorySubtitle = this.GetTemplateCategorySubtitle(templates.First<PageTemplate>().Framework);
        control4.Text = categorySubtitle;
      }
      if (control1 == null)
        return;
      control1.ItemCreated += new RepeaterItemEventHandler(this.TemplatesRepeater_ItemCreated);
      control1.DataSource = (object) templates;
      control1.DataBind();
    }

    private void TemplatesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      PageTemplate dataItem = (PageTemplate) e.Item.DataItem;
      if (dataItem == null)
        return;
      System.Web.UI.WebControls.Image control1 = (System.Web.UI.WebControls.Image) e.Item.FindControl("screenshot");
      if (control1 != null)
        control1.ImageUrl = dataItem.GetBigThumbnailUrl((Telerik.Sitefinity.Libraries.Model.Image) null, this.Page);
      ITextControl control2 = (ITextControl) e.Item.FindControl("templateName");
      if (control2 != null)
        control2.Text = (string) dataItem.Title;
      IButtonControl control3 = (IButtonControl) e.Item.FindControl("select");
      if (control3 != null)
      {
        Guid guid = Guid.Empty;
        if (this.Template != null)
          guid = this.Template.Id;
        if (dataItem.Id == guid)
          this.SetAsSelectedTemplateControl(e, (HtmlControl) e.Item.FindControl("templItem"));
        this.RegisterForPostBack(control3, dataItem.Id.ToString());
      }
      this.SetTemplatePagesCount(e, dataItem);
    }

    private void Select_Command(object sender, CommandEventArgs e)
    {
      if (Utility.IsGuid(e.CommandArgument.ToString()))
      {
        bool flag = false;
        Guid id = new Guid(e.CommandArgument.ToString());
        if (id != Guid.Empty)
        {
          PageTemplate template = this.PageManager.GetTemplate(id);
          if (template != null)
          {
            this.Template = template;
            this.MasterPage = string.IsNullOrEmpty(template.MasterPage) ? string.Empty : template.MasterPage;
            if (this.Mode == DialogModes.SelectTemplate)
            {
              this.BufferOutput = template.BufferOutput;
              this.CacheDuration = template.CacheDuration;
              this.CacheOutput = template.CacheOutput;
              this.Culture = template.Culture;
              this.EnableEventValidation = template.EnableEventValidation;
              this.EnableSessionState = template.EnableSessionState;
              this.PageEnableTheming = template.EnableTheming;
              this.PageEnableViewState = template.EnableViewState;
              this.EnableViewStateMac = template.EnableViewStateMac;
              this.ErrorPage = template.ErrorPage;
              this.MaintainScrollPositionOnPostback = template.MaintainScrollPositionOnPostback;
              this.RequireSsl = template.RequireSsl;
              this.ResponseEncoding = template.ResponseEncoding;
              this.SlidingExpiration = template.SlidingExpiration;
              this.UiCulture = template.UiCulture;
              this.ValidateRequest = template.ValidateRequest;
              this.ViewStateEncryption = template.ViewStateEncryption;
              if (template.IncludeScriptManager)
                this.IncludeScriptManager = true;
            }
            this.IncludeScriptManager = template.IncludeScriptManager;
            flag = true;
          }
        }
        else if (this.Template != null)
        {
          this.Template = (PageTemplate) null;
          flag = true;
        }
        if (flag)
        {
          if (this.PageManager.TransactionName.IsNullOrEmpty())
            this.PageManager.SaveChanges();
          else
            TransactionManager.CommitTransaction(this.PageManager.TransactionName);
        }
      }
      this.RegisterRedirectScript();
    }

    private void PostBackButton_Click(object sender, EventArgs e)
    {
      string appRelative = VirtualPathUtility.ToAppRelative(this.MasterPageFile.Value.Split(new string[1]
      {
        "***"
      }, StringSplitOptions.RemoveEmptyEntries)[0]);
      if (this.CurrentTemplate == null)
      {
        this.Template = this.CreateTemplate(appRelative);
      }
      else
      {
        this.MasterPage = appRelative;
        this.Template = (PageTemplate) null;
      }
      TransactionManager.CommitTransaction(this.PageManager.TransactionName);
      this.RegisterRedirectScript();
    }
  }
}
