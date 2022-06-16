// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Represents the language tool bar in page editor.</summary>
  public class LanguageToolBar : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.LanguageToolBar.ascx");
    private Telerik.Web.UI.RadWindow radWindow;
    private string dialogUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.LanguageToolBar" /> class.
    /// </summary>
    public LanguageToolBar() => this.LayoutTemplatePath = LanguageToolBar.layoutTemplatePath;

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the url of the dialog displayed when adding language versions.
    /// </summary>
    public string AddLanguageVersionDialogUrl
    {
      get
      {
        if (this.dialogUrl == null)
        {
          string str1;
          string str2;
          if (this.MediaType == DesignMediaType.Template)
          {
            str1 = this.IsBackend ? "BackendPageTemplates" : "FrontendPageTemplates";
            str2 = this.IsBackend ? "BackendPageTemplatesCreate" : "FrontendPageTemplatesCreate";
          }
          else
          {
            str1 = this.IsBackend ? "BackendPages" : "FrontendPages";
            str2 = this.IsBackend ? "BackendPagesCreate" : "FrontendPagesCreate";
          }
          string str3 = "?ControlDefinitionName=" + str1 + "&ViewName=" + str2;
          this.dialogUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Dialog/" + typeof (ContentViewInsertDialog).Name) + str3;
        }
        return this.dialogUrl;
      }
    }

    /// <summary>
    /// Gets or sets the ID of the current object(PageData/FormDescription).
    /// </summary>
    /// <value>The draft ID.</value>
    public Guid CurrentObjectId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this page is backend.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this page is backend; otherwise, <c>false</c>.
    /// </value>
    [DefaultValue(false)]
    public bool IsBackend { get; set; }

    public bool CanStopSync { get; set; }

    /// <summary>
    /// Gets or sets the base edit URL. Language code is appended to this url in order to redirect user to
    /// editing the object in specific language.
    /// </summary>
    /// <value>The base edit URL.</value>
    public string BaseEditUrl { get; set; }

    /// <summary>
    /// Gets a value indicating whether this object(page or form) has versions for each language or all language versions use a single object.
    /// </summary>
    public bool IsSplitByLanguage => this.Proxy.IsSplitByLanguage;

    /// <summary>
    /// Gets or sets the type of the media in which the control is located - page, template etc.
    /// </summary>
    /// <value>The type of the media in which the control is located.</value>
    public DesignMediaType MediaType { get; set; }

    internal DraftProxyBase Proxy { get; set; }

    /// <summary>Gets the toolbar wrapper.</summary>
    /// <value>The toolbar wrapper.</value>
    public HtmlGenericControl ToolbarWrapper => this.Container.GetControl<HtmlGenericControl>("toolbarWrapper", false);

    public HtmlGenericControl LanguagesPanel => this.Container.GetControl<HtmlGenericControl>("languagesPanel", false);

    public LanguageListControl LanguagesListSplit => this.Container.GetControl<LanguageListControl>("languagesListSplit", false);

    public LanguageListControl LanguagesListSynced => this.Container.GetControl<LanguageListControl>("languagesListSynced", false);

    public HtmlControl ShowTranslationsButton => this.Container.GetControl<HtmlControl>("showTranslationsButton", false);

    public HtmlControl StopSyncButton => this.Container.GetControl<HtmlControl>("stopSyncingButton", false);

    public LanguageListControl CurrentLanguageList => !this.IsSplitByLanguage ? this.LanguagesListSynced : this.LanguagesListSplit;

    public PromptDialog StopSyncWarningDialog => this.Container.GetControl<PromptDialog>("stopSyncWarningDialog", false);

    public Label WarningLabel => this.Container.GetControl<Label>("warningLabel", true);

    public HtmlControl CommandsWrapper => this.Container.GetControl<HtmlControl>("commandsWrapper", true);

    /// <summary>
    /// Gets a reference to the window manager on the template.
    /// </summary>
    /// <value>The window manager.</value>
    private ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The dialog container.</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      if (!this.IsSplitByLanguage)
      {
        if (this.Proxy.UsedLanguages.Count > 1)
        {
          this.LanguagesListSynced.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
          if (this.MediaType == DesignMediaType.Template)
            this.WarningLabel.Text = Res.Get<Labels>().Get("TemplateTranslationsAreSynchronized");
        }
        else
        {
          this.CommandsWrapper.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
          this.WarningLabel.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
        }
        if (!this.CanStopSync)
          this.StopSyncButton.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
      }
      this.CurrentLanguageList.LanguagesInUse = this.Proxy.UsedLanguages;
      this.CurrentLanguageList.LanguageSource = !this.IsBackend ? LanguageSource.Frontend : LanguageSource.Backend;
      if (this.Proxy.CurrentObjectCulture != null)
        this.CurrentLanguageList.ExcludedLanguages = new List<CultureInfo>()
        {
          this.Proxy.CurrentObjectCulture
        };
      Telerik.Web.UI.RadWindow child = new Telerik.Web.UI.RadWindow();
      child.Skin = "Default";
      child.ReloadOnShow = true;
      child.VisibleTitlebar = false;
      child.VisibleStatusbar = false;
      child.ShowContentDuringLoad = false;
      child.ID = "defaultWindow";
      this.radWindow = child;
      this.Controls.Add((Control) child);
    }

    /// <summary>
    /// Overridden. Calls Evaluate on the conditional template container to correctly use the controls inside of the templates
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    protected internal override GenericContainer CreateContainer(ITemplate template)
    {
      GenericContainer container = base.CreateContainer(template);
      Dictionary<string, Control> controls = container.GetControls<ConditionalTemplateContainer>();
      if (controls != null)
      {
        foreach (KeyValuePair<string, Control> keyValuePair in controls)
          ((ConditionalTemplateContainer) keyValuePair.Value).Evaluate((object) this);
      }
      return container;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      string str1 = this.Proxy.MediaType == DesignMediaType.Page ? this.ResolveUrl("~/Sitefinity/Services/Pages/ZoneEditorService.svc/") : this.ResolveUrl("~/Sitefinity/Services/Forms/FormsService.svc/");
      behaviorDescriptor.AddProperty("serviceUrl", (object) str1);
      behaviorDescriptor.AddProperty("_baseItemServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/"));
      behaviorDescriptor.AddProperty("mediaType", (object) this.Proxy.MediaType);
      behaviorDescriptor.AddComponentProperty("radWindow", this.radWindow.ClientID);
      behaviorDescriptor.AddComponentProperty("stopSyncWarningDialog", this.StopSyncWarningDialog.ClientID);
      if (this.Proxy is Telerik.Sitefinity.Modules.Pages.PageDraftProxy proxy)
      {
        PageNode pageNode = proxy.PageNode;
        behaviorDescriptor.AddProperty("isInSplitMode", (object) (bool) (pageNode == null ? 0 : (pageNode.LocalizationStrategy == LocalizationStrategy.Split ? 1 : 0)));
        if (pageNode != null)
        {
          JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          foreach (string availableLanguage in pageNode.AvailableLanguages)
          {
            string str2 = UrlPath.ResolveUrl(pageNode.GetBackendUrl("Edit", CultureInfo.GetCultureInfo(availableLanguage)));
            dictionary.Add(availableLanguage, str2);
          }
          behaviorDescriptor.AddProperty("itemLanguageUrls", (object) scriptSerializer.Serialize((object) dictionary));
        }
      }
      else
        behaviorDescriptor.AddProperty("isInSplitMode", (object) false);
      behaviorDescriptor.AddProperty("isSplitByLanguage", (object) this.IsSplitByLanguage);
      behaviorDescriptor.AddProperty("objectDataId", (object) this.CurrentObjectId);
      behaviorDescriptor.AddProperty("draftId", (object) this.Proxy.PageDraftId);
      behaviorDescriptor.AddProperty("dialogUrl", (object) this.AddLanguageVersionDialogUrl);
      behaviorDescriptor.AddProperty("baseEditUrl", (object) this.BaseEditUrl);
      if (this.Proxy.MediaType == DesignMediaType.Page || this.Proxy.MediaType == DesignMediaType.Template)
        behaviorDescriptor.AddProperty("_backLabelTextForAddLanguage", (object) Res.Get<PageResources>().BackToEdit);
      behaviorDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      behaviorDescriptor.AddComponentProperty("languagesList", this.CurrentLanguageList.ClientID);
      if (!this.IsSplitByLanguage)
      {
        behaviorDescriptor.AddElementProperty("stopSyncButton", this.StopSyncButton.ClientID);
        behaviorDescriptor.AddElementProperty("showTranslationsButton", this.ShowTranslationsButton.ClientID);
      }
      behaviorDescriptor.AddElementProperty("toolbarWrapper", this.ToolbarWrapper.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string str = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      });
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.LanguageToolBar.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
