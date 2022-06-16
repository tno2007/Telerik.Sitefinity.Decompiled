// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ZoneEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.DesignerToolbox;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.Services.Model;
using Telerik.Sitefinity.Modules.Forms.Web.UI;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Data;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Personalization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Adaptors;
using Telerik.Sitefinity.Web.UI.Components;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Dock;

namespace Telerik.Sitefinity.Web.UI
{
  /// 1. RadWebControl - Define a client type name for the control
  ///             <summary>
  /// Represents a control for editing controls on a page or a form.
  /// </summary>
  [EmbeddedSkin("ZoneEditor")]
  [ClientScriptResource("Telerik.Sitefinity.Web.UI.ZoneEditor", "Telerik.Sitefinity.Web.Scripts.ZoneEditorScripts.js")]
  [ClientScriptResource("Telerik.Sitefinity.Web.UI.ZoneEditor", "Telerik.Sitefinity.Web.Scripts.ClientManager.js")]
  public class ZoneEditor : RadWebControl
  {
    private IList<RadDockZone> _wrapperDockingZones = (IList<RadDockZone>) new List<RadDockZone>();
    private IList<RadDockZone> _toolboxDockingZones = (IList<RadDockZone>) new List<RadDockZone>();
    private readonly string controlPersonalizationsService = "~/RestApi/Sitefinity/personalizations/controls";
    private const string PersonalizationDropDownCommand = "personalizationDropDownCommand";
    private Dictionary<string, string> _controlCommands;
    private Dictionary<string, string> _localization;
    private IToolbox _controlToolbox;
    private IToolbox _layoutToolbox;
    private IList<ControlData> _pageControls;
    private IList<ControlProperty> controlProps;
    private IList<ControlData> _placeholders;
    protected Control ControlToolboxContainer;
    protected Control LayoutToolboxContainer;
    protected Control ThemesToolboxContainer;
    protected RadContextMenu PersonalizationMenu;
    protected RadContextMenu RadContextMenu1;
    protected RadWindowManager RadWindowManager1;
    protected RadTabStrip RadTabStrip1;
    protected RadMultiPage RadMultiPage1;
    protected RadDock ControlWrapperFactory;
    protected RadDock LayoutWrapperFactory;
    protected List<Control> _placeholderControls = new List<Control>();
    protected List<ControlData> insertedControls = new List<ControlData>();
    protected List<ControlData> orphanedLayoutControls = new List<ControlData>();
    protected Control orphanedControlsPlaceholder;
    protected Control orphanedLayoutsDockZone;
    private Dictionary<Guid, Control> controlIds = new Dictionary<Guid, Control>();
    private Dictionary<Control, ControlData> controlDataObjects = new Dictionary<Control, ControlData>();
    private Dictionary<Guid, Guid> misplacedLayoutControls = new Dictionary<Guid, Guid>();
    private Dictionary<Control, Control> emptyControls = new Dictionary<Control, Control>();
    private Dictionary<string, List<ControlData>> insertedEmptySiblingControls = new Dictionary<string, List<ControlData>>();
    private PageDraft cachedPageDraft;
    private TemplateDraft cachedTemplateDraft;
    private FormDraft cachedFormDraft;
    private LayoutEditor layoutEditor;
    private ThemesEditor themesEditor;
    private FormSettingsEditor settingsEditor;
    private static readonly object ZoneEditorLayoutEvent = new object();
    private static readonly object ZoneEditorEvent = new object();
    private HtmlContainerControl layoutTab;
    private HtmlGenericControl controlsTab;
    private HtmlGenericControl themesTab;
    private Control layoutLinks;
    private Control widgetLinks;
    private Control themesLinks;
    private Control settingsLinks;
    private PageTemplateField templateControl;
    private HtmlGenericControl settingsTab;
    private JsonDictionary<LayoutControlDataPermissions> placeholderPermissions = new JsonDictionary<LayoutControlDataPermissions>();

    /// <summary>
    /// Gets or sets the value, indicating whether to render links to the embedded skins or not.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If EnableEmbeddedSkins is set to false you will have to register the needed CSS files by hand.
    /// </remarks>
    [DefaultValue(false)]
    public override bool EnableEmbeddedSkins => false;

    /// <summary>
    /// Gets or sets a client function that will be called just before the response ends.
    /// </summary>
    /// <value>The on client response ending.</value>
    [DefaultValue("")]
    public virtual string OnClientResponseEnding
    {
      get => (string) this.ViewState[nameof (OnClientResponseEnding)] ?? string.Empty;
      set => this.ViewState[nameof (OnClientResponseEnding)] = (object) value;
    }

    /// <summary>
    /// Checks if the current user is granted permission to add controls to the page.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <returns>True if the current user is granted create permission, false otherwise</returns>
    public static bool CanUserCreateControls(PageNode page) => page != null ? page.IsGranted("Pages", "CreateChildControls") : throw new ArgumentNullException(nameof (page));

    /// <summary>
    /// Checks if the current user is granted permission to add controls to the template
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns>True if the current user is granted modify permission, false otherwise</returns>
    public static bool CanUserCreateControls(PageTemplate template) => template != null ? template.IsGranted("PageTemplates", "Modify") : throw new ArgumentNullException(nameof (template));

    /// <summary>
    /// Determines whether this instance [can user create controls] the specified form.
    /// </summary>
    /// <param name="form">The form.</param>
    /// <returns>
    ///     <c>true</c> if this instance [can user create controls] the specified form; otherwise, <c>false</c>.
    /// </returns>
    public static bool CanUserCreateControls(FormDescription form) => form != null ? form.IsGranted("Forms", "Modify") : throw new ArgumentNullException(nameof (form));

    /// <summary>
    /// Gets or sets a client function that will be called when a command is invoked.
    /// </summary>
    /// <value>The on client command.</value>
    [DefaultValue("")]
    public virtual string OnClientCommand
    {
      get => (string) this.ViewState[nameof (OnClientCommand)] ?? string.Empty;
      set => this.ViewState[nameof (OnClientCommand)] = (object) value;
    }

    /// <summary>Gets the control commands list.</summary>
    /// <value>The commands.</value>
    public Dictionary<string, string> Commands
    {
      get
      {
        if (this._controlCommands == null)
        {
          this._controlCommands = new Dictionary<string, string>();
          this._controlCommands.Add("addPersonalizedVersion,sfPersonalizeItm", Res.Get<PageResources>("ZoneEditorAddPersonalizedVersion", Res.CurrentBackendCulture));
          this._controlCommands.Add("removePersonalizedVersion,sfRemPersonalizedItm sfSeparatorDown", Res.Get<PageResources>("ZoneEditorRemovePersonalizedVersion", Res.CurrentBackendCulture));
          this._controlCommands.Add("displayWidgetOverrideText,sfDisplayText", Res.Get<PageResources>("ZoneEditorEnablePageOverrideDisplayContenxtMenuInfo", Res.CurrentBackendCulture));
          this._controlCommands.Add("beforedelete,sfDeleteItm", Res.Get<Labels>("Delete", Res.CurrentBackendCulture));
          this._controlCommands.Add("duplicate,sfDuplicateItm", Res.Get<Labels>("Duplicate", Res.CurrentBackendCulture));
          this._controlCommands.Add("widgetOverride,sfWidgetOverrideItm", Res.Get<PageResources>("ZoneEditorEnablePageOverride", Res.CurrentBackendCulture));
          this._controlCommands.Add("widgetDisableOverride,sfWidgetOverrideItm", Res.Get<PageResources>("ZoneEditorDisablePageOverride", Res.CurrentBackendCulture));
          this._controlCommands.Add("rollback,sfDisableWidgetOverrideItm", Res.Get<PageResources>("ZoneEditorRollback", Res.CurrentBackendCulture));
          if (LicenseState.Current.LicenseInfo.IsPageControlsPermissionsEnabled)
            this._controlCommands.Add("permissions,sfPermItm", Res.Get<Labels>("Permissions", Res.CurrentBackendCulture));
        }
        return this._controlCommands;
      }
    }

