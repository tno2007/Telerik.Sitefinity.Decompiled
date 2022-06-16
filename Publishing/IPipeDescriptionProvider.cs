// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.IPipeDescriptionProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  internal interface IPipeDescriptionProvider
  {
    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    /// <param name="description">The generated description.</param>
    /// <returns></returns>
    bool GetPipeSettingsShortDescription(PipeSettings pipeSettings, out string description);
  }
}
