// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsPop3SettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>A control for the POP3 settings</summary>
  public class EmailSettingsPop3SettingsView : SimpleScriptView
  {
    private static readonly string webServiceUrl = VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Configuration/EmailSettings.svc/pop3test/");
    internal const string JavaScriptReference = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.EmailSettingsPop3SettingsView.js";
    private static readonly string LayoutTemplateVirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.EmailSettingsPop3SettingsView.ascx");

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EmailSettingsPop3SettingsView.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected HyperLink PerformPop3TestBtn => this.Container.GetControl<HyperLink>("performPop3TestBtn", true);

    protected ChoiceField TrackBouncedMessagesField => this.Container.GetControl<ChoiceField>("trackBouncedMessagesField", true);

    protected HtmlGenericControl Pop3SettingsContainer => this.Container.GetControl<HtmlGenericControl>("pop3SettingsContainer", true);

    protected TextField Pop3HostField => this.Container.GetControl<TextField>("pop3HostField", true);

    protected TextField Pop3PortField => this.Container.GetControl<TextField>("pop3PortField", true);

    protected TextField Pop3Username => this.Container.GetControl<TextField>("pop3Username", true);

    protected TextField Pop3Password => this.Container.GetControl<TextField>("pop3Password", true);

    protected ChoiceField Pop3UseSmtpSSLField => this.Container.GetControl<ChoiceField>("pop3UseSmtpSSLField", true);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (EmailSettingsPop3SettingsView).FullName, this.ClientID);
      controlDescriptor.AddProperty("serviceUrl", (object) EmailSettingsPop3SettingsView.webServiceUrl);
      controlDescriptor.AddElementProperty("performPop3TestBtn", this.PerformPop3TestBtn.ClientID);
      controlDescriptor.AddComponentProperty("pop3HostField", this.Pop3HostField.ClientID);
      controlDescriptor.AddComponentProperty("pop3PortField", this.Pop3PortField.ClientID);
      controlDescriptor.AddComponentProperty("pop3Username", this.Pop3Username.ClientID);
      controlDescriptor.AddComponentProperty("pop3Password", this.Pop3Password.ClientID);
      controlDescriptor.AddComponentProperty("pop3UseSmtpSSLField", this.Pop3UseSmtpSSLField.ClientID);
      controlDescriptor.AddComponentProperty("trackBouncedMessagesField", this.TrackBouncedMessagesField.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.EmailSettingsPop3SettingsView.js", typeof (EmailSettingsPop3SettingsView).Assembly.FullName)
    };

    protected override void InitializeControls(GenericContainer container)
    {
      if (this.Pop3SettingsContainer == null || SystemManager.IsModuleEnabled("Newsletters"))
        return;
      this.Pop3SettingsContainer.Attributes["class"] = "sfDisplayNone";
    }
  }
}
