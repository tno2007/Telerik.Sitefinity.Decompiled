// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.IPipe
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>Publishing Point Pipe interface</summary>
  public interface IPipe
  {
    /// <summary>Gets the default settings.</summary>
    /// <returns>The default settings</returns>
    PipeSettings GetDefaultSettings();

    /// <summary>
    /// Gets the data structure of the medium this pipe works with
    /// </summary>
    IDefinitionField[] Definition { get; }

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    void Initialize(PipeSettings pipeSettings);

    /// <summary>
    /// Determines whether this instance can process the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns><c>true</c> if this instance can process the specified item; otherwise, <c>false</c>.</returns>
    bool CanProcessItem(object item);

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    string Name { get; }

    /// <summary>Gets or sets the pipe settings.</summary>
    /// <value>The pipe settings.</value>
    PipeSettings PipeSettings { get; set; }

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns>The pipe settings short description</returns>
    string GetPipeSettingsShortDescription(PipeSettings initSettings);

    /// <summary>Gets the type of the pipe settings.</summary>
    /// <value>The type of the pipe settings.</value>
    Type PipeSettingsType { get; }
  }
}
