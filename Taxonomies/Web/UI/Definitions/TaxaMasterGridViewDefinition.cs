// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.TaxaMasterGridViewDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Definitions;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Taxonomies.Web.UI.Definitions
{
  public class TaxaMasterGridViewDefinition : 
    MasterGridViewDefinition,
    ITaxaMasterViewDefinition,
    IMasterViewDefinition,
    IContentViewMasterDefinition,
    IContentViewDefinition,
    IDefinition
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.TaxaMasterGridViewDefinition" /> class.
    /// </summary>
    public TaxaMasterGridViewDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.TaxaMasterGridViewDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public TaxaMasterGridViewDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }
  }
}
