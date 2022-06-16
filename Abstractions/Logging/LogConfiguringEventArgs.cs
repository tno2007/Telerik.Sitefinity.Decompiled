// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.LogConfiguringEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// The event arguments for the <see cref="E:Telerik.Sitefinity.Abstractions.Log.Configuring" /> event.
  /// </summary>
  public class LogConfiguringEventArgs : LogConfigurationEventArgs
  {
    /// <summary>
    /// Gets or sets a value indicating whether the default log configuration should be canceled,
    /// thus allowing to completely override the built-in log configuration.
    /// </summary>
    public bool Cancel { get; set; }
  }
}
