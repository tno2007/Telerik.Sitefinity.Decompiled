// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.DynamicTypesHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules
{
  internal class DynamicTypesHelper
  {
    /// <summary>
    /// Determines whether the specified type has dynamic lstring properties.
    /// </summary>
    /// <param name="dynamicType"></param>
    public static bool IsTypeLocalizable(Type dynamicType)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(dynamicType))
      {
        if (property.GetType() == typeof (DynamicLstringPropertyDescriptor))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Determines whether the specified field is localizable.
    /// </summary>
    /// <param name="dynamicType"></param>
    /// <param name="fieldName"></param>
    public static bool IsFieldLocalizable(string dynamicType, string fieldName) => DynamicTypesHelper.IsFieldLocalizable(TypeResolutionService.ResolveType(dynamicType), fieldName);

    /// <summary>
    /// Determines whether the specified field is localizable.
    /// </summary>
    /// <param name="dynamicType"></param>
    /// <param name="fieldName"></param>
    public static bool IsFieldLocalizable(Type dynamicType, string fieldName) => TypeDescriptor.GetProperties(dynamicType)[fieldName].GetType() == typeof (DynamicLstringPropertyDescriptor);
  }
}
