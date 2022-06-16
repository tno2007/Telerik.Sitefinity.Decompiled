// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Selectors.IDataMemberInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Selectors
{
  /// <summary>Extraction of DataMemberInfo's properties</summary>
  public interface IDataMemberInfo
  {
    /// <summary>Template of the data member</summary>
    string ColumnTemplate { get; set; }

    /// <summary>Gets or sets the header label for the data member</summary>
    string HeaderText { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether this data member is a search field
    /// </summary>
    bool IsSearchField { get; set; }

    /// <summary>
    /// Gets or sets value indicating whether this data member is a search field of type <see cref="!:Lstring" />
    /// </summary>
    bool IsExtendedSearchField { get; set; }

    /// <summary>Gets or sets the data member name</summary>
    string Name { get; set; }
  }
}
