// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PropertyEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Represents dialog control for editing control properties.
  /// </summary>
  public class PropertyEditor : AjaxDialogBase
  {
    protected bool implementsDesigner;
    private const string propertyEditorScriptPath = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.PropertyEditor.js";
    private const string clientManagerScriptPath = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    private const string propertiesServiceUrl = "~/Sitefinity/Services/Pages/ControlPropertyService.svc/batch/{0}/";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.PropertyEditor.ascx");
    private ControlDesignerBase controlDesigner;
    private object control;
    private ControlData controlData;
    private string saveButtonTitle;
    private bool showProvidersSelector;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PropertyEditor" /> class.
    /// </summary>
    public PropertyEditor() => this.LayoutTemplatePath = PropertyEditor.layoutTemplatePath;

    /// <summary>
    ///  If set to true the the buttons that switch to advanced/simple mode are hidden
    /// </summary>
    public virtual bool HideAdvancedMode { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the control id of the control for which the properties are being edited.
    /// </summary>
    protected Guid ControlId { get; set; }

    /// <summary>
    /// Gets or sets the id of the page which contains the control that being is edited
    /// </summary>
    public Guid PageId { get; set; }

    /// <summary>
    /// Specifies the media type of the content (pages/templates/forms) that is being edited
    /// </summary>
    protected DesignMediaType MediaType { get; set; }

    /// <summary>Gets or sets the name of page provider</summary>
    protected string PageProviderName { get; set; }

    /// <summary>
    /// Represents the culture that will be used to save the values from the designer in multilingual mode
    /// </summary>
    public string PropertyValuesCulture { get; set; }

    /// <summary>
    /// Specifies whether to check the live version of the container for locking or the draft
    /// </summary>
    public string CheckLiveVersion { get; set; }

    public string UpgradePageVersion { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => this.GetType().FullName;

    public bool OpenedByBrowseAndEdit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the ProvidersSelector control will be displayed.
    /// False by default.
    /// </summary>
    public bool ShowProvidersSelector
    {
      get => this.showProvidersSelector;
      set
      {
        this.showProvidersSelector = value;
        this.ProvidersSelector.Visible = value;
      }
    }

    /// <summary>
    /// Gets the reference to the multi page which holds the user interfaces for simple and advanced modes
    /// </summary>
    protected virtual RadMultiPage ModesMultiPage => this.Container.GetControl<RadMultiPage>("modesMultiPage", true);

    /// <summary>
    /// Gets the reference for the page which holds the simple mode user interface
    /// </summary>
    protected virtual RadPageView SimpleModeView => this.Container.GetControl<RadPageView>("simpleModeView", true);

    /// <summary>
    /// Gets the reference for the page which holds the advanced mode user interface
    /// </summary>
    protected virtual RadPageView AdvancedModeView => this.Container.GetControl<RadPageView>("advancedModeView", true);

    /// <summary>
    /// Gets the reference to the html control that represents the save button
    /// </summary>
    protected virtual HtmlAnchor SaveButton => this.Container.GetControl<HtmlAnchor>("saveButton", true);

    /// <summary>
    /// Gets the reference to the html control that represents the cancel button
    /// </summary>
    protected virtual HtmlAnchor CancelButton => this.Container.GetControl<HtmlAnchor>("cancelButton", true);

    /// <summary>
    /// Gets the reference to the html control that represents the cancel button
    /// </summary>
    protected virtual HtmlAnchor OkButton => this.Container.GetControl<HtmlAnchor>("okButton", true);

    /// <summary>
    /// Gets the reference to the html control that represents the cancel button
    /// </summary>
    protected virtual Literal OrLiteral => this.Container.GetControl<Literal>("orLiteral", true);

    /// <summary>
    /// Gets the reference to the link button that switches the editor into the advanced mode.
    /// </summary>
    protected virtual LinkButton AdvancedModeButton => this.Container.GetControl<LinkButton>("advancedModeButton", true);

    /// <summary>
    /// Gets the reference to the link button that switches the editor into the simple mode.
    /// </summary>
    protected virtual LinkButton SimpleModeButton => this.Container.GetControl<LinkButton>("simpleModeButton", true);

    /// <summary>
    /// Gets the reference to the property grid control used to edit control properties
    /// in advanced mode.
    /// </summary>
    protected virtual PropertyGrid PropertyGrid => this.Container.GetControl<PropertyGrid>("propertyGrid", true);

    /// <summary>
    /// Gets the control that is used to display the title in the property editor
    /// </summary>
    protected virtual ITextControl TitleLiteral => this.Container.GetControl<ITextControl>("titleLiteral", true);

    internal PromptDialog SaveConfirmationDialog => this.Container.GetControl<PromptDialog>("saveConfirmationDialog", false);

    /// <summary>
    /// Gets or sets the reference to the control designer for the currently edited control.
    /// </summary>
    /// <remarks>
    /// In case that the control that is being edited does not support control designers, this
    /// property will retrun null.
    /// </remarks>
    protected virtual ControlDesignerBase ControlDesigner
    {
      get => this.controlDesigner;
      set => this.controlDesigner = value;
    }

    /// <summary>
    /// Gets the control that is used to display the title in the property editor
    /// </summary>
    protected virtual ITextControl SaveButtonLiteral => this.Container.GetControl<ITextControl>("saveLiteral", true);

    /// <summary>
    /// Gets the instance of the actual control of language settings container.
    /// </summary>
    protected HtmlAnchor SaveAllTranslationsButton => this.Container.GetControl<HtmlAnchor>("saveAllTranslationsButton", false);

    /// <summary>
    /// Gets the instance of the actual control of the language settings section.
    /// </summary>
    protected HtmlGenericControl LanguageSettingsSection => this.Container.GetControl<HtmlGenericControl>("languageSettingsSection", true);

    /// <summary>
    /// Gets the instance of the actual control of the language settings link.
    /// </summary>
    protected System.Web.UI.Control LanguageSettingsLink => this.Container.GetControl<System.Web.UI.Control>("languageSettingsLink", true);

    /// <summary>
    /// Gets the instance of the element that holds the title of the property Editor.
    /// </summary>
    protected HtmlGenericControl TitleElement => this.Container.GetControl<HtmlGenericControl>("titleElement", false);

    /// <summary>
    /// Gets or sets the instance of the actual control that the designer is modifing.
    /// </summary>
    /// <value>The control.</value>
    public object Control
    {
      get => this.control;
      set => this.control = value;
    }

    /// <summary>
    /// Gets or sets the designed control ControlData(the persisted properties).
    /// </summary>
    /// <value>The Control  ControlData.</value>
    public ControlData ControlData => this.controlData;

    /// <summary>Gets or sets the title of the property editor dialog</summary>
    public string TitleLiteralText
    {
      get => this.TitleLiteral.Text;
      set => this.TitleLiteral.Text = value;
    }

    /// <summary>Specifies the title of the save button</summary>
    public string SaveButtonTitle
    {
      get
      {
        if (string.IsNullOrEmpty(this.saveButtonTitle))
          this.saveButtonTitle = Res.Get<Labels>().Save;
        return this.saveButtonTitle;
      }
      set
      {
        if (this.SaveButtonLiteral != null)
          this.SaveButtonLiteral.Text = value;
        this.saveButtonTitle = value;
      }
    }

    /// <summary>Gets the instance of the provider selector control.</summary>
    protected ProvidersSelector ProvidersSelector => this.Container.GetControl<ProvidersSelector>("providersSelector", false);

    /// <summary>
    /// Gets the instance of the element that holds the or literal.
    /// </summary>
    protected HtmlGenericControl OrLabel => this.Container.GetControl<HtmlGenericControl>("orLabel", false);

    protected override void OnInit(EventArgs e)
    {
      if (FormManager.GetCurrent(this.Page) == null)
        this.Controls.Add((System.Web.UI.Control) new FormManager());
      base.OnInit(e);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      this.InitiliazeRequestProperties();
      IControlManager controlManager = this.GetControlManager(this.MediaType);
      this.controlData = this.GetControlData(this.ControlId, this.PageId, controlManager);
      CultureInfo culture = string.IsNullOrEmpty(this.PropertyValuesCulture) ? (CultureInfo) null : CultureInfo.GetCultureInfo(this.PropertyValuesCulture);
      int languageFallbackMode = (int) SystemManager.RequestLanguageFallbackMode;
      SystemManager.RequestLanguageFallbackMode = FallbackMode.NoFallback;
      this.control = (object) controlManager.LoadControl((ObjectData) this.controlData, culture);
      object[] customAttributes = this.control.GetType().GetCustomAttributes(typeof (PropertyEditorTitleAttribute), true);
      if (customAttributes.Length != 0)
        this.TitleLiteral.Text = ((PropertyEditorTitleAttribute) customAttributes[0]).PropertyEditorTitle;
      SystemManager.RequestLanguageFallbackMode = (FallbackMode) languageFallbackMode;
      if (this.HideAdvancedMode)
        this.DisableAdvancedMode();
      if (this.SaveButtonLiteral != null)
        this.SaveButtonLiteral.Text = this.SaveButtonTitle;
      if (this.control is ILicensedControl control && !control.IsLicensed)
      {
        this.HideAdvancedMode = true;
        if (this.SaveButton != null)
          this.SaveButton.Style.Add("display", "none");
        this.CancelButton.Style.Add("display", "none");
        this.OrLiteral.Text = "";
        string str = control.LicensingMessage ?? Res.Get<LicensingMessages>().WidgetNotLicensed;
        this.SimpleModeView.Controls.Add((System.Web.UI.Control) new Label()
        {
          Text = str
        });
      }
      else
      {
        if (this.OkButton != null)
          this.OkButton.Style.Add("display", "none");
        this.PrepareControlDesigner(this.controlData);
      }
      this.PropertyGrid.PropertyEditor = this;
      SystemManager.CurrentHttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
      if (!SystemManager.CurrentContext.AppSettings.Multilingual || this.SaveAllTranslationsButton == null)
        return;
      this.SaveAllTranslationsButton.Visible = true;
      string str1 = this.Page.Request.QueryString["hideSaveAllTranslations"];
      bool flag = false;
      ref bool local = ref flag;
      bool.TryParse(str1, out local);
      if (!flag)
        return;
      this.SaveAllTranslationsButton.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
    }

    /// <summary>Disables the advanced mode.</summary>
    protected virtual void DisableAdvancedMode()
    {
      if (this.AdvancedModeButton != null)
        this.AdvancedModeButton.Style.Add("display", "none");
      if (this.SimpleModeButton == null)
        return;
      this.SimpleModeButton.Style.Add("display", "none");
    }

    /// <summary>
    /// Initializes the providers selector.
    /// Sets ProvidersSelector visibility to true and specifies the name of the selected provider.
    /// </summary>
    /// <param name="manager">The manager associated with the providers listed by this selector.</param>
    public void InitializeProvidersSelector(IManager manager, string selectedProviderName)
    {
      this.ShowProvidersSelector = true;
      this.ProvidersSelector.Manager = manager;
      this.ProvidersSelector.SelectedProviderName = selectedProviderName;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      ScriptControlDescriptor controlDescriptor = source.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_controlId", (object) this.ControlId.ToString());
      controlDescriptor.AddProperty("_implementsDesigner", (object) this.implementsDesigner);
      controlDescriptor.AddProperty("_hideAdvancedMode", (object) this.HideAdvancedMode);
      controlDescriptor.AddProperty("_mediaType", (object) this.MediaType.ToString());
      controlDescriptor.AddProperty("_pageId", (object) this.PageId);
      if (this.controlDesigner != null && typeof (IScriptControl).IsAssignableFrom(this.controlDesigner.GetType()))
        controlDescriptor.AddComponentProperty("designer", this.controlDesigner.ClientID);
      string str = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute(string.Format("~/Sitefinity/Services/Pages/ControlPropertyService.svc/batch/{0}/", (object) this.ControlId.ToString())));
      controlDescriptor.AddProperty("_propertiesServiceUrl", (object) str);
      controlDescriptor.AddProperty("_propertyBag", (object) scriptSerializer.Serialize((object) this.GetPropertyBag()));
      controlDescriptor.AddComponentProperty("propertyGrid", this.PropertyGrid.ClientID);
      controlDescriptor.AddProperty("_modesMultiPageId", (object) this.ModesMultiPage.ClientID);
      if (this.SaveButton != null)
        controlDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
      if (this.SaveAllTranslationsButton != null)
        controlDescriptor.AddElementProperty("saveAllTranslationsButton", this.SaveAllTranslationsButton.ClientID);
      controlDescriptor.AddProperty("_checkLiveVersion", (object) this.CheckLiveVersion);
      controlDescriptor.AddProperty("_upgradePageVersion", (object) this.UpgradePageVersion);
      controlDescriptor.AddProperty("isOpenedByBrowseAndEdit", (object) this.OpenedByBrowseAndEdit);
      controlDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      if (this.SaveConfirmationDialog != null)
        controlDescriptor.AddComponentProperty("saveConfirmationDialog", this.SaveConfirmationDialog.ClientID);
      if (this.OkButton != null)
        controlDescriptor.AddElementProperty("okButton", this.OkButton.ClientID);
      if (this.AdvancedModeButton != null)
        controlDescriptor.AddElementProperty("advancedModeButton", this.AdvancedModeButton.ClientID);
      if (this.SimpleModeButton != null)
        controlDescriptor.AddElementProperty("simpleModeButton", this.SimpleModeButton.ClientID);
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        if (string.IsNullOrEmpty(this.PropertyValuesCulture))
          throw new ApplicationException("Culture not set for PropertyEditor");
        controlDescriptor.AddProperty("_uiCulture", (object) this.PropertyValuesCulture);
      }
      if (this.TitleElement != null)
        controlDescriptor.AddElementProperty("titleElement", this.TitleElement.ClientID);
      if (this.ProvidersSelector != null && this.ProvidersSelector.Visible)
        controlDescriptor.AddComponentProperty("providersSelector", this.ProvidersSelector.ClientID);
      if (this.OrLabel == null)
        return (IEnumerable<ScriptDescriptor>) source;
      controlDescriptor.AddElementProperty("orLabel", this.OrLabel.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (PropertyEditor).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.PropertyEditor.js", typeof (PropertyEditor).Assembly.FullName)
    }.ToArray();

    protected virtual void PrepareControlDesigner(ControlData ctrlData)
    {
      this.controlDesigner = ControlDesignerFactory.GetDesigner(ctrlData);
      if (this.controlDesigner != null)
      {
        this.controlDesigner.ControlId = this.ControlId;
        this.controlDesigner.PropertyEditor = this;
        this.SimpleModeView.Controls.Add((System.Web.UI.Control) this.controlDesigner);
      }
      this.implementsDesigner = this.controlDesigner != null;
    }

    protected virtual IList<WcfControlProperty> GetPropertyBag()
    {
      List<WcfControlProperty> wcfControlPropertyList = new List<WcfControlProperty>();
      WcfPropertyManager propManager = new WcfPropertyManager();
      CultureInfo culture = string.IsNullOrEmpty(this.PropertyValuesCulture) ? (CultureInfo) null : CultureInfo.GetCultureInfo(this.PropertyValuesCulture);
      FallbackMode languageFallbackMode = SystemManager.RequestLanguageFallbackMode;
      SystemManager.RequestLanguageFallbackMode = FallbackMode.NoFallback;
      IList<WcfControlProperty> properties = propManager.GetProperties(this.Control, this.ControlData, -1, (string) null, culture);
      SystemManager.RequestLanguageFallbackMode = languageFallbackMode;
      Func<WcfControlProperty, int> keySelector = (Func<WcfControlProperty, int>) (p => ((IEnumerable<string>) p.PropertyPath.Split(propManager.LevelDelimiter)).Count<string>());
      return (IList<WcfControlProperty>) properties.OrderBy<WcfControlProperty, int>(keySelector).ThenBy<WcfControlProperty, string>((Func<WcfControlProperty, string>) (p => p.PropertyName)).ToList<WcfControlProperty>();
    }

    private void InitiliazeRequestProperties()
    {
      if (this.Page.Request.QueryString["Id"] == null)
        throw new ArgumentNullException("ControlId must be defined through the query string of the current request.");
      this.ControlId = new Guid(this.Page.Request.QueryString["Id"]);
      if (this.Page.Request.QueryString["MediaType"] == null)
        throw new ArgumentNullException("MediaType must be defined through the query string of the current request.");
      this.MediaType = (DesignMediaType) Enum.Parse(typeof (DesignMediaType), this.Page.Request.QueryString["MediaType"]);
      if (this.Page.Request.QueryString["PageId"] == null)
        throw new ArgumentNullException("PageId must be defined through the query string of the current request.");
      this.PageId = new Guid(this.Page.Request.QueryString["PageId"]);
      bool result1 = false;
      bool.TryParse(this.Page.Request.QueryString["checkLiveVersion"], out result1);
      this.CheckLiveVersion = result1.ToString();
      bool result2 = false;
      bool.TryParse(this.Page.Request.QueryString["upgradePageVersion"], out result2);
      this.UpgradePageVersion = result2.ToString();
      this.PropertyValuesCulture = this.Page.Request.QueryString["propertyValueCulture"];
      bool result3 = false;
      bool.TryParse(this.Page.Request.QueryString["isOpenedByBrowseAndEdit"], out result3);
      this.OpenedByBrowseAndEdit = result3;
    }

    /// <summary>
    /// Gets the correct control data to be used in the property editor. If the control is overridden for the current page, or if it is overridden
    /// on a parent template the method will return the overriding control. Otherwise it will return the base control.
    /// </summary>
    /// <param name="baseControlId">The base control id.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageManager">The page manager.</param>
    /// <returns>The control data to be use in the property editor.</returns>
    private ControlData GetControlData(
      Guid baseControlId,
      Guid pageId,
      IControlManager manager)
    {
      return new WcfPropertyManager().GetOverridingControl(baseControlId, pageId) ?? manager.GetControl<ControlData>(baseControlId);
    }

    private IControlManager GetControlManager(DesignMediaType designMediaType) => designMediaType == DesignMediaType.Form ? (IControlManager) FormsManager.GetManager() : (IControlManager) PageManager.GetManager();
  }
}
