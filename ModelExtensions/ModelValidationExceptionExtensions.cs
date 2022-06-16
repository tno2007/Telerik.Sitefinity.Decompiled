// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ModelValidationExceptionExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity
{
  internal static class ModelValidationExceptionExtensions
  {
    public static string GetMessage(this ModelValidationException exception) => Res.Get(exception.ResourceKey, exception.ResourceClassId, SystemManager.CurrentContext.Culture, true, false) ?? exception.Message;
  }
}
