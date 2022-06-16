// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Config.PageUrlMirrorTextFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Config
{
  /// <summary>
  /// The configuration element for Page URL mirror text field.
  /// </summary>
  public class PageUrlMirrorTextFieldElement : 
    UrlMirrorTextFieldElement,
    IPageUrlMirrorTextFieldDefinition
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public PageUrlMirrorTextFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the custom URL validation message.</summary>
    [ConfigurationProperty("customUrlValidationMessage")]
    [ObjectInfo(typeof (PageResources), Description = "CustomUrlValidationMessageDescription", Title = "CustomUrlValidationMessageTitle")]
    public string CustomUrlValidationMessage
    {
      get => (string) this["customUrlValidationMessage"];
      set => this["customUrlValidationMessage"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (PageUrlMirrorTextField);

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new PageUrlMirrorTextFieldDefinition((ConfigElement) this);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal new struct PropertyNames
    {
      public const string CustomUrlValidationMessage = "customUrlValidationMessage";
    }
  }
}
