// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadSocialShare
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadSocialShareDescription", Name = "RadSocialShare", ResourceClassId = "RadSocialShare", Title = "RadSocialShareTitle", TitlePlural = "RadSocialShareTitlePlural")]
  public sealed class RadSocialShare : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadSocialShare" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadSocialShare()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadSocialShare" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadSocialShare(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadSocialShare</summary>
    [ResourceEntry("RadSocialShareTitle", Description = "The title of this class.", LastModified = "2020/10/01", Value = "RadSocialShare")]
    public string RadSocialShareTitle => this[nameof (RadSocialShareTitle)];

    /// <summary>RadSocialShare</summary>
    [ResourceEntry("RadSocialShareTitlePlural", Description = "The title plural of this class.", LastModified = "2020/10/01", Value = "RadSocialShare")]
    public string RadSocialShareTitlePlural => this[nameof (RadSocialShareTitlePlural)];

    /// <summary>Resource strings for RadSocialShare dialog.</summary>
    [ResourceEntry("RadSocialShareDescription", Description = "The description of this class.", LastModified = "2020/10/01", Value = "Resource strings for RadSocialShare.")]
    public string RadSocialShareDescription => this[nameof (RadSocialShareDescription)];

    [ResourceEntry("Blogger", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Blogger")]
    public string Blogger => this[nameof (Blogger)];

    [ResourceEntry("Delicious", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Delicious")]
    public string Delicious => this[nameof (Delicious)];

    [ResourceEntry("Digg", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Digg")]
    public string Digg => this[nameof (Digg)];

    [ResourceEntry("Facebook", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Facebook")]
    public string Facebook => this[nameof (Facebook)];

    [ResourceEntry("GoogleBookmarks", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Google Bookmarks")]
    public string GoogleBookmarks => this[nameof (GoogleBookmarks)];

    [ResourceEntry("GooglePlus", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Google Plus")]
    public string GooglePlus => this[nameof (GooglePlus)];

    [ResourceEntry("LinkedIn", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on LinkedIn")]
    public string LinkedIn => this[nameof (LinkedIn)];

    [ResourceEntry("MySpace", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on My Space")]
    public string MySpace => this[nameof (MySpace)];

    [ResourceEntry("Pinterest", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Pinterest")]
    public string Pinterest => this[nameof (Pinterest)];

    [ResourceEntry("Reddit", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Reddit")]
    public string Reddit => this[nameof (Reddit)];

    [ResourceEntry("StumbleUpon", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Stumble Upon")]
    public string StumbleUpon => this[nameof (StumbleUpon)];

    [ResourceEntry("Tumblr", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Tumblr")]
    public string Tumblr => this[nameof (Tumblr)];

    [ResourceEntry("Twitter", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Twitter")]
    public string Twitter => this[nameof (Twitter)];

    [ResourceEntry("Yammer", Description = "RadSocialShare resource strings.", LastModified = "2018/09/17", Value = "Share on Yammer")]
    public string Yammer => this[nameof (Yammer)];
  }
}
