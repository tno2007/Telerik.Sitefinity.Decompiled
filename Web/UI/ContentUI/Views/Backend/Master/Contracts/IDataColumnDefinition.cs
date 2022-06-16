// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IDataColumnDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts
{
  /// <summary>Declares the contract for DataColumn definition</summary>
  public interface IDataColumnDefinition : IColumnDefinition, IDefinition
  {
    /// <summary>Gets or sets the name of the bound property.</summary>
    /// <value>The name of the bound property.</value>
    string BoundPropertyName { get; set; }

    /// <summary>Gets or sets the client template.</summary>
    /// <value>The client template.</value>
    string ClientTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether sorting is disabled for the column.
    /// </summary>
    /// <value>A value indicating whether sorting is disabled.</value>
    new bool? DisableSorting { get; set; }
  }
}
