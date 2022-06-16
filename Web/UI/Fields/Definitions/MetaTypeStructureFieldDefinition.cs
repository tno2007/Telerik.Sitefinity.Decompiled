﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.MetaTypeStructureFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  public class MetaTypeStructureFieldDefinition : 
    FieldControlDefinition,
    IMetaTypeStructureFieldDefinition
  {
    private string editButtonTextResource;

    public MetaTypeStructureFieldDefinition()
    {
    }

    public MetaTypeStructureFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the edit button text resource.</summary>
    /// <value></value>
    public string EditButtonTextResource
    {
      get => this.ResolveProperty<string>(nameof (EditButtonTextResource), this.editButtonTextResource);
      set => this.editButtonTextResource = value;
    }
  }
}
