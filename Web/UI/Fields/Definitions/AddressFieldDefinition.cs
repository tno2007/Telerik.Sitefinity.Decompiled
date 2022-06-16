// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.AddressFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  public class AddressFieldDefinition : 
    FieldControlDefinition,
    IAddressFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private AddressWorkMode workMode;
    private bool isRequired;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.AssetsFieldDefinition" /> class.
    /// </summary>
    public AddressFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.AssetsFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public AddressFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets or sets the work mode of the address field control
    /// </summary>
    public AddressWorkMode WorkMode
    {
      get => this.ResolveProperty<AddressWorkMode>(nameof (WorkMode), this.workMode);
      set => this.workMode = value;
    }

    /// <summary>
    /// Gets or sets whether field control values are required
    /// </summary>
    public bool IsRequired
    {
      get => this.ResolveProperty<bool>(nameof (IsRequired), this.isRequired);
      set => this.isRequired = value;
    }
  }
}
