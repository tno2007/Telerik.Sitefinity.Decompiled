// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.TaxonFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for taxon field.</summary>
  public class TaxonFieldDefinitionElement : 
    FieldControlDefinitionElement,
    ITaxonFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public TaxonFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal TaxonFieldDefinitionElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new TaxonFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets a value indicating whether the field allows multiple selection.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the field allows multiple selection; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("allowMultipleSelection", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowMultipleSelectionDescription", Title = "AllowMultipleSelectionCaption")]
    public bool AllowMultipleSelection
    {
      get => (bool) this["allowMultipleSelection"];
      set => this["allowMultipleSelection"] = (object) value;
    }

    /// <summary>Gets or sets the taxonomy pageId.</summary>
    /// <value>The taxonomy pageId.</value>
    [ConfigurationProperty("taxonomyId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TaxonomyIdDescription", Title = "TaxonomyIdCaption")]
    public Guid TaxonomyId
    {
      get => (Guid) this["taxonomyId"];
      set => this["taxonomyId"] = (object) value;
    }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    [ConfigurationProperty("taxonomyProvider")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TaxonomyProviderDescription", Title = "TaxonomyProviderCaption")]
    public string TaxonomyProvider
    {
      get => (string) this["taxonomyProvider"];
      set => this["taxonomyProvider"] = (object) value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    [ConfigurationProperty("webServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceUrlDescription", Title = "WebServiceUrlCaption")]
    public string WebServiceUrl
    {
      get => (string) this["webServiceUrl"];
      set => this["webServiceUrl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to bind the taxon field on the server.
    /// </summary>
    /// <value><c>true</c> if to bind on the server; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("bindOnServer", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BindOnServerDescription", Title = "BindOnServerCaption")]
    public bool BindOnServer
    {
      get => (bool) this["bindOnServer"];
      set => this["bindOnServer"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating wheter classification items can be created when selecting
    /// </summary>
    [ConfigurationProperty("allowCreating", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowCreatingDescription", Title = "AllowCreatingCaption")]
    public bool AllowCreating
    {
      get => (bool) this["allowCreating"];
      set => this["allowCreating"] = (object) value;
    }
  }
}
