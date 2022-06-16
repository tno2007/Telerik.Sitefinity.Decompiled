// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ContentItemWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition for the ContentItemWidget</summary>
  public class ContentItemWidgetDefinition : 
    WidgetDefinition,
    IContentItemWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private Type contentItemType;
    private string serviceBaseUrl;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ContentItemWidgetDefinition" /> class.
    /// </summary>
    public ContentItemWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.ContentItemWidgetDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentItemWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>
    /// Gets or sets the type of the content item displayed by the content item widget
    /// </summary>
    /// <value>The type of the content item.</value>
    public Type ContentItemType
    {
      get => this.ResolveProperty<Type>(nameof (ContentItemType), this.contentItemType);
      set => this.contentItemType = value;
    }

    /// <summary>Gets or sets the service base URL.</summary>
    /// <value>The service base URL.</value>
    public string ServiceBaseUrl
    {
      get => this.ResolveProperty<string>(nameof (ServiceBaseUrl), this.serviceBaseUrl);
      set => this.serviceBaseUrl = value;
    }
  }
}
