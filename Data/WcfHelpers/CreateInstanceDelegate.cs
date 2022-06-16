// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.WcfHelpers.CreateInstanceDelegate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data.WcfHelpers
{
  /// <summary>
  /// Used to create an instance of <paramref name="requestedType" />
  /// </summary>
  /// <param name="requestedType"><see cref="T:System.Type" /> to create an instance of</param>
  /// <param name="propertyPath">Property path in the original object</param>
  /// <param name="source">The source object graph</param>
  /// <returns>Instance of <paramref name="requestedType" /></returns>
  public delegate object CreateInstanceDelegate(
    Type requestedType,
    string propertyPath,
    object source);
}
