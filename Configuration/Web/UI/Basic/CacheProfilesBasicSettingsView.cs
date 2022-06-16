// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.CacheProfilesBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>The base class for cache profile settings view.</summary>
  /// <seealso cref="!:Telerik.Sitefinity.Web.UI.ViewModeControl&lt;Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsPanel&gt;" />
  /// <seealso cref="T:System.Web.UI.IScriptControl" />
  internal class CacheProfilesBasicSettingsView : ViewModeControl<BasicSettingsPanel>, IScriptControl
  {
    internal const string JavaScriptReference = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.CacheProfilesBasicSettingsView.js";
    private static readonly string LayoutTemplateVirtualPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.CacheProfilesBasicSettingsView.ascx");

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CacheProfilesBasicSettingsView.LayoutTemplateVirtualPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (CacheProfilesBasicSettingsView).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("pageCacheGrid", this.Container.GetControl<HtmlGenericControl>("pageCacheGrid", true).ClientID);
      controlDescriptor.AddProperty("pageProfileUrl", (object) this.ResolveClientUrl("~/Sitefinity/Services/CacheProfiles/Settings.svc/page"));
      controlDescriptor.AddElementProperty("mediaCacheGrid", this.Container.GetControl<HtmlGenericControl>("mediaCacheGrid", true).ClientID);
      controlDescriptor.AddProperty("mediaProfileUrl", (object) this.ResolveClientUrl("~/Sitefinity/Services/CacheProfiles/Settings.svc/media"));
      controlDescriptor.AddComponentProperty("clientLabelManager", this.Container.GetControl<ClientLabelManager>("clientLabelManager", true).ClientID);
      controlDescriptor.AddElementProperty("createPageProfileButton", this.Container.GetControl<HtmlAnchor>("createPageProfileButton", true).ClientID);
      controlDescriptor.AddElementProperty("createMediaProfileButton", this.Container.GetControl<HtmlAnchor>("createMediaProfileButton", true).ClientID);
      controlDescriptor.AddComponentProperty("pageProfilesWindow", this.Container.GetControl<PageCacheProfileDetailsWindow>("pageProfilesWindow", true).ClientID);
      controlDescriptor.AddComponentProperty("mediaProfilesWindow", this.Container.GetControl<MediaCacheProfileDetailsWindow>("mediaProfilesWindow", true).ClientID);
      controlDescriptor.AddComponentProperty("pageProfileDeleteConfirmationDialog", this.Container.GetControl<PromptDialog>("pageProfileDeleteConfirmationDialog", true).ClientID);
      controlDescriptor.AddComponentProperty("mediaProfileDeleteConfirmationDialog", this.Container.GetControl<PromptDialog>("mediaProfileDeleteConfirmationDialog", true).ClientID);
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
    public IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.CacheProfilesBasicSettingsView.js", typeof (CacheProfilesBasicSettingsView).Assembly.FullName)
    };

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
      PageManager.ConfigureScriptManager(this.Page, this.GetRequiredCoreScripts(), false)?.RegisterScriptControl<CacheProfilesBasicSettingsView>(this);
    }

    private ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery | ScriptRef.KendoWeb;
  }
}
