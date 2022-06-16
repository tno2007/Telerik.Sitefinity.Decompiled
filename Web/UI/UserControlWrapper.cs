// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.UserControlWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Wrapper class for user controls.</summary>
  [ParseChildren(typeof (Property), ChildrenAsProperties = true, DefaultProperty = "Properties")]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class UserControlWrapper : Control
  {
    private Control userControl;

    /// <inheritdoc />
    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      if (this.Page == null || string.IsNullOrEmpty(this.VirtualPath))
        return;
      this.userControl = this.Page.LoadControl(this.VirtualPath);
      if (this.Properties != null && this.Properties.Count > 0)
        this.SetProperties(this.userControl);
      this.Controls.Add(this.userControl);
    }

    private void SetProperties(Control userControl)
    {
      PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) userControl);
      foreach (Property property in this.Properties)
      {
        PropertyDescriptor propertyDescriptor = properties.Find(property.Name, true);
        if (propertyDescriptor != null && propertyDescriptor.Converter.CanConvertFrom(typeof (string)))
        {
          object obj = propertyDescriptor.Converter.ConvertFromString(property.Value);
          propertyDescriptor.SetValue((object) userControl, obj);
        }
      }
    }

    /// <summary>
    /// Gets or sets the properties that the wrapped <see cref="P:Telerik.Sitefinity.Web.UI.UserControlWrapper.UserControl" /> has.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<Property> Properties { get; set; }

    /// <summary>
    /// Gets or sets the virtual path of the <see cref="P:Telerik.Sitefinity.Web.UI.UserControlWrapper.UserControl" />.
    /// </summary>
    public string VirtualPath { get; set; }

    /// <summary>
    /// Gets a reference to the wrapped <see cref="P:Telerik.Sitefinity.Web.UI.UserControlWrapper.UserControl" />.
    /// </summary>
    public Control UserControl => this.userControl;
  }
}
