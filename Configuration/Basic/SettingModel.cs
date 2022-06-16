// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Basic.SettingModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration.Basic
{
  internal class SettingModel
  {
    public SettingModel()
    {
      this.Title = string.Empty;
      this.Description = string.Empty;
      this.CollectionKey = string.Empty;
    }

    public string Key { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string SectionName { get; set; }

    public string CollectionKey { get; set; }

    public SettingNodeType NodeType { get; set; }

    public SettingModel Parent { get; set; }
  }
}
