// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ExceptionHandlingConfiguringEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// The arguments for events raised when the
  /// exception handling configuration is being created.
  /// </summary>
  public class ExceptionHandlingConfiguringEventArgs : ExceptionHandlingEventArgs
  {
    /// <summary>
    /// Gets or sets a value indicating whether the default exception handling configuration
    /// should be canceled, thus allowing to completely override the built-in settings.
    /// </summary>
    public bool Cancel { get; set; }
  }
}
