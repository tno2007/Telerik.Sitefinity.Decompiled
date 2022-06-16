// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.SecretDataMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Defines how the secret data should be persisted in the configuration and displayed in the UI.
  /// </summary>
  public enum SecretDataMode
  {
    /// <summary>
    /// The value is encrypted, and the encrypted value is persisted in the configuration and visible in the UI.
    /// </summary>
    Encrypt,
    /// <summary>
    /// The actual value is a stored somewhere, only a key is persisted in the configuration and visible in the UI.
    /// </summary>
    Link,
  }
}
