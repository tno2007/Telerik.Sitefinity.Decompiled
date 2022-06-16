// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormDraftProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>Represents template draft runtime data.</summary>
  public class FormDraftProxy : DraftProxyBase
  {
    private FormDescription form;
    private FormDraft formDraft;
    private static TemplateInfo emptyTemplateInfo;
    internal const string FrontendFormsTemplateName = "Telerik.Sitefinity.Resources.Pages.FormsFrontend.aspx";
    internal const string DefaultPublicCss = "Telerik.Sitefinity.Resources.Themes.Light.Styles.FormsPreview.css";

    /// <summary>Gets the empty template info.</summary>
    /// <value>The empty template info.</value>
    internal static TemplateInfo EmptyTemplateInfo
    {
      get
      {
        if (FormDraftProxy.emptyTemplateInfo == null)
          FormDraftProxy.emptyTemplateInfo = new TemplateInfo()
          {
            TemplateName = "Telerik.Sitefinity.Resources.Pages.FormsFrontend.aspx",
            ControlType = typeof (PageRouteHandler)
          };
        return FormDraftProxy.emptyTemplateInfo;
      }
    }

    /// <summary>Gets the controls toolbox name.</summary>
    /// <value>The controls toolbox.</value>
    internal override string ControlsToolbox => "FormControls";

    /// <summary>Gets the layout toolbox name.</summary>
    /// <value>The layout toolbox.</value>
    internal override string LayoutToolbox => "PageLayouts";

    /// <summary>Gets the type of the proxy class.</summary>
    /// <value>The type of the proxy.</value>
    protected internal override DesignMediaType MediaType => DesignMediaType.Form;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.FormDraftProxy" /> class.
    /// </summary>
    /// <param name="formDraft">The form data.</param>
    /// <param name="form">The form description.</param>
    /// <param name="dataProvider">The data provider.</param>
    /// <param name="isPreview">if set to <c>true</c> zones won't be rendered.</param>
    /// <param name="objectEditCulture">The edit culture.</param>
    internal FormDraftProxy(
      FormDraft formDraft,
      FormDescription form,
      FormsDataProvider dataProvider,
      bool isPreview,
      CultureInfo objectEditCulture)
    {
      this.form = form;
      this.formDraft = formDraft;
      this.CurrentObjectCulture = objectEditCulture;
      this.PageDraftId = formDraft.Id;
      this.IsPreview = isPreview;
      this.PageTitle = (string) form.Title;
      this.FormsProvider = dataProvider;
      if (this.Settings.Multilingual)
      {
        this.IsSplitByLanguage = false;
        List<CultureInfo> cultureInfoList = new List<CultureInfo>();
        CultureInfo[] availableCultures = form.AvailableCultures;
        cultureInfoList.AddRange((IEnumerable<CultureInfo>) availableCultures);
        this.UsedLanguages = cultureInfoList;
      }
      this.ShowLocalizationStrategySelector = false;
      this.ParentItemId = form.Id;
      List<IControlsContainer> controlContainers = new List<IControlsContainer>();
      PageHelper.ProcessPresentationData(((IPresentable) formDraft).Presentation, (IList<Dictionary<string, ITemplate>>) this.Layouts, this.Templates);
      controlContainers.Add((IControlsContainer) formDraft);
      if (this.IsPreview)
        PageHelper.ProcessControls((IList<Telerik.Sitefinity.Modules.Pages.ControlBuilder>) this.Controls, (IList<IControlsContainer>) controlContainers);
      else
        PageHelper.ProcessControls((IList<ControlData>) this.DockedControls, (IList<ControlData>) this.PlaceHolders, (IList<IControlsContainer>) controlContainers);
      this.Layouts.Reverse();
      this.IncludeScriptManager = true;
      this.LastControlId = formDraft.LastControlId;
      if (form.Framework == FormFramework.WebForms)
      {
        this.Framework = PageTemplateFramework.WebForms;
      }
      else
      {
        if (form.Framework != FormFramework.Mvc)
          return;
        this.Framework = PageTemplateFramework.Mvc;
      }
    }

    /// <summary>Creates the child controls.</summary>
    /// <param name="page">The page.</param>
    public override void CreateChildControls(Page page)
    {
      this.CanUnlock = this.form.IsGranted("Forms", SecurityActionTypes.Unlock.ToString());
      base.CreateChildControls(page);
      if (this.IsPreview)
      {
        ClientScriptManager clientScript = page.ClientScript;
        HtmlLink child = new HtmlLink()
        {
          Href = clientScript.GetWebResourceUrl(Config.Get<ControlsConfig>().ResourcesAssemblyInfo, "Telerik.Sitefinity.Resources.Themes.Light.Styles.FormsPreview.css")
        };
        child.Attributes["rel"] = "stylesheet";
        child.Attributes["type"] = "text/css";
        child.Attributes["media"] = "all";
        page.Header.Controls.Add((Control) child);
        HtmlGenericControl control1 = page.FindControl("PublicWrapper") as HtmlGenericControl;
        switch (this.form.FormLabelPlacement)
        {
          case FormLabelPlacement.TopAligned:
            control1.Attributes.Add("class", "sfPublicWrapper sfTopLbls");
            break;
          case FormLabelPlacement.LeftAligned:
            control1.Attributes.Add("class", "sfPublicWrapper sfLeftLbls");
            break;
          case FormLabelPlacement.RightAligned:
            control1.Attributes.Add("class", "sfPublicWrapper sfRightLbls");
            break;
        }
        Control control2 = control1.FindControl("Body");
        ObjectFactory.Resolve<IFormMultipageDecorator>().AddMultiPageFormSeparators(control2);
        ObjectFactory.Resolve<IPreviewFormDecorator>().DecorateFormPreview(page, this.formDraft);
      }
      else
      {
        if (this.Toolbar == null)
          return;
        string statusKey = (string) null;
        string statusText = (string) null;
        LifecycleExtensions.GetLifecycleStatus((ILifecycleDataItemLive) this.form, this.CurrentObjectCulture, ref statusKey, ref statusText);
        this.Toolbar.PageStatusText = statusText;
        this.Toolbar.Status = statusKey;
        this.Toolbar.ItemVisible = this.form.Visible;
        this.Toolbar.ItemVersion = this.form.Version;
        this.Toolbar.WorkflowItemState = "Not implemented at the moment";
      }
    }

    /// <summary>Gets the page template.</summary>
    /// <returns>The page template.</returns>
    public override ITemplate GetPageTemplate() => this.form.Framework == FormFramework.Mvc ? ControlUtilities.GetTemplate(FormDraftProxy.EmptyTemplateInfo) : ControlUtilities.GetTemplate(RouteHandler.EmptyTemplateInfo);
  }
}
