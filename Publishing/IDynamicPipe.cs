// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.IDynamicPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Dynamic pipe interface</summary>
  public interface IDynamicPipe : IPipe
  {
    /// <summary>Sets the name of the pipe</summary>
    /// <param name="pipeName">The pipe name</param>
    void SetPipeName(string pipeName);
  }
}
