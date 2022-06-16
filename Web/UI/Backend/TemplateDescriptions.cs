// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.TemplateDescriptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// Represents the string resources for template descriptions.
  /// </summary>
  [ObjectInfo("TemplateDescriptions", ResourceClassId = "TemplateDescriptions")]
  public class TemplateDescriptions : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.TemplateDescriptions" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public TemplateDescriptions()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Web.UI.Backend.TemplateDescriptions" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public TemplateDescriptions(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>Template Descriptions</summary>
    [ResourceEntry("TemplateDescriptionsTitle", Description = "The title of this class.", LastModified = "2009/04/30", Value = "Template Descriptions")]
    public string TemplateDescriptionsTitle => this[nameof (TemplateDescriptionsTitle)];

    /// <summary>Template Descriptions Title plural</summary>
    [ResourceEntry("TemplateDescriptionsTitlePlural", Description = "The title plural of this class.", LastModified = "2009/04/30", Value = "Template Descriptions title plural.")]
    public string TemplateDescriptionsTitlePlural => this[nameof (TemplateDescriptionsTitlePlural)];

    /// <summary>
    /// Contains localizable resources for Sitefinity template descriptions.
    /// </summary>
    [ResourceEntry("TemplateDescriptionsDescription", Description = "The description of this class.", LastModified = "2009/04/30", Value = "Contains localizable resources for Sitefinity template descriptions.")]
    public string TemplateDescriptionsDescription => this[nameof (TemplateDescriptionsDescription)];

    /// <summary>
    /// This template defines the layout of CommandPanel control when used with standard command set.
    /// It uses Repeater control to render command items.
    /// </summary>
    [ResourceEntry("CommandPanelLayout", Description = "Description of CommandPanel layout template.", LastModified = "2009/04/26", Value = "This template defines the layout of CommandPanel control when used with standard command set. It uses Repeater control to render command items.")]
    public string CommandPanelLayout => this[nameof (CommandPanelLayout)];

    /// <summary>
    /// This template defines the layout of ControlPanel control.
    /// </summary>
    [ResourceEntry("ControlPanelLayout", Description = "Description of ControlPanel layout template.", LastModified = "2009/04/26", Value = "This template defines the layout of ControlPanel control.")]
    public string ControlPanelLayout => this[nameof (ControlPanelLayout)];

    /// <summary>
    /// This template defines the layout of ProviderSelectorPanel control.
    /// </summary>
    [ResourceEntry("ProviderSelectorPanel", Description = "Description of ProviderSelectorPanel layout template.", LastModified = "2009/04/28", Value = "This template defines the layout of ProviderSelectorPanel control.")]
    public string ProviderSelectorPanel => this[nameof (ProviderSelectorPanel)];
  }
}
