// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.ContentBrowseAndEditToolbar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  /// <summary>
  /// Representing a class for toolbars used for browse and edit functionality
  /// </summary>
  public class ContentBrowseAndEditToolbar : 
    BrowseAndEditToolbar,
    IContentBrowseAndEditToolbar,
    IBrowseAndEditToolbar
  {
    private string permissionSet = "General";
    private List<TaxonomyFilterInfo> taxonomyFilters;
    private string templatePath;
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.ContentBrowseAndEditToolbarControl.ascx");
    internal const string clickMenuScript = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js";
    internal const string contentBrowseAndEditToolbarScript = "Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.Scripts.ContentBrowseAndEditToolbar.js";
    public const string DialogPrefixKey = "dialogPrefix";

    /// <summary>
    /// Gets or sets the path of the template to be used by the control that will override the default one.
    /// </summary>
    /// <value></value>
    public string TemplatePath
    {
      get => this.templatePath;
      set => this.templatePath = value;
    }

    /// <summary>Gets the layout template name</summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(this.TemplatePath) ? ContentBrowseAndEditToolbar.layoutTemplatePath : this.TemplatePath;
      set => this.TemplatePath = value;
    }

    /// <summary>Gets or sets the manager.</summary>
    public IManager DataManager { get; set; }

    /// <summary>Gets or sets the content item.</summary>
    /// <value>The content item.</value>
    public Content Item { get; set; }

    /// <summary>Gets or sets the content item.</summary>
    /// <value>The content item.</value>
    [Obsolete("Use Parent property instead.")]
    public Content ParentItem
    {
      get => this.Parent as Content;
      set => this.Parent = (IDataItem) value;
    }

    /// <summary>Gets the parent data item.</summary>
    /// <returns>The parent data item.</returns>
    public IDataItem Parent { get; set; }

    /// <summary>
    /// Gets or sets a value that indicates whether a server control is rendered as UI on the page.
    /// </summary>
    /// <returns>
    /// true if the control is visible on the page; otherwise false.
    /// </returns>
    public override bool Visible
    {
      get => false;
      set => base.Visible = value;
    }

    /// <summary>Gets the BrowseAndEditDeleteOperations container</summary>
    protected Control BrowseAndEditDeleteOperationsContainer => this.Container.GetControl<Control>("BrowseAndEditDeleteOperations", false);

    /// <summary>Gets the BrowseAndEditDeleteOperationsTitleElement</summary>
    protected Control BrowseAndEditDeleteOperationsTitleElement => this.Container.GetControl<Control>(nameof (BrowseAndEditDeleteOperationsTitleElement), false);

    /// <summary>The toolbar taxonomy filters.</summary>
    public List<TaxonomyFilterInfo> TaxonomyFiltersInfo
    {
      get
      {
        if (this.taxonomyFilters == null)
          this.taxonomyFilters = new List<TaxonomyFilterInfo>();
        return this.taxonomyFilters;
      }
      set => this.taxonomyFilters = value;
    }

    /// <summary>
    /// Gets/Sets the dialog used to warn user when deleting item
    /// </summary>
    /// <value>The dialog.</value>
    protected PromptDialog DeleteItemWarningDialog => this.Container.GetControl<PromptDialog>(nameof (DeleteItemWarningDialog), false);

    /// <summary>
    /// Gets/Sets the dialog used to warn user when unpublishing item
    /// </summary>
    /// <value>The dialog.</value>
    protected PromptDialog UnpublishItemWarningDialog => this.Container.GetControl<PromptDialog>(nameof (UnpublishItemWarningDialog), false);

    /// <summary>Overriden method InitializeControls</summary>
    /// <param name="container">The container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.DeleteItemWarningDialog.Visible = false;
      this.UnpublishItemWarningDialog.Visible = false;
      Control control = this.Container.GetControl<Control>("BrowseAndEditToolbar", false);
      if (control == null)
        return;
      control.Visible = false;
    }

    /// <summary>Configures the content toolbar.</summary>
    /// <param name="host">The container for specific views.</param>
    /// <param name="contentItem">The content item.</param>
    public void Configure(ContentView host, Content contentItem) => this.Configure(host, contentItem, (IDataItem) null);

    /// <summary>Configures the content toolbar.</summary>
    /// <param name="host">The container for specific views.</param>
    /// <param name="contentItem">The content item.</param>
    /// <param name="parentItem">The parent item.</param>
    public void Configure(ContentView host, Content contentItem, IDataItem parentItem)
    {
      this.ItemType = host.ControlDefinition.ContentType;
      this.ProviderName = host.ControlDefinition.ProviderName;
      if (contentItem == null)
        return;
      this.ItemId = contentItem.OriginalContentId != Guid.Empty ? contentItem.OriginalContentId : contentItem.Id;
      int index = host.ViewControl.Parent.Controls.IndexOf(host.ViewControl);
      LiteralControl child1 = new LiteralControl(string.Format("<SitefinityContent data-sf-id=\"{0}\" data-sf-type=\"{1}\" data-sf-provider=\"{2}\">", (object) this.ItemId, (object) this.ItemType.FullName, (object) this.ProviderName));
      LiteralControl child2 = new LiteralControl("</SitefinityContent>");
      host.ViewControl.Controls.AddAt(index, (Control) child1);
      host.ViewControl.Controls.AddAt(index + 2, (Control) child2);
    }

    /// <summary>
    /// Initializes the commands, which will be used to open BrowseAndEdit dialogs.
    /// </summary>
    protected override void InitializeCommands()
    {
      base.InitializeCommands();
      bool flag = typeof (IHasParent).IsAssignableFrom(this.ItemType);
      this.ManageMode(BrowseAndEditToolbarMode.Add, "BrowseAndEditAdd", "create", flag ? SecurityActionTypes.Manage : SecurityActionTypes.Create);
      this.ManageMode(BrowseAndEditToolbarMode.Edit, "BrowseAndEditEdit", "edit", flag ? SecurityActionTypes.Manage : SecurityActionTypes.Modify);
      this.ManageMode(BrowseAndEditToolbarMode.Delete, "BrowseAndEditDelete", "delete", flag ? SecurityActionTypes.Manage : SecurityActionTypes.Delete);
      this.ManageMode(BrowseAndEditToolbarMode.Unpublish, "BrowseAndEditUnpublish", "unpublish", flag ? SecurityActionTypes.Manage : SecurityActionTypes.Modify);
    }

    private void InitializeDialogDefinitions(ContentView host)
    {
      IEnumerable<IDialogDefinition> dialogs = ((IContentViewConfig) ((ModuleBase) SystemManager.GetModule(host.ModuleName)).ModuleConfig).ContentViewControls[host.ControlDefinitionName].Dialogs;
      string str = string.Format("{0}_{1}_", (object) this.ItemType.Name, (object) this.ProviderName);
      foreach (BrowseAndEditCommand browseAndEditCommand in (IEnumerable<BrowseAndEditCommand>) this.ToolbarControls.Values)
      {
        BrowseAndEditCommand command = browseAndEditCommand;
        IDialogDefinition dialogDefinition = dialogs.SingleOrDefault<IDialogDefinition>((Func<IDialogDefinition, bool>) (d => d.OpenOnCommandName == command.CommandName));
        command.Arguments.DialogDefinition = dialogDefinition;
        if (dialogDefinition != null)
        {
          command.Arguments.DialogName = str + command.Arguments.DialogDefinition.OpenOnCommandName;
          command.Arguments.AdditionalProperties["dialogPrefix"] = (object) str;
        }
      }
      foreach (IDialogDefinition dialogDefinition1 in dialogs)
      {
        IDialogDefinition dialogDefinition = dialogDefinition1;
        if (!this.ToolbarControls.Values.Any<BrowseAndEditCommand>((Func<BrowseAndEditCommand, bool>) (c => c.CommandName == dialogDefinition.OpenOnCommandName)))
        {
          BrowseAndEditCommand browseAndEditCommand1 = new BrowseAndEditCommand()
          {
            CommandName = dialogDefinition.OpenOnCommandName,
            Visible = false
          };
          browseAndEditCommand1.Arguments.DialogDefinition = dialogDefinition;
          browseAndEditCommand1.Arguments.DialogName = str + browseAndEditCommand1.Arguments.DialogDefinition.OpenOnCommandName;
          browseAndEditCommand1.Arguments.AdditionalProperties["dialogPrefix"] = (object) str;
          IDictionary<Control, BrowseAndEditCommand> toolbarControls = this.ToolbarControls;
          Control key = new Control();
          key.ID = browseAndEditCommand1.CommandName;
          key.Visible = false;
          BrowseAndEditCommand browseAndEditCommand2 = browseAndEditCommand1;
          toolbarControls.Add(key, browseAndEditCommand2);
          this.Commands.Add(browseAndEditCommand1);
        }
      }
    }

    /// <summary>
    /// Manages the states for modes int <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditToolbarMode" /> (on/off)
    /// </summary>
    protected void ManageMode(
      BrowseAndEditToolbarMode mode,
      string controlID,
      string commandName,
      SecurityActionTypes securityType)
    {
      Control control = this.ToolbarContainer.FindControl(controlID);
      if (control == null)
        return;
      control.Visible = false;
      if (!this.IsGrantedOperation(mode, securityType) || (this.Mode & mode) != mode)
        return;
      BrowseAndEditCommand browseAndEditCommand = new BrowseAndEditCommand()
      {
        CommandName = commandName
      };
      if (mode == BrowseAndEditToolbarMode.Delete || mode == BrowseAndEditToolbarMode.Unpublish)
        this.TurnOnDeleteOperations("BrowseAndEditDeleteOperations", "BrowseAndEditDeleteOperationsCommands");
      if ((mode == BrowseAndEditToolbarMode.Add || mode == BrowseAndEditToolbarMode.Edit) && this.ParentId != Guid.Empty)
        browseAndEditCommand.Arguments.DialogUrlParameters = new List<KeyValuePair<string, string>>()
        {
          new KeyValuePair<string, string>("parentId", this.ParentId.ToString())
        };
      if (mode == BrowseAndEditToolbarMode.Add && this.TaxonomyFiltersInfo != null && this.TaxonomyFiltersInfo.Count > 0)
        browseAndEditCommand.Arguments.AdditionalProperties.Add("TaxonomyFilters", (object) this.TaxonomyFiltersInfo);
      control.Visible = true;
      this.ToolbarControls.Add(new KeyValuePair<Control, BrowseAndEditCommand>(control, browseAndEditCommand));
      this.Commands.Add(browseAndEditCommand);
    }

    protected bool IsGrantedOperation(
      BrowseAndEditToolbarMode mode,
      SecurityActionTypes securityType)
    {
      secObj = (ISecuredObject) null;
      string permissionSetName = this.permissionSet;
      if (mode == BrowseAndEditToolbarMode.Add && this.Parent != null)
      {
        if (this.Parent is ISecuredObject secObj)
          permissionSetName = secObj.SupportedPermissionSets[1];
      }
      else if (mode == BrowseAndEditToolbarMode.Add || this.Item == null)
      {
        secObj = this.DataManager.GetSecurityRoot();
        permissionSetName = secObj.SupportedPermissionSets[0];
      }
      else if (this.Item != null && this.Item is ISecuredObject)
      {
        secObj = this.Item as ISecuredObject;
        permissionSetName = secObj.SupportedPermissionSets[0];
      }
      return secObj == null || secObj.IsSecurityActionTypeGranted(permissionSetName, securityType);
    }

    /// <summary>Turns on the delete operation control with given Id</summary>
    /// <param name="rootControlID">The Id of the root control.</param>
    /// <param name="operationsControlID">The Id of delete operation control.</param>
    private void TurnOnDeleteOperations(string rootControlID, string operationsControlID)
    {
      Control control1 = this.ToolbarContainer.FindControl(rootControlID);
      if (control1.Visible)
        return;
      control1.Visible = true;
      foreach (Control control2 in this.ToolbarContainer.FindControl(operationsControlID).Controls)
        control2.Visible = false;
    }

    /// <summary>Overriden method GetScriptDescriptors</summary>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.Type = typeof (ContentBrowseAndEditToolbar).FullName;
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>Overriden method GetScriptReferences</summary>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = name,
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js"
      });
      string str = this.GetType().Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.Scripts.ContentBrowseAndEditToolbar.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
