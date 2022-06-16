// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Decorators.IExecutionStateDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data.Decorators
{
  /// <summary>
  /// Decorators implementing this interface support temporary storage of transactional data that can be processed after the main transaction is committed
  /// </summary>
  public interface IExecutionStateDecorator
  {
    object GetExecutionStateData(string stateBagKey);

    void SetExecutionStateData(string stateBagKey, object data);

    void ClearExecutionStateBag();
  }
}
