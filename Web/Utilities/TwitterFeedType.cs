// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.TwitterFeedType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Utilities
{
  /// <summary>Enumerates the types of twitter feeds</summary>
  public enum TwitterFeedType
  {
    [StringValue("profile"), StringText("ProfileWidget")] ProfileWidget = 1,
    [StringValue("search"), StringText("SearchWidget")] SearchWidget = 2,
    [StringValue("faves"), StringText("FavesWidget")] FavesWidget = 3,
    [StringValue("list"), StringText("ListWidget")] ListWidget = 4,
  }
}
