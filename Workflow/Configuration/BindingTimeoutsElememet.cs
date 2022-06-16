// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Configuration.BindingTimeoutsElememet
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Workflow.Configuration
{
  /// <summary>
  /// Configuration element used to specify configuration values of binding timeouts.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "BindingTimeoutsConfigDescription", Title = "BindingTimeoutsConfigCaption")]
  public class BindingTimeoutsElememet : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public BindingTimeoutsElememet(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the interval of time in provided in milliseconds for a connection to close
    /// before the transport raises an exception.
    /// If the value of this property is not set the value of AllPropertiesTimeoutDefaultValue will be applied.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CloseTimeoutDescription", Title = "CloseTimeoutCaption")]
    [ConfigurationProperty("closeTimeout")]
    public int? CloseTimeout
    {
      get => (int?) this["closeTimeout"];
      set => this["closeTimeout"] = (object) value;
    }

    /// <summary>Gets or sets the interval of time in milliseconds provided for a connection to open
    /// before the transport raises an exception.
    /// If the value of this property is not set the value of AllPropertiesTimeoutDefaultValue will be applied.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OpenTimeoutDescription", Title = "OpenTimeoutCaption")]
    [ConfigurationProperty("openTimeout")]
    public int? OpenTimeout
    {
      get => (int?) this["openTimeout"];
      set => this["openTimeout"] = (object) value;
    }

    /// <summary>Gets or sets the interval of time in milliseconds that a connection can remain inactive,
    /// during which no application messages are received, before it is dropped.
    /// If the value of this property is not set the value of AllPropertiesTimeoutDefaultValue will be applied.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ReceiveTimeoutDescription", Title = "ReceiveTimeoutCaption")]
    [ConfigurationProperty("receiveTimeout")]
    public int? ReceiveTimeout
    {
      get => (int?) this["receiveTimeout"];
      set => this["receiveTimeout"] = (object) value;
    }

    /// <summary>Gets or sets the interval of time in milliseconds provided for a write operation to
    /// complete before the transport raises an exception.
    /// If the value of this property is not set the value of AllPropertiesTimeoutDefaultValue will be applied.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SendTimeoutDescription", Title = "SendTimeoutCaption")]
    [ConfigurationProperty("sendTimeout")]
    public int? SendTimeout
    {
      get => (int?) this["sendTimeout"];
      set => this["sendTimeout"] = (object) value;
    }

    /// <summary>Gets or sets the interval of time in milliseconds for setting properties
    /// OpenTimeout, CloseTimeout, ReceiveTimeout and SendTimeout if the properties are not set.
    /// If they are set their value will override value at AllPropertiesTimeoutDefaultValue property.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllPropertiesTimeoutDefaultValueDescription", Title = "AllPropertiesTimeoutDefaultValueCaption")]
    [ConfigurationProperty("allPropertiesTimeoutDefaultValue")]
    public int? AllPropertiesTimeoutDefaultValue
    {
      get => (int?) this["allPropertiesTimeoutDefaultValue"];
      set => this["allPropertiesTimeoutDefaultValue"] = (object) value;
    }

    /// <summary>
    /// Organizational structure holding the names of the properties to be resolved.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string CloseTimeout = "closeTimeout";
      public const string OpenTimeout = "openTimeout";
      public const string ReceiveTimeout = "receiveTimeout";
      public const string SendTimeout = "sendTimeout";
      public const string AllPropertiesTimeoutDefaultValue = "allPropertiesTimeoutDefaultValue";
    }
  }
}
