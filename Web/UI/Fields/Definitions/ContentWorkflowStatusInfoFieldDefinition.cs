// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.ContentWorkflowStatusInfoFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// Contains all properties needed to construct an instance of the ContentWorkflowStatusInfoField control
  /// </summary>
  public class ContentWorkflowStatusInfoFieldDefinition : 
    FieldControlDefinition,
    IContentWorkflowStatusInfoFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ContentWorkflowStatusInfoFieldDefinition" /> class.
    /// </summary>
    public ContentWorkflowStatusInfoFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ContentWorkflowStatusInfoFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentWorkflowStatusInfoFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (ContentWorkflowStatusInfoField);
  }
}
