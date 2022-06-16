// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DateFilteringWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Widget that raises a filter-by-a-DateTime-field command.
  /// </summary>
  public class DateFilteringWidget : CommandWidget
  {
    private const string notCorrectInterface = "The Definition of DateFilteringWidget control does not implement IDateFilteringWidgetDefinition interface.";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Widgets.DateFilteringWidget.ascx");
    public const string DateFilteringWidgetScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.DateFilteringWidget.js";
    private List<CommandWidget> dateRangeWidgets = new List<CommandWidget>();
    private string invalidDataRangeMessage;
    private string templatePath;

    /// <summary>Gets or sets the predefined filtering ranges.</summary>
    /// <value>The predefined filtering ranges.</value>
    public IEnumerable<IFilterRangeDefinition> PredefinedFilteringRanges { get; set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget" /> is clickable.
    /// </summary>
    /// <value><c>true</c> if clickable; otherwise, <c>false</c>.</value>
    protected override bool Clickable => false;

    /// <summary>
    /// Gets or sets the command widgets filtering by date range.
    /// </summary>
    private List<CommandWidget> DateRangeWidgets
    {
      get => this.dateRangeWidgets;
      set => this.dateRangeWidgets = value;
    }

    /// <summary>Gets or sets the property name to filter.</summary>
    /// <value>The property name to filter.</value>
    public string PropertyNameToFilter { get; set; }

    /// <summary>Gets or sets the name of the filter command.</summary>
    /// <value>The name of the filter command.</value>
    public string FilterCommandName { get; set; }

    /// <summary>
    /// Gets or sets the message to be show when an invalid date range is being selected.
    /// </summary>
    /// <value>The invalid date range message.</value>
    public string InvalidDateRangeMessage
    {
      get
      {
        if (string.IsNullOrEmpty(this.invalidDataRangeMessage))
          this.invalidDataRangeMessage = Res.Get<ErrorMessages>().InvalidDateRange;
        return this.invalidDataRangeMessage;
      }
      set => this.invalidDataRangeMessage = value;
    }

    /// <summary>Gets the custom range expander control.</summary>
    /// <value>The custom range expander control.</value>
    protected CommandWidget CustomRangeExpander => this.Container.GetControl<CommandWidget>("customRangeExpander", true);

    /// <summary>Gets the dynamic command buttons holder.</summary>
    /// <value>The dynamic command buttons holder.</value>
    protected PlaceHolder DynamicCommandButtonsHolder => this.Container.GetControl<PlaceHolder>("dynamicCommandButtonsHolder", true);

    /// <summary>
    /// Gets the command widget which when clicked will filter by a custom predefined date range.
    /// </summary>
    protected CommandWidget FilterCommandWidget => this.Container.GetControl<CommandWidget>("filterCommandWidget", true);

    /// <summary>
    /// Gets the command widget which when clicked will clear the filter.
    /// </summary>
    protected CommandWidget ClearFilter => this.Container.GetControl<CommandWidget>("clearFilter", true);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.WebControls.TextBox" /> control setting the date from which to filter.
    /// </summary>
    protected TextBox DateFromControl => this.Container.GetControl<TextBox>("dateFrom", true);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.WebControls.TextBox" /> control setting the date to which to filter.
    /// </summary>
    protected TextBox DateToControl => this.Container.GetControl<TextBox>("dateTo", true);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.WebControls.Panel" /> control wrapping the custom range defining controls.
    /// </summary>
    protected Panel CustomRangePanel => this.Container.GetControl<Panel>("customRangePanel", true);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.WebControls.Panel" /> control wrapping the custom range defining controls.
    /// </summary>
    protected Message MessageControl => this.Container.GetControl<Message>("message", true);

    public override void Configure(IWidgetDefinition definition)
    {
      if (!typeof (IDateFilteringWidgetDefinition).IsAssignableFrom(definition.GetType()))
        throw new InvalidOperationException("The Definition of DateFilteringWidget control does not implement IDateFilteringWidgetDefinition interface.");
      base.Configure(definition);
      IDateFilteringWidgetDefinition widgetDefinition = (IDateFilteringWidgetDefinition) definition;
      this.FilterCommandName = string.IsNullOrEmpty(widgetDefinition.CommandName) ? "filter" : widgetDefinition.CommandName;
      this.PredefinedFilteringRanges = widgetDefinition.PredefinedFilteringRanges;
      this.PropertyNameToFilter = widgetDefinition.PropertyNameToFilter;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the layout template path.</summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(this.templatePath) ? DateFilteringWidget.layoutTemplatePath : this.templatePath;
      set => this.templatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ConstructPredefinedDateFilteringCommandWidgets();
      this.ConfigureControlsState();
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Ul;

    private void ConstructPredefinedDateFilteringCommandWidgets()
    {
      if (string.IsNullOrEmpty(this.PropertyNameToFilter))
        throw new InvalidOperationException("The PropertyNameToFilter property of the DateFilteringWidget must be set.");
      this.DateRangeWidgets.Clear();
      this.AddStaticWidgets();
      this.AddConfigurableWidgets();
    }

    private void AddStaticWidgets()
    {
      this.DateRangeWidgets.Add(this.ClearFilter);
      this.DateRangeWidgets.Add(this.CustomRangeExpander);
      this.DateRangeWidgets.Add(this.FilterCommandWidget);
    }

    private void AddConfigurableWidgets()
    {
      foreach (IFilterRangeDefinition predefinedFilteringRange in this.PredefinedFilteringRanges)
      {
        CommandWidget commandWidget = new CommandWidget();
        commandWidget.Text = predefinedFilteringRange.Value;
        commandWidget.CommandArgument = this.CreateDateFilteringCommandArgument(TimeSpan.Parse(predefinedFilteringRange.Key));
        commandWidget.CommandName = this.FilterCommandName;
        commandWidget.WrapperTagKey = HtmlTextWriterTag.Li;
        commandWidget.ButtonType = CommandButtonType.SimpleLinkButton;
        CommandWidget child = commandWidget;
        this.DateRangeWidgets.Add(child);
        this.DynamicCommandButtonsHolder.Controls.Add((Control) child);
      }
    }

    private void ConfigureControlsState()
    {
      this.CustomRangePanel.Style.Add(HtmlTextWriterStyle.Display, "none");
      this.ClearFilter.CommandArgument = "{ filterExpression: '' }";
    }

    private string CreateDateFilteringCommandArgument(TimeSpan timeSpan) => new JavaScriptSerializer().Serialize((object) new Dictionary<string, TimeSpan>()
    {
      {
        "dateRange",
        timeSpan
      }
    });

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = base.GetScriptDescriptors().First<ScriptDescriptor>() as ScriptBehaviorDescriptor;
      List<string> stringList = new List<string>();
      foreach (Control dateRangeWidget in this.DateRangeWidgets)
        stringList.Add(dateRangeWidget.ClientID);
      behaviorDescriptor.AddProperty("_widgetIds", (object) stringList);
      behaviorDescriptor.AddProperty("_propertyNameToFilter", (object) this.PropertyNameToFilter);
      behaviorDescriptor.AddProperty("_filterCommandName", (object) this.FilterCommandName);
      behaviorDescriptor.AddProperty("invalidDateRangeMessage", (object) this.InvalidDateRangeMessage);
      behaviorDescriptor.AddElementProperty("customRangeExpander", this.CustomRangeExpander.ClientID);
      behaviorDescriptor.AddElementProperty("customRangePanel", this.CustomRangePanel.ClientID);
      behaviorDescriptor.AddElementProperty("filterCommandWidget", this.FilterCommandWidget.ClientID);
      behaviorDescriptor.AddProperty("dateFromId", (object) this.DateFromControl.ClientID);
      behaviorDescriptor.AddProperty("dateToId", (object) this.DateToControl.ClientID);
      behaviorDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      yield return (ScriptDescriptor) behaviorDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      DateFilteringWidget dateFilteringWidget = this;
      // ISSUE: reference to a compiler-generated method
      foreach (ScriptReference scriptReference in dateFilteringWidget.\u003C\u003En__1())
        yield return scriptReference;
      yield return new ScriptReference()
      {
        Assembly = dateFilteringWidget.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.DateFilteringWidget.js"
      };
      yield return new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-1.12.1.custom.min.js", "Telerik.Sitefinity.Resources");
      yield return new ScriptReference("Telerik.Sitefinity.Resources.Scripts.jquery-ui-timepicker-addon.js", "Telerik.Sitefinity.Resources");
    }
  }
}
