// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Definitions.ParentLibraryFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Definitions
{
  /// <summary>
  ///  A class that provides all information needed to construct a parent library field control.
  /// </summary>
  public class ParentLibraryFieldDefinition : 
    FieldControlDefinition,
    IParentLibraryFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string webServiceUrl;
    private string providerName;
    private bool? bindOnLoad;
    private string noParentLibTitle;
    private string selectedParentLibTitle;
    private string libraryItemName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Definitions.ParentLibraryFieldDefinition" /> class.
    /// </summary>
    public ParentLibraryFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Definitions.ParentLibraryFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ParentLibraryFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceUrl), this.webServiceUrl);
      set => this.webServiceUrl = value;
    }

    /// <summary>
    /// Gets or sets the name of the provider from which the page node ought to be selected.
    /// </summary>
    public string ProviderName
    {
      get => this.ResolveProperty<string>(nameof (ProviderName), this.providerName);
      set => this.providerName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically bind the selector on load.
    /// </summary>
    /// <value><c>true</c> if to bind on load; otherwise, <c>false</c>.</value>
    public bool? BindOnLoad
    {
      get => this.ResolveProperty<bool?>(nameof (BindOnLoad), this.bindOnLoad);
      set => this.bindOnLoad = value;
    }

    /// <summary>Gets or sets the NoParentLib title.</summary>
    /// <value><c>true</c> if to bind on load; otherwise, <c>false</c>.</value>
    public string NoParentLibTitle
    {
      get => this.ResolveProperty<string>(nameof (NoParentLibTitle), this.noParentLibTitle);
      set => this.noParentLibTitle = value;
    }

    /// <summary>Gets or sets the SelectedParentLib title.</summary>
    public string SelectedParentLibTitle
    {
      get => this.ResolveProperty<string>(nameof (SelectedParentLibTitle), this.selectedParentLibTitle);
      set => this.selectedParentLibTitle = value;
    }

    /// <summary>Gets or sets the LibraryItemName.</summary>
    public string LibraryItemName
    {
      get => this.ResolveProperty<string>(nameof (LibraryItemName), this.libraryItemName);
      set => this.libraryItemName = value;
    }
  }
}
