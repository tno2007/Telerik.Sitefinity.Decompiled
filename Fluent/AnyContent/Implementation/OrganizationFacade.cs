// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.AnyContent.Implementation.OrganizationFacade`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Fluent.AnyContent.Implementation
{
  public class OrganizationFacade<TParentFacade> : IOrganizationFacade<TParentFacade>
    where TParentFacade : class
  {
    private AppSettings settings;
    private TParentFacade parentFacade;

    public bool ReturnSuccess() => true;

    public bool SaveChanges() => AllFacadesHelper.SaveChanges(this.settings);

    public bool CancelChanges() => AllFacadesHelper.CancelChanges(this.settings);

    public IOrganizationFacade<TParentFacade> SaveAndContinue()
    {
      this.SaveChanges();
      return (IOrganizationFacade<TParentFacade>) this;
    }

    public IOrganizationFacade<TParentFacade> CancelAndContinue()
    {
      this.CancelChanges();
      return (IOrganizationFacade<TParentFacade>) this;
    }

    public TParentFacade Done()
    {
      FacadeHelper.AssertNotNull<TParentFacade>(this.parentFacade, "Parent facade must not be null when calling Done()");
      return this.parentFacade;
    }

    public IOrganizationFacade<TParentFacade> AddTaxa(
      string taxaPropertyName,
      IEnumerable<Guid> taxonIds)
    {
      throw new NotImplementedException();
    }

    public IOrganizationFacade<TParentFacade> AddTaxa(
      string taxaPropertyName,
      IEnumerable<ITaxon> taxa)
    {
      throw new NotImplementedException();
    }

    public IOrganizationFacade<TParentFacade> RemoveTaxa(
      string taxaPropertyName,
      IEnumerable<Guid> taxonIds)
    {
      throw new NotImplementedException();
    }

    public IOrganizationFacade<TParentFacade> RemoveTaxa(
      string taxaPropertyName,
      IEnumerable<ITaxon> taxa)
    {
      throw new NotImplementedException();
    }

    public IOrganizationFacade<TParentFacade> SetTaxon(
      string taxonPropertyName,
      Guid taxonId)
    {
      throw new NotImplementedException();
    }

    public IOrganizationFacade<TParentFacade> SetTaxon(
      string taxonPropertyName,
      ITaxon taxon)
    {
      throw new NotImplementedException();
    }

    public IOrganizationFacade<TParentFacade> Clear(string taxaPropertyName) => throw new NotImplementedException();

    public IOrganizationFacade<TParentFacade> Exists(
      string taxaPropertyName,
      Guid taxonId,
      out bool exists)
    {
      throw new NotImplementedException();
    }

    public IOrganizationFacade<TParentFacade> Exists(
      string taxaPropertyName,
      ITaxon taxon,
      out bool exists)
    {
      throw new NotImplementedException();
    }
  }
}
