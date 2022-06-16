// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts.IDetailFormViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts
{
  /// <summary>
  /// Defines the members of the backend detail form view definition.
  /// </summary>
  public interface IDetailFormViewDefinition : 
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>Gets the toolbar.</summary>
    /// <value>The toolbar.</value>
    IWidgetBarDefinition Toolbar { get; }

    /// <summary>Gets or sets if a top toolbar should be displayed</summary>
    /// <value>The show top toolbar.</value>
    bool? ShowTopToolbar { get; set; }

    /// <summary>Gets or sets the base url of the web service.</summary>
    string WebServiceBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets if the view will show the previous/next navigation buttons.
    /// </summary>
    bool? ShowNavigation { get; set; }

    /// <summary>Specify if the view creates blank item -serverside.</summary>
    bool? CreateBlankItem { get; set; }

    /// <summary>
    /// Specify if the system will automatically unlock the currently edited item, when exiting the detail screen
    /// Exiting the screen is registered on Back to all items, browser back button and browse window close
    /// Unlocking is executed only if the screen was opened in Write mode
    /// The unlocking uses web service REST convention to call the WebServiceBaseUrl/temp/itemid with DELETE HTTP verb
    /// </summary>
    bool? UnlockDetailItemOnExit { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to render the translation view.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if is to render translation view; otherwise, <c>false</c>.
    /// </value>
    bool? IsToRenderTranslationView { get; set; }

    /// <summary>
    /// Gets or sets the alternative title for the detail view.
    /// </summary>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for creating a different language version of the item.
    /// </remarks>
    /// <value>The alternative title.</value>
    string AlternativeTitle { get; set; }

    /// <summary>
    /// If set to false, the generated JSON on the client will be the value of the Item property of an 'item context' object.
    /// If set to true, the generated JSON will be posted as is to the web service.
    /// False is required for compatilibity with ContentServiceBase. By default, it is false.
    /// </summary>
    bool DoNotUseContentItemContext { get; set; }

    /// <summary>
    /// Gets or sets the item template string for the detail view (to be used to get the right item type, if required).
    /// </summary>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for getting a new item.
    /// </remarks>
    /// <value>The string representing the item template.</value>
    string ItemTemplate { get; set; }

    /// <summary>
    /// Gets or sets whether a detail view should display Multilingual related data
    /// </summary>
    MultilingualMode MultilingualMode { get; set; }

    /// <summary>
    /// A comma-delimited list of custom Create commands used internally by given module.
    /// </summary>
    string AdditionalCreateCommands { get; set; }
  }
}
