// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.LanguagePackHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Routing;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  public class LanguagePackHttpHandler : IHttpHandler
  {
    private const string languagePackFileName = "languagePack.xlsx";
    private RequestContext requestContext;

    public LanguagePackHttpHandler(RequestContext requestContext) => this.requestContext = requestContext;

    public bool IsReusable => false;

    public void ProcessRequest(HttpContext context)
    {
      context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
      context.Response.AddHeader("Content-Disposition", "attachment; filename={0}".Arrange((object) "languagePack.xlsx"));
      this.GenerateResourceFile(context.Response.OutputStream);
    }

    private void GenerateResourceFile(Stream stream)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Create((Stream) memoryStream, SpreadsheetDocumentType.Workbook))
        {
          spreadsheetDocument.AddWorkbookPart();
          spreadsheetDocument.WorkbookPart.Workbook = new Workbook();
          spreadsheetDocument.WorkbookPart.AddNewPart<WorksheetPart>();
          Worksheet worksheet = new Worksheet();
          spreadsheetDocument.WorkbookPart.WorksheetParts.First<WorksheetPart>().Worksheet = worksheet;
          worksheet.Append(new OpenXmlElement[1]
          {
            (OpenXmlElement) this.GenerateSheetViews()
          });
          SheetData newChild1 = new SheetData();
          newChild1.Append((IEnumerable<OpenXmlElement>) this.CreateResourceRows());
          worksheet.AppendChild<SheetData>(newChild1);
          worksheet.Save();
          spreadsheetDocument.WorkbookPart.Workbook.AppendChild<Sheets>(new Sheets());
          Sheet newChild2 = new Sheet()
          {
            Id = (StringValue) spreadsheetDocument.WorkbookPart.GetIdOfPart((OpenXmlPart) spreadsheetDocument.WorkbookPart.WorksheetParts.First<WorksheetPart>()),
            SheetId = (UInt32Value) 1U,
            Name = (StringValue) "Resources"
          };
          spreadsheetDocument.WorkbookPart.Workbook.GetFirstChild<Sheets>().AppendChild<Sheet>(newChild2);
          spreadsheetDocument.WorkbookPart.Workbook.Save();
        }
        memoryStream.WriteTo(stream);
      }
    }

    /// <summary>
    /// Populates a collection of rows corresponding to each resource entry
    /// </summary>
    /// <returns></returns>
    private List<Row> CreateResourceRows()
    {
      CultureInfo culture = this.ResolveCulture();
      IQueryable<ResourceEntry> resources = Res.GetManager().GetResources(culture);
      List<Row> resourceRows = new List<Row>();
      Cell[] cellArray = new Cell[5];
      Cell cell1 = new Cell();
      cell1.CellValue = new CellValue("ClassId");
      cell1.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[0] = cell1;
      Cell cell2 = new Cell();
      cell2.CellValue = new CellValue("Key");
      cell2.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[1] = cell2;
      Cell cell3 = new Cell();
      cell3.CellValue = new CellValue("Value");
      cell3.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[2] = cell3;
      Cell cell4 = new Cell();
      cell4.CellValue = new CellValue("Description");
      cell4.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[3] = cell4;
      Cell cell5 = new Cell();
      cell5.CellValue = new CellValue("LastModified");
      cell5.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[4] = cell5;
      Row row = new Row((OpenXmlElement[]) cellArray);
      resourceRows.Add(row);
      foreach (ResourceEntry resource in (IEnumerable<ResourceEntry>) resources)
        resourceRows.Add(this.CreateContentRow(resource));
      return resourceRows;
    }

    /// <summary>Resolves the culture from the request context</summary>
    /// <returns></returns>
    private CultureInfo ResolveCulture()
    {
      CultureInfo cultureInfo = CultureInfo.InvariantCulture;
      try
      {
        string name = this.requestContext.HttpContext.Request.QueryString["lang"];
        if (!string.IsNullOrEmpty(name))
          cultureInfo = CultureInfo.GetCultureInfo(name);
      }
      catch
      {
      }
      return cultureInfo;
    }

    /// <summary>Create row with cells from the resource entry</summary>
    private Row CreateContentRow(ResourceEntry resource)
    {
      Cell[] cellArray = new Cell[5];
      Cell cell1 = new Cell();
      cell1.CellValue = new CellValue(resource.ClassId);
      cell1.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[0] = cell1;
      Cell cell2 = new Cell();
      cell2.CellValue = new CellValue(resource.Key);
      cell2.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[1] = cell2;
      Cell cell3 = new Cell();
      cell3.CellValue = new CellValue(resource.Value);
      cell3.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[2] = cell3;
      Cell cell4 = new Cell();
      cell4.CellValue = new CellValue(resource.Description);
      cell4.DataType = (EnumValue<CellValues>) CellValues.String;
      cellArray[3] = cell4;
      Cell cell5 = new Cell();
      cell5.CellValue = new CellValue(resource.LastModified.ToOADate().ToString((IFormatProvider) CultureInfo.InvariantCulture));
      cellArray[4] = cell5;
      return new Row((OpenXmlElement[]) cellArray);
    }

    /// <summary>Sheet view that "freezez" the first row</summary>
    private SheetViews GenerateSheetViews()
    {
      SheetViews sheetViews = new SheetViews();
      SheetView sheetView = new SheetView()
      {
        TabSelected = (BooleanValue) true,
        WorkbookViewId = (UInt32Value) 0U
      };
      Pane pane = new Pane()
      {
        VerticalSplit = (DoubleValue) 1.0,
        TopLeftCell = (StringValue) "A2",
        ActivePane = (EnumValue<PaneValues>) PaneValues.BottomLeft,
        State = (EnumValue<PaneStateValues>) PaneStateValues.Frozen
      };
      sheetView.Append(new OpenXmlElement[1]
      {
        (OpenXmlElement) pane
      });
      sheetViews.Append(new OpenXmlElement[1]
      {
        (OpenXmlElement) sheetView
      });
      return sheetViews;
    }
  }
}
