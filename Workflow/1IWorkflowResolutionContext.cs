// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.IWorkflowResolutionContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using Telerik.Sitefinity.Multisite;

namespace Telerik.Sitefinity.Workflow
{
  /// <summary>
  /// Holds the item's context needed to determine which workflow to be applied.
  /// </summary>
  public interface IWorkflowResolutionContext
  {
    /// <summary>Gets item's content type.</summary>
    Type ContentType { get; }

    /// <summary>Gets item's provider name.</summary>
    string ContentProviderName { get; }

    /// <summary>Gets item's Id.</summary>
    Guid ContentId { get; }

    /// <summary>Gets item's culture.</summary>
    CultureInfo Culture { get; }

    /// <summary>Gets item's site.</summary>
    ISite Site { get; }
  }
}
