// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.IViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  internal interface IViewDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    string ViewName { get; set; }

    /// <summary>Gets or sets the type of the view.</summary>
    /// <value>The type of the view.</value>
    Type ViewType { get; set; }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    string ControlId { get; set; }
  }
}
