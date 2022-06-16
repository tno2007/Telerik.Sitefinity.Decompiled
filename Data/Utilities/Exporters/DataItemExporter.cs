// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Utilities.Exporters.DataItemExporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Data.Utilities.Exporters
{
  /// <summary>
  /// Represents base class for Sitefinity data items exporter.
  /// </summary>
  public static class DataItemExporter
  {
    /// <summary>
    /// Returns an exporer from the unity if specified for the specific format, or returns the default exporter
    /// </summary>
    /// <param name="fileFormatName"></param>
    /// <returns></returns>
    public static IDataItemExporter GetExporter(string fileFormatName) => ObjectFactory.IsTypeRegistered<IDataItemExporter>(fileFormatName) ? ObjectFactory.Resolve<IDataItemExporter>(fileFormatName) : ObjectFactory.Resolve<IDataItemExporter>();
  }
}
