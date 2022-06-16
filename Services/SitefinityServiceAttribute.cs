// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SitefinityServiceAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// This attribute is used to mark assemblies that may contain <see cref="T:Telerik.Sitefinity.Services.IService" /> types.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
  public class SitefinityServiceAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.SitefinityServiceAttribute" /> class.
    /// </summary>
    /// <param name="name">Name of the service.</param>
    /// <param name="type">Type of the service.</param>
    /// <param name="title">Title of the service.</param>
    /// <param name="description">Description of the service.</param>
    /// <param name="startupType">Startup type of the service.</param>
    public SitefinityServiceAttribute(
      string name,
      Type type,
      string title = null,
      string description = null,
      StartupType startupType = StartupType.Disabled)
    {
      this.Name = name;
      this.Type = type;
      this.Title = title ?? string.Empty;
      this.Description = description ?? string.Empty;
      this.StartupType = startupType;
    }

    /// <summary>Gets the name of the service.</summary>
    public string Name { get; private set; }

    /// <summary>Gets the type of the service.</summary>
    public Type Type { get; private set; }

    /// <summary>
    /// Gets the name that will be displayed for the item on the user interface.
    /// </summary>
    public string Title { get; private set; }

    /// <summary>Gets the description of the service.</summary>
    public string Description { get; private set; }

    /// <summary>
    /// Gets the startup type of the service. The default value is Disabled.
    /// </summary>
    public StartupType StartupType { get; private set; }
  }
}
