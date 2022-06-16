// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadGridMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadGridMainDescription", Name = "RadGrid.Main", ResourceClassId = "RadGrid.Main", Title = "RadGridMainTitle", TitlePlural = "RadGridMainTitlePlural")]
  public sealed class RadGridMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadGridMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadGridMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadGridMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadGridMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadGrid Main</summary>
    [ResourceEntry("RadGridMainTitle", Description = "The title of this class.", LastModified = "2010/09/28", Value = "RadGrid Main")]
    public string RadGridMainTitle => this[nameof (RadGridMainTitle)];

    /// <summary>RadGrid Main</summary>
    [ResourceEntry("RadGridMainTitlePlural", Description = "The title plural of this class.", LastModified = "2010/09/28", Value = "RadGrid Main")]
    public string RadGridMainTitlePlural => this[nameof (RadGridMainTitlePlural)];

    /// <summary>Resource strings for RadGrid.</summary>
    [ResourceEntry("RadGridMainDescription", Description = "The description of this class.", LastModified = "2010/09/28", Value = "Resource strings for RadGrid.")]
    public string RadGridMainDescription => this[nameof (RadGridMainDescription)];

    [ResourceEntry("AddNewRecordImageUrl", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string AddNewRecordImageUrl => this[nameof (AddNewRecordImageUrl)];

    [ResourceEntry("AddNewRecordText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Add new record")]
    public string AddNewRecordText => this[nameof (AddNewRecordText)];

    [ResourceEntry("Caption", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string Caption => this[nameof (Caption)];

    [ResourceEntry("CollapseTooltip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Collapse group")]
    public string CollapseTooltip => this[nameof (CollapseTooltip)];

    [ResourceEntry("ColumnResizeTooltipFormatString", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Width: <strong>{0}</strong> <em>pixels</em>")]
    public string ColumnResizeTooltipFormatString => this[nameof (ColumnResizeTooltipFormatString)];

    [ResourceEntry("DragToGroupOrReorder", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Drag to group or reorder")]
    public string DragToGroupOrReorder => this[nameof (DragToGroupOrReorder)];

    [ResourceEntry("DragToResize", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Drag to resize")]
    public string DragToResize => this[nameof (DragToResize)];

    [ResourceEntry("DropHereToReorder", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Drop here to reorder")]
    public string DropHereToReorder => this[nameof (DropHereToReorder)];

    [ResourceEntry("ExpandTooltip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Expand group")]
    public string ExpandTooltip => this[nameof (ExpandTooltip)];

    [ResourceEntry("ExportToCsvImageUrl", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string ExportToCsvImageUrl => this[nameof (ExportToCsvImageUrl)];

    [ResourceEntry("ExportToCsvText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Export to CSV")]
    public string ExportToCsvText => this[nameof (ExportToCsvText)];

    [ResourceEntry("ExportToExcelImageUrl", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string ExportToExcelImageUrl => this[nameof (ExportToExcelImageUrl)];

    [ResourceEntry("ExportToExcelText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Export to Excel")]
    public string ExportToExcelText => this[nameof (ExportToExcelText)];

    [ResourceEntry("ExportToPdfText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Export to PDF")]
    public string ExportToPdfText => this[nameof (ExportToPdfText)];

    [ResourceEntry("ExportToPdfImageUrl", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string ExportToPdfImageUrl => this[nameof (ExportToPdfImageUrl)];

    [ResourceEntry("ExportToWordImageUrl", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string ExportToWordImageUrl => this[nameof (ExportToWordImageUrl)];

    [ResourceEntry("ExportToWordText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Export to Word")]
    public string ExportToWordText => this[nameof (ExportToWordText)];

    [ResourceEntry("FilterExpression", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string FilterExpression => this[nameof (FilterExpression)];

    [ResourceEntry("FirstPageText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string FirstPageText => this[nameof (FirstPageText)];

    [ResourceEntry("FirstPageToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "First Page")]
    public string FirstPageToolTip => this[nameof (FirstPageToolTip)];

    [ResourceEntry("GroupByFieldsSeparator", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "; ")]
    public string GroupByFieldsSeparator => this[nameof (GroupByFieldsSeparator)];

    [ResourceEntry("GroupContinuedFormatString", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "... group continued from the previous page. ")]
    public string GroupContinuedFormatString => this[nameof (GroupContinuedFormatString)];

    [ResourceEntry("GroupContinuesFormatString", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = " Group continues on the next page.")]
    public string GroupContinuesFormatString => this[nameof (GroupContinuesFormatString)];

    [ResourceEntry("GroupPanelText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Drag a column header and drop it here to group by that column")]
    public string GroupPanelText => this[nameof (GroupPanelText)];

    [ResourceEntry("GroupSplitDisplayFormat", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Showing {0} of {1} items.")]
    public string GroupSplitDisplayFormat => this[nameof (GroupSplitDisplayFormat)];

    [ResourceEntry("GroupSplitFormat", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = " ({0})")]
    public string GroupSplitFormat => this[nameof (GroupSplitFormat)];

    [ResourceEntry("HierarchyCollapseTooltip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Collapse")]
    public string HierarchyCollapseTooltip => this[nameof (HierarchyCollapseTooltip)];

    [ResourceEntry("HierarchyExpandTooltip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Expand")]
    public string HierarchyExpandTooltip => this[nameof (HierarchyExpandTooltip)];

    [ResourceEntry("HierarchySelfCollapseTooltip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Self reference collapse")]
    public string HierarchySelfCollapseTooltip => this[nameof (HierarchySelfCollapseTooltip)];

    [ResourceEntry("HierarchySelfExpandTooltip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Self reference expand")]
    public string HierarchySelfExpandTooltip => this[nameof (HierarchySelfExpandTooltip)];

    [ResourceEntry("LastPageText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string LastPageText => this[nameof (LastPageText)];

    [ResourceEntry("LastPageToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Last Page")]
    public string LastPageToolTip => this[nameof (LastPageToolTip)];

    [ResourceEntry("LoadingText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Loading...")]
    public string LoadingText => this[nameof (LoadingText)];

    [ResourceEntry("NextPagesToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Next Pages")]
    public string NextPagesToolTip => this[nameof (NextPagesToolTip)];

    [ResourceEntry("NextPageText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string NextPageText => this[nameof (NextPageText)];

    [ResourceEntry("NextPageToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Next Page")]
    public string NextPageToolTip => this[nameof (NextPageToolTip)];

    [ResourceEntry("NoDetailRecordsText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "No child records to display.")]
    public string NoDetailRecordsText => this[nameof (NoDetailRecordsText)];

    [ResourceEntry("NoMasterRecordsText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "No records to display.")]
    public string NoMasterRecordsText => this[nameof (NoMasterRecordsText)];

    [ResourceEntry("PagerTextFormat", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Change page: {4} &nbsp;Page <strong>{0}</strong> of <strong>{1}</strong>, items <strong>{2}</strong> to <strong>{3}</strong> of <strong>{5}</strong>.")]
    public string PagerTextFormat => this[nameof (PagerTextFormat)];

    [ResourceEntry("PagerTooltipFormatString", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Page <strong>{0}</strong> of <strong>{1}</strong>")]
    public string PagerTooltipFormatString => this[nameof (PagerTooltipFormatString)];

    [ResourceEntry("PageSizeLabelText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Page size:")]
    public string PageSizeLabelText => this[nameof (PageSizeLabelText)];

    [ResourceEntry("PrevPagesToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Previous Pages")]
    public string PrevPagesToolTip => this[nameof (PrevPagesToolTip)];

    [ResourceEntry("PrevPageText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string PrevPageText => this[nameof (PrevPageText)];

    [ResourceEntry("PrevPageToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Previous Page")]
    public string PrevPageToolTip => this[nameof (PrevPageToolTip)];

    [ResourceEntry("Refresh", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Refresh")]
    public string Refresh => this[nameof (Refresh)];

    [ResourceEntry("RefreshImageUrl", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string RefreshImageUrl => this[nameof (RefreshImageUrl)];

    [ResourceEntry("ReservedResource", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("SortedAscToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Sorted asc")]
    public string SortedAscToolTip => this[nameof (SortedAscToolTip)];

    [ResourceEntry("SortedDescToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Sorted desc")]
    public string SortedDescToolTip => this[nameof (SortedDescToolTip)];

    [ResourceEntry("SortToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Click here to sort")]
    public string SortToolTip => this[nameof (SortToolTip)];

    [ResourceEntry("StatusReadyText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Ready")]
    public string StatusReadyText => this[nameof (StatusReadyText)];

    [ResourceEntry("Summary", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string Summary => this[nameof (Summary)];

    [ResourceEntry("UnGroupButtonTooltip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Click here to ungroup")]
    public string UnGroupButtonTooltip => this[nameof (UnGroupButtonTooltip)];

    [ResourceEntry("UnGroupTooltip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Drag out of the bar to ungroup")]
    public string UnGroupTooltip => this[nameof (UnGroupTooltip)];

    [ResourceEntry("BetweenText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Between")]
    public string BetweenText => this[nameof (BetweenText)];

    [ResourceEntry("ContainsText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Contains")]
    public string ContainsText => this[nameof (ContainsText)];

    [ResourceEntry("CustomText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Custom")]
    public string CustomText => this[nameof (CustomText)];

    [ResourceEntry("DoesNotContainText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "DoesNotContain")]
    public string DoesNotContainText => this[nameof (DoesNotContainText)];

    [ResourceEntry("EndsWithText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "EndsWith")]
    public string EndsWithText => this[nameof (EndsWithText)];

    [ResourceEntry("EqualToText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "EqualTo")]
    public string EqualToText => this[nameof (EqualToText)];

    [ResourceEntry("GreaterThanOrEqualToText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "GreaterThanOrEqualTo")]
    public string GreaterThanOrEqualToText => this[nameof (GreaterThanOrEqualToText)];

    [ResourceEntry("GreaterThanText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "GreaterThan")]
    public string GreaterThanText => this[nameof (GreaterThanText)];

    [ResourceEntry("IsEmptyText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "IsEmpty")]
    public string IsEmptyText => this[nameof (IsEmptyText)];

    [ResourceEntry("IsNullText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "IsNull")]
    public string IsNullText => this[nameof (IsNullText)];

    [ResourceEntry("LessThanOrEqualToText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "LessThanOrEqualTo")]
    public string LessThanOrEqualToText => this[nameof (LessThanOrEqualToText)];

    [ResourceEntry("LessThanText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "LessThan")]
    public string LessThanText => this[nameof (LessThanText)];

    [ResourceEntry("NoFilterText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "NoFilter")]
    public string NoFilterText => this[nameof (NoFilterText)];

    [ResourceEntry("NotBetweenText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "NotBetween")]
    public string NotBetweenText => this[nameof (NotBetweenText)];

    [ResourceEntry("NotEqualToText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "NotEqualTo")]
    public string NotEqualToText => this[nameof (NotEqualToText)];

    [ResourceEntry("NotIsEmptyText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "NotIsEmpty")]
    public string NotIsEmptyText => this[nameof (NotIsEmptyText)];

    [ResourceEntry("NotIsNullText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "NotIsNull")]
    public string NotIsNullText => this[nameof (NotIsNullText)];

    [ResourceEntry("StartsWithText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "StartsWith")]
    public string StartsWithText => this[nameof (StartsWithText)];

    [ResourceEntry("ChangePageSizeLabelText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Page size:")]
    public string ChangePageSizeLabelText => this[nameof (ChangePageSizeLabelText)];

    [ResourceEntry("ChangePageSizeLinkButtonText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Change")]
    public string ChangePageSizeLinkButtonText => this[nameof (ChangePageSizeLinkButtonText)];

    [ResourceEntry("GoToPageLabelText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Page:")]
    public string GoToPageLabelText => this[nameof (GoToPageLabelText)];

    [ResourceEntry("GoToPageLinkButtonText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Go")]
    public string GoToPageLinkButtonText => this[nameof (GoToPageLinkButtonText)];

    [ResourceEntry("HeaderContextMenuAndLabel", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "And")]
    public string HeaderContextMenuAndLabel => this[nameof (HeaderContextMenuAndLabel)];

    [ResourceEntry("HeaderContextMenuClearButton", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Clear Filter")]
    public string HeaderContextMenuClearButton => this[nameof (HeaderContextMenuClearButton)];

    [ResourceEntry("HeaderContextMenuColumns", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Columns")]
    public string HeaderContextMenuColumns => this[nameof (HeaderContextMenuColumns)];

    [ResourceEntry("HeaderContextMenuFilterButton", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Filter")]
    public string HeaderContextMenuFilterButton => this[nameof (HeaderContextMenuFilterButton)];

    [ResourceEntry("HeaderContextMenuFilterItemText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Filter")]
    public string HeaderContextMenuFilterItemText => this[nameof (HeaderContextMenuFilterItemText)];

    [ResourceEntry("HeaderContextMenuGroupBy", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Group By")]
    public string HeaderContextMenuGroupBy => this[nameof (HeaderContextMenuGroupBy)];

    [ResourceEntry("HeaderContextMenuRowsLabel", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Show rows with value that")]
    public string HeaderContextMenuRowsLabel => this[nameof (HeaderContextMenuRowsLabel)];

    [ResourceEntry("HeaderContextMenuSortAsc", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Sort Ascending")]
    public string HeaderContextMenuSortAsc => this[nameof (HeaderContextMenuSortAsc)];

    [ResourceEntry("HeaderContextMenuSortClear", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Clear Sorting")]
    public string HeaderContextMenuSortClear => this[nameof (HeaderContextMenuSortClear)];

    [ResourceEntry("HeaderContextMenuSortDesc", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Sort Descending")]
    public string HeaderContextMenuSortDesc => this[nameof (HeaderContextMenuSortDesc)];

    [ResourceEntry("HeaderContextMenuUnGroupBy", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Ungroup")]
    public string HeaderContextMenuUnGroupBy => this[nameof (HeaderContextMenuUnGroupBy)];

    [ResourceEntry("PageOfLabelText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "of {0}")]
    public string PageOfLabelText => this[nameof (PageOfLabelText)];

    [ResourceEntry("FilterImageToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Filter")]
    public string FilterImageToolTip => this[nameof (FilterImageToolTip)];

    [ResourceEntry("CloseText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Close")]
    public string CloseText => this[nameof (CloseText)];

    [ResourceEntry("HeaderContextMenuBestFitText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Best Fit")]
    public string HeaderContextMenuBestFitText => this[nameof (HeaderContextMenuBestFitText)];

    [ResourceEntry("HeaderContextMenuAggregates", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Aggregates")]
    public string HeaderContextMenuAggregates => this[nameof (HeaderContextMenuAggregates)];

    [ResourceEntry("HeaderContextMenuAvgAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Avg")]
    public string HeaderContextMenuAvgAggregateText => this[nameof (HeaderContextMenuAvgAggregateText)];

    [ResourceEntry("HeaderContextMenuCountAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Count")]
    public string HeaderContextMenuCountAggregateText => this[nameof (HeaderContextMenuCountAggregateText)];

    [ResourceEntry("HeaderContextMenuCountDistinctAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "CountDistinct")]
    public string HeaderContextMenuCountDistinctAggregateText => this[nameof (HeaderContextMenuCountDistinctAggregateText)];

    [ResourceEntry("HeaderContextMenuCustomAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Custom")]
    public string HeaderContextMenuCustomAggregateText => this[nameof (HeaderContextMenuCustomAggregateText)];

    [ResourceEntry("HeaderContextMenuFirstAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "First")]
    public string HeaderContextMenuFirstAggregateText => this[nameof (HeaderContextMenuFirstAggregateText)];

    [ResourceEntry("HeaderContextMenuLastAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Last")]
    public string HeaderContextMenuLastAggregateText => this[nameof (HeaderContextMenuLastAggregateText)];

    [ResourceEntry("HeaderContextMenuMaxAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Max")]
    public string HeaderContextMenuMaxAggregateText => this[nameof (HeaderContextMenuMaxAggregateText)];

    [ResourceEntry("HeaderContextMenuMinAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Min")]
    public string HeaderContextMenuMinAggregateText => this[nameof (HeaderContextMenuMinAggregateText)];

    [ResourceEntry("HeaderContextMenuNoneAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "None")]
    public string HeaderContextMenuNoneAggregateText => this[nameof (HeaderContextMenuNoneAggregateText)];

    [ResourceEntry("HeaderContextMenuSumAggregateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Sum")]
    public string HeaderContextMenuSumAggregateText => this[nameof (HeaderContextMenuSumAggregateText)];

    [ResourceEntry("CancelText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Cancel")]
    public string CancelText => this[nameof (CancelText)];

    [ResourceEntry("EditText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Edit")]
    public string EditText => this[nameof (EditText)];

    [ResourceEntry("InsertText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Insert")]
    public string InsertText => this[nameof (InsertText)];

    [ResourceEntry("UpdateText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Update")]
    public string UpdateText => this[nameof (UpdateText)];

    [ResourceEntry("DeleteText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Delete")]
    public string DeleteText => this[nameof (DeleteText)];

    [ResourceEntry("AggregateFunctionAvg", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Avg")]
    public string AggregateFunctionAvg => this[nameof (AggregateFunctionAvg)];

    [ResourceEntry("AggregateFunctionCount", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Count")]
    public string AggregateFunctionCount => this[nameof (AggregateFunctionCount)];

    [ResourceEntry("AggregateFunctionCountDistinct", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "CountDistinct")]
    public string AggregateFunctionCountDistinct => this[nameof (AggregateFunctionCountDistinct)];

    [ResourceEntry("AggregateFunctionCustom", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Custom")]
    public string AggregateFunctionCustom => this[nameof (AggregateFunctionCustom)];

    [ResourceEntry("AggregateFunctionFirst", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "First")]
    public string AggregateFunctionFirst => this[nameof (AggregateFunctionFirst)];

    [ResourceEntry("AggregateFunctionLast", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Last")]
    public string AggregateFunctionLast => this[nameof (AggregateFunctionLast)];

    [ResourceEntry("AggregateFunctionMax", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Max")]
    public string AggregateFunctionMax => this[nameof (AggregateFunctionMax)];

    [ResourceEntry("AggregateFunctionMin", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Min")]
    public string AggregateFunctionMin => this[nameof (AggregateFunctionMin)];

    [ResourceEntry("AggregateFunctionSum", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Sum")]
    public string AggregateFunctionSum => this[nameof (AggregateFunctionSum)];

    [ResourceEntry("GoToPageTextBoxToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string GoToPageTextBoxToolTip => this[nameof (GoToPageTextBoxToolTip)];

    [ResourceEntry("ChangePageSizeButtonToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Change Page Size")]
    public string ChangePageSizeButtonToolTip => this[nameof (ChangePageSizeButtonToolTip)];

    [ResourceEntry("ChangePageSizeTextBoxToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string ChangePageSizeTextBoxToolTip => this[nameof (ChangePageSizeTextBoxToolTip)];

    [ResourceEntry("ChangePageSizeComboBoxTableSummary", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string ChangePageSizeComboBoxTableSummary => this[nameof (ChangePageSizeComboBoxTableSummary)];

    [ResourceEntry("ChangePageSizeComboBoxToolTip", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "")]
    public string ChangePageSizeComboBoxToolTip => this[nameof (ChangePageSizeComboBoxToolTip)];

    [ResourceEntry("CancelChangesText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Cancel changes")]
    public string CancelChangesText => this[nameof (CancelChangesText)];

    [ResourceEntry("SaveChangesText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Save changes")]
    public string SaveChangesText => this[nameof (SaveChangesText)];

    [ResourceEntry("SliderDecreaseText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Decrease")]
    public string SliderDecreaseText => this[nameof (SliderDecreaseText)];

    [ResourceEntry("SliderDragText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Drag")]
    public string SliderDragText => this[nameof (SliderDragText)];

    [ResourceEntry("SliderIncreaseText", Description = "RadGridMain resource strings.", LastModified = "2014/06/19", Value = "Increase")]
    public string SliderIncreaseText => this[nameof (SliderIncreaseText)];

    [ResourceEntry("MobileColumnsViewDescription", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Show/Hide Columns and Drag the Icon to Reorder")]
    public string MobileColumnsViewDescription => this[nameof (MobileColumnsViewDescription)];

    [ResourceEntry("MobileColumnsViewTitle", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Columns Display")]
    public string MobileColumnsViewTitle => this[nameof (MobileColumnsViewTitle)];

    [ResourceEntry("MobileEditViewTitle", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Edit")]
    public string MobileEditViewTitle => this[nameof (MobileEditViewTitle)];

    [ResourceEntry("MobileFilterViewOptionsText", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Options")]
    public string MobileFilterViewOptionsText => this[nameof (MobileFilterViewOptionsText)];

    [ResourceEntry("MobileFilterViewTitleFormat", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Filter by {0}")]
    public string MobileFilterViewTitleFormat => this[nameof (MobileFilterViewTitleFormat)];

    [ResourceEntry("MobileFilterViewValueText", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Value")]
    public string MobileFilterViewValueText => this[nameof (MobileFilterViewValueText)];

    [ResourceEntry("MobileInsertViewTitle", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Insert")]
    public string MobileInsertViewTitle => this[nameof (MobileInsertViewTitle)];

    [ResourceEntry("MobileViewBackButtonText", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Back")]
    public string MobileViewBackButtonText => this[nameof (MobileViewBackButtonText)];

    [ResourceEntry("MobileViewCancelButtonText", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Cancel")]
    public string MobileViewCancelButtonText => this[nameof (MobileViewCancelButtonText)];

    [ResourceEntry("MobileViewDoneButtonText", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "Done")]
    public string MobileViewDoneButtonText => this[nameof (MobileViewDoneButtonText)];

    [ResourceEntry("MobileViewGroupsText", Description = "RadGridMain resource strings.", LastModified = "2014/12/15", Value = "View Groups")]
    public string MobileViewGroupsText => this[nameof (MobileViewGroupsText)];

    [ResourceEntry("NextFrozenColumnText", Description = "RadGridMain resource strings.", LastModified = "2015-03-02", Value = "Next")]
    public string NextFrozenColumnText => this[nameof (NextFrozenColumnText)];

    [ResourceEntry("PrevFrozenColumnText", Description = "RadGridMain resource strings.", LastModified = "2015-03-02", Value = "Prev")]
    public string PrevFrozenColumnText => this[nameof (PrevFrozenColumnText)];

    [ResourceEntry("HeaderContextMenuFreeze", Description = "RadGridMain resource strings.", LastModified = "2015-03-02", Value = "Freeze")]
    public string HeaderContextMenuFreeze => this[nameof (HeaderContextMenuFreeze)];

    [ResourceEntry("HeaderContextMenuUnfreeze", Description = "RadGridMain resource strings.", LastModified = "2015-03-02", Value = "Unfreeze")]
    public string HeaderContextMenuUnfreeze => this[nameof (HeaderContextMenuUnfreeze)];

    [ResourceEntry("RangeFilteringFromText", Description = "RadGridMain resource strings.", LastModified = "2015-06-25", Value = "From: ")]
    public string RangeFilteringFromText => this[nameof (RangeFilteringFromText)];

    [ResourceEntry("RangeFilteringToText", Description = "RadGridMain resource strings.", LastModified = "2015-06-25", Value = "To: ")]
    public string RangeFilteringToText => this[nameof (RangeFilteringToText)];

    [ResourceEntry("PrintGridText", Description = "RadGridMain resource strings.", LastModified = "2016/06/28", Value = "Print RadGrid")]
    public string PrintGridText => this[nameof (PrintGridText)];

    [ResourceEntry("ErrorValueText", Description = "RadPivotGridMain resource strings.", LastModified = "2016/06/28", Value = "Error")]
    public string ErrorValueText => this[nameof (ErrorValueText)];

    [ResourceEntry("GrandTotalText", Description = "RadPivotGridMain resource strings.", LastModified = "2016/06/28", Value = "Grand Total")]
    public string GrandTotalText => this[nameof (GrandTotalText)];

    [ResourceEntry("TotalValueFormat", Description = "RadPivotGridMain resource strings.", LastModified = "2016/06/28", Value = "Total {0}")]
    public string TotalValueFormat => this[nameof (TotalValueFormat)];

    [ResourceEntry("ValueTotalFormat", Description = "RadPivotGridMain resource strings.", LastModified = "2016/06/28", Value = "{0} Total")]
    public string ValueTotalFormat => this[nameof (ValueTotalFormat)];
  }
}
