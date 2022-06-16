// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Contracts
{
  /// <summary>
  /// An interface that provides all common properties to construct the actual view.
  /// </summary>
  public interface IContentViewDefinition : IDefinition
  {
    string ControlDefinitionName { get; set; }

    /// <summary>Gets or sets the description of the view.</summary>
    /// <value>The description of the view.</value>
    string Description { get; set; }

    /// <summary>
    /// Determines the display mode in which the FieldControls of the view ought to be rendered.
    /// See enumeration for possible values.
    /// </summary>
    /// <value>The display mode of the FieldControls.</value>
    FieldDisplayMode DisplayMode { get; set; }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewPlugInDefinition" /> objects.
    /// </summary>
    /// <value>A list of <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Contracts.IContentViewPlugInDefinition" /> objects</value>
    IEnumerable<IContentViewPlugInDefinition> PlugIns { get; }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The resource class pageId.</value>
    string ResourceClassId { get; set; }

    /// <summary>Gets or sets the name of the view template.</summary>
    /// <value>The name of the view template.</value>
    string TemplateName { get; set; }

    /// <summary>Gets or sets the path of the view template.</summary>
    /// <value>The path of the view template.</value>
    string TemplatePath { get; set; }

    /// <summary>Gets or sets the template key.</summary>
    /// <value>The template key.</value>
    string TemplateKey { get; set; }

    /// <summary>Gets or sets the title of the view.</summary>
    /// <value>The title of the view.</value>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the format for the Title generated from the parent content item name.
    /// </summary>
    /// <value>The parent title format.</value>
    /// <remarks>
    /// This format will be used if the Content type has a parent.
    /// Set it to null if you want the view title to be configured from the definition's Title property.
    /// </remarks>
    string ParentTitleFormat { get; set; }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    string ViewName { get; set; }

    /// <summary>Gets or sets the type of the view.</summary>
    /// <value>The type of the view.</value>
    Type ViewType { get; set; }

    /// <summary>
    /// Gets or sets the virtual path of the view if the view is implemented
    /// as a user control.
    /// </summary>
    string ViewVirtualPath { get; set; }

    /// <summary>
    /// Gets or sets the value that indicates if this view is a master view. If true view is
    /// a master view; otherwise it is a detail view.
    /// </summary>
    bool IsMasterView { get; }

    /// <summary>
    /// Gets or sets value determining weather view ought to use workflow.
    /// </summary>
    /// <value>True if view ought to use workflow; otherwise false.</value>
    bool? UseWorkflow { get; set; }

    /// <summary>
    /// Gets or sets a dictionary of external scripts to use with the MasterView and DetailFormView. The key of each
    /// element is the virtual path to the external script, while the value is the name of a method in
    /// that external script that will handle the ViewLoaded event.
    /// </summary>
    /// <value>The dictionary of external client scripts.</value>
    Dictionary<string, string> ExternalClientScripts { get; set; }

    /// <summary>
    /// Gets or sets a dictionary of localization properties.
    /// Key identifies the localization string, and value is the current culture's translation
    /// </summary>
    /// <value>The dictionary of localization strings</value>
    Dictionary<string, string> Localization { get; set; }

    /// <summary>
    /// Read-only collection of resource strings that are going to be available on the client via an instance of ClientLabelManager
    /// </summary>
    IList<ILabelDefinition> Labels { get; }

    /// <summary>Gets or sets the comments settings definition.</summary>
    /// <value>The comments definition.</value>
    ICommentsSettingsDefinition CommentsSettingsDefinition { get; }

    /// <summary>
    /// Gets the collection of prompt dialog definitions that are used on the view.
    /// </summary>
    List<IPromptDialogDefinition> PromptDialogs { get; }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    string ControlId { get; set; }
  }
}
