// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormsRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Globalization;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>
  ///  Represents a route handler for Sitefinity's forms designer.
  /// </summary>
  public class FormsRouteHandler : RouteHandler
  {
    private CultureInfo culture;

    /// <summary>
    /// Gets or sets a value indicating whether the requested page is for preview or edit mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is preview; otherwise, <c>false</c>.
    /// </value>
    public bool IsPreview { get; set; }

    /// <summary>
    /// Gets or sets the language that must be used to display the edited form.
    /// </summary>
    /// <value>The object edit culture.</value>
    public CultureInfo ObjectEditCulture
    {
      get => this.culture != null ? this.culture : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
      set => this.culture = value;
    }

    /// <summary>Sets the page directives.</summary>
    /// <param name="handler">The handler.</param>
    /// <param name="pageData">The page data.</param>
    protected override void SetPageDirectives(Page handler, IPageData pageData) => ((DraftProxyBase) pageData).SetPageDirectives(handler);

    /// <summary>Gets the page data.</summary>
    /// <param name="node">The node.</param>
    /// <returns></returns>
    protected override IPageData GetPageData(SiteMapNode node)
    {
      IDictionary items = SystemManager.CurrentHttpContext.Items;
      FormDraftProxy pageData = (FormDraftProxy) items[(object) "FormDescriptionProxy"];
      if (pageData == null)
      {
        if (!(node is FormsSiteNode formNode))
          throw new ArgumentException("Invalid node provided.");
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        CultureInfo objectEditCulture = this.ObjectEditCulture;
        if (objectEditCulture != null)
          SystemManager.CurrentContext.Culture = this.ObjectEditCulture;
        try
        {
          FormsManager manager = FormsManager.GetManager(formNode.FormsProviderName);
          FormDescription formByName = manager.GetFormByName(formNode.Key);
          if (formByName == null)
            throw new HttpException(404, "There is no form with the specified name \"{0}\"".Arrange((object) formNode.Key));
          Guid lockedBy;
          FormDraft formDraft;
          if (this.IsLocked(formNode, out lockedBy))
          {
            formDraft = manager.CreateDraft(formByName.Name);
            formDraft.ParentForm = formByName;
          }
          else if (this.IsPreview)
          {
            formDraft = manager.GetPreview(formByName.Id);
          }
          else
          {
            formDraft = manager.EditForm(formByName.Id, true);
            manager.SaveChanges();
          }
          pageData = new FormDraftProxy(formDraft, formByName, manager.Provider, this.IsPreview, this.ObjectEditCulture);
          pageData.LockedBy = lockedBy;
          items[(object) "FormDescriptionProxy"] = (object) pageData;
        }
        finally
        {
          if (objectEditCulture != null)
            SystemManager.CurrentContext.Culture = culture;
        }
      }
      return (IPageData) pageData;
    }

    /// <summary>Gets the page template.</summary>
    /// <param name="pageData">The page data.</param>
    /// <returns></returns>
    protected override ITemplate GetPageTemplate(IPageData pageData) => ((DraftProxyBase) pageData).GetPageTemplate();

    /// <summary>
    /// Applies the layouts and controls to the provided page.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="requestContext">The request context.</param>
    protected override void ApplyLayoutsAndControls(Page page, RequestContext requestContext)
    {
      DraftProxyBase pageData = (DraftProxyBase) this.GetPageData((SiteMapNode) requestContext.RouteData.DataTokens["SiteMapNode"]);
      pageData.ApplyLayouts(page);
      pageData.CreateChildControls(page);
    }

    private bool IsLocked(FormsSiteNode formNode, out Guid lockedBy)
    {
      if (formNode == null)
      {
        lockedBy = Guid.Empty;
        return false;
      }
      FormDescription formByName = FormsManager.GetManager(formNode.FormsProviderName).GetFormByName(formNode.Key);
      if (!formByName.IsEditable("Forms", "Modify"))
        throw new SecurityDemandFailException("You are not authorized to edit the content of this form.");
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      lockedBy = formByName.LockedBy;
      return formByName.LockedBy != Guid.Empty && formByName.LockedBy != currentUserId && !this.IsPreview;
    }

    /// <summary>Provides the object that processes the request.</summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <returns>An object that processes the request.</returns>
    public override IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      SystemManager.SetPageDesignMode(true);
      SystemManager.SetPagePreviewMode(this.IsPreview);
      return base.GetHttpHandler(requestContext);
    }
  }
}
