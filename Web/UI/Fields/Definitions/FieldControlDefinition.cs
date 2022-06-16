// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.FieldControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Sitefinity.Web.UI.Validation.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an
  /// instance of a field control.
  /// </summary>
  public abstract class FieldControlDefinition : 
    FieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string id;
    private string dataFieldName;
    private object value;
    private FieldDisplayMode? displayMode;
    private IValidatorDefinition validation;
    private HtmlTextWriterTag wrapperTag;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public FieldControlDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public FieldControlDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public FieldControlDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets the programmatic identifier assigned to the field control.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The programmatic identifier assigned to the field control.
    /// </returns>
    public string ID
    {
      get => this.ResolveProperty<string>(nameof (ID), this.id);
      set => this.id = value;
    }

    /// <summary>
    /// Gets or sets the name of the data item property the control will be bound to.
    /// </summary>
    /// <value>The name of the data field.</value>
    public string DataFieldName
    {
      get => this.ResolveProperty<string>(nameof (DataFieldName), this.dataFieldName);
      set => this.dataFieldName = value;
    }

    /// <summary>Gets or sets the value of the property.</summary>
    /// <value>The value.</value>
    public object Value
    {
      get => this.ResolveProperty<object>(nameof (Value), this.value);
      set => this.value = value;
    }

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    public FieldDisplayMode? DisplayMode
    {
      get => this.ResolveProperty<FieldDisplayMode?>(nameof (DisplayMode), this.displayMode);
      set => this.displayMode = value;
    }

    /// <summary>Gets or sets a validation mechanism for the control.</summary>
    /// <value>The validation.</value>
    public IValidatorDefinition Validation => this.ResolveProperty<IValidatorDefinition>(nameof (Validation), this.validation);

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    public HtmlTextWriterTag WrapperTag
    {
      get => this.ResolveProperty<HtmlTextWriterTag>(nameof (WrapperTag), this.wrapperTag, HtmlTextWriterTag.Li);
      set => this.wrapperTag = value;
    }
  }
}
