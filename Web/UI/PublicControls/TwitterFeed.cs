// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeed
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.Designers;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  [ControlDesigner(typeof (TwitterFeedDesigner))]
  [PropertyEditorTitle(typeof (Labels), "TwitterFeed")]
  [Obsolete("Will be remove in future relases")]
  public class TwitterFeed : SimpleScriptView
  {
    private string width;
    private string height;
    private int tweetsNumber;
    private string shellBackgroundColor;
    private string shellTextColor;
    private string tweetBackgroundColor;
    private string tweetTextColor;
    private string linksColor;
    private string title = string.Empty;
    private string caption = string.Empty;
    private int tweetInterval;
    private string userName = "Sitefinity";
    private string searchQuery = "Sitefinity";
    private int feedTypeValue;
    private int tweetLoadBehaviorValue;
    private bool autoWidth;
    private bool autoColor = true;
    private string profileWidgetString = "<script type='text/javascript'>\r\n                                                $(window).load(function () {{\r\n                                                    $.getScript('http://widgets.twimg.com/j/2/widget.js', function () {{\r\n                                                        var {18} = new TWTR.Widget({{\r\n                                                          id: '{18}',\r\n                                                          version: 2,\r\n                                                          type: '{0}',\r\n                                                          rpp: {1},\r\n                                                          interval: {2},\r\n                                                          width: {3},\r\n                                                          height: {4},\r\n                                                          theme: {{\r\n                                                            shell: {{\r\n                                                              background: '{5}',\r\n                                                              color: '{6}'\r\n                                                            }},\r\n                                                            tweets: {{\r\n                                                              background: '{7}',\r\n                                                              color: '{8}',\r\n                                                              links: '{9}'\r\n                                                            }}\r\n                                                          }},\r\n                                                          features: {{\r\n                                                            scrollbar: {10},\r\n                                                            loop: {11},\r\n                                                            live: {12},\r\n                                                            hashtags: {15},\r\n                                                            timestamp: {16},\r\n                                                            avatars: {17},\r\n                                                            behavior: '{13}'\r\n\r\n                                                          }}\r\n                                                        }});\r\n                                                        {18}.render().setUser('{14}').start();  \r\n                                                    }})\r\n                                                }});\r\n                          </script>";
    private string searchWidgetString = "<script type='text/javascript'>\r\n                                                $(window).load(function () {{\r\n                                                    $.getScript('http://widgets.twimg.com/j/2/widget.js', function () {{\r\n                                                        var {20} = new TWTR.Widget({{\r\n                                                          id: '{20}',\r\n                                                          version: 2,\r\n                                                          type: '{0}',\r\n                                                          search:'{19}',\r\n                                                          rpp: {1},\r\n                                                          interval: {2},\r\n                                                          title: '{14}',\r\n                                                          subject: '{15}',\r\n                                                          width: {3},\r\n                                                          height: {4},\r\n                                                          theme: {{\r\n                                                            shell: {{\r\n                                                              background: '{5}',\r\n                                                              color: '{6}'\r\n                                                            }},\r\n                                                            tweets: {{\r\n                                                              background: '{7}',\r\n                                                              color: '{8}',\r\n                                                              links: '{9}'\r\n                                                            }}\r\n                                                          }},\r\n                                                          features: {{\r\n                                                            scrollbar: {10},\r\n                                                            loop: {11},\r\n                                                            live: {12},\r\n                                                            hashtags: {16},\r\n                                                            timestamp: {17},\r\n                                                            avatars: {18},\r\n                                                            behavior: '{13}'\r\n\r\n                                                          }}\r\n                                                        }});\r\n                                                        {20}.render().start();\r\n                                                    }})\r\n                                                }});\r\n                                            </script>";
    private string favesWidgetString = "<script type='text/javascript'>\r\n                                                $(window).load(function () {{\r\n                                                    $.getScript('http://widgets.twimg.com/j/2/widget.js', function () {{\r\n                                                        var {20} = new TWTR.Widget({{\r\n                                                          id: '{20}',\r\n                                                          version: 2,\r\n                                                          type: '{0}',\r\n                                                          rpp: {1},\r\n                                                          interval: {2},\r\n                                                          title: '{15}',\r\n                                                          subject: '{16}',\r\n                                                          width: {3},\r\n                                                          height: {4},\r\n                                                          theme: {{\r\n                                                            shell: {{\r\n                                                              background: '{5}',\r\n                                                              color: '{6}'\r\n                                                            }},\r\n                                                            tweets: {{\r\n                                                              background: '{7}',\r\n                                                              color: '{8}',\r\n                                                              links: '{9}'\r\n                                                            }}\r\n                                                          }},\r\n                                                          features: {{\r\n                                                            scrollbar: {10},\r\n                                                            loop: {11},\r\n                                                            live: {12},\r\n                                                            hashtags: {17},\r\n                                                            timestamp: {18},\r\n                                                            avatars: {19},\r\n                                                            behavior: '{13}'\r\n                                                          }}\r\n                                                        }});\r\n                                                        {20}.render().setUser('{14}').start();\r\n                                                    }})\r\n                                                }});\r\n                                            </script>";
    private string listWidgetString = "<script type='text/javascript'>\r\n                                                $(window).load(function () {{\r\n                                                    $.getScript('http://widgets.twimg.com/j/2/widget.js', function () {{\r\n                                                        var {20} = new TWTR.Widget({{\r\n                                                          id: '{20}',\r\n                                                          version: 2,\r\n                                                          type: '{0}',\r\n                                                          rpp: {1},\r\n                                                          interval: {2},\r\n                                                          title: '{15}',\r\n                                                          subject: '{16}',\r\n                                                          width: {3},\r\n                                                          height: {4},\r\n                                                          theme: {{\r\n                                                            shell: {{\r\n                                                              background: '{5}',\r\n                                                              color: '{6}'\r\n                                                            }},\r\n                                                            tweets: {{\r\n                                                              background: '{7}',\r\n                                                              color: '{8}',\r\n                                                              links: '{9}'\r\n                                                            }}\r\n                                                          }},\r\n                                                          features: {{\r\n                                                            scrollbar: {10},\r\n                                                            loop: {11},\r\n                                                            live: {12},\r\n                                                            hashtags: {17},\r\n                                                            timestamp: {18},\r\n                                                            avatars: {19},\r\n                                                            behavior: '{13}'\r\n                                                          }}\r\n                                                        }});\r\n                                                        {20}.render().setList('{14}', '{21}').start();\r\n                                                    }})\r\n                                                }});\r\n                                            </script>";
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.PublicControls.TwitterFeed.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.TwitterFeed.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.TwitterFeed" /> class.
    /// </summary>
    public TwitterFeed() => this.LayoutTemplatePath = TwitterFeed.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the wrapper tag of rendered control</summary>
    /// <value></value>
    /// <remarks>Override this property to change wrapper tag</remarks>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets or sets the username of the twitter account</summary>
    public string Username
    {
      get => this.userName.Replace("'", " \\'");
      set => this.userName = value;
    }

    /// <summary>Gets or sets the search query</summary>
    public string SearchQuery
    {
      get => this.searchQuery.Replace("'", " \\'");
      set => this.searchQuery = value;
    }

    /// <summary>Gets or sets the title</summary>
    public string Title
    {
      get
      {
        if (string.IsNullOrEmpty(this.title))
          this.title = string.Empty;
        return this.title.Replace("'", " \\'");
      }
      set => this.title = value;
    }

    /// <summary>Gets or sets the caption</summary>
    public string Caption
    {
      get
      {
        if (string.IsNullOrEmpty(this.caption))
          this.caption = string.Empty;
        return this.caption.Replace("'", " \\'");
      }
      set => this.caption = value;
    }

    /// <summary>Gets or sets the name of the tweet list</summary>
    public string ListName { get; set; }

    /// <summary>Gets or sets the type of the twitter feed widget</summary>
    public int FeedType
    {
      get
      {
        if (this.feedTypeValue == 0)
          this.feedTypeValue = 1;
        return this.feedTypeValue;
      }
      set => this.feedTypeValue = value;
    }

    /// <summary>
    /// Gets or sets whether the widget will automatically load all tweets
    /// </summary>
    public int TweetLoadBehavior
    {
      get
      {
        if (this.tweetLoadBehaviorValue == 0)
          this.tweetLoadBehaviorValue = 1;
        return this.tweetLoadBehaviorValue;
      }
      set => this.tweetLoadBehaviorValue = value;
    }

    /// <summary>
    /// Gets or sets whether the widget will automatically poll for new results
    /// </summary>
    public bool PollNewResults { get; set; }

    /// <summary>
    /// Gets or sets whether the widget will show scroll bar on the rignht side
    /// </summary>
    public bool IncludeScrollbar { get; set; }

    /// <summary>
    /// Gets or sets whether the widget will loop the tweeted results
    /// </summary>
    public bool LoopResults { get; set; }

    /// <summary>Gets or sets the interval for polling new tweets</summary>
    public int TweetInterval
    {
      get
      {
        if (this.tweetInterval == 0)
          this.tweetInterval = 30000;
        return this.tweetInterval;
      }
      set => this.tweetInterval = value;
    }

    /// <summary>Gets or sets the number of showed tweets</summary>
    public int TweetsNumber
    {
      get
      {
        if (this.tweetsNumber == 0)
          this.tweetsNumber = 4;
        return this.tweetsNumber;
      }
      set => this.tweetsNumber = value;
    }

    /// <summary>Gets or sets whether the avatars will be shown</summary>
    public bool ShowAvatars { get; set; }

    /// <summary>Gets or sets whether timestamps will be shown</summary>
    public bool ShowTimestamps { get; set; }

    /// <summary>Gets or sets whether hashtags will be shown</summary>
    public bool ShowHashtags { get; set; }

    /// <summary>
    /// Gets or sets the background color of the twitter widget's shell
    /// </summary>
    public string ShellBackgroundColor
    {
      get
      {
        if (string.IsNullOrEmpty(this.shellBackgroundColor))
          this.shellBackgroundColor = "#333333";
        return this.shellBackgroundColor.Replace("%23", "#");
      }
      set => this.shellBackgroundColor = value;
    }

    /// <summary>
    /// Gets or sets the text color of the twitter widget's shell
    /// </summary>
    public string ShellTextColor
    {
      get
      {
        if (string.IsNullOrEmpty(this.shellTextColor))
          this.shellTextColor = "#ffffff";
        return this.shellTextColor.Replace("%23", "#");
      }
      set => this.shellTextColor = value;
    }

    /// <summary>Gets or sets the background color of tweet area</summary>
    public string TweetBackgroundColor
    {
      get
      {
        if (string.IsNullOrEmpty(this.tweetBackgroundColor))
          this.tweetBackgroundColor = "#000000";
        return this.tweetBackgroundColor.Replace("%23", "#");
      }
      set => this.tweetBackgroundColor = value;
    }

    /// <summary>Gets or sets the text color of tweet text</summary>
    public string TweetTextColor
    {
      get
      {
        if (string.IsNullOrEmpty(this.tweetTextColor))
          this.tweetTextColor = "#ffffff";
        return this.tweetTextColor.Replace("%23", "#");
      }
      set => this.tweetTextColor = value;
    }

    /// <summary>Gets or sets the text color of tweet links</summary>
    public string LinksColor
    {
      get
      {
        if (string.IsNullOrEmpty(this.linksColor))
          this.linksColor = "#4aed05";
        return this.linksColor.Replace("%23", "#");
      }
      set => this.linksColor = value;
    }

    /// <summary>
    /// Gets or sets whether the widget dimentions will be calculated automatically or will be defined from the user
    /// </summary>
    public bool AutoWidth
    {
      get
      {
        if (this.Width == "250" && this.Height == "300")
          this.autoWidth = true;
        return this.autoWidth;
      }
      set => this.autoWidth = value;
    }

    /// <summary>
    /// Gets or sets whether the widget color scheme will be dafault or predefined
    /// </summary>
    public bool AutoColor
    {
      get => this.autoColor;
      set => this.autoColor = value;
    }

    /// <summary>Gets or sets the widget width</summary>
    public string Width
    {
      get
      {
        if (string.IsNullOrEmpty(this.width))
          this.width = "250";
        return this.width;
      }
      set => this.width = value;
    }

    /// <summary>Gets or sets the widget height</summary>
    public string Height
    {
      get
      {
        if (string.IsNullOrEmpty(this.height))
          this.height = "300";
        return this.height;
      }
      set => this.height = value;
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.IsDesignMode() && !this.IsPreviewMode())
      {
        this.Controls.Clear();
        this.Controls.Add((Control) new LiteralControl(Res.Get<Labels>().TwitterWidgetNotAvailableInEditMode));
      }
      else
      {
        string script = string.Empty;
        switch (this.FeedType)
        {
          case 1:
            script = this.GetProfileWidgetScript();
            break;
          case 2:
            script = this.GetSearchWidgetScript();
            break;
          case 3:
            script = this.GetFavesWidgetScript();
            break;
          case 4:
            script = this.GetListWidgetScript();
            break;
        }
        ScriptManager.RegisterStartupScript((Control) this, this.GetType(), this.ClientID + "TwitterWidgetScript", script, false);
      }
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) null;

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) PageManager.GetScriptReferences(ScriptRef.JQuery);

    /// <summary>
    /// Gets the required by the control, core library scripts predefined in the <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum.
    /// </summary>
    /// <example>
    /// // The defaults are:
    /// ScriptRef.MicrosoftAjax |
    /// ScriptRef.MicrosoftAjaxWebForms |
    /// ScriptRef.JQuery |
    /// ScriptRef.JQueryValidate |
    /// ScriptRef.JQueryCookie |
    /// ScriptRef.TelerikSitefinity |
    /// ScriptRef.QueryString;
    /// </example>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Modules.Pages.ScriptRef" /> enum value indicating the mix of library scripts that the control requires.</returns>
    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    private string GetProfileWidgetScript()
    {
      string str = this.AutoWidth ? "'auto'" : this.Width;
      return string.Format(this.profileWidgetString, (object) ((TwitterFeedType) this.FeedType).GetStringValue(), (object) this.TweetsNumber.ToString(), (object) this.TweetInterval, (object) str, (object) this.Height, (object) this.ShellBackgroundColor, (object) this.ShellTextColor, (object) this.TweetBackgroundColor, (object) this.TweetTextColor, (object) this.LinksColor, (object) this.IncludeScrollbar.ToString().ToLower(), (object) this.LoopResults.ToString().ToLower(), (object) this.PollNewResults.ToString().ToLower(), (object) ((LoadBehavior) this.TweetLoadBehavior).GetStringValue(), (object) this.Username, (object) this.ShowHashtags.ToString().ToLower(), (object) this.ShowTimestamps.ToString().ToLower(), (object) this.ShowAvatars.ToString().ToLower(), (object) this.ClientID);
    }

    private string GetSearchWidgetScript()
    {
      string str = this.AutoWidth ? "'auto'" : this.Width;
      return string.Format(this.searchWidgetString, (object) ((TwitterFeedType) this.FeedType).GetStringValue(), (object) this.TweetsNumber.ToString(), (object) this.TweetInterval, (object) str, (object) this.Height, (object) this.ShellBackgroundColor, (object) this.ShellTextColor, (object) this.TweetBackgroundColor, (object) this.TweetTextColor, (object) this.LinksColor, (object) this.IncludeScrollbar.ToString().ToLower(), (object) this.LoopResults.ToString().ToLower(), (object) this.PollNewResults.ToString().ToLower(), (object) ((LoadBehavior) this.TweetLoadBehavior).GetStringValue(), (object) this.Title, (object) this.Caption, (object) this.ShowHashtags.ToString().ToLower(), (object) this.ShowTimestamps.ToString().ToLower(), (object) this.ShowAvatars.ToString().ToLower(), (object) this.SearchQuery, (object) this.ClientID);
    }

    private string GetFavesWidgetScript()
    {
      string str = this.AutoWidth ? "'auto'" : this.Width;
      return string.Format(this.favesWidgetString, (object) ((TwitterFeedType) this.FeedType).GetStringValue(), (object) this.TweetsNumber.ToString(), (object) this.TweetInterval, (object) str, (object) this.Height, (object) this.ShellBackgroundColor, (object) this.ShellTextColor, (object) this.TweetBackgroundColor, (object) this.TweetTextColor, (object) this.LinksColor, (object) this.IncludeScrollbar.ToString().ToLower(), (object) this.LoopResults.ToString().ToLower(), (object) this.PollNewResults.ToString().ToLower(), (object) ((LoadBehavior) this.TweetLoadBehavior).GetStringValue(), (object) this.Username, (object) this.Title, (object) this.Caption, (object) this.ShowHashtags.ToString().ToLower(), (object) this.ShowTimestamps.ToString().ToLower(), (object) this.ShowAvatars.ToString().ToLower(), (object) this.ClientID);
    }

    private string GetListWidgetScript()
    {
      string str = this.AutoWidth ? "'auto'" : this.Width;
      return string.Format(this.listWidgetString, (object) ((TwitterFeedType) this.FeedType).GetStringValue(), (object) this.TweetsNumber.ToString(), (object) this.TweetInterval.ToString(), (object) str, (object) this.Height, (object) this.ShellBackgroundColor, (object) this.ShellTextColor, (object) this.TweetBackgroundColor, (object) this.TweetTextColor, (object) this.LinksColor, (object) this.IncludeScrollbar.ToString().ToLower(), (object) this.LoopResults.ToString().ToLower(), (object) this.PollNewResults.ToString().ToLower(), (object) ((LoadBehavior) this.TweetLoadBehavior).GetStringValue(), (object) this.Username, (object) this.Title, (object) this.Caption, (object) this.ShowHashtags.ToString().ToLower(), (object) this.ShowTimestamps.ToString().ToLower(), (object) this.ShowAvatars.ToString().ToLower(), (object) this.ClientID, (object) this.ListName);
    }
  }
}
