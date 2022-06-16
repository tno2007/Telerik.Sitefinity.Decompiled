// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.TextTransformationSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>
  /// Represents a class for storing regex transformations for urls from titles on client/server side.
  /// </summary>
  public class TextTransformationSettings : ConfigElement, ITextTransformationSettings
  {
    public TextTransformationSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the expression for filtering the value of the mirror text field.
    /// </summary>
    /// <value>The filter expression of the mirror text field.</value>
    [ConfigurationProperty("regularExpressionFilter")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RegularExpressionFilterDescription", Title = "RegularExpressionFilterTitle")]
    public string RegularExpressionFilter
    {
      get => (string) this["regularExpressionFilter"];
      set => this["regularExpressionFilter"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the value that will be replaced for every Regular expression filter match.
    /// </summary>
    /// <value>The value to replace with.</value>
    [ConfigurationProperty("replaceWith")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ReplaceWithDescription", Title = "ReplaceWithTitle")]
    public string ReplaceWith
    {
      get => (string) this["replaceWith"];
      set => this["replaceWith"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to lower the
    /// the value of the control.
    /// </summary>
    [ConfigurationProperty("toLower", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ToLowerDescription", Title = "ToLowerTitle")]
    public bool ToLower
    {
      get => (bool) this["toLower"];
      set => this["toLower"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to trim the value of this control.
    /// </summary>
    [ConfigurationProperty("trim", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TrimDescription", Title = "TrimTitle")]
    public bool Trim
    {
      get => (bool) this["trim"];
      set => this["trim"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNames
    {
      public const string RegularExpressionFilter = "regularExpressionFilter";
      public const string ReplaceWith = "replaceWith";
      public const string ToLower = "toLower";
      public const string Trim = "trim";
    }
  }
}
