// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.OpenAccessCounterDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Exceptions;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Data.OA
{
  public class OpenAccessCounterDecorator : ICounterDecorator
  {
    private readonly IOpenAccessDataProvider provider;

    public OpenAccessCounterDecorator(IOpenAccessDataProvider provider) => this.provider = provider;

    public void InitCounter(string name, long initialValue)
    {
      using (IObjectScope newScope = this.GetNewScope())
      {
        newScope.Transaction.Begin();
        this.GetOrCreateCounter(newScope, name).LastValue = initialValue;
        newScope.Transaction.Commit();
      }
    }

    public void DeleteCounter(string name)
    {
      using (IObjectScope newScope = this.GetNewScope())
      {
        newScope.Transaction.Begin();
        AnyCounter persistentObject = newScope.Extent<AnyCounter>().Where<AnyCounter>((Expression<Func<AnyCounter, bool>>) (x => x.Name == name)).SingleOrDefault<AnyCounter>();
        if (persistentObject == null)
          return;
        try
        {
          newScope.Remove((object) persistentObject);
          newScope.Transaction.Commit();
        }
        catch (ConcurrencyControlException ex)
        {
        }
        catch (OpenAccessException ex)
        {
        }
      }
    }

    public long GetCurrentValue(string name)
    {
      using (IObjectScope newScope = this.GetNewScope())
      {
        newScope.Transaction.Begin();
        return this.GetOrCreateCounter(newScope, name).LastValue;
      }
    }

    public long GetNextValue(string name, int incrementStep = 1)
    {
      using (IObjectScope newScope = this.GetNewScope())
      {
        while (true)
        {
          try
          {
            newScope.Transaction.Begin();
            AnyCounter counter = this.GetOrCreateCounter(newScope, name);
            counter.LastValue += (long) incrementStep;
            long lastValue = counter.LastValue;
            newScope.Transaction.Commit();
            return lastValue;
          }
          catch (ConcurrencyControlException ex)
          {
          }
          catch (OpenAccessException ex)
          {
          }
        }
      }
    }

    /// <summary>
    /// Initializes multiple counters with a single transaction.
    /// </summary>
    /// <remarks>Retries on OpenAccess exceptions.</remarks>
    /// <param name="counters">The counters.</param>
    internal void InitMultipleCounters(IDictionary<string, long> counters)
    {
      using (IObjectScope newScope = this.GetNewScope())
      {
        while (true)
        {
          try
          {
            newScope.Transaction.Begin();
            foreach (string key in (IEnumerable<string>) counters.Keys)
              this.GetOrCreateCounter(newScope, key).LastValue = counters[key];
            newScope.Transaction.Commit();
            break;
          }
          catch (ConcurrencyControlException ex)
          {
          }
          catch (OpenAccessException ex)
          {
          }
        }
      }
    }

    private AnyCounter GetOrCreateCounter(IObjectScope scope, string counterName)
    {
      AnyCounter persistenceCapableObject = scope.Extent<AnyCounter>().Where<AnyCounter>((Expression<Func<AnyCounter, bool>>) (x => x.Name == counterName)).SingleOrDefault<AnyCounter>();
      if (persistenceCapableObject == null)
      {
        persistenceCapableObject = new AnyCounter(counterName);
        scope.Add((object) persistenceCapableObject);
      }
      return persistenceCapableObject;
    }

    private IObjectScope GetNewScope() => OpenAccessConnection.GetObjectScope(this.provider);
  }
}
