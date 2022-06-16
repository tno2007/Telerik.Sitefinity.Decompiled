// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.ItemManagerMappingElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.Configuration
{
  public class ItemManagerMappingElement : ConfigElement
  {
    public ItemManagerMappingElement(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("itemType", DefaultValue = null, IsKey = true, IsRequired = true)]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type ItemType
    {
      get => (Type) this["itemType"];
      set => this["itemType"] = (object) value;
    }

    [ConfigurationProperty("managerType", DefaultValue = null, IsKey = false, IsRequired = true)]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type ManagerType
    {
      get => (Type) this["managerType"];
      set => this["managerType"] = (object) value;
    }
  }
}
