// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.SitefinityFileBrowserContentProviderBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web;
using Telerik.Web.UI.Widgets;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Base class for all sitefinity FileBrowserContentProvider implementations
  /// </summary>
  public abstract class SitefinityFileBrowserContentProviderBase : FileBrowserContentProvider
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.SitefinityFileBrowserContentProviderBase" /> class.
    /// </summary>
    public SitefinityFileBrowserContentProviderBase()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.SitefinityFileBrowserContentProviderBase" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="searchPatterns">The search patterns.</param>
    /// <param name="viewPaths">The view paths.</param>
    /// <param name="uploadPaths">The upload paths.</param>
    /// <param name="deletePaths">The delete paths.</param>
    /// <param name="selectedUrl">The selected URL.</param>
    /// <param name="selectedItemTag">The selected item tag.</param>
    public SitefinityFileBrowserContentProviderBase(
      HttpContext context,
      string[] searchPatterns,
      string[] viewPaths,
      string[] uploadPaths,
      string[] deletePaths,
      string selectedUrl,
      string selectedItemTag)
      : base(context, searchPatterns, viewPaths, uploadPaths, deletePaths, selectedUrl, selectedItemTag)
    {
    }

    /// <summary>
    /// Use <see cref="T:Telerik.Sitefinity.Modules.Pages.SitefinityFileBrowserContentProviderBase.LocalizationKeys" /> to return a dictionary of override localization
    /// messages or null to return none.
    /// </summary>
    /// <returns>Dictionary of overridden messages or null if none are overridden</returns>
    public virtual Dictionary<SitefinityFileBrowserContentProviderBase.LocalizationKeys, string> GetOverriddenLocalizationMessages() => (Dictionary<SitefinityFileBrowserContentProviderBase.LocalizationKeys, string>) null;

    /// <summary>
    /// Localization key that are used by FileBrowserContentProvider
    /// </summary>
    public enum LocalizationKeys
    {
      /// <summary>Customize message for key "CreateNewFolder"</summary>
      CreateNewFolder,
      /// <summary>Customize message for key "ConfirmDelete"</summary>
      ConfirmDelete,
      /// <summary>Customize message for key "Rename"</summary>
      Rename,
      /// <summary>Customize message for key "OK"</summary>
      OK,
      /// <summary>Customize message for key "Cancel"</summary>
      Cancel,
    }
  }
}
