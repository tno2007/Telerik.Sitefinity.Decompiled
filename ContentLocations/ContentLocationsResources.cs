// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationsResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("ContentLocationsResources", Description = "ContentLocationsResourcesDescription", ResourceClassId = "ContentLocationsResources", Title = "ContentLocationsResourcesTitle")]
  public sealed class ContentLocationsResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.ContentLocationsResources" /> class.
    /// </summary>
    public ContentLocationsResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.ContentLocationsResources" /> class.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    public ContentLocationsResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Content Locations</summary>
    [ResourceEntry("ContentLocationsResourcesTitle", Description = "The title of this class.", LastModified = "2013/03/21", Value = "Content Locations")]
    public string ContentLocationsResourcesTitle => this[nameof (ContentLocationsResourcesTitle)];

    /// <summary>
    /// Contains localizable resources for Content Locations user interface.
    /// </summary>
    [ResourceEntry("ContentLocationsResourcesDescription", Description = "The description of this class.", LastModified = "2013/03/21", Value = "Contains localizable resources for Content Locations user interface.")]
    public string ContentLocationsResourcesDescription => this[nameof (ContentLocationsResourcesDescription)];

    /// <summary>Title of the item locations list</summary>
    [ResourceEntry("ItemLocationsTitle", Description = "Title of the item locations list", LastModified = "2013/03/21", Value = "Pages")]
    public string ItemLocationsTitle => this[nameof (ItemLocationsTitle)];

    /// <summary>Description of the item locations list</summary>
    [ResourceEntry("ItemLocationsNote", Description = "Description of the item locations list", LastModified = "2013/03/21", Value = "where this item can be found")]
    public string ItemLocationsNote => this[nameof (ItemLocationsNote)];

    /// <summary>The text of the button to refresh locations list</summary>
    [ResourceEntry("RefreshPagesList", Description = "The text of the button to refresh locations list", LastModified = "2013/03/25", Value = "Refresh pages list")]
    public string RefreshPagesList => this[nameof (RefreshPagesList)];

    /// <summary>Pages</summary>
    [ResourceEntry("ContentLocationsGridPageHeader", Description = "Pages", LastModified = "2013/03/20", Value = "Pages")]
    public string ContentLocationsGridPageHeader => this[nameof (ContentLocationsGridPageHeader)];

    /// <summary>Site</summary>
    [ResourceEntry("ContentLocationsGridSiteHeader", Description = "Site", LastModified = "2013/03/20", Value = "Site")]
    public string ContentLocationsGridSiteHeader => this[nameof (ContentLocationsGridSiteHeader)];

    /// <summary>Priority</summary>
    [ResourceEntry("ContentLocationsGridPriorityHeader", Description = "Priority", LastModified = "2013/03/20", Value = "Priority")]
    public string ContentLocationsGridPriorityHeader => this[nameof (ContentLocationsGridPriorityHeader)];

    /// <summary>Actions</summary>
    [ResourceEntry("ContentLocationsGridActionsHeader", Description = "Actions", LastModified = "2013/03/20", Value = "Actions")]
    public string ContentLocationsGridActionsHeader => this[nameof (ContentLocationsGridActionsHeader)];

    /// <summary>Set priority...</summary>
    [ResourceEntry("ContentLocationsGridActionsTitleSetPriority", Description = "Set priority...", LastModified = "2013/03/22", Value = "Set priority...")]
    public string ContentLocationsGridActionsTitleSetPriority => this[nameof (ContentLocationsGridActionsTitleSetPriority)];

    /// <summary>Up</summary>
    [ResourceEntry("ContentLocationsGridActionUp", Description = "Up", LastModified = "2013/03/21", Value = "Up")]
    public string ContentLocationsGridActionUp => this[nameof (ContentLocationsGridActionUp)];

    /// <summary>Down</summary>
    [ResourceEntry("ContentLocationsGridActionDown", Description = "Down", LastModified = "2013/03/21", Value = "Down")]
    public string ContentLocationsGridActionDown => this[nameof (ContentLocationsGridActionDown)];

    /// <summary>Top</summary>
    [ResourceEntry("ContentLocationsGridActionTop", Description = "Top", LastModified = "2013/03/22", Value = "Top")]
    public string ContentLocationsGridActionTop => this[nameof (ContentLocationsGridActionTop)];

    /// <summary>Bottom</summary>
    [ResourceEntry("ContentLocationsGridActionBottom", Description = "Bottom", LastModified = "2013/03/22", Value = "Bottom")]
    public string ContentLocationsGridActionBottom => this[nameof (ContentLocationsGridActionBottom)];

    /// <summary>Content locations management</summary>
    [ResourceEntry("ContentLocationsManagementNodeTitle", Description = "Content locations management", LastModified = "2013/03/19", Value = "Content Locations")]
    public string ContentLocationsManagementNodeTitle => this[nameof (ContentLocationsManagementNodeTitle)];

    /// <summary>ContentLocations</summary>
    [ResourceEntry("ContentLocationsManagementUrlName", Description = "ContentLocations", LastModified = "2013/03/19", Value = "ContentLocations")]
    public string ContentLocationsManagementUrlName => this[nameof (ContentLocationsManagementUrlName)];

    /// <summary>
    /// You can manage the content locations priorities to identify the canonical url locations.
    /// </summary>
    [ResourceEntry("ContentLocationsDescription", Description = "Content locations management", LastModified = "2013/03/19", Value = "You can manage the content locations priorities to identify the canonical url locations.")]
    public string ContentLocationsDescription => this[nameof (ContentLocationsDescription)];

    /// <summary>Pages where {0} items are published</summary>
    [ResourceEntry("ContentLocationsManagementTitleTemplate", Description = "Pages where {0} items are published", LastModified = "2013/03/20", Value = "Pages where {0} items are published")]
    public string ContentLocationsManagementTitleTemplate => this[nameof (ContentLocationsManagementTitleTemplate)];

    /// <summary>Phrase:Back</summary>
    /// <value>Back</value>
    [ResourceEntry("Back", Description = "Phrase:Back", LastModified = "2013/04/01", Value = "Back")]
    public string Back => this[nameof (Back)];

    /// <summary>The message to show when the grid is empty.</summary>
    /// <value>Search returned no locations.</value>
    [ResourceEntry("NoLocationsForThisItem", Description = "The message to show when the grid is empty.", LastModified = "2013/04/18", Value = "No pages has been set to display content like this.")]
    public string NoLocationsForThisItem => this[nameof (NoLocationsForThisItem)];
  }
}
