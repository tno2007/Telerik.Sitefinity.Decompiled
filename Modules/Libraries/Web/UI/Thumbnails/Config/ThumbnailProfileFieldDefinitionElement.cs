// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Config.ThumbnailProfileFieldDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Contracts;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Definitions;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Config
{
  public class ThumbnailProfileFieldDefinitionElement : 
    FieldControlDefinitionElement,
    IThumbnailProfileFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Config.ThumbnailProfileFieldDefinitionElement" /> class.
    /// </summary>
    /// <param name="parent">The parent.</param>
    public ThumbnailProfileFieldDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new ThumbnailProfileFieldDefinition((ConfigElement) this);

    public override Type DefaultFieldType => typeof (ThumbnailProfileField);

    /// <inheritdoc />
    [ConfigurationProperty("libraryType", DefaultValue = null)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibraryTypeDescription", Title = "LibraryTypeCaption")]
    public string LibraryType
    {
      get => this["libraryType"] as string;
      set => this["libraryType"] = (object) value;
    }

    /// <inheritdoc />
    [ConfigurationProperty("thumbnailSettingsServiceUrl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ThumbnailSettingsServiceUrlDescription", Title = "ThumbnailSettingsServiceUrlCaption")]
    public string ThumbnailSettingsServiceUrl
    {
      get => (string) this["thumbnailSettingsServiceUrl"];
      set => this["thumbnailSettingsServiceUrl"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string GenerateThumnbailsTitle = "generateThumbnailsTitle";
      public const string DoNotGenerateThumnbailsTitle = "doNotGenerateThumbnailsTitle";
      public const string LibraryType = "libraryType";
      public const string ThumbnailSettingsServiceUrl = "thumbnailSettingsServiceUrl";
    }
  }
}
