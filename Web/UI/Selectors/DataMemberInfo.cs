// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.DataMemberInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Web.UI.Selectors;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Class representing a data member of the item selector</summary>
  [ParseChildren(true, "ColumnTemplate")]
  public class DataMemberInfo : Control, IDataMemberInfo
  {
    /// <summary>Gets or sets the data member name</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets the header label for the data member</summary>
    public string HeaderText { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether this data member is a search field
    /// </summary>
    public bool IsSearchField { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether this data member is a search field of type <see cref="!:Lstring" />
    /// </summary>
    public bool IsExtendedSearchField { get; set; }

    [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public string ColumnTemplate { get; set; }
  }
}
