// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ContentViewPluginMessages
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// validation messages used in content fields entry validation
  /// </summary>
  [ObjectInfo("ContentViewPluginMessages", ResourceClassId = "ContentViewPluginMessages")]
  public class ContentViewPluginMessages : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ContentViewPluginMessages" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public ContentViewPluginMessages()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.ValidationMessages" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public ContentViewPluginMessages(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Messages for ContentView Plugins</summary>
    [ResourceEntry("ContentViewPluginMessagesTitle", Description = "The title of this class.", LastModified = "2009/09/26", Value = "Messages for ContentView Plugins")]
    public string ContentViewPluginMessagesTitle => this[nameof (ContentViewPluginMessagesTitle)];

    /// <summary>Messages for ContentView Plugins</summary>
    [ResourceEntry("ContentViewPluginMessagesTitlePlural", Description = "The titleplural of this class.", LastModified = "2009/09/26", Value = "Messages for ContentView Plugins")]
    public string ContentViewPluginMessagesTitlePlural => this[nameof (ContentViewPluginMessagesTitlePlural)];

    /// <summary>
    /// Contains localizable messages for ContentView plugins that are integrated within Sitefinity.
    /// </summary>
    [ResourceEntry("ContentViewPluginMessagesDescription", Description = "The description of this class.", LastModified = "2009/09/26", Value = "Contains localizable messages for ContentView plugins that are integrated within Sitefinity.")]
    public string ContentViewPluginMessagesDescription => this[nameof (ContentViewPluginMessagesDescription)];

    /// <summary>Messages: Average: {0:F2} out of {1} people.</summary>
    [ResourceEntry("AverageXOutOfXPeople", Description = "Displays the subtitle of a Rating control that summarizes the voting results", LastModified = "2009/11/26", Value = "Average: {0:F2} out of {1} people.")]
    public string AverageXOutOfXPeople => this[nameof (AverageXOutOfXPeople)];

    /// <summary>Messages: Average: {0:F2} out of 1 person.</summary>
    [ResourceEntry("AverageXOutOfOnePerson", Description = "Displays the subtitle of a Rating control that summarizes the voting results", LastModified = "2009/11/26", Value = "Average: {0:F2} out of 1 person.")]
    public string AverageXOutOfOnePerson => this[nameof (AverageXOutOfOnePerson)];

    /// <summary>Messages: Nobody has voted so far.</summary>
    [ResourceEntry("NobodyHasVotedSoFar", Description = "Subtitle of the Rating control that is displayed when there are no votes for a particular item.", LastModified = "2009/11/26", Value = "Nobody has voted so far.")]
    public string NobodyHasVotedSoFar => this[nameof (NobodyHasVotedSoFar)];

    /// <summary>Messages: Are you sure you want to reset rating?</summary>
    [ResourceEntry("AreYouSureToResetRating", Description = "Confirmation message shown before resetting rating results.", LastModified = "2009/11/26", Value = "Are you sure you want to reset rating?")]
    public string AreYouSureToResetRating => this[nameof (AreYouSureToResetRating)];
  }
}
