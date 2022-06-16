// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.IDesignerResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// Classes that implement this interface should contain logic for customizing control designer behaviors.
  /// </summary>
  public interface IDesignerResolver
  {
    /// <summary>
    /// Gets the control designer URL. If null then the default property editor URL should be used.
    /// </summary>
    /// <param name="controlType">Type of the control.</param>
    /// <returns></returns>
    string GetUrl(Type controlType);

    /// <summary>
    /// Checks if there are separate custom desiger views for the particular control type.
    /// </summary>
    /// <param name="controlType">The type of the control.</param>
    /// <returns></returns>
    bool HasCustomDesigners(string controlType);

    /// <summary>Gets all view names that match a pattern.</summary>
    /// <param name="controlType">The control type.</param>
    /// <param name="viewNamePattern">The view name pattern</param>
    /// <returns></returns>
    IEnumerable<string> GetViewNames(string controlType, string viewNamePattern);
  }
}
