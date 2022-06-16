// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.ParentSelectorFieldElement
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
  /// <summary>
  /// A configuration element that describes a parent selector field.
  /// </summary>
  public class ParentSelectorFieldElement : 
    FieldControlDefinitionElement,
    IParentSelectorFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ParentSelectorFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ParentSelectorFieldElement" /> class.
    /// </summary>
    internal ParentSelectorFieldElement()
    {
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (ParentSelectorField);

    /// <inheritdoc />
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ParentSelectorFieldDefinition((ConfigElement) this);

    /// <inheritdoc />
    [ConfigurationProperty("itemsType")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ParentSelectorItemsTypeDescription", Title = "ParentSelectorItemsTypeTitle")]
    public string ItemsType
    {
      get => (string) this["itemsType"];
      set => this["itemsType"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("webServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ParentSelectorWebServiceUrlDescription", Title = "ParentSelectorWebServiceUrlTitle")]
    public string WebServiceUrl
    {
      get => (string) this["webServiceUrl"];
      set => this["webServiceUrl"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("mainFieldName", DefaultValue = "Title")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ParentSelectorMainFieldDescription", Title = "ParentSelectorMainFieldTitle")]
    public string MainFieldName
    {
      get => (string) this["mainFieldName"];
      set => this["mainFieldName"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("dataKeyNames", DefaultValue = "Id")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ParentSelectorDataKeyNamesDescription", Title = "ParentSelectorDataKeyNamesTitle")]
    public string DataKeyNames
    {
      get => (string) this["dataKeyNames"];
      set => this["dataKeyNames"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("allowSearching", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ParentSelectorAllowSearchingDescription", Title = "ParentSelectorAllowSearchingTitle")]
    public bool AllowSearching
    {
      get => (bool) this["allowSearching"];
      set => this["allowSearching"] = (object) value;
    }
  }
}
