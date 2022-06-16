// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.FieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an
  /// instance of a field.
  /// </summary>
  public abstract class FieldDefinition : DefinitionBase, IFieldDefinition, IDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private string sectionName;
    private string fieldName;
    private string title;
    private string description;
    private string example;
    private Type fieldType;
    private string fieldVirtualPath;
    private string resourceClassId;
    private string cssClass;
    private bool? hidden;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.FieldDefinition" /> class.
    /// </summary>
    public FieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.FieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public FieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    public string SectionName
    {
      get => this.sectionName;
      set => this.sectionName = value;
    }

    /// <summary>
    /// Gets or sets the name of the field. This value is used to identify the field.
    /// </summary>
    /// <value></value>
    public string FieldName
    {
      get => this.ResolveProperty<string>(nameof (FieldName), this.fieldName);
      set => this.fieldName = value;
    }

    /// <summary>Gets or sets the title of the field element</summary>
    /// <value>The title.</value>
    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
    }

    /// <summary>Gets or sets the description of the field element.</summary>
    /// <value>The description.</value>
    public string Description
    {
      get => this.ResolveProperty<string>(nameof (Description), this.description);
      set => this.description = value;
    }

    /// <summary>Gets or sets the example of the field element</summary>
    /// <value>The example.</value>
    public string Example
    {
      get => this.ResolveProperty<string>(nameof (Example), this.example);
      set => this.example = value;
    }

    /// <summary>Gets or sets the type of the field.</summary>
    /// <value>The type of the field.</value>
    public Type FieldType
    {
      get => this.ResolveProperty<Type>(nameof (FieldType), this.fieldType, this.DefaultFieldType);
      set => this.fieldType = value;
    }

    /// <summary>
    /// Gets or sets the virtual path of the of the field as a user control.
    /// </summary>
    /// <value></value>
    public string FieldVirtualPath
    {
      get => this.ResolveProperty<string>(nameof (FieldVirtualPath), this.fieldVirtualPath);
      set => this.fieldVirtualPath = value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public virtual Type DefaultFieldType => (Type) null;

    /// <summary>
    /// Gets or sets the name of the resource class used to localize the labels of the field.
    /// </summary>
    /// <value>The name of the resource class.</value>
    /// <remarks>
    /// If this property is left empty, string values such as Title, Description and Example will
    /// be used directly; otherwise the values of these properties will be used as keys for the resources
    /// and localized resources will be loaded instead.
    /// </remarks>
    public virtual string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    /// <summary>Gets or sets the css class of the field control.</summary>
    /// <value>The css class of the field control.</value>
    public virtual string CssClass
    {
      get => this.ResolveProperty<string>(nameof (CssClass), this.cssClass);
      set => this.cssClass = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if the field element is hidden.
    /// </summary>
    public virtual bool? Hidden
    {
      get => this.ResolveProperty<bool?>(nameof (Hidden), this.hidden);
      set => this.hidden = value;
    }

    /// <summary>Gets the configuration definition.</summary>
    /// <returns></returns>
    protected override ConfigElement GetConfigurationDefinition() => this.controlDefinitionName.IsNullOrEmpty() ? (ConfigElement) null : (ConfigElement) ((ContentViewDetailElement) Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls[this.controlDefinitionName].ViewsConfig[this.viewName]).Sections[this.sectionName].Fields[this.fieldName];
  }
}
