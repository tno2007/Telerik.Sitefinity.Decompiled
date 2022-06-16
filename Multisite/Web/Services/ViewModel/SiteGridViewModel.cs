// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.Services.ViewModel.SiteGridViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration.Web;

namespace Telerik.Sitefinity.Multisite.Web.Services.ViewModel
{
  /// <summary>Represents the site view model for the grid.</summary>
  [DataContract]
  public class SiteGridViewModel
  {
    /// <summary>Gets or sets the site id.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the site name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the site is offline.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the site is offline; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsOffline { get; set; }

    /// <summary>Gets or sets the UI status of the site.</summary>
    [DataMember]
    public string UIStatus { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the instance is allowed to be set online or offline.
    /// </summary>
    [DataMember]
    public bool IsAllowedStartStop { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is allowed to be deleted.
    /// </summary>
    [DataMember]
    public bool IsDeleteable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is allowed to set permissions.
    /// </summary>
    [DataMember]
    public bool IsAllowedSetPermissions { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is allowed to configure modules.
    /// </summary>
    [DataMember]
    public bool IsAllowedConfigureModules { get; set; }

    /// <summary>Gets or sets the site map root node id.</summary>
    [DataMember]
    public Guid SiteMapRootNodeId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the site is the default one.
    /// </summary>
    [DataMember]
    public bool IsDefault { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is allowed to create and edit a site.
    /// </summary>
    [DataMember]
    public bool IsAllowedCreateEdit { get; set; }

    /// <summary>Gets or sets a list of public content cultures.</summary>
    [DataMember]
    public string[] CultureDisplayNames { get; set; }

    [DataMember]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public CultureViewModel[] Cultures { get; set; }

    /// <summary>Gets or sets the site URL.</summary>
    [DataMember]
    public string SiteUrl { get; set; }

    /// <summary>Gets or sets the site configuration mode.</summary>
    /// <value>The site configuration mode.</value>
    [DataMember]
    public int SiteConfigurationMode { get; set; }
  }
}
