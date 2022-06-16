// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.FieldControls.TemplateSettingsElement
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
  public class TemplateSettingsElement : ConfigElement
  {
    private const string KeyProperty = "key";
    private const string NameProperty = "name";
    private const string DescriptionProperty = "description";
    private const string IconProperty = "icon";
    private const string IconPathProperty = "iconPath";
    private const string TemplatePathProperty = "templatePath";
    private const string TemplateResourceNameProperty = "templateResourceName";
    private const string AssemblyInfoTypeProperty = "assemblyInfoType";

    public TemplateSettingsElement(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("key", IsKey = true, IsRequired = true)]
    public string Key
    {
      get => (string) this["key"];
      set => this["key"] = (object) value;
    }

    [ConfigurationProperty("name", IsRequired = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    [ConfigurationProperty("description", IsRequired = false)]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    [ConfigurationProperty("icon", IsRequired = false)]
    public string Icon
    {
      get => (string) this["icon"];
      set => this["icon"] = (object) value;
    }

    [ConfigurationProperty("iconPath", IsRequired = false)]
    public string IconPath
    {
      get => (string) this["iconPath"];
      set => this["iconPath"] = (object) value;
    }

    [ConfigurationProperty("templatePath", IsRequired = false)]
    public string TemplatePath
    {
      get => (string) this["templatePath"];
      set => this["templatePath"] = (object) value;
    }

    [ConfigurationProperty("templateResourceName", IsRequired = false)]
    public string TemplateResourceName
    {
      get => (string) this["templateResourceName"];
      set => this["templateResourceName"] = (object) value;
    }

    [ConfigurationProperty("assemblyInfoType", IsRequired = false)]
    [TypeConverter(typeof (StringTypeConverter))]
    public Type AssemblyInfoType
    {
      get => (Type) this["assemblyInfoType"];
      set => this["assemblyInfoType"] = (object) value;
    }
  }
}
