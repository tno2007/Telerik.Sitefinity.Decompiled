// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Forms.FormControlFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Fluent.Forms
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a controls management on a single Sitefinity form.
  /// </summary>
  public class FormControlFacade : 
    IItemFacade<FormControlFacade, ControlData>,
    IFacade<FormControlFacade>
  {
    private AppSettings appSettings;
    private FormFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormControlFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public FormControlFacade(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Forms.FormControlFacade" /> class.
    /// </summary>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="appSettings">The app settings that configure the way fluent API will function.</param>
    public FormControlFacade(FormFacade parentFacade, AppSettings appSettings)
      : this(appSettings)
    {
      this.parentFacade = parentFacade;
    }

    public FormControlFacade CreateNew() => throw new NotImplementedException();

    public FormControlFacade CreateNew(Guid itemId) => throw new NotImplementedException();

    public ControlData Get() => throw new NotImplementedException();

    public FormControlFacade Set(ControlData item) => throw new NotImplementedException();

    public FormControlFacade Do(Action<ControlData> setAction) => throw new NotImplementedException();

    public FormControlFacade SaveChanges() => throw new NotImplementedException();

    public FormControlFacade CancelChanges() => throw new NotImplementedException();

    public FormControlFacade Delete() => throw new NotImplementedException();
  }
}
