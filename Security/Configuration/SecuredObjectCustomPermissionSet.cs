// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.SecuredObjectCustomPermissionSet
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  public class SecuredObjectCustomPermissionSet : ConfigElement
  {
    public SecuredObjectCustomPermissionSet()
      : base(false)
    {
    }

    public SecuredObjectCustomPermissionSet(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the full name of the type for which the permission set is customized.
    /// </summary>
    [ConfigurationProperty("typeName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string TypeName
    {
      get => (string) this["typeName"];
      set => this["typeName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the a comma-delimited string of the specific IDs of objects for which the permission set is customized.
    /// This is needed, for example, on pages (where the root and the pages are all of the same time: PageNode).
    /// </summary>
    [ConfigurationProperty("securedObjectIds", DefaultValue = "", IsKey = false, IsRequired = false)]
    public string SecuredObjectIds
    {
      get => (string) this["securedObjectIds"];
      set => this["securedObjectIds"] = (object) value;
    }

    /// <summary>
    /// A collection of security actions that can be allowed or denied by a permission.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "CustomSecurityActions")]
    [ConfigurationProperty("customSecurityActions")]
    [ConfigurationCollection(typeof (SecuredObjectCustomPermissionSet), AddItemName = "customSecurityAction")]
    public ConfigElementDictionary<string, CustomSecurityAction> CustomSecurityActions => (ConfigElementDictionary<string, CustomSecurityAction>) this["customSecurityActions"];
  }
}
