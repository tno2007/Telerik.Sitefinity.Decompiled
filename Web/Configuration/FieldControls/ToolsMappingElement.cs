// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.FieldControls.ToolsMappingElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.Configuration.FieldControls
{
  /// <summary>Maps a .net type to a field control(tool)</summary>
  public class ToolsMappingElement : ConfigElement
  {
    private const string mappedTypeProperty = "type";
    private const string toolNameProperty = "toolName";

    public ToolsMappingElement(ConfigElement parent)
      : base(parent)
    {
    }

    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("type", IsKey = true, IsRequired = true)]
    public Type MappedType
    {
      get => (Type) this["type"];
      set => this["type"] = (object) value;
    }

    [ConfigurationProperty("toolName", IsKey = false, IsRequired = true)]
    [StringValidator(MinLength = 1)]
    public string ToolName
    {
      get => (string) this["toolName"];
      set => this["toolName"] = (object) value;
    }
  }
}
