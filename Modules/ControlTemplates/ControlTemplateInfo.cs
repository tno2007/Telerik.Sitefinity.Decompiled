// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ControlTemplates.ControlTemplateInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.ControlTemplates
{
  /// <summary>
  /// A class that provides all necessary properties to construct a control template.
  /// </summary>
  public class ControlTemplateInfo : IControlTemplateInfo
  {
    /// <summary>Gets or sets the type of the control.</summary>
    /// <value>The type of the control.</value>
    public Type ControlType { get; set; }

    /// <summary>Gets or sets the friendly user name.</summary>
    /// <value>The friendly user name.</value>
    public string FriendlyControlName { get; set; }

    /// <summary>Gets or sets the type of the data item.</summary>
    /// <value>The type of the data item.</value>
    public Type DataItemType { get; set; }

    /// <summary>
    /// Gets or sets the name of the area to which a control template belongs.
    /// </summary>
    /// <value></value>
    public string AreaName { get; set; }

    /// <summary>
    /// Gets or sets the class id of the resource for getting the localized AreaName and FriendlyControlName.
    /// </summary>
    public string ResourceClassId { get; set; }
  }
}
