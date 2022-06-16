// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.FieldControls;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>
  /// Abstract class which provides basic functionality for all basic settings.
  /// </summary>
  public abstract class BasicSettingsView : ViewModeControl<BasicSettingsPanel>, IScriptControl
  {
    private const string viewScript = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.BasicSettingsView.js";

    /// <summary>Gets the BasicSettingsSitePanel.</summary>
    /// <value>The BasicSettingsSitePanel.</value>
    public virtual BasicSettingsSitePanel BasicSettingsSitePanel => this.Container.GetControl<BasicSettingsSitePanel>("basicSettingsSitePanel", false);

    /// <summary>Gets the fields client binder.</summary>
    public virtual FieldControlsBinder FieldsBinder => this.Container.GetControl<FieldControlsBinder>("fieldsBinder", this.UseFieldsBinder);

    /// <summary>Gets the save button.</summary>
    public virtual HyperLink SaveButton => this.Container.GetControl<HyperLink>("btnSave", this.UseFieldsBinder);

    /// <summary>Gets the field control client ids.</summary>
    /// <value>The field control client ids.</value>
    public virtual IEnumerable<string> FieldControlClientIds => this.Container.GetControls<Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldControl>().Select<KeyValuePair<string, Control>, string>((Func<KeyValuePair<string, Control>, string>) (c => c.Value.ClientID));

    /// <summary>Gets a reference to the message control.</summary>
    public virtual Message Message => this.Container.GetControl<Message>("message", this.UseFieldsBinder);

    /// <summary>
    /// Gets a reference to the container control of the bottom buttons.
    /// </summary>
    public virtual Control ButtonsArea => this.Container.GetControl<Control>("buttonsArea", this.UseFieldsBinder);

    /// <summary>
    /// Gets a reference to the container control with the loading image.
    /// </summary>
    /// <value>The loading view.</value>
    public virtual Control LoadingView => this.Container.GetControl<Control>("loadingView", this.UseFieldsBinder);

    /// <summary>Gets the client label manager.</summary>
    /// <value>The client label manager.</value>
    public virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", this.UseFieldsBinder);

    /// <summary>Gets the heading label.</summary>
    public virtual Literal GeneralSettingsHeadingLiteral => this.Container.GetControl<Literal>(nameof (GeneralSettingsHeadingLiteral), true);

    /// <summary>Gets the reference to the SiteSelector control</summary>
    protected virtual SiteSelector SiteSelector => this.Container.GetControl<SiteSelector>("siteSelector", false);

    protected virtual bool UseFieldsBinder => true;

    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      if (this.BasicSettingsSitePanel == null || this.SiteSelector == null)
        return;
      this.BasicSettingsSitePanel.Visible = false;
      this.SiteSelector.Visible = false;
      BasicSettingsRegistration currentBasicSettingView = SystemManager.GetEditableBasicSettingsRegistrations().FirstOrDefault<BasicSettingsRegistration>((Func<BasicSettingsRegistration, bool>) (s => s.SettingsName == this.Name));
      if (currentBasicSettingView == null || !currentBasicSettingView.AllowSettingsPerSite)
        return;
      string basicSettingsTitle = this.GetBasicSettingsTitle(currentBasicSettingView);
      this.GeneralSettingsHeadingLiteral.Text = basicSettingsTitle;
      if (SystemManager.CurrentContext.GetSites(true).Count<ISite>() <= 1)
        return;
      this.InitializeMultisiteControls(basicSettingsTitle);
    }

    private void InitializeMultisiteControls(string basicSettingsViewTitle)
    {
      this.SiteSelector.Visible = true;
      this.SiteSelector.SiteMenuHeading = string.Format(Res.Get<PageResources>().BasicSettingsSiteFor, (object) basicSettingsViewTitle);
      this.SiteSelector.UseContextualSiteMenu = true;
      if (!(this.SiteSelector.SiteId != Guid.Empty) || !ClaimsManager.GetCurrentIdentity().IsGlobalUser)
        return;
      this.BasicSettingsSitePanel.Visible = true;
    }

    private string GetBasicSettingsTitle(BasicSettingsRegistration currentBasicSettingView)
    {
      string settingsTitle = currentBasicSettingView.SettingsTitle;
      if (!string.IsNullOrEmpty(currentBasicSettingView.SettingsResourceClass))
        settingsTitle = Res.Get(currentBasicSettingView.SettingsResourceClass, currentBasicSettingView.SettingsTitle);
      return settingsTitle;
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
      PageManager.ConfigureScriptManager(this.Page, ScriptRef.MicrosoftAjax | ScriptRef.MicrosoftAjaxTemplates | ScriptRef.MicrosoftAjaxWebForms | ScriptRef.JQuery | ScriptRef.JQueryValidate | ScriptRef.QueryString).RegisterScriptControl<BasicSettingsView>(this);
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
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor scriptDescriptor = this.GetScriptDescriptor();
      if (this.UseFieldsBinder)
      {
        JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
        scriptDescriptor.AddComponentProperty("binder", this.FieldsBinder.ClientID);
        scriptDescriptor.AddProperty("_fieldControlIds", (object) scriptSerializer.Serialize((object) this.FieldControlClientIds));
        scriptDescriptor.AddElementProperty("saveButton", this.SaveButton.ClientID);
        scriptDescriptor.AddComponentProperty("message", this.Message.ClientID);
        scriptDescriptor.AddElementProperty("buttonsArea", this.ButtonsArea.ClientID);
        scriptDescriptor.AddElementProperty("loadingView", this.LoadingView.ClientID);
        scriptDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
        if (this.BasicSettingsSitePanel != null && this.BasicSettingsSitePanel.Visible)
          scriptDescriptor.AddComponentProperty("basicSettingsSitePanel", this.BasicSettingsSitePanel.ClientID);
        if (this.SiteSelector != null && this.SiteSelector.SiteId != Guid.Empty && (this.SiteSelector.Visible || !ClaimsManager.GetCurrentIdentity().IsGlobalUser))
          scriptDescriptor.AddProperty("siteId", (object) this.SiteSelector.SiteId);
      }
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        scriptDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public virtual IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.BasicSettingsView.js", typeof (BasicSettingsView).Assembly.FullName)
    };

    /// <summary>
    /// Returns an initialized <see cref="T:System.Web.UI.ScriptControlDescriptor" /> that will be used by <see cref="M:Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsView.GetScriptDescriptors" />.
    /// Provides a way for inheriting types to initialize their own <see cref="T:System.Web.UI.ScriptControlDescriptor" /> object
    /// and use it in a script component.
    /// </summary>
    protected virtual ScriptControlDescriptor GetScriptDescriptor() => new ScriptControlDescriptor(typeof (BasicSettingsView).FullName, this.ClientID);
  }
}
