// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.DisplayOverridenWidgets
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public class DisplayOverridenWidgets : TemplatePagesDialog
  {
    private string controlTitle;

    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      string input = this.Page.Request.QueryString["baseControlId"];
      Guid controlIdGuid = Guid.Empty;
      ref Guid local = ref controlIdGuid;
      if (Guid.TryParse(input, out local))
      {
        TemplateDraftControl templateDraftControl = PageManager.GetManager().GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (c => c.Id == controlIdGuid)).FirstOrDefault<TemplateDraftControl>();
        if (templateDraftControl.PersonalizationMasterId != Guid.Empty)
          templateDraftControl = PageManager.GetManager().GetControl<TemplateDraftControl>(templateDraftControl.PersonalizationMasterId);
        List<PageNode> pageNodeList = new List<PageNode>();
        List<KeyValuePair<PageData, bool>> widgetChangedPages = PageTemplateViewModel.GetOverridenWidgetChangedPages((ControlData) templateDraftControl);
        List<NavigationNode> navigationNodeList = new List<NavigationNode>();
        foreach (KeyValuePair<PageData, bool> keyValuePair in widgetChangedPages)
        {
          NavigationNode navNode = PageTemplateViewModel.GetNavNode(keyValuePair.Key, keyValuePair.Value, true);
          navigationNodeList.Add(navNode);
        }
        this.navigationNodes = (IEnumerable<NavigationNode>) navigationNodeList;
        this.controlTitle = templateDraftControl.Caption;
      }
      base.InitializeControls(dialogContainer);
    }

    protected override void OnPreRender(EventArgs e)
    {
      this.Container.GetControl<ITextControl>("ltrlTemplatePageMsg", false).Text = "";
      base.OnPreRender(e);
      int num = this.navigationNodes.Count<NavigationNode>();
      if (this.PageCountLit != null)
      {
        if (num == 1)
          this.PageCountLit.Text = string.Format(Res.Get<PageResources>().ZoneEditorControlOverridenInPage, (object) this.controlTitle, (object) num.ToString());
        else
          this.PageCountLit.Text = string.Format(Res.Get<PageResources>().ZoneEditorControlOverridenInPages, (object) this.controlTitle, (object) num.ToString());
      }
      this.PagesRepeater.ItemCreated += new RepeaterItemEventHandler(((TemplatePagesDialog) this).PagesRepeater_ItemCreated);
      if (this.selectedSiteId != Guid.Empty)
      {
        Guid selectedSiteRootNodeId = SystemManager.MultisiteContext.GetSiteById(this.selectedSiteId).SiteMapRootNodeId;
        this.PagesRepeater.DataSource = (object) this.navigationNodes.Where<NavigationNode>((Func<NavigationNode, bool>) (n => n.RootNodeId == selectedSiteRootNodeId || n.RootNodeId == SiteInitializer.BackendRootNodeId));
      }
      else
        this.PagesRepeater.DataSource = (object) this.navigationNodes;
      this.PagesRepeater.DataBind();
    }
  }
}
