// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Configuration.VersionNoteControlDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Versioning.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Versioning.Configuration
{
  public class VersionNoteControlDefinitionElement : 
    FieldControlDefinitionElement,
    IVersionNoteDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Versioning.Configuration.VersionNoteControlDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public VersionNoteControlDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new VersionNoteDefinition();

    public override Type DefaultFieldType => typeof (VersionNoteControl);
  }
}
