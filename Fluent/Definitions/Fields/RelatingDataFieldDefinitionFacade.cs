// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.Fields.RelatingDataFieldDefinitionFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions.Fields
{
  /// <summary>
  /// Fluent API facade wrapping <c>RelatingDataFieldDefinitionElement</c>
  /// </summary>
  /// <typeparam name="TParentFacade">The type of the parent facade.</typeparam>
  public class RelatingDataFieldDefinitionFacade<TParentFacade> : 
    FieldControlDefinitionFacade<RelatingDataFieldDefinitionElement, RelatingDataFieldDefinitionFacade<TParentFacade>, TParentFacade>
    where TParentFacade : class
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.Fields.RelatingDataFieldDefinitionFacade`1" /> class.
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
    public RelatingDataFieldDefinitionFacade(
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
      this.Field.WrapperTag = HtmlTextWriterTag.Li;
      this.Field.FieldType = typeof (RelatingDataField);
    }

    /// <summary>Creates the config.</summary>
    /// <param name="parentElement">The parent element.</param>
    /// <returns>New instance of the parent config element.</returns>
    protected override RelatingDataFieldDefinitionElement CreateConfig(
      ConfigElementDictionary<string, FieldDefinitionElement> parentElement)
    {
      return new RelatingDataFieldDefinitionElement((ConfigElement) parentElement);
    }
  }
}
