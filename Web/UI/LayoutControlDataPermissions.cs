// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.LayoutControlDataPermissions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Web.UI
{
  [DataContract]
  public class LayoutControlDataPermissions
  {
    [DataMember]
    public string ControlDataID;
    [DataMember]
    public bool DropOn;
    [DataMember]
    public bool View;
    [DataMember]
    public bool Move;
    [DataMember]
    public bool EditProperties;
    [DataMember]
    public bool Delete;
    [DataMember]
    public bool ChangeOwner;
    [DataMember]
    public bool ChangePermissions;

    public static LayoutControlDataPermissions Create(
      ControlData layoutControlData)
    {
      if (layoutControlData == null)
        return (LayoutControlDataPermissions) null;
      string permissionSet = "LayoutElement";
      return new LayoutControlDataPermissions()
      {
        ControlDataID = layoutControlData.Id.ToString(),
        DropOn = (layoutControlData.IsGranted(permissionSet, "DropOnLayout") ? 1 : 0) != 0,
        View = (layoutControlData.IsGranted(permissionSet, "ViewLayout") ? 1 : 0) != 0,
        EditProperties = (layoutControlData.IsGranted(permissionSet, "EditLayoutProperties") ? 1 : 0) != 0,
        Delete = (layoutControlData.IsGranted(permissionSet, "DeleteLayout") ? 1 : 0) != 0,
        ChangeOwner = (layoutControlData.IsGranted(permissionSet, "ChangeLayoutOwner") ? 1 : 0) != 0,
        ChangePermissions = (layoutControlData.IsGranted(permissionSet, "ChangeLayoutOwner") ? 1 : 0) != 0
      };
    }

    public string ToJson() => new JavaScriptSerializer().Serialize((object) this);
  }
}
