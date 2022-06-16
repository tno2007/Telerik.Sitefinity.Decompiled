// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Web.Services.SettingsItemContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.SiteSettings.Web.Services
{
  [DataContract]
  public class SettingsItemContext : ItemContext<ISettingsDataContract>
  {
    public SettingsItemContext(ISettingsDataContract item, bool inherit)
    {
      this.Item = item;
      this.Inherit = inherit;
    }

    [DataMember]
    public bool Inherit { get; set; }
  }
}
