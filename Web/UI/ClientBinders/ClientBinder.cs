// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ClientBinder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ClientBinders;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// This control is used to bind data from a restful web service to a control which provides
  /// client side object implementing IClientBinder interface.
  /// </summary>
  [ToolboxBitmap(typeof (ClientBinder), "Telerik.Sitefinity.Resources.Sitefinity.bmp")]
  [ToolboxData("<{0}:ClientBinder runat=\"server\"></{0}:ClientBinder>")]
  [DefaultProperty("DataService")]
  [ParseChildren(true)]
  public abstract class ClientBinder : CompositeControl, IScriptControl
  {
    private const string actionsMenuJsPath = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js";
    private const string asyncCommandReceiver = "Telerik.Sitefinity.Web.Scripts.IAsyncCommandReceiver.js";
    private bool? supportsMultiligual;
    private Collection<BinderContainer> containers;

    /// <summary>Gets or sets the service URL.</summary>
    /// <value>The service URL.</value>
    public string ServiceUrl { get; set; }

    /// <summary>Gets or sets the manager type.</summary>
    /// <value>The manager type.</value>
    public string ManagerType { get; set; }

    /// <summary>
    /// Gets or sets the ID of the control that binder will bind to data.
    /// </summary>
    public string TargetId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the RadAjaxLoadingPanel control that will be displayed during async calls and binding operations.
    /// </summary>
    public string LoadingPanelID { get; set; }

    /// <summary>
    /// Gets or sets the data members. DataMembers are the names of the properties
    /// which are used in the binder (to be displayed, edited or inserted). Use comma
    /// to separate keys, if data item has more than one data member.
    /// </summary>
    /// <remarks>You do not have to specify DataKeys as DataMembers.</remarks>
    public string DataMembers { get; set; }

    /// <summary>
    /// Gets or sets the data key names. DataKey names are the names of the properties
    /// which define the data item primary key. Use comma to separate keys, if data item
    /// is defined by more than one key.
    /// </summary>
    /// <remarks>
    /// Specifying data key names is obligatory for the automatic create, update and delete
    /// functions of the client binders.
    /// </remarks>
    public string DataKeyNames { get; set; }

    /// <summary>Gets or sets the type of the data to be bound.</summary>
    /// <value>The type of the data.</value>
    public Type DataType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this control supports multiligual.
    /// </summary>
    /// <value><c>true</c> if supports multiligual; otherwise, <c>false</c>.</value>
    public bool SupportsMultiligual
    {
      get
      {
        if (!this.supportsMultiligual.HasValue)
          this.supportsMultiligual = new bool?(SystemManager.CurrentContext.AppSettings.Multilingual);
        return this.supportsMultiligual.Value;
      }
      set => this.supportsMultiligual = new bool?(value);
    }

    /// <summary>
    /// Gets or sets the container tag which will wrap the client template.
    /// </summary>
    /// <remarks>
    /// Use HtmlTextWriterTag enumeration. For example, if your target is an unordered
    /// list, the ContainerTag should be "li".
    /// </remarks>
    public HtmlTextWriterTag ContainerTag { get; set; }

    /// <summary>
    /// Gets the collection of binder containers defined by the concrete implementation of the
    /// ClientBinder class.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<BinderContainer> Containers
    {
      get
      {
        if (this.containers == null)
          this.containers = new Collection<BinderContainer>();
        return this.containers;
      }
    }

    /// <summary>
    /// Gets the name of the javascript class exposed by the concrete implementation of the
    /// ClientBinder name.
    /// </summary>
    protected abstract string BinderName { get; }

    /// <summary>Gets or sets the default sort expression.</summary>
    public string DefaultSortExpression { get; set; }

    /// <summary>
    /// Gets or sets the pageId of the control that triggers the SaveChanges operation
    /// on the ClientBinder.
    /// </summary>
    public string SaveInvokerId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the binder should bind the target
    /// on the page load.
    /// </summary>
    public bool BindOnLoad { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether client binder will perform an automatic deletion.
    /// </summary>
    /// <value>
    /// <c>true</c> if [allow automatic delete]; otherwise, <c>false</c>.
    /// </value>
    public bool AllowAutomaticDelete { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client DataBound event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientDataBinding { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client DataBound event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientDataBound { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client ItemDataBinding event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientItemDataBinding { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client ItemDataBound event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientItemDataBound { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client ItemEditCommand event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientItemEditCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client ItemDeleteCommand event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientItemDeleteCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the client side function to be called when client ItemSelectCommand event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientItemSelectCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the client side function to be called when client ItemCancelCommand event is raised.
    /// </summary>
    /// <value>Name of the javascript function to be called.</value>
    public string OnClientItemCancelCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the client side function to be called when client ItemSaveCommand event is raised.
    /// </summary>
    /// <value>Name of the javascript function to be called.</value>
    public string OnClientItemSaveCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the client side function to be called when client ItemCommand event is raised.
    /// </summary>
    /// <value>Name of the javascript function to be called.</value>
    public string OnClientItemCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client ItemSaving event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientItemSaving { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client Deleting event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientDeleting { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client Deleted event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientDeleted { get; set; }

    /// <summary>
    /// Gets or sets the name of the on client side function to be called when client TargetCommand event is raised.
    /// </summary>
    /// <value>Name of the javascript function be called.</value>
    public string OnClientTargetCommand { get; set; }

    /// <summary>
    /// Gets or sets the name of the javascript function that will be called just after client binder
    /// has been initialized, but before any other action. This function can be used for specifying
    /// properties of the ClientBinder before the actual binding.
    /// </summary>
    public string OnClientBinderInitialized { get; set; }

    /// <summary>
    /// Gets or sets the name of the javascript function that will be callend just before
    /// the binder saves an event. This function can be used for performing clients actions
    /// before the automatic save is performed by the client.
    /// </summary>
    /// <remarks>
    /// A typical usage of this event is to merge data from the different forms previous
    /// to save.
    /// </remarks>
    public string OnClientSaving { get; set; }

    /// <summary>
    /// Gets or sets the name of the javascript function that will be called when the binder
    /// saves an event. This function can be used for performing client actions after the automatic
    /// save performed by the ClientBinder.
    /// </summary>
    public string OnClientSaved { get; set; }

    /// <summary>
    /// Gets or sets the name of the javascript function that will be called when there is a error raised from
    /// the WCF service.
    /// </summary>
    public string OnClientError { get; set; }

    /// <summary>
    /// Gets or sets the default filter expression for this binder.
    /// </summary>
    public string FilterExpression { get; set; }

    /// <summary>Gets or sets the culture used by the client manager.</summary>
    public string Culture { get; set; }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture { get; set; }

    /// <summary>Gets or sets the provider.</summary>
    public string Provider { get; set; }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the
    /// specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents
    /// the output stream to render HTML content on the client.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer.
    /// This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents
    /// the output stream to render HTML content on the client.
    /// </param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      PageManager.ConfigureScriptManager(this.Page, this.GetRequiredCoreScripts()).RegisterScriptControl<ClientBinder>(this);
      if (string.IsNullOrEmpty(this.ServiceUrl))
        throw new InvalidOperationException(Res.Get<ErrorMessages>().RequiredPropertyNotDefined.Arrange((object) "ServiceUrl", (object) this.ID));
      if (this.IsDesignMode() && !this.IsInlineEditingMode())
        return;
      HtmlGenericControl child = new HtmlGenericControl("style");
      child.ID = "templateHider";
      child.Attributes["type"] = "text/css";
      child.InnerText = ".sys-template { display:none; position: relative; }";
      if (this.Page == null || this.Page.Header == null || this.Page.Header.FindControl("templateHider") != null)
        return;
      this.Page.Header.Controls.Add((Control) child);
    }

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.MicrosoftAjaxTemplates |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected virtual ScriptRef GetRequiredCoreScripts() => ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxTemplates | ScriptRef.MicrosoftAjaxWebForms | ScriptRef.JQuery | ScriptRef.JQueryValidate;

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use
    /// composition-based implementation to create any child controls they contain in
    /// preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      this.Controls.Clear();
      foreach (BinderContainer container in this.Containers)
      {
        if (this.ContainerTag != HtmlTextWriterTag.Unknown)
        {
          container.ContainerTag = this.ContainerTag;
          container.DataKeyNames = this.DataKeyNames;
        }
        this.Controls.Add((Control) container);
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript)
    /// client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.BinderName, this.ClientID);
      if (!string.IsNullOrEmpty(this.LoadingPanelID))
      {
        Control controlRecursive = this.FindControlRecursive((Control) this, this.LoadingPanelID);
        if (controlRecursive != null)
          behaviorDescriptor.AddProperty("_loadingPanelID", (object) controlRecursive.ClientID);
      }
      behaviorDescriptor.AddProperty("id", (object) this.ClientID);
      behaviorDescriptor.AddProperty("targetId", (object) this.GetActualTargetId());
      object obj1 = (object) ClientBinder.DeserializeStringArray(this.DataKeyNames);
      if (obj1 != null)
        behaviorDescriptor.AddProperty("dataKeyNames", obj1);
      object obj2 = (object) ClientBinder.DeserializeStringArray(this.DataMembers);
      if (obj2 != null)
        behaviorDescriptor.AddProperty("dataMembers", obj2);
      behaviorDescriptor.AddProperty("serviceBaseUrl", this.ServiceUrl.StartsWith("~", StringComparison.Ordinal) ? (object) this.Page.ResolveUrl(this.ServiceUrl) : (object) this.ServiceUrl);
      behaviorDescriptor.AddProperty("managerType", (object) this.ManagerType);
      behaviorDescriptor.AddProperty("clientTemplates", (object) ClientBinder.GetClientTemplateNames((IList<BinderContainer>) this.Containers));
      behaviorDescriptor.AddProperty("sortExpression", (object) this.DefaultSortExpression);
      behaviorDescriptor.AddProperty("filterExpression", (object) this.FilterExpression);
      behaviorDescriptor.AddProperty("bindOnLoad", (object) this.BindOnLoad);
      behaviorDescriptor.AddProperty("allowAutomaticDelete", (object) this.AllowAutomaticDelete);
      behaviorDescriptor.AddProperty("dataType", this.DataType == (Type) null ? (object) (string) null : (object) this.DataType.FullName);
      if (!string.IsNullOrEmpty(this.SaveInvokerId))
        behaviorDescriptor.AddProperty("saveInvokerId", (object) this.FindControlRecursive((Control) this.Page, this.SaveInvokerId).ClientID);
      behaviorDescriptor.AddProperty("_checkboxClass", (object) CssConstants.CheckBox);
      behaviorDescriptor.AddProperty("_dropdownClass", (object) CssConstants.Dropdown);
      behaviorDescriptor.AddProperty("isMultilingual", (object) this.SupportsMultiligual);
      if (!string.IsNullOrEmpty(this.Culture))
        behaviorDescriptor.AddProperty("culture", (object) this.Culture);
      if (!string.IsNullOrEmpty(this.UICulture))
        behaviorDescriptor.AddProperty("uiCulture", (object) this.UICulture);
      if (!string.IsNullOrEmpty(this.Provider))
        behaviorDescriptor.AddProperty("provider", (object) this.Provider);
      if (!string.IsNullOrEmpty(this.OnClientBinderInitialized))
        behaviorDescriptor.AddEvent("onBinderInitialized", this.OnClientBinderInitialized);
      if (!string.IsNullOrEmpty(this.OnClientDataBinding))
        behaviorDescriptor.AddEvent("onDataBinding", this.OnClientDataBinding);
      if (!string.IsNullOrEmpty(this.OnClientDataBound))
        behaviorDescriptor.AddEvent("onDataBound", this.OnClientDataBound);
      if (!string.IsNullOrEmpty(this.OnClientItemDataBinding))
        behaviorDescriptor.AddEvent("onItemDataBinding", this.OnClientItemDataBinding);
      if (!string.IsNullOrEmpty(this.OnClientItemDataBound))
        behaviorDescriptor.AddEvent("onItemDataBound", this.OnClientItemDataBound);
      if (!string.IsNullOrEmpty(this.OnClientTargetCommand))
        behaviorDescriptor.AddEvent("onTargetCommand", this.OnClientTargetCommand);
      if (!string.IsNullOrEmpty(this.OnClientItemEditCommand))
        behaviorDescriptor.AddEvent("onItemEditCommand", this.OnClientItemEditCommand);
      if (!string.IsNullOrEmpty(this.OnClientItemDeleteCommand))
        behaviorDescriptor.AddEvent("onItemDeleteCommand", this.OnClientItemDeleteCommand);
      if (!string.IsNullOrEmpty(this.OnClientItemSelectCommand))
        behaviorDescriptor.AddEvent("onItemSelectCommand", this.OnClientItemSelectCommand);
      if (!string.IsNullOrEmpty(this.OnClientItemCancelCommand))
        behaviorDescriptor.AddEvent("onItemCancelCommand", this.OnClientItemCancelCommand);
      if (!string.IsNullOrEmpty(this.OnClientItemSaveCommand))
        behaviorDescriptor.AddEvent("onItemSaveCommand", this.OnClientItemSaveCommand);
      if (!string.IsNullOrEmpty(this.OnClientItemCommand))
        behaviorDescriptor.AddEvent("onItemCommand", this.OnClientItemCommand);
      if (!string.IsNullOrEmpty(this.OnClientItemSaving))
        behaviorDescriptor.AddEvent("onItemSaving", this.OnClientItemSaving);
      if (!string.IsNullOrEmpty(this.OnClientDeleting))
        behaviorDescriptor.AddEvent("onDeleting", this.OnClientDeleting);
      if (!string.IsNullOrEmpty(this.OnClientDeleted))
        behaviorDescriptor.AddEvent("onDeleted", this.OnClientDeleted);
      if (!string.IsNullOrEmpty(this.OnClientSaving))
        behaviorDescriptor.AddEvent("onSaving", this.OnClientSaving);
      if (!string.IsNullOrEmpty(this.OnClientSaved))
        behaviorDescriptor.AddEvent("onSaved", this.OnClientSaved);
      if (!string.IsNullOrEmpty(this.OnClientError))
        behaviorDescriptor.AddEvent("onError", this.OnClientError);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    private string GetActualTargetId()
    {
      if (string.IsNullOrEmpty(this.TargetId))
        return string.Empty;
      string actualTargetId = this.TargetId;
      Control controlRecursive = this.FindControlRecursive((Control) this, this.TargetId);
      if (controlRecursive != null)
        actualTargetId = controlRecursive.ClientID;
      return actualTargetId;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define
    /// script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of
    /// <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
        },
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ClientBinder.js"
        },
        new ScriptReference()
        {
          Assembly = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().FullName,
          Name = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js"
        },
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.IAsyncCommandReceiver.js"
        }
      }.ToArray();
    }

    /// <summary>
    /// Serializes the binder templates and returns them as a string ready for javascript.
    /// </summary>
    /// <param name="templates">The list of binder template objects.</param>
    /// <returns>
    /// Returns 'null' if no templates are present. Otherwise returns a javascript array with the
    /// names of all the clientside templates.
    /// </returns>
    protected static string[] GetClientTemplateNames(IList<BinderContainer> templates)
    {
      List<string> stringList = new List<string>();
      foreach (BinderContainer template in (IEnumerable<BinderContainer>) templates)
        stringList.Add(template.BinderContainerId);
      return stringList.ToArray();
    }

    /// <summary>Gets the target.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected T GetTarget<T>() where T : Control
    {
      if (string.IsNullOrEmpty(this.TargetId))
        throw new InvalidOperationException(Res.Get<ErrorMessages>().RequiredPropertyNotDefined.Arrange((object) "TargetId", (object) this.ID));
      return this.FindControlRecursive((Control) this, this.TargetId) is T controlRecursive ? controlRecursive : throw new InvalidOperationException(Res.Get<ErrorMessages>().RequiredControlNotFound.Arrange((object) this.ID, (object) typeof (T).Name, (object) this.TargetId));
    }

    internal Control FindControlRecursive(Control searcher, string ID)
    {
      if (searcher.NamingContainer != null)
      {
        Control control = searcher.NamingContainer.FindControl(ID);
        if (control != null)
          return control;
      }
      Control root = searcher.Page.Master == null ? (Control) searcher.Page : (Control) searcher.Page.Master;
      Control control1 = root.FindControl(ID);
      if (control1 != null)
        return control1;
      Control control2 = searcher.Page.FindControl(ID);
      if (control2 != null)
        return control2;
      return searcher.UniqueID == ID || searcher.ClientID == ID ? searcher : this.FindControlRecursive(ID, root);
    }

    private Control FindControlRecursive(string ID, Control root)
    {
      Control controlRecursive = (Control) null;
      if (root is DataBoundControl && !root.Visible)
        return controlRecursive;
      foreach (Control control in root.Controls)
      {
        if (control is INamingContainer && control.FindControl(ID) != null)
        {
          controlRecursive = control.FindControl(ID);
          break;
        }
        if (control.HasControls())
        {
          controlRecursive = this.FindControlRecursive(ID, control);
          if (controlRecursive != null)
          {
            if (!(controlRecursive.UniqueID == ID))
            {
              if (controlRecursive.ID == ID)
                break;
            }
            else
              break;
          }
        }
      }
      return controlRecursive;
    }

    /// <summary>Serializes an object graph to json string.</summary>
    /// <param name="valueForSerialization">The object for serialization.</param>
    /// <returns>Json representation of the object.</returns>
    protected static string SerializeToJson(object valueForSerialization)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        new DataContractJsonSerializer(valueForSerialization.GetType()).WriteObject((Stream) memoryStream, valueForSerialization);
        return Encoding.Default.GetString(memoryStream.ToArray());
      }
    }

    /// <summary>
    /// Deserializes the comma delimited list into a string array.
    /// </summary>
    /// <param name="expression">The comma delimited expression.</param>
    /// <returns>Array of strings.</returns>
    protected static string[] DeserializeStringArray(string expression)
    {
      if (string.IsNullOrEmpty(expression))
        return (string[]) null;
      List<string> stringList = new List<string>();
      string str1 = expression;
      char[] chArray = new char[1]{ ',' };
      foreach (string str2 in str1.Split(chArray))
      {
        if (str2.Trim().Length > 0)
          stringList.Add(str2.Trim());
      }
      return stringList.ToArray();
    }
  }
}
