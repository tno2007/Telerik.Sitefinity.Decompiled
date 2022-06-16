// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.DownloadListViewMasterElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.Web.UI;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Libraries.Configuration
{
  /// <summary>
  /// The configuration element for DownloadListViewMasterDefinition
  /// </summary>
  public class DownloadListViewMasterElement : 
    ContentViewMasterElement,
    IDownloadListViewMasterDefinition,
    IDownloadListViewDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.DownloadListViewMasterElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public DownloadListViewMasterElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new DownloadListViewMasterDefinition((ConfigElement) this);

    /// <summary>
    /// Sets the size of the thumbnail - None - no thumbnail, Big and Small
    /// </summary>
    [ConfigurationProperty("thumbnailType", DefaultValue = ThumbnailType.NoSet, IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ThumbnailTypeDescription", Title = "ThumbnailTypeCaption")]
    public ThumbnailType ThumbnailType
    {
      get => (ThumbnailType) this["thumbnailType"];
      set => this["thumbnailType"] = (object) value;
    }

    /// <summary>
    /// A value indicating whether to displays a download link below the description.
    /// </summary>
    [ConfigurationProperty("showDownloadLinkBelowDescription", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowDownloadLinkBelowDescriptionDescription", Title = "ShowDownloadLinkBelowDescriptionCaption")]
    public bool? ShowDownloadLinkBelowDescription
    {
      get => (bool?) this["showDownloadLinkBelowDescription"];
      set => this["showDownloadLinkBelowDescription"] = (object) value;
    }

    /// <summary>
    /// A value indicating whether to displays a download link above the description.
    /// </summary>
    [ConfigurationProperty("showDownloadLinkAboveDescription", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowDownloadLinkAboveDescriptionDescription", Title = "ShowDownloadLinkAboveDescriptionCaption")]
    public bool? ShowDownloadLinkAboveDescription
    {
      get => (bool?) this["showDownloadLinkAboveDescription"];
      set => this["showDownloadLinkAboveDescription"] = (object) value;
    }

    /// <summary>
    /// Constants to hold the string keys for configuration properties of DownloadListViewMasterElement
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct DownloadListViewMasterProps
    {
      public const string ThumbnailType = "thumbnailType";
      public const string ShowDownloadLinkBelowDescription = "showDownloadLinkBelowDescription";
      public const string ShowDownloadLinkAboveDescription = "showDownloadLinkAboveDescription";
    }
  }
}
