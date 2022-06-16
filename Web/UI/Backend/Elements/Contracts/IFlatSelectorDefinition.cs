// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts.IFlatSelectorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts
{
  /// <summary>An extract of FlatSelector's properties</summary>
  public interface IFlatSelectorDefinition : IItemSelectorDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the value determining whether paging will be enabled on the selector
    /// </summary>
    bool AllowPaging { get; set; }

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    bool BindOnLoad { get; set; }

    /// <summary>Text which is shown in the search box by default</summary>
    string InnerSearchBoxText { get; set; }

    /// <summary>
    /// Gets or sets the value determining the page size if paging is enabled
    /// </summary>
    int PageSize { get; set; }

    /// <summary>the text dispayed infront of the search box</summary>
    string SearchBoxTitleText { get; set; }

    /// <summary>
    /// Gets or sets the display state of the providers selection box. The default is not to show.
    /// </summary>
    bool ShowProvidersList { get; set; }

    /// <summary>
    /// Gets or sets the option to include "All Providers" in the providers selection box. The default is to include.
    /// </summary>
    bool InclueAllProvidersOption { get; set; }

    /// <summary>Gets or sets the service URL.</summary>
    /// <value>The service URL.</value>
    string ServiceUrl { get; set; }

    /// <summary>
    /// Gets or set a value indicating whether the header item of the grid will be shown.
    /// </summary>
    /// <value><c>true</c> if the header item of the grid will be shown; otherwise, <c>false</c>.</value>
    bool ShowHeader { get; set; }
  }
}
