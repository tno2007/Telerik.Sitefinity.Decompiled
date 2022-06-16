// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions.FormsDetailViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions
{
  /// <summary>
  /// Definition for the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView" />.
  /// </summary>
  public class FormsDetailViewDefinition : 
    ContentViewDetailDefinition,
    IFormsDetailViewDefinition,
    IContentViewDetailDefinition,
    IContentViewDefinition,
    IDefinition
  {
    private string webServiceBaseUrl;
    private string formName;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions.DetailFormViewDefinition" /> class.
    /// </summary>
    public FormsDetailViewDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Definitions.DetailFormViewDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public FormsDetailViewDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets or sets the base url of the web service.</summary>
    /// <value></value>
    public string WebServiceBaseUrl
    {
      get => this.ResolveProperty<string>(nameof (WebServiceBaseUrl), this.webServiceBaseUrl);
      set => this.webServiceBaseUrl = value;
    }

    /// <summary>Gets or sets the name of the form to show.</summary>
    /// <value></value>
    public string FormName
    {
      get => this.ResolveProperty<string>(nameof (FormName), this.formName);
      set => this.formName = value;
    }
  }
}
