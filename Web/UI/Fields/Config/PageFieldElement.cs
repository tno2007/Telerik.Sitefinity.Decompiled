// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.PageFieldElement
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
  /// <summary>A configuration element that describes a page field.</summary>
  public class PageFieldElement : 
    FieldControlDefinitionElement,
    IPageFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    public const string RootNodeIDPropertyName = "rootNodeID";
    public const string WebServiceUrlPropertyName = "webServiceUrl";
    public const string ProviderNamePropertyName = "providerName";
    public const string BindOnLoadPropertyName = "bindOnLoad";
    public const string SortExpressionPropertyName = "sortExpression";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public PageFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new PageFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the ID of the page node from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    [ConfigurationProperty("rootNodeID", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RootNodeIDDescription", Title = "RootNodeIDCaption")]
    public Guid RootNodeID
    {
      get => (Guid) this["rootNodeID"];
      set => this["rootNodeID"] = (object) value;
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
    /// Gets or sets the name of the provider from which the page node ought to be selected.
    /// </summary>
    [ConfigurationProperty("providerName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProviderNameDescription", Title = "ProviderNameCaption")]
    public string ProviderName
    {
      get => (string) this["providerName"];
      set => this["providerName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically bind the selector on load.
    /// </summary>
    /// <value><c>true</c> if to bind on load; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("bindOnLoad")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "BindOnLoadDescription", Title = "BindOnLoadCaption")]
    public bool? BindOnLoad
    {
      get => (bool?) this["bindOnLoad"];
      set => this["bindOnLoad"] = (object) value;
    }

    /// <summary>Gets or sets the sort expression.</summary>
    /// <value>The sort expression.</value>
    [ConfigurationProperty("sortExpression")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SortExpressionDescription", Title = "SortExpressionCaption")]
    public string SortExpression
    {
      get => (string) this["sortExpression"];
      set => this["sortExpression"] = (object) value;
    }
  }
}
