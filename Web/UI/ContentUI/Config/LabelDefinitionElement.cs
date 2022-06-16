// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Config.LabelDefinitionElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Config
{
  /// <summary>
  /// Configuration elemnent for storing a label information (e.g. 'Labels', 'MoreItems')
  /// </summary>
  [DebuggerDisplay("LabelDefinitionElement, ({ClassId}, {MessageKey})")]
  [ObjectInfo(typeof (ConfigDescriptions), Description = "LabelDefinitionElementDescription", Title = "LabelDefinitionElementTitle")]
  public class LabelDefinitionElement : DefinitionConfigElement, ILabelDefinition, IDefinition
  {
    /// <summary>
    /// Creates a new instance of the config element by setting up a parent collection element
    /// </summary>
    /// <param name="parent">Parent config collection element</param>
    public LabelDefinitionElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Must be unique within the parent collection. <c>ClassId</c> + <c>MessageKey</c> is a reasonable default.
    /// </summary>
    [ConfigurationProperty("compoundKey", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CompoundKeyDescription", Title = "CompoundKeyTitle")]
    public string CompoundKey
    {
      get => (string) this["compoundKey"];
      set => this["compoundKey"] = (object) value;
    }

    /// <summary>Resource class ID. E.g. 'Taxonomies', 'Labels', etc.</summary>
    [ConfigurationProperty("classId", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ClassIdDescription", Title = "ClassIdTitle")]
    public string ClassId
    {
      get => (string) this["classId"];
      set => this["classId"] = (object) value;
    }

    /// <summary>
    /// ID of the label withing the resource class specified by <c>ClassId</c>.
    /// </summary>
    [ConfigurationProperty("messageKey", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MessageKeyDescription", Title = "MessageKeyTitle")]
    public string MessageKey
    {
      get => (string) this["messageKey"];
      set => this["messageKey"] = (object) value;
    }

    /// <summary>
    /// Creates an instance of a cached definition that does not read from the configuration and is instantiated by the values of this config element
    /// </summary>
    /// <returns>Cached version of <c>LabelDefinitionElelemtn</c></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new LabelDefinition((ConfigElement) this);
  }
}
