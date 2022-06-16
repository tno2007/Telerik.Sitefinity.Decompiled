// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.IOpenAccessDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Decorators;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Defines properties and methods for OpenAccess based data providers.
  /// </summary>
  [DataProviderDecorator(typeof (OpenAccessDecorator))]
  public interface IOpenAccessDataProvider : 
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    OpenAccessProviderContext Context { get; set; }
  }
}
