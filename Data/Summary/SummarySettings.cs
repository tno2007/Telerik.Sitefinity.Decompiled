// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Summary.SummarySettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Data.Summary
{
  /// <summary>
  ///     Represents a structure of settings used to create text summaries for
  ///     <see cref="!:ContentView">ContentView</see> control.
  /// </summary>
  [TypeConverter(typeof (SummarySettingsConverter))]
  [Serializable]
  public struct SummarySettings
  {
    /// <summary>
    /// Returns an instance of SummarySettings object with default settings.
    /// </summary>
    private SummaryMode mode;
    private int count;
    private string metaField;
    private bool clearFormatting;
    private string[] tagsToClear;
    private bool clearAllTags;

    /// <summary>
    /// Creates new instance of SummerySettings structure with the specified mode, count and formatting option. Important! SummeryMode.MetaField cannot be used with this constructor.
    /// </summary>
    /// <param name="mode">Defines how summery is created, by cropping the specified number of words, sentences or paragraphs.  Important! SummeryMode.MetaField cannot be used with this constructor.</param>
    /// <param name="count">Specifies the number of words, sentences or paragraphs that should be cropped when constructing the summery.</param>
    /// <param name="clearFormatting">Indicates if text formatting such as colors, bold, italic and etc. should be cleared in the summery.</param>
    /// <param name="tagsToClear">Lists the HTML tags to clear from the text.</param>
    /// <param name="clearAllTags">If set to True, indicating to clear all HTML tags; Otherwise clears only the ones specified in tagsToClear.</param>
    public SummarySettings(SummaryMode mode, int count, bool clearFormatting, bool clearAllTags)
      : this(mode, count, clearFormatting, new string[0])
    {
      this.clearAllTags = clearAllTags;
    }

    /// <summary>
    /// Creates new instance of SummerySettings structure with the specified meta field name. This constructor automatically sets the mode to  SummeryMode.MetaField.
    /// </summary>
    /// <param name="metaField">Specifies the name of the meta field that will be used for summery.</param>
    /// <param name="clearAllTags">If set to True, indicating to clear all HTML tags; Otherwise clears none.</param>
    public SummarySettings(string metaField, bool clearAllTags)
      : this(metaField)
    {
      this.clearAllTags = clearAllTags;
    }

    /// <summary>
    /// Creates new instance of SummerySettings structure with the specified mode, count and formatting option. Important! SummeryMode.MetaField cannot be used with this constructor.
    /// </summary>
    /// <param name="mode">Defines how summery is created, by cropping the specified number of words, sentences or paragraphs.  Important! SummeryMode.MetaField cannot be used with this constructor.</param>
    /// <param name="count">Specifies the number of words, sentences or paragraphs that should be cropped when constructing the summery.</param>
    /// <param name="clearFormatting">Indicates if text formatting such as colors, bold, italic and etc. should be cleared in the summery.</param>
    /// <param name="tagsToClear">Lists the HTML tags to clear from the text.</param>
    public SummarySettings(
      SummaryMode mode,
      int count,
      bool clearFormatting,
      params string[] tagsToClear)
    {
      this.mode = mode;
      this.count = count;
      this.metaField = string.Empty;
      this.clearFormatting = clearFormatting;
      this.tagsToClear = tagsToClear;
      this.clearAllTags = false;
    }

    /// <summary>
    /// Creates new instance of SummerySettings structure with the specified meta field name. This constructor automatically sets the mode to  SummeryMode.MetaField.
    /// </summary>
    /// <param name="metaField">Specifies the name of the meta field that will be used for summery.</param>
    public SummarySettings(string metaField)
    {
      this.mode = SummaryMode.MetaField;
      this.metaField = metaField;
      this.count = -1;
      this.clearFormatting = false;
      this.tagsToClear = new string[0];
      this.clearAllTags = false;
    }

    /// <summary>
    /// Defines how summery is created, by cropping a specified number of words, sentences or paragraphs or by using the content of a specified meta field.
    /// </summary>
    public SummaryMode Mode => this.mode;

    /// <summary>
    /// Specifies the number of words, sentences or paragraphs that should be cropped when constructing the summery.
    /// </summary>
    public int Count => this.count;

    /// <summary>
    /// Indicates if text formatting such as colors, bold, italic and etc. should be cleared in the summery.
    /// </summary>
    public bool ClearFormatting => this.clearFormatting;

    /// <summary>
    /// Specifies the name of the meta field that will be used for summery.
    /// </summary>
    public string MetaField => this.metaField;

    /// <summary>Lists the HTML tags to clear from the text.</summary>
    public string[] TagsToClear => this.tagsToClear;

    /// <param name="tagsToClear"></param>
    /// <param name="clearAllTags"></param>
    /// <summary>
    /// If set to True, indicating to clear all HTML tags; Otherwise clears only the ones specified in tagsToClear.
    /// </summary>
    public bool ClearAllTags => this.clearAllTags;

    /// <summary>Returns a string representation of the structure.</summary>
    /// <returns>The string representing the current structure.</returns>
    public override string ToString()
    {
      string str1 = this.mode.ToString();
      if (this.mode == SummaryMode.MetaField)
        return str1 + "; " + this.metaField;
      if (this.mode == SummaryMode.None)
        return str1;
      string str2 = string.Empty;
      if (this.tagsToClear != null && this.tagsToClear.Length != 0)
        str2 = "; {" + string.Join(",", this.tagsToClear) + "}";
      return str1 + "; " + this.count.ToString() + "; " + this.clearFormatting.ToString() + str2;
    }

    /// <summary>
    /// Converts the string representation of a SummarySettings structure to its object equivalent.
    /// Format: "Mode;Count;ClearFormatting;{tag1, tag2}"
    /// </summary>
    /// <param name="str">A string containing a representation of a SummarySettings structure to convert.</param>
    /// <returns>A SummarySettings object equivalent to the representation contained in str.</returns>
    public static SummarySettings Parse(string str)
    {
      int length1 = !string.IsNullOrEmpty(str) ? str.IndexOf(';') : throw new ArgumentNullException(nameof (str));
      if (length1 == -1)
      {
        if (str.Trim() == "None")
          return new SummarySettings();
        throw new ArgumentException(Res.Get<SummaryResources>().InvalidStringFormat);
      }
      string metaField = str.Substring(length1 + 1).Trim();
      SummaryMode mode = (SummaryMode) Enum.Parse(typeof (SummaryMode), str.Substring(0, length1).Trim());
      switch (mode)
      {
        case SummaryMode.None:
          return new SummarySettings();
        case SummaryMode.MetaField:
          return new SummarySettings(metaField);
        default:
          int length2 = metaField.IndexOf(';');
          int count = length2 != -1 ? int.Parse(metaField.Substring(0, length2).Trim()) : throw new ArgumentException(Res.Get<SummaryResources>().InvalidStringFormat);
          int num = metaField.IndexOf(';', length2 + 1);
          bool clearFormatting;
          string[] strArray;
          if (num == -1)
          {
            clearFormatting = bool.Parse(metaField.Substring(length2 + 1).Trim());
            strArray = new string[0];
          }
          else
          {
            clearFormatting = bool.Parse(metaField.Substring(length2 + 1, num - length2 - 1).Trim());
            strArray = metaField.Substring(num + 1).Trim().TrimStart('{').TrimEnd('}').Split(new char[2]
            {
              ',',
              ' '
            }, StringSplitOptions.RemoveEmptyEntries);
          }
          return new SummarySettings(mode, count, clearFormatting, strArray);
      }
    }
  }
}
