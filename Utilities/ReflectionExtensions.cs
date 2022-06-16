// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Utilities.ReflectionExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Reflection;

namespace Telerik.Sitefinity.Utilities
{
  internal static class ReflectionExtensions
  {
    public static object GetPropertyValue(this object instance, string propertyName)
    {
      Type type = instance != null ? instance.GetType() : throw new ArgumentNullException(nameof (instance));
      PropertyInfo propertyInfo = ReflectionExtensions.GetPropertyInfo(type, propertyName);
      return !(propertyInfo == (PropertyInfo) null) ? propertyInfo.GetValue(instance, (object[]) null) : throw new ArgumentOutOfRangeException(nameof (propertyName), string.Format("Couldn't find property {0} in type {1}", (object) propertyName, (object) type.FullName));
    }

    private static PropertyInfo GetPropertyInfo(Type type, string propertyName)
    {
      PropertyInfo property;
      do
      {
        property = type.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        type = type.BaseType;
      }
      while (property == (PropertyInfo) null && type != (Type) null);
      return property;
    }
  }
}
