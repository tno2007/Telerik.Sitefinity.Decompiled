// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.DataSending.DataSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.UsageTracking.HttpClients;
using Telerik.Sitefinity.UsageTracking.Model;
using Telerik.Sitefinity.UsageTracking.TrackingReporters;

namespace Telerik.Sitefinity.UsageTracking.DataSending
{
  internal class DataSender : IDataSender, IDisposable
  {
    private readonly ITrackingReporter systemReporter;
    private readonly ITrackingClient trackingClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.UsageTracking.DataSending.DataSender" /> class.
    /// </summary>
    [InjectionConstructor]
    public DataSender()
      : this((ITrackingClient) new TrackingClient(), (ITrackingReporter) new SystemTrackingReporter())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.UsageTracking.DataSending.DataSender" /> class.
    /// </summary>
    /// <param name="trackingClient">The http client used</param>
    /// <param name="systemTrackingReporter">The <see cref="T:Telerik.Sitefinity.UsageTracking.TrackingReporters.ITrackingReporter" /> used to generate the system report.</param>
    internal DataSender(ITrackingClient trackingClient, ITrackingReporter systemTrackingReporter)
    {
      if (trackingClient == null)
        throw new ArgumentNullException(nameof (trackingClient));
      if (systemTrackingReporter == null)
        throw new ArgumentNullException(nameof (systemTrackingReporter));
      this.trackingClient = trackingClient;
      this.systemReporter = systemTrackingReporter;
    }

    /// <inheritdoc />
    public void SendReport()
    {
      List<ITrackingReporter> list = this.ApplicationModules.Values.OfType<ITrackingReporter>().ToList<ITrackingReporter>();
      TrackingReportModel report1 = this.systemReporter.GetReport() as TrackingReportModel;
      foreach (ITrackingReporter trackingReporter in list)
      {
        object report2 = trackingReporter.GetReport();
        report1.ModulesInfo.Add(report2);
      }
      this.trackingClient.SendReportData(JsonConvert.SerializeObject((object) report1, new JsonSerializerSettings()
      {
        ContractResolver = (IContractResolver) new CamelCasePropertyNamesContractResolver()
      }));
    }

    /// <inheritdoc />
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>Disposes the managed resources</summary>
    /// <param name="disposing">Defines whether a disposing is executing now.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.trackingClient == null)
        return;
      this.trackingClient.Dispose();
    }

    protected virtual IDictionary<string, IModule> ApplicationModules => SystemManager.ApplicationModules;
  }
}
