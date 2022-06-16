// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.CommandColumnDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct CommandColumn
  /// </summary>
  public class CommandColumnDefinition : 
    ColumnDefinition,
    ICommandColumnDefinition,
    IColumnDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ColumnDefinition" /> class.
    /// </summary>
    public CommandColumnDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public CommandColumnDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public CommandColumnDefinition GetDefinition() => this;

    /// <summary>
    /// This is an abstract method that each concrete implementation of the ColumnDefinition
    /// class must implement. Namely, since our grid is bound on the client side (ItemsGrid), we don't
    /// need actual controls from these definitions, but rather only the markup.
    /// </summary>
    /// <returns></returns>
    public override string RenderMarkup() => throw new NotImplementedException();
  }
}
