// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Utilities.Exporters.ExcelExporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Xml;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Data.Utilities.Exporters
{
  public class ExcelExporter : DataItemExporterBase
  {
    private IEnumerable<IDataItem> items;
    private string spreadsheetName;
    private const string ExcelFileExtension = ".xlsx";
    private const string ExcelMimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    private const string DefaultSheetName = "Sheet1";
    private bool overrideSheetName;
    private bool overrideItemsType;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ExcelExporterBase&lt;T&gt;" /> class.
    /// </summary>
    public ExcelExporter() => this.items = (IEnumerable<IDataItem>) new List<IDataItem>();

    /// <summary>Gets the name of the spread sheet.</summary>
    /// <value>The name of the spread sheet.</value>
    public string SpreadSheetName
    {
      get
      {
        if (this.spreadsheetName == null)
          this.spreadsheetName = SystemManager.CurrentHttpContext.Items[(object) "SheetName"] == null ? "Sheet1" : (string) SystemManager.CurrentHttpContext.Items[(object) "SheetName"];
        return this.spreadsheetName;
      }
    }

    /// <summary>
    /// Gets the extension of the excel file that will be exported.
    /// </summary>
    /// <value>The file extension.</value>
    public override string FileExtension => ".xlsx";

    /// <summary>
    /// Gets the MIME type name for the excel file that will be exported.
    /// </summary>
    /// <value>The type of the export format MIME.</value>
    public override string ExportFormatMimeType => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

    /// <summary>
    /// Writes the binary representation of the exported excel file to the specified stream.
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
      using (MemoryStream memoryStream = new MemoryStream())
      {
        this.Export((Stream) memoryStream, itemsToExport);
        memoryStream.WriteTo(streamToExportTo);
      }
    }

    /// <summary>Gets the value of the specified object as string.</summary>
    /// <param name="value">The value.</param>
    protected override string GetValueAsString(object value)
    {
      string valueAsString;
      if (value is string)
      {
        valueAsString = (string) value;
      }
      else
      {
        TypeConverter converter = TypeDescriptor.GetConverter(value);
        valueAsString = !(value is IEnumerable<string>) ? (!(value is IEnumerable<Guid>) ? (!(value is IEnumerable<ChoiceOption>) ? (!converter.CanConvertTo(typeof (string)) ? value.ToString() : (string) converter.ConvertTo(value, typeof (string))) : string.Join(",", ((IEnumerable<ChoiceOption>) value).Select<ChoiceOption, string>((Func<ChoiceOption, string>) (co => co.ToString())))) : string.Join<Guid>(",", (IEnumerable<Guid>) value)) : string.Join(",", (IEnumerable<string>) ((IEnumerable<string>) value).ToList<string>());
      }
      return valueAsString;
    }

    protected internal virtual void Export(Stream stream, IEnumerable<IDataItem> itemsToExport)
    {
      IEnumerable<IGrouping<Type, IDataItem>> groupings = itemsToExport.GroupBy<IDataItem, Type>((Func<IDataItem, Type>) (i => i.GetType()));
      this.CheckContextVariables();
      using (SpreadsheetDocument spreadSheet = SpreadsheetDocument.Create(stream, SpreadsheetDocumentType.Workbook))
      {
        spreadSheet.AddWorkbookPart();
        spreadSheet.WorkbookPart.Workbook = new Workbook();
        spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
        spreadSheet.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
        foreach (IGrouping<Type, IDataItem> grouping in groupings)
        {
          this.items = (IEnumerable<IDataItem>) grouping;
          this.SetSheetName(grouping.Key);
          string itemsTypeFullName = this.GetItemsTypeFullName(grouping.Key);
          this.AddItemsToSpreadSheet(spreadSheet, itemsTypeFullName);
        }
      }
    }

    protected internal virtual void AddItemsToSpreadSheet(
      SpreadsheetDocument spreadSheet,
      string itemsType)
    {
      WorksheetPart part = spreadSheet.WorkbookPart.AddNewPart<WorksheetPart>();
      List<Row> rows = this.CreateRows(itemsType);
      SheetData newChild1 = new SheetData();
      newChild1.Append((IEnumerable<OpenXmlElement>) rows);
      part.Worksheet = new Worksheet();
      part.Worksheet.AppendChild<SheetData>(newChild1);
      part.Worksheet.Save();
      Sheets firstChild = spreadSheet.WorkbookPart.Workbook.GetFirstChild<Sheets>();
      string idOfPart = spreadSheet.WorkbookPart.GetIdOfPart((OpenXmlPart) part);
      uint num = firstChild == null || firstChild.Count<OpenXmlElement>() <= 0 ? 1U : firstChild.Elements<Sheet>().Select<Sheet, uint>((Func<Sheet, uint>) (s => s.SheetId.Value)).Max<uint>() + 1U;
      Sheet newChild2 = new Sheet()
      {
        Id = (StringValue) idOfPart,
        SheetId = (UInt32Value) num,
        Name = (StringValue) this.SpreadSheetName
      };
      firstChild.AppendChild<Sheet>(newChild2);
      spreadSheet.WorkbookPart.Workbook.Save();
    }

    protected internal virtual List<Row> CreateRows(string itemsType)
    {
      List<Row> rows = new List<Row>();
      if (this.items.Count<IDataItem>() <= 0)
        return rows;
      PropertyDescriptorCollection itemProperties = this.GetItemProperties((object) this.items.First<IDataItem>());
      List<Cell> headersCells = this.CreateHeadersCells(itemProperties, itemsType);
      rows.Add(new Row((IEnumerable<OpenXmlElement>) headersCells));
      foreach (IDataItem dataItem in this.items)
        rows.Add(this.CreateContentRow(dataItem, itemProperties));
      return rows;
    }

    protected internal virtual Row CreateContentRow(
      IDataItem item,
      PropertyDescriptorCollection dataItemPropertyDescriptors)
    {
      List<Cell> childElements = new List<Cell>();
      foreach (PropertyDescriptor propertyDescriptor in dataItemPropertyDescriptors)
      {
        if (!this.ShouldBeSkipped(propertyDescriptor))
        {
          if (propertyDescriptor is DynamicLstringPropertyDescriptor)
          {
            foreach (CultureInfo frontendLanguage in AppSettings.CurrentSettings.DefinedFrontendLanguages)
            {
              string text = XmlConvert.EncodeNmToken(((LstringPropertyDescriptor) propertyDescriptor).GetStringExplicitFallback((object) item, frontendLanguage, false));
              Cell cell1 = new Cell();
              cell1.CellValue = new CellValue(text);
              cell1.DataType = (EnumValue<CellValues>) CellValues.String;
              Cell cell2 = cell1;
              childElements.Add(cell2);
            }
          }
          else
          {
            string text = XmlConvert.EncodeNmToken(this.GetCellValue(propertyDescriptor, item));
            Cell cell3 = new Cell();
            cell3.CellValue = new CellValue(text);
            cell3.DataType = (EnumValue<CellValues>) CellValues.String;
            Cell cell4 = cell3;
            childElements.Add(cell4);
          }
        }
      }
      return new Row((IEnumerable<OpenXmlElement>) childElements);
    }

    protected internal virtual List<Cell> CreateHeadersCells(
      PropertyDescriptorCollection dataItemPropertyDescriptors,
      string itemsType)
    {
      List<Cell> cellList1 = new List<Cell>();
      List<IFormFieldControl> fieldControls = itemsType == null || !SystemManager.IsModuleEnabled("Forms") ? (List<IFormFieldControl>) null : ExcelExporter.GetFieldControls(itemsType);
      if (fieldControls != null)
      {
        this.AddCellsFromFieldControls(dataItemPropertyDescriptors, fieldControls, cellList1);
      }
      else
      {
        foreach (PropertyDescriptor propertyDescriptor in dataItemPropertyDescriptors)
        {
          if (!this.ShouldBeSkipped(propertyDescriptor))
          {
            if (propertyDescriptor is DynamicLstringPropertyDescriptor)
            {
              foreach (CultureInfo frontendLanguage in AppSettings.CurrentSettings.DefinedFrontendLanguages)
              {
                List<Cell> cellList2 = cellList1;
                Cell cell = new Cell();
                cell.CellValue = new CellValue(string.Format("{0}_{1}", (object) propertyDescriptor.Name, (object) frontendLanguage.Name));
                cell.DataType = (EnumValue<CellValues>) CellValues.String;
                cellList2.Add(cell);
              }
            }
            else
            {
              List<Cell> cellList3 = cellList1;
              Cell cell = new Cell();
              cell.CellValue = new CellValue(propertyDescriptor.Name);
              cell.DataType = (EnumValue<CellValues>) CellValues.String;
              cellList3.Add(cell);
            }
          }
        }
      }
      return cellList1;
    }

    private static List<IFormFieldControl> GetFieldControls(string entryType)
    {
      FormsManager manager = FormsManager.GetManager();
      string entryName = entryType.Substring("Telerik.Sitefinity.DynamicTypes.Model".Length + 1);
      FormDescription securedObject = manager.GetItems<FormDescription>().FirstOrDefault<FormDescription>((Expression<Func<FormDescription, bool>>) (fd => fd.Name == entryName));
      List<IFormFieldControl> fieldControls;
      if (securedObject != null)
      {
        securedObject.Demand(SecurityActionTypes.View);
        fieldControls = new List<IFormFieldControl>();
        IControlBehaviorResolver behaviorResolver = ObjectFactory.Resolve<IControlBehaviorResolver>();
        foreach (FormControl control1 in (IEnumerable<FormControl>) securedObject.Controls)
        {
          System.Web.UI.Control control2 = manager.LoadControl((ObjectData) control1, (CultureInfo) null);
          if (control2 != null && behaviorResolver.GetBehaviorObject(control2) is IFormFieldControl behaviorObject)
            fieldControls.Add(behaviorObject);
        }
      }
      else
        fieldControls = (List<IFormFieldControl>) null;
      return fieldControls;
    }

    private void AddCellsFromFieldControls(
      PropertyDescriptorCollection dataItemPropertyDescriptors,
      List<IFormFieldControl> fieldControls,
      List<Cell> cellList)
    {
      foreach (PropertyDescriptor propertyDescriptor in dataItemPropertyDescriptors)
      {
        if (!this.ShouldBeSkipped(propertyDescriptor))
        {
          string fieldControlValue = this.GetFieldControlValue(propertyDescriptor, fieldControls);
          List<Cell> cellList1 = cellList;
          Cell cell = new Cell();
          cell.CellValue = new CellValue(fieldControlValue);
          cell.DataType = (EnumValue<CellValues>) CellValues.String;
          cellList1.Add(cell);
        }
      }
    }

    private string GetFieldControlValue(
      PropertyDescriptor descriptor,
      List<IFormFieldControl> fieldControls)
    {
      string[] strArray = descriptor.Name.Split('_');
      string id = strArray[strArray.Length - 1];
      FieldControl fieldControl1 = fieldControls.OfType<FieldControl>().FirstOrDefault<FieldControl>((Func<FieldControl, bool>) (fc => fc.ID == id));
      if (fieldControl1 != null)
        return fieldControl1.Title;
      if (fieldControls.FirstOrDefault<IFormFieldControl>((Func<IFormFieldControl, bool>) (ffc => ffc.MetaField != null && ffc.MetaField.FieldName == descriptor.Name)) is FieldControl fieldControl2)
        return fieldControl2.Title;
      IMetaField metaField = fieldControls.Select<IFormFieldControl, IMetaField>((Func<IFormFieldControl, IMetaField>) (f => f.MetaField)).FirstOrDefault<IMetaField>((Func<IMetaField, bool>) (m => m != null && m.FieldName == descriptor.Name && !m.Title.IsNullOrEmpty()));
      return metaField != null ? metaField.Title : descriptor.Name;
    }

    private string GetCellValue(PropertyDescriptor descriptor, IDataItem item)
    {
      string cellValue;
      switch (descriptor)
      {
        case TaxonomyPropertyDescriptor _:
          cellValue = this.GetTaxonomyValue((TaxonomyPropertyDescriptor) descriptor, item);
          break;
        case MetafieldPropertyDescriptor _:
          cellValue = this.GetMetafieldValue((MetafieldPropertyDescriptor) descriptor, item);
          break;
        default:
          cellValue = this.GetDefaultCellValue(descriptor, item);
          break;
      }
      return cellValue;
    }

    protected internal virtual string GetDefaultCellValue(
      PropertyDescriptor descriptor,
      IDataItem item)
    {
      object obj = descriptor.GetValue((object) item);
      return obj == null ? string.Empty : this.GetValueAsString(obj);
    }

    private string GetMetafieldValue(MetafieldPropertyDescriptor descriptor, IDataItem item)
    {
      if (!(descriptor.GetValue((object) item) is ContentLink[] contentLinkArray))
        return this.GetDefaultCellValue((PropertyDescriptor) descriptor, item);
      if (contentLinkArray.Length == 0)
        return string.Empty;
      try
      {
        LibrariesManager manager = LibrariesManager.GetManager();
        StringBuilder stringBuilder = new StringBuilder();
        int num = 0;
        foreach (ContentLink contentLink in contentLinkArray)
        {
          object obj = manager.GetItem(TypeResolutionService.ResolveType(contentLink.ChildItemType), contentLink.ChildItemId);
          if (obj != null && obj is MediaContent)
          {
            if (num > 0)
              stringBuilder.Append(",");
            ++num;
            stringBuilder.Append(((MediaContent) obj).MediaUrl);
          }
        }
        return stringBuilder.ToString();
      }
      catch
      {
        return string.Empty;
      }
    }

    private string GetTaxonomyValue(
      TaxonomyPropertyDescriptor taxonomyPropertyDescriptor,
      IDataItem item)
    {
      return TaxonomiesHelper.GetTaxonomiesNames(((IEnumerable<Guid>) taxonomyPropertyDescriptor.GetValue((object) item)).ToArray<Guid>());
    }

    protected internal virtual bool ShouldBeSkipped(PropertyDescriptor propertyDescriptor)
    {
      Type attributeType = typeof (SkipExportProperty);
      if (!(propertyDescriptor.Attributes[attributeType] is SkipExportProperty))
      {
        switch (propertyDescriptor)
        {
          case RelatedDataPropertyDescriptor _:
          case TypeSuccessorsPropertyDescriptor _:
            break;
          default:
            return propertyDescriptor is DynamicContentParentPropertyDescriptor;
        }
      }
      return true;
    }

    private void CheckContextVariables()
    {
      this.overrideItemsType = string.IsNullOrEmpty(SystemManager.CurrentHttpContext.Items[(object) "ExportingItemsTypeName"] as string);
      this.overrideSheetName = string.IsNullOrEmpty(SystemManager.CurrentHttpContext.Items[(object) "SheetName"] as string);
    }

    private string GetItemsTypeFullName(Type itemsType) => this.overrideItemsType ? itemsType.FullName : SystemManager.CurrentHttpContext.Items[(object) "ExportingItemsTypeName"] as string;

    private void SetSheetName(Type itemsType)
    {
      if (!this.overrideSheetName)
        return;
      this.spreadsheetName = itemsType.Name;
    }
  }
}
