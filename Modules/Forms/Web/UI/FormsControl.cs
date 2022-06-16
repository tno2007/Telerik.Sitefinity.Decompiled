// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.FormsControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms.Events;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>Public control for displaying a form.</summary>
  [ControlDesigner(typeof (FormsControlDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "FormsTitle")]
  [RequireScriptManager]
  [RequiresEmbeddedWebResource("Telerik.Sitefinity.Resources.Themes.LayoutsBasics.css", "Telerik.Sitefinity.Resources.Reference")]
  public class FormsControl : 
    SimpleScriptView,
    ILicensedControl,
    ICustomWidgetVisualization,
    IHasCacheDependency
  {
    private string validationGroup;
    /// <summary>Name of the template to use</summary>
    public static readonly string formControlScript = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormsControl.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Forms.FormsControl.ascx");
    private FormDescription formData;
    private readonly List<IFormFieldControl> formFieldControls = new List<IFormFieldControl>();
    private readonly List<FormSubmitButton> submitButtonsFieldControls = new List<FormSubmitButton>();
    private FormEditRequestContext formEditContext;
    /// <summary>
    /// If false will display a message that the form no more exists
    /// </summary>
    private bool formDescriptionFound = true;
    internal const string PageFormControlsKey = "PageFormControls";
    internal const string ClrTypeDelimiter = "#ClrType#";

    /// <summary>Event when a form control is getting validated</summary>
    [Obsolete("This event is obsolete. Please use the event 'FormFieldValidatingEvent' from the EventHub instead.")]
    public event EventHandler<FormFieldValidationEventArgs> FieldValidation;

    /// <summary>Event before the form gets saved</summary>
    [Obsolete("This event is obsolete. Please use the event 'FormSavingEvent' from the EventHub instead.")]
    public event EventHandler<CancelEventArgs> BeforeFormSave;

    /// <summary>
    /// Event raised before the form action gets executed like redirect to another page or show a success message
    /// </summary>
    [Obsolete("This event is obsolete. Please use the event 'BeforeFormActionEvent' from the EventHub instead.")]
    public event EventHandler<CancelEventArgs> BeforeFormAction;

    /// <summary>Event raised after the form data is saved</summary>
    [Obsolete("This event is obsolete. Please use the event 'FormSavedEvent' from the EventHub instead.")]
    public event EventHandler<EventArgs> FormSaved;

    /// <summary>The id of the form that will be displayed.</summary>
    public Guid FormId { get; set; }

    /// <summary>Gets or sets the name of the form.</summary>
    /// <value>The name of the form.</value>
    public string FormName { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance of the control is licensed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is licensed; otherwise, <c>false</c>.
    /// </value>
    [Browsable(false)]
    public bool IsLicensed => LicenseState.CheckIsModuleLicensedInCurrentDomain("A64410F7-2F1E-4068-81D0-E28D864DE323");

    /// <summary>
    /// Gets custom the licensing message.If null the system will use a default message
    /// </summary>
    /// <value>The licensing message.</value>
    [Browsable(false)]
    public string LicensingMessage => Res.Get<FormsResources>().ModuleNotLicensed;

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    public virtual bool IsEmpty => this.FormData == null && this.FormId == Guid.Empty;

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public virtual string EmptyLinkText => Res.Get<FormsResources>().SelectForm;

    /// <summary>
    /// Validation group for the controls of the form
    /// <remarks>If not specified a new based on the form name and serer id is used</remarks>
    /// </summary>
    /// <value>The name of form validation group.</value>
    public virtual string ValidationGroup
    {
      get
      {
        if (string.IsNullOrEmpty(this.validationGroup))
          this.validationGroup = this.FormData == null ? this.ID : this.FormData.Name + this.ID;
        return this.validationGroup;
      }
      set => this.validationGroup = value;
    }

    /// <summary>
    /// Gets a list containing the field controls for this Form.
    /// </summary>
    /// <value>The field controls.</value>
    protected virtual List<IFormFieldControl> FieldControls => this.formFieldControls;

    /// <summary>Gets the form edit context.</summary>
    /// <value>The form edit context.</value>
    internal virtual FormEditRequestContext FormEditContext
    {
      get
      {
        if (this.formEditContext == null)
          this.formEditContext = new FormEditRequestContext();
        return this.formEditContext;
      }
    }

    /// <summary>Represents the current form</summary>
    protected FormDescription FormData
    {
      get
      {
        if (this.formData == null && (this.FormId != Guid.Empty || !string.IsNullOrEmpty(this.FormName)) && this.formDescriptionFound)
        {
          FormsManager manager = FormsManager.GetManager();
          FormDescription formDescription = (FormDescription) null;
          try
          {
            formDescription = !(this.FormId != Guid.Empty) ? manager.GetFormByName(this.FormName) : manager.GetForm(this.FormId);
          }
          catch (ItemNotFoundException ex)
          {
            this.formDescriptionFound = false;
          }
          if (this.formDescriptionFound && !formDescription.Visible)
          {
            this.formDescriptionFound = false;
            this.formData = (FormDescription) null;
          }
          else
            this.formData = formDescription;
        }
        return this.formData;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control to use Ajax submit when the form submit button is clicked
    /// </summary>
    public bool UseAjaxSubmit { get; set; }

    /// <summary>Gets or sets the form Submit action.</summary>
    public SubmitAction SubmitAction { get; set; }

    /// <summary>Gets or sets the submit action after form update.</summary>
    /// <value>The submit action after form update.</value>
    public SubmitAction SubmitActionAfterFormUpdate { get; set; }

    /// <summary>Gets or sets the redirect page URL after form update.</summary>
    /// <value>The redirect page URL after form update.</value>
    public string RedirectPageUrlAfterFormUpdate { get; set; }

    /// <summary>Gets or sets the success message after update.</summary>
    /// <value>The success message after update.</value>
    public string SuccessMessageAfterFormUpdate { get; set; }

    /// <summary>
    /// Gets or sets the user custom confirmation after form update.
    /// </summary>
    /// <value>The user custom confirmation after form update.</value>
    public bool UserCustomConfirmationAfterFormUpdate { get; set; }

    /// <summary>Gets or sets the form Success message.</summary>
    public string SuccessMessage { get; set; }

    /// <summary>Gets or sets the form Redirect page url.</summary>
    public string RedirectPageUrl { get; set; }

    /// <summary>
    /// Get or sets whether the user wants to use custom confirmation
    /// </summary>
    public bool UserCustomConfirmation { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the current language.</summary>
    /// <value>The current language.</value>
    public string CurrentLanguage { get; set; }

    /// <summary>
    /// Gets the current form mode. That will be used to determine the visual (hidden/read only) state of each control field.
    /// </summary>
    /// <value>The current form mode.</value>
    protected virtual string CurrentFormMode
    {
      get
      {
        string mode;
        if (this.TryGetCurrentFormModeFormEntryProvider(out mode))
          return mode;
        return !this.IsUpdateRequest ? FormsControl.FormsDefaultModes.Create : FormsControl.FormsDefaultModes.Update;
      }
    }

    /// <summary>
    /// Gets the form entry id. This value is empty when form is in create mode.
    /// </summary>
    /// <value>The form entry id.</value>
    protected virtual Guid CurrentFormEntryId => this.FormEditContext.FormEntryId;

    /// <summary>
    /// Gets a value indicating whether is a valid update request.
    /// </summary>
    /// <value>The is valid update request.</value>
    protected virtual bool IsUpdateRequest => this.FormEditContext.IsValidUpdateRequest;

    /// <summary>Gets the name of the current form provider.</summary>
    /// <value>The name of the provider.</value>
    protected virtual string FormEntriesProviderName => !string.IsNullOrWhiteSpace(this.FormEditContext.ProviderName) ? this.FormEditContext.ProviderName : this.FormData.GetProviderName();

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InitializeAjaxMode();
      if (!this.IsLicensed)
      {
        this.ErrorsPanel.Visible = true;
        this.ErrorsPanel.Controls.Add((Control) new Label()
        {
          Text = Res.Get<FormsResources>().ModuleNotLicensed
        });
      }
      else
      {
        this.ProcessFormInitialization();
        if (!this.formDescriptionFound && this.IsDesignMode())
        {
          this.ErrorsPanel.Visible = true;
          this.ErrorsPanel.Controls.Add((Control) new Label()
          {
            Text = Res.Get<FormsResources>().TheSpecifiedFormNoLongerExists
          });
        }
        this.SubscribeCacheDependency();
        this.SetPageFormControlKeyInCurrentHttpContext();
      }
    }

    /// <summary>Processes the form initialization.</summary>
    private void ProcessFormInitialization()
    {
      if (this.FormData == null || !this.AllowRenderForm())
        return;
      this.formEditContext = FormEditRequestContext.Get(SystemManager.CurrentHttpContext.Request.QueryString, this.FormId);
      this.SetHiddenFieldQueryValue();
      this.RenderFormFields();
    }

    /// <summary>Sets the hidden field query value.</summary>
    private void SetHiddenFieldQueryValue()
    {
      if (this.IsUpdateRequest)
        this.QueryFormsData.Value = this.FormEditContext.QueryData;
      if (this.CurrentFormModeField == null)
        return;
      this.CurrentFormModeField.Value = this.CurrentFormMode;
    }

    /// <summary>Allows the render form.</summary>
    /// <returns></returns>
    private bool AllowRenderForm()
    {
      bool flag = true;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        string name = this.Page.Request.QueryString["language"];
        if (!name.IsNullOrEmpty())
          culture = new CultureInfo(name);
      }
      else
        culture = SystemManager.CurrentContext.CurrentSite.DefaultCulture;
      if (this.CurrentLanguage != null)
        culture = new CultureInfo(this.CurrentLanguage);
      if (!LifecycleExtensions.IsPublished(this.FormData, culture))
        flag = false;
      return flag;
    }

    /// <summary>
    /// Sets the page form control key in current HTTP context.
    /// </summary>
    private void SetPageFormControlKeyInCurrentHttpContext()
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || !this.formDescriptionFound)
        return;
      if (!currentHttpContext.Items.Contains((object) "PageFormControls"))
        currentHttpContext.Items[(object) "PageFormControls"] = (object) string.Empty;
      currentHttpContext.Items[(object) "PageFormControls"] = (object) string.Format("{0}Id={1},ValidationGroup={2};", (object) (string) currentHttpContext.Items[(object) "PageFormControls"], (object) this.FormId, (object) this.ValidationGroup);
    }

    /// <summary>Initializes if Ajax mode.</summary>
    private void InitializeAjaxMode()
    {
      if (!this.UseAjaxSubmit)
        return;
      this.ErrorsPanel.Visible = true;
      this.ErrorsPanel.Style[HtmlTextWriterStyle.Display] = "none";
      this.ErrorsPanel.Controls.Clear();
    }

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormsControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Panel that represents the error messages</summary>
    protected Panel ErrorsPanel => this.Container.GetControl<Panel>("errorsPanel", true);

    /// <summary>Placeholde for the form controls</summary>
    protected Panel FormControls => this.Container.GetControl<Panel>("formControls", true);

    /// <summary>
    /// Gets the query forms data. This data serves as a validation token when performing form's entry update.
    /// </summary>
    /// <value>The query forms data.</value>
    protected virtual HtmlInputHidden QueryFormsData => this.Container.GetControl<HtmlInputHidden>("queryData", true);

    /// <summary>Gets the current form mode field.</summary>
    /// <value>The current form mode field.</value>
    protected virtual HtmlInputHidden CurrentFormModeField => this.Container.GetControl<HtmlInputHidden>("currentFormMode", false);

    /// <summary>Gets the label containing the success message.</summary>
    /// <value>The success message label.</value>
    protected Label SuccessMessageLabel => this.Container.GetControl<Label>("successMessage", true);

    /// <summary>Subscribes the cache dependency.</summary>
    private void SubscribeCacheDependency()
    {
      if (this.IsBackend())
        return;
      IList<CacheDependencyKey> dependencyObjects = ((IHasCacheDependency) this).GetCacheDependencyObjects();
      if (dependencyObjects.Count <= 0)
        return;
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "PageDataCacheDependencyName"))
        SystemManager.CurrentHttpContext.Items.Add((object) "PageDataCacheDependencyName", (object) new List<CacheDependencyKey>());
      ((List<CacheDependencyKey>) SystemManager.CurrentHttpContext.Items[(object) "PageDataCacheDependencyName"]).AddRange((IEnumerable<CacheDependencyKey>) dependencyObjects);
    }

    IList<CacheDependencyKey> IHasCacheDependency.GetCacheDependencyObjects()
    {
      List<CacheDependencyKey> dependencyObjects = new List<CacheDependencyKey>();
      if (this.FormId != Guid.Empty)
        dependencyObjects.AddRange(OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(typeof (FormDescription), this.FormId));
      return (IList<CacheDependencyKey>) dependencyObjects;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.AddCssClass("sfFormsEditor");
      if (!this.IsLicensed || this.FormData == null)
        return;
      switch (this.FormData.FormLabelPlacement)
      {
        case FormLabelPlacement.TopAligned:
          this.AddCssClass("sfTopLbls");
          break;
        case FormLabelPlacement.LeftAligned:
          this.AddCssClass("sfLeftLbls");
          break;
        case FormLabelPlacement.RightAligned:
          this.AddCssClass("sfRightLbls");
          break;
      }
      this.AddCssClass(this.FormData.CssClass);
    }

    /// <summary>Raises the FieldValidation event</summary>
    /// <param name="formFieldControl">The form field control.</param>
    /// <returns></returns>
    protected virtual void OnFieldValidation(FormFieldValidationEventArgs args)
    {
      if (this.FieldValidation == null)
        return;
      this.FieldValidation((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:BeforeFormSave" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    /// <returns></returns>
    protected virtual void OnBeforeFormSave(CancelEventArgs args)
    {
      if (this.BeforeFormSave == null)
        return;
      this.BeforeFormSave((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:BeforeFormAction" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:System.ComponentModel.CancelEventArgs" /> instance containing the event data.</param>
    /// <returns></returns>
    protected virtual void OnBeforeFormAction(CancelEventArgs args)
    {
      if (this.BeforeFormAction == null)
        return;
      this.BeforeFormAction((object) this, args);
    }

    /// <summary>
    /// Raises the <see cref="E:FormSaved" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void OnFormSaved(EventArgs args = null)
    {
      if (args == null)
        args = EventArgs.Empty;
      if (this.FormSaved == null)
        return;
      this.FormSaved((object) this, args);
    }

    /// <summary>Invoked on submit button click</summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void Submit_Click(object sender, EventArgs e) => this.ProcessFormSaving();

    /// <summary>Creates the loading panel.</summary>
    /// <param name="clientId">The client id.</param>
    /// <returns></returns>
    private HtmlGenericControl CreateLoadingPanel(string clientId)
    {
      string resourceName = "Telerik.Sitefinity.Resources.Themes.Light.Images.Loadings.sfLoadingData.gif";
      string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(Config.Get<ControlsConfig>().ResourcesAssemblyInfo, resourceName);
      HtmlGenericControl loadingPanel = new HtmlGenericControl("div");
      loadingPanel.ClientIDMode = ClientIDMode.Static;
      loadingPanel.ID = "loadingPanel_" + clientId;
      loadingPanel.Attributes["style"] = "display: none;";
      loadingPanel.Attributes["class"] = "sfLoadingFormBtns sfButtonArea";
      SfImage child = new SfImage();
      child.ImageUrl = webResourceUrl;
      loadingPanel.Controls.Add((Control) child);
      return loadingPanel;
    }

    /// <summary>Tries the get current form mode form entry provider.</summary>
    /// <param name="mode">The mode.</param>
    /// <returns></returns>
    private bool TryGetCurrentFormModeFormEntryProvider(out string mode)
    {
      mode = string.Empty;
      if (!ObjectFactory.IsTypeRegistered<IFormEntryEditModeProvider>())
        return false;
      IFormEntryEditModeProvider editModeProvider = ObjectFactory.Resolve<IFormEntryEditModeProvider>();
      mode = editModeProvider.GetCurrentMode(this.GetCurrentFormEntryEditContext());
      return true;
    }

    /// <summary>Renders the form fields in edit mode.</summary>
    /// <param name="formId">The form id.</param>
    /// <param name="formEntryId">The form entry id.</param>
    private void RenderFormFields()
    {
      if (this.FormData == null)
        return;
      PlaceHoldersCollection placeHolders = new PlaceHoldersCollection();
      PlaceHolder placeHolder1 = new PlaceHolder();
      placeHolder1.ID = "Body";
      PlaceHolder child1 = placeHolder1;
      PlaceHolder placeHolder2 = new PlaceHolder();
      placeHolder2.ID = "Header";
      PlaceHolder child2 = placeHolder2;
      PlaceHolder placeHolder3 = new PlaceHolder();
      placeHolder3.ID = "Footer";
      PlaceHolder child3 = placeHolder3;
      placeHolders.Add((Control) child2);
      placeHolders.Add((Control) child1);
      placeHolders.Add((Control) child3);
      this.LoadFieldControls(FormsHelper.GetSortedControlsData(this.FormData), placeHolders);
      this.FormControls.Controls.Add((Control) child2);
      this.FormControls.Controls.Add((Control) child1);
      this.FormControls.Controls.Add((Control) child3);
    }

    /// <summary>Loads the field controls.</summary>
    /// <param name="controlsData">The controls data.</param>
    /// <param name="placeHolders">The place holders.</param>
    internal void LoadFieldControls(
      List<Telerik.Sitefinity.Pages.Model.ControlData> controlsData,
      PlaceHoldersCollection placeHolders)
    {
      FormsManager manager = FormsManager.GetManager(this.FormEntriesProviderName);
      FormEntry formEntry = this.IsUpdateRequest ? manager.GetFormEntry(this.FormData.EntriesTypeName, this.CurrentFormEntryId) : (FormEntry) null;
      string currentFormMode = this.CurrentFormMode;
      foreach (Telerik.Sitefinity.Pages.Model.ControlData controlData in controlsData)
      {
        Control control1 = this.ConfigureFormControl(manager.LoadControl((ObjectData) controlData, (CultureInfo) null));
        if (control1 != null)
        {
          Control control2;
          if (placeHolders.TryGetValue(controlData.PlaceHolder, out control2))
          {
            switch (control1)
            {
              case FormSubmitButton _:
                this.LoadFormSubmitButton(control1);
                if (this.UseAjaxSubmit)
                {
                  control2.Controls.Add((Control) this.CreateLoadingPanel(control1.ClientID));
                  break;
                }
                break;
              case FormCaptcha _:
                if (this.IsUpdateRequest)
                  continue;
                break;
            }
            control2.Controls.Add(control1);
            if (control1 is IFormFieldControl formFieldControl)
            {
              this.LoadFormFieldControl(formFieldControl, formEntry, currentFormMode);
              this.formFieldControls.Add(formFieldControl);
            }
          }
          if (control1 is LayoutControl)
          {
            LayoutControl layoutControl = (LayoutControl) control1;
            layoutControl.PlaceHolder = controlData.PlaceHolder;
            placeHolders.AddRange((IEnumerable<Control>) layoutControl.Placeholders);
          }
        }
      }
    }

    private void LoadFormFieldControl(
      IFormFieldControl formFieldControl,
      FormEntry formEntry,
      string currentMode)
    {
      if (!(formFieldControl is FieldControl fieldControl))
        return;
      if (formFieldControl.MetaField != null && !string.IsNullOrEmpty(formFieldControl.MetaField.FieldName))
        fieldControl.DataFieldName = formFieldControl.MetaField.FieldName;
      fieldControl.ValidationGroup = this.ValidationGroup;
      fieldControl.ValidatorDefinition.MessageCssClass = string.Format("sfError {0}", (object) fieldControl.ValidatorDefinition.MessageCssClass);
      fieldControl.ControlCssClassOnError = string.Format("sfErrorWrp {0}", (object) fieldControl.ControlCssClassOnError);
      if (this.IsBackend() || this.IsDesignMode() || !(fieldControl is IMultiDisplayModesSupport formFieldControl1))
        return;
      if (fieldControl.Visible)
        fieldControl.Visible = formFieldControl1.GetFieldVisibleByMode(currentMode);
      if (formFieldControl1 != null)
        fieldControl.DisplayMode = formFieldControl1.GetFieldReadOnlyByMode(currentMode) ? FieldDisplayMode.Read : FieldDisplayMode.Write;
      if (!this.IsUpdateRequest)
        return;
      this.SetFieldControlValue(fieldControl, formEntry);
    }

    /// <summary>Sets the field control value.</summary>
    /// <param name="fieldControl">The field control.</param>
    /// <param name="formEntry">The form entry.</param>
    private void SetFieldControlValue(FieldControl fieldControl, FormEntry formEntry)
    {
      EventHandler preRenderEventHendler = (EventHandler) null;
      preRenderEventHendler = (EventHandler) ((sender, eventArgs) =>
      {
        fieldControl.PreRender -= preRenderEventHendler;
        if (string.IsNullOrWhiteSpace(fieldControl.DataFieldName))
          return;
        fieldControl.Value = formEntry.GetValue(fieldControl.DataFieldName);
      });
      fieldControl.PreRender += preRenderEventHendler;
    }

    /// <summary>Loads the form submit button.</summary>
    /// <param name="control">The control.</param>
    private void LoadFormSubmitButton(Control control)
    {
      this.ConfigureSubmitButton(control, this.ValidationGroup);
      this.submitButtonsFieldControls.Add((FormSubmitButton) control);
    }

    /// <summary>Processes the form update.</summary>
    /// <param name="formDescription">The form description.</param>
    private void ProcessFormSaving()
    {
      if (!this.AllowSavingForm())
        return;
      IEnumerable<IFormFieldControl> formFieldControls = FormsHelper.ProcessValidForUpdateFieldControls((IEnumerable<IFormFieldControl>) this.FieldControls, this.CurrentFormMode);
      IFormEntryResponseEditContext reponseEditContext = FormsHelper.GenerateReponseEditContext(this.CurrentFormEntryId, this.FormData);
      IList<IFormEntryEventControl> currentControlsState = FormsHelper.GetCurrentControlsState(this.IsUpdateRequest, this.FormData, this.CurrentFormEntryId);
      string errorMessage;
      if (!this.ValidateFormInput(this.CreateFormValidatingEvent(reponseEditContext, currentControlsState), formFieldControls, out errorMessage))
      {
        this.SetErrorMessage(errorMessage);
      }
      else
      {
        try
        {
          this.RaiseFormSavingEvent(reponseEditContext, currentControlsState);
        }
        catch (EventHandlerInvocationException ex)
        {
          if (ex.Lookup<CancelationException>() != null)
            return;
          throw;
        }
        CancelEventArgs args = new CancelEventArgs();
        this.OnBeforeFormSave(args);
        if (args.Cancel)
          return;
        this.ProcessFormSaving(formFieldControls);
        try
        {
          this.RaiseFormSavedEvent(reponseEditContext, currentControlsState);
        }
        catch (EventHandlerInvocationException ex)
        {
          throw;
        }
        this.OnFormSaved();
        try
        {
          this.RaiseBeforeFormActionEvent(reponseEditContext, currentControlsState);
        }
        catch (EventHandlerInvocationException ex)
        {
          if (ex.Lookup<CancelationException>() != null)
            return;
          throw;
        }
        this.ProcessFormSubmitAction();
      }
    }

    /// <summary>
    /// Get value indicating whether is allow to save the form entry.
    /// </summary>
    /// <returns></returns>
    protected virtual bool AllowSavingForm()
    {
      string errorMessage;
      if (!this.IsResponseValidToUpdate(this.FormData, out errorMessage))
      {
        this.SetErrorMessage(errorMessage);
        return false;
      }
      if (this.IsUpdateRequest || this.ValidateFormSubmissionRestrictions(this.FormData, out errorMessage))
        return true;
      this.SetErrorMessage(errorMessage);
      return false;
    }

    /// <summary>Save the current form entry.</summary>
    /// <param name="validControlsSave">Fields to be save.</param>
    protected virtual void ProcessFormSaving(IEnumerable<IFormFieldControl> validControlsSave)
    {
      if (this.IsUpdateRequest)
        this.UpdateFormEntry(this.FormData, this.CurrentFormEntryId, FormsHelper.GetFormPostedData(validControlsSave), this.FormEntriesProviderName);
      else
        this.SaveFormEntry(this.FormData, validControlsSave);
    }

    /// <summary>Get value indicating whether response is valid.</summary>
    /// <param name="formDescription">The form entry formDescription.</param>
    /// <param name="errorMessage">The form entry errorMessage.</param>
    protected virtual bool IsResponseValidToUpdate(
      FormDescription formDescription,
      out string errorMessage)
    {
      errorMessage = string.Empty;
      if (formDescription == null)
        return false;
      if (!this.IsRequestValid())
      {
        errorMessage = "Invalid request!";
        return false;
      }
      if (!this.IsRequestExpired())
        return true;
      errorMessage = "Request expired!";
      return false;
    }

    /// <summary>Raises the before form action event.</summary>
    /// <param name="formEntryResponseEditContext">The form entry response edit context.</param>
    private void RaiseBeforeFormActionEvent(
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate)
    {
      EventHub.Raise((IEvent) this.CreateBeforeFormActionEvent(formEntryResponseEditContext, controlsSate));
    }

    /// <summary>Raises the form saved event.</summary>
    /// <param name="formEntryResponseEditContext">The form entry response edit context.</param>
    private void RaiseFormSavedEvent(
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate)
    {
      EventHub.Raise((IEvent) this.CreateFormSavedEvent(formEntryResponseEditContext, controlsSate));
    }

    /// <summary>Raises the form saving event.</summary>
    /// <param name="formEntryResponseEditContext">The form entry response edit context.</param>
    private void RaiseFormSavingEvent(
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate)
    {
      EventHub.Raise((IEvent) this.CreateFormSavingEvent(formEntryResponseEditContext, controlsSate));
    }

    /// <summary>Determines whether [is request valid].</summary>
    /// <returns></returns>
    protected virtual bool IsRequestValid() => !this.IsUpdateRequest || this.FormEditContext.QueryData == this.QueryFormsData.Value;

    /// <summary>Determines whether [is request expired].</summary>
    /// <returns></returns>
    protected virtual bool IsRequestExpired() => this.FormEditContext.IsExpired;

    /// <summary>Sets the error message.</summary>
    /// <param name="message">The message.</param>
    private void SetErrorMessage(string message)
    {
      this.FormControls.Visible = true;
      string str = ControlUtilities.Sanitize(message);
      if (string.IsNullOrEmpty(str))
        return;
      this.ErrorsPanel.Visible = true;
      this.ErrorsPanel.Controls.Add((Control) new Label()
      {
        Text = str
      });
    }

    /// <summary>Gets the current form entry edit context.</summary>
    /// <returns></returns>
    private IFormEntryEditContext GetCurrentFormEntryEditContext() => (IFormEntryEditContext) new FormEntryEditContext()
    {
      FormId = this.FormData.Id,
      EntryId = this.CurrentFormEntryId,
      FormTitle = (string) this.FormData.Title,
      EntryTypeName = this.FormData.EntriesTypeName,
      ProviderName = (this.FormEditContext.ProviderName ?? this.FormData.GetProviderName()),
      FormName = this.FormData.Name,
      IpAddress = this.Page.Request.UserHostAddress,
      User = this.GetCurrentUser()
    };

    /// <summary>
    /// Validates the form against the preset submit restrictions.
    /// </summary>
    protected virtual bool ValidateFormSubmissionRestrictions(
      FormDescription formDescription,
      out string errorMessage)
    {
      return this.ValidateCaptchas(out errorMessage) && FormsHelper.ValidateFormSubmissionRestrictions(formDescription, ClaimsManager.GetCurrentUserId(), this.Page.Request.UserHostAddress, out errorMessage);
    }

    /// <summary>Validates the Captcha controls.</summary>
    /// <returns></returns>
    private bool ValidateCaptchas(out string errorMessage)
    {
      if (SystemManager.HttpContextItems[(object) (this.ID + "radCaptcha")] is List<FormCaptcha> httpContextItem)
      {
        foreach (FormCaptcha formCaptcha in httpContextItem)
        {
          if (!formCaptcha.IsValid())
          {
            errorMessage = formCaptcha.InvalidInputMessage;
            return false;
          }
        }
      }
      errorMessage = string.Empty;
      return true;
    }

    /// <summary>Validates the form input.</summary>
    /// <returns>Get value indicating whether validation is succeeded.</returns>
    protected bool ValidateFormInput() => this.ValidateFormInput(this.CreateFormValidatingEvent(FormsHelper.GenerateReponseEditContext(this.CurrentFormEntryId, this.FormData), FormsHelper.GetCurrentControlsState(this.IsUpdateRequest, this.FormData, this.CurrentFormEntryId)), (IEnumerable<IFormFieldControl>) this.FieldControls, out string _);

    /// <summary>Validates the form input.</summary>
    /// <param name="responseEditContext">The response edit context.</param>
    /// <param name="errorMessage">The error message.</param>
    /// <returns></returns>
    protected virtual bool ValidateFormInput(
      FormValidatingEvent formValidatingEvent,
      IEnumerable<IFormFieldControl> controls,
      out string errorMessage)
    {
      errorMessage = (string) null;
      try
      {
        EventHub.Raise((IEvent) formValidatingEvent, true);
      }
      catch (EventHandlerInvocationException ex)
      {
        errorMessage = (ex.Lookup<ValidationException>() ?? throw ex).Message;
        return false;
      }
      bool flag = true;
      foreach (IFormFieldControl control in controls)
      {
        FormFieldValidationEventArgs args = new FormFieldValidationEventArgs(control);
        this.OnFieldValidation(args);
        if (args.Cancel)
        {
          flag = args.IsValid;
          break;
        }
        try
        {
          EventHub.Raise((IEvent) this.CreateFormFieldValidatingEvent(control), true);
        }
        catch (EventHandlerInvocationException ex)
        {
          errorMessage = (ex.Lookup<ValidationException>() ?? throw ex).Message;
          flag = false;
          break;
        }
        if (args.Validated)
        {
          if (!args.IsValid)
            flag = false;
        }
        else if (!control.IsValid())
          flag = false;
      }
      return flag;
    }

    private void ProcessFormSubmitAction()
    {
      if (this.IsUpdateRequest)
        this.ProcessFormUpdateAction();
      else
        this.ProcessFromSubmitAction(this.FormData);
    }

    /// <summary>
    /// On successful submit navigates the user or displays success message
    /// </summary>
    /// <param name="form">The form.</param>
    protected virtual void ProcessFromSubmitAction(FormDescription formDefinition)
    {
      int num = this.UserCustomConfirmation ? (int) this.SubmitAction : (int) formDefinition.SubmitAction;
      string url = this.UserCustomConfirmation ? this.RedirectPageUrl : formDefinition.RedirectPageUrl;
      Lstring lstring = this.UserCustomConfirmation ? (Lstring) this.SuccessMessage : formDefinition.SuccessMessage;
      if (num == 1)
      {
        if (string.IsNullOrEmpty(url))
          return;
        this.Page.Response.Redirect(url, true);
      }
      else
      {
        if (formDefinition.SubmitAction != SubmitAction.TextMessage || string.IsNullOrEmpty((string) lstring))
          return;
        this.FormControls.Visible = false;
        this.SuccessMessageLabel.Text = (string) lstring;
      }
    }

    /// <summary>Processes the form update action.</summary>
    /// <param name="formDescription">The form description.</param>
    protected virtual void ProcessFormUpdateAction()
    {
      int num = this.UserCustomConfirmationAfterFormUpdate ? 1 : 0;
      SubmitAction submitAction = num != 0 ? this.SubmitActionAfterFormUpdate : this.FormData.SubmitActionAfterUpdate;
      string url = num != 0 ? this.RedirectPageUrlAfterFormUpdate : this.FormData.RedirectPageUrlAfterUpdate;
      Lstring lstring = num != 0 ? (Lstring) this.SuccessMessageAfterFormUpdate : this.FormData.SuccessMessageAfterFormUpdate;
      if (submitAction == SubmitAction.PageRedirect)
      {
        if (string.IsNullOrEmpty(url))
          return;
        this.Page.Response.Redirect(url, true);
      }
      else
      {
        if (this.FormData.SubmitActionAfterUpdate != SubmitAction.TextMessage || string.IsNullOrEmpty((string) lstring))
          return;
        this.FormControls.Visible = false;
        this.SuccessMessageLabel.Text = (string) lstring;
      }
    }

    /// <summary>Saves the form field values into a new form entry</summary>
    /// <param name="description">The description.</param>
    protected void SaveFormEntry(
      FormDescription description,
      IEnumerable<IFormFieldControl> controls)
    {
      string formLanguage = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture.Name : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
      FormPostedData formPostedData = FormsHelper.GetFormPostedData(controls);
      FormsHelper.SaveFormsEntry(description.Id, (IEnumerable<KeyValuePair<string, object>>) formPostedData.FormsData, formPostedData.Files, this.GetClientIpAddress(), formLanguage);
    }

    /// <summary>Update the form field values into existing form entry</summary>
    /// <param name="description">The description.</param>
    /// <param name="formEntryId">The formEntryId.</param>
    /// <param name="postedData">The postedData.</param>
    /// <param name="providerName">The providerName.</param>
    protected virtual void UpdateFormEntry(
      FormDescription description,
      Guid formEntryId,
      FormPostedData postedData,
      string providerName)
    {
      Guid currentUserId = ClaimsManager.GetCurrentUserId();
      string membershipProvider = ClaimsManager.GetCurrentIdentity().MembershipProvider;
      FormsHelper.UpdateFormEntry(providerName, formEntryId, description, (IEnumerable<KeyValuePair<string, object>>) postedData.FormsData, postedData.Files, this.GetClientIpAddress(), currentUserId, membershipProvider);
    }

    /// <summary>
    /// Configures the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormSubmitButton" /> of the form.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="validationGroup">The validation group.</param>
    protected virtual void ConfigureSubmitButton(Control control, string validationGroup) => (control as FormSubmitButton).Click += new EventHandler(this.Submit_Click);

    /// <summary>
    /// Configures the form control before adding it to the form.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>The control that should be added.</returns>
    protected virtual Control ConfigureFormControl(Control control) => control;

    internal string GetClientIpAddress()
    {
      IPAddress ipAddress = SystemManager.CurrentHttpContext.Request.GetIpAddress();
      return ipAddress == null ? string.Empty : ipAddress.ToString();
    }

    /// <summary>Creates the form saved event.</summary>
    /// <returns></returns>
    /// <exception cref="T:System.NotImplementedException"></exception>
    private FormSavedEvent CreateFormSavedEvent(
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate)
    {
      return FormsHelper.CreateFormSavedEvent(this.FormData, FormsHelper.GetFormPostedData((IEnumerable<IFormFieldControl>) this.formFieldControls), this.Page.Request.UserHostAddress, ClaimsManager.GetCurrentUserId(), formEntryResponseEditContext, controlsSate, this.CurrentFormMode);
    }

    /// <summary>Creates the before form action event.</summary>
    /// <returns></returns>
    private BeforeFormActionEvent CreateBeforeFormActionEvent(
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate)
    {
      return FormsHelper.CreateBeforeFormActionEvent(this.FormData, FormsHelper.GetFormPostedData((IEnumerable<IFormFieldControl>) this.formFieldControls), this.Page.Request.UserHostAddress, ClaimsManager.GetCurrentUserId(), formEntryResponseEditContext, controlsSate, this.CurrentFormMode);
    }

    /// <summary>Creates the form saving event.</summary>
    /// <returns></returns>
    /// <exception cref="T:System.NotImplementedException"></exception>
    private FormSavingEvent CreateFormSavingEvent(
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate)
    {
      return FormsHelper.CreateFormSavingEvent(this.FormData, FormsHelper.GetFormPostedData((IEnumerable<IFormFieldControl>) this.formFieldControls), this.Page.Request.UserHostAddress, ClaimsManager.GetCurrentUserId(), formEntryResponseEditContext, controlsSate, this.CurrentFormMode);
    }

    /// <summary>Creates the form validating event.</summary>
    /// <returns></returns>
    private FormValidatingEvent CreateFormValidatingEvent(
      IFormEntryResponseEditContext formEntryResponseEditContext,
      IList<IFormEntryEventControl> controlsSate)
    {
      return FormsHelper.CreateFormValidatingEvent(this.FormData, FormsHelper.GetFormPostedData((IEnumerable<IFormFieldControl>) this.formFieldControls), this.Page.Request.UserHostAddress, ClaimsManager.GetCurrentUserId(), formEntryResponseEditContext, controlsSate, this.CurrentFormMode);
    }

    /// <summary>Creates the form field validating event.</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    private FormFieldValidatingEvent CreateFormFieldValidatingEvent(
      IFormFieldControl control)
    {
      return new FormFieldValidatingEvent()
      {
        FormFieldControl = control,
        Origin = this.GetType().FullName
      };
    }

    private User GetCurrentUser()
    {
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      return currentUserId == Guid.Empty ? (User) null : UserManager.GetManager().GetUser(currentUserId);
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor1 = new ScriptControlDescriptor(typeof (FormsControl).FullName, this.ClientID);
      bool hasCaptchaField = false;
      bool hasFileUploadField = false;
      List<object> fieldsMetadata = new List<object>();
      this.formFieldControls.ForEach((Action<IFormFieldControl>) (field =>
      {
        if (!(field is FieldControl fieldControl2))
          return;
        if (fieldControl2.GetType().Equals(typeof (FormCaptcha)))
          hasCaptchaField = true;
        if (fieldControl2.GetType().Equals(typeof (FormFileUpload)))
          hasFileUploadField = true;
        fieldsMetadata.Add((object) new
        {
          Id = fieldControl2.ClientID,
          Type = fieldControl2.GetType().FullName,
          FieldName = field.MetaField.FieldName,
          ClrType = field.MetaField.ClrType
        });
      }));
      List<object> submitButtonsMetadata = new List<object>();
      this.submitButtonsFieldControls.ForEach((Action<FormSubmitButton>) (button => submitButtonsMetadata.Add((object) new
      {
        Id = button.ClientID,
        Type = button.GetType().FullName
      })));
      SubmitAction submitAction;
      if (this.FormData != null)
      {
        controlDescriptor1.AddProperty("formName", (object) this.FormData.Name);
        controlDescriptor1.AddProperty("formId", (object) this.FormData.Id);
        ScriptControlDescriptor controlDescriptor2 = controlDescriptor1;
        string str;
        if (!this.UserCustomConfirmation)
        {
          submitAction = this.FormData.SubmitAction;
          str = submitAction.ToString();
        }
        else
        {
          submitAction = this.SubmitAction;
          str = submitAction.ToString();
        }
        controlDescriptor2.AddProperty("formSubmitAction", (object) str);
        controlDescriptor1.AddProperty("formRedirectUrl", this.UserCustomConfirmation ? (object) this.RedirectPageUrl : (object) this.FormData.RedirectPageUrl);
        Lstring lstring = this.UserCustomConfirmation ? (Lstring) this.SuccessMessage : this.FormData.SuccessMessage;
        controlDescriptor1.AddProperty("successMessage", (object) lstring.Value);
      }
      controlDescriptor1.AddProperty("formDescriptionFound", (object) this.formDescriptionFound);
      controlDescriptor1.AddProperty("successMessageBlockId", (object) this.SuccessMessageLabel.ClientID);
      controlDescriptor1.AddProperty("formControlsContainerId", (object) this.FormControls.ClientID);
      controlDescriptor1.AddProperty("formFieldsMetadata", (object) JsonConvert.SerializeObject((object) fieldsMetadata));
      controlDescriptor1.AddProperty("formSubmitButtonsMetadata", (object) JsonConvert.SerializeObject((object) submitButtonsMetadata));
      controlDescriptor1.AddProperty("hasFileUploadField", (object) hasFileUploadField);
      controlDescriptor1.AddProperty("usePostbackOnSubmit", (object) (!this.UseAjaxSubmit | hasCaptchaField));
      controlDescriptor1.AddProperty("formsSubmitUrl", (object) VirtualPathUtility.ToAbsolute("~/Forms/Submit"));
      controlDescriptor1.AddProperty("errorsPanelContainerId", (object) this.ErrorsPanel.ClientID);
      controlDescriptor1.AddProperty("validationGroup", (object) this.ValidationGroup);
      controlDescriptor1.AddProperty("_clrTypeDelimiter", (object) "#ClrType#");
      controlDescriptor1.AddProperty("_currentMode", (object) this.CurrentFormMode);
      if (this.FormEditContext.IsValidUpdateRequest)
      {
        bool confirmationAfterFormUpdate = this.UserCustomConfirmationAfterFormUpdate;
        controlDescriptor1.AddElementProperty("formsQueryDataElement", this.QueryFormsData.ClientID);
        controlDescriptor1.AddProperty("_successMessageAfterFormUpdate", confirmationAfterFormUpdate ? (object) this.SuccessMessageAfterFormUpdate : (object) this.FormData.SuccessMessageAfterFormUpdate.Value);
        ScriptControlDescriptor controlDescriptor3 = controlDescriptor1;
        string str;
        if (!confirmationAfterFormUpdate)
        {
          submitAction = this.FormData.SubmitActionAfterUpdate;
          str = submitAction.ToString();
        }
        else
        {
          submitAction = this.SubmitActionAfterFormUpdate;
          str = submitAction.ToString();
        }
        controlDescriptor3.AddProperty("_submitActionAfterFormUpdate", (object) str);
        controlDescriptor1.AddProperty("_redirectPageUrlAfterFormUpdate", confirmationAfterFormUpdate ? (object) this.RedirectPageUrlAfterFormUpdate : (object) this.FormData.RedirectPageUrlAfterUpdate);
        controlDescriptor1.AddProperty("_formEditContext", (object) JsonConvert.SerializeObject((object) this.FormEditContext));
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor1
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>();
      string fullName = typeof (FormsControl).Assembly.FullName;
      scriptReferenceList.Add(new ScriptReference(FormsControl.formControlScript, fullName));
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }

    internal class FormsDefaultModes
    {
      public static readonly string Update = "update";
      public static readonly string Create = "create";
    }
  }
}
