// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.FieldHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data
{
  public class FieldHelper
  {
    private static bool GetDataMemberFieldsPredicate(PropertyDescriptor prop, bool excludeReadOnly)
    {
      if (prop is TaxonomyPropertyDescriptor || prop is RelatedDataPropertyDescriptor)
        return true;
      if (!excludeReadOnly || !prop.IsReadOnly)
      {
        HiddenPropertyAttribute attribute = prop.Attributes[typeof (HiddenPropertyAttribute)] as HiddenPropertyAttribute;
        if (prop.Attributes[typeof (DataMemberAttribute)] is DataMemberAttribute && (attribute == null || !attribute.Hidden))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Gets the property descriptors of all public properties, that are marked with <see cref="T:System.Runtime.Serialization.DataMemberAttribute" /> and are not marked with
    /// <see cref="T:Telerik.Sitefinity.Model.HiddenPropertyAttribute" />.
    /// </summary>
    /// <param name="type">The type which properties are reflected.</param>
    /// <param name="excludeReadOnly">When <c>true</c> all read-only properties are excluded from the result.</param>
    public static IEnumerable<PropertyDescriptor> GetFields(
      Type type,
      bool excludeReadOnly = false)
    {
      return FieldHelper.GetFields(type, (Func<PropertyDescriptor, bool>) (prop => FieldHelper.GetDataMemberFieldsPredicate(prop, excludeReadOnly)));
    }

    public static IEnumerable<PropertyDescriptor> GetFields(
      Type type,
      Func<PropertyDescriptor, bool> predicate)
    {
      return TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>().Where<PropertyDescriptor>(predicate);
    }

    public static IEnumerable<PropertyDescriptor> GetAllFields(
      Type type)
    {
      return TypeDescriptor.GetProperties(type).Cast<PropertyDescriptor>();
    }
  }
}
