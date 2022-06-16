// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Logging.LogHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Packaging.Content;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Packaging.Logging
{
  /// <summary>Represents the log for the Packaging functionality.</summary>
  internal static class LogHelper
  {
    private const string LogExportRequestedMessageFormat = "Content export requested by {0} with the following settings: TypeName: {1}; ProviderName: [{2}]; Languages: [{3}]; ItemsFilterExpression: {4}; ItemsSortExpression: {5}";
    private const string LogExportStartMessageFormat = "Content export started.";
    private const string LogExportSucceededMessageFormat = "Content export successfully completed for {0} items [{1} succeeded, {2} failed].";
    private const string LogExportFailedMessageFormat = "Content export failed [{0} total items; {1} successful items; {2} failed items] - {3}";
    private const string SuccessfullyExportedItemMessageFormat = "Content item {0} (Id '{1}') was successfully exported. [{2} successful out of {3} total so far]";
    private const string UnsuccessfullyExportedItemMessageFormat = "Content item {0} (Id '{1}') was not exported. [{2} failed out of {3} total so far] Reason: {4}.";
    private const string LogImportRequestedMessageFormat = "Content import requested by {0} with the following settings: TypeName: {1}";
    private const string LogImportStartMessageFormat = "Content import started.";
    private const string LogImportSucceededMessageFormat = "Content import successfully completed for {0} items [{1} succeeded, {2} failed].";
    private const string LogImportFailedMessageFormat = "Content import failed [{0} total items; {1} successful items; {2} failed items] - {3}";
    private const string SuccessfullyImportedItemMessageFormat = "Content item {0} (Id '{1}') was successfully imported. [{2} successful out of {3} total so far]";
    private const string UnsuccessfullyImportedItemMessageFormat = "Content item {0} (Id '{1}') was not imported. [{2} failed out of {3} total so far] Reason: {4}.";

    /// <summary>Writes into the packaging log.</summary>
    /// <param name="message">The message.</param>
    public static void ExportWrite(object message) => LogHelper.InnerWrite(ConfigurationPolicy.PackagingTrace, message);

    /// <summary>Writes into the packaging log.</summary>
    /// <param name="format">The format.</param>
    /// <param name="args">The arguments.</param>
    public static void ExportWriteFormat(string format, params object[] args) => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, format, args);

    /// <summary>Logs the export requested.</summary>
    /// <param name="parameters">The parameters.</param>
    public static void LogExportRequested(ExportParams parameters)
    {
      string str1 = parameters.ProviderName == null ? string.Empty : parameters.ProviderName;
      string str2 = parameters.Languages == null ? string.Empty : string.Join(", ", parameters.Languages);
      LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content export requested by {0} with the following settings: TypeName: {1}; ProviderName: [{2}]; Languages: [{3}]; ItemsFilterExpression: {4}; ItemsSortExpression: {5}", (object) LogHelper.GetCurrentUsername(), (object) (parameters.TypeName ?? string.Empty), (object) str1, (object) str2, (object) (parameters.ItemsFilterExpression ?? string.Empty), (object) (parameters.ItemsSortExpression ?? string.Empty));
    }

    /// <summary>Logs the export start.</summary>
    public static void LogExportStart() => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content export started.");

    /// <summary>Logs the export succeeded.</summary>
    /// <param name="successfulItems">The successful items count.</param>
    /// <param name="failedItems">The failed items count.</param>
    public static void LogExportSucceeded(int successfulItems, int failedItems) => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content export successfully completed for {0} items [{1} succeeded, {2} failed].", (object) (successfulItems + failedItems), (object) successfulItems, (object) failedItems);

    /// <summary>Logs the export failed.</summary>
    /// <param name="successfulItems">The successful items count.</param>
    /// <param name="failedItems">The failed items count.</param>
    /// <param name="exception">The exception.</param>
    public static void LogExportFailed(int successfulItems, int failedItems, Exception exception) => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content export failed [{0} total items; {1} successful items; {2} failed items] - {3}", (object) (successfulItems + failedItems), (object) successfulItems, (object) failedItems, (object) exception.Message);

    /// <summary>Logs the object export succeeded.</summary>
    /// <param name="obj">The object.</param>
    /// <param name="successfulItems">The successful items.</param>
    /// <param name="progress">The progress.</param>
    public static void LogObjectExportSucceeded(
      WrapperObject obj,
      int successfulItems,
      int progress)
    {
      if (obj == null)
        return;
      LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content item {0} (Id '{1}') was successfully exported. [{2} successful out of {3} total so far]", obj.GetPropertyOrDefault<object>("Title") ?? (object) string.Empty, (object) obj.GetPropertyOrDefault<Guid>("objectId"), (object) successfulItems, (object) progress);
    }

    /// <summary>Logs the object export failed.</summary>
    /// <param name="obj">The object.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="failedItems">The failed items.</param>
    /// <param name="progress">The progress.</param>
    public static void LogObjectExportFailed(
      WrapperObject obj,
      Exception exception,
      int failedItems,
      int progress)
    {
      if (obj == null)
        return;
      LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content item {0} (Id '{1}') was not exported. [{2} failed out of {3} total so far] Reason: {4}.", obj.GetPropertyOrDefault<object>("Title") ?? (object) string.Empty, (object) obj.GetPropertyOrDefault<Guid>("objectId"), (object) failedItems, (object) progress, (object) exception.Message);
    }

    /// <summary>Writes into the packaging log.</summary>
    /// <param name="message">The message.</param>
    public static void ImportWrite(object message) => LogHelper.InnerWrite(ConfigurationPolicy.PackagingTrace, message);

    /// <summary>Writes into the packaging log.</summary>
    /// <param name="format">The format.</param>
    /// <param name="args">The arguments.</param>
    public static void ImportWriteFormat(string format, params object[] args) => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, format, args);

    /// <summary>Logs the import requested.</summary>
    /// <param name="parameters">The parameters.</param>
    public static void LogImportRequested(ImportParams parameters) => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content import requested by {0} with the following settings: TypeName: {1}", (object) LogHelper.GetCurrentUsername(), (object) (parameters.TypeName ?? string.Empty));

    /// <summary>Logs the import start.</summary>
    public static void LogImportStart() => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content import started.");

    /// <summary>Logs the import succeeded.</summary>
    /// <param name="successfulItemsCount">The successful items count.</param>
    /// <param name="failedItemsCount">The failed items count.</param>
    public static void LogImportSucceeded(int successfulItemsCount, int failedItemsCount) => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content import successfully completed for {0} items [{1} succeeded, {2} failed].", (object) (successfulItemsCount + failedItemsCount), (object) successfulItemsCount, (object) failedItemsCount);

    /// <summary>Logs the import failed.</summary>
    /// <param name="successfulItems">The successful items count.</param>
    /// <param name="failedItems">The failed items count.</param>
    /// <param name="exception">The exception.</param>
    public static void LogImportFailed(int successfulItems, int failedItems, Exception exception) => LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content import failed [{0} total items; {1} successful items; {2} failed items] - {3}", (object) (successfulItems + failedItems), (object) successfulItems, (object) failedItems, (object) exception.Message);

    /// <summary>Logs the object import succeeded.</summary>
    /// <param name="obj">The object.</param>
    /// <param name="successfulItems">The successful items.</param>
    /// <param name="progress">The progress.</param>
    public static void LogObjectImportSucceeded(
      WrapperObject obj,
      int successfulItems,
      int progress)
    {
      if (obj == null)
        return;
      LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content item {0} (Id '{1}') was successfully imported. [{2} successful out of {3} total so far]", obj.GetPropertyOrDefault<object>("Title") ?? (object) string.Empty, (object) obj.GetPropertyOrDefault<Guid>("objectId"), (object) successfulItems, (object) progress);
    }

    /// <summary>Logs the object import failed.</summary>
    /// <param name="obj">The object.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="failedItems">The failed items.</param>
    /// <param name="progress">The progress.</param>
    public static void LogObjectImportFailed(
      WrapperObject obj,
      Exception exception,
      int failedItems,
      int progress)
    {
      if (obj == null)
        return;
      LogHelper.InnerWriteFormat(ConfigurationPolicy.PackagingTrace, "Content item {0} (Id '{1}') was not imported. [{2} failed out of {3} total so far] Reason: {4}.", obj.GetPropertyOrDefault<object>("Title") ?? (object) string.Empty, (object) obj.GetPropertyOrDefault<Guid>("objectId"), (object) failedItems, (object) progress, (object) exception.Message);
    }

    private static void InnerWrite(ConfigurationPolicy policy, object message) => Log.Write(message, policy);

    [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Not needed", MessageId = "System.String.Format(System.String,System.Object[])")]
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    private static void InnerWriteFormat(
      ConfigurationPolicy policy,
      string format,
      params object[] args)
    {
      try
      {
        string message = string.Format(format, args);
        LogHelper.InnerWrite(policy, (object) message);
      }
      catch
      {
      }
    }

    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    private static string GetCurrentUsername()
    {
      try
      {
        return ClaimsManager.GetCurrentIdentity().Name ?? "N/A";
      }
      catch
      {
        return "N/A";
      }
    }
  }
}
