// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.TransactionManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web.Hosting;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data
{
  /// <summary>Provides distributed transaction management.</summary>
  public static class TransactionManager
  {
    [ThreadStatic]
    private static TransactionManager.NamedTransactions transactions;
    [ThreadStatic]
    internal static TransactionManager.NamedTransactions staticContainer;
    private const string NamedTransactionsKey = "sf_NamedTransactions";

    /// <summary>
    /// Gets a collection of named transactions if available for the current request / thread.
    /// </summary>
    /// <returns></returns>
    internal static TransactionManager.NamedTransactions GetTransactions()
    {
      if (!HostingEnvironment.IsHosted)
        return TransactionManager.transactions;
      ContextTransactions currentTransactions = SystemManager.CurrentTransactions;
      return currentTransactions == null ? TransactionManager.staticContainer : (TransactionManager.NamedTransactions) currentTransactions[(object) "sf_NamedTransactions"];
    }

    /// <summary>
    /// Tries the add post commit action during committing transaction.
    /// You should do this only after transaction commit has been started. If needed we could consider different behavior.
    /// </summary>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <param name="action">The action.</param>
    /// <returns></returns>
    internal static bool TryAddPostCommitAction(string transactionName, Action action)
    {
      TransactionManager.NamedTransactions transactions = TransactionManager.GetTransactions();
      TransactionManager.Connections connections;
      if (transactions == null || !transactions.TryGetValue(transactionName, out connections))
        return false;
      if (connections.PostCommitActions == null)
        connections.PostCommitActions = new Queue<Action>();
      connections.PostCommitActions.Enqueue(action);
      return true;
    }

    /// <summary>
    /// Gets the specified transaction if found otherwise returns null.
    /// </summary>
    /// <typeparam name="TTransaction">The type of the transaction.</typeparam>
    /// <param name="transactionName">The name of the transaction to search for.</param>
    /// <param name="connectionString">The connection string to search for.</param>
    /// <returns>The transaction object or null if not found.</returns>
    public static TTransaction GetTransaction<TTransaction>(
      string transactionName,
      string connectionString)
    {
      if (transactionName == null)
        throw new ArgumentNullException(nameof (transactionName));
      if (string.IsNullOrEmpty(connectionString))
        throw new ArgumentNullException(nameof (connectionString));
      TransactionManager.NamedTransactions transactions = TransactionManager.GetTransactions();
      TransactionManager.Connections connections;
      TransactionManager.Wrapper wrapper;
      return transactions != null && transactions.TryGetValue(transactionName, out connections) && connections.TryGetValue(connectionString, out wrapper) ? (TTransaction) wrapper.Transaction : default (TTransaction);
    }

    /// <summary>
    /// Adds the provided named transaction to the current request / thread.
    /// If there is already registered transaction with the same name and connection string for the
    /// current request / thread an <see cref="T:System.ArgumentException" /> will be thrown.
    /// </summary>
    /// <param name="transactionName">The name of the transaction.</param>
    /// <param name="connectionString">The connection string.</param>
    public static void AddTransaction(
      string transactionName,
      string connectionString,
      IDataProviderDecorator decorator,
      object transaction)
    {
      if (transactionName == null)
        throw new ArgumentNullException(nameof (transactionName));
      if (string.IsNullOrEmpty(connectionString))
        throw new ArgumentNullException(nameof (connectionString));
      if (decorator == null)
        throw new ArgumentNullException(nameof (decorator));
      if (transaction == null)
        throw new ArgumentNullException(nameof (transaction));
      TransactionManager.NamedTransactions namedTransactions;
      if (HostingEnvironment.IsHosted)
      {
        ContextTransactions currentTransactions = SystemManager.CurrentTransactions;
        if (currentTransactions == null)
        {
          if (TransactionManager.staticContainer == null)
            TransactionManager.staticContainer = new TransactionManager.NamedTransactions();
          namedTransactions = TransactionManager.staticContainer;
        }
        else
        {
          namedTransactions = (TransactionManager.NamedTransactions) currentTransactions[(object) "sf_NamedTransactions"];
          if (namedTransactions == null)
          {
            namedTransactions = new TransactionManager.NamedTransactions();
            currentTransactions[(object) "sf_NamedTransactions"] = (object) namedTransactions;
          }
        }
      }
      else
      {
        if (TransactionManager.transactions == null)
          TransactionManager.transactions = new TransactionManager.NamedTransactions();
        namedTransactions = TransactionManager.transactions;
      }
      TransactionManager.Connections connections;
      if (!namedTransactions.TryGetValue(transactionName, out connections))
      {
        connections = new TransactionManager.Connections()
        {
          Key = transactionName
        };
        namedTransactions.Add(connections);
      }
      connections.Add(new TransactionManager.Wrapper()
      {
        Key = connectionString,
        Provider = decorator.DataProvider,
        Transaction = transaction
      });
    }

    /// <summary>Commits the specified transaction.</summary>
    /// <param name="transactionName">The name of the transaction to commit.</param>
    public static void CommitTransaction(string transactionName)
    {
      if (transactionName == null)
        throw new ArgumentNullException(nameof (transactionName));
      TransactionManager.NamedTransactions transactions = TransactionManager.GetTransactions();
      TransactionManager.Connections connections;
      if (transactions == null || !transactions.TryGetValue(transactionName, out connections))
        return;
      bool flag;
      try
      {
        IOrderedEnumerable<TransactionManager.Wrapper> conns = connections.OrderBy<TransactionManager.Wrapper, bool>((Func<TransactionManager.Wrapper, bool>) (w => w.Provider is MetaDataProvider));
        TransactionManager.DoWithConnections(connections, (Action<TransactionManager.Wrapper>) (wrapper => wrapper.Provider.ValidateOnCommittingInTransaction()), (IEnumerable<TransactionManager.Wrapper>) conns);
        TransactionManager.DoWithConnections(connections, (Action<TransactionManager.Wrapper>) (wrapper => wrapper.Provider.FlushTransaction()), (IEnumerable<TransactionManager.Wrapper>) conns);
        flag = true;
      }
      catch (Exception ex)
      {
        TransactionManager.RollbackTransaction(transactionName);
        throw ex;
      }
      if (!flag)
        return;
      Queue<Action> postCommitActions = connections.PostCommitActions;
      connections.PostCommitActions = (Queue<Action>) null;
      TransactionManager.DoWithConnections(connections, (Action<TransactionManager.Wrapper>) (wrapper => wrapper.Provider.CommitTransaction()), (IEnumerable<TransactionManager.Wrapper>) connections.OrderByDescending<TransactionManager.Wrapper, bool>((Func<TransactionManager.Wrapper, bool>) (w => w.Provider is MetaDataProvider)));
      if (connections.PostCommitActions != null)
      {
        while (connections.PostCommitActions.Any<Action>())
          connections.PostCommitActions.Dequeue()();
      }
      connections.PostCommitActions = (Queue<Action>) null;
      if (postCommitActions == null)
        return;
      while (postCommitActions.Any<Action>())
        postCommitActions.Dequeue()();
    }

    private static void DoWithConnections(
      TransactionManager.Connections originalConns,
      Action<TransactionManager.Wrapper> action,
      IEnumerable<TransactionManager.Wrapper> conns = null,
      int prevConnsCount = 0)
    {
      List<TransactionManager.Wrapper> wrapperList = new List<TransactionManager.Wrapper>(conns ?? (IEnumerable<TransactionManager.Wrapper>) originalConns);
      foreach (TransactionManager.Wrapper wrapper in wrapperList)
        action(wrapper);
      prevConnsCount += wrapperList.Count;
      if (originalConns.Count <= prevConnsCount)
        return;
      TransactionManager.DoWithConnections(originalConns, action, originalConns.Skip<TransactionManager.Wrapper>(prevConnsCount), prevConnsCount);
    }

    /// <summary>Commits the specified transaction.</summary>
    /// <param name="transactionName">The name of the transaction to commit.</param>
    public static void FlushTransaction(string transactionName)
    {
      if (transactionName == null)
        throw new ArgumentNullException(nameof (transactionName));
      TransactionManager.NamedTransactions transactions = TransactionManager.GetTransactions();
      TransactionManager.Connections originalConns;
      if (transactions == null || !transactions.TryGetValue(transactionName, out originalConns))
        return;
      TransactionManager.DoWithConnections(originalConns, (Action<TransactionManager.Wrapper>) (wrapper => wrapper.Provider.ValidateOnCommittingInTransaction()));
      TransactionManager.DoWithConnections(originalConns, (Action<TransactionManager.Wrapper>) (wrapper => wrapper.Provider.FlushTransaction()));
    }

    /// <summary>Rollbacks the specified transaction.</summary>
    /// <param name="transactionName">The name of the transaction to rollback.</param>
    public static void RollbackTransaction(string transactionName)
    {
      if (transactionName == null)
        throw new ArgumentNullException(nameof (transactionName));
      TransactionManager.NamedTransactions transactions = TransactionManager.GetTransactions();
      TransactionManager.Connections originalConns;
      if (transactions == null || !transactions.TryGetValue(transactionName, out originalConns))
        return;
      TransactionManager.DoWithConnections(originalConns, (Action<TransactionManager.Wrapper>) (wrapper => wrapper.Provider.RollbackTransaction()));
      originalConns.PostCommitActions = (Queue<Action>) null;
    }

    /// <summary>
    /// Sets a pessimistic lock for read on the specified object for the given transaction.
    /// </summary>
    /// <param name="transactionName">The name of the transaction.</param>
    /// <param name="targetObject">The persistent object to be locked.</param>
    public static void LockTransactionForRead(string transactionName, object target)
    {
      if (transactionName == null)
        throw new ArgumentNullException(nameof (transactionName));
      TransactionManager.NamedTransactions transactions = TransactionManager.GetTransactions();
      TransactionManager.Connections originalConns;
      if (transactions == null || !transactions.TryGetValue(transactionName, out originalConns))
        return;
      TransactionManager.DoWithConnections(originalConns, (Action<TransactionManager.Wrapper>) (wrapper => wrapper.Provider.LockTransactionForRead(target)));
    }

    /// <summary>
    /// Sets a pessimistic lock for write on the specified object for the given transaction.
    /// </summary>
    /// <param name="transactionName">The name of the transaction.</param>
    /// <param name="targetObject">The persistent object to be locked.</param>
    public static void LockTransactionForWrite(string transactionName, object target)
    {
      if (transactionName == null)
        throw new ArgumentNullException(nameof (transactionName));
      TransactionManager.NamedTransactions transactions = TransactionManager.GetTransactions();
      TransactionManager.Connections originalConns;
      if (transactions == null || !transactions.TryGetValue(transactionName, out originalConns))
        return;
      TransactionManager.DoWithConnections(originalConns, (Action<TransactionManager.Wrapper>) (wrapper => wrapper.Provider.LockTransactionForWrite(target)));
    }

    /// <summary>Disposes the specified transactions.</summary>
    /// <param name="transactionName">Name of the transaction.</param>
    public static void DisposeTransaction(string transactionName)
    {
      TransactionManager.NamedTransactions transactions = TransactionManager.GetTransactions();
      TransactionManager.Connections connections;
      if (transactions == null || !transactions.TryGetValue(transactionName, out connections))
        return;
      transactions.Remove(connections);
      connections.Dispose();
    }

    /// <summary>
    /// Disposes all transactions defined for the current request / thread.
    /// This method is called upon completion of the current request / thread.
    /// </summary>
    public static void DisposeAllTransactions()
    {
      TransactionManager.GetTransactions()?.Dispose();
      if (TransactionManager.staticContainer == null)
        return;
      TransactionManager.staticContainer.Dispose();
    }

    internal class Wrapper
    {
      public string Key { get; set; }

      public DataProviderBase Provider { get; set; }

      public object Transaction { get; set; }
    }

    internal class Connections : KeyedCollection<string, TransactionManager.Wrapper>, IDisposable
    {
      public string Key { get; set; }

      internal Queue<Action> PostCommitActions { get; set; }

      protected override string GetKeyForItem(TransactionManager.Wrapper item) => item.Key;

      public bool TryGetValue(string key, out TransactionManager.Wrapper value)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        if (this.Dictionary != null)
          return this.Dictionary.TryGetValue(key, out value);
        foreach (TransactionManager.Wrapper wrapper in (IEnumerable<TransactionManager.Wrapper>) this.Items)
        {
          if (this.Comparer.Equals(this.GetKeyForItem(wrapper), key))
          {
            value = wrapper;
            return true;
          }
        }
        value = (TransactionManager.Wrapper) null;
        return false;
      }

      public void MoveBefore(
        TransactionManager.Wrapper baseItem,
        TransactionManager.Wrapper itemToMove)
      {
        if (baseItem == null)
          throw new ArgumentNullException(nameof (baseItem));
        if (itemToMove == null)
          throw new ArgumentNullException(nameof (itemToMove));
        int index = this.Items.IndexOf(baseItem);
        if (index < 0)
          throw new ArgumentException("baseItem is not present in the collection");
        int num = this.Items.IndexOf(itemToMove);
        if (index >= num && num >= 0)
          return;
        if (num > -1)
          this.Items.Remove(itemToMove);
        this.Items.Insert(index, itemToMove);
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
        foreach (TransactionManager.Wrapper wrapper in (Collection<TransactionManager.Wrapper>) this)
        {
          if (wrapper.Transaction is IDisposable transaction)
            transaction.Dispose();
        }
      }
    }

    internal class NamedTransactions : 
      KeyedCollection<string, TransactionManager.Connections>,
      IDisposable
    {
      protected override string GetKeyForItem(TransactionManager.Connections item) => item.Key;

      public bool TryGetValue(string key, out TransactionManager.Connections value)
      {
        if (key == null)
          throw new ArgumentNullException(nameof (key));
        if (this.Dictionary != null)
          return this.Dictionary.TryGetValue(key, out value);
        foreach (TransactionManager.Connections connections in (IEnumerable<TransactionManager.Connections>) this.Items)
        {
          if (this.Comparer.Equals(this.GetKeyForItem(connections), key))
          {
            value = connections;
            return true;
          }
        }
        value = (TransactionManager.Connections) null;
        return false;
      }

      /// <summary>
      /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
      /// </summary>
      public void Dispose()
      {
        foreach (TransactionManager.Connections connections in (Collection<TransactionManager.Connections>) this)
          connections.Dispose();
      }
    }
  }
}
