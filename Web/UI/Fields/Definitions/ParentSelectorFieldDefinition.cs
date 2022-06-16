// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.ParentSelectorFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A class that provides all information that is needed to construct a ParentSelectorField control.
  /// </summary>
  public class ParentSelectorFieldDefinition : 
    FieldControlDefinition,
    IParentSelectorFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private bool allowSearching;
    private string itemsType;
    private string webServiceUrl;
    private string mainFieldName;
    private string dataKeyNames;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ParentSelectorFieldDefinition" /> class.
    /// </summary>
    public ParentSelectorFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.ParentSelectorFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ParentSelectorFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <inheritdoc />
    public string ItemsType
    {
      get => this.ResolveProperty<string>(nameof (ItemsType), this.itemsType);
      set => this.itemsType = value;
    }

    /// <inheritdoc />
    public string WebServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceUrl), this.webServiceUrl);
      set => this.webServiceUrl = value;
    }

    /// <inheritdoc />
    public string MainFieldName
    {
      get => this.ResolveProperty<string>(nameof (MainFieldName), this.mainFieldName);
      set => this.mainFieldName = value;
    }

    /// <inheritdoc />
    public string DataKeyNames
    {
      get => this.ResolveProperty<string>(nameof (DataKeyNames), this.dataKeyNames);
      set => this.dataKeyNames = value;
    }

    /// <inheritdoc />
    public bool AllowSearching
    {
      get => this.ResolveProperty<bool>(nameof (AllowSearching), this.allowSearching);
      set => this.allowSearching = value;
    }
  }
}
