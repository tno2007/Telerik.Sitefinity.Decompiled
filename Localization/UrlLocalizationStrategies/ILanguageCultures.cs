// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.UrlLocalizationStrategies.ILanguageCultures
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;

namespace Telerik.Sitefinity.Localization.UrlLocalizationStrategies
{
  /// <summary>
  /// Helper interface to store the cultures for a specified culture element / system language
  /// </summary>
  public interface ILanguageCultures
  {
    /// <summary>Gets the key.</summary>
    /// <value>The key.</value>
    string Key { get; }

    /// <summary>Gets or sets the culture.</summary>
    /// <value>The culture.</value>
    CultureInfo Culture { get; set; }

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    CultureInfo UICulture { get; set; }
  }
}
