// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.IDataProcessingEngine
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections;

namespace Telerik.Sitefinity.Data.DataProcessing
{
  /// <summary>
  /// The <see cref="T:Telerik.Sitefinity.Data.DataProcessing.IDataProcessingEngine" /> is the workhorse for processing inbound data to the database. It uses <see cref="T:Telerik.Sitefinity.Data.DataProcessing.Processors.IDataProcessor" /> to process the db bound object properties. Think of it as a middleware in the pipeline where data is being sent to the database.
  /// </summary>
  internal interface IDataProcessingEngine
  {
    /// <summary>
    /// Processes the <paramref name="items" /> properties values if new or modified before they get saved by the <paramref name="provider" />.
    /// </summary>
    /// <param name="provider">The provider.</param>
    /// <param name="items">The items.</param>
    void Process(DataProviderBase provider, IList items);
  }
}
