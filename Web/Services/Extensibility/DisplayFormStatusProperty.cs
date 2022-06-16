// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.DisplayFormStatusProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property for retrieving form display status</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class DisplayFormStatusProperty : DisplayStatusProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<DisplayStatus>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      foreach (FormDescription formDescription in items.Cast<FormDescription>())
      {
        List<DisplayStatus> displayStatusList1 = new List<DisplayStatus>();
        string statusKey = (string) null;
        string statusText = (string) null;
        LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) formDescription, culture, ref statusKey, ref statusText);
        List<DisplayStatus> displayStatusList2 = displayStatusList1;
        DisplayStatus displayStatus = new DisplayStatus();
        displayStatus.Name = statusKey;
        displayStatus.Source = StatusSource.Lifecycle.ToString();
        displayStatus.Label = statusKey;
        displayStatus.DetailedLabel = statusText;
        displayStatus.Date = formDescription.DateCreated;
        displayStatus.ExpirationDate = new DateTime?();
        displayStatus.Id = formDescription.Id.ToString();
        displayStatus.PublicationDate = new DateTime?();
        displayStatus.User = UserProfilesHelper.GetUserDisplayName(formDescription.Owner);
        displayStatus.Message = (Message) null;
        displayStatusList2.Add(displayStatus);
        Status status = StatusResolver.Resolve(typeof (FormDescription), manager.Provider.Name, formDescription.Id);
        if (status != null)
          displayStatusList1.Add(new DisplayStatus()
          {
            Name = status.Text,
            Source = status.PrimaryProvider,
            Label = status.Text,
            DetailedLabel = status.Text
          });
        values.Add((object) formDescription, (object) displayStatusList1);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
