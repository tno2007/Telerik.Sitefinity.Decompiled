// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Folders.FoldersResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Folders
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("FoldersResources", ResourceClassId = "FoldersResources", TitlePlural = "FoldersResourcesTitlePlural")]
  public sealed class FoldersResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Folders.FoldersResources" /> class with the default <see cref="!:FoldersDataProvider" />.
    /// </summary>
    public FoldersResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Folders.FoldersResources" /> class with the provided <see cref="!:FoldersDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public FoldersResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Title plural for the folders.</summary>
    /// <value>Title plural for the Multisite management module.</value>
    [ResourceEntry("FoldersResourcesTitlePlural", Description = "Title plural for the folders.", LastModified = "2013/02/20", Value = "Folders")]
    public string FoldersResourcesTitlePlural => this[nameof (FoldersResourcesTitlePlural)];
  }
}
