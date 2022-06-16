// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ThrowHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Throws exceptions according using exception policies</summary>
  public static class ThrowHelper
  {
    private static object[] emptyObjects = new object[0];

    /// <summary>Throw an exception with a localization message pageId</summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrow<TException>(
      ExceptionPolicyName policyName,
      string messageId,
      Exception innerException,
      object[] messageParams)
      where TException : Exception, new()
    {
      string message = Res.Get("ErrorMessages", messageId);
      ThrowHelper.Throw<TException>(policyName, message, innerException, messageParams);
    }

    /// <summary>Throw an exception</summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void Throw<TException>(
      ExceptionPolicyName policyName,
      string message,
      Exception innerException,
      object[] messageParams)
      where TException : Exception, new()
    {
      string message1 = string.Format(message, messageParams);
      Exception exceptionToHandle;
      try
      {
        exceptionToHandle = (Exception) Activator.CreateInstance(typeof (TException), (object) message1, (object) innerException);
      }
      catch
      {
        try
        {
          exceptionToHandle = (Exception) Activator.CreateInstance(typeof (TException), (object) message1);
        }
        catch
        {
          try
          {
            exceptionToHandle = (Exception) new TException();
          }
          catch
          {
            exceptionToHandle = new Exception(message1, innerException);
          }
        }
      }
      if (Exceptions.HandleException(exceptionToHandle, policyName))
        throw exceptionToHandle;
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and global policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrowGlobal<TException>(
      string messageId,
      Exception innerException,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrow<TException>(ExceptionPolicyName.DataProviders, messageId, innerException, messageParams);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and global policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    public static void LocalizedThrowGlobal<TException>(string messageId, Exception innerException) where TException : Exception, new() => ThrowHelper.LocalizedThrowGlobal<TException>(messageId, innerException, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with a localization message pageId and global policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrowGlobal<TException>(
      string messageId,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrowGlobal<TException>(messageId, (Exception) null, messageParams);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and global policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    public static void LocalizedThrowGlobal<TException>(string messageId) where TException : Exception, new() => ThrowHelper.LocalizedThrowGlobal<TException>(messageId, (Exception) null, ThrowHelper.emptyObjects);

    /// <summary>Throw an exception with global exception policy</summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void ThrowGlobal<TException>(
      string message,
      Exception innerException,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.Throw<TException>(ExceptionPolicyName.Global, message, innerException, messageParams);
    }

    /// <summary>Throw an exception with global exception policy</summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    public static void ThrowGlobal<TException>(string message, Exception innerException) where TException : Exception, new() => ThrowHelper.ThrowGlobal<TException>(message, innerException, ThrowHelper.emptyObjects);

    /// <summary>Throw an exception with global exception policy</summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void ThrowGlobal<TException>(string message, params object[] messageParams) where TException : Exception, new() => ThrowHelper.ThrowGlobal<TException>(message, (Exception) null, messageParams);

    /// <summary>Throw an exception with global exception policy</summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    public static void ThrowGlobal<TException>(string message) where TException : Exception, new() => ThrowHelper.ThrowGlobal<TException>(message, (Exception) null, ThrowHelper.emptyObjects);

    /// <summary>Throw an exception with global exception policy</summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    public static void ThrowGlobal<TException>() where TException : Exception, new() => ThrowHelper.ThrowGlobal<TException>(string.Empty);

    /// <summary>
    /// Throw an exception with a localization message pageId and DataProviders policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrowDataProvide<TException>(
      string messageId,
      Exception innerException,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrow<TException>(ExceptionPolicyName.DataProviders, messageId, innerException, messageParams);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and DataProviders policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    public static void LocalizedThrowDataProvide<TException>(
      string messageId,
      Exception innerException)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrowDataProvide<TException>(messageId, innerException, ThrowHelper.emptyObjects);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and DataProviders policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="messageId">Id of the message</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrowDataProvide<TException>(
      string messageId,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrowDataProvide<TException>(messageId, (Exception) null, messageParams);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and DataProviders policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="policyName">Defines the policy name to use</param>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    public static void LocalizedThrowDataProvide<TException>(string messageId) where TException : Exception, new() => ThrowHelper.LocalizedThrowDataProvide<TException>(messageId, (Exception) null, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with DataProviders exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void ThrowDataProvider<TException>(
      string message,
      Exception innerException,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.Throw<TException>(ExceptionPolicyName.DataProviders, message, innerException, messageParams);
    }

    /// <summary>
    /// Throw an exception with DataProviders exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    public static void ThrowDataProvider<TException>(string message, Exception innerException) where TException : Exception, new() => ThrowHelper.ThrowDataProvider<TException>(message, innerException, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with DataProviders exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void ThrowDataProvider<TException>(string message, params object[] messageParams) where TException : Exception, new() => ThrowHelper.ThrowDataProvider<TException>(message, (Exception) null, messageParams);

    /// <summary>
    /// Throw an exception with DataProviders exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    public static void ThrowDataProvider<TException>(string message) where TException : Exception, new() => ThrowHelper.ThrowDataProvider<TException>(message, (Exception) null, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with DataProviders exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    public static void ThrowDataProvider<TException>() where TException : Exception, new() => ThrowHelper.ThrowDataProvider<TException>(string.Empty);

    /// <summary>
    /// Throw an exception with a localization message pageId and IgnoreExceptions policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrowIgnore<TException>(
      string messageId,
      Exception innerException,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrow<TException>(ExceptionPolicyName.IgnoreExceptions, messageId, innerException, messageParams);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and IgnoreExceptions policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    public static void LocalizedThrowIgnore<TException>(string messageId, Exception innerException) where TException : Exception, new() => ThrowHelper.LocalizedThrowIgnore<TException>(messageId, innerException, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with a localization message pageId and IgnoreExceptions policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    public static void LocalizedThrowIgnore<TException>(string messageId) where TException : Exception, new() => ThrowHelper.LocalizedThrowIgnore<TException>(messageId, (Exception) null, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with a localization message pageId and IgnoreExceptions policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrowIgnore<TException>(
      string messageId,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrowIgnore<TException>(messageId, (Exception) null, messageParams);
    }

    /// <summary>
    /// Throw an exception with IgnoreExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void ThrowIgnore<TException>(
      string message,
      Exception innerException,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.Throw<TException>(ExceptionPolicyName.IgnoreExceptions, message, innerException, messageParams);
    }

    /// <summary>
    /// Throw an exception with IgnoreExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    public static void ThrowIgnore<TException>(string message, Exception innerException) where TException : Exception, new() => ThrowHelper.ThrowIgnore<TException>(message, innerException, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with IgnoreExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void ThrowIgnore<TException>(string message, params object[] messageParams) where TException : Exception, new() => ThrowHelper.ThrowIgnore<TException>(message, (Exception) null, messageParams);

    /// <summary>
    /// Throw an exception with IgnoreExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    public static void ThrowIgnore<TException>(string message) where TException : Exception, new() => ThrowHelper.ThrowIgnore<TException>(message, (Exception) null, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with IgnoreExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    public static void ThrowIgnore<TException>() where TException : Exception, new() => ThrowHelper.ThrowIgnore<TException>(string.Empty);

    /// <summary>
    /// Throw an exception with a localization message pageId and UnhandledExceptions policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrowUnhandled<TException>(
      string messageId,
      Exception innerException,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrow<TException>(ExceptionPolicyName.UnhandledExceptions, messageId, innerException, messageParams);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and UnhandledExceptions policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    /// <param name="innerException">Inner exception</param>
    public static void LocalizedThrowUnhandled<TException>(
      string messageId,
      Exception innerException)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrowUnhandled<TException>(messageId, innerException, ThrowHelper.emptyObjects);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and UnhandledExceptions policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void LocalizedThrowUnhandled<TException>(
      string messageId,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.LocalizedThrowUnhandled<TException>(messageId, (Exception) null, messageParams);
    }

    /// <summary>
    /// Throw an exception with a localization message pageId and UnhandledExceptions policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="messageId">Id of the message</param>
    public static void LocalizedThrowUnhandled<TException>(string messageId) where TException : Exception, new() => ThrowHelper.LocalizedThrowUnhandled<TException>(messageId, (Exception) null, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with UnhandledExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void ThrowUnhandled<TException>(
      string message,
      Exception innerException,
      params object[] messageParams)
      where TException : Exception, new()
    {
      ThrowHelper.Throw<TException>(ExceptionPolicyName.UnhandledExceptions, message, innerException, messageParams);
    }

    /// <summary>
    /// Throw an exception with UnhandledExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="innerException">Inner exception</param>
    public static void ThrowUnhandled<TException>(string message, Exception innerException) where TException : Exception, new() => ThrowHelper.ThrowUnhandled<TException>(message, innerException, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with UnhandledExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    /// <param name="messageParams">Paramerters to pass to String.Format to format the message</param>
    public static void ThrowUnhandled<TException>(string message, params object[] messageParams) where TException : Exception, new() => ThrowHelper.ThrowUnhandled<TException>(message, (Exception) null, messageParams);

    /// <summary>
    /// Throw an exception with UnhandledExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    /// <param name="message">Message for the exception to throw</param>
    public static void ThrowUnhandled<TException>(string message) where TException : Exception, new() => ThrowHelper.ThrowUnhandled<TException>(message, (Exception) null, ThrowHelper.emptyObjects);

    /// <summary>
    /// Throw an exception with UnhandledExceptions exception policy
    /// </summary>
    /// <typeparam name="TException">Type of the exception to throw</typeparam>
    public static void ThrowUnhandled<TException>() where TException : Exception, new() => ThrowHelper.ThrowUnhandled<TException>(string.Empty);
  }
}
