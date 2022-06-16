// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.Specifics.PageSpecifics
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Renderer.Editor.Specifics
{
  internal class PageSpecifics : IContainerSpecifics
  {
    public object GetItemFromContext(IEditorContext context, IControlManager manager) => (object) ((PageSiteNode) SiteMapBase.GetCurrentProvider().FindSiteMapNodeFromKey(context.Key) ?? throw new ItemNotFoundException());

    public void Unlock(object item, IControlManager manager) => (manager as PageManager).UnlockPage((item as PageSiteNode).CurrentPageDataItem.Id, false, SystemManager.CurrentContext.Culture);

    public bool SupportsSaveToAll(object item, IControlManager manager) => (item as PageSiteNode).LocalizationStrategy == LocalizationStrategy.Synced;

    public bool CanEdit(object siteNode, bool ensureAlreadyLocked) => ((IEnumerable<ICanEditStrategy>) new ICanEditStrategy[3]
    {
      (ICanEditStrategy) new PageSpecifics.PermissionsLockedStrategy(),
      (ICanEditStrategy) new PageSpecifics.PageLockedStrategy(ensureAlreadyLocked),
      (ICanEditStrategy) new PageSpecifics.WorkflowLockedStrategy()
    }).All<ICanEditStrategy>((Func<ICanEditStrategy, bool>) (x => x.CanEdit(siteNode)));

    public IControlsContainer CheckOut(object item, IControlManager manager)
    {
      Guid id = (item as PageSiteNode).CurrentPageDataItem.Id;
      return (IControlsContainer) (manager as PageManager).EditPage(id);
    }

    public IControlManager GetManager() => (IControlManager) PageManager.GetManager();

    public ControlData CreateControl(object item, IControlManager manager)
    {
      if (!(item as PageSiteNode).IsGranted("CreateChildControls"))
        throw new SecurityDemandFailException("No permission to create child controls was granted");
      return (ControlData) manager.CreateControl<PageDraftControl>();
    }

    public void AddControl(IControlsContainer draft, ControlData control) => (draft as PageDraft).Controls.Add(control as PageDraftControl);

    public int GetVersion(IControlsContainer container, IControlManager manager)
    {
      PageManager pageManager = manager as PageManager;
      PageData liveItem = container as PageData;
      int num1 = 0;
      int num2 = 0;
      if (liveItem != null)
      {
        PageDraft master = pageManager.PagesLifecycle.GetMaster(liveItem);
        if (master != null)
          num1 = master.Version;
        num2 = liveItem.Version;
      }
      if (container is PageDraft draftItem)
      {
        PageDraft master = pageManager.PagesLifecycle.GetMaster(draftItem);
        if (master != null)
          num1 = master.Version;
        num2 = draftItem.ParentPage.Version;
      }
      return num1 * 10 + num2;
    }

    private class PageLockedStrategy : ICanEditStrategy
    {
      private bool ensureAlreadyLocked;

      public PageLockedStrategy(bool ensureAlreadyLocked) => this.ensureAlreadyLocked = ensureAlreadyLocked;

      public bool CanEdit(object item)
      {
        PageSiteNode pageSiteNode = item as PageSiteNode;
        Guid currentUserId = ClaimsManager.GetCurrentUserId();
        bool flag = pageSiteNode.CurrentPageDataItem.LockedBy == currentUserId;
        if (!this.ensureAlreadyLocked)
          flag = flag || pageSiteNode.CurrentPageDataItem.LockedBy == Guid.Empty;
        return flag;
      }
    }

    private class PermissionsLockedStrategy : ICanEditStrategy
    {
      public bool CanEdit(object item) => (item as PageSiteNode).IsGranted("EditContent");
    }

    private class WorkflowLockedStrategy : ICanEditStrategy
    {
      public bool CanEdit(object item)
      {
        PageSiteNode siteNode = item as PageSiteNode;
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        IWorkflowExecutionDefinition executionDefinition = WorkflowManager.GetWorkflowExecutionDefinition((IWorkflowItem) new WorkflowItemWrapper(siteNode), culture);
        string itemWorkflowStatus = (string) null;
        return !siteNode.CurrentPageDataItem.ApprovalWorlflowState.TryGetValue(out itemWorkflowStatus, culture) || executionDefinition.CanUserAdvance(itemWorkflowStatus);
      }
    }
  }
}
