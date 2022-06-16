// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.ILanguageChoiceFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>
  /// Interface defining common properties for all LanguageListField.
  /// </summary>
  public interface ILanguageChoiceFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    Telerik.Sitefinity.Localization.Web.UI.LanguageSource? LanguageSource { get; set; }

    /// <summary>
    /// Gets or sets whether to hide the field if only a single language is found.
    /// </summary>
    /// <value>True if the field must be hidden when only a single language is found.</value>
    bool? HideIfSingleLanguage { get; set; }

    /// <summary>Gets or sets the languages to show.</summary>
    /// <value>The languages to show.</value>
    LanguagesSelection? LanguagesToShow { get; set; }
  }
}
