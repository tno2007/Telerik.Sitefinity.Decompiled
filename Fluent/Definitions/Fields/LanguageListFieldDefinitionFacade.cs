// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.LanguageListFieldDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  public class LanguageListFieldDefinitionFacade<TParentFacade> : 
    FieldControlDefinitionFacade<LanguageListFieldElement, LanguageListFieldDefinitionFacade<TParentFacade>, TParentFacade>
    where TParentFacade : class
  {
    public LanguageListFieldDefinitionFacade(
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
    /// Create an instance of the config element. Do not add it to the parent collection.
    /// </summary>
    /// <param name="parentElement">Parent element</param>
    /// <returns>New instance of the parent config element</returns>
    protected override LanguageListFieldElement CreateConfig(
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement)
    {
      return new LanguageListFieldElement((ConfigElement) parentElement);
    }
  }
}
