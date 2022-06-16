// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AppSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// This class is used to carry the settings of the fluent API.
  /// </summary>
  public class AppSettings
  {
    private int scheduleTaskExecuteTimeout;
    private string taxonomyProviderName;
    private string pagesProviderName;
    private string contentProviderName;
    private string metadataProviderName;
    private string versioningProviderName;
    private string transactionName;
    private string contentManagerName;
    private string formsProviderName;
    private string publishingProviderName;
    private string userProfileProviderName;
    private bool runPublishingSystemAsynchronously;
    private Dictionary<ItemTrackingStatus, List<IDataItem>> transactionItems;

    public AppSettings()
    {
      string globalTransaction = SystemManager.CurrentContext.GlobalTransaction;
      if (string.IsNullOrEmpty(globalTransaction))
        return;
      this.TransactionName = globalTransaction;
      this.IsGlobalTransaction = !globalTransaction.EndsWith("#Commit#");
    }

    public Dictionary<ItemTrackingStatus, List<IDataItem>> TransactionItems
    {
      get
      {
        if (this.transactionItems == null)
        {
          this.transactionItems = new Dictionary<ItemTrackingStatus, List<IDataItem>>();
          this.transactionItems.Add(ItemTrackingStatus.Modified, new List<IDataItem>());
          this.transactionItems.Add(ItemTrackingStatus.Deleted, new List<IDataItem>());
          this.transactionItems.Add(ItemTrackingStatus.DeletedWithAllTranslations, new List<IDataItem>());
          this.transactionItems.Add(ItemTrackingStatus.Published, new List<IDataItem>());
          this.transactionItems.Add(ItemTrackingStatus.Unpublished, new List<IDataItem>());
        }
        return this.transactionItems;
      }
    }

    /// <summary>
    /// Gets the name of the taxonomy provider to be used with the fluent API.
    /// </summary>
    public virtual string TaxonomyProviderName => this.taxonomyProviderName;

    /// <summary>
    /// Gets or set  the name of the pages provider to be used with the fluent API.
    /// </summary>
    public virtual string PagesProviderName
    {
      get => this.pagesProviderName;
      set => this.pagesProviderName = value;
    }

    /// <summary>
    /// Gets or set the name of the content provider to be used with the fluent API.
    /// </summary>
    public virtual string ContentProviderName
    {
      get => this.contentProviderName;
      set => this.contentProviderName = value;
    }

    /// <summary>
    /// Gets the name of the content provider to be used with the fluent API.
    /// </summary>
    public virtual string MetadataProviderName
    {
      get => this.metadataProviderName;
      set => this.metadataProviderName = value;
    }

    /// <summary>
    /// Gets the name of the forms provider to be used with the fluent API.
    /// </summary>
    public virtual string FormsProviderName
    {
      get => this.formsProviderName;
      set => this.formsProviderName = value;
    }

    /// <summary>
    /// Gets the name of the content manager to be used with the fluent API.
    /// </summary>
    public virtual string ContentManagerName
    {
      get => this.contentManagerName;
      set => this.contentManagerName = value;
    }

    /// <summary>
    /// Gets the name of the versioning provider to be used with the fluent API.
    /// </summary>
    public virtual string VersioningProviderName
    {
      get => this.versioningProviderName;
      set => this.versioningProviderName = value;
    }

    /// <summary>
    /// Gets or sets the publishing provider name to use with this transaction
    /// </summary>
    public virtual string PublishingProviderName
    {
      get => this.publishingProviderName;
      set => this.publishingProviderName = value;
    }

    /// <summary>
    /// Gets or sets the user profiles provider name to use with this transaction.
    /// </summary>
    public virtual string UserProfileProviderName
    {
      get => this.userProfileProviderName;
      set => this.userProfileProviderName = value;
    }

    /// <summary>
    /// Gets the name of the transaction used by all facades inside of one call.
    /// </summary>
    public string TransactionName
    {
      get
      {
        if (string.IsNullOrEmpty(this.transactionName))
          this.transactionName = Guid.NewGuid().ToString();
        return this.transactionName;
      }
      set
      {
        this.transactionName = value;
        this.IsGlobalTransaction = false;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to run publishing system asynchronously.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [run publishing system asynchronously]; otherwise, <c>false</c>.
    /// </value>
    public virtual bool RunPublishingSystemAsynchronously
    {
      get => this.runPublishingSystemAsynchronously;
      set => this.runPublishingSystemAsynchronously = value;
    }

    /// <summary>
    /// Gets or sets the execute timeout in seconds when schedule asynchronous task.
    /// </summary>
    /// <value>The execute timeout in seconds.</value>
    public virtual int ScheduleTaskExecuteTimeout
    {
      get => this.scheduleTaskExecuteTimeout;
      set => this.scheduleTaskExecuteTimeout = value;
    }

    internal bool IsGlobalTransaction { get; set; }

    /// <summary>
    /// Sets the name of the taxonomy provider to be used by the fluent API.
    /// </summary>
    /// <param name="taxonomyProviderName">Name of the provider.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> representing the configuration of the current fluent API request.</returns>
    public AppSettings SetTaxonomyProvider(string taxonomyProviderName)
    {
      this.taxonomyProviderName = taxonomyProviderName;
      return this;
    }

    /// <summary>
    /// Sets the name of the content provider to be used by the fluent API.
    /// </summary>
    /// <param name="contentProviderName">Name of the provider.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> representing the configuration of the current fluent API request.</returns>
    public AppSettings SetContentProvider(string contentProviderName)
    {
      this.contentProviderName = contentProviderName;
      return this;
    }

    /// <summary>
    /// Sets the name of the publishing provider to be used by the Fluent API
    /// </summary>
    /// <param name="publishingProvider">Name of the provider to use</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> representing the configuration of the current fluent API request.</returns>
    public AppSettings SetPublishingProvider(string publishingProvider)
    {
      this.publishingProviderName = publishingProvider;
      return this;
    }

    /// <summary>
    /// Sets the name of the pages provider to be used by the fluent API.
    /// </summary>
    /// <param name="pagesProviderName">Name of the provider.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> representing the configuration of the current fluent API request.</returns>
    public AppSettings SetPagesProvider(string pagesProviderName)
    {
      this.pagesProviderName = pagesProviderName;
      return this;
    }

    /// <summary>
    /// Sets the name of the versioning provider to be used by the fluent API.
    /// </summary>
    /// <param name="versioningProviderName">Name of the provider.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> representing the configuration of the current fluent API request.</returns>
    public AppSettings SetVersioningProvider(string versioningProviderName)
    {
      this.versioningProviderName = versioningProviderName;
      return this;
    }

    /// <summary>
    /// Sets the name of the forms provider to be used by the fluent API.
    /// </summary>
    /// <param name="formsProvider">Name of the forms provider.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> representing the configuration of the current fluent API request.</returns>
    public AppSettings SetFormsProvider(string formsProvider)
    {
      this.formsProviderName = formsProvider;
      return this;
    }

    /// <summary>
    /// Sets the name of the transaction to be used by the fluent API.
    /// </summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns></returns>
    public AppSettings SetTransactionName(string transactionName)
    {
      this.transactionName = transactionName;
      this.IsGlobalTransaction = false;
      return this;
    }

    /// <summary>
    /// Sets the a flag indicating if the publishing system will be run asynchronous.
    /// </summary>
    /// <param name="runPublishingSystemAsynchronous">A flag indicating if the publishing system will be run asynchronous.</param>
    /// <returns></returns>
    public AppSettings SetRunPublishingSystemAsynchronous(
      bool runPublishingSystemAsynchronously = true)
    {
      this.runPublishingSystemAsynchronously = runPublishingSystemAsynchronously;
      return this;
    }

    /// <summary>Sets the schedule task execute timeout.</summary>
    /// <param name="scheduleTaskExecuteTimeout">if set to <c>true</c> [schedule task execute timeout].</param>
    /// <returns></returns>
    public AppSettings SetScheduleTaskExecuteTimeout(int scheduleTaskExecuteTimeout)
    {
      this.scheduleTaskExecuteTimeout = scheduleTaskExecuteTimeout;
      return this;
    }

    /// <summary>
    /// Use this method to mark that you are done with configuration of the API request and ready to work with the fluent API.
    /// </summary>
    /// <returns></returns>
    public FluentSitefinity WorkWith() => new FluentSitefinity(this);

    internal void ClearTransactionItems()
    {
      if (this.transactionItems == null)
        return;
      foreach (KeyValuePair<ItemTrackingStatus, List<IDataItem>> transactionItem in this.TransactionItems)
        transactionItem.Value.Clear();
    }

    public void TrackPublishedItem(IDataItem item)
    {
      this.ClearCollection((IList<IDataItem>) this.TransactionItems[ItemTrackingStatus.Published], item.Id);
      if (this.IfItemIsDeleted(item.Id))
        return;
      this.TransactionItems[ItemTrackingStatus.Published].Add(item);
    }

    public void TrackUnpublishedItem(IDataItem item)
    {
      this.ClearCollection((IList<IDataItem>) this.TransactionItems[ItemTrackingStatus.Unpublished], item.Id);
      if (this.IfItemIsDeleted(item.Id))
        return;
      this.TransactionItems[ItemTrackingStatus.Unpublished].Add(item);
    }

    public void TrackModifiedItem(IDataItem item)
    {
      this.ClearCollection((IList<IDataItem>) this.TransactionItems[ItemTrackingStatus.Modified], item.Id);
      if (this.IfItemIsDeleted(item.Id))
        return;
      this.TransactionItems[ItemTrackingStatus.Modified].Add(item);
    }

    private void ClearCollection(IList<IDataItem> items, Guid id)
    {
      for (int index = 0; index < items.Count; ++index)
      {
        if (items[index].Id == id)
        {
          items.RemoveAt(index);
          --index;
        }
      }
    }

    public bool IfItemIsDeleted(Guid itemId) => this.transactionItems != null && this.TransactionItems[ItemTrackingStatus.Deleted].Where<IDataItem>((Func<IDataItem, bool>) (item => item.Id == itemId)).Any<IDataItem>();

    public void TrackDeletedItem(IDataItem item) => this.TrackDeletedItem(item, SystemManager.CurrentContext.Culture.Name);

    public void TrackDeletedItem(IDataItem item, string culture)
    {
      this.ClearCollection((IList<IDataItem>) this.TransactionItems[ItemTrackingStatus.Modified], item.Id);
      this.ClearCollection((IList<IDataItem>) this.TransactionItems[ItemTrackingStatus.Published], item.Id);
      this.TransactionItems[ItemTrackingStatus.Deleted].Add(item);
    }

    /// <summary>
    /// Tracks the item that is fully deleted (all translations are deleted)
    /// </summary>
    /// <param name="item">The deleted item</param>
    public void TrackFullyDeletedItem(IDataItem item)
    {
      this.ClearCollection((IList<IDataItem>) this.TransactionItems[ItemTrackingStatus.Modified], item.Id);
      this.ClearCollection((IList<IDataItem>) this.TransactionItems[ItemTrackingStatus.Published], item.Id);
      this.TransactionItems[ItemTrackingStatus.DeletedWithAllTranslations].Add(item);
    }
  }
}
