// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ISiteSyncLogEntry
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.SiteSync
{
  /// <summary>
  /// Interface for persistent SiteSync entry, containing the information required to load the real item.
  /// </summary>
  public interface ISiteSyncLogEntry : ICloneable, ICopyProvider<ISiteSyncLogEntry>
  {
    /// <summary>Gets or sets the unique identity of the item.</summary>
    /// <value>The id.</value>
    Guid Id { get; }

    /// <summary>
    /// Gets or sets the id of the server where this item should be synced.
    /// </summary>
    /// <value>The id of the server.</value>
    string ServerId { get; set; }

    /// <summary>Gets or sets the version of the item.</summary>
    /// <value>The version.</value>
    int Version { get; set; }

    /// <summary>Gets or sets the type of the item.</summary>
    /// <value>The type.</value>
    string TypeName { get; set; }

    /// <summary>Gets or sets the title of the item.</summary>
    /// <value>The title.</value>
    string Title { get; set; }

    /// <summary>Gets or sets the title for the item's parent.</summary>
    /// <value>The title for the item's parent.</value>
    string ParentTitle { get; set; }

    /// <summary>Gets or sets the status details of the item.</summary>
    /// <value>The status details.</value>
    string StatusDetails { get; set; }

    /// <summary>Gets or sets the status of the item.</summary>
    /// <value>The status.</value>
    string Status { get; set; }

    /// <summary>Gets or sets the provider of the item.</summary>
    /// <value>The provider.</value>
    string Provider { get; set; }

    /// <summary>
    /// Gets or sets the value if the item is modified since last sync.
    /// </summary>
    /// <value>The value if the item is modified since last sync.</value>
    bool ModifiedSinceLastSync { get; set; }

    /// <summary>Gets or sets the language of the item.</summary>
    /// <value>The language.</value>
    string Language { get; set; }

    /// <summary>Gets or sets the unique identity of the synced item.</summary>
    /// <value>The unique identity of the synced item.</value>
    string ItemId { get; set; }

    /// <summary>Gets or sets the date when the item was modified.</summary>
    /// <value>The date when the item was modified.</value>
    DateTime Timestamp { get; set; }

    /// <summary>Gets or sets the user that modified the item.</summary>
    /// <value>The user that modified the item.</value>
    Guid UserId { get; set; }

    /// <summary>
    /// Gets or sets the name of the user that modified the item.
    /// </summary>
    /// <value>The name of the user that modified the item.</value>
    string UserName { get; set; }

    /// <summary>
    /// The action performed on the item - one of the values defined in the <see cref="T:DataEventAction" /> class.
    /// </summary>
    string ItemAction { get; set; }

    /// <summary>Gets or sets the additional info for the item.</summary>
    /// <value>The additional info for the item.</value>
    string AdditionalInfo { get; set; }

    /// <summary>
    /// Gets or sets the ids of the sites of the modified items
    /// </summary>
    /// <value>The ids of the sites to which the items belong.</value>
    IList<Guid> Sites { get; set; }
  }
}
