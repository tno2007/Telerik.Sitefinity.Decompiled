// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.FlatSelectorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions
{
  /// <summary>
  /// An implemnetation of IFlatSelectorDefinition that is meant to be used in a template (declaratively)
  /// </summary>
  public class FlatSelectorDefinition : 
    ItemSelectorDefinitionBase,
    IFlatSelectorDefinition,
    IItemSelectorDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.FlatSelectorDefinition" /> class.
    /// </summary>
    public FlatSelectorDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions.FlatSelectorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public FlatSelectorDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public FlatSelectorDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the value determining whether paging will be enabled on the selector
    /// </summary>
    /// <value></value>
    public bool AllowPaging
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the option to automatically bind the selector when the control loads
    /// </summary>
    /// <value></value>
    public bool BindOnLoad
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>Text which is shown in the search box by default</summary>
    /// <value></value>
    public string InnerSearchBoxText
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the value determining the page size if paging is enabled
    /// </summary>
    /// <value></value>
    public int PageSize
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>the text dispayed infront of the search box</summary>
    /// <value></value>
    public string SearchBoxTitleText
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the display state of the providers selection box. The default is not to show.
    /// </summary>
    /// <value></value>
    public bool ShowProvidersList
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or sets the option to include "All Providers" in the providers selection box. The default is to include.
    /// </summary>
    /// <value></value>
    public bool InclueAllProvidersOption
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>Gets or sets the service URL.</summary>
    /// <value>The service URL.</value>
    public string ServiceUrl
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }

    /// <summary>
    /// Gets or set a value indicating whether the header item of the grid will be shown.
    /// </summary>
    /// <value><c>true</c> if the header item of the grid will be shown; otherwise, <c>false</c>.</value>
    public bool ShowHeader
    {
      get => throw new NotImplementedException();
      set => throw new NotImplementedException();
    }
  }
}
