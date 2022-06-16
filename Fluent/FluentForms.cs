// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.FluentForms
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Fluent.Forms;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>
  /// Aggregation class for all facades related to forms module.
  /// </summary>
  public class FluentForms
  {
    private AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.FluentForms" /> class.
    /// </summary>
    /// <param name="appSettings">Instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> class used to configure the facade.</param>
    public FluentForms(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));

    /// <summary>
    /// Provides the methods for working with a single Sitefinty form description. Use this method when you want to work with a new form description.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormFacade" /> that provides fluent API for working with a single form description.</returns>
    public FormFacade Form() => new FormFacade(this.appSettings);

    /// <summary>
    /// Provides the methods for working with a single Sitefinity form description. Use this method when you want to work with an existing form description.
    /// </summary>
    /// <param name="formDescriptionId">The formDescriptionId of the from description you wish to work with.</param>
    /// <returns>An instance of <see cref="!:PageFacade" /> that provides fluent API for working with a single form description.</returns>
    public FormFacade Form(Guid formDescriptionId) => new FormFacade(this.appSettings, formDescriptionId);

    /// <summary>
    /// Provides the methods for working with a single Sitefinity form description. Use this method when you want to work with an existing form description.
    /// </summary>
    /// <param name="formName">The name of the form you whish to work with.</param>
    /// <returns>
    /// An instance of <see cref="!:PageFacade" /> that provides fluent API for working with a single form description.
    /// </returns>
    public FormFacade Form(string formName) => new FormFacade(this.appSettings, formName);

    /// <summary>
    /// Provides the methods for working with a collection of pages.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormsFacade" /> that provides fluent API for working with multiple forms.</returns>
    public FormsFacade Forms() => new FormsFacade(this.appSettings);
  }
}
