// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.PageTemplateFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>The configuration element for page template fields</summary>
  public class PageTemplateFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IPageTemplateFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    internal const string ShouldNotCreateTemplateForMasterPageProperty = "shouldNotCreateTemplateForMasterPage";
    internal const string IsBackendTemplateProperty = "isBackendTemplateProperty";
    internal const string ShowEmptyTemplateProperty = "showEmptyTemplateProperty";
    internal const string ShowAllBasicTemplatesProperty = "ShowAllBasicTemplatesProperty";

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public PageTemplateFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new PageTemplateFieldDefinition((ConfigElement) this);

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (PageTemplateField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();

    /// <summary>
    /// Gets or sets flag if not to create template for selected master page.
    /// </summary>
    /// <value>The should not create template for master page.</value>
    [ConfigurationProperty("shouldNotCreateTemplateForMasterPage", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShouldNotCreateTemplateForMasterPageDescription", Title = "ShouldNotCreateTemplateForMasterPageCaption")]
    public bool ShouldNotCreateTemplateForMasterPage
    {
      get => (bool) this["shouldNotCreateTemplateForMasterPage"];
      set => this["shouldNotCreateTemplateForMasterPage"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is backend template.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is backend template; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("isBackendTemplateProperty", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "IsBackendTemplateDescription", Title = "IsBackendTemplateCaption")]
    public bool IsBackendTemplate
    {
      get => (bool) this["isBackendTemplateProperty"];
      set => this["isBackendTemplateProperty"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the empty base template should be showen.
    /// </summary>
    [ConfigurationProperty("showEmptyTemplateProperty", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowEmptyTemplateDescription", Title = "ShowEmptyTemplateCaption")]
    public bool ShowEmptyTemplate
    {
      get => (bool) this["showEmptyTemplateProperty"];
      set => this["showEmptyTemplateProperty"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether all the basic templates should be shown for both Hybrid and Webforms.
    /// </summary>
    [ConfigurationProperty("ShowAllBasicTemplatesProperty", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowAllBasicTemplatesDescription", Title = "ShowAllBasicTemplatesCaption")]
    public bool ShowAllBasicTemplates
    {
      get => (bool) this["ShowAllBasicTemplatesProperty"];
      set => this["ShowAllBasicTemplatesProperty"] = (object) value;
    }
  }
}
