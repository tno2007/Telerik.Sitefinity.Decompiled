// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Multisite.Web.UI
{
  /// <summary>Represents the Multi-site management backend UI</summary>
  public class SiteDetailView : KendoWindow
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Multisite.SiteDetailView.ascx");
    internal new const string scriptReference = "Telerik.Sitefinity.Multisite.Web.UI.Scripts.SiteDetailView.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Multisite.Web.UI.SiteDetailView" /> class.
    /// </summary>
    public SiteDetailView() => this.LayoutTemplatePath = SiteDetailView.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets a reference to the outer div containing the window content.
    /// </summary>
    /// <value></value>
    protected override HtmlContainerControl OuterDiv => this.Container.GetControl<HtmlContainerControl>("siteDetailViewWrapper", true);

    /// <summary>Gets a reference to the site properties view.</summary>
    protected virtual SitePropertiesView PropertiesView => this.Container.GetControl<SitePropertiesView>("propertiesView", true);

    /// <summary>Gets a reference to the configure modules view.</summary>
    protected virtual ConfigureModulesView ConfigureModulesView => this.Container.GetControl<ConfigureModulesView>("configureModulesView", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("propertiesView", this.PropertiesView.ClientID);
      controlDescriptor.AddComponentProperty("configureModulesView", this.ConfigureModulesView.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Multisite.Web.UI.Scripts.SiteDetailView.js", typeof (SiteDetailView).Assembly.FullName)
    };
  }
}
