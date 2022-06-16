// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.ResponsiveLayoutFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  public class ResponsiveLayoutFieldDefinition : 
    FieldControlDefinition,
    IResponsiveLayoutFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private Guid itemId;
    private string itemType;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:MQDefinition" /> class.
    /// </summary>
    public ResponsiveLayoutFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:MQDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ResponsiveLayoutFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the item type.</summary>
    public string ItemType
    {
      get => this.ResolveProperty<string>(nameof (ItemType), this.itemType);
      set => this.itemType = value;
    }

    /// <summary>Gets or sets the item id.</summary>
    public Guid ItemId
    {
      get => this.ResolveProperty<Guid>(nameof (ItemId), this.itemId);
      set => this.itemId = value;
    }
  }
}
