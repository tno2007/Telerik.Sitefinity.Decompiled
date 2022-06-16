// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEditAwareEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// An event contract that contains data when form is in edit mode.
  /// </summary>
  public interface IFormEditAwareEvent : IFormEvent, IFormsModuleEvent, IEvent
  {
    /// <summary>Gets the form entry response edit context.</summary>
    /// <value>The form entry response edit context.</value>
    IFormEntryResponseEditContext FormEntryResponseEditContext { get; }

    /// <summary>Gets a value indicating whether form is in edit mode.</summary>
    /// <value>The is edit mode.</value>
    bool IsEditMode { get; }
  }
}
