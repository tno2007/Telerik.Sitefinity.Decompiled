// Decompiled with JetBrains decompiler
// Type: System.SitefinityExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Utilities;

namespace System
{
  /// <summary>Extension methods for Sitefinity resources</summary>
  public static class SitefinityExtensions
  {
    /// <summary>
    /// Replaces the format items in this instance
    /// with the text equivalent of the value of a corresponding <see cref="T:System.Object" />
    /// instance in a specified array.
    /// </summary>
    /// <param name="value">
    /// A <see cref="T:System.String" /> containing zero or more format items.
    /// </param>
    /// <param name="arguments">
    /// An <see cref="T:System.Object" /> array containing zero or more objects to format.
    /// </param>
    /// <returns>
    /// A copy of format in which the format items have been replaced by the
    /// <see cref="T:System.String" /> equivalent of the corresponding instances of
    /// <see cref="T:System.Object" /> in args.
    /// </returns>
    public static string Arrange(this string value, params object[] arguments) => string.Format((IFormatProvider) CultureInfo.CurrentCulture, value, arguments);

    /// <summary>Determines whether a string is a valid regex pattern.</summary>
    /// <param name="pattern">The string pattern.</param>
    /// <returns></returns>
    public static bool IsValidRegexPattern(this string pattern)
    {
      if (string.IsNullOrEmpty(pattern))
        return false;
      try
      {
        Regex.Match("", pattern);
      }
      catch (ArgumentException ex)
      {
        return false;
      }
      return true;
    }

    /// <summary>
    /// Returns the specified number of characters starting form the left of the string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="length">The number of characters to return.</param>
    /// <returns></returns>
    public static string Left(this string value, int length) => value.Substring(0, length);

    /// <summary>
    /// Returns the specified number of characters starting form the right of the string.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="length">The number of characters to return.</param>
    /// <returns></returns>
    public static string Right(this string value, int length) => value.Substring(value.Length - length, length);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <returns></returns>
    public static string Sub(this string value, int startIndex, int endIndex) => value.Substring(startIndex, endIndex - startIndex + 1);

    /// <summary>
    /// Indicates whether a specified string is <c>null</c>, empty, or
    /// consists only of white-space characters.
    /// </summary>
    /// <param name="value"><c>string</c> to test.</param>
    /// <returns>
    /// <c>true</c> if the value parameter is <c>null</c> <c>String.Empty</c>,
    /// or if <paramref name="value" />  consists exclusively of white-space characters.
    /// </returns>
    /// <remarks>This is an emulation of the String.IsNullOrWhitespace method in .NET 4.0</remarks>
    /// <seealso cref="!:http://msdn.microsoft.com/en-us/library/system.string.isnullorwhitespace%28VS.100%29.aspx" />
    public static bool IsNullOrWhitespace(this string value) => value == null || value.Trim().Length == 0;

    /// <summary>
    /// A shortcut for <see cref="M:System.String.IsNullOrEmpty(System.String)" />
    /// </summary>
    /// <param name="value">String to test</param>
    /// <returns>True if <paramref name="value" /> is <c>null</c> or its <c>Length</c> is 0.</returns>
    public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

    /// <summary>
    /// A shortcut for <see cref="M:Telerik.Sitefinity.Utilities.Utility.IsGuid(System.Guid)" />
    /// </summary>
    /// <param name="value">String to test.</param>
    /// <returns>True if <paramref name="value" /> passes a regular expression for GUID-s.</returns>
    public static bool IsGuid(this string value) => Utility.IsGuid(value);

    /// <summary>
    /// A method that checks if a specified types implements an interface.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <param name="interfaceType">Type of the interface.</param>
    /// <returns></returns>
    public static bool ImplementsInterface(this Type type, Type interfaceType)
    {
      string fullName = interfaceType.FullName;
      return type.GetInterface(fullName) != (Type) null;
    }

    /// <summary>
    /// Make <paramref name="value" />'s first letter lower case
    /// </summary>
    /// <param name="value">String whose first letter to convert to lower case</param>
    /// <returns>Result of String.Empty on failure.</returns>
    public static string LowerFirstLetter(this string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      char lower = char.ToLower(value[0]);
      if (value.Length <= 1)
        return lower.ToString();
      string str = value.Substring(1);
      return lower.ToString() + str;
    }

    /// <summary>
    /// Make <paramref name="value" />'s first letter upper case
    /// </summary>
    /// <param name="value">String whose first letter to convert to upper case</param>
    /// <returns>Result of String.Empty on failure.</returns>
    public static string UpperFirstLetter(this string value)
    {
      if (string.IsNullOrEmpty(value))
        return string.Empty;
      char upper = char.ToUpper(value[0]);
      if (value.Length <= 1)
        return upper.ToString();
      string str = value.Substring(1);
      return upper.ToString() + str;
    }

