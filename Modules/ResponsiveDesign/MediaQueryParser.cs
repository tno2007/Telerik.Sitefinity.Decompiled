// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.MediaQueryParser
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text.RegularExpressions;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign
{
  /// <summary>
  /// This class provides a functionality for parsing CSS media queries.
  /// </summary>
  public static class MediaQueryParser
  {
    private static readonly Regex regEx = new Regex("(?<Property>[\\-\\w]+)\\s*:\\s*(?<Value>[\\w/]*)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex numericRegex = new Regex("[^0-9]*", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex ratioRegex = new Regex("[^0-9/]*", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex monochromeRegex = new Regex("\\(\\s*monochrome\\s*\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
    private static readonly Regex gridRegex = new Regex("\\(\\s*grid\\s*\\)", RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>
    /// Parses the string representation of the media query into the structured object.
    /// </summary>
    /// <param name="mediaQuery"></param>
    /// <returns></returns>
    public static CSSMediaQuery Parse(string mediaQuery)
    {
      if (string.IsNullOrEmpty(mediaQuery))
        throw new ArgumentNullException(nameof (mediaQuery));
      CSSMediaQuery cssMediaQuery = new CSSMediaQuery();
      foreach (Match match in MediaQueryParser.regEx.Matches(mediaQuery))
      {
        string lower = match.Groups["Property"].ToString().ToLower();
        string input = match.Groups["Value"].ToString();
        switch (lower)
        {
          case "aspect-ratio":
            cssMediaQuery.AspectRatio = MediaQueryParser.ratioRegex.Replace(input, "");
            continue;
          case "device-aspect-ratio":
            cssMediaQuery.DeviceAspectRatio = MediaQueryParser.ratioRegex.Replace(input, "");
            continue;
          case "device-height":
            cssMediaQuery.DeviceHeight = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "device-width":
            cssMediaQuery.DeviceWidth = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "height":
            cssMediaQuery.Height = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "max-aspect-ratio":
            cssMediaQuery.MaxAspectRatio = MediaQueryParser.ratioRegex.Replace(input, "");
            continue;
          case "max-device-aspect-ratio":
            cssMediaQuery.MaxDeviceAspectRatio = MediaQueryParser.ratioRegex.Replace(input, "");
            continue;
          case "max-device-height":
            cssMediaQuery.MaxDeviceHeight = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "max-device-width":
            cssMediaQuery.MaxDeviceWidth = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "max-height":
            cssMediaQuery.MaxHeight = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "max-monochrome":
            cssMediaQuery.MaxMonochrome = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "max-resolution":
            cssMediaQuery.MaxResolution = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "max-width":
            cssMediaQuery.MaxWidth = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "min-aspect-ratio":
            cssMediaQuery.MinAspectRatio = MediaQueryParser.ratioRegex.Replace(input, "");
            continue;
          case "min-device-aspect-ratio":
            cssMediaQuery.MinDeviceAspectRatio = MediaQueryParser.ratioRegex.Replace(input, "");
            continue;
          case "min-device-height":
            cssMediaQuery.MinDeviceHeight = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "min-device-width":
            cssMediaQuery.MinDeviceWidth = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "min-height":
            cssMediaQuery.MinHeight = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "min-monochrome":
            cssMediaQuery.MinMonochrome = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "min-resolution":
            cssMediaQuery.MinResolution = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "min-width":
            cssMediaQuery.MinWidth = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "orientation":
            cssMediaQuery.Orientation = input;
            continue;
          case "resolution":
            cssMediaQuery.Resolution = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          case "width":
            cssMediaQuery.Width = new int?(int.Parse(MediaQueryParser.numericRegex.Replace(input, "")));
            continue;
          default:
            continue;
        }
      }
      cssMediaQuery.Monochrome = new bool?(MediaQueryParser.monochromeRegex.IsMatch(mediaQuery));
      cssMediaQuery.IsGrid = new bool?(MediaQueryParser.gridRegex.IsMatch(mediaQuery));
      return cssMediaQuery;
    }
  }
}
