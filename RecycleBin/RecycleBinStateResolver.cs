// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.RecycleBinStateResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.RecycleBin.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.RecycleBin
{
  /// <summary>Resolves Recycle Bin related states</summary>
  internal class RecycleBinStateResolver : IRecycleBinStateResolver
  {
    /// <summary>
    /// Determines whether the specified <paramref name="item" /> can be sent to the Recycle Bin.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="languageName">The language in which the item is marked as deleted. When <c>null</c> this means the item is deleted in all languages.</param>
    /// <returns>
    /// <c>false</c> if the specified item should be permanently deleted otherwise <c>true</c>.
    /// </returns>
    public bool ShouldMoveToRecycleBin(object item, string languageName = null) => this.ShouldMoveToRecycleBin() && item is IRecyclableDataItem dataItem && this.ShouldWholeItemBeDeleted(dataItem, languageName);

    /// <summary>
    /// Determines whether the specified <paramref name="itemType" /> can be sent to the Recycle Bin.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>
    /// <c>false</c> if the specified item should be permanently deleted otherwise <c>true</c>.
    /// </returns>
    public bool ShouldMoveToRecycleBin(Type itemType) => itemType.ImplementsInterface(typeof (IRecyclableDataItem)) && this.ShouldMoveToRecycleBin();

    /// <summary>
    /// Determines whether items should be sent to the Recycle Bin.
    /// </summary>
    /// <returns>
    /// <c>false</c> if items should be permanently deleted otherwise <c>true</c>.
    /// </returns>
    public bool ShouldMoveToRecycleBin() => SystemManager.IsModuleEnabled("RecycleBin") && Config.Get<RecycleBinConfigBase>("RecycleBinConfig").RecycleBinEnabled;

    private bool ShouldWholeItemBeDeleted(IRecyclableDataItem dataItem, string languageName) => languageName == null || this.IsLastAvailableLanguage(dataItem, languageName);

    private bool IsLastAvailableLanguage(IRecyclableDataItem dataItem, string languageName)
    {
      string[] availableLanguages = this.GetAvailableLanguages((object) dataItem);
      return this.IsTheLastAvailableLanguage(languageName, availableLanguages);
    }

    private string[] GetAvailableLanguages(object item, bool includeInvariantLanguage = true)
    {
      string[] source = new string[0];
      if (item is ILocalizable localizable)
        source = localizable.AvailableLanguages;
      if (!includeInvariantLanguage)
        source = ((IEnumerable<string>) source).Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name)).ToArray<string>();
      return source;
    }

    private bool IsTheLastAvailableLanguage(string languageName, string[] availableLanguages)
    {
      IEnumerable<string> source = ((IEnumerable<string>) availableLanguages).Where<string>((Func<string, bool>) (l => l != CultureInfo.InvariantCulture.Name));
      return source.Count<string>() == 1 && source.Contains<string>(languageName);
    }
  }
}
