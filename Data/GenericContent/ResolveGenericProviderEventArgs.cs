// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.GenericContent.ResolveGenericProviderEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Data.GenericContent
{
  /// <summary>
  /// Data for the event handlers of the ResolveGenericProvider event
  /// </summary>
  public class ResolveGenericProviderEventArgs : EventArgs
  {
    /// <summary>Creates a new instance</summary>
    /// <param name="contentItemTypeName">Full type name of the content item that is supposed to be served</param>
    public ResolveGenericProviderEventArgs(string contentItemTypeName)
    {
      this.ContentItemTypeName = contentItemTypeName;
      this.ProvidersHash = new AddOnlyDictionary<string, Type>();
    }

    /// <summary>Create a new instance</summary>
    /// <param name="contentItemTypeName">Full type name of the content item that is supposed to be served</param>
    /// <param name="providersHash">Collection that will be modified during the event</param>
    internal ResolveGenericProviderEventArgs(
      string contentItemTypeName,
      IDictionary<string, Type> providersHash)
      : this(contentItemTypeName)
    {
      this.ProvidersHash.Hash = providersHash;
    }

    /// <summary>
    /// Full type name of the content item that is supposed to be served
    /// </summary>
    public string ContentItemTypeName { get; private set; }

    /// <summary>
    /// A hash table of content item full type name and provider serving the content item
    /// </summary>
    public AddOnlyDictionary<string, Type> ProvidersHash { get; private set; }
  }
}
