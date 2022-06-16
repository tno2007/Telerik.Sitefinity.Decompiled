// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IRelatedDataFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  public interface IRelatedDataFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Gets or sets the label of the front end widget element.
    /// </summary>
    string FrontendWidgetLabel { get; set; }

    /// <summary>
    /// Gets or sets the type of the front end control that represents the field.
    /// </summary>
    Type FrontendWidgetType { get; set; }

    /// <summary>
    /// Gets or sets the default type of the front end control that represents the field.
    /// </summary>
    Type DefaultFrontendWidgetType { get; }

    /// <summary>
    /// Gets or sets the virtual path of the front end user control that represents the field.
    /// </summary>
    string FrontendWidgetVirtualPath { get; set; }

    /// <summary>
    /// Gets or sets the type of the content that can be related.
    /// </summary>
    string RelatedDataType { get; set; }

    /// <summary>
    /// Gets or sets the provider of the content that can be related.
    /// </summary>
    string RelatedDataProvider { get; set; }

    /// <summary>Allows multiple items selection.</summary>
    bool AllowMultipleSelection { get; set; }
  }
}
