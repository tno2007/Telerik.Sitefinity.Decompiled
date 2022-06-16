// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.SearchWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for the search widget definition
  /// </summary>
  public class SearchWidgetElement : 
    CommandWidgetElement,
    ISearchWidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.CommandWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public SearchWidgetElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new SearchWidgetDefinition((ConfigElement) this);

    /// <summary>Gets or sets the persistent type to search.</summary>
    /// <value>The persistent type to search.</value>
    [ConfigurationProperty("persistentTypeToSearch", DefaultValue = typeof (Content))]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PersistentTypeToSearchDescription", Title = "PersistentTypeToSearchCaption")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type PersistentTypeToSearch
    {
      get => (Type) this["persistentTypeToSearch"];
      set => this["persistentTypeToSearch"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("mode", DefaultValue = BackendSearchMode.NotSet)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SearchModeDescription", Title = "SearchModeCaption")]
    public BackendSearchMode Mode
    {
      get => (BackendSearchMode) this["mode"];
      set => this["mode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the name of the command that widget fires when closing the search.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("closeSearchCommandName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CloseSearchCommandNameDescription", Title = "CloseSearchCommandNameCaption")]
    public string CloseSearchCommandName
    {
      get => (string) this["closeSearchCommandName"];
      set => this["closeSearchCommandName"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct SearchWidgetProps
    {
      public const string persistentTypeToSearch = "persistentTypeToSearch";
      public const string mode = "mode";
      public const string closeSearchCommandName = "closeSearchCommandName";
    }
  }
}
