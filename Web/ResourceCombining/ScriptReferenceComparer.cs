// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceCombining.ScriptReferenceComparer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.ResourceCombining
{
  /// <summary>Comparison of ScriptReference instances</summary>
  internal class ScriptReferenceComparer : 
    IEqualityComparer<ScriptReference>,
    IComparer<ScriptReference>
  {
    /// <summary>Defines methods to support the comparison of objects for equality.</summary>
    /// <typeparam name="T">The type of objects to compare.This type parameter is contravariant.
    /// That is, you can use either the type you specified or any type that is less derived.
    /// For more information about covariance and contravariance, see Covariance and
    /// Contravariance in Generics.</typeparam>
    public bool Equals(ScriptReference x, ScriptReference y) => x.Name.CompareTo(y.Name) == 0;

    /// <summary>Defines methods to support the comparison of objects for equality.</summary>
    /// <typeparam name="T">The type of objects to compare.This type parameter is contravariant.
    /// That is, you can use either the type you specified or any type that is less derived.
    /// For more information about covariance and contravariance, see Covariance and
    /// Contravariance in Generics.</typeparam>
    public int GetHashCode(ScriptReference obj) => obj.Name.GetHashCode();

    /// <summary>Defines a method that a type implements to compare two objects.</summary>
    /// <typeparam name="T">The type of objects to compare.This type parameter is contravariant.
    /// That is, you can use either the type you specified or any type that is less derived.
    /// For more information about covariance and contravariance, see Covariance and
    /// Contravariance in Generics.</typeparam>
    /// <filterpriority>1</filterpriority>
    public int Compare(ScriptReference x, ScriptReference y) => x.Name.CompareTo(y.Name);
  }
}