    /// <summary>Returns a transformed to camelCase string.</summary>
    /// <param name="separators">Optional. You can specify the separators that will be considered when transforming the string.
    /// The default separators are ' ', '-', '.', '_'</param>
    /// <example>
    /// "PascalCasedStrings" -&gt; "pascalCasedStrings"
    /// "space underscore_and.dot separated StrinGs" -&gt; "spaceUnderscoreAndDotSeparatedStrinGs"
    /// </example>
    public static string TocamelCase(this string value, char[] separators = null) => SitefinityExtensions.ChangeStringCase(value, SitefinityExtensions.Case.camelCase, separators);

    /// <summary>Returns a transformed to PascalCase string.</summary>
    /// <param name="separators">Optional. You can specify the separators that will be considered when transforming the string.
    /// The default separators are ' ', '-', '.', '_'</param>
    /// <example>
    /// "camelCasedStrings" -&gt; "CamelCasedStrings"
    /// "space underscore_dash-and.dot separated StrinGs" -&gt; "SpaceUnderscoreDashAndDotSeparatedStrinGs"
    /// </example>
    public static string ToPascalCase(this string value, char[] separators = null) => SitefinityExtensions.ChangeStringCase(value, SitefinityExtensions.Case.PascalCase, separators);

    /// <summary>
    /// Cleans Carriage Return/Linefeed from the supplied string.
    /// Preventing CRLF Injection
    /// </summary>
    /// <param name="value">The input string that will be stripped from all CRLF chars.</param>
    /// <returns></returns>
    public static string RemoveCRLF(string value)
    {
      if (value != null)
        value = value.Replace("\n", string.Empty).Replace("\r", string.Empty).Replace("%0d", string.Empty).Replace("%0D", string.Empty).Replace("%0a", string.Empty).Replace("%0A", string.Empty);
      return value;
    }

    /// <summary>
    /// Truncates a string either to the last word, add ellipsis, etc
    /// </summary>
    /// <param name="valueToTruncate"></param>
    /// <param name="maxLength"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public static string TruncateString(
      this string valueToTruncate,
      int maxLength,
      SitefinityExtensions.TruncateOptions options)
    {
      if (valueToTruncate == null)
        return "";
      if (valueToTruncate.Length <= maxLength)
        return valueToTruncate;
      int num = (options & SitefinityExtensions.TruncateOptions.IncludeEllipsis) == SitefinityExtensions.TruncateOptions.IncludeEllipsis ? 1 : 0;
      bool flag1 = (options & SitefinityExtensions.TruncateOptions.FinishWord) == SitefinityExtensions.TruncateOptions.FinishWord;
      bool flag2 = (options & SitefinityExtensions.TruncateOptions.AllowLastWordToGoOverMaxLength) == SitefinityExtensions.TruncateOptions.AllowLastWordToGoOverMaxLength;
      string str = valueToTruncate;
      if (num != 0)
        maxLength -= 3;
      int startIndex1 = str.LastIndexOf(" ", maxLength, StringComparison.CurrentCultureIgnoreCase);
      if (!flag1)
        str = str.Remove(maxLength);
      else if (flag2)
      {
        int startIndex2 = str.IndexOf(" ", maxLength, StringComparison.CurrentCultureIgnoreCase);
        if (startIndex2 != -1)
          str = str.Remove(startIndex2);
      }
      else if (startIndex1 > -1)
        str = str.Remove(startIndex1);
      if (num != 0 && str.Length < valueToTruncate.Length)
        str += "...";
      return str;
    }

    internal static bool ContainsIgnoreCase(this string text, string valueToCheck) => text.IndexOf(valueToCheck, StringComparison.OrdinalIgnoreCase) >= 0;

    private static string ChangeStringCase(
      string value,
      SitefinityExtensions.Case cases,
      char[] separators)
    {
      if (value == null)
        throw new ArgumentNullException(nameof (value), "Can't change the case of a null string to " + cases.ToString());
      if (value == string.Empty)
        return value;
      char[] chArray = new char[4]{ ' ', '-', '.', '_' };
      if (separators == null)
        separators = chArray;
      string[] strArray = value.Split(separators, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length < 1)
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      int num = 0;
      if (cases == SitefinityExtensions.Case.camelCase)
      {
        stringBuilder.Append(char.ToLowerInvariant(strArray[0][0]));
        stringBuilder.Append(strArray[0].Substring(1));
        num = 1;
      }
      for (int index = num; index < strArray.Length; ++index)
      {
        string str = strArray[index];
        if (!string.IsNullOrEmpty(str))
        {
          stringBuilder.Append(char.ToUpperInvariant(str[0]));
          stringBuilder.Append(str.Substring(1));
        }
      }
      return stringBuilder.ToString();
    }

    [Flags]
    public enum TruncateOptions
    {
      None = 0,
      /// <summary>
      /// Truncates at the last word that fits in the fiven length
      /// Will result in "Lorem ipsum dolor sit..."
      /// </summary>
      FinishWord = 1,
      /// <summary>
      /// Allows the last word to overflow the given length
      /// Will result in "Lorem ipsum dolor sit amet..."
      /// </summary>
      AllowLastWordToGoOverMaxLength = 2,
      /// <summary>Includes ellipsis at the end</summary>
      IncludeEllipsis = 4,
    }

    private enum Case
    {
      PascalCase,
      camelCase,
    }
  }
}
