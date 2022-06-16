// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.LabelDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Diagnostics;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI
{
  /// <summary>
  /// Cached version of LabelDefinitionElement. Stores information about resource labels (a resClassId, msgKey pair)
  /// </summary>
  [DebuggerDisplay("LabelDefinition, ({ClassId}, {MessageKey})")]
  public class LabelDefinition : DefinitionBase, ILabelDefinition, IDefinition
  {
    private string compoundKey;
    private string classId;
    private string messageKey;

    /// <summary>Creates a blank label definition</summary>
    public LabelDefinition()
      : base((ConfigElement) null)
    {
    }

    /// <summary>
    /// Caches a config element (LabelDefinitionElement) for faster access
    /// </summary>
    /// <param name="config">An instance of LabelDefinitionElement to cache</param>
    public LabelDefinition(ConfigElement config)
      : base(config)
    {
    }

    /// <summary>
    /// Must be unique within the parent collection. <c>ClassId</c> + <c>MessageKey</c> is a reasonable default.
    /// </summary>
    public string CompoundKey
    {
      get => this.ResolveProperty<string>(nameof (CompoundKey), this.compoundKey);
      set => this.compoundKey = value;
    }

    /// <summary>Resource class ID. E.g. 'Taxonomies', 'Labels', etc.</summary>
    public string ClassId
    {
      get => this.ResolveProperty<string>(nameof (ClassId), this.classId);
      set => this.classId = value;
    }

    /// <summary>
    /// ID of the label withing the resource class specified by <c>ClassId</c>.
    /// </summary>
    public string MessageKey
    {
      get => this.ResolveProperty<string>(nameof (MessageKey), this.messageKey);
      set => this.messageKey = value;
    }
  }
}
