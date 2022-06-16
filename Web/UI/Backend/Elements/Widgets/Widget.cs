// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Widget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Type that construct literal widget user interface element. All widgets should inherit <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.IWidget" /> interface.
  /// </summary>
  public abstract class Widget : SimpleScriptView, IWidget
  {
    public const string IWidgetScriptPath = "Telerik.Sitefinity.Web.SitefinityJS.UI.IWidget.js";
    public const string WidgetScriptPath = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.Widget.js";

    /// <summary>Gets or sets the name of the widget.</summary>
    /// <remarks>
    /// This name has to be unique inside of a collection of widgets.
    /// </remarks>
    public virtual string Name { get; set; }

    /// <summary>Gets or sets the text of the command button.</summary>
    public virtual string Text { get; set; }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    public new virtual string CssClass { get; set; }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The CSS class.</value>
    public virtual string ResourceClassId { get; set; }

    /// <summary>Gets or sets the virtual path.</summary>
    /// <value>The item virtual path.</value>
    public virtual string WidgetVirtualPath { get; set; }

    /// <summary>Gets or sets the wrapper tag pageId.</summary>
    /// <value>The wrapper tag pageId.</value>
    public virtual string WrapperTagId { get; set; }

    /// <summary>Gets or sets the name of the wrapper tag.</summary>
    /// <value>The name of the wrapper tag.</value>
    public virtual HtmlTextWriterTag WrapperTagKey { get; set; }

    /// <summary>Gets or sets the type of the widget.</summary>
    /// <value>The type of the widget.</value>
    [TypeConverter(typeof (StringTypeConverter))]
    public virtual Type WidgetType { get; set; }

    /// <summary>
    /// Gets or sets the indication if it is a widget separator.
    /// </summary>
    /// <value>The container pageId.</value>
    public virtual bool IsSeparator { get; set; }

    public virtual IWidgetDefinition Definition { get; set; }

    public virtual void Configure(IWidgetDefinition definition)
    {
      this.Definition = definition;
      this.Name = definition.Name;
      this.Text = this.ResolveLocalizedValue(definition.Text);
      this.CssClass = definition.CssClass;
      this.ResourceClassId = definition.ResourceClassId;
      this.WidgetVirtualPath = definition.WidgetVirtualPath;
      this.WrapperTagId = definition.WrapperTagId;
      this.WrapperTagKey = definition.WrapperTagKey;
      this.WidgetType = definition.WidgetType;
      this.IsSeparator = definition.IsSeparator;
      if (!definition.Visible.HasValue)
        return;
      this.Visible = definition.Visible.Value;
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.
    /// </returns>
    protected override HtmlTextWriterTag TagKey => this.WrapperTagKey != HtmlTextWriterTag.Unknown ? this.WrapperTagKey : base.TagKey;

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (Widget).Assembly.FullName;
      yield return new ScriptReference()
      {
        Assembly = assembly,
        Name = "Telerik.Sitefinity.Web.SitefinityJS.UI.IWidget.js"
      };
      yield return new ScriptReference()
      {
        Assembly = assembly,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.Widget.js"
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
      // ISSUE: reference to a compiler-generated field
      int num = this.\u003C\u003E1__state;
      Widget widget = this;
      if (num != 0)
      {
        if (num != 1)
          return false;
        // ISSUE: reference to a compiler-generated field
        this.\u003C\u003E1__state = -1;
        return false;
      }
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = -1;
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(widget.ScriptDescriptorTypeName, widget.ClientID);
      behaviorDescriptor.AddProperty("name", (object) widget.Name);
      behaviorDescriptor.AddProperty("cssClass", (object) widget.CssClass);
      behaviorDescriptor.AddProperty("isSeparator", (object) widget.IsSeparator);
      behaviorDescriptor.AddProperty("wrapperTagId", (object) widget.WrapperTagId);
      behaviorDescriptor.AddProperty("wrapperTagKey", (object) widget.WrapperTagKey);
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E2__current = (ScriptDescriptor) behaviorDescriptor;
      // ISSUE: reference to a compiler-generated field
      this.\u003C\u003E1__state = 1;
      return true;
    }

    protected virtual string ResolveLocalizedValue(string key) => Res.ResolveLocalizedValue(this.ResourceClassId, key);
  }
}
