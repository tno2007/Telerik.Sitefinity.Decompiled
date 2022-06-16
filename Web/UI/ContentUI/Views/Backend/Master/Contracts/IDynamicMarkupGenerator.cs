// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicMarkupGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts
{
  /// <summary>
  /// Defines the interface for instances that generates HTML markup for dynamic columns.
  /// </summary>
  public interface IDynamicMarkupGenerator
  {
    /// <summary>
    /// Initialize properties of the markup generator implementing <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDynamicMarkupGenerator" />
    /// with default values from the configuration definition object.
    /// </summary>
    /// <param name="definition">The definition configuration.</param>
    void Configure(IDynamicMarkupGeneratorDefinition definition);

    /// <summary>Generates HTML markup for a dynamic column.</summary>
    /// <returns>The generated HTML markup.</returns>
    string GetMarkup();

    /// <summary>
    /// Gets a value indicating whether the dynamic column should be rendered.
    /// </summary>
    /// <value><c>true</c> if the column should be rendered; otherwise, <c>false</c>.</value>
    bool Visible { get; }
  }
}
