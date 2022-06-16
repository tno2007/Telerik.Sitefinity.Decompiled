// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ContentItemWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for the ContentItemWidget
  /// </summary>
  public class ContentItemWidgetElement : 
    WidgetElement,
    IContentItemWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ContentItemWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public ContentItemWidgetElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ContentItemWidgetDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the type of the content item displayed by the content item widget
    /// </summary>
    /// <value>The type of the content item.</value>
    [ConfigurationProperty("contentItemType", DefaultValue = typeof (ContentItem))]
    [TypeConverter(typeof (StringTypeConverter))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentItemWidgetItemTypeDescription", Title = "ContentItemWidgetItemTypeCaption")]
    public Type ContentItemType
    {
      get => (Type) this["contentItemType"];
      set => this["contentItemType"] = (object) value;
    }

    /// <summary>Gets or sets the service base URL.</summary>
    /// <value>The service base URL.</value>
    [ConfigurationProperty("serviceBaseUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentItemWidgetServiceBaseUrlDescription", Title = "ContentItemWidgetServiceBaseUrlCaption")]
    public string ServiceBaseUrl
    {
      get => (string) this["serviceBaseUrl"];
      set => this["serviceBaseUrl"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ContentItemWidgetProps
    {
      public const string contentItemType = "contentItemType";
      public const string serviceBaseUrl = "serviceBaseUrl";
    }
  }
}
