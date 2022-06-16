// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.OperationReason
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Define a class for structuring a restart reason object
  /// </summary>
  public class OperationReason
  {
    private Dictionary<string, string> details;
    private const char Separator = '|';
    private const char DetailSeparator = ':';

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReason" /> class that represents unknown reason.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReason" /> class that represents unknown reason</returns>
    public static OperationReason UnknownReason() => OperationReason.FromKey("Unknown");

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReason" /> class from the specified reason key.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReason" /> class.</returns>
    public static OperationReason FromKey(string key) => new OperationReason(key);

    /// <summary>
    /// Converts a string representation of the restart reason to a new instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReason" /> class.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReason" /> class.</returns>
    public static OperationReason Parse(string data)
    {
      if (data.IsNullOrEmpty())
        return OperationReason.UnknownReason();
      string[] strArray = data.Split('|');
      OperationReason operationReason = OperationReason.FromKey(strArray[0]);
      if (strArray.Length > 1)
      {
        for (int index = 1; index < strArray.Length; ++index)
        {
          string key = strArray[index];
          int length = key.IndexOf(':');
          if (length > -1)
            operationReason.AddInfo(key.Substring(0, length), key.Substring(length + 1));
          else
            operationReason.AddInfo(key);
        }
      }
      return operationReason;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.OperationReason" /> class.
    /// </summary>
    /// <param name="key">The key.</param>
    protected internal OperationReason(string key) => this.Key = !string.IsNullOrEmpty(key) ? key.Replace('|', '-').Replace(':', '-') : throw new ArgumentNullException(nameof (key));

    /// <summary>Gets the key of the reason.</summary>
    /// <value>The key.</value>
    public string Key { get; private set; }

    /// <summary>Gets the additional info of the reason.</summary>
    /// <value>The additional info.</value>
    public IDictionary<string, string> AdditionalInfo
    {
      get
      {
        if (this.details == null)
          this.details = new Dictionary<string, string>();
        return (IDictionary<string, string>) this.details;
      }
    }

    /// <summary>Gets or sets the message.</summary>
    /// <value>The message.</value>
    internal string Message { get; set; }

    /// <summary>
    /// Returns a <see cref="T:System.String" /> that represents the current
    /// <see cref="T:System.Object" />.
    /// </summary>
    /// <returns>
    /// A <see cref="T:System.String" /> that represents the current <see cref="T:System.Object" />.
    /// </returns>
    public override string ToString()
    {
      if (this.details == null || this.details.Count <= 0)
        return this.Key;
      StringBuilder stringBuilder = new StringBuilder(this.Key);
      foreach (KeyValuePair<string, string> detail in this.details)
      {
        stringBuilder.Append('|');
        stringBuilder.Append(detail.Key);
        if (detail.Value.Length > 0)
        {
          stringBuilder.Append(':');
          stringBuilder.Append(detail.Value);
        }
      }
      return stringBuilder.ToString();
    }

    /// <summary>Adds the info.</summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void AddInfo(string key, string value = null) => this.AdditionalInfo[key] = value ?? string.Empty;

    /// <summary>Gets the user friendly reason.</summary>
    /// <returns>The user friendly reason message.</returns>
    internal string GetUserFriendlyReason()
    {
      string userFriendlyReason = this.Message.IsNullOrEmpty() ? Res.Get("OperationReasonResources", this.Key, (CultureInfo) null, false, false) : this.Message;
      if (userFriendlyReason.IsNullOrEmpty())
        userFriendlyReason = this.Key;
      if (this.details == null || this.details.Count <= 0)
        return userFriendlyReason;
      StringBuilder stringBuilder = new StringBuilder(userFriendlyReason);
      foreach (KeyValuePair<string, string> detail in this.details)
      {
        stringBuilder.Append('|');
        stringBuilder.Append(detail.Key);
        if (detail.Value.Length > 0)
        {
          stringBuilder.Append(':');
          stringBuilder.Append(detail.Value);
        }
      }
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Common known reason keys for application restart or model reset
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct KnownKeys
    {
      /// <summary>
      /// A key for unknown reason, used for RestartApplication or ResetModel
      /// </summary>
      public const string UnknownReason = "Unknown";
      /// <summary>
      /// A key for a reason for static modules operations, used for RestartApplication
      /// </summary>
      public const string StaticModulesUpdate = "StaticModulesUpdate";
      /// <summary>
      /// A key for localization changes reason, used for RestartApplication or ResetModel
      /// </summary>
      public const string LocalizationChange = "LocalizationChange";
      /// <summary>
      /// A key for License changes reason, used for RestartApplication
      /// </summary>
      public const string LicenseUpdate = "LicenseUpdate";
      /// <summary>
      /// A key for import dynamic module reason, used for RestartApplication
      /// </summary>
      public const string DynamicModuleImport = "DynamicModuleImport";
      /// <summary>
      /// A key for install dynamic module reason, used for RestartApplication
      /// </summary>
      public const string DynamicModuleInstall = "DynamicModuleInstall";
      /// <summary>
      /// A key for uninstall dynamic module reason, used for RestartApplication
      /// </summary>
      public const string DynamicModuleUninstall = "DynamicModuleUninstall";
      /// <summary>
      /// A key for delete dynamic module reason, used for RestartApplication
      /// </summary>
      public const string DynamicModuleDelete = "DynamicModuleDelete";
      /// <summary>
      /// A key for install dynamic type reason, used for RestartApplication
      /// </summary>
      public const string DynamicModuleTypeInstall = "DynamicModuleTypeInstall";
      /// <summary>
      /// A key for delete dynamic type reason, used for RestartApplication
      /// </summary>
      public const string DynamicModuleTypeDelete = "DynamicModuleTypeDelete";
      /// <summary>A key for MetaData change reason, used for ResetModel</summary>
      public const string MetaDataChange = "MetaDataChange";
      internal const string ConfigChange = "ConfigChange";
      internal const string InternalRestartOnUpgrade = "InternalRestartOnUpgrade";
      internal const string DatabaseCleanAfterDynamicTypesOrFieldsAreDeleted = "DatabaseCleanAfterDynamicTypesOrFieldsAreDeleted";
    }
  }
}
