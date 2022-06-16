// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.CultureRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>
  /// <para>
  /// This class is intended to be used with a <c>using</c> statement, to temporarily set current thread's
  /// culture and UI culture, and safely restore the old values, even in the case of exception.
  /// </para>
  /// <para>
  /// It provides constructors using both <see cref="T:System.Globalization.CultureInfo" /> instances and culture names, both with
  /// single parameter, to set both current culture and current UI culture, and two parameters to send them individually.
  /// </para>
  /// <para>
  /// For convenience, all the constructors accept <c>null</c> as parameter, in which case the current
  /// culture and UI culture are not changed.
  /// </para>
  /// </summary>
  /// <example>
  /// <code>
  /// using (new CultureRegion("bg"))
  /// {
  ///     // "bg" is the current culture and UI culture
  /// } // The previous culture and UI culture are restored here, even on exception.
  /// </code>
  /// </example>
  public class CultureRegion : IDisposable
  {
    private CultureInfo prevUICulture;

    /// <summary>
    /// Sets both current thread's culture and UI culture to the same value
    /// </summary>
    /// <param name="culture">Current thread's new culture and UI culture.</param>
    public CultureRegion(CultureInfo culture) => this.Init(culture);

    /// <summary>
    /// Sets both current thread's culture and UI culture to the same value, specified by name.
    /// </summary>
    /// <param name="cultureName">Current thread's new culture and UI culture name.</param>
    public CultureRegion(string cultureName) => this.Init(cultureName == null ? (CultureInfo) null : CultureInfo.GetCultureInfo(cultureName));

    /// <summary>
    /// Sets both current thread's culture and UI culture to the same value, specified by LCID.
    /// </summary>
    /// <param name="cultureName">Current thread's new culture and UI culture LCID.</param>
    public CultureRegion(int cultureId) => this.Init(AppSettings.CurrentSettings.GetCultureByLcid(cultureId));

    /// <summary>
    /// Sets current thread's culture and UI culture to the individually specified values.
    /// </summary>
    /// <param name="uiCulture">Current thread's new UI culture.</param>
    /// <param name="culture">Current thread's new culture.</param>
    [Obsolete("Use CultureRegion(CultureInfo culture)")]
    public CultureRegion(CultureInfo uiCulture, CultureInfo culture) => this.Init(uiCulture);

    /// <summary>
    /// Sets current thread's culture and UI culture to the individually specified (by name) values.
    /// </summary>
    /// <param name="uiCultureName">Current thread's new UI culture name.</param>
    /// <param name="cultureName">Current thread's new culture name.</param>
    [Obsolete("Use CultureRegion(string culture)")]
    public CultureRegion(string uiCultureName, string cultureName) => this.Init(uiCultureName == null ? (CultureInfo) null : CultureInfo.GetCultureInfo(uiCultureName));

    /// <summary>
    /// Should be used by derived classes which are requried to call <see cref="M:Telerik.Sitefinity.Localization.CultureRegion.Init(System.Globalization.CultureInfo)" />.
    /// </summary>
    protected CultureRegion()
    {
    }

    public void Dispose()
    {
      if (this.prevUICulture == null)
        return;
      SystemManager.CurrentContext.Culture = this.prevUICulture;
    }

    protected void Init(CultureInfo uiCulture)
    {
      if (uiCulture == null)
        return;
      this.prevUICulture = SystemManager.CurrentContext.Culture;
      SystemManager.CurrentContext.Culture = uiCulture;
    }
  }
}
