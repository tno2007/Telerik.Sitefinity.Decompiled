// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Model.EmailMessageTemplatesReportModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.UsageTracking.Model
{
  internal class EmailMessageTemplatesReportModel
  {
    public string ResolveKey { get; internal set; }

    public string ModuleName { get; internal set; }

    public string Subject { get; internal set; }

    public DateTime? LastModified { get; internal set; }

    public string TemplateSenderName { get; internal set; }
  }
}
