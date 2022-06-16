// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.AppStatus.SchemaMigrationReportGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.OpenAccess;

namespace Telerik.Sitefinity.AppStatus
{
  internal class SchemaMigrationReportGenerator
  {
    private readonly SchemaUpdateInfo info;

    internal SchemaMigrationReportGenerator(SchemaUpdateInfo info) => this.info = info != null ? info : throw new ArgumentNullException(nameof (info));

    internal SchemaMigrationReport GenerateReport() => new SchemaMigrationReport()
    {
      MigrationType = this.GetMigrationType(),
      NewEntities = this.GetNewEntitiesReport(),
      ModifiedEntities = this.GetModifiedEntitiesReport(),
      RemovedEntities = this.GetRemovedEntitiesReport(),
      TempTableUsage = this.GetTempTableUsageReport()
    };

    private SchemaMigrationType GetMigrationType()
    {
      if (this.info.IsExtending)
        return SchemaMigrationType.Extending;
      return this.info.IsComplex ? SchemaMigrationType.Complex : SchemaMigrationType.Trivial;
    }

    private IEnumerable<string> GetNewEntitiesReport()
    {
      if (this.info.AddsColumnOnExistingTable)
        yield return "Adding a column(s) on an existing table(s).";
      if (this.info.AddsFKConstraintOnExistingTable)
        yield return "Adding a foreign key constraint(s) to an existing table(s).";
      if (this.info.AddsFKConstraintOnNewTable)
        yield return "Adding a foreign key constraint(s) to a new table(s), the create table statement(s) is included as well.";
      if (this.info.AddsIndexOnExistingTable)
        yield return "Adding an index(indices) to an existing table(s).";
      if (this.info.AddsIndexOnNewTable)
        yield return "Adding an index(indices) definition for a new table(s), the create table statement(s) is included as well.";
      if (this.info.AddsNewTable)
        yield return "Adding a new table(s).";
      if (this.info.AddsOther)
        yield return "Adding structures independent of existing.";
      if (this.info.AddsPKConstraintOnExistingTable)
        yield return "Adding a primary key constraint(s) to an existing table(s).";
      if (this.info.AddsPKConstraintOnNewTable)
        yield return "Adding a primary key constraint(s) to an new table(s), the create table statement is included as well.";
    }

    private IEnumerable<string> GetModifiedEntitiesReport()
    {
      if (this.info.ModifiesColumnOnExistingTable)
        yield return "Modifying a column(s) on an existing table(s).";
      if (this.info.ModifiesOther)
        yield return "Modifying existing structures.";
    }

    private IEnumerable<string> GetRemovedEntitiesReport()
    {
      if (this.info.RemovesColumnOnExistingTable)
        yield return "Removing a column(s) from an existing table(s).";
      if (this.info.RemovesFKConstraintFromExistingTable)
        yield return "Removing a foreign key constraint(s) from an existing table(s).";
      if (this.info.RemovesIndexOnExistingTable)
        yield return "Removing an index(indices) on an existing table(s).";
      if (this.info.RemovesPKConstraintFromExistingTable)
        yield return "Removing a primary key constraint(s) from an existing table(s).";
    }

    private IEnumerable<string> GetTempTableUsageReport()
    {
      if (this.info.UsesTemporaryTable)
        yield return "Temporary table(s) usage to migrate existing data during structural table changes.";
    }
  }
}
