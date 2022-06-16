// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.DynamicModuleTransferObject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Packaging.Package;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>Object used for transferring dynamic module packages</summary>
  internal class DynamicModuleTransferObject : IPackageTransferObject
  {
    private Func<DynamicModule, DateTime, bool, Stream> func;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.DynamicModuleTransferObject" /> class.
    /// </summary>
    /// <param name="func">The function to be executed on GetStream() method</param>
    /// <param name="module">The dynamic module</param>
    /// <param name="name">The name of the transfer object</param>
    public DynamicModuleTransferObject(
      Func<DynamicModule, DateTime, bool, Stream> func,
      DynamicModule module,
      string name)
    {
      this.Module = module;
      this.Name = name;
      this.func = func;
    }

    /// <summary>Gets or sets the dynamic module</summary>
    public DynamicModule Module { get; set; }

    /// <inheritdoc />
    public string Name { get; set; }

    /// <inheritdoc />
    public Stream GetStream(DateTime dateLastUpdated = default (DateTime), bool forceExport = false) => this.func(this.Module, dateLastUpdated, forceExport);
  }
}
