// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.IOrganizationFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent
{
  public interface IOrganizationFacade<TParentFacade> where TParentFacade : class
  {
    bool ReturnSuccess();

    bool SaveChanges();

    bool CancelChanges();

    IOrganizationFacade<TParentFacade> SaveAndContinue();

    IOrganizationFacade<TParentFacade> CancelAndContinue();

    TParentFacade Done();

    IOrganizationFacade<TParentFacade> AddTaxa(
      string taxaPropertyName,
      IEnumerable<Guid> taxonIds);

    IOrganizationFacade<TParentFacade> AddTaxa(
      string taxaPropertyName,
      IEnumerable<ITaxon> taxa);

    IOrganizationFacade<TParentFacade> RemoveTaxa(
      string taxaPropertyName,
      IEnumerable<Guid> taxonIds);

    IOrganizationFacade<TParentFacade> RemoveTaxa(
      string taxaPropertyName,
      IEnumerable<ITaxon> taxa);

    IOrganizationFacade<TParentFacade> SetTaxon(
      string taxonPropertyName,
      Guid taxonId);

    IOrganizationFacade<TParentFacade> SetTaxon(
      string taxonPropertyName,
      ITaxon taxon);

    IOrganizationFacade<TParentFacade> Clear(string taxaPropertyName);

    IOrganizationFacade<TParentFacade> Exists(
      string taxaPropertyName,
      Guid taxonId,
      out bool exists);

    IOrganizationFacade<TParentFacade> Exists(
      string taxaPropertyName,
      ITaxon taxon,
      out bool exists);
  }
}
