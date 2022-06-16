// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ZoneEditorWebServiceArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Zone editor web-service object</summary>
  [DataContract]
  public class ZoneEditorWebServiceArgs
  {
    /// <summary>Gets or sets the path to a layout template.</summary>
    /// <value>The path to the template.</value>
    [DataMember]
    public string LayoutTemlpate { get; set; }

    /// <summary>Gets or sets the command name.</summary>
    /// <value>The name of the command.</value>
    [DataMember]
    public string CommandName { get; set; }

    /// <summary>
    /// Gets or sets the error message.
    /// If ErrorMessage is set, this means operation failed - and will be reverted on the client side.
    /// </summary>
    /// <value>The error message.</value>
    [DataMember]
    public string ErrorMessage { get; set; }

    /// <summary>Gets or sets the ID of the control.</summary>
    /// <value>The control pageId.</value>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the type of the control.</summary>
    /// <value>The type of the control.</value>
    [DataMember]
    public string ControlType { get; set; }

    /// <summary>Gets or sets the ID of the placeholder.</summary>
    /// <value>The placeholder pageId.</value>
    [DataMember]
    public string PlaceholderId { get; set; }

    /// <summary>Gets or sets the page pageId.</summary>
    /// <value>The page pageId.</value>
    [DataMember]
    public Guid PageId { get; set; }

    /// <summary>Gets or sets the page node Id.</summary>
    /// <value>The page node Id.</value>
    [DataMember]
    public Guid PageNodeId { get; set; }

    /// <summary>Gets or sets the sibling pageId.</summary>
    /// <value>The sibling pageId.</value>
    [DataMember]
    public Guid SiblingId { get; set; }

    /// <summary>Gets or sets the index.</summary>
    /// <value>The index.</value>
    [DataMember]
    public string Ordinal { get; set; }

    /// <summary>Gets or sets the dock ID.</summary>
    /// <value>The dock pageId.</value>
    [DataMember]
    public string DockId { get; set; }

    /// <summary>Gets or sets the HTML we want returned to the client.</summary>
    /// <value>The HTML.</value>
    [DataMember]
    public string Html { get; set; }

    /// <summary>Gets or sets the CSS links.</summary>
    /// <value>The CSS links.</value>
    [DataMember]
    public string[] CssLinkUrls { get; set; }

    /// <summary>Gets or sets the attributes.</summary>
    /// <value>The attributes.</value>
    [DataMember]
    public Dictionary<string, string> Attributes { get; set; }

    [DataMember]
    public Dictionary<string, string> Parameters { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    [DataMember]
    public string Title { get; set; }

    /// <summary>Gets or sets the description.</summary>
    /// <value>The description.</value>
    [DataMember]
    public string Description { get; set; }

    /// <summary>Gets or sets the class ID.</summary>
    /// <value>The class ID.</value>
    [DataMember]
    public string ClassId { get; set; }

    /// <summary>
    /// Gets or sets the original URL of the page calling the service.
    /// </summary>
    /// <value>The URL.</value>
    [DataMember]
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this page represents a template.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is template; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public DesignMediaType MediaType { get; set; }

    [DataMember]
    public string LayoutHtml { get; set; }

    [DataMember]
    public Dictionary<string, string> Permissions { get; set; }

    /// <summary>
    /// Title that should be appended to the title of the control
    /// </summary>
    [DataMember]
    public string CustomTitleHtml { get; set; }

    /// <summary>
    /// True if controls of same key should be reloaded (taking into consideration reloadKey attribute)
    /// </summary>
    [DataMember]
    public bool ReloadControlsWithSameKey { get; set; }

    /// <summary>
    /// Guid - ID of the ControlData representing the LayoutControl, part of which is the placeholder identified by <see cref="P:Telerik.Sitefinity.Web.UI.ZoneEditorWebServiceArgs.PlaceholderId" />
    /// </summary>
    [DataMember]
    public string LayoutControlDataID { get; set; }

    [DataMember]
    public JsonDictionary<LayoutControlDataPermissions> ModifiedLayoutPermissios { get; set; }

    [DataMember]
    public string PreviousPlaceholderID { get; set; }

    /// <summary>Gets or sets the script urls.</summary>
    /// <value>The scripts.</value>
    [DataMember]
    public string[] Scripts { get; set; }

    /// <summary>Gets or sets the name of a module.</summary>
    /// <value>The name of a module.</value>
    [DataMember]
    public string ModuleName { get; set; }
  }
}
