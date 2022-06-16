// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.ConsentManagerScriptControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.TrackingConsent.Configuration;

namespace Telerik.Sitefinity.TrackingConsent
{
  /// <summary>Initializes TrackingConsentManger object.</summary>
  public class ConsentManagerScriptControl : Control
  {
    private const string InitScriptFormat = "(function() {{ TrackingConsentManager.init('{0}'); }})();";
    internal const string ConsentManagerScriptControlPath = "Telerik.Sitefinity.TrackingConsent.Controls.ConsentManagerScriptControl.js";

    /// <inheritdoc />
    public override void RenderControl(HtmlTextWriter writer)
    {
      if (SystemManager.IsBackendRequest())
        return;
      ITrackingConsentSettings currentConsentSettings = TrackingConsentManager.GetCurrentConsentSettings();
      if (!currentConsentSettings.ConsentIsRequired)
        return;
      string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(typeof (ConsentManagerScriptControl), "Telerik.Sitefinity.TrackingConsent.Controls.ConsentManagerScriptControl.js");
      string initScript = this.GenerateInitScript(currentConsentSettings.ConsentDialog);
      if (string.IsNullOrEmpty(webResourceUrl) || string.IsNullOrEmpty(initScript))
        return;
      writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
      writer.AddAttribute(HtmlTextWriterAttribute.Src, webResourceUrl);
      writer.RenderBeginTag(HtmlTextWriterTag.Script);
      writer.RenderEndTag();
      writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
      writer.RenderBeginTag(HtmlTextWriterTag.Script);
      writer.Write(initScript);
      writer.RenderEndTag();
    }

    private string GenerateInitScript(string dialogPath) => string.Format("(function() {{ TrackingConsentManager.init('{0}'); }})();", (object) HttpUtility.JavaScriptStringEncode(TrackingConsentManager.GetConsentDialogHtml(dialogPath)));
  }
}
