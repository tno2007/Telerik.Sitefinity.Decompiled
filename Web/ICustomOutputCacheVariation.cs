// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.CustomOutputCacheVariationBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// The base class to override to register a mechanism to specify different output cache for the page request,
  /// depending on the current context.
  /// </summary>
  [Serializable]
  public abstract class CustomOutputCacheVariationBase : ICustomOutputCacheVariation
  {
    private string key;

    /// <inheritdoc />
    public string Key
    {
      get => this.key;
      set => this.key = value;
    }

    /// <inheritdoc />
    public virtual bool NoCache => false;

    /// <inheritdoc />
    public abstract string GetValue();

    /// <inheritdoc />
    public override int GetHashCode() => this.Key.GetHashCode();

    /// <inheritdoc />
    public override bool Equals(object obj) => this.GetType().Equals(obj.GetType()) && this.Key.Equals(((CustomOutputCacheVariationBase) obj).Key);
  }
}
