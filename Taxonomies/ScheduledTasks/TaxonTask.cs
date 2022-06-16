// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Script.Serialization;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.Security;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Taxonomies.ScheduledTasks
{
  /// <summary>Defines base task for classifications actions tasks</summary>
  internal abstract class TaxonTask : ScheduledTask
  {
    private int itemsCount;
    private int currentIndex;
    private const int BatchSize = 1000;
    private const string SourceNotFoundErrorTemplate = "Source taxon {0} not found.";
    private const string TargetNotFoundErrorTemplate = "Target taxon {0} not found.";
    private const string UnknowErrorTemplate = "Unknown error when updating taxon {0}.";
    private const string PropertyNotFoundErrorTemplate = "Property for type: {0} and taxon {1} not found.";
    private List<TaxonTask.TaxonStatus> taxonStatusList = new List<TaxonTask.TaxonStatus>();

    public TaxonTask()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.ScheduledTasks.TaxonTask" /> class.
    /// </summary>
    /// <param name="taxonomy">Parent taxonomy of taxa that task is created for</param>
    public TaxonTask(ITaxonomy taxonomy)
    {
      this.ExecuteTime = DateTime.UtcNow;
      this.Serializer = new JavaScriptSerializer();
      if (taxonomy == null)
        return;
      this.TaxonomySingularName = taxonomy.TaxonName.GetString(SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage, true);
      this.TaxonomyPluralName = taxonomy.Title.GetString(SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage, true);
    }

    /// <summary>
    /// Gets or sets the ids of the taxon items that are to be moved.
    /// </summary>
    public List<TaxonTaskStateItem> SourceTaxa { get; set; }

    /// <summary>Gets or sets the id of the target taxon.</summary>
    public TaxonTaskStateItem TargetTaxon { get; set; }

    /// <summary>Gets or sets the singular name of the taxonomy.</summary>
    public string TaxonomySingularName { get; set; }

    /// <summary>Gets or sets the plural name of the taxonomy.</summary>
    public string TaxonomyPluralName { get; set; }

    internal List<TaxonTask.TaxonStatus> TaxonStatusList
    {
      get => this.taxonStatusList;
      set => this.taxonStatusList = value;
    }

    internal JavaScriptSerializer Serializer { get; set; }

    /// <inheritdoc />
    public override void SetCustomData(string customData)
    {
      TaxonTaskState taxonTaskState = this.Serializer.Deserialize<TaxonTaskState>(customData);
      this.SourceTaxa = taxonTaskState.SourceTaxa;
      this.TargetTaxon = taxonTaskState.TargetTaxon;
      this.TaxonomySingularName = taxonTaskState.TaxonomySingularName;
      this.TaxonomyPluralName = taxonTaskState.TaxonomyPluralName;
    }

    /// <inheritdoc />
    public override string BuildUniqueKey() => this.GetCustomData().ComputeSha256Hash();

    /// <inheritdoc />
    public override string GetCustomData() => this.Serializer.Serialize((object) new TaxonTaskState(this));

    /// <inheritdoc />
    public override void ExecuteTask()
    {
      try
      {
        this.BeforeTaxonItemsUpdate();
        if (this.TargetTaxon != null && !this.TargetTaxon.Id.IsEmpty())
        {
          TaxonomyManager manager = TaxonomyManager.GetManager();
          TaxonTask.CheckTargetTaxonExists(this.TargetTaxon.Id, manager);
          this.AddOrUpdateTaxonStatus(this.TargetTaxon.Id, TaxonTask.TaxonStatusType.Ok, string.Empty);
          List<Guid> sourceTaxaIds = this.SourceTaxa.Select<TaxonTaskStateItem, Guid>((Func<TaxonTaskStateItem, Guid>) (p => p.Id)).ToList<Guid>();
          this.itemsCount = (int) manager.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => sourceTaxaIds.Contains(st.TaxonId))).Sum<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, long>>) (st => (long) st.MarkedItemsCount));
          List<Guid> taxaIds = this.GetTaxaIds(manager, sourceTaxaIds);
          sourceTaxaIds.ForEach((Action<Guid>) (id => this.AddOrUpdateTaxonStatus(id, TaxonTask.TaxonStatusType.NotStarted, string.Empty)));
          foreach (Guid guid in sourceTaxaIds)
          {
            Guid taxonId = guid;
            try
            {
              if (taxaIds.Contains(taxonId))
              {
                this.UpdateTaxonItems(taxonId, this.TargetTaxon.Id);
                if (this.TaxonStatusList.Any<TaxonTask.TaxonStatus>((Func<TaxonTask.TaxonStatus, bool>) (p => p.Id == taxonId && p.TaxonStatusType == TaxonTask.TaxonStatusType.NotStarted)))
                  this.AddOrUpdateTaxonStatus(taxonId, TaxonTask.TaxonStatusType.Ok, string.Empty);
              }
              else
                this.AddOrUpdateTaxonStatus(taxonId, TaxonTask.TaxonStatusType.NotFoundError, this.GetSourceNotFoundErrorMessage(taxonId));
            }
            catch (TargetTaxonNotFoundException ex)
            {
              throw ex;
            }
            catch (Exception ex)
            {
              Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
            }
            this.UpdateCurrentStatus();
          }
        }
        this.AfterTaxonItemsUpdate();
      }
      catch (TargetTaxonNotFoundException ex)
      {
        if (this.TargetTaxon != null && this.TargetTaxon.Id != Guid.Empty)
          this.AddOrUpdateTaxonStatus(this.TargetTaxon.Id, TaxonTask.TaxonStatusType.NotFoundError, this.GetTargetNotFoundErrorMessage(this.TargetTaxon.Id));
        else
          Telerik.Sitefinity.Abstractions.Exceptions.HandleException((Exception) ex, ExceptionPolicyName.IgnoreExceptions);
      }
      catch (Exception ex)
      {
        Telerik.Sitefinity.Abstractions.Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
      finally
      {
        if (this.TaxonStatusList.Count<TaxonTask.TaxonStatus>((Func<TaxonTask.TaxonStatus, bool>) (p => (uint) p.TaxonStatusType > 0U)) > 0)
          throw new Exception(JsonConvert.SerializeObject((object) this.TaxonStatusList));
      }
    }

    internal static void CheckTargetTaxonExists(Guid targetTaxonId, TaxonomyManager taxonomyManager = null)
    {
      if (targetTaxonId.IsEmpty())
        throw new TargetTaxonNotFoundException();
      taxonomyManager = taxonomyManager ?? TaxonomyManager.GetManager();
      if (taxonomyManager.GetTaxon(targetTaxonId) == null)
        throw new TargetTaxonNotFoundException();
    }

    internal virtual void BeforeTaxonItemsUpdate()
    {
    }

    internal virtual void UpdateItem(
      object item,
      Type itemType,
      TaxonomyPropertyDescriptor prop,
      Guid taxonId,
      Guid newTaxonId,
      IManager manager)
    {
      IOrganizable organizable = item as IOrganizable;
      if (organizable.Organizer.TaxonExists(prop.Name, taxonId))
      {
        if (!prop.MetaField.IsSingleTaxon)
          organizable.Organizer.RemoveTaxa(prop.Name, taxonId);
        else
          organizable.Organizer.RemoveTaxon(prop.Name, taxonId);
      }
      if (!organizable.Organizer.TaxonExists(prop.Name, newTaxonId))
        organizable.Organizer.AddTaxa(prop.Name, newTaxonId);
      TaxonTask.CheckTargetTaxonExists(this.TargetTaxon.Id);
      manager.SaveChanges();
    }

    internal virtual void AfterTaxonItemsUpdate()
    {
    }

    internal void UpdateTaxonItems(Guid sourceTaxonId, Guid targetTaxonId)
    {
      TaxonomyManager manager = TaxonomyManager.GetManager();
      TaxonTask.CheckTargetTaxonExists(this.TargetTaxon.Id, manager);
      if (manager.GetTaxa<Taxon>().FirstOrDefault<Taxon>((Expression<Func<Taxon, bool>>) (p => p.Id == sourceTaxonId)) == null)
      {
        this.AddOrUpdateTaxonStatus(sourceTaxonId, TaxonTask.TaxonStatusType.NotFoundError, this.GetSourceNotFoundErrorMessage(sourceTaxonId));
      }
      else
      {
        IQueryable<TaxonomyStatistic> statistics = manager.GetStatistics();
        Expression<Func<TaxonomyStatistic, bool>> predicate = (Expression<Func<TaxonomyStatistic, bool>>) (st => st.TaxonId == sourceTaxonId);
        foreach (TaxonomyStatistic taxonomyStatistic in (IEnumerable<TaxonomyStatistic>) statistics.Where<TaxonomyStatistic>(predicate).Distinct<TaxonomyStatistic>((IEqualityComparer<TaxonomyStatistic>) new TaxonTask.TaxonomyStatisticEqualityComparer()))
        {
          TaxonTask.CheckTargetTaxonExists(this.TargetTaxon.Id, manager);
          Taxon taxon = manager.GetTaxa<Taxon>().FirstOrDefault<Taxon>((Expression<Func<Taxon, bool>>) (p => p.Id == sourceTaxonId));
          if (taxon == null)
          {
            this.AddOrUpdateTaxonStatus(sourceTaxonId, TaxonTask.TaxonStatusType.NotFoundError, this.GetSourceNotFoundErrorMessage(sourceTaxonId));
            break;
          }
          Type itemType = TypeResolutionService.ResolveType(taxonomyStatistic.DataItemType, false);
          TaxonomyPropertyDescriptor propertyDescriptor;
          try
          {
            propertyDescriptor = TaxonomyManager.GetPropertyDescriptor(itemType, (ITaxon) taxon);
            if (propertyDescriptor == null)
            {
              this.AddOrUpdateTaxonStatus(sourceTaxonId, TaxonTask.TaxonStatusType.PropertyNotFoundError, this.GetPropertyNotFoundErrorMessage(itemType.ToString(), sourceTaxonId));
              break;
            }
          }
          catch (NullReferenceException ex)
          {
            this.AddOrUpdateTaxonStatus(sourceTaxonId, TaxonTask.TaxonStatusType.PropertyNotFoundError, this.GetPropertyNotFoundErrorMessage(itemType.ToString(), sourceTaxonId));
            break;
          }
          IManager mappedManager = ManagerBase.GetMappedManager(itemType, taxonomyStatistic.ItemProviderName);
          if (mappedManager.Provider is IOrganizableProvider provider)
          {
            int? totalCount = new int?(0);
            IQueryable<Guid> enumerable = ((IQueryable<IDataItem>) provider.GetItemsByTaxon(sourceTaxonId, propertyDescriptor.MetaField.IsSingleTaxon, propertyDescriptor.Name, itemType, (string) null, (string) null, 0, 0, ref totalCount)).Select<IDataItem, Guid>((Expression<Func<IDataItem, Guid>>) (i => i.Id));
            List<Guid> itemIdsList = new List<Guid>();
            foreach (Guid[] collection in enumerable.OnBatchesOf<Guid>(1000))
              itemIdsList.AddRange((IEnumerable<Guid>) collection);
            this.UpdateContentItems((IEnumerable<Guid>) itemIdsList, itemType, propertyDescriptor, sourceTaxonId, targetTaxonId, mappedManager);
          }
        }
      }
    }

    internal void UpdateContentItems(
      IEnumerable<Guid> itemIdsList,
      Type itemType,
      TaxonomyPropertyDescriptor prop,
      Guid taxonId,
      Guid newTaxonId,
      IManager manager)
    {
      foreach (Guid itemIds in itemIdsList)
      {
        this.RetryUpdate(itemIds, itemType, prop, taxonId, newTaxonId, manager);
        this.UpdateProgress();
      }
    }

    protected void HandleException(
      Guid itemId,
      Type itemType,
      Guid taxonId,
      string itemProviderName,
      Exception ex)
    {
      Exception exceptionToHandle = new Exception("Failed to update taxonomy item: {0} for DataItemType: {1} ({2}) and taxon: {3}".Arrange((object) itemId.ToString(), (object) itemType.FullName, (object) itemProviderName, (object) taxonId), ex);
      if (Telerik.Sitefinity.Abstractions.Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
        throw exceptionToHandle;
    }

    private void RetryUpdate(
      Guid itemId,
      Type itemType,
      TaxonomyPropertyDescriptor prop,
      Guid taxonId,
      Guid newTaxonId,
      IManager manager)
    {
      int num = 10;
      while (num > 0)
      {
        try
        {
          this.UpdateItem(manager.GetItem(itemType, itemId), itemType, prop, taxonId, newTaxonId, manager);
          break;
        }
        catch (OptimisticVerificationException ex)
        {
          manager.Provider.RollbackTransaction();
          --num;
          if (num == 0)
            this.HandleException(itemId, itemType, taxonId, manager.Provider.Name, (Exception) ex);
          else if (num > 2)
            Thread.Sleep(new Random().Next(0, 500));
        }
        catch (TargetTaxonNotFoundException ex)
        {
          throw ex;
        }
        catch (ItemNotFoundException ex)
        {
          break;
        }
        catch (NoSuchObjectException ex)
        {
          this.AddOrUpdateTaxonStatus(taxonId, TaxonTask.TaxonStatusType.NotFoundError, this.GetSourceNotFoundErrorMessage(taxonId));
          break;
        }
        catch (Exception ex)
        {
          this.AddOrUpdateTaxonStatus(taxonId, TaxonTask.TaxonStatusType.UnknownError, this.GetUnknownErrorMessage(taxonId));
          this.HandleException(itemId, itemType, taxonId, manager.Provider.Name, ex);
          break;
        }
      }
    }

    private int GetProgress() => this.itemsCount != 0 ? this.currentIndex * 100 / this.itemsCount : 0;

    private void UpdateProgress()
    {
      ++this.currentIndex;
      this.ProgressChanged(new TaskProgressEventArgs()
      {
        Progress = this.GetProgress()
      });
    }

    private void UpdateCurrentStatus() => this.ProgressChanged(new TaskProgressEventArgs()
    {
      Progress = this.GetProgress(),
      StatusMessage = JsonConvert.SerializeObject((object) this.TaxonStatusList)
    });

    private void ProgressChanged(TaskProgressEventArgs eventArgs)
    {
      this.OnProgressChanged(eventArgs);
      if (eventArgs.Stopped)
        throw new TaskStoppedException();
    }

    private string GetTaxonName(Guid taxonId)
    {
      string name = taxonId.ToString();
      if (this.TargetTaxon.Id == taxonId)
      {
        name = this.TargetTaxon.Name;
      }
      else
      {
        TaxonTaskStateItem taxonTaskStateItem = this.SourceTaxa.FirstOrDefault<TaxonTaskStateItem>((Func<TaxonTaskStateItem, bool>) (p => p.Id == taxonId));
        if (taxonTaskStateItem != null)
          name = taxonTaskStateItem.Name;
      }
      return name;
    }

    private void AddOrUpdateTaxonStatus(
      Guid taxonId,
      TaxonTask.TaxonStatusType taxonStatusType,
      string errorMessage)
    {
      TaxonTask.TaxonStatus taxonStatus = this.TaxonStatusList.FirstOrDefault<TaxonTask.TaxonStatus>((Func<TaxonTask.TaxonStatus, bool>) (p => p.Id == taxonId));
      if (taxonStatus == null)
      {
        this.TaxonStatusList.Add(new TaxonTask.TaxonStatus()
        {
          Id = taxonId,
          Name = this.GetTaxonName(taxonId),
          TaxonType = taxonId == this.TargetTaxon.Id ? TaxonTask.TaxonType.Target : TaxonTask.TaxonType.Source,
          TaxonStatusType = taxonStatusType,
          ErrorMessage = errorMessage
        });
      }
      else
      {
        taxonStatus.TaxonStatusType = taxonStatusType;
        taxonStatus.ErrorMessage = errorMessage;
      }
    }

    private List<Guid> GetTaxaIds(TaxonomyManager taxonomyManager, List<Guid> taxonIds) => taxonomyManager.GetTaxa<Taxon>().Where<Taxon>((Expression<Func<Taxon, bool>>) (p => taxonIds.Contains(p.Id))).Select<Taxon, Guid>((Expression<Func<Taxon, Guid>>) (p => p.Id)).ToList<Guid>();

    private string GetSourceNotFoundErrorMessage(Guid taxonId) => "Source taxon {0} not found.".Arrange((object) this.GetTaxonName(taxonId));

    private string GetPropertyNotFoundErrorMessage(string type, Guid taxonId) => "Property for type: {0} and taxon {1} not found.".Arrange((object) type, (object) this.GetTaxonName(taxonId));

    private string GetTargetNotFoundErrorMessage(Guid taxonId) => "Target taxon {0} not found.".Arrange((object) this.GetTaxonName(taxonId));

    private string GetUnknownErrorMessage(Guid taxonId) => "Unknown error when updating taxon {0}.".Arrange((object) this.GetTaxonName(taxonId));

    internal class TaxonStatus
    {
      public Guid Id { get; set; }

      public string Name { get; set; }

      public string ErrorMessage { get; set; }

      public TaxonTask.TaxonType TaxonType { get; set; }

      public TaxonTask.TaxonStatusType TaxonStatusType { get; set; }
    }

    [JsonConverter(typeof (StringEnumConverter))]
    internal enum TaxonType
    {
      /// <summary>Source taxon</summary>
      Source,
      /// <summary>Target taxon</summary>
      Target,
    }

    [JsonConverter(typeof (StringEnumConverter))]
    internal enum TaxonStatusType
    {
      /// <summary>OK status</summary>
      Ok,
      /// <summary>Not started status</summary>
      NotStarted,
      /// <summary>Not found error status</summary>
      NotFoundError,
      /// <summary>Property not found error status</summary>
      PropertyNotFoundError,
      /// <summary>Unknown error status</summary>
      UnknownError,
    }

    private class TaxonomyStatisticEqualityComparer : IEqualityComparer<TaxonomyStatistic>
    {
      public bool Equals(TaxonomyStatistic x, TaxonomyStatistic y)
      {
        if (x == null && y == null)
          return true;
        return x != null && y != null && x.TaxonId == y.TaxonId && x.TaxonomyId == y.TaxonomyId && x.DataItemType == y.DataItemType && x.ItemProviderName == y.ItemProviderName;
      }

      public int GetHashCode(TaxonomyStatistic obj)
      {
        if (obj == null)
          return 0;
        Guid guid = obj.TaxonId;
        int hashCode1 = guid.GetHashCode();
        guid = obj.TaxonomyId;
        int hashCode2 = guid.GetHashCode();
        return hashCode1 + hashCode2 + obj.DataItemType.GetHashCode() + obj.ItemProviderName.GetHashCode();
      }
    }
  }
}
