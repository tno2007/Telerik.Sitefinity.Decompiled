// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Web;
using Telerik.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>Sitefinity Template Explorer control.</summary>
  [ClientScriptResource("Telerik.Sitefinity.Modules.Pages.Web.UI.TemplateExplorer", "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.TemplateExplorer.js")]
  public class TemplateExplorer : BaseExplorer
  {
    /// <summary>Creates the child controls.</summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      SplitterItem splitterItem = this.Splitter.Items[2];
      HtmlGenericControl child1 = new HtmlGenericControl("h2");
      child1.Attributes.Add("class", "sfSidebarTitle");
      child1.InnerHtml = Res.Get<Labels>().ManageAlso;
      splitterItem.Controls.Add((Control) child1);
      HtmlGenericControl child2 = new HtmlGenericControl("a");
      child2.Attributes.Add("href", this.ViewUrl.UrlEncode());
      child2.Attributes.Add("class", "sfGoToPage");
      child2.InnerHtml = Res.Get<PageResources>().Pages;
      splitterItem.Controls.Add((Control) child2);
      this.ToolBar.Items.Add((RadToolBarItem) this.CreateToolBarButton("CreateTemplate", Res.Get<PageResources>().CreateNewTemplate, string.Empty, "icnCreateTemplate", string.Empty, string.Empty));
      this.ToolBar.Items.Add((RadToolBarItem) this.CreateToolBarButton("Delete", Res.Get<PageResources>().DeleteText, string.Empty, "icnDelete", string.Empty, string.Empty));
    }

    /// <summary>Describes the component.</summary>
    /// <param name="descriptor">The descriptor.</param>
    protected override void DescribeComponent(IScriptDescriptor descriptor)
    {
      base.DescribeComponent(descriptor);
      string str1 = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/TemplatePropertiesDialog", UrlResolveOptions.Rooted);
      int rootTaxon1 = (int) this.RootTaxon;
      string str2 = str1 + "?rootTaxon=" + (object) this.RootTaxon;
      descriptor.AddProperty("dialogUrl", (object) str2);
      string str3 = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/TemplatePagesDialog", UrlResolveOptions.Rooted);
      int rootTaxon2 = (int) this.RootTaxon;
      string str4 = str3 + "?rootTaxon=" + (object) this.RootTaxon;
      descriptor.AddProperty("templatePagesDialogUrl", (object) str4);
      string str5 = RouteHelper.ResolveUrl("~/Sitefinity/Dialog/ModulePermissionsDialog", UrlResolveOptions.Rooted);
      descriptor.AddProperty("permissionsDialogUrl", (object) str5);
      descriptor.AddProperty("pageManagerType", (object) typeof (PageManager).AssemblyQualifiedName);
      descriptor.AddProperty("pageDataSecuredObjectType", (object) typeof (PageTemplate).AssemblyQualifiedName);
      descriptor.AddProperty("_templateExplorerLocalization", (object) new JavaScriptSerializer().Serialize((object) new Dictionary<string, string>()
      {
        {
          "NotBasedOnOtherTemplate",
          Res.Get<PageResources>().NotBasedOnOtherTemplate
        },
        {
          "ClickToViewWhichPagesUseThisTemplate",
          Res.Get<PageResources>().ClickToViewWhichPagesUseThisTemplate
        },
        {
          "Edit",
          Res.Get<PageResources>().Edit
        },
        {
          "SelectAnother",
          Res.Get<PageResources>().SelectAnother
        },
        {
          "Actions",
          Res.Get<Labels>().Actions
        }
      }));
    }

    /// <summary>
    /// Handles the ExplorerPopulated event of the TemplateExplorer control.
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
      if (!(lower == "parenttemplate"))
      {
        if (!(lower == "category"))
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
          attributeValueComparer.AttrKey = "Category";
          list.Sort((IComparer<FileBrowserItem>) attributeValueComparer);
          flag = true;
        }
      }
      else
      {
        attributeValueComparer.AttrKey = "ParentTemplate";
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
      GridColumn byUniqueNameSafe = this.Grid.Columns.FindByUniqueNameSafe("Size");
      if (byUniqueNameSafe != null)
        this.Grid.MasterTableView.RenderColumns[byUniqueNameSafe.OrderIndex].HeaderStyle.Width = Unit.Pixel(60);
      this.AddGridColumn("GridTemplateColumn", Res.Get<PageResources>().BasedOnGridColumnHeaderText, "ParentTemplate", "ParentTemplate", true, new int?(200), -1);
      this.AddGridColumn("GridTemplateColumn", string.Empty, "PageCount", string.Empty, false, new int?(70), -1);
      this.AddGridColumn("GridTemplateColumn", Res.Get<PageResources>().AuthorGridColumnHeaderText, "Author", "Author", true, new int?(120), -1);
      this.AddGridColumn("GridTemplateColumn", Res.Get<PageResources>().DateGridColumnHeaderText, "Date", "Date", true, new int?(120), -1);
      base.SetGridControl();
    }
  }
}
