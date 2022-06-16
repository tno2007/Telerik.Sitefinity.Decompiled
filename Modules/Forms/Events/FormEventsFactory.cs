// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.FormEventsFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Forms.Web;
using Telerik.Sitefinity.Modules.Forms.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// This class provides API for creating events related to the Forms module.
  /// </summary>
  public class FormEventsFactory
  {
    /// <summary>
    /// Creates <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.FormValidatingEvent" /> event.
    /// </summary>
    /// <param name="formEntry"><see cref="T:Telerik.Sitefinity.Modules.Forms.FormEntryDTO" /> object.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Modules.Forms.Events.FormValidatingEvent" /> object.</returns>
    public virtual FormValidatingEvent GetFormValidatingEvent(
      FormEntryDTO formEntry)
    {
      int num = formEntry.EntryId != Guid.Empty ? 1 : 0;
      IList<IFormEntryEventControl> currentControlsState = FormsHelper.GetCurrentControlsState(num != 0, formEntry.FormDescription, formEntry.EntryId);
      string mode = num != 0 ? FormsControl.FormsDefaultModes.Update : FormsControl.FormsDefaultModes.Create;
      return FormsHelper.CreateFormValidatingEvent(formEntry.FormDescription, formEntry.PostedData, formEntry.IpAddress, formEntry.UserId, (IFormEntryResponseEditContext) new FormEntryResponseEditContext(formEntry.EntryId), currentControlsState, mode);
    }

    /// <summary>
    /// Creates <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.FormValidatingEvent" /> event.
    /// </summary>
    /// <param name="formEntry"><see cref="T:Telerik.Sitefinity.Modules.Forms.FormEntryDTO" /> object.</param>
    /// <param name="postedData">The posted data.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Modules.Forms.Events.FormValidatingEvent" /> object.</returns>
    public virtual FormValidatingEvent GetFormValidatingEvent(
      FormEntryDTO formEntry,
      FormPostedData postedData)
    {
      int num = formEntry.EntryId != Guid.Empty ? 1 : 0;
      IList<IFormEntryEventControl> currentControlsState = FormsHelper.GetCurrentControlsState(num != 0, formEntry.FormDescription, formEntry.EntryId);
      string mode = num != 0 ? FormsControl.FormsDefaultModes.Update : FormsControl.FormsDefaultModes.Create;
      return FormsHelper.CreateFormValidatingEvent(formEntry.FormDescription, postedData, formEntry.IpAddress, formEntry.UserId, (IFormEntryResponseEditContext) new FormEntryResponseEditContext(formEntry.EntryId), currentControlsState, mode);
    }

    /// <summary>
    /// Creates <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.FormSavingEvent" /> event.
    /// </summary>
    /// <param name="formEntry"><see cref="T:Telerik.Sitefinity.Modules.Forms.FormEntryDTO" /> object.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Modules.Forms.Events.FormSavingEvent" /> object.</returns>
    public virtual FormSavingEvent GetFormSavingEvent(FormEntryDTO formEntry)
    {
      int num = formEntry.EntryId != Guid.Empty ? 1 : 0;
      IList<IFormEntryEventControl> currentControlsState = FormsHelper.GetCurrentControlsState(num != 0, formEntry.FormDescription, formEntry.EntryId);
      string mode = num != 0 ? FormsControl.FormsDefaultModes.Update : FormsControl.FormsDefaultModes.Create;
      return FormsHelper.CreateFormSavingEvent(formEntry.FormDescription, formEntry.PostedData, formEntry.IpAddress, formEntry.UserId, (IFormEntryResponseEditContext) new FormEntryResponseEditContext(formEntry.EntryId), currentControlsState, mode);
    }

    /// <summary>
    /// Creates <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.FormSavedEvent" /> event.
    /// </summary>
    /// <param name="formEntry"><see cref="T:Telerik.Sitefinity.Modules.Forms.FormEntryDTO" /> object.</param>
    /// <param name="isUpdate">Determines if in update mode</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Modules.Forms.Events.FormSavedEvent" /> object.</returns>
    public virtual FormSavedEvent GetFormSavedEvent(
      FormEntryDTO formEntry,
      bool isUpdate = true)
    {
      IList<IFormEntryEventControl> currentControlsState = FormsHelper.GetCurrentControlsState(isUpdate, formEntry.FormDescription, formEntry.EntryId);
      string mode = isUpdate ? FormsControl.FormsDefaultModes.Update : FormsControl.FormsDefaultModes.Create;
      return FormsHelper.CreateFormSavedEvent(formEntry.FormDescription, formEntry.PostedData, formEntry.IpAddress, formEntry.UserId, (IFormEntryResponseEditContext) new FormEntryResponseEditContext(formEntry.EntryId), currentControlsState, mode);
    }

    /// <summary>
    /// Creates <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.BeforeFormActionEvent" /> event.
    /// </summary>
    /// <param name="formEntry"><see cref="T:Telerik.Sitefinity.Modules.Forms.FormEntryDTO" /> object.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Modules.Forms.Events.BeforeFormActionEvent" /> object.</returns>
    public virtual BeforeFormActionEvent GetBeforeFormActionEvent(
      FormEntryDTO formEntry)
    {
      int num = formEntry.EntryId != Guid.Empty ? 1 : 0;
      IList<IFormEntryEventControl> currentControlsState = FormsHelper.GetCurrentControlsState(num != 0, formEntry.FormDescription, formEntry.EntryId);
      string mode = num != 0 ? FormsControl.FormsDefaultModes.Update : FormsControl.FormsDefaultModes.Create;
      return FormsHelper.CreateBeforeFormActionEvent(formEntry.FormDescription, formEntry.PostedData, formEntry.IpAddress, formEntry.UserId, (IFormEntryResponseEditContext) new FormEntryResponseEditContext(formEntry.EntryId), currentControlsState, mode);
    }
  }
}
