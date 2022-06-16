// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Config.UrlMirrorTextFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Definitions;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields.Config;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI.Fields.Config
{
  /// <summary>The configuration element for URL mirror text fields.</summary>
  public class UrlMirrorTextFieldElement : MirrorTextFieldElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public UrlMirrorTextFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition instance.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new UrlMirrorTextFieldDefinition((ConfigElement) this);

    /// <summary>
    /// Gets the pageId of the hierarchical field control which is used to generate the URL prefix.
    /// </summary>
    /// <value>The pageId of the hierarchical field control which is used to generate the URL prefix.</value>
    [ConfigurationProperty("urlControlId")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UrlControlIdDescription", Title = "UrlControlIdTitle")]
    public string UrlControlId
    {
      get => (string) this["urlControlId"];
      set => this["urlControlId"] = (object) value;
    }

    /// <summary>Gets the default type of the field.</summary>
    /// <value>The default type of the field.</value>
    public override Type DefaultFieldType => typeof (UrlMirrorTextField);

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropertyNamesUrl
    {
      public const string UrlControlId = "urlControlId";
    }
  }
}
