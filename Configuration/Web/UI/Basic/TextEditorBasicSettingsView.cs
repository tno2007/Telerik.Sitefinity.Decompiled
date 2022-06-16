// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ItemLists;

namespace Telerik.Sitefinity.Configuration.Web.UI.Basic
{
  /// <summary>View for text editor basic settings.</summary>
  public class TextEditorBasicSettingsView : BasicSettingsView
  {
    internal const string viewScript = "Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.TextEditorBasicSettingsView.js";
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Configuration.Basic.TextEditorBasicSettingsView.ascx");

    /// <summary>
    /// Gets a reference to the standardEditorConfigurationKey hidden filed
    /// </summary>
    protected virtual HiddenField StandardEditorConfigurationKey => this.Container.GetControl<HiddenField>("standardEditorConfigurationKey", true);

    /// <summary>
    /// Gets a reference to the minimalEditorConfigurationKey hidden filed
    /// </summary>
    protected virtual HiddenField MinimalEditorConfigurationKey => this.Container.GetControl<HiddenField>("minimalEditorConfigurationKey", true);

    /// <summary>
    /// Gets a reference to the forumsEditorConfigurationKey hidden filed
    /// </summary>
    protected virtual HiddenField ForumsEditorConfigurationKey => this.Container.GetControl<HiddenField>("forumsEditorConfigurationKey", true);

    /// <summary>Gets a reference to the CustomToolSetsItemsGrid.</summary>
    protected virtual ItemsGrid CustomToolSetsItemsGrid => this.Container.GetControl<ItemsGrid>("customToolSetsItemsGrid", true);

    /// <summary>Gets a reference to the DefaultToolSetsItemsGrid.</summary>
    protected virtual ItemsGrid DefaultToolSetsItemsGrid => this.Container.GetControl<ItemsGrid>("defaultToolSetsItemsGrid", true);

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? TextEditorBasicSettingsView.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets a reference to the warning about which editors use these editor settings
    /// </summary>
    protected virtual Literal TextEditorCompatibilityMessage => this.Container.GetControl<Literal>("lTextEditorCompatibilityMessage", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      this.StandardEditorConfigurationKey.Value = "Default tool set";
      this.MinimalEditorConfigurationKey.Value = "Tool set for comments";
      this.ForumsEditorConfigurationKey.Value = "Tool set for forums";
      this.TextEditorCompatibilityMessage.Text = string.Format((IFormatProvider) CultureInfo.InvariantCulture, Res.Get<Labels>().TextEditorCompatibilityMessage, (object) Res.Get<Labels>().ExternalLinkNewUiEditorSettings);
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddComponentProperty("customToolSetsItemsGrid", this.CustomToolSetsItemsGrid.ClientID);
      controlDescriptor.AddComponentProperty("defaultToolSetsItemsGrid", this.DefaultToolSetsItemsGrid.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Configuration.Web.UI.Basic.Scripts.TextEditorBasicSettingsView.js", typeof (TextEditorBasicSettingsView).Assembly.FullName)
    };

    /// <summary>
    /// Returns an initialized <see cref="T:System.Web.UI.ScriptControlDescriptor" /> that will be used by <see cref="M:Telerik.Sitefinity.Configuration.Web.UI.Basic.TextEditorBasicSettingsView.GetScriptDescriptors" />.
    /// Provides a way for inheriting types to initialize their own <see cref="T:System.Web.UI.ScriptControlDescriptor" /> object
    /// and use it in a script component.
    /// </summary>
    protected override ScriptControlDescriptor GetScriptDescriptor() => new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
  }
}
