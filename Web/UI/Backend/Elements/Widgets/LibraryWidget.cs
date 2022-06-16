// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>
  /// Represents a widget displaying information about a library. Used as a header in the library view
  /// of items in the Library module backend
  /// </summary>
  public class LibraryWidget : ContentItemWidget
  {
    private const string libraryWidget = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.LibraryWidget.js";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Libraries.LibraryWidget.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.LibraryWidget" /> class.
    /// </summary>
    public LibraryWidget() => this.LayoutTemplatePath = LibraryWidget.layoutTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

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
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> list = base.GetScriptReferences().ToList<ScriptReference>();
      list.Add(new ScriptReference()
      {
        Assembly = this.GetType().Assembly.FullName,
        Name = "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.Scripts.LibraryWidget.js"
      });
      return (IEnumerable<ScriptReference>) list;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor widgetDescriptor = this.GetContentItemWidgetDescriptor();
      ILibraryWidgetDefinition definition = this.Definition as ILibraryWidgetDefinition;
      widgetDescriptor.AddProperty("_showActionMenu", (object) definition.ShowActionMenu);
      widgetDescriptor.AddProperty("_itemName", (object) definition.ItemName);
      widgetDescriptor.AddProperty("_itemsName", (object) definition.ItemsName);
      widgetDescriptor.AddProperty("_libraryName", (object) definition.LibraryName);
      widgetDescriptor.AddProperty("_supportsReordering", (object) definition.SupportsReordering);
      return (IEnumerable<ScriptDescriptor>) new List<ScriptDescriptor>()
      {
        (ScriptDescriptor) widgetDescriptor
      };
    }
  }
}
