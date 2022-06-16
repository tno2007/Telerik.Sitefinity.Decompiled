// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.ImportLanguagePackDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>Dialog for importing language packs.</summary>
  public class ImportLanguagePackDialog : AjaxDialogBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Localization.ImportLanguagePack.ascx");
    public const string blockUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery.blockUI.js";
    public const string importLanguagePackDialogScript = "Telerik.Sitefinity.Localization.Web.UI.Scripts.ImportLanguagePackDialog.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ImportLanguagePackDialog.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public override string ClientComponentType => typeof (ImportLanguagePackDialog).FullName;

    /// <summary>
    /// Gets the reference to the control that displays the list of available
    /// languages in the Sitefinity installation.
    /// </summary>
    protected virtual DropDownList LanguagesDropDown => this.Container.GetControl<DropDownList>("languagesDropDown", true);

    /// <summary>
    /// Gets the reference to the control that uploads the file with the language pack.
    /// </summary>
    protected virtual RadUpload LanguagePackUpload => this.Container.GetControl<RadUpload>("languagePackUpload", true);

    /// <summary>
    /// Gets the reference to the control the button that imports selected language pack.
    /// </summary>
    protected virtual Label LblLanguagesDropDown => this.Container.GetControl<Label>("lblLanguagesDropDown", true);

    /// <summary>
    /// Gets the reference to the control the button that imports selected language pack.
    /// </summary>
    protected virtual LinkButton ImportLanguagePackButton => this.Container.GetControl<LinkButton>("importLanguagePackButton", true);

    /// <summary>
    /// Gets the reference to the control the button that cancels language pack importing.
    /// </summary>
    protected virtual LinkButton cancel => this.Container.GetControl<LinkButton>(nameof (cancel), true);

    protected virtual HtmlGenericControl importWindowWrapper => this.Container.GetControl<HtmlGenericControl>(nameof (importWindowWrapper), true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ImportLanguagePackButton.Click += new EventHandler(this.ImportLanguagePackButton_Click);
      this.BindAvailableLanguages();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Handles the Click event of the ImportLanguagePackButton control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    protected virtual void ImportLanguagePackButton_Click(object sender, EventArgs e)
    {
      CultureInfo culture = AppSettings.CurrentSettings.AllLanguages.Count <= 1 ? CultureInfo.InvariantCulture : new CultureInfo(this.LanguagesDropDown.SelectedValue);
      if (this.LanguagePackUpload.UploadedFiles.Count <= 0)
        return;
      this.ImportResourcesFromXlsx(this.LanguagePackUpload.UploadedFiles[0], culture);
      CacheDependency.NotifyAll();
    }

    /// <summary>Imports resources from a XLSX file</summary>
    /// <param name="languagePackFile"></param>
    /// <param name="culture"></param>
    protected virtual void ImportResourcesFromXlsx(
      UploadedFile languagePackFile,
      CultureInfo culture)
    {
      using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(languagePackFile.InputStream, false))
      {
        ResourceManager manager = ResourceManager.GetManager();
        IEnumerable<Row> source = spreadsheetDocument.WorkbookPart.WorksheetParts.First<WorksheetPart>().Worksheet.Elements<SheetData>().First<SheetData>().Elements<Row>();
        SharedStringTablePart sharedStringTablePart = spreadsheetDocument.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault<SharedStringTablePart>();
        int num = 0;
        for (int index = 1; index < source.Count<Row>(); ++index)
        {
          Row row = source.ElementAt<Row>(index);
          try
          {
            ++num;
            string cellValue1 = this.GetCellValue(row.ChildElements[0] as Cell, sharedStringTablePart);
            ++num;
            string cellValue2 = this.GetCellValue(row.ChildElements[1] as Cell, sharedStringTablePart);
            ++num;
            string str = this.GetCellValue(row.ChildElements[2] as Cell, sharedStringTablePart);
            ++num;
            string cellValue3 = this.GetCellValue(row.ChildElements[3] as Cell, sharedStringTablePart);
            ++num;
            this.GetCellValue(row.ChildElements[4] as Cell, sharedStringTablePart);
            if (!string.IsNullOrEmpty(cellValue1))
            {
              if (str != null)
                str = str.Trim();
              manager.AddItem(culture, cellValue1, cellValue2, str, cellValue3);
            }
            num = 0;
          }
          catch (Exception ex)
          {
            throw new ApplicationException(string.Format("Error reading row {0}, cell {1}. Exception message: {2}", (object) (index + 1), (object) num, (object) ex.Message));
          }
        }
        manager.SaveChanges();
      }
    }

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddElementProperty("importLanguagePackButton", this.ImportLanguagePackButton.ClientID);
      controlDescriptor.AddElementProperty("cancel", this.cancel.ClientID);
      controlDescriptor.AddElementProperty("importWindowWrapper", this.importWindowWrapper.ClientID);
      controlDescriptor.AddProperty("_shouldClose", (object) (bool) (!this.Page.IsPostBack ? (false ? 1 : 0) : (this.LanguagePackUpload.UploadedFiles.Count > 0 ? 1 : 0)));
      return scriptDescriptors;
    }

    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      ScriptReference scriptReference1 = new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery.blockUI.js", "Telerik.Sitefinity.Resources");
      ScriptReference scriptReference2 = new ScriptReference("Telerik.Sitefinity.Localization.Web.UI.Scripts.ImportLanguagePackDialog.js", typeof (ImportLanguagePackDialog).Assembly.FullName);
      scriptReferences.Add(scriptReference1);
      scriptReferences.Add(scriptReference2);
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private void BindAvailableLanguages()
    {
      this.LanguagesDropDown.Items.Clear();
      if (AppSettings.CurrentSettings.AllLanguages.Count > 1)
      {
        ICollection<CultureInfo> values = AppSettings.CurrentSettings.AllLanguages.Values;
        this.LanguagesDropDown.Items.Add(new ListItem(CultureInfo.InvariantCulture.DisplayName, CultureInfo.InvariantCulture.Name));
        foreach (CultureInfo cultureInfo in (IEnumerable<CultureInfo>) values)
          this.LanguagesDropDown.Items.Add(new ListItem()
          {
            Text = cultureInfo.DisplayName,
            Value = cultureInfo.Name
          });
      }
      else
      {
        this.LanguagesDropDown.Attributes["style"] = "display: none;";
        this.LblLanguagesDropDown.Attributes["style"] = "display: none;";
      }
    }

    /// <summary>
    /// Excel preserved the valus either as a shared or as "inline". Shared are stored in a special table
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="sharedStringTablePart"></param>
    /// <returns></returns>
    private string GetCellValue(Cell cell, SharedStringTablePart sharedStringTablePart)
    {
      string innerText = cell.InnerText;
      if (cell.DataType != null)
      {
        switch (cell.DataType.Value)
        {
          case CellValues.SharedString:
            innerText = sharedStringTablePart.SharedStringTable.ElementAt<OpenXmlElement>(int.Parse(innerText)).InnerText;
            break;
        }
      }
      return innerText;
    }
  }
}
