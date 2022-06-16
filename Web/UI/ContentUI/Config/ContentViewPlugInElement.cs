// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewPlugInElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// Represents a configuration element for Sitefinity content view plugIns.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewPlugInDescription", Title = "ContentViewPlugInTitle")]
  public class ContentViewPlugInElement : 
    DefinitionConfigElement,
    IContentViewPlugInDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ContentViewPlugInElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ContentViewPlugInDefinition((ConfigElement) this);

    /// <summary>Gets or sets the name of the content view plugIn.</summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewPlugInNameDescription", Title = "ContentViewPlugInNameCaption")]
    public string Name
    {
      get => this["name"] as string;
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the ordinal of the content view plugIn.</summary>
    /// <value>The ordinal.</value>
    [ConfigurationProperty("ordinal")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewPlugInOrdinalDescription", Title = "ContentViewPlugInOrdinalCaption")]
    public int? Ordinal
    {
      get => (int?) this["ordinal"];
      set => this["ordinal"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the place holder pageId of the content view plugIn.
    /// </summary>
    /// <value>The place holder pageId.</value>
    [ConfigurationProperty("placeholderid", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewPlugInPlaceHolderIdDescription", Title = "ContentViewPlugInPlaceHolderIdCaption")]
    public string PlaceHolderId
    {
      get => (string) this["placeholderid"];
      set => this["placeholderid"] = (object) value;
    }

    /// <summary>Gets or sets the virtual path.</summary>
    /// <value>The virtual path.</value>
    [ConfigurationProperty("virtualpath")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewPlugInVirtualPathDescription", Title = "ContentViewPlugInVirtualPathCaption")]
    public string VirtualPath
    {
      get => (string) this["virtualpath"];
      set => this["virtualpath"] = (object) value;
    }

    /// <summary>Gets or sets the type of the plug in.</summary>
    /// <value>The type of the plug in.</value>
    [ConfigurationProperty("plugintype")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewPlugInPlugInTypeDescription", Title = "ContentViewPlugInPlugInTypeCaption")]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type PlugInType
    {
      get => this["plugintype"] as Type;
      set => this["plugintype"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ContentViewProps
    {
      public const string Name = "name";
      public const string PlugInType = "plugintype";
      public const string VirtualPath = "virtualpath";
      public const string PlaceHolderId = "placeholderid";
      public const string Ordinal = "ordinal";
    }
  }
}
