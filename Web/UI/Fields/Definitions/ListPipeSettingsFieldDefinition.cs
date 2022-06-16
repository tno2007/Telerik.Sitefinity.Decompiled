// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.ListPipeSettingsFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a
  /// <see cref="T:Telerik.Sitefinity.Web.UI.Fields.ListPipeSettingsField" />
  /// </summary>
  public class ListPipeSettingsFieldDefinition : 
    FieldControlDefinition,
    IListPipeSettingsFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string providerName;
    private bool disableRemoving;
    private bool disableAdding;
    private string addPipeText;
    private string changePipeText;
    private string defaultPipeName;
    private bool disableActivation;
    private bool showDefaultPipes;
    private bool workWithOutboundPipes;
    private bool showContentLocation;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ListPipeSettingsFieldDefinition" /> class.
    /// </summary>
    public ListPipeSettingsFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ListPipeSettingsFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ListPipeSettingsFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (ListPipeSettingsField);

    public string DefaultPipeName
    {
      get => this.ResolveProperty<string>(nameof (DefaultPipeName), this.defaultPipeName);
      set => this.defaultPipeName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether adding new pipes is possible.
    /// </summary>
    /// <value><c>true</c> if adding new pipes is possible; otherwise, <c>false</c>.</value>
    public bool DisableAdding
    {
      get => this.ResolveProperty<bool>(nameof (DisableAdding), this.disableAdding);
      set => this.disableAdding = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether removing existing pipes is possible.
    /// </summary>
    /// <value><c>true</c> if removing existing pipes is possible; otherwise, <c>false</c>.</value>
    public bool DisableRemoving
    {
      get => this.ResolveProperty<bool>(nameof (DisableRemoving), this.disableRemoving);
      set => this.disableRemoving = value;
    }

    /// <summary>Gets or sets the text of the button for adding pipes.</summary>
    /// <value>The text for adding pipes.</value>
    public string AddPipeText
    {
      get => this.ResolveProperty<string>(nameof (AddPipeText), this.addPipeText);
      set => this.addPipeText = value;
    }

    /// <summary>
    /// Gets or sets the text of the button for changing pipes.
    /// </summary>
    /// <value>The text for changing pipes.</value>
    public string ChangePipeText
    {
      get => this.ResolveProperty<string>(nameof (ChangePipeText), this.changePipeText);
      set => this.changePipeText = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether activation of pipes is possible.
    /// </summary>
    /// <value><c>true</c> if activation of pipes is possible; otherwise, <c>false</c>.</value>
    public bool DisableActivation
    {
      get => this.ResolveProperty<bool>(nameof (DisableActivation), this.disableActivation);
      set => this.disableActivation = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show default pipes.
    /// </summary>
    /// <value><c>true</c> if default pipes are shown; otherwise, <c>false</c>.</value>
    public bool ShowDefaultPipes
    {
      get => this.ResolveProperty<bool>(nameof (ShowDefaultPipes), this.showDefaultPipes);
      set => this.showDefaultPipes = value;
    }

    public bool WorkWithOutboundPipes
    {
      get => this.ResolveProperty<bool>(nameof (WorkWithOutboundPipes), this.workWithOutboundPipes);
      set => this.workWithOutboundPipes = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show content location (default page url) inside the main list of pipes
    /// This is relevant only for the content pipes
    /// </summary>
    public bool ShowContentLocation
    {
      get => this.ResolveProperty<bool>(nameof (ShowContentLocation), this.showContentLocation);
      set => this.showContentLocation = value;
    }

    /// <summary>Gets or sets the publishing provider name to use</summary>
    public string ProviderName
    {
      get => this.ResolveProperty<string>(nameof (ProviderName), this.providerName);
      set => this.providerName = value;
    }
  }
}
