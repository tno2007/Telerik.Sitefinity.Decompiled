// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>The configuration element for widgets</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetDescription", Title = "WidgetCaption")]
  public abstract class WidgetElement : DefinitionConfigElement, IWidgetDefinition, IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.WidgetElement" /> class.
    /// </summary>
    /// <param name="element">The parent element.</param>
    public WidgetElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets or sets the name of the widget.</summary>
    /// <value></value>
    /// <remarks>
    /// This name has to be unique inside of a collection of widgets.
    /// </remarks>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetNameDescription", Title = "WidgetNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the pageId of the container element.</summary>
    /// <value>The container pageId.</value>
    [ConfigurationProperty("containerId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetContainerIdDescription", Title = "WidgetContainerIdCaption")]
    public string ContainerId
    {
      get => (string) this["containerId"];
      set => this["containerId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the CSS class rendered in the widget's html.
    /// </summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("cssClass", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetCssClassDescription", Title = "WidgetCssClassCaption")]
    public string CssClass
    {
      get => (string) this["cssClass"];
      set => this["cssClass"] = (object) value;
    }

    /// <summary>Gets or sets the text of the command button.</summary>
    /// <value></value>
    [ConfigurationProperty("text")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetCommandTextDescription", Title = "WidgetCommandTextCaption")]
    public string Text
    {
      get => (string) this["text"];
      set => this["text"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID for retrieving localized strings.
    /// </summary>
    /// <value>The CSS class.</value>
    [ConfigurationProperty("resourceclassid", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceclassid"];
      set => this["resourceclassid"] = (object) value;
    }

    /// <summary>Gets or sets the item template path.</summary>
    /// <value>The item template path.</value>
    [ConfigurationProperty("templatePath", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetVirtualPathDescription", Title = "WidgetVirtualPathCaption")]
    public string WidgetVirtualPath
    {
      get => (string) this["templatePath"];
      set => this["templatePath"] = (object) value;
    }

    /// <summary>Gets or sets the wrapper tag pageId.</summary>
    /// <value>The wrapper tag pageId.</value>
    [ConfigurationProperty("wrapperTagId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetWrapperTagIdDescription", Title = "WidgetWrapperTagIdCaption")]
    public string WrapperTagId
    {
      get => (string) this["wrapperTagId"];
      set => this["wrapperTagId"] = (object) value;
    }

    /// <summary>Gets or sets the name of the wrapper tag.</summary>
    /// <value>The name of the wrapper tag.</value>
    [ConfigurationProperty("wrapperTagKey", DefaultValue = HtmlTextWriterTag.Unknown)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetWrapperTagNameDescription", Title = "WidgetWrapperTagNameCaption")]
    public HtmlTextWriterTag WrapperTagKey
    {
      get => (HtmlTextWriterTag) this["wrapperTagKey"];
      set => this["wrapperTagKey"] = (object) value;
    }

    /// <summary>Gets or sets the type of the widget.</summary>
    /// <value>The type of the widget.</value>
    [ConfigurationProperty("widgetType", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetTypeDescription", Title = "WidgetTypeCaption")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type WidgetType
    {
      get => (Type) this["widgetType"];
      set => this["widgetType"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the indication if it is a widget separator.
    /// </summary>
    /// <value>The container pageId.</value>
    [ConfigurationProperty("isSeparator", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WidgetIsSeparatorDescription", Title = "WidgetIsSeparatorCaption")]
    public bool IsSeparator
    {
      get => this["isSeparator"] != null && (bool) this["isSeparator"];
      set => this["isSeparator"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether widget is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("visible")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "VisibleDescription", Title = "VisibleCaption")]
    public bool? Visible
    {
      get => (bool?) this["visible"];
      set => this["visible"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct WidgetProps
    {
      public const string Name = "name";
      public const string ContainerId = "containerId";
      public const string CssClass = "cssClass";
      public const string ResourceClassId = "resourceclassid";
      public const string WidgetVirtualPath = "templatePath";
      public const string WrapperTagId = "wrapperTagId";
      public const string WrapperTagKey = "wrapperTagKey";
      public const string WidgetType = "widgetType";
      public const string IsSeparator = "isSeparator";
      public const string Visible = "visible";
      public const string text = "text";
    }
  }
}
