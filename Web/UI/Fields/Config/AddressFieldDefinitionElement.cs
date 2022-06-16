// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.AddressFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  public class AddressFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IAddressFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public AddressFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the work mode of the address field control
    /// </summary>
    [ConfigurationProperty("workMode")]
    public AddressWorkMode WorkMode
    {
      get => (AddressWorkMode) this["workMode"];
      set => this["workMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether field control values are required
    /// </summary>
    [ConfigurationProperty("isRequired")]
    public bool IsRequired
    {
      get => (bool) this["isRequired"];
      set => this["isRequired"] = (object) value;
    }

    /// <summary>Gets the definition.</summary>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new AddressFieldDefinition((ConfigElement) this);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal new struct FieldProps
    {
      public const string WorkMode = "workMode";
      public const string IsRequired = "isRequired";
    }
  }
}
