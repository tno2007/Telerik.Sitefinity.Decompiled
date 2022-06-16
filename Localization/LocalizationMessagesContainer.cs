// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.LocalizationMessagesContainer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Globalization;
using Telerik.Sitefinity.Model.Localization;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// Provides a bridge for Telerik.Sitefinity.Model to get localizable messages from Telerik.Sitefinity
  /// </summary>
  /// <remarks>Hide it from the rest of the system, as this is just a bridge.
  /// The rest of the system should use the Res class, instead.</remarks>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class LocalizationMessagesContainer : ILocalizationMessagesContainer
  {
    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the current culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="classId">The class ID of the resource.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <returns>The localized string.</returns>
    public string Get(string classId, string key) => Res.Get(classId, key);

    /// <summary>
    /// Gets localized string for the provided key form the specified resource using the specified culture.
    /// This method works with the default data provider.
    /// </summary>
    /// <param name="classId">The class ID of the resource.</param>
    /// <param name="key">The key for the resource entry.</param>
    /// <param name="culture">The cultre for the resource.</param>
    /// <returns>The localized string.</returns>
    public string Get(string classId, string key, CultureInfo culture) => Res.Get(classId, key, culture);
  }
}
