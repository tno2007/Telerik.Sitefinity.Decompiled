// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Definitions.UrlMirrorTextFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A class that provides all information needed to construct mirror text field.
  /// </summary>
  public class UrlMirrorTextFieldDefinition : MirrorTextFieldDefinition
  {
    private string urlControlId;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public UrlMirrorTextFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public UrlMirrorTextFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (UrlMirrorTextField);

    /// <summary>
    /// Gets the pageId of the hierarchical field control which is used to generate the URL prefix.
    /// </summary>
    /// <value>The pageId of the hierarchical field control which is used to generate the URL prefix.</value>
    public string UrlControlId
    {
      get => this.ResolveProperty<string>(nameof (UrlControlId), this.urlControlId);
      set => this.urlControlId = value;
    }
  }
}
