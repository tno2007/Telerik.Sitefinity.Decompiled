// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IEtagProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services
{
  /// <summary>Provides API for generating ETag.</summary>
  internal interface IEtagProvider
  {
    /// <summary>Generates ETag by given key and content.</summary>
    /// <param name="key">Key used for generating the hash.</param>
    /// <param name="content">Content used for generating the hash.</param>
    /// <returns>The generated ETag.</returns>
    string GetEtag(string key, byte[] content);
  }
}
