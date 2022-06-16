// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.NavigationSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Class containing the navigation settings.</summary>
  public class NavigationSettings
  {
    /// <summary>
    /// Gets or sets a serialized array of the selected pages.
    /// </summary>
    /// <value>The a serialized array of selected pages.</value>
    [DataMember]
    public string SelectedPagesSerialized { get; set; }

    /// <summary>Gets or sets the levels to include.</summary>
    [DataMember]
    public virtual int? LevelsToInclude { get; set; }

    /// <summary>
    ///     Gets or sets the page links to display selection mode.
    /// </summary>
    /// <value>The page display mode.</value>
    [DataMember]
    public string SelectionModeString { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [show parent page].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [show parent page]; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool ShowParentPage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether should open external page in new tab.
    /// </summary>
    /// <value>
    /// <c>true</c> if should open external page in new tab; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool OpenExternalPageInNewTab { get; set; }

    /// <summary>
    /// Gets or sets the identifier of the page that is selected if SelectionMode is SelectedPageChildren.
    /// </summary>
    /// <value>The identifier of the page that is selected if SelectionMode is SelectedPageChildren.</value>
    [DataMember]
    public Guid SelectedPageId { get; set; }

    /// <summary>Gets or sets the current sitemap node.</summary>
    public string CurrentSiteMapNodeKey { get; set; }
  }
}
