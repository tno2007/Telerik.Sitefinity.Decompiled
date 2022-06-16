// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.PageDataElement
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
  public class PageDataElement : PageElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.PageDataElement" /> class.
    /// </summary>
    public PageDataElement()
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
    public PageDataElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the name the template to set for this page.
    /// </summary>
    [ConfigurationProperty("templateName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TemplateNameDescription", Title = "TemplateNameTitle")]
    public string TemplateName
    {
      get => (string) this["templateName"];
      set => this["templateName"] = (object) value;
    }

    /// <summary>Gets or sets the ID of the page.</summary>
    /// <value>The page pageId.</value>
    [ConfigurationProperty("pageId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageIdDescription", Title = "PageIdTitle")]
    public new Guid PageId
    {
      get => (Guid) this["pageId"];
      set => this["pageId"] = (object) value;
    }

    /// <summary>
    /// Defines global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class ID.</value>
    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public new string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display the page in navigation controls.
    /// </summary>
    /// <value><c>true</c> if [show in navigation]; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("showInNavigation", DefaultValue = true)]
    public new bool ShowInNavigation
    {
      get => (bool) this["showInNavigation"];
      set => this["showInNavigation"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the navigation node of this page should participate in the URL.
    /// </summary>
    /// <value><c>true</c> if [render as link]; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("renderAsLink", DefaultValue = true)]
    public new bool RenderAsLink
    {
      get => (bool) this["renderAsLink"];
      set => this["renderAsLink"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether view state is enabled for this page.
    /// </summary>
    /// <value><c>true</c> if [enable view state]; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("enableViewState", DefaultValue = false)]
    public bool EnableViewState
    {
      get => (bool) this["enableViewState"];
      set => this["enableViewState"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether script manager should be automatically added to the page.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [include script manager]; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("includeScriptManager", DefaultValue = false)]
    public bool IncludeScriptManager
    {
      get => (bool) this["includeScriptManager"];
      set => this["includeScriptManager"] = (object) value;
    }

    /// <summary>Gets or sets the ordinal number of the page.</summary>
    /// <value>The ordinal.</value>
    [ConfigurationProperty("ordinal", DefaultValue = 0.0f)]
    public new float Ordinal
    {
      get => (float) this["ordinal"];
      set => this["ordinal"] = (object) value;
    }

    /// <summary>Gets a collection presentation elements.</summary>
    /// <value>The presentation.</value>
    [ConfigurationProperty("presentation")]
    public new ConfigElementDictionary<string, PresentationElement> Presentation => (ConfigElementDictionary<string, PresentationElement>) this["presentation"];

    /// <summary>
    /// Defines what Sitefinity should put in the &lt;title&gt;&lt;/title&gt; tag inside the page's &lt;head&gt;&lt;/head&gt;.
    /// </summary>
    /// <value>A valid HTML title</value>
    [ConfigurationProperty("htmlTitle", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageElement_HtmlTitle_Description", Title = "PageElement_HtmlTitle_Title")]
    public string HtmlTitle
    {
      get => (string) this["htmlTitle"];
      set => this["htmlTitle"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of user-defined parameters for the provider.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public new NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    /// <summary>Permission set used to test for thew view action</summary>
    [ConfigurationProperty("permissionSet", DefaultValue = "Backend")]
    public new string PermissionSet
    {
      get => (string) this["permissionSet"];
      set => this["permissionSet"] = (object) value;
    }

    /// <summary>
    /// security action name used to test if the page is visible
    /// </summary>
    [ConfigurationProperty("viewActionName", DefaultValue = "")]
    public new string ViewActionName
    {
      get => (string) this["viewActionName"];
      set => this["viewActionName"] = (object) value;
    }

    /// <summary>Gets the controls on the page.</summary>
    /// <value>The controls.</value>
    [ConfigurationProperty("controls")]
    public ConfigElementList<PageControlElement> Controls => (ConfigElementList<PageControlElement>) this["controls"];
  }
}
