// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.PageTemplatesAvailability
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>Determines the available Page templates frameworks.</summary>
  public enum PageTemplatesAvailability
  {
    /// <summary>WebForms, MVC, and Hybrid are enabled.</summary>
    All,
    /// <summary>MVC and Hybrid are enabled.</summary>
    HybridAndMvc,
    /// <summary>MVC is only available.</summary>
    MvcOnly,
  }
}
