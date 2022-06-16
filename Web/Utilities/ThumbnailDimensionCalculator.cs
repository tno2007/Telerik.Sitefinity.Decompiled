// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.ThumbnailDimensionCalculator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Utilities
{
  public class ThumbnailDimensionCalculator
  {
    public const int DefaultMaxThumbnailHeight = 85;
    public const int DefaultMaxThumbnailWidth = 85;

    public ThumbnailDimensionCalculator()
    {
      this.MaxThumbnailWidth = 85;
      this.MaxThumbnailHeight = 85;
    }

    public int MaxThumbnailWidth { get; set; }

    public int MaxThumbnailHeight { get; set; }

    protected int Width { get; set; }

    protected int Height { get; set; }

    public void SetDimensions(int width, int height)
    {
      this.Width = width;
      this.Height = height;
    }

    public int CalculateHeight()
    {
      if (this.Width <= this.MaxThumbnailWidth && this.Height <= this.MaxThumbnailHeight)
        return this.Height;
      return this.Height > this.Width ? this.MaxThumbnailHeight : (int) ((double) this.Height / (double) this.Width * (double) this.MaxThumbnailHeight);
    }

    public int CalculateWidth()
    {
      if (this.Width <= this.MaxThumbnailWidth && this.Height <= this.MaxThumbnailHeight)
        return this.Width;
      return this.Width > this.Height ? this.MaxThumbnailWidth : (int) ((double) this.Width / (double) this.Height * (double) this.MaxThumbnailWidth);
    }
  }
}
