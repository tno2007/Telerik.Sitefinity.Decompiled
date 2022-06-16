// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RecurrencyField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>A field for editing recurrency in iCal format.</summary>
  [FieldDefinitionElement(typeof (RecurrencyFieldDefinitionElement))]
  public class RecurrencyField : FieldControl
  {
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.RecurrencyField.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.RecurrencyField.js";
    internal const string recurrenceRuleReference = "Telerik.Sitefinity.Web.UI.Fields.Scripts.RecurrenceRule.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.RecurrencyField" /> class.
    /// </summary>
    public RecurrencyField() => this.LayoutTemplatePath = RecurrencyField.layoutTemplatePath;

    /// <summary>
    /// Gets the reference to the label control that represents the title of the field control.
    /// </summary>
    /// <remarks>This control is mandatory only in write mode.</remarks>
    protected internal virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", true);

    /// <summary>Gets a reference to the recurrence type drop down.</summary>
    protected virtual DropDownList RecurrenceTypeDropDown => this.Container.GetControl<DropDownList>("recurrenceTypeDropDown", true);

    /// <summary>Gets a reference to the repeat options panel.</summary>
    protected virtual Panel RepeatOptionsPanel => this.Container.GetControl<Panel>("repeatOptionsPanel", true);

    /// <summary>Gets a reference to the daily pattern page.</summary>
    protected virtual RadPageView DailyPatternPage => this.Container.GetControl<RadPageView>("dailyPatternPage", true);

    /// <summary>Gets a reference to the weekly pattern page.</summary>
    protected virtual RadPageView WeeklyPatternPage => this.Container.GetControl<RadPageView>("weeklyPatternPage", true);

    /// <summary>Gets a reference to the monthly pattern page.</summary>
    protected virtual RadPageView MonthlyPatternPage => this.Container.GetControl<RadPageView>("monthlyPatternPage", true);

    /// <summary>Gets a reference to the yearly pattern page.</summary>
    protected virtual RadPageView YearlyPatternPage => this.Container.GetControl<RadPageView>("yearlyPatternPage", true);

    /// <summary>
    /// Gets a reference to the recurrence pattern multi page.
    /// </summary>
    protected virtual RadMultiPage RecurrencePatternMultiPage => this.Container.GetControl<RadMultiPage>("recurrencePatternMultiPage", true);

    /// <summary>Gets a reference to the daily pattern view.</summary>
    protected virtual RecurrenceDailyPatternView DailyPatternView => this.Container.GetControl<RecurrenceDailyPatternView>("dailyPatternView", true);

    /// <summary>Gets a reference to the weekly pattern view.</summary>
    protected virtual RecurrenceWeeklyPatternView WeeklyPatternView => this.Container.GetControl<RecurrenceWeeklyPatternView>("weeklyPatternView", true);

    /// <summary>Gets a reference to the monthly pattern view.</summary>
    protected virtual RecurrenceMonthlyPatternView MonthlyPatternView => this.Container.GetControl<RecurrenceMonthlyPatternView>("monthlyPatternView", true);

    /// <summary>Gets a reference to the yearly pattern view.</summary>
    protected virtual RecurrenceYearlyPatternView YearlyPatternView => this.Container.GetControl<RecurrenceYearlyPatternView>("yearlyPatternView", true);

    /// <summary>Gets a reference to the range view.</summary>
    protected virtual RecurrenceRangeView RangeView => this.Container.GetControl<RecurrenceRangeView>("rangeView", true);

    /// <summary>Gets a reference to the time view.</summary>
    protected virtual RecurrenceTimeView TimeView => this.Container.GetControl<RecurrenceTimeView>("timeView", true);

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the range start text.</summary>
    public virtual string RangeStartText { get; set; }

    /// <summary>Gets or sets the range end text.</summary>
    public virtual string RangeEndText { get; set; }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.Text = this.Title;
      this.RangeView.RangeStartText = this.RangeStartText;
      this.RangeView.RangeEndText = this.RangeEndText;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptorList = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("recurrenceTypeDropDown", this.RecurrenceTypeDropDown.ClientID);
      controlDescriptor.AddElementProperty("repeatOptionsPanel", this.RepeatOptionsPanel.ClientID);
      controlDescriptor.AddProperty("dailyPatternPageId", (object) this.DailyPatternPage.ClientID);
      controlDescriptor.AddProperty("weeklyPatternPageId", (object) this.WeeklyPatternPage.ClientID);
      controlDescriptor.AddProperty("monthlyPatternPageId", (object) this.MonthlyPatternPage.ClientID);
      controlDescriptor.AddProperty("yearlyPatternPageId", (object) this.YearlyPatternPage.ClientID);
      controlDescriptor.AddComponentProperty("recurrencePatternMultiPage", this.RecurrencePatternMultiPage.ClientID);
      controlDescriptor.AddComponentProperty("dailyPatternView", this.DailyPatternView.ClientID);
      controlDescriptor.AddComponentProperty("weeklyPatternView", this.WeeklyPatternView.ClientID);
      controlDescriptor.AddComponentProperty("monthlyPatternView", this.MonthlyPatternView.ClientID);
      controlDescriptor.AddComponentProperty("yearlyPatternView", this.YearlyPatternView.ClientID);
      controlDescriptor.AddComponentProperty("rangeView", this.RangeView.ClientID);
      controlDescriptor.AddComponentProperty("timeView", this.TimeView.ClientID);
      scriptDescriptorList.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptorList.ToArray();
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (RecurrencyField).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.RecurrenceRule.js", fullName),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Scripts.RecurrencyField.js", fullName)
      };
    }

    /// <summary>
    /// Initialize properties of the field implementing <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IField" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    public override void Configure(IFieldDefinition definition)
    {
      base.Configure(definition);
      if (!(definition is IRecurrencyFieldDefinition recurrencyFieldDefinition))
        return;
      if (string.IsNullOrEmpty(recurrencyFieldDefinition.ResourceClassId))
      {
        this.RangeStartText = recurrencyFieldDefinition.RangeStartText;
        this.RangeEndText = recurrencyFieldDefinition.RangeEndText;
      }
      else
      {
        this.RangeStartText = Res.Get(recurrencyFieldDefinition.ResourceClassId, recurrencyFieldDefinition.RangeStartText);
        this.RangeEndText = Res.Get(recurrencyFieldDefinition.ResourceClassId, recurrencyFieldDefinition.RangeEndText);
      }
    }
  }
}
