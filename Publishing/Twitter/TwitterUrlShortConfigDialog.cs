// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Publishing.Twitter
{
  public class TwitterUrlShortConfigDialog : AjaxDialogBase
  {
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Publishing.Twitter.TwitterUrlShortConfigDialog.ascx");
    internal const string scriptPath = "Telerik.Sitefinity.Publishing.Twitter.Scripts.TwitterUrlShortConfigDialog.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog" /> class.
    /// </summary>
    public TwitterUrlShortConfigDialog() => this.LayoutTemplatePath = TwitterUrlShortConfigDialog.UiPath;

    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override string ClientComponentType => typeof (TwitterUrlShortConfigDialog).FullName;

    /// <inheritdoc />
    public virtual LinkButton SaveButton => this.Container.GetControl<LinkButton>("saveButton", true);

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      string fullName = typeof (TwitterUrlShortConfigDialog).Assembly.FullName;
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Web.Scripts.ClientManager.js", fullName));
      scriptReferences.Add(new ScriptReference("Telerik.Sitefinity.Publishing.Twitter.Scripts.TwitterUrlShortConfigDialog.js", fullName));
      return (IEnumerable<ScriptReference>) scriptReferences;
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      foreach (FieldControl fieldControl in this.Container.GetControls<FieldControl>().Values)
      {
        if (!string.IsNullOrEmpty(fieldControl.DataFieldName))
          dictionary.Add(fieldControl.DataFieldName, fieldControl.ClientID);
      }
      controlDescriptor.AddProperty("dataFieldNameControlIdMap", (object) dictionary);
      controlDescriptor.AddProperty("serviceUrl", (object) this.Page.ResolveUrl("~/Sitefinity/Services/Twitter/TwitterUrlShortConfigService.svc"));
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }
  }
}
