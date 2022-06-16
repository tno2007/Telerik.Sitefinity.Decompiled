// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Model.Extensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.DynamicModules.Builder.Model
{
  internal static class Extensions
  {
    internal static string GetFullTypeName(this IDynamicModuleField field) => field.FieldNamespace + "." + field.Name;

    internal static string GetFullTypeName(this IDynamicModuleType type) => type.TypeNamespace + "." + type.TypeName;
  }
}
