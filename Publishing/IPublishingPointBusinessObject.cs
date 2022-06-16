// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.IPublishingPointBusinessObject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Publishing Point Interface</summary>
  public interface IPublishingPointBusinessObject
  {
    /// <summary>Initializes the specified settings.</summary>
    /// <param name="settings">The settings.</param>
    void Initialize(PublishingPoint settings);

    /// <summary>Gets the publishing point items.</summary>
    /// <returns></returns>
    IQueryable<WrapperObject> GetPublishingPointItems();

    /// <summary>Gets the inbound pipes.</summary>
    /// <value>The inbound pipes.</value>
    List<IInboundPipe> InboundPipes { get; }

    /// <summary>Gets the outbound pipes.</summary>
    /// <value>The outbound pipes.</value>
    List<IOutboundPipe> OutboundPipes { get; }

    /// <summary>Calls the inbound pipes.</summary>
    /// <param name="items">The items.</param>
    void CallInboundPipes(IList<PublishingSystemEventInfo> items);

    /// <summary>Calls the outbound pipes.</summary>
    /// <param name="items">The items.</param>
    void CallOutboundPipes(IList<PublishingSystemEventInfo> items);

    /// <summary>Adds the items.</summary>
    /// <param name="items">The items.</param>
    void AddItems(IList<WrapperObject> items);

    /// <summary>Removes the items.</summary>
    /// <param name="items">The items.</param>
    void RemoveItems(IList<WrapperObject> items);

    /// <summary>Puts the items.</summary>
    /// <param name="items">The items.</param>
    void PutItems(IList<PublishingSystemEventInfo> items);
  }
}
