// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.Messages.ImageDataMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;
using System.IO;

namespace Telerik.Sitefinity.Services.InlineEditing.Messages
{
  /// <summary>
  /// Represents the ServiceStack message for retrieving data for images
  /// </summary>
  public class ImageDataMessage : IReturn<Stream>, IReturn
  {
    public string ImageId { get; set; }

    public string ProviderName { get; set; }
  }
}
