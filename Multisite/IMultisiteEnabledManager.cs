// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.IMultisiteEnabledManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>
  /// Marks manager that manage items that can be shared between sites in Multisite Management. It has some methods implemented in MultisiteExtensions.
  /// </summary>
  internal interface IMultisiteEnabledManager : IManager, IDisposable, IProviderResolver
  {
    Type[] GetShareableTypes();

    Type[] GetSiteSpecificTypes();
  }
}
