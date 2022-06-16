// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.FormsDetailViewElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config
{
  /// <summary>
  /// Configuration element for the <see cref="!:FormsDetailView" /> view.
  /// </summary>
  public class FormsDetailViewElement : 
    ContentViewDetailElement,
    IFormsDetailViewDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Config.DetailFormViewElement" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public FormsDetailViewElement(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new FormsDetailViewDefinition((ConfigElement) this);

    /// <summary>
    /// Gets or sets the ID of the page that should display the details view.
    /// If this property is not set the current page is assumed.
    /// </summary>
    /// <value>The master page pageId.</value>
    [ConfigurationProperty("webServiceBaseUrl", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "WebServiceBaseUrlDescription", Title = "WebServiceBaseUrlCaption")]
    public string WebServiceBaseUrl
    {
      get => (string) this["webServiceBaseUrl"];
      set => this["webServiceBaseUrl"] = (object) value;
    }

    /// <summary>Gets or sets the name of the form to show.</summary>
    /// <value></value>
    [ConfigurationProperty("formName", IsRequired = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "FormNameDescription", Title = "FormNameCaption")]
    public string FormName
    {
      get => (string) this["formName"];
      set => this["formName"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string WebServiceBaseUrl = "webServiceBaseUrl";
      public const string FormName = "formName";
    }
  }
}
