// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.Messages.RenderMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.InlineEditing.Messages
{
  /// <summary>
  /// Represents the ServiceStack message for the render method
  /// </summary>
  public class RenderMessage
  {
    public string PageId { get; set; }

    public string ControlId { get; set; }

    public string DataItemId { get; set; }

    public string FieldName { get; set; }

    public string Url { get; set; }
  }
}
