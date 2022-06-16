// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Definitions.SiteSelectorDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Config;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;

namespace Telerik.Sitefinity.Web.UI.Fields.Definitions
{
  public class SiteSelectorDefinition : 
    FieldControlDefinition,
    ISiteSelectorFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private List<IChoiceDefinition> siteSelectorItems;
    private bool showSiteLanguageSelector;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.SiteSelectorDefinition" /> class.
    /// </summary>
    public SiteSelectorDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Definitions.SiteSelectorDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public SiteSelectorDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceDefinition" /> objects, representing the items
    /// that the control ought to render.
    /// </summary>
    public List<IChoiceDefinition> SiteSeletorItems
    {
      get
      {
        if (this.siteSelectorItems == null)
          this.siteSelectorItems = ((IEnumerable<IChoiceDefinition>) ((SiteSelectorFieldElement) this.ConfigDefinition).SiteSelectorConfig.Elements.Select<SiteSelectorFieldElement, ChoiceDefinition>((Func<SiteSelectorFieldElement, ChoiceDefinition>) (c => new ChoiceDefinition((ConfigElement) c)))).ToList<IChoiceDefinition>();
        return this.siteSelectorItems;
      }
    }

    /// <summary>
    /// Gets or sets whether the site language selector should be visible.
    /// </summary>
    public bool ShowSiteLanguageSelector
    {
      get => this.ResolveProperty<bool>(nameof (ShowSiteLanguageSelector), this.showSiteLanguageSelector, false);
      set => this.showSiteLanguageSelector = value;
    }
  }
}
