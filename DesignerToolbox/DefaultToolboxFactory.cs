// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DesignerToolbox.DefaultToolboxFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;

namespace Telerik.Sitefinity.DesignerToolbox
{
  internal class DefaultToolboxFactory : IToolboxFactory
  {
    /// <summary>
    /// Gets the instance of the <see cref="!:Toolboxes" /> config
    /// which is used as a toolbox store for this factory.
    /// </summary>
    protected virtual ToolboxesConfig ToolboxesConfig => Config.Get<ToolboxesConfig>();

    /// <inheritdoc />
    public IToolbox ResolveToolbox(string toolboxName)
    {
      if (string.IsNullOrEmpty(toolboxName))
        throw new ArgumentNullException(nameof (toolboxName));
      return (IToolbox) this.ToolboxesConfig.Toolboxes[toolboxName];
    }
  }
}
