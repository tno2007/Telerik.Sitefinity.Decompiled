// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Configuration.ByteRangeConfigElement
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
  /// Provides configuration information about the byte range serving
  /// </summary>
  public class ByteRangeConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Configuration.ByteRangeConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ByteRangeConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Enables byte range serving for the Sitefinity build in <see cref="!:LibraryHttpHandler" />.
    /// </summary>
    [ConfigurationProperty("enabled", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnabledByteRangeDescription", Title = "EnabledByteRangeTitle")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropNames
    {
      public const string Enabled = "enabled";
    }
  }
}
