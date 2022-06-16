// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Contracts.ILanguagesColumnMarkupGeneratorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Localization.Contracts
{
  /// <summary>
  /// Defines the interface for settings used to configure languages column markup generators.
  /// </summary>
  public interface ILanguagesColumnMarkupGeneratorDefinition : 
    IDynamicMarkupGeneratorDefinition,
    IDefinition
  {
    /// <summary>Gets or sets source for available languages.</summary>
    /// <value>The language source.</value>
    LanguageSource LanguageSource { get; set; }

    /// <summary>
    /// Gets or sets the list of all listed cultures. This is only used if LanguageSource is set to Custom.
    /// </summary>
    /// <value>All available languages.</value>
    IList<string> AvailableLanguages { get; set; }

    /// <summary>Gets or sets the number of items in a group.</summary>
    int ItemsInGroupCount { get; set; }

    /// <summary>Gets or sets the tag of the container element.</summary>
    string ContainerTag { get; set; }

    /// <summary>Gets or sets the tag of the group element.</summary>
    string GroupTag { get; set; }

    /// <summary>Gets or sets the tag of the item element.</summary>
    string ItemTag { get; set; }

    /// <summary>Gets or sets the css class of the container element.</summary>
    string ContainerClass { get; set; }

    /// <summary>Gets or sets the css class of the group element.</summary>
    string GroupClass { get; set; }

    /// <summary>Gets or sets the css class of the item element.</summary>
    string ItemClass { get; set; }
  }
}
