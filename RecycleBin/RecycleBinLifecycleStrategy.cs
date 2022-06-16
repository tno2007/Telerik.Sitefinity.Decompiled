// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinLifecycleStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.RecycleBin.Conflicts;
using Telerik.Sitefinity.RecycleBin.Security;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Recycle Bin strategy for <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleManager" /> managers.
  /// Responsible for <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> recycling.
  /// </summary>
  public class RecycleBinLifecycleStrategy : IRecycleBinStrategy
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.RecycleBinLifecycleStrategy" /> class.
    /// </summary>
    public RecycleBinLifecycleStrategy()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.RecycleBinLifecycleStrategy" /> class.
    /// </summary>
    /// <param name="manager">The decorated manager of the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.</param>
    /// <param name="eventRegistry">A component that will be used to register the occurrence of
    /// Recycle Bin related events like 'move to' and 'restore from'.</param>
    /// <param name="permissionsAuthorizer">The component responsible for authorizing Recycle Bin related actions.</param>
    /// <param name="urlValidator">URL validator for lifecycle items.</param>
    /// <exception cref="T:System.Exception">The supported recycling manager should implement the ILifecycleManager interface.</exception>
    [InjectionConstructor]
    public RecycleBinLifecycleStrategy(
      ISupportRecyclingManager manager,
      IRecycleBinEventRegistry eventRegistry,
      IRecycleBinActionsAuthorizer permissionsAuthorizer,
      IRecycleBinUrlValidator<ILifecycleDataItem> urlValidator)
      : this(manager, eventRegistry, permissionsAuthorizer)
    {
      this.UrlValidator = urlValidator;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.RecycleBinLifecycleStrategy" /> class.
    /// </summary>
    /// <param name="manager">The decorated manager of the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.</param>
    /// <param name="eventRegistry">A component that will be used to register the occurrence of
    /// Recycle Bin related events like 'move to' and 'restore from'.</param>
    /// <param name="permissionsAuthorizer">The component responsible for authorizing Recycle Bin related actions.</param>
    /// <exception cref="T:System.Exception">The supported recycling manager should implement the ILifecycleManager interface.</exception>
    protected internal RecycleBinLifecycleStrategy(
      ISupportRecyclingManager manager,
      IRecycleBinEventRegistry eventRegistry,
      IRecycleBinActionsAuthorizer permissionsAuthorizer)
    {
      this.RecyclingItemLifecycleManager = manager is ILifecycleManager lifecycleManager ? lifecycleManager : throw new Exception("The supported recycling manager should implement the ILifecycleManager interface.");
      this.EventRegistry = eventRegistry;
      this.PermissionsAuthorizer = permissionsAuthorizer;
    }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.RecycleBin.ISupportRecyclingManager" /> manager to be used
    /// when moving items to the Recycle Bin, restoring or deleting them.
    /// </summary>
    /// <value>The manager used by the strategy.</value>
    protected ILifecycleManager RecyclingItemLifecycleManager { get; set; }

    /// <summary>
    /// Gets or sets a component that will be used to register the occurrence of
    /// Recycle Bin related events like 'move to' and 'restore from'.
    /// </summary>
    /// <value>The event register.</value>
    protected IRecycleBinEventRegistry EventRegistry { get; set; }

    /// <summary>Gets or sets the UrlValidator.</summary>
    /// <value>The UrlValidator.</value>
    protected IRecycleBinUrlValidator<ILifecycleDataItem> UrlValidator { get; set; }

    /// <summary>
    /// Gets or sets the component responsible for permissions checks.
    /// </summary>
    /// <value>The component responsible for permissions checks.</value>
    protected IRecycleBinActionsAuthorizer PermissionsAuthorizer { get; set; }

    /// <summary>
    /// Marks the specified <paramref name="dataItem" /> as deleted.
    /// The opposite of RestoreFromRecycleBin.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="language">The specific language translation that will be sent to the Recycle Bin.</param>
    /// <exception cref="T:System.Exception">Throws exception if the item does not implement the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> interface.</exception>
    public virtual void MoveToRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (language != null)
        throw new NotSupportedException();
      if (!(dataItem is ILifecycleDataItem lifecycleDataItem))
        throw new Exception(string.Format("The supported dataItem of type {0} with Id {1} should implement the ILifecycleDataItem interface.", (object) dataItem.GetType().FullName, (object) dataItem.Id));
      this.PermissionsAuthorizer.EnsureMoveToRecycleBinPermissions((object) dataItem);
      this.RecyclingItemLifecycleManager.Lifecycle.DiscardAllTemps(lifecycleDataItem);
      if (this.RecyclingItemLifecycleManager.Lifecycle.GetLive(lifecycleDataItem) is IRecyclableDataItem live)
      {
        live.IsDeleted = true;
        this.DeletePublishedTranslations(live);
        this.OnMovedToRecycleBin(lifecycleDataItem);
        string[] excludingInvariant = this.GetAvailableLanguagesExcludingInvariant(live);
        this.EventRegistry.RegisterMoveToRecycleBinOperation(live, excludingInvariant);
      }
      ILifecycleDataItem master = this.RecyclingItemLifecycleManager.Lifecycle.GetMaster(lifecycleDataItem);
      IRecyclableDataItem dataItem1 = (IRecyclableDataItem) master;
      dataItem1.IsDeleted = true;
      this.DeletePublishedTranslations(dataItem1);
      this.OnMovedToRecycleBin(master);
      string[] excludingInvariant1 = this.GetAvailableLanguagesExcludingInvariant(dataItem1);
      this.EventRegistry.RegisterMoveToRecycleBinOperation(dataItem1, excludingInvariant1);
    }

    /// <summary>Permanently deletes item from recycle bin.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="language">The specific language translation that will be permanently deleted.</param>
    /// <remarks>Same as invoking IManager.DeleteItem method.</remarks>
    public virtual void PermanentlyDeleteFromRecycleBin(
      IRecyclableDataItem dataItem,
      CultureInfo language = null)
    {
      if (!dataItem.IsDeleted)
        return;
      this.RecyclingItemLifecycleManager.DeleteItem((object) dataItem, language);
    }

    /// <summary>
    /// Restores the specified <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.
    /// The opposite of MoveToRecycleBin.
    /// </summary>
    /// <param name="dataItem">The data item to restore.</param>
    /// <param name="language">The specific language translation that will be restored from the Recycle Bin.</param>
    /// <exception cref="T:System.Exception">Throws exception if the item does not implement the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> interface.</exception>
    public virtual void RestoreFromRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (language != null)
        throw new NotSupportedException();
      if (!(dataItem is ILifecycleDataItem lifecycleDataItem))
        throw new Exception(string.Format("The supported dataItem of type {0} with Id {1} should implement the ILifecycleDataItem interface.", (object) dataItem.GetType().FullName, (object) dataItem.Id));
      this.PermissionsAuthorizer.EnsureRestoreFromRecycleBinPermissions((object) dataItem);
      this.AssertNoUrlConflicts(lifecycleDataItem);
      dataItem.IsDeleted = false;
      if (this.RecyclingItemLifecycleManager.Lifecycle.GetLive(lifecycleDataItem) is IRecyclableDataItem live)
      {
        live.IsDeleted = false;
        this.RestorePublishedTranslations(live);
        this.OnRestoredFromRecycleBin(lifecycleDataItem);
        string[] excludingInvariant = this.GetAvailableLanguagesExcludingInvariant(live);
        this.EventRegistry.RegisterRestoreFromRecycleBinOperation(live, excludingInvariant);
      }
      IRecyclableDataItem dataItem1 = lifecycleDataItem.Status != ContentLifecycleStatus.Deleted ? (IRecyclableDataItem) this.RecyclingItemLifecycleManager.Lifecycle.GetMaster(lifecycleDataItem) : dataItem;
      dataItem1.IsDeleted = false;
      this.RestorePublishedTranslations(dataItem1);
      this.OnRestoredFromRecycleBin(dataItem1 as ILifecycleDataItem);
      string[] excludingInvariant1 = this.GetAvailableLanguagesExcludingInvariant(dataItem1);
      this.EventRegistry.RegisterRestoreFromRecycleBinOperation(dataItem, excludingInvariant1);
    }

    /// <summary>
    /// Validates whether the specified <paramref name="dataItem" /> can be restored.
    /// </summary>
    /// <param name="dataItem">The data item which restoration will be validated.</param>
    /// <param name="language">The specific language translation that will be validated for restore from the Recycle Bin.</param>
    /// <returns>Return a list of <see cref="T:Telerik.Sitefinity.RecycleBin.Conflicts.IRestoreConflict" /> containing invalid restore reasons.</returns>
    public virtual IList<IRestoreConflict> ValidateRestore(
      IRecyclableDataItem dataItem,
      CultureInfo language = null)
    {
      if (!(dataItem is ILifecycleDataItem dataItem1))
        throw new Exception(string.Format("The supported dataItem of type {0} with Id {1} should implement the ILifecycleDataItem interface.", (object) dataItem.GetType().FullName, (object) dataItem.Id));
      List<IRestoreConflict> restoreConflictList = new List<IRestoreConflict>();
      IRestoreConflict urlConflict = this.GetUrlConflict(dataItem1);
      if (urlConflict != null)
        restoreConflictList.Add(urlConflict);
      return (IList<IRestoreConflict>) restoreConflictList;
    }

    /// <summary>Asserts the no URL conflicts.</summary>
    /// <param name="dataItem">The data item.</param>
    internal virtual void AssertNoUrlConflicts(ILifecycleDataItem dataItem) => this.UrlValidator.AssertNoUrlConflicts((IManager) this.RecyclingItemLifecycleManager, dataItem);

    /// <summary>Gets the URL conflict.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <returns>Returns a URL conflict if any.</returns>
    internal virtual IRestoreConflict GetUrlConflict(ILifecycleDataItem dataItem) => this.UrlValidator.GetUrlConflict((IManager) this.RecyclingItemLifecycleManager, dataItem);

    /// <summary>
    /// A template method used for execution of custom additional logic in sub class when moving to recycle bin
    /// </summary>
    /// <param name="dataItem">A lifecycle data item</param>
    protected virtual void OnMovedToRecycleBin(ILifecycleDataItem dataItem)
    {
    }

    /// <summary>
    /// A template method used for execution of custom additional logic in sub class when moving to recycle bin
    /// </summary>
    /// <param name="dataItem">A lifecycle data item</param>
    protected virtual void OnRestoredFromRecycleBin(ILifecycleDataItem dataItem)
    {
    }

    private string[] GetAvailableLanguagesExcludingInvariant(IRecyclableDataItem dataItem) => dataItem is ILocalizable localizable ? ((IEnumerable<string>) localizable.AvailableLanguages).Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name)).ToArray<string>() : new string[0];

    private void DeletePublishedTranslations(IRecyclableDataItem dataItem)
    {
      if (!(dataItem is ILifecycleDataItem lifecycleDataItem))
        return;
      lifecycleDataItem.PublishedTranslations.Clear();
    }

    private void RestorePublishedTranslations(IRecyclableDataItem dataItem)
    {
      if (!(dataItem is ILifecycleDataItem lifecycleDataItem))
        return;
      foreach (string str in lifecycleDataItem.LanguageData.Where<LanguageData>((Func<LanguageData, bool>) (ld => ld.ContentState == LifecycleState.Published && ld.Language != null)).Select<LanguageData, string>((Func<LanguageData, string>) (ld => ld.Language)).ToList<string>())
        lifecycleDataItem.PublishedTranslations.Add(str);
    }
  }
}
