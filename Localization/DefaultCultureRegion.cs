// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.DefaultCultureRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// This class is intended to be used with a <c>using</c> statement, to temporarily set current thread's
  /// culture and UI culture to the Sitefinity default frontend/backend culture and safely restore the old
  /// values, even in the case of exception.
  /// </summary>
  /// <example>
  /// <code>
  /// using (new DefaultCultureRegion())
  /// {
  ///     // Current culture and UI culture are set to Sitefinity's default frontend one.
  /// } // The previous culture and UI culture are restored here, even on exception.
  /// </code>
  /// </example>
  public class DefaultCultureRegion : CultureRegion
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Localization.CultureRegion" /> class and sets the UI culture to the
    /// default frontend/backend UI culture.
    /// </summary>
    /// <param name="backend">
    /// When <c>true</c>, sets the default backend UI culture to the current thread, otherwise sets the
    /// default frontend UI culture one. The default is <c>false</c>.
    /// </param>
    public DefaultCultureRegion(bool backend = false)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      this.Init(backend ? appSettings.DefaultBackendLanguage : appSettings.DefaultFrontendLanguage);
    }
  }
}
