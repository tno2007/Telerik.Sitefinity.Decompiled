// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.HierarchicalTaxonFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>
  /// A configuration element that describes the hierarchical taxonomy field.
  /// </summary>
  public class HierarchicalTaxonFieldDefinitionElement : 
    TaxonFieldDefinitionElement,
    IHierarchicalTaxonFieldDefinition,
    ITaxonFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public HierarchicalTaxonFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (HierarchicalTaxonField);

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    [ConfigurationProperty("expandableDefinition")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ExpandableControlElementDescription", Title = "ExpandableControlElementCaption")]
    public new ExpandableControlElement ExpandableDefinitionConfig
    {
      get => (ExpandableControlElement) this["expandableDefinition"];
      set => this["expandableDefinition"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the hierarchical taxon field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition => (IExpandableControlDefinition) this.ExpandableDefinitionConfig;

    /// <summary>
    /// Gets or sets a value indicating whether to allow root selection.
    /// </summary>
    /// <value><c>true</c> if to allow root selection; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("allowRootSelection", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowRootSelectionDescription", Title = "AllowRootSelectionCaption")]
    public bool? AllowRootSelection
    {
      get => (bool?) this["allowRootSelection"];
      set => this["allowRootSelection"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the done-selecting button.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show the done-selecting button; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("showDoneSelectingButton", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowDoneSelectingButtonDescription", Title = "ShowDoneSelectingButtonCaption")]
    public bool? ShowDoneSelectingButton
    {
      get => (bool?) this["showDoneSelectingButton"];
      set => this["showDoneSelectingButton"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the create-new-taxon button.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if to show the create-new-taxon button; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("showCreateNewTaxonButton", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowCreateNewTaxonButtonDescription", Title = "ShowCreateNewTaxonButtonCaption")]
    public bool? ShowCreateNewTaxonButton
    {
      get => (bool?) this["showCreateNewTaxonButton"];
      set => this["showCreateNewTaxonButton"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the ID of the taxon from which the binding should start.
    /// </summary>
    /// <value>The root taxon ID.</value>
    [ConfigurationProperty("rootTaxonID", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RootTaxonIDDescription", Title = "RootTaxonIDCaption")]
    public Guid RootTaxonID
    {
      get => (Guid) this["rootTaxonID"];
      set => this["rootTaxonID"] = (object) value;
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new HierarchicalTaxonFieldDefinition((ConfigElement) this);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string ExpandableDefinition = "expandableDefinition";
      public const string AllowRootSelection = "allowRootSelection";
      public const string ShowDoneSelectingButton = "showDoneSelectingButton";
      public const string ShowCreateNewTaxonButton = "showCreateNewTaxonButton";
      public const string RootTaxonID = "rootTaxonID";
    }
  }
}
