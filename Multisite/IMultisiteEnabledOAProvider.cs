// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.IMultisiteEnabledOAProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Multisite
{
  /// <summary>
  /// Marks OpenAccessDataProviders that have mappings to SiteItemLinks table. Its methods have implementations in MultisiteExtensions.
  /// </summary>
  internal interface IMultisiteEnabledOAProvider : 
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
  }
}
