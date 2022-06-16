// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.Designers.FeedEmbedControlDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Utilities.Json;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Web.UI.PublicControls.Designers
{
  public class FeedEmbedControlDesigner : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.EmbedControls.FeedEmbedControlDesigner.ascx");
    private string FeedEmbedControlDesignerJS = "Telerik.Sitefinity.Web.UI.PublicControls.Designers.Scripts.FeedEmbedControlDesigner.js";
    private const string JqueryUIScript = "Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js";

    /// <summary>Represents the text filed that holds the CSS styles</summary>
    protected internal virtual FlatSelector FeedSelector => this.Container.GetControl<FlatSelector>("feedSelector", true);

    /// <summary>Gets the feed selection panel.</summary>
    /// <value>The feed selection panel.</value>
    protected internal virtual Panel FeedSelectionPanel => this.Container.GetControl<Panel>("pnlSelectFeed", true);

    /// <summary>Gets the "no feed selected" label control.</summary>
    /// <value>The "no feed selected" label control.</value>
    protected internal virtual Label NoFeedSelectedLabel => this.Container.GetControl<Label>("noFeedSelected", true);

    /// <summary>Gets the selected feed title label.</summary>
    /// <value>The selected feed title label.</value>
    protected internal virtual Label SelectedFeedTitle => this.Container.GetControl<Label>("selectedFeedTitle", true);

    /// <summary>Gets the [select feed] button.</summary>
    /// <value>The [select feed] button.</value>
    protected internal virtual LinkButton SelectFeedButton => this.Container.GetControl<LinkButton>("selectFeedButton", true);

    /// <summary>Gets the [done selecting feed] button.</summary>
    /// <value>The [done selecting feed] button.</value>
    protected internal virtual LinkButton DoneSelectingFeedButton => this.Container.GetControl<LinkButton>("lnkDoneSelectingFeed", true);

    /// <summary>Gets the [cancel selecting] feed button.</summary>
    /// <value>The cancel selecting feed button.</value>
    protected internal virtual LinkButton CancelSelectingFeedButton => this.Container.GetControl<LinkButton>("cancelSelectingFeed", true);

    /// <summary>
    /// Gets the "links in page and address bar" radio button.
    /// </summary>
    /// <value>The "links in page and address bar" radio button.</value>
    protected internal virtual RadioButton RadioLinksInPageAndAddressBar => this.Container.GetControl<RadioButton>("radioLinksInPageAndAddressBar", true);

    /// <summary>
    /// Gets the "link in the browser address bar only" radio button.
    /// </summary>
    /// <value>The "link in the browser address bar only" radio button.</value>
    protected internal virtual RadioButton RadioLinkInTheBrowserAddressBarOnly => this.Container.GetControl<RadioButton>("radioLinkInTheBrowserAddressBarOnly", true);

    /// <summary>Gets the "link in the page only" radio button.</summary>
    /// <value>The "link in the page only" radio button.</value>
    protected internal virtual RadioButton RadioLinkInThePageOnly => this.Container.GetControl<RadioButton>("radioLinkInThePageOnly", true);

    /// <summary>Gets the feed text to display textbox.</summary>
    /// <value>The feed text to display textbox.</value>
    protected internal virtual TextBox TextToDisplay => this.Container.GetControl<TextBox>("txtTextToDisplay", true);

    /// <summary>Gets the "big icon" radio button.</summary>
    /// <value>The "big icon" radio button.</value>
    protected internal virtual RadioButton RadioBigIcon => this.Container.GetControl<RadioButton>("radioBigIcon", true);

    /// <summary>Gets the "small icon" radio button.</summary>
    /// <value>The "big icon" radio button.</value>
    protected internal virtual RadioButton RadioSmallIcon => this.Container.GetControl<RadioButton>("radioSmallIcon", true);

    /// <summary>Gets the "no icon" radio button.</summary>
    /// <value>The "no icon" radio button.</value>
    protected internal virtual RadioButton RadioNoIcon => this.Container.GetControl<RadioButton>("radioNoIcon", true);

    /// <summary>Gets the "more options" button.</summary>
    /// <value>The "more options" button.</value>
    protected internal virtual LinkButton MoreOptionsButton => this.Container.GetControl<LinkButton>("btnMoreOptions", true);

    /// <summary>Gets the tooltip textbox.</summary>
    /// <value>The tooltip textbox.</value>
    protected internal virtual TextBox Tooltip => this.Container.GetControl<TextBox>("txtTooltip", true);

    /// <summary>Gets the CSS Class textbox.</summary>
    /// <value>The CSS Class textbox.</value>
    protected internal virtual TextBox CssClassTextBox => this.Container.GetControl<TextBox>("txtCssClass", true);

    /// <summary>Gets the "open in new window" checkbox.</summary>
    /// <value>The "open in new window" checkbox.</value>
    protected internal virtual CheckBox OpenInNewWindow => this.Container.GetControl<CheckBox>("chkOpenInNewWindow", true);

    /// <summary>Gets the more options panel.</summary>
    /// <value>The more options panel.</value>
    protected internal virtual Panel MoreOptionsPanel => this.Container.GetControl<Panel>("pnlMoreOptions", true);

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FeedEmbedControlDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.FeedSelector.AllowSearching = true;
      this.FeedSelector.AllowPaging = true;
      this.FeedSelector.PageSize = 20;
      this.FeedSelector.ServiceUrl = RouteHelper.ResolveUrl(string.Format("~/Sitefinity/Services/Publishing/PublishingService.svc/pipes/?providerName={0}&pipeTypeName={1}", (object) "OAPublishingProvider", (object) "RSSOutboundPipe"), UrlResolveOptions.Rooted);
      this.FeedSelector.ItemType = typeof (PipeSettings).AssemblyQualifiedName;
      this.FeedSelector.DataKeyNames = "ID";
      this.FeedSelector.BindOnLoad = false;
      this.FeedSelector.AllowMultipleSelection = false;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddProperty("_feedSelectionPanelID", (object) this.FeedSelectionPanel.ClientID);
      controlDescriptor.AddProperty("_feedSelectorID", (object) this.FeedSelector.ClientID);
      controlDescriptor.AddProperty("_noFeedSelectedLabelID", (object) this.NoFeedSelectedLabel.ClientID);
      controlDescriptor.AddProperty("_selectedFeedTitleID", (object) this.SelectedFeedTitle.ClientID);
      controlDescriptor.AddProperty("_selectFeedButtonID", (object) this.SelectFeedButton.ClientID);
      controlDescriptor.AddProperty("_doneSelectingFeedButtonID", (object) this.DoneSelectingFeedButton.ClientID);
      controlDescriptor.AddProperty("_cancelSelectingFeedButtonID", (object) this.CancelSelectingFeedButton.ClientID);
      controlDescriptor.AddProperty("_radioLinksInPageAndAddressBarID", (object) this.RadioLinksInPageAndAddressBar.ClientID);
      controlDescriptor.AddProperty("_radioLinkInTheBrowserAddressBarOnlyID", (object) this.RadioLinkInTheBrowserAddressBarOnly.ClientID);
      controlDescriptor.AddProperty("_radioLinkInThePageOnlyID", (object) this.RadioLinkInThePageOnly.ClientID);
      controlDescriptor.AddProperty("_textToDisplayID", (object) this.TextToDisplay.ClientID);
      controlDescriptor.AddProperty("_radioBigIconID", (object) this.RadioBigIcon.ClientID);
      controlDescriptor.AddProperty("_radioSmallIconID", (object) this.RadioSmallIcon.ClientID);
      controlDescriptor.AddProperty("_radioNoIconID", (object) this.RadioNoIcon.ClientID);
      controlDescriptor.AddProperty("_moreOptionsButtonID", (object) this.MoreOptionsButton.ClientID);
      controlDescriptor.AddProperty("_tooltipID", (object) this.Tooltip.ClientID);
      controlDescriptor.AddProperty("_cssClassTextBoxID", (object) this.CssClassTextBox.ClientID);
      controlDescriptor.AddProperty("_openInNewWindowID", (object) this.OpenInNewWindow.ClientID);
      controlDescriptor.AddProperty("_moreOptionsPanelID", (object) this.MoreOptionsPanel.ClientID);
      controlDescriptor.AddProperty("_feedEmbedControlEnum", (object) JsonUtility.EnumToJson(typeof (FeedEmbedControl.LinkInsertionMethod)));
      controlDescriptor.AddProperty("_iconSizeEnum", (object) JsonUtility.EnumToJson(typeof (FeedEmbedControl.IconSize)));
      controlDescriptor.AddProperty("_selectAFeedText", (object) Res.Get<PublicControlsResources>().SelectAFeed);
      controlDescriptor.AddProperty("_changeSelectedFeedText", (object) Res.Get<Labels>().ChangeBtnIn);
      controlDescriptor.AddElementProperty("feedSelectionPanel", this.FeedSelectionPanel.ClientID);
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
      string assembly = typeof (FeedEmbedControlDesigner).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference(this.FeedEmbedControlDesignerJS, assembly),
        new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources")
      };
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
