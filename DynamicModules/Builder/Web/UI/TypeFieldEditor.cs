// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.UI.TypeFieldEditor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.UI
{
  /// <summary>
  /// Widget which provides functionality for creating and editing fields of the custom type.
  /// </summary>
  public class TypeFieldEditor : KendoView
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ModuleBuilder.TypeFieldEditor.ascx");
    internal const string scriptRef = "Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.TypeFieldEditor.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
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
          base.LayoutTemplatePath = TypeFieldEditor.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the container holding the "How to translate" link
    /// </summary>
    private HtmlControl HowToTranslateContainer => this.Container.GetControl<HtmlControl>("howToTranslateContainer", true);

    /// <summary>
    /// Gets the HiddenField holding the state of Google Map API key (valid or not)
    /// </summary>
    private HiddenField IsGoogleMapApiKeyValid => this.Container.GetControl<HiddenField>("isGoogleMapApiKeyValid", true);

    private HiddenField MultisiteVal => this.Container.GetControl<HiddenField>("multisiteVal", true);

    private HiddenField DbType => this.Container.GetControl<HiddenField>("dbtype", true);

    /// <summary>
    /// Gets the HiddenField holding the names of the properties of the DynamicContent type
    /// </summary>
    private HiddenField UsedPropertyNames => this.Container.GetControl<HiddenField>("usedPropertyNames", true);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that
    /// corresponds to this Web server control. This property is used primarily by control
    /// developers.
    /// </summary>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration
    /// values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.UsedPropertyNames.Value = JsonConvert.SerializeObject((object) ((IEnumerable<PropertyInfo>) typeof (DynamicContent).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)).Where<PropertyInfo>((Func<PropertyInfo, bool>) (p => p.Name.IndexOf(".") == -1)).Select<PropertyInfo, string>((Func<PropertyInfo, string>) (p => p.Name)));
      this.IsGoogleMapApiKeyValid.Value = string.IsNullOrEmpty(Config.Get<SystemConfig>().GeoLocationSettings.GoogleMapApiV3Key) ? "false" : "true";
      this.MultisiteVal.Value = true.ToString();
      this.DbType.Value = ((IOpenAccessDataProvider) ModuleBuilderManager.GetManager().Provider).GetContext().OpenAccessConnection.DbType.ToString();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects
    /// that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" />
    /// objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.DynamicModules.Builder.Web.Scripts.TypeFieldEditor.js", typeof (TypeFieldEditor).Assembly.FullName)
    };
  }
}
