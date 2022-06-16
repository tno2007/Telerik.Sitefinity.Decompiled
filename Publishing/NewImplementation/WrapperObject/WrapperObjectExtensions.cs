// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.WrapperObjectExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Extension methods of instances of type <see cref="T:Telerik.Sitefinity.Publishing.WrapperObject" />
  /// </summary>
  [Obsolete("Use the instance methods of the WrapperObject class instead.")]
  public static class WrapperObjectExtensions
  {
    /// <summary>
    /// Gets the value of <paramref name="obj" />'s property named <paramref name="propertyName" />.
    /// </summary>
    /// <param name="obj">The wrapper object.</param>
    /// <param name="propertyName">Name of the property.</param>
    [Obsolete("Use WrapperObject.GetPropertyOrNull or WrapperObject.GetPropertyOrDefault<T> instead.")]
    public static object GetWrapperValue(this WrapperObject obj, string propertyName) => obj.GetPropertyOrNull(propertyName);

    /// <summary>
    /// Assuming that <paramref name="obj" /> is a <see cref="T:Telerik.Sitefinity.Publishing.WrapperObject" />,
    /// gets its property named <paramref name="propertyName" />.
    /// </summary>
    /// <param name="obj">The wrapper object.</param>
    /// <param name="propertyName">Name of the property.</param>
    [Obsolete("Cast to WrapperObject and use its GetPropertyOrNull or GetPropertyOrDefault<T>(string propertyName) method instead.")]
    public static object GetWrapperValue(this object obj, string propertyName) => obj is WrapperObject wrapperObject ? wrapperObject.GetPropertyOrNull(propertyName) : throw new ArgumentException("obj is not a WrapperObject");
  }
}
