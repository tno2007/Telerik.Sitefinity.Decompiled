// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.PageTemplateFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a
  /// <see cref="!:PageTemplateField" />
  /// </summary>
  public class PageTemplateFieldDefinition : 
    FieldControlDefinition,
    IPageTemplateFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private bool shouldNotCreateTemplateForMasterPage;
    private bool isBackendTemplate;
    private bool showEmptyTemplate;
    private bool showAllBasicTemplates;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public PageTemplateFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public PageTemplateFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Specifies whether to create or not a template when master page is selected.
    /// </summary>
    /// <value></value>
    public bool ShouldNotCreateTemplateForMasterPage
    {
      get => this.ResolveProperty<bool>(nameof (ShouldNotCreateTemplateForMasterPage), this.shouldNotCreateTemplateForMasterPage);
      set => this.shouldNotCreateTemplateForMasterPage = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is backend template.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is backend template; otherwise, <c>false</c>.
    /// </value>
    public bool IsBackendTemplate
    {
      get => this.ResolveProperty<bool>(nameof (IsBackendTemplate), this.isBackendTemplate);
      set => this.isBackendTemplate = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field should show empty templates for Hybrid and Webforms frameworks.
    /// </summary>
    public bool ShowEmptyTemplate
    {
      get => this.ResolveProperty<bool>(nameof (ShowEmptyTemplate), this.showEmptyTemplate);
      set => this.showEmptyTemplate = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the field should list extended basic templates for Webforms framework.
    /// </summary>
    public bool ShowAllBasicTemplates
    {
      get => this.ResolveProperty<bool>(nameof (ShowAllBasicTemplates), this.showAllBasicTemplates);
      set => this.showAllBasicTemplates = value;
    }
  }
}
