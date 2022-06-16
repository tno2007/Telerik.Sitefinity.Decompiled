// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.SystemEmailsBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>A control for the email settings</summary>
  public class SystemEmailsBasicSettingsView : SimpleScriptView
  {
    internal const string JavaScriptReference = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.SystemEmailsBasicSettingsView.js";
    private static readonly string LayoutTemplateVirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.SystemEmailsBasicSettingsView.ascx");

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SystemEmailsBasicSettingsView.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the reference to the grid control</summary>
    protected virtual HtmlGenericControl SystemEmailsGrid => this.Container.GetControl<HtmlGenericControl>("systemEmailsGrid", true);

    /// <summary>Gets the reference to the ClientLabelManager control</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets the reference to the MessageTemplateEditor control
    /// </summary>
    protected virtual MessageTemplateEditor MessageTemplateEditor => this.Container.GetControl<MessageTemplateEditor>("messageTemplateEditor", true);

    /// <summary>Gets the reference to the SiteSelector control</summary>
    protected virtual SiteSelector SiteSelector => this.Container.GetControl<SiteSelector>("siteSelector", false);

    /// <summary>
    /// Gets the reference to the BasicSettingsSitePanel control
    /// </summary>
    protected virtual BasicSettingsSitePanel BasicSettingsSitePanel => this.Container.GetControl<BasicSettingsSitePanel>("basicSettingsSitePanel", false);

    /// <summary>Gets the current site Id</summary>
    protected virtual Guid SiteId
    {
      get
      {
        Guid result = Guid.Empty;
        string input = SystemManager.CurrentHttpContext.Request.QueryString["sf_site"];
        if (!input.IsNullOrEmpty())
          Guid.TryParse(input, out result);
        else if (!ClaimsManager.GetCurrentIdentity().IsGlobalUser)
          result = SystemManager.CurrentContext.CurrentSite.Id;
        return result;
      }
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (SystemEmailsBasicSettingsView).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("systemEmailsGrid", this.SystemEmailsGrid.ClientID);
      controlDescriptor.AddProperty("serviceUrl", (object) this.ResolveClientUrl("~/Sitefinity/Services/SystemEmails/Settings.svc"));
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      controlDescriptor.AddComponentProperty("messageTemplateEditor", this.MessageTemplateEditor.ClientID);
      if (this.SiteSelector != null)
        controlDescriptor.AddComponentProperty("siteSelector", this.SiteSelector.ClientID);
      if (this.BasicSettingsSitePanel != null && this.BasicSettingsSitePanel.Visible)
        controlDescriptor.AddComponentProperty("basicSettingsSitePanel", this.BasicSettingsSitePanel.ClientID);
      if (this.SiteSelector != null && this.SiteId != Guid.Empty)
        controlDescriptor.AddProperty("siteId", (object) this.SiteId);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.SystemEmailsBasicSettingsView.js", typeof (SystemEmailsBasicSettingsView).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The view container.</param>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.BasicSettingsSitePanel == null || this.SiteSelector == null)
        return;
      this.SiteSelector.Visible = false;
      this.BasicSettingsSitePanel.Visible = false;
      if (SystemManager.CurrentContext.GetSites(true).Count<ISite>() <= 1)
        return;
      this.SiteSelector.SiteMenuHeading = Res.Get<ConfigDescriptions>().SystemEmailsFor;
      this.SiteSelector.UseContextualSiteMenu = true;
      this.SiteSelector.Visible = true;
      if (!(this.SiteId != Guid.Empty) || !ClaimsManager.GetCurrentIdentity().IsGlobalUser)
        return;
      this.BasicSettingsSitePanel.Visible = true;
    }
  }
}
