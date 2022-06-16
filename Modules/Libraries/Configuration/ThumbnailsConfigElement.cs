// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.ThumbnailsConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// Provides configuration information about the Thumbnails in the Libraries module.
  /// </summary>
  public class ThumbnailsConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.ThumbnailsConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ThumbnailsConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the thumbnail profiles configuration.</summary>
    [ConfigurationProperty("thumbnails")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ThumbnailProfilesTitleDescription", Title = "ThumbnailProfilesTitle")]
    [ConfigurationCollection(typeof (ThumbnailProfileConfigElement), AddItemName = "add")]
    public ConfigElementDictionary<string, ThumbnailProfileConfigElement> Profiles => (ConfigElementDictionary<string, ThumbnailProfileConfigElement>) this["thumbnails"];

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropNames
    {
      public const string Thumbnails = "thumbnails";
    }
  }
}
