// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.FlatSelectorElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Contracts;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  /// <summary>
  /// Implementation of IFlatSelectorDefinition that can be used in configuration
  /// </summary>
  public class FlatSelectorElement : 
    ItemSelectorElementBase,
    IFlatSelectorDefinition,
    IItemSelectorDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.FlatSelectorElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public FlatSelectorElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new FlatSelectorDefinition((ConfigElement) this);

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

    /// <summary>Constants for configration property names</summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal new struct PropertyNames
    {
      /// <summary>Config property name for "AllowPaging"</summary>
      public const string AllowPaging = "allowPaging";
      /// <summary>Config property name for "BindOnLoad"</summary>
      public const string BindOnLoad = "bindOnLoad";
      /// <summary>Config property name for "InnerSearchBoxText"</summary>
      public const string InnerSearchBoxText = "innerSearchBoxText";
      /// <summary>Config property name for "PageSize"</summary>
      public const string PageSize = "pageSize";
      /// <summary>Config property name for "SearchBoxTitleText"</summary>
      public const string SearchBoxTitleText = "searchBoxTitleText";
      /// <summary>Config property name for "ShowProvidersList"</summary>
      public const string ShowProvidersList = "showProvidersList";
      /// <summary>Config property name for "InclueAllProvidersOption"</summary>
      public const string InclueAllProvidersOption = "inclueAllProvidersOption";
    }
  }
}
