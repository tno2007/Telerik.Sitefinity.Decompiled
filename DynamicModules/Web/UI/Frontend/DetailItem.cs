// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DetailItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.DynamicModules.Web.UI.Frontend
{
  /// <summary>
  /// This class represents a boundable item of the <see cref="T:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DynamicContentViewDetail" /> data boundable control.
  /// </summary>
  public class DetailItem : Control, IDataItemContainer, INamingContainer
  {
    private readonly object dataItem;
    private readonly int dataItemIndex;
    private readonly int displayIndex;

    /// <summary>
    /// Creates a new instace of <see cref="T:Telerik.Sitefinity.DynamicModules.Web.UI.Frontend.DetailItem" />.
    /// </summary>
    /// <param name="dataItem">
    /// The instance of data item that control should be bound to.
    /// </param>
    /// <param name="index">
    /// The index of the data item in the data source.
    /// </param>
    public DetailItem(object dataItem, int index)
    {
      this.dataItem = dataItem;
      this.dataItemIndex = this.displayIndex = index;
    }

    /// <summary>
    /// When implemented, gets an object that is used in simplified data-binding
    /// operations.
    /// </summary>
    /// <returns>
    /// An object that represents the value to use when data-binding operations
    /// are performed.
    /// </returns>
    public object DataItem => this.dataItem;

    /// <summary>
    /// When implemented, gets the index of the data item bound to a control.
    /// </summary>
    /// <returns>
    /// An Integer representing the index of the data item in the data source.
    /// </returns>
    public int DataItemIndex => 0;

    /// <summary>
    /// When implemented, gets the position of the data item as displayed in
    /// a control.
    /// </summary>
    /// <returns>
    /// An Integer representing the position of the data item as displayed in
    /// a control.
    /// </returns>
    public int DisplayIndex => 0;
  }
}
