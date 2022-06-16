// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.MetadataConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>Defines metadata configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "MetadataConfig")]
  public class MetadataConfig : ModuleConfigBase
  {
    /// <summary>Gets a collection of metafield to database mappings</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "DatabaseMappings")]
    [ConfigurationProperty("databaseMappings")]
    public ConfigElementDictionary<string, DatabaseMappingsElement> DatabaseMappings => (ConfigElementDictionary<string, DatabaseMappingsElement>) this["databaseMappings"];

    /// <summary>
    /// Called when the corresponding XML element is read and properties loaded.
    /// </summary>
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.AddMapping(UserFriendlyDataType.ShortText.ToString(), typeof (string), "VARCHAR", "NVARCHAR", dbLength: new int?((int) byte.MaxValue), required: false);
      this.AddMapping(UserFriendlyDataType.LongText.ToString(), typeof (string), "CLOB", "TEXT", required: false);
      this.AddMapping(UserFriendlyDataType.MultipleChoice.ToString(), typeof (string), "VARCHAR", "NVARCHAR", dbLength: new int?((int) byte.MaxValue), required: false);
      this.AddMapping(UserFriendlyDataType.Choices.ToString(), typeof (string), "VARCHAR", "NVARCHAR", dbLength: new int?((int) byte.MaxValue), required: false);
      this.AddMapping(UserFriendlyDataType.YesNo.ToString(), typeof (bool), "BIT", "BIT", required: false);
      this.AddMapping(UserFriendlyDataType.Currency.ToString(), typeof (Decimal?), "DECIMAL", "MONEY", required: false);
      this.AddMapping(UserFriendlyDataType.DateAndTime.ToString(), typeof (DateTime?), "DATE", "DATETIME", required: false);
      this.AddMapping(UserFriendlyDataType.Number.ToString(), typeof (Decimal?), "NUMERIC", "NUMERIC", required: false);
      this.AddMapping(UserFriendlyDataType.Classification.ToString(), typeof (Taxon), "", "", required: false);
      this.AddMapping(UserFriendlyDataType.Image.ToString(), typeof (ContentLink), "", "", required: false);
      this.AddMapping(UserFriendlyDataType.Video.ToString(), typeof (ContentLink), "", "", required: false);
      this.AddMapping(UserFriendlyDataType.ContentLink.ToString(), typeof (ContentLink), "", "", required: false);
      this.AddMapping(UserFriendlyDataType.RelatedMedia.ToString(), typeof (RelatedItems), "", "", required: false);
      this.AddMapping(UserFriendlyDataType.RelatedData.ToString(), typeof (RelatedItems), "", "", required: false);
      this.AddMapping(UserFriendlyDataType.Integer.ToString(), typeof (int), "INTEGER", "INT", required: false);
      this.AddMapping(UserFriendlyDataType.Date.ToString(), typeof (DateTime?), "DATE", "DATETIME", required: false);
      this.AddMapping(UserFriendlyDataType.FileUpload.ToString(), typeof (ContentLink[]), "", "", required: false);
    }

    private void AddMapping(
      string name,
      Type clrType,
      string defaultDbType,
      string dbSqlType,
      string additionalDbTypeChoices = null,
      int? dbLength = null,
      bool required = true)
    {
      this.DatabaseMappings.Add(name, new DatabaseMappingsElement((ConfigElement) this.DatabaseMappings)
      {
        Name = name,
        DbType = defaultDbType,
        DbSqlType = dbSqlType,
        AdditionalDbTypeChoices = string.IsNullOrWhiteSpace(additionalDbTypeChoices) ? (string) null : additionalDbTypeChoices,
        DbLength = !dbLength.HasValue ? (string) null : dbLength.ToString(),
        ClrType = clrType.FullName,
        Nullable = !required
      });
    }

    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores persistent objects metadata in database using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessMetaDataProvider)
      });
    }

    /// <summary>Upgrades the specified old version.</summary>
    /// <param name="oldVersion">The old version.</param>
    /// <param name="newVersion">The new version.</param>
    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      if (!(oldVersion < SitefinityVersion.Sitefinity8_1))
        return;
      string key1 = UserFriendlyDataType.Number.ToString();
      if (this.DatabaseMappings.ContainsKey(key1))
        this.DatabaseMappings[key1].ClrType = typeof (Decimal?).FullName;
      string key2 = UserFriendlyDataType.Currency.ToString();
      if (!this.DatabaseMappings.ContainsKey(key2))
        return;
      this.DatabaseMappings[key2].ClrType = typeof (Decimal?).FullName;
    }
  }
}
