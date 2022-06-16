// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Definitions.IViewContainerDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Web.UI.Definitions
{
  internal interface IViewContainerDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    string ControlDefinitionName { get; set; }

    /// <summary>Gets the views.</summary>
    /// <value>The views.</value>
    [TypeConverter(typeof (GenericCollectionConverter))]
    ViewDefinitionDictionary Views { get; }

    /// <summary>Tries the get view.</summary>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="view">The view.</param>
    /// <returns></returns>
    bool TryGetView(string viewName, out IViewDefinition view);

    /// <summary>
    /// Determines whether the views collection contains view with the specified name.
    /// </summary>
    /// <param name="viewName">Name of the view.</param>
    /// <returns>
    /// 	<c>true</c> if the view exists; otherwise, <c>false</c>.
    /// </returns>
    bool ContainsView(string viewName);
  }
}
