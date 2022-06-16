// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IDynamicCommandWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// A contract providing all members that need to be implemented by widgets which are going to fire a command dynamically
  /// </summary>
  public interface IDynamicCommandWidgetDefinition : IWidgetDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the default size of one page of items in the list
    /// </summary>
    /// <value>The default size of one page of items in the list.</value>
    int PageSize { get; set; }

    /// <summary>
    /// Gets or sets the text for the header that appears before the combo/list of commands
    /// </summary>
    string HeaderText { get; set; }

    /// <summary>
    /// Gets or sets the css class for the control that displays the header label
    /// </summary>
    string HeaderTextCssClass { get; set; }

    /// <summary>
    /// Gets or sets the text for the link which needs to be clicked to show more items
    /// </summary>
    /// <value>The text for the link which needs to be clicked to show more items.</value>
    string MoreLinkText { get; set; }

    /// <summary>
    /// Gets or sets the css class of the link used to display more items when bound on the client
    /// </summary>
    string MoreLinkCssClass { get; set; }

    /// <summary>Gets or sets the text for the link showing less items</summary>
    string LessLinkText { get; set; }

    /// <summary>
    /// Gets or sets the css class for the link showing less items.
    /// </summary>
    string LessLinkCssClass { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    string BaseServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the URL for the service used to get child taxa
    /// </summary>
    string ChildItemsServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the URL for the service used to get predecessor taxa
    /// </summary>
    string PredecessorServiceUrl { get; set; }

    /// <summary>Gets the URL parameters.</summary>
    IDictionary<string, string> UrlParameters { get; }

    /// <summary>Gets or sets the selected item CSS class.</summary>
    /// <value>The selected item CSS class.</value>
    string SelectedItemCssClass { get; }

    /// <summary>Gets or sets the data source.</summary>
    IEnumerable<IDynamicItemDefinition> Items { get; }

    /// <summary>
    /// Gets or sets the custom commands list. This is an additional data source that enables the DynamicCommandWidget
    /// to gave some of the items in the list fire custom commands that need special handling
    /// </summary>
    IEnumerable<IDynamicItemDefinition> CustomItems { get; }

    /// <summary>
    /// Specifies how the list of commands in the dynamic command widget is rendered
    /// </summary>
    BindCommandListTo BindTo { get; set; }

    /// <summary>
    /// The item template used when binding on the client (in client and hierarchical mode)
    /// </summary>
    string ClientItemTemplate { get; set; }

    /// <summary>The content type</summary>
    Type ContentType { get; set; }

    /// <summary>The dynamic module type id</summary>
    Guid DynamicModuleTypeId { get; set; }

    /// <summary>The name of the command that is fired by the widget</summary>
    string CommandName { get; set; }

    /// <summary>Gets or sets the selected item in the widget</summary>
    string SelectedValue { get; set; }

    /// <summary>Gets or sets the sort expression.</summary>
    string SortExpression { get; set; }

    /// <summary>Gets or sets the parent key id</summary>
    string ParentDataKeyName { get; set; }

    /// <summary>
    /// Determines this command is a filter command (e.g. a filter on the sidebar)
    /// </summary>
    bool IsFilterCommand { get; set; }
  }
}
