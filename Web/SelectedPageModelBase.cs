// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SelectedPageModelBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web
{
  /// <summary>This class represents DTO for created pages.</summary>
  public class SelectedPageModelBase
  {
    /// <summary>Gets or sets the identifier of the selected page.</summary>
    /// <value>The identifier.</value>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this page is external.
    /// </summary>
    /// <value>True if the page is external and false otherwise.</value>
    public bool IsExternal { get; set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }

    /// <summary>Gets or sets the titles path.</summary>
    /// <value>The titles path.</value>
    public string TitlesPath { get; set; }

    /// <summary>Gets or sets the URL.</summary>
    /// <value>The URL.</value>
    [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings", Justification = "By design")]
    public string Url { get; set; }

    /// <summary>Gets or sets the type of the node.</summary>
    /// <value>The type of the node.</value>
    public NodeType NodeType { get; set; }
  }
}
