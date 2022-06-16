// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ActionMenuWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Type that constructs action menu widget user interface element. All widgets should inherit <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
  /// </summary>
  public class ActionMenuWidget : SimpleScriptView, IWidget
  {
    private const string notCorrectInterface = "The Definition of {0} control does not implement {1} interface.";
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Buttons.MenuButton.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ActionMenuWidget.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets a referance to the menu.</summary>
    /// <value>The menu button.</value>
    internal virtual RadMenu MenuButton => this.Container.GetControl<RadMenu>("menuButton", true);

    /// <summary>Gets or sets the name of the widget.</summary>
    /// <remarks>
    /// This name has to be unique inside of a collection of widgets.
    /// </remarks>
    public virtual string Name { get; set; }

    /// <summary>
    /// Gets or sets the indication if it is a widget separator.
    /// </summary>
    /// <value>The container pageId.</value>
    public virtual bool IsSeparator { get; set; }

    /// <summary>Gets or sets the wrapper tag pageId.</summary>
    /// <value>The wrapper tag pageId.</value>
    public virtual string WrapperTagId { get; set; }

    /// <summary>Gets or sets the name of the wrapper tag.</summary>
    /// <value>The name of the wrapper tag.</value>
    public virtual HtmlTextWriterTag WrapperTagKey { get; set; }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    public new virtual string CssClass { get; set; }

    /// <summary>Gets or sets the definition.</summary>
    /// <value>The definition.</value>
    public virtual IWidgetDefinition Definition { get; set; }

    /// <summary>
    /// Configures the action menu widget depending on the specified definition.
    /// </summary>
    /// <param name="definition">The definition.</param>
    public void Configure(IWidgetDefinition definition)
    {
      this.Definition = definition;
      this.Name = definition.Name;
      this.CssClass = definition.CssClass;
      this.WrapperTagId = definition.WrapperTagId;
      this.WrapperTagKey = definition.WrapperTagKey;
      this.IsSeparator = definition.IsSeparator;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructWidget();

    /// <summary>
    /// Method responsible for constructing the menu widget based on <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IActionMenuWidgetDefinition" /> contract definition.
    /// </summary>
    protected internal virtual void ConstructWidget()
    {
      if (!typeof (IActionMenuWidgetDefinition).IsAssignableFrom(this.Definition.GetType()))
        throw new InvalidOperationException(string.Format("The Definition of {0} control does not implement {1} interface.", (object) this.GetType().FullName, (object) typeof (IActionMenuWidgetDefinition).FullName));
      this.ConstructMenu();
    }

    /// <summary>
    /// Method constructs the menu according to specified <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IActionMenuWidgetDefinition" />.
    /// </summary>
    protected internal virtual void ConstructMenu()
    {
      RadMenuItem radMenuItem1 = new RadMenuItem(this.GetMainMenuText());
      this.MenuButton.Items.Add(radMenuItem1);
      foreach (IWidgetDefinition menuItem in (this.Definition as IActionMenuWidgetDefinition).MenuItems)
      {
        if (WidgetDefinition.IsWidgetAccessible(menuItem))
        {
          RadMenuItem radMenuItem2 = this.ConstructMenuItem(menuItem);
          radMenuItem1.Items.Add(radMenuItem2);
        }
      }
    }

    /// <summary>
    /// Method constructs a menu item for a specified definition.
    /// </summary>
    /// <param name="itemDefinition">The menu item definition.</param>
    /// <returns></returns>
    protected internal virtual RadMenuItem ConstructMenuItem(
      IWidgetDefinition itemDefinition)
    {
      Type type = itemDefinition.GetType();
      bool flag = typeof (ICommandWidgetDefinition).IsAssignableFrom(type);
      if ((flag ? 1 : (typeof (ILiteralWidgetDefinition).IsAssignableFrom(type) ? 1 : 0)) == 0)
        throw new InvalidOperationException(string.Format("An item from MenuItems property collection can be used to construct widget only from {0} or {1} inheritors.", (object) typeof (ICommandWidgetDefinition).FullName, (object) typeof (ILiteralWidgetDefinition).FullName));
      RadMenuItem radMenuItem1 = new RadMenuItem();
      radMenuItem1.Text = this.GetLabel(itemDefinition.ResourceClassId, itemDefinition.Text);
      radMenuItem1.CssClass = itemDefinition.CssClass;
      radMenuItem1.IsSeparator = itemDefinition.IsSeparator;
      RadMenuItem radMenuItem2 = radMenuItem1;
      if (flag)
        radMenuItem2.Value = (itemDefinition as ICommandWidgetDefinition).CommandName;
      return radMenuItem2;
    }

    /// <summary>Gets the texon of the main menu.</summary>
    /// <returns>The text that should be displayed as the text of the menu button itself.</returns>
    protected internal virtual string GetMainMenuText()
    {
      IActionMenuWidgetDefinition definition = this.Definition as IActionMenuWidgetDefinition;
      return string.IsNullOrEmpty(definition.Text) ? definition.Text : Res.ResolveLocalizedValue(definition.ResourceClassId, definition.Text);
    }

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
      behaviorDescriptor.AddProperty("name", (object) this.Name);
      behaviorDescriptor.AddProperty("cssClass", (object) this.CssClass);
      behaviorDescriptor.AddProperty("isSeparator", (object) this.IsSeparator);
      behaviorDescriptor.AddProperty("wrapperTagId", (object) this.WrapperTagId);
      behaviorDescriptor.AddProperty("wrapperTagKey", (object) this.WrapperTagKey);
      behaviorDescriptor.AddProperty("_menuClientId", (object) this.MenuButton.ClientID);
      behaviorDescriptor.AddProperty("_spanClientId", (object) this.ClientID);
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
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ActionMenuWidget).Assembly.FullName;
      return (IEnumerable<ScriptReference>) new ScriptReference[3]
      {
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.IWidget.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.ICommandWidget.js"
        },
        new ScriptReference()
        {
          Assembly = fullName,
          Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.ActionMenuWidget.js"
        }
      };
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Scripts
    {
      public const string ActionMenuWidget = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.ActionMenuWidget.js";
    }
  }
}
