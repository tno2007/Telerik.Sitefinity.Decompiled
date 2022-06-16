// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.EnumHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.Utilities
{
  internal static class EnumHelper
  {
    /// <summary>Gets the string value.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string GetStringValue(this Enum value)
    {
      StringValueAttribute[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (StringValueAttribute), false) as StringValueAttribute[];
      return customAttributes.Length == 0 ? (string) null : customAttributes[0].StringValue;
    }

    /// <summary>Gets the text value.</summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string GetTextValue(this Enum value)
    {
      StringTextAttribute[] customAttributes = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof (StringTextAttribute), false) as StringTextAttribute[];
      return customAttributes.Length == 0 ? (string) null : customAttributes[0].TextValue;
    }
  }
}
