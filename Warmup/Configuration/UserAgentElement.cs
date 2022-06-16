// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Warmup.Configuration.UserAgentElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Warmup.Configuration
{
  /// <summary>Defines the user-agent configuration element.</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Configuration.ConfigElement" />
  internal class UserAgentElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Warmup.Configuration.UserAgentElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public UserAgentElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the user agent.</summary>
    /// <value>The name of the user agent.</value>
    [ConfigurationProperty("name", IsKey = true)]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the user agent string.</summary>
    /// <value>The user agent string.</value>
    [ConfigurationProperty("value")]
    public string Value
    {
      get => (string) this["value"];
      set => this[nameof (value)] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropNames
    {
      internal const string Name = "name";
      internal const string Value = "value";
    }
  }
}
