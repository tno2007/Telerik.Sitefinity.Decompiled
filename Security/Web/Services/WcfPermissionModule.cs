// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfPermissionModule
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// Wraps a collection of WcfPermissionProvider (representing a provider with multiple permission sets),
  /// for a specific module
  /// </summary>
  [DataContract]
  public class WcfPermissionModule
  {
    /// <summary>Title of the related module</summary>
    [DataMember]
    public string ModuleTitle { get; set; }

    /// <summary>
    /// Gets or sets the name of the manager type associated with this module.
    /// </summary>
    [DataMember]
    public string ManagerTypeName { get; set; }

    /// <summary>List of data providers related to the module</summary>
    [DataMember]
    public WcfPermissionProvider[] Providers { get; set; }
  }
}
