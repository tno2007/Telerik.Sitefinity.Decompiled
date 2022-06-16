// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.HierarchicalTaxonFieldDefinition
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
  /// A definition class containing all information needed to construct an instance of a
  /// <see cref="T:Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField" />.
  /// </summary>
  public class HierarchicalTaxonFieldDefinition : 
    TaxonFieldDefinition,
    IHierarchicalTaxonFieldDefinition,
    ITaxonFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private IExpandableControlDefinition expandableDefinition;
    private bool? showCreateNewTaxonButton;
    private bool? showDoneSelectingButton;
    private bool? allowRootSelection;
    private Guid rootTaxonID;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public HierarchicalTaxonFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public HierarchicalTaxonFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (HierarchicalTaxonField);

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

    /// <summary>
    /// Gets or sets a value indicating whether to allow root selection.
    /// </summary>
    /// <value><c>true</c> if to allow root selection; otherwise, <c>false</c>.</value>
    public bool? AllowRootSelection
    {
      get => this.ResolveProperty<bool?>(nameof (AllowRootSelection), this.allowRootSelection, new bool?(false));
      set => this.allowRootSelection = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the done-selecting button.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show the done-selecting button; otherwise, <c>false</c>.
    /// </value>
    public bool? ShowDoneSelectingButton
    {
      get => this.ResolveProperty<bool?>(nameof (ShowDoneSelectingButton), this.showDoneSelectingButton, new bool?(true));
      set => this.showDoneSelectingButton = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the create-new-taxon button.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show the create-new-taxon button; otherwise, <c>false</c>.
    /// </value>
    public bool? ShowCreateNewTaxonButton
    {
      get => this.ResolveProperty<bool?>(nameof (ShowCreateNewTaxonButton), this.showCreateNewTaxonButton, new bool?(true));
      set => this.showCreateNewTaxonButton = value;
    }

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    public Guid RootTaxonID
    {
      get => this.ResolveProperty<Guid>(nameof (RootTaxonID), this.rootTaxonID, Guid.Empty);
      set => this.rootTaxonID = value;
    }
  }
}
