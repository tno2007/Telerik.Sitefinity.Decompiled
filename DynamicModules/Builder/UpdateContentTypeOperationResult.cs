// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.UpdateContentTypeOperationResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  internal class UpdateContentTypeOperationResult
  {
    public bool UpdateContentTypeParent { get; set; }

    public bool UpdateSelfReferencing { get; set; }

    public Guid ModuleTypeId { get; set; }

    public Guid NewParentTypeId { get; set; }
  }
}
