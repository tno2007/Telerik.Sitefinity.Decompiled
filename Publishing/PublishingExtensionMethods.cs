// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingExtensionMethods
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Extension methods related to Sitefinity's publishing system
  /// </summary>
  public static class PublishingExtensionMethods
  {
    /// <summary>Get registered inbound pipes</summary>
    /// <param name="publishingPoint">Publishing point whose inbound pipes to look up</param>
    /// <returns>Array of inbound pipes</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="publishingPoint" /> is null.</exception>
    public static IPipe[] GetInboundPipes(this IPublishingPoint publishingPoint)
    {
      if (publishingPoint == null)
        throw new ArgumentNullException(nameof (publishingPoint));
      List<IPipe> pipeList = new List<IPipe>();
      for (int index = 0; index < publishingPoint.PipeSettings.Count; ++index)
      {
        PipeSettings pipeSetting = publishingPoint.PipeSettings[index];
        if (pipeSetting.IsInbound)
        {
          IPipe pipe = PublishingSystemFactory.GetPipe(pipeSetting.PipeName);
          pipe.Initialize(pipeSetting);
          pipeList.Add(pipe);
        }
      }
      return pipeList.ToArray();
    }

    /// <summary>Ger registered outbound pipes</summary>
    /// <param name="publishingPoint">Publishing point whose outbound pupes to look up</param>
    /// <returns>Array of outbound pipes</returns>
    /// <exception cref="T:System.ArgumentNullException">If <paramref name="publishingPoint" /> is null.</exception>
    public static IPipe[] GetOutboundPipes(this IPublishingPoint publishingPoint)
    {
      if (publishingPoint == null)
        throw new ArgumentNullException(nameof (publishingPoint));
      List<IPipe> pipeList = new List<IPipe>();
      for (int index = 0; index < publishingPoint.PipeSettings.Count; ++index)
      {
        PipeSettings pipeSetting = publishingPoint.PipeSettings[index];
        if (!pipeSetting.IsInbound)
        {
          IPipe pipe = PublishingSystemFactory.GetPipe(pipeSetting.PipeName);
          pipe.Initialize(pipeSetting);
          pipeList.Add(pipe);
        }
      }
      return pipeList.ToArray();
    }

    /// <summary>
    /// Get class name and namespace from the storage type name of a publishing point
    /// </summary>
    /// <param name="publishingPoint">Publishing point whose type name to parse</param>
    /// <returns>Dynamic type name with separated class name and namespace</returns>
    public static PublishingExtensionMethods.FullTypeName GetDynamicTypeName(
      this IPublishingPoint publishingPoint)
    {
      if (publishingPoint == null)
        throw new ArgumentNullException(nameof (publishingPoint));
      if (publishingPoint.StorageTypeName == null)
        throw new ArgumentException("publishingPoint.StorageTypeName is null.");
      int length = -1;
      if (!publishingPoint.StorageTypeName.IsNullOrEmpty())
        length = publishingPoint.StorageTypeName.LastIndexOf('.');
      PublishingExtensionMethods.FullTypeName dynamicTypeName;
      if (length > -1)
      {
        dynamicTypeName.Namespace = publishingPoint.StorageTypeName.Substring(0, length);
        dynamicTypeName.ClassName = publishingPoint.StorageTypeName.Substring(length + 1);
      }
      else
      {
        dynamicTypeName.Namespace = string.Empty;
        dynamicTypeName.ClassName = publishingPoint.StorageTypeName;
      }
      return dynamicTypeName;
    }

    /// <summary>
    /// Used internally to hold info about a full type name of a class
    /// </summary>
    public struct FullTypeName
    {
      /// <summary>Class name</summary>
      public string ClassName;
      /// <summary>Namespace</summary>
      public string Namespace;
    }
  }
}
