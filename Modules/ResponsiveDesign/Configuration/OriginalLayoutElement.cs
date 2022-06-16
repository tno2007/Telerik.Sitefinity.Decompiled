// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.OriginalLayoutElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration
{
  /// <summary>
  /// Represents the original layout that will be replaced with another
  /// layout in the mobile scenarios.
  /// </summary>
  public class OriginalLayoutElement : LayoutElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public OriginalLayoutElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.ResponsiveDesign.Configuration.OriginalLayoutElement" /> class.
    /// </summary>
    internal OriginalLayoutElement()
    {
    }

    /// <summary>
    /// Gets a collection of alternate layouts that can be used to replace
    /// original layout element.
    /// </summary>
    [DescriptionResource(typeof (ResponsiveDesignResources), "AlternateLayoutsConfig")]
    [ConfigurationProperty("alternateLayouts")]
    public virtual ConfigElementDictionary<string, LayoutElement> AlternateLayouts => (ConfigElementDictionary<string, LayoutElement>) this["alternateLayouts"];

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal new struct Properties
    {
      public const string AlternateLayouts = "alternateLayouts";
    }
  }
}
