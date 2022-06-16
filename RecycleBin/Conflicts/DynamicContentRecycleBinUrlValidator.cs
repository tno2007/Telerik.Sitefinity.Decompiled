// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RecycleBin.Conflicts.DynamicContentRecycleBinUrlValidator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Net;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.Services;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.RecycleBin.Conflicts
{
  internal class DynamicContentRecycleBinUrlValidator<T> : IRecycleBinUrlValidator<T>
    where T : DynamicContent
  {
    /// <summary>
    /// Determines whether the specified <paramref name="dataItem" />'s URL is unique.
    /// </summary>
    /// <param name="manager">The manager for specified dataItem.</param>
    /// <param name="dataItem">The data item which URL will be checked.</param>
    /// <returns>Returns a URL conflict if any.</returns>
    /// <exception cref="T:System.InvalidCastException">dataItem cannot be cast to valid type for URL validation</exception>
    public IRestoreConflict GetUrlConflict(IManager manager, T dataItem)
    {
      DynamicContent dataItem1 = (object) dataItem as DynamicContent;
      if ((object) dataItem != null)
        return this.ValidateExistingContentItemUrl((DynamicModuleManager) manager, dataItem1, dataItem1.GetType());
      throw new InvalidCastException("dataItem cannot be cast to valid type for URL validation");
    }

    /// <summary>
    /// Asserts that the specified <paramref name="dataItem" />'s URL is unique.
    /// If the a URL conflict is encountered an Exception containing the
    /// conflict information will be thrown.
    /// </summary>
    /// <param name="manager">The manager for current <see cref="!:dataItem" />.</param>
    /// <param name="dataItem">The data item which URL will be checked.</param>
    /// <exception cref="T:System.InvalidCastException"><see cref="!:dataItem" /> cannot be cast to valid type for URL validation</exception>
    public void AssertNoUrlConflicts(IManager manager, T dataItem)
    {
      DynamicContent dataItem1 = (object) dataItem as DynamicContent;
      if ((object) dataItem == null)
        throw new InvalidCastException("dataItem cannot be cast to valid type for URL validation");
      this.ValidateContentItemDuplicateUrl((DynamicModuleManager) manager, dataItem1, dataItem1.GetType());
    }

    private IRestoreConflict ValidateExistingContentItemUrl(
      DynamicModuleManager manager,
      DynamicContent dataItem,
      Type dataItemType)
    {
      try
      {
        this.ValidateContentItemDuplicateUrl(manager, dataItem, dataItemType);
      }
      catch (WebProtocolException ex)
      {
        return ex.StatusCode == HttpStatusCode.InternalServerError ? (IRestoreConflict) new RestoreConflict()
        {
          IsRecoverable = false,
          Reason = System.Enum.GetName(typeof (RestoreConflictReasons), (object) RestoreConflictReasons.ExistingContentItemUrl),
          ReasonArgs = ex.Data
        } : throw ex;
      }
      return (IRestoreConflict) null;
    }

    private void ValidateContentItemDuplicateUrl(
      DynamicModuleManager manager,
      DynamicContent dataItem,
      Type dataItemType)
    {
      new DataService().ValidateDublicateUrl(manager, dataItem, dataItemType, dataItem.Id);
    }
  }
}
