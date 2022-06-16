// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Configuration.FieldControls.ToolConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.Configuration.FieldControls
{
  public class ToolConfigElement : ConfigElement
  {
    private static AssemblyQualifiedTypeNameConverter typeConverter = new AssemblyQualifiedTypeNameConverter();
    private const string nameProperty = "name";
    private const string controlTypeProperty = "control";
    private const string designerTypeProperty = "designer";
    private const string displayNameProperty = "displayName";
    private const string descriptionProperty = "description";
    private const string localizationTypeProperty = "localizationType";
    private const string sectionProperty = "section";
    private const string userControlProperty = "userControl";

    public ToolConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("name", DefaultValue = "Name", IsKey = true, IsRequired = true)]
    [StringValidator(MinLength = 1)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    [ConfigurationProperty("displayName", IsRequired = false)]
    public string DisplayName
    {
      get => (string) this["displayName"];
      set => this["displayName"] = (object) value;
    }

    [ConfigurationProperty("description", IsRequired = false)]
    public string Description
    {
      get => (string) this["description"];
      set => this["description"] = (object) value;
    }

    [ConfigurationProperty("control", IsRequired = false)]
    public string ControlTypeName
    {
      get => (string) this["control"];
      set => this["control"] = (object) value;
    }

    [ConfigurationProperty("designer", IsRequired = false)]
    public string DesignerTypeName
    {
      get => (string) this["designer"];
      set => this["designer"] = (object) value;
    }

    [ConfigurationProperty("localizationType", IsRequired = false)]
    public string LocalizationTypeName
    {
      get => (string) this["localizationType"];
      set => this["localizationType"] = (object) value;
    }

    [ConfigurationProperty("section", IsRequired = true)]
    public string Section
    {
      get => (string) this["section"];
      set => this["section"] = (object) value;
    }

    [ConfigurationProperty("userControl", IsRequired = false)]
    public string UserControl
    {
      get => (string) this["userControl"];
      set => this["userControl"] = (object) value;
    }

    public Type LocalizationType
    {
      get => string.IsNullOrEmpty(this.LocalizationTypeName) ? (Type) null : (Type) ToolConfigElement.typeConverter.ConvertFrom((object) this.LocalizationTypeName);
      set => this.LocalizationTypeName = ToolConfigElement.typeConverter.ConvertToString((object) value);
    }
  }
}
