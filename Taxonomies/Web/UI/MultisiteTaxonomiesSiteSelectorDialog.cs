// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>
  /// Dialog for selecting sites used to share specific taxonomy
  /// </summary>
  public class MultisiteTaxonomiesSiteSelectorDialog : AjaxDialogBase
  {
    private readonly string dialogLayoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Taxonomies.MultisiteTaxonomiesSiteSelectorDialog.ascx");
    internal const string ScriptReference = "Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.MultisiteTaxonomiesSiteSelectorDialog.js";
    private readonly string mutlisiteWebServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Multisite/Multisite.svc/");
    private readonly string shareTaxonomyWebServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Taxonomies/Taxonomy.svc/share/{0}/?provider=");
    private string taxonomyId;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.MultisiteTaxonomiesSiteSelectorDialog" /> class.
    /// </summary>
    public MultisiteTaxonomiesSiteSelectorDialog() => this.LayoutTemplatePath = this.dialogLayoutTemplatePath;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (MultisiteTaxonomiesSiteSelectorDialog).FullName;

    /// <summary>Gets the sites selector.</summary>
    protected virtual FlatSelector SiteSelector => this.Container.GetControl<FlatSelector>("siteSelector", true);

    /// <summary>Gets a reference to the proceed button.</summary>
    /// <value>The done button.</value>
    protected virtual LinkButton ProceedButton => this.Container.GetControl<LinkButton>("proceedButton", true);

    /// <summary>Gets a reference to the cancel button.</summary>
    /// <value>The cancel button.</value>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("cancelButton", true);

    /// <summary>
    /// Gets a reference to the done button in the confirmation view.
    /// </summary>
    /// <value>The done button.</value>
    protected virtual LinkButton ConfirmationButtonDone => this.Container.GetControl<LinkButton>("confirmationViewButtonDone", true);

    /// <summary>
    /// Gets a reference to the cancel button in the confirmation view.
    /// </summary>
    /// <value>The cancel button.</value>
    protected virtual LinkButton ConfirmationButtonCancel => this.Container.GetControl<LinkButton>("confirmationViewButtonCancel", true);

    /// <summary>Gets the title label.</summary>
    protected virtual ITextControl TitleLabel => this.Container.GetControl<ITextControl>("lblTitleShareView", false);

    /// <summary>Gets the confirmation label.</summary>
    protected virtual Label ConfirmationLabel => this.Container.GetControl<Label>("lblConfirmation", true);

    /// <summary>Gets the warning literal.</summary>
    protected virtual Literal WarningLiteral => this.Container.GetControl<Literal>("ltrWarning", true);

    /// <summary>Gets the share taxonomy by site confirmation literal.</summary>
    protected virtual Literal ShareTaxonomyBySiteConfirmationLiteral => this.Container.GetControl<Literal>("ltrShareTaxonomyBySiteConfirmation", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("siteSelector", this.SiteSelector.ClientID);
      controlDescriptor.AddElementProperty("proceedButton", this.ProceedButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("confirmationButtonDone", this.ConfirmationButtonDone.ClientID);
      controlDescriptor.AddElementProperty("confirmationButtonCancel", this.ConfirmationButtonCancel.ClientID);
      controlDescriptor.AddProperty("webServiceUrl", (object) string.Format(this.shareTaxonomyWebServiceUrl, (object) this.taxonomyId));
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
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.MultisiteTaxonomiesSiteSelectorDialog.js", typeof (MultisiteTaxonomiesSiteSelectorDialog).Assembly.FullName)
    };

    /// <summary>
    /// Initializes the MultisiteTaxonomiesSiteSelectorDialog.
    /// </summary>
    /// <param name="container">Template container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.SiteSelector.ServiceUrl = this.mutlisiteWebServiceUrl + "user/" + SecurityManager.GetCurrentUserId().ToString() + "/sites/?sortExpression=Name";
      this.taxonomyId = this.Page.Request.QueryString["taxonomyId"];
      this.SetDialogMessages(new Guid(this.taxonomyId));
    }

    /// <summary>Sets the dialog messages.</summary>
    /// <param name="taxonomyId">The taxonomy identifier.</param>
    private void SetDialogMessages(Guid taxonomyId)
    {
      string lower = TaxonomyManager.GetManager(this.Page.Request.QueryString["providerName"]).GetTaxonomy(taxonomyId).Title.ToLower();
      Type type = typeof (TaxonomyResources);
      if (this.TitleLabel != null)
        this.TitleLabel.Text = string.Format(Res.Get(type, "UseTaxonomyFrom"), (object) lower);
      if (this.ConfirmationLabel != null)
        this.ConfirmationLabel.Text = string.Format(Res.Get(type, "ShareTaxonomyBySitesConfirmation"), (object) lower);
      if (this.WarningLiteral != null)
        this.WarningLiteral.Text = string.Format(Res.Get(type, "ShareTaxonomyBySitesWarning"), (object) lower);
      if (this.ShareTaxonomyBySiteConfirmationLiteral == null)
        return;
      this.ShareTaxonomyBySiteConfirmationLiteral.Text = string.Format(Res.Get(type, "ShareTaxonomyBySiteConfirmationButtonLabel"), (object) lower);
    }
  }
}
