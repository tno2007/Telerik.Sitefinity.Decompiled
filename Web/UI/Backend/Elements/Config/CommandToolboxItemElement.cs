// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.CommandToolboxItemElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Represents a configuration element for Sitefinity CommandToolboxItem element.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandToolboxItemDescription", Title = "CommandToolboxItemTitle")]
  [DebuggerDisplay("CommandToolboxItemElement")]
  public class CommandToolboxItemElement : 
    ToolboxItemBaseElement,
    ICommandToolboxItemDefinition,
    IToolboxItemBaseDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.CommandToolboxItemElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CommandToolboxItemElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new CommandToolboxItemDefinition((ConfigElement) this);

    [ConfigurationProperty("CausesValidation", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandToolboxItemCausesValidationDescription", Title = "CommandToolboxItemCausesValidationCaption")]
    public bool CausesValidation
    {
      get => (bool) this[nameof (CausesValidation)];
      set => this[nameof (CausesValidation)] = (object) value;
    }

    [ConfigurationProperty("CommandName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandToolboxItemCommandNameDescription", Title = "CommandToolboxItemCommandNameCaption")]
    public string CommandName
    {
      get => this[nameof (CommandName)] as string;
      set => this[nameof (CommandName)] = (object) value;
    }

    [ConfigurationProperty("CommandType", DefaultValue = CommandType.NormalButton)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandToolboxItemCommandTypeDescription", Title = "CommandToolboxItemCommandTypeCaption")]
    public CommandType CommandType
    {
      get => (CommandType) this[nameof (CommandType)];
      set => this[nameof (CommandType)] = (object) value;
    }

    [ConfigurationProperty("Text")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommandToolboxItemTextDescription", Title = "CommandToolboxItemTextCaption")]
    public string Text
    {
      get => this[nameof (Text)] as string;
      set => this[nameof (Text)] = (object) value;
    }

    [ConfigurationProperty("resourceClassId", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }
  }
}
