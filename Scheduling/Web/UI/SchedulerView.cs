// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Web.UI.SchedulerView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Kendo;

namespace Telerik.Sitefinity.Scheduling.Web.UI
{
  public class SchedulerView : KendoView
  {
    internal const string schedulerViewJsPath = "Telerik.Sitefinity.Scheduling.Web.UI.Scripts.SchedulerView.js";
    internal static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Scheduling.SchedulerView.ascx");

    /// <summary>Gets the layout template path</summary>
    public override string LayoutTemplatePath => SchedulerView.layoutTemplatePath;

    /// <summary>Gets the layout template name</summary>
    protected override string LayoutTemplateName => string.Empty;

    /// <summary>Sets the text of the configuring label</summary>
    public string ConfigureSchedulingLabelText { get; set; }

    /// <summary>Gets the configure scheduling label.</summary>
    protected Literal ConfigureSchedulingLabel => this.Container.GetControl<Literal>("configureSchedulingLabel", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConfigureSchedulingLabel.Text = string.IsNullOrEmpty(this.ConfigureSchedulingLabelText) ? Res.Get<Labels>("ScheduleActionLabelDefault") : this.ConfigureSchedulingLabelText;

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
      new ScriptReference("Telerik.Sitefinity.Scheduling.Web.UI.Scripts.SchedulerView.js", typeof (SchedulerView).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
