// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ItemSelectorDataMemberElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Selectors;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Implementation of IDataMemberInfo that is suitable for use in configuration
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemSelectorDataMemberElementDescription", Title = "ItemSelectorDataMemberElementTitle")]
  public class ItemSelectorDataMemberElement : ConfigElement, IDataMemberInfo
  {
    /// <summary>Template of the data member</summary>
    /// <value></value>
    [ConfigurationProperty("columnTemplate")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemSelectorDataMemberElementColumnTemplateDescription", Title = "ItemSelectorDataMemberElementColumnTemplateTitle")]
    public string ColumnTemplate
    {
      get => (string) this["columnTemplate"];
      set => this["columnTemplate"] = (object) value;
    }

    /// <summary>Gets or sets the header label for the data member</summary>
    /// <value></value>
    [ConfigurationProperty("headerText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemSelectorDataMemberElementHeaderTextDescription", Title = "ItemSelectorDataMemberElementHeaderTextTitle")]
    public string HeaderText
    {
      get => (string) this["headerText"];
      set => this["headerText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets value indicating whether this data member is a search field
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("isSearchField")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemSelectorDataMemberElementIsSearchFieldDescription", Title = "ItemSelectorDataMemberIsSearchFieldTitle")]
    public bool IsSearchField
    {
      get => (bool) this["isSearchField"];
      set => this["isSearchField"] = (object) value;
    }

    /// <summary>
    /// Gets or sets value indicating whether this data member is a search field of type <see cref="!:Lstring" />
    /// </summary>
    [ConfigurationProperty("isExtendedSearchField")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemSelectorDataMemberElementIsExtendedSearchFieldDescription", Title = "ItemSelectorDataMemberIsExtendedSearchFieldTitle")]
    public bool IsExtendedSearchField
    {
      get => (bool) this["isExtendedSearchField"];
      set => this["isExtendedSearchField"] = (object) value;
    }

    /// <summary>Gets or sets the data member name</summary>
    /// <value></value>
    [ConfigurationProperty("name")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemSelectorDataMemberElementNameDescription", Title = "ItemSelectorDataMemberNameTitle")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Constants for confuguration property names</summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      /// <summary>Config property name for "ColumnName"</summary>
      public const string ColumnTemplate = "columnTemplate";
      /// <summary>Config property name for "HeaderText"</summary>
      public const string HeaderText = "headerText";
      /// <summary>Config property name for "IsSearchField"</summary>
      public const string IsSearchField = "isSearchField";
      /// <summary>Config property name for "IsExtendedSearchField"</summary>
      public const string IsExtendedSearchField = "isExtendedSearchField";
      /// <summary>Config property name for "Name"</summary>
      public const string Name = "name";
    }
  }
}
