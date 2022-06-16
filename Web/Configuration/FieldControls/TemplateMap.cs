// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.FieldControls.TemplateMap
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration.FieldControls
{
  public class TemplateMap : ConfigElement
  {
    public TemplateMap(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("templatesGroups")]
    [ConfigurationCollection(typeof (TemplatesGroup), AddItemName = "templatesGroup")]
    public ConfigElementList<TemplatesGroup> TemplateGroups
    {
      get => (ConfigElementList<TemplatesGroup>) this["templatesGroups"];
      set => this["templatesGroups"] = (object) value;
    }
  }
}
