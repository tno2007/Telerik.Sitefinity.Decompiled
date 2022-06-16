// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.TemplateBrowserContentProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Represents a class for populating the content of Template Browser control.
  /// </summary>
  public class TemplateBrowserContentProvider : SitefinityFileBrowserContentProviderBase
  {
    private int cacheExp;
    private string cacheKeyPrefix;
    private string taxName;
    private PageManager pageManager;
    private TaxonomyManager taxonomyManager;
    private SitefinityIdentity user;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageBrowserContentProvider" /> class.
    /// </summary>
    /// <param name="context">The current HttpContext.</param>
    /// <param name="searchPatterns">Search patterns for files. Allows wildcards.</param>
    /// <param name="viewPaths">The paths which will be displayed in the FileManager. You can disregard
    /// this value if you have custom mechanism for determining the rights for directory / file displaying.</param>
    /// <param name="uploadPaths">The paths which will allow uploading in the FileManager. You can disregard this
    /// value if you have custom mechanism for determining the rights for uploading.</param>
    /// <param name="deletePaths">The paths which will allow deleting in the dialog. You can disregard this
    /// value if you have custom mechanism for determining the rights for deleting.</param>
    /// <param name="selectedUrl">The selected url in the file browser. The file browser will navigate to the item
    /// which has this url.</param>
    /// <param name="selectedItemTag">The selected tag in the file browser. The file browser will navigate to the
    /// item which has this tag.</param>
    public TemplateBrowserContentProvider(
      HttpContext context,
      string[] searchPatterns,
      string[] viewPaths,
      string[] uploadPaths,
      string[] deletePaths,
      string selectedUrl,
      string selectedItemTag)
      : base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
    {
      string providerName1 = (string) context.Items[(object) "SfCurrentPagesProvider"];
      if (providerName1 == null)
        throw new ArgumentException("The key \"SfCurrentPagesProvider\" is not specified in the current context.");
      string providerName2 = (string) context.Items[(object) "SfCurrentTaxonomyProvider"];
      if (providerName2 == null)
        throw new ArgumentException("The key \"SfCurrentTaxonomyProvider\" is not specified in the current context.");
      this.taxName = (string) context.Items[(object) "SfCurrentTaxonomyName"];
      if (string.IsNullOrEmpty(this.taxName))
        throw new ArgumentException("The key \"SfCurrentTaxonomyName\" is not specified in the current context.");
      object obj = context.Items[(object) "SfCacheExpiration"];
      this.cacheExp = obj != null ? (int) obj : 3;
      this.pageManager = PageManager.GetManager(providerName1);
      this.taxonomyManager = TaxonomyManager.GetManager(providerName2);
      this.user = ClaimsManager.GetCurrentIdentity();
      this.cacheKeyPrefix = "TemplateBrowser|";
      this.cacheKeyPrefix = this.cacheKeyPrefix + SystemManager.CurrentContext.Culture.Name + "|";
      if (this.user.IsUnrestricted)
        return;
      this.cacheKeyPrefix = this.cacheKeyPrefix + this.user.Name + "|";
    }

    /// <summary>Gets the taxonomy manager.</summary>
    /// <value>The taxonomy manager.</value>
    protected TaxonomyManager TaxonomyManager => this.taxonomyManager;

    /// <summary>Gets the page manager.</summary>
    /// <value>The page manager.</value>
    protected PageManager PageManager => this.pageManager;

    /// <summary>
    /// Creates a directory item in the given path with the given name.
    /// </summary>
    /// <param name="path">The path where the directory item should be created.</param>
    /// <param name="name">The name of the new directory item.</param>
    /// <returns>
    /// string.Empty when the operation was successful; otherwise an error message token.
    /// </returns>
    public override string CreateDirectory(string path, string name) => throw new NotImplementedException();

    /// <summary>
    /// Deletes the directory item with the given virtual path.
    /// </summary>
    /// <param name="path">The virtual path of the directory item.</param>
    /// <returns>
    /// string.Empty when the operation was successful; otherwise an error message token.
    /// </returns>
    public override string DeleteDirectory(string path) => throw new NotImplementedException();

    /// <summary>Deletes the file item with the given virtual path.</summary>
    /// <param name="path">The virtual path of the file item.</param>
    /// <returns>
    /// string.Empty when the operation was successful; otherwise an error message token.
    /// </returns>
    public override string DeleteFile(string path)
    {
      try
      {
        int num = path.LastIndexOf("/") + 1;
        HierarchicalTaxon navigationTaxon = TemplateBrowserContentProvider.GetNavigationTaxon(path.Substring(0, num), this.taxName, this.TaxonomyManager);
        string title = path.Substring(num);
        PageTemplate pageTemplate;
        if (navigationTaxon != null)
        {
          Guid category = navigationTaxon.Id;
          pageTemplate = this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category == category && t.Title == (Lstring) title)).Single<PageTemplate>();
        }
        else
          pageTemplate = this.PageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Title == (Lstring) title)).Single<PageTemplate>();
        this.PageManager.Delete(pageTemplate);
        this.PageManager.SaveChanges();
        return string.Empty;
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return ex.Message;
        throw;
      }
    }

    /// <summary>
    /// Gets a read only Stream for accessing the file item with the given url.
    /// </summary>
    /// <param name="url">The url of the file.</param>
    /// <returns>
    /// Stream for accessing the contents of the file item with the given url.
    /// </returns>
    public override Stream GetFile(string url) => throw new NotImplementedException();

    /// <summary>Get the name of the file with the given url.</summary>
    /// <param name="url">The url of the file.</param>
    /// <returns>String containing the file name.</returns>
    public override string GetFileName(string url) => throw new NotImplementedException();

    /// <summary>Gets the virtual path of the item with the given url.</summary>
    /// <param name="url">The url of the item.</param>
    /// <returns>String containing the path of the item.</returns>
    public override string GetPath(string url) => url.Substring(0, url.LastIndexOf('/') + 1);

    /// <summary>Resolves a directory with the given path.</summary>
    /// <param name="path">The virtual path of the directory.</param>
    /// <returns>A DirectoryItem, containing the directory.</returns>
    /// <remarks>Used mainly in the Ajax calls.</remarks>
    public override DirectoryItem ResolveDirectory(string path) => this.ResolveDirectoryImpl(path);

    /// <summary>
    /// Resolves a root directory with the given path in tree mode.
    /// </summary>
    /// <param name="path">The virtual path of the directory.</param>
    /// <returns>A DirectoryItem, containing the root directory.</returns>
    public override DirectoryItem ResolveRootDirectoryAsTree(string path) => this.ResolveDirectoryImpl(path);

    /// <summary>Resolves the directory.</summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    protected virtual DirectoryItem ResolveDirectoryImpl(string path)
    {
      string key = this.cacheKeyPrefix + path.ToUpperInvariant();
      DirectoryItem directoryItem = (DirectoryItem) this.Cache[key];
      if (directoryItem == null)
      {
        if (path.ToLower().StartsWith("all"))
        {
          Guid backendTemplatesCategoryId = SiteInitializer.BackendTemplatesCategoryId;
          IOrderedQueryable<PageTemplate> templates = this.pageManager.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category != backendTemplatesCategoryId)).OrderBy<PageTemplate, short>((Expression<Func<PageTemplate, short>>) (t => t.Ordinal));
          FileItem[] childFiles = TemplateBrowserContentProvider.GetChildFiles(this.user, string.Empty, this.PageManager, this.TaxonomyManager, (IQueryable<PageTemplate>) templates);
          PathPermissions permissions = PathPermissions.Read | PathPermissions.Upload | PathPermissions.Delete;
          directoryItem = new DirectoryItem(Res.Get<PageResources>().AllTemplates, string.Empty, path, string.Empty, permissions, childFiles, new DirectoryItem[0]);
        }
        else
        {
          HierarchicalTaxon navigationTaxon = TemplateBrowserContentProvider.GetNavigationTaxon(path, this.taxName, this.TaxonomyManager);
          if (navigationTaxon != null)
          {
            directoryItem = TemplateBrowserContentProvider.GetDirectoryItem(this.user, navigationTaxon, path, this.PageManager, this.TaxonomyManager);
            this.Cache.Add(key, (object) directoryItem, CacheItemPriority.Low, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes((double) this.cacheExp)), (ICacheItemExpiration) new DataItemCacheDependency((IDataItem) navigationTaxon));
          }
        }
      }
      return directoryItem;
    }

    /// <summary>Gets the navigation taxon.</summary>
    /// <param name="path">The path.</param>
    /// <param name="taxName">Name of the taxonomy.</param>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    /// <returns></returns>
    public static HierarchicalTaxon GetNavigationTaxon(
      string path,
      string taxName,
      TaxonomyManager taxonomyManager)
    {
      IList<string> pathSegmentStrings = RouteHelper.SplitUrlToPathSegmentStrings(path, true);
      HierarchicalTaxon navigationTaxon = (HierarchicalTaxon) null;
      IEnumerable<HierarchicalTaxon> source = taxonomyManager.GetTaxonomies<HierarchicalTaxonomy>().Single<HierarchicalTaxonomy>((Expression<Func<HierarchicalTaxonomy, bool>>) (t => t.Name == taxName)).Taxa.OrderBy<Taxon, float>((Func<Taxon, float>) (t => t.Ordinal)).ToList<Taxon>().Cast<HierarchicalTaxon>();
      for (int index = 0; index < pathSegmentStrings.Count; ++index)
      {
        string segment = pathSegmentStrings[index];
        navigationTaxon = source.SingleOrDefault<HierarchicalTaxon>((Func<HierarchicalTaxon, bool>) (t => t.Name == segment));
        if (navigationTaxon == null)
          return (HierarchicalTaxon) null;
        source = (IEnumerable<HierarchicalTaxon>) navigationTaxon.Subtaxa;
      }
      return navigationTaxon;
    }

    /// <summary>Saves the template.</summary>
    /// <param name="templateId">The template pageId.</param>
    /// <param name="category">The template category.</param>
    /// <param name="title">The template title.</param>
    /// <param name="showInNavigation">if set to <c>true</c> [show in navigation].</param>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    /// <param name="pageManager">The page manager.</param>
    /// <returns></returns>
    public static string SaveTemplate(
      Guid templateId,
      Guid category,
      string title,
      bool showInNavigation,
      TaxonomyManager taxonomyManager,
      PageManager pageManager)
    {
      try
      {
        PageTemplate template;
        if (templateId == Guid.Empty)
        {
          if (pageManager.GetTemplates().Any<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category == category && t.Title == (Lstring) title)))
            throw new ArgumentException(Res.Get<PageResources>().DuplicatedName);
          template = pageManager.CreateTemplate();
        }
        else
        {
          if (pageManager.GetTemplates().Any<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Category == category && t.Title == (Lstring) title && t.Id != templateId)))
            throw new ArgumentException(Res.Get<PageResources>().DuplicatedName);
          template = pageManager.GetTemplate(templateId);
        }
        if (category != Guid.Empty)
          template.Category = category;
        template.Title = (Lstring) title;
        template.ShowInNavigation = showInNavigation;
        TransactionManager.CommitTransaction("sitefinityPagePermissionsInheritanceTransactionName");
        return template.Id.ToString();
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return ex.Message;
        throw;
      }
    }

    /// <summary>Stores an image with the given url and image format.</summary>
    /// <param name="bitmap">The Bitmap object to be stored.</param>
    /// <param name="url">The url of the bitmap.</param>
    /// <param name="format">The image format of the bitmap.</param>
    /// <returns>
    /// string.Empty when the operation was successful; otherwise an error message token.
    /// </returns>
    /// <remarks>
    /// Used when creating thumbnails in the ImageManager dialog.
    /// </remarks>
    public override string StoreBitmap(Bitmap bitmap, string url, ImageFormat format) => throw new NotImplementedException();

    /// <summary>
    /// Creates a file item from a Telerik.Web.UI.UploadedFile in the given path with the given name.
    /// </summary>
    /// <param name="file">The UploadedFile instance to store.</param>
    /// <param name="path">The virtual path where the file item should be created.</param>
    /// <param name="name">The name of the file item.</param>
    /// <param name="arguments">Additional values to be stored such as Description, DisplayName, etc.</param>
    /// <returns>
    /// String containing the full virtual path (including the file name) of the file item.
    /// </returns>
    /// <remarks>
    /// The default FileUploader control does not include the arguments parameter. If you need additional arguments
    /// you should create your own FileUploader control.
    /// </remarks>
    public override string StoreFile(
      UploadedFile file,
      string path,
      string name,
      params string[] arguments)
    {
      throw new NotImplementedException();
    }

    protected virtual ICacheManager Cache => SystemManager.GetCacheManager(CacheManagerInstance.SiteMap);

    private static DirectoryItem GetDirectoryItem(
      SitefinityIdentity user,
      HierarchicalTaxon node,
      string path,
      PageManager pageManager,
      TaxonomyManager taxonomyManager)
    {
      return (DirectoryItem) null;
    }

    private static bool IsAccessibleToUser(
      SitefinityIdentity user,
      ISecuredObject secured,
      out PathPermissions permissions)
    {
      if (user.IsUnrestricted)
      {
        permissions = PathPermissions.Read | PathPermissions.Upload | PathPermissions.Delete;
        return true;
      }
      permissions = PathPermissions.Read;
      bool user1 = secured.IsGranted("PageTemplates", "View");
      if (user1)
      {
        if (secured.IsGranted("PageTemplates", "Create"))
          permissions |= PathPermissions.Upload;
      }
      if (user1)
      {
        if (secured.IsGranted("PageTemplates", "Delete"))
          permissions |= PathPermissions.Delete;
      }
      return user1;
    }

    private static FileItem[] GetChildFiles(
      SitefinityIdentity user,
      string nodeTitle,
      PageManager pageManager,
      TaxonomyManager taxonomyManager,
      IQueryable<PageTemplate> templates)
    {
      string browserDateFormat = Config.Get<PagesConfig>().PageBrowserDateFormat;
      List<FileItem> fileItemList = new List<FileItem>();
      foreach (PageTemplate template1 in (IEnumerable<PageTemplate>) templates)
      {
        PageTemplate template = template1;
        PathPermissions permissions;
        if (TemplateBrowserContentProvider.IsAccessibleToUser(user, (ISecuredObject) template, out permissions))
        {
          string url = RouteHelper.ResolveUrl("/Sitefinity/Template/" + (object) template.Id, UrlResolveOptions.Rooted);
          FileItem fileItem = new FileItem((string) template.Title, string.Empty, 0L, string.Empty, url, string.Empty, permissions);
          fileItem.Attributes.Add("Id", template.Id.ToString());
          if (template.ParentTemplate != null)
          {
            fileItem.Attributes.Add("ParentTemplate", (string) template.ParentTemplate.Title);
            string str = RouteHelper.ResolveUrl("/Sitefinity/Template/" + (object) template.ParentTemplate.Id, UrlResolveOptions.Rooted);
            fileItem.Attributes.Add("ParentTemplateUrl", str);
          }
          else if (!string.IsNullOrEmpty(template.MasterPage))
            fileItem.Attributes.Add("ParentTemplate", template.MasterPage);
          string str1 = nodeTitle;
          if (string.IsNullOrEmpty(str1))
          {
            Guid category = template.Category;
            ITaxon taxon = taxonomyManager.GetTaxon(category);
            if (taxon != null)
              str1 = (string) taxon.Title;
          }
          fileItem.Attributes.Add("Category", str1);
          Guid id = template.Id;
          int num = pageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Template.Id == template.Id)).Count<PageData>();
          if (num == 1)
            fileItem.Attributes.Add("PageCount", string.Format(Res.Get<PageResources>().PageCount, (object) num));
          else
            fileItem.Attributes.Add("PageCount", string.Format(Res.Get<PageResources>().PagesCount, (object) num));
          if (template.Owner != Guid.Empty)
          {
            User user1 = SecurityManager.GetUser(template.Owner);
            if (user1 != null)
              fileItem.Attributes.Add("Author", user1.UserName);
          }
          fileItem.Attributes.Add("Date", template.LastModified.ToString(browserDateFormat));
          fileItem.Attributes.Add("DeleteCommand", Res.Get<PageResources>().DeleteCommandName);
          fileItem.Attributes.Add("TemplatePropertiesCommand", Res.Get<PageResources>().PropertiesCommandName);
          fileItem.Attributes.Add("ContentCommand", string.Format("{0}:" + url, (object) Res.Get<PageResources>().ContentCommandName));
          fileItem.Attributes.Add("PermissionsCommand", Res.Get<PageResources>().PermissionsCommandName);
          fileItem.Attributes.Add("ParentTemplateCommand", Res.Get<PageResources>().ParentTemplateCommandName);
          fileItemList.Add(fileItem);
        }
      }
      return fileItemList.ToArray();
    }

    /// <summary>
    /// Use <see cref="!:LocalizationKeys" /> to return a dictionary of override localization
    /// messages or null to return none.
    /// </summary>
    /// <returns>
    /// Dictionary of overridden messages or null if none are overridden
    /// </returns>
    public override Dictionary<SitefinityFileBrowserContentProviderBase.LocalizationKeys, string> GetOverriddenLocalizationMessages() => new Dictionary<SitefinityFileBrowserContentProviderBase.LocalizationKeys, string>()
    {
      [SitefinityFileBrowserContentProviderBase.LocalizationKeys.ConfirmDelete] = Res.Get<PageResources>().DeletePageTemplateConfirm
    };
  }
}
