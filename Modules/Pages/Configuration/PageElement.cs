// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.PageElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Represents configuration element for a page.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendPageElementDescription", Title = "BackendPageElementTitle")]
  public class PageElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.PageElement" /> class.
    /// </summary>
    public PageElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:BackendPageElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public PageElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the programmatic name of the configuration element.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemName", Title = "ItemNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the item that appears in the URL.
    /// </summary>
    [ConfigurationProperty("urlName", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UrlNameDescription", Title = "UrlNameTitle")]
    public string UrlName
    {
      get => (string) this["urlName"];
      set => this["urlName"] = (object) value;
    }

    /// <summary>Gets or sets the ID of the page.</summary>
    /// <value>The page pageId.</value>
    [ConfigurationProperty("pageId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageIdDescription", Title = "PageIdTitle")]
    public Guid PageId
    {
      get => (Guid) this["pageId"];
      set => this["pageId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name that appears on navigational controls such as site menu.
    /// </summary>
    [ConfigurationProperty("menuName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MenuNameDescription", Title = "MenuNameTitle")]
    public string MenuName
    {
      get => (string) this["menuName"];
      set => this["menuName"] = (object) value;
    }

    /// <summary>
    /// Describes the purpose of the page represented by this configuration element.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty("description", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageElementDescription", Title = "ItemDescriptionCaption")]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    /// <summary>
    /// Defines global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class ID.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display the page in navigation controls.
    /// </summary>
    /// <value><c>true</c> if [show in navigation]; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("showInNavigation", DefaultValue = true)]
    public bool ShowInNavigation
    {
      get => (bool) this["showInNavigation"];
      set => this["showInNavigation"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the navigation node of this page should participate in the URL.
    /// </summary>
    /// <value><c>true</c> if [render as link]; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("renderAsLink", DefaultValue = true)]
    public bool RenderAsLink
    {
      get => (bool) this["renderAsLink"];
      set => this["renderAsLink"] = (object) value;
    }

    /// <summary>Gets or sets the ordinal number of the page.</summary>
    /// <value>The ordinal.</value>
    [ConfigurationProperty("ordinal", DefaultValue = 0.0f)]
    public float Ordinal
    {
      get => (float) this["ordinal"];
      set => this["ordinal"] = (object) value;
    }

    /// <summary>Gets a collection presentation elements.</summary>
    /// <value>The presentation.</value>
    [ConfigurationProperty("presentation")]
    public ConfigElementDictionary<string, PresentationElement> Presentation => (ConfigElementDictionary<string, PresentationElement>) this["presentation"];

    /// <summary>
    /// Gets a collection of user-defined parameters for the provider.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    /// <summary>Permission set used to test for thew view action</summary>
    [ConfigurationProperty("permissionSet", DefaultValue = "Backend")]
    public string PermissionSet
    {
      get => (string) this["permissionSet"];
      set => this["permissionSet"] = (object) value;
    }

    /// <summary>
    /// security action name used to test if the page is visible
    /// </summary>
    [ConfigurationProperty("viewActionName", DefaultValue = "")]
    public string ViewActionName
    {
      get => (string) this["viewActionName"];
      set => this["viewActionName"] = (object) value;
    }

    /// <summary>Gets a collection of sub pages.</summary>
    /// <value>The backend pages.</value>
    [ConfigurationProperty("backendPages")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendPagesDescription", Title = "BackendPagesTitle")]
    public virtual ConfigElementDictionary<string, PageElement> Pages => (ConfigElementDictionary<string, PageElement>) this["backendPages"];
  }
}
