// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Extensibility.Templates.TemplateStatusProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.Services.Extensibility.Templates
{
  /// <summary>
  /// A property for retrieving page templates' status property.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class TemplateStatusProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<DisplayStatus>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      if (items == null)
        return (IDictionary<object, object>) values;
      foreach (PageTemplate pageTemplate in items)
      {
        string statusKey = (string) null;
        string statusText = (string) null;
        CultureInfo culture = SystemManager.CurrentContext.Culture;
        LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) pageTemplate, culture, ref statusKey, ref statusText);
        List<DisplayStatus> displayStatusList = new List<DisplayStatus>();
        DisplayStatus displayStatus1 = new DisplayStatus();
        displayStatus1.Name = statusKey;
        displayStatus1.Source = "Lifecycle";
        displayStatus1.Label = statusText;
        displayStatus1.DetailedLabel = statusText;
        displayStatus1.Date = pageTemplate.LastModified;
        DisplayStatus displayStatus2 = displayStatus1;
        if (pageTemplate.HasDraftNewerThanPublished(culture))
          displayStatus2.Date = pageTemplate.GetMasterItem().LastModified;
        displayStatusList.Add(displayStatus2);
        Status status = StatusResolver.Resolve(typeof (PageTemplate), pageTemplate.GetProviderName(), pageTemplate.Id);
        if (status != null)
        {
          DisplayStatus displayStatus3 = new DisplayStatus()
          {
            Name = status.Text,
            Source = status.PrimaryProvider,
            Label = status.Text,
            DetailedLabel = status.Text
          };
          displayStatusList.Add(displayStatus3);
        }
        values.Add((object) pageTemplate, (object) displayStatusList);
      }
      return (IDictionary<object, object>) values;
    }
  }
}
