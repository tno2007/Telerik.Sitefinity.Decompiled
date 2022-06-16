// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.MenuSection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// Control for populating a section of a menu, with <ul><li></li></ul> rendered elements
  /// </summary>
  public class MenuSection : SimpleView
  {
    public MenuSection() => this.LayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.MenuSection.ascx");

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The container of the control</param>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      string key;
      switch (this.Modules)
      {
        case MenuSectionModules.ApplicationModules:
          key = SiteInitializer.ModulesNodeId.ToString();
          break;
        case MenuSectionModules.ServiceModules:
          key = SiteInitializer.ServicesNodeId.ToString();
          break;
        case MenuSectionModules.Tools:
          key = SiteInitializer.ToolsNodeId.ToString();
          break;
        default:
          throw new NotSupportedException();
      }
      this.ListItemsRepeater.DataSource = (object) BackendSiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(key).ChildNodes;
      this.ListItemsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ListItemsRepeater_ItemDataBound);
    }

    /// <summary>
    /// Handle the ItemDataBound event on the repeater listing the menu items
    /// </summary>
    /// <param name="sender">Not used.</param>
    /// <param name="e">A <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> that contains the event data.</param>
    private void ListItemsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      HyperLink control = e.Item.FindControl("lnkItem") as HyperLink;
      SiteMapNode dataItem = (SiteMapNode) e.Item.DataItem;
      control.NavigateUrl = dataItem.Url;
      control.Text = dataItem.Title;
      SiteMapNode currentNode = BackendSiteMap.GetCurrentProvider().CurrentNode;
      if (currentNode == null || !(currentNode.Url == control.NavigateUrl))
        return;
      control.CssClass = "sfSel";
    }

    /// <summary>Event handler for the PreRender lifecycle event</summary>
    /// <param name="e">Not used.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.ListItemsRepeater.DataBind();
    }

    /// <summary>
    /// Retrieves the embedded path of the template used for this control
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Set of retrieve the modules which are to be loaded into the menu section
    /// </summary>
    public MenuSectionModules Modules { get; set; }

    /// <summary>A repeater listing the menu items</summary>
    protected virtual Repeater ListItemsRepeater => this.Container.GetControl<Repeater>("rptListItems", true);
  }
}
