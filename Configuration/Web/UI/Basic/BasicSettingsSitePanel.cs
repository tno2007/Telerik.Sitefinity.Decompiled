// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.BasicSettingsSitePanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>
  /// Control used to present the site specific status of the shown site settings.
  /// </summary>
  public class BasicSettingsSitePanel : SimpleScriptView
  {
    internal const string ViewScript = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.BasicSettingsSitePanel.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.BasicSettingsSitePanel.ascx");

    /// <summary>Gets a reference to the client label manager.</summary>
    protected virtual ClientLabelManager ClientLabelManager => this.Container.GetControl<ClientLabelManager>("clientLabelManager", true);

    /// <summary>
    /// Gets the reference to the span control SettingsStatusLabel
    /// </summary>
    protected virtual HtmlGenericControl SettingsStatusLabel => this.Container.GetControl<HtmlGenericControl>("settingsStatusLabel", true);

    /// <summary>
    /// Gets the reference to the HtmlGenericControl control ChangeInheritanceButton
    /// </summary>
    protected virtual HtmlAnchor ChangeInheritanceButton => this.Container.GetControl<HtmlAnchor>("changeInheritanceButton", true);

    /// <summary>
    /// Gets the reference to the HtmlGenericControl control SpecificSettingsLabel
    /// </summary>
    protected virtual HtmlGenericControl SpecificSettingsLabel => this.Container.GetControl<HtmlGenericControl>("specificSettingsLabel", true);

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the control
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = BasicSettingsSitePanel.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("globalContextQueryStringParamName", (object) "sf_global");
      controlDescriptor.AddProperty("globalContextQueryStringParamValueTrue", (object) "true");
      controlDescriptor.AddProperty("siteName", (object) SystemManager.CurrentContext.CurrentSite.Name);
      controlDescriptor.AddElementProperty("settingsStatusLabel", this.SettingsStatusLabel.ClientID);
      controlDescriptor.AddElementProperty("changeInheritanceButton", this.ChangeInheritanceButton.ClientID);
      controlDescriptor.AddElementProperty("specificSettingsLabel", this.SpecificSettingsLabel.ClientID);
      controlDescriptor.AddComponentProperty("clientLabelManager", this.ClientLabelManager.ClientID);
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Web.SitefinityJS.UI.ICommandWidget.js", typeof (CommandWidget).Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.BasicSettingsSitePanel.js", typeof (BasicSettingsSitePanel).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
