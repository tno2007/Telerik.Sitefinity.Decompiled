// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Pages;

namespace Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews
{
  /// <summary>View for editing a recurrence time.</summary>
  public class RecurrenceTimeView : SimpleScriptView
  {
    internal const string scriptReference = "Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceTimeView.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Fields.RecurrenceViews.RecurrenceTimeView.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.RecurrenceTimeView" /> class.
    /// </summary>
    public RecurrenceTimeView() => this.LayoutTemplatePath = RecurrenceTimeView.layoutTemplatePath;

    /// <summary>Gets a reference to the all day check box.</summary>
    protected virtual CheckBox AllDayCheckBox => this.Container.GetControl<CheckBox>("allDayCheckBox", true);

    /// <summary>Gets a reference to the time panel.</summary>
    protected virtual Panel TimePanel => this.Container.GetControl<Panel>("timePanel", true);

    /// <summary>Gets a reference to the start time field.</summary>
    protected virtual DateField StartTimeField => this.Container.GetControl<DateField>("startTimeField", true);

    /// <summary>Gets a reference to the end time field.</summary>
    protected virtual DateField EndTimeField => this.Container.GetControl<DateField>("endTimeField", true);

    /// <summary>Gets a reference to the time zone field.</summary>
    protected virtual ChoiceField TimeZoneField => this.Container.GetControl<ChoiceField>("timeZoneIdField", true);

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
      controlDescriptor.AddElementProperty("allDayCheckBox", this.AllDayCheckBox.ClientID);
      controlDescriptor.AddElementProperty("timePanel", this.TimePanel.ClientID);
      controlDescriptor.AddComponentProperty("startTimeField", this.StartTimeField.ClientID);
      controlDescriptor.AddComponentProperty("endTimeField", this.EndTimeField.ClientID);
      controlDescriptor.AddComponentProperty("timeZoneIdField", this.TimeZoneField.ClientID);
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
      new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.RecurrenceViews.Scripts.RecurrenceTimeView.js", typeof (RecurrenceTimeView).Assembly.FullName)
    };

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <exception cref="T:System.NotImplementedException"></exception>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      foreach (TimeZoneInfo systemTimeZone in TimeZoneInfo.GetSystemTimeZones())
        this.TimeZoneField.Choices.Add(new ChoiceItem()
        {
          Text = systemTimeZone.DisplayName,
          Value = systemTimeZone.Id
        });
    }

    protected override ScriptRef GetRequiredCoreScripts() => ScriptRef.JQuery;

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
