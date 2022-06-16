// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SecuredControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// A wrapper control containing a control with permissions.
  /// </summary>
  [PersistChildren(false)]
  [ParseChildren(true)]
  public class SecuredControl : Control
  {
    private Control control;
    private List<SecuredObjectInfo> infoCollection;
    private bool? isGranted;

    /// <summary>Gets the secured object.</summary>
    /// <value>The secured object.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public SecuredObjectInfo SecuredObject => this.infoCollection != null && this.infoCollection.Count > 0 ? this.infoCollection[0] : (SecuredObjectInfo) null;

    /// <summary>Gets a collection of secured objects.</summary>
    /// <value>The collection.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<SecuredObjectInfo> Info
    {
      get
      {
        if (this.infoCollection == null)
          this.infoCollection = new List<SecuredObjectInfo>(1);
        return this.infoCollection;
      }
    }

    /// <summary>
    /// Gets or sets the control containing the wrapped control.
    /// </summary>
    /// <value>The control.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public Control Control
    {
      get => this.control;
      set
      {
        this.control = value;
        this.Controls.Clear();
        this.Controls.Add(this.control);
      }
    }

    /// <summary>
    /// Gets or sets a value that indicates whether a server control is rendered as UI on the page.
    /// </summary>
    /// <value></value>
    /// <returns>true if the control is visible on the page; otherwise false.</returns>
    public override bool Visible
    {
      get => this.SecuredObject == null ? base.Visible : this.IsGranted;
      set => base.Visible = value;
    }

    /// <summary>
    /// Gets a value indicating whether this instance is granted a permission for view.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is granted a permission for view; otherwise, <c>false</c>.
    /// </value>
    protected bool IsGranted
    {
      get
      {
        if (!this.isGranted.HasValue)
        {
          if (this.SecuredObject == null)
            return true;
          this.isGranted = new bool?(this.SecuredObject.IsGranted(SecurityActionTypes.View));
        }
        return this.isGranted.Value;
      }
    }
  }
}
