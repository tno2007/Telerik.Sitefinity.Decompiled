// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicColumnDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts
{
  /// <summary>Declares the contract for DynamicColumn definition</summary>
  public interface IDynamicColumnDefinition : IColumnDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the the type of the dynamic markup provider.
    /// The class shoud implement <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicMarkupGenerator">IDynamicMarkupProvider</see> interface.
    /// </summary>
    /// <value>The dynamic markup provider.</value>
    Type DynamicMarkupGenerator { get; set; }

    IDynamicMarkupGeneratorDefinition GeneratorSettings { get; }
  }
}
