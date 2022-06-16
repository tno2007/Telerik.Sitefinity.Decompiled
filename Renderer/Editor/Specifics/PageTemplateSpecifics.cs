// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.Specifics.PageTemplateSpecifics
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Renderer.Editor.Specifics
{
  internal class PageTemplateSpecifics : IContainerSpecifics
  {
    public object GetItemFromContext(IEditorContext context, IControlManager manager) => (object) ((manager as PageManager).GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (x => x.Id == Guid.Parse(context.Key))) ?? throw new ItemNotFoundException());

    public IControlManager GetManager() => (IControlManager) PageManager.GetManager();

    public void Unlock(object item, IControlManager manager) => (manager as PageManager).UnlockTemplate((item as PageTemplate).Id, false, SystemManager.CurrentContext.Culture);

    public bool CanEdit(object template, bool ensureAlreadyLocked) => ((IEnumerable<ICanEditStrategy>) new ICanEditStrategy[2]
    {
      (ICanEditStrategy) new PageTemplateSpecifics.PageTemplateLockedStrategy(ensureAlreadyLocked),
      (ICanEditStrategy) new PageTemplateSpecifics.PermissionsLockedStrategy()
    }).All<ICanEditStrategy>((Func<ICanEditStrategy, bool>) (x => x.CanEdit(template)));

    public bool SupportsSaveToAll(object item, IControlManager manager) => true;

    public IControlsContainer CheckOut(object item, IControlManager manager)
    {
      PageTemplate pageTemplate = item as PageTemplate;
      return (IControlsContainer) (manager as PageManager).EditTemplate(pageTemplate.Id);
    }

    public ControlData CreateControl(object item, IControlManager manager) => (ControlData) manager.CreateControl<TemplateDraftControl>();

    public void AddControl(IControlsContainer draft, ControlData control) => (draft as TemplateDraft).Controls.Add(control as TemplateDraftControl);

    public int GetVersion(IControlsContainer container, IControlManager manager)
    {
      PageManager pageManager = manager as PageManager;
      PageTemplate liveItem = container as PageTemplate;
      int num1 = 0;
      int num2 = 0;
      if (liveItem != null)
      {
        TemplateDraft master = pageManager.TemplatesLifecycle.GetMaster(liveItem);
        if (master != null)
          num1 = master.Version;
        num2 = liveItem.Version;
      }
      if (container is TemplateDraft draftItem)
      {
        TemplateDraft master = pageManager.TemplatesLifecycle.GetMaster(draftItem);
        if (master != null)
          num1 = master.Version;
        num2 = draftItem.ParentTemplate.Version;
      }
      return num1 * 10 + num2;
    }

    private class PageTemplateLockedStrategy : ICanEditStrategy
    {
      private bool ensureAlreadyLocked;

      public PageTemplateLockedStrategy(bool ensureAlreadyLocked) => this.ensureAlreadyLocked = ensureAlreadyLocked;

      public bool CanEdit(object item)
      {
        PageTemplate pageTemplate = item as PageTemplate;
        Guid currentUserId = ClaimsManager.GetCurrentUserId();
        bool flag = pageTemplate.LockedBy == currentUserId;
        if (!this.ensureAlreadyLocked)
          flag = flag || pageTemplate.LockedBy == Guid.Empty;
        return flag;
      }
    }

    private class PermissionsLockedStrategy : ICanEditStrategy
    {
      public bool CanEdit(object item) => (item as PageTemplate).IsGranted("PageTemplates", "Modify");
    }
  }
}
