// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataProcessing.Processors.IDataProcessor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Processors;

namespace Telerik.Sitefinity.Data.DataProcessing.Processors
{
  /// <summary>
  /// Interface for data processors. They are used by <see cref="T:Telerik.Sitefinity.Data.DataProcessing.IDataProcessingEngine" /> to process data before it gets sent to the database.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Processors.IProcessor" />
  public interface IDataProcessor : IProcessor
  {
    /// <summary>
    /// The method receives the new or modified field value that is inbound to the DB.
    /// </summary>
    /// <param name="value">The value that should be processed.</param>
    void Process(ref object value);

    /// <summary>
    /// Called when a property is filtered for processing by the processor. Criteria for processing should be defined here.
    /// </summary>
    /// <param name="prop">The property that is being filtered for processing.</param>
    /// <param name="type">The type that is being filtered for processing and contains the property.</param>
    /// <returns>
    /// True if property value should be processed by this processor, false if it should be skipped.
    /// </returns>
    bool ShouldProcess(PropertyDescriptor prop, Type type);
  }
}
