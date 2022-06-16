// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>
  /// A class to supplement the EditorToolbar with Email Campaigns functionality
  /// </summary>
  internal class ZoneEditorToolBarExtension : ScriptControl
  {
    private EditorToolBar baseToolbar;
    private PromptDialog sendTestPrompt;
    private PromptDialog sendIssuePrompt;
    private Telerik.Web.UI.RadWindow campaignWizardDialog;
    private ClientLabelManager clientLabelManager;
    private const string campaignServiceUrl = "~/Sitefinity/Services/Newsletters/Campaign.svc";
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.ZoneEditorToolBarExtension.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.ZoneEditorToolBarExtension" /> class.
    /// </summary>
    /// <param name="toolbar">The toolbar.</param>
    public ZoneEditorToolBarExtension(EditorToolBar toolbar)
    {
      this.baseToolbar = toolbar;
      this.CampaignId = SystemManager.CurrentHttpContext.Request.Params[nameof (CampaignId)];
      this.ProviderName = SystemManager.CurrentHttpContext.Request.Params["providerName"];
      Guid issueId = new Guid(this.CampaignId);
      NewslettersManager manager = NewslettersManager.GetManager(this.ProviderName);
      Campaign issue = manager.GetIssue(issueId);
      ABCampaign abCampaign = manager.GetABCampaigns().FirstOrDefault<ABCampaign>((Expression<Func<ABCampaign, bool>>) (t => t.CampaignA == issue || t.CampaignB == issue));
      this.IsAbTestIssue = abCampaign != null;
      this.IsBIssue = this.IsAbTestIssue && abCampaign.CampaignB == issue;
      if (abCampaign != null)
        this.AbTestId = abCampaign.Id;
      else
        this.AbTestId = Guid.Empty;
    }

    /// <summary>Gets the campaign id.</summary>
    public string CampaignId { get; private set; }

    /// <summary>Gets the name of the provider.</summary>
    public string ProviderName { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the edited issue is A/B test issue.
    /// </summary>
    public bool IsAbTestIssue { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the edited issue is B issue of an A/B test.
    /// </summary>
    public bool IsBIssue { get; private set; }

    /// <summary>
    /// Gets the A/B test id that the edited issue belongs to.
    /// </summary>
    public Guid AbTestId { get; private set; }

    /// <summary>Gets a reference to the send test prompt dialog.</summary>
    protected PromptDialog SendTestPrompt
    {
      get => this.sendTestPrompt;
      private set => this.sendTestPrompt = value;
    }

    /// <summary>Gets a reference to the send issue prompt dialog.</summary>
    protected PromptDialog SendIssuePrompt
    {
      get => this.sendIssuePrompt;
      private set => this.sendIssuePrompt = value;
    }

    /// <summary>Gets a reference to the delete confirmation dialog.</summary>
    protected PromptDialog DeleteConfirmationDialog { get; private set; }

    /// <summary>Gets a reference to the campaign wizard dialog.</summary>
    protected Telerik.Web.UI.RadWindow CampaignWizardDialog
    {
      get => this.campaignWizardDialog;
      private set => this.campaignWizardDialog = value;
    }

    /// <summary>Gets a reference to the schedule delivery window.</summary>
    protected ScheduleDeliveryWindow ScheduleDeliveryWindow { get; private set; }

    /// <summary>Gets the ab test detail view dialog.</summary>
    protected virtual Telerik.Web.UI.RadWindow AbTestDetailViewDialog { get; private set; }

    /// <summary>Gets a reference to the client label manager.</summary>
    public ClientLabelManager ClientLabelManager
    {
      get => this.clientLabelManager;
      private set => this.clientLabelManager = value;
    }

    /// <inheritdoc />
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      PromptDialog promptDialog1 = new PromptDialog();
      promptDialog1.ID = "sendTestPrompt";
      promptDialog1.Title = Res.Get<NewslettersResources>().SendTestMessage;
      promptDialog1.Message = Res.Get<NewslettersResources>().EnterIssueTestEmailAddresses;
      promptDialog1.Width = 350;
      promptDialog1.Height = 300;
      promptDialog1.Mode = PromptMode.Input;
      promptDialog1.AllowCloseButton = true;
      promptDialog1.InputRows = 5;
      promptDialog1.ShowOnLoad = false;
      this.SendTestPrompt = promptDialog1;
      Collection<CommandToolboxItem> commands1 = this.SendTestPrompt.Commands;
      CommandToolboxItem commandToolboxItem1 = new CommandToolboxItem();
      commandToolboxItem1.Text = Res.Get<NewslettersResources>().SendTestMessage;
      commandToolboxItem1.CommandName = "sendTest";
      commandToolboxItem1.CommandType = CommandType.SaveButton;
      commandToolboxItem1.WrapperTagName = "LI";
      commands1.Add(commandToolboxItem1);
      Collection<CommandToolboxItem> commands2 = this.SendTestPrompt.Commands;
      CommandToolboxItem commandToolboxItem2 = new CommandToolboxItem();
      commandToolboxItem2.Text = Res.Get<Labels>().Cancel;
      commandToolboxItem2.CommandName = "cancel";
      commandToolboxItem2.CommandType = CommandType.CancelButton;
      commandToolboxItem2.WrapperTagName = "LI";
      commands2.Add(commandToolboxItem2);
      this.Controls.Add((Control) this.SendTestPrompt);
      if (!this.IsAbTestIssue)
      {
        PromptDialog promptDialog2 = new PromptDialog();
        promptDialog2.ID = "sendIssuePrompt";
        promptDialog2.Width = 350;
        promptDialog2.Height = 300;
        promptDialog2.Mode = PromptMode.Confirm;
        promptDialog2.AllowCloseButton = true;
        promptDialog2.InputRows = 5;
        promptDialog2.ShowOnLoad = false;
        this.SendIssuePrompt = promptDialog2;
        Collection<CommandToolboxItem> commands3 = this.SendIssuePrompt.Commands;
        CommandToolboxItem commandToolboxItem3 = new CommandToolboxItem();
        commandToolboxItem3.Text = Res.Get<NewslettersResources>().SendThisIssue;
        commandToolboxItem3.CommandName = "send";
        commandToolboxItem3.CommandType = CommandType.SaveButton;
        commandToolboxItem3.WrapperTagName = "LI";
        commands3.Add(commandToolboxItem3);
        Collection<CommandToolboxItem> commands4 = this.SendIssuePrompt.Commands;
        CommandToolboxItem commandToolboxItem4 = new CommandToolboxItem();
        commandToolboxItem4.Text = Res.Get<Labels>().Cancel;
        commandToolboxItem4.CommandName = "cancel";
        commandToolboxItem4.CommandType = CommandType.CancelButton;
        commandToolboxItem4.WrapperTagName = "LI";
        commands4.Add(commandToolboxItem4);
        this.Controls.Add((Control) this.SendIssuePrompt);
        Telerik.Web.UI.RadWindow radWindow = new Telerik.Web.UI.RadWindow();
        radWindow.ID = "campaignWizardDialog";
        radWindow.NavigateUrl = "~/Sitefinity/Dialog/CampaignDetailView";
        radWindow.CssClass = "sfMaximizedWindow";
        radWindow.Skin = "Default";
        radWindow.Behaviors = WindowBehaviors.None;
        radWindow.VisibleStatusbar = false;
        radWindow.ShowContentDuringLoad = false;
        this.CampaignWizardDialog = radWindow;
        this.Controls.Add((Control) this.CampaignWizardDialog);
        ScheduleDeliveryWindow scheduleDeliveryWindow = new ScheduleDeliveryWindow();
        scheduleDeliveryWindow.ID = "scheduleDeliveryWindow";
        this.ScheduleDeliveryWindow = scheduleDeliveryWindow;
        this.Controls.Add((Control) this.ScheduleDeliveryWindow);
        PromptDialog promptDialog3 = new PromptDialog();
        promptDialog3.ID = "deleteConfirmationDialog";
        promptDialog3.Title = "";
        promptDialog3.Message = Res.Get<Labels>().QuestionBeforeDeletingItem;
        promptDialog3.ShowOnLoad = false;
        this.DeleteConfirmationDialog = promptDialog3;
        Collection<CommandToolboxItem> commands5 = this.DeleteConfirmationDialog.Commands;
        CommandToolboxItem commandToolboxItem5 = new CommandToolboxItem();
        commandToolboxItem5.Text = Res.Get<Labels>().YesDelete;
        commandToolboxItem5.CommandName = "ok";
        commandToolboxItem5.CommandType = CommandType.NormalButton;
        commandToolboxItem5.WrapperTagName = "LI";
        commands5.Add(commandToolboxItem5);
        Collection<CommandToolboxItem> commands6 = this.DeleteConfirmationDialog.Commands;
        CommandToolboxItem commandToolboxItem6 = new CommandToolboxItem();
        commandToolboxItem6.Text = Res.Get<Labels>().Cancel;
        commandToolboxItem6.CommandName = "cancel";
        commandToolboxItem6.CommandType = CommandType.NormalButton;
        commandToolboxItem6.WrapperTagName = "LI";
        commands6.Add(commandToolboxItem6);
        this.Controls.Add((Control) this.DeleteConfirmationDialog);
      }
      Telerik.Web.UI.RadWindow radWindow1 = new Telerik.Web.UI.RadWindow();
      radWindow1.ID = "abTestDetailViewDialog";
      radWindow1.NavigateUrl = "~/Sitefinity/Dialog/AbTestDetailView";
      radWindow1.CssClass = "sfMaximizedWindow";
      radWindow1.Skin = "Default";
      radWindow1.Behaviors = WindowBehaviors.None;
      radWindow1.VisibleStatusbar = false;
      radWindow1.ShowContentDuringLoad = false;
      this.AbTestDetailViewDialog = radWindow1;
      this.Controls.Add((Control) this.AbTestDetailViewDialog);
      ClientLabelManager clientLabelManager = new ClientLabelManager();
      clientLabelManager.ID = "clientLabelManager";
      this.ClientLabelManager = clientLabelManager;
      ClientLabel clientLabel1 = new ClientLabel();
      clientLabel1.ClassId = "NewslettersResources";
      clientLabel1.Key = "EnterAtLeastOneTestEmailAddress";
      ClientLabel clientLabel2 = clientLabel1;
      ClientLabel clientLabel3 = new ClientLabel();
      clientLabel3.ClassId = "ErrorMessages";
      clientLabel3.Key = "EmailAddressViolationMessage";
      ClientLabel clientLabel4 = clientLabel3;
      ClientLabel clientLabel5 = new ClientLabel();
      clientLabel5.ClassId = "NewslettersResources";
      clientLabel5.Key = "SendIssueFor";
      ClientLabel clientLabel6 = clientLabel5;
      ClientLabel clientLabel7 = new ClientLabel();
      clientLabel7.ClassId = "NewslettersResources";
      clientLabel7.Key = "SendIssuePromptText";
      ClientLabel clientLabel8 = clientLabel7;
      this.ClientLabelManager.Labels.Add((ClientLableBase) clientLabel2);
      this.ClientLabelManager.Labels.Add((ClientLableBase) clientLabel4);
      this.ClientLabelManager.Labels.Add((ClientLableBase) clientLabel6);
      this.ClientLabelManager.Labels.Add((ClientLableBase) clientLabel8);
      this.Controls.Add((Control) this.ClientLabelManager);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      RadToolBarItem itemByValue = this.baseToolbar.MainToolBar.FindItemByValue("SendCampaign");
      itemByValue.Attributes.Add("onclick", "editorToolBar.sendCampaign();");
      this.baseToolbar.MainToolBar.FindItemByValue("SaveCampaignDraft").Attributes.Add("onclick", "editorToolBar.saveCampaignDraft();");
      RadToolBarDropDown radToolBarDropDown = (RadToolBarDropDown) this.baseToolbar.MainToolBar.FindItem((Predicate<RadToolBarItem>) (m => m.CssClass == "campaignsMoreActions"));
      radToolBarDropDown.Buttons.FindButtonByValue("SendTest").Attributes.Add("onclick", "editorToolBar.sendTest();");
      if (!this.IsAbTestIssue)
      {
        itemByValue.Text = Res.Get<NewslettersResources>().SendThisIssue;
        radToolBarDropDown.Buttons.FindButtonByValue("CreateAbTest").Attributes.Add("onclick", "editorToolBar.createAbTest();");
        radToolBarDropDown.Buttons.FindButtonByValue("ScheduleDelivery").Attributes.Add("onclick", "editorToolBar.scheduleCampaign();");
        radToolBarDropDown.Buttons.FindButtonByValue("DeleteCampaign").Attributes.Add("onclick", "editorToolBar.deleteCampaign();");
      }
      else
      {
        itemByValue.Text = Res.Get<NewslettersResources>().SendThisABTest;
        radToolBarDropDown.Buttons.FindButtonByValue("CreateAbTest").Visible = false;
        radToolBarDropDown.Buttons.FindButtonByValue("ScheduleDelivery").Visible = false;
        radToolBarDropDown.Buttons.FindButtonByValue("DeleteCampaign").Visible = false;
      }
      this.baseToolbar.LeftToolBar.FindItemByValue("Properties").Attributes.Add("onclick", "editorToolBar.showTitleAndProperties();");
    }

    /// <inheritdoc />
    protected override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("campaignServiceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Campaign.svc"));
      behaviorDescriptor.AddProperty("campaignId", (object) this.CampaignId);
      behaviorDescriptor.AddProperty("_isAbTestIssue", (object) this.IsAbTestIssue);
      behaviorDescriptor.AddProperty("_isBIssue", (object) this.IsBIssue);
      behaviorDescriptor.AddProperty("_isBIssue", (object) this.IsBIssue);
      behaviorDescriptor.AddProperty("_abTestId", (object) this.AbTestId);
      behaviorDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      behaviorDescriptor.AddComponentProperty("sendTestPrompt", this.SendTestPrompt.ClientID);
      if (!this.IsAbTestIssue)
      {
        behaviorDescriptor.AddComponentProperty("sendIssuePrompt", this.SendIssuePrompt.ClientID);
        behaviorDescriptor.AddComponentProperty("campaignWizardDialog", this.CampaignWizardDialog.ClientID);
        behaviorDescriptor.AddComponentProperty("scheduleDeliveryWindow", this.ScheduleDeliveryWindow.ClientID);
        behaviorDescriptor.AddComponentProperty("deleteConfirmationDialog", this.DeleteConfirmationDialog.ClientID);
      }
      behaviorDescriptor.AddComponentProperty("abTestDetailViewDialog", this.AbTestDetailViewDialog.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    /// <inheritdoc />
    protected override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>();
      string str = typeof (ZoneEditorToolBarExtension).Assembly.GetName().ToString();
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = str,
        Name = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.ZoneEditorToolBarExtension.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
