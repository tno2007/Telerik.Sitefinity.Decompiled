// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Utilities.Exporters.DataItemExporterBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Utilities.Exporters
{
  /// <summary>
  /// Represents the base class for the Sitefinity data item exporter;
  /// </summary>
  public abstract class DataItemExporterBase : IDataItemExporter
  {
    private string fileName = "Sitefinity Exported File";
    private const string ContentDispositionAttachemnt = "attachment;filename=";
    private const string ContentDispositionHeaderName = "Content-disposition";

    /// <summary>
    /// Gets or sets the name of the file that will be exported.
    /// </summary>
    /// <value>The name of the file.</value>
    /// <remarks>
    /// The file extension will be automatically appended.
    /// The default file name is 'Sitefinity Exported File'
    /// </remarks>
    public string FileName
    {
      get => this.fileName;
      set => this.fileName = value;
    }

    /// <summary>Gets the extension of the file that will be exported.</summary>
    /// <value>The file extension.</value>
    public abstract string FileExtension { get; }

    /// <summary>
    /// Gets the MIME type name for the file that will be exported.
    /// </summary>
    /// <value>The type of the export format MIME.</value>
    public abstract string ExportFormatMimeType { get; }

    /// <summary>
    /// Writes the binary representation of the exported file to the specified stream.
    /// </summary>
    /// <param name="streamToExportTo">The stream to export to.</param>
    /// <param name="itemsToExport">The items to export.</param>
    /// <param name="encoding">The encoding to use when writing to the stream.</param>
    public abstract void ExportToStream(
      Stream streamToExportTo,
      IEnumerable<IDataItem> itemsToExport,
      Encoding encoding);

    /// <summary>
    /// Configures the supplied <see cref="T:System.Web.HttpResponse" /> to correctly serve the file that will be exported.
    /// </summary>
    /// <param name="response">The <see cref="T:System.Web.HttpResponse" /> to configure.</param>
    public virtual void ConfigureResponse(HttpResponse response)
    {
      if (response == null)
        throw new ArgumentNullException(nameof (response));
      response.ContentType = this.ExportFormatMimeType;
      string str = "attachment;filename=" + this.FileName + this.FileExtension;
      response.AddHeader("Content-disposition", str);
    }

    /// <summary>
    /// Returns a collection of properties of the specified instance marked with DataMemberAttribute attribute.
    /// </summary>
    protected virtual PropertyDescriptorCollection GetItemProperties(
      object item)
    {
      return TypeDescriptor.GetProperties(item);
    }

    protected virtual string GetValueAsString(object value)
    {
      string valueAsString;
      if (value is string)
      {
        valueAsString = (string) value;
      }
      else
      {
        TypeConverter converter = TypeDescriptor.GetConverter(value);
        valueAsString = !converter.CanConvertTo(typeof (string)) ? value.ToString() : (string) converter.ConvertTo(value, typeof (string));
      }
      return valueAsString;
    }
  }
}
