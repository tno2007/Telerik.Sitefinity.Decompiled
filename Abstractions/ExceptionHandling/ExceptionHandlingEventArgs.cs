// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ExceptionHandlingEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// The base class for event arguments passed for exception handling configuration related events.
  /// </summary>
  public class ExceptionHandlingEventArgs : EventArgs
  {
    /// <summary>
    /// Gets or sets the exception handling configuration builder.
    /// </summary>
    /// <value>The exception handling configuration builder.</value>
    public IConfigurationSourceBuilder ConfigurationBuilder { get; set; }

    /// <summary>
    /// Gets or sets the a fluent wrapper around the configuration section
    /// that contains the exception handling settings.
    /// </summary>
    /// <value>The exception handling configuration fluent wrapper.</value>
    public IExceptionConfigurationGivenPolicyWithName Configuration { get; set; }
  }
}
