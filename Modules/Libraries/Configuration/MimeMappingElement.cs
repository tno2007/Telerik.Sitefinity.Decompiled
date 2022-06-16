// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.MimeMappingElement
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
  /// Represents mime mapping configuration element which will map file extension and mime mapping type.
  /// </summary>
  public class MimeMappingElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.MimeMappingElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public MimeMappingElement(ConfigElement parent)
      : base(parent)
    {
    }

    [ObjectInfo(typeof (ConfigDescriptions), Description = "FileExtensionDescription", Title = "FileExtensionTitle")]
    [ConfigurationProperty("fileExtension", IsKey = true, IsRequired = true)]
    public string FileExtension
    {
      get => (string) this["fileExtension"];
      set => this["fileExtension"] = (object) value;
    }

    /// <summary>Gets or sets the mime type</summary>
    /// <value>The the mime type.</value>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MimeTypeDescription", Title = "MimeTypeTitle")]
    [ConfigurationProperty("mimeType", IsRequired = true)]
    public string MimeType
    {
      get => (string) this["mimeType"];
      set => this["mimeType"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string MimeType = "mimeType";
      public const string FileExtension = "fileExtension";
    }
  }
}
