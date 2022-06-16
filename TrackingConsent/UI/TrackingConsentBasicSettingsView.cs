// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.TrackingConsent.Configuration;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.TrackingConsent.UI
{
  /// <summary>View for tracking consent setting.</summary>
  public class TrackingConsentBasicSettingsView : SimpleScriptView
  {
    private readonly string newLayoutTemplatePath = "~/Res/" + "Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView.ascx";
    internal const string XRegExp = "Telerik.Sitefinity.Resources.Scripts.xregexp-min.js";
    internal const string TrackingConsentBasicSettingsViewScriptPath = "Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView.js";

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.TrackingConsent.UI.TrackingConsentBasicSettingsView.js", typeof (TrackingConsentBasicSettingsView).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.xregexp-min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name)
    };

    /// <inheritdoc />
    protected override ScriptRef GetRequiredCoreScripts() => base.GetRequiredCoreScripts() | ScriptRef.AngularJS;

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("webServiceUrl", (object) UrlPath.ResolveUrl("~/Sitefinity/Services/BasicSettings.svc", false, true));
      controlDescriptor.AddProperty("contractType", (object) typeof (TrackingConsentSettingsContract).FullName);
      controlDescriptor.AddProperty("contractTypeShortName", (object) typeof (TrackingConsentSettingsContract).Name);
      controlDescriptor.AddProperty("contractTypeNamespace", (object) typeof (TrackingConsentSettingsContract).Namespace);
      string applicationPath = this.Context.Request.ApplicationPath;
      if (!applicationPath.EndsWith("/"))
        applicationPath += "/";
      controlDescriptor.AddProperty("appPath", (object) applicationPath);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.Container.GetControl<ClientLabelManager>("clientLabelManager", true).ClientID);
      controlDescriptor.AddComponentProperty("domainConfirmationDialog", this.Container.GetControl<PromptDialog>("domainConfirmationDialog", true).ClientID);
      controlDescriptor.AddComponentProperty("consentDetailsDialog", this.Container.GetControl<TrackingConsentDetailsWindow>("consentDetailsDialog", true).ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? this.newLayoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
