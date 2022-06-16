// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Configuration.CommentsModuleConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Comments.Configuration
{
  /// <summary>
  /// Represents the configuration section for Comments module.
  /// </summary>
  internal class CommentsModuleConfig : ViewsModuleConfigBase
  {
    /// <summary>
    /// Default settings for comment threads. Used when the comments are not on any of the defined commentable types.
    /// </summary>
    [ConfigurationProperty("DefaultSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsDefaultSettingsDescription", Title = "CommentsDefaultSettingsCaption")]
    public CommentsSettingsElement DefaultSettings
    {
      get => (CommentsSettingsElement) this[nameof (DefaultSettings)];
      set => this[nameof (DefaultSettings)] = (object) value;
    }

    [ConfigurationProperty("Notifications")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsNotificationsDescription", Title = "CommentsNotificationsCaption")]
    public CommentsNotificationSettingsElement Notifications
    {
      get => (CommentsNotificationSettingsElement) this[nameof (Notifications)];
      set => this[nameof (Notifications)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use spam protection image.
    /// </summary>
    /// <value>
    /// When set to <c>true</c> spam protection image is used; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("useSpamProtectionImage", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseSpamProtectionImageDescription", Title = "UseSpamProtectionImageCaption")]
    public bool UseSpamProtectionImage
    {
      get => (bool) this["useSpamProtectionImage"];
      set => this["useSpamProtectionImage"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of types that should be commentable.
    /// </summary>
    [ConfigurationProperty("CommentableTypes")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentableTypesDescription", Title = "CommentableTypesCaption")]
    [ConfigurationCollection(typeof (CommentableTypeElement), AddItemName = "commentableType")]
    public ConfigElementDictionary<string, CommentableTypeElement> CommentableTypes => (ConfigElementDictionary<string, CommentableTypeElement>) this[nameof (CommentableTypes)];

    /// <summary>Gets whether comments will be displayed on pages.</summary>
    [ConfigurationProperty("EnablePaging", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsEnablePagingDescription", Title = "CommentsEnablePagingCaption")]
    public bool EnablePaging
    {
      get => (bool) this[nameof (EnablePaging)];
      set => this[nameof (EnablePaging)] = (object) value;
    }

    /// <summary>Gets how many comments will be displayed on a page.</summary>
    [ConfigurationProperty("CommentsPerPage", DefaultValue = 50)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsPerPageDescription", Title = "CommentsPerPageCaption")]
    public int CommentsPerPage
    {
      get => (int) this[nameof (CommentsPerPage)];
      set => this[nameof (CommentsPerPage)] = (object) value;
    }

    /// <summary>
    /// Gets whether the newest comments will be displayed on top. Otherwise the oldest will be on top.
    /// </summary>
    [ConfigurationProperty("AreNewestOnTop", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsAreNewestOnTopDescription", Title = "CommentsAreNewestOnTopCaption")]
    public bool AreNewestOnTop
    {
      get => (bool) this[nameof (AreNewestOnTop)];
      set => this[nameof (AreNewestOnTop)] = (object) value;
    }

    /// <summary>
    /// Gets whether the module should delete comments on deleted items.
    /// </summary>
    [ConfigurationProperty("DeleteAssociatedItemComments", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DeleteAssociatedItemCommentsDescription", Title = "DeleteAssociatedItemCommentsCaption")]
    public bool DeleteAssociatedItemComments
    {
      get => (bool) this[nameof (DeleteAssociatedItemComments)];
      set => this[nameof (DeleteAssociatedItemComments)] = (object) value;
    }

    /// <summary>
    /// Gets whether the newest comments will be displayed on top. Otherwise the oldest will be on top.
    /// </summary>
    [ConfigurationProperty("AlwaysUseUTC", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AlwaysUseUTCDescription", Title = "AlwaysUseUTCCaption")]
    public bool AlwaysUseUTC
    {
      get => (bool) this[nameof (AlwaysUseUTC)];
      set => this[nameof (AlwaysUseUTC)] = (object) value;
    }

    /// <summary>Initializes the default views.</summary>
    protected override void InitializeDefaultViews(
      ConfigElementDictionary<string, ViewContainerElement> viewControls)
    {
      viewControls.Add("CommentsModuleBackend", CommentsModuleDefinitions.DefineCommentsBackendView((ConfigElement) viewControls));
    }

    /// <summary>
    /// Called after the properties of this instance have been initialized.
    /// Load default values here.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.InitializeDefaultCommentableTypes();
    }

    private void InitializeDefaultCommentableTypes()
    {
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes1 = this.CommentableTypes;
      CommentableTypeElement element1 = new CommentableTypeElement((ConfigElement) this.CommentableTypes);
      element1.FriendlyName = "Pages";
      element1.ItemType = "Telerik.Sitefinity.Pages.Model.PageNode";
      element1.AllowComments = true;
      element1.RequiresApproval = false;
      element1.RequiresAuthentication = false;
      commentableTypes1.Add(element1);
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes2 = this.CommentableTypes;
      CommentableTypeElement element2 = new CommentableTypeElement((ConfigElement) this.CommentableTypes);
      element2.FriendlyName = "News";
      element2.ItemType = "Telerik.Sitefinity.News.Model.NewsItem";
      element2.AllowComments = true;
      element2.RequiresApproval = false;
      element2.RequiresAuthentication = false;
      commentableTypes2.Add(element2);
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes3 = this.CommentableTypes;
      CommentableTypeElement element3 = new CommentableTypeElement((ConfigElement) this.CommentableTypes);
      element3.FriendlyName = "Blogs";
      element3.ItemType = "Telerik.Sitefinity.Blogs.Model.BlogPost";
      element3.AllowComments = true;
      element3.RequiresApproval = false;
      element3.RequiresAuthentication = false;
      commentableTypes3.Add(element3);
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes4 = this.CommentableTypes;
      CommentableTypeElement element4 = new CommentableTypeElement((ConfigElement) this.CommentableTypes);
      element4.FriendlyName = "Events";
      element4.ItemType = "Telerik.Sitefinity.Events.Model.Event";
      element4.AllowComments = true;
      element4.RequiresApproval = false;
      element4.RequiresAuthentication = false;
      commentableTypes4.Add(element4);
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes5 = this.CommentableTypes;
      CommentableTypeElement element5 = new CommentableTypeElement((ConfigElement) this.CommentableTypes);
      element5.FriendlyName = "Images";
      element5.ItemType = "Telerik.Sitefinity.Libraries.Model.Image";
      element5.AllowComments = true;
      element5.RequiresApproval = false;
      element5.RequiresAuthentication = false;
      commentableTypes5.Add(element5);
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes6 = this.CommentableTypes;
      CommentableTypeElement element6 = new CommentableTypeElement((ConfigElement) this.CommentableTypes);
      element6.FriendlyName = "Videos";
      element6.ItemType = "Telerik.Sitefinity.Libraries.Model.Video";
      element6.AllowComments = true;
      element6.RequiresApproval = false;
      element6.RequiresAuthentication = false;
      commentableTypes6.Add(element6);
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes7 = this.CommentableTypes;
      CommentableTypeElement element7 = new CommentableTypeElement((ConfigElement) this.CommentableTypes);
      element7.FriendlyName = "Documents";
      element7.ItemType = "Telerik.Sitefinity.Libraries.Model.Document";
      element7.AllowComments = true;
      element7.RequiresApproval = false;
      element7.RequiresAuthentication = false;
      commentableTypes7.Add(element7);
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes8 = this.CommentableTypes;
      CommentableTypeElement element8 = new CommentableTypeElement((ConfigElement) this.CommentableTypes);
      element8.FriendlyName = "Lists";
      element8.ItemType = "Telerik.Sitefinity.Lists.Model.ListItem";
      element8.AllowComments = true;
      element8.RequiresApproval = false;
      element8.RequiresAuthentication = false;
      commentableTypes8.Add(element8);
    }

    internal static void MigrateCommentsSettings(
      CommentsSettings oldCommentsSettings,
      string itemTypeName,
      string frientlyName)
    {
      ConfigManager manager = Config.GetManager();
      CommentsModuleConfig section = manager.GetSection<CommentsModuleConfig>();
      ConfigElementDictionary<string, CommentableTypeElement> commentableTypes = section.CommentableTypes;
      int num1 = commentableTypes.Contains(itemTypeName) ? 1 : 0;
      CommentableTypeElement element = num1 == 0 || commentableTypes[itemTypeName] == null ? new CommentableTypeElement((ConfigElement) commentableTypes) : commentableTypes[itemTypeName];
      bool? nullable = oldCommentsSettings.AllowComments;
      if (nullable.HasValue)
      {
        CommentableTypeElement commentableTypeElement = element;
        nullable = oldCommentsSettings.AllowComments;
        int num2 = nullable.Value ? 1 : 0;
        commentableTypeElement.AllowComments = num2 != 0;
      }
      nullable = oldCommentsSettings.ApproveComments;
      if (nullable.HasValue)
      {
        CommentableTypeElement commentableTypeElement = element;
        nullable = oldCommentsSettings.ApproveComments;
        int num3 = nullable.Value ? 1 : 0;
        commentableTypeElement.RequiresApproval = num3 != 0;
      }
      if (oldCommentsSettings.PostRights == PostRights.RegisteredUsers)
        element.RequiresAuthentication = true;
      element.FriendlyName = frientlyName;
      element.ItemType = itemTypeName;
      if (num1 == 0)
        commentableTypes.Add(element);
      manager.SaveSection((ConfigSection) section);
    }

    internal static void MigrateCommentsGlobalSettings()
    {
      ConfigManager manager = Config.GetManager();
      CommentsModuleConfig section = manager.GetSection<CommentsModuleConfig>();
      GlobalCommentsSettings commentsSettings = manager.GetSection<CommentsConfig>().CommentsSettings;
      section.UseSpamProtectionImage = commentsSettings.UseSpamProtectionImage;
      manager.SaveSection((ConfigSection) section);
    }

    private static class ConfigProps
    {
      public const string DefaultSettings = "DefaultSettings";
      public const string CommentableTypes = "CommentableTypes";
      public const string EnablePaging = "EnablePaging";
      public const string CommentsPerPage = "CommentsPerPage";
      public const string AreNewestOnTop = "AreNewestOnTop";
      public const string DeleteAssociatedItemComments = "DeleteAssociatedItemComments";
      public const string Notifications = "Notifications";
      public const string UseSpamProtectionImage = "useSpamProtectionImage";
      public const string AlwaysUseUTC = "AlwaysUseUTC";
    }
  }
}
