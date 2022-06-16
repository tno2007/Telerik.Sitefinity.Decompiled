// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.WidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>A base abstract definition class for all widgets</summary>
  public abstract class WidgetDefinition : DefinitionBase, IWidgetDefinition, IDefinition
  {
    private string name;
    private string cssClass;
    private string resourceClassId;
    private string virtualPath;
    private string wrapperTagId;
    private HtmlTextWriterTag wrapperTagKey;
    private Type widgetType;
    private bool isSeparator;
    private bool? visible;
    private string text;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.WidgetDefinition" /> class.
    /// </summary>
    public WidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.WidgetDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public WidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>Gets or sets the name of the widget.</summary>
    /// <value></value>
    /// <remarks>
    /// This name has to be unique inside of a collection of widgets.
    /// </remarks>
    public string Name
    {
      get => this.ResolveProperty<string>(nameof (Name), this.name);
      set => this.name = value;
    }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    public string CssClass
    {
      get => this.ResolveProperty<string>(nameof (CssClass), this.cssClass);
      set => this.cssClass = value;
    }

    /// <summary>Gets or sets the text of the command button.</summary>
    public string Text
    {
      get => this.ResolveProperty<string>(nameof (Text), this.text);
      set => this.text = value;
    }

    /// <summary>
    /// Gets or sets the resource class pageId for styling the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    /// <summary>Gets or sets the virtual path.</summary>
    /// <value>The item virtual path.</value>
    public string WidgetVirtualPath
    {
      get => this.ResolveProperty<string>(nameof (WidgetVirtualPath), this.virtualPath);
      set => this.virtualPath = value;
    }

    /// <summary>Gets or sets the wrapper tag pageId.</summary>
    /// <value>The wrapper tag pageId.</value>
    public string WrapperTagId
    {
      get => this.ResolveProperty<string>(nameof (WrapperTagId), this.wrapperTagId);
      set => this.wrapperTagId = value;
    }

    /// <summary>Gets or sets the name of the wrapper tag.</summary>
    /// <value>The name of the wrapper tag.</value>
    public HtmlTextWriterTag WrapperTagKey
    {
      get => this.ResolveProperty<HtmlTextWriterTag>(nameof (WrapperTagKey), this.wrapperTagKey);
      set => this.wrapperTagKey = value;
    }

    /// <summary>Gets or sets the type of the widget.</summary>
    /// <value>The type of the widget.</value>
    public Type WidgetType
    {
      get => this.ResolveProperty<Type>(nameof (WidgetType), this.widgetType);
      set => this.widgetType = value;
    }

    /// <summary>
    /// Gets or sets the indication if it is a widget separator.
    /// </summary>
    /// <value>The container pageId.</value>
    public bool IsSeparator
    {
      get => this.ResolveProperty<bool>(nameof (IsSeparator), this.isSeparator);
      set => this.isSeparator = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether widget is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    public bool? Visible
    {
      get => this.ResolveProperty<bool?>(nameof (Visible), this.visible);
      set => this.visible = value;
    }

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName { get; private set; }

    /// <summary>
    /// Gets or sets the name of the view. Used for resolving property values.
    /// </summary>
    /// <value>The name of the view.</value>
    public string ViewName { get; private set; }

    internal static bool IsWidgetAccessible(IWidgetDefinition widget)
    {
      if (!(widget is ICommandWidgetDefinitionSecured definitionSecured) || string.IsNullOrEmpty(definitionSecured.RequiredPermissionSet) || string.IsNullOrEmpty(definitionSecured.RequiredActions))
        return true;
      return AppPermission.Root.IsGranted(definitionSecured.RequiredPermissionSet, definitionSecured.RequiredActions);
    }
  }
}
