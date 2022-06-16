// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.Logging.PackagingLog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Text;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Common.Configuration.Fluent;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Services.Cmis.RestAtom;
using Telerik.Sitefinity.SiteSync;

namespace Telerik.Sitefinity.Packaging.Logging
{
  /// <summary>Represents the log for the Packaging functionality.</summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class PackagingLog : ISiteSyncLogger
  {
    internal static readonly string CategoryName = Enum.GetName(typeof (ConfigurationPolicy), (object) ConfigurationPolicy.PackagingTrace);

    /// <summary>Writes the specified log message.</summary>
    /// <param name="message">The message to log.</param>
    /// <param name="args">The arguments for formatting.</param>
    public static void Log(object message, params object[] args) => PackagingLog.Log((Exception) null, message, args);

    /// <summary>Writes the specified message.</summary>
    /// <param name="message">The message.</param>
    public void Write(object message) => PackagingLog.Log((Exception) null, message);

    /// <summary>Writes formatted message.</summary>
    /// <param name="format">The format.</param>
    /// <param name="args">The arguments.</param>
    public void WriteFormat(string format, params object[] args)
    {
      try
      {
        this.Write((object) string.Format(format, args));
      }
      catch
      {
      }
    }

    /// <summary>Prepares the message.</summary>
    /// <param name="messageTemplate">The message template.</param>
    /// <param name="errorMessageTemplate">The error message template.</param>
    /// <param name="item">The item.</param>
    /// <param name="itemId">The item identifier.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="title">The title.</param>
    /// <param name="action">The action.</param>
    /// <param name="ex">The ex.</param>
    /// <returns>The prepared message.</returns>
    public string PrepareMessage(
      string messageTemplate,
      string errorMessageTemplate,
      WrapperObject item,
      object itemId,
      string itemType,
      string itemProvider,
      string title,
      string action,
      Exception ex)
    {
      StringBuilder stringBuilder = new StringBuilder();
      try
      {
        if (item != null)
        {
          if (title == null)
            title = Reader.GetLstringValue(item.GetPropertyOrDefault<object>("Title"));
          if (itemProvider == null)
          {
            object propertyOrDefault = item.GetPropertyOrDefault<object>("Provider");
            itemProvider = propertyOrDefault != null ? propertyOrDefault.ToString() : string.Empty;
          }
          if (itemId is Guid guid && guid == Guid.Empty)
            itemId = (object) item.GetPropertyOrDefault<Guid>("ItemId");
          if (action == null)
            action = item.GetPropertyOrDefault<string>("ItemAction");
        }
        if (!string.IsNullOrEmpty(title))
        {
          if (ex == null)
            stringBuilder.AppendFormat(messageTemplate, (object) title);
          else
            stringBuilder.AppendFormat(errorMessageTemplate, (object) title);
        }
        else if (ex == null)
          stringBuilder.Append(messageTemplate);
        else
          stringBuilder.Append(errorMessageTemplate);
        stringBuilder.AppendLine();
        stringBuilder.AppendFormat("Item information: id = '{0}'; type = '{1}'; provider = '{2}'; action = '{3}'", itemId, (object) itemType, (object) itemProvider, (object) action);
      }
      catch (Exception ex1)
      {
        if (Exceptions.HandleException(ex1, ExceptionPolicyName.IgnoreExceptions))
          throw;
      }
      if (ex != null)
      {
        stringBuilder.AppendLine();
        stringBuilder.Append("Error details:");
        stringBuilder.Append(ex.ToString());
      }
      return stringBuilder.ToString();
    }

    /// <summary>Writes the specified exception.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="message">The message.</param>
    /// <param name="args">The args.</param>
    internal static void Log(Exception exception, object message, params object[] args) => PackagingLog.Log(exception, out string _, message, args);

    /// <summary>Writes the specified exception.</summary>
    /// <param name="exception">The exception.</param>
    /// <param name="loggedMessage">The logged message.</param>
    /// <param name="message">The message.</param>
    /// <param name="args">The args.</param>
    internal static void Log(
      Exception exception,
      out string loggedMessage,
      object message,
      params object[] args)
    {
      loggedMessage = PackagingLog.BuildMessage(exception, message, args);
      Telerik.Sitefinity.Abstractions.Log.Write((object) loggedMessage, PackagingLog.CategoryName);
      if (exception != null && Exceptions.HandleException(exception, ExceptionPolicyName.IgnoreExceptions))
        throw exception;
    }

    internal static void OnLogConfigured(object sender, LogConfiguredEventArgs args)
    {
      ILoggingConfigurationStart configuration = args.Configuration;
      IFormatterBuilder formatterBuilder = Telerik.Sitefinity.Abstractions.Log.LogFormatTemplate("TimeStampFormatter", "Timestamp: {timestamp}{newline}\nMessage: {message}{newline}\n");
      string categoryName1 = PackagingLog.CategoryName;
      string categoryName2 = PackagingLog.CategoryName;
      IFormatterBuilder formatBuilder = formatterBuilder;
      Telerik.Sitefinity.Abstractions.Log.ConfigureCategory((ILoggingConfigurationContd) configuration, categoryName1, categoryName2, formatBuilder);
    }

    private static string BuildMessage(Exception exception, object message, params object[] args)
    {
      string str = message.ToString();
      if (args.Length != 0)
        str = str.Arrange(args);
      if (exception != null)
        str = string.Format("{0}\r\nOriginal error: {1}.\r\nFor more details, see the error log.", (object) str, (object) exception.Message);
      return str;
    }
  }
}
