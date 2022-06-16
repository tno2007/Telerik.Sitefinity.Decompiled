// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Mvc.Proxy.IControllerSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Sitefinity.Mvc.Proxy
{
  /// <summary>
  /// Defines the common interface for objects containing controller settings.
  /// </summary>
  public interface IControllerSettings
  {
    /// <summary>Gets the controller values.</summary>
    /// <value>The values.</value>
    Dictionary<string, object> Values { get; }

    /// <summary>Gets the controller properties.</summary>
    /// <value>The properties of the controller.</value>
    PropertyDescriptorCollection Properties { get; }
  }
}
