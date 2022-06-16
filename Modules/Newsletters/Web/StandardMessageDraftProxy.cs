// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.StandardMessageDraftProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web
{
  /// <summary>Represents the standard campaign draft runtime data.</summary>
  public class StandardMessageDraftProxy : DraftProxyBase
  {
    internal const string defaultPublicCss = "Telerik.Sitefinity.Resources.Themes.Basic.Styles.FormsPreview.css";

    internal StandardMessageDraftProxy(
      PageDraft standardCampaignDraft,
      MessageBody standardMessage,
      NewslettersDataProvider dataProvider,
      bool isPreview)
    {
      this.PageDraftId = standardCampaignDraft.Id;
      this.IsPreview = isPreview;
      this.PageTitle = standardMessage.Name;
      this.NewslettersDataProvider = dataProvider;
      this.PageProvider = PageManager.GetManager().Provider;
      this.ParentItemId = standardMessage.Id;
      this.UsedLanguages = new List<CultureInfo>();
      List<IControlsContainer> controlsContainerList = new List<IControlsContainer>();
      PageHelper.ProcessPresentationData(((IPresentable) standardCampaignDraft).Presentation, (IList<Dictionary<string, ITemplate>>) this.Layouts, this.Templates);
      controlsContainerList.Add((IControlsContainer) standardCampaignDraft);
      IPageTemplate template = (IPageTemplate) null;
      try
      {
        if (standardCampaignDraft.TemplateId != Guid.Empty)
        {
          bool suppressSecurityChecks = this.PageProvider.SuppressSecurityChecks;
          this.PageProvider.SuppressSecurityChecks = true;
          template = (IPageTemplate) this.PageProvider.GetTemplate(standardCampaignDraft.TemplateId);
          this.PageProvider.SuppressSecurityChecks = suppressSecurityChecks;
        }
      }
      catch (ItemNotFoundException ex)
      {
        throw new TemplateNotFoundException(ex.Message);
      }
      bool includeScriptManager = false;
      if (this.IsPreview)
      {
        string masterPage;
        PageHelper.ProcessTemplates(template, this.Layouts, this.Templates, controlsContainerList, out masterPage, out includeScriptManager);
        this.MasterPage = masterPage;
        PageHelper.ProcessControls((IList<Telerik.Sitefinity.Modules.Pages.ControlBuilder>) this.Controls, (IList<IControlsContainer>) controlsContainerList);
      }
      else
      {
        string masterPage;
        PageHelper.ProcessTemplates(template, this.Layouts, this.Templates, controlsContainerList, out masterPage, out includeScriptManager);
        this.MasterPage = masterPage;
        PageHelper.ProcessControls((IList<ControlData>) this.DockedControls, (IList<ControlData>) this.PlaceHolders, (IList<IControlsContainer>) controlsContainerList);
      }
      this.Layouts.Reverse();
      this.IncludeScriptManager = true;
      this.LastControlId = standardCampaignDraft.LastControlId;
      this.IsTemplate = standardMessage.IsTemplate;
    }

    /// <summary>Gets the type of the proxy class.</summary>
    /// <value>The type of the proxy.</value>
    protected internal override DesignMediaType MediaType => this.IsTemplate ? DesignMediaType.NewsletterTemplate : DesignMediaType.NewsletterCampaign;

    /// <summary>Gets the controls toolbox name.</summary>
    /// <value>The controls toolbox.</value>
    internal override string ControlsToolbox => "NewsletterControls";

    /// <summary>Gets the layout toolbox name.</summary>
    /// <value>The layout toolbox.</value>
    internal override string LayoutToolbox => "PageLayouts";

    /// <summary>
    /// Gets or sets a value indicating whether this instance is template.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is template; otherwise, <c>false</c>.
    /// </value>
    internal bool IsTemplate { get; private set; }

    /// <summary>Creates the child controls.</summary>
    /// <param name="page">The page.</param>
    public override void CreateChildControls(Page page) => base.CreateChildControls(page);

    /// <summary>Gets the page template.</summary>
    /// <returns></returns>
    /// <value>The page template.</value>
    public override ITemplate GetPageTemplate() => ControlUtilities.GetTemplate(RouteHandler.EmptyTemplateInfo);
  }
}
