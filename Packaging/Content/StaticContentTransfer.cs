// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Content.StaticContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.SiteSync;
using Telerik.Sitefinity.SiteSync.Serialization;

namespace Telerik.Sitefinity.Packaging.Content
{
  /// <summary>
  /// Implements functionality for converting items from dynamic content in transferable format.
  /// </summary>
  internal abstract class StaticContentTransfer : ContentTransferBase
  {
    private Lazy<ContentItemsImporterBase> itemsImporter = new Lazy<ContentItemsImporterBase>((Func<ContentItemsImporterBase>) (() =>
    {
      return new ContentItemsImporterBase("Export/Import")
      {
        Serializer = (ISiteSyncSerializer) new SiteSyncSerializer("Export/Import")
      };
    }));

    /// <inheritdoc />
    public override SiteSyncImporter ItemsImporter => (SiteSyncImporter) this.itemsImporter.Value;

    /// <summary>Handles the parent not found action.</summary>
    /// <param name="item">The item.</param>
    internal virtual void HandleParentNotFoundAction(WrapperObject item)
    {
    }

    /// <summary>
    /// Sets the item additional values prior the transaction is completed.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="component">The component.</param>
    /// <param name="properties">The properties.</param>
    /// <param name="fluent">The fluent.</param>
    /// <param name="transaction">The transaction.</param>
    internal virtual void SetAdditionalValues(
      IDataItem dataItem,
      string provider,
      object component,
      PropertyDescriptorCollection properties,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction transaction)
    {
    }
  }
}
