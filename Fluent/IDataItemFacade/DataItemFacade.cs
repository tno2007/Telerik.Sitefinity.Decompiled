// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.IDataItemFacade.DataItemFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Fluent.IDataItemFacade
{
  internal class DataItemFacade : Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade
  {
    private AppSettings settings;
    private IDataItem item;
    private Type itemType;
    private Type managerType;
    private string providerName;

    public DataItemFacade()
    {
    }

    public DataItemFacade(AppSettings settings, IDataItem item)
      : this(settings, item.GetType(), item)
    {
    }

    public DataItemFacade(AppSettings settings, Type itemType)
      : this(settings, itemType, (IDataItem) null)
    {
    }

    public DataItemFacade(AppSettings settings, Type itemType, IDataItem item) => this.SetInitialState(settings, itemType, item, new Guid?());

    public void SetInitialState(AppSettings settings, Type itemType, IDataItem item, Guid? itemID)
    {
      FacadeHelper.AssertArgumentNotNull<AppSettings>(settings, nameof (settings));
      this.settings = settings;
      this.itemType = itemType;
      if (item != null)
      {
        this.item = item;
      }
      else
      {
        if (!itemID.HasValue)
          return;
        this.InternalLoadItem(itemType, itemID);
      }
    }

    public void SetManagerType(Type managerType) => this.managerType = managerType;

    public void SetProviderName(string providerName) => this.providerName = providerName;

    public void SetItemType(Type itemType) => this.itemType = itemType;

    private void InternalLoadItem(Type itemType, Guid? itemID)
    {
      FacadeHelper.Assert<ArgumentException>(itemID.Value != Guid.Empty, "item ID is empty");
      this.item = (IDataItem) this.Manager.GetItem(itemType, itemID.Value);
      FacadeHelper.AssertArgumentNotNull<IDataItem>(this.item, "item");
    }

    public IManager Manager => this.managerType != (Type) null ? AllFacadesHelper.GetManagerInTransaction(this.settings, this.managerType, this.providerName) : AllFacadesHelper.GetMappedManager(this.settings, this.itemType);

    public bool SaveChanges() => AllFacadesHelper.SaveChanges(this.settings, this.Manager);

    public bool CancelChanges() => AllFacadesHelper.CancelChanges(this.settings);

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade SaveAndContinue()
    {
      AllFacadesHelper.SaveChanges(this.settings, this.Manager);
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this;
    }

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade CancelAndContinue()
    {
      AllFacadesHelper.CancelChanges(this.settings);
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this;
    }

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade CreateNew() => this.CreateNew(this.Manager.Provider.GetNewGuid());

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade CreateNew(
      Guid itemId)
    {
      this.item = (IDataItem) this.Manager.CreateItem(this.itemType, itemId);
      this.settings.TrackModifiedItem(this.item);
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this;
    }

    public ILanguageDataManagerDataItemFacade AsLifeCycleItem() => (ILanguageDataManagerDataItemFacade) new LanguageDataManagerDataItemFacade((ILifecycleDataItem) this.item, (ILanguageDataManager) this.Manager);

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade Delete(
      CultureInfo language = null)
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      if (language == null)
        this.settings.TrackFullyDeletedItem(this.item);
      else
        this.settings.TrackDeletedItem(this.item);
      this.Manager.DeleteItem((object) this.item, language);
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this;
    }

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade Do(
      Action<IDataItem> setAction)
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      setAction(this.item);
      if (this.item is Content)
        CommonMethods.RecompileItemUrls((Content) this.item, this.Manager);
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this;
    }

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DoAs<T>(
      Action<T> setAction)
      where T : IDataItem
    {
      FacadeHelper.Assert(this.item != null, "Not initialized item");
      setAction((T) this.item);
      if (this.item is Content)
        CommonMethods.RecompileItemUrls((Content) this.item, this.Manager);
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this;
    }

    public IDataItem Get() => this.item;

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade Set(
      IDataItem itemToSet)
    {
      FacadeHelper.AssertArgumentNotNull<IDataItem>(itemToSet, "item");
      FacadeHelper.Assert(itemToSet.GetType() == this.itemType, "Cannot change facade item type");
      this.item = itemToSet;
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this;
    }

    public bool Exists(Guid Id)
    {
      try
      {
        if (this.Manager.GetItem(this.itemType, Id) != null)
          return true;
      }
      catch (Exception ex)
      {
      }
      return false;
    }

    public Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade Load(Guid itemId)
    {
      this.InternalLoadItem(this.itemType, new Guid?(itemId));
      return (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this;
    }

    public ILocatableFacade<Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade> AsLocatable() => (ILocatableFacade<Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade>) new LocatableFacade<Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade>(this.settings, this.itemType, (ILocatable) this.item, (IContentManager) this.Manager, (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) this);
  }
}
