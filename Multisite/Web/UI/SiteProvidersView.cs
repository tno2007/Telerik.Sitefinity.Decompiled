// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>
  /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView" /> class.
  /// </summary>
  public class SiteProvidersView : KendoView
  {
    internal const string ScriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.SiteProvidersView.js";
    private static readonly string TemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.SiteProvidersView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SiteProvidersView" /> class.
    /// </summary>
    public SiteProvidersView() => this.LayoutTemplatePath = SiteProvidersView.TemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the reference to the site detail view</summary>
    protected virtual SiteDetailView SiteDetailView => this.Container.GetControl<SiteDetailView>("siteDetailView", true);

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (SiteProvidersView).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("siteDetailView", this.SiteDetailView.ClientID);
      controlDescriptor.AddProperty("_isInStandaloneMode", (object) true);
      controlDescriptor.AddProperty("_currentSiteId", (object) SystemManager.CurrentContext.CurrentSite.Id);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<System.Web.UI.ScriptReference> GetScriptReferences() => (IEnumerable<System.Web.UI.ScriptReference>) new List<System.Web.UI.ScriptReference>(base.GetScriptReferences())
    {
      new System.Web.UI.ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.SiteProvidersView.js", typeof (SiteProvidersView).Assembly.FullName)
    };

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }
  }
}
