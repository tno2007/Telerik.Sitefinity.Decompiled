// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Configuration.IUrlLocalizationStrategySettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Localization.Configuration
{
  /// <summary>
  /// Defines settings for initializing a localizatuion strategy.
  /// </summary>
  public interface IUrlLocalizationStrategySettings
  {
    /// <summary>
    /// Gets or sets the name of the URL localization strategy.
    /// </summary>
    /// <value>The name of the URL localization strategy.</value>
    string UrlLocalizationStrategyName { get; set; }

    /// <summary>
    /// Gets or sets the type of the URL localization strategy.
    /// </summary>
    /// <value>The type of the URL localization strategy.</value>
    Type UrlLocalizationStrategyType { get; set; }

    /// <summary>Cultures the settings.</summary>
    /// <returns></returns>
    IEnumerable<ICultureSetting> GetCultureSettings();

    /// <summary>
    /// Gets a collection of user-defined parameters for the strategy.
    /// </summary>
    NameValueCollection Parameters { get; set; }
  }
}
