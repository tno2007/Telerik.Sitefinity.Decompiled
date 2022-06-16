// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>A dialog for sharing a specific item with sites.</summary>
  public class ShareItemDialog : AjaxDialogBase
  {
    private string getSharedSitesUrl;
    private string setSharedSitesUrl;
    /// <summary>
    /// Gets the name of resource file representing the dialog.
    /// </summary>
    private static readonly string DialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.ShareItemDialog.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.ShareItemDialog.js";
    private const string kendoScriptRef = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.ShareItemDialog" /> class.
    /// </summary>
    public ShareItemDialog() => this.LayoutTemplatePath = ShareItemDialog.DialogTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the type of the client component.</summary>
    /// <value>The type of the client component.</value>
    public override string ClientComponentType => typeof (ShareItemDialog).FullName;

    /// <summary>Gets the title label.</summary>
    protected virtual ITextControl TitleLabel => this.Container.GetControl<ITextControl>("lblTitle", false);

    /// <summary>Gets the cancel button.</summary>
    protected virtual LinkButton CancelButton => this.Container.GetControl<LinkButton>("lnkCancel", true);

    /// <summary>Gets the done button.</summary>
    protected virtual LinkButton DoneButton => this.Container.GetControl<LinkButton>("lnkDone", true);

    /// <summary>Gets the sites grid.</summary>
    protected virtual HtmlTable SitesGrid => this.Container.GetControl<HtmlTable>("sitesGrid", true);

    /// <summary>Gets the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the buttons panel.</summary>
    protected virtual HtmlContainerControl ButtonsPanel => this.Container.GetControl<HtmlContainerControl>("buttonAreaPanel", true);

    /// <summary>Gets the loading view.</summary>
    protected virtual HtmlContainerControl LoadingView => this.Container.GetControl<HtmlContainerControl>("loadingView", true);

    /// <summary>Gets the search box.</summary>
    protected virtual BackendSearchBox SearchBox => this.Container.GetControl<BackendSearchBox>("searchBox", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      string str = this.Page.Request.QueryString["itemId"];
      if (this.TitleLabel != null)
        this.TitleLabel.Text = Res.Get(this.Page.Request.QueryString["resourceClassId"], this.Page.Request.QueryString["title"]);
      this.getSharedSitesUrl = string.Format(this.Page.Request.QueryString["getSharedSitesUrl"], (object) str);
      this.setSharedSitesUrl = string.Format(this.Page.Request.QueryString["setSharedSitesUrl"], (object) str);
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
      controlDescriptor.AddElementProperty("cancelButton", this.CancelButton.ClientID);
      controlDescriptor.AddElementProperty("doneButton", this.DoneButton.ClientID);
      controlDescriptor.AddProperty("_getSharedSitesUrl", (object) this.getSharedSitesUrl);
      controlDescriptor.AddProperty("_setSharedSitesUrl", (object) this.setSharedSitesUrl);
      controlDescriptor.AddElementProperty("sitesGrid", this.SitesGrid.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("buttonsPanel", this.ButtonsPanel.ClientID);
      controlDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
      controlDescriptor.AddComponentProperty("searchBox", this.SearchBox.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName),
      new ScriptReference()
      {
        Assembly = typeof (ShareItemDialog).Assembly.FullName,
        Name = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.ShareItemDialog.js"
      }
    };
  }
}
