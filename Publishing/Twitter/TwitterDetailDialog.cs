// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Publishing.Twitter
{
  public class TwitterDetailDialog : AjaxDialogBase
  {
    private ITwitterApplication app;
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.Twitter.TwitterDetailDialog.ascx");
    private const string scriptPath = "Telerik.Sitefinity.Publishing.Twitter.Scripts.TwitterDetailDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog" /> class.
    /// </summary>
    public TwitterDetailDialog() => this.LayoutTemplatePath = TwitterDetailDialog.UiPath;

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      if (SystemManager.CurrentHttpContext == null)
        return;
      string key = SystemManager.CurrentHttpContext.Request.QueryString["AppName"];
      if (string.IsNullOrEmpty(key))
        return;
      this.app = new TwitterConfigCredentialsManager().Applications[key];
    }

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string ClientComponentType => typeof (TwitterDetailDialog).FullName;

    public virtual LinkButton SaveButton => this.Container.GetControl<LinkButton>("saveButton", true);

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", typeof (TwitterDetailDialog).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Publishing.Twitter.Scripts.TwitterDetailDialog.js", typeof (TwitterDetailDialog).Assembly.FullName)
    };

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      if (this.app != null)
        controlDescriptor.AddProperty("app", (object) new WcfTwitterApplication(this.app));
      controlDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary);
      controlDescriptor.AddProperty("serviceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Twitter/TwitterCredentialsService.svc"));
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }
  }
}
