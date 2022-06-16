// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.ContentView.Plugins.ContentPluginsConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration.ContentView.Plugins
{
  /// <summary>
  /// Manages the configuration of the default Content plugins
  /// </summary>
  [DescriptionResource(typeof (ConfigDescriptions), "ContentPluginsConfig")]
  public class ContentPluginsConfig : ConfigSection
  {
    /// <summary>
    /// When a user votes and their vote is recored, they can not vote for a certain ammount of time
    /// to avoid cheating. Once this time expires, they will be able to vote again.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Rating_VoteTimeout")]
    [ConfigurationProperty("voteTimeout", IsRequired = true)]
    public TimeSpan VoteTimeout
    {
      get
      {
        object voteTimeout = this["voteTimeout"];
        if (voteTimeout != null)
          return (TimeSpan) voteTimeout;
        DateTime utcNow = DateTime.UtcNow;
        return utcNow.AddYears(1) - utcNow;
      }
      set => this["voteTimeout"] = (object) value;
    }

    /// <summary>
    /// Determines whether non-authenticated users can vote. In that case, IP-blacklist will be used
    /// instead of user-blacklist to prevent users from voting multiple times.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Rating_CanNonAuthenticatedUsersVote")]
    [ConfigurationProperty("canNonAuthenticatedUsersVote", DefaultValue = false, IsRequired = true)]
    public bool CanNonAuthenticatedUsersVote
    {
      get => (bool) this["canNonAuthenticatedUsersVote"];
      set => this["canNonAuthenticatedUsersVote"] = (object) value;
    }

    /// <summary>
    /// Determines whether users are allowed to vote only once (use user/ip blacklisting), or not.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Rating_CanUsersVoteOnlyOnce")]
    [ConfigurationProperty("canUsersVoteOnlyOnce", DefaultValue = true, IsRequired = true)]
    public bool CanUsersVoteOnlyOnce
    {
      get => (bool) this["canUsersVoteOnlyOnce"];
      set => this["canUsersVoteOnlyOnce"] = (object) value;
    }

    /// <summary>
    /// Determines whether only accepted comments will appear on the public side or not
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "CommentsList_ModerateComments")]
    [ConfigurationProperty("moderateComments", DefaultValue = false, IsRequired = true)]
    public bool ModerateComments
    {
      get => (bool) this["moderateComments"];
      set => this["moderateComments"] = (object) value;
    }

    /// <summary>
    /// Determines whether vote/rate blocking is performed by IP address or not
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "RatingPlugin_VoteBlockByIpAddress")]
    [ConfigurationProperty("voteBlockByIpAddress", DefaultValue = false, IsRequired = true)]
    public bool VoteBlockByIpAddress
    {
      get => (bool) this["voteBlockByIpAddress"];
      set => this["voteBlockByIpAddress"] = (object) value;
    }

    /// <summary>
    /// Determines whether vote/rate blocking is performed by user's ID or not
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "RatingPlugin_VoteBlockByUserId")]
    [ConfigurationProperty("voteBlockByUserId", DefaultValue = true, IsRequired = true)]
    public bool VoteBlockByUserId
    {
      get => (bool) this["voteBlockByUserId"];
      set => this["voteBlockByUserId"] = (object) value;
    }
  }
}
