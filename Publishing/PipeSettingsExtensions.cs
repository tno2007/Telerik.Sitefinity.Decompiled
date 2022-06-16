// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PipeSettingsExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Extensions helper methods for PipeSettings</summary>
  public static class PipeSettingsExtensions
  {
    /// <summary>Gets the localized name if ResourceClassId is set.</summary>
    /// <param name="settings">The settings.</param>
    /// <returns></returns>
    public static string GetLocalizedUIName(this PipeSettings settings) => !string.IsNullOrEmpty(settings.ResourceClassId) && !string.IsNullOrEmpty(Res.Get(settings.ResourceClassId, settings.UIName, (CultureInfo) null, true, false)) ? Res.Get(settings.ResourceClassId, settings.UIName) : settings.UIName;
  }
}
