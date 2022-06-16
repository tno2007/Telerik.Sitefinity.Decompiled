// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IPageFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of a page field element.
  /// </summary>
  public interface IPageFieldDefinition : IFieldControlDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the ID of the page node from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    Guid RootNodeID { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets the name of the provider from which the page node ought to be selected.
    /// </summary>
    string ProviderName { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically bind the selector on load.
    /// </summary>
    /// <value><c>true</c> if to bind on load; otherwise, <c>false</c>.</value>
    bool? BindOnLoad { get; set; }

    /// <summary>Gets or sets the sort expression.</summary>
    /// <value>The sort expression.</value>
    string SortExpression { get; set; }
  }
}
