// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ResponsiveLayoutFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  public class ResponsiveLayoutFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IResponsiveLayoutFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:MQDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    public ResponsiveLayoutFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new ResponsiveLayoutFieldDefinition((ConfigElement) this);

    public override Type DefaultFieldType => typeof (ResponsiveLayoutField);

    /// <summary>Gets or sets the item id.</summary>
    [ConfigurationProperty("ItemId")]
    public Guid ItemId
    {
      get => (Guid) this[nameof (ItemId)];
      set => this[nameof (ItemId)] = (object) value;
    }

    /// <summary>Gets or sets the item type.</summary>
    [ConfigurationProperty("ItemType")]
    public string ItemType
    {
      get => (string) this[nameof (ItemType)];
      set => this[nameof (ItemType)] = (object) value;
    }
  }
}
