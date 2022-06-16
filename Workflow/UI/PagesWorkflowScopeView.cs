// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.PagesWorkflowScopeView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>
  /// View for selecting pages when defining workflow scope.
  /// </summary>
  public class PagesWorkflowScopeView : SimpleScriptView
  {
    internal const string pagesWorkflowScopeViewScriptName = "Telerik.Sitefinity.Workflow.Scripts.PagesWorkflowScopeView.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.PagesWorkflowScopeView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? PagesWorkflowScopeView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets the reference to the control for selecting a parent page from the sitemap.
    /// </summary>
    /// <value>The page selector control.</value>
    protected internal virtual PageSelector ParentPageSelector => this.Container.GetControl<PageSelector>("parentPageSelector", true);

    /// <summary>
    /// Gets the reference to the control for selecting multiple pages.
    /// </summary>
    /// <value>The pages selector control.</value>
    protected internal virtual PagesSelector PagesSelector => this.Container.GetControl<PagesSelector>("pagesSelector", true);

    /// <summary>Gets the control that shows the selected pages.</summary>
    /// <value>The control that shows the selected pages.</value>
    protected virtual PageItemsBuilder SelectedPagesBuilder => this.Container.GetControl<PageItemsBuilder>("selectedPagesBuilder", true);

    /// <summary>
    /// Gets the reference to the radio button for selecting "All Pages" option.
    /// </summary>
    protected internal virtual RadioButton AllPagesRadio => this.Container.GetControl<RadioButton>("rbAllPages", true);

    /// <summary>
    /// Gets the reference to the radio button for selecting "All pages under particular parrent page..." option.
    /// </summary>
    protected internal virtual RadioButton ParentPageRadio => this.Container.GetControl<RadioButton>("rbParentPage", true);

    /// <summary>
    /// Gets the reference to the radio button for selecting "Custom selection of pages..." option.
    /// </summary>
    protected internal virtual RadioButton CustomPagesRadio => this.Container.GetControl<RadioButton>("rbCustomPages", true);

    /// <summary>
    /// Gets the reference to the parentPageSelectionContainer control.
    /// </summary>
    protected internal virtual HtmlGenericControl ParentPageSelectionContainer => this.Container.GetControl<HtmlGenericControl>("parentPageSelectionContainer", true);

    /// <summary>
    /// Gets the reference to the pagesSelectionContainer control.
    /// </summary>
    protected internal virtual HtmlGenericControl PagesSelectionContainer => this.Container.GetControl<HtmlGenericControl>("pagesSelectionContainer", true);

    /// <summary>
    /// Gets the reference to the button opening page selector.
    /// </summary>
    protected internal virtual LinkButton SelectParentPageLink => this.Container.GetControl<LinkButton>("lnkSelectParentPage", true);

    /// <summary>
    /// Gets the reference to the button opening pages selector.
    /// </summary>
    protected internal virtual LinkButton SelectPagesLink => this.Container.GetControl<LinkButton>("lnkSelectPages", true);

    /// <summary>
    /// Gets the reference to the label displaying selected parent page.
    /// </summary>
    protected internal virtual HtmlGenericControl SelectedParentPageLabel => this.Container.GetControl<HtmlGenericControl>("selectedParentPageLabel", true);

    /// <summary>
    /// Gets the reference to the label displaying selected pages.
    /// </summary>
    protected internal virtual HtmlGenericControl SelectedPagesLabel => this.Container.GetControl<HtmlGenericControl>("selectedPagesLabel", true);

    /// <summary>
    /// Gets the reference to the control wrapping parent page selector.
    /// </summary>
    protected internal virtual HtmlGenericControl ParentPageSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("parentPageSelectorWrapper", true);

    /// <summary>
    /// Gets the reference to the control wrapping pages selector.
    /// </summary>
    protected internal virtual HtmlGenericControl PagesSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("pagesSelectorWrapper", true);

    /// <summary>
    /// Gets the reference to the control that serves as a header.
    /// </summary>
    protected internal virtual HtmlGenericControl Header => this.Container.GetControl<HtmlGenericControl>("header", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (PagesWorkflowScopeView).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("parentPageSelector", this.ParentPageSelector.ClientID);
      controlDescriptor.AddComponentProperty("pagesSelector", this.PagesSelector.ClientID);
      controlDescriptor.AddComponentProperty("selectedPagesBuilder", this.SelectedPagesBuilder.ClientID);
      controlDescriptor.AddElementProperty("allPagesRadio", this.AllPagesRadio.ClientID);
      controlDescriptor.AddElementProperty("parentPageRadio", this.ParentPageRadio.ClientID);
      controlDescriptor.AddElementProperty("customPagesRadio", this.CustomPagesRadio.ClientID);
      controlDescriptor.AddElementProperty("parentPageSelectionContainer", this.ParentPageSelectionContainer.ClientID);
      controlDescriptor.AddElementProperty("pagesSelectionContainer", this.PagesSelectionContainer.ClientID);
      controlDescriptor.AddElementProperty("selectParentPageLink", this.SelectParentPageLink.ClientID);
      controlDescriptor.AddElementProperty("selectPagesLink", this.SelectPagesLink.ClientID);
      controlDescriptor.AddElementProperty("selectedParentPageLabel", this.SelectedParentPageLabel.ClientID);
      controlDescriptor.AddElementProperty("selectedPagesLabel", this.SelectedPagesLabel.ClientID);
      controlDescriptor.AddElementProperty("parentPageSelectorWrapper", this.ParentPageSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("pagesSelectorWrapper", this.PagesSelectorWrapper.ClientID);
      controlDescriptor.AddProperty("_changeResource", (object) Res.Get<Labels>().ChangeDotDotDot);
      controlDescriptor.AddProperty("_selectParentResource", (object) Res.Get<Labels>().SelectParentWithDots);
      controlDescriptor.AddProperty("_addOtherPagesResource", (object) Res.Get<Labels>().AddOtherPagesWithDots);
      controlDescriptor.AddProperty("_allPagesUnderResource", (object) Res.Get<Labels>().AllPagesUnder);
      controlDescriptor.AddProperty("_allPagesUnderParticularParentPageResource", (object) Res.Get<Labels>().AllPagesUnderParticularParentPage);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Workflow.Scripts.PagesWorkflowScopeView.js", typeof (PagesWorkflowScopeView).Assembly.FullName)
    };
  }
}
