// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.DecisionScreenDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>
  /// Type that defines the decision screen user interface element.
  /// </summary>
  public class DecisionScreenDefinition : DefinitionBase, IDecisionScreenDefinition, IDefinition
  {
    private string name;
    private DecisionType decisionType;
    private bool? displayed;
    private string messageText;
    private MessageType messageType;
    private string title;
    private string resourceClassId;
    private List<ICommandWidgetDefinition> actions;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    public DecisionScreenDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ValidatorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public DecisionScreenDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public DecisionScreenDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the decision screen.</summary>
    /// <value></value>
    /// <remarks>
    /// This name has to be unique inside of a collection of decision screens.
    /// </remarks>
    public string Name
    {
      get => this.ResolveProperty<string>(nameof (Name), this.name);
      set => this.name = value;
    }

    /// <summary>
    /// Gets or sets the type of the decision that user is presented with
    /// </summary>
    /// <value></value>
    public DecisionType DecisionType
    {
      get => this.ResolveProperty<DecisionType>(nameof (DecisionType), this.decisionType);
      set => this.decisionType = value;
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
    public bool? Displayed
    {
      get => this.ResolveProperty<bool?>(nameof (Displayed), this.displayed);
      set => this.displayed = value;
    }

    /// <summary>
    /// Gets or sets the text of the message that user is presented with on
    /// the decision screen.
    /// </summary>
    /// <value></value>
    public string MessageText
    {
      get => this.ResolveProperty<string>(nameof (MessageText), this.messageText);
      set => this.messageText = value;
    }

    /// <summary>
    /// Gets or sets the type of the message that user is presented with on
    /// the decision screen.
    /// </summary>
    /// <value></value>
    public MessageType MessageType
    {
      get => this.ResolveProperty<MessageType>(nameof (MessageType), this.messageType);
      set => this.messageType = value;
    }

    /// <summary>Gets or sets the title of the decision screen.</summary>
    /// <value></value>
    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
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
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    /// <summary>
    /// Gets the collection of actions that user can take and hence make the decision.
    /// </summary>
    /// <value></value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<ICommandWidgetDefinition> Actions
    {
      get
      {
        if (this.actions == null)
          this.actions = new List<ICommandWidgetDefinition>();
        return this.ResolveProperty<List<ICommandWidgetDefinition>>(nameof (Actions), this.actions);
      }
    }

    IEnumerable<ICommandWidgetDefinition> IDecisionScreenDefinition.Actions => this.Actions.Cast<ICommandWidgetDefinition>();
  }
}
