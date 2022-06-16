// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicColumnDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions
{
  /// <summary>The definition class for DataColumn</summary>
  public class DynamicColumnDefinition : 
    ColumnDefinition,
    IDynamicColumnDefinition,
    IColumnDefinition,
    IDefinition
  {
    private Type dynamicMarkupGenerator;
    private IDynamicMarkupGeneratorDefinition settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicColumnDefinition" /> class.
    /// </summary>
    public DynamicColumnDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DynamicColumnDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DynamicColumnDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DynamicColumnDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the the type of the dynamic markup generator.
    /// The class shoud implement <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicMarkupGenerator">IDynamicMarkupGenerator</see> interface.
    /// </summary>
    /// <value>The dynamic markup generator.</value>
    public Type DynamicMarkupGenerator
    {
      get => this.ResolveProperty<Type>(nameof (DynamicMarkupGenerator), this.dynamicMarkupGenerator);
      set => this.dynamicMarkupGenerator = value;
    }

    public IDynamicMarkupGeneratorDefinition GeneratorSettings
    {
      get
      {
        if (this.settings == null)
          this.settings = (IDynamicMarkupGeneratorDefinition) this.ResolveProperty<IDynamicMarkupGeneratorDefinition>(nameof (GeneratorSettings), this.settings).GetDefinition();
        return this.settings;
      }
    }

    public override string RenderMarkup() => throw new NotImplementedException();
  }
}
