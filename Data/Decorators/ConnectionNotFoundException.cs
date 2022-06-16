// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Decorators.ConnectionNotFoundException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Data.Decorators
{
  /// <summary>Represents database connection exception.</summary>
  public class ConnectionNotFoundException : ConfigurationErrorsException
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Decorators.ConnectionNotFoundException" /> class.
    /// </summary>
    /// <param name="connectionName">Name of the connection.</param>
    public ConnectionNotFoundException(string connectionName)
      : base(Res.Get<ErrorMessages>().ConnectionNotFound.Arrange((object) connectionName))
    {
    }
  }
}
