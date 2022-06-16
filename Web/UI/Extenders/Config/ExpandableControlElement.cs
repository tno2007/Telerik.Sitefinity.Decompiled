// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Extenders.Config.ExpandableControlElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Extenders.Contracts;

namespace Telerik.Sitefinity.Web.UI.Extenders.Config
{
  /// <summary>
  /// A configuration element that describes a single choice item.
  /// </summary>
  public class ExpandableControlElement : ConfigElement, IExpandableControlDefinition
  {
    private string controlDefinitionName;
    private string viewName;
    private string sectionName;
    private string fieldName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Extenders.Config.ExpandableControlElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ExpandableControlElement(ConfigElement parent)
      : base(parent)
    {
      if (!typeof (IContentViewSectionDefinition).IsAssignableFrom(parent.GetType()))
        return;
      IContentViewSectionDefinition sectionDefinition = (IContentViewSectionDefinition) parent;
      this.ControlDefinitionName = sectionDefinition.ControlDefinitionName;
      this.ViewName = sectionDefinition.ViewName;
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

    /// <summary>Gets or sets the name of the field.</summary>
    /// <value>The name of the field.</value>
    public string FieldName
    {
      get => this.fieldName;
      set => this.fieldName = value;
    }

    /// <summary>
    /// Gets or sets the text to be displayed on the element that expands the control.
    /// </summary>
    [ConfigurationProperty("expandText", DefaultValue = "")]
    public string ExpandText
    {
      get => (string) this["expandText"];
      set => this["expandText"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control
    /// is to be expanded by default true; otherwise false.
    /// </summary>
    [ConfigurationProperty("expanded", DefaultValue = true)]
    public bool? Expanded
    {
      get => new bool?((bool) this["expanded"]);
      set => this["expanded"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the resource class used to localize messages.
    /// </summary>
    /// <value></value>
    /// <remarks>
    /// If this property is set; text properties will be treated as
    /// keys of localized entry in the localization class; otherwise they will be displayed
    /// as is.
    /// </remarks>
    [ConfigurationProperty("resourceClassId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ResourceClassIdDescription", Title = "ResourceClassIdCaption")]
    public string ResourceClassId
    {
      get => (string) this["resourceClassId"];
      set => this["resourceClassId"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string controlDefinitionName = "controlDefinitionName";
      public const string viewName = "viewName";
      public const string sectionName = "sectionName";
      public const string fieldName = "fieldName";
      public const string expandText = "expandText";
      public const string expanded = "expanded";
      public const string resourceClassId = "resourceClassId";
    }
  }
}
