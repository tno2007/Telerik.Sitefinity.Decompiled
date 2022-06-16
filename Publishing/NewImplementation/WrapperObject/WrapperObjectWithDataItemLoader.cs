// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.WrapperObjectWithDataItemLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// <see cref="T:Telerik.Sitefinity.Publishing.WrapperObject" /> implementation, which loads the <see cref="P:Telerik.Sitefinity.Publishing.WrapperObjectWithDataItemLoader.WrappedObject" /> on demand.
  /// </summary>
  internal class WrapperObjectWithDataItemLoader : WrapperObject
  {
    private const string defaultTransactionName = "sf_PublishingItems";

    public WrapperObjectWithDataItemLoader()
      : base((object) null)
    {
    }

    public Guid ItemId { get; set; }

    public string ProviderName { get; set; }

    public string TransactionName { get; set; }

    public PublishingSystemEventInfo parent { get; set; }

    public Type ManagerType { get; set; }

    public override object WrappedObject
    {
      get
      {
        if (base.WrappedObject == null)
        {
          string transactionName = this.TransactionName ?? "sf_PublishingItems" + Guid.NewGuid().ToString();
          IManager managerInTransaction;
          if (this.ManagerType != (Type) null)
          {
            managerInTransaction = ManagerBase.GetManagerInTransaction(this.ManagerType, this.ProviderName, transactionName);
          }
          else
          {
            try
            {
              managerInTransaction = ManagerBase.GetMappedManagerInTransaction(TypeResolutionService.ResolveType(this.parent.ItemType), this.ProviderName, transactionName);
            }
            catch (Exception ex)
            {
              return (object) null;
            }
          }
          base.WrappedObject = (object) new WrapperObject(managerInTransaction.GetItem(TypeResolutionService.ResolveType(this.parent.ItemType), this.ItemId))
          {
            Language = this.Language
          };
        }
        return base.WrappedObject;
      }
      set => base.WrappedObject = value;
    }
  }
}
