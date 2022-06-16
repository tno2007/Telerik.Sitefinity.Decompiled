// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Forms.FormsFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Fluent.Forms
{
  /// <summary>
  /// Fluent API that provides most common functionality needed to work with a collection of Sitefinity forms.
  /// </summary>
  public class FormsFacade
  {
    private AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public FormsFacade(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
  }
}
