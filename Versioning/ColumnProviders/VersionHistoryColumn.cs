// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.ColumnProviders.VersionHistoryColumn
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Versioning.ColumnProviders
{
  /// <summary>Version history column model</summary>
  public class VersionHistoryColumn : IOrderedItem
  {
    /// <summary>Gets or sets the column title</summary>
    public string Title { get; set; }

    /// <summary>Gets or sets the column field</summary>
    public string Field { get; set; }

    /// <summary>Gets or sets the column template</summary>
    public string Template { get; set; }

    /// <summary>Gets or sets the column cell css class</summary>
    public string CssClass { get; set; }

    /// <summary>Gets or sets the column header css class</summary>
    public string HeaderCssClass { get; set; }

    /// <summary>Gets or sets the column ordinal</summary>
    public float Ordinal { get; set; }
  }
}
