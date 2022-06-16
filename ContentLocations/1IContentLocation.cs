// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.IContentLocation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Provides content location information in Sitefinity and the ability to check if a particular item matches it.
  /// </summary>
  public interface IContentLocation : IContentLocationBase
  {
    /// <summary>Gets the id.</summary>
    /// <value>The id.</value>
    Guid Id { get; }

    /// <summary>
    /// Gets the ID of the control, where the item is located.
    /// </summary>
    /// <value>The site id.</value>
    Guid ControlId { get; }

    /// <summary>Gets the type of the item.</summary>
    /// <value>The type of the item.</value>
    Type ItemType { get; }

    /// <summary>Gets the type of the item.</summary>
    /// <value>The type of the item.</value>
    string ItemTypeName { get; }

    /// <summary>Gets the item provider.</summary>
    /// <value>The item provider.</value>
    string ItemProvider { get; }

    /// <summary>Gets the site id the page belogs to.</summary>
    /// <value>The site id.</value>
    bool IsMatch(Guid itemId);

    bool IsSingleItem { get; }

    int Priority { get; }

    CultureInfo Culture { get; }

    Type ItemDescendantType { get; }

    string ItemDescendantTypeName { get; }
  }
}
