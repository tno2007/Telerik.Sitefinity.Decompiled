// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.ItemToManagerMappingConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  public class ItemToManagerMappingConfig : ConfigSection
  {
    [ConfigurationProperty("map")]
    [ConfigurationCollection(typeof (AssemblyGroupElement), AddItemName = "itemManager")]
    public ConfigElementDictionary<Type, ItemManagerMappingElement> Map => this["map"] as ConfigElementDictionary<Type, ItemManagerMappingElement>;
  }
}
