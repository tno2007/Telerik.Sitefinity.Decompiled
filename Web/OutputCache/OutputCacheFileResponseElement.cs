// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.OutputCacheFileResponseElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Caching;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal class OutputCacheFileResponseElement : FileResponseElement
  {
    /// <summary>Gets a value indicating whether is impersonating</summary>
    public bool IsImpersonating { get; private set; }

    /// <summary>
    /// Gets a value indicating whether supports long transmit file
    /// </summary>
    public bool SupportsLongTransmitFile { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.OutputCacheFileResponseElement" /> class
    /// </summary>
    /// <param name="path">Path of the element</param>
    /// <param name="offset">Offset of the element</param>
    /// <param name="length">Length of the element</param>
    public OutputCacheFileResponseElement(string path, long offset, long length)
      : base(path, offset, length)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.OutputCache.OutputCacheFileResponseElement" /> class.
    /// </summary>
    /// <param name="path">Path of the element</param>
    /// <param name="offset">Offset of the element</param>
    /// <param name="length">Length of the element</param>
    /// <param name="isImpersonating">Is impersonating</param>
    /// <param name="supportsLongTransmitFile">Supports long transmit file</param>
    public OutputCacheFileResponseElement(
      string path,
      long offset,
      long length,
      bool isImpersonating,
      bool supportsLongTransmitFile)
      : base(path, offset, length)
    {
      this.IsImpersonating = isImpersonating;
      this.SupportsLongTransmitFile = supportsLongTransmitFile;
    }
  }
}
