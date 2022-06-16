// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public class PageViewVersionDialog : AjaxDialogBase
  {
    /// <summary>Path of the layout tempalte</summary>
    public static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.PageViewVersionDialog.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog" /> class.
    /// </summary>
    public PageViewVersionDialog() => this.LayoutTemplatePath = PageViewVersionDialog.TemplatePath;

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a reference to the window manager on the template.
    /// </summary>
    /// <value>The window manager.</value>
    private ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    private PromptDialog RevertPrompt => this.Container.GetControl<PromptDialog>("revertPrompt", true);

    /// <summary>Gets the back button.</summary>
    /// <value>The back button.</value>
    public virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    private HtmlAnchor BackLabel => this.Container.GetControl<HtmlAnchor>("backLabel", true);

    private bool IsTemplate => this.Page.Request.QueryString[nameof (IsTemplate)].ToLower() == "true";

    private bool IsFormEditor => this.Page.Request.QueryString["IsFromEditor"].ToLower() == "true";

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      int num = this.IsFormEditor ? 1 : 0;
      if (this.IsTemplate)
      {
        behaviorDescriptor.AddProperty("pageServiceUrl", (object) (this.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/") + "Templates/"));
        behaviorDescriptor.AddProperty("versionUrlQueryString", (object) "IsTemplate=true");
      }
      else
      {
        behaviorDescriptor.AddProperty("pageServiceUrl", (object) (this.ResolveUrl("~/Sitefinity/Services/Pages/PagesService.svc/") + "Pages/"));
        behaviorDescriptor.AddProperty("versionUrlQueryString", (object) "IsTemplate=false");
      }
      behaviorDescriptor.AddProperty("versionPageUrl", (object) this.ResolveUrl("~/Sitefinity/Versioning/"));
      behaviorDescriptor.AddProperty("versionServiceUrl", (object) this.ResolveUrl("~/Sitefinity/Services/Versioning/HistoryService.svc/"));
      behaviorDescriptor.AddComponentProperty("revertPrompt", this.RevertPrompt.ClientID);
      behaviorDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      behaviorDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      if (this.IsFormEditor)
        this.BackLabel.InnerHtml = Res.Get<PageResources>().BackToRevisionHistory;
      return ((IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      }).Union<ScriptDescriptor>(base.GetScriptDescriptors());
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return ((IEnumerable<ScriptReference>) new ScriptReference[2]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.Scripts.ClientManager.js"
        },
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PageViewVersionDialog.js"
        }
      }).Union<ScriptReference>(base.GetScriptReferences());
    }
  }
}
