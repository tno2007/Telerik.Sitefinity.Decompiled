// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.IBulkEditFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI
{
  /// <summary>
  /// Defines the contract for BulkEdit definition classes and configuration elements
  /// </summary>
  public interface IBulkEditFieldDefinition : 
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>Gets or sets the name of the field control template.</summary>
    /// <value>The name of the template.</value>
    string TemplateName { get; set; }

    /// <summary>Gets or sets the path of the field control template.</summary>
    /// <value>The path of the template.</value>
    string TemplatePath { get; set; }

    /// <summary>Gets or sets the web service URL.</summary>
    /// <value>The web service URL.</value>
    string WebServiceUrl { get; set; }

    /// <summary>Gets the type of the content.</summary>
    /// <value>The type of the content.</value>
    Type ContentType { get; set; }

    /// <summary>Gets or sets the type of the parent.</summary>
    /// <value>The type of the parent.</value>
    Type ParentType { get; set; }
  }
}
