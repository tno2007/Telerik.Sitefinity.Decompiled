// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.DocumentsConfigElement
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
  /// Provides configuration information about the Images in the Libraries module
  /// </summary>
  public class DocumentsConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.ImagesConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public DocumentsConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the connection string.</summary>
    /// <value>The name of the connection string.</value>
    [ConfigurationProperty("urlRoot", DefaultValue = "docs", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "LibrariesUrlRootDescription", Title = "LibrariesUrlRootCaption")]
    public string UrlRoot
    {
      get => (string) this["urlRoot"];
      set => this["urlRoot"] = (object) value;
    }

    /// <summary>Gets the configured AllowedExensionsElement settings.</summary>
    /// <value>The AllowedExensionsElement settings.</value>
    [ConfigurationProperty("AllowedExensionsSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedExtensionSettingsDescription", Title = "AllowedExtensionSettingsTitle")]
    public string AllowedExensionsSettings
    {
      get => (string) this[nameof (AllowedExensionsSettings)];
      set => this[nameof (AllowedExensionsSettings)] = (object) value;
    }

    /// <summary>Maximum allowed size in KB.</summary>
    /// <value>Maximum allowed size in KB.</value>
    [ConfigurationProperty("AllowedMaxDocumentSize", DefaultValue = 0)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedMaxSizeDescription", Title = "AllowedMaxSizeTitle")]
    public int AllowedMaxDocumentSize
    {
      get => (int) this[nameof (AllowedMaxDocumentSize)];
      set => this[nameof (AllowedMaxDocumentSize)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable allowed extension.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if script resources must be combined; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("AllowedExensions", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedExensionsDescription", Title = "AllowedExensionsCaption")]
    public bool? AllowedExensions
    {
      get => (bool?) this[nameof (AllowedExensions)];
      set => this[nameof (AllowedExensions)] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropNames
    {
      public const string AllowedExensionsSettings = "AllowedExensionsSettings";
      public const string AllowedMaxDocumentSize = "AllowedMaxDocumentSize";
      public const string AllowedExensions = "AllowedExensions";
    }
  }
}
