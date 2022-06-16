// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Images.ImagesResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Modules.Libraries.Images
{
  /// <summary>Represents string resources for Images module UI.</summary>
  [ObjectInfo("ImagesResources", ResourceClassId = "ImagesResources")]
  public class ImagesResources : Resource
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.ImagesResources" /> class.
    /// </summary>
    public ImagesResources()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.ImagesResources" /> class.
    /// </summary>
    /// <param name="dataProvider">The data provider.</param>
    public ImagesResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Images</summary>
    [ResourceEntry("ImagesResourcesTitle", Description = "The title of this class.", LastModified = "2010/03/16", Value = "Images")]
    public string ImagesResourcesTitle => this[nameof (ImagesResourcesTitle)];

    /// <summary>Images</summary>
    [ResourceEntry("ImagesResourcesTitlePlural", Description = "The title of this class in plural.", LastModified = "2010/03/16", Value = "Images")]
    public string ImagesResourcesTitlePlural => this[nameof (ImagesResourcesTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Images user interface.
    /// </summary>
    [ResourceEntry("ImagesResourcesDescription", Description = "The description of this class.", LastModified = "2010/03/16", Value = "Contains localizable resources for Images user interface.")]
    public string ImagesResourcesDescription => this[nameof (ImagesResourcesDescription)];

    /// <summary>Word: Image</summary>
    [ResourceEntry("Image", Description = "word: Image", LastModified = "2010/03/16", Value = "Image")]
    public string Image => this[nameof (Image)];

    [ResourceEntry("AdminAppEditOperationTitle", Description = "Title for the 'Edit' operation in the Admin app. (Alternative to 'Title and properties')", LastModified = "2020/01/20", Value = "Image properties")]
    public string AdminAppEditOperationTitle => this[nameof (AdminAppEditOperationTitle)];

    /// <summary>Word: an image</summary>
    [ResourceEntry("ImageWithArticle", Description = "word: an image", LastModified = "2010/08/04", Value = "an image")]
    public string ImageWithArticle => this[nameof (ImageWithArticle)];

    /// <summary>
    /// Word: Images
    /// Images
    /// </summary>
    [ResourceEntry("ModuleTitle", Description = "The title of the Images module.", LastModified = "2010/03/17", Value = "Images")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>Phrase: Create a library</summary>
    [ResourceEntry("CreateAlbum", Description = "The label for creating an image library.", LastModified = "2013/03/08", Value = "Create a library")]
    public string CreateAlbum => this[nameof (CreateAlbum)];

    /// <summary>Phrase: Create and go back</summary>
    [ResourceEntry("CreateThisAlbum", Description = "The text of the button for creating an image library.", LastModified = "2013/03/27", Value = "Create and go back")]
    public string CreateThisAlbum => this[nameof (CreateThisAlbum)];

    /// <summary>Phrase: Create and add another image library</summary>
    [ResourceEntry("CreateAndAddAnotherAlbum", Description = "The text of the button for creating and adding another image library.", LastModified = "2010/07/13", Value = "Create and add another image library ")]
    public string CreateAndAddAnotherAlbum => this[nameof (CreateAndAddAnotherAlbum)];

    /// <summary>Phrase: Edit an image library</summary>
    [ResourceEntry("EditAlbum", Description = "The label for editing an image library.", LastModified = "2010/07/13", Value = "Edit an image library")]
    public string EditAlbum => this[nameof (EditAlbum)];

    /// <summary>Phrase: Save Changes</summary>
    [ResourceEntry("SaveChanges", Description = "The label for saving changes.", LastModified = "2010/07/26", Value = "Save changes")]
    public string SaveChanges => this[nameof (SaveChanges)];

    /// <summary>Phrase: Save the image as...</summary>
    [ResourceEntry("SaveImageAs", Description = "The label for saving an edited image under a different name.", LastModified = "2011/10/21", Value = "Save the image as...")]
    public string SaveImageAs => this[nameof (SaveImageAs)];

    /// <summary>phrase: Delete this image</summary>
    [ResourceEntry("DeleteThisItem", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/04/07", Value = "Delete")]
    public string DeleteThisItem => this[nameof (DeleteThisItem)];

    /// <summary>phrase: ReviewHistory</summary>
    [ResourceEntry("ReviewHistory", Description = "Used in the actions menu in the Backend Detail View form", LastModified = "2010/04/07", Value = "Revision History")]
    public string ReviewHistory => this[nameof (ReviewHistory)];

    /// <summary>Word: Status</summary>
    [ResourceEntry("Status", Description = "The 'Status' label.", LastModified = "2010/04/07", Value = "Status")]
    public string Status => this[nameof (Status)];

    /// <summary>Phrase: Publish on:</summary>
    [ResourceEntry("PublishOn", Description = "Phrase: Publish on:", LastModified = "2010/04/07", Value = "Publish on:")]
    public string PublishOn => this[nameof (PublishOn)];

    /// <summary>Phrase: Expires on:</summary>
    [ResourceEntry("ExpiresOn", Description = "Phrase: Expires on:", LastModified = "2010/04/07", Value = "Expires on:")]
    public string ExpiresOn => this[nameof (ExpiresOn)];

    /// <summary>Phrase: Publication Date cannot be empty</summary>
    [ResourceEntry("PublicationDateCannotBeEmpty", Description = "Publication Date cannot be empty.", LastModified = "2010/04/07", Value = "Publication Date cannot be empty")]
    public string PublicationDateCannotBeEmpty => this[nameof (PublicationDateCannotBeEmpty)];

    /// <summary>word: Author</summary>
    [ResourceEntry("Author", Description = "The 'Author' label.", LastModified = "2010/04/08", Value = "Author")]
    public string Author => this[nameof (Author)];

    /// <summary>Phrase: Size</summary>
    [ResourceEntry("Size", Description = "The 'Size' label.", LastModified = "2019/01/30", Value = "Size")]
    public string Size => this[nameof (Size)];

    /// <summary>Phrase: Dimensions:</summary>
    [ResourceEntry("Dimensions", Description = "The 'Dimensions:' label.", LastModified = "2010/04/09", Value = "Dimensions:")]
    public string Dimensions => this[nameof (Dimensions)];

    /// <summary>Move to another image library</summary>
    [ResourceEntry("MoveToAnotherAlbum", Description = "The text of the button for moving to another image library in the action menu.", LastModified = "2010/07/13", Value = "<strong>Move</strong> to another image library")]
    public string MoveToAnotherAlbum => this[nameof (MoveToAnotherAlbum)];

    /// <summary>Permissions for images</summary>
    [ResourceEntry("PermissionsForImages", Description = "The title of the permissions dialog.", LastModified = "2010/07/26", Value = "Permissions for all images")]
    public string PermissionsForImages => this[nameof (PermissionsForImages)];

    /// <summary>Permissions for images</summary>
    [ResourceEntry("PermissionsForImagesOperationTitle", Description = "The title of the permissions dialog.", LastModified = "2020/1/23", Value = "Permissions for images")]
    public string PermissionsForImagesOperationTitle => this[nameof (PermissionsForImagesOperationTitle)];

    /// <summary>Filter images</summary>
    [ResourceEntry("FilterImages", Description = "The 'Filter images' label in the sidebar.", LastModified = "2010/03/17", Value = "Filter images")]
    public string FilterImages => this[nameof (FilterImages)];

    /// <summary>Filter images by status, tag, category, etc.</summary>
    [ResourceEntry("FilterImagesBy", Description = "The 'Filter images' label in the sidebar.", LastModified = "2013/03/11", Value = "Filter images<br/><span class='sfNote'>by status, tag, category, etc.</span>")]
    public string FilterImagesBy => this[nameof (FilterImagesBy)];

    /// <summary>Filter images in this library</summary>
    [ResourceEntry("FilterImagesInThisAlbum", Description = "The 'Filter images in this library' label in the sidebar.", LastModified = "2010/07/13", Value = "Filter images in this library")]
    public string FilterImagesInThisAlbum => this[nameof (FilterImagesInThisAlbum)];

    /// <summary>All images</summary>
    [ResourceEntry("AllImages", Description = "The link for displaying all images in the sidebar.", LastModified = "2010/03/17", Value = "All images")]
    public string AllImages => this[nameof (AllImages)];

    /// <summary>My images</summary>
    [ResourceEntry("MyImages", Description = "The link for displaying my images in the sidebar.", LastModified = "2010/03/17", Value = "My images")]
    public string MyImages => this[nameof (MyImages)];

    /// <summary>The url of the default image library</summary>
    [ResourceEntry("MyImagesUrlName", Description = "The url of the default image library", LastModified = "2010/07/13", Value = "my-images")]
    public string MyImagesUrlName => this[nameof (MyImagesUrlName)];

    /// <summary>Draft images</summary>
    [ResourceEntry("MasterImages", Description = "The link for displaying draft images in the sidebar.", LastModified = "2010/03/17", Value = "Draft images")]
    public string MasterImages => this[nameof (MasterImages)];

    /// <summary>Phrase: Close date</summary>
    [ResourceEntry("CloseDateFilter", Description = "The link for closing the date filter widget in the sidebar.", LastModified = "2010/06/02", Value = "Close dates")]
    public string CloseDateFilter => this[nameof (CloseDateFilter)];

    /// <summary>Published images</summary>
    [ResourceEntry("PublishedImages", Description = "The link for displaying published images in the sidebar.", LastModified = "2010/07/26", Value = "Published")]
    public string PublishedImages => this[nameof (PublishedImages)];

    /// <summary>Scheduled images</summary>
    [ResourceEntry("ScheduledImages", Description = "The link for displaying scheduled images in the sidebar.", LastModified = "2010/08/20", Value = "Scheduled")]
    public string ScheduledImages => this[nameof (ScheduledImages)];

    /// <summary>Settings for images</summary>
    [ResourceEntry("SettingsForImages", Description = "The 'Settings for images' label in the sidebar.", LastModified = "2010/03/17", Value = "Settings for images")]
    public string SettingsForImages => this[nameof (SettingsForImages)];

    /// <summary>Pages where Images are published</summary>
    [ResourceEntry("PagesWhereImagesArePublished", Description = "Pages where Images are published", LastModified = "2013/03/25", Value = "Pages where Images are published")]
    public string PagesWhereImagesArePublished => this[nameof (PagesWhereImagesArePublished)];

    /// <summary>Pages where Images are published</summary>
    [ResourceEntry("PagesWhereImagesArePublishedOperationTitle", Description = "Pages where images are published", LastModified = "2020/02/07", Value = "Pages where images are published")]
    public string PagesWhereImagesArePublishedOperationTitle => this[nameof (PagesWhereImagesArePublishedOperationTitle)];

    /// <summary>Items displaying this item</summary>
    [ResourceEntry("ItemsDisplayingThisItem", Description = "Items displaying this item", LastModified = "2020/03/05", Value = "Items displaying this item")]
    public string ItemsDisplayingThisItem => this[nameof (ItemsDisplayingThisItem)];

    /// <summary>Title / Status</summary>
    [ResourceEntry("TitleStatus", Description = "The header of 'Title / Status' column in the grid.", LastModified = "2010/03/17", Value = "Title / Status")]
    public string TitleStatus => this[nameof (TitleStatus)];

    /// <summary>Image Library / Categories / Tags</summary>
    [ResourceEntry("AlbumCategoriesTags", Description = "The header of 'Image Library / Categories / Tags' column in the grid.", LastModified = "2010/07/13", Value = "Image Library / Categories / Tags")]
    public string AlbumCategoriesTags => this[nameof (AlbumCategoriesTags)];

    /// <summary>Phrase: What do you want to do now?</summary>
    [ResourceEntry("WhatDoYouWantToDoNow", Description = "The title of the decision screen when there are no images.", LastModified = "2010/03/18", Value = "What do you want to do now?")]
    public string WhatDoYouWantToDoNow => this[nameof (WhatDoYouWantToDoNow)];

    /// <summary>Phrase: No images have been uploaded yet</summary>
    [ResourceEntry("NoItemsExist", Description = "The text of the message that user is presented with on the decision screen.", LastModified = "2009/01/29", Value = "No images have been uploaded yet")]
    public string NoItemsExist => this[nameof (NoItemsExist)];

    /// <summary>Library</summary>
    [ResourceEntry("AlbumLabel", Description = "The 'Library' label in the grid column.", LastModified = "2010/07/13", Value = "<em>Library:</em>")]
    public string AlbumLabel => this[nameof (AlbumLabel)];

    /// <summary>Image Library</summary>
    [ResourceEntry("Album", Description = "phrase: Image Library", LastModified = "2010/07/13", Value = "Library")]
    public string Album => this[nameof (Album)];

    /// <summary>
    /// phrase: <i>{0}</i>
    /// </summary>
    [ResourceEntry("AlbumImagesTitleFormat", Description = "phrase: <i>{0}</i>", LastModified = "2013/02/26", Value = "<i>{0}</i>")]
    public string AlbumImagesTitleFormat => this[nameof (AlbumImagesTitleFormat)];

    /// <summary>Common image library</summary>
    [ResourceEntry("CommonAlbum", Description = "phrase: Common image library", LastModified = "2010/07/13", Value = "Common image library")]
    public string CommonAlbum => this[nameof (CommonAlbum)];

    /// <summary>Embed this image</summary>
    [ResourceEntry("EmbedThisImage", Description = "The text of the main action in the grid.", LastModified = "2010/03/18", Value = "Embed this image")]
    public string EmbedThisImage => this[nameof (EmbedThisImage)];

    /// <summary>Phrase: Enable Output Caching...</summary>
    [ResourceEntry("EnableCaching", Description = "Phrase: Enable Output Caching...", LastModified = "2011/03/15", Value = "Enable <strong>Output Caching</strong>...")]
    public string EnableCaching => this[nameof (EnableCaching)];

    /// <summary>Phrase: Enable Client Caching...</summary>
    [ResourceEntry("EnableClientCaching", Description = "Phrase: Enable Client Caching...", LastModified = "2011/03/15", Value = "Enable <strong>Client Caching</strong>...")]
    public string EnableClientCaching => this[nameof (EnableClientCaching)];

    /// <summary>Phrase: Use default settings for Caching</summary>
    [ResourceEntry("UseDefaultSettingsForCaching", Description = "Phrase: Use default settings for Caching", LastModified = "2011/03/15", Value = "Use default settings for Output Caching")]
    public string UseDefaultSettingsForCaching => this[nameof (UseDefaultSettingsForCaching)];

    /// <summary>Phrase: Use default settings for Client Caching</summary>
    [ResourceEntry("UseDefaultSettingsForClientCaching", Description = "Phrase: Use default settings for client Caching", LastModified = "2011/03/15", Value = "Use default settings for Client Caching")]
    public string UseDefaultSettingsForClientCaching => this[nameof (UseDefaultSettingsForClientCaching)];

    /// <summary>Phrase: Cache duration (in seconds)</summary>
    [ResourceEntry("CacheDuration", Description = "Phrase: Cache duration (in seconds)", LastModified = "2011/03/15", Value = "Cache duration <span class='sfNote'>(in seconds)</span>")]
    public string CacheDuration => this[nameof (CacheDuration)];

    /// <summary>Phrase: Client Cache duration (in seconds)</summary>
    [ResourceEntry("ClientCacheDuration", Description = "Phrase: Client Cache duration (in seconds)", LastModified = "2011/03/15", Value = "Client Cache duration <span class='sfNote'>(in seconds)</span>")]
    public string ClientCacheDuration => this[nameof (ClientCacheDuration)];

    /// <summary>Phrase: Cache max size (in KB)</summary>
    [ResourceEntry("CacheMaxSize", Description = "Phrase: Cache item max limit  (in KB)", LastModified = "2011/03/15", Value = "Cache item max limit <span class='sfNote'>(in KB)</span>")]
    public string CacheMaxSize => this[nameof (CacheMaxSize)];

    /// <summary>Phrase: Sliding expiration</summary>
    [ResourceEntry("SlidingExpiration", Description = "Phrase: Sliding expiration", LastModified = "2011/03/15", Value = "Sliding expiration")]
    public string SlidingExpiration => this[nameof (SlidingExpiration)];

    /// <summary>
    /// Phrase: Edit image
    /// Phrase: Edit image
    /// </summary>
    [ResourceEntry("EditImage", Description = "The title of the dialog for editing images.", LastModified = "2010/03/22", Value = "Edit image")]
    public string EditImage => this[nameof (EditImage)];

    /// <summary>Phrase: Create Image</summary>
    [ResourceEntry("CreateImage", Description = "The title of the dialog for creating images.", LastModified = "2010/11/23", Value = "Create Image")]
    public string CreateImage => this[nameof (CreateImage)];

    /// <summary>Phrase: Image Library name</summary>
    [ResourceEntry("AlbumName", Description = "The 'Image Library name' label.", LastModified = "2010/07/13", Value = "Image Library name")]
    public string AlbumName => this[nameof (AlbumName)];

    /// <summary>Example: Summer vacation</summary>
    [ResourceEntry("AlbumNameExample", Description = "The example how to fill image library name field.", LastModified = "2010/07/13", Value = "Example: Summer vacation")]
    public string AlbumNameExample => this[nameof (AlbumNameExample)];

    /// <summary>Phrase: Library name cannot be empty</summary>
    [ResourceEntry("AlbumNameCannotBeEmpty", Description = "The message shown when the image library name is empty.", LastModified = "2010/07/13", Value = "Library name cannot be empty")]
    public string AlbumNameCannotBeEmpty => this[nameof (AlbumNameCannotBeEmpty)];

    /// <summary>Word: Description</summary>
    [ResourceEntry("Description", Description = "The 'Description' label.", LastModified = "2010/03/30", Value = "Description")]
    public string Description => this[nameof (Description)];

    /// <summary>Phrase: The description of this image library</summary>
    [ResourceEntry("AlbumDescription", Description = "The description of the Description field in the image library form.", LastModified = "2010/07/13", Value = "The description of this image library.")]
    public string AlbumDescription => this[nameof (AlbumDescription)];

    /// <summary>Phrase: The description of this image</summary>
    [ResourceEntry("ImageDescription", Description = "The description of the Description field in the image form.", LastModified = "2010/04/08", Value = "The description of this image.")]
    public string ImageDescription => this[nameof (ImageDescription)];

    /// <summary>Phrase: Default image size</summary>
    [ResourceEntry("DefaultImageSize", Description = "The title of the 'Default image size' section.", LastModified = "2010/03/30", Value = "Default image size")]
    public string DefaultImageSize => this[nameof (DefaultImageSize)];

    /// <summary>
    /// Phrase: Do you want to resize images on upload in this library?
    /// </summary>
    [ResourceEntry("ResizeImagesOnUpload", Description = "The title of the field which provides options for resizing images on upload.", LastModified = "2010/07/13", Value = "Do you want to resize images on upload in this library?")]
    public string ResizeImagesOnUpload => this[nameof (ResizeImagesOnUpload)];

    /// <summary>Phrase: Don't resize, always upload original images</summary>
    [ResourceEntry("UploadOriginalImages", Description = "The text of the option for uploading original image.", LastModified = "2010/03/30", Value = "Don't resize, always upload original images")]
    public string UploadOriginalImages => this[nameof (UploadOriginalImages)];

    /// <summary>
    /// Phrase: Resize images so their width won't be larger than...
    /// </summary>
    [ResourceEntry("ResizeImages", Description = "The text of the option for resizing image.", LastModified = "2010/03/30", Value = "Resize images so their width won't be larger than...")]
    public string ResizeImages => this[nameof (ResizeImages)];

    /// <summary>Phrase: - Select size -</summary>
    [ResourceEntry("SelectSize", Description = "The default option in the dropdown for selecting size.", LastModified = "2010/03/30", Value = "- Select size -")]
    public string SelectSize => this[nameof (SelectSize)];

    /// <summary>Phrase: Thumbnail: 100 px width</summary>
    /// <value>100 px width</value>
    [ResourceEntry("Thumbnail", Description = "The thumbnail option in the dropdown for selecting size.", LastModified = "2013/07/01", Value = "100 px width")]
    public string Thumbnail => this[nameof (Thumbnail)];

    /// <summary>Phrase: Small: 240 px width</summary>
    /// <value>240 px width</value>
    [ResourceEntry("Small", Description = "The small option in the dropdown for selecting size.", LastModified = "2013/07/01", Value = "240 px width")]
    public string Small => this[nameof (Small)];

    /// <summary>Phrase: Medium: 500 px width</summary>
    /// <value>500 px width</value>
    [ResourceEntry("Medium", Description = "The medium option in the dropdown for selecting size.", LastModified = "2013/07/01", Value = "500 px width")]
    public string Medium => this[nameof (Medium)];

    /// <summary>Phrase: Large: 800 px width</summary>
    /// <value>800 px width</value>
    [ResourceEntry("Large", Description = "The large option in the dropdown for selecting size.", LastModified = "2013/07/01", Value = "800 px width")]
    public string Large => this[nameof (Large)];

    /// <summary>Phrase: Extra-large: 1024 px width</summary>
    [ResourceEntry("ExtraLarge", Description = "The extra-large option in the dropdown for selecting size.", LastModified = "2010/03/30", Value = "Extra-large: 1024 px width")]
    public string ExtraLarge => this[nameof (ExtraLarge)];

    /// <summary>Phrase: Custom size...</summary>
    [ResourceEntry("CustomSize", Description = "The custom size option in the dropdown for selecting size.", LastModified = "2010/03/30", Value = "Custom size...")]
    public string CustomSize => this[nameof (CustomSize)];

    /// <summary>Custom fields for images</summary>
    [ResourceEntry("CustomFields", Description = "The link that navigates to the dialog for managing custom fields in the sidebar.", LastModified = "2020/01/22", Value = "Custom fields for images")]
    public string CustomFields => this[nameof (CustomFields)];

    /// <summary>Phrase: Max library size</summary>
    [ResourceEntry("MaxAlbumSize", Description = "The title of the field for setting max image library size.", LastModified = "2010/07/13", Value = "Max library size")]
    public string MaxAlbumSize => this[nameof (MaxAlbumSize)];

    /// <summary>Phrase: Max image size</summary>
    [ResourceEntry("MaxImageSize", Description = "The title of the field for setting max image size.", LastModified = "2010/03/30", Value = "Max image size")]
    public string MaxImageSize => this[nameof (MaxImageSize)];

    /// <summary>Phrase: Alternative text</summary>
    [ResourceEntry("AlternativeText", Description = "The 'Alternative text' label.", LastModified = "2010/04/08", Value = "Alternative text")]
    public string AlternativeText => this[nameof (AlternativeText)];

    /// <summary>Phrase: Click to add alternative text</summary>
    [ResourceEntry("ClickToAddAlternativeText", Description = "The expand text for the alternative text field.", LastModified = "2010/04/08", Value = "Click to add alternative text")]
    public string ClickToAddAlternativeText => this[nameof (ClickToAddAlternativeText)];

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

    /// <summary>phrase: Type library name</summary>
    [ResourceEntry("TitlePlaceholder", Description = "Placeholder for library's title.", LastModified = "2019/11/27", Value = "Type library name")]
    public string TitlePlaceholder => this[nameof (TitlePlaceholder)];

    /// <summary>phrase: Image Libraries</summary>
    [ResourceEntry("Albums", Description = "phrase: Image Libraries", LastModified = "2010/07/13", Value = "Image Libraries")]
    public string Albums => this[nameof (Albums)];

    /// <summary>Phrase: Manage Image Libraries</summary>
    [ResourceEntry("ManageAlbums", Description = "The text of 'Manage Image Libraries' link.", LastModified = "2010/07/13", Value = "Manage Image Libraries")]
    public string ManageAlbums => this[nameof (ManageAlbums)];

    /// <summary>Phrase: Merge image libraries</summary>
    [ResourceEntry("MergeAlbums", Description = "The text of the 'Merge image libraries' button.", LastModified = "2010/07/13", Value = "Merge image libraries")]
    public string MergeAlbums => this[nameof (MergeAlbums)];

    /// <summary>Phrase: Upload images in this library</summary>
    [ResourceEntry("UploadImages", Description = "Phrase: Upload images in this library", LastModified = "2010/07/13", Value = "<strong>Upload images</strong> in this library")]
    public string UploadImages => this[nameof (UploadImages)];

    /// <summary>Phrase: Last uploaded on</summary>
    [ResourceEntry("LastUploaded", Description = "'Last uploaded on' label in the image libraries grid.", LastModified = "2013/03/06", Value = "Last uploaded on")]
    public string LastUploaded => this[nameof (LastUploaded)];

    /// <summary>Phrase: Upload images</summary>
    [ResourceEntry("UploadImagesLabel", Description = "phrase: Upload images", LastModified = "2010/12/20", Value = "Upload images")]
    public string UploadImagesLabel => this[nameof (UploadImagesLabel)];

    /// <summary>Phrase: View in original size</summary>
    [ResourceEntry("ViewOriginalSize", Description = "The text of the 'View in original size' button.", LastModified = "2010/04/13", Value = "View in original size")]
    public string ViewOriginalSize => this[nameof (ViewOriginalSize)];

    /// <summary>Phrase: Replace Image</summary>
    [ResourceEntry("ReplaceImage", Description = "The text of the 'Replace Image' button.", LastModified = "2010/04/13", Value = "Replace Image")]
    public string ReplaceImage => this[nameof (ReplaceImage)];

    /// <summary>Message: Back to all image libraries</summary>
    /// <value>Text of the link that will show the view that lists all image libraries.</value>
    [ResourceEntry("BackToAllAlbums", Description = "Text of the link that will show the view that lists all image libraries.", LastModified = "2010/07/13", Value = "Back to all image libraries")]
    public string BackToAllAlbums => this[nameof (BackToAllAlbums)];

    /// <summary>Message: View all images</summary>
    /// <value>Text of the link that will show the view that lists all images.</value>
    [ResourceEntry("BackToImages", Description = "Text of the link that will show the view that lists all images.", LastModified = "2010/07/26", Value = "Back to Images")]
    public string BackToImages => this[nameof (BackToImages)];

    /// <summary>Message: Back to Images</summary>
    /// <value>The back to all items button</value>
    [ResourceEntry("BackToItems", Description = "The back to all items button", LastModified = "2010/10/16", Value = "Back to Images")]
    public string BackToItems => this[nameof (BackToItems)];

    /// <summary>Phrase: Height</summary>
    [ResourceEntry("Height", Description = "The 'Height' label.", LastModified = "2019/01/30", Value = "Height")]
    public string Height => this[nameof (Height)];

    /// <summary>Phrase: Width</summary>
    [ResourceEntry("Width", Description = "The 'Width' label.", LastModified = "2019/01/30", Value = "Width")]
    public string Width => this[nameof (Width)];

    /// <summary>Phrase: px</summary>
    [ResourceEntry("px", Description = "The 'px' unit.", LastModified = "2011/01/20", Value = "px")]
    public string px => this[nameof (px)];

    /// <summary>Phrase: bytes</summary>
    [ResourceEntry("bytes", Description = "The 'bytes' unit.", LastModified = "2011/01/20", Value = "bytes")]
    public string bytes => this[nameof (bytes)];

    /// <summary>Phrase: Link (paste in email or IM)</summary>
    [ResourceEntry("LinkPasteInEmailOrIm", Description = "The 'Link (paste in email or IM)' label.", LastModified = "2010/04/28", Value = "Link <span class='sfNote'>(paste in email or IM)</span>")]
    public string LinkPasteInEmailOrIm => this[nameof (LinkPasteInEmailOrIm)];

    /// <summary>Phrase: Embed (paste in HTML)</summary>
    [ResourceEntry("EmbedPasteInHtml", Description = "The 'Embed (paste in HTML)' label.", LastModified = "2010/04/28", Value = "Embed <span class='sfNote'>(paste in HTML)</span>")]
    public string EmbedPasteInHtml => this[nameof (EmbedPasteInHtml)];

    /// <summary>Phrase: Customize embedded image</summary>
    [ResourceEntry("CustomizeEmbeddedImage", Description = "The 'Customize embedded image' label.", LastModified = "2010/04/28", Value = "Customize embedded image")]
    public string CustomizeEmbeddedImage => this[nameof (CustomizeEmbeddedImage)];

    /// <summary>Phrase: 1. Choose size</summary>
    [ResourceEntry("ChooseSize1", Description = "The '1. Choose size' label.", LastModified = "2010/04/28", Value = "1. Choose size")]
    public string ChooseSize1 => this[nameof (ChooseSize1)];

    /// <summary>Phrase: 2. Embed (paste in HTML)</summary>
    [ResourceEntry("EmbedPasteInHtml2", Description = "The '2. Embed (paste in HTML)' label.", LastModified = "2010/04/28", Value = "2. Embed (paste in HTML)")]
    public string EmbedPasteInHtml2 => this[nameof (EmbedPasteInHtml2)];

    /// <summary>Phrase: Always resize to this size by default</summary>
    [ResourceEntry("AlwaysResizeToThisSizeByDefault", Description = "The 'Always resize to this size by default' label.", LastModified = "2010/04/28", Value = "Always resize to this size by default")]
    public string AlwaysResizeToThisSizeByDefault => this[nameof (AlwaysResizeToThisSizeByDefault)];

    /// <summary>phrase: Revision History</summary>
    [ResourceEntry("LinkOrEmbed", Description = "phrase: Link or embed", LastModified = "2010/04/28", Value = "Link or embed")]
    public string LinkOrEmbed => this[nameof (LinkOrEmbed)];

    /// <summary>phrase: Custom...</summary>
    [ResourceEntry("Custom", Description = "phrase: Custom...", LastModified = "2010/04/28", Value = "Custom...")]
    public string Custom => this[nameof (Custom)];

    /// <summary>phrase: Which images to display?</summary>
    [ResourceEntry("WhichImagesToDisplay", Description = "phrase: Which images to display?", LastModified = "2010/06/08", Value = "Which images to display?")]
    public string WhichImagesToDisplay => this[nameof (WhichImagesToDisplay)];

    /// <summary>phrase: Choose Image Library</summary>
    [ResourceEntry("ChooseAlbum", Description = "phrase: Choose Image Library", LastModified = "2010/07/13", Value = "Choose Image Library")]
    public string ChooseAlbum => this[nameof (ChooseAlbum)];

    /// <summary>phrase: All published images</summary>
    [ResourceEntry("AllPublishedImages", Description = "phrase: All published images", LastModified = "2010/06/08", Value = "All published images")]
    public string AllPublishedImages => this[nameof (AllPublishedImages)];

    /// <summary>phrase: Set permissions</summary>
    [ResourceEntry("SetPermissions", Description = "The text of the 'Set permissions' link in the action menu.", LastModified = "2010/03/22", Value = "Set Permissions")]
    public string SetPermissions => this[nameof (SetPermissions)];

    /// <summary>phrase: From selected library...</summary>
    [ResourceEntry("FromSelectedAlbum", Description = "phrase: From selected image library...", LastModified = "2010/07/13", Value = "From selected image library...")]
    public string FromSelectedAlbum => this[nameof (FromSelectedAlbum)];

    /// <summary>phrase: Upload new images...</summary>
    [ResourceEntry("UploadNewImages", Description = "phrase: Upload new images...", LastModified = "2010/06/08", Value = "Upload new images...")]
    public string UploadNewImages => this[nameof (UploadNewImages)];

    /// <summary>phrase: Select image gallery type</summary>
    [ResourceEntry("SelectImageGalleryType", Description = "phrase: Select image gallery type", LastModified = "2010/06/08", Value = "Select image gallery type")]
    public string SelectImageGalleryType => this[nameof (SelectImageGalleryType)];

    /// <summary>phrase: From already uploaded images</summary>
    /// <value>From already uploaded images</value>
    [ResourceEntry("FromAlreadyUploaded", Description = "phrase: From already uploaded images", LastModified = "2013/09/12", Value = "From already uploaded images")]
    public string FromAlreadyUploaded => this[nameof (FromAlreadyUploaded)];

    /// <summary>The title of the dialog for upload an image.</summary>
    /// <value>'Upload image' in the Backend</value>
    [ResourceEntry("UploadImage", Description = "The title of the dialog for upload an image.", LastModified = "2014/04/07", Value = "'Upload image' in the Backend")]
    public string UploadImage => this[nameof (UploadImage)];

    /// <summary>Phrase: Preview image</summary>
    /// <value>Preview image</value>
    [ResourceEntry("PreviewImage", Description = "Phrase: Preview image", LastModified = "2014/04/08", Value = "Preview image")]
    public string PreviewImage => this[nameof (PreviewImage)];

    /// <summary>Phrase: Revision history</summary>
    /// <value>Revision history</value>
    [ResourceEntry("ImageRevisionHistory", Description = "Phrase: Revision history", LastModified = "2014/04/09", Value = "Revision history")]
    public string ImageRevisionHistory => this[nameof (ImageRevisionHistory)];

    /// <summary>phrase: Uploaded on</summary>
    [ResourceEntry("UploadedOn", Description = "phrase: Uploaded on", LastModified = "2010/04/12", Value = "Uploaded on")]
    public string UploadedOn => this[nameof (UploadedOn)];

    /// <summary>Word: Previous</summary>
    [ResourceEntry("Previous", Description = "word", LastModified = "2010/06/02", Value = "Previous")]
    public string Previous => this[nameof (Previous)];

    /// <summary>Word: Next</summary>
    [ResourceEntry("Next", Description = "word", LastModified = "2010/06/02", Value = "Next")]
    public string Next => this[nameof (Next)];

    /// <summary>Messsage: type name Images class</summary>
    [ResourceEntry("ImagesPluralTypeName", Description = "Images plural type name", LastModified = "2010/01/29", Value = "Images")]
    public string ImagesPluralTypeName => this[nameof (ImagesPluralTypeName)];

    /// <summary>Messsage: type name Images class</summary>
    [ResourceEntry("ImagesSingleTypeName", Description = "Images single type name", LastModified = "2010/01/29", Value = "Image")]
    public string ImagesSingleTypeName => this[nameof (ImagesSingleTypeName)];

    /// <summary>Messsage: type name Images class</summary>
    [ResourceEntry("AlbumsPluralTypeName", Description = "Image Libraries plural type name", LastModified = "2010/07/13", Value = "Image Libraries")]
    public string AlbumsPluralTypeName => this[nameof (AlbumsPluralTypeName)];

    /// <summary>Messsage: type name Images class</summary>
    [ResourceEntry("AlbumsSingleTypeName", Description = "Image Library single type name", LastModified = "2010/07/13", Value = "Image Library")]
    public string AlbumsSingleTypeName => this[nameof (AlbumsSingleTypeName)];

    /// <summary>word: Images</summary>
    [ResourceEntry("Images", Description = "word: Images", LastModified = "2010/03/16", Value = "Images")]
    public string Images => this[nameof (Images)];

    /// <summary>Messsage: Manage Images</summary>
    [ResourceEntry("ManageImages", Description = "Manage Images", LastModified = "2010/10/01", Value = "Manage Images")]
    public string ManageImages => this[nameof (ManageImages)];

    /// <summary>Phrase: Images By Date</summary>
    [ResourceEntry("ImagesByDate", Description = "phrase: Images By Date", LastModified = "2010/06/02", Value = "Display images modified in...")]
    public string ImagesByDate => this[nameof (ImagesByDate)];

    /// <summary>Upload images button</summary>
    [ResourceEntry("UploadImagesButton", Description = "The text of the upload button in grid toolbar.", LastModified = "2010/08/23", Value = "Upload images")]
    public string UploadImagesButton => this[nameof (UploadImagesButton)];

    /// <summary>Phrase: Images By Library</summary>
    [ResourceEntry("ImagesByAlbum", Description = "phrase: Images by Library", LastModified = "2010/07/13", Value = "Images by Library")]
    public string ImagesByAlbum => this[nameof (ImagesByAlbum)];

    /// <summary>Phrase: Images By Category</summary>
    [ResourceEntry("ImagesByCategory", Description = "phrase: Images by category", LastModified = "2010/07/22", Value = "Images by category")]
    public string ImagesByCategory => this[nameof (ImagesByCategory)];

    /// <summary>Phrase: Images By tag</summary>
    [ResourceEntry("ImagesByTag", Description = "phrase: Images by tag", LastModified = "2010/07/22", Value = "Images by tag")]
    public string ImagesByTag => this[nameof (ImagesByTag)];

    /// <summary>phrase: Change</summary>
    [ResourceEntry("ChangeAlbum", Description = "Change image library", LastModified = "2010/07/13", Value = "Change")]
    public string ChangeAlbum => this[nameof (ChangeAlbum)];

    /// <summary>phrase: Change</summary>
    [ResourceEntry("ChangeAlbumInSpan", Description = "Change image library", LastModified = "2010/07/13", Value = "<span class='sfLinkBtnIn'>Change library</span>")]
    public string ChangeAlbumInSpan => this[nameof (ChangeAlbumInSpan)];

    /// <summary>phrase: select an image library</summary>
    [ResourceEntry("SelectAnAlbum", Description = "phrase: -- select an image library --", LastModified = "2010/07/13", Value = "-- select an image library --")]
    public string SelectAnAlbum => this[nameof (SelectAnAlbum)];

    /// <summary>phrase: {0} more image libraries</summary>
    [ResourceEntry("ShowMoreAlbums", Description = "phrase: {0} more image libraries", LastModified = "2010/07/13", Value = "{0} more image libraries")]
    public string ShowMoreAlbums => this[nameof (ShowMoreAlbums)];

    /// <summary>phrase: {0} less image libraries</summary>
    [ResourceEntry("ShowLessAlbums", Description = "phrase: {0} less image libraries", LastModified = "2010/07/13", Value = "{0} less image libraries")]
    public string ShowLessAlbums => this[nameof (ShowLessAlbums)];

    /// <summary>phrase: Sort images</summary>
    [ResourceEntry("SortImages", Description = "phrase: Sort images ", LastModified = "2010/05/18", Value = "Sort images ")]
    public string SortImages => this[nameof (SortImages)];

    /// <summary>word: Original</summary>
    [ResourceEntry("Original", Description = "word: Original", LastModified = "2010/05/17", Value = "Original")]
    public string Original => this[nameof (Original)];

    /// <summary>phrase: Reorder images</summary>
    [ResourceEntry("ReorderImages", Description = "phrase: Reorder images", LastModified = "2010/05/20", Value = "<em>Reorder</em> images")]
    public string ReorderImages => this[nameof (ReorderImages)];

    /// <summary>phrase: From already uploaded</summary>
    [ResourceEntry("FromAlreadyUploadedImages", Description = "phrase: From already uploaded", LastModified = "2010/05/20", Value = "From already uploaded")]
    public string FromAlreadyUploadedImages => this[nameof (FromAlreadyUploadedImages)];

    /// <summary>
    /// phrase: An image was not selected or has been deleted. Please select another one.
    /// </summary>
    [ResourceEntry("ImageWasNotSelectedOrHasBeenDeleted", Description = "phrase: An image was not selected or has been deleted. Please select another one.", LastModified = "2010/07/20", Value = "An image was not selected or has been deleted. Please select another one.")]
    public string ImageWasNotSelectedOrHasBeenDeleted => this[nameof (ImageWasNotSelectedOrHasBeenDeleted)];

    /// <summary>phrase: image</summary>
    [ResourceEntry("ImageSingularItemName", Description = "phrase: image", LastModified = "2010/10/11", Value = "image")]
    public string ImageSingularItemName => this[nameof (ImageSingularItemName)];

    /// <summary>Word: Select an image</summary>
    [ResourceEntry("SelectAnImage", Description = "word: Select an image", LastModified = "2010/11/12", Value = "Select an image")]
    public string SelectAnImage => this[nameof (SelectAnImage)];

    /// <summary>phrase: Set which images to display</summary>
    [ResourceEntry("SetImagesToDisplay", Description = "phrase: Set which images to display", LastModified = "2010/11/12", Value = "Set which images to display")]
    public string SetImagesToDisplay => this[nameof (SetImagesToDisplay)];

    /// <summary>phrase: Edit Image gallery settings</summary>
    [ResourceEntry("EditImageGallerySettings", Description = "phrase: Edit Image gallery settings", LastModified = "2010/11/13", Value = "Edit Image gallery settings")]
    public string EditImageGallerySettings => this[nameof (EditImageGallerySettings)];

    /// <summary>phrase: images</summary>
    [ResourceEntry("ImagePluralItemName", Description = "phrase: images", LastModified = "2010/10/11", Value = "images")]
    public string ImagePluralItemName => this[nameof (ImagePluralItemName)];

    /// <summary>phrase: Images data fields</summary>
    [ResourceEntry("ImagesDataFields", Description = "phrase: Images data fields", LastModified = "2010/11/29", Value = "Images data fields")]
    public string ImagesDataFields => this[nameof (ImagesDataFields)];

    /// <summary>Phrase: Change photo</summary>
    [ResourceEntry("ChangePhoto", Description = "phrase: Change photo", LastModified = "2011/02/26", Value = "Change photo")]
    public string ChangePhoto => this[nameof (ChangePhoto)];

    /// <summary>Phrase: Delete photo</summary>
    [ResourceEntry("DeletePhoto", Description = "phrase: Delete photo", LastModified = "2011/02/26", Value = "Delete photo")]
    public string DeletePhoto => this[nameof (DeletePhoto)];

    /// <summary>Phrase: Create and go back to all images</summary>
    [ResourceEntry("CreateAndGoBackToAllImages", Description = "phrase: Create and go back to all images.", LastModified = "2011/01/21", Value = "Create and go back to all images")]
    public string CreateAndGoBackToAllImages => this[nameof (CreateAndGoBackToAllImages)];

    /// <summary>Phrase: Create and go to upload images</summary>
    [ResourceEntry("CreateAndGoToUploadImages", Description = "phrase: Create and go to upload images.", LastModified = "2011/01/21", Value = "Create and go to upload images")]
    public string CreateAndGoToUploadImages => this[nameof (CreateAndGoToUploadImages)];

    /// <summary>Phrase: Don't change photo</summary>
    [ResourceEntry("DontChangePhoto", Description = "phrase: Don't change photo", LastModified = "2011/03/02", Value = "Don't change photo")]
    public string DontChangePhoto => this[nameof (DontChangePhoto)];

    /// <summary>phrase: Last modified on top</summary>
    [ResourceEntry("NewModifiedFirst", Description = "Label text.", LastModified = "2010/08/16", Value = "Last modified on top")]
    public string NewModifiedFirst => this[nameof (NewModifiedFirst)];

    /// <summary>phrase: New-uploaded first</summary>
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

    /// <summary>phrase: Server caching</summary>
    [ResourceEntry("ServerCaching", Description = "phrase: Server caching", LastModified = "2011/03/31", Value = "Server caching")]
    public string ServerCaching => this[nameof (ServerCaching)];

    /// <summary>phrase: Browser caching</summary>
    [ResourceEntry("BrowserCaching", Description = "phrase: Browser caching", LastModified = "2011/03/31", Value = "Browser caching")]
    public string BrowserCaching => this[nameof (BrowserCaching)];

    /// <summary>phrase: As set for the whole site</summary>
    [ResourceEntry("AsForWholeSite", Description = "phrase: As set for the whole site", LastModified = "2011/03/31", Value = "As set for the whole site")]
    public string AsForWholeSite => this[nameof (AsForWholeSite)];

    /// <summary>phrase: File name</summary>
    [ResourceEntry("FileName", Description = "phrase: File name", LastModified = "2011/06/03", Value = "File name")]
    public string FileName => this[nameof (FileName)];

    /// <summary>phrase: File size</summary>
    [ResourceEntry("FileSize", Description = "phrase: File size", LastModified = "2011/06/03", Value = "File size")]
    public string FileSize => this[nameof (FileSize)];

    /// <summary>word: Uploaded</summary>
    [ResourceEntry("Uploaded", Description = "word: Uploaded", LastModified = "2011/06/03", Value = "Uploaded")]
    public string Uploaded => this[nameof (Uploaded)];

    /// <summary>word: Title</summary>
    [ResourceEntry("Title", Description = "word: Title", LastModified = "2011/06/03", Value = "Title")]
    public string Title => this[nameof (Title)];

    /// <summary>phrase: Image size</summary>
    [ResourceEntry("ImageSize", Description = "phrase: Image size", LastModified = "2011/06/03", Value = "Image size")]
    public string ImageSize => this[nameof (ImageSize)];

    /// <summary>phrase: Alignment</summary>
    [ResourceEntry("Alignment", Description = "phrase: Alignment", LastModified = "2013/06/11", Value = "Alignment")]
    public string Alignment => this[nameof (Alignment)];

    /// <summary>phrase: visual appearance and search engines</summary>
    [ResourceEntry("VisualAppearanceAndSearchEngines", Description = "phrase: visual appearance and search engines", LastModified = "2011/06/03", Value = "visual appearance and search engines")]
    public string VisualAppearanceAndSearchEngines => this[nameof (VisualAppearanceAndSearchEngines)];

    /// <summary>Label: Learn more with video tutorials</summary>
    [ResourceEntry("LearnMoreWithVideoTutorials", Description = "Label for the external links used in images", LastModified = "2012/04/12", Value = "Learn more with video tutorials")]
    public string LearnMoreWithVideoTutorials => this[nameof (LearnMoreWithVideoTutorials)];

    /// <summary>phrase: Sort</summary>
    [ResourceEntry("Sort", Description = "Label text.", LastModified = "2013/01/14", Value = "Sort")]
    public string Sort => this[nameof (Sort)];

    /// <summary>phrase: By Title (A-Z)</summary>
    [ResourceEntry("ByTitleAsc", Description = "Label text.", LastModified = "2013/01/14", Value = "By Title (A-Z)")]
    public string ByTitleAsc => this[nameof (ByTitleAsc)];

    /// <summary>phrase: By Title (Z-A)</summary>
    [ResourceEntry("ByTitleDesc", Description = "Label text.", LastModified = "2013/01/14", Value = "By Title (Z-A)")]
    public string ByTitleDesc => this[nameof (ByTitleDesc)];

    /// <summary>phrase: LibraryModifiedFirst</summary>
    [ResourceEntry("LibraryModifiedFirst", Description = "Label text.", LastModified = "2013/01/14", Value = "Last modified on top")]
    public string LibraryModifiedFirst => this[nameof (LibraryModifiedFirst)];

    /// <summary>phrase: Last created on top</summary>
    [ResourceEntry("LibraryCreatedFirst", Description = "Label text.", LastModified = "2013/01/14", Value = "Last created on top")]
    public string LibraryCreatedFirst => this[nameof (LibraryCreatedFirst)];

    /// <summary>Phrase: Custom sorting...</summary>
    [ResourceEntry("CustomSorting", Description = "Phrase: Custom sorting...", LastModified = "2013/01/14", Value = "Custom sorting...")]
    public string CustomSorting => this[nameof (CustomSorting)];

    /// <summary>phrase: Publish images</summary>
    [ResourceEntry("PublishImages", Description = "Publish images.", LastModified = "2013/02/28", Value = "Publish images")]
    public string PublishImages => this[nameof (PublishImages)];

    /// <summary>phrase: Unpublish images</summary>
    [ResourceEntry("UnpublishImages", Description = "Unpublish images.", LastModified = "2013/02/28", Value = "Unpublish images")]
    public string UnpublishImages => this[nameof (UnpublishImages)];

    /// <summary>phrase: Bulk edit image properties</summary>
    [ResourceEntry("BulkEditImageProperties", Description = "Bulk edit image properties", LastModified = "2013/02/28", Value = "Bulk edit image properties")]
    public string BulkEditImageProperties => this[nameof (BulkEditImageProperties)];

    /// <summary>Gets Title for the bulk edit of images</summary>
    [ResourceEntry("ImagePropertiesBulk", Description = "Image properties (bulk).", LastModified = "2020/06/18", Value = "Image properties (bulk)")]
    public string ImagePropertiesBulk => this[nameof (ImagePropertiesBulk)];

    /// <summary>phrase: Images by library</summary>
    [ResourceEntry("ImagesByLibrary", Description = "Images by library", LastModified = "2013/03/11", Value = "Images by library")]
    public string ImagesByLibrary => this[nameof (ImagesByLibrary)];

    /// <summary>Message: Back to Image</summary>
    [ResourceEntry("BackToItem", Description = "The back to item details button", LastModified = "2013/06/25", Value = "Back to Image")]
    public string BackToItem => this[nameof (BackToItem)];

    /// <summary>phrase: Edit image</summary>
    [ResourceEntry("EditImageInBackendLabel", Description = "phrase: Edit image", LastModified = "2013/09/24", Value = "Edit image")]
    public string EditImageInBackendLabel => this[nameof (EditImageInBackendLabel)];

    /// <summary>phrase: Manage also.</summary>
    [ResourceEntry("ManageAlso", Description = "phrase: Manage also", LastModified = "2013/09/25", Value = "Manage also")]
    public string ManageAlso => this[nameof (ManageAlso)];

    /// <summary>phrase: Comments for images.</summary>
    [ResourceEntry("CommentsForImages", Description = "phrase: Comments for images", LastModified = "2013/09/25", Value = "Comments for images")]
    public string CommentsForImages => this[nameof (CommentsForImages)];

    /// <summary>The 'Mb' unit.</summary>
    /// <value>Mb</value>
    [ResourceEntry("mb", Description = "The 'Mb' unit.", LastModified = "2014/01/23", Value = "Mb")]
    public string mb => this[nameof (mb)];

    /// <summary>phrase: This field is required</summary>
    /// <value>This field is required</value>
    [ResourceEntry("RequiredField", Description = "phrase: This field is required", LastModified = "2014/01/27", Value = "This field is required")]
    public string RequiredField => this[nameof (RequiredField)];

    /// <summary>Label text</summary>
    /// <value>As manually ordered</value>
    [ResourceEntry("AsManuallyOrdered", Description = "Label text", LastModified = "2014/04/23", Value = "As manually ordered")]
    public string AsManuallyOrdered => this[nameof (AsManuallyOrdered)];
  }
}
