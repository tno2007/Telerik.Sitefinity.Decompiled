// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.ConfigSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Modules.Libraries.Web
{
  /// <summary>Stores LibraryConfig settings used by LibraryRoute.</summary>
  internal class ConfigSettings
  {
    private static ConfigSettings current = new ConfigSettings();
    private ConcurrentProperty<ConfigSettings.MembersHolder> members = new ConcurrentProperty<ConfigSettings.MembersHolder>(new Func<ConfigSettings.MembersHolder>(ConfigSettings.BuildMembers));

    public static ConfigSettings Current => ConfigSettings.current;

    public ConfigSettings() => CacheDependency.Subscribe(typeof (LibrariesConfig), new ChangedCallback(this.ConfigChangedCallback));

    private static ConfigSettings.MembersHolder BuildMembers()
    {
      ConfigSettings.MembersHolder membersHolder = new ConfigSettings.MembersHolder();
      LibrariesConfig librariesConfig = Config.Get<LibrariesConfig>();
      membersHolder.imagesRoot = librariesConfig.Images.UrlRoot;
      membersHolder.videosRoot = librariesConfig.Videos.UrlRoot;
      membersHolder.documentsRoot = librariesConfig.Documents.UrlRoot;
      membersHolder.defaultRoot = librariesConfig.DefaultUrlRoot;
      membersHolder.thumbnailExtensionPrefix = librariesConfig.ThumbnailExtensionPrefix;
      membersHolder.additionalURLsToFiles = librariesConfig.MediaFileAdditionalUrls.AdditionalUrlsToFiles;
      return membersHolder;
    }

    private void ConfigChangedCallback(
      ICacheDependencyHandler caller,
      Type trackedItemType,
      string trackedItemKey)
    {
      this.members.Reset();
    }

    public string ImagesRoot => this.members.Value.imagesRoot;

    public string DocumentsRoot => this.members.Value.documentsRoot;

    public string VideosRoot => this.members.Value.videosRoot;

    public string DefaultRoot => this.members.Value.defaultRoot;

    public string ThumbnailExtensionPrefix => this.members.Value.thumbnailExtensionPrefix;

    public bool AdditionalURLsToFiles => this.members.Value.additionalURLsToFiles;

    private class MembersHolder
    {
      public string imagesRoot;
      public string documentsRoot;
      public string videosRoot;
      public string defaultRoot;
      public string thumbnailExtensionPrefix;
      public bool additionalURLsToFiles;
    }
  }
}
