// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IHasContainerType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// The widgets that implement this interface will receive information about its container type.
  /// </summary>
  /// <example> Used to maintain inforamtion whether the widget is placed on sitefinity page or template.</example>
  /// <remarks>
  /// This interface could be used on WebForms controls and MVC controller.
  /// </remarks>
  public interface IHasContainerType
  {
    /// <summary>
    /// Gets or sets the type of container holding this instance (ex. PageControl or TemplateControl).
    /// </summary>
    Type ContainerType { get; set; }
  }
}
