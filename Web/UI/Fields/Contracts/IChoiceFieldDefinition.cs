// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// An interface that mandates the members that need to be implemented by any control
  /// that wishes to act as a choice field.
  /// </summary>
  public interface IChoiceFieldDefinition : IFieldControlDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceDefinition" /> objects, representing the choices
    /// that the control ought to render.
    /// </summary>
    List<IChoiceDefinition> Choices { get; }

    /// <summary>
    /// Gets or sets a value indicating whether more than one choice can be made. If only one choice
    /// can be made true; otherwise false.
    /// </summary>
    bool MutuallyExclusive { get; set; }

    /// <summary>
    /// Gets or sets the value indicating how should the choices be rendered.
    /// </summary>
    RenderChoicesAs RenderChoiceAs { get; set; }

    /// <summary>
    /// Gets or sets the value indicating which choice(s) is currently selected.
    /// </summary>
    ICollection<int> SelectedChoicesIndex { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the title is hidden.
    /// </summary>
    bool HideTitle { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates that the values returned from the
    /// field control (client side) should always be returned in an array of strings,
    /// regardless if one or more choices have been selected.
    /// </summary>
    bool ReturnValuesAlwaysInArray { get; set; }
  }
}
