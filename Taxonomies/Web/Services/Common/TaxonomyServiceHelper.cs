// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.Common.TaxonomyServiceHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Taxonomies.Web.Services.Common
{
  /// <summary>Helper class used by the taxonomy WCF service.</summary>
  public static class TaxonomyServiceHelper
  {
    internal static WcfTaxonomy GetWcfTaxonmyObject(ITaxonomy taxonomy)
    {
      WcfTaxonomy wcfTaxonmyObject = new WcfTaxonomy();
      wcfTaxonmyObject.Id = taxonomy.Id;
      wcfTaxonmyObject.Title = (string) taxonomy.Title;
      wcfTaxonmyObject.Name = taxonomy.Name;
      wcfTaxonmyObject.EditUrl = TaxonomyManager.GetTaxonomyEditUrl(taxonomy);
      wcfTaxonmyObject.Description = (string) taxonomy.Description;
      wcfTaxonmyObject.UserFriendlyType = TaxonomyManager.GetTaxonomyUserFriendlyName(taxonomy);
      wcfTaxonmyObject.Type = taxonomy.GetType().Name;
      wcfTaxonmyObject.CssClass = TaxonomyManager.GetTaxonomyCssClass(taxonomy);
      wcfTaxonmyObject.SingleItemName = (string) taxonomy.TaxonName;
      wcfTaxonmyObject.FullTypeName = taxonomy.GetType().AssemblyQualifiedName;
      wcfTaxonmyObject.FullName = taxonomy.GetType().FullName;
      wcfTaxonmyObject.IsBuiltIn = TaxonomyManager.IsTaxonomyBuiltIn(taxonomy);
      if (taxonomy is ILocalizable)
        wcfTaxonmyObject.AvailableLanguages = (taxonomy as ILocalizable).AvailableLanguages;
      wcfTaxonmyObject.AdditionalStatus = StatusResolver.Resolve(taxonomy.GetType(), taxonomy.GetProviderName(), taxonomy.Id);
      return wcfTaxonmyObject;
    }

    /// <summary>Sets the shared sites count.</summary>
    /// <param name="taxonomyManager">The taxonomy manager.</param>
    /// <param name="taxonomy">The taxonomy.</param>
    /// <param name="wcfTaxonomy">The WCF taxonomy.</param>
    internal static int SetSharedSitesCount(
      TaxonomyManager taxonomyManager,
      ITaxonomy taxonomy,
      bool skipSiteContext)
    {
      return skipSiteContext ? taxonomyManager.GetRelatedSites(taxonomy).Count<ISite>() : taxonomyManager.GetRelatedSitesInContext(taxonomy).Count<ISite>();
    }
  }
}
