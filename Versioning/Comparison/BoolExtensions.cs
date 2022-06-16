// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.BoolExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Versioning.Comparison
{
  /// <summary>Extension methods for the boolean type</summary>
  public static class BoolExtensions
  {
    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents this instance.
    /// </summary>
    /// <param name="value">The boolean value to convert as string</param>
    /// <param name="format">The format.</param>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents this instance.
    /// </returns>
    public static string ToString(this bool value, string format)
    {
      string[] strArray = format.Split('|');
      string str1 = strArray[0];
      string str2 = strArray[1];
      return value ? str1 : str2;
    }
  }
}
