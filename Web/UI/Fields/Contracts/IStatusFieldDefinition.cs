// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IStatusFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// Defines a contract for StatusField control definition and config element
  /// </summary>
  public interface IStatusFieldDefinition : ICompositeFieldDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the definition for the child DateField control representing an item's publication date
    /// </summary>
    /// <value>The publication date field definition.</value>
    IDateFieldDefinition PublicationDateFieldDefinition { get; }

    /// <summary>
    /// Gets or sets the definition for the child ChoiceField control representing an item's status
    /// </summary>
    /// <value>The status field control definition.</value>
    IChoiceFieldDefinition StatusFieldControlDefinition { get; }

    /// <summary>
    /// Gets or sets the definition for the child DateField control representing an item's expiration date
    /// </summary>
    /// <value>The expiration date field definition.</value>
    IDateFieldDefinition ExpirationDateFieldDefinition { get; }
  }
}
