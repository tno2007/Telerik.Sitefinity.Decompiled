// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.Messages.ContainerInfoMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.InlineEditing.Messages
{
  /// <summary>
  /// Represents the ServiceStack message for the container information model
  /// </summary>
  public class ContainerInfoMessage
  {
    private List<ContainerInfoModel> containeInfoModel;

    public string PageId { get; set; }

    public string PageTitle { get; set; }

    public List<ContainerInfoModel> ContainersInfo
    {
      get
      {
        if (this.containeInfoModel == null)
          this.containeInfoModel = new List<ContainerInfoModel>();
        return this.containeInfoModel;
      }
      set => this.containeInfoModel = value;
    }
  }
}
