// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Proxy.FieldsBackendSectionProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.DynamicModules.Builder.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Proxy
{
  internal class FieldsBackendSectionProxy : IFieldsBackendSection
  {
    public FieldsBackendSectionProxy(DynamicModuleTypeProxy type, FieldsBackendSection source)
    {
      this.Type = type;
      this.Id = source.Id;
      this.ExpandText = source.ExpandText;
      this.IsExpandable = source.IsExpandable;
      this.IsExpandedByDefault = source.IsExpandedByDefault;
      this.Name = source.Name;
      this.Title = source.Title;
    }

    public DynamicModuleTypeProxy Type { get; private set; }

    public string ExpandText { get; private set; }

    public Guid Id { get; private set; }

    public bool IsExpandable { get; private set; }

    public bool IsExpandedByDefault { get; private set; }

    public string Name { get; private set; }

    public int Ordinal { get; private set; }

    public string Title { get; private set; }
  }
}
