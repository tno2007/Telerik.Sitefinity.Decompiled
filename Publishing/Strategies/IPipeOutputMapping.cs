// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Strategies.IPipeOutputMapping
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Publishing.Strategies
{
  /// <summary>
  /// Interface that allows transformations to be applied on the incoming <see cref="T:Telerik.Sitefinity.Publishing.IPublishingObject" />
  /// </summary>
  public interface IPipeOutputMapping
  {
    /// <summary>
    /// Transforms the incoming <see cref="T:Telerik.Sitefinity.Publishing.IPublishingObject" />
    /// </summary>
    /// <param name="pubObj">The incoming item</param>
    /// <returns>The transformed item</returns>
    IPublishingEvent FromPublishignObject(object pubObj);
  }
}
