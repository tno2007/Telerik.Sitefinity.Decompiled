// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.LifecycleDecoratorForms
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Lifecycle
{
  public class LifecycleDecoratorForms : LifecycleDecorator<FormDescription, FormDraft>
  {
    public LifecycleDecoratorForms(FormsManager manager)
      : base((ILifecycleManager<FormDescription, FormDraft>) manager)
    {
    }

    public FormsManager FormsManager => (FormsManager) this.Manager;

    /// <summary>
    /// Checks in the item in the "temp" state. Item becomes master after the check in.
    /// </summary>
    /// <param name="temp"></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current UI culture will be used.
    /// </remarks></param>
    /// <param name="deleteTemp"></param>
    /// <returns>An item in master state.</returns>
    public override FormDraft CheckIn(
      FormDraft temp,
      CultureInfo culture = null,
      bool deleteTemp = true)
    {
      return base.CheckIn(temp, culture, deleteTemp);
    }

    /// <summary>
    /// Checks out the content in master state. Item becomes temp after the check out.
    /// </summary>
    /// <param name="masterItem"></param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was checked out in temp state.</returns>
    public override FormDraft CheckOut(FormDraft masterItem, CultureInfo culture = null) => base.CheckOut(masterItem, culture);

    /// <summary>
    /// Edits the content in live state. Item becomes master after the edit.
    /// </summary>
    /// <param name="item">Item in live state that is to be edited.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>A content that was edited in master state.</returns>
    public override FormDraft Edit(FormDescription item, CultureInfo culture = null) => base.Edit(item, culture);

    /// <summary>
    /// Publishes the content in master state. Item becomes live after the publish.
    /// </summary>
    /// <param name="draft">Item in master state that is to be published.</param>
    /// <param name="culture">The culture in which to perform the operation.
    /// <remarks>In monolingual the culture is ignored.
    /// In multilingual mode if null - the current ui culture will be used.
    /// </remarks></param>
    /// <returns>Published content item</returns>
    public override FormDescription Publish(FormDraft draftItem, CultureInfo culture = null)
    {
      FormDescription formDescription = base.Publish(draftItem, culture);
      foreach (FormDraftControl control in (IEnumerable<FormDraftControl>) draftItem.Controls)
      {
        if (typeof (IFormFieldControl).IsAssignableFrom(FormsManager.GetFormControlType((ControlData) control)) && !control.Published)
          control.Published = true;
      }
      this.FormsManager.BuildDynamicType(formDescription);
      return formDescription;
    }

    /// <summary>
    /// Copies data from the specified draft item to the specified live item.
    /// </summary>
    /// <param name="draftItem">The draft item to get data from.</param>
    /// <param name="liveItem">The live item to write data to.</param>
    /// <param name="culture">The culture of the operation.</param>
    public override void Copy(FormDraft draftItem, FormDescription liveItem, CultureInfo culture) => this.FormsManager.CopyFormCommonData<FormDraftControl, FormControl>((IFormData<FormDraftControl>) draftItem, (IFormData<FormControl>) liveItem, CopyDirection.CopyToOriginal, culture, culture);

    /// <summary>
    /// Copies data from the specified source draft item to the specified target draft item.
    /// </summary>
    /// <param name="sourceDraft">The draft item to get data from.</param>
    /// <param name="targetDraft">The draft item to write data to.</param>
    /// <param name="culture">The culture of the operation.</param>
    public override void Copy(FormDraft sourceDraft, FormDraft targetDraft, CultureInfo culture)
    {
      CopyDirection copyDirection = this.FormsManager.ResolveDraftsCopyDirection((DraftData) sourceDraft, (DraftData) targetDraft);
      this.FormsManager.CopyFormCommonData<FormDraftControl, FormDraftControl>((IFormData<FormDraftControl>) sourceDraft, (IFormData<FormDraftControl>) targetDraft, copyDirection, culture, culture);
      targetDraft.Name = sourceDraft.Name;
      targetDraft.Version = sourceDraft.Version;
    }

    /// <summary>
    /// Copies data from the specified live item to the specified draft item.
    /// </summary>
    /// <param name="liveItem">The live item to write data to.</param>
    /// <param name="draftItem">The draft item to get data from.</param>
    /// <param name="culture">The culture of the operation.</param>
    public override void Copy(FormDescription liveItem, FormDraft draftItem, CultureInfo culture)
    {
      this.FormsManager.CopyFormCommonData<FormControl, FormDraftControl>((IFormData<FormControl>) liveItem, (IFormData<FormDraftControl>) draftItem, CopyDirection.OriginalToCopy, culture, culture);
      draftItem.Name = liveItem.Name;
      draftItem.Version = liveItem.Version;
    }
  }
}
