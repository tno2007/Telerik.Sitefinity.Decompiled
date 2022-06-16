// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.RelatedData.RelatedDataServiceStackPlugin
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using Telerik.Sitefinity.Services.RelatedData.Messages;

namespace Telerik.Sitefinity.Services.RelatedData
{
  /// <summary>
  /// Represents a ServiceStack plugin for the related data service
  /// </summary>
  public class RelatedDataServiceStackPlugin : IPlugin
  {
    private const string RelatedDataServiceRoute = "/sitefinity/related-data";

    /// <summary>Adding the related data service routes</summary>
    /// <param name="appHost"></param>
    public void Register(IAppHost appHost)
    {
      appHost.RegisterService(typeof (RelatedDataService));
      appHost.Routes.Add<ChildItemMessage>("/sitefinity/related-data" + "/" + "parent-items", "GET").Add<ParentItemMessage>("/sitefinity/related-data" + "/" + "child-items", "GET").Add<DataTypeMessage>("/sitefinity/related-data" + "/" + "data-types", "GET").Add<RelationChangeMessage>("/sitefinity/related-data" + "/" + "relations", "PUT");
    }
  }
}
