// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Data.Contracts.FieldPermissionsCollection
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.DynamicModules.Builder.Data.Contracts
{
  public class FieldPermissionsCollection
  {
    private List<FieldsPermissionSettings> fieldPermissionSettings;

    public List<FieldsPermissionSettings> FieldPermissionSettings
    {
      get
      {
        if (this.fieldPermissionSettings == null)
          this.fieldPermissionSettings = new List<FieldsPermissionSettings>();
        return this.fieldPermissionSettings;
      }
      set => this.fieldPermissionSettings = value;
    }
  }
}
