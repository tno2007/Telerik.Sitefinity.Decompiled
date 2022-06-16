// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView
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
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions;
using Telerik.Sitefinity.Web.UI.FieldControls;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail
{
  /// <summary>
  /// Represents the view for showing a form entry in detail mode.
  /// </summary>
  public class FormsDetailView : ViewBase
  {
    /// <summary>The layout template name virtual path.</summary>
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.FormsDetailView.ascx");
    /// <summary>The script file embedded resource name.</summary>
    public static readonly string scriptName = "Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Scripts.FormsDetailView.js";
    private FieldControlsBinder fieldControlsBinder;
    private HtmlContainerControl fieldsHolder;
    private Message messageControl;
    private string serviceUrl;
    private List<IFormFieldControl> formFieldControls = new List<IFormFieldControl>();
    private FormEntryDetailsField detailsField;
    private LinkButton deleteEntryButton;
    private LinkButton editEntryButton;
    private string deleteConfirmationMessage;

    /// <summary>
    /// Gets or sets the name of the form that will be displayed.
    /// </summary>
    public string FormName { get; set; }

    /// <summary>
    /// Gets or sets the url of the WCF service to get/set the data
    /// </summary>
    public string ServiceUrl
    {
      get => this.serviceUrl;
      set
      {
        this.serviceUrl = value;
        this.FieldControlsBinder.ServiceUrl = value;
      }
    }

    /// <summary>
    /// Gets or sets the message to be shown when prompting the user if they are sure they want to delete an item
    /// </summary>
    public string DeleteConfirmationMessage
    {
      get
      {
        if (string.IsNullOrEmpty(this.deleteConfirmationMessage))
          this.deleteConfirmationMessage = Res.Get<Labels>().AreYouSureYouWantToDeleteItem;
        return this.deleteConfirmationMessage;
      }
      set => this.deleteConfirmationMessage = value;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (FormsDetailView).Assembly.FullName;
      scriptReferences.Add(new ScriptReference(FormsDetailView.scriptName, fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor baseDescriptor = this.GetBaseDescriptor(typeof (FormsDetailView).FullName, this.ClientID);
      List<string> stringList = new List<string>();
      foreach (IFormFieldControl formFieldControl in this.formFieldControls)
        stringList.Add(((Control) formFieldControl).ClientID);
      baseDescriptor.AddProperty("_fieldControlIds", (object) stringList);
      baseDescriptor.AddProperty("requireDataItemControlIds", (object) this.GetRequireDataItemControlIds());
      baseDescriptor.AddComponentProperty("binder", this.FieldControlsBinder.ClientID);
      baseDescriptor.AddProperty("_deleteEntryButtonId", (object) this.DeleteEntryButton.ClientID);
      baseDescriptor.AddProperty("_editEntryButtonId", (object) this.EditEntryButton.ClientID);
      baseDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      baseDescriptor.AddProperty("_deleteConfirmationMessage", (object) this.DeleteConfirmationMessage);
      baseDescriptor.AddProperty("_baseBackendUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/"));
      baseDescriptor.AddElementProperty("referralCodeLabel", this.ReferralCodeLabel.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        baseDescriptor
      };
    }

    /// <summary>Gets the field controls client binder.</summary>
    public FieldControlsBinder FieldControlsBinder
    {
      get
      {
        if (this.fieldControlsBinder == null)
          this.fieldControlsBinder = this.Container.GetControl<FieldControlsBinder>("fieldsBinder", true);
        return this.fieldControlsBinder;
      }
    }

    /// <summary>Gets the fields holder control.</summary>
    /// <value>The fields holder.</value>
    public HtmlContainerControl FieldsHolder
    {
      get
      {
        if (this.fieldsHolder == null)
          this.fieldsHolder = this.Container.GetControl<HtmlContainerControl>("fieldsHolder", true);
        return this.fieldsHolder;
      }
    }

    /// <summary>Gets the message control.</summary>
    /// <value>The message control.</value>
    public Message MessageControl
    {
      get
      {
        if (this.messageControl == null)
          this.messageControl = this.Container.GetControl<Message>("messageControl", true);
        return this.messageControl;
      }
    }

    /// <summary>Gets the details field control.</summary>
    /// <value>The details field.</value>
    public FormEntryDetailsField DetailsField
    {
      get
      {
        if (this.detailsField == null)
          this.detailsField = this.Container.GetControl<FormEntryDetailsField>("detailsField", true);
        return this.detailsField;
      }
    }

    /// <summary>Gets the delete entry button control.</summary>
    /// <value>The delete entry button.</value>
    public LinkButton DeleteEntryButton
    {
      get
      {
        if (this.deleteEntryButton == null)
          this.deleteEntryButton = this.Container.GetControl<LinkButton>("deleteButton", true);
        return this.deleteEntryButton;
      }
    }

    /// <summary>Gets the edit entry button control.</summary>
    /// <value>The edit entry button.</value>
    public LinkButton EditEntryButton
    {
      get
      {
        if (this.editEntryButton == null)
          this.editEntryButton = this.Container.GetControl<LinkButton>("editButton", true);
        return this.editEntryButton;
      }
    }

    /// <summary>Gets or sets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormsDetailView.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the label with the referral code of the entry.</summary>
    /// <value></value>
    protected internal Label ReferralCodeLabel => this.Container.GetControl<Label>("referralCodeLabel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      FormsDetailViewDefinition formsDetailDefinition = (FormsDetailViewDefinition) definition;
      this.ServiceUrl = this.DeterminWebServiceUrl(formsDetailDefinition);
      this.FormName = formsDetailDefinition.FormName;
      this.formFieldControls.Add((IFormFieldControl) this.DetailsField);
      this.RenderFormFields();
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value>The layout template name.</value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    private List<string> GetRequireDataItemControlIds()
    {
      List<string> dataItemControlIds = new List<string>();
      foreach (IFormFieldControl formFieldControl in this.formFieldControls)
      {
        if (formFieldControl.GetType().GetCustomAttributes(typeof (RequiresDataItemAttribute), true).Length != 0)
          dataItemControlIds.Add(((Control) formFieldControl).ClientID);
      }
      return dataItemControlIds;
    }

    private string DeterminWebServiceUrl(FormsDetailViewDefinition formsDetailDefinition)
    {
      string str1 = string.Empty;
      string str2 = string.Empty;
      string providerName = this.Host.ControlDefinition.ProviderName;
      string empty = string.Empty;
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      if (this.Host.ControlDefinition.ContentType != (Type) null)
        str1 = this.Host.ControlDefinition.ContentType.FullName;
      if (this.Host.ControlDefinition.ManagerType != (Type) null)
        str2 = this.Host.ControlDefinition.ManagerType.FullName;
      string webServiceBaseUrl = formsDetailDefinition.WebServiceBaseUrl;
      if (!webServiceBaseUrl.EndsWith("/"))
        webServiceBaseUrl += "/";
      string str3 = webServiceBaseUrl + string.Format("?itemType={0}&providerName={1}&managerType={2}&siteId={3}", (object) str1, (object) providerName, (object) str2, (object) id);
      formsDetailDefinition.WebServiceBaseUrl = str3;
      return str3;
    }

    private void RenderFormFields()
    {
      FormsManager manager = FormsManager.GetManager();
      FormDescription formByName = manager.GetFormByName(this.FormName);
      if (formByName != null)
      {
        if (!formByName.IsGranted("Forms", "ManageResponses"))
        {
          this.EditEntryButton.Visible = false;
          this.DeleteEntryButton.Visible = false;
        }
      }
      List<IControlsContainer> controlsContainerList = new List<IControlsContainer>();
      controlsContainerList.Add((IControlsContainer) formByName);
      PageHelper.ProcessControls((IList<ControlData>) new List<ControlData>(), (IList<ControlData>) new List<ControlData>(), (IList<IControlsContainer>) controlsContainerList);
      PlaceHoldersCollection holdersCollection = new PlaceHoldersCollection();
      PlaceHolder placeHolder1 = new PlaceHolder();
      placeHolder1.ID = "Body";
      PlaceHolder child1 = placeHolder1;
      PlaceHolder placeHolder2 = new PlaceHolder();
      placeHolder2.ID = "Header";
      PlaceHolder child2 = placeHolder2;
      PlaceHolder placeHolder3 = new PlaceHolder();
      placeHolder3.ID = "Footer";
      PlaceHolder child3 = placeHolder3;
      holdersCollection.Add((Control) child2);
      holdersCollection.Add((Control) child1);
      holdersCollection.Add((Control) child3);
      List<ControlData> controlDataList = PageHelper.SortControls(controlsContainerList.AsEnumerable<IControlsContainer>().Reverse<IControlsContainer>(), controlsContainerList.Count);
      IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
      IFormFieldBackendConfigurator backendConfigurator = ObjectFactory.Resolve<IFormFieldBackendConfigurator>();
      foreach (ControlData controlData in controlDataList)
      {
        Control control1 = manager.LoadControl((ObjectData) controlData, (CultureInfo) null);
        object behaviorObject = behaviorResolver.GetBehaviorObject(control1);
        if (control1 != null && behaviorObject != null)
        {
          bool flag = true;
          object[] customAttributes = behaviorObject.GetType().GetCustomAttributes(typeof (FormControlDisplayModeAttribute), true);
          if (customAttributes.Length != 0 && ((FormControlDisplayModeAttribute) customAttributes[0]).FormControlDisplayMode == FormControlDisplayMode.Write)
            flag = false;
          Control control2;
          if (flag && holdersCollection.TryGetValue(controlData.PlaceHolder, out control2))
          {
            Control child4 = backendConfigurator.ConfigureFormControl(control1, formByName.Id);
            if (child4 != null)
            {
              control2.Controls.Add(child4);
              if (child4 is IFormFieldControl formFieldControl)
              {
                this.formFieldControls.Add(formFieldControl);
                if (child4 is FieldControl fieldControl)
                {
                  fieldControl.DataFieldName = formFieldControl.MetaField.FieldName;
                  fieldControl.DisplayMode = FieldDisplayMode.Read;
                }
              }
            }
            else
              continue;
          }
          if (control1 is LayoutControl)
          {
            LayoutControl layoutControl = (LayoutControl) control1;
            layoutControl.PlaceHolder = controlData.PlaceHolder;
            holdersCollection.AddRange((IEnumerable<Control>) layoutControl.Placeholders);
          }
        }
      }
      this.FieldsHolder.Controls.Add((Control) child2);
      this.FieldsHolder.Controls.Add((Control) child1);
      this.FieldsHolder.Controls.Add((Control) child3);
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
