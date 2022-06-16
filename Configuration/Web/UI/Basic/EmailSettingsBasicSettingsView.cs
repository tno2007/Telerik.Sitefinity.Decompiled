// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.EmailSettingsBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>A control for the email settings</summary>
  public class EmailSettingsBasicSettingsView : SimpleScriptView
  {
    internal const string JavaScriptReference = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.EmailSettingsBasicSettingsView.js";
    private static readonly string LayoutTemplateVirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.EmailSettingsBasicSettingsView.ascx");

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? EmailSettingsBasicSettingsView.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected HyperLink OpenSendTestEmailDialogBtn => this.Container.GetControl<HyperLink>("openSendTestEmailDialogBtn", true);

    protected EmailSettingsSendTestEmailDialog EmailSettingsSendTestEmailDialog => this.Container.GetControl<EmailSettingsSendTestEmailDialog>("emailSettingsSendTestEmailDialog", true);

    protected EmailSettingsPop3SettingsView EmailSettingsPop3SettingsView => this.Container.GetControl<EmailSettingsPop3SettingsView>("emailSettingsPop3SettingsView", true);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (EmailSettingsBasicSettingsView).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("openSendTestEmailDialogBtn", this.OpenSendTestEmailDialogBtn.ClientID);
      controlDescriptor.AddComponentProperty("emailSettingsSendTestEmailDialog", this.EmailSettingsSendTestEmailDialog.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.EmailSettingsBasicSettingsView.js", typeof (EmailSettingsBasicSettingsView).Assembly.FullName)
    };

    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the
    /// specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents
    /// the output stream to render HTML content on the client.
    /// </param>
    protected override void Render(HtmlTextWriter writer)
    {
      ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">
    /// An <see cref="T:System.EventArgs" /> object that contains the event data.
    /// </param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        throw new HttpException(Res.Get<ErrorMessages>().PageIsNull);
      PageManager.ConfigureScriptManager(this.Page, this.GetRequiredCoreScriptsWithJqueryAndKendo(), false)?.RegisterScriptControl<EmailSettingsBasicSettingsView>(this);
    }

    private ScriptRef GetRequiredCoreScriptsWithJqueryAndKendo() => this.GetRequiredCoreScripts() | ScriptRef.JQuery | ScriptRef.KendoWeb;
  }
}
