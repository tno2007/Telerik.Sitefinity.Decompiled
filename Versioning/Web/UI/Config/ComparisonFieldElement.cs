// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Web.UI.Config.ComparisonFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Versioning.Web.UI.Contracts;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Versioning.Web.UI.Config
{
  /// <summary>
  /// This class is used to define fields which will be used to compare versions in comparison screen
  /// </summary>
  public class ComparisonFieldElement : 
    FieldDefinitionElement,
    IComparisonFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private bool isHtmlEnchancedField;
    private bool includeInDetails;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ComparisonFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new ComparisonFieldDefinition((ConfigElement) this);

    public bool IsHtmlEnchancedField
    {
      get => this.isHtmlEnchancedField;
      set => this.isHtmlEnchancedField = value;
    }

    public bool IncludeInDetails
    {
      get => this.includeInDetails;
      set => this.includeInDetails = value;
    }
  }
}
