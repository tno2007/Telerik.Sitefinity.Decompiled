// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ScheduledTaskProgressBarElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.UI.Backend.Elements.Definitions;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Config
{
  internal class ScheduledTaskProgressBarElement : WidgetElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Backend.Elements.Config.ScheduledTaskProgressBarElement" /> class.
    /// </summary>
    /// <param name="parentElement">The parent element.</param>
    public ScheduledTaskProgressBarElement(ConfigElement parentElement)
      : base(parentElement)
    {
    }

    public override DefinitionBase GetDefinition() => (DefinitionBase) new ScheduledTaskProgressBarDefinition((ConfigElement) this);
  }
}
