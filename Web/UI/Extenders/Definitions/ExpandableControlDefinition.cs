// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Extenders.Definitions.ExpandableControlDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Web.UI.Extenders.Definitions
{
  /// <summary>
  /// Class that defines the behavior of the expandable control.
  /// </summary>
  [DataContract]
  public class ExpandableControlDefinition : DefinitionBase, IExpandableControlDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private string sectionName;
    private string fieldName;
    private string expandText;
    private bool? expanded;
    private string resourceClassId;

    /// <summary>Gets or sets the name of the control definition.</summary>
    /// <value>The name of the control definition.</value>
    [DataMember]
    public string ControlDefinitionName
    {
      get => this.controlDefinitionName;
      set => this.controlDefinitionName = value;
    }

    /// <summary>Gets or sets the name of the view.</summary>
    /// <value>The name of the view.</value>
    [DataMember]
    public string ViewName
    {
      get => this.viewName;
      set => this.viewName = value;
    }

    /// <summary>Gets or sets the name of the section.</summary>
    /// <value>The name of the section.</value>
    [DataMember]
    public string SectionName
    {
      get => this.sectionName;
      set => this.sectionName = value;
    }

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    [DataMember]
    public string FieldName
    {
      get => this.fieldName;
      set => this.fieldName = value;
    }

    /// <summary>
    /// Gets or sets the text to be displayed on the element that expands the control.
    /// </summary>
    /// <value></value>
    [DataMember]
    public string ExpandText
    {
      get => this.ResolveProperty<string>(nameof (ExpandText), this.expandText) ?? Res.Get<Labels>().ClickToAdd;
      set => this.expandText = value;
    }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control
    /// is to be expanded by default true; otherwise false.
    /// </summary>
    /// <value></value>
    [DataMember]
    public bool? Expanded
    {
      get => this.ResolveProperty<bool?>(nameof (Expanded), this.expanded);
      set => this.expanded = value;
    }

    /// <summary>
    /// Gets or sets the resource class used to localize message text and title.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If this property is set; text properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    [DataMember]
    public string ResourceClassId
    {
      get => this.ResolveProperty<string>(nameof (ResourceClassId), this.resourceClassId, string.Empty);
      set => this.ResourceClassId = value;
    }

    /// <summary>Gets the configuration definition.</summary>
    /// <returns></returns>
    protected override ConfigElement GetConfigurationDefinition()
    {
      if (string.IsNullOrEmpty(this.controlDefinitionName))
        return (ConfigElement) null;
      ContentViewDetailElement viewDetailElement = (ContentViewDetailElement) Telerik.Sitefinity.Configuration.Config.Get<ContentViewConfig>().ContentViewControls[this.controlDefinitionName].ViewsConfig[this.viewName];
      if (string.IsNullOrEmpty(this.FieldName))
        return (ConfigElement) viewDetailElement.Sections[this.SectionName].ExpandableDefinitionConfig;
      if (viewDetailElement.Sections[this.SectionName].Fields[this.FieldName] == null)
      {
        foreach (FieldDefinitionElement definitionElement1 in viewDetailElement.Sections[this.SectionName].Fields.Values.Where<FieldDefinitionElement>((Func<FieldDefinitionElement, bool>) (e => e is ExpandableFieldElement)).ToList<FieldDefinitionElement>())
        {
          foreach (FieldDefinitionElement definitionElement2 in (IEnumerable<FieldDefinitionElement>) (definitionElement1 as ExpandableFieldElement).ExpandableFields.Values)
          {
            if (this.FieldName == definitionElement2.FieldName)
              return (ConfigElement) definitionElement2.ExpandableDefinitionConfig;
          }
        }
      }
      return (ConfigElement) viewDetailElement.Sections[this.SectionName].Fields[this.FieldName].ExpandableDefinitionConfig;
    }
  }
}
