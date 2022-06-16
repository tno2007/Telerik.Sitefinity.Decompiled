// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ProvidersListWidgetElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
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
  /// A configuration element that represents a Providers list widget definition.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ProvidersListWidgetDescription", Title = "ProvidersListWidgetCaption")]
  public class ProvidersListWidgetElement : 
    CommandWidgetElement,
    IProvidersListWidgetDefinition,
    ICommandWidgetDefinition,
    IWidgetDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ProvidersListWidgetElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public ProvidersListWidgetElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ProvidersListWidgetDefinition((ConfigElement) this);

    /// <summary>Gets or sets the type of the data item.</summary>
    /// <value>The type of the data item.</value>
    [ConfigurationProperty("dataItemType", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DataItemTypeDescription", Title = "DataItemTypeCaption")]
    public string DataItemType
    {
      get => (string) this["dataItemType"];
      set => this["dataItemType"] = (object) value;
    }

    /// <summary>Gets or sets the type of the manager.</summary>
    /// <value>The type of the manager.</value>
    [ConfigurationProperty("managerType", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ManagerTypeDescription", Title = "ManagerTypeCaption")]
    public string ManagerType
    {
      get => (string) this["managerType"];
      set => this["managerType"] = (object) value;
    }

    /// <summary>Gets or sets the select provider message.</summary>
    /// <value>The select provider message.</value>
    [ConfigurationProperty("selectProviderMessage", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SelectProviderMessageDescription", Title = "SelectProviderMessageCaption")]
    public string SelectProviderMessage
    {
      get => (string) this["selectProviderMessage"];
      set => this["selectProviderMessage"] = (object) value;
    }

    /// <summary>Gets or sets the select provider message CSS class.</summary>
    /// <value>The select provider message CSS class.</value>
    [ConfigurationProperty("selectProviderMessageCssClass", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SelectProviderMessageCssClassDescription", Title = "SelectProviderMessageCssClassCaption")]
    public string SelectProviderMessageCssClass
    {
      get => (string) this["selectProviderMessageCssClass"];
      set => this["selectProviderMessageCssClass"] = (object) value;
    }

    /// <summary>String constants for keys of configuration properties</summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ProviderListWidgetProps
    {
      public const string DataItemType = "dataItemType";
      public const string ManagerType = "managerType";
      public const string SelectProviderMessage = "selectProviderMessage";
      public const string SelectProviderMessageCssClass = "selectProviderMessageCssClass";
    }
  }
}
