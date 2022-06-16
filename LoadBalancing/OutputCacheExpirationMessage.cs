// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.OutputCacheExpirationMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>
  /// Data transfer object for sending "reset application" message.
  /// </summary>
  public class OutputCacheExpirationMessage : SystemMessageBase
  {
    private const string Separator = "-|-";
    private const string KeysSeparator = "-,-";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.OutputCacheExpirationMessage" /> class.
    /// </summary>
    protected OutputCacheExpirationMessage() => this.Key = "OutputCacheExpiration";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.OutputCacheExpirationMessage" /> class.
    /// </summary>
    /// <param name="types">The types.</param>
    /// <param name="keys">The the keys.</param>
    public OutputCacheExpirationMessage(string[] types, string[] keys)
      : this()
    {
      this.Types = types;
      this.Keys = keys;
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.OutputCacheExpirationMessage" /> class.
    /// </summary>
    /// <param name="data">A serialized <see cref="T:Telerik.Sitefinity.LoadBalancing.OutputCacheExpirationMessage" />.</param>
    public OutputCacheExpirationMessage(string data)
      : this()
    {
      this.Deserialize(data);
    }

    /// <summary>Gets cache dependency types.</summary>
    public virtual string[] Types { get; private set; }

    /// <summary>Gets cache dependency keys</summary>
    public virtual string[] Keys { get; private set; }

    /// <inheritdoc />
    public override string MessageData
    {
      get => this.Serialize();
      set => this.Deserialize(value);
    }

    /// <summary>Returns an string representation of the object.</summary>
    /// <returns>Serialized string data.</returns>
    public string Serialize() => string.Join("-,-", this.Types) + "-|-" + string.Join("-,-", this.Types);

    private void Deserialize(string data)
    {
      string[] strArray = data.Split(new string[1]{ "-|-" }, StringSplitOptions.None);
      this.Types = strArray[0].Split(new string[1]{ "-,-" }, StringSplitOptions.RemoveEmptyEntries);
      this.Keys = strArray[1].Split(new string[1]{ "-,-" }, StringSplitOptions.RemoveEmptyEntries);
    }
  }
}
