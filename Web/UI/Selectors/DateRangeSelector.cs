// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DateRangeSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control that allows specifing of specific date ranges - last 1 day, 3 days, months, etc
  /// </summary>
  public class DateRangeSelector : SimpleScriptView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Selectors.DateRangeSelector.ascx");
    private const string controlScript = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.DateRangeSelector.js";
    private const string customRangeValue = "_CustomRange_";

    /// <summary>
    /// The title for the radio buttons that represent the date ranges
    /// </summary>
    public string DateRangesTitle { get; set; }

    /// <summary>Gets or sets the title of the control.</summary>
    public string Title { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DateRangeSelector.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Represents the title of the control</summary>
    protected SitefinityLabel TitleLabel => this.Container.GetControl<SitefinityLabel>("titleLabel", true);

    /// <summary>Represents the radio buttons with the date ranges</summary>
    protected ChoiceField DateRangesChoiceField => this.Container.GetControl<ChoiceField>("dateRangesChoiceField", true);

    /// <summary>Represents the date picker for "From date"</summary>
    protected DateField FromDateField => this.Container.GetControl<DateField>("fromDateField", true);

    /// <summary>Represents the date picker for "To date"</summary>
    protected DateField ToDateField => this.Container.GetControl<DateField>("toDateField", true);

    /// <summary>Represents the ordered list with the date pickers</summary>
    protected HtmlGenericControl DatesPanel => this.Container.GetControl<HtmlGenericControl>("datesPanel", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (!string.IsNullOrEmpty(this.Title))
        this.TitleLabel.Text = this.Title;
      if (!string.IsNullOrEmpty(this.DateRangesTitle))
        this.DateRangesChoiceField.Title = this.DateRangesTitle;
      this.InitializeDateRanges();
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddProperty("_customRangeValue", (object) "_CustomRange_");
      controlDescriptor.AddElementProperty("datesPanel", this.DatesPanel.ClientID);
      controlDescriptor.AddComponentProperty("dateRangesChoiceField", this.DateRangesChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("fromDateField", this.FromDateField.ClientID);
      controlDescriptor.AddComponentProperty("toDateField", this.ToDateField.ClientID);
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
      new ScriptReference()
      {
        Assembly = this.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Selectors.Scripts.DateRangeSelector.js"
      }
    };

    /// <summary>Adds the choice items for the specific date ranges</summary>
    private void InitializeDateRanges()
    {
      Collection<ChoiceItem> choices = this.DateRangesChoiceField.Choices;
      choices.Add(new ChoiceItem()
      {
        Value = "",
        Text = Res.Get<Labels>().AllTheTime
      });
      choices.Add(new ChoiceItem()
      {
        Value = "DateTime.UtcNow.AddDays(-1.0)",
        Text = Res.Get<Labels>().Last1Day
      });
      choices.Add(new ChoiceItem()
      {
        Value = "DateTime.UtcNow.AddDays(-3.0)",
        Text = Res.Get<Labels>().Last3Days
      });
      choices.Add(new ChoiceItem()
      {
        Value = "DateTime.UtcNow.AddDays(-7.0)",
        Text = Res.Get<Labels>().Last1Week
      });
      choices.Add(new ChoiceItem()
      {
        Value = "DateTime.UtcNow.AddMonths(-1)",
        Text = Res.Get<Labels>().Last1Month
      });
      choices.Add(new ChoiceItem()
      {
        Value = "DateTime.UtcNow.AddMonths(-6)",
        Text = Res.Get<Labels>().Last6Months
      });
      choices.Add(new ChoiceItem()
      {
        Value = "DateTime.UtcNow.AddYears(-1)",
        Text = Res.Get<Labels>().Last1Year
      });
      choices.Add(new ChoiceItem()
      {
        Value = "DateTime.UtcNow.AddYears(-2)",
        Text = Res.Get<Labels>().Last2Years
      });
      choices.Add(new ChoiceItem()
      {
        Value = "DateTime.UtcNow.AddYears(-5)",
        Text = Res.Get<Labels>().Last5Years
      });
      choices.Add(new ChoiceItem()
      {
        Value = "_CustomRange_",
        Text = Res.Get<Labels>().CustomRange
      });
    }
  }
}
