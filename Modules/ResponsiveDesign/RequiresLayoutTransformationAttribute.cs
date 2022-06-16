﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.RequiresLayoutTransformationAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign
{
  /// <summary>
  /// Widgets marked with this attribute force sitefinity to the inject the layout transofrmation CSS - which controls the client side responsive design bahaviour
  /// Such widgets are the Layout control and the Light navigation
  /// </summary>
  [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
  public class RequiresLayoutTransformationAttribute : Attribute
  {
  }
}