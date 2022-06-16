// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews
{
  /// <summary>View for editing a weekly recurrence pattern.</summary>
  public class RecurrenceWeeklyPatternView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceWeeklyPatternView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.RecurrenceViews.RecurrenceWeeklyPatternView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceWeeklyPatternView" /> class.
    /// </summary>
    public RecurrenceWeeklyPatternView() => this.LayoutTemplatePath = RecurrenceWeeklyPatternView.layoutTemplatePath;

    /// <summary>Gets a reference to the week interval text box.</summary>
    protected virtual TextBox WeekIntervalTextBox => this.Container.GetControl<TextBox>("weekIntervalTextBox", true);

    /// <summary>Gets a reference to the Monday check box.</summary>
    protected virtual CheckBox MondayCheckBox => this.Container.GetControl<CheckBox>("mondayCheckBox", true);

    /// <summary>Gets a reference to the Tuesday check box.</summary>
    protected virtual CheckBox TuesdayCheckBox => this.Container.GetControl<CheckBox>("tuesdayCheckBox", true);

    /// <summary>Gets a reference to the Wednesday check box.</summary>
    protected virtual CheckBox WednesdayCheckBox => this.Container.GetControl<CheckBox>("wednesdayCheckBox", true);

    /// <summary>Gets a reference to the Thursday check box.</summary>
    protected virtual CheckBox ThursdayCheckBox => this.Container.GetControl<CheckBox>("thursdayCheckBox", true);

    /// <summary>Gets a reference to the Friday check box.</summary>
    protected virtual CheckBox FridayCheckBox => this.Container.GetControl<CheckBox>("fridayCheckBox", true);

    /// <summary>Gets a reference to the Saturday check box.</summary>
    protected virtual CheckBox SaturdayCheckBox => this.Container.GetControl<CheckBox>("saturdayCheckBox", true);

    /// <summary>Gets a reference to the Sunday check box.</summary>
    protected virtual CheckBox SundayCheckBox => this.Container.GetControl<CheckBox>("sundayCheckBox", true);

    /// <summary>Gets the type of the script descriptor.</summary>
    protected virtual string ScriptDescriptorType => this.GetType().FullName;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorType, this.ClientID);
      controlDescriptor.AddElementProperty("weekIntervalTextBox", this.WeekIntervalTextBox.ClientID);
      controlDescriptor.AddElementProperty("mondayCheckBox", this.MondayCheckBox.ClientID);
      controlDescriptor.AddElementProperty("tuesdayCheckBox", this.TuesdayCheckBox.ClientID);
      controlDescriptor.AddElementProperty("wednesdayCheckBox", this.WednesdayCheckBox.ClientID);
      controlDescriptor.AddElementProperty("thursdayCheckBox", this.ThursdayCheckBox.ClientID);
      controlDescriptor.AddElementProperty("fridayCheckBox", this.FridayCheckBox.ClientID);
      controlDescriptor.AddElementProperty("saturdayCheckBox", this.SaturdayCheckBox.ClientID);
      controlDescriptor.AddElementProperty("sundayCheckBox", this.SundayCheckBox.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[1]
    {
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceWeeklyPatternView.js", typeof (RecurrenceWeeklyPatternView).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <exception cref="T:System.NotImplementedException"></exception>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
