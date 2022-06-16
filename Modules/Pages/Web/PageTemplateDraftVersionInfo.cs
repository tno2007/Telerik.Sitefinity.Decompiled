// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.PageTemplateDraftVersionInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Versioning.Web.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web
{
  [DataContract]
  [KnownType(typeof (WcfChange))]
  public class PageTemplateDraftVersionInfo
  {
    [DataMember]
    public WcfChange VersionInfo { get; set; }

    [DataMember]
    public string PageTitle { get; set; }

    [DataMember]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool HasConflict { get; set; }

    [DataMember]
    public bool IsLockedByCurrentUser { get; set; }

    [DataMember]
    public bool IsLocked { get; set; }

    [DataMember]
    public string LockedByUser { get; set; }

    [DataMember]
    public bool IsEditable { get; set; }

    [DataMember]
    public bool IsContentEditable { get; set; }
  }
}
