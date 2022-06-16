// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.ITaxonFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of taxonomy field element.
  /// </summary>
  public interface ITaxonFieldDefinition : IFieldControlDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets a value indicating whether the field allows multiple selection.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the field allows multiple selection; otherwise, <c>false</c>.
    /// </value>
    bool AllowMultipleSelection { get; set; }

    /// <summary>Gets or sets the taxonomy pageId.</summary>
    /// <value>The taxonomy pageId.</value>
    Guid TaxonomyId { get; set; }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    string TaxonomyProvider { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    string WebServiceUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind on the server.
    /// </summary>
    /// <value><c>true</c> if to bind on the server; otherwise, <c>false</c>.</value>
    bool BindOnServer { get; set; }

    /// <summary>
    /// Gets or sets a value indicating wheter classification items can be created when selecting
    /// </summary>
    bool AllowCreating { get; set; }
  }
}
