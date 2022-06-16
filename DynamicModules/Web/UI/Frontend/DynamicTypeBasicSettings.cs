// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicTypeBasicSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  /// <summary>
  /// This class contains the basic settings for dynamic type.
  /// </summary>
  public class DynamicTypeBasicSettings
  {
    /// <summary>
    /// Specifies whether the master view allows paging of the list of items.
    /// </summary>
    /// <value>A Boolean value. True if paging is allowed</value>
    public bool AllowPaging { get; set; }

    /// <summary>
    /// When paging is enabled through the AllowPaging property, how many items per page are displayed.
    /// </summary>
    /// <value>The number of items per page.</value>
    public int ItemsPerPage { get; set; }

    /// <summary>
    /// Gets or sets the sort expression for the list of items.
    /// </summary>
    /// <value>The sort expression.</value>
    public string SortExpression { get; set; }

    /// <summary>Gets or sets the id of the master view template.</summary>
    /// <value>The sort expression.</value>
    public Guid MasterTemplateId { get; set; }

    /// <summary>Gets or sets the id of the detail view template.</summary>
    /// <value>The template id.</value>
    public Guid DetailTemplateId { get; set; }
  }
}
