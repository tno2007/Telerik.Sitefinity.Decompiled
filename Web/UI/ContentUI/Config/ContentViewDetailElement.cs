// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewDetailElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// The configuration element for ContentViewDetailDefinition
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentViewDetailDescription", Title = "ContentViewDetailCaption")]
  public class ContentViewDetailElement : 
    ContentViewDefinitionElement,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Config.ContentViewDetailElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ContentViewDetailElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ContentViewDetailDefinition((ConfigElement) this);

    /// <summary>Gets or sets the sections config.</summary>
    /// <value>The sections config.</value>
    [ConfigurationProperty("sections")]
    [ConfigurationCollection(typeof (ContentViewSectionElement), AddItemName = "sections")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SectionsConfigDescription", Title = "SectionsConfigCaption")]
    public ConfigElementDictionary<string, ContentViewSectionElement> Sections => (ConfigElementDictionary<string, ContentViewSectionElement>) this["sections"];

    /// <summary>
    /// Gets or sets the name of the tag in which the view should be wrapped.
    /// </summary>
    /// <remarks>Default value is UL.</remarks>
    [ConfigurationProperty("wrapperTagName")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WrapperTagNameDescription", Title = "WrapperTagNameCaption")]
    public string WrapperTagName
    {
      get => (string) this["wrapperTagName"];
      set => this["wrapperTagName"] = (object) value;
    }

    [ConfigurationProperty("additionalControlData")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AdditionalControlDataDescription", Title = "AdditionalControlDataCaption")]
    public string AdditionalControlData
    {
      get => (string) this["additionalControlData"];
      set => this["additionalControlData"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the CSS class that should be applied to the wrapper tag.
    /// </summary>
    [ConfigurationProperty("wrapperCssClass")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WrapperCssClassDescription", Title = "WrapperCssClassCaption")]
    public string WrapperCssClass
    {
      get => (string) this["wrapperCssClass"];
      set => this["wrapperCssClass"] = (object) value;
    }

    /// <summary>Defines the name of the CSS class for all fields.</summary>
    /// <value></value>
    [ConfigurationProperty("fieldCssClass", DefaultValue = "", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FieldCssClassDescription", Title = "FieldCssClassCaption")]
    public string FieldCssClass
    {
      get => (string) this["fieldCssClass"];
      set => this["fieldCssClass"] = (object) value;
    }

    /// <summary>Defines the name of the CSS class for all sections.</summary>
    /// <value></value>
    [ConfigurationProperty("sectionCssClass", DefaultValue = "", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SectionCssClassDescription", Title = "SectionCssClassCaption")]
    public string SectionCssClass
    {
      get => (string) this["sectionCssClass"];
      set => this["sectionCssClass"] = (object) value;
    }

    /// <summary>Gets or sets weather to show sections.</summary>
    /// <value></value>
    [ConfigurationProperty("showSections", DefaultValue = true, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowSectionsDescription", Title = "ShowSectionsCaption")]
    public bool? ShowSections
    {
      get => (bool?) this["showSections"];
      set => this["showSections"] = (object) value;
    }

    IEnumerable<IContentViewSectionDefinition> IContentViewDetailDefinition.Sections => this.Sections.Cast<IContentViewSectionDefinition>();

    /// <summary>
    /// Gets or sets the ID of the page that should display the master view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    [ConfigurationProperty("masterPageId", DefaultValue = "00000000-0000-0000-0000-000000000000", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MasterPageIdDescription", Title = "MasterPageIdCaption")]
    public Guid MasterPageId
    {
      get => (Guid) this["masterPageId"];
      set => this["masterPageId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the data ID of the content item that should be displayed.
    /// </summary>
    /// <value>The data item pageId.</value>
    [ConfigurationProperty("dataItemId", DefaultValue = "00000000-0000-0000-0000-000000000000", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DataItemIdDescription", Title = "DataItemIdCaption")]
    public Guid DataItemId
    {
      get => (Guid) this["dataItemId"];
      set => this["dataItemId"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct DetailProps
    {
      public const string additionalControlData = "additionalControlData";
      public const string wrapperTagName = "wrapperTagName";
      public const string wrapperCssClass = "wrapperCssClass";
      public const string showSections = "showSections";
      public const string sectionCssClass = "sectionCssClass";
      public const string fieldCssClass = "fieldCssClass";
      public const string sectionsConfig = "sections";
      public const string MasterPageId = "masterPageId";
      public const string DataItemId = "dataItemId";
    }
  }
}
