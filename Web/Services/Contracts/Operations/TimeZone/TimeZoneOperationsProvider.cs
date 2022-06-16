// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.TimeZone.TimeZoneOperationsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Basic;
using Telerik.Sitefinity.SiteSettings.Web.Services;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.TimeZone
{
  internal class TimeZoneOperationsProvider : IOperationProvider
  {
    public IEnumerable<OperationData> GetOperations(Type clrType)
    {
      OperationData operationData1 = OperationData.Create<IEnumerable<TimeZoneModel>>(new Func<OperationContext, IEnumerable<TimeZoneModel>>(this.SfTimeZones));
      operationData1.OperationType = OperationType.Unbound;
      OperationData operationData2 = OperationData.Create<string, TimeZoneModel>(new Func<OperationContext, string, TimeZoneModel>(this.SfDefaultTimeZone));
      operationData2.OperationType = OperationType.Unbound;
      return (IEnumerable<OperationData>) new OperationData[2]
      {
        operationData1,
        operationData2
      };
    }

    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "By design, the calling code expects that signature. The method is called via reflection.")]
    private IEnumerable<TimeZoneModel> SfTimeZones(OperationContext context)
    {
      foreach (TimeZoneInfo sitefinityTimeZone in ObjectFactory.Resolve<ITimeZoneInfoProvider>().GetSitefinityTimeZones())
        yield return new TimeZoneModel()
        {
          Id = sitefinityTimeZone.Id,
          Name = sitefinityTimeZone.DisplayName
        };
    }

    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "By design, the calling code expects that signature. The method is called via reflection.")]
    private TimeZoneModel SfDefaultTimeZone(OperationContext context, string siteId)
    {
      BasicSettingsService basicSettingsService = new BasicSettingsService();
      if (siteId == null)
        siteId = SystemManager.CurrentContext.CurrentSite.Id.ToString();
      string fullName = typeof (TimeZoneSettingsContract).FullName;
      string siteId1 = siteId;
      TimeZoneSettingsContract settingsContract = (TimeZoneSettingsContract) basicSettingsService.GetSettings(fullName, siteId1).Item;
      return new TimeZoneModel()
      {
        Id = settingsContract.TimeZoneId,
        Name = settingsContract.TimeZone.DisplayName
      };
    }
  }
}
