// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews
{
  /// <summary>View for editing a recurrence range.</summary>
  public class RecurrenceRangeView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceRangeView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.RecurrenceViews.RecurrenceRangeView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceRangeView" /> class.
    /// </summary>
    public RecurrenceRangeView() => this.LayoutTemplatePath = RecurrenceRangeView.layoutTemplatePath;

    /// <summary>Gets a reference of the range end label.</summary>
    protected virtual SitefinityLabel RangeEndLabel => this.Container.GetControl<SitefinityLabel>("rangeEndLabel", false);

    /// <summary>Gets a reference the start date field.</summary>
    protected virtual DateField StartDateField => this.Container.GetControl<DateField>("startDateField", true);

    /// <summary>Gets a reference to the end date field.</summary>
    protected virtual DateField EndDateField => this.Container.GetControl<DateField>("endDateField", true);

    /// <summary>Gets a reference to the end after repeats radio.</summary>
    protected virtual RadioButton EndAfterRepeatsRadio => this.Container.GetControl<RadioButton>("endAfterRepeatsRadio", true);

    /// <summary>Gets a reference to the repeats text box.</summary>
    protected virtual TextBox RepeatsTextBox => this.Container.GetControl<TextBox>("repeatsTextBox", true);

    /// <summary>Gets a reference to the repeat until radio.</summary>
    protected virtual RadioButton RepeatUntilRadio => this.Container.GetControl<RadioButton>("repeatUntilRadio", true);

    /// <summary>Gets a reference to the repeat always radio.</summary>
    protected virtual RadioButton RepeatAlwaysRadio => this.Container.GetControl<RadioButton>("repeatAlwaysRadio", true);

    /// <summary>Gets the type of the script descriptor.</summary>
    protected virtual string ScriptDescriptorType => this.GetType().FullName;

    /// <summary>Gets or sets the range start text.</summary>
    public virtual string RangeStartText { get; set; }

    /// <summary>Gets or sets the range end text.</summary>
    public virtual string RangeEndText { get; set; }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.ScriptDescriptorType, this.ClientID);
      controlDescriptor.AddComponentProperty("startDateField", this.StartDateField.ClientID);
      controlDescriptor.AddComponentProperty("endDateField", this.EndDateField.ClientID);
      controlDescriptor.AddElementProperty("endAfterRepeatsRadio", this.EndAfterRepeatsRadio.ClientID);
      controlDescriptor.AddElementProperty("repeatsTextBox", this.RepeatsTextBox.ClientID);
      controlDescriptor.AddElementProperty("repeatUntilRadio", this.RepeatUntilRadio.ClientID);
      controlDescriptor.AddElementProperty("repeatAlwaysRadio", this.RepeatAlwaysRadio.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceRangeView.js", typeof (RecurrenceRangeView).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <exception cref="T:System.NotImplementedException"></exception>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.StartDateField.Title = this.RangeStartText;
      this.RangeEndLabel.Text = this.RangeEndText;
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
