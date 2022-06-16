// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Type that constructs state widget user interface element. All widgets should inherit <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
  /// </summary>
  public class StateWidget : SimpleScriptView, IWidget
  {
    private List<RadToolBarButton> stateButtons = new List<RadToolBarButton>();
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.StateButton.ascx");
    private const string notCorrectInterface = "The Definition of {0} control does not implement {1} interface.";

    /// <summary>
    /// Gets or sets the definition for constuction the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.CommandWidget" /> control.
    /// </summary>
    /// <value>The definition.</value>
    public virtual IWidgetDefinition Definition { get; set; }

    public void Configure(IWidgetDefinition definition) => this.Definition = definition;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? StateWidget.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructWidget();

    /// <summary>Constructs the widget.</summary>
    protected void ConstructWidget()
    {
      IStateWidgetDefinition widgetDefinition = typeof (IStateWidgetDefinition).IsAssignableFrom(this.Definition.GetType()) ? this.Definition as IStateWidgetDefinition : throw new InvalidOperationException(string.Format("The Definition of {0} control does not implement {1} interface.", (object) this.GetType().FullName, (object) typeof (IStateWidgetDefinition).FullName));
      RadToolBar control = this.Container.GetControl<RadToolBar>("stateButton", true);
      control.CssClass = widgetDefinition.CssClass;
      string label1 = this.GetLabel(widgetDefinition.ResourceClassId, widgetDefinition.Text);
      control.Items.Add((RadToolBarItem) new RadToolBarButton()
      {
        CheckOnClick = false,
        ItemTemplate = (ITemplate) new StateWidget.TextTemplate(label1)
      });
      foreach (IStateCommandWidgetDefinition state in widgetDefinition.States)
      {
        RadToolBarButton radToolBarButton = new RadToolBarButton(this.GetLabel(state.ResourceClassId, state.Text), state.IsSelected, label1);
        this.stateButtons.Add(radToolBarButton);
        if (!string.IsNullOrEmpty(state.NavigateUrl))
          radToolBarButton.NavigateUrl = state.NavigateUrl;
        string label2 = this.GetLabel(state.ResourceClassId, state.ToolTip);
        radToolBarButton.ToolTip = label2;
        radToolBarButton.CssClass = state.CssClass;
        radToolBarButton.Value = state.CommandName;
        control.Items.Add((RadToolBarItem) radToolBarButton);
      }
    }

    /// <summary>Gets the reference to the RadToolBar control.</summary>
    protected internal virtual RadToolBar ToolbarControl => this.Container.GetControl<RadToolBar>("stateButton", true);

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      StateWidget stateWidget = this;
      yield return new ScriptReference()
      {
        Assembly = stateWidget.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.IWidget.js"
      };
      yield return new ScriptReference()
      {
        Assembly = stateWidget.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.ICommandWidget.js"
      };
      yield return new ScriptReference()
      {
        Assembly = stateWidget.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.StateWidget.js"
      };
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      StateWidget stateWidget = this;
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(stateWidget.GetType().FullName, stateWidget.ClientID);
      IStateWidgetDefinition definition = stateWidget.Definition as IStateWidgetDefinition;
      JavaScriptSerializer scriptSerializer = new JavaScriptSerializer();
      List<string> stringList = new List<string>();
      foreach (RadToolBarButton stateButton in stateWidget.stateButtons)
        stringList.Add(stateButton.ClientID);
      string str = scriptSerializer.Serialize((object) stringList);
      behaviorDescriptor.AddProperty("stateButtonsIds", (object) str);
      behaviorDescriptor.AddProperty("commandName", (object) definition.CommandName);
      behaviorDescriptor.AddProperty("name", (object) definition.Name);
      behaviorDescriptor.AddProperty("cssClass", (object) definition.CssClass);
      behaviorDescriptor.AddProperty("isSeparator", (object) definition.IsSeparator);
      behaviorDescriptor.AddProperty("wrapperTagId", (object) definition.WrapperTagId);
      behaviorDescriptor.AddProperty("wrapperTagName", (object) definition.WrapperTagKey);
      behaviorDescriptor.AddProperty("toolbarControlId", (object) stateWidget.ToolbarControl.ClientID);
      yield return (ScriptDescriptor) behaviorDescriptor;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Scripts
    {
      public const string StateWidget = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.StateWidget.js";
    }

    /// <summary>Implements ITemplate by rendereing a string</summary>
    private class TextTemplate : ITemplate
    {
      private string text;

      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.StateWidget.TextTemplate" /> class.
      /// </summary>
      /// <param name="text">The text.</param>
      public TextTemplate(string text) => this.text = text;

      /// <summary>
      /// When implemented by a class, defines the <see cref="T:System.Web.UI.Control" /> object that child controls and templates belong to. These child controls are in turn defined within an inline template.
      /// </summary>
      /// <param name="container">The <see cref="T:System.Web.UI.Control" /> object to contain the instances of controls from the inline template.</param>
      public void InstantiateIn(Control container) => container.Controls.Add((Control) new Literal()
      {
        Text = this.text
      });
    }
  }
}
