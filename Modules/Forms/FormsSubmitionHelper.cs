// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.FormsSubmitionHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Modules.Forms.Web;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>This class provides API for forms submit.</summary>
  public class FormsSubmitionHelper
  {
    /// <summary>Saves the forms entry.</summary>
    /// <param name="formEntry">The form entry.</param>
    public virtual void Save(FormEntryDTO formEntry) => formEntry.EntryId = FormsHelper.SaveFormsEntry(formEntry.FormDescription.Id, (IEnumerable<KeyValuePair<string, object>>) formEntry.PostedData.FormsData, formEntry.PostedData.Files, formEntry.IpAddress, formEntry.FormLanguage, formEntry.NotificationEmails);

    /// <summary>
    /// Validates the form against the preset submit restrictions.
    /// </summary>
    /// <param name="formEntry">The form entry.</param>
    /// <param name="restrictionsError">The restrictions error.</param>
    /// <returns>If restrictions are met.</returns>
    public virtual bool ValidateRestrictions(FormEntryDTO formEntry, out string restrictionsError) => FormsHelper.ValidateFormSubmissionRestrictions(formEntry.FormDescription, formEntry.UserId, formEntry.IpAddress, out restrictionsError);
  }
}
