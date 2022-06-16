// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IDecisionScreenDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Enums;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>
  /// Interface that provides the common members for the definition of decision
  /// screen user interface element.
  /// </summary>
  public interface IDecisionScreenDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the decision screen.</summary>
    /// <remarks>
    /// This name has to be unique inside of a collection of decision screens.
    /// </remarks>
    string Name { get; set; }

    /// <summary>
    /// Gets or sets the type of the decision that user is presented with
    /// </summary>
    DecisionType DecisionType { get; set; }

    /// <summary>
    /// Determines whether the screen should be displayed or not.
    /// </summary>
    /// <remarks>
    /// This property works through the css; decision screen will always
    /// be rendered so that it can be displayed through the client side
    /// script.
    /// </remarks>
    bool? Displayed { get; set; }

    /// <summary>
    /// Gets or sets the text of the message that user is presented with on
    /// the decision screen.
    /// </summary>
    string MessageText { get; set; }

    /// <summary>
    /// Gets or sets the type of the message that user is presented with on
    /// the decision screen.
    /// </summary>
    MessageType MessageType { get; set; }

    /// <summary>Gets or sets the title of the decision screen.</summary>
    string Title { get; set; }

    /// <summary>
    /// Gets or sets the resource class used to localize message text and title.
    /// </summary>
    /// <remarks>
    /// If this property is set; MessageText and Title properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    string ResourceClassId { get; set; }

    /// <summary>
    /// Gets the collection of actions that user can take and hence make the decision.
    /// </summary>
    IEnumerable<ICommandWidgetDefinition> Actions { get; }
  }
}
