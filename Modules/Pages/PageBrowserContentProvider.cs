// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageBrowserContentProvider
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
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web;
using Telerik.Web.UI;
using Telerik.Web.UI.Widgets;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Represents a class for populating the content of Page Browser control.
  /// </summary>
  public class PageBrowserContentProvider : SitefinityFileBrowserContentProviderBase
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
    public PageBrowserContentProvider(
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
      this.cacheKeyPrefix = "PageBrowser|";
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
    public override string CreateDirectory(string path, string name) => PageBrowserContentProvider.SaveDirectory(Guid.Empty, path, name, name, this.taxName, this.TaxonomyManager, this.PageManager);

    /// <summary>
    /// Creates a directory item in the given path with the given name.
    /// </summary>
    /// <param name="pageId">The taxon pageId.</param>
    /// <param name="path">The path where the directory item should be created.</param>
    /// <param name="name">The name of the new directory item.</param>
    /// <param name="urlName">The url name of the page.</param>
    /// <param name="taxName">The name of the taxonomy.</param>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    /// <param name="pageManager">The page manager.</param>
    /// <returns></returns>
    public static string SaveDirectory(
      Guid id,
      string path,
      string name,
      string urlName,
      string taxName,
      TaxonomyManager taxonomyManager,
      PageManager pageManager)
    {
      try
      {
        HierarchicalTaxon navigationTaxon = PageBrowserContentProvider.GetNavigationTaxon(path, taxName, taxonomyManager);
        if (!PageBrowserContentProvider.ValidateDuplicateUrl(id, urlName, navigationTaxon, pageManager))
          throw new ArgumentException(Res.Get<PageResources>().DuplicatedUrl);
        HierarchicalTaxon taxon;
        if (id == Guid.Empty)
        {
          taxon = taxonomyManager.CreateTaxon<HierarchicalTaxon>();
        }
        else
        {
          if (navigationTaxon.Id == id)
            throw new ArgumentException(Res.Get<PageResources>().InvalidParent);
          taxon = taxonomyManager.GetTaxon<HierarchicalTaxon>(id);
        }
        taxon.Name = urlName;
        taxon.Title = (Lstring) name;
        taxon.UrlName = (Lstring) urlName;
        navigationTaxon.Subtaxa.Add(taxon);
        TransactionManager.CommitTransaction("sitefinityPagePermissionsInheritanceTransactionName");
        return taxon.Id.ToString();
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return ex.Message;
        throw;
      }
    }

    /// <summary>
    /// Deletes the directory item with the given virtual path.
    /// </summary>
    /// <param name="path">The virtual path of the directory item.</param>
    /// <returns>
    /// string.Empty when the operation was successful; otherwise an error message token.
    /// </returns>
    public override string DeleteDirectory(string path)
    {
      try
      {
        HierarchicalTaxon navigationTaxon = PageBrowserContentProvider.GetNavigationTaxon(path, this.taxName, this.TaxonomyManager);
        this.DeleteTaxonPages(navigationTaxon.Id);
        this.DeleteTaxonAndPages(navigationTaxon.Subtaxa);
        this.TaxonomyManager.Delete((ITaxon) navigationTaxon);
        this.TaxonomyManager.SaveChanges();
        return string.Empty;
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          return ex.Message;
        throw;
      }
    }

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
        HierarchicalTaxon navigationTaxon = PageBrowserContentProvider.GetNavigationTaxon(path.Substring(0, num), this.taxName, this.TaxonomyManager);
        string urlName = path.Substring(num);
        Guid parentId = navigationTaxon.Id;
        this.PageManager.Delete(this.PageManager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == parentId && n.UrlName == (Lstring) urlName)).Single<PageNode>().GetPageData());
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
    protected virtual DirectoryItem ResolveDirectoryImpl(string path) => (DirectoryItem) null;

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
      bool suppressSecurityChecks = taxonomyManager.Provider.SuppressSecurityChecks;
      taxonomyManager.Provider.SuppressSecurityChecks = true;
      HierarchicalTaxonomy hierarchicalTaxonomy = taxonomyManager.GetTaxonomies<HierarchicalTaxonomy>().Single<HierarchicalTaxonomy>((Expression<Func<HierarchicalTaxonomy, bool>>) (t => t.Name == taxName));
      taxonomyManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
      IEnumerable<HierarchicalTaxon> source = hierarchicalTaxonomy.Taxa.Cast<HierarchicalTaxon>();
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

    /// <summary>Saves the page.</summary>
    /// <param name="pageId">The page pageId.</param>
    /// <param name="path">The path.</param>
    /// <param name="menuName">The page menu name.</param>
    /// <param name="urlName">The page url name.</param>
    /// <param name="showInNavigation">if set to <c>true</c> [show in navigation].</param>
    /// <param name="title">The page title.</param>
    /// <param name="description">The page description.</param>
    /// <param name="keywords">The page keywords.</param>
    /// <param name="taxName">Name of the taxon.</param>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    /// <param name="pageManager">The page manager.</param>
    /// <returns></returns>
    public static string SavePage(
      Guid pageId,
      string path,
      string menuName,
      string urlName,
      bool showInNavigation,
      string title,
      string description,
      string keywords,
      string taxName,
      TaxonomyManager taxonomyManager,
      PageManager pageManager)
    {
      throw new NotSupportedException();
    }

    /// <summary>
    /// Gets a value indicating whether the ContentProvider can create directory items or not. The visibility of the
    /// Create New Directory icon is controlled by the value of this property.
    /// </summary>
    /// <value></value>
    public override bool CanCreateDirectory => true;

    /// <summary>
    /// Validates for  duplicate URLs when create/edit section or page.
    /// </summary>
    /// <param name="pageId">The pageId.</param>
    /// <param name="urlName">Name of the URL.</param>
    /// <param name="parent">The parent.</param>
    /// <param name="pageManager">The page manager.</param>
    /// <returns></returns>
    public static bool ValidateDuplicateUrl(
      Guid id,
      string urlName,
      HierarchicalTaxon parent,
      PageManager pageManager)
    {
      Guid parentId = parent.Id;
      bool flag1;
      bool flag2;
      if (id == Guid.Empty)
      {
        flag1 = pageManager.GetPageNodes().Any<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == parentId && p.UrlName == (Lstring) urlName));
        flag2 = parent.Subtaxa.Any<HierarchicalTaxon>((Func<HierarchicalTaxon, bool>) (t => t.Name == urlName));
      }
      else
      {
        flag1 = pageManager.GetPageDataList().Any<PageData>((Expression<Func<PageData, bool>>) (x => x.NavigationNode.Id == parentId && x.NavigationNode.UrlName == (Lstring) urlName && x.Id != id));
        flag2 = parent.Subtaxa.Any<HierarchicalTaxon>((Func<HierarchicalTaxon, bool>) (t => t.Name == urlName && t.Id != id));
      }
      return !(flag1 | flag2);
    }

    private void DeleteTaxonPages(Guid id)
    {
      IQueryable<PageNode> pageNodes = this.PageManager.GetPageNodes();
      Expression<Func<PageNode, bool>> predicate = (Expression<Func<PageNode, bool>>) (n => n.Id == id);
      foreach (PageNode pageNode in (IEnumerable<PageNode>) pageNodes.Where<PageNode>(predicate))
      {
        this.PageManager.Delete(pageNode.GetPageData());
        this.PageManager.SaveChanges();
      }
    }

    private void DeleteTaxonAndPages(IList<HierarchicalTaxon> subtaxa)
    {
      foreach (HierarchicalTaxon hierarchicalTaxon in new List<HierarchicalTaxon>((IEnumerable<HierarchicalTaxon>) subtaxa))
      {
        this.DeleteTaxonPages(hierarchicalTaxon.Id);
        this.DeleteTaxonAndPages(hierarchicalTaxon.Subtaxa);
        this.TaxonomyManager.Delete((ITaxon) hierarchicalTaxon);
      }
    }
  }
}
