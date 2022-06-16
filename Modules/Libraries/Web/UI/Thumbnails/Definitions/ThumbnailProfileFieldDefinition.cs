// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Definitions.ThumbnailProfileFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.Definitions
{
  public class ThumbnailProfileFieldDefinition : 
    FieldControlDefinition,
    IThumbnailProfileFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private string generateThumnbailsTitle;
    private string doNotGenerateThumnbailsTitle;
    private string libraryType;
    private string thumbnailSettingsServiceUrl;

    public ThumbnailProfileFieldDefinition()
    {
    }

    public ThumbnailProfileFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <inheritdoc />
    public string LibraryType
    {
      get => this.ResolveProperty<string>(nameof (LibraryType), this.libraryType);
      set => this.libraryType = value;
    }

    /// <inheritdoc />
    public string ThumbnailSettingsServiceUrl
    {
      get => this.ResolveProperty<string>(nameof (ThumbnailSettingsServiceUrl), this.thumbnailSettingsServiceUrl);
      set => this.thumbnailSettingsServiceUrl = value;
    }
  }
}
