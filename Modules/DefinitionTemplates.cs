// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.DefinitionTemplates
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules
{
  public static class DefinitionTemplates
  {
    public static FlatTaxonFieldDefinitionElement TagsFieldWriteMode(
      ConfigElement section)
    {
      FlatTaxonFieldDefinitionElement definitionElement = new FlatTaxonFieldDefinitionElement(section);
      definitionElement.ID = "tagsFieldControl";
      definitionElement.DataFieldName = "Tags";
      definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      definitionElement.ResourceClassId = typeof (TaxonomyResources).Name;
      definitionElement.TaxonomyId = TaxonomyManager.TagsTaxonomyId;
      definitionElement.WebServiceUrl = "~/Sitefinity/Services/Taxonomies/FlatTaxon.svc";
      definitionElement.AllowMultipleSelection = true;
      definitionElement.WrapperTag = HtmlTextWriterTag.Li;
      definitionElement.Title = "Tags";
      definitionElement.ExpandableDefinitionConfig.Expanded = new bool?(false);
      definitionElement.ExpandableDefinitionConfig.ExpandText = "ClickToAddTags";
      definitionElement.ExpandableDefinitionConfig.ResourceClassId = typeof (TaxonomyResources).Name;
      return definitionElement;
    }

    public static HierarchicalTaxonFieldDefinitionElement CategoriesFieldWriteMode(
      ConfigElement section)
    {
      HierarchicalTaxonFieldDefinitionElement definitionElement = new HierarchicalTaxonFieldDefinitionElement(section);
      definitionElement.ID = "categoriesFieldControl";
      definitionElement.DataFieldName = "Category";
      definitionElement.DisplayMode = new FieldDisplayMode?(FieldDisplayMode.Write);
      definitionElement.ResourceClassId = typeof (TaxonomyResources).Name;
      definitionElement.TaxonomyId = TaxonomyManager.CategoriesTaxonomyId;
      definitionElement.WebServiceUrl = "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc";
      definitionElement.AllowMultipleSelection = true;
      definitionElement.WrapperTag = HtmlTextWriterTag.Li;
      definitionElement.Title = "Categories";
      definitionElement.ExpandableDefinitionConfig.Expanded = new bool?(false);
      definitionElement.ExpandableDefinitionConfig.ExpandText = "ClickToAddCategories";
      definitionElement.ExpandableDefinitionConfig.ResourceClassId = typeof (TaxonomyResources).Name;
      return definitionElement;
    }
  }
}
