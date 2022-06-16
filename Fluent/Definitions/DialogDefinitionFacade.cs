// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.DialogDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Fluent API facade that defines a definition for dailogs.
  /// </summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class DialogDefinitionFacade<TParentFacade> where TParentFacade : class
  {
    private string moduleName;
    private string definitionName;
    private Type contentType;
    private ConfigElementCollection parentElement;
    private TParentFacade parentFacade;
    private Type dialogType;
    private string openOnCommandName;
    private CommonDialog commonDialog;
    private string backText;
    private string title;
    private string itemsName;
    private string itemName;
    private string parentItemName;
    private string permissionSetName;
    private string providerName;
    private string childServiceUrl;
    private string parentServiceUrl;
    private Type parentType;
    private Type securedObjectType;
    private string parentId;
    private string editCommandName;
    private string viewName;
    private DialogElement dialog;
    private string typeName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DialogDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public DialogDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementCollection parentElement,
      TParentFacade parentFacade)
    {
      if ((object) parentFacade == null)
        throw new ArgumentNullException(nameof (parentFacade));
      if (string.IsNullOrEmpty(definitionName))
        throw new ArgumentNullException(nameof (definitionName));
      if (parentElement == null)
        throw new ArgumentNullException(nameof (parentElement));
      this.moduleName = moduleName;
      this.definitionName = definitionName;
      this.contentType = contentType;
      this.parentElement = parentElement;
      this.parentFacade = parentFacade;
      this.dialog = new DialogElement((ConfigElement) parentElement)
      {
        Behaviors = WindowBehaviors.None,
        VisibleTitleBar = false,
        VisibleStatusBar = false,
        IsModal = false,
        Skin = "Default"
      };
      this.typeName = this.contentType != (Type) null ? this.contentType.FullName : string.Empty;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DialogDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="openOnCommandName">Name of the command that opens dialog.</param>
    /// <param name="dialogType">Type of the dialog.</param>
    public DialogDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementCollection parentElement,
      TParentFacade parentFacade,
      string openOnCommandName,
      Type dialogType)
      : this(moduleName, definitionName, contentType, parentElement, parentFacade)
    {
      if (string.IsNullOrEmpty(openOnCommandName))
        throw new ArgumentNullException(nameof (openOnCommandName));
      if (dialogType == (Type) null)
        throw new ArgumentNullException(nameof (dialogType));
      this.openOnCommandName = openOnCommandName;
      this.dialogType = dialogType;
      this.dialog.Name = dialogType.Name;
      this.dialog.OpenOnCommandName = openOnCommandName;
      parentElement.Add((ConfigElement) this.dialog);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DialogDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="commonDialog">The common dialog.</param>
    /// <param name="backText">The dialog back text.</param>
    /// <param name="title">The dialog title.</param>
    public DialogDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementCollection parentElement,
      TParentFacade parentFacade,
      CommonDialog commonDialog,
      string backText,
      string title)
      : this(moduleName, definitionName, contentType, parentElement, parentFacade)
    {
      this.commonDialog = commonDialog;
      this.backText = backText;
      this.title = title;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DialogDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="commonDialog">The common dialog.</param>
    /// <param name="backText">The dialog back text.</param>
    /// <param name="title">The dialog title.</param>
    public DialogDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementCollection parentElement,
      TParentFacade parentFacade,
      CommonDialog commonDialog,
      string backText,
      string title,
      string permissionSetName,
      string openOnCommandName,
      Type securedObjectType)
      : this(moduleName, definitionName, contentType, parentElement, parentFacade, commonDialog, backText, title)
    {
      this.permissionSetName = permissionSetName;
      this.openOnCommandName = openOnCommandName;
      this.securedObjectType = securedObjectType;
      if (commonDialog != CommonDialog.Permissions)
        return;
      this.AddPermissionsDialog();
      parentElement.Add((ConfigElement) this.dialog);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DialogDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="commonDialog">The common dialog.</param>
    /// <param name="backText">The dialog back text.</param>
    /// <param name="title">The dialog title.</param>
    /// <param name="itemsName">Localizable string that represents the name of the item in plural.</param>
    public DialogDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementCollection parentElement,
      TParentFacade parentFacade,
      CommonDialog commonDialog,
      string backText,
      string title,
      string itemsName)
      : this(moduleName, definitionName, contentType, parentElement, parentFacade, commonDialog, backText, title)
    {
      this.itemsName = itemsName;
      if (commonDialog != CommonDialog.CustomFields)
        return;
      this.AddCustomFieldsDialog();
      parentElement.Add((ConfigElement) this.dialog);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DialogDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="commonDialog">The common dialog.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="itemName">Localizable string that represents the name of the item in singular</param>
    /// <param name="itemsName">Localizable string that represents the name of the item in plural.</param>
    /// <param name="parentItemName">Localizable string that represents the parent name of the item in singular.</param>
    /// <param name="parentType">Type of the parent. When passing it as a parameter of Permissions dialog, this means we need to display the parent permissions.</param>
    /// <param name="childServiceUrl">The child service URL.</param>
    /// <param name="parentServiceUrl">The parent service URL.</param>
    /// <param name="openOnCommandName">Name of the command that opens the dialog.</param>
    public DialogDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementCollection parentElement,
      TParentFacade parentFacade,
      CommonDialog commonDialog,
      string providerName,
      string itemName,
      string itemsName,
      string parentItemName,
      Type parentType,
      string childServiceUrl,
      string parentServiceUrl,
      string openOnCommandName)
      : this(moduleName, definitionName, contentType, parentElement, parentFacade)
    {
      this.commonDialog = commonDialog;
      this.providerName = providerName;
      this.itemName = itemName;
      this.itemsName = itemsName;
      this.parentItemName = parentItemName;
      this.parentType = parentType;
      this.childServiceUrl = childServiceUrl;
      this.parentServiceUrl = parentServiceUrl;
      this.openOnCommandName = openOnCommandName;
      if (commonDialog != CommonDialog.Upload)
        return;
      this.AddUploadDialog();
      parentElement.Add((ConfigElement) this.dialog);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DialogDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="commonDialog">The common dialog.</param>
    /// <param name="backText">The dialog back text.</param>
    /// <param name="title">The dialog title.</param>
    /// <param name="parentId">The parent id.</param>
    /// <param name="parentType">Type of the parent.</param>
    /// <param name="definitionNameToUse">The definition name which will be passed as a parameter.</param>
    /// <param name="openOnCommandName">Name of the command that opens dialog.</param>
    /// <param name="editCommandName">Name of the edit command.</param>
    public DialogDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementCollection parentElement,
      TParentFacade parentFacade,
      string viewName,
      CommonDialog commonDialog,
      string backText,
      string title,
      string parentId,
      Type parentType,
      string definitionNameToUse,
      string openOnCommandName,
      string editCommandName)
      : this(moduleName, definitionName, contentType, parentElement, parentFacade, commonDialog, backText, title)
    {
      this.viewName = viewName != null ? viewName : throw new ArgumentNullException(nameof (viewName));
      this.parentId = parentId;
      this.parentType = parentType;
      if (!string.IsNullOrEmpty(definitionNameToUse))
        this.definitionName = definitionNameToUse;
      this.openOnCommandName = openOnCommandName;
      this.editCommandName = editCommandName;
      switch (commonDialog)
      {
        case CommonDialog.Insert:
          this.AddInsertDialog();
          break;
        case CommonDialog.Edit:
          this.AddEditDialog();
          break;
        case CommonDialog.Preview:
          this.AddPreviewDialog();
          break;
        case CommonDialog.HistoryComparison:
          this.AddHistoryComparisonDialog();
          break;
        case CommonDialog.HistoryGrid:
          this.AddHistoryGridDialog();
          break;
        case CommonDialog.HistoryPreview:
          this.AddHistoryPreviewDialog();
          break;
      }
      parentElement.Add((ConfigElement) this.dialog);
    }

    /// <summary>Returns the current dialog element.</summary>
    /// <returns></returns>
    public DialogElement Get() => this.dialog;

    /// <summary>Maximizes the window.</summary>
    /// <returns></returns>
    public DialogDefinitionFacade<TParentFacade> MakeFullScreen()
    {
      this.dialog.CssClass = "sfMaximizedWindow";
      this.dialog.InitialBehaviors = WindowBehaviors.Maximize;
      this.dialog.Width = Unit.Percentage(100.0);
      this.dialog.Height = Unit.Percentage(100.0);
      return this;
    }

    /// <summary>
    /// Setsthe name of the command that will fire this dialog.
    /// </summary>
    /// <param name="commandName">The name of the command.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetOpenOnCommandName(
      string commandName)
    {
      this.dialog.OpenOnCommandName = !string.IsNullOrEmpty(commandName) ? commandName : throw new ArgumentNullException(nameof (commandName));
      return this;
    }

    /// <summary>Sets the name of the dialog.</summary>
    /// <param name="name">The name of the dialog.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetName(string name)
    {
      this.dialog.Name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof (name));
      return this;
    }

    /// <summary>Sets the height of the window. Default is 100%.</summary>
    /// <param name="hight">The height.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetHeight(Unit hight)
    {
      this.dialog.Height = hight;
      return this;
    }

    /// <summary>Sets the width of the window. Default is 100%.</summary>
    /// <param name="hight">The width.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetWidth(Unit width)
    {
      this.dialog.Width = width;
      return this;
    }

    /// <summary>Sets the initial behavior. Default is Maximize.</summary>
    /// <param name="behaviors">The initial behavior.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetInitialBehaviors(
      WindowBehaviors behaviors)
    {
      this.dialog.InitialBehaviors = behaviors;
      return this;
    }

    /// <summary>Sets the autobehaviors. Default is Default.</summary>
    /// <param name="behaviors">The autobehaviors.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetAutoSizeBehaviors(
      WindowAutoSizeBehaviors autoSizeBehaviors)
    {
      this.dialog.AutoSizeBehaviors = autoSizeBehaviors;
      return this;
    }

    /// <summary>Sets the behaviors. Default is None.</summary>
    /// <param name="behaviors">The behaviors.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetBehaviors(
      WindowBehaviors behaviors)
    {
      this.dialog.Behaviors = behaviors;
      return this;
    }

    /// <summary>Displays the status bar of the window.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> DisplayStatusBar()
    {
      this.dialog.VisibleStatusBar = true;
      return this;
    }

    /// <summary>Hides the status bar of the window.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> HideStatusBar()
    {
      this.dialog.VisibleStatusBar = false;
      return this;
    }

    /// <summary>Displays the titlebar of the window.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> DisplayTitleBar()
    {
      this.dialog.VisibleTitleBar = true;
      return this;
    }

    /// <summary>Hides the titlebar of the window.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> HideTitleBar()
    {
      this.dialog.VisibleTitleBar = false;
      return this;
    }

    /// <summary>
    /// Sets a collection of querystring-like parameters to pass to the dialog callback function.
    /// </summary>
    /// <param name="parameters">The parameters to pass.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetParameters(
      string parameters)
    {
      this.dialog.Parameters = !string.IsNullOrEmpty(parameters) ? parameters : throw new ArgumentNullException(nameof (parameters));
      return this;
    }

    /// <summary>Adds a collection of querystring-like parameters.</summary>
    /// <param name="parameters">The parameters to add.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> AddParameters(
      string parameters)
    {
      if (string.IsNullOrEmpty(parameters))
        throw new ArgumentNullException(nameof (parameters));
      if (!parameters.StartsWith("&"))
        parameters = "&" + parameters;
      this.dialog.Parameters += parameters;
      return this;
    }

    /// <summary>Sets the skin name for the control user interface.</summary>
    /// <param name="name">The skin name.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetSkin(string skin)
    {
      this.dialog.Skin = !string.IsNullOrEmpty(skin) ? skin : throw new ArgumentNullException(nameof (skin));
      return this;
    }

    /// <summary>Sets the dialog as modal.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> MakeModal()
    {
      this.dialog.IsModal = true;
      return this;
    }

    /// <summary>Sets the dialog as not modal.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> MakeNotModal()
    {
      this.dialog.IsModal = false;
      return this;
    }

    /// <summary>
    /// The dialog will be disposed and made inaccessible once it is closed.
    /// The next time a window with this ID is requested,
    /// a new window with default settings is created and returned.
    /// </summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> DestroyOnClose()
    {
      this.dialog.DestroyOnClose = new bool?(true);
      return this;
    }

    /// <summary>The dialog will not be disposed once it is closed.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> DoNotDestroyOnClose()
    {
      this.dialog.DestroyOnClose = new bool?(false);
      return this;
    }

    /// <summary>
    /// The page that is loaded in the dialog's window
    /// should be loaded everytime from the server or
    /// will leave the browser default behaviour.
    /// </summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> ReloadOnShow()
    {
      this.dialog.ReloadOnShow = new bool?(true);
      return this;
    }

    /// <summary>
    /// The page that is loaded in the dialog's window
    /// will not be loaded everytime from the server.
    /// </summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> DoNotReloadOnShow()
    {
      this.dialog.ReloadOnShow = new bool?(false);
      return this;
    }

    /// <summary>Sets the css class of the dialog.</summary>
    /// <param name="cssClass">The CssClass that should be applied to the dialog.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetCssClass(string cssClass)
    {
      this.dialog.CssClass = !string.IsNullOrEmpty(cssClass) ? cssClass : throw new ArgumentNullException(nameof (cssClass));
      return this;
    }

    /// <summary>Sets the module name the dialog depends on.</summary>
    /// <param name="moduleName">The ModuleName that should be applied to the dialog.</param>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> SetModuleName(
      string moduleName)
    {
      this.dialog.ModuleName = !string.IsNullOrEmpty(moduleName) ? moduleName : throw new ArgumentNullException(nameof (moduleName));
      return this;
    }

    /// <summary>Adds the dialog to the black list.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> BlackList()
    {
      this.dialog.IsBlackListed = new bool?(true);
      return this;
    }

    /// <summary>The dialog will not be added to the black list.</summary>
    /// <returns>An instance of the current <see cref="!:DialogDefinitionFacade" />.</returns>
    public DialogDefinitionFacade<TParentFacade> DoNotBlackList()
    {
      this.dialog.IsBlackListed = new bool?(false);
      return this;
    }

    /// <summary>Returns to parent facade.</summary>
    /// <returns>The parent facade which initialized this facade.</returns>
    public TParentFacade Done() => this.parentFacade;

    private void AddInsertDialog()
    {
      string str = string.Format("?ControlDefinitionName={0}&ViewName={1}", (object) this.definitionName, (object) this.viewName);
      if (!string.IsNullOrEmpty(this.backText))
        str = str + "&backLabelText=" + this.backText;
      if (this.parentType != (Type) null)
        str = str + "&parentType=" + WcfHelper.EncodeWcfString(this.parentType.FullName);
      if (!string.IsNullOrEmpty(this.parentId))
        str = str + "&parentId=" + this.parentId;
      this.dialog.OpenOnCommandName = !string.IsNullOrEmpty(this.openOnCommandName) ? this.openOnCommandName : "create";
      this.dialog.Name = "ContentViewInsertDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddEditDialog()
    {
      string str = string.Format("?ControlDefinitionName={0}&ViewName={1}", (object) this.definitionName, (object) this.viewName);
      if (!string.IsNullOrEmpty(this.backText))
        str = str + "&backLabelText=" + this.backText;
      if (this.parentType != (Type) null)
        str = str + "&parentType=" + WcfHelper.EncodeWcfString(this.parentType.FullName);
      if (!string.IsNullOrEmpty(this.parentId))
        str = str + "&parentId=" + this.parentId;
      this.dialog.OpenOnCommandName = !string.IsNullOrEmpty(this.openOnCommandName) ? this.openOnCommandName : "edit";
      this.dialog.Name = "ContentViewEditDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddPreviewDialog()
    {
      string str = string.Format("?ControlDefinitionName={0}&ViewName={1}&SuppressBackToButtonLabelModify=true", (object) this.definitionName, (object) this.viewName);
      if (!string.IsNullOrEmpty(this.backText))
        str = str + "&backLabelText=" + this.backText;
      if (this.parentType != (Type) null)
        str = str + "&parentType=" + this.parentType.FullName;
      if (!string.IsNullOrEmpty(this.parentId))
        str = str + "&parentId=" + this.parentId;
      this.dialog.OpenOnCommandName = "viewProperties";
      this.dialog.Name = "ContentViewEditDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddPermissionsDialog()
    {
      string str = string.Format("?moduleName={0}&typeName={1}", (object) this.moduleName, this.securedObjectType != (Type) null ? (object) this.securedObjectType.FullName : (object) this.typeName);
      if (!string.IsNullOrEmpty(this.backText))
        str = str + "&backLabelText=" + this.backText;
      if (!string.IsNullOrEmpty(this.title))
        str = str + "&title=" + this.title;
      if (!string.IsNullOrEmpty(this.permissionSetName))
        str = str + "&permissionSetName=" + this.permissionSetName;
      this.dialog.OpenOnCommandName = !string.IsNullOrEmpty(this.openOnCommandName) ? this.openOnCommandName : "permissions";
      this.dialog.Name = "ModulePermissionsDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddCustomFieldsDialog()
    {
      string str = string.Format("?TypeName={0}", (object) this.typeName);
      if (!string.IsNullOrEmpty(this.backText))
        str = str + "&BackLabelText=" + this.backText;
      if (!string.IsNullOrEmpty(this.title))
        str = str + "&Title=" + this.title;
      if (!string.IsNullOrEmpty(this.itemsName))
        str = str + "&ItemsName=" + this.itemsName;
      this.dialog.OpenOnCommandName = "moduleEditor";
      this.dialog.Name = "ModuleEditorDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddHistoryComparisonDialog()
    {
      string str = string.Format("?ControlDefinitionName={0}&VersionComparisonView={1}&moduleName={2}&typeName={3}", (object) this.definitionName, (object) this.viewName, (object) this.moduleName, (object) this.typeName);
      if (!string.IsNullOrEmpty(this.backText))
        str = str + "&backLabelText=" + this.backText;
      if (!string.IsNullOrEmpty(this.title))
        str = str + "&title=" + this.title;
      if (this.parentType != (Type) null)
        str = str + "&parentType=" + this.parentType.FullName;
      if (!string.IsNullOrEmpty(this.parentId))
        str = str + "&parentId=" + this.parentId;
      if (!string.IsNullOrEmpty(this.editCommandName))
        str = str + "&editCommandName=" + this.editCommandName;
      this.dialog.OpenOnCommandName = "history";
      this.dialog.Name = "VersionHistoryDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddHistoryGridDialog()
    {
      string str = string.Format("?ControlDefinitionName={0}&VersionComparisonView={1}&moduleName={2}&typeName={3}", (object) this.definitionName, (object) this.viewName, (object) this.moduleName, (object) this.typeName);
      if (!string.IsNullOrEmpty(this.backText))
        str = str + "&backLabelText=" + this.backText;
      if (!string.IsNullOrEmpty(this.title))
        str = str + "&title=" + this.title;
      if (this.parentType != (Type) null)
        str = str + "&parentType=" + this.parentType.FullName;
      if (!string.IsNullOrEmpty(this.parentId))
        str = str + "&parentId=" + this.parentId;
      if (!string.IsNullOrEmpty(this.editCommandName))
        str = str + "&editCommandName=" + this.editCommandName;
      this.dialog.OpenOnCommandName = "historygrid";
      this.dialog.Name = "VersionHistoryDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddHistoryPreviewDialog()
    {
      string str = string.Format("?ControlDefinitionName={0}&ViewName={1}&SuppressBackToButtonLabelModify=true", (object) this.definitionName, (object) this.viewName);
      if (!string.IsNullOrEmpty(this.backText))
        str = str + "&backLabelText=" + this.backText;
      if (this.parentType != (Type) null)
        str = str + "&parentType=" + this.parentType.FullName;
      if (!string.IsNullOrEmpty(this.parentId))
        str = str + "&parentId=" + this.parentId;
      if (!string.IsNullOrEmpty(this.editCommandName))
        str = str + "&editCommandName=" + this.editCommandName;
      this.dialog.OpenOnCommandName = "versionPreview";
      this.dialog.Name = "ContentViewEditDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddUploadDialog()
    {
      string str = string.Format("?contentType={0}&providerName={1}&itemName={2}&itemsName={3}&libraryTypeName={4}&libraryType={5}&childServiceUrl={6}&parentServiceUrl={7}", (object) this.typeName, (object) this.providerName, (object) this.itemName, (object) this.itemsName, (object) this.parentItemName, (object) this.parentType.FullName, (object) HttpUtility.UrlEncode(this.childServiceUrl), (object) HttpUtility.UrlEncode(this.parentServiceUrl));
      this.dialog.OpenOnCommandName = !string.IsNullOrEmpty(this.openOnCommandName) ? this.openOnCommandName : "upload";
      this.dialog.Name = "UploadDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }

    private void AddHtml5UploadDialog()
    {
      string str = string.Format("?contentType={0}&providerName={1}&itemName={2}&itemsName={3}&libraryTypeName={4}&libraryType={5}&childServiceUrl={6}&parentServiceUrl={7}", (object) this.typeName, (object) this.providerName, (object) this.itemName, (object) this.itemsName, (object) this.parentItemName, (object) this.parentType.FullName, (object) HttpUtility.UrlEncode(this.childServiceUrl), (object) HttpUtility.UrlEncode(this.parentServiceUrl));
      this.dialog.OpenOnCommandName = !string.IsNullOrEmpty(this.openOnCommandName) ? this.openOnCommandName : "html5upload";
      this.dialog.Name = "Html5UploadDialog";
      this.dialog.Parameters = str;
      this.MakeFullScreen();
    }
  }
}
