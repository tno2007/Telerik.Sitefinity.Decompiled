// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.BasePipe`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Publishing Point Pipe base type</summary>
  /// <typeparam name="TPipeSettings">The pipe settings type.</typeparam>
  public abstract class BasePipe<TPipeSettings> : IPipe where TPipeSettings : PipeSettings
  {
    private TPipeSettings pipeSettings;
    private string publishingProviderName;

    /// <summary>Gets the default settings.</summary>
    /// <returns>The default settings</returns>
    public virtual PipeSettings GetDefaultSettings() => PublishingSystemFactory.CreatePipeSettings(this.Name, PublishingManager.GetManager(this.PublishingProviderName));

    /// <summary>
    /// Gets the data structure of the medium this pipe works with
    /// </summary>
    public abstract IDefinitionField[] Definition { get; }

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public abstract void Initialize(PipeSettings pipeSettings);

    /// <summary>
    /// Determines whether this instance can process the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns><c>true</c> if this instance can process the specified item; otherwise, <c>false</c>.</returns>
    public abstract bool CanProcessItem(object item);

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public abstract string Name { get; }

    /// <summary>Gets or sets the pipe settings.</summary>
    /// <value>The pipe settings.</value>
    public virtual PipeSettings PipeSettings
    {
      get => (PipeSettings) this.pipeSettings;
      set => this.pipeSettings = (TPipeSettings) value;
    }

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns>The pipe settings short description</returns>
    public virtual string GetPipeSettingsShortDescription(PipeSettings initSettings) => string.Empty;

    /// <summary>Gets the type of the pipe settings.</summary>
    /// <value>The type of the pipe settings.</value>
    public virtual Type PipeSettingsType => typeof (TPipeSettings);

    /// <summary>Gets or sets the provider name to use</summary>
    public virtual string PublishingProviderName
    {
      get
      {
        if (string.IsNullOrEmpty(this.publishingProviderName))
          this.publishingProviderName = Config.Get<PublishingConfig>().DefaultProvider;
        return this.publishingProviderName;
      }
      set => this.publishingProviderName = value;
    }

    /// <summary>Gets or sets pipe specific settings.</summary>
    protected TPipeSettings PipeSettingsInternal
    {
      get => this.pipeSettings;
      set => this.pipeSettings = value;
    }
  }
}
