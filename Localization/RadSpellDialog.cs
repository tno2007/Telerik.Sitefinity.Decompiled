// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadSpellDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadSpellDialogDescription", Name = "RadSpell.Dialog", ResourceClassId = "RadSpell.Dialog", Title = "RadSpellDialogTitle", TitlePlural = "RadSpellDialogTitlePlural")]
  public sealed class RadSpellDialog : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadSpellDialog" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadSpellDialog()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadSpellDialog" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadSpellDialog(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadSpell Dialog</summary>
    [ResourceEntry("RadSpellDialogTitle", Description = "The title of this class.", LastModified = "2009/09/16", Value = "RadSpell Dialog")]
    public string RadSpellDialogTitle => this[nameof (RadSpellDialogTitle)];

    /// <summary>RadSpell Dialog</summary>
    [ResourceEntry("RadSpellDialogTitlePlural", Description = "The title plural of this class.", LastModified = "2009/09/16", Value = "RadSpell Dialog")]
    public string RadSpellDialogTitlePlural => this[nameof (RadSpellDialogTitlePlural)];

    /// <summary>Resource strings for RadSpell dialog.</summary>
    [ResourceEntry("RadSpellDialogDescription", Description = "The description of this class.", LastModified = "2009/09/16", Value = "Resource strings for RadSpell dialog.")]
    public string RadSpellDialogDescription => this[nameof (RadSpellDialogDescription)];

    [ResourceEntry("AddCustom", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Add Custom")]
    public string AddCustom => this[nameof (AddCustom)];

    [ResourceEntry("AddWord1", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Are you sure you want to add '")]
    public string AddWord1 => this[nameof (AddWord1)];

    [ResourceEntry("AddWord2", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "' to the custom dictionary?")]
    public string AddWord2 => this[nameof (AddWord2)];

    [ResourceEntry("Cancel", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Close")]
    public string Cancel => this[nameof (Cancel)];

    [ResourceEntry("Change", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Change")]
    public string Change => this[nameof (Change)];

    [ResourceEntry("ChangeAll", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Change All")]
    public string ChangeAll => this[nameof (ChangeAll)];

    [ResourceEntry("ChangesMade", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "You have made changes to the text.")]
    public string ChangesMade => this[nameof (ChangesMade)];

    [ResourceEntry("Confirm", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Do you want to apply or cancel the changes to the text so far?")]
    public string Confirm => this[nameof (Confirm)];

    [ResourceEntry("Help", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Help")]
    public string Help => this[nameof (Help)];

    [ResourceEntry("Ignore", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Ignore")]
    public string Ignore => this[nameof (Ignore)];

    [ResourceEntry("IgnoreAll", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Ignore All")]
    public string IgnoreAll => this[nameof (IgnoreAll)];

    [ResourceEntry("NoPermission", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "You don't have permission to access this page or your cookie has expired!<br />Please, refresh the page!")]
    public string NoPermission => this[nameof (NoPermission)];

    [ResourceEntry("Nosuggestions", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "No suggestions")]
    public string Nosuggestions => this[nameof (Nosuggestions)];

    [ResourceEntry("NotInDictionary", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Not in Dictionary:")]
    public string NotInDictionary => this[nameof (NotInDictionary)];

    [ResourceEntry("ProgressMessage", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Spell Checking in progress....")]
    public string ProgressMessage => this[nameof (ProgressMessage)];

    [ResourceEntry("ReservedResource", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Please do not change/remove this resource. ")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("SpellCheckComplete", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "The Spell Check is complete!")]
    public string SpellCheckComplete => this[nameof (SpellCheckComplete)];

    [ResourceEntry("Suggestions", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Suggestions:")]
    public string Suggestions => this[nameof (Suggestions)];

    [ResourceEntry("Title", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Spell Check")]
    public string Title => this[nameof (Title)];

    [ResourceEntry("Undo", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Undo")]
    public string Undo => this[nameof (Undo)];

    [ResourceEntry("UndoEdit", Description = "RadSpellDialog resource strings.", LastModified = "2014/06/19", Value = "Undo Edit")]
    public string UndoEdit => this[nameof (UndoEdit)];
  }
}
