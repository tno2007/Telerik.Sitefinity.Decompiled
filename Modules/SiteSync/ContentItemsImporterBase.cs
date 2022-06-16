// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ContentItemsImporterBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.SiteSync
{
  internal class ContentItemsImporterBase : SiteSyncImporter
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.ContentItemsImporterBase" /> class.
    /// </summary>
    public ContentItemsImporterBase()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.ContentItemsImporterBase" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public ContentItemsImporterBase(string registrationPrefix) => this.RegistrationPrefix = registrationPrefix;

    internal override void ImportItemInternal(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction,
      Action<IDataItem, WrapperObject, IManager> postProcessingAction)
    {
      if (itemType == typeof (TaxonomyStatistic))
      {
        this.ImportTaxonomyStatistic(transactionName, itemId, (object) item, provider);
      }
      else
      {
        base.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, postProcessingAction);
        this.PrepareStatisticsToRemove(transactionName, itemType, itemId, provider);
      }
    }

    protected override void ImportItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction)
    {
      this.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, (Action<IDataItem, WrapperObject, IManager>) null);
    }
  }
}
