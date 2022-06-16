// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews
{
  /// <summary>View for editing a daily recurrence pattern.</summary>
  public class RecurrenceDailyPatternView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceDailyPatternView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.RecurrenceViews.RecurrenceDailyPatternView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceDailyPatternView" /> class.
    /// </summary>
    public RecurrenceDailyPatternView() => this.LayoutTemplatePath = RecurrenceDailyPatternView.layoutTemplatePath;

    /// <summary>Gets the type of the script descriptor.</summary>
    protected virtual string ScriptDescriptorType => this.GetType().FullName;

    /// <summary>Gets a reference to the daily radio.</summary>
    protected virtual RadioButton DailyRadio => this.Container.GetControl<RadioButton>("dailyRadio", true);

    /// <summary>Gets a reference to the weekday radio.</summary>
    protected virtual RadioButton WeekdaysRadio => this.Container.GetControl<RadioButton>("weekdaysRadio", true);

    /// <summary>Gets a reference to the daily text box.</summary>
    protected virtual TextBox DailyTextBox => this.Container.GetControl<TextBox>("dailyTextBox", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorType, this.ClientID);
      controlDescriptor.AddElementProperty("dailyRadio", this.DailyRadio.ClientID);
      controlDescriptor.AddElementProperty("weekdaysRadio", this.WeekdaysRadio.ClientID);
      controlDescriptor.AddElementProperty("dailyTextBox", this.DailyTextBox.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceDailyPatternView.js", typeof (RecurrenceDailyPatternView).Assembly.FullName)
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
