// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Events;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>
  /// The base abstract data provider class for the module builder module.
  /// </summary>
  public abstract class ModuleBuilderDataProvider : DataProviderBase, IDataEventProvider
  {
    private const string EventsStateBagKey = "module-builder-events";

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (typeof (DynamicModule).IsAssignableFrom(itemType))
        return (object) this.GetDynamicModule(id);
      if (typeof (DynamicModuleType).IsAssignableFrom(itemType))
        return (object) this.GetDynamicModuleType(id);
      if (typeof (DynamicModuleField).IsAssignableFrom(itemType))
        return (object) this.GetDynamicModuleField(id);
      return typeof (DynamicContentProvider).IsAssignableFrom(itemType) ? (object) this.GetDynamicContentProvider(id) : base.GetItem(itemType, id);
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (typeof (DynamicModule).IsAssignableFrom(itemType))
        return (object) this.GetDynamicModules().SingleOrDefault<DynamicModule>((Expression<Func<DynamicModule, bool>>) (module => module.Id.Equals(id)));
      if (typeof (DynamicModuleType).IsAssignableFrom(itemType))
        return (object) this.GetDynamicModuleTypes().SingleOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (type => type.Id.Equals(id)));
      if (typeof (DynamicModuleField).IsAssignableFrom(itemType))
        return (object) this.GetDynamicModuleFields().SingleOrDefault<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (field => field.Id.Equals(id)));
      if (typeof (SecurityRoot).IsAssignableFrom(itemType))
        return (object) this.GetSecurityRoot();
      if (!typeof (DynamicContentProvider).IsAssignableFrom(itemType))
        return base.GetItemOrDefault(itemType, id);
      return (object) this.GetDynamicContentProviders().SingleOrDefault<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (provider => provider.Id.Equals(id)));
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (typeof (DynamicModule).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<DynamicModule>(this.GetDynamicModules(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (DynamicModuleType).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<DynamicModuleType>(this.GetDynamicModuleTypes(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (DynamicModuleField).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<DynamicModuleField>(this.GetDynamicModuleFields(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (DynamicContentProvider).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<DynamicContentProvider>(this.GetDynamicContentProviders(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    public override void FlushTransaction()
    {
      if (this.ShouldRaiseDataEvents(out IDataEventProvider _))
      {
        using (new ReadUncommitedRegion())
          this.ProcessEvents();
      }
      base.FlushTransaction();
    }

    public override void CommitTransaction()
    {
      int num = this.ShouldRaiseDataEvents(out IDataEventProvider _) ? 1 : 0;
      if (num != 0)
        this.ProcessEvents();
      base.CommitTransaction();
      if (num == 0)
        return;
      this.RaiseEvents(false);
    }

    /// <summary>Sets the root permissions for dynamic modules.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      if (root.Permissions != null || root.Permissions.Count > 0)
        root.Permissions.Clear();
      Permission permission1 = this.CreatePermission("General", root.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(false, "View");
      root.Permissions.Add(permission1);
      Permission permission2 = this.CreatePermission("General", root.Id, SecurityManager.OwnerRole.Id);
      permission2.GrantActions(false, "Modify", "Delete");
      root.Permissions.Add(permission2);
      Permission permission3 = this.CreatePermission("General", root.Id, SecurityManager.EditorsRole.Id);
      permission3.GrantActions(false, "Create", "Modify", "Delete", "ChangeOwner");
      root.Permissions.Add(permission3);
      Permission permission4 = this.CreatePermission("General", root.Id, SecurityManager.AuthorsRole.Id);
      permission4.GrantActions(false, "Create");
      root.Permissions.Add(permission4);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    public abstract DynamicModule CreateDynamicModule();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    public abstract DynamicModule CreateDynamicModule(Guid id, string applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    public abstract DynamicModule GetDynamicModule(Guid id);

    /// <summary>Gets the query of all dynamic modules.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> type.</returns>
    public abstract IQueryable<DynamicModule> GetDynamicModules();

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="dynamicModule">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> to be deleted.</param>
    public abstract void Delete(DynamicModule dynamicModule);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    public abstract DynamicModuleType CreateDynamicModuleType();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module type to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module type ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    public abstract DynamicModuleType CreateDynamicModuleType(
      Guid id,
      string applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module type to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    public abstract DynamicModuleType GetDynamicModuleType(Guid id);

    /// <summary>Gets the query of all dynamic module types.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> type.</returns>
    public abstract IQueryable<DynamicModuleType> GetDynamicModuleTypes();

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <param name="dynamicModuleType">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> to be deleted.</param>
    public abstract void Delete(DynamicModuleType dynamicModuleType);

    /// <summary>
    /// Checks if the current user has drop table permissions for specific type table
    /// </summary>
    /// <param name="dynamicModuleType">dynamic type</param>
    /// <returns></returns>
    public virtual bool HasDropTablePermissions(DynamicModuleType dynamicModuleType) => true;

    /// <summary>
    /// Checks if the current user has alter table permissions for specific type table
    /// </summary>
    /// <param name="dynamicModuleType">dynamic type</param>
    /// <returns></returns>
    public virtual bool HasAlterTablePermissions(DynamicModuleType dynamicModuleType) => true;

    /// <summary>
    /// Sets the parent module of the specified dynamic module type.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="parentModule">The parent module.</param>
    internal virtual void SetParentModule(
      DynamicModuleType dynamicModuleType,
      DynamicModule parentModule)
    {
      dynamicModuleType.ParentModuleId = parentModule.Id;
      dynamicModuleType.ModuleName = parentModule.Name;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    public abstract DynamicModuleField CreateDynamicModuleField();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module field to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module field ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    public abstract DynamicModuleField CreateDynamicModuleField(
      Guid id,
      string applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module field to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    public abstract DynamicModuleField GetDynamicModuleField(Guid id);

    /// <summary>Gets the query of all dynamic module fields.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> type.</returns>
    public abstract IQueryable<DynamicModuleField> GetDynamicModuleFields();

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.
    /// </summary>
    /// <param name="dynamicModuleField">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> to be deleted.</param>
    public abstract void Delete(DynamicModuleField dynamicModuleField);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    public abstract FieldsBackendSection CreateFieldsBackendSection();

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the fields backend section to be created.</param>
    /// <param name="applicationName">Application name under which the fields backend section ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    public abstract FieldsBackendSection CreateFieldsBackendSection(
      Guid id,
      string applicationName);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the fields backend section to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    public abstract FieldsBackendSection GetFieldsBackendSection(Guid id);

    /// <summary>Gets the query of all field backend sections.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> type.</returns>
    public abstract IQueryable<FieldsBackendSection> GetFieldsBackendSections();

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.
    /// </summary>
    /// <param name="fieldsBackendSection">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> to be deleted.</param>
    public abstract void Delete(FieldsBackendSection fieldsBackendSection);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.</returns>
    public virtual DynamicContentProvider CreateDynamicContentProvider(
      Guid id,
      string applicationName,
      ISecuredObject parentSecuredObject)
    {
      throw new NotImplementedException();
    }

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </summary>
    /// <param name="dynamicContentProvider">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> to be deleted.</param>
    public virtual void DeleteDynamicContentProvider(DynamicContentProvider dynamicContentProvider) => throw new NotImplementedException();

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.</returns>
    public virtual DynamicContentProvider GetDynamicContentProvider(Guid id) => throw new NotImplementedException();

    /// <summary>Gets the list of all dynamic modules.</summary>
    /// <returns>
    /// A list of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> type.
    /// </returns>
    public virtual IQueryable<DynamicContentProvider> GetDynamicContentProviders() => throw new NotImplementedException();

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </summary>
    /// <param name="dynamicContentProvider">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> to be deleted.</param>
    public virtual void Delete(DynamicContentProvider dynamicContentProvider) => throw new NotImplementedException();

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns>An array of known types for this data provider.</returns>
    public override Type[] GetKnownTypes() => new Type[4]
    {
      typeof (DynamicModule),
      typeof (DynamicModuleType),
      typeof (DynamicContentProvider),
      typeof (DynamicModuleField)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => "ModuleBuilder";

    /// <inheritdoc />
    bool IDataEventProvider.DataEventsEnabled => true;

    /// <inheritdoc />
    bool IDataEventProvider.ApplyDataEventItemFilter(IDataItem item)
    {
      switch (item)
      {
        case DynamicModule _:
        case DynamicModuleType _:
        case FieldsBackendSection _:
          return true;
        default:
          return item is DynamicModuleField;
      }
    }

    [ApplyNoPolicies]
    private void ProcessEvents()
    {
      IList dirtyItems = this.GetDirtyItems();
      if (dirtyItems == null || dirtyItems.Count == 0)
        return;
      List<IModuleBuilderEvent> data = new List<IModuleBuilderEvent>();
      try
      {
        foreach (object itemInTransaction in (IEnumerable) dirtyItems)
        {
          SecurityConstants.TransactionActionType dirtyItemStatus = this.providerDecorator.GetDirtyItemStatus(itemInTransaction);
          DynamicModule module = itemInTransaction as DynamicModule;
          DynamicModuleType dynamicType = itemInTransaction as DynamicModuleType;
          FieldsBackendSection section = itemInTransaction as FieldsBackendSection;
          DynamicModuleField field = itemInTransaction as DynamicModuleField;
          DynamicContentProvider dynamicContentProvider = itemInTransaction as DynamicContentProvider;
          if (itemInTransaction is Permission permission)
            this.ProcessDataItemEvents((IDataItem) permission, dirtyItemStatus, ref data);
          else if (dynamicContentProvider != null)
          {
            dynamicContentProvider.Provider = (object) this;
            this.ProcessDataItemEvents((IDataItem) dynamicContentProvider, dirtyItemStatus, ref data);
          }
          else if (module != null)
          {
            module.Provider = (object) this;
            this.ProcessModuleEvents(module, dirtyItemStatus, ref data);
          }
          else if (dynamicType != null)
          {
            dynamicType.Provider = (object) this;
            this.ProcessTypeEvents(dynamicType, dirtyItemStatus, ref data);
          }
          else if (section != null)
          {
            section.Provider = (object) this;
            this.ProcessSectionEvents(section, dirtyItemStatus, ref data);
          }
          else if (field != null)
          {
            field.Provider = (object) this;
            this.ProcessFieldEvents(field, dirtyItemStatus, ref data);
          }
        }
      }
      catch (Exception ex)
      {
        this.RollbackTransaction();
        throw ex;
      }
      if (data.Count <= 0)
        return;
      this.SetExecutionStateData("module-builder-events", (object) data);
    }

    /// <summary>
    /// Processes the events for IDataItems through dedicated ModuleBuilder event factories.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="action">The action.</param>
    /// <param name="afterCommitEvents">The events that should be raised after the commit.</param>
    private void ProcessDataItemEvents(
      IDataItem dataItem,
      SecurityConstants.TransactionActionType action,
      ref List<IModuleBuilderEvent> afterCommitEvents)
    {
      string providerName = DataEventFactory.GetProviderName(dataItem.Provider);
      this.RaiseBeforeEvent((IEvent) ObjectFactory.Resolve<IModuleBuilderBeforeCommitEventFactory>().CreateEvent(dataItem, action, providerName));
      IModuleBuilderEvent moduleBuilderEvent = ObjectFactory.Resolve<IModuleBuilderAfterCommitEventFactory>().CreateEvent(dataItem, action, providerName);
      afterCommitEvents.Add(moduleBuilderEvent);
    }

    private void ProcessModuleEvents(
      DynamicModule module,
      SecurityConstants.TransactionActionType action,
      ref List<IModuleBuilderEvent> events)
    {
      IDynamicModuleEvent dynamicModuleEvent = (IDynamicModuleEvent) null;
      switch (action)
      {
        case SecurityConstants.TransactionActionType.New:
          this.RaiseBeforeEvent((IEvent) new DynamicModuleCreatingEvent(module));
          dynamicModuleEvent = (IDynamicModuleEvent) new DynamicModuleCreatedEvent(module);
          break;
        case SecurityConstants.TransactionActionType.Updated:
          DynamicModuleUpdatingEvent moduleUpdatingEvent = new DynamicModuleUpdatingEvent(module);
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) moduleUpdatingEvent, (IDataItem) module, (CultureInfo) null);
          this.RaiseBeforeEvent((IEvent) moduleUpdatingEvent);
          DynamicModuleUpdatedEvent evt = new DynamicModuleUpdatedEvent(module);
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) evt, (IDataItem) module, (CultureInfo) null);
          dynamicModuleEvent = (IDynamicModuleEvent) evt;
          break;
        case SecurityConstants.TransactionActionType.Deleted:
          this.RaiseBeforeEvent((IEvent) new DynamicModuleDeletingEvent(module));
          dynamicModuleEvent = (IDynamicModuleEvent) new DynamicModuleDeletedEvent(module);
          break;
      }
      if (dynamicModuleEvent == null)
        return;
      events.Add((IModuleBuilderEvent) dynamicModuleEvent);
    }

    private void ProcessTypeEvents(
      DynamicModuleType dynamicType,
      SecurityConstants.TransactionActionType action,
      ref List<IModuleBuilderEvent> events)
    {
      IDynamicModuleTypeEvent dynamicModuleTypeEvent = (IDynamicModuleTypeEvent) null;
      switch (action)
      {
        case SecurityConstants.TransactionActionType.New:
          this.RaiseBeforeEvent((IEvent) new DynamicModuleTypeCreatingEvent(dynamicType));
          dynamicModuleTypeEvent = (IDynamicModuleTypeEvent) new DynamicModuleTypeCreatedEvent(dynamicType);
          break;
        case SecurityConstants.TransactionActionType.Updated:
          DynamicModuleTypeUpdatingEvent typeUpdatingEvent = new DynamicModuleTypeUpdatingEvent(dynamicType);
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) typeUpdatingEvent, (IDataItem) dynamicType, (CultureInfo) null);
          this.RaiseBeforeEvent((IEvent) typeUpdatingEvent);
          DynamicModuleTypeUpdatedEvent evt = new DynamicModuleTypeUpdatedEvent(dynamicType);
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) evt, (IDataItem) dynamicType, (CultureInfo) null);
          dynamicModuleTypeEvent = (IDynamicModuleTypeEvent) evt;
          break;
        case SecurityConstants.TransactionActionType.Deleted:
          this.RaiseBeforeEvent((IEvent) new DynamicModuleTypeDeletingEvent(dynamicType));
          dynamicModuleTypeEvent = (IDynamicModuleTypeEvent) new DynamicModuleTypeDeletedEvent(dynamicType);
          break;
      }
      if (dynamicModuleTypeEvent == null)
        return;
      events.Add((IModuleBuilderEvent) dynamicModuleTypeEvent);
    }

    private void ProcessSectionEvents(
      FieldsBackendSection section,
      SecurityConstants.TransactionActionType action,
      ref List<IModuleBuilderEvent> events)
    {
      IFieldsBackendSectionEvent backendSectionEvent = (IFieldsBackendSectionEvent) null;
      switch (action)
      {
        case SecurityConstants.TransactionActionType.New:
          this.RaiseBeforeEvent((IEvent) new FieldsBackendSectionCreatingEvent(section));
          backendSectionEvent = (IFieldsBackendSectionEvent) new FieldsBackendSectionCreatedEvent(section);
          break;
        case SecurityConstants.TransactionActionType.Updated:
          FieldsBackendSectionUpdatingEvent sectionUpdatingEvent = new FieldsBackendSectionUpdatingEvent(section);
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) sectionUpdatingEvent, (IDataItem) section, (CultureInfo) null);
          this.RaiseBeforeEvent((IEvent) sectionUpdatingEvent);
          FieldsBackendSectionUpdatedEvent evt = new FieldsBackendSectionUpdatedEvent(section);
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) evt, (IDataItem) section, (CultureInfo) null);
          backendSectionEvent = (IFieldsBackendSectionEvent) evt;
          break;
        case SecurityConstants.TransactionActionType.Deleted:
          this.RaiseBeforeEvent((IEvent) new FieldsBackendSectionDeletingEvent(section));
          backendSectionEvent = (IFieldsBackendSectionEvent) new FieldsBackendSectionDeletedEvent(section);
          break;
      }
      if (backendSectionEvent == null)
        return;
      events.Add((IModuleBuilderEvent) backendSectionEvent);
    }

    private void ProcessFieldEvents(
      DynamicModuleField field,
      SecurityConstants.TransactionActionType action,
      ref List<IModuleBuilderEvent> events)
    {
      IDynamicModuleFieldEvent moduleFieldEvent = (IDynamicModuleFieldEvent) null;
      switch (action)
      {
        case SecurityConstants.TransactionActionType.New:
          this.RaiseBeforeEvent((IEvent) new DynamicModuleFieldCreatingEvent(field));
          moduleFieldEvent = (IDynamicModuleFieldEvent) new DynamicModuleFieldCreatedEvent(field);
          break;
        case SecurityConstants.TransactionActionType.Updated:
          DynamicModuleFieldUpdatingEvent fieldUpdatingEvent = new DynamicModuleFieldUpdatingEvent(field);
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) fieldUpdatingEvent, (IDataItem) field, (CultureInfo) null);
          this.RaiseBeforeEvent((IEvent) fieldUpdatingEvent);
          DynamicModuleFieldUpdatedEvent evt = new DynamicModuleFieldUpdatedEvent(field);
          DataEventFactory.SetChangedProperties((IPropertyChangeDataEvent) evt, (IDataItem) field, (CultureInfo) null);
          moduleFieldEvent = (IDynamicModuleFieldEvent) evt;
          break;
        case SecurityConstants.TransactionActionType.Deleted:
          this.RaiseBeforeEvent((IEvent) new DynamicModuleFieldDeletingEvent(field));
          moduleFieldEvent = (IDynamicModuleFieldEvent) new DynamicModuleFieldDeletedEvent(field);
          break;
      }
      if (moduleFieldEvent == null)
        return;
      events.Add((IModuleBuilderEvent) moduleFieldEvent);
    }

    private void RaiseEvents(bool throwExceptions)
    {
      if (!(this.GetExecutionStateData("module-builder-events") is List<IModuleBuilderEvent> executionStateData))
        return;
      object obj;
      this.TryGetExecutionStateData("EventOriginKey", out obj);
      foreach (IModuleBuilderEvent moduleBuilderEvent in executionStateData)
      {
        if (obj != null)
          moduleBuilderEvent.Origin = (string) obj;
        EventHub.Raise((IEvent) moduleBuilderEvent, throwExceptions);
      }
      this.SetExecutionStateData("module-builder-events", (object) null);
    }
  }
}
