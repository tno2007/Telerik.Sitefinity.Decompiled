// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IListPipeSettingsFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of pipe settings list field element.
  /// </summary>
  public interface IListPipeSettingsFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets or sets the default name of the pipe settings to add.
    /// </summary>
    /// <value>The default name of the pipe.</value>
    string DefaultPipeName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether adding new pipes is possible.
    /// </summary>
    /// <value><c>true</c> if adding new pipes is possible; otherwise, <c>false</c>.</value>
    bool DisableAdding { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether removing existing pipes is possible.
    /// </summary>
    /// <value><c>true</c> if removing existing pipes is possible; otherwise, <c>false</c>.</value>
    bool DisableRemoving { get; set; }

    /// <summary>Gets or sets the text of the button for adding pipes.</summary>
    /// <value>The text for adding pipes.</value>
    string AddPipeText { get; set; }

    /// <summary>
    /// Gets or sets the text of the button for changing pipes.
    /// </summary>
    /// <value>The text for changing pipes.</value>
    string ChangePipeText { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether activation of pipes is possible.
    /// </summary>
    /// <value><c>true</c> if activation of pipes is possible; otherwise, <c>false</c>.</value>
    bool DisableActivation { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show default pipes.
    /// </summary>
    /// <value><c>true</c> if default pipes are shown; otherwise, <c>false</c>.</value>
    bool ShowDefaultPipes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the control will suggest outbound or inbound pipes when creating a new pipe setting
    /// </summary>
    /// <value><c>true</c> suggest outbound pipes; otherwise, inbound</value>
    bool WorkWithOutboundPipes { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to show content location (default page url) inside the main list of pipes
    /// This is relevant only for the content pipes
    /// </summary>
    bool ShowContentLocation { get; set; }

    /// <summary>Publishing provider name to use</summary>
    string ProviderName { get; set; }
  }
}
