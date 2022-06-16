// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.ILicensedControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Licensing
{
  /// <summary>
  /// Controls should implement this interface to notify the system if they are licensed or not
  /// </summary>
  public interface ILicensedControl
  {
    /// <summary>
    /// Gets a value indicating whether this instance of the control is licensed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is licensed; otherwise, <c>false</c>.
    /// </value>
    bool IsLicensed { get; }

    /// <summary>
    /// Gets custom the licensing message.If null the system will use a default message
    /// </summary>
    /// <value>The licensing message.</value>
    string LicensingMessage { get; }
  }
}
