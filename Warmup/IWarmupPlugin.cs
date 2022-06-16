// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.IWarmupPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;

namespace Telerik.Sitefinity.Warmup
{
  /// <summary>
  /// Defines the contract for plugins that provide warmup URLs for the <see cref="T:Telerik.Sitefinity.Warmup.WarmupModule" />.
  /// </summary>
  public interface IWarmupPlugin
  {
    /// <summary>Gets the name of the warmup plug-in.</summary>
    /// <value>The name of the warmup plug-in.</value>
    string Name { get; }

    /// <summary>Initializes the new instance of the warmup plug-in.</summary>
    /// <param name="name">The name of the plug-in instance.</param>
    /// <param name="parameters">Collection of parameters to initialize the instance with.</param>
    void Initialize(string name, NameValueCollection parameters);

    /// <summary>Gets the URLs. Will run for every site.</summary>
    /// <returns>The URLs collection.</returns>
    IEnumerable<WarmupUrl> GetUrls();
  }
}
