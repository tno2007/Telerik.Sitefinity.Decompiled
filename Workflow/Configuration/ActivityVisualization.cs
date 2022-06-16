// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Workflow.Configuration.ActivityVisualization
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Workflow.Configuration
{
  public class ActivityVisualization : ConfigElement
  {
    public ActivityVisualization(ConfigElement parent)
      : base(parent)
    {
    }

    internal ActivityVisualization()
      : base(false)
    {
    }

    [ConfigurationProperty("activityType", DefaultValue = "", IsKey = true, IsRequired = true)]
    public string ActivityType
    {
      get => (string) this["activityType"];
      set => this["activityType"] = (object) value;
    }

    [ConfigurationProperty("visible", DefaultValue = true)]
    public bool Visible
    {
      get => (bool) this["visible"];
      set => this["visible"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct Props
    {
      public const string activityType = "activityType";
      public const string visible = "visible";
    }
  }
}
