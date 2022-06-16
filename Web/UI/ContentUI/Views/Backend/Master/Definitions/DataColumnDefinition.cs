// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.DataColumnDefinition
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
  public class DataColumnDefinition : 
    ColumnDefinition,
    IDataColumnDefinition,
    IColumnDefinition,
    IDefinition
  {
    private string boundPropertyName;
    private string clientTemplate;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ColumnDefinition" /> class.
    /// </summary>
    public DataColumnDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DataColumnDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DataColumnDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the bound property.</summary>
    /// <value>The name of the bound property.</value>
    public string BoundPropertyName
    {
      get => this.ResolveProperty<string>(nameof (BoundPropertyName), this.boundPropertyName);
      set => this.boundPropertyName = value;
    }

    /// <summary>Gets or sets the client template.</summary>
    /// <value>The client template.</value>
    public string ClientTemplate
    {
      get => this.ResolveProperty<string>(nameof (ClientTemplate), this.clientTemplate);
      set => this.clientTemplate = value;
    }

    /// <summary>
    /// Implementation of the base abstract method specific for DataColumnDefinition. Namely, since our grid is
    /// bound on the client side (ItemsGrid), we don't need actual controls from these definitions, but rather only the markup.
    /// </summary>
    /// <returns></returns>
    public override string RenderMarkup() => throw new NotImplementedException();
  }
}
