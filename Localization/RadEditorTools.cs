// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadEditorTools
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadEditorToolsDescription", Name = "RadEditor.Tools", ResourceClassId = "RadEditor.Tools", Title = "RadEditorToolsTitle", TitlePlural = "RadEditorToolsTitlePlural")]
  public sealed class RadEditorTools : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadEditorTools" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadEditorTools()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadEditorTools" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadEditorTools(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadEditor Tools</summary>
    [ResourceEntry("RadEditorToolsTitle", Description = "The title of this class.", LastModified = "2009/09/16", Value = "RadEditor Tools")]
    public string RadEditorToolsTitle => this[nameof (RadEditorToolsTitle)];

    /// <summary>RadEditor Tools title plural.</summary>
    [ResourceEntry("RadEditorToolsTitlePlural", Description = "The title plural of this class.", LastModified = "2009/09/16", Value = "RadEditor Tools")]
    public string RadEditorToolsTitlePlural => this[nameof (RadEditorToolsTitlePlural)];

    /// <summary>Resource strings for RadEditor Tools</summary>
    [ResourceEntry("RadEditorToolsDescription", Description = "The description of this class.", LastModified = "2009/09/16", Value = "Resource strings for RadEditor Tools")]
    public string RadEditorToolsDescription => this[nameof (RadEditorToolsDescription)];

    [ResourceEntry("ReservedResource", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Please, do not delete this string. RadEditor needs it to determine if an external resource file is present in App_GlobalResources.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("AboutDialog", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "About RadEditor")]
    public string AboutDialog => this[nameof (AboutDialog)];

    [ResourceEntry("AbsolutePosition", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Set Absolute Position")]
    public string AbsolutePosition => this[nameof (AbsolutePosition)];

    [ResourceEntry("AjaxSpellCheck", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "AJAX Spellchecker")]
    public string AjaxSpellCheck => this[nameof (AjaxSpellCheck)];

    [ResourceEntry("ApplyClass", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Apply CSS Class")]
    public string ApplyClass => this[nameof (ApplyClass)];

    [ResourceEntry("BackColor", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Background Color")]
    public string BackColor => this[nameof (BackColor)];

    [ResourceEntry("Bold", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Bold")]
    public string Bold => this[nameof (Bold)];

    [ResourceEntry("ConvertToLower", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Convert to lower case")]
    public string ConvertToLower => this[nameof (ConvertToLower)];

    [ResourceEntry("ConvertToUpper", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Convert to upper case")]
    public string ConvertToUpper => this[nameof (ConvertToUpper)];

    [ResourceEntry("Copy", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Copy")]
    public string Copy => this[nameof (Copy)];

    [ResourceEntry("Cut", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Cut")]
    public string Cut => this[nameof (Cut)];

    [ResourceEntry("DecreaseSize", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Decrease Size")]
    public string DecreaseSize => this[nameof (DecreaseSize)];

    [ResourceEntry("DeleteCell", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Delete Cell")]
    public string DeleteCell => this[nameof (DeleteCell)];

    [ResourceEntry("DeleteColumn", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Delete Column")]
    public string DeleteColumn => this[nameof (DeleteColumn)];

    [ResourceEntry("DeleteRow", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Delete Row")]
    public string DeleteRow => this[nameof (DeleteRow)];

    [ResourceEntry("DeleteTable", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Delete Table")]
    public string DeleteTable => this[nameof (DeleteTable)];

    [ResourceEntry("DocumentManager", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Document Manager")]
    public string DocumentManager => this[nameof (DocumentManager)];

    [ResourceEntry("FindAndReplace", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Find And Replace")]
    public string FindAndReplace => this[nameof (FindAndReplace)];

    [ResourceEntry("FlashManager", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Flash Manager")]
    public string FlashManager => this[nameof (FlashManager)];

    [ResourceEntry("FontName", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Font Name")]
    public string FontName => this[nameof (FontName)];

    [ResourceEntry("FontSize", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Size")]
    public string FontSize => this[nameof (FontSize)];

    [ResourceEntry("ForeColor", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Foreground Color")]
    public string ForeColor => this[nameof (ForeColor)];

    [ResourceEntry("FormatBlock", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paragraph Style")]
    public string FormatBlock => this[nameof (FormatBlock)];

    [ResourceEntry("FormatCodeBlock", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Format Code Block")]
    public string FormatCodeBlock => this[nameof (FormatCodeBlock)];

    [ResourceEntry("FormatStripper", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Format Stripper")]
    public string FormatStripper => this[nameof (FormatStripper)];

    [ResourceEntry("Help", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Help")]
    public string Help => this[nameof (Help)];

    [ResourceEntry("ImageManager", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Image Manager")]
    public string ImageManager => this[nameof (ImageManager)];

    [ResourceEntry("ImageMapDialog", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Image Map Editor")]
    public string ImageMapDialog => this[nameof (ImageMapDialog)];

    [ResourceEntry("IncreaseSize", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Increase Size")]
    public string IncreaseSize => this[nameof (IncreaseSize)];

    [ResourceEntry("Indent", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Indent")]
    public string Indent => this[nameof (Indent)];

    [ResourceEntry("InsertAnchor", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Anchor")]
    public string InsertAnchor => this[nameof (InsertAnchor)];

    [ResourceEntry("InsertColumnLeft", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Column to the Left")]
    public string InsertColumnLeft => this[nameof (InsertColumnLeft)];

    [ResourceEntry("InsertColumnRight", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Column to the Right")]
    public string InsertColumnRight => this[nameof (InsertColumnRight)];

    [ResourceEntry("InsertCustomLink", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Custom Links")]
    public string InsertCustomLink => this[nameof (InsertCustomLink)];

    [ResourceEntry("InsertDate", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Date")]
    public string InsertDate => this[nameof (InsertDate)];

    [ResourceEntry("InsertEmailLink", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Email Link")]
    public string InsertEmailLink => this[nameof (InsertEmailLink)];

    [ResourceEntry("InsertFormButton", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Button")]
    public string InsertFormButton => this[nameof (InsertFormButton)];

    [ResourceEntry("InsertFormCheckbox", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Checkbox")]
    public string InsertFormCheckbox => this[nameof (InsertFormCheckbox)];

    [ResourceEntry("InsertFormElement", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Form Element")]
    public string InsertFormElement => this[nameof (InsertFormElement)];

    [ResourceEntry("InsertFormForm", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Form")]
    public string InsertFormForm => this[nameof (InsertFormForm)];

    [ResourceEntry("InsertFormHidden", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Hidden")]
    public string InsertFormHidden => this[nameof (InsertFormHidden)];

    [ResourceEntry("InsertFormPassword", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Password")]
    public string InsertFormPassword => this[nameof (InsertFormPassword)];

    [ResourceEntry("InsertFormRadio", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Radio")]
    public string InsertFormRadio => this[nameof (InsertFormRadio)];

    [ResourceEntry("InsertFormReset", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Reset")]
    public string InsertFormReset => this[nameof (InsertFormReset)];

    [ResourceEntry("InsertFormSelect", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Select")]
    public string InsertFormSelect => this[nameof (InsertFormSelect)];

    [ResourceEntry("InsertFormSubmit", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Submit")]
    public string InsertFormSubmit => this[nameof (InsertFormSubmit)];

    [ResourceEntry("InsertFormText", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Textbox")]
    public string InsertFormText => this[nameof (InsertFormText)];

    [ResourceEntry("InsertFormTextarea", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Textarea")]
    public string InsertFormTextarea => this[nameof (InsertFormTextarea)];

    [ResourceEntry("InsertGroupbox", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Groupbox")]
    public string InsertGroupbox => this[nameof (InsertGroupbox)];

    [ResourceEntry("InsertHorizontalRule", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Horizontal Rule")]
    public string InsertHorizontalRule => this[nameof (InsertHorizontalRule)];

    [ResourceEntry("InsertOrderedList", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Numbered List")]
    public string InsertOrderedList => this[nameof (InsertOrderedList)];

    [ResourceEntry("InsertParagraph", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "New Paragraph")]
    public string InsertParagraph => this[nameof (InsertParagraph)];

    [ResourceEntry("InsertRowAbove", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Row Above")]
    public string InsertRowAbove => this[nameof (InsertRowAbove)];

    [ResourceEntry("InsertRowBelow", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Row Below")]
    public string InsertRowBelow => this[nameof (InsertRowBelow)];

    [ResourceEntry("InsertSnippet", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Code Snippet")]
    public string InsertSnippet => this[nameof (InsertSnippet)];

    [ResourceEntry("InsertSymbol", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Symbol")]
    public string InsertSymbol => this[nameof (InsertSymbol)];

    [ResourceEntry("InsertTable", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Table")]
    public string InsertTable => this[nameof (InsertTable)];

    [ResourceEntry("InsertTime", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Time")]
    public string InsertTime => this[nameof (InsertTime)];

    [ResourceEntry("InsertUnorderedList", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Bullet List")]
    public string InsertUnorderedList => this[nameof (InsertUnorderedList)];

    [ResourceEntry("Italic", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Italic")]
    public string Italic => this[nameof (Italic)];

    [ResourceEntry("JustifyCenter", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Align Center")]
    public string JustifyCenter => this[nameof (JustifyCenter)];

    [ResourceEntry("JustifyFull", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Justify")]
    public string JustifyFull => this[nameof (JustifyFull)];

    [ResourceEntry("JustifyLeft", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Align Left")]
    public string JustifyLeft => this[nameof (JustifyLeft)];

    [ResourceEntry("JustifyNone", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Remove alignment")]
    public string JustifyNone => this[nameof (JustifyNone)];

    [ResourceEntry("JustifyRight", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Align Right")]
    public string JustifyRight => this[nameof (JustifyRight)];

    [ResourceEntry("LinkManager", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Hyperlink Manager")]
    public string LinkManager => this[nameof (LinkManager)];

    [ResourceEntry("MediaManager", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Media Manager")]
    public string MediaManager => this[nameof (MediaManager)];

    [ResourceEntry("MergeColumns", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Merge Cells Horizontally")]
    public string MergeColumns => this[nameof (MergeColumns)];

    [ResourceEntry("MergeRows", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Merge Cells Vertically")]
    public string MergeRows => this[nameof (MergeRows)];

    [ResourceEntry("ModuleManager", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Module Manager")]
    public string ModuleManager => this[nameof (ModuleManager)];

    [ResourceEntry("Outdent", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Outdent")]
    public string Outdent => this[nameof (Outdent)];

    [ResourceEntry("PageProperties", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Page Properties")]
    public string PageProperties => this[nameof (PageProperties)];

    [ResourceEntry("Paste", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paste")]
    public string Paste => this[nameof (Paste)];

    [ResourceEntry("PasteAsHtml", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paste As Html")]
    public string PasteAsHtml => this[nameof (PasteAsHtml)];

    [ResourceEntry("PasteFromWord", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paste from Word")]
    public string PasteFromWord => this[nameof (PasteFromWord)];

    [ResourceEntry("PasteFromWordNoFontsNoSizes", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paste from Word, strip font")]
    public string PasteFromWordNoFontsNoSizes => this[nameof (PasteFromWordNoFontsNoSizes)];

    [ResourceEntry("PastePlainText", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paste Plain Text")]
    public string PastePlainText => this[nameof (PastePlainText)];

    [ResourceEntry("PasteStrip", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paste Options")]
    public string PasteStrip => this[nameof (PasteStrip)];

    [ResourceEntry("Print", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Print")]
    public string Print => this[nameof (Print)];

    [ResourceEntry("RealFontSize", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Real font size")]
    public string RealFontSize => this[nameof (RealFontSize)];

    [ResourceEntry("Redo", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Redo")]
    public string Redo => this[nameof (Redo)];

    [ResourceEntry("RepeatLastCommand", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Repeat Last Command")]
    public string RepeatLastCommand => this[nameof (RepeatLastCommand)];

    [ResourceEntry("SelectAll", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Select All")]
    public string SelectAll => this[nameof (SelectAll)];

    [ResourceEntry("SetCellProperties", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Cell Properties")]
    public string SetCellProperties => this[nameof (SetCellProperties)];

    [ResourceEntry("SetImageProperties", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Properties...")]
    public string SetImageProperties => this[nameof (SetImageProperties)];

    [ResourceEntry("SetLinkProperties", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Properties...")]
    public string SetLinkProperties => this[nameof (SetLinkProperties)];

    [ResourceEntry("OpenLink", Description = "RadEditorTools resource strings.", LastModified = "2018/07/24", Value = "Open Link")]
    public string OpenLink => this[nameof (OpenLink)];

    [ResourceEntry("SetTableProperties", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Table Properties")]
    public string SetTableProperties => this[nameof (SetTableProperties)];

    [ResourceEntry("SpellCheck", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Spellchecker")]
    public string SpellCheck => this[nameof (SpellCheck)];

    [ResourceEntry("SplitCell", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Split Cell Vertically")]
    public string SplitCell => this[nameof (SplitCell)];

    [ResourceEntry("StrikeThrough", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Strikethrough")]
    public string StrikeThrough => this[nameof (StrikeThrough)];

    [ResourceEntry("StripAll", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Strip All Formatting")]
    public string StripAll => this[nameof (StripAll)];

    [ResourceEntry("StripCss", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Strip Css Formatting")]
    public string StripCss => this[nameof (StripCss)];

    [ResourceEntry("StripFont", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Strip Font Elements")]
    public string StripFont => this[nameof (StripFont)];

    [ResourceEntry("StripSpan", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Strip Span Elements")]
    public string StripSpan => this[nameof (StripSpan)];

    [ResourceEntry("StripWord", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Strip Word Formatting")]
    public string StripWord => this[nameof (StripWord)];

    [ResourceEntry("StyleBuilder", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Style Builder")]
    public string StyleBuilder => this[nameof (StyleBuilder)];

    [ResourceEntry("Subscript", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Subscript")]
    public string Subscript => this[nameof (Subscript)];

    [ResourceEntry("Superscript", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "SuperScript")]
    public string Superscript => this[nameof (Superscript)];

    [ResourceEntry("TableWizard", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Table Wizard")]
    public string TableWizard => this[nameof (TableWizard)];

    [ResourceEntry("TemplateManager", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Template Manager")]
    public string TemplateManager => this[nameof (TemplateManager)];

    [ResourceEntry("ToggleDocking", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Dock all Floating Toolbars/Modules")]
    public string ToggleDocking => this[nameof (ToggleDocking)];

    [ResourceEntry("ToggleScreenMode", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Toggle Full Screen Mode")]
    public string ToggleScreenMode => this[nameof (ToggleScreenMode)];

    [ResourceEntry("ToggleTableBorder", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Show/Hide Border")]
    public string ToggleTableBorder => this[nameof (ToggleTableBorder)];

    [ResourceEntry("Underline", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Underline")]
    public string Underline => this[nameof (Underline)];

    [ResourceEntry("Undo", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Undo")]
    public string Undo => this[nameof (Undo)];

    [ResourceEntry("Unlink", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Remove Link")]
    public string Unlink => this[nameof (Unlink)];

    [ResourceEntry("Zoom", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Zoom")]
    public string Zoom => this[nameof (Zoom)];

    [ResourceEntry("ImageEditor", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Image Editor")]
    public string ImageEditor => this[nameof (ImageEditor)];

    [ResourceEntry("SilverlightManager", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Silverlight Manager")]
    public string SilverlightManager => this[nameof (SilverlightManager)];

    [ResourceEntry("TrackChangesDialog", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "[Obsolete] Track Changes")]
    public string TrackChangesDialog => this[nameof (TrackChangesDialog)];

    [ResourceEntry("XhtmlValidator", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "XHTML Validator")]
    public string XhtmlValidator => this[nameof (XhtmlValidator)];

    [ResourceEntry("SplitCellHorizontal", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Split Cell Horizontally")]
    public string SplitCellHorizontal => this[nameof (SplitCellHorizontal)];

    [ResourceEntry("InsertImage", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Image")]
    public string InsertImage => this[nameof (InsertImage)];

    [ResourceEntry("InsertLink", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Link")]
    public string InsertLink => this[nameof (InsertLink)];

    [ResourceEntry("PasteHtml", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paste Html")]
    public string PasteHtml => this[nameof (PasteHtml)];

    [ResourceEntry("InsertTableLight", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert Table")]
    public string InsertTableLight => this[nameof (InsertTableLight)];

    [ResourceEntry("ToggleFloatingToolbar", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Toggle Floating Toolbar")]
    public string ToggleFloatingToolbar => this[nameof (ToggleFloatingToolbar)];

    [ResourceEntry("CSDialog", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Compliance Check")]
    public string CSDialog => this[nameof (CSDialog)];

    [ResourceEntry("Enter", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Enter Pressed")]
    public string Enter => this[nameof (Enter)];

    [ResourceEntry("Clipboard", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Clipboard")]
    public string Clipboard => this[nameof (Clipboard)];

    [ResourceEntry("Home", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Home")]
    public string Home => this[nameof (Home)];

    [ResourceEntry("Content", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Content")]
    public string Content => this[nameof (Content)];

    [ResourceEntry("Editing", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Editing")]
    public string Editing => this[nameof (Editing)];

    [ResourceEntry("Font", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Font")]
    public string Font => this[nameof (Font)];

    [ResourceEntry("Insert", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert")]
    public string Insert => this[nameof (Insert)];

    [ResourceEntry("Links", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Links")]
    public string Links => this[nameof (Links)];

    [ResourceEntry("Media", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Media")]
    public string Media => this[nameof (Media)];

    [ResourceEntry("Paragraph", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paragraph")]
    public string Paragraph => this[nameof (Paragraph)];

    [ResourceEntry("Preferences", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Preferences")]
    public string Preferences => this[nameof (Preferences)];

    [ResourceEntry("Proofing", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Proofing")]
    public string Proofing => this[nameof (Proofing)];

    [ResourceEntry("Review", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Review")]
    public string Review => this[nameof (Review)];

    [ResourceEntry("Styles", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Styles")]
    public string Styles => this[nameof (Styles)];

    [ResourceEntry("Tables", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Tables")]
    public string Tables => this[nameof (Tables)];

    [ResourceEntry("View", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "View")]
    public string View => this[nameof (View)];

    [ResourceEntry("InsertExternalVideo", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Insert External Video")]
    public string InsertExternalVideo => this[nameof (InsertExternalVideo)];

    [ResourceEntry("ColorPicker", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "ColorPicker")]
    public string ColorPicker => this[nameof (ColorPicker)];

    [ResourceEntry("AlignmentSelector", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Alignment selector")]
    public string AlignmentSelector => this[nameof (AlignmentSelector)];

    [ResourceEntry("FormatSets", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Format Sets")]
    public string FormatSets => this[nameof (FormatSets)];

    [ResourceEntry("ToggleAdvancedToolbars", Description = "RadEditor resource strings.", LastModified = "2010/12/07", Value = "Toggle advanced toolbars")]
    public string ToggleAdvancedToolbars => this[nameof (ToggleAdvancedToolbars)];

    [ResourceEntry("PasteMarkdown", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Paste Markdown")]
    public string PasteMarkdown => this[nameof (PasteMarkdown)];

    [ResourceEntry("FormatPainter", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Format Painter")]
    public string FormatPainter => this[nameof (FormatPainter)];

    [ResourceEntry("FormatPainterApply", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Apply Format")]
    public string FormatPainterApply => this[nameof (FormatPainterApply)];

    [ResourceEntry("FormatPainterClear", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Clear Format")]
    public string FormatPainterClear => this[nameof (FormatPainterClear)];

    [ResourceEntry("FormatPainterCopy", Description = "RadEditorTools resource strings.", LastModified = "2014/06/19", Value = "Copy Format")]
    public string FormatPainterCopy => this[nameof (FormatPainterCopy)];

    [ResourceEntry("DeleteTableItems", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Delete")]
    public string DeleteTableItems => this[nameof (DeleteTableItems)];

    [ResourceEntry("InsertTableItems", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Insert")]
    public string InsertTableItems => this[nameof (InsertTableItems)];

    [ResourceEntry("MergeSplitCells", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Merge and Split Cells")]
    public string MergeSplitCells => this[nameof (MergeSplitCells)];

    [ResourceEntry("MobileEdit", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Edit")]
    public string MobileEdit => this[nameof (MobileEdit)];

    [ResourceEntry("ToggleEditMode", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Toggle Edit Mode")]
    public string ToggleEditMode => this[nameof (ToggleEditMode)];

    [ResourceEntry("ImageSizeMargins", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Size and Margins")]
    public string ImageSizeMargins => this[nameof (ImageSizeMargins)];

    [ResourceEntry("SizeMargins", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Size and Margins")]
    public string SizeMargins => this[nameof (SizeMargins)];

    [ResourceEntry("CellBorder", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Cell Border")]
    public string CellBorder => this[nameof (CellBorder)];

    [ResourceEntry("ImageBorder", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Image Border")]
    public string ImageBorder => this[nameof (ImageBorder)];

    [ResourceEntry("TableBorder", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Table Border")]
    public string TableBorder => this[nameof (TableBorder)];

    [ResourceEntry("EditContent", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Edit Content")]
    public string EditContent => this[nameof (EditContent)];

    [ResourceEntry("InsertTableDialog", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Insert Table")]
    public string InsertTableDialog => this[nameof (InsertTableDialog)];

    [ResourceEntry("MobileTableProperties", Description = "RadEditorTools resource strings.", LastModified = "2015-06-25", Value = "Table Properties")]
    public string MobileTableProperties => this[nameof (MobileTableProperties)];
  }
}
