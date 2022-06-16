// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Telerik.Sitefinity.Fluent.AnyContent.InitialPrototype.IAnySingularFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent.Telerik.Sitefinity.Fluent.AnyContent.InitialPrototype
{
  internal interface IAnySingularFacade : IAnyContentFacade, IAnyBaseFacade
  {
    IAnyCurrentFacade Get(out Content currentItem);

    Content Get();

    IAnyCurrentFacade Set(Content item);

    IAnyCurrentFacade Do(Action<Content> setAction);

    IAnyCurrentFacade Delete();

    IAnyCurrentFacade CloneFrom(Content source);

    IAnyOrgarnizationFacade Organization();

    IAnyVersioningFacade Versioning();
  }
}
