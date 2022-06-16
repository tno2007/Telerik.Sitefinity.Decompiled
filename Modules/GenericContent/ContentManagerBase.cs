// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentManagerBase`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.DataSource;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Packaging;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>Represents base implementation of Content Manager.</summary>
  /// <typeparam name="TProvider">The type of the provider.</typeparam>
  public abstract class ContentManagerBase<TProvider> : 
    ManagerBase<TProvider>,
    IDataSource,
    ICommentsManager,
    IContentManagerExtended,
    IContentManager,
    IManager,
    IDisposable,
    IProviderResolver,
    IRelatedDataSource
    where TProvider : ContentDataProviderBase
  {
    private Type moduleConfigType;

    static ContentManagerBase()
    {
      ManagerBase<TProvider>.Executing += new EventHandler<ExecutingEventArgs>(ContentManagerBase<TProvider>.Provider_Executing);
      ManagerBase<TProvider>.Executed += new EventHandler<ExecutedEventArgs>(ContentManagerBase<TProvider>.Provider_Executed);
    }

    /// <summary>
    /// Initializes a new instance of ManagerBase class with the default provider.
    /// </summary>
    protected ContentManagerBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of ManagerBase class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">The name of the provider. If empty string or null the default provider is set</param>
    protected ContentManagerBase(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.ContentManagerBase`1" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    protected ContentManagerBase(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>
    /// Gets the URL of the content item for the current UI culture.
    /// </summary>
    /// <param name="item">The content item.</param>
    /// <returns>The URL for the item.</returns>
    public virtual string GetItemUrl(ILocatable item) => this.Provider.GetItemUrl(item);

    /// <summary>Gets the first item that matches the specified URL.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="url">The URL to match.</param>
    /// <param name="redirectUrl">The URL to redirect to if there is newer URL.</param>
    /// <returns>The content item that matches the URL.</returns>
    public virtual T GetItemFromUrl<T>(string url, out string redirectUrl) where T : ILocatable => this.Provider.GetItemFromUrl<T>(url, out redirectUrl);

    /// <summary>Gets the first item that matches the specified URL.</summary>
    /// <param name="itemType">Type of the content item.</param>
    /// <param name="url">The URL to match.</param>
    /// <param name="redirectUrl">The URL to redirect to if there is newer URL.</param>
    /// <returns>The content item that matches the URL.</returns>
    public virtual IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      out string redirectUrl)
    {
      return this.Provider.GetItemFromUrl(itemType, url, out redirectUrl);
    }

    /// <summary>
    /// Retrieve a content item by its url, optionally returning only items that are visible on the public side
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="published">If true, will get only Published/Scheduled items - those that are typically visible on the public side.</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public virtual IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return this.Provider.GetItemFromUrl(itemType, url, published, out redirectUrl);
    }

    /// <summary>
    /// Retrieve a content item by its url, optionally returning only items that are visible on the public side
    /// </summary>
    /// <typeparam name="T">Type of the item to get</typeparam>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="published">If true, will get only Published/Scheduled items - those that are typically visible on the public side.</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public virtual IDataItem GetItemFromUrl<T>(
      string url,
      bool published,
      out string redirectUrl)
      where T : IDataItem, ILocatable
    {
      return this.Provider.GetItemFromUrl<T>(url, published, out redirectUrl);
    }

    /// <summary>Recompiles the URLs of the item.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The content item.</param>
    /// <remarks>
    /// Adds UrlData to the urls field of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.ILocatable" /> compiled from the item's Provider urlFormat.
    /// </remarks>
    public virtual void RecompileItemUrls<T>(T item) where T : ILocatable => this.Provider.RecompileItemUrls<T>(item);

    /// <summary>
    /// Adds an <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> item to the current URLs collection for this item.
    /// </summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The content item.</param>
    /// <param name="url">The URL string value that should be added.</param>
    public virtual void AddItemUrl<T>(T item, string url, bool isDefault = true, bool redirectToDefault = false) where T : ILocatable => this.Provider.AddItemUrl<T>(item, url, isDefault, redirectToDefault);

    /// <summary>Clears the Urls collection for this item.</summary>
    /// <typeparam name="TItem"></typeparam>
    /// <param name="item"></param>
    /// <param name="excludeDefault">if set to <c>true</c> default urls will not be cleared.</param>
    public void ClearItemUrls<TItem>(TItem item, bool excludeDefault = false) where TItem : ILocatable => this.Provider.ClearItemUrls<TItem>(item, excludeDefault);

    /// <summary>
    /// Removes all urls from the item satisfying the condition that is checked in the predicate function.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    public virtual void RemoveItemUrls<TItem>(TItem item, Func<UrlData, bool> predicate) where TItem : ILocatable => this.Provider.RemoveItemUrls<TItem>(item, predicate);

    /// <summary>Creates new UrlData.</summary>
    /// <returns>The new content item.</returns>
    public virtual T CreateUrl<T>() where T : UrlData, new() => this.Provider.CreateUrl<T>();

    /// <summary>Creates new UrlData with the specified ID.</summary>
    /// <param name="id">The id of the new UrlData.</param>
    /// <returns>The new UrlData.</returns>
    public virtual T CreateUrl<T>(Guid id) where T : UrlData, new() => this.Provider.CreateUrl<T>(id);

    /// <summary>Gets a UrlData with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A UrlData entry.</returns>
    public virtual T GetUrl<T>(Guid id) where T : UrlData => this.Provider.GetUrl<T>(id);

    /// <summary>Gets a query for UrlData.</summary>
    /// <returns>The query for UrlData.</returns>
    public virtual IQueryable<T> GetUrls<T>() where T : UrlData => this.Provider.GetUrls<T>();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The UrlData to delete.</param>
    public virtual void Delete(UrlData item) => this.Provider.Delete(item);

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    [Obsolete("This feature is not available.")]
    public virtual void BlockCommentsForEmail(string email) => this.Provider.BlockCommentsForEmail(email);

    /// <summary>Blocks the comments coming from the given IP address.</summary>
    /// <param name="ip">The IP address.</param>
    [Obsolete("This feature is not available.")]
    public virtual void BlockCommentsForIP(string ip) => this.Provider.BlockCommentsForIP(ip);

    /// <summary>Creates a comment for the specified commented item.</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public virtual Comment CreateComment(ICommentable commentedItem) => this.Provider.CreateComment(commentedItem);

    /// <summary>
    /// Creates a comment for the commented item with the given type and id.
    /// </summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public virtual Comment CreateComment(Type commentedItemType, Guid commentedItemId) => this.Provider.CreateComment(commentedItemType, commentedItemId);

    /// <summary>Deletes the specified comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().DeleteComment")]
    public virtual void Delete(Comment comment) => this.Provider.Delete(comment);

    /// <summary>Gets the comment by the specified id.</summary>
    /// <param name="commentId">The comment id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().GetComment")]
    public virtual Comment GetComment(Guid commentId) => this.Provider.GetComment(commentId);

    /// <summary>Gets an IQueryable of comments.</summary>
    [Obsolete("Use SystemManager.GetCommentService().GetComments")]
    public virtual IQueryable<Comment> GetComments() => this.Provider.GetComments();

    /// <summary>Gets an IQueryable of comments.</summary>
    [Obsolete("Use SystemManager.GetCommentService().GetComments")]
    internal IQueryable<Comment> GetComments_Old() => this.Provider.GetComments_Old();

    /// <summary>Gets the commented item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns></returns>
    public ICommentable GetCommentedItem(Type itemType, Guid itemId)
    {
      ICommentable commentedItem = (ICommentable) null;
      if (itemId != Guid.Empty)
        commentedItem = (ICommentable) this.GetItem(itemType, itemId);
      return commentedItem;
    }

    /// <summary>Gets the commented item.</summary>
    /// <param name="itemTypeName">Full Name of the type of the item.</param>
    /// <param name="itemId">The item id.</param>
    /// <returns></returns>
    public ICommentable GetCommentedItem(string itemTypeName, Guid itemId)
    {
      if (itemId != Guid.Empty)
      {
        Type itemType = TypeResolutionService.ResolveType(itemTypeName, false);
        if (itemType != (Type) null)
          return (ICommentable) this.GetItem(itemType, itemId);
      }
      return (ICommentable) null;
    }

    /// <summary>
    /// Provides functionality to evaluate queries against a content data source with
    /// specified type of the data.
    /// </summary>
    /// <typeparam name="TItem">The type of the content item to be retrieved.</typeparam>
    /// <returns>The type of the data in the data source.</returns>
    public abstract IQueryable<TItem> GetItems<TItem>() where TItem : IContent;

    private bool CanManageProviders(out Type moduleConfigType)
    {
      if (typeof (ConfigSection).IsAssignableFrom(this.ModuleConfigType) && typeof (IModuleConfig).IsAssignableFrom(this.ModuleConfigType))
      {
        moduleConfigType = this.ModuleConfigType;
        return true;
      }
      moduleConfigType = (Type) null;
      return false;
    }

    private Type ModuleConfigType
    {
      get
      {
        if (this.moduleConfigType == (Type) null)
        {
          this.moduleConfigType = this.GetConfigType();
          if (this.moduleConfigType == (Type) null)
            this.moduleConfigType = typeof (object);
        }
        return this.moduleConfigType;
      }
    }

    /// <summary>Gets the provider name default prefix.</summary>
    /// <value>The provider name default prefix.</value>
    public virtual string ProviderNameDefaultPrefix => "provider";

    /// <summary>Gets the type of the config for this manager.</summary>
    /// <returns></returns>
    protected virtual Type GetConfigType()
    {
      if (!(this.ProvidersSettings.Section is IModuleConfig moduleConfig) && !this.ModuleName.IsNullOrEmpty() && SystemManager.GetModule(this.ModuleName) is ModuleBase module)
        moduleConfig = module.ModuleConfig as IModuleConfig;
      return moduleConfig?.GetType();
    }

    [Obsolete("Use CreateProvider(providerName, providerTitle, parameters) instead.")]
    public virtual string CreateProvider(string providerTitle, NameValueCollection parameters)
    {
      Type moduleConfigType;
      if (!this.CanManageProviders(out moduleConfigType))
        throw new NotSupportedException("You should implement CreateProvider for {0}".Arrange((object) this.GetType().FullName));
      return this.CreateProvider(ManagerExtensions.GenerateProviderName(this.ProviderNameDefaultPrefix, (ConfigManager.GetManager().GetSection(moduleConfigType.Name) as IModuleConfig).Providers.Values.Select<DataProviderSettings, string>((Func<DataProviderSettings, string>) (p => p.Name))), providerTitle, parameters);
    }

    /// <inheritdoc />
    public virtual string CreateProvider(
      string providerName,
      string providerTitle,
      NameValueCollection parameters)
    {
      Type moduleConfigType;
      if (!this.CanManageProviders(out moduleConfigType))
        throw new NotSupportedException("You should implement CreateProvider for {0}".Arrange((object) this.GetType().FullName));
      this.RegisterProvider<TProvider>(moduleConfigType, providerName, providerTitle, parameters, new System.Action<IModuleConfig, DataProviderSettings>(this.ApplyProviderDefaultSettings));
      return providerName;
    }

    /// <summary>
    /// Applies the default provider settings. Called form CreateProvider() method before the a new provider is created.
    /// Override when you need to apply additional parameters to the provider.
    /// </summary>
    /// <param name="providerSettings">The provider settings.</param>
    protected virtual void ApplyProviderDefaultSettings(
      IModuleConfig config,
      DataProviderSettings providerSettings)
    {
    }

    /// <inheritdoc />
    public virtual void DeleteProvider(string providerName)
    {
      Type moduleConfigType;
      if (!this.CanManageProviders(out moduleConfigType))
        throw new NotSupportedException("You should implement DeleteProvider for {0}".Arrange((object) this.GetType().FullName));
      ConfigManager manager = ConfigManager.GetManager();
      IModuleConfig section = manager.GetSection(moduleConfigType.Name) as IModuleConfig;
      section.Providers.Remove(providerName);
      manager.SaveSection((ConfigSection) section);
      this.RemoveProvider(providerName);
    }

    /// <inheritdoc />
    public virtual void EnableProvider(string providerName)
    {
      Type moduleConfigType;
      if (!this.CanManageProviders(out moduleConfigType))
        throw new NotSupportedException("You should implement EnableProvider for {0}".Arrange((object) this.GetType().FullName));
      this.SetProviderState(true, providerName, moduleConfigType);
    }

    /// <inheritdoc />
    public virtual void DisableProvider(string providerName)
    {
      Type moduleConfigType;
      if (!this.CanManageProviders(out moduleConfigType))
        throw new NotSupportedException("You should implement DisableProvider for {0}".Arrange((object) this.GetType().FullName));
      this.SetProviderState(false, providerName, moduleConfigType);
    }

    /// <inheritdoc />
    public string Name => this.GetType().FullName;

    /// <inheritdoc />
    public string Title => this.ModuleName;

    /// <inheritdoc />
    public virtual bool CanCreateProvider => this.CanManageProviders(out Type _);

    /// <inheritdoc />
    public IEnumerable<DataProviderInfo> ProviderInfos => this.GetProviderInfos<TProvider>(this.StaticProviders.Where<TProvider>((Func<TProvider, bool>) (p => !p.IsSystemProvider())));

    /// <inheritdoc />
    public IEnumerable<DataProviderInfo> AllProviders
    {
      get
      {
        Type moduleConfigType;
        return this.CanManageProviders(out moduleConfigType) ? this.GetAllProviderInfos<TProvider>(moduleConfigType.Name) : Enumerable.Empty<DataProviderInfo>();
      }
    }

    IEnumerable<DataProviderInfo> IDataSource.Providers => this.AllProviders;

    /// <summary>Enables or disables a provider.</summary>
    /// <param name="enable">Determines whether to enable the provider (true) or disable it (false).</param>
    /// <param name="providerName">Name of the provider.</param>
    private void SetProviderState(bool enable, string providerName, Type configType)
    {
      ConfigManager manager = ConfigManager.GetManager();
      IModuleConfig section = manager.GetSection(configType.Name) as IModuleConfig;
      if (!section.Providers.Keys.Contains(providerName) || section.Providers[providerName].Enabled == enable)
        return;
      section.Providers[providerName].Enabled = enable;
      manager.SaveSection((ConfigSection) section);
      if (enable)
        this.InstatiateProvider((IDataProviderSettings) section.Providers[providerName]);
      else
        this.RemoveProvider(providerName);
    }

    /// <summary>
    /// An array of depentant data source names that this data source depends on.
    /// For example Ecommerce Orders is always selected together with Ecommerce Products.
    /// </summary>
    /// <value></value>
    public string[] DependantDataSources { get; set; }

    public string GetDefaultProvider() => ManagerBase<TProvider>.GetDefaultProviderName();

    /// <summary>Creates a comment for the specified commented item</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <param name="commentId">The comment item id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public Comment CreateComment(ICommentable commentedItem, Guid commentId) => this.Provider.CreateComment(commentedItem, commentId);

    /// <summary>
    /// Creates a comment for the commented item with the given type and id.
    /// </summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item id.</param>
    /// <param name="commentId">The comment id.</param>
    /// <returns></returns>
    [Obsolete("Use SystemManager.GetCommentService().CreateComment")]
    public Comment CreateComment(
      Type commentedItemType,
      Guid commentedItemId,
      Guid commentId)
    {
      return this.Provider.CreateComment(commentedItemType, commentedItemId, commentId);
    }

    /// <summary>Recompiles the and validate urls.</summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="content">The content.</param>
    public virtual void RecompileAndValidateUrls<TLocatable>(TLocatable content) where TLocatable : ILocatable
    {
      this.Provider.RecompileItemUrls<TLocatable>(content);
      this.ValidateUrlConstraints<TLocatable>(content);
    }

    /// <summary>Validates the URL constraints.</summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="item">The item.</param>
    public virtual void ValidateUrlConstraints<TLocatable>(TLocatable item) where TLocatable : ILocatable
    {
      Guid id = item.Id;
      List<UrlData> list = item.Urls.ToList<UrlData>();
      if (list.Count <= 0)
        return;
      Type urlTypeFor = this.Provider.GetUrlTypeFor(item.GetType());
      foreach (UrlData urlData1 in list)
      {
        UrlData urlData = urlData1;
        string url = urlData.Url;
        IEnumerable<UrlData> source = (IEnumerable<UrlData>) this.Provider.GetUrls(urlTypeFor).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Url == url && u.Culture == urlData.Culture && u.Parent.Id != id)).ToList<UrlData>();
        ILifecycleDataItemGeneric lifecycleItem = (object) item as ILifecycleDataItemGeneric;
        if (lifecycleItem != null)
        {
          source = source.Where<UrlData>((Func<UrlData, bool>) (u => u.Parent != null && ((ILifecycleDataItem) u.Parent).Status == ContentLifecycleStatus.Master));
          if (((ILifecycleDataItem) (object) item).Status != ContentLifecycleStatus.Master)
            source = source.Where<UrlData>((Func<UrlData, bool>) (u => u.Parent.Id != lifecycleItem.OriginalContentId));
        }
        else if ((object) item is Content)
        {
          Content contentItem = (object) item as Content;
          source = source.Where<UrlData>((Func<UrlData, bool>) (u => u.Parent != null && ((Content) u.Parent).Status == ContentLifecycleStatus.Master));
          if (contentItem.Status != ContentLifecycleStatus.Master)
            source = source.Where<UrlData>((Func<UrlData, bool>) (u => ((Content) u.Parent).Id != contentItem.OriginalContentId));
        }
        if (source.FirstOrDefault<UrlData>() != null)
        {
          if (item.AutoGenerateUniqueUrl)
          {
            this.provider.Delete(urlData);
            string title = (object) item is IHasTitle ? ((IHasTitle) (object) item).GetTitle() : string.Empty;
            item.UrlName = (Lstring) (CommonMethods.TitleToUrl(title) + item.Id.ToString("N"));
            this.RecompileItemUrls<TLocatable>(item);
          }
          else
            this.ThrowDuplicateUrlException(url);
        }
      }
    }

    /// <summary>Changes the item parent.</summary>
    /// <param name="item">The item.</param>
    /// <param name="newParent"></param>
    public void ChangeItemParent(Content item, Content newParent, bool recompileUrls)
    {
      if (this is ILifecycleManager lifecycleManager && item.Status != ContentLifecycleStatus.Temp && item.Status != ContentLifecycleStatus.PartialTemp)
      {
        ILifecycleDecorator lifecycle = lifecycleManager.Lifecycle;
        if (lifecycle.GetMaster((ILifecycleDataItem) item) is Content master)
          this.EnsureItemParent(master, newParent, recompileUrls);
        if (lifecycle.GetTemp((ILifecycleDataItem) item) is Content temp)
          this.EnsureItemParent(temp, newParent, recompileUrls);
        if (!(lifecycle.GetLive((ILifecycleDataItem) item) is Content live))
          return;
        this.EnsureItemParent(live, newParent, recompileUrls);
      }
      else
        this.EnsureItemParent(item, newParent, recompileUrls);
    }

    /// <summary>
    /// Ensures the item parent is set to the specified new parent.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="newParent">The new parent.</param>
    /// <param name="recompileUrls">if set to <c>true</c> [recompile urls].</param>
    /// <returns>True - if the parent is changed, otherwise false</returns>
    protected virtual bool EnsureItemParent(Content item, Content newParent, bool recompileUrls)
    {
      if (!(item is IHasParent))
        return false;
      IHasParent hasParent = item as IHasParent;
      int num = hasParent.Parent == null ? 1 : (hasParent.Parent.Id != newParent.Id ? 1 : 0);
      hasParent.Parent = newParent;
      if (num == 0)
        return num != 0;
      ILocatable content = item as ILocatable;
      if (!recompileUrls)
        return num != 0;
      if (content == null)
        return num != 0;
      this.RecompileAndValidateUrls<ILocatable>(content);
      return num != 0;
    }

    protected virtual void CopyUrls<T>(
      Content source,
      Content destination,
      IList<T> sourceUrls,
      IList<T> destinationUrls)
      where T : UrlData, new()
    {
      if (this is ILifecycleManager lifecycleManager)
      {
        IExtendedLifecycleDecorator lifecycle = lifecycleManager.Lifecycle as IExtendedLifecycleDecorator;
        ILifecycleDataItem lifecycleDataItem = destination as ILifecycleDataItem;
        ILifecycleDataItem source1 = source as ILifecycleDataItem;
        if (lifecycle != null && source1 != null && lifecycleDataItem != null && lifecycle.GetCopyOptions(source1, lifecycleDataItem) != CopyOptions.AllFields)
        {
          CultureInfo culture = SystemManager.CurrentContext.Culture;
          sourceUrls.MergeTo<T>(destinationUrls, (IDataItem) lifecycleDataItem, culture, new System.Action<T>(this.Delete));
          return;
        }
      }
      destinationUrls.ClearDestinationUrls<T>(sourceUrls, new System.Action<T>(this.Delete));
      sourceUrls.CopyTo<T>(destinationUrls, (IDataItem) destination);
    }

    private void ThrowDuplicateUrlException(string url) => throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format(Res.Get<ContentResources>().DuplicateUrlException, (object) url), (Exception) null);

    private static void Provider_Executing(object sender, ExecutingEventArgs args)
    {
      if (!(args.CommandName == "CommitTransaction") && !(args.CommandName == "FlushTransaction"))
        return;
      TProvider provider1 = sender as TProvider;
      TaxonomyStatisticsTracker data = provider1.GetExecutionStateData("taxonomy_statistics_changes") as TaxonomyStatisticsTracker;
      IList dirtyItems = provider1.GetDirtyItems();
      List<Guid> guidList = new List<Guid>();
      foreach (object obj in (IEnumerable) dirtyItems)
      {
        if (provider1.EnableCommentsBackwardCompatibility && obj is Comment comment && !comment.IsProccessedByCommentService)
        {
          if (sender is IOpenAccessDataProvider provider2)
          {
            SitefinityOAContext context = provider2.GetContext();
            ObjectState state = context.GetState((object) comment);
            if (state.HasFlag((System.Enum) ObjectState.Deleted) || state.HasFlag((System.Enum) ObjectState.MaskDeleted))
              comment.DirtyItemState = SecurityConstants.TransactionActionType.Deleted;
            comment.IsProccessedByCommentService = true;
            CommentsUtilities.ProcessGenericComment(comment);
            context.Remove(obj);
            break;
          }
          break;
        }
        if (obj is IOrganizable && (data == null || !data.SkipAutoTracking))
        {
          if (data == null)
            data = new TaxonomyStatisticsTracker();
          data.Track(obj, (DataProviderBase) provider1);
        }
        SecurityConstants.TransactionActionType dirtyItemStatus = provider1.GetDirtyItemStatus(obj);
        if (obj is Content content)
        {
          switch (dirtyItemStatus)
          {
            case SecurityConstants.TransactionActionType.New:
            case SecurityConstants.TransactionActionType.Updated:
              if (!(content is ILifecycleDataItemGeneric))
              {
                content.LastModifiedBy = ClaimsManager.GetCurrentUserId();
                continue;
              }
              continue;
            case SecurityConstants.TransactionActionType.Deleted:
              if (content.Status == ContentLifecycleStatus.Temp || content.Status == ContentLifecycleStatus.PartialTemp)
              {
                IContentLinksManager mappedRelatedManager = provider1.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
                RelatedDataHelper.DeleteNotUsedRelations((IDataItem) content, mappedRelatedManager);
                continue;
              }
              if (content.Status == ContentLifecycleStatus.Master || content.Status == ContentLifecycleStatus.Deleted)
              {
                IContentLinksManager mappedRelatedManager = provider1.GetMappedRelatedManager<ContentLink>(string.Empty) as IContentLinksManager;
                RelatedDataHelper.DeleteItemRelations((IDataItem) content, mappedRelatedManager);
                PackagingOperations.DeleteAddonLinks(content.Id, content.GetType().FullName);
                guidList.Add(content.Id);
                if (obj is IVersionSerializable)
                {
                  ContentHelper.DeleteVersionItem(provider1.GetMappedRelatedManager<Item>(string.Empty) as VersionManager, content.Id);
                  continue;
                }
                continue;
              }
              continue;
            default:
              continue;
          }
        }
        else if (obj is IDataItem && dirtyItemStatus == SecurityConstants.TransactionActionType.Deleted)
        {
          IDataItem dataItem = obj as IDataItem;
          PackagingOperations.DeleteAddonLinks(dataItem.Id, dataItem.GetType().FullName);
        }
      }
      if (guidList.Any<Guid>())
        provider1.SetExecutionStateData("deleted_workflow_items", (object) guidList);
      if (data == null || !data.HasChanges())
        return;
      provider1.SetExecutionStateData("taxonomy_statistics_changes", (object) data);
    }

    private static void Provider_Executed(object sender, ExecutedEventArgs args)
    {
      if (args.CommandName != "CommitTransaction")
        return;
      TProvider provider = sender as TProvider;
      if (provider.GetExecutionStateData("taxonomy_statistics_changes") is TaxonomyStatisticsTracker executionStateData)
      {
        executionStateData.SaveChanges();
        provider.SetExecutionStateData("taxonomy_statistics_changes", (object) null);
      }
      IApprovalWorkflowExtensions.DeleteApprovalRecords((DataProviderBase) provider);
    }

    /// <inheritdoc />
    public virtual IQueryable<T> GetRelatedItems<T>(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
      where T : IDataItem
    {
      return Queryable.OfType<T>(this.GetRelatedItems(itemType, itemProviderName, itemId, fieldName, typeof (T), status, filterExpression, orderExpression, skip, take, ref totalCount, relationDirection));
    }

    /// <inheritdoc />
    public virtual IQueryable GetRelatedItems(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
    {
      return (object) this.Provider != null ? ((IRelatedDataSource) this.Provider).GetRelatedItems(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, filterExpression, orderExpression, skip, take, ref totalCount, relationDirection) : RelatedDataHelper.GetRelatedItemsViaContains(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, (DataProviderBase) this.Provider, filterExpression, orderExpression, skip, take, ref totalCount, relationDirection);
    }

    public Dictionary<Guid, List<IDataItem>> GetRelatedItems(
      string itemType,
      string itemProviderName,
      List<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      return (object) this.Provider != null ? ((IRelatedDataSource) this.Provider).GetRelatedItems(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, status) : RelatedDataHelper.GetRelatedItemsViaContains(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, (DataProviderBase) this.Provider, status);
    }

    /// <inheritdoc />
    public IEnumerable<IDataItem> GetRelatedItemsList(
      string itemType,
      string itemProviderName,
      Collection<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      return (object) this.Provider != null ? ((IRelatedDataSource) this.Provider).GetRelatedItemsList(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, status) : RelatedDataHelper.GetRelatedItemsListViaContains(itemType, itemProviderName, parentItemIds, fieldName, relatedItemsType, (DataProviderBase) this.Provider, status);
    }
  }
}
