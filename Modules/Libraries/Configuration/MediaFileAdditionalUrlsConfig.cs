// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.MediaFileAdditionalUrlsConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// Provides configuration for serving media content files directly from the additional URLs.
  /// </summary>
  public class MediaFileAdditionalUrlsConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.MediaFileAdditionalUrlsConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public MediaFileAdditionalUrlsConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether additional URLs of media content items
    /// should be served directly from the site root no matter of their format.
    /// </summary>
    [ConfigurationProperty("additionalUrlsToFiles", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AdditionalUrlsToFilesDescription", Title = "AdditionalUrlsToFilesTitle")]
    public bool AdditionalUrlsToFiles
    {
      get => (bool) this["additionalUrlsToFiles"];
      set
      {
        if (this.AdditionalUrlsToFiles == value)
          return;
        this.MediaFilesAdditionalUrlsInitialized = false;
        this["additionalUrlsToFiles"] = (object) value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether additional cache table that holds URLs
    /// to media files is initialized.
    /// </summary>
    [ConfigurationProperty("mediaFilesAdditionalURLsInitialized", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions))]
    [Browsable(false)]
    public bool MediaFilesAdditionalUrlsInitialized
    {
      get => (bool) this["mediaFilesAdditionalURLsInitialized"];
      set => this["mediaFilesAdditionalURLsInitialized"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropNames
    {
      internal const string AdditionalUrlsToFilesPropName = "additionalUrlsToFiles";
      internal const string MediaFilesAdditionalUrlsInitializedPropName = "mediaFilesAdditionalURLsInitialized";
    }
  }
}
