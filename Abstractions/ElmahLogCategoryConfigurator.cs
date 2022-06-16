// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.ElmahLogCategoryConfigurator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Logging;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>
  /// The built-in <see cref="T:Telerik.Sitefinity.Logging.ISitefinityLogCategoryConfigurator" /> implementation
  /// configuring ELMAH logging.
  /// </summary>
  internal class ElmahLogCategoryConfigurator : ISitefinityLogCategoryConfigurator
  {
    /// <inheritdoc />
    public void Configure(SitefinityLogCategory category) => category.Configuration.WithOptions.SendTo.Elmah(category.Name);
  }
}
