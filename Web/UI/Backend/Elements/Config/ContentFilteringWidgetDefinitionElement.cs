// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ContentFilteringWidgetDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for command widget definition
  /// </summary>
  internal class ContentFilteringWidgetDefinitionElement : 
    CommandWidgetElement,
    IContentFilteringWidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.CommandWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public ContentFilteringWidgetDefinitionElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ContentFilteringWidgetDefinition((ConfigElement) this);

    /// <summary>Gets or sets the predefined filtering ranges.</summary>
    /// <value>The predefined filtering ranges.</value>
    [ConfigurationProperty("predefinedFilteringRanges")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PredefinedFilteringRangesDescription", Title = "PredefinedFilteringRangesCaption")]
    public virtual ConfigElementList<FilterRangeElement> PredefinedFilteringRanges => (ConfigElementList<FilterRangeElement>) this["predefinedFilteringRanges"];

    /// <summary>Gets or sets the predefined filtering ranges.</summary>
    /// <value></value>
    IEnumerable<IFilterRangeDefinition> IContentFilteringWidgetDefinition.PredefinedFilteringRanges => this.PredefinedFilteringRanges.Cast<IFilterRangeDefinition>();

    /// <summary>
    /// Gets or sets the name of the property to filter against.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("propertyNameToFilter", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PropertyNameToFilterDescription", Title = "PropertyNameToFilterCaption")]
    public string PropertyNameToFilter
    {
      get => (string) this["propertyNameToFilter"];
      set => this["propertyNameToFilter"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal new struct CommandWidgetProps
    {
      public const string predefinedFilteringRanges = "predefinedFilteringRanges";
      public const string propertyNameToFilter = "propertyNameToFilter";
    }
  }
}
