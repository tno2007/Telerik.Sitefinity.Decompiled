// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ContentFilterSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  [ParseChildren(true)]
  public class ContentFilterSelector : SimpleScriptView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.ContentFilterSelector.ascx");
    private List<FilterSelectorItem> additionalItems;

    public string ProviderTypeName { get; set; }

    public string ItemTypeName { get; set; }

    public string UICulture { get; set; }

    public bool HideDatesSelector { get; set; }

    private List<FilterSelectorItem> AdditionalItems
    {
      get
      {
        if (this.additionalItems != null)
          return this.additionalItems;
        this.additionalItems = new List<FilterSelectorItem>();
        return this.additionalItems;
      }
      set => this.additionalItems = value;
    }

    public FilterSelector FilterSelector => this.Container.GetControl<FilterSelector>("filterSelector", true);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>();

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>();

    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentFilterSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public string GetClassificationPluralTitle(Guid taxaId) => (string) TaxonomyManager.GetManager().GetTaxonomy(taxaId).Title;

    public void AddSelectorView(WebControl control, FilterSelectorItem parentItem)
    {
      ContentFilterSelector.TemplateSelectorResultView selectorResultView = new ContentFilterSelector.TemplateSelectorResultView(control);
      parentItem.SelectorResultView = (ITemplate) selectorResultView;
      this.AdditionalItems.Add(parentItem);
    }

    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ItemTypeName == null)
        return;
      foreach (FilterSelectorItem additionalItem in this.AdditionalItems)
        this.FilterSelector.Items.Add(additionalItem);
      Type clrType = TypeResolutionService.ResolveType(this.ItemTypeName, false);
      if (clrType != (Type) null)
        this.PopulateTaxonomyFilters(clrType);
      FilterSelectorItem filterSelectorItem = this.FilterSelector.Items.Find((Predicate<FilterSelectorItem>) (item => item.QueryFieldType == "System.DateTime"));
      if (filterSelectorItem != null)
      {
        this.FilterSelector.Items.Remove(filterSelectorItem);
        this.FilterSelector.Items.Add(filterSelectorItem);
      }
      if (!this.HideDatesSelector || filterSelectorItem == null)
        return;
      this.FilterSelector.Items.Remove(filterSelectorItem);
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    private void PopulateTaxonomyFilters(Type clrType)
    {
      TaxonomyPropertyDescriptor[] propertiesForType = OrganizerBase.GetPropertiesForType(clrType);
      if (propertiesForType.Length == 0)
        return;
      int num = 0;
      foreach (TaxonomyPropertyDescriptor propertyDescriptor in propertiesForType)
      {
        string classificationPluralTitle = this.GetClassificationPluralTitle(propertyDescriptor.TaxonomyId);
        FilterSelectorItem filterSelectorItem1 = new FilterSelectorItem();
        filterSelectorItem1.ID = "FilterSelectorItem" + (object) num++;
        filterSelectorItem1.GroupLogicalOperator = "AND";
        filterSelectorItem1.ItemLogicalOperator = "OR";
        filterSelectorItem1.ConditionOperator = "Contains";
        filterSelectorItem1.QueryDataName = propertyDescriptor.Name;
        filterSelectorItem1.QueryFieldName = propertyDescriptor.MetaField.FieldName;
        filterSelectorItem1.Text = string.Format(Res.Get<TaxonomyResources>().SortBy, (object) classificationPluralTitle);
        FilterSelectorItem filterSelectorItem2 = filterSelectorItem1;
        if (this.ItemTypeName == "Telerik.Sitefinity.Ecommerce.Catalog.Model.Product" && filterSelectorItem2.QueryDataName == "Department")
          filterSelectorItem2.QueryDataName = "Departments";
        else if (filterSelectorItem2.QueryDataName == "Category")
          filterSelectorItem2.QueryDataName = "Categories";
        this.FilterSelector.Items.Add(filterSelectorItem2);
        switch (propertyDescriptor.TaxonomyType)
        {
          case TaxonomyType.Flat:
            FlatTaxonSelectorResultView selectorResultView1 = new FlatTaxonSelectorResultView();
            selectorResultView1.ID = "FlatTaxonSelectorResultView1";
            selectorResultView1.WebServiceUrl = "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc";
            selectorResultView1.AllowMultipleSelection = true;
            selectorResultView1.TaxonomyId = propertyDescriptor.TaxonomyId;
            selectorResultView1.UICulture = this.UICulture;
            WebControl control1 = (WebControl) selectorResultView1;
            filterSelectorItem2.SelectorResultView = (ITemplate) new ContentFilterSelector.TemplateSelectorResultView(control1);
            filterSelectorItem2.QueryFieldType = "System.Guid";
            break;
          case TaxonomyType.Hierarchical:
            HierarchicalTaxonSelectorResultView selectorResultView2 = new HierarchicalTaxonSelectorResultView();
            selectorResultView2.ID = "HierarchicalTaxonSelectorResultView1";
            selectorResultView2.WebServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc";
            selectorResultView2.AllowMultipleSelection = true;
            selectorResultView2.HierarchicalTreeRootBindModeEnabled = false;
            selectorResultView2.TaxonomyId = propertyDescriptor.TaxonomyId;
            selectorResultView2.UICulture = this.UICulture;
            WebControl control2 = (WebControl) selectorResultView2;
            filterSelectorItem2.SelectorResultView = (ITemplate) new ContentFilterSelector.TemplateSelectorResultView(control2);
            filterSelectorItem2.QueryFieldType = "System.Guid";
            break;
          default:
            DateRangeSelectorResultView selectorResultView3 = new DateRangeSelectorResultView();
            selectorResultView3.ID = "DateRangeSelectorResultView1";
            selectorResultView3.SelectorDateRangesTitle = Res.Get<Labels>().DisplayNewsPublishedIn;
            WebControl control3 = (WebControl) selectorResultView3;
            filterSelectorItem2.SelectorResultView = (ITemplate) new ContentFilterSelector.TemplateSelectorResultView(control3);
            filterSelectorItem2.QueryFieldType = "System.DateTime";
            break;
        }
        if (this.ItemTypeName == "Telerik.Sitefinity.Ecommerce.Catalog.Model.Product" && filterSelectorItem2.QueryDataName == "Departments")
          this.FilterSelector.SetTaxonomyId("Departments", TaxonomyManager.DepartmentsTaxonomyId);
        if (filterSelectorItem2.QueryDataName == "Categories")
          this.FilterSelector.SetTaxonomyId("Categories", TaxonomyManager.CategoriesTaxonomyId);
      }
    }

    private class TemplateSelectorResultView : ITemplate
    {
      private readonly WebControl resultView;

      public TemplateSelectorResultView(WebControl control) => this.resultView = control;

      public void InstantiateIn(Control container) => container.Controls.Add((Control) this.resultView);
    }
  }
}
