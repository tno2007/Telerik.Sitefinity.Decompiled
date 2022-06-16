// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.DynamicModuleResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.DynamicModules
{
  /// <summary>
  /// Localization class which holds localizable lables for the dynamic module.
  /// </summary>
  internal class DynamicModuleResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public DynamicModuleResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public DynamicModuleResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Title of the dynamic module module</summary>
    [ResourceEntry("ModuleTitle", Description = "Title of the dynamic module module", LastModified = "2011/10/28", Value = "Dynamic module")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Description of the dynamic module module</summary>
    [ResourceEntry("ModuleDescription", Description = "Description of the dynamic module module", LastModified = "2011/10/28", Value = "The dynamic module which can represent any kind of a module")]
    public string ModuleDescription => this[nameof (ModuleDescription)];

    /// <summary>word: Behavior</summary>
    [ResourceEntry("Behaviour", Description = "word: Behavior", LastModified = "2012/07/13", Value = "Behavior")]
    public string Behaviour => this[nameof (Behaviour)];

    /// <summary>word: Settings</summary>
    [ResourceEntry("Settings", Description = "word: Settings", LastModified = "2012/05/03", Value = "Settings")]
    public string Settings => this[nameof (Settings)];

    /// <summary>phrase: Full functionality</summary>
    [ResourceEntry("FullFunctionality", Description = "phrase: Full functionality", LastModified = "2012/05/03", Value = "Full functionality")]
    public string FullFunctionality => this[nameof (FullFunctionality)];

    /// <summary>phrase: List of {0} template</summary>
    [ResourceEntry("ListOfTemplate", Description = "phrase: List of {0} template", LastModified = "2012/05/10", Value = "List of {0} template")]
    public string ListOfTemplate => this[nameof (ListOfTemplate)];

    /// <summary>phrase: Single {0} template</summary>
    [ResourceEntry("SingleItemTemplate", Description = "phrase: Single {0} template", LastModified = "2012/05/10", Value = "Single {0} template")]
    public string SingleItemTemplate => this[nameof (SingleItemTemplate)];

    /// <summary>phrase: Paging, limit and sorting</summary>
    [ResourceEntry("PagingLimitSorting", Description = "phrase: Paging, limit and sorting", LastModified = "2012/05/14", Value = "Paging, limit and sorting")]
    public string PagingLimitSorting => this[nameof (PagingLimitSorting)];

    /// <summary>word: Templates</summary>
    [ResourceEntry("Templates", Description = "word: Templates", LastModified = "2012/05/14", Value = "Templates")]
    public string Templates => this[nameof (Templates)];

    /// <summary>phrase: Page for {0}</summary>
    [ResourceEntry("PageFor", Description = "phrase: Page for {0}", LastModified = "2012/06/28", Value = "Page for {0}")]
    public string PageFor => this[nameof (PageFor)];

    /// <summary>
    /// phrase: This is the page which is opened clicking the name of a {0}. There must be a {2} widget on this page set to display [Type3: plural] or Full functionality.
    /// </summary>
    [ResourceEntry("PagesSelectorDescription", Description = "phrase: This is the page which is opened clicking the name of a {0}. There must be a {2} widget on this page set to display {1} or Full functionality.", LastModified = "2012/06/28", Value = "This is the page which is opened clicking the name of a {0}. There must be a {2} widget on this page set to display {1} or Full functionality.")]
    public string PagesSelectorDescription => this[nameof (PagesSelectorDescription)];

    /// <summary>word: Display...</summary>
    [ResourceEntry("Display", Description = "word: Display...", LastModified = "2012/07/12", Value = "Display...")]
    public string Display => this[nameof (Display)];

    /// <summary>
    /// phrase: This widget is no longer available since the module providing its content is deleted  or deactivated.
    /// </summary>
    [ResourceEntry("DeletedModuleWarning", Description = "phrase:This widget is no longer available since the module providing its content is deleted or deactivated.", LastModified = "2012/01/18", Value = "This widget is no longer available since the module providing its content is deleted  or deactivated.")]
    public string DeletedModuleWarning => this[nameof (DeletedModuleWarning)];

    /// <summary>
    /// phrase: This widget can't display {0} any more because hierarchy of content types is changed.
    /// </summary>
    [ResourceEntry("ThisWidgetCannotDisplayThisType", Description = "phrase:This widget can't display {0} any more because hierarchy of content types is changed.", LastModified = "2012/01/18", Value = "This widget can't display {0} any more because hierarchy of content types is changed.")]
    public string ThisWidgetCannotDisplayThisType => this[nameof (ThisWidgetCannotDisplayThisType)];

    /// <summary>word: items</summary>
    [ResourceEntry("items", Description = "word: items", LastModified = "2012/01/18", Value = "items")]
    public string Items => this[nameof (Items)];

    /// <summary>phrase: Moved items</summary>
    [ResourceEntry("MovedItemsParentTitle", Description = "phrase: Moved items", LastModified = "2012/04/24", Value = "Moved items")]
    public string MovedItemsParentTitle => this[nameof (MovedItemsParentTitle)];

    /// <summary>
    /// Message shown when a non-admin user tries to delete a Item containing children.
    /// </summary>
    [ResourceEntry("PromptMessageItemCannotDeleteChildren", Description = "Message shown when a non-admin user tries to delete a item containing children.", LastModified = "2011/01/11", Value = "To delete this item you should move or delete its child items first.")]
    public string PromptMessageItemCannotDeleteChildren => this[nameof (PromptMessageItemCannotDeleteChildren)];

    /// <summary>
    /// Title of the message box shown when a non-admin user tries to delete a item containing children.
    /// </summary>
    [ResourceEntry("PromptTitleItemCannotDeleteChildren", Description = "Title of the message box shown when a non-admin user tries to delete a item containing children.", LastModified = "2011/01/11", Value = "You cannot delete a item that has child Items.")]
    public string PromptTitleItemCannotDeleteChildren => this[nameof (PromptTitleItemCannotDeleteChildren)];

    /// <summary>word: Edit</summary>
    /// <value>Edit</value>
    [ResourceEntry("ParentItemsEditLink", Description = "word: Edit", LastModified = "2014/06/13", Value = "Edit")]
    public string ParentItemsEditLink => this[nameof (ParentItemsEditLink)];

    /// <summary>
    /// This is the text the dynamic content view widget displays when in design mode and FilterByParentUrl is enabled
    /// </summary>
    /// <value>Displays {0} from the currently open {1}</value>
    [ResourceEntry("FilteredByParentText", Description = "This is the text the dynamic content view widget displays when in design mode and FilterByParentUrl is enabled", LastModified = "2014/07/03", Value = "Displays {0} from the currently open {1}")]
    public string FilteredByParentText => this[nameof (FilteredByParentText)];
  }
}
