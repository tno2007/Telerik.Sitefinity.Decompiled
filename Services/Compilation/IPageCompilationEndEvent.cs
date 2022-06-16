// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Compilation.IPageCompilationEndEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Services.Compilation
{
  /// <summary>
  /// This interface describes the contract for an event that contains information about the end of the compilation of a <see cref="T:System.Web.UI.Page" />.
  /// </summary>
  /// <seealso cref="!:Telerik.Sitefinity.Services.IItemCompilationEvent" />
  internal interface IPageCompilationEndEvent : IPageCompilationEvent, IEvent
  {
    /// <summary>Gets the compilation end time in UTC.</summary>
    /// <value>The compilation end time in UTC.</value>
    DateTime End { get; }
  }
}
