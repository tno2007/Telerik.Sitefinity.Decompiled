// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms
{
  /// <summary>
  /// Dialog for exporting for exporting Sitefinity subscribers
  /// into .csv and .txt formats.
  /// </summary>
  public class ExportSubscribersDialog : KendoWindow
  {
    private const string exportHttpHandlerUrl = "{0}Telerik.Sitefinity.ExportSubscribersHttpHandler.ashx?provName={1}";
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.ExportSubscribersDialog.js";
    private const string subscriberServiceUrl = "~/Sitefinity/Services/Newsletters/Subscriber.svc/";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Forms.ExportSubscribersDialog.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.ExportSubscribersDialog" /> class.
    /// </summary>
    public ExportSubscribersDialog() => this.LayoutTemplatePath = ExportSubscribersDialog.layoutTemplatePath;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>Gets the reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>
    /// Gets the reference to the radio button which marks all subscribers to be exported.
    /// </summary>
    protected virtual RadioButton AllSubscribersRadio => this.Container.GetControl<RadioButton>("allSubscribersRadio", true);

    /// <summary>
    /// Gets the reference to the radio button which marks that only subscribers from selected mailing lists
    /// will be exported.
    /// </summary>
    protected virtual RadioButton SubsFromSelectedListsRadio => this.Container.GetControl<RadioButton>("subsFromSelectedListsRadio", true);

    /// <summary>
    /// Gets the reference to the radio button which marks that only subscribers that do not belong
    /// on any mailing list will be exported.
    /// </summary>
    protected virtual RadioButton SubsNoMailingListRadio => this.Container.GetControl<RadioButton>("subsNoMailingList", true);

    /// <summary>
    /// Gets the reference to the <see cref="T:System.Web.UI.WebControls.CheckBox" /> which indicates whether existing
    /// subscribers should be exported.
    /// </summary>
    protected virtual CheckBox DoNotExportExistingSubscribers => this.Container.GetControl<CheckBox>("doNotExportExistingSubscribers", true);

    /// <summary>
    /// Gets the reference to the button for selecting mailing lists.
    /// </summary>
    protected virtual LinkButton SelectListsButton => this.Container.GetControl<LinkButton>("selectListsButton", true);

    /// <summary>
    /// Gets the reference to the panel for selecting mailing lists.
    /// </summary>
    protected virtual HtmlContainerControl MailingListsPanel => this.Container.GetControl<HtmlContainerControl>("mailingListsPanel", true);

    /// <summary>
    /// Gets the reference to the radio button which marks the source of subscribers
    /// to be a comma separated list of values.
    /// </summary>
    protected virtual RadioButton CommaSeparatedListRadio => this.Container.GetControl<RadioButton>("commaSeparatedListRadio", true);

    /// <summary>
    /// Gets the reference to the radio button which marks the source of subscribers
    /// to be tab separated list of values.
    /// </summary>
    protected virtual RadioButton TabSeparatedListRadio => this.Container.GetControl<RadioButton>("tabSeparatedListRadio", true);

    /// <summary>Gets the reference to the import subscribers button.</summary>
    protected virtual LinkButton ExportSubscribersButton => this.Container.GetControl<LinkButton>("exportSubscribersButton", true);

    /// <summary>Gets the reference to the cancel button.</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("cancelButton", true);

    /// <summary>
    /// Gets the reference to the hidden field containing the list id.
    /// </summary>
    protected virtual HiddenField ListIdsHidden => this.Container.GetControl<HiddenField>("listIdsHidden", true);

    /// <summary>
    /// Gets the reference to the window manager that contains the select Lists Dialog.
    /// </summary>
    protected virtual RadWindowManager WindowManager => this.Container.GetControl<RadWindowManager>("windowManager", true);

    /// <summary>Gets the mailing lists selector window.</summary>
    protected virtual RadWindow SelectListsDialog => this.WindowManager.Windows.Cast<RadWindow>().AsQueryable<RadWindow>().Single<RadWindow>((Expression<Func<RadWindow, bool>>) (w => w.ID == "selectListsDialog"));

    /// <summary>
    /// Gets the reference to the ul that will contain the mailing list names.
    /// </summary>
    protected virtual HtmlContainerControl SelectedListsElement => this.Container.GetControl<HtmlContainerControl>("selectedLists", true);

    /// <summary>
    /// Gets the reference to label of the select lists button.
    /// </summary>
    protected virtual Label SelectListsLabel => this.Container.GetControl<Label>("selectListsLabel", true);

    /// <summary>Gets the reference to client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>Gets the reference to the export subscribers label.</summary>
    protected virtual Label ExportSubscribersLabel => this.Container.GetControl<Label>("exportSubscribersLabel", true);

    /// <summary>
    /// Gets the reference to the div that wraps the export options.
    /// </summary>
    protected virtual HtmlContainerControl ExportOptions => this.Container.GetControl<HtmlContainerControl>("exportOptions", true);

    /// <summary>Gets a reference to the outer div.</summary>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("exportSubscribersDialog", true);

    /// <summary>
    /// Gets a reference to the hidden url that will contain the url for the subscriber service.
    /// </summary>
    protected virtual HiddenField SubscribersServiceUrlHidden => this.Container.GetControl<HiddenField>("subscribersServiceUrlHidden", true);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("allSubscribersRadio", this.AllSubscribersRadio.ClientID);
      controlDescriptor.AddElementProperty("commaSeparatedListRadio", this.CommaSeparatedListRadio.ClientID);
      controlDescriptor.AddElementProperty("tabSeparatedListRadio", this.TabSeparatedListRadio.ClientID);
      controlDescriptor.AddElementProperty("subsFromSelectedListsRadio", this.SubsFromSelectedListsRadio.ClientID);
      controlDescriptor.AddElementProperty("subsNoMailingListRadio", this.SubsNoMailingListRadio.ClientID);
      controlDescriptor.AddElementProperty("mailingListsPanel", this.MailingListsPanel.ClientID);
      controlDescriptor.AddElementProperty("exportSubscribersButton", this.ExportSubscribersButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("selectListsButton", this.SelectListsButton.ClientID);
      controlDescriptor.AddComponentProperty("windowManager", this.WindowManager.ClientID);
      controlDescriptor.AddComponentProperty("selectListsDialog", this.SelectListsDialog.ClientID);
      controlDescriptor.AddElementProperty("selectedListsElement", this.SelectedListsElement.ClientID);
      controlDescriptor.AddElementProperty("selectListsLabel", this.SelectListsLabel.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddElementProperty("listIdsHidden", this.ListIdsHidden.ClientID);
      controlDescriptor.AddElementProperty("exportSubscribersLabel", this.ExportSubscribersLabel.ClientID);
      controlDescriptor.AddElementProperty("exportOptions", this.ExportOptions.ClientID);
      controlDescriptor.AddElementProperty("doNotExportExistingSubscribers", this.DoNotExportExistingSubscribers.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      string str1 = string.Format("{0}Telerik.Sitefinity.ExportSubscribersHttpHandler.ashx?provName={1}", (object) NewslettersManager.GetRootUrl(), (object) this.ProviderName);
      controlDescriptor.AddProperty("_exportHttpHandlerUrl", (object) str1);
      string str2 = this.Page.ResolveUrl("~/Sitefinity/Services/Newsletters/Subscriber.svc/");
      controlDescriptor.AddProperty("_subscriberServiceUrl", (object) str2);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Forms.Scripts.ExportSubscribersDialog.js", typeof (ExportSubscribersDialog).Assembly.FullName)
    };
  }
}
