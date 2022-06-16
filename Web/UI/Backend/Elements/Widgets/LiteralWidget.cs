// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LiteralWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Type that construct literal widget user interface element. All widgets should inherit <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
  /// </summary>
  public class LiteralWidget : SimpleScriptView, IWidget
  {
    /// <summary>The layout of template</summary>
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.LiteralWidget.ascx");
    private const string notCorrectInterface = "The Definition of {0} control does not implement {1} interface.";

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.ConstructWidget();

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? LiteralWidget.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Constructs the literal widget.</summary>
    protected internal virtual void ConstructWidget()
    {
      ILiteralWidgetDefinition widgetDefinition = typeof (ILiteralWidgetDefinition).IsAssignableFrom(this.Definition.GetType()) ? this.Definition as ILiteralWidgetDefinition : throw new InvalidOperationException(string.Format("The Definition of {0} control does not implement {1} interface.", (object) this.GetType().FullName, (object) typeof (ILiteralWidgetDefinition).FullName));
      this.WidgetItem.Controls.Add((Control) new Literal()
      {
        Text = this.GetLabel(widgetDefinition.ResourceClassId, widgetDefinition.Text)
      });
    }

    /// <summary>Gets the widget item.</summary>
    /// <value>The widget item.</value>
    protected internal virtual HtmlGenericControl WidgetItem => this.Container.GetControl<HtmlGenericControl>("literalItemWidget", true);

    public virtual IWidgetDefinition Definition { get; set; }

    public void Configure(IWidgetDefinition definition) => this.Definition = definition;

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new ScriptReference[2]
    {
      new ScriptReference()
      {
        Assembly = this.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.IWidget.js"
      },
      new ScriptReference()
      {
        Assembly = this.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.LiteralWidget.js"
      }
    };

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().FullName, this.ClientID);
      ILiteralWidgetDefinition definition = this.Definition as ILiteralWidgetDefinition;
      behaviorDescriptor.AddProperty("name", (object) definition.Name);
      behaviorDescriptor.AddProperty("cssClass", (object) definition.CssClass);
      behaviorDescriptor.AddProperty("isSeparator", (object) definition.IsSeparator);
      behaviorDescriptor.AddProperty("wrapperTagId", (object) definition.WrapperTagId);
      behaviorDescriptor.AddProperty("wrapperTagName", (object) definition.WrapperTagKey);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Scripts
    {
      public const string LiteralWidget = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.LiteralWidget.js";
    }
  }
}
