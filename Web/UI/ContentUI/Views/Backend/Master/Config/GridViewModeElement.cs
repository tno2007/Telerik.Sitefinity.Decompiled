// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.GridViewModeElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>The configuration element for GridViewModeDefinition</summary>
  public class GridViewModeElement : 
    ViewModeElement,
    IGridViewModeDefinition,
    IViewModeDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public GridViewModeElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new GridViewModeDefinition((ConfigElement) this);

    /// <summary>
    /// Gets the collection of column config elements that are displayed on the view.
    /// </summary>
    [ConfigurationProperty("columns")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BackendGridColumnsDescription", Title = "BackendGridColumnsCaption")]
    public ConfigElementDictionary<string, ColumnElement> ColumnsConfig => (ConfigElementDictionary<string, ColumnElement>) this["columns"];

    /// <summary>
    /// Gets the collection of column definitions that are displayed on the view.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IColumnDefinition> Columns => this.ColumnsConfig.Elements.Select<ColumnElement, IColumnDefinition>((Func<ColumnElement, IColumnDefinition>) (c => (IColumnDefinition) c.ToDefinition())).ToList<IColumnDefinition>();
  }
}
