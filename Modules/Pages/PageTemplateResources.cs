// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageTemplateResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Represents the string resources for the backend navigation
  /// </summary>
  [ObjectInfo("PageTemplateResources", ResourceClassId = "PageTemplateResources")]
  public class PageTemplateResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.PageResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public PageTemplateResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.PageResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public PageTemplateResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Page Resources</summary>
    [ResourceEntry("PageTemplateResourcesTitle", Description = "The title of this class.", LastModified = "2011/04/26", Value = "Page Templates Resources")]
    public string PageTemplateResourcesTitle => this[nameof (PageTemplateResourcesTitle)];

    /// <summary>Page Resources</summary>
    [ResourceEntry("PageTemplateResourcesTitlePlural", Description = "The title plural of this class.", LastModified = "2011/04/26", Value = "Page Templates Resources")]
    public string PageTemplateResourcesTitlePlural => this[nameof (PageTemplateResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Sitefinity backend pages.
    /// </summary>
    [ResourceEntry("PageTemplateResourcesDescription", Description = "The description of this class.", LastModified = "2011/04/26", Value = "Contains localizable resources for Sitefinity page templates.")]
    public string PageTemplateResourcesDescription => this[nameof (PageTemplateResourcesDescription)];

    /// <summary>phrase: Manage Templates</summary>
    [ResourceEntry("ManageTemplates", Description = "phrase: Manage Templates", LastModified = "2011/04/20", Value = "Manage Templates")]
    public string ManageTemplates => this[nameof (ManageTemplates)];

    /// <summary>phrase: My Templates</summary>
    [ResourceEntry("MyTemplates", Description = "phrase: My Templates", LastModified = "2011/04/20", Value = "My Templates")]
    public string MyTemplates => this[nameof (MyTemplates)];

    /// <summary>phrase: Filter templates</summary>
    [ResourceEntry("FilterTemplates", Description = "phrase: Filter templates", LastModified = "2010/04/26", Value = "Filter templates")]
    public string FilterTemplates => this[nameof (FilterTemplates)];

    /// <summary>phrase: Sort</summary>
    [ResourceEntry("Sort", Description = "Label text.", LastModified = "2013/01/09", Value = "Sort")]
    public string Sort => this[nameof (Sort)];

    /// <summary>phrase: Last modified on top</summary>
    [ResourceEntry("NewModifiedFirst", Description = "Label text.", LastModified = "2013/01/10", Value = "Last modified on top")]
    public string NewModifiedFirst => this[nameof (NewModifiedFirst)];

    /// <summary>phrase: New-created first</summary>
    [ResourceEntry("NewCreatedFirst", Description = "Label text.", LastModified = "2013/01/10", Value = "Last created on top")]
    public string NewCreatedFirst => this[nameof (NewCreatedFirst)];

    /// <summary>phrase: By Title (A-Z)</summary>
    [ResourceEntry("ByTitleAsc", Description = "Label text.", LastModified = "2012/01/09", Value = "By Title (A-Z)")]
    public string ByTitleAsc => this[nameof (ByTitleAsc)];

    /// <summary>phrase: By Title (Z-A)</summary>
    [ResourceEntry("ByTitleDesc", Description = "Label text.", LastModified = "2012/01/09", Value = "By Title (Z-A)")]
    public string ByTitleDesc => this[nameof (ByTitleDesc)];

    /// <summary>Template thumbnail recommended image size</summary>
    /// <value>Recommended image size: 260x240px</value>
    [ResourceEntry("TemplateThumbnailRecommendedImageSize", Description = "Template thumbnail recommended image size", LastModified = "2019/03/08", Value = "Recommended image size: 260x240px")]
    public string TemplateThumbnailRecommendedImageSize => this[nameof (TemplateThumbnailRecommendedImageSize)];

    /// <summary>The template thumbnail field title</summary>
    /// <value>Template thumbnail</value>
    [ResourceEntry("TemplateThumbnailFieldTitle", Description = "The template thumbnail field title", LastModified = "2014/05/20", Value = "Template thumbnail")]
    public string TemplateThumbnailFieldTitle => this[nameof (TemplateThumbnailFieldTitle)];

    /// <summary>Word: Change</summary>
    /// <value>Change</value>
    [ResourceEntry("ChangeLabel", Description = "Word: Change", LastModified = "2014/06/25", Value = "Change")]
    public string ChangeLabel => this[nameof (ChangeLabel)];
  }
}
