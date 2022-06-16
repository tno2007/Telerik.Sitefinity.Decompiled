// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Utilities.Exporters.CsvExporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Utilities.Exporters
{
  /// <summary>
  /// Represents a CSV (Comma separated values) Sitefinity DataItem exporter.
  /// </summary>
  public class CsvExporter : DataItemExporterBase
  {
    private const string CsvMimeTypeName = "text/csv";
    private const char Separator = ',';
    /// <summary>
    /// Characters needing encoding: double quotation mark, comma, new line, carriage return
    /// </summary>
    private static readonly char[] CharsNeedingEncoding = "\",\n\r".ToCharArray();

    /// <summary>
    /// Writes the binary representation of the exported file to the specified stream.
    /// </summary>
    /// <param name="streamToExportTo">The stream to export to.</param>
    /// <param name="itemsToExport">The items to export.</param>
    /// <param name="encoding">The encoding to use when writing to the stream.</param>
    public override void ExportToStream(
      Stream streamToExportTo,
      IEnumerable<IDataItem> itemsToExport,
      Encoding encoding)
    {
      if (streamToExportTo == null)
        throw new ArgumentNullException(nameof (streamToExportTo));
      if (itemsToExport == null)
        throw new ArgumentNullException(nameof (itemsToExport));
      if (encoding == null)
        throw new ArgumentNullException(nameof (encoding));
      StringBuilder csvAsString = new StringBuilder();
      IEnumerator<IDataItem> enumerator = itemsToExport.GetEnumerator();
      IDataItem dataItem = enumerator.MoveNext() ? enumerator.Current : throw new ArgumentException("Can't export empty collection");
      PropertyDescriptorCollection itemProperties = this.GetItemProperties((object) dataItem);
      this.AppendHeader(csvAsString, itemProperties);
      this.AppendDataItemContent(csvAsString, dataItem, itemProperties);
      while (enumerator.MoveNext())
        this.AppendDataItemContent(csvAsString, enumerator.Current, itemProperties);
      byte[] bytes = encoding.GetBytes(csvAsString.ToString());
      streamToExportTo.Write(bytes, 0, bytes.Length);
    }

    /// <summary>
    /// Gets the MIME type name for the file that will be exported.
    /// </summary>
    /// <value>The type of the export format MIME.</value>
    public override string ExportFormatMimeType => "text/csv";

    /// <summary>Gets the extension of the file that will be exported.</summary>
    /// <value>The file extension.</value>
    public override string FileExtension => ".csv";

    private void AppendHeader(
      StringBuilder csvAsString,
      PropertyDescriptorCollection dataItemPropertyDescriptors)
    {
      bool flag = false;
      foreach (PropertyDescriptor propertyDescriptor in dataItemPropertyDescriptors)
      {
        if (flag)
          csvAsString.Append(',');
        csvAsString.Append(propertyDescriptor.Name);
        flag = true;
      }
      csvAsString.AppendLine();
    }

    private void AppendDataItemContent(
      StringBuilder csvAsString,
      IDataItem dataItem,
      PropertyDescriptorCollection dataItemPropertyDescriptors)
    {
      bool flag = false;
      foreach (PropertyDescriptor propertyDescriptor in dataItemPropertyDescriptors)
      {
        if (flag)
          csvAsString.Append(',');
        IDataItem component = dataItem;
        object obj = propertyDescriptor.GetValue((object) component);
        if (obj != null)
          this.AppendValueAsString(csvAsString, obj);
        flag = true;
      }
      csvAsString.AppendLine();
    }

    private void AppendValueAsString(StringBuilder csvAsString, object value)
    {
      string valueAsString = this.GetValueAsString(value);
      this.EncodeToExcelCsvAndAppend(csvAsString, valueAsString, false);
    }

    private void EncodeToExcelCsvAndAppend(StringBuilder csvAsString, string value, bool quoteall)
    {
      if (quoteall || value.IndexOfAny(CsvExporter.CharsNeedingEncoding) > -1)
      {
        value = value.Replace("\"", "\"\"");
        csvAsString.Append("\"");
        csvAsString.Append(value);
        csvAsString.Append("\"");
      }
      else
        csvAsString.Append(value);
    }
  }
}
