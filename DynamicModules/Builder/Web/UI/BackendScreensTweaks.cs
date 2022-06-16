// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.BackendScreensTweaks
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  /// <summary>
  /// Control which provides user with options to modify the backend screens of dynamic modules.
  /// </summary>
  public class BackendScreensTweaks : ModuleEditorBase
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.BackendScreensTweaks.ascx");
    private const string backendFormServiceUrl = "~/Sitefinity/Services/DynamicModules/BackendFormService.svc/";
    private const string backendGridServiceUrl = "~/Sitefinity/Services/DynamicModules/BackendGridStructureService.svc/";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = BackendScreensTweaks.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the reference to the title of the page</summary>
    protected virtual ITextControl PageTitle => this.Container.GetControl<ITextControl>("pageTitle", true);

    /// <summary>
    /// Gets the control that displays the title of the grid editor.
    /// </summary>
    protected virtual ITextControl GridEditorTitle => this.Container.GetControl<ITextControl>("gridEditorTitle", true);

    /// <summary>
    /// Gets the control that displays the title of the form editor.
    /// </summary>
    protected virtual ITextControl FormEditorTitle => this.Container.GetControl<ITextControl>("formEditorTitle", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the url of the
    /// backend grid service.
    /// </summary>
    public virtual HiddenField BackendGridServiceUrl => this.Container.GetControl<HiddenField>("backendGridServiceUrl", true);

    /// <summary>
    /// Gets the reference to the hidden field which holds the url of the
    /// backend form service.
    /// </summary>
    protected virtual HiddenField BackendFormServiceUrl => this.Container.GetControl<HiddenField>("backendFormServiceUrl", true);

    /// <summary>
    /// The link which takes the user back to the module page.
    /// </summary>
    protected virtual HyperLink BackToModulePageLink => this.Container.GetControl<HyperLink>("backToModulePageLink", true);

    /// <summary>
    /// Gets the reference control to the fields repeater in the grid editor.
    /// </summary>
    protected virtual Repeater ContentTypesRepeater => this.Container.GetControl<Repeater>("contentTypesRepeater", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.InitializeURLDependantVariables();
      this.PageTitle.Text = "Backend screen tweaks: <span style=\"font-style:italic\">" + this.Module.Title + "</span>";
      this.BackToModulePageLink.NavigateUrl = this.GetParentUrl();
      this.BackToModulePageLink.Text = "Back to <span style=\"font-style:italic\">" + this.Module.Title + "</span>";
      this.BackendGridServiceUrl.Value = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/DynamicModules/BackendGridStructureService.svc/");
      this.BackendFormServiceUrl.Value = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/DynamicModules/BackendFormService.svc/");
      this.BindContentTypesRepeater();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.ContentTypesRepeater.DataBind();
    }

    private void BindContentTypesRepeater()
    {
      this.ContentTypesRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ContentTypesRepeater_ItemDataBound);
      this.ContentTypesRepeater.DataSource = (object) this.ModuleBuilderMngr.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.ParentModuleId == this.ModuleId)).Select(mt => new
      {
        Id = mt.Id,
        Name = PluralsResolver.Instance.ToPlural(mt.DisplayName)
      });
    }

    private void ContentTypesRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      string str = DataBinder.Eval(e.Item.DataItem, "Name") as string;
      (e.Item.FindControl("listingItemsTitle") as Literal).Text = string.Format(Res.Get<ModuleBuilderResources>().ListingItems, (object) str);
      (e.Item.FindControl("creatingEditingItemsTitle") as Literal).Text = string.Format(Res.Get<ModuleBuilderResources>().CreatingEditingItems, (object) str);
    }

    private string GetParentUrl()
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(ModuleBuilderModule.contentTypeDashboardPageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.ModuleId;
    }
  }
}
