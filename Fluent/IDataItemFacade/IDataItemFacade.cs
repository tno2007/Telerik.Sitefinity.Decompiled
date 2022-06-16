// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent.IDataItemFacade
{
  public interface IDataItemFacade
  {
    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade CancelAndContinue();

    bool CancelChanges();

    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade CreateNew();

    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade CreateNew(Guid itemId);

    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade Load(Guid itemId);

    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade Delete(
      CultureInfo language = null);

    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade Do(
      Action<IDataItem> setAction);

    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade DoAs<T>(
      Action<T> setAction)
      where T : IDataItem;

    IDataItem Get();

    IManager Manager { get; }

    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade SaveAndContinue();

    bool SaveChanges();

    Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade Set(IDataItem itemToSet);

    void SetInitialState(AppSettings settings, Type itemType, IDataItem item, Guid? itemID);

    bool Exists(Guid Id);

    void SetManagerType(Type managerType);

    void SetProviderName(string providerName);

    void SetItemType(Type itemType);

    ILocatableFacade<Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade> AsLocatable();

    ILanguageDataManagerDataItemFacade AsLifeCycleItem();
  }
}
