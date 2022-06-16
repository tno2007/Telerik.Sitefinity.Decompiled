// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Renderer.Editor.EditorState
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Renderer.Editor
{
  internal class EditorState
  {
    public EditorState() => this.WidgetState = (IList<Telerik.Sitefinity.Renderer.Editor.WidgetState>) new List<Telerik.Sitefinity.Renderer.Editor.WidgetState>();

    [DataMember]
    public int Version { get; set; }

    [DataMember]
    public bool HasChanged { get; set; }

    [DataMember]
    public bool AddAllowed { get; set; }

    [DataMember]
    public bool EditAllowed { get; set; }

    [DataMember]
    public IList<Telerik.Sitefinity.Renderer.Editor.WidgetState> WidgetState { get; set; }
  }
}
