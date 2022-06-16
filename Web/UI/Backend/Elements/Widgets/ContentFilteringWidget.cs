// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentFilteringWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>Widget that raises a filter-by-a-ContentType command.</summary>
  internal class ContentFilteringWidget : CommandWidget
  {
    private const string notCorrectInterface = "The Definition of ContentFilteringWidget control does not implement IContentFilteringWidgetDefinition interface.";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Widgets.ContentFilteringWidget.ascx");
    public const string ContentFilteringWidgetScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.ContentFilteringWidget.js";
    private List<CommandWidget> contentTypeWidgets = new List<CommandWidget>();
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
    /// Gets or sets the command widgets filtering by content type.
    /// </summary>
    private List<CommandWidget> ContentTypeWidgets
    {
      get => this.contentTypeWidgets;
      set => this.contentTypeWidgets = value;
    }

    /// <summary>Gets or sets the property name to filter.</summary>
    /// <value>The property name to filter.</value>
    public string PropertyNameToFilter { get; set; }

    /// <summary>Gets or sets the name of the filter command.</summary>
    /// <value>The name of the filter command.</value>
    public string FilterCommandName { get; set; }

    /// <summary>Gets the dynamic command buttons holder.</summary>
    /// <value>The dynamic command buttons holder.</value>
    protected PlaceHolder DynamicCommandButtonsHolder => this.Container.GetControl<PlaceHolder>("dynamicCommandButtonsHolder", true);

    public override void Configure(IWidgetDefinition definition)
    {
      if (!typeof (IContentFilteringWidgetDefinition).IsAssignableFrom(definition.GetType()))
        throw new InvalidOperationException("The Definition of ContentFilteringWidget control does not implement IContentFilteringWidgetDefinition interface.");
      base.Configure(definition);
      IContentFilteringWidgetDefinition widgetDefinition = (IContentFilteringWidgetDefinition) definition;
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
      get => string.IsNullOrEmpty(this.templatePath) ? ContentFilteringWidget.layoutTemplatePath : this.templatePath;
      set => this.templatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructPredefinedDateFilteringCommandWidgets();

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
        throw new InvalidOperationException("The PropertyNameToFilter property of the ContentFilteringWidget must be set.");
      this.ContentTypeWidgets.Clear();
      this.AddConfigurableWidgets();
    }

    private void AddConfigurableWidgets()
    {
      foreach (IFilterRangeDefinition predefinedFilteringRange in this.PredefinedFilteringRanges)
      {
        CommandWidget commandWidget = new CommandWidget();
        commandWidget.Text = predefinedFilteringRange.Value;
        commandWidget.CommandArgument = this.CreateDateFilteringCommandArgument(predefinedFilteringRange.Key);
        commandWidget.CommandName = this.FilterCommandName;
        commandWidget.WrapperTagKey = HtmlTextWriterTag.Li;
        commandWidget.ButtonType = CommandButtonType.SimpleLinkButton;
        CommandWidget child = commandWidget;
        this.ContentTypeWidgets.Add(child);
        this.DynamicCommandButtonsHolder.Controls.Add((Control) child);
      }
    }

    private string CreateDateFilteringCommandArgument(string content) => new JavaScriptSerializer().Serialize((object) new Dictionary<string, string>()
    {
      {
        "contentType",
        content
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
      foreach (Control contentTypeWidget in this.ContentTypeWidgets)
        stringList.Add(contentTypeWidget.ClientID);
      behaviorDescriptor.AddProperty("_widgetIds", (object) stringList);
      behaviorDescriptor.AddProperty("_propertyNameToFilter", (object) this.PropertyNameToFilter);
      behaviorDescriptor.AddProperty("_filterCommandName", (object) this.FilterCommandName);
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
      ContentFilteringWidget contentFilteringWidget = this;
      // ISSUE: reference to a compiler-generated method
      foreach (ScriptReference scriptReference in contentFilteringWidget.\u003C\u003En__1())
        yield return scriptReference;
      yield return new ScriptReference()
      {
        Assembly = contentFilteringWidget.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.ContentFilteringWidget.js"
      };
    }
  }
}
