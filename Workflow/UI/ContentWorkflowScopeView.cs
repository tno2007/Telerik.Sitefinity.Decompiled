// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.ContentWorkflowScopeView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Workflow.UI
{
  public class ContentWorkflowScopeView : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.ContentWorkflowScopeView.ascx");
    internal const string contentWorkflowScopeViewScriptName = "Telerik.Sitefinity.Workflow.Scripts.ContentWorkflowScopeView.js";
    protected const string CategoriesQueryDataName = "Categories";
    protected const string TagsQueryDataName = "Tags";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentWorkflowScopeView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SetTaxonomyId("Categories", TaxonomyManager.CategoriesTaxonomyId);
      this.SetTaxonomyId("Tags", TaxonomyManager.TagsTaxonomyId);
    }

    /// <summary>
    /// Sets the TaxonomyId property of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.FilterSelectorItem" /> instance with the specified query data name.
    /// </summary>
    /// <param name="queryDataName">The value of QueryDataName property.</param>
    /// <param name="taxonomyId">The id of the taxonomy.</param>
    protected virtual void SetTaxonomyId(string queryDataName, Guid taxonomyId)
    {
      FilterSelectorItem itemByQueryDataName = this.FilterSelector.FindItemByQueryDataName(queryDataName);
      if (itemByQueryDataName == null || !(itemByQueryDataName.ActualSelectorResultView is TaxonSelectorResultView selectorResultView))
        return;
      selectorResultView.TaxonomyId = taxonomyId;
    }

    /// <summary>
    /// Gets the radio button representing the selection for all content items
    /// </summary>
    protected virtual RadioButton AllContentRadio => this.Container.GetControl<RadioButton>("rbAllContent", true);

    /// <summary>
    /// Gets the radio button representing the selection of items filtered by category, tag, author or date
    /// </summary>
    protected virtual RadioButton SelectionOfContentRadio => this.Container.GetControl<RadioButton>("rbSelectionOfContent", true);

    /// <summary>
    /// Gets the radio button representing the selection of items filtered by parent (library or blog)
    /// </summary>
    protected virtual RadioButton ParentRadio => this.Container.GetControl<RadioButton>("rbParentSelection", true);

    /// <summary>Gets the radio button for advanced selection of items</summary>
    protected virtual RadioButton AdvancedSelectionRadio => this.Container.GetControl<RadioButton>("rbAdvancedSelection", true);

    /// <summary>
    /// Gets the client label manager used to work with localized resources on the client
    /// </summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets the filter selector used to construct the filter for advanced selection of content
    /// </summary>
    protected virtual FilterSelector FilterSelector => this.Container.GetControl<FilterSelector>("filterSelector", true);

    /// <summary>Gets the label showing the view title</summary>
    protected virtual Label ViewTitle => this.Container.GetControl<Label>("viewTitle", true);

    /// <summary>
    /// Gets the content selector used to select a parent to filter by
    /// </summary>
    protected virtual ContentSelector ParentContentSelector => this.Container.GetControl<ContentSelector>("contentSelector", true);

    /// <summary>Get the link that opens the parent content selector</summary>
    protected virtual LinkButton SelectParentContentLink => this.Container.GetControl<LinkButton>("lnkSelectParentContent", true);

    /// <summary>
    /// Gets the wrapper containing the button and label for selecting parent content
    /// </summary>
    protected virtual HtmlGenericControl ParentContentSelectionContainer => this.Container.GetControl<HtmlGenericControl>("parentContentSelectionContainer", true);

    /// <summary>Gets the parent content selector</summary>
    protected virtual HtmlGenericControl ParentContentSelectorWrapper => this.Container.GetControl<HtmlGenericControl>("parentContentSelectorWrapper", true);

    /// <summary>
    /// Gets the reference to the control that serves as a header.
    /// </summary>
    protected internal virtual HtmlGenericControl Header => this.Container.GetControl<HtmlGenericControl>("header", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (ContentWorkflowScopeView).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("viewTitleElement", this.ViewTitle.ClientID);
      controlDescriptor.AddProperty("allContentRadioId", (object) this.AllContentRadio.ClientID);
      controlDescriptor.AddProperty("selectionOfContentRadioId", (object) this.SelectionOfContentRadio.ClientID);
      controlDescriptor.AddProperty("parentRadioId", (object) this.ParentRadio.ClientID);
      controlDescriptor.AddProperty("advancedSelectionRadioId", (object) this.AdvancedSelectionRadio.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("filterSelector", this.FilterSelector.ClientID);
      controlDescriptor.AddComponentProperty("parentContentSelector", this.ParentContentSelector.ClientID);
      controlDescriptor.AddElementProperty("selectParentContentLink", this.SelectParentContentLink.ClientID);
      controlDescriptor.AddElementProperty("parentContentSelectorWrapper", this.ParentContentSelectorWrapper.ClientID);
      controlDescriptor.AddElementProperty("parentContentSelectionContainer", this.ParentContentSelectionContainer.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Workflow.Scripts.ContentWorkflowScopeView.js", typeof (ContentWorkflowScopeView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.FilterSelectorHelper.js", typeof (ContentWorkflowScopeView).Assembly.FullName)
    };
  }
}
