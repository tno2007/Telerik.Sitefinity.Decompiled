// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.BackgroundTasks.Configuration.BackgroundTasksElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.BackgroundTasks.Configuration
{
  /// <summary>Configuration element for the BackgroundTasksService</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "BackgroundTasksConfigDescription", Title = "BackgroundTasksCaption")]
  public class BackgroundTasksElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.BackgroundTasks.Configuration.BackgroundTasksElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public BackgroundTasksElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the the maximum number of tasks that can be executed in parallel.
    /// The default value 0 means the number of processors of the current machine will be used.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "MaxBackgroundParallelTasksPerNodeDescription")]
    [ConfigurationProperty("maxBackgroundParallelTasksPerNode", DefaultValue = 0)]
    public int MaxBackgroundParallelTasksPerNode
    {
      get => this["maxBackgroundParallelTasksPerNode"] as int? ?? 0;
      set => this["maxBackgroundParallelTasksPerNode"] = value >= 0 ? (object) value : throw new ConfigurationException(Res.Get<ConfigDescriptions>().BackgroundTasksServiceNonNegativeValueMessage);
    }

    /// <summary>
    /// Determines the maximum timeout in milliseconds that will be given to the tasks to finish before they are aborted. Default value 0.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "WaitBeforeAbortTasksDescription")]
    [ConfigurationProperty("waitBeforeAbortTasks", DefaultValue = 0)]
    public int WaitBeforeAbortTasks
    {
      get => this["waitBeforeAbortTasks"] as int? ?? 0;
      set => this["waitBeforeAbortTasks"] = value >= 0 ? (object) value : throw new ConfigurationException(Res.Get<ConfigDescriptions>().BackgroundTasksServiceNonNegativeValueMessage);
    }

    /// <summary>
    /// Constants for the names of the configuration properties
    /// </summary>
    internal static class Names
    {
      internal const string MaxBackgroundParallelTasksPerNode = "maxBackgroundParallelTasksPerNode";
      internal const string WaitBeforeAbortTasks = "waitBeforeAbortTasks";
    }
  }
}
