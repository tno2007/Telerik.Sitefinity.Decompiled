// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Data.IXmlConfigStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Configuration.Model;

namespace Telerik.Sitefinity.Configuration.Data
{
  /// <summary>
  /// Defines properties and methods for configuration storage providers
  /// </summary>
  public interface IXmlConfigStorageProvider
  {
    /// <summary>Initializes the specified provider name.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="config">The config.</param>
    /// <param name="managerType">Type of the manager.</param>
    void Initialize(string providerName, NameValueCollection config, Type managerType);

    /// <summary>Gets the element by relative path.</summary>
    /// <param name="path">The path.</param>
    /// <returns></returns>
    IXmlConfigItem GetElement(string path);

    /// <summary>Saves the element.</summary>
    /// <param name="configElement">The config element.</param>
    void SaveElement(IXmlConfigItem configElement);
  }
}
