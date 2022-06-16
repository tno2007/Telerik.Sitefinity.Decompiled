// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.TaxonomyResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Taxonomies
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("TaxonomyResources", ResourceClassId = "TaxonomyResources")]
  public class TaxonomyResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Taxonomies.TaxonomyResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public TaxonomyResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Taxonomies.TaxonomyResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public TaxonomyResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Generic Content</summary>
    [ResourceEntry("TaxonomyResourcesTitle", Description = "The title of this class.", LastModified = "2009/07/02", Value = "Taxonomies")]
    public string TaxonomyResourcesTitle => this[nameof (TaxonomyResourcesTitle)];

    /// <summary>Generic Content</summary>
    [ResourceEntry("TaxonomyResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2009/07/02", Value = "Taxonomies")]
    public string TaxonomyResourcesTitlePlural => this[nameof (TaxonomyResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Generic Content user interface.
    /// </summary>
    [ResourceEntry("TaxonomyResourcesDescription", Description = "The description of this class.", LastModified = "2009/07/02", Value = "Contains localizable resources for Taxonomy module.")]
    public string TaxonomyResourcesDescription => this[nameof (TaxonomyResourcesDescription)];

    /// <summary>Classification of content</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of Taxonomy module.", LastModified = "2010/02/03", Value = "Classification of content")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Classification of content</summary>
    [ResourceEntry("EditItem", Description = "The title of the edit taxon dialog", LastModified = "2011/05/30", Value = "Edit")]
    public string EditItem => this[nameof (EditItem)];

    /// <summary>Delete</summary>
    [ResourceEntry("DeleteThisItem", Description = "Delete", LastModified = "2011/05/30", Value = "Delete")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>phrase: Create this taxon</summary>
    [ResourceEntry("CreateThisItem", Description = "phrase: Create this tag", LastModified = "2011/05/17", Value = "Create this tag")]
    public string CreateThisItem => this[nameof (CreateThisItem)];

    /// <summary>Back to Tags</summary>
    [ResourceEntry("BackToItems", Description = "The text of the back to tags link", LastModified = "2011/05/17", Value = "Back to Tags")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>
    /// Defines the relationships that exist between separate content items in a coherent whole.
    /// </summary>
    [ResourceEntry("ModuleDescription", Description = "The description of Taxonomy module.", LastModified = "2009/07/31", Value = "Defines the relationships that exist between separate content items in a coherent whole.")]
    public string ModuleDescription => this[nameof (ModuleDescription)];

    /// <summary>word: Classifications</summary>
    [ResourceEntry("Classifications", Description = "word: Classifications", LastModified = "2009/12/12", Value = "Classifications")]
    public string Classifications => this[nameof (Classifications)];

    /// <summary>word: Classification</summary>
    [ResourceEntry("Classification", Description = "word: Classification", LastModified = "2009/12/12", Value = "Classification")]
    public string Classification => this[nameof (Classification)];

    /// <summary>phrase: Create new classification</summary>
    [ResourceEntry("CreateNewClassification", Description = "phrase: Create new classification", LastModified = "2009/12/12", Value = "Create new classification")]
    public string CreateNewClassification => this[nameof (CreateNewClassification)];

    /// <summary>phrase: Search inside classifications</summary>
    [ResourceEntry("SearchInsideClassifications", Description = "phrase: Search inside classifications", LastModified = "2009/12/12", Value = "Search inside classifications...")]
    public string SearchInsideClassifications => this[nameof (SearchInsideClassifications)];

    /// <summary>phrase: Search classifications</summary>
    [ResourceEntry("SearchClassifications", Description = "phrase: Search classifications", LastModified = "2009/12/12", Value = "Search classifications...")]
    public string SearchClassifications => this[nameof (SearchClassifications)];

    /// <summary>phrase: Close search classifications</summary>
    [ResourceEntry("CloseSearchClassifications", Description = "phrase: Close search classifications", LastModified = "2010/08/12", Value = "Close search classifications...")]
    public string CloseSearchClassifications => this[nameof (CloseSearchClassifications)];

    /// <summary>phrase: Categories and tags</summary>
    [ResourceEntry("CategoriesAndTags", Description = "phrase: Categories and tags", LastModified = "2010/08/12", Value = "Categories and tags")]
    public string CategoriesAndTags => this[nameof (CategoriesAndTags)];

    /// <summary>phrase: Filter classifications</summary>
    [ResourceEntry("FilterClassifications", Description = "phrase: Filter classifications", LastModified = "2009/12/12", Value = "Filter classifications")]
    public string FilterClassifications => this[nameof (FilterClassifications)];

    /// <summary>phrase: All classifications</summary>
    [ResourceEntry("AllClassifications", Description = "phrase: All classifications", LastModified = "2009/12/12", Value = "All classifications")]
    public string AllClassifications => this[nameof (AllClassifications)];

    /// <summary>phrase: Simple lists</summary>
    [ResourceEntry("SimpleLists", Description = "phrase: Simple lists", LastModified = "2009/12/12", Value = "Simple lists")]
    public string SimpleLists => this[nameof (SimpleLists)];

    /// <summary>word: Hierarchicals</summary>
    [ResourceEntry("Hierarchicals", Description = "word: Hierarchicals", LastModified = "2009/12/12", Value = "Hierarchicals")]
    public string Hierarchicals => this[nameof (Hierarchicals)];

    /// <summary>word: Faceted</summary>
    [ResourceEntry("Faceted", Description = "word: Faceted", LastModified = "2009/12/12", Value = "Faceted")]
    public string Faceted => this[nameof (Faceted)];

    /// <summary>phrase: Empty classifications</summary>
    [ResourceEntry("EmptyClassifications", Description = "phrase: Empty classifications", LastModified = "2009/12/12", Value = "Empty classifications")]
    public string EmptyClassifications => this[nameof (EmptyClassifications)];

    /// <summary>phrase: Permissions for all classifications</summary>
    [ResourceEntry("PermissionsForAllClassifications", Description = "phrase: Permissions for all classifications", LastModified = "2009/12/12", Value = "Permissions for all classifications")]
    public string PermissionsForAllClassifications => this[nameof (PermissionsForAllClassifications)];

    /// <summary>phrase: Classification / Type</summary>
    [ResourceEntry("ClassificationSlashType", Description = "phrase: Classification / Type", LastModified = "2009/12/12", Value = "Classification / Type")]
    public string ClassificationSlashType => this[nameof (ClassificationSlashType)];

    /// <summary>phrase: Flat list</summary>
    [ResourceEntry("FlatTaxonomyUserFriendly", Description = "phrase: Flat list", LastModified = "2009/12/12", Value = "Simple list")]
    public string FlatTaxonomyUserFriendly => this[nameof (FlatTaxonomyUserFriendly)];

    /// <summary>phrase: Hierarchical list</summary>
    [ResourceEntry("HierarchicalTaxonomyUserFriendly", Description = "phrase: Hierarchical list", LastModified = "2009/12/12", Value = "Hierarchical list")]
    public string HierarchicalTaxonomyUserFriendly => this[nameof (HierarchicalTaxonomyUserFriendly)];

    /// <summary>phrase: Create a Tag</summary>
    [ResourceEntry("CreateATagName", Description = "phrase: Create a Tag", LastModified = "2011/05/20", Value = "Create a Tag")]
    public string CreateATagName => this[nameof (CreateATagName)];

    /// <summary>phrase:Click To Add A Description</summary>
    [ResourceEntry("ClickToAddDescription", Description = "phrase: Click to add a description", LastModified = "2011/05/20", Value = "Click to add a description")]
    public string ClickToAddDescription => this["CreateATagName"];

    /// <summary>phrase:Description</summary>
    [ResourceEntry("lDescription", Description = "phrase: Description", LastModified = "2011/05/20", Value = "Description")]
    public string lDescription => this[nameof (lDescription)];

    /// <summary>phrase: Flat list</summary>
    [ResourceEntry("FlatTaxonomyUserFriendlyWithExample", Description = "phrase: Flat list", LastModified = "2009/12/12", Value = "Simple list<span class=\"sfExample\">Convenient for tags, keywords, one-level list of things.</span>")]
    public string FlatTaxonomyUserFriendlyWithExample => this[nameof (FlatTaxonomyUserFriendlyWithExample)];

    /// <summary>phrase: Hierarchical list</summary>
    [ResourceEntry("HierarchicalTaxonomyUserFriendlyWithExample", Description = "phrase: Hierarchical list", LastModified = "2009/12/12", Value = "Hierarchical list<span class=\"sfExample\">For Items, sub-items, sub-sub-items, etc. (tree).<br /> <strong>Example:</strong> List of continents, countries and cities.</span>")]
    public string HierarchicalTaxonomyUserFriendlyWithExample => this[nameof (HierarchicalTaxonomyUserFriendlyWithExample)];

    /// <summary>word: Tags</summary>
    [ResourceEntry("FlatTaxonomyMenuName", Description = "word: Tags", LastModified = "2010/02/03", Value = "Tags")]
    public string FlatTaxonomyMenuName => this[nameof (FlatTaxonomyMenuName)];

    /// <summary>word: Phrase: Permissions for tags</summary>
    [ResourceEntry("PermissionsForFlatTaxonomies", Description = "Phrase: Permissions for tags", LastModified = "2010/03/12", Value = "Permissions for tags")]
    public string PermissionsForFlatTaxonomies => this[nameof (PermissionsForFlatTaxonomies)];

    /// <summary>word: Phrase: Permissions for categories</summary>
    [ResourceEntry("PermissionsForHierarchicalTaxonomies", Description = "Phrase: Permissions for categories", LastModified = "2010/03/15", Value = "Permissions for categories")]
    public string PermissionsForHierarchicalTaxonomies => this[nameof (PermissionsForHierarchicalTaxonomies)];

    /// <summary>word: Categories</summary>
    [ResourceEntry("HierarchicalTaxonomyMenuName", Description = "word: Categories", LastModified = "2010/02/03", Value = "Categories")]
    public string HierarchicalTaxonomyMenuName => this[nameof (HierarchicalTaxonomyMenuName)];

    /// <summary>phrase: Network list</summary>
    [ResourceEntry("NetworkTaxonomyUserFriendly", Description = "phrase: Network list", LastModified = "2009/12/12", Value = "Network list")]
    public string NetworkTaxonomyUserFriendly => this[nameof (NetworkTaxonomyUserFriendly)];

    /// <summary>phrase: Facet list</summary>
    [ResourceEntry("FacetTaxonomyUserFriendly", Description = "phrase: Facet list", LastModified = "2009/12/12", Value = "Facet list")]
    public string FacetTaxonomyUserFriendly => this[nameof (FacetTaxonomyUserFriendly)];

    /// <summary>word: Flat</summary>
    [ResourceEntry("Flat", Description = "word: Flat", LastModified = "2009/12/12", Value = "Flat")]
    public string Flat => this[nameof (Flat)];

    /// <summary>word: Hierarchical</summary>
    [ResourceEntry("Hierarchical", Description = "word: Hierarchical", LastModified = "2009/12/12", Value = "Hierarchical")]
    public string Hierarchical => this[nameof (Hierarchical)];

    /// <summary>word: Network</summary>
    [ResourceEntry("Network", Description = "word: Network", LastModified = "2009/12/12", Value = "Network")]
    public string Network => this[nameof (Network)];

    /// <summary>word: Facet</summary>
    [ResourceEntry("Facet", Description = "word: Facet", LastModified = "2009/12/12", Value = "Facet")]
    public string Facet => this[nameof (Facet)];

    /// <summary>phrase: Single item</summary>
    [ResourceEntry("SingleItem", Description = "phrase: Single item", LastModified = "2009/12/12", Value = "Single item")]
    public string SingleItem => this[nameof (SingleItem)];

    /// <summary>word: Type</summary>
    [ResourceEntry("Type", Description = "word: Type", LastModified = "2009/12/12", Value = "Type")]
    public string Type => this[nameof (Type)];

    /// <summary>phrase: Name for program code</summary>
    [ResourceEntry("NameForProgramCode", Description = "phrase: Name for program code", LastModified = "2009/12/12", Value = "Name for program code")]
    public string NameForProgramCode => this[nameof (NameForProgramCode)];

    /// <summary>phrase: Delete classification</summary>
    [ResourceEntry("DeleteClassification", Description = "phrase: Delete classification", LastModified = "2009/12/12", Value = "Delete classification")]
    public string DeleteClassification => this[nameof (DeleteClassification)];

    /// <summary>phrase: Create a {0}</summary>
    [ResourceEntry("CreateATaxonName", Description = "phrase: Create a {0}", LastModified = "2009/12/12", Value = "Create a {0}")]
    public string CreateATaxonName => this[nameof (CreateATaxonName)];

    /// <summary>phrase: Edit a {0}</summary>
    [ResourceEntry("EditATaxonName", Description = "phrase: Edit a {0}", LastModified = "2009/01/06", Value = "Edit a {0}")]
    public string EditATaxonName => this[nameof (EditATaxonName)];

    /// <summary>phrase: Back to {0}</summary>
    [ResourceEntry("BackToTaxonomyName", Description = "phrase: Back to {0}", LastModified = "2009/12/13", Value = "Back to {0}")]
    public string BackToTaxonomyName => this[nameof (BackToTaxonomyName)];

    /// <summary>phrase: Create</summary>
    [ResourceEntry("CreateThisTaxonName", Description = "phrase", LastModified = "2009/12/13", Value = "Create")]
    public string CreateThisTaxonName => this[nameof (CreateThisTaxonName)];

    /// <summary>phrase: Create and add another</summary>
    [ResourceEntry("CreateAndAddAnotherTaxonName", Description = "phrase", LastModified = "2009/12/13", Value = "Create and add another")]
    public string CreateAndAddAnotherTaxonName => this[nameof (CreateAndAddAnotherTaxonName)];

    /// <summary>phrase: Example of the input for a flat taxon title</summary>
    [ResourceEntry("FlatTaxonTitleExample", Description = "phrase: Example of the input for a flat taxon title", LastModified = "2009/12/13", Value = "Example: <em>North America</em>")]
    public string FlatTaxonTitleExample => this[nameof (FlatTaxonTitleExample)];

    /// <summary>phrase: Edit synonyms</summary>
    [ResourceEntry("EditSynonyms", Description = "phrase: Edit synonyms", LastModified = "2009/12/14", Value = "Edit synonyms")]
    public string EditSynonyms => this[nameof (EditSynonyms)];

    /// <summary>word: Synonyms</summary>
    [ResourceEntry("Synonyms", Description = "word: Synonyms", LastModified = "2010/01/11", Value = "Synonyms")]
    public string Synonyms => this[nameof (Synonyms)];

    /// <summary>
    /// phrase: Synonyms (separated by space, e.g. flat apartment)
    /// </summary>
    [ResourceEntry("SynonymsExample", Description = "phrase: Synonyms (separated by space, e.g. flat apartment)", LastModified = "2010/01/11", Value = "Synonyms <span class='sfNote'>(separated by space, e.g. flat apartment)</span>")]
    public string SynonymsExample => this[nameof (SynonymsExample)];

    /// <summary>phrase: Edit URLs</summary>
    [ResourceEntry("EditUrlNames", Description = "phrase: Edit URLs", LastModified = "2009/12/14", Value = "Edit URLs")]
    public string EditUrlNames => this[nameof (EditUrlNames)];

    /// <summary>phrase: Back to classifications</summary>
    [ResourceEntry("BackToClassificationsName", Description = "phrase: Back to classifications", LastModified = "2010/01/06", Value = "Back to classifications")]
    public string BackToClassificationsName => this[nameof (BackToClassificationsName)];

    /// <summary>phrase: Create a Classification</summary>
    [ResourceEntry("CreateAClassificationName", Description = "phrase: Create a classification", LastModified = "2010/07/27", Value = "Create a classification")]
    public string CreateAClassificationName => this[nameof (CreateAClassificationName)];

    /// <summary>phrase: Edit a Classification</summary>
    [ResourceEntry("EditAClassificationName", Description = "phrase: Edit a Classification", LastModified = "2010/01/13", Value = "Edit a Classification")]
    public string EditAClassificationName => this[nameof (EditAClassificationName)];

    /// <summary>phrase: Create this Classification</summary>
    [ResourceEntry("CreateThisClassificationName", Description = "phrase: Create this Classification", LastModified = "2010/07/26", Value = "Create this classification")]
    public string CreateThisClassificationName => this[nameof (CreateThisClassificationName)];

    /// <summary>phrase: Geographic regions</summary>
    [ResourceEntry("ClassificationTitleExample", Description = "phrase: Geographic regions", LastModified = "2010/01/05", Value = "Example: <em>Geographic regions</em>")]
    public string ClassificationTitleExample => this[nameof (ClassificationTitleExample)];

    /// <summary>phrase: TaxonTitleExample</summary>
    [ResourceEntry("TaxonTitleExample", Description = "phrase: TaxonTitleExample", LastModified = "2010/01/05", Value = "Example: <em>North America</em>")]
    public string TaxonTitleExample => this[nameof (TaxonTitleExample)];

    /// <summary>phrase: Create new item</summary>
    [ResourceEntry("CreateNewItem", Description = "phrase: Create new item", LastModified = "2011/06/01", Value = "Create new item")]
    public string CreateNewItem => this[nameof (CreateNewItem)];

    /// <summary>phrase: Advanced</summary>
    [ResourceEntry("lAdvanced", Description = "phrase: Advanced", LastModified = "2010/01/05", Value = "Advanced")]
    public string lAdvanced => this[nameof (lAdvanced)];

    /// <summary>Phrase: URL</summary>
    [ResourceEntry("UrlName", Description = "phrase: URL", LastModified = "2010/03/15", Value = "URL")]
    public string UrlName => this[nameof (UrlName)];

    /// <summary>Phrase: URL cannot be empty</summary>
    [ResourceEntry("UrlNameCannotBeEmpty", Description = "phrase: URL cannot be empty", LastModified = "2010/03/15", Value = "URL cannot be empty")]
    public string UrlNameCannotBeEmpty => this[nameof (UrlNameCannotBeEmpty)];

    /// <summary>Phrase: URL contains invalid symbols</summary>
    [ResourceEntry("UrlNameInvalidSymbols", Description = "The message shown when the url contains invalid symbols.", LastModified = "2011/07/14", Value = "The URL contains invalid symbols.")]
    public string UrlNameInvalidSymbols => this[nameof (UrlNameInvalidSymbols)];

    /// <summary>phrase: Geographic region</summary>
    [ResourceEntry("ClassificationSingleItemExample", Description = "phrase: Geographic region", LastModified = "2010/01/05", Value = "Example: <em>Geographic region</em>")]
    public string ClassificationSingleItemExample => this[nameof (ClassificationSingleItemExample)];

    /// <summary>phrase: Type</summary>
    [ResourceEntry("ClassificationType", Description = "phrase: Type", LastModified = "2010/01/05", Value = "Type")]
    public string ClassificationType => this[nameof (ClassificationType)];

    /// <summary>phrase: Description</summary>
    [ResourceEntry("ClassificationDescription", Description = "phrase: Description", LastModified = "2010/01/05", Value = "Description")]
    public string ClassificationDescription => this[nameof (ClassificationDescription)];

    /// <summary>phrase: Single Item Name</summary>
    [ResourceEntry("SingleItemName", Description = "phrase: Single Item Name", LastModified = "2010/07/26", Value = "Single item name")]
    public string SingleItemName => this[nameof (SingleItemName)];

    /// <summary>word: Category</summary>
    [ResourceEntry("Category", Description = "word", LastModified = "2010/01/06", Value = "Category")]
    public string Category => this[nameof (Category)];

    /// <summary>word: Categories</summary>
    [ResourceEntry("Categories", Description = "word", LastModified = "2010/04/22", Value = "Categories")]
    public string Categories => this[nameof (Categories)];

    /// <summary>word: Tags</summary>
    [ResourceEntry("Tags", Description = "word", LastModified = "2010/04/22", Value = "Tags")]
    public string Tags => this[nameof (Tags)];

    /// <summary>word: Common categories</summary>
    [ResourceEntry("CommonCategories", Description = "phrase: Common categories", LastModified = "2010/06/15", Value = "Common categories")]
    public string CommonCategories => this[nameof (CommonCategories)];

    /// <summary>word: Common tags</summary>
    [ResourceEntry("CommonTags", Description = "phrase: Common tags", LastModified = "2010/06/15", Value = "Common tags")]
    public string CommonTags => this[nameof (CommonTags)];

    /// <summary>word: Taxon</summary>
    [ResourceEntry("Taxon", Description = "word", LastModified = "2010/01/06", Value = "Taxon")]
    public string Taxon => this[nameof (Taxon)];

    /// <summary>phrase: Simple List</summary>
    [ResourceEntry("SimpleList", Description = "phrase: Simple List", LastModified = "2010/01/07", Value = "Simple List")]
    public string SimpleList => this[nameof (SimpleList)];

    /// <summary>word: Hierarchy</summary>
    [ResourceEntry("Hierarchy", Description = "word: Hierarchy", LastModified = "2010/01/07", Value = "Hierarchy")]
    public string Hierarchy => this[nameof (Hierarchy)];

    /// <summary>word: Facets</summary>
    [ResourceEntry("Facets", Description = "word: Facets", LastModified = "2010/01/07", Value = "Facets")]
    public string Facets => this[nameof (Facets)];

    /// <summary>Messsage: Taxonomy Permissions</summary>
    /// <value>Title for the Taxonomy Permissions module.</value>
    [ResourceEntry("TaxonomyPermissionsTitle", Description = "Title for the Taxonomy Permissions module.", LastModified = "2010/01/07", Value = "Taxonomy Permissions")]
    public string TaxonomyPermissionsTitle => this[nameof (TaxonomyPermissionsTitle)];

    /// <summary>Messsage: Permissions</summary>
    /// <value>Taxonomy Permissions URL.</value>
    [ResourceEntry("TaxonomyPermissionsUrlName", Description = "Taxonomy Permissions URL.", LastModified = "2010/01/07", Value = "Permissions")]
    public string TaxonomyPermissionsUrlName => this[nameof (TaxonomyPermissionsUrlName)];

    /// <summary>Messsage: TaxonomyPermissions</summary>
    /// <value>Taxonomy Permissions Description.</value>
    [ResourceEntry("TaxonomyPermissionsDescription", Description = "Defines permissions for the Taxonomy module.", LastModified = "2010/01/07", Value = "TaxonomyPermissions")]
    public string TaxonomyPermissionsDescription => this[nameof (TaxonomyPermissionsDescription)];

    /// <summary>phrase: Item / Type / Status</summary>
    [ResourceEntry("MarkedItemTitle", Description = "phrase", LastModified = "2010/01/06", Value = "Item")]
    public string MarkedItemTitle => this[nameof (MarkedItemTitle)];

    /// <summary>phrase: Remove items from {0}</summary>
    [ResourceEntry("UnmarkItems", Description = "phrase", LastModified = "2010/01/06", Value = "Remove {0} from the selected items")]
    public string UnmarkItems => this[nameof (UnmarkItems)];

    /// <summary>phrase: Items under {0}</summary>
    [ResourceEntry("MarkedItemsTitle", Description = "phrase", LastModified = "2010/01/06", Value = "Items under {0}")]
    public string MarkedItemsTitle => this[nameof (MarkedItemsTitle)];

    /// <summary>phrase: Remove items from {0}</summary>
    [ResourceEntry("TaxonProperties", Description = "phrase", LastModified = "2010/01/06", Value = "{0} properties")]
    public string TaxonProperties => this[nameof (TaxonProperties)];

    /// <summary>phrase: Items in total</summary>
    [ResourceEntry("ItemsInTotal", Description = "phrase", LastModified = "2010/01/06", Value = "Items in total")]
    public string ItemsInTotal => this[nameof (ItemsInTotal)];

    /// <summary>phrase: Child category</summary>
    [ResourceEntry("ChildCategory", Description = "phrase: Create a child category", LastModified = "2010/01/07", Value = "Create a child category")]
    public string ChildCategory => this[nameof (ChildCategory)];

    /// <summary>phrase: Sibling category before</summary>
    [ResourceEntry("SiblingCategoryBefore", Description = "phrase: Sibling category before", LastModified = "2010/01/07", Value = "Sibling category before")]
    public string SiblingCategoryBefore => this[nameof (SiblingCategoryBefore)];

    /// <summary>phrase: Sibling category after</summary>
    [ResourceEntry("SiblingCategoryAfter", Description = "phrase: Sibling category after", LastModified = "2010/01/07", Value = "Sibling category after")]
    public string SiblingCategoryAfter => this[nameof (SiblingCategoryAfter)];

    /// <summary>phrase: Change parent {0}</summary>
    [ResourceEntry("ChangeParent", Description = "phrase: Change parent {0}", LastModified = "2010/01/07", Value = "Change parent {0}")]
    public string ChangeParent => this[nameof (ChangeParent)];

    /// <summary>
    /// phrase: Are you sure you want to delete this classification?
    /// </summary>
    [ResourceEntry("DeleteClassificationMessage", Description = "phrase", LastModified = "2010/01/06", Value = "Are you sure you want to delete this classification?")]
    public string DeleteClassificationMessage => this[nameof (DeleteClassificationMessage)];

    /// <summary>phrase: Bulk edit URLs and Synonyms</summary>
    [ResourceEntry("BulkEditFormTitle", Description = "phrase", LastModified = "2010/01/12", Value = "Bulk edit URLs and Synonyms")]
    public string BulkEditFormTitle => this[nameof (BulkEditFormTitle)];

    /// <summary>No parent (this {0} will be at the top level)</summary>
    [ResourceEntry("NoParentTaxonNameDescription", Description = "No parent (this {0} will be at the top level)", LastModified = "2010/04/21", Value = "No parent (this {0} will be at the top level)")]
    public string NoParentTaxonNameDescription => this[nameof (NoParentTaxonNameDescription)];

    /// <summary>Select a parent...</summary>
    [ResourceEntry("SelectedAParent", Description = "Select a parent...", LastModified = "2010/01/12", Value = "Select a parent...")]
    public string SelectedAParent => this[nameof (SelectedAParent)];

    /// <summary>Select a {0}</summary>
    [ResourceEntry("SelectATaxonName", Description = "Select a {0}", LastModified = "2010/01/12", Value = "Select a {0}")]
    public string SelectATaxonName => this[nameof (SelectATaxonName)];

    /// <summary>No items have been created yet</summary>
    [ResourceEntry("NoItemsHaveBeenCreated", Description = "No items have been created yet", LastModified = "2010/01/12", Value = "No items have been created yet")]
    public string NoItemsHaveBeenCreated => this[nameof (NoItemsHaveBeenCreated)];

    /// <summary>
    /// phrase: No {0} have been created yet. - argument is the title of the taxonomy
    /// </summary>
    [ResourceEntry("NoTaxaExistsYet", Description = "phrase: No {0} have been created yet. - argument is the title of the taxonomy", LastModified = "2011/05/31", Value = "No {0} have been created yet")]
    public string NoTaxaExistsYet => this[nameof (NoTaxaExistsYet)];

    /// <summary>No parent {0} have been selected.</summary>
    [ResourceEntry("NoParentSelected", Description = "No parent {0} have been selected.", LastModified = "2010/01/12", Value = "No parent {0} has been selected.")]
    public string NoParentSelected => this[nameof (NoParentSelected)];

    /// <summary>phrase: Save a {0}</summary>
    [ResourceEntry("SaveATaxonName", Description = "phrase: Save a {0}", LastModified = "2009/12/12", Value = "Save a {0}")]
    public string SaveATaxonName => this[nameof (SaveATaxonName)];

    /// <summary>phrase: Save</summary>
    [ResourceEntry("SaveThisTaxonName", Description = "phrase", LastModified = "2009/12/13", Value = "Save")]
    public string SaveThisTaxonName => this[nameof (SaveThisTaxonName)];

    /// <summary>phrase: Save and add another</summary>
    [ResourceEntry("SaveAndAddAnotherTaxonName", Description = "phrase", LastModified = "2009/12/13", Value = "Save and add another")]
    public string SaveAndAddAnotherTaxonName => this[nameof (SaveAndAddAnotherTaxonName)];

    /// <summary>phrase: (Type cannot be changed)</summary>
    [ResourceEntry("TypeCannotBeChanged", Description = "phrase", LastModified = "2009/12/13", Value = "<em class=\"sfNote\">(Type cannot be changed)</em>")]
    public string TypeCannotBeChanged => this[nameof (TypeCannotBeChanged)];

    /// <summary>message: Hierarchical taxonomy tree is malformed!</summary>
    [ResourceEntry("TaxonomyTreeMalformed", Description = "message", LastModified = "2009/12/14", Value = "Hierarchical taxonomy tree is malformed!")]
    public string TaxonomyTreeMalformed => this[nameof (TaxonomyTreeMalformed)];

    /// <summary>message: The requested taxon was not found.</summary>
    [ResourceEntry("TaxonNotFound", Description = "message", LastModified = "2009/12/14", Value = "The requested taxon was not found.")]
    public string TaxonNotFound => this[nameof (TaxonNotFound)];

    /// <summary>Link text for taxonomy help</summary>
    [ResourceEntry("ApplyTaxonomyHelp", Description = "Link text for taxonomy help", LastModified = "2010/01/14", Value = "Learn <strong>how to apply</strong> this classification to some content types, e.g. Blog posts, News, etc.")]
    public string ApplyTaxonomyHelp => this[nameof (ApplyTaxonomyHelp)];

    /// <summary>phrase: No classifications have been created yet.</summary>
    [ResourceEntry("NoClassificationsCreatedYet", Description = "phrase: No classifications have been created yet.", LastModified = "2010/01/14", Value = "No classifications have been created yet.")]
    public string NoClassificationsCreatedYet => this[nameof (NoClassificationsCreatedYet)];

    /// <summary>phrase: Create your first classification</summary>
    [ResourceEntry("CreateYourFirstClassification", Description = "phrase: Create your first classification.", LastModified = "2010/01/14", Value = "Create your first classification.")]
    public string CreateYourFirstClassification => this[nameof (CreateYourFirstClassification)];

    /// <summary>phrase: Set permissions for classifications</summary>
    [ResourceEntry("SetPermissionsForClassifications", Description = "phrase: Set permissions for classifications.", LastModified = "2010/01/14", Value = "Set permissions for classifications.")]
    public string SetPermissionsForClassifications => this[nameof (SetPermissionsForClassifications)];

    /// <summary>
    /// phrase: '{0}' classification has been successfully created.
    /// </summary>
    [ResourceEntry("ClassificationHasBeenSuccessfullyCreated", Description = "phrase: '{0}' classification has been successfully created.", LastModified = "2010/01/15", Value = "'<em>{0}</em>' classification has been successfully created.")]
    public string ClassificationHasBeenSuccessfullyCreated => this[nameof (ClassificationHasBeenSuccessfullyCreated)];

    /// <summary>
    /// phrase: Invalid operation. Cannot be parent to itself.
    /// </summary>
    [ResourceEntry("CannotBeParentToItself", Description = "phrase: Invalid operation: cannot be parent to itself.", LastModified = "2010/01/15", Value = "Invalid operation: cannot be parent to itself.")]
    public string CannotBeParentToItself => this[nameof (CannotBeParentToItself)];

    /// <summary>phrase: No classifications match the search query.</summary>
    [ResourceEntry("NoClassificationsMatchTheSearchQuery", Description = "phrase: No classifications match the search query.", LastModified = "2010/01/21", Value = "No classifications match the search query.")]
    public string NoClassificationsMatchTheSearchQuery => this[nameof (NoClassificationsMatchTheSearchQuery)];

    /// <summary>
    /// phrase : No parent (this {0} will be at the top level)
    /// </summary>
    [ResourceEntry("HierarchicalTaxonParentRootLabel", Description = "No parent (this {0} will be at the top level)", LastModified = "2010/03/06", Value = "No parent (this {0} will be at the top level)")]
    public string HierarchicalTaxonParentRootLabel => this[nameof (HierarchicalTaxonParentRootLabel)];

    [ResourceEntry("SelectAParent", Description = "phrase: Select a parent&hellip;", LastModified = "2010/03/06", Value = "Select a parent&hellip;")]
    public string SelectAParent => this[nameof (SelectAParent)];

    /// <summary>phrase: Parent</summary>
    [ResourceEntry("Parent", Description = "word: Parent", LastModified = "2010/03/06", Value = "Parent")]
    public string Parent => this[nameof (Parent)];

    /// <summary>phrase: Example : North America</summary>
    [ResourceEntry("ExampleNorthAmerica", Description = "phrase: Example : North America", LastModified = "2010/03/06", Value = "Example: <em>North America</em>")]
    public string ExampleNorthAmerica => this[nameof (ExampleNorthAmerica)];

    /// <summary>phrase: No {0} have been created yet.</summary>
    [ResourceEntry("NoTaxaHaveBeenCreatedYet", Description = "phrase: No {0} have been created yet.", LastModified = "2010/03/09", Value = "No {0} have been created yet.")]
    public string NoTaxaHaveBeenCreatedYet => this[nameof (NoTaxaHaveBeenCreatedYet)];

    /// <summary>phrase: No tags have been created yet.</summary>
    [ResourceEntry("NoTagsHaveBeenCreatedYet", Description = "phrase: No tags have been created yet.", LastModified = "2011/05/20", Value = "No tags have been created yet.")]
    public string NoTagsHaveBeenCreatedYet => this[nameof (NoTagsHaveBeenCreatedYet)];

    /// <summary>
    /// Title of the TaxaMasterGridView decisions screen action that creates a new taxon when the list is empty
    /// </summary>
    [ResourceEntry("CreateNewTaxon", Description = "Title of the TaxaMasterGridView decisions screen action that creates a new taxon when the list is empty", LastModified = "2011/05/23", Value = "Create new {0}")]
    public string CreateNewTaxon => this[nameof (CreateNewTaxon)];

    /// <summary>
    /// Error Message: An item with the URL '{0}' already exists. Please change the Url or delete the existing URL first.
    /// </summary>
    [ResourceEntry("DuplicateUrlException", Description = "Error message.", LastModified = "2009/12/19", Value = "{0} with URL '{1}' already exists in {2}.")]
    public string DuplicateUrlException => this[nameof (DuplicateUrlException)];

    /// <summary>Comma seperated, type new or existing tags</summary>
    [ResourceEntry("TagsFieldInstructions", Description = "Comma seperated, type new or existing tags", LastModified = "2009/12/19", Value = "<strong>Comma separated</strong>, type <strong>new</strong> or <strong>existing</strong> tags")]
    public string TagsFieldInstructions => this[nameof (TagsFieldInstructions)];

    /// <summary>Comma seperated, type new or existing tags</summary>
    [ResourceEntry("TagsFieldInstructionsNoCreatingAllowed", Description = "Comma seperated, type existing tags", LastModified = "2015/09/08", Value = "<strong>Comma separated</strong>, type <strong>existing</strong> tags")]
    public string TagsFieldInstructionsNoCreatingAllowed => this[nameof (TagsFieldInstructionsNoCreatingAllowed)];

    /// <summary>
    /// An empty collection of taxons was passed to the method, which is not allowed.
    /// </summary>
    [ResourceEntry("EmptyCollectionOfTaxonsIsNotAllowed", Description = "Error messages.", LastModified = "2010/10/12", Value = "An empty collection of taxons was passed to the method, which is not allowed.")]
    public string EmptyCollectionOfTaxonsIsNotAllowed => this[nameof (EmptyCollectionOfTaxonsIsNotAllowed)];

    /// <summary>Taxon cannot be moved</summary>
    [ResourceEntry("CannotMoveTaxon", Description = "Error messages.", LastModified = "2010/10/12", Value = "Taxon cannot be moved")]
    public string CannotMoveTaxon => this[nameof (CannotMoveTaxon)];

    /// <summary>Cannot move the first taxon up</summary>
    [ResourceEntry("CannotMoveFirstTaxonUp", Description = "Error messages.", LastModified = "2010/10/12", Value = "Cannot move the first taxon up")]
    public string CannotMoveFirstTaxonUp => this[nameof (CannotMoveFirstTaxonUp)];

    /// <summary>Cannot move the last taxon down</summary>
    [ResourceEntry("CannotMoveLastTaxonDown", Description = "Error messages.", LastModified = "2010/10/12", Value = "Cannot move the last taxon down")]
    public string CannotMoveLastTaxonDown => this[nameof (CannotMoveLastTaxonDown)];

    /// <summary>Cannot move the last taxon down</summary>
    [ResourceEntry("NewsItemDisplay", Description = "Type name display", LastModified = "2010/10/12", Value = "News items")]
    public string NewsItem => this["NewsItemDisplay"];

    /// <summary>phrase: Choose parent</summary>
    [ResourceEntry("ChooseParent", Description = "phrase: Choose parent", LastModified = "2010/11/04", Value = "Choose parent")]
    public string ChooseParent => this[nameof (ChooseParent)];

    /// <summary>
    /// phrase: Description which explains how to use FlatTaxonField. The argument is the title of the taxonomy.
    /// </summary>
    [ResourceEntry("TagsFieldDescription", Description = "phrase: Description which explains how to use FlatTaxonField. The argument is the title of the taxonomy.", LastModified = "2010/02/25", Value = "Type <strong>new</strong> or <strong>existing</strong> {0}, <strong>comma separated</strong>")]
    public string FlatTaxonFieldDescription => this["TagsFieldDescription"];

    /// <summary>phrase: Click to add categories</summary>
    [ResourceEntry("ClickToAddCategories", Description = "phrase: Click to add categories", LastModified = "2010/03/04", Value = "Click to add categories")]
    public string ClickToAddCategories => this[nameof (ClickToAddCategories)];

    /// <summary>phrase: Click to add tags</summary>
    [ResourceEntry("ClickToAddTags", Description = "phrase: Click to add tags", LastModified = "2010/03/04", Value = "Click to add tags")]
    public string ClickToAddTags => this[nameof (ClickToAddTags)];

    /// <summary>phrase: Click to add common categories</summary>
    [ResourceEntry("ClickToAddCommonCategories", Description = "phrase: Click to add common categories", LastModified = "2010/06/15", Value = "Click to add common categories")]
    public string ClickToAddCommonCategories => this[nameof (ClickToAddCommonCategories)];

    /// <summary>phrase: Click to add common tags</summary>
    [ResourceEntry("ClickToAddCommonTags", Description = "phrase: Click to add common tags", LastModified = "2010/06/15", Value = "Click to add common tags")]
    public string ClickToAddCommonTags => this[nameof (ClickToAddCommonTags)];

    /// <summary>phrase: Click to add synonyms.</summary>
    [ResourceEntry("ClickToAddSynonyms", Description = "phrase: Click to add synonyms.", LastModified = "2010/03/15", Value = "Click to add synonyms.")]
    public string ClickToAddSynonyms => this[nameof (ClickToAddSynonyms)];

    /// <summary>phrase: Click to add taxon.</summary>
    [ResourceEntry("ClickToAddTaxon", Description = "phrase: Click to add taxon.", LastModified = "2010/03/15", Value = "Click to add taxon.")]
    public string ClickToAddTaxon => this[nameof (ClickToAddTaxon)];

    /// <summary>phrase: For developers: name used in code.</summary>
    [ResourceEntry("ForDevelopersNameUsedInCode", Description = "phrase: For developers: name used in code.", LastModified = "2010/03/15", Value = "For developers: name used in code.")]
    public string ForDevelopersNameUsedInCode => this[nameof (ForDevelopersNameUsedInCode)];

    /// <summary>phrase: Title cannot be empty.</summary>
    [ResourceEntry("TitleCannotBeEmpty", Description = "phrase: Title cannot be empty.", LastModified = "2010/03/15", Value = "Title cannot be empty.")]
    public string TitleCannotBeEmpty => this[nameof (TitleCannotBeEmpty)];

    /// <summary>phrase: Description cannot be empty</summary>
    [ResourceEntry("DescriptionCannotBeEmpty", Description = "phrase: Description cannot be empty.", LastModified = "2010/03/15", Value = "Description cannot be empty.")]
    public string DescriptionCannotBeEmpty => this[nameof (DescriptionCannotBeEmpty)];

    /// <summary>phrase: Single item name cannot be empty.</summary>
    [ResourceEntry("SingleItemNameCannotBeEmpty", Description = "phrase: Single item name cannot be empty.", LastModified = "2010/03/15", Value = "Single item name cannot be empty.")]
    public string SingleItemNameCannotBeEmpty => this[nameof (SingleItemNameCannotBeEmpty)];

    /// <summary>phrase: Name used in code cannot be empty.</summary>
    [ResourceEntry("NameUsedInCodeCannotBeEmpty", Description = "phrase: Name used in code cannot be empty.", LastModified = "2010/03/15", Value = "Name used in code cannot be empty.")]
    public string NameUsedInCodeCannotBeEmpty => this[nameof (NameUsedInCodeCannotBeEmpty)];

    /// <summary>phrase: Classification cannot be empty</summary>
    [ResourceEntry("ClassificationCannotBeEmpty", Description = "phrase: Classification cannot be empty", LastModified = "2010/03/15", Value = "Classification cannot be empty")]
    public string ClassificationCannotBeEmpty => this[nameof (ClassificationCannotBeEmpty)];

    /// <summary>phrase: Item name cannot be empty</summary>
    [ResourceEntry("ItemNameCannotBeEmpty", Description = "phrase: Item name cannot be empty", LastModified = "2010/03/15", Value = "Item name cannot be empty")]
    public string ItemNameCannotBeEmpty => this[nameof (ItemNameCannotBeEmpty)];

    /// <summary>phrase: For developers: name used in code</summary>
    [ResourceEntry("NameUsedInCode", Description = "phrase: For developers: name used in code", LastModified = "2010/03/15", Value = "For developers: name used in code")]
    public string NameUsedInCode => this[nameof (NameUsedInCode)];

    /// <summary>phrase: Click to add name used in code</summary>
    [ResourceEntry("ClickToAddNameUsedInCode", Description = "phrase: Click to add name used in code", LastModified = "2010/03/15", Value = "Click to add name used in code")]
    public string ClickToAddNameUsedInCode => this[nameof (ClickToAddNameUsedInCode)];

    /// <summary>
    /// phrase: Convenient for tags, keywords, one-level list of things. <a href="#" class="sfMoreDetails">More details</a>
    /// </summary>
    [ResourceEntry("ClassificationSimpleListExample", Description = "phrase: Convenient for tags, keywords, one-level list of things. <a href='#' class='sfMoreDetails'>More details</a>", LastModified = "2010/03/15", Value = "Convenient for tags, keywords, one-level list of things.")]
    public string ClassificationSimpleListExample => this[nameof (ClassificationSimpleListExample)];

    /// <summary>
    /// phrase: For Items, sub-items, sub-sub-items, etc. (tree).<br /> <strong>Example:</strong> List of continents, countries and cities. <a href="#" class="sfMoreDetails">More details</a>
    /// </summary>
    [ResourceEntry("ClassificationHierarchyListExample", Description = "phrase: For Items, sub-items, sub-sub-items, etc. (tree).<br /> <strong>Example:</strong> List of continents, countries and cities. <a href='#' class='sfMoreDetails'>More details</a>", LastModified = "2010/03/15", Value = "For Items, sub-items, sub-sub-items, etc. (tree).<br /> <strong>Example:</strong> List of continents, countries and cities.")]
    public string ClassificationHierarchyListExample => this[nameof (ClassificationHierarchyListExample)];

    /// <summary>phrase: Parent {0}</summary>
    [ResourceEntry("ParentTaxonName", Description = "phrase: Parent {0}", LastModified = "2010/04/20", Value = "Parent {0}")]
    public string ParentTaxonName => this[nameof (ParentTaxonName)];

    /// <summary>Phrase: Save changes</summary>
    [ResourceEntry("SaveChanges", Description = "Phrase", LastModified = "2011/05/10", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>Label: Learn more with video tutorials</summary>
    [ResourceEntry("LearnMoreWithVideoTutorials", Description = "Label for the external links used in Taxonomies", LastModified = "2011/05/13", Value = "Learn more with video tutorials")]
    public string LearnMoreWithVideoTutorials => this[nameof (LearnMoreWithVideoTutorials)];

    [ResourceEntry("HowToSetupAndUseTags", Description = "Label: How to setup and use tags <span class=\"sfDuration\">(3:52 min)", LastModified = "2011/05/13", Value = "How to setup and use tags <span class=\"sfDuration\">(3:52 min)")]
    public string HowToSetupAndUseTags => this[nameof (HowToSetupAndUseTags)];

    /// <summary>
    /// Markup that is shown in MarkedItems that displays statistics about taxon usage. Something like 'Tax Cats'
    /// </summary>
    [ResourceEntry("TaxonNameTaxonTitleMarkup", Description = "Markup that is shown in MarkedItems that displays statistics about taxon usage. Something like 'Tax Cats'", LastModified = "2011/05/23", Value = "{0} <em>{1}</em>")]
    public string TaxonNameTaxonTitleMarkup => this[nameof (TaxonNameTaxonTitleMarkup)];

    /// <summary>Phrase: Name contains invalid symbols</summary>
    [ResourceEntry("DevNameInvalidSymbols", Description = "The message shown when the dev name contains invalid symbols.", LastModified = "2011/07/14", Value = "The name contains invalid symbols.")]
    public string DevNameInvalidSymbols => this[nameof (DevNameInvalidSymbols)];

    /// <summary>word: Departments</summary>
    [ResourceEntry("Departments", Description = "word: Departments", LastModified = "2011/07/31", Value = "Departments")]
    public string Departments => this[nameof (Departments)];

    /// <summary>phrase: Select departments...</summary>
    [ResourceEntry("SelectDepartments", Description = "phrase: Select departments...", LastModified = "2011/07/31", Value = "Select departments...")]
    public string SelectDepartments => this[nameof (SelectDepartments)];

    /// <summary>word: Taxa</summary>
    [ResourceEntry("TaxaPluralTypeName", Description = "word: Taxa", LastModified = "2011/11/25", Value = "Taxa")]
    public string TaxaPluralTypeName => this[nameof (TaxaPluralTypeName)];

    /// <summary>phrase: Sort</summary>
    [ResourceEntry("Sort", Description = "Label text.", LastModified = "2013/01/09", Value = "Sort")]
    public string Sort => this[nameof (Sort)];

    /// <summary>phrase: By Title (A-Z)</summary>
    [ResourceEntry("ByTitleAsc", Description = "Label text.", LastModified = "2012/01/09", Value = "By Title (A-Z)")]
    public string ByTitleAsc => this[nameof (ByTitleAsc)];

    /// <summary>phrase: By Title (Z-A)</summary>
    [ResourceEntry("ByTitleDesc", Description = "Label text.", LastModified = "2012/01/09", Value = "By Title (Z-A)")]
    public string ByTitleDesc => this[nameof (ByTitleDesc)];

    /// <summary>phrase: URL (A-Z)</summary>
    [ResourceEntry("UrlAsc", Description = "Label text.", LastModified = "2013/01/10", Value = "By URL (A-Z)")]
    public string UrlAsc => this[nameof (UrlAsc)];

    /// <summary>phrase: Last modified on top</summary>
    [ResourceEntry("NewModifiedFirst", Description = "Label text.", LastModified = "2013/01/10", Value = "Last modified on top")]
    public string NewModifiedFirst => this[nameof (NewModifiedFirst)];

    /// <summary>phrase: New-created first</summary>
    [ResourceEntry("NewCreatedFirst", Description = "Label text.", LastModified = "2013/01/10", Value = "Last created on top")]
    public string NewCreatedFirst => this[nameof (NewCreatedFirst)];

    /// <summary>phrase: URL (Z-A)</summary>
    [ResourceEntry("UrlDesc", Description = "Label text.", LastModified = "2013/01/10", Value = "By URL(Z-A)")]
    public string UrlDesc => this[nameof (UrlDesc)];

    /// <summary>Phrase: Custom sorting...</summary>
    [ResourceEntry("CustomSorting", Description = "Phrase: Custom sorting...", LastModified = "2013/01/09", Value = "Custom sorting...")]
    public string CustomSorting => this[nameof (CustomSorting)];

    /// <summary>phrase: by Hierarchy</summary>
    [ResourceEntry("ByHierarchy", Description = "Label text.", LastModified = "2013/01/10", Value = "by Hierarchy")]
    public string ByHierarchy => this[nameof (ByHierarchy)];

    /// <summary>phrase: Tags, Categories, Departments etc</summary>
    /// <value>Tags, Categories, Departments etc</value>
    [ResourceEntry("TaxonTypeNames", Description = "phrase: Tags, Categories, Departments etc", LastModified = "2013/07/03", Value = "Tags, Categories, Departments etc")]
    public string TaxonTypeNames => this[nameof (TaxonTypeNames)];

    /// <summary>phrase: by {0}...</summary>
    [ResourceEntry("SortBy", Description = "by {0}...", LastModified = "2013/10/31", Value = "by {0}... ")]
    public string SortBy => this[nameof (SortBy)];

    /// <summary>phrase: Change {0} for this site</summary>
    [ResourceEntry("SetTaxonomyForThisSite", Description = "phrase: Change {0} for this site", LastModified = "2015/01/26", Value = "Change {0} for this site")]
    public string SetTaxonomyForThisSite => this[nameof (SetTaxonomyForThisSite)];

    /// <summary>phrase: Use {0} from...</summary>
    [ResourceEntry("UseTaxonomyFrom", Description = "phrase: Use {0} from...", LastModified = "2015/02/03", Value = "Use {0} from...")]
    public string UseTaxonomyFrom => this[nameof (UseTaxonomyFrom)];

    /// <summary>phrase:  and {0} more</summary>
    [ResourceEntry("AndNumberMore", Description = "phrase:  and {0} more", LastModified = "2015/02/04", Value = " and {0} more")]
    public string AndNumberMore => this[nameof (AndNumberMore)];

    /// <summary>phrase: This site uses {0} shared with...</summary>
    [ResourceEntry("ThisSiteUsesTaxonomySharedWith", Description = "phrase: This site uses {0} shared with...", LastModified = "2015/01/26", Value = "This site uses {0} shared with...")]
    public string ThisSiteUsesTaxonomySharedWith => this[nameof (ThisSiteUsesTaxonomySharedWith)];

    /// <summary>phrase: Other sites...</summary>
    [ResourceEntry("OtherSites", Description = "phrase: Other sites...", LastModified = "2015/01/26", Value = "Other sites...")]
    public string OtherSites => this[nameof (OtherSites)];

    /// <summary>phrase: This site only in title</summary>
    [ResourceEntry("ThisSiteOnlyTitle", Description = "phrase: This site only in title", LastModified = "2015/01/26", Value = "<em class=\"sfLightTxt sfBiggerTxt sfAlignMiddle sfMLeft10\">(This site only)</em>")]
    public string ThisSiteOnlyTitle => this[nameof (ThisSiteOnlyTitle)];

    /// <summary>phrase: This site only</summary>
    [ResourceEntry("ThisSiteOnly", Description = "phrase: This site only", LastModified = "2015/01/26", Value = "This site only")]
    public string ThisSiteOnly => this[nameof (ThisSiteOnly)];

    /// <summary>phrase: Usage on sites</summary>
    [ResourceEntry("UsageOnSites", Description = "phrase: Usage on sites", LastModified = "2020/03/09", Value = "Usage on sites")]
    public string UsageOnSites => this[nameof (UsageOnSites)];

    /// <summary>
    /// phrase: Already applied {0} will be removed from the content items of any selected site.
    /// </summary>
    [ResourceEntry("WarningForAllSites", Description = "phrase: Already applied {0} will be removed from the content items of any selected site.", LastModified = "2020/03/09", Value = "Already applied {0} will be removed from the content items of any selected site.")]
    public string WarningForAllSites => this[nameof (WarningForAllSites)];

    /// <summary>
    /// phrase: Already applied {0} will be removed from the content items in this site.
    /// </summary>
    [ResourceEntry("WarningForThisSite", Description = "phrase: Already applied {0} will be removed from the content items in this site.", LastModified = "2020/03/09", Value = "Already applied {0} will be removed from the content items in this site.")]
    public string WarningForThisSite => this[nameof (WarningForThisSite)];

    /// <summary>phrase: Use {0} for sites...</summary>
    [ResourceEntry("UseForSites", Description = "phrase: Use {0} for sites...", LastModified = "2020/03/09", Value = "Use {0} for sites...")]
    public string UseForSites => this[nameof (UseForSites)];

    /// <summary>phrase: Change usage of {0} for...</summary>
    [ResourceEntry("ChangeUsageFor", Description = "phrase: Change usage of {0} for...", LastModified = "2020/03/09", Value = "Change usage of {0} for...")]
    public string ChangeUsageFor => this[nameof (ChangeUsageFor)];

    /// <summary>phrase: Used in {0} sites</summary>
    [ResourceEntry("UsedInSites", Description = "phrase: Used in {0} sites", LastModified = "2015/02/09", Value = "<em class=\"sfLightTxt sfBiggerTxt sfAlignMiddle sfMLeft10\">(Used in {0} sites)</em>")]
    public string UsedInSites => this[nameof (UsedInSites)];

    /// <summary>phrase: Not used</summary>
    [ResourceEntry("NotUsed", Description = "phrase: Not used", LastModified = "2015/02/11", Value = "Not used")]
    public string NotUsed => this[nameof (NotUsed)];

    /// <summary>phrase: Not used title</summary>
    [ResourceEntry("NotUsedTitle", Description = "phrase: Not used", LastModified = "2015/02/11", Value = "<em class=\"sfLightTxt sfBiggerTxt sfAlignMiddle sfMLeft10\">(Not used)</em>")]
    public string NotUsedTitle => this[nameof (NotUsedTitle)];

    /// <summary>phrase: Duplicate existing {0} to use on this site</summary>
    [ResourceEntry("DuplicateExistingTaxonomy", Description = "phrase: Duplicate existing {0} to use on this site", LastModified = "2020/03/09", Value = "Duplicate existing {0} to use on this site")]
    public string DuplicateExistingTaxonomy => this[nameof (DuplicateExistingTaxonomy)];

    /// <summary>phrase: Duplicate current {0} for this site</summary>
    [ResourceEntry("DuplicateSurrentlyTaxonomy", Description = "phrase: Duplicate current {0} for this site", LastModified = "2015/01/26", Value = "Duplicate current {0} for this site")]
    public string DuplicateSurrentlyTaxonomy => this[nameof (DuplicateSurrentlyTaxonomy)];

    /// <summary>
    /// phrase: Are you sure you want to change the {0} used in this site?
    /// </summary>
    [ResourceEntry("AreYouSureYouWantToChangeTheTaxonomy", Description = "phrase: Are you sure you want to change the {0} used in this site?", LastModified = "2015/01/26", Value = "Are you sure you want to change the {0} used in this site?")]
    public string AreYouSureYouWantToChangeTheTaxonomy => this[nameof (AreYouSureYouWantToChangeTheTaxonomy)];

    /// <summary>phrase: Warning</summary>
    [ResourceEntry("Warning", Description = "phrase: Warning", LastModified = "2015/01/26", Value = "Warning")]
    public string Warning => this[nameof (Warning)];

    /// <summary>
    /// phrase: Already applied {0} will be removed from the content items in this site
    /// </summary>
    [ResourceEntry("AppliedTaxonomyWillRemoved", Description = "phrase: Already applied {0} will be removed from the content items in this site", LastModified = "2015/01/26", Value = "Already applied {0} will be removed from the content items in this site")]
    public string AppliedTaxonomyWillRemoved => this[nameof (AppliedTaxonomyWillRemoved)];

    /// <summary>phrase: Yes, change {0}</summary>
    [ResourceEntry("ChangeTaxonomy", Description = "phrase: Yes, change {0}", LastModified = "2015/01/26", Value = "Yes, change {0}")]
    public string ChangeTaxonomy => this[nameof (ChangeTaxonomy)];

    /// <summary>phrase: Not used in any site</summary>
    [ResourceEntry("NotUsedTaxonomies", Description = "phrase: Not used in any site", LastModified = "2015/01/26", Value = "Not used in any site")]
    public string NotUsedTaxonomies => this[nameof (NotUsedTaxonomies)];

    /// <summary>
    /// phrase: Are you sure you want to change {0} used by selected site(s)?
    /// </summary>
    [ResourceEntry("ShareTaxonomyBySitesConfirmation", Description = "phrase: Are you sure you want to change {0} used by selected site(s)?", LastModified = "2015/03/10", Value = "Are you sure you want to change {0} used by selected site(s)?")]
    public string ShareTaxonomyBySitesConfirmation => this[nameof (ShareTaxonomyBySitesConfirmation)];

    /// <summary>
    /// phrase: Already applied {0} will be removed from the content items in the selected site(s)
    /// </summary>
    [ResourceEntry("ShareTaxonomyBySitesWarning", Description = "phrase: Already applied {0} will be removed from the content items in the selected site(s)", LastModified = "2015/03/10", Value = "Already applied {0} will be removed from the content items in the selected site(s)")]
    public string ShareTaxonomyBySitesWarning => this[nameof (ShareTaxonomyBySitesWarning)];

    /// <summary>phrase: Yes, change {0}</summary>
    [ResourceEntry("ShareTaxonomyBySiteConfirmationButtonLabel", Description = "phrase: Yes, change {0}", LastModified = "2015/03/09", Value = "Yes, change {0}")]
    public string ShareTaxonomyBySiteConfirmationButtonLabel => this[nameof (ShareTaxonomyBySiteConfirmationButtonLabel)];

    /// <summary>Gets External Link: Categories and tags</summary>
    [ResourceEntry("ExternalLinkCategoriesTags", Description = "External Link: Categories and tags", LastModified = "2018/10/22", Value = "https://docs.sitefinity.com/overview-classify-your-content-using-taxonomies")]
    public string ExternalLinkCategoriesTags => this[nameof (ExternalLinkCategoriesTags)];

    /// <summary>Gets External Link: Creating your own taxonomy</summary>
    [ResourceEntry("ExternalLinkCreatingOwnTaxonomy", Description = "External Link: Creating your own taxonomy", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/create-custom-classifications")]
    public string ExternalLinkCreatingOwnTaxonomy => this[nameof (ExternalLinkCreatingOwnTaxonomy)];

    /// <summary>
    /// Gets External Link: Working with the hierarchical taxomony in sitefinity
    /// </summary>
    [ResourceEntry("ExternalLinkWorkingWithHierarchicalTaxomony", Description = "External Link: Working with the hierarchical taxomony in sitefinity", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/add-categories-and-tags-to-content-items#add-categories")]
    public string ExternalLinkWorkingWithHierarchicalTaxomony => this[nameof (ExternalLinkWorkingWithHierarchicalTaxomony)];

    /// <summary>
    /// Gets External Link: Working with the flat taxomony in sitefinity
    /// </summary>
    [ResourceEntry("ExternalLinkWorkingWithFlatTaxomony", Description = "External Link: Working with the hierarchical flat in sitefinity", LastModified = "2018/10/22", Value = "https://www.progress.com/documentation/sitefinity-cms/add-categories-and-tags-to-content-items#add-tags")]
    public string ExternalLinkWorkingWithFlatTaxomony => this[nameof (ExternalLinkWorkingWithFlatTaxomony)];

    /// <summary>Gets External Link: Classify your content</summary>
    [ResourceEntry("ExternalLinkClassifyContent", Description = "External Link: Classify your content", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/overview-classify-your-content-using-taxonomies")]
    public string ExternalLinkClassifyContent => this[nameof (ExternalLinkClassifyContent)];

    /// <summary>
    /// Gets External Link: Add categories and tags to content items
    /// </summary>
    [ResourceEntry("ExternalLinkAddCategories", Description = "External Link: Add categories and tags to content items", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/add-categories-and-tags-to-content-items")]
    public string ExternalLinkAddCategories => this[nameof (ExternalLinkAddCategories)];

    /// <summary>Gets External Link: Create custom classifications</summary>
    [ResourceEntry("ExternalLinkCreateCustomClassifications", Description = "External Link: Create custom classifications", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/create-custom-classifications")]
    public string ExternalLinkCreateCustomClassifications => this[nameof (ExternalLinkCreateCustomClassifications)];

    /// <summary>Gets External Link: Set up and use tags</summary>
    [ResourceEntry("ExternalLinkSetupAndUseTags", Description = "External Link: Set up and use tags", LastModified = "2018/10/12", Value = "https://www.progress.com/documentation/sitefinity-cms/use-tags")]
    public string ExternalLinkSetupAndUseTags => this[nameof (ExternalLinkSetupAndUseTags)];

    /// <summary>Gets External Link Label: Classify your content</summary>
    [ResourceEntry("ExternalLinkLabelClassifyContent", Description = "External Link Label: Classify your content", LastModified = "2018/08/29", Value = "Classify your content")]
    public string ExternalLinkLabelClassifyContent => this[nameof (ExternalLinkLabelClassifyContent)];

    /// <summary>
    /// Gets External Link Label: Add categories and tags to content items
    /// </summary>
    [ResourceEntry("ExternalLinkLabelAddCategories", Description = "External Link Label: Add categories and tags to content items", LastModified = "2018/08/29", Value = "Add categories and tags to content items")]
    public string ExternalLinkLabelAddCategories => this[nameof (ExternalLinkLabelAddCategories)];

    /// <summary>
    /// Gets External Link Label: Create custom classifications
    /// </summary>
    [ResourceEntry("ExternalLinkLabelCreateCustomClassifications", Description = "External Link Label: Create custom classifications", LastModified = "2018/08/29", Value = "Create custom classifications")]
    public string ExternalLinkLabelCreateCustomClassifications => this[nameof (ExternalLinkLabelCreateCustomClassifications)];

    /// <summary>Gets External Link Label: Set up and use tags</summary>
    [ResourceEntry("ExternalLinkLabelSetupAndUseTags", Description = "External Link Label: Set up and use tags", LastModified = "2018/08/29", Value = "Set up and use tags")]
    public string ExternalLinkLabelSetupAndUseTags => this[nameof (ExternalLinkLabelSetupAndUseTags)];

    /// <summary>Label: Learn how to...</summary>
    [ResourceEntry("LearnHowTo", Description = "Label: Learn how to...", LastModified = "2018/08/29", Value = "Learn how to...")]
    public string LearnHowTo => this[nameof (LearnHowTo)];

    /// <summary>Move items to another tag</summary>
    [ResourceEntry("MoveItemsToAnotherTag", Description = "Move items to another tag", LastModified = "2019/08/29", Value = "Move items to another tag")]
    public string MoveItemsToAnotherTag => this[nameof (MoveItemsToAnotherTag)];

    /// <summary>Move items to another category</summary>
    [ResourceEntry("MoveItemsToAnotherCategory", Description = "Move items to another category", LastModified = "2019/08/29", Value = "Move items to another category")]
    public string MoveItemsToAnotherCategory => this[nameof (MoveItemsToAnotherCategory)];

    /// <summary>Bulk edit of properties</summary>
    [ResourceEntry("BulkEditProperties", Description = "Properties (bulk)", LastModified = "2020/02/07", Value = "Properties (bulk)")]
    public string BulkEditProperties => this[nameof (BulkEditProperties)];

    /// <summary>Create a child category</summary>
    [ResourceEntry("CreateChildCategory", Description = "Create a child category", LastModified = "2019/08/29", Value = "Create a child category")]
    public string CreateChildCategory => this[nameof (CreateChildCategory)];

    [ResourceEntry("TaxonomySingleItemNameTooltipText", Description = "The text contained in the tooltip for a classification`s properties 'single item name' input field.", LastModified = "2019/09/04", Value = "If the Classification title is Geographic regions, then the single item name would be Geographic region.")]
    public string TaxonomySingleItemNameTooltipText => this[nameof (TaxonomySingleItemNameTooltipText)];

    /// <summary>Example</summary>
    [ResourceEntry("TaxonomySingleItemNameTooltipTitle", Description = "The text contained in the title of the tooltip for a classification`s properties 'single item name' input field.", LastModified = "2019/09/04", Value = "Example")]
    public string TaxonomySingleItemNameTooltipTitle => this[nameof (TaxonomySingleItemNameTooltipTitle)];

    /// <summary>Label: Flat lists</summary>
    [ResourceEntry("FlatLists", Description = "Label: Flat lists", LastModified = "2019/11/27", Value = "Flat lists")]
    public string FlatLists => this[nameof (FlatLists)];

    /// <summary>Label: Hierarchical lists</summary>
    [ResourceEntry("HierarchicalLists", Description = "Label: Hierarchical lists", LastModified = "2019/11/27", Value = "Hierarchical lists")]
    public string HierarchicalLists => this[nameof (HierarchicalLists)];

    /// <summary>Label: Merge tags</summary>
    [ResourceEntry("MergeTags", Description = "Label: Merge tags", LastModified = "2019/12/11", Value = "Merge tags")]
    public string MergeTags => this[nameof (MergeTags)];
  }
}
