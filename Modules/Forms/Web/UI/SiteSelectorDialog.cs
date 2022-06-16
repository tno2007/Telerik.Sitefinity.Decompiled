// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  public class SiteSelectorDialog : AjaxDialogBase
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Forms.FormsSiteSelectorDialog.ascx");
    private static readonly string scriptPath = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.SiteSelectorDialog.js";
    private static readonly string mutlisiteWebServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Multisite/Multisite.svc/");
    private static readonly string formShareWebService = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Forms/FormsService.svc/share/");
    private static readonly string ItemIdParamaterName = "ItemId";
    private static readonly string ProviderNameParameterName = "provider";
    private static readonly string SiteSelectorKeyName = "Id";
    private IEnumerable<Guid> existingLinks;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.SiteSelectorDialog" /> class.
    /// </summary>
    public SiteSelectorDialog() => this.LayoutTemplatePath = SiteSelectorDialog.layoutTemplatePath;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (SiteSelectorDialog).FullName;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the Id of the selected form</summary>
    public Guid FormId
    {
      get
      {
        string input = this.Page.Request.QueryString[SiteSelectorDialog.ItemIdParamaterName];
        Guid result;
        return string.IsNullOrEmpty(input) || !Guid.TryParse(input, out result) ? Guid.Empty : result;
      }
    }

    /// <summary>Gets the current form provider name</summary>
    public string ProviderName => this.Page.Request.QueryString[SiteSelectorDialog.ProviderNameParameterName];

    public string[] ExistingLinks
    {
      get
      {
        if (this.existingLinks == null)
          this.existingLinks = (IEnumerable<Guid>) FormsManager.GetManager(this.ProviderName).GetSiteFormLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == this.FormId)).Select<SiteItemLink, Guid>((Expression<Func<SiteItemLink, Guid>>) (l => l.SiteId));
        return this.existingLinks.Select<Guid, string>((Func<Guid, string>) (l => l.ToString())).ToArray<string>();
      }
    }

    /// <summary>Gets the sites selector.</summary>
    protected virtual FlatSelector SiteSelector => this.Container.GetControl<FlatSelector>(nameof (SiteSelector), true);

    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>(nameof (DoneButton), true);

    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>(nameof (CancelButton), true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.SetupSiteSelector();

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) this.GetBaseScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("siteSelector", this.SiteSelector.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddProperty("formId", (object) this.FormId.ToString());
      controlDescriptor.AddProperty("webServiceUrl", (object) string.Format("{0}?providerName={1}", (object) SiteSelectorDialog.formShareWebService, (object) this.ProviderName));
      controlDescriptor.AddProperty("exisitngLinks", (object) this.ExistingLinks);
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
      List<ScriptReference> scriptReferences = new List<ScriptReference>(this.GetBaseScriptReferences());
      string fullName = typeof (SiteSelectorDialog).Assembly.FullName;
      scriptReferences.Add(new ScriptReference(SiteSelectorDialog.scriptPath, fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    private void SetupSiteSelector()
    {
      this.SiteSelector.DataKeyNames = SiteSelectorDialog.SiteSelectorKeyName;
      this.SetWebServiceUrl();
    }

    private void SetWebServiceUrl() => this.SiteSelector.ServiceUrl = SiteSelectorDialog.mutlisiteWebServiceUrl + "user/" + SecurityManager.GetCurrentUserId().ToString() + "/sites/?sortExpression=Name";

    internal virtual IEnumerable<ScriptDescriptor> GetBaseScriptDescriptors() => base.GetScriptDescriptors();

    internal virtual IEnumerable<ScriptReference> GetBaseScriptReferences() => base.GetScriptReferences();
  }
}
