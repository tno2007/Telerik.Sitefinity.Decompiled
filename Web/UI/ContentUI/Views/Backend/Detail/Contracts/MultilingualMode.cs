// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts.MultilingualMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts
{
  /// <summary>
  /// Defines the possible display modes of Multilingual data.
  /// </summary>
  public enum MultilingualMode
  {
    /// <summary>
    /// The control automatically determines in which mode it will be basing on the specified content type.
    /// In case that the specified content item implements <see cref="T:Telerik.Sitefinity.Localization.ILocalizable" /> the multilingual data will be displayed; otherwise will not be displayed.
    /// </summary>
    Automatic,
    /// <summary>The control will display Multilingual data.</summary>
    On,
    /// <summary>The control will not display Multilingual data.</summary>
    Off,
  }
}
