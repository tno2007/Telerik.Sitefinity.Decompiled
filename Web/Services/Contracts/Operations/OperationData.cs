// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.OperationData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations
{
  /// <summary>Class which contains data for custom operations.</summary>
  public class OperationData
  {
    private OperationType operationType;
    private Delegate methodPointer;

    private OperationData(Delegate action)
    {
      this.methodPointer = action;
      this.IsRead = true;
      this.IsAllowedUnauthorized = false;
    }

    /// <summary>
    /// Creates operation which will invoke void callback on execution.
    /// </summary>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create(Action<OperationContext> action) => new OperationData((Delegate) action);

    /// <summary>
    /// Creates operation which will invoke void callback on execution.
    /// </summary>
    /// <typeparam name="T">First additional parameter of the action.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<T>(Action<OperationContext, T> action) => new OperationData((Delegate) action);

    /// <summary>
    /// Creates operation which will invoke void callback on execution.
    /// </summary>
    /// <typeparam name="T">First additional parameter of the action.</typeparam>
    /// <typeparam name="T1">Second additional parameter of the action.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<T, T1>(Action<OperationContext, T, T1> action) => new OperationData((Delegate) action);

    /// <summary>
    /// Creates operation which will invoke void callback on execution.
    /// </summary>
    /// <typeparam name="T">First additional parameter of the action.</typeparam>
    /// <typeparam name="T1">Second additional parameter of the action.</typeparam>
    /// <typeparam name="T2">Third additional parameter of the action.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<T, T1, T2>(
      Action<OperationContext, T, T1, T2> action)
    {
      return new OperationData((Delegate) action);
    }

    /// <summary>
    /// Creates operation which will invoke callback on execution and will return result.
    /// </summary>
    /// <typeparam name="TRet">The return type of the callback.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<TRet>(Func<TRet> action) => new OperationData((Delegate) action);

    /// <summary>
    /// Creates operation which will invoke callback on execution and will return result.
    /// </summary>
    /// <typeparam name="TRet">The return type of the callback.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<TRet>(Func<OperationContext, TRet> action) => new OperationData((Delegate) action);

    /// <summary>
    /// Creates operation which will invoke callback on execution and will return result.
    /// </summary>
    /// <typeparam name="T">First additional parameter of the action.</typeparam>
    /// <typeparam name="TRet">The return type of the callback.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<T, TRet>(Func<OperationContext, T, TRet> action) => new OperationData((Delegate) action);

    /// <summary>
    /// Creates operation which will invoke callback on execution and will return result.
    /// </summary>
    /// <typeparam name="T1">First additional parameter of the action.</typeparam>
    /// <typeparam name="T2">Second additional parameter of the action.</typeparam>
    /// <typeparam name="TRet">The return type of the callback.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<T1, T2, TRet>(
      Func<OperationContext, T1, T2, TRet> action)
    {
      return new OperationData((Delegate) action);
    }

    /// <summary>
    /// Creates operation which will invoke callback on execution and will return result.
    /// </summary>
    /// <typeparam name="T1">First additional parameter of the action.</typeparam>
    /// <typeparam name="T2">Second additional parameter of the action.</typeparam>
    /// <typeparam name="T3">Third additional parameter of the action.</typeparam>
    /// <typeparam name="TRet">The return type of the callback.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<T1, T2, T3, TRet>(
      Func<OperationContext, T1, T2, T3, TRet> action)
    {
      return new OperationData((Delegate) action);
    }

    /// <summary>
    /// Creates operation which will invoke callback on execution and will return result.
    /// </summary>
    /// <typeparam name="T1">First additional parameter of the action.</typeparam>
    /// <typeparam name="T2">Second additional parameter of the action.</typeparam>
    /// <typeparam name="T3">Third additional parameter of the action.</typeparam>
    /// <typeparam name="T4">Fourth additional parameter of the action.</typeparam>
    /// <typeparam name="TRet">The return type of the callback.</typeparam>
    /// <param name="action">The action which will be invoked on execution.</param>
    /// <returns>New operation data.</returns>
    public static OperationData Create<T1, T2, T3, T4, TRet>(
      Func<OperationContext, T1, T2, T3, T4, TRet> action)
    {
      return new OperationData((Delegate) action);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the operation is read and is not modifying anything.
    /// </summary>
    public bool IsRead { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the operations is allowed for unauthorized users.
    /// </summary>
    public bool IsAllowedUnauthorized { get; set; }

    /// <summary>Gets or sets the type of the operation.</summary>
    public OperationType OperationType
    {
      get => this.operationType;
      set => this.operationType = value;
    }

    internal object Execute(object[] values) => values.Length != 0 ? this.methodPointer.DynamicInvoke(values) : throw new ArgumentNullException("context");

    internal string Name => this.methodPointer.Method.Name;

    internal IDictionary<string, Type> Parameters => (IDictionary<string, Type>) ((IEnumerable<ParameterInfo>) this.methodPointer.Method.GetParameters()).Where<ParameterInfo>((Func<ParameterInfo, bool>) (x => x.Name != "context")).ToDictionary<ParameterInfo, string, Type>((Func<ParameterInfo, string>) (x => x.Name), (Func<ParameterInfo, Type>) (y => y.ParameterType));

    internal Type ReturnType => this.methodPointer.Method.ReturnType;
  }
}
