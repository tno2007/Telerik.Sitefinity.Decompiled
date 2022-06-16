// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.ContainerInfoModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.InlineEditing
{
  /// <summary>This class represents the container info model</summary>
  public class ContainerInfoModel
  {
    public string ItemId { get; set; }

    public string ItemType { get; set; }

    public string DisplayType { get; set; }

    public string Provider { get; set; }

    public LifecycleStatusModel ItemStatus { get; set; }

    public bool IsPageControl { get; set; }

    public string DetailsViewUrl { get; set; }

    public List<FieldModel> Fields { get; set; }
  }
}
