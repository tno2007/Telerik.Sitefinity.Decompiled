// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.AssetsFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A definition class containing all information needed to construct an instance of a <see cref="T:Telerik.Sitefinity.Web.UI.Fields.AssetsField" />.
  /// </summary>
  public class AssetsFieldDefinition : 
    FieldControlDefinition,
    IAssetsFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private AssetsWorkMode? workMode;
    private string allowedExtensions;
    private int maxFileSize;
    private Guid? targetLibraryId;
    private Guid? sourceLibraryId;
    private string selectButtonText;
    private string selectButtonCssClass;
    private bool? useOnlyUploadMode;
    private bool? useOnlySelectMode;
    private string selectorCssClass;
    private bool preselectItemInSelector;
    private MediaSelectorOpenMode? selectorOpenMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.AssetsFieldDefinition" /> class.
    /// </summary>
    public AssetsFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.AssetsFieldDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public AssetsFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets or sets the mode in which assets field control will work.
    /// </summary>
    /// <value>The work mode of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.AssetsField" />.</value>
    public AssetsWorkMode? WorkMode
    {
      get => this.ResolveProperty<AssetsWorkMode?>(nameof (WorkMode), this.workMode);
      set => this.workMode = value;
    }

    /// <summary>
    /// Gets or sets the comma separated list of allowed extensions when uploading.
    /// </summary>
    public string AllowedExtensions
    {
      get => this.ResolveProperty<string>(nameof (AllowedExtensions), this.allowedExtensions);
      set => this.allowedExtensions = value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the file to be uploaded through the assets field.
    /// </summary>
    public int MaxFileSize
    {
      get => this.ResolveProperty<int>(nameof (MaxFileSize), this.maxFileSize);
      set => this.maxFileSize = value;
    }

    /// <summary>
    /// Gets or sets the id of the library that stores the file.
    /// </summary>
    public Guid? TargetLibraryId
    {
      get => this.ResolveProperty<Guid?>(nameof (TargetLibraryId), this.targetLibraryId);
      set => this.targetLibraryId = value;
    }

    /// <summary>
    /// Gets or sets the id of the library which items will be shown in the selector's select screen.
    /// </summary>
    /// <value>The source library id.</value>
    public Guid? SourceLibraryId
    {
      get => this.ResolveProperty<Guid?>(nameof (SourceLibraryId), this.sourceLibraryId);
      set => this.sourceLibraryId = value;
    }

    /// <summary>Gets or sets the select button text.</summary>
    public string SelectButtonText
    {
      get => this.ResolveProperty<string>(nameof (SelectButtonText), this.selectButtonText);
      set => this.selectButtonText = value;
    }

    /// <summary>Gets or sets the select button CSS class.</summary>
    public string SelectButtonCssClass
    {
      get => this.ResolveProperty<string>(nameof (SelectButtonCssClass), this.selectButtonCssClass);
      set => this.selectButtonCssClass = value;
    }

    /// <summary>
    /// Gets or sets the option to hide options switcher and use only upload option.
    /// </summary>
    /// <value>The flag, indicating if only upload mode will be used.</value>
    public bool? UseOnlyUploadMode
    {
      get => this.ResolveProperty<bool?>(nameof (UseOnlyUploadMode), this.useOnlyUploadMode);
      set => this.useOnlyUploadMode = value;
    }

    /// <summary>
    /// Gets or sets the option to hide upload option and use only select mode.
    /// </summary>
    /// <value>The flag, indicating if only select mode will be used.</value>
    public bool? UseOnlySelectMode
    {
      get => this.ResolveProperty<bool?>(nameof (UseOnlySelectMode), this.useOnlySelectMode);
      set => this.useOnlySelectMode = value;
    }

    /// <summary>
    /// Gets or sets the CSS class that will be applied to the generated HTML of the media selector.
    /// </summary>
    /// <value>The selector CSS class.</value>
    public string SelectorCssClass
    {
      get => this.ResolveProperty<string>(nameof (SelectorCssClass), this.selectorCssClass);
      set => this.selectorCssClass = value;
    }

    /// <summary>
    /// Determines whether to preselect already selected item in the media selector.
    /// </summary>
    /// <value>The pre select item in selector.</value>
    public bool PreSelectItemInSelector
    {
      get => this.ResolveProperty<bool>(nameof (PreSelectItemInSelector), this.preselectItemInSelector);
      set => this.preselectItemInSelector = value;
    }

    /// <summary>
    /// Determines what screen will be shown when the selector is opened - upload or select.
    /// </summary>
    /// <value>The selector open mode.</value>
    public MediaSelectorOpenMode? SelectorOpenMode
    {
      get => this.ResolveProperty<MediaSelectorOpenMode?>("selectorOpenMode", this.selectorOpenMode);
      set => this.selectorOpenMode = value;
    }
  }
}
