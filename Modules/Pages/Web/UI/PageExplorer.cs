// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Sitefinity Page Explorer control.</summary>
  [ClientScriptResource("Telerik.Sitefinity.Modules.Pages.Web.UI.PageExplorer", "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PageExplorer.js")]
  public class PageExplorer : BaseExplorer
  {
    /// <summary>Controls the pre render.</summary>
    protected override void ControlPreRender()
    {
      base.ControlPreRender();
      if (this.GridContextMenu == null)
        return;
      this.GridContextMenu.Visible = false;
    }

    /// <summary>Creates the child controls.</summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      RadToolBarDropDown radToolBarDropDown1 = new RadToolBarDropDown(Res.Get<PageResources>().CreateNewText);
      radToolBarDropDown1.CssClass += "icnCreateNew";
      radToolBarDropDown1.DropDownWidth = (Unit) 360;
      radToolBarDropDown1.Buttons.Add(this.CreateToolBarButton("CreatePage", Res.Get<PageResources>().PageCommandName, string.Empty, "icnCreatePage", "icnDescription", Res.Get<PageResources>().PageDescription));
      radToolBarDropDown1.Buttons.Add(this.CreateToolBarButton("CreateSection", Res.Get<PageResources>().SectionCommandName, string.Empty, "icnCreatePageGroup", "icnDescription", Res.Get<PageResources>().SectionDescription));
      radToolBarDropDown1.Buttons.Add(this.CreateToolBarButton("CreateBlog", Res.Get<PageResources>().BlogCommandName, string.Empty, "icnCreateBlog", "icnDescription", Res.Get<PageResources>().BlogDescription));
      radToolBarDropDown1.Buttons.Add(this.CreateToolBarButton("CreatePhotoGallery", Res.Get<PageResources>().PhotoGalleryCommandName, string.Empty, "icnPhotoGallery", "icnDescription", Res.Get<PageResources>().PhotoGalleryDescription));
      radToolBarDropDown1.Buttons.Add(this.CreateToolBarButton("CreateTemplate", Res.Get<PageResources>().TemplateCommandName, string.Empty, "icnTemplate", "icnDescription", Res.Get<PageResources>().TemplateDescription));
      this.ToolBar.Items.Add((RadToolBarItem) radToolBarDropDown1);
      this.ToolBar.Items.Add((RadToolBarItem) this.CreateToolBarButton("Delete", Res.Get<PageResources>().DeleteText, string.Empty, "icnDelete", string.Empty, string.Empty));
      RadToolBarDropDown radToolBarDropDown2 = new RadToolBarDropDown(Res.Get<PageResources>().MoreActionsText);
      radToolBarDropDown2.CssClass += "icnMoreActions";
      radToolBarDropDown2.DropDownWidth = (Unit) 200;
      radToolBarDropDown2.Buttons.Add(this.CreateToolBarButton("Publish", Res.Get<PageResources>().PublishCommandName, string.Empty, "icnPublish", string.Empty, string.Empty));
      radToolBarDropDown2.Buttons.Add(this.CreateToolBarButton("ReorderPages", Res.Get<PageResources>().ReorderPagesCommandName, string.Empty, "icnReorderPages", string.Empty, string.Empty));
      radToolBarDropDown2.Buttons.Add(this.CreateToolBarButton("PageProperties", Res.Get<PageResources>().PagePropertiesCommandName, string.Empty, "icnPageProperties", string.Empty, string.Empty));
      radToolBarDropDown2.Buttons.Add(this.CreateToolBarButton("Owner", Res.Get<PageResources>().OwnerCommandName, string.Empty, "icnOwner", string.Empty, string.Empty));
      radToolBarDropDown2.Buttons.Add(this.CreateToolBarButton("Template", Res.Get<PageResources>().TemplateCommandName, string.Empty, "icnTemplate", string.Empty, string.Empty));
      radToolBarDropDown2.Buttons.Add(this.CreateToolBarButton("Parent", Res.Get<PageResources>().ParentCommandName, string.Empty, "icnParent", string.Empty, string.Empty));
      this.ToolBar.Items.Add((RadToolBarItem) this.CreateToolBarButton("SearchPages", Res.Get<PageResources>().SearchPagesText, string.Empty, "icnSearchPages", string.Empty, string.Empty));
      this.ToolBar.Items.Add((RadToolBarItem) this.CreateToolBarButton("EditTemplates", Res.Get<PageResources>().EditTemplates, this.ViewUrl, "icnEditTemplates", string.Empty, string.Empty));
      this.ToolBar.Items.Add((RadToolBarItem) this.CreateToolBarButton("PagePermissions", Res.Get<SecurityResources>().PermissionsForFrontendPages, string.Empty, "", string.Empty, string.Empty));
      this.TreeView.ContextMenus[0].Items.Clear();
      this.TreeView.ContextMenus[0].Items.Add(this.CreateContextMenuItem("Delete", Res.Get<PageResources>().DeleteCommandName, false));
      this.TreeView.ContextMenus[0].Items.Add(this.CreateContextMenuItem("SectionProperties", Res.Get<PageResources>().PropertiesCommandName, false));
      this.TreeView.ContextMenus[0].Items.Add(this.CreateContextMenuItem("Permissions", Res.Get<PageResources>().PermissionsCommandName, false));
      this.TreeView.ContextMenus[0].Items.Add(this.CreateContextMenuItem("Owner", Res.Get<PageResources>().OwnerCommandName, false));
      this.TreeView.ContextMenus[0].Items.Add(this.CreateContextMenuItem("Parent", Res.Get<PageResources>().ParentCommandName, false));
    }

    /// <summary>Describes the component.</summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void DescribeComponent(IScriptDescriptor descriptor)
    {
      base.DescribeComponent(descriptor);
      descriptor.AddProperty("initialPath", (object) this.InitialPath);
      string str1 = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/PagePropertiesDialog", UrlResolveOptions.Rooted);
      int rootTaxon1 = (int) this.RootTaxon;
      string str2 = str1 + "?rootTaxon=" + (object) this.RootTaxon;
      descriptor.AddProperty("dialogUrl", (object) str2);
      string str3 = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/TemplatePropertiesDialog", UrlResolveOptions.Rooted);
      int rootTaxon2 = (int) this.RootTaxon;
      string str4 = str3 + "?rootTaxon=" + (object) this.RootTaxon;
      descriptor.AddProperty("templateDialogUrl", (object) str4);
      string str5 = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/SectionPropertiesDialog", UrlResolveOptions.Rooted);
      descriptor.AddProperty("sectionDialogUrl", (object) str5);
      string str6 = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ModulePermissionsDialog", UrlResolveOptions.Rooted);
      descriptor.AddProperty("permissionsDialogUrl", (object) str6);
      descriptor.AddProperty("pageManagerType", (object) typeof (PageManager).AssemblyQualifiedName);
      descriptor.AddProperty("pageDataSecuredObjectType", (object) typeof (PageData).AssemblyQualifiedName);
      descriptor.AddProperty("frontendPagesRootId", (object) SiteInitializer.CurrentFrontendRootNodeId.ToString());
      descriptor.AddProperty("permissionsTitleForFrontend", (object) Res.Get<SecurityResources>().PermissionsForFrontendPages);
      descriptor.AddProperty("sectionManagerType", (object) typeof (TaxonomyManager).AssemblyQualifiedName);
      descriptor.AddProperty("sectionSecuredObjectType", (object) typeof (HierarchicalTaxon).AssemblyQualifiedName);
      descriptor.AddProperty("pagesServiceUrl", (object) RouteHelper.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/", UrlResolveOptions.Rooted));
    }

    /// <summary>Gets the script references.</summary>
    /// <returns></returns>
    protected override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (PageExplorer).Assembly.FullName)
    };

    /// <summary>
    /// Handles the ExplorerPopulated event of the PageExplorer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadFileExplorerPopulatedEventArgs" /> instance containing the event data.</param>
    protected override void Explorer_ExplorerPopulated(
      object sender,
      RadFileExplorerPopulatedEventArgs e)
    {
      bool flag = false;
      string lower = e.SortColumnName.ToLower(CultureInfo.InvariantCulture);
      List<FileBrowserItem> list = e.List;
      BaseExplorer.AttributeValueComparer attributeValueComparer = new BaseExplorer.AttributeValueComparer();
      if (!(lower == "templatetitle"))
      {
        if (!(lower == "author"))
        {
          if (lower == "date")
          {
            list.Sort((IComparer<FileBrowserItem>) new BaseExplorer.DateComparer());
            flag = true;
          }
        }
        else
        {
          attributeValueComparer.AttrKey = "Author";
          list.Sort((IComparer<FileBrowserItem>) attributeValueComparer);
          flag = true;
        }
      }
      else
      {
        attributeValueComparer.AttrKey = "TemplateTitle";
        list.Sort((IComparer<FileBrowserItem>) attributeValueComparer);
        flag = true;
      }
      if (!flag || e.SortDirection.IndexOf("DESC", StringComparison.InvariantCultureIgnoreCase) == -1)
        return;
      list.Reverse();
    }

    /// <summary>Sets the grid control.</summary>
    protected override void SetGridControl()
    {
      this.AddGridColumn("GridClientSelectColumn", string.Empty, "ClientSelectColumn", string.Empty, false, new int?(25), 0);
      this.AddGridColumn("GridTemplateColumn", Res.Get<PageResources>().TemplateGridColumnHeaderText, "TemplateTitle", "TemplateTitle", true, new int?(250), -1);
      this.AddGridColumn("GridTemplateColumn", Res.Get<PageResources>().AuthorGridColumnHeaderText, "Author", "Author", true, new int?(120), -1);
      this.AddGridColumn("GridTemplateColumn", Res.Get<PageResources>().DateGridColumnHeaderText, "Date", "Date", true, new int?(120), -1);
      this.AddGridColumn("GridTemplateColumn", string.Empty, "View", string.Empty, false, new int?(60), -1);
      base.SetGridControl();
    }

    /// <summary>Creates the Message control.</summary>
    /// <returns></returns>
    protected override Message CreateMessageControl()
    {
      Message messageControl = base.CreateMessageControl();
      messageControl.StartPositiveColor = ColorTranslator.FromHtml("#d3eabb");
      messageControl.EndPositiveColor = ColorTranslator.FromHtml("#d3eabb");
      return messageControl;
    }
  }
}
