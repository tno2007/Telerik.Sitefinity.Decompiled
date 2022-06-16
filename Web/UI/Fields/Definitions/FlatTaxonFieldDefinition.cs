// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.FlatTaxonFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// 
  /// </summary>
  public class FlatTaxonFieldDefinition : 
    TaxonFieldDefinition,
    IFlatTaxonFieldDefinition,
    ITaxonFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private IExpandableControlDefinition expandableDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.FlatTaxonFieldDefinition" /> class.
    /// </summary>
    public FlatTaxonFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.FlatTaxonFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public FlatTaxonFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (FlatTaxonField);

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition
    {
      get
      {
        if (this.expandableDefinition == null)
        {
          this.expandableDefinition = (IExpandableControlDefinition) new ExpandableControlDefinition();
          this.expandableDefinition.ControlDefinitionName = this.ControlDefinitionName;
          this.expandableDefinition.ViewName = this.ViewName;
          this.expandableDefinition.SectionName = this.SectionName;
          this.expandableDefinition.FieldName = this.FieldName;
        }
        return this.expandableDefinition;
      }
      set => this.expandableDefinition = value;
    }
  }
}
