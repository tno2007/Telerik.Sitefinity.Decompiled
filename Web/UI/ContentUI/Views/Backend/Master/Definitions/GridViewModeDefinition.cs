// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.GridViewModeDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions
{
  /// <summary>The definition class for DataColumn</summary>
  public class GridViewModeDefinition : 
    ViewModeDefinition,
    IGridViewModeDefinition,
    IViewModeDefinition,
    IDefinition
  {
    private List<IColumnDefinition> columns;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ColumnDefinition" /> class.
    /// </summary>
    public GridViewModeDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions.ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public GridViewModeDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public GridViewModeDefinition GetDefinition() => this;

    /// <summary>
    /// Gets the collection of column definitions that are displayed on the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IColumnDefinition> Columns
    {
      get
      {
        if (this.columns == null)
        {
          IList<IColumnDefinition> columnDefinitionList = this.ResolveProperty<IList<IColumnDefinition>>(nameof (Columns), (IList<IColumnDefinition>) this.columns);
          this.columns = new List<IColumnDefinition>();
          if (columnDefinitionList != null)
          {
            foreach (IDefinition definition in (IEnumerable<IColumnDefinition>) columnDefinitionList)
              this.columns.Add((IColumnDefinition) definition.GetDefinition());
          }
        }
        return this.columns;
      }
    }
  }
}
