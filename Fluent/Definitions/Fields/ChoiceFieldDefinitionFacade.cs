// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.ChoiceFieldDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>
  /// Fluent API that wraps <c>ChoiceFieldElement</c>
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the parent facade</typeparam>
  public class ChoiceFieldDefinitionFacade<TParentFacade> : 
    BaseChoiceFieldDefinitionFacade<ChoiceFieldElement, ChoiceFieldDefinitionFacade<TParentFacade>, TParentFacade>
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.ChoiceFieldDefinitionFacade`1" /> class.
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
    public ChoiceFieldDefinitionFacade(
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
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.ChoiceFieldDefinitionFacade`1" /> class.
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
    public ChoiceFieldDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElement parentElement,
      string viewName,
      string sectionName,
      TParentFacade parentFacade,
      string fieldName,
      string resourceClassId,
      FieldDisplayMode mode)
      : base(moduleName, definitionName, contentType, parentElement, viewName, sectionName, parentFacade, fieldName, resourceClassId, mode)
    {
    }

    /// <summary>Creates a new instance of ChoiceFieldElement</summary>
    /// <param name="parentElement">Parent config elemnt</param>
    /// <returns>New instance of ChoiceFieldElement</returns>
    protected override ChoiceFieldElement CreateConfig(
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement)
    {
      return new ChoiceFieldElement((ConfigElement) parentElement);
    }

    /// <summary>Creates a new instance of ChoiceFieldElement</summary>
    /// <param name="parentElement">The parent element.</param>
    /// <returns>New instance of ChoiceFieldElement</returns>
    protected override ChoiceFieldElement CreateConfigWithSingleParent(
      ConfigElement parentElement)
    {
      return new ChoiceFieldElement(parentElement);
    }

    /// <summary>
    /// Sets the type of the control that represents the field
    /// </summary>
    /// <typeparam name="TFieldType">Type of the field</typeparam>
    /// <returns>Current facade</returns>
    public override ChoiceFieldDefinitionFacade<TParentFacade> SetFieldType<TFieldType>() => base.SetFieldType<TFieldType>();
  }
}
