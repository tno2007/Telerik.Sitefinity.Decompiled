// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.TemplateDraftProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Represents template draft runtime data.</summary>
  public class TemplateDraftProxy : DraftProxyBase
  {
    /// <summary>Gets the type of the proxy class.</summary>
    /// <value>The type of the proxy.</value>
    protected internal override DesignMediaType MediaType => DesignMediaType.Template;

    /// <summary>Gets the controls toolbox name.</summary>
    /// <value>The controls toolbox.</value>
    internal override string ControlsToolbox => "PageControls";

    /// <summary>Gets the layout toolbox name.</summary>
    /// <value>The layout toolbox.</value>
    internal override string LayoutToolbox => "PageLayouts";

    internal int PagesCount { get; private set; }

    internal int TemplatesCount { get; private set; }

    internal TemplateDraftProxy(
      TemplateDraft pageData,
      PageDataProvider provider,
      bool isPreview,
      CultureInfo objectCulture,
      bool optimized)
    {
      this.CurrentObjectCulture = objectCulture;
      this.PageDraftId = pageData.Id;
      this.PageProvider = provider;
      this.IsPreview = isPreview;
      this.ParentItemId = pageData.ParentId;
      this.PageTitle = (string) pageData.ParentTemplate.Title;
      List<IControlsContainer> controlsContainerList = new List<IControlsContainer>();
      bool includeScriptManager = false;
      if (this.Settings.Multilingual)
      {
        this.IsSplitByLanguage = false;
        List<CultureInfo> cultureInfoList = new List<CultureInfo>();
        CultureInfo[] availableCultures = pageData.ParentTemplate.AvailableCultures;
        cultureInfoList.AddRange((IEnumerable<CultureInfo>) availableCultures);
        this.UsedLanguages = cultureInfoList;
      }
      this.ShowLocalizationStrategySelector = false;
      PageHelper.ProcessPresentationData(((IPresentable) pageData).Presentation, (IList<Dictionary<string, ITemplate>>) this.Layouts, this.Templates);
      if (optimized)
        controlsContainerList.Add((IControlsContainer) new TemplateDraftControlsContainerWrapper((IControlsContainer) pageData, this.PageProvider));
      else
        controlsContainerList.Add((IControlsContainer) pageData);
      Guid guid = Guid.Empty;
      PageTemplate template = pageData.ParentTemplate.ParentTemplate;
      if (template != null)
        guid = template.Id;
      try
      {
        if (pageData.TemplateId != guid)
        {
          if (pageData.TemplateId != Guid.Empty)
          {
            bool suppressSecurityChecks = this.PageProvider.SuppressSecurityChecks;
            this.PageProvider.SuppressSecurityChecks = true;
            template = this.PageProvider.GetTemplate(pageData.TemplateId);
            this.PageProvider.SuppressSecurityChecks = suppressSecurityChecks;
          }
          else
            template = (PageTemplate) null;
        }
      }
      catch (ItemNotFoundException ex)
      {
        throw new TemplateNotFoundException(ex.Message);
      }
      this.MasterPage = pageData.MasterPage;
      if (string.IsNullOrEmpty(pageData.MasterPage))
      {
        string masterPage = PageHelper.ResolveDynamicMasterPage((IPageTemplate) pageData.ParentTemplate);
        if (string.IsNullOrEmpty(masterPage))
          PageHelper.ProcessTemplates((IPageTemplate) template, this.Layouts, this.Templates, controlsContainerList, optimized, out masterPage, out includeScriptManager);
        this.MasterPage = masterPage;
      }
      this.ControlContainersOrdered.AddRange((IEnumerable<IControlsContainer>) controlsContainerList);
      if (this.IsPreview)
        PageHelper.ProcessControls((IList<ControlBuilder>) this.Controls, (IList<IControlsContainer>) controlsContainerList);
      else
        PageHelper.ProcessControls((IList<ControlData>) this.DockedControls, (IList<ControlData>) this.PlaceHolders, (IList<IControlsContainer>) controlsContainerList);
      this.Layouts.Reverse();
      this.Themes = (IDictionary<CultureInfo, string>) PageHelper.GetTheme(pageData, pageData.ParentTemplate.ParentTemplate);
      this.ExternalPage = pageData.ExternalPage;
      this.IncludeScriptManager = pageData.IncludeScriptManager | includeScriptManager;
      this.LastControlId = pageData.LastControlId;
      this.PagesCount = pageData.ParentTemplate.GetBasesPagesCount();
      this.TemplatesCount = pageData.ParentTemplate.ChildTemplates.Count;
      this.Framework = pageData.ParentTemplate.Framework;
      this.IsBackend = pageData.IsBackend;
      int num;
      if (pageData.ParentTemplate != null)
        num = pageData.ParentTemplate.IsGranted("PageTemplates", "Unlock") ? 1 : 0;
      else
        num = 0;
      this.CanUnlock = num != 0;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.TemplateDraftProxy" /> class.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isPreview">if set to <c>true</c> zones won't be rendered.</param>
    /// <param name="objectCulture">The oobject culture.</param>
    public TemplateDraftProxy(
      TemplateDraft pageData,
      PageDataProvider provider,
      bool isPreview,
      CultureInfo objectCulture)
      : this(pageData, provider, isPreview, objectCulture, false)
    {
    }

    /// <summary>Gets the page template.</summary>
    /// <returns>The page template.</returns>
    /// <value>The page template.</value>
    public override ITemplate GetPageTemplate() => base.GetPageTemplate() ?? ControlUtilities.GetTemplate(RouteHandler.EmptyTemplateInfo);
  }
}
