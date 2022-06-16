// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorErrorData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  [DataContract]
  public class ZoneEditorErrorData
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorErrorData" /> class.
    /// </summary>
    /// <param name="lockedBy">the id of the user that has locked the content</param>
    public ZoneEditorErrorData(ItemState itemState, Guid lockedBy)
      : this(itemState, lockedBy, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.ZoneEditorErrorData" /> class.
    /// </summary>
    /// <param name="lockedBy">the id of the user that has locked the content</param>
    /// <param name="operation">The operation/service method that was invoked</param>
    public ZoneEditorErrorData(ItemState itemState, Guid lockedBy, string operation)
    {
      this.ItemState = itemState;
      this.LockedBy = lockedBy;
      this.Operation = operation;
    }

    /// <summary>
    /// Gets or sets the id of the user that has locked the content
    /// </summary>
    [DataMember]
    public Guid LockedBy { get; set; }

    /// <summary>Gets or sets the state of the item.</summary>
    /// <value>The state of the item.</value>
    [DataMember]
    public ItemState ItemState { get; set; }

    /// <summary>Gets or sets the operation/method that was invoked</summary>
    /// <value>The operation.</value>
    [DataMember]
    public string Operation { get; set; }
  }
}
