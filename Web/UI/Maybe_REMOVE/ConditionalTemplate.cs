// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Maybe.ConditionalTemplate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Maybe
{
  /// <summary>
  /// Template that is rendered only if a condition is rendered.
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.UI.Maybe.ConditionalTemplateHolder" />
  public class ConditionalTemplate : CompositeControl, IDataItemContainer, INamingContainer
  {
    /// <summary>
    /// Condition to apply to the left (and optionally) right operand
    /// </summary>
    public RenderConditions Condition { get; set; }

    /// <summary>Right operand (property name)</summary>
    public string LeftOperand { get; set; }

    /// <summary>Template to render if the condition is satisfied</summary>
    [PersistenceMode(PersistenceMode.InnerDefaultProperty)]
    public ITemplate Template { get; set; }

    /// <summary>
    /// When implemented, gets an object that is used in simplified data-binding operations.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// An object that represents the value to use when data-binding operations are performed.
    /// </returns>
    public object DataItem { get; set; }

    /// <summary>
    /// When implemented, gets the index of the data item bound to a control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// An Integer representing the index of the data item in the data source.
    /// </returns>
    public int DataItemIndex { get; set; }

    /// <summary>
    /// When implemented, gets the position of the data item as displayed in a control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// An Integer representing the position of the data item as displayed in a control.
    /// </returns>
    public int DisplayIndex { get; set; }
  }
}
