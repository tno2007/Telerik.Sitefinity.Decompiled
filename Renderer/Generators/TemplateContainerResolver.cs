// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Generators.TemplateContainerResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Versioning;

namespace Telerik.Sitefinity.Renderer.Generators
{
  internal class TemplateContainerResolver : IContainerResolver
  {
    private PageTemplate pageTemplate;
    private PageManager pageManager;

    public PageTemplate PageTemplate => this.pageTemplate;

    public TemplateContainerResolver(PageTemplate pageTemplate, PageManager manager)
    {
      this.pageTemplate = pageTemplate;
      this.pageManager = manager;
    }

    public IControlsContainer GetContainer(bool editVersion, string version)
    {
      IControlsContainer targetObject;
      if (editVersion)
      {
        using (new ElevatedModeRegion((IManager) this.pageManager))
        {
          targetObject = (IControlsContainer) this.pageTemplate.Drafts.FirstOrDefault<TemplateDraft>((Func<TemplateDraft, bool>) (x => x.IsTempDraft));
          if (targetObject == null)
          {
            targetObject = (IControlsContainer) this.pageManager.EditTemplate(this.pageTemplate.Id);
            this.pageTemplate.LockedBy = Guid.Empty;
            this.pageManager.SaveChanges();
          }
          if (!string.IsNullOrEmpty(version))
            VersionManager.GetManager().GetSpecificVersionByChangeId((object) targetObject, Guid.Parse(version));
        }
      }
      else
        targetObject = (IControlsContainer) this.pageTemplate;
      return targetObject;
    }

    public IControlManager GetManager() => (IControlManager) this.pageManager;
  }
}
