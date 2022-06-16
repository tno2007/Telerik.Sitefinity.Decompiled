// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.DecisionScreenElement
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
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Configuration element that defines the decision screen functionality through the
  /// configuration.
  /// </summary>
  public class DecisionScreenElement : 
    DefinitionConfigElement,
    IDecisionScreenDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public DecisionScreenElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new DecisionScreenDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the name of the decision screen. This name has to be unique inside of a collection of decision screens.
    /// </summary>
    [ConfigurationProperty("name", Options = ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NameDescription", Title = "NameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type of the decision that user is presented with
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("decisionType", DefaultValue = DecisionType.NotSet)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DecisionScreenDecisionTypeDescription", Title = "DecisionScreenDecisionTypeCaption")]
    public DecisionType DecisionType
    {
      get => (DecisionType) this["decisionType"];
      set => this["decisionType"] = (object) value;
    }

    /// <summary>
    /// Determines whether the screen should be displayed or not.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// This property works through the css; decision screen will always
    /// be rendered so that it can be displayed through the client side
    /// script.
    /// </remarks>
    [ConfigurationProperty("displayed")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DecisionScreenDisplayedDescription", Title = "DecisionScreenDisplayedCaption")]
    public bool? Displayed
    {
      get => (bool?) this["displayed"];
      set => this["displayed"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the text of the message that user is presented with on
    /// the decision screen.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("messageText")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DecisionScreenMessageTextDescription", Title = "DecisionScreenMessageTextCaption")]
    public string MessageText
    {
      get => (string) this["messageText"];
      set => this["messageText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the type of the message that user is presented with on
    /// the decision screen.
    /// </summary>
    /// <value></value>
    [ConfigurationProperty("messageType", DefaultValue = MessageType.Neutral)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DecisionScreenMessageTypeDescription", Title = "DecisionScreenMessageTypeCaption")]
    public MessageType MessageType
    {
      get => (MessageType) this["messageType"];
      set => this["messageType"] = (object) value;
    }

    /// <summary>Gets or sets the title of the decision screen.</summary>
    /// <value></value>
    [ConfigurationProperty("title")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DecisionScreenTitleDescription", Title = "DecisionScreenTitleCaption")]
    public string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the resource class used to localize message text and title.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If this property is set; MessageText and Title properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    [ConfigurationProperty("resourceClassId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    /// <summary>Gets the actions config.</summary>
    /// <value>The actions config.</value>
    [ConfigurationProperty("actions")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DecisionScreenActionsDescription", Title = "DecisionScreenActionsCaption")]
    public ConfigElementList<CommandWidgetElement> Actions => (ConfigElementList<CommandWidgetElement>) this["actions"];

    /// <summary>
    /// Gets the collection of actions that user can take and hence make the decision.
    /// </summary>
    /// <value></value>
    IEnumerable<ICommandWidgetDefinition> IDecisionScreenDefinition.Actions => this.Actions.Cast<ICommandWidgetDefinition>();

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct DecisionScreenProps
    {
      public const string name = "name";
      public const string decisionType = "decisionType";
      public const string displayed = "displayed";
      public const string messageText = "messageText";
      public const string messageType = "messageType";
      public const string title = "title";
      public const string resourceClassId = "resourceClassId";
      public const string actions = "actions";
    }
  }
}
