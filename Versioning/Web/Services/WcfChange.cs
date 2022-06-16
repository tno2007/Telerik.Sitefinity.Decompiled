// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.Services.WcfChange
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Versioning.Web.Services
{
  /// <summary>
  /// Version Info added to item's WCF context objects(content and pages) that is used to control UI on History Version Preview screens
  /// </summary>
  [DataContract]
  public class WcfChange : WcfItemBase
  {
    public WcfChange()
    {
    }

    public WcfChange(Change change)
    {
      bool isSyncedItem = change.Parent.IsSyncedItem;
      this.Id = change.Id;
      this.IsPublishedVersion = change.IsPublishedVersion;
      this.Comment = change.Comment ?? "";
      this.Label = change.Label;
      this.LastModified = change.LastModified;
      this.DateCreated = change.DateCreated != new DateTime() ? change.DateCreated : change.LastModified;
      this.CreatedByUserName = UserProfilesHelper.GetUserDisplayName(change.Owner);
      this.VersionNumber = change.Version;
      this.Version = VersionDataProvider.BuildUIVersionNumber(change.Version);
      this.Owner = change.Owner;
      this.AdditionalInfo = change.AdditionalInfo;
      this.ItemId = change.Parent.Id;
      this.ChangeType = change.ChangeType;
      this.IsLastPublishedVersion = change.IsLastPublishedVersion;
      this.ChangeDescription = VersionDataProvider.GetChangeDescription(change, isSyncedItem);
      this.AvailableLanguages = change.GetLanguagesFromMetadata();
      this.Metadata = this.FormatMetadata(change);
      if (!isSyncedItem || change.Culture == CultureInfo.InvariantCulture.LCID)
        return;
      this.CultureInfo = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(change.Culture);
      this.Culture = this.CultureInfo.Name;
    }

    private string FormatMetadata(Change change)
    {
      if (!change.Parent.IsSyncedItem || string.IsNullOrEmpty(change.Metadata))
        return string.Empty;
      string str1 = "<ul class=\"sfSeparatorList sfClearfix\">{0}</ul>";
      string str2 = "<li><a sys:href=\"\" data-command-name=\"versionLang\" data-command-arg=\"{0}\">{0}</a></li>";
      StringBuilder stringBuilder = new StringBuilder();
      foreach (string str3 in (IEnumerable<string>) change.GetLanguagesFromMetadata())
        stringBuilder.Append(str2.Arrange((object) str3));
      return str1.Arrange((object) stringBuilder);
    }

    /// <summary>Gets or sets the identity of this change.</summary>
    /// <value>The id.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the next version number.</summary>
    /// <value>The next version number.</value>
    [DataMember]
    public int NextVersionNumber { get; set; }

    /// <summary>Gets or sets the previous version number.</summary>
    /// <value>The previous version number.</value>
    [DataMember]
    public int PrevVersionNumber { get; set; }

    /// <summary>Gets or sets the item id that was changed.</summary>
    /// <value>The item id.</value>
    [DataMember]
    public Guid ItemId { get; set; }

    /// <summary>Gets or sets the history version number.</summary>
    /// <value>The version.</value>
    [DataMember]
    public int VersionNumber { get; set; }

    /// <summary>
    /// Gets or sets the version number in a user friendly decimal format like 1.53 or 5.20.
    /// This is needed since the original version number is stored as a single integer which has to represented as a decimal
    /// that behaves like: 5.100 is greater than 5.9
    /// </summary>
    /// <value>The version.</value>
    [DataMember]
    public string Version { get; set; }

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    [DataMember]
    public Guid Owner { get; set; }

    /// <summary>Gets or sets the comment.</summary>
    /// <value>The comment.</value>
    [DataMember]
    public string Comment { get; set; }

    /// <summary>Gets or sets the label.</summary>
    /// <value>The label.</value>
    [DataMember]
    public string Label { get; set; }

    [DataMember]
    public string ChangeType { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is published version.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is published version; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsPublishedVersion { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance is the last published version for the particluar content item.
    /// </summary>
    [DataMember]
    public bool IsLastPublishedVersion { get; set; }

    [DataMember]
    public string ChangeDescription { get; set; }

    /// <summary>Gets or sets the last modified.</summary>
    /// <value>The last modified.</value>
    [DataMember]
    public DateTime LastModified { get; set; }

    /// <summary>Gets or sets the created date.</summary>
    /// <value>The created date.</value>
    [DataMember]
    public DateTime DateCreated { get; set; }

    /// <summary>Gets or sets the name of the create by user.</summary>
    /// <value>The name of the create by user.</value>
    [DataMember]
    public string CreatedByUserName { get; set; }

    /// <summary>Gets or sets the previous version id.</summary>
    /// <value>The previous id.</value>
    [DataMember]
    public string PreviousId { get; set; }

    /// Gets or sets the next version id.
    [DataMember]
    public string NextId { get; set; }

    /// Gets or sets the metadata for the <see cref="T:Telerik.Sitefinity.Versioning.Model.Change" />
    /// .
    [DataMember]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string Metadata { get; set; }

    [DataMember]
    public string AdditionalInfo { get; set; }

    /// Gets or sets the culture for the revision version.
    [DataMember]
    public string Culture { get; set; }

    /// Gets or sets the available languages.
    [DataMember]
    public IList<string> AvailableLanguages { get; set; }

    /// Gets or sets the object of type <see cref="P:Telerik.Sitefinity.Versioning.Web.Services.WcfChange.CultureInfo" />
    /// , indicating the culture for the revisioned version.
    internal CultureInfo CultureInfo { get; set; }
  }
}
