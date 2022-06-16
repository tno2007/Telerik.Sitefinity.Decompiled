// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.Services.DynamicItemContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.DynamicModules.Web.Services
{
  /// <summary>
  /// Provides context information for a dynamic content item that is exposed in a web service.
  /// </summary>
  [DataContract]
  internal class DynamicItemContext : RelatedDataItemContextBase<DynamicContent>
  {
    private WcfChange versionInfo;

    /// <summary>Wrapped data - the content item itself.</summary>
    [DataMember]
    public override DynamicContent Item
    {
      get => base.Item;
      set
      {
        base.Item = value;
        this.ItemType = value.GetType().FullName;
      }
    }

    /// <summary>
    /// Type of the content item. Set automatically when <see cref="P:Telerik.Sitefinity.DynamicModules.Web.Services.DynamicItemContext.Item" /> is set.
    /// </summary>
    [DataMember]
    public string ItemType { get; set; }

    [DataMember]
    public bool HasLiveVersion { get; set; }

    /// <summary>Versioning information</summary>
    [DataMember]
    public WcfChange VersionInfo
    {
      get => this.versionInfo;
      set => this.versionInfo = value;
    }
  }
}
