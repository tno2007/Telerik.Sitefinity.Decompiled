// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.QueryBuilder.VisitEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Data.QueryBuilder
{
  /// <summary>Query translator visit event arguments</summary>
  public class VisitEventArgs
  {
    private StringBuilder stringBuilder;
    private QueryItem item;
    private QueryData data;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.QueryBuilder.VisitEventArgs" /> class.
    /// </summary>
    public VisitEventArgs(StringBuilder sb, QueryItem item, QueryData data)
    {
      this.stringBuilder = sb;
      this.item = item;
      this.data = data;
      this.IsHandled = false;
    }

    /// <summary>Gets the query builder.</summary>
    /// <value>The builder.</value>
    public StringBuilder Builder => this.stringBuilder;

    /// <summary>Gets the query item.</summary>
    /// <value>The query item.</value>
    public QueryItem QueryItem => this.item;

    /// <summary>Gets the query data.</summary>
    /// <value>The query data.</value>
    public QueryData QueryData => this.data;

    /// <summary>Indicate if this item visit is handled.</summary>
    /// <value>
    /// 	<c>true</c> if this instance is handled; otherwise, <c>false</c>.
    /// </value>
    public bool IsHandled { get; set; }
  }
}
