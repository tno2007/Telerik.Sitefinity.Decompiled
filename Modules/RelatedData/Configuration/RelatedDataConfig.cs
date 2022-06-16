// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.RelatedData.Configuration.RelatedDataConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.RelatedData.Configuration
{
  /// <summary>Represents related data configuration section.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "RelatedDataConfig")]
  public class RelatedDataConfig : ConfigSection
  {
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CheckRelatingDataWarningDescription", Title = "CheckRelatingDataWarningTitle")]
    [ConfigurationProperty("checkRelatingDataWarning", DefaultValue = false)]
    public bool CheckRelatingDataWarning
    {
      get => (bool) this["checkRelatingDataWarning"];
      set => this["checkRelatingDataWarning"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string checkRelatingDataWarning = "checkRelatingDataWarning";
    }
  }
}
