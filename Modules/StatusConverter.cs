﻿// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.StatusConverter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Modules
{
  public static class StatusConverter
  {
    private static readonly Dictionary<ContentLifecycleStatus, string> modelToUIMap = new Dictionary<ContentLifecycleStatus, string>();
    private static readonly Dictionary<string, ContentLifecycleStatus> uiToModelMap;

    static StatusConverter()
    {
      StatusConverter.modelToUIMap.Add(ContentLifecycleStatus.Live, ContentUIStatus.Published.ToString());
      StatusConverter.modelToUIMap.Add(ContentLifecycleStatus.Master, ContentUIStatus.Draft.ToString());
      StatusConverter.modelToUIMap.Add(ContentLifecycleStatus.Temp, ContentUIStatus.PrivateCopy.ToString());
      StatusConverter.uiToModelMap = new Dictionary<string, ContentLifecycleStatus>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      StatusConverter.uiToModelMap.Add(ContentUIStatus.Draft.ToString(), ContentLifecycleStatus.Master);
      StatusConverter.uiToModelMap.Add(ContentUIStatus.PrivateCopy.ToString(), ContentLifecycleStatus.Temp);
      StatusConverter.uiToModelMap.Add(ContentUIStatus.Published.ToString(), ContentLifecycleStatus.Live);
      StatusConverter.uiToModelMap.Add(ContentUIStatus.Scheduled.ToString(), ContentLifecycleStatus.Master);
    }

    public static string FromModel(ContentLifecycleStatus modelStatus)
    {
      string modelToUi = StatusConverter.modelToUIMap[modelStatus];
      try
      {
        return Res.Get<Labels>().Get(modelToUi);
      }
      catch
      {
        return modelToUi;
      }
    }

    public static string FromModelNotLocalzied(ContentLifecycleStatus modelStatus) => StatusConverter.modelToUIMap[modelStatus];

    public static ContentLifecycleStatus ToModel(string uiStatus) => StatusConverter.uiToModelMap[uiStatus];
  }
}
