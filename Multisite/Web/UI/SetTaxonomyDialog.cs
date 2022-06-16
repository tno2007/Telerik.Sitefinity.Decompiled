// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>A dialog for sharing a specific item with sites.</summary>
  public class SetTaxonomyDialog : AjaxDialogBase
  {
    private Guid taxonomyId;
    private Guid currentSiteTaxonomyId;
    private TaxonomyManager manager;
    /// <summary>
    /// Gets the name of resource file representing the dialog.
    /// </summary>
    private static readonly string DialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.SetTaxonomyDialog.ascx");
    internal const string ScriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.SetTaxonomyDialog.js";
    private const string TaxonomyServiceUrl = "~/Sitefinity/Services/Taxonomies/Taxonomy.svc";
    private const string KendoScriptRef = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SetTaxonomyDialog" /> class.
    /// </summary>
    public SetTaxonomyDialog() => this.LayoutTemplatePath = SetTaxonomyDialog.DialogTemplatePath;

    /// <summary>Gets the taxonomy identifier.</summary>
    public Guid TaxonomyId
    {
      get
      {
        if (this.taxonomyId == Guid.Empty)
          this.taxonomyId = new Guid(this.Page.Request.QueryString["itemId"]);
        return this.taxonomyId;
      }
    }

    /// <summary>Gets the current site taxonomy identifier.</summary>
    public Guid CurrentSiteTaxonomyId
    {
      get
      {
        if (this.currentSiteTaxonomyId == Guid.Empty)
          this.currentSiteTaxonomyId = new Guid(this.Page.Request.QueryString["currentSiteTaxonomyId"]);
        return this.currentSiteTaxonomyId;
      }
    }

    /// <summary>Gets the manager.</summary>
    public TaxonomyManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = TaxonomyManager.GetManager(this.Page.Request.QueryString["provider"]);
        return this.manager;
      }
    }

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (SetTaxonomyDialog).FullName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the loading view.</summary>
    protected virtual HtmlContainerControl LoadingView => this.Container.GetControl<HtmlContainerControl>("loadingView", true);

    /// <summary>Gets the title label.</summary>
    protected virtual ITextControl TitleLabel => this.Container.GetControl<ITextControl>("lblTitle", false);

    /// <summary>Gets the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the done button.</summary>
    protected virtual HtmlContainerControl DoneButton => this.Container.GetControl<HtmlContainerControl>("buttonDone", true);

    /// <summary>Gets the cancel button.</summary>
    protected virtual HtmlContainerControl CancelButton => this.Container.GetControl<HtmlContainerControl>("buttonCancel", true);

    /// <summary>Gets the uses taxonomy shared label.</summary>
    protected virtual Literal UsesTaxonomySharedWithLiteral => this.Container.GetControl<Literal>("ltrUsesTaxonomySharedWith", true);

    /// <summary>Gets the other site RadioButton.</summary>
    protected virtual RadioButton OtherSitesRadioButton => this.Container.GetControl<RadioButton>("rbtnOtherSites", true);

    /// <summary>Gets the other site panel.</summary>
    /// <value>The other site panel.</value>
    protected virtual Panel OtherSitesPanel => this.Container.GetControl<Panel>("pnlOtherSites", true);

    /// <summary>Gets the other sites literal.</summary>
    protected virtual HtmlContainerControl OtherSitesLiteral => this.Container.GetControl<HtmlContainerControl>("spanOtherSites", true);

    /// <summary>Gets the change site button.</summary>
    protected virtual HtmlContainerControl ChangeSiteButton => this.Container.GetControl<HtmlContainerControl>("buttonChangeSite", true);

    /// <summary>Gets the current site RadioButton.</summary>
    protected virtual RadioButton CurrentSiteRadioButton => this.Container.GetControl<RadioButton>("rbtnCurrentSite", true);

    /// <summary>Gets the duplicate CheckBox.</summary>
    protected virtual CheckBox DuplicateCheckBox => this.Container.GetControl<CheckBox>("chbDuplicate", true);

    /// <summary>Gets the title share view label.</summary>
    protected virtual ITextControl TitleShareViewLabel => this.Container.GetControl<ITextControl>("lblTitleShareView", false);

    /// <summary>Gets the sites grid.</summary>
    protected virtual HtmlTable SitesGrid => this.Container.GetControl<HtmlTable>("sitesGrid", true);

    /// <summary>Gets the done button from share view.</summary>
    protected virtual HtmlContainerControl DoneShareViewButton => this.Container.GetControl<HtmlContainerControl>("buttonDoneShareView", true);

    /// <summary>Gets the cancel button from share view.</summary>
    protected virtual HtmlContainerControl CancelShareViewButton => this.Container.GetControl<HtmlContainerControl>("buttonCancelShareView", true);

    /// <summary>Gets the secondary done button.</summary>
    protected virtual HtmlContainerControl SecondaryDoneButton => this.Container.GetControl<HtmlContainerControl>("secondaryButtonDone", true);

    /// <summary>Gets the secondary cancel button.</summary>
    protected virtual HtmlContainerControl SecondaryCancelButton => this.Container.GetControl<HtmlContainerControl>("secondaryButtonCancel", true);

    /// <summary>Gets the secondary done literal.</summary>
    protected virtual Literal SecondaryDoneLiteral => this.Container.GetControl<Literal>("ltrSecondaryDone", true);

    /// <summary>Gets the confirm message literal.</summary>
    protected virtual Literal ConfirmMessageLiteral => this.Container.GetControl<Literal>("ltrConfirmMessage", true);

    /// <summary>Gets the warning message panel.</summary>
    protected virtual Panel WarningMessagePanel => this.Container.GetControl<Panel>("pnlWarningMessage", true);

    /// <summary>Gets the applied taxonomy will removed literal.</summary>
    protected virtual Literal AppliedTaxonomyWillRemovedLiteral => this.Container.GetControl<Literal>("ltrAppliedTaxonomyWillRemoved", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      bool flag = this.Manager.GetRelatedSites(this.Manager.GetTaxonomy(MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(this.Manager).ResolveSiteTaxonomyId(this.TaxonomyId))).Count<ISite>() > 1;
      controlDescriptor.AddProperty("taxonomyServiceUrl", (object) this.ResolveClientUrl("~/Sitefinity/Services/Taxonomies/Taxonomy.svc"));
      controlDescriptor.AddProperty("taxonomyId", (object) this.TaxonomyId);
      controlDescriptor.AddProperty("currentSiteTaxonomyId", (object) this.CurrentSiteTaxonomyId);
      controlDescriptor.AddProperty("taxonomyProviderName", (object) this.Manager.Provider.Name);
      controlDescriptor.AddProperty("taxonomyIsShared", (object) flag);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddElementProperty("buttonDone", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("buttonCancel", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("otherSitesRadio", this.OtherSitesRadioButton.ClientID);
      controlDescriptor.AddElementProperty("spanOtherSites", this.OtherSitesLiteral.ClientID);
      controlDescriptor.AddElementProperty("buttonChangeSite", this.ChangeSiteButton.ClientID);
      controlDescriptor.AddElementProperty("currentSiteRadio", this.CurrentSiteRadioButton.ClientID);
      controlDescriptor.AddElementProperty("otherSitesPanel", this.OtherSitesPanel.ClientID);
      controlDescriptor.AddElementProperty("duplicateCheckbox", this.DuplicateCheckBox.ClientID);
      controlDescriptor.AddElementProperty("sitesGrid", this.SitesGrid.ClientID);
      controlDescriptor.AddElementProperty("buttonDoneShareView", this.DoneShareViewButton.ClientID);
      controlDescriptor.AddElementProperty("buttonCancelShareView", this.CancelShareViewButton.ClientID);
      controlDescriptor.AddElementProperty("secondaryButtonDone", this.SecondaryDoneButton.ClientID);
      controlDescriptor.AddElementProperty("secondaryButtonCancel", this.SecondaryCancelButton.ClientID);
      controlDescriptor.AddElementProperty("warningMessagePanel", this.WarningMessagePanel.ClientID);
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
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName),
      new System.Web.UI.ScriptReference()
      {
        Assembly = typeof (SetTaxonomyDialog).Assembly.FullName,
        Name = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.SetTaxonomyDialog.js"
      }
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer">The dialog container.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      Type type = typeof (TaxonomyResources);
      string str = HttpUtility.HtmlEncode(this.Manager.GetTaxonomy(this.TaxonomyId).Title.ToLower());
      if (this.TitleLabel != null)
        this.TitleLabel.Text = string.Format(Res.Get(type, "SetTaxonomyForThisSite"), (object) str);
      if (this.TitleShareViewLabel != null)
        this.TitleShareViewLabel.Text = string.Format(Res.Get(type, "UseTaxonomyFrom"), (object) str);
      if (this.UsesTaxonomySharedWithLiteral != null)
        this.UsesTaxonomySharedWithLiteral.Text = string.Format(Res.Get(type, "ThisSiteUsesTaxonomySharedWith"), (object) str);
      if (this.DuplicateCheckBox != null)
        this.DuplicateCheckBox.Text = string.Format(Res.Get(type, "DuplicateSurrentlyTaxonomy"), (object) str);
      if (this.ConfirmMessageLiteral != null)
        this.ConfirmMessageLiteral.Text = string.Format(Res.Get(type, "AreYouSureYouWantToChangeTheTaxonomy"), (object) str);
      if (this.AppliedTaxonomyWillRemovedLiteral != null)
        this.AppliedTaxonomyWillRemovedLiteral.Text = string.Format(Res.Get(type, "AppliedTaxonomyWillRemoved"), (object) str);
      if (this.SecondaryDoneLiteral == null)
        return;
      this.SecondaryDoneLiteral.Text = string.Format(Res.Get(type, "ChangeTaxonomy"), (object) str);
    }
  }
}
