// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ImageUrlSignatureHashAlgorithm
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>Represents hash algorithms used by image thumbnails.</summary>
  public enum ImageUrlSignatureHashAlgorithm
  {
    /// <summary>128-bit (16-byte) hash value</summary>
    MD5,
    /// <summary>
    /// 160-bit (20-byte) hash value. This is Federal Information Processing Standard compliant algorithm for encryption.
    /// </summary>
    SHA1,
  }
}
