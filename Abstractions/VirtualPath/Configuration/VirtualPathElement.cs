// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.Configuration.VirtualPathElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Abstractions.VirtualPath.Configuration
{
  /// <summary>
  /// A configuration element that describes a virtual path defintion.
  /// </summary>
  public class VirtualPathElement : ConfigElement
  {
    public VirtualPathElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal VirtualPathElement()
      : base(false)
    {
    }

    /// <summary>Gets or sets the virtual path to a unique resource.</summary>
    [ConfigurationProperty("virtualPath", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string VirtualPath
    {
      get => (string) this["virtualPath"];
      set => this["virtualPath"] = (object) value;
    }

    /// <summary>
    /// Describes the path to the resource. The content may be virtually anything - an embedded assembly resource, external URL, database record, etc.
    /// </summary>
    /// <example>
    /// <list type="">
    /// <item>Telerik.Sitefinity.Resources.Templates.Backend.Dashboard.ascx, Telerik.Sitefinity.Resources</item>
    /// <item>http://www.server.com/images/print.gif</item>
    /// <item>SELECT ImageBlob FROM tblIcon WHERE IconID = 5</item>
    /// </list>
    /// </example>
    [ConfigurationProperty("resourceLocation", DefaultValue = "", IsRequired = true)]
    public string ResourceLocation
    {
      get => (string) this["resourceLocation"];
      set => this["resourceLocation"] = (object) value;
    }

    /// <summary>
    /// The name of the resolver used by the virtual path provider to retrieve the resource.
    /// </summary>
    /// <remarks>
    /// To use the default embedded resources resolver specify "Embedded".
    /// </remarks>
    [ConfigurationProperty("resolverName", DefaultValue = "Embedded", IsRequired = true)]
    public string ResolverName
    {
      get => (string) this["resolverName"];
      set => this["resolverName"] = (object) value;
    }

    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("resolverType", IsRequired = false)]
    public Type ResolverType
    {
      get => (Type) this["resolverType"];
      set => this["resolverType"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of user-defined parameters for the provider.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }
  }
}
