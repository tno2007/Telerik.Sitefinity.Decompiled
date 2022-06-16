// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.PageFieldDefinition
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
  ///  A class that provides all information needed to construct a page field control.
  /// </summary>
  public class PageFieldDefinition : 
    FieldControlDefinition,
    IPageFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private Guid rootNodeID;
    private string webServiceUrl;
    private string providerName;
    private bool? bindOnLoad;
    private string sortExpression;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.PageFieldDefinition" /> class.
    /// </summary>
    public PageFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.PageFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public PageFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (PageField);

    /// <summary>
    /// Gets or sets the ID of the page node from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootNodeID
    {
      get => this.ResolveProperty<Guid>(nameof (RootNodeID), this.rootNodeID);
      set => this.rootNodeID = value;
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

    /// <summary>Gets or sets the sort expression.</summary>
    /// <value>The sort expression.</value>
    public string SortExpression
    {
      get => this.ResolveProperty<string>(nameof (SortExpression), this.sortExpression);
      set => this.sortExpression = value;
    }
  }
}
