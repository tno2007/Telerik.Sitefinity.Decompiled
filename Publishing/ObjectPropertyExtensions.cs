// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.ObjectPropertyExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Provides access to an arbitrary type object's properties through its <see cref="T:System.ComponentModel.TypeDescriptor" />.
  /// </summary>
  public static class ObjectPropertyExtensions
  {
    /// <summary>
    /// Gets the value of an object property using the <see cref="T:System.ComponentModel.PropertyDescriptorCollection" />
    /// returned by <see cref="M:TypeDescriptor.GetProperties(object)" />
    /// </summary>
    /// <typeparam name="T">The type of the property to be returned</typeparam>
    /// <param name="obj">The object to be searched for a property value.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>The value of the property, if any.</returns>
    public static T GetPropertyValue<T>(this object obj, string propertyName)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
      return obj.GetPropertyValue<T>(properties, propertyName);
    }

    /// <summary>
    /// Gets the value of an object property using a PropertyDescriptorCollection.
    /// </summary>
    /// <typeparam name="T">The type of the property to be returned</typeparam>
    /// <param name="obj">The object to be searched for a property value.</param>
    /// <param name="properties">Represents a collection of <see cref="T:System.ComponentModel.PropertyDescriptor" /> objects.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns>The value of the property, if any.</returns>
    public static T GetPropertyValue<T>(
      this object obj,
      PropertyDescriptorCollection properties,
      string propertyName)
    {
      PropertyDescriptor propertyDescriptor = properties.Find(propertyName, true);
      if (propertyDescriptor == null)
        return default (T);
      object obj1 = propertyDescriptor.GetValue(obj);
      return obj1 != null ? (T) obj1 : default (T);
    }
  }
}
