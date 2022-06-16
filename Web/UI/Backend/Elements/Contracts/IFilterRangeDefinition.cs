// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IFilterRangeDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// An interface that provides all common properties filter rane element.
  /// </summary>
  public interface IFilterRangeDefinition : IDefinition
  {
    /// <summary>Gets or sets the key.</summary>
    /// <value>The name of the control definition.</value>
    string Key { get; set; }

    /// <summary>Gets or sets the value.</summary>
    /// <value>The name of the view.</value>
    string Value { get; set; }
  }
}
