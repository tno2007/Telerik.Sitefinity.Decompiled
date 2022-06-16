// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IEmbedControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Defines a contract for EmbedControl control definition and config element
  /// </summary>
  public interface IEmbedControlDefinition : IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets the definition for the choice field representig the diffrenet media sizes
    /// </summary>
    /// <value>The sizes choice field definition.</value>
    IChoiceFieldDefinition SizesChoiceFieldDefinition { get; }

    /// <summary>
    /// String template used to generate the embed html
    /// </summary>
    /// <example>
    /// <img width="{0}" height="{1}" src="{2}" alt="{3}" />
    /// </example>
    string EmbedStringTemplate { get; }

    /// <summary>The title of the customize button</summary>
    string CustomizeButtonTitle { get; }

    /// <summary>
    /// If set will hide the text box with the code for embedding in a page
    /// </summary>
    bool? HideEmbedTextBox { get; }
  }
}
