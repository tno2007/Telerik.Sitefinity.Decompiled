// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.AssemblyGroupElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// Configuration element which represents a group of persistent assemblies that ought to be loaded
  /// together for a given connection name.
  /// </summary>
  public class AssemblyGroupElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Configuration.AssemblyGroupElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public AssemblyGroupElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the name of the connection string.</summary>
    /// <value>The name of the connection string.</value>
    [ConfigurationProperty("connectionStringName", DefaultValue = "Sitefinity", IsKey = true, IsRequired = true)]
    public string ConnectionStringName
    {
      get => (string) this["connectionStringName"];
      set => this["connectionStringName"] = (object) value;
    }

    /// <summary>
    /// Gets the collection of referenced assemblies within this assembly group.
    /// </summary>
    /// <value>Referenced assemblies.</value>
    [ConfigurationProperty("assemblies")]
    [ConfigurationCollection(typeof (ReferenceAssembly))]
    public ConfigElementDictionary<string, ReferenceAssembly> Assemblies => (ConfigElementDictionary<string, ReferenceAssembly>) this["assemblies"];
  }
}
