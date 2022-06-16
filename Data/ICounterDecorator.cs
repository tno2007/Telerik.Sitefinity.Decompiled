// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ICounterDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// 
  /// </summary>
  public interface ICounterDecorator
  {
    /// <summary>
    /// Creates a new or resets existing counter with the specified initial value.
    /// </summary>
    /// <param name="name">The name of the counter.</param>
    /// <param name="initialValue">The initial value.</param>
    void InitCounter(string name, long initialValue);

    /// <summary>Deletes the counter.</summary>
    /// <param name="name">The name.</param>
    void DeleteCounter(string name);

    /// <summary>Gets the next value.</summary>
    /// <param name="name">The name of the counter.</param>
    /// <param name="incrementStep">The increment step.</param>
    /// <returns></returns>
    long GetNextValue(string name, int incrementStep = 1);

    /// <summary>Gets the current value.</summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    long GetCurrentValue(string name);
  }
}
