// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Videos.VideosResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Libraries.Videos
{
  /// <summary>Represents string resources for Videos module UI.</summary>
  [ObjectInfo("VideosResources", ResourceClassId = "VideosResources")]
  public class VideosResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideosResources" /> class.
    /// </summary>
    public VideosResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideosResources" /> class.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    public VideosResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Messsage: type name Videos class</summary>
    [ResourceEntry("VideosPluralTypeName", Description = "Videos plural type name", LastModified = "2010/01/29", Value = "Videos")]
    public string VideosPluralTypeName => this[nameof (VideosPluralTypeName)];

    /// <summary>Messsage: type name Videos class</summary>
    [ResourceEntry("VideosSingleTypeName", Description = "Videos single type name", LastModified = "2010/01/29", Value = "Video")]
    public string VideosSingleTypeName => this[nameof (VideosSingleTypeName)];

    /// <summary>Messsage: type name Videos class</summary>
    [ResourceEntry("VideoLibraryPluralTypeName", Description = "Videos plural type name", LastModified = "2010/01/29", Value = "Video Libraries")]
    public string VideoLibraryPluralTypeName => this[nameof (VideoLibraryPluralTypeName)];

    /// <summary>Messsage: type name Videos class</summary>
    [ResourceEntry("VideoLibrarySingleTypeName", Description = "Videos single type name", LastModified = "2010/01/29", Value = "Video Library")]
    public string VideoLibrarySingleTypeName => this[nameof (VideoLibrarySingleTypeName)];

    /// <summary>Videos</summary>
    [ResourceEntry("VideosResourcesTitle", Description = "The title of this class.", LastModified = "2010/05/11", Value = "Videos")]
    public string VideosResourcesTitle => this[nameof (VideosResourcesTitle)];

    /// <summary>Videos</summary>
    [ResourceEntry("VideosResourcesTitlePlural", Description = "The title of this class in plural.", LastModified = "2010/05/11", Value = "Videos")]
    public string VideosResourcesTitlePlural => this[nameof (VideosResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Videos user interface.
    /// </summary>
    [ResourceEntry("VideosResourcesDescription", Description = "The description of this class.", LastModified = "2010/05/11", Value = "Contains localizable resources for Videos user interface.")]
    public string VideosResourcesDescription => this[nameof (VideosResourcesDescription)];

    /// <summary>More actions</summary>
    [ResourceEntry("MoreActions", Description = "The text of the more action menu in grid toolbar.", LastModified = "2010/10/25", Value = "More actions")]
    public string MoreActions => this[nameof (MoreActions)];

    /// <summary>Publish</summary>
    [ResourceEntry("Publish", Description = "Label of the publish action.", LastModified = "2010/07/29", Value = "Publish")]
    public string Publish => this[nameof (Publish)];

    /// <summary>word: Unpublish</summary>
    [ResourceEntry("Unpublish", Description = "word: Unpublish", LastModified = "2010/08/03", Value = "Unpublish")]
    public string Unpublish => this[nameof (Unpublish)];

    /// <summary>word: Manage Videos</summary>
    [ResourceEntry("ManageVideos", Description = "word: Manage Videos", LastModified = "2010/10/01", Value = "Manage Videos")]
    public string ManageVideos => this[nameof (ManageVideos)];

    /// <summary>Videos</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of the Videos module.", LastModified = "2010/05/11", Value = "Videos")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>word: Video</summary>
    [ResourceEntry("Video", Description = "The 'Video' string in singular.", LastModified = "2010/05/11", Value = "Video")]
    public string Video => this[nameof (Video)];

    /// <summary>word: a video</summary>
    [ResourceEntry("VideoWithArticle", Description = "The 'Video' string in singular with article in front of it.", LastModified = "2010/08/04", Value = "a video")]
    public string VideoWithArticle => this[nameof (VideoWithArticle)];

    /// <summary>word: videos</summary>
    [ResourceEntry("Videos", Description = "The 'videos' string in plural.", LastModified = "2010/05/11", Value = "videos")]
    public string Videos => this[nameof (Videos)];

    /// <summary>word: Videos</summary>
    [ResourceEntry("VideosCapitalized", Description = "The 'Videos' string in plural.", LastModified = "2010/09/27", Value = "Videos")]
    public string VideosCapitalized => this[nameof (VideosCapitalized)];

    /// <summary>phrase: Filter videos</summary>
    [ResourceEntry("FilterVideos", Description = "The 'Filter videos' label in the grid sidebar.", LastModified = "2010/05/11", Value = "Filter videos")]
    public string FilterVideos => this[nameof (FilterVideos)];

    /// <summary>Filter videos by status, tag, category, etc.</summary>
    [ResourceEntry("FilterVideosBy", Description = "The 'Filter videos' label in the sidebar.", LastModified = "2013/03/11", Value = "Filter videos<br/><span class='sfNote'>by status, tag, category, etc.</span>")]
    public string FilterVideosBy => this[nameof (FilterVideosBy)];

    /// <summary>phrase: Upload This Video</summary>
    [ResourceEntry("UploadThisVideo", Description = "Upload this video", LastModified = "2010/08/18", Value = "Upload this video")]
    public string UploadThisVideo => this[nameof (UploadThisVideo)];

    /// <summary>phrase: Filter videos in this library</summary>
    [ResourceEntry("FilterVideosInThisLibrary", Description = "The 'Filter videos in this library' label in the grid sidebar.", LastModified = "2010/05/11", Value = "Filter videos in this library")]
    public string FilterVideosInThisLibrary => this[nameof (FilterVideosInThisLibrary)];

    /// <summary>phrase: All videos</summary>
    [ResourceEntry("AllVideos", Description = "The 'All videos' link in the grid sidebar.", LastModified = "2010/05/11", Value = "All videos")]
    public string AllVideos => this[nameof (AllVideos)];

    /// <summary>phrase: My videos</summary>
    [ResourceEntry("MyVideos", Description = "The 'My videos' link in the grid sidebar.", LastModified = "2010/05/11", Value = "My videos")]
    public string MyVideos => this[nameof (MyVideos)];

    /// <summary>phrase: Videos by library</summary>
    [ResourceEntry("VideosByLibrary", Description = "The 'Videos by library' label in the grid sidebar.", LastModified = "2010/05/11", Value = "Videos by library")]
    public string VideosByLibrary => this[nameof (VideosByLibrary)];

    /// <summary>Phrase: Videos By Category</summary>
    [ResourceEntry("VideosByCategory", Description = "phrase: Videos by category", LastModified = "2010/07/22", Value = "Videos by category")]
    public string VideosByCategory => this[nameof (VideosByCategory)];

    /// <summary>Phrase: Videos By tag</summary>
    [ResourceEntry("VideosByTag", Description = "phrase: Videos By tag", LastModified = "2010/07/22", Value = "Videos By tag")]
    public string VideosByTag => this[nameof (VideosByTag)];

    /// <summary>phrase: Permissions for videos</summary>
    [ResourceEntry("PermissionsForVideos", Description = "The 'Permissions for Videos' label in the permissions dialog.", LastModified = "2010/07/26", Value = "Permissions for videos")]
    public string PermissionsForVideos => this[nameof (PermissionsForVideos)];

    /// <summary>phrase: Master videos</summary>
    [ResourceEntry("DraftVideos", Description = "The 'Draft videos' link in the permissions dialog.", LastModified = "2010/07/26", Value = "Draft")]
    public string DraftVideos => this[nameof (DraftVideos)];

    /// <summary>phrase: Published videos</summary>
    [ResourceEntry("PublishedVideos", Description = "The 'Published videos' link in the grid sidebar.", LastModified = "2010/07/26", Value = "Published")]
    public string PublishedVideos => this[nameof (PublishedVideos)];

    /// <summary>phrase: Scheduled videos</summary>
    [ResourceEntry("ScheduledVideos", Description = "The link for displaying scheduled videos in the sidebar.", LastModified = "2010/08/20", Value = "Scheduled")]
    public string ScheduledVideos => this[nameof (ScheduledVideos)];

    /// <summary>phrase: Settings for videos</summary>
    [ResourceEntry("SettingsForVideos", Description = "The 'Settings for videos' label in the grid sidebar.", LastModified = "2010/05/11", Value = "Settings for videos")]
    public string SettingsForVideos => this[nameof (SettingsForVideos)];

    /// <summary>Pages where Videos are published</summary>
    [ResourceEntry("PagesWhereVideosArePublished", Description = "Pages where Videos are published", LastModified = "2013/03/25", Value = "Pages where Videos are published")]
    public string PagesWhereVideosArePublished => this[nameof (PagesWhereVideosArePublished)];

    /// <summary>Phrase: Library name</summary>
    [ResourceEntry("LibraryName", Description = "The 'Library name' label.", LastModified = "2010/05/12", Value = "Library name")]
    public string LibraryName => this[nameof (LibraryName)];

    /// <summary>Example: Summer vacation</summary>
    [ResourceEntry("LibraryNameExample", Description = "The example how to fill library name field.", LastModified = "2010/07/26", Value = "<strong>Example:</strong> <em>Summer vacation</em>")]
    public string LibraryNameExample => this[nameof (LibraryNameExample)];

    /// <summary>Phrase: Library name cannot be empty</summary>
    [ResourceEntry("LibraryNameCannotBeEmpty", Description = "The message shown when the library name is empty.", LastModified = "2010/05/12", Value = "Library name cannot be empty")]
    public string LibraryNameCannotBeEmpty => this[nameof (LibraryNameCannotBeEmpty)];

    /// <summary>Word: Description</summary>
    [ResourceEntry("Description", Description = "The 'Description' label.", LastModified = "2010/05/12", Value = "Description")]
    public string Description => this[nameof (Description)];

    /// <summary>Phrase: The description of this library</summary>
    [ResourceEntry("LibraryDescription", Description = "The description of the Description field in the library form.", LastModified = "2010/07/26", Value = "LibraryDescription")]
    public string LibraryDescription => this[nameof (LibraryDescription)];

    /// <summary>Phrase: Create and go back</summary>
    [ResourceEntry("CreateThisLibrary", Description = "The 'Create and go back' button in the library form.", LastModified = "2013/03/27", Value = "Create and go back")]
    public string CreateThisLibrary => this[nameof (CreateThisLibrary)];

    /// <summary>Phrase: Save Changes</summary>
    [ResourceEntry("SaveChanges", Description = "The label for saving changes.", LastModified = "2010/05/12", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>Phrase: Create and add another library</summary>
    [ResourceEntry("CreateAndAddAnotherLibrary", Description = "The 'Create and add another library' button in the library form.", LastModified = "2010/05/12", Value = "Create and add another library")]
    public string CreateAndAddAnotherLibrary => this[nameof (CreateAndAddAnotherLibrary)];

    /// <summary>Phrase: Max library size</summary>
    [ResourceEntry("MaxLibrarySize", Description = "The title of the field for setting max library size.", LastModified = "2010/05/13", Value = "Max library size")]
    public string MaxLibrarySize => this[nameof (MaxLibrarySize)];

    /// <summary>Phrase: Max video size</summary>
    [ResourceEntry("MaxVideoSize", Description = "The title of the field for setting max video size.", LastModified = "2010/05/13", Value = "Max video size")]
    public string MaxVideoSize => this[nameof (MaxVideoSize)];

    /// <summary>The text of the embed video command.</summary>
    [ResourceEntry("EmbedVideo", Description = "The text of the embed video command.", LastModified = "2010/05/13", Value = "Embed")]
    public string EmbedVideo => this[nameof (EmbedVideo)];

    /// <summary>Phrase: Play original</summary>
    [ResourceEntry("PlayOriginal", Description = "The text of the 'Play' link in the action menu.", LastModified = "2010/05/13", Value = "Play")]
    public string PlayOriginal => this[nameof (PlayOriginal)];

    /// <summary>Phrase: No videos have been uploaded yet</summary>
    [ResourceEntry("NoItemsExist", Description = "The text of the message that user is presented with on the decision screen.", LastModified = "2010/05/14", Value = "No videos have been uploaded yet")]
    public string NoItemsExist => this[nameof (NoItemsExist)];

    /// <summary>Phrase: What do you want to do now?</summary>
    [ResourceEntry("WhatDoYouWantToDoNow", Description = "The title of the decision screen when there are no videos.", LastModified = "2010/05/14", Value = "What do you want to do now?")]
    public string WhatDoYouWantToDoNow => this[nameof (WhatDoYouWantToDoNow)];

    /// <summary>Phrase: Edit video</summary>
    [ResourceEntry("EditVideo", Description = "The title of the dialog for editing videos.", LastModified = "2010/05/14", Value = "Edit video")]
    public string EditVideo => this[nameof (EditVideo)];

    /// <summary>phrase: Change library</summary>
    [ResourceEntry("ChangeLibraryInSpan", Description = "phrase: Change library", LastModified = "2010/05/14", Value = "<span class='sfLinkBtnIn'>Change library</span>")]
    public string ChangeLibraryInSpan => this[nameof (ChangeLibraryInSpan)];

    /// <summary>phrase: Sort videos</summary>
    [ResourceEntry("SortVideos", Description = "phrase: Sort videos", LastModified = "2010/05/18", Value = "Sort videos")]
    public string SortVideos => this[nameof (SortVideos)];

    /// <summary>phrase: {0} more libraries</summary>
    [ResourceEntry("ShowMoreLibraries", Description = "phrase: {0} more libraries", LastModified = "2010/05/18", Value = "{0} more libraries")]
    public string ShowMoreLibraries => this[nameof (ShowMoreLibraries)];

    /// <summary>phrase: {0} less libraries</summary>
    [ResourceEntry("ShowLessLibraries", Description = "phrase: {0} less libraries", LastModified = "2010/05/18", Value = "{0} less libraries")]
    public string ShowLessLibraries => this[nameof (ShowLessLibraries)];

    /// <summary>Title / Duration / Status</summary>
    [ResourceEntry("TitleDurationStatus", Description = "The header of 'Title / Duration / Status' column in the grid.", LastModified = "2010/05/18", Value = "Title / Duration / Status")]
    public string TitleDurationStatus => this[nameof (TitleDurationStatus)];

    /// <summary>Categories</summary>
    [ResourceEntry("Categories", Description = "The header of 'Categories' column in the grid.", LastModified = "2020/08/31", Value = "Categories")]
    public string Categories => this[nameof (Categories)];

    /// <summary>Tags</summary>
    [ResourceEntry("Tags", Description = "The header of 'Tags' column in the grid.", LastModified = "2020/08/31", Value = "Tags")]
    public string Tags => this[nameof (Tags)];

    /// <summary>Library / Categories / Tags</summary>
    [ResourceEntry("LibraryCategoriesTags", Description = "The header of 'Library / Categories / Tags' column in the grid.", LastModified = "2010/05/18", Value = "Library / Categories / Tags")]
    public string LibraryCategoriesTags => this[nameof (LibraryCategoriesTags)];

    /// <summary>Embed this video</summary>
    [ResourceEntry("EmbedThisVideo", Description = "The text of the main action in the grid.", LastModified = "2010/03/18", Value = "Embed this video")]
    public string EmbedThisVideo => this[nameof (EmbedThisVideo)];

    /// <summary>
    /// Phrase: Resizing option (in process of implementation).
    /// </summary>
    [ResourceEntry("ResizeNotReady", Description = "Phrase: Resizing option (in process of implementation).", LastModified = "2010/04/09", Value = "<span class='sfNotReadYet'>Resizing option <em class='sfNote'>(in process of implementation)</em></span>")]
    public string ResizeNotReady => this[nameof (ResizeNotReady)];

    /// <summary>
    /// Phrase: Thumbnail settings (in process of implementation),
    /// </summary>
    [ResourceEntry("ThumbnailNotReady", Description = "Phrase: Thumbnail settings (in process of implementation).", LastModified = "2010/04/09", Value = "<span class='sfNotReadYet'>Thumbnail settings <em class='sfNote'>(in process of implementation)</em></span>")]
    public string ThumbnailNotReady => this[nameof (ThumbnailNotReady)];

    /// <summary>phrase: Thumbnail</summary>
    [ResourceEntry("Thumbnail", Description = "phrase: Thumbnail", LastModified = "2010/10/09", Value = "Thumbnail")]
    public string Thumbnail => this[nameof (Thumbnail)];

    /// <summary>phrase: Delete this video</summary>
    [ResourceEntry("DeleteThisItem", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/04/07", Value = "Delete")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>Permissions</summary>
    [ResourceEntry("Permissions", Description = "The link that navigates to permissions dialog in the sidebar.", LastModified = "2010/03/17", Value = "Permissions")]
    public string Permissions => this[nameof (Permissions)];

    /// <summary>word: Cancel</summary>
    [ResourceEntry("Cancel", Description = "The text of the cancel button.", LastModified = "2010/03/30", Value = "Cancel")]
    public string Cancel => this[nameof (Cancel)];

    /// <summary>phrase: ReviewHistory</summary>
    [ResourceEntry("ReviewHistory", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/07/26", Value = "Review history")]
    public string ReviewHistory => this[nameof (ReviewHistory)];

    /// <summary>phrase: Set permissions</summary>
    [ResourceEntry("SetPermissions", Description = "The text of the 'Set permissions' link in the action menu.", LastModified = "2010/03/22", Value = "Set Permissions")]
    public string SetPermissions => this[nameof (SetPermissions)];

    /// <summary>phrase: Reorder images</summary>
    [ResourceEntry("ReorderVideos", Description = "phrase: Reorder videos", LastModified = "2010/05/20", Value = "Reorder videos")]
    public string ReorderImages => this["ReorderVideos"];

    /// <summary>Phrase: Last uploaded on</summary>
    [ResourceEntry("LastUploaded", Description = "'Last uploaded on' label in the libraries grid.", LastModified = "2013/03/06", Value = "Last uploaded on")]
    public string LastUploaded => this[nameof (LastUploaded)];

    /// <summary>Phrase: Last uploaded</summary>
    [ResourceEntry("LastUploadedLabel", Description = "'Last uploaded' label in the libraries grid.", LastModified = "2020/08/31", Value = "Last uploaded")]
    public string LastUploadedLabel => this[nameof (LastUploadedLabel)];

    /// <summary>Phrase: Uploaded</summary>
    [ResourceEntry("Uploaded", Description = "'Uploaded' label in the libraries grid.", LastModified = "2020/08/31", Value = "Uploaded")]
    public string Uploaded => this[nameof (Uploaded)];

    /// <summary>Phrase: File / Size</summary>
    [ResourceEntry("FileSize", Description = "'File / Size' label in the libraries grid.", LastModified = "2020/08/31", Value = "File / Size")]
    public string FileSize => this[nameof (FileSize)];

    /// <summary>Phrase: Upload videos</summary>
    [ResourceEntry("UploadVideosLabel", Description = "phrase: Upload videos", LastModified = "2010/12/20", Value = "Upload videos")]
    public string UploadVideosLabel => this[nameof (UploadVideosLabel)];

    /// <summary>Library</summary>
    [ResourceEntry("Library", Description = "word: Library", LastModified = "2010/05/26", Value = "Library")]
    public string Library => this[nameof (Library)];

    /// <summary>
    /// phrase: <i>{0}</i>
    /// </summary>
    [ResourceEntry("LibraryVideosTitleFormat", Description = "phrase: <i>{0}</i>", LastModified = "2011/02/17", Value = "<i>{0}</i>")]
    public string LibraryVideosTitleFormat => this[nameof (LibraryVideosTitleFormat)];

    /// <summary>Common library</summary>
    [ResourceEntry("CommonLibrary", Description = "phrase: Common library", LastModified = "2010/06/18", Value = "Common library")]
    public string CommonLibrary => this[nameof (CommonLibrary)];

    /// <summary>Libraries</summary>
    [ResourceEntry("Libraries", Description = "word: Libraries", LastModified = "2010/05/26", Value = "Libraries")]
    public string Libraries => this[nameof (Libraries)];

    /// <summary>Message: View all videos</summary>
    /// <value>Text of the link that will show the view that lists all videos.</value>
    [ResourceEntry("BackToVideos", Description = "Text of the link that will show the view that lists all videos.", LastModified = "2010/05/26", Value = "Back to Videos")]
    public string BackToVideos => this[nameof (BackToVideos)];

    /// <summary>Message: Back to Videos</summary>
    /// <value>The back to all items button</value>
    [ResourceEntry("BackToItems", Description = "The back to all items button", LastModified = "2010/10/16", Value = "Back to Videos")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>Phrase: Create a library</summary>
    [ResourceEntry("CreateLibrary", Description = "The label for creating a library.", LastModified = "2010/08/16", Value = "Create a library")]
    public string CreateLibrary => this[nameof (CreateLibrary)];

    /// <summary>Phrase: Merge libraries</summary>
    [ResourceEntry("MergeLibraries", Description = "The label for merging libraries.", LastModified = "2010/05/26", Value = "Merge libraries")]
    public string MergeLibraries => this[nameof (MergeLibraries)];

    /// <summary>Phrase: Upload videos</summary>
    [ResourceEntry("UploadVideos", Description = "The label for uploading videos.", LastModified = "2010/05/26", Value = "Upload videos")]
    public string UploadVideos => this[nameof (UploadVideos)];

    /// <summary>Phrase: Back to all libraries</summary>
    [ResourceEntry("BackToAllLibraries", Description = "The label for link to return to all libraries.", LastModified = "2010/05/26", Value = "Back to all libraries")]
    public string BackToAllLibraries => this[nameof (BackToAllLibraries)];

    /// <summary>phrase: Which videos to display?</summary>
    [ResourceEntry("WhichVideosToDisplay", Description = "phrase: Which videos to display?", LastModified = "2010/06/08", Value = "Which videos to display?")]
    public string WhichVideosToDisplay => this[nameof (WhichVideosToDisplay)];

    /// <summary>phrase: Choose Library</summary>
    [ResourceEntry("ChooseLibrary", Description = "phrase: Choose Library", LastModified = "2010/06/08", Value = "Choose Library")]
    public string ChooseLibrary => this[nameof (ChooseLibrary)];

    /// <summary>phrase: All published videos</summary>
    [ResourceEntry("AllPublishedVideos", Description = "phrase: All published videos", LastModified = "2010/06/08", Value = "All published videos")]
    public string AllPublishedVideos => this[nameof (AllPublishedVideos)];

    /// <summary>phrase: From selected library...</summary>
    [ResourceEntry("FromSelectedLibrary", Description = "phrase: From selected library...", LastModified = "2010/06/08", Value = "From selected library...")]
    public string FromSelectedLibrary => this[nameof (FromSelectedLibrary)];

    /// <summary>phrase: Upload new videos...</summary>
    [ResourceEntry("UploadNewVideos", Description = "phrase: Upload new videos...", LastModified = "2010/06/08", Value = "Upload new videos...")]
    public string UploadNewVideos => this[nameof (UploadNewVideos)];

    /// <summary>phrase: Select video gallery type</summary>
    [ResourceEntry("SelectVideoGalleryType", Description = "phrase: Select video gallery type", LastModified = "2010/06/08", Value = "Select video gallery type")]
    public string SelectVideoGalleryType => this[nameof (SelectVideoGalleryType)];

    /// <summary>phrase: Show option for embedding</summary>
    [ResourceEntry("ShowOptionForEmbedding", Description = "phrase: Show option for embedding", LastModified = "2010/06/09", Value = "Show option for embedding")]
    public string ShowOptionForEmbedding => this[nameof (ShowOptionForEmbedding)];

    /// <summary>phrase: Show related videos</summary>
    [ResourceEntry("ShowRelatedVideos", Description = "phrase: Show related videos", LastModified = "2010/06/09", Value = "Show related videos")]
    public string ShowRelatedVideos => this[nameof (ShowRelatedVideos)];

    /// <summary>phrase: Allow full size</summary>
    [ResourceEntry("AllowFullSize", Description = "phrase: Allow full size", LastModified = "2010/06/09", Value = "Allow full size")]
    public string AllowFullSize => this[nameof (AllowFullSize)];

    /// <summary>phrase: From already uploaded</summary>
    [ResourceEntry("FromAlreadyUploadedVideos", Description = "phrase: From already uploaded", LastModified = "2010/06/17", Value = "From already uploaded")]
    public string FromAlreadyUploadedVideos => this[nameof (FromAlreadyUploadedVideos)];

    /// <summary>phrase: Where to store the uploaded video?</summary>
    [ResourceEntry("WhereToStoreTheUploadedVideo", Description = "phrase: Where to store the uploaded video?", LastModified = "2010/06/26", Value = "Where to store the uploaded video?")]
    public string WhereToStoreTheUploadedVideo => this[nameof (WhereToStoreTheUploadedVideo)];

    /// <summary>phrase: Which video to upload?</summary>
    [ResourceEntry("WhichVideoToUpload", Description = "phrase: Which video to upload?", LastModified = "2010/06/17", Value = "Which video to upload?")]
    public string WhichVideoToUpload => this[nameof (WhichVideoToUpload)];

    /// <summary>Phrase: Close date</summary>
    [ResourceEntry("CloseDateFilter", Description = "The link for closing the date filter widget in the sidebar.", LastModified = "2010/06/02", Value = "Close dates")]
    public string CloseDateFilter => this[nameof (CloseDateFilter)];

    /// <summary>Phrase: Videos By Date</summary>
    [ResourceEntry("VideosByDate", Description = "phrase: Videos By Date", LastModified = "2010/06/02", Value = "Display Videos modified in...")]
    public string ImagesByDate => this["VideosByDate"];

    /// <summary>Upload videos button</summary>
    [ResourceEntry("UploadVideosButton", Description = "The text of the upload button in grid toolbar.", LastModified = "2010/08/25", Value = "Upload videos")]
    public string UploadVideosButton => this[nameof (UploadVideosButton)];

    /// <summary>phrase: by Date...</summary>
    [ResourceEntry("ByDate", Description = "phrase: by Date...", LastModified = "2010/07/26", Value = "by Date...")]
    public string ByDate => this[nameof (ByDate)];

    /// <summary>phrase: Customize embedded video</summary>
    [ResourceEntry("CustomizeEmbeddedVideo", Description = "phrase: Customize embedded video", LastModified = "2012/01/05", Value = "Customize embedded video")]
    public string CustomizeEmbeddedVideo => this[nameof (CustomizeEmbeddedVideo)];

    /// <summary>The title of the edit item dialog</summary>
    [ResourceEntry("EditItem", Description = "The title of the edit item dialog", LastModified = "2010/08/11", Value = "Edit a video")]
    public string EditItem => this[nameof (EditItem)];

    /// <summary>The title of the create video dialog</summary>
    [ResourceEntry("CreateItem", Description = "The title of the create video dialog", LastModified = "2010/11/23", Value = "Create a video")]
    public string CreateItem => this[nameof (CreateItem)];

    /// <summary>The title of the view item dialog</summary>
    /// <value>Preview video</value>
    [ResourceEntry("ViewItem", Description = "The title of the view item dialog", LastModified = "2014/04/09", Value = "Preview video")]
    public string ViewItem => this[nameof (ViewItem)];

    /// <summary>phrase: Edit Video gallery settings</summary>
    [ResourceEntry("EditVideoGallerySettings", Description = "phrase: Edit Video gallery settings", LastModified = "2010/11/13", Value = "Edit Video gallery settings")]
    public string EditVideoGallerySettings => this[nameof (EditVideoGallerySettings)];

    /// <summary>phrase: Select a video</summary>
    [ResourceEntry("SelectVideo", Description = " phrase: Select a video", LastModified = "2010/11/12", Value = "Select a video")]
    public string SelectVideo => this[nameof (SelectVideo)];

    /// <summary>word: Delete</summary>
    [ResourceEntry("Delete", Description = "The text of the delete button.", LastModified = "2010/08/11", Value = "Delete")]
    public string Delete => this[nameof (Delete)];

    /// <summary>word: video</summary>
    [ResourceEntry("VideoSingularItemName", Description = "word: video", LastModified = "2010/10/11", Value = "video")]
    public string VideoSingularItemName => this[nameof (VideoSingularItemName)];

    /// <summary>word: videos</summary>
    [ResourceEntry("VideoPluralItemName", Description = "word: videos", LastModified = "2010/10/11", Value = "videos")]
    public string VideoPluralItemName => this[nameof (VideoPluralItemName)];

    /// <summary>phrase: Videos data fields</summary>
    [ResourceEntry("VideosDataFields", Description = "phrase: Videos data fields", LastModified = "2010/11/29", Value = "Videos data fields")]
    public string VideosDataFields => this[nameof (VideosDataFields)];

    /// <summary>Phrase: Create and go to upload videos</summary>
    [ResourceEntry("CreateAndGoToUploadVideos", Description = "phrase: Create and go to upload videos.", LastModified = "2011/01/24", Value = "Create and go to upload videos")]
    public string CreateAndGoToUploadVideos => this[nameof (CreateAndGoToUploadVideos)];

    /// <summary>Phrase: Manage Video Libraries</summary>
    [ResourceEntry("ManageVideoLibraries", Description = "The text of 'Manage Video Libraries' link.", LastModified = "2010/07/19", Value = "Manage Video Libraries")]
    public string ManageVideoLibraries => this[nameof (ManageVideoLibraries)];

    /// <summary>Phrase: Capture video frame</summary>
    /// <value>Capture video frame</value>
    [ResourceEntry("CaptureAnother", Description = "Phrase: Capture video frame", LastModified = "2014/03/27", Value = "Capture video frame")]
    public string CaptureAnother => this[nameof (CaptureAnother)];

    /// <summary>Phrase: Upload custom image</summary>
    /// <value>Upload custom image</value>
    [ResourceEntry("UploadCustom", Description = "Phrase: Upload custom image", LastModified = "2014/03/27", Value = "Upload custom image")]
    public string UploadCustom => this[nameof (UploadCustom)];

    /// <summary>
    /// Message shown when trying to capture a thumbnail of a video which cannot be played inside the HTML5 player of the current browser.
    /// </summary>
    /// <value>Capturing thumbnails is not supported by current version of your browser. Try newer version or a different browser.</value>
    [ResourceEntry("UnableToGenerateThumbnail", Description = "Message shown when trying to capture a thumbnail of a video which cannot be played inside the HTML5 player of the current browser.", LastModified = "2014/03/31", Value = "Capturing thumbnails is not supported by current version of your browser. Try newer version or a different browser.")]
    public string UnableToGenerateThumbnail => this[nameof (UnableToGenerateThumbnail)];

    /// <summary>Phrase: Capture thumbnail</summary>
    /// <value>Capture thumbnail</value>
    [ResourceEntry("CaptureThumbnail", Description = "Phrase: Capture thumbnail", LastModified = "2014/03/31", Value = "Capture thumbnail")]
    public string CaptureThumbnail => this[nameof (CaptureThumbnail)];

    /// <summary>The title of the dialog for upload a video.</summary>
    /// <value>'Upload video' in the Backend</value>
    [ResourceEntry("UploadVideo", Description = "The title of the dialog for upload a video.", LastModified = "2014/04/07", Value = "'Upload video' in the Backend")]
    public string UploadVideo => this[nameof (UploadVideo)];

    /// <summary>Phrase: Revision history</summary>
    /// <value>Revision history</value>
    [ResourceEntry("VersionPreviewVideo", Description = "Phrase: Revision history", LastModified = "2014/04/09", Value = "Revision history")]
    public string VersionPreviewVideo => this[nameof (VersionPreviewVideo)];

    /// <summary>Phrase: Select a video</summary>
    [ResourceEntry("MediaPlayerControlPropertyEditorTitle", Description = "The title of the designer of the MediaPlayerControl", LastModified = "2013/02/25", Value = "Select a video")]
    public string MediaPlayerControlPropertyEditorTitle => this[nameof (MediaPlayerControlPropertyEditorTitle)];

    /// <summary>
    /// phrase: A video was not selected or has been deleted. Please select another one.
    /// </summary>
    [ResourceEntry("VideoWasNotSelectedOrHasBeenDeleted", Description = "phrase: A video was not selected or has been deleted. Please select another one.", LastModified = "2010/07/20", Value = "A video was not selected or has been deleted. Please select another one.")]
    public string VideoWasNotSelectedOrHasBeenDeleted => this[nameof (VideoWasNotSelectedOrHasBeenDeleted)];

    /// <summary>
    /// phrase: If you add a video here it will not be displayed before the page is published.To see the video click Preview on the top of this page.
    /// </summary>
    [ResourceEntry("VideoNotAvailableInEditMode", Description = "phrase: If you add a video here it will not be displayed before the page is published.To see the video click Preview on the top of this page.", LastModified = "2010/08/05", Value = "<p class=\"sfVideoNotAvailable\">If you add a video here it will not be displayed before the page is published.To see the video click Preview on the top of this page.</p>")]
    public string VideoNotAvailableInEditMode => this[nameof (VideoNotAvailableInEditMode)];

    /// <summary>Phrase: Failed to open media!</summary>
    /// <value>Failed to open media!</value>
    [ResourceEntry("UnableToPlayVideo", Description = "Phrase: This video format is not supported", LastModified = "2014/03/25", Value = "This video format is not supported")]
    public string UnableToPlayVideo => this[nameof (UnableToPlayVideo)];

    /// <summary>phrase: Last modified on top</summary>
    [ResourceEntry("NewModifiedFirst", Description = "Label text.", LastModified = "2010/08/16", Value = "Last modified on top")]
    public string NewModifiedFirst => this[nameof (NewModifiedFirst)];

    /// <summary>phrase: Last uploaded on top</summary>
    [ResourceEntry("NewUploadedFirst", Description = "Label text.", LastModified = "2010/08/16", Value = "Last uploaded on top")]
    public string NewUploadedFirst => this[nameof (NewUploadedFirst)];

    /// <summary>phrase: by Hierarchy</summary>
    [ResourceEntry("ByHierarchy", Description = "Label text.", LastModified = "2010/04/06", Value = "by Hierarchy")]
    public string ByHierarchy => this[nameof (ByHierarchy)];

    /// <summary>phrase: by Status</summary>
    [ResourceEntry("ByStatus", Description = "Label text.", LastModified = "2010/04/06", Value = "by Status")]
    public string ByStatus => this[nameof (ByStatus)];

    /// <summary>phrase: by Template</summary>
    [ResourceEntry("SortByTemplate", Description = "Label text.", LastModified = "2010/04/06", Value = "by Template")]
    public string SortByTemplate => this[nameof (SortByTemplate)];

    /// <summary>phrase: Alphabetically (A-Z)</summary>
    [ResourceEntry("AlphabeticallyAsc", Description = "Label text.", LastModified = "2010/04/06", Value = "Alphabetically (A-Z)")]
    public string AlphabeticallyAsc => this[nameof (AlphabeticallyAsc)];

    /// <summary>phrase: Alphabetically (Z-A)</summary>
    [ResourceEntry("AlphabeticallyDesc", Description = "Label text.", LastModified = "2010/04/06", Value = "Alphabetically (Z-A)")]
    public string AlphabeticallyDesc => this[nameof (AlphabeticallyDesc)];

    /// <summary>Label: Learn more with video tutorials</summary>
    [ResourceEntry("LearnMoreWithVideoTutorials", Description = "Label for the external links used in videos", LastModified = "2012/04/12", Value = "Learn more with video tutorials")]
    public string LearnMoreWithVideoTutorials => this[nameof (LearnMoreWithVideoTutorials)];

    /// <summary>Phrase: Custom sorting...</summary>
    [ResourceEntry("CustomSorting", Description = "Phrase: Custom sorting...", LastModified = "2013/01/17", Value = "Custom sorting...")]
    public string CustomSorting => this[nameof (CustomSorting)];

    /// <summary>phrase: Sort</summary>
    [ResourceEntry("Sort", Description = "Label text.", LastModified = "2013/01/22", Value = "Sort")]
    public string Sort => this[nameof (Sort)];

    /// <summary>phrase: Publish videos</summary>
    [ResourceEntry("PublishVideos", Description = "Publish videos.", LastModified = "2013/02/28", Value = "Publish videos")]
    public string PublishVideos => this[nameof (PublishVideos)];

    /// <summary>phrase: Unpublish videos</summary>
    [ResourceEntry("UnpublishVideos", Description = "Unpublish videos.", LastModified = "2013/02/28", Value = "Unpublish videos")]
    public string UnpublishVideos => this[nameof (UnpublishVideos)];

    /// <summary>phrase: Bulk edit video properties</summary>
    [ResourceEntry("BulkEditVideoProperties", Description = "Bulk edit video properties", LastModified = "2013/02/28", Value = "Bulk edit video properties")]
    public string BulkEditVideoProperties => this[nameof (BulkEditVideoProperties)];

    /// <summary>phrase: Manage also.</summary>
    [ResourceEntry("ManageAlso", Description = "phrase: Manage also", LastModified = "2013/09/25", Value = "Manage also")]
    public string ManageAlso => this[nameof (ManageAlso)];

    /// <summary>phrase: Comments for videos.</summary>
    [ResourceEntry("CommentsForVideos", Description = "phrase: Comments for videos", LastModified = "2013/09/25", Value = "Comments for videos")]
    public string CommentsForVideos => this[nameof (CommentsForVideos)];

    /// <summary>Label text</summary>
    /// <value>As manually ordered</value>
    [ResourceEntry("AsManuallyOrdered", Description = "Label text", LastModified = "2014/04/23", Value = "As manually ordered")]
    public string AsManuallyOrdered => this[nameof (AsManuallyOrdered)];

    /// <summary>Word: Language</summary>
    [ResourceEntry("Language", Description = "word: Language", LastModified = "2016/05/31", Value = "Language")]
    public string Language => this[nameof (Language)];

    /// <summary>Custom fields for videos</summary>
    [ResourceEntry("CustomFields", Description = "The link that navigates to the dialog for managing custom fields in the sidebar.", LastModified = "2020/6/04", Value = "Custom fields for videos")]
    public string CustomFields => this[nameof (CustomFields)];

    /// <summary>Message: Back to videos</summary>
    [ResourceEntry("BackToVideosOperationTitle", Description = "Text of the link that will return back to videos.", LastModified = "2020/6/04", Value = "Back to videos")]
    public string BackToVideosOperationTitle => this[nameof (BackToVideosOperationTitle)];

    [ResourceEntry("AdminAppEditOperationTitle", Description = "Title for the 'Edit' operation in the Admin app. (Alternative to 'Title and properties')", LastModified = "2020/6/04", Value = "Video properties")]
    public string AdminAppEditOperationTitle => this[nameof (AdminAppEditOperationTitle)];

    /// <summary>Gets Title for the bulk edit of videos</summary>
    [ResourceEntry("VideoPropertiesBulk", Description = "Video properties (bulk).", LastModified = "2020/07/17", Value = "Video properties (bulk)")]
    public string VideoPropertiesBulk => this[nameof (VideoPropertiesBulk)];
  }
}
