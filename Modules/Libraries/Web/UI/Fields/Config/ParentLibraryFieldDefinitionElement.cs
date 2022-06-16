// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Config.ParentLibraryFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Config
{
  public class ParentLibraryFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IParentLibraryFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    public const string WebServiceUrlPropertyName = "webServiceUrl";
    public const string ProviderNamePropertyName = "providerName";
    public const string BindOnLoadPropertyName = "bindOnLoad";
    public const string NoParentLibPropertyName = "noParentLibTitle";
    public const string SelectedParentLibPropertyName = "selectedParentLibTitle";
    public const string LibraryItemNamePropertyName = "libraryItemName";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.Fields.Config.ParentLibraryFieldDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    public ParentLibraryFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ParentLibraryFieldDefinition((ConfigElement) this);

    public override Type DefaultFieldType => typeof (ParentLibraryField);

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    [ConfigurationProperty("webServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceUrlDescription", Title = "WebServiceUrlCaption")]
    public string WebServiceUrl
    {
      get => (string) this["webServiceUrl"];
      set => this["webServiceUrl"] = (object) value;
    }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
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

    /// <summary>Gets or sets the NoParentLib title.</summary>
    /// <value>The web service URL.</value>
    [ConfigurationProperty("noParentLibTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "NoParentLibDescription", Title = "NoParentLibCaption")]
    public string NoParentLibTitle
    {
      get => (string) this["noParentLibTitle"];
      set => this["noParentLibTitle"] = (object) value;
    }

    /// <summary>Gets or sets the SelectedParentLib title.</summary>
    /// <value>The web service URL.</value>
    [ConfigurationProperty("selectedParentLibTitle")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SelectedParentLibDescription", Title = "SelectedParentLibCaption")]
    public string SelectedParentLibTitle
    {
      get => (string) this["selectedParentLibTitle"];
      set => this["selectedParentLibTitle"] = (object) value;
    }

    /// <summary>Gets or sets the LibraryItemName.</summary>
    /// <value>The web service URL.</value>
    [ConfigurationProperty("libraryItemName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibraryItemNameDescription", Title = "LibraryItemNameCaption")]
    public string LibraryItemName
    {
      get => (string) this["libraryItemName"];
      set => this["libraryItemName"] = (object) value;
    }
  }
}
