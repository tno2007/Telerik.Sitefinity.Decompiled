// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// This interface mandates the members that need to be implemented by a type that
  /// acts as a single choice of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceFieldDefinition" />.
  /// </summary>
  public interface IChoiceDefinition : IDefinition
  {
    /// <summary>Gets or sets the text of the choice.</summary>
    string Text { get; set; }

    /// <summary>Gets or sets the value of the choice.</summary>
    string Value { get; set; }

    /// <summary>Gets or sets the description of the choice.</summary>
    string Description { get; set; }

    /// <summary>
    /// Gets a value which indicates whether the choice is enabled. If choice is enabled true; otherwise false.
    /// </summary>
    bool Enabled { get; set; }

    /// <summary>Indicates if the choice element is selected/checked</summary>
    bool Selected { get; set; }

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the element.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as Text will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    string ResourceClassId { get; set; }
  }
}
