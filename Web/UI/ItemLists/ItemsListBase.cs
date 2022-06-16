// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions.VirtualPath;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Toolbar;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.ItemLists
{
  /// <summary>
  /// Base class for all controls that list a collection of items
  /// </summary>
  [ParseChildren(true)]
  public abstract class ItemsListBase : SimpleScriptView
  {
    private Collection<ItemDescription> dataMembers;
    private Collection<ToolboxItemBase> toolboxItems;
    private Collection<IDialogDefinition> dialogDefinitions;
    private Collection<ILinkDefinition> links;
    private List<ClientCommand> commandItems = new List<ClientCommand>();
    private IList<string> blackListedWindows;
    private bool scrollOpenedDialogsToTop = true;
    private bool allowPaging = true;
    private int pageSize = 20;
    private bool allowSorting = true;
    private bool bindOnLoad = true;
    private bool showCheckRelatingData;
    private const string itemsListBaseJsPath = "Telerik.Sitefinity.Web.UI.ItemLists.Scripts.ItemsListBase.js";
    private const string clientManagerJsPath = "Telerik.Sitefinity.Web.Scripts.ClientManager.js";
    protected const string BinderCommandPrefix = "sf_binderCommand_";
    private const string DefaultRecycleBinServiceUrl = "/restapi/recycle-bin/dataItems/batch/restore";
    private Collection<IPromptDialogDefinition> promptDialogDefinitions;
    private Collection<PromptDialog> promptDialogControls;
    private string currentUICulture;

    /// <summary>Gets or sets the size of the page.</summary>
    /// <value>The size of the page.</value>
    public int PageSize
    {
      get => this.pageSize;
      set => this.pageSize = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [allow paging].
    /// </summary>
    /// <value><c>true</c> if [allow paging]; otherwise, <c>false</c>.</value>
    public bool AllowPaging
    {
      get => this.allowPaging;
      set => this.allowPaging = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether [allow sorting].
    /// </summary>
    /// <value><c>true</c> if [allow sorting]; otherwise, <c>false</c>.</value>
    public bool AllowSorting
    {
      get => this.allowSorting;
      set => this.allowSorting = value;
    }

    /// <summary>Gets or sets the service base URL.</summary>
    /// <value>The service base URL.</value>
    public string ServiceBaseUrl { get; set; }

    internal string ContentLocationPreviewUrl { get; set; }

    /// <summary>Gets or sets the manager type.</summary>
    /// <value>The manager type.</value>
    public string ManagerType { get; set; }

    /// <summary>
    /// Gets or sets the data key names. DataKey names are the names of the properties
    /// which define the data item primary key. Use comma to separate keys, if data item
    /// is defined by more than one key.
    /// </summary>
    /// <remarks>
    /// Specifying data key names is obligatory for the automatic create, update and delete
    /// functions of the client binders.
    /// </remarks>
    public string DataKeyNames { get; set; }

    /// <summary>
    /// Gets or sets the name of the property that holds the original data item property.
    /// Use this if the actual data item has been wrapped in another type. If omitted, the
    /// data item itself will be used.
    /// </summary>
    public string DataItemProperty { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// Whether the grid should rebind when after successful commands
    /// </summary>
    public bool BindOnSuccess { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if the list will be bind on load.
    /// </summary>
    /// <value><c>true</c> if the list will be bind on load; otherwise, <c>false</c>.</value>
    public bool BindOnLoad
    {
      get => this.bindOnLoad;
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets the links.</summary>
    /// <value>The links.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<ILinkDefinition> Links
    {
      get
      {
        if (this.links == null)
          this.links = new Collection<ILinkDefinition>();
        return this.links;
      }
    }

    /// <summary>Gets or sets the comma delimited list of items</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<ItemDescription> Items
    {
      get
      {
        if (this.dataMembers == null)
          this.dataMembers = new Collection<ItemDescription>();
        return this.dataMembers;
      }
    }

    /// <summary>
    /// Gets or sets the comma delimited list of dialogs that can be opened
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<IDialogDefinition> Dialogs
    {
      get
      {
        if (this.dialogDefinitions == null)
          this.dialogDefinitions = new Collection<IDialogDefinition>();
        return this.dialogDefinitions;
      }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<IPromptDialogDefinition> PromptDialogs
    {
      get
      {
        if (this.promptDialogDefinitions == null)
          this.promptDialogDefinitions = new Collection<IPromptDialogDefinition>();
        return this.promptDialogDefinitions;
      }
    }

    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<PromptDialog> PromptDialogControls
    {
      get
      {
        if (this.promptDialogControls == null)
          this.promptDialogControls = new Collection<PromptDialog>();
        return this.promptDialogControls;
      }
    }

    /// <summary>
    /// Gets or sets the comma delimited list of toolbox items
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Collection<ToolboxItemBase> ToolboxItems
    {
      get
      {
        if (this.toolboxItems == null)
          this.toolboxItems = new Collection<ToolboxItemBase>();
        return this.toolboxItems;
      }
    }

    /// <summary>
    /// Name of the HTML tag to use when wrapping a command item in toolboxes
    /// </summary>
    public string CommandTagWrapperName { get; set; }

    /// <summary>
    /// Message to be shown when prompting the user if they are sure they want to delete single item
    /// </summary>
    public string DeleteSingleConfirmationMessage { get; set; }

    /// <summary>
    /// Message to be shown when prompting the user if they are sure they want to delete multiple items
    /// </summary>
    public string DeleteMultipleConfirmationMessage { get; set; }

    /// <summary>
    /// The message for the check relating data option to be shown when prompting the user if they are sure they want to delete single item
    /// </summary>
    public string CheckRelatingDataMessageSingle { get; set; }

    /// <summary>
    /// The message for the check relating data option to be shown when prompting the user if they are sure they want to delete multiple items
    /// </summary>
    public string CheckRelatingDataMessageMultiple { get; set; }

    /// <summary>
    /// Gets or sets whether the Recycle Bin module is enabled.
    /// </summary>
    public bool RecycleBinEnabled { get; set; }

    /// <summary>
    /// Gets or sets the message to be shown when users confirms if they want to send single item to Recycle Bin
    /// </summary>
    public string SendToRecycleBinSingleConfirmationMessage { get; set; }

    /// <summary>
    /// Gets or sets the message to be shown when users confirms if they want to send multiple items to Recycle Bin
    /// </summary>
    public string SendToRecycleBinMultipleConfirmationMessage { get; set; }

    /// <summary>
    /// Specifies the default sorting expression of the binder
    /// </summary>
    public string DefaultSortExpression { get; set; }

    /// <summary>
    /// Specifies a filter that will be always applied to the service
    /// </summary>
    public string ConstantFilter { get; set; }

    /// <summary>
    /// Name of the wrapping tag, or empty/null if you don't want a wrapper tag
    /// </summary>
    public string WrapperTagName { get; set; }

    /// <summary>
    /// If the wrapper tag has a name - this will manipulate the wrapper tag's css class
    /// </summary>
    public string WrapperTagCssClass { get; set; }

    /// <summary>
    /// If the wrapper tag has a name - this will manipulate the wrapper tag's server-side pageId
    /// </summary>
    public string WrapperTagClientId { get; set; }

    /// <summary>
    /// If specified, this will give a custom name to the item that is listed by this items list
    /// </summary>
    /// <remarks>
    /// When used in combination with a dialog description's form item name, and the opened dialog
    /// uses DynamicFieldSet, this will determine wheter to data bind the DynamicFieldSet or not.
    /// It will be bound if the dialog's form item name and this property's value are the same.
    /// This applies only when both properties are set.
    /// </remarks>
    public string FormItemName { get; set; }

    /// <summary>
    /// Indicates whether dialogs opened via ItemsListBase should be automatically scrolled to the top
    /// </summary>
    /// <value>True to scroll to top, false otherwize. Default value is true.</value>
    public bool ScrollOpenedDialogsToTop
    {
      get => this.scrollOpenedDialogsToTop;
      set => this.scrollOpenedDialogsToTop = value;
    }

    /// <summary>
    /// Gets or sets the ID of the RadAjaxLoadingPanel control that will be displayed during async calls and binding operations.
    /// </summary>
    public string LoadingPanelID { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow multiple selection.
    /// </summary>
    public virtual bool AllowMultipleSelection { get; set; }

    /// <summary>Gets or sets the culture used by the client manager.</summary>
    public string Culture { get; private set; }

    /// <summary>
    /// Gets or sets the UI culture used by the client manager.
    /// </summary>
    public string UICulture
    {
      get
      {
        if (this.currentUICulture.IsNullOrEmpty())
          this.currentUICulture = !SystemManager.CurrentContext.AppSettings.Multilingual ? (string) null : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
        return this.currentUICulture;
      }
      set => this.currentUICulture = value;
    }

    public bool ShowCheckRelatingData
    {
      get => this.showCheckRelatingData;
      set => this.showCheckRelatingData = value;
    }

    /// <summary>Gets or sets the type of the content items.</summary>
    /// <value>The type of the content items.</value>
    internal Type ContentType { get; set; }

    /// <summary>
    /// Gets or sets the type of the recyclable content item. It is used when the item is restored from the Recycle Bin.
    /// </summary>
    /// <value>The type of the recyclable content item.</value>
    internal Type RecyclableContentItemType { get; set; }

    /// <summary>Gets the black listed windows.</summary>
    internal IList<string> BlackListedWindows
    {
      get
      {
        if (this.blackListedWindows == null)
          this.blackListedWindows = (IList<string>) new List<string>();
        return this.blackListedWindows;
      }
    }

    /// <summary>
    /// Specified whether the current instance supports multilingual
    /// </summary>
    internal bool? SupportsMultilingual { get; set; }

    /// <summary>Gets or sets the recycle bin service URL.</summary>
    /// <value>The recycle bin service URL.</value>
    public string RecycleBinServiceUrl { get; set; }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.Binder.DataKeyNames = this.DataKeyNames;
      this.Binder.DataType = this.ContentType;
      this.Binder.Provider = this.ProviderName;
      if (!string.IsNullOrEmpty(this.DefaultSortExpression))
        this.Binder.DefaultSortExpression = this.DefaultSortExpression;
      if (!string.IsNullOrEmpty(this.LoadingPanelID))
        this.Binder.LoadingPanelID = this.LoadingPanelID;
      this.ConstructList();
      foreach (IPromptDialogDefinition promptDialog in this.PromptDialogs)
      {
        PromptDialog child = PromptDialog.FromDefinition(promptDialog);
        container.Controls.Add((Control) child);
        this.PromptDialogControls.Add(child);
      }
      PromptDialog awareDeleteDialog = ItemsListBase.GetLanguageAwareDeleteDialog(Res.Get<Labels>().WhatDoYouWantToDelete, this.ShowCheckRelatingData, this.RecycleBinEnabled);
      container.Controls.Add((Control) awareDeleteDialog);
      this.PromptDialogControls.Add(awareDeleteDialog);
      PromptDialog standartDeleteDialog = ItemsListBase.GetStandartDeleteDialog(this.DeleteSingleConfirmationMessage, this.ShowCheckRelatingData, this.RecycleBinEnabled);
      container.Controls.Add((Control) standartDeleteDialog);
      this.PromptDialogControls.Add(standartDeleteDialog);
      PromptDialog restrictionDeleteDialog = ItemsListBase.GetPermissionRestrictionDeleteDialog(Res.Get<Labels>().YouDoNotHaveThePermissionsToDeleteThisItem);
      container.Controls.Add((Control) restrictionDeleteDialog);
      this.PromptDialogControls.Add(restrictionDeleteDialog);
      this.RecycleBinServiceUrl = "/restapi/recycle-bin/dataItems/batch/restore";
    }

    public static PromptDialog GetStandartDeleteDialog(
      string confirmationMessage,
      bool showCheckRelatingData = false,
      bool recycleBinEnabled = false)
    {
      PromptDialog standartDeleteDialog = new PromptDialog();
      standartDeleteDialog.Message = confirmationMessage;
      standartDeleteDialog.DialogName = "confirmDeleteSingle";
      standartDeleteDialog.ShowCheckRelatingData = showCheckRelatingData;
      Collection<CommandToolboxItem> commands1 = standartDeleteDialog.Commands;
      CommandToolboxItem commandToolboxItem1 = new CommandToolboxItem();
      commandToolboxItem1.CommandName = "language";
      commandToolboxItem1.CssClass = "sfDelete";
      commandToolboxItem1.Text = ItemsListBase.GetDeleteSingleItemCommandMessage(recycleBinEnabled);
      commandToolboxItem1.CommandType = CommandType.NormalButton;
      commands1.Add(commandToolboxItem1);
      Collection<CommandToolboxItem> commands2 = standartDeleteDialog.Commands;
      CommandToolboxItem commandToolboxItem2 = new CommandToolboxItem();
      commandToolboxItem2.CommandName = "all";
      commandToolboxItem2.CssClass = "sfDelete";
      commandToolboxItem2.Text = ItemsListBase.GetDeleteMultipleItemsCommandMessage(recycleBinEnabled);
      commandToolboxItem2.CommandType = CommandType.NormalButton;
      commands2.Add(commandToolboxItem2);
      standartDeleteDialog.Commands.Add(new CommandToolboxItem()
      {
        CommandName = "cancel",
        Text = Res.Get<Labels>().Cancel,
        CommandType = CommandType.CancelButton
      });
      return standartDeleteDialog;
    }

    public static PromptDialog GetLanguageAwareDeleteDialog(
      string confirmationMessage,
      bool showCheckRelatingData = false,
      bool recycleBinEnabled = false)
    {
      PromptDialog awareDeleteDialog = new PromptDialog();
      awareDeleteDialog.Message = confirmationMessage;
      awareDeleteDialog.DialogName = "confirmDelete";
      awareDeleteDialog.Width = 600;
      awareDeleteDialog.ShowCheckRelatingData = showCheckRelatingData;
      Collection<CommandToolboxItem> commands = awareDeleteDialog.Commands;
      CommandToolboxItem commandToolboxItem = new CommandToolboxItem();
      commandToolboxItem.CommandName = "all";
      commandToolboxItem.CssClass = "sfDelete";
      commandToolboxItem.Text = ItemsListBase.GetDeleteAllTranslationsCommandMessage(recycleBinEnabled);
      commandToolboxItem.CommandType = CommandType.NormalButton;
      commands.Add(commandToolboxItem);
      awareDeleteDialog.Commands.Add(new CommandToolboxItem()
      {
        CommandName = "language",
        Text = ItemsListBase.GetDeleteOnlyTranslationsCommandMessage(recycleBinEnabled),
        CommandType = CommandType.NormalButton
      });
      awareDeleteDialog.Commands.Add(new CommandToolboxItem()
      {
        CommandName = "cancel",
        Text = Res.Get<Labels>().Cancel,
        CommandType = CommandType.CancelButton
      });
      return awareDeleteDialog;
    }

    public static PromptDialog GetPermissionRestrictionDeleteDialog(string message)
    {
      PromptDialog restrictionDeleteDialog = new PromptDialog();
      restrictionDeleteDialog.Message = message;
      restrictionDeleteDialog.DialogName = "permissionRestriction";
      Collection<CommandToolboxItem> commands = restrictionDeleteDialog.Commands;
      CommandToolboxItem commandToolboxItem = new CommandToolboxItem();
      commandToolboxItem.CommandName = "ok";
      commandToolboxItem.Text = Res.Get<Labels>().Ok;
      commandToolboxItem.CommandType = CommandType.NormalButton;
      commandToolboxItem.CssClass = "sfSave";
      commands.Add(commandToolboxItem);
      return restrictionDeleteDialog;
    }

    public static PromptDialog GetPublishUnpublishAwareDialog(string message) => new PromptDialog()
    {
      Message = message,
      DialogName = "publishUnpublishAwareDialog",
      Commands = {
        new CommandToolboxItem()
        {
          CommandName = "ok",
          Text = Res.Get<Labels>().Ok,
          CommandType = CommandType.NormalButton
        },
        new CommandToolboxItem()
        {
          CommandName = "cancel",
          Text = Res.Get<Labels>().Cancel,
          CommandType = CommandType.CancelButton
        }
      }
    };

    /// <summary>Call this to construct the items list</summary>
    protected virtual void ConstructList()
    {
      this.ConstructToolbox();
      for (int index = 0; index < this.Items.Count; ++index)
        this.ConstructBinderContainer(this.Items[index], index);
      foreach (IDialogDefinition dialog in this.Dialogs)
        this.ConstructDialog(dialog);
    }

    protected virtual BinderContainer CreateBinderContainer(
      ItemDescription item,
      int index)
    {
      BinderContainer binderContainer = new BinderContainer();
      if (!string.IsNullOrEmpty(item.Markup))
      {
        if (item.Markup.StartsWith("~/"))
        {
          using (Stream stream = SitefinityFile.Open(item.Markup))
          {
            using (StreamReader streamReader = new StreamReader(stream))
            {
              string end = streamReader.ReadToEnd();
              binderContainer.Markup = end;
            }
          }
        }
        else
          binderContainer.Markup = item.Markup;
      }
      else
        binderContainer.Markup = "{{" + item.Name + "}}";
      return binderContainer;
    }

    protected void ConstructToolbox()
    {
      foreach (ToolboxItemBase toolboxItem in this.ToolboxItems)
      {
        Control control = this.FindControlRecursive((Control) this.Page, toolboxItem.ContainerId);
        if (!string.IsNullOrEmpty(toolboxItem.WrapperTagName))
        {
          HtmlGenericControl child = new HtmlGenericControl(toolboxItem.WrapperTagName);
          if (!string.IsNullOrEmpty(toolboxItem.WrapperTagCssClass))
            child.Attributes.Add("class", toolboxItem.WrapperTagCssClass);
          if (!string.IsNullOrEmpty(toolboxItem.WrapperTagId))
            child.ID = toolboxItem.WrapperTagId;
          control.Controls.Add((Control) child);
          control = (Control) child;
        }
        ClientCommand clientCommand1;
        switch (toolboxItem)
        {
          case ICommandButton commandButton:
            control.Controls.Add(commandButton.GenerateCommandItem());
            List<ClientCommand> commandItems1 = this.commandItems;
            clientCommand1 = new ClientCommand();
            clientCommand1.ButtonId = commandButton.ButtonClientId;
            clientCommand1.ButtonName = commandButton.GetType().Name;
            clientCommand1.CommandName = commandButton.CommandName;
            clientCommand1.IsRadMenu = commandButton is MenuToolboxItem;
            ClientCommand clientCommand2 = clientCommand1;
            commandItems1.Add(clientCommand2);
            this.OnItemAddedToToolbox(new ItemAddedToToolboxEventArgs(toolboxItem));
            continue;
          case LiteralToolboxItem _:
            control.Controls.Add(((LiteralToolboxItem) toolboxItem).GenerateItem());
            this.OnItemAddedToToolbox(new ItemAddedToToolboxEventArgs(toolboxItem));
            continue;
          case SearchPanelToolboxItem _:
            control.Controls.Add(((SearchPanelToolboxItem) toolboxItem).GenerateItem());
            ((SearchPanelToolboxItem) toolboxItem).BinderClientID = this.Binder.ClientID;
            this.OnItemAddedToToolbox(new ItemAddedToToolboxEventArgs(toolboxItem));
            continue;
          case DropDownToolboxItem _:
            DropDownToolboxItem dropDownToolboxItem = toolboxItem as DropDownToolboxItem;
            control.Controls.Add(dropDownToolboxItem.GenerateItem());
            List<ClientCommand> commandItems2 = this.commandItems;
            clientCommand1 = new ClientCommand();
            clientCommand1.ButtonId = dropDownToolboxItem.DropDownClientId;
            clientCommand1.ButtonName = dropDownToolboxItem.ItemType;
            clientCommand1.CommandName = dropDownToolboxItem.CommandName;
            clientCommand1.IsRadMenu = false;
            ClientCommand clientCommand3 = clientCommand1;
            commandItems2.Add(clientCommand3);
            this.OnItemAddedToToolbox(new ItemAddedToToolboxEventArgs(toolboxItem));
            continue;
          default:
            continue;
        }
      }
    }

    protected void ConstructBinderContainer(ItemDescription item, int index)
    {
      this.Binder.Containers.Add(this.CreateBinderContainer(item, index));
      this.OnItemAddedToClientBinder(new ItemAddedToClientBinderEventArgs(item, index));
    }

    protected void ConstructDialog(IDialogDefinition dialog)
    {
      Telerik.Web.UI.RadWindow control = new Telerik.Web.UI.RadWindow();
      control.ID = dialog.OpenOnCommandName;
      control.Behaviors = dialog.Behaviors;
      control.InitialBehaviors = dialog.InitialBehaviors;
      control.AutoSizeBehaviors = dialog.AutoSizeBehaviors;
      control.Width = dialog.Width;
      control.Height = dialog.Height;
      control.VisibleTitlebar = dialog.VisibleTitleBar;
      control.VisibleStatusbar = dialog.VisibleStatusBar;
      control.NavigateUrl = dialog.NavigateUrl;
      control.Modal = dialog.IsModal;
      Telerik.Web.UI.RadWindow radWindow1 = control;
      bool? nullable;
      int num1;
      if (!dialog.ReloadOnShow.HasValue)
      {
        num1 = 0;
      }
      else
      {
        nullable = dialog.ReloadOnShow;
        num1 = nullable.Value ? 1 : 0;
      }
      radWindow1.ReloadOnShow = num1 != 0;
      Telerik.Web.UI.RadWindow radWindow2 = control;
      nullable = dialog.DestroyOnClose;
      int num2;
      if (!nullable.HasValue)
      {
        num2 = 0;
      }
      else
      {
        nullable = dialog.DestroyOnClose;
        num2 = nullable.Value ? 1 : 0;
      }
      radWindow2.DestroyOnClose = num2 != 0;
      if (!string.IsNullOrEmpty(dialog.Skin))
        control.Skin = dialog.Skin;
      if (!string.IsNullOrEmpty(dialog.CssClass))
        control.CssClass = dialog.CssClass;
      nullable = dialog.IsBlackListed;
      if (nullable.HasValue)
      {
        nullable = dialog.IsBlackListed;
        if (nullable.Value)
          this.BlackListedWindows.Add(dialog.OpenOnCommandName);
      }
      this.WindowManager.Windows.Add(control);
    }

    private Control FindControlRecursive(Control root, string id)
    {
      if (root.ID == id)
        return root;
      foreach (Control control in root.Controls)
      {
        Control controlRecursive = this.FindControlRecursive(control, id);
        if (controlRecursive != null)
          return controlRecursive;
      }
      return (Control) null;
    }

    /// <summary>Fired while constructing the toolboxes</summary>
    public event EventHandler<ItemAddedToToolboxEventArgs> ItemAddedToToolbox;

    /// <summary>Fired while constructing the client binder</summary>
    public event EventHandler<ItemAddedToClientBinderEventArgs> ItemAddedToClientBinder;

    /// <summary>
    /// Determines wheter ItemsListBase should override Render and manually insert
    /// a wrapper tag that uses <see cref="P:Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase.WrapperTagName" />, <see cref="P:Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase.WrapperTagClientId" />
    /// and <see cref="P:Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase.WrapperTagCssClass" />
    /// </summary>
    protected virtual bool AutoInsertWrapperTag => true;

    /// <summary>Fired when the client command event was fired</summary>
    public string OnClientCommand { get; set; }

    /// <summary>Fired when a client item command is raised</summary>
    public string OnClientItemCommand { get; set; }

    /// <summary>
    /// Fired after the items list was data bound on the client
    /// </summary>
    public string OnClientDataBound { get; set; }

    /// <summary>Fires to approve data binding on the client</summary>
    public string OnClientDataBinding { get; set; }

    /// <summary>
    /// Fires when dialog opened by items list has been closed.
    /// </summary>
    public string OnClientDialogClosed { get; set; }

    /// <summary>
    /// Happens when a dialog should be opened (before opening). If cancelled, the dialog won't be opened.
    /// </summary>
    public string OnClientDialogOpened { get; set; }

    /// <summary>
    /// Happens when the dialog needs to be showed, just before the createDialog callback. If cancelled,
    /// the callback won't be called, but the dialog will still be shown.
    /// </summary>
    public string OnClientDialogShowed { get; set; }

    /// <summary>
    /// Happens when a link is clicked, just before the navigation. If cancelled, won't navigate.
    /// </summary>
    public string OnClientLinkActivated { get; set; }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      string serviceBaseUrl = this.ServiceBaseUrl;
      string[] source = this.ServiceBaseUrl.Split('?');
      string str1 = VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute(source[0]));
      if (((IEnumerable<string>) source).Count<string>() > 1)
        str1 = str1 + "?" + source[1];
      if (!string.IsNullOrEmpty(this.DataItemProperty))
        controlDescriptor.AddProperty("_dataItemProperty", (object) this.DataItemProperty);
      if (!string.IsNullOrEmpty(this.ServiceBaseUrl))
        controlDescriptor.AddProperty("serviceBaseUrl", (object) str1);
      if (!string.IsNullOrEmpty(this.DataKeyNames))
        controlDescriptor.AddProperty("keys", (object) scriptSerializer.Serialize((object) this.DataKeyNames.Split(',')));
      controlDescriptor.AddProperty("contentLocationPreviewUrl", (object) this.ContentLocationPreviewUrl);
      controlDescriptor.AddProperty("providerName", (object) this.ProviderName);
      controlDescriptor.AddProperty("bindOnSuccess", (object) this.BindOnSuccess);
      controlDescriptor.AddProperty("radWindowManagerId", (object) this.WindowManager.ClientID);
      controlDescriptor.AddProperty("binderId", (object) this.Binder.ClientID);
      controlDescriptor.AddProperty("constantFilter", (object) this.ConstantFilter);
      controlDescriptor.AddProperty("formItemName", (object) this.FormItemName);
      controlDescriptor.AddProperty("_scrollOpenedDialogsToTop", (object) this.ScrollOpenedDialogsToTop);
      controlDescriptor.AddProperty("_deleteMultipleConfirmationMessage", (object) this.DeleteMultipleConfirmationMessage);
      controlDescriptor.AddProperty("_deleteSingleConfirmationMessage", (object) this.DeleteSingleConfirmationMessage);
      controlDescriptor.AddProperty("_checkRelatingDataMessageSingle", (object) this.CheckRelatingDataMessageSingle);
      controlDescriptor.AddProperty("_checkRelatingDataMessageMultiple", (object) this.CheckRelatingDataMessageMultiple);
      controlDescriptor.AddProperty("_recycleBinEnabled", (object) this.RecycleBinEnabled);
      controlDescriptor.AddProperty("_sendToRecycleBinSingleConfirmationMessage", (object) this.SendToRecycleBinSingleConfirmationMessage);
      controlDescriptor.AddProperty("_sendToRecycleBinMultipleConfirmationMessage", (object) this.SendToRecycleBinMultipleConfirmationMessage);
      controlDescriptor.AddProperty("_bindOnLoad", (object) this.BindOnLoad);
      controlDescriptor.AddProperty("culture", (object) this.Culture);
      controlDescriptor.AddProperty("uiCulture", (object) this.UICulture);
      controlDescriptor.AddProperty("managerType", (object) this.ManagerType);
      controlDescriptor.AddProperty("recycleBinServiceUrl", (object) this.RecycleBinServiceUrl);
      controlDescriptor.AddProperty("recyclableContentItemType", this.RecyclableContentItemType == (Type) null ? (object) (string) null : (object) this.RecyclableContentItemType.FullName);
      if (!string.IsNullOrEmpty(this.OnClientCommand))
        controlDescriptor.AddEvent("command", this.OnClientCommand);
      if (!string.IsNullOrEmpty(this.OnClientItemCommand))
        controlDescriptor.AddEvent("itemCommand", this.OnClientItemCommand);
      if (!string.IsNullOrEmpty(this.OnClientDataBound))
        controlDescriptor.AddEvent("dataBound", this.OnClientDataBound);
      if (!string.IsNullOrEmpty(this.OnClientDataBinding))
        controlDescriptor.AddEvent("dataBinding", this.OnClientDataBinding);
      if (!string.IsNullOrEmpty(this.OnClientDialogClosed))
        controlDescriptor.AddEvent("dialogClosed", this.OnClientDialogClosed);
      if (!string.IsNullOrEmpty(this.OnClientDialogOpened))
        controlDescriptor.AddEvent("dialogOpened", this.OnClientDialogOpened);
      if (!string.IsNullOrEmpty(this.OnClientDialogShowed))
        controlDescriptor.AddEvent("dialogShowed", this.OnClientDialogShowed);
      if (!string.IsNullOrEmpty(this.OnClientLinkActivated))
        controlDescriptor.AddEvent("linkActivated", this.OnClientLinkActivated);
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      foreach (ILinkDefinition link in this.Links)
        dictionary1[link.CommandName] = link.NavigateUrl;
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      foreach (IDialogDefinition dialog in this.Dialogs)
        dictionary2[dialog.OpenOnCommandName] = dialog.Parameters;
      string str2 = scriptSerializer.Serialize((object) dictionary1);
      string str3 = scriptSerializer.Serialize((object) dictionary2);
      string str4 = scriptSerializer.Serialize((object) this.commandItems);
      controlDescriptor.AddProperty("linkDescriptions", (object) str2);
      controlDescriptor.AddProperty("dialogParameters", (object) str3);
      controlDescriptor.AddProperty("_commandItems", (object) str4);
      if (this.SupportsMultilingual.HasValue)
        controlDescriptor.AddProperty("_supportsMultilingual", (object) this.SupportsMultilingual);
      else
        controlDescriptor.AddProperty("_supportsMultilingual", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
      Dictionary<string, string> dictionary3 = new Dictionary<string, string>();
      Dictionary<string, string> dictionary4 = new Dictionary<string, string>();
      foreach (PromptDialog promptDialogControl in this.PromptDialogControls)
      {
        dictionary3[promptDialogControl.DialogName] = promptDialogControl.ClientID;
        if (!string.IsNullOrEmpty(promptDialogControl.OpenOnCommand))
          dictionary4[promptDialogControl.OpenOnCommand] = promptDialogControl.DialogName;
      }
      string str5 = scriptSerializer.Serialize((object) dictionary3);
      string str6 = scriptSerializer.Serialize((object) dictionary4);
      controlDescriptor.AddProperty("_promptDialogNamesJson", (object) str5);
      controlDescriptor.AddProperty("_promptDialogCommandsJson", (object) str6);
      controlDescriptor.AddProperty("_backToLabel", (object) Res.Get<Labels>().BackTo);
      controlDescriptor.AddProperty("_youDoNotHaveThePermissionsToDeleteThisItem", (object) Res.Get<Labels>().YouDoNotHaveThePermissionsToDeleteThisItem);
      controlDescriptor.AddProperty("_youDoNotHaveThePermissionsToDeleteTheseItems", (object) Res.Get<Labels>().YouDoNotHaveThePermissionsToDeleteTheseItems);
      controlDescriptor.AddProperty("_youDoNotHaveThePermissionsToDeleteSomeOfTheItems", (object) Res.Get<Labels>().YouDoNotHaveThePermissionsToDeleteSomeOfTheItems);
      controlDescriptor.AddProperty("_blackListedWindows", (object) scriptSerializer.Serialize((object) this.BlackListedWindows));
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null)
      {
        ISiteContext currentSiteContext = multisiteContext.CurrentSiteContext;
        controlDescriptor.AddProperty("_currentSiteId", (object) currentSiteContext.Site.Id.ToString());
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ItemsListBase).Assembly.GetName().FullName;
      List<ScriptReference> scriptReferenceList = new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ItemLists.Scripts.ItemsListBase.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName)
      };
      ScriptReference scriptReference = PageManager.GetScriptReferences(ScriptRef.DialogManager).SingleOrDefault<ScriptReference>();
      if (scriptReference != null)
        scriptReferenceList.Add(scriptReference);
      return (IEnumerable<ScriptReference>) scriptReferenceList.ToArray();
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      int num = !this.AutoInsertWrapperTag ? 0 : (!string.IsNullOrEmpty(this.WrapperTagName) ? 1 : 0);
      if (num != 0)
      {
        if (!string.IsNullOrEmpty(this.WrapperTagCssClass))
          writer.AddAttribute(HtmlTextWriterAttribute.Class, this.WrapperTagCssClass);
        if (!string.IsNullOrEmpty(this.WrapperTagClientId))
          writer.AddAttribute(HtmlTextWriterAttribute.Id, this.WrapperTagClientId);
        writer.RenderBeginTag(this.WrapperTagName);
      }
      base.Render(writer);
      if (num == 0)
        return;
      writer.RenderEndTag();
    }

    /// <summary>Fires the ItemAddedToToolbox event</summary>
    /// <param name="args">Event data</param>
    protected void OnItemAddedToToolbox(ItemAddedToToolboxEventArgs args)
    {
      if (this.ItemAddedToToolbox == null)
        return;
      this.ItemAddedToToolbox((object) this, args);
    }

    /// <summary>Fires the ItemAddedToClientBinder event.</summary>
    /// <param name="args">Event data.</param>
    protected void OnItemAddedToClientBinder(ItemAddedToClientBinderEventArgs args)
    {
      if (this.ItemAddedToClientBinder == null)
        return;
      this.ItemAddedToClientBinder((object) this, args);
    }

    /// <summary>Reference to the client binder</summary>
    public virtual ClientBinder Binder => this.Container.GetControl<ClientBinder>();

    /// <summary>
    /// Reference to the RadWindowManager used to create and open dialogs
    /// </summary>
    protected virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    private static string GetDeleteSingleItemCommandMessage(bool recycleBinEnabled)
    {
      Labels labels = Res.Get<Labels>();
      return recycleBinEnabled ? labels.YesMoveToRecycleBin : labels.YesDelete;
    }

    private static string GetDeleteMultipleItemsCommandMessage(bool recycleBinEnabled)
    {
      Labels labels = Res.Get<Labels>();
      return recycleBinEnabled ? labels.YesMoveToRecycleBin : labels.YesDeleteTheseItems;
    }

    private static string GetDeleteAllTranslationsCommandMessage(bool recycleBinEnabled)
    {
      Labels labels = Res.Get<Labels>();
      return recycleBinEnabled ? labels.MoveAllTranslations : labels.DeleteAllTranslations;
    }

    private static string GetDeleteOnlyTranslationsCommandMessage(bool recycleBinEnabled)
    {
      Labels labels = Res.Get<Labels>();
      return recycleBinEnabled ? labels.DeletePermanentlyTranslation : labels.DeleteOnlyTranslation;
    }
  }
}
