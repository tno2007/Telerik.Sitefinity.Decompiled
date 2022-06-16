// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.SecretDataResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Defines the base logic for secret data resolver</summary>
  public abstract class SecretDataResolver : ISecretDataResolver
  {
    private string name;
    private string title;
    private string resourceClassId;
    private bool isReadOnly;

    /// <summary>
    /// Gets the name of the resolver, used as an uniquely identifier. Should not be changed once deployed.
    /// </summary>
    public string Name => this.name;

    /// <summary>
    /// Gets the title of the resolver visible in the advanced settings UI.
    /// </summary>
    public string Title => this.resourceClassId.IsNullOrEmpty() ? this.title : Res.Get(this.resourceClassId, this.title);

    /// <summary>Initializes the singleton instance of the resolver.</summary>
    /// <param name="name">The name of the resolver.</param>
    /// <param name="config">The configuration parameters.</param>
    public virtual void Initialize(string name, NameValueCollection config)
    {
      this.name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof (name));
      this.title = config["title"];
      config.Remove("title");
      if (this.title.IsNullOrEmpty())
        this.title = name;
      this.resourceClassId = config["resourceClassId"];
      config.Remove("resourceClassId");
    }

    /// <summary>
    /// Gets the mode, defining the persistence and UI behavior.
    /// </summary>
    public abstract SecretDataMode Mode { get; }

    /// <summary>
    /// Gets a value indicating whether this resolver is configurable from advanced settings.
    /// </summary>
    /// <value>
    ///   <c>true</c> if this resolver is configurable; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsReadOnly => this.isReadOnly;

    /// <summary>
    /// Resolves the actual value from key. Invoked by the system when the setting is first access by the API.
    /// The value is not visible in the UI. Instead the key is displayed.
    /// If the mode is set to Encryption, this method should decrypt the key to get the actual value.
    /// </summary>
    /// <param name="key">The key</param>
    /// <returns>The actual value.</returns>
    public abstract string Resolve(string key);

    /// <summary>
    /// Generate the key from the the provided value.
    /// If the mode is set to Encryption, this method should encrypt the value and return the result as a key. However, if the mode is Link, the provided value should be returned as a key.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The key.</returns>
    public abstract string GenerateKey(string value);
  }
}
