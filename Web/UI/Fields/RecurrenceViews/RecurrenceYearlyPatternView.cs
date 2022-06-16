// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews
{
  /// <summary>View for editing a Yearly recurrence pattern.</summary>
  public class RecurrenceYearlyPatternView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceYearlyPatternView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.RecurrenceViews.RecurrenceYearlyPatternView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceYearlyPatternView" /> class.
    /// </summary>
    public RecurrenceYearlyPatternView() => this.LayoutTemplatePath = RecurrenceYearlyPatternView.layoutTemplatePath;

    /// <summary>Gets a reference to the day of month radio.</summary>
    protected virtual RadioButton DayOfMonthRadio => this.Container.GetControl<RadioButton>("dayOfMonthRadio", true);

    /// <summary>Gets a reference to the ordinal day radio.</summary>
    protected virtual RadioButton OrdinalDayRadio => this.Container.GetControl<RadioButton>("ordinalDayRadio", true);

    /// <summary>Gets a reference to the day of month text box.</summary>
    protected virtual TextBox DayOfMonthTextBox => this.Container.GetControl<TextBox>("dayOfMonthTextBox", true);

    /// <summary>Gets a reference to the ordinal day drop down.</summary>
    protected virtual DropDownList OrdinalDayDropDown => this.Container.GetControl<DropDownList>("ordinalDayDropDown", true);

    /// <summary>Gets a reference to the day of week drop down.</summary>
    protected virtual DropDownList DayOfWeekDropDown => this.Container.GetControl<DropDownList>("dayOfWeekDropDown", true);

    /// <summary>Gets a reference to the months drop down.</summary>
    protected virtual DropDownList MonthsDropDown => this.Container.GetControl<DropDownList>("monthsDropDown", true);

    /// <summary>Gets a reference to the ordinal months drop down.</summary>
    protected virtual DropDownList OrdinalMonthsDropDown => this.Container.GetControl<DropDownList>("ordinalMonthsDropDown", true);

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
      controlDescriptor.AddElementProperty("dayOfMonthRadio", this.DayOfMonthRadio.ClientID);
      controlDescriptor.AddElementProperty("dayOfMonthTextBox", this.DayOfMonthTextBox.ClientID);
      controlDescriptor.AddElementProperty("ordinalDayRadio", this.OrdinalDayRadio.ClientID);
      controlDescriptor.AddElementProperty("ordinalDayDropDown", this.OrdinalDayDropDown.ClientID);
      controlDescriptor.AddElementProperty("dayOfWeekDropDown", this.DayOfWeekDropDown.ClientID);
      controlDescriptor.AddElementProperty("monthsDropDown", this.MonthsDropDown.ClientID);
      controlDescriptor.AddElementProperty("ordinalMonthsDropDown", this.OrdinalMonthsDropDown.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceYearlyPatternView.js", typeof (RecurrenceYearlyPatternView).Assembly.FullName)
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
