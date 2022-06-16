// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Logging.ISitefinityLogCategoryConfigurator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Logging
{
  /// <summary>
  /// An extension point in Sitefinity log configuration process that allows individual
  /// log category configuration.
  /// </summary>
  /// <remarks>
  /// <para>
  /// The default implementation of this interface is resolved from the container
  /// and its <see cref="M:Telerik.Sitefinity.Logging.ISitefinityLogCategoryConfigurator.Configure(Telerik.Sitefinity.Logging.SitefinityLogCategory)" /> method is called for the configuration of each
  /// built-in log category.
  /// </para>
  /// <para>
  /// Unless complete overriding of the default configuration is desired,
  /// resolve the default implementation before registering your own and delegate
  /// to it, forming a chain of responsibility.
  /// </para>
  /// </remarks>
  public interface ISitefinityLogCategoryConfigurator
  {
    /// <summary>This method is called for each built-in log category.</summary>
    /// <param name="category">Provides the details about the log category being configured.</param>
    void Configure(SitefinityLogCategory category);
  }
}
