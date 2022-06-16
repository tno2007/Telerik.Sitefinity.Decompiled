// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.ControlDesign
{
  /// <summary>
  /// This attribute is used to instruct Sitefinity page editor that the control designer has been implemented for the control
  /// and to specify the type of the control that serves as the actual control designer.
  /// </summary>
  /// <remarks>
  /// Control designers are controls which provide simple and straightforward user interface for setting the properties
  /// of controls; as opposed to more advanced and complicated property grid.
  /// </remarks>
  public class ControlDesignerAttribute : Attribute
  {
    private string controlDesignerTypeName;
    private Type controlDesignerType;

    /// <summary>
    /// Creates a new instance of the control designer attribute.
    /// </summary>
    public ControlDesignerAttribute()
      : this((Type) null, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerAttribute" /> class with the specified
    /// type of the control designer.
    /// </summary>
    /// <param name="controlDesignerType">Type of the control designer.</param>
    public ControlDesignerAttribute(Type controlDesignerType)
      : this(controlDesignerType, string.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerAttribute" /> class with the specified
    /// type name of the control designer.
    /// </summary>
    /// <param name="controlDesignerTypeName">Name of the control designer type.</param>
    public ControlDesignerAttribute(string controlDesignerTypeName)
      : this((Type) null, controlDesignerTypeName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerAttribute" /> class.
    /// </summary>
    /// <param name="ctlDesignerType">Type of the control designer.</param>
    /// <param name="ctlDesignerTypeName">Name of the control designer type.</param>
    private ControlDesignerAttribute(Type ctlDesignerType, string ctlDesignerTypeName)
    {
      this.controlDesignerType = ctlDesignerType;
      this.controlDesignerTypeName = ctlDesignerTypeName;
    }

    /// <summary>Gets the name of the control designer type.</summary>
    /// <value>The name of the control designer type.</value>
    public string ControlDesignerTypeName => this.controlDesignerTypeName;

    /// <summary>Gets the type of the control designer.</summary>
    /// <value>The type of the control designer.</value>
    public Type ControlDesignerType => this.controlDesignerType;
  }
}
