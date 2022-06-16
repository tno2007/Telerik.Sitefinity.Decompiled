// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ICustomOutputCacheVariation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Defines that contract that allow to register a mechanism to specify different output cache (or no cache) for the page request,
  /// depending on the current context.
  /// </summary>
  public interface ICustomOutputCacheVariation
  {
    /// <summary>
    /// Gets the key. Should return the same value for each request
    /// </summary>
    /// <value>The key.</value>
    string Key { get; }

    /// <summary>
    /// Gets a value indicating whether there's no cache.
    /// If returns true, the GetValue() method is not called.
    /// </summary>
    /// <value>The no cache.</value>
    bool NoCache { get; }

    /// <summary>
    /// Gets the value. Should return different value for each cache variation.
    /// If the property NoCache return true, this method is not called.
    /// </summary>
    /// <returns>Different value for each cache variation</returns>
    string GetValue();
  }
}
