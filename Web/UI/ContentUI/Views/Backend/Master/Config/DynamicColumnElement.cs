// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DynamicColumnElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>Configuration element for DataColumnDefinitions</summary>
  public class DynamicColumnElement : 
    ColumnElement,
    IDynamicColumnDefinition,
    IColumnDefinition,
    IDefinition
  {
    private const string DynamicMarkupGeneratorPropertyName = "dynamicMarkupGenerator";
    private const string GeneratorSettingsPropertyName = "generatorSettings";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DynamicColumnElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new DynamicColumnDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the the type of the dynamic markup provider.
    /// The class shoud implement <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicMarkupGenerator">IDynamicMarkupProvider</see> interface.
    /// </summary>
    /// <value>The dynamic markup provider.</value>
    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("dynamicMarkupGenerator")]
    public Type DynamicMarkupGenerator
    {
      get => (Type) this["dynamicMarkupGenerator"];
      set => this["dynamicMarkupGenerator"] = (object) value;
    }

    [ConfigurationProperty("generatorSettings")]
    public DynamicMarkupGeneratorElement GeneratorSettingsElement
    {
      get => (DynamicMarkupGeneratorElement) this["generatorSettings"];
      set => this["generatorSettings"] = (object) value;
    }

    public IDynamicMarkupGeneratorDefinition GeneratorSettings => (IDynamicMarkupGeneratorDefinition) this.GeneratorSettingsElement;
  }
}
