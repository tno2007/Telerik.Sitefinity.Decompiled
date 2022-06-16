// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditableInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit
{
  /// <summary>
  /// Contains information needed to configure instances of classes implementing <see cref="T:Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.IBrowseAndEditable" />.
  /// </summary>
  public class BrowseAndEditableInfo
  {
    /// <summary>
    /// Gets or sets the id of the <see cref="!:ControlData" /> object related to the current control.
    /// </summary>
    public Guid ControlDataId { get; set; }

    /// <summary>
    /// Gets or sets the id of the <see cref="!:PageData" /> object where the current control is declared.
    /// </summary>
    public Guid PageId { get; set; }

    /// <summary>Gets or sets the type of the current control.</summary>
    public string ControlType { get; set; }
  }
}
