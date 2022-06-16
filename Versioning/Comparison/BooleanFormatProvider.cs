// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Comparison.BooleanFormatProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Versioning.Comparison
{
  /// <summary>Provide formatting for boolean types.</summary>
  /// <remarks>
  /// The standard boolean type does not allow any formatting
  /// by default. Therefore, if a boolean value has a meaning
  /// such as "yes/no", there is no default way to format the
  /// boolean.
  /// 
  /// Notes:
  ///   - The ToString() has an overload that accepts an IFormatProvider
  ///     but apparently (according to MSDN), it does nothing with that
  ///     provider. This can only be used with other formatters such as
  ///     String.Format().
  ///   - The default formatting will return either the TrueString or the
  ///     FalseString, both shared (static) methods of the Boolean type.
  ///     Unfortunately, these are readonly.
  /// </remarks>
  public class BooleanFormatProvider : IFormatProvider, ICustomFormatter
  {
    private string _defaultFormat = "true|false";
    private char _delimiter = '|';

    /// <summary>Construct a standard default instance.</summary>
    /// <remarks></remarks>
    public BooleanFormatProvider()
    {
    }

    /// <summary>Construc a formatter with a custom delimiter.</summary>
    /// <param name="delimiter"></param>
    /// <remarks></remarks>
    public BooleanFormatProvider(char delimiter) => this.Delimiter = delimiter;

    /// <summary>
    /// Construct a formatter with the default indicated format.
    /// </summary>
    /// <param name="defaultFormat"></param>
    /// <remarks></remarks>
    public BooleanFormatProvider(string defaultFormat) => this.DefaultFormat = defaultFormat;

    /// <summary>
    /// Construct a formatter with a custome delimiter and a
    /// default format string.
    /// </summary>
    /// <param name="defaultFormat"></param>
    /// <param name="delimiter"></param>
    /// <remarks></remarks>
    public BooleanFormatProvider(string defaultFormat, char delimiter)
    {
      this.Delimiter = delimiter;
      this.DefaultFormat = defaultFormat;
    }

    /// <summary>Define the default format used.</summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks>
    /// The default is to use true and false.
    /// 
    /// This can be used with the ToString of a boolean:
    /// 
    ///   True.ToString(New BooleanFormatProvider("y|n"))
    /// </remarks>
    public string DefaultFormat
    {
      get => this._defaultFormat;
      set => this._defaultFormat = value;
    }

    /// <summary>
    /// Define the delimiter that separates the true string
    /// from the false string.
    /// </summary>
    /// <value></value>
    /// <returns></returns>
    /// <remarks></remarks>
    public char Delimiter
    {
      get => this._delimiter;
      set => this._delimiter = value;
    }

    /// <summary>Handle the actual formatting of a boolean value.</summary>
    /// <param name="theFormat"></param>
    /// <param name="arg"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    /// <remarks>
    /// Valid formats provide two strings, one for true and one
    /// for false. They are delimited with the character defined
    /// for the Delimiter property, a vertical bar by default. E.g.
    /// 
    ///   y|n
    ///   yes|no
    ///   Loaded|Unloaded
    /// </remarks>
    public string Format(string theFormat, object arg, IFormatProvider formatProvider)
    {
      if (!arg.GetType().Equals(typeof (bool)))
        throw new ArgumentException("Requires a boolean value to format.");
      if (string.IsNullOrEmpty(theFormat))
        theFormat = this.DefaultFormat;
      string[] strArray = theFormat.IndexOf(this.Delimiter) >= 0 ? theFormat.Split(this.Delimiter) : throw new ArgumentException(string.Format("The format is not valid. The valid format is [true string{0}false string], without the square brackets.", (object) this.Delimiter), nameof (theFormat));
      string str1 = strArray[0];
      string str2 = strArray[1];
      return Convert.ToBoolean(arg) ? str1 : str2;
    }

    /// <summary>
    /// Implements IFormatProvider and returns the custom formatter defined
    /// in this class.
    /// </summary>
    /// <param name="formatType"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public object GetFormat(Type formatType) => formatType.Equals(typeof (ICustomFormatter)) ? (object) this : (object) null;
  }
}
