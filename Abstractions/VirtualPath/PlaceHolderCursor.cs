// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.VirtualPath.PlaceHolderCursor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Text;

namespace Telerik.Sitefinity.Abstractions.VirtualPath
{
  public class PlaceHolderCursor
  {
    public PlaceHolderCursor(string name, int position)
      : this(name, position, string.Empty)
    {
    }

    public PlaceHolderCursor(string name, int position, string parentHolder)
    {
      this.Name = name;
      this.Position = position;
      this.Output = new StringBuilder();
      this.ParentHolder = parentHolder;
    }

    public string Name { get; private set; }

    public int Position { get; private set; }

    public StringBuilder Output { get; private set; }

    public string ParentHolder { get; private set; }
  }
}
