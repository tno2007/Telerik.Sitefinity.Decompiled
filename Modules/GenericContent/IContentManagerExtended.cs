// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.IContentManagerExtended
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  public interface IContentManagerExtended
  {
    /// <summary>Changes the item parent.</summary>
    /// <param name="item">The item.</param>
    /// <param name="newParent">The new parent.</param>
    /// <param name="recompileUrls">if set to <c>true</c> [recompile urls].</param>
    void ChangeItemParent(Content item, Content newParent, bool recompileUrls);
  }
}
