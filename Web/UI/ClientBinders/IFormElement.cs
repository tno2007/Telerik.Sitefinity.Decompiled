// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ClientBinders.IFormElementExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Web.UI.ClientBinders
{
  public static class IFormElementExtensions
  {
    public static void PrepareFormElement(this IFormElement formElement)
    {
      if (string.IsNullOrEmpty(formElement.ItemTypeName))
        throw new ArgumentNullException("Instances implementing IFormElement interface, must set the ItemTypeName property before calling this method.");
      if (formElement.ItemTypeName == typeof (bool).FullName)
        formElement.ElementCssClass = CssConstants.CheckBox;
      else if (formElement.ItemTypeName == typeof (DateTime).FullName)
        formElement.ElementCssClass = CssConstants.DatePicker;
      else if (formElement.IsPropertyTypeNumeric())
        formElement.ElementCssClass = CssConstants.Numeric;
      else if (formElement.NeedsEditor)
        formElement.ElementCssClass = CssConstants.ComplexProperty;
      else if (IFormElementExtensions.IsSimpleEnum(formElement))
        formElement.ElementCssClass = CssConstants.Dropdown;
      else
        formElement.ElementCssClass = CssConstants.Text;
    }

    public static bool IsPropertyTypeNumeric(this IFormElement formElement) => formElement.ItemTypeName == typeof (short).FullName || formElement.ItemTypeName == typeof (int).FullName || formElement.ItemTypeName == typeof (long).FullName || formElement.ItemTypeName == typeof (double).FullName || formElement.ItemTypeName == typeof (Decimal).FullName || formElement.ItemTypeName == typeof (float).FullName || formElement.ItemTypeName == typeof (float).FullName || formElement.ItemTypeName == typeof (long).FullName || formElement.ItemTypeName == typeof (short).FullName || formElement.ItemTypeName == typeof (uint).FullName || formElement.ItemTypeName == typeof (ulong).FullName || formElement.ItemTypeName == typeof (ushort).FullName;

    private static bool IsSimpleEnum(IFormElement formElement)
    {
      if (formElement.ItemTypeName != typeof (string).FullName)
      {
        Type type = TypeResolutionService.ResolveType(formElement.ItemTypeName);
        if (type.IsEnum)
          return !type.CustomAttributes.Any<CustomAttributeData>((Func<CustomAttributeData, bool>) (a => a.AttributeType == typeof (FlagsAttribute)));
      }
      return false;
    }
  }
}
