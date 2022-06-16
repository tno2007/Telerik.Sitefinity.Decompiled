﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.RelatedMediaFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a <see cref="T:Telerik.Sitefinity.Web.UI.Fields.RelatedMediaField" />.
  /// </summary>
  public class RelatedMediaFieldDefinition : 
    AssetsFieldDefinition,
    IRelatedMediaFieldDefinition,
    IAssetsFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string frontendWidgetLabel;
    private string frontendWidgetVirtualPath;
    private Type frontendWidgetType;
    private string relatedDataType;
    private string relatedDataProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.RelatedMediaFieldDefinition" /> class.
    /// </summary>
    public RelatedMediaFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.RelatedMediaFieldDefinition" /> class.
    /// </summary>
    public RelatedMediaFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <inheritdoc />
    public string FrontendWidgetLabel
    {
      get => this.ResolveProperty<string>(nameof (FrontendWidgetLabel), this.frontendWidgetLabel);
      set => this.frontendWidgetLabel = value;
    }

    /// <inheritdoc />
    public Type FrontendWidgetType
    {
      get => this.ResolveProperty<Type>(nameof (FrontendWidgetType), this.frontendWidgetType, this.DefaultFrontendWidgetType);
      set => this.frontendWidgetType = value;
    }

    /// <inheritdoc />
    public Type DefaultFrontendWidgetType => (Type) null;

    /// <inheritdoc />
    public string FrontendWidgetVirtualPath
    {
      get => this.ResolveProperty<string>(nameof (FrontendWidgetVirtualPath), this.frontendWidgetVirtualPath);
      set => this.frontendWidgetVirtualPath = value;
    }

    /// <inheritdoc />
    public string RelatedDataProvider
    {
      get => this.ResolveProperty<string>(nameof (RelatedDataProvider), this.relatedDataProvider);
      set => this.relatedDataProvider = value;
    }

    /// <inheritdoc />
    public string RelatedDataType
    {
      get => this.ResolveProperty<string>(nameof (RelatedDataType), this.relatedDataType);
      set => this.relatedDataType = value;
    }
  }
}