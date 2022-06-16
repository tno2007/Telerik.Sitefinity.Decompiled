// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DateFilteringWidgetDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Config;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>Represents a definition of the command widget</summary>
  public class DateFilteringWidgetDefinition : 
    CommandWidgetDefinition,
    IDateFilteringWidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    private List<FilterRangeDefinition> predefinedFilteringRanges;
    private string propertyNameToFilter;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DateFilteringWidgetDefinition" /> class.
    /// </summary>
    public DateFilteringWidgetDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DateFilteringWidgetDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DateFilteringWidgetDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) this;

    /// <summary>
    /// Gets or sets the name of the comma predefined filtering rangesnd.
    /// </summary>
    /// <value>The name of the comma predefined filtering rangesnd.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<FilterRangeDefinition> PredefinedFilteringRanges
    {
      get
      {
        if (this.predefinedFilteringRanges == null)
        {
          this.predefinedFilteringRanges = new List<FilterRangeDefinition>();
          if (this.ConfigDefinition != null)
          {
            foreach (DefinitionConfigElement predefinedFilteringRange in ((DateFilteringWidgetDefinitionElement) this.ConfigDefinition).PredefinedFilteringRanges)
              this.predefinedFilteringRanges.Add((FilterRangeDefinition) predefinedFilteringRange.GetDefinition());
          }
        }
        return this.predefinedFilteringRanges;
      }
    }

    IEnumerable<IFilterRangeDefinition> IDateFilteringWidgetDefinition.PredefinedFilteringRanges => this.PredefinedFilteringRanges.Cast<IFilterRangeDefinition>();

    /// <summary>
    /// Gets or sets the name of the property to filter against.
    /// </summary>
    /// <value></value>
    public string PropertyNameToFilter
    {
      get => this.ResolveProperty<string>(nameof (PropertyNameToFilter), this.propertyNameToFilter);
      set => this.propertyNameToFilter = value;
    }
  }
}
