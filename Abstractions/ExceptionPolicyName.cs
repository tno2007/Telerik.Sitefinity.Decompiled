// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ExceptionPolicyName
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Defines exception policy names for application blocks.
  /// </summary>
  public enum ExceptionPolicyName
  {
    /// <summary>
    /// Defines global general purpose policy for the application.
    /// This policy should be used only if there is no suitable more specific policy and
    /// defining new policy does not make sense.
    /// </summary>
    [Description("Global Policy")] Global,
    /// <summary>Defines policy for data provider handlers.</summary>
    [Description("Data Providers Policy")] DataProviders,
    /// <summary>Defines policy for ignored exceptions handlers.</summary>
    [Description("Ignored Exceptions Policy")] IgnoreExceptions,
    /// <summary>Defines policy for unhandled exceptions handlers.</summary>
    [Description("Unhandled Exceptions")] UnhandledExceptions,
    /// <summary>Defines policy for handling HTTP 404 excepton.</summary>
    [Description("HTTP 404 Exceptions")] Http404,
    /// <summary>Defines policy for handling migration exceptions</summary>
    [Description("Migration exceptions")] Migration,
    /// <summary>Defines policy for handling module builder exceptions</summary>
    [Description("Module Builder exceptions")] ModuleBuilder,
  }
}
