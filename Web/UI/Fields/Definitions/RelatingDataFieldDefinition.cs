// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.RelatingDataFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  ///  A class that provides all information needed to construct a relating data field control.
  /// </summary>
  public class RelatingDataFieldDefinition : FieldControlDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ContentWorkflowStatusInfoFieldDefinition" /> class.
    /// </summary>
    public RelatingDataFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ContentWorkflowStatusInfoFieldDefinition" /> class.
    /// </summary>
    public RelatingDataFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    public override Type DefaultFieldType => typeof (RelatingDataField);
  }
}
