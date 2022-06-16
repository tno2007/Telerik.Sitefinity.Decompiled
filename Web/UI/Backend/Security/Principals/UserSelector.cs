// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Security.Principals.UserSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.Services;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Security.Principals
{
  /// <summary>
  /// Control for associating users and roles with actions of a specific permission set's action, for a secured object
  /// </summary>
  public class UserSelector : FlatSelector
  {
    private string itemTemplate = "<strong class='sfItemTitle'>{{Email}}</strong><span class='sfName sfLine'>{{DisplayName}}</span>";
    private bool showCheckBoxesColumn;
    private string sortExpression = "Email";
    private bool allowPaging = true;
    private int pageSize = 20;
    private bool showProvidersList = true;
    private bool showSelectedFilter;
    private bool allowSearching = true;
    private string serviceUrl = "~/Sitefinity/Services/Common/GenericItemsService.svc";

    /// <summary>
    /// Gets or sets a value indicating whether to hide admin users.
    /// </summary>
    /// <value><c>true</c> if admin users should be hiden; otherwise, <c>false</c>.</value>
    public bool HideAdminUsers { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show the check boxes column.
    /// </summary>
    public bool ShowCheckBoxesColumn
    {
      get => this.showCheckBoxesColumn;
      set => this.showCheckBoxesColumn = value;
    }

    /// <summary>
    /// Gets or sets the value determining whether paging will be enabled on the selector
    /// </summary>
    /// <value></value>
    public override bool AllowPaging
    {
      get => this.allowPaging;
      set => base.AllowPaging = value;
    }

    /// <summary>
    /// Gets or sets the value determining the page size if paging is enabled
    /// </summary>
    /// <value></value>
    public override int PageSize
    {
      get => this.pageSize;
      set => this.pageSize = value;
    }

    /// <summary>Gets or sets the selector's markup item template.</summary>
    public string ItemTemplate
    {
      get => this.itemTemplate;
      set => this.itemTemplate = value;
    }

    /// <summary>
    /// Gets or sets the display state of the providers selection box. The default is not to show.
    /// </summary>
    /// <value></value>
    public override bool ShowProvidersList
    {
      get => this.showProvidersList;
      set => this.showProvidersList = value;
    }

    /// <summary>
    /// Gets or sets the value determining whether all items / selected items filter should be
    /// displayed.
    /// </summary>
    /// <value></value>
    public override bool ShowSelectedFilter
    {
      get => this.showSelectedFilter;
      set => this.showSelectedFilter = value;
    }

    /// <summary>
    /// Gets or sets the value determining whether items in selector can be searched
    /// </summary>
    /// <value></value>
    public override bool AllowSearching
    {
      get => this.allowSearching;
      set => this.allowSearching = value;
    }

    /// <summary>Gets or sets the service URL.</summary>
    /// <value>The service URL.</value>
    public override string ServiceUrl
    {
      get => this.serviceUrl;
      set => this.serviceUrl = value;
    }

    /// <summary>
    /// Gets or sets the sortExpression to order the list of returned Users
    /// </summary>
    public string SortExpression
    {
      get => this.sortExpression;
      set => this.sortExpression = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DataKeyNames = "ProviderUserKey";
      this.BindOnLoad = true;
      this.ItemType = typeof (User).AssemblyQualifiedName;
      this.ItemSurrogateType = typeof (WcfMembershipUser).AssemblyQualifiedName;
      this.ProviderName = ManagerBase<MembershipDataProvider>.GetDefaultProviderName();
      this.DataMembers.Add(new DataMemberInfo()
      {
        ColumnTemplate = this.ItemTemplate,
        IsSearchField = true,
        Name = "Email",
        HeaderText = "Email"
      });
      this.SelectorGrid.ShowHeader = false;
      base.ShowSelectedFilter = false;
      if (!this.ShowCheckBoxesColumn)
      {
        GridColumn columnSafe = this.SelectorGrid.MasterTableView.GetColumnSafe("ClientSelectColumn");
        if (columnSafe != null)
          columnSafe.Visible = false;
      }
      this.SelectorBinder.DefaultSortExpression = "";
      base.InitializeControls(container);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("hideAdminUsers", (object) this.HideAdminUsers);
      controlDescriptor.AddProperty("sortExpression", (object) this.SortExpression);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.</returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      ScriptReference scriptReference1 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Security.Principals.Scripts.UserSelector.js"
      };
      ScriptReference scriptReference2 = new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
      };
      return base.GetScriptReferences().Concat<ScriptReference>((IEnumerable<ScriptReference>) new ScriptReference[2]
      {
        scriptReference1,
        scriptReference2
      });
    }
  }
}
