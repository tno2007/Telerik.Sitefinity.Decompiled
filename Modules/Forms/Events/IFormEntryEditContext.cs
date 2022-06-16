// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEditContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// Represents a base class describing the current editing form entry object.
  /// </summary>
  public interface IFormEntryEditContext
  {
    /// <summary>Gets the response entry id.</summary>
    /// <value>The response entry id.</value>
    Guid EntryId { get; }

    /// <summary>Gets user submitting the form.</summary>
    /// <value>The user.</value>
    User User { get; }

    /// <summary>
    /// Gets the IP address of the client that submits the form entry.
    /// </summary>
    string IpAddress { get; }

    /// <summary>Gets ID of the form being submitted.</summary>
    Guid FormId { get; }

    /// <summary>
    /// Gets the unique (code) name of the form being submitted.
    /// </summary>
    string FormName { get; }

    /// <summary>Gets the title of the form being submitted.</summary>
    string FormTitle { get; }

    /// <summary>Gets the name of the entry type.</summary>
    /// <value>The name of the entry type.</value>
    string EntryTypeName { get; }

    /// <summary>Gets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    string ProviderName { get; }
  }
}
