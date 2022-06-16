// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Utilities.Exporters.IDataItemExporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Utilities.Exporters
{
  /// <summary>
  /// Represents a common interface for all Sitefinity DataItem exporters
  /// </summary>
  public interface IDataItemExporter
  {
    /// <summary>
    /// Writes the binary representation of the exported file to the specified stream.
    /// </summary>
    /// <param name="streamToExportTo">The stream to export to.</param>
    /// <param name="itemsToExport">The items to export.</param>
    /// <param name="encoding">The encoding to use when writing to the stream.</param>
    void ExportToStream(
      Stream streamToExportTo,
      IEnumerable<IDataItem> itemsToExport,
      Encoding encoding);

    /// <summary>
    /// Configures the supplied <see cref="T:System.Web.HttpResponse" /> to correctly serve the file that will be exported.
    /// </summary>
    void ConfigureResponse(HttpResponse response);

    /// <summary>
    /// Gets the MIME type name for the file that will be exported.
    /// </summary>
    /// <value>The type of the export format MIME.</value>
    string ExportFormatMimeType { get; }

    /// <summary>
    /// Gets or sets the name of the file that will be exported.
    /// </summary>
    /// <value>The name of the file.</value>
    string FileName { get; set; }

    /// <summary>Gets the extension of the file that will be exported.</summary>
    /// <value>The file extension.</value>
    string FileExtension { get; }
  }
}
