// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.BaseFacadeWithManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;

namespace Telerik.Sitefinity.Fluent
{
  /// <summary>Base class for facades</summary>
  public abstract class BaseFacadeWithManager : BaseFacade
  {
    /// <summary>
    /// Various application settings (e.g. provider name and transaction name). Use for reading only.
    /// </summary>
    protected AppSettings settings;
    private IManager manager;

    internal BaseFacadeWithManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.BaseFacade" /> class.
    /// </summary>
    /// <param name="settings">The fluent API settings to use for this facade</param>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="settings" /> is null</exception>
    /// <exception cref="T:System.ArgumentException">When <paramref name="settings" />'s transaction name is null or empty</exception>
    public BaseFacadeWithManager(AppSettings settings)
    {
      FacadeHelper.AssertArgumentNotNull<AppSettings>(settings, nameof (settings));
      FacadeHelper.Assert<ArgumentException>(!settings.TransactionName.IsNullOrEmpty(), "Transaction name can not be empty");
      this.settings = settings;
    }

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="F:Telerik.Sitefinity.Fluent.BaseFacadeWithManager.settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="M:Telerik.Sitefinity.Fluent.BaseFacadeWithManager.GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected abstract IManager InitializeManager();

    /// <summary>
    /// Commit the shared transaction and break the fluent method chain
    /// </summary>
    public virtual bool SaveChanges()
    {
      AllFacadesHelper.SaveChanges(this.settings);
      return true;
    }

    /// <summary>
    /// Roll back all changes in the shared transaction and end the fluent method chain
    /// </summary>
    public virtual bool CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.settings.TransactionName);
      this.settings.ClearTransactionItems();
      return true;
    }

    /// <summary>Get a cached instance of this facade's manager</summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>If no manager is loaded (the first time this method is called), <see cref="M:Telerik.Sitefinity.Fluent.BaseFacadeWithManager.InitializeManager" /> will be called to create an instance.</remarks>
    public virtual IManager GetManager()
    {
      if (this.manager == null)
        this.manager = this.InitializeManager();
      FacadeHelper.Assert(this.manager != null, "manager is not initialized");
      return this.manager;
    }
  }
}
