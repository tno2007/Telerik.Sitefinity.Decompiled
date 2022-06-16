// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.AttributeConfigurator.Attributes.ViewSelectorAttribute
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.AttributeConfigurator.Attributes
{
  /// <summary>
  /// Attribute for marking properties that should be visualized with view selector in edit mode.
  /// </summary>
  internal class ViewSelectorAttribute : Attribute
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.AttributeConfigurator.Attributes.ViewSelectorAttribute" /> class.
    /// </summary>
    /// <param name="viewNamePattern">The view name pattern</param>
    public ViewSelectorAttribute(string viewNamePattern) => this.ViewNamePattern = viewNamePattern;

    /// <summary>Gets or sets the module name</summary>
    public string ViewNamePattern { get; set; }
  }
}
