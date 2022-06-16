// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Telerik.Sitefinity.Fluent.AnyContent.InitialPrototype.IAnyTempFacadeBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Fluent.AnyContent.Telerik.Sitefinity.Fluent.AnyContent.InitialPrototype
{
  internal interface IAnyTempFacadeBase : IAnySingularFacade, IAnyContentFacade, IAnyBaseFacade
  {
    IAnyParentFacade CheckIn();

    IAnyParentFacade CheckIn(bool excludeVersioning);

    IAnyPublicFacadeBase CheckInAndPublish();

    IAnyPublicFacadeBase CheckInAndPublish(bool excludeVersioning);

    IAnyParentFacade CopyToMaster();

    IAnyParentFacade CopyToMaster(bool excludeVersioning);
  }
}
