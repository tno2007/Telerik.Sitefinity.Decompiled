// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.TargetTaxonNotFoundException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>TargetTaxonNotFound exception</summary>
  internal class TargetTaxonNotFoundException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TargetTaxonNotFoundException" /> class.
    /// </summary>
    public TargetTaxonNotFoundException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TargetTaxonNotFoundException" /> class.
    /// </summary>
    /// <param name="message">The message.</param>
    public TargetTaxonNotFoundException(string message)
      : base(message)
    {
    }
  }
}
