// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ControlResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents string resources for UI labels.</summary>
  [ObjectInfo("ControlResources", ResourceClassId = "ControlResources")]
  public sealed class ControlResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Labels" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ControlResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.Labels" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public ControlResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Configuration element singular name</summary>
    [ResourceEntry("ControlResourcesTitle", Description = "Configuration element singular name.", LastModified = "2010/10/18", Value = "Control")]
    public string ControlResourcesTitle => this[nameof (ControlResourcesTitle)];

    /// <summary>Configuration element plural name</summary>
    [ResourceEntry("ControlResourcesTitlePlural", Description = "Configuration element plural name.", LastModified = "2010/10/18", Value = "Controls")]
    public string ControlResourcesTitlePlural => this[nameof (ControlResourcesTitlePlural)];

    /// <summary>Configuration element description</summary>
    [ResourceEntry("ControlResourcesDescription", Description = "The description of this class.", LastModified = "2010/10/18", Value = "Configuration element description.")]
    public string ControlResourcesDescription => this[nameof (ControlResourcesDescription)];

    /// <summary>phrase: Property must be set to a value</summary>
    [ResourceEntry("PropertyNotSet", Description = "Property must be set to a value.", LastModified = "2009/11/19", Value = "{0} property must be set to a value.")]
    public string PropertyNotSet => this[nameof (PropertyNotSet)];

    /// <summary>
    /// phrase: When searching functionality is enabled, at least one data members must be marked as IsSearchField = true
    /// </summary>
    [ResourceEntry("SearchingWithNoSearchFields", Description = "phrase: When searching functionality is enabled, at least one data members must be marked as IsSearchField = true", LastModified = "2009/11/20", Value = "When searching functionality is enabled, at least one data members must be marked as IsSearchField = true")]
    public string SearchingWithNoSearchFields => this[nameof (SearchingWithNoSearchFields)];

    /// <summary>
    /// phrase: Selector must have at least one data member defined as a key.
    /// </summary>
    [ResourceEntry("SelectorHasNoKeysDefined", Description = "phrase: Selector must have at least one data member defined as a key.", LastModified = "2009/11/20", Value = "Selector must have at least one data member defined as a key.")]
    public string SelectorHasNoKeysDefined => this[nameof (SelectorHasNoKeysDefined)];

    /// <summary>word: Sizes</summary>
    [ResourceEntry("Sizes", Description = "word: sizes", LastModified = "2009/11/20", Value = "Sizes")]
    public string Sizes => this[nameof (Sizes)];

    /// <summary>word: Spaces</summary>
    [ResourceEntry("Spaces", Description = "word: Spaces", LastModified = "2009/11/20", Value = "Spaces")]
    public string Spaces => this[nameof (Spaces)];

    [ResourceEntry("ClassesAndLabels", Description = "word: Classes & Labels", LastModified = "2014/02/26", Value = "Classes & Labels")]
    public string ClassesAndLabels => this[nameof (ClassesAndLabels)];

    /// <summary>phrase: Show sizes in</summary>
    [ResourceEntry("ShowSizesIn", Description = "phrase: Show sizes in", LastModified = "2009/11/20", Value = "Show sizes in")]
    public string ShowSizesIn => this[nameof (ShowSizesIn)];

    /// <summary>words: Widths</summary>
    [ResourceEntry("Widths", Description = "word: Widths", LastModified = "2009/11/20", Value = "Widths")]
    public string Widths => this[nameof (Widths)];

    /// <summary>word: Column</summary>
    [ResourceEntry("Column", Description = "word: Column", LastModified = "2009/11/20", Value = "Column")]
    public string Column => this[nameof (Column)];

    /// <summary>word: auto-sized</summary>
    [ResourceEntry("AutoSized", Description = "word: auto-sized", LastModified = "2009/11/20", Value = "auto-sized")]
    public string AutoSized => this[nameof (AutoSized)];

    /// <summary>phrase: Change auto-sized column</summary>
    [ResourceEntry("ChangeAutoSizedColumn", Description = "phrase: Change auto-sized column", LastModified = "2009/11/21", Value = "Change auto-sized column")]
    public string ChangeAutoSizedColumn => this[nameof (ChangeAutoSizedColumn)];

    /// <summary>phrase: Make this auto-sized</summary>
    [ResourceEntry("MakeThisAutoSized", Description = "phrase: Make this auto-sized", LastModified = "2009/11/21", Value = "Make this auto-sized")]
    public string MakeThisAutoSized => this[nameof (MakeThisAutoSized)];

    /// <summary>phrase: Cancel changing auto-sized column</summary>
    [ResourceEntry("CancelChangeAutoSizedColumnLabel", Description = "phrase: Cancel changing auto-sized column", LastModified = "2009/11/21", Value = "Cancel changing auto-sized column")]
    public string CancelChangeAutoSizedColumnLabel => this[nameof (CancelChangeAutoSizedColumnLabel)];

    /// <summary>phrase: Look left</summary>
    [ResourceEntry("LookLeft", Description = "phrase: Look left", LastModified = "2009/11/21", Value = "Look left")]
    public string LookLeft => this[nameof (LookLeft)];

    /// <summary>
    /// phrase: You can resize widths by dragging columns, too
    /// </summary>
    [ResourceEntry("ResizingInstruction", Description = "phrase: You can resize widths by dragging columns, too", LastModified = "2009/11/21", Value = "You can resize widths by dragging columns, too.")]
    public string ResizingInstruction => this[nameof (ResizingInstruction)];

    /// <summary>phrase: Show spaces in</summary>
    [ResourceEntry("ShowSpacesIn", Description = "phrase: Show spaces in", LastModified = "2009/11/21", Value = "Show spaces in")]
    public string ShowSpacesIn => this[nameof (ShowSpacesIn)];

    /// <summary>phrase: Space between columns</summary>
    [ResourceEntry("HorizontalSpaceBetweenColumns", Description = "phrase: Space between columns", LastModified = "2010/07/26", Value = "Space <strong>between</strong> columns")]
    public string HorizontalSpaceBetweenColumns => this[nameof (HorizontalSpaceBetweenColumns)];

    /// <summary>phrase: Space above and below columns</summary>
    [ResourceEntry("VerticalSpaceAboveAndBelowColumns", Description = "phrase: Space above and below columns", LastModified = "2010/07/26", Value = "Space <strong>above and below</strong> columns")]
    public string VerticalSpaceAboveAndBelowColumns => this[nameof (VerticalSpaceAboveAndBelowColumns)];

    /// <summary>phrase: Individual spaces per column</summary>
    [ResourceEntry("SpacesSideBySide", Description = "phrase: Individual spaces per column", LastModified = "2010/07/26", Value = "Individual spaces per column")]
    public string SpacesSideBySide => this[nameof (SpacesSideBySide)];

    /// <summary>phrase: Equal spaces for all columns</summary>
    [ResourceEntry("EqualSpaces", Description = "phrase: Equal spaces for all columns", LastModified = "2010/07/26", Value = "Equal spaces for all columns")]
    public string EqualSpaces => this[nameof (EqualSpaces)];

    /// <summary>word: Top</summary>
    [ResourceEntry("Top", Description = "word: Top", LastModified = "2009/11/21", Value = "Top")]
    public string Top => this[nameof (Top)];

    /// <summary>word: Right</summary>
    [ResourceEntry("Right", Description = "word: Right", LastModified = "2009/11/21", Value = "Right")]
    public string Right => this[nameof (Right)];

    /// <summary>word: Bottom</summary>
    [ResourceEntry("Bottom", Description = "word: Bottom", LastModified = "2009/11/21", Value = "Bottom")]
    public string Bottom => this[nameof (Bottom)];

    /// <summary>word: Left</summary>
    [ResourceEntry("Left", Description = "word: Left", LastModified = "2009/11/21", Value = "Left")]
    public string Left => this[nameof (Left)];

    /// <summary>phrase: You must enter a value between {0} and {1}</summary>
    [ResourceEntry("AValueBetween", Description = "phrase: You must enter a value between {0} and {1}.", LastModified = "2009/11/21", Value = "You must enter a value between {0} and {1}.")]
    public string AValueBetween => this[nameof (AValueBetween)];

    /// <summary>phrase: Edit Layout Element</summary>
    [ResourceEntry("EditLayoutElement", Description = "phrase: Edit Layout Element", LastModified = "2009/11/22", Value = "Edit layout element")]
    public string EditLayoutElement => this[nameof (EditLayoutElement)];

    /// <summary>word: Themes</summary>
    [ResourceEntry("Themes", Description = "phrase: Themes", LastModified = "2010/07/09", Value = "Themes")]
    public string Themes => this[nameof (Themes)];

    /// <summary>word : Theme</summary>
    [ResourceEntry("Theme", Description = "word: Theme", LastModified = "2010/07/12", Value = "Theme")]
    public string Theme => this[nameof (Theme)];

    /// <summary>phrase: -- Select a theme --</summary>
    [ResourceEntry("SelectATheme", Description = "phrase: Select a theme", LastModified = "2010/07/26", Value = "-- Select a theme --")]
    public string SelectATheme => this[nameof (SelectATheme)];

    /// <summary>Label: Wrapper</summary>
    [ResourceEntry("WrapperClass", Description = "Label: Wrapper", LastModified = "2010/09/09", Value = "Wrapper")]
    public string WrapperClass => this[nameof (WrapperClass)];

    /// <summary>Label: Wrapper</summary>
    [ResourceEntry("SaveToAllWarning", Description = "Label: SaveToAllWarning", LastModified = "2014/10/23", Value = "This widget will have the same content and properties in all translations. Any changes in content and properties in other translations will be lost.")]
    public string SaveToAllWarning => this[nameof (SaveToAllWarning)];
  }
}
