// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.PackagingOperations
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web.Hosting;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Packaging.Configuration;
using Telerik.Sitefinity.Packaging.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Packaging
{
  internal static class PackagingOperations
  {
    private static LocalDataStoreSlot addonErrorStatusSlot;

    /// <summary>Deletes the addon links.</summary>
    /// <param name="itemId">The item identifier.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public static void DeleteAddonLinks(Guid itemId, string itemType, string transactionName = null)
    {
      PackagingManager manager = PackagingManager.GetManager((string) null, transactionName);
      IQueryable<AddonLink> addonLinks = manager.GetAddonLinks();
      Expression<Func<AddonLink, bool>> predicate = (Expression<Func<AddonLink, bool>>) (a => a.ItemId == itemId && a.ItemType == itemType);
      foreach (AddonLink addonLink in addonLinks.Where<AddonLink>(predicate).ToList<AddonLink>())
        manager.DeleteAddonLink(addonLink);
      if (!string.IsNullOrEmpty(transactionName))
        return;
      manager.SaveChanges();
    }

    /// <summary>Sets the content import error status.</summary>
    /// <param name="hasErrors">if set to <c>true</c> [has errors].</param>
    public static void SetContentImportErrorStatus(bool hasErrors)
    {
      if (PackagingOperations.AddonErrorStatus == null)
        return;
      PackagingOperations.AddonErrorStatus.HasErrorsOnContentImport = hasErrors;
    }

    /// <summary>Sets the structure import error status.</summary>
    /// <param name="hasErrors">if set to <c>true</c> [has errors].</param>
    public static void SetStructureImportErrorStatus(bool hasErrors)
    {
      if (PackagingOperations.AddonErrorStatus == null)
        return;
      PackagingOperations.AddonErrorStatus.HasErrorsOnStructureImport = hasErrors;
    }

    /// <summary>Sets the activate addon error status.</summary>
    /// <param name="hasErrors">if set to <c>true</c> [has errors].</param>
    public static void SetActivateAddonErrorStatus(bool hasErrors)
    {
      if (PackagingOperations.AddonErrorStatus == null)
        return;
      PackagingOperations.AddonErrorStatus.HasErrorsOnActivate = hasErrors;
    }

    /// <summary>Loads the addon error status.</summary>
    /// <param name="addon">The addon.</param>
    public static void LoadAddonErrorStatus(Addon addon) => PackagingOperations.AddonErrorStatus = new AddonErrorStatus(addon.ErrorStatus);

    /// <summary>Saves the addon error status.</summary>
    /// <param name="addon">The addon.</param>
    public static void SaveAddonErrorStatus(Addon addon) => AddonErrorStatus.Copy(PackagingOperations.AddonErrorStatus, addon.ErrorStatus);

    /// <summary>Checks if multisite import/export is disabled.</summary>
    /// <returns>
    ///   <c>true</c> if multisite import/export is disabled; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsMultisiteImportExportDisabled()
    {
      bool flag = true;
      if (SystemManager.IsModuleAccessible("PackagingModule"))
        flag = Config.Get<PackagingConfig>().DisableMultisiteImportExport;
      return flag;
    }

    /// <summary>Gets or sets the addon error status.</summary>
    /// <value>The addon error status.</value>
    public static AddonErrorStatus AddonErrorStatus
    {
      get
      {
        object obj = !HostingEnvironment.IsHosted || SystemManager.CurrentHttpContext == null ? Thread.GetData(PackagingOperations.AddonErrorStatusSlot) : SystemManager.CurrentHttpContext.Items[(object) "sf-addon-error-status-key"];
        return obj != null ? obj as AddonErrorStatus : (AddonErrorStatus) null;
      }
      set
      {
        if (HostingEnvironment.IsHosted && SystemManager.CurrentHttpContext != null)
          SystemManager.CurrentHttpContext.Items[(object) "sf-addon-error-status-key"] = (object) value;
        else
          Thread.SetData(PackagingOperations.AddonErrorStatusSlot, (object) value);
      }
    }

    private static LocalDataStoreSlot AddonErrorStatusSlot
    {
      get
      {
        if (PackagingOperations.addonErrorStatusSlot == null)
          PackagingOperations.addonErrorStatusSlot = Thread.AllocateDataSlot();
        return PackagingOperations.addonErrorStatusSlot;
      }
    }
  }
}
