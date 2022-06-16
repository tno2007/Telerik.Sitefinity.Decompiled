// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.CacheProvidersSettingsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>Cache providers settings element.</summary>
  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
  public class CacheProvidersSettingsElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.CacheProvidersSettingsElement" /> class.
    /// </summary>
    public CacheProvidersSettingsElement()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.CacheProvidersSettingsElement" /> class.
    /// </summary>
    /// <param name="parent">The parent</param>
    public CacheProvidersSettingsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.CacheProvidersSettingsElement" /> class.
    /// </summary>
    /// <param name="check">if set to <c>true</c> [check].</param>
    internal CacheProvidersSettingsElement(bool check)
      : base(check)
    {
    }

    /// <summary>Gets or sets the settings for SqlServer</summary>
    /// <value>The Redis settings.</value>
    [ConfigurationProperty("sqlServerSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = null, Title = "SqlServerSettingsCaption")]
    public SqlServerConfigElement SqlServer
    {
      get => (SqlServerConfigElement) this["sqlServerSettings"];
      set => this["sqlServerSettings"] = (object) value;
    }

    /// <summary>Gets or sets the settings for Redis</summary>
    /// <value>The Redis settings.</value>
    [ConfigurationProperty("redisSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "RedisSettingsCaption")]
    public RedisConfigElement Redis
    {
      get => (RedisConfigElement) this["redisSettings"];
      set => this["redisSettings"] = (object) value;
    }

    /// <summary>Gets or sets the settings for Memcached</summary>
    /// <value>The Redis settings.</value>
    [ConfigurationProperty("memcachedSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "MemcachedSettingsCaption")]
    public MemcachedConfigElement Memcached
    {
      get => (MemcachedConfigElement) this["memcachedSettings"];
      set => this["memcachedSettings"] = (object) value;
    }

    /// <summary>Gets or sets the settings for AwsDynamoDB</summary>
    /// <value>The AwsDynamoDB settings.</value>
    [ConfigurationProperty("awsDynamoDBSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Title = "AwsDynamoDBSettingsCaption")]
    public AwsDynamoDBConfigElement AwsDynamoDB
    {
      get => (AwsDynamoDBConfigElement) this["awsDynamoDBSettings"];
      set => this["awsDynamoDBSettings"] = (object) value;
    }

    internal class PropNames
    {
      internal const string Memcached = "memcachedSettings";
      internal const string Redis = "redisSettings";
      internal const string SqlServer = "sqlServerSettings";
      internal const string AwsDynamoDB = "awsDynamoDBSettings";
    }
  }
}
