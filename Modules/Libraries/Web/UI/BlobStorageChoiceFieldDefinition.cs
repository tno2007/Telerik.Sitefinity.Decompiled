﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  internal class BlobStorageChoiceFieldDefinition : 
    ChoiceFieldDefinition,
    IBlobStorageChoiceFieldDefinition,
    IChoiceFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceFieldDefinition" /> class.
    /// </summary>
    public BlobStorageChoiceFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.BlobStorageChoiceFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public BlobStorageChoiceFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }
  }
}
