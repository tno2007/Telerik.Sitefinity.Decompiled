// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.ColumnElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>
  /// Configuration element used for configuring the definition of the column backend element.
  /// </summary>
  public abstract class ColumnElement : DefinitionConfigElement, IColumnDefinition, IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ColumnElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the column.</summary>
    /// <value></value>
    /// <remarks>
    /// This name has to be unique inside of a collection of columns.
    /// </remarks>
    [ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Determines the resource class name where the key will be looked for
    /// </summary>
    /// <value>The resouce class name.</value>
    [ConfigurationProperty("resourceClassId")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>
    /// Determines the css class of the header for this data item.
    /// </summary>
    /// <value>The css class of the header.</value>
    [ConfigurationProperty("headerCssClass")]
    public string HeaderCssClass
    {
      get => (string) this["headerCssClass"];
      set => this["headerCssClass"] = (object) value;
    }

    /// <summary>Determines the header text for this data item.</summary>
    /// <value>The header text.</value>
    [ConfigurationProperty("headerText")]
    public string HeaderText
    {
      get => (string) this["headerText"];
      set => this["headerText"] = (object) value;
    }

    /// <summary>Determines the title text for this data item.</summary>
    /// <value>The title text.</value>
    [ConfigurationProperty("titleText")]
    public string TitleText
    {
      get => (string) this["titleText"];
      set => this["titleText"] = (object) value;
    }

    /// <summary>Determines the css class for the item.</summary>
    /// <value>The item css class.</value>
    [ConfigurationProperty("itemCssClass")]
    public string ItemCssClass
    {
      get => (string) this["itemCssClass"];
      set => this["itemCssClass"] = (object) value;
    }

    /// <summary>
    /// Determines the width of the item (when displayed as a column).
    /// </summary>
    /// <value>The width.</value>
    [ConfigurationProperty("width", DefaultValue = 0)]
    public int Width
    {
      get => (int) this["width"];
      set => this["width"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether sorting is disabled for the column.
    /// </summary>
    /// <value>A value indicating whether sorting is disabled.</value>
    [ConfigurationProperty("disableSorting", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DataColumnDisableSortingDescription", Title = "DataColumnDisableSortingCaption")]
    public bool? DisableSorting
    {
      get => (bool?) this["disableSorting"];
      set => this["disableSorting"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the sort expression for the column in ItemsGrid
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("sortExpression", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ColumnElementSortExpressionDescription", Title = "ColumnElementSortExpressionCaption")]
    public string SortExpression
    {
      get => (string) this["sortExpression"];
      set => this["sortExpression"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ColumnProps
    {
      public const string name = "name";
      public const string resourceClassId = "resourceClassId";
      public const string headerCssClass = "headerCssClass";
      public const string headerText = "headerText";
      public const string titleText = "titleText";
      public const string itemCssClass = "itemCssClass";
      public const string width = "width";
      public const string DisableSorting = "disableSorting";
      public const string sortExpression = "sortExpression";
      public const string restrictionLevel = "restrictionLevel";
    }
  }
}
