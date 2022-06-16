// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataEventsCallHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Microsoft.Practices.Unity.InterceptionExtension;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// An <see cref="T:Telerik.Microsoft.Practices.Unity.InterceptionExtension.ICallHandler" /> that invokes data events (Executing and Executed).
  /// </summary>
  public class DataEventsCallHandler : ICallHandler
  {
    /// <summary>Executes handler processing.</summary>
    /// <param name="input">Inputs to the current call to the target.</param>
    /// <param name="getNext">Delegate to execute to get the next delegate in the handler
    /// chain.</param>
    /// <returns>Return value from the target.</returns>
    public IMethodReturn Invoke(
      IMethodInvocation input,
      GetNextHandlerDelegate getNext)
    {
      object[] customAttributes = input.MethodBase.GetCustomAttributes(typeof (DataEventAttribute), true);
      bool flag;
      string commandName;
      if (customAttributes.Length != 0)
      {
        DataEventAttribute dataEventAttribute = (DataEventAttribute) customAttributes[0];
        flag = dataEventAttribute.FireEvents;
        commandName = !string.IsNullOrEmpty(dataEventAttribute.CommandName) ? dataEventAttribute.CommandName : input.MethodBase.Name;
      }
      else
      {
        flag = true;
        commandName = input.MethodBase.Name;
      }
      if (!flag || !input.MethodBase.IsPublic || input.MethodBase.IsSpecialName || !(input.Target is IDataProviderEventsCall target))
        return getNext()(input, getNext);
      ExecutingEventArgs args = new ExecutingEventArgs(commandName, (object) input.Arguments);
      target.OnExecuting(args);
      if (args.Cancel)
        return input.CreateMethodReturn((object) null, (object[]) null);
      IMethodReturn methodReturn = getNext()(input, getNext);
      target.OnExecuted(new ExecutedEventArgs(commandName, (object) input.Arguments, methodReturn.ReturnValue));
      return methodReturn;
    }

    /// <summary>Order in which the handler will be executed.</summary>
    /// <value></value>
    public int Order { get; set; }
  }
}
