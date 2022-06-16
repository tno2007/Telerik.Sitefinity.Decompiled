// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>
  /// Configuration element for the <see cref="!:MasterView" /> view.
  /// </summary>
  public class DetailFormViewElement : 
    ContentViewDetailElement,
    IDetailFormViewDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private List<IPromptDialogDefinition> promptDialogs;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public DetailFormViewElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new DetailFormViewDefinition((ConfigElement) this);

    /// <summary>Gets the toolbar.</summary>
    /// <value>The toolbar.</value>
    [ConfigurationProperty("toolbar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToolbarDescription", Title = "ToolbarCaption")]
    public WidgetBarElement Toolbar => (WidgetBarElement) this["toolbar"];

    IWidgetBarDefinition IDetailFormViewDefinition.Toolbar => (IWidgetBarDefinition) this.Toolbar;

    /// <summary>Gets or sets if a top toolbar should be displayed</summary>
    /// <value>The show top toolbar.</value>
    [ConfigurationProperty("showTopToolbar")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowTopToolbarDescription", Title = "ShowTopToolbarCaption")]
    public bool? ShowTopToolbar
    {
      get => this.GetShowTopToolbar((bool?) this["showTopToolbar"]);
      set => this["showTopToolbar"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the base url of the REST web service(working with json) that will be used for the CRUD operations of the detail item that the view will show/modify.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("webServiceBaseUrl", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceBaseUrlDescription", Title = "WebServiceBaseUrlCaption")]
    public string WebServiceBaseUrl
    {
      get => (string) this["webServiceBaseUrl"];
      set => this["webServiceBaseUrl"] = (object) value;
    }

    /// <summary>
    /// A value indicating whether the view will show the previous/next navigation buttons.
    /// </summary>
    [ConfigurationProperty("showNavigation", DefaultValue = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowNavigationDescription", Title = "ShowNavigationCaption")]
    public bool? ShowNavigation
    {
      get => new bool?((bool) this["showNavigation"]);
      set => this["showNavigation"] = (object) value;
    }

    /// <summary>
    /// A value indicating whether the view creates a blank item - serverside.
    /// </summary>
    [ConfigurationProperty("createBlankItem", DefaultValue = true, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CreateBlankItemDescription", Title = "CreateBlankItemCaption")]
    public bool? CreateBlankItem
    {
      get => new bool?((bool) this["createBlankItem"]);
      set => this["createBlankItem"] = (object) value;
    }

    /// <summary>
    /// Specify if the system will automatically unlock the currently edited item, when exiting the detail screen
    /// Exiting the screen is registered on Back to all items, browser back button and browse window close
    /// Unlocking is executed only if the screen was opened in Write mode
    /// The unlocking uses web service REST convention to call the WebServiceBaseUrl/temp/itemid with DELETE HTTP verb
    /// </summary>
    [ConfigurationProperty("unlockDetailItemOnExit", DefaultValue = true, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UnlockDetailItemOnExitDescription", Title = "UnlockDetailItemOnExitCaption")]
    public bool? UnlockDetailItemOnExit
    {
      get => new bool?((bool) this["unlockDetailItemOnExit"]);
      set => this["unlockDetailItemOnExit"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to render the translation view.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if is to render translation view; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("isToRenderTranslationView")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsToRenderTranslationViewDescription", Title = "IsToRenderTranslationViewCaption")]
    public bool? IsToRenderTranslationView
    {
      get => (bool?) this["isToRenderTranslationView"];
      set => this["isToRenderTranslationView"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the alternative title for the detail view.
    /// </summary>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for creating a different language version of the item.
    /// </remarks>
    /// <value>The alternative title.</value>
    [ConfigurationProperty("alternativeTitle", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AlternativeTitleDescription", Title = "AlternativeTitleCaption")]
    public string AlternativeTitle
    {
      get => (string) this["alternativeTitle"];
      set => this["alternativeTitle"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the item template string for the detail view (to be used to get the right item type, if required).
    /// </summary>
    /// <remarks>
    /// Used in the backEnd detail form view when the edit dialog is used for getting a new item.
    /// </remarks>
    /// <value>The string representing the item template.</value>
    [ConfigurationProperty("itemTemplate", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemTemplateDescription", Title = "ItemTemplateCaption")]
    public string ItemTemplate
    {
      get => (string) this["itemTemplate"];
      set => this["itemTemplate"] = (object) value;
    }

    /// <summary>
    /// If set to false, the generated JSON on the client will be the value of the Item property of an 'item context' object.
    /// If set to true, the generated JSON will be posted as is to the web service.
    /// False is required for compatilibity with ContentServiceBase. By default, it is false.
    /// </summary>
    [ConfigurationProperty("doNotUseContentItemContext", DefaultValue = false, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DoNotUseContentItemContextDescription", Title = "DoNotUseContentItemContextCaption")]
    public bool DoNotUseContentItemContext
    {
      get => (bool) this["doNotUseContentItemContext"];
      set => this["doNotUseContentItemContext"] = (object) value;
    }

    /// <summary>
    /// Gets the collection of dialog config elements that are used on the view.
    /// </summary>
    [ConfigurationProperty("PromptDialogs")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendGridPromptDialogsDescription", Title = "BackendGridPromptDialogsCaption")]
    public new ConfigElementList<PromptDialogElement> PromptDialogsConfig => (ConfigElementList<PromptDialogElement>) this["PromptDialogs"];

    /// <inheritdoc />
    [ConfigurationProperty("multilingualMode", DefaultValue = MultilingualMode.Automatic, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MultilingualModeDescription", Title = "MultilingualModeCaption")]
    public MultilingualMode MultilingualMode
    {
      get => (MultilingualMode) this["multilingualMode"];
      set => this["multilingualMode"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("additionalCreateCommands", DefaultValue = null, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AdditionalCreateCommandsDescription", Title = "AdditionalCreateCommandsCaption")]
    public string AdditionalCreateCommands
    {
      get => (string) this["additionalCreateCommands"];
      set => this["additionalCreateCommands"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a list of prompt dialogs. Initialize from PromptDialogsConfig
    /// </summary>
    /// <value>The prompt dialogs added to the instance of the control</value>
    public new List<IPromptDialogDefinition> PromptDialogs
    {
      get
      {
        if (this.promptDialogs == null)
          this.promptDialogs = this.PromptDialogsConfig.Elements.Select<PromptDialogElement, IPromptDialogDefinition>((Func<PromptDialogElement, IPromptDialogDefinition>) (p => (IPromptDialogDefinition) p.ToDefinition())).ToList<IPromptDialogDefinition>();
        return this.promptDialogs;
      }
      set => this.promptDialogs = value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string toolbar = "toolbar";
      public const string showTopToolbar = "showTopToolbar";
      public const string WebServiceBaseUrl = "webServiceBaseUrl";
      public const string showNavigation = "showNavigation";
      public const string createBlankItem = "createBlankItem";
      public const string unlockDetailItemOnExit = "unlockDetailItemOnExit";
      public const string isToRenderTranslationView = "isToRenderTranslationView";
      public const string AlternativeTitle = "alternativeTitle";
      public const string ItemTemplate = "itemTemplate";
      public const string promptDialogs = "PromptDialogs";
      public const string DoNotUseContentItemContext = "doNotUseContentItemContext";
      public const string MultilingualMode = "multilingualMode";
      public const string AdditionalCreateCommands = "additionalCreateCommands";
    }
  }
}
