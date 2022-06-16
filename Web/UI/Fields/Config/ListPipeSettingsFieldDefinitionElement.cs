// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.ListPipeSettingsFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a pipe settings list field.
  /// </summary>
  public class ListPipeSettingsFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IListPipeSettingsFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private const string defaultPipeName = "defaultPipeName";
    public const string DisableAddingPropertyName = "disableAdding";
    public const string DisableRemovingPropertyName = "disableRemoving";
    public const string AddPipeTextPropertyName = "addPipeText";
    public const string ChangePipeTextPropertyName = "changePipeText";
    public const string DisableActivationPropertyName = "disableActivation";
    public const string ShowDefaultPipesPropertyName = "showDefaultPipes";
    public const string WorkWithOutboundPipesPropertyName = "workWithOutboundPipes";
    public const string ShowContentLocationPropertyName = "showContentLocation";
    public const string ProviderNamePropertyName = "provider";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ListPipeSettingsFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new ListPipeSettingsFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets a value indicating whether adding new pipes is possible.
    /// </summary>
    /// <value><c>true</c> if adding new pipes is possible; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("disableAdding", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableAddingDescription", Title = "DisableAddingCaption")]
    public bool DisableAdding
    {
      get => (bool) this["disableAdding"];
      set => this["disableAdding"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether removing existing pipes is possible.
    /// </summary>
    /// <value><c>true</c> if removing existing pipes is possible; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("disableRemoving", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableRemovingDescription", Title = "DisableRemovingCaption")]
    public bool DisableRemoving
    {
      get => (bool) this["disableRemoving"];
      set => this["disableRemoving"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the default name of the pipe settings to add.
    /// </summary>
    /// <value>The default name of the pipe.</value>
    [ConfigurationProperty("defaultPipeName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DefaultPipeNameDescription", Title = "DefaultPipeNameCaption")]
    public string DefaultPipeName
    {
      get => (string) this["defaultPipeName"];
      set => this["defaultPipeName"] = (object) value;
    }

    /// <summary>Gets or sets the text of the button for adding pipes.</summary>
    /// <value>The text for adding pipes.</value>
    [ConfigurationProperty("addPipeText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AddPipeTextDescription", Title = "AddPipeTextCaption")]
    public string AddPipeText
    {
      get => (string) this["addPipeText"];
      set => this["addPipeText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the text of the button for changing pipes.
    /// </summary>
    /// <value>The text for changing pipes.</value>
    [ConfigurationProperty("changePipeText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ChangePipeTextDescription", Title = "ChangePipeTextCaption")]
    public string ChangePipeText
    {
      get => (string) this["changePipeText"];
      set => this["changePipeText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether activation of pipes is possible.
    /// </summary>
    /// <value><c>true</c> if activation of pipes is possible; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("disableActivation", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableActivationDescription", Title = "DisableActivationCaption")]
    public bool DisableActivation
    {
      get => (bool) this["disableActivation"];
      set => this["disableActivation"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show default pipes.
    /// </summary>
    /// <value><c>true</c> if default pipes are shown; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("showDefaultPipes", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowDefaultPipesDescription", Title = "ShowDefaultPipesCaption")]
    public bool ShowDefaultPipes
    {
      get => (bool) this["showDefaultPipes"];
      set => this["showDefaultPipes"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to work with outbound pipes.
    /// </summary>
    [ConfigurationProperty("workWithOutboundPipes", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WorkWithOutboundPipesDescription", Title = "WorkWithOutboundPipesCaption")]
    public bool WorkWithOutboundPipes
    {
      get => (bool) this["workWithOutboundPipes"];
      set => this["workWithOutboundPipes"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show content location (default page url) inside the main list of pipes
    /// This is relevant only for the content pipes
    /// </summary>
    [ConfigurationProperty("showContentLocation", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowContentLocationDescription", Title = "ShowContentLocationCaption")]
    public bool ShowContentLocation
    {
      get => (bool) this["showContentLocation"];
      set => this["showContentLocation"] = (object) value;
    }

    /// <summary>Publishing provider name to use</summary>
    [ConfigurationProperty("provider", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PublishingProviderNameDescription", Title = "PublishingProviderNameCaption")]
    public string ProviderName
    {
      get => (string) this["provider"];
      set => this["provider"] = (object) value;
    }
  }
}
