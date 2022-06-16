// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.TwitterInboundPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Model.Publishing.Model.Twitter;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.UI.Designers;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Twitter Inbound Pipe</summary>
  [PipeDesigner(typeof (TwitterInboundPipeDesigner), null)]
  public class TwitterInboundPipe : TwitterPipeBase, IInboundPipe, IPushPipe
  {
    /// <summary>Pipe Name</summary>
    public const string PipeName = "TwitterInboundPipe";

    /// <summary>PipeName</summary>
    public override string Name => nameof (TwitterInboundPipe);

    /// <summary>Entry method for pushing data to Publishing Point</summary>
    public void ToPublishingPoint()
    {
      List<WrapperObject> wrapperObjectList = this.BuildPublishingPointItems(this.GetTweets(this.GetAutorizer()));
      this.FixTwitterItemTitels(wrapperObjectList);
      this.AddItemsToPublishingPoint(wrapperObjectList);
    }

    /// <summary>
    /// Create Unique titles for the twitter items if there are duplicates
    /// </summary>
    /// <param name="parsedTweets"></param>
    protected void FixTwitterItemTitels(List<WrapperObject> parsedTweets)
    {
      if (parsedTweets.Count <= 0)
        return;
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) parsedTweets.First<WrapperObject>());
      PropertyDescriptor propertyDescriptor = properties.Find("Title", true);
      foreach (WrapperObject parsedTweet in parsedTweets)
      {
        string title = parsedTweet.GetPropertyValue<string>(properties, "Title");
        List<WrapperObject> list = parsedTweets.Where<WrapperObject>((Func<WrapperObject, bool>) (item => item.GetPropertyValue<string>(properties, "Title") == title)).ToList<WrapperObject>();
        for (int index = 1; index < list.Count; ++index)
          propertyDescriptor.SetValue((object) list[index], (object) (title + " " + index.ToString()));
      }
    }

    /// <summary>
    /// Return false. The pipe can not process items. It is used only to push items to the PP
    /// </summary>
    /// <param name="item"> Item to process </param>
    /// <returns></returns>
    public override bool CanProcessItem(object item) => false;

    /// <summary>Save items to the Publishing Point</summary>
    /// <param name="items"></param>
    protected void AddItemsToPublishingPoint(List<WrapperObject> items)
    {
      List<WrapperObject> items1 = new List<WrapperObject>();
      foreach (WrapperObject wrapperObject in items)
        items1.Add(wrapperObject);
      List<WrapperObject> list = Queryable.OfType<DynamicTypeBase>(PublishingPointDynamicTypeManager.GetManager().GetDataItems((IPublishingPoint) this.PipeSettings.PublishingPoint).Where("PipeId.Contains(\"" + this.PipeSettings.Id.ToString() + "\")").OrderBy("PublicationDate")).Reverse<DynamicTypeBase>().Select<DynamicTypeBase, WrapperObject>((Expression<Func<DynamicTypeBase, WrapperObject>>) (item => new WrapperObject(item))).ToList<WrapperObject>();
      List<WrapperObject> items2 = new List<WrapperObject>();
      foreach (WrapperObject wrapperObject in list)
        items2.Add(wrapperObject);
      this.PublishingPoint.RemoveItems((IList<WrapperObject>) items2);
      this.PublishingPoint.AddItems((IList<WrapperObject>) items1);
    }

    /// <summary>Create Publishing point items from Tweets</summary>
    /// <param name="tweets"> tweets </param>
    /// <returns></returns>
    protected List<WrapperObject> BuildPublishingPointItems(List<Status> tweets)
    {
      List<WrapperObject> wrapperObjectList = new List<WrapperObject>();
      foreach (Status tweet in tweets)
      {
        WrapperObject wrapperObject = this.BuildPublishingPointItem(tweet);
        wrapperObjectList.Add(wrapperObject);
      }
      return wrapperObjectList;
    }

    /// <summary>Used to get the default mappings for this pipe</summary>
    /// <returns>The settings</returns>
    public static List<Mapping> GetDefaultMappings() => new List<Mapping>()
    {
      PublishingSystemFactory.CreateMapping("PublicationDate", "TransparentTranslator", true, "PublicationDate"),
      PublishingSystemFactory.CreateMapping("Content", "TransparentTranslator", true, "Link"),
      PublishingSystemFactory.CreateMapping("OwnerLastName", "TransparentTranslator", true, "OwnerLastName"),
      PublishingSystemFactory.CreateMapping("ItemHash", "TransparentTranslator", true, "ItemHash"),
      PublishingSystemFactory.CreateMapping("PipeId", "TransparentTranslator", true, "PipeId"),
      PublishingSystemFactory.CreateMapping("Link", "TransparentTranslator", true, "Link"),
      PublishingSystemFactory.CreateMapping("Username", "TransparentTranslator", true, "Username"),
      PublishingSystemFactory.CreateMapping("Owner", "TransparentTranslator", true, "Owner"),
      PublishingSystemFactory.CreateMapping("Title", "TransparentTranslator", true, "Title"),
      PublishingSystemFactory.CreateMapping("UserAvatar", "TransparentTranslator", true, "UserAvatar")
    };

    /// <summary>Convert a tweet to publishing point item</summary>
    /// <param name="tweet"> tweet </param>
    /// <returns></returns>
    protected WrapperObject BuildPublishingPointItem(Status tweet)
    {
      WrapperObject wrapperObject = new WrapperObject((object) null);
      wrapperObject.MappingSettings = this.PipeSettings.Mappings;
      wrapperObject.Language = this.PipeSettings.LanguageIds.FirstOrDefault<string>();
      wrapperObject.AddProperty("PublicationDate", (object) tweet.CreatedAt);
      wrapperObject.AddProperty("Content", (object) tweet.Text);
      wrapperObject.AddProperty("OwnerLastName", (object) tweet.User.Name);
      wrapperObject.AddProperty("ItemHash", (object) tweet.StatusID);
      wrapperObject.AddProperty("PipeId", (object) this.PipeSettings.Id);
      wrapperObject.AddProperty("Link", (object) string.Format("https://twitter.com/{0}/status/{1}", (object) tweet.User.Identifier.ID, (object) tweet.StatusID));
      wrapperObject.AddProperty("Username", (object) tweet.User.Identifier.ScreenName);
      wrapperObject.AddProperty("UserAvatar", (object) this.RemoveUriScheme(tweet.User.ProfileImageUrl));
      wrapperObject.AddProperty("Title", (object) string.Format("{0} - {1}", (object) tweet.User.Name, (object) tweet.CreatedAt.ToString("dd-MM-yyyy hh:mm")));
      return wrapperObject;
    }

    /// <summary>Load tweets from Twitter</summary>
    /// <param name="autorizer"> access token </param>
    /// <returns></returns>
    protected List<Status> GetTweets(SingleUserAuthorizer autorizer)
    {
      List<Status> tweets = new List<Status>();
      List<Status> source = new List<Status>();
      string searchPattern = this.TwitterPipeSettings.SearchPattern;
      try
      {
        Search search1;
        do
        {
          ulong maxTweetId = ulong.MaxValue;
          ulong minTweetId = 0;
          search1 = new TwitterContext((ITwitterAuthorizer) autorizer).Search.Where<Search>((Expression<Func<Search, bool>>) (search => (int) search.Type == 0 && search.Query == searchPattern && search.Count == this.PipeSettings.MaxItems + 1 && search.MaxID == maxTweetId && search.SinceID == minTweetId)).SingleOrDefault<Search>();
          if (search1 != null)
          {
            if (search1.Count > 0)
            {
              if (search1.Statuses.Any<Status>())
              {
                source.AddRange((IEnumerable<Status>) search1.Statuses);
                maxTweetId = search1.Statuses.Min<Status, ulong>((Func<Status, ulong>) (status => ulong.Parse(status.StatusID))) - 1UL;
                minTweetId = search1.Statuses.Max<Status, ulong>((Func<Status, ulong>) (status => ulong.Parse(status.StatusID))) + 1UL;
                if (source.Count >= this.PipeSettings.MaxItems)
                  break;
              }
              else
                break;
            }
            else
              break;
          }
          else
            break;
        }
        while (search1.Statuses.Count >= this.PipeSettings.MaxItems);
      }
      catch (Exception ex)
      {
      }
      tweets.AddRange(source.Take<Status>(this.PipeSettings.MaxItems));
      return tweets;
    }

    /// <summary>Call ToPublishing Point</summary>
    /// <param name="items"></param>
    public void PushData(IList<PublishingSystemEventInfo> items) => this.ToPublishingPoint();

    /// <summary>
    /// Used to get the default settings for this pipe, without creating persistent object
    /// </summary>
    /// <returns>The settings</returns>
    public static TwitterPipeSettings GetTemplatePipeSettings()
    {
      TwitterPipeSettings templatePipeSettings = new TwitterPipeSettings();
      templatePipeSettings.PipeName = nameof (TwitterInboundPipe);
      templatePipeSettings.IsInbound = true;
      templatePipeSettings.IsActive = true;
      templatePipeSettings.InvocationMode = PipeInvokationMode.Push;
      return templatePipeSettings;
    }

    private string RemoveUriScheme(string profileImageUrl) => !profileImageUrl.IsNullOrWhitespace() ? profileImageUrl.Replace("https://", "//").Replace("http://", "//") : profileImageUrl;
  }
}
