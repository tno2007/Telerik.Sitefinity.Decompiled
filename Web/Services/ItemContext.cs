// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.ItemContext`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services
{
  /// <summary>
  /// Provides context information for item that is exposed in a web service
  /// </summary>
  /// <typeparam name="T">Type of the item</typeparam>
  /// <remarks>
  /// Main benefit of wrapping the content item is that WCF interceptions work. If <typeparamref name="T" /> is not in
  /// the assembly of the web service, surrogates won't work (i.e. dynamic properties and automation transaction entering).
  /// </remarks>
  [DataContract]
  public class ItemContext<T>
  {
    private T item;
    private IEnumerable<ItemWarning> warnings;

    /// <summary>Gets or sets Wrapped data - the item itself.</summary>
    [DataMember]
    public virtual T Item
    {
      get => this.item;
      set => this.item = value;
    }

    /// <summary>
    /// Gets or sets a collection of warning messages to be shown when editing the item.
    /// </summary>
    [DataMember]
    public virtual IEnumerable<ItemWarning> Warnings
    {
      get => this.warnings;
      set => this.warnings = value;
    }
  }
}
