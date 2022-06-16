// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.WarmupUrl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Warmup
{
  /// <summary>
  /// URL data-transfer object that contains the necessary information needed by <see cref="T:Telerik.Sitefinity.Warmup.IWarmupPlugin" /> instances when providing warmup URLs to the <see cref="T:Telerik.Sitefinity.Warmup.WarmupModule" />.
  /// </summary>
  public class WarmupUrl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Warmup.WarmupUrl" /> class.
    /// </summary>
    /// <param name="url">The URL.</param>
    public WarmupUrl(string url) => this.Url = url;

    /// <summary>Gets the URL to be requested.</summary>
    /// <value>The URL to be requested.</value>
    public string Url { get; private set; }

    /// <summary>Gets or sets the warmup priority.</summary>
    /// <value>The warmup priority.</value>
    public WarmupPriority Priority { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the warmup of this instance should vary by user agent.
    /// </summary>
    /// <value>
    /// <c>true</c> if the warmup of this instance should vary by user agent; otherwise, <c>false</c>.
    /// </value>
    public bool VaryByUserAgent { get; set; }
  }
}
