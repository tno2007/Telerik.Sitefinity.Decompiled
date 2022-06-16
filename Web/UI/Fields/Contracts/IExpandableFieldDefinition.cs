// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IExpandableFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of expandable field element.
  /// </summary>
  public interface IExpandableFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets the definition for the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    /// <value>The expand control.</value>
    IChoiceFieldDefinition ExpandFieldDefinition { get; }

    /// <summary>
    /// Defines a collection of field definitions that belong to this field.
    /// </summary>
    /// <value>The expandable fields.</value>
    IEnumerable<IFieldDefinition> ExpandableFields { get; }
  }
}
