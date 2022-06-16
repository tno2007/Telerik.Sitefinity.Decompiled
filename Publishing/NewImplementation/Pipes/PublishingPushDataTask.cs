// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.PublishingPushDataTask`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>
  /// Task used for publishing Page data to Publishing point asynchronously
  /// </summary>
  /// <typeparam name="T">The pipe type</typeparam>
  /// <seealso cref="T:Telerik.Sitefinity.BackgroundTasks.IBackgroundTask" />
  internal class PublishingPushDataTask<T> : IBackgroundTask where T : IPipe, IAsyncPushPipe
  {
    private IList<PublishingSystemEventInfo> items;
    private T pipe;
    private PipeSettings pipeSettings;
    private Guid pipeSettingsId;
    private string providerName;
    private Guid siteId;
    private CultureInfo culture;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.Pipes.PublishingPushDataTask`1" /> class.
    /// </summary>
    /// <param name="items">The items.</param>
    /// <param name="pipe">The pipe.</param>
    /// <param name="siteId">The site identifier.</param>
    /// <exception cref="T:System.ArgumentNullException">
    /// items
    /// or
    /// pipe
    /// or
    /// PipeSettings
    /// </exception>
    public PublishingPushDataTask(IList<PublishingSystemEventInfo> items, T pipe, Guid siteId)
    {
      if (items == null)
        throw new ArgumentNullException(nameof (items));
      if ((object) pipe == null)
        throw new ArgumentNullException(nameof (pipe));
      if (pipe.PipeSettings == null)
        throw new ArgumentNullException("PipeSettings");
      this.items = (IList<PublishingSystemEventInfo>) items.Select<PublishingSystemEventInfo, PublishingSystemEventInfo>((Func<PublishingSystemEventInfo, PublishingSystemEventInfo>) (i => new PublishingSystemEventInfo(i))).ToList<PublishingSystemEventInfo>();
      this.pipeSettingsId = pipe.PipeSettings.Id;
      this.providerName = pipe.PipeSettings.GetProviderName();
      this.siteId = siteId;
      this.culture = SystemManager.CurrentContext.Culture;
    }

    /// <summary>Gets the pipe.</summary>
    /// <value>The pipe.</value>
    protected T Pipe
    {
      get
      {
        if ((object) this.pipe == null)
          this.pipe = (T) PublishingSystemFactory.GetPipe(PublishingManager.GetManager(this.providerName).GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (s => s.Id == this.pipeSettingsId)).FirstOrDefault<PipeSettings>());
        return this.pipe;
      }
    }

    /// <inheritdoc />
    public void Run(IBackgroundTaskContext context) => SystemManager.RunWithElevatedPrivilege(new SystemManager.RunWithElevatedPrivilegeDelegate(this.RunTask));

    private void RunTask(object[] args)
    {
      using (new CultureRegion(this.culture))
      {
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        if (multisiteContext != null)
        {
          using (new SiteRegion(multisiteContext.GetSiteById(this.siteId)))
            this.Pipe.PushDataSynchronously(this.items);
        }
        else
          this.Pipe.PushDataSynchronously(this.items);
      }
    }
  }
}
