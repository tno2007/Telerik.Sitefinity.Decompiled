// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.LanguageChoiceFieldDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>Fluent API wrapping LanguageChoiceFieldElement</summary>
  /// <typeparam name="TParentFacade">Type of the parent facade</typeparam>
  public class LanguageChoiceFieldDefinitionFacade<TParentFacade> : 
    BaseChoiceFieldDefinitionFacade<LanguageChoiceFieldElement, LanguageChoiceFieldDefinitionFacade<TParentFacade>, TParentFacade>
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.LanguageChoiceFieldDefinitionFacade`1" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="sectionName">Name of the section.</param>
    /// <param name="parentFacade">The parent facade.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="resourceClassId">The resource class id.</param>
    /// <param name="mode">The mode.</param>
    public LanguageChoiceFieldDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement,
      string viewName,
      string sectionName,
      TParentFacade parentFacade,
      string fieldName,
      string resourceClassId,
      FieldDisplayMode mode)
      : base(moduleName, definitionName, contentType, parentElement, viewName, sectionName, parentFacade, fieldName, resourceClassId, mode)
    {
      this.Field.FieldType = typeof (LanguageChoiceField);
      this.Field.Title = "Language";
      this.Field.ID = "languageChoiceField";
      this.Field.RenderChoiceAs = RenderChoicesAs.DropDown;
      this.Field.MutuallyExclusive = true;
    }

    /// <summary>Creates a new instance of LanguageChoiceFieldElement</summary>
    /// <param name="parentElement">Parent config elemnt</param>
    /// <returns>New instance of LanguageChoiceFieldElement</returns>
    protected override LanguageChoiceFieldElement CreateConfig(
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement)
    {
      return new LanguageChoiceFieldElement((ConfigElement) parentElement);
    }

    /// <summary>Set language data source</summary>
    /// <param name="source">Language data source</param>
    /// <returns>Current facade</returns>
    public LanguageChoiceFieldDefinitionFacade<TParentFacade> SetLanguageSource(
      LanguageSource source)
    {
      this.Field.LanguageSource = new LanguageSource?(source);
      return this;
    }

    /// <summary>Set language selection (avaiable/unavailable)</summary>
    /// <param name="selection">Language selection</param>
    /// <returns>Current facade</returns>
    public LanguageChoiceFieldDefinitionFacade<TParentFacade> SetLanguagesToShow(
      LanguagesSelection selection)
    {
      this.Field.LanguagesToShow = new LanguagesSelection?(selection);
      return this;
    }

    /// <summary>Hide the field if only a single language is found</summary>
    /// <returns>Current facade</returns>
    public LanguageChoiceFieldDefinitionFacade<TParentFacade> HideIfSingleLanguage()
    {
      this.Field.HideIfSingleLanguage = new bool?(true);
      return this;
    }

    /// <summary>
    /// Show the field even if only a single language is found
    /// </summary>
    /// <returns>Current facade</returns>
    public LanguageChoiceFieldDefinitionFacade<TParentFacade> ShowIfSingleLanguage()
    {
      this.Field.HideIfSingleLanguage = new bool?(false);
      return this;
    }
  }
}
