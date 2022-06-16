// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SmtpElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using System.Net.Mail;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>SMTP settings for Sitefinity</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "SmtpConfigDescription", Title = "SmtpConfigCaption")]
  [Obsolete("Use the Notifications service to register the SMTP client settings")]
  public class SmtpElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.SmtpElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public SmtpElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the name or IP address of the host used for SMTP transactions.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpHost")]
    [ConfigurationProperty("host", DefaultValue = "")]
    public string Host
    {
      get => this["host"] is string str ? str : (string) null;
      set => this["host"] = (object) value;
    }

    /// <summary>Gets or sets the port used for SMTP transactions.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpPort")]
    [ConfigurationProperty("port", DefaultValue = 25)]
    public int Port
    {
      get => this["port"] as int? ?? 0;
      set => this["port"] = value >= 0 ? (object) value : throw new ConfigurationException(Res.Get<SecurityResources>().SmtpPortMustBeNonNegative);
    }

    /// <summary>The user name associated with the SMTP credentials.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpUserName")]
    [ConfigurationProperty("userName", DefaultValue = "")]
    public string UserName
    {
      get => this["userName"] is string str ? str : (string) null;
      set => this["userName"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the password for the user name associated with the credentials
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpPassword")]
    [ConfigurationProperty("password", DefaultValue = "")]
    [SecretData]
    public string Password
    {
      get => this["password"] is string str ? str : (string) null;
      set => this["password"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the domain or computer name that verifies the credentials.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpDomain")]
    [ConfigurationProperty("domain", DefaultValue = "")]
    public string Domain
    {
      get => this["domain"] is string str ? str : (string) null;
      set => this["domain"] = (object) value;
    }

    /// <summary>
    /// Specifies how outgoing email messages will be handled.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpDeliveryMethod")]
    [ConfigurationProperty("deliveryMethod", DefaultValue = SmtpDeliveryMethod.Network)]
    public SmtpDeliveryMethod DeliveryMethod
    {
      get => this["deliveryMethod"] as SmtpDeliveryMethod? ?? SmtpDeliveryMethod.Network;
      set => this["deliveryMethod"] = (object) value;
    }

    /// <summary>
    /// Specify whether the <see cref="T:System.Net.Mail.SmtpClient" /> uses Secure Sockets
    /// Layer (SSL) to encrypt the connection.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpEnableSSL")]
    [ConfigurationProperty("enableSSL", DefaultValue = false)]
    public bool EnableSSL
    {
      get => this["enableSSL"] as bool? ?? false;
      set => this["enableSSL"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value that specifies the amount of time after which a synchronous
    /// <see cref="!:Overload:System.Net.Mail.SmtpClient.Send" /> call times out.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpTimeout")]
    [ConfigurationProperty("timeout", DefaultValue = 100000)]
    public int Timeout
    {
      get => this["timeout"] as int? ?? 0;
      set => this["timeout"] = value >= 0 ? (object) value : throw new ConfigurationException(Res.Get<SecurityResources>().SmtpTimeoutMustBeNonNegative);
    }

    /// <summary>
    /// Gets or sets the folder where applications save mail messages to be processed
    /// by the local SMTP server.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "SmtpPickupDirectoryLocation")]
    [ConfigurationProperty("pickupDirectoryLocation", DefaultValue = "")]
    public string PickupDirectoryLocation
    {
      get => this["pickupDirectoryLocation"] is string str ? str : (string) null;
      set => this["pickupDirectoryLocation"] = (object) value;
    }

    /// <summary>Sets the default sender email address.</summary>
    /// <value>The default sender email address.</value>
    [DescriptionResource(typeof (ConfigDescriptions), "DefaultSenderEmailAddress")]
    [ConfigurationProperty("DefaultSenderEmailAddress", DefaultValue = "")]
    public string DefaultSenderEmailAddress
    {
      get => this[nameof (DefaultSenderEmailAddress)] is string str ? str : (string) null;
      set => this[nameof (DefaultSenderEmailAddress)] = (object) value;
    }

    /// <summary>Gets or sets the email subject encoding.</summary>
    /// <value>The email subject encoding.</value>
    [DescriptionResource(typeof (ConfigDescriptions), "EmailSubjectEncoding")]
    [ConfigurationProperty("emailSubjectEncoding", DefaultValue = "utf-8")]
    public string EmailSubjectEncoding
    {
      get => this["emailSubjectEncoding"] is string str ? str : (string) null;
      set => this["emailSubjectEncoding"] = (object) value;
    }

    /// <summary>Gets or sets the email body encoding.</summary>
    /// <value>The email body encoding.</value>
    [DescriptionResource(typeof (ConfigDescriptions), "EmailBodyEncoding")]
    [ConfigurationProperty("emailBodyEncoding", DefaultValue = "utf-8")]
    public string EmailBodyEncoding
    {
      get => this["emailBodyEncoding"] is string str ? str : (string) null;
      set => this["emailBodyEncoding"] = (object) value;
    }

    /// <summary>
    /// Constants for the names of the configuration properties
    /// </summary>
    internal static class Names
    {
      internal const string Host = "host";
      internal const string Port = "port";
      internal const string UserName = "userName";
      internal const string Password = "password";
      internal const string Domain = "domain";
      internal const string DeliveryMethod = "deliveryMethod";
      internal const string EnableSSL = "enableSSL";
      internal const string Timeout = "timeout";
      internal const string PickupDirectoryLocation = "pickupDirectoryLocation";
      internal const string defaultSenderEmailAddress = "DefaultSenderEmailAddress";
      internal const string emailBodyEncoding = "emailBodyEncoding";
      internal const string emailSubjectEncoding = "emailSubjectEncoding";
    }
  }
}
