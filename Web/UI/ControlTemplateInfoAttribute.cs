// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlTemplateInfoAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Defines the user friendly presentation of the control type.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
  public sealed class ControlTemplateInfoAttribute : Attribute
  {
    /// <summary>
    ///  Initializes a new instance of the <see cref="!:FriendlyUserNameAttribute" /> class.
    /// </summary>
    public ControlTemplateInfoAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:FriendlyUserNameAttribute" /> class.
    /// </summary>
    /// <param name="displayName">The display name.</param>
    public ControlTemplateInfoAttribute(
      string resourceClassId,
      string controlDisplayName,
      string areaName)
    {
      this.ControlDisplayName = controlDisplayName;
      this.AreaName = areaName;
      this.ResourceClassId = resourceClassId;
    }

    public string ControlDisplayName { get; set; }

    /// <summary>Gets or sets the area name for the</summary>
    public string AreaName { get; set; }

    /// <summary>
    /// The type name of the resource class to get localized strings
    /// </summary>
    public string ResourceClassId { get; set; }
  }
}
