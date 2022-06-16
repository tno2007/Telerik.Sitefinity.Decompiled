// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.StatusFieldDefinition
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
  /// Contains all properties needed to construct an instance of the StatusField control
  /// </summary>
  public class StatusFieldDefinition : 
    CompositeFieldDefinition,
    IStatusFieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private IDateFieldDefinition publicationDateFieldDefinition;
    private IChoiceFieldDefinition statusFieldControlDefinition;
    private IDateFieldDefinition expirationDateFieldDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public StatusFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public StatusFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets or sets the definition for the child DateField control representing an item's publication date
    /// </summary>
    /// <value>The publication date field definition.</value>
    public IDateFieldDefinition PublicationDateFieldDefinition
    {
      get => this.ResolveProperty<IDateFieldDefinition>(nameof (PublicationDateFieldDefinition), this.publicationDateFieldDefinition);
      set => this.publicationDateFieldDefinition = value;
    }

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control representing an item's status
    /// </summary>
    /// <value>The status field control definition.</value>
    public IChoiceFieldDefinition StatusFieldControlDefinition
    {
      get => this.ResolveProperty<IChoiceFieldDefinition>(nameof (StatusFieldControlDefinition), this.statusFieldControlDefinition);
      set => this.statusFieldControlDefinition = value;
    }

    /// <summary>
    /// Gets or sets the definition for the child DateField control representing an item's expiration date
    /// </summary>
    /// <value>The expiration date field definition.</value>
    public IDateFieldDefinition ExpirationDateFieldDefinition
    {
      get => this.ResolveProperty<IDateFieldDefinition>(nameof (ExpirationDateFieldDefinition), this.publicationDateFieldDefinition);
      set => this.expirationDateFieldDefinition = value;
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (StatusField);
  }
}
