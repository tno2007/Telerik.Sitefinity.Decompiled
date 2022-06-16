// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InlineEditing.Messages.EditableItemMessage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.InlineEditing.Messages
{
  /// <summary>Represents the ServiceStack message for the temp item</summary>
  public class EditableItemMessage
  {
    private List<FieldValueModel> fields;

    public string ItemId { get; set; }

    public string ItemType { get; set; }

    public string Provider { get; set; }

    public List<FieldValueModel> Fields
    {
      get
      {
        if (this.fields == null)
          this.fields = new List<FieldValueModel>();
        return this.fields;
      }
      set => this.fields = value;
    }
  }
}
