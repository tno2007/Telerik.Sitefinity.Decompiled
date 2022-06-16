// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageTemplateExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Renderer.Editor.Specifics;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Extension methods which extend Sitefinity type <see cref="T:Telerik.Sitefinity.Pages.Model.PageTemplate" /> with helper methods.
  /// </summary>
  public static class PageTemplateExtensions
  {
    /// <summary>Get Pages for the specified template.</summary>
    /// <param name="template">The template.</param>
    /// <returns></returns>
    public static IQueryable<PageData> Pages(this PageTemplate template)
    {
      if (!(((IDataItem) template).Provider is PageDataProvider provider))
        return new List<PageData>().AsQueryable<PageData>();
      return provider.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (i => i.Template.Id == template.Id));
    }

    /// <summary>Sets the parent template.</summary>
    /// <param name="template">The template.</param>
    /// <param name="parentTemplate">The parent template.</param>
    public static void SetParentTemplate(this PageTemplate template, PageTemplate parentTemplate)
    {
      if (template == null)
        throw new ArgumentNullException("template is null.");
      if (parentTemplate != null)
      {
        if (new TemplateInitializer(PageManager.GetManager()).IsDefaultTemplate(template.Id))
          throw new ArgumentException(Res.Get<ErrorMessages>("BasicTemplatesCanNotBeBasedOnOtherTemplates"));
        if (parentTemplate.IsDescendantOf(template))
          throw new InvalidOperationException(Res.Get<ErrorMessages>().TemplateCannotBeBasedOnItself);
      }
      template.ParentTemplate = parentTemplate;
    }

    public static void SetParentBaseTemplate(this TemplateDraft tempDraft, PageTemplate newTemplate)
    {
      if (newTemplate != null)
      {
        tempDraft.ParentTemplate.SetParentTemplate(newTemplate);
        tempDraft.TemplateId = newTemplate.Id;
      }
      else
      {
        tempDraft.ParentTemplate.SetParentTemplate((PageTemplate) null);
        tempDraft.TemplateId = Guid.Empty;
      }
    }

    /// <summary>
    /// Determines whether the template is a descendant of the specified template.
    /// </summary>
    /// <param name="descendant">The potential descendant template.</param>
    /// <param name="ancestor">The potential ancestor.</param>
    internal static bool IsDescendantOf(this PageTemplate descendant, PageTemplate ancestor)
    {
      if (ancestor == null)
        return false;
      if (descendant.Id == ancestor.Id)
        return true;
      return descendant.ParentTemplate != null && descendant.ParentTemplate.IsDescendantOf(ancestor);
    }

    internal static bool IsExternallyRendered(this PageTemplate template) => template.GetLastContainer<IRendererCommonData>().IsExternallyRendered();

    internal static int GetBasesPagesCount(this PageTemplate template, Guid siteRoot = default (Guid)) => template.GetPageDataBasedOnTemplate(siteRoot).Count<PageData>() + template.GetDraftPagesBasedOnTemplate(siteRoot).Count<PageDraft>();

    internal static IQueryable<PageData> GetPageDataBasedOnTemplate(
      this PageTemplate template,
      Guid siteRoot = default (Guid))
    {
      IQueryable<PageData> source = template.Pages().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.NavigationNode.RootNode != default (object)));
      IQueryable<PageData> dataBasedOnTemplate;
      if (siteRoot != new Guid())
        dataBasedOnTemplate = source.Where<PageData>((Expression<Func<PageData, bool>>) (p => p.NavigationNode.RootNodeId == siteRoot));
      else
        dataBasedOnTemplate = source.Where<PageData>((Expression<Func<PageData, bool>>) (p => p.NavigationNode.RootNode.Id != NewslettersModule.standardCampaignRootNodeId));
      return dataBasedOnTemplate;
    }

    internal static IQueryable<PageDraft> GetDraftPagesBasedOnTemplate(
      this PageTemplate template,
      Guid siteRoot = default (Guid))
    {
      Guid templateId = template.Id;
      IQueryable<PageDraft> source = PageManager.GetManager().GetDrafts<PageDraft>().Where<PageDraft>((Expression<Func<PageDraft, bool>>) (p => !p.IsTempDraft && p.TemplateId == templateId && (p.ParentPage.Template.Id != p.TemplateId || p.ParentPage.Template == default (object)) && p.ParentPage.NavigationNode.RootNode != default (object)));
      IQueryable<PageDraft> pagesBasedOnTemplate;
      if (siteRoot != new Guid())
        pagesBasedOnTemplate = source.Where<PageDraft>((Expression<Func<PageDraft, bool>>) (p => p.ParentPage.NavigationNode.RootNodeId == siteRoot));
      else
        pagesBasedOnTemplate = source.Where<PageDraft>((Expression<Func<PageDraft, bool>>) (p => p.ParentPage.NavigationNode.RootNode.Id != NewslettersModule.standardCampaignRootNodeId));
      return pagesBasedOnTemplate;
    }
  }
}
