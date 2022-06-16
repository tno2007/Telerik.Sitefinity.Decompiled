// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Contracts.IParentLibraryFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Contracts
{
  public interface IParentLibraryFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
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

    /// <summary>Gets or sets the title of the NoParentLib choice.</summary>
    string NoParentLibTitle { get; set; }

    /// <summary>
    /// Gets or sets the title of the SelectedParentLib choice.
    /// </summary>
    string SelectedParentLibTitle { get; set; }

    /// <summary>Gets or sets the library item name.</summary>
    string LibraryItemName { get; set; }
  }
}
