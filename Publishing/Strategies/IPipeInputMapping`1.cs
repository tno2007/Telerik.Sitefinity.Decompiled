// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Strategies.IPipeInputMapping`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Publishing.Strategies
{
  /// <summary>Interface for converting the incoming items</summary>
  /// <typeparam name="TIn"></typeparam>
  public interface IPipeInputMapping<TIn>
  {
    /// <summary>
    /// Implements the logic that converts the input object to <see cref="T:Telerik.Sitefinity.Publishing.IPublishingObject" />
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>The converted item as <see cref="T:Telerik.Sitefinity.Publishing.IPublishingObject" /></returns>
    IPublishingObject ToPublishingObject(TIn obj);
  }
}
