// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.ReferenceAssembly
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// The configuration element which specifies a particular assembly that
  /// ought to be referenced with a given connection name.
  /// </summary>
  public class ReferenceAssembly : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Configuration.ReferenceAssembly" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public ReferenceAssembly(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the full name of the assembly.</summary>
    /// <value>The full name of the assembly.</value>
    [ConfigurationProperty("fullName", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string FullName
    {
      get => (string) this["fullName"];
      set => this["fullName"] = (object) value;
    }
  }
}
