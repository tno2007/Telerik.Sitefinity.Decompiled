// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiciesPaths
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.Services
{
  public class ServiciesPaths : ConfigElement
  {
    public ServiciesPaths(ConfigElement parent)
      : base(parent)
    {
    }

    [ConfigurationProperty("workflowBaseUrl")]
    [DataMember]
    public virtual string WorkflowBaseUrl
    {
      get => (string) this["workflowBaseUrl"];
      set => this["workflowBaseUrl"] = (object) value;
    }
  }
}
