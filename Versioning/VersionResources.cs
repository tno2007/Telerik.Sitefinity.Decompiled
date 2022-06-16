// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.VersionResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Versioning
{
  /// <summary>
  /// Represents string resources for Version Control module.
  /// </summary>
  [ObjectInfo("VersionResources", ResourceClassId = "VersionResources", TitlePlural = "VersionResourcesTitlePlural")]
  public class VersionResources : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Versioning.VersionResources" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public VersionResources()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Versioning.VersionResources" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public VersionResources(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Version Control</summary>
    [ResourceEntry("VersionResourcesTitle", Description = "The title of this class.", LastModified = "2009/12/10", Value = "Version Control")]
    public string VersionResourcesTitle => this[nameof (VersionResourcesTitle)];

    /// <summary>
    /// Contains localizable resources for Version Control user interface.
    /// </summary>
    [ResourceEntry("VersionResourcesDescription", Description = "The description of this class.", LastModified = "2009/12/10", Value = "Contains localizable resources for Version Control module.")]
    public string VersionResourcesDescription => this[nameof (VersionResourcesDescription)];

    /// <summary>Version Control</summary>
    [ResourceEntry("ModuleTitle", Description = "The title of Version Control module.", LastModified = "2009/12/10", Value = "Version Control")]
    public string ModuleTitle => this[nameof (ModuleTitle)];

    /// <summary>The title plural of Version Control module.</summary>
    [ResourceEntry("VersionResourcesTitlePlural", Description = "The title plural of Version Control module.", LastModified = "2009/12/10", Value = "Version Control Title plural.")]
    public string VersionResourcesTitlePlural => this[nameof (VersionResourcesTitlePlural)];

    /// <summary>
    /// Provides control over current and historical versions of content and business objects.
    /// </summary>
    [ResourceEntry("ModuleDescription", Description = "The description of Version Control module.", LastModified = "2009/12/10", Value = "Provides control over current and historical versions of content and business objects.")]
    public string ModuleDescription => this[nameof (ModuleDescription)];

    /// <summary>Version Control</summary>
    [ResourceEntry("VersionConfigTitle", Description = "The title of the Version Control configuration section.", LastModified = "2009/12/10", Value = "Version Control")]
    public string VersionConfigTitle => this[nameof (VersionConfigTitle)];

    /// <summary>
    /// Defines pages configuration settings for Version Control module.
    /// </summary>
    [ResourceEntry("VersionConfigDescription", Description = "The description of Version Control configuration section.", LastModified = "2009/12/10", Value = "Defines pages configuration settings for Version Control module.")]
    public string VersionConfigDescription => this[nameof (VersionConfigDescription)];

    [ResourceEntry("RevisionHistoryLabel", Description = "Title of the revision history screen", LastModified = "2010/6/10", Value = "Revision History of ")]
    public string RevisionHistoryLabel => this[nameof (RevisionHistoryLabel)];

    [ResourceEntry("EditNoteLabel", Description = "Label of the edit note button", LastModified = "2010/6/10", Value = "Edit")]
    public string EditNoteLabel => this[nameof (EditNoteLabel)];

    [ResourceEntry("WriteNoteLabel", Description = "Label of the write note button", LastModified = "2010/6/10", Value = "Write note")]
    public string WriteNoteLabel => this[nameof (WriteNoteLabel)];

    [ResourceEntry("DeleteNoteLabel", Description = "Label of the delete note button", LastModified = "2010/6/10", Value = "Delete")]
    public string DeleteNoteLabel => this[nameof (DeleteNoteLabel)];

    [ResourceEntry("HistoryMenuItemTitle", Description = "history menu item title", LastModified = "2010/6/10", Value = "Revision History")]
    public string HistoryMenuItemTitle => this[nameof (HistoryMenuItemTitle)];

    [ResourceEntry("CompareVersionWrongSelection", Description = "Compare Version Wrong number of selected items (history screen ) ", LastModified = "2010/6/10", Value = "Only two items can be compared. Select only two items to make a comparison.")]
    public string CompareVersionWrongSelection => this[nameof (CompareVersionWrongSelection)];

    /// <summary>phrase: Previously Published</summary>
    [ResourceEntry("PreviouslyPublished", Description = "History screen label related to version change type", LastModified = "2010/6/23", Value = "Previously Published")]
    public string PreviouslyPublished => this[nameof (PreviouslyPublished)];

    /// <summary>phrase: Last Published</summary>
    [ResourceEntry("LastPublished", Description = "History screen label related to version change type", LastModified = "2017/8/8", Value = "Last Published")]
    public string LastPublished => this[nameof (LastPublished)];

    /// <summary>word: draft</summary>
    [ResourceEntry("Draft", Description = "History screen label related to version change type", LastModified = "2010/6/23", Value = "Draft")]
    public string Draft => this[nameof (Draft)];

    [ResourceEntry("InitialDraft", Description = "History screen label related to version change type", LastModified = "2010/6/23", Value = "Initial Draft")]
    public string InitialDraft => this[nameof (InitialDraft)];

    /// <summary>Phrase: Back to</summary>
    [ResourceEntry("BackTo", Description = "Phrase: Back to", LastModified = "2010/10/15", Value = "Back to")]
    public string BackTo => this[nameof (BackTo)];

    /// <summary>Phrase: revisions histor</summary>
    [ResourceEntry("revisionsHistory", Description = "Phrase: revisions histor", LastModified = "2010/10/15", Value = "revisions history")]
    public string revisionsHistory => this[nameof (revisionsHistory)];

    /// <summary>Phrase: Compare versions for</summary>
    [ResourceEntry("CompareFersionsFor", Description = "Phrase: Compare versions for", LastModified = "2010/10/15", Value = "Compare versions for")]
    public string CompareFersionsFor => this[nameof (CompareFersionsFor)];

    /// <summary>Phrase: Version {0} ({1})</summary>
    [ResourceEntry("VersionInfoTemplate", Description = "Phrase: Version {0} ({1}), where {0} is version number and {1} is version status", LastModified = "2010/10/15", Value = "Version {0} ({1})")]
    public string VersionInfoTemplate => this[nameof (VersionInfoTemplate)];

    /// <summary>
    /// Phrase: modified by {0} &lt;span class='sfNowrapLine'&gt;on {1}&lt;/span&gt;
    /// </summary>
    [ResourceEntry("ModifiedByTemplate", Description = "Phrase:modified by {0} <span class='sfNowrapLine'>on {1}</span>, where {0} is user name and {1} is a date", LastModified = "2010/10/15", Value = "modified by {0} <span class='sfNowrapLine'>on {1}</span>")]
    public string ModifiedByTemplate => this[nameof (ModifiedByTemplate)];

    /// <summary>Phrase: Edit note for version</summary>
    [ResourceEntry("EditNoteForVersion", Description = "Phrase: Edit note for version", LastModified = "2010/10/15", Value = "Edit note for version")]
    public string EditNoteForVersion => this[nameof (EditNoteForVersion)];

    /// <summary>Phrase: Write note for version</summary>
    [ResourceEntry("WriteNoteForVersion", Description = "Phrase: Write note for version", LastModified = "2010/10/15", Value = "Write note for version")]
    public string WriteNoteForVersion => this[nameof (WriteNoteForVersion)];

    /// <summary>
    /// Phrase: The note was not updated.An error occurred while saving it.
    /// </summary>
    [ResourceEntry("ErrorWhileUpdatingNote", Description = "Phrase: The note was not updated.An error occurred while saving it.", LastModified = "2012/01/05", Value = "The note was not updated.An error occurred while saving it.")]
    public string ErrorWhileUpdatingNote => this[nameof (ErrorWhileUpdatingNote)];

    /// <summary>Word: Write</summary>
    [ResourceEntry("Write", Description = "Phrase: Write", LastModified = "2010/10/15", Value = "Write")]
    public string Write => this[nameof (Write)];

    /// <summary>Word: Edit</summary>
    [ResourceEntry("Edit", Description = "Phrase: Edit", LastModified = "2010/10/15", Value = "Edit")]
    public string Edit => this[nameof (Edit)];

    /// <summary>Word: Modified</summary>
    [ResourceEntry("Modified", Description = "Word: Modified", LastModified = "2010/10/15", Value = "Modified")]
    public string Modified => this[nameof (Modified)];

    /// <summary>phrase: Hide note</summary>
    [ResourceEntry("HideNote", Description = "Phrase: Hide note", LastModified = "2010/10/15", Value = "Hide note")]
    public string HideNote => this[nameof (HideNote)];

    /// <summary>phrase: Modified: at {0} by {1}</summary>
    [ResourceEntry("ModifiedAtDateByUserName", Description = "Phrase: Modified: at {0} by {1}", LastModified = "2010/10/15", Value = "Modified: at {0} by {1}")]
    public string ModifiedAtDateByUserName => this[nameof (ModifiedAtDateByUserName)];

    /// <summary>Phrase: Version notes</summary>
    [ResourceEntry("VersionNotes", Description = "Phrase: Version notes", LastModified = "2010/10/15", Value = "Version notes")]
    public string VersionNotes => this[nameof (VersionNotes)];

    /// <summary>
    /// Phrase: &lt;span class='sfTitleInBetween'&gt;Version&lt;/span&gt; {0} &lt;span class='sfTitleInBetween'&gt;of&lt;/span&gt; &lt;em title=\"{1}\"&gt;{2}&lt;/em&gt;
    /// </summary>
    [ResourceEntry("ItemVersionOfClientTemplate", Description = "Phrase: <span class='sfTitleInBetween'>Version</span> {0} <span class='sfTitleInBetween'>of</span> <em title=\"{1}\">{2}</em>", LastModified = "2010/10/15", Value = "<span class='sfTitleInBetween'>Version</span> {0} <span class='sfTitleInBetween'>of</span> <em title=\"{1}\">{2}</em>")]
    public string ItemVersionOfClientTemplate => this[nameof (ItemVersionOfClientTemplate)];

    /// <summary>Phrase: (previously published)</summary>
    [ResourceEntry("PreviouslyPublishedBrackets", Description = "Phrase: (previously published)", LastModified = "2010/10/15", Value = "(previously published)")]
    public string PreviouslyPublishedBrackets => this[nameof (PreviouslyPublishedBrackets)];

    /// <summary>
    /// Phrase: It is not allowed to delete last published version
    /// </summary>
    [ResourceEntry("CannotDeleteLastPublishedVersion", Description = "Phrase: It is not allowed to delete last published version", LastModified = "2010/10/15", Value = "It is not allowed to delete last published version")]
    public string CannotDeleteLastPublishedVersion => this[nameof (CannotDeleteLastPublishedVersion)];

    /// <summary>Phrase: Full screen</summary>
    [ResourceEntry("FullScreen", Description = "Phrase: It is not allowed to delete last published version", LastModified = "2010/10/15", Value = "Full screen")]
    public string FullScreen => this[nameof (FullScreen)];

    /// <summary>Phrase: Exit Full screen</summary>
    [ResourceEntry("ExitFullScreen", Description = "Phrase: It is not allowed to delete last published version", LastModified = "2010/10/15", Value = "Exit Full screen")]
    public string ExitFullScreen => this[nameof (ExitFullScreen)];

    /// <summary>Phrase: Exit Full screen</summary>
    [ResourceEntry("Updated", Description = "Phrase: value in newer version is different from value in compared version", LastModified = "2019/03/11", Value = "Updated")]
    public string Updated => this[nameof (Updated)];
  }
}
