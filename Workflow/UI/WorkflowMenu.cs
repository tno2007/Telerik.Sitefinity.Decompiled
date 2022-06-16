// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.UI.WorkflowMenu
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ItemLists;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Workflow.UI
{
  /// <summary>
  /// This control represents the menu with actions defined by the workflow.
  /// </summary>
  public class WorkflowMenu : WidgetBar
  {
    internal const string workflowMenuScriptName = "Telerik.Sitefinity.Workflow.Scripts.WorkflowMenu.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Workflow.WorkflowMenu.ascx");
    private const string workflowServiceUrl = "~/Sitefinity/Services/Workflow/WorkflowService.svc";
    private bool showMoreActions = true;
    private string cancelUrl;
    private string cancelText;
    private string returnUrl;
    private bool showCheckRelatingData;
    private bool recycleBinEnabled;
    private Collection<PromptDialog> promptDialogControls;

    /// <summary>
    /// Gets or sets the cancel url of the page; the url where user ought to be
    /// redirected when cancel button is pressed.
    /// </summary>
    public string CancelUrl
    {
      get => this.cancelUrl;
      set
      {
        this.cancelUrl = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>Gets or sets the text of the cancel button.</summary>
    public string CancelText
    {
      get => this.cancelText;
      set
      {
        this.cancelText = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets or sets the cancel url of the page; the url where user ought to be
    /// redirected when cancel button is pressed.
    /// </summary>
    public string ReturnUrl
    {
      get => this.returnUrl;
      set => this.returnUrl = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? WorkflowMenu.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets a value indicating whether [skip templating].</summary>
    /// <value><c>true</c> if [skip templating]; otherwise, <c>false</c>.</value>
    protected override bool SkipTemplating => false;

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
    /// Used to hide more actions workflow elements when in inline editing mode
    /// </summary>
    internal bool ShowMoreActions
    {
      get => this.showMoreActions;
      set => this.showMoreActions = value;
    }

    /// <summary>Gets or sets the show check relating data option.</summary>
    /// <value>The show check relating data.</value>
    public bool ShowCheckRelatingData
    {
      get => this.showCheckRelatingData;
      set => this.showCheckRelatingData = value;
    }

    /// <summary>
    /// Gets or sets whether the Recycle Bin module is enabled.
    /// </summary>
    public bool RecycleBinEnabled
    {
      get => this.recycleBinEnabled;
      set => this.recycleBinEnabled = value;
    }

    /// <summary>
    /// Gets the reference to the html control in which main and secondary workflow
    /// actions ought to be loaded.
    /// </summary>
    protected virtual HtmlGenericControl ActionsContainer => this.Container.GetControl<HtmlGenericControl>("actionsContainer", true);

    protected virtual HtmlGenericControl SecondaryActionsContainer => this.Container.GetControl<HtmlGenericControl>("secondaryActionsContainer", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:Telerik.Web.UI.RadMenu" /> in which the other workflow
    /// actions ought to loaded.
    /// </summary>
    protected virtual RadMenu OtherActionsMenu => this.Container.GetControl<RadMenu>("otherActionsMenu", true);

    /// <summary>
    /// Gets the reference to <see cref="T:System.Web.UI.WebControls.HyperLink" /> that closes the form.
    /// </summary>
    protected virtual HyperLink CancelLink => this.Container.GetControl<HyperLink>("cancelLink", true);

    /// <summary>
    /// Gets a reference to the container control with the loading image.
    /// </summary>
    /// <value>The loading view.</value>
    public virtual Control LoadingView => this.Container.GetControl<Control>("loadingView", true);

    /// <summary>
    /// Gets the dialog that displays the warning messages specified by the workflow activities
    /// </summary>
    protected PromptDialog WorkflowWarningDialog => this.Container.GetControl<PromptDialog>("workflowWarningDialog", true);

    protected Literal OrLiteral => this.Container.GetControl<Literal>("orLiteral", true);

    protected SfImage LoadingImage => this.Container.GetControl<SfImage>("loadingImage1", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (string.IsNullOrEmpty(this.CancelUrl))
        this.CancelLink.NavigateUrl = "javascript:void(0);";
      else if (this.CancelUrl.StartsWith("javascript:"))
        this.CancelLink.Attributes.Add("onclick", this.CancelUrl);
      else
        this.CancelLink.NavigateUrl = this.CancelUrl;
      PromptDialog awareDeleteDialog = ItemsListBase.GetLanguageAwareDeleteDialog(Res.Get<Labels>("WhatDoYouWantToDelete", Res.CurrentBackendCulture), this.ShowCheckRelatingData, this.RecycleBinEnabled);
      container.Controls.Add((Control) awareDeleteDialog);
      this.PromptDialogControls.Add(awareDeleteDialog);
      PromptDialog standartDeleteDialog = ItemsListBase.GetStandartDeleteDialog(Res.Get<Labels>("AreYouSureYouWantToDeleteItem", Res.CurrentBackendCulture), this.ShowCheckRelatingData, this.RecycleBinEnabled);
      container.Controls.Add((Control) standartDeleteDialog);
      this.PromptDialogControls.Add(standartDeleteDialog);
      RadMenuItem radMenuItem = this.OtherActionsMenu.Items[0];
      if (radMenuItem != null)
        radMenuItem.Text = Res.Get<Labels>("MoreActions", Res.CurrentBackendCulture);
      this.OrLiteral.Text = Res.Get<Labels>("Or", Res.CurrentBackendCulture);
      this.CancelLink.Text = Res.Get<Labels>("GoBack", Res.CurrentBackendCulture);
      this.LoadingImage.AlternateText = Res.Get<Labels>("SavingImgAlt", Res.CurrentBackendCulture);
      this.WorkflowWarningDialog.Title = Res.Get<Labels>("Warning", Res.CurrentBackendCulture);
      CommandToolboxItem commandToolboxItem1 = this.WorkflowWarningDialog.Commands.Where<CommandToolboxItem>((Func<CommandToolboxItem, bool>) (cmd => cmd.CommandName == "ok")).FirstOrDefault<CommandToolboxItem>();
      if (commandToolboxItem1 != null)
        commandToolboxItem1.Text = Res.Get<Labels>("ok", Res.CurrentBackendCulture);
      CommandToolboxItem commandToolboxItem2 = this.WorkflowWarningDialog.Commands.Where<CommandToolboxItem>((Func<CommandToolboxItem, bool>) (cmd => cmd.CommandName == "cancel")).FirstOrDefault<CommandToolboxItem>();
      if (commandToolboxItem2 != null)
        commandToolboxItem2.Text = Res.Get<Labels>("cancel", Res.CurrentBackendCulture);
      if (!string.IsNullOrEmpty(this.CancelText))
        this.CancelLink.Text = this.CancelText;
      PromptDialog child = PromptDialog.FromDefinition(((IMasterViewDefinition) Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls["BackendPages"].Views["BackendPagesListView"]).PromptDialogs.Single<IPromptDialogDefinition>((Func<IPromptDialogDefinition, bool>) (d => d.DialogName == "cannotDeleteParentPageDialog")));
      container.Controls.Add((Control) child);
      this.PromptDialogControls.Add(child);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = (ScriptBehaviorDescriptor) base.GetScriptDescriptors().First<ScriptDescriptor>();
      behaviorDescriptor.AddElementProperty("actionsContainer", this.ActionsContainer.ClientID);
      behaviorDescriptor.AddElementProperty("secondaryActionsContainer", this.SecondaryActionsContainer.ClientID);
      behaviorDescriptor.AddComponentProperty("otherActionsMenu", this.OtherActionsMenu.ClientID);
      behaviorDescriptor.AddComponentProperty("workflowWarningDialog", this.WorkflowWarningDialog.ClientID);
      behaviorDescriptor.AddProperty("workflowServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Workflow/WorkflowService.svc"));
      behaviorDescriptor.AddProperty("dialogBaseUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity" + "/Dialog/"));
      behaviorDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      behaviorDescriptor.AddElementProperty("cancelLink", this.CancelLink.ClientID);
      behaviorDescriptor.AddProperty("_isMultilingual", (object) SystemManager.CurrentContext.AppSettings.Multilingual);
      behaviorDescriptor.AddProperty("_backendUICulture", (object) SystemManager.CurrentContext.Culture.Name);
      behaviorDescriptor.AddProperty("_showMoreActions", (object) this.ShowMoreActions);
      behaviorDescriptor.AddProperty("_recycleBinEnabled", (object) this.RecycleBinEnabled);
      behaviorDescriptor.AddProperty("_sendToRecycleBinSingleConfirmationMessage", (object) Res.Get<Labels>().SendToRecycleBinSingleConfirmationMessage);
      behaviorDescriptor.AddProperty("_validateItemKey", (object) "validateItem");
      if (this.Page.Items.Contains((object) "validateItem") && (bool) this.Page.Items[(object) "validateItem"])
        behaviorDescriptor.AddProperty("_validateItem", (object) true);
      else
        behaviorDescriptor.AddProperty("_validateItem", (object) false);
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      Dictionary<string, string> dictionary1 = new Dictionary<string, string>();
      Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
      foreach (PromptDialog promptDialogControl in this.PromptDialogControls)
      {
        dictionary1[promptDialogControl.DialogName] = promptDialogControl.ClientID;
        if (!string.IsNullOrEmpty(promptDialogControl.OpenOnCommand))
          dictionary2[promptDialogControl.OpenOnCommand] = promptDialogControl.DialogName;
      }
      string str1 = scriptSerializer.Serialize((object) dictionary1);
      string str2 = scriptSerializer.Serialize((object) dictionary2);
      behaviorDescriptor.AddProperty("_promptDialogNamesJson", (object) str1);
      behaviorDescriptor.AddProperty("_promptDialogCommandsJson", (object) str2);
      if (!string.IsNullOrEmpty(this.ReturnUrl))
        behaviorDescriptor.AddProperty("returnUrl", (object) VirtualPathUtility.ToAbsolute(this.ReturnUrl));
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (WorkflowMenu).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Workflow.Scripts.WorkflowMenu.js", typeof (WorkflowMenu).Assembly.FullName)
    };
  }
}
