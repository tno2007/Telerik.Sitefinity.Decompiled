// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.ContentBlockDisplayStatusProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility
{
  /// <summary>A property for retrieving form display status</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class ContentBlockDisplayStatusProperty : CalculatedProperty
  {
    private const string SavedPhrase = "Saved";

    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<DisplayStatus>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (ContentItem contentItem in items.Cast<ContentItem>())
      {
        List<DisplayStatus> displayStatusList1 = new List<DisplayStatus>();
        WcfContentLifecycleStatusFactory.Create<ContentItem>(contentItem, manager as IContentLifecycleManager<ContentItem>);
        List<DisplayStatus> displayStatusList2 = displayStatusList1;
        DisplayStatus displayStatus = new DisplayStatus();
        displayStatus.Name = "Saved";
        displayStatus.Source = StatusSource.Lifecycle.ToString();
        displayStatus.Label = "Saved";
        displayStatus.DetailedLabel = "Saved";
        displayStatus.Date = contentItem.LastModified;
        displayStatus.ExpirationDate = new DateTime?();
        displayStatus.Id = contentItem.Id.ToString();
        displayStatus.PublicationDate = new DateTime?();
        displayStatus.User = UserProfilesHelper.GetUserDisplayName(contentItem.Owner);
        displayStatusList2.Add(displayStatus);
        Status status = StatusResolver.Resolve(typeof (ContentItem), manager.Provider.Name, contentItem.Id);
        if (status != null)
          displayStatusList1.Add(new DisplayStatus()
          {
            Name = status.Text,
            Source = status.PrimaryProvider,
            Label = status.Text,
            DetailedLabel = status.Text
          });
        values.Add((object) contentItem, (object) displayStatusList1);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
