// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinDynamicContentStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.RecycleBin.Conflicts;
using Telerik.Sitefinity.RecycleBin.Security;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>
  /// Recycle Bin strategy for <see cref="P:Telerik.Sitefinity.RecycleBin.RecycleBinDynamicContentStrategy.DynamicModuleManager" /> managers.
  /// Responsible for <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> items recycling.
  /// </summary>
  public class RecycleBinDynamicContentStrategy : RecycleBinLifecycleStrategy
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.RecycleBinDynamicContentStrategy" /> class.
    /// </summary>
    public RecycleBinDynamicContentStrategy()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.RecycleBin.RecycleBinDynamicContentStrategy" /> class.
    /// </summary>
    /// <param name="manager">The decorated manager of the <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.</param>
    /// <param name="eventRegistry">A component that will be used to register the occurrence of
    /// Recycle Bin related events like 'move to' and 'restore from'.</param>
    /// <param name="urlValidator">URL validator for the dynamic content items.</param>
    /// <param name="permissionsAuthorizer">The component responsible for authorizing Recycle Bin related actions.</param>
    /// <exception cref="T:System.Exception">The supported recycling manager should be an instance of DynamicModuleManager.</exception>
    [InjectionConstructor]
    public RecycleBinDynamicContentStrategy(
      ISupportRecyclingManager manager,
      IRecycleBinEventRegistry eventRegistry,
      IRecycleBinUrlValidator<DynamicContent> urlValidator,
      IRecycleBinActionsAuthorizer permissionsAuthorizer)
      : base(manager, eventRegistry, permissionsAuthorizer)
    {
      this.DynamicModuleManager = manager is DynamicModuleManager dynamicModuleManager ? dynamicModuleManager : throw new Exception("The supported recycling manager should be an instance of DynamicModuleManager.");
      this.DynamicUrlValidator = urlValidator;
    }

    /// <summary>
    /// Gets or sets the <see cref="P:Telerik.Sitefinity.RecycleBin.RecycleBinDynamicContentStrategy.DynamicModuleManager" /> manager to be used
    /// when moving items to the Recycle Bin, restoring or deleting them.
    /// </summary>
    /// <value>
    /// The <see cref="P:Telerik.Sitefinity.RecycleBin.RecycleBinDynamicContentStrategy.DynamicModuleManager" /> manager used by the strategy.
    /// </value>
    protected DynamicModuleManager DynamicModuleManager { get; set; }

    /// <summary>Gets or sets the UrlValidator.</summary>
    /// <value>The UrlValidator.</value>
    protected IRecycleBinUrlValidator<DynamicContent> DynamicUrlValidator { get; set; }

    /// <summary>
    /// Marks the specified <paramref name="dataItem" /> as deleted.
    /// The opposite of RestoreFromRecycleBin.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="language">The specific language translation that will be sent to the Recycle Bin.</param>
    /// <exception cref="T:System.Exception">Throws exception if the item is not an instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> class.</exception>
    /// <exception cref="T:System.Exception">Throws exception if the item does not implement the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> interface.</exception>
    public override void MoveToRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (language != null)
        throw new NotSupportedException();
      DynamicContent dynamicContent = this.TryCastToDynamicContent(dataItem);
      this.EnsureDynamicContentMoveToRecycleBinPermissions(this.DynamicModuleManager, dynamicContent);
      this.MoveChildrenToRecycleBin(dynamicContent, language);
      base.MoveToRecycleBin(dataItem, language);
    }

    /// <summary>
    /// Restores the specified <see cref="T:Telerik.Sitefinity.RecycleBin.IRecyclableDataItem" />.
    /// The opposite of MoveToRecycleBin.
    /// </summary>
    /// <param name="dataItem">The data item to restore.</param>
    /// <param name="language">The specific language translation that will be restored from the Recycle Bin.</param>
    /// <exception cref="T:System.Exception">Throws exception if the item is not an instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> class.</exception>
    /// <exception cref="T:System.Exception">Throws exception if the item does not implement the <see cref="T:Telerik.Sitefinity.Lifecycle.ILifecycleDataItem" /> interface.</exception>
    public override void RestoreFromRecycleBin(IRecyclableDataItem dataItem, CultureInfo language = null)
    {
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      if (language != null)
        throw new NotSupportedException();
      DynamicContent dynamicContent = this.TryCastToDynamicContent(dataItem);
      this.EnsureDynamicContentRestoreFromRecycleBinPermissions(this.DynamicModuleManager, dynamicContent);
      this.RestoreFromRecycleBinChildItems(dynamicContent, language);
      base.RestoreFromRecycleBin(dataItem, language);
      this.DynamicModuleManager.Provider.RecompileItemUrls<DynamicContent>(dynamicContent);
    }

    /// <summary>
    /// Validates whether the specified dynamic content can be restored.
    /// </summary>
    /// <param name="dataItem">The data item which restoration will be validated.</param>
    /// <param name="language">The specific language translation that will be validated for restore from the Recycle Bin.</param>
    /// <returns>Return a list of <see cref="T:Telerik.Sitefinity.RecycleBin.Conflicts.IRestoreConflict" /> containing invalid restore reasons.</returns>
    public override IList<IRestoreConflict> ValidateRestore(
      IRecyclableDataItem dataItem,
      CultureInfo language = null)
    {
      IList<IRestoreConflict> restoreConflictList = dataItem is DynamicContent dynamicContent ? base.ValidateRestore(dataItem, language) : throw new Exception(string.Format("The supported dataItem of type {0} with Id {1} should be of type {2}.", (object) dataItem.GetType().FullName, (object) dataItem.Id, (object) typeof (DynamicContent).FullName));
      IRestoreConflict missingParentConflict = this.GetMissingParentConflict(dynamicContent);
      if (missingParentConflict != null)
        restoreConflictList.Add(missingParentConflict);
      return restoreConflictList;
    }

    /// <summary>Asserts the no URL conflicts.</summary>
    /// <param name="dataItem">The data item.</param>
    internal override void AssertNoUrlConflicts(ILifecycleDataItem dataItem) => this.DynamicUrlValidator.AssertNoUrlConflicts((IManager) this.DynamicModuleManager, (DynamicContent) dataItem);

    /// <summary>Gets the URL conflict.</summary>
    /// <param name="dataItem">The data item.</param>
    /// <returns>Returns a URL conflict if any.</returns>
    internal override IRestoreConflict GetUrlConflict(ILifecycleDataItem dataItem) => this.DynamicUrlValidator.GetUrlConflict((IManager) this.DynamicModuleManager, (DynamicContent) dataItem);

    private DynamicContent TryCastToDynamicContent(IRecyclableDataItem dataItem) => dataItem is DynamicContent dynamicContent ? dynamicContent : throw new Exception(string.Format("The supported dataItem of type {0} with Id {1} should be and instance of DynamicContent.", (object) dataItem.GetType().FullName, (object) dataItem.Id));

    /// <summary>
    /// Moves to the Recycle bin the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> child items.
    /// </summary>
    /// <param name="dynamicContentItem">The parent dynamic content item.</param>
    /// <param name="language">The specific language translation that will be sent to the Recycle Bin.</param>
    private void MoveChildrenToRecycleBin(DynamicContent dynamicContentItem, CultureInfo language)
    {
      foreach (DynamicContent childItem in this.GetChildItems(dynamicContentItem))
      {
        if (childItem.Status == ContentLifecycleStatus.Master)
        {
          this.MoveChildrenToRecycleBin(childItem, language);
          this.MoveChildToRecycleBin(childItem, language);
        }
      }
    }

    /// <summary>
    /// Restores from the Recycle bin the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> child items.
    /// </summary>
    /// <param name="dynamicContentItem">The parent dynamic content item.</param>
    /// <param name="language">The specific language translation that will be restored from the Recycle Bin.</param>
    private void RestoreFromRecycleBinChildItems(
      DynamicContent dynamicContentItem,
      CultureInfo language)
    {
      foreach (DynamicContent childItem in this.GetChildItems(dynamicContentItem))
      {
        if (childItem.Status == ContentLifecycleStatus.Deleted)
          this.RestoreFromRecycleBin((IRecyclableDataItem) childItem, language);
      }
    }

    /// <summary>
    /// Gets for a <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> object his direct successors.
    /// </summary>
    /// <param name="dynamicContentItem">The parent dynamic content item.</param>
    /// <returns>A list of all direct successors.</returns>
    private List<DynamicContent> GetChildItems(DynamicContent dynamicContentItem)
    {
      List<DynamicContent> childItems = new List<DynamicContent>();
      IEnumerable<Type> childTypes = this.DynamicModuleManager.ModuleBuilderMgr.GetChildTypes(dynamicContentItem.GetType());
      if (childTypes != null && childTypes.Count<Type>() > 0)
      {
        foreach (Type itemType in childTypes)
          childItems.AddRange((IEnumerable<DynamicContent>) this.DynamicModuleManager.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.SystemParentId == dynamicContentItem.Id)).ToList<DynamicContent>());
      }
      return childItems;
    }

    /// <summary>
    /// Moves a specific <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> child object to the Recycle bin.
    /// The events registered for the MoveToRecycleBin operation indicate that the move
    /// is done as a result of a parent MoveToRecycleBin operation. As result no <see cref="!:IRecycleBinDataItems" /> should be created
    /// </summary>
    /// <param name="masterDataItem">The master child item.</param>
    /// <param name="language">The language.</param>
    private void MoveChildToRecycleBin(DynamicContent masterDataItem, CultureInfo language)
    {
      this.RecyclingItemLifecycleManager.Lifecycle.DiscardAllTemps((ILifecycleDataItem) masterDataItem);
      if (this.RecyclingItemLifecycleManager.Lifecycle.GetLive((ILifecycleDataItem) masterDataItem) is IRecyclableDataItem live)
      {
        live.IsDeleted = true;
        string[] array = ((IEnumerable<string>) (live as DynamicContent).AvailableLanguages).Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name)).ToArray<string>();
        this.EventRegistry.RegisterMoveToRecycleBinWithParentOperation(live, array);
      }
      masterDataItem.IsDeleted = true;
      string[] array1 = ((IEnumerable<string>) masterDataItem.AvailableLanguages).Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name)).ToArray<string>();
      this.EventRegistry.RegisterMoveToRecycleBinWithParentOperation((IRecyclableDataItem) masterDataItem, array1);
    }

    private IRestoreConflict GetMissingParentConflict(DynamicContent dynamicContent)
    {
      if (dynamicContent.SystemParentItem == null || !dynamicContent.SystemParentItem.IsDeleted)
        return (IRestoreConflict) null;
      return (IRestoreConflict) new RestoreConflict()
      {
        IsRecoverable = false,
        Reason = Enum.GetName(typeof (RestoreConflictReasons), (object) RestoreConflictReasons.MissingParentDynamicContent),
        ReasonArgs = (IDictionary) null
      };
    }

    /// <summary>
    /// Ensures that there is a 'Delete' permission for the recyclable dynamic content item when move data item to Recycle Bin.
    /// </summary>
    /// <param name="moduleManager">The dynamic module manager used to get dynamic type permissions.</param>
    /// <param name="contentItem">The content item.</param>
    private void EnsureDynamicContentMoveToRecycleBinPermissions(
      DynamicModuleManager moduleManager,
      DynamicContent contentItem)
    {
      DynamicModuleType dynamicModuleType = moduleManager.ModuleBuilderMgr.GetDynamicModuleType(contentItem.GetType());
      Guid owner = dynamicModuleType.Owner;
      try
      {
        dynamicModuleType.Owner = contentItem.Owner;
        contentItem.ProviderName = contentItem.GetProviderName();
        if (!dynamicModuleType.IsDeletable(contentItem.ProviderName))
          throw new SecurityDemandFailException("You are not allowed to move this item into the Recycle Bin.");
      }
      finally
      {
        dynamicModuleType.Owner = owner;
      }
    }

    /// <summary>
    /// Ensures that there is a 'Modify' permission for the recyclable dynamic content item when restore data item from Recycle Bin.
    /// </summary>
    /// <param name="moduleManager">The dynamic module manager used to get dynamic type permissions.</param>
    /// <param name="contentItem">The recyclable dynamic content item that is checked for permissions.</param>
    private void EnsureDynamicContentRestoreFromRecycleBinPermissions(
      DynamicModuleManager moduleManager,
      DynamicContent contentItem)
    {
      DynamicModuleType dynamicModuleType = moduleManager.ModuleBuilderMgr.GetDynamicModuleType(contentItem.GetType());
      Guid owner = dynamicModuleType.Owner;
      try
      {
        dynamicModuleType.Owner = contentItem.Owner;
        contentItem.ProviderName = contentItem.GetProviderName();
        if (!dynamicModuleType.IsEditable(contentItem.ProviderName))
          throw new SecurityDemandFailException("You are not allowed to restore this item from the Recycle Bin.");
      }
      finally
      {
        dynamicModuleType.Owner = owner;
      }
    }
  }
}
