// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.TypeAction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// To be used as key in the cache of TransactionPermissionAttribute objects: a key for the dictionary, consisting of a type and an action.
  /// </summary>
  public class TypeAction : IEqualityComparer<TypeAction>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.TypeAction" /> class.
    /// </summary>
    /// <param name="newTypeKey">The new type key.</param>
    /// <param name="newActionKey">The new action key.</param>
    public TypeAction(
      Type newTypeKey,
      SecurityConstants.TransactionActionType newActionKey)
    {
      this.TypeKey = newTypeKey;
      this.ActionKey = newActionKey;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.TypeAction" /> class.
    /// </summary>
    public TypeAction()
    {
    }

    /// <summary>Gets or sets the action key.</summary>
    /// <value>The action key.</value>
    public SecurityConstants.TransactionActionType ActionKey { get; set; }

    /// <summary>Gets or sets the type key.</summary>
    /// <value>The type key.</value>
    public Type TypeKey { get; set; }

    /// <summary>Equals the specified x.</summary>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    /// <returns></returns>
    public bool Equals(TypeAction x, TypeAction y) => x.TypeKey == y.TypeKey && x.ActionKey == y.ActionKey;

    /// <summary>Returns a hash code for this instance.</summary>
    /// <param name="obj">The obj.</param>
    /// <returns>
    /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
    /// </returns>
    public int GetHashCode(TypeAction obj) => (obj.TypeKey.AssemblyQualifiedName + "|" + obj.ActionKey.ToString()).GetHashCode();
  }
}
