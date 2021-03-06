// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Definitions.PageUrlMirrorTextFieldDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Definitions
{
  /// <summary>
  /// A class that provides all information needed to construct page url mirror text field.
  /// </summary>
  public class PageUrlMirrorTextFieldDefinition : 
    UrlMirrorTextFieldDefinition,
    IPageUrlMirrorTextFieldDefinition
  {
    private string customUrlValidationMessage;

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ColumnDefinition" /> class.
    /// </summary>
    public PageUrlMirrorTextFieldDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:ViewModeDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public PageUrlMirrorTextFieldDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the default type of the field control.</summary>
    /// <value>The default type of the field control.</value>
    public override Type DefaultFieldType => typeof (PageUrlMirrorTextField);

    /// <summary>Gets or sets the custom URL validation message.</summary>
    public string CustomUrlValidationMessage
    {
      get => this.ResolveProperty<string>(nameof (CustomUrlValidationMessage), this.customUrlValidationMessage);
      set => this.customUrlValidationMessage = value;
    }
  }
}
