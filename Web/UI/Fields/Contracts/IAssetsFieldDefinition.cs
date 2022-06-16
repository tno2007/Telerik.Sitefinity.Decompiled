// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Contracts.IAssetsFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Contracts
{
  /// <summary>Represents the definition of the AssetsField.</summary>
  public interface IAssetsFieldDefinition : IFieldControlDefinition, IFieldDefinition, IDefinition
  {
    /// <summary>
    /// Gets or sets the work mode of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.AssetsField" />.
    /// </summary>
    AssetsWorkMode? WorkMode { get; set; }

    /// <summary>
    /// Gets or sets the comma separated list of allowed extensions when uploading.
    /// </summary>
    string AllowedExtensions { get; set; }

    /// <summary>
    /// Gets or sets the maximum size of the file to be uploaded through the assets field.
    /// </summary>
    int MaxFileSize { get; set; }

    /// <summary>
    /// Gets or sets the id of the library that stores the file.
    /// </summary>
    Guid? TargetLibraryId { get; set; }

    /// <summary>
    /// Gets or sets the id of the library which items will be shown in the selector's select screen.
    /// </summary>
    /// <value>The source library id.</value>
    Guid? SourceLibraryId { get; set; }

    /// <summary>Gets or sets the select button text.</summary>
    string SelectButtonText { get; set; }

    /// <summary>Gets or sets the select button CSS class.</summary>
    string SelectButtonCssClass { get; set; }

    /// <summary>
    /// Gets or sets the option to hide options switcher and use only upload option.
    /// </summary>
    bool? UseOnlyUploadMode { get; set; }

    /// <summary>
    /// Gets or sets the option to hide upload option and use only select mode.
    /// </summary>
    bool? UseOnlySelectMode { get; set; }

    /// <summary>
    /// Gets or sets the CSS class that will be applied to the generated HTML of the media selector.
    /// </summary>
    /// <value>The selector CSS class.</value>
    string SelectorCssClass { get; set; }

    /// <summary>
    /// Determines whether to preselect already selected item in the media selector.
    /// </summary>
    /// <value>The pre select item in selector.</value>
    bool PreSelectItemInSelector { get; set; }

    /// <summary>
    /// Determines what screen will be shown when the selector is opened - upload or select.
    /// </summary>
    /// <value>The selector open mode.</value>
    MediaSelectorOpenMode? SelectorOpenMode { get; set; }
  }
}
