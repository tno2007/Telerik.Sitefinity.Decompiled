// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Content.IContentTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;
using Telerik.Sitefinity.Publishing;

namespace Telerik.Sitefinity.Packaging.Content
{
  /// <summary>
  /// Defines functionality for converting items from specific type in transferable format.
  /// </summary>
  internal interface IContentTransfer : IDataTransfer
  {
    /// <summary>
    /// Gets the area name for which the content processor is responsible
    /// </summary>
    string Area { get; }

    /// <summary>Gets the supported types</summary>
    IEnumerable<ExportType> SupportedTypes { get; }

    /// <summary>
    /// Gets the types on which every content type of the current content transfer depends
    /// </summary>
    IDictionary<string, IEnumerable<string>> Dependencies { get; }

    /// <summary>
    /// Determines whether the specified type can be processed.
    /// </summary>
    /// <param name="typeName">The name of the type.</param>
    /// <returns>True if module can be processed.</returns>
    bool AllowToProcess(string typeName);

    /// <summary>Extracts the items.</summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns>
    /// Collection of <see cref="T:Telerik.Sitefinity.Publishing.WrapperObject" />
    /// </returns>
    IEnumerable<WrapperObject> Export(ExportParams parameters);

    /// <summary>Gets the export folder name</summary>
    /// <param name="parameters">The parameters.</param>
    /// <returns>The export folder name</returns>
    string GetExportFolderName(ExportParams parameters);

    /// <summary>Creates the items into the system.</summary>
    /// <param name="transferableObjects">The transferable objects.</param>
    /// <param name="parameters">The parameters.</param>
    /// <param name="itemCreatedAction">The item created action.</param>
    /// <param name="itemFailedAction">The item failed action.</param>
    void Import(
      IEnumerable<WrapperObject> transferableObjects,
      ImportParams parameters,
      Action<WrapperObject, IEnumerable<ExportType>> itemCreatedAction,
      Action<WrapperObject, Exception> itemFailedAction);

    /// <summary>
    /// Counts the items for which this content transfer is responsible for.
    /// </summary>
    /// <param name="fileStream">The file stream.</param>
    /// <param name="operation">Scan operation.</param>
    void Count(Stream fileStream, ScanOperation operation);

    /// <summary>Deletes the data imported by add-on.</summary>
    /// <param name="sourceName">The source name.</param>
    void Delete(string sourceName);

    /// <summary>Gets supported types as list.</summary>
    /// <returns>Collection of supported types.</returns>
    IEnumerable<ExportType> GetSupportTypesList();
  }
}
