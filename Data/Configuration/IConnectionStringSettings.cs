// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.IConnectionStringSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// Provides an interface for connection string configuration settings element
  /// </summary>
  public interface IConnectionStringSettings
  {
    /// <summary>Gets or sets the connection string.</summary>
    string ConnectionString { get; set; }

    /// <summary>Gets or sets the key name of the connection string element.</summary>
    string Name { get; set; }

    /// <summary>Gets or sets the provider name property</summary>
    string ProviderName { get; set; }

    /// <summary>Gets or sets the type of the database.</summary>
    /// <value>The type of the database.</value>
    /// <list type="Telerik.Sitefinity.Data.Configuration.DatabaseType"></list>
    DatabaseType DatabaseType { get; set; }

    /// <summary>Gets the parameters.</summary>
    /// <value>The parameters.</value>
    NameValueCollection Parameters { get; }
  }
}
