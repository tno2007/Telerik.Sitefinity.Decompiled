// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ItemLists
{
  /// <summary>Control which displays hierarchical items in a grid</summary>
  public class ItemsTreeTable : ItemsListBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ItemLists.ItemsTreeTable.ascx");
    private const string scriptControlPath = "Telerik.Sitefinity.Web.UI.ItemLists.Scripts.ItemsTreeTable.js";
    private string serviceChildItemsBaseUrl;
    private bool hierarchicalTreeRootBindModeEnabled = true;
    private string treeViewCssClass = "sfTreeTable";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ItemsTreeTable.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the base url for retrieving the child items
    /// </summary>
    public string ServiceChildItemsBaseUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.serviceChildItemsBaseUrl))
          this.serviceChildItemsBaseUrl = this.ServiceBaseUrl;
        return this.serviceChildItemsBaseUrl;
      }
      set => this.serviceChildItemsBaseUrl = value;
    }

    /// <summary>Gets or sets the service predecessor base URL.</summary>
    /// <value>The service predecessor base URL.</value>
    /// <remarks>See RadTreeBinder.ServicePredecessorBaseUrl</remarks>
    public string ServicePredecessorBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets the service URL that is serving a tree structure.
    /// </summary>
    /// <value>The service tree URL.</value>
    public string ServiceTreeUrl { get; set; }

    /// <summary>
    /// Name of the node property that contains the parent pageId
    /// </summary>
    public string ParentDataKeyName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether drag-and-drop functionality is enabled.
    /// </summary>
    /// <value><c>true</c> if drag-and-drop functionality is enabled; otherwise, <c>false</c>.</value>
    public bool EnableDragAndDrop { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to store the expansion of the tree per user.
    /// </summary>
    public bool EnableInitialExpanding { get; set; }

    /// <summary>
    /// Gets or sets the name of the cookie that will contain the information of the expanded nodes.
    /// </summary>
    public string ExpandedNodesCookieName { get; set; }

    /// <summary>
    /// Enable or disable BindingMode = HierarchicalTreeRootBind. Default is true.
    /// </summary>
    public bool HierarchicalTreeRootBindModeEnabled
    {
      get => this.hierarchicalTreeRootBindModeEnabled;
      set => this.hierarchicalTreeRootBindModeEnabled = value;
    }

    /// <summary>
    /// Gets or sets the name of the CSS class applied to the tree view.
    /// Default value is sfTreeTable.
    /// </summary>
    /// <value>The CSS class.</value>
    internal string TreeViewCssClass
    {
      get => this.treeViewCssClass;
      set => this.treeViewCssClass = value;
    }

    /// <summary>
    /// Gets the reference to the table header repeater control
    /// </summary>
    protected virtual Repeater TableHeader => this.Container.GetControl<Repeater>("tableHeader", true);

    /// <summary>Gets the referece to the table tree view control</summary>
    protected virtual RadTreeView TreeTable => this.Container.GetControl<RadTreeView>("treeTable", true);

    protected virtual Label EmptyMessage => this.Container.GetControl<Label>("emptyMessage", false);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TableHeader.ItemDataBound += new RepeaterItemEventHandler(this.TableHeader_ItemDataBound);
      base.InitializeControls(container);
      this.Binder.ServiceUrl = this.ServiceBaseUrl;
      RadTreeBinder binder = (RadTreeBinder) this.Binder;
      binder.ServiceChildItemsBaseUrl = this.ServiceChildItemsBaseUrl;
      binder.ServicePredecessorBaseUrl = this.ServicePredecessorBaseUrl;
      binder.ServiceTreeUrl = this.ServiceTreeUrl;
      binder.ParentDataKeyName = this.ParentDataKeyName;
      binder.AllowMultipleSelection = this.AllowMultipleSelection;
      binder.EnableDragAndDrop = this.EnableDragAndDrop;
      binder.EnableInitialExpanding = this.EnableInitialExpanding;
      binder.ExpandedNodesCookieName = this.ExpandedNodesCookieName;
      binder.HierarchicalTreeRootBindModeEnabled = this.HierarchicalTreeRootBindModeEnabled;
      this.TreeTable.MultipleSelect = this.AllowMultipleSelection;
      this.TreeTable.CssClass = this.TreeViewCssClass;
    }

    /// <summary>Call this to construct the items list</summary>
    protected override void ConstructList()
    {
      this.ConstructToolbox();
      this.ConstructHeader();
      this.ConstructBinder();
      foreach (IDialogDefinition dialog in this.Dialogs)
        this.ConstructDialog(dialog);
    }

    private void ConstructHeader()
    {
      List<TreeTableItemHeader> treeTableItemHeaderList = new List<TreeTableItemHeader>();
      for (int index = this.Items.Count - 1; index >= 0; --index)
      {
        string cssClass1 = "sfHeaderColumn";
        if (!string.IsNullOrEmpty(this.Items[index].HeaderCssClass))
          cssClass1 = cssClass1 + " " + this.Items[index].HeaderCssClass;
        if (index == 0)
        {
          string cssClass2 = cssClass1 + " sfFirstHeaderColumn";
          treeTableItemHeaderList.Add(new TreeTableItemHeader(this.Items[index].HeaderText, cssClass2, this.Items[index].Width, this.Items[index].DisableSorting));
        }
        else
          treeTableItemHeaderList.Add(new TreeTableItemHeader(this.Items[index].HeaderText, cssClass1, this.Items[index].Width, this.Items[index].DisableSorting));
      }
      this.TableHeader.DataSource = (object) treeTableItemHeaderList;
      this.TableHeader.DataBind();
    }

    private void ConstructBinder()
    {
      string str1 = string.Empty;
      if (this.EnableDragAndDrop)
        str1 += "<div class='sfDragAndDropTreeTableColumn'></div>";
      for (int index = this.Items.Count - 1; index >= 0; --index)
      {
        string str2 = "sfTreeTableColumn";
        if (index == 0)
          str2 += " sfFirstTreeTableColumn";
        if (!string.IsNullOrEmpty(this.Items[index].ItemCssClass))
          str2 = str2 + " " + this.Items[index].ItemCssClass;
        str1 = (this.Items[index].Width <= 0 ? str1 + string.Format("<div class=\"{0}\">", (object) str2) : str1 + string.Format("<div class=\"{0}\" style=\"width:{1}px;\">", (object) str2, (object) this.Items[index].Width)) + this.Items[index].Markup + "</div>";
      }
      this.Binder.Containers.Add(new BinderContainer()
      {
        RenderContainer = false,
        Markup = str1
      });
    }

    private void TableHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      TreeTableItemHeader dataItem = (TreeTableItemHeader) e.Item.DataItem;
      HtmlGenericControl control1 = (HtmlGenericControl) e.Item.FindControl("headerColumn");
      control1.Attributes.Add("class", dataItem.CssClass);
      if (dataItem.Width > 0)
        control1.Style.Add("width", dataItem.Width.ToString() + "px");
      ((Literal) e.Item.FindControl("headerLiteral")).Text = dataItem.Title;
      HtmlGenericControl control2 = (HtmlGenericControl) e.Item.FindControl("headerColumnSort");
      control2.Attributes.Add("class", dataItem.CssClass);
      if (dataItem.Width > 0)
        control2.Style.Add("width", dataItem.Width.ToString() + "px");
      ((LinkButton) e.Item.FindControl("headerLink")).Text = dataItem.Title;
      control1.Visible = dataItem.DisableSorting;
      control2.Visible = !dataItem.DisableSorting;
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ScriptControlDescriptor controlDescriptor = source.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddComponentProperty("treeTable", this.TreeTable.ClientID);
      if (this.EmptyMessage == null)
        return (IEnumerable<ScriptDescriptor>) source;
      controlDescriptor.AddElementProperty("emptyMessage", this.EmptyMessage.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.ItemLists.Scripts.ItemsTreeTable.js", this.GetType().Assembly.GetName().FullName)
    };
  }
}
