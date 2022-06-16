// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IGenericHierarchicalFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that provides the common members for the definition of taxonomy field element.
  /// </summary>
  public interface IGenericHierarchicalFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the taxonomy pageId.</summary>
    /// <value>The taxonomy pageId.</value>
    Guid RootId { get; set; }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    string Provider { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    string WebServiceBaseUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to bind on the server.
    /// </summary>
    /// <value><c>true</c> if to bind on the server; otherwise, <c>false</c>.</value>
    bool BindOnServer { get; set; }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    IExpandableControlDefinition ExpandableDefinition { get; }

    /// <summary>
    /// Gets or sets the selected node data field. Used to select the property of the data item that will be displayed when a dataitem is selected.
    /// Default values is Path.
    /// </summary>
    string SelectedNodeDataFieldName { get; set; }
  }
}
