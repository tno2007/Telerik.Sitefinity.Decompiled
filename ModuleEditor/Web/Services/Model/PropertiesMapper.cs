// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModuleEditor.Web.Services.Model.PropertiesMapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.ModuleEditor.Web.Services.Model
{
  /// <summary>
  /// The class is responsible for mapping flat properties between objects.
  /// </summary>
  public static class PropertiesMapper
  {
    /// <summary>Determines whether the specified type is definition.</summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// 	<c>true</c> if the specified type is definition; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDefinition(this Type type) => typeof (IDefinition).IsAssignableFrom(type);

    /// <summary>
    /// Determines whether [is array of definition] [the specified type].
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// 	<c>true</c> if [is array of definition] [the specified type]; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsArrayOfDefinition(this Type type) => type.IsArray && typeof (IDefinition).IsAssignableFrom(type.GetElementType());

    /// <summary>
    /// Copies all the properties of the "from" object to this object if they exist.
    /// </summary>
    /// <param name="to">The object in which the properties are copied</param>
    /// <param name="from">The object which is used as a source</param>
    /// <param name="excludedProperties">Exclude these properties from the copy</param>
    public static void CopyPropertiesFrom(this object to, object from, string[] excludedProperties)
    {
      Type type1 = to.GetType();
      Type type2 = from.GetType();
      foreach (PropertyInfo property in type2.GetProperties())
      {
        if (excludedProperties == null || !((IEnumerable<string>) excludedProperties).Contains<string>(property.Name))
        {
          PropertyInfo propertyInfo = type1 == type2 ? property : type1.GetProperty(property.Name);
          if (propertyInfo != (PropertyInfo) null && propertyInfo.CanWrite & propertyInfo.PropertyType.Name == property.PropertyType.Name && !property.PropertyType.IsDefinition())
          {
            object obj = property.GetValue(from, (object[]) null);
            propertyInfo.SetValue(to, obj, (object[]) null);
          }
        }
      }
    }

    /// <summary>
    /// Copies all the properties of the "from" object to this object if they exist.
    /// </summary>
    /// <param name="to">The object in which the properties are copied</param>
    /// <param name="from">The object which is used as a source</param>
    public static void CopyPropertiesFrom(this object to, object from) => to.CopyPropertiesFrom(from, (string[]) null);

    /// <summary>
    /// Copies all the properties of this object to the "to" object
    /// </summary>
    /// <param name="to">The object in which the properties are copied</param>
    /// <param name="from">The object which is used as a source</param>
    public static void CopyPropertiesTo(this object from, object to) => to.CopyPropertiesFrom(from, (string[]) null);

    /// <summary>
    /// Copies all the properties of this object to the "to" object
    /// </summary>
    /// <param name="to">The object in which the properties are copied</param>
    /// <param name="from">The object which is used as a source</param>
    /// <param name="excludedProperties">Exclude these properties from the copy</param>
    public static void CopyPropertiesTo(this object from, object to, string[] excludedProperties) => to.CopyPropertiesFrom(from, excludedProperties);
  }
}
