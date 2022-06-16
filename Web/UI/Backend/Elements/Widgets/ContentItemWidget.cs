// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.ContentItemWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// A widget control that displays info about a content item
  /// </summary>
  public class ContentItemWidget : SimpleScriptView, IWidget
  {
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ContentUI.ContentItemWidget.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentItemWidget.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the service base URL.</summary>
    /// <value>The service base URL.</value>
    public string ServiceBaseUrl { get; set; }

    /// <summary>Gets the binder.</summary>
    /// <value>The binder.</value>
    protected virtual FormBinder Binder => this.Container.GetControl<FormBinder>("contentItemBinder", true);

    /// <summary>Gets or sets the definition.</summary>
    /// <value>The definition.</value>
    public IWidgetDefinition Definition { get; set; }

    /// <summary>Configures the specified definition.</summary>
    /// <param name="definition">The definition.</param>
    public void Configure(IWidgetDefinition definition) => this.Definition = definition;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors() => (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
    {
      this.GetContentItemWidgetDescriptor()
    };

    protected ScriptBehaviorDescriptor GetContentItemWidgetDescriptor()
    {
      ScriptBehaviorDescriptor widgetDescriptor = new ScriptBehaviorDescriptor(this.GetType().FullName, this.ClientID);
      IContentItemWidgetDefinition definition = this.Definition as IContentItemWidgetDefinition;
      widgetDescriptor.AddProperty("_binderId", (object) this.Binder.ClientID);
      widgetDescriptor.AddProperty("name", (object) definition.Name);
      widgetDescriptor.AddProperty("cssClass", (object) definition.CssClass);
      widgetDescriptor.AddProperty("isSeparator", (object) definition.IsSeparator);
      widgetDescriptor.AddProperty("wrapperTagId", (object) definition.WrapperTagId);
      widgetDescriptor.AddProperty("wrapperTagName", (object) definition.WrapperTagKey);
      widgetDescriptor.AddProperty("serviceBaseUrl", (object) VirtualPathUtility.AppendTrailingSlash(VirtualPathUtility.ToAbsolute(definition.ServiceBaseUrl)));
      return widgetDescriptor;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string fullName = typeof (ContentItemWidget).Assembly.FullName;
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
          Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.ContentItemWidget.js"
        }
      };
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Scripts
    {
      public const string ContentItemWidget = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.ContentItemWidget.js";
    }
  }
}
