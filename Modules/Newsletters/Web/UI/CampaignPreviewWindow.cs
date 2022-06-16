// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.CampaignPreviewWindow
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  public class CampaignPreviewWindow : KendoWindow
  {
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.CampaignPreviewWindow.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.CampaignPreviewWindow.ascx");

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CampaignPreviewWindow.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets a reference to the outer div.</summary>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("campaignPreviewWindow", true);

    /// <summary>Gets a reference to the back link.</summary>
    protected virtual LinkButton BackLink => this.Container.GetControl<LinkButton>("backLink", false);

    /// <summary>Gets a reference to the message subject text field.</summary>
    protected virtual TextField MessageSubject => this.Container.GetControl<TextField>("messageSubject", false);

    /// <summary>Gets a reference to from name text field.</summary>
    protected virtual TextField FromName => this.Container.GetControl<TextField>("fromName", false);

    /// <summary>Gets a reference to the reply to email text field.</summary>
    protected virtual TextField ReplyToEmail => this.Container.GetControl<TextField>("replyToEmail", false);

    /// <summary>Gets a reference to the HTML source iframe.</summary>
    protected virtual HtmlControl HtmlSource => this.Container.GetControl<HtmlControl>("htmlSource", false);

    /// <summary>Gets a reference to the plain text source text area.</summary>
    protected virtual HtmlTextArea PlainTextSource => this.Container.GetControl<HtmlTextArea>("plainTextSource", false);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      if (this.BackLink != null)
        controlDescriptor.AddElementProperty("backLink", this.BackLink.ClientID);
      if (this.MessageSubject != null)
        controlDescriptor.AddComponentProperty("messageSubject", this.MessageSubject.ClientID);
      if (this.FromName != null)
        controlDescriptor.AddComponentProperty("fromName", this.FromName.ClientID);
      if (this.ReplyToEmail != null)
        controlDescriptor.AddComponentProperty("replyToEmail", this.ReplyToEmail.ClientID);
      if (this.HtmlSource != null)
        controlDescriptor.AddElementProperty("htmlSource", this.HtmlSource.ClientID);
      if (this.PlainTextSource != null)
        controlDescriptor.AddElementProperty("plainTextSource", this.PlainTextSource.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Scripts.CampaignPreviewWindow.js", typeof (CampaignPreviewWindow).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;
  }
}
