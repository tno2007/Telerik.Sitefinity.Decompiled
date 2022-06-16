// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.ComparisonFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Versioning.Web.UI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Versioning.Web.UI
{
  public class ComparisonFieldDefinition : 
    FieldDefinition,
    IComparisonFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private bool isHtmlEnchancedField;
    private bool includeInDetails;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Versioning.Web.UI.ComparisonFieldDefinition" /> class.
    /// </summary>
    public ComparisonFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Versioning.Web.UI.ComparisonFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ComparisonFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    public bool IsHtmlEnchancedField
    {
      get => this.ResolveProperty<bool>(nameof (IsHtmlEnchancedField), this.isHtmlEnchancedField);
      set => this.isHtmlEnchancedField = value;
    }

    public bool IncludeInDetails
    {
      get => this.ResolveProperty<bool>(nameof (IncludeInDetails), this.includeInDetails);
      set => this.includeInDetails = value;
    }
  }
}
