// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.App
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Single entry point for Sitefinity API that provides the methods to do common tasks.
  /// </summary>
  public class App
  {
    /// <summary>
    /// Fired before executing data method. The method can be canceled.
    /// </summary>
    public static EventHandler<ExecutingEventArgs> Executing;
    /// <summary>Fired after executing data method.</summary>
    public static EventHandler<ExecutedEventArgs> Executed;

    /// <summary>
    /// Used to prepare the fluent API with specific provider names, transaction modes and so on.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> representing the configuration of the fluent API request.</returns>
    public static AppSettings Prepare() => new AppSettings();

    /// <summary>
    /// Used to prepare the fluent API with an instance of <see cref="T:Telerik.Sitefinity.Fluent.AppSettings" /> that has already been prepared by
    /// another instance of fluent API.
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static AppSettings Prepare(AppSettings settings) => settings;

    /// <summary>Used to get specific fluent API.</summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.FluentSitefinity" /> representing a single request to the fluent API.</returns>
    public static FluentSitefinity WorkWith() => new FluentSitefinity();

    /// <summary>
    /// Raises the <see cref="E:Executing" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutingEventArgs" /> instance containing the event data.</param>
    public static void OnExecuting(object sender, ExecutingEventArgs args)
    {
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (App.Executing == null)
        return;
      App.Executing(sender, args);
    }

    /// <summary>
    /// Raises the <see cref="E:Executed" /> event.
    /// </summary>
    /// <param name="args">The <see cref="T:Telerik.Sitefinity.Data.ExecutedEventArgs" /> instance containing the event data.</param>
    public static void OnExecuted(object sender, ExecutedEventArgs args)
    {
      if (sender == null)
        throw new ArgumentNullException(nameof (sender));
      if (args == null)
        throw new ArgumentNullException(nameof (args));
      if (App.Executed == null)
        return;
      App.Executed(sender, args);
    }
  }
}
