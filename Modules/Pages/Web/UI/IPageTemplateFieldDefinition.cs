// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.IPageTemplateFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>
  /// An interface that provides the common members for the definition of page template field
  /// </summary>
  public interface IPageTemplateFieldDefinition : 
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Specifies whether to create or not a template when master page is selected.
    /// </summary>
    bool ShouldNotCreateTemplateForMasterPage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is backend template.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is backend template; otherwise, <c>false</c>.
    /// </value>
    bool IsBackendTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the template field should show empty tempalte in the template selectod dialog.
    /// </summary>
    bool ShowEmptyTemplate { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the template field should show extended template list that includes mock templates used for basing new templates.
    /// </summary>
    bool ShowAllBasicTemplates { get; set; }
  }
}
