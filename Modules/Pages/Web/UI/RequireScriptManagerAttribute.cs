// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.RequireScriptManagerAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// Indicates whether the control requires <see cref="T:System.Web.UI.ScriptManager" /> on the page.
  /// </summary>
  [AttributeUsage(AttributeTargets.Class, Inherited = true)]
  public class RequireScriptManagerAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.RequireScriptManagerAttribute" /> class.
    /// </summary>
    public RequireScriptManagerAttribute() => this.Require = true;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.RequireScriptManagerAttribute" /> class.
    /// </summary>
    /// <param name="require">if set to <c>true</c> [require].</param>
    public RequireScriptManagerAttribute(bool require) => this.Require = require;

    /// <summary>
    /// Gets or sets a value indicating whether <see cref="T:System.Web.UI.ScriptManager" /> is required.
    /// </summary>
    /// <value><c>true</c> if require; otherwise, <c>false</c>.</value>
    public bool Require { get; private set; }
  }
}
