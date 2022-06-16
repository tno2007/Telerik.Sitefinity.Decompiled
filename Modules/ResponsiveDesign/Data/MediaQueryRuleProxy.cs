// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.MediaQueryRuleProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.ResponsiveDesign.Model;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  internal class MediaQueryRuleProxy : IMediaQueryRule
  {
    public MediaQueryRuleProxy(IMediaQueryRule source)
    {
      this.Description = source.Description;
      this.DeviceTypeName = source.DeviceTypeName;
      this.ParentMediaQueryId = source.ParentMediaQueryId;
      this.WidthType = source.WidthType;
      this.ExactWidth = source.ExactWidth;
      this.MinWidth = source.MinWidth;
      this.MaxWidth = source.MaxWidth;
      this.HeightType = source.HeightType;
      this.ExactHeight = source.ExactHeight;
      this.MinHeight = source.MinHeight;
      this.MaxHeight = source.MaxHeight;
      this.Orientation = source.Orientation;
      this.AspectRatio = source.AspectRatio;
      this.IsMonochrome = source.IsMonochrome;
      this.IsGrid = source.IsGrid;
      this.Resolution = source.Resolution;
      this.IsManualMediaQuery = source.IsManualMediaQuery;
      this.ResultingRule = source.ResultingRule;
    }

    public string Description { get; private set; }

    public string DeviceTypeName { get; private set; }

    public Guid ParentMediaQueryId { get; private set; }

    public SizeType WidthType { get; private set; }

    public string ExactWidth { get; private set; }

    public string MinWidth { get; private set; }

    public string MaxWidth { get; private set; }

    public SizeType HeightType { get; private set; }

    public string ExactHeight { get; private set; }

    public string MinHeight { get; private set; }

    public string MaxHeight { get; private set; }

    public DeviceOrientation Orientation { get; private set; }

    public string AspectRatio { get; private set; }

    public bool IsMonochrome { get; private set; }

    public bool IsGrid { get; private set; }

    public string Resolution { get; private set; }

    public bool IsManualMediaQuery { get; private set; }

    public string ResultingRule { get; private set; }
  }
}
