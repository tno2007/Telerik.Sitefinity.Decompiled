// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.TaxonFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// 
  /// </summary>
  public class TaxonFieldDefinition : 
    FieldControlDefinition,
    ITaxonFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string webServiceUrl;
    private bool allowMultipleSelection;
    private Guid taxonomyId;
    private string taxonomyProvider;
    private bool bindOnServer;
    private bool allowCreating;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.TaxonFieldDefinition" /> class.
    /// </summary>
    public TaxonFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.TaxonFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public TaxonFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field allows multiple selection.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the field allows multiple selection; otherwise, <c>false</c>.
    /// </value>
    public bool AllowMultipleSelection
    {
      get => this.ResolveProperty<bool>(nameof (AllowMultipleSelection), this.allowMultipleSelection);
      set => this.allowMultipleSelection = value;
    }

    /// <summary>Gets or sets the taxonomy pageId.</summary>
    /// <value>The taxonomy pageId.</value>
    public Guid TaxonomyId
    {
      get => this.ResolveProperty<Guid>(nameof (TaxonomyId), this.taxonomyId);
      set => this.taxonomyId = value;
    }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    public string TaxonomyProvider
    {
      get => this.ResolveProperty<string>(nameof (TaxonomyProvider), this.taxonomyProvider);
      set => this.taxonomyProvider = value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    public string WebServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceUrl), this.webServiceUrl);
      set => this.webServiceUrl = value;
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
    /// Gets or sets a value indicating whether to bind on the server.
    /// </summary>
    /// <value><c>true</c> if to bind on the server; otherwise, <c>false</c>.</value>
    public bool AllowCreating
    {
      get => this.ResolveProperty<bool>(nameof (AllowCreating), this.allowCreating);
      set => this.allowCreating = value;
    }
  }
}