    /// <summary>Gets the localization.</summary>
    /// <value>The localization.</value>
    public Dictionary<string, string> Localization
    {
      get
      {
        if (this._localization == null)
        {
          this._localization = new Dictionary<string, string>();
          this._localization.Add("ZoneEditorConfirmDeleteControl", Res.Get<PageResources>("ZoneEditorConfirmDeleteControl", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorConfirmDeleteLayout", Res.Get<PageResources>("ZoneEditorConfirmDeleteLayout", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorEditCommand", Res.Get<PageResources>("ZoneEditorEditCommand", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorMoreCommand", Res.Get<PageResources>("ZoneEditorMoreCommand", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorPermissionsCommand", Res.Get<PageResources>("ZoneEditorPermissionsCommand", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorDeleteCommand", Res.Get<PageResources>("ZoneEditorDeleteCommand", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorDuplicationCommand", Res.Get<PageResources>("ZoneEditorDuplicationCommand", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorLayouts", Res.Get<PageResources>("ZoneEditorLayouts", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorControls", Res.Get<PageResources>("ZoneEditorControls", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorThemes", Res.Get<PageResources>("ZoneEditorThemes", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorSettings", Res.Get<PageResources>("Settings", Res.CurrentBackendCulture));
          if (SystemManager.IsModuleEnabled("Newsletters"))
            this._localization.Add("ZoneEditorPlainText", Res.Get<NewslettersResources>("PlainText", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorEmptyZoneContentDragMessage", Res.Get<PageResources>("ZoneEditorEmptyZoneContentDragMessage", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorEmptyZoneLayoutCaption", Res.Get<PageResources>("ZoneEditorEmptyZoneLayoutCaption", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorEmptyZoneLayoutDragMessage", Res.Get<PageResources>("ZoneEditorEmptyZoneLayoutDragMessage", Res.CurrentBackendCulture));
          this._localization.Add("ZoneEditorEmptyZoneDraggingText", Res.Get<PageResources>("ZoneEditorEmptyZoneDraggingText", Res.CurrentBackendCulture));
        }
        return this._localization;
      }
    }

    internal static List<WidgetMenuItem> GetWidgetSegments(ControlData controlData)
    {
      List<WidgetMenuItem> widgetSegments = new List<WidgetMenuItem>();
      if (controlData.IsPersonalized)
      {
        IPersonalizationService personalizationService = SystemManager.GetPersonalizationService();
        if (personalizationService != null)
        {
          IEnumerable<ISegment> segments = personalizationService.GetAllSegments().Where<ISegment>((Func<ISegment, bool>) (s => controlData.PersonalizedSegmentIds.Contains<Guid>(s.Id)));
          IList<ControlData> personalizedControls = controlData.PersonalizedControls;
          foreach (ISegment segment1 in segments)
          {
            ISegment segment = segment1;
            ControlData controlData1 = personalizedControls.Single<ControlData>((Func<ControlData, bool>) (c => c.PersonalizationSegmentId == segment.Id));
            widgetSegments.Add(new WidgetMenuItem()
            {
              Text = segment.Name,
              CommandName = controlData1.Id.ToString(),
              CssClass = "sfPersonalizedFor"
            });
          }
        }
      }
      string str = controlData.BaseControlId != Guid.Empty ? controlData.BaseControlId.ToString() : controlData.Id.ToString();
      widgetSegments.Add(new WidgetMenuItem()
      {
        Text = Res.Get<PageResources>().NotSpecified,
        CommandName = str,
        CssClass = "sfPersonaNotSpecified"
      });
      return widgetSegments;
    }

    internal static Type GetControlType(ControlData controlData)
    {
      Type c = TypeResolutionService.ResolveType(controlData.ObjectType, false);
      if (c != (Type) null && typeof (MvcProxyBase).IsAssignableFrom(c))
      {
        string name = controlData.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && p.Value != null)).Select<ControlProperty, string>((Func<ControlProperty, string>) (p => p.Value)).FirstOrDefault<string>();
        if (!name.IsNullOrEmpty())
          c = TypeResolutionService.ResolveType(name, false);
      }
      return c;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    protected override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      EventHub.Raise((IEvent) new ScriptsRegisteringEvent()
      {
        Sender = (IScriptControl) this,
        Scripts = (IList<ScriptReference>) scriptReferences
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>Registers the CSS references</summary>
    protected override void RegisterCssReferences()
    {
      ClientScriptManager clientScript = this.Page.ClientScript;
      if (this.MediaType != DesignMediaType.Form)
        return;
      string webResourceUrl = clientScript.GetWebResourceUrl(TypeResolutionService.ResolveType("Telerik.Sitefinity.Resources.Reference, Telerik.Sitefinity.Resources"), "Telerik.Sitefinity.Resources.Themes.Light.Styles.FormsPreview.css");
      HtmlLink child = new HtmlLink();
      child.Href = webResourceUrl;
      child.Attributes.Add("type", "text/css");
      child.Attributes.Add("rel", "stylesheet");
      child.Attributes.Add("class", "SitefinityForms_stylesheet");
      this.Page.Header.Controls.Add((Control) child);
    }

    /// <summary>Set client-side properties.</summary>
    /// <param name="descriptor">The component descriptor.</param>
    protected override void DescribeComponent(IScriptDescriptor descriptor)
    {
      base.DescribeComponent(descriptor);
      if (!this.Enabled)
      {
        descriptor.AddProperty("_enabled", (object) false);
      }
      else
      {
        if (this.OnClientCommand != string.Empty)
          descriptor.AddEvent("command", this.OnClientCommand);
        if (this.OnClientResponseEnding != string.Empty)
          descriptor.AddEvent("responseEnding", this.OnClientResponseEnding);
        if (this.RadContextMenu1 != null)
          descriptor.AddProperty("_commandMenuId", (object) this.RadContextMenu1.ClientID);
        if (this.PersonalizationMenu != null)
          descriptor.AddProperty("_personalizationMenuId", (object) this.PersonalizationMenu.ClientID);
        if (this.RadWindowManager1 != null)
          descriptor.AddProperty("_windowMangerId", (object) this.RadWindowManager1.ClientID);
        if (this.RadTabStrip1 != null)
          descriptor.AddProperty("_tabStripId", (object) this.RadTabStrip1.ClientID);
        if (this.LayoutEditor != null)
          descriptor.AddProperty("_layoutEditorId", (object) this.LayoutEditor.ClientID);
        if (this.SettingsEditor != null)
          descriptor.AddProperty("_settingsEditorId", (object) this.SettingsEditor.ClientID);
        descriptor.AddComponentProperty("controlWrapperFactory", this.ControlWrapperFactory.ClientID);
        descriptor.AddComponentProperty("layoutWrapperFactory", this.LayoutWrapperFactory.ClientID);
        string str = this.WebServiceUrl;
        if (str.StartsWith("~/"))
          str = str.Replace("~/", VirtualPathUtility.AppendTrailingSlash(SystemManager.CurrentHttpContext.Request.ApplicationPath));
        descriptor.AddProperty("_webServiceUrl", (object) str);
        descriptor.AddProperty("_controlWebMethodName", (object) this.ControlWebMethodName);
        descriptor.AddProperty("_layoutWebMethodName", (object) this.LayoutWebMethodName);
        descriptor.AddProperty("_controlPersonalizationsService", (object) VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute(this.controlPersonalizationsService)));
        descriptor.AddProperty("_personalizationDropDownCommand", (object) "personalizationDropDownCommand");
        descriptor.AddProperty("_propertyEditorUrl", (object) this.Page.ResolveUrl(this.PropertyEditorUrl));
        descriptor.AddProperty("_segmentSelectorUrl", (object) this.Page.ResolveUrl(this.SegmentSelectorUrl));
        descriptor.AddProperty("_PermissionsUrl", (object) RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ModulePermissionsDialog", UrlResolveOptions.Rooted));
        descriptor.AddProperty("_OverridenControlsDialogUrl", (object) RouteHelper.ResolveUrl("~/Sitefinity/Dialog/DisplayOverridenWidgets", UrlResolveOptions.Rooted));
        descriptor.AddProperty("_backToEditorTitle", (object) Res.Get<PageResources>("BackToEditor", Res.CurrentBackendCulture));
        descriptor.AddProperty("_securedObjectType", (object) typeof (ControlData).AssemblyQualifiedName);
        descriptor.AddProperty("_managerClassName", (object) typeof (PageManager).AssemblyQualifiedName);
        descriptor.AddScriptProperty("wrapperDockingZones", this.GetDockZoneIDs(this._wrapperDockingZones));
        descriptor.AddScriptProperty("toolboxDockingZones", this.GetDockZoneIDs(this._toolboxDockingZones));
        string script = new JavaScriptSerializer().Serialize((object) this.Commands);
        descriptor.AddScriptProperty("commands", script);
        descriptor.AddComponentProperty("lockingHandler", this.LockingHandler.ClientID);
        Dictionary<string, string> emptyControlsContentIds = new Dictionary<string, string>();
        this.emptyControls.ToList<KeyValuePair<Control, Control>>().ForEach((Action<KeyValuePair<Control, Control>>) (item => emptyControlsContentIds.Add(item.Key.ClientID, item.Value.ClientID)));
        descriptor.AddProperty("emptyControlIds", (object) emptyControlsContentIds);
        descriptor.AddProperty("_pageId", (object) this.DraftId.ToString());
        descriptor.AddProperty("_pageNodeId", (object) this.PageNodeId.ToString());
        descriptor.AddProperty("_url", (object) this.Page.Request.Url.AbsolutePath);
        descriptor.AddProperty("propertyServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/ControlPropertyService.svc/"));
        string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
        if (!applicationVirtualPath.EndsWith("/"))
          applicationVirtualPath += "/";
        descriptor.AddProperty("_appPath", (object) applicationVirtualPath);
        descriptor.AddProperty("_mediaType", (object) this.MediaType);
        PageManager manager = PageManager.GetManager();
        WcfPageTemplate wcfPageTemplate1 = (WcfPageTemplate) null;
        bool flag1 = false;
        bool flag2 = false;
        bool flag3 = false;
        switch (this.MediaType)
        {
          case DesignMediaType.Page:
            descriptor.AddProperty("_basePageServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/"));
            descriptor.AddProperty("_canCreate", (object) ZoneEditor.CanUserCreateControls(this.PageProvider.GetPageNode(this.PageNodeId)));
            descriptor.AddProperty("_notAuthorizedMessage", (object) Res.Get<SecurityResources>("NotAuthorizedToManageControlsOnThisPage", Res.CurrentBackendCulture));
            PageDraft cachedPageDraft1 = this.CachedPageDraft;
            if (cachedPageDraft1.IsTempDraft)
            {
              PageDraft pageDraft = cachedPageDraft1.ParentPage.Drafts.Where<PageDraft>((Func<PageDraft, bool>) (d => !d.IsTempDraft)).First<PageDraft>();
              if (pageDraft.Version != cachedPageDraft1.Version)
                flag1 = true;
              if (pageDraft.Version == 0)
                flag2 = true;
            }
            wcfPageTemplate1 = (WcfPageTemplate) null;
            bool suppressSecurityChecks = manager.Provider.SuppressSecurityChecks;
            manager.Provider.SuppressSecurityChecks = true;
            WcfPageTemplate wcfPageTemplate2 = !(cachedPageDraft1.TemplateId != Guid.Empty) ? TemplateSelectorDialog.GetNoTemplateItem(this.Page) : new WcfPageTemplate(manager.GetTemplate(cachedPageDraft1.TemplateId));
            manager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
            descriptor.AddProperty("selectedTemplate", (object) wcfPageTemplate2);
            descriptor.AddComponentProperty("templateFieldControl", this.templateControl.ClientID);
            flag3 = cachedPageDraft1.ParentPage.NavigationNode.LocalizationStrategy == LocalizationStrategy.Split || ((IEnumerable<CultureInfo>) cachedPageDraft1.ParentPage.NavigationNode.AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => c.LCID != CultureInfo.InvariantCulture.LCID)).Count<CultureInfo>() <= 1;
            break;
          case DesignMediaType.Template:
            TemplateDraft cachedTemplateDraft = this.CachedTemplateDraft;
            descriptor.AddProperty("_canCreate", (object) ZoneEditor.CanUserCreateControls(cachedTemplateDraft.ParentTemplate));
            descriptor.AddProperty("_notAuthorizedMessage", (object) Res.Get<SecurityResources>().NotAuthorizedToManageControlsOnThisTemplate);
            if (cachedTemplateDraft.IsTempDraft)
            {
              TemplateDraft templateDraft = cachedTemplateDraft.ParentTemplate.Drafts.Where<TemplateDraft>((Func<TemplateDraft, bool>) (d => !d.IsTempDraft)).First<TemplateDraft>();
              if (templateDraft.Version != cachedTemplateDraft.Version)
                flag1 = true;
              if (templateDraft.Version == 0)
                flag2 = true;
            }
            WcfPageTemplate wcfPageTemplate3 = !(cachedTemplateDraft.TemplateId != Guid.Empty) ? TemplateSelectorDialog.GetNoTemplateItem(this.Page) : new WcfPageTemplate(manager.GetTemplate(cachedTemplateDraft.TemplateId));
            descriptor.AddProperty("selectedTemplate", (object) wcfPageTemplate3);
            descriptor.AddProperty("currentTemplateId", (object) cachedTemplateDraft.ParentId);
            descriptor.AddComponentProperty("templateFieldControl", this.templateControl.ClientID);
            flag3 = ((IEnumerable<CultureInfo>) cachedTemplateDraft.ParentTemplate.AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => c.LCID != CultureInfo.InvariantCulture.LCID)).Count<CultureInfo>() <= 1;
            break;
          case DesignMediaType.Form:
            descriptor.AddProperty("_canCreate", (object) ZoneEditor.CanUserCreateControls(this.CachedFormDraft.ParentForm));
            descriptor.AddProperty("_notAuthorizedMessage", (object) Res.Get<SecurityResources>().NotAuthorizedToManageControlsOnThisForm);
            descriptor.AddProperty("_confirmDeleteFieldWithRules", (object) Res.Get<FormsResources>().ZoneEditorConfirmDeleteFieldWithRules);
            descriptor.AddProperty("_formsServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Forms/FormsService.svc"));
            descriptor.AddProperty("_hiddenFieldLabelAttribute", (object) "sf-hidden-field-label");
            FormDraft cachedFormDraft = this.CachedFormDraft;
            if (cachedFormDraft.IsTempDraft)
            {
              FormDraft formDraft = cachedFormDraft.ParentForm.Drafts.Where<FormDraft>((Func<FormDraft, bool>) (d => !d.IsTempDraft)).First<FormDraft>();
              if (formDraft.Version != cachedFormDraft.Version)
                flag1 = true;
              if (formDraft.Version == 0)
                flag2 = true;
            }
            flag3 = ((IEnumerable<CultureInfo>) cachedFormDraft.ParentForm.AvailableCultures).Where<CultureInfo>((Func<CultureInfo, bool>) (c => c.LCID != CultureInfo.InvariantCulture.LCID)).Count<CultureInfo>() <= 1;
            break;
          case DesignMediaType.NewsletterCampaign:
          case DesignMediaType.NewsletterTemplate:
            descriptor.AddProperty("_basePageServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/"));
            descriptor.AddProperty("_canCreate", (object) "true");
            descriptor.AddProperty("_notAuthorizedMessage", (object) Res.Get<SecurityResources>("NotAuthorizedToManageControlsOnThisPage", Res.CurrentBackendCulture));
            PageDraft cachedPageDraft2 = this.CachedPageDraft;
            WcfPageTemplate wcfPageTemplate4 = !(cachedPageDraft2.TemplateId != Guid.Empty) ? TemplateSelectorDialog.GetNoTemplateItem(this.Page) : new WcfPageTemplate(manager.GetTemplate(cachedPageDraft2.TemplateId));
            descriptor.AddProperty("selectedTemplate", (object) wcfPageTemplate4);
            descriptor.AddComponentProperty("templateFieldControl", this.templateControl.ClientID);
            flag3 = true;
            break;
        }
        descriptor.AddProperty("isChangeMade", (object) flag1);
        descriptor.AddProperty("isNewDraft", (object) flag2);
        descriptor.AddProperty("_localization", (object) this.GetJsonLocalization());
        descriptor.AddProperty("hideSaveAllTranslations", (object) flag3);
        if (this.Proxy.Settings.Multilingual)
          descriptor.AddProperty("currentLanguage", (object) this.Proxy.CurrentObjectCulture.Name);
        descriptor.AddProperty("_placeholderPermissions", (object) this.placeholderPermissions.ToJson());
        descriptor.AddProperty("_confirmDiscardOverride", (object) this.Page.FindControl("confirmDiscardOverride"));
        descriptor.AddProperty("_deleteEditedWidgetConfirmationDialog", (object) this.Page.FindControl("deleteEditedWidgetConfirmationDialog"));
        descriptor.AddProperty("_forText", (object) Res.Get<PageResources>().For);
        descriptor.AddProperty("_personalizedForText", (object) Res.Get<PageResources>().PersonalizedFor);
      }
    }

    /// <summary>Get localization messages in JSON</summary>
    /// <returns>localization messages in JSON</returns>
    protected virtual string GetJsonLocalization() => new JavaScriptSerializer().Serialize((object) this.Localization);

    /// <summary>
    /// Gets or sets the Page ID for the first-level editable zones
    /// </summary>
    /// <value>The (root) Page Id.</value>
    public Guid DraftId
    {
      get => this.ViewState["PageId"] == null ? Guid.Empty : (Guid) this.ViewState["PageId"];
      set => this.ViewState["PageId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the ID of the PageNode for the first-level editable zones
    /// </summary>
    public Guid PageNodeId
    {
      get => this.ViewState[nameof (PageNodeId)] == null ? Guid.Empty : (Guid) this.ViewState[nameof (PageNodeId)];
      set => this.ViewState[nameof (PageNodeId)] = (object) value;
    }

    /// <summary>Gets or sets the URL to the control property editor.</summary>
    /// <value>The control property editor URL.</value>
    [UrlProperty]
    public string PropertyEditorUrl
    {
      get => (string) this.ViewState[nameof (PropertyEditorUrl)] ?? string.Empty;
      set => this.ViewState[nameof (PropertyEditorUrl)] = (object) value;
    }

    /// <summary>Gets or sets the URL to the segment selector.</summary>
    /// <value>The segment selector URL.</value>
    [UrlProperty]
    internal string SegmentSelectorUrl
    {
      get => (string) this.ViewState[nameof (SegmentSelectorUrl)] ?? string.Empty;
      set => this.ViewState[nameof (SegmentSelectorUrl)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the tabstrip above the ZoneEditor toolboxes should be visible.
    /// </summary>
    /// <value>The value.</value>
    [DefaultValue(false)]
    public bool ShowTabStrip
    {
      get => (bool) (this.ViewState[nameof (ShowTabStrip)] != null ? this.ViewState[nameof (ShowTabStrip)] : (object) true);
      set => this.ViewState[nameof (ShowTabStrip)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this page represents a template.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is template; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    public DesignMediaType MediaType
    {
      get
      {
        object obj = this.ViewState[nameof (MediaType)];
        return obj == null ? DesignMediaType.Page : (DesignMediaType) obj;
      }
      set => this.ViewState[nameof (MediaType)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the page template framework being used by the template / page.
    /// </summary>
    public PageTemplateFramework Framework { get; set; }

    /// <summary>Gets or sets the Controls toolbox.</summary>
    /// <value>The toolbox.</value>
    public IToolbox ControlToolbox
    {
      get => this._controlToolbox;
      set => this._controlToolbox = value;
    }

    /// <summary>
    /// Gets or sets page data provider to be used (mainly for security)
    /// </summary>
    public PageDataProvider PageProvider { get; set; }

    /// <summary>Gets or sets the metadata provider.</summary>
    /// <value>The metadata provider.</value>
    public FormsDataProvider FormsProvider { get; set; }

    /// <summary>Gets or sets the newsletters module provider.</summary>
    /// <value>The newsletters provider.</value>
    public NewslettersDataProvider NewslettersProvider { get; set; }

    /// <summary>Gets or sets the Layout[Controls] toolbox.</summary>
    /// <value>The toolbox.</value>
    public IToolbox LayoutToolbox
    {
      get => this._layoutToolbox;
      set => this._layoutToolbox = value;
    }

    /// <summary>Gets or sets the page controls.</summary>
    /// <value>List describing the page controls.</value>
    public IList<ControlData> PageControls
    {
      get => this._pageControls;
      set => this._pageControls = value;
    }

    /// <summary>Gets or sets a list of the page placeholders.</summary>
    /// <value>List describing the page placeholders.</value>
    public IList<ControlData> Placeholders
    {
      get => this._placeholders;
      set => this._placeholders = value;
    }

    /// <summary>Gets or sets the web method path.</summary>
    /// <value>The web method path.</value>
    [UrlProperty]
    public string WebServiceUrl
    {
      get => (string) this.ViewState[nameof (WebServiceUrl)] ?? string.Empty;
      set => this.ViewState[nameof (WebServiceUrl)] = (object) value;
    }

    /// <summary>Gets or sets the locking handler.</summary>
    /// <value>The locking handler.</value>
    public LockingHandler LockingHandler { get; set; }

    /// <summary>
    /// Gets or sets the web method name which handles control-related actions (new, delete, duplicate, etc)
    /// </summary>
    /// <value>The web method name.</value>
    public string ControlWebMethodName
    {
      get => (string) this.ViewState[nameof (ControlWebMethodName)] ?? string.Empty;
      set => this.ViewState[nameof (ControlWebMethodName)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the web method name which handles layout[control]-related actions (new, delete, duplicate, etc)
    /// </summary>
    /// <value>The web method name.</value>
    public string LayoutWebMethodName
    {
      get => (string) this.ViewState[nameof (LayoutWebMethodName)] ?? string.Empty;
      set => this.ViewState[nameof (LayoutWebMethodName)] = (object) value;
    }

    /// <summary>Gets the unique ID of the toolbox container.</summary>
    /// <value>The toolbox container unique ID.</value>
    public string ToolboxContainerUniqueID => this.ZoneEditorWrapper.ToolboxContainer.UniqueID;

    internal ZoneEditorWrapper ZoneEditorWrapper { get; set; }

    private LayoutEditor LayoutEditor
    {
      get
      {
        if (this.layoutEditor == null)
        {
          this.layoutEditor = new LayoutEditor(this.ClientID);
          this.layoutEditor.ID = "LayoutEditor1";
        }
        return this.layoutEditor;
      }
    }

    private ThemesEditor ThemesEditor
    {
      get
      {
        if (this.themesEditor == null)
        {
          this.themesEditor = new ThemesEditor()
          {
            PageId = this.DraftId,
            Language = this.Proxy.CurrentObjectCulture
          };
          this.themesEditor.ID = "ThemesEditor1";
          this.themesEditor.CssClass = "sfWidgetsWrp";
        }
        return this.themesEditor;
      }
    }

    private FormSettingsEditor SettingsEditor
    {
      get
      {
        if (this.settingsEditor == null)
        {
          if (this.Proxy.MediaType == DesignMediaType.Page)
          {
            this.settingsEditor = new FormSettingsEditor()
            {
              FormDraftId = this.DraftId
            };
            this.settingsEditor.ID = "SettingsEditor1";
            this.settingsEditor.CssClass = "sfWidgetsWrp";
            this.settingsEditor.CurrentCulture = this.Proxy.CurrentObjectCulture;
          }
          else if (this.Proxy.MediaType == DesignMediaType.Form)
          {
            CultureInfo cultureInfo = (CultureInfo) null;
            if (SystemManager.CurrentContext.AppSettings.Multilingual)
            {
              cultureInfo = SystemManager.CurrentContext.Culture;
              SystemManager.CurrentContext.Culture = this.Proxy.CurrentObjectCulture;
            }
            try
            {
              FormsManager manager = FormsManager.GetManager();
              FormDraft cachedFormDraft = this.CachedFormDraft;
              FormDescription form = manager.GetForm(cachedFormDraft.ParentForm.Id);
              manager.CopyFormCommonData<FormDraftControl, FormControl>((IFormData<FormDraftControl>) cachedFormDraft, (IFormData<FormControl>) form, CopyDirection.Unspecified);
              FormDescriptionViewModel descriptionViewModel = new FormDescriptionViewModel(form);
              this.settingsEditor = new FormSettingsEditor()
              {
                Form = descriptionViewModel,
                FormDraftId = this.DraftId
              };
              this.settingsEditor.ID = "SettingsEditor1";
              this.settingsEditor.CssClass = "sfWidgetsWrp";
              this.settingsEditor.CurrentCulture = this.Proxy.CurrentObjectCulture;
            }
            finally
            {
              if (SystemManager.CurrentContext.AppSettings.Multilingual)
                SystemManager.CurrentContext.Culture = cultureInfo;
            }
          }
        }
        return this.settingsEditor;
      }
    }

    /// <summary>Gets or sets the name of the item.</summary>
    /// <value>The name of the item.</value>
    public string ItemName { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>Gets or sets the locking handler id.</summary>
    /// <value>The locking handler id.</value>
    public string LockingHandlerId { get; set; }

    /// <summary>Gets or sets the proxy.</summary>
    /// <value>The proxy.</value>
    internal DraftProxyBase Proxy { get; set; }

    /// <summary>Set (propagate) skin to child subcontrols</summary>
    protected void SetChildControlSkins()
    {
      if (this.PersonalizationMenu != null)
        this.PersonalizationMenu.Skin = this.Skin;
      if (this.RadContextMenu1 != null)
        this.RadContextMenu1.Skin = this.Skin;
      if (this.RadWindowManager1 != null)
        this.RadWindowManager1.Skin = this.Skin;
      if (this.ControlWrapperFactory != null)
        this.ControlWrapperFactory.Skin = this.Skin;
      if (this.LayoutWrapperFactory != null)
        this.LayoutWrapperFactory.Skin = this.Skin;
      if (this.ControlToolboxContainer is ISkinnableControl)
        ((ISkinnableControl) this.ControlToolboxContainer).Skin = this.Skin;
      if (this.LayoutToolboxContainer is ISkinnableControl)
        ((ISkinnableControl) this.LayoutToolboxContainer).Skin = this.Skin;
      if (this.RadTabStrip1 == null)
        return;
      this.RadTabStrip1.Skin = this.Skin;
    }

    /// <summary>
    /// Code moved into this method from OnPreRender to make sure it executed when the framework skips OnPreRender() for some reason
    /// </summary>
    protected override void ControlPreRender()
    {
      this.SetChildControlSkins();
      if (this.RadTabStrip1 != null && !this.ShowTabStrip)
        this.RadTabStrip1.Style["display"] = "none";
      base.ControlPreRender();
    }

    /// <summary>Gets the dock zone IDs.</summary>
    /// <param name="zones">The zones.</param>
    /// <returns></returns>
    protected string GetDockZoneIDs(IList<RadDockZone> zones)
    {
      ArrayList arrayList = new ArrayList();
      foreach (RadDockZone zone in (IEnumerable<RadDockZone>) zones)
        arrayList.Add((object) zone.ClientID);
      return new JavaScriptSerializer().Serialize((object) arrayList);
    }

    /// <summary>
    /// Creates RadWindowManager which will open a new window once a RadDock's edit command is clicked.
    /// </summary>
    protected RadWindowManager CreateWindowManager()
    {
      RadWindowManager windowManager = new RadWindowManager();
      windowManager.ID = "RadWindowManager1";
      windowManager.Width = Unit.Pixel(600);
      windowManager.Height = Unit.Pixel(600);
      windowManager.ShowContentDuringLoad = false;
      windowManager.Behaviors = WindowBehaviors.Resize | WindowBehaviors.Close | WindowBehaviors.Move;
      windowManager.Modal = true;
      windowManager.DestroyOnClose = false;
      windowManager.ReloadOnShow = true;
      return windowManager;
    }

    /// <summary>
    /// Creates RadContextMenu which will open a new tooltip for personalization.
    /// </summary>
    /// <returns>The personalization RadContextMenu.</returns>
    protected RadContextMenu CreatePersonalizationContextMenu()
    {
      RadContextMenu personalizationContextMenu = new RadContextMenu();
      personalizationContextMenu.CollapseAnimation.Type = AnimationType.None;
      personalizationContextMenu.ExpandAnimation.Type = AnimationType.None;
      personalizationContextMenu.CssClass = "sfMoreContextMenu sfMoreContextMenuSelectedState";
      personalizationContextMenu.ID = "PersonalizationMenu";
      return personalizationContextMenu;
    }

    /// <summary>
    /// Creates RadTooltipManager which will open a new tooltip(dropdown commands) once a RadDock's more command is clicked.
    /// </summary>
    /// <returns></returns>
    protected RadContextMenu CreateCommandContextMenu()
    {
      RadContextMenu commandContextMenu = new RadContextMenu();
      commandContextMenu.CollapseAnimation.Type = AnimationType.None;
      commandContextMenu.ExpandAnimation.Type = AnimationType.None;
      commandContextMenu.CssClass = "sfMoreContextMenu";
      commandContextMenu.ID = "RadContextMenu1";
      return commandContextMenu;
    }

    /// <summary>Processes the toolbox section.</summary>
    /// <param name="section">The section.</param>
    /// <returns>The dock zone.</returns>
    protected RadDockZone ProcessToolboxSection(IToolboxSection section)
    {
      IEnumerable<IToolboxFilter> filters = ObjectFactory.Container.ResolveAll<IToolboxFilter>();
      List<IToolboxItem> list = section.Tools.Where<IToolboxItem>((Func<IToolboxItem, bool>) (s => filters.All<IToolboxFilter>((Func<IToolboxFilter, bool>) (f => f.IsToolVisible(s))))).ToList<IToolboxItem>();
      RadDockZone radDockZone = new RadDockZone();
      foreach (IToolboxItem toolboxItem in (IEnumerable<IToolboxItem>) list)
      {
        if ((this.MediaType == DesignMediaType.Template || toolboxItem.VisibilityMode != ToolboxItemVisibilityMode.Templates) && (this.MediaType == DesignMediaType.Page || toolboxItem.VisibilityMode != ToolboxItemVisibilityMode.Pages) && (this.Framework != PageTemplateFramework.Mvc || !string.IsNullOrEmpty(toolboxItem.ControllerType) || !string.IsNullOrEmpty(toolboxItem.LayoutTemplate)) && (this.Framework != PageTemplateFramework.WebForms || string.IsNullOrEmpty(toolboxItem.ControllerType)))
        {
          RadDock toolboxDock = this.CreateToolboxDock(toolboxItem);
          if (toolboxDock != null)
            radDockZone.Controls.Add((Control) toolboxDock);
        }
      }
      if (radDockZone.Controls.Count > 0)
        this._toolboxDockingZones.Add(radDockZone);
      return radDockZone;
    }

    /// <summary>Creates the tab strip and multipage.</summary>
    /// <param name="container">The container.</param>
    protected void CreateTabStripAndMultipage(Control container)
    {
      RadTabStrip child1 = new RadTabStrip();
      this.RadTabStrip1 = child1;
      RadTab tab1 = new RadTab();
      tab1.Text = this.Localization["ZoneEditorControls"];
      tab1.Text = "Controls";
      tab1.Value = "Controls";
      tab1.Selected = true;
      child1.Tabs.Add(tab1);
      RadTab tab2 = new RadTab();
      tab2.Value = "Layouts";
      tab2.Text = this.Localization["ZoneEditorLayouts"];
      child1.Tabs.Add(tab2);
      RadTab tab3 = new RadTab();
      if (this.MediaType == DesignMediaType.Form)
      {
        tab3.Value = "Settings";
        tab3.Text = this.Localization["ZoneEditorSettings"];
      }
      else if (this.MediaType == DesignMediaType.NewsletterCampaign)
      {
        tab3.Value = "EditPlainText";
        tab3.Text = this.Localization["ZoneEditorPlainText"];
      }
      else
      {
        tab3.Value = "Themes";
        tab3.Text = this.Localization["ZoneEditorThemes"];
      }
      child1.Tabs.Add(tab3);
      RadMultiPage child2 = new RadMultiPage();
      this.RadMultiPage1 = child2;
      RadPageView pageView1 = new RadPageView();
      pageView1.Controls.Add((Control) this.controlsTab);
      pageView1.CssClass = "sfContentGadgetsPane sfToolboxPane";
      RadPageView pageView2 = new RadPageView();
      pageView2.Controls.Add((Control) this.layoutTab);
      pageView2.CssClass = "sfLayoutGadgetsPane sfToolboxPane";
      RadPageView pageView3 = new RadPageView();
      if (this.MediaType == DesignMediaType.Form)
      {
        pageView3.Controls.Add((Control) this.settingsTab);
        pageView3.CssClass = "sfSettingsPane sfToolboxPane";
      }
      else if (this.MediaType == DesignMediaType.Template && this.Framework != PageTemplateFramework.Mvc)
      {
        pageView3.Controls.Add((Control) this.themesTab);
        pageView3.CssClass = "sfThemesPane sfToolboxPane";
      }
      else if (this.MediaType == DesignMediaType.NewsletterCampaign)
        pageView3.Controls.Add((Control) new PlainTextEditor());
      else
        pageView3 = (RadPageView) null;
      child2.PageViews.Add(pageView1);
      child2.PageViews.Add(pageView2);
      if (pageView3 != null)
        child2.PageViews.Add(pageView3);
      child2.ID = "ZoneEditorMultipage";
      child2.CssClass = "ZoneEditorMultipage";
      child1.MultiPageID = child2.ID;
      pageView1.Selected = true;
      child1.Skin = this.Skin;
      container.Controls.Add((Control) child1);
      container.Controls.Add((Control) child2);
    }

    /// <summary>
    /// Determines whether the server control contains child controls. If it does not, it creates child controls.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.controlIds.Clear();
      this.misplacedLayoutControls.Clear();
      this.emptyControls.Clear();
      this.insertedEmptySiblingControls.Clear();
      this.insertedControls.Clear();
      this.orphanedControlsPlaceholder = (Control) null;
      base.CreateChildControls();
      this.CreatePlaceholders();
      this.ProcessPlaceholders();
      this.ProcessMisplacedControls();
      this.ProcessOrphanedControls();
      if (!this.Enabled)
        return;
      this.CreateToolbox();
      this.RadContextMenu1 = this.CreateCommandContextMenu();
      this.Controls.Add((Control) this.RadContextMenu1);
      this.PersonalizationMenu = this.CreatePersonalizationContextMenu();
      this.Controls.Add((Control) this.PersonalizationMenu);
      this.RadWindowManager1 = this.CreateWindowManager();
      this.Controls.Add((Control) this.RadWindowManager1);
      RadDockZone child = new RadDockZone();
      this.Controls.Add((Control) child);
      this.ControlWrapperFactory = this.CreateControlWrapperDock((ControlData) null);
      child.Controls.Add((Control) this.ControlWrapperFactory);
      this.LayoutWrapperFactory = this.CreateLayoutWrapperDock((ControlData) null);
      child.Controls.Add((Control) this.LayoutWrapperFactory);
      this.Controls.Add((Control) this.LayoutEditor);
      this.Style.Add("top", "-10000px");
      this.Style.Add("left", "-10000px");
      this.Style.Add("position", "absolute");
      this.Style.Add("height", "1px");
      this.Style.Add("width", "1px");
      this.Attributes.Add("class", "sfZoneEditorWrp");
    }

    /// <summary>
    /// Handling of misplaced layout(and other) controls
    /// This happens because layouts are placed before other controls. If some layout control(placeholder) has a normal control
    /// as a sibling it cannot be inserted at the right position, because the sibling control isn't inserted yet.
    /// This should be re-worked, here is just a quick and ugly fix for beta 2.
    /// </summary>
    private void ProcessMisplacedControls()
    {
      foreach (KeyValuePair<Guid, Guid> misplacedLayoutControl in this.misplacedLayoutControls)
      {
        Guid key1 = misplacedLayoutControl.Key;
        Guid key2 = misplacedLayoutControl.Value;
        Control control1;
        this.controlIds.TryGetValue(key1, out control1);
        Control control2;
        this.controlIds.TryGetValue(key2, out control2);
        if (control1 != null && control2 != null)
        {
          Control parent = control2.Parent;
          int num1 = parent.Controls.IndexOf(control2);
          if (num1 > -1)
          {
            int num2 = parent.Controls.IndexOf(control1);
            if (num2 > -1 && num2 != num1 + 1)
            {
              List<Control> controlList = new List<Control>();
              controlList.Add(control1);
              Guid guid = key1;
              for (int index = num2 + 1; index < parent.Controls.Count; ++index)
              {
                Control control3 = parent.Controls[index];
                if (this.controlDataObjects.ContainsKey(control3))
                {
                  ControlData controlDataObject = this.controlDataObjects[control3];
                  if (controlDataObject.SiblingId == guid)
                  {
                    controlList.Add(control3);
                    guid = controlDataObject.Id;
                  }
                  else
                    break;
                }
                else
                  break;
              }
              int num3 = 0;
              for (int index1 = 0; index1 < controlList.Count; ++index1)
              {
                if (controlList[index1] is RadDock radDock)
                {
                  this.ReinitDockCommandsForControl((Control) radDock);
                  parent.Controls.Remove((Control) radDock);
                  int index2 = num1 + 1 + num3;
                  if (index2 > parent.Controls.Count)
                  {
                    index2 = parent.Controls.Count;
                  }
                  else
                  {
                    while (index2 <= parent.Controls.Count - 1)
                    {
                      if (this.controlDataObjects[parent.Controls[index2]].SiblingId == key2 && this.controlDataObjects[(Control) radDock] is Telerik.Sitefinity.Pages.Model.TemplateControl)
                      {
                        ++index2;
                        ++num3;
                      }
                      else
                        goto label_18;
                    }
                    index2 = parent.Controls.Count;
                  }
label_18:
                  parent.Controls.AddAt(index2, (Control) radDock);
                  ++num3;
                }
              }
            }
          }
        }
      }
    }

    protected virtual void ProcessOrphanedControls()
    {
      foreach (ControlData controlData in this.PageControls.Except<ControlData>((IEnumerable<ControlData>) this.insertedControls).Where<ControlData>((Func<ControlData, bool>) (control =>
      {
        Guid baseControlId = control.BaseControlId;
        return !(control.BaseControlId != Guid.Empty);
      })))
        this.AddControlToContainer(controlData, this.orphanedLayoutsDockZone, true, true);
    }

    /// <summary>
    /// Reinits the dock commands for control and all child controls.
    /// Note: this is used for a hack. Reinitializing dock commands
    /// is necessary because of re-inserting RadDock controls in the page.
    /// This is done so that the docks can be in the right order.
    /// Without this reinitialization, the commands are duplicated.
    /// </summary>
    /// <param name="c">The control from which to start.</param>
    protected virtual void ReinitDockCommandsForControl(Control c)
    {
      if (c is RadDock)
        this.ReinitCommandsForDock(c as RadDock);
      foreach (Control control in c.Controls)
        this.ReinitDockCommandsForControl(control);
    }

    /// <summary>Reinits the commands for dock.</summary>
    /// <param name="dock">The dock.</param>
    protected virtual void ReinitCommandsForDock(RadDock dock)
    {
      dock.Commands.Clear();
      dock.TitlebarContainer.Controls.Clear();
      if (dock.TitlebarTemplate != null)
        dock.TitlebarTemplate.InstantiateIn((Control) dock.TitlebarContainer);
      this.AddCommandsToDock(dock);
    }

    /// <summary>Creates the toolbox control.</summary>
    /// <param name="item">The item</param>
    /// <returns></returns>
    protected void CreateToolbox()
    {
      if (!(this.ToolboxContainerUniqueID != string.Empty))
        return;
      Control control = this.Page.FindControl(this.ToolboxContainerUniqueID);
      if (control == null)
        return;
      Panel panel = new Panel();
      panel.CssClass = "zeToolboxContainer";
      control.Controls.Add((Control) panel);
      this.CreateLayoutToolBox();
      this.CreateControlToolBox();
      if (this.MediaType == DesignMediaType.Form)
        this.CreateSettingsToolBox();
      else if (this.MediaType == DesignMediaType.Template && this.Framework != PageTemplateFramework.Mvc)
        this.CreateThemesToolBox();
      if (this.ControlToolboxContainer is ISkinnableControl)
        ((ISkinnableControl) this.ControlToolboxContainer).Skin = this.Skin;
      if (this.LayoutToolboxContainer is ISkinnableControl)
        ((ISkinnableControl) this.LayoutToolboxContainer).Skin = this.Skin;
      if (this.LayoutToolboxContainer != null && this.ControlToolboxContainer != null)
        this.CreateTabStripAndMultipage((Control) panel);
      else if (this.LayoutToolboxContainer != null)
      {
        panel.Controls.Add((Control) this.layoutTab);
      }
      else
      {
        if (this.ControlToolboxContainer == null)
          return;
        panel.Controls.Add(this.ControlToolboxContainer);
      }
    }

    /// <summary>Creates the Control toolbox which has all layouts</summary>
    /// <returns></returns>
    protected void CreateLayoutToolBox()
    {
      this.layoutTab = (HtmlContainerControl) new HtmlGenericControl("div");
      this.LayoutToolboxContainer = this.CreateToolBoxPanelbar(this.LayoutToolbox, "LayoutToolboxContainer");
      HtmlGenericControl child1 = new HtmlGenericControl("h2");
      child1.InnerHtml = Res.Get<PageResources>("PageLayoutToolboxTitle", Res.CurrentBackendCulture);
      child1.Attributes.Add("class", "sftbTitle");
      this.layoutTab.Controls.Add((Control) child1);
      this.layoutTab.Controls.Add(this.LayoutToolboxContainer);
      this.layoutTab.Controls.Add(this.LayoutLinks);
      if (this.MediaType == DesignMediaType.Page || this.MediaType == DesignMediaType.Template || this.MediaType == DesignMediaType.NewsletterCampaign || this.MediaType == DesignMediaType.NewsletterTemplate)
      {
        HtmlGenericControl child2 = new HtmlGenericControl("h2");
        child2.InnerHtml = Res.Get<PageResources>("PageLayoutChangeTemplate", Res.CurrentBackendCulture);
        child2.Attributes.Add("class", "sftbTitle sftbTitleAltOption");
        this.layoutTab.Controls.Add((Control) child2);
        bool isBackend = this.Proxy.IsBackend;
        PageTemplateField pageTemplateField = new PageTemplateField();
        pageTemplateField.ID = "PageTemplateField";
        pageTemplateField.DataFieldName = "Template";
        pageTemplateField.Title = string.Empty;
        pageTemplateField.DisplayMode = FieldDisplayMode.Write;
        pageTemplateField.WrapperTag = HtmlTextWriterTag.Div;
        pageTemplateField.ShowEmptyTemplate = true;
        pageTemplateField.CssClass = "sfPageTemplateInToolbox";
        pageTemplateField.UseDefaultTemplate = false;
        pageTemplateField.IsBackendTemplate = isBackend;
        this.templateControl = pageTemplateField;
        if (this.MediaType != DesignMediaType.Page)
          this.templateControl.Framework = new PageTemplateFramework?(this.Framework);
        if (this.MediaType == DesignMediaType.Template)
          this.templateControl.ShowAllBasicTemplates = true;
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
          this.templateControl.Language = this.Proxy.CurrentObjectCulture;
        this.layoutTab.Controls.Add((Control) this.templateControl);
      }
      if (!SystemManager.IsModuleAccessible("ResponsiveDesign") || this.Framework == PageTemplateFramework.Mvc || this.MediaType != DesignMediaType.Page && this.MediaType != DesignMediaType.Template)
        return;
      this.layoutTab.Controls.Add((Control) this.GetResponsiveLayoutField());
    }

    private ResponsiveLayoutField GetResponsiveLayoutField()
    {
      HtmlGenericControl child = new HtmlGenericControl("h2");
      child.InnerHtml = Res.Get<PageResources>("ChangeResponsiveDesignLayout", Res.CurrentBackendCulture);
      child.Attributes.Add("class", "sftbTitle sftbTitleResponsive");
      this.layoutTab.Controls.Add((Control) child);
      Guid guid = this.MediaType != DesignMediaType.Page ? (this.MediaType != DesignMediaType.Template ? Guid.Empty : this.CachedTemplateDraft.ParentTemplate.Id) : this.CachedPageDraft.ParentPage.Id;
      ResponsiveLayoutField responsiveLayoutField = new ResponsiveLayoutField();
      responsiveLayoutField.ID = "ResponsiveLayoutField";
      responsiveLayoutField.Title = string.Empty;
      responsiveLayoutField.DisplayMode = FieldDisplayMode.Write;
      responsiveLayoutField.WrapperTag = HtmlTextWriterTag.Div;
      responsiveLayoutField.CssClass = "sfResponsiveInToolbox";
      responsiveLayoutField.ItemId = guid;
      responsiveLayoutField.ItemType = this.MediaType.ToString();
      return responsiveLayoutField;
    }

    /// <summary>Creates the Control toolbox which has all tools</summary>
    /// <returns></returns>
    protected void CreateControlToolBox()
    {
      this.controlsTab = new HtmlGenericControl("div");
      this.ControlToolboxContainer = this.CreateToolBoxPanelbar(this.ControlToolbox, "ControlToolboxContainer");
      if (this.MediaType == DesignMediaType.Page || this.MediaType == DesignMediaType.Template)
      {
        HtmlGenericControl child = new HtmlGenericControl("h2");
        child.InnerHtml = Res.Get<PageResources>("PageControlsToolboxTitle", Res.CurrentBackendCulture);
        child.Attributes.Add("class", "sftbTitle sftbGadgets");
        this.controlsTab.Controls.Add((Control) child);
      }
      if (this.MediaType == DesignMediaType.Form)
      {
        HtmlGenericControl child = new HtmlGenericControl("h2");
        child.InnerHtml = Res.Get<PageResources>("DragFormWidgetsToTheLeft", Res.CurrentBackendCulture);
        child.Attributes.Add("class", "sftbTitle sftbFormGadgets");
        this.controlsTab.Controls.Add((Control) child);
      }
      if (this.MediaType == DesignMediaType.NewsletterCampaign || this.MediaType == DesignMediaType.NewsletterTemplate)
      {
        HtmlGenericControl child = new HtmlGenericControl("h2");
        child.InnerHtml = Res.Get<PageResources>("DragFormWidgetsToTheLeft", Res.CurrentBackendCulture);
        child.Attributes.Add("class", "sftbTitle sftbFormGadgets");
        this.controlsTab.Controls.Add((Control) child);
      }
      this.controlsTab.Controls.Add(this.ControlToolboxContainer);
      this.controlsTab.Controls.Add(this.WidgetLinks);
    }

    /// <summary>Creates the themes tool box.</summary>
    protected void CreateThemesToolBox()
    {
      this.themesTab = new HtmlGenericControl("div");
      this.ThemesToolboxContainer = (Control) this.ThemesEditor;
      HtmlGenericControl child = new HtmlGenericControl("h2");
      child.InnerHtml = Res.Get<PageResources>("PageThemesToolboxTitle", Res.CurrentBackendCulture);
      child.Attributes.Add("class", "sftbTitle");
      this.themesTab.Controls.Add((Control) child);
      this.themesTab.Controls.Add((Control) this.ThemesEditor);
      this.themesTab.Controls.Add(this.ThemesLinks);
    }

    /// <summary>Creates the settings tool box.</summary>
    protected void CreateSettingsToolBox()
    {
      this.SettingsEditor.ZoneEditorId = this.ClientID;
      this.settingsTab = new HtmlGenericControl("div");
      HtmlGenericControl child = new HtmlGenericControl("h2");
      child.InnerHtml = Res.Get<PageResources>("PageSettingsToolboxTitle", Res.CurrentBackendCulture);
      child.Attributes.Add("class", "sftbTitle");
      this.settingsTab.Controls.Add((Control) child);
      this.settingsTab.Controls.Add((Control) this.SettingsEditor);
      this.settingsTab.Controls.Add(this.SettingsLinks);
    }

    private Control CreateToolBoxPanelbar(IToolbox toolbox, string id)
    {
      if (toolbox == null)
        return (Control) null;
      IToolboxFilterContext context = (IToolboxFilterContext) null;
      context = (IToolboxFilterContext) new ZoneEditor.ToolboxFilterContext(this.MediaType, id);
      IEnumerable<IToolboxFilter> filters = ObjectFactory.Container.ResolveAll<IToolboxFilter>();
      IList<IToolboxSection> list = (IList<IToolboxSection>) toolbox.Sections.Where<IToolboxSection>((Func<IToolboxSection, bool>) (s => filters.All<IToolboxFilter>((Func<IToolboxFilter, bool>) (f => f.IsSectionVisible(s, context))))).ToList<IToolboxSection>();
      if (list.Count == 1)
      {
        RadDockZone toolBoxPanelbar = this.ProcessToolboxSection(list[0]);
        if (toolBoxPanelbar != null)
          toolBoxPanelbar.CssClass = "sfWidgetsWrp";
        return (Control) toolBoxPanelbar;
      }
      RadPanelBar toolBoxPanelbar1 = new RadPanelBar();
      toolBoxPanelbar1.ID = id;
      toolBoxPanelbar1.ExpandMode = PanelBarExpandMode.SingleExpandedItem;
      toolBoxPanelbar1.CollapseAnimation.Duration = 200;
      toolBoxPanelbar1.ExpandAnimation.Duration = 200;
      toolBoxPanelbar1.Width = Unit.Pixel(230);
      toolBoxPanelbar1.OnClientItemClicking = "collapseRoots";
      toolBoxPanelbar1.CssClass = "sfWidgetsWrp";
      foreach (ToolboxSection section in (IEnumerable<IToolboxSection>) list)
      {
        RadDockZone child = this.ProcessToolboxSection((IToolboxSection) section);
        if (child != null && child.Controls.Count > 0)
        {
          string text = string.IsNullOrEmpty(section.ResourceClassId) ? section.Title : Res.Get(section.ResourceClassId, section.Title, Res.CurrentBackendCulture, true, false);
          if (string.IsNullOrEmpty(text))
            text = section.Title;
          if (string.IsNullOrEmpty(text))
            text = section.Name;
          RadPanelItem radPanelItem1 = new RadPanelItem(text);
          toolBoxPanelbar1.Items.Add(radPanelItem1);
          RadPanelItem radPanelItem2 = new RadPanelItem();
          radPanelItem1.Items.Add(radPanelItem2);
          child.Skin = this.Skin;
          radPanelItem2.Controls.Add((Control) child);
        }
      }
      if (toolBoxPanelbar1.Items.Count > 0)
        toolBoxPanelbar1.Items[0].Expanded = true;
      return (Control) toolBoxPanelbar1;
    }

    /// <summary>
    /// Creates the LayoutControls and Zones and nests those in one another.
    /// </summary>
    /// <returns></returns>
    protected void CreatePlaceholders()
    {
      foreach (ControlData placeholder in (IEnumerable<ControlData>) this.Placeholders)
        this.AddPlaceholderToPage(placeholder);
    }

    private void AddPlaceholderToPage(ControlData controlData)
    {
      Control control1 = (Control) null;
      RadDock control2 = (RadDock) null;
      if (controlData.PlaceHolder != null && controlData.PlaceHolder != string.Empty)
      {
        Control placeholderById = this.GetPlaceholderById(controlData.PlaceHolder);
        if (this.Enabled)
        {
          if (placeholderById != null && (this.orphanedControlsPlaceholder == null || placeholderById.ID == "Body"))
            this.orphanedControlsPlaceholder = placeholderById;
          Control container = (Control) this.GetDockZoneByPlaceholderId(controlData.PlaceHolder);
          bool isOrphaned = false;
          if (container == null)
          {
            container = (Control) this.AddDockZone(placeholderById);
            if (placeholderById == null || container == null)
            {
              container = this.orphanedLayoutsDockZone;
              isOrphaned = true;
            }
          }
          if (container != null && (this.orphanedLayoutsDockZone == null || placeholderById != null && placeholderById.ID == "Body"))
            this.orphanedLayoutsDockZone = container;
          control2 = this.CreateLayoutWrapperDock(controlData, isOrphaned);
          control1 = (Control) control2.ContentContainer;
          if (!this.AddControlDockToContainer(controlData, control2, container, false))
            this.misplacedLayoutControls.Add(controlData.Id, controlData.SiblingId);
        }
        else
        {
          control1 = new Control();
          placeholderById.Controls.Add(control1);
        }
      }
      ZoneEditorLayoutEventArgs editorLayoutEventArgs = this.OnLayoutControlAdd(control1, controlData);
      if (this.orphanedLayoutsDockZone == null && editorLayoutEventArgs.LayoutControl != null && editorLayoutEventArgs.LayoutControl.Placeholders.Count > 0)
      {
        Control placeholder = editorLayoutEventArgs.LayoutControl.Placeholders[0];
        this.orphanedLayoutsDockZone = (Control) (this.GetDockZoneByPlaceholderId(placeholder.ID) ?? this.AddDockZone(placeholder));
      }
      if (editorLayoutEventArgs.Editable || control2 == null)
        return;
      control2.DockHandle = DockHandle.None;
    }

    /// <summary>
    /// Adds the control to the specified container handling siblings.
    /// </summary>
    /// <param name="controlData">The control data.</param>
    /// <param name="control">The control.</param>
    /// <param name="container">The container.</param>
    /// <returns>Returns a bool value indicating whether the sibling for the control is handled properly.</returns>
    private bool AddControlDockToContainer(
      ControlData controlData,
      RadDock control,
      Control container,
      bool addLast)
    {
      bool container1 = true;
      if (container != null)
      {
        if (addLast)
          container.Controls.Add((Control) control);
        else if (controlData.SiblingId == Guid.Empty)
        {
          int emptySiblingControl = PageHelper.GetInsertIndexOfEmptySiblingControl(controlData, this.insertedEmptySiblingControls, this.ContainerIdsOrdered);
          container.Controls.AddAt(emptySiblingControl, (Control) control);
          List<ControlData> controlDataList;
          this.insertedEmptySiblingControls.TryGetValue(controlData.PlaceHolder, out controlDataList);
          if (controlDataList == null)
          {
            controlDataList = new List<ControlData>();
            this.insertedEmptySiblingControls.Add(controlData.PlaceHolder, controlDataList);
          }
          controlDataList.Add(controlData);
        }
        else
        {
          Control control1;
          this.controlIds.TryGetValue(controlData.SiblingId, out control1);
          if (control1 != null)
          {
            int num = container.Controls.IndexOf(control1);
            container.Controls.AddAt(num + 1, (Control) control);
          }
          else
          {
            container.Controls.Add((Control) control);
            container1 = false;
          }
        }
      }
      this.controlIds[controlData.Id] = (Control) control;
      this.controlDataObjects[(Control) control] = controlData;
      return container1;
    }

    private void AddControlToContainer(
      ControlData controlData,
      Control container,
      bool addLast,
      bool isOrphaned = false)
    {
      if (this.Enabled)
      {
        RadDock controlWrapperDock = this.CreateControlWrapperDock(controlData, isOrphaned);
        this.AddControlDockToContainer(controlData, controlWrapperDock, container, addLast);
        ZoneEditorEventArgs zoneEditorEventArgs = this.OnControlAdd((Control) controlWrapperDock.ContentContainer, controlData);
        Control realControl = zoneEditorEventArgs.RealControl;
        Control emptyControl = ZoneEditor.TryCreateEmptyControl(realControl);
        if (emptyControl != null && zoneEditorEventArgs.Editable)
        {
          this.emptyControls.Add(emptyControl, (Control) controlWrapperDock);
          controlWrapperDock.ContentContainer.Controls.Add(emptyControl);
        }
        if (!zoneEditorEventArgs.Editable)
          controlWrapperDock.DockHandle = DockHandle.None;
        object behaviorObject1 = ControlUtilities.BehaviorResolver.GetBehaviorObject(realControl);
        JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
        if (behaviorObject1 is IZoneEditorReloader zoneEditorReloader)
          controlWrapperDock.Attributes.Add("reloadKey", zoneEditorReloader.Key);
        if (behaviorObject1 is IHasDependentControls dependentControls)
        {
          if (dependentControls.CreateDependentKeys.Count<string>() > 0)
            controlWrapperDock.Attributes.Add("createDependentKeys", scriptSerializer.Serialize((object) dependentControls.CreateDependentKeys));
          if (dependentControls.DeleteDependentKeys.Count<string>() > 0)
            controlWrapperDock.Attributes.Add("deleteDependentKeys", scriptSerializer.Serialize((object) dependentControls.DeleteDependentKeys));
          if (dependentControls.IndexChangeDependentKeys.Count<string>() > 0)
            controlWrapperDock.Attributes.Add("indexChangedDependentKeys", scriptSerializer.Serialize((object) dependentControls.IndexChangeDependentKeys));
          if (dependentControls.ReloadDependentKeys.Count<string>() > 0)
            controlWrapperDock.Attributes.Add("reloadDependentKeys", scriptSerializer.Serialize((object) dependentControls.ReloadDependentKeys));
        }
        if (ControlUtilities.BehaviorResolver.GetBehaviorObject(realControl) is IHasEditCommands behaviorObject2)
        {
          if (!LicenseState.Current.LicenseInfo.IsPageControlsPermissionsEnabled)
          {
            foreach (WidgetMenuItem widgetMenuItem in behaviorObject2.Commands.Where<WidgetMenuItem>((Func<WidgetMenuItem, bool>) (c => c.CommandName == "permissions")).ToList<WidgetMenuItem>())
              behaviorObject2.Commands.Remove(widgetMenuItem);
          }
          string str = scriptSerializer.Serialize((object) behaviorObject2.Commands);
          controlWrapperDock.Attributes.Add("widgetCommands", str);
        }
        if (SystemManager.IsModuleAccessible("Personalization") && (this.MediaType == DesignMediaType.Page || this.MediaType == DesignMediaType.Template) && ZoneEditor.ImplementsInterface<IPersonalizable>(controlData))
        {
          ControlData controlData1 = controlData;
          switch (controlData)
          {
            case PageDraftControl _:
            case TemplateDraftControl _:
            case PageControl _:
            case Telerik.Sitefinity.Pages.Model.TemplateControl _:
              if (controlData.IsOverridedControl)
              {
                ControlData controlByDraftId = this.GetOverridenControlByDraftId(this.DraftId, controlData.Id, out bool _);
                if (controlByDraftId != null)
                {
                  controlData1 = controlByDraftId;
                  break;
                }
                break;
              }
              break;
          }
          List<WidgetMenuItem> widgetSegments = ZoneEditor.GetWidgetSegments(controlData1);
          string str = scriptSerializer.Serialize((object) widgetSegments);
          controlWrapperDock.Attributes.Add("widgetSegments", str);
        }
        ZoneEditor.CustomizeControlDockTitlebar(controlData, realControl, controlWrapperDock, this.DraftId);
      }
      else
      {
        Control control = new Control();
        container.Controls.Add(control);
        this.OnControlAdd(control, controlData);
      }
    }

    /// <summary>
    /// Create an "empty" control(icon+link) for the specified control.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    internal static Control TryCreateEmptyControl(Control control)
    {
      Panel emptyControl = (Panel) null;
      if (ControlUtilities.BehaviorResolver.GetBehaviorObject(control) is ICustomWidgetVisualization behaviorObject && behaviorObject.IsEmpty)
      {
        string emptyLinkText = behaviorObject.EmptyLinkText;
        string str = "sfAddContentLnk";
        string cssClassForControl = ZoneEditor.GetCssClassForControl(control);
        if (cssClassForControl != null)
          str = str + " " + cssClassForControl;
        emptyControl = new Panel();
        emptyControl.Attributes.Add("class", "sfAddContentWrp");
        emptyControl.ID = "emptyControl";
        HtmlAnchor child1 = new HtmlAnchor();
        child1.Attributes.Add("class", str);
        HtmlGenericControl child2 = new HtmlGenericControl("span");
        child2.InnerHtml = emptyLinkText;
        child1.Controls.Add((Control) child2);
        emptyControl.Controls.Add((Control) child1);
      }
      return (Control) emptyControl;
    }

    /// <summary>
    /// Gets the CSS class for control. Tries to get the css class from all toolboxes available.
    /// Note that this is a workaround method. In most cases the method accepting a string parameter
    /// toolboxName should be invoked. It would be best to implement the CSS class of a control
    /// to be an attribute of the control, instead of being defined in the toolbox.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    internal static string GetCssClassForControl(Control control)
    {
      string cssClassForControl1 = (string) null;
      if (ControlUtilities.BehaviorResolver.GetBehaviorObject(control) is ICustomWidgetVisualizationExtended behaviorObject && !string.IsNullOrEmpty(behaviorObject.WidgetCssClass))
      {
        cssClassForControl1 = behaviorObject.WidgetCssClass;
      }
      else
      {
        foreach (string key in (IEnumerable<string>) Config.Get<ToolboxesConfig>().Toolboxes.Keys)
        {
          string cssClassForControl2 = ZoneEditor.GetCssClassForControl(control, key);
          if (cssClassForControl2 != null)
          {
            cssClassForControl1 = cssClassForControl2;
            break;
          }
        }
      }
      return cssClassForControl1;
    }

    /// <summary>Gets the CSS class for control.</summary>
    /// <param name="control">The control.</param>
    /// <param name="toolboxName">Name of the toolbox.</param>
    /// <returns></returns>
    internal static string GetCssClassForControl(Control control, string toolboxName)
    {
      string typeName = control.GetType().FullName;
      foreach (ToolboxSection section in Config.Get<ToolboxesConfig>().Toolboxes[toolboxName].Sections)
      {
        ToolboxItem toolboxItem = section.Tools.Where<ToolboxItem>((Func<ToolboxItem, bool>) (i =>
        {
          if (i.ControlType == typeName)
            return true;
          return i.ControlType.Contains<char>(',') && i.ControlType.Substring(0, i.ControlType.IndexOf(",")) == typeName;
        })).FirstOrDefault<ToolboxItem>();
        if (toolboxItem != null)
          return toolboxItem.CssClass;
      }
      return (string) null;
    }

    internal static void CustomizeControlDockTitlebar(
      ControlData controlData,
      Control control,
      RadDock dock,
      Guid draftId)
    {
      ICustomWidgetTitlebar behaviorObject = ControlUtilities.BehaviorResolver.GetBehaviorObject(control) as ICustomWidgetTitlebar;
      HtmlGenericControl child = new HtmlGenericControl("SPAN");
      StringBuilder stringBuilder = new StringBuilder();
      if (behaviorObject != null)
      {
        foreach (string customMessage in (IEnumerable<string>) behaviorObject.CustomMessages)
          stringBuilder.Append(customMessage);
      }
      if (controlData.Editable && controlData.IsOverridedControl && controlData.BaseControlId == Guid.Empty && controlData is TemplateDraftControl && ((TemplateDraftControl) controlData).Page.Id == draftId)
      {
        string overridedControlTitle = ZoneEditor.GetOverridedControlTitle(controlData, dock.ClientID);
        stringBuilder.Append(overridedControlTitle);
      }
      if (stringBuilder.Length <= 0)
        return;
      child.InnerHtml = stringBuilder.ToString();
      child.Attributes.Add("class", "sfCustomTitleWrapper");
      dock.TitlebarContainer.Controls.Add((Control) child);
      dock.CssClass += " sfCustomTitle";
    }

    /// <summary>Gets the placeholder by id.</summary>
    /// <param name="placeholder">The placeholder.</param>
    /// <returns></returns>
    protected Control GetPlaceholderById(string placeholder)
    {
      foreach (Control placeholderControl in this._placeholderControls)
      {
        if (placeholderControl.ID == placeholder)
          return placeholderControl;
      }
      return (Control) null;
    }

    /// <summary>Gets the dock zone by placeholder id.</summary>
    /// <param name="placeholder">The placeholder.</param>
    /// <returns></returns>
    protected RadDockZone GetDockZoneByPlaceholderId(string placeholder)
    {
      foreach (RadDockZone wrapperDockingZone in (IEnumerable<RadDockZone>) this._wrapperDockingZones)
      {
        if (wrapperDockingZone.Attributes["placeholderid"] == placeholder)
          return wrapperDockingZone;
      }
      return (RadDockZone) null;
    }

    /// <summary>Adds the dock zone.</summary>
    /// <param name="placeholder">The placeholder.</param>
    /// <returns></returns>
    protected RadDockZone AddDockZone(Control placeholder)
    {
      if (placeholder == null)
        return (RadDockZone) null;
      RadDockZone child = new RadDockZone();
      child.Skin = this.Skin;
      child.ID = "RadDockZone" + placeholder.ID;
      child.Attributes.Add("placeholderid", placeholder.ID);
      if (placeholder is SitefinityPlaceHolder)
      {
        SitefinityPlaceHolder sitefinityPlaceHolder = placeholder as SitefinityPlaceHolder;
        if (!string.IsNullOrEmpty(sitefinityPlaceHolder.Text))
          child.Attributes.Add("data-placeholder-label", HttpUtility.HtmlEncode(sitefinityPlaceHolder.Text.Trim().Replace(Environment.NewLine, string.Empty)));
      }
      if (placeholder is ContentPlaceHolder && placeholder.Controls.Count > 0)
      {
        for (int index = 0; index < placeholder.Controls.Count; ++index)
        {
          Control control = placeholder.Controls[index];
          if (control is LiteralControl)
          {
            string text = ((LiteralControl) control).Text;
            if (text != null && !text.IsNullOrWhitespace())
            {
              child.Attributes.Add("data-placeholder-label", HttpUtility.HtmlEncode(text.Trim().Replace(Environment.NewLine, string.Empty)));
              break;
            }
          }
        }
        placeholder.Controls.Clear();
      }
      placeholder.Controls.Add((Control) child);
      this._wrapperDockingZones.Add(child);
      child.MinHeight = Unit.Pixel(23);
      return child;
    }

    public static RadDockZone AddDockZoneStatic(Control placeholder, string skin)
    {
      if (placeholder == null)
        return (RadDockZone) null;
      RadDockZone child = new RadDockZone();
      child.Skin = skin;
      child.ID = "RadDockZone" + placeholder.ID;
      child.Attributes.Add("placeholderid", placeholder.ID);
      placeholder.Controls.Add((Control) child);
      child.MinHeight = Unit.Pixel(23);
      return child;
    }

    public List<Guid> ContainerIdsOrdered { get; set; }

    /// <summary>Processes the placeholders.</summary>
    protected void ProcessPlaceholders()
    {
      foreach (Control placeholderControl in this._placeholderControls)
        this.ProcessPlaceholder(placeholderControl);
    }

    /// <summary>Processes the placeholder.</summary>
    /// <param name="placeholder">The placeholder.</param>
    protected void ProcessPlaceholder(Control placeholder)
    {
      if (this.Enabled)
      {
        RadDockZone zone = this.GetDockZoneByPlaceholderId(placeholder.ID) ?? this.AddDockZone(placeholder);
        if (zone == null)
          return;
        this.AddControlDocksToZone(placeholder.ID, (Control) zone);
      }
      else
        this.AddControlDocksToZone(placeholder.ID, placeholder);
    }

    /// <summary>Gets the docks for placeholder.</summary>
    /// <param name="placeHolderId">The place holder pageId.</param>
    /// <returns></returns>
    protected void AddControlDocksToZone(string placeHolderId, Control zone)
    {
      List<ControlData> list = this.PageControls.Where<ControlData>((Func<ControlData, bool>) (x => x.PlaceHolder == placeHolderId)).ToList<ControlData>();
      object obj = (object) null;
      if (this.MediaType == DesignMediaType.Form && SystemManager.HttpContextItems.Contains((object) "FormControlId"))
        obj = SystemManager.HttpContextItems[(object) "FormControlId"];
      foreach (ControlData controlData in list)
      {
        if (this.MediaType == DesignMediaType.Form)
          SystemManager.HttpContextItems[(object) "FormControlId"] = (object) controlData.Id;
        this.AddControlToContainer(controlData, zone, false);
        this.insertedControls.Add(controlData);
      }
      if (this.MediaType != DesignMediaType.Form || !SystemManager.HttpContextItems.Contains((object) "FormControlId"))
        return;
      SystemManager.HttpContextItems[(object) "FormControlId"] = obj;
    }

    /// <summary>Creates the toolbox dock.</summary>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    protected RadDock CreateToolboxDock(IToolboxItem item)
    {
      if (!SystemManager.ValidateModuleItem((IModuleDependentItem) item))
        return (RadDock) null;
      Dictionary<string, string> graph = new Dictionary<string, string>();
      for (int index = 0; index < item.Parameters.Count; ++index)
        graph.Add(item.Parameters.Keys[index], item.Parameters[item.Parameters.Keys[index]]);
      string str;
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(typeof (Dictionary<string, string>)).WriteObject((Stream) memoryStream, (object) graph);
        str = Encoding.Default.GetString(memoryStream.ToArray());
      }
      RadDock toolboxDock = new RadDock();
      toolboxDock.Skin = this.Skin;
      toolboxDock.Attributes.Add("controltype", item.ControlType);
      toolboxDock.Attributes.Add("modulename", item.ModuleName);
      toolboxDock.Attributes.Add("layouttemplate", item.LayoutTemplate);
      toolboxDock.Attributes.Add("caption", item.Title);
      toolboxDock.Attributes.Add("description", item.Description);
      toolboxDock.Attributes.Add("classId", item.ResourceClassId);
      toolboxDock.Attributes.Add("parameters", str);
      if (!string.IsNullOrEmpty(item.ControllerType))
      {
        Type type = TypeResolutionService.ResolveType(item.ControllerType, false);
        if (type != (Type) null)
        {
          bool flag = type.ImplementsInterface(typeof (IFormPageBreak));
          toolboxDock.Attributes.Add("isPageBreak", flag.ToString());
        }
      }
      string controlDesignerUrl = ZoneEditor.GetControlDesignerUrl(item);
      if (!controlDesignerUrl.IsNullOrEmpty())
        toolboxDock.Attributes.Add("designerUrl", this.Page.ResolveUrl(controlDesignerUrl));
      if (string.IsNullOrEmpty(item.ResourceClassId))
      {
        toolboxDock.Title = item.Title;
        toolboxDock.ToolTip = item.Description;
      }
      else
      {
        toolboxDock.Title = Res.Get(item.ResourceClassId, item.Title, Res.CurrentBackendCulture);
        if (!string.IsNullOrEmpty(item.Description))
          toolboxDock.ToolTip = Res.Get(item.ResourceClassId, item.Description, Res.CurrentBackendCulture);
      }
      toolboxDock.CssClass = "zeToolboxItem";
      toolboxDock.TitlebarTemplate = (ITemplate) new ZoneEditor.ToolboxDockTemplate(item.CssClass);
      toolboxDock.DefaultCommands = DefaultCommands.None;
      toolboxDock.Collapsed = true;
      toolboxDock.DockMode = DockMode.Docked;
      return toolboxDock;
    }

    internal static string GetControlDesignerUrl(IToolboxItem item)
    {
      if (ObjectFactory.IsTypeRegistered<IDesignerResolver>())
      {
        IDesignerResolver designerResolver = ObjectFactory.Resolve<IDesignerResolver>();
        Type controlType = ZoneEditor.GetControlType(item);
        if (controlType != (Type) null)
          return designerResolver.GetUrl(controlType);
      }
      return (string) null;
    }

    internal static string GetControlDesignerUrl(ControlData item)
    {
      if (ObjectFactory.IsTypeRegistered<IDesignerResolver>())
      {
        IDesignerResolver designerResolver = ObjectFactory.Resolve<IDesignerResolver>();
        Type controlType = ZoneEditor.GetControlType(item);
        if (controlType != (Type) null)
          return designerResolver.GetUrl(controlType);
      }
      return (string) null;
    }

    /// <summary>Gets if the new widget editor is enabled for control</summary>
    /// <param name="control">The control data</param>
    /// <returns></returns>
    internal static bool OpenNewWidgetEditor(ControlData control)
    {
      IAdminAppSettings adminAppSettings = ObjectFactory.Resolve<IAdminAppSettings>();
      return (adminAppSettings == null ? 0 : (adminAppSettings.GetIsEnabledForCurrentUser() ? 1 : 0)) != 0 && adminAppSettings.IsWhiteListedModule("Pages") && ComponentAdaptorBase.SupportsAdminAppEditor(control);
    }

    private static Type GetControlType(IToolboxItem toolboxItem) => TypeResolutionService.ResolveType(toolboxItem.ControllerType.IsNullOrEmpty() ? toolboxItem.ControlType : toolboxItem.ControllerType, false);

    private ControlData GetOverridenControlByDraftId(
      Guid draftId,
      Guid controlId,
      out bool isTemplate)
    {
      PageDraft cachedPageDraft = this.CachedPageDraft;
      if (cachedPageDraft != null)
      {
        isTemplate = false;
        return this.GetOverridenControl(cachedPageDraft, controlId);
      }
      isTemplate = true;
      TemplateDraft cachedTemplateDraft = this.CachedTemplateDraft;
      return cachedTemplateDraft != null ? this.GetOverridenControl(cachedTemplateDraft, controlId) : (ControlData) null;
    }

    private PageDraft CachedPageDraft
    {
      get
      {
        if (this.cachedPageDraft == null)
          this.cachedPageDraft = this.PageProvider.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (x => x.Id == this.DraftId)).FirstOrDefault<PageDraft>();
        else if (this.cachedPageDraft.Id != this.DraftId)
          this.cachedPageDraft = this.PageProvider.GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (x => x.Id == this.DraftId)).FirstOrDefault<PageDraft>();
        return this.cachedPageDraft;
      }
    }

    private TemplateDraft CachedTemplateDraft
    {
      get
      {
        if (this.cachedTemplateDraft == null)
          this.cachedTemplateDraft = this.PageProvider.GetDrafts<TemplateDraft>().Where<TemplateDraft>((Expression<Func<TemplateDraft, bool>>) (p => p.Id == this.DraftId)).FirstOrDefault<TemplateDraft>();
        else if (this.cachedTemplateDraft.Id != this.DraftId)
          this.cachedTemplateDraft = this.PageProvider.GetDrafts<TemplateDraft>().Where<TemplateDraft>((Expression<Func<TemplateDraft, bool>>) (p => p.Id == this.DraftId)).FirstOrDefault<TemplateDraft>();
        return this.cachedTemplateDraft;
      }
    }

    private FormDraft CachedFormDraft
    {
      get
      {
        if (this.cachedFormDraft == null)
          this.cachedFormDraft = this.FormsProvider.GetDrafts().Where<FormDraft>((Expression<Func<FormDraft, bool>>) (x => x.Id == this.DraftId)).FirstOrDefault<FormDraft>();
        else if (this.cachedFormDraft.Id != this.DraftId)
          this.cachedFormDraft = this.FormsProvider.GetDrafts().Where<FormDraft>((Expression<Func<FormDraft, bool>>) (x => x.Id == this.DraftId)).FirstOrDefault<FormDraft>();
        return this.cachedFormDraft;
      }
    }

    private ControlData GetOverridenControl(PageDraft page, Guid controlId)
    {
      PageDraftControl pageDraftControl = page.Controls.Where<PageDraftControl>((Func<PageDraftControl, bool>) (control => control.BaseControlId == controlId)).FirstOrDefault<PageDraftControl>();
      return pageDraftControl == null && page.ParentPage.Template != null ? this.GetOverridenControl(page.ParentPage.Template, controlId) : (ControlData) pageDraftControl;
    }

    private ControlData GetOverridenControl(TemplateDraft page, Guid controlId)
    {
      TemplateDraftControl templateDraftControl = page.Controls.Where<TemplateDraftControl>((Func<TemplateDraftControl, bool>) (control => control.BaseControlId == controlId)).FirstOrDefault<TemplateDraftControl>();
      return templateDraftControl == null && page.ParentTemplate != null ? this.GetOverridenControl(page.ParentTemplate, controlId) : (ControlData) templateDraftControl;
    }

    private ControlData GetOverridenControl(PageTemplate page, Guid controlId)
    {
      Telerik.Sitefinity.Pages.Model.TemplateControl templateControl = page.Controls.Where<Telerik.Sitefinity.Pages.Model.TemplateControl>((Func<Telerik.Sitefinity.Pages.Model.TemplateControl, bool>) (control => control.BaseControlId == controlId)).FirstOrDefault<Telerik.Sitefinity.Pages.Model.TemplateControl>();
      return templateControl == null && page.ParentTemplate != null ? this.GetOverridenControl(page.ParentTemplate, controlId) : (ControlData) templateControl;
    }

    /// <summary>Creates a control wrapper dock.</summary>
    /// <param name="controlData">The control data.</param>
    /// <param name="isOrphaned">Whether the control data is orphaned.</param>
    /// <returns>The wrapper dock.</returns>
    protected RadDock CreateControlWrapperDock(ControlData controlData, bool isOrphaned = false)
    {
      RadDock dockWrapper = this.CreateDockWrapper();
      ControlData controlData1 = controlData;
      bool flag1 = false;
      if (controlData != null)
      {
        bool isTemplate = false;
        ControlData controlData2 = (ControlData) null;
        if (controlData is PageDraftControl || controlData is TemplateDraftControl || controlData is PageControl || controlData is Telerik.Sitefinity.Pages.Model.TemplateControl)
        {
          controlData2 = this.GetOverridenControlByDraftId(this.DraftId, controlData.Id, out isTemplate);
          if (controlData2 == null && controlData.PersonalizationMasterId != Guid.Empty)
          {
            ControlData controlByDraftId = this.GetOverridenControlByDraftId(this.DraftId, PageManager.GetManager().GetControl<ControlData>(controlData.PersonalizationMasterId).Id, out isTemplate);
            if (controlByDraftId != null)
              controlData2 = controlByDraftId.PersonalizedControls.FirstOrDefault<ControlData>((Func<ControlData, bool>) (c => c.PersonalizationSegmentId == controlData.PersonalizationSegmentId));
          }
        }
        dockWrapper.Title = controlData.Caption;
        dockWrapper.Attributes.Add("controlid", controlData.Id.ToString());
        dockWrapper.Attributes.Add("mastercontrolid", controlData.Id.ToString());
        dockWrapper.Attributes.Add("pageid", controlData.ContainerId.ToString());
        dockWrapper.Attributes.Add("controltype", controlData.ObjectType);
        Type type = TypeResolutionService.ResolveType(ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObjectType(controlData), false);
        if (type != (Type) null)
        {
          dockWrapper.Attributes.Add("behaviourobjecttype", type.FullName);
          flag1 = type.ImplementsInterface(typeof (IFormPageBreak));
          dockWrapper.Attributes.Add("isPageBreak", flag1.ToString());
          if (controlData is FormDraftControl formControl)
          {
            ControlProperty controlProperty = formControl.Properties.FirstOrDefault<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID"));
            if (controlProperty != null)
              dockWrapper.Attributes.Add("controlKey", controlProperty.Value);
            if (formControl.IsFieldHidden())
              dockWrapper.Attributes.Add("sf-hidden-field-label", Res.Get<FormsResources>().Hidden);
          }
        }
        if (controlData2 != null)
        {
          if (controlData is PageDraftControl)
            dockWrapper.CssClass += " EnableRevert ";
          controlData = controlData2;
        }
        string controlDesignerUrl = ZoneEditor.GetControlDesignerUrl(controlData);
        if (!controlDesignerUrl.IsNullOrEmpty())
          dockWrapper.Attributes.Add("designerUrl", this.Page.ResolveUrl(controlDesignerUrl));
        bool flag2 = !this.Proxy.IsBackend && this.MediaType == DesignMediaType.Page && ZoneEditor.OpenNewWidgetEditor(controlData);
        dockWrapper.Attributes.Add("openAdminAppEditor", flag2.ToString().ToLowerInvariant());
        foreach (KeyValuePair<string, string> controlPermission in ZoneEditor.GetControlPermissions(controlData, this.DraftId, new DesignMediaType?(this.MediaType)))
        {
          dockWrapper.ContentContainer.Attributes.Add(controlPermission.Key, controlPermission.Value);
          dockWrapper.Attributes.Add(controlPermission.Key, controlPermission.Value);
        }
      }
      string str1 = "rdTitleBar zeControlTitlebar";
      string str2 = "zeControlDock";
      if (isOrphaned)
      {
        str1 += " rdTitlebarOrphaned";
        str2 += " zeOrphanedDock";
      }
      if (flag1)
        str2 += " sfPageBreak";
      dockWrapper.TitlebarContainer.CssClass = str1;
      dockWrapper.TitlebarTemplate = (ITemplate) new ZoneEditor.WidgetDockTemplate();
      dockWrapper.CssClass = str2;
      dockWrapper.OnClientDragStart = "OnClientDragStart";
      dockWrapper.OnClientDragEnd = "OnClientDragEnd";
      if (controlData is TemplateDraftControl && ((TemplateDraftControl) controlData).Page.Id != this.DraftId || controlData is PageDraftControl && ((PageDraftControl) controlData).Page.Id != this.DraftId)
        dockWrapper.EnableDrag = false;
      return dockWrapper;
    }

    /// <summary>Creates a layout wrapper dock.</summary>
    /// <param name="controlData">The control data.</param>
    /// <returns></returns>
    protected RadDock CreateLayoutWrapperDock(ControlData controlData, bool isOrphaned = false)
    {
      RadDock dockWrapper = this.CreateDockWrapper();
      if (controlData != null)
      {
        dockWrapper.Title = controlData.Caption;
        System.Web.UI.AttributeCollection attributes1 = dockWrapper.Attributes;
        Guid guid = controlData.Id;
        string str1 = guid.ToString();
        attributes1.Add("controlid", str1);
        System.Web.UI.AttributeCollection attributes2 = dockWrapper.Attributes;
        guid = controlData.ContainerId;
        string str2 = guid.ToString();
        attributes2.Add("pageid", str2);
        string controlDesignerUrl = ZoneEditor.GetControlDesignerUrl(controlData);
        if (!controlDesignerUrl.IsNullOrEmpty())
          dockWrapper.Attributes.Add("designerUrl", this.Page.ResolveUrl(controlDesignerUrl));
        foreach (KeyValuePair<string, string> controlPermission in ZoneEditor.GetControlPermissions(controlData, this.DraftId, new DesignMediaType?(this.MediaType)))
        {
          dockWrapper.ContentContainer.Attributes.Add(controlPermission.Key, controlPermission.Value);
          dockWrapper.Attributes.Add(controlPermission.Key, controlPermission.Value);
        }
      }
      string str3 = "rdTitleBar zeLayoutTitlebar";
      string str4 = "zeLayoutDock";
      if (isOrphaned)
      {
        str3 += " rdTitlebarOrphaned";
        str4 += " zeOrphanedDock";
      }
      dockWrapper.TitlebarContainer.CssClass = str3;
      dockWrapper.CssClass = str4;
      dockWrapper.OnClientDragStart = "OnClientDragStart";
      dockWrapper.OnClientDragEnd = "OnClientDragEnd";
      return dockWrapper;
    }

    public static RadDock CreateLayoutWrapperDock(ControlData controlData, string skin)
    {
      RadDock dockWrapper = ZoneEditor.CreateDockWrapper(skin);
      if (controlData != null)
      {
        dockWrapper.Title = controlData.Caption;
        System.Web.UI.AttributeCollection attributes1 = dockWrapper.Attributes;
        Guid guid = controlData.Id;
        string str1 = guid.ToString();
        attributes1.Add("controlid", str1);
        System.Web.UI.AttributeCollection attributes2 = dockWrapper.Attributes;
        guid = controlData.ContainerId;
        string str2 = guid.ToString();
        attributes2.Add("pageid", str2);
        foreach (KeyValuePair<string, string> controlPermission in ZoneEditor.GetControlPermissions(controlData))
        {
          dockWrapper.ContentContainer.Attributes.Add(controlPermission.Key, controlPermission.Value);
          dockWrapper.Attributes.Add(controlPermission.Key, controlPermission.Value);
        }
      }
      dockWrapper.TitlebarContainer.CssClass = "rdTitleBar zeLayoutTitlebar";
      dockWrapper.CssClass = "zeLayoutDock";
      dockWrapper.OnClientDragStart = "OnClientDragStart";
      dockWrapper.OnClientDragEnd = "OnClientDragEnd";
      return dockWrapper;
    }

    public static Dictionary<string, string> GetControlPermissions(ControlData controlData)
    {
      string supportedPermissionSet = controlData.SupportedPermissionSets[0];
      Dictionary<string, string> controlPermissions = new Dictionary<string, string>();
      if (supportedPermissionSet == "Controls")
      {
        controlPermissions["perm_delete"] = controlData.IsGranted(supportedPermissionSet, "DeleteControl").ToString();
        controlPermissions["perm_indexchanged"] = controlData.IsGranted(supportedPermissionSet, "MoveControl").ToString();
        controlPermissions["perm_edit"] = controlData.IsGranted(supportedPermissionSet, "EditControlProperties").ToString();
        controlPermissions["perm_duplicate"] = controlData.IsGranted(supportedPermissionSet, "EditControlProperties").ToString();
        controlPermissions["perm_permissions"] = controlData.IsGranted(supportedPermissionSet, "ChangeControlPermissions").ToString();
        controlPermissions["perm_removePersonalizedVersion"] = controlData.IsGranted(supportedPermissionSet, "DeleteControl").ToString();
        Dictionary<string, string> dictionary = controlPermissions;
        int num;
        if (controlData.IsGranted(supportedPermissionSet, "EditControlProperties"))
          num = controlData.IsGranted(supportedPermissionSet, "ViewControl") ? 1 : 0;
        else
          num = 0;
        string str = (num != 0).ToString();
        dictionary["perm_addPersonalizedVersion"] = str;
        controlPermissions["perm_personalizationDropDownCommand"] = controlData.IsGranted(supportedPermissionSet, "ViewControl").ToString();
      }
      else
      {
        controlPermissions["perm_delete"] = controlData.IsGranted(supportedPermissionSet, "DeleteLayout").ToString();
        controlPermissions["perm_indexchanged"] = controlData.IsGranted(supportedPermissionSet, "MoveLayout").ToString();
        controlPermissions["perm_edit"] = controlData.IsGranted(supportedPermissionSet, "EditLayoutProperties").ToString();
        controlPermissions["perm_duplicate"] = controlData.IsGranted(supportedPermissionSet, "EditLayoutProperties").ToString();
        controlPermissions["perm_permissions"] = controlData.IsGranted(supportedPermissionSet, "ChangeLayoutPermissions").ToString();
        controlPermissions["perm_removePersonalizedVersion"] = controlData.IsGranted(supportedPermissionSet, "DeleteLayout").ToString();
        Dictionary<string, string> dictionary = controlPermissions;
        int num;
        if (controlData.IsGranted(supportedPermissionSet, "EditLayoutProperties"))
          num = controlData.IsGranted(supportedPermissionSet, "ViewLayout") ? 1 : 0;
        else
          num = 0;
        string str = (num != 0).ToString();
        dictionary["perm_addPersonalizedVersion"] = str;
        controlPermissions["perm_personalizationDropDownCommand"] = controlData.IsGranted(supportedPermissionSet, "ViewLayout").ToString();
      }
      return controlPermissions;
    }

    /// <summary>
    /// Get permissions for a control, as they would appear on as attributes on the client
    /// </summary>
    /// <param name="controlData">Control to get permissions for</param>
    /// <param name="page">The page Id.</param>
    /// <param name="mediaType">Indicates the type of the designed object.</param>
    /// <returns>
    /// Paris of client attribute name for permission and whether it is granted or not as a value
    /// </returns>
    public static Dictionary<string, string> GetControlPermissions(
      ControlData controlData,
      Guid page,
      DesignMediaType? mediaType = null)
    {
      PageManager manager = PageManager.GetManager();
      ControlData controlData1 = controlData;
      if (controlData1.PersonalizationMasterId != Guid.Empty)
      {
        switch (controlData)
        {
          case TemplateDraftControl _:
            controlData1 = (ControlData) manager.GetControl<TemplateDraftControl>(controlData.PersonalizationMasterId);
            break;
          case Telerik.Sitefinity.Pages.Model.TemplateControl _:
            controlData1 = (ControlData) manager.GetControl<Telerik.Sitefinity.Pages.Model.TemplateControl>(controlData.PersonalizationMasterId);
            break;
          default:
            controlData1 = manager.GetControl<ControlData>(controlData.PersonalizationMasterId);
            break;
        }
      }
      Dictionary<string, string> controlPermissions = ZoneEditor.GetControlPermissions(controlData1);
      bool flag;
      if (!controlData.IsLayoutControl)
      {
        if (controlData1.PersonalizationSegmentId != Guid.Empty || controlData1 is TemplateDraftControl && ((TemplateDraftControl) controlData1).Page.Id == page || controlData1 is PageDraftControl && ((PageDraftControl) controlData1).Page.Id == page)
        {
          if (controlData1.Editable && controlData1.IsOverridedControl)
          {
            controlPermissions["perm_widgetDisableOverride"] = true.ToString();
            controlPermissions["perm_displayOverridenControls"] = (controlData1.BaseControlId == Guid.Empty).ToString();
            controlPermissions["perm_widgetOverride"] = false.ToString();
            controlPermissions["perm_beforedelete"] = true.ToString();
          }
          else
          {
            controlPermissions["perm_widgetOverride"] = false.ToString();
            if (controlData1 is TemplateDraftControl)
            {
              TemplateDraftControl templateDraftControl = controlData1 as TemplateDraftControl;
              Dictionary<string, string> dictionary = controlPermissions;
              flag = templateDraftControl.Page.Id == page && templateDraftControl.BaseControlId == Guid.Empty;
              string str = flag.ToString();
              dictionary["perm_widgetOverride"] = str;
            }
            Dictionary<string, string> dictionary1 = controlPermissions;
            flag = false;
            string str1 = flag.ToString();
            dictionary1["perm_widgetDisableOverride"] = str1;
            Dictionary<string, string> dictionary2 = controlPermissions;
            flag = false;
            string str2 = flag.ToString();
            dictionary2["perm_displayOverridenControls"] = str2;
            Dictionary<string, string> dictionary3 = controlPermissions;
            flag = true;
            string str3 = flag.ToString();
            dictionary3["perm_beforedelete"] = str3;
            Dictionary<string, string> dictionary4 = controlPermissions;
            flag = !bool.Parse(controlPermissions["perm_displayOverridenControls"]);
            string str4 = flag.ToString();
            dictionary4["perm_displayWidgetOverrideText"] = str4;
          }
          Dictionary<string, string> dictionary5 = controlPermissions;
          flag = controlData1.BaseControlId != Guid.Empty;
          string str5 = flag.ToString();
          dictionary5["perm_displayWidgetOverrideText"] = str5;
          Dictionary<string, string> dictionary6 = controlPermissions;
          flag = false;
          string str6 = flag.ToString();
          dictionary6["perm_rollback"] = str6;
        }
        else if (controlData1 is FormDraftControl)
        {
          controlPermissions["perm_beforedelete"] = true.ToString();
          Dictionary<string, string> dictionary7 = controlPermissions;
          flag = false;
          string str7 = flag.ToString();
          dictionary7["perm_displayWidgetOverrideText"] = str7;
          Dictionary<string, string> dictionary8 = controlPermissions;
          flag = false;
          string str8 = flag.ToString();
          dictionary8["perm_widgetDisableOverride"] = str8;
          Dictionary<string, string> dictionary9 = controlPermissions;
          flag = false;
          string str9 = flag.ToString();
          dictionary9["perm_widgetOverride"] = str9;
          Dictionary<string, string> dictionary10 = controlPermissions;
          flag = false;
          string str10 = flag.ToString();
          dictionary10["perm_displayOverridenControls"] = str10;
        }
        if (controlData1.BaseControlId != Guid.Empty)
        {
          Dictionary<string, string> dictionary11 = controlPermissions;
          flag = false;
          string str11 = flag.ToString();
          dictionary11["perm_indexchanged"] = str11;
          Dictionary<string, string> dictionary12 = controlPermissions;
          flag = false;
          string str12 = flag.ToString();
          dictionary12["perm_beforedelete"] = str12;
          Dictionary<string, string> dictionary13 = controlPermissions;
          flag = false;
          string str13 = flag.ToString();
          dictionary13["perm_rollback"] = str13;
        }
        if (controlData1.Editable && controlData1 is Telerik.Sitefinity.Pages.Model.TemplateControl && ((Telerik.Sitefinity.Pages.Model.TemplateControl) controlData1).Page.Id != page)
        {
          Dictionary<string, string> dictionary = controlPermissions;
          flag = true;
          string str = flag.ToString();
          dictionary["perm_displayWidgetOverrideText"] = str;
        }
        if (controlData1.Editable && controlData1.BaseControlId != Guid.Empty)
        {
          Dictionary<string, string> dictionary14 = controlPermissions;
          flag = controlData.PersonalizationMasterId == Guid.Empty;
          string str14 = flag.ToString();
          dictionary14["perm_rollback"] = str14;
          Dictionary<string, string> dictionary15 = controlPermissions;
          flag = false;
          string str15 = flag.ToString();
          dictionary15["perm_indexchanged"] = str15;
          Dictionary<string, string> dictionary16 = controlPermissions;
          flag = false;
          string str16 = flag.ToString();
          dictionary16["perm_beforedelete"] = str16;
          Dictionary<string, string> dictionary17 = controlPermissions;
          flag = false;
          string str17 = flag.ToString();
          dictionary17["permissions"] = str17;
          Dictionary<string, string> dictionary18 = controlPermissions;
          flag = false;
          string str18 = flag.ToString();
          dictionary18["perm_widgetDisableOverride"] = str18;
          Dictionary<string, string> dictionary19 = controlPermissions;
          flag = false;
          string str19 = flag.ToString();
          dictionary19["perm_displayOverridenControls"] = str19;
        }
        if (controlData1.BaseControlId != Guid.Empty && (controlData1 is PageDraftControl || controlData1 is TemplateDraftControl))
        {
          Dictionary<string, string> dictionary20 = controlPermissions;
          flag = false;
          string str20 = flag.ToString();
          dictionary20["perm_indexchanged"] = str20;
          Dictionary<string, string> dictionary21 = controlPermissions;
          flag = false;
          string str21 = flag.ToString();
          dictionary21["perm_beforedelete"] = str21;
          Dictionary<string, string> dictionary22 = controlPermissions;
          flag = false;
          string str22 = flag.ToString();
          dictionary22["perm_widgetOverride"] = str22;
          Dictionary<string, string> dictionary23 = controlPermissions;
          flag = controlData.PersonalizationMasterId == Guid.Empty;
          string str23 = flag.ToString();
          dictionary23["perm_rollback"] = str23;
        }
        if (controlData1.Editable && controlData1 is Telerik.Sitefinity.Pages.Model.TemplateControl && controlData1.BaseControlId == Guid.Empty)
        {
          Dictionary<string, string> dictionary24 = controlPermissions;
          flag = false;
          string str24 = flag.ToString();
          dictionary24["perm_indexchanged"] = str24;
          Dictionary<string, string> dictionary25 = controlPermissions;
          flag = false;
          string str25 = flag.ToString();
          dictionary25["perm_beforedelete"] = str25;
          Dictionary<string, string> dictionary26 = controlPermissions;
          flag = false;
          string str26 = flag.ToString();
          dictionary26["perm_widgetOverride"] = str26;
          Dictionary<string, string> dictionary27 = controlPermissions;
          flag = false;
          string str27 = flag.ToString();
          dictionary27["perm_widgetDisableOverride"] = str27;
          Dictionary<string, string> dictionary28 = controlPermissions;
          flag = false;
          string str28 = flag.ToString();
          dictionary28["perm_displayOverridenControls"] = str28;
          Dictionary<string, string> dictionary29 = controlPermissions;
          flag = false;
          string str29 = flag.ToString();
          dictionary29["perm_rollback"] = str29;
        }
      }
      else
      {
        Dictionary<string, string> dictionary = controlPermissions;
        flag = true;
        string str = flag.ToString();
        dictionary["perm_beforedelete"] = str;
      }
      if (mediaType.HasValue)
      {
        DesignMediaType? nullable = mediaType;
        DesignMediaType designMediaType1 = DesignMediaType.Page;
        if (!(nullable.GetValueOrDefault() == designMediaType1 & nullable.HasValue))
        {
          nullable = mediaType;
          DesignMediaType designMediaType2 = DesignMediaType.Template;
          if (!(nullable.GetValueOrDefault() == designMediaType2 & nullable.HasValue))
            goto label_32;
        }
        if (SystemManager.IsModuleAccessible("Personalization") && ZoneEditor.ImplementsInterface<IPersonalizable>(controlData))
        {
          if (controlPermissions.ContainsKey("perm_removePersonalizedVersion") && controlPermissions["perm_removePersonalizedVersion"].ToLower() == "true")
          {
            Dictionary<string, string> dictionary = controlPermissions;
            flag = controlData.PersonalizationMasterId != Guid.Empty;
            string str = flag.ToString();
            dictionary["perm_removePersonalizedVersion"] = str;
            goto label_33;
          }
          else
            goto label_33;
        }
      }
label_32:
      Dictionary<string, string> dictionary30 = controlPermissions;
      flag = false;
      string str30 = flag.ToString();
      dictionary30["perm_removePersonalizedVersion"] = str30;
      Dictionary<string, string> dictionary31 = controlPermissions;
      flag = false;
      string str31 = flag.ToString();
      dictionary31["perm_addPersonalizedVersion"] = str31;
      Dictionary<string, string> dictionary32 = controlPermissions;
      flag = false;
      string str32 = flag.ToString();
      dictionary32["perm_personalizationDropDownCommand"] = str32;
label_33:
      return controlPermissions;
    }

    /// <summary>Creates the wrapper dock factory.</summary>
    /// <returns></returns>
    protected RadDock CreateDockWrapper(bool isOverridenControl = false) => ZoneEditor.CreateDockWrapper(this.Skin, isOverridenControl);

    public static RadDock CreateDockWrapper(string skin, bool isOverridenControl = false)
    {
      RadDock dock = new RadDock();
      dock.Skin = skin;
      dock.DockMode = DockMode.Docked;
      ZoneEditor.AddCommandsToDockStatic(dock);
      dock.EnableRoundedCorners = false;
      dock.Resizable = false;
      return dock;
    }

    /// <summary>Adds the commands to dock.</summary>
    /// <param name="dock">The dock.</param>
    protected virtual void AddCommandsToDock(RadDock dock) => ZoneEditor.AddCommandsToDockStatic(dock);

    public static void AddCommandsToDockStatic(RadDock dock)
    {
      DockToggleCommand command = new DockToggleCommand();
      command.Name = "dropDownCommand";
      command.CssClass = "rdMoreCommand";
      command.Text = Res.Get<PageResources>().ZoneEditorMoreCommand;
      command.AlternateText = command.Text;
      dock.Commands.Add((DockCommand) command);
      dock.Commands.Add(new DockCommand()
      {
        CssClass = "rdEditCommand",
        Name = "edit",
        Text = Res.Get<PageResources>().ZoneEditorEditCommand
      });
    }

    /// <summary>Implements the interface.</summary>
    /// <typeparam name="T">The type of the T.</typeparam>
    /// <param name="controlData">The control data.</param>
    /// <returns></returns>
    internal static bool ImplementsInterface<T>(ControlData controlData)
    {
      Type controlType = ZoneEditor.GetControlType(controlData);
      return !(controlType == (Type) null) && controlType.ImplementsInterface(typeof (T));
    }

    internal static string GetOverridedControlTitle(ControlData controlData, string clientID)
    {
      string overridedControlTitle = string.Format(Res.Get<PageResources>().ZoneEditorEditableInPagesWithoutALinkLabel, (object) clientID);
      if (controlData != null && controlData.IsOverridedControl && controlData.OriginalControlId != Guid.Empty)
      {
        List<KeyValuePair<PageData, bool>> widgetChangedPages = PageTemplateViewModel.GetOverridenWidgetChangedPages(controlData);
        if (widgetChangedPages.Count > 0)
          overridedControlTitle = string.Format(Res.Get<PageResources>().ZoneEditorEditableInPagesLabel, (object) clientID, (object) widgetChangedPages.Count);
      }
      return overridedControlTitle;
    }

    internal Control LayoutLinks
    {
      get
      {
        if (this.layoutLinks == null)
          this.layoutLinks = (Control) new Literal()
          {
            Text = "\r\n<div class='sftbFAQ sftbLayouts'>\r\n</div>"
          };
        return this.layoutLinks;
      }
    }

    internal Control WidgetLinks
    {
      get
      {
        if (this.widgetLinks == null)
          this.widgetLinks = (Control) new Literal()
          {
            Text = "\r\n<div class='sftbFAQ sftbGadgets'>\r\n</div>\r\n"
          };
        return this.widgetLinks;
      }
    }

    internal Control ThemesLinks
    {
      get
      {
        if (this.themesLinks == null)
          this.themesLinks = (Control) new Literal()
          {
            Text = string.Format("\r\n<div class='sftbFAQ sftbThemes'></div>\r\n", (object) Res.Get<PageResources>().OpenInNewWindow, (object) Res.Get<PageResources>().WhatAreThemes, (object) Res.Get<PageResources>().HowToUploadMyOwnTheme)
          };
        return this.themesLinks;
      }
    }

    internal Control SettingsLinks
    {
      get
      {
        if (this.settingsLinks == null)
          this.settingsLinks = (Control) new Literal()
          {
            Text = "\r\n<div class='sftbFAQ sftbGadgets'>\r\n</div>\r\n"
          };
        return this.settingsLinks;
      }
    }

    /// <summary>
    /// Called to provide for adding a Layout [control] instance to the page.
    /// </summary>
    /// <param name="dockContentContainer">The [dock] content container.</param>
    /// <param name="controlData">The control data.</param>
    protected virtual ZoneEditorLayoutEventArgs OnLayoutControlAdd(
      Control dockContentContainer,
      ControlData controlData)
    {
      EventHandler<ZoneEditorLayoutEventArgs> eventHandler = (EventHandler<ZoneEditorLayoutEventArgs>) this.Events[ZoneEditor.ZoneEditorLayoutEvent];
      if (eventHandler == null)
        return new ZoneEditorLayoutEventArgs();
      ZoneEditorLayoutEventArgs e = new ZoneEditorLayoutEventArgs(dockContentContainer, controlData);
      eventHandler((object) this, e);
      if (e.LayoutControl != null)
      {
        LayoutControl layoutControl = e.LayoutControl;
        if (controlData is StaticControlData)
        {
          this._placeholderControls.AddRange((IEnumerable<Control>) layoutControl.Placeholders);
        }
        else
        {
          LayoutControlDataPermissions controlDataPermissions = LayoutControlDataPermissions.Create(controlData);
          foreach (Control placeholder in (Collection<Control>) layoutControl.Placeholders)
          {
            this.placeholderPermissions[placeholder.ID] = controlDataPermissions;
            this._placeholderControls.Add(placeholder);
          }
        }
      }
      return e;
    }

    /// <summary>
    /// Raised when a Layout [control] must be added to the zone editor.
    /// </summary>
    public event EventHandler<ZoneEditorLayoutEventArgs> LayoutControlAdd
    {
      add => this.Events.AddHandler(ZoneEditor.ZoneEditorLayoutEvent, (Delegate) value);
      remove => this.Events.RemoveHandler(ZoneEditor.ZoneEditorLayoutEvent, (Delegate) value);
    }

    /// <summary>
    /// Called to provide for adding a control instance to a content container.
    /// </summary>
    /// <param name="dockContentContainer">The [dock] content container.</param>
    /// <param name="controlData">The control data.</param>
    protected virtual ZoneEditorEventArgs OnControlAdd(
      Control dockContentContainer,
      ControlData controlData)
    {
      EventHandler<ZoneEditorEventArgs> eventHandler = (EventHandler<ZoneEditorEventArgs>) this.Events[ZoneEditor.ZoneEditorEvent];
      ZoneEditorEventArgs e = new ZoneEditorEventArgs(dockContentContainer, controlData);
      if (eventHandler != null)
        eventHandler((object) this, e);
      return e;
    }

    /// <summary>
    /// Raised when a control must be added to the zone editor.
    /// </summary>
    public event EventHandler<ZoneEditorEventArgs> ControlAdd
    {
      add => this.Events.AddHandler(ZoneEditor.ZoneEditorEvent, (Delegate) value);
      remove => this.Events.RemoveHandler(ZoneEditor.ZoneEditorEvent, (Delegate) value);
    }

    internal class ToolboxDockTemplate : ITemplate
    {
      private string cssClass = string.Empty;

      public ToolboxDockTemplate(string cssClass) => this.cssClass = cssClass;

      public void InstantiateIn(Control container)
      {
        HtmlGenericControl child = new HtmlGenericControl("div");
        child.Attributes["class"] = this.cssClass;
        string title = (container.NamingContainer as RadDock).Title;
        child.Controls.Add((Control) new LiteralControl(title));
        container.Controls.Add((Control) child);
      }
    }

    internal class WidgetDockTemplate : ITemplate
    {
      public void InstantiateIn(Control container)
      {
        HtmlGenericControl child1 = new HtmlGenericControl("em");
        RadDock namingContainer = container.NamingContainer as RadDock;
        child1.Controls.Add((Control) new LiteralControl(namingContainer.Title));
        container.Controls.Add((Control) child1);
        HtmlGenericControl child2 = new HtmlGenericControl("span");
        child2.Attributes["class"] = "personalizationDropDownCommand";
        child2.Attributes["style"] = "cursor:pointer;";
        child2.InnerHtml = string.Empty;
        container.Controls.Add((Control) child2);
      }
    }

    internal class AttributeNames
    {
      internal const string PlaceholderId = "placeholderid";
      internal const string PlaceholderLabel = "data-placeholder-label";
      internal const string HiddenFieldLabel = "sf-hidden-field-label";
    }

    internal class ToolboxFilterContext : IToolboxFilterContext
    {
      private DesignMediaType mediaType;
      private string containerId;

      public string ContainerId
      {
        get => this.containerId;
        private set => this.containerId = value;
      }

      public DesignMediaType MediaType
      {
        get => this.mediaType;
        private set => this.mediaType = value;
      }

      public ToolboxFilterContext(DesignMediaType mediaType, string containerId)
      {
        this.MediaType = mediaType;
        this.ContainerId = containerId;
      }
    }
  }
}
