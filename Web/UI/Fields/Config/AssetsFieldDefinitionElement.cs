// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.AssetsFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for asset fields.</summary>
  public class AssetsFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IAssetsFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public AssetsFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new AssetsFieldDefinition((ConfigElement) this);

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (AssetsField);

    /// <summary>
    /// Gets an instance of configuration element that represents the definition
    /// object in configuration.
    /// </summary>
    /// <value>Configuration element representing the current definition.</value>
    public new ConfigElement ConfigDefinition => throw new NotImplementedException();

    /// <summary>Gets or sets the display mode of the control.</summary>
    /// <value>The display mode.</value>
    [ConfigurationProperty("workMode")]
    public AssetsWorkMode? WorkMode
    {
      get => (AssetsWorkMode?) this["workMode"];
      set => this["workMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the comma separated list of allowed extensions when uploading.
    /// </summary>
    [ConfigurationProperty("allowedExtensions")]
    public string AllowedExtensions
    {
      get => (string) this["allowedExtensions"];
      set => this["allowedExtensions"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the maximum size of the file to be uploaded through the assets field.
    /// </summary>
    [ConfigurationProperty("maxFileSize")]
    public int MaxFileSize
    {
      get => (int) this["maxFileSize"];
      set => this["maxFileSize"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the id of the library that stores the file.
    /// </summary>
    [ConfigurationProperty("targetLibraryId")]
    public Guid? TargetLibraryId
    {
      get => (Guid?) this["targetLibraryId"];
      set => this["targetLibraryId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the id of the library which items will be shown in the selector's select screen.
    /// </summary>
    [ConfigurationProperty("sourceLibraryId")]
    public Guid? SourceLibraryId
    {
      get => (Guid?) this["sourceLibraryId"];
      set => this["sourceLibraryId"] = (object) value;
    }

    /// <summary>Gets or sets the select button text.</summary>
    [ConfigurationProperty("selectButtonText")]
    public string SelectButtonText
    {
      get => (string) this["selectButtonText"];
      set => this["selectButtonText"] = (object) value;
    }

    /// <summary>Gets or sets the select button CSS class.</summary>
    [ConfigurationProperty("selectButtonCssClass")]
    public string SelectButtonCssClass
    {
      get => (string) this["selectButtonCssClass"];
      set => this["selectButtonCssClass"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the option to hide options switcher and use only upload option.
    /// </summary>
    /// <value>The flag, indicating if only upload mode will be used.</value>
    [ConfigurationProperty("useOnlyUploadMode")]
    public bool? UseOnlyUploadMode
    {
      get => (bool?) this["useOnlyUploadMode"];
      set => this["useOnlyUploadMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the option to hide upload option and use only select mode.
    /// </summary>
    /// <value>The flag, indicating if only select mode will be used.</value>
    [ConfigurationProperty("useOnlySelectMode")]
    public bool? UseOnlySelectMode
    {
      get => (bool?) this["useOnlySelectMode"];
      set => this["useOnlySelectMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the CSS class that will be applied to the generated HTML of the media selector.
    /// </summary>
    /// <value>The selector CSS class.</value>
    [ConfigurationProperty("selectorCssClass")]
    public string SelectorCssClass
    {
      get => (string) this["selectorCssClass"];
      set => this["selectorCssClass"] = (object) value;
    }

    [ConfigurationProperty("preSelectItemInSelector")]
    public bool PreSelectItemInSelector
    {
      get => (bool) this["preSelectItemInSelector"];
      set => this["preSelectItemInSelector"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("selectorOpenMode")]
    public MediaSelectorOpenMode? SelectorOpenMode
    {
      get => (MediaSelectorOpenMode?) this["selectorOpenMode"];
      set => this["selectorOpenMode"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal new struct FieldProps
    {
      public const string WorkMode = "workMode";
      public const string allowedExtensions = "allowedExtensions";
      public const string maxFileSize = "maxFileSize";
      public const string targetLibraryId = "targetLibraryId";
      public const string sourceLibraryId = "sourceLibraryId";
      public const string selectButtonText = "selectButtonText";
      public const string selectButtonCssClass = "selectButtonCssClass";
      public const string useOnlyUploadMode = "useOnlyUploadMode";
      public const string useOnlySelectMode = "useOnlySelectMode";
      public const string selectorCssClass = "selectorCssClass";
      public const string preSelectItemInSelector = "preSelectItemInSelector";
      public const string selectorOpenMode = "selectorOpenMode";
    }
  }
}
