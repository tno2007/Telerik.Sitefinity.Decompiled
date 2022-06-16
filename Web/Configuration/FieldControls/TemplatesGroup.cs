// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.FieldControls.TemplatesGroup
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration.FieldControls
{
  public class TemplatesGroup : ConfigElement
  {
    private const string TypeProperty = "type";
    private const string ToolNameProperty = "toolName";
    private const string TemplatesProperty = "templates";
    private const string DefaultTemplateProperty = "defaultTemplate";

    public TemplatesGroup(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("type", IsRequired = true)]
    public string Type
    {
      get => (string) this["type"];
      set => this["type"] = (object) value;
    }

    [ConfigurationProperty("toolName")]
    public string ToolName
    {
      get => (string) this["toolName"];
      set => this["toolName"] = (object) value;
    }

    [ConfigurationProperty("defaultTemplate")]
    public string DefaultTemplate
    {
      get => (string) this["defaultTemplate"];
      set => this["defaultTemplate"] = (object) value;
    }

    [ConfigurationProperty("templates", IsDefaultCollection = true, IsRequired = false)]
    [ConfigurationCollection(typeof (TemplateSettingsElement), AddItemName = "template")]
    public ConfigElementDictionary<string, TemplateSettingsElement> Templates
    {
      get => (ConfigElementDictionary<string, TemplateSettingsElement>) this["templates"];
      set => this["templates"] = (object) value;
    }
  }
}
