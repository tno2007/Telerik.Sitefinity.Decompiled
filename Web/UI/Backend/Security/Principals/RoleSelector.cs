// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Security.Principals.RoleSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security.Web.Services;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Security.Principals
{
  /// <summary>
  /// Control for associating users and roles with actions of a specific permission set's action, for a secured object
  /// </summary>
  public class RoleSelector : FlatSelector
  {
    private string itemTemplate = "{{Name}}";
    private bool showCheckBoxesColumn;
    private bool allowPaging = true;
    private int pageSize = 20;
    private bool showProvidersList = true;
    private bool showSelectedFilter;
    private bool allowSearching = true;
    private string serviceUrl = "~/Sitefinity/Services/Common/GenericItemsService.svc";

    /// <summary>
    /// Gets or sets a value indicating whether to show the check boxes column.
    /// </summary>
    public bool ShowCheckBoxesColumn
    {
      get => this.showCheckBoxesColumn;
      set => this.showCheckBoxesColumn = value;
    }

    /// <summary>Get/set whether to check admin role</summary>
    public bool SelectAdminRoleByDefault { get; set; }

    /// <summary>Get/set whether to hide the admin role</summary>
    public bool HideAdminRole { get; set; }

    /// <summary>Get/set whether to hide the anonymous role</summary>
    public bool HideAnonymousRole { get; set; }

    /// <summary>Gets or sets whether to hide the authenticated role.</summary>
    public bool HideAuthenticatedRole { get; set; }

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

    public bool HideUnasignableRoles { get; set; }

    /// <summary>
    /// Gets or sets the value determining whether the backend system roles should be hidden
    /// </summary>
    public bool HideBackendSystemRoles { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.InclueAllProvidersOption = true;
      this.DataKeyNames = "Id";
      this.BindOnLoad = true;
      this.ItemType = typeof (Role).AssemblyQualifiedName;
      this.ItemSurrogateType = typeof (WcfRole).AssemblyQualifiedName;
      this.DataMembers.Add(new DataMemberInfo()
      {
        ColumnTemplate = this.itemTemplate,
        IsSearchField = true,
        Name = "Name",
        HeaderText = "Name"
      });
      base.ShowSelectedFilter = false;
      this.SelectorGrid.ShowHeader = false;
      if (!this.ShowCheckBoxesColumn)
      {
        GridColumn columnSafe = this.SelectorGrid.MasterTableView.GetColumnSafe("ClientSelectColumn");
        if (columnSafe != null)
          columnSafe.Visible = false;
      }
      if (this.HideAdminRole || this.HideAnonymousRole)
      {
        string str1 = (string) null;
        if (this.HideAdminRole)
          str1 = string.Format("Id != ({0})", (object) SecurityManager.AdminRole.Id);
        if (this.HideAnonymousRole)
        {
          string str2 = string.Format("Id != ({0})", (object) SecurityManager.AnonymousRole.Id);
          str1 = !string.IsNullOrEmpty(str1) ? string.Format("{0} and {1}", (object) str1, (object) str2) : str2;
        }
        if (this.HideAuthenticatedRole)
        {
          string str3 = string.Format("Id != ({0})", (object) SecurityManager.AuthenticatedRole.Id);
          str1 = !string.IsNullOrEmpty(str1) ? string.Format("{0} and {1}", (object) str1, (object) str3) : str3;
        }
        if (string.IsNullOrEmpty(this.ConstantFilter))
          this.ConstantFilter = str1;
        else
          this.ConstantFilter = "(" + this.ConstantFilter + ") and (" + str1 + ")";
      }
      if (this.HideUnasignableRoles)
      {
        StringBuilder stringBuilder = new StringBuilder();
        List<Guid> unassignableRoles = SecurityManager.UnassignableRoles;
        for (int index = 0; index < unassignableRoles.Count - 1; ++index)
          stringBuilder.AppendFormat("Id != ({0}) and ", (object) unassignableRoles[index]);
        stringBuilder.AppendFormat("Id != ({0})", (object) unassignableRoles[unassignableRoles.Count - 1]);
        if (string.IsNullOrEmpty(this.ConstantFilter))
          this.ConstantFilter = stringBuilder.ToString();
        else
          this.ConstantFilter = "(" + this.ConstantFilter + ") and (" + (object) stringBuilder + ")";
      }
      if (this.HideBackendSystemRoles)
      {
        StringBuilder stringBuilder = new StringBuilder();
        if (!this.HideAdminRole)
          stringBuilder.AppendFormat("Id != ({0}) and ", (object) SecurityManager.AdminRole.Id);
        stringBuilder.AppendFormat("Id != ({0}) and ", (object) SecurityManager.BackEndUsersRole.Id);
        stringBuilder.AppendFormat("Id != ({0}) and ", (object) SecurityManager.EditorsRole.Id);
        stringBuilder.AppendFormat("Id != ({0}) and ", (object) SecurityManager.AuthorsRole.Id);
        stringBuilder.AppendFormat("Id != ({0}) ", (object) SecurityManager.DesignersRole.Id);
        if (string.IsNullOrEmpty(this.ConstantFilter))
          this.ConstantFilter = stringBuilder.ToString();
        else
          this.ConstantFilter = "(" + this.ConstantFilter + ") and (" + (object) stringBuilder + ")";
      }
      this.SelectorBinder.DefaultSortExpression = "Name asc";
      base.InitializeControls(container);
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("adminRoleId", (object) SecurityManager.AdminRole.Id);
      controlDescriptor.AddProperty("selectAdminRoleByDefault", (object) this.SelectAdminRoleByDefault);
      controlDescriptor.AddProperty("hideAdminRole", (object) this.HideAdminRole);
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
        Name = "Telerik.Sitefinity.Web.UI.Backend.Security.Principals.Scripts.RoleSelector.js"
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
