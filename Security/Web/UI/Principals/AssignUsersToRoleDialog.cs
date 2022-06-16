// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Principals.AssignUsersToRoleDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Security.Web.UI.Principals
{
  /// <summary>
  /// Provides a UI for selecting users and assigning them to role
  /// </summary>
  public class AssignUsersToRoleDialog : SimpleScriptView
  {
    /// <summary>Javascript embedded component path</summary>
    public const string JsComponentPath = "Telerik.Sitefinity.Security.Web.UI.Scripts.AssignUsersToRoleDialog.js";
    /// <summary>Path to the embedded layout template</summary>
    public static readonly string EmbeddedTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Principals.AssignUsersToRoleDialog.ascx");
    private string allUsersTitle;
    private string usersInRoleTitle;
    private Label allUsersLabel;
    private Label usersInRoleLabel;
    private int usersPerPage;
    private RadGrid usersInRoleGrid;
    private RadGrid allUsersGrid;
    private bool useClientSelectColumnOnly;
    private JavaScriptSerializer jsSerializer;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Principals.AssignUsersToRoleDialog" /> class.
    /// </summary>
    public AssignUsersToRoleDialog()
    {
      this.allUsersTitle = Res.Get<SecurityResources>().AllUsersCount;
      this.usersInRoleTitle = Res.Get<SecurityResources>().UsersInRoleNameCount;
      this.usersPerPage = 20;
      this.useClientSelectColumnOnly = true;
      this.Title = Res.Get<SecurityResources>().AssignOrUnassignUsersToRoleName;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.allUsersLabel = (Label) this.TabSwitcher.Tabs[0].FindControl("allUsers");
      this.usersInRoleLabel = (Label) this.TabSwitcher.Tabs[1].FindControl("usersInRole");
      this.allUsersGrid = (RadGrid) this.ModeContainer.PageViews[0].FindControl("allUsersGrid");
      this.usersInRoleGrid = (RadGrid) this.ModeContainer.PageViews[0].FindControl("usersInRoleGrid");
      this.allUsersGrid.ClientSettings.Selecting.UseClientSelectColumnOnly = this.UseClientSelectColumnOnly;
      this.usersInRoleGrid.ClientSettings.Selecting.UseClientSelectColumnOnly = this.UseClientSelectColumnOnly;
      this.allUsersLabel.Text = this.allUsersTitle;
      this.usersInRoleLabel.Text = this.usersInRoleTitle;
      this.allUsersGrid.PageSize = this.UsersPerPage;
      this.usersInRoleGrid.PageSize = this.UsersPerPage;
      UserManager manager = UserManager.GetManager();
      List<string> list = manager.GetContextProviders().Where<DataProviderBase>((Func<DataProviderBase, bool>) (p => p.ProviderGroup != "System")).Select<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Name)).ToList<string>();
      list.Insert(0, Res.Get<Labels>().AllProvidersText);
      this.ProvidersList.DataSource = (object) list;
      this.ProvidersList.DataBind();
      this.ProvidersList.SelectedValue = manager.GetDefaultContextProvider().Name;
      if (list.Count >= 3)
        return;
      this.ProvidersList.Attributes.Add("style", "display:none");
    }

    /// <summary>
    /// Get/set whether to show the role name in the "selected users" tab
    /// </summary>
    /// <value>True if role name should be shown, false otherwize</value>
    public bool ShowRowNameInTitle { get; set; }

    /// <summary>Get/set the title of the "all users" tab</summary>
    /// <value>The can optionally contain a placeholder - {0} - for the count of users</value>
    public string AllUsersTitle
    {
      get => this.allUsersTitle;
      set => this.allUsersTitle = value;
    }

    /// <summary>Get/set the title of the "users in role" tab</summary>
    /// <value>
    /// <para>Should contain two placeholdes: {0} and {1}</para>
    /// <para>{0} will contain the role name</para>
    /// <para>{1} will contain the number of users in that role</para>
    /// </value>
    public string UsersInRoleTitle
    {
      get => this.usersInRoleTitle;
      set => this.usersInRoleTitle = value;
    }

    /// <summary>Initial filter expression</summary>
    public string FilterExpression { get; set; }

    /// <summary>Initial sort expression</summary>
    public string SortExpression { get; set; }

    /// <summary>
    /// Paging: determine how many users should a page display at maximum
    /// </summary>
    public int UsersPerPage
    {
      get => this.usersPerPage;
      set => this.usersPerPage = value;
    }

    /// <summary>
    /// Enable/disable the RadGrid "feature" to deselect everything when a row is clicked
    /// </summary>
    public bool UseClientSelectColumnOnly
    {
      get => this.useClientSelectColumnOnly;
      set => this.useClientSelectColumnOnly = value;
    }

    /// <summary>
    /// Get or set the dialog title template string. It should contain a placeholder for the role name - {0}
    /// </summary>
    public string Title { get; set; }

    /// <summary>Get all user providers</summary>
    /// <returns>List of all providers' names, serialized to JSON</returns>
    private string GetClientUserProviderNames()
    {
      List<string> list = UserManager.GetManager().GetContextProviders().Select<DataProviderBase, string>((Func<DataProviderBase, string>) (p => p.Name)).ToList<string>();
      list.Insert(0, Res.Get<Labels>().AllProvidersText);
      return this.GetJavascriptSerializer().Serialize((object) list);
    }

    /// <summary>Retrieve an instance of the js serialzier</summary>
    /// <returns>Instance of the JS serialzier</returns>
    private JavaScriptSerializer GetJavascriptSerializer()
    {
      if (this.jsSerializer == null)
        this.jsSerializer = new JavaScriptSerializer();
      return this.jsSerializer;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor("Telerik.Sitefinity.Web.UI.AssignUsersToRoleDialog", this.ClientID);
      controlDescriptor.AddProperty("_allUsersLabel", (object) this.allUsersLabel.ClientID);
      controlDescriptor.AddProperty("_usersInRoleLabel", (object) this.usersInRoleLabel.ClientID);
      controlDescriptor.AddProperty("_allUsersGrid", (object) this.allUsersGrid.ClientID);
      controlDescriptor.AddProperty("_inRoleGrid", (object) this.usersInRoleGrid.ClientID);
      controlDescriptor.AddProperty("_allUsersTitle", (object) this.allUsersTitle);
      controlDescriptor.AddProperty("_usersInRoleTitle", (object) this.usersInRoleTitle);
      controlDescriptor.AddProperty("_tabSwithcer", (object) this.TabSwitcher.ClientID);
      controlDescriptor.AddProperty("_allUsersBinder", (object) this.ModeContainer.PageViews[0].FindControl("allUsersBinder").ClientID);
      controlDescriptor.AddProperty("_inRoleBinder", (object) this.ModeContainer.PageViews[1].FindControl("usersInRoleBinder").ClientID);
      controlDescriptor.AddProperty("_rolesServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Security/Roles.svc"));
      controlDescriptor.AddProperty("_usersServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Security/Users.svc"));
      controlDescriptor.AddProperty("_filterExpression", (object) (this.FilterExpression ?? ""));
      controlDescriptor.AddProperty("_sortExpression", (object) (this.SortExpression ?? ""));
      controlDescriptor.AddProperty("_messageControl", (object) this.MessageControl.ClientID);
      controlDescriptor.AddProperty("_providersList", (object) this.ProvidersList.ClientID);
      controlDescriptor.AddProperty("_allUserProviders", (object) this.GetClientUserProviderNames());
      controlDescriptor.AddProperty("_searchBox", (object) this.SearchBox.ClientID);
      controlDescriptor.AddProperty("_title", (object) this.Title);
      controlDescriptor.AddProperty("_titleLabel", (object) this.DialogTitleLabel.ClientID);
      controlDescriptor.AddProperty("_saveButton", (object) this.SaveChanges.ClientID);
      controlDescriptor.AddProperty("_cancelButton", (object) this.CancelChanges.ClientID);
      controlDescriptor.AddProperty("_showRowNameInTitle", (object) this.ShowRowNameInTitle);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
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
      ScriptReference scriptReference = new ScriptReference("Telerik.Sitefinity.Security.Web.UI.Scripts.AssignUsersToRoleDialog.js", typeof (AssignUsersToRoleDialog).Assembly.GetName().FullName);
      scriptReference.NotifyScriptLoaded = false;
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        scriptReference
      };
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? AssignUsersToRoleDialog.EmbeddedTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Returns DIV =&gt; so that the wrapping tag is rendered as "div"
    /// </summary>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Reference to the template control that swithces the "All users" and "Users in role" tabs
    /// </summary>
    protected virtual RadTabStrip TabSwitcher => this.Container.GetControl<RadTabStrip>("tabSwitcher", true);

    /// <summary>
    /// Reference to the template control that contains the two grids: "all users" and "users in role"
    /// </summary>
    protected virtual RadMultiPage ModeContainer => this.Container.GetControl<RadMultiPage>("modeContainer", true);

    /// <summary>
    /// Reference to the template control that will display status messages
    /// </summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>
    /// Reference to the template control that will display statys messages
    /// </summary>
    protected virtual DropDownList ProvidersList => this.Container.GetControl<DropDownList>("providersList", true);

    /// <summary>
    /// Reference to the template control that will be used to search (filter) users
    /// </summary>
    protected virtual BinderSearchBox SearchBox => this.Container.GetControl<BinderSearchBox>("searchBox", true);

    /// <summary>
    /// Reference to the template control that will be used to display the dialog title
    /// </summary>
    protected virtual Label DialogTitleLabel => this.Container.GetControl<Label>("dialogTitleLabel", true);

    /// <summary>
    /// Reference to the template control that will trigger the saving of changes
    /// </summary>
    protected virtual Control SaveChanges => this.Container.GetControl<Control>("saveChanges", true);

    /// <summary>
    /// Reference to the template contorl that will triger the closing of the dialog
    /// </summary>
    protected virtual Control CancelChanges => this.Container.GetControl<Control>("cancelChanges", true);
  }
}
