// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.ILibrarySelectorFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// An interface that provides the common members for the definition of text field element.
  /// </summary>
  public interface ILibrarySelectorFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets the type of the content.</summary>
    /// <value>The type of the content.</value>
    Type ContentType { get; }

    /// <summary>
    /// Gets whether to show to only system libraries or to show general libaries
    /// </summary>
    bool ShowOnlySystemLibraries { get; }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    IExpandableControlDefinition ExpandableDefinition { get; }

    /// <summary>Gets or sets the sort expression.</summary>
    string SortExpression { get; set; }
  }
}
