// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Structure.IStructureTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.IO;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Packaging.Package;

namespace Telerik.Sitefinity.Packaging.Structure
{
  /// <summary>
  /// Interface providing methods to Export the structure of the modules in Sitefinity
  /// </summary>
  internal interface IStructureTransfer : IDataTransfer
  {
    /// <summary>Exports the module structure to specific directory</summary>
    /// <param name="configuration">Configuration of the modules that needs to be exported</param>
    /// <returns>Collection of exported package transfer objects</returns>
    IEnumerable<IPackageTransferObject> Export(
      IDictionary<string, string> configuration);

    /// <summary>
    /// Checks whether files in a directory can be deleted using this IStructureTransfer object
    /// </summary>
    /// <param name="directory">The directory</param>
    /// <returns>Boolean value</returns>
    bool AllowToDelete(string directory);

    /// <summary>Imports module structure</summary>
    /// <param name="packageTransferObjects">Package objects to be imported.</param>
    void Import(
      IEnumerable<IPackageTransferObject> packageTransferObjects);

    /// <summary>Deletes module structure</summary>
    /// <param name="moduleName">The Name of the module</param>
    /// <param name="moduleDeleted">if set to <c>true</c> [module deleted].</param>
    void Delete(string moduleName, out bool moduleDeleted);

    /// <summary>Uninstalls the specified source.</summary>
    /// <param name="sourceName">Name of the source.</param>
    void Uninstall(string sourceName);

    /// <summary>Called on import complete</summary>
    void ImportCompleted();

    /// <summary>
    /// Gets the group name of the group of folders for which the structure transfer is responsible
    /// </summary>
    string GroupName { get; }

    /// <summary>
    /// Gets the area name for which the structure transfer is responsible if there is no group name set
    /// </summary>
    string Area { get; }

    /// <summary>
    /// Gets the Areas on which the current structure transfer depends
    /// </summary>
    IEnumerable<string> Dependencies { get; }

    /// <summary>Gets the supported types</summary>
    IEnumerable<ExportType> SupportedTypes { get; }

    /// <summary>Gets or sets the transfer export mode</summary>
    ExportMode ExportMode { get; set; }

    /// <summary>
    /// Counts the items for which this structure transfer is responsible for.
    /// </summary>
    /// <param name="directory">Directory info.</param>
    /// <param name="operation">Scan operation.</param>
    /// <param name="type">The addon entry type.</param>
    void Count(DirectoryInfo directory, ScanOperation operation, AddOnEntryType type);
  }
}
