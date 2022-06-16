// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.CompositeFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A class that provides all information that is needed to construct a CompositeField control
  /// </summary>
  public class CompositeFieldDefinition : 
    FieldDefinition,
    ICompositeFieldDefinition,
    IFieldDefinition,
    IDefinition
  {
    private List<IFieldControlDefinition> fields;
    private FieldDisplayMode displayMode;
    private HtmlTextWriterTag wrapperTag;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.CompositeFieldDefinition" /> class.
    /// </summary>
    public CompositeFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.CompositeFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public CompositeFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// A collection of IFieldControlDefinition objects containing the definitions of all child field controls
    /// of the composite control implementing this contract
    /// </summary>
    /// <value>The field definitions.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public List<IFieldControlDefinition> Fields
    {
      get
      {
        if (this.fields == null)
          this.fields = new List<IFieldControlDefinition>();
        return this.ResolveProperty<List<IFieldControlDefinition>>(nameof (Fields), this.fields);
      }
      set => this.fields = value;
    }

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    public FieldDisplayMode DisplayMode
    {
      get => this.ResolveProperty<FieldDisplayMode>(nameof (DisplayMode), this.displayMode);
      set => this.displayMode = value;
    }

    /// <summary>
    /// A collection of IFieldControlDefinition objects containing the definitions of all child field controls
    /// of the composite control implementing this contract
    /// </summary>
    /// <value>The field definitions.</value>
    IEnumerable<IFieldControlDefinition> ICompositeFieldDefinition.Fields => (IEnumerable<IFieldControlDefinition>) this.Fields;

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    public HtmlTextWriterTag WrapperTag
    {
      get
      {
        HtmlTextWriterTag wrapperTag = this.ResolveProperty<HtmlTextWriterTag>(nameof (WrapperTag), this.wrapperTag);
        if (wrapperTag == HtmlTextWriterTag.Unknown)
          wrapperTag = HtmlTextWriterTag.Li;
        return wrapperTag;
      }
      set => this.wrapperTag = value;
    }
  }
}
