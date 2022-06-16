// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.MetaTypeStructureFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  public class MetaTypeStructureFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IMetaTypeStructureFieldDefinitionElement
  {
    public MetaTypeStructureFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.FieldControlDefinitionElement" /> class.
    /// </summary>
    internal MetaTypeStructureFieldDefinitionElement()
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new MetaTypeStructureFieldDefinition((ConfigElement) this);

    [ConfigurationProperty("title")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TitleDescription", Title = "TitleCaption")]
    public new string Title
    {
      get => (string) this["title"];
      set => this["title"] = (object) value;
    }

    [ConfigurationProperty("resourceClassId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public new string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    public override Type DefaultFieldType => typeof (MetaTypeStructureField);

    /// <summary>Gets or sets the edit button text resource.</summary>
    /// <value></value>
    [ConfigurationProperty("editButtonTextResource", DefaultValue = "EditStructure")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EditButtonTextResourceDescription", Title = "EditButtonTextResourceCaption")]
    public string EditButtonTextResource
    {
      get => (string) this["editButtonTextResource"];
      set => this["editButtonTextResource"] = (object) value;
    }
  }
}
