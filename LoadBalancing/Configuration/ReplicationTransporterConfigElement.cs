// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationTransporterConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.LoadBalancing.Configuration
{
  /// <summary>Represents information for creating transporters.</summary>
  public class ReplicationTransporterConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationTransporterConfigElement" /> class.
    /// </summary>
    internal ReplicationTransporterConfigElement()
      : base(false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.LoadBalancing.Configuration.ReplicationTransporterConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The element parent.</param>
    public ReplicationTransporterConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the type name of transporter.</summary>
    /// <value>The value.</value>
    [ConfigurationProperty("TypeName", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ReplicationSyncConfigDescriptions), Description = "TransporterTypeDescription", Title = "TransporterTypeCaption")]
    public string TypeName
    {
      get => (string) this[nameof (TypeName)];
      set => this[nameof (TypeName)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a collection of user-defined parameters for the transporter.
    /// </summary>
    /// <value>The value.</value>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }
  }
}
