﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.ImageControlDataModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.InlineEditing
{
  /// <summary>This class represents the image control data model</summary>
  public class ImageControlDataModel
  {
    public string ToolTip { get; set; }

    public string AlternateText { get; set; }

    public Guid ImageId { get; set; }

    public string ProviderName { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public string ThumbnailName { get; set; }
  }
}
