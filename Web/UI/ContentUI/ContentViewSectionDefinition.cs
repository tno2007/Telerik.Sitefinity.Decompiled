// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.ContentViewSectionDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of the respective field section control.
  /// </summary>
  public class ContentViewSectionDefinition : 
    DefinitionBase,
    IContentViewSectionDefinition,
    IDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private string title;
    private string resourceClassId;
    private int? ordinal;
    private string sectionName;
    private List<IFieldDefinition> fields;
    private FieldDisplayMode? displayMode;
    private HtmlTextWriterTag wrapperTag;
    private string cssClass;
    private string controlId;
    private bool isHiddenInTranslationMode;
    private IExpandableControlDefinition expandableDefinition;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewSectionDefinition" /> class.
    /// </summary>
    public ContentViewSectionDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewSectionDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public ContentViewSectionDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public ContentViewSectionDefinition GetDefinition() => this;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    public string ControlDefinitionName
    {
      get => this.ResolveProperty<string>(nameof (ControlDefinitionName), this.controlDefinitionName);
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public string ViewName
    {
      get => this.ResolveProperty<string>(nameof (ViewName), this.viewName);
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the CSS class for this section.</summary>
    /// <value>The CSS class.</value>
    public string CssClass
    {
      get => this.ResolveProperty<string>(nameof (CssClass), this.cssClass);
      set => this.cssClass = value;
    }

    /// <summary>
    /// Gets or sets the <see cref="P:Telerik.Sitefinity.Web.UI.ContentUI.ContentViewSectionDefinition.DisplayMode" />.
    /// </summary>
    /// <value>The display mode.</value>
    public FieldDisplayMode? DisplayMode
    {
      get => this.ResolveProperty<FieldDisplayMode?>(nameof (DisplayMode), this.displayMode);
      set => this.displayMode = value;
    }

    /// <summary>
    /// Defines a list of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IFieldDefinition" />.
    /// </summary>
    /// <value>The fields.</value>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public virtual List<IFieldDefinition> Fields
    {
      get
      {
        if (this.fields == null)
          this.fields = this.ConfigDefinition == null ? new List<IFieldDefinition>() : ((IEnumerable<IFieldDefinition>) ((ContentViewSectionElement) this.ConfigDefinition).Fields.Elements.Select<FieldDefinitionElement, FieldDefinition>((Func<FieldDefinitionElement, FieldDefinition>) (configField =>
          {
            FieldDefinition definition = (FieldDefinition) configField.GetDefinition();
            definition.ControlDefinitionName = this.ControlDefinitionName;
            definition.ViewName = this.ViewName;
            definition.SectionName = this.Name;
            definition.FieldName = configField.FieldName;
            return definition;
          }))).ToList<IFieldDefinition>();
        return this.fields;
      }
    }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value></value>
    public string Name
    {
      get => this.ResolveProperty<string>(nameof (Name), this.sectionName);
      set => this.sectionName = value;
    }

    /// <summary>Gets or sets the ordinal position of the section.</summary>
    /// <value>The ordinal.</value>
    public int? Ordinal
    {
      get => this.ResolveProperty<int?>(nameof (Ordinal), this.ordinal);
      set => this.ordinal = value;
    }

    /// <summary>
    /// Gets or sets the global resource class ID to use for localized strings.
    /// </summary>
    /// <value></value>
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId);
      set => this.resourceClassId = value;
    }

    /// <summary>Gets or sets the title of the section.</summary>
    /// <value></value>
    public string Title
    {
      get => this.ResolveProperty<string>(nameof (Title), this.title);
      set => this.title = value;
    }

    /// <summary>
    /// Gets or sets the value for the <see cref="T:System.Web.UI.Control" /> ID property of the control that will be constructed based on this definition.
    /// </summary>
    /// <value>The control id.</value>
    public string ControlId
    {
      get => this.ResolveProperty<string>(nameof (ControlId), this.controlId);
      set => this.controlId = value;
    }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    public HtmlTextWriterTag WrapperTag
    {
      get => this.ResolveProperty<HtmlTextWriterTag>(nameof (WrapperTag), this.wrapperTag, HtmlTextWriterTag.Div);
      set => this.wrapperTag = value;
    }

    /// <summary>
    /// Gets or sets the tag that will be rendered as a wrapper.
    /// </summary>
    /// <value>The wrapper tag.</value>
    public bool IsHiddenInTranslationMode
    {
      get => this.ResolveProperty<bool>(nameof (IsHiddenInTranslationMode), this.isHiddenInTranslationMode, false);
      set => this.isHiddenInTranslationMode = value;
    }

    /// <summary>
    /// Defines a list of <see cref="!:IContentViewFieldDefinition" />.
    /// </summary>
    /// <value>The fields.</value>
    IEnumerable<IFieldDefinition> IContentViewSectionDefinition.Fields => (IEnumerable<IFieldDefinition>) this.Fields;

    /// <summary>
    /// Gets or sets the object that defines the expandable behavior of the text field.
    /// </summary>
    /// <value></value>
    public IExpandableControlDefinition ExpandableDefinition
    {
      get => this.ResolveProperty<IExpandableControlDefinition>(nameof (ExpandableDefinition), this.expandableDefinition);
      set => this.expandableDefinition = value;
    }
  }
}
