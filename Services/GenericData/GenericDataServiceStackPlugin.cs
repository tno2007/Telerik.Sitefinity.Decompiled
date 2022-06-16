// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.GenericData.GenericDataServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using Telerik.Sitefinity.Services.GenericData.Messages;

namespace Telerik.Sitefinity.Services.GenericData
{
  public class GenericDataServiceStackPlugin : IPlugin
  {
    private const string GenericDataServiceRoute = "/sitefinity/generic-data";

    /// <summary>Adding the related data service routes</summary>
    /// <param name="appHost"></param>
    public void Register(IAppHost appHost)
    {
      appHost.RegisterService(typeof (GenericDataService));
      appHost.Routes.Add<DataItemMessage>("/sitefinity/generic-data" + "/" + "data-items", "GET").Add<DataItemMessage>("/sitefinity/generic-data" + "/" + "temp-items", "DELETE");
    }
  }
}
