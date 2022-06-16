// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.RelatedDataFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a related data field.
  /// </summary>
  public class RelatedDataFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IRelatedDataFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public RelatedDataFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <inheritdoc />
    public override DefinitionBase GetDefinition() => (DefinitionBase) new RelatedDataFieldDefinition((ConfigElement) this);

    /// <summary>Gets the default type of the field.</summary>
    public override Type DefaultFieldType => typeof (RelatedDataField);

    /// <inheritdoc />
    [ConfigurationProperty("frontendWidgetLabel")]
    public string FrontendWidgetLabel
    {
      get => (string) this["frontendWidgetLabel"];
      set => this["frontendWidgetLabel"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("frontendWidgetType")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type FrontendWidgetType
    {
      get => (Type) this["frontendWidgetType"];
      set => this["frontendWidgetType"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("defaultFrontendWidgetType")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type DefaultFrontendWidgetType => (Type) this["defaultFrontendWidgetType"];

    /// <inheritdoc />
    [ConfigurationProperty("frontendWidgetVirtualPath")]
    public string FrontendWidgetVirtualPath
    {
      get => (string) this["frontendWidgetVirtualPath"];
      set => this["frontendWidgetVirtualPath"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("relatedDataType")]
    public string RelatedDataType
    {
      get => (string) this["relatedDataType"];
      set => this["relatedDataType"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("relatedDataProvider")]
    public string RelatedDataProvider
    {
      get => (string) this["relatedDataProvider"];
      set => this["relatedDataProvider"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("allowMultipleSelection")]
    public bool AllowMultipleSelection
    {
      get => (bool) this["allowMultipleSelection"];
      set => this["allowMultipleSelection"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal new struct FieldProps
    {
      public const string FrontendWidgetVirtualPath = "frontendWidgetVirtualPath";
      public const string FrontendWidgetType = "frontendWidgetType";
      public const string DefaultFrontendWidgetType = "defaultFrontendWidgetType";
      public const string FrontendWidgetLabel = "frontendWidgetLabel";
      public const string RelatedDataType = "relatedDataType";
      public const string RelatedDataProvider = "relatedDataProvider";
      public const string AllowMultipleSelection = "allowMultipleSelection";
    }
  }
}
