// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.Messages.FieldValueMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.InlineEditing.Messages
{
  /// <summary>
  /// Represents the field value message for the methodt that sets the field value
  /// Returns the response object of type Stream
  /// </summary>
  public class FieldValueMessage
  {
    public string DataItemId { set; get; }

    public string ItemType { set; get; }

    public string FieldType { set; get; }

    public string FieldName { set; get; }

    public string Provider { set; get; }
  }
}
