// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Definitions.DetailDefinitionFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Fluent.Definitions
{
  /// <summary>
  /// Fluent API facade that defines a definition for content view detail element.
  /// </summary>
  public class DetailDefinitionFacade : 
    BaseDetailDefinitionFacade<ContentViewDetailElement, DetailDefinitionFacade>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Definitions.DetailDefinitionFacade" /> class.
    /// </summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="definitionName">Name of the definition.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    /// <param name="parentFacade">The parent facade.</param>
    public DetailDefinitionFacade(
      string moduleName,
      string definitionName,
      Type contentType,
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName,
      ContentViewControlDefinitionFacade parentFacade)
      : base(moduleName, definitionName, contentType, parentElement, viewName, parentFacade)
    {
    }

    /// <summary>Defines the content view.</summary>
    /// <param name="parentElement">The parent element.</param>
    /// <param name="viewName">Name of the view.</param>
    protected override void DefineContentView(
      ConfigElementDictionary<string, ContentViewDefinitionElement> parentElement,
      string viewName)
    {
      ContentViewDetailElement viewDetailElement = new ContentViewDetailElement((ConfigElement) parentElement);
      viewDetailElement.ViewName = viewName;
      viewDetailElement.DisplayMode = FieldDisplayMode.Read;
      viewDetailElement.ShowSections = new bool?(false);
      this.ContentView = viewDetailElement;
    }
  }
}
