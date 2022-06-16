// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Config.SiteSelectorFieldElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI.Definitions;
using Telerik.Sitefinity.Web.UI.Fields.Contracts;
using Telerik.Sitefinity.Web.UI.Fields.Definitions;

namespace Telerik.Sitefinity.Web.UI.Fields.Config
{
  /// <summary>The configuration element for site selector fields.</summary>
  public class SiteSelectorFieldElement : 
    FieldControlDefinitionElement,
    ISiteSelectorFieldDefinition,
    IFieldControlDefinition,
    IFieldDefinition,
    IDefinition
  {
    private List<IChoiceDefinition> siteSelectorItems;

    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public SiteSelectorFieldElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new SiteSelectorDefinition((ConfigElement) this);

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ChoiceElement" /> objects.
    /// </summary>
    /// <value>The collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Config.ChoiceElement" /> objects.</value>
    [ConfigurationProperty("siteSelectorConfig")]
    [ConfigurationCollection(typeof (ChoiceElement), AddItemName = "element")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ChoicesConfigDescription", Title = "ChoicesConfigTitle")]
    public ConfigElementList<SiteSelectorFieldElement> SiteSelectorConfig => (ConfigElementList<SiteSelectorFieldElement>) this["siteSelectorConfig"];

    /// <summary>
    /// Gets or sets whether the site languages selector should be visible.
    /// </summary>
    /// <value>If set to <c>true</c> the site selector will be visible.</value>
    [ConfigurationProperty("showSiteLanguageSelector")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShowSiteLanguageDescription", Title = "ShowSiteLanguageCaption")]
    public bool ShowSiteLanguageSelector
    {
      get => (bool) this["showSiteLanguageSelector"];
      set => this["showSiteLanguageSelector"] = (object) value;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Contracts.IChoiceDefinition" /> objects, representing the items
    /// that the control ought to render.
    /// </summary>
    /// <value></value>
    public List<IChoiceDefinition> SiteSeletorItems
    {
      get
      {
        if (this.siteSelectorItems == null)
          this.siteSelectorItems = this.SiteSelectorConfig.Elements.Select<SiteSelectorFieldElement, IChoiceDefinition>((Func<SiteSelectorFieldElement, IChoiceDefinition>) (ch => (IChoiceDefinition) ch.ToDefinition())).ToList<IChoiceDefinition>();
        return this.siteSelectorItems;
      }
    }
  }
}
