// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.WidgetBar
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements
{
  /// <summary>
  /// Represents widget bar container of user interface controls constructed from <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IWidgetBarDefinition" /> definition collection.
  /// </summary>
  [ParseChildren(true, DefaultProperty = "Widgets")]
  public class WidgetBar : SimpleScriptView, IScriptControl
  {
    private const string missingVirthualPath = "You must specify either virtual path or the type of the widget.";
    private const string implementWidgetInterface = "The control of type '{0}' does not implement IWidget interface. All widget controls must implement IWidget interface.";
    internal const string WidgetBarScript = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.WidgetBar.js";
    private Collection<IWidget> widgets;
    private List<Control> sectionControls;
    private IWidgetBarDefinition definition;
    private IList<string> widgetIds;
    private ISet<string> forbiddenWidgetCommands;
    private static char[] customSeparators = new char[3]
    {
      ' ',
      '-',
      '.'
    };

    /// <summary>
    /// Gets the widget bar definiton on which widget bar will be build.
    /// </summary>
    /// <value>The widget bar definitons.</value>
    public IWidgetBarDefinition WidgetBarDefiniton
    {
      get => this.definition;
      set
      {
        this.definition = value;
        this.ChildControlsCreated = false;
        this.RecreateChildControls();
      }
    }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>Gets or sets the title wrapper tag name.</summary>
    /// <value>The title wrapper tag name.</value>
    public HtmlTextWriterTag TitleWrapperTagKey { get; set; }

    /// <summary>
    /// Gets or sets the resource class id for styling the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    public string ResourceClassId { get; set; }

    /// <summary>Gets or sets the wrapper tag id.</summary>
    /// <value>The wrapper tag id.</value>
    public string WrapperTagId { get; set; }

    /// <summary>Gets or sets the name of the wrapper tag.</summary>
    /// <value>The name of the wrapper tag.</value>
    public HtmlTextWriterTag WrapperTagKey { get; set; }

    /// <summary>Gets the collection of widget section definitions.</summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public IList<IWidgetBarSectionDefinition> Sections { get; set; }

    /// <summary>
    /// Gets a value indicating whether this instance has sections.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has sections; otherwise, <c>false</c>.
    /// </value>
    public bool HasSections { get; set; }

    /// <summary>Gets or sets the section controls.</summary>
    /// <value>The section controls.</value>
    public List<Control> SectionControls
    {
      get
      {
        if (this.sectionControls == null)
          this.sectionControls = new List<Control>();
        return this.sectionControls;
      }
    }

    /// <summary>
    /// Collection of widgets that are part of Widget Bar. All widgets should implement <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
    /// </summary>
    public Collection<IWidget> Widgets
    {
      get
      {
        if (this.widgets == null)
          this.widgets = new Collection<IWidget>();
        return this.widgets;
      }
      private set => this.widgets = value;
    }

    /// <summary>
    /// Gets or sets a secured object related to this widget bar, for applying permissions to the secured widgets.
    /// </summary>
    /// <value>The related secured object.</value>
    public ISecuredObject RelatedSecuredObject { get; set; }

    /// <summary>Free-form widgets declaration</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ITemplate WidgetsTemplate { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => string.Empty;

    protected virtual bool SkipTemplating => true;

    internal ISet<string> ForbiddenWidgetCommands
    {
      get
      {
        if (this.forbiddenWidgetCommands == null)
          this.forbiddenWidgetCommands = (ISet<string>) new HashSet<string>();
        return this.forbiddenWidgetCommands;
      }
      set => this.forbiddenWidgetCommands = value;
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      if (this.SkipTemplating)
        this.InitializeControls((GenericContainer) null);
      else
        base.CreateChildControls();
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Init" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnInit(EventArgs e) => base.OnInit(e);

    /// <summary>
    /// Initializes all controls instantiated in the layout container. This method is called at appropriate time for setting initial values and subscribing for events of layout controls.
    /// </summary>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.widgetIds == null)
        this.widgetIds = (IList<string>) new List<string>(this.Widgets.Count);
      this.ForbiddenWidgetCommands.Add("thumbnailSettingsPage");
      this.ForbiddenWidgetCommands.Add("moduleEditor");
      if (this.WidgetBarDefiniton == null)
        return;
      this.ConfigureWidgetBar();
      this.ConstructWidgetBar();
    }

    private bool IsForbiddenWidget(string commandName)
    {
      string b = this.ForbiddenWidgetCommands.FirstOrDefault<string>((Func<string, bool>) (x => x == commandName));
      return b != null && string.Equals(commandName, b, StringComparison.InvariantCulture);
    }

    private void ConfigureWidgetBar()
    {
      this.Sections = (IList<IWidgetBarSectionDefinition>) new List<IWidgetBarSectionDefinition>(this.WidgetBarDefiniton.Sections);
      this.WrapperTagId = this.WidgetBarDefiniton.WrapperTagId;
      this.WrapperTagKey = this.WidgetBarDefiniton.WrapperTagKey;
      this.Title = this.WidgetBarDefiniton.Title;
      this.CssClass = this.WidgetBarDefiniton.CssClass;
      this.ResourceClassId = this.WidgetBarDefiniton.ResourceClassId;
    }

    /// <summary>
    /// Constructs a widget control by specified widget bar definition.
    /// </summary>
    protected void ConstructWidgetBar()
    {
      Control child1 = this.ConstructWidgetBarWrapper();
      foreach (IWidgetBarSectionDefinition section in (IEnumerable<IWidgetBarSectionDefinition>) this.Sections)
      {
        IWidgetBarSectionDefinition sectionDefinition = section;
        if (!(sectionDefinition is IModuleDependentItem moduleDependentItem) || string.IsNullOrEmpty(moduleDependentItem.ModuleName) || SystemManager.IsModuleEnabled(moduleDependentItem.ModuleName))
        {
          Control child2 = this.ConstructSection(sectionDefinition);
          IWidget widget;
          foreach (IWidgetDefinition definition1 in sectionDefinition.Items)
          {
            if (definition1 is ICommandWidgetDefinition widgetDefinition && widgetDefinition.Name == "StatusFilterTemplate")
            {
              foreach (FilterInfo filterInfo in SystemManager.StatusProviderRegistry.GetFilters(this.Context.Items[(object) "sf_content_type_context_item"] as Type).ToArray<FilterInfo>())
              {
                ICommandWidgetDefinition definition2 = (ICommandWidgetDefinition) widgetDefinition.GetDefinition();
                definition2.Name = filterInfo.Key;
                definition2.CommandName = filterInfo.Key;
                definition2.Text = filterInfo.Title;
                definition2.Visible = new bool?(true);
                Control child3 = this.ConstructWidget((IWidgetDefinition) definition2, out widget);
                if (child3 != null)
                  child2.Controls.Add(child3);
              }
            }
            else if (widgetDefinition == null || !this.IsForbiddenWidget(widgetDefinition.CommandName) || ClaimsManager.IsUnrestricted())
            {
              Control child4 = this.ConstructWidget(definition1, out widget);
              if (child4 != null)
              {
                if (sectionDefinition.Visible.HasValue && !sectionDefinition.Visible.Value && widget is DynamicCommandWidget dynamicCommandWidget)
                  dynamicCommandWidget.Hidden = true;
                child2.Controls.Add(child4);
              }
            }
          }
          Control child5 = this.ConstructSectionHolderWrapper(sectionDefinition);
          child5.Controls.Add(child2);
          this.SectionControls.Add(child5);
          child5.Visible = this.Widgets.Any<IWidget>((Func<IWidget, bool>) (w => sectionDefinition.Items.Contains<IWidgetDefinition>(w.Definition)));
          child1.Controls.Add(child5);
        }
      }
      this.Controls.Add(child1);
      this.TabIndex = (short) -1;
    }

    /// <summary>
    /// Method constructs a section with tilte if exists according to provided definition.
    /// </summary>
    /// <returns></returns>
    protected Control ConstructWidgetBarWrapper()
    {
      Control control = WidgetBar.ConstrucWrapper(this.WrapperTagKey == HtmlTextWriterTag.Unknown ? HtmlTextWriterTag.Div : this.WrapperTagKey, this.CssClass, this.WrapperTagId);
      if (!string.IsNullOrEmpty(this.Title))
      {
        HtmlGenericControl child = this.ConstructTitle(this.TitleWrapperTagKey == HtmlTextWriterTag.Unknown ? HtmlTextWriterTag.H2 : this.TitleWrapperTagKey, this.Title, this.ResourceClassId);
        control.Controls.AddAt(0, (Control) child);
      }
      return control;
    }

    /// <summary>
    /// Constructs the section container wrapper. The container will contain of title of the section
    /// </summary>
    /// <param name="definition">The definition.</param>
    /// <returns></returns>
    protected Control ConstructSectionHolderWrapper(IWidgetBarSectionDefinition definition)
    {
      Control control = WidgetBar.ConstrucWrapper(HtmlTextWriterTag.Div, definition.CssClass, definition.WrapperTagId, definition.Visible);
      if (!string.IsNullOrEmpty(definition.Title))
      {
        HtmlGenericControl child = this.ConstructTitle(definition.TitleWrapperTagKey == HtmlTextWriterTag.Unknown ? HtmlTextWriterTag.H3 : definition.TitleWrapperTagKey, definition.Title, definition.ResourceClassId);
        control.Controls.AddAt(0, (Control) child);
      }
      return control;
    }

    /// <summary>
    /// Method constructs a section with tilte if exists according to provided definition.
    /// </summary>
    /// <param name="definition">The definition.</param>
    /// <returns></returns>
    protected Control ConstructSection(IWidgetBarSectionDefinition definition) => WidgetBar.ConstrucWrapper(definition.WrapperTagKey == HtmlTextWriterTag.Unknown ? HtmlTextWriterTag.Ul : definition.WrapperTagKey, string.Empty, definition.Name);

    /// <summary>Constructs the section title.</summary>
    /// <param name="titleTagName">The tag name of an element.</param>
    /// <param name="title">The title of the section.</param>
    /// <param name="resourceClassId">The resource class pageId.</param>
    /// <returns></returns>
    protected HtmlGenericControl ConstructTitle(
      HtmlTextWriterTag titleTagKey,
      string title,
      string resourceClassId)
    {
      HtmlGenericControl htmlGenericControl = new HtmlGenericControl(titleTagKey.ToString());
      htmlGenericControl.InnerText = this.GetLabel(resourceClassId, title);
      return htmlGenericControl;
    }

    /// <summary>Constructs a widget control by specified definition.</summary>
    /// <param name="definition">The definition.</param>
    protected internal Control ConstructWidget(
      IWidgetDefinition definition,
      out IWidget widget)
    {
      Control control = this.LoadWidgetControl(definition);
      if (control == null)
      {
        widget = (IWidget) null;
        return (Control) null;
      }
      widget = control as IWidget;
      if (control is CommandWidget commandWidget)
        commandWidget.TabIndex = this.TabIndex;
      if (control.Visible)
        this.Widgets.Add(widget);
      return WidgetBar.CreateWidgetControlWrapper(widget);
    }

    /// <summary>Loads the widget control.</summary>
    /// <param name="definition">The definition.</param>
    /// <returns></returns>
    internal virtual Control LoadWidgetControl(IWidgetDefinition definition) => WidgetBar.CreateWidgetControl(definition, this.RelatedSecuredObject);

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().FullName, this.ClientID);
      this.widgetIds = (IList<string>) new List<string>();
      foreach (Control widget in this.Widgets)
        this.widgetIds.Add(widget.ClientID);
      Dictionary<string, string> dictionary = new Dictionary<string, string>(this.SectionControls.Count);
      foreach (Control sectionControl in this.SectionControls)
      {
        if (!string.IsNullOrEmpty(sectionControl.ID))
          dictionary.Add(sectionControl.ID, sectionControl.ClientID);
      }
      behaviorDescriptor.AddProperty("widgetIds", (object) this.widgetIds);
      behaviorDescriptor.AddProperty("sectionIdMappings", (object) dictionary);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
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
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Scripts.WidgetBar.js"
      }
    };

    internal static Control CreateWidgetControl(IWidgetDefinition definition) => WidgetBar.CreateWidgetControl(definition, (ISecuredObject) null);

    internal static Control CreateWidgetControl(
      IWidgetDefinition definition,
      ISecuredObject relatedSecuredObject)
    {
      Exception innerException = (Exception) null;
      string message = (string) null;
      Control control = (Control) null;
      try
      {
        if (!string.IsNullOrEmpty(definition.WidgetVirtualPath))
          control = (Control) ControlUtilities.LoadControl(definition.WidgetVirtualPath);
        else if (definition.WidgetType != (Type) null)
          control = (Control) Activator.CreateInstance(definition.WidgetType);
        else
          message = "You must specify either virtual path or the type of the widget.";
      }
      catch (Exception ex)
      {
        innerException = ex;
        message = ex.Message;
      }
      if (control == null)
      {
        DefinitionBase definition1 = definition.GetDefinition();
        if (definition1 != null && !string.IsNullOrEmpty(definition1.ConfigDefinitionPath))
          message += " The path of the configuration element is: {0}.".Arrange((object) definition1.ConfigDefinitionPath);
        InvalidOperationException exceptionToHandle = new InvalidOperationException(message, innerException);
        if (Exceptions.HandleException((Exception) exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
          throw exceptionToHandle;
        return (Control) null;
      }
      if (!string.IsNullOrEmpty(definition.Name))
        ControlUtilities.SetControlIdFromName(definition.Name, control, WidgetBar.customSeparators);
      if (!typeof (IWidget).IsAssignableFrom(control.GetType()))
        throw new InvalidOperationException(string.Format("The control of type '{0}' does not implement IWidget interface. All widget controls must implement IWidget interface.", (object) control.GetType().FullName));
      if (control is CommandWidget && definition is ISecuredCommandWidget)
      {
        ISecuredCommandWidget securedCommandWidget = definition as ISecuredCommandWidget;
        if (relatedSecuredObject == null && !string.IsNullOrWhiteSpace(securedCommandWidget.RelatedSecuredObjectId))
        {
          if (!string.IsNullOrWhiteSpace(securedCommandWidget.RelatedSecuredObjectTypeName))
          {
            try
            {
              Guid result;
              Guid.TryParse(securedCommandWidget.RelatedSecuredObjectId, out result);
              if (result != Guid.Empty)
              {
                Type itemType = TypeResolutionService.ResolveType(securedCommandWidget.RelatedSecuredObjectTypeName);
                object obj = ManagerBase.GetMappedManager(itemType, securedCommandWidget.RelatedSecuredObjectProviderName).GetItem(itemType, result);
                if (obj != null)
                {
                  if (obj is ISecuredObject)
                  {
                    ((CommandWidget) control).RelatedSecuredObject = (ISecuredObject) obj;
                    goto label_28;
                  }
                  else
                    goto label_28;
                }
                else
                  goto label_28;
              }
              else
                goto label_28;
            }
            catch
            {
              if (relatedSecuredObject != null)
              {
                ((CommandWidget) control).RelatedSecuredObject = relatedSecuredObject;
                goto label_28;
              }
              else
                goto label_28;
            }
          }
        }
        if (relatedSecuredObject != null)
          ((CommandWidget) control).RelatedSecuredObject = relatedSecuredObject;
      }
label_28:
      ((IWidget) control).Configure(definition);
      return control;
    }

    internal static Control CreateWidgetControlWrapper(IWidget widgetControl)
    {
      Control widgetControlWrapper = WidgetBar.ConstrucWidgetWrapper(widgetControl.Definition);
      widgetControlWrapper.Controls.Add((Control) widgetControl);
      return widgetControlWrapper;
    }

    /// <summary>Construcs the widget wrapper.</summary>
    /// <param name="def">The definition.</param>
    /// <returns></returns>
    internal static Control ConstrucWidgetWrapper(IWidgetDefinition def) => WidgetBar.ConstrucWrapper(def.WrapperTagKey == HtmlTextWriterTag.Unknown ? HtmlTextWriterTag.Li : def.WrapperTagKey, def.CssClass, def.WrapperTagId);

    /// <summary>
    /// Helper method for constructing a <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> wrapper.
    /// </summary>
    /// <param name="wrapperTagName">Name of the wrapper tag.</param>
    /// <param name="wrapperCssClass">The wrapper CSS class.</param>
    /// <param name="wrapperTagId">The wrapper tag pageId.</param>
    internal static Control ConstrucWrapper(
      HtmlTextWriterTag wrapperTagKey,
      string wrapperCssClass,
      string wrapperTagId)
    {
      return WidgetBar.ConstrucWrapper(wrapperTagKey, wrapperCssClass, wrapperTagId, new bool?(true));
    }

    /// <summary>
    /// Helper method for constructing a <see cref="T:System.Web.UI.HtmlControls.HtmlGenericControl" /> wrapper.
    /// </summary>
    /// <param name="wrapperTagName">Name of the wrapper tag.</param>
    /// <param name="wrapperCssClass">The wrapper CSS class.</param>
    /// <param name="wrapperTagId">The wrapper tag pageId.</param>
    /// <param name="visible">Defines if the wrapper will have a display style - 'none' attribute.</param>
    internal static Control ConstrucWrapper(
      HtmlTextWriterTag wrapperTagKey,
      string wrapperCssClass,
      string wrapperTagId,
      bool? visible)
    {
      HtmlGenericControl htmlGenericControl = new HtmlGenericControl(wrapperTagKey.ToString());
      if (!string.IsNullOrEmpty(wrapperCssClass))
        htmlGenericControl.Attributes.Add("class", wrapperCssClass);
      if (!string.IsNullOrEmpty(wrapperTagId))
        ControlUtilities.SetControlIdFromName(wrapperTagId, (Control) htmlGenericControl, WidgetBar.customSeparators);
      if (visible.HasValue && !visible.Value)
        htmlGenericControl.Style.Add(HtmlTextWriterStyle.Display, "none");
      return (Control) htmlGenericControl;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Defaults
    {
      public const HtmlTextWriterTag WidgetBarWrapperTagKey = HtmlTextWriterTag.Div;
      public const HtmlTextWriterTag WidgetWrapperTagKey = HtmlTextWriterTag.Li;
      public const HtmlTextWriterTag SectionHolderWrapperTagKey = HtmlTextWriterTag.Div;
      public const HtmlTextWriterTag SectionWrapperTagKey = HtmlTextWriterTag.Ul;
      public const HtmlTextWriterTag SectionTitleTagKey = HtmlTextWriterTag.H3;
      public const HtmlTextWriterTag WidgetBarTitleTagKey = HtmlTextWriterTag.H2;
    }
  }
}
