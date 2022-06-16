// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.RelatingDataFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes a relating data field.
  /// </summary>
  public class RelatingDataFieldDefinitionElement : FieldControlDefinitionElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    public RelatingDataFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new RelatingDataFieldDefinition((ConfigElement) this);

    /// <summary>Gets the default type of the field.</summary>
    public override Type DefaultFieldType => typeof (RelatingDataField);
  }
}
