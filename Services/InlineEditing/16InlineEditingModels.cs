// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.SiteModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.InlineEditing
{
  /// <summary>Contains basic information for a Site</summary>
  public class SiteModel
  {
    public string Name { get; set; }

    public bool IsCurrent { get; set; }

    public Guid SiteId { get; set; }

    public Guid SiteMapRootNodeId { get; set; }
  }
}
