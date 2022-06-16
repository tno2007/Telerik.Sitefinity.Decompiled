// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.FieldControls;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>Control used to render the form fields of a form.</summary>
  public class FormEntryEditControl : FormsControl, IScriptControl, IFormEntryEditControl
  {
    private FieldControlsBinder fieldsBinder;
    private const string script = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormEntryEditControl.js";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormEntryEditControl.ascx");
    private Button submitButton;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl" /> class.
    /// </summary>
    public FormEntryEditControl() => this.LayoutTemplatePath = FormEntryEditControl.layoutTemplatePath;

    /// <summary>Gets or sets the field controls binder.</summary>
    /// <value>The fields binder.</value>
    public FieldControlsBinder FieldsBinder
    {
      get
      {
        if (this.fieldsBinder == null)
          this.fieldsBinder = this.Container.GetControl<FieldControlsBinder>("fieldsBinder", true);
        return this.fieldsBinder;
      }
    }

    /// <summary>Gets the submit button control.</summary>
    public Button SubmitButton => this.Container.GetControl<Button>("submitButton", true);

    /// <summary>Gets the cancel link control.</summary>
    /// <value>The cancel link.</value>
    public HyperLink CancelLink => this.Container.GetControl<HyperLink>("cancelLink", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InitializeBinder();
      this.SetUpToolbar();
      base.InitializeControls(container);
    }

    private void InitializeBinder() => this.FieldsBinder.ServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Forms/FormsService.svc/entry/") + this.FormName + "/?itemType=" + this.FormData.EntriesTypeName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    protected override void ConfigureSubmitButton(Control control, string validationGroup) => control.Visible = false;

    /// <summary>
    /// Configures the form control before adding it to the form.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>The control that should be added.</returns>
    protected override Control ConfigureFormControl(Control control) => control == null ? (Control) null : ObjectFactory.Resolve<IFormFieldBackendConfigurator>().ConfigureFormControl(control, this.FormData.Id);

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e)
    {
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxWebForms | ScriptRef.JQuery | ScriptRef.JQueryValidate | ScriptRef.TelerikSitefinity | ScriptRef.JQueryCookie);
      base.OnInit(e);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public new IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      FormEntryEditControl entryEditControl = this;
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(entryEditControl.GetType().FullName, entryEditControl.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      FormEntry blankEntry = entryEditControl.CreateBlankEntry();
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(blankEntry.GetType(), (IEnumerable<Type>) new Type[1]
        {
          typeof (FormEntry)
        }).WriteObject((Stream) memoryStream, (object) blankEntry);
        controlDescriptor.AddProperty("_blankDataItem", (object) Encoding.Default.GetString(memoryStream.ToArray()));
      }
      controlDescriptor.AddProperty("fieldControlIds", (object) scriptSerializer.Serialize((object) entryEditControl.GetFieldControlsClientIds()));
      controlDescriptor.AddProperty("requireDataItemFieldControlIds", (object) scriptSerializer.Serialize((object) entryEditControl.GetRequireDataItemFieldControlClientIds()));
      controlDescriptor.AddComponentProperty("binder", entryEditControl.FieldsBinder.ClientID);
      controlDescriptor.AddElementProperty("submitButton", entryEditControl.SubmitButton.ClientID);
      controlDescriptor.AddElementProperty("cancelLink", entryEditControl.CancelLink.ClientID);
      yield return (ScriptDescriptor) controlDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public new IEnumerable<ScriptReference> GetScriptReferences()
    {
      yield return new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormEntryEditControl.js", typeof (FormEntryEditControl).Assembly.FullName);
    }

    private List<string> GetFieldControlsClientIds()
    {
      List<string> controlsClientIds = new List<string>();
      foreach (Control fieldControl in this.FieldControls)
        controlsClientIds.Add(fieldControl.ClientID);
      return controlsClientIds;
    }

    private List<string> GetRequireDataItemFieldControlClientIds()
    {
      List<string> controlClientIds = new List<string>();
      foreach (Control fieldControl in this.FieldControls)
      {
        if (fieldControl.GetType().GetCustomAttributes(typeof (RequiresDataItemAttribute), true) != null)
          controlClientIds.Add(fieldControl.ClientID);
      }
      return controlClientIds;
    }

    private FormEntry CreateBlankEntry() => FormsManager.GetManager("", Guid.NewGuid().ToString()).CreateFormEntry(this.FormData.EntriesTypeName);

    private void SetUpToolbar() => this.CancelLink.Text = Res.Get<FormsResources>().Cancel;
  }
}
