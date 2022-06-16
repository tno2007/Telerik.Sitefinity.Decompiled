// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.GenericHierarchicalFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for taxon field.</summary>
  public class GenericHierarchicalFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IGenericHierarchicalFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public GenericHierarchicalFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal GenericHierarchicalFieldDefinitionElement()
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new GenericHierarchicalFieldDefinition((ConfigElement) this);

    /// <summary>Gets or sets the taxonomy pageId.</summary>
    /// <value>The taxonomy pageId.</value>
    [ConfigurationProperty("rootId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TaxonomyIdDescription", Title = "TaxonomyIdCaption")]
    public Guid RootId
    {
      get => (Guid) this["rootId"];
      set => this["rootId"] = (object) value;
    }

    /// <summary>Gets or sets the taxonomy provider.</summary>
    /// <value>The taxonomy provider.</value>
    [ConfigurationProperty("provider")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TaxonomyProviderDescription", Title = "TaxonomyProviderCaption")]
    public string Provider
    {
      get => (string) this["provider"];
      set => this["provider"] = (object) value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    [ConfigurationProperty("webServiceBaseUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceUrlDescription", Title = "WebServiceUrlCaption")]
    public string WebServiceBaseUrl
    {
      get => (string) this["webServiceBaseUrl"];
      set => this["webServiceBaseUrl"] = (object) value;
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
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    [ConfigurationProperty("expandableDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandableControlElementDescription", Title = "ExpandableControlElementCaption")]
    public new ExpandableControlElement ExpandableDefinitionConfig
    {
      get => (ExpandableControlElement) this["expandableDefinition"];
      set => this["expandableDefinition"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition => (IExpandableControlDefinition) this.ExpandableDefinitionConfig;

    /// <summary>
    /// Gets or sets the selected node data field. Used to select the property of the data item that will be displayed when a dataitem is selected.
    /// Default values is Path.
    /// </summary>
    [ConfigurationProperty("selectedNodeDataFieldName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SelectedNodeDataFieldNameDescription", Title = "SelectedNodeDataFieldNameCaption")]
    public string SelectedNodeDataFieldName
    {
      get => (string) this["selectedNodeDataFieldName"];
      set => this["selectedNodeDataFieldName"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (GenericHierarchicalField);
  }
}
