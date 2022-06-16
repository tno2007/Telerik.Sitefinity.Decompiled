// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.IContentFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// 
  /// </summary>
  public interface IContentFilter
  {
    /// <summary>Applies the specified content.</summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    string Apply(Content content, string html);
  }
}
