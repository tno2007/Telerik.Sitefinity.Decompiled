// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationFilterExpressionSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.ContentLocations
{
  internal class ContentLocationFilterExpressionSettings
  {
    public ContentLocationFilterExpressionSettings()
      : this("Parent.Id")
    {
    }

    public ContentLocationFilterExpressionSettings(string parentMember) => this.ParentMember = parentMember;

    public string ParentMember { get; set; }

    public bool SkipParentFilter { get; set; }
  }
}
