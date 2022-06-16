// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.HierarchicalTaxonParentSelectorFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Definitions
{
  /// <summary>
  /// Stores information used by <see cref="!:Telerik.Sitefinity.Web.UI.HierarchicalTaxonParentSelectorField" />
  /// </summary>
  public class HierarchicalTaxonParentSelectorFieldDefinition : 
    FieldControlDefinition,
    IHierarchicalTaxonParentSelectorFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string webServiceBaseUrl;
    private Guid rootId;
    private string provider;
    private bool bindOnServer;
    private IExpandableControlDefinition expandableDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public HierarchicalTaxonParentSelectorFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public HierarchicalTaxonParentSelectorFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the taxonomy id.</summary>
    /// <value>The taxonomy id.</value>
    public Guid TaxonomyId
    {
      get => this.ResolveProperty<Guid>("RootId", this.rootId);
      set => this.rootId = value;
    }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    public string Provider
    {
      get => this.ResolveProperty<string>(nameof (Provider), this.provider);
      set => this.provider = value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceBaseUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceBaseUrl), this.webServiceBaseUrl);
      set => this.webServiceBaseUrl = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to bind on the server.
    /// </summary>
    /// <value><c>true</c> if to bind on the server; otherwise, <c>false</c>.</value>
    public bool BindOnServer
    {
      get => this.ResolveProperty<bool>(nameof (BindOnServer), this.bindOnServer);
      set => this.bindOnServer = value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition
    {
      get
      {
        if (this.expandableDefinition == null)
        {
          this.expandableDefinition = (IExpandableControlDefinition) new ExpandableControlDefinition();
          this.expandableDefinition.ControlDefinitionName = this.ControlDefinitionName;
          this.expandableDefinition.ViewName = this.ViewName;
          this.expandableDefinition.SectionName = this.SectionName;
          this.expandableDefinition.FieldName = this.FieldName;
        }
        return this.expandableDefinition;
      }
      set => this.expandableDefinition = value;
    }
  }
}
