// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.UI.TaxonDetailFormView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Taxonomies.Web.Services;
using Telerik.Sitefinity.Taxonomies.Web.UI.Definitions.Contracts;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.Contracts;

namespace Telerik.Sitefinity.Taxonomies.Web.UI
{
  /// <summary>
  /// Customizes <see cref="T:Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView" /> to for editing taxa
  /// </summary>
  public class TaxonDetailFormView : DetailFormView
  {
    private ITaxonomy taxonomy;
    private Guid? taxonomyId;
    private Type taxonType;
    private float? ordinal;
    private Guid? taxonId;
    private bool? skipSiteContext;
    internal const string taxonDetailFormViewScript = "Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.TaxonDetailFormView.js";

    /// <summary>Gets the taxonomy id.</summary>
    /// <value>The taxonomy id.</value>
    protected virtual Guid? TaxonomyId
    {
      get
      {
        Guid result;
        if (!this.taxonomyId.HasValue && Guid.TryParse(this.Page.Request.QueryString[nameof (TaxonomyId)], out result))
          this.taxonomyId = new Guid?(result);
        return this.taxonomyId;
      }
    }

    /// <summary>Gets the type of the taxon.</summary>
    /// <value>The type of the taxon.</value>
    protected virtual Type TaxonType
    {
      get
      {
        if (this.taxonType == (Type) null)
        {
          string name = this.Page.Request.QueryString[nameof (TaxonType)];
          if (!string.IsNullOrEmpty(name))
            this.taxonType = TypeResolutionService.ResolveType(name, false, true);
        }
        return this.taxonType;
      }
    }

    /// <summary>Gets the ordinal.</summary>
    /// <value>The ordinal.</value>
    protected virtual float? Ordinal
    {
      get
      {
        float result;
        if (!this.ordinal.HasValue && float.TryParse(this.Page.Request.QueryString[nameof (Ordinal)], out result))
          this.ordinal = new float?(result);
        return this.ordinal;
      }
    }

    /// <summary>Gets the initially selected taxon ID</summary>
    protected virtual Guid? TaxonId
    {
      get
      {
        Guid result;
        if (!this.taxonId.HasValue && Guid.TryParse(this.Page.Request.QueryString[nameof (TaxonId)], out result))
          this.taxonId = new Guid?(result);
        return this.taxonId;
      }
    }

    /// <summary>
    /// Gets the flag that indicates whether a taxonomy should be resolved for the current site.
    /// </summary>
    /// <value>The taxonomy id.</value>
    protected virtual bool? SkipSiteContext
    {
      get
      {
        if (!this.skipSiteContext.HasValue)
        {
          bool result;
          this.skipSiteContext = !bool.TryParse(this.Page.Request.QueryString["skipSiteContext"], out result) ? new bool?(false) : new bool?(result);
        }
        return this.skipSiteContext;
      }
    }

    /// <summary>
    /// Create a blank data item. When bound on the client, this item will be used to construct
    /// the JS object sent back to the server.
    /// </summary>
    /// <returns>Blank data item</returns>
    protected override object CreateBlankDataItem()
    {
      object blankDataItem1 = base.CreateBlankDataItem();
      if (!(blankDataItem1 is WcfHierarchicalTaxon blankDataItem2) || !this.TaxonId.HasValue || !(this.TaxonType != (Type) null))
        return blankDataItem1;
      blankDataItem2.ParentTaxonId = this.TaxonId;
      return (object) blankDataItem2;
    }

    /// <summary>Gets the current taxonomy.</summary>
    /// <value>The current taxonomy.</value>
    protected virtual ITaxonomy CurrentTaxonomy
    {
      get
      {
        if (this.taxonomy == null && this.TaxonomyId.HasValue)
          this.taxonomy = TaxonomyManager.GetManager(this.Host.ControlDefinition.ProviderName).GetTaxonomy(this.TaxonomyId.Value);
        return this.taxonomy;
      }
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The container.</param>
    /// <param name="definition">The definition.</param>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (definition is IDetailFormViewDefinition formViewDefinition)
      {
        if (this.CurrentTaxonomy != null)
        {
          string name = typeof (TaxonomyResources).Name;
          string title = formViewDefinition.Title;
          formViewDefinition.Title = Res.Get(name, title).Arrange((object) HttpUtility.HtmlEncode(this.CurrentTaxonomy.TaxonName.ToLower()));
          string alternativeTitle = formViewDefinition.AlternativeTitle;
          formViewDefinition.AlternativeTitle = Res.Get(name, alternativeTitle).Arrange((object) HttpUtility.HtmlEncode(this.CurrentTaxonomy.TaxonName.ToLower()));
          formViewDefinition.ResourceClassId = string.Empty;
        }
        formViewDefinition.DoNotUseContentItemContext = true;
        IContentViewSectionDefinition sectionDefinition = formViewDefinition.Sections.SingleOrDefault<IContentViewSectionDefinition>((Func<IContentViewSectionDefinition, bool>) (s => s.Name == "MainSection"));
        if (sectionDefinition != null)
        {
          IHierarchicalTaxonParentSelectorFieldDefinition selectorFieldDefinition = sectionDefinition.Fields.OfType<IHierarchicalTaxonParentSelectorFieldDefinition>().Where<IHierarchicalTaxonParentSelectorFieldDefinition>((Func<IHierarchicalTaxonParentSelectorFieldDefinition, bool>) (f => f.ID == "parentSelectorField")).SingleOrDefault<IHierarchicalTaxonParentSelectorFieldDefinition>();
          if (selectorFieldDefinition != null)
            selectorFieldDefinition.TaxonomyId = this.CurrentTaxonomy.Id;
        }
      }
      base.InitializeControls(container, definition);
    }

    /// <summary>Determines the service URL.</summary>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="definition">The definition.</param>
    protected override void DetermineServiceUrl(
      Type contentType,
      IDetailFormViewDefinition definition)
    {
      string empty = string.Empty;
      this.ServiceUrl = string.Format((string.IsNullOrEmpty(definition.WebServiceBaseUrl) ? "~/Sitefinity/Services/Taxonomies/HierarchicalTaxon.svc/" : definition.WebServiceBaseUrl) + "{0}/?ordinal=&insertionPosition={1}&itemType={2}&skipSiteContext={3}", (object) this.TaxonomyId, (object) this.Ordinal, (object) this.TaxonType, (object) this.SkipSiteContext);
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>();
      controlDescriptor.Type = typeof (TaxonDetailFormView).FullName;
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    /// <inheritdoc />
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Taxonomies.Web.UI.Scripts.TaxonDetailFormView.js", typeof (TaxonDetailFormView).Assembly.FullName)
    };
  }
}
