// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Api.OData.Exceptions.LockedItemException
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.Api.OData.Exceptions
{
  internal class LockedItemException : ErrorCodeException
  {
    public LockedItemException(string operation)
      : base(LockedItemException.GetError(operation))
    {
    }

    private static string GetError(string operation) => string.Format("Item is locked and operation {0} cannot be performed", (object) operation);
  }
}
