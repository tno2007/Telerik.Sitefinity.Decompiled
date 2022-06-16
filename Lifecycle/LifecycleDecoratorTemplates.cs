// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleDecoratorTemplates
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Lifecycle
{
  public class LifecycleDecoratorTemplates : LifecycleDecorator<PageTemplate, TemplateDraft>
  {
    public LifecycleDecoratorTemplates(PageManager manager)
      : base((ILifecycleManager<PageTemplate, TemplateDraft>) manager)
    {
    }

    public PageManager PageManager => (PageManager) this.Manager;

    /// <summary>
    /// Copies data from the specified draft item to the specified live item.
    /// </summary>
    /// <param name="draftItem">The draft item to get data from.</param>
    /// <param name="liveItem">The live item to wrtie data to.</param>
    /// <param name="culture">The culture of the operation.</param>
    public override void Copy(TemplateDraft draftItem, PageTemplate liveItem, CultureInfo culture)
    {
      this.PageManager.CopyTemplateData<TemplateDraftControl, TemplateDraftPresentation, TemplateControl, TemplatePresentation>((ITemplateData<TemplateDraftControl, TemplateDraftPresentation>) draftItem, (ITemplateData<TemplateControl, TemplatePresentation>) liveItem, CopyDirection.CopyToOriginal, culture, culture);
      if (draftItem.TemplateId != Guid.Empty)
      {
        bool suppressSecurityChecks = this.PageManager.Provider.SuppressSecurityChecks;
        this.PageManager.Provider.SuppressSecurityChecks = true;
        PageTemplate template = this.PageManager.GetTemplate(draftItem.TemplateId);
        liveItem.ParentTemplate = template;
        this.PageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      }
      else
        liveItem.ParentTemplate = (PageTemplate) null;
      liveItem.LastModifiedBy = draftItem.LastModifiedBy;
    }

    /// <summary>
    /// Copies data from the specified source draft item to the specified target draft item.
    /// </summary>
    /// <param name="sourceDraft">The draft item to get data from.</param>
    /// <param name="targetDraft">The draft item to wrtie data to.</param>
    /// <param name="sourceCulture"></param>
    public override void Copy(
      TemplateDraft sourceDraft,
      TemplateDraft targetDraft,
      CultureInfo sourceCulture)
    {
      CopyDirection copyDirection = this.PageManager.ResolveDraftsCopyDirection((DraftData) sourceDraft, (DraftData) targetDraft);
      this.PageManager.CopyTemplateData<TemplateDraftControl, TemplateDraftPresentation, TemplateDraftControl, TemplateDraftPresentation>((ITemplateData<TemplateDraftControl, TemplateDraftPresentation>) sourceDraft, (ITemplateData<TemplateDraftControl, TemplateDraftPresentation>) targetDraft, copyDirection);
      targetDraft.TemplateId = sourceDraft.TemplateId;
      targetDraft.Version = sourceDraft.Version;
      targetDraft.LastModifiedBy = sourceDraft.LastModifiedBy;
    }

    /// <summary>
    /// Copies data from the specified live item to the specified draft item.
    /// </summary>
    /// <param name="liveItem">The live item to wrtie data to.</param>
    /// <param name="draftItem">The draft item to get data from.</param>
    /// <param name="culture">The culture of the operation.</param>
    public override void Copy(PageTemplate liveItem, TemplateDraft draftItem, CultureInfo culture)
    {
      this.PageManager.CopyTemplateData<TemplateControl, TemplatePresentation, TemplateDraftControl, TemplateDraftPresentation>((ITemplateData<TemplateControl, TemplatePresentation>) liveItem, (ITemplateData<TemplateDraftControl, TemplateDraftPresentation>) draftItem, CopyDirection.OriginalToCopy);
      if (liveItem.ParentTemplate != null)
        draftItem.TemplateId = liveItem.ParentTemplate.Id;
      draftItem.Version = liveItem.Version;
      draftItem.LastModifiedBy = liveItem.LastModifiedBy;
    }

    public override TemplateDraft CheckIn(
      TemplateDraft temp,
      CultureInfo culture = null,
      bool deleteTemp = true)
    {
      TemplateDraft templateDraft = base.CheckIn(temp, culture, deleteTemp);
      templateDraft.LastModifiedBy = SecurityManager.GetCurrentUserId();
      return templateDraft;
    }

    public override PageTemplate Publish(TemplateDraft master, CultureInfo culture = null)
    {
      PageTemplate pageTemplate = base.Publish(master, culture);
      master.LastModifiedBy = SecurityManager.GetCurrentUserId();
      return pageTemplate;
    }

    public override TemplateDraft Unpublish(PageTemplate liveItem, CultureInfo culture = null)
    {
      TemplateDraft templateDraft = base.Unpublish(liveItem, culture);
      if (templateDraft != null)
        templateDraft.LastModifiedBy = SecurityManager.GetCurrentUserId();
      return templateDraft;
    }
  }
}
